using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BusinessLogicLayer
{
   public class CustomerReceiptAdjustmentBl
    {
       public DataSet PopulateCustomerReceiptAdjustmentDetails()
       {
           ProcedureExecute proc = new ProcedureExecute("Prc_CustomerReceiptAdjustment_details");
           proc.AddVarcharPara("@Action", 50, "GetLoadDetails");
           proc.AddVarcharPara("@companyId", 20, Convert.ToString(HttpContext.Current.Session["LastCompany"]));
           proc.AddVarcharPara("@FinYear", 10, Convert.ToString(HttpContext.Current.Session["LastFinYear"]));
           proc.AddVarcharPara("@BranchList", -1, Convert.ToString(HttpContext.Current.Session["userbranchHierarchy"]));
           return proc.GetDataSet();
       }

       public DataTable GetAdvanceListAdd(string date, string customerId)
       {
           ProcedureExecute proc = new ProcedureExecute("Prc_CustomerReceiptAdjustment_details");
           proc.AddVarcharPara("@Action", 50, "GetAdvanceListAdd");
           proc.AddVarcharPara("@CustomerId", 15, customerId);
           proc.AddVarcharPara("@tranDate", 10, date);
           return proc.GetTable();
       }

       public DataTable GetDocumentList(string Mode, string ReceiptId, string customerId, string TransDate,string AdjId) 
       {
           ProcedureExecute proc = new ProcedureExecute("Prc_CustomerReceiptAdjustment_details");
           proc.AddVarcharPara("@Action", 50, "GetDocList");
           proc.AddVarcharPara("@CustomerId", 15, customerId);
           proc.AddIntegerPara("@recPayId", Convert.ToInt32(ReceiptId));
           proc.AddVarcharPara("@tranDate", 10, TransDate);
           proc.AddVarcharPara("@Mode", 10, Mode);
           proc.AddVarcharPara("@AdjId", 10, AdjId);
           return proc.GetTable();
       }


       public DataSet GetEditedData(string AdjId)
       {
           ProcedureExecute proc = new ProcedureExecute("Prc_CustomerReceiptAdjustment_details");
           proc.AddVarcharPara("@Action", 50, "GetEditedData");
           proc.AddVarcharPara("@AdjId", 10, AdjId);
           proc.AddVarcharPara("@companyId", 20, Convert.ToString(HttpContext.Current.Session["LastCompany"]));
           proc.AddVarcharPara("@FinYear", 10, Convert.ToString(HttpContext.Current.Session["LastFinYear"]));
           proc.AddVarcharPara("@BranchList", -1, Convert.ToString(HttpContext.Current.Session["userbranchHierarchy"]));
           return proc.GetDataSet();
       }

       public void AddEditAdvanceAdjustment(string Mode, string SchemeId, string Adjustment_No, string Adjustment_Date, string Branch, string Customer_id,
           string Adjusted_doc_id, string Adjusted_Doc_no, string Adjusted_DocAmt, string ExchangeRate, string Adjusted_DocAmt_inBaseCur,
           string Remarks, string Adjusted_DocOSAmt, string Adjusted_Amount, string userId, ref int AdjustedId, ref string ReturnNumber,
           DataTable AdjustmentTable, ref int ErrorCode, string Adj_id)
       {
           DataTable dsInst = new DataTable();
                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["DBConnectionDefault"]);
                SqlCommand cmd = new SqlCommand("prc_CustomerReceiptAdjustment_AddEdit", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Mode", Mode);
                cmd.Parameters.AddWithValue("@SchemeId", SchemeId);
                cmd.Parameters.AddWithValue("@Adjustment_No", Adjustment_No);
                cmd.Parameters.AddWithValue("@Adjustment_Date", Adjustment_Date);
                cmd.Parameters.AddWithValue("@Branch", Branch);
                cmd.Parameters.AddWithValue("@Customer_id", Customer_id);
                cmd.Parameters.AddWithValue("@Adjusted_doc_id", Adjusted_doc_id);
                cmd.Parameters.AddWithValue("@Adjusted_Doc_no", Adjusted_Doc_no);
                cmd.Parameters.AddWithValue("@Adjusted_DocAmt", Adjusted_DocAmt);
                cmd.Parameters.AddWithValue("@ExchangeRate", ExchangeRate);
                cmd.Parameters.AddWithValue("@Adjusted_DocAmt_inBaseCur", Adjusted_DocAmt_inBaseCur);
                cmd.Parameters.AddWithValue("@Remarks", Remarks);
                cmd.Parameters.AddWithValue("@Adjusted_DocOSAmt", Adjusted_DocOSAmt);
                cmd.Parameters.AddWithValue("@Adjusted_Amount", Adjusted_Amount);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@DetailTable", AdjustmentTable);
                cmd.Parameters.AddWithValue("@Adj_id", Adj_id);
                cmd.Parameters.AddWithValue("@FinYear", HttpContext.Current.Session["LastFinYear"].ToString());
                cmd.Parameters.AddWithValue("@CompanyID", HttpContext.Current.Session["LastCompany"].ToString());

                cmd.Parameters.Add("@ReturnValue", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@ReturnId", SqlDbType.VarChar,10);
                cmd.Parameters.Add("@ErrorCode", SqlDbType.Int);

                cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;
                cmd.Parameters["@ReturnId"].Direction = ParameterDirection.Output;
                cmd.Parameters["@ErrorCode"].Direction = ParameterDirection.Output;

                cmd.CommandTimeout = 0;
                SqlDataAdapter Adap = new SqlDataAdapter();
                Adap.SelectCommand = cmd;
                Adap.Fill(dsInst);
            
                AdjustedId = Convert.ToInt32(cmd.Parameters["@ReturnId"].Value);
                ReturnNumber = Convert.ToString(cmd.Parameters["@ReturnValue"].Value);
                ErrorCode = Convert.ToInt32(cmd.Parameters["@ErrorCode"].Value);
       }
    }
}

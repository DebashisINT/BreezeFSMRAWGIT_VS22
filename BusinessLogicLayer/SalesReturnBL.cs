using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Web;
using EntityLayer;

namespace BusinessLogicLayer
{
    public class SalesReturnBL
    {
        public DataSet GetAllDropDownDetailForSalesChallan(string userbranch, string CompanyID, string BranchID, string Year)
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 100, "GetAllDropDownDetailForSalesChallan");
            proc.AddNTextPara("@BranchList",  userbranch);
            proc.AddVarcharPara("@CompanyID", 100, CompanyID);
            proc.AddVarcharPara("@BrID", 100, BranchID);
            proc.AddVarcharPara("@FinYear", 100, Year);
            ds = proc.GetDataSet();
            return ds;
        }
        public DataSet GetAllDropDownDetailForSalesInvoice(string userbranch, string CompanyID, string BranchID, string Year)
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 100, "GetAllDropDownDetailForSalesInvoice");
            proc.AddNTextPara("@BranchList", userbranch);
            proc.AddVarcharPara("@CompanyID", 100, CompanyID);
            proc.AddVarcharPara("@BrID", 100, BranchID);
            proc.AddVarcharPara("@FinYear", 100, Year);
            ds = proc.GetDataSet();
            return ds;
        }
        public DataTable PopulateContactPersonPhone(string InternalId)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 100, "PopulateContactPersonPhone");
            proc.AddVarcharPara("@InternalId", 100, InternalId);
            ds = proc.GetTable();
            return ds;
        }
        public DataTable PopulateCustomerDetail()
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 100, "PopulateCustomerDetail");
            dt = proc.GetTable();
            return dt;
        }
        public DataTable GetCustomerDetails_InvoiceRelated(string strCustomerID)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 100, "GetCustomerInvoiceDetails");
            proc.AddVarcharPara("@CustomerID", 1000, strCustomerID);
            dt = proc.GetTable();
            return dt;
        }
        public DataTable GetSalesReturnListGridData(string userbranchlist, string lastCompany, string Fiyear, string userbranchID, string FromDate, string ToDate)
        {

            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 500, "GetSalesReturnListGridData");
            proc.AddNTextPara("@BranchList", userbranchlist);
            proc.AddVarcharPara("@CompanyID", 50, lastCompany);
            proc.AddVarcharPara("@FinYear", 50, Fiyear);

            proc.AddVarcharPara("@Branch", 300, userbranchID);

            proc.AddDateTimePara("@FinYearStartdate", Convert.ToDateTime(FromDate));
            proc.AddDateTimePara("@FinYearEnddate", Convert.ToDateTime(ToDate));
            dt = proc.GetTable();
            return dt;
        }
        public DataTable GetSalesReturnNormalListGridData(string userbranchlist, string lastCompany, string Fiyear, string userbranchID, string FromDate, string ToDate)
        {

            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 500, "GetSalesReturnNormalListGridData");
            proc.AddNTextPara("@BranchList", userbranchlist);
            proc.AddVarcharPara("@CompanyID", 50, lastCompany);
            proc.AddVarcharPara("@FinYear", 50, Fiyear);

            proc.AddVarcharPara("@Branch", 300, userbranchID);

            proc.AddDateTimePara("@FinYearStartdate", Convert.ToDateTime(FromDate));
            proc.AddDateTimePara("@FinYearEnddate", Convert.ToDateTime(ToDate));
            dt = proc.GetTable();
            return dt;
        }
        public DataTable GetUndeliveryReturnListGridData(string userbranchlist, string lastCompany, string Fiyear, string userbranchID, string FromDate, string ToDate)
        {

            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 500, "GetUndeliveryReturnListGridData");
            proc.AddNTextPara("@BranchList", userbranchlist);
            proc.AddVarcharPara("@CompanyID", 50, lastCompany);
            proc.AddVarcharPara("@FinYear", 50, Fiyear);

            proc.AddVarcharPara("@Branch", 300, userbranchID);

            proc.AddDateTimePara("@FinYearStartdate", Convert.ToDateTime(FromDate));
            proc.AddDateTimePara("@FinYearEnddate", Convert.ToDateTime(ToDate));
            dt = proc.GetTable();
            return dt;
        }

        public DataTable GetSalesReturnStockInNormalListGridData(string userbranchlist, string lastCompany, string Fiyear, string StartDate, string EndDate, string ubranchlist, string userbranchID)
        {

            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 500, "GetSalesReturnStockInNormalListGridData");
            proc.AddNTextPara("@BranchList",userbranchlist);
            proc.AddNTextPara("@TransferBranchList", ubranchlist);
            proc.AddVarcharPara("@CompanyID", 50, lastCompany);
            proc.AddDateTimePara("@FinYearStartdate", Convert.ToDateTime(StartDate));
            proc.AddDateTimePara("@FinYearEnddate", Convert.ToDateTime(EndDate));
            proc.AddVarcharPara("@FinYear", 50, Fiyear);
            proc.AddVarcharPara("@Branch", 300, userbranchID);
            dt = proc.GetTable();
            return dt;
        }

        //public DataTable GetCustomerReturnListGridData(string userbranchlist, string lastCompany, string StartDate, string EndDate, string DocType)
        //{

        //    DataTable dt = new DataTable();
        //    ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
        //    proc.AddVarcharPara("@Action", 500, "GetCustomerReturnListGridData");
        //    proc.AddNTextPara("@BranchList", userbranchlist);
        //    proc.AddVarcharPara("@CompanyID", 50, lastCompany);
        //    proc.AddDateTimePara("@FinYearStartdate", Convert.ToDateTime(StartDate));
        //    proc.AddDateTimePara("@FinYearEnddate", Convert.ToDateTime(EndDate));

        //    proc.AddVarcharPara("@Type", 50, DocType);
        //    dt = proc.GetTable();
        //    return dt;
        //}
        public DataTable GetCustomerReturnListGridData(string userbranchlist, string lastCompany, string Fiyear, string userbranchID, string FromDate, string ToDate, string DocType)
        {

            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 500, "GetCustomerReturnListGridData");
            proc.AddNTextPara("@BranchList", userbranchlist);
            proc.AddVarcharPara("@CompanyID", 50, lastCompany);
            proc.AddVarcharPara("@FinYear", 50, Fiyear);

            proc.AddVarcharPara("@Branch", 300, userbranchID);

            proc.AddDateTimePara("@FinYearStartdate", Convert.ToDateTime(FromDate));
            proc.AddDateTimePara("@FinYearEnddate", Convert.ToDateTime(ToDate));

            proc.AddVarcharPara("@Type", 50, DocType);
            dt = proc.GetTable();
            return dt;
        }

        public DataTable GetIssueToCustomerReturnListGridData(string userbranchlist, string lastCompany, string Fiyear, string userbranchID, string FromDate, string ToDate, string DocType)
        {

            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 500, "GetIssueToCustomerReturnListGridData");
            proc.AddNTextPara("@BranchList", userbranchlist);
            proc.AddVarcharPara("@CompanyID", 50, lastCompany);
            proc.AddVarcharPara("@FinYear", 50, Fiyear);

            proc.AddVarcharPara("@Branch", 300, userbranchID);

            proc.AddDateTimePara("@FinYearStartdate", Convert.ToDateTime(FromDate));
            proc.AddDateTimePara("@FinYearEnddate", Convert.ToDateTime(ToDate));

            proc.AddVarcharPara("@Type", 50, DocType);
            dt = proc.GetTable();
            return dt;
        }


        public DataTable GetCustomerStockInReturnListGridData(string userbranchlist, string lastCompany , string Fiyear,string StartDate, string EndDate, string DocType, string ubranchlist, string userbranchID)
        {

            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 500, "GetCustomerStockInReturnListGridData");
            proc.AddNTextPara("@BranchList", userbranchlist);
            proc.AddVarcharPara("@CompanyID", 50, lastCompany);
            proc.AddDateTimePara("@FinYearStartdate", Convert.ToDateTime(StartDate));
            proc.AddDateTimePara("@FinYearEnddate", Convert.ToDateTime(EndDate));
            proc.AddNTextPara("@TransferBranchList", ubranchlist);
            proc.AddVarcharPara("@Type", 50, DocType);
            proc.AddVarcharPara("@FinYear", 50, Fiyear);

            proc.AddVarcharPara("@Branch", 300, userbranchID);
            dt = proc.GetTable();
            return dt;
        }



        public DataTable GetSalesReturnEditData(string SalesReturnID)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 500, "SalesReturnEditDetails");
            proc.AddVarcharPara("@SalesReturnID", 500, SalesReturnID);
            dt = proc.GetTable();
            return dt;
        }
        public DataTable GetSalesReturnNormalStockEditData(string SalesReturnID)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 500, "SalesReturnNormalStockInEditDetails");
            proc.AddVarcharPara("@SalesReturnID", 500, SalesReturnID);
            dt = proc.GetTable();
            return dt;
        }
        public DataTable GetSalesReturnBranchEditData(string SalesReturnID)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 500, "SalesReturnBranchEditDetails");
            proc.AddVarcharPara("@SalesReturnID", 500, SalesReturnID);
            dt = proc.GetTable();
            return dt;
        }
        public DataSet GetSalesReturnProductData(string strSalesReturnID, string LastFinYear, string LastCompany)
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 50, "SalesReturnProductDetails");
            proc.AddVarcharPara("@SalesReturnID", 500, strSalesReturnID);
            proc.AddVarcharPara("@FinYear", 100, LastFinYear);
            proc.AddVarcharPara("@campany_Id", 100, LastCompany);
            ds = proc.GetDataSet();
            return ds;
        }

        public DataSet GetCustomerReturnProductData(string strSalesReturnID, string LastFinYear, string LastCompany)
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 50, "CustomerReturnProductDetails");
            proc.AddVarcharPara("@SalesReturnID", 500, strSalesReturnID);
            proc.AddVarcharPara("@FinYear", 100, LastFinYear);
            proc.AddVarcharPara("@campany_Id", 100, LastCompany);
            ds = proc.GetDataSet();
            return ds;
        }
        public DataSet GetSalesReturnTaggingProductData(string strSalesReturnID, string LastFinYear, string LastCompany)
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 50, "SalesReturnTaggingProductDetails");
            proc.AddVarcharPara("@SalesReturnID", 500, strSalesReturnID);
            proc.AddVarcharPara("@FinYear", 100, LastFinYear);
            proc.AddVarcharPara("@campany_Id", 100, LastCompany);
            ds = proc.GetDataSet();
            return ds;
        }

        public DataSet GetCustomerReturnTaggingProductData(string strSalesReturnID, string LastFinYear, string LastCompany)
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 50, "CustomerReturnTaggingProductDetails");
            proc.AddVarcharPara("@SalesReturnID", 500, strSalesReturnID);
            proc.AddVarcharPara("@FinYear", 100, LastFinYear);
            proc.AddVarcharPara("@campany_Id", 100, LastCompany);
            ds = proc.GetDataSet();
            return ds;
        }

        public DataTable GetSalesReturnBillingAddress(string strSalesReturnID)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 50, "SalesReturnBillingAddress");
            proc.AddVarcharPara("@SalesReturnID", 500, strSalesReturnID);
            dt = proc.GetTable();
            return dt;
        }


        public int DeleteSalesReturn(string strSalesReturnID, string comapanyid, string finyear, string ComponentType, string branch)
        {
            int i;
            int rtrnvalue = 0;

            if (!string.IsNullOrEmpty(strSalesReturnID) && !string.IsNullOrEmpty(comapanyid) && !string.IsNullOrEmpty(finyear))
            {
                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn");
                proc.AddVarcharPara("@Action", 100, "DeleteSalesReturnDetails");
                proc.AddVarcharPara("@DeleteSalesReturnId", 100, strSalesReturnID);
                proc.AddVarcharPara("@CompanyID", 100, comapanyid);
                proc.AddVarcharPara("@FinYear", 100, finyear);
                proc.AddVarcharPara("@ComponentType", 50, ComponentType);                
                proc.AddVarcharPara("@IsEditidMode", 10, "N");
                proc.AddVarcharPara("@BranchID", 100, branch);
                proc.AddVarcharPara("@ReturnValue", 50, "0", QueryParameterDirection.Output);
                i = proc.RunActionQuery();
                rtrnvalue = Convert.ToInt32(proc.GetParaValue("@ReturnValue"));
            }

            return rtrnvalue;


        
        }

        public DataTable GetComponent(string Customer, string Date, string strReturnID, string BranchId)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 500, "GetInvoice");
            proc.AddVarcharPara("@CustomerID", 500, Customer);
            proc.AddVarcharPara("@ReturnID", 20, strReturnID);
            proc.AddIntegerPara("@branchId", Convert.ToInt32(BranchId));
            // proc("@Date",  Convert.ToDateTime(Date).ToString("YYYY-mm-dd"));
            proc.AddVarcharPara("@Date", 30, Date);
            dt = proc.GetTable();
            return dt;
        }

        public DataTable GetSRComponent(string Customer, string Date, string strReturnID, string BranchId)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 500, "GetSalesReturnInvoice");
            proc.AddVarcharPara("@CustomerID", 500, Customer);
            proc.AddVarcharPara("@ReturnID", 20, strReturnID);
            proc.AddIntegerPara("@branchId", Convert.ToInt32(BranchId));
            // proc("@Date",  Convert.ToDateTime(Date).ToString("YYYY-mm-dd"));
            proc.AddVarcharPara("@Date", 30, Date);
            dt = proc.GetTable();
            return dt;
        }
        public DataTable GetNormalInvoice(string Customer, string Date, string strReturnID, string BranchId)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 500, "GetNormalInvoice");
            proc.AddVarcharPara("@CustomerID", 500, Customer);
            proc.AddVarcharPara("@ReturnID", 20, strReturnID);
            proc.AddIntegerPara("@branchId", Convert.ToInt32(BranchId));
            // proc("@Date",  Convert.ToDateTime(Date).ToString("YYYY-mm-dd"));
            proc.AddVarcharPara("@Date", 30, Date);
            dt = proc.GetTable();
            return dt;
        }
        public DataTable GetStkWHDetails(string Type, string strReturnID)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 500, "GetStkWHDetails");
            proc.AddVarcharPara("@Type", 100, Type);
            proc.AddVarcharPara("@ReturnID", 50, strReturnID);
          
            dt = proc.GetTable();
            return dt;
        }

        public DataTable GetInvoiceWithoutChallan(string Customer, string Date, string strReturnID, string BranchId)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 500, "GetInvoiceWithoutChallan");
            proc.AddVarcharPara("@CustomerID", 500, Customer);
            proc.AddVarcharPara("@ReturnID", 20, strReturnID);
            proc.AddIntegerPara("@branchId", Convert.ToInt32(BranchId));
            // proc("@Date",  Convert.ToDateTime(Date).ToString("YYYY-mm-dd"));
            proc.AddVarcharPara("@Date", 30, Date);
            dt = proc.GetTable();
            return dt;
        }
        public DataTable GetTaxDetailsSI(string InvoiceID)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 500, "TaxDetailsSI");
            proc.AddVarcharPara("@invoice_ID", 500, InvoiceID);
            dt = proc.GetTable();
            return dt;
        }
        public DataTable GetTaxDetailsSC(string ChallanID)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 500, "TaxDetailsSC");
            proc.AddVarcharPara("@challan_ID", 500, ChallanID);
            dt = proc.GetTable();
            return dt;
        }
        public DataTable GetTaxDetailsSR(string CustomerReturnID)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 500, "TaxDetailsCR");
            proc.AddVarcharPara("@ReturnID", 50, CustomerReturnID);
            dt = proc.GetTable();
            return dt;
        }
        public DataTable GetCustomerGSTIN(string Customer)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 500, "GetGSTIN");
            proc.AddVarcharPara("@InternalId", 500, Customer);

            dt = proc.GetTable();
            return dt;
        }

        public DataTable GetCustomerNewGSTIN(string Customer, string BranchId)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 500, "GetGstnDetails");
            proc.AddVarcharPara("@InternalId", 500, Customer);
            proc.AddIntegerPara("@branchId", Convert.ToInt32(BranchId));
            dt = proc.GetTable();
            return dt;
        }
        public DataTable GetChallanComponent(string Customer, string Date, string strReturnID, string BranchId)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 500, "GetChallan");
            proc.AddVarcharPara("@CustomerID", 500, Customer);
            proc.AddVarcharPara("@ReturnID", 20, strReturnID);
            proc.AddIntegerPara("@branchId", Convert.ToInt32(BranchId));
            // proc("@Date",  Convert.ToDateTime(Date).ToString("YYYY-mm-dd"));
            proc.AddVarcharPara("@Date", 30, Date);
            dt = proc.GetTable();
            return dt;
        }
        public DataTable GetCustomerReturnComponent(string Customer, string Date, string strReturnID, string BranchId)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 500, "GetCustomerReturn");
            proc.AddVarcharPara("@CustomerID", 500, Customer);
            proc.AddVarcharPara("@ReturnID", 20, strReturnID);
            proc.AddIntegerPara("@branchId", Convert.ToInt32(BranchId));
            // proc("@Date",  Convert.ToDateTime(Date).ToString("YYYY-mm-dd"));
            proc.AddVarcharPara("@Date", 30, Date);
            dt = proc.GetTable();
            return dt;
        }
        public DataTable GetCustomerOutStanding(string Customer)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 500, "GetCustomerOutStanding");
            proc.AddVarcharPara("@CustomerID", 500, Customer);

            dt = proc.GetTable();
            return dt;
        }

        #region Get Invoice  Date
        public DataTable GetInvoiceDate(string InvoiceNo)
        {
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Invoice_Number", 100, InvoiceNo);
            proc.AddVarcharPara("@Action", 100, "GetActivityTypeList");

            return proc.GetTable();
        }
        #endregion  Get Invoice  Date

        #region Get Challan  Date
        public DataTable GetChallanDate(string ChallanNo)
        {
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Invoice_Number", 100, ChallanNo);
            proc.AddVarcharPara("@Action", 100, "GetChallanDate");

            return proc.GetTable();
        }
        #endregion  Get Challan  Date

        #region Get Sales Return Date
        public DataTable GetCustomerReturnDate(string ReturnNo)
        {
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Invoice_Number", 100, ReturnNo);
            proc.AddVarcharPara("@Action", 100, "GetCustomerReturnDate");

            return proc.GetTable();
        }
        #endregion  Get Sales Return Date
        public DataTable GetSchemaLengthSalesReturn()
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("[prc_CRMSalesReturn_Details]");

            proc.AddVarcharPara("@Action", 100, "GetSchemaLengthSalesReturn");

            dt = proc.GetTable();
            return dt;
        }
        public DataTable GetSchemaLengthCustomerReturn()
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("[prc_CRMSalesReturn_Details]");

            proc.AddVarcharPara("@Action", 100, "GetSchemaLengthCustomerReturn");

            dt = proc.GetTable();
            return dt;
        }
        
        public DataTable GetInvoiceDetailsOnly(string Indent_Id, string Type)
        {
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 100, "GetInvoiceDetailsOnly");
            proc.AddVarcharPara("@invoice_ID", 4000, Indent_Id);
            proc.AddVarcharPara("@Type", 10, Type);
            return proc.GetTable();
        }

        public DataTable GetInvoiceDetailsForSecondHandSale(string Indent_Id)
        {
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 100, "GetInvoiceDetailsForSecondHandSale");
            proc.AddVarcharPara("@invoice_ID", 4000, Indent_Id);
          
            return proc.GetTable();
        }
        public DataTable GetOldUnitDetails(string invoice_ID)
        {
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 100, "GetOldUnitDetails");
            proc.AddVarcharPara("@invoice_ID", 4000, invoice_ID);
         
            return proc.GetTable();
        }

        public DataTable GetChallanDetailsOnly(string Indent_Id, string Type)
        {
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 100, "GetChallanDetailsOnly");
            proc.AddVarcharPara("@invoice_ID", 4000, Indent_Id);
            proc.AddVarcharPara("@Type", 10, Type);
            return proc.GetTable();
        }
        public DataTable GetIndentDetailsForPOGridBind(string Indent_Id, string Order_Key, string Product_Ids, string comapanyid, string finyear)
        {
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 100, "GetInvoiceforSR");
            proc.AddVarcharPara("@invoice_ID", 4000, Indent_Id);
            proc.AddVarcharPara("@OrderID_Key", 1000, Order_Key);
            proc.AddVarcharPara("@Product_Id", 1000, Product_Ids);
            proc.AddVarcharPara("@campany_Id", 100, comapanyid);
            proc.AddVarcharPara("@FinYear", 100, finyear);

            return proc.GetTable();
        }



        public DataTable PopulateCustomerInEditMode(string strCustomerID)
        {
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 100, "PopulateCustomerInEditMode");
            proc.AddVarcharPara("@CustomerID", 1000, strCustomerID);

            return proc.GetTable();
        }
        public DataTable GetIndentDetailsForUSRGridBind(string Indent_Id, string Order_Key, string comapanyid, string finyear)
        {
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 100, "GetInvoiceforUSR");
            proc.AddVarcharPara("@invoice_ID", 4000, Indent_Id);
            proc.AddVarcharPara("@OrderID_Key", 1000, Order_Key);
           
            proc.AddVarcharPara("@campany_Id", 100, comapanyid);
            proc.AddVarcharPara("@FinYear", 100, finyear);

            return proc.GetTable();
        }
        public DataTable GetIndentDetailsForCRGridBind(string Indent_Id, string Order_Key, string Product_Ids, string comapanyid, string finyear)
        {
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 100, "GetChallanforCR");
            proc.AddVarcharPara("@invoice_ID", 4000, Indent_Id);
            proc.AddVarcharPara("@OrderID_Key", 1000, Order_Key);
            proc.AddVarcharPara("@Product_Id", 1000, Product_Ids);
            proc.AddVarcharPara("@campany_Id", 100, comapanyid);
            proc.AddVarcharPara("@FinYear", 100, finyear);

            return proc.GetTable();
        }



        public DataTable GetReturnforCRI(string Indent_Id,string comapanyid, string finyear)
        {
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 100, "GetReturnforCRI");
            proc.AddVarcharPara("@invoice_ID", 4000, Indent_Id);
           // proc.AddVarcharPara("@OrderID_Key", 1000, Order_Key);
          //  proc.AddVarcharPara("@Product_Id", 1000, Product_Ids);
            proc.AddVarcharPara("@campany_Id", 100, comapanyid);
            proc.AddVarcharPara("@FinYear", 100, finyear);

            return proc.GetTable();
        }
        public DataTable GetNecessaryData(string strComponentIDs, string strType)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 100, "GetComponentSelectData");
            proc.AddVarcharPara("@SelectedComponentList", 1000, strComponentIDs);
            proc.AddVarcharPara("@ComponentType", 1000, strType);
            dt = proc.GetTable();
            return dt;
        }
        public int IsPoSAdjustment(string ReturnID)
        {
            DataTable dt = new DataTable();
            int i = 0;
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 500, "IsPoSAdjustment");
            proc.AddVarcharPara("@SalesReturnID", 500, Convert.ToString(ReturnID));
            dt = proc.GetTable();
            if (dt != null && dt.Rows.Count > 0)
            {
                if (Convert.ToInt32(dt.Rows[0]["OUTPUT"]) > 0)
                {
                    i = 1;
                }
            }

            return i;
        }




        public DataTable GetProductDetails(string LastFinYear, string LastCompany)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 50, "ProductDetails");
          
            proc.AddVarcharPara("@FinYear", 100, LastFinYear);
            proc.AddVarcharPara("@campany_Id", 100, LastCompany);
            dt = proc.GetTable();
            return dt;
        }




        public DataTable PopulatePrOnDemand(string filter, int startindex, int EndIndex, string companyid, string FinYear)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CRMSalesReturn_Details");
            proc.AddVarcharPara("@Action", 500, "ProductLoadOnDemandDetails");
            //proc.AddVarcharPara("@branchId", 20, branchId);
            proc.AddVarcharPara("@filter", 20, filter);
            proc.AddIntegerPara("@startindex", startindex);
            proc.AddIntegerPara("@EndIndex", EndIndex);
            proc.AddVarcharPara("@CompanyId", 10, companyid);
            proc.AddVarcharPara("@FinYear", 10, FinYear);
           
           
            dt = proc.GetTable();
            return dt;
        }
    }
}

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
using System.Configuration;

namespace BusinessLogicLayer
{
    public class DebitCreditNoteBL
    {
        public DataTable GetSearchGridData(string BranchList, string CompanyID, string FinYear, string Action)
        {

            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("proc_DebitCreditNoteDetails");
            proc.AddVarcharPara("@BranchList", 500, BranchList);
            proc.AddVarcharPara("@CompanyID", 50, CompanyID);
            proc.AddVarcharPara("@FinYear", 50, FinYear);
            proc.AddVarcharPara("@Action", 500, Action);
            dt = proc.GetTable();
            return dt;
        }
        public DataTable GetNoteDetails(string Action, string NoteID)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("proc_DebitCreditNoteDetails");
            proc.AddVarcharPara("@Action", 500, Action);
            proc.AddVarcharPara("@NoteID", 500, NoteID);
            dt = proc.GetTable();
            return dt;
        }

        public void ModifyDrCrNote(string ActionType, string NotelNo, string BillNo, string FinYear, string CompanyID, string BranchID, string NoteDate,
                                   string CurrencyID, string Narration, string NoteType, string CustomerName, string InvoiceNo, string Currency, decimal Rate, string UserID, DataTable JournalDetails,
                                   ref int strIsComplete, ref int strOutNotelNo, string strPartyInvoice, string strPartyDate,DataTable tempBillAddress, DataTable TaxDetailTable)
        {
            try
            {

                DataSet dsInst = new DataSet();
                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["DBConnectionDefault"]);
                SqlCommand cmd = new SqlCommand("prc_InsertDebitCreditNoteEntry", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", ActionType);
                cmd.Parameters.AddWithValue("@NoteID", NotelNo);
                cmd.Parameters.AddWithValue("@BillNo", BillNo);
                cmd.Parameters.AddWithValue("@FinYear", FinYear);
                cmd.Parameters.AddWithValue("@CompanyID", CompanyID);
                cmd.Parameters.AddWithValue("@BranchID", BranchID);
                cmd.Parameters.AddWithValue("@NoteDate", Convert.ToDateTime(NoteDate));
                cmd.Parameters.AddWithValue("@CurrencyID", CurrencyID);
                cmd.Parameters.AddWithValue("@Narration", Narration);
                cmd.Parameters.AddWithValue("@NoteType", NoteType);
                cmd.Parameters.AddWithValue("@CustomerName", CustomerName);
                cmd.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
                cmd.Parameters.AddWithValue("@DCNote_CurrencyId", Currency);
                cmd.Parameters.AddWithValue("@Rate", Rate);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@JournalDetails", JournalDetails);

                cmd.Parameters.AddWithValue("@PartyInvoiceNo", strPartyInvoice);
                cmd.Parameters.AddWithValue("@PartyInvoiceDate", strPartyDate);
                cmd.Parameters.AddWithValue("@BillAddress", tempBillAddress);
                cmd.Parameters.AddWithValue("@TaxDetail", TaxDetailTable);

                cmd.Parameters.Add("@OutNoteID", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@ReturnValue", SqlDbType.VarChar, 50);

                cmd.Parameters["@OutNoteID"].Direction = ParameterDirection.Output;
                cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;

                cmd.CommandTimeout = 0;
                SqlDataAdapter Adap = new SqlDataAdapter();
                Adap.SelectCommand = cmd;
                Adap.Fill(dsInst);

                strIsComplete = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
                strOutNotelNo = Convert.ToInt32(cmd.Parameters["@OutNoteID"].Value.ToString());

                cmd.Dispose();
                con.Dispose();
            }
            catch (Exception ex)
            {
            }
        }

        //public void ModifyDrCrNote(string ActionType, string NotelNo, string BillNo, string FinYear, string CompanyID, string BranchID, string NoteDate,
        //                            string CurrencyID, string Narration, string NoteType, string CustomerName, string InvoiceNo, string Currency, decimal Rate, string UserID, DataTable JournalDetails,
        //                            ref int strIsComplete, ref int strOutNotelNo, string strPartyInvoice, string strPartyDate, DataTable tempBillAddress)
        //{
        //    try
        //    {
        //        DataSet dsInst = new DataSet();
        //        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["DBConnectionDefault"]);
        //        SqlCommand cmd = new SqlCommand("prc_InsertDebitCreditNoteEntry", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@Action", ActionType);
        //        cmd.Parameters.AddWithValue("@NoteID", NotelNo);
        //        cmd.Parameters.AddWithValue("@BillNo", BillNo);
        //        cmd.Parameters.AddWithValue("@FinYear", FinYear);
        //        cmd.Parameters.AddWithValue("@CompanyID", CompanyID);
        //        cmd.Parameters.AddWithValue("@BranchID", BranchID);
        //        cmd.Parameters.AddWithValue("@NoteDate", Convert.ToDateTime(NoteDate));
        //        cmd.Parameters.AddWithValue("@CurrencyID", CurrencyID);
        //        cmd.Parameters.AddWithValue("@Narration", Narration);
        //        cmd.Parameters.AddWithValue("@NoteType", NoteType);
        //        cmd.Parameters.AddWithValue("@CustomerName", CustomerName);
        //        cmd.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
        //        cmd.Parameters.AddWithValue("@DCNote_CurrencyId", Currency);
        //        cmd.Parameters.AddWithValue("@Rate", Rate);
        //        cmd.Parameters.AddWithValue("@UserID", UserID);
        //        cmd.Parameters.AddWithValue("@JournalDetails", JournalDetails);

        //        cmd.Parameters.AddWithValue("@PartyInvoiceNo", strPartyInvoice);
        //        cmd.Parameters.AddWithValue("@PartyInvoiceDate", strPartyDate);
        //        cmd.Parameters.AddWithValue("@BillAddress", tempBillAddress);

        //        cmd.Parameters.Add("@OutNoteID", SqlDbType.VarChar, 50);
        //        cmd.Parameters.Add("@ReturnValue", SqlDbType.VarChar, 50);

        //        cmd.Parameters["@OutNoteID"].Direction = ParameterDirection.Output;
        //        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;

        //        cmd.CommandTimeout = 0;
        //        SqlDataAdapter Adap = new SqlDataAdapter();
        //        Adap.SelectCommand = cmd;
        //        Adap.Fill(dsInst);

        //        strIsComplete = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        //        strOutNotelNo = Convert.ToInt32(cmd.Parameters["@OutNoteID"].Value.ToString());

        //        cmd.Dispose();
        //        con.Dispose();
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}
        public void DeleteDrCrNote(string ActionType, string NotelNo, ref int strIsComplete)
        {
            try
            {
                DataSet dsInst = new DataSet();
                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["DBConnectionDefault"]);
                SqlCommand cmd = new SqlCommand("prc_InsertDebitCreditNoteEntry", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", ActionType);
                cmd.Parameters.AddWithValue("@NoteID", NotelNo);

                cmd.Parameters.Add("@ReturnValue", SqlDbType.VarChar, 50);
                cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;

                cmd.CommandTimeout = 0;
                SqlDataAdapter Adap = new SqlDataAdapter();
                Adap.SelectCommand = cmd;
                Adap.Fill(dsInst);

                strIsComplete = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());

                cmd.Dispose();
                con.Dispose();
            }
            catch (Exception ex)
            {
            }
        }
        public DataTable GetInvoiceDetails(string Action, string CustVenID, string FinYear, string BranchList)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("proc_DebitCreditNoteDetails");
            proc.AddVarcharPara("@Action", 500, Action);
            proc.AddVarcharPara("@CustVenID", 500, CustVenID);
            proc.AddVarcharPara("@FinYear", 500, FinYear);
            proc.AddVarcharPara("@BranchList", 500, BranchList);
            dt = proc.GetTable();
            return dt;
        }

    }
}

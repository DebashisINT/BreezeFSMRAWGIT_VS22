using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesmanTrack
{
   public class InvoiceDetailsUpdateBL
    {
        public DataTable GenerateLocationReportData(string Employee, string FROMDATE, string TODATE, long login_id, string state, string desig, string REPORT_BY)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("FTS_API_OREDER_STATUS_REPORT");
            proc.AddPara("@Employee", Employee);
            proc.AddPara("@FROM_DATE", FROMDATE);
            proc.AddPara("@TO_DATE", TODATE);
            proc.AddPara("@LOGIN_ID", login_id);
            proc.AddPara("@stateID", state);
            proc.AddPara("@DESIGNID", desig);
            proc.AddPara("@REPORT_BY", REPORT_BY);
            ds = proc.GetTable();

            return ds;
        }

        public DataTable GetallorderDetails(int OrderID)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Proc_FTS_OrderSummaryDetailsBind");
            proc.AddPara("@OrderID", OrderID);
            ds = proc.GetTable();
            return ds;
        }

        public int UpdateDeleteInvoice(string invoice_no, string OrderCode, DateTime invoice_date, string invoice_amount, string ACTION, string user_id, string remarks, string NewInvoice)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_UPDATE_tbl_FTS_BillingDetails");

            int i = 0;
            proc.AddPara("@invoice_no", invoice_no);
            proc.AddPara("@OrderCode", OrderCode);
            proc.AddPara("@user_id", user_id);
            proc.AddPara("@invoice_date", invoice_date);
            proc.AddPara("@invoice_amount", invoice_amount);
            proc.AddPara("@ACTION", ACTION);
            proc.AddPara("@remarks", remarks);
            proc.AddPara("@NewInvoice", NewInvoice);
            i = proc.RunActionQuery();
            return i;
        }

        public DataTable GetInvoiceDetails(string invoice_no, string OrderCode)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_VIEW_BILLINGDETAILS");
            proc.AddPara("@invoice_no", invoice_no);
            proc.AddPara("@OrderCode", OrderCode);
            ds = proc.GetTable();
            return ds;
        }

        public DataTable GetProducts()
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Proc_FTS_ProductList_order");

            ds = proc.GetTable();
            return ds;
        }

        public DataTable GetallInvoiceDetails(String OrderID,String InvoiceID)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Proc_FTS_InvoiceDetailsBind");
            proc.AddPara("@OrderID", OrderID);
            proc.AddPara("@InvoiceID", InvoiceID);
            ds = proc.GetTable();
            return ds;
        }


        public int InvoiceProductModifyDelete(String ProdInvoiceId, String OrderId, String prodqty, String prodrate, String Action, String Invoice_Id)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Proc_FTS_InvoiceProductManage");
            proc.AddPara("@OrderID", OrderId);
            proc.AddPara("@InvoiceProd_Id", ProdInvoiceId);
            proc.AddPara("@Prod_Qty", prodqty);
            proc.AddPara("@Prod_Rate", prodrate);
            proc.AddPara("@Invoice_Id", Invoice_Id);
            proc.AddPara("@Action", Action);
            int i = proc.RunActionQuery();
            return i;
        }

        public DataTable InvoiceProductFetch(string ProdIdorder, string OrderId, string Action)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Proc_FTS_InvoiceProductManage");
            proc.AddPara("@OrderID", OrderId);
            proc.AddPara("@InvoiceProd_Id", ProdIdorder);
            proc.AddPara("@Action", Action);
            ds = proc.GetTable();
            return ds;
        }

    }
}

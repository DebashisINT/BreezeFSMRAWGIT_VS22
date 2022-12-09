using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BusinessLogicLayer.SalesTrackerReports
{

    public class InvoiceHierarchywiseBL
    {
        public DataTable GetOrderList()
        {
            DBEngine objDb = new DBEngine();
            DataTable dt = new DataTable();
            dt = objDb.GetDataTable("select CONVERT(VARCHAR(20),OrderId) order_id,CONVERT(VARCHAR(20),OrderCode) order_number from tbl_trans_fts_Orderupdate INNER JOIN fts_orderstage ON ORDER_ID=OrderCode AND stage_id=1 WHERE OrderCode not in (select OrderCode from tbl_FTS_BillingDetails)");
            return dt;
        }

        public DataTable GetallInvoiceDetails(string OrderID, string InvoiceID)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Proc_FTS_InvoiceDetails");
            proc.AddPara("@OrderID", OrderID);
            proc.AddPara("@InvoiceID", InvoiceID);
            ds = proc.GetTable();
            return ds;
        }

        public void SaveReceipt(string invoicenumber, string receiptamount)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Proc_FTS_ReceiptEntry");
            proc.AddPara("@invoicenumber", invoicenumber);
            proc.AddPara("@receiptamount", receiptamount);
            proc.AddPara("@user_id", Convert.ToString(HttpContext.Current.Session["userid"]));
            ds = proc.GetTable();
        }
    }
}

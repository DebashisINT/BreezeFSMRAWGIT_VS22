using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesmanTrack
{



    public class OrderList
    {
        public DataTable GetallorderList(string stateid, string shopid, string fromdate, string todate, int userid)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Proc_FTS_OrderListADMIN");

            proc.AddPara("@start_date", fromdate);
            proc.AddPara("@end_date", todate);
            proc.AddPara("@stateID", stateid);

            proc.AddPara("@shop_id", shopid);
            proc.AddPara("@user_id", userid);

            ds = proc.GetTable();

            return ds;
        }


        #region Summary Details
        public DataTable GetallorderListSummary(string stateid, string shopid, string fromdate, string todate, string EmployeeId, String Userid = "0")
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Proc_FTS_OrderSummaryBind");

            proc.AddPara("@start_date", fromdate);
            proc.AddPara("@end_date", todate);
            proc.AddPara("@stateID", stateid);
            proc.AddPara("@shop_id", shopid);
            proc.AddPara("@Employee_id", EmployeeId);

            proc.AddPara("@LOGIN_ID", Userid);
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

        public DataTable GetProducts()
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Proc_FTS_ProductList_order");

            ds = proc.GetTable();
            return ds;
        }
        public int OrderDelete(string OrderId)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Proc_FTS_OrderManage");
            proc.AddPara("@Order_Id", OrderId);
            int i = proc.RunActionQuery();
            return i;
        }
        public int OrderProductModifyDelete(long ProdorderId, long OrderId, decimal prodqty, decimal prodrate, string Action)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Proc_FTS_OrderProductManage");
            proc.AddPara("@OrderID", OrderId);
            proc.AddPara("@OrderProd_Id", ProdorderId);
            proc.AddPara("@Prod_Qty", prodqty);
            proc.AddPara("@Prod_Rate", prodrate);
            proc.AddPara("@Action", Action);
            int i = proc.RunActionQuery();
            return i;
        }

        public DataTable OrderProductFetch(string ProdIdorder, string OrderId, string Action)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Proc_FTS_OrderProductManage");
            proc.AddPara("@OrderID", OrderId);
            proc.AddPara("@OrderProd_Id", ProdIdorder);
            proc.AddPara("@Action", Action);
            ds = proc.GetTable();
            return ds;
        }
        #endregion

    }






}

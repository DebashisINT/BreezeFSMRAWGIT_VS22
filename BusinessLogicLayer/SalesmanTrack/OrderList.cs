﻿//====================================================== Revision History ==========================================================
//1.0  03-02-2023    2.0.38    Priti     0025604: Enhancement Required in the Order Summary Report
//====================================================== Revision History ==========================================================
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
        public int OrderProductModifyDelete(long ProdorderId, long OrderId, decimal prodqty, decimal prodrate, decimal productMrp, decimal productDiscount, string Action)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Proc_FTS_OrderProductManage");
            proc.AddPara("@OrderID", OrderId);
            proc.AddPara("@OrderProd_Id", ProdorderId);
            proc.AddPara("@Prod_Qty", prodqty);
            proc.AddPara("@Prod_Rate", prodrate);

            //REV 1.0
            proc.AddPara("@Prod_Mrp", productMrp);
            proc.AddPara("@Prod_Discount", productDiscount);
            //REV 1.0 END
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

        public DataTable getMrpDiscount(long Product_Id, string Action)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Proc_FTS_OrderProductManage");
            proc.AddPara("@OrderID", Product_Id);            
            proc.AddPara("@Action", Action);
            ds = proc.GetTable();
            return ds;
        }
        #endregion

    }






}

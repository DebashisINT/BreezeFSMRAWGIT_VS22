//====================================================== Revision History ==========================================================
//1.0  03-02-2023   V2.0.38    Priti     0025604: Enhancement Required in the Order Summary Report
//2.0  19-07-2023   V2.0.42    Priti     0026135: Branch Parameter is required for various FSM reports
//3.0  29/05/2024   V2.0.47    Sanchita  0027405: Colum Chooser Option needs to add for the following Modules
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
        //Rev 2.0
        //public DataTable GetallorderListSummary(string stateid, string shopid, string fromdate, string todate, string EmployeeId, String Userid = "0")
        public DataTable GetallorderListSummary(string stateid, string shopid, string fromdate, string todate, string EmployeeId, string Branch_Id, String Userid = "0")
        //Rev 2.0 End
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Proc_FTS_OrderSummaryBind");

            proc.AddPara("@start_date", fromdate);
            proc.AddPara("@end_date", todate);
            proc.AddPara("@stateID", stateid);
            proc.AddPara("@shop_id", shopid);
            proc.AddPara("@Employee_id", EmployeeId);
            proc.AddPara("@LOGIN_ID", Userid);
            //Rev 2.0
            proc.AddPara("@BRANCHID", Branch_Id);
            //Rev 2.0 End
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

        // Rev 3.0
        public int InsertPageRetention(string Col, String USER_ID, String ReportName)
        {
            ProcedureExecute proc = new ProcedureExecute("PRC_FTS_PageRetention");
            proc.AddPara("@Col", Col);
            proc.AddPara("@ReportName", ReportName);
            proc.AddPara("@USER_ID", USER_ID);
            proc.AddPara("@ACTION", "INSERT");
            int i = proc.RunActionQuery();
            return i;
        }

        public DataTable GetPageRetention(String USER_ID, String ReportName)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTS_PageRetention");
            proc.AddPara("@ReportName", ReportName);
            proc.AddPara("@USER_ID", USER_ID);
            proc.AddPara("@ACTION", "DETAILS");
            dt = proc.GetTable();
            return dt;
        }
        // End of Rev 3.0

        #endregion

    }






}

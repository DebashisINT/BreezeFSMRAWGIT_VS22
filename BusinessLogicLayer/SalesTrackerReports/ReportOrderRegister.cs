//====================================================== Revision History ==========================================================
//1.0  19-07-2023   V2 .0.42   Priti     0026135: Branch Parameter is required for various FSM reports
//====================================================== Revision History ==========================================================
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesTrackerReports
{
    public class ReportOrderRegister
    {
        public DataTable GetReportOrderRegister(string fromdate, string todate, string userid, string stateID, string ShopID, string userlist)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSORDERREGISTER_REPORT");

            proc.AddPara("@FROMDATE", fromdate);
            proc.AddPara("@TODATE", todate);
            proc.AddPara("@STATEID", stateID);
            proc.AddPara("@SHOPID", ShopID);

            proc.AddPara("@USERID", userid);
            proc.AddPara("@EMPID", userlist);
            ds = proc.GetTable();

            return ds;
        }
        //Rev 2.0
        public DataTable GetReportOrderRegisterNew(string fromdate, string todate, string userid, string stateID, string ShopID, string userlist, string Branch_Id)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSORDERREGISTER_REPORT");
            proc.AddPara("@FROMDATE", fromdate);
            proc.AddPara("@TODATE", todate);
            proc.AddPara("@STATEID", stateID);
            proc.AddPara("@SHOPID", ShopID);
            proc.AddPara("@USERID", userid);
            proc.AddPara("@EMPID", userlist);
            //Rev 2.0
            proc.AddPara("@BRANCHID", Branch_Id);
            //Rev 2.0 End
            ds = proc.GetTable();

            return ds;
        }
        //Rev 2.0 End
        public DataTable GetReportOrderRegister(string fromdate, string todate, string userid, string stateID, string ShopID, string userlist, string ishierarchy)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSORDERREGISTER_HIERARCHY_REPORT");

            proc.AddPara("@FROMDATE", fromdate);
            proc.AddPara("@TODATE", todate);
            proc.AddPara("@STATEID", stateID);
            proc.AddPara("@SHOPID", ShopID);

            proc.AddPara("@USERID", userid);
            proc.AddPara("@EMPID", userlist);
            ds = proc.GetTable();

            return ds;
        }




    }
}

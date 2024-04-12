//====================================================== Revision History ==========================================================
//1.0  19-07-2023   V2 .0.42   Priti     0026135: Branch Parameter is required for various FSM reports
//2.0  04-04-2024   V2 .0.46   Sanchita  0027345: Two checkbox required in parameter for Order register report.
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
        // Rev 2.0 [ paramters IsShowMRP, IsShowDiscount added ]
        public DataTable GetReportOrderRegisterNew(string fromdate, string todate, string userid, string stateID, 
            string ShopID, string userlist, string Branch_Id, int IsShowMRP, int IsShowDiscount)
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
            // Rev 2.0
            proc.AddPara("@SHOWMRP", IsShowMRP);
            proc.AddPara("@SHOWDISCOUNT", IsShowDiscount);
            // End of Rev 2.0
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

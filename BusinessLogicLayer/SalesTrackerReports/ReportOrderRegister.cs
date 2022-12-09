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

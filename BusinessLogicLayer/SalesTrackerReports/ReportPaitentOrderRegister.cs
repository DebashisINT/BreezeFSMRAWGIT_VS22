using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesTrackerReports
{
    public class ReportPaitentOrderRegister
    {
        public DataTable GetReportPaitentOrderRegister(string fromdate, string todate, string userid, string stateID, string ShopID, string userlist)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSPAITENTORDERREGISTER_REPORT");

            proc.AddPara("@FROMDATE", fromdate);
            proc.AddPara("@TODATE", todate);
            proc.AddPara("@STATEID", stateID);
            proc.AddPara("@SHOPID", ShopID);
            proc.AddPara("@EMPID", userlist);
            proc.AddPara("@USERID", userid);            
            ds = proc.GetTable();

            return ds;
        }
    }
}

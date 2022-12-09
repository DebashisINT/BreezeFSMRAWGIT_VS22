using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesTrackerReports
{
   public class AttendancewisePerformanceReport_List
    {
        public DataTable GetAttendancewisePerformanceListReport(string month, string userid, string stateID, string desigid, string empid,string Action,string year)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSATTENDANCEWISEPERFORMANCE_REPORT");   

            proc.AddPara("@MONTH", month);
            proc.AddPara("@STATEID", stateID);
            proc.AddPara("@DESIGNID", desigid);            
            proc.AddPara("@EMPID", empid);
            proc.AddPara("@RPTTYPE", Action);
            proc.AddPara("@USERID", userid);
            proc.AddPara("@YEARS", year);
            ds = proc.GetTable();
            return ds;
        }

        public DataTable GetYearList()
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_MASTERYEARLIST");
            ds = proc.GetTable();
            return ds;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataAccessLayer;

namespace BusinessLogicLayer.SalesmanTrack
{
    public class EmployeeActivityDetailsBL
    {
        public DataTable GenerateEmployeeActivityDetailsData(string Employee, string FROMDATE, string TODATE, string stateID, string DESIGNID, long login_id)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSEMPLOYEEACTIVITYDETAILS_REPORT");
            proc.AddPara("@Employee", Employee);
            proc.AddPara("@FROMDATE", FROMDATE);
            proc.AddPara("@TODATE", TODATE);
            proc.AddPara("@stateID", stateID);
            proc.AddPara("@DESIGNID", DESIGNID);
            proc.AddPara("@USERID", login_id);
            ds = proc.GetTable();

            return ds;
        }

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
    }
}

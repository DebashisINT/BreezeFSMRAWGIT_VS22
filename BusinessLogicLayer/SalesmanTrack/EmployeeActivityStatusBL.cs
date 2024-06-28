/************************************************************************************************************************
 * 1.0  29/05/2024   V2.0.47    Sanchita  0027405: Colum Chooser Option needs to add for the following Modules
 * 
 * ************************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataAccessLayer;

namespace BusinessLogicLayer.SalesmanTrack
{
    public class EmployeeActivityStatusBL
    {
        public DataTable GenerateEmployeeActivitySummaryData(string Employee, string FROMDATE, string TODATE, long login_id, string stateID, string DESIGNID)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSEMPLOYEEACTIVITYSTATUS_REPORT");
            proc.AddPara("@EMPCODE", Employee);
            proc.AddPara("@FROMDATE", FROMDATE);
            proc.AddPara("@TODATE", TODATE);
            proc.AddPara("@STATEID", stateID);
            proc.AddPara("@DESIGNID", DESIGNID);
            proc.AddPara("@USERID", "");
            proc.AddPara("@VISITDATE", "");
            proc.AddPara("@REPORTTYPE", "Summary");
            proc.AddPara("@LOGINID", login_id);            
            ds = proc.GetTable();

            return ds;
        }

        public DataTable GenerateEmployeeActivityDetailsData(string userid, string FROMDATE, string TODATE, long login_id, string date)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSEMPLOYEEACTIVITYSTATUS_REPORT");
            proc.AddPara("@EMPCODE", "");
            proc.AddPara("@FROMDATE", FROMDATE);
            proc.AddPara("@TODATE", TODATE);
            proc.AddPara("@STATEID", "");
            proc.AddPara("@DESIGNID", "");
            proc.AddPara("@USERID", userid);
            proc.AddPara("@VISITDATE", date);
            proc.AddPara("@REPORTTYPE", "Details");
            proc.AddPara("@LOGINID", login_id);
            ds = proc.GetTable();

            return ds;
        }

        // Rev 1.0
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
        // End of Rev 1.0


    }
}

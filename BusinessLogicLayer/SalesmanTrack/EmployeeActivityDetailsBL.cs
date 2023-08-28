#region=====================================Revision History=========================================================================
//1.0   V2.0.42     Priti       19/07/2023      0026135: Branch Parameter is required for various FSM reports
#endregion===================================End of Revision History==================================================================
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
        //Rev 1.0
        //public DataTable GenerateEmployeeActivityDetailsData(string Employee, string FROMDATE, string TODATE, string stateID, string DESIGNID, long login_id)
        public DataTable GenerateEmployeeActivityDetailsData(string Employee, string FROMDATE, string TODATE, string stateID, string DESIGNID, long login_id, string Branch_Id)
        //REv 1.0 End
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSEMPLOYEEACTIVITYDETAILS_REPORT");
            proc.AddPara("@Employee", Employee);
            proc.AddPara("@FROMDATE", FROMDATE);
            proc.AddPara("@TODATE", TODATE);
            proc.AddPara("@stateID", stateID);
            proc.AddPara("@DESIGNID", DESIGNID);
            //Rev 1.0
            proc.AddPara("@BRANCHID", Branch_Id);
            //Rev 1.0 End
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

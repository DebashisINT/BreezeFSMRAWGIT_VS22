using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataAccessLayer;

namespace BusinessLogicLayer.SalesmanTrack
{
   public class EmployeeActivityBL
    {
        //Rev Debashis Mantis: 0025104 && Added two fields as IsOnlyLoginData & IsOnlyLogoutData
       public DataTable GenerateAnalysisSummaryData(string Employee, string FROMDATE, string TODATE, long login_id, string stateID, string DESIGNID, string IsOnlyLoginData, string IsOnlyLogoutData)
       {
           DataTable ds = new DataTable();
           ProcedureExecute proc = new ProcedureExecute("PRC_API_EMPLOYEEACTIVITY_REPORT");
           proc.AddPara("@Employee", Employee);
           proc.AddPara("@FROMDATE", FROMDATE);
           proc.AddPara("@TODATE", TODATE);
           proc.AddPara("@LOGIN_ID", login_id);
           proc.AddPara("@stateID", stateID);
           proc.AddPara("@DESIGNID", DESIGNID);
           proc.AddPara("@ISONLYLOGINDATA", IsOnlyLoginData);
           proc.AddPara("@ISONLYLOGOUTDATA", IsOnlyLogoutData);
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

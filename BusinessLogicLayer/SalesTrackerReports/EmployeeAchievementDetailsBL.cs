using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesTrackerReports
{
    public class EmployeeAchievementDetailsBL
    {
        public DataTable GetReportEmployeeAchivement(string fromdate, string todate, string userid, String deptid, String empcode, String Desgid, String Supercode)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_EmployeeAchivementReport");

            proc.AddPara("@FromDate", fromdate);
            proc.AddPara("@TODATE", todate);
            proc.AddPara("@user_id", userid);
            proc.AddPara("@EMPID", empcode);
            proc.AddPara("@deptid", deptid);
            proc.AddPara("@DesigId", Desgid);
            proc.AddPara("@Supercode", Supercode);
            ds = proc.GetTable();
            return ds;
        }
    }
}

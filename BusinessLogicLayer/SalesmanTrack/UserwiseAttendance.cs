using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesmanTrack
{
    public class UserwiseAttendance
    {

        public DataTable GetAttendanceuserlist(string userid, string fromdate, string todate,string usertype)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("SP_API_Attendance_Userwise");

            proc.AddPara("@user_id", userid);
            proc.AddPara("@start_date", fromdate);
            proc.AddPara("@end_date", todate);
            proc.AddPara("@usertype", usertype);

            ds = proc.GetTable();
            return ds;

        }

        public DataTable GetAttendanceuserlist(string desgid, string fromdate,string usertype,int userid)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("SP_API_Attendance_Userwise");

            proc.AddPara("@start_date", fromdate);
            proc.AddPara("@DesgId", desgid);
            proc.AddPara("@Action", "Designationwise");
            proc.AddPara("@usertype", usertype);
            proc.AddPara("@user_id", userid);
 
            ds = proc.GetTable();

            return ds;
        }

        public DataTable GetAttendanceList(string desgid, string fromdate,  string todate,string usertype, int userid)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Proc_FTS_AttendanceList_User");

            proc.AddPara("@start_date", fromdate);
            proc.AddPara("@DesgId", desgid);
            proc.AddPara("@Action", "Designationwise");
            proc.AddPara("@usertype", usertype);
            proc.AddPara("@user_id", userid);

            ds = proc.GetTable();

            return ds;
        }


    }
}

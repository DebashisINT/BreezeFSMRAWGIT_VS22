using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{

    public class AttendancerecordInput
    {


        public string session_token { get; set; }
        public string user_id { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }


    }


    public class AttendancerecordOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        //public DatalistsAttendance data { get; set; }

        public List<Attendancerecord> shop_list { get; set; }

    }

    public class DatalistsAttendance
    {

        public string session_token { get; set; }

   

    }

    public class Attendancerecord
    {
        public DateTime? login_date { get; set; }
        public DateTime? login_time { get; set; }

        public DateTime? logout_time { get; set; }

        public string duration { get; set; }
        public string Isonleave { get; set; }

        //public string login_date { get; set; }
        //public DateTime? login_time { get; set; }

        //public DateTime? logout_time { get; set; }
        //public string logout_date { get; set; }

        //public string duration { get; set; }


    }
    //Rev Debashis
    public class LeaveAttendanceDeleteInput
    {
        public string user_id { get; set; }
        public string leave_apply_date { get; set; }
        public string isOnLeave { get; set; }
        //Rev Debashis Row 730
        public string IsLeaveDelete { get; set; }
        //End of Rev Debashis Row 730
    }

    public class LeaveAttendanceDeleteOutput
    {
        public string user_id { get; set; }
        public string status { get; set; }
        public string message { get; set; }
    }
    //End of Rev Debashis  
}
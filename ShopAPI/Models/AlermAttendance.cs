using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class AlermAttendance
    {
        public string member_name { get; set; }
        public string member_id { get; set; }
        public string login_time { get; set; }
        public string contact_no { get; set; }
        public string status { get; set; }
        public string report_to { get; set; }
     

    }
    public class AlermAttendanceInput
    {
        public string user_id { get; set; }
        public string session_token { get; set; }
        public String date { get; set; }
    }

    public class AlermAttendanceOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<AlermAttendance> attendance_report_list { get; set; }
    }
}
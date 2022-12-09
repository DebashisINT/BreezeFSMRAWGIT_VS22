using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class DemoAttendanceModel
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
    }

    public class AttendanceInputData
    {
        public string data { get; set; }
        public HttpPostedFileBase image { get; set; }
    }

    public class DemoLoginInput
    {
        public string user_name { get; set; }
        public string password { get; set; }
    }

    public class DemoLoginOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }
}
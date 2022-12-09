using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{

    public class LeavetypeInput
    {
        public string user_id { get; set; }
    }


    public class Leavetypeoutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<Leavetype> leave_type_list { get; set; }
    }
    public class Leavetype
    {
        public int id { get; set; }
        public string type_name { get; set; }

    }

    public class LeaveListInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }

    }
    public class LeaveListoutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<LeaveList> leave_list { get; set; }
    }

    public class LeaveList
    {
        public string id { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }
        public string leave_type { get; set; }
        public string desc { get; set; }
        public string status { get; set; }


    }
}
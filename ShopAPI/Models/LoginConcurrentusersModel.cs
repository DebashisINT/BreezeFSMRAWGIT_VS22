using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class LoginConcurrentusersInsertInput
    {
        public string user_id { get; set; }
        public string imei { get; set; }
        public string date_time { get; set; }
    }

    public class LoginConcurrentusersInsertOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class LoginConcurrentusersFetchInput
    {
        public string user_id { get; set; }
    }

    public class LoginConcurrentusersFetchOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string user_id { get; set; }
        public string imei { get; set; }
        public DateTime? date_time { get; set; }
    }

    public class LoginConcurrentusersDeleteInput
    {
        public string user_id { get; set; }
    }

    public class LoginConcurrentusersDeleteOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }
}
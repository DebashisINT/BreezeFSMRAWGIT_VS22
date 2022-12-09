using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class ReviewConfirmmodel
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string report_time { get; set; }
        public string view_time { get; set; }
        public string alarm_id { get; set; }
        public string report_id { get; set; }

    }

}
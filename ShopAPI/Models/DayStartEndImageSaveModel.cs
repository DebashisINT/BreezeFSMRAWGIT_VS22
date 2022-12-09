using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class DayStartEndImageSaveInput
    {
        public string data { get; set; }
        public HttpPostedFileBase image { get; set; }
    }

    public class DayStartEndImageSaveDetails
    {
        public string session_token { get; set; }
        public string user_id { get; set; }        
        public DateTime date_time { get; set; }
        public int day_start { get; set; }
        public int day_end { get; set; }
    }

    public class DayStartEndImageSaveOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }
}
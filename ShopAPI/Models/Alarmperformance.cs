using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class Alarmperformance
    {
        public string member_name { get; set; }
        public string member_id { get; set; }
        public string total_shop_count { get; set; }
        public string total_travel_distance { get; set; }
        public string shop_name { get; set; }
        public string report_to { get; set; }
        public string order_vale { get; set; }
        public string collection_value { get; set; }




    }
    public class AlarmperformanceInput
    {
        public string user_id { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }

        public string session_token { get; set; }


    }

    public class AlarmperformanceOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<Alarmperformance> performance_report_list { get; set; }

    }


}
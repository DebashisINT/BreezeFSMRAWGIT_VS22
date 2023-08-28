#region======================================Revision History=========================================================
//1.0   V2.0.41     Debashis    18/07/2023      Some new parameters have been added.Row: 857 & Refer: 0026547
#endregion===================================End of Revision History==================================================
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
        //Rev 1.0 Row: 857 & Mantis: 0026547
        public string user_id { get; set; }
        public int attendance_present_count { get; set; }
        public int attendance_absent_count { get; set; }
        public int visit_inactivity_party_count { get; set; }
        public int order_inactivity_party_count { get; set; }
        public string last_visited_date { get; set; }
        public string last_order_date { get; set; }
        //End of Rev 1.0 Row: 857 & Mantis: 0026547
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
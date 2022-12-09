using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class AlarmShopvisit
    {
        //Rev Debashis Row 728
        public string user_id { get; set; }
        //End of Rev Debashis Row 728
        public string member_name { get; set; }
        public string member_id { get; set; }
        public string total_shop_count { get; set; }
        public string report_to { get; set; }
        public string status { get; set; }
        public string total_distance_travelled { get; set; }

        public List<Shopvisitalarm> visit_details_list { get; set; }
    }
    public class Shopvisitalarm
    {
        public string shop_name { get; set; }
        public string visit_time { get; set; }
        public string duration_spent { get; set; }
        public string distance { get; set; }
        //Rev Debashis
        public string date { get; set; }
        //End of Rev Debashis
        //Rev Debashis Row 728
        public string beat_id { get; set; }
        public string beat_name { get; set; }
        public string visit_status { get; set; }
        //End of Rev Debashis Row 728
    }

    public class AlermShopvisitInput
    {
        public string user_id { get; set; }
        public string session_token { get; set; }
    }

    public class AlermShopvisitOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<AlarmShopvisit> visit_report_list { get; set; }
    }

}
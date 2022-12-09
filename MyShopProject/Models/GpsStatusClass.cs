using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{

     public class GpsStatusClassInput
    {
        public string selectedusrid { get; set; }

        public List<GetUserName> userlsit { get; set; }

        

        public string Fromdate { get; set; }

        public string Todate { get; set; }



    }





     public class GpsStatusClasstOutput
    {
        public string UserName { get; set; }
        public string user_id { get; set; }
        public string total_time_spent_at_shop { get; set; }
        public string total_shop_visited { get; set; }
        public string gps { get; set; }
        public string Ratio { get; set; }

        public string name { get; set; }

    

        public string active_hrs { get; set; }
        public string inactive_hrs { get; set; }
        public string idle_percentage { get; set; }

    }

     public class GpsStatusActivityshopClasstOutput
     {
         public string UserName { get; set; }
         public string shop_name { get; set; }
         public string visited_date { get; set; }
         public DateTime? visited_time { get; set; }
         public string time_visit { get; set; }
         public string duration_spent { get; set; }


     }



     public class GpsStatuInactiveClasstOutput
     {
         public DateTime? date { get; set; }
         public string gps_on_time { get; set; }
         public string gps_off_time { get; set; }
         public DateTime? visited_time { get; set; }
         public string duration { get; set; }

     }
}
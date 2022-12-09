using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class LocationfetchInput
    {
        public int? user_id { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }
        public int? date_span { get; set; }
    }

    public class LocationfetchOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<Locationfetch> location_details { get; set; }
        //Extra parameter Distance Tanmoy 09-12-2020
        public string visit_distance { get; set; }
        //Extra parameter Distance Tanmoy 09-12-2020 End
    }

    public class Locationfetch
    {
        public string location_name { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public decimal distance_covered { get; set; }
        public string last_update_time { get; set; }
        public DateTime? date { get; set; }
        public int shops_covered { get; set; }
        public int meeting_attended { get; set; }
        public string network_status { get; set; }
        public string battery_percentage { get; set; }
    }

    public class NewLocationfetchInput
    {
        public int? user_id { get; set; }
        public string date { get; set; }
        public string session_token { get; set; }
    }

    public class NewLocationfetchOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string visit_distance { get; set; }
        public List<NewLocationfetch> location_details { get; set; }        
    }

    public class NewLocationfetch
    {
        public string location_name { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public decimal distance_covered { get; set; }
        public string last_update_time { get; set; }
        public DateTime? date { get; set; }
        public int shops_covered { get; set; }
        public int meeting_attended { get; set; }
    }
}
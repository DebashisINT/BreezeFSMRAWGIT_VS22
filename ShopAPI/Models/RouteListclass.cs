using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{

    public class RouteListclass
    {
        public string shop_id { get; set; }
        public string shop_address { get; set; }
        public string shop_name { get; set; }
        public string shop_contact_no { get; set; }
    }

    public class RoutefetchInput
    {

        public string session_token { get; set; }
        public string user_id { get; set; }



    }
    public class RouteDetailsOutput
    {
        public string status { get; set; }
        public string message { get; set; }

        public List<RouteDetailsOutputfetch> route_list { get; set; }

    }

    public class RouteDetailsOutputfetch
    {
        public string id { get; set; }

        public string route_name { get; set; }
        public List<RouteListclass> shop_details_list { get; set; }
    }

}
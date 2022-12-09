using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class Gpslocation
    {

    }
    public class GpslocationInput
    {
        public string session_token { get; set; }
        public string gps_id { get; set; }
        public string user_id { get; set; }

        public string date { get; set; }
        public string gps_off_time { get; set; }
        public string gps_on_time { get; set; }
        public string duration { get; set; }
    }
    public class GpslocationOutput
    {
        public string status { get; set; }
        public string message { get; set; }
     

    }
}
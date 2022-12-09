using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class AddressInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string pin_code { get; set; }

    }

    public class AddressOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string city_id { get; set; }
        public string city { get; set; }
        public string state_id { get; set; }
        public string state { get; set; }
        public string country { get; set; }



    }



}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class OTPClass
    {

        public string user_id { get; set; }
        public string shop_id { get; set; }
        public string session_token { get; set; }
       public string OTP { get; set; }
    }

    public class OTPClassOutput
    {

        public string status { get; set; }
        public string message { get; set; }
        public string OTP { get; set; }
        public string Shop_Owner_Contact { get; set; }
        public string Shop_Owner { get; set; }
    }
}
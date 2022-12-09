using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class EarlyExitInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string reason { get; set; }

    }

    public class EarlyExitOutput
    {
        public string status { get; set; }
        public string message { get; set; }

    }
  

}
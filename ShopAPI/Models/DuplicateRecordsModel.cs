using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class DuplicatePhNoFetchInput
    {
        public string user_id { get; set; }
        public string session_token { get; set; }
        public string new_shop_phone { get; set; }
    }

    public class DuplicatePhNoFetchOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class BankDetailsInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string shop_id { get; set; }
        public string account_holder { get; set; }
        public string account_no { get; set; }
        public string bank_name { get; set; }
        public string ifsc { get; set; }
        public string upi { get; set; }

    }

    public class BankDetailsOutput
    {
        public string status { get; set; }
        public string message { get; set; }

    }
}
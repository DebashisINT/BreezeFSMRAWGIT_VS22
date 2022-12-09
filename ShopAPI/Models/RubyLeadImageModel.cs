using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class RubyLeadImage1Input
    {
        public string data { get; set; }
        public HttpPostedFileBase rubylead_image1 { get; set; }
    }

    public class RubyLeadImage1Output
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class RubyLeadImage1InputDetails
    {
        public string session_token { get; set; }
        public string lead_shop_id { get; set; }
        public string user_id { get; set; }
    }

    public class RubyLeadImage2Input
    {
        public string data { get; set; }
        public HttpPostedFileBase rubylead_image2 { get; set; }
    }

    public class RubyLeadImage2Output
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class RubyLeadImage2InputDetails
    {
        public string session_token { get; set; }
        public string lead_shop_id { get; set; }
        public string user_id { get; set; }
    }
}
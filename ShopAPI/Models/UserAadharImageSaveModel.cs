using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class UserAadharImageSaveInput
    {
        public string data { get; set; }
        public HttpPostedFileBase attachments { get; set; }
    }

    public class UserAadharImageSaveDetails
    {
        public string session_token { get; set; }
        public string aadhaar_holder_user_id { get; set; }
        public string aadhaar_holder_user_contactid { get; set; }
        public string aadhaar_no { get; set; }
        public string date { get; set; }
        public string feedback { get; set; }
        public string address { get; set; }
    }

    public class UserAadharImageSaveOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }
}
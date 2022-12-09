using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class UserWiseAadharInfoInput
    {
        [Required]
        public string session_token { get; set; }
        public string aadhaar_holder_user_id { get; set; }
        public string aadhaar_holder_user_contactid { get; set; }
        public string aadhaar_no { get; set; }
        public string date { get; set; }
        public string feedback { get; set; }
        public string address { get; set; }
    }

    public class UserWiseAadharInfoOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class UserAadharListInput
    {
        [Required]
        public string session_token { get; set; }
    }

    public class UserAadharListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<AlluserAadharList> all_aadhaar_list { get; set; }
    }

    public class AlluserAadharList
    {
        public long user_id { get; set; }
        public string user_login_id { get; set; }
        public string RegisteredAadhaarNo { get; set; }
    }
}
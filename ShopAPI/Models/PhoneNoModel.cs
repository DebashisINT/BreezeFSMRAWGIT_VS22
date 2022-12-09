using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class PhoneNoInsertInput
    {
        [Required]
        public string session_token { get; set; }
        [Required]
        public string user_id { get; set; }
        [Required]
        public string user_contactid { get; set; }
        [Required]
        public string phone_no { get; set; }
    }

    public class PhoneNoInsertOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class PhoneNoUpdateInput
    {
        [Required]
        public string session_token { get; set; }
        [Required]
        public string user_id { get; set; }
        [Required]
        public string user_contactid { get; set; }
        [Required]
        public string old_phone_no { get; set; }
        [Required]
        public string new_phone_no { get; set; }
    }

    public class PhoneNoUpdateOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }
}
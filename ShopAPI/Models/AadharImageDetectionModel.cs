using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace ShopAPI.Models
{
    public class AadharImageDetectionInput
    {
        public string data { get; set; }
        public HttpPostedFileBase attachments { get; set; }
    }

    public class AadharImageDetectionInputDetails
    {
        public string user_id { get; set; }
        public string session_token { get; set; }
        public DateTime registration_date_time { get; set; }
    }

    public class AadharImageDetectionOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string user_id { get; set; }
        public string aadhaar_image_link { get; set; }
    }

    public class AadharImageDetectionInfoInput
    {
        [Required]
        public string user_id { get; set; }
        public string session_token { get; set; }
        public string name_on_aadhaar { get; set; }        
        public string DOB_on_aadhaar { get; set; }
        public string Aadhaar_number { get; set; }
        public string REG_DOC_TYP { get; set; }
    }

    public class AadharImageDetectionInfoOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string user_id { get; set; }
    }
}
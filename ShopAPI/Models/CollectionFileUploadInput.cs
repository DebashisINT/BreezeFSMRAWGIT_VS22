using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class CollectionFileUploadInput
    {
        public String data { get; set; }
        [Required]
        public HttpPostedFileBase doc { get; set; }
    }

    public class CollectionFileUploadInputData
    {
        public String bill_id { get; set; }
        [Required]
        public String collection { get; set; }
        [Required]
        public String collection_date { get; set; }
        public String collection_id { get; set; }
        public String order_id { get; set; }
        [Required]
        public String session_token { get; set; }
        [Required]
        public String shop_id { get; set; }
        [Required]
        public String user_id { get; set; }
        [Required]
        public String payment_id { get; set; }
        public String instrument_no { get; set; }
        public String bank { get; set; }
        public String remarks { get; set; }
        //Extra Input for 4Basecare
        public string patient_no { get; set; }
        public string patient_name { get; set; }
        public string patient_address { get; set; }
        //Extra Input for 4Basecare
        //Extra Input for EuroBond
        public string Hospital { get; set; }
        public string Email_Address { get; set; }
        //Extra Input for EuroBond
    }

    public class CollectionFileUploadOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }
}
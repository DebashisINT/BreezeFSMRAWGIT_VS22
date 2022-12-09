using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace ShopAPI.Models
{
    public class BreakageMaterialsDetectionSaveInput
    {
        public string data { get; set; }
        public HttpPostedFileBase attachments { get; set; }
    }

    public class BreakageMaterialsDetectionSaveInputDetails
    {
        [Required]
        public string user_id { get; set; }
        public string session_token { get; set; }
        [Required]
        public DateTime date_time { get; set; }
        [Required]
        public string breakage_number { get; set; }
        [Required]
        public long product_id { get; set; }
        [Required]
        public string description_of_breakage { get; set; }
        public string customer_feedback { get; set; }
        public string remarks { get; set; }
        [Required]
        public string shop_id { get; set; }
    }

    public class BreakageMaterialsDetectionSaveOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string user_id { get; set; }
        public string image_link { get; set; }
    }

    public class BreakageMaterialsDeleteInput
    {
        [Required]
        public string user_id { get; set; }
        [Required]
        public string breakage_number { get; set; }
        public string session_token { get; set; }
    }

    public class BreakageMaterialsDeleteOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class ListForBreakageMaterialsInput
    {
        [Required]
        public string user_id { get; set; }
        [Required]
        public string from_date { get; set; }
        [Required]
        public string to_date { get; set; }
        [Required]
        public string shop_id { get; set; }
    }

    public class ListForBreakageMaterialsOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string user_id { get; set; }
        public string user_name { get; set; }
        public string shop_id { get; set; }
        public string shop_name { get; set; }
        public List<BreakagelistOutput> breakage_list { get; set; }
    }

    public class BreakagelistOutput
    {
        public DateTime date_time { get; set; }
        public long product_id { get; set; }
        public string product_name { get; set; }
        public string breakage_number { get; set; }
        public string description_of_breakage { get; set; }
        public string customer_feedback { get; set; }
        public string remarks { get; set; }
        public string image_link { get; set; }
    }
}
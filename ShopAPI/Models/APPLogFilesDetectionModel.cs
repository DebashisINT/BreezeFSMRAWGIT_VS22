using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class APPLogFilesDetectionInput
    {
        public string data { get; set; }
        public HttpPostedFileBase attachments { get; set; }
    }

    public class APPLogFilesDetectionInputDetails
    {
        public string user_id { get; set; }
    }

    public class APPLogFilesDetectionOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string user_id { get; set; }
        public string file_url { get; set; }
    }
}
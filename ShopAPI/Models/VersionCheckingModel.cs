using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class VersionCheckingModel
    {

        public string devicetype { get; set; }

    }

    public class VersionCheckingModeloutput
    {

        public string status { get; set; }
        public string message { get; set; }
        public string min_req_version { get; set; }
        public string play_store_version { get; set; }

        public string mandatory_msg { get; set; }
        public string optional_msg { get; set; }

        public string apk_url { get; set; }

    }

}
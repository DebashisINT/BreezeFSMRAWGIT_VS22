using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class ShopvisitImageUpload
    {
        public string data { get; set; }
        public HttpPostedFileBase shop_image { get; set; }
    }

    public class ShopvisitImageclass
    {
        public string session_token { get; set; }
        //public string shop_unique { get; set; }
        public string shop_id { get; set; }
        public string user_id { get; set; }
        public DateTime? visit_datetime { get; set; }

    }

    public class ShopvisitImageUploadOutput
    {
        public string status { get; set; }
        public string message { get; set; }

    }

    public class ImageDetailsfrXml
    {
        public string shop_id { get; set; }
        public string user_id { get; set; }
        public string ImageName { get; set; }
        public DateTime? visit_datetime { get; set; }
        public string date { get; set; }
    }
}
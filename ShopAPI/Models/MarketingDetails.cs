using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class MarketingDetails
    {

        public string marketing_detail { get; set; }

        public List<HttpPostedFileBase> material_image { get; set; }
    }

    public class MarketingDetailsoutput
    {

        public string status { get; set; }
        public string message { get; set; }
    }


    public class MarketingDetailImages
    {
        public string material_images { get; set; }
    
    }
}
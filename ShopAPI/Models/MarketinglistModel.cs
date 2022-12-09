using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class MarketinglistModel
    {
        public string status { get; set; }
        public string message { get; set; }

        public List<RetailBrndingclass> RetailBranding { get; set; }

        public List<Popmaterialsclass> POPMaterial { get; set; }
    }

    public  class RetailBrndingclass
    {
        public double material_id { get; set; }
        public string  material_name { get; set; }

    }

    public class Popmaterialsclass
    {
        public double material_id { get; set; }
        public string material_name { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class Marketingdetailslistinput
    {
        public string shop_id{ get; set; }
        public string user_id { get; set; }
    }

    public class Marketingdetailslistioutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<Materialdetailslist> material_details { get; set; }
        public List<Materialimageslist> marketing_img { get; set; }
    }

    public class Materialdetailslist
    {
        public int material_id { get; set; }
        public Nullable<DateTime> date { get; set; }
        public int typeid { get; set; }
    }
    public class Materialimageslist
    {
        public int image_id { get; set; }
        public string image_url { get; set; }
    }
}
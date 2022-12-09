using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class ShopUpdationInput
    {
        public string user_id { get; set; }
        public string shop_id { get; set; }
        public string shop_lat { get; set; }
        public string shop_long { get; set; }
        public string shop_address { get; set; }
        public string isAddressUpdated { get; set; }
        public string pincode { get; set; }
     
    }
}
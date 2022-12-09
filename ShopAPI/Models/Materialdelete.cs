using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class Materialdelete
    {
          public string shop_id { get; set; }
          public string image_id { get; set; }
          public string user_id { get; set; }
       
    }

    public class Materialdeleteoutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string materialimage { get; set; }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class FileMove
    {
        public string DataType { get; set; }
        public HttpPostedFileBase File { get; set; }
    }
}
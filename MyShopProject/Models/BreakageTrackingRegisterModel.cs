using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class BreakageTrackingRegisterModel
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Fromdate { get; set; }



        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Todate { get; set; }

        public List<GetAllProducts> product { get; set; }

        public List<string> prodcode { get; set; }

        public List<string> empcode { get; set; }
        public List<GetAllEmployee> employee { get; set; }
        public string is_pageload { get; set; }
    }
}
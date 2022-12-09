using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class PriorityMasterModel
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Fromdate { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Todate { get; set; }

        public string is_pageload { get; set; }

        public String Priority { get; set; }
    }
}
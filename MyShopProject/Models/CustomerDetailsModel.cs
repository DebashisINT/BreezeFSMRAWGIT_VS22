using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class CustomerDetailsModel
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Fromdate { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Todate { get; set; }

        public List<string> StateId { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public List<string> desgid { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public List<string> empcode { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public List<string> Department { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public List<string> Supervisor { get; set; }
       
        public string is_pageload { get; set; }
    }

   
}
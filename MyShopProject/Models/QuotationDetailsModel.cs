using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class QuotationDetailsModel
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Fromdate { get; set; }



        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Todate { get; set; }

        public List<GetAllshops> shop { get; set; }

        public List<string> shopcode { get; set; }

        public List<string> empcode { get; set; }
        public List<GetAllEmployee> employee { get; set; }
        public string is_pageload { get; set; }
    }
}

public class GetAllshops
{
    public string Shop_Code { get; set; }

    public string Shop_Name { get; set; }

}
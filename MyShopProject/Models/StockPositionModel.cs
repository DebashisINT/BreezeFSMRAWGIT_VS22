using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class StockPositionModel
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Fromdate { get; set; }



        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Todate { get; set; }

        public List<GetAllProducts> product { get; set; }

        public List<string> prodcode { get; set; }
        public string is_pageload { get; set; }
    }
}

public class GetAllProducts
{
    public string sProducts_ID { get; set; }

    public string sProducts_Code { get; set; }
    public string sProducts_Name { get; set; }
    public string sProducts_Description { get; set; }
}
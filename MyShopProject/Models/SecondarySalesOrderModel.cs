using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class SecondarySalesOrderModel
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

        public string Shop_code { get; set; }

        public string order_code { get; set; }
    }


    public class SecondarySalesOrder1ststage
    {
        public int SEQ { get; set; }
        public string FROM_DATE { get; set; }
        public string TO_DATE { get; set; }
        public decimal ORDERAMOUNT { get; set; }
    }

    public class SecondarySalesOrder2ndstage
    {
        public int SEQ { get; set; }
        public String FROM_DATE { get; set; }
        public String TO_DATE { get; set; }
        public String Shop_Name { get; set; }
        public decimal ORDERAMOUNT { get; set; }
        public String SHOP_CODE { get; set; }
    }

    public class SecondarySalesOrder3rdstage
    {
        public int SEQ { get; set; }
        public String DATE { get; set; }
        public String Shop_Name { get; set; }
        public String OrderCode { get; set; }
        public decimal ORDERAMOUNT { get; set; }
        public String SHOP_CODE { get; set; }
    }

    public class SecondarySalesOrder4thstage
    {
        public int SEQ { get; set; }
        public String Orderdate { get; set; }
        public String Shop_Name { get; set; }
        public String OrderCode { get; set; }
        public String sProducts_Name { get; set; }
        public decimal Product_Qty { get; set; }
        public decimal Product_Price { get; set; }
        public String SHOP_CODE { get; set; }
        public decimal Product_Rate { get; set; }
    }
}
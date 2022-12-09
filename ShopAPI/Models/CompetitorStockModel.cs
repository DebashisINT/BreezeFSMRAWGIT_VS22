using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class CompetitorStockModel
    {
    }

    public class AddCompetitorStockInput
    {
        public string user_id { get; set; }
        public string session_token { get; set; }
        public string competitor_stock_id { get; set; }
        public string shop_id { get; set; }
        public string visited_datetime { get; set; }
        public List<AddCompetitorStockProduct> competitor_stock_list { get; set; }
    }

    public class AddCompetitorStockProduct
    {
        public string brand_name { get; set; }
        public string product_name { get; set; }
        public string qty { get; set; }
        public string mrp { get; set; }
    }

    public class AddCompetitorStockOutPut
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class CompetitorStockListInput
    {
        public string user_id { get; set; }
        public string session_token { get; set; }
        public string date { get; set; }
    }

    public class CompetitorStockListOutPut
    {
        public string status { get; set; }
        public string message { get; set; }
        public string total_stocklist_count { get; set; }
        public List<CompetitorStockLists> competitor_stock_list { get; set; }
    }

    public class CompetitorStockLists
    {
        public DateTime visited_datetime { get; set; }
        public string competitor_stock_id { get; set; }
        public string shop_id { get; set; }
        public string total_qty{get;set;}
        public List<CompetitorStockProductList> product_list { get; set; }
    }

    public class CompetitorStockProductList
    {
        public int id { get; set; }
        public string brand_name { get; set; }
        public string product_name { get; set; }
        public string qty { get; set; }
        public string mrp { get; set; }
    }
}
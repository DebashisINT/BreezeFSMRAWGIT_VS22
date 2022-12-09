using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class StockModel
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string stock_amount { get; set; }
        public string shop_id { get; set; }
        public string stock_id { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string address { get; set; }
        public string shop_type { get; set; }
        public DateTime? stock_date_time { get; set; }
        public List<StockProductlist> product_list { get; set; }
    }

    public class StockProductlist
    {
        public string id { get; set; }
        public string product_name { get; set; }
        public string qty { get; set; }
        public string rate { get; set; }
        public string total_price { get; set; }
    }

    public class Stockoutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string user_id { get; set; }
        public string Stock_amount { get; set; }
        public string Stock_id { get; set; }
        public string description { get; set; }

        public string Shop_Name { get; set; }
        public string Shop_Owner_Contact { get; set; }
        public string Shop_Owner { get; set; }

    }

    public class StockDetailsfetchInput_Shop
    {
        public string session_token { get; set; }
        public string user_id { get; set; }

        //public string date { get; set; }

    }

    public class StockDetailsOutput_Shop
    {
        public string status { get; set; }
        public string message { get; set; }
        public string total_stocklist_count { get; set; }
        public List<StockfetchdetailsList_Shop> stock_list { get; set; }
    }

    public class StockfetchdetailsList_Shop
    {
        public string shop_id { get; set; }
        public string shop_name { get; set; }
        public string address { get; set; }
        public string pin_code { get; set; }
        public string shop_lat { get; set; }
        public string shop_long { get; set; }
        public string stock_id { get; set; }
        public Nullable<DateTime> stock_date_time { get; set; }
        public Decimal stock_amount { get; set; }
        public string stock_qty { get; set; }
        public List<ShopProducts_Shop> product_list { get; set; }
    }
    public class ShopProducts_Shop
    {
        public long id { get; set; }
        public long brand_id { get; set; }
        public long category_id { get; set; }
        public long watt_id { get; set; }

        public string brand { get; set; }
        public string category { get; set; }
        public string watt { get; set; }
        public string product_name { get; set; }

        public decimal qty { get; set; }
        public decimal rate { get; set; }
        public decimal total_price { get; set; }

    }


    // Current Stock
    public class CurrentStockModel
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string stock_id { get; set; }
        public string shop_id { get; set; }
        public DateTime? visited_datetime { get; set; }
        public List<CurrentStockProductlist> stock_product_list { get; set; }
    }

    public class CurrentStockProductlist
    {
        public string product_id { get; set; }
        public string product_stock_qty { get; set; }
    }

    public class CurrentStockModelOutPut
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class CurrentStockListInput
    {
        public string user_id { get; set; }
        public string session_token { get; set; }
        public string date { get; set; }
    }

    public class CurrentStockListOutPut
    {
        public string status { get; set; }
        public string message { get; set; }
        public string total_stocklist_count { get; set; }
        public List<CurrentStockLists> stock_list { get; set; }
    }

    public class CurrentStockLists
    {
        public DateTime visited_datetime { get; set; }
        public string stock_id { get; set; }
        public string shop_id { get; set; }
        public string total_qty { get; set; }
        public List<CurrentStockOutProductList> product_list { get; set; }
    }

    public class CurrentStockOutProductList
    {
        public int id { get; set; }
        public int product_id { get; set; }
        public string product_name { get; set; }
        public string product_stock_qty { get; set; }
    }
}
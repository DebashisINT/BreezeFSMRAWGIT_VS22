#region======================================Revision History=========================================================
//1.0   V2.0.41     Debashis    21/08/2023      Some new parameters have been added.Row: 865
//2.0   V2.0.45     Debashis    03/04/2024      Some new parameters have been added.Row: 909,910 & 916
#endregion===================================End of Revision History==================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class Productclass
    {
        public long id { get; set; }
        public long brand_id { get; set; }
        public long category_id { get; set; }
        public long watt_id { get; set; }
        public string brand { get; set; }
        public string product_name { get; set; }
        public string category { get; set; }

        public string watt { get; set; }
        //Rev Debashis Row:773
        public decimal product_mrp_show { get; set; }
        //End of Rev Debashis Row:773 
        //Rev Debashis Row:777
        public decimal product_discount_show { get; set; }
        //End of Rev Debashis Row:777

    }
    public class ProductclassInput
    {

        public string session_token { get; set; }
        public string user_id { get; set; }

        public string last_update_date { get; set; }

    }

    public class ProductlistOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public int total_product_list_count { get; set; }
        public List<Productclass> product_list { get; set; }
    }

    public class ProductRateInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string shop_id { get; set; }
    }

    public class ProductRateOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<ProductRateList> product_rate_list { get; set; }
    }

    public class ProductRateList
    {
        public String product_id { get; set; }
        public String rate { get; set; }
        public String stock_amount { get; set; }
        public String stock_unit { get; set; }
        public bool isStockShow { get; set; }
        public bool isRateShow { get; set; }
        //Rev 1.0 Row:865
        public decimal Qty_per_Unit { get; set; }
        public decimal Scheme_Qty { get; set; }
        public decimal Effective_Rate { get; set; }
        //End of Rev 1.0 Row:865
    }

    public class OfflineProductRateInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
    }

    public class OfflineProductRateOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<OfflineProductRateList> product_rate_list { get; set; }
    }

    public class OfflineProductRateList
    {
        public String product_id { get; set; }
        public String rate1 { get; set; }
        public String rate2 { get; set; }
        public String rate3 { get; set; }
        public String rate4 { get; set; }
        public String rate5 { get; set; }
        public String stock_amount { get; set; }
        public String stock_unit { get; set; }
        public bool isStockShow { get; set; }
        public bool isRateShow { get; set; }
    }

    public class ModelListInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
    }

    public class ModelListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        //public List<ItemList> model_list { get; set; }
        public List<ModelItemList> model_list { get; set; }
    }

    public class ItemList
    {
        public String id { get; set; }
        public String name { get; set; }
    }

    public class ModelItemList
    {
        public String model_id { get; set; }
        public String model_name { get; set; }
    }

    public class PrimaryApplicationtOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<ItemList> primary_application_list { get; set; }
    }

    public class SecondaryApplicationOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<ItemList> secondary_application_list { get; set; }
    }

    //Rev 2.0 Row: 909 & 910
    public class ITCProdMastListInput
    {
        public long user_id { get; set; }
    }

    public class ITCProdMastListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<ITCProdMastLists> product_list { get; set; }
    }

    public class ITCProdMastLists
    {
        public long product_id { get; set; }
        public string product_name { get; set; }
        public long brand_id { get; set; }
        public string brand_name { get; set; }
        public long category_id { get; set; }
        public string category_name { get; set; }
        public long watt_id { get; set; }
        public string watt_name { get; set; }
        public string UOM { get; set; }
    }

    public class ITCProdRateListInput
    {
        public long user_id { get; set; }
    }

    public class ITCProdRateListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<ITCProdRateLists> product_rate_list { get; set; }
    }

    public class ITCProdRateLists
    {
        public long product_id { get; set; }
        public decimal mrp { get; set; }
        public decimal item_price { get; set; }
        public decimal specialRate { get; set; }
    }
    //End of Rev 2.0 Row: 909 & 910

}



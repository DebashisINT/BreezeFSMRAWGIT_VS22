using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class OrderWithProductAttributeInputModel
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string order_id { get; set; }
        public string shop_id { get; set; }
        public string order_date { get; set; }
        public List<ProductListInput> product_list { get; set; }
    }

    public class ProductListInput
    {
        public string product_id { get; set; }
        public string product_name { get; set; }
        public string gender { get; set; }
        public string size { get; set; }
        public int qty { get; set; }
        public string color_id { get; set; }
        public decimal rate { get; set; }
    }

    public class OrderWithProductAttributeOutputModel
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class ListOrderProductInputModel
    {        
        public string user_id { get; set; }
        public string session_token { get; set; }
    }

    public class ListOrderProductOutputModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<GenderlistOutput> Gender_list { get; set; }
        public List<OrderProductlistOutput> Product_list { get; set; }
        public List<ColorlistOutput> Color_list { get; set; }
        public List<SizelistOutput> size_list { get; set; }
    }

    public class GenderlistOutput
    {
        public int gender_id { get; set; }
        public string gender { get; set; }
    }

    public class OrderProductlistOutput
    {
        public long product_id { get; set; }
        public string product_name { get; set; }
        public string product_for_gender { get; set; }
    }

    public class ColorlistOutput
    {
        public int color_id { get; set; }
        public string color_name { get; set; }
        public long product_id { get; set; }
    }

    public class SizelistOutput
    {
        public string size { get; set; }
        public long product_id { get; set; }
    }

    public class NewListOrderProductInputModel
    {
        public string user_id { get; set; }
        public string session_token { get; set; }
    }

    public class NewListOrderProductOutputModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public string user_id { get; set; }
        public List<ShoplistOutput> Shop_list { get; set; }
    }

    public class ShoplistOutput
    {
        public string shop_name { get; set; }
        public string shop_id { get; set; }
        public string owner_name { get; set; }
        public string PhoneNumber { get; set; }
        public List<OrderlistOutput> OrderList { get; set; }
    }

    public class OrderlistOutput
    {
        public string order_id { get; set; }
        public string order_date { get; set; }
        public List<NewProductlistOutput> product_list { get; set; }
    }

    public class NewProductlistOutput
    {
        public long product_id { get; set; }
        public string product_name { get; set; }
        public string gender { get; set; }
        public List<NewColorlistOutput> color_list { get; set; }
    }

    public class NewColorlistOutput
    {
        public string size { get; set; }
        public int qty { get; set; }
        public string color_id { get; set; }
    }

    public class NewProductOrderListInputModel
    {
        public string user_id { get; set; }
        public string session_token { get; set; }
    }

    public class NewProductOrderListOutputModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public string user_id { get; set; }
        public List<ProdOrderlistOutput> order_list { get; set; }
    }

    public class ProdOrderlistOutput
    {
        public string order_id { get; set; }
        public long product_id { get; set; }
        public string product_name { get; set; }
        public string gender { get; set; }
        public string size { get; set; }
        public int qty { get; set; }
        public string order_date { get; set; }
        public string shop_id { get; set; }
        public int color_id { get; set; }
        public string color_name { get; set; }
        public bool isUploaded { get; set; }
        public decimal rate { get; set; }
    }
}
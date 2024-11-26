#region======================================Revision History=========================================================
//1.0   V2.0.38     Debashis    23/01/2023      Some new parameters have been added.Row: 805 to 806
//2.0   V2.0.49     Debashis    17/09/2024      A new parameter has been added.Row: 977
#endregion===================================End of Revision History==================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class OrdermanagementInput
    {

        public string session_token { get; set; }
        public string user_id { get; set; }
        public string order_amount { get; set; }
        public string order_id { get; set; }
        public string shop_id { get; set; }
        public string description { get; set; }

        public string collection { get; set; }
        public DateTime? order_date { get; set; }
        public string remarks { get; set; }
        public List<OrderProductlist> product_list { get; set; }

        //Extra Input for 4Basecare
        public string patient_no { get; set; }
        public string patient_name { get; set; }
        public string patient_address { get; set; }
        //Extra Input for 4Basecare
        //Extra Input for RubyFood
        public decimal scheme_amount { get; set; }
        //Extra Input for RubyFood
        //Extra Input for EuroBond
        public string Hospital { get; set; }
        public string Email_Address { get; set; }
        //Extra Input for EuroBond
        //Rev 2.0 Row: 977
        public string OrderStatus { get; set; }
        //End of Rev 2.0 Row: 977
    }
    public class OrderProductlist
    {
        public string id { get; set; }
        public string qty { get; set; }

        public string rate { get; set; }
        public string total_price { get; set; }
        public string product_name { get; set; }
        //Extra Input for RubyFood
        public decimal scheme_qty { get; set; }
        public decimal scheme_rate { get; set; }
        public decimal total_scheme_price { get; set; }
        //Extra Input for RubyFood
        //Extra Input for EuroBond
        public decimal MRP { get; set; }
        //Extra Input for EuroBond
        //Rev 1.0 Row: 805
        public decimal order_mrp { get; set; }
        public decimal order_discount { get; set; }
        //End of Rev 1.0 Row: 805
    }

    public class OrderAddoutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string user_id { get; set; }
        public string order_amount { get; set; }
        public string order_id { get; set; }
        public string description { get; set; }

        public string Shop_Name { get; set; }
        public string Shop_Owner_Contact { get; set; }
        public string Shop_Owner { get; set; }

    }


    public class OrderfetchInput
    {


        public string user_id { get; set; }

        public string date { get; set; }


    }


    public class OrderfetchOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<Orderfetch> order_list { get; set; }

    }

    public class Orderfetch
    {


        public DateTime? date { get; set; }
        public string shop_id { get; set; }
        public string shop_name { get; set; }
        public string address { get; set; }
        public string pin_code { get; set; }
        public string shop_lat { get; set; }
        public string shop_long { get; set; }
        public string owner_name { get; set; }
        public string owner_contact_no { get; set; }
        public string owner_email { get; set; }
        public string shop_performance { get; set; }
        public string order_amount { get; set; }
        public string order_id { get; set; }
        public string shop_image_link { get; set; }
        public string collection { get; set; }

    }


    public class OrderDetailsfetchInput
    {

        public string session_token { get; set; }
        public string user_id { get; set; }

        public string shop_id { get; set; }

        public string order_id { get; set; }

    }
    public class OrderDetailsOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string total_orderlist_count { get; set; }
        public string order_id { get; set; }

        public string shop_id { get; set; }
        public List<OrderfetchdetailsList> order_details_list { get; set; }

    }

    public class OrderfetchdetailsList
    {
        public string id { get; set; }
        public string date { get; set; }
        public string amount { get; set; }
        public string description { get; set; }
        public string collection { get; set; }
        public List<OrderProducts> product_list { get; set; }
    }
    public class OrderProducts
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


    #region Order Details Shop List

    public class OrderDetailsfetchInput_Shop
    {
        public string session_token { get; set; }
        public string user_id { get; set; }

        public string date { get; set; }

    }
    public class OrderDetailsOutput_Shop
    {
        public string status { get; set; }
        public string message { get; set; }
        public string total_orderlist_count { get; set; }

        public List<OrderfetchdetailsList_Shop> order_list { get; set; }

    }


    public class OrderfetchdetailsList_Shop
    {
        public string shop_id { get; set; }
        public string shop_address { get; set; }
        public string shop_name { get; set; }
        public string shop_contact_no { get; set; }
        public string pin_code { get; set; }
        public string shop_lat { get; set; }
        public string shop_long { get; set; }
        public string order_id { get; set; }
        public Nullable<DateTime> order_date_time { get; set; }
        public Decimal order_amount { get; set; }
        public string order_lat { get; set; }
        public string order_long { get; set; }

        //Extra Input for 4Basecare
        public string patient_no { get; set; }
        public string patient_name { get; set; }
        public string patient_address { get; set; }
        //Extra Input for 4Basecare
        //Extra Output for RubyFood
        public decimal scheme_amount { get; set; }
        //Extra Output for RubyFood
        //Extra Input for EuroBond
        public string Hospital { get; set; }
        public string Email_Address { get; set; }
        //Extra Input for EuroBond
        public List<OrderProducts_Shop> product_list { get; set; }
        //Rev Debashis
        //public string order_lat { get; set; }
        //public string order_long { get; set; }

        ////Extra Input for 4Basecare
        //public string patient_no { get; set; }
        //public string patient_name { get; set; }
        //public string patient_address { get; set; }
        ////Extra Input for 4Basecare
        //End of Rev Debashis
    }
    public class OrderProducts_Shop
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
        //Extra Output for RubyFood
        public decimal scheme_qty { get; set; }
        public decimal scheme_rate { get; set; }
        public decimal total_scheme_price { get; set; }
        //Extra Output for RubyFood
        //Extra Output for EuroBond
        public decimal MRP { get; set; }
        //Extra Output for EuroBond
        //Rev 1.0 Row: 806
        public decimal order_mrp { get; set; }
        public decimal order_discount { get; set; }
        //End of Rev 1.0 Row: 806
    }

    #endregion



    public class OrderMailInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string order_id { get; set; }
        public string shop_id { get; set; }
        public string type { get; set; }

    }

    public class OrderMailOutput
    {
        public string status { get; set; }
        public string message { get; set; }

    }
    //Rev 3.0 Row: 978
    public class OrderStatusfetchInput
    {
        public Int32 user_id { get; set; }
    }
    public class OrderStatusfetchOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public Int32 user_id { get; set; }
        public List<OrderStatusfetch> order_status_list { get; set; }
    }
    public class OrderStatusfetch
    {
        public string Order_Code { get; set; }
        public string OrderStatus { get; set; }
        public string Order_date { get; set; }
    }
    //End of Rev 3.0 Row: 978
}
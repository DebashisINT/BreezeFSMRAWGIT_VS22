using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class Collectionclass_Input
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string shop_id { get; set; }
        public string collection { get; set; }
        public string collection_id { get; set; }
        public string collection_date { get; set; }
        public string bill_id { get; set; }

        //Extra Input for Collection
        public string payment_id { get; set; }
        public string instrument_no { get; set; }
        public string bank { get; set; }
        public string remarks { get; set; }
        //Extra Input for Collection

        //Extra Input for 4Basecare
        public string patient_no { get; set; }
        public string patient_name { get; set; }
        public string patient_address { get; set; }
        //Extra Input for 4Basecare
        //Extra Input for EuroBond
        public string Hospital { get; set; }
        public string Email_Address { get; set; }
        //Extra Input for EuroBond
        //Rev Debashis
        public string order_id { get; set; }
        //End of Rev Debashis
    }


    public class CollectionInvoiceList_Input
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string shop_id { get; set; }

    }

    public class InvoiceListReport_Input
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string date { get; set; }

    }

    public class CollectionListReport_Input
    {
        public string session_token { get; set; }
        public string user_id { get; set; }

    }


    public class CollectionListReport_Output
    {
        public string status { get; set; }
        public string message { get; set; }
        public string total_pending { get; set; }
        public string total_paid { get; set; }
        public string today_pending { get; set; }
        public string today_paid { get; set; }

    }


    public class InvoiceListReport_Output
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<InvoiceListReport> amount_list { get; set; }


    }

    public class InvoiceListReport
    {
        public string shop_id { get; set; }
        public string shop_name { get; set; }
        public string shop_image { get; set; }
        public string total_amount { get; set; }
        public string total_collection { get; set; }
        public string total_bal { get; set; }

    }


    public class CollectionInvoiceList_Output
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<Invoice_List> billing_list { get; set; }


    }


    public class Invoice_List
    {
        public string user_id { get; set; }
        public string bill_id { get; set; }
        public string invoice_no { get; set; }
        public string invoice_date { get; set; }
        public string total_amount { get; set; }
        public string paid_amount { get; set; }
        public string bal_amount { get; set; }
        public string order_id { get; set; }
        public string billing_image { get; set; }

        public List<Product_List> product_list { get; set; }

    }


    public class Product_List
    {
        public string id { get; set; }
        public string product_name { get; set; }
        public string brand { get; set; }
        public string brand_id { get; set; }
        public string category { get; set; }
        public string category_id { get; set; }
        public string watt { get; set; }
        public string watt_id { get; set; }
        public string qty { get; set; }
        public string rate { get; set; }
        public string total_price { get; set; }

    }





    public class Collectionclass_Output
    {
        public string status { get; set; }
        public string message { get; set; }
        public string user_id { get; set; }
        public string shop_id { get; set; }
        public string collection { get; set; }
        public string Shop_Name { get; set; }
        public string Shop_Owner_Contact { get; set; }
        public string Shop_Owner { get; set; }

    }


    public class CollectionList_Output
    {
        public string status { get; set; }
        public string message { get; set; }
        public int total_orderlist_count { get; set; }
        public List<collection_details_list> collection_details_list { get; set; }
    }
    public class collection_details_list
    {
        public string shop_id { get; set; }
        public decimal collection { get; set; }
        public string collection_date { get; set; }
        public string collection_id { get; set; }
    }

    public class CollectionList_Output_bind
    {
        public string status { get; set; }
        public string message { get; set; }
        public int total_collectionist_count { get; set; }
        public List<collection_details_list_bind> collection_list { get; set; }
    }
    public class collection_details_list_bind
    {
        public string shop_id { get; set; }
        public decimal collection { get; set; }
        //public DateTime collection_date { get; set; }
        public string date { get; set; }
        public string only_time { get; set; }
        public string collection_id { get; set; }

        public string shop_name { get; set; }
        public string address { get; set; }
        public string pin_code { get; set; }
        public string shop_lat { get; set; }
        public string shop_long { get; set; }

        //Extra OutPut for CollectionList
        public string payment_id { get; set; }
        public string instrument_no { get; set; }
        public string bank { get; set; }
        //public string remarks { get; set; }
        public string feedback { get; set; }
        //public string doc { get; set; }
        public string file_path { get; set; }
        public string bill_id { get; set; }
        public string order_id { get; set; }
        //Extra OutPut for CollectionList

        //Extra Input for 4Basecare
        public string patient_no { get; set; }
        public string patient_name { get; set; }
        public string patient_address { get; set; }
        //Extra Input for 4Basecare
        //Extra Input for EuroBond
        public string Hospital { get; set; }
        public string Email_Address { get; set; }
        //Extra Input for EuroBond

        public bool isUploaded { get; set; }
    }

    public class PaymentMode_Input
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
    }

    public class PaymentMode
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<PaymentMode_list> paymemt_mode_list { get; set; }
    }
    public class PaymentMode_list
    {
        public long id { get; set; }
        public string name { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class BillingModel
    {
        public string user_id { get; set; }
        public string bill_id { get; set; }
        public string invoice_no { get; set; }
        public string invoice_date { get; set; }
        public string invoice_amount { get; set; }
        public string remarks { get; set; }
        public string order_id { get; set; }
        //Add Product in add billing Tanmoy 22-11-2019
        public List<ProductList> product_list { get; set; }
        //End Add Product in add billing Tanmoy 22-11-2019

        //Extra Input for 4Basecare
        public string patient_no { get; set; }
        public string patient_name { get; set; }
        public string patient_address { get; set; }
        //Extra Input for 4Basecare
    }

    public class BillingListInput
    {
        public string user_id { get; set; }
        public string session_token { get; set; }
        public string order_id { get; set; }
    }

    public class BillingListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<BillingModelList> billing_list { get; set; }
    }

    public class BillingModelList
    {
        public string user_id { get; set; }
        public string bill_id { get; set; }
        public string invoice_no { get; set; }
        public string invoice_date { get; set; }
        public string invoice_amount { get; set; }
        public string remarks { get; set; }
        public string order_id { get; set; }
        public string billing_image { get; set; }
        public List<ProductsBillList> product_list { get; set; }
        //Extra Input for 4Basecare
        public string patient_no { get; set; }
        public string patient_name { get; set; }
        public string patient_address { get; set; }
        //Extra Input for 4Basecare
    }

    //Add Product in add billing Tanmoy 22-11-2019
    public class ProductList
    {
        public String id { get; set; }
        public String product_name { get; set; }
        public decimal qty { get; set; }
        public decimal rate { get; set; }
        public decimal total_price { get; set; }
    }
    //End Add Product in add billing Tanmoy 22-11-2019

    public class BillingInputData
    {
        public string data { get; set; }
        public HttpPostedFileBase billing_image { get; set; }
    }

    public class BillingImageInput
    {
        public String session_token { get; set; }
        public String user_id { get; set; }
        public String bill_id { get; set; }
        public String invoice_no { get; set; }
        public String invoice_date { get; set; }
        public String invoice_amount { get; set; }
        public String remarks { get; set; }
        public String order_id { get; set; }
        public string billing_image { get; set; }
        //Add Product in add billing Tanmoy 22-11-2019
        public List<ProductList> product_list { get; set; }
        //End Add Product in add billing Tanmoy 22-11-2019

        //Extra Input for 4Basecare
        public string patient_no { get; set; }
        public string patient_name { get; set; }
        public string patient_address { get; set; }
        //Extra Input for 4Basecare
    }


    public class ProductsBillList
    {
        public String id { get; set; }
        public String product_name { get; set; }

        public String brand { get; set; }
        public String brand_id { get; set; }
        public String category { get; set; }
        public String category_id { get; set; }
        public String watt { get; set; }
        public String watt_id { get; set; }

        public decimal qty { get; set; }
        public decimal rate { get; set; }
        public decimal total_price { get; set; }
    }
}
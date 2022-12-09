using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class SalesQuotationSaveInput
    {
        //public string session_token { get; set; }
        public string user_id { get; set; }
        public string save_date_time { get; set; }
        public string quotation_number { get; set; }
        public string quotation_date_selection { get; set; }
        public string project_name { get; set; }
        public string taxes { get; set; }
        public string Freight { get; set; }
        public string delivery_time { get; set; }
        public string payment { get; set; }
        public string validity { get; set; }
        public string billing { get; set; }
        public string product_tolerance_of_thickness { get; set; }
        public string tolerance_of_coating_thickness { get; set; }
        public string salesman_user_id { get; set; }
        public string shop_id { get; set; }
        public string quotation_created_lat { get; set; }
        public string quotation_created_long { get; set; }
        public string quotation_created_address { get; set; }
        //Rev Debashis Row:732
        public string Remarks { get; set; }
        public string document_number { get; set; }
        public string quotation_status { get; set; }
        //End of Rev Debashis Row:732
        public List<ProductListSaveInput> product_list { get; set; }
    }

    public class ProductListSaveInput
    {
        public string product_id { get; set; }
        public string color_id { get; set; }
        public decimal rate_sqft { get; set; }
        public decimal rate_sqmtr { get; set; }
        public int qty { get; set; }
        public decimal amount { get; set; }
    }

    public class SalesQuotationSaveOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class ShopWiseSalesQuotationListInput
    {
        [Required]
        public string shop_id { get; set; }
    }
    public class ShopWiseSalesQuotationListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string shop_id { get; set; }
        public string shop_name { get; set; }
        public string shop_phone_no { get; set; }
        public List<ShopwisesalesquotationlistOutput> shop_wise_quotation_list { get; set; }
    }
    public class ShopwisesalesquotationlistOutput
    {
        public string quotation_number { get; set; }
        public DateTime save_date_time { get; set; }
        //Rev Debashis Row:733
        public string quotation_status { get; set; }
        public string document_number { get; set; }
        //End of Rev Debashis Row:733
    }

    public class SalesQuotationListInput
    {
        [Required]
        public string quotation_number { get; set; }
    }

    public class SalesQuotationListOutput
    {
        public string status { get; set; }
        public string message { get; set; }        
        public string quotation_number { get; set; }
        public string save_date_time { get; set; }
        public string quotation_date_selection { get; set; }
        public string project_name { get; set; }
        public string taxes { get; set; }
        public string Freight { get; set; }
        public string delivery_time { get; set; }
        public string payment { get; set; }
        public string validity { get; set; }
        public string billing { get; set; }
        public string product_tolerance_of_thickness { get; set; }
        public string tolerance_of_coating_thickness { get; set; }
        public string salesman_user_id { get; set; }
        public string shop_id { get; set; }
        public string shop_name { get; set; }
        public string shop_phone_no { get; set; }
        public string quotation_created_lat { get; set; }
        public string quotation_created_long { get; set; }
        public string quotation_created_address { get; set; }
        public string shop_addr { get; set; }
        public string shop_email { get; set; }
        public string shop_owner_name { get; set; }
        public string salesman_name { get; set; }
        public string salesman_designation { get; set; }
        public string salesman_login_id { get; set; }
        public string salesman_email { get; set; }
        public string salesman_phone_no { get; set; }
        //Rev Debashis Row:734
        public string Remarks { get; set; }
        public string document_number { get; set; }
        //End of Rev Debashis Row:734
        //Rev Debashis Row:747
        public string shop_address_pincode { get; set; }
        //End of Rev Debashis Row:747
        public List<QuotationProductDetailsList> quotation_product_details_list { get; set; }
    }

    public class QuotationProductDetailsList
    {
        public long product_id { get; set; }
        public string product_name { get; set; }
        public string color_id { get; set; }
        public string color_name { get; set; }
        public decimal rate_sqft { get; set; }
        public decimal rate_sqmtr { get; set; }
        public int qty { get; set; }
        public decimal amount { get; set; }
    }

    public class SalesQuotationEditInput
    {
        public string updated_by_user_id { get; set; }
        public string updated_date_time { get; set; }
        public string quotation_number { get; set; }
        public string quotation_date_selection { get; set; }
        public string project_name { get; set; }
        public string taxes { get; set; }
        public string Freight { get; set; }
        public string delivery_time { get; set; }
        public string payment { get; set; }
        public string validity { get; set; }
        public string billing { get; set; }
        public string product_tolerance_of_thickness { get; set; }
        public string tolerance_of_coating_thickness { get; set; }
        public string salesman_user_id { get; set; }
        public string shop_id { get; set; }
        public string quotation_updated_lat { get; set; }
        public string quotation_updated_long { get; set; }
        public string quotation_updated_address { get; set; }
        public List<ProductListEditInput> product_list { get; set; }
    }

    public class ProductListEditInput
    {
        public string product_id { get; set; }
        public string color_id { get; set; }
        public decimal rate_sqft { get; set; }
        public decimal rate_sqmtr { get; set; }
        public int qty { get; set; }
        public decimal amount { get; set; }
    }

    public class SalesQuotationEditOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class SalesQuotationDeleteInput
    {
        public string quotation_number { get; set; }
    }

    public class SalesQuotationDeleteOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    //Rev Debashis Row:735
    public class SalesDocumentNoQuotationListInput
    {
        [Required]
        public string document_number { get; set; }
    }

    public class SalesDocumentNoQuotationListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string quotation_number { get; set; }
        public string save_date_time { get; set; }
        public string quotation_date_selection { get; set; }
        public string project_name { get; set; }
        public string taxes { get; set; }
        public string Freight { get; set; }
        public string delivery_time { get; set; }
        public string payment { get; set; }
        public string validity { get; set; }
        public string billing { get; set; }
        public string product_tolerance_of_thickness { get; set; }
        public string tolerance_of_coating_thickness { get; set; }
        public string salesman_user_id { get; set; }
        public string shop_id { get; set; }
        public string shop_name { get; set; }
        public string shop_phone_no { get; set; }
        public string quotation_created_lat { get; set; }
        public string quotation_created_long { get; set; }
        public string quotation_created_address { get; set; }
        public string shop_addr { get; set; }
        public string shop_email { get; set; }
        public string shop_owner_name { get; set; }
        public string salesman_name { get; set; }
        public string salesman_designation { get; set; }
        public string salesman_login_id { get; set; }
        public string salesman_email { get; set; }
        public string salesman_phone_no { get; set; }
        public string Remarks { get; set; }
        public string document_number { get; set; }
        public List<DocumentNoQuotationProductDetailsList> quotation_product_details_list { get; set; }
    }

    public class DocumentNoQuotationProductDetailsList
    {
        public long product_id { get; set; }
        public string product_name { get; set; }
        public string color_id { get; set; }
        public string color_name { get; set; }
        public decimal rate_sqft { get; set; }
        public decimal rate_sqmtr { get; set; }
        public int qty { get; set; }
        public decimal amount { get; set; }
    }
    //End of Rev Debashis Row:735
    //Rev Debashis Row:737
    public class SalesDocumentNoQuotationSaveInput
    {
        public string user_id { get; set; }
        public string save_date_time { get; set; }
        public string quotation_number { get; set; }
        public string quotation_date_selection { get; set; }
        public string project_name { get; set; }
        public string taxes { get; set; }
        public string Freight { get; set; }
        public string delivery_time { get; set; }
        public string payment { get; set; }
        public string validity { get; set; }
        public string billing { get; set; }
        public string product_tolerance_of_thickness { get; set; }
        public string tolerance_of_coating_thickness { get; set; }
        public string salesman_user_id { get; set; }
        public string shop_id { get; set; }
        public string quotation_created_lat { get; set; }
        public string quotation_created_long { get; set; }
        public string quotation_created_address { get; set; }
        public string Remarks { get; set; }
        public string document_number { get; set; }
        public string quotation_status { get; set; }
        public List<DocumentNoProductListSaveInput> product_list { get; set; }
    }

    public class DocumentNoProductListSaveInput
    {
        public string product_id { get; set; }
        public string color_id { get; set; }
        public decimal rate_sqft { get; set; }
        public decimal rate_sqmtr { get; set; }
        public int qty { get; set; }
        public decimal amount { get; set; }
    }

    public class SalesDocumentNoQuotationSaveOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }
    //End of Rev Debashis Row:737
}
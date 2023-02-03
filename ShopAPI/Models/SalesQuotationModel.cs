#region======================================Revision History=========================================================
//1.0   V2.0.32     Debashis    01/09/2022      Some new parameters have been added.Row: 732 to 735
//2.0   V2.0.35     Debashis    14/10/2022      A new parameter has been added.Row: 747
//3.0   V2.0.37     Debashis    10/01/2023      Some new parameters have been added.Row: 790 to 791
//4.0   V2.0.37     Debashis    12/01/2023      Some new parameters have been added.Row: 792 to 793
//5.0   V2.0.37     Debashis    18/01/2023      Some new parameters have been added.Row: 802 to 803
#endregion===================================End of Revision History==================================================
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
        //Rev 1.0 Row:732
        public string Remarks { get; set; }
        public string document_number { get; set; }
        public string quotation_status { get; set; }
        //End of Rev 1.0 Row:732
        //Rev 3.0 Row:791
        public string sel_quotation_pdf_template { get; set; }
        //public string quotation_contact_person { get; set; }
        //public string quotation_contact_number { get; set; }
        //public string quotation_contact_email { get; set; }
        //public string quotation_contact_doa { get; set; }
        //End of Rev 3.0 Row:791
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
        //Rev 1.0 Row:733
        public string quotation_status { get; set; }
        public string document_number { get; set; }
        //End of Rev 1.0 Row:733
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
        //Rev 1.0 Row:734
        public string Remarks { get; set; }
        public string document_number { get; set; }
        //End of Rev 1.0 Row:734
        //Rev 2.0 Row:747
        public string shop_address_pincode { get; set; }
        //End of Rev 2.0 Row:747
        //Rev 3.0 Row:790
        public string sel_quotation_pdf_template { get; set; }
        //public string quotation_contact_person { get; set; }
        //public string quotation_contact_number { get; set; }
        //public string quotation_contact_email { get; set; }
        //public string quotation_contact_doa { get; set; }
        //End of Rev 3.0 Row:790
        public List<QuotationProductDetailsList> quotation_product_details_list { get; set; }
        //Rev 5.0 Row:803
        public List<QuotationExtraContactDetailsList> extra_contact_list { get; set; }
        //End of Rev 5.0 Row:803
    }

    public class QuotationProductDetailsList
    {
        public long product_id { get; set; }
        public string product_name { get; set; }
        //Rev 3.0 Row:790
        public string product_des { get; set; }
        //End of Rev 3.0 Row:790
        public string color_id { get; set; }
        public string color_name { get; set; }
        public decimal rate_sqft { get; set; }
        public decimal rate_sqmtr { get; set; }
        public int qty { get; set; }
        public decimal amount { get; set; }
    }

    //Rev 5.0 Row:803
    public class QuotationExtraContactDetailsList
    {
        public string quotation_contact_person { get; set; }
        public string quotation_contact_number { get; set; }
        public string quotation_contact_email { get; set; }
        public string quotation_contact_doa { get; set; }
        public string quotation_contact_dob { get; set; }
    }
    //End of Rev 5.0 Row:803

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

    //Rev 1.0 Row:735
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
        //Rev 4.0 Row:792
        public string sel_quotation_pdf_template { get; set; }
        //public string quotation_contact_person { get; set; }
        //public string quotation_contact_number { get; set; }
        //public string quotation_contact_email { get; set; }
        //public string quotation_contact_doa { get; set; }
        //End of Rev 4.0 Row:792
        public List<DocumentNoQuotationProductDetailsList> quotation_product_details_list { get; set; }
        //Rev 5.0 Row:804
        public List<DocumentNoQuotationExtraContactDetailsList> extra_contact_list { get; set; }
        //End of Rev 5.0 Row:804
    }

    public class DocumentNoQuotationProductDetailsList
    {
        public long product_id { get; set; }
        public string product_name { get; set; }
        //Rev 4.0 Row:792
        public string product_des { get; set; }
        //End of Rev 4.0 Row:792
        public string color_id { get; set; }
        public string color_name { get; set; }
        public decimal rate_sqft { get; set; }
        public decimal rate_sqmtr { get; set; }
        public int qty { get; set; }
        public decimal amount { get; set; }
    }
    //End of Rev 1.0 Row:735

    //Rev 5.0 Row:804
    public class DocumentNoQuotationExtraContactDetailsList
    {
        public string quotation_contact_person { get; set; }
        public string quotation_contact_number { get; set; }
        public string quotation_contact_email { get; set; }
        public string quotation_contact_doa { get; set; }
        public string quotation_contact_dob { get; set; }
    }
    //End of Rev 5.0 Row:804

    //Rev 1.0 Row:737
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
        //Rev 4.0 Row:793
        public string sel_quotation_pdf_template { get; set; }
        //public string quotation_contact_person { get; set; }
        //public string quotation_contact_number { get; set; }
        //public string quotation_contact_email { get; set; }
        //public string quotation_contact_doa { get; set; }
        //End of Rev 4.0 Row:793
        public List<DocumentNoProductListSaveInput> product_list { get; set; }
        //Rev 5.0 Row:802
        public List<DocumentNoExtraContactListSaveInput> extra_contact_list { get; set; }
        //End of Rev 5.0 Row:802
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

    //Rev 5.0 Row:802
    public class DocumentNoExtraContactListSaveInput
    {
        public string quotation_contact_person { get; set; }
        public string quotation_contact_number { get; set; }
        public string quotation_contact_email { get; set; }
        public string quotation_contact_doa { get; set; }
        public string quotation_contact_dob { get; set; }
    }
    //End of Rev 5.0 Row:802

    public class SalesDocumentNoQuotationSaveOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }
    //End of Rev 1.0 Row:737
}
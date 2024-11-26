/******************************************************************************************************************
  1.0       16-08-2023        V2.0.42          Sanchita          The enquiry doesn't showing in the listing after modification
                                                                 Mantis: 26721
  2.0       23-11-2023        V2.0.43          Sanchita          Bulk Import feature required for Enquiry Module.Mantis: 27020   
  3.0		25-04-2024	      V2.0.46		   Priti             0027383: New Enquires type Add and Hide # Eurobond Portal
  4.0       10/09/2024        V2.0.48          Sanchita          27690: Quotation Notification issue @ Eurobond
**************************************************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class CRMEnquiriesModel
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Fromdate { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Todate { get; set; }

        public List<GetEnquiryFrom> EnquiryFrom { get; set; }
        public List<string> EnquiryFromDesc { get; set; }
        public string Is_PageLoad { get; set; }

        // Add New Enquiries
        public string CRM_ID { get; set; }
        public string Action_type { get; set; }
        public DateTime Date { get; set; }
        public string Customer_Name { get; set; }
        public string Contact_Person { get; set; }
        public string PhoneNo { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [Display(Name = "Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
             ErrorMessage = "Please Enter Correct Email Address")]
        public string Email { get; set; }
        public string Location { get; set; }
        public string Product_Required { get; set; }
        public string Qty { get; set; }
        public string UOM { get; set; }
        public decimal Order_Value { get; set; }
        public string Provided_By { get; set; }
        public string Enq_Details { get; set; }
        public string response_code { get; set; }
        public string response_msg { get; set; }

        // End Add New Enquiries

        // Assign Salesman

        public string Is_PageLoadBulkAssign { get; set; }

        //Mantis Issue 24776
        public string Is_PageLoadBulkReAssign { get; set; }
        //End of Mantis Issue 24776
        public Int32 SalesmanUser { get; set; }
        public string SalesmanUserId { get; set; }
        public List<SalesmanUserAssign> SalesmanUserList { get; set; }

        // End Assign Salesman
        // Rev 1.0
        public string txtDate { get; set; }
        // End of Rev 1.0
    }

    public class GetEnquiryFrom
    {

        //public Int64 EnqID { get; set; }

        public string EnquiryFromDesc { get; set; }
    }

    // Mantis Issue 24890
    public class IndiamartModelClassKey
    {
        public Int32 CODE { get; set; }
        public string STATUS { get; set; }
        public string MESSAGE { get; set; }
        public Int32 TOTAL_RECORDS { get; set; }
        public List<IndiamartModelClass> RESPONSE { get; set; }


    }
    // End of Mantis Issue 24890

    public class IndiamartModelClass
    {
        // Mantis Issue 24890
        //public string Indiamart_Id { get; set; }
        //public string Rn { get; set; }
        //public string QUERY_ID { get; set; }
        //public string QTYPE { get; set; }
        //public string SENDERNAME { get; set; }
        //public string SENDEREMAIL { get; set; }
        //public string SUBJECT { get; set; }
        //public string DATE_RE { get; set; }
        //public string DATE_R { get; set; }
        //public string DATE_TIME_RE { get; set; }
        //public string GLUSR_USR_COMPANYNAME { get; set; }
        //public string READ_STATUS { get; set; }
        //public string SENDER_GLUSR_USR_ID { get; set; }
        //public string MOB { get; set; }
        //public string COUNTRY_FLAG { get; set; }
        //public string QUERY_MODID { get; set; }
        //public string LOG_TIME { get; set; }
        //public string QUERY_MODREFID { get; set; }
        //public string DIR_QUERY_MODREF_TYPE { get; set; }
        //public string ORG_SENDER_GLUSR_ID { get; set; }
        //public string ENQ_MESSAGE { get; set; }
        //public string ENQ_ADDRESS { get; set; }
        //public string ENQ_CALL_DURATION { get; set; }
        //public string ENQ_RECEIVER_MOB { get; set; }
        //public string ENQ_CITY { get; set; }
        //public string ENQ_STATE { get; set; }
        //public string PRODUCT_NAME { get; set; }
        //public string COUNTRY_ISO { get; set; }
        //public string EMAIL_ALT { get; set; }
        //public string MOBILE_ALT { get; set; }
        //public string PHONE { get; set; }
        //public string PHONE_ALT { get; set; }
        //public string IM_MEMBER_SINCE { get; set; }
        //public string TOTAL_COUNT { get; set; }

        public string Indiamart_Id { get; set; }
        public string UNIQUE_QUERY_ID { get; set; }
        public string QUERY_TYPE { get; set; }
        public string QUERY_TIME { get; set; }
        public string SENDER_NAME { get; set; }
        public string SENDER_MOBILE { get; set; }
        public string SENDER_EMAIL { get; set; }
        public string SENDER_COMPANY { get; set; }
        public string SENDER_ADDRESS { get; set; }
        public string SENDER_CITY { get; set; }
        public string SENDER_STATE { get; set; }
        public string SENDER_COUNTRY_ISO { get; set; }
        public string SENDER_MOBILE_ALT { get; set; }
        public string SENDER_EMAIL_ALT { get; set; }
        public string QUERY_PRODUCT_NAME { get; set; }
        public string QUERY_MESSAGE { get; set; }
        public string CALL_DURATION { get; set; }
        public string RECEIVER_MOBILE { get; set; }

        // End of Mantis Issue 24890

    }

    public class IndiamartModelErrorClass
    {
        public string MobileNo { get; set; }
        public string ErrorMessage { get; set; }
        public string jsonError { get; set; }

        public List<IndiamartModelClass> Indiamart { get; set; }

    }

    // Mantis Issue 24936
    public class IndiamartArcherModelClassKey
    {
        public Int32 CODE { get; set; }
        public string STATUS { get; set; }
        public string MESSAGE { get; set; }
        public Int32 TOTAL_RECORDS { get; set; }
        public List<IndiamartArcherModelClass> RESPONSE { get; set; }


    }

    public class IndiamartArcherModelClass
    {
        public string Indiamart_Id { get; set; }
        public string UNIQUE_QUERY_ID { get; set; }
        public string QUERY_TYPE { get; set; }
        public string QUERY_TIME { get; set; }
        public string SENDER_NAME { get; set; }
        public string SENDER_MOBILE { get; set; }
        public string SENDER_EMAIL { get; set; }
        public string SENDER_COMPANY { get; set; }
        public string SENDER_ADDRESS { get; set; }
        public string SENDER_CITY { get; set; }
        public string SENDER_STATE { get; set; }
        public string SENDER_COUNTRY_ISO { get; set; }
        public string SENDER_MOBILE_ALT { get; set; }
        public string SENDER_EMAIL_ALT { get; set; }
        public string QUERY_PRODUCT_NAME { get; set; }
        public string QUERY_MESSAGE { get; set; }
        public string CALL_DURATION { get; set; }
        public string RECEIVER_MOBILE { get; set; }

   
    }

    public class IndiamartArcherModelErrorClass
    {
        public string MobileNo { get; set; }
        public string ErrorMessage { get; set; }
        public string jsonError { get; set; }

        public List<IndiamartArcherModelClass> IndiamartArcher { get; set; }

    }
    // End of Mantis Issue 24936

    public class SalesmanUserAssign
    {
        public string UserID { get; set; }

        public string username { get; set; }
    }

    public class MacoyModelClassKey 
    {
        public Boolean status { get; set; }
        public string message { get; set; }
        public Int32 statusCode { get; set; }
        public MacoyModelClassKeyData data { get; set; }


    }
    public class MacoyModelClassKeyData
    {
        public string user { get; set; }
        public string key { get; set; }
        public string token { get; set; }

    }


    public class MacoyModelClassLead
    {
        public Boolean status { get; set; }
        public string message { get; set; }
        public Int32 statusCode { get; set; }
        public List<MacoyModelClassLeadData> data { get; set; }


    }
    public class MacoyModelClassLeadData
    {
        public string lead_id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string location { get; set; }
        public string lead_for { get; set; }
        public string quantity { get; set; }
        public string glv { get; set; }
        public string required_for { get; set; }

        public string role { get; set; }
        public string stage_requirement { get; set; }
        public string description { get; set; }
        public string date { get; set; }
    }

    // Rev 2.0
    public class CRMEnquiriesImportLogModel
    {
        public DateTime Date { get; set; }
        public string Customer_Name { get; set; }
        public string Contact_Person { get; set; }
        public string PhoneNo { get; set; }

        public string Email { get; set; }
        public string Location { get; set; }
        public string Product_Required { get; set; }
        public decimal Qty { get; set; }
        public string UOM { get; set; }
        public decimal Order_Value { get; set; }
        public string vend_type { get; set; }
        public string Enq_Details { get; set; }
        public DateTime ImportDate { get; set; }
        public string ImportMsg { get; set; }
        public string ImportStatus { get; set; }
        
       
        
    }
    // End of Rev 2.0

    //Rev 3.0
    public class ProvidedBy
    {
        public string EnquiryFromDescValue { get; set; }
        public string EnquiryFromDesc { get; set; }
    }
    //Rev 3.0 End

    // Rev 4.0
    public class Data
    {

        public string body {get; set;}
        public string title { get; set; }
        public string key_1 { get; set; }
        public string key_2 { get; set; }
        public string UserName { get; set; }
        public string UserID { get; set; }
        public string header { get; set; }
        public string type { get; set; }
        public string imgNotification_Icon { get; set; }
        public string lead_date {  get; set; }
        public string enquiry_type { get; set;}
    }

    public class Message
    {
        public string token { get; set; }
        public Data data { get; set; }
        public Notification notification { get; set; }
    }

    public class Notification
    {
        public string title { get; set; }
        public string body { get; set; }
    }

    public class Root
    {
        public Message message { get; set; }

    }
    // End of Rev 4.0
}


using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml;

using System.IO;

using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
namespace BreezeERPAPI.Models
{
    #region CountryList
    public class countyryListRespose
    {
        public string ResponseCode { get; set; }
        public string Responsedetails { get; set; }
        public List<CountryList> country_list { get; set; }
    }
    public class CountryList
    {

        public int cou_id { get; set; }
        public string cou_country { get; set; }

    }



    #endregion

    #region StateList
    public class StateListResponse
    {
        public string ResponseCode { get; set; }
        public string Responsedetails { get; set; }
        public string isUpdated { get; set; }
        public List<StateList> state_list { get; set; }
    }
    public class StateList
    {
        public int state_id { get; set; }
        public string state_name { get; set; }
        public int country_id { get; set; }
    }



    #endregion

    #region CityList
    public class CityListResponse
    {
        public string ResponseCode { get; set; }
        public string Responsedetails { get; set; }
        public string isUpdated { get; set; }
        public List<CityList> city_list { get; set; }
    }
    public class CityList
    {

        public int city_id { get; set; }
        public string city_name { get; set; }
        public int state_id { get; set; }

    }



    #endregion


    #region PincodeList
    public class PincodeListResponse
    {
        public string ResponseCode { get; set; }
        public string Responsedetails { get; set; }
        public List<PincodeList> pincodelist { get; set; }
    }
    public class PincodeList
    {
        public int pincode_id { get; set; }
        public string pincode_no { get; set; }
        public decimal city_id { get; set; }
    }

    #endregion



    #region Customer Add
    public class CustomerInputParameters
    {
        public string mobile_no { get; set; }
        public string alternate_mobile_no { get; set; }
        public string email { get; set; }
        public string pan_number { get; set; }
        public string aadhar_no { get; set; }
        public string cust_name { get; set; }



        public string gender { get; set; }
        public string date_of_birth { get; set; }
        public string block_no { get; set; }
        public string street_no { get; set; }
        public string flat_no { get; set; }
        public string floor { get; set; }
        public string landmark { get; set; }
        public string country { get; set; }
        public string state { get; set; }

        public string city { get; set; }
        public string pin_code { get; set; }
        public string sales_man_id { get; set; }

        public string Token { get; set; }


    }
    public class CustomerOutputParameters
    {
        public string ResponseCode { get; set; }
        public string Responsedetails { get; set; }
        public string customer_id { get; set; }
        public string customer_name { get; set; }



    }


    #endregion

    #region Customer Update
    public class CustomerupdateInputParameters
    {
        public string mobile_no { get; set; }
        public string alternate_mobile_no { get; set; }
        public string email { get; set; }
        public string pan_number { get; set; }
        public string aadhar_no { get; set; }
        public string cust_name { get; set; }



        public string gender { get; set; }
        public string date_of_birth { get; set; }
        public string block_no { get; set; }
        public string street_no { get; set; }
        public string flat_no { get; set; }
        public string floor { get; set; }
        public string landmark { get; set; }
        public string country { get; set; }
        public string state { get; set; }

        public string city { get; set; }
        public string pin_code { get; set; }
        public string sales_man_id { get; set; }
        public string customer_id { get; set; }
        public string Token { get; set; }


    }

    public class CustomerUpdateOutputParameters
    {
        public string ResponseCode { get; set; }
        public string Responsedetails { get; set; }




    }

    #endregion

    #region Product List
    public class ProductListOutput
    {


        public string ResponseCode { get; set; }
        public string Responsedetails { get; set; }

        public int totalcount { get; set; }
        public List<ProductDetails> product_details { get; set; }


    }

    public class ProductDetails
    {
        public long product_id { get; set; }
        public string product_name { get; set; }

        public decimal product_price { get; set; }
        public string product_small_description { get; set; }
        public string product_full_desc { get; set; }
        public string product_service_desc { get; set; }
        public string product_brand_name { get; set; }

        public string product_category_name { get; set; }
        public int product_brand_id { get; set; }
        public int product_category_id { get; set; }

        public decimal product_min_price { get; set; }

    }

    #endregion

    #region Product Search
    public class ProductsearchInput
    {
        public string product_name { get; set; }
    }

    public class ProductsearchOutput
    {
        public string ResponseCode { get; set; }
        public string Responsedetails { get; set; }
        public int totalcount { get; set; }
        public List<ProductDetails> product_details { get; set; }
    }

    #endregion

    #region Brand List
    public class Brandlistoutput
    {
        public string ResponseCode { get; set; }
        public string Responsedetails { get; set; }
        public int totalcount { get; set; }
        public List<Brands> brand_details { get; set; }
    }

    public class Brands
    {
        public int brand_id { get; set; }
        public string brand_name { get; set; }
        public string brand_details { get; set; }

    }


    #endregion

    #region Category List
    public class Categorylistoutput
    {
        public string ResponseCode { get; set; }
        public string Responsedetails { get; set; }
        public int totalcount { get; set; }
        public List<CategoryClass> category_details { get; set; }
    }

    public class CategoryClass
    {
        public int category_id { get; set; }
        public string category_name { get; set; }


    }


    #endregion

    #region Basket Add

    public class BasketInputParameters
    {

        public string product_id { get; set; }
        public string product_price { get; set; }
        public string customer_id { get; set; }
        public string quantity { get; set; }
        public string salesman_id { get; set; }

        public string Token { get; set; }
        public string discount_percent { get; set; }

    }
    public class BasketOutputParameters
    {
        public string ResponseCode { get; set; }
        public string Responsedetails { get; set; }




    }


    #endregion

    #region  Sale on Progress

    public class saleProgressOutputpara
    {
        public string ResponseCode { get; set; }
        public string Responsedetails { get; set; }
        public int totalcount { get; set; }
        public List<Customer> sale_on_progress_list { get; set; }

    }

    public class Customer
    {
        public string customer_id { get; set; }

        public string mobile_no { get; set; }

        public string alternate_mobile_no { get; set; }
        public string email { get; set; }

        public string pan_number { get; set; }

        public string aadhar_no { get; set; }
        public string cust_name { get; set; }

        public int gender { get; set; }

        public string date_of_birth { get; set; }

        public string block_no { get; set; }
        public string street_no { get; set; }
        public string flat_no { get; set; }
        public string floor { get; set; }
        public string landmark { get; set; }
        public int country { get; set; }
        public int state { get; set; }

        public int city { get; set; }
        public string pin_code { get; set; }

        public bool has_basket { get; set; }

        public string customer_doj { get; set; }
        public long temp_unique_id { get; set; }
    }

    public class saleProgressSearchInputpara
    {
        public string Token { get; set; }
        public string user_id { get; set; }
        public string customer_name { get; set; }
        public string pageno { get; set; }
        public string rowcount { get; set; }


    }

    #endregion

    #region  Customer basket Show

    public class Customerbasketviewoutput
    {
        public string ResponseCode { get; set; }
        public string Responsedetails { get; set; }

        public long RequestId { get; set; }
        public List<productbasket> customer_basket_details { get; set; }


    }
    public class productbasket
    {

        public int product_id { get; set; }
        public string product_name { get; set; }
        public decimal product_price { get; set; }
        public string product_small_description { get; set; }
        public decimal product_quantity { get; set; }
        public decimal total_price { get; set; }
        public bool has_disc_applied { get; set; }
        public decimal price_after_discount { get; set; }
        public decimal discount_percent { get; set; }
    }

    #endregion

    #region Basket Delete customerwise


    public class Basketdeleteinputparameter
    {
        public string Token { get; set; }
        public string customer_id { get; set; }

        public string product_id { get; set; }
        public string sales_man_id { get; set; }

    }

    public class Basketdeleteoutputparameter
    {
        public string ResponseCode { get; set; }
        public string Responsedetails { get; set; }



    }

    #endregion

    #region Customer  Delete


    public class Customerdeleteinputparameter
    {
        public string Token { get; set; }
        public string user_id { get; set; }
        public string customer_id { get; set; }
        public string temp_unique_id { get; set; }

        public string[] temp_unique_id_list { get; set; }
    }

    public class Customerdeleteoutputparameter
    {
        public string ResponseCode { get; set; }
        public string Responsedetails { get; set; }
    }

    #endregion

    #region FinancerList
    public class FinacOutput
    {
        public string ResponseCode { get; set; }
        public string Responsedetails { get; set; }
        public List<finace> financer_details { get; set; }

    }

    public class finace
    {

        public string financer_name { get; set; }
        public int financer_id { get; set; }
    }
    #endregion

    #region Sales on progress - View customer basket- Send Discount Request

    public class SenddiscountSalesonprogressInput
    {

        public string customer_id { get; set; }
        public string user_id { get; set; }
        public string request_type { get; set; }
        public string payment_type { get; set; }

        public string exchange_amount { get; set; }
        public string financer_id { get; set; }
        public string Token { get; set; }
        public string RequestId { get; set; }
        public List<Productsrequestdetails> request_details { get; set; }


    }

    public class Productsrequestdetails
    {
        public string product_id { get; set; }
        public string discount_percentage { get; set; }
        public string product_quantity { get; set; }
        public bool Salesman_Isapplied { get; set; }
        public string discount_amount { get; set; }
        public string final_discount_price { get; set; }

    }

    public class SenddiscountSalesonprogressOuput
    {

        public string ResponseCode { get; set; }
        public string Responsedetails { get; set; }
        public int temp_request_id { get; set; }



    }

    #endregion

    #region  Billing Request with Address

    public class BillingDetailscustomerInput
    {
        public string temp_request_id { get; set; }
        public string delivery_option_type { get; set; }

        public string is_delivery_address_same { get; set; }
        public string block_no { get; set; }
        public string street_no { get; set; }
        public string flat_no { get; set; }
        public string floor { get; set; }
        public string landmark { get; set; }
        public string country { get; set; }

        public string state { get; set; }

        public string city { get; set; }

        public string pin_code { get; set; }

        public string Token { get; set; }
        public string delivery_date { get; set; }

        public string gstin { get; set; }

    }
    #endregion

    #region View Customer Approval

    public class CustomerlistforapprovalInput
    {

        public string user_id { get; set; }
        public string Token { get; set; }
        public string customer_name { get; set; }
        public string discount_requested_status { get; set; }
        public int pageno { get; set; }
        public int rowcount { get; set; }

    }

    public class CustomerlistforapprovalOutput
    {
        public string ResponseCode { get; set; }
        public string Responsedetails { get; set; }
        public List<Customerapprove> customer_approval_details { get; set; }
    }
    public class Customerapprove
    {
        public string customer_id { get; set; }
        public string customer_name { get; set; }
        public string customer_doj { get; set; }



        public string mobile_no { get; set; }

        public string alternate_mobile_no { get; set; }
        public string email { get; set; }

        public string pan_number { get; set; }

        public string aadhar_no { get; set; }


        public int gender { get; set; }

        public string date_of_birth { get; set; }

        public string block_no { get; set; }
        public string street_no { get; set; }
        public string flat_no { get; set; }
        public string floor { get; set; }
        public string landmark { get; set; }
        public int country { get; set; }
        public int state { get; set; }

        public int city { get; set; }
        public string pin_code { get; set; }

        public long RequestId { get; set; }

        public string DiscountApprovedStatus { get; set; }

    }
    #endregion

    #region View Discount Approval

    public class Salesmandiscountapproval
    {
        public string ResponseCode { get; set; }
        public string Responsedetails { get; set; }
        public long RequestId { get; set; }
        public decimal exchange_amount { get; set; }
        public string payment_type { get; set; }
        public List<productfordiscountsalesman> view_approval_details { get; set; }


    }

    public class productfordiscountsalesman
    {
        public int product_id { get; set; }
        public string product_name { get; set; }
        public decimal product_price { get; set; }
        public string product_small_description { get; set; }
        public decimal product_quantity { get; set; }
        public decimal total_price { get; set; }
        public decimal request_for_discount { get; set; }
        public decimal approved_discount { get; set; }
        public decimal price_after_discount { get; set; }
        public bool Salesman_Isapplied { get; set; }

    }

    #endregion

    #region View Discount Approval- Send Request
    public class ViewsendRequestCustomer
    {
        public string customer_id { get; set; }
        public string user_id { get; set; }
        public string request_type { get; set; }
        public string exchange_amount { get; set; }
        public string payment_type { get; set; }
        public string financer_id { get; set; }
        public string[] product_ids { get; set; }
        public string Token { get; set; }
        public string RequestId { get; set; }
    }
    public class SDiscountApprovalSendRequestOutput
    {
        public string ResponseCode { get; set; }
        public string Responsedetails { get; set; }
        public string temp_request_id { get; set; }
    }
    #endregion

    #region Sales Manager Discount Request View

    public class ViewDiscountRequestInput
    {
        public string user_id { get; set; }
        public string request_status { get; set; }
        public string Isappliedtop { get; set; }
        public string user_type { get; set; }
        public string Token { get; set; }
    }
    public class ViewDiscountRequestOutput
    {
        public string ResponseCode { get; set; }
        public string Responsedetails { get; set; }

        public List<DiscountrequestList> disc_request_list { get; set; }
    }

    public class DiscountrequestList
    {
        public string customer_id { get; set; }
        public string customer_name { get; set; }
        public string salesman_name { get; set; }
        public string salesman_id { get; set; }
        public string discount_requested_date { get; set; }
        public string discount_requested_status { get; set; }
        public long Requestid { get; set; }
        public bool is_approved_by_top { get; set; }

    }
    #endregion

    #region Sales Manager Discount Request(s) - details

    public class SalesmandiscountRequestProductInput
    {
        public string user_id { get; set; }
        public string customer_id { get; set; }
        public string salesman_id { get; set; }
        public string Token { get; set; }

        public string Requestid { get; set; }
    }
    public class SalesmandiscountRequestProductOutput
    {
        public string ResponseCode { get; set; }
        public string Responsedetails { get; set; }
        public long Requestid { get; set; }
        public decimal exchange_amount { get; set; }
        public List<Discountsalesman> discount_request_details { get; set; }

    }

    public class Discountsalesman
    {
        public int product_id { get; set; }
        public string product_name { get; set; }

        public decimal product_price { get; set; }

        public decimal product_discount_percent { get; set; }

        public string product_small_description { get; set; }

        public decimal product_quantity { get; set; }
        public decimal total_price { get; set; }
        public bool Salesman_Isapplied { get; set; }

        public decimal discount_amount { get; set; }

        public decimal min_discount_applied { get; set; }

    }
    #endregion

    #region Sales  Manager Discount Request(s) - Approve / Reject

    public class DiscountApprovalInput
    {
        public string customer_id { get; set; }

        public string request_type { get; set; }
        public string user_id { get; set; }
        public string Token { get; set; }
        public string sales_man_id { get; set; }
        public string Requestid { get; set; }

        public string Isappliedtop { get; set; }
        public List<Productsrequestdetails> request_details { get; set; }
    }
    #endregion

    #region Finance Request List

    public class FinanceRequestOutput
    {
        public string ResponseCode { get; set; }
        public string Responsedetails { get; set; }
        public List<FinanceRequest> finance_request_list { get; set; }
        public int total_count { get; set; }
    }

    public class FinanceRequest
    {
        public long finance_req_id { get; set; }
        public string customer_id { get; set; }
        public string customer_name { get; set; }

        public int salesman_id { get; set; }
        public string salesman_name { get; set; }
        public string finance_requested_date { get; set; }
        public string finance_requested_status { get; set; }
        public string finance_approval_no { get; set; }
        public string total_amount { get; set; }
        public string loan_amount { get; set; }
        public string processing_fee { get; set; }
        public string other_charges { get; set; }
        public string finance_scheme { get; set; }
        public string dbd_no { get; set; }
        public string downpayment { get; set; }
        public string no_of_emi { get; set; }
        public string emi_amount { get; set; }

    }
    #endregion

    #region View Sales Man

    public class SalesmanagersalesmanRequestOutput
    {
        public string ResponseCode { get; set; }
        public string Responsedetails { get; set; }
        public List<SalesmanListManger> salesman_list { get; set; }
        public int total_count { get; set; }
    }

    public class SalesmanListManger
    {
        public string salesman_name { get; set; }
        public string salesman_id { get; set; }
        public string salesman_mobile_no { get; set; }
        public string salesman_branch_name { get; set; }



    }
    #endregion

    #region  Finance Request Details(Salesman and Financer)

    public class FinanceRequestDetailssalesfinanceOutput
    {
        public string ResponseCode { get; set; }
        public string Responsedetails { get; set; }
        public string exchange_amount { get; set; }
        public List<productbasketforFinanceRequest> view_finance_details { get; set; }


    }
    public class productbasketforFinanceRequest
    {
        public int product_id { get; set; }
        public string product_name { get; set; }
        public decimal product_price { get; set; }
        public string product_small_description { get; set; }
        public decimal product_quantity { get; set; }
        public decimal total_price { get; set; }
        public decimal request_for_discount { get; set; }
        public decimal approved_discount { get; set; }
        public decimal price_after_discount { get; set; }
        public bool Salesman_Isapplied { get; set; }



    }


    #endregion

    #region Finance Request Details accept By Financer

    public class FinancedetailsAcceptInput
    {
        public string finance_userid { get; set; }
        public string finance_req_id { get; set; }
        public string total_amount { get; set; }
        public string finance_approval_no { get; set; }
        public string Token { get; set; }
        public string loan_amount { get; set; }
        public string processing_fee { get; set; }
        public string other_charges { get; set; }
        public string finance_scheme { get; set; }
        public string dbd_no { get; set; }
        public string downpayment { get; set; }
        public string no_of_emi { get; set; }
        public string emi_amount { get; set; }
        public string reason { get; set; }
    }

    #endregion

    #region View Finance Status Customer

    public class ViewFinanceStatusCustomerOutput
    {
        public string ResponseCode { get; set; }
        public string Responsedetails { get; set; }
        public List<FinanceRequestCustomer> finance_status_details { get; set; }
    }
    public class FinanceRequestCustomer
    {
        public long finance_req_id { get; set; }
        public string customer_id { get; set; }
        public string customer_name { get; set; }
        public string customer_mobile_no { get; set; }
        public string salesman_id { get; set; }
        public string finance_requested_date { get; set; }
        public string finance_requested_status { get; set; }

        public List<FinanceRequestFinance> financer_list { get; set; }
    }
    public class FinanceRequestFinance
    {
        public string financer_name { get; set; }
        public string financer_id { get; set; }
        public string rejected_reason { get; set; }
        public string finance_status { get; set; }
        public string finance_status_date { get; set; }



        public bool Is_financer_to_apply { get; set; }

    }


    #endregion

    #region  View Finance Status (Apply Again)
    public class Viewfinancestatusinput
    {
        public string user_id { get; set; }
        public string customer_id { get; set; }
        public string finance_request_id { get; set; }
        public string financer_id { get; set; }
        public string Token { get; set; }
    }



    #endregion

    #region Notification Sales man
    public class ViewNotificationsales
    {
        public string ResponseCode { get; set; }
        public string Responsedetails { get; set; }
        public List<ViewNotificationsalesdiscount> notification_details { get; set; }
    }
    public class ViewNotificationsalesdiscount
    {
        public string customer_id { get; set; }
        public string customer_name { get; set; }
        public string discount_requested_date { get; set; }
        public string discount_requested_status { get; set; }
        public string notification_read_status { get; set; }
        public long notification_id { get; set; }

        public string request_type { get; set; }
        public string mobile_no { get; set; }
    }
    #endregion


    #region Notification Sales  Manager
    public class ViewNotificationsalesmanager
    {
        public string ResponseCode { get; set; }
        public string Responsedetails { get; set; }
        public List<ViewNotificationsalesmanagerdiscount> notification_details { get; set; }
    }
    public class ViewNotificationsalesmanagerdiscount
    {
        public string customer_id { get; set; }
        public string customer_name { get; set; }
        public string discount_requested_date { get; set; }
        public string discount_requested_status { get; set; }
        public string notification_read_status { get; set; }
        public long notification_id { get; set; }

        public int salesman_id { get; set; }
        public string Salesman_name { get; set; }
    }
    #endregion


    #region Notification Financer
    public class ViewNotificationfinancerOutput
    {
        public string ResponseCode { get; set; }
        public string Responsedetails { get; set; }
        public List<ViewNotificationfinance> notification_list { get; set; }
    }
    public class ViewNotificationfinance
    {
        public string customer_id { get; set; }
        public string customer_name { get; set; }
        public long finance_req_id { get; set; }
        public int salesman_id { get; set; }
        public string salesman_name { get; set; }
        public string finance_requested_date { get; set; }
        public string finance_requested_status { get; set; }
        public string finance_approval_no { get; set; }
        public decimal total_amount { get; set; }
        public decimal loan_amount { get; set; }
        public decimal processing_fee { get; set; }
        public decimal other_charges { get; set; }
        public string finance_scheme { get; set; }
        public string dbd_no { get; set; }
        public decimal downpayment { get; set; }
        public decimal emi_amount { get; set; }
        public string notification_read_status { get; set; }
    }
    #endregion

    #region Pushnotification
    public class AndroidFCMPushNotificationStatus
    {
        public bool Successful
        {
            get;
            set;
        }

        public string Response
        {
            get;
            set;
        }
        public Exception Error
        {
            get;
            set;
        }
    }
    #endregion

    #region Login Logout
    public class UserLoginInputParameters
    {
        public string user_name { get; set; }
        public string password { get; set; }

        public string device_id { get; set; }

        public string device_type { get; set; }
        public string Token { get; set; }

        public string Imei_no { get; set; }
    }

    public class UserLoginOutputParameters
    {
        public string ResponseCode { get; set; }
        public string Responsedetails { get; set; }

        public string userId { get; set; }
        public string user_type { get; set; }
        public int? user_country_id { get; set; }
        public int? user_state_id { get; set; }

        public int? user_city_id { get; set; }

        public string notification_count { get; set; }

        public int User_login_Id { get; set; }

        public string session_token { get; set; }
        public string Logintype { get; set; }
        public string full_name { get; set; }
    }

    #endregion



    #region IMEI
    public class IMEIClass
    {
        public string ResponseCode { get; set; }
        public string Responsedetails { get; set; }

        public string isPresent { get; set; }
        public string userids { get; set; }
    }
    #endregion


    #region Company Logo
    public class CompanyLogoclass
    {
        public string ResponseCode { get; set; }
        public string Responsedetails { get; set; }
        public string logo_url { get; set; }
    }
    #endregion
    public class ErrorModel
    {
        public string ResponseCode { get; set; }
        public string Responsedetails { get; set; }
    }

    public class UserLogOutputParameters
    {
        public string ResponseCode { get; set; }
        public string Responsedetails { get; set; }



    }
    public class Commonclass
    {
        public string ResponseCode { get; set; }
        public string Responsedetails { get; set; }
    }



    #region HelperMethod
    public class APIHelperMethods
    {

        public static T ToModel<T>(DataTable dt)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    foreach (PropertyInfo pro in temp.GetProperties())
                    {
                        if (pro.Name == column.ColumnName && dt.Rows[0][column.ColumnName] != DBNull.Value)
                        {
                            try
                            {
                                pro.SetValue(obj, dt.Rows[0][column.ColumnName], null);
                            }
                            catch
                            {

                            }
                        }
                    }
                }
            }

            return obj;
        }

        public static List<T> ToModelList<T>(DataTable dt)
        {
            Type temp = typeof(T);

            List<T> objList = new List<T>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    T obj = Activator.CreateInstance<T>();

                    foreach (DataColumn column in row.Table.Columns)
                    {
                        foreach (PropertyInfo pro in temp.GetProperties())
                        {
                            if (pro.Name == column.ColumnName && row[column.ColumnName] != DBNull.Value)
                            {
                                try
                                {
                                    pro.SetValue(obj, row[column.ColumnName], null);
                                }
                                catch
                                {

                                }
                            }
                        }
                    }

                    objList.Add(obj);
                }
            }

            return objList;
        }


        public static string ConvertToXml<T>(List<T> table, int metaIndex = 0)
        {
            XmlDocument ChoiceXML = new XmlDocument();
            ChoiceXML.AppendChild(ChoiceXML.CreateElement("root"));
            Type temp = typeof(T);

            foreach (var item in table)
            {
                XmlElement element = ChoiceXML.CreateElement("data");

                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    element.AppendChild(ChoiceXML.CreateElement(pro.Name)).InnerText = Convert.ToString(item.GetType().GetProperty(pro.Name).GetValue(item, null));
                }
                ChoiceXML.DocumentElement.AppendChild(element);
            }

            return ChoiceXML.InnerXml.ToString();
        }

    }
    #endregion


    #region Encryption
    public class Encryption
    {
        #region Properties

        private string Password = "3269875";
        private string Salt = "05983654";
        private string HashAlgorithm = "SHA1";
        private int PasswordIterations = 2;
        private string InitialVector = "OFRna73m*aze01xY";
        private int KeySize = 256;

        public string password
        {
            get { return Password; }
        }

        public string salt
        {
            get { return Salt; }
        }

        public string hashAlgo
        {
            get { return HashAlgorithm; }
        }

        public int passwordterations
        {
            get { return PasswordIterations; }
        }

        public string initialvector
        {
            get { return InitialVector; }
        }

        public int keysize
        {
            get { return KeySize; }
        }

        #endregion Properties

        #region Encrypt region

        public string Encrypt(string PlainText)
        {
            if (string.IsNullOrEmpty(PlainText))
                return "";
            byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(initialvector);
            byte[] SaltValueBytes = Encoding.ASCII.GetBytes(salt);
            byte[] PlainTextBytes = Encoding.UTF8.GetBytes(PlainText);
            PasswordDeriveBytes DerivedPassword = new PasswordDeriveBytes(password, SaltValueBytes, hashAlgo, passwordterations);
            byte[] KeyBytes = DerivedPassword.GetBytes(KeySize / 8);
            RijndaelManaged SymmetricKey = new RijndaelManaged();
            SymmetricKey.Mode = CipherMode.CBC;
            byte[] CipherTextBytes = null;
            using (ICryptoTransform Encryptor = SymmetricKey.CreateEncryptor(KeyBytes, InitialVectorBytes))
            {
                using (MemoryStream MemStream = new MemoryStream())
                {
                    using (CryptoStream CryptoStream = new CryptoStream(MemStream, Encryptor, CryptoStreamMode.Write))
                    {
                        CryptoStream.Write(PlainTextBytes, 0, PlainTextBytes.Length);
                        CryptoStream.FlushFinalBlock();
                        CipherTextBytes = MemStream.ToArray();
                        MemStream.Close();
                        CryptoStream.Close();
                    }
                }
            }
            SymmetricKey.Clear();
            return Convert.ToBase64String(CipherTextBytes);
        }

        #endregion Encrypt region

        #region Decrypt Region

        public string Decrypt(string CipherText)
        {
            try
            {
                if (string.IsNullOrEmpty(CipherText))
                    return "";
                byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(initialvector);
                byte[] SaltValueBytes = Encoding.ASCII.GetBytes(salt);
                byte[] CipherTextBytes = Convert.FromBase64String(CipherText);
                PasswordDeriveBytes DerivedPassword = new PasswordDeriveBytes(password, SaltValueBytes, hashAlgo, passwordterations);
                byte[] KeyBytes = DerivedPassword.GetBytes(KeySize / 8);
                RijndaelManaged SymmetricKey = new RijndaelManaged();
                SymmetricKey.Mode = CipherMode.CBC;
                byte[] PlainTextBytes = new byte[CipherTextBytes.Length];
                int ByteCount = 0;
                using (ICryptoTransform Decryptor = SymmetricKey.CreateDecryptor(KeyBytes, InitialVectorBytes))
                {
                    using (MemoryStream MemStream = new MemoryStream(CipherTextBytes))
                    {
                        using (CryptoStream CryptoStream = new CryptoStream(MemStream, Decryptor, CryptoStreamMode.Read))
                        {
                            ByteCount = CryptoStream.Read(PlainTextBytes, 0, PlainTextBytes.Length);
                            MemStream.Close();
                            CryptoStream.Close();
                        }
                    }
                }
                SymmetricKey.Clear();
                return Encoding.UTF8.GetString(PlainTextBytes, 0, ByteCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }

    #endregion

    #region My Sales
    public class MysalesReportOutput
    {
        public string ResponseCode { get; set; }
        public string Responsedetails { get; set; }
        public Int32 total_sales { get; set; }
        public List<MysalesReportCustomerOutput> customer_approval_details { get; set; }
    }
    public class MysalesReportCustomerOutput
    {
        public string customer_id { get; set; }

        public string mobile_no { get; set; }

        public string alternate_mobile_no { get; set; }
        public string email { get; set; }

        public string pan_number { get; set; }

        public string aadhar_no { get; set; }
        public string cust_name { get; set; }

        public int gender { get; set; }

        public string customer_doj { get; set; }


        public int country { get; set; }
        public int state { get; set; }

        public int city { get; set; }
        public string pin_code { get; set; }


        public long RequestId { get; set; }
    }
    #endregion


    #region Change Password


    public class ChangePasswordInput
    {
        public string User_id { get; set; }
        public string Old_password { get; set; }
        public string New_password { get; set; }
        public string Token { get; set; }
    }


    #endregion
}
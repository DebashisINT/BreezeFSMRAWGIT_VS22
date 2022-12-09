using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class RubyFoodProspectListInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
    }

    public class RubyFoodProspectListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<ProspectList> Prospect_list { get; set; }
    }

    public class ProspectList
    {
        public string pros_id { get; set; }
        public string pros_name { get; set; }
    }

    public class RubyFoodQuestionListInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
    }

    public class RubyFoodQuestionListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<QuestionList> Question_list { get; set; }
    }

    public class QuestionList
    {
        public string question_id { get; set; }
        public string question { get; set; }
    }

    public class RubyFoodQuestionAnswerListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<QuestionAnswerList> Question_list { get; set; }
    }

    public class QuestionAnswerList
    {
        public string shop_id { get; set; }
        public string question_id { get; set; }
        public bool answer { get; set; }
        public bool isUploaded { get; set; }
    }

    public class RubyFoodQuestionListSaveInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string shop_id { get; set; }
        public List<QuestionListInput> Question_list { get; set; }
    }

    public class QuestionListInput
    {
        public string question_id { get; set; }
        public bool answer { get; set; }
    }

    public class RubyFoodQuestionListSaveOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class RubyFoodImageLinkInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
    }

    public class RubyFoodImageLinkOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string user_id { get; set; }
        public List<LeadShopImageList> lead_shop_list { get; set; }
    }

    public class LeadShopImageList
    {
        public string lead_shop_id { get; set; }
        public string rubylead_image1 { get; set; }
        public string rubylead_image2 { get; set; }
    }

    public class RubyFoodOrderReturnInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public decimal return_amount { get; set; }
        public string shop_id { get; set; }
        public string return_id { get; set; }
        public string description { get; set; }
        public string return_date_time { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string address { get; set; }
        public List<OrderReturnList> return_list { get; set; }
    }

    public class OrderReturnList
    {
        public long id { get; set; }
        public string product_name { get; set; }
        [Required]
        public decimal qty { get; set; }
        public decimal rate { get; set; }
        public decimal total_price { get; set; }
    }

    public class RubyFoodOrderReturnOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        //public List<OrderReturnOutDataList> data { get; set; }
    }

    public class OrderReturnOutDataList
    {
        public string status { get; set; }
        public string message { get; set; }
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string return_id { get; set; }
        public decimal return_amount { get; set; }
        public string description { get; set; }
    }

    public class OrderReturnFetchInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string date { get; set; }
    }

    public class OrderReturnFetchOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string total_returnlist_count { get; set; }
        public List<OrderReturnFetchdetailsList> return_list { get; set; }
    }

    public class OrderReturnFetchdetailsList
    {
        public string shop_id { get; set; }
        public string shop_address { get; set; }
        public string shop_name { get; set; }
        public string shop_contact_no { get; set; }
        public string pin_code { get; set; }
        public string shop_lat { get; set; }
        public string shop_long { get; set; }
        public string return_lat { get; set; }
        public string return_long { get; set; }
        public string return_id { get; set; }
        public string return_date_time { get; set; }
        public decimal return_amount { get; set; }
        public List<OrderReturnProducts> product_list { get; set; }
    }

    public class OrderReturnProducts
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
}
#region======================================Revision History=========================================================
//Written By : Debashis Talukder On 31/05/2024
//Purpose: For CRM Opportunity Details.Row: 932 to 936
#endregion===================================End of Revision History==================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class CRMOpportunityStatusInput
    {
        public string session_token { get; set; }
    }
    public class CRMOpportunityStatusOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<StatusList> status_list { get; set; }
    }

    public class StatusList
    {
        public long status_id { get; set; }
        public string status_name { get; set; }
    }

    public class OpportunityDetailSaveInput
    {
        public long user_id { get; set; }
        public string session_token { get; set; }
        public string shop_id { get; set; }
        public string shop_name { get; set; }
        public int shop_type { get; set; }
        public string opportunity_id { get; set; }
        public string opportunity_description { get; set; }
        public decimal opportunity_amount { get; set; }
        public long opportunity_status_id { get; set; }
        public string opportunity_status_name { get; set; }
        public string opportunity_created_date { get; set; }
        public string opportunity_created_time { get; set; }
        public string opportunity_created_date_time { get; set; }        
        public List<OpportunityProductLists> opportunity_product_list { get; set; }
    }

    public class OpportunityProductLists
    {
        public string opportunity_id { get; set; }
        public string shop_id { get; set; }
        public long product_id { get; set; }
        public string product_name { get; set; }
    }

    public class OpportunityDetailSaveOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class OpportunityDetailEditInput
    {
        public long user_id { get; set; }
        public string session_token { get; set; }
        public string shop_id { get; set; }
        public string shop_name { get; set; }
        public int shop_type { get; set; }
        public string opportunity_id { get; set; }
        public string opportunity_description { get; set; }
        public decimal opportunity_amount { get; set; }
        public long opportunity_status_id { get; set; }
        public string opportunity_status_name { get; set; }
        public string opportunity_created_date { get; set; }
        public string opportunity_created_time { get; set; }
        public string opportunity_created_date_time { get; set; }
        public string opportunity_edited_date_time { get; set; }
        public List<OpportunityEditProductLists> edit_opportunity_product_list { get; set; }
    }

    public class OpportunityEditProductLists
    {
        public string opportunity_id { get; set; }
        public string shop_id { get; set; }
        public long product_id { get; set; }
        public string product_name { get; set; }
    }

    public class OpportunityDetailEditOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class OpportunityDetailDeleteInput
    {
        public long user_id { get; set; }
        public string session_token { get; set; }
        public List<OpportunityDeleteProductLists> opportunity_delete_list { get; set; }
    }

    public class OpportunityDeleteProductLists
    {
        public string opportunity_id { get; set; }
    }

    public class OpportunityDetailDeleteOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class OpportunityDetailListsInput
    {
        public string user_id { get; set; }
    }

    public class OpportunityDetailListsOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string user_id { get; set; }
        public List<OpportunitylistOutput> opportunity_list { get; set; }
    }

    public class OpportunitylistOutput
    {
        public string shop_id { get; set; }
        public string shop_name { get; set; }
        public int shop_type { get; set; }
        public string opportunity_id { get; set; }
        public string opportunity_description { get; set; }
        public decimal opportunity_amount { get; set; }
        public long opportunity_status_id { get; set; }
        public string opportunity_status_name { get; set; }
        public string opportunity_created_date { get; set; }
        public string opportunity_created_time { get; set; }
        public string opportunity_created_date_time { get; set; }
        public string opportunity_edited_date_time { get; set; }
        public List<OpportunityProductListOutput> opportunity_product_list { get; set; }
    }

    public class OpportunityProductListOutput
    {
        public string shop_id { get; set; }
        public string opportunity_id { get; set; }
        public long product_id { get; set; }
        public string product_name { get; set; }
    }
}
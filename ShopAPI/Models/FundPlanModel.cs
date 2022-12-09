using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class FundPlanModel
    {

    }

    public class FundPlanListInput
    {
        public string user_id { get; set; }
        public String session_token { get; set; }
    }

    public class FundPlanList
    {
        public string plan_id { get; set; }
        public string party_name { get; set; }
        public string contact_no { get; set; }
        public string location { get; set; }
        public string last_plan_date { get; set; }
        public string last_plan_value { get; set; }
        public string last_achv_amount { get; set; }
        public string last_plan_feedback { get; set; }
        public string last_achv_feedback { get; set; }
    }

    public class FundPlanListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<FundPlanList> update_plan_list { get; set; }
    }

    public class DetailsPlanListInput
    {
        public string user_id { get; set; }
        public string plan_id { get; set; }
        public String session_token { get; set; }
    }

    public class DetailsPlanListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<FundPlanDetailsList> plan_data_details { get; set; }
    }

    public class FundPlanDetailsList
    {
        public string details_id { get; set; }
        public string plan_date { get; set; }
        public string plan_value { get; set; }
        public string plan_remarks { get; set; }
        public string achievement_value { get; set; }
        public string achievement_date { get; set; }
        public string achievement_remarks { get; set; }
        public string percnt { get; set; }
    }

    public class updateFoundPlanInput
    {
        public String session_token { get; set; }
        public String user_id { get; set; }
        public List<UpdatePlanList> update_plan_list { get; set; }
    }

    public class ALLPlanListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<FundPlanDetailsListALL> plan_data { get; set; }
    }

    public class FundPlanDetailsListALL
    {
        public string plan_data_id { get; set; }
        public string date { get; set; }
        public string plan_date { get; set; }
        public string plan_value { get; set; }
        public string plan_remarks { get; set; }
        public string achievement_value { get; set; }
        public string achievement_date { get; set; }
        public string achievement_remarks { get; set; }
        public string percnt { get; set; }

        public string party_name { get; set; }
        public string type { get; set; }
        public string contact_no { get; set; }
        public string location { get; set; }
    }
}
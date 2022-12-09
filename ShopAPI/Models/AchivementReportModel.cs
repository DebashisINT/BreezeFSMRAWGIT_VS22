using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class AchivementReportModel
    {

    }

    public class AchivementReportInput
    {
        public String session_token { get; set; }
        public String user_id { get; set; }
        public String from_date { get; set; }
        public String to_date { get; set; }
    }

    public class AchivementReportOutput
    {
        public String status { get; set; }
        public String message { get; set; }
        public List<AchivementMemberList> achv_report_list { get; set; }
    }

    public class AchivementMemberList
    {
        public String member_name { get; set; }
        public String member_id { get; set; }
        public String stage_count { get; set; }
        public String report_to { get; set; }
        public List<AchivementDetailsList> achv_details_list { get; set; }
    }

    public class AchivementDetailsList
    {
        public String cust_name { get; set; }
        public String visit_time { get; set; }
        public String visit_date { get; set; }
        public String stage { get; set; }
    }

    public class AchivementTargetReportOutput
    {
        public String status { get; set; }
        public String message { get; set; }
        public List<AchivementTargetMemberList> targ_achv_report_list { get; set; }
    }

    public class AchivementTargetMemberList
    {
        public String member_name { get; set; }
        public String member_id { get; set; }
        public String report_to { get; set; }
        public List<AchivementTargetDetailsList> targ_achv_details_list { get; set; }
    }

    public class AchivementTargetDetailsList
    {
        public String enquiry { get; set; }
        public String lead { get; set; }
        public String test_drive { get; set; }
        public String booking { get; set; }
        public String retail { get; set; }
    }

    public class QuotationSMSMailInput
    {
        public String session_token { get; set; }
        public String user_id { get; set; }
        public String quo_id { get; set; }
        public String shop_id { get; set; }
        public bool isSms { get; set; }
    }

    public class QuotationSMSMailOutput
    {
        public String status { get; set; }
        public String message { get; set; }
    }

}
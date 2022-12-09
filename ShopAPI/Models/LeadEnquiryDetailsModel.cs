using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class LeadEnquiryWiseCustListInput
    {
        public string from_date { get; set; }
        public string to_date { get; set; }
        public string enquiry_from { get; set; }
        public string user_id { get; set; }
    }

    public class LeadEnquiryWiseCustListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string enquiry_from { get; set; }
        public string user_id { get; set; }
        public List<CustDelList> customer_dtls_list { get; set; }
    }

    public class CustDelList
    {
        public System.Guid crm_id { get; set; }
        public string customer_name { get; set; }
        public string mobile_no { get; set; }
        public string email { get; set; }
        public string customer_addr { get; set; }
        public decimal qty { get; set; }
        public string UOM { get; set; }
        public decimal order_value { get; set; }
        public string enquiry_details { get; set; }
        public string product_req { get; set; }
        public string contact_person { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string source_vend_type { get; set; }
        public string status { get; set; }
    }

    public class SaveActivityInput
    {
        public string crm_id { get; set; }
        public string activity_date { get; set; }
        public string activity_time { get; set; }
        public string activity_type_name { get; set; }
        public string activity_status { get; set; }
        public string activity_details { get; set; }
        public string other_remarks { get; set; }
        //Rev Debashis Row: 676
        public string activity_next_date { get; set; }
        //End of Rev Debashis Row: 676
    }

    public class SaveActivityOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class ShowActivityListInput
    {
        public string crm_id { get; set; }
    }

    public class ShowActivityListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string crm_id { get; set; }
        public List<ActivityDelList> activity_dtls_list { get; set; }
    }

    public class ActivityDelList
    {
        public long activity_id { get; set; }
        public string activity_date { get; set; }
        public string activity_time { get; set; }
        public string activity_status { get; set; }
        public string activity_type_name { get; set; }
        public string activity_details { get; set; }
        public string other_remarks { get; set; }
        //Rev Debashis Row: 676
        public string activity_next_date { get; set; }
        //End of Rev Debashis Row: 676
    }

    public class UpdateActivityInput
    {
        public string crm_id { get; set; }
        public long activity_id { get; set; }
        public string activity_date { get; set; }
        public string activity_time { get; set; }
        public string activity_type_name { get; set; }
        public string activity_status { get; set; }
        public string activity_details { get; set; }
        public string other_remarks { get; set; }
        //Rev Debashis Row: 676
        public string activity_next_date { get; set; }
        //End of Rev Debashis Row: 676
    }

    public class UpdateActivityOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }
}
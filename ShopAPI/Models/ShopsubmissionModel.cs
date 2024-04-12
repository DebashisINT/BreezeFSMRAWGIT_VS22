#region======================================Revision History=========================================================
//1.0   V2.0.37     Debashis    10/01/2023      Some new parameters have been added.Row: 786
//2.0   V2.0.38     Debashis    13/03/2023      A new parameter has been added.Row: 814
//3.0   V2.0.39     Debashis    24/04/2023      Some new parameters have been added.Row: 820
//4.0   V2.0.40     Debashis    10/07/2023      A new parameter has been added.Row: 855
//5.0   V2.0.45     Debashis    03/04/2024      Some new parameters have been added.Row: 915 & Refer: 0027335
#endregion===================================End of Revision History==================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class ShopsubmissionInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        //Rev Debashis : Mantis:0025529 & Row:779
        public int isnewShop { get; set; }
        //End of Rev Debashis : Mantis:0025529 & Row:779

        public List<ShopsubmissionModel> shop_list { get; set; }
    }

    public class ShopsubmissionModel
    {

        public string shop_id { get; set; }
        public string visited_date { get; set; }
        public string visited_time { get; set; }
        public string spent_duration { get; set; }
        public int total_visit_count { get; set; }
        public string distance_travelled { get; set; }
        public string user_id { get; set; }

        public string feedback { get; set; }
        public string isFirstShopVisited { get; set; }
        public string distanceFromHomeLoc { get; set; }
        public string early_revisit_reason { get; set; }

        public string device_model { get; set; }
        public string android_version { get; set; }
        public string battery { get; set; }
        public string net_status { get; set; }
        public string net_type { get; set; }

        public string in_time { get; set; }
        public string out_time { get; set; }
        public string start_timestamp { get; set; }
        public string in_location { get; set; }
        public string out_location { get; set; }
        public string shop_revisit_uniqKey { get; set; }
        //Rev Debashis
        public string pros_id { get; set; }
        public string updated_by { get; set; }
        public String updated_on { get; set; }
        public string agency_name { get; set; }
        public string approximate_1st_billing_value { get; set; }
        //End of Rev Debashis
        //Rev 1.0 Row:786
        public string multi_contact_name { get; set; }
        public string multi_contact_number { get; set; }
        //End of Rev 1.0 Row:786
        //Rev 2.0 Row:814
        public bool IsShopUpdate { get; set; }
        //End of Rev 2.0 Row:814
        //Rev 3.0 Row:820
        public decimal distFromProfileAddrKms { get; set; }
        public int stationCode { get; set; }
        //End of Rev 3.0 Row:820
        //Rev 5.0 Row: 915 & Refer: 0027335
        public string shop_lat { get; set; }
        public string shop_long { get; set; }
        public string shop_addr { get; set; }
        //End of Rev 5.0 Row: 915 & Refer: 0027335
    }

    public class ShopsubmissionOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<DatalistsSumission> shop_list { get; set; }
    }

    public class DatalistsSumission
    {

        public string shopid { get; set; }
        public int total_visit_count { get; set; }

        public DateTime? visited_date { get; set; }
        public DateTime? visited_time { get; set; }
        public string spent_duration { get; set; }
        public string distance_travelled { get; set; }
        //Rev 2.0 Row:814
        public bool IsShopUpdate { get; set; }
        //End of Rev 2.0 Row:814
    }

    public class MeetingVisitInput
    {
        public String session_token { get; set; }
        public String user_id { get; set; }
        public List<MeetingVisitList> meeting_list { get; set; }
    }

    public class MeetingVisitList
    {
        public String remarks { get; set; }
        public String latitude { get; set; }
        public String longitude { get; set; }
        public String duration { get; set; }
        public String meeting_type_id { get; set; }
        public String address { get; set; }
        public String pincode { get; set; }
        public String distance_travelled { get; set; }
        public String date { get; set; }
        public String date_time { get; set; }
    }

    public class MeetingVisitOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }


    public class OrderNotTakenInput
    {
        public String session_token { get; set; }
        public String user_id { get; set; }
        public List<OrderNotTakenList> ordernottaken_list { get; set; }
    }

    public class OrderNotTakenList
    {
        public String shop_id { get; set; }
        public String order_status { get; set; }
        public String order_remarks { get; set; }
        public String shop_revisit_uniqKey { get; set; }
    }

    //Rev Debashis Row:748
    public class ITCShopsubmissionInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public List<ITCShopsubmissionModel> shop_list { get; set; }
    }

    public class ITCShopsubmissionModel
    {
        public string shop_id { get; set; }
        public string visited_date { get; set; }
        public string visited_time { get; set; }
        public string spent_duration { get; set; }
        public int total_visit_count { get; set; }
        public string distance_travelled { get; set; }
        public string user_id { get; set; }
        public string feedback { get; set; }
        public string isFirstShopVisited { get; set; }
        public string distanceFromHomeLoc { get; set; }
        public string early_revisit_reason { get; set; }
        public string device_model { get; set; }
        public string android_version { get; set; }
        public string battery { get; set; }
        public string net_status { get; set; }
        public string net_type { get; set; }
        public string in_time { get; set; }
        public string out_time { get; set; }
        public string start_timestamp { get; set; }
        public string in_location { get; set; }
        public string out_location { get; set; }
        public string shop_revisit_uniqKey { get; set; }
        public string pros_id { get; set; }
        public string updated_by { get; set; }
        public String updated_on { get; set; }
        public string agency_name { get; set; }
        public string approximate_1st_billing_value { get; set; }
        public bool IsShopUpdate { get; set; }
        //Rev 4.0 Row: 855
        public bool isNewShop { get; set; }
        //End of Rev 4.0 Row: 855
        //Rev 5.0 Row: 915
        public string shop_lat { get; set; }
        public string shop_long { get; set; }
        public string shop_addr { get; set; }
        //End of Rev 5.0 Row: 915
    }

    public class ITCShopsubmissionOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<ITCDatalistsSumission> shop_list { get; set; }
    }

    public class ITCDatalistsSumission
    {
        public string shopid { get; set; }
        public int total_visit_count { get; set; }
        public DateTime? visited_date { get; set; }
        public DateTime? visited_time { get; set; }
        public string spent_duration { get; set; }
        public string distance_travelled { get; set; }
        public bool IsShopUpdate { get; set; }
    }
    //End of Rev Debashis Row:748
}
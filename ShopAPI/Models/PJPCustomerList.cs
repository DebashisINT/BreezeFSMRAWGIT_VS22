using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class PJPOutPut
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class PJPCustomer
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<PJPCustomerList> cust_list { get; set; }
    }

    public class PJPCustomerList
    {
        public string cust_id { get; set; }
        public string cust_name { get; set; }
    }

    public class PJPCustomerInput
    {
        public string session_token { get; set; }
        public String user_id { get; set; }
        public String creater_user_id { get; set; }
    }

    public class PJPInsertInput
    {
        public string session_token { get; set; }
        public String user_id { get; set; }
        public String creater_user_id { get; set; }
        public String date { get; set; }
        public string from_time { get; set; }
        public string to_time { get; set; }
        public string cust_id { get; set; }
        public string location { get; set; }
        public string remarks { get; set; }
        //Extra input
        public string pjp_lat { get; set; }
        public string pjp_long { get; set; }
        public string pjp_radius { get; set; }
    }

    public class PJPEditInput
    {
        public String session_token { get; set; }
        public String user_id { get; set; }
        public String creater_user_id { get; set; }
        public String date { get; set; }
        public String from_time { get; set; }
        public String to_time { get; set; }
        public String cust_id { get; set; }
        public String location { get; set; }
        public String remarks { get; set; }
        public String PJP_id { get; set; }

        //Extra input
        public string pjp_lat { get; set; }
        public string pjp_long { get; set; }
        public string pjp_radius { get; set; }
    }

    public class PJPDeleetInput
    {
        public String session_token { get; set; }
        public String user_id { get; set; }
        public String creater_user_id { get; set; }
        public String PJP_id { get; set; }
    }

    public class PJPDetailsListInput
    {
        public String session_token { get; set; }
        public String user_id { get; set; }
        public String creater_user_id { get; set; }
        public String year { get; set; }
        public String month { get; set; }
    }

    public class PJPDetailsOutupt
    {
        public String status { get; set; }
        public String message { get; set; }
        public String supervisor_name { get; set; }
        public List<PJPDetailsList> pjp_list { get; set; }
    }

    public class PJPDetailsList
    {
        public String id { get; set; }
        public String from_time { get; set; }
        public String to_time { get; set; }
        public String customer_name { get; set; }
        public String customer_id { get; set; }
        public String location { get; set; }
        public String date { get; set; }
        public bool isUpdateable { get; set; }
        public String remarks { get; set; }
        public String user_id { get; set; }
        public String creater_user_id { get; set; }

        //Extra input
        public string pjp_lat { get; set; }
        public string pjp_long { get; set; }
        public string pjp_radius { get; set; }
    }

    public class PJPConfigInput
    {
        public String session_token { get; set; }
        public String user_id { get; set; }
        public String creater_user_id { get; set; }
    }

    public class PJPConfigOutPut
    {
        public String status { get; set; }
        public String message { get; set; }
        public String pjp_past_days { get; set; }
        public String supervisor_name { get; set; }
    }

    public class TeamLocationInput
    {
        public String session_token { get; set; }
        public String user_id { get; set; }
        public String creater_user_id { get; set; }
        public String date { get; set; }
    }

    public class TeamLocation
    {
        public String status { get; set; }
        public String message { get; set; }
        public String total_distance { get; set; }
        public String total_visit_distance { get; set; }
        public List<TeamLocationList> location_details { get; set; }
    }

    public class TeamLocationList
    {
        public String id { get; set; }
        public String location_name { get; set; }
        public String latitude { get; set; }
        public String longitude { get; set; }
        public String distance_covered { get; set; }
        public String last_update_time { get; set; }
        public String shops_covered { get; set; }
        public String meetings_attended { get; set; }
        public string network_status { get; set; }
        public string battery_percentage { get; set; }
    }

    public class PJPListInput
    {
        public String session_token { get; set; }
        public String user_id { get; set; }
        public String date { get; set; }
    }

    public class PJPListOutupt
    {
        public String status { get; set; }
        public String message { get; set; }
        public List<PJPList> pjp_list { get; set; }
    }

    public class PJPList
    {
        public String id { get; set; }
        public String from_time { get; set; }
        public String to_time { get; set; }
        public String customer_name { get; set; }
        public String customer_id { get; set; }
        public String location { get; set; }
        public String date { get; set; }
        public String remarks { get; set; }
    }
}
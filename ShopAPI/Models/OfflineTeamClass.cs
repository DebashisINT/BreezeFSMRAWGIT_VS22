using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class OfflineTeamClass
    {
    }
    public class AreaInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string city_id { get; set; }

    }

    public class AreaOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<Area> area_list { get; set; }

    }
    public class Area
    {
        public string area_id { get; set; }
        public string area_name { get; set; }
        public string user_id { get; set; }

    }

    public class OfflineMemberInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string date { get; set; }

    }

    public class OfflineMemberOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<OfflineMember> member_list { get; set; }

    }
    public class OfflineMember
    {
        public string user_id { get; set; }
        public string user_name { get; set; }
        public string contact_no { get; set; }
        public string super_id { get; set; }
        public string super_name { get; set; }

    }

    public class OfflineShopInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string date { get; set; }

    }

    public class OfflineShopOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<OfflineShop> shop_list { get; set; }

    }
    public class OfflineShop
    {
        public string shop_id { get; set; }
        public string shop_name { get; set; }
        public string shop_lat { get; set; }
        public string shop_long { get; set; }
        public string shop_address { get; set; }
        public string shop_pincode { get; set; }
        public string shop_contact { get; set; }
        public string total_visited { get; set; }
        public string last_visit_date { get; set; }
        public string shop_type { get; set; }
        public string dd_name { get; set; }
        public string entity_code { get; set; }
        public string model_id { get; set; }
        public string primary_app_id { get; set; }
        public string secondary_app_id { get; set; }
        public string lead_id { get; set; }
        public string funnel_stage_id { get; set; }
        public string stage_id { get; set; }
        public string booking_amount { get; set; }
        public string type_id { get; set; }
        public string area_id { get; set; }
        public string user_id { get; set; }
        public string assign_to_pp_id { get; set; }
        public string assign_to_dd_id { get; set; }
    }




}
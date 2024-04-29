#region======================================Revision History=========================================================
//1.0   V2.0.32     Debashis    17/01/2023      Some new parameters have been added.Row: 798
//2.0   V2.0.46     Debashis    24/04/2024      A new parameter has been added.Row: 925
#endregion===================================End of Revision History==================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class UserHierarchyModel
    {
    }

    public class useHierarchyinput
    {
        public String user_id { get; set; }
        public String session_token { get; set; }
        public String isFirstScreen { get; set; }
        public String isAllTeam { get; set; }

    }

    public class useHierarchyOutput
    {
        public String status { get; set; }
        public String message { get; set; }
        public List<User_list> member_list { get; set; }
    }

    public class User_list
    {
        public String user_id { get; set; }
        public String user_name { get; set; }
        public String contact_no { get; set; }
        //Rev Debashis
        public bool isLeavePending { get; set; }
        public bool isLeaveApplied { get; set; }
        public String State { get; set; }
        public String Branch { get; set; }
        public String Designation { get; set; }
        //End of Rev Debashis
        //Rev Debashis Row: 780
        public String Employee_Code { get; set; }
        //End of Rev Debashis Row: 780
        //Rev 2.0 Row: 925
        public String EMP_ContactID { get; set; }
        //End of Rev 2.0 Row: 925
    }

    public class UseHierarchyShopInput
    {
        public String user_id { get; set; }
        public String session_token { get; set; }
        public String area_id { get; set; }
    }

    public class useHierarchyShopOutput
    {
        public String status { get; set; }
        public String message { get; set; }
        public List<UserShopList> shop_list { get; set; }
    }

    public class UserShopList
    {
        public String shop_id { get; set; }
        public String shop_name { get; set; }
        public String shop_lat { get; set; }
        public String shop_long { get; set; }
        public String shop_address { get; set; }
        public String shop_pincode { get; set; }
        public String shop_contact { get; set; }
        public String total_visited { get; set; }
        public String last_visit_date { get; set; }
        public String shop_type { get; set; }
        public String dd_name { get; set; }

        public String entity_code { get; set; }

        //for Gajgarhia
        public string model_id { get; set; }
        public string primary_app_id { get; set; }
        public string secondary_app_id { get; set; }
        public string lead_id { get; set; }
        public string funnel_stage_id { get; set; }
        public string stage_id { get; set; }
        public decimal booking_amount { get; set; }

        //Rev Start Add new parameter party type id and Area Id for Mescab
        public string type_id { get; set; }
        public string area_id { get; set; }
        //Rev End Add new parameter party type id and Area Id for  Mescab
        //Rev Debashis Row:744
        public string owner_name { get; set; }
        //End of Rev Debashis Row:744
        //Rev Debashis Row:746
        public int total_visit_count { get; set; }
        //End of Rev Debashis Row:746
    }

    //Rev 1.0 Row:798
    public class TypeWiseuseHierarchyShopOutput
    {
        public String status { get; set; }
        public String message { get; set; }
        public List<TypeWiseUserShopList> shop_list { get; set; }
    }
    public class TypeWiseUserShopList
    {
        public String shop_id { get; set; }
        public String shop_name { get; set; }
        public String shop_lat { get; set; }
        public String shop_long { get; set; }
        public String shop_address { get; set; }
        public String shop_pincode { get; set; }
        public String shop_contact { get; set; }
        public String total_visited { get; set; }
        public String last_visit_date { get; set; }
        public String shop_type { get; set; }
        public String dd_name { get; set; }

        public String entity_code { get; set; }

        //for Gajgarhia
        public string model_id { get; set; }
        public string primary_app_id { get; set; }
        public string secondary_app_id { get; set; }
        public string lead_id { get; set; }
        public string funnel_stage_id { get; set; }
        public string stage_id { get; set; }
        public decimal booking_amount { get; set; }

        //Rev Start Add new parameter party type id and Area Id for Mescab
        public string type_id { get; set; }
        public string area_id { get; set; }
        //Rev End Add new parameter party type id and Area Id for  Mescab
        public string owner_email { get; set; }
        public string owner_doa { get; set; }
        public string owner_name { get; set; }
        public int total_visit_count { get; set; }
    }
    //End of Rev 1.0 Row:798

    public class UserSopTypeShopInput
    {
        public String user_id { get; set; }
        public String session_token { get; set; }
        public String shop_id { get; set; }
        public String area_id { get; set; }
    }

    //Rev Debashis
    public class UserReportToInfoInput
    {
        public string user_id { get; set; }
        public string session_token { get; set; }
    }

    public class UserReportToInfoOutput
    {
        public String status { get; set; }
        public String message { get; set; }
        public string user_id { get; set; }
        public string report_to_user_id { get; set; }
        public string report_to_user_name { get; set; }
    }
    //End of Rev Debashis
}
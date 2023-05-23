#region======================================Revision History=========================================================
//1.0   V2.0.38     Debashis    24/01/2023      A new parameter has been added.Row: 807
//2.0   V2.0.39     Debashis    17/05/2023      Two new parameters have been added.Row: 840
#endregion===================================End of Revision History==================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{

    public class AttendancemanageInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string work_type { get; set; }
        public string work_desc { get; set; }
        public string work_lat { get; set; }
        public string work_long { get; set; }
        public string work_address { get; set; }
        public DateTime? work_date_time { get; set; }
        public string is_on_leave { get; set; }
        public string add_attendence_time { get; set; }
        public string route { get; set; }
        public string leave_from_date { get; set; }
        public string leave_to_date { get; set; }
        public string leave_type { get; set; }

        public string Distributor_Name { get; set; }
        public string Market_Worked { get; set; }        
        public string order_taken { get; set; }
        public string collection_taken { get; set; }
        public string new_shop_visit { get; set; }
        public string revisit_shop { get; set; }
        public string state_id { get; set; }
        public string IsNoPlanUpdate { get; set; }
        public List<shopAttendance> shop_list { get; set; }
        public List<StatewiseTraget> primary_value_list { get; set; }
        public List<UpdatePlanList> Update_Plan_List { get; set; }

        //Rev Tanmoy add leave resion add
        public string leave_reason { get; set; }
        //Rev End Tanmoy add leave resion add

        //Rev Tanmoy add from to Area id and Distance 04-12-2020
        public string from_id { get; set; }
        public string to_id { get; set; }
        public string distance { get; set; }
        //Rev End Tanmoy add from to Area id and Distance 04-12-2020
        //Rev Debashis Row 725
        public long beat_id { get; set; }
        //End of Rev Debashis Row 725
        //Rev Debashis Row 776
        public bool IsJointVisit { get; set; }
        public string JointVisitTeam_MemberName { get; set; }
        public long JointVisitTeam_Member_User_ID { get; set; }
        //End of Rev Debashis Row 776
        //Rev 1.0 Row: 807
        public string IsDistributorwiseNearbyShopVisit { get; set; }
        //End of Rev 1.0 Row: 807
        //Rev 2.0 Row: 840
        public int visit_location_id { get; set; }
        public int area_location_id { get; set; }
        //End of Rev 2.0 Row: 840
    }


    public class StatewiseTraget
    {
        public string id { get; set; }
        public string primary_value { get; set; }

    }



    public class shopAttendance
    {
        public string route { get; set; }
        public string shop_id { get; set; }

    }
    public class AttendancemanageOutput
    {
        public string status { get; set; }
        public string message { get; set; }

    }

    public class UpdatePlanList
    {
        public string plan_id { get; set; }
        public DateTime plan_date { get; set; }
        public decimal? plan_value { get; set; }
        public string plan_remarks { get; set; }
        public decimal? achievement_value { get; set; }
       // public DateTime? achievement_date { get; set; }
        public string acheivement_remarks { get; set; }
    }

    //Rev Debashis Row 725
    public class BeatDetListInput
    {
        public string user_id { get; set; }
        public string beat_date { get; set; }
        public string session_token { get; set; }
    }

    public class BeatDetListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string beat_id { get; set; }
        public string beat_name { get; set; }
    }

    public class UpdateBeatInput
    {
        public string user_id { get; set; }
        public long updating_beat_id { get; set; }
        public string updating_date { get; set; }
    }

    public class UpdateBeatOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string updated_beat_id { get; set; }
        public string beat_name { get; set; }
    }
    //End of Rev Debashis Row 725
}
#region======================================Revision History=========================================================
//Written By : Debashis Talukder On 11/04/2024
//Purpose: For Leaderboard.Row: 905 to 908 & Refer: 0027300
//1.0   V2.0.46     Debashis    19/04/2024      API updation.Refer: 0027382
#endregion===================================End of Revision History==================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class LeaderboardOverallInput
    {
        public long user_id { get; set; }
        public string activitybased { get; set; }
        //Rev 1.0 Mantis: 0027382
        //public long branchwise { get; set; }
        public string branchwise { get; set; }
        //End of Rev 1.0 Mantis: 0027382
        public string flag { get; set; }
    }

    public class LeaderboardOverallOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<LeaderboarduserlistOutput> user_list { get; set; }
    }

    public class LeaderboarduserlistOutput
    {
        public long user_id { get; set; }
        public string user_name { get; set; }
        public string user_phone { get; set; }
        public decimal attendance { get; set; }
        public decimal new_visit { get; set; }
        public decimal revisit { get; set; }
        public decimal order { get; set; }
        public decimal activities { get; set; }
        public Int32 position { get; set; }
        public decimal totalscore { get; set; }
        public string profile_pictures_url { get; set; }
    }

    public class LeaderboardOwnInput
    {
        public long user_id { get; set; }
        public string activitybased { get; set; }
        //Rev 1.0 Mantis: 0027382
        //public long branchwise { get; set; }
        public string branchwise { get; set; }
        //End of Rev 1.0 Mantis: 0027382
        public string flag { get; set; }
    }

    public class LeaderboardOwnOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public long user_id { get; set; }
        public string user_name { get; set; }
        public string user_phone { get; set; }
        public decimal attendance { get; set; }
        public decimal new_visit { get; set; }
        public decimal revisit { get; set; }
        public decimal order { get; set; }
        public decimal activities { get; set; }
        public Int32 position { get; set; }
        public decimal totalscore { get; set; }
        public string profile_pictures_url { get; set; }
    }

    public class LeaderboardActivityListsInput
    {
        public string session_token { get; set; }
    }

    public class LeaderboardActivityListsOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<LeaderboardActivitieslists> activities_list { get; set; }
    }

    public class LeaderboardActivitieslists
    {
        public long id { get; set; }
        public string value { get; set; }
    }

    public class LeaderboardBranchListsInput
    {
        public string session_token { get; set; }
    }

    public class LeaderboardBranchListsOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<HeadBranchlistsOutput> branch_list { get; set; }
    }

    public class HeadBranchlistsOutput
    {
        public string branch_head { get; set; }
        public long branch_head_id { get; set; }
        public List<SubbranchListsOutput> sub_branch { get; set; }
    }
    public class SubbranchListsOutput
    {
        public long id { get; set; }
        public string value { get; set; }
    }
}
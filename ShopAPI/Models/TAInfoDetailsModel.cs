#region======================================Revision History=========================================================
//Written By : Debashis Talukder On 11/11/2024
//Purpose: Target Vs Achievement Info Details.Refer: Row: 992 & 993
#endregion===================================End of Revision History==================================================


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class TargetTypeListInput
    {
        public long user_id { get; set; }
    }
    public class TargetTypeListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<TAlistOutput> target_type_list { get; set; }
    }
    public class TAlistOutput
    {
        public long id { get; set; }
        public string name { get; set; }
    }

    public class TargetLevelListInput
    {
        public long user_id { get; set; }
    }
    public class TargetLevelListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<TLlistOutput> target_level_list { get; set; }
    }
    public class TLlistOutput
    {
        public long id { get; set; }
        public string name { get; set; }
    }

    public class TargetTimeFrameListInput
    {
        public long user_id { get; set; }
    }
    public class TargetTimeFrameListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<TTFlistOutput> target_time_list { get; set; }
    }
    public class TTFlistOutput
    {
        public long id { get; set; }
        public string name { get; set; }
    }

    public class FetchTargetAchievementDetailsInput
    {
        public long user_id { get; set; }
        public long target_type_id { get; set; }
        public long target_level_id { get; set; }
        public string time_frame { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
    }

    public class FetchTargetAchievementDetailsOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string value_for { get; set; }
        public string target { get; set; }
        public string achv { get; set; }
        public List<AchvlistOutput> achv_list { get; set; }
    }
    public class AchvlistOutput
    {
        public string name { get; set; }
        public string value { get; set; }
        public string date_time { get; set; }
    }

    //public class CompositeInputModel
    //{
    //    public FetchTargetAchievementDetailsInput Model1 { get; set; }
    //    public FetchTargetAchievementDetailsDecimalInput Model2 { get; set; }
    //}

    //public class FetchTargetAchievementDetailsInput
    //{
    //    public long user_id { get; set; }
    //    public long target_type_id { get; set; }
    //    public long target_level_id { get; set; }
    //    public string time_frame { get; set; }
    //    public string start_date { get; set; }
    //    public string end_date { get; set; }
    //}
    //public class FetchTargetAchievementDetailsOutput
    //{
    //    public string status { get; set; }
    //    public string message { get; set; }
    //    public string value_for { get; set; }
    //    public Int32 target { get; set; }
    //    public Int32 achv { get; set; }
    //    public List<AchvlistOutput> achv_list { get; set; }
    //}
    //public class AchvlistOutput
    //{
    //    public string name { get; set; }
    //    public Int32 value { get; set; }
    //    public string date_time { get; set; }
    //}

    //public class FetchTargetAchievementDetailsDecimalInput
    //{
    //    public long user_id { get; set; }
    //    public long target_type_id { get; set; }
    //    public long target_level_id { get; set; }
    //    public string time_frame { get; set; }
    //    public string start_date { get; set; }
    //    public string end_date { get; set; }
    //}
    //public class FetchTargetAchievementDetailsDecimalOutput
    //{
    //    public string status { get; set; }
    //    public string message { get; set; }
    //    public string value_for { get; set; }
    //    public decimal target { get; set; }
    //    public decimal achv { get; set; }
    //    public List<AchvDecimallistOutput> achv_list { get; set; }
    //}
    //public class AchvDecimallistOutput
    //{
    //    public string name { get; set; }
    //    public decimal value { get; set; }
    //    public string date_time { get; set; }
    //}
}
#region======================================Revision History=========================================================
//Written By : Debashis Talukder On 18/01/2023
//Purpose : For New Area/Route/Beat informations.Row 794 to 795
//1.0   V2.0.39     Debashis    19/05/2023      Some new parameters have been added.Row: 842
#endregion===================================End of Revision History==================================================
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class AreaRouteBeatListInput
    {
        [Required]
        public long user_id { get; set; }
        [Required]
        public long beat_id { get; set;}
    }

    public class AreaRouteBeatListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string area_id { get; set; }
        public string area_name { get; set; }
        public string route_id { get; set; }
        public string route_name { get; set; }
        public string beat_id { get; set; }
        public string beat_name { get; set; }
    }

    public class AreaRouteBeatDetailsListInput
    {
        public string user_id { get; set; }
        public string session_token { get; set; }
    }
    public class AreaRouteBeatDetailsListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<ArealistOutput> area_list { get; set; }
    }

    public class ArealistOutput
    {
        public string area_id { get; set; }
        public string area_name { get; set; }
        public List<RoutelistOutput> route_list { get; set; }
    }

    public class RoutelistOutput
    {
        public string route_id { get; set; }
        public string route_name { get; set; }
        public List<BeatlistOutput> beat_list { get; set; }
    }

    public class BeatlistOutput
    {
        public string beat_id { get; set; }
        public string beat_name { get; set; }
    }
    //Rev Debashis Row: 842
    public class BeatAreaRouteListInput
    {
        public string session_token { get; set; }
        [Required]
        public long user_id { get; set; }
    }
    public class BeatAreaRouteListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string user_id { get; set; }
        public string PLAN_ASSNBEATID { get; set; }
        public string PLAN_ASSNBEATName { get; set; }
        public string PLAN_ASSNAREAID { get; set; }
        public string PLAN_ASSNAREAName { get; set; }
        public string PLAN_ASSNROUTEID { get; set; }
        public string PLAN_ASSNROUTEName { get; set; }
    }
    //End of Rev Debashis Row: 842
}
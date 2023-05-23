#region======================================Revision History=========================================================
//1.0   V2.0.39     Debashis    17/05/2023      Some new Parameters have been added.Row: 840
#endregion===================================End of Revision History==================================================
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class AreaModel
    {
        [Required]
        public string session_token { get; set; }
        [Required]
        public string user_id { get; set; }
    }

    public class AreaOutPut
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<AreaLocation> loc_list { get; set; }
    }

    public class AreaLocation
    {
        public string id { get; set; }
        public string location { get; set; }
        public string lattitude { get; set; }
        public string longitude { get; set; }
    }

    public class DistanceListInput
    {
        [Required]
        public string session_token { get; set; }
        [Required]
        public string user_id { get; set; }
        [Required]
        public string from_id { get; set; }
        [Required]
        public string to_id { get; set; }
    }

    public class DistanceListOutPut
    {
        public string status { get; set; }
        public string message { get; set; }
        public string distance { get; set; }
    }
    //Rev 1.0 Row: 840
    public class AreaListByCityInput
    {
        public string session_token { get; set; }
        [Required]
        public long user_id { get; set; }
        [Required]
        public int city_id { get; set;}
    }
    public class AreaListByCityOutPut
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<AreaListByCity> area_list_by_city { get; set; }
    }

    public class AreaListByCity
    {
        public string area_location_id { get; set; }
        public string area_location_name { get; set; }
        public string area_lat { get; set; }
        public string area_long { get; set; }
    }
    //End of Rev 1.0 Row: 840
}
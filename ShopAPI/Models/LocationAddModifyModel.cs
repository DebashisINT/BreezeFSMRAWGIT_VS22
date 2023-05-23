#region======================================Revision History=========================================================
//1.0   V2.0.39     Debashis    17/05/2023      Some new Parameters have been added.Row: 837
#endregion===================================End of Revision History==================================================
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class LocationAddModifyModel
    {
        [Required]
        public string session_token { get; set; }
        [Required]
        public string user_id { get; set; }
        public List<LocationListInput> location_list { get; set; }
    }

    public class LocationListInput
    {
        public string id { get; set; }
        public string location_name { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
    }

    public class LocationAddModifyListInput
    {
        [Required]
        public string session_token { get; set; }
        [Required]
        public string user_id { get; set; }
    }

    public class LocationAddModifyListOutPut
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<LocationListInput> location_list { get; set; }
    }
    //Rev 1.0 Row: 837
    public class VisitLocationListInput
    {
        public string session_token { get; set; }
    }
    public class VisitLocationListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<VisitLocationListDetail> visit_location_list { get; set; }
    }
    public class VisitLocationListDetail
    {
        public int id { get; set; }
        public string visit_location { get; set; }
    }
    //End of Rev 1.0 Row: 837
}
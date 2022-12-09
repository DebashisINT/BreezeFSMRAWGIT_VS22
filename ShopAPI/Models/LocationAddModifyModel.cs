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
}
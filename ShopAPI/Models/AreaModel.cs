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
}
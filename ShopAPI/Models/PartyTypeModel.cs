using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class PartyTypeModel
    {
        [Required]
        public string session_token { get; set; }
        [Required]
        public string user_id { get; set; }
    }

    public class PartyType
    {
        public String id { get; set; }
        public String name { get; set; }
    }

    public class PartyTypeOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<PartyType> type_list { get; set; }
    }
}
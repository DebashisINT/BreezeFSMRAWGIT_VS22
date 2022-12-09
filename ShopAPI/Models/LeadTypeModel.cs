using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class LeadTypeModel
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
    }

    public class LeadType
    {
        public String id { get; set; }
        public String name { get; set; }
    }

    public class LeadTypeOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<LeadType> lead_type_list { get; set; }
    }

}
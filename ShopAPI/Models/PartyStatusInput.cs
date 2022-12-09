using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class PartyStatusInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
    }

    public class PartyStatusOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<PartyStatus> party_status { get; set; }
    }

    public class PartyStatus
    {
        public string id { get; set; }
        public string name { get; set; }

    }

    public class PartyStatusUpdateInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string shop_id { get; set; }
        public string party_status_id { get; set; }
        public string reason { get; set; }

    }

    public class PartyStatusUpdateOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }
}

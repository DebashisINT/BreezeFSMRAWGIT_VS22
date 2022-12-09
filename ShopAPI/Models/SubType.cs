using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class SubTypeInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }

    }

    public class RetailerListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<RetailerList> retailer_list { get; set; }

    }

    public class RetailerList
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type_id { get; set; }


    }

    public class DDListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<DDList> dealer_list { get; set; }

    }

    public class DDList
    {
        public string id { get; set; }
        public string name { get; set; }

    }

    public class BeatListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<BeatList> beat_list { get; set; }

    }

    public class BeatList
    {
        public string id { get; set; }
        public string name { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class NearByTeamModalInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }

    }
    public class NearByTeamModalOutput
    {

        public string status { get; set; }
        public string message { get; set; }
        public List<NearByTeamModal> user_list { get; set; }


    }

    public class NearByTeamModal
    {
        public string id { get; set; }
        public string name { get; set; }
        public string phone_no { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }

    }

}
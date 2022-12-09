using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class IdealModel
    {
        public string ideal_id { get; set; }
        public string start_ideal_date_time { get; set; }
        public string end_ideal_date_time { get; set; }
        public string start_ideal_lat { get; set; }
        public string start_ideal_lng { get; set; }
        public string end_ideal_lat { get; set; }
        public string end_ideal_lng { get; set; }

    }
    public class IdealModelInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public List<IdealModel> location_list { get; set; }

    }


        public class IdealModelOutput
    {
        public string status { get; set; }
        public string message { get; set; }
     
    }
}
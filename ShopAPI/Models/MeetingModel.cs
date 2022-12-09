using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class MeetingModel
    {
    }

    public class MeetingListInput
    {
        public String session_token { get; set; }
        public String user_id { get; set; }
    }

    public class MeetingListOutPut
    {
        public String status { get; set; }
        public String message { get; set; }
        public List<MeetingList> meeting_list { get; set; }
    }

    public class MeetingList
    {
        public String latitude { get; set; }
        public String longitude { get; set; }
        public String address { get; set; }
        public String pincode { get; set; }
        public string date { get; set; }
        public DateTime date_time { get; set; }
        public Decimal distance_travelled { get; set; }
        public String duration { get; set; }
        public String remarks { get; set; }
        public int meeting_type_id { get; set; }
    }
}
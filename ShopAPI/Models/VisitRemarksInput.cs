using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class VisitRemarksInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }

    }

    public class VisitRemarksOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<VisitRemarks> remarks_list { get; set; }

    }

    public class VisitRemarks
    {
        public string id { get; set; }
        public string name { get; set; }

    }
}


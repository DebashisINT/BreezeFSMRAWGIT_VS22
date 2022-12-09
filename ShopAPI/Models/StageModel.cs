using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class StageModel
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
    }

    public class Stages
    {
        public String id { get; set; }
        public String name { get; set; }
    }

    public class StageOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<Stages> stage_list { get; set; }
    }

    public class FunnelStageOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<Stages> funnel_stage_list { get; set; }
    }
}
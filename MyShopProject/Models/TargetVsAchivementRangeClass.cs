using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class TargetVsAchivementRangeClass
    {

        public List<TargetVsAchivementRangeList> TargetVsAchivementList { get; set; }
        public List<StateListRangeTarget> StateListTarget { get; set; }

    }



    public class StateListRangeTarget
    {
        public string ID { get; set; }
        public string Name { get; set; }

    }
    public class TargetVsAchivementRangeList
    {
        public string EMPNAME { get; set; }
        public string LOGINID { get; set; }
        public string STATE { get; set; }
        public string DEG { get; set; }
        public string RPTTOEMPCODE { get; set; }
        public Int32 TODAYNC { get; set; }
        public Int32 ACHVNC { get; set; }
        public Int32 TODAYRV { get; set; }
        public Int32 ACHVRV { get; set; }
        public decimal TODAYORDER { get; set; }
        public decimal ACHVORD { get; set; }
        public decimal TODAYCOL { get; set; }
        public decimal ACHVCOL { get; set; }

        public string REPORTTO { get; set; }


    }
}

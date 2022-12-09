using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class HierarchyWiseTargetClass
    {
        public List<TargetVsAchivementList> FirstLevelGrid { get; set; }
        public List<TargetVsAchivementList> SecondLevelGrid { get; set; }
        public List<TargetVsAchivementList> ThirdLevelGrid { get; set; }
        public List<TargetVsAchivementList> FourthLevelGrid { get; set; }
        public List<TargetVsAchivementList> FifthLevelGrid { get; set; }
        public List<TargetVsAchivementList> SixthLevelGrid { get; set; }
        public List<TargetVsAchivementList> SeventhLevelGrid { get; set; }
        public List<StateListTarget> StateListTarget { get; set; }
        public List<MonthList> MonthList { get; set; }
        public List<YearList> YearList { get; set; }

        public List<PPDDShopList> PPDDShopList { get; set; }
    }
}
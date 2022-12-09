using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class TargetVsAchivementClass
    {
        public List<TargetVsAchivementList> TargetVsAchivementList { get; set; }
        public List<StateListTarget> StateListTarget { get; set; }
        public List<MonthList> MonthList { get; set; }
        public List<YearList> YearList { get; set; }
        


    }
    public class MonthList
    {
        public string ID { get; set; }
        public string MonthName { get; set; }
    }

    public class YearList
    {
        public string ID { get; set; }
        public string YearName { get; set; }
    }


    public class StateListTarget
    {
        public string ID { get; set; }
        public string Name { get; set; }

    }
    public class TargetVsAchivementList
    {
        public string EMPCODE { get; set; }
        public string EMPNAME { get; set; }
        public string CONTACTNO { get; set; }
        public string STATE { get; set; }
        public string DESIGNATION { get; set; }
        public string RPTTOEMPCODE { get; set; }
        public Int32 TGT_NC { get; set; }
        public Int32 ACHV_NC { get; set; }
        public Int32 TGT_RV { get; set; }
        public Int32 ACHV_RV { get; set; }
        public decimal TGT_ORDERVALUE { get; set; }
        public decimal ACHV_ORDERVALUE { get; set; }
        public decimal TGT_COLLECTION { get; set; }
        public decimal ACHV_COLLECTION { get; set; }

        public string REPORTTO { get; set; }


    }


    public class PPDDShopList
    {
        public string EMPNAME { get; set; }
        public string SHOP_NAME { get; set; }

        public string SHOP_TYPE { get; set; }
        public string CONTACTNO { get; set; }
        public string STATE { get; set; }
        public decimal TGT_ORDERVALUE { get; set; }
        public decimal ACHV_ORDERVALUE { get; set; }
        public decimal TGT_COLLECTION { get; set; }
        public decimal ACHV_COLLECTION { get; set; }

    }
}
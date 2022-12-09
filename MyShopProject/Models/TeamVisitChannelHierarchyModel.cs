using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class TeamVisitChannelHierarchyModel
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Fromdate { get; set; }



        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Todate { get; set; }


        public List<string> BranchId { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public List<string> desgid { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public List<string> empcode { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public List<string> ChannelId { get; set; }

        public List<GetDesignation> designation { get; set; }

        public List<GetAllEmployee> employee { get; set; }

        public List<GetBranchTVCH> modelbranch = new List<GetBranchTVCH>();

        public string is_pageload { get; set; }
    }

    public class GetBranchTVCH
    {
        public long BRANCH_ID { get; set; }
        public string CODE { get; set; }
    }
}
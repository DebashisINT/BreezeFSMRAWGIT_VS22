using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class DSVisitChannelHierarchy
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

        public List<string> ChannelId { get; set; }

        public List<GetDesignation> designation { get; set; }

        public List<GetAllEmployee> employee { get; set; }

        public List<GetBranchDSCH> modelbranch = new List<GetBranchDSCH>();

        public string is_pageload { get; set; }
    }

    public class DSVisitChannelHierarchyModel
    {
        public string USERID { get; set; }
        public string SEQ { get; set; }
        public string EMPROWID { get; set; }
        public string LOGINDATE { get; set; }
        public string LOGIN_DATETIME { get; set; }
        public string BRANCH_ID { get; set; }
        public string BRANCHDESC { get; set; }
        public decimal EMPUSERID { get; set; }
        public decimal EMPCODE { get; set; }
        public string EMPID { get; set; }
        public string EMPNAME { get; set; }
        public string STATEID { get; set; }
        public string STATE { get; set; }
        public string DEG_ID { get; set; }
        public string DESIGNATION { get; set; }
        public string DATEOFJOINING { get; set; }
        public string CONTACTNO { get; set; }
        public string REPORTTOIDWD { get; set; }
        public string REPORTTOUIDWD { get; set; }
        public string REPORTTOWD { get; set; }
        public string RPTTODESGWD { get; set; }
        public string REPORTTOIDAE { get; set; }
        public string REPORTTOUIDAE { get; set; }
        public string REPORTTOAE { get; set; }
        public string RPTTODESGAE { get; set; }
        public int OUTLETSMAPPED { get; set; }
        public int NEWSHOP_VISITED { get; set; }
        public int RE_VISITED { get; set; }
        public int TOTAL_VISIT { get; set; }
        public decimal DISTANCE_TRAVELLED { get; set; }
        public string AVGTIMESPENTINMARKET { get; set; }
        public string DAYSTTIME { get; set; }
        public string DAYENDTIME { get; set; }
        public string AVGSPENTDURATION { get; set; }
        public long DSTYPEID { get; set; }
		public string DSTYPE { get; set; }
		public string CIRCLE { get; set; }
		public string SECTION { get; set; }
		public long CH_ID { get; set; }
        public string CHANNEL { get; set; }
    }

    public class GetBranchDSCH
    {
        public long BRANCH_ID { get; set; }
        public string CODE { get; set; }
    }
}
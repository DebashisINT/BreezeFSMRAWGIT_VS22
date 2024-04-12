#region======================================Revision History=========================================================================
//1.0   V2 .0.44    Debashis    27/02/2024      'Sale Value' Field required in DS Visit Details/DS Visit Summary.Refer: 0027276
#endregion===================================End of Revision History==================================================================
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class DSSummaryReport
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

        public List<GetDesignation> designation { get; set; }

        public List<GetAllEmployee> employee { get; set; }

        public List<GetBranch> modelbranch = new List<GetBranch>();

        public string is_pageload { get; set; }
    }

    public class DSSummaryListModel
    {
        public string USERID { get; set; }
        public string SEQ { get; set; }
        public string BRANCH_ID { get; set; }
        public string BRANCHDESC { get; set; }
        public string EMPCODE { get; set; }
        public string EMPID { get; set; }
        public string EMPNAME { get; set; }
        public decimal STATEID { get; set; }
        public decimal STATE { get; set; }
        public string DEG_ID { get; set; }
        public string DESIGNATION { get; set; }
        public string DATEOFJOINING { get; set; }
        public string CONTACTNO { get; set; }
        public string REPORTTOID { get; set; }
        public string REPORTTOUID { get; set; }
        public string REPORTTO { get; set; }
        public string RPTTODESG { get; set; }
        public decimal HREPORTTOID { get; set; }
        public decimal HREPORTTOUID { get; set; }
        public decimal HREPORTTO { get; set; }
        public string HRPTTODESG { get; set; }
        public string DATERANGE { get; set; }
        public string NEWSHOP_VISITED { get; set; }
        public string RE_VISITED { get; set; }
        public string TOTAL_VISIT { get; set; }
        //Rev 1.0 Mantis: 0027276
        public string SALE_VALUE { get; set; }
        //End of Rev 1.0 Mantis: 0027276
        public string DISTANCE_TRAVELLED { get; set; }
        public string AVGTIMESPENTINMARKET { get; set; }
        public string AVGSPENTDURATION { get; set; }

    }
}
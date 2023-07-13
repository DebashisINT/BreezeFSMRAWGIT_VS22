#region======================================Revision History=========================================================================
//1.0   V2.0.41    Debashis     13/07/2023      A New Report Required for ITC under Report > MIS > Outlet Revisit Details
//                                              And Parameter will be same as 'DS Visit Details' report.Refer: 0026473
#endregion===================================End of Revision History==================================================================

using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class OutletRevisitDetailsReport
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

    public class OutletRVDetailsModel
    {
        public string USERID { get; set; }
        public string SEQ { get; set; }
        public string EMPROWID { get; set; }
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
        public string STARTENDDATE { get; set; }
        public decimal REPORTTOID { get; set; }
        public decimal REPORTTOUID { get; set; }
        public decimal REPORTTO { get; set; }
        public string RPTTODESG { get; set; }
        public string HREPORTTOID { get; set; }
        public string HREPORTTOUID { get; set; }
        public string HREPORTTO { get; set; }
        public string HRPTTODESG { get; set; }
        public string VISITEDDATE { get; set; }
        public string VISITEDDATEORDBY { get; set; }
        public string VISITEDTIME { get; set; }
        public long OUTLETID { get; set; }
        public string OUTLETNAME { get; set; }
        public string LATITUDE { get; set; }
        public string LONGITUDE { get; set; }
        public long DSTYPEID { get; set; }
        public string DSTYPE { get; set; }
    }
}
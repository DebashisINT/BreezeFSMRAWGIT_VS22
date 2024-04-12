/**************************************************************************************************************************
 * 1.0         03-04-2024        2.0.46           Sanchita            0027343: Employee Outlet Master : Report parameter one check 
                                                                      box required 'Consider Inactive Outlets'
*************************************************************************************************************************/
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class OutletMasterReport
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
        // Rev 1.0
        public string IsInactiveOutlets { get; set; }
        // End of Rev 1.0
    }
    public class OutletMasterListModel
    {
        public string USERID { get; set; }
        public string SEQ { get; set; }
        public string BRANCH_ID { get; set; }
        public string BRANCHDESC { get; set; }
        public string EMPCODE { get; set; }
        public string EMPID { get; set; }
        public string EMPNAME { get; set; }
        public decimal STATEID { get; set; }
        public string STATE { get; set; }
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
        public string OUTLETID { get; set; }
        public string OUTLETNAME { get; set; }
        public string OUTLETADDRESS { get; set; }
        public string OUTLETCONTACT { get; set; }
        public string OUTLETLAT { get; set; }
        public string OUTLETLANG { get; set; }

    }
}

//public class GetDesignation
//{
//    public string desgid { get; set; }

//    public string designame { get; set; }
//}
//public class GetAllEmployee
//{

//    public string empcode { get; set; }

//    public string empname { get; set; }
//}
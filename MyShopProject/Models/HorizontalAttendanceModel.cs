/*************************************************************************************************************
 * Rev 1.0      Sanchita       V2.0.42      19/07/2023      Add Branch parameter in Listing of MIS - Attendance Register. Mantis : 26135
 * Rev 2.0      Sanchita       V2.0.42      11/08/2023      Two check box is required to show the first call time & last call time in Attendance Register Report
 *                                                          Mantis : 26707
 * Rev 3.0      Sanchita       V2.0.44      08-11-2023      26954 : In Attendance Register Report, Including Inactive users check box implementation is required
 * ******************************************************************************************************************/
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class HorizontalAttendanceModel
    {
        public String Fromdate { get; set; }
        public String Todate { get; set; }
        public List<string> ProductID { get; set; }
        public List<string> empcode { get; set; }
        public String is_pageload { get; set; }
        public String PageName { get; set; }

        public String TotalKMTravelled { get; set; }
        public String SecondarySalesValue { get; set; }
        public String IdleTimeCount { get; set; }
        public String ShowAttendanceSelfie { get; set; }

        //Mantise work 0025111
        public int ShowFullday { get; set; }
        public int ShowLoginLocation { get; set; }
        public int ShowLogoutLocation { get; set; }
        //End of Mantise work 0025111
        // Rev 1.0
        public List<string> BranchId { get; set; }
        public List<GetBranch> modelbranch = new List<GetBranch>();
        // End of Rev 1.0
        // Rev 2.0
        public String ShowFirstCallTime { get; set; }
        public String ShowLastCallTime { get; set; }
        // End of Rev 2.0
        // Rev 3.0
        public String ShowInactiveUser { get; set; }
        // End of Rev 3.0
    }
}
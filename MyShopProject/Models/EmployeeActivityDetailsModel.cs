#region======================================Revision History=========================================================================                                             
//1.0   V2.0.42     Priti       20/07/2023      Branch Parameter is required for various FSM reports.Refer:0026135
#endregion===================================End of Revision History==================================================================
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class EmployeeActivityDetailsModel
    {
        public List<string> EmployeeID { get; set; }
        public List<string> State { get; set; }
        public List<string> Designation_id { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Ispageload { get; set; }
        //Rev 1.0
        public List<string> BranchId { get; set; }
        public List<GetBranch> modelbranch = new List<GetBranch>();
        //Rev 1.0 End
    }

    public class EmployeeActiveDetailsLists
    {
        public string STATE_NAME { get; set; }
        public string DESIGNATION { get; set; }
        public string EMPNAME { get; set; }
        public string SHOP_NAME { get; set; }
        public string SHOP_TYPE { get; set; }
        public string MOBILE_NO { get; set; }
        public string LOCATION { get; set; }
        public DateTime VISITDATE { get; set; }
        public DateTime VISITTIME { get; set; }
        public string DURATION { get; set; }
        public decimal DISTANCE { get; set; }
        public string VISIT_TYPE { get; set; }
        public long USER_ID { get; set; }
    }
}
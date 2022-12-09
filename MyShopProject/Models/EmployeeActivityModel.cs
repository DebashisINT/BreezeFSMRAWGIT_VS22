using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class EmployeeActivityModel
    {
        public List<string> EmployeeID { get; set; }
        public List<string> State { get; set; }
        public List<string> Designation_id { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Ispageload { get; set; }
        //Rev Debashis Mantis: 0025104
        public string IsOnlyLoginData { get; set; }
        public string IsOnlyLogoutData { get; set; }
        //End of Rev Debashis Mantis: 0025104
    }

    public class EmployeeActiveLists
    {
        public string State_name { get; set; }
        public string Designation { get; set; }
        public string Employee_Name { get; set; }
        public string SHOPE_NAME { get; set; }
        public string SHOPE_TYPE { get; set; }
        public string MOBILE_NO { get; set; }
        public string LOCATION { get; set; }
        public DateTime VISIT_TIME { get; set; }
        public string DURATION { get; set; }
        public decimal DISTANCE { get; set; }
        public string VISIT_TYPE { get; set; }
        public long USER_ID { get; set; }
    }
}
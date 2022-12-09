using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class LocationReportModel
    {
        public List<string> EmployeeID { get; set; }
        public List<string> State { get; set; }
        public List<string> Designation_id { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Ispageload { get; set; }
    }

    public class LocationReportList
    {
        public string State_name { get; set; }
        public string Designation { get; set; }
        public string Employee_Name { get; set; }
        public DateTime VISIT_TIME { get; set; }
        public string LOCATION { get; set; }
        public string SHOPE_NAME { get; set; }
        public string SHOPE_TYPE { get; set; }
        public string ADDRESS { get; set; }
    }
}
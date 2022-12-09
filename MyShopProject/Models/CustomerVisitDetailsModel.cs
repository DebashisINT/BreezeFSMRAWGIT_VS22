using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class CustomerVisitDetailsModel
    {
        public List<string> EmployeeID { get; set; }
        public List<string> State { get; set; }
        public List<string> Designation_id { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Ispageload { get; set; }
    }

    public class CustomerVisitDetailsLists
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class ShopPerformanceSummaryModel
    {
        public List<string> EmployeeID { get; set; }
        public List<string> State { get; set; }
        public List<string> Designation_id { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Ispageload { get; set; }
    }

    public class ShopPerformanceSummaryListModel
    {
        public DateTime Orderdate { get; set; }
        public string StateName { get; set; }
        public string EmployeeName { get; set; }
        public string deg_designation { get; set; }
        public string Shop_Name { get; set; }
        public string ENTITYCODE { get; set; }
        public string Typename { get; set; }
        public decimal Ordervalue { get; set; }
        public decimal Total_invoiceamt { get; set; }
    }
}
//====================================================== Revision History =================================================================================================
//1.0    03-05-2024    V2 .0.47    Priti   0027407: "Party Status" - needs to add in the following reports.
//====================================================== Revision History =================================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class ShopPerformanceDetailsModel
    {
        public List<string> EmployeeID { get; set; }
        public List<string> State { get; set; }
        public List<string> Designation_id { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Ispageload { get; set; }
    }

    public class ShopPerformanceDetailsListModel
    {
        public DateTime Orderdate { get; set; }
        public string StateName { get; set; }
        public string EmployeeName { get; set; }
        public string deg_designation { get; set; }
        public string Shop_Name { get; set; }
        public string ENTITYCODE { get; set; }
        public string Typename { get; set; }
        public string Brand_Name { get; set; }
        public string Category { get; set; }
        public string Strength { get; set; }
        public long Order_ID { get; set; }
        public string sProducts_Name { get; set; }
        public decimal Product_Qty { get; set; }
        public decimal Product_Rate { get; set; }
        public decimal Product_Price { get; set; }
        //REV 1.0
        public string PARTYSTATUS { get; set; }
        //REV 1.0 END
    }
}
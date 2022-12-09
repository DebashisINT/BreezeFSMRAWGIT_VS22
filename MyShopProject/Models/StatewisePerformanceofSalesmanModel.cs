using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class StatewisePerformanceofSalesmanModel
    {

        public String Fromdate { get; set; }
        public String Todate { get; set; }
        public List<string> ProductID { get; set; }
        public List<string> empcode { get; set; }
        public String is_pageload { get; set; }
        public String PageName { get; set; }
    }

    public class StatewisePerformanceSales
    {
        public string PID { get; set; }
        public string PMonthName { get; set; }
    }

    public class ProductList
    {
        public String ProductName { get; set; }
        public String Productcode { get; set; }
    }

    public class MonthwisePerformanceofSalesmanModel
    {
        public String MonthID { get; set; }
        public List<string> ProductID { get; set; }
        public List<string> empcode { get; set; }
        public String is_pageload { get; set; }
        public String PageName { get; set; }
        public String YearID { get; set; }
    }
}
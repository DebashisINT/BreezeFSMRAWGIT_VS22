using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class OrderStatusModel
    {
        public List<string> EmployeeID { get; set; }
        public List<string> StateId { get; set; }
        public List<string> desgid { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Ispageload { get; set; }
        public string REPORT_BY { get; set; }
        //Rev Debashis 0025198
        public List<string> BranchId { get; set; }
        public List<GetBranch> modelbranch = new List<GetBranch>();
        //End of Rev Debashis 0025198
    }

    public class OrderStatusListModel
    {
        public string Employee_Name { get; set; }
        public string Shop_Name { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Shop_Type { get; set; }
        public DateTime Order_Date { get; set; }
        public string Order_Number { get; set; }
        public Decimal Order_Value { get; set; }
        public string Invoice_Number { get; set; }
        public DateTime Invoice_Date { get; set; }
        public Decimal Delivered_Value { get; set; }
    }

    public class OrderDetailsStatusProductslist
    {
        public decimal Product_Qty { get; set; }
        public string Shop_Name { get; set; }
        public long Order_ProdId { get; set; }
        public decimal Product_Rate { get; set; }
        public decimal Product_Price { get; set; }
        public string sProducts_Name { get; set; }
        public string Shop_code { get; set; }
        public long Order_ID { get; set; }
        public long Product_Id { get; set; }
    }
}
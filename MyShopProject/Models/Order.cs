//====================================================== Revision History ==========================================================
//1.0  03-02-2023    2.0.38    Priti     0025604: Enhancement Required in the Order Summary Report
//====================================================== Revision History ==========================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class OrderDetailsListInput
    {
        public string selectedusrid { get; set; }

        public List<GetUserName> userlsit { get; set; }
        public string stateid { get; set; }
        public List<GetStateName> statelist { get; set; }
        public string shopId { get; set; }
        public List<Getmaster> shoplist { get; set; }
        public string Fromdate { get; set; }

        public string Todate { get; set; }

        public string usertype { get; set; }

    }


    public class Getmaster
    {
        public string ID { get; set; }
        public string Name { get; set; }

    }


    public class GetStateName
    {
        public string ID { get; set; }
        public string StateName { get; set; }

    }
    public class GetUserName
    {
        public string UserID { get; set; }
        public string username { get; set; }


    }

    public class OrderDetailsListOutput
    {
        public string shop_name { get; set; }

        public string address { get; set; }

        public string owner_name { get; set; }

        public string owner_contact_no { get; set; }

        public decimal order_amount { get; set; }

        public DateTime? Orderdate { get; set; }

        public decimal collection { get; set; }

        public string Shoptype { get; set; }

        public string UserName { get; set; }
        public string Order_Description { get; set; }
        public string State { get; set; }

    }


    #region Order Details Summary 
    public class OrderDetailsSummary
    {
        public string EmployeeName { get; set; }
        public string BRANCHDESC { get; set; }
        public string shop_name { get; set; }
        public string ENTITYCODE { get; set; }
        public string address { get; set; }
        public string owner_contact_no { get; set; }
        public string Shoptype { get; set; }
        public string date { get; set; }
        public string OrderCode { get; set; }
        public decimal order_amount { get; set; }
        public long OrderId { get; set; }
        //Rev Debashis
        public string Patient_Name { get; set; }
        public string Patient_Phone_No { get; set; }
        public string Patient_Address { get; set; }
        public string Hospital { get; set; }
        public string Email_Address { get; set; }
        //End of Rev Debashis
    }

    public class OrderDetailsSummaryProducts
    {

        public List<Productlist_Order> products { get; set; }

        public long Product_Id { get; set; }

        public decimal Product_Qty { get; set; }
        public decimal Product_Rate { get; set; }
        public decimal Product_Price { get; set; }
        public long Order_ID { get; set; }
        public long Order_ProdId { get; set; }
        public List<OrderDetailsSummaryProductslist> productdetails { get; set; }
        //REV 1.0
        public decimal Product_MRP { get; set; }
        public decimal Product_Discount { get; set; }
        //REV 1.0 END


    }

    public class OrderDetailsSummaryProductslist
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
        //Rev Debashis
        public decimal MRP { get; set; }
        //End of Rev Debashis

        //REV 1.0
        public decimal Product_MRP { get; set; }
        public decimal Product_Discount { get; set; }
        //REV 1.0 END

    }
    public  class Productlist_Order
    {

        public long Id { get; set; }
        public string Product_name { get; set; }


    }
    //REV 1.0
    public class ProductDetails
    {
        public decimal sProduct_MRP { get; set; }
        public decimal sProducts_Discount { get; set; }
    }
    //REV 1.0 END

    #endregion
}
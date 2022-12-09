using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class InvoiceHierarchywise
    {
        public string selectedusrid { get; set; }
        public List<string> StateId { get; set; }
        public List<GetUserName> userlsit { get; set; }
        public List<GetUsersStates> states { get; set; }
        public List<string> empcode { get; set; }
        public string Is_PageLoad { get; set; }
        public List<string> shopId { get; set; }
        public List<Getmasterstock> shoplist { get; set; }
        public string Fromdate { get; set; }
        public string Todate { get; set; }
        public int Uniquecont { get; set; }


        public string   order_id { get; set; }

        public List<orders> order_list { get; set; }
        public string Product_Id { get; set; }

        public List<Productlist_Order> products { get; set; }

      




    }

    public class orders
    {
        public string order_id { get; set; }
        public string order_number { get; set; }
    }
}
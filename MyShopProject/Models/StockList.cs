using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class StockDetailsListInput
    {
        public string selectedusrid { get; set; }

        public List<GetUserNameStock> userlsit { get; set; }
        public string stateid { get; set; }
        public List<GetStateNamestock> statelist { get; set; }
        public string shopId { get; set; }
        public List<Getmasterstock> shoplist { get; set; }
        public string Fromdate { get; set; }

        public string Todate { get; set; }

        public string usertype { get; set; }

    }


    public class Getmasterstock
    {
        public string ID { get; set; }
        public string Name { get; set; }

    }

    public class GetmasterCity
    {
        public string CityID { get; set; }
        public string CityName { get; set; }
    }

    public class GetmasterArea
    {
        public string AreaID { get; set; }
        public string AreaName { get; set; }
    }


    public class GetStateNamestock
    {
        public string ID { get; set; }
        public string StateName { get; set; }

    }
    public class GetUserNameStock
    {
        public string UserID { get; set; }
        public string username { get; set; }


    }

    public class StockDetailsListOutput
    {
        public string shop_name { get; set; }

        public string address { get; set; }

        public string opening_stock_amount { get; set; }

        public string closing_stock_amount { get; set; }

        public string order_amount { get; set; }

        public DateTime? StockDate { get; set; }

        public decimal opening_stock_month { get; set; }

        public string closing_stock_month { get; set; }
        public string assign_type { get; set; }
        public string UserName { get; set; }
        public string m_o { get; set; }
        public string c_o { get; set; }
        public string p_o { get; set; }

        public string State { get; set; }

    }


}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{

    public class StockmanagementInput
    {

        public string session_token { get; set; }
        public string user_id { get; set; }
        public string stock_id { get; set; }
        public string opening_stock_amount { get; set; }
        public string closing_stock_amount { get; set; }
        public string opening_stock_month { get; set; }
        public string closing_stock_month { get; set; }
        public string opening_stock_month_val { get; set; }
        public string opening_stock_year_val { get; set; }
        public string closing_stock_month_val { get; set; }
        public string closing_stock_year_val { get; set; }
        public string shop_id { get; set; }
        public string m_o { get; set; }
        public string c_o { get; set; }
        public string p_o { get; set; }
        public string stock_date { get; set; }


    }
    public class StockAddoutput
    {
        public string status { get; set; }
        public string message { get; set; }

    }






    public class StockfetchInput
    {


        public string user_id { get; set; }

        public string session_token { get; set; }


    }


    public class StockfetchOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<Stockfetch> stock_list { get; set; }

    }

    public class Stockfetch
    {



        public string shop_id { get; set; }
        public string shop_name { get; set; }
        public string address { get; set; }
        public string pin_code { get; set; }
        public string shop_lat { get; set; }
        public string shop_long { get; set; }
        public string owner_name { get; set; }
        public string owner_contact_no { get; set; }
        public string owner_email { get; set; }

        public string order_amount { get; set; }
        public string stock_id { get; set; }

        public string shop_image_link { get; set; }

        public string opening_stock_amount { get; set; }
        public string opening_stock_month { get; set; }
        public string closing_stock_amount { get; set; }
        public string closing_stock_month { get; set; }
        public string total_visit_count { get; set; }
        public string last_visit_date { get; set; }
        public string assign_type { get; set; }

        public string m_o { get; set; }
        public string c_o { get; set; }
        public string p_o { get; set; }
        public string stock_date { get; set; }
      



    }

    public class StockfetchOutputList
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<StockfetchList> stock_list { get; set; }

    }


    public class StockfetchList
    {

        public string shop_id { get; set; }
        public string stock_value { get; set; }
        public string m_o { get; set; }
        public string c_o { get; set; }
        public string p_o { get; set; }
        public string stock_date { get; set; }
    }

}
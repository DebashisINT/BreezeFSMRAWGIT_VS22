using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class MISInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string month { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string year { get; set; }
    }

    public class MISlistOutput
    {
        public string status { get; set; }
        public string message { get; set; }
       
        public ShopMisClasscounting shop_list_count { get; set; }
        public List<MISShopslists> shop_list { get; set; }
    }

    public class ShopMisClasscounting
    {
        public int total_time_spent_at_shop { get; set; }
        public int total_shop_visited { get; set; }

        public int total_attendance { get; set; }

        public int total_new_shop_added { get; set; }

    }

    public class MISShopslists
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

        public string Shop_Image { get; set; }

        public DateTime? dob { get; set; }
        public DateTime? date_aniversary { get; set; }

        public DateTime? last_visit_date { get; set; }

        public int total_visit_count { get; set; }
    }
}
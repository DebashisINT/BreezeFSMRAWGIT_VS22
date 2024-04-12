#region======================================Revision History=========================================================
//Written By : Debashis Talukder On 03/04/2024
//Purpose: For ITC New Order.Row: 911 to 912
#endregion===================================End of Revision History==================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class ITCOrderWithProductDetailSaveInput
    {
        public long user_id { get; set; }
        public string order_id { get; set; }
        public string order_date { get; set; }
        public string order_time { get; set; }
        public DateTime order_date_time { get; set; }
        public string shop_id { get; set; }
        public string shop_name { get; set; }
        public int shop_type { get; set; }
        public int isInrange { get; set; }
        public string order_lat { get; set; }
        public string order_long { get; set; }
        public string shop_addr { get; set; }
        public string shop_pincode { get; set; }
        public decimal order_total_amt { get; set; }
        public string order_remarks { get; set; }
        public List<ITCOrderProductLists> product_list { get; set; }
    }
    public class ITCOrderProductLists
    {
        public string order_id { get; set; }
        public long product_id { get; set; }
        public string product_name { get; set; }
        public decimal submitedQty { get; set; }
        public decimal submitedSpecialRate { get; set; }
    }
    public class ITCOrderWithProductDetailSaveOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class ITCListforOrderProductInput
    {
        public string user_id { get; set; }
    }

    public class ITCListforOrderProductOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string user_id { get; set; }
        public List<ITCOrderlistOutput> order_list { get; set; }
    }

    public class ITCOrderlistOutput
    {
        public string order_id { get; set; }
        public string order_date { get; set; }
        public string order_time { get; set; }
        public DateTime order_date_time { get; set; }
        public string shop_id { get; set; }
        public string shop_name { get; set; }
        public int shop_type { get; set; }
        public int isInrange { get; set; }
        public string order_lat { get; set; }
        public string order_long { get; set; }
        public string shop_addr { get; set; }
        public string shop_pincode { get; set; }
        public decimal order_total_amt { get; set; }
        public string order_remarks { get; set; }
        public string isUploaded { get; set; }
        public List<ITCOrderProductListOutput> product_list { get; set; }
    }

    public class ITCOrderProductListOutput
    {
        public string order_id { get; set; }
        public long product_id { get; set; }
        public string product_name { get; set; }
        public decimal submitedQty { get; set; }
        public decimal submitedSpecialRate { get; set; }
    }
}
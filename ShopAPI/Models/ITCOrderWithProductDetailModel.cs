#region======================================Revision History=========================================================
//Written By : Debashis Talukder On 03/04/2024
//Purpose: For ITC New Order.Row: 911 to 912
//1.0   V2.0.47     Debashis    06/06/2024      Some new parameters have been added.Row: 938 to 940
//2.0   V2.0.47     Debashis    11/06/2024      Some new parameters have been added.Row: 942 to 944
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
        //Rev 2.0 Row: 942
        public decimal total_amt { get; set; }
        public decimal mrp { get; set; }
        public decimal itemPrice { get; set; }
        //End of Rev 2.0 Row: 942
    }
    public class ITCOrderWithProductDetailSaveOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    //Rev 1.0
    public class ITCOrderWithProductDetailEditInput
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
        public DateTime order_edit_date_time { get; set; }
        public string order_edit_remarks { get; set; }
        public List<ITCOrderEditProductLists> product_list { get; set; }
    }

    public class ITCOrderEditProductLists
    {
        public string order_id { get; set; }
        public long product_id { get; set; }
        public string product_name { get; set; }
        public decimal submitedQty { get; set; }
        public decimal submitedSpecialRate { get; set; }
        //Rev 2.0 Row: 944
        public decimal total_amt { get; set; }
        public decimal mrp { get; set; }
        public decimal itemPrice { get; set; }
        //End of Rev 2.0 Row: 944
    }

    public class ITCOrderWithProductDetailEditOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class ITCOrderWithProductDetailDeleteInput
    {
        public long user_id { get; set; }
        public string session_token { get; set; }
        public List<ITCDeleteProductLists> order_delete_list { get; set; }
    }

    public class ITCDeleteProductLists
    {
        public string order_id { get; set; }
    }

    public class ITCOrderWithProductDetailDeleteOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }
    //End of Rev 1.0

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
        //Rev 1.0
        public DateTime order_edit_date_time { get; set; }
        public string order_edit_remarks { get; set; }
        public string isEdited { get; set; }
        //End of Rev 1.0
        public List<ITCOrderProductListOutput> product_list { get; set; }
    }

    public class ITCOrderProductListOutput
    {
        public string order_id { get; set; }
        public long product_id { get; set; }
        public string product_name { get; set; }
        public decimal submitedQty { get; set; }
        public decimal submitedSpecialRate { get; set; }
        //Rev 2.0 Row: 943
        public decimal total_amt { get; set; }
        public decimal mrp { get; set; }
        public decimal itemPrice { get; set; }
        //End of Rev 2.0 Row: 943
    }
}
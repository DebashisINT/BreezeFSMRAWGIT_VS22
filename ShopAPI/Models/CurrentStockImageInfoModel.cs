#region======================================Revision History=========================================================
//Written By : Debashis Talukder On 17/05/2023
//Purpose : Save Current Stock Images.
#endregion===================================End of Revision History==================================================
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class SaveCurrentStockImage1Input
    {
        public string data { get; set; }
        public HttpPostedFileBase attachment_stock_image1 { get; set; }
    }
    public class SaveCurrentStockImage1InputDetails
    {
        public string session_token { get; set; }
        public string stock_id { get; set; }
        public string user_id { get; set; }
    }
    public class SaveCurrentStockImage1Output
    {
        public string status { get; set; }
        public string message { get; set; }
    }
    public class SaveCurrentStockImage2Input
    {
        public string data { get; set; }
        public HttpPostedFileBase attachment_stock_image2 { get; set; }
    }
    public class SaveCurrentStockImage2InputDetails
    {
        public string session_token { get; set; }
        public string stock_id { get; set; }
        public string user_id { get; set; }
    }
    public class SaveCurrentStockImage2Output
    {
        public string status { get; set; }
        public string message { get; set; }
    }
    public class CurrentStockImageLinkInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string stock_id { get; set; }
    }
    public class CurrentStockImageLinkOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string user_id { get; set; }
        public string stock_id { get; set; }
        public List<StockWiseImageList> stockwise_image_list { get; set; }
    }

    public class StockWiseImageList
    {
        public string attachment_stock_image1 { get; set; }
        public string attachment_stock_image2 { get; set; }
    }
}
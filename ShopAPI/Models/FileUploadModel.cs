#region======================================Revision History=========================================================
//1.0   V2.0.48     Debashis    31/07/2024      Some new Parameters have been added.Row: 957
//2.0   V2.0.49     Debashis    17/09/2024      A new parameter has been added.Row: 977
#endregion===================================End of Revision History==================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class FileUploadModel
    {
    }

    public class AudioUpload
    {
        public String data { get; set; }
        public HttpPostedFileBase audio { get; set; }
    }

    public class RevisitAudioInput
    {
        public String session_token { get; set; }
        public String user_id { get; set; }
        public String shop_id { get; set; }
        public String visit_datetime { get; set; }
        public String audio { get; set; }
    }

    public class RevisitAudioOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class OrderSignatureUpload
    {
        public String data { get; set; }
        public HttpPostedFileBase signature { get; set; }
    }

    public class OrderSignatureOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class OrderSignatureInput
    {
        public string address { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }

        public string session_token { get; set; }
        public string user_id { get; set; }
        public string order_amount { get; set; }
        public string order_id { get; set; }
        public string shop_id { get; set; }
        public string description { get; set; }
        public string collection { get; set; }
        public DateTime? order_date { get; set; }
        public string remarks { get; set; }
        //Extra Input for 4Basecare
        public string patient_no { get; set; }
        public string patient_name { get; set; }
        public string patient_address { get; set; }
        //Extra Input for 4Basecare
        //Extra Input for EuroBond
        public string Hospital { get; set; }
        public string Email_Address { get; set; }
        //Extra Input for EuroBond
        //Rev 2.0 Row: 977
        public string OrderStatus { get; set; }
        //End of Rev 2.0 Row: 977
        public List<OrderSignaturelist> product_list { get; set; }
    }
    public class OrderSignaturelist
    {
        public string id { get; set; }
        public string product_name { get; set; }
        public string qty { get; set; }
        public string rate { get; set; }
        public string total_price { get; set; }
        //Extra Input for EuroBond
        public decimal MRP { get; set; }
        //Extra Input for EuroBond
    }

    //Rev 1.0 Row: 957
    public class UploadShopRevisitAudioInput
    {
        public string data { get; set; }
        public HttpPostedFileBase audio { get; set; }
    }

    public class UploadRevisitAudio
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string shop_id { get; set; }
        public string visit_datetime { get; set; }
        public string revisitORvisit { get; set; }
        public string audio { get; set; }
    }

    public class UploadRevisitAudioOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }
    //End of Rev 1.0 Row: 957
}
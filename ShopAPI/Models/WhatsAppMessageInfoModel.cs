#region======================================Revision History=========================================================
//Written By : Debashis Talukder On 01/08/2023
//Purpose : Save & Fetch List Shop WhatsApp Message.Row: 861 to 862
#endregion===================================End of Revision History==================================================

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class WhatsAppMessageInfoSaveInput
    {
        public string user_id { get; set; }
        public List<ShopWhatsAppApiSave> shop_whatsapp_api_list { get; set; }
    }
    public class ShopWhatsAppApiSave
    {
        public string shop_id { get; set; }
        public string shop_name { get; set; }
        public string contactNo { get; set; }
        public bool isNewShop { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public bool isWhatsappSent { get; set; }
        public string whatsappSentMsg { get; set; }
    }
    public class WhatsAppMessageInfoSaveOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }
    public class WhatsAppMessageInfoListInput
    {
        public string user_id { get; set; }
        public string session_token { get; set; }        
    }
    public class WhatsAppMessageInfoListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string user_id { get; set; }
        public List<ShopWhatsAppApiList> shop_whatsapp_api_list { get; set; }
    }
    public class ShopWhatsAppApiList
    {
        public string shop_id { get; set; }
        public string shop_name { get; set; }
        public string contactNo { get; set; }
        public bool isNewShop { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public bool isWhatsappSent { get; set; }
        public string whatsappSentMsg { get; set; }
    }
}
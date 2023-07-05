#region======================================Revision History=========================================================
//Written By : Debashis Talukder On 01/06/2023
//Purpose : Save QR Code Image.
#endregion===================================End of Revision History==================================================
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class SaveQRCodeImageInput
    {
        public string data { get; set; }
        public HttpPostedFileBase attachments { get; set; }
    }
    public class SaveQRCodeImageInputDetails
    {
        public string user_id { get; set; }
        public string session_token { get; set; }
    }
    public class SaveQRCodeImageOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string qr_img_link { get; set; }
    }
    public class FetchQRCodeImageInput
    {
        public string user_id { get; set; }
        public string session_token { get; set; }
    }
    public class FetchQRCodeImageOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string qr_img_link { get; set; }
    }
    public class DeleteQRCodeImageInput
    {
        public string user_id { get; set; }
    }
    public class DeleteQRCodeImageOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }
}
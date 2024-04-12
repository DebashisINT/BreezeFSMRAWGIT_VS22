#region======================================Revision History=========================================================
//Written By : Debashis Talukder On 03/04/2024
//Purpose: For Send Auto Mail.Row: 913
#endregion===================================End of Revision History==================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class SendAutoMailInput
    {
        public string session_token { get; set; }
    }

    public class SendAutoMailOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string automail_sending_email { get; set; }
        public string automail_sending_pass { get; set; }
        public string recipient_email_ids { get; set; }
    }
}
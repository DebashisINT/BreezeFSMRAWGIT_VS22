using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class ModelNotification
    {

        public string id { get; set; }
        public string notificationmessage { get; set; }

        public DateTime date_time { get; set; }

        public bool isUnreadNotificationPresent { get; set; }

    }
    public class ModelNotificationInput
    {
        public string user_id { get; set; }
        public string session_token { get; set; }
    }
    public class ModelNotificationOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<ModelNotification> notification_list { get; set; }

    }
    public class ModelNotificationOutputUnread
    {
        public string status { get; set; }
        public string message { get; set; }
        public bool isUnreadNotificationPresent { get; set; }

    }
}
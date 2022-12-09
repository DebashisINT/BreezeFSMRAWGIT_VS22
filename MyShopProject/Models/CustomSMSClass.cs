using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class CustomSMSClass
    {
        public List<UserNotificationList> UserList { get; set; }
        public List<SupervisorList> SupervisorList { get; set; }
        public List<StateList> StateList { get; set; }
    }
}
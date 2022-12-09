using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class EmployeeHiarchyModel
    {

        public string UserID { get; set; }
        public string username { get; set; }
        public string UserIDParent { get; set; }
        public string usernamereport { get; set; }

        public string shopcount { get; set; }
    }
}
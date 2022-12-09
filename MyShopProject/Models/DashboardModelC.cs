using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class DashboardModelC
    {
        public int shopcount { get; set; }
        public int usercount { get; set; }
        public int activeusercount { get; set; }
        public int Nonactiveuser { get; set; }

        public string Userid { get; set; }

        public string employeecount { get; set; }
      
        public string employeeonleave { get; set; }
        public string employeeatwork { get; set; }
        public string employeenotlogin { get; set; }
    }
}
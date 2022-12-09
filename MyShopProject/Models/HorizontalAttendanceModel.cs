using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class HorizontalAttendanceModel
    {
        public String Fromdate { get; set; }
        public String Todate { get; set; }
        public List<string> ProductID { get; set; }
        public List<string> empcode { get; set; }
        public String is_pageload { get; set; }
        public String PageName { get; set; }

        public String TotalKMTravelled { get; set; }
        public String SecondarySalesValue { get; set; }
        public String IdleTimeCount { get; set; }
        public String ShowAttendanceSelfie { get; set; }

        //Mantise work 0025111
        public int ShowFullday { get; set; }
        public int ShowLoginLocation { get; set; }
        public int ShowLogoutLocation { get; set; }
        //End of Mantise work 0025111
    }
}
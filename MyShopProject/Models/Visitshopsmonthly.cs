using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{

    public class VisitshpsDate
    {
        public string Date { get; set; }
    }
    public class VisitshopReportInput
    {
        public int Month { get; set; }
        public string Year { get; set; }
    }

    public class MonthlyVisitshopReportOutput
    {
        public List<VisitReport> visitshop { get; set; }
        public List<Dateormatsmontwiseshop> dates { get; set; }
        //public List<Userformats> users { get; set; }

    }
    public class VisitReport
    {
        public string UserName { get; set; }
        public string ReportName { get; set; }
        public string UserLoginID { get; set; }

        public int User_Id { get; set; }
        public string Designation { get; set; }
        public int Date_1 { get; set; }
        public int Date_2 { get; set; }
        public int Date_3 { get; set; }
        public int Date_4 { get; set; }
        public int Date_5 { get; set; }
        public int Date_6 { get; set; }
               
        public int Date_7 { get; set; }
        public int Date_8 { get; set; }
        public int Date_9 { get; set; }
        public int Date_10 { get; set; }
               
        public int Date_11 { get; set; }
        public int Date_12 { get; set; }
        public int Date_13 { get; set; }
        public int Date_14 { get; set; }
        public int Date_15 { get; set; }
        public int Date_16 { get; set; }
        public int Date_17 { get; set; }
        public int Date_18 { get; set; }
        public int Date_19 { get; set; }
        public int Date_20 { get; set; }
        public int Date_21 { get; set; }
               
        public int Date_22 { get; set; }
        public int Date_23 { get; set; }
        public int Date_24 { get; set; }
        public int Date_25 { get; set; }
        public int Date_26 { get; set; }
        public int Date_27 { get; set; }
               
        public int Date_28 { get; set; }
        public int Date_29 { get; set; }
        public int Date_30 { get; set; }
        public int Date_31 { get; set; }

        public int TotaldShopvisitCal { get; set; }
    }

    public class Dateormatsmontwiseshop
    {
        public string datevisit { get; set; }
    }

    public class Userformatsshops
    {
        public int user_id { get; set; }

    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models

{


    public class DistanceDate
    {
        public string Date { get; set; }
    }
    public class DistanceReportInput
    {

        public int Month { get; set; }

        public string  Year { get; set; }
    }

    public class DistanceReportOutput
    {

        public List<DistanceReport> distance { get; set; }
        public List<Dateormatsmontwise> dates { get; set; }
        //public List<Userformats> users { get; set; }

    }
    public class DistanceReport
    {
        public string UserName { get; set; }
        public string ReportName { get; set; }
        public int User_Id { get; set; }
        public string UserLoginID { get; set; }
        public string Designation { get; set; }
        public decimal Date_1 { get; set; }
        public decimal Date_2 { get; set; }
        public decimal Date_3 { get; set; }
        public decimal Date_4 { get; set; }
        public decimal Date_5 { get; set; }
        public decimal Date_6 { get; set; }

        public decimal Date_7 { get; set; }
        public decimal Date_8 { get; set; }
        public decimal Date_9 { get; set; }
        public decimal Date_10 { get; set; }

        public decimal Date_11 { get; set; }
        public decimal Date_12 { get; set; }
        public decimal Date_13 { get; set; }
        public decimal Date_14 { get; set; }
        public decimal Date_15 { get; set; }
        public decimal Date_16 { get; set; }
        public decimal Date_17 { get; set; }
        public decimal Date_18 { get; set; }
        public decimal Date_19 { get; set; }
        public decimal Date_20 { get; set; }
        public decimal Date_21 { get; set; }

        public decimal Date_22 { get; set; }
        public decimal Date_23 { get; set; }
        public decimal Date_24 { get; set; }
        public decimal Date_25 { get; set; }
        public decimal Date_26 { get; set; }
        public decimal Date_27 { get; set; }

        public decimal Date_28 { get; set; }
        public decimal Date_29 { get; set; }
        public decimal Date_30 { get; set; }
        public decimal Date_31 { get; set; }
        public decimal Totaldistancecal { get; set; }
    }

    public class Dateormatsmontwise
    {
        public string datevisit { get; set; }

    }

    public class Userformats
    {
        public int user_id { get; set; }

    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class PJPListModel
    {
        public string selectedusrid { get; set; }
        public string Is_PageLoad { get; set; }
        public string Fromdate { get; set; }
        public string Todate { get; set; }
        public int Uniquecont { get; set; }
        public List<string> empcode { get; set; }
        public List<string> StateId { get; set; }
    }

    public class PJPImportLog
    {
        public String Date { get; set; }
        public String From_Time { get; set; }
        public String To_Time { get; set; }
        public String Employee { get; set; }
        public String Shop_Name { get; set; }
        public String Shop_Contact { get; set; }
        public String City { get; set; }

        public String Area { get; set; }
        public String Remarks { get; set; }
        public String STATUS { get; set; }
        public String STATUS_MESSAGE { get; set; }

        public String State { get; set; }
        public String Shop_Type { get; set; }
        public String LAT { get; set; }
        public String LONG { get; set; }
        public String Radius { get; set; }
    }

    public class PJPEmployeeListModel
    {
       
        public List<string> AreaId { get; set; }

        public List<string> EmpId { get; set; }
    }
}
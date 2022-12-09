using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class ReportPaitentOrderRegisterModel
    {
    }

    public class ReportPaitentOrderRegisterInput
    {
        public string selectedusrid { get; set; }
        public List<string> StateId { get; set; }
        public List<GetUserName> userlsit { get; set; }
        public List<GetUsersStates> states { get; set; }
        public List<string> empcode { get; set; }
        public string Is_PageLoad { get; set; }
        public List<string> shopId { get; set; }
        public List<Getmasterstock> shoplist { get; set; }
        public string Fromdate { get; set; }
        public string Todate { get; set; }
        public int Uniquecont { get; set; }
        public int IsSchemeDetails { get; set; }
    }

    public class ReportPaitentOrderRegisterOutput
    {
        public string Fromdate { get; set; }
        public string Todate { get; set; }
        public int Uniquecont { get; set; }
    }
}
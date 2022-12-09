using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Models;
using System.Web.Mvc;

namespace Models
{
    public class AttedancewisePerformanceReport
    {
        public List<string> StateId { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public List<string> desgid { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public List<string> empcode { get; set; }

        public List<GetUsersStates> states { get; set; }

        public List<GetDesignation> designation { get; set; }

        public List<GetAllEmployee> employee { get; set; }

        public string is_pageload { get; set; }
        public List<AttenMonths> MonthList { get; set; }

        public String Month { get; set; }
        public String EmployeeCode { get; set; }
        public Int32 is_procfirst { get; set; }

        public String Year { get; set; }
        public List<Years> yearList { get; set; }
    }

    public class AttenMonths
    {
        public string MID { get; set; }
        public string AttenMonthName { get; set; }
    }
}
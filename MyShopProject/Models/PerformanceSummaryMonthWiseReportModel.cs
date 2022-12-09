using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Models;
using System.Web.Mvc;

namespace Models
{
    public class PerformanceSummaryMonthWiseReportModel
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
        public List<PerformanceMonth> MonthList { get; set; }

        public String Month { get; set; }
        public String EmployeeCode { get; set; }
        public Int32 is_procfirst { get; set; }

        public String Year { get; set; }
        public List<Years> YearList { get; set; }
    }

    public class PerformanceMonth
    {
        public string PID { get; set; }
        public string PMonthName { get; set; }
    }
}
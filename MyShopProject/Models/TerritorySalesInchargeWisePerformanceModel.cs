using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Models;
using System.Web.Mvc;

namespace Models
{
    public class TerritorySalesInchargeWisePerformanceModel
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
        public List<TDDPMonth> MonthList { get; set; }

        public String Month { get; set; }
        public String EmployeeCode { get; set; }
        public Int32 is_procfirst { get; set; }

        public String Year { get; set; }
        public List<TDDPYears> YearList { get; set; }
    }

    public class TDDPYears
    {
        public string TID { get; set; }
        public string TYearName { get; set; }
    }

    public class TDDPMonth
    {
        public string TMID { get; set; }
        public string TMonthName { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Models;
using System.Web.Mvc;

namespace Models
{
    public class DealerDistributorWisePerformanceModel
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
        public List<DDPMonth> MonthList { get; set; }

        public String Month { get; set; }
        public String EmployeeCode { get; set; }
        public Int32 is_procfirst { get; set; }

        public String Year { get; set; }
        public List<DDPYears> YearList { get; set; }
    }

    public class DDPYears
    {
        public string ID { get; set; }
        public string YearName { get; set; }
    }

    public class DDPMonth
    {
        public string MID { get; set; }
        public string MonthName { get; set; }
    }
}
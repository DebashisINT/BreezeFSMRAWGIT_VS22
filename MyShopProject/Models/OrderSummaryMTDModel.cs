/****************************************************************************************************************************************
    Written by Sanchita  for    V2.0.41      06-07-2023       A new report is required in FSM as "Order Summary(MTD) 
                                                              Mantis: 26429
 ***************************************************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Models;
using System.Web.Mvc;

namespace Models
{
    public class OrderSummaryMTDModel
    {
        public List<string> StateId { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public List<string> desgid { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]

        public List<GetUsersStates> states { get; set; }

        public List<GetDesignation> designation { get; set; }


        public string is_pageload { get; set; }
        public List<OrderSumMTD> MonthList { get; set; }

        public String Month { get; set; }
        public Int32 is_procfirst { get; set; }

        public String Year { get; set; }
        public List<Years> YearList { get; set; }

        public List<string> empcode { get; set; }
        public List<GetAllEmployee> employee { get; set; }
    }

    public class OrderSumMTD
    {
        public string PID { get; set; }
        public string PMonthName { get; set; }
    }

    //public class GetAllEmployee
    //{
    //    public string empcode { get; set; }

    //    public string empname { get; set; }
    //}
}
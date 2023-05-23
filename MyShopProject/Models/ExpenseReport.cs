/*********************************************************************************************************
 * Created by Sanchita for V2.0.40 on 04-05-2023. Work done in Controller, View and Model
 * A New Expense Report is Required for BP Poddar. Refer: 25833
 * *******************************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class ExpenseReport
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string FromDate { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ToDate { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Is_PageLoad { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string EmployeeCode { get; set; }

        public DateTime dtFromDate { get; set; }
        public DateTime dtToDate { get; set; }

        public class GetHQName
        {
            public string HQid { get; set; }

            public string HQname { get; set; }
        }

        public class GetExpenseType
        {
            public string expid { get; set; }

            public string expense_type { get; set; }
        }
    }
}
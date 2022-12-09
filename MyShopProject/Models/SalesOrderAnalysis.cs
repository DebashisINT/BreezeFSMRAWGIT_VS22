using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class SalesOrderAnalysis
    {

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string FromDate { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ToDate { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string UserID { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Is_PageLoad { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string EmployeeCode { get; set; }
    }
}
﻿using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class DSVisitDetailsReport
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Fromdate { get; set; }



        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Todate { get; set; }


        public List<string> BranchId { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public List<string> desgid { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public List<string> empcode { get; set; }

        public List<GetDesignation> designation { get; set; }

        public List<GetAllEmployee> employee { get; set; }

        public List<GetBranch> modelbranch = new List<GetBranch>();

        public string is_pageload { get; set; }
    }

    public class DSVisitDetailsModel
    {
        public string USERID { get; set; }
        public string SEQ { get; set; }
        public string EMPROWID { get; set; }
        public string LOGINDATE { get; set; }
        public string LOGIN_DATETIME { get; set; }
        public string BRANCH_ID { get; set; }
        public string BRANCHDESC { get; set; }
        public decimal EMPUSERID { get; set; }
        public decimal EMPCODE { get; set; }
        public string EMPID { get; set; }
        public string EMPNAME { get; set; }
        public string STATEID { get; set; }
        public string STATE { get; set; }
        public string DEG_ID { get; set; }
        public string DESIGNATION { get; set; }
        public string DATEOFJOINING { get; set; }
        public string CONTACTNO { get; set; }
        public decimal REPORTTOID { get; set; }
        public decimal REPORTTOUID { get; set; }
        public decimal REPORTTO { get; set; }
        public string RPTTODESG { get; set; }
        public string HREPORTTOID { get; set; }
        public string HREPORTTOUID { get; set; }
        public string HREPORTTO { get; set; }
        public string HRPTTODESG { get; set; }
        public string OUTLETSMAPPED { get; set; }
        public string NEWSHOP_VISITED { get; set; }
        public string RE_VISITED { get; set; }
        public string TOTAL_VISIT { get; set; }
        public string DISTANCE_TRAVELLED { get; set; }
        public string AVGTIMESPENTINMARKET { get; set; }
        public string AVGSPENTDURATION { get; set; }

    }
}
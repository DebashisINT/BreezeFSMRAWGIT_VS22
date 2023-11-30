//====================================================== Revision History ===================================================
//Rev Number DATE              VERSION          DEVELOPER           CHANGES
//Written by Sanchita on 23-11-2023 for V2.2.43    A new report required as Enquiry Analytics.Mantis: 27021 
//====================================================== Revision History =================================================== 

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class EnquiryAnalyticsModel
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Fromdate { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Todate { get; set; }

        public List<GetEnquiryFrom> EnquiryFrom { get; set; }
        public List<string> EnquiryFromDesc { get; set; }
        public string Is_PageLoad { get; set; }

        public Int64 TotalPendingEnquiry { get; set; }
        public Int64 TotalInProcessEnquiry { get; set; }
        public Int64 TotalNotInterestedEnquiry { get; set; }
        public Int64 TotalAssignedEnquiry { get; set; }
        public Int64 TotalReassignedEnquiry { get; set; }
        public Int64 TotalHighRiskEnquiry { get; set; }

    }
}
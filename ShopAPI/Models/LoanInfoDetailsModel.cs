#region======================================Revision History=========================================================
//Written By : Debashis Talukder On 15/11/2024
//Purpose: Loan Info Details.Refer: Row: 1000,1001,1002,1003 & 1004
#endregion===================================End of Revision History==================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class LoanRiskTypeListInput
    {
        public long user_id { get; set; }
    }
    public class LoanRiskTypeListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<LRlistOutput> data_list { get; set; }
    }
    public class LRlistOutput
    {
        public long id { get; set; }
        public string name { get; set; }
    }

    public class LoanDispositionListInput
    {
        public long user_id { get; set; }
    }
    public class LoanDispositionListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<LDlistOutput> data_list { get; set; }
    }
    public class LDlistOutput
    {
        public long id { get; set; }
        public string name { get; set; }
    }

    public class LoanFinalStatusListInput
    {
        public long user_id { get; set; }
    }
    public class LoanFinalStatusListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<LFSlistOutput> data_list { get; set; }
    }
    public class LFSlistOutput
    {
        public long id { get; set; }
        public string name { get; set; }
    }

    public class LoanDetailFetchInput
    {
        public long user_id { get; set; }
        public string shop_id { get; set; }
    }

    public class LoanDetailFetchOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<LoanlistOutput> data_list { get; set; }        
    }

    public class LoanlistOutput
    {
        public string shop_id { get; set; }
        public string bkt { get; set; }
        public decimal total_outstanding { get; set; }
        public decimal pos { get; set; }
        public decimal emi_amt { get; set; }
        public decimal all_charges { get; set; }
        public decimal total_Collectable { get; set; }
        public long risk_id { get; set; }
        public string risk_name { get; set; }
        public string workable { get; set; }
        public long disposition_code_id { get; set; }
        public string disposition_code_name { get; set; }
        public string ptp_Date { get; set; }
        public decimal ptp_amt { get; set; }
        public string collection_date { get; set; }
        public decimal collection_amount { get; set; }
        public long final_status_id { get; set; }
        public string final_status_name { get; set; }
    }

    public class LoanDetailUpdateInput
    {
        public long user_id { get; set; }
        public string shop_id { get; set; }
        public long risk_id { get; set; }
        public string risk_name { get; set; }
        public string workable { get; set; }
        public long disposition_code_id { get; set; }
        public string disposition_code_name { get; set; }
        public string ptp_Date { get; set; }
        public decimal ptp_amt { get; set; }
        public string collection_date { get; set; }
        public decimal collection_amount { get; set; }
        public long final_status_id { get; set; }
        public string final_status_name { get; set; }
    }
    public class LoanDetailUpdateOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }
}
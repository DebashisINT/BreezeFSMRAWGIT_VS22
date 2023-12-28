#region======================================Revision History=========================================================
//Written By : Debashis Talukder On 05/12/2023
//Purpose : Save & Fetch List CRM Contact.Row: 880 to 884 & 898
#endregion===================================End of Revision History==================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class CRMCompanyListInput
    {
        public string session_token { get; set; }
    }

    public class CRMCompanyListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<CRMCompanyList> company_list { get; set; }
    }

    public class CRMCompanyList
    {
        public int company_id { get; set; }
        public string company_name { get; set; }
    }

    public class CRMTypeListInput
    {
        public string session_token { get; set; }
    }

    public class CRMTypeListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<CRMTypeList> type_list { get; set; }
    }

    public class CRMTypeList
    {
        public int type_id { get; set; }
        public string type_name { get; set; }
    }

    public class CRMStatusListInput
    {
        public string session_token { get; set; }
    }

    public class CRMStatusListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<CRMStatusList> status_list { get; set; }
    }

    public class CRMStatusList
    {
        public int status_id { get; set; }
        public string status_name { get; set; }
    }

    public class CRMSourceListInput
    {
        public string session_token { get; set; }
    }

    public class CRMSourceListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<CRMSourceList> source_list { get; set; }
    }

    public class CRMSourceList
    {
        public int source_id { get; set; }
        public string source_name { get; set; }
    }

    public class CRMStageListInput
    {
        public string session_token { get; set; }
    }

    public class CRMStageListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<CRMStageList> stage_list { get; set; }
    }

    public class CRMStageList
    {
        public int stage_id { get; set; }
        public string stage_name { get; set; }
    }

    public class CRMCompanySaveInput
    {
        public string session_token { get; set; }
        public int created_by { get; set; }
        public List<Companynamelist> company_name_list { get; set; }
    }

    public class Companynamelist
    {
        public string company_name { get; set; }
    }

    public class CRMCompanySaveOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }
}
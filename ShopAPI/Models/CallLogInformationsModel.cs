#region======================================Revision History=========================================================
//Written By : Debashis Talukder On 18/12/2023
//Purpose : Save & Fetch List Call Log.Row: 888 to 889
#endregion===================================End of Revision History==================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class CallLogListSaveInput
    {
        public int user_id { get; set; }
        public List<Callhislist> call_his_list { get; set; }
    }
    public class Callhislist
    {
        public string shop_id { get; set; }
        public string call_number { get; set; }
        public string call_date { get; set; }
        public string call_time { get; set; }
        public string call_date_time { get; set; }
        public string call_type { get; set; }
        public string call_duration_sec { get; set; }
        public string call_duration { get; set; }
    }
    public class CallLogListSaveOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }
    public class CallLogListInput
    {
        public int user_id { get; set; }
    }
    public class CallLogListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public int user_id { get; set; }
        public List<CallhislistOutput> call_his_list { get; set; }
    }
    public class CallhislistOutput
    {
        public string shop_id { get; set; }
        public string call_number { get; set; }
        public string call_date { get; set; }
        public string call_time { get; set; }
        public DateTime? call_date_time { get; set; }
        public string call_type { get; set; }
        public string call_duration_sec { get; set; }
        public string call_duration { get; set; }
        public bool isUploaded { get; set; }
    }
}
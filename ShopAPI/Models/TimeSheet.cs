using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{

    public class TimeSheet
    {
        public string id { get; set; }
        public string time { get; set; }
        public string client_id { get; set; }
        public string client_name { get; set; }

        public string product_id { get; set; }
        public string product_name { get; set; }
        public string project_id { get; set; }
        public string project_name { get; set; }
        public string date { get; set; }
        public string activity_id { get; set; }
        public string activity_name { get; set; }
        public string comments { get; set; }
        public bool isUpdateable { get; set; }
        public bool timesheet_status { get; set; }
        public string image { get; set; }


    }

    public class TimeSheetList
    {
        public string status { get; set; }
        public string message { get; set; }
        public string superviser_name { get; set; }
        public string total_hrs { get; set; }

        public List<TimeSheet> timesheet_list { get; set; }
    }

    public class inputTimeSheet
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string date { get; set; }
        public string timesheet_id { get; set; }
        public string isAdd { get; set; }
    }
    public class TimeSheetConfig
    {
        public string status { get; set; }
        public string message { get; set; }
        public string timesheet_past_days { get; set; }
        public string supervisor_name { get; set; }
        public string client_text { get; set; }
        public string project_text { get; set; }
        public string product_text { get; set; }
        public string activity_text { get; set; }
        public string time_text { get; set; }
        public string comment_text { get; set; }
        public string submit_text { get; set; }
    }

    public class client
    {
        public string client_id { get; set; }
        public string client_name { get; set; }
    }
    public class project
    {
        public string project_id { get; set; }
        public string project_name { get; set; }
    }

    public class activity
    {
        public string activity_id { get; set; }
        public string activity_name { get; set; }
    }

    public class product
    {
        public string product_id { get; set; }
        public string product_name { get; set; }
    }

    public class getDropDownList
    {
        public string status { get; set; }
        public string message { get; set; }

        public List<product> product_list { get; set; }
        public List<client> client_list { get; set; }
        public List<project> project_list { get; set; }
        public List<activity> activity_list { get; set; }
    }

    public class TimeSheetEntryMultipart
    {
        public HttpPostedFileBase image { get; set; }
        public string data { get; set; }
    }
    public class TimeSheetEntry
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string time { get; set; }
        public string client_id { get; set; }
        public string product_id { get; set; }
        public string project_id { get; set; }
        public string date { get; set; }
        public string activity_id { get; set; }
        public string comments { get; set; }
        public string timesheet_id { get; set; }
    }

    public class addEditSuccess
    {
        public string status { get; set; }
        public string message { get; set; }
        public string timesheet_status { get; set; }

    }

}
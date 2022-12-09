using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class TaskDeleteInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string id { get; set; }
    }

    public class TaskUpdateInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string id { get; set; }
        public string isCompleted { get; set; }
    }

    public class TaskListInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string date { get; set; }
    }

    public class TaskListOutput
    {
        public string status { get; set; }
        public string message { get; set; }

        public List<TaskList> task_list { get; set; }
    }


    public class TaskList
    {
        public string id { get; set; }
        public string date { get; set; }
        public string task { get; set; }
        public string details { get; set; }
        public string isCompleted { get; set; }
        //Rev Add extra parameter 06-11-2020
        public string eventID { get; set; }
        //End Of Rev Add extra parameter 06-11-2020

    }


   
    public class TaskAddInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string id { get; set; }
        public string date { get; set; }
        public string task { get; set; }
        public string details { get; set; }
        public string isCompleted { get; set; }
        //Rev Add extra parameter 06-11-2020
        public string eventID { get; set; }
        //End Of Rev Add extra parameter 06-11-2020
    }
    public class TaskAddOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }
}
#region======================================Revision History=========================================================
//1.0   V2.0.39     Debashis    16/05/2023      Some new methods have been added.Row: 828 to 832
#endregion===================================End of Revision History==================================================
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

    //Rev 1.0 Row: 828,829,830,831,832
    public class TaskPriorityListInput
    {
        public string session_token { get; set; }
    }
    public class TaskPriorityListOutput
    {
        public string status { get; set; }
        public string message { get; set; }

        public List<TaskPriorityList> task_priority_list { get; set; }
    }
    public class TaskPriorityList
    {
        public long task_priority_id { get; set; }
        public string task_priority_name { get; set; }
    }

    public class TaskPriorityWiseListInput
    {
        public string session_token { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }
        public string task_priority_id { get; set; }
        public string task_priority_name { get; set;}
        public long user_id { get; set; }
    }
    public class TaskPriorityWiseListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string task_priority_name { get; set; }
        public string task_priority_id { get; set; }
        public long user_id { get; set; }
        public List<TaskPriorityDetList> task_dtls_list { get; set; }
    }
    public class TaskPriorityDetList
    {
        public long task_id { get; set; }
        public string task_name { get; set; }
        public string task_details { get; set; }
        public string due_date { get; set; }
        public string due_time { get; set; }
        public string start_date { get; set; }
        public string start_time { get; set; }
        public string priority_type_name { get; set; }
        public string status { get; set; }
    }

    public class AddTaskDetailListInput
    {
        public long user_id { get; set; }
        public long task_id { get; set; }
        public DateTime task_date { get; set;}
        public string task_time { get; set; }
        public string task_status { get; set; }
        public string task_details { get; set; }
        public string other_remarks { get; set; }
        public DateTime task_next_date { get; set; }
    }
    public class AddTaskDetailListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class EditTaskDetailListInput
    {
        public long user_id { get; set; }
        public long task_status_id { get; set; }
        public long task_id { get; set; }
        public DateTime task_date { get; set; }
        public string task_time { get; set; }
        public string task_status { get; set; }
        public string task_details { get; set; }
        public string other_remarks { get; set; }
        public DateTime task_next_date { get; set; }
    }
    public class EditTaskDetailListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }
    public class GetTaskDetailListInput
    {
        public long task_id { get; set; }
    }
    public class GetTaskDetailListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public long task_id { get; set; }
        public List<TaskStatusDetailList> task_status_dtls_list { get; set; }
    }
    public class TaskStatusDetailList
    {
        public long task_status_id { get; set; }
        public string task_date { get; set; }
        public string task_time { get; set; }
        public string task_status { get; set; }
        public string task_details { get; set; }
        public string other_remarks { get; set; }
        public string task_next_date { get; set; }
        public bool isactive_status { get; set; }
    }
    //End of Rev 1.0 Row: 828,829,830,831,832
}
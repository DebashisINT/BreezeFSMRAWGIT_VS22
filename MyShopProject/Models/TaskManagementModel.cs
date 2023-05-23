#region======================================Revision History=========================================================================
//Page Create By : PRITI On  09/05/2023 for V2.0.40 Refer:
//0026031:Copy the current Enquiry page, and create a duplicate and name it as 'Task Management'
//0026032: Customization in Task Management Page
//0026034: Customization in Add Task Page
#endregion===================================End of Revision History==================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class TaskManagementModel
    {
        public List<SalesmanUserAssignTM> SalesmanUserList;

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Fromdate { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Todate { get; set; }

        public List<GetTaskPriority> TaskPriorityFrom { get; set; }
        public List<string> TaskPriorityDesc { get; set; }
        public string Is_PageLoad { get; set; }

        // Add New Enquiries
        public string TASK_ID { get; set; }
        public string Action_type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }

        public string _StartDate { get; set; }
        public string _DueDate { get; set; }
        public string Task_Name { get; set; }        
        public string Priority { get; set; }       
        public string Task_Details { get; set; }
        public string SalesmanUserId { get; internal set; }
        public string response_code { get; set; }
        public string response_msg { get; set; }
        public Int32 SalesmanUser { get; set; }

    }


    public class GetTaskPriority
    {
        public string TASKPRIORITY_ID { get; set; }
        public string TaskPriorityDesc { get; set; }
    }
    public class SalesmanUserAssignTM
    {
        public string UserID { get; set; }
        public string username { get; set; }
    }



}


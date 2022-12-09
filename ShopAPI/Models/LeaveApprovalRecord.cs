using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class LeaveApprovalRecord
    {
        public string user_id { get; set; }
        public string session_token { get; set; }
        public string leave_type { get; set; }
        public string leave_to_date { get; set; }
        public string leave_reason { get; set; }
        public string leave_from_date { get; set; }
        public string leave_long { get; set; }
        public string leave_lat { get; set; }

        public string leave_add { get; set; }

    }

    //Rev Debashis
    public class LeaveForApprovalStatusInput
    {
        public string user_id { get; set; }
        public string current_status { get; set; }
        public string Approve_User { get; set; }
        public string approval_date_time { get; set; }
        public string approver_remarks { get; set; }
        public string applied_date_time { get; set; }
    }

    public class LeaveForApprovalStatusOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class UserLeaveListInput
    {        
        public string user_id_leave_applied { get; set; }
        public string session_token { get; set; }
    }

    public class UserLeaveListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string user_id_leave_applied { get; set; }
        public string user_name_leave_applied { get; set; }
        public List<UserLeaveList> leave_list { get; set; }
    }

    public class UserLeaveList
    {
        public string applied_date { get; set; }
        public DateTime? applied_date_time { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }
        public string leave_type { get; set; }
        public bool approve_status { get; set; }
        public bool reject_status { get; set; }
        public string leave_reason { get; set; }
        public DateTime? approval_date_time { get; set; }
        public string approver_remarks { get; set; }
    }
    //End of Rev Debashis
}
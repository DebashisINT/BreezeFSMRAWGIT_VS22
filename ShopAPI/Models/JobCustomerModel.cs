using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class JobCustomerModel
    {
        [Required]
        public string session_token { get; set; }
        [Required]
        public string user_id { get; set; }
        public string date { get; set; }
    }

    public class JobCustomerOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<JobCustomerList> job_list { get; set; }
    }

    public class JobCustomerList
    {
        public long id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string contact_person { get; set; }
        public string contact_no { get; set; }
        public string status { get; set; }
        public string service_for { get; set; }
        public decimal total_service { get; set; }
        public string service_frequency { get; set; }
        public decimal total_service_commited { get; set; }
        public decimal total_service_pending { get; set; }

        public string job_code { get; set; }
        public Boolean isShowUpdateStatus { get; set; }
        public string last_service_committed { get; set; }
    }

    public class GetStatusInput
    {
        [Required]
        public string session_token { get; set; }
        [Required]
        public string user_id { get; set; }
        [Required]
        public string id { get; set; }
    }

    public class GetStatusOutPut
    {
        public string status { get; set; }
        public string message { get; set; }
        public string job_status { get; set; }
        public string last_status { get; set; }
    }

    public class GetWipSettingsOutPut
    {
        public string status { get; set; }
        public string message { get; set; }
        public string uom_text { get; set; }
        public string service_due_for { get; set; }
    }

    public class JobCustomerAssignJobInput
    {
        public string session_token { get; set; }
        //[Required]
        public string job_id { get; set; }
        //[Required]
        public string shop_code { get; set; }
        //[Required]
        public string user_id { get; set; }
        //[Required]
        public string status { get; set; }
        //[Required]
        public string service_for { get; set; }
        //[Required]
        public string service_due_for { get; set; }
        //[Required]
        public string UOM { get; set; }
        //[Required]
        public string total_service { get; set; }
        //[Required]
        public string service_frequency { get; set; }
        //[Required]
        public string total_service_commited { get; set; }
        //[Required]
        public string total_service_pending { get; set; }
        //[Required]
        public string jobcreate_date { get; set; }
        //[Required]
        public string create_user { get; set; }

        public string sub_userid { get; set; }
        //[Required]
        public string job_code { get; set; }
    }

    public class AssignJobOutPut
    {
        public string status { get; set; }
        public string message { get; set; }
        public string id { get; set; }
    }

    public class WorkInProgressInput
    {
        [Required]
        public string session_token { get; set; }
        [Required]
        public string user_id { get; set; }
        [Required]
        public string job_id { get; set; }
        [Required]
        public string start_date { get; set; }
        [Required]
        public string start_time { get; set; }
        public string service_due { get; set; }
        public string service_completed { get; set; }
        public string next_date { get; set; }
        public string next_time { get; set; }
        public string remarks { get; set; }
        public string date_time { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string address { get; set; }

        public string fsm_id { get; set; }
    }

    public class WorkOnHoldInput
    {
        [Required]
        public string session_token { get; set; }
        [Required]
        public string user_id { get; set; }
        [Required]
        public string job_id { get; set; }
        [Required]
        public string hold_date { get; set; }
        [Required]
        public string hold_time { get; set; }
        [Required]
        public string reason_hold { get; set; }

        public string remarks { get; set; }
        [Required]
        public string date_time { get; set; }
        [Required]
        public string latitude { get; set; }
        [Required]
        public string longitude { get; set; }
        [Required]
        public string address { get; set; }

        public string fsm_id { get; set; }
    }

    public class WorkOnCompletedInput
    {
        [Required]
        public string session_token { get; set; }
        [Required]
        public string user_id { get; set; }
        [Required]
        public string job_id { get; set; }
        [Required]
        public string finish_date { get; set; }
        [Required]
        public string finish_time { get; set; }

        public string remarks { get; set; }
        [Required]
        public string date_time { get; set; }
        [Required]
        public string latitude { get; set; }
        [Required]
        public string longitude { get; set; }
        [Required]
        public string address { get; set; }
        public string phone_no { get; set; }

        public string fsm_id { get; set; }
    }

    public class WorkCancelledInput
    {
        [Required]
        public string session_token { get; set; }
        [Required]
        public string user_id { get; set; }
        [Required]
        public string job_id { get; set; }
        [Required]
        public string date { get; set; }
        [Required]
        public string time { get; set; }
        [Required]
        public string cancel_reason { get; set; }

        public string remarks { get; set; }
        [Required]
        public string date_time { get; set; }
        [Required]
        public string latitude { get; set; }
        [Required]
        public string longitude { get; set; }
        [Required]
        public string address { get; set; }

        public string cancelled_by { get; set; }

        public string user { get; set; }

        public string fsm_id { get; set; }
    }

    public class ReviewInput
    {
        [Required]
        public string session_token { get; set; }
        [Required]
        public string user_id { get; set; }
        [Required]
        public string job_id { get; set; }
        [Required]
        public string review { get; set; }
        [Required]
        public string rate { get; set; }
        [Required]
        public string date_time { get; set; }
        [Required]
        public string latitude { get; set; }
        [Required]
        public string longitude { get; set; }
        [Required]
        public string address { get; set; }

        public string fsm_id { get; set; }
    }

    public class CustomerJobStatusAttachment
    {
        public string data { get; set; }
        public List<HttpPostedFileBase> attachments { get; set; }
    }

    public class CustomerJobStatusImagees
    {
        public string attachment { get; set; }
    }

    public class CompletedSetiings
    {
        [Required]
        public string session_token { get; set; }
        [Required]
        public string user_id { get; set; }
        [Required]
        public string id { get; set; }
    }

    public class CompletedSetiingsOutPut
    {
        public string status { get; set; }
        public string message { get; set; }
        public bool isAttachmentMandatory { get; set; }
        public string phone_no { get; set; }
    }

    public class CustomerlistInput
    {
        [Required]
        public string session_token { get; set; }
        [Required]
        public string user_id { get; set; }
    }

    public class CustomerListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<UserCustomerList> customer_list { get; set; }
    }

    public class UserCustomerList
    {
        public string id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string contact_person { get; set; }
        public string contact_no { get; set; }
        public string service_for { get; set; }
        public decimal total_service { get; set; }
        public string service_frequency { get; set; }
        public decimal total_service_commited { get; set; }
        public decimal total_service_pending { get; set; }
        public string last_service_committed { get; set; }
    }

    public class WorkUnholdInput
    {
        [Required]
        public string session_token { get; set; }
        [Required]
        public string user_id { get; set; }
        [Required]
        public string job_id { get; set; }
        [Required]
        public string unhold_date { get; set; }
        [Required]
        public string unhold_time { get; set; }
        [Required]
        public string reason_unhold { get; set; }
        public string remarks { get; set; }
        [Required]
        public DateTime date_time { get; set; }
        [Required]
        public string latitude { get; set; }
        [Required]
        public string longitude { get; set; }
        [Required]
        public string address { get; set; }

        public string fsm_id { get; set; }
    }

    public class Jobhistory
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<Jobhistorylist> history_list { get; set; }
    }

    public class Jobhistorylist
    {
        public long id { get; set; }
        public DateTime schedule_date_time { get; set; }
        public string job_code { get; set; }
        public string service_for { get; set; }
        public string area { get; set; }
        public string team { get; set; }
        public string status { get; set; }
        public DateTime? start_date_time { get; set; }
        public string service_due_for { get; set; }
        public string service_completed_for { get; set; }
        public DateTime? next_date_time { get; set; }
        public string wip_remarks { get; set; }
        public string wip_attachment { get; set; }
        public string wip_photo { get; set; }
        public DateTime? hold_date_time { get; set; }
        public string hold_reason { get; set; }
        public string hold_remarks { get; set; }
        public string hold_attachment { get; set; }
        public string hold_photo { get; set; }
        public DateTime? complete_date_time { get; set; }
        public string complete_remarks { get; set; }
        public string complete_attachment { get; set; }
        public string complete_photo { get; set; }
        public DateTime? cancelled_date_time { get; set; }
        public string cancel_reason { get; set; }
        public string cancel_remarks { get; set; }
        public string cancel_attachment { get; set; }
        public string cancel_photo { get; set; }
        public string review_details { get; set; }
        public string review_attachment { get; set; }
        public string review_photo { get; set; }
        public decimal ratings { get; set; }
        public string uom_text { get; set; }
    }

    public class JobhistoryInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
    }

    public class UpdateAssignJobInput
    {
        public string job_id { get; set; }
        public string date { get; set; }
        public string technician_id { get; set; }
        public string subtechnician_id { get; set; }
    }

    public class JobWisehistoryInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string id { get; set; }
    }

    public class SubmitWorkRescheduleInput
    {
        [Required]
        public string session_token { get; set; }
        [Required]
        public string user_id { get; set; }
        [Required]
        public string job_id { get; set; }
        [Required]
        public string reschedule_date { get; set; }
        [Required]
        public string reschedule_time { get; set; }
        [Required]
        public string resc_reason { get; set; }
        public string remarks { get; set; }
        [Required]
        public DateTime date_time { get; set; }
        [Required]
        public string latitude { get; set; }
        [Required]
        public string longitude { get; set; }
        [Required]
        public string address { get; set; }

        public string fsm_id { get; set; }

        public string reschedule_by { get; set; }

        public string user { get; set; }
    }
}
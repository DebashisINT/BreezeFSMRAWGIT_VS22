using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class ActivityModel
    {
        [Required]
        public String session_token { get;set;}
        [Required]
        public String user_id { get; set; }
    }

    public class ActivityDropdownOutput
    {
        public String status { get; set; }
        public String message { get; set; }
        public List<Activities> activity_list { get; set; }
    }

    public class Activities
    {
        public String id { get; set; }
        public String name { get; set; }
        public String activityId { get; set; }
    }

    public class ActivityTypeListOutput
    {
        public String status { get; set; }
        public String message { get; set; }
        public List<Activities> type_list { get; set; }
    }

    public class ActivityPriorityListOutput
    {
        public String status { get; set; }
        public String message { get; set; }
        public List<Activities> priority_list { get; set; }
    }

    public class ActivityProductListOutput
    {
        public String status { get; set; }
        public String message { get; set; }
        public List<Activities> product_list { get; set; }
    }

    public class ActivityAddInput
    {
        [Required]
        public String session_token { get; set; }
        [Required]
        public String user_id { get; set; }
        public String id { get; set; }
        public String party_id { get; set; }
        public String date { get; set; }
        public String time { get; set; }
        public String name { get; set; }
        public String activity_id { get; set; }
        public String type_id { get; set; }
        public String product_id { get; set; }
        public String subject { get; set; }
        public String details { get; set; }
        public String duration { get; set; }
        public String priority_id { get; set; }
        public String due_date { get; set; }
        public String due_time { get; set; }
    }

    public class ActivityAddOutput
    {
        public String status { get; set; }
        public String message { get; set; }
        public string id { get; set; }
    }

    public class ActivityListOutput
    {
        public String status { get; set; }
        public String message { get; set; }
        public List<ActivityList> activity_list { get; set; }
    }

    public class ActivityList
    {
        public String id { get; set; }
        public String party_id { get; set; }
        public String date { get; set; }
        public String time { get; set; }
        public String name { get; set; }
        public String activity_id { get; set; }
        public String type_id { get; set; }
        public String product_id { get; set; }
        public String subject { get; set; }
        public String details { get; set; }
        public String duration { get; set; }
        public String priority_id { get; set; }
        public String due_date { get; set; }
        public String due_time { get; set; }
        public String attachments { get; set; }
        public String image { get; set; }
    }

    public class ActivityAddAttachmentInput
    {
        public string data { get; set; }
        public List<HttpPostedFileBase> attachments { get; set; }
    }

    public class Imagees
    {
        public string attachment { get; set; }
        public string attachmenttype { get; set; }
    }
}
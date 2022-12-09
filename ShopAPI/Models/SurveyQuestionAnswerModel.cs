using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{    
    public class SurveyQuestionListInput
    {
        public string session_token { get; set; }
    }

    public class SurveyQuestionListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<SurveyQuestionList> Question_list { get; set; }
    }

    public class SurveyQuestionList
    {
        public string question_id { get; set; }
        public string question_desc { get; set; }
        public string question_type { get; set; }
        public string question_value { get; set; }
        public long question_for_shoptype_id { get; set; }
        public string group_name { get; set; }
    }

    public class SurveyAnswerListSaveInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string shop_id { get; set; }
        public string survey_id { get; set; }
        public DateTime date_time { get; set; }
        public string group_name { get; set; }
        public long question_for_shoptype_id { get; set; }
        public List<AnswerListInput> answer_list { get; set; }
    }

    public class AnswerListInput
    {
        public string question_id { get; set; }
        public string answer { get; set; }
    }

    public class SurveyAnswerListSaveOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class SurveyQAImageInput
    {
        public string data { get; set; }
        public HttpPostedFileBase attachments { get; set; }
    }

    public class SurveyQAImageOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class SurveyQAImageInputDetails
    {
        [Required]
        public string user_id { get; set; }
        public string session_token { get; set; }
        [Required]
        public long question_id { get; set; }
        [Required]
        public string survey_id { get; set; }
    }

    public class SurveyQADetailsListInput
    {
        public string session_token { get; set; }
        [Required]
        public string user_id { get; set; }
        [Required]
        public string shop_id { get; set; }
    }

    public class SurveyQADetailsListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string user_id { get; set; }
        public string shop_id { get; set; }
        public List<SurveyDetailList> survey_list { get; set; }
    }

    public class SurveyDetailList
    {
        public string survey_id { get; set; }
        public DateTime saved_date_time { get; set; }
        public long question_for_shoptype_id { get; set; }
        public string group_name { get; set; }
        public List<SurveyQuestionAnsDetailList> question_ans_list { get; set; }
    }

    public class SurveyQuestionAnsDetailList
    {
        public long question_id { get; set; }
        public string question_desc { get; set; }
        public string answer { get; set; }
        public string image_link { get; set; }
    }

    public class SurveyDeleteInputModel
    {
        [Required]
        public string user_id { get; set; }
        [Required]
        public string survey_id { get; set; }
    }

    public class SurveyDeleteOutputModel
    {
        public string status { get; set; }
        public string message { get; set; }
    }
}
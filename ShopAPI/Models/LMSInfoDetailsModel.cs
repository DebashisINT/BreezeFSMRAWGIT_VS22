#region======================================Revision History=========================================================
//Written By : Debashis Talukder On 02/07/2024
//Purpose: LMS Info Details.Row: 945,947,948,949,950,952,953,955,956,971,972,973,974,975,988,989 & 995
#endregion===================================End of Revision History==================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class TopicListInput
    {
        public long user_id { get; set; }
    }

    public class TopicListOutputModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public long user_id { get; set; }
        public List<TopiclistOutput> topic_list { get; set; }
    }

    public class TopiclistOutput
    {
        public long topic_id { get; set; }
        public string topic_name { get; set; }
        public int video_count { get; set; }
        public int no_of_pending_video_count { get; set; }
        public int topic_parcentage { get; set; }
        public long topic_sequence { get; set; }
    }

    public class TopicWiseListsInput
    {
        public long topic_id { get; set; }
        public long user_id { get; set; }
    }

    public class TopicWiseListsOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public long topic_id { get; set; }
        public string topic_name { get; set; }
        public List<ContentlistOutput> content_list { get; set; }
    }

    public class ContentlistOutput
    {
        public long content_id { get; set; }
        public string content_url { get; set; }
        public string content_thumbnail { get; set; }
        public string content_title { get; set; }
        public string content_description { get; set; }
        public long content_play_sequence { get; set; }
        public bool isAllowLike { get; set; }
        public bool isAllowComment { get; set; }
        public bool isAllowShare { get; set; }
        public int no_of_comment { get; set; }
        public bool like_flag { get; set; }
        public int share_count { get; set; }
        public string content_length { get; set; }
        public string content_watch_length { get; set; }
        public bool content_watch_completed { get; set; }
        public string content_last_view_date_time { get; set; }
        public string WatchStartTime { get; set; }
        public string WatchEndTime { get; set; }
        public string WatchedDuration { get; set; }
        public string Timestamp { get; set; }
        public string DeviceType { get; set; }
        public string Operating_System { get; set; }
        public string Location { get; set; }
        public string PlaybackSpeed { get; set; }
        public int Watch_Percentage { get; set; }
        public int QuizAttemptsNo { get; set; }
        public decimal QuizScores { get; set; }
        public bool CompletionStatus { get; set; }
        public string CONTENT_QUIZTIME { get; set; }
        public string isBookmarked { get; set; }
        public List<QuestionlistOutput> question_list { get; set; }
    }

    public class QuestionlistOutput
    {
        public long topic_id { get; set; }
        public long content_id { get; set; }
        public long question_id { get; set; }
        public string question { get; set; }
        public string question_description { get; set; }
        public List<OptionlistOutput> option_list { get; set; }
    }

    public class OptionlistOutput
    {
        public long question_id { get; set; }
        public long option_id { get; set; }
        public string option_no_1 { get; set; }
        public long option_point_1 { get; set; }
        public bool isCorrect_1 { get; set; }
        public string option_no_2 { get; set; }
        public long option_point_2 { get; set; }
        public bool isCorrect_2 { get; set; }
        public string option_no_3 { get; set; }
        public long option_point_3 { get; set; }
        public bool isCorrect_3 { get; set; }
        public string option_no_4 { get; set; }
        public long option_point_4 { get; set; }
        public bool isCorrect_4 { get; set; }
    }

    public class UserWiseLMSModulesInfoSaveInput
    {
        public long user_id { get; set; }
        public List<Userlmsinfolists> user_lms_info_list { get; set; }
    }

    public class Userlmsinfolists
    {
        public string module_name { get; set; }
        public int count_of_use { get; set; }
        public string time_spend { get; set; }
        public string last_current_loc_lat { get; set; }
        public string last_current_loc_long { get; set; }
        public string last_current_loc_address { get; set; }
        public string date_time { get; set; }
        public string phone_model { get; set; }
    }
    public class UserWiseLMSModulesInfoSaveOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class TopicContentDetailsSaveInput
    {
        public long user_id { get; set; }
        public long topic_id { get; set; }
        public string topic_name { get; set; }
        public long content_id { get; set; }
        public bool like_flag { get; set; }
        public int share_count { get; set; }
        public string content_length { get; set; }
        public string content_watch_length { get; set; }
        public bool content_watch_completed { get; set; }
        public string content_last_view_date_time { get; set; }
        public string content_watch_start_date { get; set; }
        public string WatchStartTime { get; set; }
        public string content_watch_end_date { get; set; }
        public string WatchEndTime { get; set; }
        public string WatchedDuration { get; set; }
        public string Timestamp { get; set; }
        public string DeviceType { get; set; }
        public string Operating_System { get; set; }
        public string Location { get; set; }
        public string PlaybackSpeed { get; set; }
        public int Watch_Percentage { get; set; }
        public int QuizAttemptsNo { get; set; }
        public decimal QuizScores { get; set; }
        public bool CompletionStatus { get; set; }
        public List<Commentlists> comment_list { get; set; }
    }

    public class Commentlists
    {
        public long topic_id { get; set; }
        public long content_id { get; set; }
        public long commented_user_id { get; set; }
        public string commented_user_name { get; set; }
        public string comment_id { get; set; }
        public string comment_description { get; set; }
        public string comment_date_time { get; set; }
    }

    public class TopicContentDetailsSaveOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class LearningContentListsInput
    {
        public long user_id { get; set; }
    }

    public class LearningContentListsOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public long user_id { get; set; }
        public List<LearningContentInfolistOutput> learning_content_info_list { get; set; }
    }

    public class LearningContentInfolistOutput
    {
        public long topic_id { get; set; }
        public string topic_name { get; set; }
        public long content_id { get; set; }
        public string content_url { get; set; }
        public string content_title { get; set; }
        public string content_description { get; set; }
        public string content_length { get; set; }
        public string content_watch_length { get; set; }
        public bool content_watch_completed { get; set; }
        public string content_last_view_date_time { get; set; }
        public string WatchStartTime { get; set; }
        public string WatchEndTime { get; set; }
        public string WatchedDuration { get; set; }
        public string Timestamp { get; set; }
        public string DeviceType { get; set; }
        public string Operating_System { get; set; }
        public string Location { get; set; }
        public string PlaybackSpeed { get; set; }
        public string Watch_Percentage { get; set; }
        public int QuizAttemptsNo { get; set; }
        public decimal QuizScores { get; set; }
        public bool CompletionStatus { get; set; }
        public string content_thumbnail { get; set; }
    }

    public class CommentListInput
    {
        public long topic_id { get; set; }
        public long content_id { get; set; }
    }

    public class CommentListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<CommentlistsOutput> comment_list { get; set; }
    }

    public class CommentlistsOutput
    {
        public long topic_id { get; set; }
        public long content_id { get; set; }
        public string comment_id { get; set; }
        public string comment_description { get; set; }
        public string comment_date_time { get; set; }
        public long commented_user_id { get; set; }
        public string commented_user_name { get; set; }
    }

    public class ContentCountSaveInput
    {
        public long user_id { get; set; }
        public string save_date { get; set; }
        public int like_count { get; set; }
        public int share_count { get; set; }
        public int comment_count { get; set; }
        public int correct_answer_count { get; set; }
        public int wrong_answer_count { get; set; }
        public int content_watch_count { get; set; }
    }

    public class ContentCountSaveOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class TopicContentWiseQASaveInput
    {
        public long user_id { get; set; }
        public List<QAsavelist> question_answer_save_list { get; set; }
    }

    public class QAsavelist
    {
        public long topic_id { get; set; }
        public string topic_name { get; set; }
        public long content_id { get; set; }
        public long question_id { get; set; }
        public string question { get; set; }
        public long option_id { get; set; }
        public string option_number { get; set; }
        public long option_point { get; set; }
        public bool isCorrect { get; set; }
        public bool completionStatus { get; set; }
    }

    public class TopicContentWiseQASaveOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }    

    public class LMSLeaderboardOverallListInput
    {
        public long user_id { get; set; }
        public string branchwise { get; set; }
        public string flag { get; set; }
    }

    public class LMSLeaderboardOverallListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<LeaderboardOveralluserlist> user_list { get; set; }
    }

    public class LeaderboardOveralluserlist
    {
        public long user_id { get; set; }
        public string user_name { get; set; }
        public string user_phone { get; set; }
        public int watch { get; set; }
        public int like { get; set; }
        public int comment { get; set; }
        public int share { get; set; }
        public int correct_answer { get; set; }
        public Int32 position { get; set; }
        public int totalscore { get; set; }
        public string profile_pictures_url { get; set; }
    }

    public class LMSLeaderboardOwnListInput
    {
        public long user_id { get; set; }
        public string branchwise { get; set; }
        public string flag { get; set; }
    }

    public class LMSLeaderboardOwnListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public long user_id { get; set; }
        public string user_name { get; set; }
        public string user_phone { get; set; }
        public int watch { get; set; }
        public int like { get; set; }
        public int comment { get; set; }
        public int share { get; set; }
        public int correct_answer { get; set; }
        public Int32 position { get; set; }
        public int totalscore { get; set; }
        public string profile_pictures_url { get; set; }
    }

    public class LMSSectionsPointsListInput
    {
        public string session_token { get; set; }
    }

    public class LMSSectionsPointsListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public int content_watch_point { get; set; }
        public int content_like_point { get; set; }
        public int content_share_point { get; set; }
        public int content_comment_point { get; set; }
    }

    public class LMSSaveBookMarkInput
    {
        public long user_id { get; set; }
        public long topic_id { get; set; }
        public string topic_name { get; set; }
        public long content_id { get; set; }
        public string content_name { get; set; }
        public string content_desc { get; set; }
        public string content_bitmap { get; set; }
        public string content_url { get; set; }
        public string addBookmark { get; set; }
    }

    public class LMSSaveBookMarkOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class LMSFetchBookMarkInput
    {
        public long user_id { get; set; }
    }

    public class LMSFetchBookMarkOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<LMSBookMarkOutputList> bookmark_list { get; set; }
    }

    public class LMSBookMarkOutputList
    {
        public long topic_id { get; set; }
        public string topic_name { get; set; }
        public long content_id { get; set; }
        public string content_name { get; set; }
        public string content_desc { get; set; }
        public string content_bitmap { get; set; }
        public string content_url { get; set; }
    }

    public class TopicContentWiseAnswerListsInput
    {
        public long user_id { get; set; }
        public long topic_id { get; set; }
        public long content_id { get; set; }
    }
    public class TopicContentWiseAnswerListsOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public long user_id { get; set; }
        public long topic_id { get; set; }
        public string topic_name { get; set; }
        public long content_id { get; set; }
        public string content_name { get; set; }
        public List<QuestionAnswerfetchlistOutput> question_answer_fetch_list { get; set; }
    }
    public class QuestionAnswerfetchlistOutput
    {
        public long topic_id { get; set; }
        public long content_id { get; set; }
        public long question_id { get; set; }
        public string question { get; set; }
        public string question_description { get; set; }
        public string answered { get; set; }
        public bool isCorrectAnswer { get; set; }
        public List<QAOptionlistOutput> option_list { get; set; }
    }
    public class QAOptionlistOutput
    {
        public long question_id { get; set; }
        public long option_id { get; set; }
        public string option_no_1 { get; set; }
        public long option_point_1 { get; set; }
        public bool isCorrect_1 { get; set; }
        public string option_no_2 { get; set; }
        public long option_point_2 { get; set; }
        public bool isCorrect_2 { get; set; }
        public string option_no_3 { get; set; }
        public long option_point_3 { get; set; }
        public bool isCorrect_3 { get; set; }
        public string option_no_4 { get; set; }
        public long option_point_4 { get; set; }
        public bool isCorrect_4 { get; set; }
    }
    public class TopicContentWiseAnswerUpdateInput
    {
        public long user_id { get; set; }
        public string session_token { get; set; }
        public long topic_id { get; set; }
        public string topic_name { get; set; }
        public long content_id { get; set; }
        public long question_id { get; set; }
        public string question { get; set; }
        public long option_id { get; set; }
        public string option_number { get; set; }
        public long option_point { get; set; }
        public bool isCorrect { get; set; }
    }
    public class TopicContentWiseAnswerUpdateOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class UserWiseAPPCrashDetailsSaveInput
    {
        public long user_id { get; set; }
        public List<Userappcrashinfolists> crash_report_save_list { get; set; }
    }

    public class Userappcrashinfolists
    {
        public string errorMessage { get; set; }
        public string stackTrace { get; set; }
        public string date_time { get; set; }
        public string device { get; set; }
        public string os_version { get; set; }
        public string app_version { get; set; }
        public string user_remarks { get; set; }
    }
    public class UserWiseAPPCrashDetailsSaveOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }
}
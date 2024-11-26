#region======================================Revision History=========================================================
//Written By : Debashis Talukder On 02/07/2024
//Purpose: LMS Info Details.Row: 945,947,948,949,950,952,953,955,956,971,972,973,974,975,988,989 & 995
#endregion===================================End of Revision History==================================================

using Newtonsoft.Json;
using ShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using System.Web;

namespace ShopAPI.Controllers
{
    public class LMSInfoDetailsController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage TopicLists(TopicListInput model)
        {
            TopicListOutputModel omodel = new TopicListOutputModel();
            List<TopiclistOutput> Tview = new List<TopiclistOutput>();

            try
            {
                if (!ModelState.IsValid)
                {
                    omodel.status = "213";
                    omodel.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
                }
                else
                {
                    DataSet ds = new DataSet();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_FSMLMSINFODETAILS", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "TOPICLISTS");
                    sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(ds);
                    sqlcon.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Successfully Get List.";
                        Tview = APIHelperMethods.ToModelList<TopiclistOutput>(ds.Tables[0]);
                        omodel.user_id = model.user_id;
                        omodel.topic_list = Tview;
                    }
                    var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                    return message;
                }
            }
            catch (Exception ex)
            {
                omodel.status = "204";
                omodel.message = ex.Message;
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage TopicWiseLists(TopicWiseListsInput model)
        {
            TopicWiseListsOutput omodel = new TopicWiseListsOutput();
            List<ContentlistOutput> Coview = new List<ContentlistOutput>();
            int MultiContent=0;

            try
            {
                if (!ModelState.IsValid)
                {
                    omodel.status = "213";
                    omodel.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
                }
                else
                {
                    String AttachmentUrl = System.Configuration.ConfigurationManager.AppSettings["DocumentAttachment"];
                    DataSet ds = new DataSet();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_FSMLMSINFODETAILS", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "TOPICWISELISTS");
                    sqlcmd.Parameters.AddWithValue("@TOPIC_ID", model.topic_id);
                    sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@AttachmentUrl", AttachmentUrl);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(ds);
                    sqlcon.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                            {
                                //List<ContentlistOutput> Coview = new List<ContentlistOutput>();
                                List<QuestionlistOutput> Qoview = new List<QuestionlistOutput>();
                                MultiContent = 1;
                                for (int k = 0; k < ds.Tables[2].Rows.Count; k++)
                                {
                                    //List<QuestionlistOutput> Qoview = new List<QuestionlistOutput>();
                                    List<OptionlistOutput> Ooview = new List<OptionlistOutput>();
                                    for (int l = 0; l < ds.Tables[3].Rows.Count; l++)
                                    {
                                        //List<OptionlistOutput> Ooview = new List<OptionlistOutput>();
                                        if (Convert.ToInt64(ds.Tables[3].Rows[l]["topic_id"]) == Convert.ToInt64(ds.Tables[1].Rows[j]["topic_id"]) &&
                                            Convert.ToInt64(ds.Tables[3].Rows[l]["content_id"]) == Convert.ToInt64(ds.Tables[2].Rows[k]["content_id"])
                                            &&
                                            Convert.ToInt64(ds.Tables[3].Rows[l]["question_id"]) == Convert.ToInt64(ds.Tables[2].Rows[k]["question_id"])
                                            )
                                        {
                                            Ooview.Add(new OptionlistOutput()
                                            {
                                                question_id = Convert.ToInt64(ds.Tables[3].Rows[l]["question_id"]),
                                                option_id = Convert.ToInt64(ds.Tables[3].Rows[l]["option_id"]),
                                                option_no_1 = Convert.ToString(ds.Tables[3].Rows[l]["option_no_1"]),
                                                option_point_1 = Convert.ToInt64(ds.Tables[3].Rows[l]["option_point_1"]),
                                                isCorrect_1 = Convert.ToBoolean(ds.Tables[3].Rows[l]["isCorrect_1"]),
                                                option_no_2 = Convert.ToString(ds.Tables[3].Rows[l]["option_no_2"]),
                                                option_point_2 = Convert.ToInt64(ds.Tables[3].Rows[l]["option_point_2"]),
                                                isCorrect_2 = Convert.ToBoolean(ds.Tables[3].Rows[l]["isCorrect_2"]),
                                                option_no_3 = Convert.ToString(ds.Tables[3].Rows[l]["option_no_3"]),
                                                option_point_3 = Convert.ToInt64(ds.Tables[3].Rows[l]["option_point_3"]),
                                                isCorrect_3 = Convert.ToBoolean(ds.Tables[3].Rows[l]["isCorrect_3"]),
                                                option_no_4 = Convert.ToString(ds.Tables[3].Rows[l]["option_no_4"]),
                                                option_point_4 = Convert.ToInt64(ds.Tables[3].Rows[l]["option_point_4"]),
                                                isCorrect_4 = Convert.ToBoolean(ds.Tables[3].Rows[l]["isCorrect_4"])
                                            });
                                        }
                                    }
                                        if (Convert.ToInt64(ds.Tables[1].Rows[j]["topic_id"]) == Convert.ToInt64(ds.Tables[2].Rows[k]["topic_id"]) &&
                                        Convert.ToInt64(ds.Tables[1].Rows[j]["content_id"]) == Convert.ToInt64(ds.Tables[2].Rows[k]["content_id"])
                                        //&&
                                        //Convert.ToInt64(ds.Tables[3].Rows[l]["question_id"]) == Convert.ToInt64(ds.Tables[2].Rows[k]["question_id"])
                                        )
                                        {
                                            Qoview.Add(new QuestionlistOutput()
                                            {
                                                topic_id = Convert.ToInt64(ds.Tables[2].Rows[k]["topic_id"]),
                                                content_id = Convert.ToInt64(ds.Tables[2].Rows[k]["content_id"]),
                                                question_id = Convert.ToInt64(ds.Tables[2].Rows[k]["question_id"]),
                                                question = Convert.ToString(ds.Tables[2].Rows[k]["question"]),
                                                question_description = Convert.ToString(ds.Tables[2].Rows[k]["question_description"]),
                                                //question_id = Convert.ToInt64(ds.Tables[3].Rows[l]["question_id"]),
                                                //question = Convert.ToString(ds.Tables[3].Rows[l]["question"]),
                                                //question_description = Convert.ToString(ds.Tables[3].Rows[l]["question_description"]),
                                                option_list = Ooview
                                            });
                                        }
                                    }
                                    if (Convert.ToInt64(ds.Tables[0].Rows[i]["topic_id"]) == Convert.ToInt64(ds.Tables[1].Rows[j]["topic_id"]) &&
                                        MultiContent==1)
                                    {
                                        Coview.Add(new ContentlistOutput()
                                        {
                                            content_id = Convert.ToInt64(ds.Tables[1].Rows[j]["content_id"]),
                                            //content_url= AttachmentUrl+Convert.ToString(ds.Tables[1].Rows[j]["content_url"]),
                                            content_url = Convert.ToString(ds.Tables[1].Rows[j]["content_url"]),
                                            content_thumbnail = Convert.ToString(ds.Tables[1].Rows[j]["content_thumbnail"]),
                                            content_title = Convert.ToString(ds.Tables[1].Rows[j]["content_title"]),
                                            content_description = Convert.ToString(ds.Tables[1].Rows[j]["content_description"]),
                                            content_play_sequence = Convert.ToInt64(ds.Tables[1].Rows[j]["content_play_sequence"]),
                                            isAllowLike = Convert.ToBoolean(ds.Tables[1].Rows[j]["isAllowLike"]),
                                            isAllowComment = Convert.ToBoolean(ds.Tables[1].Rows[j]["isAllowComment"]),
                                            isAllowShare = Convert.ToBoolean(ds.Tables[1].Rows[j]["isAllowShare"]),
                                            no_of_comment = Convert.ToInt32(ds.Tables[1].Rows[j]["no_of_comment"]),
                                            like_flag = Convert.ToBoolean(ds.Tables[1].Rows[j]["like_flag"]),
                                            share_count = Convert.ToInt32(ds.Tables[1].Rows[j]["share_count"]),
                                            content_length= Convert.ToString(ds.Tables[1].Rows[j]["content_length"]),
                                            content_watch_length = Convert.ToString(ds.Tables[1].Rows[j]["content_watch_length"]),
                                            content_watch_completed = Convert.ToBoolean(ds.Tables[1].Rows[j]["content_watch_completed"]),
                                            content_last_view_date_time = Convert.ToString(ds.Tables[1].Rows[j]["content_last_view_date_time"]),
                                            WatchStartTime = Convert.ToString(ds.Tables[1].Rows[j]["WatchStartTime"]),
                                            WatchEndTime = Convert.ToString(ds.Tables[1].Rows[j]["WatchEndTime"]),
                                            WatchedDuration = Convert.ToString(ds.Tables[1].Rows[j]["WatchedDuration"]),
                                            Timestamp = Convert.ToString(ds.Tables[1].Rows[j]["Timestamp"]),
                                            DeviceType = Convert.ToString(ds.Tables[1].Rows[j]["DeviceType"]),
                                            Operating_System = Convert.ToString(ds.Tables[1].Rows[j]["Operating_System"]),
                                            Location = Convert.ToString(ds.Tables[1].Rows[j]["Location"]),
                                            PlaybackSpeed = Convert.ToString(ds.Tables[1].Rows[j]["PlaybackSpeed"]),
                                            Watch_Percentage = Convert.ToInt32(ds.Tables[1].Rows[j]["Watch_Percentage"]),
                                            QuizAttemptsNo = Convert.ToInt32(ds.Tables[1].Rows[j]["QuizAttemptsNo"]),
                                            QuizScores = Convert.ToDecimal(ds.Tables[1].Rows[j]["QuizScores"]),
                                            CompletionStatus = Convert.ToBoolean(ds.Tables[1].Rows[j]["CompletionStatus"]),
                                            CONTENT_QUIZTIME = Convert.ToString(ds.Tables[1].Rows[j]["CONTENT_QUIZTIME"]),
                                            isBookmarked = Convert.ToString(ds.Tables[1].Rows[j]["isBookmarked"]),
                                            question_list = Qoview
                                        });
                                    }
                                    MultiContent = 0;
                                //}
                                if (ds.Tables[2].Rows.Count == 0 || ds.Tables[3].Rows.Count == 0)
                                {
                                    Coview = APIHelperMethods.ToModelList<ContentlistOutput>(ds.Tables[1]);
                                    omodel.content_list = Coview;
                                }
                                else
                                {
                                    omodel.content_list = Coview;
                                }
                            }
                        }

                        omodel.status = "200";
                        omodel.message = "Successfully Get List.";
                        omodel.topic_id = model.topic_id;
                        omodel.topic_name = Convert.ToString(ds.Tables[0].Rows[0]["topic_name"]);
                    }
                    var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                    return message;
                }
            }
            catch (Exception ex)
            {
                omodel.status = "204";
                omodel.message = ex.Message;
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage UserWiseLMSModulesInfo(UserWiseLMSModulesInfoSaveInput model)
        {
            UserWiseLMSModulesInfoSaveOutput omodel = new UserWiseLMSModulesInfoSaveOutput();

            try
            {
                if (!ModelState.IsValid)
                {
                    omodel.status = "213";
                    omodel.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
                }
                else
                {
                    List<Userlmsinfolists> omodel2 = new List<Userlmsinfolists>();
                    foreach (var s2 in model.user_lms_info_list)
                    {
                        omodel2.Add(new Userlmsinfolists()
                        {
                            module_name = s2.module_name,
                            count_of_use = s2.count_of_use,
                            time_spend = s2.time_spend,
                            last_current_loc_lat = s2.last_current_loc_lat,
                            last_current_loc_long = s2.last_current_loc_long,
                            last_current_loc_address = s2.last_current_loc_address,
                            date_time = s2.date_time,
                            phone_model = s2.phone_model
                        });
                    }

                    string JsonXML = XmlConversion.ConvertToXml(omodel2, 0);

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_FSMLMSINFODETAILS", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "MODULEINFOSAVE");
                    sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@JsonXML", JsonXML);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0 && Convert.ToInt64(dt.Rows[0][0]) == model.user_id)
                    {
                        omodel.status = "200";
                        omodel.message = "Information added successfully.";
                    }
                    else
                    {
                        omodel.status = "205";
                        omodel.message = "Data not Saved.";
                    }
                    var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                    return message;
                }
            }
            catch (Exception ex)
            {
                omodel.status = "204";
                omodel.message = ex.Message;
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage TopicContentDetailsSave(TopicContentDetailsSaveInput model)
        {
            TopicContentDetailsSaveOutput omodel = new TopicContentDetailsSaveOutput();
            try
            {
                if (!ModelState.IsValid)
                {
                    omodel.status = "213";
                    omodel.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
                }
                else
                {
                    List<Commentlists> omedl2 = new List<Commentlists>();
                    foreach (var s2 in model.comment_list)
                    {
                        omedl2.Add(new Commentlists()
                        {
                            topic_id = s2.topic_id,
                            content_id = s2.content_id,
                            commented_user_id = s2.commented_user_id,
                            commented_user_name = s2.commented_user_name,
                            comment_id = s2.comment_id,
                            comment_description = s2.comment_description,
                            comment_date_time = s2.comment_date_time
                        });
                    }

                    string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_FSMLMSINFODETAILS", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "TOPICCONTENTDETAILSSAVE");
                    sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@TOPIC_ID", model.topic_id);
                    sqlcmd.Parameters.AddWithValue("@TOPICNAME", model.topic_name);
                    sqlcmd.Parameters.AddWithValue("@CONTENT_ID", model.content_id);
                    sqlcmd.Parameters.AddWithValue("@ISLIKE", model.like_flag);
                    sqlcmd.Parameters.AddWithValue("@SHARE_COUNT", model.share_count);
                    sqlcmd.Parameters.AddWithValue("@CONTENT_LENGTH", model.content_length);
                    sqlcmd.Parameters.AddWithValue("@CONTENT_WATCH_LENGTH", model.content_watch_length);
                    sqlcmd.Parameters.AddWithValue("@ISCONTENTCOMPLETED", model.content_watch_completed);
                    sqlcmd.Parameters.AddWithValue("@CONTENTLASTVIEW", model.content_last_view_date_time);
                    sqlcmd.Parameters.AddWithValue("@WATCHSTARTDATE", model.content_watch_start_date);
                    sqlcmd.Parameters.AddWithValue("@WATCHSTARTTIME", model.WatchStartTime);
                    sqlcmd.Parameters.AddWithValue("@WATCHENDDATE", model.content_watch_end_date);
                    sqlcmd.Parameters.AddWithValue("@WATCHENDTIME", model.WatchEndTime);
                    sqlcmd.Parameters.AddWithValue("@WATCHEDDURATION", model.WatchedDuration);
                    sqlcmd.Parameters.AddWithValue("@WATCHTIMESTAMP", model.Timestamp);
                    sqlcmd.Parameters.AddWithValue("@DEVICETYPE", model.DeviceType);
                    sqlcmd.Parameters.AddWithValue("@OPERATING_SYSTEM", model.Operating_System);
                    sqlcmd.Parameters.AddWithValue("@WATCHLOCATION", model.Location);
                    sqlcmd.Parameters.AddWithValue("@PLAYBACKSPEED", model.PlaybackSpeed);
                    sqlcmd.Parameters.AddWithValue("@WATCH_PERCENTAGE", model.Watch_Percentage);
                    sqlcmd.Parameters.AddWithValue("@QUIZATTEMPTSNO", model.QuizAttemptsNo);
                    sqlcmd.Parameters.AddWithValue("@QUIZSCORES", model.QuizScores);
                    sqlcmd.Parameters.AddWithValue("@COMPLETIONSTATUS", model.CompletionStatus);
                    sqlcmd.Parameters.AddWithValue("@JsonXML", JsonXML);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Saved Successfully.";
                    }
                    var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                    return message;
                }
            }
            catch (Exception ex)
            {
                omodel.status = "204";
                omodel.message = ex.Message;
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage LearningContentLists(LearningContentListsInput model)
        {
            LearningContentListsOutput omodel = new LearningContentListsOutput();
            List<LearningContentInfolistOutput> Tview = new List<LearningContentInfolistOutput>();

            try
            {
                if (!ModelState.IsValid)
                {
                    omodel.status = "213";
                    omodel.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
                }
                else
                {
                    String AttachmentUrl = System.Configuration.ConfigurationManager.AppSettings["DocumentAttachment"];
                    DataSet ds = new DataSet();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_FSMLMSINFODETAILS", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "LEARNINGCONTENTLIST");
                    sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@AttachmentUrl", AttachmentUrl);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(ds);
                    sqlcon.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Successfully Get List.";
                        Tview = APIHelperMethods.ToModelList<LearningContentInfolistOutput>(ds.Tables[0]);
                        omodel.user_id = model.user_id;
                        omodel.learning_content_info_list = Tview;
                    }
                    var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                    return message;
                }
            }
            catch (Exception ex)
            {
                omodel.status = "204";
                omodel.message = ex.Message;
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage CommentLists(CommentListInput model)
        {
            CommentListOutput omodel = new CommentListOutput();
            List<CommentlistsOutput> Cview = new List<CommentlistsOutput>();

            try
            {
                if (!ModelState.IsValid)
                {
                    omodel.status = "213";
                    omodel.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
                }
                else
                {
                    DataSet ds = new DataSet();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_FSMLMSINFODETAILS", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "COMMENTLIST");
                    sqlcmd.Parameters.AddWithValue("@TOPIC_ID", model.topic_id);
                    sqlcmd.Parameters.AddWithValue("@CONTENT_ID", model.content_id);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(ds);
                    sqlcon.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Successfully Get List.";
                        Cview = APIHelperMethods.ToModelList<CommentlistsOutput>(ds.Tables[0]);
                        omodel.comment_list = Cview;
                    }
                    var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                    return message;
                }
            }
            catch (Exception ex)
            {
                omodel.status = "204";
                omodel.message = ex.Message;
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage ContentCountSave(ContentCountSaveInput model)
        {
            ContentCountSaveOutput omodel = new ContentCountSaveOutput();
            try
            {
                if (!ModelState.IsValid)
                {
                    omodel.status = "213";
                    omodel.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
                }
                else
                {
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_FSMLMSINFODETAILS", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "CONTENTCOUNTSAVE");
                    sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@CONTENTCOUNTSAVEDATE", model.save_date);
                    sqlcmd.Parameters.AddWithValue("@LIKECOUNT", model.like_count);
                    sqlcmd.Parameters.AddWithValue("@SHARE_COUNT", model.share_count);
                    sqlcmd.Parameters.AddWithValue("@COMMENTCOUNT", model.comment_count);
                    sqlcmd.Parameters.AddWithValue("@CORRECTANSCOUNT", model.correct_answer_count);
                    sqlcmd.Parameters.AddWithValue("@WRONGANSCOUNT", model.wrong_answer_count);
                    sqlcmd.Parameters.AddWithValue("@WATCHCOUNT", model.content_watch_count);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Saved Successfully.";
                    }
                    var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                    return message;
                }
            }
            catch (Exception ex)
            {
                omodel.status = "204";
                omodel.message = ex.Message;
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage TopicContentWiseQASave(TopicContentWiseQASaveInput model)
        {
            TopicContentWiseQASaveOutput omodel = new TopicContentWiseQASaveOutput();
            try
            {
                if (!ModelState.IsValid)
                {
                    omodel.status = "213";
                    omodel.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
                }
                else
                {
                    List<QAsavelist> omedl2 = new List<QAsavelist>();
                    foreach (var s2 in model.question_answer_save_list)
                    {
                        omedl2.Add(new QAsavelist()
                        {
                            topic_id = s2.topic_id,
                            topic_name = s2.topic_name,
                            content_id = s2.content_id,
                            question_id = s2.question_id,
                            question = s2.question,
                            option_id = s2.option_id,
                            option_number = s2.option_number,
                            option_point = s2.option_point,
                            isCorrect = s2.isCorrect,
                            completionStatus = s2.completionStatus
                        });
                    }

                    string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_FSMLMSINFODETAILS", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "TOPICCONTENTWISEQASAVE");
                    sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@JsonXML", JsonXML);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Saved Successfully.";
                    }
                    var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                    return message;
                }
            }
            catch (Exception ex)
            {
                omodel.status = "204";
                omodel.message = ex.Message;
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage LMSLeaderboardOverallList(LMSLeaderboardOverallListInput model)
        {
            LMSLeaderboardOverallListOutput omodel = new LMSLeaderboardOverallListOutput();
            List<LeaderboardOveralluserlist> oview = new List<LeaderboardOveralluserlist>();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                String ProfileImagePath = System.Configuration.ConfigurationManager.AppSettings["ProfileImageURL"];
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_FSMLMSINFODETAILS", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "LEADERBOARD");
                sqlcmd.Parameters.AddWithValue("@SUBACTION", "OVERALL");
                sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                sqlcmd.Parameters.AddWithValue("@BRANCHWISE", model.branchwise);
                sqlcmd.Parameters.AddWithValue("@ACTIVITYMODE", model.flag);
                sqlcmd.Parameters.AddWithValue("@PROFILEIMAGEPATH", ProfileImagePath);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<LeaderboardOveralluserlist>(dt);
                    omodel.status = "200";
                    omodel.message = "Success";
                    omodel.user_list = oview;
                }
                else
                {
                    omodel.status = "205";
                    omodel.message = "No data found";
                }

                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage LMSLeaderboardOwnList(LMSLeaderboardOwnListInput model)
        {
            LMSLeaderboardOwnListOutput omodel = new LMSLeaderboardOwnListOutput();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                String ProfileImagePath = System.Configuration.ConfigurationManager.AppSettings["ProfileImageURL"];
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_FSMLMSINFODETAILS", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "LEADERBOARD");
                sqlcmd.Parameters.AddWithValue("@SUBACTION", "OWN");
                sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                sqlcmd.Parameters.AddWithValue("@BRANCHWISE", model.branchwise);
                sqlcmd.Parameters.AddWithValue("@ACTIVITYMODE", model.flag);
                sqlcmd.Parameters.AddWithValue("@PROFILEIMAGEPATH", ProfileImagePath);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Success";
                    omodel.user_id = Convert.ToInt64(dt.Rows[0]["user_id"]);
                    omodel.user_name = Convert.ToString(dt.Rows[0]["user_name"]);
                    omodel.user_phone = Convert.ToString(dt.Rows[0]["user_phone"]);
                    omodel.watch = Convert.ToInt32(dt.Rows[0]["watch"]);
                    omodel.like = Convert.ToInt32(dt.Rows[0]["like"]);
                    omodel.comment = Convert.ToInt32(dt.Rows[0]["comment"]);
                    omodel.share = Convert.ToInt32(dt.Rows[0]["share"]);
                    omodel.correct_answer = Convert.ToInt32(dt.Rows[0]["correct_answer"]);
                    omodel.position = Convert.ToInt32(dt.Rows[0]["position"]);
                    omodel.totalscore = Convert.ToInt32(dt.Rows[0]["totalscore"]);
                    omodel.profile_pictures_url = Convert.ToString(dt.Rows[0]["profile_pictures_url"]);
                }
                else
                {
                    omodel.status = "205";
                    omodel.message = "No data found";
                }

                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage LMSSectionsPointsList(LMSSectionsPointsListInput model)
        {
            LMSSectionsPointsListOutput omodel = new LMSSectionsPointsListOutput();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                String ProfileImagePath = System.Configuration.ConfigurationManager.AppSettings["ProfileImageURL"];
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_FSMLMSINFODETAILS", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "SECTIONSPOINTS");

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Success";
                    omodel.content_watch_point = Convert.ToInt32(dt.Rows[0]["content_watch_point"]);
                    omodel.content_like_point = Convert.ToInt32(dt.Rows[0]["content_like_point"]);
                    omodel.content_share_point = Convert.ToInt32(dt.Rows[0]["content_share_point"]);
                    omodel.content_comment_point = Convert.ToInt32(dt.Rows[0]["content_comment_point"]);
                }
                else
                {
                    omodel.status = "205";
                    omodel.message = "No data found";
                }

                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage LMSSaveBookMark(LMSSaveBookMarkInput model)
        {
            LMSSaveBookMarkOutput omodel = new LMSSaveBookMarkOutput();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                String ProfileImagePath = System.Configuration.ConfigurationManager.AppSettings["ProfileImageURL"];
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_FSMLMSINFODETAILS", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "SAVEBOOKMARK");
                sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                sqlcmd.Parameters.AddWithValue("@TOPIC_ID", model.topic_id);
                sqlcmd.Parameters.AddWithValue("@TOPICNAME", model.topic_name);
                sqlcmd.Parameters.AddWithValue("@CONTENT_ID", model.content_id);
                sqlcmd.Parameters.AddWithValue("@CONTENTNAME", model.content_name);
                sqlcmd.Parameters.AddWithValue("@CONTENTDESC", model.content_desc);
                sqlcmd.Parameters.AddWithValue("@CONTENTBITMAP", model.content_bitmap);
                sqlcmd.Parameters.AddWithValue("@CONTENTURL", model.content_url);
                sqlcmd.Parameters.AddWithValue("@ADDBOOKMARK", model.addBookmark);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Success";
                }
                else
                {
                    omodel.status = "205";
                    omodel.message = "No data found";
                }

                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage LMSFetchBookMark(LMSFetchBookMarkInput model)
        {
            LMSFetchBookMarkOutput omodel = new LMSFetchBookMarkOutput();
            List<LMSBookMarkOutputList> oview = new List<LMSBookMarkOutputList>();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                String ProfileImagePath = System.Configuration.ConfigurationManager.AppSettings["ProfileImageURL"];
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_FSMLMSINFODETAILS", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "FETCHBOOKMARK");
                sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<LMSBookMarkOutputList>(dt);
                    omodel.status = "200";
                    omodel.message = "Success";
                    omodel.bookmark_list = oview;
                }
                else
                {
                    omodel.status = "205";
                    omodel.message = "No data found";
                }

                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage TopicContentWiseAnswerLists(TopicContentWiseAnswerListsInput model)
        {
            TopicContentWiseAnswerListsOutput omodel = new TopicContentWiseAnswerListsOutput();
            List<QuestionAnswerfetchlistOutput> Qoview = new List<QuestionAnswerfetchlistOutput>();

            try
            {
                if (!ModelState.IsValid)
                {
                    omodel.status = "213";
                    omodel.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
                }
                else
                {
                    DataSet ds = new DataSet();
                    string con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_FSMLMSINFODETAILS", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "TOPICCONTENTWISEANSWER");
                    sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@TOPIC_ID", model.topic_id);
                    sqlcmd.Parameters.AddWithValue("@CONTENT_ID", model.content_id);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(ds);
                    sqlcon.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                            {
                                List<QAOptionlistOutput> Ooview = new List<QAOptionlistOutput>();
                                for (int k = 0; k < ds.Tables[2].Rows.Count; k++)
                                {
                                    if (Convert.ToInt64(ds.Tables[2].Rows[k]["topic_id"]) == Convert.ToInt64(ds.Tables[1].Rows[j]["topic_id"]) &&
                                        Convert.ToInt64(ds.Tables[2].Rows[k]["content_id"]) == Convert.ToInt64(ds.Tables[1].Rows[j]["content_id"])
                                        &&
                                        Convert.ToInt64(ds.Tables[2].Rows[k]["question_id"]) == Convert.ToInt64(ds.Tables[1].Rows[j]["question_id"])
                                        )
                                    {
                                        Ooview.Add(new QAOptionlistOutput()
                                        {
                                            question_id = Convert.ToInt64(ds.Tables[2].Rows[k]["question_id"]),
                                            option_id = Convert.ToInt64(ds.Tables[2].Rows[k]["option_id"]),
                                            option_no_1 = Convert.ToString(ds.Tables[2].Rows[k]["option_no_1"]),
                                            option_point_1 = Convert.ToInt64(ds.Tables[2].Rows[k]["option_point_1"]),
                                            isCorrect_1 = Convert.ToBoolean(ds.Tables[2].Rows[k]["isCorrect_1"]),
                                            option_no_2 = Convert.ToString(ds.Tables[2].Rows[k]["option_no_2"]),
                                            option_point_2 = Convert.ToInt64(ds.Tables[2].Rows[k]["option_point_2"]),
                                            isCorrect_2 = Convert.ToBoolean(ds.Tables[2].Rows[k]["isCorrect_2"]),
                                            option_no_3 = Convert.ToString(ds.Tables[2].Rows[k]["option_no_3"]),
                                            option_point_3 = Convert.ToInt64(ds.Tables[2].Rows[k]["option_point_3"]),
                                            isCorrect_3 = Convert.ToBoolean(ds.Tables[2].Rows[k]["isCorrect_3"]),
                                            option_no_4 = Convert.ToString(ds.Tables[2].Rows[k]["option_no_4"]),
                                            option_point_4 = Convert.ToInt64(ds.Tables[2].Rows[k]["option_point_4"]),
                                            isCorrect_4 = Convert.ToBoolean(ds.Tables[2].Rows[k]["isCorrect_4"])
                                        });
                                    }
                                }
                                    if (Convert.ToInt64(ds.Tables[1].Rows[j]["topic_id"]) == Convert.ToInt64(ds.Tables[0].Rows[i]["topic_id"]) &&
                                    Convert.ToInt64(ds.Tables[1].Rows[j]["content_id"]) == Convert.ToInt64(ds.Tables[0].Rows[i]["content_id"])
                                    )
                                    {
                                        Qoview.Add(new QuestionAnswerfetchlistOutput()
                                        {
                                            topic_id = Convert.ToInt64(ds.Tables[1].Rows[j]["topic_id"]),
                                            content_id = Convert.ToInt64(ds.Tables[1].Rows[j]["content_id"]),
                                            question_id = Convert.ToInt64(ds.Tables[1].Rows[j]["question_id"]),
                                            question = Convert.ToString(ds.Tables[1].Rows[j]["question"]),
                                            question_description = Convert.ToString(ds.Tables[1].Rows[j]["question_description"]),
                                            answered = Convert.ToString(ds.Tables[1].Rows[j]["answered"]),
                                            isCorrectAnswer = Convert.ToBoolean(ds.Tables[1].Rows[j]["isCorrectAnswer"]),
                                            option_list = Ooview
                                        });
                                    }
                                }
                            }
                        }

                        omodel.status = "200";
                        omodel.message = "Successfully Get List.";
                        omodel.question_answer_fetch_list = Qoview;
                        omodel.user_id = model.user_id;
                        omodel.topic_id = model.topic_id;
                        omodel.topic_name = Convert.ToString(ds.Tables[0].Rows[0]["topic_name"]);
                        omodel.content_id=model.content_id;
                        omodel.content_name= Convert.ToString(ds.Tables[0].Rows[0]["content_name"]);
                    }
                    var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                    return message;
            }
            catch (Exception ex)
            {
                omodel.status = "204";
                omodel.message = ex.Message;
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage TopicContentWiseAnswerUpdate(TopicContentWiseAnswerUpdateInput model)
        {
            TopicContentWiseAnswerUpdateOutput omodel = new TopicContentWiseAnswerUpdateOutput();
            try
            {
                if (!ModelState.IsValid)
                {
                    omodel.status = "213";
                    omodel.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
                }
                else
                {
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_FSMLMSINFODETAILS", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "TOPICCONTENTWISEANSWERUPDATE");
                    sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@TOPIC_ID", model.topic_id);
                    sqlcmd.Parameters.AddWithValue("@TOPICNAME", model.topic_name);
                    sqlcmd.Parameters.AddWithValue("@CONTENT_ID", model.content_id);
                    sqlcmd.Parameters.AddWithValue("@QUESTION_ID", model.question_id);
                    sqlcmd.Parameters.AddWithValue("@QUESTION", model.question);
                    sqlcmd.Parameters.AddWithValue("@OPTION_ID", model.option_id);
                    sqlcmd.Parameters.AddWithValue("@OPTION_NUMBER", model.option_number);
                    sqlcmd.Parameters.AddWithValue("@OPTION_POINT", model.option_point);
                    sqlcmd.Parameters.AddWithValue("@ISCORRECT", model.isCorrect);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Saved Successfully.";
                    }
                    var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                    return message;
                }
            }
            catch (Exception ex)
            {
                omodel.status = "204";
                omodel.message = ex.Message;
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage UserWiseAPPCrashDetails(UserWiseAPPCrashDetailsSaveInput model)
        {
            UserWiseAPPCrashDetailsSaveOutput omodel = new UserWiseAPPCrashDetailsSaveOutput();

            try
            {
                if (!ModelState.IsValid)
                {
                    omodel.status = "213";
                    omodel.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
                }
                else
                {
                    List<Userappcrashinfolists> omodel2 = new List<Userappcrashinfolists>();
                    foreach (var s2 in model.crash_report_save_list)
                    {
                        omodel2.Add(new Userappcrashinfolists()
                        {
                            errorMessage = s2.errorMessage,
                            stackTrace = s2.stackTrace,
                            date_time = s2.date_time,
                            device = s2.device,
                            os_version = s2.os_version,
                            app_version = s2.app_version,
                            user_remarks = s2.user_remarks
                        });
                    }

                    string JsonXML = XmlConversion.ConvertToXml(omodel2, 0);

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_FSMLMSINFODETAILS", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "APPCRASHDETAILS");
                    sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@JsonXML", JsonXML);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0 && Convert.ToInt64(dt.Rows[0][0]) == model.user_id)
                    {
                        omodel.status = "200";
                        omodel.message = "Crash report submitted successfully.";
                    }
                    else
                    {
                        omodel.status = "205";
                        omodel.message = "Failed to submit crash report. Please try again later.";
                    }
                    var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                    return message;
                }
            }
            catch (Exception ex)
            {
                omodel.status = "204";
                omodel.message = ex.Message;
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }
    }
}

using ShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShopAPI.Controllers
{
    public class SurveyQuestionAnswerController : ApiController
    {
        public HttpResponseMessage QuestionList(SurveyQuestionListInput model)
        {
            SurveyQuestionListOutput omodel = new SurveyQuestionListOutput();
            List<SurveyQuestionList> oview = new List<SurveyQuestionList>();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_FSMAPISURVEYQUESTIONANSWERINFO", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "QUESTIONLIST");
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<SurveyQuestionList>(dt);
                    omodel.status = "200";
                    omodel.message = "Successfully get Question list.";
                    omodel.Question_list = oview;
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
        public HttpResponseMessage AnswerListSave(SurveyAnswerListSaveInput model)
        {
            SurveyAnswerListSaveOutput omodel = new SurveyAnswerListSaveOutput();
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
                    List<AnswerListInput> omedl2 = new List<AnswerListInput>();
                    foreach (var s2 in model.answer_list)
                    {
                        omedl2.Add(new AnswerListInput()
                        {
                            question_id = s2.question_id,
                            answer = s2.answer
                        });
                    }

                    string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_FSMAPISURVEYQUESTIONANSWERINFO", sqlcon);
                    sqlcmd.Parameters.Add("@ACTION", "INSERTDATA");
                    sqlcmd.Parameters.Add("@USER_ID", model.user_id);
                    sqlcmd.Parameters.Add("@SHOP_ID", model.shop_id);
                    sqlcmd.Parameters.Add("@SURVEY_ID", model.survey_id);
                    sqlcmd.Parameters.Add("@DATE_TIME", model.date_time);
                    sqlcmd.Parameters.Add("@GROUP_NAME", model.group_name);
                    sqlcmd.Parameters.Add("@QUESTION_FOR_SHOPTYPE_ID", model.question_for_shoptype_id);
                    sqlcmd.Parameters.Add("@JsonXML", JsonXML);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Successfully Answer Saved.";
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

        public HttpResponseMessage QuestionAnswerDetailsList(SurveyQADetailsListInput model)
        {
            SurveyQADetailsListOutput odata = new SurveyQADetailsListOutput();
            String APIHostingPort = System.Configuration.ConfigurationSettings.AppSettings["APIHostingPort"];

            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
            }
            else
            {
                DataSet ds = new DataSet();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_FSMAPISURVEYQUESTIONANSWERINFO", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "QUESTIONANSWERDETAILLIST");
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@SHOP_ID", model.shop_id);
                sqlcmd.Parameters.Add("@BaseURL", APIHostingPort);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(ds);
                sqlcon.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<SurveyDetailList> oview = new List<SurveyDetailList>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        List<SurveyQuestionAnsDetailList> delivermodelQA = new List<SurveyQuestionAnsDetailList>();
                        for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                        {
                            if (Convert.ToString(ds.Tables[1].Rows[j]["survey_id"]) == Convert.ToString(ds.Tables[0].Rows[i]["survey_id"]))
                            {
                                delivermodelQA.Add(new SurveyQuestionAnsDetailList()
                                {
                                    question_id = Convert.ToInt32(ds.Tables[1].Rows[j]["question_id"]),
                                    question_desc = Convert.ToString(ds.Tables[1].Rows[j]["question_desc"]),
                                    answer = Convert.ToString(ds.Tables[1].Rows[j]["answer"]),
                                    image_link = Convert.ToString(ds.Tables[1].Rows[j]["image_link"])
                                });
                            }
                        }
                        oview.Add(new SurveyDetailList()
                        {
                            question_ans_list = delivermodelQA,
                            survey_id = Convert.ToString(ds.Tables[0].Rows[i]["survey_id"]),
                            saved_date_time = Convert.ToDateTime(ds.Tables[0].Rows[i]["saved_date_time"]),
                            question_for_shoptype_id = Convert.ToInt32(ds.Tables[0].Rows[i]["question_for_shoptype_id"]),
                            group_name = Convert.ToString(ds.Tables[0].Rows[i]["group_name"])
                        });
                    }
                    odata.status = "200";
                    odata.message = "Successfully get question list.";
                    odata.user_id = model.user_id;
                    odata.shop_id = model.shop_id;
                    odata.survey_list = oview;
                }
                else
                {
                    odata.status = "205";
                    odata.message = "No data found";
                }

                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }
        }
    }
}

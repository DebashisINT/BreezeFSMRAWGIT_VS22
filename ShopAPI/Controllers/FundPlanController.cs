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
    public class FundPlanController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage FundPlanList(FundPlanListInput model)
        {
            try
            {
                FundPlanListOutput odata = new FundPlanListOutput();

                if (!ModelState.IsValid)
                {
                    odata.status = "213";
                    odata.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
                }
                else
                {
                    String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                    //if (token == model.session_token)
                    //{
                        string sessionId = "";

                        DataSet ds = new DataSet();
                        String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                        SqlCommand sqlcmd = new SqlCommand();
                        SqlConnection sqlcon = new SqlConnection(con);
                        sqlcon.Open();
                        sqlcmd = new SqlCommand("Proc_FTS_APIFundPlanList", sqlcon);
                        sqlcmd.Parameters.Add("@user_id", model.user_id);
                        sqlcmd.Parameters.Add("@Action", "List");
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                        da.Fill(ds);
                        sqlcon.Close();

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            List<FundPlanList> oview = new List<FundPlanList>();

                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                oview.Add(new FundPlanList()
                                {
                                    plan_id = Convert.ToString(ds.Tables[0].Rows[i]["PLAN_ID"]),
                                    party_name = Convert.ToString(ds.Tables[0].Rows[i]["PARTY_NAME"]),
                                    contact_no = Convert.ToString(ds.Tables[0].Rows[i]["CONTACT_NO"]),
                                    location = Convert.ToString(ds.Tables[0].Rows[i]["LOCATION"]),
                                    last_plan_date = Convert.ToString(ds.Tables[0].Rows[i]["PLAN_DATE"]) == "" ? "" : Convert.ToDateTime(ds.Tables[0].Rows[i]["PLAN_DATE"]).ToString("yyyy-MM-dd"),
                                    last_plan_value = Convert.ToString(ds.Tables[0].Rows[i]["PLAN_AMT"]),
                                    last_achv_amount = Convert.ToString(ds.Tables[0].Rows[i]["ACHIV_AMT"]),
                                    last_plan_feedback = Convert.ToString(ds.Tables[0].Rows[i]["PLAN_REMARKS"]),
                                    last_achv_feedback = Convert.ToString(ds.Tables[0].Rows[i]["ACHIV_REMARKS"])
                                });
                            }
                            odata.update_plan_list = oview;
                            odata.status = "200";
                            odata.message = "Update plan list";
                        }
                        else
                        {
                            odata.status = "205";
                            odata.message = "No data found";
                        }
                        var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                        return message;
                    //}
                    //else
                    //{
                    //    odata.status = "215";
                    //    odata.message = "You are not authorized.";
                    //}
                    //var messages = Request.CreateResponse(HttpStatusCode.OK, odata);
                    //return messages;
                }
            }
            catch (Exception ex)
            {
                var message = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage FundPlanDetailsList(DetailsPlanListInput model)
        {
            try
            {
                DetailsPlanListOutput odata = new DetailsPlanListOutput();

                if (!ModelState.IsValid)
                {
                    odata.status = "213";
                    odata.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
                }
                else
                {
                    String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                    //if (token == model.session_token)
                    //{
                        string sessionId = "";

                        DataSet ds = new DataSet();
                        String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                        SqlCommand sqlcmd = new SqlCommand();
                        SqlConnection sqlcon = new SqlConnection(con);
                        sqlcon.Open();
                        sqlcmd = new SqlCommand("Proc_FTS_APIFundPlanList", sqlcon);
                        sqlcmd.Parameters.Add("@user_id", model.user_id);
                        sqlcmd.Parameters.Add("@plan_id", model.plan_id);
                        sqlcmd.Parameters.Add("@Action", "Details");
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                        da.Fill(ds);
                        sqlcon.Close();

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            List<FundPlanDetailsList> oview = new List<FundPlanDetailsList>();

                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                oview.Add(new FundPlanDetailsList()
                                {
                                    details_id = Convert.ToString(ds.Tables[0].Rows[i]["ID"]),
                                    plan_date = Convert.ToDateTime(ds.Tables[0].Rows[i]["PLAN_DATE"]).ToString("yyyy-MM-dd"),
                                    plan_value = Convert.ToString(ds.Tables[0].Rows[i]["PLAN_AMT"]),
                                    plan_remarks = Convert.ToString(ds.Tables[0].Rows[i]["PLAN_REMARKS"]),
                                    achievement_value = Convert.ToString(ds.Tables[0].Rows[i]["ACHIV_AMT"]) == "0.00" ? "" : Convert.ToString(ds.Tables[0].Rows[i]["ACHIV_AMT"]),
                                    achievement_date = Convert.ToString(ds.Tables[0].Rows[i]["ACHIV_DATE"]) == "" ? "" : Convert.ToDateTime(ds.Tables[0].Rows[i]["ACHIV_DATE"]).ToString("yyyy-MM-dd"),
                                    achievement_remarks = Convert.ToString(ds.Tables[0].Rows[i]["ACHIV_REMARKS"]),
                                    percnt = Convert.ToString(ds.Tables[0].Rows[i]["PERCENTAGES"])
                                });
                            }

                            odata.plan_data_details = oview;
                            odata.status = "200";
                            odata.message = "Update plan details";
                        }
                        else
                        {
                            odata.status = "205";
                            odata.message = "No data found";
                        }
                        var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                        return message;
                    //}
                    //else
                    //{
                    //    odata.status = "215";
                    //    odata.message = "You are not authorized.";
                    //}
                    //var messages = Request.CreateResponse(HttpStatusCode.OK, odata);
                    //return messages;

                }
            }
            catch (Exception ex)
            {
                var message = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateFundPlan(updateFoundPlanInput model)
        {
            try
            {
                DetailsPlanListOutput odata = new DetailsPlanListOutput();

                if (!ModelState.IsValid)
                {
                    odata.status = "213";
                    odata.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
                }
                else
                {
                    String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                    //if (token == model.session_token)
                    //{
                        DataTable fundplandt = new DataTable();
                        //if (fundplandt == null && fundplandt.Rows.Count<0)
                        //{
                        fundplandt.Columns.Add("PLAN_ID", typeof(long));
                        fundplandt.Columns.Add("PLAN_AMT", typeof(decimal));
                        fundplandt.Columns.Add("PLAN_DATE", typeof(DateTime));
                        fundplandt.Columns.Add("PLAN_REMARKS", typeof(string));
                        fundplandt.Columns.Add("ACHIV_AMT", typeof(decimal));
                       // fundplandt.Columns.Add("ACHIV_DATE", typeof(DateTime));
                        fundplandt.Columns.Add("ACHIV_REMARKS", typeof(string));
                        // }

                        if (model.update_plan_list != null)
                        {
                            DateTime? ACHIV_DATE = null;
                            foreach (var s2 in model.update_plan_list)
                            {
                                fundplandt.Rows.Add(s2.plan_id, s2.plan_value, s2.plan_date, s2.plan_remarks, s2.achievement_value, s2.acheivement_remarks);  //s2.achievement_date,
                            }
                        }

                        string sessionId = "";

                        DataTable dt = new DataTable();
                        String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                        SqlCommand sqlcmd = new SqlCommand();
                        SqlConnection sqlcon = new SqlConnection(con);
                        sqlcon.Open();
                        sqlcmd = new SqlCommand("Proc_FTS_APIFundPlanList", sqlcon);
                        sqlcmd.Parameters.Add("@user_id", model.user_id);
                        sqlcmd.Parameters.Add("@Action", "Update");
                        SqlParameter paramProd = sqlcmd.Parameters.Add("@FUNDPLAN", SqlDbType.Structured);
                        paramProd.Value = fundplandt;
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                        da.Fill(dt);
                        sqlcon.Close();

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            odata.status = "200";
                            odata.message = "Successfully updated plan list.";
                        }
                        else
                        {
                            odata.status = "205";
                            odata.message = "Not update plan list. Please try again.";
                        }
                        var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                        return message;
                    //}
                    //else
                    //{
                    //    odata.status = "215";
                    //    odata.message = "You are not authorized.";
                    //}
                    //var messages = Request.CreateResponse(HttpStatusCode.OK, odata);
                    //return messages;
                }
            }
            catch (Exception ex)
            {
                var message = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage GetAllFundPlanList(FundPlanListInput model)
        {
            try
            {
                ALLPlanListOutput odata = new ALLPlanListOutput();

                if (!ModelState.IsValid)
                {
                    odata.status = "213";
                    odata.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
                }
                else
                {
                    String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                    //if (token == model.session_token)
                    //{
                    string sessionId = "";

                    DataSet ds = new DataSet();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_API_DALYPLANREPORT", sqlcon);
                    sqlcmd.Parameters.Add("@USERID", model.user_id);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(ds);
                    sqlcon.Close();

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        List<FundPlanDetailsListALL> oview = new List<FundPlanDetailsListALL>();

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            oview.Add(new FundPlanDetailsListALL()
                            {
                                plan_data_id = Convert.ToString(ds.Tables[0].Rows[i]["ID"]),
                                date = Convert.ToDateTime(ds.Tables[0].Rows[i]["DATE"]).ToString("yyyy-MM-dd"),
                                plan_date = Convert.ToString(ds.Tables[0].Rows[i]["PLAN_DATE"]) == "" ? "" : Convert.ToDateTime(ds.Tables[0].Rows[i]["PLAN_DATE"]).ToString("yyyy-MM-dd"),
                                plan_value = Convert.ToString(ds.Tables[0].Rows[i]["PLAN_AMT"]) == "0.00" ? "" : Convert.ToString(ds.Tables[0].Rows[i]["PLAN_AMT"]),
                                plan_remarks = Convert.ToString(ds.Tables[0].Rows[i]["PLAN_REMARKS"]),
                                achievement_value = Convert.ToString(ds.Tables[0].Rows[i]["ACHIV_AMT"]) == "0.00" ? "" : Convert.ToString(ds.Tables[0].Rows[i]["ACHIV_AMT"]),
                                achievement_date = Convert.ToString(ds.Tables[0].Rows[i]["ACHIV_DATE"]) == "" ? "" : Convert.ToDateTime(ds.Tables[0].Rows[i]["ACHIV_DATE"]).ToString("yyyy-MM-dd"),
                                achievement_remarks = Convert.ToString(ds.Tables[0].Rows[i]["ACHIV_REMARKS"]),
                                percnt = Convert.ToString(ds.Tables[0].Rows[i]["PERCENTAGES"]),
                                party_name = Convert.ToString(ds.Tables[0].Rows[i]["PARTY_NAME"]),
                                type = Convert.ToString(ds.Tables[0].Rows[i]["TYPE"]),
                                contact_no = Convert.ToString(ds.Tables[0].Rows[i]["CONTACT_NO"]),
                                location = Convert.ToString(ds.Tables[0].Rows[i]["LOCATION"])
                            });
                        }

                        odata.plan_data = oview;
                        odata.status = "200";
                        odata.message = "Update plan list All";
                    }
                    else
                    {
                        odata.status = "205";
                        odata.message = "No data found";
                    }
                    var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                    return message;
                    //}
                    //else
                    //{
                    //    odata.status = "215";
                    //    odata.message = "You are not authorized.";
                    //}
                    //var messages = Request.CreateResponse(HttpStatusCode.OK, odata);
                    //return messages;
                }
            }
            catch (Exception ex)
            {
                var message = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                return message;
            }
        }
    }
}

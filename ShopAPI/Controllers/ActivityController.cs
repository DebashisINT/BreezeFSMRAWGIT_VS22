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
    public class ActivityController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage ActivityDropdownList(ActivityModel model)
        {
            ActivityDropdownOutput omodel = new ActivityDropdownOutput();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                string sessionId = "";
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_FTSActivityList", sqlcon);
                sqlcmd.Parameters.Add("@Action", "ActivityDropdownList");
                sqlcmd.Parameters.Add("@user_id", model.user_id);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    List<Activities> oview = new List<Activities>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        oview.Add(new Activities()
                        {
                            id = Convert.ToString(dt.Rows[i]["Id"]),
                            name = Convert.ToString(dt.Rows[i]["ActivityName"])
                        });
                    }
                    omodel.activity_list = oview;
                    omodel.status = "200";
                    omodel.message = "Successfully get activity list";
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
        public HttpResponseMessage ActivityTypeList(ActivityModel model)
        {
            ActivityTypeListOutput omodel = new ActivityTypeListOutput();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                string sessionId = "";
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_FTSActivityList", sqlcon);
                sqlcmd.Parameters.Add("@Action", "ActivityTypeList");
                sqlcmd.Parameters.Add("@user_id", model.user_id);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    List<Activities> oview = new List<Activities>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        oview.Add(new Activities()
                        {
                            id = Convert.ToString(dt.Rows[i]["Id"]),
                            name = Convert.ToString(dt.Rows[i]["ActivityTypeName"]),
                            activityId = Convert.ToString(dt.Rows[i]["ActivityId"])
                        });
                    }
                    omodel.type_list = oview;
                    omodel.status = "200";
                    omodel.message = "Successfully get type list";
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
        public HttpResponseMessage ActivityProductList(ActivityModel model)
        {
            ActivityProductListOutput omodel = new ActivityProductListOutput();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                string sessionId = "";
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_FTSActivityList", sqlcon);
                sqlcmd.Parameters.Add("@Action", "ActivityProductList");
                sqlcmd.Parameters.Add("@user_id", model.user_id);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    List<Activities> oview = new List<Activities>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        oview.Add(new Activities()
                        {
                            id = Convert.ToString(dt.Rows[i]["sProducts_ID"]),
                            name = Convert.ToString(dt.Rows[i]["sProducts_Name"])
                        });
                    }
                    omodel.product_list = oview;
                    omodel.status = "200";
                    omodel.message = "Successfully get product list";
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
        public HttpResponseMessage ActivityPriorityList(ActivityModel model)
        {
            ActivityPriorityListOutput omodel = new ActivityPriorityListOutput();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                string sessionId = "";
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_FTSActivityList", sqlcon);
                sqlcmd.Parameters.Add("@Action", "ActivityPriorityList");
                sqlcmd.Parameters.Add("@user_id", model.user_id);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    List<Activities> oview = new List<Activities>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        oview.Add(new Activities()
                        {
                            id = Convert.ToString(dt.Rows[i]["Id"]),
                            name = Convert.ToString(dt.Rows[i]["PriorityName"])
                        });
                    }
                    omodel.priority_list = oview;
                    omodel.status = "200";
                    omodel.message = "Successfully get priority list";
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
        public HttpResponseMessage ActivityAdd(ActivityAddInput model)
        {
            ActivityAddOutput omodel = new ActivityAddOutput();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                string sessionId = "";
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_FTS_SALESACTIVITY", sqlcon);
                sqlcmd.Parameters.Add("@ACTION_TYPE", "SAVE");
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@ActivityCode", model.id);
                sqlcmd.Parameters.Add("@party_id", model.party_id);
                sqlcmd.Parameters.Add("@date", model.date);
                sqlcmd.Parameters.Add("@time", model.time);
                sqlcmd.Parameters.Add("@name", model.name);
                sqlcmd.Parameters.Add("@activity_id", model.activity_id);
                sqlcmd.Parameters.Add("@type_id", model.type_id);
                sqlcmd.Parameters.Add("@product_id", model.product_id);
                sqlcmd.Parameters.Add("@subject", model.subject);
                sqlcmd.Parameters.Add("@details", model.details);
                sqlcmd.Parameters.Add("@duration", model.duration);
                sqlcmd.Parameters.Add("@priority_id", model.priority_id);
                sqlcmd.Parameters.Add("@due_date", model.due_date);
                sqlcmd.Parameters.Add("@due_time", model.due_time);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt!=null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["STATUS"].ToString() == "Success")
                    {
                        omodel.status = "200";
                        omodel.message = dt.Rows[0]["RETURNMESSAGE"].ToString();
                    }
                    else
                    {
                        omodel.status = "205";
                        omodel.message = dt.Rows[0]["RETURNMESSAGE"].ToString();
                    }
                }
                else
                {
                    omodel.status = "205";
                    omodel.message = "Failed to add activity";
                }
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage ActivityList(ActivityModel model)
        {
            ActivityListOutput omodel = new ActivityListOutput();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                String ActivityAttachment = System.Configuration.ConfigurationSettings.AppSettings["ActivityAttachment"];
                string sessionId = "";
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_FTSActivityList", sqlcon);
                sqlcmd.Parameters.Add("@Action", "ActivityList");
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@URL", ActivityAttachment);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    List<ActivityList> oview = new List<ActivityList>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        oview.Add(new ActivityList()
                        {
                            id = Convert.ToString(dt.Rows[i]["ActivityCode"]),
                            party_id = Convert.ToString(dt.Rows[i]["Party_Code"]),
                            date = Convert.ToString(dt.Rows[i]["Activity_Date"]),
                            time = Convert.ToString(dt.Rows[i]["Activity_Time"]),
                            name = Convert.ToString(dt.Rows[i]["ContactName"]),
                            activity_id = Convert.ToString(dt.Rows[i]["Activityid"]),
                            type_id = Convert.ToString(dt.Rows[i]["Typeid"]),
                            product_id = Convert.ToString(dt.Rows[i]["ProdId"]),
                            subject = Convert.ToString(dt.Rows[i]["ActivitySubject"]),
                            details = Convert.ToString(dt.Rows[i]["ActivityDetails"]),
                            duration = Convert.ToString(dt.Rows[i]["Duration"]),
                            priority_id = Convert.ToString(dt.Rows[i]["Priorityid"]),
                            due_date = Convert.ToString(dt.Rows[i]["Duedate"]),
                            due_time = Convert.ToString(dt.Rows[i]["due_time"]),
                            attachments = Convert.ToString(dt.Rows[i]["Attachment"]),
                            image = Convert.ToString(dt.Rows[i]["IMAGE"])
                        });
                    }
                    omodel.activity_list = oview;
                    omodel.status = "200";
                    omodel.message = "Successfully get activity list";
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
    }
}

using Newtonsoft.Json;
using ShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace ShopAPI.Controllers
{
    public class JobCustomerController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage List(JobCustomerModel model)
        {
            JobCustomerOutput omodel = new JobCustomerOutput();
            List<JobCustomerList> oview = new List<JobCustomerList>();

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
                sqlcmd = new SqlCommand("PRC_CustomerJobList", sqlcon);
                sqlcmd.Parameters.Add("@Action", "CustomerJobListDateWise");
                sqlcmd.Parameters.Add("@session_token", model.session_token);
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@date", model.date);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<JobCustomerList>(dt);
                    omodel.job_list = oview;
                    omodel.status = "200";
                    omodel.message = "Successfully get job list.";
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
        public HttpResponseMessage GetStatus(GetStatusInput model)
        {
            GetStatusOutPut omodel = new GetStatusOutPut();
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
                sqlcmd = new SqlCommand("PRC_CustomerJobList", sqlcon);
                sqlcmd.Parameters.Add("@Action", "JobStatus");
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@job_id", model.id);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.job_status = Convert.ToString(dt.Rows[0]["status"]);
                    omodel.last_status = Convert.ToString(dt.Rows[0]["last_status"]);
                    omodel.status = "200";
                    omodel.message = "Successfully get status.";
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
        public HttpResponseMessage GetWipSettings(GetStatusInput model)
        {
            GetWipSettingsOutPut omodel = new GetWipSettingsOutPut();
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
                sqlcmd = new SqlCommand("PRC_CustomerJobList", sqlcon);
                sqlcmd.Parameters.Add("@Action", "WIPSettings");
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@job_id", model.id);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.uom_text = Convert.ToString(dt.Rows[0]["UOM"]);
                    omodel.service_due_for = Convert.ToString(dt.Rows[0]["SERVICE_DUE_FOR"]);
                    omodel.status = "200";
                    omodel.message = "Successfully get settings.";
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
        public HttpResponseMessage AssignJob(JobCustomerAssignJobInput model)
        {
            AssignJobOutPut omodel = new AssignJobOutPut();
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
                sqlcmd = new SqlCommand("PRC_CustomerJobList", sqlcon);
                sqlcmd.Parameters.Add("@Action", "AssignCustomerJob");
                sqlcmd.Parameters.Add("@job_id", model.job_id);
                sqlcmd.Parameters.Add("@shop_code", model.shop_code);
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@status", model.status);
                sqlcmd.Parameters.Add("@service_for", model.service_for);
                sqlcmd.Parameters.Add("@service_due_for", model.service_due_for);
                sqlcmd.Parameters.Add("@UOM", model.UOM);
                sqlcmd.Parameters.Add("@total_service", model.total_service);
                sqlcmd.Parameters.Add("@service_frequency", model.service_frequency);
                sqlcmd.Parameters.Add("@total_service_commited", model.total_service_commited);
                sqlcmd.Parameters.Add("@total_service_pending", model.total_service_pending);
                sqlcmd.Parameters.Add("@jobcreate_date", model.jobcreate_date);
                sqlcmd.Parameters.Add("@create_user", model.create_user);
                sqlcmd.Parameters.Add("@job_code", model.job_code);
                sqlcmd.Parameters.Add("@sub_userid", model.sub_userid);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    //Rev Debashis
                    //omodel.status = Convert.ToString(dt.Rows[0]["STATUSCODE"]);
                    //omodel.message = Convert.ToString(dt.Rows[0]["MSG"]);
                    omodel.status = "200";
                    omodel.message = "Success";
                    //End of Rev Debashis
                }
                else
                {
                    omodel.status = "205";
                    omodel.message = "Failed.";
                }
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage WorkInProgressSubmit(WorkInProgressInput model)
        {

            String ERPAPIBSAEURL = System.Configuration.ConfigurationSettings.AppSettings["ERPAPIBSAEURL"];
            String apiUrl = ERPAPIBSAEURL + "AssignScheduleStatus/WorkInProgressSubmit";
            AssignJobOutPut omodel = new AssignJobOutPut();
            AssignJobOutPut omodel1 = new AssignJobOutPut();
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
                sqlcmd = new SqlCommand("PRC_CustomerJobStatusInsertUpdate", sqlcon);
                sqlcmd.Parameters.Add("@Action", "WorkInProgress");
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@job_id", model.job_id);
                sqlcmd.Parameters.Add("@date", model.start_date);
                sqlcmd.Parameters.Add("@time", model.start_time);
                sqlcmd.Parameters.Add("@service_due", model.service_due);
                sqlcmd.Parameters.Add("@service_completed", model.service_completed);
                sqlcmd.Parameters.Add("@next_date", model.next_date);
                sqlcmd.Parameters.Add("@next_time", model.next_time);
                sqlcmd.Parameters.Add("@remarks", model.remarks);
                sqlcmd.Parameters.Add("@date_time", model.date_time);
                sqlcmd.Parameters.Add("@latitude", model.latitude);
                sqlcmd.Parameters.Add("@longitude", model.longitude);
                sqlcmd.Parameters.Add("@address", model.address);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Successfully submit work in progress.";
                    omodel.id = dt.Rows[0]["ID"].ToString();

                    model.fsm_id = dt.Rows[0]["ID"].ToString();

                    string data = JsonConvert.SerializeObject(model);
                    WebClient client = new WebClient();
                    client.Headers["Content-type"] = "application/json";
                    client.Encoding = Encoding.UTF8;
                    string json = client.UploadString(apiUrl, data);
                    omodel1 = JsonConvert.DeserializeObject<AssignJobOutPut>(json);
                    
                    if (omodel1.status == "205")
                    {
                        SqlCommand sqlcmd1 = new SqlCommand();
                        SqlConnection sqlcon1 = new SqlConnection(con);
                        sqlcon1.Open();
                        sqlcmd1 = new SqlCommand("PRC_CustomerJobStatusInsertUpdate", sqlcon1);
                        sqlcmd1.Parameters.Add("@Action", "DeleteWorkInProgress");
                        sqlcmd1.Parameters.Add("@StatusID", dt.Rows[0]["ID"].ToString());
                        sqlcmd1.CommandType = CommandType.StoredProcedure;
                        sqlcmd1.ExecuteNonQuery();
                        da.Fill(dt);
                        sqlcon1.Close();

                        omodel.status = "205";
                        omodel.message = "Failed.";
                        omodel.id = "0";
                    }
                    else if (omodel1.status == "200")
                    {
                        SqlCommand sqlcmd1 = new SqlCommand();
                        SqlConnection sqlcon1 = new SqlConnection(con);
                        sqlcon1.Open();
                        sqlcmd1 = new SqlCommand("PRC_CustomerJobStatusInsertUpdate", sqlcon1);
                        sqlcmd1.Parameters.Add("@Action", "UpdateERPId");
                        sqlcmd1.Parameters.Add("@StatusID", dt.Rows[0]["ID"].ToString());
                        sqlcmd1.Parameters.Add("@ERP_ID", omodel1.id);
                        sqlcmd1.CommandType = CommandType.StoredProcedure;
                        sqlcmd1.ExecuteNonQuery();
                        da.Fill(dt);
                        sqlcon1.Close();
                    }
                }
                else
                {
                    omodel.status = "205";
                    omodel.message = "Failed.";
                    omodel.id = "0";
                }
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage WorkOnHoldSubmit(WorkOnHoldInput model)
        {
            String ERPAPIBSAEURL = System.Configuration.ConfigurationSettings.AppSettings["ERPAPIBSAEURL"];
            String apiUrl = ERPAPIBSAEURL + "AssignScheduleStatus/WorkOnHoldSubmit";
            AssignJobOutPut omodel = new AssignJobOutPut();
            AssignJobOutPut omodel1 = new AssignJobOutPut();
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
                sqlcmd = new SqlCommand("PRC_CustomerJobStatusInsertUpdate", sqlcon);
                sqlcmd.Parameters.Add("@Action", "WorkOnHold");
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@job_id", model.job_id);
                sqlcmd.Parameters.Add("@date", model.hold_date);
                sqlcmd.Parameters.Add("@time", model.hold_time);
                sqlcmd.Parameters.Add("@reason_hold", model.reason_hold);
                sqlcmd.Parameters.Add("@remarks", model.remarks);
                sqlcmd.Parameters.Add("@date_time", model.date_time);
                sqlcmd.Parameters.Add("@latitude", model.latitude);
                sqlcmd.Parameters.Add("@longitude", model.longitude);
                sqlcmd.Parameters.Add("@address", model.address);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Successfully submit work on hold.";
                    omodel.id = dt.Rows[0]["ID"].ToString();

                    model.fsm_id = dt.Rows[0]["ID"].ToString();
                    string data = JsonConvert.SerializeObject(model);
                    WebClient client = new WebClient();
                    client.Headers["Content-type"] = "application/json";
                    client.Encoding = Encoding.UTF8;
                    string json = client.UploadString(apiUrl, data);
                    omodel1 = JsonConvert.DeserializeObject<AssignJobOutPut>(json);
                    if (omodel1.status == "205")
                    {
                        SqlCommand sqlcmd1 = new SqlCommand();
                        SqlConnection sqlcon1 = new SqlConnection(con);
                        sqlcon1.Open();
                        sqlcmd1 = new SqlCommand("PRC_CustomerJobStatusInsertUpdate", sqlcon1);
                        sqlcmd1.Parameters.Add("@Action", "DeleteWorkOnHold");
                        sqlcmd1.Parameters.Add("@StatusID", dt.Rows[0]["ID"].ToString());
                        sqlcmd1.CommandType = CommandType.StoredProcedure;
                        sqlcmd1.ExecuteNonQuery();
                        da.Fill(dt);
                        sqlcon1.Close();

                        omodel.status = "205";
                        omodel.message = "Failed.";
                        omodel.id = "0";
                    }
                    else if (omodel1.status == "200")
                    {
                        SqlCommand sqlcmd1 = new SqlCommand();
                        SqlConnection sqlcon1 = new SqlConnection(con);
                        sqlcon1.Open();
                        sqlcmd1 = new SqlCommand("PRC_CustomerJobStatusInsertUpdate", sqlcon1);
                        sqlcmd1.Parameters.Add("@Action", "UpdateERPId");
                        sqlcmd1.Parameters.Add("@StatusID", dt.Rows[0]["ID"].ToString());
                        sqlcmd1.Parameters.Add("@ERP_ID", omodel1.id);
                        sqlcmd1.CommandType = CommandType.StoredProcedure;
                        sqlcmd1.ExecuteNonQuery();
                        da.Fill(dt);
                        sqlcon1.Close();
                    }
                }
                else
                {
                    omodel.status = "205";
                    omodel.message = "Failed.";
                    omodel.id = "0";
                }
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage WorkOnCompletedSubmit(WorkOnCompletedInput model)
        {
            String ERPAPIBSAEURL = System.Configuration.ConfigurationSettings.AppSettings["ERPAPIBSAEURL"];
            String apiUrl = ERPAPIBSAEURL + "AssignScheduleStatus/WorkOnCompletedSubmit";
            AssignJobOutPut omodel = new AssignJobOutPut();
            AssignJobOutPut omodel1 = new AssignJobOutPut();
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
                sqlcmd = new SqlCommand("PRC_CustomerJobStatusInsertUpdate", sqlcon);
                sqlcmd.Parameters.Add("@Action", "WorkOnCompleted");
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@job_id", model.job_id);
                sqlcmd.Parameters.Add("@date", model.finish_date);
                sqlcmd.Parameters.Add("@time", model.finish_time);
                sqlcmd.Parameters.Add("@remarks", model.remarks);
                sqlcmd.Parameters.Add("@date_time", model.date_time);
                sqlcmd.Parameters.Add("@latitude", model.latitude);
                sqlcmd.Parameters.Add("@longitude", model.longitude);
                sqlcmd.Parameters.Add("@address", model.address);
                sqlcmd.Parameters.Add("@phone_no", model.phone_no);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Successfully submit work completed.";
                    omodel.id = dt.Rows[0]["ID"].ToString();

                    model.fsm_id = dt.Rows[0]["ID"].ToString();
                    string data = JsonConvert.SerializeObject(model);
                    WebClient client = new WebClient();
                    client.Headers["Content-type"] = "application/json";
                    client.Encoding = Encoding.UTF8;
                    string json = client.UploadString(apiUrl, data);
                    omodel1 = JsonConvert.DeserializeObject<AssignJobOutPut>(json);
                    if (omodel1.status == "205")
                    {
                        SqlCommand sqlcmd1 = new SqlCommand();
                        SqlConnection sqlcon1 = new SqlConnection(con);
                        sqlcon1.Open();
                        sqlcmd1 = new SqlCommand("PRC_CustomerJobStatusInsertUpdate", sqlcon1);
                        sqlcmd1.Parameters.Add("@Action", "DeleteWorkOnCompleted");
                        sqlcmd1.Parameters.Add("@StatusID", dt.Rows[0]["ID"].ToString());
                        sqlcmd1.CommandType = CommandType.StoredProcedure;
                        sqlcmd1.ExecuteNonQuery();
                        da.Fill(dt);
                        sqlcon1.Close();

                        omodel.status = "205";
                        omodel.message = "Failed.";
                        omodel.id = "0";
                    }
                    else if (omodel1.status == "200")
                    {
                        SqlCommand sqlcmd1 = new SqlCommand();
                        SqlConnection sqlcon1 = new SqlConnection(con);
                        sqlcon1.Open();
                        sqlcmd1 = new SqlCommand("PRC_CustomerJobStatusInsertUpdate", sqlcon1);
                        sqlcmd1.Parameters.Add("@Action", "UpdateERPId");
                        sqlcmd1.Parameters.Add("@StatusID", dt.Rows[0]["ID"].ToString());
                        sqlcmd1.Parameters.Add("@ERP_ID", omodel1.id);
                        sqlcmd1.CommandType = CommandType.StoredProcedure;
                        sqlcmd1.ExecuteNonQuery();
                        da.Fill(dt);
                        sqlcon1.Close();
                    }
                }
                else
                {
                    omodel.status = "205";
                    omodel.message = "Failed.";
                    omodel.id = "0";
                }
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage WorkCancelledSubmit(WorkCancelledInput model)
        {
            String ERPAPIBSAEURL = System.Configuration.ConfigurationSettings.AppSettings["ERPAPIBSAEURL"];
            String apiUrl = ERPAPIBSAEURL + "AssignScheduleStatus/WorkCancelledSubmit";
            AssignJobOutPut omodel = new AssignJobOutPut();
            AssignJobOutPut omodel1 = new AssignJobOutPut();
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
                sqlcmd = new SqlCommand("PRC_CustomerJobStatusInsertUpdate", sqlcon);
                sqlcmd.Parameters.Add("@Action", "WorkCancelled");
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@job_id", model.job_id);
                sqlcmd.Parameters.Add("@date", model.date);
                sqlcmd.Parameters.Add("@time", model.time);
                sqlcmd.Parameters.Add("@cancel_reason", model.cancel_reason);
                sqlcmd.Parameters.Add("@remarks", model.remarks);
                sqlcmd.Parameters.Add("@date_time", model.date_time);
                sqlcmd.Parameters.Add("@latitude", model.latitude);
                sqlcmd.Parameters.Add("@longitude", model.longitude);
                sqlcmd.Parameters.Add("@address", model.address);
                sqlcmd.Parameters.Add("@CANCELLED_USER", model.cancelled_by);
                sqlcmd.Parameters.Add("@CANCELLED_BY", model.user);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Successfully submit work cancelled.";
                    omodel.id = dt.Rows[0]["ID"].ToString();

                    model.fsm_id = dt.Rows[0]["ID"].ToString();
                    string data = JsonConvert.SerializeObject(model);
                    WebClient client = new WebClient();
                    client.Headers["Content-type"] = "application/json";
                    client.Encoding = Encoding.UTF8;
                    string json = client.UploadString(apiUrl, data);
                    omodel1 = JsonConvert.DeserializeObject<AssignJobOutPut>(json);
                    if (omodel1.status == "205")
                    {
                        SqlCommand sqlcmd1 = new SqlCommand();
                        SqlConnection sqlcon1 = new SqlConnection(con);
                        sqlcon1.Open();
                        sqlcmd1 = new SqlCommand("PRC_CustomerJobStatusInsertUpdate", sqlcon1);
                        sqlcmd1.Parameters.Add("@Action", "UpdateERPId");
                        sqlcmd1.Parameters.Add("@StatusID", dt.Rows[0]["ID"].ToString());
                        sqlcmd1.CommandType = CommandType.StoredProcedure;
                        sqlcmd1.ExecuteNonQuery();
                        da.Fill(dt);
                        sqlcon1.Close();

                        omodel.status = "205";
                        omodel.message = "Failed.";
                        omodel.id = "0";
                    }
                    else if (omodel1.status == "200")
                    {
                        SqlCommand sqlcmd1 = new SqlCommand();
                        SqlConnection sqlcon1 = new SqlConnection(con);
                        sqlcon1.Open();
                        sqlcmd1 = new SqlCommand("PRC_CustomerJobStatusInsertUpdate", sqlcon1);
                        sqlcmd1.Parameters.Add("@Action", "UpdateWorkCancelled");
                        sqlcmd1.Parameters.Add("@StatusID", dt.Rows[0]["ID"].ToString());
                        sqlcmd1.Parameters.Add("@ERP_ID", omodel1.id);
                        sqlcmd1.CommandType = CommandType.StoredProcedure;
                        sqlcmd1.ExecuteNonQuery();
                        da.Fill(dt);
                        sqlcon1.Close();
                    }
                }
                else
                {
                    omodel.status = "205";
                    omodel.message = "Failed.";
                    omodel.id = "0";
                }
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateReview(ReviewInput model)
        {
            String ERPAPIBSAEURL = System.Configuration.ConfigurationSettings.AppSettings["ERPAPIBSAEURL"];
            String apiUrl = ERPAPIBSAEURL + "AssignScheduleStatus/UpdateReview";
            AssignJobOutPut omodel = new AssignJobOutPut();
            AssignJobOutPut omodel1 = new AssignJobOutPut();
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
                sqlcmd = new SqlCommand("PRC_CustomerJobStatusInsertUpdate", sqlcon);
                sqlcmd.Parameters.Add("@Action", "UpdateReview");
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@job_id", model.job_id);
                sqlcmd.Parameters.Add("@review", model.review);
                sqlcmd.Parameters.Add("@rate", model.rate);
                sqlcmd.Parameters.Add("@date_time", model.date_time);
                sqlcmd.Parameters.Add("@latitude", model.latitude);
                sqlcmd.Parameters.Add("@longitude", model.longitude);
                sqlcmd.Parameters.Add("@address", model.address);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Successfully update review.";
                    omodel.id = dt.Rows[0]["ID"].ToString();

                    model.fsm_id = dt.Rows[0]["ID"].ToString();
                    string data = JsonConvert.SerializeObject(model);
                    WebClient client = new WebClient();
                    client.Headers["Content-type"] = "application/json";
                    client.Encoding = Encoding.UTF8;
                    string json = client.UploadString(apiUrl, data);
                    omodel1 = JsonConvert.DeserializeObject<AssignJobOutPut>(json);
                    if (omodel1.status == "205")
                    {
                        SqlCommand sqlcmd1 = new SqlCommand();
                        SqlConnection sqlcon1 = new SqlConnection(con);
                        sqlcon1.Open();
                        sqlcmd1 = new SqlCommand("PRC_CustomerJobStatusInsertUpdate", sqlcon1);
                        sqlcmd1.Parameters.Add("@Action", "DeleteUpdateReview");
                        sqlcmd1.Parameters.Add("@StatusID", dt.Rows[0]["ID"].ToString());
                        sqlcmd1.CommandType = CommandType.StoredProcedure;
                        sqlcmd1.ExecuteNonQuery();
                        da.Fill(dt);
                        sqlcon1.Close();

                        omodel.status = "205";
                        omodel.message = "Failed.";
                        omodel.id = "0";
                    }
                    else if (omodel1.status == "200")
                    {
                        SqlCommand sqlcmd1 = new SqlCommand();
                        SqlConnection sqlcon1 = new SqlConnection(con);
                        sqlcon1.Open();
                        sqlcmd1 = new SqlCommand("PRC_CustomerJobStatusInsertUpdate", sqlcon1);
                        sqlcmd1.Parameters.Add("@Action", "UpdateERPId");
                        sqlcmd1.Parameters.Add("@StatusID", dt.Rows[0]["ID"].ToString());
                        sqlcmd1.Parameters.Add("@ERP_ID", omodel1.id);
                        sqlcmd1.CommandType = CommandType.StoredProcedure;
                        sqlcmd1.ExecuteNonQuery();
                        da.Fill(dt);
                        sqlcon1.Close();
                    }
                }
                else
                {
                    omodel.status = "205";
                    omodel.message = "Failed.";
                    omodel.id = "0";
                }
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage WorkCompletedSetiings(CompletedSetiings model)
        {
            CompletedSetiingsOutPut omodel = new CompletedSetiingsOutPut();
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
                sqlcmd = new SqlCommand("PRC_CustomerJobList", sqlcon);
                sqlcmd.Parameters.Add("@Action", "WorkCompletedSetiings");
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@job_id", model.id);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.isAttachmentMandatory = Convert.ToBoolean(dt.Rows[0]["isAttachmentMandatory"]);
                    omodel.phone_no = Convert.ToString(dt.Rows[0]["phone_no"]);
                    omodel.status = "200";
                    omodel.message = "Successfully get settings.";
                }
                else
                {
                    omodel.status = "205";
                    omodel.message = "Failed.";
                }
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage Customerlist(CustomerlistInput model)
        {
            CustomerListOutput omodel = new CustomerListOutput();
            List<UserCustomerList> oview = new List<UserCustomerList>();

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
                sqlcmd = new SqlCommand("PRC_CustomerJobList", sqlcon);
                sqlcmd.Parameters.Add("@Action", "CustomerJobList");
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<UserCustomerList>(dt);
                    omodel.customer_list = oview;
                    omodel.status = "200";
                    omodel.message = "Successfully get customer list.";
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
        public HttpResponseMessage SubmitWorkUnhold(WorkUnholdInput model)
        {
            String ERPAPIBSAEURL = System.Configuration.ConfigurationSettings.AppSettings["ERPAPIBSAEURL"];
            String apiUrl = ERPAPIBSAEURL + "AssignScheduleStatus/SubmitWorkUnhold";
            AssignJobOutPut omodel = new AssignJobOutPut();
            AssignJobOutPut omodel1 = new AssignJobOutPut();
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
                sqlcmd = new SqlCommand("PRC_CustomerJobStatusInsertUpdate", sqlcon);
                sqlcmd.Parameters.Add("@Action", "WorkUnHold");
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@job_id", model.job_id);
                sqlcmd.Parameters.Add("@date", model.unhold_date);
                sqlcmd.Parameters.Add("@time", model.unhold_time);
                sqlcmd.Parameters.Add("@reason_hold", model.reason_unhold);
                sqlcmd.Parameters.Add("@remarks", model.remarks);
                sqlcmd.Parameters.Add("@date_time", model.date_time);
                sqlcmd.Parameters.Add("@latitude", model.latitude);
                sqlcmd.Parameters.Add("@longitude", model.longitude);
                sqlcmd.Parameters.Add("@address", model.address);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Successfully submit work unhold.";
                    omodel.id = dt.Rows[0]["ID"].ToString();

                    model.fsm_id = dt.Rows[0]["ID"].ToString();
                    string data = JsonConvert.SerializeObject(model);
                    WebClient client = new WebClient();
                    client.Headers["Content-type"] = "application/json";
                    client.Encoding = Encoding.UTF8;
                    string json = client.UploadString(apiUrl, data);
                    omodel1 = JsonConvert.DeserializeObject<AssignJobOutPut>(json);
                    if (omodel1.status == "205")
                    {
                        SqlCommand sqlcmd1 = new SqlCommand();
                        SqlConnection sqlcon1 = new SqlConnection(con);
                        sqlcon1.Open();
                        sqlcmd1 = new SqlCommand("PRC_CustomerJobStatusInsertUpdate", sqlcon1);
                        sqlcmd1.Parameters.Add("@Action", "DeleteWorkOnHold");
                        sqlcmd1.Parameters.Add("@StatusID", dt.Rows[0]["ID"].ToString());
                        sqlcmd1.CommandType = CommandType.StoredProcedure;
                        sqlcmd1.ExecuteNonQuery();
                        da.Fill(dt);
                        sqlcon1.Close();

                        omodel.status = "205";
                        omodel.message = "Failed.";
                        omodel.id = "0";
                    }
                    else if (omodel1.status == "200")
                    {
                        SqlCommand sqlcmd1 = new SqlCommand();
                        SqlConnection sqlcon1 = new SqlConnection(con);
                        sqlcon1.Open();
                        sqlcmd1 = new SqlCommand("PRC_CustomerJobStatusInsertUpdate", sqlcon1);
                        sqlcmd1.Parameters.Add("@Action", "UpdateERPId");
                        sqlcmd1.Parameters.Add("@StatusID", dt.Rows[0]["ID"].ToString());
                        sqlcmd1.Parameters.Add("@ERP_ID", omodel1.id);
                        sqlcmd1.CommandType = CommandType.StoredProcedure;
                        sqlcmd1.ExecuteNonQuery();
                        da.Fill(dt);
                        sqlcon1.Close();
                    }
                }
                else
                {
                    omodel.status = "205";
                    omodel.message = "Failed.";
                    omodel.id = "0";
                }
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage JobhistoryList(JobhistoryInput model)
        {
            Jobhistory omodel = new Jobhistory();
            List<Jobhistorylist> oview = new List<Jobhistorylist>();

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
                sqlcmd = new SqlCommand("PRC_API_AssignJobHistory", sqlcon);
                sqlcmd.Parameters.Add("@Action", "ALL");
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@start_date", model.start_date);
                sqlcmd.Parameters.Add("@end_date", model.end_date);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<Jobhistorylist>(dt);
                    omodel.history_list = oview;
                    omodel.status = "200";
                    omodel.message = "Successfully get history.";
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
        public HttpResponseMessage UpdateAssignJob(UpdateAssignJobInput model)
        {
            AssignJobOutPut omodel = new AssignJobOutPut();
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
                sqlcmd = new SqlCommand("PRC_CustomerJobList", sqlcon);
                sqlcmd.Parameters.Add("@Action", "UpdateAssignCustomerJob");
                sqlcmd.Parameters.Add("@job_id", model.job_id);
                sqlcmd.Parameters.Add("@jobcreate_date", model.date);
                sqlcmd.Parameters.Add("@user_id", model.technician_id);
                sqlcmd.Parameters.Add("@sub_userid", model.subtechnician_id);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Successfully update job.";
                }
                else
                {
                    omodel.status = "205";
                    omodel.message = "Failed";
                }
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage JobWisehistoryList(JobWisehistoryInput model)
        {
            Jobhistory omodel = new Jobhistory();
            List<Jobhistorylist> oview = new List<Jobhistorylist>();

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
                sqlcmd = new SqlCommand("PRC_API_AssignJobHistory", sqlcon);
                sqlcmd.Parameters.Add("@Action", "JOBWISE");
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@job_id", model.id);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<Jobhistorylist>(dt);
                    omodel.history_list = oview;
                    omodel.status = "200";
                    omodel.message = "Successfully get history.";
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
        public HttpResponseMessage SubmitWorkReschedule(SubmitWorkRescheduleInput model)
        {
            String ERPAPIBSAEURL = System.Configuration.ConfigurationSettings.AppSettings["ERPAPIBSAEURL"];
            String apiUrl = ERPAPIBSAEURL + "AssignScheduleStatus/SubmitWorkReschedule";
            AssignJobOutPut omodel = new AssignJobOutPut();
            AssignJobOutPut omodel1 = new AssignJobOutPut();
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
                sqlcmd = new SqlCommand("PRC_CustomerJobStatusInsertUpdate", sqlcon);
                sqlcmd.Parameters.Add("@Action", "WorkReschedule");
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@job_id", model.job_id);
                sqlcmd.Parameters.Add("@date", model.reschedule_date);
                sqlcmd.Parameters.Add("@time", model.reschedule_time);
                sqlcmd.Parameters.Add("@reason_hold", model.resc_reason);
                sqlcmd.Parameters.Add("@remarks", model.remarks);
                sqlcmd.Parameters.Add("@date_time", model.date_time);
                sqlcmd.Parameters.Add("@latitude", model.latitude);
                sqlcmd.Parameters.Add("@longitude", model.longitude);
                sqlcmd.Parameters.Add("@address", model.address);

                sqlcmd.Parameters.Add("@RESCHEDULE_USER", model.reschedule_by);
                sqlcmd.Parameters.Add("@RESCHEDULE_BY", model.user);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Successfully submit work Reschedule.";
                    omodel.id = dt.Rows[0]["ID"].ToString();

                    model.fsm_id = dt.Rows[0]["ID"].ToString();
                    string data = JsonConvert.SerializeObject(model);
                    WebClient client = new WebClient();
                    client.Headers["Content-type"] = "application/json";
                    client.Encoding = Encoding.UTF8;
                    string json = client.UploadString(apiUrl, data);
                    omodel1 = JsonConvert.DeserializeObject<AssignJobOutPut>(json);
                    if (omodel1.status == "205")
                    {
                        SqlCommand sqlcmd1 = new SqlCommand();
                        SqlConnection sqlcon1 = new SqlConnection(con);
                        sqlcon1.Open();
                        sqlcmd1 = new SqlCommand("PRC_CustomerJobStatusInsertUpdate", sqlcon1);
                        sqlcmd1.Parameters.Add("@Action", "DeleteWorkReschedule");
                        sqlcmd1.Parameters.Add("@StatusID", dt.Rows[0]["ID"].ToString());
                        sqlcmd1.CommandType = CommandType.StoredProcedure;
                        sqlcmd1.ExecuteNonQuery();
                        da.Fill(dt);
                        sqlcon1.Close();

                        omodel.status = "205";
                        omodel.message = "Failed.";
                        omodel.id = "0";
                    }
                    else if (omodel1.status == "200")
                    {
                        SqlCommand sqlcmd1 = new SqlCommand();
                        SqlConnection sqlcon1 = new SqlConnection(con);
                        sqlcon1.Open();
                        sqlcmd1 = new SqlCommand("PRC_CustomerJobStatusInsertUpdate", sqlcon1);
                        sqlcmd1.Parameters.Add("@Action", "UpdateERPId");
                        sqlcmd1.Parameters.Add("@StatusID", dt.Rows[0]["ID"].ToString());
                        sqlcmd1.Parameters.Add("@ERP_ID", omodel1.id);
                        sqlcmd1.CommandType = CommandType.StoredProcedure;
                        sqlcmd1.ExecuteNonQuery();
                        da.Fill(dt);
                        sqlcon1.Close();
                    }
                }
                else
                {
                    omodel.status = "205";
                    omodel.message = "Failed.";
                    omodel.id = "0";
                }
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }
    }
}

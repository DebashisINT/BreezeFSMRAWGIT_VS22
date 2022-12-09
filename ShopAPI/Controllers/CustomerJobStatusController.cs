using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ShopAPI.Controllers
{
    public class CustomerJobStatusController : Controller
    {
        public JsonResult WorkInProgressSubmit(CustomerJobStatusAttachment model)
        {
            String ERPAPIBSAEURL = System.Configuration.ConfigurationSettings.AppSettings["ERPAPIBSAEURL"];
            String apiUrl = ERPAPIBSAEURL + "AssignScheduleStatus/WorkInProgressSubmitMultipart";

            ActivityAddOutput omodel = new ActivityAddOutput();
            string attachmentName = "";
            string Image = "";
            List<CustomerJobStatusImagees> omedl2 = new List<CustomerJobStatusImagees>();
            try
            {
                var details = JObject.Parse(model.data);
                var hhhh = Newtonsoft.Json.JsonConvert.DeserializeObject<WorkInProgressInput>(model.data);
                //attachment = model.attachment.FileName;
                //Image = model.photo.FileName;
                //if (!string.IsNullOrEmpty(model.data))
                //{
                //    attachment = hhhh.session_token + '_' + hhhh.user_id + '_' + attachment;
                //    string vPath = Path.Combine(Server.MapPath("~/CommonFolder"), attachment);
                //    model.attachment.SaveAs(vPath);

                //    Image = hhhh.session_token + '_' + hhhh.user_id + '_' + Image;
                //    string vPath2 = Path.Combine(Server.MapPath("~/CommonFolder"), Image);
                //    model.attachment.SaveAs(vPath2);
                //}

                if (!string.IsNullOrEmpty(model.data))
                {
                    for (int i = 0; i < model.attachments.Count; i++)
                    {
                        attachmentName = model.attachments[i].FileName;
                        attachmentName = hhhh.session_token + '_' + hhhh.user_id + '_' + attachmentName;
                        string vPath = Path.Combine(Server.MapPath("~/CommonFolder/JobAttachment"), attachmentName);
                        model.attachments[i].SaveAs(vPath);

                        omedl2.Add(new CustomerJobStatusImagees()
                        {
                            attachment = attachmentName,
                        });
                    }
                }

                string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);

                string sessionId = "";

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_CustomerJobStatusInsertUpdate", sqlcon);
                sqlcmd.Parameters.Add("@Action", "WorkInProgress");
                sqlcmd.Parameters.Add("@user_id", hhhh.user_id);
                sqlcmd.Parameters.Add("@job_id", hhhh.job_id);
                sqlcmd.Parameters.Add("@date", hhhh.start_date);
                sqlcmd.Parameters.Add("@time", hhhh.start_time);
                sqlcmd.Parameters.Add("@service_due", hhhh.service_due);
                sqlcmd.Parameters.Add("@service_completed", hhhh.service_completed);
                sqlcmd.Parameters.Add("@next_date", hhhh.next_date);
                sqlcmd.Parameters.Add("@next_time", hhhh.next_time);
                sqlcmd.Parameters.Add("@remarks", hhhh.remarks);
                sqlcmd.Parameters.Add("@date_time", hhhh.date_time);
                sqlcmd.Parameters.Add("@latitude", hhhh.latitude);
                sqlcmd.Parameters.Add("@longitude", hhhh.longitude);
                sqlcmd.Parameters.Add("@address", hhhh.address);
                sqlcmd.Parameters.Add("@ATTACHMENT", JsonXML);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Successfully submit work in progress.";
                    omodel.id = dt.Rows[0]["ID"].ToString();

                    hhhh.fsm_id = dt.Rows[0]["ID"].ToString();

                    string data = JsonConvert.SerializeObject(hhhh);
                    HttpClient httpClient = new HttpClient();
                    MultipartFormDataContent form = new MultipartFormDataContent();
                    byte[] fileBytes = new byte[model.attachments[0].InputStream.Length + 1];

                    var fileContent = new StreamContent(model.attachments[0].InputStream);
                    form.Add(new StringContent(data), "data");
                    form.Add(fileContent, "attachments", model.attachments[0].FileName);
                    var result = httpClient.PostAsync(apiUrl, form).Result;
                    var oview = JsonConvert.DeserializeObject<ActivityAddOutput>(result.Content.ReadAsStringAsync().Result);
                    if (oview.status == "205" || oview.status == "204")
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
                    else if (oview.status == "200")
                    {
                        SqlCommand sqlcmd1 = new SqlCommand();
                        SqlConnection sqlcon1 = new SqlConnection(con);
                        sqlcon1.Open();
                        sqlcmd1 = new SqlCommand("PRC_CustomerJobStatusInsertUpdate", sqlcon1);
                        sqlcmd1.Parameters.Add("@Action", "UpdateERPId");
                        sqlcmd1.Parameters.Add("@StatusID", dt.Rows[0]["ID"].ToString());
                        sqlcmd1.Parameters.Add("@ERP_ID", oview.id);
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
            }
            catch (Exception msg)
            {
                omodel.status = "204" + attachmentName;
                omodel.message = msg.Message;
            }
            return Json(omodel);
        }

        public JsonResult WorkOnHoldSubmit(CustomerJobStatusAttachment model)
        {
            String ERPAPIBSAEURL = System.Configuration.ConfigurationSettings.AppSettings["ERPAPIBSAEURL"];
            String apiUrl = ERPAPIBSAEURL + "AssignScheduleStatus/WorkOnHoldSubmitMultipart";
            ActivityAddOutput omodel = new ActivityAddOutput();
            string attachmentName = "";
            string Image = "";
            List<CustomerJobStatusImagees> omedl2 = new List<CustomerJobStatusImagees>();
            try
            {
                var details = JObject.Parse(model.data);
                var hhhh = Newtonsoft.Json.JsonConvert.DeserializeObject<WorkOnHoldInput>(model.data);
                //attachment = model.attachment.FileName;
                //Image = model.photo.FileName;
                //if (!string.IsNullOrEmpty(model.data))
                //{
                //    attachment = hhhh.session_token + '_' + hhhh.user_id + '_' + attachment;
                //    string vPath = Path.Combine(Server.MapPath("~/CommonFolder"), attachment);
                //    model.attachment.SaveAs(vPath);

                //    Image = hhhh.session_token + '_' + hhhh.user_id + '_' + Image;
                //    string vPath2 = Path.Combine(Server.MapPath("~/CommonFolder"), Image);
                //    model.attachment.SaveAs(vPath2);
                //}
                if (!string.IsNullOrEmpty(model.data))
                {
                    for (int i = 0; i < model.attachments.Count; i++)
                    {
                        attachmentName = model.attachments[i].FileName;
                        attachmentName = hhhh.session_token + '_' + hhhh.user_id + '_' + attachmentName;
                        string vPath = Path.Combine(Server.MapPath("~/CommonFolder/JobAttachment"), attachmentName);
                        model.attachments[i].SaveAs(vPath);

                        omedl2.Add(new CustomerJobStatusImagees()
                        {
                            attachment = attachmentName,
                        });
                    }
                }
                string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);

                string sessionId = "";

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_CustomerJobStatusInsertUpdate", sqlcon);
                sqlcmd.Parameters.Add("@Action", "WorkOnHold");
                sqlcmd.Parameters.Add("@user_id", hhhh.user_id);
                sqlcmd.Parameters.Add("@job_id", hhhh.job_id);
                sqlcmd.Parameters.Add("@date", hhhh.hold_date);
                sqlcmd.Parameters.Add("@time", hhhh.hold_time);
                sqlcmd.Parameters.Add("@reason_hold", hhhh.reason_hold);
                sqlcmd.Parameters.Add("@remarks", hhhh.remarks);
                sqlcmd.Parameters.Add("@date_time", hhhh.date_time);
                sqlcmd.Parameters.Add("@latitude", hhhh.latitude);
                sqlcmd.Parameters.Add("@longitude", hhhh.longitude);
                sqlcmd.Parameters.Add("@address", hhhh.address);
                sqlcmd.Parameters.Add("@ATTACHMENT", JsonXML);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Successfully submit work on hold.";
                    omodel.id = dt.Rows[0]["ID"].ToString();

                    hhhh.fsm_id = dt.Rows[0]["ID"].ToString();

                    string data = JsonConvert.SerializeObject(hhhh);
                    HttpClient httpClient = new HttpClient();
                    MultipartFormDataContent form = new MultipartFormDataContent();
                    byte[] fileBytes = new byte[model.attachments[0].InputStream.Length + 1];

                    var fileContent = new StreamContent(model.attachments[0].InputStream);
                    form.Add(new StringContent(data), "data");
                    form.Add(fileContent, "attachments", model.attachments[0].FileName);
                    var result = httpClient.PostAsync(apiUrl, form).Result;
                    var oview = JsonConvert.DeserializeObject<ActivityAddOutput>(result.Content.ReadAsStringAsync().Result);
                    if (oview.status == "205" || oview.status == "204")
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
                    else if (oview.status == "200")
                    {
                        SqlCommand sqlcmd1 = new SqlCommand();
                        SqlConnection sqlcon1 = new SqlConnection(con);
                        sqlcon1.Open();
                        sqlcmd1 = new SqlCommand("PRC_CustomerJobStatusInsertUpdate", sqlcon1);
                        sqlcmd1.Parameters.Add("@Action", "UpdateERPId");
                        sqlcmd1.Parameters.Add("@StatusID", dt.Rows[0]["ID"].ToString());
                        sqlcmd1.Parameters.Add("@ERP_ID", oview.id);
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
            }
            catch (Exception msg)
            {
                omodel.status = "204" + attachmentName;
                omodel.message = msg.Message;
            }
            return Json(omodel);
        }

        public JsonResult WorkOnCompletedSubmit(CustomerJobStatusAttachment model)
        {
            String ERPAPIBSAEURL = System.Configuration.ConfigurationSettings.AppSettings["ERPAPIBSAEURL"];
            String apiUrl = ERPAPIBSAEURL + "AssignScheduleStatus/WorkOnCompletedSubmitMultipart";
            ActivityAddOutput omodel = new ActivityAddOutput();
            string attachmentName = "";
            string Image = "";
            List<CustomerJobStatusImagees> omedl2 = new List<CustomerJobStatusImagees>();
            try
            {
                var details = JObject.Parse(model.data);
                var hhhh = Newtonsoft.Json.JsonConvert.DeserializeObject<WorkOnCompletedInput>(model.data);
                //attachment = model.attachment.FileName;
                //Image = model.photo.FileName;
                //if (!string.IsNullOrEmpty(model.data))
                //{
                //    attachment = hhhh.session_token + '_' + hhhh.user_id + '_' + attachment;
                //    string vPath = Path.Combine(Server.MapPath("~/CommonFolder"), attachment);
                //    model.attachment.SaveAs(vPath);

                //    Image = hhhh.session_token + '_' + hhhh.user_id + '_' + Image;
                //    string vPath2 = Path.Combine(Server.MapPath("~/CommonFolder"), Image);
                //    model.attachment.SaveAs(vPath2);
                //}
                if (!string.IsNullOrEmpty(model.data))
                {
                    for (int i = 0; i < model.attachments.Count; i++)
                    {
                        attachmentName = model.attachments[i].FileName;
                        attachmentName = hhhh.session_token + '_' + hhhh.user_id + '_' + attachmentName;
                        string vPath = Path.Combine(Server.MapPath("~/CommonFolder/JobAttachment"), attachmentName);
                        model.attachments[i].SaveAs(vPath);

                        omedl2.Add(new CustomerJobStatusImagees()
                        {
                            attachment = attachmentName,
                        });
                    }
                }
                string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);

                string sessionId = "";

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_CustomerJobStatusInsertUpdate", sqlcon);
                sqlcmd.Parameters.Add("@Action", "WorkOnCompleted");
                sqlcmd.Parameters.Add("@user_id", hhhh.user_id);
                sqlcmd.Parameters.Add("@job_id", hhhh.job_id);
                sqlcmd.Parameters.Add("@date", hhhh.finish_date);
                sqlcmd.Parameters.Add("@time", hhhh.finish_time);
                sqlcmd.Parameters.Add("@remarks", hhhh.remarks);
                sqlcmd.Parameters.Add("@date_time", hhhh.date_time);
                sqlcmd.Parameters.Add("@latitude", hhhh.latitude);
                sqlcmd.Parameters.Add("@longitude", hhhh.longitude);
                sqlcmd.Parameters.Add("@address", hhhh.address);
                sqlcmd.Parameters.Add("@ATTACHMENT", JsonXML);
                sqlcmd.Parameters.Add("@phone_no", hhhh.phone_no);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Successfully submit work completed.";
                    omodel.id = dt.Rows[0]["ID"].ToString();

                    hhhh.fsm_id = dt.Rows[0]["ID"].ToString();

                    string data = JsonConvert.SerializeObject(hhhh);
                    HttpClient httpClient = new HttpClient();
                    MultipartFormDataContent form = new MultipartFormDataContent();
                    byte[] fileBytes = new byte[model.attachments[0].InputStream.Length + 1];

                    var fileContent = new StreamContent(model.attachments[0].InputStream);
                    form.Add(new StringContent(data), "data");
                    form.Add(fileContent, "attachments", model.attachments[0].FileName);
                    var result = httpClient.PostAsync(apiUrl, form).Result;
                    var oview = JsonConvert.DeserializeObject<ActivityAddOutput>(result.Content.ReadAsStringAsync().Result);
                    if (oview.status == "205" || oview.status == "204")
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
                    else if (oview.status == "200")
                    {
                        SqlCommand sqlcmd1 = new SqlCommand();
                        SqlConnection sqlcon1 = new SqlConnection(con);
                        sqlcon1.Open();
                        sqlcmd1 = new SqlCommand("PRC_CustomerJobStatusInsertUpdate", sqlcon1);
                        sqlcmd1.Parameters.Add("@Action", "UpdateERPId");
                        sqlcmd1.Parameters.Add("@StatusID", dt.Rows[0]["ID"].ToString());
                        sqlcmd1.Parameters.Add("@ERP_ID", oview.id);
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
            }
            catch (Exception msg)
            {
                omodel.status = "204" + attachmentName;
                omodel.message = msg.Message;
            }
            return Json(omodel);
        }

        public JsonResult WorkCancelledSubmit(CustomerJobStatusAttachment model)
        {
            String ERPAPIBSAEURL = System.Configuration.ConfigurationSettings.AppSettings["ERPAPIBSAEURL"];
            String apiUrl = ERPAPIBSAEURL + "AssignScheduleStatus/WorkCancelledSubmitMultipart";
            ActivityAddOutput omodel = new ActivityAddOutput();
            string attachmentName = "";
            string Image = "";
            List<CustomerJobStatusImagees> omedl2 = new List<CustomerJobStatusImagees>();
            try
            {
                var details = JObject.Parse(model.data);
                var hhhh = Newtonsoft.Json.JsonConvert.DeserializeObject<WorkCancelledInput>(model.data);
                //attachment = model.attachment.FileName;
                //Image = model.photo.FileName;
                //if (!string.IsNullOrEmpty(model.data))
                //{
                //    attachment = hhhh.session_token + '_' + hhhh.user_id + '_' + attachment;
                //    string vPath = Path.Combine(Server.MapPath("~/CommonFolder"), attachment);
                //    model.attachment.SaveAs(vPath);

                //    Image = hhhh.session_token + '_' + hhhh.user_id + '_' + Image;
                //    string vPath2 = Path.Combine(Server.MapPath("~/CommonFolder"), Image);
                //    model.attachment.SaveAs(vPath2);
                //}

                if (!string.IsNullOrEmpty(model.data))
                {
                    for (int i = 0; i < model.attachments.Count; i++)
                    {
                        attachmentName = model.attachments[i].FileName;
                        attachmentName = hhhh.session_token + '_' + hhhh.user_id + '_' + attachmentName;
                        string vPath = Path.Combine(Server.MapPath("~/CommonFolder/JobAttachment"), attachmentName);
                        model.attachments[i].SaveAs(vPath);

                        omedl2.Add(new CustomerJobStatusImagees()
                        {
                            attachment = attachmentName,
                        });
                    }
                }
                string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);

                string sessionId = "";

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_CustomerJobStatusInsertUpdate", sqlcon);
                sqlcmd.Parameters.Add("@Action", "WorkCancelled");
                sqlcmd.Parameters.Add("@user_id", hhhh.user_id);
                sqlcmd.Parameters.Add("@job_id", hhhh.job_id);
                sqlcmd.Parameters.Add("@date", hhhh.date);
                sqlcmd.Parameters.Add("@time", hhhh.time);
                sqlcmd.Parameters.Add("@cancel_reason", hhhh.cancel_reason);
                sqlcmd.Parameters.Add("@remarks", hhhh.remarks);
                sqlcmd.Parameters.Add("@date_time", hhhh.date_time);
                sqlcmd.Parameters.Add("@latitude", hhhh.latitude);
                sqlcmd.Parameters.Add("@longitude", hhhh.longitude);
                sqlcmd.Parameters.Add("@address", hhhh.address);
                sqlcmd.Parameters.Add("@ATTACHMENT", JsonXML);

                sqlcmd.Parameters.Add("@CANCELLED_USER", hhhh.cancelled_by);
                sqlcmd.Parameters.Add("@CANCELLED_BY", hhhh.user);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Successfully submit work cancelled.";
                    omodel.id = dt.Rows[0]["ID"].ToString();

                    hhhh.fsm_id = dt.Rows[0]["ID"].ToString();

                    string data = JsonConvert.SerializeObject(hhhh);
                    HttpClient httpClient = new HttpClient();
                    MultipartFormDataContent form = new MultipartFormDataContent();
                    byte[] fileBytes = new byte[model.attachments[0].InputStream.Length + 1];

                    var fileContent = new StreamContent(model.attachments[0].InputStream);
                    form.Add(new StringContent(data), "data");
                    form.Add(fileContent, "attachments", model.attachments[0].FileName);
                    var result = httpClient.PostAsync(apiUrl, form).Result;
                    var oview = JsonConvert.DeserializeObject<ActivityAddOutput>(result.Content.ReadAsStringAsync().Result);
                    if (oview.status == "205" || oview.status == "204")
                    {
                        SqlCommand sqlcmd1 = new SqlCommand();
                        SqlConnection sqlcon1 = new SqlConnection(con);
                        sqlcon1.Open();
                        sqlcmd1 = new SqlCommand("PRC_CustomerJobStatusInsertUpdate", sqlcon1);
                        sqlcmd1.Parameters.Add("@Action", "DeleteWorkCancelled");
                        sqlcmd1.Parameters.Add("@StatusID", dt.Rows[0]["ID"].ToString());
                        sqlcmd1.CommandType = CommandType.StoredProcedure;
                        sqlcmd1.ExecuteNonQuery();
                        da.Fill(dt);
                        sqlcon1.Close();

                        omodel.status = "205";
                        omodel.message = "Failed.";
                        omodel.id = "0";
                    }
                    else if (oview.status == "200")
                    {
                        SqlCommand sqlcmd1 = new SqlCommand();
                        SqlConnection sqlcon1 = new SqlConnection(con);
                        sqlcon1.Open();
                        sqlcmd1 = new SqlCommand("PRC_CustomerJobStatusInsertUpdate", sqlcon1);
                        sqlcmd1.Parameters.Add("@Action", "UpdateERPId");
                        sqlcmd1.Parameters.Add("@StatusID", dt.Rows[0]["ID"].ToString());
                        sqlcmd1.Parameters.Add("@ERP_ID", oview.id);
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
            }
            catch (Exception msg)
            {
                omodel.status = "204" + attachmentName;
                omodel.message = msg.Message;
            }
            return Json(omodel);
        }

        public JsonResult UpdateReview(CustomerJobStatusAttachment model)
        {
            String ERPAPIBSAEURL = System.Configuration.ConfigurationSettings.AppSettings["ERPAPIBSAEURL"];
            String apiUrl = ERPAPIBSAEURL + "AssignScheduleStatus/UpdateReviewMultipart";
            ActivityAddOutput omodel = new ActivityAddOutput();
            string attachmentName = "";
            string Image = "";
            List<CustomerJobStatusImagees> omedl2 = new List<CustomerJobStatusImagees>();
            try
            {
                var details = JObject.Parse(model.data);
                var hhhh = Newtonsoft.Json.JsonConvert.DeserializeObject<ReviewInput>(model.data);
                //attachment = model.attachment.FileName;
                //Image = model.photo.FileName;
                //if (!string.IsNullOrEmpty(model.data))
                //{
                //    attachment = hhhh.session_token + '_' + hhhh.user_id + '_' + attachment;
                //    string vPath = Path.Combine(Server.MapPath("~/CommonFolder"), attachment);
                //    model.attachment.SaveAs(vPath);

                //    Image = hhhh.session_token + '_' + hhhh.user_id + '_' + Image;
                //    string vPath2 = Path.Combine(Server.MapPath("~/CommonFolder"), Image);
                //    model.attachment.SaveAs(vPath2);
                //}
                if (!string.IsNullOrEmpty(model.data))
                {
                    for (int i = 0; i < model.attachments.Count; i++)
                    {
                        attachmentName = model.attachments[i].FileName;
                        attachmentName = hhhh.session_token + '_' + hhhh.user_id + '_' + attachmentName;
                        string vPath = Path.Combine(Server.MapPath("~/CommonFolder/JobAttachment"), attachmentName);
                        model.attachments[i].SaveAs(vPath);

                        omedl2.Add(new CustomerJobStatusImagees()
                        {
                            attachment = attachmentName,
                        });
                    }
                }
                string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);

                string sessionId = "";

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_CustomerJobStatusInsertUpdate", sqlcon);
                sqlcmd.Parameters.Add("@Action", "UpdateReview");
                sqlcmd.Parameters.Add("@user_id", hhhh.user_id);
                sqlcmd.Parameters.Add("@job_id", hhhh.job_id);
                sqlcmd.Parameters.Add("@review", hhhh.review);
                sqlcmd.Parameters.Add("@rate", hhhh.rate);
                sqlcmd.Parameters.Add("@date_time", hhhh.date_time);
                sqlcmd.Parameters.Add("@latitude", hhhh.latitude);
                sqlcmd.Parameters.Add("@longitude", hhhh.longitude);
                sqlcmd.Parameters.Add("@address", hhhh.address);
                sqlcmd.Parameters.Add("@ATTACHMENT", JsonXML);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Successfully update review.";
                    omodel.id = dt.Rows[0]["ID"].ToString();

                    hhhh.fsm_id = dt.Rows[0]["ID"].ToString();

                    string data = JsonConvert.SerializeObject(hhhh);
                    HttpClient httpClient = new HttpClient();
                    MultipartFormDataContent form = new MultipartFormDataContent();
                    byte[] fileBytes = new byte[model.attachments[0].InputStream.Length + 1];

                    var fileContent = new StreamContent(model.attachments[0].InputStream);
                    form.Add(new StringContent(data), "data");
                    form.Add(fileContent, "attachments", model.attachments[0].FileName);
                    var result = httpClient.PostAsync(apiUrl, form).Result;
                    var oview = JsonConvert.DeserializeObject<ActivityAddOutput>(result.Content.ReadAsStringAsync().Result);
                    if (oview.status == "205" || oview.status == "204")
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
                    else if (oview.status == "200")
                    {
                        SqlCommand sqlcmd1 = new SqlCommand();
                        SqlConnection sqlcon1 = new SqlConnection(con);
                        sqlcon1.Open();
                        sqlcmd1 = new SqlCommand("PRC_CustomerJobStatusInsertUpdate", sqlcon1);
                        sqlcmd1.Parameters.Add("@Action", "UpdateERPId");
                        sqlcmd1.Parameters.Add("@StatusID", dt.Rows[0]["ID"].ToString());
                        sqlcmd1.Parameters.Add("@ERP_ID", oview.id);
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
            }
            catch (Exception msg)
            {
                omodel.status = "204" + attachmentName;
                omodel.message = msg.Message;
            }
            return Json(omodel);
        }

        public JsonResult SubmitWorkUnhold(CustomerJobStatusAttachment model)
        {
            String ERPAPIBSAEURL = System.Configuration.ConfigurationSettings.AppSettings["ERPAPIBSAEURL"];
            String apiUrl = ERPAPIBSAEURL + "AssignScheduleStatus/SubmitWorkUnhold";
            ActivityAddOutput omodel = new ActivityAddOutput();
            string attachmentName = "";
            string Image = "";
            List<CustomerJobStatusImagees> omedl2 = new List<CustomerJobStatusImagees>();
            try
            {
                var details = JObject.Parse(model.data);
                var hhhh = Newtonsoft.Json.JsonConvert.DeserializeObject<WorkUnholdInput>(model.data);
                //attachment = model.attachment.FileName;
                //Image = model.photo.FileName;
                //if (!string.IsNullOrEmpty(model.data))
                //{
                //    attachment = hhhh.session_token + '_' + hhhh.user_id + '_' + attachment;
                //    string vPath = Path.Combine(Server.MapPath("~/CommonFolder"), attachment);
                //    model.attachment.SaveAs(vPath);

                //    Image = hhhh.session_token + '_' + hhhh.user_id + '_' + Image;
                //    string vPath2 = Path.Combine(Server.MapPath("~/CommonFolder"), Image);
                //    model.attachment.SaveAs(vPath2);
                //}
                if (!string.IsNullOrEmpty(model.data))
                {
                    for (int i = 0; i < model.attachments.Count; i++)
                    {
                        attachmentName = model.attachments[i].FileName;
                        attachmentName = hhhh.session_token + '_' + hhhh.user_id + '_' + attachmentName;
                        string vPath = Path.Combine(Server.MapPath("~/CommonFolder/JobAttachment"), attachmentName);
                        model.attachments[i].SaveAs(vPath);

                        omedl2.Add(new CustomerJobStatusImagees()
                        {
                            attachment = attachmentName,
                        });
                    }
                }
                string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);

                string sessionId = "";

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_CustomerJobStatusInsertUpdate", sqlcon);
                sqlcmd.Parameters.Add("@Action", "WorkUnHold");
                sqlcmd.Parameters.Add("@user_id", hhhh.user_id);
                sqlcmd.Parameters.Add("@job_id", hhhh.job_id);
                sqlcmd.Parameters.Add("@date", hhhh.unhold_date);
                sqlcmd.Parameters.Add("@time", hhhh.unhold_time);
                sqlcmd.Parameters.Add("@reason_hold", hhhh.reason_unhold);
                sqlcmd.Parameters.Add("@remarks", hhhh.remarks);
                sqlcmd.Parameters.Add("@date_time", hhhh.date_time);
                sqlcmd.Parameters.Add("@latitude", hhhh.latitude);
                sqlcmd.Parameters.Add("@longitude", hhhh.longitude);
                sqlcmd.Parameters.Add("@address", hhhh.address);
                sqlcmd.Parameters.Add("@ATTACHMENT", JsonXML);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Successfully submit work unhold.";
                    omodel.id = dt.Rows[0]["ID"].ToString();

                    hhhh.fsm_id = dt.Rows[0]["ID"].ToString();

                    string data = JsonConvert.SerializeObject(hhhh);
                    HttpClient httpClient = new HttpClient();
                    MultipartFormDataContent form = new MultipartFormDataContent();
                    byte[] fileBytes = new byte[model.attachments[0].InputStream.Length + 1];

                    var fileContent = new StreamContent(model.attachments[0].InputStream);
                    form.Add(new StringContent(data), "data");
                    form.Add(fileContent, "attachments", model.attachments[0].FileName);
                    var result = httpClient.PostAsync(apiUrl, form).Result;
                    var oview = JsonConvert.DeserializeObject<ActivityAddOutput>(result.Content.ReadAsStringAsync().Result);
                    if (oview.status == "205" || oview.status == "204")
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
                    else if (oview.status == "200")
                    {
                        SqlCommand sqlcmd1 = new SqlCommand();
                        SqlConnection sqlcon1 = new SqlConnection(con);
                        sqlcon1.Open();
                        sqlcmd1 = new SqlCommand("PRC_CustomerJobStatusInsertUpdate", sqlcon1);
                        sqlcmd1.Parameters.Add("@Action", "UpdateERPId");
                        sqlcmd1.Parameters.Add("@StatusID", dt.Rows[0]["ID"].ToString());
                        sqlcmd1.Parameters.Add("@ERP_ID", oview.id);
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
            }
            catch (Exception msg)
            {
                omodel.status = "204" + attachmentName;
                omodel.message = msg.Message;
            }
            return Json(omodel);
        }

        public JsonResult SubmitWorkReschedule(CustomerJobStatusAttachment model)
        {
            String ERPAPIBSAEURL = System.Configuration.ConfigurationSettings.AppSettings["ERPAPIBSAEURL"];
            String apiUrl = ERPAPIBSAEURL + "AssignScheduleStatus/SubmitWorkRescheduleMultipart";
            ActivityAddOutput omodel = new ActivityAddOutput();
            string attachmentName = "";
            string Image = "";
            List<CustomerJobStatusImagees> omedl2 = new List<CustomerJobStatusImagees>();
            try
            {
                var details = JObject.Parse(model.data);
                var hhhh = Newtonsoft.Json.JsonConvert.DeserializeObject<SubmitWorkRescheduleInput>(model.data);
                //attachment = model.attachment.FileName;
                //Image = model.photo.FileName;
                //if (!string.IsNullOrEmpty(model.data))
                //{
                //    attachment = hhhh.session_token + '_' + hhhh.user_id + '_' + attachment;
                //    string vPath = Path.Combine(Server.MapPath("~/CommonFolder"), attachment);
                //    model.attachment.SaveAs(vPath);

                //    Image = hhhh.session_token + '_' + hhhh.user_id + '_' + Image;
                //    string vPath2 = Path.Combine(Server.MapPath("~/CommonFolder"), Image);
                //    model.attachment.SaveAs(vPath2);
                //}
                if (!string.IsNullOrEmpty(model.data))
                {
                    for (int i = 0; i < model.attachments.Count; i++)
                    {
                        attachmentName = model.attachments[i].FileName;
                        attachmentName = hhhh.session_token + '_' + hhhh.user_id + '_' + attachmentName;
                        string vPath = Path.Combine(Server.MapPath("~/CommonFolder/JobAttachment"), attachmentName);
                        model.attachments[i].SaveAs(vPath);

                        omedl2.Add(new CustomerJobStatusImagees()
                        {
                            attachment = attachmentName,
                        });
                    }
                }
                string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);

                string sessionId = "";

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_CustomerJobStatusInsertUpdate", sqlcon);
                sqlcmd.Parameters.Add("@Action", "WorkReschedule");
                sqlcmd.Parameters.Add("@user_id", hhhh.user_id);
                sqlcmd.Parameters.Add("@job_id", hhhh.job_id);
                sqlcmd.Parameters.Add("@date", hhhh.reschedule_date);
                sqlcmd.Parameters.Add("@time", hhhh.reschedule_time);
                sqlcmd.Parameters.Add("@reason_hold", hhhh.resc_reason);
                sqlcmd.Parameters.Add("@remarks", hhhh.remarks);
                sqlcmd.Parameters.Add("@date_time", hhhh.date_time);
                sqlcmd.Parameters.Add("@latitude", hhhh.latitude);
                sqlcmd.Parameters.Add("@longitude", hhhh.longitude);
                sqlcmd.Parameters.Add("@address", hhhh.address);
                sqlcmd.Parameters.Add("@ATTACHMENT", JsonXML);

                sqlcmd.Parameters.Add("@RESCHEDULE_USER", hhhh.reschedule_by);
                sqlcmd.Parameters.Add("@RESCHEDULE_BY", hhhh.user);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Successfully submit work Reschedule.";
                    omodel.id = dt.Rows[0]["ID"].ToString();

                    hhhh.fsm_id = dt.Rows[0]["ID"].ToString();

                    string data = JsonConvert.SerializeObject(hhhh);
                    HttpClient httpClient = new HttpClient();
                    MultipartFormDataContent form = new MultipartFormDataContent();
                    byte[] fileBytes = new byte[model.attachments[0].InputStream.Length + 1];

                    var fileContent = new StreamContent(model.attachments[0].InputStream);
                    form.Add(new StringContent(data), "data");
                    form.Add(fileContent, "attachments", model.attachments[0].FileName);
                    var result = httpClient.PostAsync(apiUrl, form).Result;
                    var oview = JsonConvert.DeserializeObject<ActivityAddOutput>(result.Content.ReadAsStringAsync().Result);
                    if (oview.status == "205" || oview.status == "204")
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
                    else if (oview.status == "200")
                    {
                        SqlCommand sqlcmd1 = new SqlCommand();
                        SqlConnection sqlcon1 = new SqlConnection(con);
                        sqlcon1.Open();
                        sqlcmd1 = new SqlCommand("PRC_CustomerJobStatusInsertUpdate", sqlcon1);
                        sqlcmd1.Parameters.Add("@Action", "UpdateERPId");
                        sqlcmd1.Parameters.Add("@StatusID", dt.Rows[0]["ID"].ToString());
                        sqlcmd1.Parameters.Add("@ERP_ID", oview.id);
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
            }
            catch (Exception msg)
            {
                omodel.status = "204" + attachmentName;
                omodel.message = msg.Message;
            }
            return Json(omodel);
        }
	}
}
using ShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web;
using System.Web.Http;


namespace ShopAPI.Controllers
{
    public class AchivemetReportController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage AchivementReportList(AchivementReportInput model)
        {
            AchivementReportOutput omodel = new AchivementReportOutput();
            Datalists odata = new Datalists();

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

                List<AchivementMemberList> omedl2 = new List<AchivementMemberList>();

                DataSet ds = new DataSet();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_API_ACHIVEMENTREPORT", sqlcon);
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@FromDate", model.from_date);
                sqlcmd.Parameters.Add("@TODATE", model.to_date);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(ds);
                sqlcon.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<AchivementMemberList> oview = new List<AchivementMemberList>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        List<AchivementDetailsList> dtls = new List<AchivementDetailsList>();
                        for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                        {
                            if (Convert.ToString(ds.Tables[1].Rows[j]["USER_ID"]) == Convert.ToString(ds.Tables[0].Rows[i]["user_id"]))
                            {
                                dtls.Add(new AchivementDetailsList()
                                {
                                    cust_name = Convert.ToString(ds.Tables[1].Rows[j]["cust_name"]),
                                    visit_time = Convert.ToString(ds.Tables[1].Rows[j]["visit_time"]),
                                    visit_date = Convert.ToString(ds.Tables[1].Rows[j]["visit_date"]),
                                    stage = Convert.ToString(ds.Tables[1].Rows[j]["Stage"])
                                });
                            }
                        }

                        oview.Add(new AchivementMemberList()
                        {
                            member_name = Convert.ToString(ds.Tables[0].Rows[i]["user_name"]),
                            member_id = Convert.ToString(ds.Tables[0].Rows[i]["user_id"]),
                            stage_count = Convert.ToString(ds.Tables[0].Rows[i]["stage_count"]),
                            report_to = Convert.ToString(ds.Tables[0].Rows[i]["super_name"]),
                            achv_details_list = dtls
                        });
                    }
                    omodel.achv_report_list = oview;
                    omodel.status = "200";
                    omodel.message = "Achivement Report List.";
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
        public HttpResponseMessage TargetAchivementReportList(AchivementReportInput model)
        {
            AchivementTargetReportOutput omodel = new AchivementTargetReportOutput();
            Datalists odata = new Datalists();

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

                DataSet ds = new DataSet();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_API_ACHIVEMENTVSTARGETREPORT", sqlcon);
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@FromDate", model.from_date);
                sqlcmd.Parameters.Add("@TODATE", model.to_date);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(ds);
                sqlcon.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<AchivementTargetMemberList> oview = new List<AchivementTargetMemberList>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        List<AchivementTargetDetailsList> dtls = new List<AchivementTargetDetailsList>();
                        AchivementTargetDetailsList achv = new AchivementTargetDetailsList();
                        for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                        {
                            if (Convert.ToString(ds.Tables[1].Rows[j]["USER_ID"]) == Convert.ToString(ds.Tables[0].Rows[i]["user_id"]))
                            {
                               
                                dtls.Add(new AchivementTargetDetailsList()
                                {
                                    booking = Convert.ToString(ds.Tables[1].Rows[j]["BOOKING"]), //+ "|" + Convert.ToString(ds.Tables[1].Rows[j]["BOOKING_ACH"]),
                                    enquiry = Convert.ToString(ds.Tables[1].Rows[j]["ENQ"]),// + "|" + Convert.ToString(ds.Tables[1].Rows[j]["ENQ_ACH"]),
                                    lead = Convert.ToString(ds.Tables[1].Rows[j]["LEAD"]),// + "|" + Convert.ToString(ds.Tables[1].Rows[j]["LEAD_ACH"]),
                                    test_drive = Convert.ToString(ds.Tables[1].Rows[j]["TEST_DRIVE"]),// + "|" + Convert.ToString(ds.Tables[1].Rows[j]["TD_ACH"]),
                                    retail = Convert.ToString(ds.Tables[1].Rows[j]["RETAIL"])// + "|" + Convert.ToString(ds.Tables[1].Rows[j]["RT_ACH"])
                                });
                            }
                        }
                       
                        oview.Add(new AchivementTargetMemberList()
                        {
                            member_name = Convert.ToString(ds.Tables[0].Rows[i]["user_name"]),
                            member_id = Convert.ToString(ds.Tables[0].Rows[i]["user_id"]),
                            report_to = Convert.ToString(ds.Tables[0].Rows[i]["super_name"]),
                            targ_achv_details_list = dtls
                        });
                    }
                    omodel.targ_achv_report_list = oview;
                    omodel.status = "200";
                    omodel.message = "Achivement Report List.";
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
        public HttpResponseMessage QuotationSMSMail(QuotationSMSMailInput model)
        {
            QuotationSMSMailOutput omodel = new QuotationSMSMailOutput();
            Datalists odata = new Datalists();

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

                DataSet ds = new DataSet();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_API_SendQuotationSMSandMail", sqlcon);
                if (model.isSms)
                {
                    sqlcmd.Parameters.Add("@Action", "SMS");
                }
                else
                {
                    sqlcmd.Parameters.Add("@Action", "EMAIL");
                }
                sqlcmd.Parameters.Add("@quo_id", model.quo_id);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(ds);
                sqlcon.Close();
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (model.isSms)
                    {
                        if (ds.Tables[0].Rows[0]["STATUS"].ToString() == "FAILED")
                        {
                            omodel.status = "200";
                            omodel.message = "SMS not send.";
                        }
                        else
                        {
                            omodel.status = "200";
                            omodel.message = "Successfully send sms.";
                        }                       
                    }
                    else
                    {
                        string ToEmail = ds.Tables[0].Rows[0]["EMAIL"].ToString();
                        string Subject = "Quotation " + ds.Tables[0].Rows[0]["QuotationNo"].ToString() + " for " + ds.Tables[0].Rows[0]["Model"].ToString() + " on " + ds.Tables[0].Rows[0]["QuotationDate"].ToString();
                        string CCEmail = "";
                        string BCCEmail = "";
                        string EmailBody = "Hi " + ds.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString() + ",<br />I hope you’re well!<br />Quotation number  " + ds.Tables[0].Rows[0]["QuotationNo"].ToString() + " for " + ds.Tables[0].Rows[0]["Model"].ToString() +
                            " generated on " + ds.Tables[0].Rows[0]["QuotationDate"].ToString() + ". Don’t hesitate to reach out if you have any questions." + "<br /><br />Kind regards,<br />Rajgarhia Motors";

                        if (ToEmail != "")
                        {
                            String mailsStatus = SendMail(ToEmail, CCEmail, BCCEmail, Subject, EmailBody, null, ds.Tables[1]);
                            if (mailsStatus == "Success")
                            {
                                omodel.status = "200";
                                omodel.message = "Successfully send email.";
                            }
                            else
                            {
                                omodel.status = "200";
                                omodel.message = "Email not send.";
                            }
                        }
                        else
                        {
                            omodel.status = "200";
                            omodel.message = "Email id not found.";
                        }
                    }
                }

                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }


        internal String SendMail(string ToEmail, string CCEmail, string BCCEmail, string Subject, string EmailBody, HttpFileCollectionBase files, DataTable dtFromEmailDet)
        {
            try
            {
                var Email = dtFromEmailDet.Rows[0][0].ToString();
                var Password = dtFromEmailDet.Rows[0][1].ToString();

                //var Email = "subhra.mukherjee@indusnet.co.in";
                //var Password = "subhra@12345";
                var FromWhere = dtFromEmailDet.Rows[0][2].ToString();
                var OutgoingSMTPHost = dtFromEmailDet.Rows[0][3].ToString();
                var OutgoingPort = dtFromEmailDet.Rows[0][4].ToString();
                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient(OutgoingSMTPHost);
                var FromAdd = Email;
                string[] ToAdd = ToEmail.Split(',');
                string[] CcAdd = CCEmail.Split(',');
                string[] BccAdd = BCCEmail.Split(',');
                var Body = EmailBody;
                var EmailSubject = Subject;
                mail.From = new MailAddress(FromAdd, FromWhere);

                foreach (string to in ToAdd)
                {
                    mail.To.Add(to);
                }
                foreach (string cc in CcAdd)
                {
                    if (cc != "")
                    {
                        mail.CC.Add(cc);
                    }
                }
                foreach (string bcc in BccAdd)
                {
                    if (bcc != "")
                    {
                        mail.Bcc.Add(bcc);
                    }
                }

                mail.Subject = EmailSubject;
                mail.IsBodyHtml = true;
                mail.Body = Body;
                HttpPostedFileBase file = null;
                HttpFileCollectionBase filess = files;
                if (filess != null)
                {
                    for (int i = 0; i < filess.Count; i++)
                    {

                        if (filess[i] != null && files[i].ContentLength > 0)
                        {
                            var attachment = new Attachment(filess[i].InputStream, filess[i].FileName);
                            mail.Attachments.Add(attachment);
                        }
                    }

                }


                smtp.Host = OutgoingSMTPHost.Trim();
                smtp.Port = Convert.ToInt32(OutgoingPort);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(FromAdd, Password);
                smtp.EnableSsl = true;

                smtp.Send(mail);
                smtp.Dispose();
                mail.Dispose();
                return "Success";
            }
            catch (Exception EX)
            {
                return null;
            }
        }
    }
}

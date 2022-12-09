using ShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace ShopAPI.Controllers
{
    public class LeaveApprovalController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Records(LeaveApprovalRecord model)
        {

            AttendancemanageOutput omodel = new AttendancemanageOutput();
            UserClass oview = new UserClass();
            string messagetextTobeSend = "";

            try
            {
                string token = string.Empty;
                string versionname = string.Empty;
                System.Net.Http.Headers.HttpRequestHeaders headers = this.Request.Headers;
                String tokenmatch = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];

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
                    sqlcmd = new SqlCommand("Proc_FTS_ApplyLeaveForApproval", sqlcon);
                    sqlcmd.Parameters.Add("@ACTION", "APPLYLEAVE");
                    sqlcmd.Parameters.Add("@user_id", model.user_id);
                    sqlcmd.Parameters.Add("@SessionToken", model.session_token);
                    sqlcmd.Parameters.Add("@leave_reason", model.leave_reason);
                    sqlcmd.Parameters.Add("@leave_from_date", model.leave_from_date);
                    sqlcmd.Parameters.Add("@leave_type", model.leave_type);
                    sqlcmd.Parameters.Add("@leave_to_date", model.leave_to_date);

                    sqlcmd.Parameters.Add("@leave_lat", model.leave_lat);
                    sqlcmd.Parameters.Add("@leave_long", model.leave_long);
                    sqlcmd.Parameters.Add("@leave_add", model.leave_add);
                    SqlParameter output = new SqlParameter("@output", SqlDbType.VarChar, 50);
                    output.Direction = ParameterDirection.Output;
                    sqlcmd.Parameters.Add(output);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    string Id = Convert.ToString(sqlcmd.Parameters["@output"].Value.ToString());
                    sqlcon.Close();
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToString(dt.Rows[0][0]) == "Leave Applied Successfully.")
                        {


                            String username = System.Configuration.ConfigurationSettings.AppSettings["username"];
                            String password = System.Configuration.ConfigurationSettings.AppSettings["password"];
                            String Provider = System.Configuration.ConfigurationSettings.AppSettings["Provider"];
                            String sender = System.Configuration.ConfigurationSettings.AppSettings["sender"];


                            DataTable dts = new DataTable();
                            sqlcon.Open();
                            sqlcmd = new SqlCommand("SELECT user_id,user_loginId FROM TBL_MASTER_USER USR INNER JOIN  FTS_LEAVE_APPROVER APPR ON USR.user_id=APPR.APPROVAR_ID", sqlcon);
                            da = new SqlDataAdapter(sqlcmd);
                            da.Fill(dts);
                            sqlcon.Close();

                            DataTable dtEmployeeName = new DataTable();
                            sqlcon.Open();
                            sqlcmd = new SqlCommand("select cnt_firstName +' '+cnt_lastName Neme from tbl_master_user usr inner join tbl_master_contact cnt on cnt.cnt_internalId=usr.user_contactId where user_id='" + Convert.ToString(model.user_id) + "'", sqlcon);
                            da = new SqlDataAdapter(sqlcmd);
                            da.Fill(dtEmployeeName);
                            sqlcon.Close();

                            string Emp_Name = "";
                            if (dtEmployeeName != null && dtEmployeeName.Rows.Count > 0)
                            {
                                Emp_Name = Convert.ToString(dtEmployeeName.Rows[0][0]);
                            }


                            foreach (DataRow dr in dts.Rows)
                            {
                                string baseUrl = System.Configuration.ConfigurationSettings.AppSettings["baseUrlLeave"];
                                string LongURL = baseUrl + "/oms/management/activities/leaveapproval.aspx?key=" + Id + "&AU=" + Convert.ToString(dts.Rows[0]["user_id"]);

                                string tinyURL = ShortURL(LongURL);

                                //string messagetext = "Leave Applied by " + Emp_Name + ". Please Approve/Reject by Clicking the link : " + tinyURL;

                                string messagetext = "";


                                DataSet dtMsg = new DataSet();
                                String conMsg = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                                SqlCommand sqlcmdMsg = new SqlCommand();
                                SqlConnection sqlconMsg = new SqlConnection(conMsg);
                                sqlconMsg.Open();
                                sqlcmdMsg = new SqlCommand("Proc_FTS_ApplyLeaveForApproval", sqlcon);
                                sqlcmdMsg.Parameters.Add("@ACTION", "GetMsgText");
                                sqlcmdMsg.Parameters.Add("@subACTION", "LeaveApply");
                                sqlcmdMsg.Parameters.Add("@user_id", model.user_id);
                                sqlcmdMsg.Parameters.Add("@Emp_Name", Emp_Name);
                                sqlcmdMsg.Parameters.Add("@tinyURL", tinyURL);
                                sqlcmdMsg.CommandType = CommandType.StoredProcedure;
                                SqlDataAdapter daMsg = new SqlDataAdapter(sqlcmdMsg);
                                daMsg.Fill(dtMsg);
                                sqlconMsg.Close();



                                if (dtMsg != null && dtMsg.Tables[0] != null && dtMsg.Tables[0].Rows.Count > 0)
                                {
                                    messagetext = Convert.ToString(dtMsg.Tables[0].Rows[0][0]);
                                }

                                // string messagetextTobeSend="Leave has been applied successfully. Please wait for the Approval/Rejection Notification.";

                                if (dtMsg != null && dtMsg.Tables[1] != null && dtMsg.Tables[1].Rows.Count > 0)
                                {
                                    messagetextTobeSend = Convert.ToString(dtMsg.Tables[1].Rows[0][0]);
                                }





                                string res = SmsSent(username, password, Provider, sender, Convert.ToString(dr["user_loginId"]), messagetext, "Text");

                            }


                            GetAndSendNotification(Convert.ToString(model.user_id), messagetextTobeSend, "leavePending");



                            omodel.status = "200";
                            omodel.message = "Leave Applied Successfully.";
                        }
                        else
                        {
                            omodel.status = "201";
                            omodel.message = Convert.ToString(dt.Rows[0][0]);
                        }
                    }
                    else
                    {

                        omodel.status = "202";
                        omodel.message = "Try again later.";

                    }

                    var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                    return message;
                }

            }
            catch (Exception ex)
            {


                omodel.status = "209";

                omodel.message = ex.Message;
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        string ShortURL(string LongUrl)
        {
            System.Uri address = new System.Uri("http://tinyurl.com/api-create.php?url=" + LongUrl);
            System.Net.WebClient client = new System.Net.WebClient();
            string tinyUrl = client.DownloadString(address);
            return tinyUrl;
        }

        void GetAndSendNotification(string user_id, string messagetext, string LeaveStatus)
        {
            string status = string.Empty;
            try
            {

                string Mobiles = "";

                DataTable dtm = new DataTable();
                String conm = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmdm = new SqlCommand();
                SqlConnection sqlconm = new SqlConnection(conm);
                sqlconm.Open();
                sqlcmdm = new SqlCommand("select user_loginId from tbl_master_user where user_id='" + user_id + "'", sqlconm);
                SqlDataAdapter dam = new SqlDataAdapter(sqlcmdm);
                dam.Fill(dtm);
                sqlconm.Close();

                if (dtm != null && dtm.Rows.Count > 0)
                {
                    Mobiles = Convert.ToString(dtm.Rows[0][0]);
                }

                String username = System.Configuration.ConfigurationSettings.AppSettings["username"];
                String password = System.Configuration.ConfigurationSettings.AppSettings["password"];
                String Provider = System.Configuration.ConfigurationSettings.AppSettings["Provider"];
                String sender = System.Configuration.ConfigurationSettings.AppSettings["sender"];

                string res = SmsSent(username, password, Provider, sender, Mobiles, messagetext, "Text");



                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("Proc_FCM_NotificationManage", sqlcon);
                sqlcmd.Parameters.Add("@Message", messagetext);
                sqlcmd.Parameters.Add("@Mobiles", Mobiles);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();



                DataTable dts = new DataTable();
                String cons = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmds = new SqlCommand();
                SqlConnection sqlcons = new SqlConnection(cons);
                sqlcons.Open();
                sqlcmds = new SqlCommand("select  device_token,musr.user_name,musr.user_id  from tbl_master_user as musr inner join tbl_FTS_devicetoken as token on musr.user_id=token.UserID  where musr.user_Id ='" + user_id + "' and musr.user_inactive='N'", sqlcon);
                SqlDataAdapter das = new SqlDataAdapter(sqlcmds);
                das.Fill(dts);
                sqlcons.Close();






                if (dts.Rows.Count > 0)
                {
                    for (int i = 0; i < dts.Rows.Count; i++)
                    {
                        if (Convert.ToString(dts.Rows[i]["device_token"]) != "")
                        {
                            SendPushNotification(messagetext, Convert.ToString(dts.Rows[i]["device_token"]), Convert.ToString(dts.Rows[i]["user_name"]), Convert.ToString(dts.Rows[i]["user_id"]), LeaveStatus);
                        }
                    }
                    status = "200";
                }


                else
                {

                    status = "202";
                }




            }
            catch
            {
                status = "300";

            }
        }
        public static void SendPushNotification(string message, string deviceid, string Customer, string Requesttype, string LeaveStatus)
        {
            try
            {
                //string applicationID = "AAAAS0O97Kk:APA91bH8_KgkJzglOUHC1ZcMEQFjQu8fsj1HBKqmyFf-FU_I_GLtXL_BFUytUjhlfbKvZFX9rb84PWjs05HNU1QyvKy_TJBx7nF70IdIHBMkPgSefwTRyDj59yXz4iiKLxMiXJ7vel8B";
                //string senderId = "323259067561";
                string applicationID = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["AppID"]);
                string senderId = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["SenderID"]);
                string deviceId = deviceid;
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";

                var data2 = new
                {
                    to = deviceId,
                    data = new
                    {
                        UserName = Customer,
                        UserID = Requesttype,
                        body = message,
                        type = LeaveStatus
                    }
                };

                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data2);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                string str = sResponseFromServer;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }

        public string SmsSent(string username, string password, string Provider, string senderId, string mobile, string message, string type)
        {

            //  http://5.189.187.82/sendsms/sendsms.php?username=QHEkaruna&password=baj8piv3&type=Text&sender=KARUNA&mobile=9831892083&message=HELO
            string response = "";
            string url = Provider + "?username=" + username + "&password=" + password + "&type=" + type + "&sender=" + senderId + "&mobile=" + mobile + "&message=" + message;
            if (mobile.Trim() != "")
            {
                try
                {
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    response = httpResponse.StatusCode.ToString();
                }
                catch
                {
                    return "0";

                }
            }
            return response;
        }

        //Rev Debashis
        [HttpPost]
        public HttpResponseMessage UserLeaveForApprovalStatus(LeaveForApprovalStatusInput model)
        {
            LeaveForApprovalStatusOutput odata = new LeaveForApprovalStatusOutput();

            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
            }
            else
            {
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_APIUSERLEAVEFORAPPROVE", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "FORAPPROVAL");
                sqlcmd.Parameters.Add("@USER_ID", model.user_id);
                sqlcmd.Parameters.Add("@CURRENT_STATUS", model.current_status);
                sqlcmd.Parameters.Add("@APPROVE_USER", model.Approve_User);
                sqlcmd.Parameters.Add("@APPROVAL_DATE_TIME", model.approval_date_time);
                sqlcmd.Parameters.Add("@APPROVER_REMARKS", model.approver_remarks);
                sqlcmd.Parameters.Add("@APPLIED_DATE_TIME", model.applied_date_time);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    odata.status = "200";
                    odata.message = "Update successfully.";
                }
                else
                {
                    odata.status = "205";
                    odata.message = "No data found.";
                }
                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage UserLeaveList(UserLeaveListInput model)
        {
            UserLeaveListOutput omodel = new UserLeaveListOutput();
            List<UserLeaveList> oview = new List<UserLeaveList>();

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
                sqlcmd = new SqlCommand("PRC_APIUSERLEAVEFORAPPROVE", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "USERLEAVELIST");
                sqlcmd.Parameters.Add("@USER_ID", model.user_id_leave_applied);

                sqlcmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<UserLeaveList>(dt);
                    omodel.status = "200";
                    omodel.message = "Successfully get list.";
                    omodel.user_id_leave_applied = dt.Rows[0]["user_id_leave_applied"].ToString();
                    omodel.user_name_leave_applied = dt.Rows[0]["user_name_leave_applied"].ToString();
                    omodel.leave_list = oview;
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
        //End of Rev Debashis
    }
}

using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP.OMS.Management.Activities
{
    public partial class leaveapproval : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["key"] != null)
                {

                    Session["LEAVE_USER"] = Request.QueryString["key"];
                    Session["Approve_USER"] = Request.QueryString["AU"];

                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    DataTable dt = new DataTable();
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();

                    sqlcmd = new SqlCommand("Proc_FTS_ApplyLeaveForApproval", sqlcon);
                    sqlcmd.Parameters.Add("@Action", "GetLeaveDetails");
                    sqlcmd.Parameters.Add("@user_id", Convert.ToString(Request.QueryString["key"]));
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        txtName.InnerText = Convert.ToString(dt.Rows[0]["NAME"]);
                        txtMobile.InnerText = Convert.ToString(dt.Rows[0]["PHONE"]);
                        txtSupervisor.InnerText = Convert.ToString(dt.Rows[0]["REPORT_TO"]);
                        txtFrom.InnerText = Convert.ToString(dt.Rows[0]["LEAVE_START_DATE"]);
                        txtTo.InnerText = Convert.ToString(dt.Rows[0]["LEAVE_END_DATE"]);
                        txtReason.InnerText = Convert.ToString(dt.Rows[0]["LEAVE_REASON"]);
                        txtStatus.InnerText = Convert.ToString(dt.Rows[0]["CURRENT_STATUS"]);
                        txtType.InnerText = Convert.ToString(dt.Rows[0]["LEAVE_TYPE"]);
                        txtMobile.InnerText = Convert.ToString(dt.Rows[0]["PHONE"]);

                        if (Convert.ToString(dt.Rows[0]["CURRENT_STATUS"]) != "PENDING")
                        {
                            dvButtons.Style.Add("display", "none;");
                            Button1.Style.Add("display", "block !important;");
                        }

                    }
                    else
                    {
                        dvButtons.Style.Add("display", "none;");
                    }


                }

                else
                {
                    dvButtons.Style.Add("display", "none;");
                }
            }
        }

        protected void btApprove_Click(object sender, EventArgs e)
        {
            try
            {




                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                DataTable dt = new DataTable();
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);



                bool flag = true;

                DataTable dtStatus = new DataTable();
                sqlcon.Open();
                sqlcmd = new SqlCommand("SELECT 1 FROM FTS_USER_LEAVEAPPLICATION WHERE ID='" + Convert.ToString(Request.QueryString["key"]) + "' and CURRENT_STATUS in ('PENDING')", sqlcon);
                SqlDataAdapter Vda = new SqlDataAdapter(sqlcmd);
                Vda.Fill(dtStatus);
                sqlcon.Close();

                string Emp_Name = "";
                if (dtStatus != null && dtStatus.Rows.Count > 0)
                {
                    flag = false;
                }





                if (!flag)
                {
                    sqlcon.Open();

                    sqlcmd = new SqlCommand("Proc_FTS_ApplyLeaveForApproval", sqlcon);
                    sqlcmd.Parameters.Add("@Action", "ApproveRejectLeave");
                    sqlcmd.Parameters.Add("@user_id", Convert.ToString(Request.QueryString["key"]));
                    sqlcmd.Parameters.Add("@isApprove", true);
                    sqlcmd.Parameters.Add("@Approve_User", Convert.ToString(Session["Approve_USER"]));

                    SqlParameter output = new SqlParameter("@output", SqlDbType.VarChar, 50);
                    output.Direction = ParameterDirection.Output;
                    sqlcmd.Parameters.Add(output);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);

                    string from_date = Convert.ToString(sqlcmd.Parameters["@output"].Value.ToString());

                    sqlcon.Close();


                    DataTable dtEmployeeName = new DataTable();
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("select cnt_firstName +' '+cnt_lastName Neme from tbl_master_user usr inner join tbl_master_contact cnt on cnt.cnt_internalId=usr.user_contactId where user_id='" + Convert.ToString(Session["Approve_USER"]) + "' ", sqlcon);
                    da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dtEmployeeName);
                    sqlcon.Close();

                    if (dtEmployeeName != null && dtEmployeeName.Rows.Count > 0)
                    {
                        Emp_Name = Convert.ToString(dtEmployeeName.Rows[0][0]);
                    }



                    string NEW_USER_ID = "";
                    DataTable dtUSER_ID = new DataTable();
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("SELECT USER_ID FROM FTS_USER_LEAVEAPPLICATION WHERE ID='" + Convert.ToString(Request.QueryString["key"]) + "'", sqlcon);
                    da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dtUSER_ID);
                    sqlcon.Close();

                    if (dtUSER_ID != null && dtUSER_ID.Rows.Count > 0)
                    {
                        NEW_USER_ID = Convert.ToString(dtUSER_ID.Rows[0][0]);
                    }


                    DataSet dtMsg = new DataSet();
                    String conMsg = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmdMsg = new SqlCommand();
                    SqlConnection sqlconMsg = new SqlConnection(conMsg);
                    sqlconMsg.Open();
                    sqlcmdMsg = new SqlCommand("Proc_FTS_ApplyLeaveForApproval", sqlcon);
                    sqlcmdMsg.Parameters.Add("@ACTION", "GetMsgText");
                    sqlcmdMsg.Parameters.Add("@subACTION", "LeaveApprove");
                    sqlcmdMsg.Parameters.Add("@user_id", NEW_USER_ID);
                    sqlcmdMsg.Parameters.Add("@Emp_Name", Emp_Name);
                    sqlcmdMsg.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter daMsg = new SqlDataAdapter(sqlcmdMsg);
                    daMsg.Fill(dtMsg);
                    sqlconMsg.Close();


                    string messagetext = "";
                    if (dtMsg != null && dtMsg.Tables[0] != null && dtMsg.Tables[0].Rows.Count > 0)
                    {
                        messagetext = Convert.ToString(dtMsg.Tables[0].Rows[0][0]);
                    }




                    GetAndSendNotification(Convert.ToString(NEW_USER_ID), messagetext, "leaveApprove", from_date);

                    Response.Redirect("LeaveSuccessPage.aspx?afterAction=Yes");
                }
                else
                {
                    Response.Redirect("LeaveSuccessPage.aspx?afterAction=No");
                }
            }
            catch
            {

            }
        }


        void GetAndSendNotification(string user_id, string messagetext, string LeaveStatus, string from_date)
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
                            SendPushNotification(messagetext, Convert.ToString(dts.Rows[i]["device_token"]), Convert.ToString(dts.Rows[i]["user_name"]), Convert.ToString(dts.Rows[i]["user_id"]), LeaveStatus, from_date);
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

        protected void btnReject_Click(object sender, EventArgs e)
        {
            String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
            DataTable dt = new DataTable();
            SqlCommand sqlcmd = new SqlCommand();
            SqlConnection sqlcon = new SqlConnection(con);
            


            bool flag = true;



            DataTable dtStatus = new DataTable();
            sqlcon.Open();
            sqlcmd = new SqlCommand("SELECT 1 FROM FTS_USER_LEAVEAPPLICATION WHERE ID='" + Convert.ToString(Request.QueryString["key"]) + "'", sqlcon);
            SqlDataAdapter Vda = new SqlDataAdapter(sqlcmd);
            Vda.Fill(dtStatus);
            sqlcon.Close();

            if (dtStatus != null && dtStatus.Rows.Count > 0)
            {
                flag = false;
            }

            if (!flag)
            {
                sqlcon.Open();
                sqlcmd = new SqlCommand("Proc_FTS_ApplyLeaveForApproval", sqlcon);
                sqlcmd.Parameters.Add("@Action", "ApproveRejectLeave");
                sqlcmd.Parameters.Add("@user_id", Convert.ToString(Request.QueryString["key"]));
                sqlcmd.Parameters.Add("@isApprove", false);
                sqlcmd.Parameters.Add("@Approve_User", Convert.ToString(Session["Approve_USER"]));

                SqlParameter output = new SqlParameter("@output", SqlDbType.VarChar, 50);
                output.Direction = ParameterDirection.Output;
                sqlcmd.Parameters.Add(output);


                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);

                string from_date = Convert.ToString(sqlcmd.Parameters["@output"].Value.ToString());

                sqlcon.Close();

                //


               


                DataTable dtEmployeeName = new DataTable();
                sqlcon.Open();
                sqlcmd = new SqlCommand("select cnt_firstName +' '+cnt_lastName Neme from tbl_master_user usr inner join tbl_master_contact cnt on cnt.cnt_internalId=usr.user_contactId where user_id='" + Convert.ToString(Session["Approve_USER"]) + "'", sqlcon);
                da = new SqlDataAdapter(sqlcmd);
                da.Fill(dtEmployeeName);
                sqlcon.Close();

                string Emp_Name = "";
                if (dtEmployeeName != null && dtEmployeeName.Rows.Count > 0)
                {
                    Emp_Name = Convert.ToString(dtEmployeeName.Rows[0][0]);
                }

                string NEW_USER_ID = "";
                DataTable dtUSER_ID = new DataTable();
                sqlcon.Open();
                sqlcmd = new SqlCommand("SELECT USER_ID FROM FTS_USER_LEAVEAPPLICATION WHERE ID='" + Convert.ToString(Request.QueryString["key"]) + "'", sqlcon);
                da = new SqlDataAdapter(sqlcmd);
                da.Fill(dtUSER_ID);
                sqlcon.Close();

                if (dtUSER_ID != null && dtUSER_ID.Rows.Count > 0)
                {
                    NEW_USER_ID = Convert.ToString(dtUSER_ID.Rows[0][0]);
                }


                DataSet dtMsg = new DataSet();
                String conMsg = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmdMsg = new SqlCommand();
                SqlConnection sqlconMsg = new SqlConnection(conMsg);
                sqlconMsg.Open();
                sqlcmdMsg = new SqlCommand("Proc_FTS_ApplyLeaveForApproval", sqlcon);
                sqlcmdMsg.Parameters.Add("@ACTION", "GetMsgText");
                sqlcmdMsg.Parameters.Add("@subACTION", "LeaveReject");
                sqlcmdMsg.Parameters.Add("@user_id", NEW_USER_ID);
                sqlcmdMsg.Parameters.Add("@Emp_Name", Emp_Name);
                sqlcmdMsg.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter daMsg = new SqlDataAdapter(sqlcmdMsg);
                daMsg.Fill(dtMsg);
                sqlconMsg.Close();


                string messagetext = "";
                if (dtMsg != null && dtMsg.Tables[0] != null && dtMsg.Tables[0].Rows.Count > 0)
                {
                    messagetext = Convert.ToString(dtMsg.Tables[0].Rows[0][0]);
                }


                GetAndSendNotification(Convert.ToString(NEW_USER_ID), messagetext, "leaveReject", from_date);

                Response.Redirect("LeaveSuccessPage.aspx?afterAction=Yes");
            }
            else
            {
                Response.Redirect("LeaveSuccessPage.aspx?afterAction=No");
            }
        }


        public static void SendPushNotification(string message, string deviceid, string Customer, string Requesttype, string LeaveStatus,string LeaveFromDate="")
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
                        type = LeaveStatus,
                        leaveFromDate = LeaveFromDate
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("LeaveSuccessPage.aspx?afterAction=No");
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
    }
}
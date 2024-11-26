/********************************************************************************************************************
 * 1.0       10/09/2024        V2.0.48          Sanchita          27690: Quotation Notification issue @ Eurobond
 * *******************************************************************************************************************/
using Google.Apis.Auth.OAuth2;
using ShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace ShopAPI.Controllers
{
    public class ChatController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage UserList(ChatListInput model)
        {
            ChatListOutput omodel = new ChatListOutput();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                string sessionId = "";
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_Chat", sqlcon);
                sqlcmd.Parameters.Add("@Action", "ChatUserList");
                sqlcmd.Parameters.Add("@weburl", weburl);
                sqlcmd.Parameters.Add("@user_id", model.user_id);


                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    List<ChatList> oview = new List<ChatList>();

                    oview = (from DataRow dr in dt.Rows
                             select new ChatList()
                             {
                                 id = Convert.ToString(dr["id"]),
                                 image = Convert.ToString(dr["image"]),
                                 isGroup = Convert.ToString(dr["isGroup"]),
                                 name = Convert.ToString(dr["name"]),
                                 last_msg_time = Convert.ToDateTime(dr["ASC_DATE"]),
                                 last_msg_user_id = Convert.ToString(dr["FROM_USER_ID"]),
                                 last_msg_user_name = Convert.ToString(dr["FROM_USER"]),
                                 last_msg = System.Text.RegularExpressions.Regex.Unescape(Convert.ToString(dr["MSG"]))

                             }).ToList();


                    omodel.chat_user_list = oview;
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
        public HttpResponseMessage SendMsg(ChatSendInput model)
        {
            ChatSendOutput omodel = new ChatSendOutput();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                string sessionId = "";
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_Chat", sqlcon);
                sqlcmd.Parameters.Add("@Action", "SendMsg");
                sqlcmd.Parameters.Add("@USER1", model.user_id);
                sqlcmd.Parameters.Add("@USER2", model.to_id);
                sqlcmd.Parameters.Add("@msg", model.msg);
                sqlcmd.Parameters.Add("@msg_id", model.msg_id);
                sqlcmd.Parameters.Add("@time", model.time);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();




                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToString(dt.Rows[i]["device_token"]) != "")
                        {
                            string isGroup = "false";
                            string from_user_id = "0";
                            from_user_id = Convert.ToString(Convert.ToInt32(model.user_id));
                            string from_user = model.user_name;
                            string body = model.user_name + "\n";
                            if (Convert.ToInt32(model.to_id) > 100000)
                            {
                                from_user_id = Convert.ToString(Convert.ToInt32(model.to_id));
                                isGroup = "true";                                
                                from_user = Convert.ToString(dt.Rows[i]["GROUP_NAME_MSG"]);
                                body = from_user + "\n" + model.user_name + " says";

                            }
                            // Rev 1.0
                            //SendPushNotification(model.msg, Convert.ToString(dt.Rows[i]["device_token"]), Convert.ToString(dt.Rows[i]["user_name"]),
                            //    Convert.ToString(dt.Rows[i]["user_id"]), model.msg_id, model.msg, model.time, model.user_id, model.user_name, isGroup, from_user, from_user_id, body);

                            SendPushNotification(Convert.ToString(dt.Rows[i]["device_token"]), "chat", body, model.msg, Convert.ToString(dt.Rows[i]["user_name"]),
                                Convert.ToString(dt.Rows[i]["user_id"]), model.msg_id, model.msg, model.time, model.user_id, model.user_name, isGroup, from_user, from_user_id);
                            // End of Rev 1.0
                        }
                    }
                    omodel.status = "200";
                    omodel.message = "Successfully send chat";
                }
                else
                {
                    omodel.status = "200";
                    omodel.message = "Successfully send chat.";
                }
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage ChatList(ChatListGetInput model)
        {
            ChatListGetOutput omodel = new ChatListGetOutput();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                string sessionId = "";
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_Chat", sqlcon);
                sqlcmd.Parameters.Add("@Action", "ChatList");
                sqlcmd.Parameters.Add("@weburl", weburl);
                sqlcmd.Parameters.Add("@USER1", model.user_id);
                sqlcmd.Parameters.Add("@USER2", model.opponent_id);
                sqlcmd.Parameters.Add("@page_count", model.page_count);
                sqlcmd.Parameters.Add("@page_no", model.page_no);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    List<ChatListGet> oview = new List<ChatListGet>();

                    oview = (from DataRow dr in dt.Rows
                             select new ChatListGet()
                             {
                                 id = Convert.ToString(dr["id"]),
                                 from_id = Convert.ToString(dr["from_id"]),
                                 from_name = Convert.ToString(dr["from_name"]),
                                 msg = System.Text.RegularExpressions.Regex.Unescape(Convert.ToString(dr["msg"])),
                                 time = Convert.ToDateTime(dr["time"]),
                                 status = Convert.ToString(dr["Status"])

                             }).ToList();


                    omodel.chat_list = oview;
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
        public HttpResponseMessage GroupUserList(ChatListInput model)
        {
            GroupChatListOutput omodel = new GroupChatListOutput();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                string sessionId = "";
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_Chat", sqlcon);
                sqlcmd.Parameters.Add("@Action", "GroupUserList");
                sqlcmd.Parameters.Add("@weburl", weburl);
                sqlcmd.Parameters.Add("@user_id", model.user_id);


                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    List<GroupChatList> oview = new List<GroupChatList>();

                    oview = (from DataRow dr in dt.Rows
                             select new GroupChatList()
                             {
                                 id = Convert.ToString(dr["id"]),
                                 image = Convert.ToString(dr["image"]),
                                 name = Convert.ToString(dr["name"])

                             }).ToList();


                    omodel.group_user_list = oview;
                    omodel.status = "200";
                    omodel.message = "Successfully get group user list";
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
        public HttpResponseMessage AddGroup(AddGroupInput model)
        {
            AddGroupOutput omodel = new AddGroupOutput();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                string sessionId = "";
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_Chat", sqlcon);
                sqlcmd.Parameters.Add("@Action", "SaveGroupApp");
                sqlcmd.Parameters.Add("@USER_ID", model.user_id);
                sqlcmd.Parameters.Add("@GROUP_NAME", model.grp_name);
                sqlcmd.Parameters.Add("@users", model.ids);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();




                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToString(dt.Rows[0][0]) == "Saved successfully.")
                    {
                        omodel.status = "200";
                        omodel.message = "Successfully add group.";
                    }
                    else
                    {
                        omodel.status = "201";
                        omodel.message = Convert.ToString(dt.Rows[0][0]);
                    }
                }
                else
                {
                    omodel.status = "200";
                    omodel.message = "Some input parameters are missing.";
                }
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }


        [HttpPost]
        public HttpResponseMessage AddmemberToGroup(AddMemberToGroupInput model)
        {
            AddMemberToGroupOutput omodel = new AddMemberToGroupOutput();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                string sessionId = "";
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_Chat", sqlcon);
                sqlcmd.Parameters.Add("@Action", "AddMemberToGroup");
                sqlcmd.Parameters.Add("@USER_ID", model.user_id);
                sqlcmd.Parameters.Add("@EDITGRP_ID", model.grp_id);
                sqlcmd.Parameters.Add("@users", model.ids);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();




                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToString(dt.Rows[0][0]) == "Saved successfully.")
                    {
                        omodel.status = "200";
                        omodel.message = "Successfully add group.";
                    }
                    else
                    {
                        omodel.status = "201";
                        omodel.message = Convert.ToString(dt.Rows[0][0]);
                    }
                }
                else
                {
                    omodel.status = "200";
                    omodel.message = "Some input parameters are missing.";
                }
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateStatus(UpdateStatusInput model)
        {
            UpdateStatusOutput omodel = new UpdateStatusOutput();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                string sessionId = "";
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_Chat", sqlcon);
                sqlcmd.Parameters.Add("@Action", "UpdateStatus");
                sqlcmd.Parameters.Add("@USER1", model.user_id);
                sqlcmd.Parameters.Add("@USER2", model.to_id);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();




                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToString(dt.Rows[i]["device_token"]) != "")
                        {
                            // Rev 1.0
                            //SendPushNotificationUpdateStatus(Convert.ToString(dt.Rows[i]["device_token"]));
                            SendPushNotification(Convert.ToString(dt.Rows[i]["device_token"]), "update_status", "upadated");
                            // End of Rev 1.0
                        }
                    }

                    if (Convert.ToString(dt.Rows[0][0]) == "Saved successfully.")
                    {
                        omodel.status = "200";
                        omodel.message = "Successfully update message status.";
                    }
                    else
                    {
                        omodel.status = "200";
                        omodel.message = "Successfully update message status.";
                    }
                }
                else
                {
                    omodel.status = "200";
                    omodel.message = "Some input parameters are missing.";
                }
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }


        [HttpPost]
        public HttpResponseMessage GroupSelectedUserList(GroupSelectedUserInput model)
        {
            GroupSelectedUserOutput omodel = new GroupSelectedUserOutput();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                string sessionId = "";
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_Chat", sqlcon);
                sqlcmd.Parameters.Add("@Action", "SlectedUserGroup");
                sqlcmd.Parameters.Add("@weburl", weburl);
                sqlcmd.Parameters.Add("@USER_ID", model.user_id);
                sqlcmd.Parameters.Add("@EDITGRP_ID", model.grp_id);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    List<GroupChatList> oview = new List<GroupChatList>();

                    oview = (from DataRow dr in dt.Rows
                             select new GroupChatList()
                             {
                                 id = Convert.ToString(dr["id"]),
                                 image = Convert.ToString(dr["image"]),
                                 name = Convert.ToString(dr["name"])

                             }).ToList();


                    omodel.group_user_list = oview;
                    omodel.status = "200";
                    omodel.message = "Successfully get group user list";
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

        // Rev 1.0
        //public static void SendPushNotification(string message, string deviceid, string Customer, string Requesttype, string msg_id, string msg, string time,
        //   string from_id, string from_name, string isGroup, string from_user_name, string from_user_id, string body)
        public void SendPushNotification(string deviceid, string type="", string body = "",  string message = "", string Customer = "", string Requesttype = "", string msg_id = "", 
            string msg = "", string time = "", string from_id = "", string from_name = "", string isGroup = "", string from_user_name = "", 
            string from_user_id = "")
            // End of Rev 1.0
        {
            try
            {
                // Rev 1.0
                //string applicationID = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["AppID"]);
                //string senderId = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["SenderID"]);

                //string deviceId = deviceid;
                //WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                //tRequest.Method = "post";
                //tRequest.ContentType = "application/json";

                //var data2 = new
                //{
                //    to = deviceId,
                //    data = new
                //    {
                //        UserName = Customer,
                //        UserID = Requesttype,
                //        body = body,
                //        type = "chat",
                //        msg_id = msg_id,
                //        msg = msg,
                //        time = time,
                //        from_user_id = from_user_id,
                //        isGroup = isGroup,
                //        from_id = from_id,
                //        from_name = from_name,
                //        from_user_name = from_user_name

                //    }
                //};

                //var serializer = new JavaScriptSerializer();
                //var json = serializer.Serialize(data2);
                //Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                //tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                //tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                //tRequest.ContentLength = byteArray.Length;
                //using (Stream dataStream = tRequest.GetRequestStream())
                //{
                //    dataStream.Write(byteArray, 0, byteArray.Length);
                //    using (WebResponse tResponse = tRequest.GetResponse())
                //    {
                //        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                //        {
                //            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                //            {
                //                String sResponseFromServer = tReader.ReadToEnd();
                //                string str = sResponseFromServer;
                //            }
                //        }
                //    }
                //}


                string fileName = "", projectname = "";

                //DataTable dt = odbengine.GetDataTable("select JSONFILE_NAME, PROJECT_NAME from FSM_CONFIG_FIREBASENITIFICATION WHERE ID=1");

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_Chat", sqlcon);
                sqlcmd.Parameters.Add("@Action", "GetFirebaseFileDet");
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();


                if (dt.Rows.Count > 0)
                {
                    fileName = System.Web.Hosting.HostingEnvironment.MapPath("~/" + Convert.ToString(dt.Rows[0]["JSONFILE_NAME"]));
                    projectname = Convert.ToString(dt.Rows[0]["PROJECT_NAME"]);
                }

                //string fileName = System.Web.Hosting.HostingEnvironment.MapPath("~/demofsm-fee63-firebase-adminsdk-m1emn-4e3e8bba2d.json"); //Download from Firebase Console ServiceAccount

                string scopes = "https://www.googleapis.com/auth/firebase.messaging";
                var bearertoken = ""; // Bearer Token in this variable
                using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))

                {

                    bearertoken = GoogleCredential
                      .FromStream(stream) // Loads key file
                      .CreateScoped(scopes) // Gathers scopes requested
                      .UnderlyingCredential // Gets the credentials
                      .GetAccessTokenForRequestAsync().Result; // Gets the Access Token

                }

                ///--------Calling FCM-----------------------------

                var clientHandler = new HttpClientHandler();
                var client = new HttpClient(clientHandler);

                client.BaseAddress = new Uri("https://fcm.googleapis.com/v1/projects/" + projectname + "/messages:send"); // FCM HttpV1 API

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //client.DefaultRequestHeaders.Accept.Add("Authorization", "Bearer " + bearertoken);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearertoken); // Authorization Token in this variable

                //---------------Assigning Of data To Model --------------

                Root rootObj = new Root();
                rootObj.message = new Message();
                rootObj.message.token = deviceid;  //"AAAA8_ptc9A:APA91bGhMaxl_Mm811bpvNExTIyZZz16krSTnCAp1RpbRKV8hZuIh9gsI6svxMvZO74WaZl3piBPHJzp2N3NN3JRS8a150BAmyLnwqa7nJUFay_kxNm11dQfdDCl00QUPncGCKq1kPYH"; //FCM Token id

                rootObj.message.data.UserName = Customer;
                rootObj.message.data.UserID = Requesttype;
                rootObj.message.data.body = body;
                rootObj.message.data.type = type;
                rootObj.message.data.msg_id = msg_id;
                rootObj.message.data.msg = msg;
                rootObj.message.data.time = time;
                rootObj.message.data.from_user_id = from_user_id;
                rootObj.message.data.isGroup = isGroup;
                rootObj.message.data.from_id = from_id;
                rootObj.message.data.from_name = from_name;
                rootObj.message.data.from_user_name = from_user_name;


                //-------------Convert Model To JSON ----------------------

                var jsonObj = new JavaScriptSerializer().Serialize(rootObj);

                //------------------------Calling Of FCM Notify API-------------------

                var data = new StringContent(jsonObj, Encoding.UTF8, "application/json");
                data.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = client.PostAsync("https://fcm.googleapis.com/v1/projects/" + projectname + "/messages:send", data).Result; // Calling The FCM httpv1 API

                //---------- Deserialize Json Response from API ----------------------------------

                var jsonResponse = response.Content.ReadAsStringAsync().Result;
                var responseObj = new JavaScriptSerializer().DeserializeObject(jsonResponse);
                // End of Rev 5.0
                // End of Rev 1.0
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }

        public static void SendPushNotificationUpdateStatus(string deviceid)
        {
            try
            {

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
                        body = "upadated",
                        type = "update_status"
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

    }
}

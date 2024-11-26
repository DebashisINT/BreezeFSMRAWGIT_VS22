/********************************************************************************************************************
 * 1.0       10/09/2024        V2.0.48          Sanchita          27690: Quotation Notification issue @ Eurobond
 * *******************************************************************************************************************/
using Google.Apis.Auth.OAuth2;
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
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ShopAPI.Models
{
    public class PushnotificationModel
    {
        public static void SendPushNotification(string message, string deviceid, string Customer, string Requesttype)
        {
            try
            {
                // Rev Sanchita
                //string applicationID = "AAAAS0O97Kk:APA91bH8_KgkJzglOUHC1ZcMEQFjQu8fsj1HBKqmyFf-FU_I_GLtXL_BFUytUjhlfbKvZFX9rb84PWjs05HNU1QyvKy_TJBx7nF70IdIHBMkPgSefwTRyDj59yXz4iiKLxMiXJ7vel8B";
                //string senderId = "323259067561";
                //string deviceId = deviceid;
                //WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                //tRequest.Method = "post";
                //tRequest.ContentType = "application/json";

                //var data2 = new
                //{
                //    to = deviceId,
                //    //notification = new
                //    //{
                //    //    body = message,
                //    //    title = ""
                //    //},
                //    data = new
                //    {
                //        UserName = Customer,
                //        UserID = Requesttype,
                //        body = message
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
                rootObj.message.data.body = message;
                //rootObj.message.data.type = type;
                //rootObj.message.data.msg_id = msg_id;
                //rootObj.message.data.msg = msg;
                //rootObj.message.data.time = time;
                //rootObj.message.data.from_user_id = from_user_id;
                //rootObj.message.data.isGroup = isGroup;
                //rootObj.message.data.from_id = from_id;
                //rootObj.message.data.from_name = from_name;
                //rootObj.message.data.from_user_name = from_user_name;


                //-------------Convert Model To JSON ----------------------

                var jsonObj = new JavaScriptSerializer().Serialize(rootObj);

                //------------------------Calling Of FCM Notify API-------------------

                var data = new StringContent(jsonObj, Encoding.UTF8, "application/json");
                data.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = client.PostAsync("https://fcm.googleapis.com/v1/projects/" + projectname + "/messages:send", data).Result; // Calling The FCM httpv1 API

                //---------- Deserialize Json Response from API ----------------------------------

                var jsonResponse = response.Content.ReadAsStringAsync().Result;
                var responseObj = new JavaScriptSerializer().DeserializeObject(jsonResponse);
                // End of Rev Sanchita
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }
    }
}
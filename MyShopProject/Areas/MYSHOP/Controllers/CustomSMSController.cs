/***************************************************************************************************************************
 * 1.0       10/09/2024        V2.0.48          Sanchita          27690: Quotation Notification issue @ Eurobond
 * *******************************************************************************************************************************/
using BusinessLogicLayer;
using BusinessLogicLayer.SalesmanTrack;
using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using UtilityLayer;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class CustomSMSController : Controller
    {
        DBEngine odbengine = new DBEngine();
        NotificationBL notificationbl = new NotificationBL();

        // GET: /MYSHOP/CustomSMS/
        public ActionResult CustomSMS()
        {

            CustomSMSClass notification = new CustomSMSClass();

            DataTable dt = odbengine.GetDataTable("SELECT '0' ID,'All' Name Union all select CAST(deg_id as VARCHAR(20)) ID,deg_designation Name FROM tbl_master_designation WHERE deg_id IN (SELECT desg.deg_id FROM tbl_master_employee EMP INNER JOIN (SELECT cnt.emp_cntId,desg.deg_designation,MAX(emp_id) as emp_id,desg.deg_id FROM tbl_trans_employeeCTC as cnt LEFT OUTER JOIN tbl_master_designation desg ON desg.deg_id=cnt.emp_Designation GROUP BY emp_cntId,desg.deg_designation,desg.deg_id) DESG ON DESG.emp_cntId=EMP.emp_contactId )");
            DataTable dtuser = notificationbl.FetchNotificationSP("", "", "GetFirstTimeUserWithLoginID", "", Convert.ToInt32(Session["userid"]));          
            DataTable dtstate = notificationbl.FetchNotificationSP("", "", "GetDesignationByState", "");
            notification.SupervisorList = APIHelperMethods.ToModelList<SupervisorList>(dt);
            notification.UserList = APIHelperMethods.ToModelList<UserNotificationList>(dtuser);
            notification.StateList = APIHelperMethods.ToModelList<StateList>(dtstate);


            return View(notification);
        }
        public JsonResult GetUserList(string designationid, string notificationId, string stateid)
        {

            DataTable dtuser = notificationbl.FetchNotificationSP("", designationid, "GetUserByNotificationanddesignationIdWithLoginID", stateid, Convert.ToInt32(Session["userid"]));
            return Json(APIHelperMethods.ToModelList<UserNotificationList>(dtuser));

        }

        public JsonResult SendSMS(string Mobiles, string messagetext)
        {
            string status = string.Empty;
            try
            {

                String tokenmatch = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                String username = System.Configuration.ConfigurationSettings.AppSettings["username"];
                String password = System.Configuration.ConfigurationSettings.AppSettings["password"];
                String Provider = System.Configuration.ConfigurationSettings.AppSettings["Provider"];
                String sender = System.Configuration.ConfigurationSettings.AppSettings["sender"];

                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];



                string res = SmsSent(username, password, Provider, sender, Mobiles, messagetext, "Text");
                if (res != "0")
                {
                    status = "200";
                    return Json(status, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    status = "300";
                    return Json(status, JsonRequestBehavior.AllowGet);

                }


                return Json(status, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                status = "300";
                return Json(status, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SendSMSToShop(string StateID, string messagetext)
        {
            string status = string.Empty;
            try
            {

                String tokenmatch = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                String username = System.Configuration.ConfigurationSettings.AppSettings["username"];
                String password = System.Configuration.ConfigurationSettings.AppSettings["password"];
                String Provider = System.Configuration.ConfigurationSettings.AppSettings["Provider"];
                String sender = System.Configuration.ConfigurationSettings.AppSettings["sender"];

                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];

                string res = "1";
                DataTable dt = new DataTable();
                dt = odbengine.GetDataTable("exec Proc_ShopDetails_Type  1,'"+StateID+"','Shop'");

                foreach (DataRow item in dt.Rows)
                {
                    string mobile = Convert.ToString(item[0]); //+","+Convert.ToString(item[1]);
                    res = SmsSent(username, password, Provider, sender, mobile, messagetext, "Text");
                }

                dt = odbengine.GetDataTable("exec Proc_ShopDetails_Type  1,'" + StateID + "','User'");
                foreach (DataRow item in dt.Rows)
                {
                    string mobile = Convert.ToString(item[0]); //+","+Convert.ToString(item[1]);
                    res = SmsSent(username, password, Provider, sender, mobile, messagetext, "Text");
                }


                if (res != "0")
                {
                    status = "200";
                    return Json(status, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    status = "300";
                    return Json(status, JsonRequestBehavior.AllowGet);

                }


                return Json(status, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                status = "300";
                return Json(status, JsonRequestBehavior.AllowGet);
            }
        }


     




        public  string SmsSent(string username,string password,string Provider,string senderId,string mobile,string message,string type)
        {

          //  http://5.189.187.82/sendsms/sendsms.php?username=QHEkaruna&password=baj8piv3&type=Text&sender=KARUNA&mobile=9831892083&message=HELO
            string response="";
            string url = Provider + "?username=" + username + "&password=" + password + "&type=" + type + "&sender=" + senderId + "&mobile=" + mobile + "&message=" + message;
            if (mobile.Trim() != "")
            {
                try
                {
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    response=httpResponse.StatusCode.ToString();
                }
                catch
                {
                    return "0";

                }
            }
            return response;
        }

        #region Pushnotification by FCM

        public JsonResult SendNotification(string Mobiles, string messagetext)
        {
            
            string status = string.Empty;
            try
            {
                int returnmssge= notificationbl.Savenotification(Mobiles,messagetext);
                DataTable dt = odbengine.GetDataTable("select  device_token,musr.user_name,musr.user_id  from tbl_master_user as musr inner join tbl_FTS_devicetoken as token on musr.user_id=token.UserID  where musr.user_loginId in (select items from dbo.SplitString('" + Mobiles + "',',')) and musr.user_inactive='N'");

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToString(dt.Rows[i]["device_token"]) != "")
                        {
                            // Rev 1.0
                            //SendPushNotification(messagetext, Convert.ToString(dt.Rows[i]["device_token"]), Convert.ToString(dt.Rows[i]["user_name"]), Convert.ToString(dt.Rows[i]["user_id"]));

                            CRMEnquiriesController obj = new CRMEnquiriesController();
                            obj.SendPushNotification(messagetext, Convert.ToString(dt.Rows[i]["device_token"]), Convert.ToString(dt.Rows[i]["user_name"]), Convert.ToString(dt.Rows[i]["user_id"]), "");
                            // End of Rev 1.0
                        }
                    }
                    status = "200";
                }


                else
                {

                    status = "202";
                }



                return Json(status, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                status = "300";
                return Json(status, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SendNotificationClearData(string Mobiles, string messagetext)
        {
            if (string.IsNullOrEmpty(messagetext))
            {
                messagetext = "Please clear the App Data.";
            }
            string status = string.Empty;
            try
            {
                int returnmssge = notificationbl.Savenotification(Mobiles, messagetext);
                DataTable dt = odbengine.GetDataTable("select  device_token,musr.user_name,musr.user_id  from tbl_master_user as musr inner join tbl_FTS_devicetoken as token on musr.user_id=token.UserID  where musr.user_loginId in (select items from dbo.SplitString('" + Mobiles + "',',')) and musr.user_inactive='N'");

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToString(dt.Rows[i]["device_token"]) != "")
                        {
                            // Rev 1.0
                            //SendPushNotificationClearData(messagetext, Convert.ToString(dt.Rows[i]["device_token"]), Convert.ToString(dt.Rows[i]["user_name"]), Convert.ToString(dt.Rows[i]["user_id"]));

                            CRMEnquiriesController obj = new CRMEnquiriesController();
                            obj.SendPushNotification(messagetext, Convert.ToString(dt.Rows[i]["device_token"]), Convert.ToString(dt.Rows[i]["user_name"]), Convert.ToString(dt.Rows[i]["user_id"]), "clearData");
                            // End of Rev 1.0
                        }
                    }
                    status = "200";
                }


                else
                {

                    status = "202";
                }



                return Json(status, JsonRequestBehavior.AllowGet);
            }
            catch
            {



                status = "300";
                return Json(status, JsonRequestBehavior.AllowGet);
            }
        }


        // Rev 1.0
        //public static void SendPushNotification(string message, string deviceid, string Customer, string Requesttype)
        //{
        //    try
        //    {
        //        //string applicationID = "AAAAS0O97Kk:APA91bH8_KgkJzglOUHC1ZcMEQFjQu8fsj1HBKqmyFf-FU_I_GLtXL_BFUytUjhlfbKvZFX9rb84PWjs05HNU1QyvKy_TJBx7nF70IdIHBMkPgSefwTRyDj59yXz4iiKLxMiXJ7vel8B";
        //        //string senderId = "323259067561";
        //        string applicationID = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["AppID"]);
        //        string senderId = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["SenderID"]);
        //        //string senderId = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["SenderID"]);
        //        string deviceId = deviceid;
        //        WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
        //        tRequest.Method = "post";
        //        tRequest.ContentType = "application/json";

        //        var data2 = new
        //        {
        //            to = deviceId,
        //            //notification = new
        //            //{
        //            //    body = message,
        //            //    title = ""
        //            //},
        //            data = new
        //            {
        //                UserName = Customer,
        //                UserID = Requesttype,
        //                body = message
        //            }
        //        };

        //        var serializer = new JavaScriptSerializer();
        //        var json = serializer.Serialize(data2);
        //        Byte[] byteArray = Encoding.UTF8.GetBytes(json);
        //        tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
        //        tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
        //        tRequest.ContentLength = byteArray.Length;
        //        using (Stream dataStream = tRequest.GetRequestStream())
        //        {
        //            dataStream.Write(byteArray, 0, byteArray.Length);
        //            using (WebResponse tResponse = tRequest.GetResponse())
        //            {
        //                using (Stream dataStreamResponse = tResponse.GetResponseStream())
        //                {
        //                    using (StreamReader tReader = new StreamReader(dataStreamResponse))
        //                    {
        //                        String sResponseFromServer = tReader.ReadToEnd();
        //                        string str = sResponseFromServer;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string str = ex.Message;
        //    }
        //}


       
        //public static void SendPushNotificationClearData(string message, string deviceid, string Customer, string Requesttype)
        //{
        //    try
        //    {
        //       // string applicationID = "AAAAS0O97Kk:APA91bH8_KgkJzglOUHC1ZcMEQFjQu8fsj1HBKqmyFf-FU_I_GLtXL_BFUytUjhlfbKvZFX9rb84PWjs05HNU1QyvKy_TJBx7nF70IdIHBMkPgSefwTRyDj59yXz4iiKLxMiXJ7vel8B";
        //        //string senderId = "323259067561";
        //        string applicationID = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["AppID"]);
        //        string senderId = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["SenderID"]);
        //        string deviceId = deviceid;
        //        WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
        //        tRequest.Method = "post";
        //        tRequest.ContentType = "application/json";

        //        var data2 = new
        //        {
        //            to = deviceId,
        //            //notification = new
        //            //{
        //            //    body = message,
        //            //    title = ""
        //            //},
        //            data = new
        //            {
        //                UserName = Customer,
        //                UserID = Requesttype,
        //                body = message,
        //                type = "clearData"
        //            }
        //        };
        //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //        var serializer = new JavaScriptSerializer();
        //        var json = serializer.Serialize(data2);
        //        Byte[] byteArray = Encoding.UTF8.GetBytes(json);
        //        tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
        //        tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
        //        tRequest.ContentLength = byteArray.Length;
        //        using (Stream dataStream = tRequest.GetRequestStream())
        //        {
        //            dataStream.Write(byteArray, 0, byteArray.Length);
        //            using (WebResponse tResponse = tRequest.GetResponse())
        //            {
        //                using (Stream dataStreamResponse = tResponse.GetResponseStream())
        //                {
        //                    using (StreamReader tReader = new StreamReader(dataStreamResponse))
        //                    {
        //                        String sResponseFromServer = tReader.ReadToEnd();
        //                        string str = sResponseFromServer;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string str = ex.Message;
        //    }
        //}
        // End of Rev 1.0

        #endregion




	}
    
}
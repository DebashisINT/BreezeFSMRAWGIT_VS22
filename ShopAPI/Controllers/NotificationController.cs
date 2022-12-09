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
    public class NotificationController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage GetNotification(ModelNotificationInput model)
        {

            ModelNotificationOutput odata = new ModelNotificationOutput();
            List<ModelNotification> omodel = new List<ModelNotification>();

            DataTable dt = new DataTable();
            String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
            SqlCommand sqlcmd = new SqlCommand();
            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();


            sqlcmd = new SqlCommand("Proc_FTS_NotificationList", sqlcon);
            sqlcmd.Parameters.Add("@user_id", model.user_id);
            sqlcmd.Parameters.Add("@Action", "NotificationList");

            sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dt);
            sqlcon.Close();

            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    omodel.Add(new ModelNotification()
                    {
                        id = Convert.ToString(dt.Rows[i]["NotificationID"]),
                        notificationmessage = Convert.ToString(dt.Rows[i]["Notification_Text"]),
                        date_time = Convert.ToDateTime(dt.Rows[i]["Createddate"])
                    });
                }

              

            }
            odata.status = "200";
            odata.message = "Success";
            odata.notification_list = omodel;
            var message = Request.CreateResponse(HttpStatusCode.OK, odata);
            return message;

        }


        [HttpPost]
        public HttpResponseMessage GetDiamondCounterFirstCall()
        {

            ModelNotificationOutput odata = new ModelNotificationOutput();
            List<ModelNotification> omodel = new List<ModelNotification>();

            DataTable dt = new DataTable();
            String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
            SqlCommand sqlcmd = new SqlCommand();
            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();


            sqlcmd = new SqlCommand("Proc_Notification_DiamondCounter", sqlcon);
            sqlcmd.Parameters.Add("@Action", "AllUsers");


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
                        PushnotificationModel.SendPushNotification(Convert.ToString(dt.Rows[i]["device_token"]), Convert.ToString(dt.Rows[i]["device_token"]), Convert.ToString(dt.Rows[i]["user_name"]), Convert.ToString(dt.Rows[i]["user_id"]));
                    }
                }

            }

            odata.status = "200";
            odata.message = "Success";
            odata.notification_list = omodel;



            var message = Request.CreateResponse(HttpStatusCode.OK, odata);
            return message;

        }

        [HttpPost]
        public HttpResponseMessage GetDiamondCounterSecondCall(ModelNotificationInput model)
        {

            ModelNotificationOutput odata = new ModelNotificationOutput();
            List<ModelNotification> omodel = new List<ModelNotification>();

            DataTable dt = new DataTable();
            String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
            SqlCommand sqlcmd = new SqlCommand();
            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();


            sqlcmd = new SqlCommand("Proc_Notification_DiamondCounter", sqlcon);
            sqlcmd.Parameters.Add("@Action", "DiamonCounterNotAcheive");


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
                        PushnotificationModel.SendPushNotification(Convert.ToString(dt.Rows[i]["device_token"]), Convert.ToString(dt.Rows[i]["device_token"]), Convert.ToString(dt.Rows[i]["user_name"]), Convert.ToString(dt.Rows[i]["user_id"]));
                    }
                }

            }

            odata.status = "200";
            odata.message = "Success";
            odata.notification_list = omodel;
            var message = Request.CreateResponse(HttpStatusCode.OK, odata);
            return message;

        }




        [HttpPost]
        public HttpResponseMessage GetNotificationUnread(ModelNotificationInput model)
        {

            ModelNotificationOutputUnread odata = new ModelNotificationOutputUnread();
            List<ModelNotification> omodel = new List<ModelNotification>();

            DataTable dt = new DataTable();
            String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
            SqlCommand sqlcmd = new SqlCommand();
            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();


            sqlcmd = new SqlCommand("Proc_FTS_NotificationList", sqlcon);
            sqlcmd.Parameters.Add("@user_id", model.user_id);
            sqlcmd.Parameters.Add("@Action", "Unread");

            sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dt);
            sqlcon.Close();

            if (dt.Rows.Count > 0)
            {

                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    omodel.Add(new ModelNotification()
                //    {
                //        id = Convert.ToString(dt.Rows[i]["NotificationID"]),
                //        notificationmessage = Convert.ToString(dt.Rows[i]["Notification_Text"]),
                //        date_time = Convert.ToDateTime(dt.Rows[i]["Createddate"]),
                //        isUnreadNotificationPresent = Convert.ToBoolean(dt.Rows[i]["isUnreadNotificationPresent"])
                //    });
                //}
                odata.status = "200";
                odata.message = "Success";
                odata.isUnreadNotificationPresent = Convert.ToBoolean(dt.Rows[0]["Notification_Text"]);

            }
            else
            {
                odata.status = "205";
                odata.message = "Not Data found";
                odata.isUnreadNotificationPresent = Convert.ToBoolean(0);
            }
            
            
         
            var message = Request.CreateResponse(HttpStatusCode.OK, odata);
            return message;

        }


    }


}

using DataAccessLayer;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using LMS.Models;
//using MyShop.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using UtilityLayer;
using Google.Apis.Auth.OAuth2;

namespace LMS.Areas.LMS.Controllers
{
    public class LMSTopicsController : Controller
    {
        // GET: LMS/Topics
        public ActionResult Index()
        {
            LMSTopicsModel Dtls = new LMSTopicsModel();
            
            EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/LMSTopics/Index");
            ViewBag.CanAdd = rights.CanAdd;
            ViewBag.CanView = rights.CanView;
            ViewBag.CanExport = rights.CanExport;
            ViewBag.CanEdit = rights.CanEdit;
            ViewBag.CanDelete = rights.CanDelete;

            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("PRC_LMSTOPICSMASTER");
            proc.AddPara("@ACTION", "GETDROPDOWNBINDDATA");
            ds = proc.GetDataSet();

            if (ds != null)
            {
                // Company
                List<TopicBasedOnList> TopicBasedOnList = new List<TopicBasedOnList>();
                TopicBasedOnList = APIHelperMethods.ToModelList<TopicBasedOnList>(ds.Tables[0]);
                Dtls.TopicBasedOnList = TopicBasedOnList;
            }

            return View(Dtls);
        }

        public ActionResult PartialTopicGridList(LMSTopicsModel model)
        {
            try
            {
                EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/LMSTopics/Index");
                ViewBag.CanAdd = rights.CanAdd;
                ViewBag.CanView = rights.CanView;
                ViewBag.CanExport = rights.CanExport;
                ViewBag.CanEdit = rights.CanEdit;
                ViewBag.CanDelete = rights.CanDelete;

                if (model.Is_PageLoad == "TotalTopics" || model.Is_PageLoad == "UsedTopics" || model.Is_PageLoad == "UnusedTopics")
                {
                    string Is_PageLoad = model.Is_PageLoad;

                    model.Is_PageLoad = "Ispageload";

                    return PartialView("PartialTopicGridList", GetTopicDetails(Is_PageLoad));
                }
                else
                {
                    string Is_PageLoad = string.Empty;

                    if (model.Is_PageLoad == "Ispageload")
                    {
                        Is_PageLoad = "is_pageload";

                    }


                    GetTopicListing(Is_PageLoad);

                    model.Is_PageLoad = "Ispageload";

                    return PartialView("PartialTopicGridList", GetTopicDetails(Is_PageLoad));
                }
                

            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        public void GetTopicListing(string Is_PageLoad)
        {
            string user_id = Convert.ToString(Session["userid"]);

            string action = string.Empty;
            DataTable formula_dtls = new DataTable();
            DataSet dsInst = new DataSet();

            try
            {
                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_LMSTOPICSMASTER");
                proc.AddPara("@ACTION", "GETLISTINGDATA");
                proc.AddPara("@IS_PAGELOAD", Is_PageLoad);
                proc.AddPara("@USERID", Convert.ToInt32(user_id));
                dt = proc.GetTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable GetTopicDetails(string Is_PageLoad)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);

            ////////DataTable dtColmn = GetPageRetention(Session["userid"].ToString(), "CRM Contact");
            ////////if (dtColmn != null && dtColmn.Rows.Count > 0)
            ////////{
            ////////    ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            ////////}

            if (Is_PageLoad != "is_pageload")
            {
                if (Is_PageLoad == "UsedTopics")
                {
                    LMSMasterDataContext dc = new LMSMasterDataContext(connectionString);
                    var q = from d in dc.LMS_TOPICSMASTER_LISTINGs
                            where d.USERID == Convert.ToInt32(Userid) && d.QUESTIONS_ID != 0
                            orderby d.SEQ
                            select d;
                    return q;
                }
                else if (Is_PageLoad == "UnusedTopics")
                {
                    LMSMasterDataContext dc = new LMSMasterDataContext(connectionString);
                    var q = from d in dc.LMS_TOPICSMASTER_LISTINGs
                            where d.USERID == Convert.ToInt32(Userid) && d.QUESTIONS_ID == 0
                            orderby d.SEQ
                            select d;
                    return q;
                }
                else
                {
                    LMSMasterDataContext dc = new LMSMasterDataContext(connectionString);
                    var q = from d in dc.LMS_TOPICSMASTER_LISTINGs
                            where d.USERID == Convert.ToInt32(Userid)
                            orderby d.SEQ
                            select d;
                    return q;
                }
                
            }
            else
            {
                LMSMasterDataContext dc = new LMSMasterDataContext(connectionString);
                var q = from d in dc.LMS_TOPICSMASTER_LISTINGs
                        where d.USERID == Convert.ToInt32(Userid) && d.SEQ == 1111119
                        orderby d.SEQ 
                        select d;
                return q;
            }


        }

        public JsonResult GetTopicCount()
        {


            LMSTopicsModel dtl = new LMSTopicsModel();
            try
            {
                DataSet ds = new DataSet();
                ProcedureExecute proc = new ProcedureExecute("PRC_LMSTOPICSMASTER");
                proc.AddPara("@Action", "GETTOPICSCOUNTDATA");
                proc.AddPara("@userid", Convert.ToString(HttpContext.Session["userid"]));
                ds = proc.GetDataSet();


                int TotalTopics = 0;
                int TotalUsedTopics = 0;
                int TotalUnusedTopics = 0;

                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    TotalTopics = Convert.ToInt32(item["cnt_TotalTopics"]);
                    TotalUsedTopics = Convert.ToInt32(item["cnt_TotalUsedTopics"]);
                    TotalUnusedTopics = Convert.ToInt32(item["cnt_TotalUnusedTopics"]);

                }

                dtl.TotalTopics = TotalTopics;
                dtl.TotalUsedTopics = TotalUsedTopics;
                dtl.TotalUnusedTopics = TotalUnusedTopics;

            }
            catch
            {
            }
            return Json(dtl);
        }

        public ActionResult ExporRegisterList()
        {
            return GridViewExtension.ExportToXlsx(GetDoctorBatchGridViewSettings(), GetTopicDetails(""));
        }

        private GridViewSettings GetDoctorBatchGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "gridLMSTopic";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "LMSTopics";

            settings.Columns.Add(x =>
            {
                x.FieldName = "SEQ";
                x.Caption = "Sr. No";
                x.VisibleIndex = 1;
                x.ExportWidth = 80;

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "TOPICNAME";
                x.Caption = "Topic Name";
                x.VisibleIndex = 2;
                x.ExportWidth = 350;

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "TOPICBASEDON";
                x.Caption = "Based On";
                x.VisibleIndex = 3;
                x.ExportWidth = 150;

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "TOPICSTATUS";
                x.Caption = "Publish";
                x.VisibleIndex = 4;
                x.ExportWidth = 150;

            });

            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "TOPIC_COMP_DAY";
            //    x.Caption = "Topic Completion Day(s)";
            //    x.VisibleIndex = 5;
            //    x.ExportWidth = 170;

            //});

            settings.Columns.Add(x =>
            {
                x.FieldName = "TOPIC_ISDEFAULT";
                x.Caption = "Default Topic";
                x.VisibleIndex = 6;
                x.ExportWidth = 150;

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "TOPIC_SEQ";
                x.Caption = "Topic Sequence";
                x.VisibleIndex = 7;
                x.ExportWidth = 150;

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CREATEDBY";
                x.Caption = "Created by";
                x.VisibleIndex = 8;
                x.ExportWidth = 150;

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CREATEDON";
                x.Caption = "Created on";
                x.VisibleIndex = 9;
                x.ExportWidth = 150;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "UPDATEDBY";
                x.Caption = "Modified by";
                x.VisibleIndex = 10;
                x.ExportWidth = 150;

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "UPDATEDON";
                x.Caption = "Modified on";
                x.VisibleIndex = 11;
                x.ExportWidth = 150;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
            });

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;

        }

        public JsonResult GetBasedOnList(string topic_basedon)
        {
            LMSTopicsModel model = new LMSTopicsModel();
            List<TopicMapList> TopicMapList1 = new List<TopicMapList>();

            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_LMSTOPICSMASTER");
            proc.AddPara("@Action", "GETBASEDONDATALIST");
            proc.AddPara("@TOPICBASEDON_ID", topic_basedon);
            proc.AddPara("@BRANCHID", Convert.ToString(Session["userbranchHierarchy"]));
            proc.AddPara("@USERID", Convert.ToString(HttpContext.Session["userid"]));

            dt = proc.GetTable();

            if (dt != null)
            {
                TopicMapList1 = APIHelperMethods.ToModelList<TopicMapList>(dt);
            }

            return Json(TopicMapList1, JsonRequestBehavior.AllowGet); 
        }

        [HttpPost]
        public ActionResult SaveTopic(LMSTopicsModel data)
        {
            try
            {
                if (data.DefaultTopic == null)
                {
                    data.DefaultTopic = "0";
                }

                if (data.TopicCompDay == null)
                {
                    data.TopicCompDay = "0";
                }

                if(data.TopicSequence == null)
                {
                    data.TopicSequence = "0";
                }


                //string rtrnduplicatevalue = "";
                //string Userid = Convert.ToString(Session["userid"]);
                ProcedureExecute proc = new ProcedureExecute("PRC_LMSTOPICSMASTER");
                proc.AddPara("@ACTION", data.Action);
                proc.AddPara("@TOPICID", data.TopicID);
                proc.AddPara("@TOPICNAME", data.TopicName);
                proc.AddPara("@TOPICSTATUS", data.TopicStatus);
                proc.AddPara("@TOPICBASEDON_ID", data.TopicBasedOnId);
                proc.AddPara("@SELECTEDTOPICBASEDONMAPLIST", data.selectedTopicBasedOnMapList);
                proc.AddPara("@USERID", Convert.ToString(HttpContext.Session["userid"]));
                proc.AddPara("@TOPIC_COMP_DAY", data.TopicCompDay);
                proc.AddPara("@TOPIC_ISDEFAULT", data.DefaultTopic);
                proc.AddPara("@TOPIC_SEQ", data.TopicSequence);
                proc.AddVarcharPara("@RETURN_VALUE", 500, "", QueryParameterDirection.Output);
                proc.AddVarcharPara("@RETURN_DUPLICATEMAPNAME", -1, "", QueryParameterDirection.Output);
                proc.AddVarcharPara("@RETURN_NEWASSIGN", -1, "", QueryParameterDirection.Output);
                int k = proc.RunActionQuery();
                data.RETURN_VALUE = Convert.ToString(proc.GetParaValue("@RETURN_VALUE")); // WILL RETURN NEW TOPIC ID
                data.RETURN_DUPLICATEMAPNAME = Convert.ToString(proc.GetParaValue("@RETURN_DUPLICATEMAPNAME"));
                data.RETURN_NEWASSIGN = Convert.ToString(proc.GetParaValue("@RETURN_NEWASSIGN"));

                // Send Notification
                if ( (data.Action=="ADDTOPIC" && data.TopicStatus == "true") )  // If published and ADD
                {
                    FireNotification(data.TopicName, data.RETURN_VALUE, "");
                }
                else if((data.Action == "EDITTOPIC" && data.TopicStatus == "true" && data.TopicStatusOld == "false")) // published and EDIT
                {
                    FireNotification(data.TopicName, data.TopicID,"");
                }
                else if ((data.Action == "EDITTOPIC" && data.RETURN_NEWASSIGN!="")) // published and EDIT
                {
                    FireNotification(data.TopicName, data.TopicID, data.RETURN_NEWASSIGN);
                }

                if (data.Action == "ADDTOPIC" && Convert.ToInt16(data.RETURN_VALUE) > 0)
                {
                    data.RETURN_VALUE = "1";
                }
                // End of Send Notification

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        // Send Notification
        public void FireNotification(string TopicTitle, string TopicId, string NewAssign)
        {
            string Mssg = "HI! A new Topic [" + TopicTitle + "] has been assigned to you. Please check your learning dashboard to start watching.";
            var imgNotification_Icon = ConfigurationManager.AppSettings["SPath"].ToString() + "Commonfolder/LMS/Notification_Icon.jpg";
            //string SalesMan_Nm = "";
            string SalesMan_Phn = "";

            //DataTable dt_SalesMan = odbengine.GetDataTable("select user_loginId,user_name from tbl_master_user  where user_id=" + SalesmanId + "");
            DataTable dtAssignUser = new DataTable();

            ProcedureExecute procA = new ProcedureExecute("PRC_LMSCONTENTMASTER");
            procA.AddPara("@ACTION", "GETCONTENTASSIGNUSER");
            procA.AddPara("@TOPICID", TopicId);
            procA.AddPara("@USERID", Convert.ToString(HttpContext.Session["userid"]));
            procA.AddPara("@NEWASSIGN_ID", NewAssign);
            dtAssignUser = procA.GetTable();

            if (dtAssignUser.Rows.Count > 0)
            {
                SalesMan_Phn = dtAssignUser.Rows[0]["user_loginId"].ToString();

                SendNotification(SalesMan_Phn, Mssg, imgNotification_Icon);
            }
        }

        public JsonResult SendNotification(string Mobiles, string messagetext, string imgNotification_Icon)
        {

            string status = string.Empty;
            try
            {
                //int returnmssge = notificationbl.Savenotification(Mobiles, messagetext);

                int s = 0;
                ProcedureExecute proc = new ProcedureExecute("Proc_FCM_NotificationManage");
                proc.AddPara("@Mobiles", Mobiles);
                proc.AddPara("@Message", messagetext);
                s = proc.RunActionQuery();

                //DataTable dt = odbengine.GetDataTable("select device_token,musr.user_name,musr.user_id  from tbl_master_user as musr inner join tbl_FTS_devicetoken as token on musr.user_id=token.UserID  where musr.user_loginId in (select items from dbo.SplitString('" + Mobiles + "',',')) and musr.user_inactive='N'");
                //DataTable dt = odbengine.GetDataTable("select device_token,musr.user_name,musr.user_id  from tbl_master_user as musr inner join tbl_FTS_devicetoken as token on musr.user_id=token.UserID  where musr.user_loginId in (" + Mobiles + ") and musr.user_inactive='N'");

                Mobiles = Mobiles.Replace("'", "");

                DataTable dt = new DataTable();
                ProcedureExecute procA = new ProcedureExecute("PRC_LMSCONTENTMASTER");
                procA.AddPara("@ACTION", "GETDEVICETOKENINFO");
                procA.AddPara("@MOBILES", Mobiles);
                dt = procA.GetTable();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToString(dt.Rows[i]["device_token"]) != "")
                        {
                            SendPushNotification(messagetext, Convert.ToString(dt.Rows[i]["device_token"]), Convert.ToString(dt.Rows[i]["user_name"]), Convert.ToString(dt.Rows[i]["user_id"]), imgNotification_Icon);

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

        public static void SendPushNotification(string message, string deviceid, string Customer, string Requesttype, string imgNotification_Icon)
        {
            try
            {
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
                //        header="New Topic Added",
                //        body = message,
                //        type = "lms_content_assign",
                //        imgNotification_Icon = imgNotification_Icon
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
                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_PUSHNOTIFICATIONS");
                proc.AddPara("@Action", "GETJSONFORINDUSNETTECH");
                dt = proc.GetTable();
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

                //client.BaseAddress = new Uri("https://fcm.googleapis.com/v1/projects/demofsm-fee63/messages:send"); // FCM HttpV1 API
                client.BaseAddress = new Uri("https://fcm.googleapis.com/v1/projects/" + projectname + "/messages:send"); // FCM HttpV1 API

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //client.DefaultRequestHeaders.Accept.Add("Authorization", "Bearer " + bearertoken);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearertoken); // Authorization Token in this variable

                //---------------Assigning Of data To Model --------------

                Root rootObj = new Root();
                rootObj.message = new Message();

                rootObj.message.token = deviceid;  //"AAAA8_ptc9A:APA91bGhMaxl_Mm811bpvNExTIyZZz16krSTnCAp1RpbRKV8hZuIh9gsI6svxMvZO74WaZl3piBPHJzp2N3NN3JRS8a150BAmyLnwqa7nJUFay_kxNm11dQfdDCl00QUPncGCKq1kPYH"; //FCM Token id

                rootObj.message.data = new Data();
                rootObj.message.data.title = "New Topic Added";
                rootObj.message.data.body = message;
                rootObj.message.data.key_1 = "Sample Key";
                rootObj.message.data.key_2 = "Sample Key2";

                rootObj.message.data.UserName = Customer;
                rootObj.message.data.UserID = Requesttype;
                rootObj.message.data.header = "New Topic Added";
                rootObj.message.data.type = "lms_content_assign";
                rootObj.message.data.imgNotification_Icon = imgNotification_Icon;
                // rootObj.message.data.key_2 = "Sample Key2";


                rootObj.message.notification = new Notification();
                rootObj.message.notification.title = "New Topic Added";
                rootObj.message.notification.body = message;


                //var data2 = new
                //{
                //    to = deviceid,

                //    data = new
                //    {
                //        UserName = Customer,
                //        UserID = Requesttype,
                //        header = "New Topic Added",
                //        body = message,
                //        type = "lms_content_assign",
                //        imgNotification_Icon = imgNotification_Icon
                //    }
                //};

                //var serializer = new JavaScriptSerializer();
                //var jsonObj = serializer.Serialize(data2);

                //-------------Convert Model To JSON ----------------------

                var jsonObj = new JavaScriptSerializer().Serialize(rootObj);

                //------------------------Calling Of FCM Notify API-------------------

                var data = new StringContent(jsonObj, Encoding.UTF8, "application/json");
                data.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                //var response = client.PostAsync("https://fcm.googleapis.com/v1/projects/demofsm-fee63/messages:send", data).Result; // Calling The FCM httpv1 API
                var response = client.PostAsync("https://fcm.googleapis.com/v1/projects/" + projectname + "/messages:send", data).Result; // Calling The FCM httpv1 API

                //---------- Deserialize Json Response from API ----------------------------------

                var jsonResponse = response.Content.ReadAsStringAsync().Result;
                var responseObj = new JavaScriptSerializer().DeserializeObject(jsonResponse);
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }
        // End of Send Notification

        public ActionResult ShowTopicDetails(String TopicID)
        {
            try
            {
                DataTable dt = new DataTable();
                LMSTopicsModel ret = new LMSTopicsModel();
                List<TopicMapList> TopicMapList1 = new List<TopicMapList>();

                ProcedureExecute proc = new ProcedureExecute("PRC_LMSTOPICSMASTER");
                proc.AddPara("@ACTION", "SHOWTOPIC");
                proc.AddPara("@TOPICID", TopicID);
                proc.AddPara("@BRANCHID", Convert.ToString(Session["userbranchHierarchy"]));
                proc.AddPara("@USERID", Convert.ToString(HttpContext.Session["userid"]));
                dt = proc.GetTable();

                if (dt != null && dt.Rows.Count > 0)
                {
                    TopicMapList1 = APIHelperMethods.ToModelList<TopicMapList>(dt);
                }

                return Json(TopicMapList1, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        [HttpPost]
        public JsonResult TopicDelete(string TopicID)
        {
            string output_msg = string.Empty;
           
            try
            {
                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_LMSTOPICSMASTER");
                proc.AddPara("@ACTION", "DELETETOPICS");
                proc.AddPara("@TOPICID", TopicID);
                proc.AddVarcharPara("@RETURN_VALUE", 500, "", QueryParameterDirection.Output);
                dt = proc.GetTable();
                output_msg = Convert.ToString(proc.GetParaValue("@RETURN_VALUE"));
            }
            catch (Exception ex)
            {
                output_msg = "Please try again later";
            }

            return Json(output_msg, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLastTopicSeq()
        {
            LMSTopicsModel model = new LMSTopicsModel();

            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_LMSTOPICSMASTER");
            proc.AddPara("@Action", "GETLASTTOPICSEQ");
            dt = proc.GetTable();

            if (dt != null && dt.Rows.Count > 0)
            {
                model.TopicSequence = Convert.ToString(dt.Rows[0]["TopicSequence"]);
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }


    }
}
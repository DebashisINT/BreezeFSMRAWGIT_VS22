/***************************************************************************************************************
 * Rev 1.0     Sanchita   V2.0.47    17/04/2024      0027370: Micro Learning Training content Deleting issue
 * Rev 2.0     Sanchita   V2.0.48    10/09/2024      27690: Quotation Notification issue @ Eurobond 
 * ***************************************************************************************************************/

using BusinessLogicLayer;
using MyShop.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class DocumentSharingController : Controller
    {
        //
        // GET: /MYSHOP/DocumentSharing/
        public ActionResult Index()
        {
            Session["id"] = null;
            return View();
        }


        public PartialViewResult PartialGrid()
        {
            return PartialView(GetList());
        }

        public JsonResult SaveDocumentGroup(string code, string name, string id)
        {
            int output = 0;
            string Userid = Convert.ToString(Session["userid"]);
            output = DocumentSharingModel.Obj.SaveDocumentSharing(code, name, Userid, id);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveDocumentGroupUser(string selected, string id)
        {
            int output = 0;
            string Userid = Convert.ToString(Session["userid"]);
            output = DocumentSharingUsers.Obj.SaveDocumentSharingUser(selected, id);


            SendNotification(selected, "");


            return Json(output, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SendNotification(string selected, string messagetext)
        {
            DBEngine odbengine = new DBEngine();
            string status = string.Empty;
            try
            {
                
                DataTable dt = odbengine.GetDataTable("select  device_token,musr.user_name,musr.user_id  from tbl_master_user as musr inner join tbl_FTS_devicetoken as token on musr.user_id=token.UserID  where musr.user_id in (" + selected + ") and musr.user_inactive='N'");

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToString(dt.Rows[i]["device_token"]) != "")
                        {
                            messagetext = "Hi " + Convert.ToString(dt.Rows[i]["user_name"]) + ". New document(s) shared with you. Thanks.";
                            // Rev 2.0
                            //SendPushNotification(messagetext, Convert.ToString(dt.Rows[i]["device_token"]), Convert.ToString(dt.Rows[i]["user_name"]), Convert.ToString(dt.Rows[i]["user_id"]));

                            CRMEnquiriesController obj = new CRMEnquiriesController();
                            obj.SendPushNotification(messagetext, Convert.ToString(dt.Rows[i]["device_token"]), Convert.ToString(dt.Rows[i]["user_name"]), Convert.ToString(dt.Rows[i]["user_id"]), "video_upload");
                            // End of Rev 2.0
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

        // Rev 2.0
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
        //                body = message,
        //                type = "video_upload"
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
        // End of Rev 2.0

        public JsonResult EditDocumentGroup(string id)
        {
            DataTable output = new DataTable();
            output = DocumentSharingModel.Obj.EditDocumentSharing(id);

            if (output.Rows.Count > 0)
            {
                return Json(new { code = Convert.ToString(output.Rows[0]["CODE"]), name = Convert.ToString(output.Rows[0]["NAME"]) }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { code = "", name = "" }, JsonRequestBehavior.AllowGet);
            }

        }

        public IEnumerable GetList()
        {
            string connectionString = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"]);
            ReportsDataContext dc = new ReportsDataContext(connectionString);
            var q = from d in dc.V_DOCSHARINGLISTs
                    select d;
            return q;

        }

        public PartialViewResult PartialUserGrid(string id)
        {

            return PartialView(GetUserList());
        }

        public IEnumerable GetUserList()
        {
            string connectionString = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"]);
            ReportsDataContext dc = new ReportsDataContext(connectionString);
            var q = from d in dc.tbl_master_users
                    select d;
            return q;

        }

        public JsonResult SetUsers(string ID)
        {
            DataTable DT = DocumentSharingUsers.Obj.GetUserMap(ID);
            DocumentSharingUsers.ObjList.Clear();
            foreach (DataRow dr in DT.Rows)
            {
                DocumentSharingUsers.ObjList.Add(Convert.ToString(dr["USER_ID"]));
            }

            var Selected = DocumentSharingUsers.ObjList;


            return Json(Selected);
        }

        public JsonResult Delete(string ID)
        {
            int output = 0;
            string CS = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];

            List<VideoFiles> videos = new List<VideoFiles>();
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("prc_GetDocument", con);
                cmd.Parameters.AddWithValue("@docgroup_id", ID);

                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    VideoFiles video = new VideoFiles();
                    video.ID = Convert.ToInt32(rdr["ID"]);
                    video.Name = rdr["Name"].ToString();
                    video.FileSize = Convert.ToInt32(rdr["FileSize"]);
                    video.FilePath = rdr["FilePath"].ToString();
                    video.Filetype = rdr["FileType"].ToString();
                    video.FileDescription = rdr["FileDescription"].ToString();
                    video.FilePathIcon = Convert.ToString(rdr["FilePathIcon"]);
                    video.IsActive = Convert.ToString(rdr["IsActive"]);
                    videos.Add(video);
                }
            }


            foreach (VideoFiles video in videos)
            {
                if (!string.IsNullOrEmpty(video.FilePathIcon))
                {
                    // Rev 1.0
                    //if (System.IO.File.Exists(Server.MapPath(video.FilePath)))
                    //{
                    //    System.IO.File.Delete(Server.MapPath(video.FilePath));
                    //}
                    if (System.IO.File.Exists(Server.MapPath(video.FilePathIcon)))
                    {
                        System.IO.File.Delete(Server.MapPath(video.FilePathIcon));
                    }
                    // End of Rev 1.0
                }

                if (!string.IsNullOrEmpty(video.FilePath))
                {
                    if (System.IO.File.Exists(Server.MapPath(video.FilePath)))
                    {
                        System.IO.File.Delete(Server.MapPath(video.FilePath));
                    }
                }
            }


            



            output = DocumentSharingModel.Obj.Delete(ID);
            return Json(output, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult DeleteDoc(string id)
        {
            string CS = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];

            VideoFiles video = new VideoFiles();
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("prc_GetDocument", con);
                cmd.Parameters.AddWithValue("@doc_id", id);

                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    video.ID = Convert.ToInt32(rdr["ID"]);
                    video.Name = rdr["Name"].ToString();
                    video.FileSize = Convert.ToInt32(rdr["FileSize"]);
                    video.FilePath = rdr["FilePath"].ToString();
                    video.Filetype = rdr["FileType"].ToString();
                    video.FileDescription = rdr["FileDescription"].ToString();
                    video.FilePathIcon = Convert.ToString(rdr["FilePathIcon"]);
                    video.IsActive = Convert.ToString(rdr["IsActive"]);

                }
            }


            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("prc_DeleteDocument", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }

            if (!string.IsNullOrEmpty(video.FilePathIcon))
            {
                //// Rev 1.0
                //if (System.IO.File.Exists(Server.MapPath(video.FilePath)))
                //{
                //    System.IO.File.Delete(Server.MapPath(video.FilePath));
                //}

                if (System.IO.File.Exists(Server.MapPath(video.FilePathIcon)))
                {
                    System.IO.File.Delete(Server.MapPath(video.FilePathIcon));
                }
                // End of Rev 1.0
            }

            if (!string.IsNullOrEmpty(video.FilePath))
            {
                if (System.IO.File.Exists(Server.MapPath(video.FilePath)))
                {
                    System.IO.File.Delete(Server.MapPath(video.FilePath));
                }
            }

            TempData["result"] = "File deleted.";
            return RedirectToAction("UploadVideo");
        }


        [HttpGet]
        public ActionResult UploadVideo(string id)
        {
            if (Session["id"] != null)
            {
                id = Session["id"].ToString();
            }



            DBEngine obj = new DBEngine();

            String Group_Name = Convert.ToString(obj.GetDataTable("select NAME from FSM_DOCGROUP WHERE ID=" + id).Rows[0][0]);

            ViewBag.Group_Name = Group_Name;

            Session["id"] = id;
            if (TempData["result"] != null)
                ViewBag.result = TempData["result"];
            List<VideoFiles> videolist = new List<VideoFiles>();
            string CS = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("prc_GetDocument", con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    VideoFiles video = new VideoFiles();
                    video.ID = Convert.ToInt32(rdr["ID"]);
                    video.Name = rdr["Name"].ToString();
                    video.FileSize = Convert.ToInt32(rdr["FileSize"]);
                    video.FilePath = rdr["FilePath"].ToString();
                    video.Filetype = rdr["FileType"].ToString();
                    video.FileDescription = rdr["FileDescription"].ToString();
                    video.FilePathIcon = Convert.ToString(rdr["FilePathIcon"]);
                    video.IsActive = Convert.ToString(rdr["IsActive"]);
                    videolist.Add(video);
                }
            }
            return View(videolist);
        }

        [HttpPost]
        public ActionResult UploadVideo(HttpPostedFileBase fileupload, string FileName, string FileDescription, HttpPostedFileBase fileuploadicon, string hdnAddEdit,string hdnFileDuration, bool Active = false)
        {
            var allowedExtensions = new[] { ".pdf", ".ppt", ".pptx", ".mp4", ".doc", ".docx" };

            var allowedExtensionsicon = new[] { ".jpeg", ".tif", ".png", ".gif" };

            if (hdnAddEdit == "")
            {
                if (fileupload != null)
                {
                    if (!string.IsNullOrEmpty(FileDescription))
                    {

                        string fileName = Path.GetFileName(fileupload.FileName);
                        int fileSize = fileupload.ContentLength;
                        int Size = fileSize / 1000;
                        string FileType = System.IO.Path.GetExtension(fileName);
                        int SizeIcon = 0;
                        string FileTypeIcon = "";
                        string fileNameicon = "";
                        if (fileuploadicon != null)
                        {
                            fileNameicon = Path.GetFileName(fileuploadicon.FileName);
                            int fileSizeicon = fileuploadicon.ContentLength;
                            SizeIcon = fileSizeicon / 1000;
                            FileTypeIcon = System.IO.Path.GetExtension(fileNameicon);
                        }


                        if (!allowedExtensions.Contains(FileType.ToLower()))
                        {
                            TempData["result"] = "Selected file extention does not support.";

                        }

                        else if (Size > 20480)
                        {
                            TempData["result"] = "Maximum file size should be 20MB.";

                        }
                        else if (SizeIcon > 1024 && SizeIcon != 0)
                        {
                            TempData["result"] = "Maximum icon file size should be 1MB.";

                        }
                        else if (!allowedExtensionsicon.Contains(FileTypeIcon.ToLower()) && FileTypeIcon != "")
                        {
                            TempData["result"] = "Selected file extention does not support for icon.";

                        }
                        else
                        {
                            //if (!Directory.Exists("~/Commonfolder/DocumentSharing/"))
                            //{
                            //    Directory.CreateDirectory("~/Commonfolder/DocumentSharing/");
                            //}


                            if (System.IO.File.Exists(Server.MapPath("~/Commonfolder/DocumentSharing/" + fileName)))
                            {
                                fileName = DateTime.Now.ToString("hhmmss") + fileName;
                            }


                            fileupload.SaveAs(Server.MapPath("~/Commonfolder/DocumentSharing/" + fileName));


                            if (fileuploadicon != null)
                            {

                                if (System.IO.File.Exists(Server.MapPath("~/Commonfolder/DocumentSharing/Icon/" + fileNameicon)))
                                {
                                    fileNameicon = DateTime.Now.ToString("hhmmss") + fileNameicon;
                                }

                                fileuploadicon.SaveAs(Server.MapPath("~/Commonfolder/DocumentSharing/Icon/" + fileNameicon));
                            }
                            TempData["result"] = "File uploaded successfully.";
                            string CS = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                            using (SqlConnection con = new SqlConnection(CS))
                            {
                                SqlCommand cmd = new SqlCommand("prc_SaveDocument", con);
                                cmd.CommandType = CommandType.StoredProcedure;
                                con.Open();
                                cmd.Parameters.AddWithValue("@Group_id", Session["id"]);
                                cmd.Parameters.AddWithValue("@Name", fileName);
                                cmd.Parameters.AddWithValue("@FileSize", Size);
                                cmd.Parameters.AddWithValue("@FileType", FileType);
                                cmd.Parameters.AddWithValue("@Active", Active);
                                cmd.Parameters.AddWithValue("@hdnFileDuration", hdnFileDuration);
                                cmd.Parameters.AddWithValue("FilePath", "~/Commonfolder/DocumentSharing/" + fileName);
                                if (fileuploadicon != null)
                                {
                                    cmd.Parameters.AddWithValue("FilePathIcon", "~/Commonfolder/DocumentSharing/Icon/" + fileNameicon);
                                }
                                cmd.Parameters.AddWithValue("@FileDescription", FileDescription);
                                cmd.ExecuteNonQuery();
                            }
                        }


                    }
                    else
                    {
                        TempData["result"] = "Please enter description.";
                    }
                }


                else
                {
                    TempData["result"] = "Please select file.";
                }
            }
            else
            {
                string fileName = "";
                string fileNameicon = "";
                int fileSize = 0;
                int Size = 0;
                string FileType = "";
                if (fileupload != null)
                {
                    fileName = Path.GetFileName(fileupload.FileName);
                    fileSize = fileupload.ContentLength;
                    Size = fileSize / 1000;
                    FileType = System.IO.Path.GetExtension(fileName);
                    if (!allowedExtensions.Contains(FileType.ToLower()))
                    {
                        TempData["result"] = "Selected file extention does not support.";

                    }
                    else if (Size > 20480)
                    {
                        TempData["result"] = "Maximum file size should be 20MB.";

                    }
                    else
                    {
                        if (System.IO.File.Exists(Server.MapPath("~/Commonfolder/DocumentSharing/" + fileName)))
                        {
                            fileName = DateTime.Now.ToString("hhmmss") + fileName;
                        }
                        fileupload.SaveAs(Server.MapPath("~/Commonfolder/DocumentSharing/" + fileName));
                    }
                }

                if (fileuploadicon != null)
                {
                    fileNameicon = Path.GetFileName(fileuploadicon.FileName);
                    int fileSizeicon = fileuploadicon.ContentLength;
                    int SizeIcon = fileSizeicon / 1000;
                    string FileTypeIcon = System.IO.Path.GetExtension(fileNameicon);
                    if (SizeIcon > 1024)
                    {
                        TempData["result"] = "Maximum icon file size should be 1MB.";

                    }
                    else if (!allowedExtensionsicon.Contains(FileTypeIcon.ToLower()))
                    {
                        TempData["result"] = "Selected file extention does not support for icon.";

                    }
                    else
                    {
                        if (System.IO.File.Exists(Server.MapPath("~/Commonfolder/DocumentSharing/Icon/" + fileNameicon)))
                        {
                            fileNameicon = DateTime.Now.ToString("hhmmss") + fileNameicon;
                        }
                        fileuploadicon.SaveAs(Server.MapPath("~/Commonfolder/DocumentSharing/Icon/" + fileNameicon));
                    }
                }


                if (string.IsNullOrEmpty(FileDescription))
                {
                    TempData["result"] = "Please enter description.";
                }




                if (TempData["result"] == "" || TempData["result"] == null)
                {
                    //if (!Directory.Exists("~/Commonfolder/DocumentSharing/"))
                    //{
                    //    Directory.CreateDirectory("~/Commonfolder/DocumentSharing/");
                    //}
                    TempData["result"] = "Updated succsessfully.";

                    string CS = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        SqlCommand cmd = new SqlCommand("prc_UpdateDocument", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.AddWithValue("@id", hdnAddEdit);
                        cmd.Parameters.AddWithValue("@Active", Active);
                        if (fileName != "")
                        {
                            cmd.Parameters.AddWithValue("@Name", fileName);
                            cmd.Parameters.AddWithValue("@FileSize", Size);
                            cmd.Parameters.AddWithValue("@FileType", FileType);
                            cmd.Parameters.AddWithValue("@FileDuration", hdnFileDuration);
                            cmd.Parameters.AddWithValue("FilePath", "~/Commonfolder/DocumentSharing/" + fileName);
                        }
                        if (fileNameicon != "")
                        {
                            cmd.Parameters.AddWithValue("FilePathIcon", "~/Commonfolder/DocumentSharing/Icon/" + fileNameicon);
                        }
                        cmd.Parameters.AddWithValue("@FileDescription", FileDescription);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            return Json(TempData["result"], JsonRequestBehavior.AllowGet);
        }





    }
}
using BusinessLogicLayer;
using DataAccessLayer;
using LMS.Models;
using Models;
using MyShop.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using UtilityLayer;

namespace LMS.Areas.LMS.Controllers
{
    public class LMSContentUploadController : Controller
    {
        // GET: LMS/ContentUpload
        public ActionResult Index()
        {
            //List<VideoFiles> videolist = new List<VideoFiles>();

            //string CS = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
            //using (SqlConnection con = new SqlConnection(CS))
            //{
            //    SqlCommand cmd = new SqlCommand("PRC_LMSCONTENTMASTER", con);
            //    cmd.Parameters.AddWithValue("@ACTION", "GETLISTINGDATA");
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    con.Open();
            //    SqlDataReader rdr = cmd.ExecuteReader();
            //    while (rdr.Read())
            //    {
            //        VideoFiles video = new VideoFiles();
            //        video.ID = Convert.ToInt32(rdr["ID"]);
            //        video.Name = rdr["Name"].ToString();
            //        video.FileSize = Convert.ToInt32(rdr["FileSize"]);
            //        video.FilePath = rdr["FilePath"].ToString();
            //        video.Filetype = rdr["FileType"].ToString();
            //        video.FileDescription = rdr["FileDescription"].ToString();
            //        video.FilePathIcon = Convert.ToString(rdr["FilePathIcon"]);
            //        video.IsActive = Convert.ToString(rdr["IsActive"]);
            //        videolist.Add(video);
            //    }
            //}

            LMSContentModel Dtls = new LMSContentModel();

            EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/LMSContentUpload/Index");
            ViewBag.CanAdd = rights.CanAdd;
            ViewBag.CanView = rights.CanView;
            ViewBag.CanExport = rights.CanExport;
            ViewBag.CanEdit = rights.CanEdit;
            ViewBag.CanDelete = rights.CanDelete;

            DBEngine obj1 = new DBEngine();
            ViewBag.LMSVideoUploadSize = Convert.ToString(obj1.GetDataTable("select [value] from FTS_APP_CONFIG_SETTINGS WHERE [Key]='LMSVideoUploadSize'").Rows[0][0]);

            List<TopicList> modelTopic = new List<TopicList>();

            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("PRC_LMSCONTENTMASTER");
            proc.AddPara("@ACTION", "GETDROPDOWNBINDDATA");
            ds = proc.GetDataSet();

            if (ds != null)
            {
                // Company
                List<TopicList> TopicList = new List<TopicList>();
                TopicList = APIHelperMethods.ToModelList<TopicList>(ds.Tables[0]);
                Dtls.TopicList = TopicList;
            }

            return View(Dtls);
        }

        public ActionResult GetLMSTopicList()
        {
            try
            {
                List<TopicList> modelTopic = new List<TopicList>();

                DataSet ds = new DataSet();
                ProcedureExecute proc = new ProcedureExecute("PRC_LMSCONTENTMASTER");
                proc.AddPara("@ACTION", "GETDROPDOWNBINDDATA");
                ds = proc.GetDataSet();

                if (ds != null)
                {
                    // Company
                    List<TopicList> TopicList = new List<TopicList>();
                    TopicList = APIHelperMethods.ToModelList<TopicList>(ds.Tables[0]);
                    modelTopic = TopicList;
                }

                return PartialView("~/Areas/LMS/Views/LMSContentUpload/_LMSTopicPartial.cshtml", modelTopic);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        //public ActionResult GetContentListing()
        //{
        //    List<VideoFiles> videolist = new List<VideoFiles>();

        //    string CS = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
        //    using (SqlConnection con = new SqlConnection(CS))
        //    {
        //        SqlCommand cmd = new SqlCommand("PRC_LMSCONTENTMASTER", con);
        //        cmd.Parameters.AddWithValue("@ACTION", "GETLISTINGDATA");
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        con.Open();
        //        SqlDataReader rdr = cmd.ExecuteReader();
        //        while (rdr.Read())
        //        {
        //            VideoFiles video = new VideoFiles();
        //            video.ID = Convert.ToInt32(rdr["ID"]);
        //            video.Name = rdr["Name"].ToString();
        //            video.FileSize = Convert.ToInt32(rdr["FileSize"]);
        //            video.FilePath = rdr["FilePath"].ToString();
        //            video.Filetype = rdr["FileType"].ToString();
        //            video.FileDescription = rdr["FileDescription"].ToString();
        //            video.FilePathIcon = Convert.ToString(rdr["FilePathIcon"]);
        //            video.IsActive = Convert.ToString(rdr["IsActive"]);
        //            videolist.Add(video);
        //        }
        //    }

        //    return View(videolist);
        //}

        public ActionResult PartialContentGridList(LMSContentModel model)
        {
            try
            {
                if (model.Is_PageLoad == "TotalContents" || model.Is_PageLoad == "ActiveContents" || model.Is_PageLoad == "InactiveContents")
                {
                    string Is_PageLoad = model.Is_PageLoad;

                    model.Is_PageLoad = "Ispageload";

                    return PartialView("PartialContentGridList", GetContentDetails(Is_PageLoad));
                }
                else
                {
                    EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/LMSContentUpload/Index");
                    ViewBag.CanAdd = rights.CanAdd;
                    ViewBag.CanView = rights.CanView;
                    ViewBag.CanExport = rights.CanExport;
                    ViewBag.CanEdit = rights.CanEdit;
                    ViewBag.CanDelete = rights.CanDelete;

                    string Is_PageLoad = string.Empty;

                    if (model.Is_PageLoad == "Ispageload")
                    {
                        Is_PageLoad = "is_pageload";

                    }


                    GetContentListing(Is_PageLoad);

                    model.Is_PageLoad = "Ispageload";

                    return PartialView("PartialContentGridList", GetContentDetails(Is_PageLoad));
                }
                
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        public void GetContentListing(string Is_PageLoad)
        {
            string user_id = Convert.ToString(Session["userid"]);

            string action = string.Empty;
            DataTable formula_dtls = new DataTable();
            DataSet dsInst = new DataSet();

            try
            {
                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_LMSCONTENTMASTER");
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

        public IEnumerable GetContentDetails(string Is_PageLoad)
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
                if (Is_PageLoad == "ActiveContents")
                {
                    LMSMasterDataContext dc = new LMSMasterDataContext(connectionString);
                    var q = from d in dc.LMS_CONTENTMASTER_LISTINGs
                            where d.USERID == Convert.ToInt32(Userid) && d.CONTENTSTATUS == "Yes"
                            orderby d.SEQ
                            select d;
                    return q;
                }
                else if (Is_PageLoad == "InactiveContents")
                {
                    LMSMasterDataContext dc = new LMSMasterDataContext(connectionString);
                    var q = from d in dc.LMS_CONTENTMASTER_LISTINGs
                            where d.USERID == Convert.ToInt32(Userid) && d.CONTENTSTATUS == "No"
                            orderby d.SEQ
                            select d;
                    return q;
                }
                else
                {
                    LMSMasterDataContext dc = new LMSMasterDataContext(connectionString);
                    var q = from d in dc.LMS_CONTENTMASTER_LISTINGs
                            where d.USERID == Convert.ToInt32(Userid)
                            orderby d.SEQ
                            select d;
                    return q;
                }
                
            }
            else
            {
                LMSMasterDataContext dc = new LMSMasterDataContext(connectionString);
                var q = from d in dc.LMS_CONTENTMASTER_LISTINGs
                        where d.USERID == Convert.ToInt32(Userid) && d.SEQ == 1111119
                        orderby d.SEQ 
                        select d;
                return q;
            }


        }


        [HttpPost]
        //public ActionResult SaveContent(HttpPostedFileBase fileupload, string hdnAddEditMode, string hdnContentID, string hdnFileDuration,
        //            string txtContentTitle, string txtContentDesc, string numPlaySequence, string hdnTopicID,
        //            string chkStatus, string chkAllowLike , string chkAllowComments, string chkAllowShare)
        public ActionResult SaveContent(HttpPostedFileBase fileupload, string hdnAddEditMode, string hdnContentID, string hdnFileDuration,
                    string txtContentTitle, string txtContentDesc, string numPlaySequence, LMSContentModel data,
                    string chkStatus, string chkAllowLike, string chkAllowComments, string chkAllowShare)
        {
            try
            {
                string RETURN_VALUE = string.Empty;
                string RETURN_DUPLICATEMAPNAME = string.Empty;
                int IsValid = 1;

                if (chkStatus != null && chkStatus == "on")
                {
                    chkStatus = "1";
                }
                else { 
                    chkStatus = "0"; 
                }


                if (chkAllowLike != null && chkAllowLike == "on")
                {
                    chkAllowLike = "1";
                }
                else
                {
                    chkAllowLike = "0";
                }

                if (chkAllowComments != null && chkAllowComments == "on")
                {
                    chkAllowComments = "1";
                }
                else
                {
                    chkAllowComments = "0";
                }

                if (chkAllowShare != null && chkAllowShare == "on")
                {
                    chkAllowShare = "1";
                }
                else
                {
                    chkAllowShare = "0";
                }

                var allowedExtensions = new[] {".mp4"};
                string fileName = "";
                int fileSize = 0;
                //int Size = fileSize / 1000;
                string FileType = "";

                DBEngine obj1 = new DBEngine();
                var LMSVideoUploadSize = Convert.ToString(obj1.GetDataTable("select [value] from FTS_APP_CONFIG_SETTINGS WHERE [Key]='LMSVideoUploadSize'").Rows[0][0]);

                //LMSContentAddModel data = new LMSContentAddModel();

                if (fileupload != null)
                {
                    fileName = Path.GetFileName(fileupload.FileName);
                    fileSize = fileupload.ContentLength;
                    //int Size = fileSize / 1000;
                    FileType = System.IO.Path.GetExtension(fileName);


                    if (!allowedExtensions.Contains(FileType.ToLower()))
                    {
                        IsValid = 0;
                        RETURN_VALUE = "Invalid file. Only .mp4 types of files shall be supported.";
                    }
                    else
                    {
                        if (fileSize > (Convert.ToDecimal(LMSVideoUploadSize) * 1024 * 1024))
                        {
                            IsValid = 0;
                            RETURN_VALUE = "Maximum file size shall be " + LMSVideoUploadSize + " MB.";
                        }
                        else
                        {
                            IsValid = 1;
                            
                        }
                    }
                }
                else
                {
                    if (hdnAddEditMode == "ADDCONTENT")
                    {
                        IsValid = 0;
                        RETURN_VALUE = "Please select file.";
                    }
                    else if (hdnAddEditMode == "EDITCONTENT")
                    {
                        IsValid = 1;
                        hdnFileDuration = "0";
                    }
                    
                }

                

                if (IsValid == 1)
                {
                    ProcedureExecute proc = new ProcedureExecute("PRC_LMSCONTENTMASTER");
                    proc.AddPara("@ACTION", hdnAddEditMode);
                    proc.AddPara("@CONTENTID", hdnContentID);
                    proc.AddPara("@CONTENTTITLE", txtContentTitle);
                    proc.AddPara("@CONTENTDESC", txtContentDesc);
                    proc.AddPara("@TOPICID", data.TopicId);
                    proc.AddPara("@PLAYSEQUENCE", numPlaySequence);
                    proc.AddPara("@STATUS", chkStatus);
                    proc.AddPara("@ALLOWLIKE", chkAllowLike);
                    proc.AddPara("@ALLOWCOMMENTS", chkAllowComments);
                    proc.AddPara("@ALLOWSHARE", chkAllowShare);
                    proc.AddPara("@USERID", Convert.ToString(HttpContext.Session["userid"]));

                    proc.AddPara("@CONTENT_FILENAME", fileName);
                    proc.AddPara("@CONTENT_FILESIZE", fileSize);
                    proc.AddPara("@CONTENT_FILETYPE", FileType);
                    proc.AddPara("@CONTENT_FILEDURATION", hdnFileDuration);
                    proc.AddPara("@CONTENT_FILEPATH", "~/Commonfolder/LMS/ContentUpload/" + fileName);

                    proc.AddVarcharPara("@RETURN_VALUE", 500, "", QueryParameterDirection.Output);
                    proc.AddVarcharPara("@RETURN_DUPLICATEMAPNAME", -1, "", QueryParameterDirection.Output);
                    int k = proc.RunActionQuery();
                    RETURN_VALUE = Convert.ToString(proc.GetParaValue("@RETURN_VALUE"));
                    //RETURN_DUPLICATEMAPNAME = Convert.ToString(proc.GetParaValue("@RETURN_DUPLICATEMAPNAME"));

                    if (RETURN_VALUE == "Content added succesfully." || RETURN_VALUE == "Content updated succesfully."){
                        if (fileName != null && fileName != "")
                        {
                            if (!System.IO.Directory.Exists(Server.MapPath("~/Commonfolder/LMS/ContentUpload/")))
                            {
                                // If Folder doesnot exists, CREATE the folder
                                System.IO.Directory.CreateDirectory(Server.MapPath("~/Commonfolder/LMS/ContentUpload/"));
                            }
                            else if (System.IO.File.Exists(Server.MapPath("~/Commonfolder/LMS/ContentUpload/" + fileName)))
                            {
                                // If file name already exists, RENAME the file by concatinating it by datetime
                                fileName = DateTime.Now.ToString("hhmmss") + fileName;
                            }
                            fileupload.SaveAs(Server.MapPath("~/Commonfolder/LMS/ContentUpload/" + fileName));
                        }
                    }

                    

                }


                TempData["result"] = RETURN_VALUE;

                return Json(TempData["result"], JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult ShowContentDetails(String ContentId)
        {
            try
            {
                DataTable dt = new DataTable();
              //  LMSContentAddModel ret = new LMSContentAddModel();
                List<LMSContentEditModel> TopicMapList1 = new List<LMSContentEditModel>();

                ProcedureExecute proc = new ProcedureExecute("PRC_LMSCONTENTMASTER");
                proc.AddPara("@ACTION", "SHOWCONTENT");
                proc.AddPara("@CONTENTID", ContentId);
                proc.AddPara("@BRANCHID", Convert.ToString(Session["userbranchHierarchy"]));
                proc.AddPara("@USERID", Convert.ToString(HttpContext.Session["userid"]));
                dt = proc.GetTable();

                if (dt != null && dt.Rows.Count > 0)
                {
                    TopicMapList1 = APIHelperMethods.ToModelList<LMSContentEditModel>(dt);
                }

                return Json(TopicMapList1, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        [HttpPost]
        public JsonResult DeleteContent(string ContentId)
        {
            string output_msg = string.Empty;

            try
            {
                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_LMSCONTENTMASTER");
                proc.AddPara("@ACTION", "DELETECONTENTS");
                proc.AddPara("@CONTENTID", ContentId);
                proc.AddVarcharPara("@RETURN_VALUE", 500, "", QueryParameterDirection.Output);
                dt = proc.GetTable();
                output_msg = Convert.ToString(proc.GetParaValue("@RETURN_VALUE"));


                if (output_msg != "-10" && output_msg != null && output_msg != "")
                {
                    string fileName = output_msg;

                    if (System.IO.File.Exists(Server.MapPath("~/Commonfolder/LMS/ContentUpload/" + fileName)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Commonfolder/LMS/ContentUpload/" + fileName));

                    }

                    output_msg = "1";
                }

            }
            catch (Exception ex)
            {
                output_msg = "Please try again later";
            }

            return Json(output_msg, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetContentCount()
        {
            LMSContentModel dtl = new LMSContentModel();
            try
            {
                DataSet ds = new DataSet();
                ProcedureExecute proc = new ProcedureExecute("PRC_LMSCONTENTMASTER");
                proc.AddPara("@Action", "GETCONTENTCOUNTDATA");
                proc.AddPara("@userid", Convert.ToString(HttpContext.Session["userid"]));
                ds = proc.GetDataSet();

                int TotalContents = 0;
                int ActiveContents = 0;
                int InactiveContents = 0;
                
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    TotalContents = Convert.ToInt32(item["cnt_TotalContents"]);
                    ActiveContents = Convert.ToInt32(item["cnt_ActiveContents"]);
                    InactiveContents = Convert.ToInt32(item["cnt_InactiveContents"]);
                }

                dtl.TotalContents = TotalContents;
                dtl.ActiveContents = ActiveContents;
                dtl.InactiveContents = InactiveContents;
                
            }
            catch
            {
            }
            return Json(dtl);
        }
    }
}
using BusinessLogicLayer;
using DataAccessLayer;
using DevExpress.Web.Mvc;
using DevExpress.Web;
using LMS.Models;
using Models;
//using MyShop.Models;
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
using DevExpress.Charts.Native;
using DevExpress.Data.XtraReports.Wizard.Presenters;
using NReco.VideoConverter;
using DevExpress.Utils.Drawing.Helpers;
using System.Net;
using System.Web.Script.Serialization;
using System.Text;
using Google.Apis.Auth.OAuth2;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Data.SqlTypes;
using DevExpress.DataAccess.Native.Data;
using DevExpress.XtraCharts.Native;
using DataTable = System.Data.DataTable;

namespace LMS.Areas.LMS.Controllers
{
    public class LMSContentUploadController : Controller
    {
       
        DBEngine odbengine = new DBEngine();


        // GET: LMS/ContentUpload
        public ActionResult Index()
        {

            LMSContentModel Dtls = new LMSContentModel();

            EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/LMSContentUpload/Index");
            ViewBag.CanAdd = rights.CanAdd;
            ViewBag.CanView = rights.CanView;
            ViewBag.CanExport = rights.CanExport;
            ViewBag.CanEdit = rights.CanEdit;
            ViewBag.CanDelete = rights.CanDelete;

            ViewBag.EditRights = 0;
            if(ViewBag.CanEdit)
            {
                ViewBag.EditRights = 1;
            }

            ViewBag.DeleteRights = 0;
            if (ViewBag.CanDelete)
            {
                ViewBag.DeleteRights = 1;
            }


            EntityLayer.CommonELS.UserRightsForPage rightsQ = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/LMSQuestions/Index");
            ViewBag.CanAddQues = rightsQ.CanAdd;
            ViewBag.CanEditQues = rightsQ.CanEdit;

            ViewBag.AddRightsQues = 0;
            if (ViewBag.CanAddQues)
            {
                ViewBag.AddRightsQues = 1;
            }

            ViewBag.CanEditQues = 0;
            if (ViewBag.CanAddQues)
            {
                ViewBag.EditRightsQues = 1;
            }


            EntityLayer.CommonELS.UserRightsForPage rightsT = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/LMSTopics/Index");
            ViewBag.CanAddTopic = rightsT.CanAdd;



            //DBEngine obj1 = new DBEngine();
            //ViewBag.LMSVideoUploadSize = Convert.ToString(obj1.GetDataTable("select [value] from FTS_APP_CONFIG_SETTINGS WHERE [Key]='LMSVideoUploadSize'").Rows[0][0]);

           
            DataTable dt = new DataTable();
            //String con1 = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
           
            //SqlCommand sqlcmd1 = new SqlCommand();
            //SqlConnection sqlcon1 = new SqlConnection(con1);
            //sqlcon1.Open();
            //sqlcmd1 = new SqlCommand("Proc_SystemsettingResult", sqlcon1);
            //sqlcmd1.Parameters.AddWithValue("@VariableName", "LMSVideoUploadSize");
            //sqlcmd1.CommandType = CommandType.StoredProcedure;
            //SqlDataAdapter da = new SqlDataAdapter(sqlcmd1);
            //da.Fill(dt);
            //sqlcon1.Close();
            //sqlcmd1.Dispose();


            //if (dt.Rows.Count > 0)
            //{
            //    ViewBag.LMSVideoUploadSize = Convert.ToString(dt.Rows[0]["Variable_Value"]);
            //}



            //DataTable dtAPP_CONFIG = new DataTable();
            //ProcedureExecute procCONFIG = new ProcedureExecute("PRC_LMSCONTENTMASTER");
            //procCONFIG.AddPara("@ACTION", "GETLMSVIDEOUPLOADSIZE");
            //dtAPP_CONFIG = procCONFIG.GetTable();

            //if (dtAPP_CONFIG != null)
            //{
            //    ViewBag.LMSVideoUploadSize = dtAPP_CONFIG.Rows[0]["value"];
            //}

            DataTable dtAPP_CONFIG = new DataTable();
            String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
            SqlCommand sqlcmd = new SqlCommand();
            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();
            sqlcmd = new SqlCommand("PRC_LMSCONTENTMASTER", sqlcon);
            sqlcmd.Parameters.AddWithValue("@ACTION", "GETLMSVIDEOUPLOADSIZE");
            sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
            da1.Fill(dtAPP_CONFIG);
            sqlcon.Close();
            

            if (dtAPP_CONFIG != null)
            {
                ViewBag.LMSVideoUploadSize = dtAPP_CONFIG.Rows[0]["value"];
            }


            DataSet ds = new DataSet();

            //DataTable dt = new DataTable();
            dt = GetTopicListData();

            if (dt != null)
            {
                List<TopicList> TopicList = new List<TopicList>();
                TopicList = APIHelperMethods.ToModelList<TopicList>(dt);
                Dtls.TopicList = TopicList;
            }

            //ProcedureExecute proc1 = new ProcedureExecute("PRC_LMSTOPICSMASTER");
            //proc1.AddPara("@ACTION", "GETDROPDOWNBINDDATA");
            //ds = proc1.GetDataSet();

           // String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
           // SqlCommand sqlcmd = new SqlCommand();
           // SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();
            sqlcmd = new SqlCommand("PRC_LMSTOPICSMASTER", sqlcon);
            sqlcmd.Parameters.AddWithValue("@ACTION", "GETDROPDOWNBINDDATA");
            sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(ds);
            sqlcon.Close();
            sqlcmd.Dispose();


            if (ds != null)
            {
                // Company
                List<TopicBasedOnList> TopicBasedOnList = new List<TopicBasedOnList>();
                TopicBasedOnList = APIHelperMethods.ToModelList<TopicBasedOnList>(ds.Tables[0]);
                Dtls.TopicBasedOnList = TopicBasedOnList;
            }

            ViewBag.CONTENT_TOPICIDS = 0;
            ViewBag.CONTENT_ID = 0;
            ViewBag.CONTENT_NAME = "0";
            ViewBag.TOPIC_NAME = "0";
            ViewBag.CONTENT_FROMSAVE = "0";

            if (TempData["TopicID"] != null)
            {
                ViewBag.CONTENT_TOPICIDS = Convert.ToInt64(TempData["TopicID"]);
                ViewBag.CONTENT_ID = Convert.ToInt64(TempData["ContentID"]);
                ViewBag.CONTENT_NAME = TempData["ContentName"];
                ViewBag.TOPIC_NAME = TempData["TopicName"];
                ViewBag.CONTENT_FROMSAVE = TempData["fromSave"];

                TempData["TopicID"] = null;

            }
            

            return View(Dtls);
        }

        public DataTable GetTopicListData()
        {
            DataTable dt = new DataTable();

            //ProcedureExecute proc = new ProcedureExecute("PRC_LMSCONTENTMASTER");
            //proc.AddPara("@ACTION", "GETDROPDOWNBINDDATA");
            //dt = proc.GetTable();

            String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
            SqlCommand sqlcmd = new SqlCommand();
            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();
            sqlcmd = new SqlCommand("PRC_LMSCONTENTMASTER", sqlcon);
            sqlcmd.Parameters.AddWithValue("@ACTION", "GETDROPDOWNBINDDATA");
            sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dt);
            sqlcon.Close();
            sqlcmd.Dispose();

            return dt;
        }

        public JsonResult GetTopicList()
        {
            LMSContentModel Dtls = new LMSContentModel();
            DataTable dt = new DataTable();
            dt = GetTopicListData();

            if (dt != null)
            {
                // Company
                List<TopicList> TopicList = new List<TopicList>();
                TopicList = APIHelperMethods.ToModelList<TopicList>(dt);
                Dtls.TopicList = TopicList;
            }
            return Json(Dtls.TopicList, JsonRequestBehavior.AllowGet);
        }
        

        public JsonResult GetContentGridListData(string TopicID)
        {
            DataTable dt = new DataTable();
            
            ////  LMSContentAddModel ret = new LMSContentAddModel();
            List<ContentListingData> ContentList1 = new List<ContentListingData>();

            //ProcedureExecute proc = new ProcedureExecute("PRC_LMSCONTENTMASTER");
            //proc.AddPara("@ACTION", "GETLISTINGDATA");
            //proc.AddPara("@BRANCHID", Convert.ToString(Session["userbranchHierarchy"]));
            //proc.AddPara("@TOPICID", TopicID);
            //proc.AddPara("@USERID", Convert.ToString(HttpContext.Session["userid"]));
            //dt = proc.GetTable();

            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    ContentList1 = APIHelperMethods.ToModelList<ContentListingData>(dt);
            //}


            string CS = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("PRC_LMSCONTENTMASTER", con);
                
                cmd.Parameters.AddWithValue("@ACTION", "GETLISTINGDATA");
                cmd.Parameters.AddWithValue("@BRANCHID", Convert.ToString(Session["userbranchHierarchy"]));
                cmd.Parameters.AddWithValue("@TOPICID", TopicID);
                cmd.Parameters.AddWithValue("@USERID", Convert.ToString(HttpContext.Session["userid"]));
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                // SqlDataReader rdr = cmd.ExecuteReader();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // this will query your database and return the result to your datatable
                da.Fill(dt);
                con.Close();
                da.Dispose();

                if (dt != null && dt.Rows.Count > 0)
                {
                    ContentList1 = APIHelperMethods.ToModelList<ContentListingData>(dt);
                }
            }



            return Json(ContentList1, JsonRequestBehavior.AllowGet);


        }

        public JsonResult GetTopicListForBoxData()
        {
            List<TopicListForBoxData> TopicList = new List<TopicListForBoxData>();
            DataTable dt = new DataTable();


            //ProcedureExecute proc = new ProcedureExecute("PRC_LMSCONTENTMASTER");
            //proc.AddPara("@ACTION", "GETTOPICLISTFORBOXDATA");
            //dt = proc.GetTable();

            String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
            SqlCommand sqlcmd = new SqlCommand();
            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();
            sqlcmd = new SqlCommand("PRC_LMSCONTENTMASTER", sqlcon);
            sqlcmd.Parameters.AddWithValue("@ACTION", "GETTOPICLISTFORBOXDATA");
            sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dt);
            sqlcon.Close();
            sqlcmd.Dispose();

            if (dt != null)
            {
                // Company
                
                TopicList = APIHelperMethods.ToModelList<TopicListForBoxData>(dt);
                
            }
            return Json(TopicList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLastPlaySeq(string topicid)
        {
            LMSContentEditModel model = new LMSContentEditModel();
           
            DataTable dt = new DataTable();

            //ProcedureExecute proc = new ProcedureExecute("PRC_LMSCONTENTMASTER");
            //proc.AddPara("@Action", "GETLASTPLAYSEQ");
            //proc.AddPara("@TOPICID", topicid);
            //dt = proc.GetTable();

            String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
            SqlCommand sqlcmd = new SqlCommand();
            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();
            sqlcmd = new SqlCommand("PRC_LMSCONTENTMASTER", sqlcon);
            sqlcmd.Parameters.AddWithValue("@ACTION", "GETLASTPLAYSEQ");
            sqlcmd.Parameters.AddWithValue("@TOPICID", topicid);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dt);
            sqlcon.Close();
            sqlcmd.Dispose();

            if (dt != null && dt.Rows.Count>0 )
            {
                model.PlaySequence = Convert.ToString(dt.Rows[0]["PlaySequence"]);
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLastTopicSeq()
        {
            LMSContentEditModel model = new LMSContentEditModel();

            DataTable dt = new DataTable();
            //ProcedureExecute proc = new ProcedureExecute("PRC_LMSTOPICSMASTER");
            //proc.AddPara("@Action", "GETLASTTOPICSEQ");
            //dt = proc.GetTable();

            String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
            SqlCommand sqlcmd = new SqlCommand();
            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();
            sqlcmd = new SqlCommand("PRC_LMSTOPICSMASTER", sqlcon);
            sqlcmd.Parameters.AddWithValue("@Action", "GETLASTTOPICSEQ");
            sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dt);
            sqlcon.Close();
            sqlcmd.Dispose();

            if (dt != null && dt.Rows.Count > 0)
            {
                model.TopicSequence = Convert.ToString(dt.Rows[0]["TopicSequence"]);
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult SaveContent(HttpPostedFileBase fileupload, HttpPostedFileBase fileuploadicon, string hdnAddEditMode, string hdnContentID, string hdnFileDuration,
                    string txtContentTitle, string txtContentDesc, string numPlaySequence, LMSContentModel data,
                    string chkStatus, string chkAllowLike, string chkAllowComments, string chkAllowShare, string hdnContentStatusOld)
        {
            try
            {
                string RETURN_VALUE = string.Empty;
                string RETURN_DUPLICATEMAPNAME = string.Empty;
                string RETURN_CONTENTID = string.Empty;
                string RETURN_TOPICID = string.Empty;
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
                var allowedExtensionsicon = new[] { ".jpg"};

                string fileName = "";
                int fileSize = 0;
                //int Size = fileSize / 1000;
                string FileType = "";

                string fileNameicon = "";
                int fileSizeicon = 0;
                //int Size = fileSize / 1000;
                string FileTypeicon = "";

                var _thumbnailPath = "";

                //DBEngine obj1 = new DBEngine();
                //var LMSVideoUploadSize = Convert.ToString(obj1.GetDataTable("select [value] from FTS_APP_CONFIG_SETTINGS WHERE [Key]='LMSVideoUploadSize'").Rows[0][0]);

                var LMSVideoUploadSize = "0";

                DataTable dt1 = new DataTable();
                String con1 = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd1 = new SqlCommand();
                SqlConnection sqlcon1 = new SqlConnection(con1);
                sqlcon1.Open();
                sqlcmd1 = new SqlCommand("Proc_SystemsettingResult", sqlcon1);
                sqlcmd1.Parameters.AddWithValue("@VariableName", "LMSVideoUploadSize");
                sqlcmd1.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd1);
                da1.Fill(dt1);
                sqlcon1.Close();


                if (dt1.Rows.Count > 0)
                {
                    LMSVideoUploadSize = Convert.ToString(dt1.Rows[0]["Variable_Value"]);
                }


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

                if (IsValid == 1 && fileuploadicon != null)
                {

                    fileNameicon = Path.GetFileName(fileuploadicon.FileName);
                    fileSizeicon = fileuploadicon.ContentLength;
                    //int Size = fileSize / 1000;
                    FileTypeicon = System.IO.Path.GetExtension(fileNameicon);


                    if (!allowedExtensionsicon.Contains(FileTypeicon.ToLower()))
                    {
                        IsValid = 0;
                        RETURN_VALUE = "Invalid thumbnail file. Only .jpg types of files shall be supported.";
                    }
                    else
                    {
                        if (fileSizeicon > (1024 * 1024))
                        {
                            IsValid = 0;
                            RETURN_VALUE = "Maximum thumbnail file size shall be 1MB.";
                        }
                        else
                        {
                            IsValid = 1;

                        }
                    }
                }

                if (hdnFileDuration == "")
                {
                    hdnFileDuration = "0";
                }

                if (IsValid == 1)
                {
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

                        
                    }

                    fileName = fileName.Replace('#', '_');
                    fileName = fileName.Replace('+', '_');
                    fileName = fileName.Replace('(', '_');
                    fileName = fileName.Replace(')', '_');
                    fileName = fileName.Replace('%', '_');
                    fileName = fileName.Replace('?', '_');
                    fileName = fileName.Replace('&', '_');
                    fileName = fileName.Replace('=', '_');



                    if (!System.IO.Directory.Exists(Server.MapPath("~/Commonfolder/LMS/Thumbnails/")))
                    {
                        // If Folder doesnot exists, CREATE the folder
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Commonfolder/LMS/Thumbnails/"));
                    }
                    else if (System.IO.File.Exists(Server.MapPath("~/Commonfolder/LMS/Thumbnails/" + fileNameicon)))
                    {
                        // If thumbnail file name already exists, RENAME the file by concatinating it by datetime
                        fileNameicon = DateTime.Now.ToString("hhmmss") + fileNameicon;
                    }


                    fileNameicon = fileNameicon.Replace('#', '_');
                    fileNameicon = fileNameicon.Replace('+', '_');
                    fileNameicon = fileNameicon.Replace('(', '_');
                    fileNameicon = fileNameicon.Replace(')', '_');
                    fileNameicon = fileNameicon.Replace('%', '_');
                    fileNameicon = fileNameicon.Replace('?', '_');
                    fileNameicon = fileNameicon.Replace('&', '_');
                    fileNameicon = fileNameicon.Replace('=', '_');


                    // var _thumbnailPath = Path.Combine("~/Commonfolder/LMS/Thumbnails/", Path.GetFileNameWithoutExtension(fileName.Replace(' ','_')) + ".jpg");

                    if (fileNameicon != "")
                    {
                        _thumbnailPath = Path.Combine("~/Commonfolder/LMS/Thumbnails/", Path.GetFileName(fileNameicon.Replace(' ', '_')) );
                    }
                    else if (fileName != "")
                    {
                        _thumbnailPath = Path.Combine("~/Commonfolder/LMS/Thumbnails/", Path.GetFileNameWithoutExtension(fileName.Replace(' ', '_')) + ".jpg");

                    }
                    else
                    {
                        _thumbnailPath = "";
                    }



                    //ProcedureExecute proc = new ProcedureExecute("PRC_LMSCONTENTMASTER");
                    //proc.AddPara("@ACTION", hdnAddEditMode);
                    //proc.AddPara("@CONTENTID", hdnContentID);
                    //proc.AddPara("@CONTENTTITLE", txtContentTitle);
                    //proc.AddPara("@CONTENTDESC", txtContentDesc);
                    //proc.AddPara("@TOPICID", data.TopicId);
                    //proc.AddPara("@PLAYSEQUENCE", numPlaySequence);
                    //proc.AddPara("@STATUS", chkStatus);
                    //proc.AddPara("@ALLOWLIKE", chkAllowLike);
                    //proc.AddPara("@ALLOWCOMMENTS", chkAllowComments);
                    //proc.AddPara("@ALLOWSHARE", chkAllowShare);
                    //proc.AddPara("@USERID", Convert.ToString(HttpContext.Session["userid"]));

                    //proc.AddPara("@CONTENT_FILENAME", fileName);
                    //proc.AddPara("@CONTENT_FILESIZE", fileSize);
                    //proc.AddPara("@CONTENT_FILETYPE", FileType);
                    //proc.AddPara("@CONTENT_FILEDURATION", hdnFileDuration);
                    //proc.AddPara("@CONTENT_FILEPATH", "~/Commonfolder/LMS/ContentUpload/" + fileName);                    
                    //proc.AddPara("@CONTENT_ICONFILEPATH", _thumbnailPath);
                    //proc.AddVarcharPara("@RETURN_VALUE", 500, "", QueryParameterDirection.Output);
                    //proc.AddVarcharPara("@RETURN_DUPLICATEMAPNAME", -1, "", QueryParameterDirection.Output);
                    //proc.AddVarcharPara("@RETURN_CONTENTID", 500, "", QueryParameterDirection.Output);
                    //proc.AddVarcharPara("@RETURN_TOPICID", 500, "", QueryParameterDirection.Output);
                    //proc.AddVarcharPara("@RETURN_ASSIGNUSERIDS", -1, "", QueryParameterDirection.Output);
                    //int k = proc.RunActionQuery();

                    DataTable dt = new DataTable();

                    string CS = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        SqlCommand cmd = new SqlCommand("PRC_LMSCONTENTMASTER", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        

                        cmd.Parameters.AddWithValue("@ACTION", hdnAddEditMode);
                        cmd.Parameters.AddWithValue("@CONTENTID", hdnContentID);
                        cmd.Parameters.AddWithValue("@CONTENTTITLE", txtContentTitle);
                        cmd.Parameters.AddWithValue("@CONTENTDESC", txtContentDesc);
                        cmd.Parameters.AddWithValue("@TOPICID", data.TopicId);
                        cmd.Parameters.AddWithValue("@PLAYSEQUENCE", numPlaySequence);
                        cmd.Parameters.AddWithValue("@STATUS", chkStatus);
                        cmd.Parameters.AddWithValue("@ALLOWLIKE", chkAllowLike);
                        cmd.Parameters.AddWithValue("@ALLOWCOMMENTS", chkAllowComments);
                        cmd.Parameters.AddWithValue("@ALLOWSHARE", chkAllowShare);
                        cmd.Parameters.AddWithValue("@USERID", Convert.ToString(HttpContext.Session["userid"]));

                        cmd.Parameters.AddWithValue("@CONTENT_FILENAME", fileName);
                        cmd.Parameters.AddWithValue("@CONTENT_FILESIZE", fileSize);
                        cmd.Parameters.AddWithValue("@CONTENT_FILETYPE", FileType);
                        cmd.Parameters.AddWithValue("@CONTENT_FILEDURATION", hdnFileDuration);
                        cmd.Parameters.AddWithValue("@CONTENT_FILEPATH", "~/Commonfolder/LMS/ContentUpload/" + fileName);
                        cmd.Parameters.AddWithValue("@CONTENT_ICONFILEPATH", _thumbnailPath);


                        SqlParameter output = new SqlParameter("@RETURN_VALUE", SqlDbType.VarChar,500);
                        output.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(output);


                        SqlParameter output1 = new SqlParameter("@RETURN_DUPLICATEMAPNAME", SqlDbType.VarChar,-1);
                        output1.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(output1);


                        SqlParameter output2 = new SqlParameter("@RETURN_CONTENTID", SqlDbType.VarChar,500);
                        output2.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(output2);


                        SqlParameter output3 = new SqlParameter("@RETURN_TOPICID", SqlDbType.VarChar,500);
                        output3.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(output3);

                        SqlParameter output4 = new SqlParameter("@RETURN_ASSIGNUSERIDS", SqlDbType.VarChar,-1);
                        output4.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(output4);


                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        //con.Close();

                        RETURN_VALUE = Convert.ToString(output.Value);
                        RETURN_CONTENTID = Convert.ToString(output2.Value);
                        RETURN_TOPICID = Convert.ToString(output3.Value);
                        //RETURN_DUPLICATEMAPNAME = Convert.ToString(output1.Value);

                        con.Close();
                        cmd.Dispose();
                    }

                    if (RETURN_VALUE == "Content added succesfully." || RETURN_VALUE == "Content updated succesfully."){
                        if (fileName != null && fileName != "")
                        {
                            string uploadsFolder = Server.MapPath("~/Commonfolder/LMS/ContentUpload/");

                            // Step 1: Save the original file, eg: Sample.mp4
                            //string originalFilePath = Path.Combine(uploadsFolder, Path.GetFileName(fileName));
                            string originalFilePath = Path.Combine(uploadsFolder, Path.GetFileName(fileName));
                            fileupload.SaveAs(originalFilePath);
                            // End Step 1

                            if (fileSize > (200 * 1024 * 1024))  // COMPRESSION will take place if file size is greater than 200MB
                            {
                                // Step 2: Create and Save the Compressed file with name as , eg: Sample.compressed.mp4
                                // Compress the video file
                                var compressedFilePath = CompressVideo(originalFilePath);
                                // End Step 2

                                // Step 3: Delete the orinal file Sample.mp4
                                if (System.IO.File.Exists(Server.MapPath("~/Commonfolder/LMS/ContentUpload/" + fileName)))
                                {
                                    System.IO.File.Delete(Server.MapPath("~/Commonfolder/LMS/ContentUpload/" + fileName));

                                }
                                // End Step 3

                                // Step 4: Save the compressed file to desired file location with desired file name.
                                // Here the comprssed file Sample.compressed.mp4 is re-named to Sample.mp4
                                // Move the compressed file to the desired location 
                                var finalFilePath = Path.Combine(Server.MapPath("~/Commonfolder/LMS/ContentUpload"), fileName);
                                System.IO.File.Move(compressedFilePath, finalFilePath);
                                // End Step 4
                            }

                        }

                        //Thumbnails Image save

                        if (fileNameicon != null && fileNameicon != "")  // If Thumbnail Image is manually uploaded from interface.
                        {
                            // Upload thumbnail
                            var thumbnailPath = Path.Combine(Server.MapPath("~/Commonfolder/LMS/Thumbnails"), Path.GetFileName(fileNameicon.Replace(' ', '_')));
                            fileuploadicon.SaveAs(thumbnailPath);
                        }
                        else if (fileName != null && fileName != "") // If No Thumbnail Image given manually, then system will create the Thumbnail
                        {
                            // Auto generate Thumbnail
                            var videoPath = Server.MapPath("~/Commonfolder/LMS/ContentUpload/" + fileName);
                            var thumbnailPath = Path.Combine(Server.MapPath("~/Commonfolder/LMS/Thumbnails"), Path.GetFileNameWithoutExtension(fileName.Replace(' ', '_')) + ".jpg");
                            var ffMpeg = new NReco.VideoConverter.FFMpegConverter();
                            ffMpeg.GetVideoThumbnail(videoPath, thumbnailPath);
                        }

                        //Thumbnails Image save End

                        // Send Notification
                        if ( (hdnAddEditMode == "ADDCONTENT" && chkStatus == "1") || (hdnAddEditMode == "EDITCONTENT" && chkStatus == "1" && hdnContentStatusOld=="false" ))  // If published
                        {
                           FireNotification(txtContentTitle, Convert.ToString(data.TopicId));
                        }
                        // End of Send Notification
                    }
                }

                TempData["result"] = RETURN_VALUE+"~"+ RETURN_CONTENTID+"~"+ RETURN_TOPICID;

                return Json(TempData["result"], JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        private string CompressVideo(string filePath)
        {
            // Implement video compression logic here
            // This example uses FFMpeg to compress the video
            string compressedFilePath = Path.ChangeExtension(filePath, ".compressed.mp4");
            string ffmpegPath = Server.MapPath("~/bin/ffmpeg.exe");


            // Start Step 1 : Get the Bitrate of the original video

            // int originalBitrate = 2000; // in kbps
            int originalBitrate = 0;
            string output;

            var processSIZ = new System.Diagnostics.Process
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = ffmpegPath,
                    Arguments = $"-i \"{filePath}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true, // FFmpeg outputs info to stderr, not stdout
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            // Capture the output (which contains the bitrate info)
            processSIZ.Start();
            output = processSIZ.StandardError.ReadToEnd(); // Use StandardError to capture the output
            processSIZ.WaitForExit();


            var match = System.Text.RegularExpressions.Regex.Match(output, @"bitrate:\s+(\d+)\s+kb/s");
            if (match.Success)
            {
                originalBitrate = int.Parse(match.Groups[1].Value);
            }

            // End Step 1

            // Start Step 2: Get the final Bitrate of the file after 60% compression has taken place, i.e. calculate the 40% of the original Bitrate
            int desiredBitrate = (int)(originalBitrate * 0.4); 
            // End Step 2

            // Start Step 3: Compress the file upto 60%
            var process = new System.Diagnostics.Process
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = ffmpegPath,
                    // REV SANCHITA
                    //Arguments = $"-i \"{filePath}\" -vcodec libx264 -crf 28 \"{compressedFilePath}\"",
                    //Arguments = $"-i \"{filePath}\" -b:v {desiredBitrate}k -vcodec libx264 \"{compressedFilePath}\"",
                    //Arguments = $"-i \"{filePath}\" -b:v {desiredBitrate}k -vcodec libx264 -preset ultrafast \"{compressedFilePath}\"",
                    //Arguments = $"-i \"{filePath}\" -b:v {desiredBitrate}k -vcodec libx264 -crf 22 -preset ultrafast \"{compressedFilePath}\"",
                    Arguments = $"-i \"{filePath}\" -b:v {desiredBitrate}k -vcodec libx264 -preset ultrafast \"{compressedFilePath}\"",
                    // END OF REV SANCHITA

                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            process.WaitForExit();
            // End Step 3

            return compressedFilePath;
        }

        // Send Notification
        public void FireNotification(string ContentTitle, string TopicId)
        {
            string Mssg = "HI! A new video titled [" + ContentTitle + "] has been assigned to you. Please check your learning dashboard to start watching.";
            //var imgNotification_Icon = Server.MapPath("~/Commonfolder/LMS/Notification_Icon.jpg");
            var imgNotification_Icon = ConfigurationManager.AppSettings["SPath"].ToString() + "Commonfolder/LMS/Notification_Icon.jpg";
            
            //string SalesMan_Nm = "";
            string SalesMan_Phn = "";

            //DataTable dt_SalesMan = odbengine.GetDataTable("select user_loginId,user_name from tbl_master_user  where user_id=" + SalesmanId + "");
            DataTable dtAssignUser = new DataTable();

            ProcedureExecute procA = new ProcedureExecute("PRC_LMSCONTENTMASTER");
            procA.AddPara("@ACTION", "GETCONTENTASSIGNUSER");
            procA.AddPara("@TOPICID", TopicId);
            procA.AddPara("@USERID", Convert.ToString(HttpContext.Session["userid"]));
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
                //        header = "New Content Added",
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
                rootObj.message.data.title = "New Content Added";
                rootObj.message.data.body = message;
                rootObj.message.data.key_1 = "Sample Key";
                rootObj.message.data.key_2 = "Sample Key2";

                rootObj.message.data.UserName = Customer;
                rootObj.message.data.UserID = Requesttype;
                rootObj.message.data.header = "New Topic Added";
                rootObj.message.data.type = "lms_content_assign";
                rootObj.message.data.imgNotification_Icon = imgNotification_Icon;

                rootObj.message.notification = new Notification();
                rootObj.message.notification.title = "New Content Added";
                rootObj.message.notification.body = message;

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

        public ActionResult ShowContentDetails(String ContentId)
        {
            try
            {
                DataTable dt = new DataTable();
              //  LMSContentAddModel ret = new LMSContentAddModel();
                List<LMSContentEditModel> TopicMapList1 = new List<LMSContentEditModel>();

                //ProcedureExecute proc = new ProcedureExecute("PRC_LMSCONTENTMASTER");
                //proc.AddPara("@ACTION", "SHOWCONTENT");
                //proc.AddPara("@CONTENTID", ContentId);
                //proc.AddPara("@SERVERMAPPATH", Server.MapPath("~/Commonfolder/LMS/ContentUpload/"));
                //proc.AddPara("@BRANCHID", Convert.ToString(Session["userbranchHierarchy"]));
                //proc.AddPara("@USERID", Convert.ToString(HttpContext.Session["userid"]));
                //dt = proc.GetTable();

                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_LMSCONTENTMASTER", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "SHOWCONTENT");
                sqlcmd.Parameters.AddWithValue("@CONTENTID", ContentId);
                sqlcmd.Parameters.AddWithValue("@SERVERMAPPATH", Server.MapPath("~/Commonfolder/LMS/ContentUpload/"));
                sqlcmd.Parameters.AddWithValue("@BRANCHID", Convert.ToString(Session["userbranchHierarchy"]));
                sqlcmd.Parameters.AddWithValue("@USERID", Convert.ToString(HttpContext.Session["userid"]));
               
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                sqlcmd.Dispose();

                if (dt != null && dt.Rows.Count > 0)
                {
                    TopicMapList1 = APIHelperMethods.ToModelList<LMSContentEditModel>(dt);
                }

                //TopicMapList1.VideoPath = Url.Content("~/App_Data/Uploads/" + fileName);

               


                return Json(TopicMapList1, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult Play(string fileName)
        {
            var filePath = Path.Combine(Server.MapPath(""), fileName);
            if (!System.IO.File.Exists(filePath))
            {
                return HttpNotFound();
            }

            return File(filePath, "video/mp4");
        }

        [HttpPost]
        public JsonResult DeleteContent(string ContentId)
        {
            string output_msg = string.Empty;

            try
            {
                DataTable dt = new DataTable();

                //ProcedureExecute proc = new ProcedureExecute("PRC_LMSCONTENTMASTER");
                //proc.AddPara("@ACTION", "DELETECONTENTS");
                //proc.AddPara("@CONTENTID", ContentId);
                //proc.AddVarcharPara("@RETURN_VALUE", 500, "", QueryParameterDirection.Output);
                //dt = proc.GetTable();
                //output_msg = Convert.ToString(proc.GetParaValue("@RETURN_VALUE"));


                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_LMSCONTENTMASTER", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "DELETECONTENTS");
                sqlcmd.Parameters.AddWithValue("@CONTENTID", ContentId);

                SqlParameter output = new SqlParameter("@RETURN_VALUE", SqlDbType.VarChar,500);
                output.Direction = ParameterDirection.Output;
                sqlcmd.Parameters.Add(output);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();

                //Int32 ReturnValue = Convert.ToInt32(output.Value);
                output_msg = Convert.ToString(output.Value);

                sqlcmd.Dispose();



                if (output_msg != "-10" && output_msg != null && output_msg != "")
                {
                    string fileName = output_msg;

                    if (System.IO.File.Exists(Server.MapPath("~/Commonfolder/LMS/ContentUpload/" + fileName)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Commonfolder/LMS/ContentUpload/" + fileName));

                    }

                    //REV thumbnail DELETE
                    var thumbnailPath = Path.GetFileNameWithoutExtension(fileName) + ".jpg";
                    if (System.IO.File.Exists(Server.MapPath("~/Commonfolder/LMS/Thumbnails/" + thumbnailPath)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Commonfolder/LMS/Thumbnails/" + thumbnailPath));

                    }
                    //REV thumbnail DELETE END


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

                //ProcedureExecute proc = new ProcedureExecute("PRC_LMSCONTENTMASTER");
                //proc.AddPara("@Action", "GETCONTENTCOUNTDATA");
                //proc.AddPara("@userid", Convert.ToString(HttpContext.Session["userid"]));
                //ds = proc.GetDataSet();


                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_LMSCONTENTMASTER", sqlcon);
                sqlcmd.Parameters.AddWithValue("@Action", "GETCONTENTCOUNTDATA");
                sqlcmd.Parameters.AddWithValue("@userid", Convert.ToString(HttpContext.Session["userid"]));
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(ds);
                sqlcon.Close();
                sqlcmd.Dispose();


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

        public ActionResult PartialQuestionMapGridList(LMSContentModel model)
        {
            try
            {
                EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/LMSContentUpload/Index");
                ViewBag.CanAdd = rights.CanAdd;
                ViewBag.CanView = rights.CanView;
                ViewBag.CanExport = rights.CanExport;
                ViewBag.CanEdit = rights.CanEdit;
                ViewBag.CanDelete = rights.CanDelete;

                EntityLayer.CommonELS.UserRightsForPage rightsQ = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/LMSQuestions/Index");
                ViewBag.CanAddQues = rights.CanAdd;
                ViewBag.CanEditQues = rights.CanEdit;

                //string user_id = Convert.ToString(Session["userid"]);
                List<QuestionMappedGridListModel> qmapmodel = new List<QuestionMappedGridListModel>();

                DataTable dt = new DataTable();
                //ProcedureExecute proc1 = new ProcedureExecute("PRC_LMSCONTENTMASTER");
                //proc1.AddPara("@ACTION", "GET_CONTENTQUESTIONMAP_LISTINGDATA");
                //proc1.AddPara("@CONTENTID", model.Is_ContentId);
                ////proc.AddPara("@USERID", Convert.ToInt32(user_id));
                //dt = proc1.GetTable();

                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_LMSCONTENTMASTER", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "GET_CONTENTQUESTIONMAP_LISTINGDATA");
                sqlcmd.Parameters.AddWithValue("@CONTENTID", model.Is_ContentId);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                sqlcmd.Dispose();

                if (dt != null)
                {
                    qmapmodel = APIHelperMethods.ToModelList<QuestionMappedGridListModel>(dt);
                }
                TempData["QuestionMapGridList"] = qmapmodel;
                TempData.Keep();

                return PartialView("PartialQuestionMapGridList", qmapmodel);

            }
            catch (Exception ex)
            {
                throw ex;

            }

        }





        //public ActionResult PartialQuestionMapGridList(LMSContentModel model)
        //{
        //    try
        //    {
        //        GetQuestionMapGridListDetails(model.Is_ContentId);
        //        return PartialView("PartialQuestionMapGridList", QuestionMapGridListDetails());

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;

        //    }

        //}

        //public void GetQuestionMapGridListDetails(string ContentID)
        //{
        //    string user_id = Convert.ToString(Session["userid"]);

        //    string action = string.Empty;
        //    DataTable formula_dtls = new DataTable();
        //    DataSet dsInst = new DataSet();

        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        ProcedureExecute proc = new ProcedureExecute("PRC_LMSCONTENTMASTER");
        //        proc.AddPara("@ACTION", "GET_CONTENTQUESTIONMAP_LISTINGDATA");
        //        proc.AddPara("@CONTENTID", ContentID);
        //        //proc.AddPara("@USERID", Convert.ToInt32(user_id));
        //        dt = proc.GetTable();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public IEnumerable QuestionMapGridListDetails()
        //{
        //    string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
        //    string Userid = Convert.ToString(Session["userid"]);


        //    LMSMasterDataContext dc = new LMSMasterDataContext(connectionString);
        //    var q = from d in dc.LMS_CONTENTQUESTIONMAP_LISTINGs
        //            where d.USERID == Convert.ToInt32(Userid)
        //            orderby d.SEQ
        //            select d;
        //    return q;


        //}

        public ActionResult GetLMSTopicList()
        {
            try
            {
                List<TopicList> modelTopic = new List<TopicList>();

                DataSet ds = new DataSet();
                //ProcedureExecute proc = new ProcedureExecute("PRC_LMSCONTENTMASTER");
                //proc.AddPara("@ACTION", "GETDROPDOWNBINDDATA");
                //ds = proc.GetDataSet();

                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_LMSCONTENTMASTER", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "GETDROPDOWNBINDDATA");
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(ds);
                sqlcon.Close();
                sqlcmd.Dispose();

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

        public ActionResult GetLMSCategoriesList()
        {
            try
            {
                List<CategoryList> modelCategory = new List<CategoryList>();

                DataSet ds = new DataSet();
                //ProcedureExecute proc = new ProcedureExecute("PRC_LMSCONTENTMASTER");
                //proc.AddPara("@ACTION", "GETCATEGORYDROPDOWNBINDDATA");
                //ds = proc.GetDataSet();

                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_LMSCONTENTMASTER", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "GETCATEGORYDROPDOWNBINDDATA");
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(ds);
                sqlcon.Close();
                sqlcmd.Dispose();

                if (ds != null)
                {
                    // Company
                    List<CategoryList> CategoryList = new List<CategoryList>();
                    CategoryList = APIHelperMethods.ToModelList<CategoryList>(ds.Tables[0]);
                    modelCategory = CategoryList;
                }


                return PartialView("~/Areas/LMS/Views/LMSContentUpload/_LMSCategoryPartial.cshtml", modelCategory);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public JsonResult GetQuestionListForMap(string TopicIds, string CategoryIds, string ContentId)
        {
            List<QuestionListForMap> modelQuestionListForMap = new List<QuestionListForMap>();

            DataTable dt = new DataTable();
            //ProcedureExecute proc = new ProcedureExecute("PRC_LMSCONTENTMASTER");
            //proc.AddPara("@Action", "GETQUESTIONLISTFORMAP");
            //proc.AddPara("@CONTENTID", ContentId);
            //proc.AddPara("@TOPICIDS", TopicIds);
            //proc.AddPara("@CATEGORYIDS", CategoryIds);
            //proc.AddPara("@BRANCHID", Convert.ToString(Session["userbranchHierarchy"]));
            //proc.AddPara("@USERID", Convert.ToString(HttpContext.Session["userid"]));

            //dt = proc.GetTable();

            String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
            SqlCommand sqlcmd = new SqlCommand();
            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();
            sqlcmd = new SqlCommand("PRC_LMSCONTENTMASTER", sqlcon);
            sqlcmd.Parameters.AddWithValue("@Action", "GETQUESTIONLISTFORMAP");
            sqlcmd.Parameters.AddWithValue("@CONTENTID", ContentId);
            sqlcmd.Parameters.AddWithValue("@TOPICIDS", TopicIds);
            sqlcmd.Parameters.AddWithValue("@CATEGORYIDS", CategoryIds);
            sqlcmd.Parameters.AddWithValue("@BRANCHID", Convert.ToString(Session["userbranchHierarchy"]));
            sqlcmd.Parameters.AddWithValue("@USERID", Convert.ToString(HttpContext.Session["userid"]));
            sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dt);
            sqlcon.Close();
            sqlcmd.Dispose();

            if (dt != null)
            {
                modelQuestionListForMap = APIHelperMethods.ToModelList<QuestionListForMap>(dt);
            }

            return Json(modelQuestionListForMap, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveContentQuestionMap(LMSContentAddModel data)
        {
            try
            {
                //ProcedureExecute proc = new ProcedureExecute("PRC_LMSCONTENTMASTER");
                //proc.AddPara("@ACTION", data.Action);
                //proc.AddPara("@CONTENTID", data.ContentID);
                //proc.AddPara("@SELECTEDQUESTIONMAPLIST", data.SelectedQuestionMapList);
                //proc.AddPara("@USERID", Convert.ToString(HttpContext.Session["userid"]));
                //proc.AddVarcharPara("@RETURN_VALUE", 500, "", QueryParameterDirection.Output);
                //proc.AddVarcharPara("@RETURN_DUPLICATEMAPNAME", -1, "", QueryParameterDirection.Output);
                //int k = proc.RunActionQuery();
                //data.RETURN_VALUE = Convert.ToString(proc.GetParaValue("@RETURN_VALUE"));
                //data.RETURN_DUPLICATEMAPNAME = Convert.ToString(proc.GetParaValue("@RETURN_DUPLICATEMAPNAME"));

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_LMSCONTENTMASTER", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", data.Action);
                sqlcmd.Parameters.AddWithValue("@CONTENTID", data.ContentID);
                sqlcmd.Parameters.AddWithValue("@SELECTEDQUESTIONMAPLIST", data.SelectedQuestionMapList);
                sqlcmd.Parameters.AddWithValue("@USERID", Convert.ToString(HttpContext.Session["userid"]));

                SqlParameter output = new SqlParameter("@RETURN_VALUE", SqlDbType.VarChar ,500);
                output.Direction = ParameterDirection.Output;
                sqlcmd.Parameters.Add(output);

                SqlParameter output1 = new SqlParameter("@RETURN_DUPLICATEMAPNAME", SqlDbType.NVarChar, -1);
                output1.Direction = ParameterDirection.Output;
                sqlcmd.Parameters.Add(output1);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();

                //Int32 ReturnValue = Convert.ToInt32(output.Value);
                data.RETURN_VALUE = Convert.ToString(output.Value);
                data.RETURN_DUPLICATEMAPNAME = Convert.ToString(output1.Value);

                sqlcmd.Dispose();


                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
        public JsonResult ShowContentDetailsForEdit(string ContentId)
        {
            List<QuestionListForMap> modelQuestionListForMap = new List<QuestionListForMap>();

            DataTable dt = new DataTable();
            //ProcedureExecute proc = new ProcedureExecute("PRC_LMSCONTENTMASTER");
            //proc.AddPara("@Action", "GETCONTENTDETAILSFOREDIT");
            //proc.AddPara("@CONTENTID", ContentId);
            //proc.AddPara("@BRANCHID", Convert.ToString(Session["userbranchHierarchy"]));
            //proc.AddPara("@USERID", Convert.ToString(HttpContext.Session["userid"]));
            //dt = proc.GetTable();

            String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
            SqlCommand sqlcmd = new SqlCommand();
            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();
            sqlcmd = new SqlCommand("PRC_LMSCONTENTMASTER", sqlcon);
            sqlcmd.Parameters.AddWithValue("@Action", "GETCONTENTDETAILSFOREDIT");
            sqlcmd.Parameters.AddWithValue("@CONTENTID", ContentId);
            sqlcmd.Parameters.AddWithValue("@BRANCHID", Convert.ToString(Session["userbranchHierarchy"]));
            sqlcmd.Parameters.AddWithValue("@USERID", Convert.ToString(HttpContext.Session["userid"]));
            sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dt);
            sqlcon.Close();
            sqlcmd.Dispose();

            if (dt != null)
            {
                modelQuestionListForMap = APIHelperMethods.ToModelList<QuestionListForMap>(dt);
            }

            return Json(modelQuestionListForMap, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult QuestionMapDelete(string ContentID)
        {
            string output_msg = string.Empty;

            try
            {
                DataTable dt = new DataTable();
                //ProcedureExecute proc = new ProcedureExecute("PRC_LMSCONTENTMASTER");
                //proc.AddPara("@ACTION", "QUESTIONMAPDELETE");
                //proc.AddPara("@CONTENTID", ContentID);
                //proc.AddVarcharPara("@RETURN_VALUE", 500, "", QueryParameterDirection.Output);
                //dt = proc.GetTable();
                //output_msg = Convert.ToString(proc.GetParaValue("@RETURN_VALUE"));

                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_LMSCONTENTMASTER", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "QUESTIONMAPDELETE");
                sqlcmd.Parameters.AddWithValue("@CONTENTID", ContentID);

                SqlParameter output = new SqlParameter("@RETURN_VALUE", SqlDbType.VarChar,500);
                output.Direction = ParameterDirection.Output;
                sqlcmd.Parameters.Add(output);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();

                //Int32 ReturnValue = Convert.ToInt32(output.Value);
                output_msg = Convert.ToString(output.Value);

                sqlcmd.Dispose();


            }
            catch (Exception ex)
            {
                output_msg = "Please try again later";
            }

            return Json(output_msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PartialMappedQuesForView(LMSContentModel model)
        {
            try
            {
                EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/LMSContentUpload/Index");
                ViewBag.CanAdd = rights.CanAdd;
                ViewBag.CanView = rights.CanView;
                ViewBag.CanExport = rights.CanExport;
                ViewBag.CanEdit = rights.CanEdit;
                ViewBag.CanDelete = rights.CanDelete;

                EntityLayer.CommonELS.UserRightsForPage rightsQ = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/LMSQuestions/Index");
                ViewBag.CanAddQues = rights.CanAdd;
                ViewBag.CanEditQues = rights.CanEdit;


                string user_id = Convert.ToString(Session["userid"]);
                List<QuestionMappedViewModel> qmapmodel = new List<QuestionMappedViewModel>();

                DataTable dt = new DataTable();
                //ProcedureExecute proc = new ProcedureExecute("PRC_LMSCONTENTMASTER");
                //proc.AddPara("@ACTION", "GETMAPPEDQUESTIONSFORVIEW");
                //proc.AddPara("@CONTENTID", model.Is_ContentId);
                //proc.AddPara("@USERID", Convert.ToInt32(user_id));
                //dt = proc.GetTable();

                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_LMSCONTENTMASTER", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "GETMAPPEDQUESTIONSFORVIEW");
                sqlcmd.Parameters.AddWithValue("@CONTENTID", model.Is_ContentId);
                //sqlcmd.Parameters.AddWithValue("@USERID", Convert.ToInt32(user_id));
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                sqlcmd.Dispose();


                if (dt != null)
                {
                    qmapmodel = APIHelperMethods.ToModelList<QuestionMappedViewModel>(dt);
                }

                return PartialView("PartialMappedQuesForView", qmapmodel);

            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        public ActionResult ExporRegisterList()
        {
            if (TempData["QuestionMapGridList"] != null)
            {
                return GridViewExtension.ExportToXlsx(GetDoctorBatchGridViewSettings(), TempData["QuestionMapGridList"]);
                
            }
            return null; 
        }

        private GridViewSettings GetDoctorBatchGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "gridQuestionMapGridList";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "LMSContentUpload";

            settings.Columns.Add(x =>
            {
                x.FieldName = "SEQ";
                x.Caption = "Sr. No";
                x.VisibleIndex = 1;
                x.ExportWidth = 80;

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CONTENTTITLE";
                x.Caption = "Content Title";
                x.VisibleIndex = 2;
                x.ExportWidth = 300;

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CONTENTDESC";
                x.Caption = "Description";
                x.VisibleIndex = 3;
                x.ExportWidth = 300;

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "QUESTIONSMAP_COUNT";
                x.Caption = "Mapped Questions";
                x.VisibleIndex = 4;
                x.ExportWidth = 350;

            });

            
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;

        }

        [HttpPost]
        public JsonResult UpdatePlaySequence(string ContentIdOld, string PlaySeqOld, string ContentIdNew, string PlaySeqNew)
        {
            string output_msg = string.Empty;

            try
            {
                DataTable dt = new DataTable();
                //ProcedureExecute proc = new ProcedureExecute("PRC_LMSCONTENTMASTER");
                //proc.AddPara("@ACTION", "UPDATEPLAYSEQUENCE");
                //proc.AddPara("@CONTENTIDOLD", ContentIdOld);
                //proc.AddPara("@PLAYSEQOLD", PlaySeqOld);
                //proc.AddPara("@CONTENTIDNEW", ContentIdNew);
                //proc.AddPara("@PLAYSEQNEW", PlaySeqNew);
                //proc.AddVarcharPara("@RETURN_VALUE", 500, "", QueryParameterDirection.Output);
                //dt = proc.GetTable();
                //output_msg = Convert.ToString(proc.GetParaValue("@RETURN_VALUE"));


                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_LMSCONTENTMASTER", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "UPDATEPLAYSEQUENCE");
                sqlcmd.Parameters.AddWithValue("@CONTENTIDOLD", ContentIdOld);
                sqlcmd.Parameters.AddWithValue("@PLAYSEQOLD", PlaySeqOld);
                sqlcmd.Parameters.AddWithValue("@CONTENTIDNEW", ContentIdNew);
                sqlcmd.Parameters.AddWithValue("@PLAYSEQNEW", PlaySeqNew);

                SqlParameter output = new SqlParameter("@RETURN_VALUE", SqlDbType.NVarChar ,500);
                output.Direction = ParameterDirection.Output;
                sqlcmd.Parameters.Add(output);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();

                //Int32 ReturnValue = Convert.ToInt32(output.Value);
                output_msg = Convert.ToString(output.Value);

                sqlcmd.Dispose();



                //output_msg = "1";


            }
            catch (Exception ex)
            {
                output_msg = "Please try again later";
            }

            return Json(output_msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateStatusPublish(string ContentId, string TopicId, string ContentTitle, string chkStatus)
        {
            string output_msg = string.Empty;

            if (chkStatus != null && chkStatus == "Yes")
            {
                chkStatus = "1";
            }
            else
            {
                chkStatus = "0";
            }

            try
            {
                DataTable dt = new DataTable();
                //ProcedureExecute proc = new ProcedureExecute("PRC_LMSCONTENTMASTER");
                //proc.AddPara("@ACTION", "UPDATESTATUSPUBLISH");
                //proc.AddPara("@CONTENTID", ContentId);
                //proc.AddPara("@STATUS", chkStatus);

                //proc.AddVarcharPara("@RETURN_VALUE", 500, "", QueryParameterDirection.Output);
                //dt = proc.GetTable();
                //output_msg = Convert.ToString(proc.GetParaValue("@RETURN_VALUE"));

                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_LMSCONTENTMASTER", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "UPDATESTATUSPUBLISH");
                sqlcmd.Parameters.AddWithValue("@CONTENTID", ContentId);
                sqlcmd.Parameters.AddWithValue("@STATUS", chkStatus);
               
                SqlParameter output = new SqlParameter("@RETURN_VALUE", SqlDbType.NVarChar, 500);
                output.Direction = ParameterDirection.Output;
                sqlcmd.Parameters.Add(output);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();

                //Int32 ReturnValue = Convert.ToInt32(output.Value);
                output_msg = Convert.ToString(output.Value);

                sqlcmd.Dispose();


                if (output_msg == "1" && chkStatus=="0")
                {
                    FireNotification(ContentTitle, TopicId);
                }


            }
            catch (Exception ex)
            {
                output_msg = "Please try again later";
            }

            return Json(output_msg, JsonRequestBehavior.AllowGet);
        }
         
        public JsonResult ReturnTopicIdFromQuestion(Int64 TopicID = 0, Int64 ContentID = 0, Int64 FromSave = 0)
        {
            Boolean Success = false;
            try
            {
                DataTable dt = new DataTable();
                //ProcedureExecute proc = new ProcedureExecute("PRC_LMSCONTENTMASTER");
                //proc.AddPara("@ACTION", "GETRETURNTOPICDETAILS");
                //proc.AddPara("@TOPICID", TopicID);
                //proc.AddPara("@CONTENTID", ContentID);
                //proc.AddVarcharPara("@RETURN_VALUE", 500, "", QueryParameterDirection.Output);
                //dt = proc.GetTable();

                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_LMSCONTENTMASTER", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "GETRETURNTOPICDETAILS");
                sqlcmd.Parameters.AddWithValue("@TOPICID", TopicID);
                sqlcmd.Parameters.AddWithValue("@CONTENTID", ContentID);

                SqlParameter output = new SqlParameter("@RETURN_VALUE", SqlDbType.NVarChar, 500);
                output.Direction = ParameterDirection.Output;
                sqlcmd.Parameters.Add(output);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                sqlcmd.Dispose();

                if (dt.Rows.Count > 0)
                {
                    TempData["TopicID"] = TopicID;
                    TempData["ContentID"] = ContentID;
                    TempData["TopicName"] = dt.Rows[0]["TopicName"];
                    TempData["ContentName"] = dt.Rows[0]["ContentName"];
                    TempData["fromSave"] = FromSave;
                    TempData.Keep();
                    Success = true;
                }
               
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }

            return Json(Success);
        }

        [HttpPost]
        public JsonResult UpdateQuizTime(string ContentId, string QuizTime)
        {
            string output_msg = string.Empty;

            
            try
            {
                DataTable dt = new DataTable();
                //ProcedureExecute proc = new ProcedureExecute("PRC_LMSCONTENTMASTER");
                //proc.AddPara("@ACTION", "UPDATEQUIZTIME");
                //proc.AddPara("@CONTENTID", ContentId);
                //proc.AddPara("@QUIZTIME", QuizTime);

                //proc.AddVarcharPara("@RETURN_VALUE", 500, "", QueryParameterDirection.Output);
                //dt = proc.GetTable();
                //output_msg = Convert.ToString(proc.GetParaValue("@RETURN_VALUE"));

                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_LMSCONTENTMASTER", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "UPDATEQUIZTIME");
                sqlcmd.Parameters.AddWithValue("@CONTENTID", ContentId);
                sqlcmd.Parameters.AddWithValue("@QUIZTIME", QuizTime);

                SqlParameter output = new SqlParameter("@RETURN_VALUE", SqlDbType.NVarChar, 500);
                output.Direction = ParameterDirection.Output;
                sqlcmd.Parameters.Add(output);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();

                output_msg = Convert.ToString(output.Value);


                sqlcmd.Dispose();

            }
            catch (Exception ex)
            {
                output_msg = "Please try again later";
            }

            return Json(output_msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateTopicSequence(string TopicIdOld, string TopicSeqOld, string TopicIdNew, string TopicSeqNew)
        {
            string output_msg = string.Empty;

            try
            {
                DataTable dt = new DataTable();

                //ProcedureExecute proc = new ProcedureExecute("PRC_LMSCONTENTMASTER");
                //proc.AddPara("@ACTION", "UPDATETOPICSEQUENCE");
                //proc.AddPara("@TOPICIDOLD", TopicIdOld);
                //proc.AddPara("@TOPICSEQOLD", TopicSeqOld);
                //proc.AddPara("@TOPICIDNEW", TopicIdNew);
                //proc.AddPara("@TOPICSEQNEW", TopicSeqNew);
                //proc.AddVarcharPara("@RETURN_VALUE", 500, "", QueryParameterDirection.Output);
                //dt = proc.GetTable();
                //output_msg = Convert.ToString(proc.GetParaValue("@RETURN_VALUE"));

                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_LMSCONTENTMASTER", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "UPDATETOPICSEQUENCE");
                sqlcmd.Parameters.AddWithValue("@TOPICIDOLD", TopicIdOld);
                sqlcmd.Parameters.AddWithValue("@TOPICSEQOLD", TopicSeqOld);
                sqlcmd.Parameters.AddWithValue("@TOPICIDNEW", TopicIdNew);
                sqlcmd.Parameters.AddWithValue("@TOPICSEQNEW", TopicSeqNew);

                SqlParameter output = new SqlParameter("@RETURN_VALUE", SqlDbType.NVarChar, 500);
                output.Direction = ParameterDirection.Output;
                sqlcmd.Parameters.Add(output);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();

                output_msg = Convert.ToString(output.Value);


                sqlcmd.Dispose();


                //output_msg = "1";


            }
            catch (Exception ex)
            {
                output_msg = "Please try again later";
            }

            return Json(output_msg, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult SaveQuestion(string name, string id, string description, string Option1, string Option2, string Option3, string Option4, string Point1
            , string Point2, string Point3, string Point4, string chkCorrect1, string chkCorrect2, string chkCorrect3, string chkCorrect4, string TOPIC_ID, string Category_ID
            , string MODE, string CONTENTID
            )
        {
            string output_msg = string.Empty;

            try
            {

                string Userid = Convert.ToString(Session["userid"]);

                if (id == null)
                {
                    id = "0";
                }
                // End of Rev Sanchita

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_LMS_QUESTIONS", sqlcon);

                if (id == "0")
                    sqlcmd.Parameters.AddWithValue("@ACTION", "ADD");
                else
                    sqlcmd.Parameters.AddWithValue("@ACTION", "UPDATE");

                // Rev Sanchita
                sqlcmd.Parameters.AddWithValue("@MODE", MODE);
                sqlcmd.Parameters.AddWithValue("@CONTENTID", CONTENTID);
                // End of Rev Sanchita
                sqlcmd.Parameters.AddWithValue("@QUESTIONNAME", name);
                sqlcmd.Parameters.AddWithValue("@USER_ID", Userid);
                sqlcmd.Parameters.AddWithValue("@QUESTIONDESCRIPTION", description);

                sqlcmd.Parameters.AddWithValue("@OPTION1", Option1);
                sqlcmd.Parameters.AddWithValue("@OPTION2", Option2);
                sqlcmd.Parameters.AddWithValue("@OPTION3", Option3);
                sqlcmd.Parameters.AddWithValue("@OPTION4", Option4);

                sqlcmd.Parameters.AddWithValue("@POINT1", Point1);
                sqlcmd.Parameters.AddWithValue("@POINT2", Point2);
                sqlcmd.Parameters.AddWithValue("@POINT3", Point3);
                sqlcmd.Parameters.AddWithValue("@POINT4", Point4);

                sqlcmd.Parameters.AddWithValue("@CORRECT1", chkCorrect1);
                sqlcmd.Parameters.AddWithValue("@CORRECT2", chkCorrect2);
                sqlcmd.Parameters.AddWithValue("@CORRECT3", chkCorrect3);
                sqlcmd.Parameters.AddWithValue("@CORRECT4", chkCorrect4);

                sqlcmd.Parameters.AddWithValue("@TOPIC_IDS", TOPIC_ID);
                sqlcmd.Parameters.AddWithValue("@CATEGORY_IDS", Category_ID);

                sqlcmd.Parameters.AddWithValue("@ID", id);

                SqlParameter output = new SqlParameter("@ReturnValue", SqlDbType.Int);
                output.Direction = ParameterDirection.Output;
                sqlcmd.Parameters.Add(output);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();

                //Int32 ReturnValue = Convert.ToInt32(output.Value);
                output_msg = Convert.ToString(output.Value);

                sqlcmd.Dispose();
                sqlcmd.Dispose();

                

                //output_msg = "1";


            }
            catch (Exception ex)
            {
                output_msg = "Please try again later";
            }

            return Json(output_msg, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditQuestion(string id)
        {
            LMSContentModel obj = new LMSContentModel();
            obj.QUESTIONS_ID = id;

            DataSet output = new DataSet();
            //output = obj.EditQuestion(id);

            String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
            SqlCommand sqlcmd = new SqlCommand();
            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();
            sqlcmd = new SqlCommand("PRC_LMS_QUESTIONS", sqlcon);
            sqlcmd.Parameters.AddWithValue("@ACTION", "EDIT");
            sqlcmd.Parameters.AddWithValue("@ID", id);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(output);
            sqlcon.Close();
            sqlcmd.Dispose();

            GetCategory dataobj = new GetCategory();
            List<GetCategory> productdata = new List<GetCategory>();

            if (output.Tables[2].Rows.Count > 0)
            {
                if (output.Tables[2] != null && output.Tables[2].Rows.Count > 0)
                {
                    DataTable objData = output.Tables[2];
                    foreach (DataRow row in objData.Rows)
                    {
                        dataobj = new GetCategory();
                        dataobj.CATEGORYID = Convert.ToInt64(row["QUESTIONS_CATEGORYID"]);
                        productdata.Add(dataobj);

                    }
                }

            }
            ViewBag.QUESTIONS_CATEGORYIDS = productdata;

            if (output.Tables[0].Rows.Count > 0)
            {
                return Json(new
                {
                    NAME = Convert.ToString(output.Tables[0].Rows[0]["QUESTIONS_NAME"]),
                    DESCRIPTION = Convert.ToString(output.Tables[0].Rows[0]["QUESTIONS_DESCRIPTN"]),

                    OPTIONS_NUMBER1 = Convert.ToString(output.Tables[0].Rows[0]["OPTIONS_NUMBER1"]),
                    OPTIONS_NUMBER2 = Convert.ToString(output.Tables[0].Rows[0]["OPTIONS_NUMBER2"]),
                    OPTIONS_NUMBER3 = Convert.ToString(output.Tables[0].Rows[0]["OPTIONS_NUMBER3"]),
                    OPTIONS_NUMBER4 = Convert.ToString(output.Tables[0].Rows[0]["OPTIONS_NUMBER4"]),

                    OPTIONS_POINT1 = Convert.ToString(output.Tables[0].Rows[0]["OPTIONS_POINT1"]),
                    OPTIONS_POINT2 = Convert.ToString(output.Tables[0].Rows[0]["OPTIONS_POINT2"]),
                    OPTIONS_POINT3 = Convert.ToString(output.Tables[0].Rows[0]["OPTIONS_POINT3"]),
                    OPTIONS_POINT4 = Convert.ToString(output.Tables[0].Rows[0]["OPTIONS_POINT4"]),

                    OPTIONS_CORRECT1 = Convert.ToString(output.Tables[0].Rows[0]["OPTIONS_CORRECT1"]),
                    OPTIONS_CORRECT2 = Convert.ToString(output.Tables[0].Rows[0]["OPTIONS_CORRECT2"]),
                    OPTIONS_CORRECT3 = Convert.ToString(output.Tables[0].Rows[0]["OPTIONS_CORRECT3"]),
                    OPTIONS_CORRECT4 = Convert.ToString(output.Tables[0].Rows[0]["OPTIONS_CORRECT4"]),


                    CATEGORYIDS = Convert.ToString(output.Tables[0].Rows[0]["CATEGORYIDS"]),
                    TOPICIDS = Convert.ToString(output.Tables[0].Rows[0]["TOPICIDS"]),




                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { name = "" }, JsonRequestBehavior.AllowGet);
            }

        }

    }
}
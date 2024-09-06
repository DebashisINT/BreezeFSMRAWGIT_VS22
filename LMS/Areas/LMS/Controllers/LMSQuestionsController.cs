using DevExpress.Web.Mvc;
using DevExpress.Web;
using LMS.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using UtilityLayer;
using DevExpress.Utils;
using DevExpress.XtraSpreadsheet.Model;
using DevExpress.XtraExport;
using EntityLayer;
using DataAccessLayer;
//using DevExpress.DataAccess.Native.Data;
//using DevExpress.DataAccess.Native.Data;

namespace LMS.Areas.LMS.Controllers
{
    public class LMSQuestionsController : Controller
    {
        LMSQuestionsModel obj = new LMSQuestionsModel();
        // GET: LMS/LMSQuestions
        public ActionResult Index()
        {
            EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/LMSQuestions/Index");
            ViewBag.CanAdd = rights.CanAdd;
            ViewBag.CanView = rights.CanView;
            ViewBag.CanExport = rights.CanExport;
            ViewBag.CanEdit = rights.CanEdit;
            ViewBag.CanDelete = rights.CanDelete;

            obj.QUESTIONS_ID = Convert.ToString("0");
            TempData["QUESTIONS_ID"] = null;
            TempData["TopicID"] = null;
            TempData["fromSave"] = null;
            TempData.Keep();

            return View();
        }

        public ActionResult QuestionAdd()
        {
            EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/LMSQuestions/Index");
            ViewBag.CanAdd = rights.CanAdd;
            ViewBag.CanView = rights.CanView;
            ViewBag.CanExport = rights.CanExport;
            ViewBag.CanEdit = rights.CanEdit;
            ViewBag.CanDelete = rights.CanDelete;

          

            //if (TempData["TopicID"] != null)
            //{
            //    obj.TOPICID = Convert.ToInt64(TempData["TopicID"]);
            //    TempData.Keep();         
     
            //}

            if (TempData["QUESTIONS_ID"] != null)
            {
                obj.QUESTIONS_ID = Convert.ToString(TempData["QUESTIONS_ID"]);
                TempData.Keep();

            }

            if (TempData["IsView"] != null)
            {
                ViewBag.IsView = Convert.ToInt16(TempData["IsView"]);
                TempData["IsView"] = null;
                if(ViewBag.IsView == 0)
                {
                    ViewBag.PageTitle = "Modify Question";
                }
                else if (ViewBag.IsView == 2)
                {
                    ViewBag.PageTitle = "Add Question from Content";
                }
                else
                {
                    ViewBag.PageTitle = "View Question";
                }
                
            }
            else
            {
                ViewBag.IsView = 0;
                ViewBag.PageTitle = "Add Question";
            }
            return View("~/Areas/LMS/Views/LMSQuestions/QuestionAdd.cshtml", obj);
           
        }
        public ActionResult PartialGridList(LMSQuestionsModel model)
        {
            try
            {
                EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/LMSQuestions/Index");
                ViewBag.CanAdd = rights.CanAdd;
                ViewBag.CanView = rights.CanView;
                ViewBag.CanExport = rights.CanExport;
                ViewBag.CanEdit = rights.CanEdit;
                ViewBag.CanDelete = rights.CanDelete;

                string Is_PageLoad = string.Empty;
                DataTable dt = new DataTable();

                if (model.Is_PageLoad != "1")
                    Is_PageLoad = "Ispageload";

                ViewData["ModelData"] = model;
                string Userid = Convert.ToString(Session["userid"]);

                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_LMS_QUESTIONS", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "GETLISTINGDETAILS");
                sqlcmd.Parameters.Add("@USER_ID", Userid);
                sqlcmd.Parameters.Add("@ISPAGELOAD", model.Is_PageLoad);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                return PartialView("PartialQuestionsListing", GetQuestionsDetailsList(Is_PageLoad));

            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public IEnumerable GetQuestionsDetailsList(string Is_PageLoad)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);
            if (Is_PageLoad != "Ispageload")
            {
                LMSMasterDataContext dc = new LMSMasterDataContext(connectionString);
                var q = from d in dc.LMS_QUESTIONSMASTERLISTs
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                LMSMasterDataContext dc = new LMSMasterDataContext(connectionString);
                var q = from d in dc.LMS_QUESTIONSMASTERLISTs
                        where d.SEQ == 0
                        select d;
                return q;
            }
        }
        public JsonResult SaveQuestion(string name, string id, string description, string Option1, string Option2, string Option3, string Option4, string Point1
            , string Point2, string Point3, string Point4, string chkCorrect1, string chkCorrect2, string chkCorrect3, string chkCorrect4,string TOPIC_ID,string Category_ID
            // Rev Sanchita
            , string MODE, string CONTENTID
            // End of Rev Sanchita
            )
        {
            int output = 0;
            string Userid = Convert.ToString(Session["userid"]);
            output = obj.SaveQuestion(name, Userid, id, description, Option1, Option2, Option3, Option4, Point1, Point2, Point3, Point4
            , chkCorrect1, chkCorrect2, chkCorrect3, chkCorrect4, TOPIC_ID, Category_ID, MODE, CONTENTID);
            return Json(output, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SetMapDataByID(Int64 ID = 0, Int16 IsView = 0)
        {
            Boolean Success = false;
            try
            {
                TempData["QUESTIONS_ID"] = ID;
                TempData["IsView"] = IsView;
                TempData.Keep();
                Success = true;
            }
            catch { }
            return Json(Success,JsonRequestBehavior.AllowGet);
        }
        public JsonResult EditQuestion(string id)
        {
            DataSet output = new DataSet();
            output = obj.EditQuestion(id);

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

        public JsonResult Delete(string ID)
        {
            int output = 0;
            output = obj.DeleteQuestion(ID);
            return Json(output, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ExporQuestionList(int type)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToXlsx(GetQuestionsGridViewSettings(), GetQuestionsDetailsList(""));              
                default:
                    break;
            }
            return null;
        }

        private GridViewSettings GetQuestionsGridViewSettings()
        {


            var settings = new GridViewSettings();
            settings.Name = "PartialGridList";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Questions";

            settings.Columns.Add(x =>
            {
                x.FieldName = "QUESTIONS_NAME";
                x.Caption = "Question";
                x.VisibleIndex = 1;
                x.Width = 200;
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "QUESTIONS_DESCRIPTN";
                x.Caption = "Description";
                x.VisibleIndex = 2;
                x.Width = 200;
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "CNTTOPIC"; 
                x.Caption = "Topics";
                x.VisibleIndex = 3;
                x.Width = 200;
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "CNTCATEGORY"; 
                x.Caption = "Categories";
                x.VisibleIndex = 4;
                x.Width = 200;
            });


            settings.Columns.Add(x =>
            {
                x.FieldName = "CREATEUSER";
                x.Caption = "Created By";
                x.VisibleIndex = 5;
                x.Width = 200;
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "CREATEDATE";
                x.Caption = "Created On";
                x.VisibleIndex = 6;
                x.Width = 200;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "MODIFYUSER";
                x.Caption = "Updated By";
                x.VisibleIndex = 7;
                x.Width = 200;
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "MODIFYDATE";
                x.Caption = "Updated On";
                x.VisibleIndex = 8;
                x.Width = 200;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
            });
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }


        public ActionResult GetTopicList( String QUESTIONS_ID)
        {
            BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(string.Empty);
            try
            {
                GetTopic dataobj = new GetTopic();
                List<GetTopic> productdata = new List<GetTopic>();
                List<GetTopic> modelbranch = new List<GetTopic>();               
                DataTable ComponentTable = new DataTable();

                if(QUESTIONS_ID =="" || QUESTIONS_ID ==null)
                {
                    QUESTIONS_ID = "0";
                }
                ComponentTable = obj.GETLOOKUPVALUE("GETTOPIC", QUESTIONS_ID);                
                modelbranch = APIHelperMethods.ToModelList<GetTopic>(ComponentTable);

                DataSet output = new DataSet();
                output = obj.EditQuestion(QUESTIONS_ID);
                if (output.Tables[1].Rows.Count > 0)
                {
                    if (output.Tables[1] != null && output.Tables[1].Rows.Count > 0)
                    {
                        DataTable objData = output.Tables[1];
                        foreach (DataRow row in objData.Rows)
                        {
                            dataobj = new GetTopic();
                            dataobj.TOPICID = Convert.ToInt64(row["QUESTIONS_TOPICID"]);
                            productdata.Add(dataobj);

                        }
                        ViewBag.QUESTIONS_TOPICIDS = productdata;
                    }
                    
                }
                if (TempData["TopicID"] != null)
                {
                    ViewBag.CONTENT_TOPICIDS = Convert.ToInt64(TempData["TopicID"]);
                    ViewBag.CONTENT_FROMSAVE = Convert.ToInt64(TempData["fromSave"]);
                }


                    return PartialView("~/Areas/LMS/Views/LMSQuestions/_TopicLookUpPartial.cshtml", modelbranch);

            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetCategoryList(String QUESTIONS_ID)
        {           
            try
            {
                GetCategory dataobj = new GetCategory();
                List<GetCategory> productdata = new List<GetCategory>();
                List<GetCategory> modelbranch = new List<GetCategory>();
                DataTable ComponentTable = new DataTable();
                if (QUESTIONS_ID == "")
                {
                    QUESTIONS_ID = "0";
                }
                ComponentTable = obj.GETLOOKUPVALUE("GETCATEGORY", QUESTIONS_ID);
                modelbranch = APIHelperMethods.ToModelList<GetCategory>(ComponentTable);

                DataSet output = new DataSet();
                output = obj.EditQuestion(QUESTIONS_ID);
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


                return PartialView("~/Areas/LMS/Views/LMSQuestions/_CategoryLookUpPartial.cshtml", modelbranch);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
        public ActionResult GetCategoryListJson()
        {           
            try
            {
                List<GetCategory> modelbranch = new List<GetCategory>();
                DataTable ComponentTable = new DataTable();
                ComponentTable = obj.GETLOOKUPVALUE("GETCATEGORY","0");
                modelbranch = APIHelperMethods.ToModelList<GetCategory>(ComponentTable);
                return Json(modelbranch, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
        public ActionResult GetTopicListJson()
        {
            try
            {
                List<GetTopic> modelbranch = new List<GetTopic>();
                DataTable ComponentTable = new DataTable();
                ComponentTable = obj.GETLOOKUPVALUE("GETTOPIC","0");
                modelbranch = APIHelperMethods.ToModelList<GetTopic>(ComponentTable);
                return Json(modelbranch, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult PartialCategoryGridList(LMSQuestionsModel model)
        {
            try
            {
               
                GetCategory dataobj = new GetCategory();
                List<GetCategory> productdata = new List<GetCategory>();              
                DataTable dt = new DataTable();             

                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_LMS_QUESTIONS", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "GETCATEGORYLISTINGDETAILS");             
                sqlcmd.Parameters.Add("@ID", model.QUESTIONS_ID);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataTable objData = dt;
                    foreach (DataRow row in objData.Rows)
                    {
                        dataobj = new GetCategory();
                        dataobj.CATEGORYID = Convert.ToInt64(row["CATEGORYID"]);
                        dataobj.CATEGORYNAME = Convert.ToString(row["CATEGORYNAME"]);
                        dataobj.CATEGORYDESCRIPTION = Convert.ToString(row["CATEGORYDESCRIPTION"]);
                        productdata.Add(dataobj);

                    }                    
                }
                return PartialView("PartialCategoryDetaisListing", productdata);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult PartialTopicGridList(LMSQuestionsModel model)
        {
            try
            {

                GetTopic dataobj = new GetTopic();
                List<GetTopic> productdata = new List<GetTopic>();
                DataTable dt = new DataTable();

                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_LMS_QUESTIONS", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "GETTOPICLISTINGDETAILS");
                sqlcmd.Parameters.Add("@ID", model.QUESTIONS_ID);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataTable objData = dt;
                    foreach (DataRow row in objData.Rows)
                    {
                        dataobj = new GetTopic();
                        dataobj.TOPICID = Convert.ToInt64(row["TOPICID"]);
                        dataobj.TOPICNAME = Convert.ToString(row["TOPICNAME"]);
                        dataobj.TOPICBASEDON = Convert.ToString(row["TOPICBASEDON"]);
                        
                        productdata.Add(dataobj);

                    }
                }
                return PartialView("PartialTopicDetailsListing", productdata);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public JsonResult SetMapDataByTopicID(Int64 TopicID = 0, Int64 fromSave=0)
        {
            Boolean Success = false;
            try
            {
                TempData["TopicID"] = TopicID;
                TempData["fromSave"] = fromSave;
                TempData["IsView"] = 2;
                TempData.Keep();
                Success = true;
            }
            catch { }
            return Json(Success, JsonRequestBehavior.AllowGet);
        }
    }
}
using DataAccessLayer;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using LMS.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilityLayer;
using static LMS.Models.LMSReportsModel;


namespace LMS.Areas.LMS.Controllers
{
    public class LMSReportsController : Controller
    {
        // GET: LMS/LMSReports
        LMSReportsModel obj = new LMSReportsModel();
        public ActionResult Index()
        {
            EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/LMSReports/Index");
            ViewBag.CanAdd = rights.CanAdd;
            ViewBag.CanView = rights.CanView;
            ViewBag.CanExport = rights.CanExport;
            ViewBag.CanEdit = rights.CanEdit;
            ViewBag.CanDelete = rights.CanDelete;

            return View();
        }
        public ActionResult GetTopicList()
        {
            BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(string.Empty);
            try
            {
                GetTopic dataobj = new GetTopic();
                List<GetTopic> productdata = new List<GetTopic>();
                List<GetTopic> modelbranch = new List<GetTopic>();
                DataTable ComponentTable = new DataTable();
               
                ComponentTable = obj.GETDROPDOWNVALUE("GETTOPIC");
                modelbranch = APIHelperMethods.ToModelList<GetTopic>(ComponentTable);
                return PartialView("~/Areas/LMS/Views/LMSReports/_TopicLookUpPartial.cshtml", modelbranch);

            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GETUSERLIST()
        {
            BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(string.Empty);
            try
            {
                GetUser dataobj = new GetUser();
                List<GetUser> productdata = new List<GetUser>();
                List<GetUser> modelbranch = new List<GetUser>();
                DataTable ComponentTable = new DataTable();

                ComponentTable = obj.GETDROPDOWNVALUE("GETUSER");
                modelbranch = APIHelperMethods.ToModelList<GetUser>(ComponentTable);
                return PartialView("~/Areas/LMS/Views/LMSReports/_UserLookUpPartial.cshtml", modelbranch);

            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
        public ActionResult GetContentList()
        {
            BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(string.Empty);
            try
            {
                GetContent dataobj = new GetContent();
                List<GetContent> productdata = new List<GetContent>();
                List<GetContent> modelbranch = new List<GetContent>();
                DataTable ComponentTable = new DataTable();
                ComponentTable = obj.GETDROPDOWNVALUE("GetContent");
                modelbranch = APIHelperMethods.ToModelList<GetContent>(ComponentTable);
                return PartialView("~/Areas/LMS/Views/LMSReports/_ContentLookUpPartial.cshtml", modelbranch);

            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public PartialViewResult PartialReportListing(string is_pageload)
        {
            return PartialView(GetReport(is_pageload));
        }


        public IEnumerable GetReport(string is_pageload)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;          
            string Userid = Convert.ToString(Session["userid"]);

            if (is_pageload != "1")
            {
                LMSMasterDataContext dc = new LMSMasterDataContext(connectionString);
                var q = from d in dc.LMS_REPORTSLISTs
                        where Convert.ToString(d.USERID) == Userid                      
                        select d;
                return q;
            }
            else
            {
                LMSMasterDataContext dc = new LMSMasterDataContext(connectionString);
                var q = from d in dc.LMS_REPORTSLISTs
                        where 1 == 0                        
                        select d;
                return q;
            }
        }

        
        public JsonResult CreateLINQTable(string UserIds, string Topic_Id, string Content_Id,  DateTime fromdate, DateTime todate,string _Status)
        {
            string output = "";            
            obj.CreateTable(UserIds, Topic_Id, Content_Id,fromdate, todate, Convert.ToString(Session["userid"]), _Status);
            return Json(output, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ExporSummaryList(int type)
        {


            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(), GetReport(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(), GetReport(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(), GetReport(""));
                case 4:
                    return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(), GetReport(""));
                case 5:
                    return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(), GetReport(""));
                //break;

                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetEmployeeBatchGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "Learning Analytics";
            settings.CallbackRouteValues = new { Action = "PartialReportListing", Controller = "LMSReports" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Learning Analytics";

            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "SEQ";
            //    x.Caption = "SL";
            //    x.VisibleIndex = 1;
            //    x.Width =100;
            //});

            settings.Columns.Add(x =>
            {
                x.FieldName = "user_loginId";
                x.Caption = "Login ID";
                x.VisibleIndex = 2;
                x.Width = 100;               
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "USER_NAME";
                x.Caption = "User Name";
                x.VisibleIndex = 2;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "TOPICNAME";
                x.Caption = "Topic Name";
                x.VisibleIndex = 4;
                x.Width = 200;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CONTENTTITLE";
                x.Caption = "Content Name";
                x.VisibleIndex = 5;
                x.Width = 200;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CONTENTDESC";
                x.Caption = "Content Description";
                x.VisibleIndex = 6;
                x.Width = 200;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ASSIGNEDON";
                x.Caption = "Assigned On";
                x.VisibleIndex = 7;
                x.Width = 200;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy hh:mm:ss";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "COMPLETIONSTATUS";
                x.Caption = "Completion Status";
                x.VisibleIndex = 8;                
                x.Width = 200;

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "TIMESPENT";
                x.Caption = "Time Spent(hh:mm:ss)";
                x.VisibleIndex = 9;                
                x.Width = 200;
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "CompletionDate";
                x.Caption = "Completion Date";
                x.VisibleIndex = 10;
                x.Width = 200;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy hh:mm:ss";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy hh:mm:ss";

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "FirstAccessDateandTime";
                x.Caption = "First Access Date and Time";
                x.VisibleIndex = 11;
                x.Width = 200;

                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy hh:mm:ss";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy hh:mm:ss";
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "LastAccessDateandTime";
                x.Caption = "Last Access Date and Time";
                x.VisibleIndex = 12;
                x.Width = 200;

                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy hh:mm:ss";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy hh:mm:ss";

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CompletionDurationDays";
                x.Caption = "Completion Duration(in Days)";
                x.VisibleIndex = 13;
                x.ColumnType = MVCxGridViewColumnType.TextBox;
                x.Width = 200;
            });




            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

        public JsonResult GetContentCount()
        {
            try
            {
                DataSet ds = new DataSet();
                ProcedureExecute proc = new ProcedureExecute("PRC_LMS_REPORTS");
                proc.AddPara("@Action", "GETREPORTSCOUNTDATA");
                proc.AddPara("@USER_ID", Convert.ToString(HttpContext.Session["userid"]));
                ds = proc.GetDataSet();


                int cnt_TotalPending = 0;
                int cnt_TotalCOMPLETED = 0;
                int cnt_TotalUntouchedContent = 0;

                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    cnt_TotalPending = Convert.ToInt32(item["cnt_TotalPending"]);
                    cnt_TotalCOMPLETED = Convert.ToInt32(item["cnt_TotalCOMPLETED"]);
                    cnt_TotalUntouchedContent = Convert.ToInt32(item["cnt_TotalUntouchedContent"]);

                }

                obj.TotalPending = cnt_TotalPending;
                obj.TotalCOMPLETED = cnt_TotalCOMPLETED;
                obj.TotalUntouched = cnt_TotalUntouchedContent;

            }
            catch
            {
            }
            return Json(obj);
        }

    }
}
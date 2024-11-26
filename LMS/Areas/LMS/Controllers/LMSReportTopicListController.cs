using DevExpress.Web.Mvc;
using DevExpress.Web;
using LMS.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace LMS.Areas.LMS.Controllers
{
    public class LMSReportTopicListController : Controller
    {
        LMSReportTopicListModel obj = new LMSReportTopicListModel();
        // GET: LMS/LMSReportTopicList
        public ActionResult Index()
        {
            EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/LMSReportTopicList/Index");
            ViewBag.CanAdd = rights.CanAdd;
            ViewBag.CanView = rights.CanView;
            ViewBag.CanExport = rights.CanExport;
            ViewBag.CanEdit = rights.CanEdit;
            ViewBag.CanDelete = rights.CanDelete;
            return View();
        }

        public PartialViewResult PartialReportTopicList(LMSReportTopicListModel obj)
        {
            string Is_PageLoad = string.Empty;
            if (obj.is_pageload != "1")
            {
                Is_PageLoad = "0";
            }

            string Userid = Convert.ToString(Session["userid"]);
            DataTable dt = new DataTable();
            String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
            SqlCommand sqlcmd = new SqlCommand();
            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();
            sqlcmd = new SqlCommand("PRC_LMS_REPORTS_TOPICLIST", sqlcon);            
            sqlcmd.Parameters.Add("@USER_ID", Userid);
            sqlcmd.Parameters.Add("@ISPAGELOAD", obj.is_pageload);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dt);
            sqlcon.Close();

            return PartialView(GetReport(Is_PageLoad));
        }


        public IEnumerable GetReport(string is_pageload)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);

            if (is_pageload != "0")
            {
                LMSMasterDataContext dc = new LMSMasterDataContext(connectionString);
                var q = from d in dc.LMS_REPORTTOPICLISTs
                        where Convert.ToString(d.USERID) == Userid
                        orderby d.SEQ
                        select d;
                return q;
            }
            else
            {
                LMSMasterDataContext dc = new LMSMasterDataContext(connectionString);
                var q = from d in dc.LMS_REPORTTOPICLISTs
                        where d.SEQ == 0
                        select d;
                return q;
            }
        }


        //public JsonResult CreateLINQTable(string is_pageload)
        //{
        //    string output = "";
        //    obj.CreateTable(Convert.ToString(Session["userid"]), is_pageload);
        //    return Json(output, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult ExporSummaryList(int type)
        {


            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(), GetReport("1"));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(), GetReport("1"));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(), GetReport("1"));
                case 4:
                    return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(), GetReport("1"));
                case 5:
                    return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(), GetReport("1"));
                //break;

                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetEmployeeBatchGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "Topic List Master";
            settings.CallbackRouteValues = new { Action = "PartialReportTopicList", Controller = "LMSReportTopicList" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Topic List Master";

            settings.Columns.Add(x =>
            {
                x.FieldName = "SEQ";
                x.Caption = "SL";
                x.VisibleIndex = 1;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "TOPICNAME";
                x.Caption = "Topic name";
                x.VisibleIndex = 1;
                x.ColumnType = MVCxGridViewColumnType.TextBox;
                x.Width = 100;

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "TOPICSTATUS";
                x.Caption = "Status(Published/ Unpublished)";
                x.VisibleIndex = 3;
                x.ColumnType = MVCxGridViewColumnType.TextBox;
                x.Width = 100;
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "CONTENTTITLE";
                x.Caption = "Content name";
                x.VisibleIndex = 4;
                x.ColumnType = MVCxGridViewColumnType.TextBox;
                x.Width = 200;

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CONTENTSTATUS";
                x.Caption = "Status(Published/ Unpublished)";
                x.VisibleIndex = 5;
                x.ColumnType = MVCxGridViewColumnType.TextBox;
                x.Width = 200;
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "QuestionMapped";
                x.Caption = "Question Mapped(Yes/No)";
                x.VisibleIndex = 6;
                x.ColumnType = MVCxGridViewColumnType.TextBox;
                x.Width = 200;

            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "QUESTIONSCount";
                x.Caption = "Question Count";
                x.VisibleIndex = 7;

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
    }
}
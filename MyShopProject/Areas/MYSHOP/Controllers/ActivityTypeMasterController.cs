using BusinessLogicLayer.SalesmanTrack;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using MyShop.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class ActivityTypeMasterController : Controller
    {
        ActivityTypeMasterBL obj = new ActivityTypeMasterBL();

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Rendermainpage()
        {
            return PartialView();
        }

        public PartialViewResult Rendergrid(string ispageload)
        {
            return PartialView(GetReport(ispageload));
        }

        [HttpPost]
        public ActionResult GenerateTable(ActivityMasterModel model)
        {
            try
            {
                string Is_PageLoad = string.Empty;
                DataTable dt = new DataTable();
                if (model.Fromdate == null)
                {
                    model.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                }

                if (model.Todate == null)
                {
                    model.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                }

                if (model.is_pageload != "1") Is_PageLoad = "Ispageload";

                ViewData["ModelData"] = model;

                string datfrmat = model.Fromdate.Split('-')[2] + '-' + model.Fromdate.Split('-')[1] + '-' + model.Fromdate.Split('-')[0];
                string dattoat = model.Todate.Split('-')[2] + '-' + model.Todate.Split('-')[1] + '-' + model.Todate.Split('-')[0];
                string Userid = Convert.ToString(Session["userid"]);

                double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;
                if (days <= 30)
                {
                    //dt = obj.GetReportMeetingDetails(datfrmat, dattoat, Userid, state, empcode, Desgid);
                }
                return Json(Userid, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public IEnumerable GetReport(string ispageload)
        {
            string connectionString = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"]);
            string Userid = Convert.ToString(Session["userid"]);

            if (ispageload != "1")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTS_Activities
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTS_Activities
                        where d.Id == 0
                        select d;
                return q;
            }
        }

        public ActionResult ExportMeetingDetailsReport(int type, string IsPageload)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetGridViewSettings(), GetReport(IsPageload));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetGridViewSettings(), GetReport(IsPageload));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetGridViewSettings(), GetReport(IsPageload));
                case 4:
                    return GridViewExtension.ExportToCsv(GetGridViewSettings(), GetReport(IsPageload));
                case 5:
                    return GridViewExtension.ExportToRtf(GetGridViewSettings(), GetReport(IsPageload));
                //break;

                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "ActivityMaster";
            settings.CallbackRouteValues = new { Controller = "ActivityMaster", Action = "GenerateTable" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "ActivityMaster";

            settings.Columns.Add(column =>
            {
                column.Caption = "Activity";
                column.FieldName = "ActivityName";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = System.Web.UI.WebControls.Unit.Percentage(80);
            });

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

        [HttpPost]
        public ActionResult GetActivity(string ActivityID)
        {
            String Activity = "";
            DataTable dt = obj.ActivityType(ActivityID, "", "EDIT", "");
            if (dt != null && dt.Rows.Count > 0)
            {
                Activity = dt.Rows[0]["ActivityName"].ToString();
            }
            return Json(Activity, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteActivity(string ActivityID)
        {
            String Activity = "";
            DataTable dt = obj.ActivityType(ActivityID, "", "DELETE", "");
            if (dt != null && dt.Rows.Count > 0)
            {
                Activity = dt.Rows[0]["MSG"].ToString();
            }
            return Json(Activity, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddActivity(string ActivityID, String ActivityName)
        {
            String Activity = "";
            String Actions = "";
            if (ActivityID == "")
            {
                Actions = "INSERT";
            }
            else
            {
                Actions = "UPDATE";
            }

            DataTable dt = obj.ActivityType(ActivityID, ActivityName, Actions, "");
            if (dt != null && dt.Rows.Count > 0)
            {
                Activity = dt.Rows[0]["msg"].ToString();
            }
            return Json(Activity, JsonRequestBehavior.AllowGet);
        }
    }
}
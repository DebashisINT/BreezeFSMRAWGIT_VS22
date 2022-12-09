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
    public class PriorityMasterController : Controller
    {
        PriorityMasterBL obj = new PriorityMasterBL();
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
        public ActionResult GenerateTable(PriorityMasterModel model)
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
                var q = from d in dc.FTS_Priorities
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTS_Priorities
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
            settings.Name = "PriorityMaster";
            settings.CallbackRouteValues = new { Controller = "PriorityMaster", Action = "GenerateTable" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "PriorityMaster";

            settings.Columns.Add(column =>
            {
                column.Caption = "Priority";
                column.FieldName = "PriorityName";
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
        public ActionResult GetPriority(string PriorityID)
        {
            String Priority = "";
            DataTable dt = obj.GetPriority(PriorityID,"", "EDIT");
            if (dt!=null && dt.Rows.Count > 0)
            {
                Priority = dt.Rows[0]["PriorityName"].ToString();
            }
            return Json(Priority, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeletePriority(string PriorityID)
        {
            String Priority = "";
            DataTable dt = obj.GetPriority(PriorityID, "", "DELETE");
            if (dt != null && dt.Rows.Count > 0)
            {
                Priority = dt.Rows[0]["MSG"].ToString();
            }
            return Json(Priority, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddPriority(string PriorityID, String PriorityName)
        {
            String Priority = "";
            String Actions = "";
            if (PriorityID=="")
            {
                Actions = "INSERT";
            }
            else
            {
                Actions = "UPDATE";
            }

            DataTable dt = obj.GetPriority(PriorityID, PriorityName, Actions);
            if (dt != null && dt.Rows.Count > 0)
            {
                Priority = dt.Rows[0]["msg"].ToString();
            }
            return Json(Priority, JsonRequestBehavior.AllowGet);
        }
    }
}
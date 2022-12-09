using BusinessLogicLayer.SalesTrackerReports;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using Models;
using MyShop.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class AttendanceRegisterController : Controller
    {
        AttendanceRegisterBL objAttendanceRegisterBL = new AttendanceRegisterBL();

        public ActionResult Report(string Type)
        {
            try
            {
                string userid = Session["userid"].ToString();

                AttendanceRegister omodel = new AttendanceRegister();
                omodel.FromDate = DateTime.Now.ToString("dd-MM-yyyy");
                omodel.ToDate = DateTime.Now.ToString("dd-MM-yyyy");
                omodel.UserID = userid;
                return View(omodel);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult PartialSummaryList(AttendanceRegister model)
        {
            try
            {
                DataTable dt = new DataTable();
                string Is_PageLoad = string.Empty;

                if (model.Is_PageLoad == "0") Is_PageLoad = "Ispageload";

                //if (model.FromDate == null) model.FromDate = DateTime.Now.ToString("yyyy-MM-dd");
                //if (model.ToDate == null) model.ToDate = DateTime.Now.ToString("yyyy-MM-dd");
                if (model.FromDate == null)
                {
                    model.FromDate = DateTime.Now.ToString("dd-MM-yyyy");
                }

                if (model.ToDate == null)
                {
                    model.ToDate = DateTime.Now.ToString("dd-MM-yyyy");
                }

                string datfrmat = model.FromDate.Split('-')[2] + '-' + model.FromDate.Split('-')[1] + '-' + model.FromDate.Split('-')[0];
                string dattoat = model.ToDate.Split('-')[2] + '-' + model.ToDate.Split('-')[1] + '-' + model.ToDate.Split('-')[0];

                string FromDate =model.FromDate;
                string ToDate =model.ToDate;
                string UserId = Convert.ToString(Session["userid"]);

                ViewData["ModelData"] = model;
                //double days = (DateTime.ParseExact(dattoat, "dd-MM-yyyy", CultureInfo.CurrentCulture) - DateTime.ParseExact(datfrmat, "dd-MM-yyyy", CultureInfo.CurrentCulture)).TotalDays;
                double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;
                if (days <= 35)
                {
                    dt = objAttendanceRegisterBL.GenerateSummaryData(FromDate, ToDate, UserId);
                }
                return PartialView("PartialSummaryList", GetSummary(Is_PageLoad));
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public IEnumerable GetSummary(string Is_PageLoad)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string userID = Convert.ToString(Session["userid"]);

            if (Is_PageLoad != "Ispageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSATTENDANCEREGISTER_REPORTs
                        where d.USERID == Convert.ToInt32(userID) && d.ACTION == "Summary"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSATTENDANCEREGISTER_REPORTs
                        where d.USERID == Convert.ToInt32(userID) && d.ACTION == "Summary"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
        }

        public ActionResult ExporSummaryList(int type)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetSummaryGridViewSettings(), GetSummary(""));
                case 2:
                    return GridViewExtension.ExportToXlsx(GetSummaryGridViewSettings(), GetSummary(""));
                case 3:
                    return GridViewExtension.ExportToXls(GetSummaryGridViewSettings(), GetSummary(""));
                case 4:
                    return GridViewExtension.ExportToRtf(GetSummaryGridViewSettings(), GetSummary(""));
                case 5:
                    return GridViewExtension.ExportToCsv(GetSummaryGridViewSettings(), GetSummary(""));
                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetSummaryGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "Attendance Register";
            settings.CallbackRouteValues = new { Controller = "AttendanceRegister", Action = "PartialSummaryList" };
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Attendance Register";

            settings.Columns.Add(x =>
            {
                x.FieldName = "EMPNAME";
                x.Caption = "Member Name";
                x.Width = System.Web.UI.WebControls.Unit.Percentage(40);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "AT_WORK";
                x.Caption = "Attendance";
                x.Width = System.Web.UI.WebControls.Unit.Percentage(20);
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ON_LEAVE";
                x.Caption = "On Leave";
                x.Width = System.Web.UI.WebControls.Unit.Percentage(20);
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "LATE_CNT";
                x.Caption = "Late Count";
                x.Width = System.Web.UI.WebControls.Unit.Percentage(20);
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

        #region Details Grid

        public ActionResult PartialDetailsList(string Fromdate, string Todate, string is_pageload, string EmployeeCode)
        {
            try
            {
                DataTable dt = new DataTable();
                string Is_PageLoad = string.Empty;

                AttendanceRegister model = new AttendanceRegister();
                model.FromDate = Fromdate;
                model.ToDate = Todate;
                model.UserID = is_pageload;
                model.EmployeeCode = EmployeeCode;

                if (model.Is_PageLoad == "0") Is_PageLoad = "Ispageload";
                if (model.FromDate == null) model.FromDate = DateTime.Now.ToString("yyyy-MM-dd");
                if (model.ToDate == null) model.ToDate = DateTime.Now.ToString("yyyy-MM-dd");

                string FromDate = model.FromDate;
                string ToDate = model.ToDate;
                string UserId = Convert.ToString(Session["userid"]);

                ViewData["ModelData"] = model;
                dt = objAttendanceRegisterBL.GenerateDetailsData(FromDate, ToDate, UserId);

                return PartialView("PartialDetailsList", GetDetails(Is_PageLoad, EmployeeCode));
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
        public IEnumerable GetDetails(string Is_PageLoad, string EmployeeCode)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string userID = Convert.ToString(Session["userid"]);

            ReportsDataContext dc = new ReportsDataContext(connectionString);
            var q = from d in dc.FTSATTENDANCEREGISTER_REPORTs
                    where d.USERID == Convert.ToInt32(userID) && d.ACTION == "Detail" && d.EMPCODE == EmployeeCode
                    orderby d.SEQ ascending
                    select d;
            return q;
        }

        #endregion
    }
}
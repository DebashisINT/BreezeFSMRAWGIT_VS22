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
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class SalesOrderAnalysisController : Controller
    {
        SalesOrderAnalysisBL objSalesOrderAnalysis = new SalesOrderAnalysisBL();
        // GET: MYSHOP/SalesOrderAnalysis
        public ActionResult Report()
        {
            try
            {
                string userid = Session["userid"].ToString();

                SalesOrderAnalysis omodel = new SalesOrderAnalysis();
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

        public ActionResult PartialSaleAnalysisOuterList(SalesOrderAnalysis model)
        {
            try
            {
                DataTable dt = new DataTable();
                string Is_PageLoad = string.Empty;

                if (model.Is_PageLoad == "0") Is_PageLoad = "Ispageload";

                if (model.FromDate == null) model.FromDate = DateTime.Now.ToString("yyyy-MM-dd");
                if (model.ToDate == null) model.ToDate = DateTime.Now.ToString("yyyy-MM-dd");

                string FromDate = model.FromDate;
                string ToDate = model.ToDate;
                string UserId = Convert.ToString(Session["userid"]);

                //ViewData["ModelData"] = model;

                dt = objSalesOrderAnalysis.GenerateAnalysisSummaryData(FromDate, ToDate, UserId);

                return PartialView("_SalesOrderAnalysisOuterPartialGrid", GetSaleSummaryAnalysis(Is_PageLoad));
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
        public IEnumerable GetSaleSummaryAnalysis(string Is_PageLoad)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string userID = Convert.ToString(Session["userid"]);

            if (Is_PageLoad != "Ispageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSSALESORDERANALYSIS_REPORTs
                        where d.USERID == Convert.ToInt32(userID) && d.ACTION == "Summary"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSSALESORDERANALYSIS_REPORTs
                        where d.USERID == Convert.ToInt32(userID) && d.ACTION == "Summary" && d.EMPCODE=="0"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
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

                //ViewData["ModelData"] = model;
                dt = objSalesOrderAnalysis.GenerateAnalysisDetailsData(FromDate, ToDate, UserId);

                return PartialView("_SalesOrderAnalysisPartialInnerGrid", GetDetails(Is_PageLoad, EmployeeCode));
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
            var q = from d in dc.FTSSALESORDERANALYSIS_REPORTs
                    where d.USERID == Convert.ToInt32(userID) && d.ACTION == "Detail" && d.EMPCODE == EmployeeCode
                    orderby d.SEQ ascending
                    select d;
            return q;
        }

        #endregion

        public ActionResult ExportSummaryList(int type)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetSummaryGridViewSettings(), GetSaleSummaryAnalysis(""));
                case 2:
                    return GridViewExtension.ExportToXlsx(GetSummaryGridViewSettings(), GetSaleSummaryAnalysis(""));
                case 3:
                    return GridViewExtension.ExportToXls(GetSummaryGridViewSettings(), GetSaleSummaryAnalysis(""));
                case 4:
                    return GridViewExtension.ExportToRtf(GetSummaryGridViewSettings(), GetSaleSummaryAnalysis(""));
                case 5:
                    return GridViewExtension.ExportToCsv(GetSummaryGridViewSettings(), GetSaleSummaryAnalysis(""));
                default:
                    break;
            }

            return null;
        }
        private GridViewSettings GetSummaryGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "Sales Order Analysis";
            settings.CallbackRouteValues = new { Controller = "SalesOrderAnalysis", Action = "PartialSaleAnalysisOuterList" };
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Sales Order Analysis";

            settings.Columns.Add(column =>
            {
                column.Caption = "Employee";
                column.FieldName = "EMPNAME";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Order Value";
                column.FieldName = "ORDVALUE";
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
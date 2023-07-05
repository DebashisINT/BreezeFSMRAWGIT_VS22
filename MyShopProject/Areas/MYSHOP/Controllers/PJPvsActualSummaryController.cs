/***************************************************************************************************************************
 * Rev 1.0      Sanchita    V2.0.41     30-06-2023      The Export Format is not Proper in PJP Vs Actual Details Report. refer: 26436
 ****************************************************************************************************************************/
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
    public class PJPvsActualSummaryController : Controller
    {
        PJPvsActualSummaryBL obj = new PJPvsActualSummaryBL();
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
        public ActionResult GenerateTable(CustomerDetailsModel model)
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

                string state = "";
                int i = 1;
                if (model.StateId != null && model.StateId.Count > 0)
                {
                    foreach (string item in model.StateId)
                    {
                        if (i > 1)
                            state = state + "," + item;
                        else
                            state = item;
                        i++;
                    }
                }

                string empcode = "";
                int k = 1;
                if (model.empcode != null && model.empcode.Count > 0)
                {
                    foreach (string item in model.empcode)
                    {
                        if (k > 1)
                            empcode = empcode + "," + item;
                        else
                            empcode = item;
                        k++;
                    }
                }
                string Desgid = "";
                k = 1;
                if (model.desgid != null && model.desgid.Count > 0)
                {
                    foreach (string item in model.desgid)
                    {
                        if (k > 1)
                            Desgid = Desgid + "," + item;
                        else
                            Desgid = item;
                        k++;
                    }
                }
               
                double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;
                if (days <= 30)
                {
                    dt = obj.GetReportPJPActualSummary(datfrmat, dattoat, Userid, state, empcode, Desgid);
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
                var q = from d in dc.FTS_PJPvsActualSummaryReports
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.Employee ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTS_PJPvsActualSummaryReports
                        where d.USERID == 0
                        select d;
                return q;
            }
        }

        public ActionResult ExportPJPActualSummaryReport(int type, string IsPageload)
        {
            switch (type)
            {
                // Rev 1.0
                //case 1:
                //    return GridViewExtension.ExportToXlsx(GetGridViewSettings(), GetReport(IsPageload));
                ////break;
                //case 2:
                //    return GridViewExtension.ExportToXls(GetGridViewSettings(), GetReport(IsPageload));
                ////break;
                //case 3:
                //    return GridViewExtension.ExportToPdf(GetGridViewSettings(), GetReport(IsPageload));
                //case 4:
                //    return GridViewExtension.ExportToCsv(GetGridViewSettings(), GetReport(IsPageload));
                //case 5:
                //    return GridViewExtension.ExportToRtf(GetGridViewSettings(), GetReport(IsPageload));
                ////break;

                //default:
                //    break;

                case 1:
                    return GridViewExtension.ExportToPdf(GetGridViewSettings(), GetReport(IsPageload));
                case 2:
                    return GridViewExtension.ExportToXlsx(GetGridViewSettings(), GetReport(IsPageload));
                case 3:
                    return GridViewExtension.ExportToXls(GetGridViewSettings(), GetReport(IsPageload));
                case 4:
                    return GridViewExtension.ExportToRtf(GetGridViewSettings(), GetReport(IsPageload));
                case 5:
                    return GridViewExtension.ExportToCsv(GetGridViewSettings(), GetReport(IsPageload));
                default:
                    break;
                // End of Rev 1.0
            }

            return null;
        }

        private GridViewSettings GetGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "PJPvsActualSummaryReport";
            settings.CallbackRouteValues = new { Controller = "PJPvsActualSummary", Action = "GenerateTable" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "PJPvsActualSummaryReport";

            settings.Columns.Add(column =>
            {
                column.Caption = "Date";
                column.FieldName = "Date";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ColumnType = MVCxGridViewColumnType.DateEdit;
                column.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (column.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
                column.Width = 100;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Employee Name";
                column.FieldName = "Employee";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 180;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Designation";
                column.Caption = "Designation";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 180;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Phone";
                column.FieldName = "Phone";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 100;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Supervisor";
                column.Caption = "Supervisor";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 150;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "State";
                column.FieldName = "State";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 180;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "PJP Count";
                column.FieldName = "PJPCount";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 100;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "ActualVisit";
                column.Caption = "Actual Visit";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 100;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Order Value";
                column.FieldName = "OrderValue";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.PropertiesEdit.DisplayFormatString = "0.00";
                column.Width = 100;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Productivity";
                column.Caption = "Productivity";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.PropertiesEdit.DisplayFormatString = "0.00";
                column.Width = 100;
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
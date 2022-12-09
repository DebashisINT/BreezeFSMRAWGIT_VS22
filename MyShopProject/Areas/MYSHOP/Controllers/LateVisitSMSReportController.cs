using BusinessLogicLayer;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using MyShop.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class LateVisitSMSReportController : Controller
    {
        //
        // GET: /MYSHOP/LateVisitSMSReport/
        public ActionResult DashBoard()
        {
            return View();
        }

        public ActionResult _PartialDashBoard()
        {
            return PartialView();
        }

        public ActionResult _PartialGrid(string ispageload)
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult GenerateTable(DateTime fromdate, DateTime todate)
        {
            string output = "";
            LateSMS obj = new LateSMS();
            obj.GenerateTable(fromdate , todate , Convert.ToInt32(Session["userid"]));
            return Json(output);
        }

        public IEnumerable GetReport(string ispageload)
        {
            string connectionString = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"]);
            string Userid = Convert.ToString(Session["userid"]);

            if (ispageload != "1")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.LATESMS_REPORTs
                        where d.USER_ID == Userid
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.LATESMS_REPORTs
                        where 1 == 0
                        select d;
                return q;
            }
        }

        public ActionResult ExportReport(int type, string IsPageload)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(), GetReport(IsPageload));
                //break;
                case 2:
                    return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(), GetReport(IsPageload));
                //break;
                case 3:
                    return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(), GetReport(IsPageload));
                case 4:
                    return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(), GetReport(IsPageload));
                case 5:
                    return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(), GetReport(IsPageload));
                //break;

                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetEmployeeBatchGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "CustomerRateingReport";
            settings.CallbackRouteValues = new { Controller = "ShopOrderRating", Action = "GenerateTable" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "CustomerRateingReport";

            settings.Columns.Add(column =>
            {
                column.Caption = "Date";
                column.FieldName = "Date";
                column.ColumnType = MVCxGridViewColumnType.TextBox;

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Employee Name";
                column.FieldName = "Emp_Name";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Login_ID";
                column.Caption = "Login ID";
                column.ColumnType = MVCxGridViewColumnType.TextBox;

            });
            settings.Columns.Add(column =>
            {
                column.Caption = "SMS time";
                column.FieldName = "SMS_time";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "SMS_time_type";
                column.Caption = "Type";
                column.ColumnType = MVCxGridViewColumnType.TextBox;

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
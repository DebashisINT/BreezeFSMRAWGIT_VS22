/***************************************************************************************************************************************
 * 1.0      V2.0.47     14-05-2024      Sanchita        Column Chooser facility is required in the Activity Details report. Mantis: 27422
 * *************************************************************************************************************************************/
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
    public class ActivityReportController : Controller
    {
        ActivityReportBL obj = new ActivityReportBL();
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
        public ActionResult GenerateTable(ActivityReportModel model)
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
                    dt = obj.GetReportActivityDetails(datfrmat, dattoat, Userid, state, empcode, Desgid);
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
            // Rev 1.0
            DataTable dtColmn = obj.GetPageRetention(Session["userid"].ToString(), "ACTIVITY DETAILS");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;
            }
            // End of Rev 1.0

            string connectionString = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"]);
            string Userid = Convert.ToString(Session["userid"]);

            if (ispageload != "1")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTS_ActivityDetailsReports
                        where d.USERID == Convert.ToInt32(Userid)
                        //orderby d.EMPLOYEE ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTS_ActivityDetailsReports
                        where d.USERID == 0
                        select d;
                return q;
            }
        }

        public ActionResult ExportMeetingDetailsReport(int type, string IsPageload)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToXlsx(GetGridViewSettings(), GetReport(IsPageload));
                //break;
                case 2:
                    return GridViewExtension.ExportToXls(GetGridViewSettings(), GetReport(IsPageload));
                //break;
                case 3:
                    return GridViewExtension.ExportToPdf(GetGridViewSettings(), GetReport(IsPageload));
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
            settings.Name = "ActivityDetailsReport";
            settings.CallbackRouteValues = new { Controller = "ActivityReport", Action = "GenerateTable" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "ActivityDetailsReport";

            settings.Columns.Add(column =>
            {
                column.Caption = "Party";
                column.FieldName = "Shop_Name";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 180;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Activity Date";
                column.FieldName = "Activity_Date";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 100;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Activity Time";
                column.FieldName = "Activity_Time";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 100;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Contact Name";
                column.FieldName = "ContactName";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 150;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Activity";
                column.FieldName = "ActivityName";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 150;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "ActivityTypeName";
                column.Caption = "Type";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 150;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Product_Name";
                column.Caption = "Product";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 200;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Subject";
                column.FieldName = "ActivitySubject";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 180;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "ActivityDetails";
                column.Caption = "Details";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 250;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Duration";
                column.Caption = "Duration";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 100;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Priority";
                column.FieldName = "PriorityName";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 150;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Due Date";
                column.FieldName = "Due_Date";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 100;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Due Time";
                column.FieldName = "Due_Time";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 100;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "CREATE_BY";
                column.Caption = "Created By";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 150;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Created On";
                column.FieldName = "CREATED_DATE";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ColumnType = MVCxGridViewColumnType.DateEdit;
                column.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy hh:mm:ss";
                (column.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy hh:mm:ss";
                column.Width = 150;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "MODIFIED_BY";
                column.Caption = "Modified By";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 120;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Modified On";
                column.FieldName = "MODIFIED_DATE";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ColumnType = MVCxGridViewColumnType.DateEdit;
                column.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy hh:mm:ss";
                (column.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy hh:mm:ss";
                column.Width = 150;
            });

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

        // Rev 1.0
        public ActionResult PageRetention(List<String> Columns)
        {
            try
            {
                String Col = "";
                int i = 1;
                if (Columns != null && Columns.Count > 0)
                {
                    Col = string.Join(",", Columns);
                }

                int k = obj.InsertPageRetention(Col, Session["userid"].ToString(), "ACTIVITY DETAILS");

                return Json(k, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
        // End of Rev 1.0
    }
}
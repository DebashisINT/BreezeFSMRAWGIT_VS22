using BusinessLogicLayer.SalesTrackerReports;
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
    public class EmployeeAchievementDetailsController : Controller
    {
        EmployeeAchievementDetailsBL obj = new EmployeeAchievementDetailsBL();
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

                string DeptId = "";
                k = 1;
                if (model.Department != null && model.Department.Count > 0)
                {
                    foreach (string item in model.Department)
                    {
                        if (k > 1)
                            DeptId = DeptId + "," + item;
                        else
                            DeptId = item;
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

                string Supercode = "";
                k = 1;
                if (model.Supervisor != null && model.Supervisor.Count > 0)
                {
                    foreach (string item in model.Supervisor)
                    {
                        if (k > 1)
                            Supercode = Supercode + "," + item;
                        else
                            Supercode = item;
                        k++;
                    }
                }

                //double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;
                //if (days <= 30)
                //{
                    dt = obj.GetReportEmployeeAchivement(datfrmat, dattoat, Userid, DeptId, empcode, Desgid, Supercode);
                //}
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
                var q = from d in dc.FTS_EmployeeAchievementDetailsReports
                        where d.USERID == Convert.ToInt32(Userid)
                        //orderby d.EMPLOYEE ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTS_EmployeeAchievementDetailsReports
                        where d.USERID == 0
                        select d;
                return q;
            }
        }

        public ActionResult ExportEmployeeAchivementReport(int type, string IsPageload)
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
            settings.Name = "EmployeeAchievementDetailsReport";
            settings.CallbackRouteValues = new { Controller = "EmployeeAchievementDetails", Action = "GenerateTable" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "EmployeeAchievementDetailsReport";


            settings.Columns.Add(column =>
            {
                column.Caption = "Date";
                column.FieldName = "Date";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ColumnType = MVCxGridViewColumnType.DateEdit;
                column.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (column.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
                column.Width = 120;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Time";
                column.FieldName = "Time";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 100;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Employee";
                column.Caption = "Employee";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 180;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Supervisor";
                column.FieldName = "Supervisor";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 180;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Designation";
                column.Caption = "Designation";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 150;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Department";
                column.FieldName = "Department";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 180;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "CustomerName";
                column.Caption = "Customer Name";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 180;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "CustomerAddress";
                column.Caption = "Customer Address";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 200;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Contact No.";
                column.FieldName = "ContactNo";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 100;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Mail";
                column.Caption = "Mail Id";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 150;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Model";
                column.Caption = "Model";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 180;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Primary Application";
                column.FieldName = "PrimaryApplication";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 180;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "SecondaryApplication";
                column.Caption = "Secondary Application";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 180;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "BookingAmount";
                column.Caption = "Booking Amount";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.PropertiesEdit.DisplayFormatString = "0.00";
                column.Width = 100;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "LeadType";
                column.Caption = "Lead Type";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 100;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Stage";
                column.Caption = "Stage";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 100;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "FunnelStage";
                column.Caption = "Funnel Stage";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
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
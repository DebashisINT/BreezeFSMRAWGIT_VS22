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
    public class MeetingDetailsController : Controller
    {
        MeetingDetailsBL obj = new MeetingDetailsBL();
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
                    dt = obj.GetReportMeetingDetails(datfrmat, dattoat, Userid, state, empcode, Desgid);
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
                var q = from d in dc.FTS_MeetingDetailsReports
                        where d.USERID == Convert.ToInt32(Userid)
                        //orderby d.EMPLOYEE ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTS_MeetingDetailsReports
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
            settings.Name = "MeetingDetailsReport";
            settings.CallbackRouteValues = new { Controller = "MeetingDetails", Action = "GenerateTable" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "MeetingDetailsReport";

            settings.Columns.Add(column =>
            {
                column.Caption = "Employee Name";
                column.FieldName = "EMPLOYEE";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 180;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "User Login Id";
                column.FieldName = "USER_LOGINID";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 100;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Employee Code";
                column.FieldName = "EMPLOYEE_CODE";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 100;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "EMPLOYEE_DESIGNATION";
                column.Caption = "Designation";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 180;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "REPORTTO";
                column.Caption = "Supervisor";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 200;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Type";
                column.FieldName = "SHOP_NAME";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 180;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "MEETING_NAME";
                column.Caption = "Meeting Type";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 200;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "LATITUDE";
                column.Caption = "Latitude";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 100;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Longitude";
                column.FieldName = "LONGITUDE";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 100;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "MEETING_ADDRESS";
                column.Caption = "Meeting Address";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 250;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "MEETING_PINCODE";
                column.Caption = "Meeting Pincode";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 100;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "REMARKS";
                column.Caption = "Remarks";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 250;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Visited Date Time";
                column.FieldName = "VISITED_TIME";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ColumnType = MVCxGridViewColumnType.DateEdit;
                column.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy hh:mm:ss";
                (column.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy hh:mm:ss";
                column.Width = 100;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "SPENT_DURATION";
                column.Caption = "Spent Duration";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 200;
            });

            //settings.Columns.Add(column =>
            //{
            //    column.FieldName = "TOTAL_VISIT_COUNT";
            //    column.Caption = "Total Visit";
            //    column.ColumnType = MVCxGridViewColumnType.TextBox;
            //    column.Width = 200;
            //});

            settings.Columns.Add(column =>
            {
                column.FieldName = "DISTANCE_TRAVELLED";
                column.Caption = "Distance Travelled";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 100;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Meeting Sync Date & Time";
                column.FieldName = "CREATEDDATE";
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
	}
}
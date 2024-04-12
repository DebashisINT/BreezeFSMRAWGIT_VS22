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
    public class DailyAccountabilityTrackerController : Controller
    {
        DailyAccountabilityTrackerBL obj = new DailyAccountabilityTrackerBL();
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
            DataTable dt = (DataTable)TempData["Count"];
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewBag.VisitCount = dt.Rows[0]["VisitCount"].ToString();
                ViewBag.PhnActivityCount = dt.Rows[0]["PhnActivityCount"].ToString();
                ViewBag.SocialMediaActivityCount = dt.Rows[0]["SocialMediaActivityCount"].ToString();
                ViewBag.OthersActivityCount = dt.Rows[0]["OthersActivityCount"].ToString();
                ViewBag.DoctorLeadCount = dt.Rows[0]["DoctorLeadCount"].ToString();
                ViewBag.OrderCount = dt.Rows[0]["OrderCount"].ToString();
                ViewBag.InvUpdateCount = dt.Rows[0]["InvUpdateCount"].ToString();
                ViewBag.CollectionCount = dt.Rows[0]["CollectionCount"].ToString();
            }
            else
            {
                ViewBag.VisitCount = "0";
                ViewBag.PhnActivityCount = "0";
                ViewBag.SocialMediaActivityCount = "0";
                ViewBag.OthersActivityCount = "0";
                ViewBag.DoctorLeadCount = "0";
                ViewBag.OrderCount = "0";
                ViewBag.InvUpdateCount = "0";
                ViewBag.CollectionCount = "0";
            }
            TempData.Keep();
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
                    dt = obj.GetDailyAccountabilityTracker(datfrmat, dattoat, Userid, state, empcode, Desgid);

                    TempData["Count"] = dt;
                    TempData.Keep();
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
            DataTable dtColmn = obj.GetPageRetention(Session["userid"].ToString(), "DAILY ACCOUNTABILITY TRACKER");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;// DataTable na class pathao ok wait
            }

            string connectionString = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"]);
            string Userid = Convert.ToString(Session["userid"]);

            if (ispageload != "1")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSDAILYACCOUNTABILITYTRACKER_REPORTs
                        where d.USERID == Convert.ToInt32(Userid)
                        //orderby d.EMPLOYEE ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSDAILYACCOUNTABILITYTRACKER_REPORTs
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
            DataTable dtColmn = obj.GetPageRetention(Session["userid"].ToString(), "DAILY ACCOUNTABILITY TRACKER");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            }

            var settings = new GridViewSettings();
            settings.Name = "DailyAccountabilityTracker";
            settings.CallbackRouteValues = new { Controller = "DailyAccountabilityTracker", Action = "GenerateTable" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "DailyAccountabilityTracker";

            settings.Columns.Add(column =>
            {
                column.Caption = "Date";
                column.FieldName = "DATE";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ColumnType = MVCxGridViewColumnType.DateEdit;
                column.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (column.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
                //column.ExportWidth = 100;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DATE'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 80;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 80;
                }
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Employee";
                column.FieldName = "Employee";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                //column.ExportWidth = 150;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Employee'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 200;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 200;
                }
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Doctor/Lab (Visited)";
                column.FieldName = "Doctors_Visited";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                //column.ExportWidth = 150;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Doctors_Visited'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 180;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 180;
                }
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Topic of Discussion";
                column.FieldName = "TopicOfDiscussion";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                //column.ExportWidth = 150;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='TopicOfDiscussion'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 150;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 150;
                }
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Doctor/Lab (Phone)";
                column.FieldName = "Doctors_Phone";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                //column.ExportWidth = 150;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Doctors_Phone'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 130;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 130;
                }
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Topic of Discussion";
                column.FieldName = "Doctors_PhoneTopicOfDiscussion";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                //column.ExportWidth = 150;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Doctors_PhoneTopicOfDiscussion'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 150;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 150;
                }
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "DoctorsWhatsApp";
                column.Caption = "Doctor/Lab (Social Media)";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                //column.ExportWidth = 150;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DoctorsWhatsApp'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 150;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 150;
                }
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "DoctorsWhatsApp_TopicOfDiscussion";
                column.Caption = "Topic of Discussion";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                //column.ExportWidth = 200;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DoctorsWhatsApp_TopicOfDiscussion'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 200;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 200;
                }
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Doctor/Lab (Others)";
                column.FieldName = "DoctorsOthers";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                //column.ExportWidth = 180;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DoctorsOthers'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 180;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 180;
                }
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "DoctorsOthers_TopicOfDiscussion";
                column.Caption = "Topic of Discussion";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                //column.ExportWidth = 250;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DoctorsOthers_TopicOfDiscussion'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 250;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 250;
                }
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "DoctorLead";
                column.Caption = "Doctor/Lab - Lead";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                //column.ExportWidth = 150;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DoctorLead'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 150;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 150;
                }
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "DoctorSamplePickedUp";
                column.Caption = "Doctor/Lab (Sample Picked Up)";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                //column.ExportWidth = 150;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DoctorSamplePickedUp'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 150;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 150;
                }
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Order Amount";
                column.FieldName = "OrderAmount";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                //column.ExportWidth = 150;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='OrderAmount'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 120;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 120;
                }
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Doctor/Lab (Sample Actual)";
                column.FieldName = "DoctorSampleActual";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                //column.ExportWidth = 150;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DoctorSampleActual'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 150;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 150;
                }
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Inv. Amount";
                column.FieldName = "InvAmount";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                //column.ExportWidth = 150;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='InvAmount'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 120;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 120;
                }
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Doctor/Lab Amount Rec.";
                column.FieldName = "DoctorLabAmountRec";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                //column.ExportWidth = 150;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DoctorLabAmountRec'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 150;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 150;
                }
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Collection Amt.";
                column.FieldName = "CollectionAmt";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                //column.ExportWidth = 150;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CollectionAmt'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 120;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 120;
                }
            });



            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

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
                int k = obj.InsertPageRetention(Col, Session["userid"].ToString(), "DAILY ACCOUNTABILITY TRACKER");

                return Json(k, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
    }
}
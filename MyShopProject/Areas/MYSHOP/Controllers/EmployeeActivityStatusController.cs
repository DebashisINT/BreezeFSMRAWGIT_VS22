using BusinessLogicLayer.SalesmanTrack;
using BusinessLogicLayer.SalesTrackerReports;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using MyShop.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilityLayer;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class EmployeeActivityStatusController : Controller
    {
        EmployeeActivityStatusBL objshop = new EmployeeActivityStatusBL();
        public ActionResult Index()
        {
            try
            {
                TempData["EmployeeActivityStatus2ndStagedt"] = null;

                EmployeeActivityStatusModel omodel = new EmployeeActivityStatusModel();
                omodel.FromDate = DateTime.Now.ToString("dd-MM-yyyy");
                omodel.ToDate = DateTime.Now.ToString("dd-MM-yyyy");

                DataTable dt = new SalesSummary_Report().GetStateMandatory(Session["userid"].ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewBag.StateMandatory = dt.Rows[0]["IsStateMandatoryinReport"].ToString();
                }
                return View(omodel);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetEmployeeActivityStatuslistPartial(EmployeeActivityStatusModel model)
        {
            try
            {
                List<EmployeeActiveStatusLists> omel = new List<EmployeeActiveStatusLists>();
                DataTable dt = new DataTable();

                string Employee = "";
                int i = 1;

                if (model.EmployeeID != null && model.EmployeeID.Count > 0)
                {
                    foreach (string item in model.EmployeeID)
                    {
                        if (i > 1)
                            Employee = Employee + "," + item;
                        else
                            Employee = item;
                        i++;
                    }
                }

                string State_id = "";
                int j = 1;
                if (model.State != null && model.State.Count > 0)
                {
                    foreach (string item in model.State)
                    {
                        if (j > 1)
                            State_id = State_id + "," + item;
                        else
                            State_id = item;
                        j++;
                    }
                }

                string Designation_id = "";
                int k = 1;
                if (model.Designation_id != null && model.Designation_id.Count > 0)
                {
                    foreach (string item in model.Designation_id)
                    {
                        if (k > 1)
                            Designation_id = Designation_id + "," + item;
                        else
                            Designation_id = item;
                        k++;
                    }
                }

                string Is_PageLoad = string.Empty;

                if (model.Ispageload == "0")
                {
                    Is_PageLoad = "Ispageload";
                    model.FromDate = DateTime.Now.AddDays(-1).ToString("dd-MM-yyyy");
                    model.ToDate = DateTime.Now.AddDays(-1).ToString("dd-MM-yyyy");
                }

                if (model.FromDate == null)
                {
                    model.FromDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                }
                else
                {
                    model.FromDate = model.FromDate.Split('-')[2] + '-' + model.FromDate.Split('-')[1] + '-' + model.FromDate.Split('-')[0];
                }

                if (model.ToDate == null)
                {
                    model.ToDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                }
                else
                {
                    model.ToDate = model.ToDate.Split('-')[2] + '-' + model.ToDate.Split('-')[1] + '-' + model.ToDate.Split('-')[0];
                }

                string FromDate = model.FromDate;
                string ToDate = model.ToDate;
                string userID = Convert.ToString(Session["userid"]);

                double days = (Convert.ToDateTime(model.ToDate) - Convert.ToDateTime(model.FromDate)).TotalDays;

                if (days <= 30)
                {
                    if (model.Ispageload != "0")
                    {
                        dt = objshop.GenerateEmployeeActivitySummaryData(Employee, FromDate, ToDate, Convert.ToInt64(userID), State_id, Designation_id);
                    }
                }
                return PartialView("_PartialGridEmployeeActivityStatus", GetEmployeeActiveStatus(Is_PageLoad));
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public IEnumerable GetEmployeeActiveStatus(string Is_PageLoad)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string userID = Convert.ToString(Session["userid"]);

            if (Is_PageLoad != "Ispageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSEMPLOYEEACTIVITYSTATUS_REPORTs
                        where d.LOGINID == Convert.ToInt32(userID) && d.REPORTTYPE=="Summary"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                if (Is_PageLoad != "Ispageload")
                {
                    ReportsDataContext dc = new ReportsDataContext(connectionString);
                    var q = from d in dc.FTSEMPLOYEEACTIVITYSTATUS_REPORTs
                            where d.LOGINID == Convert.ToInt32(userID) && d.REPORTTYPE == ""
                            orderby d.SEQ ascending
                            select d;
                    return q;
                }
                else
                {
                    return null;
                }
            }
        }

        public ActionResult ExportActivityStatuslist(int type)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetEmployeeActivityStatusExport(), GetEmployeeActiveStatus(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetEmployeeActivityStatusExport(), GetEmployeeActiveStatus(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetEmployeeActivityStatusExport(), GetEmployeeActiveStatus(""));
                //break;
                case 4:
                    return GridViewExtension.ExportToRtf(GetEmployeeActivityStatusExport(), GetEmployeeActiveStatus(""));
                //break;
                case 5:
                    return GridViewExtension.ExportToCsv(GetEmployeeActivityStatusExport(), GetEmployeeActiveStatus(""));
                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetEmployeeActivityStatusExport()
        {
            var settings = new GridViewSettings();
            settings.Name = "Employee Activity Status";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Employee_Activity_Status";

            settings.Columns.Add(column =>
            {
                column.Caption = "Date";
                column.FieldName = "VISIT_DATETIME";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Employee ID";
                column.FieldName = "EMPLOYEE_ID";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Employee Name";
                column.FieldName = "EMPNAME";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "State Name";
                column.FieldName = "STATE_NAME";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Designation";
                column.FieldName = "DESIGNATION";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Login Time";
                column.FieldName = "LOGGEDIN";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Logout Time";
                column.FieldName = "LOGEDOUT";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Activity Count";
                column.FieldName = "ACTIVITYCNT";
                column.PropertiesEdit.DisplayFormatString = "0";
            });

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

        [HttpPost]
        public ActionResult GetSecondStageData(EmployeeActivityStatus2ndStageModel model)
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
                string Loginid = Convert.ToString(Session["userid"]);                

                double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;
                dt = objshop.GenerateEmployeeActivityDetailsData(model.userid, datfrmat, dattoat, Convert.ToInt64(Loginid), model.date);
                //if (dt.Rows.Count > 0)
                //{

                //    TempData["EmployeeActivityStatus2ndStagedt"] = dt;
                //    TempData.Keep();
                //}
                //else
                //{
                //    TempData["EmployeeActivityStatus2ndStagedt"] = null;
                //    TempData.Keep();
                //}
                return Json(Loginid, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public PartialViewResult RenderSecondStagegrid(string Ispageload)
        {
            string Is_PageLoad = string.Empty;

            if (Ispageload == "0")
            {
                Is_PageLoad = "Ispageload";
            }
            return PartialView("_PartialRenderSecondStage", GetEmployeeActive2ndStatus(Is_PageLoad));
        }

        public IEnumerable GetEmployeeActive2ndStatus(string Is_PageLoad)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string userID = Convert.ToString(Session["userid"]);

            if (Is_PageLoad != "Ispageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSEMPLOYEEACTIVITYSTATUS_REPORTs
                        where d.LOGINID == Convert.ToInt32(userID) && d.REPORTTYPE == "Details"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                if (Is_PageLoad != "Ispageload")
                {
                    ReportsDataContext dc = new ReportsDataContext(connectionString);
                    var q = from d in dc.FTSEMPLOYEEACTIVITYSTATUS_REPORTs
                            where d.LOGINID == Convert.ToInt32(userID) && d.REPORTTYPE == ""
                            orderby d.SEQ ascending
                            select d;
                    return q;
                }
                else
                {
                    return null;
                }
            }
        }

        public ActionResult ExportEmployeeActivityStatus2ndStageList(int type)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetEmployeeActivity2ndStatusExport(), GetEmployeeActive2ndStatus(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetEmployeeActivity2ndStatusExport(), GetEmployeeActive2ndStatus(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetEmployeeActivity2ndStatusExport(), GetEmployeeActive2ndStatus(""));
                //break;
                case 4:
                    return GridViewExtension.ExportToRtf(GetEmployeeActivity2ndStatusExport(), GetEmployeeActive2ndStatus(""));
                //break;
                case 5:
                    return GridViewExtension.ExportToCsv(GetEmployeeActivity2ndStatusExport(), GetEmployeeActive2ndStatus(""));
                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetEmployeeActivity2ndStatusExport()
        {
            var settings = new GridViewSettings();
            settings.Name = "Employee Activity Status Details";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Employee_Activity_Status_Details";

            settings.Columns.Add(column =>
            {
                column.Caption = "Employee ID";
                column.FieldName = "EMPLOYEE_ID";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Employee Name";
                column.FieldName = "EMPNAME";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "State Name";
                column.FieldName = "STATE_NAME";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Branch";
                column.FieldName = "BRANCHDESC";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Designation";
                column.FieldName = "DESIGNATION";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Shop Name";
                column.FieldName = "SHOP_NAME";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Code";
                column.FieldName = "ENTITYCODE";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Shop Type";
                column.FieldName = "SHOP_TYPE";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Mobile No.";
                column.FieldName = "MOBILE_NO";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Location";
                column.FieldName = "VISITLOCATION";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Visit Time";
                column.FieldName = "VISIT_DATETIME";
                column.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy hh:mm:ss";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Duration";
                column.FieldName = "DURATION";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Visit Type";
                column.FieldName = "VISIT_TYPE";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Distance";
                column.FieldName = "DISTANCE";
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Remarks";
                column.FieldName = "REMARKS";
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
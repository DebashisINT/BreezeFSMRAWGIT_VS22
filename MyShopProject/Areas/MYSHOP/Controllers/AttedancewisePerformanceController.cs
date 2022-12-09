using SalesmanTrack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BusinessLogicLayer.SalesTrackerReports;
using System.Data;
using UtilityLayer;
using System.Collections;
using System.Configuration;
using MyShop.Models;
using DevExpress.Web.Mvc;
using DevExpress.Web;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class AttedancewisePerformanceController : Controller
    {
        UserList lstuser = new UserList();
        AttendancewisePerformanceReport_List objgps = null; 
        public AttedancewisePerformanceController()
        {
            objgps = new AttendancewisePerformanceReport_List();
        }
        //
        // GET: /MYSHOP/AttedancewisePerformance/
        public ActionResult AttedancewisePerformance()
        {
             try
            {
            AttedancewisePerformanceReport omodel = new AttedancewisePerformanceReport();
            string userid = Session["userid"].ToString();

            List<AttenMonths> month = new List<AttenMonths>();
            month.Add(new AttenMonths { MID = "JAN", AttenMonthName = "January" });
            month.Add(new AttenMonths { MID = "FEB", AttenMonthName = "February" });
            month.Add(new AttenMonths { MID = "MAR", AttenMonthName = "March" });
            month.Add(new AttenMonths { MID = "APR", AttenMonthName = "April" });
            month.Add(new AttenMonths { MID = "MAY", AttenMonthName = "May" });
            month.Add(new AttenMonths { MID = "JUN", AttenMonthName = "June" });
            month.Add(new AttenMonths { MID = "JUL", AttenMonthName = "July" });
            month.Add(new AttenMonths { MID = "AUG", AttenMonthName = "August" });
            month.Add(new AttenMonths { MID = "SEP", AttenMonthName = "September" });
            month.Add(new AttenMonths { MID = "OCT", AttenMonthName = "October" });
            month.Add(new AttenMonths { MID = "NOV", AttenMonthName = "November" });
            month.Add(new AttenMonths { MID = "DEC", AttenMonthName = "December" });

            List<Years> year = new List<Years>();

            DataTable dtyr = objgps.GetYearList();
            if (dtyr != null && dtyr.Rows.Count > 0)
            {
                foreach (DataRow item in dtyr.Rows)
                {
                    year.Add(new Years
                    {
                        ID = Convert.ToString(item["YEARS"]),
                        YearName = Convert.ToString(item["YEARS"])
                    });
                }
            }

            omodel.MonthList = month;
            omodel.yearList = year;
            return View(omodel);
            }
             catch
             {
                 return RedirectToAction("Logout", "Login", new { Area = "" });
             }
        }

        public PartialViewResult GetAttendancePerformance(AttedancewisePerformanceReport AttenPerformance)
        {
            DataTable dt = new DataTable();
            string frmdate = string.Empty;

            if (AttenPerformance.is_pageload == "0")
            {
                frmdate = "Ispageload";
            }

            if (AttenPerformance.is_procfirst == 1)
            {
                ViewData["ModelData"] = AttenPerformance;

                string month = AttenPerformance.Month;
                string year = AttenPerformance.Year;
                string Userid = Convert.ToString(Session["userid"]);

                string state = "";
                int i = 1;

                if (AttenPerformance.StateId != null && AttenPerformance.StateId.Count > 0)
                {
                    foreach (string item in AttenPerformance.StateId)
                    {
                        if (i > 1)
                            state = state + "," + item;
                        else
                            state = item;
                        i++;
                    }
                }

                string desig = "";
                int j = 1;

                if (AttenPerformance.desgid != null && AttenPerformance.desgid.Count > 0)
                {
                    foreach (string item in AttenPerformance.desgid)
                    {
                        if (j > 1)
                            desig = desig + "," + item;
                        else
                            desig = item;
                        j++;
                    }
                }

                string empcode = "";
                int k = 1;

                if (AttenPerformance.empcode != null && AttenPerformance.empcode.Count > 0)
                {
                    foreach (string item in AttenPerformance.empcode)
                    {
                        if (k > 1)
                            empcode = empcode + "," + item;
                        else
                            empcode = item;
                        k++;
                    }
                }
                dt = objgps.GetAttendancewisePerformanceListReport(month, Userid, state, desig, empcode, "Summary", year);
            }
            return PartialView("GetAttendancePerformance", LGetAttendancewisePerformance(frmdate));
        }

        public ActionResult GetStateList()
        {
            try
            {
                List<GetUsersStates> modelstate = new List<GetUsersStates>();
                DataTable dtstate = lstuser.GetStateList();
                modelstate = APIHelperMethods.ToModelList<GetUsersStates>(dtstate);
                return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_StatesPartial.cshtml", modelstate);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetDesigList()
        {
            try
            {
                DataTable dtdesig = lstuser.Getdesiglist();
                List<GetDesignation> modeldesig = new List<GetDesignation>();
                modeldesig = APIHelperMethods.ToModelList<GetDesignation>(dtdesig);

                return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_DesigPartial.cshtml", modeldesig);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetEmpList(ReimbursementReport model)
        {
            try
            {
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

                string desig = "";
                int k = 1;

                if (model.desgid != null && model.desgid.Count > 0)
                {
                    foreach (string item in model.desgid)
                    {
                        if (k > 1)
                            desig = desig + "," + item;
                        else
                            desig = item;
                        k++;
                    }
                }
                DataTable dtemp = lstuser.Getemplist(state, desig);
                List<GetAllEmployee> modelemp = new List<GetAllEmployee>();
                modelemp = APIHelperMethods.ToModelList<GetAllEmployee>(dtemp);
                return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_EmpPartial.cshtml", modelemp);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }

        public IEnumerable LGetAttendancewisePerformance(string frmdate)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);

            if (frmdate != "Ispageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSATTENDANCEWISEPERFORMANCE_REPORTs
                        where d.USERID == Convert.ToInt32(Userid) && d.RPTTYPE=="Summary"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSATTENDANCEWISEPERFORMANCE_REPORTs
                        where d.USERID == Convert.ToInt32(Userid) && d.EMPCODE == "0"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
        }

        public IEnumerable LGetAttendancewisePerformanceDetails(string frmdate)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);

            if (frmdate != "Ispageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSATTENDANCEWISEPERFORMANCE_REPORTs
                        where d.USERID == Convert.ToInt32(Userid) && d.RPTTYPE == "Details"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSATTENDANCEWISEPERFORMANCE_REPORTs
                        where d.USERID == Convert.ToInt32(Userid) && d.EMPCODE == "0"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
        }

        public ActionResult ExporAttendancewisePerformanceList(int type)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetAttendancePerformance(), LGetAttendancewisePerformance(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetAttendancePerformance(), LGetAttendancewisePerformance(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetAttendancePerformance(), LGetAttendancewisePerformance(""));
                case 4:
                    return GridViewExtension.ExportToRtf(GetAttendancePerformance(), LGetAttendancewisePerformance(""));
                case 5:
                    return GridViewExtension.ExportToCsv(GetAttendancePerformance(), LGetAttendancewisePerformance(""));
                //break;

                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetAttendancePerformance()
        {
            var settings = new GridViewSettings();
            settings.Name = "Attendance Vs Performance - Summary";
            settings.CallbackRouteValues = new { Controller = "AttedancewisePerformance", Action = "GetAttendancePerformance" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Attendance Vs Performance - Summary";

            settings.Columns.Add(column =>
            {
                column.Caption = "Employee ID";
                column.FieldName = "EMPID";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Employee Name";
                column.FieldName = "EMPNAME";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Login ID";
                column.FieldName = "CONTACTNO";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Supervisor";
                column.FieldName = "REPORTTO";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "State";
                column.FieldName = "STATE";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Designation";
                column.FieldName = "DESIGNATION";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Late Count";
                column.FieldName = "LATE_CNT";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Absent Count";
                column.FieldName = "ABSENT_CNT";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Not Login Count";
                column.FieldName = "NOTLOGIN_CNT";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Target NC";
                column.FieldName = "TGT_NC";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Achv. NC";
                column.FieldName = "ACHV_NC";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Target RV";
                column.FieldName = "TGT_RV";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Achv. RV";
                column.FieldName = "ACHV_RV";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Target Value";
                column.FieldName = "TGT_ORDERVALUE";
                column.PropertiesEdit.DisplayFormatString = "0.00";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Achv. Value";
                column.FieldName = "ACHV_ORDERVALUE";
                column.PropertiesEdit.DisplayFormatString = "0.00";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Target Collection";
                column.FieldName = "TGT_COLLECTION";
                column.PropertiesEdit.DisplayFormatString = "0.00";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Achv. Collection";
                column.FieldName = "ACHV_COLLECTION";
                column.PropertiesEdit.DisplayFormatString = "0.00";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });            

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

        public ActionResult PartialAttendancewisePerformanceDetails(AttedancewisePerformanceReport AttenPerformanceDetails)
        {
            try
            {
                string frmdate = string.Empty;
                if (AttenPerformanceDetails.is_pageload == "1")
                {
                    DataTable dt = new DataTable();                                       

                    string month = AttenPerformanceDetails.Month;
                    string Userid = Convert.ToString(Session["userid"]);
                    string empcode = AttenPerformanceDetails.EmployeeCode;
                    string year=AttenPerformanceDetails.Year;

                    dt = objgps.GetAttendancewisePerformanceListReport(month, Userid, "", "", empcode, "Details", year);
                }
                return PartialView("PartialAttendancewisePerformanceDetails", LGetAttendancewisePerformanceDetails(frmdate));
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult ExporAttendancewisePerformanceDetailsList(int type)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetAttendancePerformanceDetails(), LGetAttendancewisePerformanceDetails(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetAttendancePerformanceDetails(), LGetAttendancewisePerformanceDetails(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetAttendancePerformanceDetails(), LGetAttendancewisePerformanceDetails(""));
                case 4:
                    return GridViewExtension.ExportToRtf(GetAttendancePerformanceDetails(), LGetAttendancewisePerformanceDetails(""));
                case 5:
                    return GridViewExtension.ExportToCsv(GetAttendancePerformanceDetails(), LGetAttendancewisePerformanceDetails(""));
                //break;

                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetAttendancePerformanceDetails()
        {
            var settings = new GridViewSettings();
            settings.Name = "Attendance Vs Performance - Details";
            settings.CallbackRouteValues = new { Controller = "AttedancewisePerformance", Action = "PartialAttendancewisePerformanceDetails" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Attendance Vs Performance - Details";

            settings.Columns.Add(column =>
            {
                column.Caption = "Date";
                column.FieldName = "WORK_DATE";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Login ID";
                column.FieldName = "CONTACTNO";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Employee Name";
                column.FieldName = "EMPNAME";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Approved In Time(Hrs.)";
                column.FieldName = "IN_TIME";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Grace Period(Hrs.)";
                column.FieldName = "GRACE_TIME";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Attendance Time";
                column.FieldName = "LOGGEDIN";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Late (Hrs.)";
                column.FieldName = "LATE_HRS";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Target NC/Day";
                column.FieldName = "TGT_NC";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Achv. NC";
                column.FieldName = "ACHV_NC";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Target RV/Day";
                column.FieldName = "TGT_RV";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Achv. RV";
                column.FieldName = "ACHV_RV";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Target Value/Day";
                column.FieldName = "TGT_ORDERVALUE";
                column.PropertiesEdit.DisplayFormatString = "0.00";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Achv. Value";
                column.FieldName = "ACHV_ORDERVALUE";
                column.PropertiesEdit.DisplayFormatString = "0.00";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Target Collection/Day";
                column.FieldName = "TGT_COLLECTION";
                column.PropertiesEdit.DisplayFormatString = "0.00";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Achv. Collection";
                column.FieldName = "ACHV_COLLECTION";
                column.PropertiesEdit.DisplayFormatString = "0.00";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
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
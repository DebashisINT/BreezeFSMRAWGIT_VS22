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
    public class PerformanceSummaryMonthWiseController : Controller
    {
        UserList lstuser = new UserList();
        PerformanceSummaryMonthWise_List objgps = null;
        public PerformanceSummaryMonthWiseController()
        {
            objgps = new PerformanceSummaryMonthWise_List();
        }
        //
        // GET: /MYSHOP/PerformanceSummaryMonthWise/
        public ActionResult PerformanceSummaryMonthWise()
        {
            try
            {
                PerformanceSummaryMonthWiseReportModel omodel = new PerformanceSummaryMonthWiseReportModel();
                string userid = Session["userid"].ToString();

                List<PerformanceMonth> Pmonth = new List<PerformanceMonth>();
                Pmonth.Add(new PerformanceMonth { PID = "JAN", PMonthName = "January" });
                Pmonth.Add(new PerformanceMonth { PID = "FEB", PMonthName = "February" });
                Pmonth.Add(new PerformanceMonth { PID = "MAR", PMonthName = "March" });
                Pmonth.Add(new PerformanceMonth { PID = "APR", PMonthName = "April" });
                Pmonth.Add(new PerformanceMonth { PID = "MAY", PMonthName = "May" });
                Pmonth.Add(new PerformanceMonth { PID = "JUN", PMonthName = "June" });
                Pmonth.Add(new PerformanceMonth { PID = "JUL", PMonthName = "July" });
                Pmonth.Add(new PerformanceMonth { PID = "AUG", PMonthName = "August" });
                Pmonth.Add(new PerformanceMonth { PID = "SEP", PMonthName = "September" });
                Pmonth.Add(new PerformanceMonth { PID = "OCT", PMonthName = "October" });
                Pmonth.Add(new PerformanceMonth { PID = "NOV", PMonthName = "November" });
                Pmonth.Add(new PerformanceMonth { PID = "DEC", PMonthName = "December" });

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

                omodel.YearList = year;

                omodel.MonthList = Pmonth;
                return View(omodel);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public PartialViewResult PartialPerformanceSummaryMonthWise(PerformanceSummaryMonthWiseReportModel PerformanceMonthWise)
        {
            DataTable dt = new DataTable();
            string frmdate = string.Empty;

            if (PerformanceMonthWise.is_pageload == "0")
            {
                frmdate = "Ispageload";
            }

            if (PerformanceMonthWise.is_procfirst == 1)
            {
                ViewData["ModelData"] = PerformanceMonthWise;

                string month = PerformanceMonthWise.Month;
                string year = PerformanceMonthWise.Year;
                string Userid = Convert.ToString(Session["userid"]);

                string state = "";
                int i = 1;

                if (PerformanceMonthWise.StateId != null && PerformanceMonthWise.StateId.Count > 0)
                {
                    foreach (string item in PerformanceMonthWise.StateId)
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

                if (PerformanceMonthWise.desgid != null && PerformanceMonthWise.desgid.Count > 0)
                {
                    foreach (string item in PerformanceMonthWise.desgid)
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

                if (PerformanceMonthWise.empcode != null && PerformanceMonthWise.empcode.Count > 0)
                {
                    foreach (string item in PerformanceMonthWise.empcode)
                    {
                        if (k > 1)
                            empcode = empcode + "," + item;
                        else
                            empcode = item;
                        k++;
                    }
                }
                dt = objgps.GetPerformanceMonthWiseListReport(month, Userid, state, desig, empcode, year);
            }
            return PartialView("PartialPerformanceSummaryMonthWise", LGetPerformanceMonthWise(frmdate));
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

        public IEnumerable LGetPerformanceMonthWise(string frmdate)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
                string Userid = Convert.ToString(Session["userid"]);

                if (frmdate != "Ispageload")
                {
                    ReportsDataContext dc = new ReportsDataContext(connectionString);
                    var q = from d in dc.FTSPERFORMANCESUMMARYMONTHWISE_REPORTs
                            where d.USERID == Convert.ToInt32(Userid)
                            orderby d.SEQ ascending
                            select d;
                    return q;
                }
                else
                {
                    if (frmdate != "Ispageload")
                    {
                        ReportsDataContext dc = new ReportsDataContext(connectionString);
                        var q = from d in dc.FTSPERFORMANCESUMMARYMONTHWISE_REPORTs
                                where d.USERID == Convert.ToInt32(Userid) && d.EMPCODE == "0"
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
            catch{
                return null;
            }
        }

        public ActionResult ExporPerformanceMonthWiseList(int type)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(PartialPerformanceSummaryMonthWise(), LGetPerformanceMonthWise(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(PartialPerformanceSummaryMonthWise(), LGetPerformanceMonthWise(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(PartialPerformanceSummaryMonthWise(), LGetPerformanceMonthWise(""));
                case 4:
                    return GridViewExtension.ExportToRtf(PartialPerformanceSummaryMonthWise(), LGetPerformanceMonthWise(""));
                case 5:
                    return GridViewExtension.ExportToCsv(PartialPerformanceSummaryMonthWise(), LGetPerformanceMonthWise(""));
                //break;

                default:
                    break;
            }

            return null;
        }

        private GridViewSettings PartialPerformanceSummaryMonthWise()
        {
            var settings = new GridViewSettings();
            settings.Name = "Performance Summary Month Wise";
            settings.CallbackRouteValues = new { Controller = "PerformanceSummaryMonthWise", Action = "PartialPerformanceSummaryMonthWise" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Performance Summary Month Wise";

            settings.Columns.Add(column =>
            {
                column.Caption = "Employee ID";
                column.FieldName = "EMPID";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Employee(s)";
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
                column.Caption = "Late";
                column.FieldName = "LATE_CNT";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Less Visit (>7 & <10)";
                column.FieldName = "LATE_LESSVISIT";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Absent (Late & Less Visit)";
                column.FieldName = "ABSENT_CNT";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Absent (<7 Visit)";
                column.FieldName = "ABSENT_LESSVISIT";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Not Login";
                column.FieldName = "NOTLOGIN_CNT";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Present (Work+Leave)";
                column.FieldName = "PRESENT_CNT";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Man Days";
                column.FieldName = "MANDAYS_CNT";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Visit Target";
                column.FieldName = "VISIT_TGT";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Visit Achv.";
                column.FieldName = "VISIT_ACHV";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Avg. Achv.";
                column.FieldName = "AVG_VISITACHV";
                column.PropertiesEdit.DisplayFormatString = "0.00";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Target Order";
                column.FieldName = "ORDVAL_TGT";
                column.PropertiesEdit.DisplayFormatString = "0.00";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Achv. Order";
                column.FieldName = "ORDERVALUE_MTD";
                column.PropertiesEdit.DisplayFormatString = "0.00";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Order Cnt.";
                column.FieldName = "ORDERCNT";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Delivered";
                column.FieldName = "DELIVERED";
                column.PropertiesEdit.DisplayFormatString = "0.00";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Inv. Cnt.";
                column.FieldName = "BILLCNT";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Distance Covered(KM)";
                column.FieldName = "DISTANCE_COVERED";
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
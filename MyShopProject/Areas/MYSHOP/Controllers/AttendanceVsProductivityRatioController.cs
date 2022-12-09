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
    public class AttendanceVsProductivityRatioController : Controller
    {
        UserList lstuser = new UserList();
        SalesSummary_Report objgps = new SalesSummary_Report();

        public ActionResult Report()
        {
            try
            {
                SalesSummaryReport omodel = new SalesSummaryReport();
                string userid = Session["userid"].ToString();
                omodel.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                omodel.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                return View(omodel);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetSalesSummaryList1()
        {
            return PartialView("PartialGetSalesSummaryList", GetAttenProductRatioData("0"));
        }
        public ActionResult GetAttenProductRatioList(SalesSummaryReport model)
        {
            try
            {
                DataTable dt = new DataTable();
                string frmdate = string.Empty;

                if (model.is_pageload == "0")
                {
                    frmdate = "Ispageload";
                }

                if (model.Fromdate == null)
                {
                    model.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                }

                if (model.Todate == null)
                {
                    model.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                }

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

                string desig = "";
                int j = 1;

                if (model.desgid != null && model.desgid.Count > 0)
                {
                    foreach (string item in model.desgid)
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

                double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;

                //if (days <= 7)
                if (days <= 30)
                {
                    dt = objgps.GetAttendProductRatioReport(datfrmat, dattoat, Userid, state, desig, empcode);
                }

                return PartialView("_PartialGetAttenProductRatio", GetAttenProductRatioData(frmdate));
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
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

        public ActionResult GetEmpList(SalesSummaryReport model)
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

        public IEnumerable GetAttenProductRatioData(string frmdate)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);

            if (frmdate != "Ispageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSATTENDANCEVSPRODUCTIVITYRATIO_REPORTs
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSATTENDANCEVSPRODUCTIVITYRATIO_REPORTs
                        where d.USERID == Convert.ToInt32(Userid) && d.EMPCODE == "0"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
        }


        public ActionResult ExportAttendanceVsProductivityRatioList(int type)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(), GetAttenProductRatioData(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(), GetAttenProductRatioData(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(), GetAttenProductRatioData(""));
                case 4:
                    return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(), GetAttenProductRatioData(""));
                case 5:
                    return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(), GetAttenProductRatioData(""));
                //break;

                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetEmployeeBatchGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "AttendanceVsProductivityRatio";
            settings.CallbackRouteValues = new { Controller = "AttendanceVsProductivityRatio", Action = "GetAttenProductRatioList" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "AttendanceVsProductivityRatio";

            settings.Columns.Add(x =>
            {
                x.FieldName = "LOGIN_DATE";
                x.Caption = "Login Date";
                x.VisibleIndex = 1;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CONTACTNO";
                x.Caption = "Login ID";
                x.VisibleIndex = 2;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "EMPID";
                x.Caption = "Employee ID";
                x.VisibleIndex = 3;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "EMPNAME";
                x.Caption = "Employee(s)";
                x.VisibleIndex = 4;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "STATE";
                x.Caption = "State";
                x.VisibleIndex = 5;
                x.Width = 120;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "OFFICE_ADDRESS";
                x.Caption = "Office Address";
                x.VisibleIndex = 6;
                x.Width = 180;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "DESIGNATION";
                x.Caption = "Designation";
                x.VisibleIndex = 7;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "REPORTTO";
                x.Caption = "Supervisor";
                x.VisibleIndex = 8;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "RPTTODESG";
                x.Caption = "Supervisor Desg.";
                x.VisibleIndex = 9;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ATTEN_STATUS";
                x.Caption = "Attendance Type";
                x.VisibleIndex = 10;
                x.Width = 180;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "WORK_LEAVE_TYPE";
                x.Caption = "Work/Leave Type";
                x.VisibleIndex = 11;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "REMARKS";
                x.Caption = "Remarks";
                x.VisibleIndex = 12;
                x.Width = 120;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ACTINTIME";
                x.Caption = "Actual Attendance time";
                x.VisibleIndex = 13;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ACTGRACEINTIME";
                x.Caption = "Grace Time";
                x.VisibleIndex = 14;
                x.Width = 120;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ALTINTIME";
                x.Caption = "Alt. Attendance time";
                x.VisibleIndex = 15;
                x.Width = 120;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ALTGRACEINTIME";
                x.Caption = "Alt. Grace Time";
                x.VisibleIndex = 16;
                x.Width = 120;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "APPLOGGEDIN";
                x.Caption = "App Login Time";
                x.VisibleIndex = 17;
                x.Width = 120;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "FIRSTCALLTIME";
                x.Caption = "First Call Time";
                x.VisibleIndex = 18;
                x.Width = 120;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "LASTCALLTIME";
                x.Caption = "Last Call time";
                x.VisibleIndex = 19;
                x.Width = 120;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "APPLOGEDOUT";
                x.Caption = "App Logout Time";
                x.VisibleIndex = 20;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ATTENDANCETYPE";
                x.Caption = "Attendance Type";
                x.VisibleIndex = 21;
                x.Width = 50;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ATTENDANCESTATUS";
                x.Caption = "Attendance Status";
                x.VisibleIndex = 22;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "WORKINGDURATION";
                x.Caption = "Working Duration (HH:MM)";
                x.VisibleIndex = 23;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "UNDERTIME";
                x.Caption = "Undertime (HH:MM)";
                x.VisibleIndex = 24;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "GPS_INACTIVE_DURATION";
                x.Caption = "GPS Inactive Duration (HH:MM)";
                x.VisibleIndex = 25;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "GPSINACTIVECNT";
                x.Caption = "GPS Inactive Count";
                x.VisibleIndex = 26;
                x.Width = 70;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.PropertiesEdit.DisplayFormatString = "0";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "IDEAL_TIME";
                x.Caption = "Idle Time (HH:MM)";
                x.VisibleIndex = 27;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "IDEALTIME_CNT";
                x.Caption = "Idle Time Count";
                x.VisibleIndex = 28;
                x.Width = 70;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.PropertiesEdit.DisplayFormatString = "0";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "NEWSHOP_VISITED";
                x.Caption = "Total New Visit";
                x.VisibleIndex = 29;
                x.Width = 60;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.PropertiesEdit.DisplayFormatString = "0";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "RE_VISITED";
                x.Caption = "Total Revisit";
                x.VisibleIndex = 30;
                x.Width = 60;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.PropertiesEdit.DisplayFormatString = "0";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "TOTMETTING";
                x.Caption = "Total Meeting";
                x.VisibleIndex = 31;
                x.Width = 60;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.PropertiesEdit.DisplayFormatString = "0";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "TOTALORDER";
                x.Caption = "Total Order";
                x.VisibleIndex = 32;
                x.Width = 100;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "TOTALDELV";
                x.Caption = "Total Delv.";
                x.VisibleIndex = 33;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "TOTALCOLLECTION";
                x.Caption = "Total Collection";
                x.VisibleIndex = 34;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "TC";
                x.Caption = "TC (Total call)";
                x.VisibleIndex = 35;
                x.Width = 100;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.PropertiesEdit.DisplayFormatString = "0";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "PC";
                x.Caption = "PC(Productive Call)";
                x.VisibleIndex = 36;
                x.Width = 100;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.PropertiesEdit.DisplayFormatString = "0";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "PRATIO";
                x.Caption = "Productive Ratio";
                x.VisibleIndex = 37;
                x.Width = 100;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "DROPSIZE";
                x.Caption = "Drop Size";
                x.VisibleIndex = 38;
                x.Width = 100;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "COVERAGE";
                x.Caption = "Coverage %";
                x.VisibleIndex = 39;
                x.Width = 100;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.PropertiesEdit.DisplayFormatString = "0.00";
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
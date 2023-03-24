#region======================================Revision History=========================================================================
//Written by : Debashis Talukder on 31/01/2023
//Module: Employee Performance Analytics
//1.0   V2.0.39     Debashis    14/02/2023      While exporting the Performance Analytics report, the distance column is showing a
//                                              $ sign.Now solved.Refer: 0025672
#endregion===================================End of Revision History==================================================================
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
using DocumentFormat.OpenXml.Drawing;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class PerformanceAnalyticsController : Controller
    {
        // GET: MYSHOP/PerformanceAnalytics
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
                DataTable dt = objgps.GetStateMandatory(Session["userid"].ToString());
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
        public ActionResult GetSalesSummaryList1()
        {
            return PartialView("PartialGetSalesSummaryList", GetPerformanceAnalySummary("0"));
        }
        public ActionResult GetSalesParformanceAnalyList(SalesSummaryReport model)
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
                if (model.IsRevisitContactDetails != null)
                {
                    TempData["IsRevisitContactDetails"] = model.IsRevisitContactDetails;
                    TempData.Keep();
                }
                double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;

                if (days <= 35)
                {
                    dt = objgps.GetPerformanceAnalyReport(datfrmat, dattoat, Userid, state, desig, empcode, model.IsRevisitContactDetails);
                }

                return PartialView("PartialGetPerformanceAnalytics", GetPerformanceAnalySummary(frmdate));

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

        public IEnumerable GetPerformanceAnalySummary(string frmdate)
        {
            DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "PERFORMANCE ANALYTICS");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            }

            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);

            if (frmdate != "Ispageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSEMPLOYEEPERFORMANCEANALYTICS_REPORTs
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSEMPLOYEEPERFORMANCEANALYTICS_REPORTs
                        where d.USERID == Convert.ToInt32(Userid) && d.EMPCODE == "0"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
        }

        public ActionResult ExporPerformanceAnalyList(int type)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(), GetPerformanceAnalySummary(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(), GetPerformanceAnalySummary(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(), GetPerformanceAnalySummary(""));
                case 4:
                    return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(), GetPerformanceAnalySummary(""));
                case 5:
                    return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(), GetPerformanceAnalySummary(""));
                //break;

                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetEmployeeBatchGridViewSettings()
        {
            DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "PERFORMANCE ANALYTICS");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            }
            var settings = new GridViewSettings();
            settings.Name = "Performance Analytics";
            settings.CallbackRouteValues = new { Controller = "PerformanceAnalytics", Action = "GetSalesParformanceAnalyList" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Performance_Analytics";

            if (TempData["IsRevisitContactDetails"].ToString() == "0")
            {
                settings.Columns.Add(x =>
                {
                    x.FieldName = "SEQ";
                    x.Caption = "Serial";
                    x.VisibleIndex = 1;
                    x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                    x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SEQ'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 60;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 60;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "EMPID";
                    x.Caption = "Emp. ID";
                    x.VisibleIndex = 2;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='EMPID'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "EMPNAME";
                    x.Caption = "Emp. Name";
                    x.VisibleIndex = 3;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='EMPNAME'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "DESIGNATION";
                    x.Caption = "Category";
                    x.VisibleIndex = 4;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DESIGNATION'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 120;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 120;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "DATEOFJOINING";
                    x.Caption = "DOJ";
                    x.VisibleIndex = 5;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DATEOFJOINING'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "REPORTTO";
                    x.Caption = "ASM Reporting";
                    x.VisibleIndex = 6;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='REPORTTO'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 120;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 120;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "HREPORTTO";
                    x.Caption = "ZM Reporting";
                    x.VisibleIndex = 7;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='HREPORTTO'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 120;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 120;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "LOGIN_DATE";
                    x.Caption = "Date";
                    x.VisibleIndex = 8;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='LOGIN_DATE'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "LOGGEDIN";
                    x.Caption = "Login Time";
                    x.VisibleIndex = 9;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='LOGGEDIN'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "LOGINLOATION";
                    x.Caption = "Login Location";
                    x.VisibleIndex = 10;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='LOGINLOATION'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 200;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 200;
                    }

                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "LOGEDOUT";
                    x.Caption = "Logout Time";
                    x.VisibleIndex = 11;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='LOGEDOUT'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "LOGOUTLOATION";
                    x.Caption = "Logout Location";
                    x.VisibleIndex = 12;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='LOGOUTLOATION'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 200;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 200;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "ATTEN_STATUS";
                    x.Caption = "Attendance";
                    x.VisibleIndex = 13;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ATTEN_STATUS'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "WORK_LEAVE_TYPE";
                    x.Caption = "Work/Leave Type";
                    x.VisibleIndex = 14;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='WORK_LEAVE_TYPE'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "REMARKS";
                    x.Caption = "Remarks";
                    x.VisibleIndex = 15;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='REMARKS'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "MDD_YESNO";
                    x.Caption = "Login from MDD (Yes/No)";
                    x.VisibleIndex = 16;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PP_NAME'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 120;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 120;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "SHOP_TYPE";
                    x.Caption = "Visit Type (Shop/MDD)";
                    x.VisibleIndex = 17;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOP_TYPE'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 120;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 120;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "VISITED_TIME";
                    x.Caption = "Visit/Revisit Timing";
                    x.VisibleIndex = 18;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='VISITED_TIME'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "DD_NAME";
                    x.Caption = "MDD Name";
                    x.VisibleIndex = 19;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DD_NAME'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 120;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 120;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "DD_CODE";
                    x.Caption = "MDD Code";
                    x.VisibleIndex = 20;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DD_CODE'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 120;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 120;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "DDADDR_CONTACT";
                    x.Caption = "MDD Address & Mobile no.";
                    x.VisibleIndex = 21;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DDADDR_CONTACT'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 200;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 200;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "SHOP_NAME";
                    x.Caption = "Outlet Name";
                    x.VisibleIndex = 22;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOP_NAME'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 120;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 120;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "OUTLET_TYPE";
                    x.Caption = "Outlet Type";
                    x.VisibleIndex = 23;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='OUTLET_TYPE'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 120;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 120;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "ENTITYCODE";
                    x.Caption = "Outlet Code";
                    x.VisibleIndex = 24;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ENTITYCODE'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 120;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 120;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "SHOPADDR_CONTACT";
                    x.Caption = "Outlet Address & Mobile no.";
                    x.VisibleIndex = 25;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOPADDR_CONTACT'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 200;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 200;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "MANNINGSTATUS";
                    x.Caption = "Status (Manning)";
                    x.VisibleIndex = 26;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='MANNINGSTATUS'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "TOTAL_VISIT";
                    x.Caption = "Total Visit Count";
                    x.VisibleIndex = 27;
                    x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                    x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='TOTAL_VISIT'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 70;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 70;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "RE_VISITED";
                    x.Caption = "Re Visit Count";
                    x.VisibleIndex = 28;
                    x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                    x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='RE_VISITED'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 60;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 60;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "NEWSHOP_VISITED";
                    x.Caption = "New Visit Count";
                    x.VisibleIndex = 29;
                    x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                    x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='NEWSHOP_VISITED'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 60;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 60;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "SPENT_DURATION";
                    x.Caption = "Duration Spend";
                    x.VisibleIndex = 30;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SPENT_DURATION'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "DISTANCE_TRAVELLED";
                    x.Caption = "Travelled(KM)";
                    x.VisibleIndex = 31;
                    //Rev 1.0 Mantis: 0025672
                    x.PropertiesEdit.DisplayFormatString = "0.00";
                    //End of Rev 1.0 Mantis: 0025672
                    x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                    x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DISTANCE_TRAVELLED'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 100;
                    }
                });
            }
            if (TempData["IsRevisitContactDetails"].ToString() == "1")
            {
                settings.Columns.Add(x =>
                {
                    x.FieldName = "SEQ";
                    x.Caption = "Serial";
                    x.VisibleIndex = 1;
                    x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                    x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SEQ'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 60;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 60;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "EMPID";
                    x.Caption = "Emp. ID";
                    x.VisibleIndex = 2;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='EMPID'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "EMPNAME";
                    x.Caption = "Emp. Name";
                    x.VisibleIndex = 3;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='EMPNAME'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "DESIGNATION";
                    x.Caption = "Category";
                    x.VisibleIndex = 4;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DESIGNATION'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 120;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 120;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "DATEOFJOINING";
                    x.Caption = "DOJ";
                    x.VisibleIndex = 5;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DATEOFJOINING'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "REPORTTO";
                    x.Caption = "ASM Reporting";
                    x.VisibleIndex = 6;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='REPORTTO'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 120;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 120;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "HREPORTTO";
                    x.Caption = "ZM Reporting";
                    x.VisibleIndex = 7;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='HREPORTTO'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 120;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 120;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "LOGIN_DATE";
                    x.Caption = "Date";
                    x.VisibleIndex = 8;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='LOGIN_DATE'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "LOGGEDIN";
                    x.Caption = "Login Time";
                    x.VisibleIndex = 9;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='LOGGEDIN'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "LOGINLOATION";
                    x.Caption = "Login Location";
                    x.VisibleIndex = 10;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='LOGINLOATION'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 200;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 200;
                    }

                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "LOGEDOUT";
                    x.Caption = "Logout Time";
                    x.VisibleIndex = 11;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='LOGEDOUT'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "LOGOUTLOATION";
                    x.Caption = "Logout Location";
                    x.VisibleIndex = 12;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='LOGOUTLOATION'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 200;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 200;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "ATTEN_STATUS";
                    x.Caption = "Attendance";
                    x.VisibleIndex = 13;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ATTEN_STATUS'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "WORK_LEAVE_TYPE";
                    x.Caption = "Work/Leave Type";
                    x.VisibleIndex = 14;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='WORK_LEAVE_TYPE'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "REMARKS";
                    x.Caption = "Remarks";
                    x.VisibleIndex = 15;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='REMARKS'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "MDD_YESNO";
                    x.Caption = "Login from MDD (Yes/No)";
                    x.VisibleIndex = 16;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PP_NAME'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 120;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 120;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "SHOP_TYPE";
                    x.Caption = "Visit Type (Shop/MDD)";
                    x.VisibleIndex = 17;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOP_TYPE'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 120;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 120;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "VISITED_TIME";
                    x.Caption = "Visit/Revisit Timing";
                    x.VisibleIndex = 18;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='VISITED_TIME'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "DD_NAME";
                    x.Caption = "MDD Name";
                    x.VisibleIndex = 19;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DD_NAME'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 120;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 120;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "DD_CODE";
                    x.Caption = "MDD Code";
                    x.VisibleIndex = 20;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DD_CODE'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 120;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 120;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "DDADDR_CONTACT";
                    x.Caption = "MDD Address & Mobile no.";
                    x.VisibleIndex = 21;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DDADDR_CONTACT'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 200;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 200;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "SHOP_NAME";
                    x.Caption = "Outlet Name";
                    x.VisibleIndex = 22;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOP_NAME'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 120;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 120;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "OUTLET_TYPE";
                    x.Caption = "Outlet Type";
                    x.VisibleIndex = 23;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='OUTLET_TYPE'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 120;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 120;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "ENTITYCODE";
                    x.Caption = "Outlet Code";
                    x.VisibleIndex = 24;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ENTITYCODE'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 120;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 120;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "SHOPADDR_CONTACT";
                    x.Caption = "Outlet Address & Mobile no.";
                    x.VisibleIndex = 25;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOPADDR_CONTACT'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 200;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 200;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "MANNINGSTATUS";
                    x.Caption = "Status (Manning)";
                    x.VisibleIndex = 26;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='MANNINGSTATUS'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "MULTI_CONTACT_NAME";
                    x.Caption = "Contact Name";
                    x.VisibleIndex = 27;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='MULTI_CONTACT_NAME'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                            x.Width = 200;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 200;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 200;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "MULTI_CONTACT_NUMBER";
                    x.Caption = "Contact Number";
                    x.VisibleIndex = 28;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='MULTI_CONTACT_NUMBER'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 200;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 200;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "TOTAL_VISIT";
                    x.Caption = "Total Visit Count";
                    x.VisibleIndex = 29;
                    x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                    x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='TOTAL_VISIT'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 70;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 70;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "RE_VISITED";
                    x.Caption = "Re Visit Count";
                    x.VisibleIndex = 30;
                    x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                    x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='RE_VISITED'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 60;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 60;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "NEWSHOP_VISITED";
                    x.Caption = "New Visit Count";
                    x.VisibleIndex = 31;
                    x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                    x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='NEWSHOP_VISITED'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 60;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 60;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "SPENT_DURATION";
                    x.Caption = "Duration Spend";
                    x.VisibleIndex = 32;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SPENT_DURATION'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "DISTANCE_TRAVELLED";
                    x.Caption = "Travelled(KM)";
                    x.VisibleIndex = 33;
                    //Rev 1.0 Mantis: 0025672
                    x.PropertiesEdit.DisplayFormatString = "0.00";
                    //End of Rev 1.0 Mantis: 0025672
                    x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                    x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DISTANCE_TRAVELLED'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 100;
                    }
                });
            }

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
                int k = objgps.InsertPageRetention(Col, Session["userid"].ToString(), "PERFORMANCE ANALYTICS");

                return Json(k, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
    }
}
using BusinessLogicLayer.SalesTrackerReports;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using Models;
using MyShop.Models;
using SalesmanTrack;
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
    public class GPSStatisticsReportController : Controller
    {
        //
        // GET: /MYSHOP/GPSStatisticsReport/
        UserList lstuser = new UserList();
        SalesSummary_Report objgps = new SalesSummary_Report();
        public ActionResult GPSStatisticsReport()
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

        public ActionResult GetGPSStatisticsList(SalesSummaryReport model)
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
                //double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;

                //if (days <= 35)
                //{
                //    dt = objgps.GetSalesPerformanceReport(datfrmat, dattoat, Userid, state, desig, empcode);
                //}
                dt = objgps.GetGPSStatsReport(datfrmat, dattoat, Userid, state, desig, empcode);
                return PartialView("PartialGetGPSStatistics", GetGPSStatistics(frmdate));


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

        public IEnumerable GetGPSStatistics(string frmdate)
        {
            DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "GPS Statistics");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            }

            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);

            if (frmdate != "Ispageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSGPSSTATISTICS_REPORTs
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSGPSSTATISTICS_REPORTs
                        where d.USERID == Convert.ToInt32(Userid) && d.EMPCODE == "0"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }



        }

        public ActionResult ExportGPSStatisticsList(int type)
        {


            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(), GetGPSStatistics(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(), GetGPSStatistics(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(), GetGPSStatistics(""));
                case 4:
                    return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(), GetGPSStatistics(""));
                case 5:
                    return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(), GetGPSStatistics(""));
                //break;

                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetEmployeeBatchGridViewSettings()
        {
            DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "GPS Statistics");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;
            }
            var settings = new GridViewSettings();
            settings.Name = "GPS Statistics Report";
            settings.CallbackRouteValues = new { Controller = "GPSStatisticsReport", Action = "GetGPSStatisticsList" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "GPS Statistics Report";


            settings.Columns.Add(x =>
            {
                x.FieldName = "EMPNAME";
                x.Caption = "Name";
                x.VisibleIndex = 1;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='EMPNAME'");
                    if (row != null && row.Length > 0)  
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                    }
                }
                else
                {
                    x.Visible = true;
                }

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CONTACTNO";
                x.Caption = "Contact Number";
                x.VisibleIndex = 2;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACTNO'");
                    if (row != null && row.Length > 0)  
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                    }
                }
                else
                {
                    x.Visible = true;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "DESIGNATION";
                x.Caption = "Designation";
                x.VisibleIndex = 3;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DESIGNATION'");
                    if (row != null && row.Length > 0)  
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                    }
                }
                else
                {
                    x.Visible = true;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "REPORTTO";
                x.Caption = "Supervisor";
                x.VisibleIndex = 4;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='REPORTTO'");
                    if (row != null && row.Length > 0)  
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                    }
                }
                else
                {
                    x.Visible = true;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "GPsDate";
                x.Caption = "Date";
                x.VisibleIndex = 5;
                x.Width = 100;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='GPsDate'");
                    if (row != null && row.Length > 0) 
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                    }
                }
                else
                {
                    x.Visible = true;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Gps_OffTime";
                x.Caption = "GPS Off Time";
                x.VisibleIndex = 6;
                x.Width = 120;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Gps_OffTime'");
                    if (row != null && row.Length > 0)  
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                    }
                }
                else
                {
                    x.Visible = true;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Gps_on_Time";
                x.Caption = "GPS On Time";
                x.VisibleIndex = 7;
                x.Width = 120;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Gps_on_Time'");
                    if (row != null && row.Length > 0)  
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                    }
                }
                else
                {
                    x.Visible = true;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Duration";
                x.Caption = "Duration (HH-MM-SS)";
                x.VisibleIndex = 8;
                x.Width = 180;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Duration'");
                    if (row != null && row.Length > 0)  
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                    }
                }
                else
                {
                    x.Visible = true;
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
                int k = objgps.InsertPageRetention(Col, Session["userid"].ToString(), "GPS Statistics");

                return Json(k, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
	}
}
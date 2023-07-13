#region======================================Revision History=========================================================================
//1.0   V2.0.41    Debashis     13/07/2023      A New Report Required for ITC under Report > MIS > Outlet Revisit Details
//                                              And Parameter will be same as 'DS Visit Details' report.Refer: 0026473
#endregion===================================End of Revision History==================================================================

using BusinessLogicLayer.SalesTrackerReports;
using DataAccessLayer;
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
    public class OutletRevisitDetailsController : Controller
    {
        // GET: MYSHOP/OutletRevisitDetails
        SalesSummary_Report objgps = new SalesSummary_Report();
        public ActionResult OutletRevisitDetails()
        {
            try
            {
                UserList lstuser = new UserList();
                OutletRevisitDetailsReport omodel = new OutletRevisitDetailsReport();
                string userid = Session["userid"].ToString();
                omodel.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                omodel.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                DataTable dtbranch = lstuser.GetHeadBranchList(Convert.ToString(Session["userbranchHierarchy"]), "HO");
                DataTable dtBranchChild = new DataTable();
                if (dtbranch.Rows.Count > 0)
                {
                    dtBranchChild = lstuser.GetChildBranch(Convert.ToString(Session["userbranchHierarchy"]));
                    if (dtBranchChild.Rows.Count > 0)
                    {
                        DataRow dr;
                        dr = dtbranch.NewRow();
                        dr[0] = 0;
                        dr[1] = "All";
                        dtbranch.Rows.Add(dr);
                        dtbranch.DefaultView.Sort = "BRANCH_ID ASC";
                        dtbranch = dtbranch.DefaultView.ToTable();
                    }
                }
                omodel.modelbranch = APIHelperMethods.ToModelList<GetBranch>(dtbranch);
                string h_id = omodel.modelbranch.First().BRANCH_ID.ToString();
                ViewBag.h_id = h_id;
                return View(omodel);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetOutletRevisitDetailsList(OutletRevisitDetailsReport model)
        {
            try
            {
                String weburl = System.Configuration.ConfigurationManager.AppSettings["SiteURL"];
                List<OutletRVDetailsModel> omel = new List<OutletRVDetailsModel>();

                DataTable dt = new DataTable();

                string Employee = "";
                int i = 1;

                if (model.empcode != null && model.empcode.Count > 0)
                {
                    foreach (string item in model.empcode)
                    {
                        if (i > 1)
                            Employee = Employee + "," + item;
                        else
                            Employee = item;
                        i++;
                    }
                }

                string Branch_Id = "";
                int j = 1;
                if (model.BranchId != null && model.BranchId.Count > 0)
                {
                    foreach (string item in model.BranchId)
                    {
                        if (j > 1)
                            Branch_Id = Branch_Id + "," + item;
                        else
                            Branch_Id = item;
                        j++;
                    }
                }

                string Is_PageLoad = string.Empty;

                if (model.is_pageload == "0")
                {
                    Is_PageLoad = "is_pageload";
                    model.Fromdate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                    model.Todate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                }

                if (model.Fromdate == null)
                {
                    model.Fromdate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                }
                else
                {
                    if (model.is_pageload == "0")
                    {
                        model.Fromdate = model.Fromdate.Split('-')[0] + '-' + model.Fromdate.Split('-')[1] + '-' + model.Fromdate.Split('-')[2];
                    }
                    else
                    {
                        model.Fromdate = model.Fromdate.Split('-')[2] + '-' + model.Fromdate.Split('-')[1] + '-' + model.Fromdate.Split('-')[0];
                    }
                }

                if (model.Todate == null)
                {
                    model.Todate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                }
                else
                {
                    if (model.is_pageload == "0")
                    {
                        model.Todate = model.Todate.Split('-')[0] + '-' + model.Todate.Split('-')[1] + '-' + model.Todate.Split('-')[2];
                    }
                    else
                    {
                        model.Todate = model.Todate.Split('-')[2] + '-' + model.Todate.Split('-')[1] + '-' + model.Todate.Split('-')[0];
                    }
                }

                string FromDate = model.Fromdate;
                string ToDate = model.Todate;

                if (model.is_pageload == "1")
                {

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        omel = APIHelperMethods.ToModelList<OutletRVDetailsModel>(dt);
                        TempData["ExportOutletRevisitDetails"] = omel;
                        TempData.Keep();
                    }
                    else
                    {
                        TempData["ExportOutletRevisitDetails"] = null;
                        TempData.Keep();
                    }

                }
                //if (model.is_pageload == "1")
                //{
                    double days = (Convert.ToDateTime(ToDate) - Convert.ToDateTime(FromDate)).TotalDays;
                    if (days <= 1)
                    {
                        dt = GetOutletRevisitDetails(Employee, FromDate, ToDate, Branch_Id,model.is_pageload);
                    }
                //}

                return PartialView("_PartialGridOutletRevisitDetails", LGetOutletRevisitDetails(Is_PageLoad));
            }
            catch
            {
                //   return Redirect("~/OMS/Signoff.aspx");
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }

        }

        public DataTable GetOutletRevisitDetails(string Employee, string start_date, string end_date, string Branch_Id,string IsPageLoad)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSOUTLETREVISITDETAILS_REPORT");
            proc.AddPara("@FROMDATE", start_date);
            proc.AddPara("@TODATE", end_date);
            proc.AddPara("@BRANCHID", Branch_Id);
            proc.AddPara("@EMPID", Employee);
            proc.AddPara("@ISPAGELOAD", IsPageLoad);
            proc.AddPara("@USERID", Convert.ToInt32(Session["userid"]));
            ds = proc.GetTable();
            return ds;
        }

        public IEnumerable LGetOutletRevisitDetails(string Is_PageLoad)
        {
            DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "Outlet Revisit Details");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;
            }
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);

            if (Is_PageLoad != "is_pageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSOUTLETREVISITDETAILS_REPORTs
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSOUTLETREVISITDETAILS_REPORTs
                        where d.USERID == Convert.ToInt32(Userid) && d.EMPCODE == "0"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
        }

        public ActionResult ExportOutletRevisitDetailsList(int type)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetDSSummaryGridViewSettings(), LGetOutletRevisitDetails(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetDSSummaryGridViewSettings(), LGetOutletRevisitDetails(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetDSSummaryGridViewSettings(), LGetOutletRevisitDetails(""));
                case 4:
                    return GridViewExtension.ExportToRtf(GetDSSummaryGridViewSettings(), LGetOutletRevisitDetails(""));
                case 5:
                    return GridViewExtension.ExportToCsv(GetDSSummaryGridViewSettings(), LGetOutletRevisitDetails(""));
                //break;

                default:
                    break;
            }

            return null;
        }
        private GridViewSettings GetDSSummaryGridViewSettings()
        {
            DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "Outlet Revisit Details");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;
            }
            var settings = new GridViewSettings();
            settings.Name = "OutletRevisitDetails";
            settings.CallbackRouteValues = new { Controller = "OutletRevisitDetails", Action = "GetOutletRevisitDetailsList" };

            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "OutletRevisitDetails";

            settings.Columns.Add(x =>
            {
                x.FieldName = "BRANCHDESC";
                x.Caption = "Branch";
                x.VisibleIndex = 1;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='BRANCHDESC'");
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
                x.FieldName = "HREPORTTOUID";
                x.Caption = "AE ID";
                x.VisibleIndex = 2;
                x.PropertiesEdit.DisplayFormatString = "0.00";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='HREPORTTOUID'");
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

                x.FieldName = "REPORTTOUID";
                x.Caption = "WD ID";
                x.VisibleIndex = 3;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='REPORTTOUID'");
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
                x.FieldName = "EMPID";
                x.Caption = "DS ID";
                x.VisibleIndex = 4;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='EMPID'");
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
                x.FieldName = "EMPNAME";
                x.Caption = "DS Name";
                x.VisibleIndex = 5;
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
                x.FieldName = "VISITEDDATE";
                x.Caption = "ReVisit Date";
                x.VisibleIndex = 6;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='VISITEDDATE'");
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
                x.FieldName = "VISITEDTIME";
                x.Caption = "Revisit Time";
                x.VisibleIndex = 7;

                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='VISITEDTIME'");
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
                x.FieldName = "OUTLETID";
                x.Caption = "Outlet ID";
                x.VisibleIndex = 8;

                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='OUTLETID'");
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
                x.FieldName = "OUTLETNAME";
                x.Caption = "Outlet Name";
                x.VisibleIndex = 9;

                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='OUTLETNAME'");
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
                x.FieldName = "LATITUDE";
                x.Caption = "Latitude";
                x.VisibleIndex = 10;

                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='LATITUDE'");
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
                x.FieldName = "LONGITUDE";
                x.Caption = "Longitude";
                x.VisibleIndex = 11;

                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='LONGITUDE'");
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
                int k = objgps.InsertPageRetention(Col, Session["userid"].ToString(), "OUTLET REVISIT DETAILS");

                return Json(k, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
    }
}
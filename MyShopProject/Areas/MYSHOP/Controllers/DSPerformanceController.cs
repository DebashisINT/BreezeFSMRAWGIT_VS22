#region======================================Revision History===================================================================================================
//1.0   V2 .0.41    Debashis   09/08/2023      A coloumn named as Gender needs to be added in all the ITC reports.Refer: 0026680
//2.0   V2.0.45     Debashis   12/04/2024      The above mentioned two DS types need to be considered in the below reports.Refer: 0027360
//3.0   V2.0.47    Debashis    03/06/2024      A new coloumn shall be added in the below mentioned reports.Refer: 0027402
//4.0   V2.0.47    Debashis    03/06/2024      The respective Sales Value coloumn in the below mentioned reports shall be replaced with “Delivery value”.Refer: 0027499
//5.0   V2.0.47    Debashis    11/06/2024      Add a new column at the end named as “Total CDM Days" in selected date range.Refer: 0027508
#endregion===================================End of Revision History============================================================================================

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
    public class DSPerformanceController : Controller
    {
        //
        // GET: /MYSHOP/DSPerformance/
        SalesSummary_Report objgps = new SalesSummary_Report();
        public ActionResult DSPerformance()
        {
            try
            {
                UserList lstuser = new UserList();
                DSPerformanceReport omodel = new DSPerformanceReport();
                string userid = Session["userid"].ToString();
                omodel.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                omodel.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                DataTable dtbranch = lstuser.GetHeadBranchList(Convert.ToString(Session["userbranchHierarchy"]), "HO");
                DataTable dtBranchChild = new DataTable();
                if (dtbranch.Rows.Count > 0)
                {
                    dtBranchChild = lstuser.GetChildBranch(Convert.ToString(Session["userbranchHierarchy"]));
                    //Rev Debashis 0025198
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
                    //End of Rev Debashis 0025198
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
        public ActionResult GetDSPerformanceList(DSPerformanceReport model)
        {
            try
            {
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
                List<DSPerformanceListModel> omel = new List<DSPerformanceListModel>();
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
                    //Rev Debashis 0024715 (While page load is happen the From date format needs to 'yyyy-MM-dd' but while report generate is happen the From date format needs to 'dd-MM-yyyy'. So is_pageload checking is implemented.)
                    //model.Fromdate = model.Fromdate.Split('-')[2] + '-' + model.Fromdate.Split('-')[1] + '-' + model.Fromdate.Split('-')[0];
                    if (model.is_pageload == "0")
                    {
                        model.Fromdate = model.Fromdate.Split('-')[0] + '-' + model.Fromdate.Split('-')[1] + '-' + model.Fromdate.Split('-')[2];
                    }
                    else
                    {
                        model.Fromdate = model.Fromdate.Split('-')[2] + '-' + model.Fromdate.Split('-')[1] + '-' + model.Fromdate.Split('-')[0];
                    }
                    //End of Rev Debashis 0024715
                }

                if (model.Todate == null)
                {
                    model.Todate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                }
                else
                {
                    //Rev Debashis 0024715 (While page load is happen the To date format needs to 'yyyy-MM-dd' but while report generate is happen the To date format needs to 'dd-MM-yyyy'. So is_pageload checking is implemented.)
                    //model.Todate = model.Todate.Split('-')[2] + '-' + model.Todate.Split('-')[1] + '-' + model.Todate.Split('-')[0];
                    if (model.is_pageload == "0")
                    {
                        model.Todate = model.Todate.Split('-')[0] + '-' + model.Todate.Split('-')[1] + '-' + model.Todate.Split('-')[2];
                    }
                    else
                    {
                        model.Todate = model.Todate.Split('-')[2] + '-' + model.Todate.Split('-')[1] + '-' + model.Todate.Split('-')[0];
                    }
                    //End of Rev Debashis 0024715
                }

                string FromDate = model.Fromdate;
                string ToDate = model.Todate;

                if (model.is_pageload == "1")
                {
                    //Mantis Issue 24728
                    //double days = (Convert.ToDateTime(model.Todate) - Convert.ToDateTime(model.Fromdate)).TotalDays;
                    //End of Mantis Issue 24728
                    //dt = GetDSPerformance(Employee, FromDate, ToDate, Branch_Id);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        omel = APIHelperMethods.ToModelList<DSPerformanceListModel>(dt);
                        TempData["ExportDSPerformance"] = omel;
                        TempData.Keep();
                    }
                    else
                    {
                        TempData["ExportDSPerformance"] = null;
                        TempData.Keep();
                    }
                    // return PartialView("_PartialGridEmployeeOutletMaster", omel);
                    //return PartialView("_PartialGridDSPerformance", LDSPerformance(Is_PageLoad));
                }
                //else
                //{
                //    // return PartialView("_PartialGridEmployeeOutletMaster", omel);
                //    return PartialView("_PartialGridDSPerformance", LDSPerformance(Is_PageLoad));
                //}
                //Mantis Issue 24728
                //Mantis Issue 24791
                //double days = (Convert.ToDateTime(ToDate) - Convert.ToDateTime(FromDate)).TotalDays;
                //if (days <= 35)
                //{
                //    dt = GetDSPerformance(Employee, FromDate, ToDate, Branch_Id);
                //}
                if (model.is_pageload == "1")
                {
                    double days = (Convert.ToDateTime(ToDate) - Convert.ToDateTime(FromDate)).TotalDays;
                    if (days <= 35)
                    {
                        dt = GetDSPerformance(Employee, FromDate, ToDate, Branch_Id);
                    }
                }
                //End of Mantis Issue 24791
                //dt = GetDSPerformance(Employee, FromDate, ToDate, Branch_Id);
                //End of Mantis Issue 24728
                
                return PartialView("_PartialGridDSPerformance", LDSPerformance(Is_PageLoad));
            }
            catch
            {
                //   return Redirect("~/OMS/Signoff.aspx");
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }

        }
        public DataTable GetDSPerformance(string Employee, string start_date, string end_date, string Branch_Id)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSDSPERFORMANCESUMMARY_REPORT");
            proc.AddPara("@FROMDATE", start_date);
            proc.AddPara("@TODATE", end_date);
            proc.AddPara("@BRANCHID", Branch_Id);
            proc.AddPara("@EMPID", Employee);
            proc.AddPara("@USERID", Convert.ToInt32(Session["userid"]));
            ds = proc.GetTable();
            return ds;
        }
        public IEnumerable LDSPerformance(string Is_PageLoad)
        {
            DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "DS PERFORMANCE");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            }
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);

            if (Is_PageLoad != "is_pageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSDSPERFORMANCESUMMARY_REPORTs
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSDSPERFORMANCESUMMARY_REPORTs
                        where d.USERID == Convert.ToInt32(Userid) && d.EMPCODE == "0"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }



        }

        public ActionResult ExporDSPerformanceList(int type)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetDSPerformanceGridViewSettings(), LDSPerformance(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetDSPerformanceGridViewSettings(), LDSPerformance(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetDSPerformanceGridViewSettings(), LDSPerformance(""));
                case 4:
                    return GridViewExtension.ExportToRtf(GetDSPerformanceGridViewSettings(), LDSPerformance(""));
                case 5:
                    return GridViewExtension.ExportToCsv(GetDSPerformanceGridViewSettings(), LDSPerformance(""));
                //break;

                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetDSPerformanceGridViewSettings()
        {
            DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "DS PERFORMANCE");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            }
            var settings = new GridViewSettings();
            settings.Name = "DSPerformance";
            settings.CallbackRouteValues = new { Controller = "DSPerformance", Action = "GetDSPerformanceList" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "DS Performance Summary Report";

            //settings.Columns.Add(column =>
            //{
            //column.Caption = "Branch";
            //column.FieldName = "BRANCHDESC";

            //});
            settings.Columns.Add(x =>
            {
                x.FieldName = "BRANCHDESC";
                x.Caption = "Branch";
                x.VisibleIndex = 1;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='BRANCHDESC'");
                    if (row != null && row.Length > 0)  /// Check now
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

            //settings.Columns.Add(column =>
            //{
            //    column.Caption = "AE ID";
            //    column.FieldName = "HREPORTTO";
            //});
            settings.Columns.Add(x =>
            {
                //Rev Debashis 0024715
                //x.FieldName = "HREPORTTO";
                x.FieldName = "HREPORTTOUID";
                //End of Rev Debashis 0024715
                x.Caption = "AE ID";
                x.VisibleIndex = 2;
                //rev Pratik
                x.PropertiesEdit.DisplayFormatString = "0.00";
                //End of rev Pratik
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
                    }
                }
                else
                {
                    x.Visible = true;
                }
            });

            //settings.Columns.Add(column =>
            //{
            //    column.Caption = "WD ID";
            //    column.FieldName = "REPORTTO";
            //});
            settings.Columns.Add(x =>
            {
                //Rev Debashis 0024715
                //x.FieldName = "REPORTTO";
                x.FieldName = "REPORTTOUID";
                //End of Rev Debashis 0024715
                x.Caption = "WD ID";
                x.VisibleIndex = 3;
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
                    }
                }
                else
                {
                    x.Visible = true;
                }
            });


            //settings.Columns.Add(column =>
            //{
            //    column.Caption = "DS ID";
            //    column.FieldName = "EMPID";
            //});
            settings.Columns.Add(x =>
            {
                x.FieldName = "EMPID";
                //Rev Debashis 0024715
                //x.Caption = "DS ID";
                //Rev 2.0 Mantis: 0027360
                //x.Caption = "DS/TL ID";
                x.Caption = "DS ID";
                //End of Rev 2.0 Mantis: 0027360
                //End of Rev Debashis 0024715
                x.VisibleIndex = 4;
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
                    }
                }
                else
                {
                    x.Visible = true;
                }
            });

            //settings.Columns.Add(column =>
            //{
            //    column.Caption = "DS Name";
            //    column.FieldName = "EMPNAME";
            //});
            settings.Columns.Add(x =>
            {
                x.FieldName = "EMPNAME";
                //Rev Debashis 0024715
                //x.Caption = "DS Name";
                //Rev 2.0 Mantis: 0027360
                //x.Caption = "DS/TL Name";
                x.Caption = "DS Name";
                //End of Rev 2.0 Mantis: 0027360
                //End of Rev Debashis 0024715
                x.VisibleIndex = 5;
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
                    }
                }
                else
                {
                    x.Visible = true;
                }
            });

            //settings.Columns.Add(column =>
            //{
            //    column.Caption = "Outlet ID";
            //    column.FieldName = "OUTLETID";
            //});
            //Rev 1.0 Mantis: 0026680
            settings.Columns.Add(x =>
            {
                x.FieldName = "GENDERDESC";
                x.Caption = "Gender";
                x.VisibleIndex = 6;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='GENDERDESC'");
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
            //End of Rev 1.0 Mantis: 0026680

            settings.Columns.Add(x =>
            {
                x.FieldName = "DATERANGE";
                x.Caption = "From-To Date";
                x.VisibleIndex = 7;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DATERANGE'");
                    if (row != null && row.Length > 0)  /// Check now
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

            //settings.Columns.Add(column =>
            //{
            //    column.Caption = "Outlet Name";
            //    column.FieldName = "OUTLETNAME";
            //});
            settings.Columns.Add(x =>
            {
                x.FieldName = "DAYSPRESENT";
                x.Caption = "No. of Days Present";
                x.VisibleIndex = 8;
               
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DAYSPRESENT'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                        //x.Width = 120;
                    }
                    else
                    {
                        x.Visible = true;
                       // x.Width = 120;
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
                x.FieldName = "QUALIFYATTENDANCE";
                x.Caption = "Qualified Attendance";
                x.VisibleIndex = 9;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='QUALIFYATTENDANCE'");
                    if (row != null && row.Length > 0)  /// Check now
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

            //settings.Columns.Add(column =>
            //{
            //    column.Caption = "Outlet Address";
            //    column.FieldName = "OUTLETADDRESS";
            //});
            settings.Columns.Add(x =>
            {
                x.FieldName = "DAYSPOINTVISITED";
                x.Caption = "No.of Days Point Visited";
                x.VisibleIndex = 10;
                //rev Pratik
                x.PropertiesEdit.DisplayFormatString = "0.00";
                //End of rev Pratik
                //x.Width = 150;
                //x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DAYSPOINTVISITED'");
                    if (row != null && row.Length > 0)  /// Check now
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
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "NEWSHOP_VISITED";
                x.Caption = "Outlets Mapped( Added)";
                x.VisibleIndex = 11;
                //rev Pratik
                x.PropertiesEdit.DisplayFormatString = "0.00";
                //End of rev Pratik
                //x.Width = 120;
                //x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
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
                        
                    }
                }
                else
                {
                    x.Visible = true;
                    
                }
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "RE_VISITED";
                x.Caption = "Outlets Re-Visited";
                x.VisibleIndex = 12;
                //rev Pratik
                x.PropertiesEdit.DisplayFormatString = "0.00";
                //End of rev Pratik
                //x.Width = 120;
                //x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
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
                        
                    }
                }
                else
                {
                    x.Visible = true;
                    
                }
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "TOTAL_VISIT";
                x.Caption = "Total Outlets Visited";
                x.VisibleIndex = 13;
                //rev Pratik
                x.PropertiesEdit.DisplayFormatString = "0.00";
                //End of rev Pratik
                //x.Width = 180;
                // x.Width = System.Web.UI.WebControls.Unit.Percentage(20);
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
                        
                    }
                }
                else
                {
                    x.Visible = true;
                    
                }

            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "DISTANCE_TRAVELLED";
                x.Caption = "Distance Travelled(Km.Mtr)";
                x.VisibleIndex = 14;
                //rev Pratik
                x.PropertiesEdit.DisplayFormatString = "0.00";
                //End of rev Pratik
                //x.Width = 180;
                //x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
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
                        
                    }
                }
                else
                {
                    x.Visible = true;
                    
                }
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "AVGTIMESPENTINMARKET";
                x.Caption = "Avg time spent in the market(HH:MM)";
                x.VisibleIndex = 15;
                //x.Width = 220;
                //x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='AVGTIMESPENTINMARKET'");
                    if (row != null && row.Length > 0)  /// Check now
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
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "AVGSPENTDURATION";
                x.Caption = "Avg time spent in OL(CFT-Customer Facing Time)(HH:MM)";
                x.VisibleIndex = 16;
                //x.Width = 320;
                //x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='AVGSPENTDURATION'");
                    if (row != null && row.Length > 0)  /// Check now
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
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "SALE_VALUE";
                //Rev 4.0 Mantis: 0027402
                //x.Caption = "Total Sales Value";
                x.Caption = "Total Delivery Value";
                //End of Rev 4.0 Mantis: 0027402
                x.VisibleIndex = 17;
                //rev Pratik
                x.PropertiesEdit.DisplayFormatString = "0.00";
                //End of rev Pratik
                //x.Width = 180;
                // x.Width = System.Web.UI.WebControls.Unit.Percentage(20);
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SALE_VALUE'");
                    if (row != null && row.Length > 0)  /// Check now
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
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            //Rev 5.0 Mantis: 0027508
            settings.Columns.Add(x =>
            {
                x.FieldName = "TOTALCDMDAYS";
                x.Caption = "Total CDM Days";
                x.VisibleIndex = 18;
                x.PropertiesEdit.DisplayFormatString = "0";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='TOTALCDMDAYS'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 180;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 180;
                }
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });
            //End of Rev 5.0 Mantis: 0027508

            //Rev 3.0 Mantis: 0027402
            settings.Columns.Add(x =>
            {
                x.FieldName = "ORDER_VALUE";
                x.Caption = "Total Order Value";
                x.VisibleIndex = 19;
                x.PropertiesEdit.DisplayFormatString = "0.00";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ORDER_VALUE'");
                    if (row != null && row.Length > 0)  /// Check now
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
            //End of Rev 3.0 Mantis: 0027402
            
            //Rev Pratik
            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "USERID";
            //    x.Caption = "User Id";
            //    x.VisibleIndex = 17;
            //    //x.Width = 180;
            //    // x.Width = System.Web.UI.WebControls.Unit.Percentage(20);
            //    if (ViewBag.RetentionColumn != null)
            //    {
            //        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='USERID'");
            //        if (row != null && row.Length > 0)  /// Check now
            //        {
            //            x.Visible = false;
            //        }
            //        else
            //        {
            //            x.Visible = true;
            //        }
            //    }
            //    else
            //    {
            //        x.Visible = true;
            //    }

            //});
            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "SEQ";
            //    x.Caption = "SEQ";
            //    x.VisibleIndex = 18;
            //    if (ViewBag.RetentionColumn != null)
            //    {
            //        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SEQ'");
            //        if (row != null && row.Length > 0)  /// Check now
            //        {
            //            x.Visible = false;
            //        }
            //        else
            //        {
            //            x.Visible = true;
            //        }
            //    }
            //    else
            //    {
            //        x.Visible = true;
            //    }

            //});
            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "BRANCH_ID";
            //    x.Caption = "BRANCH ID";
            //    x.VisibleIndex = 19;
            //    //x.Width = 180;
            //    // x.Width = System.Web.UI.WebControls.Unit.Percentage(20);
            //    if (ViewBag.RetentionColumn != null)
            //    {
            //        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='BRANCH_ID'");
            //        if (row != null && row.Length > 0)  /// Check now
            //        {
            //            x.Visible = false;
            //        }
            //        else
            //        {
            //            x.Visible = true;
            //        }
            //    }
            //    else
            //    {
            //        x.Visible = true;
            //    }

            //});
            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "EMPCODE";
            //    x.Caption = "Employee Code";
            //    x.VisibleIndex = 20;
            //    //x.Width = 180;
            //    // x.Width = System.Web.UI.WebControls.Unit.Percentage(20);
            //    if (ViewBag.RetentionColumn != null)
            //    {
            //        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='EMPCODE'");
            //        if (row != null && row.Length > 0)  /// Check now
            //        {
            //            x.Visible = false;
            //        }
            //        else
            //        {
            //            x.Visible = true;

            //        }
            //    }
            //    else
            //    {
            //        x.Visible = true;
            //    }

            //});
            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "STATEID";
            //    x.Caption = "State Id";
            //    x.VisibleIndex = 21;
            //    //rev Pratik
            //    x.PropertiesEdit.DisplayFormatString = "0.00";
            //    //End of rev Pratik
            //    //x.Width = 180;
            //    // x.Width = System.Web.UI.WebControls.Unit.Percentage(20);
            //    if (ViewBag.RetentionColumn != null)
            //    {
            //        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='STATEID'");
            //        if (row != null && row.Length > 0)  /// Check now
            //        {
            //            x.Visible = false;
            //        }
            //        else
            //        {
            //            x.Visible = true;
            //        }
            //    }
            //    else
            //    {
            //        x.Visible = true;
            //    }

            //});

            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "STATE";
            //    x.Caption = "State";
            //    x.VisibleIndex = 22;
            //    //x.Width = 180;
            //    // x.Width = System.Web.UI.WebControls.Unit.Percentage(20);
            //    if (ViewBag.RetentionColumn != null)
            //    {
            //        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='STATE'");
            //        if (row != null && row.Length > 0)  /// Check now
            //        {
            //            x.Visible = false;
            //        }
            //        else
            //        {
            //            x.Visible = true;
            //        }
            //    }
            //    else
            //    {
            //        x.Visible = true;
            //    }

            //});
            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "DEG_ID";
            //    x.Caption = "Designation Id";
            //    x.VisibleIndex = 23;
            //    //x.Width = 180;
            //    // x.Width = System.Web.UI.WebControls.Unit.Percentage(20);
            //    if (ViewBag.RetentionColumn != null)
            //    {
            //        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DEG_ID'");
            //        if (row != null && row.Length > 0)  /// Check now
            //        {
            //            x.Visible = false;
            //        }
            //        else
            //        {
            //            x.Visible = true;
            //        }
            //    }
            //    else
            //    {
            //        x.Visible = true;
            //    }

            //});
            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "DESIGNATION";
            //    x.Caption = "Designation";
            //    x.VisibleIndex = 24;
            //    //x.Width = 180;
            //    // x.Width = System.Web.UI.WebControls.Unit.Percentage(20);
            //    if (ViewBag.RetentionColumn != null)
            //    {
            //        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DESIGNATION'");
            //        if (row != null && row.Length > 0)  /// Check now
            //        {
            //            x.Visible = false;
            //        }
            //        else
            //        {
            //            x.Visible = true;
            //        }
            //    }
            //    else
            //    {
            //        x.Visible = true;
            //    }

            //});
            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "DATEOFJOINING";
            //    x.Caption = "DOJ";
            //    x.VisibleIndex = 25;
            //    //x.Width = 180;
            //    // x.Width = System.Web.UI.WebControls.Unit.Percentage(20);
            //    if (ViewBag.RetentionColumn != null)
            //    {
            //        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DATEOFJOINING'");
            //        if (row != null && row.Length > 0)  /// Check now
            //        {
            //            x.Visible = false;
            //        }
            //        else
            //        {
            //            x.Visible = true;
            //        }
            //    }
            //    else
            //    {
            //        x.Visible = true;
            //    }

            //});
            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "CONTACTNO";
            //    x.Caption = "Contact No.";
            //    x.VisibleIndex = 26;
            //    //x.Width = 180;
            //    // x.Width = System.Web.UI.WebControls.Unit.Percentage(20);
            //    if (ViewBag.RetentionColumn != null)
            //    {
            //        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACTNO'");
            //        if (row != null && row.Length > 0)  /// Check now
            //        {
            //            x.Visible = false;
            //        }
            //        else
            //        {
            //            x.Visible = true;
            //        }
            //    }
            //    else
            //    {
            //        x.Visible = true;
            //    }

            //});
            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "REPORTTOID";
            //    x.Caption = "WD CODE";
            //    x.VisibleIndex = 27;
            //    //x.Width = 180;
            //    // x.Width = System.Web.UI.WebControls.Unit.Percentage(20);
            //    if (ViewBag.RetentionColumn != null)
            //    {
            //        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='REPORTTOID'");
            //        if (row != null && row.Length > 0)  /// Check now
            //        {
            //            x.Visible = false;
            //        }
            //        else
            //        {
            //            x.Visible = true;
            //        }
            //    }
            //    else
            //    {
            //        x.Visible = true;
            //    }

            //});
            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "RPTTODESG";
            //    x.Caption = "WD Designation";
            //    x.VisibleIndex = 28;
            //    if (ViewBag.RetentionColumn != null)
            //    {
            //        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='RPTTODESG'");
            //        if (row != null && row.Length > 0)  /// Check now
            //        {
            //            x.Visible = false;
            //        }
            //        else
            //        {
            //            x.Visible = true;
            //        }
            //    }
            //    else
            //    {
            //        x.Visible = true;
            //    }

            //});
            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "HREPORTTOID";
            //    x.Caption = "AE CODE";
            //    x.VisibleIndex = 29;
            //    //rev Pratik
            //    x.PropertiesEdit.DisplayFormatString = "0.00";
            //    //End of rev Pratik
            //    if (ViewBag.RetentionColumn != null)
            //    {
            //        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='HREPORTTOID'");
            //        if (row != null && row.Length > 0)  /// Check now
            //        {
            //            x.Visible = false;
            //        }
            //        else
            //        {
            //            x.Visible = true;
            //        }
            //    }
            //    else
            //    {
            //        x.Visible = true;
            //    }

            //});
            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "HRPTTODESG";
            //    x.Caption = "AE Designation";
            //    x.VisibleIndex = 30;
            //    if (ViewBag.RetentionColumn != null)
            //    {
            //        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='HRPTTODESG'");
            //        if (row != null && row.Length > 0)  /// Check now
            //        {
            //            x.Visible = false;
            //        }
            //        else
            //        {
            //            x.Visible = true;
            //        }
            //    }
            //    else
            //    {
            //        x.Visible = true;
            //    }

            //});
            //End of rev Pratik

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
                int k = objgps.InsertPageRetention(Col, Session["userid"].ToString(), "DS PERFORMANCE");

                return Json(k, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
    }
}
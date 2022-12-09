﻿using BusinessLogicLayer.SalesTrackerReports;
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
    public class DSVisitDetailsController : Controller
    {
        //
        // GET: /MYSHOP/DSVisitDetails/
        SalesSummary_Report objgps = new SalesSummary_Report();
        public ActionResult DSVisitDetails()
        {
            try
            {
                UserList lstuser = new UserList();
                DSVisitDetailsReport omodel = new DSVisitDetailsReport();
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

        public ActionResult GetDSVisitDetailsList(DSVisitDetailsReport model)
        {
            try
            {
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
                List<DSVisitDetailsModel> omel = new List<DSVisitDetailsModel>();

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
                        omel = APIHelperMethods.ToModelList<DSVisitDetailsModel>(dt);
                        TempData["ExportDSVisitDetails"] = omel;
                        TempData.Keep();
                    }
                    else
                    {
                        TempData["ExportDSVisitDetails"] = null;
                        TempData.Keep();
                    }
                    
                }
                //Rev Debashis 0024906
                if (model.is_pageload == "1")
                {
                    //End of Rev Debashis 0024906
                    double days = (Convert.ToDateTime(ToDate) - Convert.ToDateTime(FromDate)).TotalDays;
                    if (days <= 35)
                    {
                        dt = GetDSVisitDetails(Employee, FromDate, ToDate, Branch_Id);
                    }
                //Rev Debashis 0024906
                }
                //End of Rev Debashis 0024906

                return PartialView("_PartialGridDSVisitDetails", LGetDSVisitDetails(Is_PageLoad));
            }
            catch
            {
                //   return Redirect("~/OMS/Signoff.aspx");
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }

        }

        public DataTable GetDSVisitDetails(string Employee, string start_date, string end_date, string Branch_Id)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSDSVISITDETAILS_REPORT");
            proc.AddPara("@FROMDATE", start_date);
            proc.AddPara("@TODATE", end_date);
            proc.AddPara("@BRANCHID", Branch_Id);
            proc.AddPara("@EMPID", Employee);
            proc.AddPara("@USERID", Convert.ToInt32(Session["userid"]));
            ds = proc.GetTable();
            return ds;
        }

        public IEnumerable LGetDSVisitDetails(string Is_PageLoad)
        {
            DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "DS Visit Details");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;
            }
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);

            if (Is_PageLoad != "is_pageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSDSVISITDETAILS_REPORTs
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSDSVISITDETAILS_REPORTs
                        where d.USERID == Convert.ToInt32(Userid) && d.EMPCODE == "0"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }



        }

        public ActionResult ExportDSVisitDetailsList(int type)
        {


            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetDSSummaryGridViewSettings(), LGetDSVisitDetails(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetDSSummaryGridViewSettings(), LGetDSVisitDetails(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetDSSummaryGridViewSettings(), LGetDSVisitDetails(""));
                case 4:
                    return GridViewExtension.ExportToRtf(GetDSSummaryGridViewSettings(), LGetDSVisitDetails(""));
                case 5:
                    return GridViewExtension.ExportToCsv(GetDSSummaryGridViewSettings(), LGetDSVisitDetails(""));
                //break;

                default:
                    break;
            }

            return null;
        }
        private GridViewSettings GetDSSummaryGridViewSettings()
        {
            DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "DS Visit Details");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;
            }
            var settings = new GridViewSettings();
            settings.Name = "DSVisitDetails";
            settings.CallbackRouteValues = new { Controller = "DSVisitDetails", Action = "GetDSVisitDetailsList" };

            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "DS Visit Details Report";


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
            //Rev Debashis Mantis:0025218
            settings.Columns.Add(x =>
            {
                x.FieldName = "CIRCLE";
                x.Caption = "Circle";
                x.VisibleIndex = 2;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CIRCLE'");
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

            });
            //End of Rev Debashis Mantis:0025218

            settings.Columns.Add(x =>
            {
                x.FieldName = "HREPORTTOUID";
                x.Caption = "AE ID";
                x.VisibleIndex = 3;
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
                x.VisibleIndex = 4;
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
                //Rev Debashis Mantis:0025218
                //x.Caption = "DS/TL ID";
                x.Caption = "DS ID";
                //End of Rev Debashis Mantis:0025218
                x.VisibleIndex = 5;
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
                //Rev Debashis Mantis:0025218
                //x.Caption = "DS/TL Name";
                x.Caption = "DS Name";
                //End of Rev Debashis Mantis:0025218
                x.VisibleIndex = 6;
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
            //Rev Debashis Mantis:0025218
            settings.Columns.Add(x =>
            {
                x.FieldName = "DSTYPE";
                x.Caption = "DS Type";
                x.VisibleIndex = 7;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DSTYPE'");
                    if (row != null && row.Length > 0)
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

            });
            //End of Rev Debashis Mantis:0025218

            settings.Columns.Add(x =>
            {
                x.FieldName = "LOGIN_DATETIME";
                x.Caption = "Visit Date";
                x.VisibleIndex = 8;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='LOGIN_DATETIME'");
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
                x.FieldName = "OUTLETSMAPPED";
                x.Caption = "Outlets Mapped(Added)";
                x.VisibleIndex = 9;
 
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='OUTLETSMAPPED'");
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
                x.FieldName = "RE_VISITED";
                x.Caption = "Outlets Re-Visited";
                x.VisibleIndex = 10;
                
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='RE_VISITED'");
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
                x.FieldName = "DISTANCE_TRAVELLED";
                x.Caption = "Distance Travelled(Km.Mtr)";
                //Rev Debashis 0024906
                x.PropertiesEdit.DisplayFormatString = "0.00";
                //End of Rev Debashis 0024906
                x.VisibleIndex = 11;
                
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DISTANCE_TRAVELLED'");
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
                x.FieldName = "AVGTIMESPENTINMARKET";
                //Rev Debashis 0024906
                //x.Caption = "Avg time spent in the market(HH:MM)";
                x.Caption = "Total time spent in the market(HH:MM)";
                //End of Rev Debashis 0024906
                x.VisibleIndex = 12;
                
                x.PropertiesEdit.DisplayFormatString = "0.00";
                
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='AVGTIMESPENTINMARKET'");
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

            //Rev Debashis 0024956
            settings.Columns.Add(x =>
            {
                x.FieldName = "DAYSTTIME";
                x.Caption = "Day Start(HH:MM)";
                x.VisibleIndex = 13;

                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DAYSTTIME'");
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
                x.FieldName = "DAYENDTIME";
                x.Caption = "Day End(HH:MM)";
                x.VisibleIndex = 14;

                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DAYENDTIME'");
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
            //End of Rev Debashis 0024956

            
            settings.Columns.Add(x =>
            {
                x.FieldName = "AVGSPENTDURATION";
                //Rev Debashis 0024906
                //x.Caption = "Avg time spent in OL(CFT-Customer Facing Time)(HH:MM)";
                x.Caption = "Avg time spent in OL(CFT-New&Revisit)(HH:MM)";
                //End of Rev Debashis 0024906
                x.VisibleIndex = 15;
                
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='AVGSPENTDURATION'");
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
                int k = objgps.InsertPageRetention(Col, Session["userid"].ToString(), "DS VISIT DETAILS");

                return Json(k, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
	}
}
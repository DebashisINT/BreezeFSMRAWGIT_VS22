//****************************************************************************************************************************************
//    1.0     Sanchita      V2.0.40      11-05-2023      Feedback column is required in Horizontal Performance Summary & Detail Report. 
//                                                       Refer: 25786
//    2.0     Sanchita      V2.0.42      19/07/2023      Add Branch parameter in Listing of MIS - Horizontal Performance Summary & Detail.
//                                                       Mantis : 26135
//    3.0     Sanchita      V2.0.43      07-11-2023      0026895: System will prompt for Branch selection if the Branch hierarchy is activated.
//* ***************************************************************************************************************************************

using BusinessLogicLayer;
using BusinessLogicLayer.SalesTrackerReports;
using DataAccessLayer;
using DevExpress.Export;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DevExpress.XtraPrinting;
using Models;
using MyShop.Models;
using SalesmanTrack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilityLayer;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class HorizontalPerformanceSummaryDetailsController : Controller
    {
        //
        // GET: /MYSHOP/HorizontalPerformanceSummaryDetails/
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
                // Rev 3.0
                DBEngine obj1 = new DBEngine();
                ViewBag.BranchMandatory = Convert.ToString(obj1.GetDataTable("select [value] from FTS_APP_CONFIG_SETTINGS WHERE [Key]='IsActivateEmployeeBranchHierarchy'").Rows[0][0]);
                // End of Rev 3.0

                DataSet ds = new DataSet();
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                DataTable dt3 = new DataTable();

                ds.Tables.Add(dt1);
                ds.Tables.Add(dt2);
                ds.Tables.Add(dt3);

                // Rev 2.0
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
                ViewBag.HeadBranch = omodel.modelbranch;
                ViewBag.h_id = h_id;
                // End of Rev 2.0

                return View(ds);
                //return View(omodel);

            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public PartialViewResult PerformanceSummeryDetailsGridViewCallback(SalesSummaryReport model)
        {

            string IsPageLoad = string.Empty;

            if (model.is_pageload == "0")
            {
                IsPageLoad = "Ispageload";
            }

            if (model.Fromdate == null)
            {
                model.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
            }

            if (model.Todate == null)
            {
                model.Todate = DateTime.Now.ToString("dd-MM-yyyy");
            }


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
            // Rev 2.0
            string Branch_Id = "";
            int b = 1;
            if (model.BranchId != null && model.BranchId.Count > 0)
            {
                foreach (string item in model.BranchId)
                {
                    if (b > 1)
                        Branch_Id = Branch_Id + "," + item;
                    else
                        Branch_Id = item;
                    b++;
                }
            }
            // End of Rev 2.0

            DataSet ds = new DataSet();
            if (model.is_pageload != "0" && model.is_pageload != null)
            {
                double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;
                //Mantis Issue 24728
                //if (days <= 62)
                if (days <= 35)
                //End of Mantis Issue 24728
                {
                    ProcedureExecute proc = new ProcedureExecute("PRC_FTSHORIZONTALEMPLOYEEPERFORMANCESUMDET_REPORT");
                    //  proc.AddVarcharPara("@Action", 100, "GetAllDropDownDetailForSalesQuotation");
                    proc.AddPara("@FROMDATE", datfrmat);
                    proc.AddPara("@TODATE", dattoat);
                    proc.AddPara("@STATECODE", state);
                    proc.AddPara("@DESIGNID", desig);
                    proc.AddPara("@EMPCODES", empcode);
                    proc.AddPara("@REPORTTYPE", "Summary");
                    proc.AddPara("@USERID", Convert.ToInt32(Session["userid"]));
                    // Rev 2.0
                    proc.AddVarcharPara("@BRANCHID", -1, Branch_Id);
                    // End of Rev 2.0
                    ds = proc.GetDataSet();
                }
            }
            else
            {
                ds.Tables.Add(new DataTable());
                ds.Tables.Add(new DataTable());
                ds.Tables.Add(new DataTable());
            }
            TempData["PerformanceSummeryDetailsGridView"] = ds;
            return PartialView(ds);
        }

        public PartialViewResult PerformanceDetailsGridViewCallback(SalesSummaryReport model)
        {

            string IsPageLoad = string.Empty;

            if (model.is_pageload == "0")
            {
                IsPageLoad = "Ispageload";
            }

            //if (model.Fromdate == null)
            //{
            //    model.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
            //}

            //if (model.Todate == null)
            //{
            //    model.Todate = DateTime.Now.ToString("dd-MM-yyyy");
            //}


            //string datfrmat = model.Fromdate.Split('-')[2] + '-' + model.Fromdate.Split('-')[1] + '-' + model.Fromdate.Split('-')[0];
            //string dattoat = model.Todate.Split('-')[2] + '-' + model.Todate.Split('-')[1] + '-' + model.Todate.Split('-')[0];
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
            // Rev 2.0
            string Branch_Id = "";
            int b = 1;
            if (model.BranchId != null && model.BranchId.Count > 0)
            {
                foreach (string item in model.BranchId)
                {
                    if (b > 1)
                        Branch_Id = Branch_Id + "," + item;
                    else
                        Branch_Id = item;
                    b++;
                }
            }
            // End of Rev 2.0

            DataSet ds = new DataSet();
            if (model.is_pageload != "0" && model.is_pageload != null)
            {
                //double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;
                //Mantis Issue 24728
                //if (days <= 62)
                //if (days <= 35)
                ////End of Mantis Issue 24728
                //{
                ProcedureExecute proc = new ProcedureExecute("PRC_FTSHORIZONTALEMPLOYEEPERFORMANCESUMDET_FETCH");
                    //  proc.AddVarcharPara("@Action", 100, "GetAllDropDownDetailForSalesQuotation");
                    proc.AddPara("@FROMDATE", model.Fromdate);
                    proc.AddPara("@TODATE", model.Todate);
                    proc.AddPara("@STATEID", state);
                    proc.AddPara("@DESIGNID", desig);
                    proc.AddPara("@EMPID", empcode);
                    proc.AddPara("@REPORTTYPE", "Details");
                    proc.AddPara("@VISITTYPE", model.is_pageload);
                    proc.AddPara("@USERID", Convert.ToInt32(Session["userid"]));
                    // Rev 2.0
                    proc.AddVarcharPara("@BRANCHID", -1, Branch_Id);
                    // End of Rev 2.0
                ds = proc.GetDataSet();
                //}
            }
            else
            {
                ds.Tables.Add(new DataTable());
                ds.Tables.Add(new DataTable());
                ds.Tables.Add(new DataTable());
            }
            TempData["PerformanceDetailsGridView"] = ds;
            return PartialView(ds);
        }

        public ActionResult ExporTPerformanceSummaryList(int type)
        {
            //Rev Tanmoy get data through execute query
            //DataTable dbDashboardData = new DataTable();
            //DBEngine objdb = new DBEngine();
            //String query = TempData["DashboardGridView"].ToString();
            //dbDashboardData = objdb.GetDataTable(query);

            ViewData["PerformanceSummeryDetailsGridView"] = TempData["PerformanceSummeryDetailsGridView"];

            DataSet DS = (DataSet)ViewData["PerformanceSummeryDetailsGridView"];

            //End Rev
            TempData.Keep();

            if (ViewData["PerformanceSummeryDetailsGridView"] != null)
            {

                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetDashboardGridView(ViewData["PerformanceSummeryDetailsGridView"]), DS.Tables[1]);
                    //break;
                    case 2:
                        //return GridViewExtension.ExportToXlsx(GetDashboardGridView(ViewData["PerformanceSummeryDetailsGridView"]), DS.Tables[1]);
                        return GridViewExtension.ExportToXlsx(GetDashboardGridView(ViewData["PerformanceSummeryDetailsGridView"]), DS.Tables[1], new XlsxExportOptionsEx() { ExportType = ExportType.WYSIWYG });
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetDashboardGridView(ViewData["PerformanceSummeryDetailsGridView"]), DS.Tables[1]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetDashboardGridView(ViewData["PerformanceSummeryDetailsGridView"]), DS.Tables[1]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetDashboardGridView(ViewData["PerformanceSummeryDetailsGridView"]), DS.Tables[1]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetDashboardGridView(object dataset)
        {
            var settings = new GridViewSettings();
            settings.Name = "Horizontal Performance Summary & Detail report";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Horizontal Performance Summary & Detail report";
            String ID = Convert.ToString(TempData["PerformanceSummeryDetailsGridView"]);
            TempData.Keep();
            DataSet dt = (DataSet)dataset;



            System.Data.DataTable dtColumnTable = new System.Data.DataTable();

            if (dt != null && dt.Tables.Count > 0)
            {

                dtColumnTable = dt.Tables[0];
                if (dtColumnTable != null && dtColumnTable.Rows.Count > 0)
                {
                    System.Data.DataRow[] drr = dtColumnTable.Select("PARRENTID=0");
                    int i = 0;
                    foreach (System.Data.DataRow dr in drr)
                    {
                        i = i + 1;
                        System.Data.DataRow[] drrRow = dtColumnTable.Select("PARRENTID='" + Convert.ToString(dr["HEADID"]) + "'");

                        if (drrRow.Length > 0)
                        {

                            settings.Columns.AddBand(x =>
                            {
                                //x.FieldName = Convert.ToString(dr["HEADNAME"]);
                                x.Caption = Convert.ToString(dr["HEADNAME"]).Trim();
                                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                                x.VisibleIndex = i;
                                //x.Settings.AutoFilterCondition = AutoFilterCondition.Contains;

                                foreach (System.Data.DataRow drrs in drrRow)
                                {
                                    System.Data.DataRow[] drrRows = dtColumnTable.Select("PARRENTID='" + Convert.ToString(drrs["HEADID"]) + "'");

                                    if (drrRows.Length > 0)
                                    {
                                        x.Columns.AddBand(xSecond =>
                                        {
                                            xSecond.Caption = Convert.ToString(drrs["HEADNAME"]).Trim();
                                            xSecond.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                                            foreach (System.Data.DataRow drrss in drrRows)
                                            {
                                                System.Data.DataRow[] drrRowss = dtColumnTable.Select("PARRENTID='" + Convert.ToString(drrss["HEADID"]) + "'");
                                                if (drrRowss.Length > 0)
                                                {

                                                    xSecond.Columns.AddBand(xThird =>
                                                    {
                                                        xThird.Caption = Convert.ToString(drrss["HEADNAME"]).Trim();
                                                        xThird.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                                                        foreach (System.Data.DataRow drrrrs in drrRowss)
                                                        {
                                                            xThird.Columns.Add(xFourth =>
                                                            {
                                                                xFourth.Caption = Convert.ToString(drrrrs["HEADNAME"]).Trim();
                                                                xFourth.FieldName = Convert.ToString(drrrrs["HEADSHRTNAME"]).Trim();
                                                                xFourth.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                                                            });
                                                        }

                                                    });


                                                }
                                                else
                                                {
                                                    xSecond.Columns.Add(xThird =>
                                                    {
                                                        xThird.Caption = Convert.ToString(drrss["HEADNAME"]).Trim();
                                                        xThird.FieldName = Convert.ToString(drrss["HEADSHRTNAME"]).Trim();
                                                        xThird.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                                                    });
                                                }

                                            }


                                        });

                                    }
                                    else
                                    {
                                        x.Columns.Add(xSecond =>
                                        {
                                            xSecond.Caption = Convert.ToString(drrs["HEADNAME"]).Trim();
                                            xSecond.FieldName = Convert.ToString(drrs["HEADSHRTNAME"]).Trim();
                                            xSecond.Settings.AutoFilterCondition = AutoFilterCondition.Contains;

                                        });
                                    }

                                }

                            });



                        }
                        else
                        {
                            settings.Columns.Add(x =>
                            {
                                x.Caption = Convert.ToString(dr["HEADNAME"]).Trim();
                                x.FieldName = Convert.ToString(dr["HEADSHRTNAME"]).Trim();
                                x.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                                x.VisibleIndex = i;

                            });
                        }

                    }
                }

            }



            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

        public ActionResult ExporTPerformanceSummaryDetailsList(int type)
        {
            //Rev Tanmoy get data through execute query
            //DataTable dbDashboardData = new DataTable();
            //DBEngine objdb = new DBEngine();
            //String query = TempData["DashboardGridView"].ToString();
            //dbDashboardData = objdb.GetDataTable(query);

            ViewData["PerformanceDetailsGridView"] = TempData["PerformanceDetailsGridView"];

            DataSet DS = (DataSet)ViewData["PerformanceDetailsGridView"];

            //End Rev
            TempData.Keep();

            if (ViewData["PerformanceDetailsGridView"] != null)
            {

                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetPerformanceDetailsGridView(ViewData["PerformanceDetailsGridView"]), DS.Tables[0]);
                    //break;
                    case 2:
                        //return GridViewExtension.ExportToXlsx(GetDashboardGridView(ViewData["PerformanceSummeryDetailsGridView"]), DS.Tables[1]);
                        return GridViewExtension.ExportToXlsx(GetPerformanceDetailsGridView(ViewData["PerformanceDetailsGridView"]), DS.Tables[0], new XlsxExportOptionsEx() { ExportType = ExportType.WYSIWYG });
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetPerformanceDetailsGridView(ViewData["PerformanceDetailsGridView"]), DS.Tables[0]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetPerformanceDetailsGridView(ViewData["PerformanceDetailsGridView"]), DS.Tables[0]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetPerformanceDetailsGridView(ViewData["PerformanceDetailsGridView"]), DS.Tables[0]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetPerformanceDetailsGridView(object dataset)
        {
            //var settings = new GridViewSettings();
            //settings.Name = "Horizontal Performance Summary & Detail report";
            //settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            //settings.SettingsExport.FileName = "Horizontal Performance Summary & Detail report";
            //String ID = Convert.ToString(TempData["PerformanceSummeryDetailsGridView"]);
            //TempData.Keep();
            //DataSet dt = (DataSet)dataset;




           // DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "PERFORMANCE SUMMARY");
            var settings = new GridViewSettings();
            settings.Name = "Horizontal Performance Detail Report";
            settings.CallbackRouteValues = new { Controller = "HorizontalPerformanceSummaryDetails", Action = "PerformanceDetailsGridViewCallback" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Horizontal Performance Detail Report";


            settings.Columns.Add(x =>
            {
                x.FieldName = "CUSTNAME";
                x.Caption = "Customer Name";
                x.VisibleIndex = 1;
                x.Visible = true;

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ADDRESS";
                x.Caption = "Address";
                x.VisibleIndex = 2;
                x.Visible = true;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "MOBILENO";
                x.Caption = "Customer Mobile";
                x.VisibleIndex = 3;
                x.Visible = true;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "SHOPTYPE";
                x.Caption = "Type (Shop type)";
                x.VisibleIndex = 4;
                x.Visible = true;
            });

            // Rev 1.0
            settings.Columns.Add(x =>
            {
                x.FieldName = "BEAT";
                x.Caption = "Beat";
                x.VisibleIndex = 5;
                x.Visible = true;
            });
            // End of Rev 1.0

            settings.Columns.Add(x =>
            {
                x.FieldName = "VISITDATE";
                x.Caption = "Visited Date";
                x.VisibleIndex = 6;
                x.Width = 100;
                x.Visible = true;
            });

            //Rev Debashis -- 0024575
            settings.Columns.Add(x =>
            {
                x.FieldName = "VISITTIME";
                x.Caption = "Visited Time";
                x.VisibleIndex = 7;
                x.Width = 120;
                x.Visible = true;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "SPENTDURATION";
                x.Caption = "Duration Spent";
                x.VisibleIndex = 8;
                x.Width = 120;
                x.Visible = true;
            });

            // Rev 1.0
            settings.Columns.Add(x =>
            {
                x.FieldName = "FEEDBACK";
                x.Caption = "Feedback";
                x.VisibleIndex = 9;
                x.Visible = true;
            });
            // End of Rev 1.0

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }
	}
}
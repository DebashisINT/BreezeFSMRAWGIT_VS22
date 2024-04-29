#region======================================Revision History===============================================================================================
//1.0   V2.0.38     Debashis    20/01/2023      Revisit Contact information is required in the Performance Summary report.
//                                              Refer: 0025586
//2.0   V2.0.41     Sanchita    19/07/2023      Add Branch parameter in MIS -> Performance Summary. Mantis : 26135
//3.0   V2.0.43     Sanchita    07-11-2023      0026895: System will prompt for Branch selection if the Branch hierarchy is activated.
//4.0   V2.0.44     Debashis    03/01/2024      Performance Summary Report required a new column 'Type' & 'Stage'.Refer: 0027066 & 0027091
//5.0   V2.0.44     Pallab      10/01/2024      Compact column width required in the Performance Summary report grid excel export.Refer: 0027174
//6.0   V2.0.46     Debashis    16/04/2024      Separate column is required for Phone number in Performance Summary report.Refer: 0027278
#endregion===================================End of Revision History========================================================================================
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
using BusinessLogicLayer;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class PerformanceController : Controller
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
                DataTable dt = objgps.GetStateMandatory(Session["userid"].ToString());
                if (dt!=null && dt.Rows.Count>0)
                {
                    ViewBag.StateMandatory = dt.Rows[0]["IsStateMandatoryinReport"].ToString();
                }
                // Rev 3.0
                DBEngine obj1 = new DBEngine();
                ViewBag.BranchMandatory = Convert.ToString(obj1.GetDataTable("select [value] from FTS_APP_CONFIG_SETTINGS WHERE [Key]='IsActivateEmployeeBranchHierarchy'").Rows[0][0]);
                // End of Rev 3.0
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
                ViewBag.h_id = h_id;
                // End of Rev 2.0

                return View(omodel);

            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
        public ActionResult GetSalesSummaryList1()
        {
            return PartialView("PartialGetSalesSummaryList", GetSalesSummary("0"));
        }
        public ActionResult GetSalesParformanceList(SalesSummaryReport model)
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
                //Rev 1.0 Mantis:0025586
                if (model.IsRevisitContactDetails != null)
                {
                    TempData["IsRevisitContactDetails"] = model.IsRevisitContactDetails;
                    TempData.Keep();
                }
                //End of Rev 1.0 Mantis:0025586
                double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;

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

                //if (days <= 7)
                //Mantis Issue 24728
                //if (days <= 30)
                if (days <= 35)
                //End of Mantis Issue 24728
                {
                    //Rev 1.0 Mantis: 0025586
                    //dt = objgps.GetSalesPerformanceReport(datfrmat, dattoat, Userid, state, desig, empcode);
                    // Rev 2.0
                    //dt = objgps.GetSalesPerformanceReport(datfrmat, dattoat, Userid, state, desig, empcode,model.IsRevisitContactDetails);
                    dt = objgps.GetSalesPerformanceReport(Branch_Id, datfrmat, dattoat, Userid, state, desig, empcode, model.IsRevisitContactDetails);
                    // End of Rev 2.0
                    //End of Rev 1.0 Mantis: 0025586
                }

                return PartialView("PartialGetPerformanceSummary", GetSalesSummary(frmdate));


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

        public IEnumerable GetSalesSummary(string frmdate)
        {
            DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "PERFORMANCE SUMMARY");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            }

            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);

            if (frmdate != "Ispageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSEMPLOYEEPERFORMANCE_REPORTs
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSEMPLOYEEPERFORMANCE_REPORTs
                        where d.USERID == Convert.ToInt32(Userid) && d.EMPCODE == "0"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }



        }


        public ActionResult ExporPerformanceList(int type)
        {


            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(), GetSalesSummary(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(), GetSalesSummary(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(), GetSalesSummary(""));
                case 4:
                    return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(), GetSalesSummary(""));
                case 5:
                    return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(), GetSalesSummary(""));
                //break;

                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetEmployeeBatchGridViewSettings()
        {
            DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "PERFORMANCE SUMMARY");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            }
            var settings = new GridViewSettings();
            settings.Name = "Performance Report";
            settings.CallbackRouteValues = new { Controller = "Performance", Action = "GetSalesParformanceList" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Performance Report";
            //Rev 5.0 Mantis: "x.Width" replace with "x.ExportWidth" in all columns and column width adjustment for export excel
            //Rev 1.0 Mantis: 0025586
            if (TempData["IsRevisitContactDetails"].ToString() == "0")
            {
                //End of Rev 1.0 Mantis: 0025586
                settings.Columns.Add(x =>
            {
                x.FieldName = "WORK_DATE";
                x.Caption = "Date";
                x.VisibleIndex = 1;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='WORK_DATE'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 80;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.ExportWidth = 80;
                }

            });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "LOGGEDIN";
                    x.Caption = "Login Time";
                    x.VisibleIndex = 2;
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
                            x.ExportWidth = 80;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 80;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "LOGEDOUT";
                    x.Caption = "Logout Time";
                    x.VisibleIndex = 3;
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
                            x.ExportWidth = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "CONTACTNO";
                    x.Caption = "Use id";
                    x.VisibleIndex = 4;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACTNO'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "STATE";
                    x.Caption = "State";
                    x.VisibleIndex = 5;
                    x.Width = 100;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='STATE'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 80;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 80;
                    }
                });

                //Rev 4.0 -- 0024575
                settings.Columns.Add(x =>
                {
                    x.FieldName = "SHOP_DISTRICT";
                    x.Caption = "District";
                    x.VisibleIndex = 6;
                    x.Width = 120;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOP_DISTRICT'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "SHOP_PINCODE";
                    x.Caption = "Pincode";
                    x.VisibleIndex = 7;
                    //x.Width = 120;
                    
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOP_PINCODE'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 80;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 80;
                    }
                });
                //End of Rev 4.0 -- 0024575

                settings.Columns.Add(x =>
                {
                    x.FieldName = "BRANCHDESC";
                    x.Caption = "Branch";
                    x.VisibleIndex = 8;
                    //x.Width = 180;
                    
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
                            x.ExportWidth = 130;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 130;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "OFFICE_ADDRESS";
                    x.Caption = "Office Address";
                    x.VisibleIndex = 9;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='OFFICE_ADDRESS'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 120;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 120;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "ATTEN_STATUS";
                    x.Caption = "Attendance";
                    x.VisibleIndex = 10;
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
                            x.ExportWidth = 120;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 120;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "WORK_LEAVE_TYPE";
                    x.Caption = "Work/Leave Type";
                    x.VisibleIndex = 11;
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
                            x.ExportWidth = 140;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 140;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "REMARKS";
                    x.Caption = "Remarks";
                    x.VisibleIndex = 12;
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
                            x.ExportWidth = 80;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 80;
                    }
                });


                settings.Columns.Add(x =>
                {
                    x.FieldName = "EMPNAME";
                    x.Caption = "Emp. Name";
                    x.VisibleIndex = 13;
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
                            x.ExportWidth = 130;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 130;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "EMPID";
                    x.Caption = "Emp. ID";
                    x.VisibleIndex = 14;
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
                            x.ExportWidth = 120;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 120;
                    }
                });


                settings.Columns.Add(x =>
                {
                    x.FieldName = "DESIGNATION";
                    x.Caption = "Designation";
                    x.VisibleIndex = 15;
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
                            x.ExportWidth = 130;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 130;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "DATEOFJOINING";
                    x.Caption = "DOJ";
                    x.VisibleIndex = 16;
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
                            x.ExportWidth = 80;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 80;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "REPORTTO";
                    x.Caption = "Supervisor";
                    x.VisibleIndex = 17;
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
                            x.ExportWidth = 140;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 140;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "RPTTODESG";
                    x.Caption = "Supervisor Desg.";
                    x.VisibleIndex = 18;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='RPTTODESG'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 140;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 140;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "PP_NAME";
                    x.Caption = "PP Name";
                    x.VisibleIndex = 19;
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
                            x.ExportWidth = 90;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 90;
                    }
                });


                settings.Columns.Add(x =>
                {
                    x.FieldName = "DD_NAME";
                    x.Caption = "DD Name";
                    x.VisibleIndex = 20;
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
                            x.ExportWidth = 90;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 90;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "SHOP_NAME";
                    x.Caption = "Retailer Name";
                    x.VisibleIndex = 21;
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
                            x.ExportWidth = 110;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 110;
                    }
                });

                //Rev Debashis Mantis: 0025524
                settings.Columns.Add(x =>
                {
                    x.FieldName = "BEATNAME";
                    x.Caption = "Beat";
                    x.VisibleIndex = 22;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='BEATNAME'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                            //x.Width = 0;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 100;
                    }
                });
                //End of Rev Debashis Mantis: 0025524

                settings.Columns.Add(x =>
                {
                    x.FieldName = "ENTITYCODE";
                    x.Caption = "Code";
                    x.VisibleIndex = 23;
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
                            x.ExportWidth = 80;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 80;
                    }
                });

                //Rev Debashis -- 0024575
                settings.Columns.Add(x =>
                {
                    x.FieldName = "SHOP_CLUSTER";
                    x.Caption = "Cluster";
                    x.VisibleIndex = 24;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOP_CLUSTER'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 80;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 80;
                    }
                });
                //End of Rev Debashis -- 0024575

                settings.Columns.Add(x =>
                {
                    x.FieldName = "SHOP_TYPE";
                    x.Caption = "Type";
                    x.VisibleIndex = 25;
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
                            x.ExportWidth = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    //Rev 6.0 Mantis: 0027278
                    //x.FieldName = "SHOPADDR_CONTACT";
                    //x.Caption = "Retailer Address & Mobile no.";
                    x.FieldName = "SHOPADDR";
                    x.Caption = "Retailer Address";
                    //End of Rev 6.0 Mantis: 0027278
                    x.VisibleIndex = 26;
                    
                    if (ViewBag.RetentionColumn != null)
                    {
                        //Rev 6.0 Mantis: 0027278
                        //System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOPADDR_CONTACT'");
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOPADDR'");
                        //End of Rev 6.0 Mantis: 0027278
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 350;

                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 350;

                    }
                });

                //Rev 6.0 Mantis: 0027278
                settings.Columns.Add(x =>
                {
                    x.FieldName = "SHOPCONTACT";
                    x.Caption = "Mobile no.";
                    x.VisibleIndex = 27;

                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOPCONTACT'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 150;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 150;
                    }
                });
                //End of Rev 6.0 Mantis: 0027278

                settings.Columns.Add(x =>
                {
                    //Rev 6.0 Mantis: 0027278
                    //x.FieldName = "PPADDR_CONTACT";
                    //x.Caption = "PP Address & Mobile no.";
                    //x.VisibleIndex = 27;
                    x.FieldName = "PPADDR";
                    x.Caption = "PP Address";
                    x.VisibleIndex = 28;
                    //End of Rev 6.0 Mantis: 0027278
                    if (ViewBag.RetentionColumn != null)
                    {
                        //Rev 6.0 Mantis: 0027278
                        //System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PPADDR_CONTACT'");
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PPADDR'");
                        //End of Rev 6.0 Mantis: 0027278
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 300;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 300;
                    }
                });

                //Rev 6.0 Mantis: 0027278
                settings.Columns.Add(x =>
                {
                    x.FieldName = "PPCONTACT";
                    x.Caption = "Mobile no.";
                    x.VisibleIndex = 29;

                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PPCONTACT'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 150;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 150;
                    }
                });
                //End of Rev 6.0 Mantis: 0027278

                settings.Columns.Add(x =>
                {
                    //Rev 6.0 Mantis: 0027278
                    //x.FieldName = "DDADDR_CONTACT";
                    //x.Caption = "DD Address & Mobile no.";
                    //x.VisibleIndex = 28;
                    x.FieldName = "DDADDR";
                    x.Caption = "DD Address";
                    x.VisibleIndex = 30;
                    //End of Rev 6.0 Mantis: 0027278
                    if (ViewBag.RetentionColumn != null)
                    {
                        //Rev 6.0 Mantis: 0027278
                        //System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DDADDR_CONTACT'");
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DDADDR'");
                        //End of Rev 6.0 Mantis: 0027278
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 300;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 300;
                    }
                    // x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                });

                //Rev 6.0 Mantis: 0027278
                settings.Columns.Add(x =>
                {
                    x.FieldName = "DDCONTACT";
                    x.Caption = "Mobile no.";
                    x.VisibleIndex = 31;

                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DDCONTACT'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 150;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 150;
                    }
                });
                //End of Rev 6.0 Mantis: 0027278

                //Rev Debashis -- 0024577
                settings.Columns.Add(x =>
                {
                    x.FieldName = "ALT_MOBILENO1";
                    x.Caption = "Alternate Phone No.";
                    x.VisibleIndex = 32;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ALT_MOBILENO1'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 140;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 140;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "SHOP_OWNER_EMAIL2";
                    x.Caption = "Alternate Email ID";
                    x.VisibleIndex = 33;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOP_OWNER_EMAIL2'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 130;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 130;
                    }
                });
                //End of Rev Debashis -- 0024577

                settings.Columns.Add(x =>
                {
                    x.FieldName = "MEETING_ADDRESS";
                    x.Caption = "Meeting Address";
                    x.VisibleIndex = 34;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='MEETING_ADDRESS'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 120;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 120;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "MEETINGREMARKS";
                    x.Caption = "Meeting Remarks";
                    x.VisibleIndex = 35;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='MEETINGREMARKS'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 110;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 110;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "VISITREMARKS";
                    x.Caption = "Feedback";
                    x.VisibleIndex = 36;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='VISITREMARKS'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 100;
                    }
                });

                //Rev 4.0 Mantis: 0027066 & 0027091
                settings.Columns.Add(x =>
                {
                    x.FieldName = "PARTYSTATUSTYPE";
                    x.Caption = "Type";
                    x.VisibleIndex = 37;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PARTYSTATUSTYPE'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 80;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 80;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "STAGE";
                    x.Caption = "Stage";
                    x.VisibleIndex = 38;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='STAGE'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 80;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 80;
                    }
                });
                //End of Rev 4.0 Mantis: 0027066 & 0027091

                settings.Columns.Add(x =>
                {
                    x.FieldName = "TOTAL_VISIT";
                    x.Caption = "Visit Count";
                    x.VisibleIndex = 39;
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
                            x.ExportWidth = 110;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 110;
                    }
                    // x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                });


                settings.Columns.Add(x =>
                {
                    x.FieldName = "RE_VISITED";
                    x.Caption = "Re Visit";
                    x.VisibleIndex = 40;
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
                            x.ExportWidth = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 100;
                    }
                    // x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "NEWSHOP_VISITED";
                    x.Caption = "New Visit";
                    x.VisibleIndex = 41;
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
                            x.ExportWidth = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 100;
                    }
                    // x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "TOTMETTING";
                    x.Caption = "Meeting";
                    x.VisibleIndex = 42;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='TOTMETTING'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "SPENT_DURATION";
                    x.Caption = "Duration Spend";
                    x.VisibleIndex = 43;
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
                            x.ExportWidth = 120;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 120;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "DISTANCE_TRAVELLED";
                    x.Caption = "Travelled(KM)";
                    x.VisibleIndex = 44;
                    x.PropertiesEdit.DisplayFormatString = "0.00";
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
                            x.ExportWidth = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "TOTAL_ORDER_BOOKED_VALUE";
                    x.Caption = "Sale Value";
                    x.VisibleIndex = 45;
                    x.PropertiesEdit.DisplayFormatString = "0.00";
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='TOTAL_ORDER_BOOKED_VALUE'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 100;
                    }
                    // x.Width = System.Web.UI.WebControls.Unit.Percentage(10);

                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "TOTAL_COLLECTION";
                    x.Caption = "Collection value";
                    x.VisibleIndex = 46;
                    x.PropertiesEdit.DisplayFormatString = "0.00";
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='TOTAL_COLLECTION'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 130;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 130;
                    }
                    // x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                });
                //Rev 1.0 Mantis: 0025586
            }
            if (TempData["IsRevisitContactDetails"].ToString() == "1")
            {
                settings.Columns.Add(x =>
                {
                    x.FieldName = "WORK_DATE";
                    x.Caption = "Date";
                    x.VisibleIndex = 1;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='WORK_DATE'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 80;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 80;
                    }

                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "LOGGEDIN";
                    x.Caption = "Login Time";
                    x.VisibleIndex = 2;
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
                            x.ExportWidth = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "LOGEDOUT";
                    x.Caption = "Logout Time";
                    x.VisibleIndex = 3;
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
                            x.ExportWidth = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "CONTACTNO";
                    x.Caption = "Use id";
                    x.VisibleIndex = 4;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACTNO'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 90;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 90;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "STATE";
                    x.Caption = "State";
                    x.VisibleIndex = 5;
                    
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='STATE'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 80;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 80;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "SHOP_DISTRICT";
                    x.Caption = "District";
                    x.VisibleIndex = 6;
                    
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOP_DISTRICT'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 90;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 90;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "SHOP_PINCODE";
                    x.Caption = "Pincode";
                    x.VisibleIndex = 7;
                    //x.Width = 120;
                    
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOP_PINCODE'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "BRANCHDESC";
                    x.Caption = "Branch";
                    x.VisibleIndex = 8;
                    //x.Width = 180;
                    
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
                            x.ExportWidth = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "OFFICE_ADDRESS";
                    x.Caption = "Office Address";
                    x.VisibleIndex = 9;
                    
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='OFFICE_ADDRESS'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 120;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 120;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "ATTEN_STATUS";
                    x.Caption = "Attendance";
                    x.VisibleIndex = 10;
                    
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
                            x.ExportWidth = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "WORK_LEAVE_TYPE";
                    x.Caption = "Work/Leave Type";
                    x.VisibleIndex = 11;
                    
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
                            x.ExportWidth = 130;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 130;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "REMARKS";
                    x.Caption = "Remarks";
                    x.VisibleIndex = 12;
                    
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
                            x.ExportWidth = 130;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 130;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "EMPNAME";
                    x.Caption = "Emp. Name";
                    x.VisibleIndex = 13;
                    
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
                            x.ExportWidth = 130;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 130;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "EMPID";
                    x.Caption = "Emp. ID";
                    x.VisibleIndex = 14;
                    
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
                            x.ExportWidth = 120;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 120;
                    }
                });


                settings.Columns.Add(x =>
                {
                    x.FieldName = "DESIGNATION";
                    x.Caption = "Designation";
                    x.VisibleIndex = 15;
                    
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
                            x.ExportWidth = 130;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 130;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "DATEOFJOINING";
                    x.Caption = "DOJ";
                    x.VisibleIndex = 16;
                    
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
                            x.ExportWidth = 80;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 80;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "REPORTTO";
                    x.Caption = "Supervisor";
                    x.VisibleIndex = 17;
                    
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
                            x.ExportWidth = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "RPTTODESG";
                    x.Caption = "Supervisor Desg.";
                    x.VisibleIndex = 18;
                    
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='RPTTODESG'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 130;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 130;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "PP_NAME";
                    x.Caption = "PP Name";
                    x.VisibleIndex = 19;
                    
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
                            x.ExportWidth = 110;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 110;
                    }
                });


                settings.Columns.Add(x =>
                {
                    x.FieldName = "DD_NAME";
                    x.Caption = "DD Name";
                    x.VisibleIndex = 20;
                    
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
                            x.ExportWidth = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "SHOP_NAME";
                    x.Caption = "Retailer Name";
                    x.VisibleIndex = 21;
                    
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
                            x.ExportWidth = 120;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 120;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "BEATNAME";
                    x.Caption = "Beat";
                    x.VisibleIndex = 22;
                    
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='BEATNAME'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                            //x.Width = 0;
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
                    x.FieldName = "ENTITYCODE";
                    x.Caption = "Code";
                    x.VisibleIndex = 23;
                    
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
                            x.ExportWidth = 80;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 80;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "SHOP_CLUSTER";
                    x.Caption = "Cluster";
                    x.VisibleIndex = 24;
                    
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOP_CLUSTER'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 90;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 90;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "SHOP_TYPE";
                    x.Caption = "Type";
                    x.VisibleIndex = 25;
                    
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
                            x.ExportWidth = 80;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 80;
                    }
                });

                settings.Columns.Add(x =>
                {
                    //Rev 6.0 Mantis: 0027278
                    //x.FieldName = "SHOPADDR_CONTACT";
                    //x.Caption = "Retailer Address & Mobile no.";
                    x.FieldName = "SHOPADDR";
                    x.Caption = "Retailer Address";
                    //End of Rev 6.0 Mantis: 0027278
                    x.VisibleIndex = 26;
                    
                    if (ViewBag.RetentionColumn != null)
                    {
                        //Rev 6.0 Mantis: 0027278
                        //System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOPADDR_CONTACT'");
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOPADDR'");
                        //End of Rev 6.0 Mantis: 0027278
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 250;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 250;
                    }
                });

                //Rev 6.0 Mantis: 0027278
                settings.Columns.Add(x =>
                {
                    x.FieldName = "SHOPCONTACT";
                    x.Caption = "Mobile no.";
                    x.VisibleIndex = 27;

                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOPCONTACT'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 150;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 150;
                    }
                });
                //End of Rev 6.0 Mantis: 0027278

                settings.Columns.Add(x =>
                {
                    //Rev 6.0 Mantis: 0027278
                    //x.FieldName = "PPADDR_CONTACT";
                    //x.Caption = "PP Address & Mobile no.";
                    //x.VisibleIndex = 27;
                    x.FieldName = "PPADDR";
                    x.Caption = "PP Address";
                    x.VisibleIndex = 28;
                    //End of Rev 6.0 Mantis: 0027278
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PPADDR'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 250;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 250;
                    }
                });

                //Rev 6.0 Mantis: 0027278
                settings.Columns.Add(x =>
                {
                    x.FieldName = "PPCONTACT";
                    x.Caption = "Mobile no.";
                    x.VisibleIndex = 29;

                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PPCONTACT'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 150;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 150;
                    }
                });
                //End of Rev 6.0 Mantis: 0027278

                settings.Columns.Add(x =>
                {
                    //Rev 6.0 Mantis: 0027278
                    //x.FieldName = "DDADDR_CONTACT";
                    //x.Caption = "DD Address & Mobile no.";
                    //x.VisibleIndex = 28;
                    x.FieldName = "DDADDR";
                    x.Caption = "DD Address";
                    x.VisibleIndex = 30;
                    //End of Rev 6.0 Mantis: 0027278

                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DDADDR'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 250;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 250;
                    }
                });

                //Rev 6.0 Mantis: 0027278
                settings.Columns.Add(x =>
                {
                    x.FieldName = "DDCONTACT";
                    x.Caption = "Mobile no.";
                    x.VisibleIndex = 31;

                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DDCONTACT'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 150;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 150;
                    }
                });
                //End of Rev 6.0 Mantis: 0027278

                settings.Columns.Add(x =>
                {
                    x.FieldName = "ALT_MOBILENO1";
                    x.Caption = "Alternate Phone No.";
                    x.VisibleIndex = 32;
                    
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ALT_MOBILENO1'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 150;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 150;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "SHOP_OWNER_EMAIL2";
                    x.Caption = "Alternate Email ID";
                    x.VisibleIndex = 33;
                    
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOP_OWNER_EMAIL2'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 150;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 150;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "MEETING_ADDRESS";
                    x.Caption = "Meeting Address";
                    x.VisibleIndex = 34;
                    
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='MEETING_ADDRESS'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 130;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 130;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "MEETINGREMARKS";
                    x.Caption = "Meeting Remarks";
                    x.VisibleIndex = 35;
                    
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='MEETINGREMARKS'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 120;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 120;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "VISITREMARKS";
                    x.Caption = "Feedback";
                    x.VisibleIndex = 36;
                    
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='VISITREMARKS'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 150;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 150;
                    }
                });

                //Rev 4.0 Mantis: 0027066 & 0027091
                settings.Columns.Add(x =>
                {
                    x.FieldName = "PARTYSTATUSTYPE";
                    x.Caption = "Type";
                    x.VisibleIndex = 37;
                    
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PARTYSTATUSTYPE'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "STAGE";
                    x.Caption = "Stage";
                    x.VisibleIndex = 38;
                    
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='STAGE'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 90;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 90;
                    }
                });
                //End of Rev 4.0 Mantis: 0027066 & 0027091

                settings.Columns.Add(x =>
                {
                    x.FieldName = "MULTI_CONTACT_NAME";
                    x.Caption = "Contact Name";
                    x.VisibleIndex = 39;
                    
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='MULTI_CONTACT_NAME'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 140;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 140;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "MULTI_CONTACT_NUMBER";
                    x.Caption = "Contact Number";
                    x.VisibleIndex = 40;
                    
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
                            x.ExportWidth = 140;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 140;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "TOTAL_VISIT";
                    x.Caption = "Visit Count";
                    x.VisibleIndex = 41;
                    
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
                            x.ExportWidth = 120;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 120;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "RE_VISITED";
                    x.Caption = "Re Visit";
                    x.VisibleIndex = 42;
                    
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
                            x.ExportWidth = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "NEWSHOP_VISITED";
                    x.Caption = "New Visit";
                    x.VisibleIndex = 43;
                    
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
                            x.ExportWidth = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "TOTMETTING";
                    x.Caption = "Meeting";
                    x.VisibleIndex = 44;
                    
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='TOTMETTING'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 90;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 90;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "SPENT_DURATION";
                    x.Caption = "Duration Spend";
                    x.VisibleIndex = 45;
                    
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
                            x.ExportWidth = 120;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 120;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "DISTANCE_TRAVELLED";
                    x.Caption = "Travelled(KM)";
                    x.VisibleIndex = 46;
                    
                    x.PropertiesEdit.DisplayFormatString = "0.00";
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
                            x.ExportWidth = 110;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 110;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "TOTAL_ORDER_BOOKED_VALUE";
                    x.Caption = "Sale Value";
                    x.VisibleIndex = 47;
                    
                    x.PropertiesEdit.DisplayFormatString = "0.00";
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='TOTAL_ORDER_BOOKED_VALUE'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "TOTAL_COLLECTION";
                    x.Caption = "Collection value";
                    x.VisibleIndex = 48;
                    
                    x.PropertiesEdit.DisplayFormatString = "0.00";
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='TOTAL_COLLECTION'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 120;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 120;
                    }
                });
            }
            //End of Rev 1.0 Mantis: 0025586
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
                int k = objgps.InsertPageRetention(Col, Session["userid"].ToString(), "PERFORMANCE SUMMARY");

                return Json(k, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
    }
}
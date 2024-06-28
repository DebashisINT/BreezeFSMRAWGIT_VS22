#region======================================Revision History=========================================================================
//1.0   V2 .0.39    PRITI       13/02/2023      0025663:Last Visit fields shall be available in Outlet Reports
//2.0   V2 .0.39    Debashis    12/05/2023      Optimization required for Employee Outlet Master.Refer: 0026020
//3.0   V2.0.41     Sanchita    30-05-2023      Employee Outlet Master : Report a column required "Status".
//                                              It will the Showing status 'Active' or 'Inactive'. refer: 26240
//4.0   V2 .0.41    Debashis    09/08/2023      A coloumn named as Gender needs to be added in all the ITC reports.Refer: 0026680
//5.0   V2.0.43     Sanchita    06/09/2023      A new user wise settings required named as ShowLatLongInOutletMaster. Refer: 26794
//6.0   V2.0.46     Sanchita    03/04/2024      0027343: Employee Outlet Master : Report parameter one check box required 'Consider Inactive Outlets'
//7.0   V2.0.47     Debashis    04/06/2024      In Employee Outlet Master report, add a “DS Type” column at the end.Refer:0027506
#endregion===================================End of Revision History==================================================================

using BusinessLogicLayer;
using BusinessLogicLayer.SalesTrackerReports;
using DataAccessLayer;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DocumentFormat.OpenXml.Spreadsheet;
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
    public class OutletMasterController : Controller
    {
        //
        // GET: /MYSHOP/OutletMaster/
        SalesSummary_Report objgps = new SalesSummary_Report();
        public ActionResult OutletMaster()
        {

            try
            {
                UserList lstuser = new UserList();
                OutletMasterReport omodel = new OutletMasterReport();
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

               //rev Pratik
                string h_id = omodel.modelbranch.First().BRANCH_ID.ToString();
               // ViewBag.HeadBranch = omodel.modelbranch;
                ViewBag.h_id = h_id;
                //End of erv Pratik

                return View(omodel);

            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetOutletMasterList(OutletMasterReport model)
        {
            try
            {
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
                List<OutletMasterListModel> omel = new List<OutletMasterListModel>();

                DataTable dt = new DataTable();
                string datfrmat = "";
                string dattoat = "";

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
                //List<object> BranchIDList = BranchGridLookup.GridView.GetSelectedFieldValues("ID");
                //foreach (object Branch in BranchIDList)
                //{
                //    Branch_Id += "," + Branch;
                //}
                //Branch_Id = Branch_Id.TrimStart(',');

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
                    //Mantis issue 0024778
                    //model.Fromdate = model.Fromdate.Split('-')[2] + '-' + model.Fromdate.Split('-')[1] + '-' + model.Fromdate.Split('-')[0];
                    if (model.is_pageload == "0")
                    {
                        model.Fromdate = model.Fromdate.Split('-')[0] + '-' + model.Fromdate.Split('-')[1] + '-' + model.Fromdate.Split('-')[2];
                    }
                    else
                    {
                        model.Fromdate = model.Fromdate.Split('-')[2] + '-' + model.Fromdate.Split('-')[1] + '-' + model.Fromdate.Split('-')[0];
                    }
                    //End of Mantis Issue 0024778
                }

                if (model.Todate == null)
                {
                    model.Todate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                }
                else
                {
                    //Mantis Issue 0024778
                   //model.Todate = model.Todate.Split('-')[2] + '-' + model.Todate.Split('-')[1] + '-' + model.Todate.Split('-')[0];
                    if (model.is_pageload == "0")
                    {
                        model.Todate = model.Todate.Split('-')[0] + '-' + model.Todate.Split('-')[1] + '-' + model.Todate.Split('-')[2];
                    }
                    else
                    {
                        model.Todate = model.Todate.Split('-')[2] + '-' + model.Todate.Split('-')[1] + '-' + model.Todate.Split('-')[0];
                    }
                    //End of Mantis Issue 0024778
                }

                string FromDate = model.Fromdate;
                string ToDate = model.Todate;

                if (model.is_pageload == "1")
                {
                    //double days = (Convert.ToDateTime(model.Todate) - Convert.ToDateTime(model.Fromdate)).TotalDays;
                    
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        omel = APIHelperMethods.ToModelList<OutletMasterListModel>(dt);
                        TempData["ExportEmployeeOutletMaster"] = omel;
                        TempData.Keep();
                    }
                    else
                    {
                        TempData["ExportEmployeeOutletMaster"] = null;
                        TempData.Keep();
                    }
                   // return PartialView("_PartialGridEmployeeOutletMaster", omel);
                    //return PartialView("_PartialGridEmployeeOutletMaster", LGetOutletMaster(Is_PageLoad));
                }
                //Mantis Issue 24728
                double days = (Convert.ToDateTime(ToDate) - Convert.ToDateTime(FromDate)).TotalDays;
                if (days <= 35)
                {
                    //Rev 2.0 0026020
                    //dt = GetEmployeeOutletMaster(Employee, FromDate, ToDate, Branch_Id);
                    // Rev 6.0
                    //dt = GetEmployeeOutletMaster(Employee, FromDate, ToDate, Branch_Id,model.is_pageload);
                    dt = GetEmployeeOutletMaster(Employee, FromDate, ToDate, Branch_Id, model.is_pageload, model.IsInactiveOutlets);
                    // End of Rev 6.0
                    //End of Rev 2.0 0026020
                }
                //dt = GetEmployeeOutletMaster(Employee, FromDate, ToDate, Branch_Id);
                //End of Mantis Issue 24728

                // Rev 5.0
                string userid = Session["userid"].ToString();
                string IsShowLatLongInOutletMaster = "0";
                DBEngine obj1 = new DBEngine();
                IsShowLatLongInOutletMaster = Convert.ToString(obj1.GetDataTable("select IsShowLatLongInOutletMaster from tbl_master_user WHERE user_id='" + userid + "'").Rows[0][0]);
                ViewBag.IsShowLatLongInOutletMaster = IsShowLatLongInOutletMaster;
                // End of Rev 5.0

                return PartialView("_PartialGridEmployeeOutletMaster", LGetOutletMaster(Is_PageLoad));
                //else
                //{
                //   // return PartialView("_PartialGridEmployeeOutletMaster", omel);
                //    return PartialView("_PartialGridEmployeeOutletMaster", LGetOutletMaster(Is_PageLoad));
                //}
                //return PartialView("_PartialGridEmployeeOutletMaster", LGetOutletMaster(Is_PageLoad));
            }
            catch
            {
                //   return Redirect("~/OMS/Signoff.aspx");
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
            
        }

        //Rev 2.0 0026020
        //public DataTable GetEmployeeOutletMaster(string Employee, string start_date, string end_date, string Branch_Id)
        // Rev 6.0
        //public DataTable GetEmployeeOutletMaster(string Employee, string start_date, string end_date, string Branch_Id,string IsPageLoad)
        public DataTable GetEmployeeOutletMaster(string Employee, string start_date, string end_date, string Branch_Id, string IsPageLoad, string IsInactiveOutlets)
        // End of Rev 6.0
        //End of Rev 2.0 0026020
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSEMPLOYEEOUTLETMASTER_REPORT");
            proc.AddPara("@FROMDATE", start_date);
            proc.AddPara("@TODATE", end_date);
            proc.AddPara("@BRANCHID", Branch_Id);
            proc.AddPara("@EMPID", Employee);
            //Rev 2.0 0026020
            proc.AddPara("@ISPAGELOAD", IsPageLoad);
            //End of Rev 2.0 0026020
            proc.AddPara("@USERID", Convert.ToInt32(Session["userid"]));
            // Rev 6.0
            proc.AddPara("@ISINACTIVEOUTLETS", IsInactiveOutlets);
            // End of Rev 6.0
            ds = proc.GetTable();
            return ds;
        }

        public IEnumerable LGetOutletMaster(string Is_PageLoad)
        {
            DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "OUTLET MASTER");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            }
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);

            if (Is_PageLoad != "is_pageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSEMPLOYEEOUTLETMASTER_REPORTs
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSEMPLOYEEOUTLETMASTER_REPORTs
                        where d.USERID == Convert.ToInt32(Userid) && d.EMPCODE == "0"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }



        }

        public ActionResult ExporOutletMasterList(int type)
        {


            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetOutletMasterGridViewSettings(), LGetOutletMaster(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetOutletMasterGridViewSettings(), LGetOutletMaster(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetOutletMasterGridViewSettings(), LGetOutletMaster(""));
                case 4:
                    return GridViewExtension.ExportToRtf(GetOutletMasterGridViewSettings(), LGetOutletMaster(""));
                case 5:
                    return GridViewExtension.ExportToCsv(GetOutletMasterGridViewSettings(), LGetOutletMaster(""));
                //break;

                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetOutletMasterGridViewSettings()
        {
            DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "OUTLET MASTER");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            }

            // Rev 5.0
            string userid = Session["userid"].ToString();
            string IsShowLatLongInOutletMaster = "0";
            DBEngine obj1 = new DBEngine();
            IsShowLatLongInOutletMaster = Convert.ToString(obj1.GetDataTable("select IsShowLatLongInOutletMaster from tbl_master_user WHERE user_id='" + userid + "'").Rows[0][0]);
            // End of Rev 5.0

            var settings = new GridViewSettings();
            settings.Name = "Reimbursement";
            settings.CallbackRouteValues = new { Controller = "OutletMaster", Action = "GetOutletMasterList" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Outlet Master List Report";

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
                x.Caption = "DS/TL ID";
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
                x.Caption = "DS/TL Name";
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
            //Rev 4.0 Mantis: 0026680
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
            //End of Rev 4.0 Mantis: 0026680

            settings.Columns.Add(x =>
            {
                x.FieldName = "OUTLETID";
                x.Caption = "Outlet ID";
                x.VisibleIndex = 7;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='OUTLETID'");
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
                x.FieldName = "OUTLETNAME";
                x.Caption = "Outlet Name";
                x.VisibleIndex = 8;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='OUTLETNAME'");
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
                x.FieldName = "OUTLETADDRESS";
                x.Caption = "Outlet Address";
                x.VisibleIndex = 9;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='OUTLETADDRESS'");
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
            //    column.Caption = "Outlet Contact No.";
            //    column.FieldName = "OUTLETCONTACT";
            //});
            settings.Columns.Add(x =>
            {
                x.FieldName = "OUTLETCONTACT";
                x.Caption = "Outlet Contact No.";
                x.VisibleIndex = 10;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='OUTLETCONTACT'");
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
            //    column.Caption = "Outlet Lat";
            //    column.FieldName = "OUTLETLAT";
            //});

            // Rev 5.0
            if (IsShowLatLongInOutletMaster == "True")
            {
            // End of Rev 5.0
                settings.Columns.Add(x =>
                {
                    x.FieldName = "OUTLETLAT";
                    x.Caption = "Latitude";
                    x.VisibleIndex = 11;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='OUTLETLAT'");
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
                //    column.Caption = "Outlet Lang";
                //    column.FieldName = "OUTLETLANG";
                //});
                settings.Columns.Add(x =>
                {
                    x.FieldName = "OUTLETLANG";
                    x.Caption = "Longitude";
                    x.VisibleIndex = 12;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='OUTLETLANG'");
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
            // Rev 5.0
            }
            // End of Rev 5.0
            
            //REV 1.0
            settings.Columns.Add(x =>
            {
                x.FieldName = "LASTVISITDATE";
                x.Caption = "Last Visit Date";
                x.VisibleIndex = 13;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='LASTVISITDATE'");
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
                x.FieldName = "LASTVISITTIME";
                x.Caption = "Last Visit Time";
                x.VisibleIndex = 14;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='LASTVISITTIME'");
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
                x.FieldName = "LASTVISITEDBY";
                x.Caption = "Last Visited By";
                x.VisibleIndex = 15;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='LASTVISITEDBY'");
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
            //REV 1.0 END
            // Rev 3.0
            settings.Columns.Add(x =>
            {
                x.FieldName = "OUTLETSTATUS";
                x.Caption = "Status";
                x.VisibleIndex = 16;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='OUTLETSTATUS'");
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
            // End of Rev 3.0
            // Rev 7.0 Mantis: 0027506
            settings.Columns.Add(x =>
            {
                x.FieldName = "DSTYPE";
                x.Caption = "DS Type";
                x.VisibleIndex = 17;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DSTYPE'");
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
            // End of Rev 7.0 Mantis: 0027506


            #region//rev Pratik
            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "USERID";
            //    x.Caption = "User Id";
            //    x.VisibleIndex = 12;
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
            //            //x.Width = 100;
            //        }
            //    }
            //    else
            //    {
            //        x.Visible = true;
            //        //x.Width = 100;
            //    }

            //});
            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "SEQ";
            //    x.Caption = "SEQ";
            //    x.VisibleIndex = 13;
            //    //x.Width = 180;
            //    // x.Width = System.Web.UI.WebControls.Unit.Percentage(20);
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
            //            //x.Width = 100;
            //        }
            //    }
            //    else
            //    {
            //        x.Visible = true;
            //        //x.Width = 100;
            //    }

            //});
            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "BRANCH_ID";
            //    x.Caption = "BRANCH ID";
            //    x.VisibleIndex = 14;
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
            //            //x.Width = 100;
            //        }
            //    }
            //    else
            //    {
            //        x.Visible = true;
            //        //x.Width = 100;
            //    }

            //});
            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "EMPCODE";
            //    x.Caption = "Employee Code";
            //    x.VisibleIndex = 15;
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
            //            //x.Width = 100;
            //        }
            //    }
            //    else
            //    {
            //        x.Visible = true;
            //        //x.Width = 100;
            //    }

            //});
            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "STATEID";
            //    x.Caption = "State Id";
            //    x.VisibleIndex = 16;
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
            //            //x.Width = 100;
            //        }
            //    }
            //    else
            //    {
            //        x.Visible = true;
            //        //x.Width = 100;
            //    }

            //});
            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "STATEID";
            //    x.Caption = "State Id";
            //    x.VisibleIndex = 17;
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
            //            //x.Width = 100;
            //        }
            //    }
            //    else
            //    {
            //        x.Visible = true;
            //        //x.Width = 100;
            //    }

            //});
            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "STATE";
            //    x.Caption = "State";
            //    x.VisibleIndex = 18;
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
            //            //x.Width = 100;
            //        }
            //    }
            //    else
            //    {
            //        x.Visible = true;
            //        //x.Width = 100;
            //    }

            //});
            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "DEG_ID";
            //    x.Caption = "Designation Id";
            //    x.VisibleIndex = 19;
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
            //            //x.Width = 100;
            //        }
            //    }
            //    else
            //    {
            //        x.Visible = true;
            //        //x.Width = 100;
            //    }

            //});
            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "DESIGNATION";
            //    x.Caption = "Designation";
            //    x.VisibleIndex = 20;
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
            //            //x.Width = 100;
            //        }
            //    }
            //    else
            //    {
            //        x.Visible = true;
            //        //x.Width = 100;
            //    }

            //});
            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "DATEOFJOINING";
            //    x.Caption = "DOJ";
            //    x.VisibleIndex = 21;
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
            //            //x.Width = 100;
            //        }
            //    }
            //    else
            //    {
            //        x.Visible = true;
            //        //x.Width = 100;
            //    }

            //});
            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "CONTACTNO";
            //    x.Caption = "Contact No.";
            //    x.VisibleIndex = 22;
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
            //            //x.Width = 100;
            //        }
            //    }
            //    else
            //    {
            //        x.Visible = true;
            //        //x.Width = 100;
            //    }

            //});
            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "REPORTTOID";
            //    x.Caption = "WD ID.";
            //    x.VisibleIndex = 23;
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
            //            //x.Width = 100;
            //        }
            //    }
            //    else
            //    {
            //        x.Visible = true;
            //        //x.Width = 100;
            //    }

            //});
            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "RPTTODESG";
            //    x.Caption = "WD Designation";
            //    x.VisibleIndex = 24;
            //    //x.Width = 180;
            //    // x.Width = System.Web.UI.WebControls.Unit.Percentage(20);
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
            //            //x.Width = 100;
            //        }
            //    }
            //    else
            //    {
            //        x.Visible = true;
            //        //x.Width = 100;
            //    }

            //});
            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "HREPORTTOID";
            //    x.Caption = "AE ID";
            //    x.VisibleIndex = 25;
            //    //rev Pratik
            //    x.PropertiesEdit.DisplayFormatString = "0.00";
            //    //End of rev Pratik
            //    //x.Width = 180;
            //    // x.Width = System.Web.UI.WebControls.Unit.Percentage(20);
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
            //            //x.Width = 100;
            //        }
            //    }
            //    else
            //    {
            //        x.Visible = true;
            //        //x.Width = 100;
            //    }

            //});
            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "HRPTTODESG";
            //    x.Caption = "AE Designation";
            //    x.VisibleIndex = 26;
            //    //x.Width = 180;
            //    // x.Width = System.Web.UI.WebControls.Unit.Percentage(20);
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
            //            //x.Width = 100;
            //        }
            //    }
            //    else
            //    {
            //        x.Visible = true;
            //        //x.Width = 100;
            //    }

            //});
            #endregion //End of rev Pratik
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

        public JsonResult GetEmployeeListDesigWise(string SearchKey, string Desig, string SearchKeyAE, string SearchKeyWD)
        {
            List<EmployeeModel> listEmployee = new List<EmployeeModel>();
            if (Session["userid"] != null)
            {
                SearchKey = SearchKey.Replace("'", "''");
                //BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();
                //DataTable Shop = oDBEngine.GetDataTable("select top(10)cnt_internalId,Replace(ISNULL(cnt_firstName,'')+' '+ISNULL(cnt_middleName,'')+ ' '+ISNULL(cnt_lastName,''),'''','&#39;') AS Employee_Name,cnt_UCC from tbl_master_contact where (cnt_firstName like '%" + SearchKey + "%') or  (cnt_middleName like '%" + SearchKey + "%') or  (cnt_lastName like '%" + SearchKey + "%')");
                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_EmployeeNameSearch_Desg");
                proc.AddPara("@USER_ID", Convert.ToInt32(Session["userid"]));
                proc.AddPara("@SearchKey", SearchKey);
                proc.AddPara("@DesigId", Desig);
                proc.AddPara("@Action", Desig);
                proc.AddPara("@AeId", SearchKeyAE);
                proc.AddPara("@WdId", SearchKeyWD);
                dt = proc.GetTable();

                listEmployee = (from DataRow dr in dt.Rows
                                select new EmployeeModel()
                                {
                                    id = Convert.ToString(dr["cnt_internalId"]),
                                    Employee_Code = Convert.ToString(dr["cnt_UCC"]),
                                    Employee_Name = Convert.ToString(dr["Employee_Name"])
                                }).ToList();
            }

            return Json(listEmployee,JsonRequestBehavior.AllowGet);
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
                int k = objgps.InsertPageRetention(Col, Session["userid"].ToString(), "OUTLET MASTER");

                return Json(k, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
	}
}
/*****************************************************************************************************
 * Rev 1.0      Sanchita       V2.0.40      04-05-2023      After Giving Selfie Attendance the Selfie is 
 *                                                          not sync-ed in the Portal. refer: 25962
 * Rev 2.0      Sanchita       V2.0.42      19/07/2023      Add Branch parameter in Listing of MIS - Attendance Register. Mantis : 26135
 * Rev 3.0      Sanchita       V2.0.42      11/08/2023      Two check box is required to show the first call time & last call time in Attendance Register Report
 *                                                          Mantis : 26707
 * Rev 4.0      Sanchita       V2.0.43      07-11-2023      0026895: System will prompt for Branch selection if the Branch hierarchy is activated.                                                         
 * Rev 5.0      Sanchita       V2.0.44      08-11-2023      In Attendance Register Report, Including Inactive users check box implementation is required
 *                                                          Mantis: 26954
 * *****************************************************************************************************/
using BusinessLogicLayer;
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
    public class HorizontalAttendanceController : Controller
    {
        // Rev 2.0
        UserList lstuser = new UserList();
        // End of Rev 2.0
        public ActionResult Index()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            
            ds.Tables.Add(dt);
            ds.Tables.Add(dt1);
            ds.Tables.Add(dt2);

            // Rev 2.0
            HorizontalAttendanceModel omodel = new HorizontalAttendanceModel();
            //string h_id = "";
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
            // Rev 4.0
            DBEngine obj1 = new DBEngine();
            ViewBag.BranchMandatory = Convert.ToString(obj1.GetDataTable("select [value] from FTS_APP_CONFIG_SETTINGS WHERE [Key]='IsActivateEmployeeBranchHierarchy'").Rows[0][0]);
            // End of Rev 4.0

            return View(ds);
        }

        public PartialViewResult PerformanceGridView(HorizontalAttendanceModel model)
        {
           
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


            string datfrmat = model.Fromdate.Split('-')[2] + '-' + model.Fromdate.Split('-')[1] + '-' + model.Fromdate.Split('-')[0];
            string dattoat = model.Todate.Split('-')[2] + '-' + model.Todate.Split('-')[1] + '-' + model.Todate.Split('-')[0];
            string Userid = Convert.ToString(Session["userid"]);

            string prod = "";
            int j = 1;

            if (model.ProductID != null && model.ProductID.Count > 0)
            {
                foreach (string item in model.ProductID)
                {
                    if (j > 1)
                        prod = prod + "," + item;
                    else
                        prod = item;
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
            //if (model.is_pageload != "0" && model.is_pageload != null)
            //{
            double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;
            //if (days <= 1)
            //{
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSHORIZONTALATTENDANCE_REPORT");
            //  proc.AddVarcharPara("@Action", 100, "GetAllDropDownDetailForSalesQuotation");
            proc.AddVarcharPara("@FROM_DATE", 10, datfrmat);
            proc.AddVarcharPara("@TO_DATE", 10, dattoat);
            proc.AddVarcharPara("@EMPID", 500, empcode);
            // Rev 2.0
            proc.AddVarcharPara("@BRANCHID", -1, empcode);
            // End of Rev 2.0
            ds = proc.GetDataSet();
            // }
            //}
            //else
            //{
            //    ds.Tables.Add(new DataTable());
            //    ds.Tables.Add(new DataTable());
            //    ds.Tables.Add(new DataTable());
            //}
            TempData["SalesGridView"] = ds;
            return PartialView(ds);
        }

        public PartialViewResult PerformanceGridViewCallback(HorizontalAttendanceModel model)
        {

            string frmdate = string.Empty;
            // Rev 1.0
            //String Path = System.Configuration.ConfigurationSettings.AppSettings["Path"];
            String Path = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
            // End of Rev 1.0
            String weburl = Path + "AttendanceImageDemo/";

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


            string datfrmat = model.Fromdate.Split('-')[2] + '-' + model.Fromdate.Split('-')[1] + '-' + model.Fromdate.Split('-')[0];
            string dattoat = model.Todate.Split('-')[2] + '-' + model.Todate.Split('-')[1] + '-' + model.Todate.Split('-')[0];
            string Userid = Convert.ToString(Session["userid"]);

            string prod = "";
            int j = 1;

            if (model.ProductID != null && model.ProductID.Count > 0)
            {
                foreach (string item in model.ProductID)
                {
                    if (j > 1)
                        prod = prod + "," + item;
                    else
                        prod = item;
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
                //End of Mantis Issue 24728
                //if (days <= 30)
                if (days <= 35)
                //Mantis Issue 24728
                {
                    ProcedureExecute proc = new ProcedureExecute("PRC_FTSHORIZONTALATTENDANCE_REPORT");
                    //  proc.AddVarcharPara("@Action", 100, "GetAllDropDownDetailForSalesQuotation");
                    proc.AddVarcharPara("@FROM_DATE", 10, datfrmat);
                    proc.AddVarcharPara("@TO_DATE", 10, dattoat);
                    proc.AddVarcharPara("@EMPID", 500, empcode);

                    proc.AddVarcharPara("@TotalKMTravelled", 10, model.TotalKMTravelled);
                    proc.AddVarcharPara("@SecondarySalesValue", 10, model.SecondarySalesValue);
                    proc.AddVarcharPara("@IdleTimeCount", 500, model.IdleTimeCount);
                    proc.AddVarcharPara("@ShowAttendanceSelfie", 10, model.ShowAttendanceSelfie);
                    proc.AddPara("@SelfieURL", weburl);

                    proc.AddVarcharPara("@Userid", 500, Userid);
                    //Mantise work 0025111
                    proc.AddVarcharPara("@ShowFullday", 500,Convert.ToString(model.ShowFullday));
                    proc.AddVarcharPara("@ISONLYLOGINDATA", 500,Convert.ToString(model.ShowLoginLocation));
                    proc.AddVarcharPara("@ISONLYLOGOUTDATA", 500,Convert.ToString(model.ShowLogoutLocation));
                    //End of Mantise work 0025111
                    // Rev 2.0
                    proc.AddVarcharPara("@BRANCHID", -1, Branch_Id);
                    // End of Rev 2.0
                    // Rev 3.0
                    proc.AddVarcharPara("@ShowFirstVisitTime", 500, Convert.ToString(model.ShowFirstCallTime));
                    proc.AddVarcharPara("@ShowLastVisitTime", 500, Convert.ToString(model.ShowLastCallTime));
                    // End of Rev 3.0
                    // Rev 5.0
                    proc.AddVarcharPara("@ShowInactiveUser", 500, Convert.ToString(model.ShowInactiveUser));
                    // End of Rev 5.0
                    ds = proc.GetDataSet();
                }
            }
            else
            {
                ds.Tables.Add(new DataTable());
                ds.Tables.Add(new DataTable());
                ds.Tables.Add(new DataTable());
            }
            TempData["SalesGridView"] = ds;
            return PartialView(ds);
        }


        public ActionResult ExportSalesGridView(int type)
        {
            //Rev Tanmoy get data through execute query
            //DataTable dbDashboardData = new DataTable();
            //DBEngine objdb = new DBEngine();
            //String query = TempData["DashboardGridView"].ToString();
            //dbDashboardData = objdb.GetDataTable(query);

            ViewData["SalesGridView"] = TempData["SalesGridView"];

            DataSet DS = (DataSet)ViewData["SalesGridView"];

            //End Rev
            TempData.Keep();

            if (ViewData["SalesGridView"] != null)
            {

                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetDashboardGridView(ViewData["SalesGridView"]), DS.Tables[1]);
                    //break;
                    case 2:
                        //return GridViewExtension.ExportToXlsx(GetDashboardGridView(ViewData["SalesGridView"]), DS.Tables[1]);
                        return GridViewExtension.ExportToXlsx(GetDashboardGridView(ViewData["SalesGridView"]), DS.Tables[1], new XlsxExportOptionsEx() { ExportType = ExportType.WYSIWYG });
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetDashboardGridView(ViewData["SalesGridView"]), DS.Tables[1]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetDashboardGridView(ViewData["SalesGridView"]), DS.Tables[1]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetDashboardGridView(ViewData["SalesGridView"]), DS.Tables[1]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetDashboardGridView(object dataset)
        {
            var settings = new GridViewSettings();
            settings.Name = "Daily Performance Report of Sales Personnel";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Daily Performance Report of Sales Personnel";
            String ID = Convert.ToString(TempData["SalesGridView"]);
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
    }
}
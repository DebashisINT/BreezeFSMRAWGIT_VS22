/*******************************************************************************************************************
 * Rev 1.0      Sanchita        V2.0.41     05/06/2023      Reports - Listing of Masters - Employee(s) - The Aadhar, PAN and Voter photo 
 *                                                          should show based on global settings. Refer: 26288
 ********************************************************************************************************************/
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
using DataAccessLayer;
using System.Web.UI.WebControls;
using DevExpress.Xpo.DB;
// Rev 1.0
using BusinessLogicLayer;
// End of Rev 1.0

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class EmpListController : Controller
    {
        UserList lstuser = new UserList();
        //SalesSummary_Report objgps = new SalesSummary_Report();
        // Rev 1.0
        CommonBL objSystemSettings = new CommonBL();
        // End of Rev 1.0

        public ActionResult Index()
        {

            try
            {

                EmpListModel model = new EmpListModel();
                string userid = Session["userid"].ToString();
                //omodel.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
               // omodel.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                //DataTable dt = objgps.GetStateMandatory(Session["userid"].ToString());
                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    ViewBag.StateMandatory = dt.Rows[0]["IsStateMandatoryinReport"].ToString();
                //}

                DataTable dtbranch = lstuser.GetHeadBranchList(Convert.ToString(Session["userbranchHierarchy"]), "HO");
                DataTable dtBranchChild = new DataTable();
                if (dtbranch.Rows.Count > 0)
                {
                    dtBranchChild = lstuser.GetChildBranch(Convert.ToString(Session["userbranchHierarchy"]));
                }
                model.modelbranch = APIHelperMethods.ToModelList<GetBranch>(dtbranch);

                return View(model);

            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
        public ActionResult GetSalesSummaryList1()
        {
            return PartialView("PartialGetSalesSummaryList", LGetEmpListReport("0"));
        }
        public ActionResult GetEmpList(EmpListModel model)
        {
            try
            {

                DataTable dt = new DataTable();
                //string frmdate = string.Empty;

                //if (model.is_pageload == "0")
                //{
                //    frmdate = "is_pageload";
                //}

                //if (model.Fromdate == null)
                //{
                //    model.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                //}

                //if (model.Todate == null)
                //{
                //    model.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                //}

                ViewData["ModelData"] = model;

                //string datfrmat = model.Fromdate.Split('-')[2] + '-' + model.Fromdate.Split('-')[1] + '-' + model.Fromdate.Split('-')[0];
                //string dattoat = model.Todate.Split('-')[2] + '-' + model.Todate.Split('-')[1] + '-' + model.Todate.Split('-')[0];
                string Userid = Convert.ToString(Session["userid"]);


                string Branch = "";
                int i = 1;

                if (model.BranchId != null && model.BranchId.Count > 0)
                {
                    foreach (string item in model.BranchId)
                    {
                        if (i > 1)
                            Branch = Branch + "," + item;
                        else
                            Branch = item;
                        i++;
                    }

                }

                //string desig = "";
                //int j = 1;

                //if (model.desgid != null && model.desgid.Count > 0)
                //{
                //    foreach (string item in model.desgid)
                //    {
                //        if (j > 1)
                //            desig = desig + "," + item;
                //        else
                //            desig = item;
                //        j++;
                //    }

                //}

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

                ////if (days <= 7)
                //if (days <= 30)
                //{
                //    dt = objgps.GetSalesPerformanceReport(datfrmat, dattoat, Userid, state, desig, empcode);
                //}

                string Is_PageLoad = string.Empty;

                if (model.is_pageload == "0")
                {
                    Is_PageLoad = "is_pageload";
                   
                }

                //if (model.is_pageload == "1")
                //{
                //    dt = GetEmpListReport(empcode, Branch);

                //    return PartialView("PartialEmpList", LGetEmpListReport(Is_PageLoad));
                //}

                //else
                //{
                //    return PartialView("PartialEmpList", LGetEmpListReport(Is_PageLoad));
                //}
                dt = GetEmpListReport(empcode, Branch);
                
                // Rev 1.0
                string IsShowIdentityPhotoInEmployeeReport = objSystemSettings.GetSystemSettingsResult("IsShowIdentityPhotoInEmployeeReport");//REV 1.0
                ViewBag.IsShowIdentityPhotoInEmployeeReport = IsShowIdentityPhotoInEmployeeReport;
                // End of Rev 1.0

                return PartialView("PartialEmpList", LGetEmpListReport(Is_PageLoad));


            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }

        public DataTable GetEmpListReport(string Employee, string Branch)
        {
            DataTable ds = new DataTable();
            try
            {
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];

                ProcedureExecute proc = new ProcedureExecute("Get_Employees_List");
                proc.AddPara("@BranchId", Branch);
                proc.AddPara("@EmpID", Employee);
                proc.AddPara("@User_id", Convert.ToInt32(Session["userid"]));
                proc.AddPara("@weburl", weburl);
                ds = proc.GetTable();
            }
             catch
             {
                // return RedirectToAction("Logout", "Login", new { Area = "" });

             }
            return ds;
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

        //public ActionResult GetDesigList()
        //{
        //    try
        //    {
        //        DataTable dtdesig = lstuser.Getdesiglist();
        //        List<GetDesignation> modeldesig = new List<GetDesignation>();
        //        modeldesig = APIHelperMethods.ToModelList<GetDesignation>(dtdesig);


        //        return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_DesigPartial.cshtml", modeldesig);



        //    }
        //    catch
        //    {
        //        return RedirectToAction("Logout", "Login", new { Area = "" });

        //    }
        //}

        //public ActionResult GetEmpList(EmpList model)
        //{
        //    try
        //    {
        //        string Branch = "";
        //        int i = 1;

        //        if (model.BranchId != null && model.BranchId.Count > 0)
        //        {
        //            foreach (string item in model.BranchId)
        //            {
        //                if (i > 1)
        //                    Branch = Branch + "," + item;
        //                else
        //                    Branch = item;
        //                i++;
        //            }

        //        }

        //        //string desig = "";
        //        //int k = 1;

        //        //if (model.desgid != null && model.desgid.Count > 0)
        //        //{
        //        //    foreach (string item in model.desgid)
        //        //    {
        //        //        if (k > 1)
        //        //            desig = desig + "," + item;
        //        //        else
        //        //            desig = item;
        //        //        k++;
        //        //    }

        //        //}



        //        DataTable dtemp = lstuser.Getemplist("","");
        //        List<GetAllEmployee> modelemp = new List<GetAllEmployee>();
        //        modelemp = APIHelperMethods.ToModelList<GetAllEmployee>(dtemp);

        //        return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_EmpPartial.cshtml", modelemp);



        //    }
        //    catch
        //    {
        //        return RedirectToAction("Logout", "Login", new { Area = "" });

        //    }
        //}

        public IEnumerable LGetEmpListReport(string frmdate)
        {
            //DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "EMPLOYEE LIST");
            //if (dtColmn != null && dtColmn.Rows.Count > 0)
            //{
            //    ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            //}

            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);

            if (frmdate != "is_pageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTS_EMPLIST_REPORTs
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.Code ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTS_EMPLIST_REPORTs
                        where d.USERID == Convert.ToInt32(Userid) && d.Code == "0"
                        orderby d.Code ascending
                        select d;
                return q;
            }



        }


        public ActionResult ExporEmpList(int type)
        {


            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(), LGetEmpListReport(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(), LGetEmpListReport(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(), LGetEmpListReport(""));
                case 4:
                    return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(), LGetEmpListReport(""));
                case 5:
                    return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(), LGetEmpListReport(""));
                //break;

                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetEmployeeBatchGridViewSettings()
        {
            //DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "PERFORMANCE SUMMARY");
            //if (dtColmn != null && dtColmn.Rows.Count > 0)
            //{
            //    ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            //}

            // Rev 1.0
            string IsShowIdentityPhotoInEmployeeReport = objSystemSettings.GetSystemSettingsResult("IsShowIdentityPhotoInEmployeeReport");//REV 1.0
            // End of Rev 1.0

            var settings = new GridViewSettings();
            settings.Name = "Employee List";
            settings.CallbackRouteValues = new { Controller = "EmpList", Action = "GetEmpList" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Employee(s) List";
            


            settings.Columns.Add(x =>
            {
                x.FieldName = "Code";
                x.Caption = "Employee Code";
                x.VisibleIndex = 1;
                //x.Width = 100;
                //x.Width = System.Web.UI.WebControls.Unit.Percentage(15);
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Code'");
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
                x.FieldName = "Name";
                x.Caption = "Name";
                x.VisibleIndex = 2;
                //x.Width = 100;
                //x.Width = System.Web.UI.WebControls.Unit.Percentage(15);
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Name'");
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
                x.FieldName = "user_loginId";
                x.Caption = "User Id";
                x.VisibleIndex = 3;
                //x.Width = 100;
                //x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Code'");
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
                x.FieldName = "Employee_Grade";
                x.Caption = "Grade";
                x.VisibleIndex = 4;
                //x.Width = 100;
                // x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Employee_Grade'");
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

            // Mantis Issue 25421
            settings.Columns.Add(x =>
            {
                x.FieldName = "Beat";
                x.Caption = "Beat";
                x.VisibleIndex = 5;
                //x.Width = 100;
                // x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Beat'");
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
            // End of Mantis Issue 25421

            // Mantis Issue 24736
            settings.Columns.Add(x =>
            {
                x.FieldName = "cnt_OtherID";
                x.Caption = "Other ID";
                x.VisibleIndex = 6;
                //x.Width = 100;
                // x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='cnt_OtherID'");
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
            // End of Mantis Issue 24736

            settings.Columns.Add(x =>
            {
                x.FieldName = "Company";
                x.Caption = "Company";
                x.VisibleIndex = 7;
                //x.Width = 120;
                x.ColumnType = MVCxGridViewColumnType.TextBox;
                // x.Width = System.Web.UI.WebControls.Unit.Percentage(15);
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Company'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                        //x.Width = 0;
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
                x.FieldName = "BranchName";
                x.Caption = "Branch";
                x.VisibleIndex = 8;
                //x.Width = 180;
                x.ColumnType = MVCxGridViewColumnType.TextBox;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='BranchName'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                        //x.Width = 0;
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

            settings.Columns.Add(x =>
            {
                x.FieldName = "Department";
                x.Caption = "Department";
                x.VisibleIndex = 9;
                //x.Width = 180;
                // x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Department'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                        //x.Width = 0;
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

            settings.Columns.Add(x =>
            {
                x.FieldName = "Designation";
                x.Caption = "Designation";
                x.VisibleIndex = 10;
                //x.Width = 100;
                // x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Designation'");
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
                x.FieldName = "DOJ";
                x.Caption = "Joining On";
                x.VisibleIndex = 11;
                //x.Width = 100;
                // x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DOJ'");
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
                x.FieldName = "ReportTo";
                x.Caption = "Report To";
                x.VisibleIndex = 12;
                //x.Width = 100;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ReportTo'");
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

            // Rev 1.0
            settings.Columns.Add(x =>
            {
                x.FieldName = "FacePhoto";
                x.Caption = "Face Photo";
                x.VisibleIndex = 13;
                x.ColumnType = MVCxGridViewColumnType.HyperLink;
                //x.Width = 100;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='FacePhoto'");
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

            if (IsShowIdentityPhotoInEmployeeReport == "1")
            {
                settings.Columns.Add(x =>
                {
                    x.FieldName = "AadharPhoto";
                    x.Caption = "Aadhar Photo";
                    x.VisibleIndex = 14;
                    x.ColumnType = MVCxGridViewColumnType.HyperLink;
                    //x.Width = 100;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='AadharPhoto'");
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
                    x.FieldName = "PANPhoto";
                    x.Caption = "PAN Photo";
                    x.VisibleIndex = 15;
                    x.ColumnType = MVCxGridViewColumnType.HyperLink;
                    //x.Width = 100;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PANPhoto'");
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
                    x.FieldName = "VoterPhoto";
                    x.Caption = "Voter Photo";
                    x.VisibleIndex = 16;
                    x.ColumnType = MVCxGridViewColumnType.HyperLink;
                    //x.Width = 100;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='VoterPhoto'");
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
            }
            // End of Rev 1.0

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

        public ActionResult GetBranchList()
        {
            BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(string.Empty);
            try
            {
                List<GetBranch> modelbranch = new List<GetBranch>();
                //DataTable dtbranch = lstuser.GetHeadBranchList(Hoid, Hoid);
                //DataTable dtBranchChild = new DataTable();
                DataTable ComponentTable = new DataTable();
                //if ()
                //{

                //}

                
                //if (Hoid != "0")
                //{
                //    Session["Hoid"] = Hoid;
                //    ComponentTable = lstuser.GetBranch(Convert.ToString(Session["userbranchHierarchy"]), Hoid);
                //}
                //else
                //{
                //    Session["Hoid"] = Hoid;
                //    //ComponentTable = oDBEngine.GetDataTable("select * from (select branch_id as ID,branch_description,branch_code from tbl_master_branch a where a.branch_id=1  union all select branch_id as ID,branch_description,branch_code from tbl_master_branch b where b.branch_parentId=1) a order by branch_description");
                //    ComponentTable = oDBEngine.GetDataTable("select * from (select branch_id as BRANCH_ID,branch_description as CODE,branch_code from tbl_master_branch a where a.branch_id=1  union all select branch_id as BRANCH_ID,branch_description as CODE,branch_code from tbl_master_branch b where b.branch_parentId=1) a order by CODE");
                //}
                ComponentTable = oDBEngine.GetDataTable("select branch_id as BRANCH_ID,branch_description as CODE from tbl_master_branch");
                modelbranch = APIHelperMethods.ToModelList<GetBranch>(ComponentTable);
                return PartialView("~/Areas/MYSHOP/Views/EmpList/_BranchPartial.cshtml", modelbranch);

            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
        public JsonResult GetBranchListSelectAll()
        {
            BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(string.Empty);
            try
            {
                List<GetBranch> modelbranch = new List<GetBranch>();
                //DataTable dtbranch = lstuser.GetHeadBranchList(Hoid, Hoid);
                //DataTable dtBranchChild = new DataTable();
                DataTable ComponentTable = new DataTable();
                ComponentTable = oDBEngine.GetDataTable("select branch_id as BRANCH_ID,branch_description as CODE from tbl_master_branch");
                modelbranch = APIHelperMethods.ToModelList<GetBranch>(ComponentTable);
                return Json(modelbranch, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        //public ActionResult PageRetention(List<String> Columns)
        //{
        //    try
        //    {
        //        String Col = "";
        //        int i = 1;
        //        if (Columns != null && Columns.Count > 0)
        //        {
        //            Col = string.Join(",", Columns);
        //        }
        //        int k = objgps.InsertPageRetention(Col, Session["userid"].ToString(), "EMPLOYEE LIST");

        //        return Json(k, JsonRequestBehavior.AllowGet);
        //    }
        //    catch
        //    {
        //        return RedirectToAction("Logout", "Login", new { Area = "" });
        //    }
        //}

	}
}
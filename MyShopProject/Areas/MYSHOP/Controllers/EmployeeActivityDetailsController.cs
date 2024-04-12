//====================================================== Revision History ==========================================================
//1.0  20-07-2023   V2.0.42   Priti     0026135: Branch Parameter is required for various FSM reports
//2.0  07-11-2023   V2.0.43   Sanchita  0026895: System will prompt for Branch selection if the Branch hierarchy is activated.
//3.0  18-01-2024   V2.0.44   Pallab    0027197: Compact column width required in the Employee Activity Details report grid excel export.
//====================================================== Revision History ==========================================================
using BusinessLogicLayer.SalesmanTrack;
using BusinessLogicLayer.SalesTrackerReports;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using Models;
using MyShop.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//Rev 1.0
using SalesmanTrack;
using UtilityLayer;
using BusinessLogicLayer;
//Rev 1.0 End
namespace MyShop.Areas.MYSHOP.Controllers
{
    public class EmployeeActivityDetailsController : Controller
    {
        //Rev 1.0
        UserList lstuser = new UserList();
        //Rev 1.0 End
        EmployeeActivityDetailsBL objshop = new EmployeeActivityDetailsBL();
        public ActionResult Index()
        {
            try
            {
                string userid = Session["userid"].ToString();

                EmployeeActivityDetailsModel omodel = new EmployeeActivityDetailsModel();
                omodel.FromDate = DateTime.Now.ToString("dd-MM-yyyy");
                omodel.ToDate = DateTime.Now.ToString("dd-MM-yyyy");

                DataTable dt = new SalesSummary_Report().GetStateMandatory(Session["userid"].ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewBag.StateMandatory = dt.Rows[0]["IsStateMandatoryinReport"].ToString();
                }
                // Rev 2.0
                DBEngine obj1 = new DBEngine();
                ViewBag.BranchMandatory = Convert.ToString(obj1.GetDataTable("select [value] from FTS_APP_CONFIG_SETTINGS WHERE [Key]='IsActivateEmployeeBranchHierarchy'").Rows[0][0]);
                // End of Rev 2.0
                // omodel.UserID = userid;

                //REV 1.0
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
                //REV 1.0 End
                return View(omodel);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetEmployeeActivityDetailslistPartial(EmployeeActivityDetailsModel model)
        {
            try
            {
                List<EmployeeActiveDetailsLists> omel = new List<EmployeeActiveDetailsLists>();
                DataTable dt = new DataTable();
                string datfrmat = "";
                string dattoat = "";

                string Employee = "";
                int i = 1;

                if (model.EmployeeID != null && model.EmployeeID.Count > 0)
                {
                    foreach (string item in model.EmployeeID)
                    {
                        if (i > 1)
                            Employee = Employee + "," + item;
                        else
                            Employee = item;
                        i++;
                    }
                }

                string State_id = "";
                int j = 1;
                if (model.State != null && model.State.Count > 0)
                {
                    foreach (string item in model.State)
                    {
                        if (j > 1)
                            State_id = State_id + "," + item;
                        else
                            State_id = item;
                        j++;
                    }
                }

                string Designation_id = "";
                int k = 1;
                if (model.Designation_id != null && model.Designation_id.Count > 0)
                {
                    foreach (string item in model.Designation_id)
                    {
                        if (k > 1)
                            Designation_id = Designation_id + "," + item;
                        else
                            Designation_id = item;
                        k++;
                    }
                }

                string Is_PageLoad = string.Empty;

                if (model.Ispageload == "0")
                {
                    Is_PageLoad = "Ispageload";
                    model.FromDate = DateTime.Now.AddDays(-1).ToString("dd-MM-yyyy");
                    model.ToDate = DateTime.Now.AddDays(-1).ToString("dd-MM-yyyy");
                }

                if (model.FromDate == null)
                {
                    model.FromDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                }
                else
                {
                    model.FromDate = model.FromDate.Split('-')[2] + '-' + model.FromDate.Split('-')[1] + '-' + model.FromDate.Split('-')[0];
                }

                if (model.ToDate == null)
                {
                    model.ToDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                }
                else
                {
                    model.ToDate = model.ToDate.Split('-')[2] + '-' + model.ToDate.Split('-')[1] + '-' + model.ToDate.Split('-')[0];
                }

                string FromDate = model.FromDate;
                string ToDate = model.ToDate;
                string userID = Convert.ToString(Session["userid"]);

                double days = (Convert.ToDateTime(model.ToDate) - Convert.ToDateTime(model.FromDate)).TotalDays;

                //Rev 1.0
                string Branch_Id = "";
                int l = 1;
                if (model.BranchId != null && model.BranchId.Count > 0)
                {
                    foreach (string item in model.BranchId)
                    {
                        if (l > 1)
                            Branch_Id = Branch_Id + "," + item;
                        else
                            Branch_Id = item;
                        l++;
                    }
                }
                //Rev 1.0 End

                if (days <= 30)
                {
                    if (model.Ispageload != "0")
                    {
                        //Rev 1.0
                        //dt = objshop.GenerateEmployeeActivityDetailsData(Employee, FromDate, ToDate, State_id, Designation_id, Convert.ToInt64(userID));
                        dt = objshop.GenerateEmployeeActivityDetailsData(Employee, FromDate, ToDate, State_id, Designation_id, Convert.ToInt64(userID), Branch_Id);
                        //Rev 1.0 End
                    }
                }
                return PartialView("_PartialGridEmployeeActivityDetails", GetEmployeeActivityDetails(Is_PageLoad));
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public IEnumerable GetEmployeeActivityDetails(string Is_PageLoad)
        {
            DataTable dtColmn = objshop.GetPageRetention(Session["userid"].ToString(), "EMPLOYEE ACTIVITY DETAILS");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string userID = Convert.ToString(Session["userid"]);

            if (Is_PageLoad != "Ispageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSEMPLOYEEACTIVITYDETAILS_REPORTs
                        where d.LOGIN_ID == Convert.ToInt32(userID)
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                if (Is_PageLoad != "Ispageload")
                {
                    ReportsDataContext dc = new ReportsDataContext(connectionString);
                    var q = from d in dc.FTSEMPLOYEEACTIVITYDETAILS_REPORTs
                            where d.LOGIN_ID == Convert.ToInt32(userID)
                            orderby d.SEQ ascending
                            select d;
                    return q;
                }
                else
                {
                    return null;
                }
            }
        }

        public ActionResult ExportActivityDetailslist(int type)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(), GetEmployeeActivityDetails(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(), GetEmployeeActivityDetails(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(), GetEmployeeActivityDetails(""));
                //break;
                case 4:
                    return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(), GetEmployeeActivityDetails(""));
                //break;
                case 5:
                    return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(), GetEmployeeActivityDetails(""));
                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetEmployeeBatchGridViewSettings()
        {
            DataTable dtColmn = objshop.GetPageRetention(Session["userid"].ToString(), "EMPLOYEE ACTIVITY DETAILS");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;
            }

            var settings = new GridViewSettings();
            settings.Name = "Employee Activity Details";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "EmployeeActivityDetails";
            //Rev 3.0 Mantis: "column.ExportWidth" added in all columns and column width adjustment for export excel
            settings.Columns.Add(column =>
            {
                column.Caption = "Employee ID";
                column.FieldName = "EMPID";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='EMPID'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 200;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 200;
                }
                column.FixedStyle = GridViewColumnFixedStyle.Left;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Employee Name";
                column.FieldName = "EMPNAME";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='EMPNAME'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 200;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 200;
                }
                column.FixedStyle = GridViewColumnFixedStyle.Left;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "State Name";
                column.FieldName = "STATE_NAME";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='STATE_NAME'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 200;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 200;
                }
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Branch";
                column.FieldName = "BRANCHDESC";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='BRANCHDESC'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 150;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 150;
                }
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Designation";
                column.FieldName = "DESIGNATION";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DESIGNATION'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 180;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 180;
                }
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Parent Customer";
                column.FieldName = "PCUSTNAME";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PCUSTNAME'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 210;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 210;
                }
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Customer Name";
                column.FieldName = "CUSTNAME";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CUSTNAME'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 150;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 150;
                }
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Customer Type";
                column.FieldName = "CUSTTYPE";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CUSTTYPE'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 150;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 150;
                }
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Entity Type";
                column.FieldName = "ENTITYTYPE";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ENTITYTYPE'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 110;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 110;
                }
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Mobile No.";
                column.FieldName = "MOBILE_NO";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='MOBILE_NO'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 110;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 110;
                }
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Location";
                column.FieldName = "LOCATION";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='LOCATION'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 280;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 280;
                }
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Visit Date";
                column.FieldName = "VISITDATE";
                column.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='VISITDATE'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 110;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 110;
                }
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Visit Time";
                column.FieldName = "VISITTIME";
                column.PropertiesEdit.DisplayFormatString = "hh:mm:ss";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='VISITTIME'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 110;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 110;
                }
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Duration";
                column.FieldName = "DURATION";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DURATION'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 100;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 100;
                }

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Visit Type";
                column.FieldName = "VISIT_TYPE";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='VISIT_TYPE'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 90;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 90;
                }
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Distance";
                column.FieldName = "DISTANCE";
                column.PropertiesEdit.DisplayFormatString = "0.00";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DISTANCE'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 100;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 100;
                }
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Remarks";
                column.FieldName = "REMARKS";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='REMARKS'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 90;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 90;
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
                int k = objshop.InsertPageRetention(Col, Session["userid"].ToString(), "EMPLOYEE ACTIVITY DETAILS");

                return Json(k, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
    }
}
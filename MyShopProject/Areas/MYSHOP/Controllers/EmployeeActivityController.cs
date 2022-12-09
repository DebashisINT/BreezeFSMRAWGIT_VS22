using BusinessLogicLayer.SalesmanTrack;
using BusinessLogicLayer.SalesTrackerReports;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using MyShop.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class EmployeeActivityController : Controller
    {
        EmployeeActivityBL objshop = new EmployeeActivityBL();
        public ActionResult Index()
        {
            try
            {
                string userid = Session["userid"].ToString();

                EmployeeActivityModel omodel = new EmployeeActivityModel();
                omodel.FromDate = DateTime.Now.ToString("dd-MM-yyyy");
                omodel.ToDate = DateTime.Now.ToString("dd-MM-yyyy");

                DataTable dt = new SalesSummary_Report().GetStateMandatory(Session["userid"].ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewBag.StateMandatory = dt.Rows[0]["IsStateMandatoryinReport"].ToString();
                }
                // omodel.UserID = userid;
                return View(omodel);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetEmployeeActivitylistPartial(EmployeeActivityModel model)
        {
            try
            {
                List<EmployeeActiveLists> omel = new List<EmployeeActiveLists>();
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
                //Rev Debashis Mantis: 0025104
                if (model.IsOnlyLoginData != null)
                {
                    TempData["IsOnlyLoginData"] = model.IsOnlyLoginData;
                    TempData.Keep();
                }
                if (model.IsOnlyLogoutData != null)
                {
                    TempData["IsOnlyLogoutData"] = model.IsOnlyLogoutData;
                    TempData.Keep();
                }
                //End of Rev Debashis Mantis: 0025104
                double days = (Convert.ToDateTime(model.ToDate) - Convert.ToDateTime(model.FromDate)).TotalDays;

                if (days <= 7)
                {
                    if (model.Ispageload != "0")
                    {
                        //Rev Debashis Mantis: 0025104
                        //dt = objshop.GenerateAnalysisSummaryData(Employee, FromDate, ToDate, Convert.ToInt64(userID), State_id, Designation_id);
                        dt = objshop.GenerateAnalysisSummaryData(Employee, FromDate, ToDate, Convert.ToInt64(userID), State_id, Designation_id,model.IsOnlyLoginData,model.IsOnlyLogoutData);
                        //End of Rev Debashis Mantis: 0025104
                    }
                }
                // return PartialView("_PartialGridEmployeeActivity", GetEmployeeActive(Is_PageLoad));
                //if (dt.Rows.Count > 0)
                //{
                //    //  omel = APIHelperMethods.ToModelList<ListHomeLocation>(dt);
                //    TempData["ExportEmployeeActivity"] = omel;
                //    TempData.Keep();
                //}
                //else
                //{
                //    TempData["ExportEmployeeActivity"] = null;
                //    TempData.Keep();
                //}
                return PartialView("_PartialGridEmployeeActivity", GetEmployeeActive(Is_PageLoad));
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public IEnumerable GetEmployeeActive(string Is_PageLoad)
        {
            DataTable dtColmn = objshop.GetPageRetention(Session["userid"].ToString(), "EMPLOYEE ACTIVITY");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            }

            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string userID = Convert.ToString(Session["userid"]);

            if (Is_PageLoad != "Ispageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSACTIVITY_REPORTs
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
                    var q = from d in dc.FTSACTIVITY_REPORTs
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

        public ActionResult ExportActivitylist(int type)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(), GetEmployeeActive(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(), GetEmployeeActive(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(), GetEmployeeActive(""));
                //break;
                case 4:
                    return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(), GetEmployeeActive(""));
                //break;
                case 5:
                    return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(), GetEmployeeActive(""));
                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetEmployeeBatchGridViewSettings()
        {
            DataTable dtColmn = objshop.GetPageRetention(Session["userid"].ToString(), "EMPLOYEE ACTIVITY");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            }

            var settings = new GridViewSettings();
            settings.Name = "Employee Activity";
            // settings.CallbackRouteValues = new { Controller = "Employee", Action = "ExportEmployee" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Employee Activity";

            settings.Columns.Add(column =>
            {
                column.Caption = "Employee ID";
                column.FieldName = "Employee_ID";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Employee_ID'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Employee Name";
                column.FieldName = "Employee_Name";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Employee_Name'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "State Name";
                column.FieldName = "State_name";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='State_name'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
            });

            //Rev Debashis -- 0024575
            settings.Columns.Add(column =>
            {
                column.Caption = "District";
                column.FieldName = "SHOP_DISTRICT";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOP_DISTRICT'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Pincode";
                column.FieldName = "SHOP_PINCODE";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOP_PINCODE'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
            });
            //End of Rev Debashis -- 0024575

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
                    }
                }
                else
                {
                    column.Visible = true;
                }
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Designation";
                column.FieldName = "Designation";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Designation'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Shop Name";
                column.FieldName = "SHOP_NAME";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOP_NAME'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Code";
                column.FieldName = "ENTITYCODE";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ENTITYCODE'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Shop Type";
                column.FieldName = "SHOP_TYPE";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOP_TYPE'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
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
                    }
                }
                else
                {
                    column.Visible = true;
                }
            });

            //Rev Debashis -- 0024577
            settings.Columns.Add(column =>
            {
                column.Caption = "Alternate Phone No.";
                column.FieldName = "ALT_MOBILENO1";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ALT_MOBILENO1'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Alternate Email ID";
                column.FieldName = "SHOP_OWNER_EMAIL2";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOP_OWNER_EMAIL2'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
            });
            //End of Rev Debashis -- 0024577

            //Rev Debashis -- 0024575
            settings.Columns.Add(column =>
            {
                column.Caption = "Cluster";
                column.FieldName = "SHOP_CLUSTER";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOP_CLUSTER'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
            });
            //End of Rev Debashis -- 0024575

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
                    }
                }
                else
                {
                    column.Visible = true;
                }
            });



            settings.Columns.Add(column =>
            {
                column.Caption = "Visit Time";
                column.FieldName = "VISIT_TIME";
                column.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy hh:mm:ss";
                // (column.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='VISIT_TIME'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
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
                    }
                }
                else
                {
                    column.Visible = true;
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
                    }
                }
                else
                {
                    column.Visible = true;
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
                    }
                }
                else
                {
                    column.Visible = true;
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
                    }
                }
                else
                {
                    column.Visible = true;
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
                int k = objshop.InsertPageRetention(Col, Session["userid"].ToString(), "EMPLOYEE ACTIVITY");

                return Json(k, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
    }
}
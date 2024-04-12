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
    public class CustomerVisitDetailsController : Controller
    {
        CustomerVisitDetailsBL objshop = new CustomerVisitDetailsBL();
        public ActionResult Index()
        {
            try
            {
                string userid = Session["userid"].ToString();

                CustomerVisitDetailsModel omodel = new CustomerVisitDetailsModel();
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

        public ActionResult GetCustomerVisitDetailslistPartial(EmployeeActivityDetailsModel model)
        {
            try
            {
                List<CustomerVisitDetailsLists> omel = new List<CustomerVisitDetailsLists>();
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

                if (days <= 30)
                {
                    if (model.Ispageload != "0")
                    {
                        dt = objshop.GenerateCustomerVisitDetailsData(Employee, FromDate, ToDate, State_id, Designation_id, Convert.ToInt64(userID));
                    }
                }
                return PartialView("_PartialGridCustomerVisitDetails", GetCustomerVisitDetails(Is_PageLoad));
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public IEnumerable GetCustomerVisitDetails(string Is_PageLoad)
        {
            DataTable dtColmn = objshop.GetPageRetention(Session["userid"].ToString(), "CUSTOMER VISIT");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string userID = Convert.ToString(Session["userid"]);

            if (Is_PageLoad != "Ispageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSCUSTOMERVISITDETAILS_REPORTs
                        where d.USERID == Convert.ToInt32(userID)
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                if (Is_PageLoad != "Ispageload")
                {
                    ReportsDataContext dc = new ReportsDataContext(connectionString);
                    var q = from d in dc.FTSCUSTOMERVISITDETAILS_REPORTs
                            where d.USERID == Convert.ToInt32(userID)
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
                    return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(), GetCustomerVisitDetails(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(), GetCustomerVisitDetails(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(), GetCustomerVisitDetails(""));
                //break;
                case 4:
                    return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(), GetCustomerVisitDetails(""));
                //break;
                case 5:
                    return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(), GetCustomerVisitDetails(""));
                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetEmployeeBatchGridViewSettings()
        {
            DataTable dtColmn = objshop.GetPageRetention(Session["userid"].ToString(), "CUSTOMER VISIT");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;
            }

            var settings = new GridViewSettings();
            settings.Name = "Customer Visit";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "CustomerVisitDetails";

            settings.Columns.Add(column =>
            {
                column.Caption = "Date";
                column.FieldName = "WORK_DATE";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='WORK_DATE'");
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
                column.Caption = "State";
                column.FieldName = "STATE";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='STATE'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 120;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 120;
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
                        column.ExportWidth = 120;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 120;
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
                        column.ExportWidth = 120;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 120;
                }
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Parent Customer(Retailer)";
                column.FieldName = "COMPNAME";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='COMPNAME'");
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
                column.Caption = "Parent Customer(GPTPL/Distributor)";
                column.FieldName = "GPTPLNAME";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='GPTPLNAME'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 220;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 220;
                }
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Check In Time";
                column.FieldName = "CHECKIN_TIME";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CHECKIN_TIME'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 120;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 120;
                }
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Check In Address(GPS)";
                column.FieldName = "CHECKIN_ADDRESS";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CHECKIN_ADDRESS'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 220;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 220;
                }
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Shop Address";
                column.FieldName = "SHOPADDRESS";
                column.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOPADDRESS'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 300;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 300;
                }
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Check Out Time";
                column.FieldName = "CHECKOUT_TIME";
                column.PropertiesEdit.DisplayFormatString = "hh:mm:ss";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CHECKOUT_TIME'");
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
                column.Caption = "Check Out Address(GPS)";
                column.FieldName = "CHECKOUT_ADDRESS";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CHECKOUT_ADDRESS'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 220;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 220;
                }

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Total Time Spent(HH:MM:SS)";
                column.FieldName = "TOTTIMESPENT";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='TOTTIMESPENT'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 140;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 140;
                }
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Visit Purpose";
                column.FieldName = "VISITPURPOSE";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='VISITPURPOSE'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                        column.ExportWidth = 140;
                    }
                }
                else
                {
                    column.Visible = true;
                    column.ExportWidth = 140;
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
using BusinessLogicLayer.SalesmanTrack;
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
using System.Web.UI;
using UtilityLayer;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class LocationReportController : Controller
    {
        //
        // GET: /MYSHOP/LocationReport/
        LocationReportBL objshop = new LocationReportBL();
        public ActionResult LocationReport()
        {
            try
            {
                string userid = Session["userid"].ToString();

                LocationReportModel omodel = new LocationReportModel();
                omodel.FromDate = DateTime.Now.ToString("dd-MM-yyyy");
                omodel.ToDate = DateTime.Now.ToString("dd-MM-yyyy");
                // omodel.UserID = userid;
                return View(omodel);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetLocationReportlistPartial(LocationReportModel model)
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
                double days = (Convert.ToDateTime(model.ToDate) - Convert.ToDateTime(model.FromDate)).TotalDays;

                //if (days <= 7)
                if (days <= 30)
                {
                    if (model.Ispageload != "0")
                    {
                        string FromDate = model.FromDate;
                        string ToDate = model.ToDate;
                        string userID = Convert.ToString(Session["userid"]);
                        dt = objshop.GenerateLocationReportData(Employee, FromDate, ToDate, Convert.ToInt64(userID), State_id, Designation_id);
                    }
                }
                else
                {
                   // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
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
                return PartialView("_PartialLocationReportGrid", GetLocalReportActive(Is_PageLoad));
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public IEnumerable GetLocalReportActive(string Is_PageLoad)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string userID = Convert.ToString(Session["userid"]);

            if (Is_PageLoad != "Ispageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSLOCATION_REPORTs
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
                    var q = from d in dc.FTSLOCATION_REPORTs
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

        public ActionResult ExportLocallist(int type)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetLocalreportGridViewSettings(), GetLocalReportActive(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetLocalreportGridViewSettings(), GetLocalReportActive(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetLocalreportGridViewSettings(), GetLocalReportActive(""));
                //break;
                case 4:
                    return GridViewExtension.ExportToRtf(GetLocalreportGridViewSettings(), GetLocalReportActive(""));
                //break;
                case 5:
                    return GridViewExtension.ExportToCsv(GetLocalreportGridViewSettings(), GetLocalReportActive(""));
                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetLocalreportGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "Salesman With Supervisor Tracking";
            // settings.CallbackRouteValues = new { Controller = "Employee", Action = "ExportEmployee" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Salesman With Supervisor Tracking";

            settings.Columns.Add(column =>
            {
                column.Caption = "Employee Name";
                column.FieldName = "Employee_Name";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Designation";
                column.FieldName = "Designation";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "State";
                column.FieldName = "State_name";

            });           

            settings.Columns.Add(column =>
            {
                column.Caption = "From Date & Time";
                column.FieldName = "VISITFROMDATE";
                column.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy hh:mm:ss";
                // (column.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "To Date & Time";
                column.FieldName = "VISITTODATE";
                column.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy hh:mm:ss";
                // (column.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Duration (HH:MM)";
                column.FieldName = "TOTAL_HRS_WORKED";
                column.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy hh:mm:ss";
                // (column.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Location";
                column.FieldName = "LOCATION";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Shop Name";
                column.FieldName = "SHOP_NAME";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Code";
                column.FieldName = "ENTITYCODE";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Shop Type";
                column.FieldName = "SHOP_TYPE";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Shop Address";
                column.FieldName = "ADDRESS";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Shop owner Name";
                column.FieldName = "SHOP_OWNER_NAME";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Shop Owner Contact";
                column.FieldName = "SHOP_OWNER_CONTACT";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Count";
                column.FieldName = "NOOFDATEVISIT";

            });

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

        public ActionResult GetDesigList()
        {
            try
            {
                DataTable dtdesig = objshop.Getdesiglist();
                List<GetDesignation> modeldesig = new List<GetDesignation>();
                modeldesig = APIHelperMethods.ToModelList<GetDesignation>(dtdesig);
                return PartialView("~/Areas/MYSHOP/Views/LocationReport/_PartialOfficerDesignation.cshtml", modeldesig);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }
	}
}
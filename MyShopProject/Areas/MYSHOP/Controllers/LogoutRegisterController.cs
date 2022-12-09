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

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class LogoutRegisterController : Controller
    {
        //
        // GET: /MYSHOP/LogoutRegister/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetLogoutlistPartial(BusinessLogicLayer.SalesmanTrack.LogoutRegister.LogoutRegisterModel model)
        {
            LogoutRegister objshop = new LogoutRegister();
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

                double days = (Convert.ToDateTime(model.ToDate) - Convert.ToDateTime(model.FromDate)).TotalDays;

                if (days <= 7)
                {
                    if (model.Ispageload != "0")
                    {
                       dt= objshop.GetLogoutRegister(Employee, FromDate, ToDate, Convert.ToString(userID), State_id, Designation_id);
                    }
                }
                TempData["dt"] = dt;
                TempData.Keep();
                return PartialView("_PartialLogoutRegister", dt);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
        //public IEnumerable GetEmployeeActive(string Is_PageLoad)
        //{
           
        //    string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
        //    string userID = Convert.ToString(Session["userid"]);

        //    if (Is_PageLoad != "Ispageload")
        //    {
        //        ReportsDataContext dc = new ReportsDataContext(connectionString);
        //        var q = from d in dc.FTSACTIVITY_REPORTs
        //                where d.LOGIN_ID == Convert.ToInt32(userID)
        //                orderby d.SEQ ascending
        //                select d;
        //        return q;
        //    }
        //    else
        //    {
        //        if (Is_PageLoad != "Ispageload")
        //        {
        //            ReportsDataContext dc = new ReportsDataContext(connectionString);
        //            var q = from d in dc.FTSACTIVITY_REPORTs
        //                    where d.LOGIN_ID == Convert.ToInt32(userID)
        //                    orderby d.SEQ ascending
        //                    select d;
        //            return q;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}

        public ActionResult ExportActivitylist(int type)
        {
            
            
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(), TempData["dt"]);
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(), TempData["dt"]);
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(), TempData["dt"]);
                //break;
                case 4:
                    return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(), TempData["dt"]);
                //break;
                case 5:
                    return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(), TempData["dt"]);
                default:
                    break;
            }

            TempData.Keep();

            return null;
        }

        private GridViewSettings GetEmployeeBatchGridViewSettings()
        {
            

            var settings = new GridViewSettings();
            settings.Name = "Employee Activity";
            // settings.CallbackRouteValues = new { Controller = "Employee", Action = "ExportEmployee" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Employee Activity";

            settings.Columns.Add(x =>
            {
                x.FieldName = "Date";
                x.Caption = "Date";
                x.VisibleIndex = 1;
                //x.Width = 150;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                var Properties = x.PropertiesEdit as DevExpress.Web.DateEditProperties;
                Properties.DisplayFormatString = "dd-MM-yyy";

            });



            settings.Columns.Add(x =>
            {
                x.FieldName = "user_name";
                x.Caption = "User Name";
                x.VisibleIndex = 2;
                x.ColumnType = MVCxGridViewColumnType.TextBox;


            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Name";
                x.Caption = "Emp. Name";
                x.VisibleIndex = 3;

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ShortCode";
                x.Caption = "Emp. Code";
                x.VisibleIndex = 4;

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Designation";
                x.Caption = "Designation";
                x.VisibleIndex = 5;


            });


            settings.Columns.Add(x =>
            {
                x.FieldName = "Supervisor";
                x.Caption = "Supervisor";
                x.VisibleIndex = 6;
                //x.Width = 200;
                x.ColumnType = MVCxGridViewColumnType.TextBox;


            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Supervisor_designation";
                x.Caption = "Supervisor Desg.";
                x.VisibleIndex = 7;
                x.ColumnType = MVCxGridViewColumnType.TextBox;


            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "State_name";
                x.Caption = "State";
                x.VisibleIndex = 8;
                //x.Width = 150;
                x.ColumnType = MVCxGridViewColumnType.TextBox;

            });


            settings.Columns.Add(x =>
            {
                x.FieldName = "Logout_Time";
                x.Caption = "Logout Time";
                x.VisibleIndex = 9;

            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "Reason";
                x.Caption = "Reason";
                x.VisibleIndex = 10;


            });

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }
	}
}
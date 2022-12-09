using BusinessLogicLayer;
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
    public class EmpAchivementReportController : Controller
    {
        PlanAchiveReportBL obj = new PlanAchiveReportBL();

        public ActionResult EmpAchivementIndex()
        {
            try
            {
                PlanAchivReportVM omodel = new PlanAchivReportVM();
                string userid = Session["userid"].ToString();
                omodel.selectedusrid = userid;
                omodel.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                omodel.Todate = DateTime.Now.ToString("dd-MM-yyyy");

                return View(omodel);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetEMPAchivPlanListList(PlanAchivReportVM model)
        {
            try
            {
                string Is_PageLoad = string.Empty;
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
               
                DataTable dt = new DataTable();

                if (model.Fromdate == null)
                {
                    model.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                }

                if (model.Todate == null)
                {
                    model.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                }

                if (model.Is_PageLoad != "1") Is_PageLoad = "Ispageload";

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

                double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;
                if (days <= 30)
                {
                    dt = obj.GetReportPlanAchivement(datfrmat, dattoat, Userid, state);
                }

                return Json(state, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetPlanAcvivementReportList(string Is_PageLoad)
        {

            return PartialView("~/Areas/MYSHOP/Views/EmpAchivementReport/_PartialPlanAchiveReportGrid.cshtml", GetPlanDate(Is_PageLoad));
        }

        public IEnumerable GetPlanDate(string Is_PageLoad)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);
            if (Is_PageLoad != "Ispageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSPARTYPLAN_REPORTs
                        where d.LOGIN_ID == Convert.ToInt32(Userid)
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSPARTYPLAN_REPORTs
                        where d.LOGIN_ID == Convert.ToInt32(Userid) && d.Employee_ID == "0"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
        }

        public ActionResult ExporPlanRegisterList(int type, String Is_PageLoad)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetEmployeePlanGridViewSettings(), GetPlanDate(Is_PageLoad));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetEmployeePlanGridViewSettings(), GetPlanDate(Is_PageLoad));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetEmployeePlanGridViewSettings(), GetPlanDate(Is_PageLoad));
                case 4:
                    return GridViewExtension.ExportToRtf(GetEmployeePlanGridViewSettings(), GetPlanDate(Is_PageLoad));
                case 5:
                    return GridViewExtension.ExportToCsv(GetEmployeePlanGridViewSettings(), GetPlanDate(Is_PageLoad));
                //break;

                default:
                    break;
            }

            return null;
        }
        private GridViewSettings GetEmployeePlanGridViewSettings()
        {

            var settings = new GridViewSettings();
            settings.Name = "EmployeeAchivementReport";
         //   settings.CallbackRouteValues = new { Controller = "InvoiceDeliveryRegister", Action = "GetRegisterreporttatusList" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Employee Achivement Report";


            settings.Columns.Add(x =>
            {
                x.FieldName = "STATE_NAME";
                x.Caption = "State";
                x.VisibleIndex = 1;
                x.Width = 200;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "TYPE";
                x.Caption = "Type";
                x.VisibleIndex = 2;
                x.Width = 50;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "PARTY_NAME";
                x.Caption = "Party Name";
                x.VisibleIndex = 3;
                x.Width = 200;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CONTACT_NO";
                x.Caption = "Contact No";
                x.VisibleIndex = 4;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "LOCATION";
                x.Caption = "Location";
                x.VisibleIndex = 5;
                x.Width = 200;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "EMPLOYEE_NAME";
                x.Caption = "Employee Name";
                x.VisibleIndex = 6;
                x.Width = 200;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "PLAN_DATE";
                x.Caption = "Plan Date";
                x.VisibleIndex = 7;
                x.Width = 100;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "PLAN_DATE";
                x.Caption = "Plan Time";
                x.VisibleIndex = 8;
                x.Width = 100;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "hh:mm:ss";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "hh:mm:ss";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "PLAN_AMT";
                x.Caption = "Plan Amt.";
                x.VisibleIndex = 9;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.Width = 100;
                x.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "PLAN_REMARKS";
                x.Caption = "Plan Remarks";
                x.VisibleIndex = 10;
                x.Width = 200;
            });


            settings.Columns.Add(x =>
            {
                x.FieldName = "ACHIV_DATE";
                x.Caption = "Achv. Date";
                x.VisibleIndex = 11;
                x.Width = 120;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ACHIV_DATE";
                x.Caption = "Achv. Time";
                x.VisibleIndex = 12;
                x.Width = 120;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "hh:mm:ss";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "hh:mm:ss";

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ACHIV_AMT";
                x.Caption = "Achv. Amt.";
                x.VisibleIndex = 13;
                x.Width = 120;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ACHIV_REMARKS";
                x.Caption = "Achv. Remarks";
                x.VisibleIndex = 14;
                x.Width = 200;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "PERCENTAGES";
                x.Caption = "Percentage";
                x.VisibleIndex = 15;
                x.Width = 100;
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
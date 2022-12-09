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
    public class ChemistActivityReportController : Controller
    {
        ChemistActivityBL obj = new ChemistActivityBL();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChemistActivity()
        {
            try
            {
                ChemistActivityModel omodel = new ChemistActivityModel();
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

        public ActionResult ChemistActivityGrid(string Is_PageLoad)
        {
            return PartialView(GetDataActivity(Is_PageLoad));
        }

        public IEnumerable GetDataActivity(string Is_PageLoad)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);
            if (Is_PageLoad != "Ispageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSCHEMISTACTIVITY_REPORTs
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSCHEMISTACTIVITY_REPORTs
                        where d.USERID == Convert.ToInt32(Userid) && d.EMPCODE == "0"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
        }

        public ActionResult GetChemistActivityList(ChemistActivityModel model)
        {
            try
            {
                string Is_PageLoad = string.Empty;
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


                double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;
                if (days <= 30)
                {
                    dt = obj.GetReportChemistActivity(datfrmat, dattoat, Userid, state, empcode);
                }

                return Json(empcode, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }

        public ActionResult ExporRegisterList(int type)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetChemistBatchGridViewSettings(), GetDataActivity(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetChemistBatchGridViewSettings(), GetDataActivity(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetChemistBatchGridViewSettings(), GetDataActivity(""));
                case 4:
                    return GridViewExtension.ExportToRtf(GetChemistBatchGridViewSettings(), GetDataActivity(""));
                case 5:
                    return GridViewExtension.ExportToCsv(GetChemistBatchGridViewSettings(), GetDataActivity(""));
                //break;

                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetChemistBatchGridViewSettings()
        {

            var settings = new GridViewSettings();
            settings.Name = "gridChemistActivity";
            settings.CallbackRouteValues = new { Controller = "InvoiceDeliveryRegister", Action = "GetRegisterreporttatusList" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Chemist Activity Report";

            settings.Columns.Add(x =>
            {
                x.FieldName = "ACTIVITY_DATE";
                x.Caption = "Activity Date";
                x.VisibleIndex = 1;
                x.Width = 100;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "EMPLOYEE_NAME";
                x.Caption = "Employee Name";
                x.VisibleIndex = 2;
                x.Width = 150;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "STATE_NAME";
                x.Caption = "State";
                x.VisibleIndex = 3;
                x.Width = 100;
            });


            settings.Columns.Add(x =>
            {
                x.FieldName = "DESIGNATION";
                x.Caption = "Designation";
                x.VisibleIndex = 4;
                x.Width = 80;


            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "REPORT_TO";
                x.Caption = "Report To";
                x.VisibleIndex = 5;
                x.Width = 200;

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CHEMIST_NAME";
                x.Caption = "Chemist Name";
                x.VisibleIndex = 6;
                x.Width = 200;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "SHOP_TYPE";
                x.Caption = "Type";
                x.VisibleIndex = 7;
                x.Width = 50;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ISPOB";
                x.Caption = "POB";
                x.VisibleIndex = 8;
                x.Width = 80;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "VOLUME";
                x.Caption = "Volume";
                x.VisibleIndex = 9;
                x.Width = 80;
                x.PropertiesEdit.DisplayFormatString = "0.00";
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "REMARKS";
                x.Caption = "Remarks";
                x.VisibleIndex = 10;
                x.Width = 300;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "NEXT_VISIT_DATE";
                x.Caption = "Next Visit";
                x.VisibleIndex = 11;
                x.Width = 100;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "MR_REMARKS";
                x.Caption = "MR Remarks";
                x.VisibleIndex = 12;
                x.Width = 300;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "PRODUCT";
                x.Caption = "Product";
                x.VisibleIndex = 13;
                x.Width = 500;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "POB_PRODUCT";
                x.Caption = "POB Product";
                x.VisibleIndex = 14;
                x.Width = 500;
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
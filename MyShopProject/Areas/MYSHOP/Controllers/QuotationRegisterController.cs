using BusinessLogicLayer.SalesTrackerReports;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using MyShop.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class QuotationRegisterController : Controller
    {
        QuotationRegisterBL obj = new QuotationRegisterBL();
        public ActionResult Index()
        {
            return View();
        }


        public PartialViewResult Rendermainpage()
        {
            return PartialView();
        }

        public PartialViewResult Rendergrid(string ispageload)
        {
            return PartialView(GetReport(ispageload));
        }

        [HttpPost]
        public ActionResult GenerateTable(CustomerDetailsModel model)
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

                if (model.is_pageload != "1") Is_PageLoad = "Ispageload";

                ViewData["ModelData"] = model;

                string datfrmat = model.Fromdate.Split('-')[2] + '-' + model.Fromdate.Split('-')[1] + '-' + model.Fromdate.Split('-')[0];
                string dattoat = model.Todate.Split('-')[2] + '-' + model.Todate.Split('-')[1] + '-' + model.Todate.Split('-')[0];
                string Userid = Convert.ToString(Session["userid"]);

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

                string DeptId = "";
                k = 1;
                if (model.Department != null && model.Department.Count > 0)
                {
                    foreach (string item in model.Department)
                    {
                        if (k > 1)
                            DeptId = DeptId + "," + item;
                        else
                            DeptId = item;
                        k++;
                    }
                }

                string Desgid = "";
                k = 1;
                if (model.desgid != null && model.desgid.Count > 0)
                {
                    foreach (string item in model.desgid)
                    {
                        if (k > 1)
                            Desgid = Desgid + "," + item;
                        else
                            Desgid = item;
                        k++;
                    }
                }

                //double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;
                //if (days <= 30)
                //{
                    dt = obj.GetReportQuotationRegister(datfrmat, dattoat, Userid, DeptId, empcode, Desgid);
                //}
                return Json(Userid, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public IEnumerable GetReport(string ispageload)
        {
            string connectionString = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"]);
            string Userid = Convert.ToString(Session["userid"]);

            if (ispageload != "1")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTS_QuotationRegisterReports
                        where d.USERID == Convert.ToInt32(Userid)
                        //orderby d.EMPLOYEE ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTS_QuotationRegisterReports
                        where d.USERID == 0
                        select d;
                return q;
            }
        }

        public ActionResult ExportTargetvsAchivReport(int type, string IsPageload)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToXlsx(GetGridViewSettings(), GetReport(IsPageload));
                //break;
                case 2:
                    return GridViewExtension.ExportToXls(GetGridViewSettings(), GetReport(IsPageload));
                //break;
                case 3:
                    return GridViewExtension.ExportToPdf(GetGridViewSettings(), GetReport(IsPageload));
                case 4:
                    return GridViewExtension.ExportToCsv(GetGridViewSettings(), GetReport(IsPageload));
                case 5:
                    return GridViewExtension.ExportToRtf(GetGridViewSettings(), GetReport(IsPageload));
                //break;

                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "QuotationRegisterReport";
            settings.CallbackRouteValues = new { Controller = "QuotationRegister", Action = "GenerateTable" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "QuotationRegisterReport";

            settings.Columns.Add(column =>
            {
                column.Caption = "Qutation No";
                column.FieldName = "QUOTATION_NO";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 100;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Qutation Date";
                column.FieldName = "QUOTATION_DATE";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ColumnType = MVCxGridViewColumnType.DateEdit;
                column.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (column.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
                column.Width = 120;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Hypothecation";
                column.FieldName = "HYPOTHECATION";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 100;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "ACCOUNT_NO";
                column.Caption = "A/C No";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 100;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Model";
                column.FieldName = "MODEL";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 200;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "BS";
                column.Caption = "BS";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 100;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Desc1";
                column.FieldName = "DESC1";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 100;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "DESC2";
                column.Caption = "Desc2";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 100;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "DESC3";
                column.Caption = "Desc3";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 100;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Desc4";
                column.FieldName = "DESC4";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 100;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "DESC5";
                column.Caption = "Desc5";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 100;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "DESC6";
                column.Caption = "Desc6";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 100;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Desc7";
                column.FieldName = "DESC7";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 100;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "AMOUNT";
                column.Caption = "Amount";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.PropertiesEdit.DisplayFormatString = "0.00";
                column.Width = 100;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "DISCOUNT";
                column.Caption = "Discount";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.PropertiesEdit.DisplayFormatString = "0.00";
                column.Width = 100;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "CGST";
                column.Caption = "CGST";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.PropertiesEdit.DisplayFormatString = "0.00";
                column.Width = 100;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "SGST";
                column.Caption = "SGST";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.PropertiesEdit.DisplayFormatString = "0.00";
                column.Width = 100;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "TCS";
                column.Caption = "TCS";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.PropertiesEdit.DisplayFormatString = "0.00";
                column.Width = 100;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "INSURANCE";
                column.Caption = "Insurance";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.PropertiesEdit.DisplayFormatString = "0.00";
                column.Width = 100;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "NET_AMOUNT";
                column.Caption = "Net Amount";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.PropertiesEdit.DisplayFormatString = "0.00";
                column.Width = 100;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "USERS";
                column.Caption = "Created By";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 200;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "CREATE_DATE";
                column.Caption = "Created On";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ColumnType = MVCxGridViewColumnType.DateEdit;
                column.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy hh:mm:ss";
                (column.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy hh:mm:ss";
                column.Width = 150;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "UPDATE_DATE";
                column.Caption = "Updated On";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ColumnType = MVCxGridViewColumnType.DateEdit;
                column.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy hh:mm:ss";
                (column.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy hh:mm:ss";
                column.Width = 150;
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
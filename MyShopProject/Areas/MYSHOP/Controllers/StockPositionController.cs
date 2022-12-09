using BusinessLogicLayer.SalesTrackerReports;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using Models;
using MyShop.Models;
using SalesmanTrack;
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
    public class StockPositionController : Controller
    {
        //
        // GET: /MYSHOP/StockPosition/

        UserList lstuser = new UserList();
        SalesSummary_Report objgps = new SalesSummary_Report();
        public ActionResult StockPosition()
        {
            try
            {

                StockPositionModel omodel = new StockPositionModel();
                string userid = Session["userid"].ToString();
                omodel.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                omodel.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                //DataTable dt = objgps.GetStateMandatory(Session["userid"].ToString());
                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    ViewBag.StateMandatory = dt.Rows[0]["IsStateMandatoryinReport"].ToString();
                //}

                return View(omodel);

            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetStockPositionList(StockPositionModel model)
        {
            try
            {

                DataTable dt = new DataTable();
                string is_pageload = string.Empty;

                if (model.is_pageload == "0")
                {
                    is_pageload = "Ispageload";
                }

                if (model.Fromdate == null)
                {
                    model.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                }

                if (model.Todate == null)
                {
                    model.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                }

                ViewData["ModelData"] = model;

                string datfrmat = model.Fromdate.Split('-')[2] + '-' + model.Fromdate.Split('-')[1] + '-' + model.Fromdate.Split('-')[0];
                string dattoat = model.Todate.Split('-')[2] + '-' + model.Todate.Split('-')[1] + '-' + model.Todate.Split('-')[0];
                string Userid = Convert.ToString(Session["userid"]);


                string product = "";

                int k = 1;

                if (model.prodcode != null && model.prodcode.Count > 0)
                {
                    foreach (string item in model.prodcode)
                    {
                        if (k > 1)
                            product = product + "," + item;
                        else
                            product = item;
                        k++;
                    }

                }
                //double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;

                //if (days <= 35)
                //{
                string type = "Summary";
                dt = objgps.GetStockPositonReport(datfrmat, dattoat, Userid, product, type);
                //}

                return PartialView("PartialGetStockPositonSummary", GetStockPositon(is_pageload));


            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }

        public IEnumerable GetStockPositon(string is_pageload)
        {
            DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "Stock Positon");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            }

            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);

            if (is_pageload != "Ispageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSSTOCKPOSITION_REPORTs
                        where d.USERID == Convert.ToInt32(Userid) && Convert.ToString(d.REPORTTYPE) == "Summary"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSSTOCKPOSITION_REPORTs
                        where d.USERID == Convert.ToInt32(Userid) && d.PRODID == 0 && Convert.ToString(d.REPORTTYPE) == "Summary"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }



        }

        public ActionResult GetStockPositionDetailsList(StockPositionModel model)
        {

            try
            {

                DataTable dt = new DataTable();
                string is_pageload = string.Empty;

                if (model.is_pageload == "0")
                {
                    is_pageload = "Ispageload";
                }

                if (model.Fromdate == null)
                {
                    model.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                }

                if (model.Todate == null)
                {
                    model.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                }

                ViewData["ModelData"] = model;

                string datfrmat = model.Fromdate.Split('-')[2] + '-' + model.Fromdate.Split('-')[1] + '-' + model.Fromdate.Split('-')[0];
                string dattoat = model.Todate.Split('-')[2] + '-' + model.Todate.Split('-')[1] + '-' + model.Todate.Split('-')[0];
                string Userid = Convert.ToString(Session["userid"]);


                string product = "";

                int k = 1;

                if (model.prodcode != null && model.prodcode.Count > 0)
                {
                    foreach (string item in model.prodcode)
                    {
                        if (k > 1)
                            product = product + "," + item;
                        else
                            product = item;
                        k++;
                    }

                }
                //double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;

                //if (days <= 35)
                //{
                string type = "Details";
                dt = objgps.GetStockPositonReport(datfrmat, dattoat, Userid, product, type);
                //}

                return PartialView("PartialGetStockPositonDetails", GetStockPositonDetails(is_pageload));


            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }

        public IEnumerable GetStockPositonDetails(string is_pageload)
        {
            //DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "Stock Positon");
            //if (dtColmn != null && dtColmn.Rows.Count > 0)
            //{
            //    ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            //}

            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);

            if (is_pageload != "Ispageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSSTOCKPOSITION_REPORTs
                        where d.USERID == Convert.ToInt32(Userid) && Convert.ToString(d.REPORTTYPE) == "Details"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSSTOCKPOSITION_REPORTs
                        where d.USERID == Convert.ToInt32(Userid) && d.PRODID == 0 && Convert.ToString(d.REPORTTYPE) == "Details"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }



        }

        public ActionResult ExportStockPositionList(int type)
        {


            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetStockPositionBatchGridViewSettings(), GetStockPositon(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetStockPositionBatchGridViewSettings(), GetStockPositon(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetStockPositionBatchGridViewSettings(), GetStockPositon(""));
                case 4:
                    return GridViewExtension.ExportToRtf(GetStockPositionBatchGridViewSettings(), GetStockPositon(""));
                case 5:
                    return GridViewExtension.ExportToCsv(GetStockPositionBatchGridViewSettings(), GetStockPositon(""));
                //break;

                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetStockPositionBatchGridViewSettings()
        {
            DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "Stock Positon");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            }
            var settings = new GridViewSettings();
            settings.Name = "Stock Positon";
            settings.CallbackRouteValues = new { Controller = "StockPosition", Action = "GetStockPositionList" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Stock Positon";


            settings.Columns.Add(x =>
            {
                x.FieldName = "PRODNAME";
                x.Caption = "Product Name";
                x.VisibleIndex = 1;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PRODNAME'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                    }
                }
                else
                {
                    x.Visible = true;
                }

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "PRODCLASS";
                x.Caption = "Product Class";
                x.VisibleIndex = 2;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PRODCLASS'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                    }
                }
                else
                {
                    x.Visible = true;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "OPSTK";
                x.Caption = "Opening Stock";
                x.VisibleIndex = 3;
                x.PropertiesEdit.DisplayFormatString = "0.00";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='OPSTK'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                    }
                }
                else
                {
                    x.Visible = true;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "INVQTY";
                x.Caption = "Invoiced";
                x.VisibleIndex = 4;
                x.PropertiesEdit.DisplayFormatString = "0.00";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='INVQTY'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                    }
                }
                else
                {
                    x.Visible = true;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "BALQTY";
                x.Caption = "Balance Stock";
                x.VisibleIndex = 5;
                x.Width = 100;
                x.PropertiesEdit.DisplayFormatString = "0.00";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='BALQTY'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                    }
                }
                else
                {
                    x.Visible = true;
                }
            });

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

        public ActionResult ExportStockPositionDetailsList(int type)
        {


            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetStockPositionDetailsBatchGridViewSettings(), GetStockPositonDetails(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetStockPositionDetailsBatchGridViewSettings(), GetStockPositonDetails(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetStockPositionDetailsBatchGridViewSettings(), GetStockPositonDetails(""));
                case 4:
                    return GridViewExtension.ExportToRtf(GetStockPositionDetailsBatchGridViewSettings(), GetStockPositonDetails(""));
                case 5:
                    return GridViewExtension.ExportToCsv(GetStockPositionDetailsBatchGridViewSettings(), GetStockPositonDetails(""));
                //break;

                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetStockPositionDetailsBatchGridViewSettings()
        {
            DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "Stock Positon");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            }
            var settings = new GridViewSettings();
            settings.Name = "Stock Positon Details";
            settings.CallbackRouteValues = new { Controller = "StockPosition", Action = "GetStockPositionDetailsList" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Stock Positon Details";


            settings.Columns.Add(x =>
            {
                x.FieldName = "ORDDATE";
                x.Caption = "Order Date";
                x.VisibleIndex = 1;
                x.Visible = true;
                x.Width = 120;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ORDNO";
                x.Caption = "Order Number";
                x.VisibleIndex = 2;
                x.Visible = true;
                x.Width = 120;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "INVDATE";
                x.Caption = "Invoice Dt.";
                x.VisibleIndex = 3;
                x.Visible = true;
                x.Width = 120;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "INVNO";
                x.Caption = "Inv No.";
                x.VisibleIndex = 4;
                x.Visible = true;
                x.Width = 120;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "PRODNAME";
                x.Caption = "Product";
                x.VisibleIndex = 5;
                x.Width = 150;
                x.Visible = true;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "PRODCLASS";
                x.Caption = "PRD. Class";
                x.VisibleIndex = 6;
                x.Width = 120;
                x.Visible = true;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "INVQTY";
                x.Caption = "Quantity";
                x.VisibleIndex = 7;
                x.Width = 120;
                x.Visible = true;
                x.PropertiesEdit.DisplayFormatString = "0.00";
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
                int k = objgps.InsertPageRetention(Col, Session["userid"].ToString(), "Stock Positon");

                return Json(k, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
	}
}
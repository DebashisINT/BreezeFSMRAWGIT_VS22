using BusinessLogicLayer.SalesmanTrack;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilityLayer;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class PrimarySalesOrderController : Controller
    {
        PrimarySalesOrderBL obj = new PrimarySalesOrderBL();
        public ActionResult Index()
        {
            TempData["PrimarySalesOrderdt"] = null;
            TempData["PrimarySalesOrder2ndStagedt"] = null;
            TempData["PrimarySalesOrder3rdStagedt"] = null;
            TempData["PrimarySalesOrder4thStagedt"] = null;
            return View();
        }

        public PartialViewResult Rendermainpage()
        {
            return PartialView();
        }

        public PartialViewResult Rendergrid(string ispageload)
        {
            List<PrimarySalesOrder1ststage> omel = new List<PrimarySalesOrder1ststage>();
           DataTable dt= (DataTable)TempData["PrimarySalesOrderdt"];
            TempData.Keep();
            omel = APIHelperMethods.ToModelList<PrimarySalesOrder1ststage>(dt);
            return PartialView(omel);
        }

        [HttpPost]
        public ActionResult GenerateTable(PrimarySalesOrderModel model)
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

                double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;
                //if (days <= 30)
                //{
                dt = obj.GetReportPrimarySalesOrder("1ST_STAGE", Userid, "0", "0", datfrmat, dattoat, empcode);
                    if (dt.Rows.Count > 0)
                    {
                       
                        TempData["PrimarySalesOrderdt"] = dt;
                        TempData.Keep();
                    }
                    else
                    {
                        TempData["PrimarySalesOrderdt"] = null;
                        TempData.Keep();
                    }
               // }
                return Json(Userid, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        [HttpPost]
        public ActionResult GetSecondStageData(PrimarySalesOrderModel model)
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

                double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;
                //if (days <= 30)
                //{
                dt = obj.GetReportPrimarySalesOrder("2ND_STAGE", Userid, "0", "0", datfrmat, dattoat, empcode);
                if (dt.Rows.Count > 0)
                {

                    TempData["PrimarySalesOrder2ndStagedt"] = dt;
                    TempData.Keep();
                }
                else
                {
                    TempData["PrimarySalesOrder2ndStagedt"] = null;
                    TempData.Keep();
                }
                // }
                return Json(Userid, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public PartialViewResult RenderSecondStagegrid()
        {
            List<PrimarySalesOrder2ndstage> omel = new List<PrimarySalesOrder2ndstage>();
            DataTable dt = (DataTable)TempData["PrimarySalesOrder2ndStagedt"];
            TempData.Keep();
            omel = APIHelperMethods.ToModelList<PrimarySalesOrder2ndstage>(dt);
            return PartialView(omel);
        }

        [HttpPost]
        public ActionResult GetThirdStageData(PrimarySalesOrderModel model)
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

                double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;
                //if (days <= 30)
                //{
                dt = obj.GetReportPrimarySalesOrder("3RD_STAGE", Userid, "0", model.Shop_code, datfrmat, dattoat, empcode);
                if (dt.Rows.Count > 0)
                {

                    TempData["PrimarySalesOrder3rdStagedt"] = dt;
                    TempData.Keep();
                }
                else
                {
                    TempData["PrimarySalesOrder3rdStagedt"] = null;
                    TempData.Keep();
                }
                // }
                return Json(Userid, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public PartialViewResult RenderThirdStagegrid()
        {
            List<PrimarySalesOrder3rdstage> omel = new List<PrimarySalesOrder3rdstage>();
            DataTable dt = (DataTable)TempData["PrimarySalesOrder3rdStagedt"];
            TempData.Keep();
            omel = APIHelperMethods.ToModelList<PrimarySalesOrder3rdstage>(dt);
            return PartialView(omel);
        }

        [HttpPost]
        public ActionResult GetFourthStageData(PrimarySalesOrderModel model)
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

                double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;
                //if (days <= 30)
                //{
                dt = obj.GetReportPrimarySalesOrder("4TH_STAGE", Userid, model.order_code, model.Shop_code, datfrmat, dattoat, empcode);
                if (dt.Rows.Count > 0)
                {

                    TempData["PrimarySalesOrder4thStagedt"] = dt;
                    TempData.Keep();
                }
                else
                {
                    TempData["PrimarySalesOrder4thStagedt"] = null;
                    TempData.Keep();
                }
                // }
                return Json(Userid, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public PartialViewResult RenderFourthStagegrid()
        {
            List<PrimarySalesOrder4thstage> omel = new List<PrimarySalesOrder4thstage>();
            DataTable dt = (DataTable)TempData["PrimarySalesOrder4thStagedt"];
            TempData.Keep();
            omel = APIHelperMethods.ToModelList<PrimarySalesOrder4thstage>(dt);
            return PartialView(omel);
        }



        public ActionResult ExportPrimarySalesOrderList(int type)
        {
            ViewData["PrimarySalesOrderdt"] = TempData["PrimarySalesOrderdt"];

            TempData.Keep();

            if (ViewData["PrimarySalesOrderdt"] != null)
            {
                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetPrimarySalesOrderGridView(ViewData["PrimarySalesOrderdt"]), ViewData["PrimarySalesOrderdt"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetPrimarySalesOrderGridView(ViewData["PrimarySalesOrderdt"]), ViewData["PrimarySalesOrderdt"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXlsx(GetPrimarySalesOrderGridView(ViewData["PrimarySalesOrderdt"]), ViewData["PrimarySalesOrderdt"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetPrimarySalesOrderGridView(ViewData["PrimarySalesOrderdt"]), ViewData["PrimarySalesOrderdt"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetPrimarySalesOrderGridView(ViewData["ReAssignShopUserManualLog"]), ViewData["ReAssignShopUserManualLog"]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetPrimarySalesOrderGridView(object datatable)
        {
            var settings = new GridViewSettings();
            settings.Name = "PrimarySalesOrder";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Primary Sales Order";

            settings.Columns.Add(column =>
            {
                column.Caption = "From Date";
                column.FieldName = "FROM_DATE";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = System.Web.UI.WebControls.Unit.Percentage(30);
                column.VisibleIndex = 1;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "To Date";
                column.FieldName = "TO_DATE";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = System.Web.UI.WebControls.Unit.Percentage(30);
                column.VisibleIndex = 2;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ORDERAMOUNT";
                x.Caption = "Order Value";
                x.VisibleIndex = 3;
                x.ColumnType = MVCxGridViewColumnType.TextBox;
                x.Width = System.Web.UI.WebControls.Unit.Percentage(40);
                x.PropertiesEdit.DisplayFormatString = "0.00";
            });


            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

        public ActionResult ExportPrimarySalesOrder2ndStageList(int type)
        {
            ViewData["PrimarySalesOrder2ndStagedt"] = TempData["PrimarySalesOrder2ndStagedt"];

            TempData.Keep();

            if (ViewData["PrimarySalesOrder2ndStagedt"] != null)
            {
                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetPrimarySalesOrder2ndStageGridView(ViewData["PrimarySalesOrder2ndStagedt"]), ViewData["PrimarySalesOrder2ndStagedt"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetPrimarySalesOrder2ndStageGridView(ViewData["PrimarySalesOrder2ndStagedt"]), ViewData["PrimarySalesOrder2ndStagedt"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXlsx(GetPrimarySalesOrder2ndStageGridView(ViewData["PrimarySalesOrder2ndStagedt"]), ViewData["PrimarySalesOrder2ndStagedt"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetPrimarySalesOrder2ndStageGridView(ViewData["PrimarySalesOrder2ndStagedt"]), ViewData["PrimarySalesOrder2ndStagedt"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetPrimarySalesOrder2ndStageGridView(ViewData["PrimarySalesOrder2ndStagedt"]), ViewData["PrimarySalesOrder2ndStagedt"]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetPrimarySalesOrder2ndStageGridView(object datatable)
        {
            var settings = new GridViewSettings();
            settings.Name = "PrimarySalesOrder2ndStage";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Primary Sales Order 2nd Stage";

            settings.Columns.Add(column =>
            {
                column.Caption = "From Date";
                column.FieldName = "FROM_DATE";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = System.Web.UI.WebControls.Unit.Percentage(20);
                column.VisibleIndex = 1;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "To Date";
                column.FieldName = "TO_DATE";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = System.Web.UI.WebControls.Unit.Percentage(20);
                column.VisibleIndex = 2;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_Name";
                x.Caption = "Customer Name";
                x.VisibleIndex = 3;
                x.ColumnType = MVCxGridViewColumnType.TextBox;
                x.Width = System.Web.UI.WebControls.Unit.Percentage(30);
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Order Value";
                column.FieldName = "ORDERAMOUNT";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = System.Web.UI.WebControls.Unit.Percentage(30);
                column.VisibleIndex = 4;
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });


            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

        public ActionResult ExportPrimarySalesOrder3rdStageList(int type)
        {
            ViewData["PrimarySalesOrder3rdStagedt"] = TempData["PrimarySalesOrder3rdStagedt"];

            TempData.Keep();

            if (ViewData["PrimarySalesOrder3rdStagedt"] != null)
            {
                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetPrimarySalesOrder3rdStageGridView(ViewData["PrimarySalesOrder3rdStagedt"]), ViewData["PrimarySalesOrder3rdStagedt"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetPrimarySalesOrder3rdStageGridView(ViewData["PrimarySalesOrder3rdStagedt"]), ViewData["PrimarySalesOrder3rdStagedt"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXlsx(GetPrimarySalesOrder3rdStageGridView(ViewData["PrimarySalesOrder3rdStagedt"]), ViewData["PrimarySalesOrder3rdStagedt"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetPrimarySalesOrder3rdStageGridView(ViewData["PrimarySalesOrder3rdStagedt"]), ViewData["PrimarySalesOrder3rdStagedt"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetPrimarySalesOrder3rdStageGridView(ViewData["PrimarySalesOrder3rdStagedt"]), ViewData["PrimarySalesOrder3rdStagedt"]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetPrimarySalesOrder3rdStageGridView(object datatable)
        {
            var settings = new GridViewSettings();
            settings.Name = "PrimarySalesOrder3rdStage";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Primary Sales Order 3rd Stage";

            settings.Columns.Add(column =>
            {
                column.Caption = "Date";
                column.FieldName = "DATE";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = System.Web.UI.WebControls.Unit.Percentage(20);
                column.VisibleIndex = 1;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Customer Name";
                column.FieldName = "Shop_Name";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = System.Web.UI.WebControls.Unit.Percentage(30);
                column.VisibleIndex = 2;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "OrderCode";
                x.Caption = "Order Number";
                x.VisibleIndex = 3;
                x.ColumnType = MVCxGridViewColumnType.TextBox;
                x.Width = System.Web.UI.WebControls.Unit.Percentage(30);
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Order Value";
                column.FieldName = "ORDERAMOUNT";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = System.Web.UI.WebControls.Unit.Percentage(20);
                column.VisibleIndex = 4;
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });


            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

        public ActionResult ExportPrimarySalesOrder4thStageList(int type)
        {
            ViewData["PrimarySalesOrder4thStagedt"] = TempData["PrimarySalesOrder4thStagedt"];

            TempData.Keep();

            if (ViewData["PrimarySalesOrder4thStagedt"] != null)
            {
                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetPrimarySalesOrder4thStageGridView(ViewData["PrimarySalesOrder4thStagedt"]), ViewData["PrimarySalesOrder4thStagedt"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetPrimarySalesOrder4thStageGridView(ViewData["PrimarySalesOrder4thStagedt"]), ViewData["PrimarySalesOrder4thStagedt"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXlsx(GetPrimarySalesOrder4thStageGridView(ViewData["PrimarySalesOrder4thStagedt"]), ViewData["PrimarySalesOrder4thStagedt"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetPrimarySalesOrder4thStageGridView(ViewData["PrimarySalesOrder4thStagedt"]), ViewData["PrimarySalesOrder4thStagedt"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetPrimarySalesOrder4thStageGridView(ViewData["PrimarySalesOrder4thStagedt"]), ViewData["PrimarySalesOrder4thStagedt"]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetPrimarySalesOrder4thStageGridView(object datatable)
        {
            var settings = new GridViewSettings();
            settings.Name = "PrimarySalesOrderProductDetails";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Primary Sales Order Product Details";

            settings.Columns.Add(column =>
            {
                column.Caption = "Date";
                column.FieldName = "Orderdate";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                column.VisibleIndex = 1;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Customer Name";
                column.FieldName = "Shop_Name";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = System.Web.UI.WebControls.Unit.Percentage(20);
                column.VisibleIndex = 2;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "OrderCode";
                x.Caption = "Order Number";
                x.VisibleIndex = 3;
                x.ColumnType = MVCxGridViewColumnType.TextBox;
                x.Width = System.Web.UI.WebControls.Unit.Percentage(20);
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Product Name";
                column.FieldName = "sProducts_Name";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = System.Web.UI.WebControls.Unit.Percentage(20);
                column.VisibleIndex = 4;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Qty";
                column.FieldName = "Product_Qty";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                column.VisibleIndex = 5;
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Rate";
                column.FieldName = "Product_Rate";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                column.VisibleIndex = 6;
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Product Value";
                column.FieldName = "Product_Price";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                column.VisibleIndex = 7;
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }


        [HttpPost]
        public ActionResult GetDate()
        {
            try
            {
                DateTime dat = System.DateTime.Now;//.ToString("dd-MM-yyyy");
                DataTable dt = obj.GetDate("GetDate");
                if (dt!=null && dt.Rows.Count>0)
                {
                    dat = Convert.ToDateTime(dt.Rows[0]["Orderdate"].ToString());//.ToString("dd-MM-yyyy");
                }
                return Json(dat, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
	}
}
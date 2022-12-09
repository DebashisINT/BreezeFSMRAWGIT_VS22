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
    public class SecondarySalesOrderController : Controller
    {
        SecondarySalesOrderBL obj = new SecondarySalesOrderBL();
        public ActionResult Index()
        {
            TempData["SecondarySalesOrderdt"] = null;
            TempData["SecondarySalesOrder2ndStagedt"] = null;
            TempData["SecondarySalesOrder3rdStagedt"] = null;
            TempData["SecondarySalesOrder4thStagedt"] = null;
            return View();
        }

        public PartialViewResult Rendermainpage()
        {
            return PartialView();
        }

        public PartialViewResult Rendergrid(string ispageload)
        {
            List<SecondarySalesOrder1ststage> omel = new List<SecondarySalesOrder1ststage>();
            DataTable dt = (DataTable)TempData["SecondarySalesOrderdt"];
            TempData.Keep();
            omel = APIHelperMethods.ToModelList<SecondarySalesOrder1ststage>(dt);
            return PartialView(omel);
        }

        [HttpPost]
        public ActionResult GenerateTable(SecondarySalesOrderModel model)
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
                dt = obj.GetReportSecondarySalesOrder("1ST_STAGE", Userid, "0", "0", datfrmat, dattoat, empcode);
                if (dt.Rows.Count > 0)
                {

                    TempData["SecondarySalesOrderdt"] = dt;
                    TempData.Keep();
                }
                else
                {
                    TempData["SecondarySalesOrderdt"] = null;
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
        public ActionResult GetSecondStageData(SecondarySalesOrderModel model)
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
                dt = obj.GetReportSecondarySalesOrder("2ND_STAGE", Userid, "0", "0", datfrmat, dattoat, empcode);
                if (dt.Rows.Count > 0)
                {

                    TempData["SecondarySalesOrder2ndStagedt"] = dt;
                    TempData.Keep();
                }
                else
                {
                    TempData["SecondarySalesOrder2ndStagedt"] = null;
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
            List<SecondarySalesOrder2ndstage> omel = new List<SecondarySalesOrder2ndstage>();
            DataTable dt = (DataTable)TempData["SecondarySalesOrder2ndStagedt"];
            TempData.Keep();
            omel = APIHelperMethods.ToModelList<SecondarySalesOrder2ndstage>(dt);
            return PartialView(omel);
        }

        [HttpPost]
        public ActionResult GetThirdStageData(SecondarySalesOrderModel model)
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
                dt = obj.GetReportSecondarySalesOrder("3RD_STAGE", Userid, "0", model.Shop_code, datfrmat, dattoat, empcode);
                if (dt.Rows.Count > 0)
                {

                    TempData["SecondarySalesOrder3rdStagedt"] = dt;
                    TempData.Keep();
                }
                else
                {
                    TempData["SecondarySalesOrder3rdStagedt"] = null;
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
            List<SecondarySalesOrder3rdstage> omel = new List<SecondarySalesOrder3rdstage>();
            DataTable dt = (DataTable)TempData["SecondarySalesOrder3rdStagedt"];
            TempData.Keep();
            omel = APIHelperMethods.ToModelList<SecondarySalesOrder3rdstage>(dt);
            return PartialView(omel);
        }

        [HttpPost]
        public ActionResult GetFourthStageData(SecondarySalesOrderModel model)
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
                dt = obj.GetReportSecondarySalesOrder("4TH_STAGE", Userid, model.order_code, model.Shop_code, datfrmat, dattoat, empcode);
                if (dt.Rows.Count > 0)
                {

                    TempData["SecondarySalesOrder4thStagedt"] = dt;
                    TempData.Keep();
                }
                else
                {
                    TempData["SecondarySalesOrder4thStagedt"] = null;
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
            List<SecondarySalesOrder4thstage> omel = new List<SecondarySalesOrder4thstage>();
            DataTable dt = (DataTable)TempData["SecondarySalesOrder4thStagedt"];
            TempData.Keep();
            omel = APIHelperMethods.ToModelList<SecondarySalesOrder4thstage>(dt);
            return PartialView(omel);
        }


        public ActionResult ExportSecondarySalesOrderList(int type)
        {
            ViewData["SecondarySalesOrderdt"] = TempData["SecondarySalesOrderdt"];

            TempData.Keep();

            if (ViewData["SecondarySalesOrderdt"] != null)
            {
                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetSecondarySalesOrderGridView(ViewData["SecondarySalesOrderdt"]), ViewData["SecondarySalesOrderdt"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetSecondarySalesOrderGridView(ViewData["SecondarySalesOrderdt"]), ViewData["SecondarySalesOrderdt"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXlsx(GetSecondarySalesOrderGridView(ViewData["SecondarySalesOrderdt"]), ViewData["SecondarySalesOrderdt"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetSecondarySalesOrderGridView(ViewData["SecondarySalesOrderdt"]), ViewData["SecondarySalesOrderdt"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetSecondarySalesOrderGridView(ViewData["ReAssignShopUserManualLog"]), ViewData["ReAssignShopUserManualLog"]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetSecondarySalesOrderGridView(object datatable)
        {
            var settings = new GridViewSettings();
            settings.Name = "SecondarySalesOrder";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Secondary Sales Order";

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

        public ActionResult ExportSecondarySalesOrder2ndStageList(int type)
        {
            ViewData["SecondarySalesOrder2ndStagedt"] = TempData["SecondarySalesOrder2ndStagedt"];

            TempData.Keep();

            if (ViewData["SecondarySalesOrder2ndStagedt"] != null)
            {
                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetSecondarySalesOrder2ndStageGridView(ViewData["SecondarySalesOrder2ndStagedt"]), ViewData["SecondarySalesOrder2ndStagedt"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetSecondarySalesOrder2ndStageGridView(ViewData["SecondarySalesOrder2ndStagedt"]), ViewData["SecondarySalesOrder2ndStagedt"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXlsx(GetSecondarySalesOrder2ndStageGridView(ViewData["SecondarySalesOrder2ndStagedt"]), ViewData["SecondarySalesOrder2ndStagedt"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetSecondarySalesOrder2ndStageGridView(ViewData["SecondarySalesOrder2ndStagedt"]), ViewData["SecondarySalesOrder2ndStagedt"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetSecondarySalesOrder2ndStageGridView(ViewData["SecondarySalesOrder2ndStagedt"]), ViewData["SecondarySalesOrder2ndStagedt"]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetSecondarySalesOrder2ndStageGridView(object datatable)
        {
            var settings = new GridViewSettings();
            settings.Name = "SecondarySalesOrder2ndStage";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Secondary Sales Order 2nd Stage";

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

        public ActionResult ExportSecondarySalesOrder3rdStageList(int type)
        {
            ViewData["SecondarySalesOrder3rdStagedt"] = TempData["SecondarySalesOrder3rdStagedt"];

            TempData.Keep();

            if (ViewData["SecondarySalesOrder3rdStagedt"] != null)
            {
                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetSecondarySalesOrder3rdStageGridView(ViewData["SecondarySalesOrder3rdStagedt"]), ViewData["SecondarySalesOrder3rdStagedt"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetSecondarySalesOrder3rdStageGridView(ViewData["SecondarySalesOrder3rdStagedt"]), ViewData["SecondarySalesOrder3rdStagedt"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXlsx(GetSecondarySalesOrder3rdStageGridView(ViewData["SecondarySalesOrder3rdStagedt"]), ViewData["SecondarySalesOrder3rdStagedt"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetSecondarySalesOrder3rdStageGridView(ViewData["SecondarySalesOrder3rdStagedt"]), ViewData["SecondarySalesOrder3rdStagedt"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetSecondarySalesOrder3rdStageGridView(ViewData["SecondarySalesOrder3rdStagedt"]), ViewData["SecondarySalesOrder3rdStagedt"]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetSecondarySalesOrder3rdStageGridView(object datatable)
        {
            var settings = new GridViewSettings();
            settings.Name = "SecondarySalesOrder3rdStage";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Secondary Sales Order 3rd Stage";

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

        public ActionResult ExportSecondarySalesOrder4thStageList(int type)
        {
            ViewData["SecondarySalesOrder4thStagedt"] = TempData["SecondarySalesOrder4thStagedt"];

            TempData.Keep();

            if (ViewData["SecondarySalesOrder4thStagedt"] != null)
            {
                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetSecondarySalesOrder4thStageGridView(ViewData["SecondarySalesOrder4thStagedt"]), ViewData["SecondarySalesOrder4thStagedt"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetSecondarySalesOrder4thStageGridView(ViewData["SecondarySalesOrder4thStagedt"]), ViewData["SecondarySalesOrder4thStagedt"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXlsx(GetSecondarySalesOrder4thStageGridView(ViewData["SecondarySalesOrder4thStagedt"]), ViewData["SecondarySalesOrder4thStagedt"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetSecondarySalesOrder4thStageGridView(ViewData["SecondarySalesOrder4thStagedt"]), ViewData["SecondarySalesOrder4thStagedt"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetSecondarySalesOrder4thStageGridView(ViewData["SecondarySalesOrder4thStagedt"]), ViewData["SecondarySalesOrder4thStagedt"]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetSecondarySalesOrder4thStageGridView(object datatable)
        {
            var settings = new GridViewSettings();
            settings.Name = "SecondarySalesOrderProductDetails";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Secondary Sales Order Product Details";

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
                if (dt != null && dt.Rows.Count > 0)
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
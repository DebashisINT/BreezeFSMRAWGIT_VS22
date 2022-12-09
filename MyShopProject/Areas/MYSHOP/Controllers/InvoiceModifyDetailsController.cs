using BusinessLogicLayer.SalesmanTrack;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using Models;
using MyShop.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilityLayer;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class InvoiceModifyDetailsController : Controller
    {
        InvoiceDetailsUpdateBL objInv = new InvoiceDetailsUpdateBL();

        OrderDetailsSummaryProducts mproductwindow = new OrderDetailsSummaryProducts();
        public ActionResult InvoiceModifyIndex()
        {
            try
            {
                string userid = Session["userid"].ToString();
                InvoiceDetailsUpdateModel omodel = new InvoiceDetailsUpdateModel();
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

        public ActionResult GetInvoiceModifylistPartial(InvoiceDetailsUpdateModel model)
        {
            try
            {
                DataTable dt = new DataTable();
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

                string state = "";
                int k = 1;

                if (model.StateId != null && model.StateId.Count > 0)
                {
                    foreach (string item in model.StateId)
                    {
                        if (k > 1)
                            state = state + "," + item;
                        else
                            state = item;
                        k++;
                    }

                }

                string desig = "";
                int j = 1;

                if (model.desgid != null && model.desgid.Count > 0)
                {
                    foreach (string item in model.desgid)
                    {
                        if (j > 1)
                            desig = desig + "," + item;
                        else
                            desig = item;
                        j++;
                    }

                }

                string Is_PageLoad = string.Empty;
                if (model.Ispageload == "0")
                {
                    Is_PageLoad = "Ispageload";
                    model.FromDate = DateTime.Now.ToString("yyyy-MM-dd");
                    model.ToDate = DateTime.Now.ToString("yyyy-MM-dd");
                    model.REPORT_BY = "0";
                }

                if (model.FromDate == null)
                {
                    model.FromDate = DateTime.Now.ToString("yyyy-MM-dd");
                }
                else
                {
                    model.FromDate = model.FromDate.Split('-')[2] + '-' + model.FromDate.Split('-')[1] + '-' + model.FromDate.Split('-')[0];
                }

                if (model.ToDate == null)
                {
                    model.ToDate = DateTime.Now.ToString("yyyy-MM-dd");
                }
                else
                {
                    model.ToDate = model.ToDate.Split('-')[2] + '-' + model.ToDate.Split('-')[1] + '-' + model.ToDate.Split('-')[0];
                }
                string FromDate = model.FromDate;
                string ToDate = model.ToDate;

                string userID = Convert.ToString(Session["userid"]);
                if (model.Ispageload != "0")
                {
                    double days = (Convert.ToDateTime(ToDate) - Convert.ToDateTime(FromDate)).TotalDays;
                     if (days <= 30)
                     {
                         dt = objInv.GenerateLocationReportData(Employee, FromDate, ToDate, Convert.ToInt64(userID), state, desig, model.REPORT_BY);
                     }
                }
                return PartialView("_PartialInvoiceModifyProductDetails", GetOrderStatusReportActive(Is_PageLoad));
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public IEnumerable GetOrderStatusReportActive(string Is_PageLoad)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string userID = Convert.ToString(Session["userid"]);

            if (Is_PageLoad != "Ispageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTS_ORDER_STATUS_REPORTs
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
                    var q = from d in dc.FTS_ORDER_STATUS_REPORTs
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

        public ActionResult ExportInvoiceModifylist(int type)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetInvoiceModifyGridViewSettings(), GetOrderStatusReportActive(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetInvoiceModifyGridViewSettings(), GetOrderStatusReportActive(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetInvoiceModifyGridViewSettings(), GetOrderStatusReportActive(""));
                //break;
                case 4:
                    return GridViewExtension.ExportToRtf(GetInvoiceModifyGridViewSettings(), GetOrderStatusReportActive(""));
                //break;
                case 5:
                    return GridViewExtension.ExportToCsv(GetInvoiceModifyGridViewSettings(), GetOrderStatusReportActive(""));
                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetInvoiceModifyGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "Order Status";
            // settings.CallbackRouteValues = new { Controller = "Employee", Action = "ExportEmployee" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Order Status";

            settings.Columns.Add(column =>
            {
                column.Caption = "Employee ID";
                column.FieldName = "Employee_ID";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Employee Name";
                column.FieldName = "Employee_Name";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "State";
                column.FieldName = "State_name";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Shop Name";
                column.FieldName = "Shop_Name";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Code";
                column.FieldName = "ENTITYCODE";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Address";
                column.FieldName = "Address";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Contact";
                column.FieldName = "Contact";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Shop Type";
                column.FieldName = "Shop_Type";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "PP Name";
                column.FieldName = "PPName";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "DD Name";
                column.FieldName = "DDName";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Order Number";
                column.FieldName = "Order_Number";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Order Date";
                column.FieldName = "Order_Date";
                column.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                // (column.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Order Value";
                column.FieldName = "Order_Value";
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Inv No";
                column.FieldName = "Invoice_Number";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Inv Date";
                column.FieldName = "Invoice_Date";
                column.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                // (column.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Delivered Value";
                column.FieldName = "Delivered_Value";
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

        public ActionResult DeleteInvoice(string OrderCode, string BillingID)
        {
            InvoiceDetails omodel = new InvoiceDetails();
            string userid = Convert.ToString(Session["userid"]);
            DataTable dt = new DataTable();
            int i = objInv.UpdateDeleteInvoice(BillingID, OrderCode, DateTime.Now, null, "DELETE", userid, "", null);
            if (i > 0)
            {
                return Json("Success");
            }
            else
            {
                return Json("Failure");

            }
            return View(omodel);

        }

        public ActionResult ViewInvoice(string invoice_no, string OrderCode)
        {
            InvoiceDetails omodel = new InvoiceDetails();
            string userid = Convert.ToString(Session["userid"]);
            //  DataTable dt = new DataTable();
            DataTable dt = objInv.GetInvoiceDetails(invoice_no, OrderCode);
            if (dt != null && dt.Rows.Count > 0)
            {
                omodel.InvoiceNo = dt.Rows[0]["invoice_no"].ToString();
                omodel.InvoiceAmount = dt.Rows[0]["invoice_amount"].ToString();
                omodel.InvoiceDate =Convert.ToDateTime(dt.Rows[0]["invoice_date"].ToString());
                omodel.Renarks = dt.Rows[0]["Remarks"].ToString();
                omodel.Status = "Success";
                return Json(omodel);
            }
            else
            {
                omodel.InvoiceNo = "";
                omodel.InvoiceAmount = "";
                omodel.InvoiceDate = DateTime.Now;
                omodel.Renarks = "";
                omodel.Status = "Success";
                return Json(omodel);
            }
        }

        public ActionResult UpdateInvoice(string OrderCode, string invoice_no, string InvoiceDate, string InvoiceAmount, string Renarks, string newInvoice)
        {
            InvoiceDetails omodel = new InvoiceDetails();
            string userid = Convert.ToString(Session["userid"]);
           // string dateINV = Convert.ToDateTime(InvoiceDate).ToString("yyyy-MM-dd");
            DataTable dt = new DataTable();
            int i = objInv.UpdateDeleteInvoice(invoice_no, OrderCode, DateTime.ParseExact(InvoiceDate, "dd-MM-yyyy", CultureInfo.CurrentCulture), InvoiceAmount, "UPDATE", userid, Renarks, newInvoice);
            if (i > 0)
            {
                return Json("Success");
            }
            else
            {
                return Json("Failure");

            }
            return View(omodel);

        }

        public ActionResult PartialInvoiceProductDetails()
        {
            List<OrderDetailsSummaryProducts> oproduct = new List<OrderDetailsSummaryProducts>();
            try
            {
                string Is_PageLoad = string.Empty;
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
                List<GpsStatusClasstOutput> omel = new List<GpsStatusClasstOutput>();
                DataTable dt = new DataTable();
                DataTable dtproduct = new DataTable();
                dtproduct = objInv.GetProducts();
                List<Productlist_Order> oproductlist = new List<Productlist_Order>();
                oproductlist = APIHelperMethods.ToModelList<Productlist_Order>(dtproduct);
                mproductwindow.products = oproductlist;
                return PartialView("_PartialInvoiceProductDetails", mproductwindow);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult PartialInvoiceAllProducts(string OrderID = null, String InvoiceID = null)
        {
            try
            {
                string Is_PageLoad = string.Empty;
                List<InvoiceDetailsStatusProductslist> omel = new List<InvoiceDetailsStatusProductslist>();

                DataTable dt = new DataTable();
                if (!string.IsNullOrEmpty(InvoiceID) && !string.IsNullOrEmpty(OrderID))
                {
                    dt = objInv.GetallInvoiceDetails(OrderID, InvoiceID);
                    omel = APIHelperMethods.ToModelList<InvoiceDetailsStatusProductslist>(dt);
                }
                return PartialView("_PartialInvoiceAllProducts", omel);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult DeleteProduct(String OrderId, String ProdID, String InvoiceID)
        {
            int output = objInv.InvoiceProductModifyDelete(ProdID, OrderId, "0", "0", "Delete", InvoiceID);
            if (output > 0)
            {
                return Json("Success");
            }
            else
            {
                return Json("failure");
            }
        }

        public ActionResult EditInvoiceProducts(string OrderId, string ProdID)
        {

            List<OrderDetailsSummaryProducts> oproduct = new List<OrderDetailsSummaryProducts>();
            try
            {
                string Is_PageLoad = string.Empty;
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
                List<GpsStatusClasstOutput> omel = new List<GpsStatusClasstOutput>();

                DataTable dt = new DataTable();
                DataTable dtproduct = new DataTable();
                dtproduct = objInv.GetProducts();
                List<Productlist_Order> oproductlist = new List<Productlist_Order>();
                oproductlist = APIHelperMethods.ToModelList<Productlist_Order>(dtproduct);

               DataTable dtquery = objInv.InvoiceProductFetch(ProdID, OrderId, "Edit");
                mproductwindow = APIHelperMethods.ToModel<OrderDetailsSummaryProducts>(dtquery);
                mproductwindow.products = oproductlist;


                return Json(mproductwindow);

            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }

        public ActionResult UpdateInvoiceProduct(String Order_ID,String Invoice_ProdId,String Product_Qty,String Product_Rate,String InvoiceID)
        {
            if (Invoice_ProdId != "")
            {
                int output = objInv.InvoiceProductModifyDelete(Invoice_ProdId, Order_ID, Product_Qty, Product_Rate, "Update", InvoiceID);
                if (output > 0)
                {
                    return Json("Success");
                }
                else
                {
                    return Json("failure");
                }
            }
            else
            {
                return Json("failure");
            }
        }
    }
}
﻿//====================================================== Revision History ==========================================================
//1.0  03-02-2023    2.0.38    Priti     0025604: Enhancement Required in the Order Summary Report
//2.0  17-03-2023    2.0.39    Priti     0025734: Separate Design required to exclude the MRP & Discount fields in the Sales Order output if these settings are off in Sales Order
//====================================================== Revision History ==========================================================

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web.Mvc;
using BusinessLogicLayer;
using BusinessLogicLayer.SalesmanTrack;
using BusinessLogicLayer.SalesTrackerReports;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using Models;
using SalesmanTrack;
using UtilityLayer;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class OrderSummaryController : Controller
    {
        CommonBL objSystemSettings = new CommonBL();//1.0
        UserList lstuser = new UserList();
        OrderList objshop = new OrderList();
        DataTable dtuser = new DataTable();
        DataTable dtstate = new DataTable();
        DataTable dtshop = new DataTable();
        List<OrderDetailsSummaryProducts> oproduct = new List<OrderDetailsSummaryProducts>();
        OrderDetailsSummaryProducts mproductwindow = new OrderDetailsSummaryProducts();
        List<OrderDetailsSummary> omodel = new List<OrderDetailsSummary>();
        ProductDetails _productDetails = new ProductDetails();
        DataTable dtquery = new DataTable();
        public ActionResult Summary()
        {

            try
            {

                Reportorderregisterinput omodel = new Reportorderregisterinput();
                string userid = Session["userid"].ToString();
                omodel.selectedusrid = userid;
                omodel.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                omodel.Todate = DateTime.Now.ToString("dd-MM-yyyy");

                DataTable dt = new SalesSummary_Report().GetStateMandatory(Session["userid"].ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewBag.StateMandatory = dt.Rows[0]["IsStateMandatoryinReport"].ToString();
                }
                //Mantis Issue 24944
                EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/OrderSummary/Summary");
                ViewBag.CanPrint = rights.CanPrint;
                //End of Mantis Issue 24944
                return View(omodel);

            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
        public ActionResult PartialOrderSummary(Reportorderregisterinput model)
        {
            try
            {
                string Is_PageLoad = string.Empty;
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
                List<GpsStatusClasstOutput> omel = new List<GpsStatusClasstOutput>();

                DataTable dt = new DataTable();

                if (model.Fromdate == null)
                {
                    model.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                }

                if (model.Todate == null)
                {
                    model.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                }

                if (model.Is_PageLoad == "0") Is_PageLoad = "Ispageload";

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

                string shop = "";
                int j = 1;

                if (model.shopId != null && model.shopId.Count > 0)
                {
                    foreach (string item in model.shopId)
                    {
                        if (j > 1)
                            shop = shop + "," + item;
                        else
                            shop = item;
                        j++;
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

                //Rev Debashis
                if (model.IsPaitentDetails != null)
                {
                    TempData["IsPaitentDetails"] = model.IsPaitentDetails;
                    TempData.Keep();
                }
                //End of Rev Debashis

                if (model.Is_PageLoad != "0")
                {
                    double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;
                    if (days <= 30)
                    {
                        dt = objshop.GetallorderListSummary(state, shop, datfrmat, dattoat, empcode, Userid);
                    }
                    omodel = APIHelperMethods.ToModelList<OrderDetailsSummary>(dt);
                }
                //Mantis Issue 24944
                EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/OrderSummary/Summary");
                ViewBag.CanPrint = rights.CanPrint;
                //End of Mantis Issue 24944
                TempData["OrderSummaryList"] = omodel;
                return PartialView("_PartialOrderSummary", omodel);



            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }
        public ActionResult PartialOrderProductDetails()
        {
            string IsDiscountInOrder = objSystemSettings.GetSystemSettingsResult("IsDiscountInOrder");//REV 1.0
            string IsViewMRPInOrder = objSystemSettings.GetSystemSettingsResult("IsViewMRPInOrder");//REV 1.0
            List<OrderDetailsSummaryProducts> oproduct = new List<OrderDetailsSummaryProducts>();
            try
            {
                string Is_PageLoad = string.Empty;
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
                List<GpsStatusClasstOutput> omel = new List<GpsStatusClasstOutput>();

                DataTable dt = new DataTable();
                DataTable dtproduct = new DataTable();
                dtproduct = objshop.GetProducts();
                List<Productlist_Order> oproductlist = new List<Productlist_Order>();

                oproductlist = APIHelperMethods.ToModelList<Productlist_Order>(dtproduct);

                mproductwindow.products = oproductlist;

                //REV 1.0
                ViewBag.IsDiscountInOrder = IsDiscountInOrder;
                ViewBag.IsViewMRPInOrder = IsViewMRPInOrder;
                //REV 1.0 END
                return PartialView("_PartialOrderProductDetails", mproductwindow);

            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }
        public ActionResult PartialOrderSummaryAllProducts(string OrderId = null)
        {

            try
            {     
                string IsDiscountInOrder = objSystemSettings.GetSystemSettingsResult("IsDiscountInOrder");//REV 1.0
                string IsViewMRPInOrder = objSystemSettings.GetSystemSettingsResult("IsViewMRPInOrder");//REV 1.0
                string Is_PageLoad = string.Empty;
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
                List<GpsStatusClasstOutput> omel = new List<GpsStatusClasstOutput>();

                DataTable dt = new DataTable();
                if (!string.IsNullOrEmpty(OrderId))
                {
                    dt = objshop.GetallorderDetails(Int32.Parse(OrderId));
                    mproductwindow.productdetails = APIHelperMethods.ToModelList<OrderDetailsSummaryProductslist>(dt);

                }


                //REV 1.0
                ViewBag.IsDiscountInOrder = IsDiscountInOrder;
                ViewBag.IsViewMRPInOrder = IsViewMRPInOrder;
                //REV 1.0 END
                return PartialView("_PartialOrderSummaryAllProducts", mproductwindow.productdetails);

            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }



        public ActionResult DeleteOrder(string OrderId)
        {
            int output = objshop.OrderDelete(OrderId);
            if (output > 0)
            {
                return Json("Success");
            }
            else
            {
                return Json("failure");
            }
        }
        //Mantis Issue 24944
        public JsonResult PrintSalesOrder(string OrderId)
        {
            
            string[] filePaths = new string[] { };
            string DesignPath = "";
            if (ConfigurationManager.AppSettings["IsDevelopedZone"] != null)
            {
                DesignPath = @"Reports\Reports\RepxReportDesign\OrderSummary\DocDesign\Designes";
            }
            else
            {
                DesignPath = @"Reports\RepxReportDesign\OrderSummary\DocDesign\Designes";
            }
            //DesignPath = @"Reports\Reports\RepxReportDesign\OrderSummary\DocDesign\Designes";
            string fullpath = Server.MapPath("~");
            fullpath = fullpath.Replace("ERP.UI\\", "");
            //fullpath = "D:\\FTS-GIT\\";
            string DesignFullPath = fullpath + DesignPath;
            filePaths = System.IO.Directory.GetFiles(DesignFullPath, "*.repx");
            List<DesignList> Listobj = new List<DesignList>();
            DesignList desig = new DesignList();
            foreach (string filename in filePaths)
            {
                //Rev 2.0
                desig = new DesignList();
                //Rev 2.0 End
                string reportname = Path.GetFileNameWithoutExtension(filename);
                string name = "";
                if (reportname.Split('~').Length > 1)
                {
                    name = reportname.Split('~')[0];
                }
                else 
                {
                    name = reportname;
                }
                string reportValue = reportname;                

                desig.name = name;
                desig.reportValue = reportname;
                Listobj.Add(desig);               

            }
            //CmbDesignName.SelectedIndex = 0;
            return Json(Listobj, JsonRequestBehavior.AllowGet);
        }
        public class DesignList
        {
            public string name { get; set; }
            public string reportValue { get; set; }

        }
        //End of MAntis Issue 24944
        public ActionResult DeleteProduct(int OrderId, int ProdID)
        {
            int output = objshop.OrderProductModifyDelete(ProdID, OrderId, 0, 0,0,0, "Delete");
            if (output > 0)
            {
                return Json("Success");
            }
            else
            {
                return Json("failure");
            }
        }

        public ActionResult EditOrderProducts(string OrderId, string ProdID)
        {

            List<OrderDetailsSummaryProducts> oproduct = new List<OrderDetailsSummaryProducts>();
            try
            {
                string Is_PageLoad = string.Empty;
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
                List<GpsStatusClasstOutput> omel = new List<GpsStatusClasstOutput>();

                DataTable dt = new DataTable();
                DataTable dtproduct = new DataTable();
                dtproduct = objshop.GetProducts();
                List<Productlist_Order> oproductlist = new List<Productlist_Order>();
                oproductlist = APIHelperMethods.ToModelList<Productlist_Order>(dtproduct);

                dtquery = objshop.OrderProductFetch(ProdID, OrderId, "Edit");
                mproductwindow = APIHelperMethods.ToModel<OrderDetailsSummaryProducts>(dtquery);
                // Mantis Issue 25478
                //mproductwindow.products = oproductlist;
                // End of Mantis Issue 25478
                return Json(mproductwindow);
                
            }
            catch
            {
               return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }



        public ActionResult UpdateOrderProduct(OrderDetailsSummaryProducts model)
        {
            if (model.Order_ProdId != 0)
            {
                //REV 1.0
                //int output = objshop.OrderProductModifyDelete(model.Order_ProdId, model.Order_ID, model.Product_Qty, model.Product_Rate, "Update");
                int output = objshop.OrderProductModifyDelete(model.Order_ProdId, model.Order_ID, model.Product_Qty, model.Product_Rate, model.Product_MRP, model.Product_Discount, "Update");
                //REV 1.0 END
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



        public ActionResult ExporOrdrSummaryList(int type)
        {
            ViewData["OrderSummaryList"] = TempData["OrderSummaryList"];
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetOrderRegisterList(), ViewData["OrderSummaryList"]);
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetOrderRegisterList(), ViewData["OrderSummaryList"]);
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetOrderRegisterList(), ViewData["OrderSummaryList"]);
                case 4:
                    return GridViewExtension.ExportToRtf(GetOrderRegisterList(), ViewData["OrderSummaryList"]);
                case 5:
                    return GridViewExtension.ExportToCsv(GetOrderRegisterList(), ViewData["OrderSummaryList"]);
                //break;

                default:
                    break;
            }
            TempData["OrderSummaryList"] = ViewData["OrderSummaryList"];
            return null;
        }
        private GridViewSettings GetOrderRegisterList()
        {

            var settings = new GridViewSettings();
            settings.Name = "gridsummarylist";
            //    settings.CallbackRouteValues = new { Controller = "Report", Action = "GetRegisterreporttatusList" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Order Summary";

             //Rev Debashis
    if (TempData["IsPaitentDetails"].ToString() == "0")
    {
        //End of Rev Debashis
            settings.Columns.Add(x =>
            {
                x.FieldName = "EmployeeName";
                x.Caption = "Employee Name";
                x.VisibleIndex = 1;
                x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "BRANCHDESC";
                x.Caption = "Branch";
                x.VisibleIndex = 2;
                x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
            });


            settings.Columns.Add(x =>
            {
                x.FieldName = "shop_name";
                x.Caption = "Shop Name";
                x.VisibleIndex = 3;
                x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ENTITYCODE";
                x.Caption = "Code";
                x.VisibleIndex = 4;
                x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "address";
                x.Caption = "Address";
                x.VisibleIndex = 5;
                x.Width = System.Web.UI.WebControls.Unit.Percentage(20);

            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "owner_contact_no";
                x.Caption = "Contact";
                x.VisibleIndex = 6;
                x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shoptype";
                x.Caption = "Shop type";
                x.VisibleIndex = 7;
                x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
            });


            settings.Columns.Add(x =>
            {
                x.FieldName = "date";
                x.Caption = "Order Date";
                x.VisibleIndex = 8;
                x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
            });


            settings.Columns.Add(x =>
            {
                x.FieldName = "OrderCode";
                x.Caption = "Order Number";
                x.VisibleIndex = 9;
                x.Width = System.Web.UI.WebControls.Unit.Percentage(15);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "order_amount";
                x.Caption = "Order Value";
                x.VisibleIndex = 10;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                x.PropertiesEdit.DisplayFormatString = "0.00";
            });

        //Rev Debashis
    }
    if (TempData["IsPaitentDetails"].ToString() == "1")
    {
        settings.Columns.Add(x =>
        {
            x.FieldName = "EmployeeName";
            x.Caption = "Employee Name";
            x.VisibleIndex = 1;
            x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
        });

        settings.Columns.Add(x =>
        {
            x.FieldName = "BRANCHDESC";
            x.Caption = "Branch";
            x.VisibleIndex = 2;
            x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
        });


        settings.Columns.Add(x =>
        {
            x.FieldName = "shop_name";
            x.Caption = "Shop Name";
            x.VisibleIndex = 3;
            x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
        });

        settings.Columns.Add(x =>
        {
            x.FieldName = "ENTITYCODE";
            x.Caption = "Code";
            x.VisibleIndex = 4;
            x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
        });

        settings.Columns.Add(x =>
        {
            x.FieldName = "address";
            x.Caption = "Address";
            x.VisibleIndex = 5;
            x.Width = System.Web.UI.WebControls.Unit.Percentage(20);

        });
        settings.Columns.Add(x =>
        {
            x.FieldName = "owner_contact_no";
            x.Caption = "Contact";
            x.VisibleIndex = 6;
            x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
        });

        settings.Columns.Add(x =>
        {
            x.FieldName = "Shoptype";
            x.Caption = "Shop type";
            x.VisibleIndex = 7;
            x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
        });

        settings.Columns.Add(x =>
        {
            x.FieldName = "Patient_Name";
            x.Caption = "Patient Name";
            x.VisibleIndex = 8;
            //x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
        });

        settings.Columns.Add(x =>
        {
            x.FieldName = "Patient_Phone_No";
            x.Caption = "Patient Phone No.";
            x.VisibleIndex = 9;
            //x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
        });

        settings.Columns.Add(x =>
        {
            x.FieldName = "Patient_Address";
            x.Caption = "Patient Address";
            x.VisibleIndex = 10;
            //x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
        });

        settings.Columns.Add(x =>
        {
            x.FieldName = "Hospital";
            x.Caption = "Patient Hospital";
            x.VisibleIndex = 11;
            // x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
        });

        settings.Columns.Add(x =>
        {
            x.FieldName = "Email_Address";
            x.Caption = "Patient Email Address";
            x.VisibleIndex = 12;
            //x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
        });

        settings.Columns.Add(x =>
        {
            x.FieldName = "date";
            x.Caption = "Order Date";
            x.VisibleIndex = 13;
            x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
        });


        settings.Columns.Add(x =>
        {
            x.FieldName = "OrderCode";
            x.Caption = "Order Number";
            x.VisibleIndex = 14;
            x.Width = System.Web.UI.WebControls.Unit.Percentage(15);
        });

        settings.Columns.Add(x =>
        {
            x.FieldName = "order_amount";
            x.Caption = "Order Value";
            x.VisibleIndex = 15;
            x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
            x.PropertiesEdit.DisplayFormatString = "0.00";
        });
    }
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

        public ActionResult getMrpDiscount(OrderDetailsSummaryProducts model)
        {
            if (model.Order_ProdId != 0)
            {
                dtquery = objshop.getMrpDiscount(model.Product_Id, "ProductIdWiseMrpDiscount");
                _productDetails = APIHelperMethods.ToModel<ProductDetails>(dtquery);                
                return Json(_productDetails);
            }
            else
            {
                return Json("failure");
            }
        }

    }
}
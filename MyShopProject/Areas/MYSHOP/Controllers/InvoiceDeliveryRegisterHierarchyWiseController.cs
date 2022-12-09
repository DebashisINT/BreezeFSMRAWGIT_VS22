using BusinessLogicLayer.SalesmanTrack;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using BusinessLogicLayer;
using System.Configuration;
using System.Data.SqlClient;
using Models;
using SalesmanTrack;
using BusinessLogicLayer.SalesTrackerReports;
using UtilityLayer;
using System.Collections;
using System.Globalization;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class InvoiceDeliveryRegisterHierarchyWiseController : Controller
    {
        //
        // GET: /MYSHOP/InvoiceDeliveryRegister/
        InvoiceDetailsUpdateBL objInv = new InvoiceDetailsUpdateBL();
        UserList lstuser = new UserList();
        ReportOrderRegister objgps = new ReportOrderRegister();
        DataTable dtuser = new DataTable();
        DataTable dtstate = new DataTable();
        DataTable dtshop = new DataTable();
        InvoiceHierarchywiseBL objBL = new InvoiceHierarchywiseBL();
        InvoiceDeliveryRegister_List obj = new InvoiceDeliveryRegister_List();
        OrderDetailsSummaryProducts mproductwindow = new OrderDetailsSummaryProducts();
        public ActionResult InvoiceDeliveryRegister()
        {
            try
            {
                Session["productList"] = null;
                Session["Orderid"] = null;

                InvoiceHierarchywise omodel = new InvoiceHierarchywise();
                string userid = Session["userid"].ToString();
                //dtuser = lstuser.GetUserList(userid);
                //dtshop = lstuser.GetShopList();

                dtuser = objBL.GetOrderList();
                List<orders> model = new List<orders>();

                model = APIHelperMethods.ToModelList<orders>(dtuser);
                omodel.order_list = model;

                //List<GetUsersStates> modelstate = new List<GetUsersStates>();
                //DataTable dtstate = lstuser.GetStateList();
                //modelstate = APIHelperMethods.ToModelList<GetUsersStates>(dtstate);


                //List<Getmasterstock> modelshop = new List<Getmasterstock>();
                //modelshop = APIHelperMethods.ToModelList<Getmasterstock>(dtshop);
                DataTable dtproduct = objInv.GetProducts();
                List<Productlist_Order> oproductlist = new List<Productlist_Order>();
                oproductlist = APIHelperMethods.ToModelList<Productlist_Order>(dtproduct);

                omodel.products = oproductlist;

                //omodel.userlsit = model;
                omodel.selectedusrid = userid;


                omodel.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                omodel.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                // omodel.states = modelstate;
                //omodel.shoplist = modelshop;



                //if (TempData["Orderregister"] != null)
                //{
                //    omodel.selectedusrid = TempData["Orderregister"].ToString();

                //    TempData.Clear();
                //}

                return View(omodel);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
        public ActionResult GetStateList()
        {
            try
            {
                List<Reportorderregisterinput> modelstate = new List<Reportorderregisterinput>();
                DataTable dtstate = lstuser.GetStateList();
                modelstate = APIHelperMethods.ToModelList<Reportorderregisterinput>(dtstate);
                return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_StatesPartial.cshtml", modelstate);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }
        //Grid Portion
        public ActionResult GetOrderRegisterList(Reportorderregisterinput model)
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


                double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;
                if (days <= 30)
                {
                    dt = obj.GetReportinvoiceRegister(datfrmat, dattoat, Userid, state, shop, empcode, "");
                }

                //return PartialView("~/Areas/MYSHOP/Views/InvoiceDeliveryRegister/_InvoiceRegisterReportPartial.cshtml", GetOrderRegisterList(Is_PageLoad));

                return Json(empcode, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }
        public ActionResult GetInvoiceRegisterreporttatusList(string Is_PageLoad)
        {

            return PartialView(GetDataInvoice(Is_PageLoad));

        }


        public IEnumerable GetDataInvoice(string Is_PageLoad)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);
            if (Is_PageLoad != "Ispageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSINVOICEREGISTER_REPORTs
                        where d.LOGIN_ID == Convert.ToInt32(Userid)
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSINVOICEREGISTER_REPORTs
                        where d.LOGIN_ID == Convert.ToInt32(Userid) && d.Employee_ID == "0"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
        }


        public ActionResult LoadImageDocument(string MapExpenseID)
        {
            //String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
            string fileLocation = System.Configuration.ConfigurationManager.AppSettings["Path"];
            List<ReimbursementApplicationbills> list = new List<ReimbursementApplicationbills>();
            string userid = Session["userid"].ToString();
            DataTable dt = obj.ReimbursementLoadImageDocument(MapExpenseID, userid);
            FilePathResult files = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    ReimbursementApplicationbills objj = new ReimbursementApplicationbills();
                    //objj.MapExpenseID = Convert.ToString(row["MapExpenseID"]);
                    //objj.Bills = fileLocation + Convert.ToString(row["Bills"]);
                    //objj.Image_Name = Convert.ToString(row["Image_Name"]);

                    //objj.MapExpenseID = fileLocation + Convert.ToString(row["OrderCode"]);
                    //objj.Bills = fileLocation + Convert.ToString(row["Inv_Img_id"]);
                    //objj.Image_Name = Convert.ToString(row["Inv_Img_id"]);
                    //list.Add(objj);

                    string path = fileLocation;
                    //byte[] fileBytes = System.IO.File.ReadAllBytes(path + Convert.ToString(row["Inv_Img_id"]));
                    string fileName = fileLocation + Convert.ToString(row["Inv_Img_id"]);
                    files = File(path + Convert.ToString(row["Inv_Img_id"]), System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

                    //using (var client = new WebClient())
                    //{
                    //    client.DownloadFile(path + Convert.ToString(row["Inv_Img_id"]), Convert.ToString(row["Inv_Img_id"]));
                    //}
                }
            }
            //if (false)
            //  //  return PartialView("~/Areas/MYSHOP/Views/Reimbursement/_ReimbursementLoadDDocument.cshtml", list);
            //else
            //{
            return Json(files);
            //}
        }

        public ActionResult ExporRegisterList(int type)
        {

            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(), GetDataInvoice(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(), GetDataInvoice(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(), GetDataInvoice(""));
                case 4:
                    return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(), GetDataInvoice(""));
                case 5:
                    return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(), GetDataInvoice(""));
                //break;

                default:
                    break;
            }

            return null;
        }
        private GridViewSettings GetEmployeeBatchGridViewSettings()
        {

            var settings = new GridViewSettings();
            settings.Name = "gridorderregister";
            settings.CallbackRouteValues = new { Controller = "InvoiceDeliveryRegister", Action = "GetRegisterreporttatusList" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Invoice/DeliveryRegisterList" + DateTime.Now;

            settings.Columns.Add(x =>
            {
                x.FieldName = "Employee_ID";
                x.Caption = "Employee ID";
                x.VisibleIndex = 1;
                x.Width = 150;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Employee_Name";
                x.Caption = "Employee Name";
                x.VisibleIndex = 2;
                x.Width = 150;
            });


            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_Name";
                x.Caption = "Shop Name";
                x.VisibleIndex = 3;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ENTITYCODE";
                x.Caption = "Code";
                x.VisibleIndex = 4;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Address";
                x.Caption = "Address";
                x.VisibleIndex = 5;
                x.Width = 300;

            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "Contact";
                x.Caption = "Contact";
                x.VisibleIndex = 6;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_Type";
                x.Caption = "Shop type";
                x.VisibleIndex = 7;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "PPName";
                x.Caption = "PP Name";
                x.VisibleIndex = 8;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "DDName";
                x.Caption = "DD Name";
                x.VisibleIndex = 9;
                x.Width = 100;
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "Order_Number";
                x.Caption = "Order No.";
                x.VisibleIndex = 10;
                x.Width = 100;
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "Order_Date";
                x.Caption = "Order Date";
                x.VisibleIndex = 11;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Order_Create_Date";
                x.Caption = "Order Entered On";
                x.VisibleIndex = 12;
                x.Width = 100;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy hh:mm:ss";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy hh:mm:ss";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Invoice_Date";
                x.Caption = "Inv. Date";
                x.VisibleIndex = 13;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Invoice_Number";
                x.Caption = "Inv. Number";
                x.VisibleIndex = 14;
                x.Width = 100;

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Invoice_Create_Date";
                x.Caption = "Inv. Entered On";
                x.VisibleIndex = 15;
                x.Width = 100;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy hh:mm:ss";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy hh:mm:ss";
            });

            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "ORDDATE";
            //    x.Caption = "Order Date";
            //    x.VisibleIndex = 3;
            //    x.Width = 100;
            //});


            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "ORDRNO";
            //    x.Caption = "Order Number";
            //    x.VisibleIndex = 3;
            //    x.Width = 200;

            //});



            settings.Columns.Add(x =>
            {
                x.FieldName = "PRODNAME";
                x.Caption = "Product";
                x.VisibleIndex = 16;
                x.Width = 200;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Product_Qty";
                x.Caption = "Quantity";
                x.VisibleIndex = 17;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.Width = 100;
                x.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Product_Rate";
                x.Caption = "Rate";
                x.VisibleIndex = 18;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.Width = 100;
                x.PropertiesEdit.DisplayFormatString = "0.00";
            });
            //    settings.Columns.Add(x =>
            //{
            //    x.FieldName = "VALUE";
            //    x.Caption = "Value";
            //    x.VisibleIndex = 3;
            //    x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            //    x.Width = 100;
            //});

            settings.Columns.Add(x =>
            {
                x.FieldName = "Product_TotalAmount";
                x.Caption = "Value";
                x.VisibleIndex = 19;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.Width = 100;
                x.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }
        public ActionResult GetRegisterreporttatusList(Reportorderregisterinput model)
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


                double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;
                if (days <= 30)
                {
                    dt = objgps.GetReportOrderRegister(datfrmat, dattoat, Userid, state, shop, empcode);
                }

                return PartialView("_InvoiceRegisterReportPartial", GetDataInvoice(Is_PageLoad));


            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }

        public PartialViewResult GetEmpList(EmployeeListModel model)
        {
            try
            {
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

                string desig = "";
                int k = 1;

                if (model.desgid != null && model.desgid.Count > 0)
                {
                    foreach (string item in model.desgid)
                    {
                        if (k > 1)
                            desig = desig + "," + item;
                        else
                            desig = item;
                        k++;
                    }

                }

                string dept = "";
                int L = 1;
                if (model.DeptId != null && model.DeptId.Count > 0)
                {
                    foreach (string item in model.DeptId)
                    {
                        if (L > 1)
                            dept = dept + "," + item;
                        else
                            dept = item;
                        L++;
                    }
                }



                DataTable dtemp = lstuser.Getemplist(state, desig, Convert.ToString(Session["userid"]), dept, "");


                DataView view = new DataView(dtemp);
                DataTable distinctValues = view.ToTable(true, "empcode", "empname");

                List<GetAllEmployee> modelemp = new List<GetAllEmployee>();
                modelemp = APIHelperMethods.ToModelList<GetAllEmployee>(distinctValues);

                return PartialView(modelemp);



            }
            catch
            {
                return PartialView();

            }
        }


        public ActionResult PartialInvoiceAllProducts(string OrderID = null, String InvoiceID = null)
        {
            try
            {
                string Is_PageLoad = string.Empty;
                List<InvoiceDetailsStatusProductslist> omel = new List<InvoiceDetailsStatusProductslist>();

                if (Convert.ToString(Session["Orderid"]) != OrderID)
                {
                    Session["Orderid"] = OrderID;
                    Session["productList"] = null;
                }


                DataTable dt = new DataTable();
                if (!string.IsNullOrEmpty(OrderID))
                {
                    if (Session["productList"] != null)
                    {
                        dt = (DataTable)Session["productList"];
                    }
                    else
                    {
                        dt = objBL.GetallInvoiceDetails(OrderID, InvoiceID);
                        Session["productList"] = dt;
                    }
                    omel = APIHelperMethods.ToModelList<InvoiceDetailsStatusProductslist>(dt);
                }
                return PartialView("PartialInvoiceAllProducts", omel);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult DeleteProduct(String OrderId, String ProdID, String InvoiceID)
        {
            //int output = objInv.InvoiceProductModifyDelete(ProdID, OrderId, "0", "0", "Delete", InvoiceID);
            int output = 1;
            try
            {
                DataTable dt = new System.Data.DataTable();
                if (Session["productList"] != null)
                {
                    dt = (DataTable)Session["productList"];

                    DataRow[] drr = dt.Select("Invoice_ProdId='" + ProdID + "'");

                    foreach (DataRow dr in drr)
                    {
                        dr.Delete();
                    }
                    dt.AcceptChanges();
                    Session["productList"] = dt;
                }
            }
            catch
            {
                output = 0;
            }



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

                if (Session["productList"] != null)
                {
                    dt = (DataTable)Session["productList"];

                    DataRow[] drr = dt.Select("Invoice_ProdId='" + ProdID + "'");
                }

                DataTable dtproduct = new DataTable();
                dtproduct = objInv.GetProducts();
                List<Productlist_Order> oproductlist = new List<Productlist_Order>();
                oproductlist = APIHelperMethods.ToModelList<Productlist_Order>(dtproduct);

                DataTable dtquery = dt.Select("Invoice_ProdId='" + ProdID + "'").CopyToDataTable();
                mproductwindow = APIHelperMethods.ToModel<OrderDetailsSummaryProducts>(dtquery);
                mproductwindow.products = oproductlist;


                return Json(mproductwindow);

            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }

        public ActionResult UpdateInvoiceProduct(String Order_ID, String Invoice_ProdId, String Product_Qty, String Product_Rate, String InvoiceID, string price)
        {
            if (Invoice_ProdId != "")
            {
                int output = 0;// objInv.InvoiceProductModifyDelete(Invoice_ProdId, Order_ID, Product_Qty, Product_Rate, "Update", InvoiceID);


                try
                {
                    DataTable dt = new System.Data.DataTable();
                    if (Session["productList"] != null)
                    {
                        dt = (DataTable)Session["productList"];

                        DataRow[] drr = dt.Select("Product_Id='" + Invoice_ProdId + "'");

                        foreach (DataRow dr in drr)
                        {
                            dr["Product_Qty"] = Convert.ToString(Product_Qty);
                            dr["Product_Rate"] = Convert.ToString(Product_Rate);
                            dr["Product_Price"] = Convert.ToString(price);

                        }
                        dt.AcceptChanges();
                        Session["productList"] = dt;
                    }
                }
                catch
                {
                    output = 0;
                }


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


        public ActionResult SaveInvoice(string orderid, string invoicedate, string refer_id)
        {


            try
            {

                string output = "";
                if (!string.IsNullOrEmpty(orderid))
                {

                    DBEngine objDb = new DBEngine();


                    DataTable dtOrd = objDb.GetDataTable("select UserID,OrderCode,OrderId from tbl_trans_fts_Orderupdate where OrderId='" + orderid + "'");

                    if (dtOrd != null && dtOrd.Rows.Count > 0)
                    {
                        DataTable dt = new System.Data.DataTable();
                        if (Session["productList"] != null)
                        {
                            dt = (DataTable)Session["productList"];

                        }

                        if (dt != null && dt.Rows.Count > 0)
                        {

                            orderid = Convert.ToString(dtOrd.Rows[0]["OrderCode"]);
                            DataTable dtProdinProcess = objDb.GetDataTable("select * from  FTS_ORDERSTAGE where ORDER_ID='" + orderid + "' and stage_id=1");
                            if (dtProdinProcess != null && dtProdinProcess.Rows.Count > 0)
                            {
                                string userid = Convert.ToString(Session["userid"]);

                                List<MyShop.Models.ACCOUNTINGALLOCATIONSLIST.ProductLists> omedl2 = new List<MyShop.Models.ACCOUNTINGALLOCATIONSLIST.ProductLists>();



                                decimal total = 0;
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        omedl2.Add(new MyShop.Models.ACCOUNTINGALLOCATIONSLIST.ProductLists()
                                        {
                                            id = Convert.ToString(dr["Product_Id"]),
                                            qty = Convert.ToDecimal(dr["Product_Qty"]),
                                            rate = Convert.ToDecimal(dr["Product_Rate"]),
                                            total_price = Convert.ToDecimal(dr["Product_Price"]),
                                            product_name = Convert.ToString(dr["sProducts_Name"])
                                        });

                                        total = total + Convert.ToDecimal(dr["Product_Price"]);
                                    }


                                }

                                string billid = userid + "_bill_" + DateTime.Now.ToString("ddMMyyyyhhmmss");
                                if (string.IsNullOrEmpty(refer_id))
                                    refer_id = billid;


                                string date = DateTime.ParseExact(invoicedate, "dd-MM-yyyy",
                                                    CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");


                                string JsonXML = MyShop.Models.ACCOUNTINGALLOCATIONSLIST.XmlConversion.ConvertToXml(omedl2, 0);
                                //End Add Product in add billing Tanmoy 22-11-2019

                                SqlCommand sqlcmd = new SqlCommand();
                                SqlConnection sqlcon = new SqlConnection(ConfigurationManager.AppSettings["DBConnectionDefault"]);
                                sqlcon.Open();

                                sqlcmd = new SqlCommand("Proc_FTS_BillingManagement", sqlcon);
                                sqlcmd.Parameters.Add("@user_id", userid);
                                sqlcmd.Parameters.Add("@bill_id", billid);
                                sqlcmd.Parameters.Add("@invoice_no", billid);
                                sqlcmd.Parameters.Add("@invoice_date", date);
                                sqlcmd.Parameters.Add("@invoice_amount", total);
                                sqlcmd.Parameters.Add("@remarks", "From FSM");
                                sqlcmd.Parameters.Add("@order_id", orderid);
                                //Add Product in add billing Tanmoy 22-11-2019
                                sqlcmd.Parameters.Add("@Product_List", JsonXML);
                                //End Add Product in add billing Tanmoy 22-11-2019
                                sqlcmd.Parameters.Add("@Action", "Insert");

                                sqlcmd.CommandType = CommandType.StoredProcedure;
                                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                                da.Fill(dt);
                                sqlcon.Close();

                                DataTable dtordProd = objDb.GetDataTable("update FTS_ORDERSTAGE set STAGE_ID=4 where ORDER_ID='" + orderid + "'");
                                output = "Invoice updated succsessfully.";

                            }
                            else
                            {
                                output = "Selected Orders are not 'In Process'. Cannot Invoice.";
                            }
                        }
                        else
                        {
                            output = "You must add one product to proceed.";
                        }
                    }

                    else
                    {
                        output = "Order not found. Cannot Invoice.";
                    }
                }
                else
                {
                    output = "Order not found. Cannot Invoice.";
                }
                return Json(output);

            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }

        public ActionResult SaveReceipt(string invoicenumber, string receiptamount)
        {
            string output = "";
            try
            {
                objBL.SaveReceipt(invoicenumber, receiptamount);
                return Json(output);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }

        public ActionResult GetReceiptDetails(string invoiceid)
        {

            string output = "";
            try
            {
                DBEngine dbengine = new DBEngine();
                DataTable dt = dbengine.GetDataTable("select invoice_no,OrderCode,CONVERT(VARCHAR(10),invoice_date,105) invoice_date,invoice_amount,ISNULL(Invoice_Unpaid,0.00) Invoice_Unpaid from tbl_FTS_BillingDetails where invoice_no='" + invoiceid + "'");
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (Convert.ToDecimal(dt.Rows[0]["Invoice_Unpaid"]) > 0)
                    {
                        return Json(new
                        {
                            isok = true,
                            invoice_no = Convert.ToString(dt.Rows[0]["invoice_no"]),
                            OrderCode = Convert.ToString(dt.Rows[0]["OrderCode"]),
                            invoice_date = Convert.ToString(dt.Rows[0]["invoice_date"]),
                            invoice_amount = Convert.ToString(dt.Rows[0]["invoice_amount"]),
                            Invoice_Unpaid = Convert.ToString(dt.Rows[0]["Invoice_Unpaid"])
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { isok = false, message = "Full payment done. Can not receipt." }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { isok = false, message = "No data found" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }

    }
}
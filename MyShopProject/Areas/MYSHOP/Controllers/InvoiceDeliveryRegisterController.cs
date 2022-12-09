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


namespace MyShop.Areas.MYSHOP.Controllers
{
    public class InvoiceDeliveryRegisterController : Controller
    {
        //
        // GET: /MYSHOP/InvoiceDeliveryRegister/
        UserList lstuser = new UserList();
        ReportOrderRegister objgps = new ReportOrderRegister();
        DataTable dtuser = new DataTable();
        DataTable dtstate = new DataTable();
        DataTable dtshop = new DataTable();
        InvoiceDeliveryRegister_List obj = new InvoiceDeliveryRegister_List();
        public ActionResult InvoiceDeliveryRegister()
        {
            try
            {
                Reportorderregisterinput omodel = new Reportorderregisterinput();
                string userid = Session["userid"].ToString();
                //dtuser = lstuser.GetUserList(userid);
                //dtshop = lstuser.GetShopList();
                // dtuser = lstuser.GetUserList();
                // List<GetUserName> model = new List<GetUserName>();

                // model = APIHelperMethods.ToModelList<GetUserName>(dtuser);


                //List<GetUsersStates> modelstate = new List<GetUsersStates>();
                //DataTable dtstate = lstuser.GetStateList();
                //modelstate = APIHelperMethods.ToModelList<GetUsersStates>(dtstate);


                //List<Getmasterstock> modelshop = new List<Getmasterstock>();
                //modelshop = APIHelperMethods.ToModelList<Getmasterstock>(dtshop);


                //omodel.userlsit = model;
                omodel.selectedusrid = userid;


                omodel.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                omodel.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                //Rev Debashis 0025198
                DataTable dtbranch = lstuser.GetHeadBranchList(Convert.ToString(Session["userbranchHierarchy"]), "HO");
                DataTable dtBranchChild = new DataTable();
                if (dtbranch.Rows.Count > 0)
                {
                    dtBranchChild = lstuser.GetChildBranch(Convert.ToString(Session["userbranchHierarchy"]));
                    if (dtBranchChild.Rows.Count > 0)
                    {
                        DataRow dr;
                        dr = dtbranch.NewRow();
                        dr[0] = 0;
                        dr[1] = "All";
                        dtbranch.Rows.Add(dr);
                        dtbranch.DefaultView.Sort = "BRANCH_ID ASC";
                        dtbranch = dtbranch.DefaultView.ToTable();
                    }
                }
                omodel.modelbranch = APIHelperMethods.ToModelList<GetBranch>(dtbranch);
                string h_id = omodel.modelbranch.First().BRANCH_ID.ToString();
                ViewBag.h_id = h_id;
                //End of Rev Debashis 0025198
                // omodel.states = modelstate;
                //omodel.shoplist = modelshop;



                //if (TempData["Orderregister"] != null)
                //{
                //    omodel.selectedusrid = TempData["Orderregister"].ToString();

                //    TempData.Clear();
                //}
                return View("~/Areas/MYSHOP/Views/InvoiceDeliveryRegister/InvoiceDeliveryRegister.cshtml", omodel);
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

                //Rev Debashis 0025198
                string Branch_Id = "";
                int b = 1;
                if (model.BranchId != null && model.BranchId.Count > 0)
                {
                    foreach (string item in model.BranchId)
                    {
                        if (b > 1)
                            Branch_Id = Branch_Id + "," + item;
                        else
                            Branch_Id = item;
                        b++;
                    }
                }
                //End of Rev Debashis 0025198

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
                    //Rev Debashis 0025198
                    //dt = obj.GetReportinvoiceRegister(datfrmat, dattoat, Userid, state, shop, empcode);
                    dt = obj.GetReportinvoiceRegister(datfrmat, dattoat, Userid, Branch_Id,state, shop, empcode);
                    //End of Rev Debashis 0025198
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

            return PartialView("~/Areas/MYSHOP/Views/InvoiceDeliveryRegister/_InvoiceRegisterReportPartial.cshtml", GetDataInvoice(Is_PageLoad));
           
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

            //Rev Debashis -- 0025198
            settings.Columns.Add(x =>
            {
                x.FieldName = "BRANCHDESC";
                x.Caption = "Branch";
                x.VisibleIndex = 3;
                x.Width = 150;
            });
            //End of Rev Debashis -- 0025198

            settings.Columns.Add(x =>
            {
                x.FieldName = "State_name";
                x.Caption = "State";
                x.VisibleIndex = 4;
                x.Width = 150;
            });

            //Rev Debashis -- 0024575
            settings.Columns.Add(x =>
            {
                x.FieldName = "SHOP_DISTRICT";
                x.Caption = "District";
                x.VisibleIndex = 5;
                x.Width = 150;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "SHOP_PINCODE";
                x.Caption = "Pincode";
                x.VisibleIndex = 6;
                x.Width = 150;
            });
            //End of Rev Debashis -- 0024575


            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_Name";
                x.Caption = "Shop Name";
                x.VisibleIndex = 7;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ENTITYCODE";
                x.Caption = "Code";
                x.VisibleIndex = 8;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Address";
                x.Caption = "Address";
                x.VisibleIndex = 9;
                x.Width = 300;

            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "Contact";
                x.Caption = "Contact";
                x.VisibleIndex = 10;
                x.Width = 100;
            });

            //Rev Debashis -- 0024577
            settings.Columns.Add(x =>
            {
                x.FieldName = "ALT_MOBILENO1";
                x.Caption = "Alternate Phone No.";
                x.VisibleIndex = 11;
                x.Width = 150;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "SHOP_OWNER_EMAIL2";
                x.Caption = "Alternate Email ID";
                x.VisibleIndex = 12;
                x.Width = 150;
            });
            //End of Rev Debashis -- 0024577

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_Type";
                x.Caption = "Shop type";
                x.VisibleIndex = 13;
                x.Width = 100;
            });

            //Rev Debashis -- 0024575
            settings.Columns.Add(x =>
            {
                x.FieldName = "SHOP_CLUSTER";
                x.Caption = "Cluster";
                x.VisibleIndex = 14;
                x.Width = 100;
            });
            //End of Rev Debashis -- 0024575

            settings.Columns.Add(x =>
            {
                x.FieldName = "PPName";
                x.Caption = "PP Name";
                x.VisibleIndex = 15;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "DDName";
                x.Caption = "DD Name";
                x.VisibleIndex = 16;
                x.Width = 100;
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "Order_Number";
                x.Caption = "Order No.";
                x.VisibleIndex = 17;
                x.Width = 100;
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "Order_Date";
                x.Caption = "Order Date";
                x.VisibleIndex = 18;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Order_Create_Date";
                x.Caption = "Order Entered On";
                x.VisibleIndex = 19;
                x.Width = 100;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy hh:mm:ss";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy hh:mm:ss";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Invoice_Date";
                x.Caption = "Inv. Date";
                x.VisibleIndex = 20;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Invoice_Number";
                x.Caption = "Inv. Number";
                x.VisibleIndex = 21;
                x.Width = 100;

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Invoice_Create_Date";
                x.Caption = "Inv. Entered On";
                x.VisibleIndex = 22;
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
                x.VisibleIndex = 23;
                x.Width = 200;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Product_Qty";
                x.Caption = "Quantity";
                x.VisibleIndex = 24;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.Width = 100;
                x.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Product_Rate";
                x.Caption = "Rate";
                x.VisibleIndex = 25;
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
                x.VisibleIndex = 26;
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
    }
}
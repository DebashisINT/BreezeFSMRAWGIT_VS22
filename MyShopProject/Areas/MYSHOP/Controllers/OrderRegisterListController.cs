using SalesmanTrack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BusinessLogicLayer.SalesTrackerReports;
using System.Data;
using UtilityLayer;
using System.Collections;
using System.Configuration;
using MyShop.Models;
using DevExpress.Web.Mvc;
using DevExpress.Web;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class OrderRegisterListController : Controller
    {
        UserList lstuser = new UserList();
        SalesSummary_Report objgps = new SalesSummary_Report();

        public ActionResult Report()
        {

            try
            {
                SalesSummaryReport omodel = new SalesSummaryReport();
                string userid = Session["userid"].ToString();
                omodel.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                omodel.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                DataTable dt = objgps.GetStateMandatory(Session["userid"].ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewBag.StateMandatory = dt.Rows[0]["IsStateMandatoryinReport"].ToString();
                }

                return View(omodel);

            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
        public ActionResult GetOrderRegisterList1()
        {
            return PartialView("PartialOrderRegisterList", OrderRegisterListSummary("0"));
        }
        public ActionResult GetOrderRegisterList(SalesSummaryReport model)
        {
            try
            {
                DataTable dt = new DataTable();
                string frmdate = string.Empty;

                if (model.is_pageload == "0")
                {
                    frmdate = "Ispageload";
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
                //Rev Debashis && 0025066
                if (model.IsShowRate != null)
                {
                    TempData["IsShowRate"] = model.IsShowRate;
                    TempData.Keep();
                }
                //End of Rev Debashis && 0025066
                
                double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;

                //if (days <= 7)
                //if (days <= 30)
                //{
                dt = objgps.GetOrderRegisterListReport(datfrmat, dattoat, Userid, state, desig, empcode);
                //}

                return PartialView("PartialGetOrderRegisterListSummary", OrderRegisterListSummary(frmdate));
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
                List<GetUsersStates> modelstate = new List<GetUsersStates>();
                DataTable dtstate = lstuser.GetStateList();
                modelstate = APIHelperMethods.ToModelList<GetUsersStates>(dtstate);

                return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_StatesPartial.cshtml", modelstate);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetDesigList()
        {
            try
            {
                DataTable dtdesig = lstuser.Getdesiglist();
                List<GetDesignation> modeldesig = new List<GetDesignation>();
                modeldesig = APIHelperMethods.ToModelList<GetDesignation>(dtdesig);

                return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_DesigPartial.cshtml", modeldesig);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetEmpList(SalesSummaryReport model)
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

                DataTable dtemp = lstuser.Getemplist(state, desig);
                List<GetAllEmployee> modelemp = new List<GetAllEmployee>();
                modelemp = APIHelperMethods.ToModelList<GetAllEmployee>(dtemp);

                return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_EmpPartial.cshtml", modelemp);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public IEnumerable OrderRegisterListSummary(string frmdate)
        {
            DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "ORDER REGISTER LIST");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            }

            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);

            if (frmdate != "Ispageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSORDERWITHPRODUCTATTRIBUTE_REPORTs
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSORDERWITHPRODUCTATTRIBUTE_REPORTs
                        where d.USERID == Convert.ToInt32(Userid) && d.EMPCODE == "0"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
        }

        public ActionResult ExporOrderRegisterList(int type)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetOrderRegisterListBatchGridViewSettings(), OrderRegisterListSummary(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetOrderRegisterListBatchGridViewSettings(), OrderRegisterListSummary(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetOrderRegisterListBatchGridViewSettings(), OrderRegisterListSummary(""));
                case 4:
                    return GridViewExtension.ExportToRtf(GetOrderRegisterListBatchGridViewSettings(), OrderRegisterListSummary(""));
                case 5:
                    return GridViewExtension.ExportToCsv(GetOrderRegisterListBatchGridViewSettings(), OrderRegisterListSummary(""));
                //break;

                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetOrderRegisterListBatchGridViewSettings()
        {
            DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "ORDER REGISTER LIST");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            }
            var settings = new GridViewSettings();
            settings.Name = "Order Register List";
            settings.CallbackRouteValues = new { Controller = "OrderRegisterList", Action = "GetOrderRegisterList" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "OrderRegisterList";

            //Rev Debashis && 0025066
            if (TempData["IsShowRate"].ToString() == "0")
            {
                //End of Rev Debashis && 0025066
                settings.Columns.Add(x =>
                {
                    x.FieldName = "EMPID";
                    x.Caption = "Employee ID";
                    x.VisibleIndex = 1;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='EMPID'");
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
                    x.FieldName = "EMPNAME";
                    x.Caption = "Employee Name";
                    x.VisibleIndex = 2;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='EMPNAME'");
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
                    x.FieldName = "BRANCHDESC";
                    x.Caption = "Branch";
                    x.VisibleIndex = 3;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='BRANCHDESC'");
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
                    x.FieldName = "SHOP_NAME";
                    x.Caption = "Shop Name";
                    x.VisibleIndex = 4;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOP_NAME'");
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
                    x.FieldName = "SHOP_CODE";
                    x.Caption = "Code";
                    x.VisibleIndex = 5;
                    x.Width = 100;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOP_CODE'");
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
                    x.FieldName = "SHOPADDRESS";
                    x.Caption = "Address";
                    x.VisibleIndex = 6;
                    x.Width = 180;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOPADDRESS'");
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
                    x.FieldName = "SHOP_CONTACT";
                    x.Caption = "Contact";
                    x.VisibleIndex = 7;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOP_CONTACT'");
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
                    x.FieldName = "SHOP_TYPE";
                    x.Caption = "Shop Type";
                    x.VisibleIndex = 8;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOP_TYPE'");
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
                    x.FieldName = "PP_NAME";
                    x.Caption = "PP Name";
                    x.VisibleIndex = 9;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PP_NAME'");
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
                    x.FieldName = "DD_NAME";
                    x.Caption = "DD Name";
                    x.VisibleIndex = 10;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DD_NAME'");
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
                    x.FieldName = "ORDER_DATE";
                    x.Caption = "Order Date";
                    x.VisibleIndex = 11;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ORDER_DATE'");
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
                    x.FieldName = "ORDER_NO";
                    x.Caption = "Order No.";
                    x.VisibleIndex = 12;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ORDER_NO'");
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
                    x.FieldName = "GENDER";
                    x.Caption = "Gender";
                    x.VisibleIndex = 13;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='GENDER'");
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
                    x.FieldName = "PRODUCT_NAME";
                    x.Caption = "Product";
                    x.VisibleIndex = 14;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PRODUCT_NAME'");
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
                    x.FieldName = "COLOR_NAME";
                    x.Caption = "Color";
                    x.VisibleIndex = 15;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='COLOR_NAME'");
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
                    x.FieldName = "SIZE";
                    x.Caption = "Size";
                    x.VisibleIndex = 16;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SIZE'");
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
                    x.FieldName = "QUANTITY";
                    x.Caption = "Quantity";
                    x.VisibleIndex = 17;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='QUANTITY'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.PropertiesEdit.DisplayFormatString = "0.00";
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.PropertiesEdit.DisplayFormatString = "0.00";
                    }
                });
                //Debashis && 0025066
            }
            if (TempData["IsShowRate"].ToString() == "1")
            {
                settings.Columns.Add(x =>
                {
                    x.FieldName = "EMPID";
                    x.Caption = "Employee ID";
                    x.VisibleIndex = 1;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='EMPID'");
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
                    x.FieldName = "EMPNAME";
                    x.Caption = "Employee Name";
                    x.VisibleIndex = 2;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='EMPNAME'");
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
                    x.FieldName = "BRANCHDESC";
                    x.Caption = "Branch";
                    x.VisibleIndex = 3;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='BRANCHDESC'");
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
                    x.FieldName = "SHOP_NAME";
                    x.Caption = "Shop Name";
                    x.VisibleIndex = 4;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOP_NAME'");
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
                    x.FieldName = "SHOP_CODE";
                    x.Caption = "Code";
                    x.VisibleIndex = 5;
                    x.Width = 100;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOP_CODE'");
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
                    x.FieldName = "SHOPADDRESS";
                    x.Caption = "Address";
                    x.VisibleIndex = 6;
                    x.Width = 180;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOPADDRESS'");
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
                    x.FieldName = "SHOP_CONTACT";
                    x.Caption = "Contact";
                    x.VisibleIndex = 7;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOP_CONTACT'");
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
                    x.FieldName = "SHOP_TYPE";
                    x.Caption = "Shop Type";
                    x.VisibleIndex = 8;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOP_TYPE'");
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
                    x.FieldName = "PP_NAME";
                    x.Caption = "PP Name";
                    x.VisibleIndex = 9;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PP_NAME'");
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
                    x.FieldName = "DD_NAME";
                    x.Caption = "DD Name";
                    x.VisibleIndex = 10;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DD_NAME'");
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
                    x.FieldName = "ORDER_DATE";
                    x.Caption = "Order Date";
                    x.VisibleIndex = 11;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ORDER_DATE'");
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
                    x.FieldName = "ORDER_NO";
                    x.Caption = "Order No.";
                    x.VisibleIndex = 12;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ORDER_NO'");
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
                    x.FieldName = "GENDER";
                    x.Caption = "Gender";
                    x.VisibleIndex = 13;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='GENDER'");
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
                    x.FieldName = "PRODUCT_NAME";
                    x.Caption = "Product";
                    x.VisibleIndex = 14;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PRODUCT_NAME'");
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
                    x.FieldName = "COLOR_NAME";
                    x.Caption = "Color";
                    x.VisibleIndex = 15;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='COLOR_NAME'");
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
                    x.FieldName = "SIZE";
                    x.Caption = "Size";
                    x.VisibleIndex = 16;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SIZE'");
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
                    x.FieldName = "QUANTITY";
                    x.Caption = "Quantity";
                    x.VisibleIndex = 17;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='QUANTITY'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.PropertiesEdit.DisplayFormatString = "0.00";
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.PropertiesEdit.DisplayFormatString = "0.00";
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "RATE";
                    x.Caption = "Rate";
                    x.VisibleIndex = 18;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='RATE'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.PropertiesEdit.DisplayFormatString = "0.00";
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.PropertiesEdit.DisplayFormatString = "0.00";
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "TOTAMOUNT";
                    x.Caption = "Value";
                    x.VisibleIndex = 19;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='TOTAMOUNT'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.PropertiesEdit.DisplayFormatString = "0.00";
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.PropertiesEdit.DisplayFormatString = "0.00";
                    }
                });
            }
                //End of Rev Debashis && 0025066
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
                int k = objgps.InsertPageRetention(Col, Session["userid"].ToString(), "ORDER REGISTER LIST");

                return Json(k, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BusinessLogicLayer;
using SalesmanTrack;
using System.Data;
using UtilityLayer;
using System.Web.Script.Serialization;
using MyShop.Models;
using BusinessLogicLayer.SalesmanTrack;
using DevExpress.Web.Mvc;
using DevExpress.Web;
using System.Collections;
using System.Configuration;
using BusinessLogicLayer.SalesTrackerReports;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class PaitentOrderRegisterController : Controller
    {
        // GET: /MYSHOP/GPSStatus/ 
        UserList lstuser = new UserList();
        ReportPaitentOrderRegister objgps = new ReportPaitentOrderRegister();

        public ActionResult OrderRegister()
        {
            try
            {
                ReportPaitentOrderRegisterInput omodel = new ReportPaitentOrderRegisterInput();
                string userid = Session["userid"].ToString();
                omodel.selectedusrid = userid;
                omodel.Is_PageLoad = "0";

                omodel.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                omodel.Todate = DateTime.Now.ToString("dd-MM-yyyy");

                DataTable dt = new SalesSummary_Report().GetStateMandatory(Session["userid"].ToString());
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

        public ActionResult GetRegisterreporttatusList(ReportPaitentOrderRegisterInput model)
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
                if (model.IsSchemeDetails != null)
                {
                    TempData["IsSchemeDetails"] = model.IsSchemeDetails;
                    TempData.Keep();
                }

                double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;
                //if (days <= 30)
                //{
                    dt = objgps.GetReportPaitentOrderRegister(datfrmat, dattoat, Userid, state, shop, empcode);
                //}

                return PartialView("_PartialPaitentOrderRegister", GetPaitentOrderRegisterList(Is_PageLoad));

            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }

        public IEnumerable GetPaitentOrderRegisterList(string Is_PageLoad)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);
            if (Is_PageLoad != "Ispageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSPAITENTORDERREGISTER_REPORTs
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSPAITENTORDERREGISTER_REPORTs
                        where d.USERID == Convert.ToInt32(Userid) && d.EMPCODE == "0"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
        }

        public ActionResult ExporRegisterList(int type)
        {

            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(), GetPaitentOrderRegisterList(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(), GetPaitentOrderRegisterList(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(), GetPaitentOrderRegisterList(""));
                case 4:
                    return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(), GetPaitentOrderRegisterList(""));
                case 5:
                    return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(), GetPaitentOrderRegisterList(""));
                //break;

                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetEmployeeBatchGridViewSettings()
        {

            var settings = new GridViewSettings();
            settings.Name = "gridpaitentorderregister";
            settings.CallbackRouteValues = new { Controller = "PaitentOrderRegister", Action = "GetRegisterreporttatusList" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Paitent Order Register List";

            settings.Columns.Add(column =>
            {
                column.Caption = "Employee ID";
                column.FieldName = "EMPLOYEE_ID";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Employee Name";
                column.FieldName = "EMPNAME";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Branch";
                column.FieldName = "BRANCHDESC";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Shop Name";
                column.FieldName = "SHOPNAME";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Code";
                column.FieldName = "ENTITYCODE";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Address";
                column.FieldName = "ADDRESS";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Contact";
                column.FieldName = "CONTACT";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Shop Type";
                column.FieldName = "SHOPTYPE";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Paitent Name";
                column.FieldName = "PATIENT_NAME";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Paitent Ph. No.";
                column.FieldName = "PATIENT_PHONE_NO";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Paitent Address";
                column.FieldName = "PATIENT_ADDRESS";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Paitent Hospital";
                column.FieldName = "HOSPITAL";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Paitent Email ID";
                column.FieldName = "EMAIL_ADDRESS";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "PP Name";
                column.FieldName = "PPNAME";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "DD Name";
                column.FieldName = "DDNAME";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Order Date";
                column.FieldName = "ORDDATE";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Order No";
                column.FieldName = "ORDRNO";
            });
            settings.Columns.Add(column =>
            {
                column.Caption = "Product";
                column.FieldName = "PRODUCT";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Quantity";
                column.FieldName = "QUANTITY";
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Rate";
                column.FieldName = "RATE";
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Order Value";
                column.FieldName = "ORDVALUE";
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "MRP";
                column.FieldName = "MRP";
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });
            if (TempData["IsSchemeDetails"].ToString() == "1")
            {
                settings.Columns.Add(column =>
                {
                    column.FieldName = "SCHEME_QTY";
                    column.Caption = "Scheme Qty";
                    column.PropertiesEdit.DisplayFormatString = "0.00";
                });
                settings.Columns.Add(column =>
                {
                    column.FieldName = "SCHEME_RATE";
                    column.Caption = "Scheme Rate";
                    column.PropertiesEdit.DisplayFormatString = "0.00";
                });
                settings.Columns.Add(column =>
                {
                    column.FieldName = "TOTAL_SCHEME_PRICE";
                    column.Caption = "Scheme Value";
                    column.PropertiesEdit.DisplayFormatString = "0.00";
                });
            }
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }
    }
}
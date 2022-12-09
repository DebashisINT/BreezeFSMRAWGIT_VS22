using BusinessLogicLayer.SalesmanTrack;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using MyShop.Models;
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
    public class ChemistStockistDetailsController : Controller
    {
        ChemistStockistDetailsBL obj = new ChemistStockistDetailsBL();
        public ActionResult ChemistStockistIndex()
        {
            try
            {
                string key = Convert.ToString(Session["ApiKey"]);
                ChemistStockistDetailsModel omodel = new ChemistStockistDetailsModel();
                string userid = Session["userid"].ToString();
                omodel.selectedusrid = userid;
                omodel.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                omodel.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                omodel.KeyId = key;
                return View(omodel);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult ChemistStockistDetailslistGrid(string Is_PageLoad)
        {
            return PartialView(GetDataDetails(Is_PageLoad));
        }

        public IEnumerable GetDataDetails(string Is_PageLoad)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);
            if (Is_PageLoad != "Ispageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSCHEMISTSTOCKIST_REPORTs
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSCHEMISTSTOCKIST_REPORTs
                        where d.USERID == Convert.ToInt32(Userid) && d.EMPCODE == "0"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
        }

        public ActionResult GetChemistDetailsList(ChemistStockistDetailsModel model)
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
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];

                double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;
                if (days <= 30)
                {
                    dt = obj.GetReportChemistDetails(datfrmat, dattoat, Userid, state, empcode, weburl);
                }

                return Json(empcode, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }

        public ActionResult ExporRegisterList(int type)
        {

            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetChemistBatchGridViewSettings(), GetDataDetails(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetChemistBatchGridViewSettings(), GetDataDetails(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetChemistBatchGridViewSettings(), GetDataDetails(""));
                case 4:
                    return GridViewExtension.ExportToRtf(GetChemistBatchGridViewSettings(), GetDataDetails(""));
                case 5:
                    return GridViewExtension.ExportToCsv(GetChemistBatchGridViewSettings(), GetDataDetails(""));
                //break;

                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetChemistBatchGridViewSettings()
        {

            var settings = new GridViewSettings();
            settings.Name = "gridChemistStockistDetails";
            //settings.CallbackRouteValues = new { Controller = "InvoiceDeliveryRegister", Action = "GetRegisterreporttatusList" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Chemist Stockist Details Report";

            settings.Columns.Add(x =>
            {
                x.FieldName = "EMPID";
                x.Caption = "Emp ID";
                x.VisibleIndex = 1;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "EMPLOYEE_NAME";
                x.Caption = "Employee Name";
                x.VisibleIndex = 2;
                x.Width = 150;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "DESIGNATION";
                x.Caption = "Designation";
                x.VisibleIndex = 3;
                x.Width = 80;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "REPORT_TO";
                x.Caption = "Report To";
                x.VisibleIndex = 4;
                x.Width = 200;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "STATE_NAME";
                x.Caption = "State";
                x.VisibleIndex = 5;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_Name";
                x.Caption = "Party Name";
                x.VisibleIndex = 6;
                x.Width = 200;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "SHOP_TYPE";
                x.Caption = "Type";
                x.VisibleIndex = 7;
                x.Width = 50;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Address";
                x.Caption = "Address";
                x.VisibleIndex = 8;
                x.Width = 300;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_Owner";
                x.Caption = "Contact Name";
                x.VisibleIndex = 9;
                x.Width = 200;
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_Owner_Contact";
                x.Caption = "Contact No";
                x.VisibleIndex = 10;
                x.Width = 80;
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_Owner_Email";
                x.Caption = "Contact Email";
                x.VisibleIndex = 11;
                x.Width = 100;
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "dob";
                x.Caption = "Contact DOB";
                x.VisibleIndex = 12;
                x.Width = 100;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "date_aniversary";
                x.Caption = "Contact Anniversary";
                x.VisibleIndex = 13;
                x.Width = 100;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Partner_Name";
                x.Caption = "Prop/Partner Name";
                x.VisibleIndex = 14;
                x.Width = 300;
            });


            settings.Columns.Add(x =>
            {
                x.FieldName = "FamilyMember_DOB";
                x.Caption = "Family DOB";
                x.VisibleIndex = 15;
                x.Width = 100;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "KeyPerson_Name";
                x.Caption = "Key Person Name";
                x.VisibleIndex = 16;
                x.Width = 300;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "phone_no";
                x.Caption = "Phone No";
                x.VisibleIndex = 17;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Addtional_DOB";
                x.Caption = "DOB";
                x.VisibleIndex = 18;
                x.Width = 100;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "Addtional_DOA";
                x.Caption = "Anniversary";
                x.VisibleIndex = 19;
                x.Width = 100;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CREATE_DATE";
                x.Caption = "Created On";
                x.VisibleIndex = 20;
                x.Width = 100;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy hh:mm:ss";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy hh:mm:ss";
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
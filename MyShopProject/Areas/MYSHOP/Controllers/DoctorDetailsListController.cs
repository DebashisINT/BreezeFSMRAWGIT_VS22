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
    public class DoctorDetailsListController : Controller
    {
        DoctorDetailListBL obj = new DoctorDetailListBL();
        public ActionResult DoctorDetailsIndex()
        {
            try
            {
                string key = Convert.ToString(Session["ApiKey"]);
                DoctorDetailsListModel omodel = new DoctorDetailsListModel();
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

        public ActionResult DoctorDetailslistGrid(string Is_PageLoad)
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
                var q = from d in dc.FTSDOCTORLIST_REPORTs
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSDOCTORLIST_REPORTs
                        where d.USERID == Convert.ToInt32(Userid) && d.EMPCODE == "0"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
        }

        public ActionResult GetDoctorDetailsList(DoctorDetailsListModel model)
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
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["DoctorDegree"];

                double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;
                if (days <= 30)
                {
                    dt = obj.GetReportDoctorDetails(datfrmat, dattoat, Userid, state, empcode, weburl);
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
                    return GridViewExtension.ExportToPdf(GetDoctorBatchGridViewSettings(), GetDataDetails(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetDoctorBatchGridViewSettings(), GetDataDetails(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetDoctorBatchGridViewSettings(), GetDataDetails(""));
                case 4:
                    return GridViewExtension.ExportToRtf(GetDoctorBatchGridViewSettings(), GetDataDetails(""));
                case 5:
                    return GridViewExtension.ExportToCsv(GetDoctorBatchGridViewSettings(), GetDataDetails(""));
                //break;

                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetDoctorBatchGridViewSettings()
        {

            var settings = new GridViewSettings();
            settings.Name = "gridDoctorDetails";
            //settings.CallbackRouteValues = new { Controller = "InvoiceDeliveryRegister", Action = "GetRegisterreporttatusList" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Doctor Details Report";

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
                x.FieldName = "SPECIALIZATION";
                x.Caption = "Specalization";
                x.VisibleIndex = 14;
                x.Width = 100;
            });


            settings.Columns.Add(x =>
            {
                x.FieldName = "AVG_PATIENT_PER_DAY";
                x.Caption = "Average Patient";
                x.VisibleIndex = 15;
                x.Width = 100;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CATEGORY";
                x.Caption = "Category";
                x.VisibleIndex = 16;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "FAMILY_MEMBER_DOB";
                x.Caption = "Family DOB";
                x.VisibleIndex = 17;
                x.Width = 100;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "DOC_ADDRESS";
                x.Caption = "Doc. Address";
                x.VisibleIndex = 18;
                x.Width = 300;
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "PINCODE";
                x.Caption = "Pin Code";
                x.VisibleIndex = 19;
                x.Width = 80;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Chamber_Hospital";
                x.Caption = "Chamber/Hospital";
                x.VisibleIndex = 20;
                x.Width = 100;
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "CHEMIST_NAME";
                x.Caption = "Chemist Name";
                x.VisibleIndex = 21;
                x.Width = 200;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CHEMIST_ADDRESS";
                x.Caption = "Chemist Address";
                x.VisibleIndex = 22;
                x.Width = 300;
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "CHEMIST_PINCODE";
                x.Caption = "Pin Code";
                x.VisibleIndex = 23;
                x.Width = 80;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ASSISTANT_NAME";
                x.Caption = "Assistant Name";
                x.VisibleIndex = 24;
                x.Width = 200;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ASSISTANT_CONTACT_NO";
                x.Caption = "Assistant Phone";
                x.VisibleIndex = 25;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ASSISTANT_DOB";
                x.Caption = "Asst. DOB";
                x.VisibleIndex = 26;
                x.Width = 100;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ASSISTANT_DOA";
                x.Caption = "Asst. Anniversary";
                x.VisibleIndex = 27;
                x.Width = 100;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ASSISTANT_FAMILY_DOB";
                x.Caption = "Asst. Family DOB";
                x.VisibleIndex = 28;
                x.Width = 100;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "DEGREE";
                x.Caption = "Degree";
                x.VisibleIndex = 29;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CREATE_DATE";
                x.Caption = "Created On";
                x.VisibleIndex = 30;
                x.Width = 150;
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
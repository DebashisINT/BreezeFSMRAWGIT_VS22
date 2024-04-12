#region======================================Revision History=========================================================================
//1.0   V2 .0.39    PRITI       13/02/2023      0025663:Last Visit fields shall be available in Outlet Reports
#endregion===================================End of Revision History==================================================================

using BusinessLogicLayer.SalesmanTrack;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using MyShop.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class CustomerDetailsController : Controller
    {
        CustomerDetailsBL obj = new CustomerDetailsBL();
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Rendermainpage()
        {
            return PartialView();
        }

        public PartialViewResult Rendergrid(string ispageload)
        {
            return PartialView(GetReport(ispageload));
        }

        [HttpPost]
        public ActionResult GenerateTable(CustomerDetailsModel model)
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
                string Deptid = "";
                 k = 1;
                 if (model.Department != null && model.Department.Count > 0)
                {
                    foreach (string item in model.Department)
                    {
                        if (k > 1)
                            Deptid = Deptid + "," + item;
                        else
                            Deptid = item;
                        k++;
                    }
                }

                //Rev Debashis
                //double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;
                //if (days <= 30)
                //{
                //    dt = obj.GetReportCustomerDetails(datfrmat, dattoat, Userid, state, empcode, Desgid, Deptid);
                //}
                dt = obj.GetReportCustomerDetails(datfrmat, dattoat, Userid, state, empcode, Desgid, Deptid);
                //End of Rev Debashis
                return Json(Userid, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public IEnumerable GetReport(string ispageload)
        {
            string connectionString = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"]);
            string Userid = Convert.ToString(Session["userid"]);

            if (ispageload != "1")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTS_CustomerDetailsReports
                        where d.USERID == Convert.ToInt32(Userid)
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTS_CustomerDetailsReports
                        where d.USERID == 0
                        select d;
                return q;
            }
        }

        public ActionResult ExportCustomerDetailsReport(int type, string IsPageload)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(), GetReport(IsPageload));
                //break;
                case 2:
                    return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(), GetReport(IsPageload));
                //break;
                case 3:
                    return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(), GetReport(IsPageload));
                case 4:
                    return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(), GetReport(IsPageload));
                case 5:
                    return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(), GetReport(IsPageload));
                //break;

                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetEmployeeBatchGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "CustomerDetailsReport";
            settings.CallbackRouteValues = new { Controller = "CustomerDetails", Action = "GenerateTable" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "CustomerDetailsReport";

            settings.Columns.Add(column =>
            {
                column.Caption = "Date";
                column.FieldName = "Shop_CreateTime";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ColumnType = MVCxGridViewColumnType.DateEdit;
                column.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (column.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
                column.ExportWidth = 100;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Employee";
                column.FieldName = "Employee";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ExportWidth = 200;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "State";
                column.FieldName = "STATE_NAME";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ExportWidth = 140;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "District";
                column.FieldName = "DISTRICT";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ExportWidth = 140;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Supervisor";
                column.Caption = "Supervisor";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ExportWidth = 180;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Designation";
                column.FieldName = "Designation";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ExportWidth = 180;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "DEPARTMENT";
                column.Caption = "Department";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ExportWidth = 150;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Customer Name";
                column.FieldName = "CustomerName";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ExportWidth = 180;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Party Type";
                column.FieldName = "Party_Type";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ExportWidth = 180;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Type";
                column.FieldName = "Type";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ExportWidth = 180;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Entity Type";
                column.FieldName = "Entity_Type";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ExportWidth = 150;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Customer Address";
                column.FieldName = "CustomerAddress";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ExportWidth = 300;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "ContactNo";
                column.Caption = "Contact No";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ExportWidth = 100;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Assoc. Customer";
                column.FieldName = "Assoc_Customer";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ExportWidth = 180;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Mail Id";
                column.FieldName = "MailId";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ExportWidth = 200;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Model";
                column.Caption = "Model";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ExportWidth = 150;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Primary Application";
                column.FieldName = "PrimaryApplication";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ExportWidth = 150;

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Secondary Application";
                column.FieldName = "SecondaryApplication";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ExportWidth = 150;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "BookingAmount";
                column.Caption = "Booking Amount";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ExportWidth = 130;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Lead Type";
                column.FieldName = "LeadType";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ExportWidth = 150;
            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "Stage";
                column.Caption = "Stage";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ExportWidth = 150;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Funnel Stage";
                column.FieldName = "FunnelStage";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ExportWidth = 150;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Feedback";
                column.FieldName = "Feedback";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ExportWidth = 150;
            });


            settings.Columns.Add(column =>
            {
                column.Caption = "DD Type";
                column.FieldName = "DD_Type";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ExportWidth = 150;

            });


            settings.Columns.Add(column =>
            {
                column.Caption = "Shop Type";
                column.FieldName = "Shop_Type";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ExportWidth = 150;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Party Status";
                column.FieldName = "Party_Status";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ExportWidth = 150;
            });
            settings.Columns.Add(column =>
            {
                column.Caption = "Group/Beat";
                column.FieldName = "Group_Beat";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ExportWidth = 150;
            });
            

            settings.Columns.Add(column =>
            {
                column.Caption = "Account Holder";
                column.FieldName = "Account_Holder";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ExportWidth = 150;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Bank Name";
                column.FieldName = "Bank_Name";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ExportWidth = 150;
            });


            settings.Columns.Add(column =>
            {
                column.Caption = "Account No";
                column.FieldName = "Account_No";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ExportWidth = 150;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "IFSC Code";
                column.FieldName = "IFSC_Code";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ExportWidth = 150;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "UPI ID";
                column.FieldName = "UPI_ID";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ExportWidth = 150;
            });

            //Rev 1.0
            settings.Columns.Add(column =>
            {
                column.FieldName = "LASTVISITDATE";
                column.Caption = "Last Visit Date";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ExportWidth = 120;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "LASTVISITTIME";
                column.Caption = "Last Visit Time";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ExportWidth = 120;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "LASTVISITEDBY";
                column.Caption = "Last Visited By";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.ExportWidth = 150;
            });
            //Rev 1.0 End
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }
	}
}
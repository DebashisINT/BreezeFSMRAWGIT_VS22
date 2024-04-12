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
    public class LeaveApplyListController : Controller
    {
        LeaveDetailsListBL obj = new LeaveDetailsListBL();
        public ActionResult LeaveListIndex()
        {
            try
            {
                LeaveDetailsModel omodel = new LeaveDetailsModel();
                string userid = Session["userid"].ToString();
                omodel.selectedusrid = userid;
                omodel.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                omodel.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                return View(omodel);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
        
        public ActionResult LeaveDetailslistGrid(string Is_PageLoad)
        {
            return PartialView(GetDataDetails(Is_PageLoad));
        }
        public ActionResult LeaveDetailslistGridDB(string Is_PageLoad)
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
                var q = from d in dc.FTSLEAVELIST_REPORTs
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSLEAVELIST_REPORTs
                        where d.USERID == Convert.ToInt32(Userid) && d.LEAVE_USERID == 0
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
        }

        public ActionResult GetDoctorDetailsList(LeaveDetailsModel model)
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

                //string state = "";
                //int i = 1;
                //if (model.StateId != null && model.StateId.Count > 0)
                //{
                //    foreach (string item in model.StateId)
                //    {
                //        if (i > 1)
                //            state = state + "," + item;
                //        else
                //            state = item;
                //        i++;
                //    }
                //}

                //string empcode = "";
                //int k = 1;
                //if (model.empcode != null && model.empcode.Count > 0)
                //{
                //    foreach (string item in model.empcode)
                //    {
                //        if (k > 1)
                //            empcode = empcode + "," + item;
                //        else
                //            empcode = item;
                //        k++;
                //    }
                //}


                double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;
                if (days <= 30)
                {
                    dt = obj.GetReportLeaveDetails(datfrmat, dattoat, Userid);
                }

                return Json(Userid, JsonRequestBehavior.AllowGet);
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
            settings.Name = "gridLeaveDetails";
            //settings.CallbackRouteValues = new { Controller = "InvoiceDeliveryRegister", Action = "GetRegisterreporttatusList" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Leave Details Report";

            settings.Columns.Add(x =>
            {
                x.FieldName = "CREATED_DATE";
                x.Caption = "Apply Date";
                x.VisibleIndex = 1;
                x.ExportWidth = 90;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "EMP_NAME";
                x.Caption = "Applied By";
                x.VisibleIndex = 2;
                x.ExportWidth = 150;
            });

            //Rev Debashis 0024681
            settings.Columns.Add(x =>
            {
                x.FieldName = "EMPID";
                x.Caption = "Employee Code";
                x.VisibleIndex = 3;
                x.ExportWidth = 200;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "EMPDESG";
                x.Caption = "Designation";
                x.VisibleIndex = 4;
                x.ExportWidth = 180;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CONTACTNO";
                x.Caption = "Contact Number";
                x.VisibleIndex = 5;
                x.ExportWidth = 130;
            });
            //End of Rev Debashis 0024681

            settings.Columns.Add(x =>
            {
                x.FieldName = "LEAVE_START_DATE";
                x.Caption = "From Date";
                x.VisibleIndex = 6;
                x.ExportWidth = 90;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
            });


            settings.Columns.Add(x =>
            {
                x.FieldName = "LEAVE_END_DATE";
                x.Caption = "To Date";
                x.VisibleIndex = 7;
                x.ExportWidth = 90;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";

            });

            //Rev Debashis 0024681
            settings.Columns.Add(x =>
            {
                x.FieldName = "APPLIED_TIME";
                x.Caption = "Applied time";
                x.VisibleIndex = 8;
                x.ExportWidth = 110;
            });
            //End of Rev Debashis 0024681

            settings.Columns.Add(x =>
            {
                x.FieldName = "LeaveType";
                x.Caption = "Leave Type";
                x.VisibleIndex = 9;
                x.ExportWidth = 120;

            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "LEAVE_REASON";
                x.Caption = "Remarks";
                x.VisibleIndex = 10;
                x.ExportWidth = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CURRENT_STATUS";
                x.Caption = "Status";
                x.VisibleIndex = 11;
                x.ExportWidth = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "APPROVE_EMP_NAME";
                x.Caption = "Approve/Reject By";
                x.VisibleIndex = 12;
                x.ExportWidth = 200;
            });

            //Rev Debashis 0024681
            settings.Columns.Add(x =>
            {
                x.FieldName = "AEMPID";
                x.Caption = "Employee Code";
                x.VisibleIndex = 13;
                x.ExportWidth = 200;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "AEMPDESG";
                x.Caption = "Designation";
                x.VisibleIndex = 14;
                x.ExportWidth = 150;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ACONTACTNO";
                x.Caption = "Contact Number";
                x.VisibleIndex = 15;
                x.ExportWidth = 150;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "APPROVEREJECT_DATE";
                x.Caption = "Approve/Reject date";
                x.VisibleIndex = 16;
                x.ExportWidth = 180;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "APPROVEREJECT_TIME";
                x.Caption = "Approve/Reject time";
                x.VisibleIndex = 17;
                x.ExportWidth = 180;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "APPROVER_REMARKS";
                x.Caption = "Approver Remarks";
                x.VisibleIndex = 18;
                x.ExportWidth = 180;
            });
            //End of Rev Debashis 0024681

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }
	}
}
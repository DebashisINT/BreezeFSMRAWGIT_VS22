/******************************************************************************************************
 * Created by Sanchita for V2.0.40 on 04-05-2023. Work done in Controller, View and Model
 * A New Expense Report is Required for BP Poddar. Refer: 25833
 * Rev 1.0      Sanchita    V2.0.40     19/05/2023      ‘Other Allowance’ Coloumn name should be renamed as ‘Travelling Allowance’ 
 *                                                      when IsShowReimbursementTypeInAttendance=1. Refer: 26146
 * Rev 2.0      Sanchita    V2.0.40     22-05-2023      Need to implement Branch and Area in the Expense Register Report. Refer: 26185                                                      
 * *******************************************************************************************************/
using BusinessLogicLayer.SalesmanTrack;
using DataAccessLayer;
using DevExpress.Web.Mvc;
using DevExpress.Web;
using MyShop.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilityLayer;
using static MyShop.Models.ExpenseReport;
using DevExpress.Data.Mask;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Wordprocessing;
using Models;
using EntityLayer.MailingSystem;
using System.IO;
using System.IO.Compression;
using System.Text;
using DocumentFormat.OpenXml.Vml.Office;
using System.Web.WebPages;
using Ionic.Zip;
using BusinessLogicLayer;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class ExpenseReportController : Controller
    {
        // Rev 1.0
        CommonBL objSystemSettings = new CommonBL();
        // End of Rev 1.0

        // GET: MYSHOP/ExpenseReport
        public ActionResult Index()
        {
            ExpenseReport omodel = new ExpenseReport();

            var today = DateTime.Today;
            var month = new DateTime(today.Year, today.Month, 1);
            var first = month.AddMonths(-1);
            var last = month.AddDays(-1);

            omodel.FromDate = first.ToString("dd-MM-yyyy");
            omodel.ToDate = last.ToString("dd-MM-yyyy");
            
            omodel.dtFromDate = first;
            omodel.dtToDate = last;


            return View(omodel);
        }

        public ActionResult GetHQNameList()
        {
            try
            {
                DataTable dtHQName = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_FTSExpenseReport");
                proc.AddPara("@ACTION", "GetHQName");
                dtHQName = proc.GetTable();

                List<GetHQName> modelHQ = new List<GetHQName>();
                modelHQ = APIHelperMethods.ToModelList<GetHQName>(dtHQName);

                return PartialView("~/Areas/MYSHOP/Views/ExpenseReport/_HQNamePartial.cshtml", modelHQ);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetExpenseTypeList()
        {
            try
            {
                DataTable dtExp = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_FTSExpenseReport");
                proc.AddPara("@ACTION", "GetExpenseType");
                dtExp = proc.GetTable();

                List<GetExpenseType> modelExp = new List<GetExpenseType>();
                modelExp = APIHelperMethods.ToModelList<GetExpenseType>(dtExp);

                return PartialView("~/Areas/MYSHOP/Views/ExpenseReport/_ExpenseTypePartial.cshtml", modelExp);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public PartialViewResult _PartialExpenseReport(string ispageload)
        {
            //if (StartDate != null)
            //{
            //    ViewBag.StartDate = StartDate.ToString("hh:mm tt");
            //    TempData["StartDate"] = StartDate.ToString("hh:mm tt");
            //}
            //if (EndDate != null)
            //{
            //    ViewBag.EndDate = EndDate.ToString("hh:mm tt");
            //    TempData["EndDate"] = EndDate.ToString("hh:mm tt");
            //}

            //TempData.Keep();
            // Rev 1.0
            string IsShowReimbursementTypeInAttendance = objSystemSettings.GetSystemSettingsResult("IsShowReimbursementTypeInAttendance");
            ViewBag.IsShowReimbursementTypeInAttendance = IsShowReimbursementTypeInAttendance;
            // End of Rev 1.0
            return PartialView(GetReport(ispageload));
        }

        public IEnumerable GetReport(string ispageload)
        {

            string connectionString = Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]);
            string Userid = Convert.ToString(Session["userid"]);

            if (ispageload != "1")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.EXPENSE_REPORTs
                        where Convert.ToString(d.USER_ID) == Userid
                        orderby d.REIMBURSEMENT_DATE descending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.EXPENSE_REPORTs
                        where 1 == 0
                        orderby d.REIMBURSEMENT_DATE descending
                        select d;
                return q;
            }



        }

        public JsonResult GetExpenseLINQTable(string emp, string hqid, string expid, DateTime fromdate, DateTime todate)
        {
            string output = "";
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSExpenseReport");
            proc.AddPara("@ACTION", "LIST");
            proc.AddPara("@fromdate", fromdate);
            proc.AddPara("@todate", todate);
            proc.AddPara("@HQid_list", hqid);
            proc.AddPara("@expid_list", expid);
            proc.AddPara("@employee_list", emp);
            proc.AddPara("@USER_ID", Convert.ToString(Session["userid"]));
            
            proc.GetScalar();
            return Json(output, JsonRequestBehavior.AllowGet);

        }



        public ActionResult ExporSummaryList(int type)
        {


            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(), GetReport(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(), GetReport(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(), GetReport(""));
                case 4:
                    return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(), GetReport(""));
                case 5:
                    return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(), GetReport(""));
                //break;

                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetEmployeeBatchGridViewSettings()
        {
            // Rev 1.0
            string IsShowReimbursementTypeInAttendance = objSystemSettings.GetSystemSettingsResult("IsShowReimbursementTypeInAttendance");
            // End of Rev 1.0

            var settings = new GridViewSettings();
            settings.Name = "Employee Summary Report";
            settings.CallbackRouteValues = new { Action = "_PartialLateVisitGrid", Controller = "ShopVisitRegister" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Employee Late Visit Report";

            settings.Columns.Add(x =>
            {
                x.FieldName = "SL";
                x.Caption = "SL";
                // x.VisibleIndex = 1;
                x.Width = 0;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "REIMBURSEMENT_DATE";
                x.Caption = "Date of Reimbursement";
                x.VisibleIndex = 1;
                x.Width = 100;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                (x.PropertiesEdit as DateEditProperties).DisplayFormatString = "dd-MM-yyyy";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "HQ_NAME";
                x.Caption = "HQ Name";
                x.VisibleIndex = 2;
                x.Width = 100;
            });

            // Rev 2.0
            if (IsShowReimbursementTypeInAttendance == "1")
            {
                settings.Columns.Add(x =>
                {
                    x.FieldName = "BRANCHNAME";
                    x.Caption = "Branch";
                    x.VisibleIndex = 3;
                    x.Width = 100;
                });
                settings.Columns.Add(x =>
                {
                    x.FieldName = "AREANAME";
                    x.Caption = "Area";
                    x.VisibleIndex = 4;
                    x.Width = 100;
                });
            }
            // End of Rev 2.0

            settings.Columns.Add(x =>
            {
                x.FieldName = "EMP_NAME";
                x.Caption = "Employee Name";
                x.VisibleIndex = 5;
                x.Width = 200;
            });

            //Rev Debashis 0025198
            settings.Columns.Add(x =>
            {
                x.FieldName = "EMPID";
                x.Caption = "Employee ID";
                x.VisibleIndex = 6;
                x.Width = 200;
            });
            //End of Rev Debashis 0025198

            settings.Columns.Add(x =>
            {
                x.FieldName = "EMP_DESIGNATION";
                x.Caption = "Designation";
                x.VisibleIndex = 7;
                x.Width = 200;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "REPORTTO_NAME";
                x.Caption = "Reporting Manager";
                x.VisibleIndex = 8;
                x.Width = 200;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "EXPENSE_TYPE";
                x.Caption = "Expense Type";
                x.VisibleIndex = 9;
                x.Width = 200;
                x.ColumnType = MVCxGridViewColumnType.TextBox;
            });

            // Rev 1.0
            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "OTHER_ALLOWANCE";
            //    x.Caption = "Other Allowance";
            //    x.VisibleIndex = 8;
            //    x.Width = 100;
            //    x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            //    x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            //    x.PropertiesEdit.DisplayFormatString = "0.00";
            //});

            if (IsShowReimbursementTypeInAttendance == "1") {
                settings.Columns.Add(x =>
                {
                    x.FieldName = "OTHER_ALLOWANCE";
                    x.Caption = "Travelling Allowance";
                    x.VisibleIndex = 10;
                    x.Width = 100;
                    x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                    x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                    x.PropertiesEdit.DisplayFormatString = "0.00";
                });
            }
            else
            {
                settings.Columns.Add(x =>
                {
                    x.FieldName = "OTHER_ALLOWANCE";
                    x.Caption = "Other Allowance";
                    x.VisibleIndex = 11;
                    x.Width = 100;
                    x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                    x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                    x.PropertiesEdit.DisplayFormatString = "0.00";
                });
            }
            // End of Rev 1.0
            

            settings.Columns.Add(x =>
            {
                x.FieldName = "DAILY_ALLOWANCE";
                x.Caption = "Daily Allowance";
                x.VisibleIndex = 12;
                x.Width = 100;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "TOTAL_ALLOWANCE";
                x.Caption = "Total Allowance";
                x.VisibleIndex = 13;
                x.Width = 100;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.PropertiesEdit.DisplayFormatString = "0.00";
            });

            

            ///Summary
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "OTHER_ALLOWANCE").DisplayFormat = "0.00";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "DAILY_ALLOWANCE").DisplayFormat = "0.00";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "TOTAL_ALLOWANCE").DisplayFormat = "0.00";

            TempData.Keep();


            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

        public ActionResult LoadImageDocument(string REIMBURSEMENT_DATE, string EMPID)
        {
            //string weburl = System.Web.Hosting.HostingEnvironment.MapPath("~/APP/CommonFolder/Reimbursement/");
            string weburl =  Server.MapPath("~/CommonFolder/Reimbursement/");

            //weburl = weburl.Replace("MyShopProject", "APP");
            weburl = weburl.Replace("PORTAL", "APP");

            List<ReimbursementApplicationbills> list = new List<ReimbursementApplicationbills>();

            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSExpenseReport");
            proc.AddPara("@ACTION", "LOADIMAGE");
            proc.AddPara("@REIMBURSEMENT_DATE", REIMBURSEMENT_DATE);
            proc.AddPara("@EMPID", EMPID);
            dt = proc.GetTable();


            if (dt != null && dt.Rows.Count > 0)
            {
                //string dir = Server.MapPath("~/CommonFolder/Reimbursement/Reimbursement_Attachment");
                string dir = weburl + "Reimbursement_Attachment";
                // If directory does not exist, create it
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                foreach (DataRow row in dt.Rows)
                {
                    string FileName = Convert.ToString(row["Bills"]);

                    //var source = Server.MapPath("~/CommonFolder/Reimbursement/" + FileName + "");
                    //var destination = Server.MapPath("~/CommonFolder/Reimbursement/Reimbursement_Attachment/" + FileName + "");
                    var source = weburl + FileName ;
                    var destination = weburl + "Reimbursement_Attachment/" + FileName ;

                    //Do your job with "file"  
                    if (!System.IO.File.Exists(destination))
                    {
                        System.IO.File.Copy(source, destination);
                    }
                }


                using (var zip = new Ionic.Zip.ZipFile())
                {
                    //zip.AddFiles(Directory.GetFiles(Server.MapPath("~/CommonFolder/Reimbursement/Reimbursement_Attachment")), "Reimbursement");
                    //zip.Save(Server.MapPath("~/CommonFolder/Reimbursement/Reimbursement_Attachment.zip"));
                    
                    zip.AddFiles(Directory.GetFiles(weburl + "Reimbursement_Attachment"), "Reimbursement");
                    zip.Save(weburl + "Reimbursement_Attachment.zip");
                }


                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                response.ClearHeaders();
                response.ClearContent();
                response.Buffer = true;
                response.Clear();
                response.ContentType = "image/jpeg";
                response.AddHeader("Content-Disposition", "attachment; filename=Reimbursement_Attachment.zip;");
                response.TransmitFile(weburl + "Reimbursement_Attachment.zip");
                response.Flush();
                response.End();

                if (Directory.Exists(dir))
                {
                    System.IO.Directory.Delete(dir, true);
                }

                string zippath = weburl + "Reimbursement_Attachment.zip";
                if (System.IO.File.Exists(zippath))
                {
                    System.IO.File.Delete(zippath);
                }
            }

            return null;
        }
    }
}
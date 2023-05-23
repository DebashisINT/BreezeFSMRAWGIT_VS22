//====================================================== Revision History ==========================================================
//1.0  18-05-2023    2.0.40    Priti     0026136: Modification in CURRENT STOCK REGISTER report
//====================================================== Revision History ==========================================================

using BusinessLogicLayer;
using BusinessLogicLayer.SalesmanTrack;
using DataAccessLayer;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using Models;
using MyShop.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class CurrentStockReportController : Controller
    {
        CommonBL objSystemSettings = new CommonBL();
        CurrentStockReportBL obj = new CurrentStockReportBL();
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
        public ActionResult GenerateTable(CurrentStockReportModel model)
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
                string shopIds = "";
                k = 1;
                if (model.shopId != null && model.shopId.Count > 0)
                {
                    foreach (string item in model.shopId)
                    {
                        if (k > 1)
                            shopIds = shopIds + "," + item;
                        else
                            shopIds = item;
                        k++;
                    }
                }

                double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;
                if (days <= 30)
                {
                    dt = obj.GetReportCurrentStock(datfrmat, dattoat, Userid, state, empcode, shopIds);
                }
                return Json(Userid, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public IEnumerable GetReport(string ispageload)
        {
            //Rev 1.0
            string IsAttachmentAvailableForCurrentStock = objSystemSettings.GetSystemSettingsResult("IsAttachmentAvailableForCurrentStock");//REV 1.0
            ViewBag.IsAttachmentAvailableForCurrentStock = IsAttachmentAvailableForCurrentStock;
            //Rev 1.0 End
            DataTable dtColmn = obj.GetPageRetention(Session["userid"].ToString(), "Current Stock Register");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            }

            string connectionString = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"]);
            string Userid = Convert.ToString(Session["userid"]);

            if (ispageload != "1")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSCURRENTSTOCK_REPORTs
                        where d.USERID == Convert.ToInt32(Userid)
                        //orderby d.EMPLOYEE ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSCURRENTSTOCK_REPORTs
                        where d.USERID == 0
                        select d;
                return q;
            }
        }

        public ActionResult ExportMeetingDetailsReport(int type, string IsPageload)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToXlsx(GetGridViewSettings(), GetReport(IsPageload));
                //break;
                case 2:
                    return GridViewExtension.ExportToXls(GetGridViewSettings(), GetReport(IsPageload));
                //break;
                case 3:
                    return GridViewExtension.ExportToPdf(GetGridViewSettings(), GetReport(IsPageload));
                case 4:
                    return GridViewExtension.ExportToCsv(GetGridViewSettings(), GetReport(IsPageload));
                case 5:
                    return GridViewExtension.ExportToRtf(GetGridViewSettings(), GetReport(IsPageload));
                //break;

                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetGridViewSettings()
        {
            DataTable dtColmn = obj.GetPageRetention(Session["userid"].ToString(), "Current Stock Register");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            }

            var settings = new GridViewSettings();
            settings.Name = "CurrentStockRegister";
            settings.CallbackRouteValues = new { Controller = "CurrentStockReport", Action = "GenerateTable" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "CurrentStockRegister";

            settings.Columns.Add(x =>
            {
                x.FieldName = "Employee_ID";
                x.Caption = "Employee ID";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Employee_ID'");
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
                x.FieldName = "SHOPNAME";
                x.Caption = "Shop Name";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOPNAME'");
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
                x.FieldName = "ENTITYCODE";
                x.Caption = "Code";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ENTITYCODE'");
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
                x.FieldName = "ADDRESS";
                x.Caption = "Address";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ADDRESS'");
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
                x.FieldName = "CONTACT";
                x.Caption = "Contact";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACT'");
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
                x.FieldName = "SHOPTYPE";
                x.Caption = "Shop type";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOPTYPE'");
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
                x.FieldName = "PPName";
                x.Caption = "PP Name";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PPName'");
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
                x.FieldName = "DDName";
                x.Caption = "DD Name";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DDName'");
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
                x.FieldName = "CURRENTSTKDATE";
                x.Caption = "Current Stock Date";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CURRENTSTKDATE'");
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
                x.FieldName = "CURRENTSTKNO";
                x.Caption = "Current Stock Number";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CURRENTSTKNO'");
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
                x.FieldName = "PRODUCT";
                x.Caption = "Product";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PRODUCT'");
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
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.PropertiesEdit.DisplayFormatString = "0.00";
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
                    }
                }
                else
                {
                    x.Visible = true;
                }
            });

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
                int k = obj.InsertPageRetention(Col, Session["userid"].ToString(), "Current Stock Register");

                return Json(k, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        //Rev 1.0
        public ActionResult LoadImageDocument(string CURRENTSTKID)
        {           
            string weburl = Server.MapPath("~/CommonFolder/CurrentStockImage/");           
            weburl = weburl.Replace("PORTAL", "APP");
            List<ReimbursementApplicationbills> list = new List<ReimbursementApplicationbills>();

            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSCURRENTSTOCK_REPORT");
            proc.AddPara("@ACTION", "LOADIMAGE");           
            proc.AddPara("@CURRENTSTKID", CURRENTSTKID);
            dt = proc.GetTable();


            if (dt != null && dt.Rows.Count > 0)
            {                
                string dir = weburl + "CURRENTSTOCK_Attachment";
                // If directory does not exist, create it
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }


                foreach (DataRow row in dt.Rows)
                {
                    string _FileName1 = Convert.ToString(row["STOCKIMAGEPATH1"]);
                    if (_FileName1 != "")
                    {

                        string filename1 = Path.GetFileName(_FileName1);
                        var source = weburl + filename1;
                        var destination = weburl + "CURRENTSTOCK_Attachment/" + filename1;

                        //Do your job with "file"  
                        if (!System.IO.File.Exists(destination))
                        {
                            System.IO.File.Copy(source, destination);
                        }
                    }


                    string _FileName2 = Convert.ToString(row["STOCKIMAGEPATH2"]);
                    if (_FileName2 != "")
                    {
                        string filename2 = Path.GetFileName(_FileName2);
                        var source1 = weburl + filename2;
                        var destination1 = weburl + "CURRENTSTOCK_Attachment/" + filename2;

                        //Do your job with "file"  
                        if (!System.IO.File.Exists(destination1))
                        {
                            System.IO.File.Copy(source1, destination1);
                        }
                    }
                }


                using (var zip = new Ionic.Zip.ZipFile())
                {                  

                    zip.AddFiles(Directory.GetFiles(weburl + "CURRENTSTOCK_Attachment"), "CurrentStockImage");
                    zip.Save(weburl + "CURRENTSTOCK_Attachment.zip");
                }


                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                response.ClearHeaders();
                response.ClearContent();
                response.Buffer = true;
                response.Clear();
                response.ContentType = "image/jpeg";
                response.AddHeader("Content-Disposition", "attachment; filename=Reimbursement_Attachment.zip;");
                response.TransmitFile(weburl + "CURRENTSTOCK_Attachment.zip");
                response.Flush();
                response.End();

                if (Directory.Exists(dir))
                {
                    System.IO.Directory.Delete(dir, true);
                }

                string zippath = weburl + "CURRENTSTOCK_Attachment.zip";
                if (System.IO.File.Exists(zippath))
                {
                    System.IO.File.Delete(zippath);
                }
            }

            return null;
        }
        //Rev 1.0 End
    }
}
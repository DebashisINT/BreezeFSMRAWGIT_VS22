/******************************************************************************************************
 * Created by Priti for V2.0.44 on 19/12/2023. Work done in Controller, View and Model
 * A new report is required as Outlet wise Call Logging Details Report (Customisation). Refer: 0027064                                                      
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
using System.Configuration;
using BusinessLogicLayer.SalesTrackerReports;
using System.Runtime.CompilerServices;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class OutletWiseCallLoggingController : Controller
    {
        
        CommonBL objSystemSettings = new CommonBL();
        OutletWiseCallLogging_BL objBL = new OutletWiseCallLogging_BL();

        
        public ActionResult Report()
        {
            OutletWiseCallLogging omodel = new OutletWiseCallLogging();
            string userid = Session["userid"].ToString();
            omodel.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
            omodel.Todate = DateTime.Now.ToString("dd-MM-yyyy");
            DataTable dt = objBL.GetStateMandatory(Session["userid"].ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewBag.StateMandatory = dt.Rows[0]["IsStateMandatoryinReport"].ToString();
            }

            return View(omodel);


        }

        public ActionResult PartialGetOutletWiseCallLog(OutletWiseCallLogging model)
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
                
                dt = objBL.GetOutletWiseCallLogReport(datfrmat, dattoat, Userid, state, desig, empcode);
               
                return PartialView("PartialGetOutletWiseCallLog", GetOutletWiseCallLogDetails(frmdate));

            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }
        public IEnumerable GetOutletWiseCallLogDetails(string frmdate)
        {
            DataTable dtColmn = objBL.GetPageRetention(Session["userid"].ToString(), "Outlet wise call log");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            }

            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);

            if (frmdate != "Ispageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.OUTLETWISECALLLOGGING_REPORTs
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.OUTLETWISECALLLOGGING_REPORTs
                        where d.USERID == Convert.ToInt32(Userid) && d.EMPID == "0"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
        }

        public ActionResult ExporPerformanceAnalyList(int type)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(), GetOutletWiseCallLogDetails(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(), GetOutletWiseCallLogDetails(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(), GetOutletWiseCallLogDetails(""));
                case 4:
                    return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(), GetOutletWiseCallLogDetails(""));
                case 5:
                    return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(), GetOutletWiseCallLogDetails(""));
                //break;

                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetEmployeeBatchGridViewSettings()
        {
            DataTable dtColmn = objBL.GetPageRetention(Session["userid"].ToString(), "Outlet wise call log");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            }
            var settings = new GridViewSettings();
            settings.Name = "Outlet wise call log";
            settings.CallbackRouteValues = new { Controller = "OutletWiseCallLogging", Action = "PartialGetOutletWiseCallLog" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "OutletWiseCallLogging";

            //if (TempData["IsRevisitContactDetails"].ToString() == "0")
            //{
                settings.Columns.Add(x =>
                {
                    x.FieldName = "SEQ";
                    x.Caption = "Serial";
                    x.VisibleIndex = 1;
                    x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                    x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SEQ'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 50;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 50;
                    }
                });
            
                 settings.Columns.Add(x =>
                 {
                     x.FieldName = "CALLDATE";
                     x.Caption = "DATE";
                     x.VisibleIndex = 2;
                     if (ViewBag.RetentionColumn != null)
                     {
                         System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DATE'");
                         if (row != null && row.Length > 0)  /// Check now
                         {
                             x.Visible = false;
                         }
                         else
                         {
                             x.Visible = true;
                             x.ExportWidth = 100;
                         }
                     }
                     else
                     {
                         x.Visible = true;
                         x.ExportWidth = 100;
                     }
                 });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "EMPID";
                    x.Caption = "Emp. ID";
                    x.VisibleIndex = 3;
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
                            x.ExportWidth = 180;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 180;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "EMPName";
                    x.Caption = "Emp. Name";
                    x.VisibleIndex = 4;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='EMPName'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 150;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 150;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "Category";
                    x.Caption = "Category";
                    x.VisibleIndex = 5;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Category'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 120;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 120;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "ASMReporting";
                    x.Caption = "ASM Reporting";
                    x.VisibleIndex = 6;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ASMReporting'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 120;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 120;
                    }
                });

               

                settings.Columns.Add(x =>
                {
                    x.FieldName = "VisitType";
                    x.Caption = "Visit Type";
                    x.VisibleIndex = 7;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='VisitType'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "VisitRevisitTime";
                    x.Caption = "Visit/Revisit Time";
                    x.VisibleIndex = 8;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='VisitRevisitTime'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "MDDName";
                    x.Caption = "MDD Name";
                    x.VisibleIndex = 9;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='MDDName'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 120;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 120;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "MDDCode";
                    x.Caption = "MDD Code";
                    x.VisibleIndex = 10;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='MDDCode'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 120;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 120;
                    }

                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "OutletName";
                    x.Caption = "Outlet Name";
                    x.VisibleIndex = 11;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='OutletName'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 120;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 120;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "OutletCode";
                    x.Caption = "Outlet Code";
                    x.VisibleIndex = 12;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='OutletCode'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "MobileNo";
                    x.Caption = "Mobile No";
                    x.VisibleIndex = 13;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='MobileNo'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "OwnerName";
                    x.Caption = "Owner Name";
                    x.VisibleIndex = 14;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='OwnerName'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 120;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 120;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "CALL_DATE";
                    x.Caption = "Call Date";
                    x.VisibleIndex = 15;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CALL_DATE'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "CALL_TIME";
                    x.Caption = "Call Time";
                    x.VisibleIndex = 16;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CALL_TIME'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "CALL_DURATION";
                    x.Caption = "Call Duration";
                    x.VisibleIndex = 17;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CALL_DURATION'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 100;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 100;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "CalLCount";
                    x.Caption = "Call Count";
                    x.VisibleIndex = 18;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CalLCount'");
                        if (row != null && row.Length > 0)  /// Check now
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.ExportWidth = 80;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 80;
                    }
                });

               

                
          //  }
            
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
                int k = objBL.InsertPageRetention(Col, Session["userid"].ToString(), "PERFORMANCE ANALYTICS");

                return Json(k, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
    }
}
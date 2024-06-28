//====================================================== Revision History ===================================================
//Rev Number DATE              VERSION          DEVELOPER           CHANGES*@
//Written by Sanchita on 23-11-2023 for V2.2.43    A new report required as Enquiry Analytics.Mantis: 27021 *@
// Rev 1.0    Sanchita   30-04-2024      V2.2.43    0027410: ENQUIRY ANALYTICS REPORT Column chooser not Working # Local
//====================================================== Revision History ===================================================

using BusinessLogicLayer;
using BusinessLogicLayer.SalesmanTrack;
using DataAccessLayer;
using DevExpress.Utils;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Models;
using MyShop.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml;
using UtilityLayer;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class EnquiryAnalyticsController : Controller
    {
        NotificationBL notificationbl = new NotificationBL();
        DBEngine odbengine = new DBEngine();

        // GET: MYSHOP/EnquiryAnalytics
        public ActionResult Index()
        {
            EnquiryAnalyticsModel Dtls = new EnquiryAnalyticsModel();

            Dtls.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
            Dtls.Todate = DateTime.Now.ToString("dd-MM-yyyy");
            Dtls.Is_PageLoad = "Ispageload";

            EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/EnquiryAnalytics/Index");
            ViewBag.CanView = rights.CanView;
            ViewBag.CanExport = rights.CanExport;
            
            return View(Dtls);
        }

        public ActionResult PartialEnquiryAnalyticsGridList(EnquiryAnalyticsModel model)
        {
            try
            {
                if (model.Is_PageLoad == "PendingEnquiry" || model.Is_PageLoad == "InProcessEnquiry" || model.Is_PageLoad == "NotInterestedEnquiry"
                    || model.Is_PageLoad == "AssignedEnquiry" || model.Is_PageLoad == "ReassignedEnquiry" || model.Is_PageLoad == "HighRiskEnquiry")
                {
                    string Is_PageLoad = model.Is_PageLoad;

                    model.Is_PageLoad = "Ispageload";

                    return PartialView("PartialEnquiryAnalyticsGridList", GetEnquiryAnalyticsDetails(Is_PageLoad));
                }
                else
                {
                    EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/EnquiryAnalytics/Index");
                    ViewBag.CanView = rights.CanView;
                    ViewBag.CanExport = rights.CanExport;

                    string EnquiryFrom = "";
                    int i = 1;

                    if (model.EnquiryFromDesc != null && model.EnquiryFromDesc.Count > 0)
                    {
                        foreach (string item in model.EnquiryFromDesc)
                        {
                            if (i > 1)
                                EnquiryFrom = EnquiryFrom + "," + item;
                            else
                                EnquiryFrom = item;
                            i++;
                        }

                    }


                    string Is_PageLoad = string.Empty;

                    if (model.Is_PageLoad == "Ispageload")
                    {
                        Is_PageLoad = "is_pageload";

                    }

                    string datfrmat = model.Fromdate.Split('-')[2] + '-' + model.Fromdate.Split('-')[1] + '-' + model.Fromdate.Split('-')[0];
                    string dattoat = model.Todate.Split('-')[2] + '-' + model.Todate.Split('-')[1] + '-' + model.Todate.Split('-')[0];

                    GetEnquiryAnalyticsListing(EnquiryFrom, datfrmat, dattoat, Is_PageLoad);

                    model.Is_PageLoad = "Ispageload";

                    return PartialView("PartialEnquiryAnalyticsGridList", GetEnquiryAnalyticsDetails(Is_PageLoad));
                }
                

            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        public void GetEnquiryAnalyticsListing(string EnquiryFrom, string FromDate, string ToDate, string Is_PageLoad)
        {
            string user_id = Convert.ToString(Session["userid"]);
            
            string action = string.Empty;
            DataTable formula_dtls = new DataTable();
            DataSet dsInst = new DataSet();

            try
            {
                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_FTSEnquiryAnalyticsShow");
                proc.AddPara("@ACTION", "GETLISTINGDATA");
                proc.AddPara("@IS_PAGELOAD", Is_PageLoad);
                proc.AddPara("@USERID", Convert.ToInt32(user_id));
                proc.AddPara("@ENQUIRIESFROM", EnquiryFrom);
                proc.AddPara("@FROMDATE", FromDate);
                proc.AddPara("@TODATE", ToDate);
                dt = proc.GetTable();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable GetEnquiryAnalyticsDetails(string Is_PageLoad)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);
            
            DataTable dtColmn = GetPageRetention(Session["userid"].ToString(), "Enquiry Analytics");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            }
            
            if (Is_PageLoad != "is_pageload")
            {
                if(Is_PageLoad== "PendingEnquiry")
                {
                    ReportsDataContext dc = new ReportsDataContext(connectionString);
                    var q = from d in dc.ENQUIRYANALYTICS_LISTINGs
                            where d.USERID == Convert.ToInt32(Userid) && d.STATUS== "Pending"
                            orderby d.SEQ
                            select d;
                    return q;
                }
                else if(Is_PageLoad == "InProcessEnquiry")
                {
                    ReportsDataContext dc = new ReportsDataContext(connectionString);
                    var q = from d in dc.ENQUIRYANALYTICS_LISTINGs
                            where d.USERID == Convert.ToInt32(Userid) && d.ACTIVITY_STATUS == "In Process"
                            orderby d.SEQ
                            select d;
                    return q;
                }
                else if (Is_PageLoad == "NotInterestedEnquiry")
                {
                    ReportsDataContext dc = new ReportsDataContext(connectionString);
                    var q = from d in dc.ENQUIRYANALYTICS_LISTINGs
                            where d.USERID == Convert.ToInt32(Userid) && d.ACTIVITY_STATUS == "Not Interested"
                            orderby d.SEQ
                            select d;
                    return q;
                }
                else if (Is_PageLoad == "AssignedEnquiry")
                {
                    ReportsDataContext dc = new ReportsDataContext(connectionString);
                    var q = from d in dc.ENQUIRYANALYTICS_LISTINGs
                            where d.USERID == Convert.ToInt32(Userid) && d.STATUS == "Assigned"
                            orderby d.SEQ
                            select d;
                    return q;
                }
                else if (Is_PageLoad == "ReassignedEnquiry")
                {
                    ReportsDataContext dc = new ReportsDataContext(connectionString);
                    var q = from d in dc.ENQUIRYANALYTICS_LISTINGs
                            where d.USERID == Convert.ToInt32(Userid) && (d.ReASSIGNED_TO !=null  && d.ReASSIGNED_TO != "")
                            orderby d.SEQ
                            select d;
                    return q;
                }
                else if (Is_PageLoad == "HighRiskEnquiry")
                {
                    ReportsDataContext dc = new ReportsDataContext(connectionString);
                    var q = from d in dc.ENQUIRYANALYTICS_LISTINGs
                            where d.USERID == Convert.ToInt32(Userid) && (d.ACTIVITY_DATE == null || d.ACTIVITY_DATE == "")
                            orderby d.SEQ
                            select d;
                    return q;
                }
                else
                {
                    ReportsDataContext dc = new ReportsDataContext(connectionString);
                    var q = from d in dc.ENQUIRYANALYTICS_LISTINGs
                            where d.USERID == Convert.ToInt32(Userid)
                            orderby d.SEQ
                            select d;
                    return q;
                }
                
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.ENQUIRYANALYTICS_LISTINGs
                        where d.USERID == Convert.ToInt32(Userid) && d.SEQ == 11111111119
                        orderby d.SEQ
                        select d;
                return q;

                //gridEnquiryAnalytics.JSProperties["cpOpenCnt"] = (from d in dc.CRM_SalesOpportunities_Temps
                //               where Convert.ToString(d.USERID) == Userid && d.closed == true
                //               orderby d.ID
                //               select d.ID).Count();

                //TotalClosed = (from d in dc.CRM_SalesOpportunities_Temps
                //               where Convert.ToString(d.USERID) == Userid && d.closed == true
                //               orderby d.ID
                //               select d.ID).Count();

                //TotalClosed = (from d in dc.CRM_SalesOpportunities_Temps
                //               where Convert.ToString(d.USERID) == Userid && d.closed == true
                //               orderby d.ID
                //               select d.ID).Count();

                //TotalClosed = (from d in dc.CRM_SalesOpportunities_Temps
                //               where Convert.ToString(d.USERID) == Userid && d.closed == true
                //               orderby d.ID
                //               select d.ID).Count();

            }

        }

        // Rev 1.0
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
                int k = InsertPageRetention(Col, Session["userid"].ToString(), "Enquiry Analytics");

                return Json(k, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
        public int InsertPageRetention(string Col, String USER_ID, String ReportName)
        {
            ProcedureExecute proc = new ProcedureExecute("PRC_FTS_PageRetention");
            proc.AddPara("@Col", Col);
            proc.AddPara("@ReportName", ReportName);
            proc.AddPara("@USER_ID", USER_ID);
            proc.AddPara("@ACTION", "INSERT");
            int i = proc.RunActionQuery();
            return i;
        }
        // End of Rev 1.0
        public DataTable GetPageRetention(String USER_ID, String ReportName)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTS_PageRetention");
            proc.AddPara("@ReportName", ReportName);
            proc.AddPara("@USER_ID", USER_ID);
            proc.AddPara("@ACTION", "DETAILS");
            dt = proc.GetTable();
            return dt;
        }
        public ActionResult GetEnquiryFrom()
        {
            try
            {
                List<GetEnquiryFrom> modelEnquiryFrom = new List<GetEnquiryFrom>();

                DataTable dtEnquiryFrom = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_FTSEnquiryAnalyticsShow");
                proc.AddPara("@ACTION", "GetEnquiryFrom");
                dtEnquiryFrom = proc.GetTable();

                modelEnquiryFrom = APIHelperMethods.ToModelList<GetEnquiryFrom>(dtEnquiryFrom);

                return PartialView("~/Areas/MYSHOP/Views/CRMEnquiries/_EnquiryFromPartial.cshtml", modelEnquiryFrom);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
        public JsonResult GetEnquiryFromListSelectAll()
        {
            BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(string.Empty);
            try
            {
                List<GetEnquiryFrom> modelEnquiryFrom = new List<GetEnquiryFrom>();
                //DataTable dtEnquiryFrom = lstuser.GetHeadEnquiryFromList(Hoid, Hoid);
                //DataTable dtEnquiryFromChild = new DataTable();
                DataTable ComponentTable = new DataTable();
                ComponentTable = oDBEngine.GetDataTable("SELECT EnqID, EnquiryFromDesc from tbl_master_EnquiryFrom order by EnqID");
                modelEnquiryFrom = APIHelperMethods.ToModelList<GetEnquiryFrom>(ComponentTable);
                return Json(modelEnquiryFrom, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetEnquiriesCount(string start, string end, string EnquiryFromDesc)
        {


            EnquiryAnalyticsModel dtl = new EnquiryAnalyticsModel();
            try
            {
                DataSet ds = new DataSet();
                ProcedureExecute proc = new ProcedureExecute("PRC_FTSEnquiryAnalyticsShow");
                proc.AddPara("@Action", "GetEnquiriesCountData");
                proc.AddPara("@ToDate", DateTime.Now.ToString("yyyy-MM-dd"));
                proc.AddPara("@Fromdate", DateTime.Now.ToString("yyyy-MM-dd"));
                proc.AddPara("@userid", Convert.ToString(HttpContext.Session["userid"]));
                proc.AddPara("@ENQUIRIESFROM", EnquiryFromDesc);
                ds = proc.GetDataSet();


                int TotalPendingEnquiry = 0;
                int TotalInProcessEnquiry = 0;
                int TotalNotInterestedEnquiry = 0;
                int TotalAssignedEnquiry = 0;
                int TotalReassignedEnquiry = 0;
                int TotalHighRiskEnquiry = 0;

                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    TotalPendingEnquiry = Convert.ToInt32(item["cnt_PendingEnquiry"]); 
                    TotalInProcessEnquiry = Convert.ToInt32(item["cnt_InProcessEnquiry"]);
                    TotalNotInterestedEnquiry = Convert.ToInt32(item["cnt_NotInterestedEnquiry"]); 
                    TotalAssignedEnquiry = Convert.ToInt32(item["cnt_AssignedEnquiry"]); 
                    TotalReassignedEnquiry = Convert.ToInt32(item["cnt_ReassignedEnquiry"]); 
                    TotalHighRiskEnquiry = Convert.ToInt32(item["cnt_HighRiskEnquiry"]); 
                }
               
                dtl.TotalPendingEnquiry = TotalPendingEnquiry;
                dtl.TotalInProcessEnquiry = TotalInProcessEnquiry;
                dtl.TotalNotInterestedEnquiry= TotalNotInterestedEnquiry;
                dtl.TotalAssignedEnquiry= TotalAssignedEnquiry;
                dtl.TotalReassignedEnquiry= TotalReassignedEnquiry;
                dtl.TotalHighRiskEnquiry= TotalHighRiskEnquiry;
            }
            catch
            {
            }
            return Json(dtl);
        }

        public ActionResult ExporRegisterList(int type)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetDoctorBatchGridViewSettings(), GetEnquiryAnalyticsDetails(""));
                case 2:
                    return GridViewExtension.ExportToXlsx(GetDoctorBatchGridViewSettings(), GetEnquiryAnalyticsDetails(""));
                case 3:
                    return GridViewExtension.ExportToXls(GetDoctorBatchGridViewSettings(), GetEnquiryAnalyticsDetails(""));
                case 4:
                    return GridViewExtension.ExportToRtf(GetDoctorBatchGridViewSettings(), GetEnquiryAnalyticsDetails(""));
                case 5:
                    return GridViewExtension.ExportToCsv(GetDoctorBatchGridViewSettings(), GetEnquiryAnalyticsDetails(""));
                default:
                    break;
            }
            return null;
        }

        private GridViewSettings GetDoctorBatchGridViewSettings()
        {
            DataTable dtColmn = GetPageRetention(Session["userid"].ToString(), "Enquiry Analytics");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            }
            
            var settings = new GridViewSettings();
            settings.Name = "gridEnquiryAnalytics";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "EnquiryAnalytics";

             settings.Columns.Add(x =>
            {
                x.FieldName = "SEQ";
                x.Caption = "Sr. No";
                x.VisibleIndex = 1;
                x.Width = 80;
                
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
                        //x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    //x.Width = 100;
                }
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "Year";
                x.Caption = "Year";
                x.VisibleIndex = 2;
                x.Width = 60;
                
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Year'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        //x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    //x.Width = 100;
                }
                
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "Month";
                x.Caption = "Month";
                x.VisibleIndex = 3;
                x.Width = 60;
                
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Month'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        //x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    //x.Width = 100;
                }
                
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "Day";
                x.Caption = "Date";
                x.VisibleIndex = 4;
                x.Width = 110;
                
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Day'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        //x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    //x.Width = 100;
                }
                
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "VEND_TYPE";
                x.Caption = "Lead From";
                x.VisibleIndex = 5;
                x.Width = 110;
                
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='VEND_TYPE'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        //x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    //x.Width = 100;
                }
                
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "CUSTOMER_NAME";
                x.Caption = "Company Name";
                x.VisibleIndex = 6;
                x.Width = 110;
                
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CUSTOMER_NAME'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        //x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    //x.Width = 100;
                }
                
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "PHONENO";
                x.Caption = "Contact number";
                x.VisibleIndex = 7;
                x.Width = 80;
                
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PHONENO'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        //x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    //x.Width = 100;
                }
                
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "CONTACT_PERSON";
                x.Caption = "Contact Person";
                x.VisibleIndex = 8;
                x.Width = 96;

                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACT_PERSON'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        //x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    //x.Width = 100;
                }

            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "EMAIL";
                x.Caption = "Email Id";
                x.VisibleIndex = 9;
                x.Width = 100;
                
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='EMAIL'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        //x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    //x.Width = 100;
                }
                
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "State";
                x.Caption = "State";
                x.VisibleIndex = 10;
                x.Width = 100;
                
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='State'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        //x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    //x.Width = 100;
                }
                
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "Area";
                x.Caption = "Area";
                x.VisibleIndex = 11;
                x.Width = 100;
                
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Area'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        //x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    //x.Width = 100;
                }
                
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "ASSIGNED_TO";
                x.Caption = "HOD";
                x.VisibleIndex = 12;
                x.Width = 100;
                
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ASSIGNED_TO'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        //x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    //x.Width = 100;
                }
                

            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "ReASSIGNED_TO";
                x.Caption = "Pass to";
                x.VisibleIndex = 13;
                x.Width = 100;
                
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ReASSIGNED_TO'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        //x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    //x.Width = 100;
                }
                

            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "FEEDBACK";
                x.Caption = "Remark";
                x.VisibleIndex = 14;
                x.Width = 100;
                
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='FEEDBACK'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        //x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    //x.Width = 100;
                }
                
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "PRODUCT_REQUIRED";
                x.Caption = "Products Req.";
                x.VisibleIndex = 15;
                x.Width = 90;
                
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PRODUCT_REQUIRED'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        //x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    //x.Width = 100;
                }
                
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "QTY";
                x.Caption = "Qty.";
                x.VisibleIndex = 16;
                x.Width = 40;
                
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='QTY'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        //x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    //x.Width = 100;
                }
                
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "UOM";
                x.Caption = "UOM";
                x.VisibleIndex = 17;
                x.Width = 80;
                
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='UOM'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        //x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    //x.Width = 100;
                }
                
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ORDER_VALUE";
                x.Caption = "Order value";
                x.VisibleIndex = 18;
                x.Width = 80;
                // Rev 3.0
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.PropertiesEdit.DisplayFormatString = "0.00";
                // End of Rev 3.0
                
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ORDER_VALUE'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        //x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    //x.Width = 100;
                }
                
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ENQ_DETAILS";
                x.Caption = "Enquiry Details";
                x.VisibleIndex = 19;
                x.Width = 160;
                
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ENQ_DETAILS'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        //x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    //x.Width = 100;
                }
                
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "STATUS";
                x.Caption = "Status";
                x.VisibleIndex = 20;
                x.Width = 80;
                
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='STATUS'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        //x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    //x.Width = 100;
                }
                
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "SalesmanAssign_date";
                x.Caption = "Assign date";
                x.VisibleIndex = 21;
                x.Width = 80;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SalesmanAssign_date'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        //x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    //x.Width = 100;
                }

            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "ReASSIGNED_TO";
                x.Caption = "Reassign To";
                x.VisibleIndex = 22;
                x.Width = 80;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ReASSIGNED_TO'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        //x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    //x.Width = 100;
                }
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "ReASSIGNED_BY";
                x.Caption = "Reassign By";
                x.VisibleIndex = 23;
                x.Width = 80;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ReASSIGNED_BY'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        //x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    //x.Width = 100;
                }
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "ReSalesmanAssign_date";
                x.Caption = "Re Assign date";
                x.VisibleIndex = 24;
                x.Width = 80;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ReSalesmanAssign_date'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        //x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    //x.Width = 100;
                }
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "MODIFIED_BY";
                x.Caption = "Modified By";
                x.VisibleIndex = 25;
                x.Width = 80;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='MODIFIED_BY'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        //x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    //x.Width = 100;
                }
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "MODIFIED_ON";
                x.Caption = "Modified On";
                x.VisibleIndex = 26;
                x.Width = 80;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='MODIFIED_ON'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        //x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    //x.Width = 100;
                }
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "ACTIVITY_DATE";
                x.Caption = "Activity Date";
                x.VisibleIndex = 27;
                x.Width = 80;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ACTIVITY_DATE'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        //x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    //x.Width = 100;
                }
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "ACTIVITY_TIME";
                x.Caption = "Activity Time";
                x.VisibleIndex = 28;
                x.Width = 80;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ACTIVITY_TIME'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        //x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    //x.Width = 100;
                }
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "ACTIVITY_DETAILS";
                x.Caption = "Activity Feedback";
                x.VisibleIndex = 29;
                x.Width = 80;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ACTIVITY_DETAILS'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        //x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    //x.Width = 100;
                }
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "OTHER_REMARKS";
                x.Caption = "Activity Remarks";
                x.VisibleIndex = 30;
                x.Width = 80;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='OTHER_REMARKS'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        //x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    //x.Width = 100;
                }
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "ACTIVITY_STATUS";
                x.Caption = "Activity Status";
                x.VisibleIndex = 31;
                x.Width = 80;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ACTIVITY_STATUS'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        //x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    //x.Width = 100;
                }
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "ACTIVITY_TYPE_NAME";
                x.Caption = "Activity Type";
                x.VisibleIndex = 32;
                x.Width = 80;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ACTIVITY_TYPE_NAME'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        //x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    //x.Width = 100;
                }
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "ACTIVITY_NEXT_DATE";
                x.Caption = "Next Activity Date";
                x.VisibleIndex = 33;
                x.Width = 80;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ACTIVITY_NEXT_DATE'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        //x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    //x.Width = 100;
                }
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
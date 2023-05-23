/*************************************************************************************************************
Written by  Sanchita for   V2.0.40    15/05/2023      Beat Paln
*****************************************************************************************************************/

using BusinessLogicLayer;
using BusinessLogicLayer.SalesmanTrack;
using ClosedXML.Excel;
using DataAccessLayer;
using DevExpress.Utils;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DevExpress.XtraSpreadsheet.Forms;
//using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Models;
using MyShop.Models;
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
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using UtilityLayer;
using Excel = Microsoft.Office.Interop.Excel;
using SalesmanTrack;
using DevExpress.Data.Mask;
using System.Reflection.Emit;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class BeatPlanController : Controller
    {
        // GET: MYSHOP/BeatPlan
        UserList lstuser = new UserList();
        public ActionResult Index()
        {
            try
            {
                BeatPlanModel Dtls = new BeatPlanModel();
                DataSet ds = new DataSet();
                ProcedureExecute proc = new ProcedureExecute("PRC_BEATPLAN");
                proc.AddPara("@ACTION", "GetAreaRouteBeat");
                ds = proc.GetDataSet();

                if (ds != null)
                {
                    List<clsBeat> Beat = new List<clsBeat>();
                    Beat = APIHelperMethods.ToModelList<clsBeat>(ds.Tables[0]);

                    List<clsArea> Area = new List<clsArea>();
                    Area = APIHelperMethods.ToModelList<clsArea>(ds.Tables[1]);

                    List<clsRoute> Route = new List<clsRoute>();
                    Route = APIHelperMethods.ToModelList<clsRoute>(ds.Tables[2]);


                    Dtls.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                    Dtls.Todate = DateTime.Now.ToString("dd-MM-yyyy");

                    Dtls.date_AssignedFrom = DateTime.Now.ToString("dd-MM-yyyy");
                    Dtls.date_AssignedTo = DateTime.Now.ToString("dd-MM-yyyy");

                    Dtls.BeatList = Beat;
                    Dtls.AreaList = Area;
                    Dtls.RouteList = Route;


                }

                // Listing Page Branch data bind
                EmployeeMasterReport omodel = new EmployeeMasterReport();
                DataTable dtbranch = lstuser.GetHeadBranchList(Convert.ToString(Session["userbranchHierarchy"]), "HO");
                DataTable dtBranchChild = new DataTable();
                if (dtbranch.Rows.Count > 0)
                {
                    dtBranchChild = lstuser.GetChildBranch(Convert.ToString(Session["userbranchHierarchy"]));
                    if (dtBranchChild.Rows.Count > 0)
                    {
                        DataRow dr;
                        dr = dtbranch.NewRow();
                        dr[0] = 0;
                        dr[1] = "All";
                        dtbranch.Rows.Add(dr);
                        dtbranch.DefaultView.Sort = "BRANCH_ID ASC";
                        dtbranch = dtbranch.DefaultView.ToTable();
                    }
                }
                omodel.modelbranch = APIHelperMethods.ToModelList<GetBranch>(dtbranch);
                string h_id = omodel.modelbranch.First().BRANCH_ID.ToString();
                ViewBag.HeadBranch = omodel.modelbranch;
                ViewBag.h_id = h_id;
                // Listing Page Branch data bind

                EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/BeatPlan/Index");
                ViewBag.CanView = rights.CanView;
                ViewBag.CanExport = rights.CanExport;
                ViewBag.CanBulkUpdate = rights.CanBulkUpdate;
               

                return View(Dtls);
            }
            catch (Exception ex)
            {
                //return Json("Error occurred. Error details: " + ex.Message);
                throw new Exception(ex.Message);

            }
        }
        public ActionResult GetFromToDate(String Plan)
        {
            BeatPlanModel ret = new BeatPlanModel();

            if (Plan == "Daily" || Plan == "Custom")
            {
                ret.date_AssignedFrom = DateTime.Now.ToString("dd-MM-yyyy");
                ret.date_AssignedTo = DateTime.Now.ToString("dd-MM-yyyy");
            }
            else if(Plan == "Weekly")
            {
                ret.date_AssignedFrom = DateTime.Now.ToString("dd-MM-yyyy");
                ret.date_AssignedTo = DateTime.Now.AddDays(6).ToString("dd-MM-yyyy");
            }
            else if(Plan == "Monthly")
            {
                var month = DateTime.Now.Month;
                var year = DateTime.Now.Year;
                var daysInmonth = DateTime.DaysInMonth(year,month);
                var startdate = "01-"+ DateTime.Now.ToString("MM") + "-"+ DateTime.Now.ToString("yyyy");
                var enddate = daysInmonth.ToString().PadLeft(2,'0') + "-" + DateTime.Now.ToString("MM") + "-" + DateTime.Now.ToString("yyyy");

                ret.date_AssignedFrom = startdate;
                ret.date_AssignedTo = enddate;
            }
            
            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAreaRouteChain(String code_type, String code_id)
        {
            BeatPlanModel ret = new BeatPlanModel();

            BeatPlanModel Dtls = new BeatPlanModel();
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_BEATPLAN");
            proc.AddPara("@ACTION", "GetAreaRouteChain");
            proc.AddPara("@CODE_TYPE", code_type);
            proc.AddPara("@CODE_ID", code_id);
            dt = proc.GetTable();

            if (dt != null)
            {
                ret.AreaId = dt.Rows[0]["AreaId"].ToString();
                ret.RouteId = dt.Rows[0]["RouteId"].ToString();
            }

            return Json(ret, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult SaveBeatPlan(saveBeatPlan data)
        {
            try
            {
                string FromDate = null;
                string ToDate = null;
                string rtrnvalue = "";
                string Userid = Convert.ToString(Session["userid"]);

                if (data.FromDate != null && data.FromDate != "01-01-0100")
                {
                    FromDate = data.FromDate.Split('-')[2] + '-' + data.FromDate.Split('-')[1] + '-' + data.FromDate.Split('-')[0];
                }
                if (data.ToDate != null && data.ToDate != "01-01-0100")
                {
                    ToDate = data.ToDate.Split('-')[2] + '-' + data.ToDate.Split('-')[1] + '-' + data.ToDate.Split('-')[0];
                }

                ProcedureExecute proc = new ProcedureExecute("PRC_BEATPLAN");
                proc.AddPara("@ACTION", data.Mode);
                proc.AddPara("@PLAN_ID", data.PLAN_ID);
                proc.AddPara("@FROMDATE", FromDate);
                proc.AddPara("@TODATE", ToDate);
                proc.AddPara("@PLAN", data.Plan);
                proc.AddPara("@EMPCNTID", data.EmpCntId);
                proc.AddPara("@BEATID", data.BeatId);
                proc.AddPara("@ROUTEID", data.RouteId);
                proc.AddPara("@AREAID", data.AreaId);
                proc.AddPara("@REMARKS", data.Remarks);
                proc.AddPara("@USERID", Userid);
                proc.AddVarcharPara("@RETURN_VALUE", 50, "", QueryParameterDirection.Output);
                int k = proc.RunActionQuery();
                rtrnvalue = Convert.ToString(proc.GetParaValue("@RETURN_VALUE"));
                return Json(rtrnvalue, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }

        }

        public ActionResult GetBeatPlanList(BeatPlanModel model)
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

                if (model.Is_PageLoad != "1") 
                    Is_PageLoad = "Ispageload";

                ViewData["ModelData"] = model;

                string datfrmat = model.Fromdate.Split('-')[2] + '-' + model.Fromdate.Split('-')[1] + '-' + model.Fromdate.Split('-')[0];
                string dattoat = model.Todate.Split('-')[2] + '-' + model.Todate.Split('-')[1] + '-' + model.Todate.Split('-')[0];
                string Userid = Convert.ToString(Session["userid"]);

                string Branch = model.BranchId;
                //int i = 1;
                //if (model.BranchId != null && model.BranchId.Count > 0)
                //{
                //    foreach (string item in model.BranchId)
                //    {
                //        if (i > 1)
                //            state = state + "," + item;
                //        else
                //            state = item;
                //        i++;
                //    }
                //}

                string empcode = model.Empcode;
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

                DataTable ds = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_BEATPLAN");
                proc.AddPara("@ACTION", "GETBEATPLANLIST");
                proc.AddPara("@FROMDATE", datfrmat);
                proc.AddPara("@TODATE", dattoat);
                proc.AddPara("@BRANCH", Branch);
                proc.AddPara("@USERID", Userid);
                proc.AddPara("@EMPID", empcode);
                ds = proc.GetTable();

                return Json(Userid, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult PartialBeatPlanGridList(String Is_PageLoad)
        {
            EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/BeatPlan/Index");
            ViewBag.CanView = rights.CanView;
            ViewBag.CanExport = rights.CanExport;
            return PartialView(GetDataDetails(Is_PageLoad));
        }

        public IEnumerable GetDataDetails(string Is_PageLoad)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);
            if (Is_PageLoad != "Ispageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTS_BEATPLANLISTs
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTS_BEATPLANLISTs
                        where d.USERID == Convert.ToInt32(Userid) && d.PLAN_ID == 0
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
        }

        public ActionResult ShowBeatPlanDetails(String PLAN_ID)
        {
            try
            {
                DataTable dt = new DataTable();
                saveBeatPlan ret = new saveBeatPlan();
                
                ProcedureExecute proc = new ProcedureExecute("PRC_BEATPLAN");
                proc.AddPara("@ACTION", "EDITBEATPLAN");
                proc.AddPara("@PLAN_ID", PLAN_ID);
                dt = proc.GetTable();

                if (dt != null && dt.Rows.Count > 0)
                {
                    ret.Plan = dt.Rows[0]["PLAN_TYPE"].ToString();
                    ret.FromDate = dt.Rows[0]["PLAN_FROMDATE"].ToString();
                    ret.ToDate = dt.Rows[0]["PLAN_TODATE"].ToString();
                    ret.EMPNAME = dt.Rows[0]["EMPNAME"].ToString();
                    ret.BeatId = dt.Rows[0]["PLAN_ASSNBEATID"].ToString();
                    ret.AreaId = dt.Rows[0]["PLAN_ASSNAREAID"].ToString();
                    ret.RouteId = dt.Rows[0]["PLAN_ASSNROUTEID"].ToString();
                    ret.BeatNameOld = dt.Rows[0]["BeatNameOld"].ToString();
                    ret.AreaNameOld = dt.Rows[0]["AreaNameOld"].ToString();
                    ret.RouteNameOld = dt.Rows[0]["RouteNameOld"].ToString();
                    ret.Remarks = dt.Rows[0]["PLAN_REMARKS"].ToString();
                }
                return Json(ret, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        [HttpPost]
        public JsonResult BeatPlanDelete(string PLAN_ID)
        {
            string output_msg = string.Empty;
            try
            {
                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_BEATPLAN");
                proc.AddPara("@ACTION", "DELETEBEATPLAN");
                proc.AddPara("@PLAN_ID", PLAN_ID);
                dt = proc.GetTable();

                if (dt != null && dt.Rows.Count > 0)
                {
                    output_msg = dt.Rows[0]["MSG"].ToString();
                }
            }
            catch (Exception ex)
            {
                output_msg = "Please try again later";
            }

            return Json(output_msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExporBeatPlanList(int type)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetDoctorBatchGridViewSettings(), GetDataDetails(""));
                case 2:
                    return GridViewExtension.ExportToXlsx(GetDoctorBatchGridViewSettings(), GetDataDetails(""));
                case 3:
                    return GridViewExtension.ExportToXls(GetDoctorBatchGridViewSettings(), GetDataDetails(""));
                case 4:
                    return GridViewExtension.ExportToRtf(GetDoctorBatchGridViewSettings(), GetDataDetails(""));
                case 5:
                    return GridViewExtension.ExportToCsv(GetDoctorBatchGridViewSettings(), GetDataDetails(""));
                default:
                    break;
            }
            return null;
        }

        private GridViewSettings GetDoctorBatchGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "gridBeatPlan";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Beat Plan Details";

            settings.Columns.Add(x =>
            {
                x.FieldName = "FROM_DATE";
                x.Caption = "From Date";
                x.VisibleIndex = 1;
                x.Width = 100;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "TO_DATE";
                x.Caption = "To Date";
                x.VisibleIndex = 2;
                x.Width = 100;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "EMPNAME";
                x.Caption = "Employee";
                x.VisibleIndex = 3;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "BEATNAME";
                x.Caption = "Plan Beat";
                x.VisibleIndex = 4;
                x.Width = 200;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ROUTENAME";
                x.Caption = "Plan Route";
                x.VisibleIndex = 5;
                x.Width = 200;
            });
            //Mantis Issue 24928
            settings.Columns.Add(x =>
            {
                x.FieldName = "AREANAME";
                x.Caption = "Plan Area";
                x.VisibleIndex = 6;
                x.Width = 250;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "DEVIATION";
                x.Caption = "Deviate";
                x.VisibleIndex = 7;
                x.Width = 100;
            });
            //End of Mantis Issue 24928
            settings.Columns.Add(x =>
            {
                x.FieldName = "DEV_BEATNAME";
                x.Caption = "Dev. Beat";
                x.VisibleIndex = 8;
                x.Width = 200;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "VISITED";
                x.Caption = "Visited";
                x.VisibleIndex = 9;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "TOTAL_VISITED";
                x.Caption = "Total Outlet Visited";
                x.VisibleIndex = 10;
                x.Width = 150;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.PropertiesEdit.DisplayFormatString = "0.00";
            });


            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

        public ActionResult DownloadMonthlyPlanFormat()
        {
            string FileName = "MonthlyBeatPlan.xlsx";
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "image/jpeg";
            response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ";");
            response.TransmitFile(Server.MapPath("~/Commonfolder/BeatPlanMonthly.xlsx"));
            response.Flush();
            response.End();

            return null;
        }

        public ActionResult DownloadDailyPlanFormat()
        {
            string FileName = "DailyBeatPlan.xlsx";
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "image/jpeg";
            response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ";");
            response.TransmitFile(Server.MapPath("~/Commonfolder/BeatPlanDaily.xlsx"));
            response.Flush();
            response.End();

            return null;
        }

        public ActionResult ImportPlanExcel()
        {
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        String extension = Path.GetExtension(fname);
                        fname = DateTime.Now.Ticks.ToString() + extension;
                        fname = Path.Combine(Server.MapPath("~/Temporary/"), fname);
                        file.SaveAs(fname);
                        Import_To_Grid(fname, extension, file);
                    }
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        public Int32 Import_To_Grid(string FilePath, string Extension, HttpPostedFileBase file)
        {
            Boolean Success = false;
            Int32 HasLog = 0;
            string MONTHLY_DAILY = "";

            if (file.FileName.Trim() != "")
            {
                if (Extension.ToUpper() == ".XLS" || Extension.ToUpper() == ".XLSX")
                {
                    DataTable dt = new DataTable();
                 
                    string conString = string.Empty;
                    conString = ConfigurationManager.AppSettings["ExcelConString"];
                    conString = string.Format(conString, FilePath);
                    using (OleDbConnection excel_con = new OleDbConnection(conString))
                    {
                        excel_con.Open();
                        string sheet1 = "List$"; //ī;

                        using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
                        {
                            oda.Fill(dt);
                        }
                        excel_con.Close();
                    }

                    if (dt.Columns.Contains("MONTH*"))
                    {
                        MONTHLY_DAILY = "MONTHLY";
                    }
                    else if (dt.Columns.Contains("DATE*"))
                    {
                        MONTHLY_DAILY = "DAILY";
                    }

                        // }
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataTable dtExcelData = new DataTable();
                        dtExcelData.Columns.Add("YEAR", typeof(string));
                        dtExcelData.Columns.Add("MONTH_DATE", typeof(string));
                        dtExcelData.Columns.Add("EMPLOYEE_NAME", typeof(string));
                        dtExcelData.Columns.Add("BEAT_NAME", typeof(string));
                        dtExcelData.Columns.Add("ROUTE_NAME", typeof(string));
                        dtExcelData.Columns.Add("AREA_NAME", typeof(string));
                        
                        foreach (DataRow row in dt.Rows)
                        {
                            if (Convert.ToString(row["YEAR*"]) != "" && MONTHLY_DAILY!="" && Convert.ToString(row["EMPLOYEE NAME*"]) != ""
                              && (Convert.ToString(row["BEAT NAME*"]) != "" || Convert.ToString(row["ROUTE NAME"]) != "" || Convert.ToString(row["AREA NAME"]) != "") )
                            {
                                if (MONTHLY_DAILY == "MONTHLY")
                                {
                                    dtExcelData.Rows.Add(Convert.ToString(row["YEAR*"]), Convert.ToString(row["MONTH*"]), Convert.ToString(row["EMPLOYEE NAME*"]),
                                                    Convert.ToString(row["BEAT NAME*"]), Convert.ToString(row["ROUTE NAME"]), Convert.ToString(row["AREA NAME"]) );
                                }
                                else if (MONTHLY_DAILY == "DAILY")
                                {
                                    dtExcelData.Rows.Add(Convert.ToString(row["YEAR*"]), Convert.ToString(row["DATE*"]), Convert.ToString(row["EMPLOYEE NAME*"]),
                                                    Convert.ToString(row["BEAT NAME*"]), Convert.ToString(row["ROUTE NAME"]), Convert.ToString(row["AREA NAME"]) );
                                }
                            }

                        }
                        try
                        {
                            TempData["PartyImportLog"] = dtExcelData;
                            TempData.Keep();

                            DataTable dtCmb = new DataTable();
                            ProcedureExecute proc = new ProcedureExecute("PRC_BEATPLAN");
                            proc.AddPara("@ACTION", "BULKIMPORT");
                            proc.AddPara("@MONTHLY_DAILY", MONTHLY_DAILY);
                            proc.AddPara("@IMPORT_TABLE", dtExcelData);
                            proc.AddPara("@USERID", Convert.ToInt32(Session["userid"]));
                            dtCmb = proc.GetTable();

                        }
                        catch (Exception)
                        {

                        }
                    }
                }
            }
            return HasLog;
        }
    }
}
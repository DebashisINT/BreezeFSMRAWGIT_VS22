//***************************************************************************************************************
//1.0   V2.0.41     Sanchita    19/07/2023      Add Branch parameter in Listing of Master -> Shops report. Mantis : 26135
//2.0   v2.0.43     Sanchita    16-10-2023      On demand search is required in Product Master & Projection Entry
//                                              Mantis: 26858
//3.0   V2.0.45     Sanchita    06-02-2024       Project name will be auto loaded after selecting the customer in Project & Projection report. Mantis: 27222
//*************************************************************************************************************
using BusinessLogicLayer;
using BusinessLogicLayer.SalesmanTrack;
using DataAccessLayer;
using DevExpress.Utils;
using DevExpress.Web;
using DevExpress.Web.Mvc;
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
    public class ProjectReportController : Controller
    {
        //
        // GET: /MYSHOP/ProjectReport/
        public ActionResult Index()
        {
            ProjectReportModel Dtls = new ProjectReportModel();

            Dtls.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
            Dtls.Todate = DateTime.Now.ToString("dd-MM-yyyy");
            Dtls.Is_PageLoad = "Ispageload";

            // Rev 1.0
            //EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/CRMEnquiries/Index");
            EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/ProjectReport/Index");
            // End of Rev 1.0
            ViewBag.CanAdd = rights.CanAdd;
            ViewBag.CanView = rights.CanView;
            ViewBag.CanExport = rights.CanExport;
            ViewBag.CanDelete = rights.CanDelete;
            ViewBag.CanEdit = rights.CanEdit;

            string user_id = Convert.ToString(Session["userid"]);

            DataSet dsListdata = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("PRC_PROJECTREPORT_LISTING");
            proc.AddPara("@ACTION", "GetListData");
            proc.AddPara("@USERID", Convert.ToInt32(user_id));
            dsListdata = proc.GetDataSet();

            // Rev 2.0
            //DataTable dtShop = new DataTable();
            //dtShop = dsListdata.Tables[0];
            //List<ShopUserAssign> Shoplist = new List<ShopUserAssign>();
            //Shoplist = APIHelperMethods.ToModelList<ShopUserAssign>(dtShop);
            //Dtls.ShopUserList = Shoplist;
            //Dtls.ShopUserId = "0";
            

            //DataTable dtProj = new DataTable();
            //dtProj = dsListdata.Tables[1];
            //List<ProjectNameAssign> ProjectNamelist = new List<ProjectNameAssign>();
            //ProjectNamelist = APIHelperMethods.ToModelList<ProjectNameAssign>(dtProj);
            //Dtls.ProjectNameList = ProjectNamelist;
            //Dtls.ProjectNameId = "0";
            // End of Rev 2.0

            DataTable dtShopType = new DataTable();
            dtShopType = dsListdata.Tables[2];
            List<ShopTypeAssign> ShopTypelist = new List<ShopTypeAssign>();
            ShopTypelist = APIHelperMethods.ToModelList<ShopTypeAssign>(dtShopType);
            Dtls.ShopTypeList = ShopTypelist;
            Dtls.ShopTypeId = "0";

            Dtls.ExecName = Convert.ToString(dsListdata.Tables[3].Rows[0]["user_name"]);

            // Mantis Issue 25203
            ViewBag.CanAddHODRemarks = false;
            if(dsListdata.Tables[4].Rows.Count>0){
                ViewBag.CanAddHODRemarks = true;
            }
            // End of Mantis Issue 25203

            return View(Dtls);
        }

        public ActionResult PartialProjectReportGridList(ProjectReportModel model)
        {
            try
            {
                // Rev 1.0
                //EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/CRMEnquiries/Index");
                EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/ProjectReport/Index");
                // End of Rev 1.0
                ViewBag.CanAdd = rights.CanAdd;
                ViewBag.CanView = rights.CanView;
                ViewBag.CanExport = rights.CanExport;
                ViewBag.CanDelete = rights.CanDelete;
                ViewBag.CanEdit = rights.CanEdit;
                //ViewBag.CanReassign = rights.CanReassign;
                //ViewBag.CanAssign = rights.CanAssign;

                // Mantis Issue 25203
                string user_id = Convert.ToString(Session["userid"]);

                DataSet dsListdata = new DataSet();
                ProcedureExecute proc = new ProcedureExecute("PRC_PROJECTREPORT_LISTING");
                proc.AddPara("@ACTION", "GetListData");
                proc.AddPara("@USERID", Convert.ToInt32(user_id));
                dsListdata = proc.GetDataSet();

                ViewBag.CanAddHODRemarks = false;
                if (dsListdata.Tables[4].Rows.Count > 0)
                {
                    ViewBag.CanAddHODRemarks = true;
                }
                // End of Mantis Issue 25203
                
                //string EnquiryFrom = "";
                //int i = 1;

                //if (model.EnquiryFromDesc != null && model.EnquiryFromDesc.Count > 0)
                //{
                //    foreach (string item in model.EnquiryFromDesc)
                //    {
                //        if (i > 1)
                //            EnquiryFrom = EnquiryFrom + "," + item;
                //        else
                //            EnquiryFrom = item;
                //        i++;
                //    }

                //}


                string Is_PageLoad = string.Empty;

                if (model.Is_PageLoad == "Ispageload")
                {
                    Is_PageLoad = "is_pageload";

                }

                string datfrmat = model.Fromdate.Split('-')[2] + '-' + model.Fromdate.Split('-')[1] + '-' + model.Fromdate.Split('-')[0];
                string dattoat = model.Todate.Split('-')[2] + '-' + model.Todate.Split('-')[1] + '-' + model.Todate.Split('-')[0];


                GetProjectReportListing(datfrmat, dattoat, Is_PageLoad);

               // model.Is_PageLoad = "Ispageload";

                return PartialView("PartialProjectReportGridList", GetProjectReportDetails(Is_PageLoad, model.FilterTypes, model.OrderLostReasonId,model.ProjectCompletedDTId));

            }
            catch (Exception ex)
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }

        }

        public void GetProjectReportListing(string FromDate, string ToDate, string Is_PageLoad)
        {
            string user_id = Convert.ToString(Session["userid"]);
            //string FROMDATE = dtFrom.ToString("yyyy-MM-dd");
            //string TODATE = dtTo.ToString("yyyy-MM-dd");

            string action = string.Empty;
            DataTable formula_dtls = new DataTable();
            DataSet dsInst = new DataSet();

            try
            {
                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_PROJECTREPORT_LISTING");
                proc.AddPara("@ACTION", "GETLISTINGDATA");
                proc.AddPara("@USERID", Convert.ToInt32(user_id));
                proc.AddPara("@FROMDATE", FromDate);
                proc.AddPara("@TODATE", ToDate);
                proc.AddPara("@Is_PageLoad", Is_PageLoad);
                dt = proc.GetTable();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable GetProjectReportDetails(string Is_PageLoad, string FilterTypes, string OrderLostReasonId, string ProjectCompletedDTId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);
            
            DataTable dtColmn = GetPageRetention(Session["userid"].ToString(), "Project & Projection Entry");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            }
            
            if (Is_PageLoad != "is_pageload")
            {
                if (FilterTypes == "ORDER_LOST_REASON" && OrderLostReasonId != null && OrderLostReasonId != "")
                {
                    //string OrderLostReason = OrderLostReasonId.Replace(" ", "");
                    //OrderLostReason = OrderLostReason.Replace("~", " ");
                    //OrderLostReason = "'" + OrderLostReason.Replace(",","','") + "'" ;
                    string OrderLostReason = OrderLostReasonId.Replace("~", " ");

                    List<string> ReasonIDlist;
                    ReasonIDlist = new List<string>(OrderLostReason.Split(','));
                    
                    ReportsDataContext dc = new ReportsDataContext(connectionString);
                    var q = from d in dc.PROJECTREPORT_LISTINGs
                            where d.USERID == Convert.ToInt32(Userid) && ReasonIDlist.Contains(Convert.ToString(d.ORDER_LOST))
                            orderby d.SEQ
                            select d;
                    return q;
                }
                else if (FilterTypes == "PROJECT_COMPLETE_DATE" && ProjectCompletedDTId != null && ProjectCompletedDTId != "")
                {
                    //string OrderLostReason = OrderLostReasonId.Replace(" ", "");
                    //OrderLostReason = OrderLostReason.Replace("~", " ");
                    //OrderLostReason = "'" + OrderLostReason.Replace(",","','") + "'" ;
                    //string OrderLostReason = OrderLostReasonId.Replace("~", " ");

                    List<string> CompletedDTlist;
                    CompletedDTlist = new List<string>(ProjectCompletedDTId.Split(','));

                    ReportsDataContext dc = new ReportsDataContext(connectionString);
                    var q = from d in dc.PROJECTREPORT_LISTINGs
                            where d.USERID == Convert.ToInt32(Userid) && CompletedDTlist.Contains(Convert.ToString(d.PROJ_COMPLETE_DT_ID))
                            orderby d.SEQ
                            select d;
                    return q;
                }
                else{
                    ReportsDataContext dc = new ReportsDataContext(connectionString);
                    var q = from d in dc.PROJECTREPORT_LISTINGs
                            where d.USERID == Convert.ToInt32(Userid)
                            orderby d.SEQ
                            select d;
                    return q;
                }
                
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.PROJECTREPORT_LISTINGs
                        where d.USERID == Convert.ToInt32(Userid) && d.SEQ == 11111111119
                        orderby d.SEQ
                        select d;
                return q;
            }

           

        }
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

        [HttpPost]
        public JsonResult Savedata(ProjectReportModel apply, string uniqueid)
        {
            string ReturnCode = "";
            string ReturnMsg = "";
            //_enquiries = new EnquiriesRepo();
            try
            {
                string Userid = Convert.ToString(Session["userid"]);

                // Rev 1.0
                string StartDate = null;
                string EndDate = null;
                if (apply.Date != null && apply.Date != "01-01-0100")
                {
                    StartDate = apply.Date.Split('/')[2] + '-' + apply.Date.Split('/')[1] + '-' + apply.Date.Split('/')[0];
                }
                if (apply.proj_complete_dt != null && apply.proj_complete_dt != "01-01-0100")
                {
                    EndDate = apply.proj_complete_dt.Split('/')[2] + '-' + apply.proj_complete_dt.Split('/')[1] + '-' + apply.proj_complete_dt.Split('/')[0];
                }
                // End of Rev 1.0

                DataTable dtImport = new DataTable();
                ProcedureExecute proc1 = new ProcedureExecute("PRC_PROJECTREPORT_LISTING");
                proc1.AddPara("@USERID", Userid);
                proc1.AddPara("@ACTION", apply.Action_type);

                proc1.AddPara("@Month", apply.Month);
                proc1.AddPara("@Year", apply.Year);
                //proc1.AddPara("@PROJ_START_DT", Convert.ToDateTime( apply.Date));
                proc1.AddPara("@PROJ_START_DT", StartDate);
                proc1.AddPara("@Project_Name", Convert.ToString(apply.ProjectName));
                proc1.AddPara("@Area", apply.Area);
                proc1.AddPara("@Shop_Code", apply.Shop);
                proc1.AddPara("@ShopTypeId", apply.ShopType);
                proc1.AddPara("@Contact_Person", apply.Contact_Person);
                proc1.AddPara("@PhoneNo", apply.PhoneNo);
                proc1.AddPara("@ApproxQty", apply.ApproxQty);

                proc1.AddPara("@Grade", apply.Grade);
                proc1.AddPara("@ProdName", apply.ProdName);
                proc1.AddPara("@ExptMonth", apply.ExptMonth);
                proc1.AddPara("@ExptYear", apply.ExptYear);
                proc1.AddPara("@Remarks", apply.Remarks);
                proc1.AddPara("@OrderLost", apply.OrderLost);

                if (apply.proj_complete_dt != null)
                {
                    // Rev 1.0
                    //proc1.AddPara("@proj_complete_dt", Convert.ToDateTime(apply.proj_complete_dt));
                    proc1.AddPara("@proj_complete_dt", EndDate);
                    // End of Rev 1.0
                }

                if (!String.IsNullOrEmpty(uniqueid))
                {
                    proc1.AddPara("@PR_ID", (uniqueid));
                } 

                // Mantis Issue 25203
                proc1.AddPara("@ArctName", apply.ArctName);
                proc1.AddPara("@ConslName", apply.ConslName);
                proc1.AddPara("@FabrName", apply.FabrName);
                proc1.AddPara("@Others", apply.Others);
                // End of Mantis Issue 25203
              
                proc1.AddVarcharPara("@RETURNMESSAGE", 50, "", QueryParameterDirection.Output);
                proc1.AddVarcharPara("@RETURNCODE", 50, "", QueryParameterDirection.Output);
                dtImport = proc1.GetTable();

                ReturnMsg = Convert.ToString(proc1.GetParaValue("@RETURNMESSAGE"));
                ReturnCode = Convert.ToString(proc1.GetParaValue("@RETURNCODE"));

                //ReturnMsg = Convert.ToStrisng(execProc.outputPara[0].value);
                //ReturnCode = Convert.ToInt32(execProc.outputPara[1].value);



                //    enquiries.save(apply, uniqueid, ref ReturnCode, ref ReturnMsg);
                if (ReturnMsg == "Success" && ReturnCode == "1")
                {
                    apply.response_code = "Success";
                    apply.response_msg = "Success";
                }
                else if (ReturnMsg != "Success" && ReturnCode == "-1")
                {
                    apply.response_code = "Error";
                    apply.response_msg = ReturnMsg;
                }
                else
                {
                    apply.response_code = "Error";
                    apply.response_msg = "Please try again later";
                }

            }

            catch (Exception ex)
            {
                apply.response_code = "CatchError";
                apply.response_msg = "Please try again later";
            }

            return Json(apply, JsonRequestBehavior.AllowGet);
        }

        // Mantis Issue 25203
        [HttpPost]
        public JsonResult SaveHODRemarks(string HODRemarks, string uniqueid)
        {
            ProjectReportModel apply = new ProjectReportModel();
            string ReturnCode = "";
            string ReturnMsg = "";
            //_enquiries = new EnquiriesRepo();
            try
            {
                string Userid = Convert.ToString(Session["userid"]);

                DataTable dtImport = new DataTable();
                ProcedureExecute proc1 = new ProcedureExecute("PRC_PROJECTREPORT_LISTING");
                proc1.AddPara("@USERID", Userid);
                proc1.AddPara("@ACTION", "SaveHODRemarks");
                proc1.AddPara("@PR_ID", uniqueid);
                proc1.AddPara("@HODRemarks", HODRemarks);
               
                proc1.AddVarcharPara("@RETURNMESSAGE", 50, "", QueryParameterDirection.Output);
                proc1.AddVarcharPara("@RETURNCODE", 50, "", QueryParameterDirection.Output);
                dtImport = proc1.GetTable();

                ReturnMsg = Convert.ToString(proc1.GetParaValue("@RETURNMESSAGE"));
                ReturnCode = Convert.ToString(proc1.GetParaValue("@RETURNCODE"));

                //ReturnMsg = Convert.ToStrisng(execProc.outputPara[0].value);
                //ReturnCode = Convert.ToInt32(execProc.outputPara[1].value);



                //    enquiries.save(apply, uniqueid, ref ReturnCode, ref ReturnMsg);
                if (ReturnMsg == "Success" && ReturnCode == "1")
                {
                    apply.response_code = "Success";
                    apply.response_msg = "Success";
                }
                else if (ReturnMsg != "Success" && ReturnCode == "-1")
                {
                    apply.response_code = "Error";
                    apply.response_msg = ReturnMsg;
                }
                else
                {
                    apply.response_code = "Error";
                    apply.response_msg = "Please try again later";
                }

            }

            catch (Exception ex)
            {
                apply.response_code = "CatchError";
                apply.response_msg = "Please try again later";
            }

            return Json(apply, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetHODRemarks(String PR_ID)
        {
            try
            {
                ProjectReportModel _apply = new ProjectReportModel();
                
                try
                {
                    ExecProcedure execProc = new ExecProcedure();
                    string Userid = Convert.ToString(Session["userid"]);

                    DataTable _getenq = new DataTable();
                    ProcedureExecute proc1 = new ProcedureExecute("PRC_PROJECTREPORT_LISTING");
                    proc1.AddPara("@USERID", Userid);
                    proc1.AddPara("@PR_ID", PR_ID);
                    proc1.AddPara("@ACTION", "GetHODRemarks");
                    _getenq = proc1.GetTable();


                    if (_getenq.Rows.Count > 0)
                    {
                        _apply.HODRemarks = _getenq.Rows[0]["HODRemarks"].ToString();
                    }

                }
                catch (Exception ex)
                {

                }
                return Json(_apply, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
        // End of Mantis Issue 25203

        public ActionResult ShowDetails(String PR_ID)
        {
            try
            {
                //DataSet _getenq = new DataSet();
                ProjectReportModel _apply = new ProjectReportModel();
                //EnquiriesDet _header = new EnquiriesDet();
                try
                {
                    ExecProcedure execProc = new ExecProcedure();

                    DataTable _getenq = new DataTable();
                    ProcedureExecute proc1 = new ProcedureExecute("PRC_PROJECTREPORT_LISTING");
                    proc1.AddPara("@PR_ID", PR_ID);
                    proc1.AddPara("@ACTION", "EDIT");
                    _getenq = proc1.GetTable();


                    if (_getenq.Rows.Count > 0)
                    {
                        _apply.PR_ID = PR_ID;
                        _apply.Month = Convert.ToInt32(_getenq.Rows[0]["MONTH"]);
                        _apply.Year = _getenq.Rows[0]["YEAR"].ToString();
                        //_apply.Date = Convert.ToDateTime(_getenq.Rows[0]["PROJ_STARTDT"]);
                        _apply.Date = _getenq.Rows[0]["PROJ_STARTDT"].ToString();
                        _apply.Shop = _getenq.Rows[0]["Shop_Code"].ToString();
                        // Rev 2.0
                        _apply.ShopName = _getenq.Rows[0]["Shop_Name"].ToString();
                        // End of Rev 2.0
                        _apply.ProjectName = _getenq.Rows[0]["Project_Name"].ToString();
                        _apply.Area = _getenq.Rows[0]["Area"].ToString();
                        _apply.ShopType = _getenq.Rows[0]["ShopTypeId"].ToString();
                        _apply.Contact_Person = _getenq.Rows[0]["CONTACT_PERSON"].ToString();
                        _apply.PhoneNo = _getenq.Rows[0]["PHONE_NO"].ToString();
                        _apply.ApproxQty = Convert.ToDecimal(_getenq.Rows[0]["APPROX_QTY_SQFT"]);
                        _apply.Grade = _getenq.Rows[0]["GRADE"].ToString();
                        _apply.ProdName = _getenq.Rows[0]["PROD_NAMECODE"].ToString();
                        _apply.ExptMonth = _getenq.Rows[0]["EXPECTED_MONTH"].ToString();
                        _apply.ExptYear = _getenq.Rows[0]["EXPECTED_YEAR"].ToString();
                        _apply.Remarks = _getenq.Rows[0]["CURRENT_REMARKS"].ToString();
                        _apply.OrderLost = _getenq.Rows[0]["ORDER_LOST"].ToString();
                       // _apply.proj_complete_dt = Convert.ToDateTime(_getenq.Rows[0]["PROJ_COMPLETE_DT"]);
                        _apply.proj_complete_dt = _getenq.Rows[0]["PROJ_COMPLETE_DT"].ToString();
                        // Mantis Issue 25203
                        _apply.ArctName = _getenq.Rows[0]["ArctName"].ToString();
                        _apply.ConslName = _getenq.Rows[0]["ConslName"].ToString();
                        _apply.FabrName = _getenq.Rows[0]["FabrName"].ToString();
                        _apply.Others = _getenq.Rows[0]["Others"].ToString();
                        // End of Mantis Issue 25203

                    }

                }
                catch (Exception ex)
                {

                }
                return Json(_apply, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public JsonResult DeleteData(string PR_ID)
        {
            string output_msg = string.Empty;
            try
            {
                DataTable dt = new DataTable();
                ProcedureExecute proc1 = new ProcedureExecute("PRC_PROJECTREPORT_LISTING");
                proc1.AddPara("@PR_ID", PR_ID);
                proc1.AddPara("@ACTION", "DELETE");

                proc1.AddVarcharPara("@RETURNMESSAGE", 50, "", QueryParameterDirection.Output);
                proc1.AddVarcharPara("@RETURNCODE", 50, "", QueryParameterDirection.Output);
                dt = proc1.GetTable();

                output_msg = Convert.ToString(proc1.GetParaValue("@RETURNMESSAGE"));

            }
            catch (Exception ex)
            {
                output_msg = "Please try again later";
            }

            return Json(output_msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProjectNameList(string selShop)
        {
            ProjectReportModel Dtls = new ProjectReportModel();
            string user_id = Convert.ToString(Session["userid"]);

            DataSet dsListdata = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("PRC_PROJECTREPORT_LISTING");
            proc.AddPara("@ACTION", "GetProjectNameList");
            proc.AddPara("@USERID", Convert.ToInt32(user_id));
            proc.AddPara("@Shop_Code", selShop);
            dsListdata = proc.GetDataSet();

            DataTable dtProj = new DataTable();
            dtProj = dsListdata.Tables[0];
            List<ProjectNameAssign> ProjectNamelist = new List<ProjectNameAssign>();
            ProjectNamelist = APIHelperMethods.ToModelList<ProjectNameAssign>(dtProj);
            Dtls.ProjectNameList = ProjectNamelist;
            Dtls.ProjectNameId = "0";

            return Json(Dtls, JsonRequestBehavior.AllowGet);
        }

        // Rev 3.0
        public ActionResult GetProjectName(string selShop)
        {
            ProjectReportModel Dtls = new ProjectReportModel();
            string user_id = Convert.ToString(Session["userid"]);

            DataSet dsListdata = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("PRC_PROJECTREPORT_LISTING");
            proc.AddPara("@ACTION", "GetProjectName");
            proc.AddPara("@USERID", Convert.ToInt32(user_id));
            proc.AddPara("@Shop_Code", selShop);
            dsListdata = proc.GetDataSet();

            DataTable dtProj = new DataTable();
            dtProj = dsListdata.Tables[0];
            if (dtProj.Rows.Count > 0)
            {
                Dtls.ProjectName =  dtProj.Rows[0]["Project_Name"].ToString();
                Dtls.ProjectNameId = dtProj.Rows[0]["Project_Name"].ToString();
            }
            

            return Json(Dtls, JsonRequestBehavior.AllowGet);
        }
        // End of Rev 3.0

        public ActionResult ShowAutofillDetails(string selShop, string selProjectName)
        {
            try
            {
                //DataSet _getenq = new DataSet();
                ProjectReportModel _apply = new ProjectReportModel();
                //EnquiriesDet _header = new EnquiriesDet();
                try
                {
                    ExecProcedure execProc = new ExecProcedure();

                    DataTable _getenq = new DataTable();
                    ProcedureExecute proc1 = new ProcedureExecute("PRC_PROJECTREPORT_LISTING");
                    proc1.AddPara("@ACTION", "AUTOFILL");
                    //proc1.AddPara("@Month", selMonth);
                    //proc1.AddPara("@Year", selYear);
                    proc1.AddPara("@Shop_Code", selShop);
                    proc1.AddPara("@Project_Name", selProjectName);
                    _getenq = proc1.GetTable();


                    if (_getenq.Rows.Count > 0)
                    {
                       // _apply.PR_ID = _getenq.Rows[0]["PR_ID"].ToString(); 
                        _apply.Month = Convert.ToInt32(_getenq.Rows[0]["MONTH"]);
                        _apply.Year = _getenq.Rows[0]["YEAR"].ToString();
                        //_apply.Date = Convert.ToDateTime(_getenq.Rows[0]["PROJ_STARTDT"]);
                        _apply.Date = _getenq.Rows[0]["PROJ_STARTDT"].ToString();
                        //_apply.Shop = _getenq.Rows[0]["Shop_Code"].ToString();
                        //_apply.ProjectName = _getenq.Rows[0]["Project_Name"].ToString();
                        _apply.Area = _getenq.Rows[0]["Area"].ToString();
                        _apply.ShopType = _getenq.Rows[0]["ShopTypeId"].ToString();
                        _apply.Contact_Person = _getenq.Rows[0]["CONTACT_PERSON"].ToString();
                        _apply.PhoneNo = _getenq.Rows[0]["PHONE_NO"].ToString();
                        _apply.ApproxQty = Convert.ToDecimal(_getenq.Rows[0]["APPROX_QTY_SQFT"]);
                        _apply.Grade = _getenq.Rows[0]["GRADE"].ToString();
                        _apply.ProdName = _getenq.Rows[0]["PROD_NAMECODE"].ToString();
                        _apply.ExptMonth = _getenq.Rows[0]["EXPECTED_MONTH"].ToString();
                        _apply.ExptYear = _getenq.Rows[0]["EXPECTED_YEAR"].ToString();
                        _apply.Remarks = _getenq.Rows[0]["CURRENT_REMARKS"].ToString();
                        _apply.OrderLost = _getenq.Rows[0]["ORDER_LOST"].ToString();
                        // _apply.proj_complete_dt = Convert.ToDateTime(_getenq.Rows[0]["PROJ_COMPLETE_DT"]);
                        _apply.proj_complete_dt = _getenq.Rows[0]["PROJ_COMPLETE_DT"].ToString();


                    }

                }
                catch (Exception ex)
                {

                }
                return Json(_apply, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetOrderLostReason()
        {
            ProjectReportModel Dtls = new ProjectReportModel();
            string user_id = Convert.ToString(Session["userid"]);

            DataSet dsListdata = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("PRC_PROJECTREPORT_LISTING");
            proc.AddPara("@ACTION", "GetOrderLostReason");
            proc.AddPara("@USERID", Convert.ToInt32(user_id));
            dsListdata = proc.GetDataSet();

            DataTable dtOrderLost = new DataTable();
            dtOrderLost = dsListdata.Tables[0];
            List<OrderLostReason> OrderLostReasonlist = new List<OrderLostReason>();
            OrderLostReasonlist = APIHelperMethods.ToModelList<OrderLostReason>(dtOrderLost);
            Dtls.OrderLostReasonList = OrderLostReasonlist;
            Dtls.OrderLostReasonId = "0";

            return Json(Dtls, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProjectCompletedDT()
        {
            ProjectReportModel Dtls = new ProjectReportModel();
            string user_id = Convert.ToString(Session["userid"]);

            DataSet dsListdata = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("PRC_PROJECTREPORT_LISTING");
            proc.AddPara("@ACTION", "GetProjectCompletedDT");
            proc.AddPara("@USERID", Convert.ToInt32(user_id));
            dsListdata = proc.GetDataSet();

            DataTable dtProjectCompletedDT = new DataTable();
            dtProjectCompletedDT = dsListdata.Tables[0];
            List<ProjectCompletedDT> ProjectCompletedDTlist = new List<ProjectCompletedDT>();
            ProjectCompletedDTlist = APIHelperMethods.ToModelList<ProjectCompletedDT>(dtProjectCompletedDT);
            Dtls.ProjectCompletedDTList = ProjectCompletedDTlist;
            Dtls.ProjectCompletedDTId = "0";

            return Json(Dtls, JsonRequestBehavior.AllowGet);
        }

        // Start Export
        public ActionResult ExporRegisterList(int type)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetDoctorBatchGridViewSettings(), GetProjectReportDetails("","","",""));
                case 2:
                    return GridViewExtension.ExportToXlsx(GetDoctorBatchGridViewSettings(), GetProjectReportDetails("", "","",""));
                case 3:
                    return GridViewExtension.ExportToXls(GetDoctorBatchGridViewSettings(), GetProjectReportDetails("", "","",""));
                case 4:
                    return GridViewExtension.ExportToRtf(GetDoctorBatchGridViewSettings(), GetProjectReportDetails("", "","",""));
                case 5:
                    return GridViewExtension.ExportToCsv(GetDoctorBatchGridViewSettings(), GetProjectReportDetails("", "","",""));
                default:
                    break;
            }
            return null;
        }

        private GridViewSettings GetDoctorBatchGridViewSettings()
        {
            DataTable dtColmn = GetPageRetention(Session["userid"].ToString(), "Project & Projection Entry");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            }
           
            var settings = new GridViewSettings();
            settings.Name = "gridProjectReport";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Project & Projection";

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
                    }
                }
                else
                {
                    x.Visible = true;
                }
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "MONTH_YEAR";
                x.Caption = "Month";
                x.VisibleIndex = 2;
                x.Width = 110;
                
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='MONTH_YEAR'");
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
                x.FieldName = "PROJ_STARTDT_ID";
                x.Caption = "Project Start Date";
                x.VisibleIndex = 3;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PROJ_STARTDT_ID'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 120;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 120;
                }
            });


            settings.Columns.Add(x =>
            {
                x.FieldName = "PROJ_NAME";
                x.Caption = "Name of the Project";
                x.VisibleIndex = 4;
                //x.Width = 60;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PROJ_NAME'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 160;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 160;
                }
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "Area";
                x.Caption = "Area";
                x.VisibleIndex = 5;
                //
                //x.Width = 60;
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
                        x.Width = 80;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 80;
                }
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "CUST_NAME";
                x.Caption = "Architect/Fabricator/Builder/Contractor info";
                x.VisibleIndex = 6;
                //
                //x.Width = 60;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CUST_NAME'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 120;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 120;
                }
            });
           
            settings.Columns.Add(x =>
            {
                x.FieldName = "CATEGORY";
                x.Caption = "Category";
                x.VisibleIndex = 7;
                
               if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CATEGORY'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 110;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 110;
                }
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "CONTACT_PERSON";
                x.Caption = "Contact Person";
                x.VisibleIndex = 8;
                // 
                //x.Width = 80;
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
                        x.Width = 120;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 120;
                }
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "PHONE_NO";
                x.Caption = "Contact No.";
                x.VisibleIndex = 9;
                // 
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PHONE_NO'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 100;
                }
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "APPROX_QTY_SQFT";
                x.Caption = "Approx. Qty.(sqft)";
                x.VisibleIndex = 10;
                //  
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='APPROX_QTY_SQFT'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 120;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 120;
                }
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "GRADE";
                x.Caption = "Grade";
                x.VisibleIndex = 11;
                //  
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='GRADE'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 100;
                }
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "PROD_NAMECODE";
                x.Caption = "Product Name & Code";
                x.VisibleIndex = 12;
                //x.Width = 120;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PROD_NAMECODE'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 130;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 130;
                }
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "EXPECTED_MONTH";
                x.Caption = "Expected Month";
                x.VisibleIndex = 13;
                //x.Width = 120;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='EXPECTED_MONTH'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 100;
                }
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "CURRENT_REMARKS";
                x.Caption = "Current Remarks";
                x.VisibleIndex = 14;
                //x.Width = 100;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CURRENT_REMARKS'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 500;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 500;
                }
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "EXECUTIVE_NAME";
                x.Caption = "Executive Name";
                x.VisibleIndex = 15;
                //x.Width = 96;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='EXECUTIVE_NAME'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 100;
                }

            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "ORDER_LOST";
                x.Caption = "Order Lost";
                x.VisibleIndex = 16;

                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ORDER_LOST'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 110;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 110;
                }

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "PROJ_COMPLETE_DT_ID";
                x.Caption = "Completed Project";
                x.VisibleIndex = 17;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";

                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PROJ_COMPLETE_DT_ID'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 120;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 120;
                }
                //End of Mantis Issue 24816
            });

            // Mantis Issue 25203
            settings.Columns.Add(x =>
            {
                x.FieldName = "ARCTNAME";
                x.Caption = "Architect Name";
                x.VisibleIndex = 18;
                //
                //x.Width = 60;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ARCTNAME'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 80;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 80;
                }
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "CONSLNAME";
                x.Caption = "Consultant Name";
                x.VisibleIndex = 19;
                //
                //x.Width = 60;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONSLNAME'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 80;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 80;
                }
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "FABRNAME";
                x.Caption = "Fabricator Name";
                x.VisibleIndex = 20;
                //
                //x.Width = 60;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='FABRNAME'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 80;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 80;
                }
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "OTHERS";
                x.Caption = "Others";
                x.VisibleIndex = 21;
                //
                //x.Width = 60;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='OTHERS'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 80;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 80;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "HODREMARKS";
                x.Caption = "HOD Remarks";
                x.VisibleIndex = 22;
                //
                //x.Width = 60;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='HODREMARKS'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 120;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 120;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "HODVISITEDON";
                x.Caption = "Supervisor Last seen";
                x.VisibleIndex = 23;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy hh:mm:ss";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy hh:mm:ss";
                //
                //x.Width = 60;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='HODVISITEDON'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 120;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 120;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "HODVISITEDBY";
                x.Caption = "Supervisor Name";
                x.VisibleIndex = 24;
                //
                //x.Width = 60;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='HODVISITEDBY'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 80;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 80;
                }
            });
            // End of Mantis Issue 25203

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }


        public ActionResult DownloadFormat()
        {
            string FileName = "CRMEnquiries.xlsx";
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "image/jpeg";
            response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ";");
            response.TransmitFile(Server.MapPath("~/Commonfolder/CRMEnquiries.xlsx"));
            response.Flush();
            response.End();

            return null;
        }

        public ActionResult ImportExcel()
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

                    // }
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        try
                        {
                            TempData["PartyImportLog"] = dt;
                            TempData.Keep();

                            DataTable dtCmb = new DataTable();
                            ProcedureExecute proc = new ProcedureExecute("PRC_FTSImportNewParty");
                            proc.AddPara("@IMPORT_TABLE", dt);
                            proc.AddPara("@CreateUser_Id", Convert.ToInt32(Session["userid"]));
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

        private string GetValue(SpreadsheetDocument doc, Cell cell)
        {
            string value = cell.CellValue.InnerText;
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value)).InnerText;
            }
            return value;
        }
        //Susanta 25-07-22
        public JsonResult UserListFORCHART()
        {




            //Dashboard dashboarddataobj = new Dashboard();
            List<UserListClass> caldata = new List<UserListClass>();
            try
            {
                DataSet ds = new DataSet();
                ProcedureExecute proc = new ProcedureExecute("PRC_PROJECTREPORT_LISTING");

                proc.AddPara("@ACTION", "UserList");
                proc.AddPara("@USERID", Session["userid"].ToString());
                ds = proc.GetDataSet();
                DataSet objData = ds;
              

                //DataSet objData = dashboarddataobj.GetOrderAnalytics(fromDate, toDate, "ORDCNTDATE", Session["userid"].ToString());
                if (objData != null && objData.Tables[0] != null && objData.Tables[0].Rows.Count > 0)
                {
                    caldata = (from DataRow dr in objData.Tables[0].Rows
                               select new UserListClass()
                               {
                                   UserId = Convert.ToString(dr["UserId"]),
                                   UserName = Convert.ToString(dr["UserName"])
                               }).ToList();
                }
            }
            catch
            {

            }
            return Json(caldata);
        }
        public JsonResult CHARTONE(string userId)
        {
            //Dashboard dashboarddataobj = new Dashboard();
            List<chartOneClass> caldata = new List<chartOneClass>();
            try
            {
                DataSet ds = new DataSet();
                ProcedureExecute proc = new ProcedureExecute("PRC_PROJECTREPORT_LISTING");

                proc.AddPara("@ACTION", "TotProject_VS_OrderLost");
                proc.AddPara("@USERID", userId);
                ds = proc.GetDataSet();
                DataSet objData = ds;


                //DataSet objData = dashboarddataobj.GetOrderAnalytics(fromDate, toDate, "ORDCNTDATE", Session["userid"].ToString());
                if (objData != null && objData.Tables[0] != null && objData.Tables[0].Rows.Count > 0)
                {
                    caldata = (from DataRow dr in objData.Tables[0].Rows
                               select new chartOneClass()
                               {
                                   TotProjectCnt = Convert.ToString(dr["TotProjectCnt"]),
                                   OrderLostCnt = Convert.ToString(dr["OrderLostCnt"])
                               }).ToList();
                }
            }
            catch
            {

            }
            return Json(caldata);
        }

        public JsonResult CHARTTWO(string userId)
        {

            //Dashboard dashboarddataobj = new Dashboard();
            List<chartTwoClass> caldata = new List<chartTwoClass>();
            try
            {
                DataSet ds = new DataSet();
                ProcedureExecute proc = new ProcedureExecute("PRC_PROJECTREPORT_LISTING");

                proc.AddPara("@ACTION", "TotProject_VS_InactivProject");
                proc.AddPara("@USERID", userId);
                ds = proc.GetDataSet();
                DataSet objData = ds;


                //DataSet objData = dashboarddataobj.GetOrderAnalytics(fromDate, toDate, "ORDCNTDATE", Session["userid"].ToString());
                if (objData != null && objData.Tables[0] != null && objData.Tables[0].Rows.Count > 0)
                {
                    caldata = (from DataRow dr in objData.Tables[0].Rows
                               select new chartTwoClass()
                               {
                                   TotProjectCnt = Convert.ToString(dr["TotProjectCnt"]),
                                   InactivProjectCnt = Convert.ToString(dr["InactivProjectCnt"])
                               }).ToList();
                }
            }
            catch
            {

            }
            return Json(caldata);
        }
        // End Export
	}

    public class chartOneClass
    {
       	
        public string TotProjectCnt { get; set; }
        public string OrderLostCnt { get; set; }	
    }
    public class chartTwoClass
    {	
        public string TotProjectCnt { get; set; }
        public string InactivProjectCnt { get; set; }
    }


}
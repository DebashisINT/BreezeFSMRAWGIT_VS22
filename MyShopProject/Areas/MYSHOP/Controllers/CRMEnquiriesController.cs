/******************************************************************************************************************
  1.0       16-08-2023        V2.0.42          Sanchita          The enquiry doesn't showing in the listing after modification
                                                                 Mantis: 26721
  2.0       18-08-2023        V2.0.42          Sanchita          Customisation required in Lead Activity for BreezeFSM App.
                                                                 Mantis: 26727
  3.0       23-11-2023        V2.0.43          Sanchita          Bulk Import feature required for Enquiry Module.Mantis: 27020 
  4.0		25-04-2024	      V2.0.46		   Priti             0027383: New Enquires type Add and Hide # Eurobond Portal
  5.0       10/09/2024        V2.0.48          Sanchita          27690: Quotation Notification issue @ Eurobond
  6.0       28/09/2024        V2.0.49          Priti             Report taking too much time time to generate.Now resolved.Refer: 0027728

**************************************************************************************************************************/
using BusinessLogicLayer;
using BusinessLogicLayer.SalesmanTrack;
using DataAccessLayer;
using DevExpress.Utils;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Google.Apis.Auth.OAuth2;
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
using System.Web.Script.Services;
using System.Web.Services;
using System.Xml;
using UtilityLayer;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class CRMEnquiriesController : Controller
    {
       //
        // GET: /MYSHOP/CRMEnquiries/
       // AddPartyDetailsBL obj = new AddPartyDetailsBL();

        // INDIAMART API
        //List<IndiamartModelClass> omodel = new List<IndiamartModelClass>();
        //List<IndiamartModelErrorClass> indmdlerror = new List<IndiamartModelErrorClass>();
        //private Random _random = new Random();
        //DBEngine objsql = new DBEngine();
        //DataTable dtobsql = new DataTable();
        // API

        //Mantis Issue 0024759
        NotificationBL notificationbl = new NotificationBL();
        DBEngine odbengine = new DBEngine();
        //End of Mantis Issue 0024759
        public ActionResult Index()
        {
            CRMEnquiriesModel Dtls = new CRMEnquiriesModel();

            Dtls.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
            Dtls.Todate = DateTime.Now.ToString("dd-MM-yyyy");
            Dtls.Is_PageLoad = "Ispageload";

            EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/CRMEnquiries/Index");
            ViewBag.CanAdd = rights.CanAdd;
            ViewBag.CanView = rights.CanView;
            ViewBag.CanExport = rights.CanExport;
            ViewBag.CanReassign = rights.CanReassign;
            //Mantis Issue 24832
            ViewBag.CanAssign = rights.CanAssign;
            //End of Mantis Issue 24832
            // Rev 3.0
            ViewBag.CanBulkUpdate = rights.CanBulkUpdate;
            // End of Rev 3.0
            // Get Salesmanlist
            DataTable dtSals = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_CRUD_ENQUIRIES");
            proc.AddPara("@ACTION_TYPE", "GetSalesmanlist");
            dtSals = proc.GetTable();

            List<SalesmanUserAssign> Salesmanlist = new List<SalesmanUserAssign>();
            Salesmanlist = APIHelperMethods.ToModelList<SalesmanUserAssign>(dtSals);
            Dtls.SalesmanUserList = Salesmanlist;
            Dtls.SalesmanUserId = "";
            // End Get Salesmanlist

            return View(Dtls);
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
        //Rev 4.0
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult GetProvidedByList(string MODE)
        {
            List<ProvidedBy> ProvidedByList = new List<ProvidedBy>();
            {
                ProcedureExecute pro = new ProcedureExecute("PRC_CRUD_ENQUIRIES");
                pro.AddVarcharPara("@ACTION_TYPE", 100, "GetProvidedBy");
                pro.AddVarcharPara("@MODE", 20, MODE);
                DataTable addtab = pro.GetTable();               

                ProvidedByList = APIHelperMethods.ToModelList<ProvidedBy>(addtab);
                return Json(ProvidedByList, JsonRequestBehavior.AllowGet);

            }
            
        }
        //Rev 4.0 End
        public ActionResult GetEnquiryFrom()
        {
            try
            {
                List<GetEnquiryFrom> modelEnquiryFrom = new List<GetEnquiryFrom>();

                DataTable dtEnquiryFrom = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_CRUD_ENQUIRIES");
                proc.AddPara("@ACTION_TYPE", "GetEnquiryFrom");
                dtEnquiryFrom = proc.GetTable();

                modelEnquiryFrom = APIHelperMethods.ToModelList<GetEnquiryFrom>(dtEnquiryFrom);

                return PartialView("~/Areas/MYSHOP/Views/CRMEnquiries/_EnquiryFromPartial.cshtml", modelEnquiryFrom);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult PartialCRMEnquiriesGridList(CRMEnquiriesModel model)
        {
            try
            {
                EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/CRMEnquiries/Index");
                ViewBag.CanAdd = rights.CanAdd;
                ViewBag.CanView = rights.CanView;
                ViewBag.CanExport = rights.CanExport;
                ViewBag.CanReassign = rights.CanReassign;
                //Mantis Issue 24832
                ViewBag.CanAssign = rights.CanAssign;
                //End of Mantis Issue 24832
                // Rev 3.0
                ViewBag.CanBulkUpdate = rights.CanBulkUpdate;
                // End of Rev 3.0

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

                

                if (model.Is_PageLoad == "1")
                {


                    if (EnquiryFrom.Contains("IndiaMart"))
                    {
                        // IndiaMart
                        double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;
                        if (days <= 7)
                        {
                            getIndiaMartAPIData(datfrmat, dattoat);
                        }
                        
                    }

                    // Mantis Issue 24890  [This is needed for using in module FSM - CRM - Enquiries for Eurobond. Fetching of data from Mccoymart API will not take place from our Local Server. It will take place only in Live Server since data can be fetched from Mccoymart API only once for a particular date.]
                    //if (EnquiryFrom.Contains("MccoyMart"))
                    string FSM_MccoymartActive = ConfigurationManager.AppSettings["FSM_MccoymartActive"];
                    if (EnquiryFrom.Contains("MccoyMart") && FSM_MccoymartActive == "1")
                        // End of Mantis Issue 24890
                    {
                        double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;
                        if (days <= 7)
                        {
                            // MccoyMart
                            System.DateTime dtFromdate = Convert.ToDateTime(datfrmat);
                            System.DateTime dtTodate = Convert.ToDateTime(dattoat);

                            System.TimeSpan diffResult = dtTodate - dtFromdate;
                            System.DateTime dtParamDate = Convert.ToDateTime(datfrmat);

                            DataTable dtOutPut = new DataTable();
                            dtOutPut.Columns.Add("lead_id", typeof(string));
                            dtOutPut.Columns.Add("name", typeof(string));
                            dtOutPut.Columns.Add("email", typeof(string));
                            dtOutPut.Columns.Add("mobile", typeof(string));
                            dtOutPut.Columns.Add("location", typeof(string));
                            dtOutPut.Columns.Add("lead_for", typeof(string));
                            dtOutPut.Columns.Add("quantity", typeof(string));
                            dtOutPut.Columns.Add("glv", typeof(string));
                            dtOutPut.Columns.Add("required_for", typeof(string));
                            dtOutPut.Columns.Add("role", typeof(string));
                            dtOutPut.Columns.Add("stage_requirement", typeof(string));
                            dtOutPut.Columns.Add("description", typeof(string));
                            dtOutPut.Columns.Add("date", typeof(DateTime));

                            for (i = 0; i <= Convert.ToInt32(diffResult.Days); i++)
                            {
                                string strParamDate = Convert.ToString(dtParamDate.Year) + '-' + Convert.ToString(dtParamDate.Month).PadLeft(2, '0') + '-' + Convert.ToString(dtParamDate.Day).PadLeft(2, '0');


                                BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();
                                // Mantis Issue 24907
                                //Int32 cntData = oDBEngine.getCount("tbl_Import_MccoyMart", "convert(date,date)='" + strParamDate + "'");

                                //if (cntData == 0)
                                //{
                                //    getMccoyMartAPIData(strParamDate, dtOutPut);
                                //}
                                getMccoyMartAPIData(strParamDate, dtOutPut);
                                // Mantis Issue 24907

                                dtParamDate = dtParamDate.AddDays(1);
                            }

                            if (dtOutPut.Rows.Count > 0)
                            {
                                int user_id = Convert.ToInt32(Session["userid"]);

                                DataTable dtImport = new DataTable();
                                ProcedureExecute proc1 = new ProcedureExecute("Proc_Import_MccoyMart");
                                proc1.AddPara("@dtOutPut", dtOutPut);
                                proc1.AddPara("@user_id", user_id);
                                //proc1.AddPara("@Action", Action);
                                //proc1.AddPara("@Errortext", omodel1.jsonError);
                                //proc1.AddPara("@ReturnValue", SqlDbType.Char, 50);
                                proc1.AddVarcharPara("@ReturnValue", 50, "", QueryParameterDirection.Output);
                                dtImport = proc1.GetTable();
                                string output = Convert.ToString(proc1.GetParaValue("@ReturnValue"));
                            }
                        }
                        
                    }

                    // Mantis Issue 24936
                    if (EnquiryFrom.Contains("IndiaMart (ARCHER)"))
                    {
                        // IndiaMart
                        double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;
                        if (days <= 7)
                        {
                            getIndiaMartArcherAPIData(datfrmat, dattoat);
                        }

                    }
                    // End of Mantis Issue 24936

                    //int user_id = Convert.ToInt32(HttpContext.Current.Session["userid"]);

                    // DataTable dt = new DataTable();
                    
                }
                //REV 6.0
                //GetCRMEnquiriesListing(EnquiryFrom, datfrmat, dattoat);
                GetCRMEnquiriesListing(EnquiryFrom, datfrmat, dattoat, model.Is_PageLoad);
                //REV 6.0 END

                model.Is_PageLoad = "Ispageload";

                return PartialView("PartialCRMEnquiriesGridList", GetCRMEnquiriesDetails(Is_PageLoad));

            }
            catch(Exception ex)
            {
                // Rev Sanchita
                //return RedirectToAction("Logout", "Login", new { Area = "" });
                throw ex;
                // End of Rev Sanchita

            }
           
        }
        //REV 6.0 
        //public void GetCRMEnquiriesListing(string EnquiryFrom, string FromDate, string ToDate)
        public void GetCRMEnquiriesListing(string EnquiryFrom, string FromDate, string ToDate,string IsPageLoad)
        //REV 6.0 END
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
                ProcedureExecute proc = new ProcedureExecute("PRC_ENQUIRIES_LISTING");
                proc.AddPara("@USERID", Convert.ToInt32(user_id));
                proc.AddPara("@ENQUIRIESFROM", EnquiryFrom);
                proc.AddPara("@FROMDATE", FromDate);
                proc.AddPara("@TODATE", ToDate);
                //REV 6.0 
                proc.AddPara("@ISPAGELOAD", IsPageLoad);
                //REV 6.0 END
                dt = proc.GetTable();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable GetCRMEnquiriesDetails(string Is_PageLoad)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);
            //Mantis Issue 24816
            DataTable dtColmn = GetPageRetention(Session["userid"].ToString(), "CRM Enquiries");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            }
            //End of Mantis Issue 24816
            if (Is_PageLoad != "is_pageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.ENQUIRIES_LISTING1s
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.SEQ
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.ENQUIRIES_LISTING1s
                        where d.USERID == Convert.ToInt32(Userid) && d.SEQ == 11111111119
                        orderby d.SEQ
                        select d;
                return q;
            }

            //if (Is_PageLoad != "Ispageload")
            //{
            //    CRM.Models.DataContext.CRMClassDataContext DC = new CRM.Models.DataContext.CRMClassDataContext(connectionString);
            //    var q = from d in DC.ENQUIRIES_LISTING1s
            //            where d.USERID == Convert.ToInt32(Userid)
            //            orderby d.SEQ
            //            select d;
            //    return q;

            //}
            //else
            //{
            //    //CRM.Models.DataContext.CRMClassDataContext DC = new CRM.Models.DataContext.CRMClassDataContext(connectionString);
            //    //var q = from d in DC.ENQUIRIES_LISTING1s
            //    //        where d.USERID == Convert.ToInt32(Userid) && d.SEQ == 11111111119
            //    //        orderby d.SEQ descending
            //    //        select d;
            //    //return q;
            //    return null;
            //}

        }

        public ActionResult ShowEnquiriesDetails(String _enquiryId)
        {
            try
            {
                //DataSet _getenq = new DataSet();
                CRMEnquiriesModel _apply = new CRMEnquiriesModel();
                //EnquiriesDet _header = new EnquiriesDet();
                try
                {
                    ExecProcedure execProc = new ExecProcedure();

                    DataTable _getenq = new DataTable();
                    ProcedureExecute proc1 = new ProcedureExecute("PRC_CRUD_ENQUIRIES");
                    proc1.AddPara("@CRM_ID", _enquiryId);
                    proc1.AddPara("@ACTION_TYPE", "Edit");
                    _getenq = proc1.GetTable();

                   
                    if (_getenq.Rows.Count > 0)
                    {
                        _apply.CRM_ID = _enquiryId;
                        _apply.Customer_Name = _getenq.Rows[0]["Customer_Name"].ToString();
                        _apply.Contact_Person = _getenq.Rows[0]["Contact_Person"].ToString();
                        _apply.Date = Convert.ToDateTime(_getenq.Rows[0]["Date"]);
                        _apply.PhoneNo = _getenq.Rows[0]["PhoneNo"].ToString();
                        _apply.Email = _getenq.Rows[0]["Email"].ToString();
                        _apply.Location = _getenq.Rows[0]["Location"].ToString();
                        _apply.Product_Required = _getenq.Rows[0]["Product_Required"].ToString();
                        _apply.Qty = _getenq.Rows[0]["Qty"].ToString();
                        _apply.Order_Value = Convert.ToDecimal(_getenq.Rows[0]["Order_Value"]);
                        _apply.Enq_Details = _getenq.Rows[0]["Enq_Details"].ToString();
                        _apply.Provided_By = _getenq.Rows[0]["vend_type"].ToString();
                        _apply.UOM = _getenq.Rows[0]["UOM"].ToString();
                        // Rev 1.0
                        _apply.txtDate = _getenq.Rows[0]["txtDate"].ToString();
                        // End of Rev 1.0
                        //_apply.SUPERVISOR = Convert.ToBoolean(_getenq.Rows[0]["Supervisor"]);
                        //_apply.SALESMAN = Convert.ToBoolean(_getenq.Rows[0]["salesman"]);
                        //_apply.VERIFY = Convert.ToBoolean(_getenq.Rows[0]["verify"]);

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

        Random _random = new Random();

        public void getIndiaMartAPIData(string FromDate, string ToDate)
        {
         
            List<IndiamartModelClass> omodel = new List<IndiamartModelClass>();
            List<IndiamartModelErrorClass> indmdlerror = new List<IndiamartModelErrorClass>();
            

            DataTable dt = new DataTable();
            SqlCommand sqlcmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                    | SecurityProtocolType.Tls11
                    | SecurityProtocolType.Tls12
                    | SecurityProtocolType.Ssl3;

            string output = string.Empty;
            string strerrormessage = "";
            string mobileno = "";
            string Action = "";
            // Mantis Issue 24890
            int TotalCount = 0;
            // End of Mantis Issue 24890

            List<IndiamartModelErrorClass> idnMart = new List<IndiamartModelErrorClass>();


            DataTable dtobsql = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PROC_GET_VENDOR_CRMCONFIGURATION");
            proc.AddPara("@EnquiryFrom", "IndiaMart");
            proc.AddPara("@VENDORID", "1");
            proc.AddPara("@FromDate", FromDate);
            proc.AddPara("@ToDate", ToDate);
            dtobsql = proc.GetTable();


            //ReadWriteMasterDatabaseBL obj = new ReadWriteMasterDatabaseBL();

            //// String con = Convert.ToString(Session["ERPConnection"]);
            //String con = obj.GetDefaultConnectionStringWithoutSession();

            //SqlConnection sqlcon = new SqlConnection(con);

            //sqlcon.Open();
            //sqlcmd = new SqlCommand("Proc_Get_Vendor_CrmConfiguration", sqlcon);
            //sqlcmd.Parameters.Add("@vendorID", "1,4");

            //sqlcmd.CommandType = CommandType.StoredProcedure;
            //SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            //da.Fill(dtobsql);
            //sqlcon.Close();
            string URL = "";
            try
            {
                if (dtobsql != null)
                {
                    if (dtobsql.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtobsql.Rows.Count; i++)
                        {
                            IndiamartModelErrorClass objErr = new IndiamartModelErrorClass();
                            URL = Convert.ToString(dtobsql.Rows[i][0]);
                            mobileno = Convert.ToString(dtobsql.Rows[i][1]);

                            objErr.MobileNo = mobileno;

                            using (var webClient = new System.Net.WebClient())
                            {
                                var json = webClient.DownloadString(URL);
                                // Now parse with JSON.Net


                                JavaScriptSerializer ser = new JavaScriptSerializer();

                                // Mantis Issue 24890
                                //  List<IndiamartModelClass> indiamartlist = ser.Deserialize<List<IndiamartModelClass>>(json);
                               
                                //if (indiamartlist.Count > 1)
                                //{
                                //    Action = "Success";
                                //    objErr.jsonError = "Success";
                                //    objErr.ErrorMessage = Action;
                                //    omodel = new List<IndiamartModelClass>();
                                //    foreach (var s2 in indiamartlist)
                                //    {
                                //        string rand = GenerateRandomNo();
                                //        omodel.Add(new IndiamartModelClass()
                                //        {
                                //            Indiamart_Id = "Indiamart" + "_" + DateTime.Now.Ticks.ToString() + "_" + rand + "_" + Guid.NewGuid(),
                                //            Rn = s2.Rn,
                                //            QUERY_ID = s2.QUERY_ID,
                                //            QTYPE = s2.QTYPE,
                                //            SENDERNAME = s2.SENDERNAME,
                                //            SENDEREMAIL = s2.SENDEREMAIL,
                                //            SUBJECT = s2.SUBJECT,
                                //            DATE_RE = s2.DATE_RE,
                                //            DATE_R = s2.DATE_R,
                                //            DATE_TIME_RE = s2.DATE_TIME_RE,
                                //            GLUSR_USR_COMPANYNAME = s2.GLUSR_USR_COMPANYNAME,
                                //            READ_STATUS = s2.READ_STATUS,
                                //            SENDER_GLUSR_USR_ID = s2.SENDER_GLUSR_USR_ID,
                                //            MOB = s2.MOB,
                                //            COUNTRY_FLAG = s2.COUNTRY_FLAG,
                                //            QUERY_MODID = s2.QUERY_MODID,
                                //            LOG_TIME = s2.LOG_TIME,
                                //            QUERY_MODREFID = s2.QUERY_MODREFID,
                                //            DIR_QUERY_MODREF_TYPE = s2.DIR_QUERY_MODREF_TYPE,
                                //            ORG_SENDER_GLUSR_ID = s2.ORG_SENDER_GLUSR_ID,
                                //            ENQ_MESSAGE = s2.ENQ_MESSAGE,
                                //            ENQ_ADDRESS = s2.ENQ_ADDRESS,
                                //            ENQ_CALL_DURATION = s2.ENQ_CALL_DURATION,
                                //            ENQ_RECEIVER_MOB = s2.ENQ_RECEIVER_MOB,
                                //            ENQ_CITY = s2.ENQ_CITY,
                                //            ENQ_STATE = s2.ENQ_STATE,
                                //            PRODUCT_NAME = s2.PRODUCT_NAME,
                                //            COUNTRY_ISO = s2.COUNTRY_ISO,
                                //            EMAIL_ALT = s2.EMAIL_ALT,
                                //            MOBILE_ALT = s2.MOBILE_ALT,
                                //            PHONE = s2.PHONE,
                                //            PHONE_ALT = s2.PHONE_ALT,
                                //            IM_MEMBER_SINCE = s2.IM_MEMBER_SINCE,
                                //            TOTAL_COUNT = s2.TOTAL_COUNT


                                //        });

                                //    }
                                //    objErr.Indiamart = omodel;

                                //}
                                //else
                                //{
                                //    Action = "Error";
                                //    strerrormessage = json.ToString();
                                //    objErr.ErrorMessage = Action;
                                //    objErr.jsonError = strerrormessage;

                                //}

                                IndiamartModelClassKey indiamartListKey = ser.Deserialize<IndiamartModelClassKey>(json);

                                TotalCount = indiamartListKey.RESPONSE.Count;

                                if (indiamartListKey.STATUS == "SUCCESS" && TotalCount > 1)
                                {
                                    Action = "Success";
                                    objErr.jsonError = "Success";
                                    objErr.ErrorMessage = Action;
                                    omodel = new List<IndiamartModelClass>();
                                    foreach (var s2 in indiamartListKey.RESPONSE)
                                    {
                                        string rand = GenerateRandomNo();
                                        omodel.Add(new IndiamartModelClass()
                                        {
                                            Indiamart_Id = "Indiamart" + "_" + DateTime.Now.Ticks.ToString() + "_" + rand + "_" + Guid.NewGuid(),
                                            UNIQUE_QUERY_ID = s2.UNIQUE_QUERY_ID,
                                            QUERY_TYPE = s2.QUERY_TYPE,
                                            QUERY_TIME = s2.QUERY_TIME,
                                            SENDER_NAME = s2.SENDER_NAME,
                                            SENDER_MOBILE = s2.SENDER_MOBILE,
                                            SENDER_EMAIL = s2.SENDER_EMAIL,
                                            SENDER_COMPANY = s2.SENDER_COMPANY,
                                            SENDER_ADDRESS = s2.SENDER_ADDRESS,
                                            SENDER_CITY = s2.SENDER_CITY,
                                            SENDER_STATE = s2.SENDER_STATE,
                                            SENDER_COUNTRY_ISO = s2.SENDER_COUNTRY_ISO,
                                            SENDER_MOBILE_ALT = s2.SENDER_MOBILE_ALT,
                                            SENDER_EMAIL_ALT = s2.SENDER_EMAIL_ALT,
                                            QUERY_PRODUCT_NAME = s2.QUERY_PRODUCT_NAME,
                                            QUERY_MESSAGE = s2.QUERY_MESSAGE,
                                            CALL_DURATION = s2.CALL_DURATION,
                                            RECEIVER_MOBILE = s2.RECEIVER_MOBILE
                                        });

                                    }
                                    objErr.Indiamart = omodel;

                                }
                                else
                                {
                                    Action = "Error";
                                    strerrormessage = json.ToString();
                                    objErr.ErrorMessage = Action;
                                    objErr.jsonError = strerrormessage;

                                }

                                // End of Mantis Issue 24890
                            }

                            idnMart.Add(objErr);
                        }

                    }

                    foreach (IndiamartModelErrorClass omodel1 in idnMart)
                    {
                        string JsonXML = "";
                        if (omodel1.Indiamart != null)
                        {
                            JsonXML = XmlConversion.ConvertToXml(omodel1.Indiamart, 0);
                        }
                        else
                        {
                            JsonXML = "";
                        }


                        DataTable dtImport = new DataTable();
                        // Mantis Issue 24890
                        //ProcedureExecute proc1 = new ProcedureExecute("Proc_Import_IndiaMart");
                        ProcedureExecute proc1 = new ProcedureExecute("FSM_Proc_Import_IndiaMart");
                        // End of Mantis Issue 24890
                        proc1.AddPara("@JsonXML", JsonXML);
                        proc1.AddPara("@MobileNo", omodel1.MobileNo);
                        proc1.AddPara("@Action", Action);
                        proc1.AddPara("@Errortext", omodel1.jsonError);
                        // Mantis Issue 24890
                        proc1.AddPara("@TotalCount", TotalCount);
                        proc1.AddPara("@Vendor_Name", "IndiaMart");
                        // End of Mantis Issue 24890
                        //proc1.AddPara("@ReturnValue", SqlDbType.Char, 50);
                        proc1.AddVarcharPara("@ReturnValue", 50, "", QueryParameterDirection.Output);
                        dtImport = proc1.GetTable();
                        output = Convert.ToString(proc1.GetParaValue("@ReturnValue"));


                        //DataTable dtImport = new DataTable();
                        ////SqlCommand sqlcmd = new SqlCommand();
                        ////SqlConnection sqlcon = new SqlConnection(con);
                        //sqlcon.Open();


                        //sqlcmd = new SqlCommand("Proc_Import_IndiaMart", sqlcon);
                        //sqlcmd.Parameters.Add("@JsonXML", JsonXML);
                        //sqlcmd.Parameters.Add("@MobileNo", omodel.MobileNo);
                        //sqlcmd.Parameters.Add("@Action", Action);
                        //sqlcmd.Parameters.Add("@Errortext", omodel.jsonError);
                        //sqlcmd.Parameters.Add("@ReturnValue", SqlDbType.Char, 50);
                        //sqlcmd.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;

                        //sqlcmd.CommandType = CommandType.StoredProcedure;
                        //da = new SqlDataAdapter(sqlcmd);
                        //da.Fill(dtImport);
                        //sqlcmd.Dispose();
                        //output = (string)sqlcmd.Parameters["@ReturnValue"].Value;
                        //sqlcon.Close();

                        //if (dtImport.Rows.Count > 0)
                        //{

                        //}
                    }




                    //return Json("Success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                   // return Json("Duplicate", JsonRequestBehavior.AllowGet);
                    //  string URL = "http://mapi.indiamart.com/wservce/enquiry/listing/GLUSR_MOBILE/7042445112/GLUSR_MOBILE_KEY/NzA0MjQ0NTExMiMxMDM1OTU4MA==/";
                }

            }
            catch (Exception ex)
            {
                DataTable dtError = new DataTable();
                // Mantis Issue 24890
                //ProcedureExecute proc2 = new ProcedureExecute("Proc_Import_IndiaMart");
                ProcedureExecute proc2 = new ProcedureExecute("FSM_Proc_Import_IndiaMart");
                // End of Mantis Issue 24890
                proc2.AddPara("@MobileNo", "");
                proc2.AddPara("@Action", "Error");
                // Mantis Issue 24936
                proc2.AddPara("@Vendor_Name", "IndiaMart");
                // End of Mantis Issue 24936
                proc2.AddPara("@Errortext", ex.ToString());
                dtError = proc2.GetTable();
                
                //DataTable dt = new DataTable();
                ////SqlCommand sqlcmd = new SqlCommand();
                ////SqlConnection sqlcon = new SqlConnection(con);
                //sqlcon.Open();


                //sqlcmd = new SqlCommand("Proc_Import_IndiaMart", sqlcon);
                //// sqlcmd.Parameters.Add("@JsonXML", JsonXML);
                //sqlcmd.Parameters.Add("@MobileNo", "");
                //sqlcmd.Parameters.Add("@Action", "Error");
                //sqlcmd.Parameters.Add("@Errortext", ex.ToString());

                //sqlcmd.CommandType = CommandType.StoredProcedure;
                //da = new SqlDataAdapter(sqlcmd);
                //da.Fill(dt);
                //sqlcon.Close();

               // return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }
        public string GenerateRandomNo()
        {
            return _random.Next(0, 9999).ToString("D4");
        }
        public class XmlConversion
        {
            #region ******************************************** Xml Conversion  ********************************************
            public static string ConvertToXml<T>(List<T> table, int metaIndex = 0)
            {
                XmlDocument ChoiceXML = new XmlDocument();
                ChoiceXML.AppendChild(ChoiceXML.CreateElement("root"));
                Type temp = typeof(T);

                foreach (var item in table)
                {
                    XmlElement element = ChoiceXML.CreateElement("data");

                    foreach (PropertyInfo pro in temp.GetProperties())
                    {
                        element.AppendChild(ChoiceXML.CreateElement(pro.Name)).InnerText = Convert.ToString(item.GetType().GetProperty(pro.Name).GetValue(item, null));
                    }
                    ChoiceXML.DocumentElement.AppendChild(element);
                }

                return ChoiceXML.InnerXml.ToString();
            }


            #endregion

        }

        public void getMccoyMartAPIData(string paramDate, DataTable dtOutPut)
        {
            String ServcEntityUser = ConfigurationManager.AppSettings["ServcEntityUser"];
            String ServcEntityPass = ConfigurationManager.AppSettings["ServcEntityPass"];

            // STEP 1: FISRT API CALL TO GENERATE TOKEN
            //******************************************
            var url = "https://api.mccoymart.com/global/generate-token";
            string _ContentType = "application/json";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_ContentType));
           //client.DefaultRequestHeaders.Add("token-auth", "");
           //client.DefaultRequestHeaders.Add("USER", "");
           //client.DefaultRequestHeaders.Add("PASSWORD", "");

            var data = new Dictionary<string, string>
                {
                    {"user" , "eurobond"} ,  {"key","eyJhbGciOiJIHHUM"}
                };
               

            var content = new FormUrlEncodedContent(data);
            // Mantis Issue 24907
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            // End of Mantis Issue 24907
            var response = client.PostAsync(url, content).Result;
            
            var content1 = response.Content.ReadAsStringAsync().Result;
            MacoyModelClassKey dataset1 = JsonConvert.DeserializeObject<MacoyModelClassKey>(content1);

            var strToken = dataset1.data.GetType().GetProperties()[2].GetValue(dataset1.data, null);

            if (strToken != null && strToken.ToString().Length > 0)
            {
                // STEP 2: SECOND API CALL TO GET LEAD
                //******************************************
                var url2 = "https://api.mccoymart.com/global/get-lead-notification";
                string _ContentType2 = "application/json";
                var client2 = new HttpClient();
                client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_ContentType2));
               // client2.DefaultRequestHeaders.Add("Authorization", strToken.ToString());
                client2.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken.ToString());
                //client.DefaultRequestHeaders.Add("USER", "");
                //client.DefaultRequestHeaders.Add("PASSWORD", "");

                var data2 = new Dictionary<string, string>
                {
                    {"search" , paramDate} ,  {"type","date"}
                };


                var content2 = new FormUrlEncodedContent(data2);
                var response2 = client2.PostAsync(url2, content2).Result;
                var content12 = response2.Content.ReadAsStringAsync().Result;
                MacoyModelClassLead dataset2 = JsonConvert.DeserializeObject<MacoyModelClassLead>(content12);

                if (dataset2.message == "success")
                {
                //if (content12.Contains("no data"))
                //{
                //    // Loop
                //}
                //else
                //{
                    if (dataset2.data.Count > 0)
                    {
                        for(int i=0 ; i<dataset2.data.Count; i++)
                        {
                            // The format of dataset2.data[0].date returned from API is "2019-02-03T13:20:41.000Z"
                            dataset2.data[0].date = dataset2.data[i].date.Replace("T"," ");
                            dataset2.data[0].date = dataset2.data[i].date.Replace("Z", "");
                            

                            DataRow drOutput = dtOutPut.NewRow();
                            drOutput["lead_id"] = dataset2.data[i].lead_id;
                            drOutput["name"] = dataset2.data[i].name;
                            drOutput["email"] = dataset2.data[i].email;
                            drOutput["mobile"] = dataset2.data[i].mobile;
                            drOutput["location"] = dataset2.data[i].location;
                            drOutput["lead_for"] = dataset2.data[i].lead_for;
                            drOutput["quantity"] = dataset2.data[i].quantity;
                            drOutput["glv"] = dataset2.data[0].glv;
                            drOutput["required_for"] = dataset2.data[i].required_for;
                            drOutput["stage_requirement"] = dataset2.data[i].stage_requirement;
                            drOutput["description"] = dataset2.data[i].description;
                            drOutput["date"] = dataset2.data[i].date;
                            dtOutPut.Rows.Add(drOutput);

                        }

                    }

                }
                else
                {
                    int user_id = Convert.ToInt32(Session["userid"]);

                    DataTable dtImport = new DataTable();
                    ProcedureExecute proc1 = new ProcedureExecute("Proc_Import_MccoyMart");
                    proc1.AddPara("@errorMessage", dataset2.message);
                    proc1.AddPara("@fetchDate", paramDate);
                    proc1.AddPara("@user_id", user_id);
                    dtImport = proc1.GetTable();
                }


            }

            


        }
        public static DataTable ObjectToData(object o)
        {
            DataTable dt = new DataTable("OutputData");
            if (o != null)
            {
                DataRow dr = dt.NewRow();
                dt.Rows.Add(dr);

                o.GetType().GetProperties().ToList().ForEach(f =>
                {
                    try
                    {
                        f.GetValue(o, null);
                        dt.Columns.Add(f.Name, f.PropertyType);
                        dt.Rows[0][f.Name] = f.GetValue(o, null);
                    }
                    catch { }
                });
            }
            return dt;
        }

        // Mantis Issue 24936
        public void getIndiaMartArcherAPIData(string FromDate, string ToDate)
        {

            List<IndiamartArcherModelClass> omodel = new List<IndiamartArcherModelClass>();
            List<IndiamartArcherModelErrorClass> indmdlerror = new List<IndiamartArcherModelErrorClass>();


            DataTable dt = new DataTable();
            SqlCommand sqlcmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                    | SecurityProtocolType.Tls11
                    | SecurityProtocolType.Tls12
                    | SecurityProtocolType.Ssl3;

            string output = string.Empty;
            string strerrormessage = "";
            string mobileno = "";
            string Action = "";
            int TotalCount = 0;

            List<IndiamartArcherModelErrorClass> idnMartArcher = new List<IndiamartArcherModelErrorClass>();


            DataTable dtobsql = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PROC_GET_VENDOR_CRMCONFIGURATION");
            proc.AddPara("@EnquiryFrom", "IndiaMartArcher");
            proc.AddPara("@VENDORID", "2");
            proc.AddPara("@FromDate", FromDate);
            proc.AddPara("@ToDate", ToDate);
            dtobsql = proc.GetTable();


            string URL = "";
            try
            {
                if (dtobsql != null)
                {
                    if (dtobsql.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtobsql.Rows.Count; i++)
                        {
                            IndiamartArcherModelErrorClass objErr = new IndiamartArcherModelErrorClass();
                            URL = Convert.ToString(dtobsql.Rows[i][0]);
                            mobileno = Convert.ToString(dtobsql.Rows[i][1]);

                            objErr.MobileNo = mobileno;

                            using (var webClient = new System.Net.WebClient())
                            {
                                var json = webClient.DownloadString(URL);
                                // Now parse with JSON.Net


                                JavaScriptSerializer ser = new JavaScriptSerializer();



                                IndiamartArcherModelClassKey indiamartArcherListKey = ser.Deserialize<IndiamartArcherModelClassKey>(json);

                                TotalCount = indiamartArcherListKey.RESPONSE.Count;

                                if (indiamartArcherListKey.STATUS == "SUCCESS" && TotalCount > 1)
                                {
                                    Action = "Success";
                                    objErr.jsonError = "Success";
                                    objErr.ErrorMessage = Action;
                                    omodel = new List<IndiamartArcherModelClass>();
                                    foreach (var s2 in indiamartArcherListKey.RESPONSE)
                                    {
                                        string rand = GenerateRandomNo();
                                        omodel.Add(new IndiamartArcherModelClass()
                                        {
                                            Indiamart_Id = "IndiamartArcher" + "_" + DateTime.Now.Ticks.ToString() + "_" + rand + "_" + Guid.NewGuid(),
                                            UNIQUE_QUERY_ID = s2.UNIQUE_QUERY_ID,
                                            QUERY_TYPE = s2.QUERY_TYPE,
                                            QUERY_TIME = s2.QUERY_TIME,
                                            SENDER_NAME = s2.SENDER_NAME,
                                            SENDER_MOBILE = s2.SENDER_MOBILE,
                                            SENDER_EMAIL = s2.SENDER_EMAIL,
                                            SENDER_COMPANY = s2.SENDER_COMPANY,
                                            SENDER_ADDRESS = s2.SENDER_ADDRESS,
                                            SENDER_CITY = s2.SENDER_CITY,
                                            SENDER_STATE = s2.SENDER_STATE,
                                            SENDER_COUNTRY_ISO = s2.SENDER_COUNTRY_ISO,
                                            SENDER_MOBILE_ALT = s2.SENDER_MOBILE_ALT,
                                            SENDER_EMAIL_ALT = s2.SENDER_EMAIL_ALT,
                                            QUERY_PRODUCT_NAME = s2.QUERY_PRODUCT_NAME,
                                            QUERY_MESSAGE = s2.QUERY_MESSAGE,
                                            CALL_DURATION = s2.CALL_DURATION,
                                            RECEIVER_MOBILE = s2.RECEIVER_MOBILE
                                        });

                                    }
                                    objErr.IndiamartArcher = omodel;

                                }
                                else
                                {
                                    Action = "Error";
                                    strerrormessage = json.ToString();
                                    objErr.ErrorMessage = Action;
                                    objErr.jsonError = strerrormessage;

                                }
                            }

                            idnMartArcher.Add(objErr);
                        }

                    }

                    foreach (IndiamartArcherModelErrorClass omodel1 in idnMartArcher)
                    {
                        string JsonXML = "";
                        if (omodel1.IndiamartArcher != null)
                        {
                            JsonXML = XmlConversion.ConvertToXml(omodel1.IndiamartArcher, 0);
                        }
                        else
                        {
                            JsonXML = "";
                        }


                        DataTable dtImport = new DataTable();
                        ProcedureExecute proc1 = new ProcedureExecute("FSM_Proc_Import_IndiaMart");
                        proc1.AddPara("@JsonXML", JsonXML);
                        proc1.AddPara("@MobileNo", omodel1.MobileNo);
                        proc1.AddPara("@Action", Action);
                        proc1.AddPara("@Errortext", omodel1.jsonError);
                        proc1.AddPara("@TotalCount", TotalCount);
                        proc1.AddPara("@Vendor_Name", "IndiaMart (ARCHER)");
                        proc1.AddVarcharPara("@ReturnValue", 50, "", QueryParameterDirection.Output);
                        dtImport = proc1.GetTable();
                        output = Convert.ToString(proc1.GetParaValue("@ReturnValue"));


                    }

                }
                //else
                //{
                //    // return Json("Duplicate", JsonRequestBehavior.AllowGet);
                //    //  string URL = "http://mapi.indiamart.com/wservce/enquiry/listing/GLUSR_MOBILE/7042445112/GLUSR_MOBILE_KEY/NzA0MjQ0NTExMiMxMDM1OTU4MA==/";
                //}

            }
            catch (Exception ex)
            {
                DataTable dtError = new DataTable();
                ProcedureExecute proc2 = new ProcedureExecute("FSM_Proc_Import_IndiaMart");
                 proc2.AddPara("@MobileNo", "");
                proc2.AddPara("@Action", "Error");
                proc2.AddPara("@Vendor_Name", "IndiaMart (ARCHER)");
                proc2.AddPara("@Errortext", ex.ToString());
                dtError = proc2.GetTable();

            }

        }
        // End of Mantis Issue 24936

        //public ActionResult GetPartyDetailsList(AddPartyDetailsModel model)
        //{
        //    try
        //    {
        //        string Is_PageLoad = string.Empty;
        //        DataTable dt = new DataTable();
        //        if (model.Fromdate == null)
        //        {
        //            model.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
        //        }

        //        if (model.Todate == null)
        //        {
        //            model.Todate = DateTime.Now.ToString("dd-MM-yyyy");
        //        }

        //        if (model.Is_PageLoad != "1") Is_PageLoad = "Ispageload";

        //        ViewData["ModelData"] = model;

        //        string datfrmat = model.Fromdate.Split('-')[2] + '-' + model.Fromdate.Split('-')[1] + '-' + model.Fromdate.Split('-')[0];
        //        string dattoat = model.Todate.Split('-')[2] + '-' + model.Todate.Split('-')[1] + '-' + model.Todate.Split('-')[0];
        //        string Userid = Convert.ToString(Session["userid"]);

        //        string state = "";
        //        int i = 1;
        //        if (model.StateIds != null && model.StateIds.Count > 0)
        //        {
        //            foreach (string item in model.StateIds)
        //            {
        //                if (i > 1)
        //                    state = state + "," + item;
        //                else
        //                    state = item;
        //                i++;
        //            }
        //        }

        //        string empcode = "";
        //        int k = 1;
        //        if (model.empcode != null && model.empcode.Count > 0)
        //        {
        //            foreach (string item in model.empcode)
        //            {
        //                if (k > 1)
        //                    empcode = empcode + "," + item;
        //                else
        //                    empcode = item;
        //                k++;
        //            }
        //        }

        //        //Rev Debashis
        //        //double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;
        //        //if (days <= 30)
        //        //{
        //        //    dt = obj.GetReportPartyDetails(datfrmat, dattoat, Userid, state, empcode, model.IS_ReAssignedDate);
        //        //}
        //        dt = obj.GetReportPartyDetails(datfrmat, dattoat, Userid, state, empcode, model.IS_ReAssignedDate);
        //        //End of Rev Debashis
        //        return Json(Userid, JsonRequestBehavior.AllowGet);
        //    }
        //    catch
        //    {
        //        return RedirectToAction("Logout", "Login", new { Area = "" });
        //    }
        //}


        [HttpPost]
        public JsonResult Apply(CRMEnquiriesModel apply, string uniqueid)
        {
            string ReturnCode = "";
            string ReturnMsg = "";
            //_enquiries = new EnquiriesRepo();
            try
            {
                string Userid = Convert.ToString(Session["userid"]);

                DataTable dtImport = new DataTable();
                ProcedureExecute proc1 = new ProcedureExecute("PRC_CRUD_ENQUIRIES");
                proc1.AddPara("@USERID", Userid);
                proc1.AddPara("@ACTION_TYPE", apply.Action_type);

                proc1.AddPara("@DATE", apply.Date);
                proc1.AddPara("@CUSTNAME", apply.Customer_Name);
                proc1.AddPara("@CONTACTPERSON", apply.Contact_Person);
                proc1.AddPara("@PHONENO", apply.PhoneNo);
                proc1.AddPara("@EMAIL", apply.Email);
                proc1.AddPara("@LOCATION", apply.Location);
                proc1.AddPara("@PRODUCTREQUIRED", apply.Product_Required);
                proc1.AddPara("@QTY", apply.Qty);
                proc1.AddPara("@ORDER_VALUE", apply.Order_Value);
                proc1.AddPara("@VEND_TYPE", apply.Provided_By);
                proc1.AddPara("@ENQ_DETAILS", apply.Enq_Details);
                proc1.AddPara("@UOM", apply.UOM);
                if (!String.IsNullOrEmpty(uniqueid))
                {
                    proc1.AddPara("@CRM_ID", (uniqueid));
                }
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

        //public ActionResult PartyChangeStatus(String ShopCode)
        //{
        //    try
        //    {
        //        int k = obj.ShopActiveInactive(ShopCode);

        //        return Json(k, JsonRequestBehavior.AllowGet);
        //    }
        //    catch
        //    {
        //        return RedirectToAction("Logout", "Login", new { Area = "" });
        //    }
        //}

        //[HttpPost]
        //public ActionResult AddPartyDetails()
        //{
        //    try
        //    {
        //        int k = 0;
        //        HttpFileCollectionBase files = Request.Files;

        //        var obj = Request.Form;
        //        String Shop_code = Convert.ToString(obj["Shop_code"]);
        //        int State = Convert.ToInt32(obj["State"]);
        //        string dobstr = Convert.ToString(obj["dobstr"]);
        //        string date_aniversarystr = Convert.ToString(obj["date_aniversarystr"]);
        //        int type = Convert.ToInt32(obj["type"]);
        //        string AssignedTo = Convert.ToString(obj["AssignedTo"]);
        //        string Party_Name = Convert.ToString(obj["Party_Name"]);
        //        string Party_Code = Convert.ToString(obj["Party_Code"]);

        //        string Address = Convert.ToString(obj["Address"]);
        //        string Pin_Code = Convert.ToString(obj["Pin_Code"]);
        //        string owner_name = Convert.ToString(obj["owner_name"]);
        //        string owner_contact_no = Convert.ToString(obj["owner_contact_no"]);
        //        string Alternate_Contact = Convert.ToString(obj["Alternate_Contact"]);
        //        string owner_email = Convert.ToString(obj["owner_email"]);
        //        string ShopStatus = Convert.ToString(obj["ShopStatus"]);
        //        int EntyType = Convert.ToInt32(obj["EntyType"]);
        //        string Owner_PAN = Convert.ToString(obj["Owner_PAN"]);
        //        string Owner_Adhar = Convert.ToString(obj["Owner_Adhar"]);

        //        string Remarks = Convert.ToString(obj["Remarks"]);
        //        long NewUser = Convert.ToInt64(obj["NewUser"]);
        //        long OldUser = Convert.ToInt64(obj["OldUser"]);
        //        string shop_lat = Convert.ToString(obj["shop_lat"]);
        //        string shop_long = Convert.ToString(obj["shop_long"]);

        //        String ImagePath = System.Configuration.ConfigurationSettings.AppSettings["Path"];
        //        //for (int i = 0; i < files.Count; i++)
        //        //{
        //        //    String FileName = String.Empty;

        //        //    HttpPostedFileBase file = files[i];
        //        //    string fname;

        //        //    // Checking for Internet Explorer  
        //        //    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
        //        //    {
        //        //        string[] testfiles = file.FileName.Split(new char[] { '\\' });
        //        //        fname = testfiles[testfiles.Length - 1];
        //        //        FileName = fname;
        //        //    }
        //        //    else
        //        //    {
        //        //        fname = file.FileName;
        //        //        FileName = fname;
        //        //    }

        //        //fname = Path.Combine(ImagePath, fname);
        //        //file.SaveAs(fname);

        //        //byte[] filebyte = null;

        //        //using (StreamReader reader = new StreamReader(file.InputStream))
        //        //{
        //        //    filebyte = Encoding.UTF8.GetBytes(reader.ReadToEnd());
        //        //        reader.Close();
        //        //}



        //        //FtpWebRequest reqFTP;
        //        //reqFTP = (FtpWebRequest)FtpWebRequest.Create(ImagePath);
        //        //reqFTP.KeepAlive = true;
        //        //reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
        //        //reqFTP.UseBinary = true;s
        //        //reqFTP.ContentLength = filebyte.Length;

        //        //int buffLength = 2048;
        //        //byte[] buff = new byte[buffLength];
        //        //MemoryStream ms = new MemoryStream(filebyte);

        //        //try
        //        //{
        //        //    int contenctLength;
        //        //    using (Stream strm = reqFTP.GetRequestStream())
        //        //    {
        //        //        contenctLength = ms.Read(buff, 0, buffLength);
        //        //        while (contenctLength > 0)
        //        //        {
        //        //            strm.Write(buff, 0, contenctLength);
        //        //            contenctLength = ms.Read(buff, 0, buffLength);
        //        //        }
        //        //    }
        //        //}
        //        //catch (Exception ex)
        //        //{
        //        //    throw new Exception("Failed to upload", ex.InnerException);
        //        //}













        //        string DOB = null;
        //        string Anniversary = null;
        //        if (dobstr != null && dobstr != "01-01-0100")
        //        {
        //            DOB = dobstr.Split('-')[2] + '-' + dobstr.Split('-')[1] + '-' + dobstr.Split('-')[0];
        //        }

        //        if (date_aniversarystr != null && date_aniversarystr != "01-01-0100")
        //        {
        //            Anniversary = date_aniversarystr.Split('-')[2] + '-' + date_aniversarystr.Split('-')[1] + '-' + date_aniversarystr.Split('-')[0];
        //        }

        //        string Userid = Convert.ToString(Session["userid"]);
        //        //int i = obj.AddNewShop(Shop_code, State,  type,  AssignedTo,  Party_Name,  Party_Code,  Address,  Pin_Code,  owner_name, DOB,
        //        //    Anniversary,  owner_contact_no,  Alternate_Contact,  owner_email,  ShopStatus,  EntyType,  Owner_PAN,  Owner_Adhar,  Remarks,
        //        //     NewUser,  OldUser,  shop_lat,  shop_long);
        //        ProcedureExecute proc = new ProcedureExecute("PRC_FTSInsertUpdateNewParty");
        //        if (Shop_code == null || Shop_code == "")
        //        {
        //            proc.AddPara("@ACTION", "InsertShop");
        //        }
        //        else
        //        {
        //            proc.AddPara("@ACTION", "UpdateShop");
        //        }
        //        proc.AddPara("@ShopCode", Shop_code);
        //        proc.AddPara("@stateId", State);
        //        proc.AddPara("@ShopType", type);
        //        proc.AddPara("@assigned_to_pp_id", AssignedTo);
        //        proc.AddPara("@Shop_Name", Party_Name);
        //        proc.AddPara("@EntityCode", Party_Code);
        //        proc.AddPara("@Address", Address);
        //        proc.AddPara("@Pincode", Pin_Code);
        //        proc.AddPara("@Shop_Owner", owner_name);
        //        proc.AddPara("@dob", DOB);
        //        proc.AddPara("@date_aniversary", Anniversary);
        //        proc.AddPara("@Shop_Owner_Contact", owner_contact_no);
        //        proc.AddPara("@Alt_MobileNo", Alternate_Contact);
        //        proc.AddPara("@Shop_Owner_Email", owner_email);
        //        proc.AddPara("@Entity_Status", ShopStatus);
        //        proc.AddPara("@Entity_Type", EntyType);
        //        proc.AddPara("@ShopOwner_PAN", Owner_PAN);
        //        proc.AddPara("@ShopOwner_Aadhar", Owner_Adhar);
        //        proc.AddPara("@Remarks", Remarks);
        //        proc.AddPara("@user_id", NewUser);
        //        proc.AddPara("@OLD_CreateUser", OldUser);
        //        proc.AddPara("@Shop_Lat", shop_lat);
        //        proc.AddPara("@Shop_Long", shop_long);
        //        proc.AddPara("@Shop_Image", "");
        //        k = proc.RunActionQuery();
        //        // }
        //        return Json(k, JsonRequestBehavior.AllowGet);
        //    }
        //    catch
        //    {
        //        return RedirectToAction("Logout", "Login", new { Area = "" });
        //    }
        //}

        //public ActionResult ShowPartyDetails(String ShopCode)
        //{
        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        AddPartyDetailsModel ret = new AddPartyDetailsModel();
        //        dt = obj.ShopGetDetails(ShopCode);
        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            ret.Shop_code = dt.Rows[0]["Shop_Code"].ToString();
        //            ret.State = Convert.ToInt32(dt.Rows[0]["stateId"].ToString());
        //            ret.type = Convert.ToInt32(dt.Rows[0]["type"].ToString());
        //            ret.AssignedTo = dt.Rows[0]["pp_name"].ToString();
        //            ret.AssignedToDD = dt.Rows[0]["DD_name"].ToString();
        //            ret.PPCode = dt.Rows[0]["assigned_to_pp_id"].ToString();
        //            ret.DDCode = dt.Rows[0]["assigned_to_dd_id"].ToString();
        //            ret.Party_Name = dt.Rows[0]["Shop_Name"].ToString();
        //            ret.Party_Code = dt.Rows[0]["EntityCode"].ToString();
        //            ret.Address = dt.Rows[0]["Address"].ToString();
        //            ret.Pin_Code = dt.Rows[0]["Pincode"].ToString();
        //            ret.owner_name = dt.Rows[0]["Shop_Owner"].ToString();
        //            //DOB,
        //            //Anniversary,
        //            ret.owner_contact_no = dt.Rows[0]["Shop_Owner_Contact"].ToString();
        //            ret.Alternate_Contact = dt.Rows[0]["Alt_MobileNo"].ToString();
        //            ret.owner_email = dt.Rows[0]["Shop_Owner_Email"].ToString();
        //            ret.ShopStatus = dt.Rows[0]["Entity_Status"].ToString();
        //            ret.EntyType = Convert.ToInt32(dt.Rows[0]["Entity_Type"].ToString());
        //            ret.Owner_PAN = dt.Rows[0]["ShopOwner_PAN"].ToString();
        //            ret.Owner_Adhar = dt.Rows[0]["ShopOwner_Aadhar"].ToString();
        //            ret.Remarks = dt.Rows[0]["Remarks"].ToString();
        //            ret.NewUser = Convert.ToInt64(dt.Rows[0]["Shop_CreateUser"].ToString());
        //            ret.OldUser = Convert.ToInt64(dt.Rows[0]["OLD_CreateUser"].ToString());

        //            ret.NewUserName = Convert.ToString(dt.Rows[0]["NewUserName"]);
        //            ret.OldUserName = Convert.ToString(dt.Rows[0]["OldUserName"]);

        //            ret.shop_lat = dt.Rows[0]["Shop_Lat"].ToString();
        //            ret.shop_long = dt.Rows[0]["Shop_Long"].ToString();
        //            ret.CountryID = dt.Rows[0]["countryId"].ToString();

        //            ret.AreaID = dt.Rows[0]["Area_id"].ToString();
        //            ret.CityId = dt.Rows[0]["Shop_City"].ToString();
        //            ret.Location = dt.Rows[0]["Entity_Location"].ToString();

        //            ret.Entity = dt.Rows[0]["Entity_Id"].ToString();
        //            ret.PartyStatus = dt.Rows[0]["Party_Status_id"].ToString();
        //            ret.GroupBeat = dt.Rows[0]["beat_id"].ToString();
        //            ret.AccountHolder = dt.Rows[0]["account_holder"].ToString();
        //            ret.BankName = dt.Rows[0]["bank_name"].ToString();
        //            ret.AccountNo = dt.Rows[0]["account_no"].ToString();
        //            ret.IFSCCode = dt.Rows[0]["ifsc"].ToString();
        //            ret.UPIID = dt.Rows[0]["upi_id"].ToString();

        //            ret.retailer_id = dt.Rows[0]["retailer_id"].ToString();
        //            ret.dealer_id = dt.Rows[0]["dealer_id"].ToString();

        //            ret.Retaile = dt.Rows[0]["assigned_to_shop_id"].ToString();
        //            ret.AssignedToRetaile = dt.Rows[0]["assignedShopName"].ToString();
        //        }
        //        return Json(ret, JsonRequestBehavior.AllowGet);
        //    }
        //    catch
        //    {
        //        return RedirectToAction("Logout", "Login", new { Area = "" });
        //    }
        //}

        //public ActionResult LasteEntityCode(String stateid, String ShopType)
        //{
        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        String output = "";
        //        dt = obj.LasteEntityCodeStateWise(stateid, ShopType);
        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            output = dt.Rows[0]["EntityCode"].ToString();
        //        }
        //        return Json(output, JsonRequestBehavior.AllowGet);
        //    }
        //    catch
        //    {
        //        return RedirectToAction("Logout", "Login", new { Area = "" });
        //    }
        //}

        public ActionResult ExporRegisterList(int type)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetDoctorBatchGridViewSettings(), GetCRMEnquiriesDetails(""));
                case 2:
                    return GridViewExtension.ExportToXlsx(GetDoctorBatchGridViewSettings(), GetCRMEnquiriesDetails(""));
                case 3:
                    return GridViewExtension.ExportToXls(GetDoctorBatchGridViewSettings(), GetCRMEnquiriesDetails(""));
                case 4:
                    return GridViewExtension.ExportToRtf(GetDoctorBatchGridViewSettings(), GetCRMEnquiriesDetails(""));
                case 5:
                    return GridViewExtension.ExportToCsv(GetDoctorBatchGridViewSettings(), GetCRMEnquiriesDetails(""));
                default:
                    break;
            }
            return null;
        }

        private GridViewSettings GetDoctorBatchGridViewSettings()
        {
            //Mantis Issue 24827
            DataTable dtColmn = GetPageRetention(Session["userid"].ToString(), "CRM Enquiries");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            }
            //End of Mantis Issue 24827
            var settings = new GridViewSettings();
            settings.Name = "gridCRMEnquiries";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Enquiries";

            //Mantis Issue 24816
            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "DATE";
            //    x.Caption = "Date & Time";
            //    x.VisibleIndex = 1;
            //    x.Width = 80;
            //});
            settings.Columns.Add(x =>
            {
                x.FieldName = "SEQ";
                x.Caption = "Sr. No";
                x.VisibleIndex = 1;
                x.Width = 80;
                //Mantis Issue 24827
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
                //End of Mantis Issue 24827
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "VEND_TYPE";
                x.Caption = "Lead From";
                x.VisibleIndex = 2;
                x.Width = 110;
                //Mantis Issue 24827
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
                //End of Mantis Issue 24827
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "Year";
                x.Caption = "Year";
                x.VisibleIndex = 3;
                x.Width = 60;
                //Mantis Issue 24827
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
                //End of Mantis Issue 24827
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "Month";
                x.Caption = "Month";
                x.VisibleIndex = 4;
                x.Width = 60;
                //Mantis Issue 24827
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
                //End of Mantis Issue 24827
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "Day";
                x.Caption = "Date";
                x.VisibleIndex = 5;
                x.Width = 110;
                //Mantis Issue 24827
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
                //End of Mantis Issue 24827
            });
            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "CUSTOMER_NAME";
            //    x.Caption = "Customer";
            //    x.VisibleIndex = 2;
            //    x.Width = 110;
            //});
            settings.Columns.Add(x =>
            {
                x.FieldName = "CUSTOMER_NAME";
                x.Caption = "Company Name";
                x.VisibleIndex = 6;
                x.Width = 110;
                //Mantis Issue 24827
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
                //End of Mantis Issue 24827
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "PHONENO";
                x.Caption = "Contact number";
                x.VisibleIndex = 7;
                x.Width = 80;
                //Mantis Issue 24827
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
                //End of Mantis Issue 24827
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "EMAIL";
                x.Caption = "Email Id";
                x.VisibleIndex = 8;
                x.Width = 100;
                //Mantis Issue 24827
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
                //End of Mantis Issue 24827
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "State";
                x.Caption = "State";
                x.VisibleIndex = 9;
                x.Width = 100;
                //Mantis Issue 24827
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
                //End of Mantis Issue 24827
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "Area";
                x.Caption = "Area";
                x.VisibleIndex = 10;
                x.Width = 100;
                //Mantis Issue 24827
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
                //End of Mantis Issue 24827
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "ASSIGNED_TO";
                x.Caption = "HOD";
                x.VisibleIndex = 11;
                x.Width = 100;
                //Mantis Issue 24827
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
                //End of Mantis Issue 24827

            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "ReASSIGNED_TO";
                x.Caption = "Pass to";
                x.VisibleIndex = 12;
                x.Width = 100;
                //Mantis Issue 24827
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
                //End of Mantis Issue 24827

            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "FEEDBACK";
                x.Caption = "Remark";
                x.VisibleIndex = 13;
                x.Width = 100;
                //Mantis Issue 24827
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
                //End of Mantis Issue 24827
            });
            //End of Mantis Issue 24816
            

            

            settings.Columns.Add(x =>
            {
                x.FieldName = "CONTACT_PERSON";
                x.Caption = "Contact Person";
                x.VisibleIndex = 14;
                x.Width = 96;
                //Mantis Issue 24827
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
                //End of Mantis Issue 24827
            });
            //Mantis Issue 24816
            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "PHONENO";
            //    x.Caption = "Contact No";
            //    x.VisibleIndex = 4;
            //    x.Width = 80;
            //});
            
            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "EMAIL";
            //    x.Caption = "Email";
            //    x.VisibleIndex = 5;
            //    x.Width = 100;
            //});
            //End of Mantis Issue 24816
            settings.Columns.Add(x =>
            {
                x.FieldName = "LOCATION";
                x.Caption = "Location";
                x.VisibleIndex = 15;
                x.Width = 100;
                //Mantis Issue 24827
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='LOCATION'");
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
                //End of Mantis Issue 24827
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "PRODUCT_REQUIRED";
                x.Caption = "Products Req.";
                x.VisibleIndex = 16;
                x.Width = 90;
                //Mantis Issue 24827
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
                //End of Mantis Issue 24827
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "QTY";
                x.Caption = "Qty.";
                x.VisibleIndex = 17;
                x.Width = 40;
                //Mantis Issue 24827
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
                //End of Mantis Issue 24827
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "UOM";
                x.Caption = "UOM";
                x.VisibleIndex = 18;
                x.Width = 80;
                //Mantis Issue 24827
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
                //End of Mantis Issue 24827
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ORDER_VALUE";
                x.Caption = "Order value";
                x.VisibleIndex = 19;
                x.Width = 80;
                // Rev 3.0
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.PropertiesEdit.DisplayFormatString = "0.00";
                // End of Rev 3.0
                //Mantis Issue 24827
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
                //End of Mantis Issue 24827
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ENQ_DETAILS";
                x.Caption = "Enquiry Details";
                x.VisibleIndex = 20;
                x.Width = 160;
                //Mantis Issue 24827
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
                //End of Mantis Issue 24827
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "VEND_TYPE";
                x.Caption = "Provided by";
                x.VisibleIndex = 21;
                x.Width = 100;
                //Mantis Issue 24827
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
                //End of Mantis Issue 24827
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ENQ_NUMBER";
                x.Caption = "Enq. No.";
                x.VisibleIndex = 22;
                x.Width = 70;
                //Mantis Issue 24827
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ENQ_NUMBER'");
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
                //End of Mantis Issue 24827
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "INDUSTRY";
                x.Caption = "Industry";
                x.VisibleIndex = 23;
                x.Width = 150;
                //Mantis Issue 24827
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='INDUSTRY'");
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
                //End of Mantis Issue 24827
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "MISC_COMMENTS";
                x.Caption = "Misc Comments";
                x.VisibleIndex = 24;
                x.Width = 150;
                //Mantis Issue 24827
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='MISC_COMMENTS'");
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
                //End of Mantis Issue 24827
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "PRIORITYS";
                x.Caption = "Priority";
                x.VisibleIndex = 25;
                x.Width = 100;
                //Mantis Issue 24827
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PRIORITYS'");
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
                //End of Mantis Issue 24827
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "EXIST_CUST";
                x.Caption = "Existing Customer";
                x.VisibleIndex = 26;
                x.Width = 140;
                //Mantis Issue 24827
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='EXIST_CUST'");
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
                //End of Mantis Issue 24827
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "LAST_CONTACT_DATE";
                x.Caption = "Last Contact Date";
                x.VisibleIndex = 27;
                x.Width = 120;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
                //Mantis Issue 24827
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='LAST_CONTACT_DATE'");
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
                //End of Mantis Issue 24827
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "NEXT_CONTACT_DATE";
                x.Caption = "Next Contact Date";
                x.VisibleIndex = 28;
                x.Width = 120;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
                //Mantis Issue 24827
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='NEXT_CONTACT_DATE'");
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
                //End of Mantis Issue 24827
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CONTACTEDBY";
                x.Caption = "Contacted By";
                x.VisibleIndex = 29;
                x.Width = 140;
                //Mantis Issue 24827
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACTEDBY'");
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
                //End of Mantis Issue 24827
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ENQ_PRODREQ";
                x.Caption = "Product Required";
                x.VisibleIndex = 30;
                x.Width = 100;
                //Mantis Issue 24827
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ENQ_PRODREQ'");
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
                //End of Mantis Issue 24827
            });
            //Mantis Issue 24816
            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "FEEDBACK";
            //    x.Caption = "Feedback";
            //    x.VisibleIndex = 22;
            //    x.Width = 100;
            //});
            //End of Mantis Issue 24816
            settings.Columns.Add(x =>
            {
                x.FieldName = "FINAL_INDUSTRY";
                x.Caption = "Final Industry";
                x.VisibleIndex = 31;
                x.Width = 140;
                //Mantis Issue 24827
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='FINAL_INDUSTRY'");
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
                //End of Mantis Issue 24827
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ACTIVITY";
                x.Caption = "Activity";
                x.VisibleIndex = 32;
                x.Width = 120;
                //Mantis Issue 24827
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ACTIVITY'");
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
                //End of Mantis Issue 24827
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "VERIFY_BY";
                x.Caption = "Verified By";
                x.VisibleIndex = 33;
                x.Width = 120;
                //Mantis Issue 24827
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='VERIFY_BY'");
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
                //End of Mantis Issue 24827
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "VERIFY_ON";
                x.Caption = "Verified On";
                x.VisibleIndex = 34;
                x.Width = 80;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
                //Mantis Issue 24827
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='VERIFY_ON'");
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
                //End of Mantis Issue 24827
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "VERIFY_CLOSUREDATE";
                x.Caption = "Closure Date";
                x.VisibleIndex = 35;
                x.Width = 80;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
                //Mantis Issue 24827
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='VERIFY_CLOSUREDATE'");
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
                //End of Mantis Issue 24827
            });
            // Mantis Issue 24890
            settings.Columns.Add(x =>
            {
                x.FieldName = "STATUS";
                x.Caption = "Status";
                x.VisibleIndex = 36;
                x.Width = 80;
                //Mantis Issue 24827
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
                //End of Mantis Issue 24827
            });
            //Mantis Issue 24816
            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "ASSIGNED_TO";
            //    x.Caption = "Assigned To";
            //    x.VisibleIndex = 29;
            //    x.Width = 120;

            //});
            //End of Mantis Issue 24816
            // End of Mantis Issue 24890

           //Mantis Issue 24827
            settings.Columns.Add(x =>
            {
                x.FieldName = "SalesmanAssign_date";
                x.Caption = "Assign date";
                x.VisibleIndex = 37;
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
                x.FieldName = "ReSalesmanAssign_date";
                x.Caption = "Re Assign date";
                x.VisibleIndex = 38;
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
           //End of Mantis Issue 24827

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

        // Rev 3.0
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
                        DataTable dtExcelData = new DataTable();
                        dtExcelData.Columns.Add("Date", typeof(string));
                        dtExcelData.Columns.Add("Customer_Name", typeof(string));
                        dtExcelData.Columns.Add("Contact_Person", typeof(string));
                        dtExcelData.Columns.Add("PhoneNo", typeof(string));
                        dtExcelData.Columns.Add("Email", typeof(string));
                        dtExcelData.Columns.Add("Location", typeof(string));
                        dtExcelData.Columns.Add("vend_type", typeof(string));
                        dtExcelData.Columns.Add("Product_Required", typeof(string));
                        dtExcelData.Columns.Add("Quantity", typeof(decimal));
                        dtExcelData.Columns.Add("UOM", typeof(string));
                        dtExcelData.Columns.Add("Order_Value", typeof(decimal));
                        dtExcelData.Columns.Add("Enq_Details", typeof(string));

                        foreach (DataRow row in dt.Rows)
                        {
                            // Blank excel row will not be processed
                            if (Convert.ToString(row["Date *"])!="" || Convert.ToString(row["Customer Name *"]) != "" || 
                                Convert.ToString(row["Contact Person"]) != "" || Convert.ToString(row["Phone No *"]) != "" ||
                                Convert.ToString(row["Email"]) != "" || Convert.ToString(row["Location"]) != "" || 
                                Convert.ToString(row["Provided By *"]) != "" || Convert.ToString(row["Product Required"]) != "" || 
                                Convert.ToString(row["Quantity"]) != "" || Convert.ToString(row["UOM"]) != "" ||
                                Convert.ToString(row["Order Value"]) != "" || Convert.ToString(row["Enquiry Details"])!="")
                            {
                                if (Convert.ToString(row["Quantity"]) == "")
                                {
                                    row["Quantity"] = 0;
                                }

                                if (Convert.ToString(row["Order Value"]) == "")
                                {
                                    row["Order Value"] = 0;
                                }


                                string dtDate = "";
                                if (Convert.ToString(row["Date *"]) != "")
                                {
                                    dtDate = Convert.ToString( Convert.ToDateTime(row["Date *"]).Year + "-" + Convert.ToDateTime(row["Date *"]).Month + "-"+ Convert.ToDateTime(row["Date *"]).Day );
                                }


                                dtExcelData.Rows.Add(dtDate, Convert.ToString(row["Customer Name *"]), Convert.ToString(row["Contact Person"]),
                                    Convert.ToString(row["Phone No *"]), Convert.ToString(row["Email"]), Convert.ToString(row["Location"]), Convert.ToString(row["Provided By *"]),
                                    Convert.ToString(row["Product Required"]), Convert.ToString(row["Quantity"]), Convert.ToString(row["UOM"]),
                                    Convert.ToString(row["Order Value"]), Convert.ToString(row["Enquiry Details"]));
                            }
                        }

                        try
                        {
                            TempData["EnquiriesImportLog"] = dtExcelData;
                            TempData.Keep();

                            DataTable dtCmb = new DataTable();
                            ProcedureExecute proc = new ProcedureExecute("PRC_FSMImportCRMEnquiries");
                            proc.AddPara("@Action", "BULKIMPORT");
                            proc.AddPara("@IMPORT_TABLE", dtExcelData);
                            proc.AddPara("@CreateUser_Id", Convert.ToInt32(Session["userid"]));
                            dtCmb = proc.GetTable();

                        }
                        catch (Exception ex)
                        {
                            throw ex;
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

        [HttpPost]
        public JsonResult BulkEnquiriesImportUserLog(string Fromdt, String ToDate)
        {
            string output_msg = string.Empty;
            try
            {
                string datfrmat = Fromdt.Split('-')[2] + '-' + Fromdt.Split('-')[1] + '-' + Fromdt.Split('-')[0];
                string dattoat = ToDate.Split('-')[2] + '-' + ToDate.Split('-')[1] + '-' + ToDate.Split('-')[0];
                
                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_FSMImportCRMEnquiries");
                proc.AddPara("@ACTION", "GetBulkEnquiriesImportLog");
                proc.AddPara("@FromDate", datfrmat);
                proc.AddPara("@ToDate", dattoat);
                dt = proc.GetTable();

                if (dt != null && dt.Rows.Count > 0)
                {
                    TempData["EnquiriesImportLog"] = dt;
                    TempData.Keep();
                    output_msg = "True";
                }
                else
                {
                    output_msg = "Log not found.";
                }
            }
            catch (Exception ex)
            {
                output_msg = "Please try again later";
            }
            return Json(output_msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BulkImportLog()
        {
            List<CRMEnquiriesImportLogModel> list = new List<CRMEnquiriesImportLogModel>();
            DataTable dt = new DataTable();
            try
            {
                if (TempData["EnquiriesImportLog"] != null)
                {
                    DataTable dtCmb = new DataTable();
                    ProcedureExecute proc = new ProcedureExecute("PRC_FSMImportCRMEnquiries");
                    proc.AddPara("@Action", "SHOWIMPORTLOG");
                    proc.AddPara("@IMPORT_TABLE", (DataTable)TempData["EnquiriesImportLog"]);
                    proc.AddPara("@CreateUser_Id", Convert.ToInt32(Session["userid"]));
                    dt = proc.GetTable();
                    TempData.Keep();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        CRMEnquiriesImportLogModel data = null;
                        foreach (DataRow row in dt.Rows)
                        {
                            data = new CRMEnquiriesImportLogModel();
                            data.Date = Convert.ToDateTime(row["Date"]);
                            data.Customer_Name = Convert.ToString(row["Customer_Name"]);
                            data.Contact_Person = Convert.ToString(row["Contact_Person"]);
                            data.PhoneNo = Convert.ToString(row["PhoneNo"]);
                            data.Email = Convert.ToString(row["Email"]);
                            data.Location = Convert.ToString(row["Location"]);
                            data.Product_Required = Convert.ToString(row["Product_Required"]);
                            data.Qty = Convert.ToDecimal(row["Qty"]);
                            data.Order_Value = Convert.ToDecimal(row["Order_Value"]);
                            data.Enq_Details = Convert.ToString(row["Enq_Details"]);
                            data.vend_type = Convert.ToString(row["vend_type"]);
                            data.ImportDate = Convert.ToDateTime(row["ImportDate"]);
                            data.ImportStatus = Convert.ToString(row["ImportStatus"]);
                            data.ImportMsg = Convert.ToString(row["ImportMsg"]);
                           
                            list.Add(data);
                        }
                    }
                    //TempData["EnquiriesImportLog"] = dt;
                }

            }
            catch (Exception ex)
            {

            }
            TempData.Keep();
            return PartialView(list);
        }

        public ActionResult ExportBulkImportLogGrid(int type)
        {
            //ViewData["EnquiriesImportLog"] = TempData["EnquiriesImportLog"];

            //TempData.Keep();

            DataTable dtExp = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FSMImportCRMEnquiries");
            proc.AddPara("@Action", "SHOWIMPORTLOG");
            proc.AddPara("@IMPORT_TABLE", (DataTable)TempData["EnquiriesImportLog"]);
            proc.AddPara("@CreateUser_Id", Convert.ToInt32(Session["userid"]));
            dtExp = proc.GetTable();

            // if (ViewData["EnquiriesImportLog"] != null )
            if (dtExp != null && dtExp.Rows.Count >0 )
            {
                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetBulkImportLogGrid(dtExp), dtExp);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetBulkImportLogGrid(dtExp), dtExp);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXlsx(GetBulkImportLogGrid(dtExp), dtExp);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetBulkImportLogGrid(dtExp), dtExp);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetBulkImportLogGrid(dtExp), dtExp);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetBulkImportLogGrid(object datatable)
        {
            var settings = new GridViewSettings();
            settings.Name = "BulkImportLog";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Bulk Import Enquiries Log";

            settings.Columns.Add(x =>
            {
                x.FieldName = "Date";
                x.Caption = "Date";
                x.VisibleIndex = 1;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(140);
                x.ColumnType = MVCxGridViewColumnType.DateEdit;

                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy hh:mm tt";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy hh:mm tt";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Customer_Name";
                x.Caption = "Customer Name";
                x.VisibleIndex = 2;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(200);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Contact_Person";
                x.Caption = "Contact Person";
                x.VisibleIndex = 3;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(200);
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "PhoneNo";
                x.Caption = "Phone No.";
                x.VisibleIndex = 4;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(200);
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "Email";
                x.Caption = "Email";
                x.VisibleIndex = 5;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(200);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Location";
                x.Caption = "Location";
                x.VisibleIndex = 6;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(150);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Product_Required";
                x.Caption = "Product Required";
                x.VisibleIndex = 7;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(100);

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Qty";
                x.Caption = "Quantity";
                x.VisibleIndex = 8;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(160);
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Order_Value";
                x.Caption = "Order Value";
                x.VisibleIndex = 9;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(80);
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Enq_Details";
                x.Caption = "Enquery Details";
                x.VisibleIndex = 10;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(80);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "vend_type";
                x.Caption = "Provided By";
                x.VisibleIndex = 11;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(80);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ImportStatus";
                x.Caption = "Import Status";
                x.VisibleIndex = 12;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(80);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ImportMsg";
                x.Caption = "Import Msg";
                x.VisibleIndex = 13;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(80);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ImportDate";
                x.Caption = "Import Date";
                x.VisibleIndex = 14;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(140);
                x.ColumnType = MVCxGridViewColumnType.DateEdit;

                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy hh:mm tt";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy hh:mm tt";
            });

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

        // End of Rev 3.0

        public JsonResult EnquiryDelete(string uniqueCode)
        {
            string output_msg = string.Empty;
            try
            {
                DataTable dt = new DataTable();
                ProcedureExecute proc1 = new ProcedureExecute("PRC_CRUD_ENQUIRIES");
                proc1.AddPara("@CRM_ID", uniqueCode);
                proc1.AddPara("@ACTION_TYPE", "DELETE");

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

        #region Bulk Assign

        //public PartialViewResult PartialBulk()
        //{
        //    // Get Salesmanlist
        //    CRMEnquiriesModel Dtls = new CRMEnquiriesModel();
        //    DataTable dtSals = new DataTable();
        //    ProcedureExecute proc = new ProcedureExecute("PRC_CRUD_ENQUIRIES");
        //    proc.AddPara("@ACTION", "GetSalesmanlist");
        //    dtSals = proc.GetTable();

        //    List<SalesmanUserAssign> Salesmanlist = new List<SalesmanUserAssign>();
        //    Salesmanlist = APIHelperMethods.ToModelList<SalesmanUserAssign>(dtSals);
        //    Dtls.SalesmanUserList = Salesmanlist;
        //    Dtls.SalesmanUserId = "";
        //    // End Get Salesmanlist

        //    return PartialView("~/Areas/MYSHOP/Views/CRMEnquiries/_EnquriesBulk.cshtml", Dtls);
        //}
        public PartialViewResult PartialBulkAssign(CRMEnquiriesModel model)
        {
            string Is_PageLoadBulkAssign = string.Empty;

            if (model.Is_PageLoadBulkAssign == "Ispageload")
            {
                Is_PageLoadBulkAssign = "Is_Pageload";

            }

            return PartialView("~/Areas/MYSHOP/Views/CRMEnquiries/_EnquiriesBulkAssign.cshtml", GetBulkAssignListing(Is_PageLoadBulkAssign));
        }
        public IEnumerable GetBulkAssignListing(string Is_PageLoadBulkAssign)
        {
            string connectionString = Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]);
            string Userid = Convert.ToString(Session["userid"]);

            if (Is_PageLoadBulkAssign != "Is_Pageload")
            {
                ReportsDataContext DC = new ReportsDataContext(connectionString);
                var q = from d in DC.ENQUIRIES_LISTING1s
                        where d.USERID == Convert.ToInt32(Userid) && d.STATUS=="Pending"
                        orderby d.SEQ
                        select d;
                return q;

            }
            else
            {
                ReportsDataContext DC = new ReportsDataContext(connectionString);
                var q = from d in DC.ENQUIRIES_LISTING1s
                        where d.USERID == Convert.ToInt32(Userid) && d.SEQ == 11111111119
                        orderby d.SEQ
                        select d;
                return q;

            }
        }
        [HttpPost]
        public JsonResult BulkAssignSalesman(string ActionType, string Uniqueid, string SalesmanId)
        {

            string output_msg = string.Empty;
            string ReturnCode = "";
            Msg _msg = new Msg();
          
            try
            {
                DataTable dtAssign = new DataTable();
                ProcedureExecute proc1 = new ProcedureExecute("PRC_CRUD_ENQUIRIES");
                proc1.AddPara("@ACTION_TYPE", ActionType);
                proc1.AddPara("@CRM_IDS", Uniqueid);
                proc1.AddPara("@SALESMANID", SalesmanId);

                proc1.AddVarcharPara("@RETURNMESSAGE", 50, "", QueryParameterDirection.Output);
                proc1.AddVarcharPara("@RETURNCODE", 50, "", QueryParameterDirection.Output);
                dtAssign = proc1.GetTable();

                output_msg = Convert.ToString(proc1.GetParaValue("@RETURNMESSAGE"));
                ReturnCode = Convert.ToString(proc1.GetParaValue("@RETURNCODE"));

                if (output_msg == "Success" && ReturnCode == "1")
                {
                    _msg.response_code = "Success";
                    _msg.response_msg = "Success";
                    //Mantis Issue 0024759
                    string Mssg = "";
                    string SalesMan_Nm = "";
                    string SalesMan_Phn = "";
                    // Rev 2.0
                    //DataTable dt_CRM_IDS = odbengine.GetDataTable("select tci.Crm_Id,tci.Customer_Name,tci.PhoneNo from tbl_CRM_Import as tci where tci.Crm_Id in (select items from dbo.SplitString('" + Uniqueid + "','|'))");

                    string lead_date = "";
                    string enquiry_type = "";
                   
                    DataTable dt_CRM_IDS = odbengine.GetDataTable("select tci.Crm_Id,tci.Customer_Name,tci.PhoneNo, CONVERT(VARCHAR(10), tci.Date,126) AS LEAD_DATE, tci.vend_type from tbl_CRM_Import as tci where tci.Crm_Id in (select items from dbo.SplitString('" + Uniqueid + "','|'))");
                    // End of Rev 2.0
                    //DataTable dt_SalesMan = odbengine.GetDataTable("select phf_phoneNumber,user_name from tbl_master_user inner join tbl_master_phonefax on user_contactId=phf_cntId where user_id=" + SalesmanId + "");
                    DataTable dt_SalesMan = odbengine.GetDataTable("select user_loginId,user_name from tbl_master_user  where user_id=" + SalesmanId + "");
                    if (dt_SalesMan.Rows.Count > 0)
                    {
                        SalesMan_Nm = dt_SalesMan.Rows[0]["user_name"].ToString();
                        //SalesMan_Phn = dt_SalesMan.Rows[0]["phf_phoneNumber"].ToString();
                        SalesMan_Phn = dt_SalesMan.Rows[0]["user_loginId"].ToString();
                        if (dt_CRM_IDS.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt_CRM_IDS.Rows.Count; i++)
                            {
                                // Rev 2.0
                                //Mssg = "Hi, " + SalesMan_Nm + " Your Admin/Supervisor has assigned an enquiry of " + dt_CRM_IDS.Rows[i]["Customer_Name"].ToString() + ". Please take action on it.";
                                // SendNotification(SalesMan_Phn, Mssg);

                                Mssg = "Hi, " + SalesMan_Nm + " Your Admin/Supervisor has assigned an enquiry of " + dt_CRM_IDS.Rows[i]["Customer_Name"].ToString() + ". Please take action on it. Lead date : "+ dt_CRM_IDS.Rows[i]["LEAD_DATE"].ToString()+". Enquiry Type : "+ dt_CRM_IDS.Rows[i]["vend_type"].ToString()+".";
                                
                                lead_date = dt_CRM_IDS.Rows[i]["LEAD_DATE"].ToString();
                                enquiry_type = dt_CRM_IDS.Rows[i]["vend_type"].ToString();
                                SendNotification(SalesMan_Phn, Mssg, lead_date, enquiry_type);
                                // End of Rev 2.0
                            }
                        }
                    }
                    //End of Mantis Issue 0024759
                }
                else if (output_msg != "Success" && ReturnCode == "-1")
                {
                    _msg.response_code = "Error";
                    _msg.response_msg = output_msg;
                }
                else
                {
                    _msg.response_code = "Error";
                    _msg.response_msg = "Please try again later";
                }

            }

            catch (Exception ex)
            {
                _msg.response_code = "CatchError";
                _msg.response_msg = "Please try again later";
            }

            return Json(_msg, JsonRequestBehavior.AllowGet);
        }

       
        public JsonResult CheckAssignSalesman(string CRM_ID)
        {

            string output_msg = string.Empty;
            string ReturnCode = "";
            Msg _msg = new Msg();

            try
            {
                DataTable dtAssign = new DataTable();
                ProcedureExecute proc1 = new ProcedureExecute("PRC_CRUD_ENQUIRIES");
                proc1.AddPara("@ACTION_TYPE", "CHECKASSIGNSALESMAN");
                proc1.AddPara("@CRM_ID", CRM_ID);
               
                proc1.AddVarcharPara("@RETURNMESSAGE", 50, "", QueryParameterDirection.Output);
                proc1.AddVarcharPara("@RETURNCODE", 50, "", QueryParameterDirection.Output);
                dtAssign = proc1.GetTable();

                output_msg = Convert.ToString(proc1.GetParaValue("@RETURNMESSAGE"));
                ReturnCode = Convert.ToString(proc1.GetParaValue("@RETURNCODE"));

                if (output_msg == "Exist" && ReturnCode == "-1")
                {
                    _msg.response_code = "-1";
                    _msg.response_msg = "Exist";
                }
                else
                {
                    _msg.response_code = "1";
                    _msg.response_msg = "NotExist";
                }

            }

            catch (Exception ex)
            {
                _msg.response_code = "CatchError";
                _msg.response_msg = "Please try again later";
            }

            return Json(_msg, JsonRequestBehavior.AllowGet);
        }

        //Mantis Issue 24776
        public JsonResult GetOldAssignedSalesman(string CRM_ID)
        {
            List<SalesManList> objSalesManList = new List<SalesManList>();
            try
            {
                DataTable dtAssign = new DataTable();
                ProcedureExecute proc1 = new ProcedureExecute("PRC_CRUD_ENQUIRIES");
                proc1.AddPara("@ACTION_TYPE", "OldAssignedSalesMan");
                proc1.AddPara("@CRM_ID", CRM_ID);
                dtAssign = proc1.GetTable();
                
                objSalesManList = APIHelperMethods.ToModelList<SalesManList>(dtAssign);
            }

            catch (Exception ex)
            {
                //_msg.response_code = "CatchError";
                //_msg.response_msg = "Please try again later";
            }

            return Json(objSalesManList, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult PartialBulkReAssign(CRMEnquiriesModel model)
        {
            string Is_PageLoadBulkReAssign = string.Empty;

            if (model.Is_PageLoadBulkReAssign == "Ispageload")
            {
                Is_PageLoadBulkReAssign = "Is_Pageload";

            }

            return PartialView("~/Areas/MYSHOP/Views/CRMEnquiries/_EnquiriesBulkReAssign.cshtml", GetBulkReAssignListing(Is_PageLoadBulkReAssign));
        }
        public IEnumerable GetBulkReAssignListing(string Is_PageLoadBulkReAssign)
        {
            string connectionString = Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]);
            string Userid = Convert.ToString(Session["userid"]);

            if (Is_PageLoadBulkReAssign != "Is_Pageload")
            {
                ReportsDataContext DC = new ReportsDataContext(connectionString);
                var q = from d in DC.ENQUIRIES_LISTING1s
                        //Mantis Issue 24810
                        //where d.USERID == Convert.ToInt32(Userid) && d.STATUS == "Assigned"
                        where d.USERID == Convert.ToInt32(Userid) && d.STATUS == "Assigned" || d.STATUS == "Re Assigned"
                        //End of Mantis Issue 24810
                        orderby d.SEQ
                        select d;
                return q;

            }
            else
            {
                ReportsDataContext DC = new ReportsDataContext(connectionString);
                var q = from d in DC.ENQUIRIES_LISTING1s
                        where d.USERID == Convert.ToInt32(Userid) && d.SEQ == 11111111119
                        orderby d.SEQ
                        select d;
                return q;

            }
        }

        #endregion
        //Mantis Issue 0024759
        // Rev 2.0 [parameters "lead_date" and "enquiry_type" added]
        public JsonResult SendNotification(string Mobiles, string messagetext, string lead_date, string enquiry_type)
        {
            
            string status = string.Empty;
            try
            {
                int returnmssge = notificationbl.Savenotification(Mobiles, messagetext);
                DataTable dt = odbengine.GetDataTable("select  device_token,musr.user_name,musr.user_id  from tbl_master_user as musr inner join tbl_FTS_devicetoken as token on musr.user_id=token.UserID  where musr.user_loginId in (select items from dbo.SplitString('" + Mobiles + "',',')) and musr.user_inactive='N'");

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToString(dt.Rows[i]["device_token"]) != "")
                        {
                            // Rev 2.0
                            //SendPushNotification(messagetext, Convert.ToString(dt.Rows[i]["device_token"]), Convert.ToString(dt.Rows[i]["user_name"]), Convert.ToString(dt.Rows[i]["user_id"]));
                            SendPushNotification(messagetext, Convert.ToString(dt.Rows[i]["device_token"]), Convert.ToString(dt.Rows[i]["user_name"]), Convert.ToString(dt.Rows[i]["user_id"]), "lead_work", lead_date, enquiry_type);
                            // End of 2.0
                        }
                    }
                    status = "200";
                }


                else
                {

                    status = "202";
                }



                return Json(status, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                status = "300";
                return Json(status, JsonRequestBehavior.AllowGet);
            }
        }
        // Rev 2.0 [parameters "lead_date" and "enquiry_type" added]
        // Rev 5.0
        //public static void SendPushNotification(string message="", string deviceid = "", string Customer="", string Requesttype="", 
        //    string lead_date="", string enquiry_type="", string title="", string type="")
        public void SendPushNotification(string message = "", string deviceid = "", string Customer = "", string Requesttype = "", string type = "",
            string lead_date = "", string enquiry_type = "", string title = "")
            // End of Rev 5.0
        {
            try
            {
                //string applicationID = "AAAAS0O97Kk:APA91bH8_KgkJzglOUHC1ZcMEQFjQu8fsj1HBKqmyFf-FU_I_GLtXL_BFUytUjhlfbKvZFX9rb84PWjs05HNU1QyvKy_TJBx7nF70IdIHBMkPgSefwTRyDj59yXz4iiKLxMiXJ7vel8B";
                //string senderId = "323259067561";
                // Rev 5.0
                //string applicationID = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["AppID"]);
                //string senderId = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["SenderID"]);
                ////string senderId = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["SenderID"]);
                //string deviceId = deviceid;
                //WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                //tRequest.Method = "post";
                //tRequest.ContentType = "application/json";

                //var data2 = new
                //{
                //    to = deviceId,
                //    //notification = new
                //    //{
                //    //    body = message,
                //    //    title = ""
                //    //},
                //    data = new
                //    {
                //        UserName = Customer,
                //        UserID = Requesttype,
                //        body = message,
                //        // Rev 2.0
                //        type = "lead_work",
                //        lead_date = lead_date,
                //        enquiry_type = enquiry_type
                //        // End of Rev 2.0
                //    }
                //};

                //var serializer = new JavaScriptSerializer();
                //var json = serializer.Serialize(data2);
                //Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                //tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                //tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                //tRequest.ContentLength = byteArray.Length;
                //using (Stream dataStream = tRequest.GetRequestStream())
                //{
                //    dataStream.Write(byteArray, 0, byteArray.Length); 
                //    using (WebResponse tResponse = tRequest.GetResponse())
                //    {
                //        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                //        {
                //            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                //            {
                //                String sResponseFromServer = tReader.ReadToEnd();
                //                string str = sResponseFromServer;
                //            }
                //        }
                //    }
                //}


                DBEngine odbengine = new DBEngine();
                string fileName = "", projectname = "" ;
                
                DataTable dt = odbengine.GetDataTable("select JSONFILE_NAME, PROJECT_NAME from FSM_CONFIG_FIREBASENITIFICATION WHERE ID=1");
                
                if (dt.Rows.Count > 0 ) {
                    fileName = System.Web.Hosting.HostingEnvironment.MapPath("~/"+ Convert.ToString(dt.Rows[0]["JSONFILE_NAME"]));
                    projectname = Convert.ToString(dt.Rows[0]["PROJECT_NAME"]);
                }

                //string fileName = System.Web.Hosting.HostingEnvironment.MapPath("~/demofsm-fee63-firebase-adminsdk-m1emn-4e3e8bba2d.json"); //Download from Firebase Console ServiceAccount

                string scopes = "https://www.googleapis.com/auth/firebase.messaging";
                var bearertoken = ""; // Bearer Token in this variable
                using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))

                {

                    bearertoken = GoogleCredential
                      .FromStream(stream) // Loads key file
                      .CreateScoped(scopes) // Gathers scopes requested
                      .UnderlyingCredential // Gets the credentials
                      .GetAccessTokenForRequestAsync().Result; // Gets the Access Token

                }

                ///--------Calling FCM-----------------------------

                var clientHandler = new HttpClientHandler();
                var client = new HttpClient(clientHandler);

                client.BaseAddress = new Uri("https://fcm.googleapis.com/v1/projects/"+ projectname + "/messages:send"); // FCM HttpV1 API

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //client.DefaultRequestHeaders.Accept.Add("Authorization", "Bearer " + bearertoken);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearertoken); // Authorization Token in this variable

                //---------------Assigning Of data To Model --------------

                Root rootObj = new Root();
                rootObj.message = new Message();
                rootObj.message.token = deviceid;  //"AAAA8_ptc9A:APA91bGhMaxl_Mm811bpvNExTIyZZz16krSTnCAp1RpbRKV8hZuIh9gsI6svxMvZO74WaZl3piBPHJzp2N3NN3JRS8a150BAmyLnwqa7nJUFay_kxNm11dQfdDCl00QUPncGCKq1kPYH"; //FCM Token id

                rootObj.message.data = new Data();
                rootObj.message.data.UserName = Customer;
                rootObj.message.data.UserID = Requesttype;
                rootObj.message.data.body = message;
                rootObj.message.data.type = type;
                rootObj.message.data.lead_date = lead_date;
                rootObj.message.data.enquiry_type = enquiry_type;


                //rootObj.message.data.title = title;
                //rootObj.message.data.header = title;
                //rootObj.message.data.imgNotification_Icon = imgNotification_Icon;
                // rootObj.message.data.key_2 = "Sample Key2";
                //rootObj.message.notification = new Notification();
                //rootObj.message.notification.title = title;
                //rootObj.message.notification.body = message;

                //-------------Convert Model To JSON ----------------------

                var jsonObj = new JavaScriptSerializer().Serialize(rootObj);

                //------------------------Calling Of FCM Notify API-------------------

                var data = new StringContent(jsonObj, Encoding.UTF8, "application/json");
                data.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = client.PostAsync("https://fcm.googleapis.com/v1/projects/" + projectname +"/messages:send", data).Result; // Calling The FCM httpv1 API

                //---------- Deserialize Json Response from API ----------------------------------

                var jsonResponse = response.Content.ReadAsStringAsync().Result;
                var responseObj = new JavaScriptSerializer().DeserializeObject(jsonResponse);
                // End of Rev 5.0
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }
        //End of Mantis Issue 0024759
        //Mantis Issue 24816
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
                int k = InsertPageRetention(Col, Session["userid"].ToString(), "CRM Enquiries");

                return Json(k, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
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
        //End of Mantis Issue 24816
    }
    //Mantis Issue 24776
    public class SalesManList
    {
        public int user_id { get; set; }
        public string user_name { get; set; }
    }
    //End of Mantis Issue 24776

}
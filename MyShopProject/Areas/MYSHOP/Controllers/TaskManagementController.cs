//====================================================== Revision History =================================================== 
//Page Create By : PRITI On  09/05/2023 for V2.0.40 Refer:
//    0026031:Copy the current Enquiry page, and create a duplicate and name it as 'Task Management'
//    0026032: Customization in Task Management Page
//    0026034: Customization in Add Task Page
// 1.0       10 / 09 / 2024        V2.0.48          Sanchita          27690: Quotation Notification issue @ Eurobond
//====================================================== Revision History ===================================================

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
    public class TaskManagementController : Controller
    {

        NotificationBL notificationbl = new NotificationBL();
        DBEngine odbengine = new DBEngine();

        public ActionResult Index()
        {
            TaskManagementModel Dtls = new TaskManagementModel();

            Dtls.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
            Dtls.Todate = DateTime.Now.ToString("dd-MM-yyyy");
            Dtls.Is_PageLoad = "Ispageload";

            EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/TaskManagement/Index");
            ViewBag.CanAdd = rights.CanAdd;
            ViewBag.CanView = rights.CanView;
            ViewBag.CanExport = rights.CanExport;
            ViewBag.CanReassign = rights.CanReassign;
            ViewBag.CanAssign = rights.CanAssign;

            DataTable dtSals = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_TASKMANAGEMENT");
            proc.AddPara("@ACTION_TYPE", "GetSalesmanlist");
            dtSals = proc.GetTable();

            List<SalesmanUserAssignTM> SalesManTMList = new List<SalesmanUserAssignTM>();
            SalesManTMList = APIHelperMethods.ToModelList<SalesmanUserAssignTM>(dtSals);
            Dtls.SalesmanUserList = SalesManTMList;
            Dtls.SalesmanUserId = "";


            return View(Dtls);
        }

        public JsonResult GetEnquiryFromListSelectAll()
        {
            BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(string.Empty);
            try
            {
                List<GetTaskPriority> modelEnquiryFrom = new List<GetTaskPriority>();
                //DataTable dtEnquiryFrom = lstuser.GetHeadEnquiryFromList(Hoid, Hoid);
                //DataTable dtEnquiryFromChild = new DataTable();
                DataTable ComponentTable = new DataTable();
                ComponentTable = oDBEngine.GetDataTable("SELECT cast(TASKPRIORITY_ID as varchar)TASKPRIORITY_ID, TASKPRIORITY_FROM TaskPriorityDesc from MASTER_TASKPRIORITY order by TASKPRIORITY_ID");
                modelEnquiryFrom = APIHelperMethods.ToModelList<GetTaskPriority>(ComponentTable);
                return Json(modelEnquiryFrom, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetTaskPriority()
        {
            try
            {
                List<GetTaskPriority> modelEnquiryFrom = new List<GetTaskPriority>();

                DataTable dtTaskPriority = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_TASKMANAGEMENT");
                proc.AddPara("@ACTION_TYPE", "GetTaskPriority");
                dtTaskPriority = proc.GetTable();

                modelEnquiryFrom = APIHelperMethods.ToModelList<GetTaskPriority>(dtTaskPriority);

                return PartialView("~/Areas/MYSHOP/Views/TaskManagement/_TaskManagementFromPartial.cshtml", modelEnquiryFrom);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult PartialTaskManagementGridList(TaskManagementModel model)
        {
            try
            {
                EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/TaskManagement/Index");
                ViewBag.CanAdd = rights.CanAdd;
                ViewBag.CanView = rights.CanView;
                ViewBag.CanExport = rights.CanExport;
                ViewBag.CanReassign = rights.CanReassign;
                ViewBag.CanAssign = rights.CanAssign;

                string TaskPriority = "";
                int i = 1;

                if (model.TaskPriorityDesc != null && model.TaskPriorityDesc.Count > 0)
                {
                    foreach (string item in model.TaskPriorityDesc)
                    {
                        if (i > 1)
                            TaskPriority = TaskPriority + "," + item;
                        else
                            TaskPriority = item;
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





                GetTaskManagementListing(TaskPriority, datfrmat, dattoat, Is_PageLoad);

                model.Is_PageLoad = "Ispageload";

                return PartialView("PartialTaskManagementGridList", GetCRMEnquiriesDetails(Is_PageLoad));

            }
            catch (Exception ex)
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }

        }

        public void GetTaskManagementListing(string TaskPriority, string FromDate, string ToDate, string Is_PageLoad)
        {
            string user_id = Convert.ToString(Session["userid"]);
            //string FROMDATE = dtFrom.ToString("yyyy-MM-dd");
            //string TODATE = dtTo.ToString("yyyy-MM-dd");

            string action = string.Empty;
            DataTable formula_dtls = new DataTable();
            DataSet dsInst = new DataSet();
            if (Is_PageLoad == "is_pageload")
            {
                Is_PageLoad = "1";
            }
            else
            {
                Is_PageLoad = "0";
            }
            try
            {
                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_TASKMANAGEMENT_LISTING");
                proc.AddPara("@USERID", Convert.ToInt32(user_id));
                proc.AddPara("@TaskPriority", TaskPriority);
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

        public IEnumerable GetCRMEnquiriesDetails(string Is_PageLoad)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);
            ////Mantis Issue 24816
            //DataTable dtColmn = GetPageRetention(Session["userid"].ToString(), "CRM Enquiries");
            //if (dtColmn != null && dtColmn.Rows.Count > 0)
            //{
            //    ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            //}
            ////End of Mantis Issue 24816
            if (Is_PageLoad != "is_pageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.TASKMANAGEMENT_LISTINGs
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.SEQ
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.TASKMANAGEMENT_LISTINGs
                        where d.USERID == Convert.ToInt32(Userid) && d.SEQ == 11111111119
                        orderby d.SEQ
                        select d;
                return q;
            }


        }

        public ActionResult ShowTaskDetails(String TASK_ID)
        {
            try
            {
                TaskManagementModel _apply = new TaskManagementModel();
                try
                {
                    ExecProcedure execProc = new ExecProcedure();

                    DataTable _getenq = new DataTable();
                    ProcedureExecute proc1 = new ProcedureExecute("PRC_TASKMANAGEMENT");
                    proc1.AddPara("@TASK_ID", TASK_ID);
                    proc1.AddPara("@ACTION_TYPE", "Edit");
                    _getenq = proc1.GetTable();


                    if (_getenq.Rows.Count > 0)
                    {
                        _apply.TASK_ID = TASK_ID;
                        _apply._StartDate = _getenq.Rows[0]["TASK_STARTDATE"].ToString();
                        _apply._DueDate = _getenq.Rows[0]["TASK_DUEDATE"].ToString();
                        _apply.Task_Name = _getenq.Rows[0]["TASK_NAME"].ToString();
                        _apply.Task_Details = _getenq.Rows[0]["Task_Details"].ToString();
                        _apply.Priority = _getenq.Rows[0]["TASK_PRIORITY"].ToString();

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
                        for (int i = 0; i < dataset2.data.Count; i++)
                        {
                            // The format of dataset2.data[0].date returned from API is "2019-02-03T13:20:41.000Z"
                            dataset2.data[0].date = dataset2.data[i].date.Replace("T", " ");
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
        public JsonResult Apply(TaskManagementModel apply, string uniqueid)
        {
            string ReturnCode = "";
            string ReturnMsg = "";
            //_enquiries = new EnquiriesRepo();
            try
            {
                string Userid = Convert.ToString(Session["userid"]);

                DataTable dtImport = new DataTable();
                ProcedureExecute proc1 = new ProcedureExecute("PRC_TASKMANAGEMENT");
                proc1.AddPara("@USERID", Userid);
                proc1.AddPara("@ACTION_TYPE", apply.Action_type);
                proc1.AddPara("@STARTDATE", apply.StartDate);
                proc1.AddPara("@DUETDATE", apply.DueDate);
                proc1.AddPara("@Task_Name", apply.Task_Name);
                proc1.AddPara("@Priority", apply.Priority);
                proc1.AddPara("@Task_Details", apply.Task_Details);
                if (!String.IsNullOrEmpty(uniqueid))
                {
                    proc1.AddPara("@TASK_ID", (uniqueid));
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

            //DataTable dtColmn = GetPageRetention(Session["userid"].ToString(), "Task Management");
            //if (dtColmn != null && dtColmn.Rows.Count > 0)
            //{
            //    ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            //}

            var settings = new GridViewSettings();
            settings.Name = "gridTaskManagement";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "TaskManagement";


            settings.Columns.Add(x =>
            {
                x.FieldName = "SEQ";
                x.Caption = "Sr. No";
                x.VisibleIndex = 1;
                x.Width = 80;
                //if (ViewBag.RetentionColumn != null)
                //{
                //    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SEQ'");
                //    if (row != null && row.Length > 0)  /// Check now
                //    {
                //        x.Visible = false;
                //    }
                //    else
                //    {
                //        x.Visible = true;
                //        //x.Width = 100;
                //    }
                //}
                //else
                //{
                //    x.Visible = true;
                //    //x.Width = 100;
                //}

            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "STARTDATE";
                x.Caption = "Task Date";
                x.VisibleIndex = 2;
                x.Width = 120;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";

                //if (ViewBag.RetentionColumn != null)
                //{
                //    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='STARTDATE'");
                //    if (row != null && row.Length > 0)  /// Check now
                //    {
                //        x.Visible = false;
                //    }
                //    else
                //    {
                //        x.Visible = true;
                //        //x.Width = 100;
                //    }
                //}
                //else
                //{
                //    x.Visible = true;
                //    //x.Width = 100;
                //}

            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "DUETDATE";
                x.Caption = "Due Date";
                x.VisibleIndex = 3;
                x.Width = 120;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
                //if (ViewBag.RetentionColumn != null)
                //{
                //    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DUETDATE'");
                //    if (row != null && row.Length > 0)  /// Check now
                //    {
                //        x.Visible = false;
                //    }
                //    else
                //    {
                //        x.Visible = true;

                //    }
                //}
                //else
                //{
                //    x.Visible = true;

                //}

            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "Task_Name";
                x.Caption = "Task Name";
                x.VisibleIndex = 4;
                x.Width = 110;

                //if (ViewBag.RetentionColumn != null)
                //{
                //    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Task_Name'");
                //    if (row != null && row.Length > 0)  /// Check now
                //    {
                //        x.Visible = false;
                //    }
                //    else
                //    {
                //        x.Visible = true;
                //        //x.Width = 100;
                //    }
                //}
                //else
                //{
                //    x.Visible = true;
                //    //x.Width = 100;
                //}

            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "Task_Priority";
                x.Caption = "Priority";
                x.VisibleIndex = 5;
                x.Width = 80;

            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "Task_Details";
                x.Caption = "Task Details";
                x.VisibleIndex = 6;
                x.Width = 100;

            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "ASSIGNED_TO";
                x.Caption = "Assign to Name";
                x.VisibleIndex = 7;
                x.Width = 100;

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "SalesmanAssign_date";
                x.Caption = "Assign To Date";
                x.VisibleIndex = 8;
                x.Width = 120;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";

                //if (ViewBag.RetentionColumn != null)
                //{
                //    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SalesmanAssign_date'");
                //    if (row != null && row.Length > 0)  /// Check now
                //    {
                //        x.Visible = false;
                //    }
                //    else
                //    {
                //        x.Visible = true;
                //        //x.Width = 100;
                //    }
                //}
                //else
                //{
                //    x.Visible = true;
                //    //x.Width = 100;
                //}

            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "ASSIGNED_TOID";
                x.Caption = "Assign To ID";
                x.VisibleIndex = 9;
                x.Width = 100;

                //if (ViewBag.RetentionColumn != null)
                //{
                //    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ASSIGNED_TOID'");
                //    if (row != null && row.Length > 0)  /// Check now
                //    {
                //        x.Visible = false;
                //    }
                //    else
                //    {
                //        x.Visible = true;
                //        //x.Width = 100;
                //    }
                //}
                //else
                //{
                //    x.Visible = true;
                //x.Width = 100;
                // }

            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "Task_STATUS";
                x.Caption = "Task Status";
                x.VisibleIndex = 10;
                x.Width = 140;

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CREATED_DATE";
                x.Caption = "Created On";
                x.VisibleIndex = 11;
                x.Width = 80;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";

                //if (ViewBag.RetentionColumn != null)
                //{
                //    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CREATED_DATE'");
                //    if (row != null && row.Length > 0)  /// Check now
                //    {
                //        x.Visible = false;
                //    }
                //    else
                //    {
                //        x.Visible = true;
                //        //x.Width = 100;
                //    }
                //}
                //else
                //{
                //    x.Visible = true;
                //    //x.Width = 100;
                //}

            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "CREATED_BY";
                x.Caption = "Created By";
                x.VisibleIndex = 12;
                x.Width = 140;

            });



            settings.Columns.Add(x =>
            {
                x.FieldName = "MODIFIED_DATE";
                x.Caption = "Updated on";
                x.VisibleIndex = 13;
                x.Width = 80;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "MODIFIED_BY";
                x.Caption = "Updated By";
                x.VisibleIndex = 14;
                x.Width = 140;

            });

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


        public JsonResult TaskDelete(string uniqueCode)
        {
            string output_msg = string.Empty;
            try
            {
                DataTable dt = new DataTable();
                ProcedureExecute proc1 = new ProcedureExecute("PRC_TASKMANAGEMENT");
                proc1.AddPara("@TASK_ID", uniqueCode);
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
        //    // Get SalesManTMList
        //    CRMEnquiriesModel Dtls = new CRMEnquiriesModel();
        //    DataTable dtSals = new DataTable();
        //    ProcedureExecute proc = new ProcedureExecute("PRC_TASKMANAGEMENT");
        //    proc.AddPara("@ACTION", "GetSalesmanlist");
        //    dtSals = proc.GetTable();

        //    List<SalesmanUserAssign> SalesManTMList = new List<SalesmanUserAssign>();
        //    SalesManTMList = APIHelperMethods.ToModelList<SalesmanUserAssign>(dtSals);
        //    Dtls.SalesmanUserList = SalesManTMList;
        //    Dtls.SalesmanUserId = "";
        //    // End Get SalesManTMList

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
                        where d.USERID == Convert.ToInt32(Userid) && d.STATUS == "Pending"
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
                ProcedureExecute proc1 = new ProcedureExecute("PRC_TASKMANAGEMENT");
                proc1.AddPara("@ACTION_TYPE", ActionType);
                proc1.AddPara("@Task_IDS", Uniqueid);
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

                    string Mssg = "";
                    string SalesMan_Nm = "";
                    string SalesMan_Phn = "";
                    DataTable dt_CRM_IDS = odbengine.GetDataTable("select tci.TASK_ID,tci.TASK_NAME from MASTER_TASKMANAGEMENT as tci where tci.TASK_ID in (select items from dbo.SplitString('" + Uniqueid + "','|'))");
                    DataTable dt_SalesMan = odbengine.GetDataTable("select user_loginId,user_name from tbl_master_user  where user_id=" + SalesmanId + "");
                    if (dt_SalesMan.Rows.Count > 0)
                    {
                        SalesMan_Nm = dt_SalesMan.Rows[0]["user_name"].ToString();
                        SalesMan_Phn = dt_SalesMan.Rows[0]["user_loginId"].ToString();
                        if (dt_CRM_IDS.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt_CRM_IDS.Rows.Count; i++)
                            {
                                Mssg = "Hi, " + SalesMan_Nm + " Your Admin/Supervisor has assigned an enquiry of " + dt_CRM_IDS.Rows[i]["TASK_NAME"].ToString() + ". Please take action on it.";
                                SendNotification(SalesMan_Phn, Mssg);
                            }
                        }
                    }

                }
                else
                if (output_msg != "Success" && ReturnCode == "-1")
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


        public JsonResult CheckAssignSalesman(string Task_ID)
        {

            string output_msg = string.Empty;
            string ReturnCode = "";
            Msg _msg = new Msg();

            try
            {
                DataTable dtAssign = new DataTable();
                ProcedureExecute proc1 = new ProcedureExecute("PRC_TASKMANAGEMENT");
                proc1.AddPara("@ACTION_TYPE", "CHECKASSIGNSALESMAN");
                proc1.AddPara("@TASK_ID", Task_ID);

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
        public JsonResult GetOldAssignedSalesman(string TASK_ID)
        {
            List<SalesManTMList> objSalesManList = new List<SalesManTMList>();
            try
            {
                DataTable dtAssign = new DataTable();
                ProcedureExecute proc1 = new ProcedureExecute("PRC_TASKMANAGEMENT");
                proc1.AddPara("@ACTION_TYPE", "OldAssignedSalesMan");
                proc1.AddPara("@TASK_ID", TASK_ID);
                dtAssign = proc1.GetTable();

                objSalesManList = APIHelperMethods.ToModelList<SalesManTMList>(dtAssign);
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
        public JsonResult SendNotification(string Mobiles, string messagetext)
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
                            // Rev 1.0
                            //SendPushNotification(messagetext, Convert.ToString(dt.Rows[i]["device_token"]), Convert.ToString(dt.Rows[i]["user_name"]), Convert.ToString(dt.Rows[i]["user_id"]));

                            CRMEnquiriesController obj = new CRMEnquiriesController();
                            obj.SendPushNotification(messagetext, Convert.ToString(dt.Rows[i]["device_token"]), Convert.ToString(dt.Rows[i]["user_name"]), Convert.ToString(dt.Rows[i]["user_id"]), "");
                            // End of Rev 1.0
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

        // Rev 1.0
        //public static void SendPushNotification(string message, string deviceid, string Customer, string Requesttype)
        //{
        //    try
        //    {
        //        //string applicationID = "AAAAS0O97Kk:APA91bH8_KgkJzglOUHC1ZcMEQFjQu8fsj1HBKqmyFf-FU_I_GLtXL_BFUytUjhlfbKvZFX9rb84PWjs05HNU1QyvKy_TJBx7nF70IdIHBMkPgSefwTRyDj59yXz4iiKLxMiXJ7vel8B";
        //        //string senderId = "323259067561";
        //        string applicationID = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["AppID"]);
        //        string senderId = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["SenderID"]);
        //        //string senderId = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["SenderID"]);
        //        string deviceId = deviceid;
        //        WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
        //        tRequest.Method = "post";
        //        tRequest.ContentType = "application/json";

        //        var data2 = new
        //        {
        //            to = deviceId,
        //            //notification = new
        //            //{
        //            //    body = message,
        //            //    title = ""
        //            //},
        //            data = new
        //            {
        //                UserName = Customer,
        //                UserID = Requesttype,
        //                body = message
        //            }
        //        };

        //        var serializer = new JavaScriptSerializer();
        //        var json = serializer.Serialize(data2);
        //        Byte[] byteArray = Encoding.UTF8.GetBytes(json);
        //        tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
        //        tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
        //        tRequest.ContentLength = byteArray.Length;
        //        using (Stream dataStream = tRequest.GetRequestStream())
        //        {
        //            dataStream.Write(byteArray, 0, byteArray.Length);
        //            using (WebResponse tResponse = tRequest.GetResponse())
        //            {
        //                using (Stream dataStreamResponse = tResponse.GetResponseStream())
        //                {
        //                    using (StreamReader tReader = new StreamReader(dataStreamResponse))
        //                    {
        //                        String sResponseFromServer = tReader.ReadToEnd();
        //                        string str = sResponseFromServer;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string str = ex.Message;
        //    }
        //}
        //End of Mantis Issue 0024759
        // End of Rev 1.0

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
    public class SalesManTMList
    {
        public int user_id { get; set; }
        public string user_name { get; set; }
    }
    //End of Mantis Issue 24776

}
/*************************************************************************************************************
Rev 1.0     Sanchita   V2.0.28    27/01/2023      Bulk modification feature is required in Parties menu. Refer: 25609
Rev 2.0     Sanchita   V2.0.44    19/12/2023      Beat related tab will be added in the security roles of Parties. Mantis: 27080  
Rev 3.0     Sanchita   V2.0.46    11/04/2024      0027348: FSM: Master > Contact > Parties [Delete Facility]    
Rev 4.0     Sanchita   V2.0.47    29/05/2024      0027405: Colum Chooser Option needs to add for the following Modules   
Rev 5.0     Sanchita   V2.0.47    30/05/2024      Mass Delete related tabs will be added in the security roles of Parties. Mantis: 27489
Rev 6.0     Priti      V2.0.48    08-07-2024      0027407: "Party Status" - needs to add in the following reports.
Rev 7.0     Priti      V2.0.49    15-11-2024      0027799: A new Global settings required as WillShowLoanDetailsInParty.
*****************************************************************************************************************/
using BusinessLogicLayer;
using BusinessLogicLayer.SalesmanTrack;
using ClosedXML.Excel;
using DataAccessLayer;
using DevExpress.Utils;
using DevExpress.Web;
using DevExpress.Web.ASPxHtmlEditor.Internal;
using DevExpress.Web.Mvc;
using DevExpress.XtraCharts.Design;
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
// Rev 1.0
using Excel = Microsoft.Office.Interop.Excel;
// End of Rev 1.0


namespace MyShop.Areas.MYSHOP.Controllers
{
    public class partyDetailsController : Controller
    {
        CommonBL objSystemSettings = new CommonBL();
        AddPartyDetailsBL obj = new AddPartyDetailsBL();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PartyList()
        {
            try
            {
                AddPartyDetailsModel Dtls = new AddPartyDetailsModel();
                DataSet ds = obj.AddShopGetDetails();
                if (ds != null)
                {
                    List<shopTypes> shoplst = new List<shopTypes>();
                    shoplst = APIHelperMethods.ToModelList<shopTypes>(ds.Tables[0]);

                    List<PPList> pplst = new List<PPList>();
                    pplst = APIHelperMethods.ToModelList<PPList>(ds.Tables[1]);

                    List<EntityTypes> Entitylst = new List<EntityTypes>();
                    Entitylst = APIHelperMethods.ToModelList<EntityTypes>(ds.Tables[2]);

                    List<Usersshopassign> model = new List<Usersshopassign>();
                    model = APIHelperMethods.ToModelList<Usersshopassign>(ds.Tables[3]);

                    List<StateListShop> StateLst = new List<StateListShop>();
                    StateLst = APIHelperMethods.ToModelList<StateListShop>(ds.Tables[4]);

                    List<CountryList> CountryList = new List<CountryList>();
                    CountryList = APIHelperMethods.ToModelList<CountryList>(ds.Tables[5]);

                    List<Usersshopassign> InactiveUser = new List<Usersshopassign>();
                    InactiveUser = APIHelperMethods.ToModelList<Usersshopassign>(ds.Tables[6]);

                    List<Usersshopassign> ActiveReassgnUser = new List<Usersshopassign>();
                    ActiveReassgnUser = APIHelperMethods.ToModelList<Usersshopassign>(ds.Tables[7]);

                    List<clsGroupBeat> ActiveEntity = new List<clsGroupBeat>();
                    ActiveEntity = APIHelperMethods.ToModelList<clsGroupBeat>(ds.Tables[9]);

                    List<clsGroupBeat> ActivePartyStatus = new List<clsGroupBeat>();
                    ActivePartyStatus = APIHelperMethods.ToModelList<clsGroupBeat>(ds.Tables[8]);

                    List<clsGroupBeat> ActiveGroupBeat = new List<clsGroupBeat>();
                    ActiveGroupBeat = APIHelperMethods.ToModelList<clsGroupBeat>(ds.Tables[12]);
                    //Mantis Issue 25133
                    List<GroupBeatAssign> InactiveGroupBeat = new List<GroupBeatAssign>();
                    InactiveGroupBeat = APIHelperMethods.ToModelList<GroupBeatAssign>(ds.Tables[13]);
                    //End of Mantis Issue 25133

                    // Mantis Issue 25545
                    List<Usersshopassign> AreaRouteBeatUser = new List<Usersshopassign>();
                    AreaRouteBeatUser = APIHelperMethods.ToModelList<Usersshopassign>(ds.Tables[14]);

                    List<Usersshopassign> AreaRouteBeatReassignedUser = new List<Usersshopassign>();
                    AreaRouteBeatReassignedUser = APIHelperMethods.ToModelList<Usersshopassign>(ds.Tables[15]);
                    // End of Mantis Issue 25545

                    // Rev 1.0
                    List<StateList_BulkModify> StateLst_BulkModify = new List<StateList_BulkModify>();
                    StateLst_BulkModify = APIHelperMethods.ToModelList<StateList_BulkModify>(ds.Tables[16]);
                    // End of Rev 1.0

                    Dtls.shop_lat = "0";
                    Dtls.shop_long = "0";
                    Dtls.ShpTypeList = shoplst;
                    Dtls.PPCodeList = pplst;
                    Dtls.EntityTypeList = Entitylst;
                    Dtls.NewUseridList = model;
                    Dtls.OldUseridList = model;
                    Dtls.StateList = StateLst;
                    Dtls.CountryList = CountryList;
                    Dtls.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                    Dtls.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                    Dtls.user_id = Convert.ToString(Session["userid"]);

                    Dtls.InactiveUseridList = InactiveUser;
                    Dtls.ActiveReassgnUseridList = ActiveReassgnUser;
                    Dtls.FromLogdate = DateTime.Now.ToString("dd-MM-yyyy");
                    Dtls.ToLogdate = DateTime.Now.ToString("dd-MM-yyyy");

                    Dtls.EntityList = ActiveEntity;
                    Dtls.PartyStatusList = ActivePartyStatus;
                    Dtls.GroupBeatList = ActiveGroupBeat;
                    //Mantis Issue 25133
                    Dtls.InactiveGroupBeatidList = InactiveGroupBeat;
                    //End of Mantis Issue 25133
                    // Mantis Issue 25545
                    Dtls.AreaRouteBeatUseridList = AreaRouteBeatUser;
                    Dtls.AreaRouteBeatReassignedUseridList = AreaRouteBeatReassignedUser;
                    // End of Mantis Issue 25545
                    // Rev 1.0
                    Dtls.StateList_BulkModify = StateLst_BulkModify;
                    // End of Rev 1.0

                }

                //REV 7.0
                string WillShowLoanDetailsInParty = objSystemSettings.GetSystemSettingsResult("WillShowLoanDetailsInParty");

                if (WillShowLoanDetailsInParty=="1")
                {
                    
                    DataSet dsLoanDetails = obj.GetLoanDetails();
                    if (dsLoanDetails != null)
                    {
                        List<LOANTypes> RiskDetailsList = new List<LOANTypes>();
                        RiskDetailsList = APIHelperMethods.ToModelList<LOANTypes>(dsLoanDetails.Tables[0]);
                        Dtls.RiskList = RiskDetailsList;


                        List<WORKABLEVALUE> WORKABLEDetailsList = new List<WORKABLEVALUE>();
                        WORKABLEDetailsList = APIHelperMethods.ToModelList<WORKABLEVALUE>(dsLoanDetails.Tables[1]);
                        Dtls.WORKABLELIST = WORKABLEDetailsList;


                        List<LOANTypes> DISPOSITIONCODEDETAILSLIST = new List<LOANTypes>();
                        DISPOSITIONCODEDETAILSLIST = APIHelperMethods.ToModelList<LOANTypes>(dsLoanDetails.Tables[2]);
                        Dtls.DISPOSITIONCODELIST = DISPOSITIONCODEDETAILSLIST;

                        List<LOANTypes> FINALSTATUSLISTDETAILSLIST = new List<LOANTypes>();
                        FINALSTATUSLISTDETAILSLIST = APIHelperMethods.ToModelList<LOANTypes>(dsLoanDetails.Tables[3]);
                        Dtls.FINALSTATUSLIST = FINALSTATUSLISTDETAILSLIST;
                    }
                   
                }

                //REV 7.0 END



                // Mantis Issue 24603
                string IsAutoCodificationRequired = "0";
                DBEngine obj1 = new DBEngine();
                IsAutoCodificationRequired = Convert.ToString(obj1.GetDataTable("select [value] from FTS_APP_CONFIG_SETTINGS WHERE [Key]='IsAutoCodificationRequired'").Rows[0][0]);
                ViewBag.IsAutoCodificationRequired = IsAutoCodificationRequired;
                // End of Mantis Issue 24603

                // Rev 1.0
                EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/partyDetails/PartyList");
                ViewBag.CanView = rights.CanView;
                ViewBag.CanExport = rights.CanExport;
                ViewBag.CanBulkUpdate = rights.CanBulkUpdate;
                // End of Rev 1.0
                // Rev 2.0
                ViewBag.CanReassignedBeatParty = rights.CanReassignedBeatParty;
                ViewBag.CanReassignedBeatPartyLog = rights.CanReassignedBeatPartyLog;
                ViewBag.CanReassignedAreaRouteBeat = rights.CanReassignedAreaRouteBeat;
                ViewBag.CanReassignedAreaRouteBeatLog = rights.CanReassignedAreaRouteBeatLog;
                // End of Rev 2.0
                // Rev 5.0
                ViewBag.CanMassDelete = rights.CanMassDelete;
                ViewBag.CanMassDeleteDownloadImport = rights.CanMassDeleteDownloadImport;
                // End of Rev 5.0
                // Rev 3.0
                CommonBL cbl = new CommonBL();
                ViewBag.ShopDeleteWithAllTransactions = cbl.GetSystemSettingsResult("ShopDeleteWithAllTransactions");
                ViewBag.CanDelete = rights.CanDelete;
                // End of Rev 3.0

                //REV 7.0
                ViewBag.WillShowLoanDetailsInParty = WillShowLoanDetailsInParty;
                //REV 7.0 END



                return View(Dtls);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }

        public ActionResult PartialPartyDetailsGridList(String Is_PageLoad)
        {
            EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/partyDetails/PartyList");
            ViewBag.CanView = rights.CanView;
            ViewBag.CanExport = rights.CanExport;
            // Rev 1.0
            ViewBag.CanBulkUpdate = rights.CanBulkUpdate;
            // End of Rev 1.0
            // Rev 3.0
            ViewBag.CanDelete = rights.CanDelete;
            // End of Rev 3.0
            //REV 7.0
            string WillShowLoanDetailsInParty = objSystemSettings.GetSystemSettingsResult("WillShowLoanDetailsInParty");
            ViewBag.WillShowLoanDetailsInParty = WillShowLoanDetailsInParty;
            //REV 7.0 END
            return PartialView(GetDataDetails(Is_PageLoad));
        }

        public IEnumerable GetDataDetails(string Is_PageLoad)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);
            // Rev 4.0
            DataTable dtColmn = obj.GetPageRetention(Session["userid"].ToString(), "PARTY LIST");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            }
            // End of Rev 4.0

            if (Is_PageLoad != "Ispageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTS_NewPartyDetailsReports
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTS_NewPartyDetailsReports
                        where d.USERID == Convert.ToInt32(Userid) && d.ShopCode == "0"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
        }

        public ActionResult GetPartyDetailsList(AddPartyDetailsModel model)
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

                string state = "";
                int i = 1;
                if (model.StateIds != null && model.StateIds.Count > 0)
                {
                    foreach (string item in model.StateIds)
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

                //Rev Debashis
                //double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;
                //if (days <= 30)
                //{
                //    dt = obj.GetReportPartyDetails(datfrmat, dattoat, Userid, state, empcode, model.IS_ReAssignedDate);
                //}
                dt = obj.GetReportPartyDetails(datfrmat, dattoat, Userid, state, empcode, model.IS_ReAssignedDate);
                //End of Rev Debashis
                return Json(Userid, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult PartyChangeStatus(String ShopCode)
        {
            try
            {
                int k = obj.ShopActiveInactive(ShopCode);

                return Json(k, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        [HttpPost]
        public ActionResult AddPartyDetails()
        {
            try
            {
                int k = 0;
                HttpFileCollectionBase files = Request.Files;

                var obj = Request.Form;
                String Shop_code = Convert.ToString(obj["Shop_code"]);
                int State = Convert.ToInt32(obj["State"]);
                string dobstr = Convert.ToString(obj["dobstr"]);
                string date_aniversarystr = Convert.ToString(obj["date_aniversarystr"]);
                int type = Convert.ToInt32(obj["type"]);
                string AssignedTo = Convert.ToString(obj["AssignedTo"]);
                string Party_Name = Convert.ToString(obj["Party_Name"]);
                string Party_Code = Convert.ToString(obj["Party_Code"]);

                string Address = Convert.ToString(obj["Address"]);
                string Pin_Code = Convert.ToString(obj["Pin_Code"]);
                string owner_name = Convert.ToString(obj["owner_name"]);
                string owner_contact_no = Convert.ToString(obj["owner_contact_no"]);
                string Alternate_Contact = Convert.ToString(obj["Alternate_Contact"]);
                string owner_email = Convert.ToString(obj["owner_email"]);
                string ShopStatus = Convert.ToString(obj["ShopStatus"]);
                int EntyType = Convert.ToInt32(obj["EntyType"]);
                string Owner_PAN = Convert.ToString(obj["Owner_PAN"]);
                string Owner_Adhar = Convert.ToString(obj["Owner_Adhar"]);

                string Remarks = Convert.ToString(obj["Remarks"]);
                long NewUser = Convert.ToInt64(obj["NewUser"]);
                long OldUser = Convert.ToInt64(obj["OldUser"]);
                string shop_lat = Convert.ToString(obj["shop_lat"]);
                string shop_long = Convert.ToString(obj["shop_long"]);

                String ImagePath = System.Configuration.ConfigurationSettings.AppSettings["Path"];
                //for (int i = 0; i < files.Count; i++)
                //{
                //    String FileName = String.Empty;

                //    HttpPostedFileBase file = files[i];
                //    string fname;

                //    // Checking for Internet Explorer  
                //    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                //    {
                //        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                //        fname = testfiles[testfiles.Length - 1];
                //        FileName = fname;
                //    }
                //    else
                //    {
                //        fname = file.FileName;
                //        FileName = fname;
                //    }

                //fname = Path.Combine(ImagePath, fname);
                //file.SaveAs(fname);

                //byte[] filebyte = null;

                //using (StreamReader reader = new StreamReader(file.InputStream))
                //{
                //    filebyte = Encoding.UTF8.GetBytes(reader.ReadToEnd());
                //        reader.Close();
                //}



                //FtpWebRequest reqFTP;
                //reqFTP = (FtpWebRequest)FtpWebRequest.Create(ImagePath);
                //reqFTP.KeepAlive = true;
                //reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
                //reqFTP.UseBinary = true;s
                //reqFTP.ContentLength = filebyte.Length;

                //int buffLength = 2048;
                //byte[] buff = new byte[buffLength];
                //MemoryStream ms = new MemoryStream(filebyte);

                //try
                //{
                //    int contenctLength;
                //    using (Stream strm = reqFTP.GetRequestStream())
                //    {
                //        contenctLength = ms.Read(buff, 0, buffLength);
                //        while (contenctLength > 0)
                //        {
                //            strm.Write(buff, 0, contenctLength);
                //            contenctLength = ms.Read(buff, 0, buffLength);
                //        }
                //    }
                //}
                //catch (Exception ex)
                //{
                //    throw new Exception("Failed to upload", ex.InnerException);
                //}













                string DOB = null;
                string Anniversary = null;
                if (dobstr != null && dobstr != "01-01-0100")
                {
                    DOB = dobstr.Split('-')[2] + '-' + dobstr.Split('-')[1] + '-' + dobstr.Split('-')[0];
                }

                if (date_aniversarystr != null && date_aniversarystr != "01-01-0100")
                {
                    Anniversary = date_aniversarystr.Split('-')[2] + '-' + date_aniversarystr.Split('-')[1] + '-' + date_aniversarystr.Split('-')[0];
                }

                string Userid = Convert.ToString(Session["userid"]);
                //int i = obj.AddNewShop(Shop_code, State,  type,  AssignedTo,  Party_Name,  Party_Code,  Address,  Pin_Code,  owner_name, DOB,
                //    Anniversary,  owner_contact_no,  Alternate_Contact,  owner_email,  ShopStatus,  EntyType,  Owner_PAN,  Owner_Adhar,  Remarks,
                //     NewUser,  OldUser,  shop_lat,  shop_long);
                ProcedureExecute proc = new ProcedureExecute("PRC_FTSInsertUpdateNewParty");
                if (Shop_code == null || Shop_code == "")
                {
                    proc.AddPara("@ACTION", "InsertShop");
                }
                else
                {
                    proc.AddPara("@ACTION", "UpdateShop");
                }
                proc.AddPara("@ShopCode", Shop_code);
                proc.AddPara("@stateId", State);
                proc.AddPara("@ShopType", type);
                proc.AddPara("@assigned_to_pp_id", AssignedTo);
                proc.AddPara("@Shop_Name", Party_Name);
                proc.AddPara("@EntityCode", Party_Code);
                proc.AddPara("@Address", Address);
                proc.AddPara("@Pincode", Pin_Code);
                proc.AddPara("@Shop_Owner", owner_name);
                proc.AddPara("@dob", DOB);
                proc.AddPara("@date_aniversary", Anniversary);
                proc.AddPara("@Shop_Owner_Contact", owner_contact_no);
                proc.AddPara("@Alt_MobileNo", Alternate_Contact);
                proc.AddPara("@Shop_Owner_Email", owner_email);
                proc.AddPara("@Entity_Status", ShopStatus);
                proc.AddPara("@Entity_Type", EntyType);
                proc.AddPara("@ShopOwner_PAN", Owner_PAN);
                proc.AddPara("@ShopOwner_Aadhar", Owner_Adhar);
                proc.AddPara("@Remarks", Remarks);
                proc.AddPara("@user_id", NewUser);
                proc.AddPara("@OLD_CreateUser", OldUser);
                proc.AddPara("@Shop_Lat", shop_lat);
                proc.AddPara("@Shop_Long", shop_long);
                proc.AddPara("@Shop_Image", "");
                k = proc.RunActionQuery();
                // }
                return Json(k, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult ShowPartyDetails(String ShopCode)
        {
            try
            {
                DataTable dt = new DataTable();
                AddPartyDetailsModel ret = new AddPartyDetailsModel();
                dt = obj.ShopGetDetails(ShopCode);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ret.Shop_code = dt.Rows[0]["Shop_Code"].ToString();
                    ret.State = Convert.ToInt32(dt.Rows[0]["stateId"].ToString());
                    ret.type = Convert.ToInt32(dt.Rows[0]["type"].ToString());
                    ret.AssignedTo = dt.Rows[0]["pp_name"].ToString();
                    ret.AssignedToDD = dt.Rows[0]["DD_name"].ToString();
                    ret.PPCode = dt.Rows[0]["assigned_to_pp_id"].ToString();
                    ret.DDCode = dt.Rows[0]["assigned_to_dd_id"].ToString();
                    ret.Party_Name = dt.Rows[0]["Shop_Name"].ToString();
                    ret.Party_Code = dt.Rows[0]["EntityCode"].ToString();
                    ret.Address = dt.Rows[0]["Address"].ToString();
                    ret.Pin_Code = dt.Rows[0]["Pincode"].ToString();
                    ret.owner_name = dt.Rows[0]["Shop_Owner"].ToString();
                    //DOB,
                    //Anniversary,
                    ret.owner_contact_no = dt.Rows[0]["Shop_Owner_Contact"].ToString();
                    ret.Alternate_Contact = dt.Rows[0]["Alt_MobileNo"].ToString();
                    ret.owner_email = dt.Rows[0]["Shop_Owner_Email"].ToString();
                    ret.ShopStatus = dt.Rows[0]["Entity_Status"].ToString();
                    ret.EntyType = Convert.ToInt32(dt.Rows[0]["Entity_Type"].ToString());
                    ret.Owner_PAN = dt.Rows[0]["ShopOwner_PAN"].ToString();
                    ret.Owner_Adhar = dt.Rows[0]["ShopOwner_Aadhar"].ToString();
                    ret.Remarks = dt.Rows[0]["Remarks"].ToString();
                    ret.NewUser = Convert.ToInt64(dt.Rows[0]["Shop_CreateUser"].ToString());
                    ret.OldUser = Convert.ToInt64(dt.Rows[0]["OLD_CreateUser"].ToString());

                    ret.NewUserName = Convert.ToString(dt.Rows[0]["NewUserName"]);
                    ret.OldUserName = Convert.ToString(dt.Rows[0]["OldUserName"]);

                    ret.shop_lat = dt.Rows[0]["Shop_Lat"].ToString();
                    ret.shop_long = dt.Rows[0]["Shop_Long"].ToString();
                    ret.CountryID = dt.Rows[0]["countryId"].ToString();

                    ret.AreaID = dt.Rows[0]["Area_id"].ToString();
                    ret.CityId = dt.Rows[0]["Shop_City"].ToString();
                    ret.Location = dt.Rows[0]["Entity_Location"].ToString();

                    ret.Entity = dt.Rows[0]["Entity_Id"].ToString();
                    ret.PartyStatus = dt.Rows[0]["Party_Status_id"].ToString();
                    // Mantis Issue 25433
                    //ret.GroupBeat = dt.Rows[0]["beat_id"].ToString();
                    ret.GroupBeatId = dt.Rows[0]["beat_id"].ToString();

                    ret.Cluster = dt.Rows[0]["Cluster"].ToString();
                    ret.GSTN_NUMBER = dt.Rows[0]["GSTN_NUMBER"].ToString();
                    ret.Trade_Licence_Number = dt.Rows[0]["Trade_Licence_Number"].ToString();
                    ret.Alt_MobileNo1 = dt.Rows[0]["Alt_MobileNo1"].ToString();
                    ret.Shop_Owner_Email2 = dt.Rows[0]["Shop_Owner_Email2"].ToString();
                    // End of Mantis Issue 25433
                    ret.AccountHolder = dt.Rows[0]["account_holder"].ToString();
                    ret.BankName = dt.Rows[0]["bank_name"].ToString();
                    ret.AccountNo = dt.Rows[0]["account_no"].ToString();
                    ret.IFSCCode = dt.Rows[0]["ifsc"].ToString();
                    ret.UPIID = dt.Rows[0]["upi_id"].ToString();

                    ret.retailer_id = dt.Rows[0]["retailer_id"].ToString();
                    ret.dealer_id = dt.Rows[0]["dealer_id"].ToString();

                    ret.Retaile = dt.Rows[0]["assigned_to_shop_id"].ToString();
                    ret.AssignedToRetaile = dt.Rows[0]["assignedShopName"].ToString();
                    //Rev work start 01.08.2022 0025120: Owner dob,owner aniversery date not fetch in edit mode in party master
                    ret.dobstr = dt.Rows[0]["dob"].ToString();
                    ret.date_aniversarystr = dt.Rows[0]["date_aniversary"].ToString();
                    //Rev work close 01.08.2022 0025120: Owner dob,owner aniversery date not fetch in edit mode in party master


                    //REV Priti
                    ret.BKT = dt.Rows[0]["BKT"].ToString();
                    ret.TOTALOUTSTANDING = Convert.ToDecimal(dt.Rows[0]["TOTALOUTSTANDING"]);
                    ret.POS = Convert.ToDecimal(dt.Rows[0]["POS"]);
                    ret.EMIAMOUNT = Convert.ToDecimal(dt.Rows[0]["EMIAMOUNT"]);
                    ret.ALLCHARGES = Convert.ToDecimal(dt.Rows[0]["ALLCHARGES"]);
                    
                    ret.TOTALCOLLECTABLE = Convert.ToDecimal(dt.Rows[0]["TOTALCOLLECTABLE"]);
                    ret.RISK = dt.Rows[0]["RISK"].ToString();
                    ret.WORKABLE = dt.Rows[0]["WORKABLE"].ToString();
                    ret.DISPOSITIONCODE = dt.Rows[0]["DISPOSITIONCODE"].ToString();
                    ret.PTPDATE = (dt.Rows[0]["PTPDATE"]).ToString();

                    ret.PTPAMOUNT = Convert.ToDecimal(dt.Rows[0]["PTPAMOUNT"]);
                    ret.COLLECTIONDATE = (dt.Rows[0]["COLLECTIONDATE"]).ToString();
                    ret.COLLECTIONAMOUNT = Convert.ToDecimal(dt.Rows[0]["COLLECTIONAMOUNT"]);
                    ret.FINALSTATUS = dt.Rows[0]["FINALSTATUS"].ToString();
                    //REV Priti END


                }
                return Json(ret, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult LasteEntityCode(String stateid, String ShopType)
        {
            try
            {
                DataTable dt = new DataTable();
                String output = "";
                dt = obj.LasteEntityCodeStateWise(stateid, ShopType);
                if (dt != null && dt.Rows.Count > 0)
                {
                    output = dt.Rows[0]["EntityCode"].ToString();
                }
                return Json(output, JsonRequestBehavior.AllowGet);
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
            //REV 7.0
            string WillShowLoanDetailsInParty = objSystemSettings.GetSystemSettingsResult("WillShowLoanDetailsInParty");
            //REV 7.0 END

            // Rev 4.0
            DataTable dtColmn = obj.GetPageRetention(Session["userid"].ToString(), "PARTY LIST");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            }
            // End of Rev 4.0

            var settings = new GridViewSettings();
            settings.Name = "gridPartyDetails";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Party Details";
            
            settings.Columns.Add(x =>
            {
                x.FieldName = "EmpName";
                x.Caption = "Emp. Name";
                x.VisibleIndex = 1;
                x.Width = 200;

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='EmpName'");
                    if (row != null && row.Length > 0)
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
                // End of Rev 4.0
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "EmpCode";
                x.Caption = "Emp. Code";
                x.VisibleIndex = 2;
                x.Width = 100;

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='EmpCode'");
                    if (row != null && row.Length > 0)
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
                // End of Rev 4.0
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Designation";
                x.Caption = "Designation";
                x.VisibleIndex = 3;
                x.Width = 100;

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Designation'");
                    if (row != null && row.Length > 0)
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
                // End of Rev 4.0
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Supervisor";
                x.Caption = "Supervisor";
                x.VisibleIndex = 4;
                x.Width = 200;

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Supervisor'");
                    if (row != null && row.Length > 0)
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
                // End of Rev 4.0
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "SupervisorCode";
                x.Caption = "Supervisor Code";
                x.VisibleIndex = 5;
                x.Width = 100;

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SupervisorCode'");
                    if (row != null && row.Length > 0)
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
                // End of Rev 4.0
            });
            //Mantis Issue 24928
            settings.Columns.Add(x =>
            {
                x.FieldName = "Address";
                x.Caption = "Address";
                x.VisibleIndex = 6;
                x.Width = 250;

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Address'");
                    if (row != null && row.Length > 0)
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
                // End of Rev 4.0
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Pincode";
                x.Caption = "Pincode";
                x.VisibleIndex = 7;
                x.Width = 100;

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Pincode'");
                    if (row != null && row.Length > 0)
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
                // End of Rev 4.0
            });
            //End of Mantis Issue 24928
            settings.Columns.Add(x =>
            {
                x.FieldName = "country";
                x.Caption = "Country";
                x.VisibleIndex = 8;
                x.Width = 150;

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='country'");
                    if (row != null && row.Length > 0)
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
                // End of Rev 4.0
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "state";
                x.Caption = "State";
                x.VisibleIndex = 9;
                x.Width = 150;

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='state'");
                    if (row != null && row.Length > 0)
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
                // End of Rev 4.0
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "city";
                x.Caption = "City";
                x.VisibleIndex = 10;
                x.Width = 150;

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='city'");
                    if (row != null && row.Length > 0)
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
                // End of Rev 4.0
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "area";
                x.Caption = "Area";
                x.VisibleIndex = 11;
                x.Width = 150;

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='area'");
                    if (row != null && row.Length > 0)
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
                // End of Rev 4.0
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "PP_CODE";
                x.Caption = "PP Code";
                x.VisibleIndex = 12;
                x.Width = 100;

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PP_CODE'");
                    if (row != null && row.Length > 0)
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
                // End of Rev 4.0
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "PP_Name";
                x.Caption = "PP Name";
                x.VisibleIndex = 13;
                x.Width = 200;

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PP_Name'");
                    if (row != null && row.Length > 0)
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
                // End of Rev 4.0
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "PP_LOCATION";
                x.Caption = "PP Location";
                x.VisibleIndex = 14;
                x.Width = 250;

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PP_LOCATION'");
                    if (row != null && row.Length > 0)
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
                // End of Rev 4.0
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "DD_CODE";
                x.Caption = "DD Code";
                x.VisibleIndex = 15;
                x.Width = 100;

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DD_CODE'");
                    if (row != null && row.Length > 0)
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
                // End of Rev 4.0
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "DD_Name";
                x.Caption = "DD Name";
                x.VisibleIndex = 16;
                x.Width = 200;

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DD_Name'");
                    if (row != null && row.Length > 0)
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
                // End of Rev 4.0
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "DD_LOCATION";
                x.Caption = "DD Location";
                x.VisibleIndex = 17;
                x.Width = 250;

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DD_LOCATION'");
                    if (row != null && row.Length > 0)
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
                // End of Rev 4.0
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Outlet_Code";
                x.Caption = "Outlet Code";
                x.VisibleIndex = 18;
                x.Width = 100;

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Outlet_Code'");
                    if (row != null && row.Length > 0)
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
                // End of Rev 4.0
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Outlet_Name";
                x.Caption = "Outlet Name";
                x.VisibleIndex = 19;
                x.Width = 200;

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Outlet_Name'");
                    if (row != null && row.Length > 0)
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
                // End of Rev 4.0
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Outlet_type";
                x.Caption = "Outlet Type";
                x.VisibleIndex = 20;
                x.Width = 100;

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Outlet_type'");
                    if (row != null && row.Length > 0)
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
                // End of Rev 4.0
            });

            //REV 6.0
            settings.Columns.Add(x =>
            {
                x.FieldName = "PARTYSTATUS";
                x.Caption = "Party Status";
                x.VisibleIndex = 20;
                x.Width = 100;               
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PARTYSTATUS'");
                    if (row != null && row.Length > 0)
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
            //REV 6.0 End


            // Mantis Issue 25421
            settings.Columns.Add(x =>
            {
                x.FieldName = "Beat";
                x.Caption = "Beat";
                x.VisibleIndex = 21;
                x.Width = 100;

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Beat'");
                    if (row != null && row.Length > 0)
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
                // End of Rev 4.0
            });
            // End of Mantis Issue 25421

            settings.Columns.Add(x =>
            {
                x.FieldName = "Outlet_owner";
                x.Caption = "Outlet Owner";
                x.VisibleIndex = 22;
                x.Width = 150;

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Outlet_owner'");
                    if (row != null && row.Length > 0)
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
                // End of Rev 4.0
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Outlet_ContactNo";
                x.Caption = "Outlet Contact No";
                x.VisibleIndex = 23;
                x.Width = 100;

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Outlet_ContactNo'");
                    if (row != null && row.Length > 0)
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
                // End of Rev 4.0
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "AlternateNo";
                x.Caption = "Alternate No";
                x.VisibleIndex = 24;
                x.Width = 100;

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='AlternateNo'");
                    if (row != null && row.Length > 0)
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
                // End of Rev 4.0
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "OutletLocation";
                x.Caption = "Outlet Location";
                x.VisibleIndex = 25;
                x.Width = 250;

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='OutletLocation'");
                    if (row != null && row.Length > 0)
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
                // End of Rev 4.0
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "OutletStatus";
                x.Caption = "Outlet Status";
                x.VisibleIndex = 26;
                x.Width = 100;

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='OutletStatus'");
                    if (row != null && row.Length > 0)
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
                // End of Rev 4.0
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "OutletSpecify";
                x.Caption = "Outlet Specify";
                x.VisibleIndex = 27;
                x.Width = 100;

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='OutletSpecify'");
                    if (row != null && row.Length > 0)
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
                // End of Rev 4.0
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "OwnerDOB";
                x.Caption = "Owner DOB";
                x.VisibleIndex = 28;
                x.Width = 100;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='OwnerDOB'");
                    if (row != null && row.Length > 0)
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
                // End of Rev 4.0
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "OwnerAnniversary";
                x.Caption = "Owner Anniversary";
                x.VisibleIndex = 29;
                x.Width = 100;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='OwnerAnniversary'");
                    if (row != null && row.Length > 0)
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
                // End of Rev 4.0
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "PanCard";
                x.Caption = "Pan Card";
                x.VisibleIndex = 30;
                x.Width = 100;

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PanCard'");
                    if (row != null && row.Length > 0)
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
                // End of Rev 4.0
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "AdhaarCard";
                x.Caption = "Aadhaar Card";
                x.VisibleIndex = 31;
                x.Width = 100;

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='AdhaarCard'");
                    if (row != null && row.Length > 0)
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
                // End of Rev 4.0
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "OutletLat";
                x.Caption = "Outlet Lat";
                x.VisibleIndex = 32;
                x.Width = 100;

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='OutletLat'");
                    if (row != null && row.Length > 0)
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
                // End of Rev 4.0
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "OutletLong";
                x.Caption = "Outlet Long";
                x.VisibleIndex = 33;
                x.Width = 100;

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='OutletLong'");
                    if (row != null && row.Length > 0)
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
                // End of Rev 4.0
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CreateDate";
                x.Caption = "Created On";
                x.VisibleIndex = 34;
                x.Width = 100;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy HH:mm:ss";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy HH:mm:ss";

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CreateDate'");
                    if (row != null && row.Length > 0)
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
                // End of Rev 4.0
            });

            //REV 7.0
            if (WillShowLoanDetailsInParty == "1")
            {
                settings.Columns.Add(x =>
                {
                    x.FieldName = "BKT";
                    x.Caption = "BKT";
                    x.VisibleIndex = 39;
                    x.Width = 100;

                    // Rev 2.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='BKT'");
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
                    // End of Rev 2.0
                });
                settings.Columns.Add(x =>
                {
                    x.FieldName = "TOTALOUTSTANDING";
                    x.Caption = "TOTAL OUTSTANDING";
                    x.VisibleIndex = 40;
                    x.Width = 100;
                    x.PropertiesEdit.DisplayFormatString = "0.00";
                    // Rev 2.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='TOTALOUTSTANDING'");
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
                    // End of Rev 2.0
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "POS";
                    x.Caption = "POS";
                    x.VisibleIndex = 41;
                    x.Width = 100;
                    x.PropertiesEdit.DisplayFormatString = "0.00";
                    // Rev 2.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='POS'");
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
                    // End of Rev 2.0
                });


                settings.Columns.Add(x =>
                {
                    x.FieldName = "ALLCHARGES";
                    x.Caption = "ALL CHARGES";
                    x.VisibleIndex = 42;
                    x.Width = 100;
                    x.PropertiesEdit.DisplayFormatString = "0.00";
                    // Rev 2.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ALLCHARGES'");
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
                    // End of Rev 2.0
                });


                settings.Columns.Add(x =>
                {
                    x.FieldName = "TOTALCOLLECTABLE";
                    x.Caption = "TOTAL COLLECTABLE";
                    x.VisibleIndex = 43;
                    x.Width = 100;
                    x.PropertiesEdit.DisplayFormatString = "0.00";
                    // Rev 2.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='TOTALCOLLECTABLE'");
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
                    // End of Rev 2.0
                });


                settings.Columns.Add(x =>
                {
                    x.FieldName = "WORKABLE";
                    x.Caption = "WORKABLE";
                    x.VisibleIndex = 44;
                    x.Width = 100;

                    // Rev 2.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='WORKABLE'");
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
                    // End of Rev 2.0
                });


                settings.Columns.Add(x =>
                {
                    x.FieldName = "PTPDATE";
                    x.Caption = "PTP DATE";
                    x.VisibleIndex = 45;
                    x.Width = 100;
                    x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                    // Rev 2.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PTPDATE'");
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
                    // End of Rev 2.0
                });


                settings.Columns.Add(x =>
                {
                    x.FieldName = "PTPAMOUNT";
                    x.Caption = "PTP AMOUNT";
                    x.VisibleIndex = 46;
                    x.Width = 100;
                    x.PropertiesEdit.DisplayFormatString = "0.00";
                    // Rev 2.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PTPAMOUNT'");
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
                    // End of Rev 2.0
                });


                settings.Columns.Add(x =>
                {
                    x.FieldName = "COLLECTIONDATE";
                    x.Caption = "COLLECTION DATE";
                    x.VisibleIndex = 47;
                    x.Width = 100;
                    x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                    // Rev 2.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='COLLECTIONDATE'");
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
                    // End of Rev 2.0
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "COLLECTIONAMOUNT";
                    x.Caption = "COLLECTION AMOUNT";
                    x.VisibleIndex = 48;
                    x.Width = 100;
                    x.PropertiesEdit.DisplayFormatString = "0.00";
                    // Rev 2.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='COLLECTIONAMOUNT'");
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
                    // End of Rev 2.0
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "EMIAMOUNT";
                    x.Caption = "EMI AMOUNT";
                    x.VisibleIndex = 49;
                    x.Width = 100;
                    x.PropertiesEdit.DisplayFormatString = "0.00";
                    // Rev 2.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='EMIAMOUNT'");
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
                    // End of Rev 2.0
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "RISK";
                    x.Caption = "RISK";
                    x.VisibleIndex = 50;
                    x.Width = 100;

                    // Rev 2.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='RISK'");
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
                    // End of Rev 2.0
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "DISPOSITIONCODE";
                    x.Caption = "DISPOSITION CODE";
                    x.VisibleIndex = 51;
                    x.Width = 100;

                    // Rev 2.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DISPOSITIONCODE'");
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
                    // End of Rev 2.0
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "FINALSTATUS";
                    x.Caption = "FINAL STATUS";
                    x.VisibleIndex = 52;
                    x.Width = 100;

                    // Rev 2.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='FINALSTATUS'");
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
                    // End of Rev 2.0
                });

            }
            //REV 7.0 END


            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

        [HttpPost]
        public ActionResult GetState(string Country)
        {
            List<StateListShop> StateLst = new List<StateListShop>();
            DataTable dtState = obj.GetStateList(Country);
            if (dtState.Rows.Count > 0)
            {
                StateLst = APIHelperMethods.ToModelList<StateListShop>(dtState);
            }
            return Json(StateLst, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult GetCity(string State)
        {
            List<CityListShop> CityLst = new List<CityListShop>();
            DataTable dtState = obj.GetCityList(State);
            if (dtState.Rows.Count > 0)
            {
                CityLst = APIHelperMethods.ToModelList<CityListShop>(dtState);
            }
            return Json(CityLst, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult GetArea(string City)
        {
            List<AreaListShop> AreaLst = new List<AreaListShop>();
            DataTable dtState = obj.GetAreaList(City);
            if (dtState.Rows.Count > 0)
            {
                AreaLst = APIHelperMethods.ToModelList<AreaListShop>(dtState);
            }
            return Json(AreaLst, JsonRequestBehavior.AllowGet);

        }

        public ActionResult DownloadFormat()
        {
            string FileName = "PartyList.xlsx";
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "image/jpeg";
            response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ";");
            response.TransmitFile(Server.MapPath("~/Commonfolder/PartyList.xlsx"));
            response.Flush();
            response.End();

            return null;
        }

        public ActionResult ImportExcel()
        {
            //if (fileprod.HasFile)
            //{
            //    string FileName = Path.GetFileName(fileprod.PostedFile.FileName);
            //    string Extension = Path.GetExtension(fileprod.PostedFile.FileName);
            //    string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
            //    string FilePath = Server.MapPath("~/Temporary/") + FileName;
            //    fileprod.SaveAs(FilePath);
            //    Import_To_Grid(FilePath, Extension, "No");

            //    File.Delete(FilePath);

            //}
            //BindProClassCode();
            //BindBrand();
            //BindGrid();
            //BindProductSize();

            //productlog.DataSource = null;
            //productlog.DataBind();

            //return null;

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
                    //using (SpreadsheetDocument doc = SpreadsheetDocument.Open(FilePath, false))
                    //{
                        //Sheet sheet = doc.WorkbookPart.Workbook.Sheets.GetFirstChild<Sheet>();
                        //Worksheet worksheet = (doc.WorkbookPart.GetPartById(sheet.Id.Value) as WorksheetPart).Worksheet;
                        //IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>().DefaultIfEmpty();
                        //foreach (Row row in rows)
                        //{
                        //    if (row.RowIndex.Value == 1)
                        //    {
                        //        foreach (Cell cell in row.Descendants<Cell>())
                        //        {
                        //            if (cell.CellValue != null)
                        //            {
                        //                dt.Columns.Add(GetValue(doc, cell));
                        //            }
                        //        }
                        //    }
                        //    else
                        //    {
                        //        dt.Rows.Add();
                        //        int i = 0;
                        //        foreach (Cell cell in row.Descendants<Cell>())
                        //        {
                        //            if (cell.CellValue != null)
                        //            {
                        //                dt.Rows[dt.Rows.Count - 1][i] = GetValue(doc, cell);
                        //            }
                        //            i++;
                        //        }
                        //    }
                        //}

                        //DataTable dtExcelData = new DataTable();
                        string conString = string.Empty;
                        conString = ConfigurationManager.AppSettings["ExcelConString"];
                        conString = string.Format(conString, FilePath);
                        using (OleDbConnection excel_con = new OleDbConnection(conString))
                        {
                            excel_con.Open();
                            string sheet1 = "List$"; //ī;

                            //dtExcelData.Columns.Add("Segment*", typeof(string));
                            //dtExcelData.Columns.Add("Unique ID*", typeof(string));
                            //dtExcelData.Columns.Add("Name*", typeof(string));
                            //dtExcelData.Columns.Add("Parent", typeof(string));
                            //dtExcelData.Columns.Add("GSTIN", typeof(string));
                            //dtExcelData.Columns.Add("Billing_Add1*", typeof(string));
                            //dtExcelData.Columns.Add("Billing_Add2", typeof(string));
                            //dtExcelData.Columns.Add("Billing_Country", typeof(string));
                            //dtExcelData.Columns.Add("Billing_State", typeof(string));
                            //dtExcelData.Columns.Add("Billing_Dist", typeof(string));
                            //dtExcelData.Columns.Add("Billing_Lat", typeof(string));
                            //dtExcelData.Columns.Add("Billing_Long", typeof(string));
                            //dtExcelData.Columns.Add("Billing_PIN*", typeof(string));
                            //dtExcelData.Columns.Add("Billing_Phone*", typeof(string));

                            //dtExcelData.Columns.Add("Service_Add1*", typeof(string));
                            //dtExcelData.Columns.Add("Service_Add2", typeof(string));
                            //dtExcelData.Columns.Add("Service_Country", typeof(string));
                            //dtExcelData.Columns.Add("Service_State", typeof(string));
                            //dtExcelData.Columns.Add("Service_Dist", typeof(string));
                            //dtExcelData.Columns.Add("Service_Lat", typeof(string));
                            //dtExcelData.Columns.Add("Service_Long", typeof(string));
                            //dtExcelData.Columns.Add("Service_PIN*", typeof(string));
                            //dtExcelData.Columns.Add("Service_Phone*", typeof(string));
                            //dtExcelData.Columns.Add("Treatment_Area", typeof(string));

                            //dtExcelData.Columns.Add("ServiceBranch", typeof(string));

                            using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
                            {
                                oda.Fill(dt);
                            }
                            excel_con.Close();
                        }

                   // }
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        //Mantis Issue 24674
                        //** New Datatable included to resolve no. format for Phone numbers. State and Contact blank check was implemented to filter out blank rows from the excel Sheet. **//
                        DataTable dtExcelData = new DataTable();
                        dtExcelData.Columns.Add("State", typeof(string));
                        dtExcelData.Columns.Add("City", typeof(string));
                        dtExcelData.Columns.Add("Area", typeof(string));
                        dtExcelData.Columns.Add("Cluster", typeof(string));
                        dtExcelData.Columns.Add("Type", typeof(string));
                        dtExcelData.Columns.Add("Shop type", typeof(string));
                        dtExcelData.Columns.Add("Entity Type", typeof(string));
                        dtExcelData.Columns.Add("Assigned To PP", typeof(string));
                        dtExcelData.Columns.Add("DD Type", typeof(string));
                        dtExcelData.Columns.Add("Assigned To DD", typeof(string));
                        dtExcelData.Columns.Add("Party Name", typeof(string));
                        dtExcelData.Columns.Add("Party Code", typeof(string));
                        dtExcelData.Columns.Add("Party Status", typeof(string));
                        dtExcelData.Columns.Add("Address", typeof(string));
                        dtExcelData.Columns.Add("Pin Code", typeof(string));
                        dtExcelData.Columns.Add("Owner", typeof(string));
                        dtExcelData.Columns.Add("DOB", typeof(string));
                        dtExcelData.Columns.Add("Anniversary", typeof(string));
                        dtExcelData.Columns.Add("Contact", typeof(string));
                        dtExcelData.Columns.Add("Alternate Contact", typeof(string));
                        dtExcelData.Columns.Add("Alternate Contact 1", typeof(string));
                        dtExcelData.Columns.Add("Email", typeof(string));
                        dtExcelData.Columns.Add("Alternate Email", typeof(string));
                        dtExcelData.Columns.Add("Status", typeof(string));
                        dtExcelData.Columns.Add("Entity Category", typeof(string));

                        dtExcelData.Columns.Add("Owner PAN", typeof(string));
                        dtExcelData.Columns.Add("Owner Aadhaar", typeof(string));
                        dtExcelData.Columns.Add("GSTIN", typeof(string));
                        dtExcelData.Columns.Add("Trade License", typeof(string));
                        dtExcelData.Columns.Add("Location", typeof(string));
                        dtExcelData.Columns.Add("Group/Beat", typeof(string));
                        dtExcelData.Columns.Add("Account Holder", typeof(string));
                        dtExcelData.Columns.Add("Bank Name", typeof(string));
                        dtExcelData.Columns.Add("Account No", typeof(string));
                        dtExcelData.Columns.Add("IFSC Code", typeof(string));
                        dtExcelData.Columns.Add("UPI ID", typeof(string));
                        dtExcelData.Columns.Add("Remarks", typeof(string));
                        dtExcelData.Columns.Add("Assign to User", typeof(string));
                        dtExcelData.Columns.Add("Party Location Lat", typeof(string));
                        dtExcelData.Columns.Add("Party Location Lang", typeof(string));
                        foreach (DataRow row in dt.Rows)
                        {
                            //row["Contact*"] = Convert.ToString(row["Contact*"]);
                            //row["Alternate Contact"] = Convert.ToString(row["Alternate Contact"]);
                            //row["Alternate Contact 1"] = Convert.ToString(row["Alternate Contact 1"]);
                            if (Convert.ToString(row["State*"]) != "" && Convert.ToString(row["Contact*"]) != "")
                            {
                                dtExcelData.Rows.Add(Convert.ToString(row["State*"]), Convert.ToString(row["City*"]), Convert.ToString(row["Area"]), Convert.ToString(row["Cluster*"]), Convert.ToString(row["Type*"]),
                                                Convert.ToString(row["Shop Type"]), Convert.ToString(row["Entity Type"]), Convert.ToString(row["Assigned To PP"]), Convert.ToString(row["DD Type"]), Convert.ToString(row["Assigned To DD"]),
                                                Convert.ToString(row["Party Name*"]), Convert.ToString(row["Party Code*"]), Convert.ToString(row["Party Status"]), Convert.ToString(row["Address*"]), Convert.ToString(row["Pin Code*"])
                                                , Convert.ToString(row["Owner*"]), Convert.ToString(row["DOB"]), Convert.ToString(row["Anniversary"]), Convert.ToString(row["Contact*"]), Convert.ToString(row["Alternate Contact"])
                                                , Convert.ToString(row["Alternate Contact 1"]), Convert.ToString(row["Email"]), Convert.ToString(row["Alternate Email"]), Convert.ToString(row["Status*"]), Convert.ToString(row["Entity Category"])
                                                , Convert.ToString(row["Owner PAN"]), Convert.ToString(row["Owner Aadhaar"]), Convert.ToString(row["GSTIN"]), Convert.ToString(row["Trade License"]), Convert.ToString(row["Location"])
                                                , Convert.ToString(row["Group/Beat"]), Convert.ToString(row["Account Holder"]), Convert.ToString(row["Bank Name"]), Convert.ToString(row["Account No"]), Convert.ToString(row["IFSC Code"])
                                                , Convert.ToString(row["UPI ID"]), Convert.ToString(row["Remarks"]), Convert.ToString(row["Assign to User*"]), Convert.ToString(row["Party Location Lat"]), Convert.ToString(row["Party Location Lang"]));
                            }
                           
                        }
                        //End of Mantis Issue 24674
                        try
                        {
                            //Mantis Issue 24674
                            //TempData["PartyImportLog"] = dt;
                            TempData["PartyImportLog"] = dtExcelData;
                            //End of Mantis Issue 24674
                            TempData.Keep();

                            DataTable dtCmb = new DataTable();
                            ProcedureExecute proc = new ProcedureExecute("PRC_FTSImportNewParty");
                            proc.AddPara("@IMPORT_TABLE", dtExcelData);
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

        // Rev 1.0
        
        public ActionResult DownloadBulkModifyTempate(string StateId)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSBulkModifyParty");
            proc.AddPara("@STATE", StateId);
            proc.AddPara("@ACTION", "FetchDataStatewise");
            proc.AddPara("@CreateUser_Id", Convert.ToInt32(Session["userid"]));
            dt = proc.GetTable();

            // This block will show error when run from loacl machine in debug mode. But will run properly in test server.
            // Refer of ClosedXML.dll added in MyshopProject
            dt.TableName = "List";
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=BulkImportTemplate.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }


            return null;
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch
            {
                obj = null;
                //MessageBox.Show("Exception Occured while releasing object " + ex.ToString());  
            }
            finally
            {
                GC.Collect();
            }
        }

        public ActionResult BulkModifyParty()
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
                        BulkModify_To_Grid(fname, extension, file);
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

        public Int32 BulkModify_To_Grid(string FilePath, string Extension, HttpPostedFileBase file)
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

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataTable dtExcelData = new DataTable();
                        dtExcelData.Columns.Add("Shop_Code", typeof(string));
                        dtExcelData.Columns.Add("Retailer", typeof(string));
                        dtExcelData.Columns.Add("Party_Status", typeof(string));
                        
                        foreach (DataRow row in dt.Rows)
                        {
                            if (Convert.ToString(row["Shop_Code"]) != "")
                            {
                                dtExcelData.Rows.Add(Convert.ToString(row["Shop_Code"]), Convert.ToString(row["Retailer"]), Convert.ToString(row["Party_Status"]));
                            }

                        }
                        try
                        {
                            //TempData["BulkModifyPartyLog"] = dtExcelData;
                            //TempData.Keep();

                            DataTable dtCmb = new DataTable();
                            ProcedureExecute proc = new ProcedureExecute("PRC_FTSBulkModifyParty");
                            proc.AddPara("@BULKMODIFYPARTY_TABLE", dtExcelData);
                            proc.AddPara("@ACTION", "BulkUpdate");
                            proc.AddPara("@CreateUser_Id", Convert.ToInt32(Session["userid"]));
                            dtCmb = proc.GetTable();

                            TempData["BulkModifyPartyLog"] = dtCmb;
                            TempData.Keep();

                        }
                        catch (Exception)
                        {

                        }
                    }
                }
            }
            return HasLog;
        }

        [HttpPost]
        public JsonResult BulkModifyUserLog(string Fromdt, String ToDate)
        {
            string output_msg = string.Empty;
            try
            {
                string datfrmat = Fromdt.Split('-')[2] + '-' + Fromdt.Split('-')[1] + '-' + Fromdt.Split('-')[0];
                string dattoat = ToDate.Split('-')[2] + '-' + ToDate.Split('-')[1] + '-' + ToDate.Split('-')[0];
                DataTable dt = obj.BulkModifyPartyLog(datfrmat, dattoat);
                if (dt != null && dt.Rows.Count > 0)
                {
                    TempData["BulkModifyPartyLog"] = dt;
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

        public ActionResult BulkModifyLog()
        {
            List<BulkModifyPartyLog> list = new List<BulkModifyPartyLog>();
            DataTable dt = new DataTable();
            try
            {
                if (TempData["BulkModifyPartyLog"] != null)
                {
                    dt = (DataTable)TempData["BulkModifyPartyLog"];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        BulkModifyPartyLog data = null;
                        foreach (DataRow row in dt.Rows)
                        {
                            data = new BulkModifyPartyLog();
                            data.Shop_Code = Convert.ToString(row["Shop_Code"]);
                            data.Shop_Name = Convert.ToString(row["Shop_Name"]);
                            data.Shop_Type = Convert.ToString(row["Shop_Type"]);
                            data.Shop_Owner_Contact = Convert.ToString(row["Shop_Owner_Contact"]);
                            data.State = Convert.ToString(row["State"]);
                            data.Entitycode = Convert.ToString(row["Entitycode"]);
                            data.Retailer = Convert.ToString(row["Retailer"]);
                            data.Party_Status = Convert.ToString(row["Party_Status"]);

                            data.Status = Convert.ToString(row["Status"]);
                            data.Reason = Convert.ToString(row["Reason"]);
                            data.UpdateOn = Convert.ToString(row["UpdateOn"]);
                            data.UpdatedBy = Convert.ToString(row["UpdatedBy"]);

                            list.Add(data);
                        }
                    }
                    TempData["BulkModifyPartyLog"] = dt;
                }
            }
            catch (Exception ex)
            {

            }
            TempData.Keep();
            return PartialView(list);
        }

        public ActionResult ExportBulkModifyLogGrid(int type)
        {
            ViewData["BulkModifyPartyLog"] = TempData["BulkModifyPartyLog"];

            TempData.Keep();

            if (ViewData["BulkModifyPartyLog"] != null)
            {
                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetBulkModifyLogGrid(ViewData["BulkModifyPartyLog"]), ViewData["BulkModifyPartyLog"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetBulkModifyLogGrid(ViewData["BulkModifyPartyLog"]), ViewData["BulkModifyPartyLog"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXlsx(GetBulkModifyLogGrid(ViewData["BulkModifyPartyLog"]), ViewData["BulkModifyPartyLog"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetBulkModifyLogGrid(ViewData["BulkModifyPartyLog"]), ViewData["BulkModifyPartyLog"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetBulkModifyLogGrid(ViewData["BulkModifyPartyLog"]), ViewData["BulkModifyPartyLog"]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetBulkModifyLogGrid(object datatable)
        {
            var settings = new GridViewSettings();
            settings.Name = "BulkModifyLog";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Bulk Update Party Log";

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_Code";
                x.Caption = "Shop Code";
                x.VisibleIndex = 1;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(200);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_Name";
                x.Caption = "Shop Name";
                x.VisibleIndex = 2;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(200);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_Type";
                x.Caption = "Shop Type";
                x.VisibleIndex = 3;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(200);
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_Owner_Contact";
                x.Caption = "Shop Owner Contact";
                x.VisibleIndex = 4;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(200);
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "State";
                x.Caption = "State";
                x.VisibleIndex = 5;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(200);
            });
           
            settings.Columns.Add(x =>
            {
                x.FieldName = "Entitycode";
                x.Caption = "Entitycode";
                x.VisibleIndex = 6;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(150);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Retailer";
                x.Caption = "Retailer";
                x.VisibleIndex = 7;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(100);

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Party_Status";
                x.Caption = "Party Status";
                x.VisibleIndex = 8;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(160);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Status";
                x.Caption = "Status";
                x.VisibleIndex = 9;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(80);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Reason";
                x.Caption = "Reason";
                x.VisibleIndex = 10;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(80);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "UpdateOn";
                x.Caption = "Update On";
                x.VisibleIndex = 11;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(140);
                x.ColumnType = MVCxGridViewColumnType.DateEdit;

                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy hh:mm tt";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy hh:mm tt";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "UpdatedBy";
                x.Caption = "Updated By";
                x.VisibleIndex = 12;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(80);
            });

           
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }
        // End of Rev 1.0

        string uploadtext = "~/CommonFolder/Log/ShopRegistration.txt";
        [AcceptVerbs("POST")]
        public JsonResult RegisterShop(RegisterShopInputData model)
        {
            String weburl = System.Configuration.ConfigurationSettings.AppSettings["PortalShopAdd"];
            string apiUrl = weburl;
            //"http://3.7.30.86:82/ShopRegisterPortal/RegisterShop"

            HttpClient httpClient = new HttpClient();
            MultipartFormDataContent form = new MultipartFormDataContent();
            byte[] fileBytes = new byte[model.shop_image.InputStream.Length + 1];

            var fileContent = new StreamContent(model.shop_image.InputStream);
            // fileContent.Headers.ContentType.MediaType = model.shop_image.ContentType;



            form.Add(new StringContent(model.data), "data");
            //form.Add(new ByteArrayContent(fileBytes, 0, fileBytes.Length), "shop_image", model.shop_image.FileName);
            form.Add(fileContent, "shop_image", model.shop_image.FileName);
            var result = httpClient.PostAsync(apiUrl, form).Result;


            //using (HttpClient client = new HttpClient())
            //{
            //    using (var content = new MultipartFormDataContent())
            //    {
            //        byte[] fileBytes = new byte[model.shop_image.InputStream.Length + 1];
            //        model.shop_image.InputStream.Read(fileBytes, 0, fileBytes.Length);
            //        var fileContent = new ByteArrayContent(fileBytes);
            //        //fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment") { FileName = model.shop_image.FileName };


            //        content.Add(fileContent,"shop_image");
            //        content.Add(new StringContent(model.data), "data");
            //        var result = client.PostAsync(apiUrl, content).Result;
            //        if (result.StatusCode == System.Net.HttpStatusCode.Created)
            //        {
            //            ViewBag.Message = "Created";
            //        }
            //        else
            //        {
            //            ViewBag.Message = "Failed";
            //        }
            //    }
            //}

            return Json(result.ReasonPhrase);
        }

        [AcceptVerbs("POST")]
        public JsonResult EditShop(RegisterShopInputData model)
        {
            String weburl = System.Configuration.ConfigurationSettings.AppSettings["PortalShopEdit"];
            string apiUrl = weburl;
            //"http://localhost:16126/ShopRegisterPortal/EditShop"

            //HttpClient httpClient = new HttpClient();
            //MultipartFormDataContent form = new MultipartFormDataContent();
            //byte[] fileBytes = new byte[model.shop_image.InputStream.Length + 1];
            //form.Add(new StringContent(model.data), "data");
            //form.Add(new ByteArrayContent(fileBytes, 0, fileBytes.Length), "shop_image", model.shop_image.FileName);
            //var result = httpClient.PostAsync(apiUrl, form).Result;

            HttpClient httpClient = new HttpClient();
            MultipartFormDataContent form = new MultipartFormDataContent();
            byte[] fileBytes = new byte[model.shop_image.InputStream.Length + 1];
            var fileContent = new StreamContent(model.shop_image.InputStream);
            form.Add(new StringContent(model.data), "data");
            form.Add(fileContent, "shop_image", model.shop_image.FileName);
            var result = httpClient.PostAsync(apiUrl, form).Result;

            return Json(result.ReasonPhrase);
        }

        [HttpPost]
        public ActionResult AddParty(RegisterShopInput data)
        {
            try
            {

                string DOB = null;
                string Anniversary = null;
                if (data.dob != null && data.dob != "01-01-0100")
                {
                    DOB = data.dob.Split('-')[2] + '-' + data.dob.Split('-')[1] + '-' + data.dob.Split('-')[0];
                }

                if (data.date_aniversary != null && data.date_aniversary != "01-01-0100")
                {
                    Anniversary = data.date_aniversary.Split('-')[2] + '-' + data.date_aniversary.Split('-')[1] + '-' + data.date_aniversary.Split('-')[0];
                }

                //REV 7.0
                string _PTPDATE = null;
                string _COLLECTIONDATE = null;
                if (data.PTPDATE != null && data.PTPDATE != "01-01-0100")
                {
                    _PTPDATE = data.PTPDATE.Split('-')[2] + '-' + data.PTPDATE.Split('-')[1] + '-' + data.PTPDATE.Split('-')[0];
                }

                if (data.COLLECTIONDATE != null && data.COLLECTIONDATE != "01-01-0100")
                {
                    _COLLECTIONDATE = data.COLLECTIONDATE.Split('-')[2] + '-' + data.COLLECTIONDATE.Split('-')[1] + '-' + data.COLLECTIONDATE.Split('-')[0];
                }
                //REV 7.0 END

                // Mantis Issue 24450,24451
                string rtrnvalue = "";
                // End of Mantis Issue 24450,24451
                string Userid = Convert.ToString(Session["userid"]);
                //int i = obj.AddNewShop(Shop_code, State,  type,  AssignedTo,  Party_Name,  Party_Code,  Address,  Pin_Code,  owner_name, DOB,
                //    Anniversary,  owner_contact_no,  Alternate_Contact,  owner_email,  ShopStatus,  EntyType,  Owner_PAN,  Owner_Adhar,  Remarks,
                //     NewUser,  OldUser,  shop_lat,  shop_long);
                ProcedureExecute proc = new ProcedureExecute("PRC_FTSInsertUpdateNewParty");
                if (data.shop_id == null || data.shop_id == "")
                {
                    proc.AddPara("@ACTION", "InsertShop");
                }
                else
                {
                    proc.AddPara("@ACTION", "UpdateShop");
                }
                proc.AddPara("@ShopCode", data.shop_id);
                proc.AddPara("@stateId", data.State_ID);
                proc.AddPara("@ShopType", data.type);
                proc.AddPara("@assigned_to_pp_id", data.assigned_to_pp_id);
                proc.AddPara("@assigned_to_dd_id", data.assigned_to_dd_id);
                proc.AddPara("@Shop_Name", data.shop_name);
                proc.AddPara("@EntityCode", data.EntityCode);
                proc.AddPara("@Address", data.address);
                proc.AddPara("@Pincode", data.pin_code);
                proc.AddPara("@Shop_Owner", data.owner_name);
                proc.AddPara("@dob", DOB);
                proc.AddPara("@date_aniversary", Anniversary);
                proc.AddPara("@Shop_Owner_Contact", data.owner_contact_no);
                proc.AddPara("@Alt_MobileNo", data.Alt_MobileNo);
                proc.AddPara("@Shop_Owner_Email", data.owner_email);
                proc.AddPara("@Entity_Status", data.Entity_Status);
                proc.AddPara("@Entity_Type", data.Entity_Type);
                proc.AddPara("@ShopOwner_PAN", data.ShopOwner_PAN);
                proc.AddPara("@ShopOwner_Aadhar", data.ShopOwner_Aadhar);
                proc.AddPara("@Remarks", data.Remarks);
                proc.AddPara("@user_id", data.user_id);
                proc.AddPara("@Shop_City", data.CityId);
                proc.AddPara("@Area_id", data.AreaId);
                proc.AddPara("@Entity_Location", data.Entity_Location);
                proc.AddPara("@OLD_CreateUser", data.Old_userID);
                proc.AddPara("@Shop_Lat", data.shop_lat);
                proc.AddPara("@Shop_Long", data.shop_long);
                proc.AddPara("@CraetedUser_id", Userid);
                proc.AddPara("@Shop_Image", "");


                proc.AddPara("@retailer_id", data.retailer_id);
                proc.AddPara("@dealer_id", data.dealer_id);
                proc.AddPara("@Entity", data.entity_id);
                proc.AddPara("@PartyStatus", data.party_status_id);
                proc.AddPara("@GroupBeat", data.beat_id);
                proc.AddPara("@AccountHolder", data.AccountHolder);
                proc.AddPara("@BankName", data.BankName);
                proc.AddPara("@AccountNo", data.AccountNo);
                proc.AddPara("@IFSCCode", data.IFSCCode);
                proc.AddPara("@UPIID", data.UPIID);
                proc.AddPara("@assigned_to_shop_id", data.assigned_to_shop_id);
                //Mantis Issue 24571
                proc.AddPara("@GSTN_NUMBER", data.GSTN_NUMBER);
                proc.AddPara("@Trade_Licence_Number", data.Trade_Licence_Number);
                proc.AddPara("@Cluster", data.Cluster);
                proc.AddPara("@Alt_MobileNo1", data.Alt_MobileNo1);
                proc.AddPara("@Shop_Owner_Email2", data.Shop_Owner_Email2);
                //End of Mantis Issue 24571


                //REV 7.0
                proc.AddPara("@BKT", data.BKT);
                proc.AddPara("@TOTALOUTSTANDING", data.TOTALOUTSTANDING);
                proc.AddPara("@POS", data.POS);
                proc.AddPara("@EMIAMOUNT", data.EMIAMOUNT);
                proc.AddPara("@ALLCHARGES", data.ALLCHARGES);
                proc.AddPara("@TOTALCOLLECTABLE", data.TOTALCOLLECTABLE);
                proc.AddPara("@RISK", data.RiskId);
                proc.AddPara("@WORKABLE", data.WORKABLE);
                proc.AddPara("@DISPOSITIONCODE", data.DISPOSITIONID);

                proc.AddPara("@PTPDATE", _PTPDATE);
                proc.AddPara("@PTPAMOUNT", data.PTPAMOUNT);
                proc.AddPara("@COLLECTIONDATE", _COLLECTIONDATE);
                proc.AddPara("@COLLECTIONAMOUNT", data.COLLECTIONAMOUNT);
                proc.AddPara("@FINALSTATUS", data.FINALSTATUSID);

                //REV 7.0 END

                // Mantis Issue 24450,24451
                proc.AddVarcharPara("@RETURN_VALUE", 50, "", QueryParameterDirection.Output);
                // End of Mantis Issue 24450,24451

                int k = proc.RunActionQuery();

                // }
                // Mantis Issue 24450,24451 rtrnvalue = Convert.ToInt32(proc.GetParaValue("@RETURN_VALUE"));
                //return Json(k, JsonRequestBehavior.AllowGet);
                rtrnvalue = Convert.ToString(proc.GetParaValue("@RETURN_VALUE"));
                return Json(rtrnvalue, JsonRequestBehavior.AllowGet);
                // End of Mantis Issue 24450,24451
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetPartyImportLog()
        {
            List<partyDetailsModel> list = new List<partyDetailsModel>();
            DataTable dt = new DataTable();
            try
            {
                if (TempData["PartyImportLog"] != null)
                {
                    DataTable dtCmb = new DataTable();
                    //Mantis Issue 24572
                    //ProcedureExecute proc = new ProcedureExecute("PRC_FTSImportNewPartyLog");
                    ProcedureExecute proc = new ProcedureExecute("PRC_FTSImportNewPartyLog_New");
                    //End of Mantis Issue 24572
                    proc.AddPara("@IMPORT_TABLE", (DataTable)TempData["PartyImportLog"]);
                    proc.AddPara("@CreateUser_Id", Convert.ToInt32(Session["userid"]));
                    dt = proc.GetTable();
                    //dt = (DataTable)TempData["PartyImportLog"];
                    //Mantis Issue 24674
                    //dt = (DataTable)TempData["PartyImportLog"];
                    //End of Mantis Issue 24674
                    TempData.Keep();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        partyDetailsModel data = null;
                        foreach (DataRow row in dt.Rows)
                        {
                            data = new partyDetailsModel();
                            data.State = Convert.ToString(row["State"]);
                            data.City = Convert.ToString(row["City"]);
                            data.Area = Convert.ToString(row["Area"]);
                            data.Type = Convert.ToString(row["Type"]);
                            data.AssignedToPP = Convert.ToString(row["Assigned To PP"]);
                            data.AssignedToDD = Convert.ToString(row["Assigned To DD"]);
                            data.PartyName = Convert.ToString(row["Party Name"]);
                            data.PartyCode = Convert.ToString(row["Party Code"]);
                            data.Address = Convert.ToString(row["Address"]);
                            data.PinCode = Convert.ToString(row["Pin Code"]);
                            data.Owner = Convert.ToString(row["Owner"]);
                            data.DOB = Convert.ToString(row["DOB"]);
                            data.Anniversary = Convert.ToString(row["Anniversary"]);
                            data.Contact = Convert.ToString(row["Contact"]);
                            data.AlternateContact = Convert.ToString(row["Alternate Contact"]);
                            data.Email = Convert.ToString(row["Email"]);
                            data.Status = Convert.ToString(row["Status"]);
                            data.EntityType = Convert.ToString(row["Entity Type"]);
                            data.OwnerPAN = Convert.ToString(row["Owner PAN"]);
                            data.OwnerAadhaar = Convert.ToString(row["Owner Aadhaar"]);
                            data.Remarks = Convert.ToString(row["Remarks"]);
                            data.AssigntoUser = Convert.ToString(row["Assign to User"]);
                            data.PartyLocationLat = Convert.ToString(row["Party Location Lat"]);
                            data.PartyLocationLang = Convert.ToString(row["Party Location Lang"]);
                            data.Location = Convert.ToString(row["Location"]);
                            //Mantis Issue 24674
                            data.ImportStatus = Convert.ToString(row["ImportStatus"]);
                            //data.ImportStatus = Convert.ToString(row["Status"]);
                            ////End of Mantis Issue 24674
                            data.ImportMsg = Convert.ToString(row["ImportMsg"]);
                            data.ImportDate = Convert.ToString(row["ImportDate"]);
                            //Mantis Issue 24572
                            data.GSTIN = Convert.ToString(row["GSTN_NUMBER"]);
                            data.TradeLicense = Convert.ToString(row["Trade_Licence_Number"]);
                            data.Cluster = Convert.ToString(row["Cluster"]);
                            data.AlternateContact1 = Convert.ToString(row["Alt_MobileNo1"]);
                            data.AlternateEmail = Convert.ToString(row["Shop_Owner_Email2"]);
                            //End of Mantis Issue 24572
                            list.Add(data);
                        }
                    }
                    //TempData["PartyImportLog"] = dt;
                }

            }
            catch (Exception ex)
            {

            }
            TempData.Keep();
            return PartialView(list);
        }

        public ActionResult ExportPartyLogList(int type)
        {
            ViewData["PartyImportLog"] = TempData["PartyImportLog"];

            TempData.Keep();

            if (ViewData["PartyImportLog"] != null)
            {
                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetPartyLogGridView(ViewData["PartyImportLog"]), ViewData["PartyImportLog"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetPartyLogGridView(ViewData["PartyImportLog"]), ViewData["PartyImportLog"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXlsx(GetPartyLogGridView(ViewData["PartyImportLog"]), ViewData["PartyImportLog"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetPartyLogGridView(ViewData["PartyImportLog"]), ViewData["PartyImportLog"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetPartyLogGridView(ViewData["PartyImportLog"]), ViewData["PartyImportLog"]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetPartyLogGridView(object datatable)
        {
            var settings = new GridViewSettings();
            settings.Name = "gridPartyDetailsImportLog";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Party Details Import Log";

            settings.Columns.Add(x =>
            {
                x.FieldName = "State";
                x.Caption = "State";
                x.VisibleIndex = 1;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(100);

            });


            settings.Columns.Add(x =>
            {
                x.FieldName = "City";
                x.Caption = "City";
                x.VisibleIndex = 3;
                x.EditFormSettings.Visible = DefaultBoolean.False;

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Area";
                x.Caption = "Area";
                x.VisibleIndex = 4;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(160);

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Type";
                x.Caption = "Type";
                x.VisibleIndex = 5;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(80);
                //x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                //x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "AssignedToPP";
                x.Caption = "Assigned To PP";
                x.VisibleIndex = 6;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(80);

                //x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                //x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "PartyName";
                x.Caption = "Party Name";
                x.VisibleIndex = 7;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(100);

                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
            });



            settings.Columns.Add(x =>
            {
                x.FieldName = "PartyCode";
                x.Caption = "Party Code";
                x.VisibleIndex = 8;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(80);

                //x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                //x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Address";
                x.Caption = "Address";
                x.VisibleIndex = 9;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(200);

                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;

            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "PinCode";
                x.Caption = "Pin Code";
                x.VisibleIndex = 10;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(100);

                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Owner";
                x.Caption = "Owner";
                x.VisibleIndex = 11;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(150);

                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Anniversary";
                x.Caption = "Anniversary";
                x.VisibleIndex = 12;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(160);

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Contact";
                x.Caption = "Contact";
                x.VisibleIndex = 13;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(80);
                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "AlternateContact";
                x.Caption = "Alternate Contact";
                x.VisibleIndex = 14;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(80);

                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Email";
                x.Caption = "Email";
                x.VisibleIndex = 15;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(100);

                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
            });



            settings.Columns.Add(x =>
            {
                x.FieldName = "Status";
                x.Caption = "Party Status";
                x.VisibleIndex = 16;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(80);

                //x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                //x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "EntityType";
                x.Caption = "Entity Type";
                x.VisibleIndex = 17;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(100);

                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;

            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "OwnerPAN";
                x.Caption = "Owner PAN";
                x.VisibleIndex = 18;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(100);

                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "OwnerAadhaar";
                x.Caption = "Owner Aadhaar";
                x.VisibleIndex = 19;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(150);

                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Remarks";
                x.Caption = "Remarks";
                x.VisibleIndex = 20;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(200);

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "AssigntoUser";
                x.Caption = "Assign to User";
                x.VisibleIndex = 21;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(80);
                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "PartyLocationLat";
                x.Caption = "Party Location Lat";
                x.VisibleIndex = 22;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(80);

                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "PartyLocationLang";
                x.Caption = "Party Location Long";
                x.VisibleIndex = 23;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(100);

                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
            });



            settings.Columns.Add(x =>
            {
                x.FieldName = "Location";
                x.Caption = "Location";
                x.VisibleIndex = 24;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(80);

                //x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                //x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Status";
                x.Caption = "ImportStatus";
                x.VisibleIndex = 25;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(80);

                //x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                //x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });


            settings.Columns.Add(x =>
            {
                x.FieldName = "ImportMsg";
                x.Caption = "Reason";
                x.VisibleIndex = 26;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(160);

                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
            });


            settings.Columns.Add(x =>
            {
                x.FieldName = "ImportDate";
                x.Caption = "Import Date & Time";
                x.VisibleIndex = 27;
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

        // Rev 3.0
        public ActionResult DownloadMassDeleteFormat()
        {
            string FileName = "PartyDelete.xlsx";
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "image/jpeg";
            response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ";");
            response.TransmitFile(Server.MapPath("~/Commonfolder/PartyDelete.xlsx"));
            response.Flush();
            response.End();

            return null;
        }

        [HttpPost]
        public JsonResult MassDeletePartyLog(string Fromdt, String ToDate)
        {
            string output_msg = string.Empty;
            try
            {
                string datfrmat = Fromdt.Split('-')[2] + '-' + Fromdt.Split('-')[1] + '-' + Fromdt.Split('-')[0];
                string dattoat = ToDate.Split('-')[2] + '-' + ToDate.Split('-')[1] + '-' + ToDate.Split('-')[0];

                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_FTSBulkModifyParty");
                proc.AddPara("@ACTION", "GetMassDeletePartyLog");
                proc.AddPara("@FromDate", datfrmat);
                proc.AddPara("@ToDate", dattoat);
                dt = proc.GetTable();

                if (dt != null && dt.Rows.Count > 0)
                {
                    TempData["MassDeletePartyLog"] = dt;
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

        public ActionResult MassDeleteLog()
        {
            List<BulkModifyPartyLog> list = new List<BulkModifyPartyLog>();
            DataTable dt = new DataTable();
            try
            {
                if (TempData["MassDeletePartyLog"] != null)
                {
                    dt = (DataTable)TempData["MassDeletePartyLog"];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        BulkModifyPartyLog data = null;
                        foreach (DataRow row in dt.Rows)
                        {
                            data = new BulkModifyPartyLog();
                            data.Shop_Code = Convert.ToString(row["Shop_Id"]);
                            data.Reason = Convert.ToString(row["Reason"]);
                            data.UpdateOn = Convert.ToString(row["UpdateOn"]);
                            data.UpdatedBy = Convert.ToString(row["UpdatedBy"]);

                            list.Add(data);
                        }
                    }
                    TempData["BulkModifyPartyLog"] = dt;
                }
            }
            catch (Exception ex)
            {

            }
            TempData.Keep();
            return PartialView(list);
        }

        public ActionResult ExportMassDeleteLogGrid(int type)
        {
            ViewData["MassDeletePartyLog"] = TempData["MassDeletePartyLog"];

            TempData.Keep();

            if (ViewData["MassDeletePartyLog"] != null)
            {
                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetMassDeleteLogGrid(ViewData["MassDeletePartyLog"]), ViewData["MassDeletePartyLog"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetMassDeleteLogGrid(ViewData["MassDeletePartyLog"]), ViewData["MassDeletePartyLog"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXlsx(GetMassDeleteLogGrid(ViewData["MassDeletePartyLog"]), ViewData["MassDeletePartyLog"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetMassDeleteLogGrid(ViewData["MassDeletePartyLog"]), ViewData["MassDeletePartyLog"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetMassDeleteLogGrid(ViewData["MassDeletePartyLog"]), ViewData["MassDeletePartyLog"]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetMassDeleteLogGrid(object datatable)
        {
            var settings = new GridViewSettings();
            settings.Name = "MassDeleteLog";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Mass Delete Party Log";

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_ID";
                x.Caption = "Shop Code";
                x.VisibleIndex = 1;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(200);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Reason";
                x.Caption = "Reason";
                x.VisibleIndex = 10;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(80);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "UpdateOn";
                x.Caption = "Update On";
                x.VisibleIndex = 11;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(140);
                x.ColumnType = MVCxGridViewColumnType.DateEdit;

                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy hh:mm tt";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy hh:mm tt";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "UpdatedBy";
                x.Caption = "Updated By";
                x.VisibleIndex = 12;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(80);
            });


            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }
        // End of Rev 3.0

        [HttpPost]
        public JsonResult PartyDelete(string ShopCode)
        {
            string output_msg = string.Empty;
            // Rev 3.0
            string image_name = string.Empty;
            // End of Rev 3.0
            try
            {
                DataTable dt = obj.PartyDelate(ShopCode);
                if (dt != null && dt.Rows.Count > 0)
                {
                    output_msg = dt.Rows[0]["MSG"].ToString();
                    // Rev 3.0
                    if (dt.Columns.Contains("SHOP_IMAGE"))
                    {
                        //string sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/CommonFolder/");
                        //String Path = System.Configuration.ConfigurationManager.AppSettings["Path"];
                        String SiteURL = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];

                        foreach (DataRow item in dt.Rows)
                        {
                            if (System.IO.File.Exists(SiteURL + Convert.ToString(item["SHOP_IMAGE"])))
                            {
                                // SAVE THE FILES IN THE FOLDER.
                                System.IO.File.Delete(SiteURL + Convert.ToString(item["SHOP_IMAGE"]));
                            }
                        }
                    }
                    // End of Rev 3.0
                }
            }
            catch (Exception ex)
            {
                output_msg = "Please try again later";
            }

            return Json(output_msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ShopReAssignUser(ReAssignShop data)
        {
            string output_msg = string.Empty;
            try
            {
                string ShopCodes = "";
                int j = 1;

                if (data.ShopCode != null && data.ShopCode.Count > 0)
                {
                    foreach (string item in data.ShopCode)
                    {
                        if (j > 1)
                            ShopCodes = ShopCodes + "," + item;
                        else
                            ShopCodes = item;
                        j++;
                    }
                }

                DataTable dt = obj.ShopReAssignUser(Convert.ToString(Session["userid"]), data.OldUser, data.NewUser, ShopCodes);
                if (dt != null && dt.Rows.Count > 0)
                {
                    TempData["ReAssignShopUserLog"] = dt;
                    TempData.Keep();
                    output_msg = "Update Succesfully.";
                }
                else
                {
                    output_msg = "Party not found.";
                }
            }
            catch (Exception ex)
            {
                output_msg = "Please try again later";
            }
            return Json(output_msg, JsonRequestBehavior.AllowGet);
        }

        // Rev 3.0
        public ActionResult MassDeleteImportParty()
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
                        BulkDelete_To_Grid(fname, extension, file);
                    }
                    return Json("Shop deleted Successfully!");
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

        public Int32 BulkDelete_To_Grid(string FilePath, string Extension, HttpPostedFileBase file)
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

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataTable dtExcelData = new DataTable();
                        dtExcelData.Columns.Add("SHOP_ID", typeof(string));
                       
                        foreach (DataRow row in dt.Rows)
                        {
                            if (Convert.ToString(row["Outlet ID"]) != "")
                            {
                                dtExcelData.Rows.Add(Convert.ToString(row["Outlet ID"]));
                            }

                        }
                        try
                        {
                            //TempData["BulkModifyPartyLog"] = dtExcelData;
                            //TempData.Keep();

                            DataTable dtCmb = new DataTable();
                            ProcedureExecute proc = new ProcedureExecute("PRC_FTSBulkModifyParty");
                            proc.AddPara("@BULKDELETEPARTY_TABLE", dtExcelData);
                            proc.AddPara("@ACTION", "BulkDelete");
                            proc.AddPara("@CreateUser_Id", Convert.ToInt32(Session["userid"]));
                            dtCmb = proc.GetTable();

                            TempData["BulkDeletePartyLog"] = dtCmb;
                            TempData.Keep();

                        }
                        catch (Exception)
                        {

                        }
                    }
                }
            }
            return HasLog;
        }
        // End of Rev 3.0

        public ActionResult GetReAssignShopUserLog()
        {
            List<ReAssignShopModel> list = new List<ReAssignShopModel>();
            DataTable dt = new DataTable();
            try
            {
                if (TempData["ReAssignShopUserLog"] != null)
                {
                    dt = (DataTable)TempData["ReAssignShopUserLog"];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ReAssignShopModel data = null;
                        foreach (DataRow row in dt.Rows)
                        {
                            data = new ReAssignShopModel();
                            data.SHOP_CODE = Convert.ToString(row["SHOP_CODE"]);
                            data.Shop_Name = Convert.ToString(row["Shop_Name"]);
                            data.Type = Convert.ToString(row["Type"]);
                            data.Shop_Owner = Convert.ToString(row["Shop_Owner"]);
                            data.Shop_Owner_Contact = Convert.ToString(row["Shop_Owner_Contact"]);
                            data.Address = Convert.ToString(row["Address"]);
                            data.DD_NAME = Convert.ToString(row["DD_NAME"]);
                            data.PP_NAME = Convert.ToString(row["PP_NAME"]);

                            data.UPDATED_ON = Convert.ToString(row["UPDATED_ON"]);
                            data.OLD_UserName = Convert.ToString(row["OLD_UserName"]);
                            data.New_UserName = Convert.ToString(row["New_UserName"]);
                            list.Add(data);
                        }
                    }
                    TempData["ReAssignShopUserLog"] = dt;
                }
            }
            catch (Exception ex)
            {

            }
            TempData.Keep();
            return PartialView(list);
        }

        public ActionResult ExportReAssignLogList(int type)
        {
            ViewData["ReAssignShopUserLog"] = TempData["ReAssignShopUserLog"];

            TempData.Keep();

            if (ViewData["ReAssignShopUserLog"] != null)
            {
                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetReAssignShopLogGridView(ViewData["ReAssignShopUserLog"]), ViewData["ReAssignShopUserLog"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetReAssignShopLogGridView(ViewData["ReAssignShopUserLog"]), ViewData["ReAssignShopUserLog"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXlsx(GetReAssignShopLogGridView(ViewData["ReAssignShopUserLog"]), ViewData["ReAssignShopUserLog"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetReAssignShopLogGridView(ViewData["ReAssignShopUserLog"]), ViewData["ReAssignShopUserLog"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetReAssignShopLogGridView(ViewData["ReAssignShopUserLog"]), ViewData["ReAssignShopUserLog"]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetReAssignShopLogGridView(object datatable)
        {
            var settings = new GridViewSettings();
            settings.Name = "ReAssignShopLog";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Re Assign Shop Log";

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_Name";
                x.Caption = "Party Name";
                x.VisibleIndex = 1;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(200);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Type";
                x.Caption = "Party Type";
                x.VisibleIndex = 2;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(200);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_Owner";
                x.Caption = "Owner";
                x.VisibleIndex = 3;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(150);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_Owner_Contact";
                x.Caption = "Owner Contact";
                x.VisibleIndex = 4;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(100);

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Address";
                x.Caption = "Address";
                x.VisibleIndex = 5;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(160);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "PP_NAME";
                x.Caption = "Assigned To PP";
                x.VisibleIndex = 6;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(80);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "DD_NAME";
                x.Caption = "Assigned To DD";
                x.VisibleIndex = 7;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(80);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "OLD_UserName";
                x.Caption = "Old User";
                x.VisibleIndex = 8;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(80);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "New_UserName";
                x.Caption = "Assigned User";
                x.VisibleIndex = 9;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(80);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "UPDATED_ON";
                x.Caption = "Re-assign Date & Time";
                x.VisibleIndex = 10;
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

        [HttpPost]
        public JsonResult ShopReAssignUserLog(string Fromdt, String ToDate)
        {
            string output_msg = string.Empty;
            try
            {
                string datfrmat = Fromdt.Split('-')[2] + '-' + Fromdt.Split('-')[1] + '-' + Fromdt.Split('-')[0];
                string dattoat = ToDate.Split('-')[2] + '-' + ToDate.Split('-')[1] + '-' + ToDate.Split('-')[0];
                DataTable dt = obj.ShopReAssignUserLog(datfrmat, dattoat);
                if (dt != null && dt.Rows.Count > 0)
                {
                    TempData["ReAssignShopUserManualLog"] = dt;
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

        public ActionResult ReAssignShopUserManualLog()
        {
            List<ReAssignShopModelLog> list = new List<ReAssignShopModelLog>();
            DataTable dt = new DataTable();
            try
            {
                if (TempData["ReAssignShopUserManualLog"] != null)
                {
                    dt = (DataTable)TempData["ReAssignShopUserManualLog"];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ReAssignShopModelLog data = null;
                        foreach (DataRow row in dt.Rows)
                        {
                            data = new ReAssignShopModelLog();
                            data.SHOP_CODE = Convert.ToString(row["SHOP_CODE"]);
                            data.Shop_Name = Convert.ToString(row["Shop_Name"]);
                            data.Type = Convert.ToString(row["Type"]);
                            data.Shop_Owner = Convert.ToString(row["Shop_Owner"]);
                            data.Shop_Owner_Contact = Convert.ToString(row["Shop_Owner_Contact"]);
                            data.Address = Convert.ToString(row["Address"]);
                            data.DD_NAME = Convert.ToString(row["DD_NAME"]);
                            data.PP_NAME = Convert.ToString(row["PP_NAME"]);

                            data.UPDATED_ON = Convert.ToString(row["UPDATED_ON"]);
                            data.OLD_UserName = Convert.ToString(row["OLD_UserName"]);
                            data.New_UserName = Convert.ToString(row["New_UserName"]);
                            list.Add(data);
                        }
                    }
                    TempData["ReAssignShopUserManualLog"] = dt;
                }
            }
            catch (Exception ex)
            {

            }
            TempData.Keep();
            return PartialView(list);
        }

        public ActionResult ExportReAssignManualLogList(int type)
        {
            ViewData["ReAssignShopUserManualLog"] = TempData["ReAssignShopUserManualLog"];

            TempData.Keep();

            if (ViewData["ReAssignShopUserManualLog"] != null)
            {
                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetReAssignShopManualLogGridView(ViewData["ReAssignShopUserManualLog"]), ViewData["ReAssignShopUserManualLog"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetReAssignShopManualLogGridView(ViewData["ReAssignShopUserManualLog"]), ViewData["ReAssignShopUserManualLog"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXlsx(GetReAssignShopManualLogGridView(ViewData["ReAssignShopUserManualLog"]), ViewData["ReAssignShopUserManualLog"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetReAssignShopManualLogGridView(ViewData["ReAssignShopUserManualLog"]), ViewData["ReAssignShopUserManualLog"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetReAssignShopManualLogGridView(ViewData["ReAssignShopUserManualLog"]), ViewData["ReAssignShopUserManualLog"]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetReAssignShopManualLogGridView(object datatable)
        {
            var settings = new GridViewSettings();
            settings.Name = "ReAssignShopLog";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Re Assign Shop Log";

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_Name";
                x.Caption = "Party Name";
                x.VisibleIndex = 1;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(200);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Type";
                x.Caption = "Party Type";
                x.VisibleIndex = 2;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(200);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_Owner";
                x.Caption = "Owner";
                x.VisibleIndex = 3;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(150);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_Owner_Contact";
                x.Caption = "Owner Contact";
                x.VisibleIndex = 4;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(100);

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Address";
                x.Caption = "Address";
                x.VisibleIndex = 5;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(160);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "PP_NAME";
                x.Caption = "Assigned To PP";
                x.VisibleIndex = 6;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(80);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "DD_NAME";
                x.Caption = "Assigned To DD";
                x.VisibleIndex = 7;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(80);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "OLD_UserName";
                x.Caption = "Old User";
                x.VisibleIndex = 8;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(80);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "New_UserName";
                x.Caption = "Assigned User";
                x.VisibleIndex = 9;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(80);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "UPDATED_ON";
                x.Caption = "Re-assign Date & Time";
                x.VisibleIndex = 10;
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

        [HttpPost]
        public JsonResult GenerateReAssignShopList(string userid)
        {
            string output_msg = string.Empty;
            try
            {
                TempData["ReAssignShopList"] = null;
                DataTable dt = obj.GenerateReAssignShopList(userid);
                if (dt != null && dt.Rows.Count > 0)
                {
                    TempData["ReAssignShopList"] = dt;
                    TempData.Keep();
                    output_msg = "True";
                }
                else
                {
                    output_msg = "Party not found.";
                }
            }
            catch (Exception ex)
            {
                output_msg = "Please try again later";
            }
            return Json(output_msg, JsonRequestBehavior.AllowGet);
        }
        //Mantis Issue 25133
        [HttpPost]
        public JsonResult GenerateReAssignShopListForGroupBeat(string GroupBeatid)
        {
            string output_msg = string.Empty;
            try
            {
                TempData["ReAssignShopListForGroupBeat"] = null;
                DataTable dt = obj.GenerateReAssignShopListGroupBeat(GroupBeatid);
                if (dt != null && dt.Rows.Count > 0)
                {
                    TempData["ReAssignShopListForGroupBeat"] = dt;
                    TempData.Keep();
                    output_msg = "True";
                }
                else
                {
                    output_msg = "Party not found.";
                }
            }
            catch (Exception ex)
            {
                output_msg = "Please try again later";
            }
            return Json(output_msg, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ReAssignShopListForGroupBeat()
        {
            List<ReAssignShopModelLog> list = new List<ReAssignShopModelLog>();
            DataTable dt = new DataTable();
            try
            {
                if (TempData["ReAssignShopListForGroupBeat"] != null)
                {
                    dt = (DataTable)TempData["ReAssignShopListForGroupBeat"];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ReAssignShopModelLog data = null;
                        foreach (DataRow row in dt.Rows)
                        {
                            data = new ReAssignShopModelLog();
                            data.SHOP_CODE = Convert.ToString(row["SHOP_CODE"]);
                            data.Shop_Name = Convert.ToString(row["Shop_Name"]);
                            data.Type = Convert.ToString(row["Type"]);
                            data.Shop_Owner = Convert.ToString(row["Shop_Owner"]);
                            data.Shop_Owner_Contact = Convert.ToString(row["Shop_Owner_Contact"]);
                            data.Address = Convert.ToString(row["Address"]);
                            data.DD_NAME = Convert.ToString(row["DD_NAME"]);
                            data.PP_NAME = Convert.ToString(row["PP_NAME"]);

                            data.UserName = Convert.ToString(row["user_name"]);
                            data.UserLoginid = Convert.ToString(row["user_loginId"]);
                            list.Add(data);
                        }
                    }
                    TempData["ReAssignShopListForGroupBeat"] = dt;
                }
            }
            catch (Exception ex)
            {

            }
            TempData.Keep();
            return PartialView(list);
        }

        [HttpPost]
        public JsonResult ShopReAssignGroupBeat(ReAssignGroupBeat data)
        {
            string output_msg = string.Empty;
            try
            {
                string ShopCodes = "";
                int j = 1;

                if (data.ShopCode != null && data.ShopCode.Count > 0)
                {
                    foreach (string item in data.ShopCode)
                    {
                        if (j > 1)
                            ShopCodes = ShopCodes + "," + item;
                        else
                            ShopCodes = item;
                        j++;
                    }
                }

                DataTable dt = obj.ShopReAssignGroupBeat(Convert.ToString(Session["userid"]), data.OldGroupBeat, data.NewGroupBeat, ShopCodes);
                if (dt != null && dt.Rows.Count > 0)
                {
                    TempData["ReAssignShopGroupBeatLog"] = dt;
                    TempData.Keep();
                    output_msg = "Update Succesfully.";
                }
                else
                {
                    output_msg = "Party not found.";
                }
            }
            catch (Exception ex)
            {
                output_msg = "Please try again later";
            }
            return Json(output_msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetReAssignShopGroupBeatLog()
        {
            List<ReAssignShopGroupBeatModel> list = new List<ReAssignShopGroupBeatModel>();
            DataTable dt = new DataTable();
            try
            {
                if (TempData["ReAssignShopGroupBeatLog"] != null)
                {
                    dt = (DataTable)TempData["ReAssignShopGroupBeatLog"];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ReAssignShopGroupBeatModel data = null;
                        foreach (DataRow row in dt.Rows)
                        {
                            data = new ReAssignShopGroupBeatModel();
                            data.SHOP_CODE = Convert.ToString(row["SHOP_CODE"]);
                            data.Shop_Name = Convert.ToString(row["Shop_Name"]);
                            data.Type = Convert.ToString(row["Type"]);
                            data.Shop_Owner = Convert.ToString(row["Shop_Owner"]);
                            data.Shop_Owner_Contact = Convert.ToString(row["Shop_Owner_Contact"]);
                            data.Address = Convert.ToString(row["Address"]);
                            data.DD_NAME = Convert.ToString(row["DD_NAME"]);
                            data.PP_NAME = Convert.ToString(row["PP_NAME"]);

                            data.UPDATED_ON = Convert.ToString(row["UPDATED_ON"]);
                            //data.OLD_UserName = Convert.ToString(row["OLD_UserName"]);
                            //data.New_UserName = Convert.ToString(row["New_UserName"]);
                            data.GroupBeatName = Convert.ToString(row["GroupBeatName"]);
                            data.GroupBeatCode = Convert.ToString(row["GroupBeatCode"]);
                            data.GroupBeatId = Convert.ToString(row["GroupBeatId"]);
                            list.Add(data);
                        }
                    }
                    TempData["ReAssignShopGroupBeatLog"] = dt;
                }
            }
            catch (Exception ex)
            {

            }
            TempData.Keep();
            return PartialView(list);
        }
        public ActionResult ExportReAssignGroupBeatLogList(int type)
        {
            ViewData["ReAssignShopGroupBeatLog"] = TempData["ReAssignShopGroupBeatLog"];

            TempData.Keep();

            if (ViewData["ReAssignShopGroupBeatLog"] != null)
            {
                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetReAssignShopGroupBeatLogGridView(ViewData["ReAssignShopGroupBeatLog"]), ViewData["ReAssignShopGroupBeatLog"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetReAssignShopGroupBeatLogGridView(ViewData["ReAssignShopGroupBeatLog"]), ViewData["ReAssignShopGroupBeatLog"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXlsx(GetReAssignShopGroupBeatLogGridView(ViewData["ReAssignShopGroupBeatLog"]), ViewData["ReAssignShopGroupBeatLog"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetReAssignShopGroupBeatLogGridView(ViewData["ReAssignShopGroupBeatLog"]), ViewData["ReAssignShopGroupBeatLog"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetReAssignShopGroupBeatLogGridView(ViewData["ReAssignShopGroupBeatLog"]), ViewData["ReAssignShopGroupBeatLog"]);
                    default:
                        break;
                }
            }
            return null;
        }

        // Mantis Issue 25545
        [HttpPost]
        public JsonResult GenerateReAssignShopListForAreaRouteBeat(string userid)
        {
            string output_msg = string.Empty;
            try
            {
                TempData["ReAssignShopListForAreaRouteBeat"] = null;
                DataTable dt = obj.GenerateReAssignShopListForAreaRouteBeat(userid);
                if (dt != null && dt.Rows.Count > 0)
                {
                    TempData["ReAssignShopListForAreaRouteBeat"] = dt;
                    TempData.Keep();
                    output_msg = "True";
                }
                else
                {
                    output_msg = "Party not found.";
                }
            }
            catch (Exception ex)
            {
                output_msg = "Please try again later";
            }
            return Json(output_msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReAssignShopList_ForAreaRouteBeat()
        {
            List<ReAssignShopModelLog> list = new List<ReAssignShopModelLog>();
            DataTable dt = new DataTable();
            try
            {
                if (TempData["ReAssignShopListForAreaRouteBeat"] != null)
                {
                    dt = (DataTable)TempData["ReAssignShopListForAreaRouteBeat"];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ReAssignShopModelLog data = null;
                        foreach (DataRow row in dt.Rows)
                        {
                            data = new ReAssignShopModelLog();
                            data.SHOP_CODE = Convert.ToString(row["SHOP_CODE"]);
                            data.Shop_Name = Convert.ToString(row["Shop_Name"]);
                            data.Type = Convert.ToString(row["Type"]);
                            data.Shop_Owner = Convert.ToString(row["Shop_Owner"]);
                            data.Shop_Owner_Contact = Convert.ToString(row["Shop_Owner_Contact"]);
                            data.Address = Convert.ToString(row["Address"]);
                            data.DD_NAME = Convert.ToString(row["DD_NAME"]);
                            data.PP_NAME = Convert.ToString(row["PP_NAME"]);

                            data.UserName = Convert.ToString(row["user_name"]);
                            data.UserLoginid = Convert.ToString(row["user_loginId"]);
                            // Mantis Issue 25431
                            data.Beat = Convert.ToString(row["Beat"]);
                            // End of Mantis Issue 25431
                            // Mantis Issue 25545
                            data.Area = Convert.ToString(row["Area"]);
                            data.Route = Convert.ToString(row["Route"]);
                            // End of Mantis Issue 25545
                            list.Add(data);
                        }
                    }
                    TempData["ReAssignShopListForAreaRouteBeat"] = dt;
                }
            }
            catch (Exception ex)
            {

            }
            TempData.Keep();
            return PartialView(list);
        }

        [HttpPost]
        public JsonResult ShopReAssignUser_ForAreaRouteBeat(ReAssignShop data)
        {
            string output_msg = string.Empty;
            try
            {
                string ShopCodes = "";
                int j = 1;

                if (data.ShopCode != null && data.ShopCode.Count > 0)
                {
                    foreach (string item in data.ShopCode)
                    {
                        if (j > 1)
                            ShopCodes = ShopCodes + "," + item;
                        else
                            ShopCodes = item;
                        j++;
                    }
                }

                DataTable dt = obj.ShopReAssignUser_ForAreaRouteBeat(Convert.ToString(Session["userid"]), data.OldUser, data.NewUser, ShopCodes);
                if (dt != null && dt.Rows.Count > 0)
                {
                    TempData["ReAssignShopUserManualLog_AreaRouteBeat"] = dt;
                    TempData.Keep();
                    output_msg = "Update Succesfully.";
                }
                else
                {
                    output_msg = "Party not found.";
                }
            }
            catch (Exception ex)
            {
                output_msg = "Please try again later";
            }
            return Json(output_msg, JsonRequestBehavior.AllowGet);
        }





        [HttpPost]
        public JsonResult ShopReAssignUserLog_AreaRouteBeat(string Fromdt, String ToDate)
        {
            string output_msg = string.Empty;
            try
            {
                string datfrmat = Fromdt.Split('-')[2] + '-' + Fromdt.Split('-')[1] + '-' + Fromdt.Split('-')[0];
                string dattoat = ToDate.Split('-')[2] + '-' + ToDate.Split('-')[1] + '-' + ToDate.Split('-')[0];
                DataTable dt = obj.ShopReAssignUserLog_AreaRouteBeat(datfrmat, dattoat);
                if (dt != null && dt.Rows.Count > 0)
                {
                    TempData["ReAssignShopUserManualLog_AreaRouteBeat"] = dt;
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

        public ActionResult ReAssignShopUserManualLog_AreaRouteBeat()
        {
            List<ReAssignShopModelLog> list = new List<ReAssignShopModelLog>();
            DataTable dt = new DataTable();
            try
            {
                if (TempData["ReAssignShopUserManualLog_AreaRouteBeat"] != null)
                {
                    dt = (DataTable)TempData["ReAssignShopUserManualLog_AreaRouteBeat"];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ReAssignShopModelLog data = null;
                        foreach (DataRow row in dt.Rows)
                        {
                            data = new ReAssignShopModelLog();
                            data.SHOP_CODE = Convert.ToString(row["SHOP_CODE"]);
                            data.Shop_Name = Convert.ToString(row["Shop_Name"]);
                            data.Type = Convert.ToString(row["Type"]);
                            data.Shop_Owner = Convert.ToString(row["Shop_Owner"]);
                            data.Shop_Owner_Contact = Convert.ToString(row["Shop_Owner_Contact"]);
                            data.Address = Convert.ToString(row["Address"]);
                            data.DD_NAME = Convert.ToString(row["DD_NAME"]);
                            data.PP_NAME = Convert.ToString(row["PP_NAME"]);

                            data.UPDATED_ON = Convert.ToString(row["UPDATED_ON"]);
                            data.OLD_UserName = Convert.ToString(row["OLD_UserName"]);
                            data.New_UserName = Convert.ToString(row["New_UserName"]);
                            data.Beat = Convert.ToString(row["Beat"]);
                            data.Area = Convert.ToString(row["Area"]);
                            data.Route = Convert.ToString(row["Route"]);
                            list.Add(data);
                        }
                    }
                    TempData["ReAssignShopUserManualLog_AreaRouteBeat"] = dt;
                }
            }
            catch (Exception ex)
            {

            }
            TempData.Keep();
            return PartialView(list);
        }
        
        public ActionResult ExportReAssignManualLogList_AreaRouteBeat(int type)
        {
            ViewData["ReAssignShopUserManualLog_AreaRouteBeat"] = TempData["ReAssignShopUserManualLog_AreaRouteBeat"];

            TempData.Keep();

            if (ViewData["ReAssignShopUserManualLog_AreaRouteBeat"] != null)
            {
                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetReAssignShopManualLogGridView_AreaRouteBeat(ViewData["ReAssignShopUserManualLog_AreaRouteBeat"]), ViewData["ReAssignShopUserManualLog_AreaRouteBeat"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetReAssignShopManualLogGridView_AreaRouteBeat(ViewData["ReAssignShopUserManualLog_AreaRouteBeat"]), ViewData["ReAssignShopUserManualLog_AreaRouteBeat"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXlsx(GetReAssignShopManualLogGridView_AreaRouteBeat(ViewData["ReAssignShopUserManualLog_AreaRouteBeat"]), ViewData["ReAssignShopUserManualLog_AreaRouteBeat"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetReAssignShopManualLogGridView_AreaRouteBeat(ViewData["ReAssignShopUserManualLog_AreaRouteBeat"]), ViewData["ReAssignShopUserManualLog_AreaRouteBeat"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetReAssignShopManualLogGridView_AreaRouteBeat(ViewData["ReAssignShopUserManualLog_AreaRouteBeat"]), ViewData["ReAssignShopUserManualLog_AreaRouteBeat"]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetReAssignShopManualLogGridView_AreaRouteBeat(object datatable)
        {
            var settings = new GridViewSettings();
            settings.Name = "ReAssignShopLog_AreaRouteBeat";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Re Assign Area/Route/Beat Log";

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_Name";
                x.Caption = "Party Name";
                x.VisibleIndex = 1;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(200);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Type";
                x.Caption = "Party Type";
                x.VisibleIndex = 2;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(200);
            });

            // Mantis Issue 25545
            settings.Columns.Add(x =>
            {
                x.FieldName = "Area";
                x.Caption = "Area";
                x.VisibleIndex = 3;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(200);
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "Route";
                x.Caption = "Route";
                x.VisibleIndex = 4;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(200);
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "Beat";
                x.Caption = "Beat";
                x.VisibleIndex = 5;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(200);
            });
            // End of Mantis Issue 25545


            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_Owner";
                x.Caption = "Owner";
                x.VisibleIndex = 6;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(150);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_Owner_Contact";
                x.Caption = "Owner Contact";
                x.VisibleIndex = 7;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(100);

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Address";
                x.Caption = "Address";
                x.VisibleIndex = 8;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(160);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "PP_NAME";
                x.Caption = "Assigned To PP";
                x.VisibleIndex = 9;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(80);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "DD_NAME";
                x.Caption = "Assigned To DD";
                x.VisibleIndex = 10;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(80);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "OLD_UserName";
                x.Caption = "Old User";
                x.VisibleIndex = 11;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(80);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "New_UserName";
                x.Caption = "Assigned User";
                x.VisibleIndex = 12;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(80);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "UPDATED_ON";
                x.Caption = "Re-assign Date & Time";
                x.VisibleIndex = 13;
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
        // End of Mantis Issue 25545
        private GridViewSettings GetReAssignShopGroupBeatLogGridView(object datatable)
        {
            var settings = new GridViewSettings();
            settings.Name = "ReAssignShopGroupBeatLog";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Re Assign Shop Group Beat Log";

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_Name";
                x.Caption = "Party Name";
                x.VisibleIndex = 1;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(200);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Type";
                x.Caption = "Party Type";
                x.VisibleIndex = 2;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(200);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_Owner";
                x.Caption = "Owner";
                x.VisibleIndex = 3;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(150);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_Owner_Contact";
                x.Caption = "Owner Contact";
                x.VisibleIndex = 4;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(100);

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Address";
                x.Caption = "Address";
                x.VisibleIndex = 5;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(160);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "PP_NAME";
                x.Caption = "Assigned To PP";
                x.VisibleIndex = 6;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(80);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "DD_NAME";
                x.Caption = "Assigned To DD";
                x.VisibleIndex = 7;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(80);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "GroupBeatName";
                x.Caption = "Group Beat Name";
                x.VisibleIndex = 8;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(80);
            });

            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "New_UserName";
            //    x.Caption = "Assigned User";
            //    x.VisibleIndex = 9;
            //    x.Width = System.Web.UI.WebControls.Unit.Pixel(80);
            //});

            settings.Columns.Add(x =>
            {
                x.FieldName = "UPDATED_ON";
                x.Caption = "Re-assign Date & Time";
                x.VisibleIndex = 9;
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

        [HttpPost]
        public JsonResult ShopReAssignGroupBeatLog(string Fromdt, String ToDate)
        {
            string output_msg = string.Empty;
            try
            {
                string datfrmat = Fromdt.Split('-')[2] + '-' + Fromdt.Split('-')[1] + '-' + Fromdt.Split('-')[0];
                string dattoat = ToDate.Split('-')[2] + '-' + ToDate.Split('-')[1] + '-' + ToDate.Split('-')[0];
                DataTable dt = obj.ShopReAssignGroupBeatLog(datfrmat, dattoat);
                if (dt != null && dt.Rows.Count > 0)
                {
                    TempData["ReAssignShopGroupBeatManualLog"] = dt;
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

        public ActionResult ReAssignShopGroupBeatManualLog()
        {
            List<ReAssignShopGroupBeatModelLog> list = new List<ReAssignShopGroupBeatModelLog>();
            DataTable dt = new DataTable();
            try
            {
                if (TempData["ReAssignShopGroupBeatManualLog"] != null)
                {
                    dt = (DataTable)TempData["ReAssignShopGroupBeatManualLog"];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ReAssignShopGroupBeatModelLog data = null;
                        foreach (DataRow row in dt.Rows)
                        {
                            data = new ReAssignShopGroupBeatModelLog();
                            data.SHOP_CODE = Convert.ToString(row["SHOP_CODE"]);
                            data.Shop_Name = Convert.ToString(row["Shop_Name"]);
                            data.Type = Convert.ToString(row["Type"]);
                            data.Shop_Owner = Convert.ToString(row["Shop_Owner"]);
                            data.Shop_Owner_Contact = Convert.ToString(row["Shop_Owner_Contact"]);
                            data.Address = Convert.ToString(row["Address"]);
                            data.DD_NAME = Convert.ToString(row["DD_NAME"]);
                            data.PP_NAME = Convert.ToString(row["PP_NAME"]);
                            data.NEW_GROUPBEAT = Convert.ToString(row["NEW_GROUPBEAT"]);
                            data.OLD_GROUPBEAT = Convert.ToString(row["OLD_GROUPBEAT"]);
                            data.UPDATED_ON = Convert.ToString(row["UPDATED_ON"]);

                            list.Add(data);
                        }
                    }
                    TempData["ReAssignShopGroupBeatManualLog"] = dt;
                }
            }
            catch (Exception ex)
            {

            }
            TempData.Keep();
            return PartialView(list);
        }

        public ActionResult ExportReAssignGroupBeatManualLogList(int type)
        {
            ViewData["ReAssignShopGroupBeatManualLog"] = TempData["ReAssignShopGroupBeatManualLog"];
            
            TempData.Keep();

            if (ViewData["ReAssignShopGroupBeatManualLog"] != null)
            {
                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetReAssignShopGroupBeatManualLogGridView(ViewData["ReAssignShopGroupBeatManualLog"]), ViewData["ReAssignShopGroupBeatManualLog"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetReAssignShopGroupBeatManualLogGridView(ViewData["ReAssignShopGroupBeatManualLog"]), ViewData["ReAssignShopGroupBeatManualLog"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXlsx(GetReAssignShopGroupBeatManualLogGridView(ViewData["ReAssignShopGroupBeatManualLog"]), ViewData["ReAssignShopGroupBeatManualLog"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetReAssignShopGroupBeatManualLogGridView(ViewData["ReAssignShopGroupBeatManualLog"]), ViewData["ReAssignShopGroupBeatManualLog"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetReAssignShopGroupBeatManualLogGridView(ViewData["ReAssignShopGroupBeatManualLog"]), ViewData["ReAssignShopGroupBeatManualLog"]);
                    default:
                        break;
                }
            }
            return null;
        }
        private GridViewSettings GetReAssignShopGroupBeatManualLogGridView(object datatable)
        {
            var settings = new GridViewSettings();
            settings.Name = "ReAssignShopGroupBeatLog";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Re Assign Shop Group Beat Log";

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_Name";
                x.Caption = "Party Name";
                x.VisibleIndex = 1;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(200);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Type";
                x.Caption = "Party Type";
                x.VisibleIndex = 2;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(200);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_Owner";
                x.Caption = "Owner";
                x.VisibleIndex = 3;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(150);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_Owner_Contact";
                x.Caption = "Owner Contact";
                x.VisibleIndex = 4;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(100);

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Address";
                x.Caption = "Address";
                x.VisibleIndex = 5;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(160);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "PP_NAME";
                x.Caption = "Assigned To PP";
                x.VisibleIndex = 6;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(80);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "DD_NAME";
                x.Caption = "Assigned To DD";
                x.VisibleIndex = 7;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(80);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "OLD_GROUPBEAT";
                x.Caption = "Old Group Beat";
                x.VisibleIndex = 8;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(80);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "NEW_GROUPBEAT";
                x.Caption = "New Group Beat";
                x.VisibleIndex = 9;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(80);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "UPDATED_ON";
                x.Caption = "Re-assign Date & Time";
                x.VisibleIndex = 10;
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
        //End of Mantis Issue 25133

        public ActionResult ReAssignShopList()
        {
            List<ReAssignShopModelLog> list = new List<ReAssignShopModelLog>();
            DataTable dt = new DataTable();
            try
            {
                if (TempData["ReAssignShopList"] != null)
                {
                    dt = (DataTable)TempData["ReAssignShopList"];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ReAssignShopModelLog data = null;
                        foreach (DataRow row in dt.Rows)
                        {
                            data = new ReAssignShopModelLog();
                            data.SHOP_CODE = Convert.ToString(row["SHOP_CODE"]);
                            data.Shop_Name = Convert.ToString(row["Shop_Name"]);
                            data.Type = Convert.ToString(row["Type"]);
                            data.Shop_Owner = Convert.ToString(row["Shop_Owner"]);
                            data.Shop_Owner_Contact = Convert.ToString(row["Shop_Owner_Contact"]);
                            data.Address = Convert.ToString(row["Address"]);
                            data.DD_NAME = Convert.ToString(row["DD_NAME"]);
                            data.PP_NAME = Convert.ToString(row["PP_NAME"]);

                            data.UserName = Convert.ToString(row["user_name"]);
                            data.UserLoginid = Convert.ToString(row["user_loginId"]);
                            // Mantis Issue 25431
                            data.Beat = Convert.ToString(row["Beat"]);
                            // End of Mantis Issue 25431
                            list.Add(data);
                        }
                    }
                    TempData["ReAssignShopList"] = dt;
                }
            }
            catch (Exception ex)
            {

            }
            TempData.Keep();
            return PartialView(list);
        }

        [HttpPost]
        public ActionResult GetShopTypes(string shoptype)
        {
            List<clsGroupBeat> ActiveGroupBeat = new List<clsGroupBeat>();
            DataSet ds = obj.AddShopGetDetails();
            if (ds!= null)
            {
                if (shoptype=="1" && ds.Tables[10].Rows.Count>0)
                {
                    ActiveGroupBeat = APIHelperMethods.ToModelList<clsGroupBeat>(ds.Tables[10]); 
                }
                else if (shoptype == "4" && ds.Tables[11].Rows.Count > 0)
                {
                    ActiveGroupBeat = APIHelperMethods.ToModelList<clsGroupBeat>(ds.Tables[11]); 
                }
            }
            return Json(ActiveGroupBeat, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult GetddShopTypes(string shoptype, String TypeID)
        {
            List<clsGroupBeat> ActiveGroupBeat = new List<clsGroupBeat>();
            DataTable dt = obj.GetddShopTypeDetails(shoptype, TypeID, "GetddShopType");
            if (dt != null && dt.Rows.Count>0)
            {
                ActiveGroupBeat = APIHelperMethods.ToModelList<clsGroupBeat>(dt);               
            }
            return Json(ActiveGroupBeat, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetElectricianShopTypes(string shoptype)
        {
            List<clsGroupBeat> ActiveGroupBeat = new List<clsGroupBeat>();
            DataTable dt = obj.GetddShopTypeDetails(shoptype, "0", "GetElectricianShopType");
            if (dt != null && dt.Rows.Count > 0)
            {
                ActiveGroupBeat = APIHelperMethods.ToModelList<clsGroupBeat>(dt);
            }
            return Json(ActiveGroupBeat, JsonRequestBehavior.AllowGet);
        }

        // Rev 4.0
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
                int k = obj.InsertPageRetention(Col, Session["userid"].ToString(), "PARTY LIST");

                return Json(k, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
        // End of Rev 4.0
    }
}
/******************************************************************************************************
 * 1.0     01-09-2023       V2.0.38      Sanchita     Distance issue in View route of Nordusk. Refer: 25515
 * 2.0     24-11-2023       V2.0.43      Priti        0027031: Dashboard report issue(check in local Rubyfoods db)
 * 3.0     03-04-2024       V2.0.46      Sanchita     0027314: The Google API key should store in the database with a new table.
 * ******************************************************************************************************/
using BusinessLogicLayer;
using BusinessLogicLayer.SalesmanTrack;
using DataAccessLayer;
using DevExpress.Data.Mask;
using DevExpress.XtraExport;
using Microsoft.Owin.BuilderProperties;
using Models;
using MyShop.Models;
using SalesmanTrack;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilityLayer;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class ViewRouteController : Controller
    {
        UserList lstuser = new UserList();
        EmployeeMasterReport omodel = new EmployeeMasterReport();
        DataTable dtuser = new DataTable();
        DataTable dttrack = new DataTable();
        Locationdata objlocation = new Locationdata();

        public ActionResult Index()
        {
            string settings = "1";


            DBEngine obj = new DBEngine();
            settings = Convert.ToString(obj.GetDataTable("select [value] from FTS_APP_CONFIG_SETTINGS WHERE [Key]='IsGmap'").Rows[0][0]);
            //Rev Debashis 0025198
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
            //End of Rev Debashis 0025198
            
            // Rev 1.0
            string strTotalDistanceShowinViewRouteAsPerGoogleAPI = "0";
            strTotalDistanceShowinViewRouteAsPerGoogleAPI = Convert.ToString(obj.GetDataTable("select [value] from FTS_APP_CONFIG_SETTINGS WHERE [Key]='IsTotalDistanceShowinViewRouteAsPerGoogleAPI'").Rows[0][0]);
            ViewBag.strTotalDistanceShowinViewRouteAsPerGoogleAPI = strTotalDistanceShowinViewRouteAsPerGoogleAPI;
            // End of Rev 1.0

            if (settings == "1")
            // Rev 3.0
            //  return View(@"IndexGmap");
            {
                CommonBL cbl = new CommonBL();
                ViewBag.GoogleAPIKey = cbl.GetSystemSettingsResult("GoogleMapKey");

                if (ViewBag.GoogleAPIKey != "")
                    ViewBag.HasGoogleAPIKey = 1;
                else
                    ViewBag.HasGoogleAPIKey = 0;
                
                return View(@"IndexGmap");
            }
            // End of Rev 3.0
            else
                return View();
        }

        public ActionResult ShowRoute(string id, String Date)
        {
            ViewBag.id = id;
            ViewBag.Date = Date;
            return View();
        }

        public ActionResult ShowRouteVShops(string id, String Date)
        {
            ViewBag.id = id;
            ViewBag.Date = Date;
            return View();
        }

        [HttpPost]
        public ActionResult SatetListView(string countryID)
        {
            string userid = Session["userid"].ToString();
            List<GetUsersStates> modelstate = new List<GetUsersStates>();
            DataTable dtstate = lstuser.GetStateList(userid);
            List<StateData> lststate = new List<StateData>();
            //  modelstate = APIHelperMethods.ToModelList<GetUsersStates>(dtstate);

            DataTable statetable = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_Dashboard");
            proc.AddPara("@DASHBOARDACTION", "DashboardStateList");
            proc.AddPara("@USERID", userid);
            statetable = proc.GetTable();
            lststate = (from DataRow dr in statetable.Rows
                        select new StateData()
                        {
                            id = dr["ID"].ToString(),
                            name = dr["name"].ToString(),
                        }).ToList();




            return Json(lststate, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult Mapopen(string StateID, String Date)
        public ActionResult Mapopen(string StateID, String Date,string BranchIds)
        {
            List<MapDashboard> AddressList = new List<MapDashboard>();

            string newdate = Date.Split('-')[2] + '-' + Date.Split('-')[1] + '-' + Date.Split('-')[0];

            Dashboard model = new Dashboard();
            //Rev Debashis 0025198
            //DataTable dtmodellatest = model.GETMapDashboard(StateID, newdate, Session["userid"].ToString());
            DataTable dtmodellatest = model.GETMapDashboard(BranchIds, StateID, newdate, Session["userid"].ToString());
            //End of Rev Debashis 0025198
            AddressList = APIHelperMethods.ToModelList<MapDashboard>(dtmodellatest);

            return Json(AddressList);
        }
        //Rev Work start 15.06.2022 0024954: Need to change View Route of FSM Dashboard
        public ActionResult Locationopen(string userid, string output)
        {
            List<LocationDashboard> LocationList = new List<LocationDashboard>();
            string fromdt=string.Empty;
            string todt=string.Empty;
            string DataSpan = "";          

            string newdate = output.Split('-')[2] + '-' + output.Split('-')[1] + '-' + output.Split('-')[0];

            fromdt = newdate;
            todt = newdate;

            Dashboard model = new Dashboard();
            DataTable dtmodellatest = model.GETLocationDashboard(userid,fromdt,todt,DataSpan);
            LocationList = APIHelperMethods.ToModelList<LocationDashboard>(dtmodellatest);

            return Json(LocationList);
        }
        //Rev Work close 15.06.2022 0024954: Need to change View Route of FSM Dashboard
        public ActionResult GetParty(string StateID, string TYPE_ID, string PARTY_ID, string IS_Electician)
        {
            
            List<PartyDashboard> AddressList = new List<PartyDashboard>();
            
            //string newdate = Date.Split('-')[2] + '-' + Date.Split('-')[1] + '-' + Date.Split('-')[0];

            Dashboard model = new Dashboard();
            DataTable dtmodellatest = model.GETPartyDashboard(StateID, TYPE_ID, PARTY_ID, IS_Electician, Session["userid"].ToString());
            AddressList = APIHelperMethods.ToModelList<PartyDashboard>(dtmodellatest);
            return Json(AddressList);
        } 
        public ActionResult GetOutlets(string StateID, string PARTY_ID, string PartyStatus, string month, string year)
        {
            List<PartyDashboard> AddressList = new List<PartyDashboard>();

            //string newdate = Date.Split('-')[2] + '-' + Date.Split('-')[1] + '-' + Date.Split('-')[0];

            Dashboard model = new Dashboard();
            DataTable dtmodellatest = model.GETOutletDashboard(StateID, PARTY_ID, PartyStatus, month, year, Session["userid"].ToString());
            //REV 2.0
            AddressList = APIHelperMethods.ToModelList<PartyDashboard>(dtmodellatest);
            //return Json(AddressList);
            var jsonResult = Json(AddressList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
            //REV 2.0 END

        }

        public ActionResult GetSalesmanLocation(UserListTrackModel modelinput)
        {
            try
            {
                Trackshop model = new Trackshop();
                //string datfrmat = DateTime.Now.ToString("yyyy-MM-dd");
                string datfrmat = modelinput.Date.Split('-')[2] + '-' + modelinput.Date.Split('-')[1] + '-' + modelinput.Date.Split('-')[0];

                dtuser = lstuser.GetUserLocationListRoute(modelinput.selectedusrid, datfrmat);
                if (dtuser.Rows.Count > 0)
                {
                    model.Lat_visit = Convert.ToString(dtuser.Rows[0]["Lat_visit"]);
                    model.Long_visit = Convert.ToString(dtuser.Rows[0]["Long_visit"]);
                    model.location_name = Convert.ToString(dtuser.Rows[0]["location_name"]);
                    model.loginstatus = Convert.ToString(dtuser.Rows[0]["loginstatus"]);
                    model.SDate = Convert.ToString(dtuser.Rows[0]["SDate"]);
                    model.latlanLogin = Convert.ToString(dtuser.Rows[0]["latlanLogin"]);
                    model.latlanLogout = Convert.ToString(dtuser.Rows[0]["latlanLogout"]);
                    model.distance = Convert.ToString(dtuser.Rows[0]["distance"]);
                    model.IsShowDistance = Convert.ToString(dtuser.Rows[0]["setting"]);
                   

                    string Pathget = "~\\jsonsresponse\\" + "_" + modelinput.selectedusrid + "_" + datfrmat + ".json";

                    if (System.IO.File.Exists(Server.MapPath(Pathget)))
                    {
                        using (StreamReader r = new StreamReader(Server.MapPath(Pathget)))
                        {
                            string json = r.ReadToEnd().ToString();
                            model.Fullresponse = json;
                        }
                    }
                    Dashboard dashbrd = new Dashboard();
                    //dttrack = lstuser.GetUserLocationTrackList(modelinput.selectedusrid, datfrmat);
                    dttrack = dashbrd.GetUserLocationTrackListRoute(modelinput.selectedusrid, datfrmat, modelinput.IsGmap);
                    List<TracksalesmanAreaTrack> lstarea = new List<TracksalesmanAreaTrack>();
                    lstarea = APIHelperMethods.ToModelList<TracksalesmanAreaTrack>(dttrack);
                    model.salesmanarea = lstarea;
                    return Json(model);
                }
                else
                {
                    // return PartialView("PartialTracking", model);
                    return Json(model);
                }
            }
            catch
            {
                //    return Redirect("/OMS/Signoff.aspx");
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }


        // Susanta 25-06-2021
        public ActionResult GetSalesmanLocationOutlet(UserListTrackModel modelinput)
        {
            try
            {
                Trackshop model = new Trackshop();
                //string datfrmat = DateTime.Now.ToString("yyyy-MM-dd");
                string datfrmat = modelinput.Date.Split('-')[2] + '-' + modelinput.Date.Split('-')[1] + '-' + modelinput.Date.Split('-')[0];

                dtuser = lstuser.GetUserLocationListRoute(modelinput.selectedusrid, datfrmat);
                if (dtuser.Rows.Count > 0)
                {
                    model.Lat_visit = Convert.ToString(dtuser.Rows[0]["Lat_visit"]);
                    model.Long_visit = Convert.ToString(dtuser.Rows[0]["Long_visit"]);
                    model.location_name = Convert.ToString(dtuser.Rows[0]["location_name"]);
                    model.loginstatus = Convert.ToString(dtuser.Rows[0]["loginstatus"]);
                    model.SDate = Convert.ToString(dtuser.Rows[0]["SDate"]);
                    model.latlanLogin = Convert.ToString(dtuser.Rows[0]["latlanLogin"]);
                    model.latlanLogout = Convert.ToString(dtuser.Rows[0]["latlanLogout"]);
                    model.distance = Convert.ToString(dtuser.Rows[0]["distance"]);
                    model.IsShowDistance = Convert.ToString(dtuser.Rows[0]["setting"]);


                    string Pathget = "~\\jsonsresponse\\" + "_" + modelinput.selectedusrid + "_" + datfrmat + ".json";

                    if (System.IO.File.Exists(Server.MapPath(Pathget)))
                    {
                        using (StreamReader r = new StreamReader(Server.MapPath(Pathget)))
                        {
                            string json = r.ReadToEnd().ToString();
                            model.Fullresponse = json;
                        }
                    }
                    Dashboard dashbrd = new Dashboard();
                    //dttrack = lstuser.GetUserLocationTrackList(modelinput.selectedusrid, datfrmat);
                    dttrack = dashbrd.GetUserLocationTrackListRouteOutlet(modelinput.selectedusrid, datfrmat, modelinput.IsGmap);
                    List<TracksalesmanAreaTrack> lstarea = new List<TracksalesmanAreaTrack>();
                    lstarea = APIHelperMethods.ToModelList<TracksalesmanAreaTrack>(dttrack);
                    model.salesmanarea = lstarea;
                    return Json(model);
                }
                else
                {
                    // return PartialView("PartialTracking", model);
                    return Json(model);
                }
            }
            catch
            {
                //    return Redirect("/OMS/Signoff.aspx");
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
        
        // Susanta New map vs shops
        public JsonResult GetShopsAssigned(UserListTrackModel modelinput)
        {
            //Dashboard dashboarddataobj = new Dashboard();
            string datfrmat = modelinput.Date.Split('-')[2] + '-' + modelinput.Date.Split('-')[1] + '-' + modelinput.Date.Split('-')[0];
            List<shopsClass> caldata = new List<shopsClass>();
            try
            {
                DataSet ds = new DataSet();
                ProcedureExecute proc = new ProcedureExecute("PRC_GetUserShopVisitDetails");

                proc.AddPara("@Date", datfrmat);
                proc.AddPara("@UserID", modelinput.selectedusrid);
                ds = proc.GetDataSet();
                DataSet objData = ds;


                //DataSet objData = dashboarddataobj.GetOrderAnalytics(fromDate, toDate, "ORDCNTDATE", Session["userid"].ToString());
                if (objData != null && objData.Tables[0] != null && objData.Tables[0].Rows.Count > 0)
                {
                    caldata = (from DataRow dr in objData.Tables[0].Rows
                               select new shopsClass()
                               {
                                   Shop_Code = Convert.ToString(dr["Shop_Code"]),
                                   Shop_Name = Convert.ToString(dr["Shop_Name"]),
                                   Shop_Lat = Convert.ToString(dr["Shop_Lat"]),
                                   Shop_Long = Convert.ToString(dr["Shop_Long"]),
                                   Visited = Convert.ToString(dr["Visited"])
                               }).ToList();
                }
            }
            catch
            {

            }
            return Json(caldata);
        }
    
    }

    public class shopsClass
    {
        public string Shop_Code { get; set; }
        public string Shop_Name { get; set; }
        public string Shop_Lat { get; set; }
        public string Shop_Long { get; set; }
        public string Visited { get; set; }  				
    }

}
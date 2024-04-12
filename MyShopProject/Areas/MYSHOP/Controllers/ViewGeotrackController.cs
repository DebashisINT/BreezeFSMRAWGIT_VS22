/*******************************************************************************************************************
 * Written  by  Sanchita  V2.0.46  20/03/2024   0027322: A new dashboard floating menu is required as "View Geotrack".
 *
 * 1.0      03-04-2024    V2.0.46    Sanchita       0027314: The Google API key should store in the database with a new table.
 ********************************************************************************************************************/
using SalesmanTrack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BusinessLogicLayer.SalesTrackerReports;
using System.Data;
using UtilityLayer;
using System.Collections;
using System.Configuration;
using MyShop.Models;
using DevExpress.Web.Mvc;
using DevExpress.Web;
using DataAccessLayer;
using System.Web.UI.WebControls;
using DevExpress.Xpo.DB;
using BusinessLogicLayer;
using DocumentFormat.OpenXml.EMMA;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class ViewGeotrackController : Controller
    {
        UserList lstuser = new UserList();
        CommonBL objSystemSettings = new CommonBL();

        // GET: MYSHOP/ViewGeotrack
        public ActionResult Index()
        {
            try
            {

                ViewGeotrackModel model = new ViewGeotrackModel();
                string userid = Session["userid"].ToString();

                //DataTable dtstate = lstuser.GetStateList(userid);
                //model.modelstates = APIHelperMethods.ToModelList<GetUsersState>(dtstate);


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
                model.modelbranch = APIHelperMethods.ToModelList<GetUsersBranch>(dtbranch);
                string h_id = model.modelbranch.First().BRANCH_ID.ToString();
                ViewBag.h_id = h_id;

                //DataTable dtpartytype = GetPartyTypeList();
                //model.modelpartytypes = APIHelperMethods.ToModelList<GetPartyType>(dtpartytype);

                // Rev 1.0
                CommonBL cbl = new CommonBL();
                ViewBag.GoogleAPIKey = cbl.GetSystemSettingsResult("GoogleMapKey");

                if (ViewBag.GoogleAPIKey != "")
                    ViewBag.HasGoogleAPIKey = 1;
                else
                    ViewBag.HasGoogleAPIKey = 0;
                // End of Rev 1.0

                return View(model);

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        //public ActionResult GetPartyTypeList()
        //{
        //    BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(string.Empty);
        //    try
        //    {
        //        ViewGeotrackModel model = new ViewGeotrackModel();

        //        DataTable dtpartytype = new DataTable();
        //        ProcedureExecute proc = new ProcedureExecute("FTS_VIEWGEOTRACK");
        //        proc.AddPara("@ACTION", "GETPARTYTYPE");
        //        dtpartytype = proc.GetTable();


        //        model.modelpartytypes = APIHelperMethods.ToModelList<GetPartyType>(dtpartytype);

        //        return PartialView("~/Areas/MYSHOP/Views/ViewGeotrack/_PartyTypeList.cshtml", model.modelpartytypes);

        //    }
        //    catch
        //    {
        //        return RedirectToAction("Logout", "Login", new { Area = "" });
        //    }
        //}

        //public JsonResult GetPartyTypeListSelectAll()
        //{
        //    BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(string.Empty);
        //    try
        //    {
        //        ViewGeotrackModel model = new ViewGeotrackModel();

        //        DataTable dtpartytype = new DataTable();
        //        ProcedureExecute proc = new ProcedureExecute("FTS_VIEWGeotrack");
        //        proc.AddPara("@ACTION", "GETPARTYTYPE");
        //        dtpartytype = proc.GetTable();

        //        model.modelpartytypes = APIHelperMethods.ToModelList<GetPartyType>(dtpartytype);

        //        return Json(model.modelpartytypes, JsonRequestBehavior.AllowGet);

        //    }
        //    catch
        //    {
        //        return Json("", JsonRequestBehavior.AllowGet);
        //    }
        //}

        public ActionResult GetUserRoutes(string stateid, string branchid, string empId,string fromdate, string todate)
        {
            List<UserRuoteList> AddressList = new List<UserRuoteList>();

            //string newdate = Date.Split('-')[2] + '-' + Date.Split('-')[1] + '-' + Date.Split('-')[0];

            ViewGeotrackModel model = new ViewGeotrackModel();
            string userid = Session["userid"].ToString();

            DataTable dtmodellatest = new DataTable();

            if (fromdate == null)
            {
                fromdate = DateTime.Now.ToString("dd-MM-yyyy");
            }

            if (todate == null)
            {
                todate = DateTime.Now.ToString("dd-MM-yyyy");
            }

            string datfrmat = fromdate.Split('-')[2] + '-' + fromdate.Split('-')[1] + '-' + fromdate.Split('-')[0];
            string dattoat = todate.Split('-')[2] + '-' + todate.Split('-')[1] + '-' + todate.Split('-')[0];

            if (branchid == "")
            {
                branchid = Convert.ToString(Session["userbranchHierarchy"]);
            }

            ProcedureExecute proc = new ProcedureExecute("FTS_VIEWGEOTRACK");
            proc.AddPara("@ACTION", "GETUSERROUTE");
            proc.AddPara("@State", stateid);
            proc.AddPara("@Branch", branchid);
            proc.AddPara("@empId", empId);
            proc.AddPara("@dtFrom", datfrmat);
            proc.AddPara("@dtTo", dattoat);
            proc.AddPara("@USER_ID", userid);
            dtmodellatest = proc.GetTable();

            AddressList = APIHelperMethods.ToModelList<UserRuoteList>(dtmodellatest);
            //return Json(AddressList);
            var jsonResult = Json(AddressList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;


        }

    }
}
/***************************************************************************************************************
1.0  v2 .0.36  1.0  12/01/2023     Appconfig and User wise setting "IsAllDataInPortalwithHeirarchy = True" 
                                        then data in portal shall be populated based on Hierarchy Only. Refer: 25504
***********************************************************************************************************/
using BusinessLogicLayer.SalesmanTrack;
using MyShop.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// Mantis Issue 25536, 25535, 25542, 25543, 25544
using UtilityLayer;
// End of Mantis Issue 25536, 25535, 25542, 25543, 25544
// Rev 1.0
using DataAccessLayer;
// End of Rev 1.0

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class BeatController : Controller
    {
        //
        // GET: /MYSHOP/Beat/
        public ActionResult Index()
        {
            // Mantis Issue 25536, 25535, 25542, 25543, 25544
            BeatModel Dtls = new BeatModel();
            DataSet ds = GroupBeat.Obj.GetListDataDetails();

            List<AreaList> AreaLst = new List<AreaList>();
            AreaLst = APIHelperMethods.ToModelList<AreaList>(ds.Tables[0]);
            Dtls.AreaList = AreaLst;
            Dtls.Area = 0;

            List<RouteList> RouteLst = new List<RouteList>();
            RouteLst = APIHelperMethods.ToModelList<RouteList>(ds.Tables[1]);
            Dtls.RouteList = RouteLst;
            Dtls.Route = 0;
            // End of Mantis Issue 25536, 25535, 25542, 25543, 25544

            return View(Dtls);
        }

        public PartialViewResult PartialGrid()
        {
            return PartialView(GetList());
        }

        // Mantis Issue 25536, 25535, 25542, 25543, 25544 [area and route added]
        public JsonResult SaveGroupBeat(string code, string name, string id, int route)
        {
            int output = 0;
            string Userid = Convert.ToString(Session["userid"]);
            output = GroupBeat.Obj.SaveBeat(code, name, Userid, route, id);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveGroupBeatUser(string selected, string id)
        {
            int output = 0;
            string Userid = Convert.ToString(Session["userid"]);
            output = GroupBeatUsers.Obj.SaveBeatUser(selected, id);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        // Mantis Issue 25536, 25535, 25542, 25543, 25544 [ type added ]
        public JsonResult EditGroupBeat(string id, string type)
        {
            DataTable output = new DataTable();
            output = GroupBeat.Obj.EditBeat(id, type);  // Mantis Issue 25536, 25535, 25542, 25543, 25544 [ type added ]

            if (output.Rows.Count > 0)
            {
                return Json(new { code = Convert.ToString(output.Rows[0]["CODE"]), name = Convert.ToString(output.Rows[0]["NAME"]), area = Convert.ToString(output.Rows[0]["AREA_CODE"]), route= Convert.ToString(output.Rows[0]["ROUTE_CODE"]) }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { code = "", name = "" }, JsonRequestBehavior.AllowGet);
            }

        }

        //Mantis Issue 25536, 25535, 25542, 25543, 25544
        public JsonResult SaveArea(string code, string name, string id)
        {
            int output = 0;
            string Userid = Convert.ToString(Session["userid"]);
            output = GroupBeat.Obj.SaveArea(code, name, Userid, id);
            return Json(output, JsonRequestBehavior.AllowGet);
        }


        public JsonResult SaveRoute(string code, string name, int area, string id)
        {
            int output = 0;
            string Userid = Convert.ToString(Session["userid"]);
            output = GroupBeat.Obj.SaveRoute(code, name, area, Userid, id);
            return Json(output, JsonRequestBehavior.AllowGet);
        }
        // End of Mantis Issue 25536, 25535, 25542, 25543, 25544

        public IEnumerable GetList()
        {
            string connectionString = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"]);
            ReportsDataContext dc = new ReportsDataContext(connectionString);
            var q = from d in dc.V_GROUPBEATLISTs
                    select d;
            return q;

        }

        public PartialViewResult PartialUserGrid(string id)
        {
            // Rev 1.0
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("PRC_GROUPBEAT");
            proc.AddPara("@ACTION", "GETMAPUSERLISTDATA");
            proc.AddPara("@USER_ID", Convert.ToString(Session["userid"]));
            ds = proc.GetDataSet();
            // End of Rev 1.0

            return PartialView(GetUserList());
        }

        public IEnumerable GetUserList()
        {
            string connectionString = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"]);
            ReportsDataContext dc = new ReportsDataContext(connectionString);
            // Rev 1.0
            //var q = from d in dc.tbl_master_users
            //        select d;

            int userid = Convert.ToInt32(Session["userid"]);

            var q = from d in dc.FTS_MapUserLists where d.userid == userid
                    orderby d.user_id
                    select d;
            // End of Rev 1.0
            return q;

        }

        public JsonResult SetUsers(string ID)
        {
            DataTable DT = GroupBeatUsers.Obj.GetUserMap(ID);
            GroupBeatUsers.ObjList.Clear();
            foreach (DataRow dr in DT.Rows)
            {                
                GroupBeatUsers.ObjList.Add(Convert.ToString(dr["USER_ID"]));
            }

            var Selected = GroupBeatUsers.ObjList;
            

            return Json(Selected);
        }

        // Mantis Issue 25536, 25535, 25542, 25543, 25544 [ type added]
        public JsonResult Delete(string ID, string type)
        {
            int output = 0;
            output = GroupBeat.Obj.Delete(ID, type);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

    }
}
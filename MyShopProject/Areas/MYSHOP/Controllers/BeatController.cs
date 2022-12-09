using BusinessLogicLayer.SalesmanTrack;
using MyShop.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class BeatController : Controller
    {
        //
        // GET: /MYSHOP/Beat/
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult PartialGrid()
        {
            return PartialView(GetList());
        }

        public JsonResult SaveGroupBeat(string code, string name, string id)
        {
            int output = 0;
            string Userid = Convert.ToString(Session["userid"]);
            output = GroupBeat.Obj.SaveBeat(code, name, Userid, id);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveGroupBeatUser(string selected, string id)
        {
            int output = 0;
            string Userid = Convert.ToString(Session["userid"]);
            output = GroupBeatUsers.Obj.SaveBeatUser(selected, id);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditGroupBeat(string id)
        {
            DataTable output = new DataTable();
            output = GroupBeat.Obj.EditBeat(id);

            if (output.Rows.Count > 0)
            {
                return Json(new { code = Convert.ToString(output.Rows[0]["CODE"]), name = Convert.ToString(output.Rows[0]["NAME"]) }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { code = "", name = "" }, JsonRequestBehavior.AllowGet);
            }

        }

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

            return PartialView(GetUserList());
        }

        public IEnumerable GetUserList()
        {
            string connectionString = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"]);
            ReportsDataContext dc = new ReportsDataContext(connectionString);
            var q = from d in dc.tbl_master_users
                    select d;
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

        public JsonResult Delete(string ID)
        {
            int output = 0;
            output = GroupBeat.Obj.Delete(ID);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

    }
}
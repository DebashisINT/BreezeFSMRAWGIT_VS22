using BusinessLogicLayer;
using MyShop.Models;
using SalesmanTrack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilityLayer;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class ViewPartyController : Controller
    {
        //
        // GET: /MYSHOP/ViewParty/
        
        public ActionResult ViewParty()
        {
            return View();
        }

        public ActionResult GetType()
        {
            DBEngine obj = new DBEngine();
            DataTable dt = obj.GetDataTable("select id,Name from tbl_shoptypeDetails");
            List<ddl> objddl = new List<ddl>();
            objddl = APIHelperMethods.ToModelList<ddl>(dt);
            return Json(objddl);
        }
        public ActionResult GetTypeOutlet()
        {
            DBEngine obj = new DBEngine();
            DataTable dt = obj.GetDataTable("select shop_typeId,Name  from tbl_shoptype  where IsActive=1");
            List<ddlOutlet> objddl = new List<ddlOutlet>();
            objddl = APIHelperMethods.ToModelList<ddlOutlet>(dt);
            return Json(objddl);
        }
        public ActionResult GetPartyStatus()
        {
            DBEngine obj = new DBEngine();
            DataTable dt = obj.GetDataTable("select ID id ,PARTYSTATUS Name from FSM_PARTYSTATUS");
            List<ddl> objddl = new List<ddl>();
            objddl = APIHelperMethods.ToModelList<ddl>(dt);
            return Json(objddl);
        }


        public class ddl
        {
            public Int64 id { get; set; }
            public string Name { get; set; }

        }

        public class ddlOutlet 
        {
            public Int64 shop_typeId { get; set; }
            public string Name { get; set; }

        }
	}


}
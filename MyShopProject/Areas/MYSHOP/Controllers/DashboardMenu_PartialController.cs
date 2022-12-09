using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer.SalesmanTrack;
using UtilityLayer;
namespace MyShop.Areas.MYSHOP.Controllers
{
    public partial class DashboardMenuController : Controller
    {
       
        //
        // GET: /MYSHOP/DashboardMenu_Partial/

        public ActionResult DashboardNew()
        {
            return PartialView();

        }
        public ActionResult DashboardNewFV()
        {
            return PartialView();

        }
        public ActionResult Mapopen(string StateID)
        {
            List<MapDashboard> AddressList = new List<MapDashboard>();

            Dashboard model = new Dashboard();
            DataTable dtmodellatest = model.GETMapDashboard(StateID, Session["userid"].ToString());
            AddressList = APIHelperMethods.ToModelList<MapDashboard>(dtmodellatest);
        

            return Json(AddressList);
        }
	}
}
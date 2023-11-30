//====================================================== Revision History ==========================================================
//1.0  23-11-2023    V2.0.43    Priti     0027031: Dashboard report issue(check in local Rubyfoods db)
//====================================================== Revision History ==========================================================
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
    public class OutletRouteController : Controller
    {
        //
        // GET: /MYSHOP/OutletRoute/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowRouteOutlet(string id, String Date)
        {
            ViewBag.id = id;
            ViewBag.Date = Date;
            return View();
        }
        //Rev 1.0
        public ActionResult Mapopen(string StateID, String Date, string BranchIds)
        {
            List<MapDashboard> AddressList = new List<MapDashboard>();
            string newdate = Date.Split('-')[2] + '-' + Date.Split('-')[1] + '-' + Date.Split('-')[0];
            Dashboard model = new Dashboard();            
            DataTable dtmodellatest = model.GETMapDashboardEMPOutlet(BranchIds, StateID, newdate, Session["userid"].ToString());          
            AddressList = APIHelperMethods.ToModelList<MapDashboard>(dtmodellatest);

            return Json(AddressList);
        }
        //Rev 1.0 End
    }
}
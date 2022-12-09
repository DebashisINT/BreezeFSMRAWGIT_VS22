using Models;
using System;
using System.Collections.Generic;
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
        
	}
}
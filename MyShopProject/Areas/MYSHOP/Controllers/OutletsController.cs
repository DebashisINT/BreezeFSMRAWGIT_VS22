/****************************************************************************************************************
 * 1.0      03-04-2024    V2.0.46       Sanchita        0027314: The Google API key should store in the database with a new table.
 * ****************************************************************************************************************/
using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class OutletsController : Controller
    {
        //
        // GET: /MYSHOP/Outlets/
        public ActionResult viewOutlets() 
        {
            // Rev 1.0
            CommonBL cbl = new CommonBL();
            ViewBag.GoogleAPIKey = cbl.GetSystemSettingsResult("GoogleMapKey");

            if (ViewBag.GoogleAPIKey != "")
                ViewBag.HasGoogleAPIKey = 1;
            else
                ViewBag.HasGoogleAPIKey = 0;
            // End of Rev 1.0
            return View();
        }
	}
}
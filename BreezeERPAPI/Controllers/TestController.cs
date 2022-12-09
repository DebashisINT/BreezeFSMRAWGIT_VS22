using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BreezeERPAPI.Controllers
{

    
    public class TestController : Controller
    {
       
        public JsonResult Index()
        {
            string URL = "https://www.tradeindia.com/utils/my_inquiry.html?userid=2008551&profile_id=2467957&key=aae74985801ae0f73584d3ebc6645112&from_date=2016-01-01&to_date=2016-12-31&limit=10&page_no=2";
            using (var webClient = new System.Net.WebClient())
            {
                var json = webClient.DownloadString(URL);
                // Now parse with JSON.Net
            }

            return Json(null);
        
        }


	}
}
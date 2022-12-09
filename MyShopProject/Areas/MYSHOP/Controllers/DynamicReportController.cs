using BusinessLogicLayer.SalesmanTrack;
using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class DynamicReportController : Controller
    {
        //
        // GET: /MYSHOP/DynamicReport/
        public ActionResult Index()
        {
            Session["key"] = null;
            DynamicLayoutReport dl = new DynamicLayoutReport();
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            ReportsDataContext dc = new ReportsDataContext(connectionString);
            var q = from d in dc.MASTER_DYNAMICLAYOUT1s
                    //where d.LAYOUT_ISACTIVE == true
                    orderby d.ID ascending
                    select d;
            dl.layout_list = q.ToList();
            return View(dl);
        }

        public ActionResult Report(string key)
        {
            Session["key"] = key;
            return View();
        }


        public PartialViewResult ShowGrid(string fromdate, string todate)
        {
            
            string date1 = DateTime.ParseExact(fromdate, "dd-MM-yyyy",
                                    CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            string date2 = DateTime.ParseExact(todate, "dd-MM-yyyy",
                        CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            DynamicLayout obj = new DynamicLayout();
            DataSet ds = new DataSet();
            ds = obj.GetDynamicData(Convert.ToString(Session["key"]), date1, date2);

            return PartialView(ds);
        }
    }
}
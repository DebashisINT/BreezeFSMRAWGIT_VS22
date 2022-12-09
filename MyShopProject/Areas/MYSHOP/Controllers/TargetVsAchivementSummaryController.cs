using BusinessLogicLayer.SalesmanTrack;
using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilityLayer;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class TargetVsAchivementSummaryController : Controller
    {
        TargetVsAchivementBL tarBL = new TargetVsAchivementBL();
        //
        // GET: /MYSHOP/TargetVsAchivementSummary/
        public ActionResult TargetVsAchivementSummary()
        {
            //DataTable tardt = tarBL.GetSummaryList("GETGRIDDATA","","");

            DataTable statedt = tarBL.GetSummaryList("GETSTATE","","");

            List<MonthList> month = new List<MonthList>();
            month.Add(new MonthList { ID = "JAN", MonthName = "January" });
            month.Add(new MonthList { ID = "FEB", MonthName = "February" });
            month.Add(new MonthList { ID = "MAR", MonthName = "March" });
            month.Add(new MonthList { ID = "APR", MonthName = "April" });
            month.Add(new MonthList { ID = "MAY", MonthName = "May" });
            month.Add(new MonthList { ID = "JUN", MonthName = "June" });
            month.Add(new MonthList { ID = "JUL", MonthName = "July" });
            month.Add(new MonthList { ID = "AUG", MonthName = "August" });
            month.Add(new MonthList { ID = "SEP", MonthName = "September" });
            month.Add(new MonthList { ID = "OCT", MonthName = "October" });
            month.Add(new MonthList { ID = "NOV", MonthName = "November" });
            month.Add(new MonthList { ID = "DEC", MonthName = "December" });


            List<YearList> year = new List<YearList>();
            DateTime dtnow = new DateTime();
            dtnow = DateTime.Now;
            year.Add(new YearList { ID = Convert.ToString(dtnow.Year - 1), YearName = Convert.ToString(dtnow.Year - 1) });
            year.Add(new YearList { ID = Convert.ToString(dtnow.Year ), YearName = Convert.ToString(dtnow.Year) });
            year.Add(new YearList { ID = Convert.ToString(dtnow.Year + 1), YearName = Convert.ToString(dtnow.Year + 1) });



            TargetVsAchivementClass model = new TargetVsAchivementClass();
           // model.TargetVsAchivementList = APIHelperMethods.ToModelList<TargetVsAchivementList>(tardt);
            model.TargetVsAchivementList = new List<TargetVsAchivementList>();

            model.StateListTarget = APIHelperMethods.ToModelList<StateListTarget>(statedt);
            model.MonthList = month;
            model.YearList = year;
            return View(model);
        }

        public PartialViewResult TargetVsAchivementTreeGrid(string Month, string States)
        {
            DataTable tardt = tarBL.GetSummaryList("GETGRIDDATA", Month, States);
            TargetVsAchivementClass model = new TargetVsAchivementClass();
            model.TargetVsAchivementList = APIHelperMethods.ToModelList<TargetVsAchivementList>(tardt);
            return PartialView(model.TargetVsAchivementList);
        }
	}
}
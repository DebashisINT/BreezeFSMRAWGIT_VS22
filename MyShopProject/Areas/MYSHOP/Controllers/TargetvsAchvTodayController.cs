/*******************************************************************************************************************
 * 1.0      Sanchita    V2.0.38     Appconfig and User wise setting "IsAllDataInPortalwithHeirarchy = True" then data 
 *                                  in portal shall be populated based on Hierarchy Only. Refer: 25504
 * 
 * ***************************************************************************************************************/

using BusinessLogicLayer.SalesmanTrack;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilityLayer;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class TargetvsAchvTodayController : Controller
    {
        TargetVsAchivementBL tarBL = new TargetVsAchivementBL();
        //
        // GET: /MYSHOP/TargetvsAchvToday/
        public ActionResult TargetvsAchvToday()
        {
            DataTable statedt = tarBL.GetSummaryList("GETSTATE", "", "");
            TargetVsAchivementRangeClass model = new TargetVsAchivementRangeClass();
            model.TargetVsAchivementList = new List<TargetVsAchivementRangeList>();
            model.StateListTarget = APIHelperMethods.ToModelList<StateListRangeTarget>(statedt);
            return View(model);

        }

        public PartialViewResult TargetVsAchivementRangeGrid(string fromdate, string enddate, string States)
        {
            string datfrmat = fromdate.Split('-')[2] + '-' + fromdate.Split('-')[1] + '-' + fromdate.Split('-')[0];
            string dattoat = enddate.Split('-')[2] + '-' + enddate.Split('-')[1] + '-' + enddate.Split('-')[0];
            DataTable tardt = null;
            double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;
             if (days <= 30)
             {
                // Rev 1.0
                //tardt = tarBL.GetrangeList("GETGRIDDATA", DateTime.ParseExact(fromdate, "dd-MM-yyyy", CultureInfo.InvariantCulture), DateTime.ParseExact(enddate, "dd-MM-yyyy", CultureInfo.InvariantCulture), States);
                tardt = tarBL.GetrangeList("GETGRIDDATA", datfrmat, dattoat, States, Convert.ToString(Session["userid"]));
                // End of Rev 1.0
            }
                TargetVsAchivementRangeClass model = new TargetVsAchivementRangeClass();
            model.TargetVsAchivementList = APIHelperMethods.ToModelList<TargetVsAchivementRangeList>(tardt);
            TempData["DataGrid"] = model.TargetVsAchivementList;
            TempData.Keep();
            return PartialView(model.TargetVsAchivementList);
        }
        public ActionResult ExportGridView(int type)
        {
            ViewData["DataGrid"] = TempData["DataGrid"];
            //ViewData["DashboardGridViewSalesmanDetailType"] = TempData["DashboardGridViewSalesmanDetailType"];
            TempData.Keep();

            if (ViewData["DataGrid"] != null)
            {


                return GridViewExtension.ExportToXls(GetDashboardGridViewSalesman(ViewData["DataGrid"]), ViewData["DataGrid"]);


            }
            return null;
        }

        private GridViewSettings GetDashboardGridViewSalesman(object datatable)
        {
            var settings = new GridViewSettings();
            settings.Name = "gridtreeList";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Daily Target vs Achievement";

            TempData.Keep();
            settings.Columns.Add("DEG", "Designation");

            settings.Columns.Add("EMPNAME", "Employee");
            settings.Columns.Add("LOGINID", "Login ID.");
            settings.Columns.Add("REPORTTO", "Supervisor");

            settings.Columns.Add(x =>
            {
                x.FieldName = "STATE";
                x.Caption = "State";
                x.Width = System.Web.UI.WebControls.Unit.Pixel(200);
                x.GroupIndex = 0;

            });


            //settings.Columns.Add("SEQ", "SL").Width = System.Web.UI.WebControls.Unit.Pixel(200);
            settings.Columns.Add("TODAYNC", "Tgt. New Counter").PropertiesEdit.DisplayFormatString = "0";
            settings.Columns.Add("ACHVNC", "Achv. New Counter").PropertiesEdit.DisplayFormatString = "0";
            settings.Columns.Add("TODAYRV", "Tgt. Re-Visit").PropertiesEdit.DisplayFormatString = "0";
            settings.Columns.Add("ACHVRV", "Achv. Re-Visit").PropertiesEdit.DisplayFormatString = "0";

            settings.Columns.Add("TODAYORDER", "Tgt. Value").PropertiesEdit.DisplayFormatString = "0.00";
            settings.Columns.Add("ACHVCOL", "Achv. Value").PropertiesEdit.DisplayFormatString = "0.00";
            settings.Columns.Add("TODAYCOL", "Tgt. Collection").PropertiesEdit.DisplayFormatString = "0.00";
            settings.Columns.Add("ACHVORD", "Achv. Collection").PropertiesEdit.DisplayFormatString = "0.00";

            settings.SettingsExport.BeforeExport = (s, e) =>
            {
                MVCxGridView g = (MVCxGridView)s;
                g.ExpandAll();
            };

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }
    }
}
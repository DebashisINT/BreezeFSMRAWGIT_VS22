using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BusinessLogicLayer;
using SalesmanTrack;
using System.Data;
using UtilityLayer;
using System.Web.Script.Serialization;
using MyShop.Models;
using BusinessLogicLayer.SalesmanTrack;
using DevExpress.Web.Mvc;
using BusinessLogicLayer.SalesTrackerReports;
using DevExpress.Web;
namespace MyShop.Areas.MYSHOP.Controllers
{
    public class DashboardTeamController : Controller
    {
        //
        // GET: /MYSHOP/DashboardTeam/
        Dashboard dashbrd = new Dashboard();
        DataTable dtdashboard = new DataTable();
        DBDashboardSettings dashboardsetting = new DBDashboardSettings();
        public ActionResult Index()
        {
            return View();
        }

        

        public PartialViewResult DashboardGridViewSalesmanDetailTeam(string ID, string action, string rptype, string empid, string stateid, string designid)
        {
            DBEngine objdb = new DBEngine();
            DataTable dbDashboardData = new DataTable();
            try
            {
                dbDashboardData = dashbrd.GetDashboardGridData(action, rptype, empid, stateid, designid);
                ViewBag.Type = ID;
                if (ID == "1")
                {
                    dbDashboardData = objdb.GetDataTable("SELECT CAST(DEG_ID as varchar(50)) DEGID,DESIGNATION [Designation],STATE [State],VISITCNT [Visit Count],EMPCNT [Employee Count], (VISITCNT / EMPCNT) [Avg Count] FROm FTSDASHBOARDGRIDDETAILS_REPORT WHERE USERID='" + Convert.ToString(Session["userid"]) + "' and ACTION='" + Convert.ToString(action) + "' and RPTTYPE='" + rptype + "' ORDER BY VISITCNT DESC");
                }
                else if (ID == "2")
                {
                    dbDashboardData = objdb.GetDataTable("SELECT EMPCODE EMPID,EMPNAME [Employee Name],STATE [State],DESIGNATION [Designation],SHOPASSIGN [Assigned Shops],DISTANCE_COVERED [KM Travelled],SHOPS_VISITED [Today's Visit],LAST7DAYVISIT [Visit-Last 7 Days],PENDINGVISIT7DAYS [Pending-Last 7 Days] FROM FTSDASHBOARDGRIDDETAILS_REPORT  WHERE USERID='" + Convert.ToString(Session["userid"]) + "' and ACTION='" + Convert.ToString(action) + "' and RPTTYPE='" + rptype + "' order by SHOPS_VISITED DESC");
                }
                else if (ID == "3")
                {
                    dbDashboardData = objdb.GetDataTable("SELECT EMPCODE EMPID,SHOP_NAME [Shop Name],SHOP_TYPE [Shop Type],SHOPLOCATION [Location],SHOPCONTACT [Mobile No.],VISITED_TIME [Visit Time],SPENT_DURATION [Duration Spent],VISIT_TYPE [Visit Type] FROM FTSDASHBOARDGRIDDETAILS_REPORT  WHERE USERID='" + Convert.ToString(Session["userid"]) + "' and ACTION='" + Convert.ToString(action) + "' and RPTTYPE='" + rptype + "' ORDER BY VISITED_TIME");
                }
            }
            catch { }
            TempData["DashboardGridViewSalesmanDetail"] = dbDashboardData;
            TempData["DashboardGridViewSalesmanDetailType"] = ViewBag.Type;
            TempData.Keep();
            return PartialView(dbDashboardData);
        }
        // Mantis Issue 25468
        public PartialViewResult DashboardGridViewSalesmanDetailTeamH(string ID, string action, string rptype, string empid, string stateid, string designid)
        {
            DBEngine objdb = new DBEngine();
            DataTable dbDashboardData = new DataTable();
            try
            {
                dbDashboardData = dashbrd.GetDashboardGridDataH(action, rptype, empid, stateid, designid);
                ViewBag.Type = ID;
                if (ID == "1")
                {
                    dbDashboardData = objdb.GetDataTable("SELECT CAST(DEG_ID as varchar(50)) DEGID,DESIGNATION [Designation],STATE [State],VISITCNT [Visit Count],EMPCNT [Employee Count], (VISITCNT / EMPCNT) [Avg Count] FROm FTSDASHBOARDGRIDDETAILS_REPORT_HIERARCHY WHERE USERID='" + Convert.ToString(Session["userid"]) + "' and ACTION='" + Convert.ToString(action) + "' and RPTTYPE='" + rptype + "' ORDER BY VISITCNT DESC");
                }
                else if (ID == "2")
                {
                    dbDashboardData = objdb.GetDataTable("SELECT EMPCODE EMPID,EMPNAME [Employee Name],STATE [State],DESIGNATION [Designation],SHOPASSIGN [Assigned Shops],DISTANCE_COVERED [KM Travelled],SHOPS_VISITED [Today's Visit],LAST7DAYVISIT [Visit-Last 7 Days],PENDINGVISIT7DAYS [Pending-Last 7 Days] FROM FTSDASHBOARDGRIDDETAILS_REPORT_HIERARCHY  WHERE USERID='" + Convert.ToString(Session["userid"]) + "' and ACTION='" + Convert.ToString(action) + "' and RPTTYPE='" + rptype + "' order by SHOPS_VISITED DESC");
                }
                else if (ID == "3")
                {
                    dbDashboardData = objdb.GetDataTable("SELECT EMPCODE EMPID,SHOP_NAME [Shop Name],SHOP_TYPE [Shop Type],SHOPLOCATION [Location],SHOPCONTACT [Mobile No.],VISITED_TIME [Visit Time],SPENT_DURATION [Duration Spent],VISIT_TYPE [Visit Type] FROM FTSDASHBOARDGRIDDETAILS_REPORT_HIERARCHY  WHERE USERID='" + Convert.ToString(Session["userid"]) + "' and ACTION='" + Convert.ToString(action) + "' and RPTTYPE='" + rptype + "' ORDER BY VISITED_TIME");
                }
            }
            catch { }
            TempData["DashboardGridViewSalesmanDetailH"] = dbDashboardData;
            TempData["DashboardGridViewSalesmanDetailTypeH"] = ViewBag.Type;
            TempData.Keep();
            return PartialView(dbDashboardData);
        }
        // End of Mantis Issue 25468
	}
}
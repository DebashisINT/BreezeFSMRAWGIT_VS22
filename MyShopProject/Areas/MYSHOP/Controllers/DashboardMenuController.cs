/******************************************************************************************************************
 * Rev 1.0      21-03-2023      Sanchita    V2.0.39     Dashboard optimization. Refer: 25741
 * Rev 2.0      27-06-2023      Sanchita    V2.0.41     State & Branch selection facility is required in the Order Analytics in Dashboard
 *                                                      Refer: 26309
 * Rev 3.0      27-06-2023      Sanchita    V2.0.41     In Portal Dashboard, under Field Visit and Team Visit tabs Order Value coloumn shall be added
 * Rev 4.0      21-08-2024      Priti       V2.0.48     LMS Dashboard.Mantis: 0027667                                                    Refer: 27403
 *******************************************************************************************************************/
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
using System.Runtime.InteropServices.ComTypes;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Data.SqlClient;


namespace MyShop.Areas.MYSHOP.Controllers
{
    public partial class DashboardMenuController : Controller
    {
        Dashboard dashbrd = new Dashboard();
        DataTable dtdashboard = new DataTable();
        DBDashboardSettings dashboardsetting = new DBDashboardSettings();

        public ActionResult Dashboard()
        {
            try
            {
                DashboardModelC model = new DashboardModelC();
                string userid = Session["userid"].ToString();

                string todaydate = DateTime.Now.ToString("yyyy-MM-dd");
                DataTable dtdashboard = dashbrd.GetFtsDashboardyList(todaydate, userid);

                model.employeecount = "0";
                model.employeeatwork = "0";
                model.employeeonleave = "0";
                model.employeenotlogin = "0";
                model.Userid = userid;

                BusinessLogicLayer.DBEngine objEngine = new BusinessLogicLayer.DBEngine();
                DataTable dt = objEngine.GetDataTable("FTSDASHBOARD_REPORT", " ACTION,EMPCNT,AT_WORK,ON_LEAVE,NOT_LOGIN ", " USERID='" + userid + "' AND RPTTYPE='Summary' AND ACTION in ('EMP','AT_WORK','NOT_LOGIN','ON_LEAVE')");
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string strString = Convert.ToString(row["ACTION"]);
                        if (strString == "EMP") model.employeecount = Convert.ToString(row["EMPCNT"]);
                        else if (strString == "AT_WORK") model.employeeatwork = Convert.ToString(row["AT_WORK"]);
                        else if (strString == "ON_LEAVE") model.employeeonleave = Convert.ToString(row["ON_LEAVE"]);
                        else if (strString == "NOT_LOGIN") model.employeenotlogin = Convert.ToString(row["NOT_LOGIN"]);


                    }
                }

                return View(model);





            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }        
        public ActionResult FSMDashboard()
        {
            TempData["LMSDashboardGridView"] = null;
            if (Session["userid"] != null)
            {
                FSMDashBoardFilter obj = new FSMDashBoardFilter();
                try
                {
                    string userid = Session["userid"].ToString();
                    DataSet dsdashboard = dashboardsetting.GetDashboardSettingMappedListByID(Convert.ToInt32(userid));
                    DataTable dt = dsdashboard.Tables[0];
                    List<DashboardSettingMapped> list = new List<DashboardSettingMapped>();
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            DashboardSettingMapped objmap = null;
                            foreach (DataRow row in dt.Rows)
                            {
                                objmap = new DashboardSettingMapped();
                                objmap.DashboardSettingMappedID = Convert.ToInt32(row["DashboardSettingMappedID"]);
                                objmap.FKDashboardSettingID = Convert.ToInt32(row["FKDashboardSettingID"]);
                                objmap.FKuser_id = Convert.ToInt32(row["FKuser_id"]);
                                objmap.FKDashboardDetailsID = Convert.ToInt32(row["FKDashboardDetailsID"]);
                                objmap.DetailsName = Convert.ToString(row["DetailsName"]);
                                list.Add(objmap);
                            }
                        }
                    }
                    obj.DashboardSettingMappedList = list;
                }
                catch { }

                //Mantis Issue 24729
                Session["PageloadChk"] = "1";
                //End of Mantis Issue 24729
                return View(obj);

            }
            else
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }


        }

        public ActionResult Dashboard1()
        {

            return View();
        }
        // Mantis Issue 24729
        //public JsonResult GetDashboardData(string stateid)
        public JsonResult GetDashboardData(string stateid, string branchid)
            // End of Mantis Issue 24729
        {


            Dashboard dashboarddataobj = new Dashboard();
            FSMDashboard Dashboarddata = new FSMDashboard();
            try
            {
                // Mantis Issue 24729
                //DataSet objData = dashboarddataobj.CreateLINQforDashBoard(stateid);
                DataSet objData = dashboarddataobj.CreateLINQforDashBoard(stateid, branchid);
                // End of Mantis Issue 24729
                //DataTable objData = dashboarddataobj.GetDashboardAttendanceData();

                int NOT_LOGIN = 0;
                int AT_WORK = 0;
                int ON_LEAVE = 0;
                int Total = 0;

                string TOTALSHOP = "0";
                string REVISIT = "0";
                string NEWSHOPVISIT = "0";

                string AVGSHOPVISIT = "0";
                string AVGDURATION = "00:00";
                string TODAYSALES = "0.00";
                string AVGSALES = "0.00";
                string TOTALSALES = "0.00";

                foreach (DataRow item in objData.Tables[0].Rows)
                {
                    if (Convert.ToString(item["ACTION"]) == "NOT_LOGIN")
                    {
                        NOT_LOGIN = Convert.ToInt32(item["Count"]);
                    }
                    if (Convert.ToString(item["ACTION"]) == "AT_WORK")
                    {
                        AT_WORK = Convert.ToInt32(item["Count"]);
                    }
                    if (Convert.ToString(item["ACTION"]) == "ON_LEAVE")
                    {
                        ON_LEAVE = Convert.ToInt32(item["Count"]);
                    }
                    if (Convert.ToString(item["ACTION"]) == "EMP")
                    {
                        Total = Convert.ToInt32(item["Count"]);

                    }

                }
                Dashboarddata.lblAtWork = AT_WORK;
                Dashboarddata.lblOnLeave = ON_LEAVE;
                Dashboarddata.lblNotLoggedIn = NOT_LOGIN;
                Dashboarddata.lblTotal = Total;

                Dashboarddata.NewVisit = Convert.ToInt32(NEWSHOPVISIT);
                Dashboarddata.ReVisit = Convert.ToInt32(REVISIT);
                Dashboarddata.TotalVisit = Convert.ToInt32(TOTALSHOP);
                Dashboarddata.AvgPerDay = Convert.ToDecimal(AVGSHOPVISIT);
                Dashboarddata.AvgDurationPerShop = AVGDURATION;
                Dashboarddata.AVGSALES = AVGSALES;
                Dashboarddata.TODAYSALES = TODAYSALES;
                Dashboarddata.TOTALSALES = TOTALSALES;
            }
            catch
            {
            }
            return Json(Dashboarddata);
        }
        // Team Visit
        public JsonResult GetDashboardDataVisit(string stateid, string branchid)
        {
            Dashboard dashboarddataobj = new Dashboard();
            FSMDashboard Dashboarddata = new FSMDashboard();
            try
            {
                DataSet objData = dashboarddataobj.CreateLINQforDashBoardTeamVisit(stateid, branchid);

                int NOT_LOGIN = 0;
                int AT_WORK = 0;
                int ON_LEAVE = 0;
                int Total = 0;

                string TOTALSHOP = "0";
                string REVISIT = "0";
                string NEWSHOPVISIT = "0";

                string AVGSHOPVISIT = "0";
                string AVGDURATION = "00:00";
                string TODAYSALES = "0.00";
                string AVGSALES = "0.00";
                string TOTALSALES = "0.00";

                foreach (DataRow item in objData.Tables[0].Rows)
                {
                    if (Convert.ToString(item["ACTION"]) == "NOT_LOGIN")
                    {
                        NOT_LOGIN = Convert.ToInt32(item["Count"]);
                    }
                    if (Convert.ToString(item["ACTION"]) == "AT_WORK")
                    {
                        AT_WORK = Convert.ToInt32(item["Count"]);
                    }
                    if (Convert.ToString(item["ACTION"]) == "ON_LEAVE")
                    {
                        ON_LEAVE = Convert.ToInt32(item["Count"]);
                    }
                    if (Convert.ToString(item["ACTION"]) == "EMP")
                    {
                        Total = Convert.ToInt32(item["Count"]);

                    }

                }

                Dashboarddata.lblAtWork = AT_WORK;
                Dashboarddata.lblOnLeave = ON_LEAVE;
                Dashboarddata.lblNotLoggedIn = NOT_LOGIN;
                Dashboarddata.lblTotal = Total;

                Dashboarddata.NewVisit = Convert.ToInt32(NEWSHOPVISIT);
                Dashboarddata.ReVisit = Convert.ToInt32(REVISIT);
                Dashboarddata.TotalVisit = Convert.ToInt32(TOTALSHOP);
                Dashboarddata.AvgPerDay = Convert.ToDecimal(AVGSHOPVISIT);
                Dashboarddata.AvgDurationPerShop = AVGDURATION;
                Dashboarddata.AVGSALES = AVGSALES;
                Dashboarddata.TODAYSALES = TODAYSALES;
                Dashboarddata.TOTALSALES = TOTALSALES;
            }
            catch
            {
            }
            return Json(Dashboarddata);
        }
                
        public PartialViewResult DashboardGridView(FSMDashBoardFilter dd)
        {
            ViewBag.WindowSize = dd.WindowSize;
            SalesSummary_Report objgps = new SalesSummary_Report();
            DBEngine objdb = new DBEngine();
            DataTable dbDashboardData = new DataTable();
            string query = "";
            string orderby = "";

            orderby = " order by EMPNAME";

            if (dd.Type == "Attendance")
            {
                string ColumnName = "*";

                // Mantis Issue 25434 [ column BRANCH [Branch] added ]
                if (dd.FilterName == "AT_WORK")
                {
                    ColumnName = "EMPNAME [Employee],DESIGNATION [Designation],BRANCH [Branch],DEPARTMENT [Department],ISNULL(CONTACTNO,'') [Mobile No.],LOGGEDIN [First in time],CASE WHEN LOGEDOUT='--' THEN '' ELSE LOGEDOUT END [Last logout time],CURRENT_STATUS [Current Status],ISNULL(GPS_INACTIVE_DURATION,'00:00') +' (HH:MM)' [GPS Inactivity],ISNULL(SHOPS_VISITED,0) [Shops Visited],ISNULL(TOTAL_ORDER_BOOKED_VALUE,'0.00') [Order Value],ISNULL(TOTAL_COLLECTION,'0.00') [Collection Amt.],EMPCODE EMPID";

                    orderby = " order by SHOPS_VISITED DESC";
                }
                else if (dd.FilterName == "NOT_LOGIN")
                {
                    ColumnName = "EMPNAME [Employee],DESIGNATION [Designation],BRANCH [Branch],DEPARTMENT [Department],ISNULL(STATE,'') [State],REPORTTO [Supervisor],CONTACTNO [Contact No.]";

                }
                else if (dd.FilterName == "ON_LEAVE")
                {
                    ColumnName = "EMPNAME [Employee],DESIGNATION [Designation],BRANCH [Branch],DEPARTMENT [Department],LEAVEDATE [Applied Leave Date]";
                }
                else if (dd.FilterName == "EMP")
                {
                    ColumnName = "EMPNAME [Employee],DESIGNATION [Designation],BRANCH [Branch],DEPARTMENT [Department],REPORTTO [Supervisor],CONTACTNO [Contact No.]";
                }

                //string StateId = dd.statefilterid == "0" ? "" : dd.statefilterid;

                // Mantis Issue 24765
                //DataTable dt = objgps._GetSalesSummaryReport(DateTime.Now.ToString("yyyy-MM-dd"), Convert.ToString(Session["userid"]), dd.StateId == "0" ? "" : dd.StateId, "", "", dd.FilterName);
                DataTable dt = objgps._GetSalesSummaryReport(DateTime.Now.ToString("yyyy-MM-dd"), Convert.ToString(Session["userid"]), dd.StateId == "0" ? "" : dd.StateId, "", "", dd.FilterName, dd.BranchId == "0" ? "" : dd.BranchId);
                // End of Mantis Issue 24765

                query = "Select " + ColumnName + " from FTSDASHBOARD_REPORT where USERID='" + Convert.ToString(Session["userid"]) + "' and ACTION='" + Convert.ToString(dd.FilterName) + "'";

                //if (dd.statefilterid != null)
                //    query += " AND STATE='" + Convert.ToString(dd.statefilterid) + "'";
                //else
                //    query += " AND ISNULL(STATE,'')=''";

                //if (Convert.ToString(dd.designation) != "" && dd.designation != null)
                //    query += " AND DESIGNATION='" + Convert.ToString(dd.designation) + "'";

                //if (Convert.ToString(dd.statefilterid) != "" && dd.statefilterid != null)
                //    query += " AND STATE='" + Convert.ToString(dd.statefilterid) + "'";

                query += orderby;

                dbDashboardData = objdb.GetDataTable(query);
            }
            // Rev Tanmoy store query in tempdata
            // TempData["DashboardGridView"] = dbDashboardData;
            TempData["DashboardGridView"] = query;
            //End Rev 
            // DataTable MyInstrumentsList = null;
            return PartialView(dbDashboardData);
        }

        public PartialViewResult DashboardGridViewFV(FSMDashBoardFilter dd)
        {
            ViewBag.WindowSize = dd.WindowSize;
            SalesSummary_Report objgps = new SalesSummary_Report();
            DBEngine objdb = new DBEngine();
            DataTable dbDashboardData = new DataTable();
            string query = "";
            string orderby = "";

            orderby = " order by EMPNAME";

            if (dd.Type == "Attendance")
            {
                string ColumnName = "*";


                if (dd.FilterName == "AT_WORK")
                {
                    // Rev 3.0
                    //ColumnName = "EMPNAME [Employee],DESIGNATION [Designation],EMPID [Employee ID],BRANCH [Branch],ISNULL(CONTACTNO,'') [Login ID],LOGGEDIN [First in time],CASE WHEN LOGEDOUT='--' THEN '' ELSE LOGEDOUT END [Last logout time],CURRENT_STATUS [Current Status],ISNULL(GPS_INACTIVE_DURATION,'00:00') +' (HH:MM)' [GPS Inactivity],ISNULL(SHOPS_VISITED,0) [Shops Visited],ISNULL(TOTAL_ORDER_BOOKED_VALUE,'0.00') [Sales Value],ISNULL(TOTAL_COLLECTION,'0.00') [Collection Amt.],EMPCODE EMPID";
                    ColumnName = "EMPNAME [Employee],DESIGNATION [Designation],EMPID [Employee ID],BRANCH [Branch],ISNULL(CONTACTNO,'') [Login ID],LOGGEDIN [First in time],CASE WHEN LOGEDOUT='--' THEN '' ELSE LOGEDOUT END [Last logout time],CURRENT_STATUS [Current Status],ISNULL(GPS_INACTIVE_DURATION,'00:00') +' (HH:MM)' [GPS Inactivity],ISNULL(SHOPS_VISITED,0) [Shops Visited],ISNULL(TOTAL_ORDER_BOOKED_VALUE,'0.00') [Sales Value],ISNULL(ITCORDER_VALUE,'0.00') [Order Value],ISNULL(TOTAL_COLLECTION,'0.00') [Collection Amt.],EMPCODE EMPID ";
                    // End of Rev 3.0

                    orderby = " order by SHOPS_VISITED DESC";
                }
                else if (dd.FilterName == "NOT_LOGIN")
                {
                    ColumnName = "EMPNAME [Employee],DESIGNATION [Designation],EMPID [Employee ID],BRANCH [Branch],ISNULL(STATE,'') [State],REPORTTO [Supervisor],REPORTTOUID [Supervisor ID],CONTACTNO [Login ID]";

                }
                else if (dd.FilterName == "ON_LEAVE")
                {
                    ColumnName = "EMPNAME [Employee],DESIGNATION [Designation],EMPID [Employee ID],BRANCH [Branch],LEAVEDATE [Applied Leave Date]";
                }
                else if (dd.FilterName == "EMP")
                {
                    ColumnName = "EMPNAME [Employee],DESIGNATION [Designation],EMPID [Employee ID],BRANCH [Branch],REPORTTO [Supervisor],REPORTTOUID [Supervisor ID],CONTACTNO [Login ID]";
                }

                //string StateId = dd.statefilterid == "0" ? "" : dd.statefilterid;

                // Mantis Issue 24765
                //DataTable dt = objgps._GetSalesSummaryReport(DateTime.Now.ToString("yyyy-MM-dd"), Convert.ToString(Session["userid"]), dd.StateId == "0" ? "" : dd.StateId, "", "", dd.FilterName);
                DataTable dt = objgps._GetSalesSummaryReport(DateTime.Now.ToString("yyyy-MM-dd"), Convert.ToString(Session["userid"]), dd.StateId == "0" ? "" : dd.StateId, "", "", dd.FilterName, dd.BranchId == "0" ? "" : dd.BranchId);
                // End of Mantis Issue 24765

                query = "Select " + ColumnName + " from FTSDASHBOARD_REPORT where USERID='" + Convert.ToString(Session["userid"]) + "' and ACTION='" + Convert.ToString(dd.FilterName) + "'";

                //if (dd.statefilterid != null)
                //    query += " AND STATE='" + Convert.ToString(dd.statefilterid) + "'";
                //else
                //    query += " AND ISNULL(STATE,'')=''";

                //if (Convert.ToString(dd.designation) != "" && dd.designation != null)
                //    query += " AND DESIGNATION='" + Convert.ToString(dd.designation) + "'";

                //if (Convert.ToString(dd.statefilterid) != "" && dd.statefilterid != null)
                //    query += " AND STATE='" + Convert.ToString(dd.statefilterid) + "'";

                query += orderby;

                dbDashboardData = objdb.GetDataTable(query);
            }
            // Rev Tanmoy store query in tempdata
            // TempData["DashboardGridView"] = dbDashboardData;
            TempData["DashboardGridViewFV"] = query;
            //End Rev 
            // DataTable MyInstrumentsList = null;
            return PartialView(dbDashboardData);
        }
        // Team Visit
        public PartialViewResult DashboardGridViewTeam(FSMDashBoardFilter dd)
        {
            ViewBag.WindowSize = dd.WindowSize;
            SalesSummary_Report objgps = new SalesSummary_Report();
            DBEngine objdb = new DBEngine();
            DataTable dbDashboardData = new DataTable();
            string query = "";
            string orderby = "";

            orderby = " order by EMPNAME";

            if (dd.Type == "Attendance")
            {
                string ColumnName = "*";


                if (dd.FilterName == "AT_WORK")
                {
                    // Rev 3.0
                    //ColumnName = "EMPNAME [Employee],DESIGNATION [Designation],EMPID [Employee ID],BRANCHDESC [Branch],ISNULL(CONTACTNO,'') [Login ID],LOGGEDIN [First in time],CASE WHEN LOGEDOUT='--' THEN '' ELSE LOGEDOUT END [Last logout time],CURRENT_STATUS [Current Status],ISNULL(GPS_INACTIVE_DURATION,'00:00') +' (HH:MM)' [GPS Inactivity],ISNULL(SHOPS_VISITED,0) [Shops Visited],ISNULL(TOTAL_ORDER_BOOKED_VALUE,'0.00') [Sales Value],ISNULL(TOTAL_COLLECTION,'0.00') [Collection Amt.],CHANNEL [Channel],CIRCLE [Circle],SECTION [Section], EMPCODE EMPID";
                    ColumnName = "EMPNAME [Employee],DESIGNATION [Designation],EMPID [Employee ID],BRANCHDESC [Branch],ISNULL(CONTACTNO,'') [Login ID],LOGGEDIN [First in time],CASE WHEN LOGEDOUT='--' THEN '' ELSE LOGEDOUT END [Last logout time],CURRENT_STATUS [Current Status],ISNULL(GPS_INACTIVE_DURATION,'00:00') +' (HH:MM)' [GPS Inactivity],ISNULL(SHOPS_VISITED,0) [Shops Visited],ISNULL(TOTAL_ORDER_BOOKED_VALUE,'0.00') [Sales Value],ISNULL(ITCORDER_VALUE,'0.00') [Order Value],ISNULL(TOTAL_COLLECTION,'0.00') [Collection Amt.],CHANNEL [Channel],CIRCLE [Circle],SECTION [Section], EMPCODE EMPID";
                    // End of Rev 3.0

                    orderby = " order by SHOPS_VISITED DESC";
                }
                else if (dd.FilterName == "NOT_LOGIN")
                {
                    ColumnName = "EMPNAME [Employee],DESIGNATION [Designation],EMPID [Employee ID],BRANCHDESC [Branch],ISNULL(STATE,'') [State],REPORTTO [Supervisor], REPORTTOUID [Supervisor ID], CONTACTNO [Login ID]";

                }
                else if (dd.FilterName == "ON_LEAVE")
                {
                    ColumnName = "EMPNAME [Employee],DESIGNATION [Designation],EMPID [Employee ID],BRANCHDESC [Branch],LEAVEDATE [Applied Leave Date]";
                }
                else if (dd.FilterName == "EMP")
                {
                    ColumnName = "EMPNAME [Employee],DESIGNATION [Designation],EMPID [Employee ID],BRANCHDESC [Branch],REPORTTO [Supervisor], REPORTTOUID [Supervisor ID],CONTACTNO [Login ID], CHANNEL [Channel],CIRCLE [Circle],SECTION [Section]";
                }

                // mantis issue 25567 
                //DataTable dt = objgps._GetSalesSummaryReportTeam(DateTime.Now.ToString("yyyy-MM-dd"), Convert.ToString(Session["userid"]), dd.StateId == "0" ? "" : dd.StateId, dd.BranchIdTV, "", "", dd.FilterName);
                DataTable dt = objgps._GetSalesSummaryReportTeam(DateTime.Now.ToString("yyyy-MM-dd"), Convert.ToString(Session["userid"]), dd.StateId == "0" ? "" : dd.StateId, dd.BranchIdTV == "0" ? "" : dd.BranchIdTV, "", "", dd.FilterName);
                // End of mantis issue 25567

                query = "Select " + ColumnName + " from FTSTEAMVISITDASHBOARD_REPORT where USERID='" + Convert.ToString(Session["userid"]) + "' and ACTION='" + Convert.ToString(dd.FilterName) + "'";

                query += orderby;

                dbDashboardData = objdb.GetDataTable(query);
            }
            // Rev Tanmoy store query in tempdata
            // TempData["DashboardGridView"] = dbDashboardData;
            TempData["DashboardGridViewTeam"] = query;
            //End Rev 
            // DataTable MyInstrumentsList = null;
            return PartialView(dbDashboardData);
        }

       
        public PartialViewResult DashboardGridViewDetailsTeamVisit(FSMDashBoardFilter dd)
        {
            ViewBag.WindowSize = dd.WindowSize;
            DataTable dbDashboardData = new DataTable();
            dbDashboardData = dashbrd.GetSalesManVisitDetails(dd.EMPCODE);
            TempData["DashboardGridViewDetailsTeamVisit"] = dbDashboardData;
            return PartialView(dbDashboardData);
        }
        //Team visit End

        public PartialViewResult DashboardSummaryGridView(FSMDashBoardFilter dd)
        {
            ViewBag.WindowSize = dd.WindowSize;
            SalesSummary_Report objgps = new SalesSummary_Report();
            DBEngine objdb = new DBEngine();
            DataTable dbDashboardData = new DataTable();

            if (dd.Type == "Attendance")
            {
                string ColumnName = "*";

                ColumnName = "STATE [State],DESIGNATION [Designation],COUNT(*) Count";


                if (dd.FilterName != "AT_WORK")
                {
                    // Mantis Issue 24765
                    //DataTable dt = objgps._GetSalesSummaryReport(DateTime.Now.ToString("yyyy-MM-dd"), Convert.ToString(Session["userid"]), dd.StateId == "0" ? "" : dd.StateId, "", "", dd.FilterName);
                    DataTable dt = objgps._GetSalesSummaryReport(DateTime.Now.ToString("yyyy-MM-dd"), Convert.ToString(Session["userid"]), dd.StateId == "0" ? "" : dd.StateId, "", "", dd.FilterName,dd.BranchId);
                    // End o f Mantis Issue 24765

                    if (dd.StateId != null && dd.StateId != "0")
                    {
                        dbDashboardData = objdb.GetDataTable("Select " + ColumnName + " from FTSDASHBOARD_REPORT where USERID='" + Convert.ToString(Session["userid"]) + "' and ACTION='" + Convert.ToString(dd.FilterName) + "' GROUP BY DESIGNATION,STATE order by STATE,COUNT(*) DESC");
                    }
                    else
                    {
                        dbDashboardData = objdb.GetDataTable("Select " + ColumnName + " from FTSDASHBOARD_REPORT where USERID='" + Convert.ToString(Session["userid"]) + "' and ACTION='" + Convert.ToString(dd.FilterName) + "'  GROUP BY DESIGNATION,STATE order by STATE");
                    }
                }
                else
                {
                    DataTable dt = dashbrd.GetAtWorkSummary(dd.StateId);
                    dbDashboardData = objdb.GetDataTable("Select STATE [State],DESIGNATION [Designation],EMPCNT [Count] from FTSDASHBOARD_REPORT where USERID='" + Convert.ToString(Session["userid"]) + "' and ACTION='" + Convert.ToString(dd.FilterName) + "'");
                }

                ViewBag.Type = "Attendance";

            }
            else if (dd.Type == "Tracking")
            {
                dbDashboardData = dashbrd.GetLiveVisits(dd.StateId);
                ViewBag.Type = "Tracking";
            }
            if (dbDashboardData.Rows.Count > 0)
            {
                TempData["ExportDashboardSummaryGridList"] = dbDashboardData;
                TempData.Keep();
                ViewBag.ExportDashboardSummaryGridListCount = dbDashboardData.Rows.Count;
            }
            else
            {

                ViewBag.ExportDashboardSummaryGridListCount = "0";
            }

            // DataTable MyInstrumentsList = null;
            return PartialView(dbDashboardData);
        }

        public PartialViewResult DashboardSummaryGridViewFV(FSMDashBoardFilter dd)
        {
            ViewBag.WindowSize = dd.WindowSize;
            SalesSummary_Report objgps = new SalesSummary_Report();
            DBEngine objdb = new DBEngine();
            DataTable dbDashboardData = new DataTable();

            if (dd.Type == "Attendance")
            {
                string ColumnName = "*";

                ColumnName = "STATE [State],DESIGNATION [Designation],COUNT(*) Count";


                if (dd.FilterName != "AT_WORK")
                {
                    // Mantis Issue 24765
                    //DataTable dt = objgps._GetSalesSummaryReport(DateTime.Now.ToString("yyyy-MM-dd"), Convert.ToString(Session["userid"]), dd.StateId == "0" ? "" : dd.StateId, "", "", dd.FilterName);
                    DataTable dt = objgps._GetSalesSummaryReport(DateTime.Now.ToString("yyyy-MM-dd"), Convert.ToString(Session["userid"]), dd.StateId == "0" ? "" : dd.StateId, "", "", dd.FilterName, dd.BranchId);
                    // End o f Mantis Issue 24765

                    if (dd.StateId != null && dd.StateId != "0")
                    {
                        dbDashboardData = objdb.GetDataTable("Select " + ColumnName + " from FTSDASHBOARD_REPORT where USERID='" + Convert.ToString(Session["userid"]) + "' and ACTION='" + Convert.ToString(dd.FilterName) + "' GROUP BY DESIGNATION,STATE order by STATE,COUNT(*) DESC");
                    }
                    else
                    {
                        dbDashboardData = objdb.GetDataTable("Select " + ColumnName + " from FTSDASHBOARD_REPORT where USERID='" + Convert.ToString(Session["userid"]) + "' and ACTION='" + Convert.ToString(dd.FilterName) + "'  GROUP BY DESIGNATION,STATE order by STATE");
                    }
                }
                else
                {
                    DataTable dt = dashbrd.GetAtWorkSummary(dd.StateId);
                    dbDashboardData = objdb.GetDataTable("Select STATE [State],DESIGNATION [Designation],EMPCNT [Count] from FTSDASHBOARD_REPORT where USERID='" + Convert.ToString(Session["userid"]) + "' and ACTION='" + Convert.ToString(dd.FilterName) + "'");
                }

                ViewBag.Type = "Attendance";

            }
            else if (dd.Type == "Tracking")
            {
                dbDashboardData = dashbrd.GetLiveVisits(dd.StateId);
                ViewBag.Type = "Tracking";
            }
            if (dbDashboardData.Rows.Count > 0)
            {
                TempData["ExportDashboardSummaryGridListFV"] = dbDashboardData;
                TempData.Keep();
                ViewBag.ExportDashboardSummaryGridListCount = dbDashboardData.Rows.Count;
            }
            else
            {

                ViewBag.ExportDashboardSummaryGridListCount = "0";
            }

            // DataTable MyInstrumentsList = null;
            return PartialView(dbDashboardData);
        }
        public PartialViewResult DashboardGridViewDetails(FSMDashBoardFilter dd)
        {
            ViewBag.WindowSize = dd.WindowSize;
            DataTable dbDashboardData = new DataTable();
            dbDashboardData = dashbrd.GetSalesManVisitDetails(dd.EMPCODE);
            TempData["DashboardGridViewDetails"] = dbDashboardData;
            return PartialView(dbDashboardData);
        }

        public PartialViewResult DashboardGridViewDetailsFV(FSMDashBoardFilter dd)
        {
            ViewBag.WindowSize = dd.WindowSize;
            DataTable dbDashboardData = new DataTable();
            dbDashboardData = dashbrd.GetSalesManVisitDetails(dd.EMPCODE);
            TempData["DashboardGridViewDetailsFV"] = dbDashboardData;
            return PartialView(dbDashboardData);
        }
        public PartialViewResult DashboardGridViewSalesmanDetail(string ID, string action, string rptype, string empid, string stateid, string designid)
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
        public PartialViewResult DashboardGridViewSalesmanDetailFV(string ID, string action, string rptype, string empid, string stateid, string designid)
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
        
        public ActionResult DashboardStateComboboxFV()
        {

            FSMDashBoardFilter dashboard = new FSMDashBoardFilter();
            string userid = Session["userid"].ToString();
            //string userid = "0";
            List<StateData> statedate = new List<StateData>();
            List<StateData> statedateobj = new List<StateData>();
            try
            {
                StateData obj = null;
                statedate = dashboard.GetStateList(Convert.ToInt32(userid));
                foreach (var item in statedate)
                {
                    obj = new StateData();
                    obj.StateID = !String.IsNullOrEmpty(item.id) ? Convert.ToInt32(item.id) : 0;
                    obj.name = item.name;
                    statedateobj.Add(obj);
                }
            }
            catch { }
            ViewBag.StateListCount = statedate.Count;
            // Rev 1.0
            TempData["statedate"] = statedate;
            TempData["statedateobj"] = statedateobj;
            // End of Rev 1.0

            return PartialView(statedateobj);
        }
        public ActionResult DashboardStateCombobox()
        {
           
            FSMDashBoardFilter dashboard = new FSMDashBoardFilter();
            string userid = Session["userid"].ToString();
            //string userid = "0";
            List<StateData> statedate = new List<StateData>();
            List<StateData> statedateobj = new List<StateData>();

            // Rev 1.0
            if (TempData["statedateobj"] == null)
            {
                // End of Rev 1.0
                try
                {
                    StateData obj = null;
                    statedate = dashboard.GetStateList(Convert.ToInt32(userid));
                    foreach (var item in statedate)
                    {
                        obj = new StateData();
                        obj.StateID = !String.IsNullOrEmpty(item.id) ? Convert.ToInt32(item.id) : 0;
                        obj.name = item.name;
                        statedateobj.Add(obj);
                    }
                }
                catch { }
                ViewBag.StateListCount = statedate.Count;
            // Rev 1.0
            }
            else
            {
                statedate = (List<StateData>)TempData["statedate"];
                statedateobj = (List<StateData>)TempData["statedateobj"];

                ViewBag.StateListCount = statedate.Count;
            }
            // End of Rev 1.0

            return PartialView(statedateobj);
        }
        public ActionResult DashboardBranchComboboxFV(string stateid)
        {

            FSMDashBoardFilter dashboard = new FSMDashBoardFilter();
            string userid = Session["userid"].ToString();
            int chkState = 0;
            if (stateid == null)
            {
                chkState = 1;
            }

            List<BranchData> branchdate = new List<BranchData>();
            List<BranchData> branchdateobj = new List<BranchData>();

            string stateIds = dashboard.StateId;
            try
            {
                BranchData obj = null;
                if (stateid == null)
                {
                    stateid = "";
                }

                branchdate = dashboard.GetBranchList(Convert.ToInt32(userid), stateid);
                foreach (var item in branchdate)
                {
                    obj = new BranchData();
                    obj.BranchID = !String.IsNullOrEmpty(Convert.ToString(item.BranchID)) ? Convert.ToInt32(item.BranchID) : 0;
                    obj.name = item.name;
                    branchdateobj.Add(obj);
                }
            }
            catch { }
            ViewBag.BranchListCount = branchdate.Count;
            // Rev 1.0
            TempData["branchdate"] = branchdate;
            TempData["branchdateobj"] = branchdateobj;
            // End of Rev 1.0

            if (chkState == 1)
            {
                return PartialView("DashboardBranchComboboxFV", branchdateobj);
            }
            else
            {
                Session["PageloadChk"] = "0";
                Session["BranchList"] = branchdateobj;
                return Json(branchdate, JsonRequestBehavior.AllowGet);
            }
        }
        // Mantis Issue 24729
        public ActionResult DashboardBranchCombobox(string stateid)
        {
            FSMDashBoardFilter dashboard = new FSMDashBoardFilter();
            string userid = Session["userid"].ToString();
            int chkState = 0;
            if (stateid == null)
            {
                chkState = 1;
            }
            List<BranchData> branchdate = new List<BranchData>();
            List<BranchData> branchdateobj = new List<BranchData>();

            // Rev 1.0
            if (TempData["branchdateobj"] == null)
            {
            // End of Rev 1.0
                string stateIds = dashboard.StateId;
                try
                {
                    BranchData obj = null;
                    if (stateid == null)
                    {
                        stateid = "";
                    }

                    branchdate = dashboard.GetBranchList(Convert.ToInt32(userid), stateid);

                    foreach (var item in branchdate)
                    {
                        obj = new BranchData();
                        obj.BranchID = !String.IsNullOrEmpty(Convert.ToString(item.BranchID)) ? Convert.ToInt32(item.BranchID) : 0;
                        obj.name = item.name;
                        branchdateobj.Add(obj);
                    }
                }
                catch { }
                ViewBag.BranchListCount = branchdate.Count;
            // Rev 1.0
            }
            else
            {
                branchdate = (List<BranchData>)TempData["branchdate"];
                branchdateobj = (List<BranchData>)TempData["branchdateobj"];

                ViewBag.BranchListCount = branchdate.Count;
            }
            // End of Rev 1.0

            if (chkState == 1)
            {
                return PartialView("DashboardBranchCombobox", branchdateobj);
            }
            else
            {
                Session["PageloadChk"] = "0";
                Session["BranchList"] = branchdateobj;
                return Json(branchdate, JsonRequestBehavior.AllowGet);
            }
        }

        // End of Mantis Issue 24729
        // bRANCH TV
        public ActionResult DashboardBranchComboboxTV(string stateid)
        {

            FSMDashBoardFilter dashboard = new FSMDashBoardFilter();
            string userid = Session["userid"].ToString();
            int chkState = 0;
            if (stateid == null)
            {
                chkState = 1;
            }

            List<BranchData> branchdate = new List<BranchData>();
            List<BranchData> branchdateobj = new List<BranchData>();

            // Rev 1.0
            if (TempData["branchdateobj"] == null) { 
            // End of Rev 1.0
                string stateIds = dashboard.StateId;
                try
                {
                    BranchData obj = null;
                    if (stateid == null)
                    {
                        stateid = "";
                    }

                    // Rev 1.0
                    //branchdate = dashboard.GetBranchList(Convert.ToInt32(userid), stateid);
                    if (TempData["branchdate"] != null)
                    {
                        branchdate = (List<BranchData>)TempData["branchdate"];
                    }
                    else
                    {
                        branchdate = dashboard.GetBranchList(Convert.ToInt32(userid), stateid);
                    }
                    // End of Rev 1.0

                    foreach (var item in branchdate)
                    {
                        obj = new BranchData();
                        obj.BranchID = !String.IsNullOrEmpty(Convert.ToString(item.BranchID)) ? Convert.ToInt32(item.BranchID) : 0;
                        obj.name = item.name;
                        branchdateobj.Add(obj);
                    }
                }
                catch { }
                ViewBag.BranchListCount = branchdate.Count;
            // Rev 1.0
            }
            else
            {
                branchdate = (List<BranchData>) TempData["branchdate"];
                branchdateobj = (List<BranchData>) TempData["branchdateobj"];

                ViewBag.BranchListCount = branchdate.Count;
            }
            // End of Rev 1.0

            if (chkState == 1)
            {
                return PartialView("DashboardBranchComboboxTV", branchdateobj);
            }
            else
            {
                Session["PageloadChk"] = "0";
                Session["BranchList"] = branchdateobj;
                return Json(branchdate, JsonRequestBehavior.AllowGet);
            }
        }

        
        //
        public ActionResult DashboardStateComboboxTV()
        {
            FSMDashBoardFilter dashboard = new FSMDashBoardFilter();
            string userid = Session["userid"].ToString();
            //string userid = "0";
            List<StateData> statedate = new List<StateData>();
            List<StateData> statedateobj = new List<StateData>();

            // Rev 1.0
            if(TempData["statedateobj"] == null)
            {
            // End of Rev 1.0
                try
                {
                    StateData obj = null;
                    statedate = dashboard.GetStateList(Convert.ToInt32(userid));
                    foreach (var item in statedate)
                    {
                        obj = new StateData();
                        obj.StateID = !String.IsNullOrEmpty(item.id) ? Convert.ToInt32(item.id) : 0;
                        obj.name = item.name;
                        statedateobj.Add(obj);
                    }
                }
                catch { }
                ViewBag.StateListCount = statedate.Count;
            
            // Rev 1.0
            }
            else
            {
                statedate = (List<StateData>) TempData["statedate"];
                statedateobj = (List<StateData>) TempData["statedateobj"];

                ViewBag.StateListCount = statedate.Count;
            }
            // End of Rev 1.0
            return PartialView(statedateobj);
        }
        public ActionResult DashboardAttendance(List<DashboardSettingMapped> list)
        {

            return PartialView(list);
        }
        public ActionResult DashboardAttendanceFV(List<DashboardSettingMapped> list)
        {

            return PartialView(list);
        }

        public ActionResult DashboardAttendanceTV(List<DashboardSettingMapped> list)
        {

            return PartialView(list);
        }
        public ActionResult leaveListView(List<DashboardSettingMapped> list)
        {

            return PartialView(list);
        }

        // Rev Sanchita
        public ActionResult DashboardStateComboboxOrder()
        {
            FSMDashBoardFilter dashboard = new FSMDashBoardFilter();
            string userid = Session["userid"].ToString();
            //string userid = "0";
            List<StateData> statedate = new List<StateData>();
            List<StateData> statedateobj = new List<StateData>();

            if (TempData["statedateobj"] == null)
            {
                try
                {
                    StateData obj = null;
                    statedate = dashboard.GetStateList(Convert.ToInt32(userid));
                    foreach (var item in statedate)
                    {
                        obj = new StateData();
                        obj.StateID = !String.IsNullOrEmpty(item.id) ? Convert.ToInt32(item.id) : 0;
                        obj.name = item.name;
                        statedateobj.Add(obj);
                    }
                }
                catch { }
                ViewBag.StateListCount = statedate.Count;

            }
            else
            {
                statedate = (List<StateData>)TempData["statedate"];
                statedateobj = (List<StateData>)TempData["statedateobj"];

                ViewBag.StateListCount = statedate.Count;
            }
            return PartialView(statedateobj);
        }

        public ActionResult DashboardBranchComboboxOrder(string stateid)
        {

            FSMDashBoardFilter dashboard = new FSMDashBoardFilter();
            string userid = Session["userid"].ToString();
            int chkState = 0;
            if (stateid == null)
            {
                chkState = 1;
            }

            List<BranchData> branchdate = new List<BranchData>();
            List<BranchData> branchdateobj = new List<BranchData>();

            if (TempData["branchdateobj"] == null)
            {
                string stateIds = dashboard.StateId;
                try
                {
                    BranchData obj = null;
                    if (stateid == null)
                    {
                        stateid = "";
                    }

                    if (TempData["branchdate"] != null)
                    {
                        branchdate = (List<BranchData>)TempData["branchdate"];
                    }
                    else
                    {
                        branchdate = dashboard.GetBranchList(Convert.ToInt32(userid), stateid);
                    }
            
                    foreach (var item in branchdate)
                    {
                        obj = new BranchData();
                        obj.BranchID = !String.IsNullOrEmpty(Convert.ToString(item.BranchID)) ? Convert.ToInt32(item.BranchID) : 0;
                        obj.name = item.name;
                        branchdateobj.Add(obj);
                    }
                }
                catch { }
                ViewBag.BranchListCount = branchdate.Count;

            }
            else
            {
                branchdate = (List<BranchData>)TempData["branchdate"];
                branchdateobj = (List<BranchData>)TempData["branchdateobj"];

                ViewBag.BranchListCount = branchdate.Count;
            }
       
            if (chkState == 1)
            {
                return PartialView("DashboardBranchComboboxOrder", branchdateobj);
            }
            else
            {
                Session["PageloadChk"] = "0";
                Session["BranchList"] = branchdateobj;
                return Json(branchdate, JsonRequestBehavior.AllowGet);
            }
        }
        // End of Rev Sanchita

        public ActionResult CheckDashboardDataExists()
        {
            ViewData["ExportDashboardSummaryGridList"] = TempData["ExportDashboardSummaryGridList"];
            TempData.Keep();
            if (ViewData["ExportDashboardSummaryGridList"] != null)
            {
                return Json("Success");
            }
            else
            {
                return Json("failure");
            }

        }
        public ActionResult ExportDashboardSummaryGridView(int type, String Name)
        {
            ViewData["ExportDashboardSummaryGridList"] = TempData["ExportDashboardSummaryGridList"];
            TempData.Keep();

            if (ViewData["ExportDashboardSummaryGridList"] != null)
            {
                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(ViewData["ExportDashboardSummaryGridList"], Name), ViewData["ExportDashboardSummaryGridList"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(ViewData["ExportDashboardSummaryGridList"], Name), ViewData["ExportDashboardSummaryGridList"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(ViewData["ExportDashboardSummaryGridList"], Name), ViewData["ExportDashboardSummaryGridList"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(ViewData["ExportDashboardSummaryGridList"], Name), ViewData["ExportDashboardSummaryGridList"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(ViewData["ExportDashboardSummaryGridList"], Name), ViewData["ExportDashboardSummaryGridList"]);
                    default:
                        break;
                }
                return Json("success");
            }
            return null;
        }

        private GridViewSettings GetEmployeeBatchGridViewSettings(object datatable, String Name)
        {
            var settings = new GridViewSettings();
            settings.Name = Name;
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = Name;

            DataTable dt = (DataTable)datatable;

            foreach (System.Data.DataColumn datacolumn in dt.Columns)
            {
                settings.Columns.Add(column =>
                {
                    column.Caption = datacolumn.ColumnName;
                    column.FieldName = datacolumn.ColumnName;
                    if (datacolumn.DataType.FullName == "System.Decimal" || datacolumn.DataType.FullName == "System.Int32" || datacolumn.DataType.FullName == "System.Int64")
                    {
                        column.PropertiesEdit.DisplayFormatString = "0.00";
                    }

                });


            }

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

        public ActionResult ExportDashboardGridViewSalesmanDetail(int type, String Name)
        {
            ViewData["DashboardGridViewSalesmanDetail"] = TempData["DashboardGridViewSalesmanDetail"];
            //ViewData["DashboardGridViewSalesmanDetailType"] = TempData["DashboardGridViewSalesmanDetailType"];
            TempData.Keep();

            if (ViewData["DashboardGridViewSalesmanDetail"] != null)
            {
                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetDashboardGridViewSalesman(ViewData["DashboardGridViewSalesmanDetail"], Name), ViewData["DashboardGridViewSalesmanDetail"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetDashboardGridViewSalesman(ViewData["DashboardGridViewSalesmanDetail"], Name), ViewData["DashboardGridViewSalesmanDetail"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetDashboardGridViewSalesman(ViewData["DashboardGridViewSalesmanDetail"], Name), ViewData["DashboardGridViewSalesmanDetail"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetDashboardGridViewSalesman(ViewData["DashboardGridViewSalesmanDetail"], Name), ViewData["DashboardGridViewSalesmanDetail"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetDashboardGridViewSalesman(ViewData["DashboardGridViewSalesmanDetail"], Name), ViewData["DashboardGridViewSalesmanDetail"]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetDashboardGridViewSalesman(object datatable, String Name)
        {
            var settings = new GridViewSettings();
            settings.Name = Name;
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = Name;
            String ID = Convert.ToString(TempData["DashboardGridViewSalesmanDetailType"]);
            TempData.Keep();
            DataTable dt = (DataTable)datatable;

            foreach (System.Data.DataColumn datacolumn in dt.Columns)
            {
                if (datacolumn.ColumnName != "EMPID" && datacolumn.ColumnName != "DEGID")
                {
                    settings.Columns.Add(column =>
                    {
                        column.Caption = datacolumn.ColumnName;
                        column.FieldName = datacolumn.ColumnName;
                        if (datacolumn.DataType.FullName == "System.Decimal" || datacolumn.DataType.FullName == "System.Int32" || datacolumn.DataType.FullName == "System.Int64")
                        {
                            column.PropertiesEdit.DisplayFormatString = "0.00";
                        }
                    });
                }

            }

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }



        public ActionResult ExportDashboardGridView(int type, String Name)
        {
            //Rev Tanmoy get data through execute query
            DataTable dbDashboardData = new DataTable();
            DBEngine objdb = new DBEngine();
            String query = TempData["DashboardGridView"].ToString();
            dbDashboardData = objdb.GetDataTable(query);

            ViewData["DashboardGridView"] = dbDashboardData;

            //End Rev
            TempData.Keep();

            if (ViewData["DashboardGridView"] != null)
            {

                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetDashboardGridView(ViewData["DashboardGridView"], Name), ViewData["DashboardGridView"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetDashboardGridView(ViewData["DashboardGridView"], Name), ViewData["DashboardGridView"]);
                    // return GridViewExtension.ExportToXlsx(GetDashboardGridView(ViewData["DashboardGridView"]), dbDashboardData);Replace ViewData To datatable
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetDashboardGridView(ViewData["DashboardGridView"], Name), ViewData["DashboardGridView"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetDashboardGridView(ViewData["DashboardGridView"], Name), ViewData["DashboardGridView"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetDashboardGridView(ViewData["DashboardGridView"], Name), ViewData["DashboardGridView"]);
                    default:
                        break;
                }
            }
            return null;
        }


        public ActionResult ExportDashboardGridViewTV(int type, String Name)
        {
            //Rev Tanmoy get data through execute query
            DataTable dbDashboardData = new DataTable();
            DBEngine objdb = new DBEngine();
            String query = TempData["DashboardGridViewTeam"].ToString();
            dbDashboardData = objdb.GetDataTable(query);

            ViewData["DashboardGridViewTeam"] = dbDashboardData;

            //End Rev
            TempData.Keep();

            if (ViewData["DashboardGridViewTeam"] != null)
            {

                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetDashboardGridView(ViewData["DashboardGridViewTeam"], Name), ViewData["DashboardGridViewTeam"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetDashboardGridView(ViewData["DashboardGridViewTeam"], Name), ViewData["DashboardGridViewTeam"]);
                    // return GridViewExtension.ExportToXlsx(GetDashboardGridView(ViewData["DashboardGridView"]), dbDashboardData);Replace ViewData To datatable
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetDashboardGridView(ViewData["DashboardGridViewTeam"], Name), ViewData["DashboardGridViewTeam"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetDashboardGridView(ViewData["DashboardGridViewTeam"], Name), ViewData["DashboardGridViewTeam"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetDashboardGridView(ViewData["DashboardGridViewTeam"], Name), ViewData["DashboardGridViewTeam"]);
                    default:
                        break;
                }
            }
            return null;
        }

        public ActionResult ExportDashboardGridViewFV(int type, String Name)
        {
            //Rev Tanmoy get data through execute query
            DataTable dbDashboardData = new DataTable();
            DBEngine objdb = new DBEngine();
            String query = TempData["DashboardGridViewFV"].ToString();
            dbDashboardData = objdb.GetDataTable(query);

            ViewData["DashboardGridViewFV"] = dbDashboardData;

            //End Rev
            TempData.Keep();

            if (ViewData["DashboardGridViewFV"] != null)
            {

                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetDashboardGridView(ViewData["DashboardGridViewFV"], Name), ViewData["DashboardGridViewFV"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetDashboardGridView(ViewData["DashboardGridViewFV"], Name), ViewData["DashboardGridViewFV"]);
                    // return GridViewExtension.ExportToXlsx(GetDashboardGridView(ViewData["DashboardGridView"]), dbDashboardData);Replace ViewData To datatable
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetDashboardGridView(ViewData["DashboardGridViewFV"], Name), ViewData["DashboardGridViewFV"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetDashboardGridView(ViewData["DashboardGridViewFV"], Name), ViewData["DashboardGridViewFV"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetDashboardGridView(ViewData["DashboardGridViewFV"], Name), ViewData["DashboardGridViewFV"]);
                    default:
                        break;
                }
            }
            return null;
        }
        private GridViewSettings GetDashboardGridView(object datatable, String Name)
        {
            var settings = new GridViewSettings();
            //settings.Name = "DashboardGridView";
            settings.Name = Name;
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            // settings.SettingsExport.FileName = "DashboardGridView";
            settings.SettingsExport.FileName = Name;
            String ID = Convert.ToString(TempData["DashboardGridView"]);
            TempData.Keep();
            DataTable dt = (DataTable)datatable;

            foreach (System.Data.DataColumn datacolumn in dt.Columns)
            {
                if (datacolumn.ColumnName != "EMPID")
                {
                    settings.Columns.Add(column =>
                    {
                        column.Caption = datacolumn.ColumnName;
                        column.FieldName = datacolumn.ColumnName;
                        if (datacolumn.DataType.FullName == "System.Decimal" || datacolumn.DataType.FullName == "System.Int32" || datacolumn.DataType.FullName == "System.Int64")
                        {
                            if (datacolumn.ColumnName != "Shops Visited")
                            {
                                column.PropertiesEdit.DisplayFormatString = "0.00";
                            }
                        }
                    });
                }

            }

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

        public ActionResult ExportDashboardGridViewDetails(int type, String Headedtext)
        {
            ViewData["DashboardGridViewDetails"] = TempData["DashboardGridViewDetails"];

            TempData.Keep();

            if (ViewData["DashboardGridViewDetails"] != null)
            {

                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetDashboardGridViewDetails(ViewData["DashboardGridViewDetails"], Headedtext), ViewData["DashboardGridViewDetails"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetDashboardGridViewDetails(ViewData["DashboardGridViewDetails"], Headedtext), ViewData["DashboardGridViewDetails"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetDashboardGridViewDetails(ViewData["DashboardGridViewDetails"], Headedtext), ViewData["DashboardGridViewDetails"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetDashboardGridViewDetails(ViewData["DashboardGridViewDetails"], Headedtext), ViewData["DashboardGridViewDetails"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetDashboardGridViewDetails(ViewData["DashboardGridViewDetails"], Headedtext), ViewData["DashboardGridViewDetails"]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetDashboardGridViewDetails(object datatable, String Headedtext)
        {
            var settings = new GridViewSettings();
            settings.Name = Headedtext;
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = Headedtext;
            String ID = Convert.ToString(TempData["DashboardGridViewDetails"]);
            TempData.Keep();
            DataTable dt = (DataTable)datatable;

            foreach (System.Data.DataColumn datacolumn in dt.Columns)
            {
                if (datacolumn.ColumnName != "shopvisit_image" && datacolumn.ColumnName != "Image")
                {
                    settings.Columns.Add(column =>
                    {
                        column.Caption = datacolumn.ColumnName;
                        column.FieldName = datacolumn.ColumnName;
                        if (datacolumn.DataType.FullName == "System.Decimal" || datacolumn.DataType.FullName == "System.Int32" || datacolumn.DataType.FullName == "System.Int64")
                        {
                            column.PropertiesEdit.DisplayFormatString = "0.00";
                        }
                    });
                }

            }

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }


        public ActionResult attendanceDetails()
        {

            return PartialView();
        }
        public ActionResult orderAnalytics()
        {

            return PartialView();
        }
        public ActionResult TargetAchievementAnalytics()
        {
            DateTime current = DateTime.Now;
            DateTime last1 = DateTime.Now.AddMonths(-1);
            DateTime last2 = DateTime.Now.AddMonths(-2);
            Dictionary<string, string> DateList = new Dictionary<string, string>();

            DateList.Add(current.ToString("yyyy-MM-dd"), current.ToString("MMMM") + "," + current.Year);
            DateList.Add(last1.ToString("yyyy-MM-dd"), last1.ToString("MMMM") + "," + last1.Year);
            DateList.Add(last2.ToString("yyyy-MM-dd"), last2.ToString("MMMM") + "," + last2.Year);

            ViewBag.DateList = DateList;
            return PartialView();
        }
        public ActionResult reimbursementAnalytics()
        {

            return PartialView();
        }
        #region Start Salesman/Distribution grid

        public PartialViewResult DashboardGridDistributor()
        {
            DataTable dbDashboardData = new DataTable();
            dbDashboardData.Columns.Add("State", typeof(String));
            dbDashboardData.Columns.Add("Distributor Name", typeof(String));
            dbDashboardData.Columns.Add("Total Order(s)", typeof(Decimal));
            dbDashboardData.Columns.Add("Pending Order", typeof(Decimal));
            dbDashboardData.Columns.Add("Invoiced", typeof(Decimal));
            dbDashboardData.Columns.Add("Invoiced Value(INR)", typeof(Decimal));
            dbDashboardData.Columns.Add("Received(INR)", typeof(Decimal));
            dbDashboardData.Columns.Add("Balance Amt.(INR)", typeof(Decimal));


            dbDashboardData.Rows.Add("West Bengal", "Mohan Enterprise", 10, 4, 6, 4300.00, 4300.00, 0.00);
            dbDashboardData.Rows.Add("West Bengal", "Prabhat Stores", 7, 6, 1, 8720.00, 5000.00, 3720.00);
            dbDashboardData.Rows.Add("West Bengal", "Indro Stores", 6, 5, 1, 9600.00, 600.00, 9000.00);
            dbDashboardData.Rows.Add("Assam", "Goutam Enterprise", 3, 2, 1, 2540.00, 1540.00, 1000.00);
            dbDashboardData.Rows.Add("Maharashtra", "Ganesh Enterprise", 8, 6, 2, 4500.00, 4500.00, 0.00);
            dbDashboardData.Rows.Add("Maharashtra", "Pixel Enterprise", 2, 2, 0, 0.00, 0.00, 0.00);
            dbDashboardData.Rows.Add("Assam", "Param Enterprise", 8, 5, 3, 2580.00, 580.00, 2000.00);
            dbDashboardData.Rows.Add("Assam", "Chinmoy Enterprise", 11, 11, 0, 0, 0, 0.00);
            dbDashboardData.Rows.Add("Maharashtra", "Arindam Stores", 26, 0, 26, 7800.00, 2400.00, 5400.00);
            dbDashboardData.Rows.Add("Rajasthan", "Ratan Enterprise", 1, 1, 0, 0, 0, 0.00);
            dbDashboardData.Rows.Add("Rajasthan", "Bhandari Enterprise", 7, 3, 4, 2800.00, 2500.00, 300.00);
            dbDashboardData.Rows.Add("Rajasthan", "Suman Enterprise", 10, 9, 1, 9950.00, 9950.00, 0.00);





            return PartialView(dbDashboardData);
        }

        public PartialViewResult DashboardGridSalesMan()
        {
            DataTable dbDashboardData = new DataTable();
            dbDashboardData.Columns.Add("State", typeof(String));
            dbDashboardData.Columns.Add("Salesman", typeof(String));
            dbDashboardData.Columns.Add("Total Order(s)", typeof(Decimal));
            dbDashboardData.Columns.Add("Pending Order", typeof(Decimal));
            dbDashboardData.Columns.Add("Invoiced", typeof(Decimal));
            dbDashboardData.Columns.Add("Invoiced Value(INR)", typeof(Decimal));
            dbDashboardData.Columns.Add("Received(INR)", typeof(Decimal));
            dbDashboardData.Columns.Add("Balance Amt.(INR)", typeof(Decimal));

            dbDashboardData.Rows.Add("West Bengal", "CHIRADEEP MUKHERJEE", 10, 6, 4, 4300, 4000.00, 300.00);
            dbDashboardData.Rows.Add("West Bengal", "MANIKA SAHA", 7, 4, 3, 8720.00, 5300.00, 3420.00);
            dbDashboardData.Rows.Add("West Bengal", "RASHID AHMED", 6, 1, 5, 9600.00, 1600.00, 8000.00);
            dbDashboardData.Rows.Add("Assam", "AJAY KUMAR SHAW", 3, 3, 0, 2540.00, 540.00, 2000.00);
            dbDashboardData.Rows.Add("Maharashtra", "SHIV KUMAR YADAV", 8, 8, 0, 4500.00, 2500.00, 2000.00);
            dbDashboardData.Rows.Add("Maharashtra", "CHANDRADIP ROY", 2, 2, 0, 0.00, 0.00, 0.00);
            dbDashboardData.Rows.Add("Assam", "CHINMOY MAITI", 8, 6, 2, 2580.00, 2580.00, 0.00);
            dbDashboardData.Rows.Add("Assam", "SUMAN ROY", 11, 10, 1, 0, 0, 0.00);
            dbDashboardData.Rows.Add("Maharashtra", "ARINDAM GHOSHAL", 26, 1, 25, 7800.00, 700.00, 7100.00);
            dbDashboardData.Rows.Add("Rajasthan", "SUDIP KUMAR PAL", 1, 1, 0, 0, 0, 0.00);
            dbDashboardData.Rows.Add("Rajasthan", "GOUTAM DAS", 7, 3, 4, 2800.00, 2800.00, 00.00);
            dbDashboardData.Rows.Add("Rajasthan", "DEBASHISH TALUKDAR", 10, 9, 1, 9950.00, 9950.00, 0.00);

            return PartialView(dbDashboardData);
        }

        #endregion

        public JsonResult GetAttendanceResults()
        {


            Dashboard dashboarddataobj = new Dashboard();
            attendanceResult Dashboarddata = new attendanceResult();
            try
            {
                DataSet objData = dashboarddataobj.GetAttendanceDashboard("", "", "", "GETBOXDATA", Session["userid"].ToString());
                //DataTable objData = dashboarddataobj.GetDashboardAttendanceData();

                if (objData != null && objData.Tables[0] != null && objData.Tables[0].Rows.Count > 0)
                {
                    Dashboarddata.TOTAL_ABSENT = Convert.ToString(objData.Tables[0].Rows[0]["TOTAL_ABSENT"]);
                    Dashboarddata.TOTAL_EMPLOYEE = Convert.ToString(objData.Tables[0].Rows[0]["TOTAL_EMPLOYEE"]);
                    Dashboarddata.TOTAL_ONTIME = Convert.ToString(objData.Tables[0].Rows[0]["TOTAL_ONTIME"]);
                    Dashboarddata.TOTAL_ONTIME_PERCENTAGE = Convert.ToString(objData.Tables[0].Rows[0]["TOTAL_ONTIME_PERCENTAGE"]);
                    Dashboarddata.TOTAL_LATETODAY = Convert.ToString(objData.Tables[0].Rows[0]["TOTAL_LATETODAY"]);
                }

            }
            catch
            {

            }

            return Json(Dashboarddata);


        }

        public JsonResult GetMonthlyAttendance()
        {


            Dashboard dashboarddataobj = new Dashboard();
            List<monthlyAttResult> Dashboarddata = new List<monthlyAttResult>();
            try
            {
                DataSet objData = dashboarddataobj.GetAttendanceDashboard("", "", "", "GETATTNSUMMARY", Session["userid"].ToString());
                //DataTable objData = dashboarddataobj.GetDashboardAttendanceData();

                if (objData != null && objData.Tables[0] != null && objData.Tables[0].Rows.Count > 0)
                {
                    Dashboarddata = (from DataRow dr in objData.Tables[0].Rows
                                     select new monthlyAttResult()
                                {

                                    MONTH = Convert.ToString(dr["month_name"]),
                                    EMP_COUNT = Convert.ToString(dr["cnt"]),
                                    ord = Convert.ToString(dr["ord"])
                                }).ToList();
                }

            }
            catch
            {

            }

            return Json(Dashboarddata);


        }


        public JsonResult GetUserData()
        {


            Dashboard dashboarddataobj = new Dashboard();
            List<userClass> Userdata = new List<userClass>();
            try
            {
                DataSet objData = dashboarddataobj.GetAttendanceDashboard("", "", "", "GETUSER", Session["userid"].ToString());
                //DataTable objData = dashboarddataobj.GetDashboardAttendanceData();

                if (objData != null && objData.Tables[0] != null && objData.Tables[0].Rows.Count > 0)
                {
                    Userdata = (from DataRow dr in objData.Tables[0].Rows
                                select new userClass()
                       {

                           NAME = Convert.ToString(dr["NAME"]),
                           USER_ID = Convert.ToString(dr["USER_ID"])
                       }).ToList();
                }

            }
            catch
            {

            }

            return Json(Userdata);


        }

        public JsonResult GetCalenderData(string userId, string Month, string Year)
        {


            Dashboard dashboarddataobj = new Dashboard();
            List<calenderClass> caldata = new List<calenderClass>();
            try
            {
                DataSet objData = dashboarddataobj.GetAttendanceDashboard(userId, Month, Year, "GETCALENDERDATA", Session["userid"].ToString());
                if (objData != null && objData.Tables[0] != null && objData.Tables[0].Rows.Count > 0)
                {
                    caldata = (from DataRow dr in objData.Tables[0].Rows
                               select new calenderClass()
                               {

                                   name = Convert.ToString(dr["name"]),
                                   date = Convert.ToString(dr["date"])
                               }).ToList();
                }

            }
            catch
            {

            }

            return Json(caldata);


        }


        public JsonResult GetLeaveData()
        {


            Dashboard dashboarddataobj = new Dashboard();
            List<leaveClass> LeaveData = new List<leaveClass>();
            try
            {
                DataSet objData = dashboarddataobj.GetAttendanceDashboard("", "", "", "GETRECENTLEAVE", Session["userid"].ToString());
                //DataTable objData = dashboarddataobj.GetDashboardAttendanceData();

                if (objData != null && objData.Tables[0] != null && objData.Tables[0].Rows.Count > 0)
                {
                    LeaveData = (from DataRow dr in objData.Tables[0].Rows
                                 select new leaveClass()
                                {

                                    name = Convert.ToString(dr["NAME"]),
                                    LeaveStartdate = Convert.ToString(dr["LEAVE_START_DATE"]),
                                    LeaveEnddate = Convert.ToString(dr["LEAVE_END_DATE"]),
                                    LEAVETYPE = Convert.ToString(dr["LEAVETYPE"]),
                                    LEAVE_REASON = Convert.ToString(dr["LEAVE_REASON"]),
                                    CURRENT_STATUS = Convert.ToString(dr["CURRENT_STATUS"])

                                }).ToList();
                }

            }
            catch
            {

            }

            return Json(LeaveData);


        }

        // Rev 2.0
        //public JsonResult GetOrderAnalyticTotalOrderCount(string fromDate, string toDate)
        public JsonResult GetOrderAnalyticTotalOrderCount(string fromDate, string toDate, string stateid, string branchid)
            // End of Rev 2.0
        {


            Dashboard dashboarddataobj = new Dashboard();
            List<TotalOrderCount> caldata = new List<TotalOrderCount>();
            try
            {
                // Rev 2.0
                //DataSet objData = dashboarddataobj.GetOrderAnalytics(fromDate, toDate, "ORDCNT", Session["userid"].ToString());
                DataSet objData = dashboarddataobj.GetOrderAnalytics(fromDate, toDate, "ORDCNT", stateid, branchid, Session["userid"].ToString());
                // End of Rev 2.0
                if (objData != null && objData.Tables[0] != null && objData.Tables[0].Rows.Count > 0)
                {
                    caldata = (from DataRow dr in objData.Tables[0].Rows
                               select new TotalOrderCount()
                               {

                                   ORDCNT = Convert.ToString(dr["ORDCNT"])
                               }).ToList();
                }
            }
            catch
            {

            }
            return Json(caldata);
        }

        // Rev 2.0 [parameters , string stateid, string branchid added]
        public JsonResult GetOrderAnalyticTotalOrderValue(string fromDate, string toDate, string stateid, string branchid)
        {

            Dashboard dashboarddataobj = new Dashboard();
            List<TotalOrderValue> caldata = new List<TotalOrderValue>();
            try
            {
                // Rev 2.0 [parameters , stateid, branchid added]
                DataSet objData = dashboarddataobj.GetOrderAnalytics(fromDate, toDate, "ORDVALUE", stateid, branchid, Session["userid"].ToString());
                if (objData != null && objData.Tables[0] != null && objData.Tables[0].Rows.Count > 0)
                {
                    caldata = (from DataRow dr in objData.Tables[0].Rows
                               select new TotalOrderValue()
                               {

                                   ORDVALUE = Convert.ToString(dr["ORDVALUE"])
                               }).ToList();
                }
            }
            catch
            {

            }
            return Json(caldata);
        }
        // Rev 2.0 [parameters , string stateid, string branchid added]
        public JsonResult GetOrderAnalyticAvgOrderValue(string fromDate, string toDate, string stateid, string branchid)
        {


            Dashboard dashboarddataobj = new Dashboard();
            List<AvgOrderValue> caldata = new List<AvgOrderValue>();
            try
            {
                // Rev 2.0 [ parameter , stateid, branchid added]
                DataSet objData = dashboarddataobj.GetOrderAnalytics(fromDate, toDate, "AVGORDVALUE", stateid, branchid, Session["userid"].ToString());
                if (objData != null && objData.Tables[0] != null && objData.Tables[0].Rows.Count > 0)
                {
                    caldata = (from DataRow dr in objData.Tables[0].Rows
                               select new AvgOrderValue()
                               {

                                   AVGORDVALUE = Convert.ToString(dr["AVGORDVALUE"])
                               }).ToList();
                }
            }
            catch
            {

            }
            return Json(caldata);
        }

        // Rev 2.0 [parameters , string stateid, string branchid added]
        public JsonResult GetOrderAnalyticOrderDelivered(string fromDate, string toDate, string stateid, string branchid)
        {


            Dashboard dashboarddataobj = new Dashboard();
            List<OrderDelivered> caldata = new List<OrderDelivered>();
            try
            {
                // Rev 2.0 [ parameter , stateid, branchid added]
                DataSet objData = dashboarddataobj.GetOrderAnalytics(fromDate, toDate, "ORDDELV", stateid, branchid, Session["userid"].ToString());
                if (objData != null && objData.Tables[0] != null && objData.Tables[0].Rows.Count > 0)
                {
                    caldata = (from DataRow dr in objData.Tables[0].Rows
                               select new OrderDelivered()
                               {

                                   ORDDELV = Convert.ToString(dr["ORDDELV"])
                               }).ToList();
                }
            }
            catch
            {

            }
            return Json(caldata);
        }
        // order top 10 value
        // Rev 2.0 [parameters , string stateid, string branchid added]
        public JsonResult Gettop10orderValue(string fromDate, string toDate, string stateid, string branchid)
        {


            Dashboard dashboarddataobj = new Dashboard();
            List<OrderValueTopClass> caldata = new List<OrderValueTopClass>();
            try
            {
                // Rev 2.0 [ parameter , stateid, branchid added]
                DataSet objData = dashboarddataobj.GetOrderAnalytics(fromDate, toDate, "TOP10ITEMSVAL", stateid, branchid, Session["userid"].ToString());
                if (objData != null && objData.Tables[0] != null && objData.Tables[0].Rows.Count > 0)
                {
                    caldata = (from DataRow dr in objData.Tables[0].Rows
                               select new OrderValueTopClass()
                               {
                                   PRODUCT = Convert.ToString(dr["PRODUCT"]),
                                   ORDVALUE = Convert.ToString(dr["ORDVALUE"])
                               }).ToList();
                }
            }
            catch
            {

            }
            return Json(caldata);
        }
        // order quantity
        // Rev 2.0 [parameters , string stateid, string branchid added]
        public JsonResult Gettop10orderQuantity(string fromDate, string toDate, string stateid, string branchid)
        {


            Dashboard dashboarddataobj = new Dashboard();
            List<OrderQuantityTopClass> caldata = new List<OrderQuantityTopClass>();
            try
            {
                // Rev 2.0 [ parameter , stateid, branchid added]
                DataSet objData = dashboarddataobj.GetOrderAnalytics(fromDate, toDate, "TOP10ITEMSQTY", stateid, branchid, Session["userid"].ToString());
                if (objData != null && objData.Tables[0] != null && objData.Tables[0].Rows.Count > 0)
                {
                    caldata = (from DataRow dr in objData.Tables[0].Rows
                               select new OrderQuantityTopClass()
                               {
                                   PRODUCT = Convert.ToString(dr["PRODUCT"]),
                                   ORDQTY = Convert.ToString(dr["ORDQTY"])
                               }).ToList();
                }
            }
            catch
            {

            }
            return Json(caldata);
        }
        // top customers
        // Rev 2.0 [parameters , string stateid, string branchid added]
        public JsonResult GettopCustomers(string fromDate, string toDate, string stateid, string branchid)
        {


            Dashboard dashboarddataobj = new Dashboard();
            List<topCustomerClass> caldata = new List<topCustomerClass>();
            try
            {
                // Rev 2.0 [ parameter , stateid, branchid added]
                DataSet objData = dashboarddataobj.GetOrderAnalytics(fromDate, toDate, "TOP10CUSTVAL", stateid, branchid, Session["userid"].ToString());
                if (objData != null && objData.Tables[0] != null && objData.Tables[0].Rows.Count > 0)
                {
                    caldata = (from DataRow dr in objData.Tables[0].Rows
                               select new topCustomerClass()
                               {
                                   SHOPNAME = Convert.ToString(dr["SHOPNAME"]),
                                   ORDVALUE = Convert.ToString(dr["ORDVALUE"])
                               }).ToList();
                }
            }
            catch
            {

            }
            return Json(caldata);
        }

        // Rev 2.0 [parameters , string stateid, string branchid added]
        public JsonResult GettopOrdersStateWise(string fromDate, string toDate, string stateid, string branchid)
        {


            Dashboard dashboarddataobj = new Dashboard();
            List<topOrderStateClass> caldata = new List<topOrderStateClass>();
            try
            {
                // Rev 2.0 [ parameter , stateid, branchid added]
                DataSet objData = dashboarddataobj.GetOrderAnalytics(fromDate, toDate, "TOP10STVAL", stateid, branchid, Session["userid"].ToString());
                if (objData != null && objData.Tables[0] != null && objData.Tables[0].Rows.Count > 0)
                {
                    caldata = (from DataRow dr in objData.Tables[0].Rows
                               select new topOrderStateClass()
                               {
                                   STCODE = Convert.ToString(dr["STCODE"]),
                                   STATENAME = Convert.ToString(dr["STATENAME"]),
                                   ORDVALUE = Convert.ToString(dr["ORDVALUE"])
                               }).ToList();
                }
            }
            catch
            {

            }
            return Json(caldata);
        }

        // OrdercountChart
        // Rev 2.0 [parameters , string stateid, string branchid added]
        public JsonResult OrdercountChart(string fromDate, string toDate, string stateid, string branchid)
        {


            Dashboard dashboarddataobj = new Dashboard();
            List<orderCountChartClass> caldata = new List<orderCountChartClass>();
            try
            {
                // Rev 2.0 [ parameter , stateid, branchid added]
                DataSet objData = dashboarddataobj.GetOrderAnalytics(fromDate, toDate, "ORDCNTDATE", stateid, branchid, Session["userid"].ToString());
                if (objData != null && objData.Tables[0] != null && objData.Tables[0].Rows.Count > 0)
                {
                    caldata = (from DataRow dr in objData.Tables[0].Rows
                               select new orderCountChartClass()
                               {
                                   ORDERDATE = Convert.ToString(dr["ORDERDATE"]),
                                   ORDCNT = Convert.ToString(dr["ORDCNT"])
                               }).ToList();
                }
            }
            catch
            {

            }
            return Json(caldata);
        }
        //OrderTotalChart
        // Rev 2.0 [parameters , string stateid, string branchid added]
        public JsonResult OrderTotalChart(string fromDate, string toDate, string stateid, string branchid)
        {


            Dashboard dashboarddataobj = new Dashboard();
            List<orderTotalChartClass> caldata = new List<orderTotalChartClass>();
            try
            {
                // Rev 2.0 [ parameter , stateid, branchid added]
                DataSet objData = dashboarddataobj.GetOrderAnalytics(fromDate, toDate, "ORDVALDT", stateid, branchid, Session["userid"].ToString());
                if (objData != null && objData.Tables[0] != null && objData.Tables[0].Rows.Count > 0)
                {
                    caldata = (from DataRow dr in objData.Tables[0].Rows
                               select new orderTotalChartClass()
                               {
                                   ORDERDATE = Convert.ToString(dr["ORDERDATE"]),
                                   ORDVALUE = Convert.ToString(dr["ORDVALUE"])
                               }).ToList();
                }
            }
            catch
            {

            }
            return Json(caldata);
        }

        // Rev 2.0 [parameters , string stateid, string branchid added]
        public JsonResult OrderDeliveredChart(string fromDate, string toDate, string stateid, string branchid)
        {


            Dashboard dashboarddataobj = new Dashboard();
            List<orderDeliveredChartClass> caldata = new List<orderDeliveredChartClass>();
            try
            {
                // Rev 2.0 [ parameter , stateid, branchid added]
                DataSet objData = dashboarddataobj.GetOrderAnalytics(fromDate, toDate, "ORDVALDELV", stateid, branchid, Session["userid"].ToString());
                if (objData != null && objData.Tables[0] != null && objData.Tables[0].Rows.Count > 0)
                {
                    caldata = (from DataRow dr in objData.Tables[0].Rows
                               select new orderDeliveredChartClass()
                               {
                                   ORDERDATE = Convert.ToString(dr["ORDERDATE"]),
                                   BILLVALUE = Convert.ToString(dr["BILLVALUE"])
                               }).ToList();
                }
            }
            catch
            {

            }
            return Json(caldata);
        }

        public JsonResult TrgtAchiement()
        {


            Dashboard dashboarddataobj = new Dashboard();
            TgtVsAch objTgtVsAch = new TgtVsAch();
            List<TargetAchievementBoxData> caldata = new List<TargetAchievementBoxData>();
            List<TargetAchievementMonthData> cmonthdata = new List<TargetAchievementMonthData>();
            List<TargetAchievementEmployeeData> cemployeedata = new List<TargetAchievementEmployeeData>();

            try
            {
                DataSet objData = dashboarddataobj.GetTrgtboxData();
                if (objData != null && objData.Tables[0] != null && objData.Tables[0].Rows.Count > 0)
                {
                    caldata = (from DataRow dr in objData.Tables[0].Rows
                               select new TargetAchievementBoxData()
                               {
                                   target = Convert.ToString(dr["Target"]),
                                   Achievement = Convert.ToString(dr["Achievement"]),
                                   Stage = Convert.ToString(dr["Stage"])

                               }).ToList();
                }

                objTgtVsAch.boxdata = caldata;

                if (objData != null && objData.Tables[1] != null && objData.Tables[1].Rows.Count > 0)
                {
                    cmonthdata = (from DataRow dr in objData.Tables[1].Rows
                                  select new TargetAchievementMonthData()
                                  {
                                      MONTHYEAR = Convert.ToString(dr["MONTHYEAR"]),
                                      ENQ_TGT = Convert.ToString(dr["ENQ_TGT"]),
                                      ENQ_ACH = Convert.ToString(dr["ENQ_ACH"]),
                                      LEAD_TGT = Convert.ToString(dr["LEAD_TGT"]),
                                      LEAD_ACH = Convert.ToString(dr["LEAD_ACH"]),
                                      TD_TGT = Convert.ToString(dr["TD_TGT"]),
                                      TD_ACH = Convert.ToString(dr["TD_ACH"]),
                                      BOOKING_TGT = Convert.ToString(dr["BOOKING_TGT"]),
                                      BOOKING_ACH = Convert.ToString(dr["BOOKING_ACH"]),
                                      RT_TGT = Convert.ToString(dr["RT_TGT"]),
                                      RT_ACH = Convert.ToString(dr["RT_ACH"])
                                  }).ToList();
                }
                objTgtVsAch.monthwisedata = cmonthdata;

                if (objData != null && objData.Tables[2] != null && objData.Tables[2].Rows.Count > 0)
                {
                    cemployeedata = (from DataRow dr in objData.Tables[2].Rows
                                     select new TargetAchievementEmployeeData()
                                  {
                                      EMP_NAME = Convert.ToString(dr["EMP_NAME"]),
                                      DEPT = Convert.ToString(dr["DEPT"]),
                                      DESIG = Convert.ToString(dr["DESIG"]),
                                      ENQ_TGT = Convert.ToString(dr["ENQ_TGT"]),
                                      ENQ_ACH = Convert.ToString(dr["ENQ_ACH"]),
                                      LEAD_TGT = Convert.ToString(dr["LEAD_TGT"]),
                                      LEAD_ACH = Convert.ToString(dr["LEAD_ACH"]),
                                      TD_TGT = Convert.ToString(dr["TD_TGT"]),
                                      TD_ACH = Convert.ToString(dr["TD_ACH"]),
                                      BOOKING_TGT = Convert.ToString(dr["BOOKING_TGT"]),
                                      BOOKING_ACH = Convert.ToString(dr["BOOKING_ACH"]),
                                      RT_TGT = Convert.ToString(dr["RT_TGT"]),
                                      RT_ACH = Convert.ToString(dr["RT_ACH"])
                                  }).ToList();
                }
                objTgtVsAch.employeewisedata = cemployeedata;


            }
            catch
            {

            }
            return Json(objTgtVsAch);
        }

        public JsonResult RefreshMonthwiseSummary(string monthyear)
        {


            Dashboard dashboarddataobj = new Dashboard();
            TgtVsAch objTgtVsAch = new TgtVsAch();
            List<TargetAchievementMonthData> cmonthdata = new List<TargetAchievementMonthData>();


            try
            {
                DataSet objData = dashboarddataobj.GetTrgtMonthwiseData(monthyear);


                if (objData != null && objData.Tables[0] != null && objData.Tables[0].Rows.Count > 0)
                {
                    cmonthdata = (from DataRow dr in objData.Tables[0].Rows
                                  select new TargetAchievementMonthData()
                                  {
                                      MONTHYEAR = Convert.ToString(dr["MONTHYEAR"]),
                                      ENQ_TGT = Convert.ToString(dr["ENQ_TGT"]),
                                      ENQ_ACH = Convert.ToString(dr["ENQ_ACH"]),
                                      LEAD_TGT = Convert.ToString(dr["LEAD_TGT"]),
                                      LEAD_ACH = Convert.ToString(dr["LEAD_ACH"]),
                                      TD_TGT = Convert.ToString(dr["TD_TGT"]),
                                      TD_ACH = Convert.ToString(dr["TD_ACH"]),
                                      BOOKING_TGT = Convert.ToString(dr["BOOKING_TGT"]),
                                      BOOKING_ACH = Convert.ToString(dr["BOOKING_ACH"]),
                                      RT_TGT = Convert.ToString(dr["RT_TGT"]),
                                      RT_ACH = Convert.ToString(dr["RT_ACH"])
                                  }).ToList();
                }
                objTgtVsAch.monthwisedata = cmonthdata;



            }
            catch
            {

            }
            return Json(objTgtVsAch);
        }

        public JsonResult GetRemBox(string fromDate, string toDate)
        {


            Dashboard dashboarddataobj = new Dashboard();
            List<getboxClass> caldata = new List<getboxClass>();
            try
            {
                DataSet objData = dashboarddataobj.GetReimbursementAnalytics(fromDate, toDate, "BOXDATA", Session["userid"].ToString());
                if (objData != null && objData.Tables[0] != null && objData.Tables[0].Rows.Count > 0)
                {
                    caldata = (from DataRow dr in objData.Tables[0].Rows
                               select new getboxClass()
                               {
                                   total = Convert.ToString(dr["total"]),
                                   approved = Convert.ToString(dr["approved"]),
                                   rejected = Convert.ToString(dr["rejected"]),
                                   pending = Convert.ToString(dr["pending"])
                               }).ToList();
                }
            }
            catch
            {

            }
            return Json(caldata);
        }
        public JsonResult Getchart1(string fromDate, string toDate)
        {


            Dashboard dashboarddataobj = new Dashboard();
            List<remchart1Class> caldata = new List<remchart1Class>();
            try
            {
                DataSet objData = dashboarddataobj.GetReimbursementAnalytics(fromDate, toDate, "APPLIEDAMOUNTMONTH", Session["userid"].ToString());
                if (objData != null && objData.Tables[0] != null && objData.Tables[0].Rows.Count > 0)
                {
                    caldata = (from DataRow dr in objData.Tables[0].Rows
                               select new remchart1Class()
                               {
                                   Amount = Convert.ToString(dr["Amount"]),
                                   months = Convert.ToString(dr["months"])
                               }).ToList();
                }
            }
            catch
            {

            }
            return Json(caldata);
        }
        public JsonResult Getchart2(string fromDate, string toDate)
        {


            Dashboard dashboarddataobj = new Dashboard();
            List<remchart2Class> caldata = new List<remchart2Class>();
            try
            {
                DataSet objData = dashboarddataobj.GetReimbursementAnalytics(fromDate, toDate, "BYTYPE", Session["userid"].ToString());
                if (objData != null && objData.Tables[0] != null && objData.Tables[0].Rows.Count > 0)
                {
                    caldata = (from DataRow dr in objData.Tables[0].Rows
                               select new remchart2Class()
                               {
                                   Amount = Convert.ToString(dr["Amount"]),
                                   Expence_type = Convert.ToString(dr["Expence_type"])
                               }).ToList();
                }
            }
            catch
            {

            }
            return Json(caldata);
        }

        public JsonResult Getchart3(string fromDate, string toDate)
        {


            Dashboard dashboarddataobj = new Dashboard();
            List<remchart2Class> caldata = new List<remchart2Class>();
            try
            {
                DataSet objData = dashboarddataobj.GetReimbursementAnalytics(fromDate, toDate, "BYTYPECURRENTMONTH", Session["userid"].ToString());
                if (objData != null && objData.Tables[0] != null && objData.Tables[0].Rows.Count > 0)
                {
                    caldata = (from DataRow dr in objData.Tables[0].Rows
                               select new remchart2Class()
                               {
                                   Amount = Convert.ToString(dr["Amount"]),
                                   Expence_type = Convert.ToString(dr["Expence_type"])
                               }).ToList();
                }
            }
            catch
            {

            }
            return Json(caldata);
        }

        public JsonResult Getchart4(string fromDate, string toDate)
        {


            Dashboard dashboarddataobj = new Dashboard();
            List<remchart2Class> caldata = new List<remchart2Class>();
            try
            {
                DataSet objData = dashboarddataobj.GetReimbursementAnalytics(fromDate, toDate, "BYTYPELASTMONTH", Session["userid"].ToString());
                if (objData != null && objData.Tables[0] != null && objData.Tables[0].Rows.Count > 0)
                {
                    caldata = (from DataRow dr in objData.Tables[0].Rows
                               select new remchart2Class()
                               {
                                   Amount = Convert.ToString(dr["Amount"]),
                                   Expence_type = Convert.ToString(dr["Expence_type"])
                               }).ToList();
                }
            }
            catch
            {

            }
            return Json(caldata);
        }

        // party view
        public JsonResult viewPartyDetails(string id)
        {


            Dashboard dashboarddataobj = new Dashboard();
            List<ViewpartyClass> caldata = new List<ViewpartyClass>();
            try
            {
                DataTable objData = dashboarddataobj.ViewParty(id);
                if (objData != null  && objData.Rows.Count > 0)
                {
                    caldata = (from DataRow dr in objData.Rows
                               select new ViewpartyClass()
                               {
                                   Shop_Name = Convert.ToString(dr["Shop_Name"]),
                                   Address = Convert.ToString(dr["Address"]),
                                   Shop_Owner = Convert.ToString(dr["Shop_Owner"]),
                                   Shop_Lat = Convert.ToString(dr["Shop_Lat"]),
                                   Shop_Long = Convert.ToString(dr["Shop_Long"]),
                                   Shop_Owner_Contact = Convert.ToString(dr["Shop_Owner_Contact"]),
                                   PARTYSTATUS = Convert.ToString(dr["PARTYSTATUS"]),
                                   MAP_COLOR = Convert.ToString(dr["MAP_COLOR"])
                               }).ToList();
                }
            }
            catch
            {

            }
            return Json(caldata);
        }

        // Mantis Issue 25468
        public JsonResult GetDashboardDataVisitH(string stateid, string branchid)
        {
            Dashboard dashboarddataobj = new Dashboard();
            FSMDashboard Dashboarddata = new FSMDashboard();
            try
            {
                DataSet objData = dashboarddataobj.CreateLINQforDashBoardTeamVisitH(stateid, branchid);

                int NOT_LOGIN = 0;
                int AT_WORK = 0;
                int ON_LEAVE = 0;
                int Total = 0;

                string TOTALSHOP = "0";
                string REVISIT = "0";
                string NEWSHOPVISIT = "0";

                string AVGSHOPVISIT = "0";
                string AVGDURATION = "00:00";
                string TODAYSALES = "0.00";
                string AVGSALES = "0.00";
                string TOTALSALES = "0.00";

                foreach (DataRow item in objData.Tables[0].Rows)
                {
                    if (Convert.ToString(item["ACTION"]) == "NOT_LOGIN")
                    {
                        NOT_LOGIN = Convert.ToInt32(item["Count"]);
                    }
                    if (Convert.ToString(item["ACTION"]) == "AT_WORK")
                    {
                        AT_WORK = Convert.ToInt32(item["Count"]);
                    }
                    if (Convert.ToString(item["ACTION"]) == "ON_LEAVE")
                    {
                        ON_LEAVE = Convert.ToInt32(item["Count"]);
                    }
                    if (Convert.ToString(item["ACTION"]) == "EMP")
                    {
                        Total = Convert.ToInt32(item["Count"]);

                    }

                }

                Dashboarddata.lblAtWork = AT_WORK;
                Dashboarddata.lblOnLeave = ON_LEAVE;
                Dashboarddata.lblNotLoggedIn = NOT_LOGIN;
                Dashboarddata.lblTotal = Total;

                Dashboarddata.NewVisit = Convert.ToInt32(NEWSHOPVISIT);
                Dashboarddata.ReVisit = Convert.ToInt32(REVISIT);
                Dashboarddata.TotalVisit = Convert.ToInt32(TOTALSHOP);
                Dashboarddata.AvgPerDay = Convert.ToDecimal(AVGSHOPVISIT);
                Dashboarddata.AvgDurationPerShop = AVGDURATION;
                Dashboarddata.AVGSALES = AVGSALES;
                Dashboarddata.TODAYSALES = TODAYSALES;
                Dashboarddata.TOTALSALES = TOTALSALES;
            }
            catch
            {
            }
            return Json(Dashboarddata);
        }

        public PartialViewResult DashboardGridViewTeamH(FSMDashBoardFilter dd)
        {
            ViewBag.WindowSize = dd.WindowSize;
            SalesSummary_Report objgps = new SalesSummary_Report();
            DBEngine objdb = new DBEngine();
            DataTable dbDashboardData = new DataTable();
            string query = "";
            string orderby = "";

            orderby = " order by EMPNAME";

            if (dd.Type == "Attendance")
            {
                string ColumnName = "*";


                if (dd.FilterName == "AT_WORK")
                {
                    ColumnName = "EMPNAME [Employee],DESIGNATION [Designation],EMPID [Employee ID],BRANCHDESC [Branch],ISNULL(CONTACTNO,'') [Login ID],LOGGEDIN [First in time],CASE WHEN LOGEDOUT='--' THEN '' ELSE LOGEDOUT END [Last logout time],CURRENT_STATUS [Current Status],ISNULL(GPS_INACTIVE_DURATION,'00:00') +' (HH:MM)' [GPS Inactivity],ISNULL(SHOPS_VISITED,0) [Shops Visited],ISNULL(TOTAL_ORDER_BOOKED_VALUE,'0.00') [Sales Value],ISNULL(TOTAL_COLLECTION,'0.00') [Collection Amt.],CHANNEL [Channel],CIRCLE [Circle],SECTION [Section], EMPCODE EMPID";

                    orderby = " order by SHOPS_VISITED DESC";
                }
                else if (dd.FilterName == "NOT_LOGIN")
                {
                    ColumnName = "EMPNAME [Employee],DESIGNATION [Designation],EMPID [Employee ID],BRANCHDESC [Branch],ISNULL(STATE,'') [State],REPORTTO [Supervisor], REPORTTOUID [Supervisor ID], CONTACTNO [Login ID]";

                }
                else if (dd.FilterName == "ON_LEAVE")
                {
                    ColumnName = "EMPNAME [Employee],DESIGNATION [Designation],EMPID [Employee ID],BRANCHDESC [Branch],LEAVEDATE [Applied Leave Date]";
                }
                else if (dd.FilterName == "EMP")
                {
                    ColumnName = "EMPNAME [Employee],DESIGNATION [Designation],EMPID [Employee ID],BRANCHDESC [Branch],REPORTTO [Supervisor], REPORTTOUID [Supervisor ID],CONTACTNO [Login ID], CHANNEL [Channel],CIRCLE [Circle],SECTION [Section]";
                }

                DataTable dt = objgps._GetSalesSummaryReportTeamH(DateTime.Now.ToString("yyyy-MM-dd"), Convert.ToString(Session["userid"]), dd.StateId == "0" ? "" : dd.StateId, dd.BranchIdTV == "0" ? "" : dd.BranchIdTV, "", "", dd.FilterName);

                query = "Select " + ColumnName + " from FTSTEAMVISITDASHBOARD_REPORT_HIERARCHY where USERID='" + Convert.ToString(Session["userid"]) + "' and ACTION='" + Convert.ToString(dd.FilterName) + "'";

                query += orderby;

                dbDashboardData = objdb.GetDataTable(query);
            }
            // Rev Tanmoy store query in tempdata
            // TempData["DashboardGridView"] = dbDashboardData;
            TempData["DashboardGridViewTeamH"] = query;
            //End Rev 
            // DataTable MyInstrumentsList = null;
            return PartialView(dbDashboardData);
        }


        public PartialViewResult DashboardGridViewDetailsTeamVisitH(FSMDashBoardFilter dd)
        {
            ViewBag.WindowSize = dd.WindowSize;
            DataTable dbDashboardData = new DataTable();
            dbDashboardData = dashbrd.GetSalesManVisitDetails(dd.EMPCODE);
            TempData["DashboardGridViewDetailsTeamVisitH"] = dbDashboardData;
            return PartialView(dbDashboardData);
        }

        public ActionResult DashboardBranchComboboxTVH(string stateid)
        {

            FSMDashBoardFilter dashboard = new FSMDashBoardFilter();
            string userid = Session["userid"].ToString();
            int chkState = 0;
            if (stateid == null)
            {
                chkState = 1;
            }

            List<BranchData> branchdate = new List<BranchData>();
            List<BranchData> branchdateobj = new List<BranchData>();

            // Rev 1.0
            if (TempData["branchdateobj"] == null) { 
            // End of Rev 1.0
                string stateIds = dashboard.StateId;
                try
                {
                    BranchData obj = null;
                    if (stateid == null)
                    {
                        stateid = "";
                    }

                    // Rev 1.0
                    //branchdate = dashboard.GetBranchList(Convert.ToInt32(userid), stateid);
                    if (TempData["branchdate"] != null)
                    {
                        branchdate = (List<BranchData>)TempData["branchdate"];
                    }
                    else
                    {
                        branchdate = dashboard.GetBranchList(Convert.ToInt32(userid), stateid);
                    }
                    // End of Rev 1.0

                    foreach (var item in branchdate)
                    {
                        obj = new BranchData();
                        obj.BranchID = !String.IsNullOrEmpty(Convert.ToString(item.BranchID)) ? Convert.ToInt32(item.BranchID) : 0;
                        obj.name = item.name;
                        branchdateobj.Add(obj);
                    }
                }
                catch { }
                ViewBag.BranchListCount = branchdate.Count;

            // Rev 1.0
            }
            else
            {
                branchdate = (List<BranchData>) TempData["branchdate"];
                branchdateobj = (List<BranchData>) TempData["branchdateobj"];

                ViewBag.BranchListCount = branchdate.Count;
            }
            // End of Rev 1.0

            if (chkState == 1)
            {
                return PartialView("DashboardBranchComboboxTVH", branchdateobj);
            }
            else
            {
                Session["PageloadChk"] = "0";
                Session["BranchList"] = branchdateobj;
                return Json(branchdate, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult DashboardStateComboboxTVH()
        {
            FSMDashBoardFilter dashboard = new FSMDashBoardFilter();
            string userid = Session["userid"].ToString();
            //string userid = "0";
            List<StateData> statedate = new List<StateData>();
            List<StateData> statedateobj = new List<StateData>();

            // Rev 1.0
            if (TempData["statedateobj"] == null) { 
            // End of Rev 1.0
                try
                {
                    StateData obj = null;
                    statedate = dashboard.GetStateList(Convert.ToInt32(userid));
                    foreach (var item in statedate)
                    {
                        obj = new StateData();
                        obj.StateID = !String.IsNullOrEmpty(item.id) ? Convert.ToInt32(item.id) : 0;
                        obj.name = item.name;
                        statedateobj.Add(obj);
                    }
                }
                catch { }
                ViewBag.StateListCount = statedate.Count;
            // Rev 1.0
            }
            else
            {
                statedate = (List<StateData>) TempData["statedate"];
                statedateobj = (List<StateData>) TempData["statedateobj"];

                ViewBag.StateListCount = statedate.Count;
            }
            // End of Rev 1.0
          
            return PartialView(statedateobj);
        }


        public ActionResult DashboardAttendanceTVH(List<DashboardSettingMapped> list)
        {

            return PartialView(list);
        }


        public ActionResult ExportDashboardGridViewTVH(int type, String Name)
        {
            //Rev Tanmoy get data through execute query
            DataTable dbDashboardData = new DataTable();
            DBEngine objdb = new DBEngine();
            String query = TempData["DashboardGridViewTeamH"].ToString();
            dbDashboardData = objdb.GetDataTable(query);

            ViewData["DashboardGridViewTeamH"] = dbDashboardData;

            //End Rev
            TempData.Keep();

            if (ViewData["DashboardGridViewTeamH"] != null)
            {

                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetDashboardGridView(ViewData["DashboardGridViewTeamH"], Name), ViewData["DashboardGridViewTeamH"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetDashboardGridView(ViewData["DashboardGridViewTeamH"], Name), ViewData["DashboardGridViewTeamH"]);
                    // return GridViewExtension.ExportToXlsx(GetDashboardGridView(ViewData["DashboardGridView"]), dbDashboardData);Replace ViewData To datatable
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetDashboardGridView(ViewData["DashboardGridViewTeamH"], Name), ViewData["DashboardGridViewTeamH"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetDashboardGridView(ViewData["DashboardGridViewTeamH"], Name), ViewData["DashboardGridViewTeamH"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetDashboardGridView(ViewData["DashboardGridViewTeamH"], Name), ViewData["DashboardGridViewTeamH"]);
                    default:
                        break;
                }
            }
            return null;
        }

        public ActionResult ExportDashboardSummaryGridViewH(int type, String Name)
        {
            ViewData["ExportDashboardSummaryGridList"] = TempData["ExportDashboardSummaryGridList"];
            TempData.Keep();

            if (ViewData["ExportDashboardSummaryGridList"] != null)
            {
                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(ViewData["ExportDashboardSummaryGridList"], Name), ViewData["ExportDashboardSummaryGridList"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(ViewData["ExportDashboardSummaryGridList"], Name), ViewData["ExportDashboardSummaryGridList"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(ViewData["ExportDashboardSummaryGridList"], Name), ViewData["ExportDashboardSummaryGridList"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(ViewData["ExportDashboardSummaryGridList"], Name), ViewData["ExportDashboardSummaryGridList"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(ViewData["ExportDashboardSummaryGridList"], Name), ViewData["ExportDashboardSummaryGridList"]);
                    default:
                        break;
                }
                return Json("success");
            }
            return null;
        }

        public ActionResult ExportDashboardGridViewDetailsH(int type, String Headedtext)
        {
            ViewData["DashboardGridViewDetails"] = TempData["DashboardGridViewDetails"];

            TempData.Keep();

            if (ViewData["DashboardGridViewDetails"] != null)
            {

                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetDashboardGridViewDetails(ViewData["DashboardGridViewDetails"], Headedtext), ViewData["DashboardGridViewDetails"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetDashboardGridViewDetails(ViewData["DashboardGridViewDetails"], Headedtext), ViewData["DashboardGridViewDetails"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetDashboardGridViewDetails(ViewData["DashboardGridViewDetails"], Headedtext), ViewData["DashboardGridViewDetails"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetDashboardGridViewDetails(ViewData["DashboardGridViewDetails"], Headedtext), ViewData["DashboardGridViewDetails"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetDashboardGridViewDetails(ViewData["DashboardGridViewDetails"], Headedtext), ViewData["DashboardGridViewDetails"]);
                    default:
                        break;
                }
            }
            return null;
        }

        public ActionResult ExportDashboardGridViewSalesmanDetailH(int type, String Name)
        {
            ViewData["DashboardGridViewSalesmanDetailH"] = TempData["DashboardGridViewSalesmanDetailH"];
            //ViewData["DashboardGridViewSalesmanDetailType"] = TempData["DashboardGridViewSalesmanDetailType"];
            TempData.Keep();

            if (ViewData["DashboardGridViewSalesmanDetailH"] != null)
            {
                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetDashboardGridViewSalesman(ViewData["DashboardGridViewSalesmanDetailH"], Name), ViewData["DashboardGridViewSalesmanDetailH"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetDashboardGridViewSalesman(ViewData["DashboardGridViewSalesmanDetailH"], Name), ViewData["DashboardGridViewSalesmanDetailH"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetDashboardGridViewSalesman(ViewData["DashboardGridViewSalesmanDetailH"], Name), ViewData["DashboardGridViewSalesmanDetailH"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetDashboardGridViewSalesman(ViewData["DashboardGridViewSalesmanDetailH"], Name), ViewData["DashboardGridViewSalesmanDetailH"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetDashboardGridViewSalesman(ViewData["DashboardGridViewSalesmanDetailH"], Name), ViewData["DashboardGridViewSalesmanDetailH"]);
                    default:
                        break;
                }
            }
            return null;
        }
        // End of Mantis Issue 25468


        //REV 4.0
        public ActionResult DashboardLMS(List<DashboardSettingMapped> list)
        {

            return PartialView(list);
        }

        public JsonResult GETLMSCOUNTDATA(string stateid, string branchid)       
        {
            Dashboard dashboarddataobj = new Dashboard();
            FSMDashboard Dashboarddata = new FSMDashboard();
            try
            {                
                DataSet objData = dashboarddataobj.LINQFORLMSDASHBOARD(stateid, branchid);
               
                int TotalLearnersCNT = 0;
                int AssignedTopicsCNT = 0;
                int YettoStartCNT = 0;
                int InProgressCNT = 0;
                int CompletedCNT = 0;
                decimal AverageProgressCNT = 0;


                foreach (DataRow item in objData.Tables[0].Rows)
                {
                    TotalLearnersCNT = Convert.ToInt32(item["CNT"]);
                }
                foreach (DataRow item in objData.Tables[1].Rows)
                {
                    AssignedTopicsCNT = Convert.ToInt32(item["CNT"]);
                }
                foreach (DataRow item in objData.Tables[2].Rows)
                {
                    YettoStartCNT = Convert.ToInt32(item["CNT"]);
                }
                foreach (DataRow item in objData.Tables[3].Rows)
                {
                    InProgressCNT = Convert.ToInt32(item["CNT"]);
                }
                foreach (DataRow item in objData.Tables[4].Rows)
                {
                    CompletedCNT = Convert.ToInt32(item["CNT"]);
                }
                foreach (DataRow item in objData.Tables[5].Rows)
                {
                    AverageProgressCNT = Convert.ToDecimal(item["AverageProgress"]);
                }

                Dashboarddata.TotalLearners = TotalLearnersCNT;
                Dashboarddata.AssignedTopics = AssignedTopicsCNT;
                Dashboarddata.YettoStart = YettoStartCNT;
                Dashboarddata.InProgress = InProgressCNT;
                Dashboarddata.Completed = CompletedCNT;
                Dashboarddata.AverageProgress = AverageProgressCNT;

            }
            catch
            {
            }
            return Json(Dashboarddata);
        }

        public PartialViewResult DashBoardGVLMS(FSMDashBoardFilter dd)
        {
            DataTable dt=new DataTable();
            String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
            SqlCommand sqlcmd = new SqlCommand();
            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();
            sqlcmd = new SqlCommand("prc_LMSDASHBOARDDATA", sqlcon);
            sqlcmd.Parameters.Add("@ACTION", dd.ActionType);
            sqlcmd.Parameters.Add("@USERID", Convert.ToString(Session["userid"]));
            sqlcmd.Parameters.Add("@STATEID", dd.STATEIDS);
            sqlcmd.Parameters.Add("@BRANCHID", dd.BRANCHIDS);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dt);
            sqlcon.Close();
            TempData["LMSDashboardGridView"] = dt;
            TempData.Keep();
            return PartialView(dt);
        }

        public ActionResult LMSExportDashboardGridView(int type, String Name)
        {
            //Rev Tanmoy get data through execute query
            DataTable dbDashboardData = new DataTable();
            DBEngine objdb = new DBEngine();           

            if (TempData["LMSDashboardGridView"] != null)
            {

                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetDashboardGridViewLMS(TempData["LMSDashboardGridView"], Name), TempData["LMSDashboardGridView"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetDashboardGridViewLMS(TempData["LMSDashboardGridView"], Name), TempData["LMSDashboardGridView"]);
                    // return GridViewExtension.ExportToXlsx(GetDashboardGridView(ViewData["DashboardGridView"]), dbDashboardData);Replace ViewData To datatable
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetDashboardGridViewLMS(TempData["LMSDashboardGridView"], Name), TempData["LMSDashboardGridView"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetDashboardGridViewLMS(TempData["LMSDashboardGridView"], Name), TempData["LMSDashboardGridView"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetDashboardGridViewLMS(TempData["LMSDashboardGridView"], Name), TempData["LMSDashboardGridView"]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetDashboardGridViewLMS(object datatable, String Name)
        {
            var settings = new GridViewSettings();
            //settings.Name = "DashboardGridView";
            settings.Name = Name;
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            // settings.SettingsExport.FileName = "DashboardGridView";
            settings.SettingsExport.FileName = Name;
            //String ID = Convert.ToString(TempData["LMSDashboardGridView"]);
            //TempData.Keep();
            DataTable dt = (DataTable)datatable;

            foreach (System.Data.DataColumn datacolumn in dt.Columns)
            {
                //if (datacolumn.ColumnName != "EMPID")
                //{
                    settings.Columns.Add(column =>
                    {
                        column.Caption = datacolumn.ColumnName;
                        column.FieldName = datacolumn.ColumnName;
                        //if (datacolumn.DataType.FullName == "System.Decimal" || datacolumn.DataType.FullName == "System.Int32" || datacolumn.DataType.FullName == "System.Int64")
                        //{
                        //    if (datacolumn.ColumnName != "Shops Visited")
                        //    {
                        //        column.PropertiesEdit.DisplayFormatString = "0.00";
                        //    }
                        //}
                    });
                //}

            }

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }
        public ActionResult DashboardBranchComboboxLMS(string stateid)
        {
            FSMDashBoardFilter dashboard = new FSMDashBoardFilter();
            string userid = Session["userid"].ToString();
            int chkState = 0;
            if (stateid == null)
            {
                chkState = 1;
            }
            List<BranchData> branchdate = new List<BranchData>();
            List<BranchData> branchdateobj = new List<BranchData>();            
            if (TempData["branchdateobjLMS"] == null)
            {                
                string stateIds = dashboard.StateId;
                try
                {
                    BranchData obj = null;
                    if (stateid == null)
                    {
                        stateid = "";
                    }

                    branchdate = dashboard.GetLMSBranchList(Convert.ToInt32(userid), stateid);

                    foreach (var item in branchdate)
                    {
                        obj = new BranchData();
                        obj.BranchID = !String.IsNullOrEmpty(Convert.ToString(item.BranchID)) ? Convert.ToInt32(item.BranchID) : 0;
                        obj.name = item.name;
                        branchdateobj.Add(obj);
                    }
                }
                catch { }
                ViewBag.BranchListCount = branchdate.Count;              
            }
            else
            {
                branchdate = (List<BranchData>)TempData["branchdate"];
                branchdateobj = (List<BranchData>)TempData["branchdateobjLMS"];
                ViewBag.BranchListCount = branchdate.Count;
            }          

            if (chkState == 1)
            {
                return PartialView("DashboardBranchComboboxLMS", branchdateobj);
            }
            else
            {
                Session["PageloadChk"] = "0";
                Session["BranchList"] = branchdateobj;
                return Json(branchdate, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DashboardStateComboboxLMS()
        {

            FSMDashBoardFilter dashboard = new FSMDashBoardFilter();
            string userid = Session["userid"].ToString();
            //string userid = "0";
            List<StateData> statedate = new List<StateData>();
            List<StateData> statedateobj = new List<StateData>();

            // Rev 1.0
            if (TempData["statedateobj"] == null)
            {
                // End of Rev 1.0
                try
                {
                    StateData obj = null;
                    statedate = dashboard.GetStateList(Convert.ToInt32(userid));
                    foreach (var item in statedate)
                    {
                        obj = new StateData();
                        obj.StateID = !String.IsNullOrEmpty(item.id) ? Convert.ToInt32(item.id) : 0;
                        obj.name = item.name;
                        statedateobj.Add(obj);
                    }
                }
                catch { }
                ViewBag.StateListCount = statedate.Count;
                // Rev 1.0
            }
            else
            {
                statedate = (List<StateData>)TempData["statedate"];
                statedateobj = (List<StateData>)TempData["statedateobj"];

                ViewBag.StateListCount = statedate.Count;
            }
            // End of Rev 1.0

            return PartialView(statedateobj);
        }

        //REV 4.0 END
    }

}


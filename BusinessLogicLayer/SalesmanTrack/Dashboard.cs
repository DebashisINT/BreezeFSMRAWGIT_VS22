/******************************************************************************************************************
 * Rev 1.0      27-06-2023      Sanchita    V2.0.41     State & Branch selection facility is required in the Order Analytics in Dashboard
 *                                                      Refer: 26309
 * Rev 2.0      31-08-2023      Sanchita    V2.0.43     FSM - Dashboard - View Party - Enhancement required. Refer: 26753 
 * Rev 3.0      23-11-2023      Priti       V2.0.43     0027031: Dashboard report issue(check in local Rubyfoods db)
 * ******************************************************************************************************************/
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BusinessLogicLayer.SalesmanTrack
{
    public class Dashboard
    {
        // view party
        public DataTable ViewParty(string userid)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_DASHBOARDPARTYDETAIL");

            proc.AddPara("@Action", "GETLIST");
            proc.AddPara("@user_id", userid);
            ds = proc.GetTable();

            return ds;
        }
        public DataTable GetFtsDashboardyList(string userid)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("FTS_Dashboard");

            proc.AddPara("@UseriD", userid);
            ds = proc.GetTable();

            return ds;
        }

        public DataTable GetUserLocationTrackList(string user, string date)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_FSMDashboardData");

            proc.AddPara("@userid", user);
            proc.AddPara("@Action", "TrackRoute");
            proc.AddPara("@ToDate", date);
            ds = proc.GetTable();
            return ds;
        }

        public DataTable GetUserLocationTrackListRoute(string user, string date, string IsGmap)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_FSMDashboardDataRoute");

            proc.AddPara("@userid", user);
            proc.AddPara("@Action", "TrackRoute");
            proc.AddPara("@ToDate", date);
            proc.AddPara("@isGmap", IsGmap);

            ds = proc.GetTable();
            return ds;
        }

        public DataTable GetUserLocationTrackListRouteOutlet(string user, string date, string IsGmap)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_FSMDashboardDataRoute");

            proc.AddPara("@userid", user);
            proc.AddPara("@Action", "ShopRouteTrack");
            proc.AddPara("@ToDate", date);
            proc.AddPara("@isGmap", IsGmap);

            ds = proc.GetTable();
            return ds;
        }

        public DataTable GetFtsDashboardyList(string todaydate, string userid)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSDASHBOARD_REPORT");

            proc.AddPara("@TODAYDATE", todaydate);
            proc.AddPara("@ACTION", "ALL");
            proc.AddPara("@RPTTYPE", "Summary");
            proc.AddPara("@USERID", userid);
            ds = proc.GetTable();

            return ds;
        }

        public DataTable GetSalesManVisitDetails(string empcode)
        {
            int i = 0;
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_DashboardShopDetails");


            proc.AddPara("@Action", "ShopDetails");
            proc.AddPara("@ToDate", DateTime.Now.ToString("yyyy-MM-dd"));
            proc.AddPara("@Fromdate", DateTime.Now.ToString("yyyy-MM-dd"));
            proc.AddPara("@user_id", Convert.ToString(HttpContext.Current.Session["userid"]));
            proc.AddPara("@EMPCODE", empcode);
            ds = proc.GetTable();
            return ds;
        }
        // Mantis Issue 25468
        //public DataSet CreateLINQforDashBoard(string stateid)
        public DataSet CreateLINQforDashBoard(string stateid, string branchid)
            // End of Mantis Issue 25468
        {
            int i = 0;
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("prc_FSMDashboardData");

            // Mantis Issue 25468
            if (branchid == null)
            {
                branchid = "";
            }
            // End of Mantis Issue 25468

            proc.AddPara("@Action", "TODAYDATA");
            proc.AddPara("@ToDate", DateTime.Now.ToString("yyyy-MM-dd"));
            proc.AddPara("@Fromdate", DateTime.Now.ToString("yyyy-MM-dd"));
            proc.AddPara("@userid", Convert.ToString(HttpContext.Current.Session["userid"]));
            proc.AddPara("@stateid", stateid);
            // Mantis Issue 25468
            proc.AddPara("@branchid", branchid);
            // End of Mantis Issue 25468
            ds = proc.GetDataSet();
            return ds;
        }
        // Team visit
        public DataSet CreateLINQforDashBoardTeamVisit(string stateid, string branchid)
        {
            int i = 0;
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("prc_FSMDashboardData");
            if (branchid == null)
            {
                branchid = "";
            }

            proc.AddPara("@Action", "TODAYTEAMVISITDATA");
            proc.AddPara("@ToDate", DateTime.Now.ToString("yyyy-MM-dd"));
            proc.AddPara("@Fromdate", DateTime.Now.ToString("yyyy-MM-dd"));
            proc.AddPara("@userid", Convert.ToString(HttpContext.Current.Session["userid"]));
            proc.AddPara("@stateid", stateid);
            proc.AddPara("@branchid", branchid);
            ds = proc.GetDataSet();
            return ds;
        }
        //End
        //public DataTable GetDashboardAttendanceData()
        //{
        //    DBEngine Dbengine = new DBEngine();
        //    DataTable DashboardTable = Dbengine.GetDataTable("select ACTION,COUNT(*) Count from FTSDASHBOARD_REPORT   WHERE USERID='" + Convert.ToString(HttpContext.Current.Session["userid"]) + "' group by ACTION");
        //    return DashboardTable;
        //}
        // Mantis Issue 25468
        public DataSet CreateLINQforDashBoardTeamVisitH(string stateid, string branchid)
        {
            int i = 0;
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("prc_FSMDashboardDataHierarchy");
            if (branchid == null)
            {
                branchid = "";
            }

            proc.AddPara("@Action", "TODAYTEAMVISITDATA");
            proc.AddPara("@ToDate", DateTime.Now.ToString("yyyy-MM-dd"));
            proc.AddPara("@Fromdate", DateTime.Now.ToString("yyyy-MM-dd"));
            proc.AddPara("@userid", Convert.ToString(HttpContext.Current.Session["userid"]));
            proc.AddPara("@stateid", stateid);
            proc.AddPara("@branchid", branchid);
            ds = proc.GetDataSet();
            return ds;
        }

        public DataTable GetDashboardGridDataH(string action, string rptype, string empid, string stateid, string designid)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSDASHBOARDGRIDDETAILS_REPORT_HIERARCHY");

            proc.AddPara("@TODAYDATE", DateTime.Now.ToString("yyyy-MM-dd"));
            proc.AddPara("@STATEID", stateid);
            proc.AddPara("@DESIGNID", designid);
            proc.AddPara("@USERID", Convert.ToString(HttpContext.Current.Session["userid"]));
            proc.AddPara("@EMPID", empid);
            proc.AddPara("@HIERARCHY", "A");
            //proc.AddPara("@DAYCOUNT", "7");
            proc.AddPara("@ACTION", action);
            proc.AddPara("@RPTTYPE", rptype);


            ds = proc.GetTable();

            return ds;


        }
        // End of Mantis Issue 25468
        public DataTable GETMapDashboard(string stateID, string CREATE_USERID = "0")
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PROC_MAPPLOT_DASHBOARD");

            proc.AddPara("@StateID", stateID);
            proc.AddPara("@CREATE_USERID", CREATE_USERID);
            ds = proc.GetTable();

            return ds;
        }
        //Rev work start 15.06.2022 0024954: Need to change View Route of FSM Dashboard
        public DataTable GETLocationDashboard(string CREATE_USERID, string FromDT, string ToDT, string DataSpan)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Sp_API_Locationfetch");
            proc.AddPara("@user_id", CREATE_USERID);
            proc.AddPara("@from_date", FromDT);
            proc.AddPara("@to_date", ToDT);
            proc.AddPara("@date_span", DataSpan);
            ds = proc.GetTable();

            return ds;
        }
        //Rev work close 15.06.2022 0024954: Need to change View Route of FSM Dashboard
        public DataTable GETPartyDashboard(string stateID, string TYPE_ID, string PARTY_ID, string IS_Electician, string CREATE_USERID = "0")
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_DASHBOARDPARTYDETAIL");
            proc.AddPara("@ACTION", "GETLISTSTATEWISE");
            proc.AddPara("@StateID", stateID);
            proc.AddPara("@TYPE_ID", TYPE_ID);
            proc.AddPara("@PARTY_ID", PARTY_ID);
            proc.AddPara("@IS_Electician", IS_Electician);
            proc.AddPara("@CREATE_USERID", CREATE_USERID);
            ds = proc.GetTable();

            return ds;
        }

        public DataTable GETOutletDashboard(string stateID, string PARTY_ID, string PartyStatus, string month, string year, string CREATE_USERID="0")
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_OutletListMapReport");
            //proc.AddPara("@ACTION", "GETLISTSTATEWISE");
            proc.AddPara("@State", stateID);
            proc.AddPara("@PartyStatus", PartyStatus);
            proc.AddPara("@PartyType", PARTY_ID);
            proc.AddPara("@Month", month);
            proc.AddPara("@Year", year);
            proc.AddPara("@CREATE_USERID", CREATE_USERID);
            ds = proc.GetTable();

            return ds;
        }
        //Rev Debashis && Added BranchId 0025198
        public DataTable GETMapDashboard(string BranchId,string stateID, String Date, string CREATE_USERID = "0")
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PROC_MAPPLOT_DASHBOARDDateWise");
            proc.AddPara("@StateID", stateID);
            proc.AddPara("@Date", Date);
            proc.AddPara("@CREATE_USERID", CREATE_USERID);
            proc.AddPara("@BRANCHID", BranchId);
            ds = proc.GetTable();

            return ds;
        }
        //Rev 3.0
        public DataTable GETMapDashboardEMPOutlet(string BranchId, string stateID, String Date, string CREATE_USERID = "0")
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PROC_EMPOUTLETMAPPDASHBOARDDATEWISE");
            proc.AddPara("@StateID", stateID);
            proc.AddPara("@Date", Date);
            proc.AddPara("@CREATE_USERID", CREATE_USERID);
            proc.AddPara("@BRANCHID", BranchId);
            ds = proc.GetTable();

            return ds;
        }
        //Rev 3.0 End
        public DataTable GetLiveVisits(string stateID)
        {

            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSTRACKSALESMANCURRENTLOCATION_REPORT");
            proc.AddPara("@TODAYDATE", DateTime.Now.ToString("yyyy-MM-dd"));
            proc.AddPara("@HIERARCHY", "A");
            proc.AddPara("@STATEID", stateID);
            proc.AddPara("@RPTTYPE", "Summary");
            proc.AddPara("@USERID", Convert.ToString(HttpContext.Current.Session["userid"]));

            dt = proc.GetTable();
            return dt;
        }

        public DataTable GetAtWorkSummary(string stateid)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSDASHBOARD_REPORT");

            proc.AddPara("@TODAYDATE", DateTime.Now.ToString("yyyy-MM-dd"));
            proc.AddPara("@STATEID", stateid);
            proc.AddPara("@DESIGNID", "");

            proc.AddPara("@USERID", Convert.ToString(HttpContext.Current.Session["userid"]));
            proc.AddPara("@EMPID", "");

            proc.AddPara("@ACTION", "AT_WORK");
            proc.AddPara("@RPTTYPE", "Summary");
            ds = proc.GetTable();

            return ds;


        }

        public DataTable GetDashboardGridData(string action, string rptype, string empid, string stateid, string designid)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSDASHBOARDGRIDDETAILS_REPORT");

            proc.AddPara("@TODAYDATE", DateTime.Now.ToString("yyyy-MM-dd"));
            proc.AddPara("@STATEID", stateid);
            proc.AddPara("@DESIGNID", designid);
            proc.AddPara("@USERID", Convert.ToString(HttpContext.Current.Session["userid"]));
            proc.AddPara("@EMPID", empid);
            proc.AddPara("@HIERARCHY", "A");
            //proc.AddPara("@DAYCOUNT", "7");
            proc.AddPara("@ACTION", action);
            proc.AddPara("@RPTTYPE", rptype);


            ds = proc.GetTable();

            return ds;


        }

        public DataSet GetAttendanceDashboard(string userid, string month, string year, string action, string CREATE_USERID="0")
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSATTNDASHBOARD");

            proc.AddPara("@User_iD", userid);
            proc.AddPara("@month", month);
            proc.AddPara("@year", year);
            proc.AddPara("@action", action);
            proc.AddPara("@CREATE_USERID", CREATE_USERID);
            ds = proc.GetDataSet();

            return ds;
        }
        // Rev 1.0
        //public DataSet GetOrderAnalytics(string fromDate, string toDate, string reportType, string USERID="0")
        public DataSet GetOrderAnalytics(string fromDate, string toDate, string reportType, string stateid, string branchid, string USERID = "0")
            // End of Rev 1.0
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSORDERDBWITHANOTHERTAB_REPORT");

            proc.AddPara("@FROMDATE", fromDate);
            proc.AddPara("@TODATE", toDate);
            //proc.AddPara("@REPORTTYPE", reportType);
            proc.AddPara("@REPORTTYPE", reportType);
            proc.AddPara("@USERID", USERID);
            // Rev 1.0
            proc.AddPara("@STATEID", stateid);
            proc.AddPara("@BRANCHID", branchid);
            // End of Rev 1.0
            ds = proc.GetDataSet();

            return ds;
        }


        public DataSet GetTrgtboxData()
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("PRC_DASHBOARD_TARGETACHIEVEMENT");
            proc.AddPara("@ACTION", "BOXDATA");
            ds = proc.GetDataSet();

            return ds;
        }

        public DataSet GetTrgtMonthwiseData(string monthyear)
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("PRC_DASHBOARD_TARGETACHIEVEMENT");
            proc.AddPara("@ACTION", "LastFiveMonthData");
            proc.AddPara("@MONTHYEARCHOOSEN", monthyear);
            ds = proc.GetDataSet();

            return ds;
        }

        public DataSet GetReimbursementAnalytics(string fromDate, string toDate, string action, string CREATE_USERID="0")
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSDashboardReimbursement");

            proc.AddPara("@FROMDATE", fromDate);
            proc.AddPara("@TODATE", toDate);
            //proc.AddPara("@REPORTTYPE", reportType);
            proc.AddPara("@ACTION", action);
            proc.AddPara("@CREATE_USERID", CREATE_USERID);
            ds = proc.GetDataSet();

            return ds;
        }

        public DataSet LINQFORLMSDASHBOARD(string stateid, string branchid)
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("prc_LMSDASHBOARDDATA");
            proc.AddPara("@ACTION", "TOTALCOUNT");
            proc.AddPara("@USERID", Convert.ToString(HttpContext.Current.Session["userid"]));
            proc.AddPara("@STATEID", stateid);           
            proc.AddPara("@BRANCHID", branchid);
            ds = proc.GetDataSet();

            return ds;
        }



    }


}

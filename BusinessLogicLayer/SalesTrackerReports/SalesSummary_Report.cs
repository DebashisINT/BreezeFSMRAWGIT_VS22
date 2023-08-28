#region======================================Revision History=========================================================================
//1.0   V2.0.38     Debashis    20/01/2023      Revisit Contact information is required in the Performance Summary report.
//                                              Refer: 0025586
//2.0   V2.0.38     Debashis    31/01/2023      A new report is required as "Performance Analytics".
//                                              Refer: 0025620

//3.0   V2.0.42     Priti       19/07/2023      0026135: Branch Parameter is required for various FSM reports

#endregion===================================End of Revision History==================================================================
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesTrackerReports
{
    public class SalesSummary_Report
    {
        //Rev 3.0
        //public DataTable GetSalesSummaryReport(string fromdate, string todate, string userid, string stateID, string desigid, string empid)
        public DataTable GetSalesSummaryReport(string fromdate, string todate, string userid, string stateID, string desigid, string empid, string Branch_Id)
        //REV 3.0 END
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSSALESSUMMARY_REPORT");

            proc.AddPara("@FROMDATE", fromdate);
            proc.AddPara("@TODATE", todate);
            proc.AddPara("@STATEID", stateID);
            proc.AddPara("@DESIGNID", desigid);
            proc.AddPara("@USERID", userid);
            proc.AddPara("@EMPID", empid);
            //Rev 3.0
            proc.AddPara("@BRANCHID", Branch_Id);
            //Rev 3.0 End
            ds = proc.GetTable();
            return ds;
        }
        //Rev 1.0 Mantis: 0025586
        //public DataTable GetSalesPerformanceReport(string fromdate, string todate, string userid, string stateID, string desigid, string empid)
        // Rev 3.0 [ parameter string BranchId added ]
        public DataTable GetSalesPerformanceReport(string BranchId,string fromdate, string todate, string userid, string stateID, string desigid, string empid, int IsRevisitContactDetails)
        //End of Rev 1.0 Mantis: 0025586
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSEMPLOYEEPERFORMANCE_REPORT");

            proc.AddPara("@FROMDATE", fromdate);
            proc.AddPara("@TODATE", todate);
            proc.AddPara("@STATEID", stateID);
            proc.AddPara("@DESIGNID", desigid);            
            proc.AddPara("@EMPID", empid);
            //Rev 1.0 Mantis: 0025586
            proc.AddPara("@ISREVISITCONTACTDETAILS", IsRevisitContactDetails);
            //End of Rev 1.0 Mantis: 0025586
            proc.AddPara("@USERID", userid);
            // Rev 3.0
            proc.AddPara("@BRANCHID", BranchId);
            // End of Rev 3.0
            ds = proc.GetTable();

            return ds;
        }

        //Rev 2.0 Mantis: 0025620
        public DataTable GetPerformanceAnalyReport(string fromdate, string todate, string userid, string stateID, string desigid, string empid, int IsRevisitContactDetails)        
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSEMPLOYEEPERFORMANCEANALYTICS_REPORT");

            proc.AddPara("@FROMDATE", fromdate);
            proc.AddPara("@TODATE", todate);
            proc.AddPara("@STATEID", stateID);
            proc.AddPara("@DESIGNID", desigid);
            proc.AddPara("@EMPID", empid);
            proc.AddPara("@ISREVISITCONTACTDETAILS", IsRevisitContactDetails);
            proc.AddPara("@USERID", userid);
            ds = proc.GetTable();

            return ds;
        }
        //End of Rev 2.0 Mantis: 0025620

        //Rev Debashis
        public DataTable GetOrderRegisterListReport(string fromdate, string todate, string userid, string stateID, string desigid, string empid)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSORDERWITHPRODUCTATTRIBUTE_REPORT");

            proc.AddPara("@FROMDATE", fromdate);
            proc.AddPara("@TODATE", todate);
            proc.AddPara("@STATEID", stateID);
            proc.AddPara("@DESIGNID", desigid);
            proc.AddPara("@EMPID", empid);
            proc.AddPara("@USERID", userid);
            ds = proc.GetTable();

            return ds;
        }
        //End of Rev Debashis

        public DataTable GetEmployeePerformanceReport(string fromdate, string todate, string userid, string stateID, string desigid, string empid)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSEMPLOYEEPERFORMANCEDETAILS_REPORT");

            proc.AddPara("@FROMDATE", fromdate);
            proc.AddPara("@TODATE", todate);
            proc.AddPara("@STATEID", stateID);
            proc.AddPara("@DESIGNID", desigid);
            proc.AddPara("@USERID", userid);
            proc.AddPara("@EMPID", empid);
            ds = proc.GetTable();

            return ds;
        }

        //Rev Debashis
        public DataTable GetPerformanceVisitRegReport(string fromdate, string todate, string userid, string stateID, string desigid, string empid)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSEMPLOYEEPERFORMANCEVISITREGISTER_REPORT");

            proc.AddPara("@FROMDATE", fromdate);
            proc.AddPara("@TODATE", todate);
            proc.AddPara("@STATEID", stateID);
            proc.AddPara("@DESIGNID", desigid);
            proc.AddPara("@EMPID", empid);
            proc.AddPara("@USERID", userid);
            ds = proc.GetTable();

            return ds;
        }
        //End of Rev Debashis

        public DataTable GetAttendProductRatioReport(string fromdate, string todate, string userid, string stateID, string desigid, string empid)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSATTENDANCEVSPRODUCTIVITYRATIO_REPORT");

            proc.AddPara("@FROMDATE", fromdate);
            proc.AddPara("@TODATE", todate);
            proc.AddPara("@STATEID", stateID);
            proc.AddPara("@DESIGNID", desigid);
            proc.AddPara("@EMPID", empid);
            proc.AddPara("@USERID", userid);
            ds = proc.GetTable();

            return ds;
        }


        // Mantis Issue 24765
        //public DataTable _GetSalesSummaryReport(string reportdate, string userid, string stateID, string desigid, string empid, string Type)
        public DataTable _GetSalesSummaryReport(string reportdate, string userid, string stateID, string desigid, string empid, string Type, string branchID)
            // End of Mantis Issue 24765
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSDASHBOARD_REPORT");

            proc.AddPara("@TODAYDATE", reportdate);
            proc.AddPara("@STATEID", stateID);
            proc.AddPara("@DESIGNID", desigid);

            proc.AddPara("@USERID", userid);
            proc.AddPara("@EMPID", empid);
            // Mantis Issue 24765
            proc.AddPara("@BRANCHID", branchID);
            // End of Mantis Issue 24765

            proc.AddPara("@ACTION", Type);
            proc.AddPara("@RPTTYPE", "Detail");
            ds = proc.GetTable();

            return ds;
        }
        public DataTable _GetSalesSummaryReportTeam(string reportdate, string userid, string stateID, string BRANCHID, string desigid, string empid, string Type)
        { 
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSTEAMVISITDASHBOARD_REPORT");

            proc.AddPara("@TODAYDATE", reportdate);
            proc.AddPara("@STATEID", stateID);
            proc.AddPara("@DESIGNID", desigid);
            proc.AddPara("@BRANCHID", BRANCHID);
            proc.AddPara("@USERID", userid);
            proc.AddPara("@EMPID", empid);

            proc.AddPara("@ACTION", Type);
            proc.AddPara("@RPTTYPE", "Detail");
            ds = proc.GetTable();

            return ds;
        }
        //Rev 3.0
        //public DataTable GetSalesSummaryReportDayWise(string fromdate, string todate, string userid, string stateID, string desigid, string empid)
        public DataTable GetSalesSummaryReportDayWise(string fromdate, string todate, string userid, string stateID, string desigid, string empid, string Branch_Id)
        //Rev 3.0 End
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSSALESSUMMARY_REPORTDALY");
            proc.AddPara("@FROMDATE", fromdate);
            proc.AddPara("@TODATE", todate);
            proc.AddPara("@STATEID", stateID);
            proc.AddPara("@DESIGNID", desigid);
            proc.AddPara("@USERID", userid);
            proc.AddPara("@EMPID", empid);
            //Rev 3.0
            proc.AddPara("@BRANCHID", Branch_Id);
            //Rev 3.0 End
            ds = proc.GetTable();

            return ds;
        }

        public DataTable GetStateMandatory(string userid)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_StateMandatoryforReport");
            proc.AddPara("@USERID", userid);
            dt = proc.GetTable();
            return dt;
        }

        public int InsertPageRetention(string Col, String USER_ID, String ReportName)
        {
            ProcedureExecute proc = new ProcedureExecute("PRC_FTS_PageRetention");
            proc.AddPara("@Col", Col);
            proc.AddPara("@ReportName", ReportName);
            proc.AddPara("@USER_ID", USER_ID);
            proc.AddPara("@ACTION", "INSERT");
            int i = proc.RunActionQuery();
            return i;
        }

        public DataTable GetPageRetention(String USER_ID, String ReportName)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTS_PageRetention");
            proc.AddPara("@ReportName", ReportName);
            proc.AddPara("@USER_ID", USER_ID);
            proc.AddPara("@ACTION", "DETAILS");
            dt = proc.GetTable();
            return dt;
        }
        //rev Pratik
        public DataTable GetStockPositonReport(string fromdate, string todate, string userid, string product, string type)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSSTOCKPOSITION_REPORT");

            proc.AddPara("@FROMDATE", fromdate);
            proc.AddPara("@TODATE", todate);
            proc.AddPara("@PRODID", product);
            proc.AddPara("@REPORTTYPE ", type);
            proc.AddPara("@USERID", userid);
            ds = proc.GetTable();

            return ds;
        }
        //End of rev Pratik
        //Mantis Issue 24684
        public DataTable GetGPSStatsReport(string fromdate, string todate, string userid, string stateID, string desigid, string empid)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSGPSSTATISTICS_REPORT");

            proc.AddPara("@FROMDATE", fromdate);
            proc.AddPara("@TODATE", todate);
            proc.AddPara("@STATEID", stateID);
            proc.AddPara("@DESIGNID", desigid);

            proc.AddPara("@USERID", userid);
            proc.AddPara("@EMPID", empid);
            ds = proc.GetTable();

            return ds;
        }
        //End of Mantis Issue 24684
        //rev Pratik
        public DataTable GetQuotationDetails(string fromdate, string todate, string userid, string shopcode, string empcode)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSQUOTATIONDETAILS_REPORT");

            proc.AddPara("@FROMDATE", fromdate);
            proc.AddPara("@TODATE", todate);
            proc.AddPara("@EMPID", empcode);
            proc.AddPara("@SHOPID", shopcode);
            proc.AddPara("@USERID", userid);
            ds = proc.GetTable();

            return ds;
        }
        //End of rev Pratik
        //Rev Pratik
        public DataTable GetBreakageTrackingRegister(string fromdate, string todate, string userid, string prodcode, string empcode)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSBREAKAGETRACKREGISTER_REPORT");

            proc.AddPara("@FROMDATE", fromdate);
            proc.AddPara("@TODATE", todate);
            proc.AddPara("@EMPID", empcode);
            proc.AddPara("@PRODID", prodcode);
            proc.AddPara("@USERID", userid);
            ds = proc.GetTable();

            return ds;
        }
        //End of rev Pratik

        // Mantis Issue 25468_old
        public DataTable GetQuotationApprovalDetails(string fromdate, string todate, string userid, string shopcode, string empcode)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSQUOTATIONAPPROVALDETAILS_REPORT");

            proc.AddPara("@ACTION", "LISTING");
            proc.AddPara("@FROMDATE", fromdate);
            proc.AddPara("@TODATE", todate);
            proc.AddPara("@EMPID", empcode);
            proc.AddPara("@SHOPID", shopcode);
            proc.AddPara("@USERID", userid);
            ds = proc.GetTable();

            return ds;
        }
        // End of Mantis Issue 25468_old
        // Mantis Issue 25468
        public DataTable _GetSalesSummaryReportTeamH(string reportdate, string userid, string stateID, string BRANCHID, string desigid, string empid, string Type)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSTEAMVISITDASHBOARD_REPORT_HIERARCHY");

            proc.AddPara("@TODAYDATE", reportdate);
            proc.AddPara("@STATEID", stateID);
            proc.AddPara("@DESIGNID", desigid);
            proc.AddPara("@BRANCHID", BRANCHID);
            proc.AddPara("@USERID", userid);
            proc.AddPara("@EMPID", empid);

            proc.AddPara("@ACTION", Type);
            proc.AddPara("@RPTTYPE", "Detail");
            ds = proc.GetTable();

            return ds;
        }
        // End of Mantis Issue 25468
    }
}

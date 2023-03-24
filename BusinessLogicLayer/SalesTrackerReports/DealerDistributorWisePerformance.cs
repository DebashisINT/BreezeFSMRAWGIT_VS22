/* ****************************************************************************************************************************
 * Rev 1.0		Sanchita	V2.0.39		16/03/2023		All months are not showing for Previous year while selecting parameter 
 *                                                      in Dealer/Distributor wise Sales report. Refer: 25732
**************************************************************************************************************************** */
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesTrackerReports
{
    public class DealerDistributorWisePerformance
    {
        public DataTable GetDealerDistributorWisePerformanceReport(string month, string stateID, string empid, String year, string userid)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSDEALERDISTRIBUTORWISEPERFORMANCE_REPORT");

            proc.AddPara("@MONTH", month);
            proc.AddPara("@STATEID", stateID);
            proc.AddPara("@EMPID", empid);
            proc.AddPara("@YEARS", year);
            proc.AddPara("@USERID", userid);            
            ds = proc.GetTable();
            return ds;
        }

        public DataTable GetYearList()
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_MASTERYEARLIST");
            ds = proc.GetTable();
            return ds;
        }

        // Rev 1.0 [ parameter "year_id" added ]
        public DataTable GetMonthList(string years)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSDYANAMICMONTHSBIND_REPORT");
            // Rev 1.0
            proc.AddPara("@YEARS", years);
            // End of Rev 1.0
            ds = proc.GetTable();
            return ds;
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
    }
}

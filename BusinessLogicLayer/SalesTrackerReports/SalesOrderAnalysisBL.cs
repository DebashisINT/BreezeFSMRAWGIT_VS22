using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesTrackerReports
{
    public class SalesOrderAnalysisBL
    {
        public DataTable GenerateAnalysisSummaryData(string FromDate, string ToDate, string UserID)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSSALESORDERANALYSIS_REPORT");
            proc.AddPara("@MODULE", "PORTAL");
            proc.AddPara("@FROMDATE", FromDate);
            proc.AddPara("@TODATE", ToDate);
            proc.AddPara("@ACTION", "Summary");
            proc.AddPara("@USERID", UserID);
            ds = proc.GetTable();

            return ds;
        }

        public DataTable GenerateAnalysisDetailsData(string FromDate, string ToDate, string UserID)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSSALESORDERANALYSIS_REPORT");
            proc.AddPara("@MODULE", "PORTAL");
            proc.AddPara("@FROMDATE", FromDate);
            proc.AddPara("@TODATE", ToDate);
            proc.AddPara("@ACTION", "Detail");
            proc.AddPara("@USERID", UserID);
            ds = proc.GetTable();

            return ds;
        }
    }
}

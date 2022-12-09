using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesTrackerReports
{
    public class AttendanceRegisterBL
    {
        public DataTable GenerateDetailsData(string FromDate, string ToDate, string UserID)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSATTENDANCEREGISTER_REPORT");
            proc.AddPara("@MODULETYPE", "PORTAL");
            proc.AddPara("@FROMDATE", FromDate);
            proc.AddPara("@TODATE", ToDate);
            proc.AddPara("@ACTION", "Detail");
            proc.AddPara("@USERID", UserID);
            ds = proc.GetTable();

            return ds;
        }
        public DataTable GenerateSummaryData(string FromDate, string ToDate, string UserID)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSATTENDANCEREGISTER_REPORT");
            proc.AddPara("@MODULETYPE", "PORTAL");
            proc.AddPara("@FROMDATE", FromDate);
            proc.AddPara("@TODATE", ToDate);
            proc.AddPara("@ACTION", "Summary");
            proc.AddPara("@USERID", UserID);
            ds = proc.GetTable();

            return ds;
        }
    }
}

using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesTrackerReports
{
    public class RevisitVariance
    {
        public DataTable GetRevisitVarianceSumDetReport(string fromdate, string todate, string userid, string stateID, string desigid, string empid,string rpttype)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSEMPLOYEEREVISITVARIANCESUMMDET_REPORT");

            proc.AddPara("@FROMDATE", fromdate);
            proc.AddPara("@TODATE", todate);
            proc.AddPara("@STATEID", stateID);
            proc.AddPara("@DESIGNID", desigid);
            proc.AddPara("@EMPID", empid);
            proc.AddPara("@REPORTTYPE", rpttype);
            proc.AddPara("@USERID", userid);
            
            ds = proc.GetTable();

            return ds;
        }
    }
}

using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesmanTrack
{
   public class ChemistActivityBL
    {
        public DataTable GetReportChemistActivity(string fromdate, string todate, string userid, string stateID, string userlist)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSCHEMISTACTIVITY_REPORT");

            proc.AddPara("@FROMDATE", fromdate);
            proc.AddPara("@TODATE", todate);
            proc.AddPara("@STATEID", stateID);
            proc.AddPara("@USERID", userid);
            proc.AddPara("@EMPID", userlist);
            ds = proc.GetTable();
            return ds;
        }
    }
}

using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class PlanAchiveReportBL
    {
        public DataTable GetReportPlanAchivement(string fromdate, string todate, string userid, string stateID)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSEMPLOYEEPLAN_REPORT");
            proc.AddPara("@FROM_DATE", fromdate);
            proc.AddPara("@TO_DATE", todate);
            proc.AddPara("@stateID", stateID);
            proc.AddPara("@LOGIN_ID", userid);
            ds = proc.GetTable();
            return ds;
        }
    }
}

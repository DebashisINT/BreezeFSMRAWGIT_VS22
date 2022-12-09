using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesmanTrack
{
    public class EmployeeLateVisit
    {
        //Rev Debashis && BranchId added 0025198
        public void CreateTable(string emp, string deg, string state, string BranchId, DateTime fromdate, DateTime todate, DateTime firsttime, DateTime lasttime, string user_id, bool inactive, bool notlogged, string duration)
        {
            
            ProcedureExecute proc = new ProcedureExecute("PRC_LATEVBISITSUMMARY");
            proc.AddPara("@ACTION", "0");
            proc.AddPara("@fromdate", fromdate);
            proc.AddPara("@todate", todate);
            proc.AddPara("@stateid", state);
            proc.AddPara("@designation", deg);
            proc.AddPara("@employee", emp);
            proc.AddPara("@firstshopvisittime", firsttime);
            proc.AddPara("@lastshopvisittime", lasttime);
            proc.AddPara("@USER_ID", user_id);
            proc.AddPara("@inactive", inactive);
            proc.AddPara("@notlogged", notlogged);
            proc.AddPara("@duration", duration);
            proc.AddPara("@BRANCHID", BranchId);

            proc.GetScalar();

        }
    }
}

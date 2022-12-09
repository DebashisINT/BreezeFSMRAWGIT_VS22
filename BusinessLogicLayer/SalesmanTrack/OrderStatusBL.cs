using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesmanTrack
{
    public class OrderStatusBL
    {
        //Rev Debashis && Add BranchId 0025198
        public DataTable GenerateLocationReportData(string Employee, string FROMDATE, string TODATE, long login_id, string state, string desig, string REPORT_BY, string BranchId)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("FTS_API_OREDER_STATUS_REPORT");
            proc.AddPara("@Employee", Employee);
            proc.AddPara("@FROM_DATE", FROMDATE);
            proc.AddPara("@TO_DATE", TODATE);
            proc.AddPara("@LOGIN_ID", login_id);
            proc.AddPara("@stateID", state);
            proc.AddPara("@DESIGNID", desig);
            proc.AddPara("@REPORT_BY", REPORT_BY);
            proc.AddPara("@BRANCHID", BranchId);
            ds = proc.GetTable();

            return ds;
        }

        public DataTable GetallorderDetails(int OrderID)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Proc_FTS_OrderSummaryDetailsBind");
            proc.AddPara("@OrderID", OrderID);
            ds = proc.GetTable();
            return ds;
        }
    }
}

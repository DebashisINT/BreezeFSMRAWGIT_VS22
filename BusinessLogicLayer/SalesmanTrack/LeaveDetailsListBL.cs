using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesmanTrack
{
    public class LeaveDetailsListBL
    {
        public DataTable GetReportLeaveDetails(string fromdate, string todate, string userid)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSLEAVEDETAILSLIST");

            proc.AddPara("@FROMDATE", fromdate);
            proc.AddPara("@TODATE", todate);
            //proc.AddPara("@STATEID", stateID);
            proc.AddPara("@USERID", userid);
            //proc.AddPara("@EMPID", userlist);
            ds = proc.GetTable();
            return ds;
        }
    }
}

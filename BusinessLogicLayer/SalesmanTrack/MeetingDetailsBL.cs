using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesmanTrack
{
    public class MeetingDetailsBL
    {
        public DataTable GetReportMeetingDetails(string fromdate, string todate, string userid, String stateID, String userlist, String Desgid)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSMeetingDetails_List");
            proc.AddPara("@FROMDATE", fromdate);
            proc.AddPara("@TODATE", todate);
            proc.AddPara("@STATEID", stateID);
            proc.AddPara("@USERID", userid);
            proc.AddPara("@EMPID", userlist);
            proc.AddPara("@DesigId", Desgid);
            ds = proc.GetTable();
            return ds;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataAccessLayer;

namespace BusinessLogicLayer.SalesmanTrack
{
    public class DoctorDetailListBL
    {
        public DataTable GetReportDoctorDetails(string fromdate, string todate, string userid, string stateID, string userlist, String weburl)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSDOCTORDETAILS_REPORT");

            proc.AddPara("@FROMDATE", fromdate);
            proc.AddPara("@TODATE", todate);
            proc.AddPara("@STATEID", stateID);
            proc.AddPara("@USERID", userid);
            proc.AddPara("@EMPID", userlist);
            proc.AddPara("@weburl", weburl);
            ds = proc.GetTable();
            return ds;
        }
    }
}

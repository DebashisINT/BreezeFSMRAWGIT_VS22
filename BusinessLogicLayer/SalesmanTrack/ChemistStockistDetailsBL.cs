using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesmanTrack
{
   public class ChemistStockistDetailsBL
    {
       public DataTable GetReportChemistDetails(string fromdate, string todate, string userid, String stateID, String userlist, String weburl)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSCHEMISTSTOCKISTDETAILS_REPORT");

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

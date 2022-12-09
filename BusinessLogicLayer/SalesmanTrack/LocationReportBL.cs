using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesmanTrack
{
    public class LocationReportBL
    {
        public DataTable GenerateLocationReportData(string Employee, string FROMDATE, string TODATE, long login_id, string stateID, string DESIGNID)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("FTS_API_LOCAL_REPORT");
            proc.AddPara("@Employee", Employee);
            proc.AddPara("@FROM_DATE", FROMDATE);
            proc.AddPara("@TO_DATE", TODATE);
            proc.AddPara("@LOGIN_ID", login_id);
            proc.AddPara("@stateID", stateID);
            proc.AddPara("@DESIGNID", DESIGNID);
            ds = proc.GetTable();

            return ds;
        }

        public DataTable Getdesiglist()
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("FTS_API_Designation_Officer");
            ds = proc.GetTable();
            return ds;
        }
    }
}

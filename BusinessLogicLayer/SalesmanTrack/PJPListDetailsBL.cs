using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesmanTrack
{
    public class PJPListDetailsBL
    {
        public DataTable GetReportPJPDetails(string fromdate, string todate, string userid, String stateID, String userlist)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSPJPLIST_REPORT");

            proc.AddPara("@FROMDATE", fromdate);
            proc.AddPara("@TODATE", todate);
            proc.AddPara("@STATEID", stateID);
            proc.AddPara("@USERID", userid);
            proc.AddPara("@EMPID", userlist);
            ds = proc.GetTable();
            return ds;
        }

        public DataTable GetExportPJPList(String AreaList,String EmpIdList)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSPJPImportInsert");
            proc.AddPara("@EmpID", EmpIdList);
            proc.AddPara("@AREAID", AreaList);
            ds = proc.GetTable();
            return ds;
        }

        public DataTable GetPJPDetailsImport(DataTable userDT, string CREATE_BY)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSImportPJPDetails");
            proc.AddPara("@IMPORT_TABLE", userDT);
            proc.AddPara("@USER_ID", CREATE_BY);
            ds = proc.GetTable();

            return ds;
        }
        
    }
}

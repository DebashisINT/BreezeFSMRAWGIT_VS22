using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesmanTrack
{
    public class PriorityMasterBL
    {
        public DataTable GetPriority(string PriorityID, String PriorityName, String Action)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTS_PriorityInsertUpdate");
            proc.AddPara("@ACTION_TYPE", Action);
            proc.AddPara("@PriorityName", PriorityName);
            proc.AddPara("@PriorityID", PriorityID);
            ds = proc.GetTable();
            return ds;
        }
    }
}

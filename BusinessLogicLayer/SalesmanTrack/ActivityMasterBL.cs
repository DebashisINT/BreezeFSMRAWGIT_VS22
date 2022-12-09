using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesmanTrack
{
    public class ActivityMasterBL
    {
        public DataTable GetActivity(string ActivityID, String ActivityName, String Action)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTS_ActivityInsertUpdate");
            proc.AddPara("@ACTION_TYPE", Action);
            proc.AddPara("@ActivityName", ActivityName);
            proc.AddPara("@ActivityID", ActivityID);
            ds = proc.GetTable();
            return ds;
        }
    }
}

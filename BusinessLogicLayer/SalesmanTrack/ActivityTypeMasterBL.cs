using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesmanTrack
{
    public class ActivityTypeMasterBL
    {
        public DataTable ActivityType(string ActivityID, String ActivityTypeName, String Action,String ActivityTypeID)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTS_ActivityInsertUpdate");
            proc.AddPara("@ACTION_TYPE", Action);
            proc.AddPara("@ActivityTypeName", ActivityTypeName);
            proc.AddPara("@ActivityID", ActivityID);
            proc.AddPara("@ActivityTypeID", ActivityTypeID);
            ds = proc.GetTable();
            return ds;
        }
    }
}

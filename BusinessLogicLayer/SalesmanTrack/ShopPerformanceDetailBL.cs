using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesmanTrack
{
    public class ShopPerformanceDetailBL
    {
        public DataTable GetShopPerfgormanceDetails(string Employee, string start_date, string end_date, string stateID, string DesignationID)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Proc_FTS_OrderPerformanceDetails");
            proc.AddPara("@Employee_id", Employee);
            proc.AddPara("@start_date", start_date);
            proc.AddPara("@end_date", end_date);
            proc.AddPara("@stateID", stateID);
            proc.AddPara("@DesignationID", DesignationID);
            ds = proc.GetTable();
            return ds;
        }
    }
}

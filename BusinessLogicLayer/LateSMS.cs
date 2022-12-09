using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class LateSMS
    {
        public DataSet GenerateTable(DateTime fromdate, DateTime todate, int UserId)
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("PRC_LATESMS_REPORT");
            proc.AddPara("@fromdate", fromdate);
            proc.AddPara("@todate", todate);
            proc.AddPara("@user_Id", UserId);
            ds = proc.GetDataSet();
            return ds;
        }
    }
}

using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesmanTrack
{
    public class ShopOrderRatingBL
    {
        public DataSet GenerateTable(string month, string Year, int UserId)
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("PRC_SHOPRATING_REPORT");
            proc.AddPara("@Month", month);
            proc.AddPara("@Year", Year);
            proc.AddPara("@user_Id", UserId);
            ds = proc.GetDataSet();
            return ds;
        }
    }
}

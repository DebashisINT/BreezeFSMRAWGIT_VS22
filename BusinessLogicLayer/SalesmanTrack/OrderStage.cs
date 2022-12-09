using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BusinessLogicLayer.SalesmanTrack
{
    public class OrderStage
    {
        public DataTable SaveStage(string ordernumber,string stageid)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_ORDERSTAGEUPDATE");
            proc.AddPara("@ACTION", "SAVE");
            proc.AddPara("@ordernumber", ordernumber);
            proc.AddPara("@stageid", stageid);
            proc.AddPara("@USER", Convert.ToString(HttpContext.Current.Session["userid"]));
            ds = proc.GetTable();
            return ds;
        }
    }
}

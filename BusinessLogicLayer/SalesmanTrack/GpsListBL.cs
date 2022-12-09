using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesmanTrack
{
   public  class GpsListBL
    {
       public DataTable GetGpsList(string fromdate, string todate,string userid)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Proc_FTS_GPSStatusList");

            proc.AddPara("@FromDate", fromdate);
            proc.AddPara("@ToDate", todate);
            proc.AddPara("@Action", "portal");
            proc.AddPara("@user_id", userid);

            ds = proc.GetTable();

            return ds;
        }

       public DataTable GetGpsListShop(string fromdate, string todate, string userid)
       {
           DataTable ds = new DataTable();
           ProcedureExecute proc = new ProcedureExecute("Proc_FTS_GPSStatusList");

           proc.AddPara("@FromDate", fromdate);
           proc.AddPara("@ToDate", todate);
           proc.AddPara("@user_id", userid);
           proc.AddPara("@Action", "Listshop");


           ds = proc.GetTable();

           return ds;
       }

       public DataTable GetGpsStatusShop(string fromdate, string todate, string userid,string Action)
       {
           DataTable ds = new DataTable();
           ProcedureExecute proc = new ProcedureExecute("Proc_FTS_GPSStatusList");

           proc.AddPara("@FromDate", fromdate);
           proc.AddPara("@ToDate", todate);
           proc.AddPara("@user_id", userid);
           proc.AddPara("@Action", Action);

           ds = proc.GetTable();

           return ds;
       }

    }
}

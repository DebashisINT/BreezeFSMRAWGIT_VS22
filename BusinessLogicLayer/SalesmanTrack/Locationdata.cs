using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SalesmanTrack
{
    public  class Locationdata
    {
        public DataTable GetLocationList(string userid, string fromdate, string todate, string datespan, int Create_UserId=0)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("[Sp_API_Locationfetch_report]");
            proc.AddPara("@user_id", userid);
            proc.AddPara("@date_span", datespan);
            proc.AddPara("@from_date", fromdate);
            proc.AddPara("@to_date", todate);
            proc.AddPara("@Create_UserId", Create_UserId);
            ds = proc.GetTable();
            return ds;
        }

        public int LocationUpdate(string locationID,string location)
        {
            int s = 0;
            ProcedureExecute proc = new ProcedureExecute("Sp_API_Locationfetch_report");
            proc.AddPara("@locationID", locationID);
            proc.AddPara("@location", location);
            s = proc.RunActionQuery();

            return s;
        }



        public int LocationDistanceInsertion(DataTable locationdatatable,string UserId,string date)
        {
            int s = 0;
            ProcedureExecute proc = new ProcedureExecute("Sp_API_LocationdistanceInsert_report");

            proc.AddPara("@locationdetdatatable", locationdatatable);
            proc.AddPara("@userid", UserId);
            proc.AddPara("@date", date);
            s = proc.RunActionQuery();
            return s;
        }

    }
}

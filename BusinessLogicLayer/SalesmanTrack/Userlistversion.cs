using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesmanTrack
{
   public  class Userlistversion
    {
       public DataTable Getversionuserlist(string userid, int Create_UserId=0)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Sp_API_FTSGetVersionUsers");
            proc.AddPara("@UseriD", userid);
            proc.AddPara("@Create_UserId", Create_UserId);
            ds = proc.GetTable();
            return ds;
        }
    }
}

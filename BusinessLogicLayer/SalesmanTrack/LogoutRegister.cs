using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesmanTrack
{
    public class LogoutRegister
    {
        public DataTable GetLogoutRegister(string Employee, string FromDate, string ToDate, string userID, string State_id, string designation_id)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_LOGOUTREGISTER_REPORT");
            proc.AddPara("@Employee", Employee);
            proc.AddPara("@FROMDATE", FromDate);
            proc.AddPara("@TODATE", ToDate);
            proc.AddPara("@LOGIN_ID", userID);
            proc.AddPara("@stateID", State_id);
            proc.AddPara("@DESIGNID", designation_id);
            ds = proc.GetTable();

            return ds;
        }
        public class LogoutRegisterModel
        {
            public List<string> EmployeeID { get; set; }
            public List<string> State { get; set; }
            public List<string> Designation_id { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public string Ispageload { get; set; }
        }
    }
}

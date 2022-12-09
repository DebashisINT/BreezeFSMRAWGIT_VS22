using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesERP
{
    public class SalesPersontracking
    {

        public DataTable FetchEmployeeFTS(string Action,String User_id="0")
        {

            DataTable ds = new DataTable();


            ProcedureExecute proc = new ProcedureExecute("Proc_FTS_ERP_EmployeeList");
            //  proc.AddVarcharPara("@action", 50, "SelectVehicles");
            //   proc.AddVarcharPara("@InetrnalId", 50, InternalId);
            proc.AddPara("@Action", Action);
            proc.AddPara("@userid", User_id);
            ds = proc.GetTable();
            return ds;

        }


        public static DataTable SubmitSupervisorEmployeeFTS(string fromsuper, string tosupe, string usrid)
        {

            DataTable ds = new DataTable();


            ProcedureExecute proc = new ProcedureExecute("Proc_FTS_ERP_SupervisorChangedSubmit");
            proc.AddPara("@fromsuper", fromsuper);
            proc.AddPara("@tosuper", tosupe);
            proc.AddPara("@User_Id", usrid);

            ds = proc.GetTable();
            return ds;

        }


        public static DataTable SubmitEmployeeState(string EmployeeID, string states, string UsderID)
        {

            DataTable ds = new DataTable();


            ProcedureExecute proc = new ProcedureExecute("PROC_MAPStateemployee");
            proc.AddPara("@EMPID", EmployeeID);
            proc.AddPara("@STATELIST", states);
            proc.AddPara("@UserID", UsderID);

            ds = proc.GetTable();
            return ds;

        }
        public DataTable MobileUserloginIDRellocation(string usrloginID)
        {
            DataTable ds = new DataTable();

            ProcedureExecute proc = new ProcedureExecute("Proc_FTS_ERP_MobileReallocation");
            proc.AddPara("@UserLogin", usrloginID);
            ds = proc.GetTable();
            return ds;

        }
        public DataTable MobileUserloginIDRellocationUpdate(string usrloginID, string userID)
        {
            DataTable ds = new DataTable();

            ProcedureExecute proc = new ProcedureExecute("Proc_FTS_ERP_MobileReallocation");
            proc.AddPara("@UserLogin", usrloginID);
            proc.AddPara("@UserID", userID);
            proc.AddPara("@Action", "Update");
            ds = proc.GetTable();
            return ds;

        }

        public DataTable CheckUserlIsinactive(string usrloginID, string isctive)
        {
            DataTable ds = new DataTable();

            ProcedureExecute proc = new ProcedureExecute("Proc_FTS_ERP_Inactivestatus");
            proc.AddPara("@UserLogin", usrloginID);
            proc.AddPara("@Isinactive", isctive);
            ds = proc.GetTable();
            return ds;

        }

        public static DataTable UpdateEmployeeID(string EmployeeID, string newEmpID, string UsderID)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_EmployeeCodeChange");
            proc.AddPara("@EMPID", EmployeeID);
            proc.AddPara("@newEmpID", newEmpID);
            proc.AddPara("@UserID", UsderID);
            ds = proc.GetTable();
            return ds;
        }
    }
}

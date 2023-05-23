/****************************************************************************************************************************
1.0     v2.0.40     Priti    19/05/2023      0026145:Modification in the ‘Configure Travelling Allowance’ page.
*********************************************************************************************************************************/

using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class TravelConveyanceClass
    {


        public DataTable GetConveyanceTypes(string Action = null, string expensetype = null, string Tsid = null)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Proc_FTS_Getallmasters");

            proc.AddPara("@ExpenseID", expensetype);
            proc.AddPara("@Tcid", Tsid);
            proc.AddPara("@Action", Action);

            dt = proc.GetTable();
            return dt;
        }
        public int GetConveyanceDelete( string Action, string TcID )
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Proc_TravelConveyanceManage");

         
            int i = 0;
            proc.AddPara("@TcID", TcID);
            proc.AddPara("@Action", Action);

            i = proc.RunActionQuery();
            return i;
        }


        public int GetConveyanceInsert(int VisitlocId, int ExpenseId, int DesignationId, int TravelId, int StateId, int EmpgradeId, decimal EligibleDistanc, decimal EligibleRate, decimal EligibleAmtday, int FuelID,string UserID, string Action, bool IsActive,string TcID=null)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Proc_TravelConveyanceManage");

            int i = 0;
            proc.AddPara("@VisitlocId", VisitlocId);
            proc.AddPara("@ExpenseId", ExpenseId);
            proc.AddPara("@DesignationId", DesignationId);
            proc.AddPara("@TravelId", TravelId);
            proc.AddPara("@StateId", StateId);
            proc.AddPara("@EmpgradeId", EmpgradeId);
            proc.AddPara("@EligibleDistanc", EligibleDistanc);
            proc.AddPara("@EligibleRate", EligibleRate);
            proc.AddPara("@EligibleAmtday", EligibleAmtday);
            proc.AddPara("@FuelID", FuelID);
            proc.AddPara("@USERID", UserID);
            proc.AddPara("@Action", Action);
            proc.AddPara("@IsActive", IsActive);
            proc.AddPara("@TcID", TcID);
            i = proc.RunActionQuery();
            return i;
        }



        public int GetConveyanceConfig( DataTable dttravel ,string UserID, string Action, bool IsActive,DataTable dtBranch, DataTable dtArea)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Proc_ReimbesementConfiguration");
            int i = 0;
            proc.AddPara("@Conveyance", dttravel);
            proc.AddPara("@USERID", UserID);
            proc.AddPara("@Action", Action);
            proc.AddPara("@IsActive", IsActive);
            //Rev 1.0
            proc.AddPara("@BranchList", dtBranch);
            proc.AddPara("@AreaList", dtArea);
            //Rev 1.0 End
            //proc.AddPara("@TcID", TcID);
            i = proc.RunActionQuery();
            return i;
        }

    }



}

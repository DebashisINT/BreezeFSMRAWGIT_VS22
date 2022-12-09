using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesTrackerReports
{
    public class ReimbursementAllowanceReport
    {

        public DataTable GetReimbursementAllowanceReport(string stateid, string expensetype, string visitlocation, string employeegrade, string modeoftravel, string fueltype,string action)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_ReimbursementAllowance_Report_Get");
            proc.AddPara("@STATEID", stateid);
            proc.AddPara("@EXPENSETYPE", expensetype);
            proc.AddPara("@VISITLOCATION", visitlocation);

            proc.AddPara("@EMPLOYEEGRADE", employeegrade);
            proc.AddPara("@MODEOFTRAVEL", modeoftravel);
            proc.AddPara("@FUELTYPE", fueltype);
            proc.AddPara("@ACTION", action);
            ds = proc.GetTable();

            return ds;
        }
    }
}

/**************************************************************************************************
 * 1.0      Sanchita    V2.0.38     02/02/2023      Appconfig and User wise setting "IsAllDataInPortalwithHeirarchy = True"
 *                                                  then data in portal shall be populated based on Hierarchy Only. Refer: 25504
 * ****************************************************************************************************/
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

        // Rev 1.0 [ parameter Userid added ]
        public DataTable GetReimbursementAllowanceReport(string stateid, string expensetype, string visitlocation, string employeegrade, string modeoftravel, string fueltype,string action, string Userid)
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
            // Rev 1.0
            proc.AddPara("@USERID", Userid);
            // End of Rev 1.0
            ds = proc.GetTable();

            return ds;
        }
    }
}

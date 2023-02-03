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

namespace BusinessLogicLayer.SalesmanTrack
{
   public class ShopPerformanceSummaryBL
    {
        // Rev 1.0
        //public DataTable GetShopPerfgormanceSummary(string Employee, string start_date, string end_date, string stateID, string DesignationID)
        public DataTable GetShopPerfgormanceSummary(string Employee, string start_date, string end_date, string stateID, string DesignationID, string Userid)
            // End of Rev 1.0
        {
           DataTable ds = new DataTable();
           ProcedureExecute proc = new ProcedureExecute("Proc_FTS_OrderPerformancesummary_shopwise");
           proc.AddPara("@Employee_id", Employee);
           proc.AddPara("@start_date", start_date);
           proc.AddPara("@end_date", end_date);
           proc.AddPara("@stateID", stateID);
           proc.AddPara("@DesignationID", DesignationID);
           // Rev 1.0
           proc.AddPara("@USERID", Userid);
           // End of Rev 1.0
            ds = proc.GetTable();
           return ds;
       }
    }
}

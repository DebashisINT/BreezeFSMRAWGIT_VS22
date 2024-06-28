/**************************************************************************************************
 * 1.0      Sanchita    V2.0.38     02/02/2023      Appconfig and User wise setting "IsAllDataInPortalwithHeirarchy = True"
 *                                                  then data in portal shall be populated based on Hierarchy Only. Refer: 25504
 * 2.0      Sanchita    V2.0.47     29/05/2024      0027405: Colum Chooser Option needs to add for the following Modules                                                 
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
    public class ShopPerformanceDetailBL
    {
        // Rev 1.0 [ parameter Userid added ]
        public DataTable GetShopPerfgormanceDetails(string Employee, string start_date, string end_date, string stateID, string DesignationID, string Userid)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Proc_FTS_OrderPerformanceDetails");
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

        // Rev 2.0
        public int InsertPageRetention(string Col, String USER_ID, String ReportName)
        {
            ProcedureExecute proc = new ProcedureExecute("PRC_FTS_PageRetention");
            proc.AddPara("@Col", Col);
            proc.AddPara("@ReportName", ReportName);
            proc.AddPara("@USER_ID", USER_ID);
            proc.AddPara("@ACTION", "INSERT");
            int i = proc.RunActionQuery();
            return i;
        }

        public DataTable GetPageRetention(String USER_ID, String ReportName)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTS_PageRetention");
            proc.AddPara("@ReportName", ReportName);
            proc.AddPara("@USER_ID", USER_ID);
            proc.AddPara("@ACTION", "DETAILS");
            dt = proc.GetTable();
            return dt;
        }
        // End of Rev 2.0
    }
}

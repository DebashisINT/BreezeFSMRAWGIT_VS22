/*******************************************************************************************************************
 * 1.0      Sanchita    V2.0.38     Appconfig and User wise setting "IsAllDataInPortalwithHeirarchy = True" then data 
 *                                  in portal shall be populated based on Hierarchy Only. Refer: 25504
 * 
 * ***************************************************************************************************************/
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BusinessLogicLayer.SalesmanTrack
{
    public class TargetVsAchivementBL
    {
        public DataTable GetSummaryList(string action, string month, string states,int userid = 0)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_TargetVsAchivement");

            proc.AddPara("@Action", action);
            proc.AddPara("@states", states);
            proc.AddPara("@month", month);
            //proc.AddPara("@stateid", stateid);
            proc.AddPara("@USERID", userid);
            ds = proc.GetTable();
            return ds;
        }

        public DataTable GetYearList()
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_MASTERYEARLIST");
            ds = proc.GetTable();
            return ds;
        }

        public void CreateTable(string month, string states,string year)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSTARGETVSACHIEVEMENTSUMMARY_REPORT");
            proc.AddPara("@STATEID", states);
            proc.AddPara("@MONTH", month);
            proc.AddPara("@USERID", Convert.ToString(HttpContext.Current.Session["userid"]));
            proc.AddPara("@YEAR", year);
            ds = proc.GetTable();
        }
        // Rev 1.0
        //public DataTable GetrangeList(string action, DateTime fromdate,DateTime todate, string states)
        public DataTable GetrangeList(string action, string fromdate, string todate, string states, string userid)
        // End of Rev 1.0
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Prc_TargetvsAchivementDateRange");
            proc.AddPara("@Action", action);
            proc.AddPara("@TODATE", todate);
            proc.AddPara("@FROMDATE", fromdate);
            proc.AddPara("@STATEID", states);
            // Rev 1.0
            proc.AddPara("@USERID", userid);
            // End of Rev 1.0
            ds = proc.GetTable();
            return ds;
        }
        public DataTable GeneratePPDDList(string Month, string Employee_id, string userid,String Year)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSTARGETVSACHIEVEMENTDETAILS_REPORT");
            proc.AddPara("@Month", Month);
            proc.AddPara("@EMPID", Employee_id);
            proc.AddPara("@USERID", userid);
            proc.AddPara("@YEAR", Year);
            ds = proc.GetTable();
            return ds;
        }



    }
}

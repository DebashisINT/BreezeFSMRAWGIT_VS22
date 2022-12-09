using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesTrackerReports
{
    public class Reimbursement_List
    {
        public DataTable GetReimbursementListReport(string month, string userid, string stateID, string desigid, string empid,String year)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSEMPLOYEEREIMBURSEMENTLIST_REPORT");

            proc.AddPara("@MONTH", month);
            proc.AddPara("@STATEID", stateID);
            proc.AddPara("@DESIGNID", desigid);

            proc.AddPara("@USERID", userid);
            proc.AddPara("@EMPID", empid);
            proc.AddPara("@YEAR", year);
            ds = proc.GetTable();

            return ds;
        }

        public DataTable GetReimbursementDetailsReport(string userid, string month,string year)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSREIMBURSEMENTLIST_VIEW_REPORT");

            proc.AddPara("@MONTH", month);
            proc.AddPara("@USERID", userid);
            proc.AddPara("@YEAR", year);
            ds = proc.GetTable();
            return ds;
        }

        public DataTable ReimbursementInsertUpdate(string user_contactId, string ApplicationID, int is_ApprovedReject, decimal Apprvd_Dist, decimal Apprvd_Amt, string App_Rej_Remarks)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_ReimbursementApplication_Verified_InsertUpdate");

            proc.AddPara("@USER_CONTACTID", user_contactId);
            proc.AddPara("@APPLICATIONID", ApplicationID);
            proc.AddPara("@APPRVD_DIST", Apprvd_Dist);
            proc.AddPara("@APPRVD_AMT", Apprvd_Amt);
            proc.AddPara("@IS_ApprovedReject", is_ApprovedReject);
            proc.AddPara("@APP_REJ_REMARKS", App_Rej_Remarks);

            ds = proc.GetTable();
            return ds;
        }


        public DataTable ReimbursementLoadImageDocument(string MapExpenseID)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSREIMBURSEMENTIMAGE_REPORT");

            proc.AddPara("@MapExpenseID", MapExpenseID);
            
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
    }
}

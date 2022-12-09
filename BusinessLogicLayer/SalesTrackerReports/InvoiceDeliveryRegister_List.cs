using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesTrackerReports
{
    public class InvoiceDeliveryRegister_List
    {
        public DataTable GetReimbursementListReport(string month, string userid, string stateID, string desigid, string empid)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSEMPLOYEEREIMBURSEMENTLIST_REPORT");

            proc.AddPara("@MONTH", month);
            proc.AddPara("@STATEID", stateID);
            proc.AddPara("@DESIGNID", desigid);

            proc.AddPara("@USERID", userid);
            proc.AddPara("@EMPID", empid);
            ds = proc.GetTable();

            return ds;
        }

        public DataTable GetReimbursementDetailsReport(string userid, string month)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSREIMBURSEMENTLIST_VIEW_REPORT");

            proc.AddPara("@MONTH", month);
            proc.AddPara("@USERID", userid);
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


        public DataTable ReimbursementLoadImageDocument(string Invoiceno, string userid)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSINVENTORYREGISTER_REPORT");

            proc.AddPara("@invoice_no", Invoiceno);
            proc.AddPara("@User_Id", userid);
            ds = proc.GetTable();
            return ds;
        }

        //Rev Debashis && BranchId added 0025198
        public DataTable GetReportinvoiceRegister(string fromdate, string todate, string userid, string BranchId,string stateID, string ShopID, string userlist)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSINVOICEREGISTER_REPORT");

            proc.AddPara("@FROM_DATE", fromdate);
            proc.AddPara("@TO_DATE", todate);
            proc.AddPara("@STATEID", stateID);
            proc.AddPara("@SHOPID", ShopID);
            proc.AddPara("@LOGIN_ID", userid);
            proc.AddPara("@Employee", userlist);
            proc.AddPara("@BRANCHID", BranchId);
            ds = proc.GetTable();

            return ds;
        }
        //Rev Debashis && BranchId added 0025198
        public DataTable GetReportinvoiceRegister(string fromdate, string todate, string userid, string BranchId, string stateID, string ShopID, string userlist, string isHierarchy)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSINVOICEREGISTER_HIERARCHY_REPORT");

            proc.AddPara("@FROM_DATE", fromdate);
            proc.AddPara("@TO_DATE", todate);
            proc.AddPara("@STATEID", stateID);
            proc.AddPara("@SHOPID", ShopID);
            proc.AddPara("@LOGIN_ID", userid);
            proc.AddPara("@Employee", userlist);
            proc.AddPara("@BRANCHID", BranchId);
            ds = proc.GetTable();

            return ds;
        }


    }
}

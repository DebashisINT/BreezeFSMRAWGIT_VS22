using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesTrackerReports
{
    public class QuotationRegisterBL
    {
        public DataTable GetReportQuotationRegister(string fromdate, string todate, string userid, String deptid, String empcode, String Desgid)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_QuotationRegisterReport");

            proc.AddPara("@FromDate", fromdate);
            proc.AddPara("@TODATE", todate);
            proc.AddPara("@user_id", userid);
            proc.AddPara("@EMPID", empcode);
            proc.AddPara("@deptid", deptid);
            proc.AddPara("@DesigId", Desgid);
            ds = proc.GetTable();
            return ds;
        }
    }
}

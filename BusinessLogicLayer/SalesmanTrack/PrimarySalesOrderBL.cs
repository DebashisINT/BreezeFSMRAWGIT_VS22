using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesmanTrack
{
    public class PrimarySalesOrderBL
    {
        public DataTable GetReportPrimarySalesOrder(string ACTION, string USER_ID, string ORDER_CODE, String SHOP_CODE, String FROM_DATE, String TO_DATE,string Emp_code)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Proc_FTSPrimarySalesOrder");
            proc.AddPara("@ACTION", ACTION);
            proc.AddPara("@USER_ID", USER_ID);
            proc.AddPara("@ORDER_CODE", ORDER_CODE);
            proc.AddPara("@SHOP_CODE", SHOP_CODE);
            proc.AddPara("@FROM_DATE", FROM_DATE);
            proc.AddPara("@TO_DATE", TO_DATE);
            proc.AddPara("@Emp_code", Emp_code);
            ds = proc.GetTable();
            return ds;
        }

        public DataTable GetDate(string ACTION)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Proc_FTSPrimarySalesOrder");
            proc.AddPara("@ACTION", ACTION);
            ds = proc.GetTable();
            return ds;
        }
    }
}

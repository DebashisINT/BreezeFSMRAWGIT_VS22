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
    public class DynamicLayout
    {

        public string SaveLayout(string name, string desc)
        {
            try
            {
                DataTable ds = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_DYNAMIC_LAYOUT");
                proc.AddPara("@action", "SAVELAYOUT");
                proc.AddPara("@name", name);
                proc.AddPara("@desc", desc);
                proc.AddPara("@user_id", Convert.ToString(HttpContext.Current.Session["user_id"]));
                ds = proc.GetTable();
                return "Saved Successfully.";
            }
            catch
            {
                return "Try again later.";

            }
        }

        public string SaveDetails(string key, string result)
        {
            try
            {
                DataTable ds = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_DYNAMIC_LAYOUT");
                proc.AddPara("@action", "SAVEDETAILS");
                proc.AddPara("@key", key);
                proc.AddPara("@result", result);
                proc.AddPara("@user_id", Convert.ToString(HttpContext.Current.Session["user_id"]));
                ds = proc.GetTable();
                return "Saved Successfully.";
            }
            catch
            {
                return "Try again later.";

            }
        }

        public DataTable GetLayout(string key)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_DYNAMIC_LAYOUT");
            proc.AddPara("@action", "GETDETAILS");
            proc.AddPara("@key", key);
            proc.AddPara("@user_id", Convert.ToString(HttpContext.Current.Session["user_id"]));
            ds = proc.GetTable();
            return ds;
        }

        public DataSet GetDynamicData(string key, string fromdate, string todate)
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("PRC_DYNAMIC_LAYOUT");
            proc.AddPara("@action", "GETREPORT");
            proc.AddPara("@key", key);
            proc.AddPara("@fromdate", fromdate);
            proc.AddPara("@todate", todate);
            proc.AddPara("@user_id", Convert.ToString(HttpContext.Current.Session["user_id"]));
            ds = proc.GetDataSet();
            return ds;
        }
    }
}

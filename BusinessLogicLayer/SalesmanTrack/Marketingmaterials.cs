using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesmanTrack
{
    public class Marketingmaterials
    {
        public DataSet GetMarketingDetails()
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("FTS_Report_MarketingDetails");
        //  proc.AddPara("@Month", month);
            ds = proc.GetDataSet();
            return ds;
        }

        public DataTable GetMaterialImagesDetails(string Shop_ID,string weburl)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("FTS_Report_MarketingDetails");
            proc.AddPara("@Action", "MaterialImage");
            proc.AddPara("@Shop_ID", Shop_ID);
            proc.AddPara("@Weburl", weburl);
            ds = proc.GetTable();
            return ds;
        }

    }
}

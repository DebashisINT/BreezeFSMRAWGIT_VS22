using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesmanTrack
{

    public class MarketingLists
    {

        public DataSet Getmarketinglists()
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("API_Getmarketinglists");
            ds = proc.GetDataSet();
            return ds;
        }


    }
}

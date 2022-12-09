using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesmanTrack
{
    public class ProductOpeningStockBL
    {
        public DataSet GetMasterDropdownListAll()
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("PRC_MasterAllList");
            proc.AddPara("@ACTION", "ALL");
            ds = proc.GetDataSet();
            return ds;
        }

        public DataTable GetProductOpeningStockList(String Shop_code)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_ProductOpeningStock");
            proc.AddPara("@Shop_code", Shop_code);
            proc.AddPara("@ACTION", "LIST");
            dt = proc.GetTable();
            return dt;
        }

        public DataTable InsertProductOpeningStock(String ACTION, string WAREHOUSE_ID, Int64 USER_ID,DataTable UDT)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_ProductOpeningStock");
            proc.AddPara("@ACTION", ACTION);
            proc.AddPara("@WAREHOUSE_ID", WAREHOUSE_ID);
            proc.AddPara("@USER_ID", USER_ID);
            proc.AddPara("@UDT_ProductStock", UDT);
            dt = proc.GetTable();
            return dt;
        }

        public DataTable GetProductStockList(String WareHouse_id)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_ProductOpeningStock");
            proc.AddPara("@ACTION", "ExcelProductStock");
            proc.AddPara("@WAREHOUSE_ID", WareHouse_id);
            dt = proc.GetTable();
            return dt;
        }

        public DataTable SetProductStockEmport(DataTable uDT, String User_id)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_ProductOpeningStock");
            proc.AddPara("@ACTION", "ImportExcel");
            proc.AddPara("@USER_ID", User_id);
            proc.AddPara("@UDT_ProductStockExcel", uDT);
            dt = proc.GetTable();
            return dt;
        }
    }
}

using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesmanTrack
{
    public class MasterWarehouseBL
    {
        public DataSet GetMasterDropdownListAll()
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("PRC_MasterAllList");
            proc.AddPara("@ACTION", "ALL");
            ds = proc.GetDataSet();
            return ds;
        }

        public DataTable GetMasterDropdownList(string countryId, string state_id, String actions)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_MasterAllList");
            proc.AddPara("@ACTION", actions);
            proc.AddPara("@countryId", countryId);
            proc.AddPara("@state_id", state_id);
            ds = proc.GetTable();
            return ds;
        }

        public DataTable Masterdatainsert(String action, String WareHouseID, string warehouseName, string address1, String address2, String address3, String country, String State, String City, String Pin, String contactPerson, String ContactPhone, String Distributor, String defaultvalue, String userID)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_MasterWarehouseChanges");
            proc.AddPara("@ACTION", action);
            proc.AddPara("@WAREHOUSE_NAME", warehouseName);
            proc.AddPara("@ADDRESS1", address1);
            proc.AddPara("@ADDRESS2", address2);
            proc.AddPara("@ADDRESS3", address3);
            proc.AddPara("@COUNTRY_ID", country);
            proc.AddPara("@STATE_ID", State);
            proc.AddPara("@DISTRICT", City);
            proc.AddPara("@PIN", Pin);
            proc.AddPara("@CONTACT_NAME", contactPerson);
            proc.AddPara("@CONTACT_PHONE", ContactPhone);
            proc.AddPara("@DISTRIBUTER_CODE", Distributor);
            proc.AddPara("@ISDEFAULT", defaultvalue);
            proc.AddPara("@USER_ID", userID);
            proc.AddPara("@WAREHOUSE_ID", WareHouseID);
            dt = proc.GetTable();
            return dt;
        }

        public DataTable MasterdataList(String action, String userID)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_MasterWarehouseChanges");
            proc.AddPara("@ACTION", action);
            proc.AddPara("@USER_ID", userID);
            dt = proc.GetTable();
            return dt;
        }

        public DataTable MasterdataView(String action, String WAREHOUSE_ID)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_MasterWarehouseChanges");
            proc.AddPara("@ACTION", action);
            proc.AddPara("@WAREHOUSE_ID", WAREHOUSE_ID);
            dt = proc.GetTable();
            return dt;
        }

        public DataTable GetMasterShopList(string Type, String actions)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_MasterAllList");
            proc.AddPara("@ACTION", actions);
            proc.AddPara("@ShopType", Type);
            ds = proc.GetTable();
            return ds;
        }

        public DataTable GetShopListByparam(string Stateid = null, string Action = null, string Type = null)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_MasterAllList");
            proc.AddPara("@STATEID", Stateid);
            proc.AddPara("@ACTION", Action);
            proc.AddPara("@ShopType", Type);
            ds = proc.GetTable();
            return ds;
        }
    }
}

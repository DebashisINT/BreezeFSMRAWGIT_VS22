using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesmanTrack
{
    public class HomeLocationBL
    {
        public DataTable GetShopListEmployeewise(string Employee, string EmployeeAll, string ACTION, int? EMP_ID, int Create_UserId=0)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_API_EMPLOYEEHOMELOCATION_REPORT");
            proc.AddPara("@Employee", Employee);
            proc.AddPara("@EmployeeAll", EmployeeAll);
            proc.AddPara("@ACTION", ACTION);
            proc.AddPara("@EMP_ID", EMP_ID);
            proc.AddPara("@Create_UserId", Create_UserId);
            ds = proc.GetTable();
            return ds;
        }

        //public DataTable HomeLocationGetDetails(string shopid)
        //{
        //    DataTable ds = new DataTable();
        //    ProcedureExecute proc = new ProcedureExecute("PRC_API_EMPLOYEEHOMELOCATION_REPORT");
        //    proc.AddPara("@shopid", shopid);
        //    proc.AddPara("@Action", "ShopDetailsById");
        //    ds = proc.GetTable();
        //    return ds;
        //}

        public DataTable GetStateList()
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("APIStateList");
            ds = proc.GetTable();
            return ds;
        }

        public DataTable GetCityList()
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("APICityList");
            ds = proc.GetTable();
            return ds;
        }

        public DataTable GetCountryList()
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("APICountryList");
            ds = proc.GetTable();
            return ds;
        }

        public int GetHomeLocationDelete(string Action, string USER_ID)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_API_HOMELOCATION_MODIFY");

            int i = 0;
            proc.AddPara("@USER_ID", USER_ID);
            proc.AddPara("@ACTION", Action);

            i = proc.RunActionQuery();
            return i;
        }

        public int GetHomeLocationUpdate(string Action, string address, string Latitude, string  longatude, long UserID, string cityName, string stateName,string PinCode)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_API_HOMELOCATION_MODIFY");

            int i = 0;
            proc.AddPara("@USER_ID", UserID);
            proc.AddPara("@ACTION", Action);
            proc.AddPara("@Latitude", Latitude);
            proc.AddPara("@Longitude", longatude);
            proc.AddPara("@ADDRESS", address);
            proc.AddPara("@CITY", cityName);
            proc.AddPara("@STATE", stateName);
            proc.AddPara("@PINCODE", PinCode);

            i = proc.RunActionQuery();
            return i;
        }
    }
}

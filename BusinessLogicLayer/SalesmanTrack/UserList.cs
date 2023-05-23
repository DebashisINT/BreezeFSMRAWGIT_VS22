/**************************************************************Revision History**************************************************************
*   1.0     v2.0.36     Sanchita    10/01/2023      Appconfig and User wise setting "IsAllDataInPortalwithHeirarchy = True" then 
*                                                   data in portal shall be populated based on Hierarchy Only. Refer: 25504
*   2.0     v2.0.40     Priti       19/05/2023      0026145:Modification in the ‘Configure Travelling Allowance’ page.

 *************************************************************End Revision History************************************************************/

using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SalesmanTrack
{
    public class UserList
    {
        public DataTable GetUserList(string userid)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("API_Salesman_Getuserslist");
            proc.AddPara("@userreportto", userid);
            ds = proc.GetTable();
            return ds;
        }



        public DataTable GetUserList(string userid, string Type)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("API_Salesman_Getuserslist");
            proc.AddPara("@userreportto", userid);
            proc.AddPara("@Type", Type);
            ds = proc.GetTable();
            return ds;
        }


        public DataTable GetDesignationList()
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("SP_API_Designation_Userwise");


            ds = proc.GetTable();
            return ds;
        }

        public DataTable GetStateList()
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("SP_API_State_Userwise");

            ds = proc.GetTable();
            return ds;
        }
        //Rev 2.0
        public DataTable GetBranchList(string StateId)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTS_Branch");
            proc.AddPara("@Action", "AllBranch");
            proc.AddPara("@StateId", StateId);
            ds = proc.GetTable();
            return ds;
        }
        public DataTable GetArealistByBranch(string BranchId)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTS_Area_Userwise");
            proc.AddPara("@Action", "GetAreaByBranch");
            proc.AddPara("@BranchId", BranchId);
            ds = proc.GetTable();
            return ds;
        }
        //Rev 2.0 End
        public DataTable GetDepartmentList()
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Prc_Department_Userwise");           
            ds = proc.GetTable();
            return ds;
        }

        public DataTable GetStateList(string user_id)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("SP_API_State_Userwise");
            proc.AddPara("@user_id", user_id);

            ds = proc.GetTable();
            return ds;
        }

        public DataTable GetStateListCountryWise(String Country)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("SP_FTS_State_Userwise");
            proc.AddPara("@countryId", Country);
            ds = proc.GetTable();
            return ds;
        }

        public DataTable GetCountryList()
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("SP_FTS_Country_Userwise");
            ds = proc.GetTable();
            return ds;
        }

        public DataTable GetShopList()
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("SP_API_SHop_Userwise");
            ds = proc.GetTable();
            return ds;
        }
        public DataTable GetShopListByparam(string Stateid=null,string Action=null)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("SP_API_SHop_Userwise");
            proc.AddPara("@StateID", Stateid);
            proc.AddPara("@Action", Action);
            ds = proc.GetTable();
            return ds;
        }

        public DataTable GetCityListByparam(string Stateid = null, string Action = null)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("SP_FTS_City_Userwise");
            proc.AddPara("@StateID", Stateid);
            proc.AddPara("@Action", Action);
            ds = proc.GetTable();
            return ds;
        }

        public DataTable GetAreaListByparam(string Cityid = null, string Action = null)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTS_Area_Userwise");
            proc.AddPara("@CITYID", Cityid);
            proc.AddPara("@Action", Action);
            ds = proc.GetTable();
            return ds;
        }

        public DataTable GetUserLocationList(string user, string date)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("API_SalesmanLocationcurrent");

            proc.AddPara("@userid", user);
            proc.AddPara("@Action", "Trackpoint");
            proc.AddPara("@Date", date);
            ds = proc.GetTable();
            return ds;
        }

        public DataTable GetUserLocationListRoute(string user, string date)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("API_SalesmanLocationcurrentRoute");

            proc.AddPara("@userid", user);
            proc.AddPara("@Action", "Trackpoint");
            proc.AddPara("@Date", date);
            ds = proc.GetTable();
            return ds;
        }
        public DataTable GetUserLocationListRouteOutlet(string user, string date)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_FSMDashboardDataRoute");

            proc.AddPara("@userid", user);
            proc.AddPara("@Action", "ShopRouteTrack");
            proc.AddPara("@ToDate", date);
            ds = proc.GetTable();
            return ds;
        }
        public DataTable Getdesiglist()
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("SP_API_Designation");
            ds = proc.GetTable();
            return ds;
        }

        public DataTable Getemplist(string StateId, string desig,String userId,String DeptId)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("SP_API_EmployeeList");
            proc.AddPara("@StateId", StateId);
            proc.AddPara("@DesigId", desig);
            //rev Pratik
            //proc.AddPara("@UserId", userId);
            //rev Pratik
            // Rev 1.0
            proc.AddPara("@UserId", userId);
            // End of Rev 1.0
            proc.AddPara("@DeptId", DeptId);
            ds = proc.GetTable();
            return ds;
        }
        public DataTable Getemplist(string StateId, string desig, String userId, String DeptId,string onHierarchy)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PROC_EmployeeList_Hierarchy");
            proc.AddPara("@StateId", StateId);
            proc.AddPara("@DesigId", desig);
            proc.AddPara("@UserId", userId);
            proc.AddPara("@DeptId", DeptId);
            ds = proc.GetTable();
            return ds;
        }

        public DataTable Getemplist(string StateId, string desig)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("SP_API_EmployeeList");
            proc.AddPara("@StateId", StateId);
            proc.AddPara("@DesigId", desig);
            proc.AddPara("@UserId", "");
            ds = proc.GetTable();
            return ds;
        }


        public DataTable GetemplistByParam(string StateId, string desig=null,string shopId=null,string action=null)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("SP_API_EmployeeList");
            proc.AddPara("@StateId", StateId);
            proc.AddPara("@DesigId", desig);
            proc.AddPara("@ShopID", shopId);

            ds = proc.GetTable();
            return ds;
        }



        public DataTable GetUserLocationTrackList(string user, string date)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("API_SalesmanLocationcurrent");

            proc.AddPara("@userid", user);
            proc.AddPara("@Action", "LocationPath");
            proc.AddPara("@Date", date);
            ds = proc.GetTable();
            return ds;
        }


        public DataTable GetShopTypes()
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("proc_FTS_ShopTypes");
            ds = proc.GetTable();
            return ds;
        }
        //Rev work start 16.06.2022 mantise 0024948: Show All checkbox required for Shops report
        public DataTable GetShopTypesData()
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("proc_FTS_ShopTypeData");
            ds = proc.GetTable();
            return ds;
        }
        //Rev work close 16.06.2022 mantise 0024948: Show All checkbox required for Shops report
        public DataTable GetemplistActive(string StateId, string desig)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("SP_API_EmployeeListActive");
            proc.AddPara("@StateId", StateId);
            proc.AddPara("@DesigId", desig);

            ds = proc.GetTable();
            return ds;
        }

        public DataTable GetProductlist()
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("SP_API_PRODUCT");
            ds = proc.GetTable();
            return ds;
        }

        public DataTable GetSupervisorList()
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Prc_SupervisorList");

            ds = proc.GetTable();
            return ds;
        }
        //rev Pratik
        public DataTable GetHeadBranchList(string childbranch, string Action)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Get_AllbranchHO");
            proc.AddPara("@childbranch", childbranch);
            proc.AddPara("@Ation", Action);
            ds = proc.GetTable();
            return ds;
        }
        public DataTable GetChildBranch(string CHILDBRANCH)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FINDCHILDBRANCH_REPORT");
            proc.AddPara("@CHILDBRANCH", CHILDBRANCH);
            dt = proc.GetTable();
            return dt;
        }
        public DataTable GetBranch(string BRANCH_ID, string Ho)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("GetFinancerBranchfetchhowise");
            proc.AddPara("@Branch", BRANCH_ID);
            proc.AddPara("@Hoid", Ho);
            dt = proc.GetTable();
            return dt;
        }
        //End of rev Pratik
    }
}

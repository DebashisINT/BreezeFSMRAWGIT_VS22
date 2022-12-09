using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesmanTrack
{
    public class DBDashboardSettings
    {
        public DataSet GetDashBoardSettingList()
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("prc_DashboardSettingData_Get");
            ds = proc.GetDataSet();
            return ds;
        }

        public DataSet GetUserGroupList()
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("prc_UserGroup_Get");
            ds = proc.GetDataSet();
            return ds;
        }

        public DataSet GetDashBoardSettingByID(Int32 dashboardsettingid)
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("prc_DashBoardSettingByID_Get");
            proc.AddPara("@DASHBOARDSETTINGID", dashboardsettingid);
            ds = proc.GetDataSet();
            return ds;
        }

        public DataSet GetUserList(Int32 usergroup = 0, Int32 User_id = 0)
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("prc_UserList_Get");
            proc.AddPara("@USER_GROUP", usergroup);
            proc.AddPara("@User_id", User_id);
            ds = proc.GetDataSet();
            return ds;
        }

        public DataSet GetDashboardHeaderList()
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("prc_DashboardHeader_Get");
            ds = proc.GetDataSet();
            return ds;
        }

        public DataSet GetDashboardDetailsList()
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("prc_DashboardDetails_Get");
            ds = proc.GetDataSet();
            return ds;
        }


        public DataSet GetDashboardSettingMappedList(Int32 DashboardSettingID)
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("prc_DashboardSettingMapped_Get");
            proc.AddPara("@DASHBOARDSETTINGID", DashboardSettingID);
            ds = proc.GetDataSet();
            return ds;
        }

        public DataSet DashBoardSettingInsertUpdate(Int32 DashboardSettingID, String SettingName, String dataxml, Int32 PermissionLevel, Int32 UserID)
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("prc_DashBoardSetting_InsertUpdate");
            proc.AddPara("@DASHBOARDSETTINGID", DashboardSettingID);
            proc.AddPara("@SETTINGNAME", SettingName);
            proc.AddPara("@XML", dataxml);
            proc.AddPara("@PERMISSIONLEVEL", PermissionLevel);
            proc.AddPara("@USERID", UserID);
            ds = proc.GetDataSet();
            return ds;
        }

        public DataSet GetDashboardSettingMappedListByID(Int32 userid)
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("prc_DashboardSettingMappedListByID_Get");
            proc.AddPara("@USERID", userid);
            ds = proc.GetDataSet();
            return ds;
        }

        public DataTable DashboardSettingMappedRemove(Int32 DashboardSettingID)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_DashboardSetting_Remove");
            proc.AddPara("@DASHBOARDSETTINGID", DashboardSettingID);
            ds = proc.GetTable();
            return ds;
        }
    }
}

using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class DashBoardSetting
    {
        public Int32 DashboardSettingID { get; set; }

        public String SettingName { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public List<UserGroupList> UserGroupList { get; set; }

        public List<GetUsers> UserList { get; set; }

        public List<DashboardHeader> DashboardHeaderList { get; set; }

        public List<DashboardDetails> DashboardDetailsList { get; set; }

        public List<DashboardSettingMapped> DashboardSettingMappedList { get; set; }

        public Int32 PermissionLevel { get; set; }

        public String PermissionLevelText { get; set; }
    }

    public class DashboardHeader
    {
        public Int32 DashboardHeaderID { get; set; }

        public String HeaderName { get; set; }

        public DateTime CreatedDate { get; set; }
    }


    public class DashboardDetails
    {
        public Int32 DashboardDetailsID { get; set; }

        public Int32 FKDashboardHeaderID { get; set; }

        public String DetailsName { get; set; }

        public DateTime CreatedDate { get; set; }
    }


    public class DashboardSettingMapped
    {
        public Int32 DashboardSettingMappedID { get; set; }

        public Int32 FKDashboardSettingID { get; set; }

        public Int32 FKuser_id { get; set; }

        public Int32 FKDashboardDetailsID { get; set; }

        public DateTime CreatedDate { get; set; }

        public String User_Name { get; set; }

        public String DetailsName { get; set; }

        public Int32 PermissionLevel { get; set; }
    }
}
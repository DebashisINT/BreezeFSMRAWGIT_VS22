using BusinessLogicLayer.SalesmanTrack;
using Models;
using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class DashboardSettingController : Controller
    {
        // GET: MYSHOP/DashboardSetting
        public ActionResult Index()
        {
            return View();
        }

        #region For DashBoard Setting List

        [HttpGet]
        public ActionResult DashBoardSetting()
        {
            return View();
        }

        public ActionResult GetDashBoardSettingList()
        {
            DBDashboardSettings dashboarddataobj = new DBDashboardSettings();
            List<DashBoardSetting> dashboardsettingdata = new List<DashBoardSetting>();
            try
            {
                DataSet objData = dashboarddataobj.GetDashBoardSettingList();
                if (objData != null && objData.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = objData.Tables[0];
                    DashBoardSetting data = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        data = new DashBoardSetting();
                        data.DashboardSettingID = Convert.ToInt32(row["DashboardSettingID"]);
                        data.SettingName = Convert.ToString(row["SettingName"]);
                        data.PermissionLevel = Convert.ToInt32(row["PermissionLevel"]);
                        data.PermissionLevelText = Convert.ToString(row["PermissionLevelText"]);
                        data.CreatedDate = Convert.ToDateTime(row["CreatedDate"]);
                        data.ModifiedDate = Convert.ToDateTime(row["ModifiedDate"]);
                        dashboardsettingdata.Add(data);
                    }
                }
            }
            catch { }
            return PartialView("_DashBoardSettingList", dashboardsettingdata);
        }

        public ActionResult GetDashBoardSettingByID(Int32 dashboardsettingid)
        {
            DBDashboardSettings dashboarddataobj = new DBDashboardSettings();
            DashBoardSetting data = new DashBoardSetting();
            try
            {
                DataSet objData = dashboarddataobj.GetDashBoardSettingByID(dashboardsettingid);
                if (objData != null && objData.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = objData.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        data.DashboardSettingID = Convert.ToInt32(row["DashboardSettingID"]);
                        data.SettingName = Convert.ToString(row["SettingName"]);
                        data.PermissionLevel = Convert.ToInt32(row["PermissionLevel"]);
                        data.CreatedDate = Convert.ToDateTime(row["CreatedDate"]);
                        data.ModifiedDate = Convert.ToDateTime(row["ModifiedDate"]);
                    }
                    data.DashboardSettingMappedList = GetDashboardSettingMappedList(data.DashboardSettingID);
                }
                data.UserGroupList = GetUserGroupList();
                data.UserList = GetUserList();
                data.DashboardHeaderList = GetDashboardHeaderList();
                data.DashboardDetailsList = GetDashboardDetailsList();
            }
            catch { }
            return PartialView("_DashBoardSettingByID", data);
        }


        public JsonResult DashBoardSettingInsertUpdate(Int32 DashboardSettingID, String SettingName, Int32 usergroup, String seletedUser, String SubList, Int32 PermissionLevel)
        {
            DBDashboardSettings dashboarddataobj = new DBDashboardSettings();
            Boolean Success = false;
            String dataxml = String.Empty;
            try
            {

                string userid = Session["userid"].ToString();
                String[] UserList = seletedUser.Split('|');
                String[] DetailsNameList = SubList.Split('|');
                
                String xml = "";
                for (int i = 0; i < UserList.Length; i++)
                {
                    if (!String.IsNullOrEmpty(UserList[i].Trim()))
                    {
                        String user_id = "<FKuser_id>" + UserList[i].Trim() + "</FKuser_id>";
                        for (int j = 0; j < DetailsNameList.Length; j++)
                        {
                            if (!String.IsNullOrEmpty(DetailsNameList[j].Trim()))
                            {
                                String DetailsName = "<FKDashboardDetailsID>" + DetailsNameList[j].Trim() + "</FKDashboardDetailsID>";
                                xml = "<data>" + user_id + DetailsName + "</data>";
                                dataxml = dataxml + xml;
                            }
                        }
                    }
                }
                dataxml = "<datalist>" + dataxml + "</datalist>";
                DataSet objData = dashboarddataobj.DashBoardSettingInsertUpdate(DashboardSettingID, SettingName, dataxml, PermissionLevel, Convert.ToInt32(userid));
                if (objData != null && objData.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = objData.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        Success = Convert.ToBoolean(row["Success"]);
                    }
                }
            }
            catch { }
            return Json(Success);
        }

        public JsonResult GetUserListByGroupID(Int32 usergroup)
        {
            var List = GetUserList(usergroup);
            return Json(List);
        }


        public List<UserGroupList> GetUserGroupList()
        {
            DBDashboardSettings dashboarddataobj = new DBDashboardSettings();
            List<UserGroupList> items = new List<UserGroupList>();
            try
            {
                DataSet objData = dashboarddataobj.GetUserGroupList();
                if (objData != null && objData.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = objData.Tables[0];
                    UserGroupList data = null;
                    data = new UserGroupList();
                    data.grp_id = 0;
                    data.grp_name = "Select";
                    items.Add(data);
                    foreach (DataRow row in dt.Rows)
                    {
                        data = new UserGroupList();
                        data.grp_id = Convert.ToInt32(row["grp_id"]);
                        data.grp_name = Convert.ToString(row["grp_name"]);
                        items.Add(data);
                    }
                }
            }
            catch { }
            return items;
        }

        public List<GetUsers> GetUserList(Int32 usergroup = 0)
        {
            DBDashboardSettings dashboarddataobj = new DBDashboardSettings();
            List<GetUsers> items = new List<GetUsers>();
            try
            {
                DataSet objData = dashboarddataobj.GetUserList(usergroup, Convert.ToInt32(Session["userid"]));
                if (objData != null && objData.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = objData.Tables[0];
                    GetUsers data = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        data = new GetUsers();
                        data.UserID = Convert.ToString(row["user_id"]);
                        data.username = Convert.ToString(row["user_name"]);
                        items.Add(data);
                    }
                }
            }
            catch { }
            return items;
        }

        public List<DashboardHeader> GetDashboardHeaderList()
        {
            DBDashboardSettings dashboarddataobj = new DBDashboardSettings();
            List<DashboardHeader> items = new List<DashboardHeader>();
            try
            {
                DataSet objData = dashboarddataobj.GetDashboardHeaderList();
                if (objData != null && objData.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = objData.Tables[0];
                    DashboardHeader data = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        data = new DashboardHeader();
                        data.DashboardHeaderID = Convert.ToInt32(row["DashboardHeaderID"]);
                        data.HeaderName = Convert.ToString(row["HeaderName"]);
                        data.CreatedDate = Convert.ToDateTime(row["CreatedDate"]);
                        items.Add(data);
                    }
                }
            }
            catch { }
            return items;
        }

        public List<DashboardDetails> GetDashboardDetailsList()
        {
            DBDashboardSettings dashboarddataobj = new DBDashboardSettings();
            List<DashboardDetails> items = new List<DashboardDetails>();
            try
            {
                DataSet objData = dashboarddataobj.GetDashboardDetailsList();
                if (objData != null && objData.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = objData.Tables[0];
                    DashboardDetails data = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        data = new DashboardDetails();
                        data.DashboardDetailsID = Convert.ToInt32(row["DashboardDetailsID"]);
                        data.FKDashboardHeaderID = Convert.ToInt32(row["FKDashboardHeaderID"]);
                        data.DetailsName = Convert.ToString(row["DetailsName"]);
                        data.CreatedDate = Convert.ToDateTime(row["CreatedDate"]);
                        items.Add(data);
                    }
                }
            }
            catch { }
            return items;
        }

        public List<DashboardSettingMapped> GetDashboardSettingMappedList(Int32 dashboardsettingid)
        {
            DBDashboardSettings dashboarddataobj = new DBDashboardSettings();
            List<DashboardSettingMapped> items = new List<DashboardSettingMapped>();
            try
            {
                DataSet objData = dashboarddataobj.GetDashboardSettingMappedList(dashboardsettingid);
                if (objData != null && objData.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = objData.Tables[0];
                    DashboardSettingMapped data = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        data = new DashboardSettingMapped();
                        data.DashboardSettingMappedID = Convert.ToInt32(row["DashboardSettingMappedID"]);
                        data.FKDashboardSettingID = Convert.ToInt32(row["FKDashboardSettingID"]);
                        data.FKuser_id = Convert.ToInt32(row["FKuser_id"]);
                        data.FKDashboardDetailsID = Convert.ToInt32(row["FKDashboardDetailsID"]);
                        data.CreatedDate = Convert.ToDateTime(row["CreatedDate"]);
                        data.User_Name = Convert.ToString(row["User_Name"]);
                        data.DetailsName = Convert.ToString(row["DetailsName"]);
                        items.Add(data);
                    }
                }
            }
            catch { }
            return items;
        }


        public JsonResult RemoveDashBoardSettingByID(Int32 dashboardsettingid)
        {
            Boolean Success = false;
            DBDashboardSettings dashboarddataobj = new DBDashboardSettings();
            try
            {
                DataTable dt = dashboarddataobj.DashboardSettingMappedRemove(dashboardsettingid); 
                Success = true;
            }
            catch { }
            return Json(Success);
        }

        #endregion
    }
}
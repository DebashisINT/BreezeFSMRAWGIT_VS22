﻿using BusinessLogicLayer;
using BusinessLogicLayer.MenuBLS;
using BusinessLogicLayer.UserGroupsBLS;
using EntityLayer.CommonELS;
using EntityLayer.MenuHelperELS;
using EntityLayer.UserGroupsEL;
using ERP.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DevExpress.Web;

namespace ERP.OMS.Management.Master
{
    [UserAuthorize("/management/master/root_UserGroups.aspx")]
    public partial class management_master_root_UserGroups : ERP.OMS.ViewState_class.VSPage
    {
        UserGroupBL userGroupBL = new UserGroupBL();
        MenuBL menuBl = new MenuBL();
        DBEngine oDBEngine = new DBEngine(ConfigurationManager.AppSettings["DBConnectionDefault"]);

        public EntityLayer.CommonELS.UserRightsForPage rights = new UserRightsForPage();

        
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //'http://localhost:2957/InfluxCRM/management/testProjectMainPage_employee.aspx'
                string sPath = HttpContext.Current.Request.Url.ToString();
                oDBEngine.Call_CheckPageaccessebility(sPath);
            }
           
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/management/master/root_UserGroups.aspx");
            hdnMessage.Value = "";
            if (!IsPostBack)
            {
              
                BusinessLogicLayer.CommonBLS.CommonBL.CreateUserRightSession("/management/master/root_UserGroups.aspx");
                ResetAll();
                tblCreateModifyForms.Visible = false;

               
                GenerateMenus();

                if (Session["UserGroupUpdateMessage"] != null)
                {
                    hdnMessage.Value = Convert.ToString(Session["UserGroupUpdateMessage"]);
                    Session["UserGroupUpdateMessage"] = null;
                }
                //bindUserGroups();
            }
            bindUserGroups();
           
        }

        
        protected void cmbExport_SelectedIndexChanged(object sender, EventArgs e)
        {
            //bindUserGroups();
            Int32 Filter = int.Parse(drdExport.SelectedItem.Value.ToString());
            switch (Filter)
            {
                case 1:
                    //exporter.WritePdfToResponse();

                    using (MemoryStream stream = new MemoryStream())
                    {
                        exporter.WritePdf(stream);
                        WriteToResponse("ExportFileName", true, "pdf", stream);
                    }
                    //Page.Response.End();
                    break;
                case 2:
                    exporter.WriteXlsToResponse();
                    break;
                case 3:
                    exporter.WriteRtfToResponse();
                    break;
                case 4:
                    exporter.WriteCsvToResponse();
                    break;
            }
        }

        protected void WriteToResponse(string fileName, bool saveAsFile, string fileFormat, MemoryStream stream)
        {
            if (Page == null || Page.Response == null) return;
            string disposition = saveAsFile ? "attachment" : "inline";
            Page.Response.Clear();
            Page.Response.Buffer = false;
            Page.Response.AppendHeader("Content-Type", string.Format("application/{0}", fileFormat));
            Page.Response.AppendHeader("Content-Transfer-Encoding", "binary");
            Page.Response.AppendHeader("Content-Disposition", string.Format("{0}; filename={1}.{2}", disposition, HttpUtility.UrlEncode(fileName).Replace("+", "%20"), fileFormat));
            if (stream.Length > 0)
                Page.Response.BinaryWrite(stream.ToArray());
            //Page.Response.End();
        }

      
        protected void GridUserGroup_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string grp_id = e.Keys[0].ToString();

            UserGroupSaveModel saveModel = new UserGroupSaveModel();
            saveModel.grp_id = Convert.ToInt32(grp_id);
            saveModel.grp_segmentId = 1;
            saveModel.mode = PROC_USP_UserGroups_Modes.DELETE.ToString();

            CommonResult stat = userGroupBL.SaveUserGroupData(saveModel);

            //e.Cancel = true;

            ResetAll();
            tblCreateModifyForms.Visible = false;

            hdnMessage.Value = stat.Message;

            Session["UserGroupUpdateMessage"] = stat.Message;

            bindUserGroups();
           
        }

        protected void GridUserGroup_PageIndexChanged(object sender, EventArgs e)
        {

            int pageIndex = (sender as ASPxGridView).PageIndex;
            GridUserGroup.PageIndex = pageIndex;
            bindUserGroups();

            //  GridUserGroup.PageIndex = e.ToString
        }

        protected void GridUserGroup_BeforeGetCallbackResult(object sender, EventArgs e)
        {
            //bindUserGroups();
        }

        protected void btn_Add_New_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmAddUserGroup.aspx", false);
            ////ResetAll();
            ////tblCreateModifyForms.Visible = true;
        }
      
        protected void GridUserGroup_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
        {
           // ResetAll();
            string grp_id = Convert.ToString(e.KeyValue);
            string commanName = e.CommandArgs.CommandName;

            if (commanName == "edit")
            {
                Session["GroupId"] = grp_id;
                string GroupName = GridUserGroup.GetRowValuesByKeyValue(e.KeyValue, "grp_name").ToString();
                txtGroupName.Text = GroupName;

                Session["GroupName"] = GroupName;
              //  GetSetGroupAccessValues(Convert.ToInt32(grp_id));
               
                Response.Redirect("frmAddUserGroup.aspx");
                //tblCreateModifyForms.Visible = true;
            }
            else if (commanName == "delete")
            {

                string[,] acccode = oDBEngine.GetFieldValue("tbl_master_user",
                              "user_group", "user_group=(select grp_id from tbl_master_userGroup where grp_id='" + grp_id + "')", 1);

                if (acccode[0, 0] != "n")
                {
                    string message = "alert('Used in other modules. Cannot Delete.')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "script", message, true);
                    //gridStatus.JSProperties["cpDelmsg"] = "Used in other modules. Cannot Delete.";
                }
                else
                {
                    UserGroupSaveModel saveModel = new UserGroupSaveModel();
                    saveModel.grp_id = Convert.ToInt32(grp_id);
                    saveModel.grp_segmentId = 1;
                    saveModel.mode = PROC_USP_UserGroups_Modes.DELETE.ToString();

                    CommonResult stat = userGroupBL.SaveUserGroupData(saveModel);

                    ResetAll();

                    if (stat.IsSuccess)
                    {
                        BusinessLogicLayer.CommonBLS.CommonBL.DestroyUserRightSession();
                        rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/management/master/root_UserGroups.aspx");
                    }

                    bindUserGroups();

                    hdnMessage.Value = stat.Message;
                    string message = "alert('Deleted Successfully')";
                    //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "script", message, true);
                }
              
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                int? userId = null;

                if (Session["userid"] != null)
                {
                    try
                    {
                        userId = Convert.ToInt32(Session["userid"]);
                    }
                    catch
                    {
                        userId = null;
                    }
                }

                UserGroupSaveModel saveModel = new UserGroupSaveModel();
                saveModel.grp_name = txtGroupName.Text.Trim();
                saveModel.grp_segmentId = 1;
                saveModel.UserGroupRights = GroupUserRights.Value.Trim();
                saveModel.CreateUser = userId;
                saveModel.LastModifyUser = userId;

                if (Session["GroupId"] != null)
                {
                    try
                    {
                        saveModel.grp_id = Convert.ToInt32(Session["GroupId"]);
                        saveModel.mode = PROC_USP_UserGroups_Modes.UPDATE.ToString();
                    }
                    catch
                    {
                        saveModel.grp_id = 0;
                        saveModel.mode = PROC_USP_UserGroups_Modes.INSERT.ToString();
                    }
                }
                else
                {
                    saveModel.mode = PROC_USP_UserGroups_Modes.INSERT.ToString();
                }


                CommonResult stat = userGroupBL.SaveUserGroupData(saveModel);

                if (stat.IsSuccess)
                {
                    ResetAll();
                    tblCreateModifyForms.Visible = false;

                    BusinessLogicLayer.CommonBLS.CommonBL.DestroyUserRightSession();
                    rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/management/master/root_UserGroups.aspx");

                    bindUserGroups();
                }

                hdnMessage.Value = stat.Message;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ResetAll();
            tblCreateModifyForms.Visible = false;
        }

        #region Private Usercontrol Methods

        private void bindUserGroups()
        {
            GridUserGroup.DataSource = userGroupBL.FetchAllGroupsDataTable().DefaultView;
            GridUserGroup.DataBind();
        }

        private void GenerateMenus()
        {
            List<MenuEL> AllMenu = menuBl.GetAllMenus(1);
              List<RightEL> rights = menuBl.GetAllRights();
            if (AllMenu != null && AllMenu.Count() > 0)
            {
                string MenuTreeString = "<ul id=\"ulMenuTree\">";

                //---------------------------Body-----------------------


                List<MenuEL> ParentMenus = AllMenu.Where(t => t.mun_parentId == 0).ToList();
               
                foreach (MenuEL pMenus in ParentMenus.ToList())
                {
                    List<MenuEL> level1Menus = AllMenu.Where(t => t.mun_parentId == pMenus.mnu_id).ToList();

                    MenuTreeString += "<li id=\"0\">";
                    MenuTreeString += "<span>" + pMenus.mnu_menuName + "</span>";
                    if (level1Menus != null && level1Menus.Count() > 0)
                    {
                        MenuTreeString += "<ul>";
                        foreach (MenuEL lvl1 in level1Menus)
                        {
                            List<MenuEL> level2Menus = AllMenu.Where(t => t.mun_parentId == lvl1.mnu_id).ToList();

                            bool stat = !string.IsNullOrWhiteSpace(lvl1.mnu_menuLink) ? true : false;

                            MenuTreeString += "<li id=\"" + ((stat) ? lvl1.mnu_id : 0) + "\">";
                            MenuTreeString += "<span><div style=\"float:left\">" + lvl1.mnu_menuName + "</div>";

                            if (stat)
                            {
                                 List<RightEL> allowedRights = menuBl.GetRights(lvl1.RightsToCheck, rights);

                                                        if (allowedRights == null || allowedRights.Count() <= 0)
                                                        {
                                                            allowedRights = new List<RightEL>();
                                                        }
                                                      
                                                          MenuTreeString += "<span style=\"position:relative;left:16px;\">";
                                                            foreach (var item in rights)
                                                            {
                                                                if (allowedRights.Where(t => t.Id == item.Id).Count() > 0)
                                                                {                                                                  
                                                                    MenuTreeString += "<input type=\"checkbox\" class=\"chckRights\" data-id=\"" + item.Id + "\" data-menuid=\"" + lvl1.mnu_id + "\" />&nbsp;"+ item.Rights+"&nbsp;";
                                                                 
                                                                }
                                                                else
                                                                {
                                                                    MenuTreeString += "<input type=\"checkbox\" class=\"chckRights\"  data-id=\"" + item.Id + "\" data-menuid=\"" + lvl1.mnu_id + "\"   style=\"visibility:hidden\"/><label style=\"visibility:hidden\">&nbsp;" + item.Rights + "&nbsp;</label>";
                                                                 
                                                                }
                                                            }
                                                       MenuTreeString += "</span>";
                                                        
                                //MenuTreeString += "<span style=\"position:relative;left:16px;\">";

                                //MenuTreeString += "<input type=\"checkbox\" class=\"chckRights\" data-id=\"1\" data-menuid=\"" + lvl1.mnu_id + "\" />&nbsp;Add&nbsp;";
                                //MenuTreeString += "<input type=\"checkbox\" class=\"chckRights\" data-id=\"2\" data-menuid=\"" + lvl1.mnu_id + "\" />&nbsp;Modify&nbsp;";
                                //MenuTreeString += "<input type=\"checkbox\" class=\"chckRights\" data-id=\"3\" data-menuid=\"" + lvl1.mnu_id + "\" />&nbsp;Delete&nbsp;";
                                //MenuTreeString += "<input type=\"checkbox\" class=\"chckRights\" data-id=\"4\" data-menuid=\"" + lvl1.mnu_id + "\" />&nbsp;View&nbsp;";

                                //MenuTreeString += "</span>";
                            }

                            MenuTreeString += "</span>";

                            if (level2Menus != null && level2Menus.Count() > 0)
                            {


                                MenuTreeString += "<ul>";
                                foreach (MenuEL lvl2 in level2Menus)
                                {

                                    List<RightEL> allowedRights = menuBl.GetRights(lvl2.RightsToCheck, rights);

                                    if (allowedRights == null || allowedRights.Count() <= 0)
                                    {
                                        allowedRights = new List<RightEL>();
                                    }
                                    MenuTreeString += "<li id=\"" + lvl2.mnu_id + "\">";

                                    MenuTreeString += "<span><div style=\"float:left\">" + lvl2.mnu_menuName + "</div>";

                                    MenuTreeString += "<span style=\"position:relative;left:16px;\">";

                                    foreach (var item in rights)
                                    {
                                        if (allowedRights.Where(t => t.Id == item.Id).Count() > 0)
                                        {
                                            MenuTreeString += "<input type=\"checkbox\" class=\"chckRights\" data-id=\"" + item.Id + "\" data-menuid=\"" + lvl2.mnu_id + "\" />&nbsp;" + item.Rights + "&nbsp;";

                                        }
                                        else
                                        {
                                            MenuTreeString += "<input type=\"checkbox\" class=\"chckRights\"  data-id=\"" + item.Id + "\" data-menuid=\"" + lvl2.mnu_id + "\"   style=\"visibility:hidden\"/><label style=\"visibility:hidden\">&nbsp;" + item.Rights + "&nbsp;</label>";

                                        }
                                    }

                                    //MenuTreeString += "<input type=\"checkbox\" class=\"chckRights\" data-id=\"1\" data-menuid=\"" + lvl2.mnu_id + "\" />&nbsp;Add&nbsp;";
                                    //MenuTreeString += "<input type=\"checkbox\" class=\"chckRights\" data-id=\"2\" data-menuid=\"" + lvl2.mnu_id + "\" />&nbsp;Modify&nbsp;";
                                    //MenuTreeString += "<input type=\"checkbox\" class=\"chckRights\" data-id=\"3\" data-menuid=\"" + lvl2.mnu_id + "\" />&nbsp;Delete&nbsp;";
                                    //MenuTreeString += "<input type=\"checkbox\" class=\"chckRights\" data-id=\"4\" data-menuid=\"" + lvl2.mnu_id + "\" />&nbsp;View&nbsp;";

                                    MenuTreeString += "</span>";

                                    MenuTreeString += "</span>";

                                    MenuTreeString += "</li>";
                                }
                                MenuTreeString += "</ul>";
                            }

                            MenuTreeString += "</li>";
                        }
                        MenuTreeString += "</ul>";
                    }
                    MenuTreeString += "</li>";
                }

                //---------------------------Body-----------------------



                MenuTreeString += "</ul>";

                dvTreeMenus.InnerHtml = MenuTreeString;
            }
        }

        private void GetSetGroupAccessValues(int GroupId)
        {
            List<TranAccessByGroupModel> accessList = userGroupBL.GetTranAccessByGroup(GroupId);

            if (accessList != null && accessList.Count() > 0)
            {
                string UserGroupRightsString = "";
                foreach (TranAccessByGroupModel model in accessList)
                {
                    string TempString = "";


                    if (model.CanAdd || model.CanEdit || model.CanDelete || model.CanView || model.CanIndustry || model.CanCreateActivity || model.CanContactPerson || model.CanHistory || model.CanAddUpdateDocuments || model.CanMembers || model.CanOpeningAddUpdate || model.CanAssetDetails)
                    {
                        if (model.CanAdd)
                        {
                            if (!string.IsNullOrWhiteSpace(TempString))
                            {
                                TempString += "|1";
                            }
                            else
                            {
                                TempString += "1";
                            }
                        }

                        if (model.CanEdit)
                        {
                            if (!string.IsNullOrWhiteSpace(TempString))
                            {
                                TempString += "|2";
                            }
                            else
                            {
                                TempString += "2";
                            }
                        }

                        if (model.CanDelete)
                        {
                            if (!string.IsNullOrWhiteSpace(TempString))
                            {
                                TempString += "|3";
                            }
                            else
                            {
                                TempString += "3";
                            }
                        }

                        if (model.CanView)
                        {
                            if (!string.IsNullOrWhiteSpace(TempString))
                            {
                                TempString += "|4";
                            }
                            else
                            {
                                TempString += "4";
                            }
                        }
                        if (model.CanIndustry)
                        {
                            if (!string.IsNullOrWhiteSpace(TempString))
                            {
                                TempString += "|6";
                            }
                            else
                            {
                                TempString += "6";
                            }
                        }
                        if (model.CanCreateActivity)
                        {
                            if (!string.IsNullOrWhiteSpace(TempString))
                            {
                                TempString += "|5";
                            }
                            else
                            {
                                TempString += "5";
                            }
                        }

                        if (model.CanContactPerson)
                        {
                            if (!string.IsNullOrWhiteSpace(TempString))
                            {
                                TempString += "|7";
                            }
                            else
                            {
                                TempString += "7";
                            }
                        }

                        if (model.CanHistory)
                        {
                            if (!string.IsNullOrWhiteSpace(TempString))
                            {
                                TempString += "|8";
                            }
                            else
                            {
                                TempString += "8";
                            }
                        }
                        if (model.CanAddUpdateDocuments)
                        {
                            if (!string.IsNullOrWhiteSpace(TempString))
                            {
                                TempString += "|9";
                            }
                            else
                            {
                                TempString += "9";
                            }
                        }
                        if (model.CanMembers)
                        {
                            if (!string.IsNullOrWhiteSpace(TempString))
                            {
                                TempString += "|10";
                            }
                            else
                            {
                                TempString += "10";
                            }
                        }
                        if (model.CanOpeningAddUpdate)
                        {
                            if (!string.IsNullOrWhiteSpace(TempString))
                            {
                                TempString += "|11";
                            }
                            else
                            {
                                TempString += "11";
                            }
                        }
                        if (model.CanAssetDetails)
                        {
                            if (!string.IsNullOrWhiteSpace(TempString))
                            {
                                TempString += "|12";
                            }
                            else
                            {
                                TempString += "12";
                            }
                        }
                        TempString = model.MenuId + "^" + TempString;
                    }

                    if (!string.IsNullOrWhiteSpace(TempString))
                    {
                        if (!string.IsNullOrWhiteSpace(UserGroupRightsString))
                        {
                            UserGroupRightsString += "_" + TempString;
                        }
                        else
                        {
                            UserGroupRightsString = TempString;
                        }
                    }
                }

                GroupUserRights.Value = UserGroupRightsString;
            }
        }

        private void ResetAll()
        {
            if (Session["GroupId"] != null)
            {
                Session["GroupId"] = null;
            }

            GroupUserRights.Value = "";
            txtGroupName.Text = "";
            hdnMessage.Value = "";
        }

        #endregion

  
    }
}
/*************************************************************************************************************
Rev 1.0     Sanchita   V2.0.28    27/01/2023      Bulk modification feature is required in Parties menu. Refer: 25609
Rev 2.0     Sanchita   V2.0.44    19/12/2023      Beat related tab will be added in the security roles of Parties. Mantis: 27080  
Rev 3.0     Sanchita   V2.0.47    30/05/2024      Mass Delete related tabs will be added in the security roles of Parties. Mantis: 27489
Rev 4.0     Sanchita   V2.0.47    03/06/2024      27500: Attendance/ Leave Clear tab need to add in security Role of "Users"
Rev 5.0     Sanchuta   V2.0.49    17/09/2024      27698: Customization work of New Order Status Update module  
*****************************************************************************************************************/
using BusinessLogicLayer;
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

namespace ERP.OMS.Management.Master
{
    public partial class frmAddUserGroup : ERP.OMS.ViewState_class.VSPage//System.Web.UI.Page
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

                // tblCreateModifyForms.Visible = false;
                if (Convert.ToString(Session["GroupId"]) != null && Convert.ToString(Session["GroupId"]) != "")
                {
                    txtGroupName.Text = Convert.ToString(Session["GroupName"]);

                    GetSetGroupAccessValues(Convert.ToInt32(Session["GroupId"]));
                }
                GenerateMenus();

                if (Session["UserGroupUpdateMessage"] != null)
                {
                    hdnMessage.Value = Convert.ToString(Session["UserGroupUpdateMessage"]);
                    Session["UserGroupUpdateMessage"] = null;
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
                    Response.Redirect("root_UserGroups.aspx", false);
                    //bindUserGroups();
                }

                hdnMessage.Value = stat.Message;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("root_UserGroups.aspx", false);
            //ResetAll();
            //tblCreateModifyForms.Visible = false;
        }

        #region Private Usercontrol Methods



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
                                //MenuTreeString += "<span >";
                                MenuTreeString += "<span style=\"position:relative;left:16px;\">";
                                foreach (var item in rights)
                                {
                                    if (allowedRights.Where(t => t.Id == item.Id).Count() > 0)
                                    {

                                        MenuTreeString += "<input type=\"checkbox\" class=\"chckRights\" data-id=\"" + item.Id + "\" data-menuid=\"" + lvl1.mnu_id + "\" />&nbsp;" + item.Rights + "&nbsp;";

                                    }
                                    else
                                    {

                                        MenuTreeString += "<input type=\"checkbox\" class=\"chckRights\" data-id=\"" + item.Id + "\" data-menuid=\"" + lvl1.mnu_id + "\"   style=\"visibility:hidden\"/><label style=\"visibility:hidden\">&nbsp;" + item.Rights + "&nbsp;</label>";

                                    }
                                }
                                MenuTreeString += "</span>";
                                //MenuTreeString += "<span style=\"position:relative;left:31px;\">";

                                //MenuTreeString += "<input type=\"checkbox\" class=\"chckRights\" data-id=\"1\" data-menuid=\"" + lvl1.mnu_id + "\" />&nbsp;Add&nbsp;&nbsp;";
                                //MenuTreeString += "<input type=\"checkbox\" class=\"chckRights\" data-id=\"2\" data-menuid=\"" + lvl1.mnu_id + "\" />&nbsp;Modify&nbsp;&nbsp;";
                                //MenuTreeString += "<input type=\"checkbox\" class=\"chckRights\" data-id=\"3\" data-menuid=\"" + lvl1.mnu_id + "\" />&nbsp;Delete&nbsp;&nbsp;";
                                //MenuTreeString += "<input type=\"checkbox\" class=\"chckRights\" data-id=\"4\" data-menuid=\"" + lvl1.mnu_id + "\" />&nbsp;View&nbsp;&nbsp;";

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

                                    ////MenuTreeString += "<span style=\"position:relative;left:16px;\">";
                                    MenuTreeString += "<span >";

                                    foreach (var item in rights)
                                    {
                                        if (allowedRights.Where(t => t.Id == item.Id).Count() > 0)
                                        {
                                            MenuTreeString += "<input type=\"checkbox\" class=\"chckRights\" data-id=\"" + item.Id + "\" data-menuid=\"" + lvl2.mnu_id + "\" />&nbsp;" + item.Rights + "&nbsp;";

                                        }
                                        else
                                        {
                                            //Rev 2.0
                                            //MenuTreeString += "<input type=\"checkbox\" class=\"chckRights\" data-id=\"" + item.Id + "\" data-menuid=\"" + lvl2.mnu_id + "\"   style=\"visibility:hidden\"/><label style=\"visibility:hidden\">&nbsp;" + item.Rights + "&nbsp;</label>";
                                            MenuTreeString += "<input type=\"checkbox\" class=\"chckRights\" data-id=\"" + item.Id + "\" data-menuid=\"" + lvl2.mnu_id + "\"   style=\"display: none\"/><label style=\"display: none\">&nbsp;" + item.Rights + "&nbsp;</label>";
                                            //Rev end 2.0
                                        }
                                    }

                                    //MenuTreeString += "<input type=\"checkbox\" class=\"chckRights\" data-id=\"1\" data-menuid=\"" + lvl2.mnu_id + "\" />&nbsp;Add&nbsp;&nbsp;";
                                    //MenuTreeString += "<input type=\"checkbox\" class=\"chckRights\" data-id=\"2\" data-menuid=\"" + lvl2.mnu_id + "\" />&nbsp;Modify&nbsp;&nbsp;";
                                    //MenuTreeString += "<input type=\"checkbox\" class=\"chckRights\" data-id=\"3\" data-menuid=\"" + lvl2.mnu_id + "\" />&nbsp;Delete&nbsp;&nbsp;";
                                    //MenuTreeString += "<input type=\"checkbox\" class=\"chckRights\" data-id=\"4\" data-menuid=\"" + lvl2.mnu_id + "\" />&nbsp;View&nbsp;&nbsp;";

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

                    //Mantis Issue 24832
                    //if (model.CanAdd || model.CanEdit || model.CanDelete || model.CanView || model.CanIndustry || model.CanCreateActivity || model.CanContactPerson || model.CanHistory || model.CanAddUpdateDocuments || model.CanMembers || model.CanOpeningAddUpdate || model.CanAssetDetails || model.CanExport || model.CanPrint || model.CanBudget || model.CanAssignbranch || model.Cancancelassignmnt || model.CanReassign || model.CanClose || model.CanCancel)
                    // Rev 1.0
                    //if (model.CanAdd || model.CanEdit || model.CanDelete || model.CanView || model.CanIndustry || model.CanCreateActivity || model.CanContactPerson || model.CanHistory || model.CanAddUpdateDocuments || model.CanMembers || model.CanOpeningAddUpdate || model.CanAssetDetails || model.CanExport || model.CanPrint || model.CanBudget || model.CanAssignbranch || model.Cancancelassignmnt || model.CanReassign || model.CanClose || model.CanCancel || model.CanAssign)
                    // Rev 2.0
                    //if (model.CanAdd || model.CanEdit || model.CanDelete || model.CanView || model.CanIndustry || model.CanCreateActivity || model.CanContactPerson || model.CanHistory || model.CanAddUpdateDocuments || model.CanMembers || model.CanOpeningAddUpdate || model.CanAssetDetails || model.CanExport || model.CanPrint || model.CanBudget || model.CanAssignbranch || model.Cancancelassignmnt || model.CanReassign || model.CanClose || model.CanCancel || model.CanAssign || model.CanBulkUpdate)
                    if (model.CanAdd || model.CanEdit || model.CanDelete || model.CanView || model.CanIndustry || model.CanCreateActivity || 
                        model.CanContactPerson || model.CanHistory || model.CanAddUpdateDocuments || model.CanMembers || model.CanOpeningAddUpdate || 
                        model.CanAssetDetails || model.CanExport || model.CanPrint || model.CanBudget || model.CanAssignbranch || model.Cancancelassignmnt || 
                        model.CanReassign || model.CanClose || model.CanCancel || model.CanAssign || model.CanBulkUpdate || 
                        model.CanReassignedAreaRouteBeat || model.CanReassignedAreaRouteBeatLog || model.CanReassignedBeatParty || model.CanReassignedBeatPartyLog
                        // Rev 3.0
                        || model.CanMassDelete || model.CanMassDeleteDownloadImport
                        // End of Rev 3.0
                        // Rev 4.0
                        || model.CanAttendanceLeaveClear
                        // End of Rev 4.0
                        // Rev 5.0
                        || model.CanInvoice || model.CanReadyToDispatch || model.CanDispatch || model.CanDeliver
                        // End of Rev 5.0
                        )
                    // End of Rev 2.0
                    // End of Rev 1.0
                    //End of Mantis Issue 24832
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

                       

                        if (model.CanView)
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

                        if (model.CanEdit)
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

                        if (model.CanDelete)
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
                        if (model.CanExport)
                        {
                            if (!string.IsNullOrWhiteSpace(TempString))
                            {
                                TempString += "|13";
                            }
                            else
                            {
                                TempString += "13";
                            }
                        }
                        if (model.CanPrint)
                        {
                            if (!string.IsNullOrWhiteSpace(TempString))
                            {
                                TempString += "|14";
                            }
                            else
                            {
                                TempString += "14";
                            }
                        }

                        if (model.CanBudget)
                        {
                            if (!string.IsNullOrWhiteSpace(TempString))
                            {
                                TempString += "|15";
                            }
                            else
                            {
                                TempString += "15";
                            }
                        }

                        if (model.CanAssignbranch)
                        {
                            if (!string.IsNullOrWhiteSpace(TempString))
                            {
                                TempString += "|16";
                            }
                            else
                            {
                                TempString += "16";
                            }
                        }

                        if (model.Cancancelassignmnt)
                        {
                            if (!string.IsNullOrWhiteSpace(TempString))
                            {
                                TempString += "|17";
                            }
                            else
                            {
                                TempString += "17";
                            }
                        }
                        if (model.CanReassign)
                        {
                            if (!string.IsNullOrWhiteSpace(TempString))
                            {
                                TempString += "|18";
                            }
                            else
                            {
                                TempString += "18";
                            }
                        }
                        if (model.CanClose)
                        {
                            if (!string.IsNullOrWhiteSpace(TempString))
                            {
                                TempString += "|19";
                            }
                            else
                            {
                                TempString += "19";
                            }
                        }
                        if (model.CanSpecialEdit)
                        {
                            if (!string.IsNullOrWhiteSpace(TempString))
                            {
                                TempString += "|20";
                            }
                            else
                            {
                                TempString += "20";
                            }
                        }
                        if (model.CanCancel)
                        {
                            if (!string.IsNullOrWhiteSpace(TempString))
                            {
                                TempString += "|21";
                            }
                            else
                            {
                                TempString += "21";
                            }
                        }
                        //Mantis Issue 24832
                        if (model.CanAssign)
                        {
                            if (!string.IsNullOrWhiteSpace(TempString))
                            {
                                TempString += "|22";
                            }
                            else
                            {
                                TempString += "22";
                            }
                        }
                        //End of Mantis Issue 24832
                        // Rev 1.0
                        if (model.CanBulkUpdate)
                        {
                            if (!string.IsNullOrWhiteSpace(TempString))
                            {
                                TempString += "|25";
                            }
                            else
                            {
                                TempString += "25";
                            }
                        }
                        // End of Rev 1.0
                        // Rev 2.0
                        if (model.CanReassignedBeatParty)
                        {
                            if (!string.IsNullOrWhiteSpace(TempString))
                            {
                                TempString += "|26";
                            }
                            else
                            {
                                TempString += "26";
                            }
                        }
                        if (model.CanReassignedBeatPartyLog)
                        {
                            if (!string.IsNullOrWhiteSpace(TempString))
                            {
                                TempString += "|27";
                            }
                            else
                            {
                                TempString += "27";
                            }
                        }
                        if (model.CanReassignedAreaRouteBeat)
                        {
                            if (!string.IsNullOrWhiteSpace(TempString))
                            {
                                TempString += "|28";
                            }
                            else
                            {
                                TempString += "28";
                            }
                        }
                        if (model.CanReassignedAreaRouteBeatLog)
                        {
                            if (!string.IsNullOrWhiteSpace(TempString))
                            {
                                TempString += "|29";
                            }
                            else
                            {
                                TempString += "29";
                            }
                        }
                        // End of Rev 2.0
                        // Rev 3.0
                        if (model.CanMassDelete)
                        {
                            if (!string.IsNullOrWhiteSpace(TempString))
                            {
                                TempString += "|30";
                            }
                            else
                            {
                                TempString += "30";
                            }
                        }
                        if (model.CanMassDeleteDownloadImport)
                        {
                            if (!string.IsNullOrWhiteSpace(TempString))
                            {
                                TempString += "|31";
                            }
                            else
                            {
                                TempString += "31";
                            }
                        }
                        // End of Rev 3.0
                        // Rev 4.0
                        if (model.CanAttendanceLeaveClear)
                        {
                            if (!string.IsNullOrWhiteSpace(TempString))
                            {
                                TempString += "|32";
                            }
                            else
                            {
                                TempString += "32";
                            }
                        }
                        // End of Rev 4.0
                        // Rev 5.0
                        if (model.CanInvoice)
                        {
                            if (!string.IsNullOrWhiteSpace(TempString))
                            {
                                TempString += "|33";
                            }
                            else
                            {
                                TempString += "33";
                            }
                        }
                        if (model.CanReadyToDispatch)
                        {
                            if (!string.IsNullOrWhiteSpace(TempString))
                            {
                                TempString += "|34";
                            }
                            else
                            {
                                TempString += "34";
                            }
                        }
                        if (model.CanDispatch)
                        {
                            if (!string.IsNullOrWhiteSpace(TempString))
                            {
                                TempString += "|35";
                            }
                            else
                            {
                                TempString += "35";
                            }
                        }
                        if (model.CanDeliver)
                        {
                            if (!string.IsNullOrWhiteSpace(TempString))
                            {
                                TempString += "|36";
                            }
                            else
                            {
                                TempString += "36";
                            }
                        }
                        // End of Rev 5.0

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
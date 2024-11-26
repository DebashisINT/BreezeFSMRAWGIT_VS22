/*******************************************************************************************************************
 * 1.0                26-04-2023       V2.0.40           Sanchita         A checkbox required for performance module,check box name is Show Employee Performance.
                                                                          Refer: 25911
 * 2.0                08-05-2023       V2.0.40           Sanchita         In user table a column exist as IsShowBeatInMenu. 
 *                                                                        This will show in portal under user settings as"ShowBeatInMenu".
                                                                          Refer: 25947
 * 3.0                06-06-2023       V2.0.41           Sanchita         Required below System settings + user wise settings in portal
                                                                          Refer: 26245  
 * 4.0                31-08-2023       V2.0.43           Sanchita         User wise settings required in Web Portal Front end User Master
                                                                          Show menu for AI Market Assistant
                                                                          USB Debugging Restricted  
                                                                          Refer: 26768  
 * 5.0                06-09-2023       V2.0.43           Sanchita         A new user wise settings required named as ShowLatLongInOutletMaster
 *                                                                        Refer: 26794 
 * 6.0                19-12-2023       V2.0.44           Sanchita         Call log facility is required in the FSM App - IsCallLogHistoryActivated” - 
                                                                          User Account - Add User master settings. Mantis: 27063
 * 7.0                16-04-2024       V2.0.47           Sanchita         0027369: The mentioned settings are required in the User master in FSM
 * 8.0                17-04-2024       V2.0.47           Priti            0027372: ShowPartyWithCreateOrder setting shall be available User wise setting also
 *                                                                        0027374: ShowPartyWithGeoFence setting shall be available User wise setting also
 * 9.0                16-04-2024       V2.0.47           Sanchita         0027369: The mentioned settings are required in the User master in FSM
 * 10.0               22-05-2024       V2.0.47           Priti            0027467: Some changes are required in CRM Modules
 * 11.0               25-05-2024       V2.0.47           Sanchita         New User wise settings required. Mantis: 27474, 27477 *
 * 12.0               25-05-2024       V2.0.47           Sanchita         New User wise settings required. Mantis: 27502 *
 * 13.0               03-06-2024       V2.0.47           Sanchita         Some global settings are required for CRM Opportunity module. Mantis: 27481 *
 * 14.0               18-06-2024       V2.0.47           Sanchita         27436: Please create a global settings IsShowDateWiseOrderInApp   
 * 15.0               21-06-2024       V2.0.48           Sanchita         0027564: The default value should be zero for some of Global & User wise setting in FSM
 * 16.0               04-07-2024       V2.0.48           Sanchita         27575: Two new global and user settings are required as 'IsUserWiseLMSEnable' and 'IsUserWiseLMSFeatureOnly'        
 * 17.0               29-08-2024       V2.0.48           Sanchita         27648: Global and User wise settings isRecordAudioEnableForVisitRevisit shall be available 
                                                                          in both System settings page and in User master.  
 * 18.0               03-09-2024       V2.0.48           Priti            0027684: Create a new user setting as ShowClearQuiz
 * 19.0               24-09-2024       V2.0.49           Sanchita         0027705: A new Global and user wise settings required as IsAllowProductCurrentStockUpdateFromApp    
 * 20.0               27-09-2024       V2.0.49           Priti            0027714: Default value of the below user settings shall be changed as false .
 * 21.0               26-11-2024       v2.0.49           Sanchita         27793: A new user settings required as ShowTargetOnApp     
 *********************************************************************************************************************************/
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using BusinessLogicLayer;
using ClsDropDownlistNameSpace;
using System.Drawing;
using System.Web.Services;
using System.Collections.Generic;
using UtilityLayer;
using BusinessLogicLayer.SalesERP;
using System.Data.SqlClient;
using DataAccessLayer;
namespace ERP.OMS.Management.Master
{
    public partial class management_master_RootUserDetails : ERP.OMS.ViewState_class.VSPage
    {

        string Id;
        int CreateUser;
        DateTime CreateDate;        
        //DBEngine oDBEngine = new DBEngine(ConfigurationManager.AppSettings["DBConnectionDefault"]);

        BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(ConfigurationManager.AppSettings["DBConnectionDefault"]);
        clsDropDownList oclsDropDownList = new clsDropDownList();

        public string pageAccess = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            // Code  Added and Commented By Priti on 20122016 to use Covert.Tostring() instead of Tostring()
            //txtReportTo.Attributes.Add("onkeyup", "CallList(this,'SearchByEmpCont',event)");
            //txtReportTo.Attributes.Add("onfocus", "CallList(this,'SearchByEmpCont',event)");
            //txtReportTo.Attributes.Add("onkeyup", "CallList(this,'SearchByEmpCont',event)");
            //txtReportTo.Attributes.Add("onfocus", "CallList(this,'SearchByEmp',event)");
            //txtReportTo.Attributes.Add("onclick", "CallList(this,'SearchByEmp',event)");
            if (HttpContext.Current.Session["userid"] == null)
            {
                //Page.ClientScript.RegisterStartupScript(GetType(), "SighOff", "<script>SignOff();</script>");
            }

            Id = Request.QueryString["id"];
            hdnentrymode.Value = Id;
            // Code Added By Sandip on 22032017 to use Query String Value in Web Method for Chosen DropDown
            ActionMode = Request.QueryString["id"];
            // Code  Above Added By Sandip on 22032017 to use Query String Value in Web Method for Chosen DropDown
            CreateUser = Convert.ToInt32(HttpContext.Current.Session["userid"]);//Session UserID
            CreateDate = Convert.ToDateTime(oDBEngine.GetDate().ToShortDateString());
            if (!IsPostBack)
            {
                UserWiseSetings();
                EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/management/master/root_user.aspx");
                Session["addedituser"] = "yes";
                // FillComboContact();
                BindUserGroups();
                FillComboBranch();
                Fillgridview();
                FillComboPartyType();
                //Mantis Issue 25015
                FillComboType();
                //End of Mantis Issue 25015
                //Rev work start 26.04.2022 Mantise ID:0024856: Copy feature add in User master
                //if (Id != "Add")
                if (Id != "Add" && Request.QueryString["Mode"] !="Copy")
                //Rev work close 26.04.2022 Mantise ID:0024856: Copy feature add in User master
                {
                    //brnChangeUsersPassword.Visible = true;
                    if (!rights.CanEdit)
                    {
                        Response.Redirect("/OMS/Management/master/root_user.aspx");
                    }
                    ShowData(Id);
                    //Mantis Issue 24408,24364
                    //UserWiseSetings();
                    //End of Mantis Issue 24408,24364
                    //    txtusername.Enabled = false;


                }
                //Rev work start 26.04.2022 Mantise ID:0024856: Copy feature add in User master
                else if (Id != "Add" && Request.QueryString["Mode"]=="Copy")
                {
                    if (!rights.CanAdd)
                    {
                        Response.Redirect("/OMS/Management/master/root_user.aspx");
                    }
                    ShowData(Id);
                   // UserWiseSetings();
                }
                //Rev work close 26.04.2022 Mantise ID:0024856: Copy feature add in User master
                else
                {
                    //brnChangeUsersPassword.Visible = false;
                    if (!rights.CanAdd)
                    {
                        Response.Redirect("/OMS/Management/master/root_user.aspx");
                    }
                }

                //if (HttpContext.Current.Session["superuser"].ToString().Trim() == "Y")
                if (Convert.ToString(HttpContext.Current.Session["superuser"]).Trim() == "Y")
                {
                    cbSuperUser.Visible = true;
                }
                else
                {
                    cbSuperUser.Visible = false;
                }


            }
            /*--Set Page Accesss--*/
            string pageAccess = oDBEngine.CheckPageAccessebility("root_user.aspx");
            Session["PageAccess"] = pageAccess;
            //this.Page.ClientScript.RegisterStartupScript(GetType(), "heightL", "<script>height();</script>");
            /*Rev work start 12.05.2022 Mantise no :24856 In User master username,loginid,password,associate employee make blank in Copy Mode*/
            if(Request.QueryString["Mode"]=="Copy")
            { 
                //ModeState = Request.QueryString["Mode"].ToString();
                ActionMode = Request.QueryString["Mode"].ToString();
            }
            /*Rev work close 12.05.2022 Mantise no :24856 In User master username,loginid,password,associate employee make blank in Copy Mode*/
        }

        private void BindUserGroups()
        {
            ddlGroups.Items.Clear();

            DataTable dt = new BusinessLogicLayer.UserGroupsBLS.UserGroupBL().FetchAllGroupsDataTable();

            if (dt != null && dt.Rows.Count > 0)
            {
                ddlGroups.DataSource = dt;
                ddlGroups.DataTextField = "grp_name";
                ddlGroups.DataValueField = "grp_id";
                ddlGroups.DataBind();
            }

            ddlGroups.Items.Insert(0, "Select Group");
        }

        protected void FillComboPartyType()
        {
            string[,] DataPartyType = oDBEngine.GetFieldValue("tbl_shoptype", "TypeId,Name", "IsActive=1", 2);
            oclsDropDownList.AddDataToDropDownList(DataPartyType, ddlPartyType);
        }
        //Mantis Issue 25015
        protected void FillComboType()
        {
            ddlType.Items.Clear();

            DataTable dt = oDBEngine.GetDataTable("select StageID,Stage from FTS_Stage");

            if (dt != null && dt.Rows.Count > 0)
            {
                ddlType.DataSource = dt;
                ddlType.DataTextField = "Stage";
                ddlType.DataValueField = "StageID";
                ddlType.DataBind();
            }

            ddlType.Items.Insert(0, "Select Type");
        }
        //End of Mantis Issue 25015
        /*Code  Added  By Priti on 06122016 to use jquery Choosen*/
        [WebMethod]
        public static List<string> ALLEmployee(string reqStr)
        {

            BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(ConfigurationSettings.AppSettings["DBConnectionDefault"]);
            DataTable DT = new DataTable();
            DT.Rows.Clear();
            UserBL objUserBL = new UserBL();
            /*Rev work start 12.05.2022 Mantise no :24856 In User master username,loginid,password,associate employee make blank in Copy Mode*/
            if (ActionMode == "Add")
            //if (ActionMode == "Add" || ModeState == "Copy")
            {
                //DT = oDBEngine.GetDataTable("tbl_master_employee, tbl_master_contact,tbl_trans_employeeCTC ", "ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') +'['+cnt_shortName+']' AS Name, cnt_internalId as Id    ", " tbl_master_employee.emp_contactId = tbl_trans_employeeCTC.emp_cntId and tbl_trans_employeeCTC.emp_cntId = tbl_master_contact.cnt_internalId    and cnt_contactType='EM' and (emp_dateofLeaving is null or emp_dateofLeaving='1/1/1900 12:00:00 AM' OR emp_dateofLeaving>getdate()) and (cnt_firstName Like '" + reqStr + "%' or cnt_shortName like '" + reqStr + "%')");
                //DT = oDBEngine.GetDataTable("tbl_master_employee, tbl_master_contact,tbl_trans_employeeCTC ", " ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') +'['+cnt_shortName+']' AS Name, cnt_internalId as Id ", " tbl_master_employee.emp_contactId = tbl_trans_employeeCTC.emp_cntId and tbl_trans_employeeCTC.emp_cntId = tbl_master_contact.cnt_internalId  and cnt_contactType='EM' and (emp_dateofLeaving is null or emp_dateofLeaving='1/1/1900 12:00:00 AM' OR emp_dateofLeaving>getdate()) and (cnt_firstName Like '" + reqStr + "%' or cnt_shortName like '" + reqStr + "%') and tbl_master_contact.cnt_internalId not in (select user_contactId from tbl_master_user) group by tbl_trans_employeeCTC.emp_id,ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') +'['+cnt_shortName+']' , cnt_internalId   having  max(tbl_trans_employeeCTC.emp_id) in (select MAX(tbl_trans_employeeCTC.emp_id)from tbl_trans_employeeCTC group by emp_cntId)");
                DT = objUserBL.PopulateAssociatedEmployee(0, "Add", Convert.ToInt32(HttpContext.Current.Session["userid"]));
            }
            else if(ActionMode=="Copy")
            {
                DT = objUserBL.PopulateAssociatedEmployee(0, "Add", Convert.ToInt32(HttpContext.Current.Session["userid"]));
            }
            //Rev work close 12.05.2022 Mantise no :24856 In User master username,loginid,password,associate employee make blank in Copy Mode
            else
            {
                DT = objUserBL.PopulateAssociatedEmployee(Convert.ToInt32(ActionMode), "Edit", Convert.ToInt32(HttpContext.Current.Session["userid"]));
            }
            if (DT.Rows.Count > 0)
            {

                List<string> obj = new List<string>();
                foreach (DataRow dr in DT.Rows)
                {

                    obj.Add(Convert.ToString(dr["Name"]) + "|" + Convert.ToString(dr["Id"]));
                }

                return obj;
            }
            else
            {
                return null;
            }

        }
        //...............code end........

        // Mantis Issue 24723
        [WebMethod]
        public static int chkLoginIdExist(string userLoginId, string action, string userid)
        {
            int IdExist = 0;
            DBEngine oDBEngine = new DBEngine(ConfigurationManager.AppSettings["DBConnectionDefault"]);

            if (action == "ADD")
            {
                string[,] checkUser = oDBEngine.GetFieldValue("tbl_master_user", "user_loginId", " user_loginId='" + userLoginId.Trim() + "'", 1);
                string check = checkUser[0, 0];
                if (check != "n")
                {
                    IdExist = 1;
                }
            }
            else if (action == "UPDATE")
            {
                string[,] checkUser = oDBEngine.GetFieldValue("tbl_master_user", "user_loginId", " user_loginId='" + userLoginId.Trim() + "' and user_id<> '" + userid + "'", 1);
                string check = checkUser[0, 0];
                if (check != "n")
                {
                    IdExist = 1;
                }
            }
            
            return IdExist;
        }
        // End of Mantis Issue 24723


        //protected void FillComboContact()
        //{
        //    //DBEngine objEngine = new DBEngine(ConfigurationManager.AppSettings["DBConnectionDefault"]);
        //    string[,] Data = oDBEngine.GetFieldValue("tbl_master_contact", "cnt_internalId AS Id, cnt_firstName + ' ' + ISNULL(cnt_lastName,'') + '[' + ISNULL(cnt_shortName,'') + ']'  AS Name", "cnt_contacttype='EM'", 2, " cnt_FirstName ");
        //    oDBEngine.AddDataToDropDownList(Data, drpContact);
        //}
        protected void FillComboBranch()
        {
            //DBEngine objEngine = new DBEngine(ConfigurationManager.AppSettings["DBConnectionDefault"]);
            string[,] DataBranch = oDBEngine.GetFieldValue("tbl_master_branch", "branch_id, branch_description", null, 2);
            //oDBEngine.AddDataToDropDownList(DataBranch, dropdownlistbranch);
            oclsDropDownList.AddDataToDropDownList(DataBranch, dropdownlistbranch);
        }
        protected void Fillgridview()
        {
            //DBEngine objEngine = new DBEngine(ConfigurationManager.AppSettings["DBConnectionDefault"]);
            DataSet dsDocument = new DataSet();
            dsDocument = oDBEngine.PopulateData("seg_id as Id, seg_name as SegmentName", "tbl_master_segment", null);
            if (dsDocument.Tables["TableName"].Rows.Count > 0)
            {
                grdUserAccess.DataSource = dsDocument.Tables["TableName"];
                grdUserAccess.DataBind();
            }

        }
        protected void grdUserAccess_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //DBEngine objEngine = new DBEngine(ConfigurationManager.AppSettings["DBConnectionDefault"]);
                DropDownList drpUserGroup = (DropDownList)e.Row.FindControl("drpUserGroup");
                Label lbl = (Label)e.Row.FindControl("lblId");
                string[,] DatadropDown = oDBEngine.GetFieldValue("tbl_master_userGroup", "grp_id, grp_name", "grp_segmentId='" + lbl.Text + "'", 2, "grp_name");
                string checkId1 = DatadropDown[0, 0];
                if (checkId1 != "n")
                {
                    //oDBEngine.AddDataToDropDownList(DatadropDown, drpUserGroup);
                    oclsDropDownList.AddDataToDropDownList(DatadropDown, drpUserGroup);
                }
            }
        }
        protected void ShowData(string Id)
        {
            // Code  Added and Commented By Priti on 20122016 to use Covert.Tostring() instead of Tostring()
            //DBEngine objEngine = new DBEngine(ConfigurationManager.AppSettings["DBConnectionDefault"]);
            //Rev work start 26.04.2022 Mantise ID:0024856: Copy feature add in User master
            //user_password.Visible = false;
            if (Request.QueryString["Mode"] == "Copy")
            {
                user_password.Visible = true;
            }
            else
            {
                user_password.Visible = false;
            }
            //Rev work close 26.04.2022 Mantise ID:0024856: Copy feature add in User master
            Int64 userId = Convert.ToInt64(Id);
            DataSet dsUserDetail = new DataSet();
            //dsUserDetail = oDBEngine.PopulateData("u.user_name as user1 , u.user_loginId as Login,u.user_branchId as Branchid,u.user_group as usergroup,u.user_AllowAccessIP,u.user_contactId as ContactId, c.cnt_firstName + ' ' +c.cnt_lastName+'['+c.cnt_shortName+']' AS Name,c.cnt_internalId,c.cnt_id,u.user_id,u.user_superUser ,u.user_inactive,u.user_maclock,u.user_EntryProfile,u.Gps_Accuracy,u.HierarchywiseTargetSettings,ISNULL(u.willLeaveApprovalEnable,0) AS willLeaveApprovalEnable,ISNULL(u.IsAutoRevisitEnable,0) AS IsAutoRevisitEnable,ISNULL(u.IsShowPlanDetails,0) AS IsShowPlanDetails,ISNULL(u.IsMoreDetailsMandatory,0) AS IsMoreDetailsMandatory,ISNULL(u.IsShowMoreDetailsMandatory,0) AS IsShowMoreDetailsMandatory,ISNULL(u.isMeetingAvailable,0) AS isMeetingAvailable,ISNULL(u.isRateNotEditable,0) AS isRateNotEditable,ISNULL(u.IsShowTeamDetails,0) AS IsShowTeamDetails,ISNULL(u.IsAllowPJPUpdateForTeam,0) AS IsAllowPJPUpdateForTeam,ISNULL(u.willReportShow,0) AS willReportShow,ISNULL(u.isFingerPrintMandatoryForAttendance,0) AS isFingerPrintMandatoryForAttendance,ISNULL(u.isFingerPrintMandatoryForVisit,0) AS isFingerPrintMandatoryForVisit,ISNULL(u.isSelfieMandatoryForAttendance,0) AS isSelfieMandatoryForAttendance,ISNULL(u.isAttendanceReportShow,0) AS isAttendanceReportShow,ISNULL(u.isPerformanceReportShow,0) AS isPerformanceReportShow,ISNULL(u.isVisitReportShow,0) AS isVisitReportShow,ISNULL(u.willTimesheetShow,0) AS willTimesheetShow,ISNULL(u.isAttendanceFeatureOnly,0) AS isAttendanceFeatureOnly,ISNULL(u.isOrderShow,0) AS isOrderShow,ISNULL(u.isVisitShow,0) AS isVisitShow,ISNULL(u.iscollectioninMenuShow,0) AS iscollectioninMenuShow,ISNULL(u.isShopAddEditAvailable,0) AS isShopAddEditAvailable,ISNULL(u.isEntityCodeVisible,0) AS isEntityCodeVisible,ISNULL(u.isAreaMandatoryInPartyCreation,0) AS isAreaMandatoryInPartyCreation,ISNULL(u.isShowPartyInAreaWiseTeam,0) AS isShowPartyInAreaWiseTeam,ISNULL(u.isChangePasswordAllowed,0) AS isChangePasswordAllowed,ISNULL(u.isHomeRestrictAttendance,0) AS isHomeRestrictAttendance,ISNULL(u.isQuotationShow,0) AS isQuotationShow,ISNULL(u.IsStateMandatoryinReport,0) AS IsStateMandatoryinReport,ISNULL(isAchievementEnable,0) AS isAchievementEnable,ISNULL(isTarVsAchvEnable,0) AS isTarVsAchvEnable,homeLocDistance,shopLocAccuracy", "tbl_master_user u,tbl_master_contact c", "u.user_id='" + userId + "' AND u.user_contactId=c.cnt_internalId");
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSInsertUpdateUser");
            proc.AddPara("@ACTION", "EDIT");
            proc.AddPara("@user_id", userId);
            dsUserDetail = proc.GetDataSet();

            if (dsUserDetail.Tables[0].Rows.Count > 0)
            {
                txtgps.Text = Convert.ToString(dsUserDetail.Tables[0].Rows[0]["Gps_Accuracy"]);
                //txtusername.Text = dsUserDetail.Tables["TableName"].Rows[0]["user1"].ToString();
                /*Rev work start 12.05.2022 Mantise no :24856 In User master username,loginid,password,associate employee make blank in Copy Mode*/
                //txtusername.Text = Convert.ToString(dsUserDetail.Tables[0].Rows[0]["user1"]);
                if (Request.QueryString["Mode"] == "Copy")
                {
                    txtusername.Text ="";
                    txtuserid.Text = "";
                    txtpassword.Text = "";
                }
                else
                {
                    txtusername.Text = Convert.ToString(dsUserDetail.Tables[0].Rows[0]["user1"]);
                    txtuserid.Text = Convert.ToString(dsUserDetail.Tables[0].Rows[0]["Login"]);
                }
                /*Rev work close 12.05.2022 Mantise no :24856 In User master username,loginid,password,associate employee make blank in Copy Mode*/
                //txtloginid.Text = dsUserDetail.Tables["TableName"].Rows[0]["Login"].ToString();
                /*Rev work start 12.05.2022 Mantise no :24856 In User master username,loginid,password,associate employee make blank in Copy Mode*/
                //txtuserid.Text = Convert.ToString(dsUserDetail.Tables[0].Rows[0]["Login"]);
                /*Rev work close 12.05.2022 Mantise no :24856 In User master username,loginid,password,associate employee make blank in Copy Mode*/
                //txtReportTo.Text = dsUserDetail.Tables["TableName"].Rows[0]["Name"].ToString();
                //txtReportTo_hidden.Value = dsUserDetail.Tables["TableName"].Rows[0]["cnt_internalId"].ToString();
                /*Rev work start 12.05.2022 Mantise no :24856 In User master username,loginid,password,associate employee make blank in Copy Mode*/
                //txtReportTo_hidden.Value = Convert.ToString(dsUserDetail.Tables[0].Rows[0]["cnt_internalId"]);
                if (Request.QueryString["id"] != "Add" && Request.QueryString["Mode"] != "Copy")
                {
                    txtReportTo_hidden.Value = Convert.ToString(dsUserDetail.Tables[0].Rows[0]["cnt_internalId"]);
                }
                /*Rev work close 12.05.2022 Mantise no :24856 In User master username,loginid,password,associate employee make blank in Copy Mode*/
                //dropdownlistbranch.SelectedValue = dsUserDetail.Tables["TableName"].Rows[0]["Branchid"].ToString();
                dropdownlistbranch.SelectedValue = Convert.ToString(dsUserDetail.Tables[0].Rows[0]["Branchid"]);
                //string usergroup = dsUserDetail.Tables["TableName"].Rows[0]["usergroup"].ToString();
                string usergroup = Convert.ToString(dsUserDetail.Tables[0].Rows[0]["usergroup"]);
                try
                {
                    ddlGroups.SelectedValue = usergroup.Trim();
                }
                catch
                {
                    ddlGroups.SelectedIndex = 0;
                }
                // hdncontactId.Value = dsUserDetail.Tables["TableName"].Rows[0]["ContactId"].ToString();
                //txtReportTo_hidden.Value = dsUserDetail.Tables["TableName"].Rows[0]["ContactId"].ToString();
                /*Rev work start 12.05.2022 Mantise no :24856 In User master username,loginid,password,associate employee make blank in Copy Mode*/
                //txtReportTo_hidden.Value = Convert.ToString(dsUserDetail.Tables[0].Rows[0]["ContactId"]);
                if (Request.QueryString["id"] != "Add" && Request.QueryString["Mode"] != "Copy")
                {
                    txtReportTo_hidden.Value = Convert.ToString(dsUserDetail.Tables[0].Rows[0]["ContactId"]);
                }
                /*Rev work close 12.05.2022 Mantise no :24856 In User master username,loginid,password,associate employee make blank in Copy Mode*/
                //ddDataEntry.SelectedValue = dsUserDetail.Tables["TableName"].Rows[0]["user_EntryProfile"].ToString();
                ddDataEntry.SelectedValue = Convert.ToString(dsUserDetail.Tables[0].Rows[0]["user_EntryProfile"]);
                selectedValue(usergroup);
                //if (dsUserDetail.Tables["TableName"].Rows[0]["user_superUser"].ToString().Trim() == "Y")
                if (Convert.ToString(dsUserDetail.Tables[0].Rows[0]["user_superUser"]).Trim() == "Y")
                {
                    cbSuperUser.Checked = true;
                }
                else
                {
                    cbSuperUser.Checked = false;
                }


                //if (dsUserDetail.Tables["TableName"].Rows[0]["user_inactive"].ToString().Trim() == "Y")
                if (Convert.ToString(dsUserDetail.Tables[0].Rows[0]["user_inactive"]).Trim() == "Y")
                {
                    chkIsActive.Checked = true;
                }
                else
                {
                    chkIsActive.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["HierarchywiseTargetSettings"]) == true)
                {
                    chkTargetSettings.Checked = true;
                }
                else
                {
                    chkTargetSettings.Checked = false;
                }

                //Rev Start new seetings add Tanmoy
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["willLeaveApprovalEnable"]) == true)
                {
                    chkLeaveEanbleSettings.Checked = true;
                }
                else
                {
                    chkLeaveEanbleSettings.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsAutoRevisitEnable"]) == true)
                {
                    chkRevisitSettings.Checked = true;
                }
                else
                {
                    chkRevisitSettings.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowPlanDetails"]) == true)
                {
                    chkPlanDetailsSettings.Checked = true;
                }
                else
                {
                    chkPlanDetailsSettings.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsMoreDetailsMandatory"]) == true)
                {
                    chkMoreDetailsMandatorySettings.Checked = true;
                }
                else
                {
                    chkMoreDetailsMandatorySettings.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowMoreDetailsMandatory"]) == true)
                {
                    chkShowMoreDetailsSettings.Checked = true;
                }
                else
                {
                    chkShowMoreDetailsSettings.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isMeetingAvailable"]) == true)
                {
                    chkMeetingsSettings.Checked = true;
                }
                else
                {
                    chkMeetingsSettings.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isRateNotEditable"]) == true)
                {
                    chkRateEditableSettings.Checked = true;
                }
                else
                {
                    chkRateEditableSettings.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowTeamDetails"]) == true)
                {
                    chkShowTeam.Checked = true;
                }
                else
                {
                    chkShowTeam.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsAllowPJPUpdateForTeam"]) == true)
                {
                    chkAllowPJPupdateforTeam.Checked = true;
                }
                else
                {
                    chkAllowPJPupdateforTeam.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["willReportShow"]) == true)
                {
                    chkwillReportShow.Checked = true;
                }
                else
                {
                    chkwillReportShow.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isFingerPrintMandatoryForAttendance"]) == true)
                {
                    chkIsFingerPrintMandatoryForAttendance.Checked = true;
                }
                else
                {
                    chkIsFingerPrintMandatoryForAttendance.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isFingerPrintMandatoryForVisit"]) == true)
                {
                    chkIsFingerPrintMandatoryForVisit.Checked = true;
                }
                else
                {
                    chkIsFingerPrintMandatoryForVisit.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isSelfieMandatoryForAttendance"]) == true)
                {
                    chkIsSelfieMandatoryForAttendance.Checked = true;
                }
                else
                {
                    chkIsSelfieMandatoryForAttendance.Checked = false;
                }



                DataSet dsApprove = new DataSet();
                dsApprove = oDBEngine.PopulateData("ID", "FTS_LEAVE_APPROVER", "APPROVAR_ID='" + userId + "'");
                if (dsApprove.Tables["TableName"].Rows.Count > 0)
                {
                    chkLeaveApprover.Checked = true;
                }
                else
                {
                    chkLeaveApprover.Checked = false;
                }



                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isAttendanceReportShow"]) == true)
                {
                    chkisAttendanceReportShow.Checked = true;
                }
                else
                {
                    chkisAttendanceReportShow.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isPerformanceReportShow"]) == true)
                {
                    chkisPerformanceReportShow.Checked = true;
                }
                else
                {
                    chkisPerformanceReportShow.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isVisitReportShow"]) == true)
                {
                    chkisVisitReportShow.Checked = true;
                }
                else
                {
                    chkisVisitReportShow.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["willTimesheetShow"]) == true)
                {
                    chkwillTimesheetShow.Checked = true;
                }
                else
                {
                    chkwillTimesheetShow.Checked = false;
                }
                /*Rev work start 11.05.2022 Mantise No:0024880 Horizontal Performance Summary & detail report required with hierarchy setting*/
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsHorizontalPerformReportShow"]) == true)
                {
                    chkisHorizontalReportShow.Checked = true;
                }
                else
                {
                    chkisHorizontalReportShow.Checked = false;
                }
                /*Rev work close 11.05.2022 Mantise No:0024880 Horizontal Performance Summary & detail report required with hierarchy setting*/
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isAttendanceFeatureOnly"]) == true)
                {
                    chkisAttendanceFeatureOnly.Checked = true;
                }
                else
                {
                    chkisAttendanceFeatureOnly.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isOrderShow"]) == true)
                {
                    chkisOrderShow.Checked = true;
                }
                else
                {
                    chkisOrderShow.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isVisitShow"]) == true)
                {
                    chkisVisitShow.Checked = true;
                }
                else
                {
                    chkisVisitShow.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["iscollectioninMenuShow"]) == true)
                {
                    chkiscollectioninMenuShow.Checked = true;
                }
                else
                {
                    chkiscollectioninMenuShow.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isShopAddEditAvailable"]) == true)
                {
                    chkisShopAddEditAvailable.Checked = true;
                }
                else
                {
                    chkisShopAddEditAvailable.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isEntityCodeVisible"]) == true)
                {
                    chkisEntityCodeVisible.Checked = true;
                }
                else
                {
                    chkisEntityCodeVisible.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isAreaMandatoryInPartyCreation"]) == true)
                {
                    chkisAreaMandatoryInPartyCreation.Checked = true;
                }
                else
                {
                    chkisAreaMandatoryInPartyCreation.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isShowPartyInAreaWiseTeam"]) == true)
                {
                    chkisShowPartyInAreaWiseTeam.Checked = true;
                }
                else
                {
                    chkisShowPartyInAreaWiseTeam.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isChangePasswordAllowed"]) == true)
                {
                    chkisChangePasswordAllowed.Checked = true;
                }
                else
                {
                    chkisChangePasswordAllowed.Checked = false;
                }

                //if (Convert.ToBoolean(dsUserDetail.Tables["TableName"].Rows[0]["isHomeRestrictAttendance"]) == true)
                //{
                //    chkisHomeRestrictAttendance.Checked = true;
                //}
                //else
                //{
                //    chkisHomeRestrictAttendance.Checked = false;
                //}

                ddlRestrictionHomeLocation.SelectedValue = dsUserDetail.Tables[0].Rows[0]["isHomeRestrictAttendance"].ToString();
                //Mantis issue 25015
                ddlType.SelectedValue = dsUserDetail.Tables[0].Rows[0]["FaceRegTypeID"].ToString();
                //End of Mantis Issue 25015

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isQuotationShow"]) == true)
                {
                    chkisQuotationShow.Checked = true;
                }
                else
                {
                    chkisQuotationShow.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsStateMandatoryinReport"]) == true)
                {
                    chkIsStateMandatoryinReport.Checked = true;
                }
                else
                {
                    chkIsStateMandatoryinReport.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isAchievementEnable"]) == true)
                {
                    chkisAchievementEnable.Checked = true;
                }
                else
                {
                    chkisAchievementEnable.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isTarVsAchvEnable"]) == true)
                {
                    chkisTarVsAchvEnable.Checked = true;
                }
                else
                {
                    chkisTarVsAchvEnable.Checked = false;
                }
                //new settings 18-08-2020
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isQuotationPopupShow"]) == true)
                {
                    chkisQuotationPopupShow.Checked = true;
                }
                else
                {
                    chkisQuotationPopupShow.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isOrderReplacedWithTeam"]) == true)
                {
                    chkisOrderReplacedWithTeam.Checked = true;
                }
                else
                {
                    chkisOrderReplacedWithTeam.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isMultipleAttendanceSelection"]) == true)
                {
                    chkisMultipleAttendanceSelection.Checked = true;
                }
                else
                {
                    chkisMultipleAttendanceSelection.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isOfflineTeam"]) == true)
                {
                    chkisOfflineTeam.Checked = true;
                }
                else
                {
                    chkisOfflineTeam.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isDDShowForMeeting"]) == true)
                {
                    chkisDDShowForMeeting.Checked = true;
                }
                else
                {
                    chkisDDShowForMeeting.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isDDMandatoryForMeeting"]) == true)
                {
                    chkisDDMandatoryForMeeting.Checked = true;
                }
                else
                {
                    chkisDDMandatoryForMeeting.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isAllTeamAvailable"]) == true)
                {
                    chkisAllTeamAvailable.Checked = true;
                }
                else
                {
                    chkisAllTeamAvailable.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isRecordAudioEnable"]) == true)
                {
                    chkisRecordAudioEnable.Checked = true;
                }
                else
                {
                    chkisRecordAudioEnable.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isNextVisitDateMandatory"]) == true)
                {
                    chkisNextVisitDateMandatory.Checked = true;
                }
                else
                {
                    chkisNextVisitDateMandatory.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isShowCurrentLocNotifiaction"]) == true)
                {
                    chkisShowCurrentLocNotifiaction.Checked = true;
                }
                else
                {
                    chkisShowCurrentLocNotifiaction.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isUpdateWorkTypeEnable"]) == true)
                {
                    chkisUpdateWorkTypeEnable.Checked = true;
                }
                else
                {
                    chkisUpdateWorkTypeEnable.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isLeaveEnable"]) == true)
                {
                    chkisLeaveEnable.Checked = true;
                }
                else
                {
                    chkisLeaveEnable.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isOrderMailVisible"]) == true)
                {
                    chkisOrderMailVisible.Checked = true;
                }
                else
                {
                    chkisOrderMailVisible.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["LateVisitSMS"]) == true)
                {
                    chkLateVisitSMS.Checked = true;
                }
                else
                {
                    chkLateVisitSMS.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isShopEditEnable"]) == true)
                {
                    chkisShopEditEnable.Checked = true;
                }
                else
                {
                    chkisShopEditEnable.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isTaskEnable"]) == true)
                {
                    chkisTaskEnable.Checked = true;
                }
                else
                {
                    chkisTaskEnable.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isAppInfoEnable"]) == true)
                {
                    chkisAppInfoEnable.Checked = true;
                }
                else
                {
                    chkisAppInfoEnable.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["willDynamicShow"]) == true)
                {
                    chkwillDynamicShow.Checked = true;
                }
                else
                {
                    chkwillDynamicShow.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["willActivityShow"]) == true)
                {
                    chkwillActivityShow.Checked = true;
                }
                else
                {
                    chkwillActivityShow.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isDocumentRepoShow"]) == true)
                {
                    chkisDocumentRepoShow.Checked = true;
                }
                else
                {
                    chkisDocumentRepoShow.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isChatBotShow"]) == true)
                {
                    chkisChatBotShow.Checked = true;
                }
                else
                {
                    chkisChatBotShow.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isAttendanceBotShow"]) == true)
                {
                    chkisAttendanceBotShow.Checked = true;
                }
                else
                {
                    chkisAttendanceBotShow.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isVisitBotShow"]) == true)
                {
                    chkisVisitBotShow.Checked = true;
                }
                else
                {
                    chkisVisitBotShow.Checked = false;
                }

                //Add extra Settings 
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isInstrumentCompulsory"]) == true)
                {
                    chkisInstrumentCompulsory.Checked = true;
                }
                else
                {
                    chkisInstrumentCompulsory.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isBankCompulsory"]) == true)
                {
                    chkisBankCompulsory.Checked = true;
                }
                else
                {
                    chkisBankCompulsory.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isComplementaryUser"]) == true)
                {
                    chkisComplementaryUser.Checked = true;
                }
                else
                {
                    chkisComplementaryUser.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isVisitPlanShow"]) == true)
                {
                    chkisVisitPlanShow.Checked = true;
                }
                else
                {
                    chkisVisitPlanShow.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isVisitPlanMandatory"]) == true)
                {
                    chkisVisitPlanMandatory.Checked = true;
                }
                else
                {
                    chkisVisitPlanMandatory.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isAttendanceDistanceShow"]) == true)
                {
                    chkisAttendanceDistanceShow.Checked = true;
                }
                else
                {
                    chkisAttendanceDistanceShow.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["willTimelineWithFixedLocationShow"]) == true)
                {
                    chkwillTimelineWithFixedLocationShow.Checked = true;
                }
                else
                {
                    chkwillTimelineWithFixedLocationShow.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isShowOrderRemarks"]) == true)
                {
                    chkisShowOrderRemarks.Checked = true;
                }
                else
                {
                    chkisShowOrderRemarks.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isShowOrderSignature"]) == true)
                {
                    chkisShowOrderSignature.Checked = true;
                }
                else
                {
                    chkisShowOrderSignature.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isShowSmsForParty"]) == true)
                {
                    chkisShowSmsForParty.Checked = true;
                }
                else
                {
                    chkisShowSmsForParty.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isShowTimeline"]) == true)
                {
                    chkisShowTimeline.Checked = true;
                }
                else
                {
                    chkisShowTimeline.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["willScanVisitingCard"]) == true)
                {
                    chkwillScanVisitingCard.Checked = true;
                }
                else
                {
                    chkwillScanVisitingCard.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isCreateQrCode"]) == true)
                {
                    chkisCreateQrCode.Checked = true;
                }
                else
                {
                    chkisCreateQrCode.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isScanQrForRevisit"]) == true)
                {
                    chkisScanQrForRevisit.Checked = true;
                }
                else
                {
                    chkisScanQrForRevisit.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isShowLogoutReason"]) == true)
                {
                    chkisShowLogoutReason.Checked = true;
                }
                else
                {
                    chkisShowLogoutReason.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["willShowHomeLocReason"]) == true)
                {
                    chkwillShowHomeLocReason.Checked = true;
                }
                else
                {
                    chkwillShowHomeLocReason.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["willShowShopVisitReason"]) == true)
                {
                    chkwillShowShopVisitReason.Checked = true;
                }
                else
                {
                    chkwillShowShopVisitReason.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["willShowPartyStatus"]) == true)
                {
                    chkwillShowPartyStatus.Checked = true;
                }
                else
                {
                    chkwillShowPartyStatus.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["willShowEntityTypeforShop"]) == true)
                {
                    chkwillShowEntityTypeforShop.Checked = true;
                }
                else
                {
                    chkwillShowEntityTypeforShop.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isShowRetailerEntity"]) == true)
                {
                    chkisShowRetailerEntity.Checked = true;
                }
                else
                {
                    chkisShowRetailerEntity.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isShowDealerForDD"]) == true)
                {
                    chkisShowDealerForDD.Checked = true;
                }
                else
                {
                    chkisShowDealerForDD.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isShowBeatGroup"]) == true)
                {
                    chkisShowBeatGroup.Checked = true;
                }
                else
                {
                    chkisShowBeatGroup.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isShowShopBeatWise"]) == true)
                {
                    chkisShowShopBeatWise.Checked = true;
                }
                else
                {
                    chkisShowShopBeatWise.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isShowOTPVerificationPopup"]) == true)
                {
                    chkisShowOTPVerificationPopup.Checked = true;
                }
                else
                {
                    chkisShowOTPVerificationPopup.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isShowMicroLearing"]) == true)
                {
                    chkisShowMicroLearing.Checked = true;
                }
                else
                {
                    chkisShowMicroLearing.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isMultipleVisitEnable"]) == true)
                {
                    chkisMultipleVisitEnable.Checked = true;
                }
                else
                {
                    chkisMultipleVisitEnable.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isShowVisitRemarks"]) == true)
                {
                    chkisShowVisitRemarks.Checked = true;
                }
                else
                {
                    chkisShowVisitRemarks.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isShowNearbyCustomer"]) == true)
                {
                    chkisShowNearbyCustomer.Checked = true;
                }
                else
                {
                    chkisShowNearbyCustomer.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isServiceFeatureEnable"]) == true)
                {
                    chkisServiceFeatureEnable.Checked = true;
                }
                else
                {
                    chkisServiceFeatureEnable.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isPatientDetailsShowInOrder"]) == true)
                {
                    chkisPatientDetailsShowInOrder.Checked = true;
                }
                else
                {
                    chkisPatientDetailsShowInOrder.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isPatientDetailsShowInCollection"]) == true)
                {
                    chkisPatientDetailsShowInCollection.Checked = true;
                }
                else
                {
                    chkisPatientDetailsShowInCollection.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isAttachmentMandatory"]) == true)
                {
                    chkisAttachmentMandatory.Checked = true;
                }
                else
                {
                    chkisAttachmentMandatory.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isShopImageMandatory"]) == true)
                {
                    chkisShopImageMandatory.Checked = true;
                }
                else
                {
                    chkisShopImageMandatory.Checked = false;
                }




                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isLogShareinLogin"]) == true)
                {
                    chkisLogShareinLogin.Checked = true;
                }
                else
                {
                    chkisLogShareinLogin.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsCompetitorenable"]) == true)
                {
                    chkIsCompetitorenable.Checked = true;
                }
                else
                {
                    chkIsCompetitorenable.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsOrderStatusRequired"]) == true)
                {
                    chkIsOrderStatusRequired.Checked = true;
                }
                else
                {
                    chkIsOrderStatusRequired.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsCurrentStockEnable"]) == true)
                {
                    chkIsCurrentStockEnable.Checked = true;
                }
                else
                {
                    chkIsCurrentStockEnable.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsCurrentStockApplicableforAll"]) == true)
                {
                    chkIsCurrentStockApplicableforAll.Checked = true;
                }
                else
                {
                    chkIsCurrentStockApplicableforAll.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IscompetitorStockRequired"]) == true)
                {
                    chkIscompetitorStockRequired.Checked = true;
                }
                else
                {
                    chkIscompetitorStockRequired.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsCompetitorStockforParty"]) == true)
                {
                    chkIsCompetitorStockforParty.Checked = true;
                }
                else
                {
                    chkIsCompetitorStockforParty.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["ShowFaceRegInMenu"]) == true)
                {
                    chkShowFaceRegInMenu.Checked = true;
                }
                else
                {
                    chkShowFaceRegInMenu.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsFaceDetection"]) == true)
                {
                    chkIsFaceDetection.Checked = true;
                }
                else
                {
                    chkIsFaceDetection.Checked = false;
                }
                //if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isFaceRegistered"]) == true)
                //{
                //    chkisFaceRegistered.Checked = true;
                //}
                //else
                //{
                //    chkisFaceRegistered.Checked = false;
                //}
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsUserwiseDistributer"]) == true)
                {
                    chkIsUserwiseDistributer.Checked = true;
                }
                else
                {
                    chkIsUserwiseDistributer.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsPhotoDeleteShow"]) == true)
                {
                    chkIsPhotoDeleteShow.Checked = true;
                }
                else
                {
                    chkIsPhotoDeleteShow.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsAllDataInPortalwithHeirarchy"]) == true)
                {
                    chkIsAllDataInPortalwithHeirarchy.Checked = true;
                }
                else
                {
                    chkIsAllDataInPortalwithHeirarchy.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsFaceDetectionWithCaptcha"]) == true)
                {
                    chkIsFaceDetectionWithCaptcha.Checked = true;
                }
                else
                {
                    chkIsFaceDetectionWithCaptcha.Checked = false;
                }



                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowMenuAddAttendance"]) == true)
                {
                    chkIsShowMenuAddAttendance.Checked = true;
                }
                else
                {
                    chkIsShowMenuAddAttendance.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowMenuAttendance"]) == true)
                {
                    chkIsShowMenuAttendance.Checked = true;
                }
                else
                {
                    chkIsShowMenuAttendance.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowMenuShops"]) == true)
                {
                    chkIsShowMenuShops.Checked = true;
                }
                else
                {
                    chkIsShowMenuShops.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowMenuOutstandingDetailsPPDD"]) == true)
                {
                    chkIsShowMenuOutstandingDetailsPPDD.Checked = true;
                }
                else
                {
                    chkIsShowMenuOutstandingDetailsPPDD.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowMenuStockDetailsPPDD"]) == true)
                {
                    chkIsShowMenuStockDetailsPPDD.Checked = true;
                }
                else
                {
                    chkIsShowMenuStockDetailsPPDD.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowMenuTA"]) == true)
                {
                    chkIsShowMenuTA.Checked = true;
                }
                else
                {
                    chkIsShowMenuTA.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowMenuMISReport"]) == true)
                {
                    chkIsShowMenuMISReport.Checked = true;
                }
                else
                {
                    chkIsShowMenuMISReport.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowMenuReimbursement"]) == true)
                {
                    chkIsShowMenuReimbursement.Checked = true;
                }
                else
                {
                    chkIsShowMenuReimbursement.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowMenuAchievement"]) == true)
                {
                    chkIsShowMenuAchievement.Checked = true;
                }
                else
                {
                    chkIsShowMenuAchievement.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowMenuMapView"]) == true)
                {
                    chkIsShowMenuMapView.Checked = true;
                }
                else
                {
                    chkIsShowMenuMapView.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowMenuShareLocation"]) == true)
                {
                    chkIsShowMenuShareLocation.Checked = true;
                }
                else
                {
                    chkIsShowMenuShareLocation.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowMenuHomeLocation"]) == true)
                {
                    chkIsShowMenuHomeLocation.Checked = true;
                }
                else
                {
                    chkIsShowMenuHomeLocation.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowMenuWeatherDetails"]) == true)
                {
                    chkIsShowMenuWeatherDetails.Checked = true;
                }
                else
                {
                    chkIsShowMenuWeatherDetails.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowMenuChat"]) == true)
                {
                    chkIsShowMenuChat.Checked = true;
                }
                else
                {
                    chkIsShowMenuChat.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowMenuScanQRCode"]) == true)
                {
                    chkIsShowMenuScanQRCode.Checked = true;
                }
                else
                {
                    chkIsShowMenuScanQRCode.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowMenuPermissionInfo"]) == true)
                {
                    chkIsShowMenuPermissionInfo.Checked = true;
                }
                else
                {
                    chkIsShowMenuPermissionInfo.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowMenuAnyDesk"]) == true)
                {
                    chkIsShowMenuAnyDesk.Checked = true;
                }
                else
                {
                    chkIsShowMenuAnyDesk.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsDocRepoFromPortal"]) == true)
                {
                    chkIsDocRepoFromPortal.Checked = true;
                }
                else
                {
                    chkIsDocRepoFromPortal.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsDocRepShareDownloadAllowed"]) == true)
                {
                    chkIsDocRepShareDownloadAllowed.Checked = true;
                }
                else
                {
                    chkIsDocRepShareDownloadAllowed.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsScreenRecorderEnable"]) == true)
                {
                    chkIsScreenRecorderEnable.Checked = true;
                }
                else
                {
                    chkIsScreenRecorderEnable.Checked = false;
                }
                //Add extra Settings 

                //Rev Add new Settings 25-08-2021
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowPartyOnAppDashboard"]) == true)
                {
                    chkIsShowPartyOnAppDashboard.Checked = true;
                }
                else
                {
                    chkIsShowPartyOnAppDashboard.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowAttendanceOnAppDashboard"]) == true)
                {
                    chkIsShowAttendanceOnAppDashboard.Checked = true;
                }
                else
                {
                    chkIsShowAttendanceOnAppDashboard.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowTotalVisitsOnAppDashboard"]) == true)
                {
                    chkIsShowTotalVisitsOnAppDashboard.Checked = true;
                }
                else
                {
                    chkIsShowTotalVisitsOnAppDashboard.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowVisitDurationOnAppDashboard"]) == true)
                {
                    chkIsShowVisitDurationOnAppDashboard.Checked = true;
                }
                else
                {
                    chkIsShowVisitDurationOnAppDashboard.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowDayStart"]) == true)
                {
                    chkIsShowDayStart.Checked = true;
                }
                else
                {
                    chkIsShowDayStart.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsshowDayStartSelfie"]) == true)
                {
                    chkIsshowDayStartSelfie.Checked = true;
                }
                else
                {
                    chkIsshowDayStartSelfie.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowDayEnd"]) == true)
                {
                    chkIsShowDayEnd.Checked = true;
                }
                else
                {
                    chkIsShowDayEnd.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsshowDayEndSelfie"]) == true)
                {
                    chkIsshowDayEndSelfie.Checked = true;
                }
                else
                {
                    chkIsshowDayEndSelfie.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowLeaveInAttendance"]) == true)
                {
                    chkIsShowLeaveInAttendance.Checked = true;
                }
                else
                {
                    chkIsShowLeaveInAttendance.Checked = false;
                }
               
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsLeaveGPSTrack"]) == true)
                {
                    chkIsLeaveGPSTrack.Checked = true;
                }
                else
                {
                    chkIsLeaveGPSTrack.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowActivitiesInTeam"]) == true)
                {
                    chkIsShowActivitiesInTeam.Checked = true;
                }
                else
                {
                    chkIsShowActivitiesInTeam.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowMarkDistVisitOnDshbrd"]) == true)
                {
                    chkIsShowMarkDistVisitOnDshbrd.Checked = true;
                }
                else
                {
                    chkIsShowMarkDistVisitOnDshbrd.Checked = false;
                }
                //Mantis Issue 24408,24364
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsRevisitRemarksMandatory"]) == true)
                {
                    chkIsRevisitRemarksMandatory.Checked = true;
                }
                else
                {
                    chkIsRevisitRemarksMandatory.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["GPSAlert"]) == true)
                {
                    chkGPSAlert.Checked = true;
                    //TdIsGPSAlertwithSound.Style.Add("display", "table-cell");
                }
                else
                {
                    chkGPSAlert.Checked = false;
                    chkGPSAlertwithSound.Checked = false;
                    TdIsGPSAlertwithSound.Style.Add("display", "none");
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["GPSAlert"]) == true && chkGPSAlert.Checked == true && Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["GPSAlertwithSound"]) == true)
                {
                    chkGPSAlertwithSound.Checked = true;
                }
                else
                {
                    chkGPSAlertwithSound.Checked = false;
                }
                //End of Mantis Issue 24408,24364

                // Mantis Issue 24596,24597
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["FaceRegistrationFrontCamera"]) == true)
                {
                    chkFaceRegistrationFrontCamera.Checked = true;
                }
                else
                {
                    chkFaceRegistrationFrontCamera.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["MRPInOrder"]) == true)
                {
                    chkMRPInOrder.Checked = true;
                }
                else
                {
                    chkMRPInOrder.Checked = false;
                }
                // End of Mantis Issue 24596,24597
                //Mantis Issue 25035
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["Showdistributorwisepartyorderreport"]) == true)
                {
                    chkDistributerwisePartyOrderReport.Checked = true;
                }
                else
                {
                    chkDistributerwisePartyOrderReport.Checked = false;
                }
                //End of Mantis Issue 25035
                //Mantis Issue 25116
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["ShowAttednaceClearmenu"]) == true)
                {
                    chkShowAttednaceClearmenu.Checked = true;
                }
                else
                {
                    chkShowAttednaceClearmenu.Checked = false;
                }
                //End of Mantis Issue 25116

                // Mantis Issue 25207
                
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["AllowProfileUpdate"]) == true)
                {
                    chkAllowProfileUpdate.Checked = true;
                }
                else
                {
                    chkAllowProfileUpdate.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["AutoDDSelect"]) == true)
                {
                    chkAutoDDSelect.Checked = true;
                }
                else
                {
                    chkAutoDDSelect.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["BatterySetting"]) == true)
                {
                    chkBatterySetting.Checked = true;
                }
                else
                {
                    chkBatterySetting.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["CommonAINotification"]) == true)
                {
                    chkCommonAINotification.Checked = true;
                }
                else
                {
                    chkCommonAINotification.Checked = false;
                }
                
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["Custom_Configuration"]) == true)
                {
                    chkCustom_Configuration.Checked = true;
                }
                else
                {
                    chkCustom_Configuration.Checked = false;
                }

                
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isAadharRegistered"]) == true)
                {
                    chkisAadharRegistered.Checked = true;
                }
                else
                {
                    chkisAadharRegistered.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsActivateNewOrderScreenwithSize"]) == true)
                {
                    chkIsActivateNewOrderScreenwithSize.Checked = true;
                }
                else
                {
                    chkIsActivateNewOrderScreenwithSize.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsAllowBreakageTracking"]) == true)
                {
                    chkIsAllowBreakageTracking.Checked = true;
                }
                else
                {
                    chkIsAllowBreakageTracking.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsAllowBreakageTrackingunderTeam"]) == true)
                {
                    chkIsAllowBreakageTrackingunderTeam.Checked = true;
                }
                else
                {
                    chkIsAllowBreakageTrackingunderTeam.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsAllowClickForPhotoRegister"]) == true)
                {
                    chkIsAllowClickForPhotoRegister.Checked = true;
                }
                else
                {
                    chkIsAllowClickForPhotoRegister.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsAllowClickForVisit"]) == true)
                {
                    chkIsAllowClickForVisit.Checked = true;
                }
                else
                {
                    chkIsAllowClickForVisit.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsAllowClickForVisitForSpecificUser"]) == true)
                {
                    chkIsAllowClickForVisitForSpecificUser.Checked = true;
                }
                else
                {
                    chkIsAllowClickForVisitForSpecificUser.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsAllowShopStatusUpdate"]) == true)
                {
                    chkIsAllowShopStatusUpdate.Checked = true;
                }
                else
                {
                    chkIsAllowShopStatusUpdate.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsAlternateNoForCustomer"]) == true)
                {
                    chkIsAlternateNoForCustomer.Checked = true;
                }
                else
                {
                    chkIsAlternateNoForCustomer.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsAttendVisitShowInDashboard"]) == true)
                {
                    chkIsAttendVisitShowInDashboard.Checked = true;
                }
                else
                {
                    chkIsAttendVisitShowInDashboard.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsAutoLeadActivityDateTime"]) == true)
                {
                    chkIsAutoLeadActivityDateTime.Checked = true;
                }
                else
                {
                    chkIsAutoLeadActivityDateTime.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsBeatRouteReportAvailableinTeam"]) == true)
                {
                    chkIsBeatRouteReportAvailableinTeam.Checked = true;
                }
                else
                {
                    chkIsBeatRouteReportAvailableinTeam.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsCollectionOrderWise"]) == true)
                {
                    chkIsCollectionOrderWise.Checked = true;
                }
                else
                {
                    chkIsCollectionOrderWise.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsFaceRecognitionOnEyeblink"]) == true)
                {
                    chkIsFaceRecognitionOnEyeblink.Checked = true;
                }
                else
                {
                    chkIsFaceRecognitionOnEyeblink.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["isFaceRegistered"]) == true)
                {
                    chkisFaceRegistered.Checked = true;
                }
                else
                {
                    chkisFaceRegistered.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsFeedbackAvailableInShop"]) == true)
                {
                    chkIsFeedbackAvailableInShop.Checked = true;
                }
                else
                {
                    chkIsFeedbackAvailableInShop.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsFeedbackHistoryActivated"]) == true)
                {
                    chkIsFeedbackHistoryActivated.Checked = true;
                }
                else
                {
                    chkIsFeedbackHistoryActivated.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsFromPortal"]) == true)
                {
                    chkIsFromPortal.Checked = true;
                }
                else
                {
                    chkIsFromPortal.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsIMEICheck"]) == true)
                {
                    chkIsIMEICheck.Checked = true;
                }
                else
                {
                    chkIsIMEICheck.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IslandlineforCustomer"]) == true)
                {
                    chkIslandlineforCustomer.Checked = true;
                }
                else
                {
                    chkIslandlineforCustomer.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsNewQuotationfeatureOn"]) == true)
                {
                    chkIsNewQuotationfeatureOn.Checked = true;
                }
                else
                {
                    chkIsNewQuotationfeatureOn.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsNewQuotationNumberManual"]) == true)
                {
                    chkIsNewQuotationNumberManual.Checked = true;
                }
                else
                {
                    chkIsNewQuotationNumberManual.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsPendingCollectionRequiredUnderTeam"]) == true)
                {
                    chkIsPendingCollectionRequiredUnderTeam.Checked = true;
                }
                else
                {
                    chkIsPendingCollectionRequiredUnderTeam.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsprojectforCustomer"]) == true)
                {
                    chkIsprojectforCustomer.Checked = true;
                }
                else
                {
                    chkIsprojectforCustomer.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsRateEnabledforNewOrderScreenwithSize"]) == true)
                {
                    chkIsRateEnabledforNewOrderScreenwithSize.Checked = true;
                }
                else
                {
                    chkIsRateEnabledforNewOrderScreenwithSize.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsRestrictNearbyGeofence"]) == true)
                {
                    chkIsRestrictNearbyGeofence.Checked = true;
                }
                else
                {
                    chkIsRestrictNearbyGeofence.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsReturnEnableforParty"]) == true)
                {
                    chkIsReturnEnableforParty.Checked = true;
                }
                else
                {
                    chkIsReturnEnableforParty.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowHomeLocationMap"]) == true)
                {
                    chkIsShowHomeLocationMap.Checked = true;
                }
                else
                {
                    chkIsShowHomeLocationMap.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowManualPhotoRegnInApp"]) == true)
                {
                    chkIsShowManualPhotoRegnInApp.Checked = true;
                }
                else
                {
                    chkIsShowManualPhotoRegnInApp.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowMyDetails"]) == true)
                {
                    chkIsShowMyDetails.Checked = true;
                }
                else
                {
                    chkIsShowMyDetails.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowNearByTeam"]) == true)
                {
                    chkIsShowNearByTeam.Checked = true;
                }
                else
                {
                    chkIsShowNearByTeam.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowRepeatOrderinNotification"]) == true)
                {
                    chkIsShowRepeatOrderinNotification.Checked = true;
                }
                else
                {
                    chkIsShowRepeatOrderinNotification.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowRepeatOrdersNotificationinTeam"]) == true)
                {
                    chkIsShowRepeatOrdersNotificationinTeam.Checked = true;
                }
                else
                {
                    chkIsShowRepeatOrdersNotificationinTeam.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowRevisitRemarksPopup"]) == true)
                {
                    chkIsShowRevisitRemarksPopup.Checked = true;
                }
                else
                {
                    chkIsShowRevisitRemarksPopup.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowTypeInRegistration"]) == true)
                {
                    chkIsShowTypeInRegistration.Checked = true;
                }
                else
                {
                    chkIsShowTypeInRegistration.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsTeamAttendance"]) == true)
                {
                    chkIsTeamAttendance.Checked = true;
                }
                else
                {
                    chkIsTeamAttendance.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsTeamAttenWithoutPhoto"]) == true)
                {
                    chkIsTeamAttenWithoutPhoto.Checked = true;
                }
                else
                {
                    chkIsTeamAttenWithoutPhoto.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsWhatsappNoForCustomer"]) == true)
                {
                    chkIsWhatsappNoForCustomer.Checked = true;
                }
                else
                {
                    chkIsWhatsappNoForCustomer.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["Leaveapprovalfromsupervisor"]) == true)
                {
                    chkLeaveapprovalfromsupervisor.Checked = true;
                }
                else
                {
                    chkLeaveapprovalfromsupervisor.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["Leaveapprovalfromsupervisorinteam"]) == true)
                {
                    chkLeaveapprovalfromsupervisorinteam.Checked = true;
                }
                else
                {
                    chkLeaveapprovalfromsupervisorinteam.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["LogoutWithLogFile"]) == true)
                {
                    chkLogoutWithLogFile.Checked = true;
                }
                else
                {
                    chkLogoutWithLogFile.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["MarkAttendNotification"]) == true)
                {
                    chkMarkAttendNotification.Checked = true;
                }
                else
                {
                    chkMarkAttendNotification.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["PartyUpdateAddrMandatory"]) == true)
                {
                    chkPartyUpdateAddrMandatory.Checked = true;
                }
                else
                {
                    chkPartyUpdateAddrMandatory.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["PowerSaverSetting"]) == true)
                {
                    chkPowerSaverSetting.Checked = true;
                }
                else
                {
                    chkPowerSaverSetting.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["ShopScreenAftVisitRevisit"]) == true)
                {
                    chkShopScreenAftVisitRevisit.Checked = true;
                }
                else
                {
                    chkShopScreenAftVisitRevisit.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["Show_App_Logout_Notification"]) == true)
                {
                    chkShow_App_Logout_Notification.Checked = true;
                }
                else
                {
                    chkShow_App_Logout_Notification.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["ShowAmountNewQuotation"]) == true)
                {
                    chkShowAmountNewQuotation.Checked = true;
                }
                else
                {
                    chkShowAmountNewQuotation.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["ShowAutoRevisitInAppMenu"]) == true)
                {
                    chkShowAutoRevisitInAppMenu.Checked = true;
                }
                else
                {
                    chkShowAutoRevisitInAppMenu.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["ShowAutoRevisitInDashboard"]) == true)
                {
                    chkShowAutoRevisitInDashboard.Checked = true;
                }
                else
                {
                    chkShowAutoRevisitInDashboard.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["ShowCollectionAlert"]) == true)
                {
                    chkShowCollectionAlert.Checked = true;
                }
                else
                {
                    chkShowCollectionAlert.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["ShowCollectionOnlywithInvoiceDetails"]) == true)
                {
                    chkShowCollectionOnlywithInvoiceDetails.Checked = true;
                }
                else
                {
                    chkShowCollectionOnlywithInvoiceDetails.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["ShowPurposeInShopVisit"]) == true)
                {
                    chkShowPurposeInShopVisit.Checked = true;
                }
                else
                {
                    chkShowPurposeInShopVisit.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["ShowQuantityNewQuotation"]) == true)
                {
                    chkShowQuantityNewQuotation.Checked = true;
                }
                else
                {
                    chkShowQuantityNewQuotation.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["ShowTotalVisitAppMenu"]) == true)
                {
                    chkShowTotalVisitAppMenu.Checked = true;
                }
                else
                {
                    chkShowTotalVisitAppMenu.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["ShowUserwiseLeadMenu"]) == true)
                {
                    chkShowUserwiseLeadMenu.Checked = true;
                }
                else
                {
                    chkShowUserwiseLeadMenu.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["ShowZeroCollectioninAlert"]) == true)
                {
                    chkShowZeroCollectioninAlert.Checked = true;
                }
                else
                {
                    chkShowZeroCollectioninAlert.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["UpdateOtherID"]) == true)
                {
                    chkUpdateOtherID.Checked = true;
                }
                else
                {
                    chkUpdateOtherID.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["UpdateUserID"]) == true)
                {
                    chkUpdateUserID.Checked = true;
                }
                else
                {
                    chkUpdateUserID.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["UpdateUserName"]) == true)
                {
                    chkUpdateUserName.Checked = true;
                }
                else
                {
                    chkUpdateUserName.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["WillRoomDBShareinLogin"]) == true)
                {
                    chkWillRoomDBShareinLogin.Checked = true;
                }
                else
                {
                    chkWillRoomDBShareinLogin.Checked = false;
                }
                // End of Mantis Issue 25207

                //End of Add New Settings 25-08-2021
                txtshopLocAccuracy.Text = dsUserDetail.Tables[0].Rows[0]["shopLocAccuracy"].ToString();

                txthomeLocDistance.Text = dsUserDetail.Tables[0].Rows[0]["homeLocDistance"].ToString();

                txtDeviceInfoMin.Text = dsUserDetail.Tables[0].Rows[0]["appInfoMins"].ToString();

                //Rev End new seetings add Tanmoy

                if (Convert.ToString(dsUserDetail.Tables[0].Rows[0]["user_maclock"]).Trim() == "Y")
                {
                    chkmac.Checked = true;
                }
                else
                {
                    chkmac.Checked = false;
                }
                // Rev 1.0
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowEmployeePerformance"]) == true)
                {
                    chkShowEmployeePerformance.Checked = true;
                }
                else
                {
                    chkShowEmployeePerformance.Checked = false;
                }
                // End of Rev 1.0
                // Rev 2.0
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowBeatInMenu"]) == true)
                {
                    chkShowBeatInMenu.Checked = true;
                }
                else
                {
                    chkShowBeatInMenu.Checked = false;
                }
                // End of Rev 2.0
                // Rev 3.0
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowWorkType"]) == true)
                {
                    chkShowWorkType.Checked = true;
                }
                else
                {
                    chkShowWorkType.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowMarketSpendTimer"]) == true)
                {
                    chkShowMarketSpendTimer.Checked = true;
                }
                else
                {
                    chkShowMarketSpendTimer.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowUploadImageInAppProfile"]) == true)
                {
                    chkShowUploadImageInAppProfile.Checked = true;
                }
                else
                {
                    chkShowUploadImageInAppProfile.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowCalendar"]) == true)
                {
                    chkShowCalendar.Checked = true;
                }
                else
                {
                    chkShowCalendar.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowCalculator"]) == true)
                {
                    chkShowCalculator.Checked = true;
                }
                else
                {
                    chkShowCalculator.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowInactiveCustomer"]) == true)
                {
                    chkShowInactiveCustomer.Checked = true;
                }
                else
                {
                    chkShowInactiveCustomer.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowAttendanceSummary"]) == true)
                {
                    chkShowAttendanceSummary.Checked = true;
                }
                else
                {
                    chkShowAttendanceSummary.Checked = false;
                }
                // End of Rev 3.0
                // Rev 4.0
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsMenuShowAIMarketAssistant"]) == true)
                {
                    chkMenuShowAIMarketAssistant.Checked = true;
                }
                else
                {
                    chkMenuShowAIMarketAssistant.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsUsbDebuggingRestricted"]) == true)
                {
                    chkUsbDebuggingRestricted.Checked = true;
                }
                else
                {
                    chkUsbDebuggingRestricted.Checked = false;
                }
                // End of Rev 4.0

                // Rev 5.0
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowLatLongInOutletMaster"]) == true)
                {
                    chkShowLatLongInOutletMaster.Checked = true;
                }
                else
                {
                    chkShowLatLongInOutletMaster.Checked = false;
                }
                // End of Rev 5.0

                // Rev 6.0
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsCallLogHistoryActivated"]) == true)
                {
                    chkIsCallLogHistoryActivated.Checked = true;
                }
                else
                {
                    chkIsCallLogHistoryActivated.Checked = false;
                }
                // End of Rev 6.0
                // Rev 7.0
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowMenuCRMContacts"]) == true)
                {
                    chkIsShowMenuCRMContacts.Checked = true;
                }
                else
                {
                    chkIsShowMenuCRMContacts.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsCheckBatteryOptimization"]) == true)
                {
                    chkIsCheckBatteryOptimization.Checked = true;
                }
                else
                {
                    chkIsCheckBatteryOptimization.Checked = false;
                }
                // End of Rev 7.0
                //REV 8.0
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["ShowUserwisePartyWithGeoFence"]) == true)
                {
                    chkShowPartyWithGeoFence.Checked = true;
                }
                else
                {
                    chkShowPartyWithGeoFence.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["ShowUserwisePartyWithCreateOrder"]) == true)
                {
                    chkShowPartyWithCreateOrder.Checked = true;
                }
                else
                {
                    chkShowPartyWithCreateOrder.Checked = false;
                }
                //REV 8.0 END
                // Rev 9.0
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["AdditionalinfoRequiredforContactListing"]) == true)
                {
                    chkAdditionalinfoRequiredforContactListing.Checked = true;
                }
                else
                {
                    chkAdditionalinfoRequiredforContactListing.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["AdditionalinfoRequiredforContactAdd"]) == true)
                {
                    chkAdditionalinfoRequiredforContactAdd.Checked = true;
                }
                else
                {
                    chkAdditionalinfoRequiredforContactAdd.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["ContactAddresswithGeofence"]) == true)
                {
                    chkContactAddresswithGeofence.Checked = true;
                }
                else
                {
                    chkContactAddresswithGeofence.Checked = false;
                }
                // End of Rev 9.0

                // Rev 10.0
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsCRMPhonebookSyncEnable"]) == true)
                {
                    chkIsCRMPhonebookSyncEnable.Checked = true;
                }
                else
                {
                    chkIsCRMPhonebookSyncEnable.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsCRMSchedulerEnable"]) == true)
                {
                    chkIsCRMSchedulerEnable.Checked = true;
                }
                else
                {
                    chkIsCRMSchedulerEnable.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsCRMAddEnable"]) == true)
                {
                    chkIsCRMAddEnable.Checked = true;
                }
                else
                {
                    chkIsCRMAddEnable.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsCRMEditEnable"]) == true)
                {
                    chkIsCRMEditEnable.Checked = true;
                }
                else
                {
                    chkIsCRMEditEnable.Checked = false;
                }
                // End of Rev 10.0
                // Rev 11.0
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowAddressInParty"]) == true)
                {
                    chkIsShowAddressInParty.Checked = true;
                }
                else
                {
                    chkIsShowAddressInParty.Checked = false;
                }

                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowUpdateInvoiceDetails"]) == true)
                {
                    chkIsShowUpdateInvoiceDetails.Checked = true;
                }
                else
                {
                    chkIsShowUpdateInvoiceDetails.Checked = false;
                }
                // End of Rev 11.0
                // Rev 12.0
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsSpecialPriceWithEmployee"]) == true)
                {
                    chkIsSpecialPriceWithEmployee.Checked = true;
                }
                else
                {
                    chkIsSpecialPriceWithEmployee.Checked = false;
                }
                // End of Rev 12.0
                // Rev 13.0
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowCRMOpportunity"]) == true)
                {
                    chkIsShowCRMOpportunity.Checked = true;
                }
                else
                {
                    chkIsShowCRMOpportunity.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsEditEnableforOpportunity"]) == true)
                {
                    chkIsEditEnableforOpportunity.Checked = true;
                }
                else
                {
                    chkIsEditEnableforOpportunity.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsDeleteEnableforOpportunity"]) == true)
                {
                    chkIsDeleteEnableforOpportunity.Checked = true;
                }
                else
                {
                    chkIsDeleteEnableforOpportunity.Checked = false;
                }
                // End of Rev 13.0
                // Rev 14.0
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsShowDateWiseOrderInApp"]) == true)
                {
                    chkIsShowDateWiseOrderInApp.Checked = true;
                }
                else
                {
                    chkIsShowDateWiseOrderInApp.Checked = false;
                }
                // End of Rev 14.0
                // Rev 16.0
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsUserWiseLMSEnable"]) == true)
                {
                    chkIsUserWiseLMSEnable.Checked = true;
                }
                else
                {
                    chkIsUserWiseLMSEnable.Checked = false;
                }
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsUserWiseLMSFeatureOnly"]) == true)
                {
                    chkIsUserWiseLMSFeatureOnly.Checked = true;
                }
                else
                {
                    chkIsUserWiseLMSFeatureOnly.Checked = false;
                }
                // End of Rev 16.0
                // Rev 17.0
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsUserWiseRecordAudioEnableForVisitRevisit"]) == true)
                {
                    chkIsUserWiseRecordAudioEnableForVisitRevisit.Checked = true;
                }
                else
                {
                    chkIsUserWiseRecordAudioEnableForVisitRevisit.Checked = false;
                }
                // End of Rev 17.0
                // Rev 18.0
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["ShowClearQuiz"]) == true)
                {
                    chkDivShowClearQuiz.Checked = true;
                }
                else
                {
                    chkDivShowClearQuiz.Checked = false;
                }
                // End of Rev 18.0
                // Rev 19.0
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["IsAllowProductCurrentStockUpdateFromApp"]) == true)
                {
                    chkIsAllowProductCurrentStockUpdateFromApp.Checked = true;
                }
                else
                {
                    chkIsAllowProductCurrentStockUpdateFromApp.Checked = false;
                }
                // End of Rev 19.0
                // Rev 21.0
                if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["ShowTargetOnApp"]) == true)
                {
                    chkShowTargetOnApp.Checked = true;
                }
                else
                {
                    chkShowTargetOnApp.Checked = false;
                }
                // End of Rev 21.0

                hdnPartyType.Value = dsUserDetail.Tables[1].Rows[0]["Shop_TypeId"].ToString();

                if (Convert.ToString(dsUserDetail.Tables[0].Rows[0]["user_AllowAccessIP"]) != "")
                {
                    string IP = Convert.ToString(dsUserDetail.Tables[0].Rows[0]["user_AllowAccessIP"]);
                    string[] IParray = IP.Split('.');
                    if (IParray.Length == 4)
                    {
                        txtIp1.Text = IParray[0];
                        txtIp2.Text = IParray[1];
                        txtIp3.Text = IParray[2];
                        txtIp4.Text = IParray[3];
                    }
                    if (IParray.Length == 3)
                    {
                        txtIp1.Text = IParray[0];
                        txtIp2.Text = IParray[1];
                        txtIp3.Text = IParray[2];
                    }
                    if (IParray.Length == 2)
                    {
                        txtIp1.Text = IParray[0];
                        txtIp2.Text = IParray[1];
                    }
                    if (IParray.Length == 1)
                    {
                        txtIp1.Text = IParray[0];
                    }
                }
            }

        }
        protected void selectedValue(string str)
        {
            for (int i = 0; i <= grdUserAccess.Rows.Count - 1; i++)
            {
                DropDownList drp = (DropDownList)grdUserAccess.Rows[i].FindControl("drpUserGroup");
                for (int j = 0; j <= drp.Items.Count - 1; j++)
                {
                    string[] s = str.Split(',');
                    for (int k = 0; k < s.Length; k++)
                    {
                        if (s[k].ToString() == drp.Items[j].Value)
                        {
                            CheckBox chk = (CheckBox)grdUserAccess.Rows[i].FindControl("chkSegmentId");
                            chk.Checked = true;
                            drp.SelectedValue = Convert.ToString(drp.Items.FindByValue(s[k].ToString()).Value);
                        }
                    }
                }
            }
        }

        protected void lnkChangePassword_Click(object sender, EventArgs e)
        {
            try
            {
                string ChangePassOfUserId = Convert.ToString(Request.QueryString["id"]);
                Session["ChangePassOfUserid"] = ChangePassOfUserId;
                Response.Redirect("../ToolsUtilities/frmchangeuserspassword.aspx");
            }
            catch { }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string contact;


            //-------------Allow Ip Adress---
            string IP1 = txtIp1.Text.Trim();
            string IP2 = txtIp2.Text.Trim();
            string IP3 = txtIp3.Text.Trim();
            string IP4 = txtIp4.Text.Trim();
            string IPAddress = string.Empty;
            if (IP1 != "")
            {
                IPAddress = IP1;
                if (IP2 != "")
                {
                    IPAddress = IPAddress + "." + IP2;
                    if (IP3 != "")
                    {
                        IPAddress = IPAddress + "." + IP3;
                        if (IP4 != "")
                        {
                            IPAddress = IPAddress + "." + IP4;
                        }
                    }
                }
            }
            //------------------------------



            //if (drpContact.SelectedValue.ToString() != "")
            //{
            //    contact = drpContact.SelectedValue;
            //}
            //else
            //{
            //    contact = hdncontactId.Value.ToString();
            //}

            //if (txtReportTo_hidden.Value.ToString() != "")
            if (txtReportTo_hidden.Value.ToString() != "")
            {
                contact = txtReportTo_hidden.Value;
            }
            else
            {
                contact = txtReportTo_hidden.Value;
            }


            string usergroup = getuserGroup();
            if (usergroup != "" && usergroup != "Select Group")
            {
                //string[,] grpsegment = oDBEngine.GetFieldValue("tbl_master_userGroup", "top 1 grp_segmentid,grp_name", "grp_id in (" + usergroup.ToString() + ")", 2);
                string[,] grpsegment = oDBEngine.GetFieldValue("tbl_master_userGroup", "top 1 grp_segmentid", "grp_id in (" + usergroup.ToString() + ")", 1);
                string[,] segname = oDBEngine.GetFieldValue("tbl_master_segment", "seg_name", "seg_id='" + grpsegment[0, 0] + "'", 1);
                // string[,] BranchId = oDBEngine.GetFieldValue("tbl_master_contact", "cnt_branchid", " cnt_internalId='" + drpContact.SelectedValue.ToString() + "'", 1);
                string[,] BranchId = oDBEngine.GetFieldValue("tbl_master_contact", "top 1 cnt_branchid", " cnt_internalId='" + txtReportTo_hidden.Value.ToString() + "'", 1);
                string b_id = BranchId[0, 0];
                if (b_id == "n")
                {
                    b_id = "1";
                }
                string superuser = "";
                if (cbSuperUser.Checked == true)
                    superuser = "Y";
                else
                    superuser = "N";

                string isactive = "";
                string isactivemac = "";
                int istargetsettings = 0;

                //REV Start new settings add Tanmoy
                int isLeaveApprovalEnable = 0;
                int IsAutoRevisitEnable = 0;
                int IsShowPlanDetails = 0;
                int IsMoreDetailsMandatory = 0;
                int IsShowMoreDetailsMandatory = 0;
                int isMeetingAvailable = 0;
                int isRateNotEditable = 0;

                int IsShowTeamDetails = 0;
                int IsAllowPJPUpdateForTeam = 0;

                int willReportShow = 0;
                int isFingerPrintMandatoryForAttendance = 0;
                int isFingerPrintMandatoryForVisit = 0;
                int isSelfieMandatoryForAttendance = 0;

                int isAttendanceReportShow = 0;
                int isPerformanceReportShow = 0;
                int isVisitReportShow = 0;
                int willTimesheetShow = 0;
                /*Rev work start 11.05.2022 Mantise No:0024880 Horizontal Performance Summary & detail report required with hierarchy setting*/
                int isHorizontalPerformReportShow = 0;
                /*Rev work close 11.05.2022 Mantise No:0024880 Horizontal Performance Summary & detail report required with hierarchy setting*/
                int isAttendanceFeatureOnly = 0;
                int isOrderShow = 0;
                int isVisitShow = 0;
                int iscollectioninMenuShow = 0;
                int isShopAddEditAvailable = 0;
                int isEntityCodeVisible = 0;
                int isAreaMandatoryInPartyCreation = 0;
                int isShowPartyInAreaWiseTeam = 0;
                int isChangePasswordAllowed = 0;
                int isHomeRestrictAttendance = 0;
                int isQuotationShow = 0;
                int IsStateMandatoryinReport = 0;

                int isAchievementEnable = 0;
                int isTarVsAchvEnable = 0;


                int isQuotationPopupShow = 0;
                int isOrderReplacedWithTeam = 0;
                int isMultipleAttendanceSelection = 0;
                int isOfflineTeam = 0;
                int isDDShowForMeeting = 0;
                int isDDMandatoryForMeeting = 0;
                int isAllTeamAvailable = 0;
                int isRecordAudioEnable = 0;
                int isNextVisitDateMandatory = 0;
                int isShowCurrentLocNotifiaction = 0;
                int isUpdateWorkTypeEnable = 0;
                int isLeaveEnable = 0;
                int isOrderMailVisible = 0;
                int LateVisitSMS = 0;
                int isShopEditEnable = 0;
                int isTaskEnable = 0;

                int isAppInfoEnable = 0;
                int willDynamicShow = 0;
                int willActivityShow = 0;
                int isDocumentRepoShow = 0;
                int isChatBotShow = 0;
                int isAttendanceBotShow = 0;
                int isVisitBotShow = 0;


                int isInstrumentCompulsory = 0;
                int isBankCompulsory = 0;


                int isComplementaryUser = 0;
                int isVisitPlanShow = 0;
                int isVisitPlanMandatory = 0;
                int isAttendanceDistanceShow = 0;
                int willTimelineWithFixedLocationShow = 0;
                int isShowOrderRemarks = 0;
                int isShowOrderSignature = 0;
                int isShowSmsForParty = 0;
                int isShowTimeline = 0;
                int willScanVisitingCard = 0;
                int isCreateQrCode = 0;
                int isScanQrForRevisit = 0;
                int isShowLogoutReason = 0;
                int willShowHomeLocReason = 0;
                int willShowShopVisitReason = 0;
                int willShowPartyStatus = 0;
                int willShowEntityTypeforShop = 0;
                int isShowRetailerEntity = 0;
                int isShowDealerForDD = 0;
                int isShowBeatGroup = 0;
                int isShowShopBeatWise = 0;
                int isShowBankDetailsForShop = 0;
                int isShowOTPVerificationPopup = 0;
                int isShowMicroLearing = 0;
                int isMultipleVisitEnable = 0;
                int isShowVisitRemarks = 0;
                int isShowNearbyCustomer = 0;
                int isServiceFeatureEnable = 0;
                int isPatientDetailsShowInOrder = 0;
                int isPatientDetailsShowInCollection = 0;
                int isAttachmentMandatory = 0;
                int isShopImageMandatory = 0;

                int isLogShareinLogin = 0;
                int IsCompetitorenable = 0;
                int IsOrderStatusRequired = 0;
                int IsCurrentStockEnable = 0;
                int IsCurrentStockApplicableforAll = 0;
                int IscompetitorStockRequired = 0;
                int IsCompetitorStockforParty = 0;
                int ShowFaceRegInMenu = 0;
                int IsFaceDetection = 0;
                //int isFaceRegistered = 0;
                int IsUserwiseDistributer = 0;
                int IsPhotoDeleteShow = 0;
                int IsAllDataInPortalwithHeirarchy = 0;
                int IsFaceDetectionWithCaptcha = 0;

                int IsShowMenuAddAttendance = 0;
                int IsShowMenuAttendance = 0;
                int IsShowMenuShops = 0;
                int IsShowMenuOutstandingDetailsPPDD = 0;
                int IsShowMenuStockDetailsPPDD = 0;
                int IsShowMenuTA = 0;
                int IsShowMenuMISReport = 0;
                int IsShowMenuReimbursement = 0;
                int IsShowMenuAchievement = 0;
                int IsShowMenuMapView = 0;
                int IsShowMenuShareLocation = 0;
                int IsShowMenuHomeLocation = 0;
                int IsShowMenuWeatherDetails = 0;
                int IsShowMenuChat = 0;
                int IsShowMenuScanQRCode = 0;
                int IsShowMenuPermissionInfo = 0;
                int IsShowMenuAnyDesk = 0;

                int IsDocRepoFromPortal = 0;
                int IsDocRepShareDownloadAllowed = 0;
                int IsScreenRecorderEnable = 0;
                //REV END new settings add Tanmoy

                //Rev Add new Settings Tanmoy
                int IsShowPartyOnAppDashboard = 0;
                int IsShowAttendanceOnAppDashboard = 0;
                int IsShowTotalVisitsOnAppDashboard = 0;
                int IsShowVisitDurationOnAppDashboard = 0;
                int IsShowDayStart = 0;
                int IsshowDayStartSelfie = 0;
                int IsShowDayEnd = 0;
                int IsshowDayEndSelfie = 0;
                int IsShowLeaveInAttendance = 0;
                int IsLeaveGPSTrack = 0;
                int IsShowActivitiesInTeam = 0;
                int IsShowMarkDistVisitOnDshbrd = 0;
                //Mantis Issue 24408,24364
                int IsRevisitRemarksMandatory = 0;
                int GPSAlert = 0;
                int GPSAlertwithSound = 0;
                //End of Mantis Issue 24408,24364
                //End of Rev Add new Settings Tanmoy
                // Mantis Issue 24596,24597
                int FaceRegistrationFrontCamera = 0;
                int MRPInOrder = 0;
                // End of Mantis Issue 24596,24597
                //Mantis Issue 25015
                int FaceRegTypeID = 0;
                //End of Mantis Issue 25015
                //Mantis Issue 25035
                int DistributerwisePartyOrderReport = 0;
                //End of Mantis Issue 25035
                //Mantis Issue 25116
                int ShowAttednaceClearmenu = 0;
                //End of Mantis Issue 25116
                // Mantis Issue 25207
                int ShowAllowProfileUpdate = 0;
                int ShowAutoDDSelect = 0;
                int ShowBatterySetting = 0;
                int ShowCommonAINotification = 0;
                int ShowCustom_Configuration = 0;
                int ShowisAadharRegistered = 0;
                int ShowIsActivateNewOrderScreenwithSize = 0;
                int ShowIsAllowBreakageTracking = 0;
                int ShowIsAllowBreakageTrackingunderTeam = 0;
                int ShowIsAllowClickForPhotoRegister = 0;
                int ShowIsAllowClickForVisit = 0;
                int ShowIsAllowClickForVisitForSpecificUser = 0;
                int ShowIsAllowShopStatusUpdate = 0;
                int ShowIsAlternateNoForCustomer = 0;
                int ShowIsAttendVisitShowInDashboard = 0;
                int ShowIsAutoLeadActivityDateTime = 0;
                int ShowIsBeatRouteReportAvailableinTeam = 0;
                int ShowIsCollectionOrderWise = 0;
                int ShowIsFaceRecognitionOnEyeblink = 0;
                int ShowisFaceRegistered = 0;
                int ShowIsFeedbackAvailableInShop = 0;
                int ShowIsFeedbackHistoryActivated = 0;
                int ShowIsFromPortal = 0;
                int ShowIsIMEICheck = 0;
                int ShowIslandlineforCustomer = 0;
                int ShowIsNewQuotationfeatureOn = 0;
                int ShowIsNewQuotationNumberManual = 0;
                int ShowIsPendingCollectionRequiredUnderTeam = 0;
                int ShowIsprojectforCustomer = 0;
                int ShowIsRateEnabledforNewOrderScreenwithSize = 0;
                int ShowIsRestrictNearbyGeofence = 0;
                int ShowIsReturnEnableforParty = 0;
                int ShowIsShowHomeLocationMap = 0;
                int ShowIsShowManualPhotoRegnInApp = 0;
                int ShowIsShowMyDetails = 0;
                int ShowIsShowNearByTeam = 0;
                int ShowIsShowRepeatOrderinNotification = 0;
                int ShowIsShowRepeatOrdersNotificationinTeam = 0;
                int ShowIsShowRevisitRemarksPopup = 0;
                int ShowIsShowTypeInRegistration = 0;
                int ShowIsTeamAttendance = 0;
                int ShowIsTeamAttenWithoutPhoto = 0;
                int ShowIsWhatsappNoForCustomer = 0;
                int ShowLeaveapprovalfromsupervisor = 0;
                int ShowLeaveapprovalfromsupervisorinteam = 0;
                int ShowLogoutWithLogFile = 0;
                int ShowMarkAttendNotification = 0;
                int ShowPartyUpdateAddrMandatory = 0;
                int ShowPowerSaverSetting = 0;
                int ShowShopScreenAftVisitRevisit = 0;
                int Show_App_Logout_Notification = 0;
                int ShowAmountNewQuotation = 0;
                int ShowAutoRevisitInAppMenu = 0;
                int ShowAutoRevisitInDashboard = 0;
                int ShowCollectionAlert = 0;
                int ShowCollectionOnlywithInvoiceDetails = 0;
                int ShowPurposeInShopVisit = 0;
                int ShowQuantityNewQuotation = 0;
                int ShowTotalVisitAppMenu = 0;
                int ShowUserwiseLeadMenu = 0;
                int ShowZeroCollectioninAlert = 0;
                int ShowUpdateOtherID = 0;
                int ShowUpdateUserID = 0;
                int ShowUpdateUserName = 0;
                int ShowWillRoomDBShareinLogin = 0;
                // End of Mantis Issue 25207
                // Rev 1.0
                int IsShowEmployeePerformance = 0;
                // End of Rev 1.0
                // Rev 2.0
                int IsShowBeatInMenu = 0;
                // End of Rev 2.0
                // Rev 3.0
                int IsShowWorkType = 0;
                int IsShowMarketSpendTimer = 0;
                int IsShowUploadImageInAppProfile = 0;
                int IsShowCalendar = 0;
                int IsShowCalculator = 0;
                int IsShowInactiveCustomer = 0;
                int IsShowAttendanceSummary = 0;
                // End of Rev 3.0
                // Rev 4.0
                int IsMenuShowAIMarketAssistant = 0;
                int IsUsbDebuggingRestricted = 0;
                // End of Rev 4.0
                // Rev 5.0
                int IsShowLatLongInOutletMaster = 0;
                // End of Rev 5.0
                // Rev 6.0
                int IsCallLogHistoryActivated = 0;
                // End of Rev 6.0
                // Rev 7.0
                int IsShowMenuCRMContacts = 0;
                int IsCheckBatteryOptimization = 0;
                // End of Rev 7.0
                // Rev 8.0
                int ShowUserwisePartyWithGeoFence = 0;
                int ShowUserwisePartyWithCreateOrder = 0;
                // End of Rev 8.0
                // Rev 9.0
                int AdditionalinfoRequiredforContactListing = 0;
                int AdditionalinfoRequiredforContactAdd = 0;
                int ContactAddresswithGeofence = 0;
                // End of Rev 9.0
                
                // Rev 10.0
                int IsCRMPhonebookSyncEnable = 0;
                int IsCRMSchedulerEnable = 0;
                int IsCRMAddEnable = 0;
                int IsCRMEditEnable = 0;
                // End of Rev 10.0
                // Rev 11.0
                int IsShowAddressInParty = 0;
                int IsShowUpdateInvoiceDetails = 0;
                // End of Rev 11.0
                // Rev 12.0
                int IsSpecialPriceWithEmployee = 0;
                // End of Rev 12.0
                // Rev 13.0
                int IsShowCRMOpportunity = 0;
                int IsEditEnableforOpportunity = 0;
                int IsDeleteEnableforOpportunity = 0;
                // End of Rev 13.0
                // Rev 14.0
                int IsShowDateWiseOrderInApp = 0;
                // End of Rev 14.0
                // Rev 16.0
                int IsUserWiseLMSEnable = 0;
                int IsUserWiseLMSFeatureOnly = 0;
                // End of Rev 16.0
                // Rev 17.0
                int IsUserWiseRecordAudioEnableForVisitRevisit = 0;
                // End of Rev 17.0

                // Rev 18.0
                int ShowClearQuiz = 0;
                // End of Rev 18.0
                // Rev 19.0
                int IsAllowProductCurrentStockUpdateFromApp = 0;
                // End of Rev 19.0
                // Rev 21.0
                int ShowTargetOnApp = 0;
                // End of Rev 21.0


                if (chkIsActive.Checked == true)
                    isactive = "Y";
                else
                    isactive = "N";

                if (chkmac.Checked == true)
                    isactivemac = "Y";
                else
                    isactivemac = "N";

                if (chkTargetSettings.Checked == true)
                    istargetsettings = 1;
                else
                    istargetsettings = 0;
                //REV Start new settings add Tanmoy
                if (chkLeaveEanbleSettings.Checked == true)
                    isLeaveApprovalEnable = 1;
                else
                    isLeaveApprovalEnable = 0;

                if (chkRevisitSettings.Checked == true)
                    IsAutoRevisitEnable = 1;
                else
                    IsAutoRevisitEnable = 0;

                if (chkPlanDetailsSettings.Checked == true)
                    IsShowPlanDetails = 1;
                else
                    IsShowPlanDetails = 0;

                if (chkMoreDetailsMandatorySettings.Checked == true)
                    IsMoreDetailsMandatory = 1;
                else
                    IsMoreDetailsMandatory = 0;

                if (chkShowMoreDetailsSettings.Checked == true)
                    IsShowMoreDetailsMandatory = 1;
                else
                    IsShowMoreDetailsMandatory = 0;

                if (chkMeetingsSettings.Checked == true)
                    isMeetingAvailable = 1;
                else
                    isMeetingAvailable = 0;

                if (chkRateEditableSettings.Checked == true)
                    isRateNotEditable = 1;
                else
                    isRateNotEditable = 0;

                if (chkShowTeam.Checked == true)
                    IsShowTeamDetails = 1;
                else
                    IsShowTeamDetails = 0;

                if (chkAllowPJPupdateforTeam.Checked == true)
                    IsAllowPJPUpdateForTeam = 1;
                else
                    IsAllowPJPUpdateForTeam = 0;

                if (chkwillReportShow.Checked == true)
                    willReportShow = 1;
                else
                    willReportShow = 0;

                if (chkIsFingerPrintMandatoryForAttendance.Checked == true)
                    isFingerPrintMandatoryForAttendance = 1;
                else
                    isFingerPrintMandatoryForAttendance = 0;

                if (chkIsFingerPrintMandatoryForVisit.Checked == true)
                    isFingerPrintMandatoryForVisit = 1;
                else
                    isFingerPrintMandatoryForVisit = 0;

                if (chkIsSelfieMandatoryForAttendance.Checked == true)
                    isSelfieMandatoryForAttendance = 1;
                else
                    isSelfieMandatoryForAttendance = 0;


                if (chkisAttendanceReportShow.Checked == true)
                    isAttendanceReportShow = 1;
                else
                    isAttendanceReportShow = 0;

                if (chkisPerformanceReportShow.Checked == true)
                    isPerformanceReportShow = 1;
                else
                    isPerformanceReportShow = 0;

                if (chkisVisitReportShow.Checked == true)
                    isVisitReportShow = 1;
                else
                    isVisitReportShow = 0;

                if (chkwillTimesheetShow.Checked == true)
                    willTimesheetShow = 1;
                else
                    willTimesheetShow = 0;
                /*Rev work start 11.05.2022 Mantise No:0024880 Horizontal Performance Summary & detail report required with hierarchy setting*/
                if (chkisHorizontalReportShow.Checked == true)
                    isHorizontalPerformReportShow = 1;
                else
                    isHorizontalPerformReportShow = 0;
                /*Rev work close 11.05.2022 Mantise No:0024880 Horizontal Performance Summary & detail report required with hierarchy setting*/
                if (chkisAttendanceFeatureOnly.Checked == true)
                    isAttendanceFeatureOnly = 1;
                else
                    isAttendanceFeatureOnly = 0;

                if (chkisOrderShow.Checked == true)
                    isOrderShow = 1;
                else
                    isOrderShow = 0;

                if (chkisVisitShow.Checked == true)
                    isVisitShow = 1;
                else
                    isVisitShow = 0;

                if (chkiscollectioninMenuShow.Checked == true)
                    iscollectioninMenuShow = 1;
                else
                    iscollectioninMenuShow = 0;

                if (chkisShopAddEditAvailable.Checked == true)
                    isShopAddEditAvailable = 1;
                else
                    isShopAddEditAvailable = 0;

                if (chkisEntityCodeVisible.Checked == true)
                    isEntityCodeVisible = 1;
                else
                    isEntityCodeVisible = 0;

                if (chkisAreaMandatoryInPartyCreation.Checked == true)
                    isAreaMandatoryInPartyCreation = 1;
                else
                    isAreaMandatoryInPartyCreation = 0;

                if (chkisShowPartyInAreaWiseTeam.Checked == true)
                    isShowPartyInAreaWiseTeam = 1;
                else
                    isShowPartyInAreaWiseTeam = 0;

                if (chkisChangePasswordAllowed.Checked == true)
                    isChangePasswordAllowed = 1;
                else
                    isChangePasswordAllowed = 0;

                //if (chkisHomeRestrictAttendance.Checked == true)
                //    isHomeRestrictAttendance = 1;
                //else
                //    isHomeRestrictAttendance = 0;
                isHomeRestrictAttendance = Convert.ToInt32(ddlRestrictionHomeLocation.SelectedValue.ToString());
                //Mantis Issue 25015
                string ddlVal = ddlType.SelectedValue.ToString();
                if (ddlVal == "Select Type")
                {
                    FaceRegTypeID = 0;
                }
                else
                {
                    FaceRegTypeID = Convert.ToInt32(ddlType.SelectedValue.ToString());
                }
                //End of Mantis Issue 25015
                if (chkisQuotationShow.Checked == true)
                    isQuotationShow = 1;
                else
                    isQuotationShow = 0;

                if (chkIsStateMandatoryinReport.Checked == true)
                    IsStateMandatoryinReport = 1;
                else
                    IsStateMandatoryinReport = 0;

                if (chkisAchievementEnable.Checked == true)
                    isAchievementEnable = 1;
                else
                    isAchievementEnable = 0;

                if (chkisTarVsAchvEnable.Checked == true)
                    isTarVsAchvEnable = 1;
                else
                    isTarVsAchvEnable = 0;

                //new settings 18-08-2020

                if (chkisQuotationPopupShow.Checked == true)
                    isQuotationPopupShow = 1;
                else
                    isQuotationPopupShow = 0;

                if (chkisOrderReplacedWithTeam.Checked == true)
                    isOrderReplacedWithTeam = 1;
                else
                    isOrderReplacedWithTeam = 0;

                if (chkisMultipleAttendanceSelection.Checked == true)
                    isMultipleAttendanceSelection = 1;
                else
                    isMultipleAttendanceSelection = 0;

                if (chkisOfflineTeam.Checked == true)
                    isOfflineTeam = 1;
                else
                    isOfflineTeam = 0;

                if (chkisDDShowForMeeting.Checked == true)
                    isDDShowForMeeting = 1;
                else
                    isDDShowForMeeting = 0;

                if (chkisDDMandatoryForMeeting.Checked == true)
                    isDDMandatoryForMeeting = 1;
                else
                    isDDMandatoryForMeeting = 0;

                if (chkisAllTeamAvailable.Checked == true)
                    isAllTeamAvailable = 1;
                else
                    isAllTeamAvailable = 0;

                if (chkisRecordAudioEnable.Checked == true)
                    isRecordAudioEnable = 1;
                else
                    isRecordAudioEnable = 0;

                if (chkisNextVisitDateMandatory.Checked == true)
                    isNextVisitDateMandatory = 1;
                else
                    isNextVisitDateMandatory = 0;

                if (chkisShowCurrentLocNotifiaction.Checked == true)
                    isShowCurrentLocNotifiaction = 1;
                else
                    isShowCurrentLocNotifiaction = 0;

                if (chkisUpdateWorkTypeEnable.Checked == true)
                    isUpdateWorkTypeEnable = 1;
                else
                    isUpdateWorkTypeEnable = 0;

                if (chkisLeaveEnable.Checked == true)
                    isLeaveEnable = 1;
                else
                    isLeaveEnable = 0;

                if (chkisOrderMailVisible.Checked == true)
                    isOrderMailVisible = 1;
                else
                    isOrderMailVisible = 0;

                if (chkLateVisitSMS.Checked == true)
                    LateVisitSMS = 1;
                else
                    LateVisitSMS = 0;

                if (chkisShopEditEnable.Checked == true)
                    isShopEditEnable = 1;
                else
                    isShopEditEnable = 0;

                if (chkisTaskEnable.Checked == true)
                    isTaskEnable = 1;
                else
                    isTaskEnable = 0;

                if (chkisAppInfoEnable.Checked == true)
                    isAppInfoEnable = 1;
                else
                    isAppInfoEnable = 0;

                if (chkwillDynamicShow.Checked == true)
                    willDynamicShow = 1;
                else
                    willDynamicShow = 0;

                if (chkwillActivityShow.Checked == true)
                    willActivityShow = 1;
                else
                    willActivityShow = 0;

                if (chkisDocumentRepoShow.Checked == true)
                    isDocumentRepoShow = 1;
                else
                    isDocumentRepoShow = 0;

                if (chkisChatBotShow.Checked == true)
                    isChatBotShow = 1;
                else
                    isChatBotShow = 0;

                if (chkisAttendanceBotShow.Checked == true)
                    isAttendanceBotShow = 1;
                else
                    isAttendanceBotShow = 0;

                if (chkisVisitBotShow.Checked == true)
                    isVisitBotShow = 1;
                else
                    isVisitBotShow = 0;

                if (chkisInstrumentCompulsory.Checked == true)
                    isInstrumentCompulsory = 1;
                else
                    isInstrumentCompulsory = 0;

                if (chkisBankCompulsory.Checked == true)
                    isBankCompulsory = 1;
                else
                    isBankCompulsory = 0;

                //------------------------------------
                if (chkisComplementaryUser.Checked == true)
                    isComplementaryUser = 1;
                else
                    isComplementaryUser = 0;

                if (chkisVisitPlanShow.Checked == true)
                    isVisitPlanShow = 1;
                else
                    isVisitPlanShow = 0;

                if (chkisVisitPlanMandatory.Checked == true)
                    isVisitPlanMandatory = 1;
                else
                    isVisitPlanMandatory = 0;

                if (chkisAttendanceDistanceShow.Checked == true)
                    isAttendanceDistanceShow = 1;
                else
                    isAttendanceDistanceShow = 0;

                if (chkwillTimelineWithFixedLocationShow.Checked == true)
                    willTimelineWithFixedLocationShow = 1;
                else
                    willTimelineWithFixedLocationShow = 0;

                if (chkisShowOrderRemarks.Checked == true)
                    isShowOrderRemarks = 1;
                else
                    isShowOrderRemarks = 0;

                if (chkisShowOrderSignature.Checked == true)
                    isShowOrderSignature = 1;
                else
                    isShowOrderSignature = 0;

                if (chkisShowSmsForParty.Checked == true)
                    isShowSmsForParty = 1;
                else
                    isShowSmsForParty = 0;

                if (chkisShowTimeline.Checked == true)
                    isShowTimeline = 1;
                else
                    isShowTimeline = 0;

                if (chkwillScanVisitingCard.Checked == true)
                    willScanVisitingCard = 1;
                else
                    willScanVisitingCard = 0;

                if (chkisCreateQrCode.Checked == true)
                    isCreateQrCode = 1;
                else
                    isCreateQrCode = 0;

                if (chkisScanQrForRevisit.Checked == true)
                    isScanQrForRevisit = 1;
                else
                    isScanQrForRevisit = 0;

                if (chkisShowLogoutReason.Checked == true)
                    isShowLogoutReason = 1;
                else
                    isShowLogoutReason = 0;

                if (chkwillShowHomeLocReason.Checked == true)
                    willShowHomeLocReason = 1;
                else
                    willShowHomeLocReason = 0;

                if (chkwillShowShopVisitReason.Checked == true)
                    willShowShopVisitReason = 1;
                else
                    willShowShopVisitReason = 0;

                if (chkwillShowPartyStatus.Checked == true)
                    willShowPartyStatus = 1;
                else
                    willShowPartyStatus = 0;

                if (chkwillShowEntityTypeforShop.Checked == true)
                    willShowEntityTypeforShop = 1;
                else
                    willShowEntityTypeforShop = 0;

                if (chkisShowRetailerEntity.Checked == true)
                    isShowRetailerEntity = 1;
                else
                    isShowRetailerEntity = 0;

                if (chkisShowDealerForDD.Checked == true)
                    isShowDealerForDD = 1;
                else
                    isShowDealerForDD = 0;

                if (chkisShowBeatGroup.Checked == true)
                    isShowBeatGroup = 1;
                else
                    isShowBeatGroup = 0;

                if (chkisShowShopBeatWise.Checked == true)
                    isShowShopBeatWise = 1;
                else
                    isShowShopBeatWise = 0;

                if (chkisShowOTPVerificationPopup.Checked == true)
                    isShowOTPVerificationPopup = 1;
                else
                    isShowOTPVerificationPopup = 0;

                if (chkisShowMicroLearing.Checked == true)
                    isShowMicroLearing = 1;
                else
                    isShowMicroLearing = 0;

                if (chkisMultipleVisitEnable.Checked == true)
                    isMultipleVisitEnable = 1;
                else
                    isMultipleVisitEnable = 0;

                if (chkisShowVisitRemarks.Checked == true)
                    isShowVisitRemarks = 1;
                else
                    isShowVisitRemarks = 0;

                if (chkisShowNearbyCustomer.Checked == true)
                    isShowNearbyCustomer = 1;
                else
                    isShowNearbyCustomer = 0;

                if (chkisServiceFeatureEnable.Checked == true)
                    isServiceFeatureEnable = 1;
                else
                    isServiceFeatureEnable = 0;

                if (chkisPatientDetailsShowInOrder.Checked == true)
                    isPatientDetailsShowInOrder = 1;
                else
                    isPatientDetailsShowInOrder = 0;

                if (chkisPatientDetailsShowInCollection.Checked == true)
                    isPatientDetailsShowInCollection = 1;
                else
                    isPatientDetailsShowInCollection = 0;

                if (chkisAttachmentMandatory.Checked == true)
                    isAttachmentMandatory = 1;
                else
                    isAttachmentMandatory = 0;

                if (chkisShopImageMandatory.Checked == true)
                    isShopImageMandatory = 1;
                else
                    isShopImageMandatory = 0;


                if (chkisLogShareinLogin.Checked == true)
                    isLogShareinLogin = 1;
                else
                    isLogShareinLogin = 0;

                if (chkIsCompetitorenable.Checked == true)
                    IsCompetitorenable = 1;
                else
                    IsCompetitorenable = 0;

                if (chkIsOrderStatusRequired.Checked == true)
                    IsOrderStatusRequired = 1;
                else
                    IsOrderStatusRequired = 0;

                if (chkIsCurrentStockEnable.Checked == true)
                    IsCurrentStockEnable = 1;
                else
                    IsCurrentStockEnable = 0;

                if (chkIsCurrentStockApplicableforAll.Checked == true)
                    IsCurrentStockApplicableforAll = 1;
                else
                    IsCurrentStockApplicableforAll = 0;

                if (chkIscompetitorStockRequired.Checked == true)
                    IscompetitorStockRequired = 1;
                else
                    IscompetitorStockRequired = 0;

                if (chkIsCompetitorStockforParty.Checked == true)
                    IsCompetitorStockforParty = 1;
                else
                    IsCompetitorStockforParty = 0;

                if (chkShowFaceRegInMenu.Checked == true)
                    ShowFaceRegInMenu = 1;
                else
                    ShowFaceRegInMenu = 0;

                if (chkIsFaceDetection.Checked == true)
                    IsFaceDetection = 1;
                else
                    IsFaceDetection = 0;

                //if (chkisFaceRegistered.Checked == true)
                //    isFaceRegistered = 1;
                //else
                //    isFaceRegistered = 0;

                if (chkIsUserwiseDistributer.Checked == true)
                    IsUserwiseDistributer = 1;
                else
                    IsUserwiseDistributer = 0;

                if (chkIsPhotoDeleteShow.Checked == true)
                    IsPhotoDeleteShow = 1;
                else
                    IsPhotoDeleteShow = 0;

                if (chkIsAllDataInPortalwithHeirarchy.Checked == true)
                    IsAllDataInPortalwithHeirarchy = 1;
                else
                    IsAllDataInPortalwithHeirarchy = 0;

                if (chkIsFaceDetectionWithCaptcha.Checked == true)
                    IsFaceDetectionWithCaptcha = 1;
                else
                    IsFaceDetectionWithCaptcha = 0;




                if (chkIsShowMenuAddAttendance.Checked == true)
                    IsShowMenuAddAttendance = 1;
                else
                    IsShowMenuAddAttendance = 0;

                if (chkIsShowMenuAttendance.Checked == true)
                    IsShowMenuAttendance = 1;
                else
                    IsShowMenuAttendance = 0;

                if (chkIsShowMenuShops.Checked == true)
                    IsShowMenuShops = 1;
                else
                    IsShowMenuShops = 0;

                if (chkIsShowMenuOutstandingDetailsPPDD.Checked == true)
                    IsShowMenuOutstandingDetailsPPDD = 1;
                else
                    IsShowMenuOutstandingDetailsPPDD = 0;

                if (chkIsShowMenuStockDetailsPPDD.Checked == true)
                    IsShowMenuStockDetailsPPDD = 1;
                else
                    IsShowMenuStockDetailsPPDD = 0;

                if (chkIsShowMenuTA.Checked == true)
                    IsShowMenuTA = 1;
                else
                    IsShowMenuTA = 0;

                if (chkIsShowMenuMISReport.Checked == true)
                    IsShowMenuMISReport = 1;
                else
                    IsShowMenuMISReport = 0;

                if (chkIsShowMenuReimbursement.Checked == true)
                    IsShowMenuReimbursement = 1;
                else
                    IsShowMenuReimbursement = 0;

                if (chkIsShowMenuAchievement.Checked == true)
                    IsShowMenuAchievement = 1;
                else
                    IsShowMenuAchievement = 0;

                if (chkIsShowMenuMapView.Checked == true)
                    IsShowMenuMapView = 1;
                else
                    IsShowMenuMapView = 0;

                if (chkIsShowMenuShareLocation.Checked == true)
                    IsShowMenuShareLocation = 1;
                else
                    IsShowMenuShareLocation = 0;

                if (chkIsShowMenuHomeLocation.Checked == true)
                    IsShowMenuHomeLocation = 1;
                else
                    IsShowMenuHomeLocation = 0;

                if (chkIsShowMenuWeatherDetails.Checked == true)
                    IsShowMenuWeatherDetails = 1;
                else
                    IsShowMenuWeatherDetails = 0;

                if (chkIsShowMenuChat.Checked == true)
                    IsShowMenuChat = 1;
                else
                    IsShowMenuChat = 0;

                if (chkIsShowMenuScanQRCode.Checked == true)
                    IsShowMenuScanQRCode = 1;
                else
                    IsShowMenuScanQRCode = 0;

                if (chkIsShowMenuPermissionInfo.Checked == true)
                    IsShowMenuPermissionInfo = 1;
                else
                    IsShowMenuPermissionInfo = 0;

                if (chkIsShowMenuAnyDesk.Checked == true)
                    IsShowMenuAnyDesk = 1;
                else
                    IsShowMenuAnyDesk = 0;

                if (chkIsDocRepoFromPortal.Checked == true)
                    IsDocRepoFromPortal = 1;
                else
                    IsDocRepoFromPortal = 0;

                if (chkIsDocRepShareDownloadAllowed.Checked == true)
                    IsDocRepShareDownloadAllowed = 1;
                else
                    IsDocRepShareDownloadAllowed = 0;

                if (chkIsScreenRecorderEnable.Checked == true)
                    IsScreenRecorderEnable = 1;
                else
                    IsScreenRecorderEnable = 0;
                //REV END new settings add Tanmoy

                //Rev Add new Settings Tanmoy
                if (chkIsShowPartyOnAppDashboard.Checked == true)
                    // Mantis Issue 24362
                    //IsShowPartyOnAppDashboard = 0;
                    IsShowPartyOnAppDashboard = 1;
                // End of Mantis Issue 24362
                else
                    IsShowPartyOnAppDashboard = 0;

                if (chkIsShowAttendanceOnAppDashboard.Checked == true)
                    IsShowAttendanceOnAppDashboard = 1;
                else
                    IsShowAttendanceOnAppDashboard = 0;

                if (chkIsShowTotalVisitsOnAppDashboard.Checked == true)
                    IsShowTotalVisitsOnAppDashboard = 1;
                else
                    IsShowTotalVisitsOnAppDashboard = 0;

                if (chkIsShowVisitDurationOnAppDashboard.Checked == true)
                    IsShowVisitDurationOnAppDashboard = 1;
                else
                    IsShowVisitDurationOnAppDashboard = 0;

                if (chkIsShowDayStart.Checked == true)
                    IsShowDayStart = 1;
                else
                    IsShowDayStart = 0;

                if (chkIsshowDayStartSelfie.Checked == true)
                    IsshowDayStartSelfie = 1;
                else
                    IsshowDayStartSelfie = 0;

                if (chkIsShowDayEnd.Checked == true)
                    IsShowDayEnd = 1;
                else
                    IsShowDayEnd = 0;

                if (chkIsshowDayEndSelfie.Checked == true)
                    IsshowDayEndSelfie = 1;
                else
                    IsshowDayEndSelfie = 0;

                if (chkIsShowLeaveInAttendance.Checked == true)
                    IsShowLeaveInAttendance = 1;
                else
                    IsShowLeaveInAttendance = 0;


                if (chkIsLeaveGPSTrack.Checked == true)
                    IsLeaveGPSTrack = 1;
                else
                    IsLeaveGPSTrack = 0;

                if (chkIsShowActivitiesInTeam.Checked == true)
                    IsShowActivitiesInTeam = 1;
                else
                    IsShowActivitiesInTeam = 0;

                if (chkIsShowMarkDistVisitOnDshbrd.Checked == true)
                    IsShowMarkDistVisitOnDshbrd = 1;
                else
                    IsShowMarkDistVisitOnDshbrd = 0;
                //End of Rev Add new Settings Tanmoy
                //Mantis Issue 24408,24364
                if (chkIsRevisitRemarksMandatory.Checked == true)
                    IsRevisitRemarksMandatory = 1;
                else
                    IsRevisitRemarksMandatory = 0;
                if (chkGPSAlert.Checked == true)
                    GPSAlert = 1;
                else
                    GPSAlert = 0;
                if (chkGPSAlertwithSound.Checked == true)
                    GPSAlertwithSound = 1;
                else
                    GPSAlertwithSound = 0;
                //End of Mantis Issue 24408,24364
                // Mantis Issue 24596,24597
                if (chkFaceRegistrationFrontCamera.Checked == true)
                    FaceRegistrationFrontCamera = 1;
                else
                    FaceRegistrationFrontCamera = 0;

                if (chkMRPInOrder.Checked == true)
                    MRPInOrder = 1;
                else
                    MRPInOrder = 0;
                // End of Mantis Issue 24596,24597
                //Mantis Issue 25035
                if (chkDistributerwisePartyOrderReport.Checked == true)
                    DistributerwisePartyOrderReport = 1;
                else
                    DistributerwisePartyOrderReport = 0;
                //End of Mantis Issue 25035
                //Mantis Issue 25116
                if (chkShowAttednaceClearmenu.Checked == true)
                    ShowAttednaceClearmenu = 1;
                else
                    ShowAttednaceClearmenu = 0;
                //End of Mantis Issue 25116

                // Mantis Issue 25207
                if (chkAllowProfileUpdate.Checked == true)
                {
                    ShowAllowProfileUpdate = 1;
                }
                else
                {
                    ShowAllowProfileUpdate = 0;
                }

                if (chkAutoDDSelect.Checked == true)
                {
                    ShowAutoDDSelect = 1;
                }
                else
                {
                    ShowAutoDDSelect = 0;
                }

                if (chkBatterySetting.Checked == true)
                {
                    ShowBatterySetting = 1;
                }
                else
                {
                    ShowBatterySetting = 0;
                }

                if (chkCommonAINotification.Checked == true)
                {
                    ShowCommonAINotification = 1;
                }
                else
                {
                    ShowCommonAINotification = 0;
                }

                if (chkCustom_Configuration.Checked == true)
                {
                    ShowCustom_Configuration = 1;
                }
                else
                {
                    ShowCustom_Configuration = 0;
                }

                if (chkisAadharRegistered.Checked == true)
                {
                    ShowisAadharRegistered = 1;
                }
                else
                {
                    ShowisAadharRegistered = 0;
                }

                if (chkIsActivateNewOrderScreenwithSize.Checked == true)
                {
                    ShowIsActivateNewOrderScreenwithSize = 1;
                }
                else
                {
                    ShowIsActivateNewOrderScreenwithSize = 0;
                }

                if (chkIsAllowBreakageTracking.Checked == true)
                {
                    ShowIsAllowBreakageTracking = 1;
                }
                else
                {
                    ShowIsAllowBreakageTracking = 0;
                }

                if (chkIsAllowBreakageTrackingunderTeam.Checked == true)
                {
                    ShowIsAllowBreakageTrackingunderTeam = 1;
                }
                else
                {
                    ShowIsAllowBreakageTrackingunderTeam = 0;
                }

                if (chkIsAllowClickForPhotoRegister.Checked == true)
                {
                    ShowIsAllowClickForPhotoRegister = 1;
                }
                else
                {
                    ShowIsAllowClickForPhotoRegister = 0;
                }

                if (chkIsAllowClickForVisit.Checked == true)
                {
                    ShowIsAllowClickForVisit = 1;
                }
                else
                {
                    ShowIsAllowClickForVisit = 0;
                }

                if (chkIsAllowClickForVisitForSpecificUser.Checked == true)
                {
                    ShowIsAllowClickForVisitForSpecificUser = 1;
                }
                else
                {
                    ShowIsAllowClickForVisitForSpecificUser = 0;
                }

                if (chkIsAllowShopStatusUpdate.Checked == true)
                {
                    ShowIsAllowShopStatusUpdate = 1;
                }
                else
                {
                    ShowIsAllowShopStatusUpdate = 0;
                }

                if (chkIsAlternateNoForCustomer.Checked == true)
                {
                    ShowIsAlternateNoForCustomer = 1;
                }
                else
                {
                    ShowIsAlternateNoForCustomer = 0;
                }

                if (chkIsAttendVisitShowInDashboard.Checked == true)
                {
                    ShowIsAttendVisitShowInDashboard = 1;
                }
                else
                {
                    ShowIsAttendVisitShowInDashboard = 0;
                }

                if (chkIsAutoLeadActivityDateTime.Checked == true)
                {
                    ShowIsAutoLeadActivityDateTime = 1;
                }
                else
                {
                    ShowIsAutoLeadActivityDateTime = 0;
                }

                if (chkIsBeatRouteReportAvailableinTeam.Checked == true)
                {
                    ShowIsBeatRouteReportAvailableinTeam = 1;
                }
                else
                {
                    ShowIsBeatRouteReportAvailableinTeam = 0;
                }

                if (chkIsCollectionOrderWise.Checked == true)
                {
                    ShowIsCollectionOrderWise = 1;
                }
                else
                {
                    ShowIsCollectionOrderWise = 0;
                }

                if (chkIsFaceRecognitionOnEyeblink.Checked == true)
                {
                    ShowIsFaceRecognitionOnEyeblink = 1;
                }
                else
                {
                    ShowIsFaceRecognitionOnEyeblink = 0;
                }

                if (chkisFaceRegistered.Checked == true)
                {
                    ShowisFaceRegistered = 1;
                }
                else
                {
                    ShowisFaceRegistered = 0;
                }

                if (chkIsFeedbackAvailableInShop.Checked == true)
                {
                    ShowIsFeedbackAvailableInShop = 1;
                }
                else
                {
                    ShowIsFeedbackAvailableInShop = 0;
                }

                if (chkIsFeedbackHistoryActivated.Checked == true)
                {
                    ShowIsFeedbackHistoryActivated = 1;
                }
                else
                {
                    ShowIsFeedbackHistoryActivated = 0;
                }

                if (chkIsFromPortal.Checked == true)
                {
                    ShowIsFromPortal = 1;
                }
                else
                {
                    ShowIsFromPortal = 0;
                }

                if (chkIsIMEICheck.Checked == true)
                {
                    ShowIsIMEICheck = 1;
                }
                else
                {
                    ShowIsIMEICheck = 0;
                }

                if (chkIslandlineforCustomer.Checked == true)
                {
                    ShowIslandlineforCustomer = 1;
                }
                else
                {
                    ShowIslandlineforCustomer = 0;
                }

                if (chkIsNewQuotationfeatureOn.Checked == true)
                {
                    ShowIsNewQuotationfeatureOn = 1;
                }
                else
                {
                    ShowIsNewQuotationfeatureOn = 0;
                }

                if (chkIsNewQuotationNumberManual.Checked == true)
                {
                    ShowIsNewQuotationNumberManual = 1;
                }
                else
                {
                    ShowIsNewQuotationNumberManual = 0;
                }

                if (chkIsPendingCollectionRequiredUnderTeam.Checked == true)
                {
                    ShowIsPendingCollectionRequiredUnderTeam = 1;
                }
                else
                {
                    ShowIsPendingCollectionRequiredUnderTeam = 0;
                }

                if (chkIsprojectforCustomer.Checked == true)
                {
                    ShowIsprojectforCustomer = 1;
                }
                else
                {
                    ShowIsprojectforCustomer = 0;
                }

                if (chkIsRateEnabledforNewOrderScreenwithSize.Checked == true)
                {
                    ShowIsRateEnabledforNewOrderScreenwithSize = 1;
                }
                else
                {
                    ShowIsRateEnabledforNewOrderScreenwithSize = 0;
                }

                if (chkIsRestrictNearbyGeofence.Checked == true)
                {
                    ShowIsRestrictNearbyGeofence = 1;
                }
                else
                {
                    ShowIsRestrictNearbyGeofence = 0;
                }

                if (chkIsReturnEnableforParty.Checked == true)
                {
                    ShowIsReturnEnableforParty = 1;
                }
                else
                {
                    ShowIsReturnEnableforParty = 0;
                }

                if (chkIsShowHomeLocationMap.Checked == true)
                {
                    ShowIsShowHomeLocationMap = 1;
                }
                else
                {
                    ShowIsShowHomeLocationMap = 0;
                }

                if (chkIsShowManualPhotoRegnInApp.Checked == true)
                {
                    ShowIsShowManualPhotoRegnInApp = 1;
                }
                else
                {
                    ShowIsShowManualPhotoRegnInApp = 0;
                }

                if (chkIsShowMyDetails.Checked == true)
                {
                    ShowIsShowMyDetails = 1;
                }
                else
                {
                    ShowIsShowMyDetails = 0;
                }

                if (chkIsShowNearByTeam.Checked == true)
                {
                    ShowIsShowNearByTeam = 1;
                }
                else
                {
                    ShowIsShowNearByTeam = 0;
                }

                if (chkIsShowRepeatOrderinNotification.Checked == true)
                {
                    ShowIsShowRepeatOrderinNotification = 1;
                }
                else
                {
                    ShowIsShowRepeatOrderinNotification = 0;
                }

                if (chkIsShowRepeatOrdersNotificationinTeam.Checked == true)
                {
                    ShowIsShowRepeatOrdersNotificationinTeam = 1;
                }
                else
                {
                    ShowIsShowRepeatOrdersNotificationinTeam = 0;
                }

                if (chkIsShowRevisitRemarksPopup.Checked == true)
                {
                    ShowIsShowRevisitRemarksPopup = 1;
                }
                else
                {
                    ShowIsShowRevisitRemarksPopup = 0;
                }

                if (chkIsShowTypeInRegistration.Checked == true)
                {
                    ShowIsShowTypeInRegistration = 1;
                }
                else
                {
                    ShowIsShowTypeInRegistration = 0;
                }

                if (chkIsTeamAttendance.Checked == true)
                {
                    ShowIsTeamAttendance = 1;
                }
                else
                {
                    ShowIsTeamAttendance = 0;
                }

                if (chkIsTeamAttenWithoutPhoto.Checked == true)
                {
                    ShowIsTeamAttenWithoutPhoto = 1;
                }
                else
                {
                    ShowIsTeamAttenWithoutPhoto = 0;
                }

                if (chkIsWhatsappNoForCustomer.Checked == true)
                {
                    ShowIsWhatsappNoForCustomer = 1;
                }
                else
                {
                    ShowIsWhatsappNoForCustomer = 0;
                }

                if (chkLeaveapprovalfromsupervisor.Checked == true)
                {
                    ShowLeaveapprovalfromsupervisor = 1;
                }
                else
                {
                    ShowLeaveapprovalfromsupervisor = 0;
                }

                if (chkLeaveapprovalfromsupervisorinteam.Checked == true)
                {
                    ShowLeaveapprovalfromsupervisorinteam = 1;
                }
                else
                {
                    ShowLeaveapprovalfromsupervisorinteam = 0;
                }

                if (chkLogoutWithLogFile.Checked == true)
                {
                    ShowLogoutWithLogFile = 1;
                }
                else
                {
                    ShowLogoutWithLogFile = 0;
                }

                if (chkMarkAttendNotification.Checked == true)
                {
                    ShowMarkAttendNotification = 1;
                }
                else
                {
                    ShowMarkAttendNotification = 0;
                }

                if (chkPartyUpdateAddrMandatory.Checked == true)
                {
                    ShowPartyUpdateAddrMandatory = 1;
                }
                else
                {
                    ShowPartyUpdateAddrMandatory = 0;
                }

                if (chkPowerSaverSetting.Checked == true)
                {
                    ShowPowerSaverSetting = 1;
                }
                else
                {
                    ShowPowerSaverSetting = 0;
                }

                if (chkShopScreenAftVisitRevisit.Checked == true)
                {
                    ShowShopScreenAftVisitRevisit = 1;
                }
                else
                {
                    ShowShopScreenAftVisitRevisit = 0;
                }

                if (chkShow_App_Logout_Notification.Checked == true)
                {
                    Show_App_Logout_Notification = 1;
                }
                else
                {
                    Show_App_Logout_Notification = 0;
                }

                if (chkShowAmountNewQuotation.Checked == true)
                {
                    ShowAmountNewQuotation = 1;
                }
                else
                {
                    ShowAmountNewQuotation = 0;
                }

                if (chkShowAutoRevisitInAppMenu.Checked == true)
                {
                    ShowAutoRevisitInAppMenu = 1;
                }
                else
                {
                    ShowAutoRevisitInAppMenu = 0;
                }

                if (chkShowAutoRevisitInDashboard.Checked == true)
                {
                    ShowAutoRevisitInDashboard = 1;
                }
                else
                {
                    ShowAutoRevisitInDashboard = 0;
                }

                if (chkShowCollectionAlert.Checked == true)
                {
                    ShowCollectionAlert = 1;
                }
                else
                {
                    ShowCollectionAlert = 0;
                }

                if (chkShowCollectionOnlywithInvoiceDetails.Checked == true)
                {
                    ShowCollectionOnlywithInvoiceDetails = 1;
                }
                else
                {
                    ShowCollectionOnlywithInvoiceDetails = 0;
                }

                if (chkShowPurposeInShopVisit.Checked == true)
                {
                    ShowPurposeInShopVisit = 1;
                }
                else
                {
                    ShowPurposeInShopVisit = 0;
                }

                if (chkShowQuantityNewQuotation.Checked == true)
                {
                    ShowQuantityNewQuotation = 1;
                }
                else
                {
                    ShowQuantityNewQuotation = 0;
                }

                if (chkShowTotalVisitAppMenu.Checked == true)
                {
                    ShowTotalVisitAppMenu = 1;
                }
                else
                {
                    ShowTotalVisitAppMenu = 0;
                }

                if (chkShowUserwiseLeadMenu.Checked == true)
                {
                    ShowUserwiseLeadMenu = 1;
                }
                else
                {
                    ShowUserwiseLeadMenu = 0;
                }

                if (chkShowZeroCollectioninAlert.Checked == true)
                {
                    ShowZeroCollectioninAlert = 1;
                }
                else
                {
                    ShowZeroCollectioninAlert = 0;
                }

                if (chkUpdateOtherID.Checked == true)
                {
                    ShowUpdateOtherID = 1;
                }
                else
                {
                    ShowUpdateOtherID = 0;
                }

                if (chkUpdateUserID.Checked == true)
                {
                    ShowUpdateUserID = 1;
                }
                else
                {
                    ShowUpdateUserID = 0;
                }

                if (chkUpdateUserName.Checked == true)
                {
                    ShowUpdateUserName = 1;
                }
                else
                {
                    ShowUpdateUserName = 0;
                }

                if (chkWillRoomDBShareinLogin.Checked == true)
                {
                    ShowWillRoomDBShareinLogin = 1;
                }
                else
                {
                    ShowWillRoomDBShareinLogin = 0;
                }
                // End of Mantis Issue 25207
                // Rev 1.0
                if (chkShowEmployeePerformance.Checked == true)
                    IsShowEmployeePerformance = 1;
                else
                    IsShowEmployeePerformance = 0;
                // End of Rev 1.0
                // Rev 2.0
                if (chkShowBeatInMenu.Checked == true)
                    IsShowBeatInMenu = 1;
                else
                    IsShowBeatInMenu = 0;
                // End of Rev 2.0
                // Rev 3.0
                if (chkShowWorkType.Checked == true)
                    IsShowWorkType = 1;
                else
                    IsShowWorkType = 0;

                if (chkShowMarketSpendTimer.Checked == true)
                    IsShowMarketSpendTimer = 1;
                else
                    IsShowMarketSpendTimer = 0;

                if (chkShowUploadImageInAppProfile.Checked == true)
                    IsShowUploadImageInAppProfile = 1;
                else
                    IsShowUploadImageInAppProfile = 0;

                if (chkShowCalendar.Checked == true)
                    IsShowCalendar = 1;
                else
                    IsShowCalendar = 0;

                if (chkShowCalculator.Checked == true)
                    IsShowCalculator = 1;
                else
                    IsShowCalculator = 0;

                if (chkShowInactiveCustomer.Checked == true)
                    IsShowInactiveCustomer = 1;
                else
                    IsShowInactiveCustomer = 0;

                if (chkShowAttendanceSummary.Checked == true)
                    IsShowAttendanceSummary = 1;
                else
                    IsShowAttendanceSummary = 0;
                // End of Rev 3.0
                // Rev 4.0
                if (chkMenuShowAIMarketAssistant.Checked == true)
                    IsMenuShowAIMarketAssistant = 1;
                else
                    IsMenuShowAIMarketAssistant = 0;

                if (chkUsbDebuggingRestricted.Checked == true)
                    IsUsbDebuggingRestricted = 1;
                else
                    IsUsbDebuggingRestricted = 0;
                // End of Rev 4.0
                // Rev 5.0
                if (chkShowLatLongInOutletMaster.Checked == true)
                    IsShowLatLongInOutletMaster = 1;
                else
                    IsShowLatLongInOutletMaster = 0;
                // End of Rev 5.0
                // Rev 6.0
                if (chkIsCallLogHistoryActivated.Checked == true)
                    IsCallLogHistoryActivated = 1;
                else
                    IsCallLogHistoryActivated = 0;
                // End of Rev 6.0
                // Rev 7.0
                if (chkIsShowMenuCRMContacts.Checked == true)
                    IsShowMenuCRMContacts = 1;
                else
                    IsShowMenuCRMContacts = 0;

                if (chkIsCheckBatteryOptimization.Checked == true)
                    IsCheckBatteryOptimization = 1;
                else
                    IsCheckBatteryOptimization = 0;
                // End of Rev 7.0

                // Rev 8.0
                if (chkShowPartyWithGeoFence.Checked == true)
                    ShowUserwisePartyWithGeoFence = 1;
                else
                    ShowUserwisePartyWithGeoFence = 0;

                if (chkShowPartyWithCreateOrder.Checked == true)
                    ShowUserwisePartyWithCreateOrder = 1;
                else
                    ShowUserwisePartyWithCreateOrder = 0;
                // End of Rev 8.0

                // Rev 9.0
                if (chkAdditionalinfoRequiredforContactListing.Checked == true)
                    AdditionalinfoRequiredforContactListing = 1;
                else
                    AdditionalinfoRequiredforContactListing = 0;

                if (chkAdditionalinfoRequiredforContactAdd.Checked == true)
                    AdditionalinfoRequiredforContactAdd = 1;
                else
                    AdditionalinfoRequiredforContactAdd = 0;

                if (chkContactAddresswithGeofence.Checked == true)
                    ContactAddresswithGeofence = 1;
                else
                    ContactAddresswithGeofence = 0;
                // End of Rev 9.0

                // Rev 10.0
                if (chkIsCRMPhonebookSyncEnable.Checked == true)
                {
                    IsCRMPhonebookSyncEnable = 1;
                }
                else
                {
                    IsCRMPhonebookSyncEnable = 0;
                }                    

                if (chkIsCRMSchedulerEnable.Checked == true)
                {
                    IsCRMSchedulerEnable = 1;
                }                    
                else
                {
                    IsCRMSchedulerEnable = 0;
                }                    

                if (chkIsCRMAddEnable.Checked == true)
                {
                    IsCRMAddEnable = 1;
                }                    
                else
                {
                    IsCRMAddEnable = 0;
                }                    

                if (chkIsCRMEditEnable.Checked == true)
                {
                    IsCRMEditEnable = 1;
                }                    
                else
                {
                    IsCRMEditEnable = 0;
                }

                // End of Rev 10.0
                // Rev 11.0
                if (chkIsShowAddressInParty.Checked == true)
                    IsShowAddressInParty = 1;
                else
                    IsShowAddressInParty = 0;

                if (chkIsShowUpdateInvoiceDetails.Checked == true)
                    IsShowUpdateInvoiceDetails = 1;
                else
                    IsShowUpdateInvoiceDetails = 0;
                // End of Rev 11.0
                // Rev 12.0
                if (chkIsSpecialPriceWithEmployee.Checked == true)
                    IsSpecialPriceWithEmployee = 1;
                else
                    IsSpecialPriceWithEmployee = 0;
                // End of Rev 12.0
                // Rev 13.0
                if (chkIsShowCRMOpportunity.Checked == true)
                    IsShowCRMOpportunity = 1;
                else
                    IsShowCRMOpportunity = 0;
                if (chkIsEditEnableforOpportunity.Checked == true)
                    IsEditEnableforOpportunity = 1;
                else
                    IsEditEnableforOpportunity = 0;
                if (chkIsDeleteEnableforOpportunity.Checked == true)
                    IsDeleteEnableforOpportunity = 1;
                else
                    IsDeleteEnableforOpportunity = 0;
                // End of Rev 13.0
                // Rev 14.0
                if (chkIsShowDateWiseOrderInApp.Checked == true)
                    IsShowDateWiseOrderInApp = 1;
                else
                    IsShowDateWiseOrderInApp = 0;
                // End of Rev 14.0
                // Rev 16.0
                if (chkIsUserWiseLMSEnable.Checked == true)
                    IsUserWiseLMSEnable = 1;
                else
                    IsUserWiseLMSEnable = 0;
                if (chkIsUserWiseLMSFeatureOnly.Checked == true)
                    IsUserWiseLMSFeatureOnly = 1;
                else
                    IsUserWiseLMSFeatureOnly = 0;
                // End of Rev 16.0
                // Rev 17.0
                if (chkIsUserWiseRecordAudioEnableForVisitRevisit.Checked == true)
                    IsUserWiseRecordAudioEnableForVisitRevisit = 1;
                else
                    IsUserWiseRecordAudioEnableForVisitRevisit = 0;
                // End of Rev 17.0

                // Rev 18.0
                if (chkDivShowClearQuiz.Checked == true)
                    ShowClearQuiz = 1;
                else
                    ShowClearQuiz = 0;
                // End of Rev 18.0
                // Rev 19.0
                if (chkIsAllowProductCurrentStockUpdateFromApp.Checked == true)
                    IsAllowProductCurrentStockUpdateFromApp = 1;
                else
                    IsAllowProductCurrentStockUpdateFromApp = 0;
                // End of Rev 19.0
                // Rev 21.0
                if (chkShowTargetOnApp.Checked == true)
                    ShowTargetOnApp = 1;
                else
                    ShowTargetOnApp = 0;
                // End of Rev 21.0

                String PartyType = hdnPartyType.Value.ToString();
                //Rev work start 26.04.2022 Mantise ID:0024856: Copy feature add in User master
                //if (Id == "Add")
                if (Id == "Add" && Request.QueryString["Mode"]!="Copy")
                //Rev work close 26.04.2022 Mantise ID:0024856: Copy feature add in User master
                {
                    int LoginIDExist = 0;

                    string[,] checkUser = oDBEngine.GetFieldValue("tbl_master_user", "user_loginId", " user_loginId='" + txtuserid.Text.ToString().Trim() + "'", 1);
                    string check = checkUser[0, 0];
                    if (check != "n")
                    {
                        //    txtuserid.Text = "Login Id All Ready Exist !! ";
                        //    txtuserid.ForeColor = Color.Red;
                        // Mantis Issue 24723
                        //SalesPersontracking ob = new SalesPersontracking();
                        //DataTable dtfromtosumervisor = ob.MobileUserloginIDRellocation(txtuserid.Text.ToString().Trim());

                        LoginIDExist = 1;
                        // End of Mantis Issue 24723


                    }

                    // Mantis Issue 24723
                    if(LoginIDExist == 0)
                    {
                        if (Convert.ToString(Session["PageAccess"]).Trim() == "All" || Convert.ToString(Session["PageAccess"]).Trim() == "Add" || Convert.ToString(Session["PageAccess"]).Trim() == "DelAdd")
                        {



                            //// Encrypt  the Password
                            Encryption epasswrd = new Encryption();
                            string Encryptpass = epasswrd.Encrypt(txtpassword.Text.Trim());

                            // oDBEngine.InsertDataFromAnotherTable(" tbl_master_user ", " user_name,user_branchId,user_loginId,user_password,user_contactId,user_group,CreateDate,CreateUser,
                            //user_lastsegement,user_TimeForTickerRefrsh,user_superuser,user_EntryProfile,user_AllowAccessIP,user_inactive,user_maclock,Gps_Accuracy,HierarchywiseTargetSettings,
                            //willLeaveApprovalEnable,IsAutoRevisitEnable,IsShowPlanDetails,IsMoreDetailsMandatory,IsShowMoreDetailsMandatory,isMeetingAvailable,isRateNotEditable,IsShowTeamDetails,
                            //IsAllowPJPUpdateForTeam,willReportShow,isAddAttendence,isFingerPrintMandatoryForAttendance,isFingerPrintMandatoryForVisit,isSelfieMandatoryForAttendance", null, 
                            //"'" + txtusername.Text.Trim() + "','" + b_id + "','" + txtuserid.Text.Trim() + "','" + Encryptpass + "','" + contact + "','" + usergroup + "','" + CreateDate.ToString() 
                            //+ "','" + CreateUser + "', ( select top 1 grp_segmentId from tbl_master_userGroup where grp_id in(" + usergroup + ")),86400,'" + superuser + "','" 
                            //+ ddDataEntry.SelectedItem.Value + "','" + IPAddress.Trim() + "','" + isactive + "','" + isactivemac + "'," + txtgps.Text + "," + istargetsettings + "," + isLeaveApprovalEnable + ",
                            //" + IsAutoRevisitEnable + "," + IsShowPlanDetails + "," + IsMoreDetailsMandatory + "," + IsShowMoreDetailsMandatory + "," + isMeetingAvailable + "," + isRateNotEditable + "," + 
                            //IsShowTeamDetails + "," + IsAllowPJPUpdateForTeam + "," + willReportShow + "," + isAddAttendence + "," + isFingerPrintMandatoryForAttendance + "," + isFingerPrintMandatoryForVisit + ",
                            //" + isSelfieMandatoryForAttendance + "", null);

                            ProcedureExecute proc = new ProcedureExecute("PRC_FTSInsertUpdateUser");
                            proc.AddPara("@ACTION", "INSERT");
                            proc.AddPara("@txtusername", txtusername.Text.Trim());
                            proc.AddPara("@b_id", b_id);
                            proc.AddPara("@txtuserid", txtuserid.Text.Trim());
                            proc.AddPara("@Encryptpass", Encryptpass);
                            proc.AddPara("@contact", contact);
                            proc.AddPara("@usergroup", usergroup);
                            proc.AddPara("@CreateDate", CreateDate.ToString());
                            proc.AddPara("@CreateUser", CreateUser);
                            proc.AddPara("@superuser", superuser);
                            proc.AddPara("@ddDataEntry", ddDataEntry.SelectedItem.Value);
                            proc.AddPara("@IPAddress", IPAddress.Trim());
                            proc.AddPara("@isactive", isactive);
                            proc.AddPara("@isactivemac", isactivemac);
                            proc.AddPara("@txtgps", txtgps.Text);
                            proc.AddPara("@istargetsettings", istargetsettings);
                            proc.AddPara("@isLeaveApprovalEnable", isLeaveApprovalEnable);
                            proc.AddPara("@IsAutoRevisitEnable", IsAutoRevisitEnable);
                            proc.AddPara("@IsShowPlanDetails", IsShowPlanDetails);
                            proc.AddPara("@IsMoreDetailsMandatory", IsMoreDetailsMandatory);
                            proc.AddPara("@IsShowMoreDetailsMandatory", IsShowMoreDetailsMandatory);

                            proc.AddPara("@isMeetingAvailable", isMeetingAvailable);
                            proc.AddPara("@isRateNotEditable", isRateNotEditable);
                            proc.AddPara("@IsShowTeamDetails", IsShowTeamDetails);
                            proc.AddPara("@IsAllowPJPUpdateForTeam", IsAllowPJPUpdateForTeam);
                            proc.AddPara("@willReportShow", willReportShow);
                            proc.AddPara("@isFingerPrintMandatoryForAttendance", isFingerPrintMandatoryForAttendance);
                            proc.AddPara("@isFingerPrintMandatoryForVisit", isFingerPrintMandatoryForVisit);
                            proc.AddPara("@isSelfieMandatoryForAttendance", isSelfieMandatoryForAttendance);

                            proc.AddPara("@isAttendanceReportShow", isAttendanceReportShow);
                            proc.AddPara("@isPerformanceReportShow", isPerformanceReportShow);
                            proc.AddPara("@isVisitReportShow", isVisitReportShow);
                            proc.AddPara("@willTimesheetShow", willTimesheetShow);
                            proc.AddPara("@isAttendanceFeatureOnly", isAttendanceFeatureOnly);
                            proc.AddPara("@isOrderShow", isOrderShow);
                            proc.AddPara("@isVisitShow", isVisitShow);
                            proc.AddPara("@iscollectioninMenuShow", iscollectioninMenuShow);
                            proc.AddPara("@isShopAddEditAvailable", isShopAddEditAvailable);
                            proc.AddPara("@isEntityCodeVisible", isEntityCodeVisible);
                            proc.AddPara("@isAreaMandatoryInPartyCreation", isAreaMandatoryInPartyCreation);
                            proc.AddPara("@isShowPartyInAreaWiseTeam", isShowPartyInAreaWiseTeam);
                            proc.AddPara("@isChangePasswordAllowed", isChangePasswordAllowed);
                            proc.AddPara("@isHomeRestrictAttendance", isHomeRestrictAttendance);
                            proc.AddPara("@isQuotationShow", isQuotationShow);
                            proc.AddPara("@IsStateMandatoryinReport", IsStateMandatoryinReport);

                            proc.AddPara("@isAchievementEnable", isAchievementEnable);
                            proc.AddPara("@isTarVsAchvEnable", isTarVsAchvEnable);
                            proc.AddPara("@shopLocAccuracy", txtshopLocAccuracy.Text);
                            proc.AddPara("@homeLocDistance", txthomeLocDistance.Text);


                            //New Settings 18-08-2020

                            proc.AddPara("@isQuotationPopupShow", isQuotationPopupShow);
                            proc.AddPara("@isOrderReplacedWithTeam", isOrderReplacedWithTeam);
                            proc.AddPara("@isMultipleAttendanceSelection", isMultipleAttendanceSelection);
                            proc.AddPara("@isOfflineTeam", isOfflineTeam);
                            proc.AddPara("@isDDShowForMeeting", isDDShowForMeeting);
                            proc.AddPara("@isDDMandatoryForMeeting", isDDMandatoryForMeeting);
                            proc.AddPara("@isAllTeamAvailable", isAllTeamAvailable);
                            proc.AddPara("@isRecordAudioEnable", isRecordAudioEnable);
                            proc.AddPara("@isNextVisitDateMandatory", isNextVisitDateMandatory);
                            proc.AddPara("@isShowCurrentLocNotifiaction", isShowCurrentLocNotifiaction);
                            proc.AddPara("@isUpdateWorkTypeEnable", isUpdateWorkTypeEnable);
                            proc.AddPara("@isLeaveEnable", isLeaveEnable);
                            proc.AddPara("@isOrderMailVisible", isOrderMailVisible);
                            proc.AddPara("@LateVisitSMS", LateVisitSMS);
                            proc.AddPara("@isShopEditEnable", isShopEditEnable);
                            proc.AddPara("@isTaskEnable", isTaskEnable);

                            proc.AddPara("@PartyType", PartyType);


                            proc.AddPara("@isAppInfoEnable", isAppInfoEnable);
                            proc.AddPara("@willDynamicShow", willDynamicShow);
                            proc.AddPara("@willActivityShow", willActivityShow);
                            proc.AddPara("@isDocumentRepoShow", isDocumentRepoShow);
                            proc.AddPara("@isChatBotShow", isChatBotShow);
                            proc.AddPara("@isAttendanceBotShow", isAttendanceBotShow);
                            proc.AddPara("@isVisitBotShow", isVisitBotShow);
                            proc.AddPara("@appInfoMins", txtDeviceInfoMin.Text);

                            //Add extra settings 01-12-2020
                            proc.AddPara("@isInstrumentCompulsory", isInstrumentCompulsory);
                            proc.AddPara("@isBankCompulsory", isBankCompulsory);
                            //Add extra settings 01-12-2020

                            //Add extra settings 12-05-2021
                            proc.AddPara("@isComplementaryUser", isComplementaryUser);
                            proc.AddPara("@isVisitPlanShow", isVisitPlanShow);
                            proc.AddPara("@isVisitPlanMandatory", isVisitPlanMandatory);
                            proc.AddPara("@isAttendanceDistanceShow", isAttendanceDistanceShow);
                            proc.AddPara("@willTimelineWithFixedLocationShow", willTimelineWithFixedLocationShow);
                            proc.AddPara("@isShowOrderRemarks", isShowOrderRemarks);
                            proc.AddPara("@isShowOrderSignature", isShowOrderSignature);
                            proc.AddPara("@isShowSmsForParty", isShowSmsForParty);
                            proc.AddPara("@isShowTimeline", isShowTimeline);
                            proc.AddPara("@willScanVisitingCard", willScanVisitingCard);
                            proc.AddPara("@isCreateQrCode", isCreateQrCode);
                            proc.AddPara("@isScanQrForRevisit", isScanQrForRevisit);
                            proc.AddPara("@isShowLogoutReason", isShowLogoutReason);
                            proc.AddPara("@willShowHomeLocReason", willShowHomeLocReason);
                            proc.AddPara("@willShowShopVisitReason", willShowShopVisitReason);
                            proc.AddPara("@willShowPartyStatus", willShowPartyStatus);
                            proc.AddPara("@willShowEntityTypeforShop", willShowEntityTypeforShop);
                            proc.AddPara("@isShowRetailerEntity", isShowRetailerEntity);
                            proc.AddPara("@isShowDealerForDD", isShowDealerForDD);
                            proc.AddPara("@isShowBeatGroup", isShowBeatGroup);
                            proc.AddPara("@isShowShopBeatWise", isShowShopBeatWise);
                            proc.AddPara("@isShowBankDetailsForShop", isShowBankDetailsForShop);
                            proc.AddPara("@isShowOTPVerificationPopup", isShowOTPVerificationPopup);
                            proc.AddPara("@isShowMicroLearing", isShowMicroLearing);
                            proc.AddPara("@isMultipleVisitEnable", isMultipleVisitEnable);
                            proc.AddPara("@isShowVisitRemarks", isShowVisitRemarks);
                            proc.AddPara("@isShowNearbyCustomer", isShowNearbyCustomer);
                            proc.AddPara("@isServiceFeatureEnable", isServiceFeatureEnable);
                            proc.AddPara("@isPatientDetailsShowInOrder", isPatientDetailsShowInOrder);
                            proc.AddPara("@isPatientDetailsShowInCollection", isPatientDetailsShowInCollection);
                            proc.AddPara("@isAttachmentMandatory", isAttachmentMandatory);
                            proc.AddPara("@isShopImageMandatory", isShopImageMandatory);
                            //Add extra settings 12-05-2021

                            //Add extra settings 27-07-2021
                            proc.AddPara("@isLogShareinLogin", isLogShareinLogin);
                            proc.AddPara("@IsCompetitorenable", IsCompetitorenable);
                            proc.AddPara("@IsOrderStatusRequired", IsOrderStatusRequired);
                            proc.AddPara("@IsCurrentStockEnable", IsCurrentStockEnable);
                            proc.AddPara("@IsCurrentStockApplicableforAll", IsCurrentStockApplicableforAll);
                            proc.AddPara("@IscompetitorStockRequired", IscompetitorStockRequired);
                            proc.AddPara("@IsCompetitorStockforParty", IsCompetitorStockforParty);
                            proc.AddPara("@ShowFaceRegInMenu", ShowFaceRegInMenu);
                            proc.AddPara("@IsFaceDetection", IsFaceDetection);
                            //proc.AddPara("@isFaceRegistered", isFaceRegistered);
                            proc.AddPara("@IsUserwiseDistributer", IsUserwiseDistributer);
                            proc.AddPara("@IsPhotoDeleteShow", IsPhotoDeleteShow);
                            proc.AddPara("@IsAllDataInPortalwithHeirarchy", IsAllDataInPortalwithHeirarchy);
                            proc.AddPara("@IsFaceDetectionWithCaptcha", IsFaceDetectionWithCaptcha);
                            //Add extra settings 27-07-2021
                            //Add Etra setting 06-08-2021
                            proc.AddPara("@IsShowMenuAddAttendance", IsShowMenuAddAttendance);
                            proc.AddPara("@IsShowMenuAttendance", IsShowMenuAttendance);
                            proc.AddPara("@IsShowMenuShops", IsShowMenuShops);
                            proc.AddPara("@IsShowMenuOutstandingDetailsPPDD", IsShowMenuOutstandingDetailsPPDD);
                            proc.AddPara("@IsShowMenuStockDetailsPPDD", IsShowMenuStockDetailsPPDD);
                            proc.AddPara("@IsShowMenuTA", IsShowMenuTA);
                            proc.AddPara("@IsShowMenuMISReport", IsShowMenuMISReport);
                            proc.AddPara("@IsShowMenuReimbursement", IsShowMenuReimbursement);
                            proc.AddPara("@IsShowMenuAchievement", IsShowMenuAchievement);
                            proc.AddPara("@IsShowMenuMapView", IsShowMenuMapView);
                            proc.AddPara("@IsShowMenuShareLocation", IsShowMenuShareLocation);
                            proc.AddPara("@IsShowMenuHomeLocation", IsShowMenuHomeLocation);
                            proc.AddPara("@IsShowMenuWeatherDetails", IsShowMenuWeatherDetails);
                            proc.AddPara("@IsShowMenuChat", IsShowMenuChat);
                            proc.AddPara("@IsShowMenuScanQRCode", IsShowMenuScanQRCode);
                            proc.AddPara("@IsShowMenuPermissionInfo", IsShowMenuPermissionInfo);
                            proc.AddPara("@IsShowMenuAnyDesk", IsShowMenuAnyDesk);

                            proc.AddPara("@IsDocRepoFromPortal", IsDocRepoFromPortal);
                            proc.AddPara("@IsDocRepShareDownloadAllowed", IsDocRepShareDownloadAllowed);
                            proc.AddPara("@IsScreenRecorderEnable", IsScreenRecorderEnable);
                            //Add Etra setting 06-08-2021

                            //Add Etra setting 25-08-2021
                            proc.AddPara("@IsShowPartyOnAppDashboard", IsShowPartyOnAppDashboard);
                            proc.AddPara("@IsShowAttendanceOnAppDashboard", IsShowAttendanceOnAppDashboard);
                            proc.AddPara("@IsShowTotalVisitsOnAppDashboard", IsShowTotalVisitsOnAppDashboard);
                            proc.AddPara("@IsShowVisitDurationOnAppDashboard", IsShowVisitDurationOnAppDashboard);
                            proc.AddPara("@IsShowDayStart", IsShowDayStart);
                            proc.AddPara("@IsshowDayStartSelfie", IsshowDayStartSelfie);
                            proc.AddPara("@IsShowDayEnd", IsShowDayEnd);
                            proc.AddPara("@IsshowDayEndSelfie", IsshowDayEndSelfie);
                            proc.AddPara("@IsShowLeaveInAttendance", IsShowLeaveInAttendance);
                            proc.AddPara("@IsLeaveGPSTrack", IsLeaveGPSTrack);
                            proc.AddPara("@IsShowActivitiesInTeam", IsShowActivitiesInTeam);
                            proc.AddPara("@IsShowMarkDistVisitOnDshbrd", IsShowMarkDistVisitOnDshbrd);
                            //Mantis Issue 24408,24364
                            proc.AddPara("@IsRevisitRemarksMandatory", IsRevisitRemarksMandatory);
                            proc.AddPara("@GPSAlert", GPSAlert);
                            proc.AddPara("@GPSAlertwithSound", GPSAlertwithSound);
                            //End of Mantis Issue 24408,24364
                            // Mantis Issue 24596,24597
                            proc.AddPara("@FaceRegistrationFrontCamera", FaceRegistrationFrontCamera);
                            proc.AddPara("@MRPInOrder", MRPInOrder);
                            // End of Mantis Issue 24596,24597
                            //Add Etra setting 25-08-2021
                            /*Rev work start 11.05.2022 Mantise No:0024880 Horizontal Performance Summary & detail report required with hierarchy setting*/
                            proc.AddPara("@isHorizontalPerformReportShow", isHorizontalPerformReportShow);
                            /*Rev work close 11.05.2022 Mantise No:0024880 Horizontal Performance Summary & detail report required with hierarchy setting*/
                            //Mantis Issue 25015
                            proc.AddPara("@FaceRegTypeID", FaceRegTypeID);
                            //End of Mantis Issue 25015
                            //Mantis Issue 25035
                            proc.AddPara("@DistributerwisePartyOrderReport", DistributerwisePartyOrderReport);
                            //End of Mantis Issue 25035
                            //Mantis Issue 25116
                            proc.AddPara("@ShowAttednaceClearmenu", ShowAttednaceClearmenu);
                            //End of Mantis Issue 25116

                            // Mantis Issue 25207
                            proc.AddPara("@ShowAllowProfileUpdate", ShowAllowProfileUpdate);
                            proc.AddPara("@ShowAutoDDSelect", ShowAutoDDSelect);
                            proc.AddPara("@ShowBatterySetting", ShowBatterySetting);
                            proc.AddPara("@ShowCommonAINotification", ShowCommonAINotification);
                            proc.AddPara("@ShowCustom_Configuration", ShowCustom_Configuration);
                            proc.AddPara("@ShowisAadharRegistered", ShowisAadharRegistered);
                            proc.AddPara("@ShowIsActivateNewOrderScreenwithSize", ShowIsActivateNewOrderScreenwithSize);
                            proc.AddPara("@ShowIsAllowBreakageTracking", ShowIsAllowBreakageTracking);
                            proc.AddPara("@ShowIsAllowBreakageTrackingunderTeam", ShowIsAllowBreakageTrackingunderTeam);
                            proc.AddPara("@ShowIsAllowClickForPhotoRegister", ShowIsAllowClickForPhotoRegister);
                            proc.AddPara("@ShowIsAllowClickForVisit", ShowIsAllowClickForVisit);
                            proc.AddPara("@ShowIsAllowClickForVisitForSpecificUser", ShowIsAllowClickForVisitForSpecificUser);
                            proc.AddPara("@ShowIsAllowShopStatusUpdate", ShowIsAllowShopStatusUpdate);
                            proc.AddPara("@ShowIsAlternateNoForCustomer", ShowIsAlternateNoForCustomer);
                            proc.AddPara("@ShowIsAttendVisitShowInDashboard", ShowIsAttendVisitShowInDashboard);
                            proc.AddPara("@ShowIsAutoLeadActivityDateTime", ShowIsAutoLeadActivityDateTime);
                            proc.AddPara("@ShowIsBeatRouteReportAvailableinTeam", ShowIsBeatRouteReportAvailableinTeam);
                            proc.AddPara("@ShowIsCollectionOrderWise", ShowIsCollectionOrderWise);
                            proc.AddPara("@ShowIsFaceRecognitionOnEyeblink", ShowIsFaceRecognitionOnEyeblink);
                            proc.AddPara("@ShowisFaceRegistered", ShowisFaceRegistered);
                            proc.AddPara("@ShowIsFeedbackAvailableInShop", ShowIsFeedbackAvailableInShop);
                            proc.AddPara("@ShowIsFeedbackHistoryActivated", ShowIsFeedbackHistoryActivated);
                            proc.AddPara("@ShowIsFromPortal", ShowIsFromPortal);
                            proc.AddPara("@ShowIsIMEICheck", ShowIsIMEICheck);
                            proc.AddPara("@ShowIslandlineforCustomer", ShowIslandlineforCustomer);
                            proc.AddPara("@ShowIsNewQuotationfeatureOn", ShowIsNewQuotationfeatureOn);
                            proc.AddPara("@ShowIsNewQuotationNumberManual", ShowIsNewQuotationNumberManual);
                            proc.AddPara("@ShowIsPendingCollectionRequiredUnderTeam", ShowIsPendingCollectionRequiredUnderTeam);
                            proc.AddPara("@ShowIsprojectforCustomer", ShowIsprojectforCustomer);
                            proc.AddPara("@ShowIsRateEnabledforNewOrderScreenwithSize", ShowIsRateEnabledforNewOrderScreenwithSize);
                            proc.AddPara("@ShowIsRestrictNearbyGeofence", ShowIsRestrictNearbyGeofence);
                            proc.AddPara("@ShowIsReturnEnableforParty", ShowIsReturnEnableforParty);
                            proc.AddPara("@ShowIsShowHomeLocationMap", ShowIsShowHomeLocationMap);
                            proc.AddPara("@ShowIsShowManualPhotoRegnInApp", ShowIsShowManualPhotoRegnInApp);
                            proc.AddPara("@ShowIsShowMyDetails", ShowIsShowMyDetails);
                            proc.AddPara("@ShowIsShowNearByTeam", ShowIsShowNearByTeam);
                            proc.AddPara("@ShowIsShowRepeatOrderinNotification", ShowIsShowRepeatOrderinNotification);
                            proc.AddPara("@ShowIsShowRepeatOrdersNotificationinTeam", ShowIsShowRepeatOrdersNotificationinTeam);
                            proc.AddPara("@ShowIsShowRevisitRemarksPopup", ShowIsShowRevisitRemarksPopup);
                            proc.AddPara("@ShowIsShowTypeInRegistration", ShowIsShowTypeInRegistration);
                            proc.AddPara("@ShowIsTeamAttendance", ShowIsTeamAttendance);
                            proc.AddPara("@ShowIsTeamAttenWithoutPhoto", ShowIsTeamAttenWithoutPhoto);
                            proc.AddPara("@ShowIsWhatsappNoForCustomer", ShowIsWhatsappNoForCustomer);
                            proc.AddPara("@ShowLeaveapprovalfromsupervisor", ShowLeaveapprovalfromsupervisor);
                            proc.AddPara("@ShowLeaveapprovalfromsupervisorinteam", ShowLeaveapprovalfromsupervisorinteam);
                            proc.AddPara("@ShowLogoutWithLogFile", ShowLogoutWithLogFile);
                            proc.AddPara("@ShowMarkAttendNotification", ShowMarkAttendNotification);
                            proc.AddPara("@ShowPartyUpdateAddrMandatory", ShowPartyUpdateAddrMandatory);
                            proc.AddPara("@ShowPowerSaverSetting", ShowPowerSaverSetting);
                            proc.AddPara("@ShowShopScreenAftVisitRevisit", ShowShopScreenAftVisitRevisit);
                            proc.AddPara("@Show_App_Logout_Notification", Show_App_Logout_Notification);
                            proc.AddPara("@ShowAmountNewQuotation", ShowAmountNewQuotation);
                            proc.AddPara("@ShowAutoRevisitInAppMenu", ShowAutoRevisitInAppMenu);
                            proc.AddPara("@ShowAutoRevisitInDashboard", ShowAutoRevisitInDashboard);
                            proc.AddPara("@ShowCollectionAlert", ShowCollectionAlert);
                            proc.AddPara("@ShowCollectionOnlywithInvoiceDetails", ShowCollectionOnlywithInvoiceDetails);
                            proc.AddPara("@ShowPurposeInShopVisit", ShowPurposeInShopVisit);
                            proc.AddPara("@ShowQuantityNewQuotation", ShowQuantityNewQuotation);
                            proc.AddPara("@ShowTotalVisitAppMenu", ShowTotalVisitAppMenu);
                            proc.AddPara("@ShowUserwiseLeadMenu", ShowUserwiseLeadMenu);
                            proc.AddPara("@ShowZeroCollectioninAlert", ShowZeroCollectioninAlert);
                            proc.AddPara("@ShowUpdateOtherID", ShowUpdateOtherID);
                            proc.AddPara("@ShowUpdateUserID", ShowUpdateUserID);
                            proc.AddPara("@ShowUpdateUserName", ShowUpdateUserName);
                            proc.AddPara("@ShowWillRoomDBShareinLogin", ShowWillRoomDBShareinLogin);
                            // End of Mantis Issue 25207
                            // Rev 1.0
                            proc.AddPara("@IsShowEmployeePerformance", IsShowEmployeePerformance);
                            // End of Rev 1.0
                            // Rev 2.0
                            proc.AddPara("@IsShowBeatInMenu", IsShowBeatInMenu);
                            // End of Rev 2.0
                            // Rev 3.0
                            proc.AddPara("@IsShowWorkType", IsShowWorkType);
                            proc.AddPara("@IsShowMarketSpendTimer", IsShowMarketSpendTimer);
                            proc.AddPara("@IsShowUploadImageInAppProfile", IsShowUploadImageInAppProfile);
                            proc.AddPara("@IsShowCalendar", IsShowCalendar);
                            proc.AddPara("@IsShowCalculator", IsShowCalculator);
                            proc.AddPara("@IsShowInactiveCustomer", IsShowInactiveCustomer);
                            proc.AddPara("@IsShowAttendanceSummary", IsShowAttendanceSummary);
                            // End of Rev 3.0
                            // Rev 4.0
                            proc.AddPara("@IsMenuShowAIMarketAssistant", IsMenuShowAIMarketAssistant);
                            proc.AddPara("@IsUsbDebuggingRestricted", IsUsbDebuggingRestricted);
                            // End of Rev 4.0
                            // Rev 5.0
                            proc.AddPara("@IsShowLatLongInOutletMaster", IsShowLatLongInOutletMaster);
                            // End of Rev 5.0
                            // Rev 6.0
                            proc.AddPara("@IsCallLogHistoryActivated", IsCallLogHistoryActivated);
                            // End of Rev 6.0
                            // Rev 7.0
                            proc.AddPara("@IsShowMenuCRMContacts", IsShowMenuCRMContacts);
                            proc.AddPara("@IsCheckBatteryOptimization", IsCheckBatteryOptimization);
                            // End of Rev 7.0

                            // Rev 8.0
                            proc.AddPara("@ShowUserwisePartyWithGeoFence", ShowUserwisePartyWithGeoFence);
                            proc.AddPara("@ShowUserwisePartyWithCreateOrder", ShowUserwisePartyWithCreateOrder);
                            // End of Rev 8.0

                            // Rev 9.0
                            proc.AddPara("@AdditionalinfoRequiredforContactListing", AdditionalinfoRequiredforContactListing);
                            proc.AddPara("@AdditionalinfoRequiredforContactAdd", AdditionalinfoRequiredforContactAdd);
                            proc.AddPara("@ContactAddresswithGeofence", ContactAddresswithGeofence);
                            // End of Rev 9.0

                            // Rev 10.0
                            proc.AddPara("@IsCRMPhonebookSyncEnable", IsCRMPhonebookSyncEnable);
                            proc.AddPara("@IsCRMSchedulerEnable", IsCRMSchedulerEnable);                            
                            proc.AddPara("@IsCRMAddEnable", IsCRMAddEnable);
                            proc.AddPara("@IsCRMEditEnable", IsCRMEditEnable);
                            // End of Rev 10.0
                            // Rev 11.0
                            proc.AddPara("@IsShowAddressInParty", IsShowAddressInParty);
                            proc.AddPara("@IsShowUpdateInvoiceDetails", IsShowUpdateInvoiceDetails);
                            // End of Rev 11.0
                            // Rev 12.0
                            proc.AddPara("@IsSpecialPriceWithEmployee", IsSpecialPriceWithEmployee);
                            // End of Rev 12.0
                            // Rev 13.0
                            proc.AddPara("@IsShowCRMOpportunity", IsShowCRMOpportunity);
                            proc.AddPara("@IsEditEnableforOpportunity", IsEditEnableforOpportunity);
                            proc.AddPara("@IsDeleteEnableforOpportunity", IsDeleteEnableforOpportunity);
                            // End of Rev 13.0
                            // Rev 14.0
                            proc.AddPara("@IsShowDateWiseOrderInApp", IsShowDateWiseOrderInApp);
                            // End of Rev 14.0
                            // Rev 16.0
                            proc.AddPara("@IsUserWiseLMSEnable", IsUserWiseLMSEnable);
                            proc.AddPara("@IsUserWiseLMSFeatureOnly", IsUserWiseLMSFeatureOnly);
                            // End of Rev 16.0
                            // Rev 17.0
                            proc.AddPara("@IsUserWiseRecordAudioEnableForVisitRevisit", IsUserWiseRecordAudioEnableForVisitRevisit);
                            // End of Rev 17.0

                            // Rev 18.0
                            proc.AddPara("@ShowClearQuiz", ShowClearQuiz);
                            // End of Rev 18.0
                            // Rev 19.0
                            proc.AddPara("@IsAllowProductCurrentStockUpdateFromApp", IsAllowProductCurrentStockUpdateFromApp);
                            // End of Rev 19.0
                            // Rev 21.0
                            proc.AddPara("@ShowTargetOnApp", ShowTargetOnApp);
                            // End of Rev 21.0

                            DataTable dt = proc.GetTable();


                            string[,] userid = oDBEngine.GetFieldValue("tbl_master_user", "max(user_id)", null, 1);


                            //oDBEngine.InsertDataFromAnotherTable(" tbl_master_user ", " user_name,user_branchId,user_loginId,user_password,user_contactId,user_group,CreateDate,CreateUser,user_lastsegement,user_TimeForTickerRefrsh,user_superuser,user_EntryProfile,user_AllowAccessIP,user_inactive", null, "'" + txtusername.Text.Trim() + "','" + b_id + "','" + txtloginid.Text.Trim() + "','" + txtpassword.Text.Trim() + "','" + contact + "','" + usergroup + "','" + CreateDate.ToString() + "','" + CreateUser + "',( select top 1 grp_segmentId from tbl_master_userGroup where grp_id in(" + usergroup + ")),86400,'" + superuser + "','" + ddDataEntry.SelectedItem.Value + "','" + IPAddress.Trim() + "','" + isactive + "'", null);
                            //string[,] userid = oDBEngine.GetFieldValue("tbl_master_user", "max(user_id)", null, 1);

                            //Start Leave Approver Tanmoy

                            DataSet dsApprove = new DataSet();
                            dsApprove = oDBEngine.PopulateData("ID", "FTS_LEAVE_APPROVER", "APPROVAR_ID='" + userid[0, 0] + "'");

                            if (chkLeaveApprover.Checked == true)
                            {
                                if (dsApprove.Tables["TableName"].Rows.Count == 0)
                                {
                                    oDBEngine.InsertDataFromAnotherTable("FTS_LEAVE_APPROVER ", "APPROVAR_ID", null, "'" + userid[0, 0] + "'", null);
                                }
                            }
                            else
                            {
                                if (dsApprove.Tables["TableName"].Rows.Count > 0)
                                {
                                    oDBEngine.DeleteValue("FTS_LEAVE_APPROVER", "APPROVAR_ID='" + userid[0, 0] + "'");
                                }
                            }
                            //End Leave Approver Tanmoy

                            string splitsegname = segname[0, 0].Split('-')[0].ToString().Trim();
                            // string splitsegname1 = segname[0, 0].Split('-')[1].ToString().Trim(); 

                            string[,] exchsegid = oDBEngine.GetFieldValue("Master_Exchange", "top 1 Exchange_Id", "Exchange_ShortName='" + splitsegname + "'", 1);

                            // Jitendra- Need to work in Financial year validation, this time removed it temporarly
                            //string[,] finyr = oDBEngine.GetFieldValue("Master_FinYear", "top 1 FinYear_Code", "Getdate() between FinYear_StartDate and FinYear_EndDate", 1);
                            // string[,] finyr = oDBEngine.GetFieldValue("Master_FinYear", "top 1 FinYear_Code", null, 1);

                            string FinancialYear = GetFinancialYear();



                            string[,] exhCntID = oDBEngine.GetFieldValue("Tbl_Master_Exchange", "top 1 exh_CntID", "Exh_ShortName= '" + splitsegname.ToString().Trim() + "'", 1);
                            //  string[,] exchmaster = oDBEngine.GetFieldValue("Master_ExchangeSegments", "top 1 exchangesegment_id", "Exchangesegment_code='" + splitsegname1 + "'", 1);
                            //  string[,] settno = oDBEngine.GetFieldValue("Master_Settlements", "top 1 Settlements_Number", "Settlements_ExchangeSegmentID='" + exchmaster[0, 0] + "' and Settlements_FinYear='" + finyr[0, 0] + "'   and Settlements_StartDateTime=(Select Max(Settlements_StartDateTime) from Master_Settlements Where Settlements_ExchangeSegmentID='" + exchmaster[0, 0] + "' and Settlements_FinYear='" + finyr[0, 0] + "'  ) ", 1);                                
                            //string[,] settno1 = oDBEngine.GetFieldValue("Master_Settlements", "top 1 Settlements_Number", "Settlements_ExchangeSegmentID='" + exchsegid[0, 0] + "' and Settlements_FinYear='" + finyr[0, 0] + "' and Settlements_TypeSuffix='W'  and Settlements_StartDateTime=(Select Max(Settlements_StartDateTime) from Master_Settlements Where Settlements_ExchangeSegmentID='" + exchsegid[0, 0] + "' and Settlements_FinYear='" + finyr[0, 0] + "' and Settlements_TypeSuffix='W' ) ", 1);
                            //string[,] settno2 = oDBEngine.GetFieldValue("Master_Settlements", "top 1 Settlements_Number", "Settlements_ExchangeSegmentID='" + exchsegid[0, 0] + "' and Settlements_FinYear='" + finyr[0, 0] + "' and Settlements_TypeSuffix='F'  and Settlements_StartDateTime=(Select Max(Settlements_StartDateTime) from Master_Settlements Where Settlements_ExchangeSegmentID='" + exchsegid[0, 0] + "' and Settlements_FinYear='" + finyr[0, 0] + "' and Settlements_TypeSuffix='F' ) ", 1);
                            // string[,] settno = oDBEngine.GetFieldValue("Master_Settlements", "top 1 Settlements_Number", "Settlements_ExchangeSegmentID='" + exchsegid[0, 0] + "' and Settlements_FinYear='" + finyr[0, 0] + "' and (case when '" + splitsegname1 + "==CM" + "' and '" + exchsegid[0, 0] + "==1" + "' then Settlements_TypeSuffix='N' when '" + splitsegname1 + "==CM" + "' and '" + exchsegid[0, 0] + "==2" + "'  then Settlements_TypeSuffix='W' else Settlements_TypeSuffix='F' end) and Settlements_StartDateTime=(Select Max(Settlements_StartDateTime) from Master_Settlements Where Settlements_ExchangeSegmentID='" + exchsegid[0, 0] + "' and Settlements_FinYear='" + finyr[0, 0] + "' and (case when '" + splitsegname1 + "==CM" + "' and '" + exchsegid[0, 0] + "==1" + "' then Settlements_TypeSuffix='N' when '" + splitsegname1 + "==CM" + "' and '" + exchsegid[0, 0] + "==2" + "' then Settlements_TypeSuffix='W' else Settlements_TypeSuffix='F' end)) ", 1);

                            // string[,] companymain = oDBEngine.GetFieldValue("Tbl_Master_companyExchange", "top 1 Exch_InternalID,Exch_CompID", "Exch_ExchID='" + exhCntID[0, 0].ToString().Trim() + "' and exch_segmentId='1'", 2);
                            // oDBEngine.InsurtFieldValue("tbl_trans_LastSegment", "ls_cntid,ls_lastsegment,ls_userid,ls_lastdpcoid,ls_lastCompany,ls_lastFinYear,ls_lastSettlementNo,ls_lastSettlementType", "'" + contact + "','" + grpsegment[0, 0] + "','" + userid[0, 0] + "','" + companymain[0, 0] + "','" + companymain[0, 1].ToString() + "','" + finyr[0, 0].ToString().Trim() + "','','N'");


                            //Added New code to add eefault company in the tbl_master_user
                            string[,] userInternalId = oDBEngine.GetFieldValue("tbl_master_user", "user_Contactid", "user_id=" + userid[0, 0] + "", 1);
                            //DataTable dtcmp = oDBEngine.GetDataTable(" tbl_master_company  ", "*", "cmp_id=(select emp_organization from tbl_trans_employeectc where emp_cntId='" + userInternalId[0,0] + "')");
                            DataTable dtcmp = oDBEngine.GetDataTable(" tbl_master_company  ", "*", "cmp_id=(select top 1 emp_organization  from tbl_trans_employeectc where emp_cntId='" + userInternalId[0, 0] + "' and emp_id=(select MAX(emp_id) from tbl_trans_employeectc e where e.emp_cntId='" + userInternalId[0, 0] + "'))");

                            if (dtcmp.Rows.Count > 0)
                            {
                                string SegmentId = "1";
                                oDBEngine.InsurtFieldValue("Master_UserCompany", "UserCompany_UserID,UserCompany_CompanyID,UserCompany_CreateUser,UserCompany_CreateDateTime", "'" + userid[0, 0] + "','" + Convert.ToString(dtcmp.Rows[0]["cmp_internalid"]) + "','" + Convert.ToString(Session["userid"]) + "','" + DateTime.Now + "'");
                                //oDBEngine.InsurtFieldValue("tbl_trans_LastSegment", "ls_cntid,ls_lastsegment,ls_userid,ls_lastdpcoid,ls_lastCompany,ls_lastFinYear,ls_lastSettlementNo,ls_lastSettlementType", "'" + contact + "','" + grpsegment[0, 0] + "','" + userid[0, 0] + "','" + SegmentId + "','" + Convert.ToString(dtcmp.Rows[0]["cmp_internalid"]) + "','" + finyr[0, 0].ToString().Trim() + "','','N'");
                                oDBEngine.InsurtFieldValue("tbl_trans_LastSegment", "ls_cntid,ls_lastsegment,ls_userid,ls_lastdpcoid,ls_lastCompany,ls_lastFinYear,ls_lastSettlementNo,ls_lastSettlementType", "'" + contact + "','" + grpsegment[0, 0] + "','" + userid[0, 0] + "','" + SegmentId + "','" + Convert.ToString(dtcmp.Rows[0]["cmp_internalid"]) + "','" + FinancialYear.Trim() + "','','N'");

                            }
                            else
                            {
                                string[,] companymain = oDBEngine.GetFieldValue("Tbl_Master_companyExchange", "top 1 Exch_InternalID,Exch_CompID", "Exch_ExchID='" + exhCntID[0, 0].ToString().Trim() + "' and exch_segmentId='1'", 2);
                                //oDBEngine.InsurtFieldValue("tbl_trans_LastSegment", "ls_cntid,ls_lastsegment,ls_userid,ls_lastdpcoid,ls_lastCompany,ls_lastFinYear,ls_lastSettlementNo,ls_lastSettlementType", "'" + contact + "','" + grpsegment[0, 0] + "','" + userid[0, 0] + "','" + companymain[0, 0] + "','" + companymain[0, 1].ToString() + "','" + finyr[0, 0].ToString().Trim() + "','','N'");
                                oDBEngine.InsurtFieldValue("tbl_trans_LastSegment", "ls_cntid,ls_lastsegment,ls_userid,ls_lastdpcoid,ls_lastCompany,ls_lastFinYear,ls_lastSettlementNo,ls_lastSettlementType", "'" + contact + "','" + grpsegment[0, 0] + "','" + userid[0, 0] + "','" + companymain[0, 0] + "','" + companymain[0, 1].ToString() + "','" + FinancialYear.Trim() + "','','N'");
                            }
                            //--------------------------------
                            Response.Redirect("/OMS/Management/Master/root_user.aspx");
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "OnClick", "<script language='javascript'> alert('Not Authorised To Add Records!') </script>");
                        }
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "OnClick", "<script language='javascript'> alert('Duplicate user id found ! Please Talk to Administrator.') </script>");
                    }
                    // End of Mantis Issue 24723
                    
                }
                //Rev work start 26.04.2022 Mantise ID:0024856: Copy feature add in User master               
                else if (Id!="Add" && Request.QueryString["Mode"]=="Copy")
                //Rev work close 26.04.2022 Mantise ID:0024856: Copy feature add in User master
                {
                    int LoginIDExist = 0;

                    string[,] checkUser = oDBEngine.GetFieldValue("tbl_master_user", "user_loginId", " user_loginId='" + txtuserid.Text.ToString().Trim() + "'", 1);
                    string check = checkUser[0, 0];
                    if (check != "n")
                    {
                        //    txtuserid.Text = "Login Id All Ready Exist !! ";
                        //    txtuserid.ForeColor = Color.Red;
                        // Mantis Issue 24723
                        //SalesPersontracking ob = new SalesPersontracking();
                        //DataTable dtfromtosumervisor = ob.MobileUserloginIDRellocation(txtuserid.Text.ToString().Trim());

                        LoginIDExist = 1;
                        // End of Mantis Issue 24723
                    }
                    // Mantis Issue 24723
                    if (LoginIDExist == 0)
                    {
                        if (Convert.ToString(Session["PageAccess"]).Trim() == "All" || Convert.ToString(Session["PageAccess"]).Trim() == "Add" || Convert.ToString(Session["PageAccess"]).Trim() == "DelAdd")
                        {

                            //// Encrypt  the Password
                            Encryption epasswrd = new Encryption();
                            string Encryptpass = epasswrd.Encrypt(txtpassword.Text.Trim());

                            // oDBEngine.InsertDataFromAnotherTable(" tbl_master_user ", " user_name,user_branchId,user_loginId,user_password,user_contactId,user_group,CreateDate,CreateUser,
                            //user_lastsegement,user_TimeForTickerRefrsh,user_superuser,user_EntryProfile,user_AllowAccessIP,user_inactive,user_maclock,Gps_Accuracy,HierarchywiseTargetSettings,
                            //willLeaveApprovalEnable,IsAutoRevisitEnable,IsShowPlanDetails,IsMoreDetailsMandatory,IsShowMoreDetailsMandatory,isMeetingAvailable,isRateNotEditable,IsShowTeamDetails,
                            //IsAllowPJPUpdateForTeam,willReportShow,isAddAttendence,isFingerPrintMandatoryForAttendance,isFingerPrintMandatoryForVisit,isSelfieMandatoryForAttendance", null, 
                            //"'" + txtusername.Text.Trim() + "','" + b_id + "','" + txtuserid.Text.Trim() + "','" + Encryptpass + "','" + contact + "','" + usergroup + "','" + CreateDate.ToString() 
                            //+ "','" + CreateUser + "', ( select top 1 grp_segmentId from tbl_master_userGroup where grp_id in(" + usergroup + ")),86400,'" + superuser + "','" 
                            //+ ddDataEntry.SelectedItem.Value + "','" + IPAddress.Trim() + "','" + isactive + "','" + isactivemac + "'," + txtgps.Text + "," + istargetsettings + "," + isLeaveApprovalEnable + ",
                            //" + IsAutoRevisitEnable + "," + IsShowPlanDetails + "," + IsMoreDetailsMandatory + "," + IsShowMoreDetailsMandatory + "," + isMeetingAvailable + "," + isRateNotEditable + "," + 
                            //IsShowTeamDetails + "," + IsAllowPJPUpdateForTeam + "," + willReportShow + "," + isAddAttendence + "," + isFingerPrintMandatoryForAttendance + "," + isFingerPrintMandatoryForVisit + ",
                            //" + isSelfieMandatoryForAttendance + "", null);

                            ProcedureExecute proc = new ProcedureExecute("PRC_FTSInsertUpdateUser");
                            proc.AddPara("@ACTION", "INSERT");
                            proc.AddPara("@txtusername", txtusername.Text.Trim());
                            proc.AddPara("@b_id", b_id);
                            proc.AddPara("@txtuserid", txtuserid.Text.Trim());
                            proc.AddPara("@Encryptpass", Encryptpass);
                            proc.AddPara("@contact", contact);
                            proc.AddPara("@usergroup", usergroup);
                            proc.AddPara("@CreateDate", CreateDate.ToString());
                            proc.AddPara("@CreateUser", CreateUser);
                            proc.AddPara("@superuser", superuser);
                            proc.AddPara("@ddDataEntry", ddDataEntry.SelectedItem.Value);
                            proc.AddPara("@IPAddress", IPAddress.Trim());
                            proc.AddPara("@isactive", isactive);
                            proc.AddPara("@isactivemac", isactivemac);
                            proc.AddPara("@txtgps", txtgps.Text);
                            proc.AddPara("@istargetsettings", istargetsettings);
                            proc.AddPara("@isLeaveApprovalEnable", isLeaveApprovalEnable);
                            proc.AddPara("@IsAutoRevisitEnable", IsAutoRevisitEnable);
                            proc.AddPara("@IsShowPlanDetails", IsShowPlanDetails);
                            proc.AddPara("@IsMoreDetailsMandatory", IsMoreDetailsMandatory);
                            proc.AddPara("@IsShowMoreDetailsMandatory", IsShowMoreDetailsMandatory);

                            proc.AddPara("@isMeetingAvailable", isMeetingAvailable);
                            proc.AddPara("@isRateNotEditable", isRateNotEditable);
                            proc.AddPara("@IsShowTeamDetails", IsShowTeamDetails);
                            proc.AddPara("@IsAllowPJPUpdateForTeam", IsAllowPJPUpdateForTeam);
                            proc.AddPara("@willReportShow", willReportShow);
                            proc.AddPara("@isFingerPrintMandatoryForAttendance", isFingerPrintMandatoryForAttendance);
                            proc.AddPara("@isFingerPrintMandatoryForVisit", isFingerPrintMandatoryForVisit);
                            proc.AddPara("@isSelfieMandatoryForAttendance", isSelfieMandatoryForAttendance);

                            proc.AddPara("@isAttendanceReportShow", isAttendanceReportShow);
                            proc.AddPara("@isPerformanceReportShow", isPerformanceReportShow);
                            proc.AddPara("@isVisitReportShow", isVisitReportShow);
                            proc.AddPara("@willTimesheetShow", willTimesheetShow);
                            proc.AddPara("@isAttendanceFeatureOnly", isAttendanceFeatureOnly);
                            proc.AddPara("@isOrderShow", isOrderShow);
                            proc.AddPara("@isVisitShow", isVisitShow);
                            proc.AddPara("@iscollectioninMenuShow", iscollectioninMenuShow);
                            proc.AddPara("@isShopAddEditAvailable", isShopAddEditAvailable);
                            proc.AddPara("@isEntityCodeVisible", isEntityCodeVisible);
                            proc.AddPara("@isAreaMandatoryInPartyCreation", isAreaMandatoryInPartyCreation);
                            proc.AddPara("@isShowPartyInAreaWiseTeam", isShowPartyInAreaWiseTeam);
                            proc.AddPara("@isChangePasswordAllowed", isChangePasswordAllowed);
                            proc.AddPara("@isHomeRestrictAttendance", isHomeRestrictAttendance);
                            proc.AddPara("@isQuotationShow", isQuotationShow);
                            proc.AddPara("@IsStateMandatoryinReport", IsStateMandatoryinReport);

                            proc.AddPara("@isAchievementEnable", isAchievementEnable);
                            proc.AddPara("@isTarVsAchvEnable", isTarVsAchvEnable);
                            proc.AddPara("@shopLocAccuracy", txtshopLocAccuracy.Text);
                            proc.AddPara("@homeLocDistance", txthomeLocDistance.Text);


                            //New Settings 18-08-2020

                            proc.AddPara("@isQuotationPopupShow", isQuotationPopupShow);
                            proc.AddPara("@isOrderReplacedWithTeam", isOrderReplacedWithTeam);
                            proc.AddPara("@isMultipleAttendanceSelection", isMultipleAttendanceSelection);
                            proc.AddPara("@isOfflineTeam", isOfflineTeam);
                            proc.AddPara("@isDDShowForMeeting", isDDShowForMeeting);
                            proc.AddPara("@isDDMandatoryForMeeting", isDDMandatoryForMeeting);
                            proc.AddPara("@isAllTeamAvailable", isAllTeamAvailable);
                            proc.AddPara("@isRecordAudioEnable", isRecordAudioEnable);
                            proc.AddPara("@isNextVisitDateMandatory", isNextVisitDateMandatory);
                            proc.AddPara("@isShowCurrentLocNotifiaction", isShowCurrentLocNotifiaction);
                            proc.AddPara("@isUpdateWorkTypeEnable", isUpdateWorkTypeEnable);
                            proc.AddPara("@isLeaveEnable", isLeaveEnable);
                            proc.AddPara("@isOrderMailVisible", isOrderMailVisible);
                            proc.AddPara("@LateVisitSMS", LateVisitSMS);
                            proc.AddPara("@isShopEditEnable", isShopEditEnable);
                            proc.AddPara("@isTaskEnable", isTaskEnable);

                            proc.AddPara("@PartyType", PartyType);


                            proc.AddPara("@isAppInfoEnable", isAppInfoEnable);
                            proc.AddPara("@willDynamicShow", willDynamicShow);
                            proc.AddPara("@willActivityShow", willActivityShow);
                            proc.AddPara("@isDocumentRepoShow", isDocumentRepoShow);
                            proc.AddPara("@isChatBotShow", isChatBotShow);
                            proc.AddPara("@isAttendanceBotShow", isAttendanceBotShow);
                            proc.AddPara("@isVisitBotShow", isVisitBotShow);
                            proc.AddPara("@appInfoMins", txtDeviceInfoMin.Text);

                            //Add extra settings 01-12-2020
                            proc.AddPara("@isInstrumentCompulsory", isInstrumentCompulsory);
                            proc.AddPara("@isBankCompulsory", isBankCompulsory);
                            //Add extra settings 01-12-2020

                            //Add extra settings 12-05-2021
                            proc.AddPara("@isComplementaryUser", isComplementaryUser);
                            proc.AddPara("@isVisitPlanShow", isVisitPlanShow);
                            proc.AddPara("@isVisitPlanMandatory", isVisitPlanMandatory);
                            proc.AddPara("@isAttendanceDistanceShow", isAttendanceDistanceShow);
                            proc.AddPara("@willTimelineWithFixedLocationShow", willTimelineWithFixedLocationShow);
                            proc.AddPara("@isShowOrderRemarks", isShowOrderRemarks);
                            proc.AddPara("@isShowOrderSignature", isShowOrderSignature);
                            proc.AddPara("@isShowSmsForParty", isShowSmsForParty);
                            proc.AddPara("@isShowTimeline", isShowTimeline);
                            proc.AddPara("@willScanVisitingCard", willScanVisitingCard);
                            proc.AddPara("@isCreateQrCode", isCreateQrCode);
                            proc.AddPara("@isScanQrForRevisit", isScanQrForRevisit);
                            proc.AddPara("@isShowLogoutReason", isShowLogoutReason);
                            proc.AddPara("@willShowHomeLocReason", willShowHomeLocReason);
                            proc.AddPara("@willShowShopVisitReason", willShowShopVisitReason);
                            proc.AddPara("@willShowPartyStatus", willShowPartyStatus);
                            proc.AddPara("@willShowEntityTypeforShop", willShowEntityTypeforShop);
                            proc.AddPara("@isShowRetailerEntity", isShowRetailerEntity);
                            proc.AddPara("@isShowDealerForDD", isShowDealerForDD);
                            proc.AddPara("@isShowBeatGroup", isShowBeatGroup);
                            proc.AddPara("@isShowShopBeatWise", isShowShopBeatWise);
                            proc.AddPara("@isShowBankDetailsForShop", isShowBankDetailsForShop);
                            proc.AddPara("@isShowOTPVerificationPopup", isShowOTPVerificationPopup);
                            proc.AddPara("@isShowMicroLearing", isShowMicroLearing);
                            proc.AddPara("@isMultipleVisitEnable", isMultipleVisitEnable);
                            proc.AddPara("@isShowVisitRemarks", isShowVisitRemarks);
                            proc.AddPara("@isShowNearbyCustomer", isShowNearbyCustomer);
                            proc.AddPara("@isServiceFeatureEnable", isServiceFeatureEnable);
                            proc.AddPara("@isPatientDetailsShowInOrder", isPatientDetailsShowInOrder);
                            proc.AddPara("@isPatientDetailsShowInCollection", isPatientDetailsShowInCollection);
                            proc.AddPara("@isAttachmentMandatory", isAttachmentMandatory);
                            proc.AddPara("@isShopImageMandatory", isShopImageMandatory);
                            //Add extra settings 12-05-2021

                            //Add extra settings 27-07-2021
                            proc.AddPara("@isLogShareinLogin", isLogShareinLogin);
                            proc.AddPara("@IsCompetitorenable", IsCompetitorenable);
                            proc.AddPara("@IsOrderStatusRequired", IsOrderStatusRequired);
                            proc.AddPara("@IsCurrentStockEnable", IsCurrentStockEnable);
                            proc.AddPara("@IsCurrentStockApplicableforAll", IsCurrentStockApplicableforAll);
                            proc.AddPara("@IscompetitorStockRequired", IscompetitorStockRequired);
                            proc.AddPara("@IsCompetitorStockforParty", IsCompetitorStockforParty);
                            proc.AddPara("@ShowFaceRegInMenu", ShowFaceRegInMenu);
                            proc.AddPara("@IsFaceDetection", IsFaceDetection);
                            //proc.AddPara("@isFaceRegistered", isFaceRegistered);
                            proc.AddPara("@IsUserwiseDistributer", IsUserwiseDistributer);
                            proc.AddPara("@IsPhotoDeleteShow", IsPhotoDeleteShow);
                            proc.AddPara("@IsAllDataInPortalwithHeirarchy", IsAllDataInPortalwithHeirarchy);
                            proc.AddPara("@IsFaceDetectionWithCaptcha", IsFaceDetectionWithCaptcha);
                            //Add extra settings 27-07-2021
                            //Add Etra setting 06-08-2021
                            proc.AddPara("@IsShowMenuAddAttendance", IsShowMenuAddAttendance);
                            proc.AddPara("@IsShowMenuAttendance", IsShowMenuAttendance);
                            proc.AddPara("@IsShowMenuShops", IsShowMenuShops);
                            proc.AddPara("@IsShowMenuOutstandingDetailsPPDD", IsShowMenuOutstandingDetailsPPDD);
                            proc.AddPara("@IsShowMenuStockDetailsPPDD", IsShowMenuStockDetailsPPDD);
                            proc.AddPara("@IsShowMenuTA", IsShowMenuTA);
                            proc.AddPara("@IsShowMenuMISReport", IsShowMenuMISReport);
                            proc.AddPara("@IsShowMenuReimbursement", IsShowMenuReimbursement);
                            proc.AddPara("@IsShowMenuAchievement", IsShowMenuAchievement);
                            proc.AddPara("@IsShowMenuMapView", IsShowMenuMapView);
                            proc.AddPara("@IsShowMenuShareLocation", IsShowMenuShareLocation);
                            proc.AddPara("@IsShowMenuHomeLocation", IsShowMenuHomeLocation);
                            proc.AddPara("@IsShowMenuWeatherDetails", IsShowMenuWeatherDetails);
                            proc.AddPara("@IsShowMenuChat", IsShowMenuChat);
                            proc.AddPara("@IsShowMenuScanQRCode", IsShowMenuScanQRCode);
                            proc.AddPara("@IsShowMenuPermissionInfo", IsShowMenuPermissionInfo);
                            proc.AddPara("@IsShowMenuAnyDesk", IsShowMenuAnyDesk);

                            proc.AddPara("@IsDocRepoFromPortal", IsDocRepoFromPortal);
                            proc.AddPara("@IsDocRepShareDownloadAllowed", IsDocRepShareDownloadAllowed);
                            proc.AddPara("@IsScreenRecorderEnable", IsScreenRecorderEnable);
                            //Add Etra setting 06-08-2021

                            //Add Etra setting 25-08-2021
                            proc.AddPara("@IsShowPartyOnAppDashboard", IsShowPartyOnAppDashboard);
                            proc.AddPara("@IsShowAttendanceOnAppDashboard", IsShowAttendanceOnAppDashboard);
                            proc.AddPara("@IsShowTotalVisitsOnAppDashboard", IsShowTotalVisitsOnAppDashboard);
                            proc.AddPara("@IsShowVisitDurationOnAppDashboard", IsShowVisitDurationOnAppDashboard);
                            proc.AddPara("@IsShowDayStart", IsShowDayStart);
                            proc.AddPara("@IsshowDayStartSelfie", IsshowDayStartSelfie);
                            proc.AddPara("@IsShowDayEnd", IsShowDayEnd);
                            proc.AddPara("@IsshowDayEndSelfie", IsshowDayEndSelfie);
                            proc.AddPara("@IsShowLeaveInAttendance", IsShowLeaveInAttendance);
                            proc.AddPara("@IsLeaveGPSTrack", IsLeaveGPSTrack);
                            proc.AddPara("@IsShowActivitiesInTeam", IsShowActivitiesInTeam);
                            proc.AddPara("@IsShowMarkDistVisitOnDshbrd", IsShowMarkDistVisitOnDshbrd);
                            //Mantis Issue 24408,24364
                            proc.AddPara("@IsRevisitRemarksMandatory", IsRevisitRemarksMandatory);
                            proc.AddPara("@GPSAlert", GPSAlert);
                            proc.AddPara("@GPSAlertwithSound", GPSAlertwithSound);
                            //End of Mantis Issue 24408,24364
                            // Mantis Issue 24596,24597
                            proc.AddPara("@FaceRegistrationFrontCamera", FaceRegistrationFrontCamera);
                            proc.AddPara("@MRPInOrder", MRPInOrder);
                            // End of Mantis Issue 24596,24597
                            //Add Etra setting 25-08-2021
                            /*Rev work start 11.05.2022 Mantise No:0024880 Horizontal Performance Summary & detail report required with hierarchy setting*/
                            proc.AddPara("@isHorizontalPerformReportShow", isHorizontalPerformReportShow);
                            /*Rev work close 11.05.2022 Mantise No:0024880 Horizontal Performance Summary & detail report required with hierarchy setting*/
                            //Mantis Issue 25015
                            proc.AddPara("@FaceRegTypeID", FaceRegTypeID);
                            //End of Mantis Issue 25015
                            proc.AddPara("@DistributerwisePartyOrderReport", DistributerwisePartyOrderReport);
                            //End of Mantis Issue 25035
                            //Mantis Issue 25116
                            proc.AddPara("@ShowAttednaceClearmenu", ShowAttednaceClearmenu);
                            //End of Mantis Issue 25116

                            // Mantis Issue 25207
                            proc.AddPara("@ShowAllowProfileUpdate", ShowAllowProfileUpdate);
                            proc.AddPara("@ShowAutoDDSelect", ShowAutoDDSelect);
                            proc.AddPara("@ShowBatterySetting", ShowBatterySetting);
                            proc.AddPara("@ShowCommonAINotification", ShowCommonAINotification);
                            proc.AddPara("@ShowCustom_Configuration", ShowCustom_Configuration);
                            proc.AddPara("@ShowisAadharRegistered", ShowisAadharRegistered);
                            proc.AddPara("@ShowIsActivateNewOrderScreenwithSize", ShowIsActivateNewOrderScreenwithSize);
                            proc.AddPara("@ShowIsAllowBreakageTracking", ShowIsAllowBreakageTracking);
                            proc.AddPara("@ShowIsAllowBreakageTrackingunderTeam", ShowIsAllowBreakageTrackingunderTeam);
                            proc.AddPara("@ShowIsAllowClickForPhotoRegister", ShowIsAllowClickForPhotoRegister);
                            proc.AddPara("@ShowIsAllowClickForVisit", ShowIsAllowClickForVisit);
                            proc.AddPara("@ShowIsAllowClickForVisitForSpecificUser", ShowIsAllowClickForVisitForSpecificUser);
                            proc.AddPara("@ShowIsAllowShopStatusUpdate", ShowIsAllowShopStatusUpdate);
                            proc.AddPara("@ShowIsAlternateNoForCustomer", ShowIsAlternateNoForCustomer);
                            proc.AddPara("@ShowIsAttendVisitShowInDashboard", ShowIsAttendVisitShowInDashboard);
                            proc.AddPara("@ShowIsAutoLeadActivityDateTime", ShowIsAutoLeadActivityDateTime);
                            proc.AddPara("@ShowIsBeatRouteReportAvailableinTeam", ShowIsBeatRouteReportAvailableinTeam);
                            proc.AddPara("@ShowIsCollectionOrderWise", ShowIsCollectionOrderWise);
                            proc.AddPara("@ShowIsFaceRecognitionOnEyeblink", ShowIsFaceRecognitionOnEyeblink);
                            proc.AddPara("@ShowisFaceRegistered", ShowisFaceRegistered);
                            proc.AddPara("@ShowIsFeedbackAvailableInShop", ShowIsFeedbackAvailableInShop);
                            proc.AddPara("@ShowIsFeedbackHistoryActivated", ShowIsFeedbackHistoryActivated);
                            proc.AddPara("@ShowIsFromPortal", ShowIsFromPortal);
                            proc.AddPara("@ShowIsIMEICheck", ShowIsIMEICheck);
                            proc.AddPara("@ShowIslandlineforCustomer", ShowIslandlineforCustomer);
                            proc.AddPara("@ShowIsNewQuotationfeatureOn", ShowIsNewQuotationfeatureOn);
                            proc.AddPara("@ShowIsNewQuotationNumberManual", ShowIsNewQuotationNumberManual);
                            proc.AddPara("@ShowIsPendingCollectionRequiredUnderTeam", ShowIsPendingCollectionRequiredUnderTeam);
                            proc.AddPara("@ShowIsprojectforCustomer", ShowIsprojectforCustomer);
                            proc.AddPara("@ShowIsRateEnabledforNewOrderScreenwithSize", ShowIsRateEnabledforNewOrderScreenwithSize);
                            proc.AddPara("@ShowIsRestrictNearbyGeofence", ShowIsRestrictNearbyGeofence);
                            proc.AddPara("@ShowIsReturnEnableforParty", ShowIsReturnEnableforParty);
                            proc.AddPara("@ShowIsShowHomeLocationMap", ShowIsShowHomeLocationMap);
                            proc.AddPara("@ShowIsShowManualPhotoRegnInApp", ShowIsShowManualPhotoRegnInApp);
                            proc.AddPara("@ShowIsShowMyDetails", ShowIsShowMyDetails);
                            proc.AddPara("@ShowIsShowNearByTeam", ShowIsShowNearByTeam);
                            proc.AddPara("@ShowIsShowRepeatOrderinNotification", ShowIsShowRepeatOrderinNotification);
                            proc.AddPara("@ShowIsShowRepeatOrdersNotificationinTeam", ShowIsShowRepeatOrdersNotificationinTeam);
                            proc.AddPara("@ShowIsShowRevisitRemarksPopup", ShowIsShowRevisitRemarksPopup);
                            proc.AddPara("@ShowIsShowTypeInRegistration", ShowIsShowTypeInRegistration);
                            proc.AddPara("@ShowIsTeamAttendance", ShowIsTeamAttendance);
                            proc.AddPara("@ShowIsTeamAttenWithoutPhoto", ShowIsTeamAttenWithoutPhoto);
                            proc.AddPara("@ShowIsWhatsappNoForCustomer", ShowIsWhatsappNoForCustomer);
                            proc.AddPara("@ShowLeaveapprovalfromsupervisor", ShowLeaveapprovalfromsupervisor);
                            proc.AddPara("@ShowLeaveapprovalfromsupervisorinteam", ShowLeaveapprovalfromsupervisorinteam);
                            proc.AddPara("@ShowLogoutWithLogFile", ShowLogoutWithLogFile);
                            proc.AddPara("@ShowMarkAttendNotification", ShowMarkAttendNotification);
                            proc.AddPara("@ShowPartyUpdateAddrMandatory", ShowPartyUpdateAddrMandatory);
                            proc.AddPara("@ShowPowerSaverSetting", ShowPowerSaverSetting);
                            proc.AddPara("@ShowShopScreenAftVisitRevisit", ShowShopScreenAftVisitRevisit);
                            proc.AddPara("@Show_App_Logout_Notification", Show_App_Logout_Notification);
                            proc.AddPara("@ShowAmountNewQuotation", ShowAmountNewQuotation);
                            proc.AddPara("@ShowAutoRevisitInAppMenu", ShowAutoRevisitInAppMenu);
                            proc.AddPara("@ShowAutoRevisitInDashboard", ShowAutoRevisitInDashboard);
                            proc.AddPara("@ShowCollectionAlert", ShowCollectionAlert);
                            proc.AddPara("@ShowCollectionOnlywithInvoiceDetails", ShowCollectionOnlywithInvoiceDetails);
                            proc.AddPara("@ShowPurposeInShopVisit", ShowPurposeInShopVisit);
                            proc.AddPara("@ShowQuantityNewQuotation", ShowQuantityNewQuotation);
                            proc.AddPara("@ShowTotalVisitAppMenu", ShowTotalVisitAppMenu);
                            proc.AddPara("@ShowUserwiseLeadMenu", ShowUserwiseLeadMenu);
                            proc.AddPara("@ShowZeroCollectioninAlert", ShowZeroCollectioninAlert);
                            proc.AddPara("@ShowUpdateOtherID", ShowUpdateOtherID);
                            proc.AddPara("@ShowUpdateUserID", ShowUpdateUserID);
                            proc.AddPara("@ShowUpdateUserName", ShowUpdateUserName);
                            proc.AddPara("@ShowWillRoomDBShareinLogin", ShowWillRoomDBShareinLogin);
                            // End of Mantis Issue 25207
                            // Rev 1.0
                            proc.AddPara("@IsShowEmployeePerformance", IsShowEmployeePerformance);
                            // End of Rev 1.0
                            // Rev 2.0
                            proc.AddPara("@IsShowBeatInMenu", IsShowBeatInMenu);
                            // End of Rev 2.0
                            // Rev 3.0
                            proc.AddPara("@IsShowWorkType", IsShowWorkType);
                            proc.AddPara("@IsShowMarketSpendTimer", IsShowMarketSpendTimer);
                            proc.AddPara("@IsShowUploadImageInAppProfile", IsShowUploadImageInAppProfile);
                            proc.AddPara("@IsShowCalendar", IsShowCalendar);
                            proc.AddPara("@IsShowCalculator", IsShowCalculator);
                            proc.AddPara("@IsShowInactiveCustomer", IsShowInactiveCustomer);
                            proc.AddPara("@IsShowAttendanceSummary", IsShowAttendanceSummary);
                            // End of Rev 3.0
                            // Rev 4.0
                            proc.AddPara("@IsMenuShowAIMarketAssistant", IsMenuShowAIMarketAssistant);
                            proc.AddPara("@IsUsbDebuggingRestricted", IsUsbDebuggingRestricted);
                            // End of Rev 4.0
                            // Rev 5.0
                            proc.AddPara("@IsShowLatLongInOutletMaster", IsShowLatLongInOutletMaster);
                            // End of Rev 5.0
                            // Rev 6.0
                            proc.AddPara("@IsCallLogHistoryActivated", IsCallLogHistoryActivated);
                            // End of Rev 6.0
                            // Rev 7.0
                            proc.AddPara("@IsShowMenuCRMContacts", IsShowMenuCRMContacts);
                            proc.AddPara("@IsCheckBatteryOptimization", IsCheckBatteryOptimization);
                            // End of Rev 7.0
                            // Rev 8.0
                            proc.AddPara("@ShowUserwisePartyWithGeoFence", ShowUserwisePartyWithGeoFence);
                            proc.AddPara("@ShowUserwisePartyWithCreateOrder", ShowUserwisePartyWithCreateOrder);
                            // End of Rev 8.0
                            // Rev 9.0
                            proc.AddPara("@AdditionalinfoRequiredforContactListing", AdditionalinfoRequiredforContactListing);
                            proc.AddPara("@AdditionalinfoRequiredforContactAdd", AdditionalinfoRequiredforContactAdd);
                            proc.AddPara("@ContactAddresswithGeofence", ContactAddresswithGeofence);
                            // End of Rev 9.0

                            // Rev 10.0
                            proc.AddPara("@IsCRMPhonebookSyncEnable", IsCRMPhonebookSyncEnable);
                            proc.AddPara("@IsCRMSchedulerEnable", IsCRMSchedulerEnable);
                            proc.AddPara("@IsCRMAddEnable", IsCRMAddEnable);
                            proc.AddPara("@IsCRMEditEnable", IsCRMEditEnable);
                            // End of Rev 10.0
                            // Rev 11.0
                            proc.AddPara("@IsShowAddressInParty", IsShowAddressInParty);
                            proc.AddPara("@IsShowUpdateInvoiceDetails", IsShowUpdateInvoiceDetails);
                            // End of Rev 11.0
                            // Rev 12.0
                            proc.AddPara("@IsSpecialPriceWithEmployee", IsSpecialPriceWithEmployee);
                            // End of Rev 12.0
                            // Rev 13.0
                            proc.AddPara("@IsShowCRMOpportunity", IsShowCRMOpportunity);
                            proc.AddPara("@IsEditEnableforOpportunity", IsEditEnableforOpportunity);
                            proc.AddPara("@IsDeleteEnableforOpportunity", IsDeleteEnableforOpportunity);
                            // End of Rev 13.0
                            // Rev 14.0
                            proc.AddPara("@IsShowDateWiseOrderInApp", IsShowDateWiseOrderInApp);
                            // End of Rev 14.0
                            // Rev 16.0
                            proc.AddPara("@IsUserWiseLMSEnable", IsUserWiseLMSEnable);
                            proc.AddPara("@IsUserWiseLMSFeatureOnly", IsUserWiseLMSFeatureOnly);
                            // End of Rev 16.0
                            // Rev 17.0
                            proc.AddPara("@IsUserWiseRecordAudioEnableForVisitRevisit", IsUserWiseRecordAudioEnableForVisitRevisit);
                            // End of Rev 17.0

                            // Rev 18.0
                            proc.AddPara("@ShowClearQuiz", ShowClearQuiz);
                            // End of Rev 18.0
                            // Rev 19.0
                            proc.AddPara("@IsAllowProductCurrentStockUpdateFromApp", IsAllowProductCurrentStockUpdateFromApp);
                            // End of Rev 19.0
                            // Rev 21.0
                            proc.AddPara("@ShowTargetOnApp", ShowTargetOnApp);
                            // End of Rev 21.0

                            DataTable dt = proc.GetTable();


                            string[,] userid = oDBEngine.GetFieldValue("tbl_master_user", "max(user_id)", null, 1);


                            //oDBEngine.InsertDataFromAnotherTable(" tbl_master_user ", " user_name,user_branchId,user_loginId,user_password,user_contactId,user_group,CreateDate,CreateUser,user_lastsegement,user_TimeForTickerRefrsh,user_superuser,user_EntryProfile,user_AllowAccessIP,user_inactive", null, "'" + txtusername.Text.Trim() + "','" + b_id + "','" + txtloginid.Text.Trim() + "','" + txtpassword.Text.Trim() + "','" + contact + "','" + usergroup + "','" + CreateDate.ToString() + "','" + CreateUser + "',( select top 1 grp_segmentId from tbl_master_userGroup where grp_id in(" + usergroup + ")),86400,'" + superuser + "','" + ddDataEntry.SelectedItem.Value + "','" + IPAddress.Trim() + "','" + isactive + "'", null);
                            //string[,] userid = oDBEngine.GetFieldValue("tbl_master_user", "max(user_id)", null, 1);

                            //Start Leave Approver Tanmoy

                            DataSet dsApprove = new DataSet();
                            dsApprove = oDBEngine.PopulateData("ID", "FTS_LEAVE_APPROVER", "APPROVAR_ID='" + userid[0, 0] + "'");

                            if (chkLeaveApprover.Checked == true)
                            {
                                if (dsApprove.Tables["TableName"].Rows.Count == 0)
                                {
                                    oDBEngine.InsertDataFromAnotherTable("FTS_LEAVE_APPROVER ", "APPROVAR_ID", null, "'" + userid[0, 0] + "'", null);
                                }
                            }
                            else
                            {
                                if (dsApprove.Tables["TableName"].Rows.Count > 0)
                                {
                                    oDBEngine.DeleteValue("FTS_LEAVE_APPROVER", "APPROVAR_ID='" + userid[0, 0] + "'");
                                }
                            }
                            //End Leave Approver Tanmoy

                            string splitsegname = segname[0, 0].Split('-')[0].ToString().Trim();
                            // string splitsegname1 = segname[0, 0].Split('-')[1].ToString().Trim(); 

                            string[,] exchsegid = oDBEngine.GetFieldValue("Master_Exchange", "top 1 Exchange_Id", "Exchange_ShortName='" + splitsegname + "'", 1);

                            // Jitendra- Need to work in Financial year validation, this time removed it temporarly
                            //string[,] finyr = oDBEngine.GetFieldValue("Master_FinYear", "top 1 FinYear_Code", "Getdate() between FinYear_StartDate and FinYear_EndDate", 1);
                            // string[,] finyr = oDBEngine.GetFieldValue("Master_FinYear", "top 1 FinYear_Code", null, 1);

                            string FinancialYear = GetFinancialYear();



                            string[,] exhCntID = oDBEngine.GetFieldValue("Tbl_Master_Exchange", "top 1 exh_CntID", "Exh_ShortName= '" + splitsegname.ToString().Trim() + "'", 1);
                            //  string[,] exchmaster = oDBEngine.GetFieldValue("Master_ExchangeSegments", "top 1 exchangesegment_id", "Exchangesegment_code='" + splitsegname1 + "'", 1);
                            //  string[,] settno = oDBEngine.GetFieldValue("Master_Settlements", "top 1 Settlements_Number", "Settlements_ExchangeSegmentID='" + exchmaster[0, 0] + "' and Settlements_FinYear='" + finyr[0, 0] + "'   and Settlements_StartDateTime=(Select Max(Settlements_StartDateTime) from Master_Settlements Where Settlements_ExchangeSegmentID='" + exchmaster[0, 0] + "' and Settlements_FinYear='" + finyr[0, 0] + "'  ) ", 1);                                
                            //string[,] settno1 = oDBEngine.GetFieldValue("Master_Settlements", "top 1 Settlements_Number", "Settlements_ExchangeSegmentID='" + exchsegid[0, 0] + "' and Settlements_FinYear='" + finyr[0, 0] + "' and Settlements_TypeSuffix='W'  and Settlements_StartDateTime=(Select Max(Settlements_StartDateTime) from Master_Settlements Where Settlements_ExchangeSegmentID='" + exchsegid[0, 0] + "' and Settlements_FinYear='" + finyr[0, 0] + "' and Settlements_TypeSuffix='W' ) ", 1);
                            //string[,] settno2 = oDBEngine.GetFieldValue("Master_Settlements", "top 1 Settlements_Number", "Settlements_ExchangeSegmentID='" + exchsegid[0, 0] + "' and Settlements_FinYear='" + finyr[0, 0] + "' and Settlements_TypeSuffix='F'  and Settlements_StartDateTime=(Select Max(Settlements_StartDateTime) from Master_Settlements Where Settlements_ExchangeSegmentID='" + exchsegid[0, 0] + "' and Settlements_FinYear='" + finyr[0, 0] + "' and Settlements_TypeSuffix='F' ) ", 1);
                            // string[,] settno = oDBEngine.GetFieldValue("Master_Settlements", "top 1 Settlements_Number", "Settlements_ExchangeSegmentID='" + exchsegid[0, 0] + "' and Settlements_FinYear='" + finyr[0, 0] + "' and (case when '" + splitsegname1 + "==CM" + "' and '" + exchsegid[0, 0] + "==1" + "' then Settlements_TypeSuffix='N' when '" + splitsegname1 + "==CM" + "' and '" + exchsegid[0, 0] + "==2" + "'  then Settlements_TypeSuffix='W' else Settlements_TypeSuffix='F' end) and Settlements_StartDateTime=(Select Max(Settlements_StartDateTime) from Master_Settlements Where Settlements_ExchangeSegmentID='" + exchsegid[0, 0] + "' and Settlements_FinYear='" + finyr[0, 0] + "' and (case when '" + splitsegname1 + "==CM" + "' and '" + exchsegid[0, 0] + "==1" + "' then Settlements_TypeSuffix='N' when '" + splitsegname1 + "==CM" + "' and '" + exchsegid[0, 0] + "==2" + "' then Settlements_TypeSuffix='W' else Settlements_TypeSuffix='F' end)) ", 1);

                            // string[,] companymain = oDBEngine.GetFieldValue("Tbl_Master_companyExchange", "top 1 Exch_InternalID,Exch_CompID", "Exch_ExchID='" + exhCntID[0, 0].ToString().Trim() + "' and exch_segmentId='1'", 2);
                            // oDBEngine.InsurtFieldValue("tbl_trans_LastSegment", "ls_cntid,ls_lastsegment,ls_userid,ls_lastdpcoid,ls_lastCompany,ls_lastFinYear,ls_lastSettlementNo,ls_lastSettlementType", "'" + contact + "','" + grpsegment[0, 0] + "','" + userid[0, 0] + "','" + companymain[0, 0] + "','" + companymain[0, 1].ToString() + "','" + finyr[0, 0].ToString().Trim() + "','','N'");


                            //Added New code to add eefault company in the tbl_master_user
                            string[,] userInternalId = oDBEngine.GetFieldValue("tbl_master_user", "user_Contactid", "user_id=" + userid[0, 0] + "", 1);
                            //DataTable dtcmp = oDBEngine.GetDataTable(" tbl_master_company  ", "*", "cmp_id=(select emp_organization from tbl_trans_employeectc where emp_cntId='" + userInternalId[0,0] + "')");
                            DataTable dtcmp = oDBEngine.GetDataTable(" tbl_master_company  ", "*", "cmp_id=(select top 1 emp_organization  from tbl_trans_employeectc where emp_cntId='" + userInternalId[0, 0] + "' and emp_id=(select MAX(emp_id) from tbl_trans_employeectc e where e.emp_cntId='" + userInternalId[0, 0] + "'))");

                            if (dtcmp.Rows.Count > 0)
                            {
                                string SegmentId = "1";
                                oDBEngine.InsurtFieldValue("Master_UserCompany", "UserCompany_UserID,UserCompany_CompanyID,UserCompany_CreateUser,UserCompany_CreateDateTime", "'" + userid[0, 0] + "','" + Convert.ToString(dtcmp.Rows[0]["cmp_internalid"]) + "','" + Convert.ToString(Session["userid"]) + "','" + DateTime.Now + "'");
                                //oDBEngine.InsurtFieldValue("tbl_trans_LastSegment", "ls_cntid,ls_lastsegment,ls_userid,ls_lastdpcoid,ls_lastCompany,ls_lastFinYear,ls_lastSettlementNo,ls_lastSettlementType", "'" + contact + "','" + grpsegment[0, 0] + "','" + userid[0, 0] + "','" + SegmentId + "','" + Convert.ToString(dtcmp.Rows[0]["cmp_internalid"]) + "','" + finyr[0, 0].ToString().Trim() + "','','N'");
                                oDBEngine.InsurtFieldValue("tbl_trans_LastSegment", "ls_cntid,ls_lastsegment,ls_userid,ls_lastdpcoid,ls_lastCompany,ls_lastFinYear,ls_lastSettlementNo,ls_lastSettlementType", "'" + contact + "','" + grpsegment[0, 0] + "','" + userid[0, 0] + "','" + SegmentId + "','" + Convert.ToString(dtcmp.Rows[0]["cmp_internalid"]) + "','" + FinancialYear.Trim() + "','','N'");

                            }
                            else
                            {
                                string[,] companymain = oDBEngine.GetFieldValue("Tbl_Master_companyExchange", "top 1 Exch_InternalID,Exch_CompID", "Exch_ExchID='" + exhCntID[0, 0].ToString().Trim() + "' and exch_segmentId='1'", 2);
                                //oDBEngine.InsurtFieldValue("tbl_trans_LastSegment", "ls_cntid,ls_lastsegment,ls_userid,ls_lastdpcoid,ls_lastCompany,ls_lastFinYear,ls_lastSettlementNo,ls_lastSettlementType", "'" + contact + "','" + grpsegment[0, 0] + "','" + userid[0, 0] + "','" + companymain[0, 0] + "','" + companymain[0, 1].ToString() + "','" + finyr[0, 0].ToString().Trim() + "','','N'");
                                oDBEngine.InsurtFieldValue("tbl_trans_LastSegment", "ls_cntid,ls_lastsegment,ls_userid,ls_lastdpcoid,ls_lastCompany,ls_lastFinYear,ls_lastSettlementNo,ls_lastSettlementType", "'" + contact + "','" + grpsegment[0, 0] + "','" + userid[0, 0] + "','" + companymain[0, 0] + "','" + companymain[0, 1].ToString() + "','" + FinancialYear.Trim() + "','','N'");
                            }
                            //--------------------------------
                            Response.Redirect("/OMS/Management/Master/root_user.aspx");
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "OnClick", "<script language='javascript'> alert('Not Authorised To Add Records!') </script>");
                        }
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "OnClick", "<script language='javascript'> alert('Duplicate user id found ! Please Talk to Administrator.') </script>");
                    }
                }
                //Rev work close 26.04.2022 Mantise ID:0024856: Copy feature add in User master
                else
                {
                    Int64 userId = Convert.ToInt64(Id);

                    // Mantis Issue 24723
                    //SalesPersontracking ob = new SalesPersontracking();
                    //DataTable dtfromtosumervisor = ob.MobileUserloginIDRellocationUpdate(txtuserid.Text.ToString().Trim(), Convert.ToString(userId));

                    int LoginIDExist = 0;

                    string[,] checkUser = oDBEngine.GetFieldValue("tbl_master_user", "user_loginId", " user_loginId='" + txtuserid.Text.ToString().Trim() + "' and user_id<> '"+Convert.ToString(userId)+"'", 1);
                    string check = checkUser[0, 0];
                    if (check != "n")
                    {
                        LoginIDExist = 1;
                    }

                    if (LoginIDExist == 0)
                    {
                        // End of Mantis Issue 24723

                        //if (dtfromtosumervisor.Rows.Count > 0 && Convert.ToString(dtfromtosumervisor.Rows[0][0]) == "0")
                        //{
                        //    Page.ClientScript.RegisterStartupScript(GetType(), "OnClick", "<script language='javascript'> alert('Already user exist with this Login Id.!') </script>");

                        //}
                        //else
                        //{
                        //if (Session["PageAccess"].ToString().Trim() == "Add" || Session["PageAccess"].ToString().Trim() == "Edit" || Session["PageAccess"].ToString().Trim() == "All")
                        if (Convert.ToString(Session["PageAccess"]).Trim() == "Add" || Convert.ToString(Session["PageAccess"]).Trim() == "Edit" || Convert.ToString(Session["PageAccess"]).Trim() == "All")
                        {

                            //  oDBEngine.SetFieldValue("tbl_master_user", "user_name='" + txtusername.Text + "',user_branchId=" + b_id + ",user_group='" + usergroup + "',user_loginId='" + txtuserid.Text + "',user_inactive='" + isactive + "',user_maclock='" + isactivemac + "',user_contactid='" + contact + "',LastModifyDate='" + CreateDate.ToString() + "',LastModifyUser='" + CreateUser + "',user_superuser ='" + superuser + "',user_EntryProfile='" + ddDataEntry.SelectedItem.Value + "',user_AllowAccessIP='" + IPAddress.Trim() + "',Gps_Accuracy=" + txtgps.Text + ",HierarchywiseTargetSettings=" + istargetsettings + ",willLeaveApprovalEnable=" + isLeaveApprovalEnable + ",IsAutoRevisitEnable=" + IsAutoRevisitEnable + ",IsShowPlanDetails=" + IsShowPlanDetails + ",IsMoreDetailsMandatory=" + IsMoreDetailsMandatory + ",IsShowMoreDetailsMandatory=" + IsShowMoreDetailsMandatory + ",isMeetingAvailable=" + isMeetingAvailable + ",isRateNotEditable=" + isRateNotEditable + ",IsShowTeamDetails=" + IsShowTeamDetails + ",IsAllowPJPUpdateForTeam=" + IsAllowPJPUpdateForTeam + ",willReportShow=" + willReportShow + ",isAddAttendence=" + isAddAttendence + ",isFingerPrintMandatoryForAttendance=" + isFingerPrintMandatoryForAttendance + ",isFingerPrintMandatoryForVisit=" + isFingerPrintMandatoryForVisit + ",isSelfieMandatoryForAttendance=" + isSelfieMandatoryForAttendance + "", " user_id ='" + userId + "'");

                            //// Encrypt  the Password
                            Encryption epasswrd = new Encryption();
                            string Encryptpass = epasswrd.Encrypt("breezefsm");

                            ProcedureExecute proc = new ProcedureExecute("PRC_FTSInsertUpdateUser");
                            proc.AddPara("@ACTION", "UPDATE");
                            proc.AddPara("@user_id", userId);
                            proc.AddPara("@txtusername", txtusername.Text.Trim());
                            proc.AddPara("@Encryptpass", Encryptpass);
                            proc.AddPara("@b_id", b_id);
                            proc.AddPara("@txtuserid", txtuserid.Text.Trim());
                            proc.AddPara("@contact", contact);
                            proc.AddPara("@usergroup", usergroup);
                            proc.AddPara("@CreateDate", CreateDate.ToString());
                            proc.AddPara("@CreateUser", CreateUser);
                            proc.AddPara("@superuser", superuser);
                            proc.AddPara("@ddDataEntry", ddDataEntry.SelectedItem.Value);
                            proc.AddPara("@IPAddress", IPAddress.Trim());
                            proc.AddPara("@isactive", isactive);
                            proc.AddPara("@isactivemac", isactivemac);
                            proc.AddPara("@txtgps", txtgps.Text);
                            proc.AddPara("@istargetsettings", istargetsettings);
                            proc.AddPara("@isLeaveApprovalEnable", isLeaveApprovalEnable);
                            proc.AddPara("@IsAutoRevisitEnable", IsAutoRevisitEnable);
                            proc.AddPara("@IsShowPlanDetails", IsShowPlanDetails);
                            proc.AddPara("@IsMoreDetailsMandatory", IsMoreDetailsMandatory);
                            proc.AddPara("@IsShowMoreDetailsMandatory", IsShowMoreDetailsMandatory);

                            proc.AddPara("@isMeetingAvailable", isMeetingAvailable);
                            proc.AddPara("@isRateNotEditable", isRateNotEditable);
                            proc.AddPara("@IsShowTeamDetails", IsShowTeamDetails);
                            proc.AddPara("@IsAllowPJPUpdateForTeam", IsAllowPJPUpdateForTeam);
                            proc.AddPara("@willReportShow", willReportShow);
                            proc.AddPara("@isFingerPrintMandatoryForAttendance", isFingerPrintMandatoryForAttendance);
                            proc.AddPara("@isFingerPrintMandatoryForVisit", isFingerPrintMandatoryForVisit);
                            proc.AddPara("@isSelfieMandatoryForAttendance", isSelfieMandatoryForAttendance);


                            proc.AddPara("@isAttendanceReportShow", isAttendanceReportShow);
                            proc.AddPara("@isPerformanceReportShow", isPerformanceReportShow);
                            proc.AddPara("@isVisitReportShow", isVisitReportShow);
                            proc.AddPara("@willTimesheetShow", willTimesheetShow);
                            proc.AddPara("@isAttendanceFeatureOnly", isAttendanceFeatureOnly);
                            proc.AddPara("@isOrderShow", isOrderShow);
                            proc.AddPara("@isVisitShow", isVisitShow);
                            proc.AddPara("@iscollectioninMenuShow", iscollectioninMenuShow);
                            proc.AddPara("@isShopAddEditAvailable", isShopAddEditAvailable);
                            proc.AddPara("@isEntityCodeVisible", isEntityCodeVisible);
                            proc.AddPara("@isAreaMandatoryInPartyCreation", isAreaMandatoryInPartyCreation);
                            proc.AddPara("@isShowPartyInAreaWiseTeam", isShowPartyInAreaWiseTeam);
                            proc.AddPara("@isChangePasswordAllowed", isChangePasswordAllowed);
                            proc.AddPara("@isHomeRestrictAttendance", isHomeRestrictAttendance);
                            proc.AddPara("@isQuotationShow", isQuotationShow);
                            proc.AddPara("@IsStateMandatoryinReport", IsStateMandatoryinReport);

                            proc.AddPara("@isAchievementEnable", isAchievementEnable);
                            proc.AddPara("@isTarVsAchvEnable", isTarVsAchvEnable);
                            proc.AddPara("@shopLocAccuracy", txtshopLocAccuracy.Text);
                            proc.AddPara("@homeLocDistance", txthomeLocDistance.Text);

                            //New Settings 18-08-2020

                            proc.AddPara("@isQuotationPopupShow", isQuotationPopupShow);
                            proc.AddPara("@isOrderReplacedWithTeam", isOrderReplacedWithTeam);
                            proc.AddPara("@isMultipleAttendanceSelection", isMultipleAttendanceSelection);
                            proc.AddPara("@isOfflineTeam", isOfflineTeam);
                            proc.AddPara("@isDDShowForMeeting", isDDShowForMeeting);
                            proc.AddPara("@isDDMandatoryForMeeting", isDDMandatoryForMeeting);
                            proc.AddPara("@isAllTeamAvailable", isAllTeamAvailable);
                            proc.AddPara("@isRecordAudioEnable", isRecordAudioEnable);
                            proc.AddPara("@isNextVisitDateMandatory", isNextVisitDateMandatory);
                            proc.AddPara("@isShowCurrentLocNotifiaction", isShowCurrentLocNotifiaction);
                            proc.AddPara("@isUpdateWorkTypeEnable", isUpdateWorkTypeEnable);
                            proc.AddPara("@isLeaveEnable", isLeaveEnable);
                            proc.AddPara("@isOrderMailVisible", isOrderMailVisible);
                            proc.AddPara("@LateVisitSMS", LateVisitSMS);
                            proc.AddPara("@isShopEditEnable", isShopEditEnable);
                            proc.AddPara("@isTaskEnable", isTaskEnable);

                            proc.AddPara("@PartyType", PartyType);


                            proc.AddPara("@isAppInfoEnable", isAppInfoEnable);
                            proc.AddPara("@willDynamicShow", willDynamicShow);
                            proc.AddPara("@willActivityShow", willActivityShow);
                            proc.AddPara("@isDocumentRepoShow", isDocumentRepoShow);
                            proc.AddPara("@isChatBotShow", isChatBotShow);
                            proc.AddPara("@isAttendanceBotShow", isAttendanceBotShow);
                            proc.AddPara("@isVisitBotShow", isVisitBotShow);
                            proc.AddPara("@appInfoMins", txtDeviceInfoMin.Text);
                            //Add extra settings 01-12-2020
                            proc.AddPara("@isInstrumentCompulsory", isInstrumentCompulsory);
                            proc.AddPara("@isBankCompulsory", isBankCompulsory);
                            //Add extra settings 01-12-2020
                            //Add extra settings 12-05-2021
                            proc.AddPara("@isComplementaryUser", isComplementaryUser);
                            proc.AddPara("@isVisitPlanShow", isVisitPlanShow);
                            proc.AddPara("@isVisitPlanMandatory", isVisitPlanMandatory);
                            proc.AddPara("@isAttendanceDistanceShow", isAttendanceDistanceShow);
                            proc.AddPara("@willTimelineWithFixedLocationShow", willTimelineWithFixedLocationShow);
                            proc.AddPara("@isShowOrderRemarks", isShowOrderRemarks);
                            proc.AddPara("@isShowOrderSignature", isShowOrderSignature);
                            proc.AddPara("@isShowSmsForParty", isShowSmsForParty);
                            proc.AddPara("@isShowTimeline", isShowTimeline);
                            proc.AddPara("@willScanVisitingCard", willScanVisitingCard);
                            proc.AddPara("@isCreateQrCode", isCreateQrCode);
                            proc.AddPara("@isScanQrForRevisit", isScanQrForRevisit);
                            proc.AddPara("@isShowLogoutReason", isShowLogoutReason);
                            proc.AddPara("@willShowHomeLocReason", willShowHomeLocReason);
                            proc.AddPara("@willShowShopVisitReason", willShowShopVisitReason);
                            proc.AddPara("@willShowPartyStatus", willShowPartyStatus);
                            proc.AddPara("@willShowEntityTypeforShop", willShowEntityTypeforShop);
                            proc.AddPara("@isShowRetailerEntity", isShowRetailerEntity);
                            proc.AddPara("@isShowDealerForDD", isShowDealerForDD);
                            proc.AddPara("@isShowBeatGroup", isShowBeatGroup);
                            proc.AddPara("@isShowShopBeatWise", isShowShopBeatWise);
                            proc.AddPara("@isShowBankDetailsForShop", isShowBankDetailsForShop);
                            proc.AddPara("@isShowOTPVerificationPopup", isShowOTPVerificationPopup);
                            proc.AddPara("@isShowMicroLearing", isShowMicroLearing);
                            proc.AddPara("@isMultipleVisitEnable", isMultipleVisitEnable);
                            proc.AddPara("@isShowVisitRemarks", isShowVisitRemarks);
                            proc.AddPara("@isShowNearbyCustomer", isShowNearbyCustomer);
                            proc.AddPara("@isServiceFeatureEnable", isServiceFeatureEnable);
                            proc.AddPara("@isPatientDetailsShowInOrder", isPatientDetailsShowInOrder);
                            proc.AddPara("@isPatientDetailsShowInCollection", isPatientDetailsShowInCollection);
                            proc.AddPara("@isAttachmentMandatory", isAttachmentMandatory);
                            proc.AddPara("@isShopImageMandatory", isShopImageMandatory);
                            //Add extra settings 12-05-2021

                            //Add extra settings 27-07-2021
                            proc.AddPara("@isLogShareinLogin", isLogShareinLogin);
                            proc.AddPara("@IsCompetitorenable", IsCompetitorenable);
                            proc.AddPara("@IsOrderStatusRequired", IsOrderStatusRequired);
                            proc.AddPara("@IsCurrentStockEnable", IsCurrentStockEnable);
                            proc.AddPara("@IsCurrentStockApplicableforAll", IsCurrentStockApplicableforAll);
                            proc.AddPara("@IscompetitorStockRequired", IscompetitorStockRequired);
                            proc.AddPara("@IsCompetitorStockforParty", IsCompetitorStockforParty);
                            proc.AddPara("@ShowFaceRegInMenu", ShowFaceRegInMenu);
                            proc.AddPara("@IsFaceDetection", IsFaceDetection);
                            //proc.AddPara("@isFaceRegistered", isFaceRegistered);
                            proc.AddPara("@IsUserwiseDistributer", IsUserwiseDistributer);
                            proc.AddPara("@IsPhotoDeleteShow", IsPhotoDeleteShow);
                            proc.AddPara("@IsAllDataInPortalwithHeirarchy", IsAllDataInPortalwithHeirarchy);
                            proc.AddPara("@IsFaceDetectionWithCaptcha", IsFaceDetectionWithCaptcha);
                            //Add extra settings 27-07-2021
                            //Add Etra setting 06-08-2021
                            proc.AddPara("@IsShowMenuAddAttendance", IsShowMenuAddAttendance);
                            proc.AddPara("@IsShowMenuAttendance", IsShowMenuAttendance);
                            proc.AddPara("@IsShowMenuShops", IsShowMenuShops);
                            proc.AddPara("@IsShowMenuOutstandingDetailsPPDD", IsShowMenuOutstandingDetailsPPDD);
                            proc.AddPara("@IsShowMenuStockDetailsPPDD", IsShowMenuStockDetailsPPDD);
                            proc.AddPara("@IsShowMenuTA", IsShowMenuTA);
                            proc.AddPara("@IsShowMenuMISReport", IsShowMenuMISReport);
                            proc.AddPara("@IsShowMenuReimbursement", IsShowMenuReimbursement);
                            proc.AddPara("@IsShowMenuAchievement", IsShowMenuAchievement);
                            proc.AddPara("@IsShowMenuMapView", IsShowMenuMapView);
                            proc.AddPara("@IsShowMenuShareLocation", IsShowMenuShareLocation);
                            proc.AddPara("@IsShowMenuHomeLocation", IsShowMenuHomeLocation);
                            proc.AddPara("@IsShowMenuWeatherDetails", IsShowMenuWeatherDetails);
                            proc.AddPara("@IsShowMenuChat", IsShowMenuChat);
                            proc.AddPara("@IsShowMenuScanQRCode", IsShowMenuScanQRCode);
                            proc.AddPara("@IsShowMenuPermissionInfo", IsShowMenuPermissionInfo);
                            proc.AddPara("@IsShowMenuAnyDesk", IsShowMenuAnyDesk);

                            proc.AddPara("@IsDocRepoFromPortal", IsDocRepoFromPortal);
                            proc.AddPara("@IsDocRepShareDownloadAllowed", IsDocRepShareDownloadAllowed);
                            proc.AddPara("@IsScreenRecorderEnable", IsScreenRecorderEnable);
                            //Add Etra setting 06-08-2021

                            //Add Etra setting 25-08-2021
                            proc.AddPara("@IsShowPartyOnAppDashboard", IsShowPartyOnAppDashboard);
                            proc.AddPara("@IsShowAttendanceOnAppDashboard", IsShowAttendanceOnAppDashboard);
                            proc.AddPara("@IsShowTotalVisitsOnAppDashboard", IsShowTotalVisitsOnAppDashboard);
                            proc.AddPara("@IsShowVisitDurationOnAppDashboard", IsShowVisitDurationOnAppDashboard);
                            proc.AddPara("@IsShowDayStart", IsShowDayStart);
                            proc.AddPara("@IsshowDayStartSelfie", IsshowDayStartSelfie);
                            proc.AddPara("@IsShowDayEnd", IsShowDayEnd);
                            proc.AddPara("@IsshowDayEndSelfie", IsshowDayEndSelfie);
                            proc.AddPara("@IsShowLeaveInAttendance", IsShowLeaveInAttendance);
                            proc.AddPara("@IsLeaveGPSTrack", IsLeaveGPSTrack);
                            proc.AddPara("@IsShowActivitiesInTeam", IsShowActivitiesInTeam);
                            proc.AddPara("@IsShowMarkDistVisitOnDshbrd", IsShowMarkDistVisitOnDshbrd);
                            //Add Etra setting 25-08-2021
                            //Mantis Issue 24408,24364
                            proc.AddPara("@IsRevisitRemarksMandatory", IsRevisitRemarksMandatory);
                            proc.AddPara("@GPSAlert", GPSAlert);
                            proc.AddPara("@GPSAlertwithSound", GPSAlertwithSound);
                            //End of Mantis Issue 24408,24364
                            // Mantis Issue 24596,24597
                            proc.AddPara("@FaceRegistrationFrontCamera", FaceRegistrationFrontCamera);
                            proc.AddPara("@MRPInOrder", MRPInOrder);
                            // End of Mantis Issue 24596,24597
                            /*Rev work start 11.05.2022 Mantise No:0024880 Horizontal Performance Summary & detail report required with hierarchy setting*/
                            proc.AddPara("@isHorizontalPerformReportShow", isHorizontalPerformReportShow);
                            /*Rev work close 11.05.2022 Mantise No:0024880 Horizontal Performance Summary & detail report required with hierarchy setting*/
                            //Mantis Issue 25015
                            proc.AddPara("@FaceRegTypeID", FaceRegTypeID);
                            //End of Mantis Issue 25015
                            //Mantis Issue 25035
                            proc.AddPara("@DistributerwisePartyOrderReport", DistributerwisePartyOrderReport);
                            //End of Mantis Issue 25035
                            //Mantis Issue 25116
                            proc.AddPara("@ShowAttednaceClearmenu", ShowAttednaceClearmenu);
                            //End of Mantis Issue 25116
                            // Mantis Issue 25207
                            proc.AddPara("@ShowAllowProfileUpdate", ShowAllowProfileUpdate);
                            proc.AddPara("@ShowAutoDDSelect", ShowAutoDDSelect);
                            proc.AddPara("@ShowBatterySetting", ShowBatterySetting);
                            proc.AddPara("@ShowCommonAINotification", ShowCommonAINotification);
                            proc.AddPara("@ShowCustom_Configuration", ShowCustom_Configuration);
                            proc.AddPara("@ShowisAadharRegistered", ShowisAadharRegistered);
                            proc.AddPara("@ShowIsActivateNewOrderScreenwithSize", ShowIsActivateNewOrderScreenwithSize);
                            proc.AddPara("@ShowIsAllowBreakageTracking", ShowIsAllowBreakageTracking);
                            proc.AddPara("@ShowIsAllowBreakageTrackingunderTeam", ShowIsAllowBreakageTrackingunderTeam);
                            proc.AddPara("@ShowIsAllowClickForPhotoRegister", ShowIsAllowClickForPhotoRegister);
                            proc.AddPara("@ShowIsAllowClickForVisit", ShowIsAllowClickForVisit);
                            proc.AddPara("@ShowIsAllowClickForVisitForSpecificUser", ShowIsAllowClickForVisitForSpecificUser);
                            proc.AddPara("@ShowIsAllowShopStatusUpdate", ShowIsAllowShopStatusUpdate);
                            proc.AddPara("@ShowIsAlternateNoForCustomer", ShowIsAlternateNoForCustomer);
                            proc.AddPara("@ShowIsAttendVisitShowInDashboard", ShowIsAttendVisitShowInDashboard);
                            proc.AddPara("@ShowIsAutoLeadActivityDateTime", ShowIsAutoLeadActivityDateTime);
                            proc.AddPara("@ShowIsBeatRouteReportAvailableinTeam", ShowIsBeatRouteReportAvailableinTeam);
                            proc.AddPara("@ShowIsCollectionOrderWise", ShowIsCollectionOrderWise);
                            proc.AddPara("@ShowIsFaceRecognitionOnEyeblink", ShowIsFaceRecognitionOnEyeblink);
                            proc.AddPara("@ShowisFaceRegistered", ShowisFaceRegistered);
                            proc.AddPara("@ShowIsFeedbackAvailableInShop", ShowIsFeedbackAvailableInShop);
                            proc.AddPara("@ShowIsFeedbackHistoryActivated", ShowIsFeedbackHistoryActivated);
                            proc.AddPara("@ShowIsFromPortal", ShowIsFromPortal);
                            proc.AddPara("@ShowIsIMEICheck", ShowIsIMEICheck);
                            proc.AddPara("@ShowIslandlineforCustomer", ShowIslandlineforCustomer);
                            proc.AddPara("@ShowIsNewQuotationfeatureOn", ShowIsNewQuotationfeatureOn);
                            proc.AddPara("@ShowIsNewQuotationNumberManual", ShowIsNewQuotationNumberManual);
                            proc.AddPara("@ShowIsPendingCollectionRequiredUnderTeam", ShowIsPendingCollectionRequiredUnderTeam);
                            proc.AddPara("@ShowIsprojectforCustomer", ShowIsprojectforCustomer);
                            proc.AddPara("@ShowIsRateEnabledforNewOrderScreenwithSize", ShowIsRateEnabledforNewOrderScreenwithSize);
                            proc.AddPara("@ShowIsRestrictNearbyGeofence", ShowIsRestrictNearbyGeofence);
                            proc.AddPara("@ShowIsReturnEnableforParty", ShowIsReturnEnableforParty);
                            proc.AddPara("@ShowIsShowHomeLocationMap", ShowIsShowHomeLocationMap);
                            proc.AddPara("@ShowIsShowManualPhotoRegnInApp", ShowIsShowManualPhotoRegnInApp);
                            proc.AddPara("@ShowIsShowMyDetails", ShowIsShowMyDetails);
                            proc.AddPara("@ShowIsShowNearByTeam", ShowIsShowNearByTeam);
                            proc.AddPara("@ShowIsShowRepeatOrderinNotification", ShowIsShowRepeatOrderinNotification);
                            proc.AddPara("@ShowIsShowRepeatOrdersNotificationinTeam", ShowIsShowRepeatOrdersNotificationinTeam);
                            proc.AddPara("@ShowIsShowRevisitRemarksPopup", ShowIsShowRevisitRemarksPopup);
                            proc.AddPara("@ShowIsShowTypeInRegistration", ShowIsShowTypeInRegistration);
                            proc.AddPara("@ShowIsTeamAttendance", ShowIsTeamAttendance);
                            proc.AddPara("@ShowIsTeamAttenWithoutPhoto", ShowIsTeamAttenWithoutPhoto);
                            proc.AddPara("@ShowIsWhatsappNoForCustomer", ShowIsWhatsappNoForCustomer);
                            proc.AddPara("@ShowLeaveapprovalfromsupervisor", ShowLeaveapprovalfromsupervisor);
                            proc.AddPara("@ShowLeaveapprovalfromsupervisorinteam", ShowLeaveapprovalfromsupervisorinteam);
                            proc.AddPara("@ShowLogoutWithLogFile", ShowLogoutWithLogFile);
                            proc.AddPara("@ShowMarkAttendNotification", ShowMarkAttendNotification);
                            proc.AddPara("@ShowPartyUpdateAddrMandatory", ShowPartyUpdateAddrMandatory);
                            proc.AddPara("@ShowPowerSaverSetting", ShowPowerSaverSetting);
                            proc.AddPara("@ShowShopScreenAftVisitRevisit", ShowShopScreenAftVisitRevisit);
                            proc.AddPara("@Show_App_Logout_Notification", Show_App_Logout_Notification);
                            proc.AddPara("@ShowAmountNewQuotation", ShowAmountNewQuotation);
                            proc.AddPara("@ShowAutoRevisitInAppMenu", ShowAutoRevisitInAppMenu);
                            proc.AddPara("@ShowAutoRevisitInDashboard", ShowAutoRevisitInDashboard);
                            proc.AddPara("@ShowCollectionAlert", ShowCollectionAlert);
                            proc.AddPara("@ShowCollectionOnlywithInvoiceDetails", ShowCollectionOnlywithInvoiceDetails);
                            proc.AddPara("@ShowPurposeInShopVisit", ShowPurposeInShopVisit);
                            proc.AddPara("@ShowQuantityNewQuotation", ShowQuantityNewQuotation);
                            proc.AddPara("@ShowTotalVisitAppMenu", ShowTotalVisitAppMenu);
                            proc.AddPara("@ShowUserwiseLeadMenu", ShowUserwiseLeadMenu);
                            proc.AddPara("@ShowZeroCollectioninAlert", ShowZeroCollectioninAlert);
                            proc.AddPara("@ShowUpdateOtherID", ShowUpdateOtherID);
                            proc.AddPara("@ShowUpdateUserID", ShowUpdateUserID);
                            proc.AddPara("@ShowUpdateUserName", ShowUpdateUserName);
                            proc.AddPara("@ShowWillRoomDBShareinLogin", ShowWillRoomDBShareinLogin);
                            // End of Mantis Issue 25207
                            // Rev 1.0
                            proc.AddPara("@IsShowEmployeePerformance", IsShowEmployeePerformance);
                            // End of Rev 1.0
                            // Rev 2.0
                            proc.AddPara("@IsShowBeatInMenu", IsShowBeatInMenu);
                            // End of Rev 2.0
                            // Rev 3.0
                            proc.AddPara("@IsShowWorkType", IsShowWorkType);
                            proc.AddPara("@IsShowMarketSpendTimer", IsShowMarketSpendTimer);
                            proc.AddPara("@IsShowUploadImageInAppProfile", IsShowUploadImageInAppProfile);
                            proc.AddPara("@IsShowCalendar", IsShowCalendar);
                            proc.AddPara("@IsShowCalculator", IsShowCalculator);
                            proc.AddPara("@IsShowInactiveCustomer", IsShowInactiveCustomer);
                            proc.AddPara("@IsShowAttendanceSummary", IsShowAttendanceSummary);
                            // End of Rev 3.0
                            // Rev 4.0
                            proc.AddPara("@IsMenuShowAIMarketAssistant", IsMenuShowAIMarketAssistant);
                            proc.AddPara("@IsUsbDebuggingRestricted", IsUsbDebuggingRestricted);
                            // End of Rev 4.0
                            // Rev 5.0
                            proc.AddPara("@IsShowLatLongInOutletMaster", IsShowLatLongInOutletMaster);
                            // End of Rev 5.0
                            // Rev 6.0
                            proc.AddPara("@IsCallLogHistoryActivated", IsCallLogHistoryActivated);
                            // End of Rev 6.0
                            // Rev 7.0
                            proc.AddPara("@IsShowMenuCRMContacts", IsShowMenuCRMContacts);
                            proc.AddPara("@IsCheckBatteryOptimization", IsCheckBatteryOptimization);
                            // End of Rev 7.0
                            // Rev 8.0
                            proc.AddPara("@ShowUserwisePartyWithGeoFence", ShowUserwisePartyWithGeoFence);
                            proc.AddPara("@ShowUserwisePartyWithCreateOrder", ShowUserwisePartyWithCreateOrder);
                            // End of Rev 8.0
                            // Rev 9.0
                            proc.AddPara("@AdditionalinfoRequiredforContactListing", AdditionalinfoRequiredforContactListing);
                            proc.AddPara("@AdditionalinfoRequiredforContactAdd", AdditionalinfoRequiredforContactAdd);
                            proc.AddPara("@ContactAddresswithGeofence", ContactAddresswithGeofence);
                            // End of Rev 9.0
                            // Rev 10.0
                            proc.AddPara("@IsCRMPhonebookSyncEnable", IsCRMPhonebookSyncEnable);
                            proc.AddPara("@IsCRMSchedulerEnable", IsCRMSchedulerEnable);
                            proc.AddPara("@IsCRMAddEnable", IsCRMAddEnable);
                            proc.AddPara("@IsCRMEditEnable", IsCRMEditEnable);
                            // End of Rev 10.0
                            // Rev 11.0
                            proc.AddPara("@IsShowAddressInParty", IsShowAddressInParty);
                            proc.AddPara("@IsShowUpdateInvoiceDetails", IsShowUpdateInvoiceDetails);
                            // End of Rev 11.0
                            // Rev 12.0
                            proc.AddPara("@IsSpecialPriceWithEmployee", IsSpecialPriceWithEmployee);
                            // End of Rev 12.0
                            // Rev 13.0
                            proc.AddPara("@IsShowCRMOpportunity", IsShowCRMOpportunity);
                            proc.AddPara("@IsEditEnableforOpportunity", IsEditEnableforOpportunity);
                            proc.AddPara("@IsDeleteEnableforOpportunity", IsDeleteEnableforOpportunity);
                            // End of Rev 13.0
                            // Rev 14.0
                            proc.AddPara("@IsShowDateWiseOrderInApp", IsShowDateWiseOrderInApp);
                            // End of Rev 14.0
                            // Rev 16.0
                            proc.AddPara("@IsUserWiseLMSEnable", IsUserWiseLMSEnable);
                            proc.AddPara("@IsUserWiseLMSFeatureOnly", IsUserWiseLMSFeatureOnly);
                            // End of Rev 16.0
                            // Rev 17.0
                            proc.AddPara("@IsUserWiseRecordAudioEnableForVisitRevisit", IsUserWiseRecordAudioEnableForVisitRevisit);
                            // End of Rev 17.0 

                            // Rev 18.0
                            proc.AddPara("@ShowClearQuiz", ShowClearQuiz);
                            // End of Rev 18.0 
                            // Rev 19.0
                            proc.AddPara("@IsAllowProductCurrentStockUpdateFromApp", IsAllowProductCurrentStockUpdateFromApp);
                            // End of Rev 19.0
                            // Rev 21.0
                            proc.AddPara("@ShowTargetOnApp", ShowTargetOnApp);
                            // End of Rev 21.0

                            DataTable dt = proc.GetTable();


                            //Start Leave Approver Tanmoy

                            DataSet dsApprove = new DataSet();
                            dsApprove = oDBEngine.PopulateData("ID", "FTS_LEAVE_APPROVER", "APPROVAR_ID='" + userId + "'");

                            if (chkLeaveApprover.Checked == true)
                            {
                                if (dsApprove.Tables["TableName"].Rows.Count == 0)
                                {
                                    oDBEngine.InsertDataFromAnotherTable("FTS_LEAVE_APPROVER ", "APPROVAR_ID", null, "'" + userId + "'", null);
                                }
                            }
                            else
                            {
                                if (dsApprove.Tables["TableName"].Rows.Count > 0)
                                {
                                    oDBEngine.DeleteValue("FTS_LEAVE_APPROVER", "APPROVAR_ID='" + userId + "'");
                                }
                            }
                            //End Leave Approver Tanmoy

                            Fillgridview();
                            //Page.ClientScript.RegisterStartupScript(GetType(), "OnC", "<script language='javascript'> Close(); </script>");
                            Response.Redirect("/OMS/Management/Master/root_user.aspx");
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "OnClick", "<script language='javascript'> alert('Not Authorised To Modify Records!') </script>");
                        }
                    // Mantis Issue 24723
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "OnClick", "<script language='javascript'> alert('Duplicate user id found ! Please Talk to Administrator.') </script>");
                    }
                    // End of Mantis Issue 24723
                    //}
                }
            }
            //else
            //{
            //    Page.ClientScript.RegisterStartupScript(GetType(), "OnClick", "<script language='javascript'> alert('Please select Group') </script>");
            //}
        }

        public static string GetFinancialYear()
        {
            string finyear = "";
            DateTime dt = Convert.ToDateTime(System.DateTime.Now);
            int m = dt.Month;
            int y = dt.Year;
            if (m > 3)
            {
                finyear = y.ToString() + "-" + Convert.ToString((y + 1));
                //get last  two digits (eg: 10 from 2010);
            }
            else
            {
                finyear = Convert.ToString((y - 1)) + "-" + y.ToString();
            }
            return finyear;
        }

        protected string getuserGroup()
        {
            //string str = "";
            //bool flag = true;
            //for (int i = 0; i <= grdUserAccess.Rows.Count - 1; i++)
            //{
            //    CheckBox chk = (CheckBox)grdUserAccess.Rows[i].FindControl("chkSegmentId");
            //    if (chk.Checked == true)
            //    {
            //        DropDownList drp = (DropDownList)grdUserAccess.Rows[i].FindControl("drpUserGroup");
            //        if (flag == true)
            //        {
            //            str += drp.SelectedValue;
            //            flag = false;
            //        }
            //        else
            //        {
            //            str += "," + drp.SelectedValue;
            //        }
            //    }
            //}
            //return str;
            return ddlGroups.SelectedValue.ToString();
        }
        //protected void Page_Unload(object sender, EventArgs e)
        //{
        //    this.Page.ClientScript.RegisterStartupScript(GetType(), "heightL", "<script>alert('height');height();</script>");
        //}



        // Code Added By Sandip on 22032017 to use Query String Value in Web Method for Chosen DropDown

        public static string ActionMode { get; set; }
        /*Rev work start 12.05.2022 Mantise no :24856 In User master username,loginid,password,associate employee make blank in Copy Mode*/
        public static string ModeState { get; set; }
        /*Rev work close 12.05.2022 Mantise no :24856 In User master username,loginid,password,associate employee make blank in Copy Mode*/

        public void UserWiseSetings()
        {
            //REV 18.0
            string strShowClearQuiz = ConfigurationManager.AppSettings["ShowClearQuiz"];
            //REV 18.0 END
            //String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
            DataTable dt = new DataTable();
            //SqlCommand sqlcmd = new SqlCommand();
            //SqlConnection sqlcon = new SqlConnection(con);

            //sqlcon.Open();
            //sqlcmd = new SqlCommand("select [key],[Value] from FTS_APP_CONFIG_SETTINGS where [Key] in ('EnableLeaveonApproval','ActiveAutomaticRevisit','InputDayPlan','ActiveMoreDetailsMandatory','DisplayMoreDetailsWhileNewVisit','ShowMeetingsOption','ShowProductRateInApp','isActivatePJPFeature','FingerPrintAttend','FingerPrintVisit','SelfieAttend','IsShowReport','isAttendanceReportShow','isPerformanceReportShow','isVisitReportShow','willTimesheetShow','isAttendanceFeatureOnly','isOrderShow','isVisitShow','iscollectioninMenuShow','isShopAddEditAvailable','isEntityCodeVisible','isAreaMandatoryInPartyCreation','isShowPartyInAreaWiseTeam','isChangePasswordAllowed','isHomeRestrictAttendance','isQuotationShow','isCustomerFeatureEnable')", sqlcon);
            //SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            //da.Fill(dt);
            //sqlcon.Close();

            ProcedureExecute proc = new ProcedureExecute("PRC_FTSInsertUpdateUser");
            proc.AddPara("@ACTION", "ShowSettings");
            dt = proc.GetTable();

            if (dt.Rows.Count > 0)
            {
                //List<string> obj = new List<string>();
                foreach (DataRow dr in dt.Rows)
                {
                    //obj.Add(Convert.ToString(dr["key"]) + "|" + Convert.ToString(dr["Value"]));

                    if (Convert.ToString(dr["key"]) == "EnableLeaveonApproval")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            DivEnableLeaveonApproval.Style.Add("display", "table-cell");
                            //For leave approver Start Tanmoy
                            DivEnableLeaveonApprover.Style.Add("display", "table-cell");
                            //For leave approver End Tanmoy
                        }
                        else
                        {
                            DivEnableLeaveonApproval.Style.Add("display", "none");
                            //For leave approver Start Tanmoy
                            DivEnableLeaveonApprover.Style.Add("display", "none");
                            //For leave approver End Tanmoy
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "ActiveAutomaticRevisit")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            DivActiveautomaticRevisit.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            DivActiveautomaticRevisit.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "InputDayPlan")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            DivInputDayPlan.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            DivInputDayPlan.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "ActiveMoreDetailsMandatory")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            DivActiveMoreDetailsMandatory.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            DivActiveMoreDetailsMandatory.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "DisplayMoreDetailsWhileNewVisit")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            DivDisplayMoreDetailswhilenewVisit.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            DivDisplayMoreDetailswhilenewVisit.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "ShowMeetingsOption")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            DivShowMeetingsOption.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            DivShowMeetingsOption.Style.Add("display", "none");
                        }
                    }
                    // Rev 3.0
                    //else if (Convert.ToString(dr["key"]) == "ShowProductRateInApp")
                    //{
                    //    if (Convert.ToString(dr["Value"]) == "1")
                    //    {
                    //        DivShowProductRateinApp.Style.Add("display", "table-cell");
                    //    }
                    //    else
                    //    {
                    //        DivShowProductRateinApp.Style.Add("display", "none");
                    //    }
                    //}
                    // End of Rev 3.0
                    else if (Convert.ToString(dr["key"]) == "isActivatePJPFeature")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            //DivShowTeam.Style.Add("display", "table-cell");
                            DivAllowPJPupdateforTeam.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            //DivShowTeam.Style.Add("display", "none");
                            DivAllowPJPupdateforTeam.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsShowReport")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            DivwillReportShow.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            DivwillReportShow.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "FingerPrintAttend")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            DivIsFingerPrintMandatoryForAttendance.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            DivIsFingerPrintMandatoryForAttendance.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "FingerPrintVisit")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            DivIsFingerPrintMandatoryForVisit.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            DivIsFingerPrintMandatoryForVisit.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "SelfieAttend")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            DivIsSelfieMandatoryForAttendance.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            DivIsSelfieMandatoryForAttendance.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isAttendanceReportShow")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisAttendanceReportShow.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisAttendanceReportShow.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isPerformanceReportShow")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisPerformanceReportShow.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisPerformanceReportShow.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isVisitReportShow")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisVisitReportShow.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisVisitReportShow.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "willTimesheetShow")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdwillTimesheetShow.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdwillTimesheetShow.Style.Add("display", "none");
                        }
                    }
                    /*Rev work start 11.05.2022 Mantise No:0024880 Horizontal Performance Summary & detail report required with hierarchy setting*/
                    else if (Convert.ToString(dr["key"]) == "IsHierarchyforHorizontalPerformanceReport")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdHorizontalPerformanceReportShow.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdHorizontalPerformanceReportShow.Style.Add("display", "none");
                        }
                    }
                    /*Rev work close 11.05.2022 Mantise No:0024880 Horizontal Performance Summary & detail report required with hierarchy setting*/
                    else if (Convert.ToString(dr["key"]) == "isAttendanceFeatureOnly")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisAttendanceFeatureOnly.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisAttendanceFeatureOnly.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isOrderShow")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisOrderShow.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisOrderShow.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isVisitShow")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisVisitShow.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisVisitShow.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "iscollectioninMenuShow")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdiscollectioninMenuShow.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdiscollectioninMenuShow.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isShopAddEditAvailable")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisShopAddEditAvailable.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisShopAddEditAvailable.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isEntityCodeVisible")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisEntityCodeVisible.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisEntityCodeVisible.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isAreaMandatoryInPartyCreation")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisAreaMandatoryInPartyCreation.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisAreaMandatoryInPartyCreation.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isShowPartyInAreaWiseTeam")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisShowPartyInAreaWiseTeam.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisShowPartyInAreaWiseTeam.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isChangePasswordAllowed")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisChangePasswordAllowed.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisChangePasswordAllowed.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isHomeRestrictAttendance")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            DivisHomeRestrictAttendance.Style.Add("display", "block");
                        }
                        else
                        {
                            DivisHomeRestrictAttendance.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isQuotationShow")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisQuotationShow.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisQuotationShow.Style.Add("display", "none");
                        }
                    }
                    //New settings
                    else if (Convert.ToString(dr["key"]) == "isQuotationPopupShow")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisQuotationPopupShow.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisQuotationPopupShow.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isAchievementEnable")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisAchievementEnable.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisAchievementEnable.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isTarVsAchvEnable")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisTarVsAchvEnable.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisTarVsAchvEnable.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isOrderReplacedWithTeam")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisOrderReplacedWithTeam.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisOrderReplacedWithTeam.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isMultipleAttendanceSelection")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisMultipleAttendanceSelection.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisMultipleAttendanceSelection.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isOfflineTeam")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisOfflineTeam.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisOfflineTeam.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isDDShowForMeeting")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisDDShowForMeeting.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisDDShowForMeeting.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isDDMandatoryForMeeting")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisDDMandatoryForMeeting.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisDDMandatoryForMeeting.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isAllTeamAvailable")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisAllTeamAvailable.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisAllTeamAvailable.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isRecordAudioEnable")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisRecordAudioEnable.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisRecordAudioEnable.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isNextVisitDateMandatory")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisNextVisitDateMandatory.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisNextVisitDateMandatory.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isShowCurrentLocNotifiaction")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisShowCurrentLocNotifiaction.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisShowCurrentLocNotifiaction.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isUpdateWorkTypeEnable")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisUpdateWorkTypeEnable.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisUpdateWorkTypeEnable.Style.Add("display", "none");
                        }
                    }

                    else if (Convert.ToString(dr["key"]) == "isLeaveEnable")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisLeaveEnable.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisLeaveEnable.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isOrderMailVisible")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisOrderMailVisible.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisOrderMailVisible.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "LateVisitSMS")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdLateVisitSMS.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdLateVisitSMS.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isShopEditEnable")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisShopEditEnable.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisShopEditEnable.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isTaskEnable")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisTaskEnable.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisTaskEnable.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "HierarchywiseTargetSettings")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdHierarchywiseTargetSettings.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdHierarchywiseTargetSettings.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsShowTeamDetails")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            DivShowTeam.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            DivShowTeam.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isAppInfoEnable")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisAppInfoEnable.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisAppInfoEnable.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "willDynamicShow")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdwillDynamicShow.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdwillDynamicShow.Style.Add("display", "none");
                        }
                    }
                    // Rev 3.0
                    //else if (Convert.ToString(dr["key"]) == "IsCRMApplicable")
                    else if (Convert.ToString(dr["key"]) == "willActivityShow")
                        // End of Rev 3.0
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdwillActivityShow.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdwillActivityShow.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isDocumentRepoShow")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisDocumentRepoShow.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisDocumentRepoShow.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isChatBotShow")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisChatBotShow.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisChatBotShow.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isAttendanceBotShow")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisAttendanceBotShow.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisAttendanceBotShow.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isVisitBotShow")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisVisitBotShow.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisVisitBotShow.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isInstrumentCompulsory")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisInstrumentCompulsory.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisInstrumentCompulsory.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isBankCompulsory")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisBankCompulsory.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisBankCompulsory.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isComplementaryUser")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisComplementaryUser.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisComplementaryUser.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isVisitPlanShow")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisVisitPlanShow.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisVisitPlanShow.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isVisitPlanMandatory")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisVisitPlanMandatory.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisVisitPlanMandatory.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isAttendanceDistanceShow")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisAttendanceDistanceShow.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisAttendanceDistanceShow.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "willTimelineWithFixedLocationShow")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdwillTimelineWithFixedLocationShow.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdwillTimelineWithFixedLocationShow.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isShowOrderRemarks")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisShowOrderRemarks.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisShowOrderRemarks.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isShowOrderSignature")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisShowOrderSignature.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisShowOrderSignature.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isShowSmsForParty")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisShowSmsForParty.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisShowSmsForParty.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isShowTimeline")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisShowTimeline.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisShowTimeline.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "willScanVisitingCard")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdwillScanVisitingCard.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdwillScanVisitingCard.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isCreateQrCode")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisCreateQrCode.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisCreateQrCode.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isScanQrForRevisit")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisScanQrForRevisit.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisScanQrForRevisit.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isShowLogoutReason")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisShowLogoutReason.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisShowLogoutReason.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "willShowHomeLocReason")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdwillShowHomeLocReason.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdwillShowHomeLocReason.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "willShowShopVisitReason")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdwillShowShopVisitReason.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdwillShowShopVisitReason.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "willShowPartyStatus")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdwillShowPartyStatus.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdwillShowPartyStatus.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "willShowEntityTypeforShop")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdwillShowEntityTypeforShop.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdwillShowEntityTypeforShop.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isShowRetailerEntity")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisShowRetailerEntity.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisShowRetailerEntity.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isShowDealerForDD")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisShowDealerForDD.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisShowDealerForDD.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isShowBeatGroup")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisShowBeatGroup.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisShowBeatGroup.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isShowShopBeatWise")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisShowShopBeatWise.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisShowShopBeatWise.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isShowOTPVerificationPopup")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisShowOTPVerificationPopup.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisShowOTPVerificationPopup.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isShowMicroLearing")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisShowMicroLearing.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisShowMicroLearing.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isMultipleVisitEnable")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisMultipleVisitEnable.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisMultipleVisitEnable.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isShowVisitRemarks")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisShowVisitRemarks.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisShowVisitRemarks.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isShowNearbyCustomer")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisShowNearbyCustomer.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisShowNearbyCustomer.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isServiceFeatureEnable")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisServiceFeatureEnable.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisServiceFeatureEnable.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isPatientDetailsShowInOrder")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisPatientDetailsShowInOrder.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisPatientDetailsShowInOrder.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isPatientDetailsShowInCollection")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisPatientDetailsShowInCollection.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisPatientDetailsShowInCollection.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isAttachmentMandatory")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisAttachmentMandatory.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisAttachmentMandatory.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isShopImageMandatory")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisShopImageMandatory.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisShopImageMandatory.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "isLogShareinLogin")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdisLogShareinLogin.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdisLogShareinLogin.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsCompetitorenable")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsCompetitorenable.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsCompetitorenable.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsOrderStatusRequired")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsOrderStatusRequired.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsOrderStatusRequired.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsCurrentStockEnable")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsCurrentStockEnable.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsCurrentStockEnable.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsCurrentStockApplicableforAll")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsCurrentStockApplicableforAll.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsCurrentStockApplicableforAll.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IscompetitorStockRequired")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIscompetitorStockRequired.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIscompetitorStockRequired.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsCompetitorStockforParty")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsCompetitorStockforParty.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsCompetitorStockforParty.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsFaceDetectionOn")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdShowFaceRegInMenu.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdShowFaceRegInMenu.Style.Add("display", "none");
                        }
                        // Mantis Issue 24490
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsFaceDetection.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsFaceDetection.Style.Add("display", "none");
                        }

                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsPhotoDeleteShow.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsPhotoDeleteShow.Style.Add("display", "none");
                        }

                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsFaceDetectionWithCaptcha.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsFaceDetectionWithCaptcha.Style.Add("display", "none");
                        }
                        // End of Mantis Issue 24490
                    }
                        // Mantis Issue 24490
                    //else if (Convert.ToString(dr["key"]) == "IsFaceDetectionOn")
                    //{
                    //    if (Convert.ToString(dr["Value"]) == "1")
                    //    {
                    //        TdIsFaceDetection.Style.Add("display", "table-cell");
                    //    }
                    //    else
                    //    {
                    //        TdIsFaceDetection.Style.Add("display", "none");
                    //    }
                    //}
                        // End of Mantis Issue 24490
                    //else if (Convert.ToString(dr["key"]) == "isFaceRegistered")
                    //{
                    //    if (Convert.ToString(dr["Value"]) == "1")
                    //    {
                    //        TdisFaceRegistered.Style.Add("display", "table-cell");
                    //    }
                    //    else
                    //    {
                    //        TdisFaceRegistered.Style.Add("display", "none");
                    //    }
                    //}
                    else if (Convert.ToString(dr["key"]) == "IsUserwiseDistributer")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsUserwiseDistributer.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsUserwiseDistributer.Style.Add("display", "none");
                        }
                    }
                        // Mantis Issue 24490
                    //else if (Convert.ToString(dr["key"]) == "IsFaceDetectionOn")
                    //{
                    //    if (Convert.ToString(dr["Value"]) == "1")
                    //    {
                    //        TdIsPhotoDeleteShow.Style.Add("display", "table-cell");
                    //    }
                    //    else
                    //    {
                    //        TdIsPhotoDeleteShow.Style.Add("display", "none");
                    //    }
                    //}
                    // End of Mantis Issue 24490 
                    //else if (Convert.ToString(dr["key"]) == "IsDMSFeatureOn")
                    else if (Convert.ToString(dr["key"]) == "IsAllDataInPortalwithHeirarchy")
                    // End of Mantis Issue 24362 
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsAllDataInPortalwithHeirarchy.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsAllDataInPortalwithHeirarchy.Style.Add("display", "none");
                        }
                        // Mantis Issue 24490
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsShowDayStart.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsShowDayStart.Style.Add("display", "none");
                        }

                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsshowDayStartSelfie.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsshowDayStartSelfie.Style.Add("display", "none");
                        }

                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsShowDayEnd.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsShowDayEnd.Style.Add("display", "none");
                        }

                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsshowDayEndSelfie.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsshowDayEndSelfie.Style.Add("display", "none");
                        }

                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsShowMarkDistVisitOnDshbrd.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsShowMarkDistVisitOnDshbrd.Style.Add("display", "none");
                        }
                        // End of Mantis Issue 24490
                    }
                        // Mantis Issue 24490
                    //else if (Convert.ToString(dr["key"]) == "IsFaceDetectionOn")
                    //{
                    //    if (Convert.ToString(dr["Value"]) == "1")
                    //    {
                    //        TdIsFaceDetectionWithCaptcha.Style.Add("display", "table-cell");
                    //    }
                    //    else
                    //    {
                    //        TdIsFaceDetectionWithCaptcha.Style.Add("display", "none");
                    //    }
                    //}
                    // End of Mantis Issue 24490


                    else if (Convert.ToString(dr["key"]) == "IsShowMenuAddAttendance")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsShowMenuAddAttendance.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsShowMenuAddAttendance.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsShowMenuAttendance")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsShowMenuAttendance.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsShowMenuAttendance.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsShowMenuShops")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsShowMenuShops.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsShowMenuShops.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsShowMenuOutstandingDetailsPPDD")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsShowMenuOutstandingDetailsPPDD.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsShowMenuOutstandingDetailsPPDD.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsShowMenuStockDetailsPPDD")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsShowMenuStockDetailsPPDD.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsShowMenuStockDetailsPPDD.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsShowMenuTA")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsShowMenuTA.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsShowMenuTA.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsShowMenuMISReport")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsShowMenuMISReport.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsShowMenuMISReport.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsShowMenuReimbursement")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsShowMenuReimbursement.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsShowMenuReimbursement.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsShowMenuAchievement")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsShowMenuAchievement.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsShowMenuAchievement.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsShowMenuMapView")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsShowMenuMapView.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsShowMenuMapView.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsShowMenuShareLocation")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsShowMenuShareLocation.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsShowMenuShareLocation.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsShowMenuHomeLocation")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsShowMenuHomeLocation.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsShowMenuHomeLocation.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsShowMenuWeatherDetails")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsShowMenuWeatherDetails.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsShowMenuWeatherDetails.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsShowMenuChat")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsShowMenuChat.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsShowMenuChat.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsShowMenuScanQRCode")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsShowMenuScanQRCode.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsShowMenuScanQRCode.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsShowMenuPermissionInfo")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsShowMenuPermissionInfo.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsShowMenuPermissionInfo.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsShowMenuAnyDesk")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsShowMenuAnyDesk.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsShowMenuAnyDesk.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsDocRepoFromPortal")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsDocRepoFromPortal.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsDocRepoFromPortal.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsDocRepShareDownloadAllowed")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsDocRepShareDownloadAllowed.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsDocRepShareDownloadAllowed.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsScreenRecorderEnable")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsScreenRecorderEnable.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsScreenRecorderEnable.Style.Add("display", "none");
                        }

                        // Mantis Issue 24490
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsShowAttendanceOnAppDashboard.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsShowAttendanceOnAppDashboard.Style.Add("display", "none");
                        }
                        // End of Mantis Issue 24490
                    }
                    //Rev Add new Settings
                    else if (Convert.ToString(dr["key"]) == "IsShowPartyOnAppDashboard")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsShowPartyOnAppDashboard.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsShowPartyOnAppDashboard.Style.Add("display", "none");
                        }
                    }
                        // Mantis Issue 24490
                    //else if (Convert.ToString(dr["key"]) == "IsScreenRecorderEnable")
                    //{
                    //    if (Convert.ToString(dr["Value"]) == "1")
                    //    {
                    //        TdIsShowAttendanceOnAppDashboard.Style.Add("display", "table-cell");
                    //    }
                    //    else
                    //    {
                    //        TdIsShowAttendanceOnAppDashboard.Style.Add("display", "none");
                    //    }
                    //}
                        // End of Mantis Issue 24490
                    else if (Convert.ToString(dr["key"]) == "IsShowTotalVisitsOnAppDashboard")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsShowTotalVisitsOnAppDashboard.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsShowTotalVisitsOnAppDashboard.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsShowVisitDurationOnAppDashboard")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsShowVisitDurationOnAppDashboard.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsShowVisitDurationOnAppDashboard.Style.Add("display", "none");
                        }
                    }
                    // Mantis Issue 24362
                    //else if (Convert.ToString(dr["key"]) == "IsDMSFeatureOn")
                    // Mantis Issue 24490
                    //else if (Convert.ToString(dr["key"]) == "IsAllDataInPortalwithHeirarchy")
                    //// End of Mantis Issue 24362
                    //{
                    //    if (Convert.ToString(dr["Value"]) == "1")
                    //    {
                    //        TdIsShowDayStart.Style.Add("display", "table-cell");
                    //    }
                    //    else
                    //    {
                    //        TdIsShowDayStart.Style.Add("display", "none");
                    //    }
                    //}
                    //    // Mantis Issue 24362
                    ////else if (Convert.ToString(dr["key"]) == "IsDMSFeatureOn")
                    //else if (Convert.ToString(dr["key"]) == "IsAllDataInPortalwithHeirarchy")
                    //    // End of Mantis Issue 24362
                    //{
                    //    if (Convert.ToString(dr["Value"]) == "1")
                    //    {
                    //        TdIsshowDayStartSelfie.Style.Add("display", "table-cell");
                    //    }
                    //    else
                    //    {
                    //        TdIsshowDayStartSelfie.Style.Add("display", "none");
                    //    }
                    //}
                    //    // Mantis Issue 24362
                    ////else if (Convert.ToString(dr["key"]) == "IsDMSFeatureOn")
                    //else if (Convert.ToString(dr["key"]) == "IsAllDataInPortalwithHeirarchy")
                    //    // End of Mantis Issue 24362
                    //{
                    //    if (Convert.ToString(dr["Value"]) == "1")
                    //    {
                    //        TdIsShowDayEnd.Style.Add("display", "table-cell");
                    //    }
                    //    else
                    //    {
                    //        TdIsShowDayEnd.Style.Add("display", "none");
                    //    }
                    //}
                    //    // Mantis Issue 24362
                    ////else if (Convert.ToString(dr["key"]) == "IsDMSFeatureOn")
                    //else if (Convert.ToString(dr["key"]) == "IsAllDataInPortalwithHeirarchy")
                    //    // End of Mantis Issue 24362
                    //{
                    //    if (Convert.ToString(dr["Value"]) == "1")
                    //    {
                    //        TdIsshowDayEndSelfie.Style.Add("display", "table-cell");
                    //    }
                    //    else
                    //    {
                    //        TdIsshowDayEndSelfie.Style.Add("display", "none");
                    //    }
                    //}
                        // End of Mantis Issue 24490
                    else if (Convert.ToString(dr["key"]) == "IsShowLeaveInAttendance")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsShowLeaveInAttendance.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsShowLeaveInAttendance.Style.Add("display", "none");
                        }
                    }
                    
                    else if (Convert.ToString(dr["key"]) == "IsLeaveGPSTrack")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsLeaveGPSTrack.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsLeaveGPSTrack.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsShowActivitiesInTeam")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsShowActivitiesInTeam.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsShowActivitiesInTeam.Style.Add("display", "none");
                        }
                    }
                     // Mantis Issue 24490
                    //    // Mantis Issue 24362
                    ////else if (Convert.ToString(dr["key"]) == "IsDMSFeatureOn")
                    //else if (Convert.ToString(dr["key"]) == "IsAllDataInPortalwithHeirarchy")
                    //    // End of Mantis Issue 24362
                    //{
                    //    if (Convert.ToString(dr["Value"]) == "1")
                    //    {
                    //        TdIsShowMarkDistVisitOnDshbrd.Style.Add("display", "table-cell");
                    //    }
                    //    else
                    //    {
                    //        TdIsShowMarkDistVisitOnDshbrd.Style.Add("display", "none");
                    //    }
                    //}
                        // End of Mantis Issue 24490
                    //End of Rev Add new setting
                    //Mantis Issue 24408,24364
                    else if (Convert.ToString(dr["key"]) == "IsRevisitRemarksMandatory")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsRevisitRemarksMandatory.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdIsRevisitRemarksMandatory.Style.Add("display", "none");
                            chkIsRevisitRemarksMandatory.Checked = false;
                           
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "GPSAlert")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdIsGPSAlert.Style.Add("display", "table-cell");
                            if (chkGPSAlert.Checked == true)
                            {
                                TdIsGPSAlertwithSound.Style.Add("display", "table-cell");
                            }
                            else
                            {
                                TdIsGPSAlertwithSound.Style.Add("display", "none");
                                chkGPSAlertwithSound.Checked = false;
                            }
                        }
                        else
                        {
                            TdIsGPSAlert.Style.Add("display", "none");
                            TdIsGPSAlertwithSound.Style.Add("display", "none");
                            chkGPSAlert.Checked = false;
                            chkGPSAlertwithSound.Checked = false;
                        }
                    }
                    //else if (Convert.ToString(dr["key"]) == "GPSAlert" && chkGPSAlert.Checked == true)
                    //{
                    //    if (Convert.ToString(dr["Value"]) == "1")
                    //    {
                    //        TdIsGPSAlertwithSound.Style.Add("display", "table-cell");
                    //    }
                    //    else
                    //    {
                    //        TdIsGPSAlertwithSound.Style.Add("display", "none");
                    //        chkGPSAlertwithSound.Checked = false;
                    //    }
                    //}
                    //End of Mantis Issue 24408,24364
                    // Mantis Issue 24596,24597
                    else if (Convert.ToString(dr["key"]) == "FaceRegistrationFrontCamera")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdFaceRegistrationFrontCamera.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdFaceRegistrationFrontCamera.Style.Add("display", "none");
                            chkFaceRegistrationFrontCamera.Checked = false;

                        }
                    }

                    else if (Convert.ToString(dr["key"]) == "MRPInOrder")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            TdMRPInOrder.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            TdMRPInOrder.Style.Add("display", "none");
                            chkMRPInOrder.Checked = false;

                        }
                    }
                    // End of Mantis Issue 24596,24597
                    //Mantis Issue 25015,25016
                    else if (Convert.ToString(dr["key"]) == "IsUserTypeMandatory")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            IsUserTypeMandatory.Value = "1";
                        }
                        else
                        {
                            IsUserTypeMandatory.Value = "0";
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsShowUserType")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            IsShowUserType.Value = "1";
                        }
                        else
                        {
                            divType.Style.Add("display", "none");
                            IsShowUserType.Value = "0";
                            IsUserTypeMandatory.Value = "0";
                        }
                    }
                    //End of Mantis Issue 25015,25016
                    // Rev 1.0
                    else if (Convert.ToString(dr["key"]) == "IsShowEmployeePerformance")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            divShowEmployeePerformance.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            divShowEmployeePerformance.Style.Add("display", "none");
                            chkShowEmployeePerformance.Checked = false;

                        }
                    }
                    // End of Rev 1.0
                    // Rev 3.0
                    else if (Convert.ToString(dr["key"]) == "IsShowWorkType")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            divShowWorkType.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            divShowWorkType.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsShowMarketSpendTimer")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            divShowMarketSpendTimer.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            divShowMarketSpendTimer.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsShowUploadImageInAppProfile")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            divShowUploadImageInAppProfile.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            divShowUploadImageInAppProfile.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsShowCalendar")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            divShowCalendar.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            divShowCalendar.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsShowCalculator")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            divShowCalculator.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            divShowCalculator.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsShowInactiveCustomer")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            divShowInactiveCustomer.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            divShowInactiveCustomer.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsShowAttendanceSummary")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            divShowAttendanceSummary.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            divShowAttendanceSummary.Style.Add("display", "none");
                        }
                    }
                    // End of Rev 3.0
                    // Rev 6.0
                    else if (Convert.ToString(dr["key"]) == "IsCallLogHistoryActivated")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            divIsCallLogHistoryActivated.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            divIsCallLogHistoryActivated.Style.Add("display", "none");
                        }
                    }
                    // End of Rev 6.0
                    // Rev 7.0
                    else if (Convert.ToString(dr["key"]) == "IsShowMenuCRMContacts")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            divIsShowMenuCRMContacts.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            divIsShowMenuCRMContacts.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsCheckBatteryOptimization")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            divIsCheckBatteryOptimization.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            divIsCheckBatteryOptimization.Style.Add("display", "none");
                        }
                    }
                    // End of Rev 7.0
                    //Rev 8.0
                    else if (Convert.ToString(dr["key"]) == "ShowPartyWithCreateOrder")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            divShowPartyWithCreateOrder.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            divShowPartyWithCreateOrder.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "divShowPartyWithGeoFence")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            divShowPartyWithGeoFence.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            divShowPartyWithGeoFence.Style.Add("display", "none");
                        }
                    }
                    //Rev 8.0 End
                    // Rev 9.0
                    else if (Convert.ToString(dr["key"]) == "AdditionalinfoRequiredforContactListing")
                    {
                        // Rev 15.0
                        //chkAdditionalinfoRequiredforContactListing.Checked = true;
                        // End of Rev 15.0

                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            divAdditionalinfoRequiredforContactListing.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            divAdditionalinfoRequiredforContactListing.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "AdditionalinfoRequiredforContactAdd")
                    {
                        // Rev 15.0
                        //chkAdditionalinfoRequiredforContactAdd.Checked = true;
                        // End of Rev 15.0

                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            divAdditionalinfoRequiredforContactAdd.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            divAdditionalinfoRequiredforContactAdd.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "ContactAddresswithGeofence")
                    {
                        //Rev 20.0
                        //chkContactAddresswithGeofence.Checked = true;
                        //Rev 20.0 End
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            divContactAddresswithGeofence.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            divContactAddresswithGeofence.Style.Add("display", "none");
                        }
                    }
                    // End of Rev 9.0
                    // Rev 10.0
                    else if (Convert.ToString(dr["key"]) == "IsCRMPhonebookSyncEnable")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            divIsCRMPhonebookSyncEnable.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            divIsCRMPhonebookSyncEnable.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsCRMSchedulerEnable")
                    { 
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            divIsCRMSchedulerEnable.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            divIsCRMSchedulerEnable.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsCRMAddEnable")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            divIsCRMAddEnable.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            divIsCRMAddEnable.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsCRMEditEnable")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            divIsCRMEditEnable.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            divIsCRMEditEnable.Style.Add("display", "none");
                        }
                    }
                    // Rev 10.0 End
                    // Rev 11.0
                    else if (Convert.ToString(dr["key"]) == "IsShowAddressInParty")
                    {
                        // Rev 15.0
                        //chkIsShowAddressInParty.Checked = true;
                        // End of Rev 15.0

                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            divIsShowAddressInParty.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            divIsShowAddressInParty.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsShowUpdateInvoiceDetails")
                    {
                        //chkIsShowUpdateInvoiceDetails.Checked = true;

                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            divIsShowUpdateInvoiceDetails.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            divIsShowUpdateInvoiceDetails.Style.Add("display", "none");
                        }
                    }
                    // End of Rev 11.0
                    // Rev 12.0
                    else if (Convert.ToString(dr["key"]) == "IsSpecialPriceWithEmployee")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            divIsSpecialPriceWithEmployee.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            divIsSpecialPriceWithEmployee.Style.Add("display", "none");
                        }
                    }
                    // End of Rev 12.0
                    // Rev 13.0
                    else if (Convert.ToString(dr["key"]) == "IsShowCRMOpportunity")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            divIsShowCRMOpportunity.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            divIsShowCRMOpportunity.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsEditEnableforOpportunity")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            divIsEditEnableforOpportunity.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            divIsEditEnableforOpportunity.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsSpecialPriceWithEmployee")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            divIsSpecialPriceWithEmployee.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            divIsSpecialPriceWithEmployee.Style.Add("display", "none");
                        }
                    }
                    // End of Rev 13.0
                    // Rev 14.0
                    else if (Convert.ToString(dr["key"]) == "IsShowDateWiseOrderInApp")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            divIsShowDateWiseOrderInApp.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            divIsShowDateWiseOrderInApp.Style.Add("display", "none");
                        }
                    }
                    // End of Rev 14.0
                    // Rev 16.0
                    else if (Convert.ToString(dr["key"]) == "IsUserWiseLMSEnable")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            divIsUserWiseLMSEnable.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            divIsUserWiseLMSEnable.Style.Add("display", "none");
                        }
                    }
                    else if (Convert.ToString(dr["key"]) == "IsUserWiseLMSFeatureOnly")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            divIsUserWiseLMSFeatureOnly.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            divIsUserWiseLMSFeatureOnly.Style.Add("display", "none");
                        }
                    }
                    // End of Rev 16.0
                    // Rev 17.0
                    else if (Convert.ToString(dr["key"]) == "isRecordAudioEnableForVisitRevisit")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            divIsUserWiseRecordAudioEnableForVisitRevisit.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            divIsUserWiseRecordAudioEnableForVisitRevisit.Style.Add("display", "none");
                        }
                    }
                    // End of Rev 17.0
                    // Rev 19.0
                    else if (Convert.ToString(dr["key"]) == "IsAllowProductCurrentStockUpdateFromApp")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            divIsAllowProductCurrentStockUpdateFromApp.Style.Add("display", "table-cell");
                        }
                        else
                        {
                            divIsAllowProductCurrentStockUpdateFromApp.Style.Add("display", "none");
                        }
                    }
                    // End of Rev 19.0
                    

                }
            }


            // Rev 18.0
            if (strShowClearQuiz == "YES")
            {
                DivShowClearQuiz.Style.Add("display", "table-cell");                
            }
            else
            {
                DivShowClearQuiz.Style.Add("display", "none");
            }
            // End of Rev 18.0
        }
    }
}
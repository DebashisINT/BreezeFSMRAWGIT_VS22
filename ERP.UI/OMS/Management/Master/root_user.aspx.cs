/******************************************************************************************************
 * Rev 1.0      Sanchita    07/02/2023      V2.0.36     FSM Employee & User Master - To implement Show button. refer: 25641
 * Rev 2.0      Sanchita     15/02/2023      V2.0.39     A setting required for Employee and User Master module in FSM Portal. 
 * Rev 3.0      Sanchita    08/01/2023      V2.0.45     Attendance/Leave clear option not working properly in the Portal. mantis: 27114    
 *******************************************************************************************************/
using System;
using System.Web;
using System.Web.UI;
using BusinessLogicLayer;
using System.Configuration;
using System.Web.Services;
using DataAccessLayer;
using System.Data;
using ClsDropDownlistNameSpace;
using DevExpress.Web;
using System.Collections.Generic;
using UtilityLayer;
using ERP.Models;
// Rev 1.0
using System.Linq;
// End of Rev 1.0

namespace ERP.OMS.Management.Master
{
    public partial class management_master_root_user : ERP.OMS.ViewState_class.VSPage
    {
        public string pageAccess = "";
        //DBEngine oDBEngine = new DBEngine(string.Empty);

        BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(string.Empty);
        public EntityLayer.CommonELS.UserRightsForPage rights = new EntityLayer.CommonELS.UserRightsForPage();
        clsDropDownList oclsDropDownList = new clsDropDownList();

        public bool IsFaceDetectionOn = false;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Code  Added and Commented By Priti on 20122016 to use Covert.Tostring() instead of Tostring()
                //'http://localhost:2957/InfluxCRM/management/testProjectMainPage_employee.aspx'
                //string sPath = HttpContext.Current.Request.Url.ToString();
                string sPath = Convert.ToString(HttpContext.Current.Request.Url);
                oDBEngine.Call_CheckPageaccessebility(sPath);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/management/master/root_user.aspx");
            //------- For Read Only User in SQL Datasource Connection String   Start-----------------

            if (HttpContext.Current.Session["EntryProfileType"] != null)
            {
                if (Convert.ToString(HttpContext.Current.Session["EntryProfileType"]) == "R")
                {
                    RootUserDataSource.ConnectionString = ConfigurationSettings.AppSettings["DBReadOnlyConnection"];
                }
                else
                {
                    RootUserDataSource.ConnectionString = ConfigurationSettings.AppSettings["DBConnectionDefault"];
                }
            }

            //------- For Read Only User in SQL Datasource Connection String   End-----------------


            if (!IsPostBack)
            {
                userGrid.SettingsCookies.CookiesID = "BreeezeErpGridCookiesroot_useruserGrid";

                this.Page.ClientScript.RegisterStartupScript(GetType(), "setCookieOnStorage", "<script>addCookiesKeyOnStorage('BreeezeErpGridCookiesroot_useruserGrid');</script>");

                Session["addedituser"] = "";
                Session["PartyListTable"] = null;
                Session["UsersListTable"] = null;
                Session["exportval"] = null;

                FillComboPartyType();
                //Rev Start Column Name Change Tanmoy
                //DataTable DataSettings = oDBEngine.GetDataTable("select IsFaceDetectionOn from tbl_master_user where USER_ID=" + Convert.ToString(HttpContext.Current.Session["userid"]));
                //hdnIsFaceDetectionOn.Value = Convert.ToString(DataSettings.Rows[0]["IsFaceDetectionOn"]);
                //if (hdnIsFaceDetectionOn.Value == "True")
                //{
                //    IsFaceDetectionOn = true;
                //}

                DataTable DataSettings = oDBEngine.GetDataTable("select ShowFaceRegInMenu from tbl_master_user where USER_ID=" + Convert.ToString(HttpContext.Current.Session["userid"]));
                hdnIsFaceDetectionOn.Value = Convert.ToString(DataSettings.Rows[0]["ShowFaceRegInMenu"]);
                if (hdnIsFaceDetectionOn.Value == "True")
                {
                    IsFaceDetectionOn = true;
                }
                //End of Rev Column name change

                // Rev 2.0
                string IsShowEmpAndUserSearchInMaster = "0";
                DBEngine obj1 = new DBEngine();
                IsShowEmpAndUserSearchInMaster = Convert.ToString(obj1.GetDataTable("select [value] from FTS_APP_CONFIG_SETTINGS WHERE [Key]='IsShowEmpAndUserSearchInMaster'").Rows[0][0]);

                if (IsShowEmpAndUserSearchInMaster == "1")
                {
                    divUser.Visible = true;
                }
                else
                {
                    divUser.Visible = false;
                }
                // End of Rev 2.0
            }


            //commented by Jitendra on 21-03-2017
            // RootUserDataSource.SelectCommand = "SELECT distinct user_id,user_name,user_loginId,case when  (user_inactive ='Y') then 'Inactive' else 'Active' end as Status,user_status as Onlinestatus, (select deg_designation from tbl_master_designation where deg_id =emp_Designation) as designation FROM [tbl_master_user],tbl_trans_employeeCTC where emp_cntId=user_contactId  and user_branchId in (" + HttpContext.Current.Session["userbranchHierarchy"] + ")";

            // RootUserDataSource.SelectCommand = "SELECT  distinct user_id,user_name,user_loginId,case when  (user_inactive ='Y') then 'Inactive' else 'Active' end as Status,user_status as Onlinestatus, (select top 1  deg_designation from tbl_master_designation where deg_id in (select top 1 emp_Designation from tbl_trans_employeeCTC where emp_CntId= user_contactId order by emp_id desc )) as designation FROM [tbl_master_user],tbl_master_employee where emp_ContactId=user_contactId  and user_branchId in (" + HttpContext.Current.Session["userbranchHierarchy"] + ")";


            //RootUserDataSource.SelectCommand = "SELECT  distinct user_id,user_name,user_loginId,case when  (user_inactive ='Y') then 'Inactive' else 'Active' end as Status,case when  (user_maclock ='Y') then 'Mac Restriction' else 'Mac Open' end as StatusMac,user_status as Onlinestatus, " +
            //   " (select top 1  deg_designation from tbl_master_designation where deg_id in (select top 1 emp_Designation from tbl_trans_employeeCTC where emp_CntId= user_contactId order by emp_id desc )) as designation,"+
            //    " isnull(cnt_firstName,'')+' '+isnull(cnt_middleName,'')+' '+isnull(cnt_lastName,'') as 'AssignedUser', (select branch_description from tbl_master_branch where branch_id=tbl_master_contact.cnt_branchid) as BranchName,grp_name " +
            //    "FROM [tbl_master_user],tbl_master_employee,tbl_master_contact,tbl_master_usergroup  where emp_ContactId=user_contactId  and cnt_InternalId=user_contactId  "+
            //" and user_group=grp_id  and user_branchId in (" + HttpContext.Current.Session["userbranchHierarchy"] + ")";

            // Rev 1.0
            //userGrid.DataSource = BindUserList();
            //userGrid.DataBind();
            // Rev 1.0

        }
        public void bindexport(int Filter)
        {
            //Code  Added and Commented By Priti on 20122016 to use Export Header,date
            // userGrid.Columns[5].Visible = false;
            // Rev 1.0 [ Branch now showing in Export ]
            //userGrid.Columns[6].Visible = false;
            // End of Rev 1.0
            string filename = "Users";
            exporter.FileName = filename;

            exporter.PageHeader.Left = "Users";
            exporter.PageFooter.Center = "[Page # of Pages #]";
            exporter.PageFooter.Right = "[Date Printed]";
            switch (Filter)
            {
                case 1:
                    exporter.WritePdfToResponse();
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
        protected void cmbExport_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Code  Added and Commented By Priti on 20122016 to use Covert.Tostring() instead of Tostring()
            //Int32 Filter = int.Parse(drdExport.SelectedItem.Value.ToString());
            Int32 Filter = int.Parse(Convert.ToString(drdExport.SelectedItem.Value));
            //Code  Added and Commented By Priti on 20122016 to use Export Header,date
            if (Filter != 0)
            {
                if (Session["exportval"] == null)
                {
                    Session["exportval"] = Filter;
                    bindexport(Filter);
                }
                else if (Convert.ToInt32(Session["exportval"]) != Filter)
                {
                    Session["exportval"] = Filter;
                    bindexport(Filter);
                }
            }

        }
        protected void userGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            // Rev 1.0
            string WhichCall = e.Parameters.Split('~')[0];
            if (WhichCall == "Show")
            {
                userGrid.DataSource = BindUserList();
                userGrid.DataBind();
            }
            else
            {
                // End of Rev 1.0
                // Code  Added and Commented By Priti on 20122016 to use Covert.Tostring() instead of Tostring()
                ////RootUserDataSource.SelectCommand = "SELECT user_id,user_name,user_loginId,case when  (emp_effectiveuntil is null or emp_effectiveuntil='1900-01-01 00:00:00.000') then 'Active' else 'Deactive' end as Status, (select deg_designation from tbl_master_designation where deg_id =emp_Designation) as designation FROM [tbl_master_user],tbl_trans_employeeCTC where emp_cntId=user_contactId and user_branchId in (" + HttpContext.Current.Session["userbranchHierarchy"] + ")";
                //if (Session["addedituser"].ToString() == "yes")
                if (Convert.ToString(Session["addedituser"]) == "yes")
                {
                    Session["addedituser"] = "";
                    if (userGrid.FilterExpression == "")
                    {
                        // RootUserDataSource.SelectCommand = "SELECT distinct user_id,user_name,user_loginId,case when  (user_inactive ='Y') then 'Inactive' else 'Active' end as Status,user_status as Onlinestatus, (select deg_designation from tbl_master_designation where deg_id =emp_Designation) as designation FROM [tbl_master_user],tbl_trans_employeeCTC where emp_cntId=user_contactId and user_branchId in (" + HttpContext.Current.Session["userbranchHierarchy"] + ")";
                        RootUserDataSource.SelectCommand = "SELECT  distinct user_id,user_name,user_loginId,case when  (user_inactive ='Y') then 'Inactive' else 'Active' end as Status,case when  (user_maclock ='Y') then 'Mac Restriction' else 'Mac Open' end as StatusMac,user_status as Onlinestatus, (select top 1  deg_designation from tbl_master_designation where deg_id in (select top 1 emp_Designation from tbl_trans_employeeCTC where emp_CntId= user_contactId order by emp_id desc )) as designation FROM [tbl_master_user],tbl_master_employee where emp_ContactId=user_contactId  and user_branchId in (" + HttpContext.Current.Session["userbranchHierarchy"] + ")";

                        userGrid.DataBind();
                    }
                    else
                    {
                        RootUserDataSource.SelectCommand = "SELECT  distinct user_id,user_name,user_loginId,case when  (user_inactive ='Y') then 'Inactive' else 'Active' end as Status,case when  (user_maclock ='Y') then 'Mac Restriction' else 'Mac Open' end as StatusMac,user_status as Onlinestatus, (select top 1  deg_designation from tbl_master_designation where deg_id in (select top 1 emp_Designation from tbl_trans_employeeCTC where emp_CntId= user_contactId order by emp_id desc )) as designation FROM [tbl_master_user],tbl_master_employee where emp_ContactId=user_contactId  and user_branchId in (" + HttpContext.Current.Session["userbranchHierarchy"] + ") and " + userGrid.FilterExpression;

                        //RootUserDataSource.SelectCommand = "SELECT distinct user_id,user_name,user_loginId,case when  (user_inactive ='Y') then 'Inactive' else 'Active' end as Status,user_status as Onlinestatus, (select deg_designation from tbl_master_designation where deg_id =emp_Designation) as designation FROM [tbl_master_user],tbl_trans_employeeCTC where emp_cntId=user_contactId and user_branchId in (" + HttpContext.Current.Session["userbranchHierarchy"] + ") and " + userGrid.FilterExpression;
                        userGrid.DataBind();
                    }
                }
                else
                {
                    //if (e.Parameters == "s")
                    //    userGrid.Settings.ShowFilterRow = true;
                    if (e.Parameters == "All")
                    {
                        userGrid.FilterExpression = string.Empty;
                        //RootUserDataSource.SelectCommand = "SELECT distinct user_id,user_name,user_loginId,case when  (user_inactive ='Y') then 'Inactive' else 'Active' end as Status,user_status as Onlinestatus, (select deg_designation from tbl_master_designation where deg_id =emp_Designation) as designation FROM [tbl_master_user],tbl_trans_employeeCTC where emp_cntId=user_contactId and user_branchId in (" + HttpContext.Current.Session["userbranchHierarchy"] + ")";
                        RootUserDataSource.SelectCommand = "SELECT  distinct user_id,user_name,user_loginId,case when  (user_inactive ='Y') then 'Inactive' else 'Active' end as Status,case when  (user_maclock ='Y') then 'Mac Restriction' else 'Mac Open' end as StatusMac,user_status as Onlinestatus, (select top 1  deg_designation from tbl_master_designation where deg_id in (select top 1 emp_Designation from tbl_trans_employeeCTC where emp_CntId= user_contactId order by emp_id desc )) as designation FROM [tbl_master_user],tbl_master_employee where emp_ContactId=user_contactId  and user_branchId in (" + HttpContext.Current.Session["userbranchHierarchy"] + ")";


                        userGrid.DataBind();
                    }
                }
                // Rev 1.0
            }
            // End of Rev 1.0
        }
        protected void userGrid_CustomJSProperties(object sender, DevExpress.Web.ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpHeight"] = "1";
        }


        [WebMethod]
        public static bool Resetloggedin(string Userid)
        {
            UserBL objUserBL = new UserBL();

            bool flag = false;

            int i = objUserBL.UpdateUserStatus(Userid);
            if (i > 0)
            {
                flag = true;
            }
            return flag;

        }

        public DataTable BindUserList()
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_UserListBind");
            proc.AddIntegerPara("@userid", Convert.ToInt32(HttpContext.Current.Session["userid"]));
            proc.AddPara("@BRANCHID", Convert.ToString(HttpContext.Current.Session["userbranchHierarchy"]));
            proc.AddPara("@ACTION", "BINDUSERLIST");
            // Rev 2.0
            proc.AddPara("@Users", Convert.ToString(txtUser_hidden.Value));
            // End of Rev 2.0
            dt = proc.GetTable();
            return dt;
        }
        // Rev 1.0
        protected void EntityServerModelogDataSource_Selecting(object sender, DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs e)
        {
            e.KeyExpression = "user_id";
            string connectionString = Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]);
            string IsFilter = Convert.ToString(hfIsFilter.Value);
           
            ERPDataClassesDataContext dc1 = new ERPDataClassesDataContext(connectionString);

            if (IsFilter == "Y")
            {
                var q = from d in dc1.FSMUser_Master_Lists
                        where d.USERID == Convert.ToInt64(HttpContext.Current.Session["userid"].ToString())
                        orderby d.SRLNO
                        select d;
                e.QueryableSource = q;

            }
            else
            {
                var q = from d in dc1.FSMUser_Master_Lists
                        where d.SRLNO == 0
                        select d;
                e.QueryableSource = q;
            }
        }
        // End of Rev 1.0
        protected void FillComboPartyType()
        {
            string[,] DataPartyType = oDBEngine.GetFieldValue("tbl_shoptype", "TypeId,Name", "IsActive=1", 2);
            oclsDropDownList.AddDataToDropDownList(DataPartyType, ddlPartyType);

            oclsDropDownList.AddDataToDropDownList(DataPartyType, ddlPartyTypes);

            DataTable PartyListTable = oDBEngine.GetDataTable("select distinct SHOP_CODE as Shop_Code from FTS_EmployeeShopMap where ASSIGN_BY=" + Convert.ToString(HttpContext.Current.Session["userid"]));
            Session["PartyListTable"] = PartyListTable;

            DataTable UsersListTable = oDBEngine.GetDataTable("select distinct USER_ID as User_id from FTS_EmployeeShopMap where ASSIGN_BY=" + Convert.ToString(HttpContext.Current.Session["userid"]));
            Session["UsersListTable"] = UsersListTable;

            // Mantis Issue 24362
            string[,] dtBranchList = oDBEngine.GetFieldValue("tbl_master_branch", "branch_id, branch_description ", null, 2);
            oclsDropDownList.AddDataToDropDownList(dtBranchList, ddlBranch);
            // End of Mantis Issue 24362
        }

        protected void PartyGrid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string receviedString = e.Parameters;
            PartyGrid.JSProperties["cpReceviedString"] = receviedString;

            if (receviedString == "SelectAllPartyFromList")
            {
                PartyGrid.Selection.SelectAll();
            }
            else if (receviedString == "ClearSelectedParty")
            {
                PartyGrid.Selection.UnselectAll();
            }
            else if (receviedString == "SetAllRecordToDataTable")
            {
                List<object> PartyList = PartyGrid.GetSelectedFieldValues("Shop_Code");
                if (PartyList.Count > 0)
                {
                    DataTable PartyListtable = new DataTable();//(DataTable)Session["PartyListTable"];
                    PartyListtable.Columns.Add("Shop_Code", typeof(string));
                    foreach (object obj in PartyList)
                    {
                        if (Convert.ToString(obj) != "0")
                            PartyListtable.Rows.Add(Convert.ToString(obj));
                    }
                    Session["PartyListTable"] = PartyListtable;
                    PartyGrid.JSProperties["cpPartyselected"] = 1;
                }
                else
                {
                    PartyGrid.JSProperties["cpPartyselected"] = 2;
                }
            }
            else if (receviedString == "SetAllSelectedRecord")
            {
                DataTable PartyListtable = (DataTable)Session["PartyListTable"];
                PartyGrid.Selection.UnselectAll();
                if (PartyListtable != null)
                {
                    foreach (DataRow dr in PartyListtable.Rows)
                    {
                        PartyGrid.Selection.SelectRowByKey(dr["Shop_Code"]);
                        if (Convert.ToString(dr["Shop_Code"]) == "0")
                        {
                            PartyGrid.JSProperties["Shop_Code"] = 1;
                        }
                    }
                }
            }
            else if (receviedString == "BindPartyList")
            {
                PartyGrid.DataSource = BindPartyList();
                PartyGrid.DataBind();

                DataTable PartyListtable = (DataTable)Session["PartyListTable"];
                PartyGrid.Selection.UnselectAll();
                //if (PartyListtable != null)
                //{
                //    foreach (DataRow dr in PartyListtable.Rows)
                //    {
                //        PartyGrid.Selection.SelectRowByKey(dr["Shop_Code"]);
                //    }
                //}
            }
        }


        public DataTable BindPartyList()
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_UserListBind");
            proc.AddIntegerPara("@userid", Convert.ToInt32(HttpContext.Current.Session["userid"]));
            proc.AddPara("@BRANCHID", Convert.ToString(HttpContext.Current.Session["userbranchHierarchy"]));
            proc.AddPara("@ACTION", "BINDPARTY");
            proc.AddPara("@PARTY_TYPE", ddlPartyType.SelectedValue);
            dt = proc.GetTable();
            return dt;
        }


        protected void UsersGrid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string receviedString = e.Parameters;
            UsersGrid.JSProperties["cpReceviedString"] = receviedString;

            if (receviedString == "SelectAllUsersFromList")
            {
                UsersGrid.Selection.SelectAll();
            }
            else if (receviedString == "ClearSelectedUsers")
            {
                UsersGrid.Selection.UnselectAll();
            }
            else if (receviedString == "SetAllRecordToDataTable")
            {
                DataTable PartyListTable = (DataTable)Session["PartyListTable"];
                if (PartyListTable.Rows.Count > 0)
                {
                    List<object> UsersList = UsersGrid.GetSelectedFieldValues("user_id");
                    if (UsersList.Count > 0)
                    {
                        DataTable UsersListtable = new DataTable();// (DataTable)Session["UsersListTable"];
                        UsersListtable.Columns.Add("User_id", typeof(string));
                        foreach (object obj in UsersList)
                        {
                            if (Convert.ToInt32(obj) != 0)
                                UsersListtable.Rows.Add(Convert.ToInt32(obj));
                        }
                        Session["UsersListTable"] = UsersListtable;

                        DataTable dtchk = EmployeeShopMapInsertUpdate();
                        if (dtchk != null && dtchk.Rows.Count > 0)
                        {
                            UsersGrid.JSProperties["cpPartyselected"] = 1;
                        }
                        else
                        {
                            UsersGrid.JSProperties["cpPartyselected"] = 5;
                        }

                    }
                    else
                    {
                        UsersGrid.JSProperties["cpPartyselected"] = 3;
                    }
                }
                else
                {
                    UsersGrid.JSProperties["cpPartyselected"] = 2;
                }
            }
            else if (receviedString == "SetAllSelectedRecord")
            {
                DataTable UsersListtable = (DataTable)Session["UsersListTable"];
                UsersGrid.Selection.UnselectAll();
                if (UsersListtable != null)
                {
                    foreach (DataRow dr in UsersListtable.Rows)
                    {
                        UsersGrid.Selection.SelectRowByKey(dr["user_id"]);
                        if (Convert.ToString(dr["user_id"]) == "0")
                        {
                            UsersGrid.JSProperties["cpBrChecked"] = 1;
                        }
                    }
                }
            }
            else if (receviedString == "BindUsersList")
            {
                UsersGrid.DataSource = BindUsersList();
                UsersGrid.DataBind();

                DataTable UsersListtable = (DataTable)Session["UsersListTable"];
                UsersGrid.Selection.UnselectAll();
                if (UsersListtable != null)
                {
                    //foreach (DataRow dr in UsersListtable.Rows)
                    //{
                    //    UsersGrid.Selection.SelectRowByKey(dr["user_id"]);
                    //}
                }
            }
        }


        public DataTable BindUsersList()
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_UserListBind");
            proc.AddIntegerPara("@userid", Convert.ToInt32(HttpContext.Current.Session["userid"]));
            proc.AddPara("@BRANCHID", Convert.ToString(HttpContext.Current.Session["userbranchHierarchy"]));
            proc.AddPara("@ACTION", "BINDUSERS");
            dt = proc.GetTable();
            return dt;
        }

        public DataTable EmployeeShopMapInsertUpdate()
        {
            DataTable PartyListtable = (DataTable)Session["PartyListTable"];
            DataTable UsersListtable = (DataTable)Session["UsersListTable"];

            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_EmployeeShopMapInsertUpdate");
            proc.AddPara("@ACTION", "INSERT");
            proc.AddIntegerPara("@userid", Convert.ToInt32(HttpContext.Current.Session["userid"]));
            proc.AddPara("@SHOP_CODEList", PartyListtable);
            proc.AddPara("@User_IdList", UsersListtable);
            proc.AddPara("@PARTY_TYPE", ddlPartyType.SelectedValue);
            dt = proc.GetTable();
            return dt;
        }

        protected void PartyGrid_DataBinding(object sender, EventArgs e)
        {
            PartyGrid.DataSource = BindPartyList();


            DataTable PartyListtable = (DataTable)Session["PartyListTable"];
            //PartyGrid.Selection.UnselectAll();
            if (PartyListtable != null)
            {
                //foreach (DataRow dr in PartyListtable.Rows)
                //{
                //    PartyGrid.Selection.SelectRowByKey(dr["Shop_Code"]);
                //}
            }
        }

        protected void UsersGrid_DataBinding(object sender, EventArgs e)
        {
            UsersGrid.DataSource = BindUsersList();


            DataTable UsersListtable = (DataTable)Session["UsersListTable"];
            // UsersGrid.Selection.UnselectAll();
            if (UsersListtable != null)
            {
                //foreach (DataRow dr in UsersListtable.Rows)
                //{
                //    UsersGrid.Selection.SelectRowByKey(dr["user_id"]);
                //}
            }
        }

        protected void UnAssignPartyGrid_DataBinding(object sender, EventArgs e)
        {
            UnAssignPartyGrid.DataSource = BindAllSelectedPartyList();
        }

        protected void UnAssignPartyGrid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string receviedString = e.Parameters;
            UnAssignPartyGrid.JSProperties["cpReceviedString"] = receviedString;

            if (receviedString == "SelectAllPartyFromList")
            {
                UnAssignPartyGrid.Selection.SelectAll();
            }
            else if (receviedString == "ClearSelectedParty")
            {
                UnAssignPartyGrid.Selection.UnselectAll();
            }
            else if (receviedString == "SetAllRecordToDataTable")
            {
                List<object> PartyList = UnAssignPartyGrid.GetSelectedFieldValues("ID");
                if (PartyList.Count > 0)
                {
                    DataTable PartyListtable = new DataTable();//(DataTable)Session["PartyListTable"];
                    PartyListtable.Columns.Add("ID", typeof(string));
                    foreach (object obj in PartyList)
                    {
                        if (Convert.ToString(obj) != "0")
                            PartyListtable.Rows.Add(Convert.ToString(obj));
                    }

                    EmployeeShopMapUnAssign(PartyListtable);
                    UnAssignPartyGrid.JSProperties["cpPartyselected"] = 1;
                }
                else
                {
                    UnAssignPartyGrid.JSProperties["cpPartyselected"] = 2;
                }
            }
            else if (receviedString == "SetAllSelectedRecord")
            {
                DataTable PartyListtable = (DataTable)Session["PartyListTable"];
                UnAssignPartyGrid.Selection.UnselectAll();
                if (PartyListtable != null)
                {
                    foreach (DataRow dr in PartyListtable.Rows)
                    {
                        UnAssignPartyGrid.Selection.SelectRowByKey(dr["Shop_Code"]);
                        if (Convert.ToString(dr["Shop_Code"]) == "0")
                        {
                            UnAssignPartyGrid.JSProperties["Shop_Code"] = 1;
                        }
                    }
                }
            }
            else if (receviedString == "BindAssignPartyList")
            {
                UnAssignPartyGrid.DataSource = BindAllSelectedPartyList();
                UnAssignPartyGrid.DataBind();

                UnAssignPartyGrid.Selection.UnselectAll();
            }
        }

        public DataTable BindAllSelectedPartyList()
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_UserListBind");
            proc.AddIntegerPara("@userid", Convert.ToInt32(HttpContext.Current.Session["userid"]));
            proc.AddPara("@BRANCHID", Convert.ToString(HttpContext.Current.Session["userbranchHierarchy"]));
            proc.AddPara("@ACTION", "BindAllSelectedPartyList");
            dt = proc.GetTable();
            return dt;
        }

        public DataTable EmployeeShopMapUnAssign(DataTable AssignID)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_EmployeeShopMapInsertUpdate");
            proc.AddPara("@ACTION", "UnAssign");
            proc.AddIntegerPara("@userid", Convert.ToInt32(HttpContext.Current.Session["userid"]));
            proc.AddPara("@SHOP_CODEList", AssignID);
            dt = proc.GetTable();
            return dt;
        }

        protected void grdAssignPartyList_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string receviedString = e.Parameters;

            grdAssignPartyList.DataSource = BindAssignParty();
            grdAssignPartyList.DataBind();
        }

        protected void grdAssignPartyList_DataBinding(object sender, EventArgs e)
        {
            grdAssignPartyList.DataSource = BindAssignParty();
        }

        public DataTable BindAssignParty()
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_UserListBind");
            proc.AddIntegerPara("@userid", Convert.ToInt32(HttpContext.Current.Session["userid"]));
            proc.AddPara("@BRANCHID", Convert.ToString(HttpContext.Current.Session["userbranchHierarchy"]));
            proc.AddPara("@ACTION", "BindAssignParty");
            dt = proc.GetTable();
            return dt;
        }

        [WebMethod(EnableSession = true)]
        public static object GetParty(string SearchKey, string PartyType)
        {
            List<PartyModel> listShop = new List<PartyModel>();
            if (HttpContext.Current.Session["userid"] != null)
            {
                SearchKey = SearchKey.Replace("'", "''");

                BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();
                 /* Rev Work 08.04.2022
         Mantise No:0024819 In user master in Assign Party entry section in edit mode selected party not coming as checked*/
               // DataTable Shopdt = oDBEngine.GetDataTable("select top(10)Shop_Code as Id,Entity_Location,Replace(Shop_Name,'''','&#39;') as Name,EntityCode from tbl_Master_shop where (type='" + PartyType + "' and Shop_Name like '%" + SearchKey + "%' ) or  (type='" + PartyType + "' and EntityCode like '%" + SearchKey + "%')");
                DataTable Shopdt = oDBEngine.GetDataTable("select top(10)Shop_Code as id,Entity_Location,Replace(Shop_Name,'''','&#39;') as Name,EntityCode from tbl_Master_shop where (type='" + PartyType + "' and Shop_Name like '%" + SearchKey + "%' ) or  (type='" + PartyType + "' and EntityCode like '%" + SearchKey + "%')");
              /* Rev Work close 08.04.2022
         Mantise No:0024819 In user master in Assign Party entry section in edit mode selected party not coming as checked*/
                listShop = APIHelperMethods.ToModelList<PartyModel>(Shopdt);
                
            }

            return listShop;
        }

        public class PartyModel
        {
            /*Rev Work 08.04.2022
             Mantise No:0024819 In user master in Assign Party entry section in edit mode selected party not coming as checked*/
            //public string Id { get; set; }
            public string id { get; set; }
            /*Rev Work close 08.04.2022
             Mantise No:0024819 In user master in Assign Party entry section in edit mode selected party not coming as checked*/
            public string Name { get; set; } 
            //public string Entity_Location { get; set; }
            //public string EntityCode { get; set; }
        }

        // Mantis Issue 24362 [parameter branch added]
        [WebMethod(EnableSession = true)]
        public static object GetUserList(string shop_code, string branch)
        {
            List<UserList> listShop = new List<UserList>();
            if (HttpContext.Current.Session["userid"] != null)
            {
                DataTable Userdt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("prc_UserListBind");
                proc.AddPara("@ACTION", "BindUserListNew");
                proc.AddPara("@SHOP_CODE", shop_code);
                proc.AddPara("@userid", Convert.ToString(HttpContext.Current.Session["userid"]));
                // Mantis Issue 24362
                proc.AddPara("@BRANCHID", branch);
                // End of Mantis Issue 24362
                Userdt = proc.GetTable();
                listShop = APIHelperMethods.ToModelList<UserList>(Userdt);

            }

            return listShop;
        }

        public class UserList
        {
            public string UserID { get; set; }
            public string username { get; set; }
            public bool selected { get; set; }
        }

        // Mantis Issue 24363
        public class PartyList
        {
            public string Shop_Code { get; set; }
            public string Shop_Name { get; set; }
            public bool selected { get; set; }
        }
        // End of Mantis Issue 24363

        /*Rev Work 08.04.2022
         Mantise No:0024819 In user master in Assign Party entry section in edit mode selected party not coming as checked*/
        public class PartySelectID
        {
            public string id { get; set; }
            public string Shop_Code { get; set; }
            public string Shop_Name { get; set; }        
        }
        /*Rev Work close 08.04.2022
         Mantise No:0024819 In user master in Assign Party entry section in edit mode selected party not coming as checked*/

        // Mantis Issue 24362 [ parameter Branch added ]
        [WebMethod(EnableSession = true)]
        public static string SaveAssignParty(string shop_code, string Shop_type, string Users, string Headr_name, string header_id, string branch)
        {
            string returnmsg = "";
            if (HttpContext.Current.Session["userid"] != null)
            {
                DataTable Userdt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("prc_EmployeeShopMapInsertUpdate");
                proc.AddPara("@ACTION", "AssignShopUserNew");
                proc.AddPara("@SHOP_CODE", shop_code);
                proc.AddPara("@PARTY_TYPE", Shop_type);
                proc.AddPara("@Users", Users);
                proc.AddPara("@NAME", Headr_name);
                proc.AddPara("@headerid", header_id);
                proc.AddPara("@userid", Convert.ToString(HttpContext.Current.Session["userid"]));
                // Mantis Issue 24362
                proc.AddPara("@BRANCHID", branch);
                // End of Mantis Issue 24362
                Userdt = proc.GetTable();
                if (Userdt!=null)
                {
                    returnmsg = "OK";
                }
                else
                {
                    returnmsg = "Already exists";
                }
            }
            return returnmsg;
        }

        [WebMethod(EnableSession = true)]
        public static object EditGetUserList(string Header_id)
        {
            EditPartyAssignUserList edtidtls = new EditPartyAssignUserList();
            List<UserList> listusrs = new List<UserList>();
            //Mantis Issue 24363
           List<PartyList> listParty = new List<PartyList>();
            // End of Mantis Issue 24363

           /*Rev Work 08.04.2022
            Mantise No:0024819 In user master in Assign Party entry section in edit mode selected party not coming as checked*/
           List<PartySelectID> PartySelectIDList = new List<PartySelectID>();
           /*Rev Work close 08.04.2022
            Mantise No:0024819 In user master in Assign Party entry section in edit mode selected party not coming as checked*/

           if (HttpContext.Current.Session["userid"] != null)
            {
                DataSet Userds = new DataSet();
                ProcedureExecute proc = new ProcedureExecute("prc_UserListBind");
                proc.AddPara("@ACTION", "EditBindUserListNew");
                proc.AddPara("@Header_id", Header_id);
                proc.AddPara("@userid", Convert.ToString(HttpContext.Current.Session["userid"]));
                Userds = proc.GetDataSet();
                listusrs = APIHelperMethods.ToModelList<UserList>(Userds.Tables[1]);
                // Mantis Issue 24363
                listParty = APIHelperMethods.ToModelList<PartyList>(Userds.Tables[2]);
                // End of Mantis Issue 24363
                edtidtls.Name = Convert.ToString(Userds.Tables[0].Rows[0]["NAME"]);
                // Mantis Issue 24363
                //edtidtls.Shop_Name = Convert.ToString(Userds.Tables[0].Rows[0]["Shop_Name"]);
                //edtidtls.Shop_code = Convert.ToString(Userds.Tables[0].Rows[0]["Shop_code"]);
                edtidtls.PartyList = listParty;
                // End of Mantis Issue 24363
                edtidtls.Shop_type = Convert.ToString(Userds.Tables[0].Rows[0]["Shop_type"]);
                edtidtls.UserList = listusrs;

                /*Rev Work 08.04.2022
                 Mantise No:0024819 In user master in Assign Party entry section in edit mode selected party not coming as checked*/
                PartySelectIDList = APIHelperMethods.ToModelList<PartySelectID>(Userds.Tables[3]);
                edtidtls.PartySelectID = PartySelectIDList;
                /*Rev Work close 08.04.2022
                 Mantise No:0024819 In user master in Assign Party entry section in edit mode selected party not coming as checked*/
                //Rev work start 01.08.2022 mantise no:0025119 branch details not fetch in assign party details edit mode in user master
                edtidtls.BranchCode = Convert.ToString(Userds.Tables[0].Rows[0]["BranchCode"]);
                //Rev work start 01.08.2022 mantise no:0025119 branch details not fetch in assign party details edit mode in user master
            }

            return edtidtls;
        }

        public class EditPartyAssignUserList
        {
            public string Name { get; set; }
            public string Shop_Name { get; set; }
            public string Shop_code { get; set; }
            public string Shop_type { get; set; }
            public List<UserList> UserList { get; set; }
            // Mantis Issue 24363
            public List<PartyList> PartyList { get; set; }
            // End of Mantis Issue 24363

            /*Rev Work 08.04.2022
             Mantise No:0024819 In user master in Assign Party entry section in edit mode selected party not coming as checked*/
            public List<PartySelectID> PartySelectID { get; set; }
            /*Rev Work close 08.04.2022
             Mantise No:0024819 In user master in Assign Party entry section in edit mode selected party not coming as checked*/
            //Rev work start 01.08.2022 mantise no:0025119 branch details not fetch in assign party details edit mode in user master
            public string BranchCode { get; set; }
            //Rev work close 01.08.2022 mantise no:0025119 branch details not fetch in assign party details edit mode in user master
        }

        [WebMethod(EnableSession = true)]
        public static string DeleteAssignParty(string header_id)
        {
            string returnmsg = "";
            if (HttpContext.Current.Session["userid"] != null)
            {                
                ProcedureExecute proc = new ProcedureExecute("prc_EmployeeShopMapInsertUpdate");
                proc.AddPara("@ACTION", "DeleteShopUserNew");
                proc.AddPara("@headerid", header_id);
                proc.AddPara("@userid", Convert.ToString(HttpContext.Current.Session["userid"]));
                int i = proc.RunActionQuery();
                if (i > 0)
                {
                    returnmsg = "OK";
                }               
            }
            return returnmsg;
        }
        //Mantis Issue 25116
        [WebMethod(EnableSession = true)]
        public static string AttendanceLeaveClear(string User_Id)
        {
            var message = "";
            Int64 userId = Convert.ToInt64(User_Id);
            DataSet dsUserDetail = new DataSet();

            ProcedureExecute proc = new ProcedureExecute("PRC_FTSInsertUpdateUser");
            proc.AddPara("@ACTION", "EDIT");
            proc.AddPara("@user_id", userId);
            dsUserDetail = proc.GetDataSet();

            if (dsUserDetail.Tables[0].Rows.Count > 0)
            {
                // Rev 3.0
                //if (Convert.ToBoolean(dsUserDetail.Tables[0].Rows[0]["ShowAttednaceClearmenu"]) == true)
                //{
                // End of Rev 3.0
                    DataTable dt = new DataTable();
                    ProcedureExecute proc1 = new ProcedureExecute("PRC_DeleteAttendanceLeve");
                    proc1.AddPara("@ACTION", "DELETELEAVEATTENDANCE");
                    proc1.AddPara("@user_id", userId);
                    //proc1.AddPara("@LEAVE_APPLY_DATE", "EDIT");
                    //proc1.AddPara("@ISONLEAVE", userId);
                    //proc1.AddPara("@ISLEAVEDELETE", "EDIT");
                    dsUserDetail = proc1.GetDataSet();
                    dt = dsUserDetail.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        message = "Attendance/Leave Cleared Successfully.";
                    }
                    else
                    {
                        message = "No data found";
                    }
                // Rev 3.0    
                //}
                //else
                //{
                //    message = "Clear Attendance/Leave is disabled.";
                //}
                // End of Rev 3.0
            }

            return message;
        }
        //End of Mantis Issue 25116
        // Rev 2.0
        public class UserModel
        {
            public string id { get; set; }
            public string user_name { get; set; }
            public string user_loginId { get; set; }
            public string EmployeeID { get; set; }
        }
        [WebMethod]
        public static object GetOnDemandUser(string SearchKey)
        {
            List<UserModel> listUser = new List<UserModel>();
            if (HttpContext.Current.Session["userid"] != null)
            {
                SearchKey = SearchKey.Replace("'", "''");
                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_UserNameSearchForListing");
                proc.AddPara("@USER_ID", Convert.ToInt32(HttpContext.Current.Session["userid"]));
                proc.AddPara("@SearchKey", SearchKey);
                dt = proc.GetTable();

                listUser = (from DataRow dr in dt.Rows
                                select new UserModel()
                                {
                                    id = Convert.ToString(dr["user_id"]),
                                    user_name = Convert.ToString(dr["user_name"]),
                                    user_loginId = Convert.ToString(dr["user_loginId"]),
                                    EmployeeID = Convert.ToString(dr["EmployeeID"])
                                }).ToList();
            }

            return listUser;
        }
        // End of Rev 2.0
    }
}
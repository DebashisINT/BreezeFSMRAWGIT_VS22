//*********************************************************************************************************************
// 1.0      17/02/2023      2.0.39      Sanchita        A setting required for 'User Account' Master module in FSM Portal
//                                                      Refer: 25669
//*********************************************************************************************************************
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
using System.Web.UI.WebControls;

namespace ERP.OMS.Management.Master
{
    public partial class management_master_UserAccountList : ERP.OMS.ViewState_class.VSPage
    {
        public string pageAccess = "";       

        BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(string.Empty);
        public EntityLayer.CommonELS.UserRightsForPage rights = new EntityLayer.CommonELS.UserRightsForPage();
        clsDropDownList oclsDropDownList = new clsDropDownList();

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
            rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/management/master/UserAccountList.aspx");

            //------- For Read Only User in SQL Datasource Connection String   Start-----------------

            //if (HttpContext.Current.Session["EntryProfileType"] != null)
            //{
            //    if (Convert.ToString(HttpContext.Current.Session["EntryProfileType"]) == "R")
            //    {
            //        RootUserDataSource.ConnectionString = ConfigurationSettings.AppSettings["DBReadOnlyConnection"];
            //    }
            //    else
            //    {
            //        RootUserDataSource.ConnectionString = ConfigurationSettings.AppSettings["DBConnectionDefault"];
            //    }
            //}
            //------- For Read Only User in SQL Datasource Connection String   End-----------------

            // Rev 1.0
            string IsShowUserAccountForITC = "0";
            DBEngine obj1 = new DBEngine();
            IsShowUserAccountForITC = Convert.ToString(obj1.GetDataTable("select [value] from FTS_APP_CONFIG_SETTINGS WHERE [Key]='IsShowUserAccountForITC'").Rows[0][0]);

            if (IsShowUserAccountForITC == "1")
            {
                userGrid.Columns[3].Visible = true;
                userGrid.Columns[6].Visible = true;
                userGrid.Columns[7].Visible = true;
                userGrid.Columns[8].Visible = true;
            }
            else
            {
                userGrid.Columns[3].Visible = false;
                userGrid.Columns[6].Visible = false;
                userGrid.Columns[7].Visible = false;
                userGrid.Columns[8].Visible = false;
            }
            // End of Rev 1.0

            if (!IsPostBack)
            {
                userGrid.SettingsCookies.CookiesID = "BreeezeErpGridCookiesroot_useruserGrid";
                this.Page.ClientScript.RegisterStartupScript(GetType(), "setCookieOnStorage", "<script>addCookiesKeyOnStorage('BreeezeErpGridCookiesroot_useruserGrid');</script>");                               
                Session["exportval"] = null;               
            }
            userGrid.DataSource = BindUserList();
            userGrid.DataBind();
        }
        public DataTable BindUserList()
        {
            string strFromDOJ = String.Empty, strToDOJ = String.Empty;

            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Prc_UserAccountData");
            proc.AddPara("@FromJoinDate", strFromDOJ == "" ? "1900-01-01" : strFromDOJ);
            proc.AddPara("@ToJoinDate", strToDOJ == "" ? "9999-12-31" : strToDOJ);
            proc.AddIntegerPara("@User_id", Convert.ToInt32(HttpContext.Current.Session["userid"]));
            dt = proc.GetTable();
            return dt;
        }
        protected void cmbExport_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            Int32 Filter = int.Parse(Convert.ToString(drdExport.SelectedItem.Value));           
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
        public void bindexport(int Filter)
        {         
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
        protected void userGrid_CustomJSProperties(object sender, DevExpress.Web.ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpHeight"] = "1";
        }
    }
}
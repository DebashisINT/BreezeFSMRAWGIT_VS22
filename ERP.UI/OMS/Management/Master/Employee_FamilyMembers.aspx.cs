using System;
using System.Web;
using BusinessLogicLayer;
using System.Configuration;
using EntityLayer.CommonELS;
using DevExpress.Web;

namespace ERP.OMS.Management.Master
{
    public partial class management_master_Employee_FamilyMembers : ERP.OMS.ViewState_class.VSPage
    {
        BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine("");
        public EntityLayer.CommonELS.UserRightsForPage rights = new UserRightsForPage();
        protected void Page_Load(object sender, EventArgs e)
        {
            rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/management/Master/employee.aspx");
            
            //------- For Read Only User in SQL Datasource Connection String   Start-----------------

            if (HttpContext.Current.Session["EntryProfileType"] != null)
            {
                if (Convert.ToString(HttpContext.Current.Session["EntryProfileType"]) == "R")
                {
                    FamilyMemberData.ConnectionString = ConfigurationSettings.AppSettings["DBReadOnlyConnection"];
                }
                else
                {
                    FamilyMemberData.ConnectionString = ConfigurationSettings.AppSettings["DBConnectionDefault"];
                }
            }

            //------- For Read Only User in SQL Datasource Connection String   End-----------------

            string[,] EmployeeNameID = oDBEngine.GetFieldValue(" tbl_master_contact ", " case when cnt_firstName is null then '' else cnt_firstName end + ' '+case when cnt_middleName is null then '' else cnt_middleName end+ ' '+case when cnt_lastName is null then '' else cnt_lastName end+' ['+cnt_shortName+']' as name ", " cnt_internalId='" + HttpContext.Current.Session["KeyVal_InternalID"] + "'", 1);
            if (EmployeeNameID[0, 0] != "n")
            {
                lblHeader.Text = EmployeeNameID[0, 0].ToUpper();
            }

        }
        protected void FamilyMemberGrid_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            FamilyMemberGrid.SettingsText.PopupEditFormCaption = "Modify Family Relationship";
        }
        protected void FamilyMemberGrid_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            if (!rights.CanDelete)
            {
                if (e.ButtonType == ColumnCommandButtonType.Delete)
                {
                    e.Visible = false;
                }
            }


            if (!rights.CanEdit)
            {
                if (e.ButtonType == ColumnCommandButtonType.Edit)
                {
                    e.Visible = false;
                }
            }
        }
    }
}

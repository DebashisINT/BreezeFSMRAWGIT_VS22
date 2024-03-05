//********************************************************************************************************************
// Written by    Sanchita    V2.0.45     14-02-2024      Change Password option not working. Mantis: 27242

//**********************************************************************************************************************

using System;
using System.Web;
using System.Web.UI;
using System.Configuration;
using BusinessLogicLayer;
using UtilityLayer;


namespace ERP.OMS.Management.ToolsUtilities
{
    public partial class frmchangepassword : System.Web.UI.Page
    {
        BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();
        public string pageAccess = "";
        //  DBEngine oDBEngine = new DBEngine(string.Empty);
        protected void Page_PreInit(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //'http://localhost:2957/InfluxCRM/management/testProjectMainPage_employee.aspx'
                string sPath = HttpContext.Current.Request.Url.ToString();
                oDBEngine.Call_CheckUserSession(sPath);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["userid"] == null)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "SighOff", "<script>SignOff();</script>");
            }
            //this.Page.ClientScript.RegisterStartupScript(GetType(), "heightL", "<script>height();</script>");
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {

            //// Encrypt  the Password
            Encryption epasswrd = new Encryption();
            string Encryptpass_Old = epasswrd.Encrypt(TxtOldPassword.Text.Trim());
            string Encryptpass_New = epasswrd.Encrypt(TxtNewPassword.Text.Trim());


            //if (Session["userpassword"].ToString() == TxtOldPassword.Text)
            if (Session["userpassword"].ToString() == Encryptpass_Old)
            {

                 oDBEngine.SetFieldValue("tbl_master_user", "user_password='" + Encryptpass_New + "'", " user_id='" + Session["userid"].ToString() + "'");

              
                Session.Abandon();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "jAlert('Password has been changed successfully. You need to Re-login.','Alert',function(){window.location='/OMS/SignOff.aspx'}); ", true);

                TxtConfirmPassword.Text = "";
                TxtNewPassword.Text = "";
                TxtOldPassword.Text = "";


            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "JScript", "<script>jAlert('Invalid Old Password !!')</script>");
                TxtOldPassword.Text = "";
                TxtNewPassword.Text = "";
                TxtConfirmPassword.Text = "";
            }
        }
    }
}
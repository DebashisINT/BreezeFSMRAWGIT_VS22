﻿using System;
using System.Web;
using System.Web.UI;
using System.Configuration;
using BusinessLogicLayer;
using UtilityLayer;

namespace ERP.OMS.Management.ToolsUtilities
{
    public partial class frmchangeuserspassword : System.Web.UI.Page
    {
        //DBEngine oDBEngine = new DBEngine(ConfigurationSettings.AppSettings["DBConnectionDefault"]);
        BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(ConfigurationSettings.AppSettings["DBConnectionDefault"]);
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
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {

                string Encryptpass_New = "";
                Encryption epasswrd = new Encryption();
                if (TxtNewPassword.Text != "")
                {
                     Encryptpass_New = epasswrd.Encrypt(TxtNewPassword.Text.Trim());
                }

                if (Request.QueryString["uid"] != null)
                {
                    string[] strisactive = oDBEngine.GetFieldValue1("tbl_master_user", "user_status", " user_id='" + Convert.ToString(Request.QueryString["uid"]) + "'", 1);
                    if (strisactive[0] == "0")
                    {
                        oDBEngine.SetFieldValue("tbl_master_user", "user_password='" + Encryptpass_New + "'", " user_id='" + Convert.ToString(Request.QueryString["uid"]) + "'");

                        Session.Remove("ChangePassOfUserid");

                        Page.ClientScript.RegisterStartupScript(GetType(), "JScript", "<script>jAlert('Changed Successfully !!')</script>");
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "JScript", "<script>jAlert('Already User logged in.To change password please logout!!')</script>");

                    }
                    TxtConfirmPassword.Text = "";
                    TxtNewPassword.Text = "";
                }
                //}
                //else
                //{
                //    Page.ClientScript.RegisterStartupScript(GetType(), "JScript", "<script>alert('Invalid Password !!')</script>");
                //    TxtOldPassword.Text = "";
                //    TxtNewPassword.Text = "";
                //    TxtConfirmPassword.Text = "";
                //}
            }
            catch { }
        }
    }
}
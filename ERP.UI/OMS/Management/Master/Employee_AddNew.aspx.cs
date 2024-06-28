/***********************************************************************************************************
 * 1.0      Sanchita        V2.0.30     24-03-2023          Unable to modify Channel, Section, Circle once tagged with an Employee. 
 *                                                          Refer: 25754
 * 2.0		PRITI           V2.0.47     13-05-2024	        0027425: At the time of any Employee creation, the branch ID shall be updated in employee branch mapping table                                                          
 * ***********************************************************************************************************/
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using ClsDropDownlistNameSpace;
using ERP.OMS.CustomFunctions;
using System.Web.Services;
using System.Collections.Generic;
using DataAccessLayer;
using UtilityLayer;

namespace ERP.OMS.Management.Master
{
    public partial class management_master_Employee_AddNew : ERP.OMS.ViewState_class.VSPage
    {
        // Tier architecture
        BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(ConfigurationSettings.AppSettings["DBConnectionDefault"]);
        BusinessLogicLayer.Converter Oconverter = new BusinessLogicLayer.Converter();
        BusinessLogicLayer.GenericMethod oGenericMethod;
        clsDropDownList oclsDropDownList = new clsDropDownList();
        static string CntID = string.Empty;
        string empCompCode = string.Empty;


        SelectListOptions objSelectListOptions = new SelectListOptions();
        DataTable DT = new DataTable();
        //Rev work start 26.04.2022 0024853: Copy feature add in Employee master
        Int32 ID;
        public string WLanguage = "";
        public string SpLanguage = "";
        //Rev work close 26.04.2022 0024853: Copy feature add in Employee master
        protected void Page_Load(object sender, EventArgs e)
        {
            //cmbDOJ.UseMaskBehavior = true;
            //cmbDOJ.EditFormatString = Oconverter.GetDateFormat();
            //cmbDOJ.EditFormatString = "dd-MM-yyyy";
            //cmbDOJ.DisplayFormatString = "dd-MM-yyyy";

            //JoiningDate.UseMaskBehavior = true;
            //JoiningDate.EditFormatString = Oconverter.GetDateFormat();
            //JoiningDate.EditFormatString = "dd-MM-yyyy";
            //JoiningDate.DisplayFormatString = "dd-MM-yyyy";

            //cmbLeaveEff.UseMaskBehavior = true;
            //cmbLeaveEff.EditFormatString = Oconverter.GetDateFormat();
            //cmbLeaveEff.EditFormatString = "dd-MM-yyyy";
            //cmbLeaveEff.DisplayFormatString = "dd-MM-yyyy";

            if (!IsPostBack)
            {
                //Rev work start 26.04.2022 0024853: Copy feature add in Employee master
                if (Request.QueryString["id"] == "ADD")
                {
                    //Rev work close 26.04.2022 0024853: Copy feature add in Employee master
                Page.ClientScript.RegisterStartupScript(GetType(), "PageLD", "<script>Pageload();</script>");
                //txtReportTo.Attributes.Add("onkeyup", "CallList(this,'SearchByEmp',event)");
                //txtReportTo.Attributes.Add("onfocus", "CallList(this,'SearchByEmp',event)");
                //txtReportTo.Attributes.Add("onclick", "CallList(this,'SearchByEmp',event)");

                Session["KeyVal_InternalID"] = "n";



                DT = new DataTable();

                DT = objSelectListOptions.SearchByEmp("", Session["KeyVal_InternalID"]);
                //ddlReportTo.DataSource = DT;
                //ddlReportTo.DataValueField = "Id";
                //ddlReportTo.DataTextField = "Name";
                //ddlReportTo.DataBind();

                //ddlReportTo.Items.Insert(0, "");

                //JoiningDate.UseMaskBehavior = true;
                //JoiningDate.EditFormatString = Oconverter.GetDateFormat("Date");
                //JoiningDate.Value = oDBEngine.GetDate();
                //JoiningDate.EditFormatString = "dd-MM-yyyy";

                ShowForm();
                //Mantis Issue 25148
                GetConfigSettings();
                //End of Mantis Issue 25148
                cmbDOJ.UseMaskBehavior = true;
                cmbDOJ.EditFormatString = "dd-MM-yyyy";
                cmbDOJ.DisplayFormatString = "dd-MM-yyyy";
                cmbDOJ.Date = DateTime.Today;

                JoiningDate.UseMaskBehavior = true;
                JoiningDate.EditFormatString = "dd-MM-yyyy";
                JoiningDate.DisplayFormatString = "dd-MM-yyyy";
                JoiningDate.Date = DateTime.Today;

                cmbLeaveEff.UseMaskBehavior = true;
                cmbLeaveEff.EditFormatString = "dd-MM-yyyy";
                cmbLeaveEff.DisplayFormatString = "dd-MM-yyyy";
                cmbLeaveEff.Date = DateTime.Today;
                    //Rev work start 26.04.2022 0024853: Copy feature add in Employee master
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "PageLD", "<script>Pageload();</script>");
                    Session["KeyVal_InternalID"] = "n";
                    DT = new DataTable();
                    DT = objSelectListOptions.SearchByEmp("", Session["KeyVal_InternalID"]);
                    hrCopy.Value = Request.QueryString["Mode"];
                    ShowForm();
                    //Mantis Issue 25148
                    GetConfigSettings();
                    //End of Mantis Issue 25148
                    cmbDOJ.UseMaskBehavior = true;
                    cmbDOJ.EditFormatString = "dd-MM-yyyy";
                    cmbDOJ.DisplayFormatString = "dd-MM-yyyy";
                    //cmbDOJ.Date = DateTime.Today;
                    cmbDOJ.EditFormatString = Oconverter.GetDateFormat();                    

                    JoiningDate.UseMaskBehavior = true;
                    JoiningDate.EditFormatString = "dd-MM-yyyy";
                    JoiningDate.DisplayFormatString = "dd-MM-yyyy";
                    JoiningDate.Date = DateTime.Today;

                    cmbLeaveEff.UseMaskBehavior = true;
                    cmbLeaveEff.EditFormatString = "dd-MM-yyyy";
                    cmbLeaveEff.DisplayFormatString = "dd-MM-yyyy";
                    //cmbLeaveEff.Date = DateTime.Today;
                    cmbLeaveEff.EditFormatString = Oconverter.GetDateFormat();                                        
                }
                //Rev work close 26.04.2022 0024853: Copy feature add in Employee master
            }

            // Mantis Issue 24655
            if (txtChannels.Text != "") {
                chkChannelDefault.ClientEnabled=true;
            }
            else {
                chkChannelDefault.Checked=false;
                chkChannelDefault.ClientEnabled= false;
            }

            if (txtCircle.Text != "")
            {
                chkCircleDefault.ClientEnabled = true;
            }
            else
            {
                chkCircleDefault.Checked = false;
                chkCircleDefault.ClientEnabled = false;
            }

            if (txtSection.Text != "")
            {
                chkSectionDefault.ClientEnabled = true;
            }
            else
            {
                chkSectionDefault.Checked = false;
                chkSectionDefault.ClientEnabled = false;
            }
            // End of Mantis Issue 24655
            btnSave.Attributes.Add("Onclick", "Javascript:return ValidateGeneral();");
            btnJoin.Attributes.Add("Onclick", "Javascript:return ValidateDOJ();");
            btnCTC.Attributes.Add("Onclick", "Javascript:return ValidateCTC();");
            btnEmpID.Attributes.Add("Onclick", "Javascript:return ValidateEMPID();");
        }

        [WebMethod]
        public static List<string> GetreportTo(string firstname, string shortname)
        {
            BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(ConfigurationSettings.AppSettings["DBConnectionDefault"]);
            DataTable DT = new DataTable();
            DT.Rows.Clear();
            //DT = oDBEngine.GetDataTable("tbl_master_employee, tbl_master_contact,tbl_trans_employeeCTC", "ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') +'['+cnt_shortName+']' AS Name, tbl_master_employee.emp_id as Id    ", " tbl_master_employee.emp_contactId = tbl_trans_employeeCTC.emp_cntId and  tbl_trans_employeeCTC.emp_cntId = tbl_master_contact.cnt_internalId   and cnt_contactType='EM'  and (cnt_firstName Like '" + firstname + "%' or cnt_shortName like '" + shortname + "%')");
            ProcedureExecute proc = new ProcedureExecute("PRC_FetchReportTo");
            proc.AddPara("@action", "ADDNEW");
            proc.AddPara("@userid", Convert.ToString(HttpContext.Current.Session["userid"]));
            proc.AddPara("@firstname", firstname);
            proc.AddPara("@shortname", shortname);
            DT = proc.GetTable();
            List<string> obj = new List<string>();
            foreach (DataRow dr in DT.Rows)
            {

                obj.Add(Convert.ToString(dr["Name"]) + "|" + Convert.ToString(dr["Id"]));
            }
            return obj;
            //    Response.Write("No Record Found###No Record Found|");


        }
        protected void btnSave_Click(object sender, EventArgs e) // 1st
        {
            try
            {
                Employee_BL objEmployee = new Employee_BL();
                bool chkAllow = false;

                // Mantis Issue 24655 [ paarmeter Convert.ToString(DBNull.Value) added for ChannelType, Circle, Section. Also ChannelDefault, CircleDefault and SectionDefault added ]
                //Tire architecture
                string InternalID = objEmployee.btnSave_Click_BL(Convert.ToString(DBNull.Value), CmbSalutation.SelectedItem.Value, txtFirstNmae.Text,
                txtMiddleName.Text, txtLastName.Text, txtAliasName.Text, "0",
                cmbGender.SelectedItem.Value, Convert.ToString(DBNull.Value), Convert.ToString(DBNull.Value), Convert.ToString(DBNull.Value),
                Convert.ToString(DBNull.Value), Convert.ToString(DBNull.Value), Convert.ToString(DBNull.Value), Convert.ToString(DBNull.Value), Convert.ToString(DBNull.Value),
                chkAllow, Convert.ToString(DBNull.Value), Convert.ToString(DBNull.Value), Convert.ToString(DBNull.Value), Convert.ToString(DBNull.Value), Convert.ToString(DBNull.Value));

                HttpContext.Current.Session["KeyVal_InternalID"] = InternalID;
                string[,] cnt_id = oDBEngine.GetFieldValue(" tbl_master_contact", " cnt_id", " cnt_internalId='" + InternalID + "'", 1);
                if (cnt_id[0, 0].ToString() != "n")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "General", "ForJoin();", true);
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "General", "jAlert('Please Try Again!..');", true);
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "General", "ForJoin();", true);
        }
        protected void btnJoin_Click(object sender, EventArgs e) // 2nd
        {
            oGenericMethod = new BusinessLogicLayer.GenericMethod();
            string value = string.Empty;
            //Checking For Expiry Date 
            string ValidationResult = oGenericMethod.IsProductExpired(Convert.ToDateTime(cmbDOJ.Value));
            if (Convert.ToBoolean(ValidationResult.Split('~')[0]))
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Vscript", "jAlert('" + ValidationResult.Split('~')[1] + "');", true);
            else
            {
                value = "emp_din=' ', emp_dateofJoining ='" + cmbDOJ.Value + "', emp_dateofLeaving ='" + DBNull.Value + "',emp_ReasonLeaving  ='" + DBNull.Value + "', emp_NextEmployer ='" + DBNull.Value + "', emp_AddNextEmployer  ='" + DBNull.Value + "',LastModifyDate='" + oDBEngine.GetDate().ToString() + "',LastModifyUser='" + Session["userid"].ToString() + "'";
                Int32 rowsEffected = oDBEngine.SetFieldValue("tbl_master_employee", value, " emp_contactid ='" + HttpContext.Current.Session["KeyVal_InternalID"] + "'");
                //Int32 RowsEffect = oDBEngine.SetFieldValue("tbl_trans_employeeCTC", "emp_effectiveuntil='" + cmbDOL.Value + "',LastModifyDate='" + oDBEngine.GetDate().ToString() + "',LastModifyUser='" + Session["userid"].ToString() + "'", " emp_cntId='" + HttpContext.Current.Session["KeyVal_InternalID"] + "'");
                if (rowsEffected > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CTC", "ForCTC();", true);
                    //DateTime dtDob;
                    //dtDob = Convert.ToDateTime(cmbDOJ.Value);
                    //JoiningDate.Date = dtDob;
                }

                if (Session["KeyVal_InternalID"] != "n")
                {
                    DataTable DT_empCTC = oDBEngine.GetDataTable(" tbl_trans_employeeCTC ", " emp_id ", " emp_cntId='" + Convert.ToString(Session["KeyVal_InternalID"]) + "'");
                    if (DT_empCTC.Rows.Count > 0)
                        JoiningDate.Value = oDBEngine.GetDate();
                    else
                    {
                        DataTable dt = oDBEngine.GetDataTable(" tbl_master_employee", "emp_dateofJoining", " (emp_contactId = '" + Convert.ToString(Session["KeyVal_InternalID"]) + "')");
                        if (dt.Rows.Count > 0)
                            JoiningDate.Value = dt.Rows[0][0];

                    }
                }
            }

        }
        protected void btnCTC_Click(object sender, EventArgs e)//3rd
        {
            try
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "GeneralLoading", "startLoading();", true);
                // ------------ Check IF company Schema Exists -------------
                // Mantis Issue 24655
                // Mantis Issue 25148
                //if (calledFromChannelLookup_hidden.Value == "1" || calledFromCircleLookup_hidden.Value == "1" || calledFromSectionLookup_hidden.Value == "1" ||
                //    calledFromColleagueLookup_hidden.Value == "1" || calledFromColleague1Lookup_hidden.Value=="1" ||
                //    calledFromColleague2Lookup_hidden.Value == "1" || calledFromAReportHeadLookup_hidden.Value=="1" )
                //{
                if ( (IsChannelCircleSectionMandatory.Value == "0" && 
                            (calledFromChannelLookup_hidden.Value == "1" || calledFromCircleLookup_hidden.Value == "1" || calledFromSectionLookup_hidden.Value == "1")) ||
                    calledFromColleagueLookup_hidden.Value == "1" || calledFromColleague1Lookup_hidden.Value == "1" ||
                    calledFromColleague2Lookup_hidden.Value == "1" || calledFromAReportHeadLookup_hidden.Value == "1")
                {
                    // End of Mantis Issue 25148
                    calledFromChannelLookup_hidden.Value = "0";
                    calledFromCircleLookup_hidden.Value = "0";
                    calledFromSectionLookup_hidden.Value = "0";
                    calledFromColleagueLookup_hidden.Value = "0";
                    calledFromColleague1Lookup_hidden.Value = "0";
                    calledFromColleague2Lookup_hidden.Value = "0";
                    calledFromAReportHeadLookup_hidden.Value = "0";


                    ScriptManager.RegisterStartupScript(this, this.GetType(), "GeneralLoading", "stopLoading();", true);
                    return;
                }
                // End of Mantis Issue 24655
                DataTable dtS = new DataTable();
                dtS = oDBEngine.GetDataTable("tbl_master_company", "onrole_schema_id, offrole_schema_id", "cmp_id=" + cmbOrganization.SelectedValue + "");

                if (dtS.Rows[0]["onrole_schema_id"].ToString() == "" || dtS.Rows[0]["offrole_schema_id"].ToString() == "")
                {
                    // ------------ Check IF company Schema Exists -------------
                    string alert = "'Please define Company On & Off Role Schema for " + cmbOrganization.SelectedItem.Text + "'";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "General", "jAlert(" + alert + ");", true);
                    return;
                }
                else if (!checkNMakeEmpCode(Convert.ToInt32(cmbOrganization.SelectedValue)))
                {
                    // ------------ Check for different validation ---------------
                    return;
                }
                // Mantis Issue 24655
                String ChannelType = txtChannel_hidden.Value.ToString();
                String Circle = txtCircle_hidden.Value.ToString();
                String Section = txtSection_hidden.Value.ToString();
                
                String DefaultType = "";
                if (chkChannelDefault.Checked == true){
                    DefaultType = "Channel";
                }
                else if (chkCircleDefault.Checked==true){
                    DefaultType = "Circle";
                }
                else if (chkSectionDefault.Checked == true){
                    DefaultType = "Section";
                }
                // End of Mantis Issue 24655
                
                //=======================For naming Part / 1st part ========================================
                Employee_BL objEmployee = new Employee_BL();
                bool chkAllow = false;
                //Tire architecture
                // Mantis Issue 24655 [ paarmeter ChannelType, Circle and Section  added. Also ChannelDefault, CircleDefault and SectionDefault added ]
                string InternalID = objEmployee.btnSave_Click_BL(Convert.ToString(DBNull.Value), CmbSalutation.SelectedItem.Value, txtFirstNmae.Text,
                txtMiddleName.Text, txtLastName.Text, txtAliasName.Text,
                //REV 2.0
                //"0",
                cmbBranch.SelectedItem.Value,
                //REV 2.0 END
                cmbGender.SelectedItem.Value, Convert.ToString(DBNull.Value), Convert.ToString(DBNull.Value), Convert.ToString(DBNull.Value),
                Convert.ToString(DBNull.Value), Convert.ToString(DBNull.Value), Convert.ToString(DBNull.Value), Convert.ToString(DBNull.Value), Convert.ToString(DBNull.Value),
                chkAllow, Convert.ToString(DBNull.Value), Convert.ToString(ChannelType), Convert.ToString(Circle), Convert.ToString(Section),
                DefaultType);

                /// Coded By Samrat Roy -- 18/04/2017 
                /// To Insert Contact Type selection on Employee Type (DME/ISD)
                //if (EmpType.SelectedValue == "21" || EmpType.SelectedValue == "19")
                if (EmpType.SelectedValue == "19")
                {
                    string contactPrefix = string.Empty;
                    switch (EmpType.SelectedValue)
                    {
                        //case "21":
                        //    contactPrefix = "FI";
                        //    break;
                        case "19":
                            contactPrefix = "DV";
                            break;

                    }
                    Employee_AddNew_BL objEmployee_AddNew_BL = new Employee_AddNew_BL();
                    string contactID = Request.Form["ctl00$ContentPlaceHolder1$ContactType"].ToString();
                    if (!string.IsNullOrEmpty(contactID))
                    {
                        objEmployee_AddNew_BL.InsertContactType_BL(InternalID, contactID, contactPrefix);
                    }
                }
                ///////###################################################//////

                HttpContext.Current.Session["KeyVal_InternalID"] = InternalID;
                string[,] cnt_id = oDBEngine.GetFieldValue(" tbl_master_contact", " cnt_id", " cnt_internalId='" + InternalID + "'", 1);
                if (cnt_id[0, 0].ToString() != "n")
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "General", "ForJoin();", true);

                    //===============================For join Date/ 2nd Part=====================================
                    oGenericMethod = new BusinessLogicLayer.GenericMethod();
                    string value = string.Empty;
                    //Checking For Expiry Date 
                    string ValidationResult = oGenericMethod.IsProductExpired(Convert.ToDateTime(cmbDOJ.Value));
                    if (Convert.ToBoolean(ValidationResult.Split('~')[0]))
                    {
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Vscript", "jAlert('" + ValidationResult.Split('~')[1] + "');", true);
                    }
                    else
                    {
                        value = "emp_din=' ', emp_dateofJoining ='" + cmbDOJ.Value + "', emp_dateofLeaving ='" + DBNull.Value + "',emp_ReasonLeaving  ='" + DBNull.Value + "', emp_NextEmployer ='" + DBNull.Value + "', emp_AddNextEmployer  ='" + DBNull.Value + "',LastModifyDate='" + oDBEngine.GetDate().ToString() + "',LastModifyUser='" + Session["userid"].ToString() + "'";
                        Int32 rowsEffected = oDBEngine.SetFieldValue("tbl_master_employee", value, " emp_contactid ='" + HttpContext.Current.Session["KeyVal_InternalID"] + "'");

                        if (Session["KeyVal_InternalID"] != "n")
                        {
                            DataTable DT_empCTC = oDBEngine.GetDataTable(" tbl_trans_employeeCTC ", " emp_id ", " emp_cntId='" + Convert.ToString(Session["KeyVal_InternalID"]) + "'");
                            if (DT_empCTC.Rows.Count > 0)
                                JoiningDate.Value = oDBEngine.GetDate();
                            else
                            {
                                DataTable dt = oDBEngine.GetDataTable(" tbl_master_employee", "emp_dateofJoining", " (emp_contactId = '" + Convert.ToString(Session["KeyVal_InternalID"]) + "')");
                                if (dt.Rows.Count > 0)
                                    JoiningDate.Value = dt.Rows[0][0];
                            }
                        }

                        if (rowsEffected > 0)
                        {
                            Employee_BL objEmployee_BL = new Employee_BL();

                            string emp_cntId = Convert.ToString(HttpContext.Current.Session["KeyVal_InternalID"]);
                            CntID = Convert.ToString(HttpContext.Current.Session["KeyVal_InternalID"]);
                            string joiningDate = string.Empty;
                            string emp_LeaveSchemeAppliedFrom = string.Empty;
                            if (JoiningDate.Value != null)
                            {
                                joiningDate = JoiningDate.Value.ToString();
                            }
                            else
                            {
                                joiningDate = "";
                            }

                            if (cmbLeaveEff.Value != null)
                            {
                                emp_LeaveSchemeAppliedFrom = cmbLeaveEff.Value.ToString();
                            }
                            else
                            {
                                emp_LeaveSchemeAppliedFrom = "";
                            }

                            // Mantis Issue 24655
                            // Mantis Issue 24983
                            //string ReportHead = txtAReportHead_hidden.Value;
                            //if (ReportHead == "")
                            //{
                            //    ReportHead = txtReportTo_hidden.Value;
                            //}

                            //string Colleague = txtColleague_hidden.Value;
                            //if (Colleague == "")
                            //{
                            //    Colleague = txtReportTo_hidden.Value;
                            //}

                            string ReportHead = txtAReportHead_hidden.Value;
                            if (ReportHead == "")
                            {
                                ReportHead = "0";
                            }

                            string Colleague = txtColleague_hidden.Value;
                            if (Colleague == "")
                            {
                                Colleague = "0";
                            }
                            // End of Mantis Issue 24983

                            string Colleague1 = txtColleague1_hidden.Value;
                            if (Colleague1 == "")
                            {
                                Colleague1 = "0";
                            }

                            string Colleague2 = txtColleague2_hidden.Value;
                            if (Colleague2 == "")
                            {
                                Colleague2 = "0";
                            }
                            // End of Mantis Issue 24655

                            // Tire Architecture 
                            // Mantis Issue 24655
                            //objEmployee_BL.btnCTC_Click_BL(emp_cntId, joiningDate,
                            //cmbOrganization.SelectedItem.Value, cmbJobResponse.SelectedItem.Value, cmbDesg.SelectedItem.Value, EmpType.SelectedItem.Value,
                            //cmbDept.SelectedItem.Value, txtReportTo_hidden.Value, txtReportTo_hidden.Value, txtReportTo_hidden.Value,
                            //cmbWorkingHr.SelectedItem.Value, cmbLeaveP.SelectedItem.Value, emp_LeaveSchemeAppliedFrom, cmbBranch.SelectedItem.Value, 
                            //Convert.ToString(DBNull.Value));
                            objEmployee_BL.btnCTC_Click_BL(emp_cntId, joiningDate,
                            cmbOrganization.SelectedItem.Value, cmbJobResponse.SelectedItem.Value, cmbDesg.SelectedItem.Value, EmpType.SelectedItem.Value,
                            cmbDept.SelectedItem.Value, txtReportTo_hidden.Value, ReportHead, Colleague,
                            cmbWorkingHr.SelectedItem.Value, cmbLeaveP.SelectedItem.Value, emp_LeaveSchemeAppliedFrom, cmbBranch.SelectedItem.Value,
                            Convert.ToString(DBNull.Value), Colleague1, Colleague2);
                            // End of Mantis Issue 24655

                            // Generate and save employee id
                            // added by Aditya 12-Dec-2016
                            oDBEngine.SetFieldValue("tbl_master_contact", "Cnt_UCC ='" + empCompCode.ToString() + "'", " cnt_internalid ='" + emp_cntId + "'");
                            oDBEngine.SetFieldValue("tbl_master_employee", "emp_uniquecode='" + empCompCode.ToString() + "'", "emp_contactID='" + emp_cntId + "'");
                            txtAliasName.Text = empCompCode.ToString();

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "GeneralLoading", "stopLoading();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "EMP", "ForEMPID();", true);
                        }
                    }
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "General", "jAlert('Please Try Again!..');", true);
            }
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "General", "ForJoin();", true);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "GeneralLoading", "stopLoading();", true);
        }


        #region checkNMakeEmpCode BackUp
        //protected bool checkNMakeEmpCode(int cmp_Id)
        //{
        //    DataTable dtS = new DataTable();
        //    DataTable dtSchemaOn = new DataTable();
        //    DataTable dtSchemaOff = new DataTable();
        //    DataTable dtCTC = new DataTable();
        //    DataTable dtC = new DataTable();
        //    string prefCompCode = string.Empty, sufxCompCode = string.Empty, ShortName = string.Empty, TempCode = string.Empty,
        //        startNo, paddedStr;
        //    int EmpCode, prefLen, sufxLen, paddCounter;

        //    //string emp_cntId = Convert.ToString(HttpContext.Current.Session["KeyVal_InternalID"]);
        //    //dtCTC = oDBEngine.GetDataTable("tbl_trans_employeeCTC", " *", "emp_cntID='" + emp_cntId + "'");
        //    dtS = oDBEngine.GetDataTable("tbl_master_company", "onrole_schema_id, offrole_schema_id", "cmp_id=" + cmp_Id + "");

        //    if (dtS.Rows[0]["onrole_schema_id"].ToString() != "" && dtS.Rows[0]["offrole_schema_id"].ToString() != "")
        //    {
        //        dtSchemaOn = oDBEngine.GetDataTable("tbl_master_idschema", "prefix, suffix, digit, startno", "id=" + Convert.ToInt64(dtS.Rows[0]["onrole_schema_id"]));
        //        dtSchemaOff = oDBEngine.GetDataTable("tbl_master_idschema", "prefix, suffix, digit, startno", "id=" + Convert.ToInt64(dtS.Rows[0]["offrole_schema_id"]));

        //        if (Convert.ToInt32(EmpType.SelectedValue) > 0)
        //        {
        //            if (Convert.ToInt32(EmpType.SelectedValue) == 1 || Convert.ToInt32(EmpType.SelectedValue) == 2)
        //            {
        //                startNo = dtSchemaOn.Rows[0]["startno"].ToString();
        //                paddCounter = Convert.ToInt32(dtSchemaOn.Rows[0]["digit"]);
        //                paddedStr = startNo.PadLeft(paddCounter, '0');
        //                prefCompCode = dtSchemaOn.Rows[0]["prefix"].ToString();
        //                sufxCompCode = dtSchemaOn.Rows[0]["suffix"].ToString();
        //            }
        //            else
        //            {
        //                startNo = dtSchemaOff.Rows[0]["startno"].ToString();
        //                paddCounter = Convert.ToInt32(dtSchemaOff.Rows[0]["digit"]);
        //                prefCompCode = dtSchemaOff.Rows[0]["prefix"].ToString();
        //                sufxCompCode = dtSchemaOff.Rows[0]["suffix"].ToString();
        //            }
        //            prefLen = Convert.ToInt32(prefCompCode.Length);
        //            sufxLen = Convert.ToInt32(sufxCompCode.Length);

        //            string sql_statement = "SELECT max(tmc.Cnt_UCC) FROM tbl_master_contact tmc WHERE tmc.cnt_internalId IN(SELECT ttectc1.emp_cntId FROM tbl_trans_employeeCTC ttectc1 WHERE ttectc1.emp_Organization = " + cmp_Id + ") AND dbo.RegexMatch('";
        //            if (prefCompCode.Length > 0) sql_statement += "[a-zA-Z0-9-/\\[\\](){}]{" + prefLen + "}";
        //            if (startNo.Length > 0) sql_statement += "[0-9]{" + paddCounter + "}";
        //            if (sufxCompCode.Length > 0) sql_statement += "[a-zA-Z0-9-/\\[\\](){}]{" + sufxLen + "}";
        //            sql_statement += "?$', tmc.cnt_UCC) = 1 AND tmc.cnt_internalid like 'EM%'";
        //            dtC = oDBEngine.GetDataTable(sql_statement);

        //            if (dtC.Rows[0][0].ToString() == "")
        //            {
        //                sql_statement = "SELECT max(tmc.Cnt_UCC) FROM tbl_master_contact tmc WHERE tmc.cnt_internalId IN(SELECT ttectc1.emp_cntId FROM tbl_trans_employeeCTC ttectc1 WHERE ttectc1.emp_Organization = " + cmp_Id + ") AND dbo.RegexMatch('";
        //                if (prefCompCode.Length > 0) sql_statement += "[a-zA-Z0-9-/\\[\\](){}]{" + prefLen + "}";
        //                if (startNo.Length > 0) sql_statement += "[0-9]{" + (paddCounter - 1) + "}";
        //                if (sufxCompCode.Length > 0) sql_statement += "[a-zA-Z0-9-/\\[\\](){}]{" + sufxLen + "}";
        //                sql_statement += "?$', tmc.cnt_UCC) = 1 AND tmc.cnt_internalid like 'EM%'";
        //                dtC = oDBEngine.GetDataTable(sql_statement);
        //            }

        //            if (dtC.Rows.Count > 0 && dtC.Rows[0][0].ToString() != "")
        //            {
        //                string uccCode = dtC.Rows[0][0].ToString();
        //                int UCCLen = uccCode.Length;
        //                int decimalPartLen = UCCLen - (prefCompCode.Length + sufxCompCode.Length);
        //                string uccCodeSubstring = uccCode.Substring(prefCompCode.Length, decimalPartLen);
        //                EmpCode = Convert.ToInt32(uccCodeSubstring) + 1;

        //                if (EmpCode.ToString().Length > paddCounter)
        //                {
        //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "General", "jAlert('Cannot Add More Employees as Schema Exausted for Current Employee Role. <br />Please Update Employee Schema.');", true);
        //                    return false;
        //                }
        //                else
        //                {
        //                    paddedStr = EmpCode.ToString().PadLeft(paddCounter, '0');
        //                    empCompCode = prefCompCode + paddedStr + sufxCompCode;
        //                    return true;
        //                }
        //            }
        //            else
        //            {
        //                paddedStr = startNo.PadLeft(paddCounter, '0');
        //                empCompCode = prefCompCode + paddedStr + sufxCompCode;
        //                return true;
        //            }
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "JSct", "jAlert('Employee Type Not Found !);", true);
        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "JSct55", "jAlert('Company On / Off Role Schema Not Defined !');", true);
        //        return false;
        //    }
        //}

#endregion

         protected bool checkNMakeEmpCode(int cmp_Id)
        {
            DataTable dtS = new DataTable();
            DataTable dtSchemaOn = new DataTable();
            DataTable dtSchemaOff = new DataTable();
            DataTable dtCTC = new DataTable();
            DataTable dtC = new DataTable();
            string prefCompCode = string.Empty, sufxCompCode = string.Empty, ShortName = string.Empty, TempCode = string.Empty,
                startNo, paddedStr;
            int EmpCode, prefLen, sufxLen, paddCounter;

            //string emp_cntId = Convert.ToString(HttpContext.Current.Session["KeyVal_InternalID"]);
            //dtCTC = oDBEngine.GetDataTable("tbl_trans_employeeCTC", " *", "emp_cntID='" + emp_cntId + "'");
            dtS = oDBEngine.GetDataTable("tbl_master_company", "onrole_schema_id, offrole_schema_id", "cmp_id=" + cmp_Id + "");

            if (dtS.Rows[0]["onrole_schema_id"].ToString() != "" && dtS.Rows[0]["offrole_schema_id"].ToString() != "")
            {
                dtSchemaOn = oDBEngine.GetDataTable("tbl_master_idschema", "prefix, suffix, digit, startno", "id=" + Convert.ToInt64(dtS.Rows[0]["onrole_schema_id"]));
                dtSchemaOff = oDBEngine.GetDataTable("tbl_master_idschema", "prefix, suffix, digit, startno", "id=" + Convert.ToInt64(dtS.Rows[0]["offrole_schema_id"]));

                if (Convert.ToInt32(EmpType.SelectedValue) > 0)
                {
                    if (Convert.ToInt32(EmpType.SelectedValue) == 1 || Convert.ToInt32(EmpType.SelectedValue) == 2)
                    {
                        startNo = dtSchemaOn.Rows[0]["startno"].ToString();
                        paddCounter = Convert.ToInt32(dtSchemaOn.Rows[0]["digit"]);
                        paddedStr = startNo.PadLeft(paddCounter, '0');
                        prefCompCode = dtSchemaOn.Rows[0]["prefix"].ToString();
                        sufxCompCode = dtSchemaOn.Rows[0]["suffix"].ToString();
                    }
                    else
                    {
                        startNo = dtSchemaOff.Rows[0]["startno"].ToString();
                        paddCounter = Convert.ToInt32(dtSchemaOff.Rows[0]["digit"]);
                        prefCompCode = dtSchemaOff.Rows[0]["prefix"].ToString();
                        sufxCompCode = dtSchemaOff.Rows[0]["suffix"].ToString();
                    }
                    prefLen = Convert.ToInt32(prefCompCode.Length);
                    sufxLen = Convert.ToInt32(sufxCompCode.Length);

                    // Mantis Issue 24450,24451
                    //string sql_statement = "SELECT max(tmc.Cnt_UCC) FROM tbl_master_contact tmc WHERE tmc.cnt_internalId IN(SELECT ttectc1.emp_cntId FROM tbl_trans_employeeCTC ttectc1 WHERE ttectc1.emp_Organization = " + cmp_Id + ") ";
                    ////AND dbo.RegexMatch('";
                    ////if (prefCompCode.Length > 0) sql_statement += "[" + prefCompCode + "]{" + prefLen + "}"; //"^" + prefCompCode;
                    ////if (startNo.Length > 0) sql_statement += "[0-9]{" + paddCounter + "}";
                    ////if (sufxCompCode.Length > 0) sql_statement += "[" + sufxCompCode + "]{" + sufxLen + "}";//"^" + sufxCompCode;
                    ////sql_statement += "?$', tmc.cnt_UCC) = 1 
                    //sql_statement += " AND tmc.cnt_internalid like 'EM%' and tmc.cnt_UCC like '%DEMOP%' ";
                    //dtC = oDBEngine.GetDataTable(sql_statement);

                    //if (dtC.Rows[0][0].ToString() == "")
                    //{
                    //    sql_statement = "SELECT max(tmc.Cnt_UCC) FROM tbl_master_contact tmc WHERE tmc.cnt_internalId IN(SELECT ttectc1.emp_cntId FROM tbl_trans_employeeCTC ttectc1 WHERE ttectc1.emp_Organization = " + cmp_Id + ") ";
                    //    //AND dbo.RegexMatch('";
                    //    //if (prefCompCode.Length > 0) sql_statement += "[" + prefCompCode + "]{" + prefLen + "}"; //"^" + prefCompCode;
                    //    //if (startNo.Length > 0) sql_statement += "[0-9]{" + (paddCounter - 1) + "}";
                    //    //if (sufxCompCode.Length > 0) sql_statement += "[" + sufxCompCode + "]{" + sufxLen + "}";//"^" + sufxCompCode;
                    //    //sql_statement += "?$', tmc.cnt_UCC) = 1 
                    //    sql_statement += " AND tmc.cnt_internalid like 'EM%'  and tmc.cnt_UCC like '%DEMOP%' ";
                    //    dtC = oDBEngine.GetDataTable(sql_statement);
                    //}

                    // Mantis Issue 24655 [ Optimization done ]
                    //string sql_statement = "SELECT max(tmc.Cnt_UCC) FROM tbl_master_contact tmc WHERE tmc.cnt_internalId IN(SELECT ttectc1.emp_cntId FROM tbl_trans_employeeCTC ttectc1 WHERE ttectc1.emp_Organization = " + cmp_Id + ") AND dbo.RegexMatch('";
                    string sql_statement = "SELECT max(tmc.Cnt_UCC) FROM tbl_master_contact tmc WHERE tmc.cnt_internalId IN(SELECT ttectc1.emp_cntId FROM tbl_trans_employeeCTC ttectc1 WHERE ttectc1.emp_Organization = " + cmp_Id + ") ";
                    if (prefCompCode.Length > 0)
                        sql_statement += " and tmc.cnt_UCC like '" + prefCompCode + "%' ";
                    sql_statement += " AND dbo.RegexMatch('";
                    // End of Mantis Issue 24655
                    if (prefCompCode.Length > 0) sql_statement += "[" + prefCompCode + "]{" + prefLen + "}"; //"^" + prefCompCode;
                    if (startNo.Length > 0) sql_statement += "[0-9]{" + paddCounter + "}";
                    if (sufxCompCode.Length > 0) sql_statement += "[" + sufxCompCode + "]{" + sufxLen + "}";//"^" + sufxCompCode;
                    sql_statement += "?$', tmc.cnt_UCC) = 1 AND tmc.cnt_internalid like 'EM%'";
                    dtC = oDBEngine.GetDataTable(sql_statement);
                    // End of Mantis Issue 24450,24451

                    if (dtC.Rows.Count > 0 && dtC.Rows[0][0].ToString() != "")
                    {
                        string uccCode = dtC.Rows[0][0].ToString();
                        int UCCLen = uccCode.Length;
                        int decimalPartLen = UCCLen - (prefCompCode.Length + sufxCompCode.Length);
                        string uccCodeSubstring = uccCode.Substring(prefCompCode.Length, decimalPartLen);
                        EmpCode = Convert.ToInt32(uccCodeSubstring) + 1;

                        if (EmpCode.ToString().Length > paddCounter)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "General", "jAlert('Cannot Add More Employees as Schema Exausted for Current Employee Role. <br />Please Update Employee Schema.');", true);
                            return false;
                        }
                        else
                        {
                            paddedStr = EmpCode.ToString().PadLeft(paddCounter, '0');
                            empCompCode = prefCompCode + paddedStr + sufxCompCode;
                            return true;
                        }
                    }
                    else
                    {
                        paddedStr = startNo.PadLeft(paddCounter, '0');
                        empCompCode = prefCompCode + paddedStr + sufxCompCode;
                        return true;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "JSct", "jAlert('Employee Type Not Found !);", true);
                    return false;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "JSct55", "jAlert('Company On / Off Role Schema Not Defined !');", true);
                return false;
            }
        }






        protected void btnEmpID_Click(object sender, EventArgs e)
        {
            if (txtAliasName.Text.ToString() != "")
            {
                /*DataTable dt = oDBEngine.GetDataTable("tbl_master_contact", " cnt_ucc ", " cnt_internalid !='" + Session["KeyVal_InternalID"] + 
                    "' and (cnt_ucc like '" + txtAliasName.Text.ToString().Trim() + "' or cnt_ShortName like  '" + txtAliasName.Text.ToString().Trim() + "')");*/

                DataTable dt = oDBEngine.GetDataTable("SELECT cnt_id FROM tbl_master_contact WHERE cnt_UCC = '" + txtAliasName.Text.ToString().Trim() +
                    "' AND cnt_internalId != '" + Session["KeyVal_InternalID"] + "' AND cnt_internalId IN(SELECT emp_cntId FROM tbl_trans_employeeCTC " +
                    " WHERE emp_Organization IN (SELECT tte.emp_Organization FROM tbl_trans_employeeCTC tte WHERE emp_cntId = '" + Session["KeyVal_InternalID"] + "'))");

                if (dt.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert12", "jAlert('Employee Code already Exists!..');", true);
                }
                else
                {
                    // Mantis Issue 24736 [ txtOtherID added ]
                    oDBEngine.SetFieldValue("tbl_master_employee", "emp_uniquecode='" + txtAliasName.Text.ToString() + "', cnt_OtherID = '" + txtOtherID.Text.ToString() + "'", "emp_contactID='" + Session["KeyVal_InternalID"] + "'");
                    oDBEngine.SetFieldValue("tbl_master_contact", "cnt_ucc='" + txtAliasName.Text.ToString() + "',cnt_ShortName='" + txtAliasName.Text.ToString() + "', cnt_OtherID = '" + txtOtherID.Text.ToString() + "'", "cnt_internalid='" + Session["KeyVal_InternalID"] + "'");
                    DataTable dtchk = oDBEngine.GetDataTable("tbl_master_contact", " cnt_id ", "cnt_internalid='" + Session["KeyVal_InternalID"] + "'");
                    ScriptManager.RegisterStartupScript(this, GetType(), "JScript", "window.location ='employee_general.aspx?id=" + Convert.ToInt32(dtchk.Rows[0][0].ToString()) + "';", true);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "General", "jAlert('Employee ID can not be blank');", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "CLOSE122", "editwin.close();", true);
            }
        }

        private void ShowForm()
        {
            //Rev work start 26.04.2022 0024853: Copy feature add in Employee master
            if (Request.QueryString["id"] == "ADD")
            {
                //Rev work close 26.04.2022 0024853: Copy feature add in Employee master
                cmbDOJ.EditFormatString = Oconverter.GetDateFormat();
                string[,] Data = oDBEngine.GetFieldValue("tbl_master_salutation", "sal_id, sal_name", null, 2, "sal_name");
                //oDBEngine.AddDataToDropDownList(Data, CmbSalutation);
                oclsDropDownList.AddDataToDropDownList(Data, CmbSalutation);
                CmbSalutation.SelectedValue = "1";
                Data = oDBEngine.GetFieldValue("tbl_master_Company", "cmp_id, cmp_name", null, 2, "cmp_name");
                //oDBEngine.AddDataToDropDownList(Data, cmbOrganization);
                oclsDropDownList.AddDataToDropDownList(Data, cmbOrganization);
                Data = oDBEngine.GetFieldValue("tbl_master_jobresponsibility", "job_id, job_responsibility", null, 2, "job_responsibility");
                //oDBEngine.AddDataToDropDownList(Data, cmbJobResponse);
                oclsDropDownList.AddDataToDropDownList(Data, cmbJobResponse);
                Data = oDBEngine.GetFieldValue("tbl_master_branch", "branch_id, branch_description ", null, 2, "branch_description");
                //oDBEngine.AddDataToDropDownList(Data, cmbBranch);
                oclsDropDownList.AddDataToDropDownList(Data, cmbBranch);
                Data = oDBEngine.GetFieldValue("tbl_master_Designation", "deg_id, deg_designation ", null, 2, "deg_designation");
                //oDBEngine.AddDataToDropDownList(Data, cmbDesg);
                oclsDropDownList.AddDataToDropDownList(Data, cmbDesg);
                Data = oDBEngine.GetFieldValue("tbl_master_employeeType", "emptpy_id, emptpy_type ", null, 2, "emptpy_type");
                //oDBEngine.AddDataToDropDownList(Data, EmpType);
                oclsDropDownList.AddDataToDropDownList(Data, EmpType);
                Data = oDBEngine.GetFieldValue("tbl_master_costCenter", "cost_id, cost_description ", " cost_costCenterType = 'department' ", 2, "cost_description");
                //oDBEngine.AddDataToDropDownList(Data, cmbDept);
                oclsDropDownList.AddDataToDropDownList(Data, cmbDept);



                //Data = oDBEngine.GetFieldValue("tbl_master_workingHours", "wor_id,wor_scheduleName  ", null, 2, "wor_scheduleName");
                //oclsDropDownList.AddDataToDropDownList(Data, cmbWorkingHr);


                Data = oDBEngine.GetFieldValue("tbl_EmpWorkingHours", "Id,Name  ", null, 2, "Name");
                oclsDropDownList.AddDataToDropDownList(Data, cmbWorkingHr);

                Data = oDBEngine.GetFieldValue("tbl_master_LeaveScheme", "ls_id, ls_name  ", null, 2, "ls_name");
                //oDBEngine.AddDataToDropDownList(Data, cmbLeaveP);
                oclsDropDownList.AddDataToDropDownList(Data, cmbLeaveP);


                cmbOrganization.Items.Insert(0, new ListItem("--Select--", "0"));
                cmbJobResponse.Items.Insert(0, new ListItem("--Select--", "0"));
                cmbBranch.Items.Insert(0, new ListItem("--Select--", "0"));
                cmbDesg.Items.Insert(0, new ListItem("--Select--", "0"));
                EmpType.Items.Insert(0, new ListItem("--Select--", "0"));
                cmbDept.Items.Insert(0, new ListItem("--Select--", "0"));
                cmbWorkingHr.Items.Insert(0, new ListItem("--Select--", "0"));
                cmbLeaveP.Items.Insert(0, new ListItem("--Select--", "0"));
                //Rev work start 26.04.2022 0024853: Copy feature add in Employee master
            }
            else
            {
                int Unqid_empCTC = 0;
                string[,] emp_id;

                //TrLang.Visible = true;
                if (Request.QueryString["id"] != null && Convert.ToString(Request.QueryString["id"]) != "")
                {
                    ID = Int32.Parse(Request.QueryString["id"]);
                    HttpContext.Current.Session["KeyVal"] = ID;
                }
                string[,] InternalId;

                if (ID != 0)
                {
                    InternalId = oDBEngine.GetFieldValue("tbl_master_contact", "cnt_internalId", "cnt_id=" + ID, 1);                  
                }
                else
                {
                    InternalId = oDBEngine.GetFieldValue("tbl_master_contact", "cnt_internalId", "cnt_id=" + HttpContext.Current.Session["KeyVal"], 1);
                }
                if (InternalId[0, 0].Trim().StartsWith("EM"))
                {
                    // txtAliasName.Enabled = false;
                    LinkButton1.Enabled = false;
                }
                else
                {
                    // txtAliasName.Enabled = true;
                    LinkButton1.Enabled = true;
                }               
                // if emp code is  system generated txtAliasName should be un editable #ag18102016k
                HttpContext.Current.Session["KeyVal_InternalID"] = InternalId[0, 0];
                emp_id = oDBEngine.GetFieldValue("tbl_trans_employeeCTC", "emp_id", "emp_cntId='" + Convert.ToString(HttpContext.Current.Session["KeyVal_InternalID"]) + "'", 1);
                Unqid_empCTC = Convert.ToInt32(emp_id[0,0]);
                //LanGuage();
                string[,] ContactData;
                string[,] EmployeeDate;
                string[,] CTCData;
                string[,] ReportData;
                
                if (ID != 0)
                {
                    ContactData = oDBEngine.GetFieldValue("tbl_master_contact",
                                            "cnt_ucc, cnt_salutation,  cnt_firstName, cnt_middleName, cnt_lastName, cnt_shortName, cnt_branchId, cnt_sex, cnt_maritalStatus, cnt_DOB, cnt_anniversaryDate, cnt_legalStatus, cnt_education, cnt_profession, cnt_organization, cnt_jobResponsibility, cnt_designation, cnt_industry, cnt_contactSource, cnt_referedBy, cnt_contactType, cnt_contactStatus,cnt_bloodgroup,WebLogIn,cnt_internalId,cnt_OtherID",
                                            " cnt_id=" + ID, 26);
                    CTCData = oDBEngine.GetFieldValue("tbl_trans_employeeCTC",
                                           "emp_Organization, emp_JobResponsibility,  emp_Designation, emp_Department, emp_workinghours, emp_totalLeavePA ,emp_type,emp_reportTo",
                                           " emp_cntId='" + HttpContext.Current.Session["KeyVal_InternalID"] + "'", 8);
                    ReportData = oDBEngine.GetFieldValue("tbl_trans_employeeCTC A LEFT JOIN tbl_master_designation ON A.emp_Designation = tbl_master_designation.deg_id LEFT JOIN FTS_Employee_Grade as grad ON A.Emp_Grade  = grad.Id LEFT JOIN tbl_master_costCenter ON A.emp_Department = tbl_master_costCenter.cost_id LEFT JOIN tbl_master_company ON A.emp_Organization = tbl_master_company.cmp_id  LEFT JOIN tbl_master_employee ON A.emp_cntId = tbl_master_employee.emp_contactId ",
                               "A.emp_effectiveDate AS EffectiveDate,convert(varchar(11),A.emp_effectiveDate,113) AS EffectiveDate1,convert(varchar(11),A.emp_effectiveuntil,113) AS EffectiveUntil1, A.emp_effectiveuntil AS EffectiveUntil, A.emp_id AS Id, tbl_master_designation.deg_designation AS Designation, tbl_master_costCenter.cost_description AS Department, tbl_master_company.cmp_Name AS CompanyName, A.emp_id, A.emp_cntId, A.emp_effectiveDate as emp_dateofJoining, A.emp_effectiveuntil, A.emp_Organization, A.emp_JobResponsibility, A.emp_Designation, (select branch_description from tbl_master_branch where branch_id= A.emp_branch) as BranchName,	A.emp_branch,A.emp_LeaveSchemeAppliedFrom,A.emp_type, A.emp_Department, A.emp_reportTo, A.emp_deputy, A.emp_colleague, A.emp_workinghours, A.emp_currentCTC, A.emp_basic, A.emp_HRA, A.emp_CCA, A.emp_spAllowance, A.emp_childrenAllowance, A.emp_totalLeavePA, A.emp_PF, A.emp_medicalAllowance, A.emp_LTA, A.emp_convence, A.emp_mobilePhoneExp,A.EMP_CarAllowance, A.EMP_UniformAllowance,A.EMP_BooksPeriodicals ,A.EMP_SeminarAllowance ,A.EMP_OtherAllowance,A.emp_totalMedicalLeavePA, A.CreateDate, A.CreateUser, A.LastModifyDate, A.LastModifyUser,A.emp_Remarks,grad.Id,A.emp_colleague1,A.emp_colleague2",
                               " (A.emp_cntId = '" + Convert.ToString(HttpContext.Current.Session["KeyVal_InternalID"]) + "'and A.emp_id='" + Unqid_empCTC + "') ", 50);
                }
                else
                {
                    ContactData = oDBEngine.GetFieldValue("tbl_master_contact",
                                            "cnt_ucc, cnt_salutation,  cnt_firstName, cnt_middleName, cnt_lastName, cnt_shortName, cnt_branchId, cnt_sex, cnt_maritalStatus,cnt_DOB, cnt_anniversaryDate, cnt_legalStatus, cnt_education, cnt_profession, cnt_organization, cnt_jobResponsibility, cnt_designation, cnt_industry, cnt_contactSource, cnt_referedBy, cnt_contactType, cnt_contactStatus,cnt_bloodgroup,WebLogIn,cnt_internalId,cnt_OtherID",
                                            " cnt_id=" + HttpContext.Current.Session["KeyVal"], 26);
                    CTCData = oDBEngine.GetFieldValue("tbl_trans_employeeCTC",
                                            "emp_Organization, emp_JobResponsibility,  emp_Designation, emp_Department, emp_workinghours, emp_totalLeavePA,emp_type,emp_reportTo",
                                            " emp_cntId='" + HttpContext.Current.Session["KeyVal_InternalID"] + "'", 8);
                    CTCData = oDBEngine.GetFieldValue("tbl_trans_employeeCTC",
                                           "emp_Organization, emp_JobResponsibility,  emp_Designation, emp_Department, emp_workinghours, emp_totalLeavePA ,emp_type,emp_reportTo",
                                           " emp_cntId='" + HttpContext.Current.Session["KeyVal_InternalID"] + "'", 8);
                    ReportData = oDBEngine.GetFieldValue("tbl_trans_employeeCTC A LEFT JOIN tbl_master_designation ON A.emp_Designation = tbl_master_designation.deg_id LEFT JOIN FTS_Employee_Grade as grad ON A.Emp_Grade  = grad.Id LEFT JOIN tbl_master_costCenter ON A.emp_Department = tbl_master_costCenter.cost_id LEFT JOIN tbl_master_company ON A.emp_Organization = tbl_master_company.cmp_id  LEFT JOIN tbl_master_employee ON A.emp_cntId = tbl_master_employee.emp_contactId ",
                               "A.emp_effectiveDate AS EffectiveDate,convert(varchar(11),A.emp_effectiveDate,113) AS EffectiveDate1,convert(varchar(11),A.emp_effectiveuntil,113) AS EffectiveUntil1, A.emp_effectiveuntil AS EffectiveUntil, A.emp_id AS Id, tbl_master_designation.deg_designation AS Designation, tbl_master_costCenter.cost_description AS Department, tbl_master_company.cmp_Name AS CompanyName, A.emp_id, A.emp_cntId, A.emp_effectiveDate as emp_dateofJoining, A.emp_effectiveuntil, A.emp_Organization, A.emp_JobResponsibility, A.emp_Designation, (select branch_description from tbl_master_branch where branch_id= A.emp_branch) as BranchName,	A.emp_branch,A.emp_LeaveSchemeAppliedFrom,A.emp_type, A.emp_Department, A.emp_reportTo, A.emp_deputy, A.emp_colleague, A.emp_workinghours, A.emp_currentCTC, A.emp_basic, A.emp_HRA, A.emp_CCA, A.emp_spAllowance, A.emp_childrenAllowance, A.emp_totalLeavePA, A.emp_PF, A.emp_medicalAllowance, A.emp_LTA, A.emp_convence, A.emp_mobilePhoneExp,A.EMP_CarAllowance, A.EMP_UniformAllowance,A.EMP_BooksPeriodicals ,A.EMP_SeminarAllowance ,A.EMP_OtherAllowance,A.emp_totalMedicalLeavePA, A.CreateDate, A.CreateUser, A.LastModifyDate, A.LastModifyUser,A.emp_Remarks,grad.Id,A.emp_colleague1,A.emp_colleague2",
                               " (A.emp_cntId = '" + Convert.ToString(HttpContext.Current.Session["KeyVal_InternalID"]) + "'and A.emp_id='" + Unqid_empCTC + "') ", 50);
                }
                string[,] Data = oDBEngine.GetFieldValue("tbl_master_salutation", "sal_id, sal_name", null, 2);
                string[,] OrgData = oDBEngine.GetFieldValue("tbl_master_Company", "cmp_id, cmp_Name", null, 2);
                string[,] JobData = oDBEngine.GetFieldValue("tbl_master_jobresponsibility", "job_id, job_responsibility", null, 2, "job_responsibility");
                string[,] DesigData = oDBEngine.GetFieldValue("tbl_master_Designation", "deg_id, deg_designation ", null, 2, "deg_designation");
                string[,] DeptData = oDBEngine.GetFieldValue("tbl_master_costCenter", "cost_id, cost_description ", " cost_costCenterType = 'department' ", 2, "cost_description");
                string[,] WorkHrsData = oDBEngine.GetFieldValue("tbl_EmpWorkingHours", "Id,Name  ", null, 2, "Name");
                string[,] LeaveData = oDBEngine.GetFieldValue("tbl_master_LeaveScheme", "ls_id, ls_name  ", null, 2, "ls_name");
                string[,] EmpTypeData = oDBEngine.GetFieldValue("tbl_master_employeeType", "emptpy_id, emptpy_type ", null, 2, "emptpy_type");

                if (ContactData[0, 19] != "")
                {
                    // ddlReferedBy.SelectedIndex = ddlReferedBy.Items.IndexOf(ddlReferedBy.Items.FindByValue(ContactData[0, 19].Trim()));
                }
                if (ContactData[0, 1] != "")
                {
                    // oDBEngine.AddDataToDropDownList(Data, CmbSalutation, Int32.Parse(ContactData[0, 1]));
                    oclsDropDownList.AddDataToDropDownList(Data, CmbSalutation, Int32.Parse(ContactData[0, 1]));

                }
                else
                {

                    //oDBEngine.AddDataToDropDownList(Data, CmbSalutation, 0);
                    oclsDropDownList.AddDataToDropDownList(Data, CmbSalutation, 0);
                }

                txtFirstNmae.Text = ContactData[0, 2];
                txtMiddleName.Text = ContactData[0, 3];
                txtLastName.Text = ContactData[0, 4];
                //txtAliasName.Text = ContactData[0, 5];
                //cmbBloodgroup.SelectedValue = ContactData[0, 22];
                Data = oDBEngine.GetFieldValue("tbl_master_branch", "branch_id, branch_description ", null, 2);
                if (ContactData[0, 6] != "")
                {
                    //oDBEngine.AddDataToDropDownList(Data, cmbBranch, Int32.Parse(ContactData[0, 6]));
                    oclsDropDownList.AddDataToDropDownList(Data, cmbBranch, Int32.Parse(ContactData[0, 6]));
                }
                else
                {
                    //oDBEngine.AddDataToDropDownList(Data, cmbBranch, 0);
                    oclsDropDownList.AddDataToDropDownList(Data, cmbBranch, 0);
                }
                if (ContactData[0, 7] != "")
                {
                    cmbGender.SelectedValue = ContactData[0, 7];
                }
                else
                {
                    cmbGender.SelectedIndex.Equals(0);
                }
                if (CTCData[0, 0] != "")
                {
                    // oDBEngine.AddDataToDropDownList(Data, CmbSalutation, Int32.Parse(ContactData[0, 1]));
                    oclsDropDownList.AddDataToDropDownList(OrgData, cmbOrganization, Int32.Parse(CTCData[0, 0]));
                }
                else
                {
                    //oDBEngine.AddDataToDropDownList(Data, CmbSalutation, 0);
                    oclsDropDownList.AddDataToDropDownList(OrgData, cmbOrganization, 0);
                }
                if (CTCData[0, 1] != "")
                {
                    oclsDropDownList.AddDataToDropDownList(JobData, cmbJobResponse, Int32.Parse(CTCData[0, 1]));
                }
                else
                {
                    oclsDropDownList.AddDataToDropDownList(JobData, cmbJobResponse, 0);
                }
                if (CTCData[0, 2] != "")
                {
                    oclsDropDownList.AddDataToDropDownList(DesigData, cmbDesg, Int32.Parse(CTCData[0, 2]));
                }
                else
                {
                    oclsDropDownList.AddDataToDropDownList(DesigData, cmbDesg, 0);
                }
                if (CTCData[0, 3] != "")
                {
                    oclsDropDownList.AddDataToDropDownList(DeptData, cmbDept, Int32.Parse(CTCData[0, 3]));
                }
                else
                {
                    oclsDropDownList.AddDataToDropDownList(DeptData, cmbDept, 0);
                }
                if (CTCData[0, 4] != "")
                {
                    oclsDropDownList.AddDataToDropDownList(WorkHrsData, cmbWorkingHr, Int32.Parse(CTCData[0, 4]));
                }
                else
                {
                    oclsDropDownList.AddDataToDropDownList(WorkHrsData, cmbWorkingHr, 0);
                }
                if (CTCData[0, 5] != "")
                {
                    oclsDropDownList.AddDataToDropDownList(LeaveData, cmbLeaveP, Int32.Parse(CTCData[0, 5]));
                }
                else
                {
                    oclsDropDownList.AddDataToDropDownList(LeaveData, cmbLeaveP, 0);
                }
                if (CTCData[0, 6] != "")
                {
                    oclsDropDownList.AddDataToDropDownList(EmpTypeData, EmpType, Int32.Parse(CTCData[0, 6]));
                }
                else
                {
                    oclsDropDownList.AddDataToDropDownList(EmpTypeData, EmpType, 0);
                }
                EmployeeDate = oDBEngine.GetFieldValue(" tbl_master_employee", "cast( emp_dateofJoining as datetime) as  emp_dateofJoining", " emp_contactId='" + ContactData[0, 24].ToString() + "'", 1);
                if (EmployeeDate[0, 0] != "n")
                {
                    if (EmployeeDate[0, 0] != "" && EmployeeDate[0, 0] != "01/01/1900" && EmployeeDate[0, 0] != "1/1/1900 12:00:00 AM")
                    {
                        cmbDOJ.Value = Convert.ToDateTime(EmployeeDate[0, 0]);
                    }
                    else
                    {
                        cmbDOJ.Value = null;
                    }
                }
                else
                {
                    cmbDOJ.Value = null;
                }
                if (ReportData[0, 17] != "" && ReportData[0, 17] != "01/01/1900" && ReportData[0, 17] != "1/1/1900 12:00:00 AM")
                {
                    cmbLeaveEff.Value = Convert.ToDateTime(ReportData[0, 17]);
                    //Hidden_LEF.Value = ContactData[0, 17];
                }
                else
                    cmbLeaveEff.Value = null;
                string[,] DataRT = oDBEngine.GetFieldValue(" tbl_master_employee, tbl_master_contact", " ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') +'['+cnt_shortName+']' AS Name ", " tbl_master_employee.emp_contactId = tbl_master_contact.cnt_internalId and  tbl_master_employee.emp_id ='" + ReportData[0, 20].ToString() + "'", 1);
                if (DataRT[0, 0] != "n")
                {
                    // txtReportTo.Text = DataRT[0, 0];
                    txtReportTo_hidden.Value = ReportData[0, 20].ToString();
                    //lstReportTo.Text = ContactData[0, 20].ToString();
                    txtReportTo.Text = DataRT[0, 0].ToString();
                    //hdnReportToId.Value = ReportData[0, 20].ToString();
                }
                else
                {
                    //lstReportTo.Text = ReportData[0, 20].ToString();
                    txtReportTo.Text = "";
                    //hdnReportToId.Value = "0";
                    // txtReportTo.Text = ReportData[0, 20].ToString();
                }
                string[,] DataRH = oDBEngine.GetFieldValue(" tbl_master_employee, tbl_master_contact", " ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') +'['+cnt_shortName+']' AS Name ", " tbl_master_employee.emp_contactId = tbl_master_contact.cnt_internalId and  tbl_master_employee.emp_id ='" + ReportData[0, 21].ToString() + "'", 1);
                if (DataRH[0, 0] != "n")
                {
                    //txtAReportHead.Text = DataRH[0, 0];
                    txtAReportHead_hidden.Value = ReportData[0, 21].ToString();
                    //lstReportHead.Text = ContactData[0, 21].ToString();
                    txtAdditionalReportingHead.Text = DataRH[0, 0].ToString();
                }
                else
                {
                    // txtAReportHead.Text = ContactData[0, 21].ToString();
                    //lstReportHead.Text = ContactData[0, 21].ToString();
                    txtAdditionalReportingHead.Text = "";
                }

                string[,] DataCLG = oDBEngine.GetFieldValue(" tbl_master_employee, tbl_master_contact", " ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') +'['+cnt_shortName+']' AS Name ", " tbl_master_employee.emp_contactId = tbl_master_contact.cnt_internalId and  tbl_master_employee.emp_id ='" + ReportData[0, 22].ToString() + "'", 1);
                if (DataCLG[0, 0] != "n")
                {
                    //txtColleague.Text = DataCLG[0, 0];
                    txtColleague_hidden.Value = ReportData[0, 22].ToString();
                    //lstColleague.Text = ContactData[0, 22].ToString();
                    txtColleague.Text = DataCLG[0, 0].ToString();
                }
                else
                {
                    //txtColleague.Text = ContactData[0, 22].ToString();
                    //lstColleague.Text = ContactData[0, 22].ToString();
                    txtColleague.Text = "";
                }

                // Mantis Issue 24655
                string[,] DataCLG1 = oDBEngine.GetFieldValue(" tbl_master_employee, tbl_master_contact", " ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') +'['+cnt_shortName+']' AS Name ", " tbl_master_employee.emp_contactId = tbl_master_contact.cnt_internalId and  tbl_master_employee.emp_id ='" + ReportData[0, 48].ToString() + "'", 1);
                if (DataCLG1[0, 0] != "n")
                {
                    txtColleague1_hidden.Value = ReportData[0, 48].ToString();
                    txtColleague1.Text = DataCLG1[0, 0].ToString();
                }
                else
                {
                    txtColleague1.Text = "";
                }

                string[,] DataCLG2 = oDBEngine.GetFieldValue(" tbl_master_employee, tbl_master_contact", " ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') +'['+cnt_shortName+']' AS Name ", " tbl_master_employee.emp_contactId = tbl_master_contact.cnt_internalId and  tbl_master_employee.emp_id ='" + ReportData[0, 49].ToString() + "'", 1);
                if (DataCLG2[0, 0] != "n")
                {
                    txtColleague2_hidden.Value = ReportData[0, 49].ToString();
                    txtColleague2.Text = DataCLG2[0, 0].ToString();
                }
                else
                {
                    txtColleague2.Text = "";
                }
                PopulateMultiSelectItems(ContactData[0, 24].ToString());
            }
            //Rev work close 26.04.2022 0024853: Copy feature add in Employee master
        }
        //Rev work start 26.04.2022 0024853: Copy feature add in Employee master
        private void PopulateMultiSelectItems(string Emp_ContactID)
        {
            // Channel
            DataTable dtChannel = new DataTable();
            // Rev 1.0
            //dtChannel = oDBEngine.GetDataTable("Employee_Channel", " ch_id as Id, ch_Channel as Name  ", " ch_id in (select EP_CH_ID from Employee_ChannelMap where EP_EMP_CONTACTID='" + Emp_ContactID + "')");
            dtChannel = oDBEngine.GetDataTable("Employee_Channel", " convert(varchar(100),ch_id) as Id, ch_Channel as Name  ", " ch_id in (select EP_CH_ID from Employee_ChannelMap where EP_EMP_CONTACTID='" + Emp_ContactID + "')");
            // End of Rev 1.0
            string selectedChannelName = "";
            string selectedChannelId = "";

            foreach (DataRow drCh in dtChannel.Rows)
            {
                if (selectedChannelName == "")
                {
                    selectedChannelName = selectedChannelName + drCh["Name"];
                    selectedChannelId = selectedChannelId + drCh["Id"];
                }
                else
                {
                    selectedChannelName = selectedChannelName + "," + drCh["Name"];
                    selectedChannelId = selectedChannelId + "," + drCh["Id"];
                }
            }
            txtChannels.Text = selectedChannelName;
            txtChannel_hidden.Value = selectedChannelId;

            /*Rev work 14.04.2022
             Mantise Issue :0024828: In employee master in entry section in edit mode selected channel,circle,section, not coming as checked*/
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dtChannel.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dtChannel.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            jsonChannel.InnerText = serializer.Serialize(rows);
            /*Rev work close 14.04.2022
             Mantise Issue :0024828: In employee master in entry section in edit mode selected channel,circle,section, not coming as checked*/
            //

            // Circle
            DataTable dtCircle = new DataTable();
            // Rev 1.0
            //dtCircle = oDBEngine.GetDataTable("Employee_Circle", " crl_id as Id, crl_Circle as Name  ", " crl_id in (select EP_CRL_ID from Employee_CircleMap where EP_EMP_CONTACTID='" + Emp_ContactID + "')");
            dtCircle = oDBEngine.GetDataTable("Employee_Circle", " convert(varchar(100),crl_id) as Id, crl_Circle as Name  ", " crl_id in (select EP_CRL_ID from Employee_CircleMap where EP_EMP_CONTACTID='" + Emp_ContactID + "')");
            // End of Rev 1.0

            string selectedCircleName = "";
            string selectedCircleId = "";

            foreach (DataRow drCh in dtCircle.Rows)
            {
                if (selectedCircleName == "")
                {
                    selectedCircleName = selectedCircleName + drCh["Name"];
                    selectedCircleId = selectedCircleId + drCh["Id"];
                }
                else
                {
                    selectedCircleName = selectedCircleName + "," + drCh["Name"];
                    selectedCircleId = selectedCircleId + "," + drCh["Id"];
                }
            }
            txtCircle.Text = selectedCircleName;
            txtCircle_hidden.Value = selectedCircleId;

            /*Rev work 14.04.2022
             Mantise Issue :0024828: In employee master in entry section in edit mode selected channel,circle,section, not coming as checked*/
            System.Web.Script.Serialization.JavaScriptSerializer serializer1 = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows1 = new List<Dictionary<string, object>>();
            Dictionary<string, object> row1;
            foreach (DataRow dr in dtCircle.Rows)
            {
                row1 = new Dictionary<string, object>();
                foreach (DataColumn col in dtCircle.Columns)
                {
                    row1.Add(col.ColumnName, dr[col]);
                }
                rows1.Add(row1);
            }
            jsonCircle.InnerText = serializer1.Serialize(rows1);
            /*Rev work close 14.04.2022
             Mantise Issue :0024828: In employee master in entry section in edit mode selected channel,circle,section, not coming as checked*/
            //

            // Section
            DataTable dtSection = new DataTable();
            // Rev 1.0
            //dtSection = oDBEngine.GetDataTable("Employee_Section", " sec_id as Id, sec_Section as Name  ", " sec_id in (select EP_SEC_ID from Employee_SectionMap where EP_EMP_CONTACTID='" + Emp_ContactID + "')");
            dtSection = oDBEngine.GetDataTable("Employee_Section", " convert(varchar(100),sec_id) as Id, sec_Section as Name  ", " sec_id in (select EP_SEC_ID from Employee_SectionMap where EP_EMP_CONTACTID='" + Emp_ContactID + "')");
            // End of Rev 1.0

            string selectedSectionName = "";
            string selectedSectionId = "";

            foreach (DataRow drCh in dtSection.Rows)
            {
                if (selectedSectionName == "")
                {
                    selectedSectionName = selectedSectionName + drCh["Name"];
                    selectedSectionId = selectedSectionId + drCh["Id"];
                }
                else
                {
                    selectedSectionName = selectedSectionName + "," + drCh["Name"];
                    selectedSectionId = selectedSectionId + "," + drCh["Id"];
                }
            }
            txtSection.Text = selectedSectionName;
            txtSection_hidden.Value = selectedSectionId;
            /*Rev work 14.04.2022
             Mantise Issue :0024828: In employee master in entry section in edit mode selected channel,circle,section, not coming as checked*/
            System.Web.Script.Serialization.JavaScriptSerializer serializer2 = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows2 = new List<Dictionary<string, object>>();
            Dictionary<string, object> row2;
            foreach (DataRow dr in dtSection.Rows)
            {
                row2 = new Dictionary<string, object>();
                foreach (DataColumn col in dtSection.Columns)
                {
                    row2.Add(col.ColumnName, dr[col]);
                }
                rows2.Add(row2);
            }
            jsonSection.InnerText = serializer2.Serialize(rows2);
            /*Rev work close 14.04.2022
             Mantise Issue :0024828: In employee master in entry section in edit mode selected channel,circle,section, not coming as checked*/
            //

            // Set as Default

            chkChannelDefault.Checked = false;
            chkCircleDefault.Checked = false;
            chkSectionDefault.Checked = false;

            if (txtChannels.Text == "")
            {
                chkChannelDefault.ClientEnabled = false;
            }
            if (txtCircle.Text == "")
            {
                chkCircleDefault.ClientEnabled = false;
            }
            if (txtSection.Text == "")
            {
                chkSectionDefault.ClientEnabled = false;
            }

            string[,] DefaultType = oDBEngine.GetFieldValue("tbl_master_employee", "DefaultType", "emp_contactId='" + Emp_ContactID + "'", 1);
            if (DefaultType[0, 0].ToString() != "n" && DefaultType[0, 0].ToString() != "")
            {
                if (DefaultType[0, 0].ToString() == "Channel" && txtChannels.Text != "")
                {
                    chkChannelDefault.Checked = true;
                }
                else if (DefaultType[0, 0].ToString() == "Circle" && txtCircle.Text != "")
                {
                    chkCircleDefault.Checked = true;
                }
                else if (DefaultType[0, 0].ToString() == "Section" && txtSection.Text != "")
                {
                    chkSectionDefault.Checked = true;
                }
            }
            //

        }
        //Rev work close 26.04.2022 0024853: Copy feature add in Employee master
       
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            //DataTable dtV = new DataTable();
            DataTable dtS = new DataTable();
            DataTable dtB = new DataTable();
            DataTable dtC = new DataTable();
            string CompCode = string.Empty;
            int EmpCode;
            String ShortName = string.Empty;
            string TempCode = string.Empty;
            DataTable dtCTC = new DataTable();
            if (Request.QueryString["id"] == "ADD")
            {
                dtCTC = oDBEngine.GetDataTable("tbl_trans_employeeCTC", " *", "emp_cntID='" + CntID + "'");
            }
            else
            {
                dtCTC = oDBEngine.GetDataTable("tbl_trans_employeeCTC", " *", "emp_cntID='" + HttpContext.Current.Session["KeyVal_InternalID"] + "'");

            }
            if (dtCTC.Rows.Count > 0)
            {
                if (dtCTC.Rows[0]["emp_Organization"].ToString().Length != 0 || dtCTC.Rows[0]["emp_branch"].ToString().Length != 0)
                {
                    dtS = oDBEngine.GetDataTable("tbl_master_company", "cmp_OffRoleShortName,cmp_OnRoleShortName", "cmp_id=" + dtCTC.Rows[0]["emp_Organization"] + "");
                    dtB = oDBEngine.GetDataTable("tbl_master_branch", "branch_Code", "branch_id=" + dtCTC.Rows[0]["emp_branch"] + "");
                    if (dtB.Rows.Count > 0)
                    {
                        if (dtS.Rows.Count > 0)
                        {
                            if (dtCTC.Rows[0]["emp_type"].ToString().Length != 0)
                            {
                                if (dtCTC.Rows[0]["emp_type"].ToString() == "1")
                                {
                                    //CompCode = dtS.Rows[0]["cmp_OnRoleShortName"].ToString() + dtB.Rows[0]["branch_Code"].ToString();
                                    CompCode = dtS.Rows[0]["cmp_OnRoleShortName"].ToString();

                                }
                                else
                                {
                                    //CompCode = dtS.Rows[0]["cmp_OffRoleShortName"].ToString() + dtB.Rows[0]["branch_Code"].ToString();
                                    CompCode = dtS.Rows[0]["cmp_OffRoleShortName"].ToString();

                                }

                                if (CompCode.ToString().Length > 0)
                                {
                                    dtC = oDBEngine.GetDataTable("tbl_master_contact", "max(Cnt_UCC) ", "Cnt_UCC like '" + CompCode.ToString().Trim() + "%' and cnt_internalid like 'EM%'");
                                    if (dtC.Rows.Count > 0)
                                    {
                                        if (dtC.Rows[0][0].ToString().Length != 0)
                                        {
                                            int j = dtC.Rows[0][0].ToString().Length;
                                            int k = j - 7;
                                            EmpCode = Convert.ToInt32(dtC.Rows[0][0].ToString().Substring(7, k)) + 1;
                                            if (EmpCode.ToString().Length > 0)
                                            {
                                                if (EmpCode.ToString().Length == 1)
                                                {
                                                    TempCode = "00" + EmpCode.ToString();

                                                }
                                                else if (EmpCode.ToString().Length == 2)
                                                {
                                                    TempCode = "0" + EmpCode.ToString();


                                                }
                                                else
                                                {
                                                    TempCode = EmpCode.ToString();

                                                }
                                                CompCode = CompCode.ToString().Trim() + TempCode.ToString().Trim();

                                            }
                                        }
                                        else
                                        {
                                            CompCode = CompCode.ToString().Trim() + "001";

                                        }
                                    }
                                    else
                                    {
                                        CompCode = CompCode.ToString().Trim() + "001";

                                    }
                                    txtAliasName.Text = CompCode.ToString();
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "JSct55", "jAlert('Please Add Employee CTC details first.');", true);

                                }

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "JSct", "jAlert('Employee Type Not Found!);return false;", true);

                            }

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "JSct43", "jAlert('Company short name not found.');", true);

                        }
                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "JSct44", "jAlert('Branch code not found.');", true);

                    }




                }
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "JSct3", "jAlert('Please Add Employee CTC details first.');", true);
            }


        }



        [WebMethod]
        public static List<string> BindContactType(string reqID)
        {
            Employee_AddNew_BL objEmployee_AddNew_BL = new Employee_AddNew_BL();
            DataTable ContactDt = objEmployee_AddNew_BL.GetContactTypeonEmployeeType(reqID);

            List<string> obj = new List<string>();
            foreach (DataRow dr in ContactDt.Rows)
            {
                obj.Add(Convert.ToString(dr["Name"]) + "|" + Convert.ToString(dr["ID"]));
            }
            return obj;
            //    Response.Write("No Record Found###No Record Found|");


        }

        // Mantis Issue 24655
        public class ChannelModel
        {
            /*Rev work 14.04.2022
             Mantise Issue :0024828: In employee master in entry section in edit mode selected channel,circle,section, not coming as checked*/
            //public Int64 Id { get; set; }
            public Int64 id { get; set; }
            /*Rev work close 14.04.2022
             Mantise Issue :0024828: In employee master in entry section in edit mode selected channel,circle,section, not coming as checked*/
            public string Name { get; set; }
        }

        [WebMethod(EnableSession = true)]
        public static object GetChannel(string SearchKey)
        {
            List<ChannelModel> listChannel = new List<ChannelModel>();
            if (HttpContext.Current.Session["userid"] != null)
            {
                SearchKey = SearchKey.Replace("'", "''");

                BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();
                /*Rev work 14.04.2022
                 Mantise Issue :0024828: In employee master in entry section in edit mode selected channel,circle,section, not coming as checked*/
                //DataTable Channeldt = oDBEngine.GetDataTable("select top(10) ch_id as Id ,ch_Channel as Name from Employee_Channel where ch_Channel like '%" + SearchKey + "%' ");
                DataTable Channeldt = oDBEngine.GetDataTable("select top(10) ch_id as id ,ch_Channel as Name from Employee_Channel where ch_Channel like '%" + SearchKey + "%' ");
                /*Rev work close 14.04.2022
                 Mantise Issue :0024828: In employee master in entry section in edit mode selected channel,circle,section, not coming as checked*/
                listChannel = APIHelperMethods.ToModelList<ChannelModel>(Channeldt);

            }

            return listChannel;
        }

        public class CircleModel
        {
            /*Rev work 14.04.2022
             Mantise Issue :0024828: In employee master in entry section in edit mode selected channel,circle,section, not coming as checked*/
            //public Int64 Id { get; set; }
            public Int64 id { get; set; }
            /*Rev work close 14.04.2022
             Mantise Issue :0024828: In employee master in entry section in edit mode selected channel,circle,section, not coming as checked*/
            public string Name { get; set; }
        }

        [WebMethod(EnableSession = true)]
        public static object GetCircle(string SearchKey)
        {
            List<CircleModel> listCircle = new List<CircleModel>();
            if (HttpContext.Current.Session["userid"] != null)
            {
                SearchKey = SearchKey.Replace("'", "''");

                BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();
                /*Rev work 14.04.2022
                 Mantise Issue :0024828: In employee master in entry section in edit mode selected channel,circle,section, not coming as checked*/
                //DataTable Circledt = oDBEngine.GetDataTable("select top(10) crl_id as Id ,crl_Circle as Name from Employee_Circle where crl_Circle like '%" + SearchKey + "%' ");
                DataTable Circledt = oDBEngine.GetDataTable("select top(10) crl_id as id ,crl_Circle as Name from Employee_Circle where crl_Circle like '%" + SearchKey + "%' ");
                /*Rev work close 14.04.2022
                 Mantise Issue :0024828: In employee master in entry section in edit mode selected channel,circle,section, not coming as checked*/
                listCircle = APIHelperMethods.ToModelList<CircleModel>(Circledt);

            }

            return listCircle;
        }

        public class SectionModel
        {
            /*Rev work 14.04.2022
             Mantise Issue :0024828: In employee master in entry section in edit mode selected channel,circle,section, not coming as checked*/
            //public Int64 Id { get; set; }
            public Int64 id { get; set; }
            /*Rev work close 14.04.2022
             Mantise Issue :0024828: In employee master in entry section in edit mode selected channel,circle,section, not coming as checked*/
            public string Name { get; set; }
        }

        [WebMethod(EnableSession = true)]
        public static object GetSection(string SearchKey)
        {
            List<SectionModel> listSection = new List<SectionModel>();
            if (HttpContext.Current.Session["userid"] != null)
            {
                SearchKey = SearchKey.Replace("'", "''");

                BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();
                /*Rev work 14.04.2022
                 Mantise Issue :0024828: In employee master in entry section in edit mode selected channel,circle,section, not coming as checked*/
                //DataTable Sectiondt = oDBEngine.GetDataTable("select top(10) sec_id as Id ,sec_Section as Name from Employee_Section where sec_Section like '%" + SearchKey + "%' ");
                DataTable Sectiondt = oDBEngine.GetDataTable("select top(10) sec_id as id ,sec_Section as Name from Employee_Section where sec_Section like '%" + SearchKey + "%' ");
                /*Rev work close 14.04.2022
                 Mantise Issue :0024828: In employee master in entry section in edit mode selected channel,circle,section, not coming as checked*/
                listSection = APIHelperMethods.ToModelList<SectionModel>(Sectiondt);

            }

            return listSection;
        }
        // End of Mantis Issue 24655

        [WebMethod(EnableSession = true)]
        public static object GetOnDemandEmpCTC(string reqStr)
        {
            List<Employeels> listEmployee = new List<Employeels>();
            BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(ConfigurationSettings.AppSettings["DBConnectionDefault"]);
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FetchReportTo");
            proc.AddPara("@action", "ADDNEW");
            proc.AddPara("@userid", Convert.ToString(HttpContext.Current.Session["userid"]));
            proc.AddPara("@firstname", reqStr);
            proc.AddPara("@shortname", reqStr);
            dt = proc.GetTable();

            foreach (DataRow dr in dt.Rows)
            {
                Employeels Employee = new Employeels();
                Employee.id = dr["Id"].ToString();
                Employee.Name = dr["Name"].ToString();
                listEmployee.Add(Employee);
            }
            return listEmployee;
        }

        
        public class Employeels
        {
            public string id { get; set; }
            public string Name { get; set; }
        }
        //Mantis Issue 25148
        private void GetConfigSettings()
        {
            DataTable dtCon = oDBEngine.GetDataTable("select [Key],[Value],[Description] from FTS_APP_CONFIG_SETTINGS");
            if (dtCon.Rows.Count > 0)
            {
                foreach (DataRow dr in dtCon.Rows)
                {
                    if (Convert.ToString(dr["key"]) == "IsChannelCircleSectionMandatory")
                    {
                        if (Convert.ToString(dr["Value"]) == "1")
                        {
                            IsChannelCircleSectionMandatory.Value = "1";
                        }
                        else
                        {
                            IsChannelCircleSectionMandatory.Value = "0";
                        }
                    }
                }
            }
        }
        //End of Mantis Issue 25148
    }
}
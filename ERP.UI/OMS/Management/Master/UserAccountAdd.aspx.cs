//====================================================== Revision History ==========================================================
// Rev Number       DATE              VERSION          DEVELOPER           CHANGES
// 1.0              17/02/2023        2.0.39           Sanchita            A setting required for 'User Account' Master module in FSM Portal
//                                                                         Refer: 25669
// 2.0              23/03/2023        2.0.39           Sanchita            Duplicate office address getting saved.
//                                                                         Refer: 25747
// 3.0              23/03/2023        2.0.39           Sanchita            Also Party Type in User Master not getting mapped to Shop in ITC.
//                                                                         Refer: 25748    
// 4.0              16/0/2024        V2.0.47           Sanchita            These two DS types need to be inserted in the table where the rest of the DS types are available (RMD, Conv DS, Conv TL etc)
//                                                                         Mantis: 27356 
//====================================================== Revision History ==========================================================
using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BusinessLogicLayer.SalesERP;

namespace ERP.OMS.Management.Master
{
    public partial class management_master_UserAccountAdd : ERP.OMS.ViewState_class.VSPage
    {
        BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(ConfigurationSettings.AppSettings["DBConnectionDefault"]);
        BusinessLogicLayer.Converter Oconverter = new BusinessLogicLayer.Converter();
        BusinessLogicLayer.GenericMethod oGenericMethod;
        clsDropDownList oclsDropDownList = new clsDropDownList();
        static string CntID = string.Empty;
        string empCompCode = string.Empty;


        SelectListOptions objSelectListOptions = new SelectListOptions();
        DataTable DT = new DataTable();
        Int32 ID;
        public string WLanguage = "";
        public string SpLanguage = "";
        int CreateUser;
        DateTime CreateDate;
        string usergroup = "";
        // Rev 1.0
        public string IsShowUserAccountForITC = "0";
        // End of Rev 1.0

        protected void Page_Load(object sender, EventArgs e)
        {
            CreateUser = Convert.ToInt32(HttpContext.Current.Session["userid"]);//Session UserID
            CreateDate = Convert.ToDateTime(oDBEngine.GetDate().ToShortDateString());

            // Rev 1.0
            IsShowUserAccountForITC = "0";
            DBEngine obj1 = new DBEngine();
            IsShowUserAccountForITC = Convert.ToString(obj1.GetDataTable("select [value] from FTS_APP_CONFIG_SETTINGS WHERE [Key]='IsShowUserAccountForITC'").Rows[0][0]);

            if (IsShowUserAccountForITC == "1")
            {
                divUserType.Visible = true;
                divChannel.Visible = true;
                divCircle.Visible = true;
                divSection.Visible = true;
            }
            else
            {
                divUserType.Visible = false;
                divChannel.Visible = false;
                divCircle.Visible = false;
                divSection.Visible = false;
            }
            // End of Rev 1.0

            if (!IsPostBack)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "PageLD", "<script>Pageload();</script>");
                ShowForm();
                //Mantis Issue 25148
                GetConfigSettings();
                //End of Mantis Issue 25148
            }
            if (txtChannels.Text != "")
            {
                chkChannelDefault.ClientEnabled = true;
            }
            else
            {
                chkChannelDefault.Checked = false;
                chkChannelDefault.ClientEnabled = false;
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
            btnCTC.Attributes.Add("Onclick", "Javascript:return ValidateCTC();");
        }
        private void ShowForm()
        {

            string[,] Data = oDBEngine.GetFieldValue("tbl_master_branch", "branch_id, branch_description ", null, 2, "branch_description");
            oclsDropDownList.AddDataToDropDownList(Data, cmbBranch);
            //Data = oDBEngine.GetFieldValue("tbl_master_Designation", "deg_id, deg_designation ", null, 2, "deg_designation");
            // Rev 1.0
            //Data = oDBEngine.GetFieldValue("tbl_master_Designation", "deg_id, deg_designation ", "deg_designation in('DS','TL')", 2, "deg_designation");

            if (IsShowUserAccountForITC == "1")
            {
                Data = oDBEngine.GetFieldValue("tbl_master_Designation", "deg_id, deg_designation ", "deg_designation in('DS','TL')", 2, "deg_designation");
            }
            else
            {
                Data = oDBEngine.GetFieldValue("tbl_master_Designation", "deg_id, deg_designation ", null, 2, "deg_designation");
            }
            // End of Rev 1.0

            oclsDropDownList.AddDataToDropDownList(Data, cmbDesg);
            Data = oDBEngine.GetFieldValue("FTS_Stage", "StageID, Stage ", null, 2, "Stage");
            oclsDropDownList.AddDataToDropDownList(Data, ddlType);
            
            //Data = oDBEngine.GetFieldValue("tbl_master_userGroup", "grp_id, grp_name ", null, 2, "grp_name");
            // Rev 1.0
            //Data = oDBEngine.GetFieldValue("tbl_master_userGroup", "grp_id, grp_name ", "grp_name in ('ATTEND-USER','FIELD-USER')", 2, "grp_name");
            if (IsShowUserAccountForITC == "1")
            {
                Data = oDBEngine.GetFieldValue("tbl_master_userGroup", "grp_id, grp_name ", "grp_name in ('ATTEND-USER','FIELD-USER')", 2, "grp_name");
            }
            else
            {
                Data = oDBEngine.GetFieldValue("tbl_master_userGroup", "grp_id, grp_name ", null, 2, "grp_name");
            }
            // End of Rev 1.0
            
            oclsDropDownList.AddDataToDropDownList(Data, ddlGroups);


            cmbBranch.Items.Insert(0, new ListItem("--Select--", "0"));
            cmbDesg.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlType.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlGroups.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        [WebMethod]
        public static List<string> GetreportTo(string firstname, string shortname)
        {
            BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(ConfigurationSettings.AppSettings["DBConnectionDefault"]);
            DataTable DT = new DataTable();
            DT.Rows.Clear();
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
        [WebMethod(EnableSession = true)]
        public static object GetOnDemandEmpCTC(string reqStr)
        {
            List<Employeels> listEmployee = new List<Employeels>();
            BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(ConfigurationSettings.AppSettings["DBConnectionDefault"]);
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FetchReportTo");
            
            //proc.AddPara("@action", "ADDNEW_USERACCOUNT");
            proc.AddPara("@action", "ADDNEW_WD");
            
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
        [WebMethod(EnableSession = true)]
        public static object GetChannel(string SearchKey)
        {
            List<ChannelModel> listChannel = new List<ChannelModel>();
            if (HttpContext.Current.Session["userid"] != null)
            {
                SearchKey = SearchKey.Replace("'", "''");

                BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();
                DataTable Channeldt = oDBEngine.GetDataTable("select top(10) ch_id as id ,ch_Channel as Name from Employee_Channel where ch_Channel like '%" + SearchKey + "%' ");
                listChannel = APIHelperMethods.ToModelList<ChannelModel>(Channeldt);

            }

            return listChannel;
        }
        public class ChannelModel
        {
            public Int64 id { get; set; }
            public string Name { get; set; }
        }
        public class CircleModel
        {
            public Int64 id { get; set; }
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
                DataTable Circledt = oDBEngine.GetDataTable("select top(10) crl_id as id ,crl_Circle as Name from Employee_Circle where crl_Circle like '%" + SearchKey + "%' ");
                listCircle = APIHelperMethods.ToModelList<CircleModel>(Circledt);

            }

            return listCircle;
        }

        public class SectionModel
        {
            public Int64 id { get; set; }
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
                DataTable Sectiondt = oDBEngine.GetDataTable("select top(10) sec_id as id ,sec_Section as Name from Employee_Section where sec_Section like '%" + SearchKey + "%' ");
                listSection = APIHelperMethods.ToModelList<SectionModel>(Sectiondt);
            }
            return listSection;
        }
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
            
            //string EmpType = "1";+
            string[] ET = oDBEngine.GetFieldValue1("tbl_master_employeeType", "emptpy_id", "emptpy_code='RG'", 1);

            string EmpType = ET[0].ToString() ;
            

            dtS = oDBEngine.GetDataTable("tbl_master_company", "onrole_schema_id, offrole_schema_id", "cmp_id=" + cmp_Id + "");

            if (dtS.Rows.Count>0 && dtS.Rows[0]["onrole_schema_id"].ToString() != "" && dtS.Rows[0]["offrole_schema_id"].ToString() != "")
            {
                dtSchemaOn = oDBEngine.GetDataTable("tbl_master_idschema", "prefix, suffix, digit, startno", "id=" + Convert.ToInt64(dtS.Rows[0]["onrole_schema_id"]));
                dtSchemaOff = oDBEngine.GetDataTable("tbl_master_idschema", "prefix, suffix, digit, startno", "id=" + Convert.ToInt64(dtS.Rows[0]["offrole_schema_id"]));

                if (Convert.ToInt32(EmpType) > 0)
                {
                    if (Convert.ToInt32(EmpType) == 1 || Convert.ToInt32(EmpType) == 2)
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

                    string sql_statement = "SELECT max(tmc.Cnt_UCC) FROM tbl_master_contact tmc WHERE tmc.cnt_internalId IN(SELECT ttectc1.emp_cntId FROM tbl_trans_employeeCTC ttectc1 WHERE ttectc1.emp_Organization = " + cmp_Id + ") ";
                    if (prefCompCode.Length > 0)
                        sql_statement += " and tmc.cnt_UCC like '" + prefCompCode + "%' ";
                    sql_statement += " AND dbo.RegexMatch('";

                    if (prefCompCode.Length > 0) sql_statement += "[" + prefCompCode + "]{" + prefLen + "}"; //"^" + prefCompCode;
                    if (startNo.Length > 0) sql_statement += "[0-9]{" + paddCounter + "}";
                    if (sufxCompCode.Length > 0) sql_statement += "[" + sufxCompCode + "]{" + sufxLen + "}";//"^" + sufxCompCode;
                    sql_statement += "?$', tmc.cnt_UCC) = 1 AND tmc.cnt_internalid like 'EM%'";
                    dtC = oDBEngine.GetDataTable(sql_statement);

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
        public int InsertAddress(string emp_cntId, string branch_address1, string branch_address2, string branch_address3, string branch_country, string branch_state, string branch_city, string branch_pin, string branch_area, string branch_id)
        {
            ProcedureExecute proc;
            try
            {
                using (proc = new ProcedureExecute("insert_correspondence"))
                {
                    proc.AddNVarcharPara("@insuId", 100, emp_cntId);
                    proc.AddNVarcharPara("@Type", 100, "Office");
                    proc.AddNVarcharPara("@contacttype", 100, "employee");
                    proc.AddNVarcharPara("@Address1", 500, branch_address1);
                    proc.AddNVarcharPara("@Address2", 500, branch_address2);
                    proc.AddNVarcharPara("@Address3", 500, branch_address3);

                    proc.AddNVarcharPara("@LandMark", 500, "");
                    proc.AddNVarcharPara("@Address4", 500, "");
                    if (branch_city == "")
                    {
                        proc.AddIntegerPara("@City", 0);
                    }
                    else
                    {
                        proc.AddIntegerPara("@City", Convert.ToInt32(branch_city));
                    }
                    if (branch_area == "")
                    {
                        proc.AddIntegerPara("@area", Convert.ToInt32(DBNull.Value));
                    }
                    else
                    {
                        proc.AddIntegerPara("@area", Convert.ToInt32(branch_area));
                    }
                    proc.AddNVarcharPara("@contactperson", 500, "");
                    if (branch_country == "")
                    {
                        proc.AddIntegerPara("@Country", 0);
                    }
                    else
                    {
                        proc.AddIntegerPara("@Country", Convert.ToInt32(branch_country));
                    }
                    if (branch_state == "")
                    {
                        proc.AddIntegerPara("@State", 0);
                    }
                    else
                    {
                        proc.AddIntegerPara("@State", Convert.ToInt32(branch_state));
                    }
                    if (branch_pin == "")
                    {
                        proc.AddIntegerPara("@PinCode", 0);
                    }
                    else
                    {
                        proc.AddIntegerPara("@PinCode", Convert.ToInt32(branch_pin));
                    }
                    proc.AddIntegerPara("@CreateUser", CreateUser);
                    proc.AddDecimalPara("@Isdefault", 0, 0, 0);
                    proc.AddIntegerPara("@Branch", Convert.ToInt32(branch_id));
                    proc.AddNVarcharPara("@Phone", 100, "");

                    proc.AddNVarcharPara("@add_Email", 100, "");
                    proc.AddNVarcharPara("@add_Website", 200, "");
                    proc.AddIntegerPara("@add_Designation", 0);
                    proc.AddDecimalPara("@Id", 0, 18, 0);

                    int NoOfRowEffected = proc.RunActionQuery();
                    return NoOfRowEffected;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                proc = null;
            }
        }
        public int InsertPhone(string emp_cntId, string Phoneno)
        {
            ProcedureExecute proc;
            try
            {
                using (proc = new ProcedureExecute("insert_correspondence_phone"))
                {
                    proc.AddNVarcharPara("@PhfId", 50, emp_cntId);
                    proc.AddNVarcharPara("@phf_type", 50, "Office");
                    proc.AddNVarcharPara("@contacttype", 50, "employee");
                    proc.AddNVarcharPara("@phf_countryCode", 50, "");
                    proc.AddNVarcharPara("@phf_areaCode", 50, "");
                    proc.AddNVarcharPara("@phf_phoneNumber", 50, Phoneno);

                    proc.AddNVarcharPara("@phf_extension", 50, "");
                    proc.AddNVarcharPara("@phf_Availablefrom", 50, "");
                    proc.AddNVarcharPara("@phf_AvailableTo", 50, "");
                    proc.AddNVarcharPara("@phf_SMSFacility", 50, "");

                    proc.AddIntegerPara("@CreateUser", CreateUser);
                    proc.AddDecimalPara("@Isdefault", 0, 0, 0);
                    proc.AddNVarcharPara("@phf_ContactPerson", 100, "");
                    proc.AddNVarcharPara("@phf_ContactPersonDesignation", 100, "");
                    int NoOfRowEffected = proc.RunActionQuery();
                    return NoOfRowEffected;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                proc = null;
            }
        }
        protected string getuserGroup()
        {
            return ddlGroups.SelectedValue.ToString();
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
            }
            else
            {
                finyear = Convert.ToString((y - 1)) + "-" + y.ToString();
            }
            return finyear;
        }
        protected void btnCTC_Click(object sender, EventArgs e)
        {
            try
            {
                 
                //if (calledFromChannelLookup_hidden.Value == "1" || calledFromCircleLookup_hidden.Value == "1" || calledFromSectionLookup_hidden.Value == "1")
                if (IsChannelCircleSectionMandatory.Value == "0" && (calledFromChannelLookup_hidden.Value == "1" || calledFromCircleLookup_hidden.Value == "1" || calledFromSectionLookup_hidden.Value == "1") )
                    
                {
                    calledFromChannelLookup_hidden.Value = "0";
                    calledFromCircleLookup_hidden.Value = "0";
                    calledFromSectionLookup_hidden.Value = "0";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "GeneralLoading", "stopLoading();", true);
                    return;
                }
               

                int LoginIDExist = 0;
               /* string[,] checkUser = oDBEngine.GetFieldValue("tbl_master_user", "user_loginId", " user_loginId='" + txtuserid.Text.ToString().Trim() + "'", 1);
                string check = checkUser[0, 0];
                if (check != "n")
                {
                    LoginIDExist = 1;

                }*/

                
                //string ulogid = string.Empty;
                //string empuniqid = string.Empty;

               
                //DataTable Logindt = oDBEngine.GetDataTable("Select u.user_loginid,e.emp_uniqueCode From tbl_master_user u,tbl_master_employee e " +
                //"Where u.user_contactid=e.emp_contactid and u.user_loginid='" + txtuserid.Text.ToString().Trim() + "'");

                // Duplicate validation if found in tbl_master_user- 'User_Loginid' or tbl_master_employee-'emp_uniqueCode'
                DataTable Logindt = oDBEngine.GetDataTable("SELECT emp_uniqueCode FROM TBL_MASTER_EMPLOYEE WHERE emp_uniqueCode='" + txtuserid.Text.ToString().Trim() + "' UNION " +
                                                             "select user_loginid from tbl_master_user where user_loginid='" + txtuserid.Text.ToString().Trim() + "'");
                if (Logindt != null && Logindt.Rows.Count > 0)
                 {
                     //ulogid = Logindt.Rows[0]["user_loginid"].ToString();
                     //empuniqid = Logindt.Rows[0]["emp_uniqueCode"].ToString();
                     LoginIDExist = 1;
                 }
                 //else
                 //{
                 //    ulogid = "n";
                 //    empuniqid = "n";
                 //}

                 //if (ulogid != "n" || empuniqid!="n")
                 //{
                 //    LoginIDExist = 1;
                 //}

                

                if (LoginIDExist == 0)
                {
                     
                    //string Organization = "68";
                    //DataTable dtS = new DataTable();
                    //dtS = oDBEngine.GetDataTable("tbl_master_company", "onrole_schema_id, offrole_schema_id", "cmp_id=" + Organization + "");

                    string Organization = "";
                    DataTable dtS = oDBEngine.GetDataTable("tbl_master_company", "onrole_schema_id, offrole_schema_id, cmp_id", null);
                    if (dtS.Rows.Count > 0)
                    {
                        Organization = dtS.Rows[0]["cmp_id"].ToString();
                    }

                    if (dtS.Rows.Count > 0 && ( dtS.Rows[0]["onrole_schema_id"].ToString() == "" || dtS.Rows[0]["offrole_schema_id"].ToString() == "") )
                    {
                        // ------------ Check IF company Schema Exists -------------
                        string alert = "'Please define Company On & Off Role Schema for " + Organization + "'";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "General", "jAlert(" + alert + ");", true);
                        return;
                    }
                    else if (!checkNMakeEmpCode(Convert.ToInt32(Organization)))
                    {
                        // ------------ Check for different validation ---------------
                        return;
                    }
                    String ChannelType = txtChannel_hidden.Value.ToString();
                    String Circle = txtCircle_hidden.Value.ToString();
                    String Section = txtSection_hidden.Value.ToString();

                    String DefaultType = "";
                    
                    //if (chkChannelDefault.Checked == true)
                    //{
                    //    DefaultType = "Channel";
                    //}
                    //else if (chkCircleDefault.Checked == true)
                    //{
                    //    DefaultType = "Circle";
                    //}
                    //else if (chkSectionDefault.Checked == true || Section!="")
                    //{
                    //    DefaultType = "Section";
                    //}

                    if (Section != "")
                    {
                        DefaultType = "Section";
                    }
                    
                    // End of Mantis Issue 24655

                    //=======================For naming Part / 1st part ========================================
                    Employee_BL objEmployee = new Employee_BL();
                    bool chkAllow = false;

                    string Salutation = "1";
                    string AliasName = "";
                    string Gender = "1";
                    string JoiningDate = "";

                    string InternalID = objEmployee.btnSave_Click_BL(Convert.ToString(DBNull.Value), Salutation, txtFirstNmae.Text,
                    txtMiddleName.Text, txtLastName.Text, AliasName, "0", Gender, Convert.ToString(DBNull.Value), Convert.ToString(DBNull.Value), Convert.ToString(DBNull.Value),
                    Convert.ToString(DBNull.Value), Convert.ToString(DBNull.Value), Convert.ToString(DBNull.Value), Convert.ToString(DBNull.Value), Convert.ToString(DBNull.Value),
                    chkAllow, Convert.ToString(DBNull.Value), Convert.ToString(ChannelType), Convert.ToString(Circle), Convert.ToString(Section), DefaultType);

                    
                    //string EmpType = "1";
                    string[] ET = oDBEngine.GetFieldValue1("tbl_master_employeeType", "emptpy_id", "emptpy_code='RG'", 1);
                    string EmpType = ET[0].ToString();
                    

                    //if (EmpType == "19")
                    //{
                    //    string contactPrefix = string.Empty;
                    //    switch (EmpType)
                    //    {
                    //        case "19":
                    //            contactPrefix = "DV";
                    //            break;
                    //    }
                    //    Employee_AddNew_BL objEmployee_AddNew_BL = new Employee_AddNew_BL();
                    //    string contactID = Request.Form["ctl00$ContentPlaceHolder1$ContactType"].ToString();
                    //    if (!string.IsNullOrEmpty(contactID))
                    //    {
                    //        objEmployee_AddNew_BL.InsertContactType_BL(InternalID, contactID, contactPrefix);
                    //    }
                    //}
                    ///////###################################################//////

                    HttpContext.Current.Session["KeyVal_InternalID"] = InternalID;
                    string[,] cnt_id = oDBEngine.GetFieldValue(" tbl_master_contact", " cnt_id", " cnt_internalId='" + InternalID + "'", 1);
                    if (cnt_id[0, 0].ToString() != "n")
                    {

                        //===============================For join Date/ 2nd Part=====================================
                        oGenericMethod = new BusinessLogicLayer.GenericMethod();
                        string value = string.Empty;
                        //Checking For Expiry Date 
                        DateTime now = DateTime.Now;
                        //string ValidationResult = oGenericMethod.IsProductExpired(Convert.ToDateTime(cmbDOJ.Value));
                        string ValidationResult = oGenericMethod.IsProductExpired(Convert.ToDateTime(now));
                        if (Convert.ToBoolean(ValidationResult.Split('~')[0]))
                        {
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Vscript", "jAlert('" + ValidationResult.Split('~')[1] + "');", true);
                        }
                        else
                        {
                            //value = "emp_din=' ', emp_dateofJoining ='" + cmbDOJ.Value + "', emp_dateofLeaving ='" + DBNull.Value + "',emp_ReasonLeaving  ='" + DBNull.Value + "', emp_NextEmployer ='" + DBNull.Value + "', emp_AddNextEmployer  ='" + DBNull.Value + "',LastModifyDate='" + oDBEngine.GetDate().ToString() + "',LastModifyUser='" + Session["userid"].ToString() + "'";
                            value = "emp_din=' ', emp_dateofJoining ='" + now + "', emp_dateofLeaving ='" + DBNull.Value + "',emp_ReasonLeaving  ='" + DBNull.Value + "', emp_NextEmployer ='" + DBNull.Value + "', emp_AddNextEmployer  ='" + DBNull.Value + "',LastModifyDate='" + oDBEngine.GetDate().ToString() + "',LastModifyUser='" + Session["userid"].ToString() + "'";
                            Int32 rowsEffected = oDBEngine.SetFieldValue("tbl_master_employee", value, " emp_contactid ='" + HttpContext.Current.Session["KeyVal_InternalID"] + "'");

                            if (Session["KeyVal_InternalID"] != "n")
                            {

                                DataTable DT_empCTC = oDBEngine.GetDataTable(" tbl_trans_employeeCTC ", " emp_id ", " emp_cntId='" + Convert.ToString(Session["KeyVal_InternalID"]) + "'");
                                if (DT_empCTC.Rows.Count > 0)
                                    //JoiningDate.Value = oDBEngine.GetDate();
                                    now = oDBEngine.GetDate();
                                else
                                {
                                    DataTable dt = oDBEngine.GetDataTable(" tbl_master_employee", "emp_dateofJoining", " (emp_contactId = '" + Convert.ToString(Session["KeyVal_InternalID"]) + "')");
                                    if (dt.Rows.Count > 0)
                                        // JoiningDate.Value = dt.Rows[0][0];
                                        JoiningDate = dt.Rows[0][0].ToString();
                                }
                            }

                            if (rowsEffected > 0)
                            {
                                Employee_BL objEmployee_BL = new Employee_BL();

                                string emp_cntId = Convert.ToString(HttpContext.Current.Session["KeyVal_InternalID"]);
                                CntID = Convert.ToString(HttpContext.Current.Session["KeyVal_InternalID"]);
                                string joiningDate = string.Empty;
                                string emp_LeaveSchemeAppliedFrom = string.Empty;
                                //if (JoiningDate.Value != null)
                                if (now != null)
                                {
                                    //joiningDate = JoiningDate.Value.ToString();
                                    joiningDate = now.ToString();
                                }
                                else
                                {
                                    joiningDate = "";
                                }

                                //if (cmbLeaveEff.Value != null)
                                if (now != null)
                                {
                                    //emp_LeaveSchemeAppliedFrom = cmbLeaveEff.Value.ToString();
                                    emp_LeaveSchemeAppliedFrom = now.ToString();
                                }
                                else
                                {
                                    emp_LeaveSchemeAppliedFrom = "";
                                }

                                //string ReportHead = txtAReportHead_hidden.Value;
                                string ReportHead = "";
                                if (ReportHead == "")
                                {
                                    ReportHead = "0";
                                }

                                //string Colleague = txtColleague_hidden.Value;
                                string Colleague = "";
                                if (Colleague == "")
                                {
                                    Colleague = "0";
                                }

                                //string Colleague1 = txtColleague1_hidden.Value;
                                string Colleague1 = "";
                                if (Colleague1 == "")
                                {
                                    Colleague1 = "0";
                                }

                                //string Colleague2 = txtColleague2_hidden.Value;
                                string Colleague2 = "";
                                if (Colleague2 == "")
                                {
                                    Colleague2 = "0";
                                }

                                
                                //string jobresponsibility = "8";
                                //string Department = "3";
                                //string EmpWorkingHours = "1";
                                //string LeaveScheme = "2";

                                string jobresponsibility = "";
                                string Department = "";
                                string EmpWorkingHours = "";
                                string LeaveScheme = "";

                                DataTable dtJ = oDBEngine.GetDataTable("tbl_master_jobResponsibility", "job_id", " (job_responsibility = 'Marketing & Sales')");
                                if (dtJ.Rows.Count > 0)
                                    jobresponsibility = dtJ.Rows[0][0].ToString();

                                DataTable dtD = oDBEngine.GetDataTable("tbl_master_costCenter", "cost_id", " (cost_description = 'Marketing & Sales')");
                                if (dtD.Rows.Count > 0)
                                    Department = dtD.Rows[0][0].ToString();

                                DataTable dtW = oDBEngine.GetDataTable("tbl_EmpWorkingHours", "Id", " (Name = 'Default')");
                                if (dtW.Rows.Count > 0)
                                    EmpWorkingHours = dtW.Rows[0][0].ToString();

                                DataTable dtL = oDBEngine.GetDataTable("tbl_master_LeaveScheme", "ls_id", " (ls_name = 'Default')");
                                if (dtL.Rows.Count > 0)
                                    LeaveScheme = dtL.Rows[0][0].ToString();
                                

                                objEmployee_BL.btnCTC_Click_BL(emp_cntId, joiningDate, Organization, jobresponsibility, cmbDesg.SelectedItem.Value, EmpType,
                                Department, txtReportTo_hidden.Value, ReportHead, Colleague, EmpWorkingHours, LeaveScheme, emp_LeaveSchemeAppliedFrom, cmbBranch.SelectedItem.Value,
                                Convert.ToString(DBNull.Value), Colleague1, Colleague2);

                                oDBEngine.SetFieldValue("tbl_master_contact", "Cnt_UCC ='" + empCompCode.ToString() + "'", " cnt_internalid ='" + emp_cntId + "'");
                                oDBEngine.SetFieldValue("tbl_master_employee", "emp_uniquecode='" + empCompCode.ToString() + "'", "emp_contactID='" + emp_cntId + "'");
                                //Rev work start 26.07.2022 mantise no:25046
                                //AliasName = empCompCode.ToString(); 
                                AliasName = txtuserid.Text; 
                                string OtherID = AliasName;
                                //Rev work close 26.07.2022 mantise no:25046
                                if (AliasName.ToString() != "")
                                {
                                    DataTable dt = oDBEngine.GetDataTable("SELECT cnt_id FROM tbl_master_contact WHERE cnt_UCC = '" + AliasName.ToString().Trim() +
                                   "' AND cnt_internalId != '" + Session["KeyVal_InternalID"] + "' AND cnt_internalId IN(SELECT emp_cntId FROM tbl_trans_employeeCTC " +
                                   " WHERE emp_Organization IN (SELECT tte.emp_Organization FROM tbl_trans_employeeCTC tte WHERE emp_cntId = '" + Session["KeyVal_InternalID"] + "'))");

                                    if (dt.Rows.Count > 0)
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert12", "jAlert('Employee Code already Exists!..');", true);
                                    }
                                    else
                                    {
                                        oDBEngine.SetFieldValue("tbl_master_employee", "emp_uniquecode='" + AliasName.ToString() + "', cnt_OtherID = '" + OtherID.ToString() + "'", "emp_contactID='" + Session["KeyVal_InternalID"] + "'");
                                        oDBEngine.SetFieldValue("tbl_master_contact", "cnt_ucc='" + AliasName.ToString() + "',cnt_ShortName='" + AliasName.ToString() + "', cnt_OtherID = '" + OtherID.ToString() + "'", "cnt_internalid='" + Session["KeyVal_InternalID"] + "'");
                                        DataTable dtchk = oDBEngine.GetDataTable("tbl_master_contact", " cnt_id ", "cnt_internalid='" + Session["KeyVal_InternalID"] + "'");
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "General", "jAlert('Employee ID can not be blank');", true);
                                }

                                DataTable dtAddr = oDBEngine.GetDataTable("SELECT branch_id,branch_internalId,branch_code,branch_address1,branch_address2,branch_address3,branch_country, " +
                                    "branch_state,branch_city,branch_pin,branch_area " +
                                "FROM tbl_master_branch WHERE branch_id = '" + cmbBranch.SelectedValue.ToString() + "'");

                                string branch_id = string.Empty;
                                string branch_address1 = string.Empty;
                                string branch_address2 = string.Empty;
                                string branch_address3 = string.Empty;
                                string branch_country = string.Empty;
                                string branch_state = string.Empty;
                                string branch_city = string.Empty;
                                string branch_pin = string.Empty;
                                string branch_area = string.Empty;
                                string Phoneno = string.Empty;
                                Phoneno = txtPhno.Text;

                                if (dtAddr.Rows.Count > 0)
                                {
                                    branch_id = dtAddr.Rows[0]["branch_id"].ToString();
                                    branch_address1 = dtAddr.Rows[0]["branch_address1"].ToString();
                                    branch_address2 = dtAddr.Rows[0]["branch_address2"].ToString();
                                    branch_address3 = dtAddr.Rows[0]["branch_address3"].ToString();
                                    branch_country = dtAddr.Rows[0]["branch_country"].ToString();
                                    branch_state = dtAddr.Rows[0]["branch_state"].ToString();
                                    branch_city = dtAddr.Rows[0]["branch_city"].ToString();
                                    branch_pin = dtAddr.Rows[0]["branch_pin"].ToString();
                                    branch_area = dtAddr.Rows[0]["branch_area"].ToString();
                                }

                                ////Address insert
                                // Rev 2.0 [ Address already getting inserted while CTC add by objEmployee_BL.btnCTC_Click_BL - Mantis 25531, 25533 ]
                                //int n = InsertAddress(emp_cntId, branch_address1, branch_address2, branch_address3, branch_country,
                                //    branch_state, branch_city, branch_pin, branch_area, branch_id);
                                // End of Rev 2.0

                                //Phone no insert
                                int x = InsertPhone(emp_cntId, Phoneno);

                                //usermaster insert
                                string password = "123";

                                Encryption epasswrd = new Encryption();
                                string Encryptpass = epasswrd.Encrypt(password.Trim());

                                usergroup = getuserGroup();
                                //if (usergroup != "" && usergroup != "Select Group")
                                //{
                                //    int y = InsertUser(Encryptpass, emp_cntId);
                                //}
                                int y = InsertUser(Encryptpass, emp_cntId);

                                //User state mapping
                                SalesPersontracking ob = new SalesPersontracking();
                                DataTable dtfromtosumervisor = SalesPersontracking.SubmitEmployeeState(emp_cntId, branch_state, Convert.ToString(HttpContext.Current.Session["userid"]));

                                //Assign party                                
                                string reportto_uniqueid = string.Empty;
                                string reportto_userid = string.Empty;
                                string empuserid = string.Empty;
                                string shop_code = string.Empty;

                                DataTable dtassignparty = oDBEngine.GetDataTable("select e.emp_uniquecode,u.user_id from tbl_master_employee e ,tbl_master_user u where e.emp_id=( " +
                                "select ctc.emp_reportTo from tbl_master_employee emp,tbl_trans_employeeCTC ctc where emp.emp_contactId=ctc.emp_cntId and emp.emp_contactId='" + emp_cntId + "')and e.emp_contactId=u.user_contactId");
                                if (dtassignparty.Rows.Count > 0)
                                {
                                    reportto_uniqueid = dtassignparty.Rows[0]["emp_uniquecode"].ToString();
                                    reportto_userid = dtassignparty.Rows[0]["user_id"].ToString();
                                }
                                DataTable dtempuserid = oDBEngine.GetDataTable("select user_id from tbl_master_user where user_contactId='" + emp_cntId + "'");
                                if(dtempuserid.Rows.Count>0)
                                {
                                    
                                   // empuserid = dtassignparty.Rows[0]["user_id"].ToString();
                                    empuserid = dtempuserid.Rows[0]["user_id"].ToString();
                                    
                                }
                                DataTable dtshopcode = oDBEngine.GetDataTable("select s.Shop_Code from tbl_Master_shop s,tbl_master_employee e,tbl_master_user u where s.type='4' and s.EntityCode=e.emp_uniqueCode " +
                                 "and e.emp_contactId=u.user_contactId and u.user_id=s.shop_createuser and e.emp_uniqueCode='"+reportto_uniqueid+"'");
                                if (dtshopcode.Rows.Count > 0)
                                {
                                    shop_code = dtshopcode.Rows[0]["Shop_Code"].ToString();
                                }

                                
                                //SaveAssignParty(shop_code, empuserid, reportto_uniqueid, reportto_userid, cmbBranch.SelectedValue.ToString());

                                string selected_users = reportto_userid + "," + empuserid;
                                SaveAssignParty(shop_code, selected_users, reportto_uniqueid, reportto_userid, cmbBranch.SelectedValue.ToString());
                                
                                Response.Redirect("/OMS/Management/Master/UserAccountList.aspx");
                            }
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "General", "jAlert('Duplicate user id found ! Please Talk to Administrator.!..');", true);
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "General", "jAlert('Please Try Again!..');", true);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "GeneralLoading", "stopLoading();", true);
            //Rev work start 27.07.2022 mantise no:0025046: User Account
            //Response.Redirect("/OMS/Management/Master/UserAccountAdd.aspx");
            //Response.Redirect("/OMS/Management/Master/UserAccountList.aspx");
            //Rev work close 27.07.2022 mantise no:0025046: User Account
        }

        private void SaveAssignParty(string shop_code, string empuserid, string reportto_uniqueid, string reportto_userid, string branch)
        {
            string returnmsg = "";
            if (HttpContext.Current.Session["userid"] != null)
            {
                
                string PARTY_TYPE = "";
                DataTable dtS = oDBEngine.GetDataTable("tbl_shoptype", "shop_typeId", " (Name = 'DISTRIBUTOR')");
                if (dtS.Rows.Count > 0)
                    PARTY_TYPE = dtS.Rows[0][0].ToString();
                

                DataTable Userdt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("prc_EmployeeShopMapInsertUpdate");
                proc.AddPara("@ACTION", "AssignShopUserNew");
                proc.AddPara("@SHOP_CODE", shop_code);
                
                //proc.AddPara("@PARTY_TYPE", 4);
                proc.AddPara("@PARTY_TYPE", PARTY_TYPE);
                
                proc.AddPara("@Users", empuserid);
                proc.AddPara("@NAME", reportto_uniqueid);
                proc.AddPara("@headerid", 0);
                proc.AddPara("@userid", reportto_userid);                
                proc.AddPara("@BRANCHID",Convert.ToInt32(branch));                
                Userdt = proc.GetTable();
                if (Userdt != null)
                {
                    returnmsg = "OK";
                }
                else
                {
                    returnmsg = "Already exists";
                }
            }
        }
        public int InsertUser(string Encryptpass, string emp_cntId)
        {
            ProcedureExecute proc;
            try
            {
                //Rev work start 27.07.2022 mantise no:25046
                string isactive = "N";
                string isactivemac = "N";
                int istargetsettings = 0;
                
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
                int isHorizontalPerformReportShow = 0;

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

                int IsRevisitRemarksMandatory = 0;
                int GPSAlert = 0;
                int GPSAlertwithSound = 0;
                int FaceRegistrationFrontCamera = 0;
                int MRPInOrder = 0;
                //Rev work start 26.07.2022 mantise no:25046
                int FaceRegTypeID = 0;
                FaceRegTypeID = Convert.ToInt32(ddlType.SelectedValue.ToString());
                //Rev work close 26.07.2022 mantise no:25046

                
                //string channel = txtChannel_hidden.Value.ToString();
                //string Stage = ddlType.SelectedValue.ToString();
                //string Desg = cmbDesg.SelectedValue.ToString();

                string[] channel = txtChannels.Text.ToString().Split(',');
                string Stage = ddlType.SelectedItem.ToString();
                string Desg = cmbDesg.SelectedItem.ToString();
                

                
                string user_name = txtFirstNmae.Text.ToString().Trim();

                if (txtMiddleName.Text.Trim() != "")
                    user_name = user_name + " " + txtMiddleName.Text.ToString().Trim();

                if (txtLastName.Text.Trim() != "")
                    user_name = user_name + " " + txtLastName.Text.ToString().Trim();
                


                string[,] grpsegment = oDBEngine.GetFieldValue("tbl_master_userGroup", "top 1 grp_segmentid", "grp_id in (" + usergroup.ToString() + ")", 1);
                string[,] segname = oDBEngine.GetFieldValue("tbl_master_segment", "seg_name", "seg_id='" + grpsegment[0, 0] + "'", 1);

                using (proc = new ProcedureExecute("PRC_FTSInsertUpdateUser"))
                {
                    //if (channel == "1" && Desg == "291" && Stage == "1" || Stage == "2"))

                    //if (channel == "1" && Desg == "291" && Stage == "1")
                    // Rev 4.0
                    //if (channel.Contains("CFP") && Desg == "DS" && (Stage == "Conv SR" || Stage == "RMD") && ddlGroups.SelectedItem.ToString() == "FIELD-USER")
                    if (channel.Contains("CFP") && Desg == "DS" && (Stage == "Conv SR" || Stage == "RMD" || Stage == "Stockist DS" || Stage == "Emerging DS") && ddlGroups.SelectedItem.ToString() == "FIELD-USER")
                        // End of Rev 4.0

                    {
                        proc.AddPara("@ACTION", "INSERT");
                        
                        //proc.AddPara("@txtusername", txtuserid.Text.Trim());
                        proc.AddPara("@txtusername", user_name);
                        
                        proc.AddPara("@b_id", cmbBranch.SelectedValue.ToString());
                        proc.AddPara("@txtuserid", txtuserid.Text.Trim());
                        proc.AddPara("@Encryptpass", Encryptpass);
                        proc.AddPara("@contact", emp_cntId);
                        proc.AddPara("@usergroup", ddlGroups.SelectedValue.ToString());
                        proc.AddPara("@CreateDate", CreateDate.ToString());
                        proc.AddPara("@CreateUser", CreateUser);
                        proc.AddPara("@superuser", "N");
                        proc.AddPara("@ddDataEntry", "F");
                        proc.AddPara("@IPAddress", "");
                        proc.AddPara("@isactive", isactive);
                        proc.AddPara("@isactivemac", isactivemac);
                        proc.AddPara("@txtgps", 500);

                        proc.AddPara("@istargetsettings", istargetsettings);
                        proc.AddPara("@isLeaveApprovalEnable", isLeaveApprovalEnable);
                        proc.AddPara("@IsAutoRevisitEnable", 1);
                        proc.AddPara("@IsShowPlanDetails", IsShowPlanDetails);
                        proc.AddPara("@IsMoreDetailsMandatory", IsMoreDetailsMandatory);
                        proc.AddPara("@IsShowMoreDetailsMandatory", IsShowMoreDetailsMandatory);

                        proc.AddPara("@isMeetingAvailable", isMeetingAvailable);
                        proc.AddPara("@isRateNotEditable", isRateNotEditable);
                        proc.AddPara("@IsShowTeamDetails", 0);
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
                        proc.AddPara("@isVisitShow", 1);
                        proc.AddPara("@iscollectioninMenuShow", iscollectioninMenuShow);
                        proc.AddPara("@isShopAddEditAvailable", 1);
                        proc.AddPara("@isEntityCodeVisible", isEntityCodeVisible);
                        proc.AddPara("@isAreaMandatoryInPartyCreation", isAreaMandatoryInPartyCreation);
                        proc.AddPara("@isShowPartyInAreaWiseTeam", isShowPartyInAreaWiseTeam);
                        proc.AddPara("@isChangePasswordAllowed", isChangePasswordAllowed);
                        proc.AddPara("@isHomeRestrictAttendance", 1);
                        proc.AddPara("@isQuotationShow", isQuotationShow);
                        proc.AddPara("@IsStateMandatoryinReport", IsStateMandatoryinReport);

                        proc.AddPara("@isAchievementEnable", isAchievementEnable);
                        proc.AddPara("@isTarVsAchvEnable", isTarVsAchvEnable);
                        proc.AddPara("@shopLocAccuracy", 500.00);
                        proc.AddPara("@homeLocDistance", "100.00");

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
                        proc.AddPara("@isShopEditEnable", 1);
                        proc.AddPara("@isTaskEnable", isTaskEnable);


                        //proc.AddPara("@PartyType", "1");
                        // Rev 3.0
                        //string PARTY_TYPE = "";
                        //DataTable dtS = oDBEngine.GetDataTable("tbl_shoptype", "shop_typeId", " (Name = 'DEALER')");
                        //if (dtS.Rows.Count > 0)
                        //    PARTY_TYPE = dtS.Rows[0][0].ToString();

                        //proc.AddPara("@PartyType", PARTY_TYPE);

                        if(IsShowUserAccountForITC == "1")
                        {
                            proc.AddPara("@PartyType", "1");
                        }
                        else
                        {
                            proc.AddPara("@PartyType", "0");
                        }
                        // End of Rev 3.0


                        proc.AddPara("@isAppInfoEnable", 1);
                        proc.AddPara("@willDynamicShow", willDynamicShow);
                        proc.AddPara("@willActivityShow", willActivityShow);
                        proc.AddPara("@isDocumentRepoShow", isDocumentRepoShow);
                        proc.AddPara("@isChatBotShow", isChatBotShow);
                        proc.AddPara("@isAttendanceBotShow", isAttendanceBotShow);
                        proc.AddPara("@isVisitBotShow", isVisitBotShow);
                        proc.AddPara("@appInfoMins", 99);


                        proc.AddPara("@isInstrumentCompulsory", isInstrumentCompulsory);
                        proc.AddPara("@isBankCompulsory", isBankCompulsory);

                        proc.AddPara("@isComplementaryUser", isComplementaryUser);
                        proc.AddPara("@isVisitPlanShow", isVisitPlanShow);
                        proc.AddPara("@isVisitPlanMandatory", isVisitPlanMandatory);
                        proc.AddPara("@isAttendanceDistanceShow", isAttendanceDistanceShow);
                        proc.AddPara("@willTimelineWithFixedLocationShow", willTimelineWithFixedLocationShow);
                        proc.AddPara("@isShowOrderRemarks", isShowOrderRemarks);
                        proc.AddPara("@isShowOrderSignature", isShowOrderSignature);
                        proc.AddPara("@isShowSmsForParty", isShowSmsForParty);
                        proc.AddPara("@isShowTimeline", 0);
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
                        proc.AddPara("@isMultipleVisitEnable", 0);
                        proc.AddPara("@isShowVisitRemarks", isShowVisitRemarks);
                        proc.AddPara("@isShowNearbyCustomer", 1);
                        proc.AddPara("@isServiceFeatureEnable", isServiceFeatureEnable);
                        proc.AddPara("@isPatientDetailsShowInOrder", isPatientDetailsShowInOrder);
                        proc.AddPara("@isPatientDetailsShowInCollection", isPatientDetailsShowInCollection);
                        proc.AddPara("@isAttachmentMandatory", isAttachmentMandatory);
                        proc.AddPara("@isShopImageMandatory", isShopImageMandatory);

                        proc.AddPara("@isLogShareinLogin", 1);
                        proc.AddPara("@IsCompetitorenable", IsCompetitorenable);
                        proc.AddPara("@IsOrderStatusRequired", IsOrderStatusRequired);
                        proc.AddPara("@IsCurrentStockEnable", IsCurrentStockEnable);
                        proc.AddPara("@IsCurrentStockApplicableforAll", IsCurrentStockApplicableforAll);
                        proc.AddPara("@IscompetitorStockRequired", IscompetitorStockRequired);
                        proc.AddPara("@IsCompetitorStockforParty", IsCompetitorStockforParty);
                        proc.AddPara("@ShowFaceRegInMenu", 0);
                        proc.AddPara("@IsFaceDetection", 1);

                        proc.AddPara("@IsUserwiseDistributer", 1);
                        proc.AddPara("@IsPhotoDeleteShow", 0);
                        proc.AddPara("@IsAllDataInPortalwithHeirarchy", 1);
                        proc.AddPara("@IsFaceDetectionWithCaptcha", IsFaceDetectionWithCaptcha);

                        proc.AddPara("@IsShowMenuAddAttendance", 0);
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

                        proc.AddPara("@IsShowPartyOnAppDashboard", 1);
                        proc.AddPara("@IsShowAttendanceOnAppDashboard", IsShowAttendanceOnAppDashboard);
                        proc.AddPara("@IsShowTotalVisitsOnAppDashboard", IsShowTotalVisitsOnAppDashboard);
                        proc.AddPara("@IsShowVisitDurationOnAppDashboard", IsShowVisitDurationOnAppDashboard);
                        proc.AddPara("@IsShowDayStart", 1);
                        proc.AddPara("@IsshowDayStartSelfie", IsshowDayStartSelfie);
                        proc.AddPara("@IsShowDayEnd", 1);
                        proc.AddPara("@IsshowDayEndSelfie", IsshowDayEndSelfie);
                        proc.AddPara("@IsShowLeaveInAttendance", IsShowLeaveInAttendance);
                        proc.AddPara("@IsLeaveGPSTrack", IsLeaveGPSTrack);
                        proc.AddPara("@IsShowActivitiesInTeam", IsShowActivitiesInTeam);
                        proc.AddPara("@IsShowMarkDistVisitOnDshbrd", 1);

                        proc.AddPara("@IsRevisitRemarksMandatory", IsRevisitRemarksMandatory);
                        proc.AddPara("@GPSAlert", 1);
                        proc.AddPara("@GPSAlertwithSound", GPSAlertwithSound);
                        proc.AddPara("@FaceRegistrationFrontCamera", FaceRegistrationFrontCamera);
                        proc.AddPara("@MRPInOrder", MRPInOrder);
                        proc.AddPara("@isHorizontalPerformReportShow", isHorizontalPerformReportShow);
                        //Rev work start 26.07.2022 mantise no:25046
                        proc.AddPara("@FaceRegTypeID", FaceRegTypeID);
                        //Rev work close 26.07.2022 mantise no:25046
                        
                        proc.AddPara("@CalledFromUserAccount", 1);
                        proc.AddPara("@ChType_CFP", 1);
                        
                     }
                    else
                    {
                        proc.AddPara("@ACTION", "INSERT");
                        
                        //proc.AddPara("@txtusername", txtuserid.Text.Trim());
                        proc.AddPara("@txtusername", user_name);
                        
                        proc.AddPara("@b_id", cmbBranch.SelectedValue.ToString());
                        proc.AddPara("@txtuserid", txtuserid.Text.Trim());
                        proc.AddPara("@Encryptpass", Encryptpass);
                        proc.AddPara("@contact", emp_cntId);
                        proc.AddPara("@usergroup", ddlGroups.SelectedValue.ToString());
                        proc.AddPara("@CreateDate", CreateDate.ToString());
                        proc.AddPara("@CreateUser", CreateUser);
                        proc.AddPara("@superuser", "N");
                        proc.AddPara("@ddDataEntry", "F");
                        proc.AddPara("@IPAddress", "");
                        proc.AddPara("@isactive", isactive);
                        proc.AddPara("@isactivemac", isactivemac);
                        proc.AddPara("@txtgps", 500);

                        proc.AddPara("@istargetsettings", istargetsettings);
                        proc.AddPara("@isLeaveApprovalEnable", isLeaveApprovalEnable);
                        proc.AddPara("@IsAutoRevisitEnable", 0);
                        proc.AddPara("@IsShowPlanDetails", IsShowPlanDetails);
                        proc.AddPara("@IsMoreDetailsMandatory", IsMoreDetailsMandatory);
                        proc.AddPara("@IsShowMoreDetailsMandatory", IsShowMoreDetailsMandatory);

                        proc.AddPara("@isMeetingAvailable", isMeetingAvailable);
                        proc.AddPara("@isRateNotEditable", isRateNotEditable);
                        proc.AddPara("@IsShowTeamDetails", 0);
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
                        proc.AddPara("@isVisitShow", 0);
                        proc.AddPara("@iscollectioninMenuShow", iscollectioninMenuShow);
                        proc.AddPara("@isShopAddEditAvailable", 0);
                        proc.AddPara("@isEntityCodeVisible", isEntityCodeVisible);
                        proc.AddPara("@isAreaMandatoryInPartyCreation", isAreaMandatoryInPartyCreation);
                        proc.AddPara("@isShowPartyInAreaWiseTeam", isShowPartyInAreaWiseTeam);
                        proc.AddPara("@isChangePasswordAllowed", isChangePasswordAllowed);
                        proc.AddPara("@isHomeRestrictAttendance", 1);
                        proc.AddPara("@isQuotationShow", isQuotationShow);
                        proc.AddPara("@IsStateMandatoryinReport", IsStateMandatoryinReport);

                        proc.AddPara("@isAchievementEnable", isAchievementEnable);
                        proc.AddPara("@isTarVsAchvEnable", isTarVsAchvEnable);
                        proc.AddPara("@shopLocAccuracy", 500.00);
                        proc.AddPara("@homeLocDistance", "100.00");

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
                        proc.AddPara("@isShopEditEnable", 0);
                        proc.AddPara("@isTaskEnable", isTaskEnable);

                        // Rev 3.0
                        //proc.AddPara("@PartyType", "1");

                        if (IsShowUserAccountForITC == "1")
                        {
                            proc.AddPara("@PartyType", "1");
                        }
                        else
                        {
                            proc.AddPara("@PartyType", "0");
                        }
                        // End of Rev 3.0


                        proc.AddPara("@isAppInfoEnable", 1);
                        proc.AddPara("@willDynamicShow", willDynamicShow);
                        proc.AddPara("@willActivityShow", willActivityShow);
                        proc.AddPara("@isDocumentRepoShow", isDocumentRepoShow);
                        proc.AddPara("@isChatBotShow", isChatBotShow);
                        proc.AddPara("@isAttendanceBotShow", isAttendanceBotShow);
                        proc.AddPara("@isVisitBotShow", isVisitBotShow);
                        proc.AddPara("@appInfoMins", 99);


                        proc.AddPara("@isInstrumentCompulsory", isInstrumentCompulsory);
                        proc.AddPara("@isBankCompulsory", isBankCompulsory);

                        proc.AddPara("@isComplementaryUser", isComplementaryUser);
                        proc.AddPara("@isVisitPlanShow", isVisitPlanShow);
                        proc.AddPara("@isVisitPlanMandatory", isVisitPlanMandatory);
                        proc.AddPara("@isAttendanceDistanceShow", isAttendanceDistanceShow);
                        proc.AddPara("@willTimelineWithFixedLocationShow", willTimelineWithFixedLocationShow);
                        proc.AddPara("@isShowOrderRemarks", isShowOrderRemarks);
                        proc.AddPara("@isShowOrderSignature", isShowOrderSignature);
                        proc.AddPara("@isShowSmsForParty", isShowSmsForParty);
                        proc.AddPara("@isShowTimeline", 0);
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
                        proc.AddPara("@isMultipleVisitEnable", 0);
                        proc.AddPara("@isShowVisitRemarks", isShowVisitRemarks);
                        proc.AddPara("@isShowNearbyCustomer", 0);
                        proc.AddPara("@isServiceFeatureEnable", isServiceFeatureEnable);
                        proc.AddPara("@isPatientDetailsShowInOrder", isPatientDetailsShowInOrder);
                        proc.AddPara("@isPatientDetailsShowInCollection", isPatientDetailsShowInCollection);
                        proc.AddPara("@isAttachmentMandatory", isAttachmentMandatory);
                        proc.AddPara("@isShopImageMandatory", isShopImageMandatory);

                        proc.AddPara("@isLogShareinLogin", 1);
                        proc.AddPara("@IsCompetitorenable", IsCompetitorenable);
                        proc.AddPara("@IsOrderStatusRequired", IsOrderStatusRequired);
                        proc.AddPara("@IsCurrentStockEnable", IsCurrentStockEnable);
                        proc.AddPara("@IsCurrentStockApplicableforAll", IsCurrentStockApplicableforAll);
                        proc.AddPara("@IscompetitorStockRequired", IscompetitorStockRequired);
                        proc.AddPara("@IsCompetitorStockforParty", IsCompetitorStockforParty);
                        proc.AddPara("@ShowFaceRegInMenu", 0);
                        proc.AddPara("@IsFaceDetection", 1);

                        proc.AddPara("@IsUserwiseDistributer", 1);
                        proc.AddPara("@IsPhotoDeleteShow", 0);
                        proc.AddPara("@IsAllDataInPortalwithHeirarchy", 1);
                        proc.AddPara("@IsFaceDetectionWithCaptcha", IsFaceDetectionWithCaptcha);

                        proc.AddPara("@IsShowMenuAddAttendance", 0);
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

                        proc.AddPara("@IsShowPartyOnAppDashboard", 1);
                        proc.AddPara("@IsShowAttendanceOnAppDashboard", IsShowAttendanceOnAppDashboard);
                        proc.AddPara("@IsShowTotalVisitsOnAppDashboard", IsShowTotalVisitsOnAppDashboard);
                        proc.AddPara("@IsShowVisitDurationOnAppDashboard", IsShowVisitDurationOnAppDashboard);
                        proc.AddPara("@IsShowDayStart", 1);
                        proc.AddPara("@IsshowDayStartSelfie", IsshowDayStartSelfie);
                        proc.AddPara("@IsShowDayEnd", 1);
                        proc.AddPara("@IsshowDayEndSelfie", IsshowDayEndSelfie);
                        proc.AddPara("@IsShowLeaveInAttendance", IsShowLeaveInAttendance);
                        proc.AddPara("@IsLeaveGPSTrack", IsLeaveGPSTrack);
                        proc.AddPara("@IsShowActivitiesInTeam", IsShowActivitiesInTeam);
                        proc.AddPara("@IsShowMarkDistVisitOnDshbrd",0);

                        proc.AddPara("@IsRevisitRemarksMandatory", IsRevisitRemarksMandatory);
                        proc.AddPara("@GPSAlert", 1);
                        proc.AddPara("@GPSAlertwithSound", GPSAlertwithSound);
                        proc.AddPara("@FaceRegistrationFrontCamera", FaceRegistrationFrontCamera);
                        proc.AddPara("@MRPInOrder", MRPInOrder);
                        proc.AddPara("@isHorizontalPerformReportShow", isHorizontalPerformReportShow);                       
                        proc.AddPara("@FaceRegTypeID", FaceRegTypeID);
                        
                        proc.AddPara("@CalledFromUserAccount", 1);
                        proc.AddPara("@ChType_CFP", 0);
                        
                    }
                    

                    DataTable dt = proc.GetTable();

                    string[,] userid = oDBEngine.GetFieldValue("tbl_master_user", "max(user_id)", null, 1);
                    DataSet dsApprove = new DataSet();
                    dsApprove = oDBEngine.PopulateData("ID", "FTS_LEAVE_APPROVER", "APPROVAR_ID='" + userid[0, 0] + "'");

                    if (dsApprove.Tables["TableName"].Rows.Count > 0)
                    {
                        oDBEngine.DeleteValue("FTS_LEAVE_APPROVER", "APPROVAR_ID='" + userid[0, 0] + "'");
                    }

                    string splitsegname = segname[0, 0].Split('-')[0].ToString().Trim();
                    string[,] exchsegid = oDBEngine.GetFieldValue("Master_Exchange", "top 1 Exchange_Id", "Exchange_ShortName='" + splitsegname + "'", 1);

                    string FinancialYear = GetFinancialYear();
                    string[,] exhCntID = oDBEngine.GetFieldValue("Tbl_Master_Exchange", "top 1 exh_CntID", "Exh_ShortName= '" + splitsegname.ToString().Trim() + "'", 1);
                    string[,] userInternalId = oDBEngine.GetFieldValue("tbl_master_user", "user_Contactid", "user_id=" + userid[0, 0] + "", 1);
                    DataTable dtcmp = oDBEngine.GetDataTable(" tbl_master_company  ", "*", "cmp_id=(select top 1 emp_organization  from tbl_trans_employeectc where emp_cntId='" + userInternalId[0, 0] + "' and emp_id=(select MAX(emp_id) from tbl_trans_employeectc e where e.emp_cntId='" + userInternalId[0, 0] + "'))");

                    if (dtcmp.Rows.Count > 0)
                    {
                        string SegmentId = "1";
                        oDBEngine.InsurtFieldValue("Master_UserCompany", "UserCompany_UserID,UserCompany_CompanyID,UserCompany_CreateUser,UserCompany_CreateDateTime", "'" + userid[0, 0] + "','" + Convert.ToString(dtcmp.Rows[0]["cmp_internalid"]) + "','" + Convert.ToString(Session["userid"]) + "','" + DateTime.Now + "'");
                        oDBEngine.InsurtFieldValue("tbl_trans_LastSegment", "ls_cntid,ls_lastsegment,ls_userid,ls_lastdpcoid,ls_lastCompany,ls_lastFinYear,ls_lastSettlementNo,ls_lastSettlementType", "'" + emp_cntId + "','" + grpsegment[0, 0] + "','" + userid[0, 0] + "','" + SegmentId + "','" + Convert.ToString(dtcmp.Rows[0]["cmp_internalid"]) + "','" + FinancialYear.Trim() + "','','N'");

                    }
                    else
                    {
                        string[,] companymain = oDBEngine.GetFieldValue("Tbl_Master_companyExchange", "top 1 Exch_InternalID,Exch_CompID", "Exch_ExchID='" + exhCntID[0, 0].ToString().Trim() + "' and exch_segmentId='1'", 2);
                        oDBEngine.InsurtFieldValue("tbl_trans_LastSegment", "ls_cntid,ls_lastsegment,ls_userid,ls_lastdpcoid,ls_lastCompany,ls_lastFinYear,ls_lastSettlementNo,ls_lastSettlementType", "'" + emp_cntId + "','" + grpsegment[0, 0] + "','" + userid[0, 0] + "','" + companymain[0, 0] + "','" + companymain[0, 1].ToString() + "','" + FinancialYear.Trim() + "','','N'");
                    }
                    //--------------------------------
                    //Response.Redirect("/OMS/Management/Master/User_Account.aspx");

                    //int NoOfRowEffected = proc.RunActionQuery();
                    //return NoOfRowEffected;
                    int returnmsg = 0;
                    if(dt!=null)
                    {
                        returnmsg = 0;
                    }
                    else
                    {
                        returnmsg = 1;
                    }
                    return returnmsg;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                proc = null;
            }
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
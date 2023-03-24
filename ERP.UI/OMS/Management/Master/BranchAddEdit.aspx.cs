//====================================================== Revision History ================================================================
//Rev Number DATE              VERSION          DEVELOPER           CHANGES
//1.0        22-03-2023        2.0.39           Priti               0025745 :While click the Add button of Branch Master, it is taking some time to load the page & take the input    
//2.0        22-03-2023        2.0.39           Sanchita            While creating a new Branch, that branch should be mapped automatically for System Admin Employee/User
//                                                                  Refer: 25744
//====================================================== Revision History ================================================================


using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using DevExpress.Web.ASPxClasses;
//using DevExpress.Web.ASPxEditors;
//using DevExpress.Web.ASPxGridView;
//using DevExpress.Web.ASPxTabControl;
using DevExpress.Web;
using System.Configuration;
using ClsDropDownlistNameSpace;
using EntityLayer.CommonELS;
using System.Web.Services;
using System.Collections.Generic;
using BusinessLogicLayer;
using DataAccessLayer;

namespace ERP.OMS.Managemnent.Master
{
    public partial class management_Master_BranchAddEdit : ERP.OMS.ViewState_class.VSPage
    {
        public EntityLayer.CommonELS.UserRightsForPage rights = new UserRightsForPage();
        BusinessLogicLayer.DBEngine objEngine = new BusinessLogicLayer.DBEngine(ConfigurationManager.AppSettings["DBConnectionDefault"]);
        public string pageAccess = "";
        public static string brId = string.Empty;
        BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(string.Empty);
        string checking = "a";
        public static string branchid = "";
        BusinessLogicLayer.Converter oConverter = new BusinessLogicLayer.Converter();

        BusinessLogicLayer.GenericStoreProcedure oGenericStoreProcedure;
        clsDropDownList clsdropdown = new clsDropDownList();
        BusinessLogicLayer.RemarkCategoryBL reCat = new BusinessLogicLayer.RemarkCategoryBL();
        // Rev 2.0
        public bool ActivateEmployeeBranchHierarchy { get; set; }
        // End of Rev 2.0
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Code  Added and Commented By Priti on 21122016 to add Convert.ToString instead of ToString()
            if (!IsPostBack)
            {
                //'http://localhost:2957/InfluxCRM/management/testProjectMainPage_employee.aspx'
                string sPath = Convert.ToString(HttpContext.Current.Request.Url);
                oDBEngine.Call_CheckPageaccessebility(sPath);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/management/Master/BranchAddEdit.aspx");
                Session["requesttype"] = "Branches";
                Session["ContactType"] = "Branches";
                TabPage page = PageControl1.TabPages.FindByName("Documents");
                page.Enabled = true;
                page = PageControl1.TabPages.FindByName("UDF");
                page.Enabled = true;
                btnUdf.Attributes.Remove("disabled");
                btnUdf.Enabled = true;
                //if (HttpContext.Current.Session["userid"] == null)
                //{
                //    //Page.ClientScript.RegisterStartupScript(GetType(), "SighOff", "<script>SignOff();</script>");
                //}
                // Rev 2.0
                CommonBL cbl = new CommonBL();
                string mastersettings = cbl.GetSystemSettingsResult("IsActivateEmployeeBranchHierarchy");
                if (mastersettings == "0")
                {
                    ActivateEmployeeBranchHierarchy = false;
                }
                else
                {
                    ActivateEmployeeBranchHierarchy = true;
                }
                // End of Rev 2.0

                if (!IsPostBack)
                {
                    //Debjyoti 23-12-2016
                    //Reason: UDF count
                    IsUdfpresent.Value = Convert.ToString(getUdfCount());
                    //End Debjyoti 23-12-2016

                    ShowForm();
                    SetCountry();
                    SetMainAccount();
                }

                gridTerminalId.JSProperties["cpCompCombo"] = null;
            }
            catch { }
        }

        protected void ShowForm()
        {
            if (Convert.ToString(Request.QueryString["id"]) == "ADD")
            {
                Session["requesttype"] = "";
                Session["ContactType"] = "";

                string[,] Data = oDBEngine.GetFieldValue("tbl_master_branch ", "branch_id as parentId, branch_description as ParentBranch", null, 2, "branch_description");
                clsdropdown.AddDataToDropDownList(Data, cmbParentBranch);
                Data = oDBEngine.GetFieldValue("tbl_master_regions", "reg_id, reg_region", null, 2, "reg_region");
                clsdropdown.AddDataToDropDownList(Data, cmbBranchRegion);
                cmbBranchRegion.Items.Insert(0, new ListItem("--Select--", "0"));
                cmbParentBranch.Items.Insert(0, new ListItem("None", "0"));
                TabPage page = PageControl1.TabPages.FindByName("Documents");
                page.Enabled = false;
                page = PageControl1.TabPages.FindByName("UDF");
                page.Enabled = false;
                Keyval_internalId.Value = "Add";
                
            }
            else
            {
               

                if (Request.QueryString["id"] != null)
                {
                    branchid = Convert.ToString(Request.QueryString["id"]);
                    Session["assignedBranch"]=branchid;
                }
                else
                {
                    if(Session["assignedBranch"]!=null)
                     {
                         branchid = Convert.ToString(Session["assignedBranch"]);
                      }
                }

                //if (Request.QueryString["id"] != null)
                if (branchid != "")
                {
                    brId = branchid;//Convert.ToString(Request.QueryString["id"]);
                    Session["con_branch"] = branchid;// Convert.ToString(Request.QueryString["id"]);
                    string[,] DT = oDBEngine.GetFieldValue("tbl_master_branch ", "tbl_master_branch.branch_id, tbl_master_branch.branch_internalId, tbl_master_branch.branch_code, tbl_master_branch.branch_type, tbl_master_branch.branch_parentId, tbl_master_branch.branch_description, tbl_master_branch.branch_regionid, tbl_master_branch.branch_address1, tbl_master_branch.branch_address2,tbl_master_branch.branch_address3, tbl_master_branch.branch_country, tbl_master_branch.branch_state, tbl_master_branch.branch_pin, tbl_master_branch.branch_city, tbl_master_branch.branch_phone, tbl_master_branch.branch_head, tbl_master_branch.branch_contactPerson, tbl_master_branch.branch_cpPhone,tbl_master_branch.branch_cpEmail, tbl_master_branch.CreateDate, tbl_master_branch.CreateUser, tbl_master_branch.LastModifyDate, tbl_master_branch.LastModifyUser, tbl_master_branch.branch_Fax, (case when tbl_master_branch.branch_parentId = 0 then 'None' else (select A.branch_description from tbl_master_branch A where A.branch_id=tbl_master_branch.branch_parentId) end) as ParentBranch, tbl_master_branch.branch_area,tbl_master_branch.branch_GSTIN,branch_MainAccount,tbl_master_branch.branch_CIN,Case When IsNull(convert(nvarchar(10),tbl_master_branch.branch_CINdt,105),'')='01-01-1900' Then '' Else IsNull(convert(nvarchar(10),tbl_master_branch.branch_CINdt,105),'') End as branch_CINdt ", "branch_id=" + branchid, 30);
                   
                    if (DT[0, 0] != "n")
                    {

                        Session["branch_InternalId"] = Convert.ToString( DT[0, 1]);
                        Session["KeyVal_InternalID_New"] = Convert.ToString(DT[0, 1]);
                        string GSTIN = "";
                        if (DT[0, 26] != "")
                        {

                            GSTIN = DT[0, 26];
                            txtGSTIN1.Text = GSTIN.Substring(0, 2);
                            txtGSTIN2.Text = GSTIN.Substring(2, 10);
                            txtGSTIN3.Text = GSTIN.Substring(12, 3);
                        }


                        DataSet dsbranchhrchy = new DataSet();
                        string[] strSpParam = new string[1];
                        strSpParam[0] = "branchid|" + BusinessLogicLayer.GenericStoreProcedure.ParamDBType.Int + "|10|" + branchid + "|" + BusinessLogicLayer.GenericStoreProcedure.ParamType.ExParam;

                        oGenericStoreProcedure = new BusinessLogicLayer.GenericStoreProcedure();

                        dsbranchhrchy = oGenericStoreProcedure.Procedure_DataSet(strSpParam, "Hr_GetBranchSubTree");
                        string[,] Data = oDBEngine.GetFieldValue("tbl_master_branch ", "branch_id as parentId, branch_description as ParentBranch", "branch_id not in (" + (dsbranchhrchy.Tables[0].Rows[0][0].ToString()) + ")", 2, "branch_description");
                        if (Data[0, 0] != "n")
                        {
                            //....................................... Code commented and updated by Sam on 29092016..............................

                            if (DT[0, 4] != "")
                            {
                                clsdropdown.AddDataToDropDownList(Data, cmbParentBranch, int.Parse(Convert.ToString(DT[0, 4])));
                                cmbParentBranch.Items.Insert(0, new ListItem("None", "0"));
                                cmbParentBranch.SelectedValue = DT[0, 4];
                            }
                            else
                            {

                                clsdropdown.AddDataToDropDownList(Data, cmbParentBranch, 0);
                            }
                            //if (DT[0, 4] != "")
                            //{
                            //    clsdropdown.AddDataToDropDownList(Data, cmbParentBranch, int.Parse(DT[0, 4].ToString()));
                            //}
                            //else
                            //{

                            //    clsdropdown.AddDataToDropDownList(Data, cmbParentBranch, 0);
                            //}
                            //....................................... Code above commented and updated by Sam on 29092016..............................
                        }
                        else
                        {
                            cmbParentBranch.Items.Insert(0, new ListItem("None", "0"));
                        }


                        Data = oDBEngine.GetFieldValue("tbl_master_regions", "reg_id, reg_region", null, 2, "reg_region");
                        if (DT[0, 6] != "")
                        {
                            clsdropdown.AddDataToDropDownList(Data, cmbBranchRegion, int.Parse(Convert.ToString(DT[0, 6])));
                            cmbBranchRegion.Items.Insert(0, new ListItem("--Select--", "0"));
                            cmbBranchRegion.SelectedValue = DT[0, 6];
                        }
                        else
                        {
                            clsdropdown.AddDataToDropDownList(Data, cmbBranchRegion, 0);
                        }

                        txtCode.Text = DT[0, 2];
                        txtBranchDesc.Text = DT[0, 5];
                        txtAddress1.Text = DT[0, 7];
                        txtAddress2.Text = DT[0, 8];
                        txtAddress3.Text = DT[0, 9];
                        cmbBranchType.SelectedValue = DT[0, 3];
                        Session["Name"] = txtBranchDesc.Text.Trim();
                        

                        txtCountry_hidden.Value = DT[0, 10];

                        
                        txtState_hidden.Value = DT[0, 11];


                       

                        txtCity_hidden.Value = DT[0, 13];
                  



                        HdPin.Value = DT[0, 12];

                        txtPhone.Text = DT[0, 14];
                        txtFax.Text = DT[0, 23];
                        hdLstArea.Value = DT[0, 25];
                        if (Convert.ToString(DT[0, 15]).Length > 0)
                        {
                            DataTable dtBRHD = oDBEngine.GetDataTable(" tbl_master_contact", " ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') +'['+cnt_shortName+']' AS Name  ", "  cnt_internalid='" + DT[0, 15] + "' ");

                            if (dtBRHD.Rows.Count > 0)
                            {

                                txtBranchHead.Text = Convert.ToString(dtBRHD.Rows[0][0]);
                            }
                            else
                            {

                                txtBranchHead.Text = "";
                            }
                        }
                        else
                        {
                            //  txtCity.Text = "";
                        }
                        txtBranchHead_hidden.Value = DT[0, 15];
                        txtBranchHead.Text = DT[0, 15];
                        txtContPhone.Text = DT[0, 17];
                        txtContPerson.Text = DT[0, 16];
                        txtContEmail.Text = DT[0, 18];

                       
                        Keyval_internalId.Value = Convert.ToString(DT[0, 1]);
                        cmbBranchRegion.Items.Insert(0, new ListItem("--Select--", "0"));
                       

                        hdlstMainAccount.Value = DT[0, 27];

                        txtCIN.Text = DT[0, 28];


                         if (DT[0, 29] != "")
                           {
                            txtCINVdate.Value = Convert.ToDateTime(DT[0, 29]);
                           }
                      
                    }
                }
            }

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                if (Convert.ToString(Request.QueryString["id"]) == "ADD")
                {
                    string BranchType = "";
                    string Code = "";
                    string PBranch = "";
                    string Description = "";
                    string RId = "";
                    string Add1 = "";
                    string Add2 = "";
                    string Add3 = "";
                    string Country = "";
                    string State = "";
                    string City = "";
                    string area = "";
                    string Pin = "";
                    string Phone = "";
                    string BHead = "";
                    string CPerson = "";
                    string CPersonPhone = "";
                    string Email = "";
                    string Fax = "";
                    string mainAccount = "";

                    if (hdlstMainAccount.Value != "")
                    {
                        mainAccount = hdlstMainAccount.Value;
                    }
                    if (cmbBranchType.SelectedItem.Value != "")
                    {
                        BranchType = cmbBranchType.SelectedItem.Value;
                    }
                    else
                    {
                        BranchType = Convert.ToString(DBNull.Value);
                    }
                    if (txtCode.Text.Trim() != "")
                    {
                        Code = Convert.ToString(txtCode.Text);
                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Notify", "alert('Please enter short name');", true);
                        return;
                        
                    }
                    if (cmbParentBranch.SelectedItem.Value != "")
                    {
                        PBranch = cmbParentBranch.SelectedItem.Value;
                    }
                    else
                    {
                        PBranch = Convert.ToString(DBNull.Value);
                    }
                    if (txtBranchDesc.Text != "")
                    {
                        Description = Convert.ToString(txtBranchDesc.Text);
                    }
                    else
                    {
                        Description = Convert.ToString(DBNull.Value);
                    }
                    if (cmbBranchRegion.SelectedItem.Value != "")
                    {
                        RId = cmbBranchRegion.SelectedItem.Value;
                    }
                    else
                    {
                        RId = Convert.ToString(DBNull.Value);
                    }
                    if (txtAddress1.Text != "")
                    {
                        Add1 = Convert.ToString(txtAddress1.Text);
                    }
                    else
                    {
                        Add1 = Convert.ToString(DBNull.Value);
                    }
                    if (txtAddress2.Text != "")
                    {
                        Add2 = Convert.ToString(txtAddress2.Text);
                    }
                    else
                    {
                        Add2 = Convert.ToString(DBNull.Value);
                    }
                    if (txtAddress3.Text != "")
                    {
                        Add3 = Convert.ToString(txtAddress3.Text);
                    }
                    else
                    {
                        Add3 = Convert.ToString(DBNull.Value);
                    }
                    if (txtCountry_hidden.Value != "")
                    {
                        Country = txtCountry_hidden.Value;
                    }
                    else
                    {
                        Country = Convert.ToString(DBNull.Value);
                    }
                    if (txtState_hidden.Value != "")
                    {
                        State = txtState_hidden.Value;
                    }
                    else
                    {
                        State = Convert.ToString(DBNull.Value);
                    }
                    if (txtCity_hidden.Value != "")
                    {
                        City = txtCity_hidden.Value;
                    }
                    else
                    {
                        City = Convert.ToString(DBNull.Value);
                    }
                    if (hdLstArea.Value != "")
                    {
                        area = hdLstArea.Value;
                    }
                    else
                    {
                        area = Convert.ToString(DBNull.Value);
                    }
                  
                    if (HdPin.Value != "")
                    {
                        Pin = Convert.ToString(HdPin.Value);
                    }
                    else
                    {
                        Pin = Convert.ToString(DBNull.Value);
                    }

                    if (txtPhone.Text != "")
                    {
                        Phone = Convert.ToString(txtPhone.Text);

                    }
                    else
                    {
                        Phone = Convert.ToString(DBNull.Value);
                    }
                    if (txtBranchHead_hidden.Value != "")
                    {
                        BHead = txtBranchHead_hidden.Value;
                    }
                    else
                    {
                        BHead = Convert.ToString(DBNull.Value);
                    }
                    if (txtContPerson.Text != "")
                    {
                        CPerson = Convert.ToString(txtContPerson.Text);
                    }
                    else
                    {
                        CPerson = Convert.ToString(DBNull.Value);
                    }
                    if (txtContPhone.Text != "")
                    {
                        CPersonPhone = Convert.ToString(txtContPhone.Text);
                    }
                    else
                    {
                        CPersonPhone = Convert.ToString(DBNull.Value);
                    }
                    if (txtContEmail.Text != "")
                    {

                        Email = Convert.ToString(txtContEmail.Text);

                    }
                    else
                    {
                        Email = Convert.ToString(DBNull.Value);
                    }

                    if (txtFax.Text != "")
                    {
                        Fax = Convert.ToString(txtFax.Text);
                    }
                    else
                    {
                        Fax = Convert.ToString(DBNull.Value);

                    }
                    int noofRows = 0;
                    string firstChar = string.Empty;
                    firstChar = Convert.ToString(txtBranchDesc.Text);
                    if (firstChar != "")
                    {
                        firstChar = firstChar.Substring(0, 1).ToUpper();
                    }

                    // Mantis Issue 25530
                    //if (!reCat.isAllMandetoryDone((DataTable)Session["UdfDataOnAdd"], "Br"))
                    //{
                    //    Page.ClientScript.RegisterStartupScript(GetType(), "udferror", "<script>udfError()</script>");
                    //    return;

                    //}
                    // End of Mantis Issue 25530

                    //Debjyoti GSTIN  
                    string GSTIN = "";
                    GSTIN = txtGSTIN1.Text.Trim() + txtGSTIN2.Text.Trim() + txtGSTIN3.Text.Trim();

                    string CIN = txtCIN.Text;
                  



                    string NewId = objEngine.GetInternalId("BR" + firstChar, "tbl_master_branch", "branch_internalId", "branch_internalId");
                    Session["KeyVal_InternalID"] = NewId;
                    string[,] count = oDBEngine.GetFieldValue("tbl_master_branch", "branch_parentid", "branch_parentid=0", 1);
                    if (count[0, 0] == "n")
                    {
                        noofRows = objEngine.InsurtFieldValue("tbl_master_branch", "branch_internalId,branch_code,branch_parentId,branch_description,branch_address1,branch_address2,branch_address3,branch_country,branch_state,branch_city,branch_pin, branch_phone, branch_type,branch_regionid, branch_head,branch_contactPerson,branch_cpPhone,branch_cpEmail,CreateDate,CreateUser, branch_Fax, branch_area,branch_GSTIN,branch_MainAccount,branch_CIN,branch_CINdt", "'" + NewId + "','" + Code + "','" + PBranch + "','" + Description + "','" + Add1 + "','" + Add2 + "','" + Add3 + "','" + Country + "','" + State + "','" + City + "','" + Pin + "','" + Phone + "','" + BranchType + "','" + RId + "','" + BHead + "','" + CPerson + "','" + CPersonPhone + "','" + Email + "','" + oDBEngine.GetDate().ToString() + "','" + Session["userid"].ToString() + "','" + Fax.ToString() + "','" + area + "','" + GSTIN + "','" + mainAccount + "','" + CIN + "','" + txtCINVdate.Value + "'");

                        
                        if (noofRows > 0)
                        {
                            // Rev 2.0
                            if (ActivateEmployeeBranchHierarchy == false)
                            {
                                DataTable dt = new DataTable();
                                ProcedureExecute proc = new ProcedureExecute("Prc_BranchMasterDetails");
                                proc.AddPara("@action", "UpdateEmployeeBranchMap");
                                proc.AddPara("@branch_internalId", NewId);
                                dt = proc.GetTable();
                            }
                            // End of Rev 2.0

                            Page.ClientScript.RegisterStartupScript(GetType(), "pagecall", "<script>alert('Saved Successfully..');   window.location.href='/OMS/management/Master/branch.aspx'</script>");
                        }
                    }
                    else
                    {

                        //................. Code Commented by Sam On 29092016.....................................

                        noofRows = objEngine.InsurtFieldValue("tbl_master_branch", "branch_internalId,branch_code,branch_parentId,branch_description,branch_address1,branch_address2,branch_address3,branch_country,branch_state,branch_city,branch_pin, branch_phone, branch_type,branch_regionid, branch_head,branch_contactPerson,branch_cpPhone,branch_cpEmail,CreateDate,CreateUser, branch_Fax, branch_area,branch_GSTIN,branch_MainAccount,branch_CIN,branch_CINdt", "'" + NewId + "','" + Code + "','" + PBranch + "','" + Description + "','" + Add1 + "','" + Add2 + "','" + Add3 + "','" + Country + "','" + State + "','" + City + "','" + Pin + "','" + Phone + "','" + BranchType + "','" + RId + "','" + BHead + "','" + CPerson + "','" + CPersonPhone + "','" + Email + "','" + oDBEngine.GetDate().ToString() + "','" + Session["userid"].ToString() + "','" + Fax.ToString() + "','" + area + "','" + GSTIN + "','" + mainAccount + "','" + CIN + "','" + txtCINVdate.Value + "'");
                        if (noofRows > 0)
                        {

                             //Udf Add mode
                            DataTable udfTable = (DataTable)Session["UdfDataOnAdd"];
                            if (udfTable != null)
                                Session["UdfDataOnAdd"] = reCat.insertRemarksCategoryAddMode("Br", NewId, udfTable, Convert.ToString(Session["userid"]));

                            // Rev 2.0
                            if (ActivateEmployeeBranchHierarchy == false)
                            {
                                DataTable dt = new DataTable();
                                ProcedureExecute proc = new ProcedureExecute("Prc_BranchMasterDetails");
                                proc.AddPara("@action", "UpdateEmployeeBranchMap");
                                proc.AddPara("@branch_internalId", NewId);
                                dt = proc.GetTable();
                            }
                            // End of Rev 2.0

                            Page.ClientScript.RegisterStartupScript(GetType(), "pagecall456", "<script>alert('Saved Successfully..');    window.location.href='/OMS/management/Master/branch.aspx'</script>");
                        }
                       
                        //................. Code Above Commented by Sam On 29092016.....................................
                    }

                }
                else
                {
                    if (txtCode.Text.Trim() == "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Notify", "alert('Please enter short name');", true);
                        return;
                      
                    }

                    string GSTIN = "";
                    GSTIN = txtGSTIN1.Text.Trim() + txtGSTIN2.Text.Trim() + txtGSTIN3.Text.Trim();
                    RequiredFieldValidator4.Visible = false;
                    RequiredFieldValidator3.Visible = false;
                    string[,] originalparent = oDBEngine.GetFieldValue("tbl_master_branch", "branch_parentid", "branch_id=" + branchid + "", 1);
                    //string[,] originalparent = oDBEngine.GetFieldValue("tbl_master_branch", "branch_parentid", "branch_id=" + Request.QueryString["id"] + "", 1);
                    string[,] count1 = oDBEngine.GetFieldValue("tbl_master_branch", "branch_parentid", "branch_parentid=0", 1);
                    if (count1[0, 0] == "n")
                    {
                       
                        //oDBEngine.SetFieldValue("tbl_master_branch", "branch_code ='" + Convert.ToString(txtCode.Text).Trim() + "',branch_parentId='" + cmbParentBranch.SelectedItem.Value + "',branch_description='" + Convert.ToString(txtBranchDesc.Text).Trim() + "',branch_address1='" + Convert.ToString(txtAddress1.Text).Trim() + "',branch_address2='" + Convert.ToString(txtAddress2.Text).Trim() + "',branch_address3='" + Convert.ToString(txtAddress3.Text).Trim() + "',branch_country='" + txtCountry_hidden.Value + "',branch_state='" + txtState_hidden.Value + "',branch_city='" + txtCity_hidden.Value + "',branch_pin='" + Convert.ToString(HdPin.Value) + "', branch_phone='" + Convert.ToString(txtPhone.Text).Trim() + "', branch_type ='" + cmbBranchType.SelectedItem.Value + "',branch_regionid='" + cmbBranchRegion.SelectedItem.Value + "' , branch_head='" + txtBranchHead_hidden.Value + "',branch_contactPerson='" + Convert.ToString(txtContPerson.Text).Trim() + "',branch_cpPhone='" + Convert.ToString(txtContPhone.Text).Trim() + "',branch_cpEmail='" + Convert.ToString(txtContEmail.Text).Trim() + "', branch_Fax='" + Convert.ToString(txtFax.Text).Trim() + "' ,LastModifyUser='" + Convert.ToString(Session["userid"]) + "',LastModifyDate=getdate() ,branch_area='" + hdLstArea.Value + "',branch_GSTIN='" + GSTIN + "',branch_MainAccount='" + Convert.ToString(hdlstMainAccount.Value) + "',branch_CIN='" + txtCIN.Text + "',branch_CINdt='" + txtCINVdate.Text + "'", " branch_ID=" + Request.QueryString["id"] + "");
                        oDBEngine.SetFieldValue("tbl_master_branch", "branch_code ='" + Convert.ToString(txtCode.Text).Trim() + "',branch_parentId='" + cmbParentBranch.SelectedItem.Value + "',branch_description='" + Convert.ToString(txtBranchDesc.Text).Trim() + "',branch_address1='" + Convert.ToString(txtAddress1.Text).Trim() + "',branch_address2='" + Convert.ToString(txtAddress2.Text).Trim() + "',branch_address3='" + Convert.ToString(txtAddress3.Text).Trim() + "',branch_country='" + txtCountry_hidden.Value + "',branch_state='" + txtState_hidden.Value + "',branch_city='" + txtCity_hidden.Value + "',branch_pin='" + Convert.ToString(HdPin.Value) + "', branch_phone='" + Convert.ToString(txtPhone.Text).Trim() + "', branch_type ='" + cmbBranchType.SelectedItem.Value + "',branch_regionid='" + cmbBranchRegion.SelectedItem.Value + "' , branch_head='" + txtBranchHead_hidden.Value + "',branch_contactPerson='" + Convert.ToString(txtContPerson.Text).Trim() + "',branch_cpPhone='" + Convert.ToString(txtContPhone.Text).Trim() + "',branch_cpEmail='" + Convert.ToString(txtContEmail.Text).Trim() + "', branch_Fax='" + Convert.ToString(txtFax.Text).Trim() + "' ,LastModifyUser='" + Convert.ToString(Session["userid"]) + "',LastModifyDate=getdate() ,branch_area='" + hdLstArea.Value + "',branch_GSTIN='" + GSTIN + "',branch_MainAccount='" + Convert.ToString(hdlstMainAccount.Value) + "',branch_CIN='" + txtCIN.Text + "',branch_CINdt='" + txtCINVdate.Text + "'", " branch_ID=" + branchid + "");
                        RequiredFieldValidator4.Visible = false;
                        RequiredFieldValidator3.Visible = false;
                        Page.ClientScript.RegisterStartupScript(GetType(), "pagecalldd", "<script>jAlert('Branch updated successfully.');</script>");
                    }
                    else
                    {


                        if (cmbParentBranch.SelectedItem.Value == originalparent[0, 0])
                        {
                            //oDBEngine.SetFieldValue("tbl_master_branch", "branch_code ='" + Convert.ToString(txtCode.Text).Trim() + "',branch_parentId='" + cmbParentBranch.SelectedItem.Value + "',branch_description='" + Convert.ToString(txtBranchDesc.Text).Trim() + "',branch_address1='" + Convert.ToString(txtAddress1.Text).Trim() + "',branch_address2='" + Convert.ToString(txtAddress2.Text).Trim() + "',branch_address3='" + Convert.ToString(txtAddress3.Text).Trim() + "',branch_country='" + txtCountry_hidden.Value + "',branch_state='" + txtState_hidden.Value + "',branch_city='" + txtCity_hidden.Value + "',branch_pin='" + Convert.ToString(HdPin.Value) + "', branch_phone='" + Convert.ToString(txtPhone.Text).Trim() + "', branch_type ='" + cmbBranchType.SelectedItem.Value + "',branch_regionid='" + cmbBranchRegion.SelectedItem.Value + "' , branch_head='" + txtBranchHead_hidden.Value + "',branch_contactPerson='" + Convert.ToString(txtContPerson.Text).Trim() + "',branch_cpPhone='" + Convert.ToString(txtContPhone.Text).Trim() + "',branch_cpEmail='" + Convert.ToString(txtContEmail.Text).Trim() + "', branch_Fax='" + Convert.ToString(txtFax.Text).Trim() + "' ,LastModifyUser='" + Convert.ToString(Session["userid"]) + "',LastModifyDate=getdate()  ,branch_area='" + hdLstArea.Value + "',branch_GSTIN='" + GSTIN + "',branch_MainAccount='" + Convert.ToString(hdlstMainAccount.Value) + "',branch_CIN='" + txtCIN.Text + "',branch_CINdt='" + txtCINVdate.Text + "'", " branch_ID=" + Request.QueryString["id"] + "");
                            oDBEngine.SetFieldValue("tbl_master_branch", "branch_code ='" + Convert.ToString(txtCode.Text).Trim() + "',branch_parentId='" + cmbParentBranch.SelectedItem.Value + "',branch_description='" + Convert.ToString(txtBranchDesc.Text).Trim() + "',branch_address1='" + Convert.ToString(txtAddress1.Text).Trim() + "',branch_address2='" + Convert.ToString(txtAddress2.Text).Trim() + "',branch_address3='" + Convert.ToString(txtAddress3.Text).Trim() + "',branch_country='" + txtCountry_hidden.Value + "',branch_state='" + txtState_hidden.Value + "',branch_city='" + txtCity_hidden.Value + "',branch_pin='" + Convert.ToString(HdPin.Value) + "', branch_phone='" + Convert.ToString(txtPhone.Text).Trim() + "', branch_type ='" + cmbBranchType.SelectedItem.Value + "',branch_regionid='" + cmbBranchRegion.SelectedItem.Value + "' , branch_head='" + txtBranchHead_hidden.Value + "',branch_contactPerson='" + Convert.ToString(txtContPerson.Text).Trim() + "',branch_cpPhone='" + Convert.ToString(txtContPhone.Text).Trim() + "',branch_cpEmail='" + Convert.ToString(txtContEmail.Text).Trim() + "', branch_Fax='" + Convert.ToString(txtFax.Text).Trim() + "' ,LastModifyUser='" + Convert.ToString(Session["userid"]) + "',LastModifyDate=getdate()  ,branch_area='" + hdLstArea.Value + "',branch_GSTIN='" + GSTIN + "',branch_MainAccount='" + Convert.ToString(hdlstMainAccount.Value) + "',branch_CIN='" + txtCIN.Text + "',branch_CINdt='" + txtCINVdate.Text + "'", " branch_ID=" + branchid + "");

                          

                            Page.ClientScript.RegisterStartupScript(GetType(), "pagecalldddasxxd", "<script>jAlert('Branch updated successfully.');</script>");
                        }
                        else
                        {
                           
                            if (Request.QueryString["id"] == cmbParentBranch.SelectedItem.Value)
                            {
                                Page.ClientScript.RegisterStartupScript(GetType(), "pagecallddddd", "<script>alert('Parent Branch Can not be Same Branch');</script>");
                                cmbParentBranch.Focus();
                            }
                          
                            else
                            {
                                //oDBEngine.SetFieldValue("tbl_master_branch", "branch_code ='" + Convert.ToString(txtCode.Text).Trim() + "',branch_parentId='" + cmbParentBranch.SelectedItem.Value + "',branch_description='" + Convert.ToString(txtBranchDesc.Text).Trim() + "',branch_address1='" + txtAddress1.Text.ToString().Trim() + "',branch_address2='" + txtAddress2.Text.ToString().Trim() + "',branch_address3='" + txtAddress3.Text.ToString().Trim() + "',branch_country='" + txtCountry_hidden.Value + "',branch_state='" + txtState_hidden.Value + "',branch_city='" + txtCity_hidden.Value + "',branch_pin='" + Convert.ToString(HdPin.Value) + "', branch_phone='" + txtPhone.Text.ToString().Trim() + "', branch_type ='" + cmbBranchType.SelectedItem.Value + "',branch_regionid='" + cmbBranchRegion.SelectedItem.Value + "' , branch_head='" + txtBranchHead_hidden.Value + "',branch_contactPerson='" + txtContPerson.Text.ToString().Trim() + "',branch_cpPhone='" + txtContPhone.Text.ToString().Trim() + "',branch_cpEmail='" + txtContEmail.Text.ToString().Trim() + "', branch_Fax='" + txtFax.Text.ToString().Trim() + "' ,LastModifyUser='" + Session["userid"].ToString() + "',LastModifyDate=getdate(),branch_MainAccount='" + Convert.ToString(hdlstMainAccount.Value) + "'", " branch_ID=" + Request.QueryString["id"] + "");
                                oDBEngine.SetFieldValue("tbl_master_branch", "branch_code ='" + Convert.ToString(txtCode.Text).Trim() + "',branch_parentId='" + cmbParentBranch.SelectedItem.Value + "',branch_description='" + Convert.ToString(txtBranchDesc.Text).Trim() + "',branch_address1='" + txtAddress1.Text.ToString().Trim() + "',branch_address2='" + txtAddress2.Text.ToString().Trim() + "',branch_address3='" + txtAddress3.Text.ToString().Trim() + "',branch_country='" + txtCountry_hidden.Value + "',branch_state='" + txtState_hidden.Value + "',branch_city='" + txtCity_hidden.Value + "',branch_pin='" + Convert.ToString(HdPin.Value) + "', branch_phone='" + txtPhone.Text.ToString().Trim() + "', branch_type ='" + cmbBranchType.SelectedItem.Value + "',branch_regionid='" + cmbBranchRegion.SelectedItem.Value + "' , branch_head='" + txtBranchHead_hidden.Value + "',branch_contactPerson='" + txtContPerson.Text.ToString().Trim() + "',branch_cpPhone='" + txtContPhone.Text.ToString().Trim() + "',branch_cpEmail='" + txtContEmail.Text.ToString().Trim() + "', branch_Fax='" + txtFax.Text.ToString().Trim() + "' ,LastModifyUser='" + Session["userid"].ToString() + "',LastModifyDate=getdate(),branch_MainAccount='" + Convert.ToString(hdlstMainAccount.Value) + "'", " branch_ID=" + branchid + "");

                                
                                Page.ClientScript.RegisterStartupScript(GetType(), "pagecalldddd", "<script>jAlert('Branch updated successfully');</script>");
                            }

                        }
                    }
                }
            }
            catch { }
        }

        protected void comboExchange_Callback(object source, CallbackEventArgsBase e)
        {
            //   ASPxPageControl pControl1 = (ASPxPageControl)GridBranch.FindEditFormTemplateControl("PageControl1");
            ASPxGridView pControl = (ASPxGridView)PageControl1.ActiveTabPage.TabControl.FindControl("gridTerminalId");
            ASPxComboBox company = (ASPxComboBox)pControl.FindEditFormTemplateControl("comboCompany");
            ASPxComboBox exchange = (ASPxComboBox)pControl.FindEditFormTemplateControl("comboExchange");
            string ID = Convert.ToString(company.SelectedItem.Value);
            Session["ID"] = ID.ToString();
            DataTable dtseg = oDBEngine.GetDataTable("select exch_internalId,(select exh_shortName from tbl_master_exchange where exh_cntId=tbl_master_companyExchange.exch_exchId)+'-'+ exch_segmentId as Exchange from tbl_master_companyExchange where exch_compId='" + ID + "' and exch_segmentId is not null order by exch_internalId");
            if (dtseg.Rows.Count > 0)
            {
                Session["ID1"] = Convert.ToString(dtseg.Rows[0]["Exchange"]).Trim();
                SqlExchange.SelectCommand = "select exch_internalId,(select exh_shortName from tbl_master_exchange where exh_cntId=tbl_master_companyExchange.exch_exchId)+'-'+ exch_segmentId as Exchange from tbl_master_companyExchange where exch_compId='" + ID + "' and exch_segmentId is not null order by exch_internalId";
                exchange.DataBind();
            }


        }
        protected void comboVendor_Callback(object source, CallbackEventArgsBase e)
        {
            string param = e.Parameter;
            if (param != "Save")
            {
                //ASPxPageControl pControl1 = (ASPxPageControl)GridBranch.FindEditFormTemplateControl("PageControl1");
                ASPxGridView pControl = (ASPxGridView)PageControl1.ActiveTabPage.TabControl.FindControl("gridTerminalId");
                ASPxComboBox exchange = (ASPxComboBox)pControl.FindEditFormTemplateControl("comboExchange");
                ASPxComboBox vendor = (ASPxComboBox)pControl.FindEditFormTemplateControl("comboVendor");
                string ID = Convert.ToString(exchange.SelectedItem.Text);
                Session["ID1"] = ID.ToString();
                SqlVendor.SelectCommand = "select CTCLVendor_ID,CTCLVendor_Name+' ['+CTCLVendor_ProductType+']' as CTCLVendor_Name from Master_CTCLVendor where CTCLVendor_ExchangeSegment='" + ID + "'";
                vendor.DataBind();
                ListEditItem noneItem = new ListEditItem("None", 0);
                vendor.Items.Insert(0, noneItem);
                //vendor.Focus();
            }
            else if (param == "Save")
            {

            }
        }

        protected void ASPxComboBox1_Callback(object source, CallbackEventArgsBase e)
        {
            //ASPxPageControl pControl1 = (ASPxPageControl)GridBranch.FindEditFormTemplateControl("PageControl1");

            ASPxGridView pControl = (ASPxGridView)PageControl1.ActiveTabPage.TabControl.FindControl("gridTerminalId");
            ASPxComboBox company = (ASPxComboBox)pControl.FindEditFormTemplateControl("comboCompany");
            ASPxComboBox exchange = (ASPxComboBox)pControl.FindEditFormTemplateControl("comboExchange");
            TextBox terminalID = (TextBox)pControl.FindEditFormTemplateControl("txtTerminalId");
            ASPxComboBox PTerminalId = (ASPxComboBox)pControl.FindEditFormTemplateControl("parentTerID");
            ASPxComboBox CTClVendor = (ASPxComboBox)pControl.FindEditFormTemplateControl("comboVendor");
            TextBox CtclID = (TextBox)pControl.FindEditFormTemplateControl("txtCTCLID");
            TextBox ContactName = (TextBox)pControl.FindEditFormTemplateControl("txtContactName_hidden");
            TextBox CMode = (TextBox)pControl.FindEditFormTemplateControl("txtConnection");
            ASPxDateEdit dateActivation = (ASPxDateEdit)pControl.FindEditFormTemplateControl("dtActivation");
            ASPxDateEdit dateDeActivation = (ASPxDateEdit)pControl.FindEditFormTemplateControl("dtDeactivation");
            TextBox MappingID = (TextBox)pControl.FindEditFormTemplateControl("txtMappinID");
            TextBox AllTrade = (TextBox)pControl.FindEditFormTemplateControl("txtAllTrade_hidden");
            TextBox ClientTrade = (TextBox)pControl.FindEditFormTemplateControl("txtClientTrade_hidden");
            TextBox ProTrade = (TextBox)pControl.FindEditFormTemplateControl("txtProductTrade_hidden");
            TextBox ProTrade1 = (TextBox)pControl.FindEditFormTemplateControl("txtProductTrade");
            ASPxComboBox AspxCombo1 = (ASPxComboBox)pControl.FindEditFormTemplateControl("ASPxComboBox1");
            TextBox AllTrade1 = (TextBox)pControl.FindEditFormTemplateControl("txtAllTrade");
            TextBox ClientTrade1 = (TextBox)pControl.FindEditFormTemplateControl("txtClientTrade");
            TextBox brokertrade = (TextBox)pControl.FindEditFormTemplateControl("txtBrokername_hidden");
            TextBox brokertrade1 = (TextBox)pControl.FindEditFormTemplateControl("txtBrokername");
            TextBox ContactName1 = (TextBox)pControl.FindEditFormTemplateControl("txtContactName");
            //ASPxButton btnCancel = (ASPxButton)pControl.FindEditFormTemplateControl("btnCancel");
            string Columns = "TradingTerminal_CompanyID,TradingTerminal_ExchangeSegmentID,TradingTerminal_BranchID,TradingTerminal_TerminalID,TradingTerminal_ParentTerminalID,TradingTerminal_CTCLVendorID,TradingTerminal_CTCLID,TradingTerminal_ContactID,TradingTerminal_ConnectionMode,TradingTerminal_ActivationDate,TradingTerminal_DeactivationDate,TradingTerminal_MapinID,TradingTerminal_ProTradeID,TradingTerminal_CliTradeID,TradingTerminal_AllTradeID,TradingTerminal_CreateUser,TradingTerminal_CreateDateTime,Tradingterminal_BrokerId";
            string Comp = "";
            string Exch = "";
            string PTer = "";
            string CtClVend = "";
            string Values = "";
            string null1 = Convert.ToString(System.DBNull.Value);
            string null2 = Convert.ToString(System.DBNull.Value);
            if (company.SelectedItem != null)
            {
                Comp = Convert.ToString(company.SelectedItem.Value);
            }
            if (exchange.SelectedItem != null)
            {
                Exch = Convert.ToString(exchange.SelectedItem.Value);
            }
            if (PTerminalId.SelectedItem != null)
            {
                PTer =Convert.ToString(PTerminalId.SelectedItem.Value);
            }
            if (CTClVendor.SelectedItem != null)
            {
                CtClVend = Convert.ToString(CTClVendor.SelectedItem.Value);
            }
            DateTime activation = Convert.ToDateTime(dateActivation.Value);
            DateTime deactivation = Convert.ToDateTime(dateDeActivation.Value);
            if (deactivation == Convert.ToDateTime("1/1/0001 12:00:00 AM"))
            {
                deactivation = Convert.ToDateTime("1/1/1900 12:00:00 AM");
            }
            if (activation == Convert.ToDateTime("1/1/0001 12:00:00 AM"))
            {
                activation = Convert.ToDateTime("1/1/1900 12:00:00 AM");
            }
            //because the hidden fields are not cleared at the time of updation 23/04/2014 
            TextBox txtAllTrade = (TextBox)pControl.FindEditFormTemplateControl("txtAllTrade");
            TextBox txtClientTrade = (TextBox)pControl.FindEditFormTemplateControl("txtClientTrade");
            TextBox txtProductTrade = (TextBox)pControl.FindEditFormTemplateControl("txtProductTrade");
            TextBox txtBrokername = (TextBox)pControl.FindEditFormTemplateControl("txtBrokername");
            TextBox txtContactName = (TextBox)pControl.FindEditFormTemplateControl("txtContactName");
            if (txtAllTrade.Text.Trim() == "")
                AllTrade.Text = "";
            if (txtClientTrade.Text.Trim() == "")
                ClientTrade.Text = "";
            if (txtProductTrade.Text.Trim() == "")
                ProTrade.Text = "";
            if (txtContactName.Text.Trim() == "")
                ContactName.Text = "";
            if (txtBrokername.Text.Trim() == "")
                brokertrade.Text = "";
            //Values = "'" + Comp + "','" + Exch + "','" + Session["KeyVal_InternalID"].ToString() + "','" + terminalID.Text + "','" + PTer + "','" + CtClVend + "','" + CtclID.Text + "','" + ContactName.Text + "','" + CMode.Text + "','" + activation + "','" + deactivation + "','" + MappingID.Text + "','" + ProTrade.Text + "','" + ClientTrade.Text + "','" + AllTrade.Text + "','" + Session["userid"].ToString() + "','" + oDBEngine.GetDate().ToString() + "','" + brokertrade.Text.ToString().Trim() + "'";
            Values = "'" + Comp + "','" + Exch + "','" + Session["KeyVal_InternalID"].ToString() + "','" + terminalID.Text + "','" + PTer + "','" + CtClVend + "','" + CtclID.Text + "','" + ContactName.Text + "','" + CMode.Text + "','" + activation + "','" + deactivation + "','" + MappingID.Text + "','" + ProTrade.Text + "','" + ClientTrade.Text + "','" + AllTrade.Text + "','" + Session["userid"].ToString() + "','" + oDBEngine.GetDate().ToString() + "','" + brokertrade.Text.ToString().Trim() + "'";

            //string[,] BranchID = objEngine.GetFieldValue("Master_TradingTerminal", "TradingTerminal_BranchID", " TradingTerminal_ID='" + Session["KeyVal"].ToString() + "'", 1);
            if (Session["InternalID"] != null)
            {
                string[,] BID = objEngine.GetFieldValue("Master_TradingTerminal", "TradingTerminal_BranchID,TradingTerminal_ID", " TradingTerminal_CompanyID='" + Comp + "' and TradingTerminal_ExchangeSegmentID='" + Exch + "' and TradingTerminal_TerminalID='" + terminalID.Text + "' and TradingTerminal_CTCLVendorID='" + CtClVend + "'", 2);
                if (BID[0, 0] != "n")
                {
                    if (BID[0, 1] == Convert.ToString(Session["KeyVal"]))
                    {
                        //int NoorRows = objEngine.SetFieldValue("Master_TradingTerminal", "TradingTerminal_CompanyID='" + Comp + "',TradingTerminal_ExchangeSegmentID='" + Exch + "',TradingTerminal_TerminalID='" + terminalID.Text + "',TradingTerminal_ParentTerminalID='" + PTer + "',TradingTerminal_CTCLVendorID='" + CtClVend + "',TradingTerminal_CTCLID='" + CtclID.Text + "',TradingTerminal_ContactID='" + ContactName.Text + "',TradingTerminal_ConnectionMode='" + CMode.Text + "',TradingTerminal_ActivationDate='" + activation + "',TradingTerminal_DeactivationDate='" + deactivation + "',TradingTerminal_MapinID='" + MappingID.Text + "',TradingTerminal_ProTradeID='" + ProTrade.Text + "',Tradingterminal_BrokerId='" + brokertrade.Text.ToString().Trim() + "',TradingTerminal_CliTradeID='" + ClientTrade.Text + "',TradingTerminal_AllTradeID='" + AllTrade.Text + "',TradingTerminal_ModifyUser='" + Session["userid"].ToString() + "',TradingTerminal_ModifyDateTime='" + oDBEngine.GetDate().ToString() + "'", " TradingTerminal_ID='" + Session["KeyVal"].ToString() + "'");
                        string UpdateQuery = @"Update Master_TradingTerminal
                    Set TradingTerminal_CompanyID='" + Comp + "',TradingTerminal_ExchangeSegmentID='" + Exch +
                         "',TradingTerminal_TerminalID='" + terminalID.Text + "',TradingTerminal_ParentTerminalID='" + PTer +
                             "',TradingTerminal_CTCLVendorID='" + CtClVend + "',TradingTerminal_CTCLID='" + CtclID.Text +
                             "',TradingTerminal_ContactID='" + ContactName.Text + "',TradingTerminal_ConnectionMode='" + CMode.Text +
                             "',TradingTerminal_ActivationDate='" + activation + "',TradingTerminal_DeactivationDate='" + deactivation +
                             "',TradingTerminal_MapinID='" + MappingID.Text +
                             "',Tradingterminal_BrokerId='" + Convert.ToString(brokertrade.Text).Trim() + "',TradingTerminal_ModifyUser='" + Convert.ToString(Session["userid"]) +
                             "',TradingTerminal_ModifyDateTime='" + Convert.ToString(oDBEngine.GetDate());

                        if (ProTrade.Text.Trim().Length == 0)
                            UpdateQuery = UpdateQuery + "',TradingTerminal_ProTradeID=null";
                        else
                            UpdateQuery = UpdateQuery + "',TradingTerminal_ProTradeID='" + ProTrade.Text + "'";

                        if (ClientTrade.Text.Trim().Length == 0)
                            UpdateQuery = UpdateQuery + ",TradingTerminal_CliTradeID=null";
                        else
                            UpdateQuery = UpdateQuery + ",TradingTerminal_CliTradeID='" + ClientTrade.Text + "'";

                        if (AllTrade.Text.Trim().Length == 0)
                            UpdateQuery = UpdateQuery + ",TradingTerminal_AllTradeID=null";
                        else
                            UpdateQuery = UpdateQuery + ",TradingTerminal_AllTradeID='" + AllTrade.Text + "'";


                        UpdateQuery = UpdateQuery + " Where TradingTerminal_ID='" + Convert.ToString(Session["KeyVal"]) + "'";


                        //int NoorRows = objEngine.SetFieldValue("Master_TradingTerminal", "TradingTerminal_CompanyID='" + Comp + "',TradingTerminal_ExchangeSegmentID='" + Exch + "',TradingTerminal_TerminalID='" + terminalID.Text + "',TradingTerminal_ParentTerminalID='" + PTer + "',TradingTerminal_CTCLVendorID='" + CtClVend + "',TradingTerminal_CTCLID='" + CtclID.Text + "',TradingTerminal_ContactID='" + ContactName.Text + "',TradingTerminal_ConnectionMode='" + CMode.Text + "',TradingTerminal_ActivationDate='" + activation + "',TradingTerminal_DeactivationDate='" + deactivation + "',TradingTerminal_MapinID='" + MappingID.Text + "',TradingTerminal_ProTradeID='" + ProTrade.Text + "',Tradingterminal_BrokerId='" + brokertrade.Text.ToString().Trim() + "',TradingTerminal_CliTradeID='" + ClientTrade.Text + "',TradingTerminal_AllTradeID='" + AllTrade.Text + "',TradingTerminal_ModifyUser='" + Session["userid"].ToString() + "',TradingTerminal_ModifyDateTime='" + oDBEngine.GetDate().ToString() + "'", " TradingTerminal_ID='" + Session["KeyVal"].ToString() + "'");
                        int NoorRows = objEngine.GetDataTable(UpdateQuery).Rows.Count;

                        //if (NoorRows > 0)
                        AspxCombo1.JSProperties["cpDataExists"] = "update";
                        //else
                        //    AspxCombo1.JSProperties["cpDataExists"] = "invalid";
                    }
                    else
                    {
                        AspxCombo1.JSProperties["cpDataExists"] = "invalid";
                    }
                }
                else
                {
                    if (BID[0, 0] == "n")
                    {
                        //int NoorRows = objEngine.SetFieldValue("Master_TradingTerminal", "TradingTerminal_CompanyID='" + Comp + "',TradingTerminal_ExchangeSegmentID='" + Exch + "',TradingTerminal_TerminalID='" + terminalID.Text + "',TradingTerminal_ParentTerminalID='" + PTer + "',TradingTerminal_CTCLVendorID='" + CtClVend + "',TradingTerminal_CTCLID='" + CtclID.Text + "',TradingTerminal_ContactID='" + ContactName.Text + "',TradingTerminal_ConnectionMode='" + CMode.Text + "',TradingTerminal_ActivationDate='" + activation + "',TradingTerminal_DeactivationDate='" + deactivation + "',TradingTerminal_MapinID='" + MappingID.Text + "',TradingTerminal_ProTradeID='" + ProTrade.Text + "',Tradingterminal_BrokerId='" + brokertrade.Text.ToString().Trim() + "',TradingTerminal_CliTradeID='" + ClientTrade.Text + "',TradingTerminal_AllTradeID='" + AllTrade.Text + "',TradingTerminal_ModifyUser='" + Session["userid"].ToString() + "',TradingTerminal_ModifyDateTime='" + oDBEngine.GetDate().ToString() + "'", " TradingTerminal_ID='" + Session["KeyVal"].ToString() + "'");
                        int NoorRows = objEngine.SetFieldValue("Master_TradingTerminal", "TradingTerminal_CompanyID='" + Comp + "',TradingTerminal_ExchangeSegmentID='" + Exch + "',TradingTerminal_TerminalID='" + terminalID.Text + "',TradingTerminal_ParentTerminalID='" + PTer + "',TradingTerminal_CTCLVendorID='" + CtClVend + "',TradingTerminal_CTCLID='" + CtclID.Text + "',TradingTerminal_ContactID='" + ContactName.Text + "',TradingTerminal_ConnectionMode='" + CMode.Text + "',TradingTerminal_ActivationDate='" + activation + "',TradingTerminal_DeactivationDate='" + deactivation + "',TradingTerminal_MapinID='" + MappingID.Text + "',TradingTerminal_ProTradeID='" + ProTrade.Text + "',Tradingterminal_BrokerId='" + brokertrade.Text.ToString().Trim() + "',TradingTerminal_CliTradeID='" + ClientTrade.Text + "',TradingTerminal_AllTradeID='" + AllTrade.Text + "',TradingTerminal_ModifyUser='" + Session["userid"].ToString() + "',TradingTerminal_ModifyDateTime='" + oDBEngine.GetDate().ToString() + "'", " TradingTerminal_ID='" + Session["KeyVal"].ToString() + "'");
                        AspxCombo1.JSProperties["cpDataExists"] = "update";
                    }
                    else
                    {
                        AspxCombo1.JSProperties["cpDataExists"] = "invalid";
                    }
                }
                Session["InternalID"] = null;
            }
            else
            {
                string[,] BID = objEngine.GetFieldValue("Master_TradingTerminal", "TradingTerminal_BranchID", " TradingTerminal_CompanyID='" + Comp + "' and TradingTerminal_ExchangeSegmentID='" + Exch + "' and TradingTerminal_TerminalID='" + terminalID.Text + "' and TradingTerminal_CTCLVendorID='" + CtClVend + "'", 1);
                if (BID[0, 0] == "n")
                {

                    int NoofRowsAffected = objEngine.InsurtFieldValue("Master_TradingTerminal", Columns, Values);
                    AspxCombo1.JSProperties["cpDataExists"] = "insert";
                }
                else
                {
                    AspxCombo1.JSProperties["cpDataExists"] = "invalid";
                }
                Session["InternalID"] = null;
            }
            pControl.CancelEdit();
        }

        protected void gridTerminalId_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
        {
            //ASPxPageControl pControl1 = (ASPxPageControl)GridBranch.FindEditFormTemplateControl("PageControl1");
            ASPxGridView pControl = (ASPxGridView)PageControl1.ActiveTabPage.TabControl.FindControl("gridTerminalId");
            TextBox contact = (TextBox)pControl.FindEditFormTemplateControl("txtContactName");

            ASPxDateEdit dateActivation = (ASPxDateEdit)pControl.FindEditFormTemplateControl("dtActivation");
            ASPxDateEdit dateDeActivation = (ASPxDateEdit)pControl.FindEditFormTemplateControl("dtDeactivation");
            dateActivation.EditFormatString = oConverter.GetDateFormat("Date");
            dateDeActivation.EditFormatString = oConverter.GetDateFormat("Date");

            contact.Attributes.Add("onkeyup", "CallList(this,'ContactName',event)");
            TextBox Alltrade = (TextBox)pControl.FindEditFormTemplateControl("txtAllTrade");
            Alltrade.Attributes.Add("onkeyup", "CallList(this,'AllContact',event)");
            Alltrade.Attributes.Add("onblur", "hide_show('All')");
            TextBox client = (TextBox)pControl.FindEditFormTemplateControl("txtClientTrade");
            client.Attributes.Add("onkeyup", "CallList(this,'AllContact',event)");
            //client.Attributes.Add("onchange", "hide_show('Client')");
            TextBox pro = (TextBox)pControl.FindEditFormTemplateControl("txtProductTrade");
            pro.Attributes.Add("onkeyup", "CallList(this,'AllContact',event)");

            TextBox broker = (TextBox)pControl.FindEditFormTemplateControl("txtBrokername");
            broker.Attributes.Add("onkeyup", "CallList(this,'AllContactbroker',event)");
            //pro.Attributes.Add("onchange", "hide_show('pro')");
        }
        protected void gridTerminalId_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            if (Session["KeyVal_InternalID"] != null)
            {
                string id = Convert.ToString(e.EditingKeyValue);
                Session["KeyVal"] = id.ToString();
               
                ASPxGridView pControl = (ASPxGridView)PageControl1.ActiveTabPage.TabControl.FindControl("gridTerminalId");
                TextBox contact = (TextBox)pControl.FindEditFormTemplateControl("txtContactName");
                TextBox Alltrade = (TextBox)pControl.FindEditFormTemplateControl("txtAllTrade");
                TextBox client = (TextBox)pControl.FindEditFormTemplateControl("txtClientTrade");
                TextBox pro = (TextBox)pControl.FindEditFormTemplateControl("txtProductTrade");
                ASPxComboBox company = (ASPxComboBox)pControl.FindEditFormTemplateControl("comboCompany");
                ASPxComboBox exchange = (ASPxComboBox)pControl.FindEditFormTemplateControl("comboExchange");
                TextBox terminalID = (TextBox)pControl.FindEditFormTemplateControl("txtTerminalId");
                ASPxComboBox PTerminalId = (ASPxComboBox)pControl.FindEditFormTemplateControl("parentTerID");
                ASPxComboBox CTClVendor = (ASPxComboBox)pControl.FindEditFormTemplateControl("comboVendor");
                TextBox CtclID = (TextBox)pControl.FindEditFormTemplateControl("txtCTCLID");
                TextBox CMode = (TextBox)pControl.FindEditFormTemplateControl("txtConnection");
                ASPxDateEdit dateActivation = (ASPxDateEdit)pControl.FindEditFormTemplateControl("dtActivation");
                ASPxDateEdit dateDeActivation = (ASPxDateEdit)pControl.FindEditFormTemplateControl("dtDeactivation");
                TextBox MappingID = (TextBox)pControl.FindEditFormTemplateControl("txtMappinID");
                TextBox ConID = (TextBox)pControl.FindEditFormTemplateControl("txtContactName_hidden");
                TextBox PId = (TextBox)pControl.FindEditFormTemplateControl("txtProductTrade_hidden");
                TextBox CliId = (TextBox)pControl.FindEditFormTemplateControl("txtClientTrade_hidden");
                TextBox AllId = (TextBox)pControl.FindEditFormTemplateControl("txtAllTrade_hidden");
                TextBox brokid = (TextBox)pControl.FindEditFormTemplateControl("txtBrokername_hidden");
                TextBox brok = (TextBox)pControl.FindEditFormTemplateControl("txtBrokername");
                DataTable dtBranch = objEngine.GetDataTable("Master_TradingTerminal", "(select cmp_name from tbl_master_company where cmp_internalid=Master_TradingTerminal.TradingTerminal_CompanyID) as CompName,(select (select exh_shortName from tbl_master_exchange where exh_cntId=tbl_master_companyExchange.exch_exchId)+'-'+ exch_segmentId as Exchange from tbl_master_companyExchange where exch_internalId=Master_TradingTerminal.TradingTerminal_ExchangeSegmentID) as Exchange,TradingTerminal_TerminalID as Terninal,TradingTerminal_ParentTerminalID as PTerminalID,(select CTCLVendor_Name from Master_CTCLVendor where CTCLVendor_ID=Master_TradingTerminal.TradingTerminal_CTCLVendorID) as VendorID,TradingTerminal_CTCLID as CTCLID,(select isnull(cnt_firstName,'') +' '+isnull(cnt_middleName,'')+' '+isnull(cnt_lastName,'')+ ' [' + isnull(ltrim(rtrim(cnt_UCC)),'') + '] ' from tbl_master_contact where cnt_internalId=Master_TradingTerminal.TradingTerminal_ContactID) as ContactName,TradingTerminal_ConnectionMode as ConnMode,cast(TradingTerminal_ActivationDate as datetime) as ActDate,cast(TradingTerminal_DeactivationDate as datetime) as DActDate,(select isnull(cnt_firstName,'') +' '+isnull(cnt_middleName,'')+' '+isnull(cnt_lastName,'')+ ' [' + isnull(ltrim(rtrim(cnt_UCC)),'') + '] ' from tbl_master_contact where cnt_internalId=Master_TradingTerminal.TradingTerminal_ProTradeID) as ProTradeID,(select isnull(cnt_firstName,'') +' '+isnull(cnt_middleName,'')+' '+isnull(cnt_lastName,'')+ ' [' + isnull(ltrim(rtrim(cnt_UCC)),'') + '] ' from tbl_master_contact where cnt_internalId=Master_TradingTerminal.TradingTerminal_BrokerID) as brokid,(select isnull(cnt_firstName,'') +' '+isnull(cnt_middleName,'')+' '+isnull(cnt_lastName,'')+ ' [' + isnull(ltrim(rtrim(cnt_UCC)),'') + '] ' from tbl_master_contact where cnt_internalId=Master_TradingTerminal.TradingTerminal_CliTradeID) as ClientTradeID,(select isnull(cnt_firstName,'') +' '+isnull(cnt_middleName,'')+' '+isnull(cnt_lastName,'')+ ' [' + isnull(ltrim(rtrim(cnt_UCC)),'') + '] ' from tbl_master_contact where cnt_internalId=Master_TradingTerminal.TradingTerminal_AllTradeID) as AllTradeID,TradingTerminal_MapinID as MappingID,TradingTerminal_ContactID as cID,TradingTerminal_ProTradeID as pid,TradingTerminal_brokerID as bid,TradingTerminal_CliTradeID as cli,TradingTerminal_AllTradeID as allId,TradingTerminal_ExchangeSegmentID as SegID,TradingTerminal_CompanyID as CompID", " TradingTerminal_ID='" + id + "'");
                if (dtBranch.Rows.Count > 0)
                {
                    company.Text = dtBranch.Rows[0]["CompName"].ToString();

                    Session["ID"] = dtBranch.Rows[0]["CompID"].ToString();
                    SqlExchange.SelectCommand = "select exch_internalId,(select exh_shortName from tbl_master_exchange where exh_cntId=tbl_master_companyExchange.exch_exchId)+'-'+ exch_segmentId as Exchange from tbl_master_companyExchange where exch_compId='" + dtBranch.Rows[0]["CompID"].ToString() + "'";
                    exchange.DataBind();
                    exchange.Text = dtBranch.Rows[0]["Exchange"].ToString();
                    terminalID.Text = dtBranch.Rows[0]["Terninal"].ToString();
                    PTerminalId.Text = dtBranch.Rows[0]["PTerminalID"].ToString();
                    Session["ID1"] = dtBranch.Rows[0]["Exchange"].ToString();
                    SqlVendor.SelectCommand = "select CTCLVendor_ID,CTCLVendor_Name+' ['+CTCLVendor_ProductType+']' as CTCLVendor_Name from Master_CTCLVendor where CTCLVendor_ExchangeSegment='" + dtBranch.Rows[0]["Exchange"].ToString() + "'";
                    CTClVendor.DataBind();
                    CTClVendor.Text = dtBranch.Rows[0]["VendorID"].ToString();
                    CtclID.Text = dtBranch.Rows[0]["CTCLID"].ToString();
                    dateActivation.Value = Convert.ToDateTime(dtBranch.Rows[0]["ActDate"].ToString());
                    dateDeActivation.Value = Convert.ToDateTime(dtBranch.Rows[0]["DActDate"].ToString());
                    CMode.Text = dtBranch.Rows[0]["ConnMode"].ToString();
                    MappingID.Text = dtBranch.Rows[0]["MappingID"].ToString();
                    contact.Text = dtBranch.Rows[0]["ContactName"].ToString();
                    client.Text = dtBranch.Rows[0]["ClientTradeID"].ToString();
                    pro.Text = dtBranch.Rows[0]["ProTradeID"].ToString();
                    brok.Text = dtBranch.Rows[0]["brokid"].ToString();
                    brokid.Text = dtBranch.Rows[0]["bid"].ToString();
                    Alltrade.Text = dtBranch.Rows[0]["AllTradeID"].ToString();
                    ConID.Text = dtBranch.Rows[0]["cID"].ToString();
                    PId.Text = dtBranch.Rows[0]["pid"].ToString();
                    CliId.Text = dtBranch.Rows[0]["cli"].ToString();
                    AllId.Text = dtBranch.Rows[0]["allId"].ToString();
                }
                if (AllId.Text.Trim() != "")
                    checking = "b";

            }
            Session["InternalID"] = "Edit";
            gridTerminalId.JSProperties["cpCompCombo"] = "anew";
        }
        protected void gridTerminalId_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {


            // TrdTerminal.SelectCommand = "select td.TradingTerminal_ID,e.exh_shortName+'-'+ce.exch_segmentId as Exchange,td.TradingTerminal_TerminalID,td.TradingTerminal_ParentTerminalID from Master_TradingTerminal td,tbl_master_exchange e,tbl_master_companyExchange ce where td.TradingTerminal_CompanyID=ce.exch_compId and td.TradingTerminal_ExchangeSegmentID=ce.exch_InternalId and e.exh_cntId=ce.exch_exchId and td.TradingTerminal_BranchID='" + Session["KeyVal_InternalID"].ToString() + "'";
            TrdTerminal.SelectCommand = "select td.TradingTerminal_ID,e.exh_shortName+'-'+ce.exch_segmentId as Exchange,td.TradingTerminal_TerminalID,td.TradingTerminal_ParentTerminalID,td.TradingTerminal_ProTradeID ,(Select  ISNULL(ltrim(rtrim(cnt_firstName)), '') + ' ' + ISNULL(ltrim(rtrim(cnt_middleName)), '')   + ' ' + ISNULL(ltrim(rtrim(cnt_lastName)), '') + ' [' + isnull(ltrim(rtrim(cnt_UCC)),'') + '] ' AS cnt_firstName from tbl_master_contact WHERE  cnt_internalid =td.TradingTerminal_ProTradeID) as ProTradeID,td.TradingTerminal_brokerid ,(select isnull(cnt_firstName,'') +' '+isnull(cnt_middleName,'')+' '+isnull(cnt_lastName,'')+ ' [' + isnull(ltrim(rtrim(cnt_UCC)),'') + '] ' from tbl_master_contact where cnt_internalId=td.TradingTerminal_BrokerID) as brokid,(Select  ISNULL(ltrim(rtrim(cnt_firstName)), '') + ' ' + ISNULL(ltrim(rtrim(cnt_middleName)), '')   + ' ' + ISNULL(ltrim(rtrim(cnt_lastName)), '') + ' [' + isnull(ltrim(rtrim(cnt_UCC)),'') + '] ' AS cnt_firstName from tbl_master_contact WHERE  cnt_internalid =td.TradingTerminal_CliTradeID) as CliTradeID,(Select  ISNULL(ltrim(rtrim(cnt_firstName)), '') + ' ' + ISNULL(ltrim(rtrim(cnt_middleName)), '')   + ' ' + ISNULL(ltrim(rtrim(cnt_lastName)), '') + ' [' + isnull(ltrim(rtrim(cnt_UCC)),'') + '] ' AS cnt_firstName from tbl_master_contact WHERE  cnt_internalid =td.TradingTerminal_AllTradeID) as AllTradeID from  Master_TradingTerminal td, tbl_master_exchange e, tbl_master_companyExchange ce where td.TradingTerminal_CompanyID=ce.exch_compId  and td.TradingTerminal_ExchangeSegmentID=ce.exch_InternalId and  e.exh_cntId=ce.exch_exchId and td.TradingTerminal_BranchID='" + Session["KeyVal_InternalID"].ToString() + "' order By TradingTerminal_ID desc ";
            //ASPxPageControl pControl1 = (ASPxPageControl)GridBranch.FindEditFormTemplateControl("PageControl1");
            ASPxGridView pControl = (ASPxGridView)PageControl1.ActiveTabPage.TabControl.FindControl("gridTerminalId");
            pControl.DataBind();
            pControl.CancelEdit();
            if (e.Parameters == "s1")
                pControl.Settings.ShowFilterRow = true;

            if (e.Parameters == "All1")
            {
                pControl.FilterExpression = string.Empty;
            }
        }
        protected void gridTerminalId_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {

        }
        protected void gridTerminalId_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            Session["InternalID"] = null;
            gridTerminalId.JSProperties["cpCompCombo"] = "anew";

        }
        protected void gridTerminalId_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpExist"] = checking;
        }

        protected void cmbExport_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 Filter = int.Parse(cmbExport.SelectedItem.Value.ToString());
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

        protected void Button2_Click(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, GetType(), "JScript", "window.location ='Branch.aspx';", true);
            Response.Redirect("Branch.aspx");
        }

        public void SetCountry()
        {
            //objEngine
            DataTable DT = new DataTable();
            DT = oDBEngine.GetDataTable("tbl_master_country", "  ltrim(rtrim(cou_country)) Name,ltrim(rtrim(cou_id)) Code ", null);
            lstCountry.DataSource = DT;
            lstCountry.DataMember = "Code";
            lstCountry.DataTextField = "Name";
            lstCountry.DataValueField = "Code";
            lstCountry.DataBind();
        }

        public void SetMainAccount()
        {
            DataTable DT = new DataTable();
            DT = oDBEngine.GetDataTable("Master_MainAccount", "MainAccount_Name Name,MainAccount_AccountCode Code", null);
            lstMainAccount.DataSource = DT;
            lstMainAccount.DataMember = "Code";
            lstMainAccount.DataTextField = "Name";
            lstMainAccount.DataValueField = "Code";
            lstMainAccount.DataBind(); 
        }

        [WebMethod]
        public static List<string> GetStates(string CountryCode)
        {
            BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(ConfigurationSettings.AppSettings["DBConnectionDefault"]);
            DataTable DT = new DataTable();
            if (CountryCode != "")
            {
                DT = oDBEngine.GetDataTable("tbl_master_state", " ltrim(rtrim(state)) Name,ltrim(rtrim(id)) Code", "countryId=" + CountryCode);
            }
            List<string> obj = new List<string>();
            foreach (DataRow dr in DT.Rows)
            {

                obj.Add(Convert.ToString(dr["Name"]) + "|" + Convert.ToString(dr["Code"]));
            }
            return obj;
        }
        [WebMethod]
        public static List<string> GetCities(string StateCode)
        {
            BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(ConfigurationSettings.AppSettings["DBConnectionDefault"]);
            DataTable DT = new DataTable();
            if (StateCode != "")
            {
                DT = oDBEngine.GetDataTable("tbl_master_city", " ltrim(rtrim(city_name)) Name,ltrim(rtrim(city_id))Code", "state_id=" + StateCode);
            }
            List<string> obj = new List<string>();
            foreach (DataRow dr in DT.Rows)
            {

                obj.Add(Convert.ToString(dr["Name"]) + "|" + Convert.ToString(dr["Code"]));
            }
            return obj;
        }

        [WebMethod]
        public static List<string> GetArea(string CityCode)
        {
            BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(ConfigurationSettings.AppSettings["DBConnectionDefault"]);
            DataTable DT = new DataTable();
            if (CityCode != "")
            {
                DT = oDBEngine.GetDataTable("tbl_master_area", " ltrim(rtrim(area_name)) Name,ltrim(rtrim(area_id))Code,ISNULL(area_pincode,'') pin", "city_id=" + CityCode);
            }
            List<string> obj = new List<string>();
            foreach (DataRow dr in DT.Rows)
            {

                obj.Add(Convert.ToString(dr["Name"]) + "|" + Convert.ToString(dr["Code"]) + "|" + Convert.ToString(dr["pin"]));
            }
            return obj;
        }

        [WebMethod]
        public static List<string> GetPin(string CityCode)
        {
            BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(ConfigurationSettings.AppSettings["DBConnectionDefault"]);
            DataTable DT = new DataTable();
            if (CityCode != "")
            {
                DT = oDBEngine.GetDataTable("tbl_master_pinzip", " pin_id,pin_code", "city_id=" + CityCode, "pin_code");
            }
            List<string> obj = new List<string>();
            foreach (DataRow dr in DT.Rows)
            {

                obj.Add(Convert.ToString(dr["pin_code"]) + "|" + Convert.ToString(dr["pin_id"]));
            }
            return obj;
        }
        /*Code  Added  By Priti on 06122016 to use jquery Choosen*/
        [WebMethod]
        public static List<string> GetBranchHead(string reqStr)
        {
            BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(ConfigurationSettings.AppSettings["DBConnectionDefault"]);
            DataTable DT = new DataTable();
            DT.Rows.Clear();
            DT = oDBEngine.GetDataTable("tbl_master_employee, tbl_master_contact,tbl_trans_employeeCTC ", "ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') +'['+cnt_shortName+']' AS Name, cnt_internalId as Id    ", " tbl_master_employee.emp_contactId = tbl_trans_employeeCTC.emp_cntId and tbl_trans_employeeCTC.emp_cntId = tbl_master_contact.cnt_internalId    and cnt_contactType='EM' and (emp_dateofLeaving is null or emp_dateofLeaving='1/1/1900 12:00:00 AM' OR emp_dateofLeaving>getdate()) and (cnt_firstName Like '" + reqStr + "%' or cnt_shortName like '" + reqStr + "%')");

            List<string> obj = new List<string>();
            foreach (DataRow dr in DT.Rows)
            {

                obj.Add(Convert.ToString(dr["Name"]) + "|" + Convert.ToString(dr["Id"]));
            }
            return obj;
        }
       
        /*Code  Added  By Priti on 21122016 to Check unique short name*/
        [WebMethod]
        public static bool CheckUniqueName(string ShortName, string qString)
        {
            string ShortCode = "0";
           
            if (qString != "ADD")
            {
                ShortCode = qString;
            }
                

            MShortNameCheckingBL objMShortNameCheckingBL = new MShortNameCheckingBL();
            DataTable dt = new DataTable();
            Boolean status = false;
            BusinessLogicLayer.GenericMethod oGeneric = new BusinessLogicLayer.GenericMethod();
            if (ShortCode != "" && Convert.ToString(ShortName).Trim() != "")
            {
                status = objMShortNameCheckingBL.CheckUnique(ShortName, ShortCode, "Add_Edit_Branch");
            }
            return status;
        }


        //...............code end........

        protected int getUdfCount()
        {
            DataTable udfCount = oDBEngine.GetDataTable("select 1 from tbl_master_remarksCategory rc where cat_applicablefor='Br'   and ( exists (select * from tbl_master_udfGroup where id=rc.cat_group_id and grp_isVisible=1) or rc.cat_group_id=0)");
            return udfCount.Rows.Count;
        }
        //Rev 1.0

        [WebMethod(EnableSession = true)]
        public static object GetOnDemandBranchHead(string reqStr)
        {
            List<BranchHeadLists> listBranchHead = new List<BranchHeadLists>();
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Prc_BranchMasterDetails");
            proc.AddPara("@action", "BranchHead");           
            proc.AddPara("@firstname", reqStr);
            proc.AddPara("@shortname", reqStr);
            dt = proc.GetTable();

            foreach (DataRow dr in dt.Rows)
            {
                BranchHeadLists BranchHead = new BranchHeadLists();
                BranchHead.id = dr["Id"].ToString();
                BranchHead.Name = dr["Name"].ToString();
                listBranchHead.Add(BranchHead);
            }
            return listBranchHead;
        }

        public class BranchHeadLists
        {
            public string id { get; set; }
            public string Name { get; set; }
        }
        //Rev 1.0 End
    }
}
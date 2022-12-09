using System;
using System.Web.UI;
using System.Configuration;
using ClsDropDownlistNameSpace;
using System.Data;
using BusinessLogicLayer;
using System.Web.UI.WebControls;

namespace ERP.OMS.Management.Master
{
    public partial class management_master_RootBuildingInsertUpdate : ERP.OMS.ViewState_class.VSPage
    {
        //  DBEngine objEngine = new DBEngine(ConfigurationManager.AppSettings["DBConnectionDefault"]);
        BusinessLogicLayer.DBEngine objEngine = new BusinessLogicLayer.DBEngine(ConfigurationManager.AppSettings["DBConnectionDefault"]);
        public string pageAccess = "";
        //DBEngine oDBEngine = new DBEngine(string.Empty);
        BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(string.Empty);
        clsDropDownList OclsDropDownList = new clsDropDownList();
        WarehouseConfigMasterBL objWarehouseConfigMaster = new WarehouseConfigMasterBL();
        protected void Page_Load(object sender, EventArgs e)
        {

            string ID = Request.QueryString["id"].ToString();
            //  BtnAdd.Attributes.Add("onclick", "callpopup()");
            string pageAccess = objEngine.CheckPageAccessebility("RootBuilding.aspx");
            Session["PageAccess"] = pageAccess;
            if (Session["PageAccess"].ToString().Trim() == "Add" || Session["PageAccess"].ToString().Trim() == "Modify" || Session["PageAccess"].ToString().Trim() == "All")
            {
                BtnAdd.Enabled = true;
            }
            else
            {
                BtnAdd.Enabled = false;
            }
            if (Session["PageAccess"].ToString().Trim() == "DelAdd" && Request.QueryString["id"].ToString() != "ADD")
            {
                BtnSave.Enabled = false;
            }
            else
            {
                BtnSave.Enabled = true;
            }
            if (!IsPostBack)
            {
                SetCountry();
                PopulateBranchByBranchHierarchy();
                //if (Request.QueryString["id"].ToString() == "ADD")
                //{
                //    BindBuilding();
                //    BindState();
                //    BindCity();
                //}
                //else
                //{

                //    BindBuilding();


                //}

                BindBuilding();


            }
        }
        public void BindBuilding()
        {
            string KeyId = Request.QueryString["id"].ToString();
            //if (KeyId == "ADD")
            //{
            //    DDLState.Visible = true;
            //    DDLCity.Visible = true;
            //}
            string[,] CareTaker = objEngine.GetFieldValue("tbl_master_contact", "cnt_internalId as Id,ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '')+'['+isnull(cnt_shortName,'')+']' AS Name", " cnt_contactType='EM'", 2, "cnt_firstName");
            //objEngine.AddDataToDropDownList(CareTaker, DDLCareTaker);
            OclsDropDownList.AddDataToDropDownList(CareTaker, DDLCareTaker);

            string[,] Country = objEngine.GetFieldValue("tbl_master_country", "cou_id,cou_country", null, 2, "cou_country");
            //objEngine.AddDataToDropDownList(Country, DDLCountry);
           // OclsDropDownList.AddDataToDropDownList(Country, DDLCountry);
            ShowData();
        }
        //protected void DDLCountry_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    TxtCity.Visible = false;
        //    TxtState.Visible = false;
        //    DDLState.Visible = true;
        //    DDLCity.Visible = true;
        //    BindState();
        //    BindCity();
        //}
        //protected void DDLState_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //   // BindCity();
        //}
        //public void BindState()
        //{
        //    DDLState.Items.Clear();
        //    string[,] State = objEngine.GetFieldValue("tbl_master_state", "id,state", " countryId='" + DDLCountry.SelectedItem.Value + "'", 2);
        //    if (State[0, 0] != "n")
        //    {
        //        //objEngine.AddDataToDropDownList(State, DDLState);
        //        OclsDropDownList.AddDataToDropDownList(State, DDLState);

        //    }
        //    else
        //    {
        //    }
        //}
        //public void BindCity()
        //{
        //    if (DDLState.SelectedValue != "")
        //    {
        //        DDLCity.Items.Clear();
        //        string[,] City = objEngine.GetFieldValue("tbl_master_city", "city_id,city_name", " state_id='" + DDLState.SelectedItem.Value + "'", 2);
        //        if (City[0, 0] != "n")
        //        {
        //            //objEngine.AddDataToDropDownList(City, DDLCity);
        //            OclsDropDownList.AddDataToDropDownList(City, DDLCity);

        //        }
        //        else
        //        {
        //        }
        //    }
        //    else
        //    {
        //        DDLCity.Items.Clear();
        //    }
        //}
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            //DateTime Createdate = Convert.ToDateTime(oDBEngine.GetDate().ToShortDateString());
            DateTime Createdate = Convert.ToDateTime(oDBEngine.GetDate().ToShortDateString());
            string KeyId = Request.QueryString["id"].ToString();
            if (KeyId == "ADD")
            {
                   //objEngine.InsurtFieldValue("tbl_master_building", "bui_Name,bui_contactId,bui_address1,bui_address2,bui_address3,bui_landmark,bui_country,bui_state,bui_city,bui_pin,CreateDate,CreateUser", "'" + TxtBuilding.Text + "','" + DDLCareTaker.SelectedItem.Value + "','" + TxtAdd1.Text + "','" + TxtAdd2.Text + "','" + TxtAdd3.Text + "','" + TxtLand.Text + "','" + DDLCountry.SelectedItem.Value + "','" + DDLState.SelectedItem.Value + "','" + DDLCity.SelectedItem.Value + "','" + TxtPin.Text + "','" + Createdate + "','" + Session["userid"].ToString() + "'");
                objEngine.InsurtFieldValue("tbl_master_building", "bui_Name,bui_contactId,bui_address1,bui_address2,bui_address3,bui_landmark,bui_country,bui_state,bui_city,bui_pin,CreateDate,CreateUser,bui_BranchId", "'" + TxtBuilding.Text + "','" + DDLCareTaker.SelectedItem.Value + "','" + TxtAdd1.Text + "','" + TxtAdd2.Text + "','" + TxtAdd3.Text + "','" + TxtLand.Text + "','" + Convert.ToString(txtCountry_hidden.Value) + "','" + Convert.ToString(txtState_hidden.Value) + "','" + Convert.ToString(txtCity_hidden.Value) + "','" + Convert.ToString(HdPin.Value) + "','" + Createdate + "','" + Session["userid"].ToString() + "','"+ddl_Branch.SelectedItem.Value+"'");
                    string popupscript = "";
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "adfa", "parent.editwin.close();", true);
                    //popupscript = "<script language='javascript'>window.opener.location.href = window.opener.location.href;window.close();</script>";
                    //ClientScript.RegisterStartupScript(GetType(), "JScript", popupscript);
                    //Page.ClientScript.RegisterStartupScript(GetType(), "pagecall", "<script>parent.editwin.close();</script>");
                    Response.Redirect("RootBuilding.aspx");
                

            }
            else
            {
               
                    //objEngine.SetFieldValue("tbl_master_building", "bui_Name='" + TxtBuilding.Text + "',bui_contactId='" + DDLCareTaker.SelectedItem.Value + "',bui_address1='" + TxtAdd1.Text + "',bui_address2='" + TxtAdd2.Text + "',bui_address3='" + TxtAdd3.Text + "',bui_landmark='" + TxtLand.Text + "',bui_country='" + DDLCountry.SelectedItem.Value + "',bui_state='" + DDLState.SelectedItem.Value + "',bui_city='" + DDLCity.SelectedItem.Value + "',bui_pin='" + TxtPin.Text + "',LastModifyDate='" + Createdate + "',LastModifyUser='" + Session["userid"].ToString() + "'", " bui_id='" + KeyId + "'");
                objEngine.SetFieldValue("tbl_master_building", "bui_Name='" + TxtBuilding.Text + "',bui_contactId='" + DDLCareTaker.SelectedItem.Value + "',bui_address1='" + TxtAdd1.Text + "',bui_address2='" + TxtAdd2.Text + "',bui_address3='" + TxtAdd3.Text + "',bui_landmark='" + TxtLand.Text + "',bui_country='" + Convert.ToString(txtCountry_hidden.Value) + "',bui_state='" + Convert.ToString(txtState_hidden.Value) + "',bui_city='" + Convert.ToString(txtCity_hidden.Value) + "',bui_pin='" + Convert.ToString(HdPin.Value) + "',LastModifyDate='" + Createdate + "',LastModifyUser='" + Session["userid"].ToString() + "',bui_BranchId='" + ddl_Branch.SelectedItem.Value + "'", " bui_id='" + KeyId + "'");
                    string popupscript = "";
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "adfaaa", "parent.editwin.close();", true);
                    //popupscript = "<script language='javascript'>window.opener.location.href = window.opener.location.href;window.close();</script>";
                    //ClientScript.RegisterStartupScript(GetType(), "JScript", popupscript);
                    //Page.ClientScript.RegisterStartupScript(GetType(), "pagecall1", "<script>parent.editwin.close();</script>");
                    Response.Redirect("RootBuilding.aspx");
                
                 
            }
        }
        public void ShowData()
        {
            string KeyId = Request.QueryString["id"].ToString();
            if (KeyId != "ADD")
            {
                Label12.Text = "Update";
                Session["KeyId"] = KeyId.ToString();
                string[,] show = objEngine.GetFieldValue("tbl_master_building", "bui_Name,bui_contactId,bui_address1,bui_address2,bui_address3,bui_landmark,bui_country,bui_state,bui_city,bui_pin,bui_BranchId", " bui_id=" + KeyId, 11);
                if (show[0, 0] != "n")
                {
                    //TxtCity.Visible = true;
                    //TxtState.Visible = true;
                    TxtBuilding.Text = show[0, 0];
                    string CId = show[0, 1];
                    string[,] CId1 = objEngine.GetFieldValue("tbl_master_contact", "ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') AS Name", " cnt_internalId='" + CId + "'", 1);
                    DDLCareTaker.SelectedItem.Text = CId1[0, 0];
                    TxtAdd1.Text = show[0, 2];
                    TxtAdd2.Text = show[0, 3];
                    TxtAdd3.Text = show[0, 4];
                    TxtLand.Text = show[0, 5];

                    string Country = show[0, 6];
                   // DDLCountry.SelectedValue = show[0, 6];
                    txtCountry_hidden.Value = show[0, 6];
                    //string[,] Country1 = objEngine.GetFieldValue("tbl_master_country", "cou_country", " cou_id=" + Country, 1);
                    //DDLCountry.SelectedItem.Text = Country1[0, 0];
                    //string State = show[0, 7];
                    //string[,] State1 = objEngine.GetFieldValue("tbl_master_state", "state", " id=" + State, 1);
                    //TxtState.Text = State1[0, 0];
                    //string City = show[0, 8];
                    //string[,] City1 = objEngine.GetFieldValue("tbl_master_city", "city_name", " city_id=" + City, 1);
                    //TxtCity.Text = City1[0, 0];
                    //TxtPin.Text = show[0, 9];

                    //BindState();
                //    DDLState.SelectedValue = show[0, 7];
                    txtState_hidden.Value = show[0, 7];

                   //BindCity();
                   // DDLCity.SelectedValue = show[0, 8];
                    txtCity_hidden.Value = show[0, 8];
                    //string State = show[0, 7];
                    //string[,] State1 = objEngine.GetFieldValue("tbl_master_state", "state", " id=" + State, 1);
                    //TxtState.Text = State1[0, 0];
                    //string City = show[0, 8];
                    //string[,] City1 = objEngine.GetFieldValue("tbl_master_city", "city_name", " city_id=" + City, 1);
                    //TxtCity.Text = City1[0, 0];
                    //TxtPin.Text = show[0, 9];
                    HdPin.Value = show[0, 9];

                    if (show[0, 10] !=null)
                   {
                    int branchindex = 0;
                    int cnt = 0;
                    foreach (ListItem li in ddl_Branch.Items)
                    {
                        if (li.Value == show[0, 10])
                        {
                            cnt = 1;
                            break;
                        }
                        else
                        {
                            branchindex += 1;
                        }
                    }
                    if (cnt == 1)
                    {
                        ddl_Branch.SelectedIndex = branchindex;
                    }
                    else
                    {
                        ddl_Branch.SelectedIndex = cnt;
                    }
                }
                     
                }
            }
            else
            {
                Label12.Text = "Add";
                BtnAdd.Enabled = false;
            }
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
        public void PopulateBranchByBranchHierarchy()
        {
            string userbranch = "";
            DataTable dt = new DataTable();
            if (Session["userbranchHierarchy"] != null)
            {
                userbranch = Convert.ToString(Session["userbranchHierarchy"]);
            }
            dt = objWarehouseConfigMaster.PopulateBranchByBranchHierarchy(userbranch);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddl_Branch.DataTextField = "branch_description";
                ddl_Branch.DataValueField = "branch_id";
                ddl_Branch.DataSource = dt;
                ddl_Branch.DataBind();
                ddl_Branch.Items.Insert(0, new ListItem("--ALL--", "0"));
                //ddl_Branch.Items.Insert(0, new ListItem("Select", "0"));
            }
        }

    }
}
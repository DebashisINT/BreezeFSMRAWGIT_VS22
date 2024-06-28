//====================================================== Revision History ================================================================
//Rev Number DATE              VERSION          DEVELOPER           CHANGES
//1.0        07-05-2024        2.0.43          Priti               0027428 :Under Branch Wise Product mapping module , if IsActivateEmployeeBranchHierarchy=0, then the
//====================================================== Revision History ================================================================

using ClosedXML.Excel;
using DataAccessLayer;
using DevExpress.Web.Mvc;
using DevExpress.Web;
using DevExpress.XtraPrinting;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using ERP.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Threading.Tasks;
using iTextSharp.text.log;
using System.Globalization;
using DocumentFormat.OpenXml.Office.Word;
using BusinessLogicLayer;
using ClsDropDownlistNameSpace;
using SalesmanTrack;
using static ERP.OMS.Management.Activities.SpecialPriceUpload;
using static ERP.OMS.Management.Activities.CustSaleRateLock;


namespace ERP.OMS.Management.Activities
{
    public partial class ProductsBranchMap : System.Web.UI.Page
    {
        UserList lstuser = new UserList();
        BusinessLogicLayer.GenericMethod oGenericMethod;
        BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(ConfigurationSettings.AppSettings["DBConnectionDefault"]);
        clsDropDownList oclsDropDownList = new clsDropDownList();

        protected void Page_Load(object sender, EventArgs e)
        {
            string MAPID = "";
            if (!IsPostBack)
            {
                Session["ComponentData_Branch"] = null;
                Session["ComponentData_Product"] = null;

                //REV 1.0 
                BranchHoOffice();
                CommonBL cbl = new CommonBL();
                string mastersettings = cbl.GetSystemSettingsResult("IsActivateEmployeeBranchHierarchy");
                if (mastersettings == "0")
                {
                    hdnActivateEmployeeBranchHierarchy.Value = "0";
                }
                else
                {
                    hdnActivateEmployeeBranchHierarchy.Value = "1";
                }
                //REV 1.0  End

                if (Convert.ToString(Request.QueryString["key"]) != "ADD")
                {
                    hdAddOrEdit.Value = "Edit";
                    MAPID = Convert.ToString(Request.QueryString["key"]);
                    hdnPageEditId.Value = MAPID;
                    MAPDetails(MAPID);
                }
                else
                {
                    hdAddOrEdit.Value = "Add";
                    hdnPageEditId.Value = "0";
                }
            }
        }
        //REV 1.0 
        public void BranchHoOffice()
        {            
            DataTable stbill = new DataTable();            
            DataTable dtBranchChild = new DataTable();
            stbill = GetBranchheadoffice(Convert.ToString(HttpContext.Current.Session["userbranchHierarchy"]), "HO");           
            if (stbill.Rows.Count > 0)
            {
                ddlbranchHO.DataSource = stbill;
                ddlbranchHO.DataTextField = "Code";
                ddlbranchHO.DataValueField = "branch_id";
                ddlbranchHO.DataBind();
                dtBranchChild = GetChildBranch(Convert.ToString(HttpContext.Current.Session["userbranchHierarchy"]));
                if (dtBranchChild.Rows.Count > 0)
                {
                    ddlbranchHO.Items.Insert(0, new ListItem("All", "All"));
                }              
            }
        }
        public DataTable GetChildBranch(string CHILDBRANCH)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]));
            SqlCommand cmd = new SqlCommand("PRC_FINDCHILDBRANCH_REPORT", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CHILDBRANCH", CHILDBRANCH);
            cmd.CommandTimeout = 0;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);
            cmd.Dispose();
            con.Dispose();
            return dt;
        }
        public DataTable GetBranchheadoffice(string CHILDBRANCH, string Action)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Get_AllbranchHO");
            proc.AddPara("@CHILDBRANCH", CHILDBRANCH);
            proc.AddPara("@Ation", Action);
            ds = proc.GetTable();
            return ds;
        }
        //REV 1.0  End
        public void MAPDetails(string MAPID)
        {
            DataSet dsBranchMapEditDetails = GetDetailsOfBranchMAP(MAPID);

            //----------------------------Branch Bind-----------------------------
            DataTable BranchMap = dsBranchMapEditDetails.Tables[0];
            if (BranchMap.Rows.Count > 0)
            {
                Session["ComponentData_Branch"] = BranchMap;
                lookup_branch.DataSource = BranchMap;
                lookup_branch.DataBind();
            }
            else
            {
                Session["ComponentData_Branch"] = BranchMap;
                lookup_branch.DataSource = null;
                lookup_branch.DataBind();
            }
            lookup_branch.GridView.Selection.CancelSelection();

            //----------------------------Branch Bind End-----------------------------

            //----------------------------ParentEMP Bind-----------------------------
            DataTable ParentEMPMap = dsBranchMapEditDetails.Tables[1];
            
            if (ParentEMPMap.Rows.Count > 0)
            {
                Session["ComponentData_ParentEmp"] = ParentEMPMap;
                gridParentEmpLookup.DataSource = ParentEMPMap;
                gridParentEmpLookup.DataBind();
            }
            else
            {
                Session["ComponentData_ParentEmp"] = ParentEMPMap;
                gridParentEmpLookup.DataSource = null;
                gridParentEmpLookup.DataBind();
            }
            gridParentEmpLookup.GridView.Selection.CancelSelection();
            //----------------------------ParentEMP Bind End-----------------------------


            //----------------------------ChildEMP Bind-----------------------------
            DataTable ChildEMPMap = dsBranchMapEditDetails.Tables[2];

            if (ChildEMPMap.Rows.Count > 0)
            {
                Session["ComponentData_ParentEmp"] = ChildEMPMap;
                ChildParentEmpLookup.DataSource = ChildEMPMap;
                ChildParentEmpLookup.DataBind();
            }
            else
            {
                Session["ComponentData_ParentEmp"] = ChildEMPMap;
                ChildParentEmpLookup.DataSource = null;
                ChildParentEmpLookup.DataBind();
            }
            ChildParentEmpLookup.GridView.Selection.CancelSelection();
            //----------------------------ChildEMP Bind End-----------------------------





            //----------------------------Products Bind-----------------------------
            DataTable ProductsMap = dsBranchMapEditDetails.Tables[3];
            if (ProductsMap.Rows.Count > 0)
            {
                Session["ComponentData_Product"] = ProductsMap;
                gridProductLookup.DataSource = ProductsMap;
                gridProductLookup.DataBind();
            }
            else
            {
                Session["ComponentData_Product"] = ProductsMap;
                gridProductLookup.DataSource = null;
                gridProductLookup.DataBind();
            }
            gridProductLookup.GridView.Selection.CancelSelection();
            //----------------------------Products Bind End-----------------------------

            DataTable Mapdetails = dsBranchMapEditDetails.Tables[4];
            if (Mapdetails != null && Mapdetails.Rows.Count > 0)
            {
                string BRANCH_ID = Convert.ToString(Mapdetails.Rows[0]["BRANCH_ID"]);
                string PARENTEMP_INTERNALID = Convert.ToString(Mapdetails.Rows[0]["PARENTEMP_INTERNALID"]);
                string CHILDEMP_INTERNALID = Convert.ToString(Mapdetails.Rows[0]["CHILDEMP_INTERNALID"]);
                string PRODUCT_ID = Convert.ToString(Mapdetails.Rows[0]["PRODUCT_ID"]);

                if (!String.IsNullOrEmpty(BRANCH_ID))
                {
                    string[] eachBRANCH_ID = BRANCH_ID.Split(',');
                    if (eachBRANCH_ID.Length > 1)
                    {
                        foreach (string val in eachBRANCH_ID)
                        {
                            lookup_branch.GridView.Selection.SelectRowByKey(Convert.ToInt32(val));
                        }
                    }
                    else if (eachBRANCH_ID.Length == 1)
                    {
                        foreach (string val in eachBRANCH_ID)
                        {
                            lookup_branch.GridView.Selection.SelectRowByKey(Convert.ToInt32(val));
                        }
                    }
                }
                if (!String.IsNullOrEmpty(PARENTEMP_INTERNALID))
                {
                    string[] eachPARENTEMP_INTERNALID = PARENTEMP_INTERNALID.Split(',');
                    if (eachPARENTEMP_INTERNALID.Length > 1)
                    {
                        foreach (string val in eachPARENTEMP_INTERNALID)
                        {
                            gridParentEmpLookup.GridView.Selection.SelectRowByKey((val).Trim());
                        }
                    }
                    else if (eachPARENTEMP_INTERNALID.Length == 1)
                    {
                        foreach (string val in eachPARENTEMP_INTERNALID)
                        {
                            gridParentEmpLookup.GridView.Selection.SelectRowByKey((val).Trim());
                        }
                    }
                }
                if (!String.IsNullOrEmpty(CHILDEMP_INTERNALID))
                {
                    string[] eachCHILDEMP_INTERNALID = CHILDEMP_INTERNALID.Split(',');
                    if (eachCHILDEMP_INTERNALID.Length > 1)
                    {
                        foreach (string val in eachCHILDEMP_INTERNALID)
                        {
                            ChildParentEmpLookup.GridView.Selection.SelectRowByKey((val).Trim());
                        }
                    }
                    else if (eachCHILDEMP_INTERNALID.Length == 1)
                    {
                        foreach (string val in eachCHILDEMP_INTERNALID)
                        {
                            ChildParentEmpLookup.GridView.Selection.SelectRowByKey((val).Trim());
                        }
                    }
                }
                if (!String.IsNullOrEmpty(PRODUCT_ID))
                {
                    string[] eachPRODUCT_ID = PRODUCT_ID.Split(',');
                    if (eachPRODUCT_ID.Length > 1)
                    {
                        foreach (string val in eachPRODUCT_ID)
                        {
                            gridProductLookup.GridView.Selection.SelectRowByKey(Convert.ToInt32(val));
                        }
                    }
                    else if (eachPRODUCT_ID.Length == 1)
                    {
                        foreach (string val in eachPRODUCT_ID)
                        {
                            gridProductLookup.GridView.Selection.SelectRowByKey(Convert.ToInt32(val));
                        }
                    }
                }
            }




        }
        public DataSet GetDetailsOfBranchMAP(string MAPID)
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("PRC_FSMBRANCHWISEPRODUCTMAPPING");
            proc.AddVarcharPara("@Action", 100, "FETCHBRANCHMAP");
            proc.AddBigIntegerPara("@PRODUCTBRANCHMAP_ID", Convert.ToInt32(MAPID));
            ds = proc.GetDataSet();
            return ds;
        }
        #region Branch Populate
        protected void Componentbranch_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
        {
            DataTable ComponentTable = new DataTable();
            string FinYear = Convert.ToString(Session["LastFinYear"]);
            if (e.Parameter.Split('~')[0] == "BindComponentGrid")
            {                
                //REV 1.0
                string Hoid = e.Parameter.Split('~')[1];
                if (Hoid != "All")
                {
                    ComponentTable = GetBranch(Convert.ToString(HttpContext.Current.Session["userbranchHierarchy"]), Hoid);                   
                }
                else
                {                                  
                    ComponentTable = oDBEngine.GetDataTable("select * from (select branch_id as ID,branch_description,branch_code from tbl_master_branch a where a.branch_id=1  union all select branch_id as ID,branch_description,branch_code from tbl_master_branch b where b.branch_parentId=1) a order by branch_description");               
                }
                if (ComponentTable.Rows.Count > 0)
                {
                    Session["ComponentData_Branch"] = ComponentTable;
                    lookup_branch.DataSource = ComponentTable;
                    lookup_branch.DataBind();
                }
                else
                {
                    Session["ComponentData_Branch"] = ComponentTable;
                    lookup_branch.DataSource = null;
                    lookup_branch.DataBind();
                }
                //REV 1.0 End
            }
            else if (e.Parameter.Split('~')[0] == "BindComponentGridBrnachMap") 
            {
                ProcedureExecute proc = new ProcedureExecute("PRC_FSMBRANCHWISEPRODUCTMAPPING");
                proc.AddVarcharPara("@Action", 50, "FETCHBRANCHS");
                proc.AddIntegerPara("@USERID", Convert.ToInt32(HttpContext.Current.Session["userid"]));
                ComponentTable = proc.GetTable();
                if (ComponentTable.Rows.Count > 0)
                {
                    Session["ComponentData_Branch"] = ComponentTable;
                    lookup_branch.DataSource = ComponentTable;
                    lookup_branch.DataBind();
                }
            }


            //if (e.Parameter.Split('~')[0] == "BindComponentGrid")
            //{
            //    DataTable ComponentTable = new DataTable();

            //    BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();
            //    ProcedureExecute proc = new ProcedureExecute("PRC_FSMBRANCHWISEPRODUCTMAPPING");
            //    proc.AddVarcharPara("@Action", 50, "FETCHBRANCHS");                
            //    ComponentTable = proc.GetTable();

            //    if (ComponentTable.Rows.Count > 0)
            //    {
            //        Session["ComponentData_Branch"] = ComponentTable;
            //        lookup_branch.DataSource = ComponentTable;
            //        lookup_branch.DataBind();
            //    }
            //    else
            //    {
            //        Session["ComponentData_Branch"] = ComponentTable;
            //        lookup_branch.DataSource = null;
            //        lookup_branch.DataBind();
            //    }
            //}
        }
        public DataTable GetBranch(string BRANCH_ID, string Ho)
        {
            DataTable dt = new DataTable();
            //SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["DBConnectionDefault"]);
            SqlConnection con = new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]));
            SqlCommand cmd = new SqlCommand("GetFinancerBranchfetchhowise", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Branch", BRANCH_ID);
            cmd.Parameters.AddWithValue("@Hoid", Ho);
            cmd.CommandTimeout = 0;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);
            cmd.Dispose();
            con.Dispose();

            return dt;
        }
        protected void lookup_branch_DataBinding(object sender, EventArgs e)
        {
            if (Session["ComponentData_Branch"] != null)
            {
                lookup_branch.DataSource = (DataTable)Session["ComponentData_Branch"];
            }
        }
        #endregion Branch Populate   

        #region Product Populate
        protected void ProductComponentPanel_Callback(object sender, CallbackEventArgsBase e)
        {
            if (e.Parameter.Split('~')[0] == "BindProductGrid")
            {
                DataTable ComponentTable = new DataTable();

                BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();
                ProcedureExecute proc = new ProcedureExecute("PRC_FSMBRANCHWISEPRODUCTMAPPING");
                proc.AddVarcharPara("@Action", 50, "FETCHPRODUCTS");
                ComponentTable = proc.GetTable();

                if (ComponentTable.Rows.Count > 0)
                {
                    Session["ComponentData_Product"] = ComponentTable;
                    gridProductLookup.DataSource = ComponentTable;
                    gridProductLookup.DataBind();
                }
                else
                {
                    Session["ComponentData_Product"] = ComponentTable;
                    gridProductLookup.DataSource = null;
                    gridProductLookup.DataBind();
                }
            }
        }

        protected void gridProductLookup_DataBinding(object sender, EventArgs e)
        {
            if (Session["ComponentData_Product"] != null)
            {
                gridProductLookup.DataSource = (DataTable)Session["ComponentData_Product"];
            }
        }
        #endregion Product Populate

        #region Parent Employee Populate
        protected void ParentEmpComponentPanel_Callback(object sender, CallbackEventArgsBase e)
        {
            if (e.Parameter.Split('~')[0] == "BindParentEmpgrid")
            {
                string BranchComponent = "";
                string BRANCH_ID = "";
                List<object> BranchList = lookup_branch.GridView.GetSelectedFieldValues("ID");
                foreach (object Brnch in BranchList)
                {
                    BranchComponent += "," + Brnch;
                }
                BRANCH_ID = BranchComponent.TrimStart(',');


                DataTable ComponentTable = new DataTable();

                BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();
                ProcedureExecute proc = new ProcedureExecute("PRC_FSMBRANCHWISEPRODUCTMAPPING");
                proc.AddVarcharPara("@Action", 50, "FETCHPARENTEMPLOYEE");
                proc.AddVarcharPara("@BRANCHID", 4000, BRANCH_ID);
                ComponentTable = proc.GetTable();

                if (ComponentTable.Rows.Count > 0)
                {
                    Session["ComponentData_ParentEmp"] = ComponentTable;
                    gridParentEmpLookup.DataSource = ComponentTable;
                    gridParentEmpLookup.DataBind();
                }
                else
                {
                    Session["ComponentData_ParentEmp"] = ComponentTable;
                    gridParentEmpLookup.DataSource = null;
                    gridParentEmpLookup.DataBind();
                }
            }
        }
        protected void ParentEmpLookup_DataBinding(object sender, EventArgs e)
        {
            if (Session["ComponentData_ParentEmp"] != null)
            {
                gridParentEmpLookup.DataSource = (DataTable)Session["ComponentData_ParentEmp"];
            }
        }
        #endregion Parent Employee Populate

        #region Child Employee Populate
        protected void ChildEmpComponentPanel_Callback(object sender, CallbackEventArgsBase e)
        {
            if (e.Parameter.Split('~')[0] == "BindChildEmpGrid")
            {
                string BranchComponent = "";
                string BRANCH_ID = "";
                List<object> BranchList = lookup_branch.GridView.GetSelectedFieldValues("ID");
                foreach (object Brnch in BranchList)
                {
                    BranchComponent += "," + Brnch;
                }
                BRANCH_ID = BranchComponent.TrimStart(',');

                string ParentEMP_ID = "";
                string ParentEMP = "";
                List<object> ParentEMPIDList = gridParentEmpLookup.GridView.GetSelectedFieldValues("cnt_internalId");
                foreach (object Parent in ParentEMPIDList)
                {
                    ParentEMP += "," + Parent;
                }
                ParentEMP_ID = ParentEMP.TrimStart(',');



                if(ParentEMP_ID=="")
                {
                    DataTable ComponentTable = new DataTable();

                    BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();
                    ProcedureExecute proc = new ProcedureExecute("PRC_FSMBRANCHWISEPRODUCTMAPPING");
                    proc.AddVarcharPara("@Action", 50, "FETCHCHILDEMPLOYEE");
                    proc.AddVarcharPara("@BRANCHID", 4000, BRANCH_ID);
                    proc.AddVarcharPara("@ParentEMPID", 4000, ParentEMP_ID);
                    ComponentTable = proc.GetTable();

                    if (ComponentTable.Rows.Count > 0)
                    {
                        Session["ComponentData_ChildEmp"] = ComponentTable;
                        ChildParentEmpLookup.DataSource = ComponentTable;
                        ChildParentEmpLookup.DataBind();
                    }
                    else
                    {
                        Session["ComponentData_ChildEmp"] = ComponentTable;
                        ChildParentEmpLookup.DataSource = null;
                        ChildParentEmpLookup.DataBind();
                    }

                }
                else
                {
                    DataTable ComponentTable = new DataTable();

                    BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();
                    ProcedureExecute proc = new ProcedureExecute("PRC_FSMBRANCHWISEPRODUCTMAPPING");
                    proc.AddVarcharPara("@Action", 50, "FETCHCHILDEMPLOYEE");
                    proc.AddVarcharPara("@BRANCHID", 4000, BRANCH_ID);
                    proc.AddVarcharPara("@ParentEMPID", 4000, ParentEMP_ID);
                    ComponentTable = proc.GetTable();

                    if (ComponentTable.Rows.Count > 0)
                    {
                        Session["ComponentData_ChildEmp"] = ComponentTable;
                        ChildParentEmpLookup.DataSource = ComponentTable;
                        ChildParentEmpLookup.DataBind();
                    }
                    else
                    {
                        Session["ComponentData_ChildEmp"] = ComponentTable;
                        ChildParentEmpLookup.DataSource = null;
                        ChildParentEmpLookup.DataBind();
                    }
                }

            }
        }

        protected void ChildParentEmpLookup_DataBinding(object sender, EventArgs e)
        {
            if (Session["ComponentData_ChildEmp"] != null)
            {
                ChildParentEmpLookup.DataSource = (DataTable)Session["ComponentData_ChildEmp"];
            }
        }

        #endregion Child Employee Populate


        #region Data Save
        protected void CallbackPanel_Callback(object sender, CallbackEventArgsBase e)
        {
            string returnPara = Convert.ToString(e.Parameter);

            string BranchComponent = "";
            string BRANCH_ID = "";
            List<object> BranchList = lookup_branch.GridView.GetSelectedFieldValues("ID");
            foreach (object Brnch in BranchList)
            {
                BranchComponent += "," + Brnch;
            }
            BRANCH_ID = BranchComponent.TrimStart(',');

            string PRODUCT_ID = "";
            string PRODUCTS = "";
            List<object> PRODUCTSList = gridProductLookup.GridView.GetSelectedFieldValues("SPRODUCTS_ID");
            foreach (object PRODUCT in PRODUCTSList)
            {
                PRODUCTS += "," + PRODUCT;
            }
            PRODUCT_ID = PRODUCTS.TrimStart(',');


            string ParentEMP_ID = "";
            string ParentEMP = "";
            List<object> ParentEMPIDList = gridParentEmpLookup.GridView.GetSelectedFieldValues("cnt_internalId");
            foreach (object Parent in ParentEMPIDList)
            {
                ParentEMP += "," + Parent;
            }
            ParentEMP_ID = ParentEMP.TrimStart(',');


            string ChildEMP_ID = "";
            string ChildEMP = "";
            List<object> ChildEMPIDList = ChildParentEmpLookup.GridView.GetSelectedFieldValues("cnt_internalId");
            foreach (object child in ChildEMPIDList)
            {
                ChildEMP += "," + child;
            }
            ChildEMP_ID = ChildEMP.TrimStart(',');


            string ActionType =Convert.ToString(hdAddOrEdit.Value);
            string MAPID = Convert.ToString(hdnPageEditId.Value);

            try
            {
                BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();
                ProcedureExecute proc = new ProcedureExecute("PRC_FSMBRANCHWISEPRODUCTMAPPING");
                proc.AddVarcharPara("@Action", 100, ActionType);
                proc.AddIntegerPara("@PRODUCTBRANCHMAP_ID", Convert.ToInt32(MAPID));
                proc.AddVarcharPara("@PRODUCTID", 4000, PRODUCT_ID);
                proc.AddVarcharPara("@BRANCHID", 4000, BRANCH_ID);
                proc.AddVarcharPara("@ParentEMPID", 4000, ParentEMP_ID);
                proc.AddVarcharPara("@ChildEMPID", 4000, ChildEMP_ID);
                proc.AddIntegerPara("@USERID", Convert.ToInt32(HttpContext.Current.Session["userid"]));
                DataTable dt = proc.GetTable();
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Success"].ToString() == "True"
)
                    {
                        CallbackPanel.JSProperties["cpSaveSuccessOrFail"] = "1";
                    }
                    else if (dt.Rows[0]["Success"].ToString() == "False")
                    {
                        CallbackPanel.JSProperties["cpSaveSuccessOrFail"] = "-10";
                    }

                }
                else
                {
                    CallbackPanel.JSProperties["cpSaveSuccessOrFail"] = "-10";
                }

            }
            catch
            {

            }
        }

        #endregion Data Save
    }
}
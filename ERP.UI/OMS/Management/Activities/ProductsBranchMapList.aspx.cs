using ERP.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using System.Web.Services;
using DocumentFormat.OpenXml.Office2010.CustomUI;

namespace ERP.OMS.Management.Activities
{
    public partial class ProductsBranchMapList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                
            }
        }
       
        protected void EntityServerModeDataSource_Selecting(object sender, DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs e)
        {
            e.KeyExpression = "PRODUCTBRANCHMAP_ID";
            int User_id = Convert.ToInt32(Session["userid"]);
            string IsFilter = Convert.ToString(hfIsFilter.Value);
            string connectionString = Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]);
            if (IsFilter == "Y")
            {

                ERPDataClassesDataContext dc = new ERPDataClassesDataContext(connectionString);
                var q = from d in dc.PRODUCTBRANCHMAPLISTs
                        where d.USERID == User_id
                        orderby d.SEQ descending
                        select d;
                e.QueryableSource = q;
            }
            else
            {
                ERPDataClassesDataContext dc = new ERPDataClassesDataContext(connectionString);
                var q = from d in dc.PRODUCTBRANCHMAPLISTs
                        where d.SEQ == 0
                        select d;
                e.QueryableSource = q;
            }
        }

        protected void CallbackPanel_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            string returnPara = Convert.ToString(e.Parameter);
            string strProduct_hiddenID = (Convert.ToString(txtProduct_hidden.Value) == "") ? "0" : Convert.ToString(txtProduct_hidden.Value);

            string BranchComponent = "";
            string BRANCH_ID = "";
            List<object> BranchList = lookup_branch.GridView.GetSelectedFieldValues("ID");
            foreach (object Branch in BranchList)
            {
                BranchComponent += "," + Branch;
            }
            BRANCH_ID = BranchComponent.TrimStart(',');

            Task PopulateStockTrialDataTask = new Task(() => GetProductsBranchMapData(strProduct_hiddenID, BRANCH_ID));
            PopulateStockTrialDataTask.RunSynchronously();
        }
        public void GetProductsBranchMapData(string Products,string BRANCH_ID)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlConnection con = new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]));
                SqlCommand cmd = new SqlCommand("PRC_BRANCHWISEPRODUCTMAP_LIST", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@COMPANYID", Convert.ToString(Session["LastCompany"]));
                cmd.Parameters.AddWithValue("@FINYEAR", Convert.ToString(Session["LastFinYear"]));
                cmd.Parameters.AddWithValue("@Products", Products);
                cmd.Parameters.AddWithValue("@BRANCHID", BRANCH_ID);
                cmd.Parameters.AddWithValue("@USERID", Convert.ToInt32(Session["userid"]));
                cmd.Parameters.AddWithValue("@ACTION", hFilterType.Value);
                cmd.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                cmd.Dispose();
                con.Dispose();
            }
            catch (Exception ex)
            {

            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public static string DeleteProductsBranchMap(string MAPID)
        {
            try
            {
                BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();
                ProcedureExecute proc = new ProcedureExecute("PRC_FSMBRANCHWISEPRODUCTMAPPING");
                proc.AddVarcharPara("@PRODUCTBRANCHMAP_ID", 50, MAPID);
                proc.AddVarcharPara("@Action", 100, "Delete");
                proc.AddIntegerPara("@UserId", Convert.ToInt32(HttpContext.Current.Session["userid"]));
                DataTable dtSaleRateLock = proc.GetTable();
                if (dtSaleRateLock.Rows.Count > 0)
                {
                    if (dtSaleRateLock.Rows[0]["Insertmsg"].ToString() == "1")
                    {
                        return "1";
                    }
                    else if (dtSaleRateLock.Rows[0]["Insertmsg"].ToString() == "-998")
                    {
                        return "-998";
                    }
                    else
                    {
                        return "0";
                    }
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return "Error occured";
            }
        }

        #region Branch Populate
        protected void Componentbranch_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
        {
            string FinYear = Convert.ToString(Session["LastFinYear"]);
            if (e.Parameter.Split('~')[0] == "BindComponentGrid")
            {
                DataTable ComponentTable = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_FSMBRANCHWISEPRODUCTMAPPING");
                proc.AddVarcharPara("@Action", 50, "FETCHBRANCHS");
                ComponentTable = proc.GetTable();
                if (ComponentTable.Rows.Count > 0)
                {
                    Session["ComponentData_Branch"] = ComponentTable;
                    lookup_branch.DataSource = ComponentTable;
                    lookup_branch.DataBind();
                }
            }            
        }
        
        protected void lookup_branch_DataBinding(object sender, EventArgs e)
        {
            if (Session["ComponentData_Branch"] != null)
            {
                lookup_branch.DataSource = (DataTable)Session["ComponentData_Branch"];
            }
        }
        #endregion Branch Populate   

    }
}
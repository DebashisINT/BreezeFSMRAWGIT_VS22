using System;
using System.Web;
using System.Web.UI;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using EntityLayer.CommonELS;
using System.IO;
using BusinessLogicLayer;

namespace ERP.OMS.Management.Master
{
    public partial class management_master_Branch : ERP.OMS.ViewState_class.VSPage
    {   /* For 3 Tier
    DBEngine oDBEngine = new DBEngine(string.Empty);
      */
        BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(ConfigurationManager.AppSettings["DBConnectionDefault"]);
        BusinessLogicLayer.Bank objBankStatementBL = new BusinessLogicLayer.Bank();
        string MyGlobalVariable = "ConnectionStrings:crmConnectionString";
        //bellow code added by debjyoti 17-11-2016
        public EntityLayer.CommonELS.UserRightsForPage rights = new UserRightsForPage();
        //end 17-11-2016
        protected void Page_Init(object sender, EventArgs e)
        {

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Response.Write(); 
                // gridStatusDataSource.
                //'http://localhost:2957/InfluxCRM/management/testProjectMainPage_employee.aspx'
                string sPath = HttpContext.Current.Request.Url.ToString();
                oDBEngine.Call_CheckPageaccessebility(sPath);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

             rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/management/Master/branch.aspx");
          
            if (HttpContext.Current.Session["userid"] == null)
            {
                //Page.ClientScript.RegisterStartupScript(GetType(), "SighOff", "<script>SignOff();</script>");
            }

            //this.Page.ClientScript.RegisterStartupScript(GetType(), "heightL", "<script>height();</script>");

            fillGrid();
            gridStatus.JSProperties["cpDelmsg"] = null;
        }
        protected void gridStatus_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            try { 
            string[] CallVal = e.Parameters.ToString().Split('~');
            // string tranid = e.Parameters.ToString();

            if (CallVal[0].ToString() == "s")
            {
                gridStatus.Settings.ShowFilterRow = true;
            }
            else if (CallVal[0].ToString() == "All")
            {
                gridStatus.FilterExpression = string.Empty;
            }
            else if (CallVal[0].ToString() == "Delete")
            {

                #region Code Commented and Modified  By Sam on 25052017 
                //string strbrCode = Convert.ToString(CallVal[1].ToString());
                //string[,] acccode = oDBEngine.GetFieldValue("tbl_master_contact",
                //              "cnt_branchid", "cnt_branchid=(select branch_id from tbl_master_branch where branch_id='" + strbrCode + "')", 1);

                //if (acccode[0, 0] != "n")
                //{
                //    gridStatus.JSProperties["cpDelmsg"] = "Used in other modules. Cannot Delete.";
                //}
                //else
                //{

                MasterDataCheckingBL obMasterDataCheckingBL = new MasterDataCheckingBL();
                    int i = 0;
                    i = obMasterDataCheckingBL.DeleteBranch(Convert.ToString(CallVal[1]));
                    if (i==1)
                    {
                        gridStatus.JSProperties["cpDelmsg"] = "Succesfully Deleted.";
                        fillGrid();
                    }
                    else
                    {
                        gridStatus.JSProperties["cpDelmsg"] = "Used in other module. Can not delete.";
                    }
                    //oDBEngine.DeleteValue("tbl_master_contactRemarks ", "rea_internalId=(select branch_internalId from tbl_master_branch where branch_id="+ CallVal[1].ToString()+")");
                    //oDBEngine.DeleteValue("tbl_master_branch ", "branch_ID ='" + CallVal[1].ToString() + "' and branch_id not in (select distinct cnt_branchid from tbl_master_contact)");


                //}
                #endregion Code Commented and Modified  By Sam on 25052017


            }
            }
            catch (Exception ex)
            {

            }

        }
        public void fillGrid()
        {
            gridStatusDataSource.SelectCommand = "select tbl_master_branch.branch_id, tbl_master_branch.branch_internalId, tbl_master_branch.branch_code, tbl_master_branch.branch_type, tbl_master_branch.branch_parentId, tbl_master_branch.branch_description, tbl_master_branch.branch_regionid, tbl_master_branch.branch_address1, tbl_master_branch.branch_address2,tbl_master_branch.branch_address3, (select top 1 cou_country from tbl_master_country where cou_id=tbl_master_branch.branch_country) as Country, (select state from tbl_master_State where id=tbl_master_branch.branch_state) as State, tbl_master_branch.branch_pin,(select top 1 city_name from tbl_master_city where city_id=tbl_master_branch.branch_city)as City, tbl_master_branch.branch_phone, tbl_master_branch.branch_head, tbl_master_branch.branch_contactPerson, tbl_master_branch.branch_cpPhone,tbl_master_branch.branch_cpEmail, tbl_master_branch.CreateDate, tbl_master_branch.CreateUser, tbl_master_branch.LastModifyDate, tbl_master_branch.LastModifyUser, tbl_master_branch.branch_Fax, (case when tbl_master_branch.branch_parentId = 0 then 'None' else (select top 1 A.branch_description from tbl_master_branch A where A.branch_id=tbl_master_branch.branch_parentId) end) as ParentBranch  from  tbl_master_branch  order by branch_id desc";
            gridStatus.DataBind();

        }
        //protected void cmbExport_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Int32 Filter = int.Parse(cmbExport.SelectedItem.Value.ToString());
        //    switch (Filter)
        //    {
        //        case 1:
        //            exporter.WritePdfToResponse();
        //            break;
        //        case 2:
        //            exporter.WriteXlsToResponse();
        //            break;
        //        case 3:
        //            exporter.WriteRtfToResponse();
        //            break;
        //        case 4:
        //            exporter.WriteCsvToResponse();
        //            break;
        //    }
        //}
        protected void cmbExport_SelectedIndexChanged(object sender, EventArgs e)
        {
            //bindUserGroups();

            //Int32 Filter = int.Parse(cmbExport.SelectedItem.Value.ToString());
            Int32 Filter = int.Parse(drdExport.SelectedItem.Value.ToString());
            switch (Filter)
            {
                case 1:
                    //exporter.WritePdfToResponse();

                    using (MemoryStream stream = new MemoryStream())
                    {
                        exporter.WritePdf(stream);
                        WriteToResponse("ExportEmployee", true, "pdf", stream);
                    }
                    //Page.Response.End();
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
        protected void WriteToResponse(string fileName, bool saveAsFile, string fileFormat, MemoryStream stream)
        {
            if (Page == null || Page.Response == null) return;
            string disposition = saveAsFile ? "attachment" : "inline";
            Page.Response.Clear();
            Page.Response.Buffer = false;
            Page.Response.AppendHeader("Content-Type", string.Format("application/{0}", fileFormat));
            Page.Response.AppendHeader("Content-Transfer-Encoding", "binary");
            Page.Response.AppendHeader("Content-Disposition", string.Format("{0}; filename={1}.{2}", disposition, HttpUtility.UrlEncode(fileName).Replace("+", "%20"), fileFormat));
            if (stream.Length > 0)
                Page.Response.BinaryWrite(stream.ToArray());
            //Page.Response.End();
        }


    }
}
using System;
using System.Data;
using System.Web;
//using DevExpress.Web;
using System.Configuration;
using BusinessLogicLayer;
using DevExpress.Web;
using EntityLayer.CommonELS;

namespace ERP.OMS.Management.Master
{
    public partial class management_master_Employee_Document : ERP.OMS.ViewState_class.VSPage
    {
        public string[] InputName = new string[5];
        public string[] InputType = new string[5];
        public string[] InputValue = new string[5];
        BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(ConfigurationManager.AppSettings["DBConnectionDefault"]);
        public EntityLayer.CommonELS.UserRightsForPage rights = new UserRightsForPage();
        protected void Page_Load(object sender, EventArgs e)
        {
            rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/management/Master/employee.aspx");
            //Session["KeyVal_InternalID"] = null;
            if (!IsPostBack)
            {
                string[,] EmployeeNameID = oDBEngine.GetFieldValue(" tbl_master_contact ", " case when cnt_firstName is null then '' else cnt_firstName end + ' '+case when cnt_middleName is null then '' else cnt_middleName end+ ' '+case when cnt_lastName is null then '' else cnt_lastName end+' ['+cnt_shortName+']' as name ", " cnt_internalId='" + HttpContext.Current.Session["KeyVal_InternalID"] + "'", 1);
                if (EmployeeNameID[0, 0] != "n")
                {
                    lblHeader.Text = EmployeeNameID[0, 0].ToUpper();
                }
            }
            BindGrid();
        }

        public void ClearArray()
        {
            Array.Clear(InputName, 0, InputName.Length - 1);
            Array.Clear(InputType, 0, InputType.Length - 1);
            Array.Clear(InputValue, 0, InputValue.Length - 1);
        }
        public void BindGrid()
        {
            string bldng = "";
            string verify = "";
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataColumn col1 = new DataColumn("Id");
            DataColumn col2 = new DataColumn("Type");
            DataColumn col3 = new DataColumn("FileName");
            DataColumn col4 = new DataColumn("Src");
            DataColumn col5 = new DataColumn("FilePath");
            DataColumn col6 = new DataColumn("ReceiveDate");
            DataColumn col7 = new DataColumn("RenewDate");



            DataColumn col8 = new DataColumn("Bldng");
            DataColumn col9 = new DataColumn("Fileno");
            DataColumn col10 = new DataColumn("vrfy");
            DataColumn col11 = new DataColumn("Note1");
            DataColumn col12 = new DataColumn("Note2");
            DataColumn col13 = new DataColumn("createuser");
            DataColumn col14 = new DataColumn("doc");
            dt.Columns.Add(col1);
            dt.Columns.Add(col2);
            dt.Columns.Add(col3);
            dt.Columns.Add(col4);
            dt.Columns.Add(col5);
            dt.Columns.Add(col6);
            dt.Columns.Add(col7);
            dt.Columns.Add(col8);
            dt.Columns.Add(col9);
            dt.Columns.Add(col10);
            dt.Columns.Add(col11);
            dt.Columns.Add(col12);
            dt.Columns.Add(col13);
            dt.Columns.Add(col14);
            //if (Session["KeyVal_InternalID"] == "")
            //{
            if (Request.QueryString["idbldng"] != null)
            {
                if (Request.QueryString["idbldng"].ToString().Contains("^"))
                {
                    string[] two = Request.QueryString["idbldng"].Split('^');
                    Session["KeyVal_InternalID"] = two[1].ToString().Trim();
                }
                else
                {
                    Session["KeyVal_InternalID"] = Request.QueryString["idbldng"].ToString();
                }

                dt1 = oDBEngine.GetDataTable("tbl_master_document INNER JOIN tbl_master_documentType ON tbl_master_document.doc_documentTypeId = tbl_master_documentType.dty_id", "tbl_master_document.doc_id AS Id, tbl_master_documentType.dty_documentType AS Type,tbl_master_document.doc_documentName AS FileName, tbl_master_document.doc_source AS Src,doc_buildingid,doc_Floor,doc_RoomNo,doc_CellNo,doc_FileNo,convert(varchar,doc_receivedate,106)as ReceiveDate,convert(varchar,doc_renewdate,106)as RenewDate,(case when doc_verifydatetime is not null then ((select user_loginid from tbl_master_user where user_id=doc_verifyuser)  + '['+ convert(varchar,doc_verifydatetime,106)+']') else 'Not Verified' end) as vrfy,doc_Note1,doc_Note2,(select user_loginid from tbl_master_user where user_id=tbl_master_document.Createuser) as createuser,tbl_master_document.doc_documentTypeId as doc ", "doc_contactId='" + Session["KeyVal_InternalID"] + "'");
            }
            else
            {
                dt1 = oDBEngine.GetDataTable("tbl_master_document INNER JOIN tbl_master_documentType ON tbl_master_document.doc_documentTypeId = tbl_master_documentType.dty_id", "tbl_master_document.doc_id AS Id, tbl_master_documentType.dty_documentType AS Type,tbl_master_document.doc_documentName AS FileName, tbl_master_document.doc_source AS Src,doc_buildingid,doc_Floor,doc_RoomNo,doc_CellNo,doc_FileNo,convert(varchar,doc_receivedate,106)as ReceiveDate,convert(varchar,doc_renewdate,106)as RenewDate,(case when doc_verifydatetime is not null then ((select user_loginid from tbl_master_user where user_id=doc_verifyuser)  + '['+ convert(varchar,doc_verifydatetime,106)+']') else 'Not Verified' end) as vrfy,doc_Note1,doc_Note2,(select user_loginid from tbl_master_user where user_id=tbl_master_document.Createuser) as createuser,tbl_master_document.doc_documentTypeId as doc ", "doc_contactId='" + Session["KeyVal_InternalID"] + "'");
            }


            if (dt1.Rows.Count != 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    if (dt1.Rows[i][4].ToString() == "0")
                    {
                        DataRow RowNew = dt.NewRow();
                        RowNew["Id"] = dt1.Rows[i][0].ToString();
                        RowNew["Type"] = dt1.Rows[i][1].ToString();
                        RowNew["FileName"] = dt1.Rows[i][2].ToString();
                        RowNew["Src"] = dt1.Rows[i][3].ToString();
                        if (dt1.Rows[i][9] != null)
                            RowNew["ReceiveDate"] = dt1.Rows[i][9].ToString();
                        else
                            RowNew["ReceiveDate"] = "";
                        if (dt1.Rows[i][10] != null)
                            RowNew["RenewDate"] = dt1.Rows[i][10].ToString();
                        else
                            RowNew["RenewDate"] = "";

                        string BName = "Floor No : " + dt1.Rows[i][5].ToString() + " " + "/ Room No-" + dt1.Rows[i][6].ToString() + " " + "/ Cabinet No-" + dt1.Rows[i][7].ToString() + " ";
                        RowNew["FilePath"] = BName;
                        RowNew["vrfy"] = dt1.Rows[i]["vrfy"].ToString();
                        RowNew["Fileno"] = dt1.Rows[i][8].ToString();
                        RowNew["bldng"] = "";
                        RowNew["Note1"] = dt1.Rows[i]["doc_Note1"].ToString();
                        RowNew["Note2"] = dt1.Rows[i]["doc_Note2"].ToString();
                        RowNew["createuser"] = dt1.Rows[i]["createuser"].ToString();
                        RowNew["doc"] = dt1.Rows[i]["doc"].ToString();
                        dt.Rows.Add(RowNew);

                    }
                    else
                    {
                        DataRow RowNew = dt.NewRow();
                        RowNew["Id"] = dt1.Rows[i][0].ToString();
                        RowNew["Type"] = dt1.Rows[i][1].ToString();
                        RowNew["FileName"] = dt1.Rows[i][2].ToString();
                        RowNew["Src"] = dt1.Rows[i][3].ToString();

                        if (dt1.Rows[i][9] != null)
                            RowNew["ReceiveDate"] = dt1.Rows[i][9].ToString();
                        else
                            RowNew["ReceiveDate"] = "";
                        if (dt1.Rows[i][10] != null)
                            RowNew["RenewDate"] = dt1.Rows[i][10].ToString();
                        else
                            RowNew["RenewDate"] = "";

                        string BuildingName = "";
                        string[,] bname1 = oDBEngine.GetFieldValue("tbl_master_building", "bui_name", " bui_id='" + dt1.Rows[i][4].ToString() + "'", 1);
                        if (bname1[0, 0] != "n")
                        {
                            BuildingName = bname1[0, 0];
                        }


                        RowNew["vrfy"] = dt1.Rows[i]["vrfy"].ToString();
                        RowNew["bldng"] = BuildingName;
                        string BName = "Floor No : " + dt1.Rows[i][5].ToString() + " " + "/ Room No-" + dt1.Rows[i][6].ToString() + " " + "/ Cabinet No-" + dt1.Rows[i][7].ToString() + " ";
                        RowNew["FilePath"] = BName;
                        RowNew["Fileno"] = dt1.Rows[i][8].ToString();
                        RowNew["Note1"] = dt1.Rows[i]["doc_Note1"].ToString();
                        RowNew["Note2"] = dt1.Rows[i]["doc_Note2"].ToString();
                        RowNew["createuser"] = dt1.Rows[i]["createuser"].ToString();
                        RowNew["doc"] = dt1.Rows[i]["doc"].ToString();
                        dt.Rows.Add(RowNew);
                    }
                }
            }
            EmployeeDocumentGrid.DataSource = dt.DefaultView;
            EmployeeDocumentGrid.DataBind();
        }

        protected void EmployeeDocumentGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {

            string[] CallVal = e.Parameters.ToString().Split('~');
            if (CallVal[0].ToString() == "Delete")
            {
                oDBEngine.DeleteValue("tbl_master_document", " doc_id='" + CallVal[1].ToString() + "'");
                BindGrid();



            }


        }

        protected void EmployeeDocumentGrid_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType != DevExpress.Web.GridViewRowType.Data) return;
            int rowindex = e.VisibleIndex;
            string Verify = EmployeeDocumentGrid.GetRowValues(rowindex, "vrfy").ToString();
            string ContactID = e.GetValue("Src").ToString();
            string Rowid = e.GetValue("Id").ToString();
            if (Verify != "Not Verified")
            {
                DataTable dt = oDBEngine.GetDataTable("select doc_verifyremarks from tbl_master_document where doc_id=" + Rowid + "");
                string tooltip = dt.Rows[0][0].ToString();
                e.Row.Cells[0].Style.Add("cursor", "hand");
                e.Row.Cells[0].ToolTip = "View Document!";
                e.Row.Cells[0].Attributes.Add("onclick", "javascript:OnDocumentView('" + ContactID + "');");

                e.Row.Cells[1].Style.Add("cursor", "hand");
                e.Row.Cells[1].ToolTip = "View Document!";
                e.Row.Cells[1].Attributes.Add("onclick", "javascript:OnDocumentView('" + ContactID + "');");

                e.Row.Cells[2].Style.Add("cursor", "hand");
                e.Row.Cells[2].ToolTip = "View Document!";
                e.Row.Cells[2].Attributes.Add("onclick", "javascript:OnDocumentView('" + ContactID + "');");

                e.Row.Cells[3].Style.Add("cursor", "hand");
                e.Row.Cells[3].ToolTip = "View Document!";
                e.Row.Cells[3].Attributes.Add("onclick", "javascript:OnDocumentView('" + ContactID + "');");

                e.Row.Cells[4].Style.Add("cursor", "hand");
                e.Row.Cells[4].ToolTip = "View Document!";
                e.Row.Cells[4].Attributes.Add("onclick", "javascript:OnDocumentView('" + ContactID + "');");

                e.Row.Cells[5].Style.Add("cursor", "hand");
                e.Row.Cells[5].ToolTip = "View Document!";
                e.Row.Cells[5].Attributes.Add("onclick", "javascript:OnDocumentView('" + ContactID + "');");

                e.Row.Cells[6].Style.Add("cursor", "hand");
                e.Row.Cells[6].ToolTip = "View Document!";
                e.Row.Cells[6].Attributes.Add("onclick", "javascript:OnDocumentView('" + ContactID + "');");

                e.Row.Cells[7].Style.Add("cursor", "hand");
                e.Row.Cells[7].ToolTip = "View Document!";
                e.Row.Cells[7].Attributes.Add("onclick", "javascript:OnDocumentView('" + ContactID + "');");

                //es
                e.Row.Cells[8].Style.Add("cursor", "hand");
                e.Row.Cells[8].ToolTip = tooltip.ToString();
            }
            if (Verify == "Not Verified")
            {
                DataTable dt = oDBEngine.GetDataTable("select doc_verifyremarks from tbl_master_document where doc_id=" + Rowid + "");
                string tooltip = dt.Rows[0][0].ToString();
                e.Row.Cells[0].Style.Add("cursor", "hand");
                e.Row.Cells[0].ToolTip = "View Document!";
                e.Row.Cells[0].Attributes.Add("onclick", "javascript:OnDocumentView('" + ContactID + "');");

                e.Row.Cells[1].Style.Add("cursor", "hand");
                e.Row.Cells[1].ToolTip = "View Document!";
                e.Row.Cells[1].Attributes.Add("onclick", "javascript:OnDocumentView('" + ContactID + "');");

                e.Row.Cells[2].Style.Add("cursor", "hand");
                e.Row.Cells[2].ToolTip = "View Document!";
                e.Row.Cells[2].Attributes.Add("onclick", "javascript:OnDocumentView('" + ContactID + "');");

                e.Row.Cells[3].Style.Add("cursor", "hand");
                e.Row.Cells[3].ToolTip = "View Document!";
                e.Row.Cells[3].Attributes.Add("onclick", "javascript:OnDocumentView('" + ContactID + "');");

                e.Row.Cells[4].Style.Add("cursor", "hand");
                e.Row.Cells[4].ToolTip = "View Document!";
                e.Row.Cells[4].Attributes.Add("onclick", "javascript:OnDocumentView('" + ContactID + "');");

                e.Row.Cells[5].Style.Add("cursor", "hand");
                e.Row.Cells[5].ToolTip = "View Document!";
                e.Row.Cells[5].Attributes.Add("onclick", "javascript:OnDocumentView('" + ContactID + "');");

                e.Row.Cells[6].Style.Add("cursor", "hand");
                e.Row.Cells[6].ToolTip = "View Document!";
                e.Row.Cells[6].Attributes.Add("onclick", "javascript:OnDocumentView('" + ContactID + "');");

                e.Row.Cells[7].Style.Add("cursor", "hand");
                e.Row.Cells[7].ToolTip = "View Document!";
                e.Row.Cells[7].Attributes.Add("onclick", "javascript:OnDocumentView('" + ContactID + "');");

                e.Row.Cells[8].Style.Add("cursor", "pointer");
                e.Row.Cells[8].ToolTip = "Click here to Verify !";
                e.Row.Cells[8].Attributes.Add("onclick", "javascript:Changestatus('" + Rowid + "');");
                e.Row.Cells[8].Style.Add("color", "Red");
            }

        }

        protected void EmployeeDocumentGrid_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            EmployeeDocumentGrid.SettingsText.PopupEditFormCaption = "Modify Documents";
        }

        protected void EmployeeDocumentGrid_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridViewEditFormEventArgs e)
        {
            EmployeeDocumentGrid.SettingsText.PopupEditFormCaption = "Add Documents";
        }
    }
}

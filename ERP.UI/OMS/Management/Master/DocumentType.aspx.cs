using System;
using System.Web;
//using DevExpress.Web;
using DevExpress.Web;
using BusinessLogicLayer;
using System.Configuration;
using EntityLayer.CommonELS;
using System.Web.Mvc;
using System.Net.Http;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Web.Services;

namespace ERP.OMS.Management.Master
{
    public partial class management_master_DocumentType : ERP.OMS.ViewState_class.VSPage
    {
        public string pageAccess = "";
        //DBEngine oDBEngine = new DBEngine(string.Empty);
        BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(string.Empty);
        public EntityLayer.CommonELS.UserRightsForPage rights = new UserRightsForPage();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Purpose : Replace .ToString() with Convert.ToString(..)
                //Name : Sudip 
                // Dated : 21-12-2016

                //'http://localhost:2957/InfluxCRM/management/testProjectMainPage_employee.aspx'

                string sPath = Convert.ToString(HttpContext.Current.Request.Url);
                oDBEngine.Call_CheckPageaccessebility(sPath);
                Session["exportval"] = null;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/management/Master/DocumentType.aspx");

            //------- For Read Only User in SQL Datasource Connection String   Start-----------------

            if (HttpContext.Current.Session["EntryProfileType"] != null)
            {
                if (Convert.ToString(HttpContext.Current.Session["EntryProfileType"]) == "R")
                {
                    DocumentType.ConnectionString = ConfigurationSettings.AppSettings["DBReadOnlyConnection"];
                }
                else
                {
                    DocumentType.ConnectionString = ConfigurationSettings.AppSettings["DBConnectionDefault"];
                }
            }

            //------- For Read Only User in SQL Datasource Connection String   End-----------------
            //this.Page.ClientScript.RegisterStartupScript(GetType(), "heightL", "<script>height();</script>");
        }
        protected void cmbExport_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 Filter = int.Parse(Convert.ToString(drdExport.SelectedItem.Value));
            if (Filter != 0)
            {
                if (Session["exportval"] == null)
                {
                    Session["exportval"] = Filter;
                    bindexport(Filter);
                }
                else if (Convert.ToInt32(Session["exportval"]) != Filter)
                {
                    Session["exportval"] = Filter;
                    bindexport(Filter);
                }
            }
        }
        public void bindexport(int Filter)
        {
            //DocumentGrid.Columns[5].Visible = false;
            string filename = "Document Types List";
            exporter.FileName = filename;

            exporter.PageHeader.Left = "Document Types";
            exporter.PageFooter.Center = "[Page # of Pages #]";
            exporter.PageFooter.Right = "[Date Printed]";

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
        protected void DocumentGrid_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            //Purpose : Replace .ToString() with Convert.ToString(..)
            //Name : Sudip 
            // Dated : 21-12-2016

            if (e.RowType == GridViewRowType.Data)
            {
                int commandColumnIndex = 1;
                for (int i = 0; i < DocumentGrid.Columns.Count; i++)
                    if (DocumentGrid.Columns[i] is GridViewCommandColumn)
                    {
                        commandColumnIndex = i;
                        break;
                    }
                if (commandColumnIndex == 1)
                    return;
                //____Two colum has been hided so index of command column will be leass by 1 
                //commandColumnIndex = commandColumnIndex - 1;
                commandColumnIndex = commandColumnIndex - 2;
                DevExpress.Web.Rendering.GridViewTableCommandCell cell = e.Row.Cells[commandColumnIndex] as DevExpress.Web.Rendering.GridViewTableCommandCell;
                for (int i = 0; i < cell.Controls.Count; i++)
                {
                    DevExpress.Web.Rendering.GridCommandButtonsCell button = cell.Controls[i] as DevExpress.Web.Rendering.GridCommandButtonsCell;
                    if (button == null) return;
                    DevExpress.Web.Internal.InternalHyperLink hyperlink = button.Controls[0] as DevExpress.Web.Internal.InternalHyperLink;

                    if (hyperlink.Text == "Delete")
                    {
                        if (Convert.ToString(Session["PageAccess"]) == "DelAdd" || Convert.ToString(Session["PageAccess"]) == "Delete" || Convert.ToString(Session["PageAccess"]) == "All")
                        {
                            hyperlink.Enabled = true;
                            continue;
                        }
                        else
                        {
                            hyperlink.Enabled = false;
                            continue;
                        }
                    }


                }

            }

        }
        protected void DocumentGrid_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridViewEditFormEventArgs e)
        {
            //if (!DocumentGrid.IsNewRowEditing)
            //{
            //    ASPxGridViewTemplateReplacement RT = DocumentGrid.FindEditFormTemplateControl("UpdateButton") as ASPxGridViewTemplateReplacement;
            //    if (Session["PageAccess"].ToString().Trim() == "Add" || Session["PageAccess"].ToString().Trim() == "Modify" || Session["PageAccess"].ToString().Trim() == "All")
            //        RT.Visible = true;
            //    else
            //        RT.Visible = false;
            //}

        }
        protected void DocumentGrid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters == "s")
                DocumentGrid.Settings.ShowFilterRow = true;

            if (e.Parameters == "All")
            {
                DocumentGrid.FilterExpression = string.Empty;
            }
        }
        protected void DocumentGrid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {

            MasterDataCheckingBL masterdata = new MasterDataCheckingBL();
            int id = Convert.ToInt32(e.Keys[0]);
            int i = masterdata.DeleteMasterDocumentType(id);
            if (i == 1)
            {
                DocumentGrid.JSProperties["cpDelmsg"] = "Succesfully Deleted";
            }
            else
            {
                DocumentGrid.JSProperties["cpDelmsg"] = "Used in other modules. Cannot Delete.";
                e.Cancel = true;
            }


            //string Id = e.Keys[0].ToString();
            //int i = 0;
            //i = oDBEngine.DeleteValue("tbl_master_documentType", " dty_id='" + Id + "'");
            ////string KeyVal = e.Keys["Id"].ToString();

            ////string[,] acccode = oDBEngine.GetFieldValue("tbl_master_groupMaster,tbl_trans_group",
            ////  "grp_contactId", "gpm_id=grp_groupMaster and gpm_id='" + KeyVal + "'", 1);

            //if (i > 0)
            //{
            //    DocumentGrid.JSProperties["cpDelmsg"] = "Succesfully Deleted";
            //}
            //else
            //{
            //    //AccountGroup.JSProperties["cpDelmsg"] = "Cannot Delete. This AccountGroup Code Is In Use";
            //    DocumentGrid.JSProperties["cpDelmsg"] = "Used in other modules. Cannot Delete.";
            //    e.Cancel = true;
            //}

        }
        //Purpose: Add Edit and delete rights to document types
        //Name: Debjyoti Dhar.
        protected void DocumentGrid_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            if (!rights.CanDelete)
            {
                if (e.ButtonType == ColumnCommandButtonType.Delete)
                {
                    e.Visible = false;
                }
            }


            if (!rights.CanEdit)
            {
                if (e.ButtonType == ColumnCommandButtonType.Edit)
                {
                    e.Visible = false;
                }
            }

        }

        [WebMethod]
        public String UploadDoc()//Docuploads model
        {
            var httpRequest = HttpContext.Current.Request;
            //var UserID = Convert.ToString(HttpContext.Current.Request.Form["UserID"]);
            var fullPath="";
            if(httpRequest.Files.Count>0)
            {
                for (int i = 0; i < httpRequest.Files.Count; i++)
                {
                    string filename = httpRequest.Files[i].FileName;
                    if (filename != "")
                    {
                        int lastSlash = filename.LastIndexOf("\\");
                        string trailingPath = filename.Substring(lastSlash + 1);
                        fullPath = System.Web.Hosting.HostingEnvironment.MapPath("//CommonFolder//LeaveDoc") + "\\" + trailingPath;
                        httpRequest.Files[i].SaveAs(fullPath);
                    }
                }
                
                
            }
        //{
            //if (model.documents != null)
            //{
            //    if (!string.IsNullOrEmpty(model.DOCUMENT_NAME))
            //    {

            //        string fileName = Path.GetFileName(model.documents.FileName);
            //        int fileSize = model.documents.ContentLength;
            //        int Size = fileSize / 1000;
            //        string FileType = System.IO.Path.GetExtension(fileName);
            //        int SizeIcon = 0;
            //        if (System.IO.File.Exists(Server.MapPath("~/Commonfolder/OrganizationDocument/" + fileName)))
            //        {
            //            fileName = DateTime.Now.ToString("hhmmss") + fileName;
            //        }
            //        model.documents.SaveAs(Server.MapPath("~/Commonfolder/OrganizationDocument/" + fileName));
            //        string CS = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
            //        using (SqlConnection con = new SqlConnection(CS))
            //        {
            //            SqlCommand cmd = new SqlCommand("PRC_DocumentUploadFromOrganization", con);
            //            cmd.CommandType = CommandType.StoredProcedure;
            //            con.Open();
            //            cmd.Parameters.AddWithValue("@DOCUMENT_NAME", model.DOCUMENT_NAME);
            //            cmd.Parameters.AddWithValue("@ATTACHMENT_CODE", model.ATTACHMENT_CODE);
            //            cmd.Parameters.AddWithValue("@TYPE_ID", model.TYPE_ID);
            //            cmd.Parameters.AddWithValue("@ATTACHMENT", "~/Commonfolder/OrganizationDocument/" + fileName);
            //            cmd.Parameters.AddWithValue("@CREATED_BY", Convert.ToString(HttpContext.Current.Session["userid"]));
            //            cmd.ExecuteNonQuery();
            //        }
            //    }
            //    else
            //    {
            //        return ("Please enter description.");
            //    }
            //}
            //else
            //{
            //    return ("Please select file.");
            //}

            return Convert.ToString("OK");
        }

        public class Docuploads
        {
            public string DOCUMENT_NAME { get; set; }
            public string ATTACHMENT_CODE { get; set; }
            public string TYPE_ID { get; set; }
            public HttpPostedFileBase documents { get; set; }
        }
    }
}
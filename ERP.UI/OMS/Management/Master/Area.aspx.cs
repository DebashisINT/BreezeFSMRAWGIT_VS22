using System;
using System.Web;
using System.Web.UI;
//using DevExpress.Web;
using DevExpress.Web;
using BusinessLogicLayer;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using GemBox.Spreadsheet;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using ClosedXML.Excel;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Xml.Serialization;
using DevExpress.XtraPrinting;
using DataAccessLayer;
using ClosedXML.Excel;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DevExpress.XtraPrinting;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using ERP.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using System.Web.UI;


namespace ERP.OMS.Management.Master
{
    public partial class management_master_Area : ERP.OMS.ViewState_class.VSPage
    {
        public string pageAccess = "";
        string[] lengthIndex;
        BusinessLogicLayer.MasterDataCheckingBL masterChecking = new BusinessLogicLayer.MasterDataCheckingBL();
        //DBEngine oDBEngine = new DBEngine(string.Empty);
        BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(string.Empty);
        public EntityLayer.CommonELS.UserRightsForPage rights = new EntityLayer.CommonELS.UserRightsForPage();
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //'http://localhost:2957/InfluxCRM/management/testProjectMainPage_employee.aspx'
                string sPath = Convert.ToString(HttpContext.Current.Request.Url);
                oDBEngine.Call_CheckPageaccessebility(sPath);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/management/Master/Area.aspx");
            //------- For Read Only User in SQL Datasource Connection String   Start-----------------

            if (HttpContext.Current.Session["EntryProfileType"] != null)
            {
                if (Convert.ToString(HttpContext.Current.Session["EntryProfileType"]) == "R")
                {
                    insertupdate.ConnectionString = ConfigurationSettings.AppSettings["DBReadOnlyConnection"];
                }
                else
                {
                    insertupdate.ConnectionString = ConfigurationSettings.AppSettings["DBConnectionDefault"];
                }
            }

            //------- For Read Only User in SQL Datasource Connection String   End-----------------

            if (HttpContext.Current.Session["userid"] == null)
            {
                ////Page.ClientScript.RegisterStartupScript(GetType(), "SighOff", "<script>SignOff();</script>");
            }
            ////this.Page.ClientScript.RegisterStartupScript(GetType(), "heightL", "<script>height();</script>");

            if (!IsPostBack)
            { }
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
            Int32 Filter = int.Parse(Convert.ToString(drdExport.SelectedItem.Value));
            string filename = "Area";
            exporter.FileName = filename;

            exporter.PageHeader.Left = "Area";
            exporter.PageFooter.Center = "[Page # of Pages #]";
            exporter.PageFooter.Right = "[Date Printed]";
            switch (Filter)
            {
                case 1:
                    //exporter.WritePdfToResponse();

                    using (MemoryStream stream = new MemoryStream())
                    {
                        exporter.WritePdf(stream);
                        WriteToResponse("Area", true, "pdf", stream);
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
        protected void btndownload_click(object sender, EventArgs e)
        {
            try
            {
                //Page.ClientScript.RegisterStartupScript(GetType(), "JScript", "<script>jAlert('Please  Year')</script>");

                //if (hdnyear.Value == "")
                //{
                //    Page.ClientScript.RegisterStartupScript(GetType(), "JScript3", "<script >showexportpopup();</script>");
                //    return;
                //}
                DataTable dt = new DataTable();
                foreach (DataRow row in dt.Rows)
                {
                    row.Delete();
                }
                dt.Columns.Add("Country", typeof(String));
                dt.Columns.Add("State", typeof(String));
                dt.Columns.Add("City", typeof(String));
                dt.Columns.Add("Area", typeof(String));


                //var SrlNo = gridrateupdate.GetSelectedFieldValues("SrlNo");
                //var sProducts_ID = gridrateupdate.GetSelectedFieldValues("sProducts_ID");
                //var sProducts_Code = gridrateupdate.GetSelectedFieldValues("sProducts_Code");
                //var sProducts_Name = gridrateupdate.GetSelectedFieldValues("sProducts_Name");
                //var sProducts_Description = gridrateupdate.GetSelectedFieldValues("sProducts_Description");
                //if (SrlNo.Count <= 0)
                //{
                //    Page.ClientScript.RegisterStartupScript(GetType(), "JScript3", "<script >getbacktolisting();</script>");
                //    return;
                //}
                //for (int i = 0; i < SrlNo.Count; i++)
                //{
                //    DataRow dr = dt.NewRow();
                //    var Srno = SrlNo[i];
                //    var ProductsCode = sProducts_Code[i];
                //    var ProductName = sProducts_Name[i];

                //    dr["SrlNo"] = Srno.ToString();
                //    dr["Code"] = ProductsCode.ToString();
                //    dr["Name"] = ProductName.ToString();
                //    dr["Jan"] = "0.000";
                //    dr["Feb"] = "0.000";
                //    dr["Mar"] = "0.000";
                //    dr["Apr"] = "0.000";
                //    dr["May"] = "0.000";
                //    dr["Jun"] = "0.000";
                //    dr["Jul"] = "0.000";
                //    dr["Aug"] = "0.000";
                //    dr["Sep"] = "0.000";
                //    dr["Oct"] = "0.000";
                //    dr["Nov"] = "0.000";
                //    dr["Dec"] = "0.000";

                //    dt.Rows.Add(dr);
                //}
                //dt.Columns.Remove("SrlNo");

                #region excel export

                //string attachment = "attachment; filename=" + hdnyear.Value.ToString() + DateTime.Now + ".XLSX";
                //Response.ClearContent();
                //Response.AddHeader("content-disposition", attachment);
                //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                ////Response.ContentType = "application/vnd.ms-excel";
                //string tab = "";
                //foreach (DataColumn dc in dt.Columns)
                //{
                //    Response.Write(tab + dc.ColumnName);
                //    tab = "\t";
                //}
                //Response.Write("\n");
                //int j;
                //foreach (DataRow dr in dt.Rows)
                //{
                //    tab = "";
                //    for (j = 0; j < dt.Columns.Count; j++)
                //    {
                //        Response.Write(tab + dr[j].ToString());
                //        tab = "\t";
                //    }
                //    Response.Write("\n");
                //}                
                //Response.End();
                dt.TableName = "table";
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=Area.xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
                //Page.ClientScript.RegisterStartupScript(GetType(), "JScript", "<script>jAlert('Please Enter Valid PAN.!')</script>");

                #endregion excel export
            }
            catch
            {

            }

        }
        protected void btnImport_click(object sender, EventArgs e)
        {

            if (fileprod.PostedFile != null)
            {
                try
                {
                    if (fileprod.HasFile)
                    {
                        string FileName = Path.GetFileName(fileprod.PostedFile.FileName);
                        string Extension = Path.GetExtension(fileprod.PostedFile.FileName);
                        string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                        string FilePath = Server.MapPath("~/Temporary/") + FileName;
                        fileprod.SaveAs(FilePath);
                        Import_To_Grid(FilePath, Extension, "No");
                    }
                }
                catch (Exception)
                {

                }
            }
        }
        public void Import_To_Grid(string FilePath, string Extension, string isHDR)
        {
            try
            {
                if (fileprod.FileName.Trim() != "")
                {
                    string fileName = Path.GetFileName(fileprod.PostedFile.FileName);

                    string extention = fileName.Substring(fileName.IndexOf('.'), fileName.Length - fileName.IndexOf('.'));
                    extention = extention.TrimStart('.');
                    extention = extention.ToUpper();

                    if (extention == "XLS" || extention == "XLSX")
                    {
                        fileName = fileName.Replace(fileName.Substring(0, fileName.IndexOf('.')), "Productupload");
                        DataTable dt = new DataTable();
                        using (SpreadsheetDocument doc = SpreadsheetDocument.Open(FilePath, false))
                        {
                            Sheet sheet = doc.WorkbookPart.Workbook.Sheets.GetFirstChild<Sheet>();
                            Worksheet worksheet = (doc.WorkbookPart.GetPartById(sheet.Id.Value) as WorksheetPart).Worksheet;
                            IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>().DefaultIfEmpty();

                            foreach (Row row in rows)
                            {
                                if (row.RowIndex.Value == 1)
                                {
                                    foreach (Cell cell in row.Descendants<Cell>())
                                    {
                                        if (cell.CellValue != null)
                                        {
                                            dt.Columns.Add(GetValue(doc, cell));
                                        }
                                    }
                                }
                                else
                                {
                                    dt.Rows.Add();
                                    int i = 0;
                                    foreach (Cell cell in row.Descendants<Cell>())
                                    {
                                        if (cell.CellValue != null)
                                        {
                                            dt.Rows[dt.Rows.Count - 1][i] = GetValue(doc, cell);
                                        }
                                        i++;
                                    }
                                }
                            }
                        }

                        DataSet dsInst = new DataSet();
                        SqlConnection con = new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]));
                        SqlCommand cmd = new SqlCommand("Proc_RollickAreaImport", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.AddWithValue("@Action", Action);
                        cmd.Parameters.AddWithValue("@AREAMPORT", dt);
                        cmd.Parameters.AddWithValue("@user_Id", Convert.ToInt32(Session["userid"]));
                        cmd.Parameters.Add("@ReturnValue", SqlDbType.VarChar, 50);
                        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;
                        cmd.CommandTimeout = 0;
                        SqlDataAdapter Adap = new SqlDataAdapter();
                        Adap.SelectCommand = cmd;
                        Adap.Fill(dsInst);
                        int ReturnValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
                        if (ReturnValue == 1)
                        {
                            //Page.ClientScript.RegisterStartupScript(GetType(), "JScript", "<script>jAlert('Rate Imported Succesfully!')</script>");
                            Page.ClientScript.RegisterStartupScript(GetType(), "JScript3", "<script>hideimportpopup();</script>");
                        }
                        else if (ReturnValue == -10)
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "JScript3", "<script>hideimportfailurepopup();</script>");
                        }
                        //Session["Datlog"] = dtCmb;
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "JScript3", "<script>hideimportfailurepopup();</script>");
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        protected void gridlogCustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
        }
        protected void EntityServerModelogDataSource_Selecting(object sender, DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs e)
        {
            e.KeyExpression = "area_id";
            //string connectionString = ConfigurationManager.ConnectionStrings["crmConnectionString"].ConnectionString; MULTI
            string connectionString = Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]);
            DataTable dtCmb = new DataTable();
            string year = string.Empty;
            string strFromDate = Convert.ToString(hfFromDate.Value);
            string strToDate = Convert.ToString(hfToDate.Value);
            ERPDataClassesDataContext dc1 = new ERPDataClassesDataContext(connectionString);

            var q = from d in dc1.v_GetAreaImportLogs
                    //where d.Year == year
                    where d.CreateDate >= Convert.ToDateTime(strFromDate) && d.CreateDate <= Convert.ToDateTime(strToDate)
                    select d;
            e.QueryableSource = q;


        }
        private string GetValue(SpreadsheetDocument doc, Cell cell)
        {
            string value = cell.CellValue.InnerText;
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value)).InnerText;
            }
            return value;
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
        protected void AreaGrid_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == GridViewRowType.Data)
            {
                int commandColumnIndex = -1;
                for (int i = 0; i < AreaGrid.Columns.Count; i++)
                    if (AreaGrid.Columns[i] is GridViewCommandColumn)
                    {
                        commandColumnIndex = i;
                        break;
                    }
                if (commandColumnIndex == -1)
                    return;
                //____One colum has been hided so index of command column will be leass by 1 
                commandColumnIndex = commandColumnIndex - 2;
                DevExpress.Web.Rendering.GridViewTableCommandCell cell = e.Row.Cells[commandColumnIndex] as DevExpress.Web.Rendering.GridViewTableCommandCell;
                for (int i = 0; i < cell.Controls.Count; i++)
                {
                    DevExpress.Web.Rendering.GridCommandButtonsCell button = cell.Controls[i] as DevExpress.Web.Rendering.GridCommandButtonsCell;
                    if (button == null) return;
                    DevExpress.Web.Internal.InternalHyperLink hyperlink = button.Controls[0] as DevExpress.Web.Internal.InternalHyperLink;

                    if (hyperlink.Text == "Delete")
                    {
                        if (Convert.ToString(Session["PageAccess"]).Trim() == "DelAdd" || Convert.ToString(Session["PageAccess"]).Trim() == "Delete" || Convert.ToString(Session["PageAccess"]).Trim() == "All")
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
        protected void AreaGrid_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridViewEditFormEventArgs e)
        {
            if (!AreaGrid.IsNewRowEditing)
            {
                ASPxGridViewTemplateReplacement RT = AreaGrid.FindEditFormTemplateControl("UpdateButton") as ASPxGridViewTemplateReplacement;
                if (Convert.ToString(Session["PageAccess"]).Trim() == "Add" || Convert.ToString(Session["PageAccess"]).Trim() == "Modify" || Convert.ToString(Session["PageAccess"]).Trim() == "All")
                    RT.Visible = true;
                else
                    RT.Visible = false;
            }

        }
        protected void AreaGrid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            //if (e.Parameters == "s")
            //    AreaGrid.Settings.ShowFilterRow = true;

            //if (e.Parameters == "All")
            //{
            //    AreaGrid.FilterExpression = string.Empty;
            //}

            if (e.Parameters == "All")
            {
                AreaGrid.FilterExpression = string.Empty;
            }
            string[] CallVal = Convert.ToString(e.Parameters).Split('~');
            lengthIndex = e.Parameters.Split('~');
            if (Convert.ToString(lengthIndex[0]) == "Delete")
            {
                string PinId = Convert.ToString(Convert.ToString(CallVal[1]));
                int retValue = masterChecking.DeleteMasterArea(Convert.ToInt32(PinId));
                if (retValue > 0)
                {
                    Session["KeyVal"] = "Succesfully Deleted";
                    AreaGrid.JSProperties["cpDelmsg"] = "Succesfully Deleted";
                    AreaGrid.DataBind();
                }
                else
                {
                    Session["KeyVal"] = "Used in other modules. Cannot Delete.";
                    AreaGrid.JSProperties["cpDelmsg"] = "Used in other modules. Cannot Delete.";

                }

            }
        }

        protected void AreaGrid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            AreaGrid.SettingsText.PopupEditFormCaption = "Add Area";
        }

        protected void AreaGrid_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            AreaGrid.SettingsText.PopupEditFormCaption = "Modify Area";
        }

        protected void AreaGrid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            int cityid = Convert.ToInt32(e.NewValues["SId"]);
            string area = Convert.ToString(e.NewValues["name"]);

            if (e.IsNewRow)
            {
                DataTable dtadd = oDBEngine.GetDataTable("select area_name from tbl_master_area where city_id=" + cityid + " and area_name='" + area + "'");

                if (dtadd.Rows.Count > 0)
                {
                    e.RowError = "Area for the selected city already exists. Cannot Proceed.";
                    return;
                }
            }
            else
            {
                string oldarea = Convert.ToString(e.OldValues["name"]);
                if (oldarea != area)
                {
                    DataTable dtadd = oDBEngine.GetDataTable("select area_name from tbl_master_area where city_id=" + cityid + " and area_name='" + area + "'");
                    if (dtadd.Rows.Count > 0)
                    {
                        e.RowError = "Area for the selected city already exists. Cannot Proceed.";
                        return;
                    }
                }

            }
        }
        protected void AreaGrid_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            //if (!rights.CanDelete)
            //{
            //    if (e.ButtonType == ColumnCommandButtonType.Delete)
            //    {
            //        e.Visible = false;
            //    }
            //}


            //if (!rights.CanEdit)
            //{
            //    if (e.ButtonType == ColumnCommandButtonType.Edit)
            //    {
            //        e.Visible = false;
            //    }
            //}

        }

    }
}
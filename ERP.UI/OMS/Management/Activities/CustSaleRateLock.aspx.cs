using ClosedXML.Excel;
using DataAccessLayer;
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
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ERP.OMS.Management.Activities
{
    public partial class CustSaleRateLock : System.Web.UI.Page
    {
        BusinessLogicLayer.GenericMethod oGenericMethod;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["Datlog"] = null;
                Fromdt.Date = DateTime.Now;
                ToDate.Date = DateTime.Now;

                Hiddenvalidfrom.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Hiddenvalidupto.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                BindState(1);
            }
        }

        [WebMethod(EnableSession = true)]
        public static object GetCustomer(string SearchKey)
        {
            List<CustomerModel> listCust = new List<CustomerModel>();
            if (HttpContext.Current.Session["userid"] != null)
            {
                SearchKey = SearchKey.Replace("'", "''");
                // BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(ConfigurationManager.AppSettings["DBConnectionDefault"]);
                BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();

                DataTable cust = oDBEngine.GetDataTable(" select top 10 cnt_internalid ,uniquename ,Name ,Billing,Type   from v_SaleRateLock_customerDetails where uniquename like '%" + SearchKey + "%' or Name like '%" + SearchKey + "%'   order by Name");

                listCust = (from DataRow dr in cust.Rows
                            select new CustomerModel()
                            {
                                id = dr["cnt_internalid"].ToString(),
                                Na = dr["Name"].ToString(),
                                UId = Convert.ToString(dr["uniquename"]),
                                add = Convert.ToString(dr["Billing"]),
                                TYPE = Convert.ToString(dr["Type"])
                            }).ToList();
            }

            return listCust;
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public static object GetProduct(string SearchKey)
        {
            List<PosProductModel> listCust = new List<PosProductModel>();
            if (HttpContext.Current.Session["userid"] != null)
            {
                SearchKey = SearchKey.Replace("'", "''");

                // BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(ConfigurationManager.AppSettings["DBConnectionDefault"]);
                BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();

                DataTable cust = oDBEngine.GetDataTable("select top 10  Products_ID,Products_Name ,Products_Description ,sProduct_MinSalePrice  from v_Product_SaleRateLock where Products_Name like '%" + SearchKey + "%'  or Products_Description  like '%" + SearchKey + "%' order by Products_Name,Products_Description");

                listCust = (from DataRow dr in cust.Rows
                            select new PosProductModel()
                            {
                                id = dr["Products_ID"].ToString(),
                                Na = dr["Products_Name"].ToString(),
                                Des = Convert.ToString(dr["Products_Description"]),
                                MinSalePrice = Convert.ToString(dr["sProduct_MinSalePrice"])
                            }).ToList();
            }

            return listCust;
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public static string addSaleRateLock(string SaleRateLockID, List<String> CustID, List<String> ProductID, string DiscSalesPrice, string MinSalePrice, string discount, string fromdt, string todate, string action, string FixRate, String SCHEME)
        {
            try
            {
                DateTime fdt = Convert.ToDateTime(fromdt);
                DateTime tdt = Convert.ToDateTime(todate);
                if (fdt > tdt)
                {
                    return "-12";
                }
                else if (fdt == tdt)
                {
                    return "-13";
                }
                else
                {
                    DataTable dtvalue = new DataTable();
                    //dtvalue = null;
                    //if (dtvalue == null)
                    //{
                    dtvalue.Columns.Add("Entity", typeof(String));
                    dtvalue.Columns.Add("Product", typeof(String));
                    //}

                    foreach (String item in CustID)
                    {
                        foreach (String prod in ProductID)
                        {
                            dtvalue.Rows.Add(new Object[] { item, prod.Split('|')[0].ToString() });
                        }
                    }

                    //List<String> ProductList = ProductID.Split(',');
                    //BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(ConfigurationManager.AppSettings["DBConnectionDefault"]);
                    BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();


                    ProcedureExecute proc = new ProcedureExecute("PRC_SaleRateLock");
                    proc.AddVarcharPara("@SaleRateLockID", 50, SaleRateLockID);
                    //proc.AddVarcharPara("@CustomerID", 50, CustID);
                    // proc.AddIntegerPara("@ProductID", Convert.ToInt32(ProductID));
                    proc.AddDecimalPara("@DiscSalesPrice", 2, 18, Convert.ToDecimal(DiscSalesPrice));
                    proc.AddIntegerPara("@ApprovedBy", Convert.ToInt32(HttpContext.Current.Session["userid"]));
                    proc.AddVarcharPara("@ValidFrom", 50, fdt.ToString("yyyy-MM-dd HH:mm:ss"));
                    proc.AddVarcharPara("@ValidUpto", 50, tdt.ToString("yyyy-MM-dd HH:mm:ss"));
                    proc.AddDecimalPara("@MinSalePrice", 2, 18, Convert.ToDecimal(MinSalePrice));
                    proc.AddDecimalPara("@Disc", 2, 18, Convert.ToDecimal(discount));
                    proc.AddVarcharPara("@Action", 4000, action);
                    proc.AddDecimalPara("@FixedRate", 2, 18, Convert.ToDecimal(FixRate));
                    proc.AddVarcharPara("@SCHEME", 300, SCHEME);
                    proc.AddPara("@UDT_RATELIST", dtvalue);
                    DataTable dtSaleRateLock = proc.GetTable();
                    if (dtSaleRateLock.Rows.Count > 0)
                    {
                        if (dtSaleRateLock.Rows[0]["Insertmsg"].ToString() == "-11")
                        {
                            return "-11";
                        }
                        else
                        {
                            return "1";
                        }
                    }
                    else
                    {
                        return "0";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error occured";
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public static string DeleteSaleRateLock(string SaleRateLockID)
        {
            try
            {
                // BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(ConfigurationManager.AppSettings["DBConnectionDefault"]);
                BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();

                ProcedureExecute proc = new ProcedureExecute("PRC_SaleRateLock");
                proc.AddVarcharPara("@SaleRateLockID", 50, SaleRateLockID);
                proc.AddVarcharPara("@Action", 4000, "delete");
                DataTable dtSaleRateLock = proc.GetTable();
                if (dtSaleRateLock.Rows.Count > 0)
                {
                    if (dtSaleRateLock.Rows[0]["Insertmsg"].ToString() == "-999")
                    {
                        return "-999";
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

        protected void EntityServerModeDataSource_Selecting(object sender, DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs e)
        {
            e.KeyExpression = "SaleRateLockID";
            //string connectionString = ConfigurationManager.ConnectionStrings["crmConnectionString"].ConnectionString;
            string connectionString = Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]);

            ERPDataClassesDataContext dc = new ERPDataClassesDataContext(connectionString);

            var q = from d in dc.v_SaleRateLockLists
                    orderby d.SaleRateLockID descending
                    select d;
            e.QueryableSource = q;
        }

        protected void GridSaleRate_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public static List<SaleRateLock> GetSaleRateLock(string SaleRateLockID)
        {
            List<SaleRateLock> listSaleRateLock = new List<SaleRateLock>();
            if (HttpContext.Current.Session["userid"] != null)
            {
                // BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(ConfigurationManager.AppSettings["DBConnectionDefault"]);
                BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();

                ProcedureExecute proc = new ProcedureExecute("PRC_SaleRateLock");
                proc.AddVarcharPara("@Action", 50, "GetSaleRateLockDetails");
                proc.AddVarcharPara("@SaleRateLockID", 10, SaleRateLockID);
                DataTable dtSaleRateLock = proc.GetTable();

                listSaleRateLock = (from DataRow dr in dtSaleRateLock.Rows
                                    select new SaleRateLock()
                                    {
                                        CustomerID = dr["CustomerID"].ToString(),
                                        CustomerName = dr["CustName"].ToString(),
                                        ProductID = Convert.ToString(dr["ProductID"]),
                                        Products_Name = Convert.ToString(dr["Products_Name"]),
                                        DiscSalesPrice = dr["DiscSalesPrice"].ToString(),
                                        MinSalePrice = dr["MinSalePrice"].ToString(),
                                        Disc = Convert.ToString(dr["Disc"]),
                                        ValidFrom = Convert.ToString(dr["ValidFrom"]),
                                        ValidUpto = Convert.ToString(dr["ValidUpto"]),
                                        IsInUse = Convert.ToString(dr["IsInUse"]),
                                        FixRate = Convert.ToString(dr["FixedRate"]),
                                        Scheme = Convert.ToString(dr["Scheme"])
                                    }).ToList();
            }

            return listSaleRateLock;

        }

        protected void EntityServerModeDataProduct_Selecting(object sender, DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs e)
        {
            e.KeyExpression = "Products_ID";

            //string connectionString = ConfigurationManager.ConnectionStrings["crmConnectionString"].ConnectionString;
            string connectionString = Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]);

            ERPDataClassesDataContext dc = new ERPDataClassesDataContext(connectionString);

            string Userid = Convert.ToString(HttpContext.Current.Session["userid"]);
            BusinessLogicLayer.DBEngine BEngine = new BusinessLogicLayer.DBEngine();

            var q = from d in dc.v_Product_SaleRateLocks
                    orderby d.Products_ID descending
                    select d;

            e.QueryableSource = q;

        }

        protected void EntityServerModeData_Selecting(object sender, DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs e)
        {
            e.KeyExpression = "cnt_internalid";

            //string connectionString = ConfigurationManager.ConnectionStrings["crmConnectionString"].ConnectionString;
            string connectionString = Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]);

            ERPDataClassesDataContext dc = new ERPDataClassesDataContext(connectionString);

            string Userid = Convert.ToString(HttpContext.Current.Session["userid"]);
            BusinessLogicLayer.DBEngine BEngine = new BusinessLogicLayer.DBEngine();

            var q = from d in dc.v_SaleRateLock_customerDetails
                    orderby d.Name //descending
                    select d;
            e.QueryableSource = q;

        }

        protected void BindState(int countryID)
        {
            CmbState.Items.Clear();

            // oGenericMethod = new GenericMethod();
            oGenericMethod = new BusinessLogicLayer.GenericMethod();

            DataTable dtCmb = new DataTable();
            dtCmb = oGenericMethod.GetDataTable("Select id,state as name From tbl_master_STATE Where countryID=" + countryID + " Order By Name");//+ " Order By state "

            AspxHelper oAspxHelper = new AspxHelper();
            if (dtCmb.Rows.Count > 0)
            {
                //CmbState.Enabled = true;
                oAspxHelper.Bind_Combo(CmbState, dtCmb, "name", "id", 0);
            }
            else
                CmbState.Enabled = false;
        }

        public class SaleRateLock
        {
            public int SaleRateLockID { get; set; }
            public string CustomerID { get; set; }
            public string CustomerName { get; set; }
            public string ProductID { get; set; }
            public string Products_Name { get; set; }
            public string DiscSalesPrice { get; set; }
            public string MinSalePrice { get; set; }
            public string Disc { get; set; }
            public string ValidFrom { get; set; }
            public string ValidUpto { get; set; }
            public string IsInUse { get; set; }
            public string FixRate { get; set; }
            public string Scheme { get; set; }
        }

        public class PosProductModel
        {
            public string id { get; set; }
            public string Na { get; set; }
            public string Des { get; set; }
            public string MinSalePrice { get; set; }
        }

        public class CustomerModel
        {
            public string id { get; set; }
            public string Na { get; set; }
            public string UId { get; set; }
            public string add { get; set; }
            public string TYPE { get; set; }
        }

        protected void btndownload_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            try
            {
                BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();

                ProcedureExecute proc = new ProcedureExecute("PRC_SaleRateLock");
                proc.AddVarcharPara("@Action", 50, "GetImportData");
                proc.AddVarcharPara("@StateID", 10, CmbState.Value.ToString());
                dt = proc.GetTable();


                // GridViewExtension.ExportToXlsx(GetShopListTemplateByAreaExcel(dt, ""), dt, true, getXlsExportOptions());

            }
            catch { }
            //vnd.ms-excel
            //string attachment = "attachment; filename=ProductRate.xlsx";
            //Response.ClearContent();
            //Response.AddHeader("content-disposition", attachment);
            //Response.ContentType = "application/xlsx";
            //Response.Clear();


            //string tab = "";
            //foreach (DataColumn dc in dt.Columns)
            //{
            //    Response.Write(tab + dc.ColumnName);
            //    tab = "\t";
            //}
            //Response.Write("\n");
            //int i;
            //foreach (DataRow dr in dt.Rows)
            //{
            //    tab = "";
            //    for (i = 0; i < dt.Columns.Count; i++)
            //    {
            //        Response.Write(tab + dr[i].ToString());
            //        tab = "\t";
            //    }
            //    Response.Write("\n");
            //}

            //Response.Flush();
            //Response.End();




            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=ProductRate.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }


        }

        [WebMethod]
        [HttpGet]
        public static void download(String State)
        {
            DataTable dt = new DataTable();
            try
            {
                BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();

                ProcedureExecute proc = new ProcedureExecute("PRC_SaleRateLock");
                proc.AddVarcharPara("@Action", 50, "GetImportData");
                proc.AddVarcharPara("@StateID", 10, State);
                dt = proc.GetTable();

                //HttpContext.Current.Session["DownloadData"] = dt;

                GridViewExtension.ExportToXlsx(GetShopListTemplateByAreaExcel(dt, ""), dt, true, getXlsExportOptions());

            }
            catch { }
            // return "true";
        }

        private static XlsxExportOptionsEx getXlsExportOptions()
        {
            DevExpress.XtraPrinting.XlsxExportOptionsEx obj = new DevExpress.XtraPrinting.XlsxExportOptionsEx(DevExpress.XtraPrinting.TextExportMode.Text);
            obj.ExportMode = DevExpress.XtraPrinting.XlsxExportMode.SingleFile;
            obj.ExportType = DevExpress.Export.ExportType.WYSIWYG;

            return obj;
        }

        private static GridViewSettings GetShopListTemplateByAreaExcel(object datatable, String dates)
        {
            var settings = new GridViewSettings();
            settings.Name = "ProductRate";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "ProductRate";
            settings.Name = "ProductRate";

            DataTable dt = (DataTable)datatable;



            foreach (System.Data.DataColumn datacolumn in dt.Columns)
            {
                if (datacolumn.ColumnName == "STATE" || datacolumn.ColumnName == "Code"
                    || datacolumn.ColumnName == "Description" || datacolumn.ColumnName == "PricetoDistributor" || datacolumn.ColumnName == "PricetoRetailer")
                {
                    settings.Columns.Add(column =>
                    {
                        if (datacolumn.ColumnName == "STATE")
                        {
                            column.Caption = "STATE";
                        }
                        else if (datacolumn.ColumnName == "Code")
                        {
                            column.Caption = "Code";
                        }
                        else if (datacolumn.ColumnName == "Description")
                        {
                            column.Caption = "Description";
                        }
                        else if (datacolumn.ColumnName == "PricetoDistributor")
                        {
                            column.Caption = "Price to Distributor";
                        }
                        else if (datacolumn.ColumnName == "PricetoRetailer")
                        {
                            column.Caption = "Price to Retailer";
                        }


                        column.FieldName = datacolumn.ColumnName;

                        if (datacolumn.DataType.FullName == "System.DateTime")
                        {
                            column.PropertiesEdit.DisplayFormatString = "DD-MM-YYYY";
                        }

                    });
                }

            }


            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

        protected void ImportExcel(object sender, EventArgs e)
        {
            if (fileprod.HasFile)
            {
                string FileName = Path.GetFileName(fileprod.PostedFile.FileName);
                string Extension = Path.GetExtension(fileprod.PostedFile.FileName);
                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                string FilePath = Server.MapPath("~/Temporary/") + FileName;
                fileprod.SaveAs(FilePath);
                Import_To_Grid(FilePath, Extension, "No");

                File.Delete(FilePath);
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "PopupproductHide();", true);
        }

        public void Import_To_Grid(string FilePath, string Extension, string isHDR)
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
                                //dt.Rows.Add();
                                //int i = 0;
                                //foreach (Cell cell in row.Descendants<Cell>())
                                //{
                                //    if (cell.CellValue != null)
                                //    {
                                //        dt.Rows[dt.Rows.Count - 1][i] = GetValue(doc, cell);
                                //    }
                                //    i++;
                                //}
                                DataRow tempRow = dt.NewRow();
                                int columnIndex = 0;
                                foreach (Cell cell in row.Descendants<Cell>())
                                {
                                    // Gets the column index of the cell with data

                                    int cellColumnIndex = (int)GetColumnIndexFromName(GetColumnName(cell.CellReference));
                                    cellColumnIndex--; //zero based index
                                    if (columnIndex < cellColumnIndex)
                                    {
                                        do
                                        {
                                            tempRow[columnIndex] = ""; //Insert blank data here;
                                            columnIndex++;
                                        }
                                        while (columnIndex < cellColumnIndex);
                                    }
                                    try
                                    {
                                        tempRow[columnIndex] = GetValue(doc, cell);

                                    }
                                    catch
                                    {
                                        tempRow[columnIndex] = "";
                                    }

                                    columnIndex++;
                                }
                                dt.Rows.Add(tempRow);
                            }
                        }
                    }

                    DataTable dtCmb = new DataTable();
                    ProcedureExecute proc = new ProcedureExecute("PRC_SaleRateLock");
                    proc.AddPara("@Action", "InsertImportData");
                    proc.AddPara("@UDT_PRODUCTRATE", dt);
                    // proc.AddPara("@user_Id", Convert.ToInt32(HttpContext.Current.Session["userid"]));
                    dtCmb = proc.GetTable();

                    Session["Datlog"] = dtCmb;
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "jAlert('Successfully imported.');", true);
                    if (dtCmb != null && dtCmb.Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "jAlert('Successfully imported.', 'Alert', function (r) {if (r == true) {  OpenProductRateLogModal();} });", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), UniqueID, "jAlert('invalid File')", true);
                }
            }
        }

        public static int? GetColumnIndexFromName(string columnName)
        {
            //return columnIndex;
            string name = columnName;
            int number = 0;
            int pow = 1;
            for (int i = name.Length - 1; i >= 0; i--)
            {
                number += (name[i] - 'A' + 1) * pow;
                pow *= 26;
            }
            return number;
        }

        public static string GetColumnName(string cellReference)
        {
            // Create a regular expression to match the column name portion of the cell name.
            Regex regex = new Regex("[A-Za-z]+");
            Match match = regex.Match(cellReference);
            return match.Value;
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

        protected void gridProductRate_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

        }

        protected void ProductRateServerModeDataSource_Selecting(object sender, DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs e)
        {
            e.KeyExpression = "ID";
            string connectionString = Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]);
            ERPDataClassesDataContext dc = new ERPDataClassesDataContext(connectionString);
            var q = from d in dc.v_ProductRates
                    orderby d.ID descending
                    select d;
            e.QueryableSource = q;
        }

        protected void grid_RateLog_DataBinding(object sender, EventArgs e)
        {
            if (Session["Datlog"] != null)
            {
                grid_RateLog.DataSource = (DataTable)Session["Datlog"];
            }
        }

        protected void ProductRateLog_Callback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (Session["Datlog"] != null)
            {
                DataTable ComponentTable = (DataTable)Session["Datlog"];
                grid_RateLog.Selection.CancelSelection();
                grid_RateLog.DataSource = ComponentTable;
                grid_RateLog.DataBind();
            }
            else
            {
                grid_RateLog.Selection.CancelSelection();
                grid_RateLog.DataSource = null;
                grid_RateLog.DataBind();
            }
        }

        protected void cmbExport_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 Filter = int.Parse(Convert.ToString(drdExport.SelectedItem.Value));
            string filename = "Product Rate Import Log";
            exporter.FileName = filename;
            exporter.PageHeader.Left = "Product Rate Import Log";
            exporter.PageFooter.Center = "[Page # of Pages #]";
            exporter.PageFooter.Right = "[Date Printed]";
            switch (Filter)
            {
                case 1:
                    using (MemoryStream stream = new MemoryStream())
                    {
                        exporter.WritePdf(stream);
                        WriteToResponse("Area", true, "pdf", stream);
                    }
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
        }
    }
}
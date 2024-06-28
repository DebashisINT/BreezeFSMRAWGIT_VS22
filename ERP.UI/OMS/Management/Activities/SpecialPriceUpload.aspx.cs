//====================================================== Revision History ================================================================
//Rev Number DATE               VERSION          DEVELOPER              CHANGES
//1.0        03-06-2024         2.0.47            Priti               	0027493: Modification in ITC special price upload module.
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
using static ERP.OMS.Management.Activities.SpecialPriceUpload;
using static ERP.OMS.Management.Master.management_master_Employee;
using Microsoft.Ajax.Utilities;

namespace ERP.OMS.Management.Activities
{
    public partial class SpecialPriceUpload : System.Web.UI.Page
    {
        BusinessLogicLayer.GenericMethod oGenericMethod;
        BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(ConfigurationSettings.AppSettings["DBConnectionDefault"]);
        clsDropDownList oclsDropDownList = new clsDropDownList();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {            
                PageLoadBind();
            }
        }


        public void PageLoadBind()
        {
            ProcedureExecute proc = new ProcedureExecute("prc_SpecialPriceImportFromExcel");
            proc.AddVarcharPara("@Action", 200, "ALLPAGELOADDATA");
            proc.AddVarcharPara("@UserbranchHierarchy", 4000, Convert.ToString(HttpContext.Current.Session["userbranchHierarchy"])); 
            DataSet dt = proc.GetDataSet();

            if (dt.Tables[0].Rows.Count > 0)
            {
                ddlBRANCH.DataSource = dt.Tables[0];
                ddlBRANCH.DataBind();
            }
            else
            {
                ddlBRANCH.DataSource = null ;
                ddlBRANCH.DataBind();
            }

            if (dt.Tables[1].Rows.Count > 0)
            {
                cmbDesg.DataSource = dt.Tables[1];
                cmbDesg.DataBind();
            }
            else
            {
                cmbDesg.DataSource = null;
                cmbDesg.DataBind();
            }

            //ddlBRANCH.Items.Insert(0, new ListItem("--Select--", "0"));
            //cmbDesg.Items.Insert(0, new ListItem("--Select--", "0"));
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

                DataTable cust = oDBEngine.GetDataTable("select top 10  SPRODUCTSID,Products_Name ,Products_Description ,sProduct_MinSalePrice  from v_Product_SaleRateLock where Products_Name like '%" + SearchKey + "%'  or Products_Description  like '%" + SearchKey + "%' order by Products_Name,Products_Description");

                listCust = (from DataRow dr in cust.Rows
                            select new PosProductModel()
                            {
                                id = dr["SPRODUCTSID"].ToString(),
                                Na = dr["Products_Name"].ToString(),
                                Des = Convert.ToString(dr["Products_Description"])
                               // MinSalePrice = Convert.ToString(dr["sProduct_MinSalePrice"])
                            }).ToList();
            }

            return listCust;
        }

        //Rev 1.0
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public static object GetEmployee(string SearchKey,string DesignationId)
        {
            List<EmployeeModel> listEmployee = new List<EmployeeModel>();
            if (HttpContext.Current.Session["userid"] != null)
            {
                SearchKey = SearchKey.Replace("'", "''");
                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("prc_SpecialPriceImportFromExcel");
                proc.AddVarcharPara("@Action", 4000, "GetEmployee");
                proc.AddPara("@DesignationId", Convert.ToInt32(DesignationId));
                proc.AddPara("@SearchKey", SearchKey);
                dt = proc.GetTable();

                listEmployee = (from DataRow dr in dt.Rows
                                select new EmployeeModel()
                                {
                                    id = Convert.ToString(dr["cnt_internalId"]),
                                    Employee_Code = Convert.ToString(dr["cnt_UCC"]),
                                    Employee_Name = Convert.ToString(dr["Employee_Name"])
                                }).ToList();
            }

            return listEmployee;
        }
        //Rev 1.0 End

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public static string DeleteSpecialPrice(string SPECIALPRICEID)
        {
            try
            {
                BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();
                ProcedureExecute proc = new ProcedureExecute("prc_SpecialPriceImportFromExcel");
                proc.AddVarcharPara("@SPECIALPRICEID", 50, SPECIALPRICEID);
                proc.AddVarcharPara("@Action", 4000, "DeleteSpecialPrice");
                proc.AddIntegerPara("@USERID", Convert.ToInt32(HttpContext.Current.Session["userid"]));
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
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public static string InsertSpecialPrice( string ProductID,string BRANCH,string SPECIALPRICE, string DesignationId, string EMPINTERNALID, string SPECIALPRICEID)
        {
            try
            {
                string Action = "";
                if (SPECIALPRICEID!="")
                {
                    Action = "UpdateSpecialPrice";
                }
                else
                {
                    Action = "InsertSpecialPrice";
                }
                BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();
                ProcedureExecute proc = new ProcedureExecute("prc_SpecialPriceImportFromExcel");
                proc.AddVarcharPara("@Action",100, Action);             
                proc.AddDecimalPara("@SPECIALPRICE", 2, 18, Convert.ToDecimal(SPECIALPRICE));
                proc.AddIntegerPara("@ProductID", Convert.ToInt32(ProductID));
                proc.AddIntegerPara("@BranchId", Convert.ToInt32(BRANCH));
                proc.AddIntegerPara("@USERID", Convert.ToInt32(HttpContext.Current.Session["userid"]));
                //Rev 1.0 
                proc.AddIntegerPara("@DesignationId", Convert.ToInt32(DesignationId));
                proc.AddVarcharPara("@EMPINTERNALID", 100, EMPINTERNALID);
                proc.AddVarcharPara("@SPECIALPRICEID", 50, SPECIALPRICEID);
                //Rev 1.0 End

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
            catch (Exception ex)
            {
                return "Error occured";
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public static string UpdateSpecialPrice(string SPECIALPRICEID, string SPECIALPRICE)
        {
            try
            {
                BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();
                ProcedureExecute proc = new ProcedureExecute("prc_SpecialPriceImportFromExcel");
                proc.AddVarcharPara("@SPECIALPRICEID", 50, SPECIALPRICEID);
                proc.AddDecimalPara("@SPECIALPRICE", 2, 18, Convert.ToDecimal(SPECIALPRICE));
                proc.AddVarcharPara("@Action", 4000, "UpdateSpecialPrice");
                proc.AddIntegerPara("@USERID", Convert.ToInt32(HttpContext.Current.Session["userid"]));
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
            catch (Exception ex)
            {
                return "Error occured";
            }
        }

        protected void EntityServerModeDataSource_Selecting(object sender, DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs e)
        {
            e.KeyExpression = "SPECIALPRICEID";
            int User_id = Convert.ToInt32(Session["userid"]);
            string IsFilter = Convert.ToString(hfIsFilter.Value);
            string connectionString = Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]);
            if (IsFilter == "Y")
            {
                
                ERPDataClassesDataContext dc = new ERPDataClassesDataContext(connectionString);
                var q = from d in dc.PRODUCTSPECIALPRICELISTs
                        where d.USERID == User_id
                        orderby d.SEQ descending
                        select d;
                e.QueryableSource = q;
            }
            else
            { 
                ERPDataClassesDataContext dc = new ERPDataClassesDataContext(connectionString);
                var q = from d in dc.PRODUCTSPECIALPRICELISTs
                        where d.SEQ == 0
                        select d;
                e.QueryableSource = q;
            }
        }
        protected void CallbackPanel_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            string returnPara = Convert.ToString(e.Parameter);

            string strProduct_hiddenID = (Convert.ToString(txtProduct_hidden.Value) == "") ? "0" : Convert.ToString(txtProduct_hidden.Value);
            Task PopulateStockTrialDataTask = new Task(() => GetSpecialPriceUploaddata(strProduct_hiddenID));
            PopulateStockTrialDataTask.RunSynchronously();
        }
        public void GetSpecialPriceUploaddata(string Products)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlConnection con = new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]));
                SqlCommand cmd = new SqlCommand("PRC_PRODUCTSPECIALPRICE_LIST", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@COMPANYID", Convert.ToString(Session["LastCompany"]));
                cmd.Parameters.AddWithValue("@FINYEAR", Convert.ToString(Session["LastFinYear"]));
                cmd.Parameters.AddWithValue("@Products", Products);
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

        protected void GridSaleRate_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public static List<SPECIALPRICE> GetSpecialPrice(string SPECIALPRICEID)
        {
            List<SPECIALPRICE> listSaleRateLock = new List<SPECIALPRICE>();
            if (HttpContext.Current.Session["userid"] != null)
            {
                BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();
                ProcedureExecute proc = new ProcedureExecute("prc_SpecialPriceImportFromExcel");
                proc.AddVarcharPara("@Action", 50, "GetSpecialPrice");
                proc.AddVarcharPara("@SPECIALPRICEID", 10, SPECIALPRICEID);
                DataTable dtSPECIALPRICE = proc.GetTable();

                listSaleRateLock = (from DataRow dr in dtSPECIALPRICE.Rows
                                    select new SPECIALPRICE()
                                    {

                                        ProductID = Convert.ToString(dr["PRODUCT_ID"]),
                                        Products_Name = Convert.ToString(dr["sProducts_Name"]),
                                        SPECIAL_PRICE = dr["SPECIAL_PRICE"].ToString(),
                                        PRODUCT_CODE = dr["PRODUCT_CODE"].ToString(),
                                        branch_description = dr["branch_description"].ToString(),
                                        deg_designation = dr["deg_designation"].ToString(),
                                        Employee_Name = dr["Employee_Name"].ToString(),

                                        BRANCH_ID = dr["BRANCH_ID"].ToString(),
                                        DesignationID = dr["DesignationID"].ToString(),
                                        EMPINTERNALID = dr["EMPINTERNALID"].ToString(),

                                    }).ToList();
            }

            return listSaleRateLock;

        }

        

      
        public class SPECIALPRICE
        {
            public int SPECIALPRICEID { get; set; }
            public string PRODUCT_CODE { get; set; }
            public string SPECIAL_PRICE { get; set; }
            public string ProductID { get; set; }
            public string Products_Name { get; set; }

            public string branch_description { get; set; }

            public string deg_designation { get; set; }
            public string Employee_Name { get; set; }

            public string BRANCH_ID { get; set; }

            public string DesignationID { get; set; }
            public string EMPINTERNALID { get; set; }
  

           
        }


        public class PosProductModel
        {
            public string id { get; set; }
            public string Na { get; set; }
            public string Des { get; set; }
           // public string MinSalePrice { get; set; }
        }

       

        protected void btndownload_Click(object sender, EventArgs e)
        {
            string strFileName = "SpecialPriceUpload.xlsx";
            string strPath = (Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory) + "/CommonFolder/" + strFileName);

            Response.ContentType = "application/xlsx";
            Response.AppendHeader("Content-Disposition", "attachment; filename=SpecialPriceUpload.xlsx");
            Response.TransmitFile(strPath);
            Response.End();

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
            string fName = string.Empty;
            Boolean HasLog = false;
            if (fileprod.HasFile)
            {
                string FileName = Path.GetFileName(fileprod.PostedFile.FileName);
                string Extension = Path.GetExtension(fileprod.PostedFile.FileName);
                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                string FilePath = Server.MapPath("~/Temporary/") + FileName;
                fileprod.SaveAs(FilePath);

                string fileExtension = Path.GetExtension(FileName);

                if (fileExtension.ToUpper() != ".XLS" && fileExtension.ToUpper() != ".XLSX")
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "PageScript", "<script language='javascript'>jAlert('Uploaded file format not supported by the system');</script>");
                    return;
                }

                if (fileExtension.Equals(".xlsx"))
                {
                    fName = FileName.Replace(".xlsx", DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xlsx");
                }

                else if (fileExtension.Equals(".xls"))
                {
                    fName = FileName.Replace(".xls", DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xls");
                }

                else if (fileExtension.Equals(".csv"))
                {
                    fName = FileName.Replace(".csv", DateTime.Now.ToString("ddMMyyyyhhmmss") + ".csv");
                }

                Session["FileName"] = fName;


                HasLog = Import_To_Grid(FilePath, Extension, "No");

                File.Delete(FilePath);
            }

            if (HasLog == false)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "PageScript", "<script language='javascript'>jAlert('invalid File!'); </script>");

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "PageScript", "<script language='javascript'>jAlert('Import Process Successfully Completed!'); </script>");

            }

        }

        public Boolean Import_To_Grid(string FilePath, string Extension, string isHDR)
        {
            Boolean Success = false;
            Boolean HasLog = false;
            if (fileprod.FileName.Trim() != "")
            {
                string fileName = Path.GetFileName(fileprod.PostedFile.FileName);
                string extention = fileName.Substring(fileName.IndexOf('.'), fileName.Length - fileName.IndexOf('.'));
                extention = extention.TrimStart('.');
                extention = extention.ToUpper();
                int loopcounter = 1;

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

                    if (dt != null && dt.Rows.Count > 0)
                    {

                        foreach (DataRow row in dt.Rows)
                        {
                            loopcounter++;
                            try
                            {
                                string BRANCH = Convert.ToString(row["BRANCH"]);
                                string PRODUCTCODE = Convert.ToString(row["ITEM CODE"]);
                                string PRODUCTNAME = Convert.ToString(row["ITEM NAME"]);
                                string SPECIALPRICE = Convert.ToString(row["SPECIAL PRICE"]);
                                //Rev 1.0 
                                string USERLOGINID = Convert.ToString(row["USER LOGIN ID"]);
                                //Rev 1.0 End
                                DataSet dt2 = InsertSpecialPriceDataFromExcel(BRANCH, PRODUCTCODE, PRODUCTNAME, SPECIALPRICE, USERLOGINID
                                       );


                                if (dt2 != null && dt2.Tables[0].Rows.Count > 0)
                                {
                                    foreach (DataRow row2 in dt2.Tables[0].Rows)
                                    {
                                        Success = Convert.ToBoolean(row2["Success"]);
                                        HasLog = Convert.ToBoolean(row2["HasLog"]);
                                    }
                                }

                                if (!HasLog)
                                {
                                    string description = Convert.ToString(dt2.Tables[0].Rows[0]["MSG"]);
                                    int loginsert = InsertSpecialPriceImportLOg(PRODUCTCODE, SPECIALPRICE, BRANCH, description, "Failed", Session["FileName"].ToString(), loopcounter, USERLOGINID);
                                }

                                else
                                {
                                    string description = Convert.ToString(dt2.Tables[0].Rows[0]["MSG"]);
                                    int loginsert = InsertSpecialPriceImportLOg(PRODUCTCODE, SPECIALPRICE, BRANCH, description, "Success", Session["FileName"].ToString(), loopcounter, USERLOGINID);
                                }



                            }
                            catch (Exception ex)
                            {
                                Success = false;
                                HasLog = false;

                            }

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), UniqueID, "jAlert('invalid File')", true);
                    }


                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), UniqueID, "jAlert('invalid File')", true);
                }


            }
            return HasLog;
        }
        public int InsertSpecialPriceImportLOg(string PRODUCTCODE, string SPECIALPRICE, string BRANCH, string description, string status, string FileName,int loopcounter,string USERLOGINID)
        {

            int i;
            ProcedureExecute proc = new ProcedureExecute("prc_SpecialPriceImportFromExcel");
            proc.AddVarcharPara("@action", 150, "InsertSpecialPriceImportLOg");
            proc.AddVarcharPara("@BRANCH", 200, BRANCH);
            proc.AddVarcharPara("@PRODUCTCODE", 200, PRODUCTCODE);
            proc.AddPara("@SPECIALPRICE", SPECIALPRICE);
            proc.AddVarcharPara("@decription", 150, description);
            proc.AddVarcharPara("@status", 150, status);
            proc.AddVarcharPara("@FileName", 500, FileName);
            proc.AddPara("@loopcounter",loopcounter);
            //Rev 1.0 
            proc.AddVarcharPara("@USERLOGINID", 200, USERLOGINID);
            //Rev 1.0 End
            proc.AddIntegerPara("@UserId", Convert.ToInt32(Session["userid"]));
            i = proc.RunActionQuery();

            return i;
        }
        public DataSet InsertSpecialPriceDataFromExcel(string BRANCH, string PRODUCTCODE, string PRODUCTNAME, string SPECIALPRICE,string USERLOGINID)
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("prc_SpecialPriceImportFromExcel");
            proc.AddVarcharPara("@Action", 100, "InsertSpecialPriceDataFromExcel");
            proc.AddIntegerPara("@UserId", Convert.ToInt32(Session["userid"]));
            proc.AddVarcharPara("@BRANCH", 200, BRANCH);
            proc.AddVarcharPara("@PRODUCTCODE", 200, PRODUCTCODE);
            proc.AddVarcharPara("@PRODUCTNAME", 200, PRODUCTNAME);
            proc.AddPara("@SPECIALPRICE", SPECIALPRICE);
            proc.AddVarcharPara("@USERLOGINID", 200, USERLOGINID);
            ds = proc.GetDataSet();
            return ds;
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

      
        protected void GvImportDetailsSearch_DataBinding(object sender, EventArgs e)
        {
            string fileName = Convert.ToString(Session["FileName"]);
            BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();
            ProcedureExecute proc = new ProcedureExecute("prc_SpecialPriceImportFromExcel");
            proc.AddVarcharPara("@Action", 50, "GetSpecialPriceLOG");
            proc.AddVarcharPara("@FileName", 500, fileName);
            DataTable dt2 = proc.GetTable();
            GvImportDetailsSearch.DataSource = dt2;
        }
    }
}
using DevExpress.XtraReports.Templates;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TargetVsAchievement.Models;
using DataAccessLayer;
using System.Configuration;
using System.Data.OleDb;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DevExpress.Web.Mvc;
using DevExpress.Web;
using DocumentFormat.OpenXml.Wordprocessing;

namespace TargetVsAchievement.Areas.TargetVsAchievement.Controllers
{
    public class TargetSetUpController : Controller
    {
        // GET: TargetVsAchievement/TargetSetUp
        public ActionResult Index()
        {
            TempData["FromManualLog"] = null;
            TempData["ImportLog"] = null;
            TempData["TARGETASSIGNGRIDVIEW"] = null;
            return View();
        }
        public PartialViewResult _PartialTargetSetUpListing(TargetLevelSetupModel dd)
        {
            DataTable dt = new DataTable();
            String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
            SqlCommand sqlcmd = new SqlCommand();
            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();
            sqlcmd = new SqlCommand("FSM_TARGETASSIGN_LISTING", sqlcon);
            sqlcmd.Parameters.Add("@ACTION", dd.TargetType);
            sqlcmd.Parameters.Add("@USERID", Convert.ToString(Session["userid"]));
            sqlcmd.Parameters.Add("@fromdate", dd.Fromdate);
            sqlcmd.Parameters.Add("@todate", dd.Todate);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dt);
            sqlcon.Close();

            TempData["TARGETASSIGNGRIDVIEW"] = dt;
            TempData.Keep();
            return PartialView(dt);
        }
        public JsonResult Delete(string ID,string TargetType)
        {           
            int i;
            int rtrnvalue = 0;

            ProcedureExecute proc = new ProcedureExecute("FSM_TARGETASSIGN_LISTING");
            proc.AddNVarcharPara("@action", 50, "DELETE");
            proc.AddNVarcharPara("@TargetType",100, TargetType);
            proc.AddNVarcharPara("@TARGET_ID", 30, ID);
            proc.AddVarcharPara("@ReturnValue", 200, "0", QueryParameterDirection.Output);
            i = proc.RunActionQuery();
            rtrnvalue = Convert.ToInt32(proc.GetParaValue("@ReturnValue"));         

            return Json(rtrnvalue, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DownloadFormat(string TargetType)
        {
            string strFileName = "";
            if (TargetType == "SalesTarget")
            {
                strFileName = "Sales Target.xlsx";               
            }
            if (TargetType == "ProductTarget")
            {
                strFileName = "Product Target.xlsx";
            }
            if (TargetType == "BrandTarget")
            {
                strFileName = "Brand Target.xlsx";
            }
            if (TargetType == "WODTarget")
            {
                strFileName = "WOD Target.xlsx";
            }


            string strPath = "/CommonFolder/" + strFileName;
            string fullName = Server.MapPath("~" + strPath);
            byte[] fileBytes = GetFile(fullName);
            return File(
                fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, strFileName);
            
        }
        byte[] GetFile(string s)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
        }

        public ActionResult ImportExcelSalesTarget()
        {
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //HttpPostedFile file = OFDBankSelect.PostedFile;
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        String extension = Path.GetExtension(fname);
                        fname = DateTime.Now.Ticks.ToString() + extension;
                        fname = Path.Combine(Server.MapPath("~/Temporary/"), fname);
                        file.SaveAs(fname);
                        Import_To_GridSalesTarget(fname, extension, file);
                    }
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public Boolean Import_To_GridSalesTarget(string FilePath, string Extension, HttpPostedFileBase file)
        {
            Boolean Success = false;
            Boolean HasLog = false;
            int loopcounter = 1;
            if (file.FileName.Trim() != "")
            {
                if (Extension.ToUpper() == ".XLS" || Extension.ToUpper() == ".XLSX")
                {
                    DataSet ds = new DataSet();
                    using (SpreadsheetDocument doc = SpreadsheetDocument.Open(FilePath, false))
                    {
                        Sheet sheet = doc.WorkbookPart.Workbook.Sheets.GetFirstChild<Sheet>();
                        IEnumerable<Sheet> sheets = doc.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();

                        foreach (var item in sheets)
                        {
                            DataTable dt = new DataTable();
                            Worksheet worksheet = (doc.WorkbookPart.GetPartById(item.Id.Value) as WorksheetPart).Worksheet;
                            IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>();
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

                            ds.Tables.Add(dt);
                        }
                    }
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {                                 
                        try
                        {                            
                            DataTable dtCmb = new DataTable();
                            ProcedureExecute proc = new ProcedureExecute("PRC_TARGET_IMPORT");
                            proc.AddPara("@IMPORT_TABLE", ds.Tables[0]);
                            proc.AddPara("@ACTION", "INSERTSALESTARGET");                            
                            proc.AddPara("@USER_ID", Convert.ToInt32(Session["userid"]));
                            dtCmb = proc.GetTable();
                            HasLog = true;
                        }
                        catch (Exception ex)
                        {
                            HasLog = false;
                        }
                        
                    }
                }
            }
            return HasLog;
        }
        public ActionResult ImportExcelProductTarget()
        {
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //HttpPostedFile file = OFDBankSelect.PostedFile;
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        String extension = Path.GetExtension(fname);
                        fname = DateTime.Now.Ticks.ToString() + extension;
                        fname = Path.Combine(Server.MapPath("~/Temporary/"), fname);
                        file.SaveAs(fname);
                        Import_To_GridProductTarget(fname, extension, file);
                    }
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public Boolean Import_To_GridProductTarget(string FilePath, string Extension, HttpPostedFileBase file)
        {
            Boolean Success = false;
            Boolean HasLog = false;
            int loopcounter = 1;
            if (file.FileName.Trim() != "")
            {
                if (Extension.ToUpper() == ".XLS" || Extension.ToUpper() == ".XLSX")
                {
                    DataSet ds = new DataSet();
                    using (SpreadsheetDocument doc = SpreadsheetDocument.Open(FilePath, false))
                    {
                        Sheet sheet = doc.WorkbookPart.Workbook.Sheets.GetFirstChild<Sheet>();
                        IEnumerable<Sheet> sheets = doc.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();

                        foreach (var item in sheets)
                        {
                            DataTable dt = new DataTable();
                            Worksheet worksheet = (doc.WorkbookPart.GetPartById(item.Id.Value) as WorksheetPart).Worksheet;
                            IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>();
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

                            ds.Tables.Add(dt);
                        }


                    }
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {

                        try
                        {
                            DataTable dtCmb = new DataTable();
                            ProcedureExecute proc = new ProcedureExecute("PRC_TARGET_IMPORT");
                            proc.AddPara("@IMPORT_PRODUCTTABLE", ds.Tables[0]);
                            proc.AddPara("@ACTION", "INSERTPRODUCTTARGET");
                            proc.AddPara("@USER_ID", Convert.ToInt32(Session["userid"]));
                            dtCmb = proc.GetTable();
                            HasLog = true;
                        }
                        catch (Exception ex)
                        {
                            HasLog = false;
                        }

                    }
                }
            }
            return HasLog;
        }


        public ActionResult ImportExcelBrandTarget()
        {
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //HttpPostedFile file = OFDBankSelect.PostedFile;
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        String extension = Path.GetExtension(fname);
                        fname = DateTime.Now.Ticks.ToString() + extension;
                        fname = Path.Combine(Server.MapPath("~/Temporary/"), fname);
                        file.SaveAs(fname);
                        Import_To_GridBrandTarget(fname, extension, file);
                    }
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public Boolean Import_To_GridBrandTarget(string FilePath, string Extension, HttpPostedFileBase file)
        {
            Boolean Success = false;
            Boolean HasLog = false;
            int loopcounter = 1;
            if (file.FileName.Trim() != "")
            {
                if (Extension.ToUpper() == ".XLS" || Extension.ToUpper() == ".XLSX")
                {
                    DataSet ds = new DataSet();
                    using (SpreadsheetDocument doc = SpreadsheetDocument.Open(FilePath, false))
                    {
                        Sheet sheet = doc.WorkbookPart.Workbook.Sheets.GetFirstChild<Sheet>();
                        IEnumerable<Sheet> sheets = doc.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();

                        foreach (var item in sheets)
                        {
                            DataTable dt = new DataTable();
                            Worksheet worksheet = (doc.WorkbookPart.GetPartById(item.Id.Value) as WorksheetPart).Worksheet;
                            IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>();
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

                            ds.Tables.Add(dt);
                        }


                    }
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {

                        try
                        {
                            DataTable dtCmb = new DataTable();
                            ProcedureExecute proc = new ProcedureExecute("PRC_TARGET_IMPORT");
                            proc.AddPara("@IMPORT_BRANDTABLE", ds.Tables[0]);
                            proc.AddPara("@ACTION", "INSERTBRANDTARGET");
                            proc.AddPara("@USER_ID", Convert.ToInt32(Session["userid"]));
                            dtCmb = proc.GetTable();
                            HasLog = true;
                        }
                        catch (Exception ex)
                        {
                            HasLog = false;
                        }

                    }
                }
            }
            return HasLog;
        }

        public ActionResult ImportExcelWODTarget()
        {
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //HttpPostedFile file = OFDBankSelect.PostedFile;
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        String extension = Path.GetExtension(fname);
                        fname = DateTime.Now.Ticks.ToString() + extension;
                        fname = Path.Combine(Server.MapPath("~/Temporary/"), fname);
                        file.SaveAs(fname);
                        Import_To_GridWODTarget(fname, extension, file);
                    }
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public Boolean Import_To_GridWODTarget(string FilePath, string Extension, HttpPostedFileBase file)
        {
            Boolean Success = false;
            Boolean HasLog = false;
            int loopcounter = 1;
            if (file.FileName.Trim() != "")
            {
                if (Extension.ToUpper() == ".XLS" || Extension.ToUpper() == ".XLSX")
                {
                    DataSet ds = new DataSet();
                    using (SpreadsheetDocument doc = SpreadsheetDocument.Open(FilePath, false))
                    {
                        Sheet sheet = doc.WorkbookPart.Workbook.Sheets.GetFirstChild<Sheet>();
                        IEnumerable<Sheet> sheets = doc.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();

                        foreach (var item in sheets)
                        {
                            DataTable dt = new DataTable();
                            Worksheet worksheet = (doc.WorkbookPart.GetPartById(item.Id.Value) as WorksheetPart).Worksheet;
                            IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>();
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

                            ds.Tables.Add(dt);
                        }


                    }
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {

                        try
                        {
                            DataTable dtCmb = new DataTable();
                            ProcedureExecute proc = new ProcedureExecute("PRC_TARGET_IMPORT");
                            proc.AddPara("@IMPORT_WODTABLE", ds.Tables[0]);
                            proc.AddPara("@ACTION", "INSERTWODTARGET");
                            proc.AddPara("@USER_ID", Convert.ToInt32(Session["userid"]));
                            dtCmb = proc.GetTable();
                            HasLog = true;
                        }
                        catch (Exception ex)
                        {
                            HasLog = false;
                        }

                    }
                }
            }
            return HasLog;
        }
        public ActionResult ImportLog()
        {
            List<ImportLogModel> list = new List<ImportLogModel>();
            DataTable dt = new DataTable();
            try
            {
               
                    if (TempData["ImportLog"] != null && TempData["FromManualLog"] != null && Convert.ToString(TempData["FromManualLog"]) == "1")
                    {
                        dt = (DataTable)TempData["ImportLog"];
                    }
                    else
                    {
                        DataTable dtCmb = new DataTable();
                        ProcedureExecute proc = new ProcedureExecute("PRC_TARGET_IMPORT");
                        proc.AddPara("@Action", "SHOWIMPORTLOG");
                        proc.AddPara("@IMPORT_TABLE", (DataTable)TempData["ImportLog"]);
                        proc.AddPara("@User_Id", Convert.ToInt32(Session["userid"]));
                        dt = proc.GetTable();
                    }

                    TempData.Keep();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ImportLogModel data = null;
                        foreach (DataRow row in dt.Rows)
                        {
                            data = new ImportLogModel();
                            
                            data.IMPORT_TARGET_ID = Convert.ToString(row["IMPORT_TARGET_ID"]);
                            data.DocumentNo = Convert.ToString(row["DocumentNo"]);
                            data.DocumentDate = Convert.ToString(row["DocumentDate"]);
                            data.EmployeeGroup = Convert.ToString(row["EmployeeGroup"]);
                            data.TargetFor = Convert.ToString(row["TargetFor"]);
                            data.TARGETLEVELNAME = Convert.ToString(row["TARGETLEVELNAME"]);
                            data.TARGETLEVELCODE = Convert.ToString(row["TARGETLEVELCODE"]);
                            data.TIMEFRAME = Convert.ToString(row["TIMEFRAME"]);
                            data.STARTDATE = Convert.ToString(row["STARTDATE"]);
                            data.ENDDATE = Convert.ToString(row["ENDDATE"]);
                            data.NEWVISIT = Convert.ToString(row["NEWVISIT"]);
                            data.REVISIT = Convert.ToString(row["REVISIT"]);
                            data.ORDERAMOUNT = Convert.ToString(row["ORDERAMOUNT"]);
                            data.COLLECTION = Convert.ToString(row["COLLECTION"]);
                            data.ORDERQTY = Convert.ToString(row["ORDERQTY"]);
                            data.BRANDID = Convert.ToString(row["BRANDID"]);
                            data.BRANDNAME = Convert.ToString(row["BRANDNAME"]);
                            data.WODCOUNT = Convert.ToString(row["WODCOUNT"]);
                            data.PRODUCTID = Convert.ToString(row["PRODUCTID"]);
                            data.PRODUCTCODE = Convert.ToString(row["PRODUCTCODE"]);
                            data.PRODUCTNAME = Convert.ToString(row["PRODUCTNAME"]);
                            data.ImportStatus = Convert.ToString(row["ImportStatus"]);
                            data.ImportMsg = Convert.ToString(row["ImportMsg"]);
                           
                            data.CREATEDBY = Convert.ToString(row["CREATEDBY"]);
                            data.CREATEDON = Convert.ToString(row["CREATEDON"]);
                          
                            list.Add(data);
                        }
                    }
                    //TempData["EnquiriesImportLog"] = dt;
                

            }
            catch (Exception ex)
            {

            }
            TempData.Keep();
            return PartialView(list);
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
        [HttpPost]
        public JsonResult ImportManualLog(string Fromdt, String ToDate)
        {
            string output_msg = string.Empty;
            try
            {
                string datfrmat = Fromdt.Split('-')[2] + '-' + Fromdt.Split('-')[1] + '-' + Fromdt.Split('-')[0];
                string dattoat = ToDate.Split('-')[2] + '-' + ToDate.Split('-')[1] + '-' + ToDate.Split('-')[0];

                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_TARGET_IMPORT");
                proc.AddPara("@ACTION", "GETIMPORTLOG");
                proc.AddPara("@FromDate", datfrmat);
                proc.AddPara("@ToDate", dattoat);
                dt = proc.GetTable();

                if (dt != null && dt.Rows.Count > 0)
                {
                    TempData["ImportLog"] = dt;
                    TempData["FromManualLog"] = "1";
                    TempData.Keep();
                    output_msg = "True";
                }
                else
                {
                    output_msg = "Log not found.";
                }
            }
            catch (Exception ex)
            {
                output_msg = "Please try again later";
            }
            return Json(output_msg, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ExporSummaryList(int type, String Name)
        {
            
            DataTable dbDashboardData = new DataTable();
            

            if (TempData["TARGETASSIGNGRIDVIEW"] != null)
            {

                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetGridView(TempData["TARGETASSIGNGRIDVIEW"], Name), TempData["TARGETASSIGNGRIDVIEW"]);
                    
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetGridView(TempData["TARGETASSIGNGRIDVIEW"], Name), TempData["TARGETASSIGNGRIDVIEW"]);
                   
                    case 3:
                        return GridViewExtension.ExportToXls(GetGridView(TempData["TARGETASSIGNGRIDVIEW"], Name), TempData["TARGETASSIGNGRIDVIEW"]);
                    
                    case 4:
                        return GridViewExtension.ExportToRtf(GetGridView(TempData["TARGETASSIGNGRIDVIEW"], Name), TempData["TARGETASSIGNGRIDVIEW"]);
                    
                    case 5:
                        return GridViewExtension.ExportToCsv(GetGridView(TempData["TARGETASSIGNGRIDVIEW"], Name), TempData["TARGETASSIGNGRIDVIEW"]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetGridView(object datatable, String Name)
        {
            var settings = new GridViewSettings();
            
            settings.Name = Name;
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            
            settings.SettingsExport.FileName = Name;
           
            DataTable dt = (DataTable)datatable;

            foreach (System.Data.DataColumn datacolumn in dt.Columns)
            {
                
                settings.Columns.Add(column =>
                {

                    if (datacolumn.ColumnName == "Action")
                    {
                        column.CellStyle.CssClass = "hide";
                        column.HeaderStyle.CssClass = "hide";
                    }
                    else if (datacolumn.ColumnName == "ID")
                    {
                        column.CellStyle.CssClass = "hide";
                        column.HeaderStyle.CssClass = "hide";
                    }
                    else
                    {
                        column.Caption = datacolumn.ColumnName;
                        column.FieldName = datacolumn.ColumnName;
                    }

                    
                    //if (datacolumn.DataType.FullName == "System.Decimal" || datacolumn.DataType.FullName == "System.Int32" || datacolumn.DataType.FullName == "System.Int64")
                    //{
                        
                    //}
                });
               

            }

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

    }


    
}
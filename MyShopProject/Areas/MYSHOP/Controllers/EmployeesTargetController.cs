/****************************************************************************************************************************
*   Rev 27-12-2018 Surojit Chatterjee
*   1.0     v2.0.36     Sanchita    10/01/2023      Appconfig and User wise setting "IsAllDataInPortalwithHeirarchy = True" then 
*                                                   data in portal shall be populated based on Hierarchy Only. Refer: 25504
*********************************************************************************************************************************/

using BusinessLogicLayer.SalesmanTrack;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Configuration;
using System.Data.OleDb;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class EmployeesTargetController : Controller
    {
        EmployeeTarget objemployee = null;
        public EmployeesTargetController()
        {
            objemployee = new EmployeeTarget();
        }
        // GET: MYSHOP/EmployeesTarget
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EmployeesTargetSetting()
        {
            //Session["userid"] = 1700;
            TempData.Clear();
            TempData["EmployeesTargetList"] = null;
            TempData["EmployeesTargetListModel"] = null;
            EmployeesTargetSetting data = new EmployeesTargetSetting();
            data.StateList = GetEmployeeStateList("GetEmployeeState");
            data.DesignationList = GetEmployeeDesignationList("GetDesigList");
            data.TypeList = GetEmployeeTypeList(0);
            data.CounterTypeList = GetEmployeeTypeList(1);
            data.IsHierarchywiseTargetSettings = GetUserHierarchywiseTargetPermission();
            data.UserState = GetEmployeeStateList("GetUserState").FirstOrDefault();
            data.UserDesg = GetEmployeeDesignationList("GetEMPDesig").FirstOrDefault();
            if (Session["userid"] != null)
            {
                data.UserID = Convert.ToInt32(Session["userid"].ToString());
            }
            return View(data);
        }

        public ActionResult GetDownloadTemplateSettings()
        {
            EmployeesTargetSetting data = new EmployeesTargetSetting();
            EmployeeDesignation obj = new EmployeeDesignation();
            try
            {
                data.StateList = GetEmployeeStateList("GetEmployeeState");
                data.DesignationList = GetEmployeeDesignationList("GetDesigList");
                data.StateList.RemoveAt(0);
                data.DesignationList.RemoveAt(0);
            }
            catch { }
            return PartialView("_DownloadTemplateSettings", data);

        }

        public Boolean GetUserHierarchywiseTargetPermission()
        {
            Boolean IsHierarchywiseTargetSettings = false;
            try
            {
                string userid = Session["userid"].ToString();
                DataTable dt = objemployee.GetEmployeeList(Convert.ToInt32(userid), 0, 0, "", "", 0, 0, "GetUserHierarchywiseTarget");
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        IsHierarchywiseTargetSettings = Convert.ToBoolean(row["HierarchywiseTargetSettings"]);
                    }
                }
            }
            catch { }
            return IsHierarchywiseTargetSettings;
        }

        public ActionResult GetImportTemplateEmployeeSettings()
        {
            return PartialView("_ImportTemplateSettings");
        }

        [HttpPost]
        public ActionResult GetUploadFilesEmployeeSettings()
        {
            TempData["EmployeesTargetSettingImportLog"] = null;
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
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
                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/Temporary/"), fname);
                        file.SaveAs(fname);
                        Import_To_Grid(fname, extension, file);
                    }
                    // Returns message that successfully uploaded  
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

        public Int32 Import_To_Grid(string FilePath, string Extension, HttpPostedFileBase file)
        {
            Boolean Success = false;
            Int32 HasLog = 0;

            if (file.FileName.Trim() != "")
            {

                if (Extension.ToUpper() == ".XLS" || Extension.ToUpper() == ".XLSX")
                {
                    DataTable dt = new DataTable();

                    //DocumentFormat.OpenXml
                    //using (SpreadsheetDocument doc = SpreadsheetDocument.Open(FilePath, false))
                    //{
                    //    Sheet sheet = doc.WorkbookPart.Workbook.Sheets.GetFirstChild<Sheet>();
                    //    Worksheet worksheet = (doc.WorkbookPart.GetPartById(sheet.Id.Value) as WorksheetPart).Worksheet;
                    //    IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>().DefaultIfEmpty();
                    //    foreach (Row row in rows)
                    //    {
                    //        if (row.RowIndex.Value == 1)
                    //        {
                    //            foreach (Cell cell in row.Descendants<Cell>())
                    //            {
                    //                if (cell.CellValue != null)
                    //                {
                    //                    dt.Columns.Add(GetValue(doc, cell));
                    //                }
                    //            }
                    //        }
                    //        else
                    //        {
                    //            dt.Rows.Add();
                    //            int i = 0;
                    //            foreach (Cell cell in row.Descendants<Cell>())
                    //            {
                    //                if (cell.CellValue != null)
                    //                {
                    //                    dt.Rows[dt.Rows.Count - 1][i] = GetValue(doc, cell);
                    //                }
                    //                i++;
                    //            }
                    //        }
                    //    }

                    //}

                    DataTable dtExcelData = new DataTable();
                    string conString = string.Empty;
                    conString = ConfigurationManager.AppSettings["ExcelConString"];
                    conString = string.Format(conString, FilePath);
                    using (OleDbConnection excel_con = new OleDbConnection(conString))
                    {
                        excel_con.Open();
                        string sheet1 = "Sheet$";                    
                        using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
                        {
                            oda.Fill(dtExcelData);
                        }
                        excel_con.Close();
                    }
                    if (dtExcelData != null && dtExcelData.Rows.Count > 0)                    
                   // if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtExcelData.Rows)
                       // foreach (DataRow row in dt.Rows)
                        {
                            int Month = DateTime.Now.Month;
                            int NEWCOUNTER = 0;
                            int REVISIT = 0;
                            int ORDERVALUE = 0;
                            int COLLECTION = 0;

                            string strNEWCOUNTER = "0";
                            string strREVISIT = "0";
                            string strORDERVALUE = "0";
                            string strCOLLECTION = "0";

                            string Type = Convert.ToString(row["Type (EMP or PP or DD)"]);

                            int YEAR = DateTime.Now.Year;
                            if (row["Year (YYYY)"] != null)
                            {
                                YEAR = Convert.ToInt32(row["Year (YYYY)"]);
                            }
                            try
                            {
                                if (row["New Customer"] != null && Type == "EMP")
                                {
                                    strNEWCOUNTER = Convert.ToString(row["New Customer"]);
                                    NEWCOUNTER = Convert.ToInt32(row["New Customer"]);
                                }
                                if (row["Re-visit"] != null && Type == "EMP")
                                {
                                    strREVISIT = Convert.ToString(row["Re-visit"]);
                                    REVISIT = Convert.ToInt32(row["Re-visit"]);
                                }
                                if (row["Target Value"] != null)
                                {
                                    strORDERVALUE = Convert.ToString(row["Target Value"]);
                                    ORDERVALUE = Convert.ToInt32(row["Target Value"]);
                                }
                                if (row["Target Collection"] != null)
                                {
                                    strCOLLECTION = Convert.ToString(row["Target Collection"]);
                                    COLLECTION = Convert.ToInt32(row["Target Collection"]);
                                }

                                var monthname = Convert.ToString(row["Month (MMM)"]);
                                if (monthname.ToLower() == "jan")
                                {
                                    Month = 1;
                                }
                                else if (monthname.ToLower() == "feb")
                                {
                                    Month = 2;
                                }
                                else if (monthname.ToLower() == "mar")
                                {
                                    Month = 3;
                                }
                                else if (monthname.ToLower() == "apr")
                                {
                                    Month = 4;
                                }
                                else if (monthname.ToLower() == "may")
                                {
                                    Month = 5;
                                }
                                else if (monthname.ToLower() == "jun")
                                {
                                    Month = 6;
                                }
                                else if (monthname.ToLower() == "jul")
                                {
                                    Month = 7;
                                }
                                else if (monthname.ToLower() == "aug")
                                {
                                    Month = 8;
                                }
                                else if (monthname.ToLower() == "sep")
                                {
                                    Month = 9;
                                }
                                else if (monthname.ToLower() == "oct")
                                {
                                    Month = 10;
                                }
                                else if (monthname.ToLower() == "nov")
                                {
                                    Month = 11;
                                }
                                else if (monthname.ToLower() == "dec")
                                {
                                    Month = 12;
                                }
                                if (NEWCOUNTER > 0 || REVISIT > 0 || ORDERVALUE > 0 || COLLECTION > 0)
                                {
                                    DataTable dt2 = objemployee.EmployeesTargetByExcelInsertUpdate(
                                        Convert.ToString(row["Login ID/Mobile"]), strNEWCOUNTER, strREVISIT,
                                         strORDERVALUE, strCOLLECTION, Month,
                                         YEAR, Convert.ToString(row["Type (EMP or PP or DD)"]), Convert.ToString(row["Emp/PP/DD"]), Convert.ToString(row["Emp Designation"]),
                                         Convert.ToString(row["State"]), Convert.ToString(row["Supervisor/Assigned"]), Convert.ToString(row["Designation"]), "InputData",
                                        Convert.ToString(row["Stage"]));
                                    if (dt2 != null && dt2.Rows.Count > 0)
                                    {
                                        foreach (DataRow row2 in dt2.Rows)
                                        {
                                            Success = Convert.ToBoolean(row2["Success"]);
                                            HasLog = Convert.ToInt32(row2["HasLog"]);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message == "Input string was not in a correct format.")
                                {
                                    DataTable dt2 = objemployee.EmployeesTargetByExcelInsertUpdate(
                                       Convert.ToString(row["Login ID/Mobile"]), strNEWCOUNTER, strREVISIT,
                                        strORDERVALUE, strCOLLECTION, Month,
                                        YEAR, Convert.ToString(row["Type (EMP or PP or DD)"]), Convert.ToString(row["Emp/PP/DD"]), Convert.ToString(row["Emp Designation"]),
                                        Convert.ToString(row["State"]), Convert.ToString(row["Supervisor/Assigned"]), Convert.ToString(row["Designation"]), "LogInsert", Convert.ToString(row["Stage"])
                                       );
                                    if (dt2 != null && dt2.Rows.Count > 0)
                                    {
                                        foreach (DataRow row2 in dt2.Rows)
                                        {
                                            Success = Convert.ToBoolean(row2["Success"]);
                                            HasLog = Convert.ToInt32(row2["HasLog"]);
                                        }
                                    }
                                }
                            }

                        }
                    }

                }
            }
            return HasLog;
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


        //public ActionResult GetEmployeesListTemplateByStateDesignation(string State, string Designation)
        //{
        //    DataTable dt = new DataTable();
        //    List<EmployeesTargetSetting> list = new List<EmployeesTargetSetting>();
        //    try
        //    {
        //        dt = objemployee.GetEmployeeList(0, 0, 0, State, Designation, 0, 0, "GetTemplateByStateDesignation");
        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            EmployeesTargetSetting data = null;
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                data = new EmployeesTargetSetting();
        //                data.EmployeeCode = Convert.ToString(row["EMPCODE"]);
        //                data.Employeename = Convert.ToString(row["EmpName"]);
        //                data.ContactNo = Convert.ToString(row["LoginID"]);
        //                data.OrderValue = 0;
        //                data.NewCounter = 0;
        //                data.Collection = 0;
        //                data.Revisit = 0;
        //                data.Supervisor = Convert.ToString(row["ReportTo"]);
        //                data.state = Convert.ToString(row["StateName"]);
        //                data.Designation = Convert.ToString(row["Designation"]);
        //                data.Type = "EMP";
        //                data.MonthName = DateTime.Today.ToString("MMM");
        //                data.SettingYear = DateTime.Today.Year;
        //                list.Add(data);
        //            }
        //        }

        //    }
        //    catch { }
        //    return PartialView("_EmployeesListTemplateByStateDesignationList", list);
        //}


        public ActionResult GetEmployeesListTemplateByStateDesignation(string State, string Designation)
        {
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            List<EmployeesTargetSetting> list = new List<EmployeesTargetSetting>();
            try
            {
                dt = objemployee.GetEmployeeList(0, 0, 0, State, Designation, 0, 0, "GetTemplateByStateDesignation");
            }
            catch { }
            return GridViewExtension.ExportToXlsx(GetEmployeesListTemplateByStateDesignationExcel(dt, dt2), dt);
        }

        private GridViewSettings GetEmployeesListTemplateByStateDesignationExcel(object datatable, object ShopPPDD)
        {
            //List<EmployeesTargetSetting> obj = (List<EmployeesTargetSetting>)datatablelist;
            //ListtoDataTable lsttodt = new ListtoDataTable();
            //DataTable datatable = ConvertListToDataTable(obj); 
            var settings = new GridViewSettings();
            settings.Name = "EmployeesTargetTemplate";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "EmployeesTargetTemplate";
            //String ID = Convert.ToString(TempData["EmployeesTargetListDataTable"]);
            TempData.Keep();
            DataTable dt = (DataTable)datatable;

            foreach (System.Data.DataColumn datacolumn in dt.Columns)
            {
                if (datacolumn.ColumnName == "LoginID" || datacolumn.ColumnName == "EmpName" || datacolumn.ColumnName == "StateName"
                    || datacolumn.ColumnName == "NewCounter" || datacolumn.ColumnName == "TargetCollection" || datacolumn.ColumnName == "ReVisit" || datacolumn.ColumnName == "TargetValue"
                    || datacolumn.ColumnName == "ReportTo" || datacolumn.ColumnName == "Type" || datacolumn.ColumnName == "Designation" || datacolumn.ColumnName == "ReportToDesignation"
                    || datacolumn.ColumnName == "MonthName" || datacolumn.ColumnName == "Year" || datacolumn.ColumnName == "Stage")
                {
                    settings.Columns.Add(column =>
                    {
                        if (datacolumn.ColumnName == "LoginID")
                        {
                            column.Caption = "Login ID/Mobile";
                        }
                        else if (datacolumn.ColumnName == "ReVisit")
                        {
                            column.Caption = "Re-visit";
                        }
                        else if (datacolumn.ColumnName == "MonthName")
                        {
                            column.Caption = "Month (MMM)";
                        }
                        else if (datacolumn.ColumnName == "Year")
                        {
                            column.Caption = "Year (YYYY)";
                        }
                        else if (datacolumn.ColumnName == "ReportToDesignation")
                        {
                            column.Caption = "Designation";
                        }
                        else if (datacolumn.ColumnName == "Stage")
                        {
                            column.Caption = "Stage";
                        }
                        else if (datacolumn.ColumnName == "Designation")
                        {
                            column.Caption = "Emp Designation";
                        }
                        else if (datacolumn.ColumnName == "ReportTo")
                        {
                            column.Caption = "Supervisor/Assigned";
                        }
                        else if (datacolumn.ColumnName == "StateName")
                        {
                            column.Caption = "State";
                        }
                        else if (datacolumn.ColumnName == "EmpName")
                        {
                            column.Caption = "Emp/PP/DD";
                        }
                        else if (datacolumn.ColumnName == "Type")
                        {
                            column.Caption = "Type (EMP or PP or DD)";
                        }

                        else if (datacolumn.ColumnName == "TargetCollection")
                        {
                            column.Caption = "Target Collection";
                        }
                        else if (datacolumn.ColumnName == "TargetValue")
                        {
                            column.Caption = "Target Value";
                        }
                        else if (datacolumn.ColumnName == "NewCounter")
                        {
                            column.Caption = "New Customer";
                        }
                        else if (datacolumn.ColumnName == "ReVisit")
                        {
                            column.Caption = "Re-Visit";
                        }
                        else
                        {
                            column.Caption = datacolumn.ColumnName;
                        }

                        column.FieldName = datacolumn.ColumnName;
                        if (datacolumn.DataType.FullName == "System.Decimal")
                        {
                            column.PropertiesEdit.DisplayFormatString = "0.00";
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

        public ActionResult GetEmployeesTargetList(string State, Int32 EmpTypeID = 0, Int32 CounterType = 0, string Designation = null, Int32 SettingMonth = 0, Int32 SettingYear = 0)
        {
            List<EmployeesTargetSetting> list = new List<EmployeesTargetSetting>();
            EmployeesTargetSetting obj = new EmployeesTargetSetting();
            DataTable dt = new DataTable();
             DataTable dt2 = new DataTable();
            try
            {
                if (TempData["EmployeesTargetList"] != null)
                {
                    list = (List<EmployeesTargetSetting>)TempData["EmployeesTargetList"];
                }
                ViewBag.CounterType = CounterType;
                //string userid = Session["userid"].ToString();
                //TempData["Designation"] = Designation;
                //TempData.Keep();
                //ViewBag.CounterType = CounterType;
                ////dt = objemployee.GetEmployeeList(Convert.ToInt32(userid), EmpTypeID, CounterType, State, Designation, SettingMonth, SettingYear, "GetList");
                //dt = (DataTable)TempData["EmployeesTargetList"];
                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    EmployeesTargetSetting data = null;
                //    foreach (DataRow row in dt.Rows)
                //    {
                //        data = new EmployeesTargetSetting();
                //        data.user_id = Convert.ToInt32(row["user_id"]);
                //        data.EmployeeCode = Convert.ToString(row["EmployeeCode"]);
                //        data.Employeename = Convert.ToString(row["Employeename"]);
                //        //data.state = Convert.ToString(row["state"]);
                //        data.ContactNo = Convert.ToString(row["ContactNo"]);
                //        //data.Designation = Convert.ToString(row["Designation"]);
                //        //data.Supervisor = Convert.ToString(row["Supervisor"]);
                //        //data.SettingMonthYear = Convert.ToString(row["SettingMonthYear"]);
                //        data.OrderValue = Convert.ToDecimal(row["OrderValue"]);
                //        data.NewCounter = Convert.ToInt32(row["NewCounter"]);
                //        data.Collection = Convert.ToDecimal(row["Collection"]);
                //        data.Revisit = Convert.ToInt32(row["Revisit"]);
                //        data.CreatedDate = Convert.ToDateTime(row["CreatedDate"]);
                //        data.ModifiedDate = Convert.ToDateTime(row["ModifiedDate"]);
                //        data.EmployeeTargetSettingID = Convert.ToInt32(row["EmployeeTargetSettingID"]);

                //        data.EmpTypeName = Convert.ToString(row["EmpTypeName"]);
                //        data.CounterTypeName = Convert.ToString(row["CounterTypeName"]);
                //        list.Add(data);
                //    }
                //}

                //dt2 = objemployee.GetEmployeeList(Convert.ToInt32(userid), 0, 0, "", "", SettingMonth, SettingYear, "GetByID");

                //if (dt2 != null && dt2.Rows.Count > 0)
                //{
                //    foreach (DataRow row in dt2.Rows)
                //    {
                //        obj.EmployeeTargetSettingID = Convert.ToInt32(row["EmployeeTargetSettingID"]);
                //        obj.EmployeeCode = Convert.ToString(row["EmployeeCode"]);
                //        obj.OrderValue = Convert.ToDecimal(row["OrderValue"]);
                //        obj.NewCounter = Convert.ToInt32(row["NewCounter"]);
                //        obj.Collection = Convert.ToDecimal(row["Collection"]);
                //        obj.Revisit = Convert.ToInt32(row["Revisit"]);
                //    }
                //}

            }
            catch (Exception ex)
            {

            }
            //TempData["EmployeesTargetList"] = dt;
            //TempData["EmployeesTargetListModel"] = list;
            ////TempData["GetEmployeesTargetByID"] = dt2;
            TempData.Keep();
            //ViewBag.Data = obj;
            //return PartialView("_EmployeesTargetList", list);
            return PartialView("_EmployeesTargetSettingList", list);
        }

        public JsonResult GetClearEmployeeList()
        {
            Boolean Success = false;
            try
            {
                TempData.Clear();
                TempData["EmployeesTargetList"] = null;
                TempData.Keep();

                Success = true;
            }
            catch (Exception ex)
            {

            }
            return Json(Success);
        }

        public JsonResult GetEmployeesCounterTargetListSet(string EmployeeCode, Int32 EmployeeTargetSettingID = 0, Int32 CounterType = 0)
        {
            List<EmployeesTargetSettingCounterTarget> list = new List<EmployeesTargetSettingCounterTarget>();
            DataTable dt = new DataTable();
            try
            {
                ViewBag.CounterType = CounterType;
                if (TempData["EmployeesCounterTargetList" + EmployeeCode] != null)
                {
                    list = (List<EmployeesTargetSettingCounterTarget>)TempData["EmployeesCounterTargetList" + EmployeeCode];
                }
                else
                {
                    dt = objemployee.GetEmployeesCounterTargetList(EmployeeCode, EmployeeTargetSettingID, 0);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        EmployeesTargetSettingCounterTarget data = null;
                        foreach (DataRow row in dt.Rows)
                        {
                            data = new EmployeesTargetSettingCounterTarget();
                            data.EmployeesCounterTargetID = Convert.ToInt32(row["EmployeesCounterTargetID"]);
                            data.EmployeeCode = Convert.ToString(row["EmployeeCode"]);
                            data.FKEmployeeTargetSettingID = Convert.ToInt32(row["FKEmployeeTargetSettingID"]);
                            //data.state = Convert.ToString(row["state"]);
                            data.Shop_Code = Convert.ToString(row["Shop_Code"]);
                            data.Shop_Name = Convert.ToString(row["Shop_Name"]);
                            //data.Designation = Convert.ToString(row["Designation"]);
                            //data.Supervisor = Convert.ToString(row["Supervisor"]);
                            //data.SettingMonthYear = Convert.ToString(row["SettingMonthYear"]);
                            data.OrderValue = Convert.ToDecimal(row["OrderValue"]);
                            data.CollectionValue = Convert.ToDecimal(row["CollectionValue"]);
                            data.CreatedDate = Convert.ToDateTime(row["CreatedDate"]);
                            data.ModifiedDate = Convert.ToDateTime(row["ModifiedDate"]);
                            list.Add(data);
                        }
                    }
                    TempData["EmployeesCounterTargetList" + EmployeeCode] = list;
                }
                TempData.Keep();
            }
            catch (Exception ex)
            {

            }
            return Json(list);
        }

        public ActionResult GetEmployeesCounterTargetList(string EmployeeCode, Int32 EmployeeTargetSettingID = 0, Int32 CounterType = 0)
        {
            List<EmployeesTargetSettingCounterTarget> list = new List<EmployeesTargetSettingCounterTarget>();
            DataTable dt = new DataTable();
            try
            {
                ViewBag.CounterType = CounterType;
                if (TempData["EmployeesCounterTargetList" + EmployeeCode] != null)
                {
                    list = (List<EmployeesTargetSettingCounterTarget>)TempData["EmployeesCounterTargetList" + EmployeeCode];
                }
                else
                {
                    dt = objemployee.GetEmployeesCounterTargetList(EmployeeCode, EmployeeTargetSettingID, 0);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        EmployeesTargetSettingCounterTarget data = null;
                        foreach (DataRow row in dt.Rows)
                        {
                            data = new EmployeesTargetSettingCounterTarget();
                            data.EmployeesCounterTargetID = Convert.ToInt32(row["EmployeesCounterTargetID"]);
                            data.EmployeeCode = Convert.ToString(row["EmployeeCode"]);
                            data.FKEmployeeTargetSettingID = Convert.ToInt32(row["FKEmployeeTargetSettingID"]);
                            //data.state = Convert.ToString(row["state"]);
                            data.Shop_Code = Convert.ToString(row["Shop_Code"]);
                            data.Shop_Name = Convert.ToString(row["Shop_Name"]);
                            //data.Designation = Convert.ToString(row["Designation"]);
                            //data.Supervisor = Convert.ToString(row["Supervisor"]);
                            //data.SettingMonthYear = Convert.ToString(row["SettingMonthYear"]);
                            data.OrderValue = Convert.ToDecimal(row["OrderValue"]);
                            data.CollectionValue = Convert.ToDecimal(row["CollectionValue"]);
                            data.CreatedDate = Convert.ToDateTime(row["CreatedDate"]);
                            data.ModifiedDate = Convert.ToDateTime(row["ModifiedDate"]);
                            list.Add(data);
                        }
                    }
                    TempData["EmployeesCounterTargetList" + EmployeeCode] = list;
                }
            }
            catch (Exception ex)
            {

            }
            TempData.Keep();
            return PartialView("_EmployeesCounterTargetList", list);
        }


        public ActionResult GetEmployeesTargetSettingImportLog(string fromdate = null, string todate = null)
        {
            List<EmployeesTargetSettingImportLog> list = new List<EmployeesTargetSettingImportLog>();
            DataTable dt = new DataTable();
            try
            {
                if (TempData["EmployeesTargetSettingImportLog"] == null || ((fromdate != null && todate != null) && (fromdate != "" && todate != "")))
                {

                    dt = objemployee.GetEmployeesTargetSettingImportLog(fromdate, todate);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        EmployeesTargetSettingImportLog data = null;
                        foreach (DataRow row in dt.Rows)
                        {
                            data = new EmployeesTargetSettingImportLog();
                            data.LogID = Convert.ToInt32(row["LogID"]);
                            data.Mobile = Convert.ToString(row["Mobile"]);
                            data.Name = Convert.ToString(row["Name"]);
                            data.Supervisor_Assigned = Convert.ToString(row["Supervisor_Assigned"]);
                            data.NewCounter = Convert.ToString(row["NewCounter"]);
                            data.Revisit = Convert.ToString(row["Revisit"]);
                            data.TargetValue = Convert.ToString(row["TargetValue"]);
                            data.TargetCollection = Convert.ToString(row["TargetCollection"]);
                            data.Type = Convert.ToString(row["Type"]);
                            data.Status = Convert.ToBoolean(row["Status"]);
                            data.Message = Convert.ToString(row["Message"]);
                            data.IsShow = Convert.ToBoolean(row["IsShow"]);
                            data.CreatedDate = Convert.ToDateTime(row["CreatedDate"]);
                            data.CreatedDateTxt = Convert.ToDateTime(row["CreatedDate"]).ToString("dd-MM-yyyy hh:mm tt");
                            list.Add(data);
                        }
                    }
                    TempData["EmployeesTargetSettingImportLog"] = dt;
                }
                else
                {
                    dt = (DataTable)TempData["EmployeesTargetSettingImportLog"];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        EmployeesTargetSettingImportLog data = null;
                        foreach (DataRow row in dt.Rows)
                        {
                            data = new EmployeesTargetSettingImportLog();
                            data.LogID = Convert.ToInt32(row["LogID"]);
                            data.Mobile = Convert.ToString(row["Mobile"]);
                            data.Name = Convert.ToString(row["Name"]);
                            data.Supervisor_Assigned = Convert.ToString(row["Supervisor_Assigned"]);
                            data.NewCounter = Convert.ToString(row["NewCounter"]);
                            data.Revisit = Convert.ToString(row["Revisit"]);
                            data.TargetValue = Convert.ToString(row["TargetValue"]);
                            data.TargetCollection = Convert.ToString(row["TargetCollection"]);
                            data.Type = Convert.ToString(row["Type"]);
                            data.Status = Convert.ToBoolean(row["Status"]);
                            data.Message = Convert.ToString(row["Message"]);
                            data.IsShow = Convert.ToBoolean(row["IsShow"]);
                            data.CreatedDate = Convert.ToDateTime(row["CreatedDate"]);
                            list.Add(data);
                        }
                    }
                    TempData["EmployeesTargetSettingImportLog"] = dt;
                }

            }
            catch (Exception ex)
            {

            }
            TempData.Keep();
            return PartialView("_ShowEmployeesTargetSettingLog", list);
        }

        public JsonResult GetTotalOrderCollectionValue(String employeecode)
        {
            EmployeesTargetSettingCounterTarget data = new EmployeesTargetSettingCounterTarget();
            List<EmployeesTargetSettingCounterTarget> list = new List<EmployeesTargetSettingCounterTarget>();
            try
            {
                if (TempData["EmployeesCounterTargetList" + employeecode] != null)
                {
                    list = (List<EmployeesTargetSettingCounterTarget>)TempData["EmployeesCounterTargetList" + employeecode];

                    data.NewOrderValue = list.Sum(x => Convert.ToInt32(x.OrderValue));
                    data.NewCollectionValue = list.Sum(x => Convert.ToInt32(x.CollectionValue));
                }
            }
            catch { }
            TempData.Keep();
            return Json(data);
        }

        [ValidateInput(false)]
        public ActionResult BatchEditingUpdateEmployeesTarget(MVCxGridViewBatchUpdateValues<EmployeesTargetSetting, int> updateValues, EmployeesTargetSetting options)
        {
            Boolean Success = false;
            Boolean IsProcess = false;
            DataTable dt2 = (DataTable)TempData["GetEmployeesTargetByID"];
            TempData.Keep();
            Decimal MyOrderValue = 0;
            Int32 MyNewCounter = 0;
            Int32 MyRevisit = 0;
            Decimal MyCollection = 0;

            Decimal OrderValue = 0;
            Int32 NewCounter = 0;
            Int32 Revisit = 0;
            Decimal Collection = 0;
            //if (dt2 != null)
            //{
            //    OrderValue = updateValues.Update.Sum(item => item.OrderValue);
            //    NewCounter = updateValues.Update.Sum(item => item.NewCounter);
            //    Revisit = updateValues.Update.Sum(item => item.Revisit);
            //    Collection = updateValues.Update.Sum(item => item.Collection);

            //    var test = dt2.Compute("Sum(OrderValue)", string.Empty);
            //    foreach (DataRow dr in dt2.Rows)
            //    {
            //        MyOrderValue = Convert.ToDecimal(dr["OrderValue"]);
            //        MyNewCounter = Convert.ToInt32(dr["NewCounter"]);
            //        MyRevisit = Convert.ToInt32(dr["Revisit"]);
            //        MyCollection = Convert.ToDecimal(dr["Collection"]);
            //    }

            //    if (MyOrderValue >= OrderValue && MyNewCounter >= NewCounter && MyRevisit >= Revisit && Collection >= MyCollection)
            //    {
            //        IsProcess = true;
            //        foreach (var product in updateValues.Update)
            //        {
            //            if (updateValues.IsValid(product))
            //                Success = EmployeesTargetByCodeInsertUpdate(product, options);
            //        }
            //    }

            //}

            foreach (var product in updateValues.Update)
            {
                if (updateValues.IsValid(product))
                {
                    Success = EmployeesTargetByCodeInsertUpdate(product, options);
                    IsProcess = true;
                }
            }
            List<EmployeesTargetSettingCounterTarget> list1 = new List<EmployeesTargetSettingCounterTarget>();
            DataTable dt3 = new DataTable();
            if (options.EmpCodeList != null)
            {
                foreach (var item in options.EmpCodeList.Split('|'))
                {
                    if (TempData["EmployeesCounterTargetList" + item] != null)
                    {
                        list1 = (List<EmployeesTargetSettingCounterTarget>)TempData["EmployeesCounterTargetList" + item];
                        foreach (var data in list1)
                        {
                            dt3 = objemployee.EmployeesCounterTargetInsertUpdate(data.Shop_Code, data.OrderValue, data.CollectionValue, data.EmployeeCode, options.EmployeeTargetSettingID, 0, 0, 0, options.SettingMonth, options.SettingYear);
                        }
                    }
                }
            }

            if (IsProcess)
            {
                if (Success)
                {
                    ViewData["BatchUpdate"] = "Update";
                    ViewBag.Success = "Data saved!";
                }
                else
                {
                    ViewData["BatchUpdate"] = null;
                    ViewBag.Success = "Please try again later!";
                }
            }
            else
            {
                ViewData["BatchUpdate"] = null;
                ViewBag.Success = "Set target should be bellow or equals to your target value!";
            }

            DataTable dt = new DataTable();
            List<EmployeesTargetSetting> list = new List<EmployeesTargetSetting>();
            //EmployeesTargetSetting obj = new EmployeesTargetSetting();
            try
            {
                string userid = Session["userid"].ToString();
                dt = objemployee.GetEmployeeList(Convert.ToInt32(userid), options.EmpTypeID, options.CounterType, options.state, Convert.ToString(TempData["Designation"]), options.SettingMonth, options.SettingYear, "GetList");
                TempData.Keep();
                ViewBag.CounterType = options.CounterType;
                if (dt != null && dt.Rows.Count > 0)
                {
                    EmployeesTargetSetting data = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        data = new EmployeesTargetSetting();
                        data.user_id = Convert.ToInt32(row["user_id"]);
                        data.EmployeeCode = Convert.ToString(row["EmployeeCode"]);
                        data.Employeename = Convert.ToString(row["Employeename"]);
                        //data.state = Convert.ToString(row["state"]);
                        data.ContactNo = Convert.ToString(row["ContactNo"]);
                        data.Designation = Convert.ToString(row["Designation"]);
                        //data.Supervisor = Convert.ToString(row["Supervisor"]);
                        // data.SettingMonthYear = Convert.ToString(row["SettingMonthYear"]);
                        data.OrderValue = Convert.ToInt32(row["OrderValue"]);
                        data.NewCounter = Convert.ToInt32(row["NewCounter"]);
                        data.Collection = Convert.ToInt32(row["Collection"]);
                        data.Revisit = Convert.ToInt32(row["Revisit"]);
                        data.CreatedDate = Convert.ToDateTime(row["CreatedDate"]);
                        data.ModifiedDate = Convert.ToDateTime(row["ModifiedDate"]);

                        data.EmpTypeName = Convert.ToString(row["EmpTypeName"]);
                        data.CounterTypeName = Convert.ToString(row["CounterTypeName"]);
                        list.Add(data);
                    }
                }

                //dt = new DataTable();
                //dt = objemployee.GetEmployeeList(Convert.ToInt32(userid), 0, 0, "", "", options.SettingMonth, options.SettingYear, "GetByID");

                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    foreach (DataRow row in dt.Rows)
                //    {
                //        obj.EmployeeTargetSettingID = Convert.ToInt32(row["EmployeeTargetSettingID"]);
                //        obj.EmployeeCode = Convert.ToString(row["EmployeeCode"]);
                //        obj.OrderValue = Convert.ToDecimal(row["OrderValue"]);
                //        obj.NewCounter = Convert.ToInt32(row["NewCounter"]);
                //        obj.Collection = Convert.ToDecimal(row["Collection"]);
                //        obj.Revisit = Convert.ToInt32(row["Revisit"]);
                //    }
                //}
            }
            catch (Exception ex)
            {

            }
            TempData["EmployeesTargetList"] = dt;
            TempData["EmployeesTargetListDataTable"] = dt;
            TempData["EmployeesTargetListModel"] = list;

            TempData.Keep();
            //ViewBag.Data = obj;
            return PartialView("_EmployeesTargetSettingList", list);
        }

        //[ValidateInput(false)]
        //public ActionResult GetEmployeesTargetList(GridViewEditingMode editMode, string State, string Designation = null)
        //{
        //    ViewBag.EditMode = editMode;
        //    List<EmployeesTargetSetting> list = new List<EmployeesTargetSetting>();
        //    try
        //    {
        //        string userid = Session["userid"].ToString();
        //        DataTable dt = objemployee.GetEmployeeList(Convert.ToInt32(userid), State, Designation);
        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            EmployeesTargetSetting data = null;
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                data = new EmployeesTargetSetting();
        //                data.user_id = Convert.ToInt32(row["user_id"]);
        //                data.EmployeeCode = Convert.ToString(row["EmployeeCode"]);
        //                data.Employeename = Convert.ToString(row["Employeename"]);
        //                data.state = Convert.ToString(row["state"]);
        //                data.ContactNo = Convert.ToString(row["ContactNo"]);
        //                data.Designation = Convert.ToString(row["Designation"]);
        //                data.Supervisor = Convert.ToString(row["Supervisor"]);
        //                list.Add(data);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return PartialView("_EmployeesTargetSettingList", list);
        //}

        //[HttpPost, ValidateInput(false)]
        //public ActionResult EditModesAddNewPartial(EmployeesTargetSetting product, GridViewEditingMode editMode)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //SafeExecute(() => NorthwindDataProvider.InsertProduct(product));
        //    }
        //    else
        //        ViewData["EditError"] = "Please, correct all errors.";
        //    return null; // GetEmployeesTargetList(editMode);
        //}

        public ActionResult GetEmployeesTargetByCode(String employeecode, String settingtype, String employeename)
        {
            EmployeesTargetSetting data = new Models.EmployeesTargetSetting();
            data.EmployeeCode = employeecode;
            //data.SettingType = settingtype;
            data.Employeename = employeename;
            data.BrandList = GetProductBrandList();
            data.TargetTypeList = GetTargetTypeList(settingtype);
            data.CategoryList = GetProductCategoryList();
            data.ItemsList = GetProductItemsList();
            //return PartialView("_EmployeesTargetByCode", data);
            return PartialView("_EmployeesTargetSetByCode");
        }

        public ActionResult GetEmployeesCounterWiseTargetByCode(String employeecode, Int32 employeetargetsettingid, Int32 CounterType)
        {
            EmployeesTargetSettingCounterTarget data = new EmployeesTargetSettingCounterTarget();
            data.EmployeeCode = employeecode;
            data.FKEmployeeTargetSettingID = employeetargetsettingid;
            List<ShopPP> list = new List<ShopPP>();
            ViewBag.CounterType = CounterType;
            try
            {
                DataTable dt = objemployee.GetShopPPList(CounterType, "GetGridRecord");
                if (dt != null && dt.Rows.Count > 0)
                {

                    ShopPP obj = new ShopPP();
                    foreach (DataRow row in dt.Rows)
                    {
                        obj = new ShopPP();
                        obj.Shop_Code = Convert.ToString(row["Shop_Code"]);
                        obj.Shop_Name = Convert.ToString(row["Shop_Name"]);
                        list.Add(obj);
                    }
                }
                data.ShopPPList = list;
            }
            catch { }
            return PartialView("_EmployeesCounterWiseTargetSetByCode", data);
        }



        public ActionResult GetEmployeesCounterWiseTargetListByCode(String employeecode, Int32 employeetargetsettingid)
        {
            List<EmployeesTargetSettingCounterTarget> list = new List<EmployeesTargetSettingCounterTarget>();
            EmployeesTargetSettingCounterTarget data = null;
            try
            {
                DataTable dt = objemployee.GetEmployeesCounterWiseTargetByCode(employeecode, employeetargetsettingid);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        data = new Models.EmployeesTargetSettingCounterTarget();
                        data.EmployeesCounterTargetID = Convert.ToInt32(row["EmployeesCounterTargetID"]);
                        data.EmployeeCode = Convert.ToString(row["EmployeeCode"]);
                        data.FKEmployeeTargetSettingID = Convert.ToInt32(row["FKEmployeeTargetSettingID"]);
                        data.Shop_Code = Convert.ToString(row["Shop_Code"]);
                        data.OrderValue = Convert.ToDecimal(row["OrderValue"]);
                        data.CollectionValue = Convert.ToDecimal(row["CollectionValue"]);

                        list.Add(data);
                    }
                }
            }
            catch { }
            return PartialView("_EmployeesCounterWiseTargetSetByCode", list);
        }

        //public ActionResult GetEmployeesTargetListByCode(String employeecode, String settingtype)
        //{
        //    List<EmployeesTargetSetting> list = new List<EmployeesTargetSetting>();
        //    EmployeesTargetSetting data = null;
        //    try
        //    {
        //        DataTable dt = objemployee.GetEmployeesTargetByCode(employeecode, settingtype);
        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                data = new Models.EmployeesTargetSetting();
        //                data.EmployeeTargetSettingID = Convert.ToInt32(row["EmployeeTargetSettingID"]);
        //                data.EmployeeCode = Convert.ToString(row["EmployeeCode"]);
        //                data.SettingYear = Convert.ToString(row["SettingYear"]);
        //                data.Brand = Convert.ToString(row["Brand"]);

        //                data.Category = Convert.ToString(row["Category"]);
        //                data.Items = Convert.ToString(row["Items"]);
        //                data.Basis = Convert.ToString(row["Basis"]);

        //                data.ApplicableFrom = Convert.ToDateTime(row["ApplicableFrom"]);
        //                data.ValidUpto = Convert.ToDateTime(row["ValidUpto"]);
        //                if (settingtype == "Visits")
        //                {
        //                    if (!DBNull.Value.Equals(row["New Visit"]))
        //                    {
        //                        data.NewVisit = Convert.ToDecimal(row["New Visit"]);
        //                    }
        //                    if (! DBNull.Value.Equals(row["Re-visit"])){
        //                    data.Revisit = Convert.ToInt32(row["Re-visit"]);
        //                    }
        //                }
        //                if (settingtype == "Sales Order Value")
        //                {
        //                    if (!DBNull.Value.Equals(row["Sales Order Value"]))
        //                    {
        //                        data.SalesOrderValue = Convert.ToDecimal(row["Sales Order Value"]);
        //                    }
        //                }
        //                if (settingtype == "Collection")
        //                {
        //                    if (!DBNull.Value.Equals(row["Collection"]))
        //                    {
        //                        data.Collection = Convert.ToDecimal(row["Collection"]);
        //                    }
        //                }
        //                //data.SettingType = Convert.ToString(row["SettingType"]);
        //                //data.Value = Convert.ToString(row["Value"]);
        //                list.Add(data);
        //            }
        //        }
        //    }
        //    catch { }
        //    ViewBag.SettingType = settingtype;
        //    return PartialView("_EmployeesTargetListByCode", list);
        //}

        public List<TargetType> GetTargetTypeList(String settingtype)
        {
            List<TargetType> list = new List<TargetType>();
            try
            {
                DataTable dt = objemployee.GetVisitTypeList(settingtype);
                if (dt != null && dt.Rows.Count > 0)
                {
                    TargetType dataobj = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        dataobj = new TargetType();
                        dataobj.TypeName = Convert.ToString(row["TypeName"]);
                        dataobj.EmployeesTargetSettingTypeID = Convert.ToInt32(row["EmployeesTargetSettingTypeID"]);
                        list.Add(dataobj);
                    }
                    list = list.Distinct().ToList();
                }
            }
            catch { }
            return list;
        }

        public List<ProductBrand> GetProductBrandList()
        {
            List<ProductBrand> list = new List<ProductBrand>();
            try
            {
                DataTable dt = objemployee.GetProductBrandList();
                if (dt != null && dt.Rows.Count > 0)
                {
                    ProductBrand dataobj = null;
                    dataobj = new ProductBrand();
                    dataobj.BrandName = "Select";
                    dataobj.BrandID = 0;
                    list.Add(dataobj);
                    foreach (DataRow row in dt.Rows)
                    {
                        dataobj = new ProductBrand();
                        dataobj.BrandName = Convert.ToString(row["Brand_Name"]);
                        dataobj.BrandID = Convert.ToInt32(row["Brand_Id"]);
                        list.Add(dataobj);
                    }
                    list = list.Distinct().ToList();
                }
            }
            catch { }
            return list;
        }

        public List<ProductCategory> GetProductCategoryList()
        {
            List<ProductCategory> list = new List<ProductCategory>();
            try
            {
                DataTable dt = objemployee.GetProductCategoryList();
                if (dt != null && dt.Rows.Count > 0)
                {
                    ProductCategory dataobj = null;
                    dataobj = new ProductCategory();
                    dataobj.CategoryName = "Select";
                    dataobj.CategoryID = 0;
                    list.Add(dataobj);
                    foreach (DataRow row in dt.Rows)
                    {
                        dataobj = new ProductCategory();
                        dataobj.CategoryName = Convert.ToString(row["ProductClass_Name"]);
                        dataobj.CategoryID = Convert.ToInt32(row["ProductClass_ID"]);
                        list.Add(dataobj);
                    }
                }
            }
            catch { }
            return list;
        }

        public List<ProductItems> GetProductItemsList()
        {
            List<ProductItems> list = new List<ProductItems>();
            try
            {
                ProductItems dataobj = null;
                dataobj = new ProductItems();
                dataobj.ProductName = "Select";
                dataobj.ProductID = 0;
                list.Add(dataobj);
            }
            catch { }
            return list;
        }

        public List<EmployeeState> GetEmployeeStateList(String Action)
        {
            List<EmployeeState> list = new List<EmployeeState>();
            try
            {
                string userid = Session["userid"].ToString();
                DataTable dt = objemployee.GetEmployeeStateList(Convert.ToInt32(userid), Action);
                if (dt != null && dt.Rows.Count > 0)
                {
                    EmployeeState dataobj = null;
                    if (Action != "GetUserState")
                    {
                        dataobj = new EmployeeState();
                        dataobj.StateName = "Select";
                        list.Add(dataobj);
                    }
                    foreach (DataRow row in dt.Rows)
                    {
                        dataobj = new EmployeeState();
                        dataobj.StateName = Convert.ToString(row["state"]);
                        list.Add(dataobj);
                    }
                }
            }
            catch { }
            return list;
        }

        public List<EmployeeDesignation> GetEmployeeDesignationList(String Action)
        {
            List<EmployeeDesignation> list = new List<EmployeeDesignation>();
            try
            {
                string userid = Session["userid"].ToString();
                DataTable dt = objemployee.GetEmployeeDesignationList(Convert.ToInt32(userid), Action);
                if (dt != null && dt.Rows.Count > 0)
                {
                    EmployeeDesignation dataobj = null;
                    if (Action != "GetEMPDesig")
                    {
                        dataobj = new EmployeeDesignation();
                        dataobj.DesignationName = "Select";
                        list.Add(dataobj);
                    }
                    foreach (DataRow row in dt.Rows)
                    {
                        dataobj = new EmployeeDesignation();
                        dataobj.DesignationName = Convert.ToString(row["Designation"]);
                        list.Add(dataobj);
                    }
                }
            }
            catch { }
            return list;
        }

        public List<EmployeeType> GetEmployeeTypeList(Int32 Type)
        {
            List<EmployeeType> list = new List<EmployeeType>();
            try
            {
                DataTable dt = objemployee.GetEmployeeTypeList();
                if (dt != null && dt.Rows.Count > 0)
                {
                    EmployeeType dataobj = null;
                    dataobj = new EmployeeType();
                    dataobj.TypeName = "Select";
                    dataobj.EmployeesTargetSettingEmpTypeID = "0";
                    dataobj.Type = 0;
                    list.Add(dataobj);
                    foreach (DataRow row in dt.Rows)
                    {
                        dataobj = new EmployeeType();
                        dataobj.TypeName = Convert.ToString(row["TypeName"]);
                        dataobj.EmployeesTargetSettingEmpTypeID = Convert.ToString(row["EmployeesTargetSettingEmpTypeID"]);
                        dataobj.Type = Convert.ToInt32(row["Type"]);
                        list.Add(dataobj);
                    }
                    if (list.Count > 0 && Type == 0)
                    {
                        list = list.Where(x => x.Type == 0).ToList();
                    }
                    if (list.Count > 0 && Type == 1)
                    {
                        list = list.Where(x => x.Type == 1).ToList();
                    }
                }
            }
            catch { }
            return list;
        }

        public Boolean EmployeesTargetByCodeInsertUpdate(EmployeesTargetSetting obj, EmployeesTargetSetting obj2)
        {
            Boolean Success = false;
            List<EmployeesTargetSettingCounterTarget> list = new List<Models.EmployeesTargetSettingCounterTarget>();
            try
            {
                DataTable dt = objemployee.EmployeesTargetByCodeInsertUpdate(obj.EmployeeTargetSettingID, obj2.EmpTypeID, obj2.CounterType, obj.EmployeeCode, obj2.SettingMonth, obj2.SettingYear, obj.OrderValue, obj.NewCounter, obj.Collection, obj.Revisit);



                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Success = Convert.ToBoolean(row["Success"]);
                    }
                }
            }
            catch { }
            return Success;
        }

        //public JsonResult EmployeesTargetByCodeInsertUpdate(EmployeesTargetSetting obj)
        //{
        //    Boolean Success = false;
        //    String dataxml = String.Empty;
        //    try
        //    {
        //        String[] Type = obj.xml.Split('|');

        //        for (int i = 0; i < Type.Length; i++)
        //        {
        //            if (!String.IsNullOrEmpty(Type[i].Trim()))
        //            {
        //                String[] data = Type[i].Split(':');
        //                if (data.Length > 1)
        //                {
        //                    if (!String.IsNullOrEmpty(data[0].Trim()))
        //                    {
        //                        String FKEmployeesTargetSettingID = "<FKEmployeesTargetSettingID>" + data[0].Trim() + "</FKEmployeesTargetSettingID>";
        //                        String Value = "<Value>" + data[1].Trim() + "</Value>";
        //                        String xml = "<Data>" + FKEmployeesTargetSettingID + Value + "</Data>";
        //                        dataxml = dataxml + xml;
        //                    }
        //                }
        //            }
        //        }
        //        if (!String.IsNullOrEmpty(dataxml))
        //        {
        //            dataxml = "<SettingDetails>" + dataxml + "</SettingDetails>";
        //        }
        //        obj.ApplicableFrom = DateTime.ParseExact(obj.ApplicableFromTxt, "dd-MM-yyyy", CultureInfo.InvariantCulture);
        //        obj.ValidUpto = DateTime.ParseExact(obj.ValidUptoTxt, "dd-MM-yyyy", CultureInfo.InvariantCulture);

        //        DataTable dt = objemployee.EmployeesTargetByCodeInsertUpdate(obj.EmployeeTargetSettingID, obj.EmployeeCode, obj.SettingYear, obj.Brand, obj.Category, obj.Items, obj.Basis, obj.ApplicableFrom, obj.ValidUpto, obj.SettingType, obj.VisitType, dataxml);
        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                Success = Convert.ToBoolean(row["Success"]);
        //            }
        //        }
        //    }
        //    catch { }
        //    return Json(Success);
        //}

        //public JsonResult GetEmployeesTargetByID(Int32 employeetargetsettingid, String settingtype)
        //{
        //    Boolean Success = false;
        //    EmployeesTargetSetting data = null;
        //    try
        //    {
        //        DataTable dt = objemployee.GetEmployeesTargetByID(employeetargetsettingid, settingtype);
        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                data = new Models.EmployeesTargetSetting();
        //                data.EmployeeTargetSettingID = Convert.ToInt32(row["EmployeeTargetSettingID"]);
        //                data.EmployeeCode = Convert.ToString(row["EmployeeCode"]);
        //                data.SettingYear = Convert.ToString(row["SettingYear"]);
        //                data.Brand = Convert.ToString(row["Brand"]);

        //                data.Category = Convert.ToString(row["Category"]);
        //                data.Items = Convert.ToString(row["Items"]);
        //                data.Basis = Convert.ToString(row["Basis"]);

        //                data.ApplicableFrom = Convert.ToDateTime(row["ApplicableFrom"]);
        //                data.ValidUpto = Convert.ToDateTime(row["ValidUpto"]);

        //                data.ApplicableFromTxt = Convert.ToString(row["ApplicableFrom"]);
        //                data.ValidUptoTxt = Convert.ToString(row["ValidUpto"]);
        //                if (settingtype == "Visits")
        //                {
        //                    data.NewVisit = Convert.ToDecimal(row["New Visit"]);
        //                    data.Revisit = Convert.ToInt32(row["Re-visit"]);
        //                }
        //                if (settingtype == "Sales Order Value")
        //                {
        //                    data.SalesOrderValue = Convert.ToDecimal(row["Sales Order Value"]);
        //                }
        //                if (settingtype == "Collection")
        //                {
        //                    data.Collection = Convert.ToDecimal(row["Collection"]);
        //                }

        //                data.SettingType = settingtype;
        //               // data.Value = Convert.ToString(row["Value"]);
        //            }
        //        }
        //    }
        //    catch { }
        //    return Json(data);
        //}

        public JsonResult EmployeesTargetRemove(Int32 employeetargetsettingid)
        {
            Boolean Success = false;
            try
            {
                DataTable dt = objemployee.EmployeesTargetRemove(employeetargetsettingid);
                Success = true;
            }
            catch { }
            return Json(Success);
        }

        public JsonResult CounterTargetInsertUpdate(String EmpPPType, Decimal OrderValue, Decimal CollectionValue, String employeecode, Int32 employeetargetsettingid, Int32 employeescountertargetid, Int32 FKEmployeesTargetSettingEmpTypeID, Int32 fkemployeescountertype, Int32 SettingMonth, Int32 SettingYear)
        {
            List<EmployeesTargetSetting> list = new List<Models.EmployeesTargetSetting>();
            EmployeesTargetSettingCounterTarget obj = new EmployeesTargetSettingCounterTarget();
            String Message = String.Empty;
            Int32 Insertemployeetargetsettingid = 0;

            try
            {
                DataTable dt = objemployee.EmployeesCounterTargetInsertUpdate(EmpPPType, OrderValue, CollectionValue, employeecode, employeetargetsettingid, employeescountertargetid, FKEmployeesTargetSettingEmpTypeID, fkemployeescountertype, SettingMonth, SettingYear);
                Message = Convert.ToString(dt.Rows[0]["Message"]);
                Insertemployeetargetsettingid = Convert.ToInt32(dt.Rows[0]["EMPLOYEETARGETSETTINGID"]);
                obj.NewOrderValue = Convert.ToInt32(dt.Rows[0]["NewOrder"]);
                obj.NewCollectionValue = Convert.ToInt32(dt.Rows[0]["NewCollection"]);
                obj.OrderUpdate = Convert.ToBoolean(dt.Rows[0]["OrderUpdate"]);
                obj.CollectionUpdate = Convert.ToBoolean(dt.Rows[0]["CollectionUpdate"]);
                obj.Message = Message;
                obj.FKEmployeeTargetSettingID = Insertemployeetargetsettingid;
                TempData["EmployeesTargetListDataTable"] = dt;
                list = (List<EmployeesTargetSetting>)TempData["EmployeesTargetList"];
                if (obj.OrderUpdate)
                {
                    (from p in list
                     where p.EmployeeTargetSettingID == Insertemployeetargetsettingid
                     select p).ToList().ForEach(x => x.OrderValue = obj.NewOrderValue);
                }
                if (obj.CollectionUpdate)
                {
                    (from p in list
                     where p.EmployeeTargetSettingID == Insertemployeetargetsettingid
                     select p).ToList().ForEach(x => x.Collection = obj.NewCollectionValue);
                }

                TempData["EmployeesTargetList"] = list;


            }
            catch { }
            TempData.Keep();
            return Json(obj);
        }

        public JsonResult CounterTargetAdd(String EmpPPType, Decimal OrderValue, Decimal CollectionValue, String employeecode, String EmpPPTypeText)
        {
            List<EmployeesTargetSettingCounterTarget> list = new List<Models.EmployeesTargetSettingCounterTarget>();
            EmployeesTargetSettingCounterTarget obj = new EmployeesTargetSettingCounterTarget();
            String Message = String.Empty;
            Int32 Insertemployeetargetsettingid = 0;

            try
            {
                //DataTable dt = objemployee.EmployeesCounterTargetInsertUpdate(EmpPPType, OrderValue, CollectionValue, employeecode, employeetargetsettingid, employeescountertargetid, FKEmployeesTargetSettingEmpTypeID, fkemployeescountertype, SettingMonth, SettingYear);
                //Message = Convert.ToString(dt.Rows[0]["Message"]);
                //Insertemployeetargetsettingid = Convert.ToInt32(dt.Rows[0]["EMPLOYEETARGETSETTINGID"]);
                //obj.NewOrderValue = Convert.ToDecimal(dt.Rows[0]["NewOrder"]);
                //obj.NewCollectionValue = Convert.ToDecimal(dt.Rows[0]["NewCollection"]);
                //obj.OrderUpdate = Convert.ToBoolean(dt.Rows[0]["OrderUpdate"]);
                //obj.CollectionUpdate = Convert.ToBoolean(dt.Rows[0]["CollectionUpdate"]);
                obj.Message = "Data Added";
                //obj.FKEmployeeTargetSettingID = Insertemployeetargetsettingid;
                if (TempData["EmployeesCounterTargetList" + employeecode] != null)
                {
                    list = (List<EmployeesTargetSettingCounterTarget>)TempData["EmployeesCounterTargetList" + employeecode];
                }

                if (list.Where(x => x.Shop_Code == EmpPPType).Count() > 0)
                {
                    (from p in list
                     where p.Shop_Code == EmpPPType
                     select p).ToList().ForEach(x => x.OrderValue = OrderValue);

                    (from p in list
                     where p.Shop_Code == EmpPPType
                     select p).ToList().ForEach(x => x.CollectionValue = CollectionValue);
                }
                else
                {
                    obj.CollectionValue = CollectionValue;
                    obj.OrderValue = OrderValue;
                    obj.Shop_Code = EmpPPType;
                    obj.NewOrderValue = list.Sum(x => Convert.ToInt32(x.OrderValue));
                    obj.NewCollectionValue = list.Sum(x => Convert.ToInt32(x.CollectionValue));
                    obj.Shop_Name = EmpPPTypeText;
                    obj.EmployeeCode = employeecode;
                    list.Add(obj);
                }
                TempData["EmployeesCounterTargetList" + employeecode] = list;
                TempData.Keep();
            }
            catch { }
            return Json(obj);
        }

        public JsonResult EmployeesCounterWiseTargetDelete(String employeecode, String shop_code, String type)
        {
            Boolean Success = false;
            DataTable dt = new DataTable();
            List<EmployeesTargetSettingCounterTarget> list = new List<Models.EmployeesTargetSettingCounterTarget>();
            try
            {
                if (type == "EmployeesTargetSettingCounterTarget")
                {
                    if (TempData["EmployeesCounterTargetList" + employeecode] != null)
                    {
                        list = (List<EmployeesTargetSettingCounterTarget>)TempData["EmployeesCounterTargetList" + employeecode];

                        list = list.Where(x => x.Shop_Code != shop_code).ToList();
                        TempData["EmployeesCounterTargetList" + employeecode] = list;
                    }

                    Success = true;
                }

            }
            catch { }
            TempData.Keep();
            return Json(Success);
        }

        public JsonResult EmployeesCounterWiseTargetRemove(String employeecode, Int32 employeetargetsettingid, String type)
        {
            Boolean Success = false;
            DataTable dt = new DataTable();
            try
            {
                if (type == "EmployeesTargetSetting")
                {
                    dt = objemployee.EmployeesCounterWiseTargetRemove(employeecode, employeetargetsettingid, "FTS_EmployeesTargetSetting");
                    Success = Convert.ToBoolean(dt.Rows[0]["Success"]);
                }
                if (type == "EmployeesTargetSettingCounterTarget")
                {
                    dt = objemployee.EmployeesCounterWiseTargetRemove(employeecode, employeetargetsettingid, "FTS_EmployeesTargetSettingCounterTarget");
                    Success = Convert.ToBoolean(dt.Rows[0]["Success"]);
                }

            }
            catch { }
            return Json(Success);
        }

        public JsonResult EmployeesTargetItemsList(Int32 Category)
        {
            List<ProductItems> list = new List<ProductItems>();
            try
            {
                DataTable dt = objemployee.EmployeesTargetItemsList(Category);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ProductItems dataobj = null;
                    dataobj = new ProductItems();
                    dataobj.ProductName = "Select";
                    dataobj.ProductID = 0;
                    list.Add(dataobj);
                    foreach (DataRow row in dt.Rows)
                    {
                        dataobj = new ProductItems();
                        dataobj.ProductName = Convert.ToString(row["sProducts_Name"]);
                        dataobj.ProductID = Convert.ToInt32(row["sProducts_ID"]);
                        list.Add(dataobj);
                    }
                }
            }
            catch { }
            return Json(list);
        }

        public JsonResult GetEmployeesCounterTargetByID(Int32 EmployeesCounterTargetID)
        {
            EmployeesTargetSettingCounterTarget data = new EmployeesTargetSettingCounterTarget();
            DataTable dt = new DataTable();
            try
            {
                dt = objemployee.GetEmployeesCounterTargetList("", 0, EmployeesCounterTargetID);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        data.EmployeesCounterTargetID = Convert.ToInt32(row["EmployeesCounterTargetID"]);
                        data.EmployeeCode = Convert.ToString(row["EmployeeCode"]);
                        data.FKEmployeeTargetSettingID = Convert.ToInt32(row["FKEmployeeTargetSettingID"]);
                        data.Shop_Code = Convert.ToString(row["Shop_Code"]);
                        data.OrderValue = Convert.ToDecimal(row["OrderValue"]);
                        data.CollectionValue = Convert.ToDecimal(row["CollectionValue"]);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Json(data);
        }

        //public JsonResult GetEmployeeTargetDateRange(String EmployeeCode, Int32 TypeID)
        //{
        //    EmployeesTargetSetting data = null;
        //    try
        //    {
        //        DataTable dt = objemployee.GetEmployeeTargetDateRange(EmployeeCode, TypeID);
        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                data = new Models.EmployeesTargetSetting();
        //                data.ApplicableFrom = Convert.ToDateTime(row["ApplicableFrom"]);
        //                data.ValidUpto = Convert.ToDateTime(row["ValidUpto"]);

        //                data.ApplicableFromTxt = Convert.ToString(row["ApplicableFrom"]);
        //                data.ValidUptoTxt = Convert.ToString(row["ValidUpto"]);
        //            }
        //        }

        //    }
        //    catch { }
        //    return Json(data);
        //}

        public ActionResult ExportEmployeesTargetList(int type)
        {
            ViewData["EmployeesTargetList"] = TempData["EmployeesTargetListDataTable"];

            TempData.Keep();

            if (ViewData["EmployeesTargetList"] != null)
            {

                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetEmployeesTargetGridView(ViewData["EmployeesTargetList"]), ViewData["EmployeesTargetList"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetEmployeesTargetGridView(ViewData["EmployeesTargetList"]), ViewData["EmployeesTargetList"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetEmployeesTargetGridView(ViewData["EmployeesTargetList"]), ViewData["EmployeesTargetList"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetEmployeesTargetGridView(ViewData["EmployeesTargetList"]), ViewData["EmployeesTargetList"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetEmployeesTargetGridView(ViewData["EmployeesTargetList"]), ViewData["EmployeesTargetList"]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetEmployeesTargetGridView(object datatable)
        {
            //List<EmployeesTargetSetting> obj = (List<EmployeesTargetSetting>)datatablelist;
            //ListtoDataTable lsttodt = new ListtoDataTable();
            //DataTable datatable = ConvertListToDataTable(obj); 
            var settings = new GridViewSettings();
            settings.Name = "EmployeesTargetList";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "EmployeesTargetList";
            //String ID = Convert.ToString(TempData["EmployeesTargetListDataTable"]);
            TempData.Keep();
            DataTable dt = (DataTable)datatable;

            foreach (System.Data.DataColumn datacolumn in dt.Columns)
            {
                if (datacolumn.ColumnName == "ContactNo" || datacolumn.ColumnName == "Employeename"
                    || datacolumn.ColumnName == "NewCounter" || datacolumn.ColumnName == "Collection" || datacolumn.ColumnName == "Revisit" || datacolumn.ColumnName == "OrderValue"
                    || datacolumn.ColumnName == "CreatedDate" || datacolumn.ColumnName == "ModifiedDate" || datacolumn.ColumnName == "EmpTypeName" || datacolumn.ColumnName == "CounterTypeName" || datacolumn.ColumnName == "Designation")
                {
                    settings.Columns.Add(column =>
                    {
                        if (datacolumn.ColumnName == "ContactNo")
                        {
                            column.Caption = "Login ID";
                        }
                        else if (datacolumn.ColumnName == "Revisit")
                        {
                            column.Caption = "Re-visit";
                        }
                        else if (datacolumn.ColumnName == "Employeename")
                        {
                            column.Caption = "Name";
                        }
                        else if (datacolumn.ColumnName == "CreatedDate")
                        {
                            column.Caption = "Entry Date";
                        }
                        else if (datacolumn.ColumnName == "ModifiedDate")
                        {
                            column.Caption = "Last Modified Date";
                        }
                        else if (datacolumn.ColumnName == "EmpTypeName")
                        {
                            column.Caption = "Target Type";
                        }
                        else if (datacolumn.ColumnName == "CounterTypeName")
                        {
                            column.Caption = "Customer Type";
                        }
                        else if (datacolumn.ColumnName == "Collection")
                        {
                            column.Caption = "Target Collection";
                        }
                        else if (datacolumn.ColumnName == "OrderValue")
                        {
                            column.Caption = "Target Value";
                        }
                        else
                        {
                            column.Caption = datacolumn.ColumnName;
                        }
                        column.FieldName = datacolumn.ColumnName;
                        if (datacolumn.DataType.FullName == "System.Decimal")
                        {
                            column.PropertiesEdit.DisplayFormatString = "0.00";
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

        public ActionResult ExportEmployeesTargetLogList(int type)
        {
            ViewData["EmployeesTargetSettingImportLog"] = TempData["EmployeesTargetSettingImportLog"];

            TempData.Keep();

            if (ViewData["EmployeesTargetSettingImportLog"] != null)
            {

                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetEmployeesTargetLogGridView(ViewData["EmployeesTargetSettingImportLog"]), ViewData["EmployeesTargetSettingImportLog"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetEmployeesTargetLogGridView(ViewData["EmployeesTargetSettingImportLog"]), ViewData["EmployeesTargetSettingImportLog"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXlsx(GetEmployeesTargetLogGridView(ViewData["EmployeesTargetSettingImportLog"]), ViewData["EmployeesTargetSettingImportLog"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetEmployeesTargetLogGridView(ViewData["EmployeesTargetSettingImportLog"]), ViewData["EmployeesTargetSettingImportLog"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetEmployeesTargetLogGridView(ViewData["EmployeesTargetSettingImportLog"]), ViewData["EmployeesTargetSettingImportLog"]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetEmployeesTargetLogGridView(object datatable)
        {
            //List<EmployeesTargetSetting> obj = (List<EmployeesTargetSetting>)datatablelist;
            //ListtoDataTable lsttodt = new ListtoDataTable();
            //DataTable datatable = ConvertListToDataTable(obj); 
            var settings = new GridViewSettings();
            settings.Name = "EmployeesTargetSettingImportLog";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "EmployeesTargetSettingImportLog";
            //String ID = Convert.ToString(TempData["EmployeesTargetListDataTable"]);
            TempData.Keep();
            DataTable dt = (DataTable)datatable;

            foreach (System.Data.DataColumn datacolumn in dt.Columns)
            {
                if (datacolumn.ColumnName == "Mobile" || datacolumn.ColumnName == "Name"
                    || datacolumn.ColumnName == "Supervisor_Assigned" || datacolumn.ColumnName == "NewCounter" || datacolumn.ColumnName == "Revisit" || datacolumn.ColumnName == "TargetValue"
                    || datacolumn.ColumnName == "TargetCollection" || datacolumn.ColumnName == "Status" || datacolumn.ColumnName == "Message" || datacolumn.ColumnName == "CreatedDate")
                {
                    settings.Columns.Add(column =>
                    {
                        if (datacolumn.ColumnName == "Mobile")
                        {
                            column.Caption = "Login ID/Mobile";
                        }
                        else if (datacolumn.ColumnName == "Name")
                        {
                            column.Caption = "Emp/PP/DD";
                        }
                        else if (datacolumn.ColumnName == "Supervisor_Assigned")
                        {
                            column.Caption = "Supervisor/Assigned";
                        }
                        else if (datacolumn.ColumnName == "NewCounter")
                        {
                            column.Caption = "New Customer";
                        }
                        else if (datacolumn.ColumnName == "Revisit")
                        {
                            column.Caption = "Re-visit";
                        }
                        else if (datacolumn.ColumnName == "TargetValue")
                        {
                            column.Caption = "Target Value";
                        }
                        else if (datacolumn.ColumnName == "TargetCollection")
                        {
                            column.Caption = "Target Collection";
                        }
                        else if (datacolumn.ColumnName == "Status")
                        {
                            column.Caption = "Status";
                        }
                        else if (datacolumn.ColumnName == "Message")
                        {
                            column.Caption = "Reason";
                        }
                        else if (datacolumn.ColumnName == "CreatedDate")
                        {
                            column.Caption = "Import Date & Time";
                            column.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy hh:mm tt";
                        }
                        else
                        {
                            column.Caption = datacolumn.ColumnName;
                        }

                        
                        column.FieldName = datacolumn.ColumnName;

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




        public JsonResult GetEmployeesListByStateDesignation(String State, String Designation)
        {
            List<EmployeesList> list = new List<EmployeesList>();
            DataTable dt = new DataTable();
            try
            {
                // Rev 1.0
                //dt = objemployee.GetEmployeesListByStateDesignation(State, Designation);
                dt = objemployee.GetEmployeesListByStateDesignation_Hier(State, Designation, Convert.ToString(Session["userid"]));
                // End of Rev 1.0
                if (dt != null && dt.Rows.Count > 0)
                {
                    EmployeesList data = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        data = new EmployeesList();
                        data.EmpName = Convert.ToString(row["EmpName"]);
                        data.EmpCode = Convert.ToString(row["EmpCode"]);
                        data.LoginID = Convert.ToString(row["LoginID"]);
                        data.UserID = Convert.ToInt32(row["UserID"]);
                        data.DisplayEmployee = data.EmpName + " (" + data.LoginID + ")";
                        list.Add(data);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Json(list);
        }

        public ActionResult GetEmployeesListByReportTo(string State, Int32 UserID = 0, Int32 EmpTypeID = 0, Int32 CounterType = 0, string Designation = null, Int32 SettingMonth = 0, Int32 SettingYear = 0)
        {
            List<EmployeesTargetSetting> list = new List<EmployeesTargetSetting>();
            //EmployeesTargetSetting obj = new EmployeesTargetSetting();
            DataTable dt = new DataTable();
            //DataTable dt2 = new DataTable();
            try
            {
                //string userid = Session["userid"].ToString();
                TempData["Designation"] = Designation;
                TempData.Keep();
                ViewBag.CounterType = CounterType;
                dt = objemployee.GetEmployeeList(Convert.ToInt32(UserID), EmpTypeID, CounterType, State, Designation, SettingMonth, SettingYear, "GetList");
                TempData["EmployeesTargetListDataTable"] = dt;
                if (dt != null && dt.Rows.Count > 0)
                {
                    EmployeesTargetSetting data = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        data = new EmployeesTargetSetting();
                        data.user_id = Convert.ToInt32(row["user_id"]);
                        data.EmployeeCode = Convert.ToString(row["EmployeeCode"]);
                        data.Employeename = Convert.ToString(row["Employeename"]);
                        data.stage = Convert.ToString(row["Stage"]);
                        //data.state = Convert.ToString(row["state"]);
                        data.ContactNo = Convert.ToString(row["ContactNo"]);
                        data.Designation = Convert.ToString(row["Designation"]);
                        //data.Supervisor = Convert.ToString(row["Supervisor"]);
                        //data.SettingMonthYear = Convert.ToString(row["SettingMonthYear"]);
                        data.OrderValue = Convert.ToInt32(row["OrderValue"]);
                        data.NewCounter = Convert.ToInt32(row["NewCounter"]);
                        data.Collection = Convert.ToInt32(row["Collection"]);
                        data.Revisit = Convert.ToInt32(row["Revisit"]);
                        data.CreatedDate = Convert.ToDateTime(row["CreatedDate"]);
                        data.ModifiedDate = Convert.ToDateTime(row["ModifiedDate"]);
                        data.EmployeeTargetSettingID = Convert.ToInt32(row["EmployeeTargetSettingID"]);

                        data.EmpTypeName = Convert.ToString(row["EmpTypeName"]);
                        data.CounterTypeName = Convert.ToString(row["CounterTypeName"]);
                        list.Add(data);
                    }
                }

                //dt2 = objemployee.GetEmployeeList(Convert.ToInt32(UserID), 0, 0, "", "", SettingMonth, SettingYear, "GetByID");

                //if (dt2 != null && dt2.Rows.Count > 0)
                //{
                //    foreach (DataRow row in dt2.Rows)
                //    {
                //        obj.EmployeeTargetSettingID = Convert.ToInt32(row["EmployeeTargetSettingID"]);
                //        obj.EmployeeCode = Convert.ToString(row["EmployeeCode"]);
                //        obj.OrderValue = Convert.ToDecimal(row["OrderValue"]);
                //        obj.NewCounter = Convert.ToInt32(row["NewCounter"]);
                //        obj.Collection = Convert.ToDecimal(row["Collection"]);
                //        obj.Revisit = Convert.ToInt32(row["Revisit"]);
                //    }
                //}

            }
            catch (Exception ex)
            {

            }
            TempData["EmployeesTargetList"] = dt;
            TempData["EmployeesTargetListModel"] = list;
            //TempData["GetEmployeesTargetByID"] = dt2;
            TempData.Keep();
            //ViewBag.Data = obj;
            return PartialView("_EmployeesListByReportTo", list);
        }

        public JsonResult GetEmployeesTargetByID(Int32 userid, Int32 month, Int32 year)
        {
            DataTable dt2 = new DataTable();
            EmployeesTargetSetting obj = new EmployeesTargetSetting();
            try
            {
                dt2 = objemployee.GetEmployeeList(Convert.ToInt32(userid), 0, 0, "", "", month, year, "GetByID");

                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    foreach (DataRow row in dt2.Rows)
                    {
                        obj.EmployeeTargetSettingID = Convert.ToInt32(row["EmployeeTargetSettingID"]);
                        obj.EmployeeCode = Convert.ToString(row["EmployeeCode"]);
                        obj.OrderValue = Convert.ToInt32(row["OrderValue"]);
                        obj.NewCounter = Convert.ToInt32(row["NewCounter"]);
                        obj.Collection = Convert.ToInt32(row["Collection"]);
                        obj.Revisit = Convert.ToInt32(row["Revisit"]);
                    }
                }
            }
            catch { }
            return Json(obj);
        }

        public JsonResult GetEmployeesFilterByCode(String empcodelist)
        {
            Boolean Success = false;
            List<EmployeesTargetSetting> dt2 = new List<EmployeesTargetSetting>();
            DataTable dt3 = new DataTable();
            DataTable selectedTable = new DataTable();

            try
            {
                List<EmployeesTargetSetting> dt = (List<EmployeesTargetSetting>)TempData["EmployeesTargetListModel"];
                dt3 = (DataTable)TempData["EmployeesTargetListDataTable"];
                selectedTable = dt3.Copy();
                selectedTable.Rows.Clear();
                string[] values = empcodelist.Split('|');
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = values[i].Trim();
                    EmployeesTargetSetting obj = new Models.EmployeesTargetSetting();
                    obj = dt.Where(x => x.EmployeeCode == values[i]).FirstOrDefault();
                    dt2.Add(obj);



                    selectedTable.ImportRow(dt3.AsEnumerable().Where(r => r.Field<string>("EmployeeCode") == values[i]).FirstOrDefault());

                }
                TempData["EmployeesTargetList"] = dt2;
                TempData["EmployeesTargetListDataTable"] = selectedTable;
                Success = true;
            }
            catch { }
            return Json(Success);
        }


    }

}
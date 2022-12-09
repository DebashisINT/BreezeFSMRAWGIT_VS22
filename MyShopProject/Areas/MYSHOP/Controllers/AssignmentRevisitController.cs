using BusinessLogicLayer.SalesmanTrack;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class AssignmentRevisitController : Controller
    {
        AssignmentRevisitBL objemployee = null;
        public AssignmentRevisitController()
        {
            objemployee = new AssignmentRevisitBL();
        }

        public ActionResult AssignmentRevisitIndex()
        {
            try
            {
                return View();
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
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



        public ActionResult GetEmployeesListTemplateByStateDesignation(string State, string Designation, string Employees, string month, string Year, string monthName)
        {
            string Employee = "";
            int i = 1;
            string[] words = Employees.Split(',');

            foreach (string item in words)
            {
                if (i > 1)
                    Employee = Employee + "," + item;
                else
                    Employee = item;
                i++;
            }

            string state = "";
            int k = 1;

            //if (model.StateId != null && model.StateId.Count > 0)
            //{
            //    foreach (string item in model.StateId)
            //    {
            //        if (k > 1)
            //            state = state + "," + item;
            //        else
            //            state = item;
            //        k++;
            //    }

            //}

            //string desig = "";
            //int j = 1;

            //if (model.desgid != null && model.desgid.Count > 0)
            //{
            //    foreach (string item in model.desgid)
            //    {
            //        if (j > 1)
            //            desig = desig + "," + item;
            //        else
            //            desig = item;
            //        j++;
            //    }

            //}

            DataTable dt = new DataTable();
            // DataTable dt2 = new DataTable();
            // List<EmployeesTargetSetting> list = new List<EmployeesTargetSetting>();
            try
            {
                dt = objemployee.GetReVisitList(Employee, month, Year, State, Designation);
                //string Employee, string MONTH, string YEAR, string stateID, string DESIGNID
            }
            catch { }


            return GridViewExtension.ExportToXlsx(GetEmployeesListTemplateByStateDesignationExcel(dt, monthName), dt);
        }

        private GridViewSettings GetEmployeesListTemplateByStateDesignationExcel(object datatable, string monthName)
        {
            var settings = new GridViewSettings();
            settings.Name = "EmployeesRevisit_" + monthName;
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "EmployeesRevisit_" + monthName;
            //String ID = Convert.ToString(TempData["EmployeesTargetListDataTable"]);
            TempData.Keep();
            DataTable dt = (DataTable)datatable;



            foreach (System.Data.DataColumn datacolumn in dt.Columns)
            {
                if (datacolumn.ColumnName == "FortheMonth" || datacolumn.ColumnName == "Enter_Date_for_Revisit" || datacolumn.ColumnName == "Employee_State"
                    || datacolumn.ColumnName == "Login_IDMobile" || datacolumn.ColumnName == "Employee_Name" || datacolumn.ColumnName == "Area_PIN" || datacolumn.ColumnName == "Shop_ID"
                    || datacolumn.ColumnName == "Assigned_ShopName" || datacolumn.ColumnName == "Contact_No" || datacolumn.ColumnName == "Address" || datacolumn.ColumnName == "State"
                    || datacolumn.ColumnName == "Type")
                {
                    settings.Columns.Add(column =>
                    {
                        if (datacolumn.ColumnName == "FortheMonth")
                        {
                            column.Caption = "For the Month";

                        }
                        else if (datacolumn.ColumnName == "Enter_Date_for_Revisit")
                        {
                            column.Caption = "Enter Date for Revisit(DD-MM-YYYY)";

                        }
                        else if (datacolumn.ColumnName == "Employee_State")
                        {
                            column.Caption = "Employee State";
                        }
                        else if (datacolumn.ColumnName == "Login_IDMobile")
                        {
                            column.Caption = "Login ID/Mobile";
                        }
                        else if (datacolumn.ColumnName == "Employee_Name")
                        {
                            column.Caption = "Employee Name";
                        }
                        else if (datacolumn.ColumnName == "Area_PIN")
                        {
                            column.Caption = "Area PIN";
                        }
                        else if (datacolumn.ColumnName == "Shop_ID")
                        {
                            column.Caption = "Shop ID";
                        }
                        else if (datacolumn.ColumnName == "Assigned_ShopName")
                        {
                            column.Caption = "Assigned Shop Name";
                        }
                        else if (datacolumn.ColumnName == "Contact_No")
                        {
                            column.Caption = "Contact No";
                        }
                        else if (datacolumn.ColumnName == "Address")
                        {
                            column.Caption = "Address";
                        }

                        else if (datacolumn.ColumnName == "State")
                        {
                            column.Caption = "State";
                        }
                        else if (datacolumn.ColumnName == "Type")
                        {
                            column.Caption = "Type";
                        }


                        column.FieldName = datacolumn.ColumnName;

                        if (datacolumn.DataType.FullName == "System.Decimal")
                        {
                            column.PropertiesEdit.DisplayFormatString = "0.00";
                        }

                        if (datacolumn.DataType.FullName == "System.DateTime")
                        {
                            column.PropertiesEdit.DisplayFormatString = "DD-MM-YYYY";
                        }

                        //DataColumn colTimeSpan = new DataColumn("Enter_Date_for_Revisit");
                        //colTimeSpan.DataType = System.Type.GetType("System.String");
                        // column.ColumnType=typeof(string);

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
                        //ReadExcel(fname, extension, file);
                    }
                    // Returns message that successfully uploaded  
                    return Json("Import Process Completed!");
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

        /// <summary>
        /// Given a cell name, parses the specified cell to get the column name.
        /// </summary>
        /// <param name="cellReference">Address of the cell (ie. B2)</param>
        /// <returns>Column Name (ie. B)</returns>
        public static string GetColumnName(string cellReference)
        {
            // Create a regular expression to match the column name portion of the cell name.
            Regex regex = new Regex("[A-Za-z]+");
            Match match = regex.Match(cellReference);

            return match.Value;
        }


        public Int32 Import_To_Grid(string FilePath, string Extension, HttpPostedFileBase file)
        {
            Boolean Success = false;
            Int32 HasLog = 0;
            string conn = string.Empty;

            if (file.FileName.Trim() != "")
            {

                if (Extension.ToUpper() == ".XLS" || Extension.ToUpper() == ".XLSX")
                {
                    DataTable dt = new DataTable();

                    //  DocumentFormat.OpenXml
                    //using (SpreadsheetDocument doc = SpreadsheetDocument.Open(FilePath, true))
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

                    using (SpreadsheetDocument spreadSheetDocument = SpreadsheetDocument.Open(FilePath, false))
                    {

                        WorkbookPart workbookPart = spreadSheetDocument.WorkbookPart;
                        IEnumerable<Sheet> sheets = spreadSheetDocument.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
                        string relationshipId = sheets.First().Id.Value;
                        WorksheetPart worksheetPart = (WorksheetPart)spreadSheetDocument.WorkbookPart.GetPartById(relationshipId);
                        Worksheet workSheet = worksheetPart.Worksheet;
                        SheetData sheetData = workSheet.GetFirstChild<SheetData>();
                        IEnumerable<Row> rows = sheetData.Descendants<Row>();

                        foreach (Cell cell in rows.ElementAt(0))
                        {
                            dt.Columns.Add(GetCellValue(spreadSheetDocument, cell));
                        }
                        foreach (Row row in rows) //this will also include your header row...
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
                                tempRow[columnIndex] = GetCellValue(spreadSheetDocument, cell);

                                columnIndex++;
                            }

                            dt.Rows.Add(tempRow);
                        }

                    }
                    dt.Rows.RemoveAt(0);

                    //if (dt.Rows.Count > 0)
                    //{
                    //    if((dt.Rows[dt.Rows.Count - 1]["For the Month"]))
                    //}



                    DataTable workTable = new DataTable();
                    if (workTable != null)
                    {
                        //  workTable = null;
                    }
                    DataColumn workCol = workTable.Columns.Add("REVISITID", typeof(Int32));
                    workCol.AllowDBNull = false;
                    workCol.Unique = true;

                    workTable.Columns.Add("FOR_MONTH", typeof(string));
                    workTable.Columns.Add("REVISITDATE", typeof(DateTime));
                    workTable.Columns.Add("LOGIN_ID", typeof(string));
                    workTable.Columns.Add("EMPLOYEE_NAME", typeof(string));
                    workTable.Columns.Add("SHOPPIN", typeof(string));
                    workTable.Columns.Add("SHOP_ID", typeof(Int32));
                    workTable.Columns.Add("SHOP_NAME", typeof(string));
                    workTable.Columns.Add("SHOP_CONTACT", typeof(string));
                    workTable.Columns.Add("SHOP_ADDRESS", typeof(string));
                    workTable.Columns.Add("SHOP_STATE", typeof(string));
                    workTable.Columns.Add("SHOP_TYPE", typeof(string));
                    workTable.Columns.Add("STATUS", typeof(Boolean));
                    workTable.Columns.Add("STATUS_MESSAGE", typeof(string));


                    int r = 0;

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string forthemonth = dt.Rows[0]["For the Month"].ToString().ToLower();
                        foreach (DataRow row in dt.Rows)
                        {
                            string STATUS_MESSAGE = "Sucess";

                            string FOR_MONTH = "";
                            DateTime? REVISITDATE = null;
                            string LOGIN_ID = "";
                            string Employee_State = "";
                            string EMPLOYEE_NAME = "";
                            string SHOPPIN = "";
                            long SHOP_ID = 0;
                            string SHOP_NAME = "";
                            string SHOP_CONTACT = "";
                            string SHOP_ADDRESS = "";
                            string SHOP_STATE = "";
                            string SHOP_TYPE = "";
                            bool STATUS = true;
                            string REVISITDATEstr = null;
                            string monthNames = Convert.ToString(row["For the Month"]);
                            string month = Convert.ToString(row["For the Month"]).ToLower();

                            try
                            {
                                var date = ConvertDateYYYYMMDD(Convert.ToString(row["Enter Date for Revisit(DD-MM-YYYY)"]));
                                REVISITDATE = Convert.ToDateTime(date);
                                if (REVISITDATE != null)
                                {
                                    REVISITDATEstr = Convert.ToDateTime(REVISITDATE).ToString("dd-MM-yyyy");
                                }
                            }
                            catch
                            {
                                STATUS_MESSAGE = "Revisit date not valid";
                                STATUS = false;
                            }
                            if (row["Login ID/Mobile"].ToString() != "")
                            {
                                LOGIN_ID = Convert.ToString(row["Login ID/Mobile"]);
                            }
                            else
                            {
                                STATUS_MESSAGE = "Login ID Invalid";
                                STATUS = false;
                            }
                            try
                            {
                                SHOP_ID = Convert.ToInt32(row["Shop ID"]);
                            }
                            catch
                            {
                                STATUS_MESSAGE = "Login ID Invalid";
                                STATUS = false;
                            }
                            if (row["Employee State"] != "")
                            {
                                Employee_State = row["Employee State"].ToString();
                            }
                            else
                            {
                                STATUS_MESSAGE = "User Sate Not Found";
                                STATUS = false;
                            }
                            if (row["Employee Name"] != "")
                            {
                                EMPLOYEE_NAME = row["Employee Name"].ToString();
                            }
                            else
                            {
                                STATUS = false;
                                STATUS_MESSAGE = "Employee Name Not Found";
                            }
                            //
                            if (row["Area PIN"].ToString() != "")
                            {
                                SHOPPIN = row["Area PIN"].ToString();
                            }
                            else
                            {
                                STATUS = false;
                                STATUS_MESSAGE = "Area PIN Not Found";
                            }
                            if (row["Assigned Shop Name"].ToString() != "")
                            {
                                SHOP_NAME = row["Assigned Shop Name"].ToString();
                            }
                            else
                            {
                                STATUS = false;
                                STATUS_MESSAGE = "Assigned Shop Name Not Found";
                            }




                            if (row["Address"].ToString() != "")
                            {
                                SHOP_ADDRESS = row["Address"].ToString();
                            }
                            else
                            {
                                STATUS = false;
                                STATUS_MESSAGE = "Shop Address Not Found";
                            }

                            if (Convert.ToString(row["Contact No"]) == "")
                            {
                                STATUS = false;
                                STATUS_MESSAGE = "Shop Contact Not Found";
                            }
                            try
                            {
                                SHOP_CONTACT = Convert.ToString(row["Contact No"]);
                            }
                            catch
                            {
                                STATUS = false;
                                STATUS_MESSAGE = "Shop contact Invalid";
                            }


                            if (row["State"].ToString() != "")
                            {
                                SHOP_STATE = row["State"].ToString();
                            }
                            else
                            {
                                STATUS = false;
                                STATUS_MESSAGE = "Shop State Not Found";
                            }

                            if (row["Type"].ToString() != "")
                            {
                                SHOP_TYPE = row["Type"].ToString();
                            }
                            else
                            {
                                STATUS = false;
                                STATUS_MESSAGE = "Shop State Not Found";
                            }

                            //FOR_MONTH = Convert.ToDateTime(REVISITDATEstr).ToString("MMMM").ToLower();
                            FOR_MONTH = Convert.ToDateTime(REVISITDATE).ToString("MMMM").ToLower();

                            //  var monthname = Convert.ToString(row["Month (MMM)"]);
                            if (forthemonth.ToString() != month.ToString())
                            {
                                STATUS_MESSAGE = "For the Month Mismatch";
                                STATUS = false;

                            }
                            if (forthemonth != FOR_MONTH)
                            {
                                STATUS_MESSAGE = "Revisit Month Mismatch";
                                STATUS = false;
                            }

                            //if (forthemonth == month && forthemonth == FOR_MONTH)
                            //{
                            //workTable.Rows.Add();
                            if (monthNames.Trim() != "")
                            {
                                DataRow dr = workTable.NewRow();
                                dr["REVISITID"] = ++r;
                                dr["FOR_MONTH"] = monthNames;
                                if (REVISITDATE == null)
                                {
                                    dr["REVISITDATE"] = DBNull.Value;
                                }
                                else
                                {
                                    dr["REVISITDATE"] = REVISITDATE;
                                }
                                dr["LOGIN_ID"] = LOGIN_ID;
                                dr["EMPLOYEE_NAME"] = EMPLOYEE_NAME;
                                dr["SHOPPIN"] = SHOPPIN;
                                dr["SHOP_ID"] = SHOP_ID;
                                dr["SHOP_NAME"] = SHOP_NAME;
                                dr["SHOP_CONTACT"] = SHOP_CONTACT;
                                dr["SHOP_ADDRESS"] = SHOP_ADDRESS;
                                dr["SHOP_STATE"] = SHOP_STATE;
                                dr["SHOP_TYPE"] = SHOP_TYPE;
                                dr["STATUS"] = STATUS;
                                dr["STATUS_MESSAGE"] = STATUS_MESSAGE;
                                workTable.Rows.Add(dr);
                            }
                            //}
                            //else
                            //{
                            //    //workTable.Rows.Add();
                            //    DataRow dr = workTable.NewRow();
                            //    dr["REVISITID"] = ++r;
                            //    dr["FOR_MONTH"] = month;
                            //    if (REVISITDATE == null)
                            //    {
                            //        dr["REVISITDATE"] = DBNull.Value;
                            //    }
                            //    else
                            //    {
                            //        dr["REVISITDATE"] = REVISITDATE;
                            //    }
                            //    dr["LOGIN_ID"] = LOGIN_ID;
                            //    dr["EMPLOYEE_NAME"] = EMPLOYEE_NAME;
                            //    dr["SHOPPIN"] = SHOPPIN;
                            //    dr["SHOP_ID"] = SHOP_ID;
                            //    dr["SHOP_NAME"] = SHOP_NAME;
                            //    dr["SHOP_CONTACT"] = SHOP_CONTACT;
                            //    dr["SHOP_ADDRESS"] = SHOP_ADDRESS;
                            //    dr["SHOP_STATE"] = SHOP_STATE;
                            //    dr["SHOP_TYPE"] = SHOP_TYPE;
                            //    dr["STATUS"] = false;
                            //    dr["STATUS_MESSAGE"] = "For the Month Mismatch";
                            //    workTable.Rows.Add(dr);
                            //}


                        }
                        if (workTable != null && workTable.Rows.Count > 0)
                        {
                            string userid = Session["userid"].ToString();
                            DataTable dtEmp = objemployee.GetReVisitEmport(workTable, userid);
                            if (dtEmp != null && dtEmp.Rows.Count > 0)
                            {
                                HasLog = 1;
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


        public static string GetCellValue(SpreadsheetDocument document, Cell cell)
        {
            SharedStringTablePart stringTablePart = document.WorkbookPart.SharedStringTablePart;
            if (cell.CellValue == null)
            {
                return "";
            }
            string value = cell.CellValue.InnerXml;
            if (cell.DataType != null && cell.DataType == CellValues.SharedString)
            {
                return stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
            }
            //else if (cell.DataType != null && cell.DataType == CellValues.Date)
            //{

            //    return Convert.ToString(Convert.ToDateTime(stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText));
            //}
            else
            {
                return value;
            }

        }

        public String ConvertDateYYYYMMDD(String dateformat)
        {
            String DateFormat = String.Empty;
            if (!String.IsNullOrEmpty(dateformat))
            {
                var format = dateformat.Split('-');
                DateFormat = format[2] + "-" + format[1] + "-" + format[0];
            }

            return DateFormat;
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
                        return GridViewExtension.ExportToPdf(GetEmployeesRevisitLogGridView(ViewData["EmployeesTargetSettingImportLog"]), ViewData["EmployeesTargetSettingImportLog"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetEmployeesRevisitLogGridView(ViewData["EmployeesTargetSettingImportLog"]), ViewData["EmployeesTargetSettingImportLog"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXlsx(GetEmployeesRevisitLogGridView(ViewData["EmployeesTargetSettingImportLog"]), ViewData["EmployeesTargetSettingImportLog"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetEmployeesRevisitLogGridView(ViewData["EmployeesTargetSettingImportLog"]), ViewData["EmployeesTargetSettingImportLog"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetEmployeesRevisitLogGridView(ViewData["EmployeesTargetSettingImportLog"]), ViewData["EmployeesTargetSettingImportLog"]);
                    default:
                        break;
                }
            }
            return null;
        }

        public ActionResult GetEmployeesTargetSettingImportLog(string fromdate = null, string todate = null)
        {
            List<AssigmentRevisitSettingImportLog> list = new List<AssigmentRevisitSettingImportLog>();
            DataTable dt = new DataTable();
            try
            {
                if (TempData["EmployeesTargetSettingImportLog"] == null || ((fromdate != null && todate != null) && (fromdate != "" && todate != "")))
                {

                    dt = objemployee.GetRevisitingSettingImportLog(fromdate, todate);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AssigmentRevisitSettingImportLog data = null;
                        foreach (DataRow row in dt.Rows)
                        {
                            data = new AssigmentRevisitSettingImportLog();
                            data.FOR_MONTH = Convert.ToString(row["FOR_MONTH"]);
                            if (string.IsNullOrEmpty(row["REVISITDATE"].ToString()))
                            {
                                data.REVISITDATE = null;
                            }
                            else
                            {
                                data.REVISITDATE = Convert.ToDateTime(row["REVISITDATE"]);
                            }
                            data.LOGIN_ID = Convert.ToString(row["LOGIN_ID"]);
                            data.EMPLOYEE_NAME = Convert.ToString(row["EMPLOYEE_NAME"]);
                            data.SHOPPIN = Convert.ToString(row["SHOPPIN"]);
                            data.SHOP_ID = Convert.ToInt64(row["SHOP_ID"]);
                            data.SHOP_NAME = Convert.ToString(row["SHOP_NAME"]);
                            data.SHOP_CONTACT = Convert.ToString(row["SHOP_CONTACT"]);
                            data.SHOP_ADDRESS = Convert.ToString(row["SHOP_ADDRESS"]);
                            data.SHOP_STATE = Convert.ToString(row["SHOP_STATE"]);
                            data.SHOP_TYPE = Convert.ToString(row["SHOP_TYPE"]);
                            data.CREATE_DATE = Convert.ToDateTime(row["CREATE_DATE"]);
                            data.STATUS = Convert.ToBoolean(row["STATUS"]);
                            data.STATUS_MESSAGE = Convert.ToString(row["STATUS_MESSAGE"]);
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
                        AssigmentRevisitSettingImportLog data = null;
                        foreach (DataRow row in dt.Rows)
                        {
                            data = new AssigmentRevisitSettingImportLog();
                            data.FOR_MONTH = Convert.ToString(row["FOR_MONTH"]);
                            if (string.IsNullOrEmpty(row["REVISITDATE"].ToString()))
                            {
                                data.REVISITDATE = null;
                            }
                            else
                            {
                                data.REVISITDATE = Convert.ToDateTime(row["REVISITDATE"]);
                            }
                            data.LOGIN_ID = Convert.ToString(row["LOGIN_ID"]);
                            data.EMPLOYEE_NAME = Convert.ToString(row["EMPLOYEE_NAME"]);
                            data.SHOPPIN = Convert.ToString(row["SHOPPIN"]);
                            data.SHOP_ID = Convert.ToInt64(row["SHOP_ID"]);
                            data.SHOP_NAME = Convert.ToString(row["SHOP_NAME"]);
                            data.SHOP_CONTACT = Convert.ToString(row["SHOP_CONTACT"]);
                            data.SHOP_ADDRESS = Convert.ToString(row["SHOP_ADDRESS"]);
                            data.SHOP_STATE = Convert.ToString(row["SHOP_STATE"]);
                            data.SHOP_TYPE = Convert.ToString(row["SHOP_TYPE"]);
                            data.CREATE_DATE = Convert.ToDateTime(row["CREATE_DATE"]);
                            data.STATUS = Convert.ToBoolean(row["STATUS"]);
                            data.STATUS_MESSAGE = Convert.ToString(row["STATUS_MESSAGE"]);
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

        private GridViewSettings GetEmployeesRevisitLogGridView(object datatable)
        {
            //List<EmployeesTargetSetting> obj = (List<EmployeesTargetSetting>)datatablelist;
            //ListtoDataTable lsttodt = new ListtoDataTable();
            //DataTable datatable = ConvertListToDataTable(obj); 
            var settings = new GridViewSettings();
            settings.Name = "EmployeesRevisitSettingImportLog";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "EmployeesRevisitSettingImportLog";
            //String ID = Convert.ToString(TempData["EmployeesTargetListDataTable"]);
            TempData.Keep();
            DataTable dt = (DataTable)datatable;

            foreach (System.Data.DataColumn datacolumn in dt.Columns)
            {
                if (datacolumn.ColumnName == "FOR_MONTH" || datacolumn.ColumnName == "REVISITDATE"
                    || datacolumn.ColumnName == "LOGIN_ID" || datacolumn.ColumnName == "EMPLOYEE_NAME" || datacolumn.ColumnName == "SHOPPIN" || datacolumn.ColumnName == "SHOP_ID"
                    || datacolumn.ColumnName == "SHOP_NAME" || datacolumn.ColumnName == "SHOP_CONTACT" || datacolumn.ColumnName == "SHOP_ADDRESS" || datacolumn.ColumnName == "SHOP_STATE" || datacolumn.ColumnName == "SHOP_TYPE"
                    || datacolumn.ColumnName == "CREATE_DATE" || datacolumn.ColumnName == "STATUS_MESSAGE")
                {
                    settings.Columns.Add(column =>
                    {
                        if (datacolumn.ColumnName == "FOR_MONTH")
                        {
                            column.Caption = "For the Month";
                        }
                        else if (datacolumn.ColumnName == "REVISITDATE")
                        {
                            column.Caption = "Re-Visit Date";
                        }
                        else if (datacolumn.ColumnName == "LOGIN_ID")
                        {
                            column.Caption = "Login ID";
                            column.PropertiesEdit.DisplayFormatString = "00";
                        }
                        else if (datacolumn.ColumnName == "EMPLOYEE_NAME")
                        {
                            column.Caption = "Employee Name";
                        }
                        else if (datacolumn.ColumnName == "SHOPPIN")
                        {
                            column.Caption = "Shop PIN";
                        }
                        else if (datacolumn.ColumnName == "SHOP_ID")
                        {
                            column.Caption = "Shop ID";
                        }
                        else if (datacolumn.ColumnName == "SHOP_NAME")
                        {
                            column.Caption = "Shop Name";
                        }
                        else if (datacolumn.ColumnName == "SHOP_CONTACT")
                        {
                            column.Caption = "Shop Contact";
                            column.PropertiesEdit.DisplayFormatString = "00";
                        }
                        else if (datacolumn.ColumnName == "SHOP_ADDRESS")
                        {
                            column.Caption = "Address";
                        }
                        else if (datacolumn.ColumnName == "SHOP_STATE")
                        {
                            column.Caption = "Shop State";
                        }
                        else if (datacolumn.ColumnName == "SHOP_TYPE")
                        {
                            column.Caption = "Shop Type";
                        }

                        else if (datacolumn.ColumnName == "CREATE_DATE")
                        {
                            column.Caption = "Import Date & Time";
                            column.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy hh:mm tt";
                        }
                        else if (datacolumn.ColumnName == "STATUS_MESSAGE")
                        {
                            column.Caption = "Reason";
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

        public ActionResult GetEmployeesTargetList(AssignmentRevisitModel model)
        {
            List<AssigmentRevisitSettingImportLog> list = new List<AssigmentRevisitSettingImportLog>();
            DataTable dtsm = null;
            if (model.SettingMonth > 0 && model.SettingYear > 0)
            {
                dtsm = objemployee.GetRevisitSettingImportLogMonthWise(Convert.ToString(model.SettingYear), Convert.ToString(model.SettingMonth));
                try
                {
                    if (dtsm != null && dtsm.Rows.Count > 0)
                    {
                        AssigmentRevisitSettingImportLog data = null;
                        foreach (DataRow row in dtsm.Rows)
                        {
                            data = new AssigmentRevisitSettingImportLog();
                            data.FOR_MONTH = Convert.ToString(row["FOR_MONTH"]);
                            if (string.IsNullOrEmpty(row["REVISITDATE"].ToString()))
                            {
                                data.REVISITDATE = null;
                            }
                            else
                            {
                                data.REVISITDATE = Convert.ToDateTime(row["REVISITDATE"]);
                            }
                            data.LOGIN_ID = Convert.ToString(row["LOGIN_ID"]);
                            data.EMPLOYEE_NAME = Convert.ToString(row["EMPLOYEE_NAME"]);
                            data.SHOPPIN = Convert.ToString(row["SHOPPIN"]);
                            data.SHOP_ID = Convert.ToInt64(row["SHOP_ID"]);
                            data.SHOP_NAME = Convert.ToString(row["SHOP_NAME"]);
                            data.SHOP_CONTACT = Convert.ToString(row["SHOP_CONTACT"]);
                            data.SHOP_ADDRESS = Convert.ToString(row["SHOP_ADDRESS"]);
                            data.SHOP_STATE = Convert.ToString(row["SHOP_STATE"]);
                            data.SHOP_TYPE = Convert.ToString(row["SHOP_TYPE"]);
                            //data.CREATE_DATE = Convert.ToDateTime(row["CREATE_DATE"]);
                            //data.STATUS = Convert.ToBoolean(row["STATUS"]);
                            //data.STATUS_MESSAGE = Convert.ToString(row["STATUS_MESSAGE"]);
                            list.Add(data);
                        }
                    }
                    ViewBag.CounterType = "";


                }
                catch (Exception ex)
                {

                }
            }
            //TempData["EmployeesTargetList"] = dt;
            //TempData["EmployeesTargetListModel"] = list;
            ////TempData["GetEmployeesTargetByID"] = dt2;
            TempData["EmployeesTargetListDataTable"] = dtsm;
            TempData.Keep();
            //ViewBag.Data = obj;
            //return PartialView("_EmployeesTargetList", list);
            return PartialView("_EmployeesRevisitSettingList", list);
        }

        public ActionResult ExportEmployeesTargetList(int type)
        {
            ViewData["EmployeesRevisitList"] = TempData["EmployeesTargetListDataTable"];

            TempData.Keep();

            if (ViewData["EmployeesRevisitList"] != null)
            {

                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetEmployeesTargetGridView(ViewData["EmployeesRevisitList"]), ViewData["EmployeesRevisitList"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetEmployeesTargetGridView(ViewData["EmployeesRevisitList"]), ViewData["EmployeesRevisitList"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetEmployeesTargetGridView(ViewData["EmployeesRevisitList"]), ViewData["EmployeesRevisitList"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetEmployeesTargetGridView(ViewData["EmployeesRevisitList"]), ViewData["EmployeesRevisitList"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetEmployeesTargetGridView(ViewData["EmployeesRevisitList"]), ViewData["EmployeesRevisitList"]);
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
            settings.Name = "EmployeesRevistList";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "EmployeesRevisitList";
            //String ID = Convert.ToString(TempData["EmployeesTargetListDataTable"]);
            TempData.Keep();
            DataTable dt = (DataTable)datatable;

            foreach (System.Data.DataColumn datacolumn in dt.Columns)
            {
                if (datacolumn.ColumnName == "FOR_MONTH" || datacolumn.ColumnName == "REVISITDATE"
                    || datacolumn.ColumnName == "LOGIN_ID" || datacolumn.ColumnName == "EMPLOYEE_NAME" || datacolumn.ColumnName == "SHOPPIN" || datacolumn.ColumnName == "SHOP_ID"
                    || datacolumn.ColumnName == "SHOP_NAME" || datacolumn.ColumnName == "SHOP_CONTACT" || datacolumn.ColumnName == "SHOP_ADDRESS" || datacolumn.ColumnName == "SHOP_STATE" || datacolumn.ColumnName == "SHOP_TYPE")
                {
                    settings.Columns.Add(column =>
                    {
                        if (datacolumn.ColumnName == "FOR_MONTH")
                        {
                            column.Caption = "For the Month";
                        }
                        else if (datacolumn.ColumnName == "REVISITDATE")
                        {
                            column.Caption = "Re-visit Date";
                        }
                        else if (datacolumn.ColumnName == "LOGIN_ID")
                        {
                            column.Caption = "Login ID";
                            column.PropertiesEdit.DisplayFormatString = "00";
                        }
                        else if (datacolumn.ColumnName == "EMPLOYEE_NAME")
                        {
                            column.Caption = "Employee Name";
                        }
                        else if (datacolumn.ColumnName == "SHOPPIN")
                        {
                            column.Caption = "Area Pin";
                        }
                        else if (datacolumn.ColumnName == "SHOP_ID")
                        {
                            column.Caption = "Shop ID";
                        }
                        else if (datacolumn.ColumnName == "SHOP_NAME")
                        {
                            column.Caption = "Shop Name";
                        }
                        else if (datacolumn.ColumnName == "SHOP_CONTACT")
                        {
                            column.Caption = "Shop Contact";
                            column.PropertiesEdit.DisplayFormatString = "00";
                        }
                        else if (datacolumn.ColumnName == "SHOP_ADDRESS")
                        {
                            column.Caption = "Address";
                        }
                        else if (datacolumn.ColumnName == "SHOP_STATE")
                        {
                            column.Caption = "Shop State";
                        }
                        else if (datacolumn.ColumnName == "SHOP_TYPE")
                        {
                            column.Caption = "Shop Type";
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
    }
}
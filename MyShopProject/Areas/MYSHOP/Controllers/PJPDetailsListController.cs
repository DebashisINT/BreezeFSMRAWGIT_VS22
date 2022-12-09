using BusinessLogicLayer.SalesmanTrack;
using DataAccessLayer;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DevExpress.XtraPrinting;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Models;
using MyShop.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class PJPDetailsListController : Controller
    {
        PJPListDetailsBL obj = new PJPListDetailsBL();
        public ActionResult PJPListIndex()
        {
            EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/PJPDetailsList/PJPListIndex");
            try
            {
                PJPListModel omodel = new PJPListModel();
                string userid = Session["userid"].ToString();
                omodel.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                omodel.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                ViewBag.CanView = rights.CanView;
                ViewBag.CanExport = rights.CanExport;
                return View(omodel);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
            //  return View();
        }

        public ActionResult PartialPJPDetailsGridList(String Is_PageLoad)
        {
            EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/PJPDetailsList/PJPListIndex");
            ViewBag.CanView = rights.CanView;
            ViewBag.CanExport = rights.CanExport;
            return PartialView(GetDataDetails(Is_PageLoad));
        }

        public IEnumerable GetDataDetails(string Is_PageLoad)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);
            if (Is_PageLoad != "Ispageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSPJPLIST_REPORTs
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSPJPLIST_REPORTs
                        where d.USERID == Convert.ToInt32(Userid) && d.PJP_ID == 0
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
        }

        public ActionResult GetPJPDetailsList(PJPListModel model)
        {
            try
            {
                string Is_PageLoad = string.Empty;
                DataTable dt = new DataTable();
                if (model.Fromdate == null)
                {
                    model.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                }

                if (model.Todate == null)
                {
                    model.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                }

                if (model.Is_PageLoad != "1") Is_PageLoad = "Ispageload";

                ViewData["ModelData"] = model;

                string datfrmat = model.Fromdate.Split('-')[2] + '-' + model.Fromdate.Split('-')[1] + '-' + model.Fromdate.Split('-')[0];
                string dattoat = model.Todate.Split('-')[2] + '-' + model.Todate.Split('-')[1] + '-' + model.Todate.Split('-')[0];
                string Userid = Convert.ToString(Session["userid"]);

                string state = "";
                int i = 1;
                if (model.StateId != null && model.StateId.Count > 0)
                {
                    foreach (string item in model.StateId)
                    {
                        if (i > 1)
                            state = state + "," + item;
                        else
                            state = item;
                        i++;
                    }
                }

                string empcode = "";
                int k = 1;
                if (model.empcode != null && model.empcode.Count > 0)
                {
                    foreach (string item in model.empcode)
                    {
                        if (k > 1)
                            empcode = empcode + "," + item;
                        else
                            empcode = item;
                        k++;
                    }
                }

                double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;
                if (days <= 30)
                {
                    dt = obj.GetReportPJPDetails(datfrmat, dattoat, Userid, state, empcode);
                }
                return Json(Userid, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult ExporRegisterList(int type)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetDoctorBatchGridViewSettings(), GetDataDetails(""));
                case 2:
                    return GridViewExtension.ExportToXlsx(GetDoctorBatchGridViewSettings(), GetDataDetails(""));
                case 3:
                    return GridViewExtension.ExportToXls(GetDoctorBatchGridViewSettings(), GetDataDetails(""));
                case 4:
                    return GridViewExtension.ExportToRtf(GetDoctorBatchGridViewSettings(), GetDataDetails(""));
                case 5:
                    return GridViewExtension.ExportToCsv(GetDoctorBatchGridViewSettings(), GetDataDetails(""));
                default:
                    break;
            }
            return null;
        }

        private GridViewSettings GetDoctorBatchGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "gridPJPDetails";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "PJP Details";

            settings.Columns.Add(x =>
            {
                x.FieldName = "EMP_NAME";
                x.Caption = "Employee Name";
                x.VisibleIndex = 1;
                x.Width = 220;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "EMP_ID";
                x.Caption = "User ID";
                x.VisibleIndex = 2;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Emp_Phone";
                x.Caption = "Phone";
                x.VisibleIndex = 3;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Supervisor";
                x.Caption = "Supervisor";
                x.VisibleIndex = 4;
                x.Width = 200;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Supervisor_ID";
                x.Caption = "Supervisor ID";
                x.VisibleIndex = 5;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "PJP_DATE";
                x.Caption = "Date";
                x.VisibleIndex = 6;
                x.Width = 100;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "From_Time";
                x.Caption = "From Time";
                x.VisibleIndex = 7;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "To_Time";
                x.Caption = "To Time";
                x.VisibleIndex = 8;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_Name";
                x.Caption = "Customer";
                x.VisibleIndex = 9;
                x.Width = 200;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Location";
                x.Caption = "Location";
                x.VisibleIndex = 10;
                x.Width = 150;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Remarks";
                x.Caption = "Remarks";
                x.VisibleIndex = 11;
                x.Width = 200;
            });


            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

        public ActionResult ImportExcel()
        {
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
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
                        Import_To_Grid(fname, extension, file);
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

        public Int32 Import_To_Grid(string FilePath, string Extension, HttpPostedFileBase file)
        {
            Boolean Success = false;
            Int32 HasLog = 0;

            if (file.FileName.Trim() != "")
            {
                if (Extension.ToUpper() == ".XLS" || Extension.ToUpper() == ".XLSX")
                {
                    DataTable dt = new DataTable();
                    DataTable dtExcelData = new DataTable();
                    //using (SpreadsheetDocument doc = SpreadsheetDocument.Open(FilePath, false))
                    //{
                    //Sheet sheet = doc.WorkbookPart.Workbook.Sheets.GetFirstChild<Sheet>();
                    //Worksheet worksheet = (doc.WorkbookPart.GetPartById(sheet.Id.Value) as WorksheetPart).Worksheet;
                    //IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>().DefaultIfEmpty();
                    //foreach (Row row in rows)
                    //{
                    //    if (row.RowIndex.Value == 1)
                    //    {
                    //        foreach (Cell cell in row.Descendants<Cell>())
                    //        {
                    //            if (cell.CellValue != null)
                    //            {
                    //                dt.Columns.Add(GetValue(doc, cell));
                    //            }
                    //        }
                    //    }
                    //    else
                    //    {
                    //        dt.Rows.Add();
                    //        int i = 0;
                    //        foreach (Cell cell in row.Descendants<Cell>())
                    //        {
                    //            if (cell.CellValue != null)
                    //            {
                    //                dt.Rows[dt.Rows.Count - 1][i] = GetValue(doc, cell);
                    //            }
                    //            i++;
                    //        }
                    //    }
                    //}

                    string conString = string.Empty;
                    conString = ConfigurationManager.AppSettings["ExcelConString"];
                    conString = string.Format(conString, FilePath);
                    using (OleDbConnection excel_con = new OleDbConnection(conString))
                    {
                        excel_con.Open();
                        string sheet1 = "Sheet$"; //excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[1]["TABLE_NAME"].ToString();

                        dtExcelData.Columns.Add("Date(DD-MM-YYYY)", typeof(string));
                        dtExcelData.Columns.Add("From Time(AM/PM)", typeof(string));
                        dtExcelData.Columns.Add("To Time(AM/PM)", typeof(string));
                        dtExcelData.Columns.Add("State", typeof(string));
                        dtExcelData.Columns.Add("Employee Name", typeof(string));
                        dtExcelData.Columns.Add("Customer Name", typeof(string));
                        dtExcelData.Columns.Add("Customer Type", typeof(string));
                        dtExcelData.Columns.Add("Mobile", typeof(string));
                        dtExcelData.Columns.Add("City", typeof(string));
                        dtExcelData.Columns.Add("Area", typeof(string));
                        dtExcelData.Columns.Add("Lat", typeof(string));
                        dtExcelData.Columns.Add("Long", typeof(string));
                        dtExcelData.Columns.Add("Radius", typeof(string));
                        dtExcelData.Columns.Add("Remarks", typeof(string));

                        using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
                        {
                            oda.Fill(dtExcelData);
                        }
                        excel_con.Close();
                    }
                    // }
                    //  dt.Rows.RemoveAt(0);

                    DataTable workTable = new DataTable();
                    if (workTable != null)
                    {
                        //  workTable = null;
                    }
                    //DataColumn workCol = workTable.Columns.Add("REVISITID", typeof(Int32));
                    //workCol.AllowDBNull = false;
                    //workCol.Unique = true;

                    workTable.Columns.Add("Date", typeof(DateTime));
                    workTable.Columns.Add("From_Time", typeof(string));
                    workTable.Columns.Add("To_Time", typeof(string));
                    workTable.Columns.Add("State", typeof(string));
                    workTable.Columns.Add("Employee", typeof(string));
                    workTable.Columns.Add("Shop_Name", typeof(string));
                    workTable.Columns.Add("Shop_Type", typeof(string));
                    workTable.Columns.Add("Shop_Contact", typeof(string));
                    workTable.Columns.Add("City", typeof(string));
                    workTable.Columns.Add("Area", typeof(string));
                    workTable.Columns.Add("Remarks", typeof(string));
                    workTable.Columns.Add("LAT", typeof(string));
                    workTable.Columns.Add("LONG", typeof(string));
                    workTable.Columns.Add("Radius", typeof(string));
                    workTable.Columns.Add("STATUS", typeof(Boolean));
                    workTable.Columns.Add("STATUS_MESSAGE", typeof(string));


                    int r = 0;

                    if (dtExcelData != null && dtExcelData.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtExcelData.Rows)
                        {
                            string STATUS_MESSAGE = "Sucess";

                            DateTime? PJPDate = null;
                            string From_Time = "";
                            string To_Time = "";
                            string EMPLOYEE_NAME = "";
                            string Shop_Name = "";
                            String Mobile = "";
                            string City = "";
                            string Area = "";
                            string Remarks = "";
                            bool STATUS = true;
                            string PJPDatestr = null;

                            if (Convert.ToString(row["Date(DD-MM-YYYY)"]) == "" && Convert.ToString(row["From Time(AM/PM)"]) == "" && row["To Time(AM/PM)"].ToString() == "" && row["Mobile"].ToString() == "" && row["Area"].ToString() == "")
                            {
                                break;
                            }

                            try
                            {
                                var date = ConvertDateYYYYMMDD(Convert.ToString(row["Date(DD-MM-YYYY)"]));
                                PJPDate = Convert.ToDateTime(date);
                                if (PJPDate != null)
                                {
                                    PJPDatestr = Convert.ToDateTime(PJPDate).ToString("yyyy-MM-dd");
                                }
                            }
                            catch
                            {
                                STATUS_MESSAGE = "Date not valid";
                                STATUS = false;
                            }

                            if (row["From Time(AM/PM)"].ToString() != "")
                            {
                                From_Time = Convert.ToString(row["From Time(AM/PM)"]);
                            }
                            else
                            {
                                //STATUS_MESSAGE = "From Time Invalid";
                                //STATUS = false;
                            }

                            if (row["To Time(AM/PM)"] != "")
                            {
                                To_Time = row["To Time(AM/PM)"].ToString();
                            }
                            else
                            {
                                //STATUS_MESSAGE = "To Time Invalid";
                                //STATUS = false;
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

                            //if (row["Shop Name"].ToString() != "")
                            //{
                            //    Shop_Name = row["Shop Name"].ToString();
                            //}
                            //else
                            //{
                            //    STATUS = true;
                            //    STATUS_MESSAGE = "Shop Name Found";
                            //}

                            //if (row["Mobile"].ToString() != "")
                            //{
                            //    Mobile = row["Mobile"].ToString();
                            //}
                            //else
                            //{
                            //    STATUS = true;
                            //    STATUS_MESSAGE = "Shop Contact Not Found";
                            //}

                            //if (row["City"].ToString() != "")
                            //{
                            //    City = row["City"].ToString();
                            //}
                            //else
                            //{
                            //    STATUS = true;
                            //    STATUS_MESSAGE = "City Not Found";
                            //}

                            //if (Convert.ToString(row["Area"]) == "")
                            //{
                            //    STATUS = true;
                            //    STATUS_MESSAGE = "Area Not Found";
                            //}
                            //else
                            //{
                            //    Area = Convert.ToString(row["Area"]);
                            //}


                            DataRow dr = workTable.NewRow();
                            if (PJPDate == null)
                            {
                                dr["Date"] = DBNull.Value;
                            }
                            else
                            {
                                dr["Date"] = PJPDate;
                            }
                            dr["From_Time"] = From_Time;
                            dr["To_Time"] = To_Time;
                            dr["State"] = row["State"].ToString();
                            dr["Employee"] = EMPLOYEE_NAME;
                            dr["Shop_Name"] = row["Customer Name"].ToString();
                            dr["Shop_Type"] = row["Customer Type"].ToString();
                            dr["Shop_Contact"] = row["Mobile"].ToString();
                            dr["City"] = row["City"].ToString();
                            dr["Area"] = Convert.ToString(row["Area"]);
                            dr["Remarks"] = row["Remarks"].ToString();
                            dr["LAT"] = Convert.ToString(row["LAT"]);
                            dr["LONG"] = row["LONG"].ToString();
                            dr["Radius"] = Convert.ToString(row["Radius"]);
                            dr["STATUS"] = STATUS;
                            dr["STATUS_MESSAGE"] = STATUS_MESSAGE;
                            workTable.Rows.Add(dr);


                        }
                        if (workTable != null && workTable.Rows.Count > 0)
                        {
                            string userid = Session["userid"].ToString();
                            DataTable dtEmp = obj.GetPJPDetailsImport(workTable, userid);
                            if (dtEmp != null && dtEmp.Rows.Count > 0)
                            {
                                HasLog = 1;
                            }

                            TempData["PJPDetailsImportLog"] = dtEmp;
                        }
                    }
                }
            }
            return HasLog;
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

        private string GetValue(SpreadsheetDocument doc, Cell cell)
        {
            string value = cell.CellValue.InnerText;
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value)).InnerText;
            }
            return value;
        }


        public ActionResult GetShopListTemplateByArea(PJPEmployeeListModel model)
        {
            string AreaId = "";
            int i = 1;
            if (model.AreaId != null && model.AreaId.Count > 0)
            {
                foreach (string item in model.AreaId)
                {
                    if (i > 1)
                        AreaId = AreaId + "," + item;
                    else
                        AreaId = item;
                    i++;
                }
            }

            string EmpId = "";
            int j = 1;
            if (model.EmpId != null && model.EmpId.Count > 0)
            {
                foreach (string item in model.EmpId)
                {
                    if (j > 1)
                        EmpId = EmpId + "," + item;
                    else
                        EmpId = item;
                    j++;
                }
            }


            DataTable dt = new DataTable();
            try
            {
                dt = obj.GetExportPJPList(AreaId, EmpId);
                TempData["PJPDataListDataTable"] = dt;
                TempData.Keep();
                //string Employee, string MONTH, string YEAR, string stateID, string DESIGNID
            }
            catch { }

            return Json("Done.");

            // return GridViewExtension.ExportToXlsx(GetShopListTemplateByAreaExcel(dt,""), dt);
        }


        public ActionResult getExportPJP()
        {

            DataTable dt = TempData["PJPDataListDataTable"] as DataTable;
            return GridViewExtension.ExportToXlsx(GetShopListTemplateByAreaExcel(dt, ""), dt, true, getXlsExportOptions());
        }

        private XlsxExportOptionsEx getXlsExportOptions()
        {
            DevExpress.XtraPrinting.XlsxExportOptionsEx obj = new DevExpress.XtraPrinting.XlsxExportOptionsEx(DevExpress.XtraPrinting.TextExportMode.Text);
            obj.ExportMode = DevExpress.XtraPrinting.XlsxExportMode.SingleFile;
            obj.ExportType = DevExpress.Export.ExportType.WYSIWYG;


            return obj;

        }

        private GridViewSettings GetShopListTemplateByAreaExcel(object datatable, String dates)
        {
            var settings = new GridViewSettings();
            settings.Name = "PJPAssignment";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "PJPAssignment";
            settings.Name = "PJP Details";
            //String ID = Convert.ToString(TempData["EmployeesTargetListDataTable"]);
            TempData.Keep();
            DataTable dt = (DataTable)datatable;



            foreach (System.Data.DataColumn datacolumn in dt.Columns)
            {
                if (datacolumn.ColumnName == "Date" || datacolumn.ColumnName == "From_Time"
                    || datacolumn.ColumnName == "To_Time" || datacolumn.ColumnName == "Employee" || datacolumn.ColumnName == "STATE" || datacolumn.ColumnName == "Shop"
                    || datacolumn.ColumnName == "SHOP_TYPE" || datacolumn.ColumnName == "Mobile" || datacolumn.ColumnName == "City" || datacolumn.ColumnName == "Area"
                    || datacolumn.ColumnName == "LAT" || datacolumn.ColumnName == "LONG" || datacolumn.ColumnName == "Radius" || datacolumn.ColumnName == "Remarks")
                {
                    settings.Columns.Add(column =>
                    {
                        if (datacolumn.ColumnName == "Date")
                        {
                            column.Caption = "Date(DD-MM-YYYY)";
                            column.ColumnType = MVCxGridViewColumnType.DateEdit;
                            // column.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                            (column.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
                        }
                        else if (datacolumn.ColumnName == "From_Time")
                        {
                            column.Caption = "From Time(AM/PM)";
                            column.ColumnType = MVCxGridViewColumnType.TimeEdit;
                            // column.PropertiesEdit.DisplayFormatString = "hh:mm";
                            (column.PropertiesEdit as TimeEditProperties).EditFormatString = "hh:mm";
                        }
                        else if (datacolumn.ColumnName == "To_Time")
                        {
                            column.Caption = "To Time(AM/PM)";
                            column.ColumnType = MVCxGridViewColumnType.TimeEdit;
                            // column.PropertiesEdit.DisplayFormatString = "hh:mm";
                            (column.PropertiesEdit as TimeEditProperties).EditFormatString = "hh:mm";
                        }
                        else if (datacolumn.ColumnName == "STATE")
                        {
                            column.Caption = "State";
                        }
                        else if (datacolumn.ColumnName == "Employee")
                        {
                            column.Caption = "Employee Name";
                        }
                        else if (datacolumn.ColumnName == "Shop")
                        {
                            column.Caption = "Customer Name";
                        }
                        else if (datacolumn.ColumnName == "SHOP_TYPE")
                        {
                            column.Caption = "Customer Type";
                        }
                        else if (datacolumn.ColumnName == "Mobile")
                        {
                            column.Caption = "Mobile";
                        }
                        else if (datacolumn.ColumnName == "City")
                        {
                            column.Caption = "City";
                        }
                        else if (datacolumn.ColumnName == "Area")
                        {
                            column.Caption = "Area";
                        }
                        else if (datacolumn.ColumnName == "LAT")
                        {
                            column.Caption = "Lat";
                        }
                        else if (datacolumn.ColumnName == "LONG")
                        {
                            column.Caption = "Long";
                        }
                        else if (datacolumn.ColumnName == "Radius")
                        {
                            column.Caption = "Radius";
                        }
                        else if (datacolumn.ColumnName == "Remarks")
                        {
                            column.Caption = "Remarks";
                        }

                        column.FieldName = datacolumn.ColumnName;

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

        public ActionResult GetPJPDetailsImportLog()
        {
            List<PJPImportLog> list = new List<PJPImportLog>();
            DataTable dt = new DataTable();
            try
            {
                if (TempData["PJPDetailsImportLog"] != null)
                {
                    dt = (DataTable)TempData["PJPDetailsImportLog"];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        PJPImportLog data = null;
                        foreach (DataRow row in dt.Rows)
                        {
                            data = new PJPImportLog();
                            data.Date = Convert.ToString(row["Date"]);
                            data.City = Convert.ToString(row["City"]);
                            data.Area = Convert.ToString(row["Area"]);
                            data.From_Time = Convert.ToString(row["From_Time"]);
                            data.To_Time = Convert.ToString(row["To_Time"]);
                            data.Employee = Convert.ToString(row["Employee"]);
                            data.Shop_Name = Convert.ToString(row["Shop_Name"]);
                            data.Shop_Contact = Convert.ToString(row["Shop_Contact"]);
                            data.STATUS = Convert.ToString(row["STATUS"]);
                            data.STATUS_MESSAGE = Convert.ToString(row["STATUS_MESSAGE"]);

                            data.State = Convert.ToString(row["State"]);
                            data.Shop_Type = Convert.ToString(row["Shop_Type"]);
                            data.LAT = Convert.ToString(row["LAT"]);
                            data.LONG = Convert.ToString(row["LONG"]);
                            data.Radius = Convert.ToString(row["Radius"]);
                            list.Add(data);
                        }
                    }
                    TempData["PJPDetailsImportLog"] = dt;
                }
            }
            catch (Exception ex)
            {
            }
            TempData.Keep();
            return PartialView(list);
        }

        public ActionResult ExportEmployeesPJPLogList(int type)
        {
            ViewData["PJPDetailsImportLog"] = TempData["PJPDetailsImportLog"];

            TempData.Keep();

            if (ViewData["PJPDetailsImportLog"] != null)
            {

                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetEmployeesPJPLogGridView(ViewData["PJPDetailsImportLog"]), ViewData["PJPDetailsImportLog"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetEmployeesPJPLogGridView(ViewData["PJPDetailsImportLog"]), ViewData["PJPDetailsImportLog"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXlsx(GetEmployeesPJPLogGridView(ViewData["PJPDetailsImportLog"]), ViewData["PJPDetailsImportLog"], true, getXlsExportOptions());
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetEmployeesPJPLogGridView(ViewData["PJPDetailsImportLog"]), ViewData["PJPDetailsImportLog"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetEmployeesPJPLogGridView(ViewData["PJPDetailsImportLog"]), ViewData["PJPDetailsImportLog"]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetEmployeesPJPLogGridView(object datatable)
        {

            var settings = new GridViewSettings();
            settings.Name = "EmployeesPJPImportLog";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "EmployeesPJPImportLog";
            //String ID = Convert.ToString(TempData["EmployeesTargetListDataTable"]);
            TempData.Keep();
            DataTable dt = (DataTable)datatable;

            foreach (System.Data.DataColumn datacolumn in dt.Columns)
            {
                if (datacolumn.ColumnName == "Date" || datacolumn.ColumnName == "From_Time"
                    || datacolumn.ColumnName == "To_Time" || datacolumn.ColumnName == "State" || datacolumn.ColumnName == "Employee" || datacolumn.ColumnName == "Shop_Name"
                    || datacolumn.ColumnName == "Shop_Type"
                    || datacolumn.ColumnName == "Shop_Contact" || datacolumn.ColumnName == "City" || datacolumn.ColumnName == "Area" || datacolumn.ColumnName == "LAT"
                    || datacolumn.ColumnName == "LONG" || datacolumn.ColumnName == "Radius" || datacolumn.ColumnName == "Remarks" || datacolumn.ColumnName == "STATUS_MESSAGE")
                {
                    settings.Columns.Add(column =>
                    {
                        if (datacolumn.ColumnName == "Date")
                        {
                            column.Caption = "Date";
                            column.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                        }
                        else if (datacolumn.ColumnName == "From_Time")
                        {
                            column.Caption = "From Time";
                        }
                        else if (datacolumn.ColumnName == "To_Time")
                        {
                            column.Caption = "To Time";
                        }
                        else if (datacolumn.ColumnName == "State")
                        {
                            column.Caption = "State";
                        }
                        else if (datacolumn.ColumnName == "Employee")
                        {
                            column.Caption = "Employee Name";
                        }
                        else if (datacolumn.ColumnName == "Shop_Name")
                        {
                            column.Caption = "Customer Name";
                        }
                        else if (datacolumn.ColumnName == "Shop_Type")
                        {
                            column.Caption = "Customer Type";
                        }
                        else if (datacolumn.ColumnName == "Shop_Contact")
                        {
                            column.Caption = "Customer Contact";
                            column.PropertiesEdit.DisplayFormatString = "00";
                        }
                        else if (datacolumn.ColumnName == "City")
                        {
                            column.Caption = "City";
                        }
                        else if (datacolumn.ColumnName == "Area")
                        {
                            column.Caption = "Area";
                        }
                        else if (datacolumn.ColumnName == "LAT")
                        {
                            column.Caption = "Lat";
                        }

                        else if (datacolumn.ColumnName == "LONG")
                        {
                            column.Caption = "Long";
                        }
                        else if (datacolumn.ColumnName == "Radius")
                        {
                            column.Caption = "Radius";
                        }
                        else if (datacolumn.ColumnName == "Remarks")
                        {
                            column.Caption = "Remarks";
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
    }
}
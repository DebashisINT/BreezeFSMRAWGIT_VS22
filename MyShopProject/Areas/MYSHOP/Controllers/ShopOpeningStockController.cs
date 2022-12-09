using BusinessLogicLayer.SalesmanTrack;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class ShopOpeningStockController : Controller
    {
        ProductOpeningStockBL objwar = new ProductOpeningStockBL();
        public ActionResult OpeningStockIndex()
        {
            OpeningStockModel model = new OpeningStockModel();
            List<DistributerList> shop = new List<DistributerList>();
            List<warehouse> warehouse = new List<warehouse>();
            DataSet ds = objwar.GetMasterDropdownListAll();

            if (ds != null && ds.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[1].Rows)
                {
                    shop.Add(new DistributerList
                    {
                        Shop_Code = Convert.ToString(item["Shop_Code"]),
                        Shop_Name = Convert.ToString(item["Shop_Name"])
                    });
                }
            }

            if (ds != null && ds.Tables[2].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[2].Rows)
                {
                    warehouse.Add(new warehouse
                    {
                        WAREHOUSE_ID = Convert.ToString(item["WAREHOUSE_ID"]),
                        WAREHOUSE_NAME = Convert.ToString(item["WAREHOUSE_NAME"])
                    });
                }
            }

            model.warehouse_List = warehouse;
            model.Distributer_List = shop;
            TempData["Count"] = 1;
            return View(model);
        }

        public PartialViewResult GridProductOnepingStock(String Shop_code)
        {
            ProductStockList productdataobj = new ProductStockList();
            List<ProductStockList> productdata = new List<ProductStockList>();

            try
            {
                if (Shop_code != "")
                {
                    DataTable objData = objwar.GetProductOpeningStockList(Shop_code);
                    if (objData != null && objData.Rows.Count > 0)
                    {
                        foreach (DataRow row in objData.Rows)
                        {
                            productdataobj = new ProductStockList();
                            productdataobj.SlNO = Convert.ToString(row["SlNO"]);
                            productdataobj.Product_Name = Convert.ToString(row["sProducts_Name"]);
                            productdataobj.product_id = Convert.ToString(row["sProducts_ID"]);
                            productdataobj.CurrentStock = Convert.ToString(row["STOCK"]);
                            productdataobj.newStock = "0.00";
                            productdataobj.ClosingStock = Convert.ToString(row["STOCK"]);
                            productdataobj.ProductClass_Name = Convert.ToString(row["ProductClass_Name"]);
                            productdataobj.Brand_Name = Convert.ToString(row["Brand_Name"]);
                            productdataobj.PRODUCT_CODE = Convert.ToString(row["PRODUCT_CODE"]);
                            productdata.Add(productdataobj);
                        }

                        //  ViewData["EstimateEntryProductsTotalAm"] = productdata.Sum(x => Convert.ToDecimal(x.NetAmount)).ToString();
                    }
                }

            }
            catch { }
            return PartialView(productdata);
        }

        [ValidateInput(false)]
        public ActionResult BatchEditingUpdateOpeningStock(DevExpress.Web.Mvc.MVCxGridViewBatchUpdateValues<ProductStockList, int> updateValues, OpeningStockModel options)
        {
            TempData["Count"] = (int)TempData["Count"] + 1;
            TempData.Keep();
            String NumberScheme = "";
            String Message = "";
            Int64 SaveDataArea = 0;

            List<udtStockProduct> udt = new List<udtStockProduct>();

            if ((int)TempData["Count"] != 2)
            {
                Boolean IsProcess = false;
                List<ProductStockList> list = new List<ProductStockList>();
                //foreach (var product in updateValues.Insert)
                //if (updateValues.Insert.Count > 0 && Convert.ToInt64(options.DetailsID) < 1)
                //{
                //    List<udtStockProducts> udtlist = new List<udtStockProducts>();
                //    udtStockProducts obj = null;
                //    updateValues.Insert = updateValues.Insert.OrderBy(x => Convert.ToInt64(x.SlNO)).ToList();
                //    foreach (var item in updateValues.Insert)
                //    {
                //        if (Convert.ToInt64(item.ProductId) > 0)
                //        {
                //            if (String.IsNullOrEmpty(item.Tag_Production_ID))
                //            {
                //                item.Tag_Production_ID = "0";
                //            }
                //            if (String.IsNullOrEmpty(item.Tag_Details_ID))
                //            {
                //                item.Tag_Details_ID = "0";
                //            }

                //            obj = new udtStockProducts();
                //            obj.ProductID = Convert.ToInt64(item.ProductId);
                //            obj.StkQty = Convert.ToDecimal(item.ProductQty);
                //            obj.StkUOM = (item.ProductUOM);
                //            obj.IssuesQty = Convert.ToDecimal(0);
                //            obj.IssuesUOM = (" ");
                //            obj.WarehouseID = Convert.ToInt64(item.ProductsWarehouseID);
                //            obj.Price = Convert.ToDecimal(item.Price);
                //            obj.Amount = Convert.ToDecimal(item.Amount);
                //            obj.Tag_Details_ID = Convert.ToInt64(item.Tag_Details_ID);
                //            obj.Tag_Production_ID = Convert.ToInt64(item.Tag_Production_ID);
                //            obj.Tag_REV_No = item.RevNo;
                //            obj.Remarks = (item.Remarks);
                //            obj.SlNo = (item.SlNO);
                //            obj.Charges = (item.Charges);
                //            obj.Discount = (item.Discount);
                //            obj.NetAmount = (item.NetAmount);
                //            obj.BudgetedPrice = (item.BudgetedPrice);
                //            obj.TaxTypeID = (item.TaxTypeID);
                //            obj.TaxType = (item.TaxType);
                //            udtlist.Add(obj);
                //            //}
                //        }
                //    }
                //    if (udtlist.Count > 0)
                //    {
                //        SaveDataArea = 1;
                //        //if (options.BOMNo)
                //        NumberScheme = checkNMakeEstimateCode(options.strEstimateNo, Convert.ToInt32(options.Estimate_SCHEMAID), Convert.ToDateTime(options.RevisionDate));
                //        if (NumberScheme == "ok")
                //        {
                //            udtlist = udtlist.OrderBy(x => Convert.ToInt64(x.SlNo)).ToList();
                //            foreach (var item in udtlist)
                //            {
                //                udtStockProduct obj1 = new udtStockProduct();
                //                obj1.ProductID = Convert.ToInt64(item.ProductID);
                //                obj1.StkQty = Convert.ToDecimal(item.StkQty);
                //                obj1.StkUOM = (item.StkUOM);
                //                obj1.IssuesQty = (item.IssuesQty);
                //                obj1.IssuesUOM = (" ");
                //                obj1.WarehouseID = Convert.ToInt64(item.WarehouseID);
                //                obj1.Price = Convert.ToDecimal(item.Price);
                //                obj1.Amount = Convert.ToDecimal(item.Amount);
                //                obj1.Tag_Details_ID = Convert.ToInt64(item.Tag_Details_ID);
                //                obj1.Tag_Production_ID = Convert.ToInt64(item.Tag_Production_ID);
                //                obj1.Tag_REV_No = item.Tag_REV_No;
                //                obj1.Remarks = (item.Remarks);
                //                obj1.Charges = (item.Charges);
                //                obj1.Discount = (item.Discount);
                //                obj1.NetAmount = (item.NetAmount);
                //                obj1.BudgetedPrice = (item.BudgetedPrice);
                //                obj1.TaxTypeID = (item.TaxTypeID);
                //                obj1.TaxType = (item.TaxType);
                //                obj1.SrlNo = (item.SlNo);
                //                udt.Add(obj1);
                //            }
                //            IsProcess = EstimateProductInsertUpdate(udt, options);
                //        }
                //        else
                //        {
                //            Message = NumberScheme;
                //        }
                //    }
                //    // list.Add(product);
                //    //}
                //}
                if (updateValues.Update.Count > 0)
                {
                    List<udtStockProducts> udtlist = new List<udtStockProducts>();
                    udtStockProducts obj = null;

                    foreach (var item in updateValues.Update)
                    {
                        if (Convert.ToInt64(item.product_id) > 0)
                        {
                            obj = new udtStockProducts();
                            obj.product_id = Convert.ToInt64(item.product_id);
                            obj.newStock = Convert.ToDecimal(item.newStock);
                            obj.Product_Name = (item.Product_Name);
                            obj.CurrentStock = Convert.ToDecimal(item.CurrentStock);
                            obj.ClosingStock = Convert.ToDecimal(item.ClosingStock);
                            obj.SlNO = (item.SlNO);
                            udtlist.Add(obj);
                        }
                    }

                    if (udtlist.Count > 0)
                    {
                        SaveDataArea = 1;

                        udtlist = udtlist.OrderBy(x => Convert.ToInt64(x.SlNO)).ToList();
                        foreach (var item in udtlist)
                        {
                            udtStockProduct obj1 = new udtStockProduct();
                            obj1.product_id = Convert.ToInt64(item.product_id);
                            obj1.newStock = Convert.ToDecimal(item.newStock);
                            obj1.Product_Name = (item.Product_Name);
                            obj1.CurrentStock = Convert.ToDecimal(item.CurrentStock);
                            obj1.ClosingStock = Convert.ToDecimal(item.ClosingStock);
                            obj1.SlNO = (item.SlNO);
                            udt.Add(obj1);
                        }

                        IsProcess = StockProductInsertUpdate(udt, options);
                    }
                }

                TempData["Count"] = 1;
                TempData.Keep();
                ViewData["Success"] = IsProcess;
                ViewData["Message"] = Message;
            }
            return PartialView("~/Areas/MYSHOP/Views/ShopOpeningStock/GridProductOnepingStock.cshtml", updateValues.Update);
            //return Json(IsProcess, JsonRequestBehavior.AllowGet);
        }

        public Boolean StockProductInsertUpdate(List<udtStockProduct> obj, OpeningStockModel obj2)
        {
            Boolean Success = false;
            try
            {
                DataTable dt_PRODUCTS = new DataTable();
                dt_PRODUCTS = ToDataTable(obj);

                DataTable dt = new DataTable();

                dt = objwar.InsertProductOpeningStock("INSERT", obj2.WreaHouse_id, Convert.ToInt64(Session["userid"]), dt_PRODUCTS);

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

        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }

            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            //put a breakpoint here and check datatable
            return dataTable;
        }

        public ActionResult GetProductListForOpeningStock(string WarehouseID, String WareHouseName)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = objwar.GetProductStockList(WarehouseID);
            }
            catch { }


            return GridViewExtension.ExportToXlsx(GetProductStockListWarehouseWiseExcel(dt, WareHouseName), dt);
        }

        private GridViewSettings GetProductStockListWarehouseWiseExcel(object datatable, String WareHouseName)
        {
            var settings = new GridViewSettings();

            TempData.Keep();
            DataTable dt = (DataTable)datatable;
            settings.Name = "ProductStock_" + dt.Rows[0]["WAREHOUSE_NAME"].ToString();
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "ProductStock_" + dt.Rows[0]["WAREHOUSE_NAME"].ToString();


            foreach (System.Data.DataColumn datacolumn in dt.Columns)
            {
                if (datacolumn.ColumnName == "WAREHOUSE_ID" || datacolumn.ColumnName == "sProducts_Code" || datacolumn.ColumnName == "WAREHOUSE_NAME"
                    || datacolumn.ColumnName == "sProducts_Name" || datacolumn.ColumnName == "QTY")
                {
                    settings.Columns.Add(column =>
                    {
                        if (datacolumn.ColumnName == "WAREHOUSE_ID")
                        {
                            column.Caption = "WAREHOUSE_ID";

                        }
                        else if (datacolumn.ColumnName == "sProducts_Code")
                        {
                            column.Caption = "Product Code";

                        }
                        else if (datacolumn.ColumnName == "WAREHOUSE_NAME")
                        {
                            column.Caption = "Warehouse Name";
                        }
                        else if (datacolumn.ColumnName == "sProducts_Name")
                        {
                            column.Caption = "Product Name";
                        }
                        else if (datacolumn.ColumnName == "QTY")
                        {
                            column.Caption = "Stock";
                        }

                        column.FieldName = datacolumn.ColumnName;

                        if (datacolumn.DataType.FullName == "System.Decimal")
                        {
                            column.PropertiesEdit.DisplayFormatString = "0.00";
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
        public ActionResult GetUploadFilesProductStockSettings()
        {
            string VALUE = "";
            TempData["ProductStockImportLog"] = null;
            if (Request.Files.Count > 0)
            {
                try
                {
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

                      VALUE=  Import_To_Grid(fname, extension, file);
                        //ReadExcel(fname, extension, file);
                    }
                    // Returns message that successfully uploaded  
                    return Json(VALUE);
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

        public String Import_To_Grid(string FilePath, string Extension, HttpPostedFileBase file)
        {

            Boolean Success = false;
            String HasLog = "";
            string conn = string.Empty;

            if (file.FileName.Trim() != "")
            {

                if (Extension.ToUpper() == ".XLS" || Extension.ToUpper() == ".XLSX")
                {
                    DataTable dt = new DataTable();

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


                    DataTable StockTable = new DataTable();
                    if (StockTable != null)
                    {
                        //  workTable = null;
                    }
                    //DataColumn workCol = workTable.Columns.Add("REVISITID", typeof(Int32));
                    //workCol.AllowDBNull = false;
                    //workCol.Unique = true;

                    StockTable.Columns.Add("Warehouse_ID", typeof(string));
                    StockTable.Columns.Add("Product_ID", typeof(string));
                    StockTable.Columns.Add("Warehouse_Name", typeof(string));
                    StockTable.Columns.Add("Product_Name", typeof(string));
                    StockTable.Columns.Add("Stock", typeof(decimal));

                    int r = 0;
                    bool STATUS = true;
                    string STATUS_MESSAGE = "Sucess";
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string warehouse = dt.Rows[0]["Warehouse Name"].ToString().ToLower();
                        foreach (DataRow row in dt.Rows)
                        {
                            string Warehouse_ID = "";
                            string Product_ID = "";
                            string Warehouse_Name = "";
                            string Product_Name = "";
                            decimal Stock = 0;

                            if (warehouse.ToUpper() != row["Warehouse Name"].ToString().ToUpper())
                            {
                                STATUS_MESSAGE = "Invalid Warehouse Name";
                                STATUS = false;
                                break;
                            }

                            if (row["WAREHOUSE_ID"].ToString() != "")
                            {
                                Warehouse_ID = Convert.ToString(row["WAREHOUSE_ID"]);
                            }
                            else
                            {
                                STATUS_MESSAGE = "Invalid Warehouse ID";
                                STATUS = false;
                                break;
                            }

                            if (row["Product Code"] != "")
                            {
                                Product_ID = row["Product Code"].ToString();
                            }
                            else
                            {
                                STATUS_MESSAGE = "Invalid Product Code";
                                STATUS = false;
                                break;
                            }

                            if (row["Warehouse Name"] != "")
                            {
                                Warehouse_Name = row["Warehouse Name"].ToString();
                            }
                            else
                            {
                                STATUS = false;
                                STATUS_MESSAGE = "Warehouse Name Not Found";
                                break;
                            }

                            if (row["Product Name"].ToString() != "")
                            {
                                Product_Name = row["Product Name"].ToString();
                            }
                            else
                            {
                                STATUS = false;
                                STATUS_MESSAGE = "Product Name Not Found";
                                break;
                            }

                            try
                            {
                                Stock = Convert.ToDecimal(row["Stock"].ToString());
                            }
                            catch
                            {
                                STATUS = false;
                                STATUS_MESSAGE = "Invalid Stock";
                                break;
                            }

                            DataRow dr = StockTable.NewRow();
                            dr["Warehouse_ID"] = Warehouse_ID;
                            dr["Product_ID"] = Product_ID;
                            dr["Warehouse_Name"] = Warehouse_Name;
                            dr["Product_Name"] = Product_Name;
                            dr["Stock"] = Stock;
                            //dr["STATUS"] = STATUS;
                            //dr["STATUS_MESSAGE"] = STATUS_MESSAGE;
                            StockTable.Rows.Add(dr);

                        }

                        if (STATUS)
                        {
                            if (StockTable != null && StockTable.Rows.Count > 0)
                            {
                                string userid = Session["userid"].ToString();
                                DataTable dtEmp = objwar.SetProductStockEmport(StockTable, userid);
                                if (dtEmp != null && dtEmp.Rows.Count > 0)
                                {
                                    HasLog = "Import Process Completed!";
                                }
                            }
                        }
                        else
                        {
                            HasLog = STATUS_MESSAGE;
                        }
                    }
                }
            }
            return HasLog;
        }

        public static string GetColumnName(string cellReference)
        {
            // Create a regular expression to match the column name portion of the cell name.
            Regex regex = new Regex("[A-Za-z]+");
            Match match = regex.Match(cellReference);
            return match.Value;
        }

        public static int? GetColumnIndexFromName(string columnName)
        {
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
            else
            {
                return value;
            }

        }
    }
}
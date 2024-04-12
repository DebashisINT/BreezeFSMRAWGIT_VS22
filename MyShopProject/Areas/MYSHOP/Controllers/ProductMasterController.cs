/***************************************************************************************************************
Written by Sanchita for  v2.0.46  on 13/03/2024     New Product Module shall be implemented for ITC Refer: 27289
***********************************************************************************************************/
using BusinessLogicLayer.SalesmanTrack;
using MyShop.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilityLayer;
using DataAccessLayer;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using DevExpress.Web.Mvc;
using DevExpress.Web;
using System.Configuration;
using System.Data.OleDb;
using System.IO;
using DocumentFormat.OpenXml.EMMA;
using Models;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class ProductMasterController : Controller
    {
        // GET: MYSHOP/ProductMaster
        public ActionResult Index()
        {
            EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/ProductMaster/Index");
            ViewBag.CanAdd = rights.CanAdd;
            ViewBag.CanView = rights.CanView;
            ViewBag.CanExport = rights.CanExport;
            ViewBag.CanEdit = rights.CanEdit;
            ViewBag.CanDelete = rights.CanDelete;


            ProductMasterModel Dtls = new ProductMasterModel();

            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("PRC_FSMPRODUCTMASTER");
            proc.AddPara("@ACTION", "GETLISTDATA");
            ds = proc.GetDataSet();

            List<ProductClassList> ProductClassLst = new List<ProductClassList>();
            ProductClassLst = APIHelperMethods.ToModelList<ProductClassList>(ds.Tables[0]);
            Dtls.ProductClassList = ProductClassLst;
            Dtls.ProductClass = 0;

            List<ProductStrengthList> ProductStrengthLst = new List<ProductStrengthList>();
            ProductStrengthLst = APIHelperMethods.ToModelList<ProductStrengthList>(ds.Tables[1]);
            Dtls.ProductStrengthList = ProductStrengthLst;
            Dtls.ProductStrength = 0;

            List<ProductUnitList> ProductUnitLst = new List<ProductUnitList>();
            ProductUnitLst = APIHelperMethods.ToModelList<ProductUnitList>(ds.Tables[2]);
            Dtls.ProductUnitList = ProductUnitLst;

            List<ProductBrandList> ProductBrandLst = new List<ProductBrandList>();
            ProductBrandLst = APIHelperMethods.ToModelList<ProductBrandList>(ds.Tables[3]);
            Dtls.ProductBrandList = ProductBrandLst;
            Dtls.ProductBrand = 0;


            List<ProductStatusList> ProductStatusLst = new List<ProductStatusList>();
            ProductStatusLst = APIHelperMethods.ToModelList<ProductStatusList>(ds.Tables[4]);
            Dtls.ProductStatusList = ProductStatusLst;
            Dtls.ProductStatus = 1;


            return View(Dtls);
        }

        public PartialViewResult _PartialProductGrid(string id, ProductMasterModel model)
        {
            EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/ProductMaster/Index");
            ViewBag.CanAdd = rights.CanAdd;
            ViewBag.CanView = rights.CanView;
            ViewBag.CanExport = rights.CanExport;
            ViewBag.CanEdit = rights.CanEdit;
            ViewBag.CanDelete = rights.CanDelete;

            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("PRC_FSMPRODUCTMASTER");
            proc.AddPara("@ACTION", "GETPRODUCTMASTERLISTDATA");
            proc.AddPara("@USER_ID", Convert.ToString(Session["userid"]));
            proc.AddPara("@IS_PAGELOAD", model.Ispageload);
            ds = proc.GetDataSet();

            return PartialView(GetProductList());
        }

        public IEnumerable GetProductList()
        {
            string connectionString = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"]);
            ReportsDataContext dc = new ReportsDataContext(connectionString);
            
            int userid = Convert.ToInt32(Session["userid"]);

            var q = from d in dc.FTS_ProductMasterLists
                    where d.USERID == userid
                    orderby d.sProducts_ID descending
                    select d;
            
            return q;

        }

        public JsonResult SaveProduct(string id, string ProductCode, string ProductName, string ProductMRP, string ProductPrice,
            string ProductClass, string ProductStrength, string ProductUnit, string ProductBrand, string ProductStatus)
        {
            int output = 0;
            string Userid = Convert.ToString(Session["userid"]);

            ProcedureExecute proc;
            int rtrnvalue = 0;
            try
            {
                using (proc = new ProcedureExecute("PRC_FSMPRODUCTMASTER"))
                {
                    proc.AddVarcharPara("@PRODID", 100, id);
                    if (id == "0")
                        proc.AddVarcharPara("@ACTION", 100, "ADDPRODUCT");
                    else
                        proc.AddVarcharPara("@ACTION", 100, "UPDATEPRODUCT");
                    proc.AddVarcharPara("@ProductCode", 100, ProductCode);
                    proc.AddVarcharPara("@ProductName", 100, ProductName);
                    proc.AddVarcharPara("@ProductMRP", 100, ProductMRP);
                    proc.AddVarcharPara("@ProductPrice", 100, ProductPrice);
                    proc.AddVarcharPara("@ProductClass", 100, ProductClass);
                    proc.AddVarcharPara("@ProductStrength", 100, ProductStrength);
                    proc.AddVarcharPara("@ProductUnit", 100, ProductUnit);
                    proc.AddVarcharPara("@ProductBrand", 100, ProductBrand);
                    proc.AddVarcharPara("@ProductStatus", 100, ProductStatus);

                    proc.AddVarcharPara("@USER_ID", 100, Convert.ToString(Session["userid"]));
                    proc.AddVarcharPara("@RETURN_VALUE", 50, "", QueryParameterDirection.Output);
                    int i = proc.RunActionQuery();
                    rtrnvalue = Convert.ToInt32(proc.GetParaValue("@RETURN_VALUE"));
                    output =  rtrnvalue;


                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                proc = null;
            }



            return Json(output, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditProduct(string id)
        {
            DataTable output = new DataTable();
            
            ProcedureExecute proc;
            
            DataTable dt = new DataTable();
            try
            {
                using (proc = new ProcedureExecute("PRC_FSMPRODUCTMASTER"))
                {
                    proc.AddVarcharPara("@PRODID", 100, id);
                    proc.AddVarcharPara("@ACTION", 100, "EDITPRODUCT");
                    output = proc.GetTable();
                    
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                proc = null;
            }

            if (output.Rows.Count > 0)
            {
                return Json(new { 
                    ProductCode = Convert.ToString(output.Rows[0]["sProducts_Code"]),
                    ProductMRP = Convert.ToString(output.Rows[0]["sProduct_MRP"]),
                    ProductName = Convert.ToString(output.Rows[0]["sProducts_Name"]),
                    ProductPrice = Convert.ToString(output.Rows[0]["sProduct_Price"]),
                    ProductClass = Convert.ToString(output.Rows[0]["ProductClass_Code"]),
                    ProductStrength = Convert.ToString(output.Rows[0]["sProducts_Size"]),
                    ProductUnit = Convert.ToString(output.Rows[0]["sProducts_TradingLotUnit"]),
                    ProductBrand = Convert.ToString(output.Rows[0]["sProducts_Brand"]),
                    ProductStatus = Convert.ToString(output.Rows[0]["sProduct_Status"])
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { code = "", name = "" }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult DeleteProduct(string ID)
        {
            int output = 0;
            
            ProcedureExecute proc;
            int rtrnvalue = 0;
            DataTable dt = new DataTable();
            try
            {
                using (proc = new ProcedureExecute("PRC_FSMPRODUCTMASTER"))
                {
                    proc.AddVarcharPara("@PRODID", 100, ID);
                    proc.AddVarcharPara("@ACTION", 100, "DELETEPRODUCT");
                    proc.AddVarcharPara("@RETURN_VALUE", 50, "", QueryParameterDirection.Output);
                    int i = proc.RunActionQuery();
                    rtrnvalue = Convert.ToInt32(proc.GetParaValue("@RETURN_VALUE"));
                    output= rtrnvalue;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                proc = null;
            }

            return Json(output, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DownloadFormat()
        {
            string FileName = "ProductsMasterList.xlsx";
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "image/jpeg";
            response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ";");
            response.TransmitFile(Server.MapPath("~/Commonfolder/ProductsMasterList.xlsx"));
            response.Flush();
            response.End();

            return null;
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
                    string conString = string.Empty;
                    conString = ConfigurationManager.AppSettings["ExcelConString"];
                    conString = string.Format(conString, FilePath);
                    using (OleDbConnection excel_con = new OleDbConnection(conString))
                    {
                        excel_con.Open();
                        string sheet1 = "PRODUCTLIST$"; //ī;

                        using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
                        {
                            oda.Fill(dt);
                        }
                        excel_con.Close();
                    }

                    // }
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        //Mantis Issue 24674
                        //** New Datatable included to resolve no. format for Phone numbers. State and Contact blank check was implemented to filter out blank rows from the excel Sheet. **//
                        DataTable dtExcelData = new DataTable();
                        dtExcelData.Columns.Add("Item Code", typeof(string));
                        dtExcelData.Columns.Add("Item Name", typeof(string));
                        dtExcelData.Columns.Add("Item Class/Category", typeof(string));
                        dtExcelData.Columns.Add("Item Brand", typeof(string));
                        dtExcelData.Columns.Add("Item Strangth", typeof(string));
                        dtExcelData.Columns.Add("Item Price", typeof(decimal));
                        dtExcelData.Columns.Add("Item MRP", typeof(decimal));
                        dtExcelData.Columns.Add("Item Status", typeof(string));
                        dtExcelData.Columns.Add("Item Unit", typeof(string));
                        foreach (DataRow row in dt.Rows)
                        {
                            if (Convert.ToString(row["Item Code*"]) != "" && Convert.ToString(row["Item Name*"]) != "")
                            {

                                if (Convert.ToString(row["Item Price*"]) == "")
                                    row["Item Price*"] = "0.00";

                                if (Convert.ToString(row["Item MRP*"]) == "")
                                    row["Item MRP*"] = "0.00";


                                dtExcelData.Rows.Add(Convert.ToString(row["Item Code*"]), Convert.ToString(row["Item Name*"]), 
                                                Convert.ToString(row["Item Class/Category*"]), 
                                                Convert.ToString(row["Item Brand*"]), Convert.ToString(row["Item Strangth*"]),
                                                Convert.ToString(row["Item Price*"]), Convert.ToString(row["Item MRP*"]), 
                                                Convert.ToString(row["Item Status (Active/Dormant)*"]), Convert.ToString(row["Item Unit*"]));
                            }

                        }
                        
                        try
                        {
                           TempData["ProductImportLog"] = dtExcelData;
                           TempData.Keep();

                            DataTable dtCmb = new DataTable();
                            ProcedureExecute proc = new ProcedureExecute("PRC_FSMPRODUCTMASTER");
                            proc.AddPara("@ACTION", "IMPORTPRODUCT");
                            proc.AddPara("@IMPORT_TABLE", dtExcelData);
                            proc.AddPara("@USER_ID", Convert.ToInt32(Session["userid"]));
                            dtCmb = proc.GetTable();

                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            }
            return HasLog;
        }

        public ActionResult ExporProductMasterList(int type)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetDoctorBatchGridViewSettings(), GetProductList());
                case 2:
                    return GridViewExtension.ExportToXlsx(GetDoctorBatchGridViewSettings(), GetProductList());
                case 3:
                    return GridViewExtension.ExportToXls(GetDoctorBatchGridViewSettings(), GetProductList());
                case 4:
                    return GridViewExtension.ExportToRtf(GetDoctorBatchGridViewSettings(), GetProductList());
                case 5:
                    return GridViewExtension.ExportToCsv(GetDoctorBatchGridViewSettings(), GetProductList());
                default:
                    break;
            }
            return null;
        }

        private GridViewSettings GetDoctorBatchGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "gridPartyDetails";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Product Master Details";

            settings.Columns.Add(x =>
            {
                x.FieldName = "sProducts_Code";
                x.Caption = "Item Code";
                x.VisibleIndex = 1;
                x.ExportWidth = 200;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "sProducts_Name";
                x.Caption = "Item Name";
                x.VisibleIndex = 2;
                x.ExportWidth = 250;
            });


            settings.Columns.Add(x =>
            {
                x.FieldName = "ProductClass_Name";
                x.Caption = "Item Class/Category";
                x.VisibleIndex = 3;
                x.ExportWidth = 250;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Brand_Name";
                x.Caption = "Item Brand";
                x.VisibleIndex = 4;
                x.ExportWidth = 200;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Size_Name";
                x.Caption = "Item Strength";
                x.VisibleIndex = 5;
                x.ExportWidth = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "sProduct_Price";
                x.Caption = "Item Price";
                x.VisibleIndex = 6;
                x.ExportWidth = 100;
                x.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "sProduct_MRP";
                x.Caption = "Item MRP";
                x.VisibleIndex = 7;
                x.ExportWidth = 100;
                x.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "sProduct_Status";
                x.Caption = "Item Status";
                x.VisibleIndex = 8;
                x.ExportWidth = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "UOM_Name";
                x.Caption = "Item Unit";
                x.VisibleIndex = 9;
                x.ExportWidth = 100;
            });


            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

        [HttpPost]
        public JsonResult ProductImportLog(string Fromdt, String ToDate)
        {
            string output_msg = string.Empty;
            try
            {
                string datfrmat = Fromdt.Split('-')[2] + '-' + Fromdt.Split('-')[1] + '-' + Fromdt.Split('-')[0];
                string dattoat = ToDate.Split('-')[2] + '-' + ToDate.Split('-')[1] + '-' + ToDate.Split('-')[0];

                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_FSMPRODUCTMASTER");
                proc.AddPara("@ACTION", "GETPRODUCTIMPORTLOG");
                proc.AddPara("@FromDate", datfrmat);
                proc.AddPara("@ToDate", dattoat);
                dt = proc.GetTable();

                if (dt != null && dt.Rows.Count > 0)
                {
                    TempData["ProductImportLog"] = dt;
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
        public ActionResult ImportLog()
        {
            List<ProductImportLog> list = new List<ProductImportLog>();
            DataTable dt = new DataTable();
            try
            {
                dt = (DataTable)TempData["ProductImportLog"];
                if (dt != null && dt.Rows.Count > 0)
                {
                    ProductImportLog data = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        data = new ProductImportLog();
                        data.ItemCode = Convert.ToString(row["ItemCode"]);
                        data.ItemName = Convert.ToString(row["ItemName"]);
                        data.ItemClass = Convert.ToString(row["ItemClass"]);
                        data.ItemBrand = Convert.ToString(row["ItemBrand"]);
                        data.ItemStrangth = Convert.ToString(row["ItemStrangth"]);
                        data.ItemPrice = Convert.ToDecimal(row["ItemPrice"]);
                        data.ItemMRP = Convert.ToDecimal(row["ItemMRP"]);
                        data.ItemStatus = Convert.ToString(row["ItemStatus"]);

                        data.ItemUnit = Convert.ToString(row["ItemUnit"]);
                        data.ImportStatus = Convert.ToString(row["ImportStatus"]);
                        data.ImportMsg = Convert.ToString(row["ImportMsg"]);
                        data.ImportDate = Convert.ToDateTime(row["ImportDate"]);
                        data.UpdatedBy = Convert.ToString(row["UpdatedBy"]);

                        list.Add(data);
                    }
                    TempData["ProductImportLog"] = dt;
                }
            }
            catch (Exception ex)
            {

            }
            TempData.Keep();
            return PartialView(list);
        }
    }
    
}
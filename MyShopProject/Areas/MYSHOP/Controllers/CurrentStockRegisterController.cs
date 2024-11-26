/***************************************************************************************
 * Written by Sanchita on 24/09/2024 for V2.0.49    
 * 27707: In Item Current Stock there shall be a stock import option. The sample import file is attached
 ****************************************************************************************/
using BusinessLogicLayer;
using BusinessLogicLayer.SalesmanTrack;
using DataAccessLayer;
using DevExpress.Utils;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Models;
using MyShop.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml;
using UtilityLayer;
using static MyShop.Models.CurrentStockImportLogModel;



namespace MyShop.Areas.MYSHOP.Controllers
{
    public class CurrentStockRegisterController : Controller
    {
        // GET: MYSHOP/CurrentStockRegister
        public ActionResult Index()
        {
            EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/CurrentStockRegister/Index");
            ViewBag.CanAdd = rights.CanAdd;
            ViewBag.CanView = rights.CanView;
            ViewBag.CanExport = rights.CanExport;
            ViewBag.CanReassign = rights.CanReassign;
            ViewBag.CanAssign = rights.CanAssign;
            ViewBag.CanBulkUpdate = rights.CanBulkUpdate;

            TempData["FromManualLog"] = null;
            TempData["CurrentStockImportLog"] = null;

            CurrentStockImportLogModel Dtls = new CurrentStockImportLogModel();

            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSINSERTUPDATECURRENTSTOCK");
            proc.AddPara("@ACTION", "GetDropdownBindData");
            ds = proc.GetDataSet();


            if (ds != null)
            {
                List<StockBranchList> StockBranchList = new List<StockBranchList>();
                StockBranchList = APIHelperMethods.ToModelList<StockBranchList>(ds.Tables[0]);
                Dtls.StockBranchList = StockBranchList;

            }



            return View(Dtls);
        }

        public ActionResult DownloadFormat()
        {
            string FileName = "CurrentStockList.xlsx";
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "image/jpeg";
            response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ";");
            response.TransmitFile(Server.MapPath("~/Commonfolder/CurrentStockList.xlsx"));
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
            TempData["FromManualLog"] = null;
            TempData["CurrentStockImportLog"] = null;

            if (file.FileName.Trim() != "")
            {
                if (Extension.ToUpper() == ".XLS" || Extension.ToUpper() == ".XLSX")
                {
                    DataTable dt = new DataTable();

                    //DataTable dtExcelData = new DataTable();
                    string conString = string.Empty;
                    conString = ConfigurationManager.AppSettings["ExcelConString"];
                    conString = string.Format(conString, FilePath);
                    using (OleDbConnection excel_con = new OleDbConnection(conString))
                    {
                        excel_con.Open();
                        string sheet1 = "List$"; //ī;

                        using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
                        {
                            oda.Fill(dt);
                        }
                        excel_con.Close();
                    }

                    // }
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        //** New Datatable included to resolve no. format for Phone numbers. State and Contact blank check was implemented to filter out blank rows from the excel Sheet. **//
                        DataTable dtExcelData = new DataTable();
                        dtExcelData.Columns.Add("Branch", typeof(string));
                        dtExcelData.Columns.Add("ShopName", typeof(string));
                        dtExcelData.Columns.Add("Code", typeof(string));
                        dtExcelData.Columns.Add("ContactNumber", typeof(string));
                        dtExcelData.Columns.Add("Shoptype", typeof(string));
                        dtExcelData.Columns.Add("CurrentStockDate", typeof(DateTime));
                        dtExcelData.Columns.Add("ProductCode", typeof(string));
                        dtExcelData.Columns.Add("ProductName", typeof(string));
                        dtExcelData.Columns.Add("Quantity", typeof(decimal));
                        
                        foreach (DataRow row in dt.Select("[Product Code*]<>''"))
                        {
                            if (Convert.ToString(row["Branch*"]) == "")
                            {
                                row["Branch*"] = "0";
                            }

                            if (Convert.ToString(row["Shop type*"]) == "")
                            {
                                row["Shop type*"] = "0";
                            }

                            if (Convert.ToString(row["Quantity*"]) == "")
                            {
                                row["Quantity*"] = "0";
                            }


                            dtExcelData.Rows.Add(Convert.ToString(row["Branch*"]), Convert.ToString(row["Shop Name"]), Convert.ToString(row["Code"]), 
                            Convert.ToString(row["Contact Number*"]), Convert.ToString(row["Shop type*"]), Convert.ToString(row["Current Stock Date*"]), 
                            Convert.ToString(row["Product Code*"]), Convert.ToString(row["Product Name*"]), Convert.ToString(row["Quantity*"]) );

                        }

                        try
                        {
                            TempData["CurrentStockImportLog"] = dtExcelData;
                            TempData.Keep();

                            DataTable dtCmb = new DataTable();
                            ProcedureExecute proc = new ProcedureExecute("PRC_FTSINSERTUPDATECURRENTSTOCK");
                            proc.AddPara("@ACTION", "IMPORTCURRENTSTOCK");
                            proc.AddPara("@IMPORT_TABLE", dtExcelData);
                            proc.AddPara("@User_Id", Convert.ToInt32(Session["userid"]));
                            dtCmb = proc.GetTable();

                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    }
                }
            }
            return HasLog;
        }
        public ActionResult CurrentStockImportLog()
        {
            List<CurrentStockImportLogModel> list = new List<CurrentStockImportLogModel>();
            DataTable dt = new DataTable();
            try
            {
                if (TempData["CurrentStockImportLog"] != null)
                {
                    if(TempData["FromManualLog"] != null && Convert.ToString(TempData["FromManualLog"]) == "1"){
                        dt = (DataTable)TempData["CurrentStockImportLog"];
                    }
                    else
                    {
                        DataTable dtCmb = new DataTable();
                        ProcedureExecute proc = new ProcedureExecute("PRC_FTSINSERTUPDATECURRENTSTOCK");
                        proc.AddPara("@Action", "SHOWIMPORTLOG");
                        proc.AddPara("@IMPORT_TABLE", (DataTable)TempData["CurrentStockImportLog"]);
                        proc.AddPara("@User_Id", Convert.ToInt32(Session["userid"]));
                        dt = proc.GetTable();
                    }
                    
                    TempData.Keep();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        CurrentStockImportLogModel data = null;
                        foreach (DataRow row in dt.Rows)
                        {
                            data = new CurrentStockImportLogModel();
                            data.Branch = Convert.ToString(row["Branch"]);
                            data.ShopName = Convert.ToString(row["ShopName"]);
                            data.Code = Convert.ToString(row["Code"]);
                            data.ContactNumber = Convert.ToString(row["ContactNumber"]);
                            data.Shoptype = Convert.ToString(row["Shoptype"]);
                            data.CurrentStockDate = Convert.ToString(row["CurrentStockDate"]);
                            data.ProductCode = Convert.ToString(row["ProductCode"]);
                            data.ProductName = Convert.ToString(row["ProductName"]);
                            data.Quantity = Convert.ToString(row["Quantity"]);
                            data.ImportStatus = Convert.ToString(row["ImportStatus"]);
                            data.ImportMsg = Convert.ToString(row["ImportMsg"]);
                            data.ImportDate = Convert.ToString(row["ImportDate"]);
                            data.CreateUser = Convert.ToString(row["CreateUser"]);
                           
                            list.Add(data);
                        }
                    }
                    //TempData["EnquiriesImportLog"] = dt;
                }

            }
            catch (Exception ex)
            {

            }
            TempData.Keep();
            return PartialView(list);
        }

        [HttpPost]
        public JsonResult CurrentStockImportManualLog(string Fromdt, String ToDate)
        {
            string output_msg = string.Empty;
            try
            {
                string datfrmat = Fromdt.Split('-')[2] + '-' + Fromdt.Split('-')[1] + '-' + Fromdt.Split('-')[0];
                string dattoat = ToDate.Split('-')[2] + '-' + ToDate.Split('-')[1] + '-' + ToDate.Split('-')[0];

                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_FTSINSERTUPDATECURRENTSTOCK");
                proc.AddPara("@ACTION", "GETCRMCONTACTIMPORTLOG");
                proc.AddPara("@FromDate", datfrmat);
                proc.AddPara("@ToDate", dattoat);
                dt = proc.GetTable();

                if (dt != null && dt.Rows.Count > 0)
                {
                    TempData["CurrentStockImportLog"] = dt;
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



        public ActionResult PartialCurrentStockGridList(CRMContactModel model)
        {
            try
            {
                EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/CurrentStockRegister/Index");
                ViewBag.CanAdd = rights.CanAdd;
                ViewBag.CanView = rights.CanView;
                ViewBag.CanExport = rights.CanExport;
                ViewBag.CanBulkUpdate = rights.CanBulkUpdate;

                string ContactFrom = "";
                int i = 1;

                
                string Is_PageLoad = string.Empty;

                if (model.Is_PageLoad == "Ispageload")
                {
                    Is_PageLoad = "is_pageload";

                }



                string user_id = Convert.ToString(Session["userid"]);

                string action = string.Empty;
                DataTable formula_dtls = new DataTable();
                DataSet dsInst = new DataSet();

                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_FTSINSERTUPDATECURRENTSTOCK");
                proc.AddPara("@ACTION", "GETLISTINGDATA");
                proc.AddPara("@IS_PAGELOAD", Is_PageLoad);
                proc.AddPara("@user_id", Convert.ToInt32(user_id));
                dt = proc.GetTable();

                model.Is_PageLoad = "Ispageload";

                return PartialView("PartialCurrentStockGridList", GetCurrentStockDetails(Is_PageLoad));

            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        public IEnumerable GetCurrentStockDetails(string Is_PageLoad)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);

            ////////DataTable dtColmn = GetPageRetention(Session["userid"].ToString(), "CRM Contact");
            ////////if (dtColmn != null && dtColmn.Rows.Count > 0)
            ////////{
            ////////    ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            ////////}

            if (Is_PageLoad != "is_pageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FSM_CURRENTSTOCK_LISTINGs
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.SEQ 
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FSM_CURRENTSTOCK_LISTINGs
                        where d.USERID == Convert.ToInt32(Userid) && d.SEQ == 11111111119
                        orderby d.SEQ 
                        select d;
                return q;
            }


        }

        [HttpPost]
        public ActionResult SaveCurrentStock(AddCurrentStockData data)
        {
            try
            {
                string CurrentStockDate = null;
                
                if (data.CurrentStockDate != null && data.CurrentStockDate != "01-01-0100")
                {
                    CurrentStockDate = data.CurrentStockDate.Split('-')[2] + '-' + data.CurrentStockDate.Split('-')[1] + '-' + data.CurrentStockDate.Split('-')[0];
                }


                string user_id = Convert.ToString(Session["userid"]);

                string rtrnvalue = "";
                string Userid = Convert.ToString(Session["userid"]);
                ProcedureExecute proc = new ProcedureExecute("PRC_FTSINSERTUPDATECURRENTSTOCK");
                proc.AddPara("@ACTION", data.Action);
                proc.AddPara("@STOCKID", data.StockId);
                proc.AddPara("@BRANCHID", data.BranchID);
                proc.AddPara("@SHOPCODE", data.ShopCode);
                proc.AddPara("@PRODUCTID", data.ProductID);
                proc.AddPara("@QUANTITY", data.Quantity);
                proc.AddPara("@CURRENTSTOCKDATE", CurrentStockDate);
                proc.AddPara("@user_id", user_id);
                proc.AddVarcharPara("@RETURN_VALUE", 500, "", QueryParameterDirection.Output);
                int k = proc.RunActionQuery();
                rtrnvalue = Convert.ToString(proc.GetParaValue("@RETURN_VALUE"));
                return Json(rtrnvalue, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult EditCurrentStock(String stockid)
        {
            try
            {
                AddCurrentStockData ret = new AddCurrentStockData();

                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_FTSINSERTUPDATECURRENTSTOCK");
                proc.AddPara("@ACTION", "EDITCURRENTSTOCK");
                proc.AddPara("@STOCKID", stockid);
                dt = proc.GetTable();

                if (dt != null && dt.Rows.Count > 0)
                {
                    ret.BranchID = dt.Rows[0]["STOCK_BRANCHID"].ToString();
                    ret.ShopCode = dt.Rows[0]["STOCK_SHOPCODE"].ToString();
                    ret.ProductID = dt.Rows[0]["STOCK_PRODUCTID"].ToString();
                    ret.CurrentStockDate = dt.Rows[0]["STOCK_CURRENTDATE"].ToString();
                    ret.Quantity = dt.Rows[0]["STOCK_PRODUCTQTY"].ToString();
                    ret.ShopName = dt.Rows[0]["STOCK_SHOPNAME"].ToString();
                    ret.ProductName = dt.Rows[0]["STOCK_PRODUCTNAME"].ToString();

                }
                return Json(ret, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        [HttpPost]
        public JsonResult DeleteCurrentStock(string StockId)
        {
            string output_msg = string.Empty;
            try
            {
                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_FTSINSERTUPDATECURRENTSTOCK");
                proc.AddPara("@ACTION", "DELETECURRENTSTOCK");
                proc.AddPara("@STOCKID", StockId);
                proc.AddVarcharPara("@RETURN_VALUE", 500, "", QueryParameterDirection.Output);
                dt = proc.GetTable();

                output_msg = Convert.ToString(proc.GetParaValue("@RETURN_VALUE"));
                
            }
            catch (Exception ex)
            {
                output_msg = "Please try again later";
            }

            return Json(output_msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExporRegisterList(int type)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetDoctorBatchGridViewSettings(), GetCurrentStockDetails(""));
                case 2:
                    return GridViewExtension.ExportToXlsx(GetDoctorBatchGridViewSettings(), GetCurrentStockDetails(""));
                case 3:
                    return GridViewExtension.ExportToXls(GetDoctorBatchGridViewSettings(), GetCurrentStockDetails(""));
                case 4:
                    return GridViewExtension.ExportToRtf(GetDoctorBatchGridViewSettings(), GetCurrentStockDetails(""));
                case 5:
                    return GridViewExtension.ExportToCsv(GetDoctorBatchGridViewSettings(), GetCurrentStockDetails(""));
                default:
                    break;
            }
            return null;
        }

        private GridViewSettings GetDoctorBatchGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "gridCurrentStock";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Current Stock Details";

            settings.Columns.Add(x =>
            {
                x.FieldName = "SEQ";
                x.Caption = "Sr. No";
                x.VisibleIndex = 1;
                x.ExportWidth = 20;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "BRANCH";
                x.Caption = "Branch";
                x.VisibleIndex = 2;
                x.ExportWidth = 200;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "SHOPNAME";
                x.Caption = "Shop Name";
                x.VisibleIndex = 3;
                x.ExportWidth = 200;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CODE";
                x.Caption = "Code";
                x.VisibleIndex = 4;
                x.ExportWidth = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CONTACTNUMBER";
                x.Caption = "Contact Number";
                x.VisibleIndex = 5;
                x.ExportWidth = 150;
            });
            //Mantis Issue 24928
            settings.Columns.Add(x =>
            {
                x.FieldName = "SHOPTYPE";
                x.Caption = "Shop type";
                x.VisibleIndex = 6;
                x.ExportWidth = 250;
            });

           
            settings.Columns.Add(x =>
            {
                x.FieldName = "CURRENTSTOCKDATE";
                x.Caption = "Current Stock Date";
                x.VisibleIndex = 7;
                x.ExportWidth = 100;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
            });
            

            settings.Columns.Add(x =>
            {
                x.FieldName = "PRODUCTCODE";
                x.Caption = "Product Code";
                x.VisibleIndex = 8;
                x.ExportWidth = 150;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "PRODUCTNAME";
                x.Caption = "Product Name";
                x.VisibleIndex = 9;
                x.ExportWidth = 100;
            });

           
            settings.Columns.Add(x =>
            {
                x.FieldName = "QUANTITY";
                x.Caption = "Quantity";
                x.VisibleIndex = 10;
                x.ExportWidth = 150;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "QUANTITY_BAL";
                x.Caption = "Bal Quantity";
                x.VisibleIndex = 11;
                x.ExportWidth = 150;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.PropertiesEdit.DisplayFormatString = "0.00";
            });



            settings.Columns.Add(x =>
            {
                x.FieldName = "CREATED_DATE";
                x.Caption = "Created Date";
                x.VisibleIndex = 12;
                x.ExportWidth = 100;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "CREATED_BY";
                x.Caption = "Created By";
                x.VisibleIndex = 13;
                x.ExportWidth = 100;
            });


            settings.Columns.Add(x =>
            {
                x.FieldName = "MODIFIED_DATE";
                x.Caption = "Modified Date";
                x.VisibleIndex = 14;
                x.ExportWidth = 120;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "MODIFIED_BY";
                x.Caption = "Modified By";
                x.VisibleIndex = 15;
                x.ExportWidth = 100;
            });
            

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

    }
}



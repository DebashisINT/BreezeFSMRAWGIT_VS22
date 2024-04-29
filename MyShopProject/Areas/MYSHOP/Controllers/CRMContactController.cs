/***************************************************************************************
 * Written by Sanchita on 24/11/2023 for V2.0.43    A new design page is required as Contact (s) under CRM menu. 
 *                                                  Mantis: 27034 
 * 1.0      v2.0.47      19/04/2024      Sanchita    0027384: Contact Module issues in Portal
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

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class CRMContactController : Controller
    {
        // GET: MYSHOP/CRMContact
        NotificationBL notificationbl = new NotificationBL();
        DBEngine odbengine = new DBEngine();

        public ActionResult Index()
        {
            CRMContactModel Dtls = new CRMContactModel();
           
            Dtls.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
            Dtls.Todate = DateTime.Now.ToString("dd-MM-yyyy");
            Dtls.Is_PageLoad = "Ispageload";
            Dtls.user_id = Convert.ToString(Session["userid"]);

            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSContactListingShow");
            proc.AddPara("@ACTION", "GetDropdownBindData");
            ds = proc.GetDataSet();
            
            if (ds != null)
            {
                // Company
                List<CompanyList> CompanyList = new List<CompanyList>();
                CompanyList = APIHelperMethods.ToModelList<CompanyList>(ds.Tables[0]);
                Dtls.CompanyList = CompanyList;

                // Assign To
                List<AssignToList> AssignToList = new List<AssignToList>();
                AssignToList = APIHelperMethods.ToModelList<AssignToList>(ds.Tables[1]);
                Dtls.AssignToList = AssignToList;

                // Type
                List<TypeList> TypeList = new List<TypeList>();
                TypeList = APIHelperMethods.ToModelList<TypeList>(ds.Tables[2]);
                Dtls.TypeList = TypeList;

                // Status
                List<StatusList> StatusList = new List<StatusList>();
                StatusList = APIHelperMethods.ToModelList<StatusList>(ds.Tables[3]);
                Dtls.StatusList = StatusList;

                // Source
                List<SourceList> SourceList = new List<SourceList>();
                SourceList = APIHelperMethods.ToModelList<SourceList>(ds.Tables[4]);
                Dtls.SourceList = SourceList;

                // Stage
                List<StageList> StageList = new List<StageList>();
                StageList = APIHelperMethods.ToModelList<StageList>(ds.Tables[5]);
                Dtls.StageList = StageList;

                // Reference
                //List<ReferenceList> ReferenceList = new List<ReferenceList>();
                //ReferenceList = APIHelperMethods.ToModelList<ReferenceList>(ds.Tables[5]);
                //Dtls.ReferenceList = ReferenceList;

            }

            EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/CRMContact/Index");
            ViewBag.CanAdd = rights.CanAdd;
            ViewBag.CanView = rights.CanView;
            ViewBag.CanExport = rights.CanExport;
            ViewBag.CanReassign = rights.CanReassign;
            ViewBag.CanAssign = rights.CanAssign;
            ViewBag.CanBulkUpdate = rights.CanBulkUpdate;

            return View(Dtls);
        }

        public ActionResult GetContactFrom()
        {
            try
            {
                List<GetEnquiryFrom> modelEnquiryFrom = new List<GetEnquiryFrom>();

                DataTable dtEnquiryFrom = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_FTSContactListingShow");
                proc.AddPara("@ACTION_TYPE", "GetContactFrom");
                dtEnquiryFrom = proc.GetTable();

                modelEnquiryFrom = APIHelperMethods.ToModelList<GetEnquiryFrom>(dtEnquiryFrom);

                return PartialView("~/Areas/MYSHOP/Views/CRMEnquiries/_EnquiryFromPartial.cshtml", modelEnquiryFrom);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult PartialCRMContactGridList(CRMContactModel model)
        {
            try
            {
                EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/CRMContact/Index");
                ViewBag.CanAdd = rights.CanAdd;
                ViewBag.CanView = rights.CanView;
                ViewBag.CanExport = rights.CanExport;
                ViewBag.CanReassign = rights.CanReassign;
                ViewBag.CanAssign = rights.CanAssign;
                ViewBag.CanBulkUpdate = rights.CanBulkUpdate;
               
                string ContactFrom = "";
                int i = 1;

                if (model.ContactFromDesc != null && model.ContactFromDesc.Count > 0)
                {
                    foreach (string item in model.ContactFromDesc)
                    {
                        if (i > 1)
                            ContactFrom = ContactFrom + "," + item;
                        else
                            ContactFrom = item;
                        i++;
                    }

                }


                string Is_PageLoad = string.Empty;

                if (model.Is_PageLoad == "Ispageload")
                {
                    Is_PageLoad = "is_pageload";

                }

                string datfrmat = model.Fromdate.Split('-')[2] + '-' + model.Fromdate.Split('-')[1] + '-' + model.Fromdate.Split('-')[0];
                string dattoat = model.Todate.Split('-')[2] + '-' + model.Todate.Split('-')[1] + '-' + model.Todate.Split('-')[0];


                GetCRMContactListing(ContactFrom, datfrmat, dattoat, Is_PageLoad);

                model.Is_PageLoad = "Ispageload";

                return PartialView("PartialCRMContactGridList", GetCRMContactDetails(Is_PageLoad));

            }
            catch (Exception ex)
            {
                throw ex;
              
            }

        }

        public void GetCRMContactListing(string ContactFrom, string FromDate, string ToDate, string Is_PageLoad)
        {
            string user_id = Convert.ToString(Session["userid"]);

            string action = string.Empty;
            DataTable formula_dtls = new DataTable();
            DataSet dsInst = new DataSet();

            try
            {
                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_FTSContactListingShow");
                proc.AddPara("@ACTION", "GETLISTINGDATA");
                proc.AddPara("@IS_PAGELOAD", Is_PageLoad);
                proc.AddPara("@USERID", Convert.ToInt32(user_id));
                proc.AddPara("@CONTACTSFROM", ContactFrom);
                proc.AddPara("@FROMDATE", FromDate);
                proc.AddPara("@TODATE", ToDate);
                dt = proc.GetTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable GetCRMContactDetails(string Is_PageLoad)
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
                var q = from d in dc.CRM_CONTACT_LISTINGs
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.SEQ descending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.CRM_CONTACT_LISTINGs
                        where d.USERID == Convert.ToInt32(Userid) && d.SEQ == 11111111119
                        orderby d.SEQ descending
                        select d;
                return q;
            }


        }

        [HttpPost]
        public ActionResult AddCRMContact(AddCrmContactData data)
        {
            try
            {
                string DOB = null;
                string Anniversary = null;
                string NextFollowDate = null;

                if (data.DOB != null && data.DOB != "01-01-0100")
                {
                    DOB = data.DOB.Split('-')[2] + '-' + data.DOB.Split('-')[1] + '-' + data.DOB.Split('-')[0];
                }

                if (data.Anniversarydate != null && data.Anniversarydate != "01-01-0100")
                {
                    Anniversary = data.Anniversarydate.Split('-')[2] + '-' + data.Anniversarydate.Split('-')[1] + '-' + data.Anniversarydate.Split('-')[0];
                }

                if (data.NextFollowDate != null && data.NextFollowDate != "01-01-0100")
                {
                    NextFollowDate = data.NextFollowDate.Split('-')[2] + '-' + data.NextFollowDate.Split('-')[1] + '-' + data.NextFollowDate.Split('-')[0];
                }

                
                string rtrnvalue = "";
                string Userid = Convert.ToString(Session["userid"]);
                ProcedureExecute proc = new ProcedureExecute("PRC_FTSInsertUpdateCRMContact");
                proc.AddPara("@ACTION", data.Action);
                proc.AddPara("@ShopCode", data.shop_code);
                proc.AddPara("@FirstName", data.FirstName);
                proc.AddPara("@LastName", data.LastName);
                proc.AddPara("@PhoneNo", data.PhoneNo);
                proc.AddPara("@Email", data.Email);
                proc.AddPara("@Address", data.Address);
                proc.AddPara("@DOB", DOB);
                proc.AddPara("@Anniversarydate", Anniversary);
                proc.AddPara("@JobTitle", data.JobTitle);
                proc.AddPara("@CompanyId", data.CompanyId);
                proc.AddPara("@AssignedTo", data.AssignedTo);
                proc.AddPara("@TypeId", data.TypeId);
                proc.AddPara("@StatusId", data.StatusId);
                proc.AddPara("@SourceId", data.SourceId);
                proc.AddPara("@ReferenceId", data.ReferenceId);
                proc.AddPara("@StageId", data.StageId);
                proc.AddPara("@Remarks", data.Remarks);
                proc.AddPara("@ExpSalesValue", data.ExpSalesValue);
                proc.AddPara("@NextFollowDate", NextFollowDate);
                proc.AddPara("@Active", data.Active);
                proc.AddPara("@user_id", data.user_id);
                // Rev 1.0
                proc.AddPara("@Login_UserId", Convert.ToInt32(Session["userid"]));
                proc.AddPara("@Pincode", data.Pincode);
                proc.AddPara("@WhatsappNo", data.WhatsappNo);
                // End of Rev 1.0
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

        public ActionResult EditCRMContact(String ShopCode)
        {
            try
            {
                AddCrmContactData ret = new AddCrmContactData();

                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_FTSInsertUpdateCRMContact");
                proc.AddPara("@ACTION", "EDITCRMCONTACT");
                proc.AddPara("@ShopCode", ShopCode);
                dt = proc.GetTable();

                if (dt != null && dt.Rows.Count > 0)
                {
                    ret.shop_code = dt.Rows[0]["shop_code"].ToString();
                    ret.FirstName = dt.Rows[0]["Shop_FirstName"].ToString();
                    ret.LastName = dt.Rows[0]["Shop_LastName"].ToString();
                    ret.PhoneNo = dt.Rows[0]["Shop_Owner_Contact"].ToString();
                    ret.Email = dt.Rows[0]["Shop_Owner_Email"].ToString();
                    ret.Address = dt.Rows[0]["Address"].ToString();
                    ret.DOB = dt.Rows[0]["DOB"].ToString();
                    ret.Anniversarydate = dt.Rows[0]["date_aniversary"].ToString();
                    ret.CompanyId = Convert.ToInt32(dt.Rows[0]["Shop_CRMCompID"].ToString());
                    ret.JobTitle = dt.Rows[0]["Shop_JobTitle"].ToString();
                    ret.AssignedTo = dt.Rows[0]["user_name"].ToString();
                    ret.AssignedToId = Convert.ToInt32(dt.Rows[0]["Shop_CreateUser"].ToString());
                    ret.TypeId = Convert.ToInt32(dt.Rows[0]["Shop_CRMTypeID"].ToString());
                    ret.StatusId = Convert.ToInt32(dt.Rows[0]["Shop_CRMStatusID"].ToString());
                    ret.SourceId = Convert.ToInt32(dt.Rows[0]["Shop_CRMSourceID"].ToString());
                    ret.ReferenceName = dt.Rows[0]["REFERENCE_NAME"].ToString();
                    ret.ReferenceId = dt.Rows[0]["Shop_CRMReferenceID"].ToString();
                    ret.StageId = Convert.ToInt32(dt.Rows[0]["Shop_CRMStageID"].ToString());
                    ret.Remarks = dt.Rows[0]["Remarks"].ToString();
                    ret.ExpSalesValue = Convert.ToDecimal(dt.Rows[0]["Amount"].ToString());
                    ret.NextFollowDate = dt.Rows[0]["Shop_NextFollowupDate"].ToString();
                    ret.Active = Convert.ToInt32(dt.Rows[0]["Entity_Status"]);
                    // Rev 1.0
                    ret.Pincode = dt.Rows[0]["Pincode"].ToString();
                    ret.WhatsappNo = dt.Rows[0]["WhatsappNoForCustomer"].ToString();
                    // End of Rev 1.0
                }
                return Json(ret, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        [HttpPost]
        public JsonResult DeleteCRMContact(string ShopCode)
        {
            string output_msg = string.Empty;
            try
            {
                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_FTSInsertUpdateCRMContact");
                proc.AddPara("@ACTION", "DELETECRMCONTACT");
                proc.AddPara("@ShopCode", ShopCode);
                dt = proc.GetTable();

                if (dt != null && dt.Rows.Count > 0)
                {
                    output_msg = dt.Rows[0]["MSG"].ToString();
                }
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
                    return GridViewExtension.ExportToPdf(GetDoctorBatchGridViewSettings(), GetCRMContactDetails(""));
                case 2:
                    return GridViewExtension.ExportToXlsx(GetDoctorBatchGridViewSettings(), GetCRMContactDetails(""));
                case 3:
                    return GridViewExtension.ExportToXls(GetDoctorBatchGridViewSettings(), GetCRMContactDetails(""));
                case 4:
                    return GridViewExtension.ExportToRtf(GetDoctorBatchGridViewSettings(), GetCRMContactDetails(""));
                case 5:
                    return GridViewExtension.ExportToCsv(GetDoctorBatchGridViewSettings(), GetCRMContactDetails(""));
                default:
                    break;
            }
            return null;
        }

        private GridViewSettings GetDoctorBatchGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "gridCRMContact";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Contact Details";

            settings.Columns.Add(x =>
            {
                x.FieldName = "SEQ";
                x.Caption = "Sr. No";
                x.VisibleIndex = 1;
                x.ExportWidth = 20;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_FirstName";
                x.Caption = "First Name";
                x.VisibleIndex = 2;
                x.ExportWidth = 200;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_LastName";
                x.Caption = "Last Name";
                x.VisibleIndex = 3;
                x.ExportWidth = 200;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_Owner_Contact";
                x.Caption = "Phone";
                x.VisibleIndex = 4;
                x.ExportWidth = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_Owner_Email";
                x.Caption = "Email";
                x.VisibleIndex = 5;
                x.ExportWidth = 150;
            });
            //Mantis Issue 24928
            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_Address";
                x.Caption = "Address";
                x.VisibleIndex = 6;
                x.ExportWidth = 250;
            });

            // Rev 1.0
            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_Pincode";
                x.Caption = "Pincode";
                x.VisibleIndex = 7;
                x.ExportWidth = 250;
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_WhatsappNoForCustomer";
                x.Caption = "Whatsapp Number";
                x.VisibleIndex = 8;
                x.ExportWidth = 250;
            });
            // End of Rev 1.0

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_DOB";
                x.Caption = "Date of Birth";
                x.VisibleIndex = 9;
                x.ExportWidth = 100;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
            });
            //End of Mantis Issue 24928
            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_date_aniversary";
                x.Caption = "Date of Anniversary";
                x.VisibleIndex = 10;
                x.ExportWidth = 150;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_CompanyName";
                x.Caption = "Company";
                x.VisibleIndex = 11;
                x.ExportWidth = 150;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_JobTitle";
                x.Caption = "Job Title";
                x.VisibleIndex = 12;
                x.ExportWidth = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_CreateUserName";
                x.Caption = "Assign To";
                x.VisibleIndex = 13;
                x.ExportWidth = 120;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_TypeName";
                x.Caption = "Type";
                x.VisibleIndex = 14;
                x.ExportWidth = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_StatusName";
                x.Caption = "Status";
                x.VisibleIndex = 15;
                x.ExportWidth = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_SourceName";
                x.Caption = "Source";
                x.VisibleIndex = 16;
                x.ExportWidth = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_ReferenceName";
                x.Caption = "Reference";
                x.VisibleIndex = 17;
                x.ExportWidth = 150;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_StageName";
                x.Caption = "Stages";
                x.VisibleIndex = 18;
                x.ExportWidth = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_Remarks";
                x.Caption = "Remarks";
                x.VisibleIndex = 19;
                x.ExportWidth = 250;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_Amount";
                x.Caption = "Expected Sales Value";
                x.VisibleIndex = 20;
                x.ExportWidth = 150;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_NextFollowupDate";
                x.Caption = "Next follow Up Date";
                x.VisibleIndex = 21;
                x.ExportWidth = 200;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
            });

            // Rev 1.0
            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_Entered_On";
                x.Caption = "Created Date";
                x.VisibleIndex = 22;
                x.ExportWidth = 100;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_Entered_ByName";
                x.Caption = "Created By";
                x.VisibleIndex = 23;
                x.ExportWidth = 100;
            });


            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_LastUpdated_On";
                x.Caption = "Modified Date";
                x.VisibleIndex = 24;
                x.ExportWidth = 120;
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_LastUpdated_ByName";
                x.Caption = "Modified By";
                x.VisibleIndex = 25;
                x.ExportWidth = 100;
            });
            // End of Rev 1.0

            settings.Columns.Add(x =>
            {
                x.FieldName = "Shop_Entity_Status";
                x.Caption = "Active";
                x.VisibleIndex = 26;
                x.ExportWidth = 100;
            });

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

        public ActionResult DownloadFormat()
        {
            string FileName = "CRMContactList.xlsx";
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "image/jpeg";
            response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ";");
            response.TransmitFile(Server.MapPath("~/Commonfolder/CRMContactList.xlsx"));
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
                        dtExcelData.Columns.Add("FirstName", typeof(string));
                        dtExcelData.Columns.Add("LastName", typeof(string));
                        dtExcelData.Columns.Add("Phone", typeof(string));
                        dtExcelData.Columns.Add("Email", typeof(string));
                        dtExcelData.Columns.Add("Address", typeof(string));
                        dtExcelData.Columns.Add("DateofBirth", typeof(string));
                        dtExcelData.Columns.Add("DateofAnniversary", typeof(string));
                        dtExcelData.Columns.Add("Company", typeof(string));
                        dtExcelData.Columns.Add("JobTitle", typeof(string));
                        dtExcelData.Columns.Add("AssignTo", typeof(string));
                        dtExcelData.Columns.Add("Type", typeof(string));
                        dtExcelData.Columns.Add("Status", typeof(string));
                        dtExcelData.Columns.Add("Source", typeof(string));
                        dtExcelData.Columns.Add("Reference", typeof(string));
                        dtExcelData.Columns.Add("Stages", typeof(string));
                        dtExcelData.Columns.Add("Remarks", typeof(string));
                        dtExcelData.Columns.Add("ExpectedSalesValue", typeof(decimal));
                        dtExcelData.Columns.Add("NextfollowUpDate", typeof(string));
                        dtExcelData.Columns.Add("Active", typeof(string));
                        // Rev 1.0
                        dtExcelData.Columns.Add("Pincode", typeof(string));
                        dtExcelData.Columns.Add("WhatsappNo", typeof(string));
                        // End of Rev 1.0

                        foreach (DataRow row in dt.Select("[First Name*]<>'' ") )
                        {
                            if(Convert.ToString(row["Expected Sales Value"]) == "")
                            {
                                row["Expected Sales Value"] = "0";
                            }


                            dtExcelData.Rows.Add(Convert.ToString(row["First Name*"]), Convert.ToString(row["Last Name"]), Convert.ToString(row["Phone*"]), Convert.ToString(row["Email"]), Convert.ToString(row["Address"]),
                            Convert.ToString(row["Date of Birth (dd-mm-yyyy)"]), Convert.ToString(row["Date of Anniversary (dd-mm-yyyy)"]), Convert.ToString(row["Company"]), Convert.ToString(row["Job Title"]), Convert.ToString(row["Assign To* (Login Id)"]),
                            Convert.ToString(row["Type"]), Convert.ToString(row["Status"]), Convert.ToString(row["Source"]), Convert.ToString(row["Reference (Login Id/Phone No)"]), Convert.ToString(row["Stages"]), 
                            Convert.ToString(row["Remarks"]), Convert.ToString(row["Expected Sales Value"]), Convert.ToString(row["Next follow Up Date (dd-mm-yyyy)"]), Convert.ToString(row["Active"]), Convert.ToString(row["Pincode"]), Convert.ToString(row["WhatsappNo"]));

                        }
                        
                        try
                        {
                            TempData["ContactImportLog"] = dtExcelData;
                            TempData.Keep();

                            DataTable dtCmb = new DataTable();
                            ProcedureExecute proc = new ProcedureExecute("PRC_FTSInsertUpdateCRMContact");
                            proc.AddPara("@ACTION", "IMPORTCONTACT");
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
        public ActionResult ContactImportLog()
        {
            List<CRMContactImportLogModel> list = new List<CRMContactImportLogModel>();
            DataTable dt = new DataTable();
            try
            {
                if (TempData["ContactImportLog"] != null)
                {
                    DataTable dtCmb = new DataTable();
                    ProcedureExecute proc = new ProcedureExecute("PRC_FTSInsertUpdateCRMContact");
                    proc.AddPara("@Action", "SHOWIMPORTLOG");
                    proc.AddPara("@IMPORT_TABLE", (DataTable)TempData["ContactImportLog"]);
                    proc.AddPara("@User_Id", Convert.ToInt32(Session["userid"]));
                    dt = proc.GetTable();
                    TempData.Keep();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        CRMContactImportLogModel data = null;
                        foreach (DataRow row in dt.Rows)
                        {
                            data = new CRMContactImportLogModel();
                            data.FirstName = Convert.ToString(row["FirstName"]);
                            data.LastName = Convert.ToString(row["LastName"]);
                            data.Phone = Convert.ToString(row["Phone"]);
                            data.Email = Convert.ToString(row["Email"]);
                            data.Address = Convert.ToString(row["Address"]);
                            data.DateofBirth = Convert.ToString(row["DateofBirth"]);
                            data.DateofAnniversary = Convert.ToString(row["DateofAnniversary"]);
                            data.Company = Convert.ToString(row["Company"]);
                            data.JobTitle = Convert.ToString(row["JobTitle"]);
                            data.AssignTo = Convert.ToString(row["AssignTo"]);
                            data.Type = Convert.ToString(row["Type"]);
                            data.Status = Convert.ToString(row["Status"]);
                            data.Source = Convert.ToString(row["Source"]);
                            data.Reference = Convert.ToString(row["Reference"]);
                            data.Stages = Convert.ToString(row["Stages"]);
                            data.Remarks = Convert.ToString(row["Remarks"]);
                            data.ExpectedSalesValue = Convert.ToDecimal(row["ExpectedSalesValue"]);
                            data.NextfollowUpDate = Convert.ToString(row["NextfollowUpDate"]);
                            data.Active = Convert.ToString(row["Active"]);
                            data.ImportStatus = Convert.ToString(row["ImportStatus"]);
                            data.ImportMsg = Convert.ToString(row["ImportMsg"]);
                            data.ImportDate = Convert.ToString(row["ImportDate"]);
                            data.CreateUser = Convert.ToString(row["CreateUser"]);
                            // Rev 1.0
                            data.Pincode = Convert.ToString(row["Pincode"]);
                            data.WhatsappNo = Convert.ToString(row["WhatsappNo"]);
                            // End of Rev 1.0

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
        public JsonResult CRMContactImportUserLog(string Fromdt, String ToDate)
        {
            string output_msg = string.Empty;
            try
            {
                string datfrmat = Fromdt.Split('-')[2] + '-' + Fromdt.Split('-')[1] + '-' + Fromdt.Split('-')[0];
                string dattoat = ToDate.Split('-')[2] + '-' + ToDate.Split('-')[1] + '-' + ToDate.Split('-')[0];

                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_FTSInsertUpdateCRMContact");
                proc.AddPara("@ACTION", "GETCRMCONTACTIMPORTLOG");
                proc.AddPara("@FromDate", datfrmat);
                proc.AddPara("@ToDate", dattoat);
                dt = proc.GetTable();

                if (dt != null && dt.Rows.Count > 0)
                {
                    TempData["ContactImportLog"] = dt;
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

        public ActionResult ExportImportLogGrid(int type)
        {
            //ViewData["EnquiriesImportLog"] = TempData["EnquiriesImportLog"];

            //TempData.Keep();

            DataTable dtExp = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSInsertUpdateCRMContact");
            proc.AddPara("@Action", "SHOWIMPORTLOG");
            proc.AddPara("@IMPORT_TABLE", (DataTable)TempData["ContactImportLog"]);
            proc.AddPara("@User_Id", Convert.ToInt32(Session["userid"]));
            dtExp = proc.GetTable();

            // if (ViewData["EnquiriesImportLog"] != null )
            if (dtExp != null && dtExp.Rows.Count > 0)
            {
                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetContactImportLogGrid(dtExp), dtExp);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetContactImportLogGrid(dtExp), dtExp);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXlsx(GetContactImportLogGrid(dtExp), dtExp);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetContactImportLogGrid(dtExp), dtExp);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetContactImportLogGrid(dtExp), dtExp);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetContactImportLogGrid(object datatable)
        {
            var settings = new GridViewSettings();
            settings.Name = "ContactImportLog";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "CRM Contact Import Log";

            settings.Columns.Add(x =>
            {
                x.FieldName = "FirstName";
                x.Caption = "First Name";
                x.VisibleIndex = 1;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(150);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "LastName";
                x.Caption = "Last Name";
                x.VisibleIndex = 2;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(150);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Phone";
                x.Caption = "Phone";
                x.VisibleIndex = 3;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(100);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Email";
                x.Caption = "Email";
                x.VisibleIndex = 4;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(100);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Address";
                x.Caption = "Address";
                x.VisibleIndex = 5;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(100);
            });

            // Rev 1.0
            settings.Columns.Add(x =>
            {
                x.FieldName = "Pincode";
                x.Caption = "Pincode";
                x.VisibleIndex = 6;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(100);
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "WhatsappNo";
                x.Caption = "WhatsappNo";
                x.VisibleIndex = 7;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(100);
            });
            // End of Rev 1.0

            settings.Columns.Add(x =>
            {
                x.FieldName = "DateofBirth";
                x.Caption = "Date of Birth";
                x.VisibleIndex = 8;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(100);
                x.ColumnType = MVCxGridViewColumnType.DateEdit;

                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy hh:mm tt";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy hh:mm tt";
            });


            settings.Columns.Add(x =>
            {
                x.FieldName = "DateofAnniversary";
                x.Caption = "Date of Anniversary";
                x.VisibleIndex = 9;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(150);
                x.ColumnType = MVCxGridViewColumnType.DateEdit;

                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy hh:mm tt";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy hh:mm tt";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Company";
                x.Caption = "Company";
                x.VisibleIndex = 10;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(100);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "JobTitle";
                x.Caption = "Job Title";
                x.VisibleIndex = 11;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(100);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "AssignTo";
                x.Caption = "Assign To";
                x.VisibleIndex = 12;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(120);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Type";
                x.Caption = "Type";
                x.VisibleIndex = 13;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(100);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Status";
                x.Caption = "Status";
                x.VisibleIndex = 14;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(100);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Source";
                x.Caption = "Source";
                x.VisibleIndex = 15;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(100);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Reference";
                x.Caption = "Reference";
                x.VisibleIndex = 16;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(100);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Stages";
                x.Caption = "Stages";
                x.VisibleIndex = 17;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(100);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Remarks";
                x.Caption = "Remarks";
                x.VisibleIndex = 18;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(100);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ExpectedSalesValue";
                x.Caption = "ExpectedSalesValue";
                x.VisibleIndex = 19;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(150);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "NextfollowUpDate";
                x.Caption = "NextfollowUpDate";
                x.VisibleIndex = 20;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(100);

                x.ColumnType = MVCxGridViewColumnType.DateEdit;

                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy hh:mm tt";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy hh:mm tt";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "Active";
                x.Caption = "Active";
                x.VisibleIndex = 21;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(100);
            });


            settings.Columns.Add(x =>
            {
                x.FieldName = "ImportDate";
                x.Caption = "Import Date";
                x.VisibleIndex = 22;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(140);
                x.ColumnType = MVCxGridViewColumnType.DateEdit;

                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy hh:mm tt";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy hh:mm tt";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ImportStatus";
                x.Caption = "Import Status";
                x.VisibleIndex = 23;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(100);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ImportMsg";
                x.Caption = "Import Msg";
                x.VisibleIndex = 24;
                x.Width = System.Web.UI.WebControls.Unit.Pixel(200);
            });

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

        public JsonResult GetTotalContactsCount(string start, string end)
        {
            CRMContactModel dtl = new CRMContactModel();
            try
            {
                DataSet ds = new DataSet();
                ProcedureExecute proc = new ProcedureExecute("PRC_FTSContactListingShow");
                proc.AddPara("@Action", "GETTOTALCONTACTSCOUNT");
                //proc.AddPara("@ToDate", DateTime.Now.ToString("yyyy-MM-dd"));
                //proc.AddPara("@Fromdate", DateTime.Now.ToString("yyyy-MM-dd"));
                proc.AddPara("@USERID", Convert.ToString(HttpContext.Session["userid"]));
                //proc.AddPara("@ENQUIRIESFROM", EnquiryFromDesc);
                ds = proc.GetDataSet();


                int TotalContacts = 0;
               
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    TotalContacts = Convert.ToInt32(item["cnt_TotalContacts"]);
                    
                }

                dtl.TotalContacts = TotalContacts;
                
            }
            catch
            {
            }
            return Json(dtl);
        }

    }
}
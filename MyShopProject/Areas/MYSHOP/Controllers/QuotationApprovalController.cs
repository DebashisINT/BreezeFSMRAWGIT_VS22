/*****************************************************************************************************************
 * 1.0      Sanchita        V2.0.48     10/09/2024      27690: Quotation Notification issue @ Eurobond
 * ********************************************************************************************************************/
using BusinessLogicLayer;
using BusinessLogicLayer.SalesTrackerReports;
using DataAccessLayer;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using Models;
using MyShop.Models;
using SalesmanTrack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using UtilityLayer;


namespace MyShop.Areas.MYSHOP.Controllers
{
    public class QuotationApprovalController : Controller
    {
         DBEngine odbengine = new DBEngine();
        //
        // GET: /MYSHOP/QuotationApproval/
        UserList lstuser = new UserList();
        SalesSummary_Report objgps = new SalesSummary_Report();

        public ActionResult Index()
        {
            try
            {

                QuotationApprovalModel omodel = new QuotationApprovalModel();
                string userid = Session["userid"].ToString();
                omodel.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                omodel.Todate = DateTime.Now.ToString("dd-MM-yyyy");

                return View(omodel);

            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetQuotationApprovalList(QuotationApprovalModel model)
        {
            try
            {

                DataTable dt = new DataTable();
                string Is_PageLoad = string.Empty;

                if (model.is_pageload == "0")
                {
                    Is_PageLoad = "Ispageload";
                }

                if (model.Fromdate == null)
                {
                    model.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                }

                if (model.Todate == null)
                {
                    model.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                }

                ViewData["ModelData"] = model;

                string datfrmat = model.Fromdate.Split('-')[2] + '-' + model.Fromdate.Split('-')[1] + '-' + model.Fromdate.Split('-')[0];
                string dattoat = model.Todate.Split('-')[2] + '-' + model.Todate.Split('-')[1] + '-' + model.Todate.Split('-')[0];
                string Userid = Convert.ToString(Session["userid"]);


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
                string shopcode = "";

                int i = 1;

                if (model.shopcode != null && model.shopcode.Count > 0)
                {
                    foreach (string item in model.shopcode)
                    {
                        if (i > 1)
                            shopcode = shopcode + "," + item;
                        else
                            shopcode = item;
                        i++;
                    }

                }

                dt = objgps.GetQuotationApprovalDetails(datfrmat, dattoat, Userid, shopcode, empcode);
                return PartialView("PartialGetQuotationApprovalDetails", GetQuotationApprovalDetails(Is_PageLoad));


            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }
        public IEnumerable GetQuotationApprovalDetails(string frmdate)
        {
            DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "Quotation Approval");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            }

            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);

            if (frmdate != "Ispageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSQUOTATIONAPPROVALDETAILS_REPORTs
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSQUOTATIONAPPROVALDETAILS_REPORTs
                        where d.USERID == Convert.ToInt32(Userid) && d.EMPCODE == "0"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }



        }

        public ActionResult UpdateApproveReject(string Status, string DOCUMENT_NUMBER, string QUOTATION_NUMBER, string Remarks)
        {
            try
            {

                string rtrnvalue = "";
                string Userid = Convert.ToString(Session["userid"]);

                ProcedureExecute proc = new ProcedureExecute("PRC_FTSQUOTATIONAPPROVALDETAILS_REPORT");
                if (Status == "Approve")
                {
                    proc.AddPara("@ACTION", "APPROVE");
                }
                else
                {
                    proc.AddPara("@ACTION", "REJECT");
                }
                proc.AddPara("@DOCUMENT_NUMBER", DOCUMENT_NUMBER);
                proc.AddPara("@QUOTATION_NUMBER", QUOTATION_NUMBER);
                proc.AddPara("@Remarks", Remarks);
                proc.AddPara("@USERID", Userid);
                proc.AddVarcharPara("@RETURN_VALUE", 50, "", QueryParameterDirection.Output);
                
                int k = proc.RunActionQuery();

                rtrnvalue = Convert.ToString(proc.GetParaValue("@RETURN_VALUE"));
                return Json(rtrnvalue, JsonRequestBehavior.AllowGet);
                
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public JsonResult SendNotification(string Mobiles, string messagetext)
        {

            string status = string.Empty;
            try
            {
                //int returnmssge = notificationbl.Savenotification(Mobiles, messagetext);
                int s = 0;
                ProcedureExecute proc = new ProcedureExecute("Proc_FCM_NotificationManage");
                proc.AddPara("@Mobiles", Mobiles);
                proc.AddPara("@Message", messagetext);
                s = proc.RunActionQuery();

                DataTable dt = odbengine.GetDataTable("select  device_token,musr.user_name,musr.user_id  from tbl_master_user as musr inner join tbl_FTS_devicetoken as token on musr.user_id=token.UserID  where musr.user_loginId in (select items from dbo.SplitString('" + Mobiles + "',',')) and musr.user_inactive='N'");

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToString(dt.Rows[i]["device_token"]) != "")
                        {
                            // Rev 1.0
                            //SendPushNotification(messagetext, Convert.ToString(dt.Rows[i]["device_token"]), Convert.ToString(dt.Rows[i]["user_name"]), Convert.ToString(dt.Rows[i]["user_id"]));

                            CRMEnquiriesController obj = new CRMEnquiriesController();
                            obj.SendPushNotification(messagetext, Convert.ToString(dt.Rows[i]["device_token"]), Convert.ToString(dt.Rows[i]["user_name"]), Convert.ToString(dt.Rows[i]["user_id"]), "flag_status_quotation_approval");
                            // End of Rev 1.0
                        }
                    }
                    status = "200";
                }


                else
                {

                    status = "202";
                }



                return Json(status, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                status = "300";
                return Json(status, JsonRequestBehavior.AllowGet);
            }
        }

        // Rev 1.0
        //public static void SendPushNotification(string message, string deviceid, string Customer, string Requesttype)
        //{
        //    try
        //    {
        //        //string applicationID = "AAAAS0O97Kk:APA91bH8_KgkJzglOUHC1ZcMEQFjQu8fsj1HBKqmyFf-FU_I_GLtXL_BFUytUjhlfbKvZFX9rb84PWjs05HNU1QyvKy_TJBx7nF70IdIHBMkPgSefwTRyDj59yXz4iiKLxMiXJ7vel8B";
        //        //string senderId = "323259067561";
        //        string applicationID = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["AppID"]);
        //        string senderId = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["SenderID"]);
        //        //string senderId = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["SenderID"]);
        //        string deviceId = deviceid;
        //        WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
        //        tRequest.Method = "post";
        //        tRequest.ContentType = "application/json";

        //        var data2 = new
        //        {
        //            to = deviceId,
        //            //notification = new
        //            //{
        //            //    body = message,
        //            //    title = ""
        //            //},
        //            data = new
        //            {
        //                UserName = Customer,
        //                UserID = Requesttype,
        //                body = message,
        //                type = "flag_status_quotation_approval"
        //            }
        //        };

        //        var serializer = new JavaScriptSerializer();
        //        var json = serializer.Serialize(data2);
        //        Byte[] byteArray = Encoding.UTF8.GetBytes(json);
        //        tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
        //        tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
        //        tRequest.ContentLength = byteArray.Length;
        //        using (Stream dataStream = tRequest.GetRequestStream())
        //        {
        //            dataStream.Write(byteArray, 0, byteArray.Length);
        //            using (WebResponse tResponse = tRequest.GetResponse())
        //            {
        //                using (Stream dataStreamResponse = tResponse.GetResponseStream())
        //                {
        //                    using (StreamReader tReader = new StreamReader(dataStreamResponse))
        //                    {
        //                        String sResponseFromServer = tReader.ReadToEnd();
        //                        string str = sResponseFromServer;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string str = ex.Message;
        //    }
        //}
        // End of Rev 1.0

        public JsonResult GetApprovalDetails(string DOCUMENT_NUMBER)
        {
            List<QuotationApproveDetailsList> objQuotationApproveDetailsList = new List<QuotationApproveDetailsList>();
            try
            {
                DataTable dtAprovalDet = new DataTable();
                ProcedureExecute proc1 = new ProcedureExecute("PRC_FTSQUOTATIONAPPROVALDETAILS_REPORT");
                proc1.AddPara("@ACTION", "GETAPPROVALDETAILS");
                proc1.AddPara("@DOCUMENT_NUMBER", DOCUMENT_NUMBER);
                dtAprovalDet = proc1.GetTable();

                objQuotationApproveDetailsList = APIHelperMethods.ToModelList<QuotationApproveDetailsList>(dtAprovalDet);
            }

            catch (Exception ex)
            {
                //_msg.response_code = "CatchError";
                //_msg.response_msg = "Please try again later";
            }

            return Json(objQuotationApproveDetailsList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExportQuotationApprovalList(int type)
        {


            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetQuotationApprovalBatchGridViewSettings(), GetQuotationApprovalDetails(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetQuotationApprovalBatchGridViewSettings(), GetQuotationApprovalDetails(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetQuotationApprovalBatchGridViewSettings(), GetQuotationApprovalDetails(""));
                case 4:
                    return GridViewExtension.ExportToRtf(GetQuotationApprovalBatchGridViewSettings(), GetQuotationApprovalDetails(""));
                case 5:
                    return GridViewExtension.ExportToCsv(GetQuotationApprovalBatchGridViewSettings(), GetQuotationApprovalDetails(""));
                //break;

                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetQuotationApprovalBatchGridViewSettings()
        {
            DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "Quotation Approval");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;
            }
            var settings = new GridViewSettings();
            settings.Name = "Quotation Approval";
            settings.CallbackRouteValues = new { Controller = "QuotationApproval", Action = "GetQuotationApprovalList" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Quotation Approval";


            settings.Columns.Add(x =>
            {
                x.FieldName = "SEQ";
                x.Caption = "Sr. No.";
                x.VisibleIndex = 1;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SEQ'");
                    if (row != null && row.Length > 0)
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                    }
                }
                else
                {
                    x.Visible = true;
                }

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "DOCUMENT_NUMBER";
                x.Caption = "Document Number";
                x.VisibleIndex = 2;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DOCUMENT_NUMBER'");
                    if (row != null && row.Length > 0)
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                    }
                }
                else
                {
                    x.Visible = true;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "QUOTATIONDATE";
                x.Caption = "Date";
                x.VisibleIndex = 3;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='QUOTATIONDATE'");
                    if (row != null && row.Length > 0)
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                    }
                }
                else
                {
                    x.Visible = true;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CUSTNAME";
                x.Caption = "Customer Name";
                x.VisibleIndex = 4;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CUSTNAME'");
                    if (row != null && row.Length > 0)
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                    }
                }
                else
                {
                    x.Visible = true;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CUSTADDRESS";
                x.Caption = "Address Details";
                x.VisibleIndex = 5;
                x.Width = 100;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CUSTADDRESS'");
                    if (row != null && row.Length > 0)
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                    }
                }
                else
                {
                    x.Visible = true;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CUSTEMAIL";
                x.Caption = "Email id";
                x.VisibleIndex = 6;
                x.Width = 120;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CUSTEMAIL'");
                    if (row != null && row.Length > 0)
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                    }
                }
                else
                {
                    x.Visible = true;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CONTACTPERSON";
                x.Caption = "Contact person";
                x.VisibleIndex = 7;
                x.Width = 120;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACTPERSON'");
                    if (row != null && row.Length > 0)
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                    }
                }
                else
                {
                    x.Visible = true;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CONTACTNO";
                x.Caption = "Contact number";
                x.VisibleIndex = 8;
                x.Width = 180;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACTNO'");
                    if (row != null && row.Length > 0)
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                    }
                }
                else
                {
                    x.Visible = true;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "PRODUCT_NAME";
                x.Caption = "Product";
                x.VisibleIndex = 9;
                x.Width = 180;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PRODUCT_NAME'");
                    if (row != null && row.Length > 0)
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                    }
                }
                else
                {
                    x.Visible = true;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "RATE_SQFT";
                x.Caption = "Rate in Sq. ft.";
                x.VisibleIndex = 10;
                x.Width = 180;
                x.PropertiesEdit.DisplayFormatString = "0.00";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='RATE_SQFT'");
                    if (row != null && row.Length > 0)
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                    }
                }
                else
                {
                    x.Visible = true;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "SALESPERSON";
                x.Caption = "Sales Person";
                x.VisibleIndex = 11;
                x.Width = 180;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SALESPERSON'");
                    if (row != null && row.Length > 0)
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                    }
                }
                else
                {
                    x.Visible = true;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "PROJECT_NAME";
                x.Caption = "Project Name";
                x.VisibleIndex = 12;
                x.Width = 180;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PROJECT_NAME'");
                    if (row != null && row.Length > 0)
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                    }
                }
                else
                {
                    x.Visible = true;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "REMARKS";
                x.Caption = "Remarks";
                x.VisibleIndex = 13;
                x.Width = 100;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='REMARKS'");
                    if (row != null && row.Length > 0)
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                   }
                }
                else
                {
                    x.Visible = true;
                    x.Width = System.Web.UI.WebControls.Unit.Percentage(8);
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "QUOTATION_STATUS";
                x.Caption = "Status";
                x.VisibleIndex = 13;
                x.Width = 100;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='QUOTATION_STATUS'");
                    if (row != null && row.Length > 0)
                    {
                        x.Visible = false;
                        //x.Width = 0;
                    }
                    else
                    {
                        x.Visible = true;
                    }
                }
                else
                {
                    x.Visible = true;
                }
            });

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

        public ActionResult PageRetention(List<String> Columns)
        {
            try
            {
                String Col = "";
                int i = 1;
                if (Columns != null && Columns.Count > 0)
                {
                    Col = string.Join(",", Columns);
                }
                int k = objgps.InsertPageRetention(Col, Session["userid"].ToString(), "Quotation Approval");

                return Json(k, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
	}

    public class QuotationApproveDetailsList
    {
        public string QUOTATION_STATUS { get; set; }
        public string ApproveRemarks {get; set;}
        public string QUOTATION_NUMBER { get; set; }
        public string user_loginId { get; set; }

    }
}
/**********************************************************************************************************************************
 * 1.0		Sanchita		V2.0.40		24-04-2023		In TRAVELLING ALLOWANCE -- Approve/Reject Page: One Coloumn('Confirm/Reject') required 
													    before 'Approve/Reject' coloumn. refer: 25809
   2.0      Sanchita        V2.0.48     10/09/2024      27690: Quotation Notification issue @ Eurobond
***********************************************************************************************************************************/
using SalesmanTrack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BusinessLogicLayer.SalesTrackerReports;
using System.Data;
using UtilityLayer;
using System.Collections;
using System.Configuration;
using MyShop.Models;
using DevExpress.Web.Mvc;
using DevExpress.Web;
using System.Net;
using System.Web.Script.Serialization;
using System.Text;
using System.IO;
using BusinessLogicLayer;
using DataAccessLayer;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class ReimbursementController : Controller
    {
        UserList lstuser = new UserList();
        Reimbursement_List objgps = new Reimbursement_List();
        // GET: MYSHOP/SalesReportSalary
        public ActionResult Reimbursement()
        {
            try
            {
                ReimbursementReport omodel = new ReimbursementReport();
                string userid = Session["userid"].ToString();

                List<Months> month = new List<Months>();
                month.Add(new Months { ID = "JAN", MonthName = "January" });
                month.Add(new Months { ID = "FEB", MonthName = "February" });
                month.Add(new Months { ID = "MAR", MonthName = "March" });
                month.Add(new Months { ID = "APR", MonthName = "April" });
                month.Add(new Months { ID = "MAY", MonthName = "May" });
                month.Add(new Months { ID = "JUN", MonthName = "June" });
                month.Add(new Months { ID = "JUL", MonthName = "July" });
                month.Add(new Months { ID = "AUG", MonthName = "August" });
                month.Add(new Months { ID = "SEP", MonthName = "September" });
                month.Add(new Months { ID = "OCT", MonthName = "October" });
                month.Add(new Months { ID = "NOV", MonthName = "November" });
                month.Add(new Months { ID = "DEC", MonthName = "December" });

                List<Years> year = new List<Years>();

                DataTable dtyr = objgps.GetYearList();
                if (dtyr != null && dtyr.Rows.Count > 0)
                {
                    foreach (DataRow item in dtyr.Rows)
                    {
                        year.Add(new Years
                        {
                            ID = Convert.ToString(item["YEARS"]),
                            YearName = Convert.ToString(item["YEARS"])
                        });
                    }
                }

                omodel.MonthList = month;
                omodel.YearList = year;

                return View(omodel);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetReimbursementList(ReimbursementReport model)
        {

            DataTable dt = new DataTable();
            string frmdate = string.Empty;

            if (model.is_pageload == "0")
            {
                frmdate = "Ispageload";
            }


            ViewData["ModelData"] = model;

            //string datfrmat = model.Fromdate.Split('-')[2] + '-' + model.Fromdate.Split('-')[1] + '-' + model.Fromdate.Split('-')[0];
            //string dattoat = model.Todate.Split('-')[2] + '-' + model.Todate.Split('-')[1] + '-' + model.Todate.Split('-')[0];
            string month = model.Month;
            String year = model.Year;
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

            string desig = "";
            int j = 1;

            if (model.desgid != null && model.desgid.Count > 0)
            {
                foreach (string item in model.desgid)
                {
                    if (j > 1)
                        desig = desig + "," + item;
                    else
                        desig = item;
                    j++;
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
            dt = objgps.GetReimbursementListReport(month, Userid, state, desig, empcode, year);
            // Rev 1.0
            string isExpenseFeatureAvailable = "0";
            DBEngine obj1 = new DBEngine();
            isExpenseFeatureAvailable = Convert.ToString(obj1.GetDataTable("select [value] from FTS_APP_CONFIG_SETTINGS WHERE [Key]='isExpenseFeatureAvailable'").Rows[0][0]);
            ViewBag.isExpenseFeatureAvailable = isExpenseFeatureAvailable;
            // End of Rev 1.0
            return PartialView("PartialGetReimbursementList", LGetReimbursement(frmdate));
        }

        public ActionResult GetStateList()
        {
            try
            {
                List<GetUsersStates> modelstate = new List<GetUsersStates>();
                DataTable dtstate = lstuser.GetStateList();
                modelstate = APIHelperMethods.ToModelList<GetUsersStates>(dtstate);




                return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_StatesPartial.cshtml", modelstate);



            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }

        public ActionResult GetDesigList()
        {
            try
            {
                DataTable dtdesig = lstuser.Getdesiglist();
                List<GetDesignation> modeldesig = new List<GetDesignation>();
                modeldesig = APIHelperMethods.ToModelList<GetDesignation>(dtdesig);

                return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_DesigPartial.cshtml", modeldesig);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetEmpList(ReimbursementReport model)
        {
            try
            {
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

                string desig = "";
                int k = 1;

                if (model.desgid != null && model.desgid.Count > 0)
                {
                    foreach (string item in model.desgid)
                    {
                        if (k > 1)
                            desig = desig + "," + item;
                        else
                            desig = item;
                        k++;
                    }

                }



                DataTable dtemp = lstuser.Getemplist(state, desig);
                List<GetAllEmployee> modelemp = new List<GetAllEmployee>();
                modelemp = APIHelperMethods.ToModelList<GetAllEmployee>(dtemp);

                return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_EmpPartial.cshtml", modelemp);



            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }

        public IEnumerable LGetReimbursement(string frmdate)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);

            if (frmdate != "Ispageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSEMPLOYEEREIMBURSEMENTLIST_REPORTs
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSEMPLOYEEREIMBURSEMENTLIST_REPORTs
                        where d.USERID == Convert.ToInt32(Userid) && d.EMPCODE == "0"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }



        }


        public ActionResult ExporReimbursementList(int type)
        {


            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(), LGetReimbursement(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(), LGetReimbursement(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(), LGetReimbursement(""));
                case 4:
                    return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(), LGetReimbursement(""));
                case 5:
                    return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(), LGetReimbursement(""));
                //break;

                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetEmployeeBatchGridViewSettings()
        {
            //Rev 1.0
            string isExpenseFeatureAvailable = "0";
            DBEngine obj1 = new DBEngine();
            isExpenseFeatureAvailable = Convert.ToString(obj1.GetDataTable("select [value] from FTS_APP_CONFIG_SETTINGS WHERE [Key]='isExpenseFeatureAvailable'").Rows[0][0]);
            // End of Rev 1.0

            var settings = new GridViewSettings();
            settings.Name = "Reimbursement";
            settings.CallbackRouteValues = new { Controller = "SalesReportSummary", Action = "GetReimbursementList" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Reimbursement List Report";

            settings.Columns.Add(column =>
            {
                column.Caption = "User Id";
                column.FieldName = "CONTACT";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Employee Name";
                column.FieldName = "EMPNAME";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Emp. Grade";
                column.FieldName = "EMPLOYEE_GRADE";
            });


            settings.Columns.Add(column =>
            {
                column.Caption = "State";
                column.FieldName = "STATE";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Designation";
                column.FieldName = "DEG_DESIGNATION";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Applied Amount";
                column.FieldName = "AMOUNT";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Approved Amount";
                column.FieldName = "APPROVED_AMOUNT";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Pending";
                column.FieldName = "PENDING_COUNT";
            });

            // Rev 1.0
            if (isExpenseFeatureAvailable == "1")
            {
                settings.Columns.Add(column =>
                {
                    column.Caption = "Confirmed";
                    column.FieldName = "CONFIRMED_COUNT";
                });
            }
            // End of Rev 1.0

            settings.Columns.Add(column =>
            {
                column.Caption = "Approved";
                column.FieldName = "APPROVED_COUNT";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Rejected";
                column.FieldName = "REJECTED_COUNT";
            });


            settings.Columns.Add(column =>
            {
                column.Caption = "Supervisor";
                column.FieldName = "REPORTTO";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Contact";
                column.FieldName = "REPORTCONTACT";
            });


            settings.Columns.Add(column =>
            {
                column.Caption = "Status";
                column.FieldName = "STATUS";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Updated By";
                column.FieldName = "UPDATEDBY";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Last Updated On";
                column.FieldName = "LASTUPDATED_ON";
            });


            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

        public ActionResult GetReimbursementView(ReimbursementDetails reimbursementDetails)
        {
            DataTable dtReimbursementDet = new DataTable();
            dtReimbursementDet = objgps.GetReimbursementDetailsReport(reimbursementDetails.Userid, reimbursementDetails.Month, reimbursementDetails.Year);
            //return PartialView("PartialGetReimbursementList", LGetReimbursement(frmdate));
            reimbursementDetails.GetReimbursementDetailsList = APIHelperMethods.ToModelList<ReimbursementDetailsList>(dtReimbursementDet);
            TempData["grddata"] = reimbursementDetails.GetReimbursementDetailsList;
            TempData.Keep();
            // Rev 1.0
            string isExpenseFeatureAvailable = "0";
            DBEngine obj1 = new DBEngine();
            isExpenseFeatureAvailable = Convert.ToString(obj1.GetDataTable("select [value] from FTS_APP_CONFIG_SETTINGS WHERE [Key]='isExpenseFeatureAvailable'").Rows[0][0]);
            ViewBag.isExpenseFeatureAvailable = isExpenseFeatureAvailable;
            // End of Rev 1.0
            return PartialView("_ReimbursementListViewList", reimbursementDetails);
        }
        public ActionResult GetViewReimbursementGrid(ReimbursementDetails reimbursementDetails)
        {
            reimbursementDetails.GetReimbursementDetailsList = (List<ReimbursementDetailsList>)TempData["grddata"];
            TempData["grddata"] = reimbursementDetails.GetReimbursementDetailsList;
            TempData.Keep();
            // Rev 1.0
            string isExpenseFeatureAvailable = "0";
            DBEngine obj1 = new DBEngine();
            isExpenseFeatureAvailable = Convert.ToString(obj1.GetDataTable("select [value] from FTS_APP_CONFIG_SETTINGS WHERE [Key]='isExpenseFeatureAvailable'").Rows[0][0]);
            ViewBag.isExpenseFeatureAvailable = isExpenseFeatureAvailable;
            // End of Rev 1.0
            return PartialView("_ReimbursementDetailsGrid", reimbursementDetails.GetReimbursementDetailsList);
        }

        public ActionResult GetReimbursementDetailsDataEdit(string user_contactId, decimal Apprvd_Dist, decimal Apprvd_Amt, string App_Rej_Remarks)
        {
            ReimbursementDetailsEdit reimbursementDetailsEdit = new ReimbursementDetailsEdit();
            reimbursementDetailsEdit.user_contactId = user_contactId;
            reimbursementDetailsEdit.Apprvd_Amt = Apprvd_Amt;
            reimbursementDetailsEdit.Apprvd_Dist = Apprvd_Dist;

            return PartialView("_ReimbursementDetailsDataApproved", reimbursementDetailsEdit);
        }

        // Rev 1.0
        public ActionResult GetReimbursementConfirmDetailsDataEdit(string user_contactId, string Conf_Rej_Remarks)
        {
            ReimbursementDetailsEdit reimbursementDetailsEdit = new ReimbursementDetailsEdit();
            reimbursementDetailsEdit.user_contactId = user_contactId;
           
            return PartialView("_ReimbursementDetailsDataConfirmed", reimbursementDetailsEdit);
        }
        // End of Rev 1.0


        [ValidateInput(false)]
        public ActionResult BatchEditingUpdateReimbursement(MVCxGridViewBatchUpdateValues<ReimbursementDetailsList, int> updateValues, ReimbursementDetailsList options)
        {
            // Rev 1.0
            string isExpenseFeatureAvailable = "0";
            DBEngine obj1 = new DBEngine();
            isExpenseFeatureAvailable = Convert.ToString(obj1.GetDataTable("select [value] from FTS_APP_CONFIG_SETTINGS WHERE [Key]='isExpenseFeatureAvailable'").Rows[0][0]);
            ViewBag.isExpenseFeatureAvailable = isExpenseFeatureAvailable;

            Boolean chkOk = true;

            if (isExpenseFeatureAvailable == "1")
            {
                if(options.is_ApprovedReject==1 || options.is_ApprovedReject == 2)
                {
                    DataTable dtCNT_NOTCONF = new DataTable();
                    ProcedureExecute proc = new ProcedureExecute("prc_ReimbursementConfirmed_Check");

                    proc.AddPara("@ACTION", "CONFIRMED_CHECK");
                    proc.AddPara("@APPLICATIONID_LIST", Convert.ToString(options.ListAppCode));
                    dtCNT_NOTCONF = proc.GetTable();

                    if (dtCNT_NOTCONF.Rows.Count > 0 && Convert.ToInt16(dtCNT_NOTCONF.Rows[0]["CNT_NOTCONF"]) > 0)
                    {
                        chkOk = false;
                    }
                }
            }

            if (chkOk == true)
            {
                // End of Rev 1.0
                DataTable dtReimbursementDet = new DataTable();
                ReimbursementDetails reimbursementDetails = new ReimbursementDetails();
                Boolean IsProcess = false;
                Boolean Success = false;
                foreach (var product in updateValues.Update)
                {
                    if (updateValues.IsValid(product))
                    {
                        Success = ReimbursementInsertUpdate(product, options);
                        IsProcess = true;
                        if (Success == true)
                        {
                            DBEngine objDB = new DBEngine();
                            DataTable dt = objDB.GetDataTable("select UserID,Expence_type,COnvert(VARCHAR(10),[Date],105) dts from FTS_Reimbursement_Application_Verified WHERE ApplicationID='" + product.ApplicationID + "'");
                            string msg = "";
                            if (options.is_ApprovedReject == 1)
                            {
                                msg = "Reimbursement for " + Convert.ToString(dt.Rows[0]["Expence_type"]) + " approved for the date " + Convert.ToString(dt.Rows[0]["dts"]) + ". Thanks.";
                                ViewBag.Message = "Approved successfully";
                                SendNotification(Convert.ToString(dt.Rows[0]["UserID"]), msg);
                            }
                            else if (options.is_ApprovedReject == 2)
                            {
                                msg = "Reimbursement for " + Convert.ToString(dt.Rows[0]["Expence_type"]) + " rejected for the date " + Convert.ToString(dt.Rows[0]["dts"]) + ". Thanks.";
                                ViewBag.Message = "Rejected successfully";
                                SendNotification(Convert.ToString(dt.Rows[0]["UserID"]), msg);


                            }
                            // Rev 1.0
                            else if (options.is_ApprovedReject == 3)
                            {
                                //msg = "Reimbursement for " + Convert.ToString(dt.Rows[0]["Expence_type"]) + " rejected for the date " + Convert.ToString(dt.Rows[0]["dts"]) + ". Thanks.";
                                ViewBag.Message = "Confirmed successfully";
                                //SendNotification(Convert.ToString(dt.Rows[0]["UserID"]), msg);

                            }
                            // End of Rev 1.0
                        }
                        else
                        {
                            options.is_ApprovedReject = 0;
                            ViewBag.Message = TempData["Message"];
                        }
                    }

                }
                dtReimbursementDet = objgps.GetReimbursementDetailsReport(options.Userid, options.Month, options.Year);
                reimbursementDetails.GetReimbursementDetailsList = APIHelperMethods.ToModelList<ReimbursementDetailsList>(dtReimbursementDet);
                TempData["grddata"] = reimbursementDetails.GetReimbursementDetailsList;
                TempData.Keep();
                return PartialView("_ReimbursementDetailsGrid", reimbursementDetails.GetReimbursementDetailsList);
                // Rev 1.0
            }
            else
            {
                DataTable dtReimbursementDet = new DataTable();
                ReimbursementDetails reimbursementDetails = new ReimbursementDetails();
                dtReimbursementDet = objgps.GetReimbursementDetailsReport(options.Userid, options.Month, options.Year);
                reimbursementDetails.GetReimbursementDetailsList = APIHelperMethods.ToModelList<ReimbursementDetailsList>(dtReimbursementDet);
                TempData["grddata"] = reimbursementDetails.GetReimbursementDetailsList;
                TempData.Keep();
                ViewBag.Message = "Wish to Approve/Reject? Please 'Confirm' first and then proceed.";
                return PartialView("_ReimbursementDetailsGrid", reimbursementDetails.GetReimbursementDetailsList);
            }
            // End of Rev 1.0

            
        }


        public void SendNotification(string user_id, string messagetext)
        {

            string status = string.Empty;
            try
            {
                DBEngine odbengine = new DBEngine();
                DataTable dt = odbengine.GetDataTable("select  device_token,musr.user_name,musr.user_id  from tbl_master_user as musr inner join tbl_FTS_devicetoken as token on musr.user_id=token.UserID  where musr.user_id='" + user_id + "' and musr.user_inactive='N'");

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToString(dt.Rows[i]["device_token"]) != "")
                        {
                            // Rev 2.0
                            //SendPushNotification(messagetext, Convert.ToString(dt.Rows[i]["device_token"]), Convert.ToString(dt.Rows[i]["user_name"]), Convert.ToString(dt.Rows[i]["user_id"]));

                            CRMEnquiriesController obj = new CRMEnquiriesController();
                            obj.SendPushNotification(messagetext, Convert.ToString(dt.Rows[i]["device_token"]), Convert.ToString(dt.Rows[i]["user_name"]), Convert.ToString(dt.Rows[i]["user_id"]), "reimbursement");
                            // End of Rev 2.0
                        }
                    }
                    status = "200";
                }


                else
                {

                    status = "202";
                }




            }
            catch
            {

            }

        }

        // Rev 2.0
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
        //                type = "reimbursement"
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
        // End of Rev 2.0

        public Boolean ReimbursementInsertUpdate(ReimbursementDetailsList obj, ReimbursementDetailsList obj2)
        {
            Boolean Success = false;
            List<ReimbursementDetailsList> list = new List<ReimbursementDetailsList>();
            try
            {
                var listAppCode = obj2.ListAppCode.Split('|');
                if (listAppCode.Length > 0)
                {
                    int pos = Array.IndexOf(listAppCode, obj.ApplicationID);
                    if (pos > -1)
                    {
                        // Rev 1.0 [ obj.Conf_Rej_Remarks added ]
                        DataTable dt = objgps.ReimbursementInsertUpdate(obj2.user_contactId, obj.ApplicationID, obj2.is_ApprovedReject, obj.Apprvd_Dist, obj.Apprvd_Amt, obj.App_Rej_Remarks, obj.Conf_Rej_Remarks);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                Success = Convert.ToBoolean(row["Success"]);
                                TempData["Message"] = Convert.ToString(row["Message"]);
                            }
                        }
                    }

                }
            }
            catch { }
            return Success;
        }

        public ActionResult LoadImageDocument(string MapExpenseID)
        {
            //String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
            string fileLocation = System.Configuration.ConfigurationManager.AppSettings["ReimbursementImageUrl"];
            List<ReimbursementApplicationbills> list = new List<ReimbursementApplicationbills>();
            DataTable dt = objgps.ReimbursementLoadImageDocument(MapExpenseID);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    ReimbursementApplicationbills obj = new ReimbursementApplicationbills();
                    obj.MapExpenseID = Convert.ToString(row["MapExpenseID"]);
                    obj.Bills = fileLocation + Convert.ToString(row["Bills"]);
                    obj.Image_Name = Convert.ToString(row["Image_Name"]);
                    list.Add(obj);
                }
            }

            return PartialView("_ReimbursementLoadDDocument", list);
        }


    }
}
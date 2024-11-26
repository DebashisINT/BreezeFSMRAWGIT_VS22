using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BusinessLogicLayer;
using BusinessLogicLayer.SalesmanTrack;
using BusinessLogicLayer.SalesTrackerReports;
using DataAccessLayer;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using Google.Apis.Auth.OAuth2;
using Models;
using MyShop.Models;
using SalesmanTrack;
using UtilityLayer;
using static MyShop.Models.UpdateOrderStatusModel;
//using DocumentFormat.OpenXml.Drawing.Charts;
//using DocumentFormat.OpenXml.Drawing.Charts;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class UpdateOrderStatusController : Controller
    {
        // GET: MYSHOP/UpdateOrderStatus       

        CommonBL objSystemSettings = new CommonBL();
        UserList lstuser = new UserList();
        OrderList objshop = new OrderList();
        DataTable dtuser = new DataTable();
        DataTable dtstate = new DataTable();
        DataTable dtshop = new DataTable();
        //List<OrderDetailsSummaryProducts> oproduct = new List<OrderDetailsSummaryProducts>();
        OrderUpdateDetailsSummary UpdateStatusWindow = new OrderUpdateDetailsSummary();
        List<OrderUpdateDetailsSummary> omodel = new List<OrderUpdateDetailsSummary>();
       // ProductDetails _productDetails = new ProductDetails();
        DataTable dtquery = new DataTable();
        NotificationBL notificationbl = new NotificationBL();
        public ActionResult Index()
        {
            try
            {
                string IsRetailOrderStatusRequired = objSystemSettings.GetSystemSettingsResult("IsRetailOrderStatusRequired");
                UpdateOrderStatusModel omodel = new UpdateOrderStatusModel();
                string userid = Session["userid"].ToString();
                omodel.selectedusrid = userid;
                omodel.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                omodel.Todate = DateTime.Now.ToString("dd-MM-yyyy");

                DataTable dt = new SalesSummary_Report().GetStateMandatory(Session["userid"].ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewBag.StateMandatory = dt.Rows[0]["IsStateMandatoryinReport"].ToString();
                }

                EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/UpdateOrderStatus/Index");
                
                ViewBag.CanView = rights.CanView;
                ViewBag.CanExport = rights.CanExport;         
                ViewBag.CanPrint = rights.CanPrint;
                ViewBag.CanInvoice= rights.CanInvoice;
                ViewBag.CanReadyToDispatch= rights.CanReadyToDispatch;
                ViewBag.CanDispatch= rights.CanDispatch;
                ViewBag.CanDeliver= rights.CanDeliver;
                ViewBag.IsRetailOrderStatusRequired = IsRetailOrderStatusRequired;


                STATUSLIST dataobj = new STATUSLIST();
                List<STATUSLIST> _STATUSLIST = new List<STATUSLIST>();


                
                dataobj.STATUSID = "Select";
                dataobj.STATUSVALUE = "Select";
                _STATUSLIST.Add(dataobj);


                if (rights.CanInvoice==true)
                {
                    dataobj = new STATUSLIST();
                    dataobj.STATUSID = "Invoiced";
                    dataobj.STATUSVALUE = "Invoiced";
                    _STATUSLIST.Add(dataobj);
                }
                if (rights.CanReadyToDispatch == true)
                {
                    dataobj = new STATUSLIST();
                    dataobj.STATUSID = "Ready To Dispatch";
                    dataobj.STATUSVALUE = "Ready To Dispatch";
                    _STATUSLIST.Add(dataobj);
                }
                if (rights.CanDispatch == true)
                {
                    dataobj = new STATUSLIST();
                    dataobj.STATUSID = "Dispatched";
                    dataobj.STATUSVALUE = "Dispatched";
                    _STATUSLIST.Add(dataobj);
                }
                if (rights.CanDeliver == true)
                {
                    dataobj = new STATUSLIST();
                    dataobj.STATUSID = "Delivered";
                    dataobj.STATUSVALUE = "Delivered";
                    _STATUSLIST.Add(dataobj);
                }

                omodel.STATUSLIST = _STATUSLIST;
                


                DataTable dtbranch = lstuser.GetHeadBranchList(Convert.ToString(Session["userbranchHierarchy"]), "HO");
                DataTable dtBranchChild = new DataTable();
                if (dtbranch.Rows.Count > 0)
                {
                    dtBranchChild = lstuser.GetChildBranch(Convert.ToString(Session["userbranchHierarchy"]));
                    if (dtBranchChild.Rows.Count > 0)
                    {
                        DataRow dr;
                        dr = dtbranch.NewRow();
                        dr[0] = 0;
                        dr[1] = "All";
                        dtbranch.Rows.Add(dr);
                        dtbranch.DefaultView.Sort = "BRANCH_ID ASC";
                        dtbranch = dtbranch.DefaultView.ToTable();
                    }
                }
                omodel.modelbranch = APIHelperMethods.ToModelList<GetBranch>(dtbranch);
                string h_id = omodel.modelbranch.First().BRANCH_ID.ToString();
                ViewBag.h_id = h_id;
               
                return View(omodel);

            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
        public ActionResult PartialOrderSummary(UpdateOrderStatusModel model)
        {
            try
            {
                string Is_PageLoad = string.Empty;
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
                List<GpsStatusClasstOutput> omel = new List<GpsStatusClasstOutput>();

                DataTable dt = new DataTable();

                if (model.Fromdate == null)
                {
                    model.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                }

                if (model.Todate == null)
                {
                    model.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                }
                if (model.Is_PageLoad == null)
                {
                    model.Is_PageLoad = "0";
                }
                   


                if (model.Is_PageLoad == "0") Is_PageLoad = "Ispageload";
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
                string shop = "";
                int j = 1;
                if (model.shopId != null && model.shopId.Count > 0)
                {
                    foreach (string item in model.shopId)
                    {
                        if (j > 1)
                            shop = shop + "," + item;
                        else
                            shop = item;
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
                //if (model.IsPaitentDetails != null)
                //{
                //    TempData["IsPaitentDetails"] = model.IsPaitentDetails;
                //    TempData.Keep();
                //}
               
                string Branch_Id = "";
                int l = 1;
                if (model.BranchId != null && model.BranchId.Count > 0)
                {
                    foreach (string item in model.BranchId)
                    {
                        if (l > 1)
                            Branch_Id = Branch_Id + "," + item;
                        else
                            Branch_Id = item;
                        l++;
                    }
                }
               

                if (model.Is_PageLoad != "0")
                {
                    double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;
                    if (days <= 30)
                    {
                      
                        dt = GetallorderListSummary(state, shop, datfrmat, dattoat, empcode, Branch_Id, Userid, model.UPDATESTATUS);
                       
                    }
                    omodel = APIHelperMethods.ToModelList<OrderUpdateDetailsSummary>(dt);
                }

                
                DataTable dtColmn = objshop.GetPageRetention(Session["userid"].ToString(), "UPDATE ORDER STATUS");
                if (dtColmn != null && dtColmn.Rows.Count > 0)
                {
                    ViewBag.RetentionColumn = dtColmn;
                }
                
                EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/UpdateOrderStatus/Index");
                ViewBag.CanPrint = rights.CanPrint;                
                TempData["UpdateOrderStatusList"] = omodel;
                return PartialView("_PartialUpdateOrderStatus", omodel);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
        public DataTable GetallorderListSummary(string stateid, string shopid, string fromdate, string todate, string EmployeeId, string Branch_Id, String Userid = "0", string UPDATESTATUS="")
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_UPDATEORDERSTATUS_LISTING");

            proc.AddPara("@start_date", fromdate);
            proc.AddPara("@end_date", todate);
            proc.AddPara("@stateID", stateid);
            proc.AddPara("@shop_id", shopid);
            proc.AddPara("@Employee_id", EmployeeId);
            proc.AddPara("@LOGIN_ID", Userid);            
            proc.AddPara("@BRANCHID", Branch_Id);
            proc.AddPara("@UPDATESTATUS", UPDATESTATUS);
            ds = proc.GetTable();

            return ds;
        }

        public JsonResult PrintSalesOrder(string OrderId)
        {

            string[] filePaths = new string[] { };
            string DesignPath = "";
            if (ConfigurationManager.AppSettings["IsDevelopedZone"] != null)
            {
                DesignPath = @"Reports\Reports\RepxReportDesign\OrderSummary\DocDesign\Designes";
            }
            else
            {
                DesignPath = @"Reports\RepxReportDesign\OrderSummary\DocDesign\Designes";
            }
            //DesignPath = @"Reports\Reports\RepxReportDesign\OrderSummary\DocDesign\Designes";
            string fullpath = Server.MapPath("~");
            fullpath = fullpath.Replace("ERP.UI\\", "");
            //fullpath = "D:\\FTS-GIT\\";
            string DesignFullPath = fullpath + DesignPath;
            filePaths = System.IO.Directory.GetFiles(DesignFullPath, "*.repx");
            List<DesignList> Listobj = new List<DesignList>();
            DesignList desig = new DesignList();
            foreach (string filename in filePaths)
            {
                //Rev 2.0
                desig = new DesignList();
                //Rev 2.0 End
                string reportname = Path.GetFileNameWithoutExtension(filename);
                string name = "";
                if (reportname.Split('~').Length > 1)
                {
                    name = reportname.Split('~')[0];
                }
                else
                {
                    name = reportname;
                }
                string reportValue = reportname;

                desig.name = name;
                desig.reportValue = reportname;
                Listobj.Add(desig);

            }
            //CmbDesignName.SelectedIndex = 0;
            return Json(Listobj, JsonRequestBehavior.AllowGet);
        }
        public class DesignList
        {
            public string name { get; set; }
            public string reportValue { get; set; }

        }
      
       

        public ActionResult EditUpdateOrderStatus(string OrderId)
        {
            try
            {
                dtquery = UpdateOrderStatusFetch(OrderId, "Edit");
                UpdateStatusWindow = APIHelperMethods.ToModel<OrderUpdateDetailsSummary>(dtquery);            
                
                return Json(UpdateStatusWindow);

            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public DataTable UpdateOrderStatusFetch(string OrderId, string Action)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_UPDATEORDERSTATUS_DETAILS");
            proc.AddPara("@OrderID", OrderId);           
            proc.AddPara("@Action", Action);
            ds = proc.GetTable();
            return ds;
        }

        public ActionResult UpdateOrderStatusModify(OrderUpdateDetailsSummary model)
        {
            if (model.ORDERSTATUSNEW != "Select")
            {               
                int output = OrderStatusModify(model.ORDERSTATUSNEW, model.OrderId, "Update");
         
                if (output > 0)
                {


                    
                    DataSet dataSet = new DataSet();
                    ProcedureExecute proc = new ProcedureExecute("PRC_PUSHNOTIFICATIONS");
                    proc.AddIntegerPara("@USERID", Convert.ToInt32(model.USERID));
                    proc.AddPara("@Action", "GETDATA");
                    dataSet = proc.GetDataSet();                 
                   
                    DataTable dtUser= dataSet.Tables[0];
                    if (dtUser.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtUser.Rows.Count; i++)
                        {
                            string user_name = dtUser.Rows[i]["user_name"].ToString();
                            string user_PHNO = dtUser.Rows[i]["user_loginId"].ToString();
                            string user_ID = dtUser.Rows[i]["user_id"].ToString();

                            string Mssg = "Order #" + model.OrderCode + " status has changed to " + model.ORDERSTATUSNEW;

                            int returnmssge = notificationbl.Savenotification(user_PHNO, Mssg);

                            SendNotification(user_PHNO, Mssg, model.OrderCode, model.ORDERSTATUSNEW);
                        }
                    }
                    return Json("Success");
                }
                else
                {
                    return Json("failure");
                }
            }
            else
            {
                return Json("failure");
            }
        }
        public JsonResult SendNotification(string Mobiles, string messagetext, string OrderCode, string ORDERSTATUS)
        {

            string status = string.Empty;
            try
            {
                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_PUSHNOTIFICATIONS");
                proc.AddPara("@MobileNO", Mobiles);
                proc.AddPara("@Action", "GETDEVICETOKEN");
                dt = proc.GetTable();
               
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToString(dt.Rows[i]["device_token"]) != "")
                        {                            
                            SendPushNotification(messagetext, Convert.ToString(dt.Rows[i]["device_token"]), Convert.ToString(dt.Rows[i]["user_name"]), Convert.ToString(dt.Rows[i]["user_id"]), "UpdateOrderStatus", OrderCode, ORDERSTATUS);
                           
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
        public void SendPushNotification(string message = "", string deviceid = "", string Customer = "", string UserID = "", string type = "",
          string lead_date = "", string enquiry_type = "", string title = "")       
        {
            try
            {
                DBEngine odbengine = new DBEngine();
                string fileName = "", projectname = "";
                DataSet dataSet = new DataSet();
                ProcedureExecute proc = new ProcedureExecute("PRC_PUSHNOTIFICATIONS");
               // proc.AddIntegerPara("@USERID", Convert.ToInt32(Session["userid"]));
                proc.AddPara("@Action", "GETDATA");
                dataSet = proc.GetDataSet();

                DataTable dt = dataSet.Tables[1];
                //DataTable dt = odbengine.GetDataTable("select JSONFILE_NAME, PROJECT_NAME from FSM_CONFIG_FIREBASENITIFICATION WHERE ID=1");

                if (dt.Rows.Count > 0)
                {
                    fileName = System.Web.Hosting.HostingEnvironment.MapPath("~/" + Convert.ToString(dt.Rows[0]["JSONFILE_NAME"]));
                    projectname = Convert.ToString(dt.Rows[0]["PROJECT_NAME"]);
                }


                string scopes = "https://www.googleapis.com/auth/firebase.messaging";
                var bearertoken = ""; // Bearer Token in this variable
                using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))

                {

                    bearertoken = GoogleCredential
                      .FromStream(stream) // Loads key file
                      .CreateScoped(scopes) // Gathers scopes requested
                      .UnderlyingCredential // Gets the credentials
                      .GetAccessTokenForRequestAsync().Result; // Gets the Access Token

                }

                ///--------Calling FCM-----------------------------

                var clientHandler = new HttpClientHandler();
                var client = new HttpClient(clientHandler);

                client.BaseAddress = new Uri("https://fcm.googleapis.com/v1/projects/" + projectname + "/messages:send"); // FCM HttpV1 API

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));               
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearertoken); // Authorization Token in this variable

                //---------------Assigning Of data To Model --------------

                Root rootObj = new Root();
                rootObj.message = new Message();
                rootObj.message.token = deviceid;  
                rootObj.message.data = new Data();
                rootObj.message.data.UserName = Customer;
                rootObj.message.data.UserID = UserID;
                rootObj.message.data.body = message;
                rootObj.message.data.type = type;
                rootObj.message.data.header = "Update Order Status";




                //-------------Convert Model To JSON ----------------------

                var jsonObj = new JavaScriptSerializer().Serialize(rootObj);

                //------------------------Calling Of FCM Notify API-------------------

                var data = new StringContent(jsonObj, Encoding.UTF8, "application/json");
                data.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = client.PostAsync("https://fcm.googleapis.com/v1/projects/" + projectname + "/messages:send", data).Result; // Calling The FCM httpv1 API

                //---------- Deserialize Json Response from API ----------------------------------

                var jsonResponse = response.Content.ReadAsStringAsync().Result;
                var responseObj = new JavaScriptSerializer().DeserializeObject(jsonResponse);
               
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }
        public int OrderStatusModify(string ORDERSTATUSNEW, long OrderId,string Action)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_UPDATEORDERSTATUS_DETAILS");
            proc.AddPara("@OrderID", OrderId);
            proc.AddPara("@ORDERSTATUSNEW", ORDERSTATUSNEW);            
            proc.AddPara("@Action", Action);
            proc.AddIntegerPara("@USERID", Convert.ToInt32(Session["userid"])); 
            int i = proc.RunActionQuery();
            return i;
        }

        public ActionResult ExporUpdateOrderStatusList(int type)
        {
            ViewData["UpdateOrderStatusList"] = TempData["UpdateOrderStatusList"];
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetOrderRegisterList(), ViewData["UpdateOrderStatusList"]);
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetOrderRegisterList(), ViewData["UpdateOrderStatusList"]);
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetOrderRegisterList(), ViewData["UpdateOrderStatusList"]);
                case 4:
                    return GridViewExtension.ExportToRtf(GetOrderRegisterList(), ViewData["UpdateOrderStatusList"]);
                case 5:
                    return GridViewExtension.ExportToCsv(GetOrderRegisterList(), ViewData["UpdateOrderStatusList"]);
                //break;

                default:
                    break;
            }
            TempData["UpdateOrderStatusList"] = ViewData["UpdateOrderStatusList"];
            return null;
        }
        private GridViewSettings GetOrderRegisterList()
        {
            
            DataTable dtColmn = objshop.GetPageRetention(Session["userid"].ToString(), "UPDATE ORDER STATUS");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;
            }
           

            var settings = new GridViewSettings();
            settings.Name = "gridsummarylist";
           
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Update Order Status";

               
                settings.Columns.Add(x =>
                {
                    x.FieldName = "EmployeeName";
                    x.Caption = "Employee Name";
                    x.VisibleIndex = 1;
                    x.Width = System.Web.UI.WebControls.Unit.Percentage(10);

                  
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='EmployeeName'");
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
                    x.FieldName = "BRANCHDESC";
                    x.Caption = "Branch";
                    x.VisibleIndex = 2;
                    x.Width = System.Web.UI.WebControls.Unit.Percentage(10);

                   
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='BRANCHDESC'");
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
                    x.FieldName = "shop_name";
                    x.Caption = "Shop Name";
                    x.VisibleIndex = 3;
                    x.Width = System.Web.UI.WebControls.Unit.Percentage(10);

                   
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='shop_name'");
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
                    x.FieldName = "ENTITYCODE";
                    x.Caption = "Code";
                    x.VisibleIndex = 4;
                    x.Width = System.Web.UI.WebControls.Unit.Percentage(10);

                  
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ENTITYCODE'");
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
                    x.FieldName = "address";
                    x.Caption = "Address";
                    x.VisibleIndex = 5;
                    x.Width = System.Web.UI.WebControls.Unit.Percentage(20);

                  
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='address'");
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
                    x.FieldName = "owner_contact_no";
                    x.Caption = "Contact";
                    x.VisibleIndex = 6;
                    x.Width = System.Web.UI.WebControls.Unit.Percentage(10);

                    
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='owner_contact_no'");
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
                    x.FieldName = "Shoptype";
                    x.Caption = "Shop type";
                    x.VisibleIndex = 7;
                    x.Width = System.Web.UI.WebControls.Unit.Percentage(10);

                   
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Shoptype'");
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
                    x.FieldName = "date";
                    x.Caption = "Order Date";
                    x.VisibleIndex = 8;
                    x.Width = System.Web.UI.WebControls.Unit.Percentage(10);

                  
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='date'");
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
                    x.FieldName = "OrderCode";
                    x.Caption = "Order Number";
                    x.VisibleIndex = 9;
                    x.Width = System.Web.UI.WebControls.Unit.Percentage(15);

                   
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='OrderCode'");
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
                    x.FieldName = "order_amount";
                    x.Caption = "Order Value";
                    x.VisibleIndex = 10;
                    x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                    x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                    x.PropertiesEdit.DisplayFormatString = "0.00";

                  
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='order_amount'");
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
                    x.FieldName = "ORDERSTATUS";
                    x.Caption = "Order Status";
                    x.VisibleIndex = 11;
                    x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                    x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                    //x.PropertiesEdit.DisplayFormatString = "0.00";


                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ORDERSTATUS'");
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
                int k = objshop.InsertPageRetention(Col, Session["userid"].ToString(), "ORDER SUMMARY");

                return Json(k, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
        
    }
}
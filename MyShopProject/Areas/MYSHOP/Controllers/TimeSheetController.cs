/****************************************************************************************************************************
 * 1.0       10/09/2024        V2.0.48          Sanchita          27690: Quotation Notification issue @ Eurobond
 * ***************************************************************************************************************************/
using BusinessLogicLayer;
using DataAccessLayer;
using MyShop.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class TimeSheetController : Controller
    {
        //
        // GET: /MYSHOP/TimeSheet/

        public ActionResult TimwSheetMaster()
        {
            return View();
        }


        #region Client
        public PartialViewResult _PartialClientGrid()
        {
            return PartialView(GetClient());
        }

        public PartialViewResult _PartialClientSelectionGrid(string empcode)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;

            ReportsDataContext dc = new ReportsDataContext(connectionString);
            List<FTS_EMPLOYEE_CLIENTMAP> obj = (from d in dc.FTS_EMPLOYEE_CLIENTMAPs
                                                 where d.EC_EMPLOYEECODE == empcode
                                                 select d).ToList();

            ViewBag.SelectedIds = obj;
            return PartialView(GetClient());
        }

        public IEnumerable GetClient()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);
            ReportsDataContext dc = new ReportsDataContext(connectionString);
            var q = from d in dc.FTS_CLIENT_MASTERs
                    select d;
            return q;

        }

        [HttpPost]
        public ActionResult ClientAddNew(FTS_CLIENT_MASTER ftsAdd)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            using (ReportsDataContext dc = new ReportsDataContext(connectionString))
            {
                dc.FTS_CLIENT_MASTERs.InsertOnSubmit(ftsAdd);
                dc.SubmitChanges();
            }

            return PartialView("~/Areas/MYSHOP/Views/TimeSheet/_PartialClientGrid.cshtml", GetClient());

        }

        [HttpPost]
        public ActionResult ClientUpdate(FTS_CLIENT_MASTER ftsUpdate)
        {


            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            using (ReportsDataContext dc = new ReportsDataContext(connectionString))
            {

                var query = (from p in dc.FTS_CLIENT_MASTERs
                             where p.Client_Id == ftsUpdate.Client_Id
                             select p).ToList();
                foreach (FTS_CLIENT_MASTER ord in query)
                {
                    ord.Client_Name = ftsUpdate.Client_Name;
                    ord.Client_Address = ftsUpdate.Client_Address;
                    ord.Client_ContactNummber = ftsUpdate.Client_ContactNummber;
                    ord.Client_IsActive = ftsUpdate.Client_IsActive;
                    ord.Client_remarks = ftsUpdate.Client_remarks;
                }

                try
                {
                    dc.SubmitChanges();
                }
                catch (Exception e)
                {

                }
            }


            return PartialView("~/Areas/MYSHOP/Views/TimeSheet/_PartialClientGrid.cshtml", GetClient());

        }

        [HttpPost]
        public ActionResult ClientDelete(FTS_CLIENT_MASTER ftsDelete)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;

            using (ReportsDataContext dc = new ReportsDataContext(connectionString))
            {
                var query = (from p in dc.FTS_CLIENT_MASTERs
                             where p.Client_Id == ftsDelete.Client_Id
                             select p).ToList();
                dc.FTS_CLIENT_MASTERs.DeleteAllOnSubmit(query);
                dc.SubmitChanges();
            }


            return PartialView("~/Areas/MYSHOP/Views/TimeSheet/_PartialClientGrid.cshtml", GetClient());
        }

        #endregion
        #region Project
        public PartialViewResult _PartialProjectGrid()
        {
            return PartialView(GetProject());
        }

        public PartialViewResult _PartialProjectSelectionGrid(string empcode)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;

            ReportsDataContext dc = new ReportsDataContext(connectionString);
            List<FTS_EMPLOYEE_PROJECTMAP> obj = (from d in dc.FTS_EMPLOYEE_PROJECTMAPs
                                                 where d.EP_EMPLOYEECODE == empcode
                                                 select d).ToList();

            ViewBag.SelectedIds = obj;


            return PartialView(GetProject());
        }

        public IEnumerable GetProject()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);
            ReportsDataContext dc = new ReportsDataContext(connectionString);
            var q = from d in dc.FTS_PROJECT_MASTERs
                    select d;
            return q;

        }

        [HttpPost]
        public ActionResult ProjectAddNew(FTS_PROJECT_MASTER ftsAdd)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            using (ReportsDataContext dc = new ReportsDataContext(connectionString))
            {
                dc.FTS_PROJECT_MASTERs.InsertOnSubmit(ftsAdd);
                dc.SubmitChanges();
            }

            return PartialView("~/Areas/MYSHOP/Views/TimeSheet/_PartialProjectGrid.cshtml", GetProject());

        }

        [HttpPost]
        public ActionResult ProjectUpdate(FTS_PROJECT_MASTER ftsUpdate)
        {


            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            using (ReportsDataContext dc = new ReportsDataContext(connectionString))
            {

                var query = (from p in dc.FTS_PROJECT_MASTERs
                             where p.Project_Id == ftsUpdate.Project_Id
                             select p).ToList();
                foreach (FTS_PROJECT_MASTER ord in query)
                {
                    ord.Project_Name = ftsUpdate.Project_Name;
                    ord.Project_Description = ftsUpdate.Project_Description;
                    ord.Project_IsActive = ftsUpdate.Project_IsActive;
                }

                try
                {
                    dc.SubmitChanges();
                }
                catch (Exception e)
                {

                }
            }


            return PartialView("~/Areas/MYSHOP/Views/TimeSheet/_PartialProjectGrid.cshtml", GetProject());

        }

        [HttpPost]
        public ActionResult ProjectDelete(FTS_PROJECT_MASTER ftsDelete)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;

            using (ReportsDataContext dc = new ReportsDataContext(connectionString))
            {
                var query = (from p in dc.FTS_PROJECT_MASTERs
                             where p.Project_Id == ftsDelete.Project_Id
                             select p).ToList();
                dc.FTS_PROJECT_MASTERs.DeleteAllOnSubmit(query);
                dc.SubmitChanges();
            }


            return PartialView("~/Areas/MYSHOP/Views/TimeSheet/_PartialProjectGrid.cshtml", GetProject());
        }

        #endregion
        #region Activity
        public PartialViewResult _PartialActivityGrid()
        {
            return PartialView(GetActivity());
        }

        public PartialViewResult _PartialActivitySelectionGrid(string empcode)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;

            ReportsDataContext dc = new ReportsDataContext(connectionString);
            List<FTS_EMPLOYEE_ACTIVITYMAP> obj = (from d in dc.FTS_EMPLOYEE_ACTIVITYMAPs
                                                 where d.EA_EMPLOYEECODE == empcode
                                                 select d).ToList();

            ViewBag.SelectedIds = obj;


            return PartialView(GetActivity());
        }

        public IEnumerable GetActivity()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);
            ReportsDataContext dc = new ReportsDataContext(connectionString);
            var q = from d in dc.FTS_ACTIVITY_MASTERs
                    select d;
            return q;

        }

        [HttpPost]
        public ActionResult ActivityAddNew(FTS_ACTIVITY_MASTER ftsAdd)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            using (ReportsDataContext dc = new ReportsDataContext(connectionString))
            {
                dc.FTS_ACTIVITY_MASTERs.InsertOnSubmit(ftsAdd);
                dc.SubmitChanges();
            }

            return PartialView("~/Areas/MYSHOP/Views/TimeSheet/_PartialActivityGrid.cshtml", GetActivity());

        }

        [HttpPost]
        public ActionResult ActivityUpdate(FTS_ACTIVITY_MASTER ftsUpdate)
        {


            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            using (ReportsDataContext dc = new ReportsDataContext(connectionString))
            {

                var query = (from p in dc.FTS_ACTIVITY_MASTERs
                             where p.Activity_Id == ftsUpdate.Activity_Id
                             select p).ToList();
                foreach (FTS_ACTIVITY_MASTER ord in query)
                {
                    ord.Activity_Name = ftsUpdate.Activity_Name;
                    ord.Activity_Description = ftsUpdate.Activity_Description;
                    ord.Activity_IsActive = ftsUpdate.Activity_IsActive;

                }

                try
                {
                    dc.SubmitChanges();
                }
                catch (Exception e)
                {

                }
            }


            return PartialView("~/Areas/MYSHOP/Views/TimeSheet/_PartialActivityGrid.cshtml", GetActivity());

        }

        [HttpPost]
        public ActionResult ActivityDelete(FTS_ACTIVITY_MASTER ftsDelete)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;

            using (ReportsDataContext dc = new ReportsDataContext(connectionString))
            {
                var query = (from p in dc.FTS_ACTIVITY_MASTERs
                             where p.Activity_Id == ftsDelete.Activity_Id
                             select p).ToList();
                dc.FTS_ACTIVITY_MASTERs.DeleteAllOnSubmit(query);
                dc.SubmitChanges();
            }


            return PartialView("~/Areas/MYSHOP/Views/TimeSheet/_PartialActivityGrid.cshtml", GetActivity());
        }

        #endregion
        #region Time Sheet Approval

        public ActionResult TimeSheetApproval()
        {
            return View();
        }

        public PartialViewResult _PartialSheetApprovalGrid(bool isMassDeleteClicked,string fromdate,string todate,string userid)
        {
            ViewBag.IsAll = isMassDeleteClicked;

            DateTime fdate = DateTime.Now;
            DateTime tdate = DateTime.Now;

            if (!string.IsNullOrEmpty(fromdate))
            {
                fdate = DateTime.ParseExact(fromdate, "dd-MM-yyyy", CultureInfo.CurrentCulture);
            }

            if (!string.IsNullOrEmpty(todate))
            {
                tdate = DateTime.ParseExact(todate, "dd-MM-yyyy", CultureInfo.CurrentCulture);
            }


            return PartialView(GetTimeSheet(fdate,tdate,userid));
        }
        public IEnumerable GetTimeSheet(DateTime? fromdate, DateTime todate, string userid)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            if(!string.IsNullOrEmpty(userid) && userid!="0" ){
            
            
            ReportsDataContext dc = new ReportsDataContext(connectionString);
            var q = from d in dc.V_TIMESHEETs
                    where d.timesheet_date >= Convert.ToDateTime(fromdate) && d.timesheet_date <= Convert.ToDateTime(todate)
                    && d.user_id==Convert.ToInt32(userid)
                    select d;
            return q;
            }
            else
            {
                            ReportsDataContext dc = new ReportsDataContext(connectionString);
            var q = from d in dc.V_TIMESHEETs
                    where d.timesheet_date>=Convert.ToDateTime(fromdate) && d.timesheet_date<=Convert.ToDateTime(todate)
                    select d;
            return q;
            }

        }


        #endregion

        [HttpPost]
        public JsonResult ApproveReject(string timesheet_id,string status)
        {
            string output = "";
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("API_PRC_TIMESHEET");

            proc.AddPara("@timesheet_id", timesheet_id);
            proc.AddPara("@Action", status);
            proc.AddPara("@user_id",HttpContext.Session["userid"]);
            ds = proc.GetDataSet();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                output = Convert.ToString(ds.Tables[0].Rows[0][0]);
            }
            if (ds != null && ds.Tables[1].Rows.Count > 0)
            {
                string mobile = Convert.ToString(ds.Tables[1].Rows[0][1]);
                string msg = Convert.ToString(ds.Tables[1].Rows[0][0]);
                SendNotification(mobile, msg);

            }

            return Json(output);
        }


        public void SendNotification(string Mobiles, string messagetext)
        {

            string status = string.Empty;
            try
            {
                DBEngine odbengine = new DBEngine();
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
                            obj.SendPushNotification(messagetext, Convert.ToString(dt.Rows[i]["device_token"]), Convert.ToString(dt.Rows[i]["user_name"]), Convert.ToString(dt.Rows[i]["user_id"]), "timesheet");
                            // End of Rev 1.0
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
        //                type = "timesheet"
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


        [HttpPost]
        
        public JsonResult MassApproveReject(string timesheet_id, string status)
        {
            string output = "";

            if (!string.IsNullOrEmpty(timesheet_id))
            {
                string[] arrId = timesheet_id.Split(',');
                foreach (string id in arrId)
                {
                    DataSet ds = new DataSet();
                    ProcedureExecute proc = new ProcedureExecute("API_PRC_TIMESHEET");

                    proc.AddPara("@timesheet_id", id);
                    proc.AddPara("@Action", status);
                    proc.AddPara("@user_id", HttpContext.Session["userid"]);
                    ds = proc.GetDataSet();

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        output = Convert.ToString(ds.Tables[0].Rows[0][0]);
                    }
                    if (ds != null && ds.Tables[1].Rows.Count > 0)
                    {
                        string mobile = Convert.ToString(ds.Tables[1].Rows[0][1]);
                        string msg = Convert.ToString(ds.Tables[1].Rows[0][0]);
                        SendNotification(mobile, msg);

                    }


                }

               
            }
            else
            {
                output = "Please select atleat one entry to proceed.";
            }

            return Json(output);
        }


        public PartialViewResult _PartialProductSelectionGrid(string empcode)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;

            ReportsDataContext dc = new ReportsDataContext(connectionString);
            List<FTS_EMPLOYEE_PRODUCTMAP> obj= (from d in dc.FTS_EMPLOYEE_PRODUCTMAPs where d.EP_EMPLOYEECODE==empcode
                    select d).ToList();

            ViewBag.SelectedIds = obj;

            return PartialView(GetProduct());
        }

        public IEnumerable GetProduct()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);
            ReportsDataContext dc = new ReportsDataContext(connectionString);
            var q = from d in dc.V_PRODUCTLISTs
                    select d;
            return q;

        }

        public JsonResult GetEmployee()
        {
            List<EmpListTimeSheet> obj = new List<EmpListTimeSheet>();
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("API_PRC_TIMESHEET");
            proc.AddPara("@Action", "EMPLOYEEDROPDOWN");
            ds = proc.GetTable();

            if (ds != null && ds.Rows.Count > 0)
            {
                obj = (from DataRow Dr in ds.Rows
                       select new EmpListTimeSheet()
                       {
                           EmpCode=Convert.ToString(Dr["EmpCode"]),
                           NAME = Convert.ToString(Dr["NAME"])
                       }).ToList();
            }

            return Json(obj);


        }

        public JsonResult SaveAttatchment(string product,string client,string activity,string project,string empcode)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            if(!string.IsNullOrEmpty(product))
            {

                using (ReportsDataContext dc = new ReportsDataContext(connectionString))
                {
                    var query = (from p in dc.FTS_EMPLOYEE_PRODUCTMAPs
                                 where p.EP_EMPLOYEECODE == empcode
                                 select p).ToList();
                    dc.FTS_EMPLOYEE_PRODUCTMAPs.DeleteAllOnSubmit(query);
                    dc.SubmitChanges();
                }
                string[] lst = product.Split(',');

                foreach (string item in lst)
                {
                    FTS_EMPLOYEE_PRODUCTMAP obj = new FTS_EMPLOYEE_PRODUCTMAP();
                    obj.EP_PRODUCT_ID = Convert.ToInt32(item);
                    obj.EP_EMPLOYEECODE = empcode;

                    using (ReportsDataContext dc = new ReportsDataContext(connectionString))
                    {
                        dc.FTS_EMPLOYEE_PRODUCTMAPs.InsertOnSubmit(obj);
                        dc.SubmitChanges();
                    }
                }

                

            }

            if (!string.IsNullOrEmpty(activity))
            {

                using (ReportsDataContext dc = new ReportsDataContext(connectionString))
                {
                    var query = (from p in dc.FTS_EMPLOYEE_ACTIVITYMAPs
                                 where p.EA_EMPLOYEECODE == empcode
                                 select p).ToList();
                    dc.FTS_EMPLOYEE_ACTIVITYMAPs.DeleteAllOnSubmit(query);
                    dc.SubmitChanges();
                }
                string[] lst = activity.Split(',');

                foreach (string item in lst)
                {
                    FTS_EMPLOYEE_ACTIVITYMAP obj = new FTS_EMPLOYEE_ACTIVITYMAP();
                    obj.EA_ACTIVITY_ID = Convert.ToInt32(item);
                    obj.EA_EMPLOYEECODE = empcode;

                    using (ReportsDataContext dc = new ReportsDataContext(connectionString))
                    {
                        dc.FTS_EMPLOYEE_ACTIVITYMAPs.InsertOnSubmit(obj);
                        dc.SubmitChanges();
                    }
                }



            }


            if (!string.IsNullOrEmpty(project))
            {

                using (ReportsDataContext dc = new ReportsDataContext(connectionString))
                {
                    var query = (from p in dc.FTS_EMPLOYEE_PROJECTMAPs
                                 where p.EP_EMPLOYEECODE == empcode
                                 select p).ToList();
                    dc.FTS_EMPLOYEE_PROJECTMAPs.DeleteAllOnSubmit(query);
                    dc.SubmitChanges();
                }
                string[] lst = project.Split(',');

                foreach (string item in lst)
                {
                    FTS_EMPLOYEE_PROJECTMAP obj = new FTS_EMPLOYEE_PROJECTMAP();
                    obj.EP_PROJECT_ID = Convert.ToInt32(item);
                    obj.EP_EMPLOYEECODE = empcode;

                    using (ReportsDataContext dc = new ReportsDataContext(connectionString))
                    {
                        dc.FTS_EMPLOYEE_PROJECTMAPs.InsertOnSubmit(obj);
                        dc.SubmitChanges();
                    }
                }



            }


            if (!string.IsNullOrEmpty(client))
            {

                using (ReportsDataContext dc = new ReportsDataContext(connectionString))
                {
                    var query = (from p in dc.FTS_EMPLOYEE_CLIENTMAPs
                                 where p.EC_EMPLOYEECODE == empcode
                                 select p).ToList();
                    dc.FTS_EMPLOYEE_CLIENTMAPs.DeleteAllOnSubmit(query);
                    dc.SubmitChanges();
                }
                string[] lst = client.Split(',');

                foreach (string item in lst)
                {
                    FTS_EMPLOYEE_CLIENTMAP obj = new FTS_EMPLOYEE_CLIENTMAP();
                    obj.EC_CLIENT_ID = Convert.ToInt32(item);
                    obj.EC_EMPLOYEECODE = empcode;

                    using (ReportsDataContext dc = new ReportsDataContext(connectionString))
                    {
                        dc.FTS_EMPLOYEE_CLIENTMAPs.InsertOnSubmit(obj);
                        dc.SubmitChanges();
                    }
                }



            }

            string output = "Sucsess";

            return Json(output);


        }

    }


    public class EmpListTimeSheet
    {
        public string EmpCode { get; set; }
        public string NAME { get; set; }
    }
}
/**************************************************************************************************************************
 * Rev 1.0      Sanchita     V2.0.40        Console error after click "Total Employees" box in FSM dashboard attendance tab
 *                                          Refer: 25894
 *************************************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BusinessLogicLayer;
using SalesmanTrack;
using System.Data;
using UtilityLayer;
using System.Web.Script.Serialization;
using MyShop.Models;
using BusinessLogicLayer.SalesmanTrack;
using DevExpress.Web.Mvc;
using DevExpress.Web;
using System.Data.SqlClient;


namespace MyShop.Areas.MYSHOP.Controllers
{
    public class AttendanceController : Controller
    {
      
        UserList lstuser = new UserList();
        Attendanceclass objshop = new Attendanceclass();

        DataTable dtuser = new DataTable();
        public ActionResult List()
        {
            try
            {
             
                AttendanceModelInput omodel = new AttendanceModelInput();
                string userid = Session["userid"].ToString();
                dtuser = lstuser.GetUserList(userid);
                // dtuser = lstuser.GetUserList();
                List<GetUsersshopsforattendance> model = new List<GetUsersshopsforattendance>();

                model = APIHelperMethods.ToModelList<GetUsersshopsforattendance>(dtuser);
                omodel.userlsit = model;
                if (TempData["Attendanceuser"] != null)
                {
                    omodel.selectedusrid = TempData["Attendanceuser"].ToString();
                    TempData.Clear();
                }
                return View(omodel);

            }
            catch
            {

                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetAttendanceIndex(string User)
        {
            TempData["Attendanceuser"] = User;
            return RedirectToAction("List");
        }


        public ActionResult GetAttendanceList(AttendanceModelInput model)
        {
            if (Session["userid"] != null)
            {
                try
                {
                    String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
                    List<AttendancerecordModel> omel = new List<AttendancerecordModel>();

                    DataTable dt = new DataTable();

                    if (model.Fromdate == null)
                    {

                        model.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                    }

                    if (model.Todate == null)
                    {

                        model.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                    }


                    string datfrmat = model.Fromdate.Split('-')[2] + '-' + model.Fromdate.Split('-')[1] + '-' + model.Fromdate.Split('-')[0];
                    string dattoat = model.Todate.Split('-')[2] + '-' + model.Todate.Split('-')[1] + '-' + model.Todate.Split('-')[0];

                    dt = objshop.GetAttendanceist(model.selectedusrid, datfrmat, dattoat);

                    if (dt.Rows.Count > 0)
                    {

                        omel = APIHelperMethods.ToModelList<AttendancerecordModel>(dt);
                        TempData["Exportattendance"] = omel;
                        TempData.Keep();
                    }
                    else
                    {
                        return PartialView("_PartialAttendance", omel);

                    }
                    return PartialView("_PartialAttendance", omel);
                }
                catch
                {

                    //return Redirect("/OMS/Signoff.aspx", new {are});

                    return RedirectToAction("Logout", "Login", new { Area = "" });
                }
            }
            else
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }


        public ActionResult ExportAttendance(int type)
        {
            List<AttendancerecordModel> model = new List<AttendancerecordModel>();
            if (TempData["Exportattendance"] != null)
            {

                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(), TempData["Exportattendance"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(), TempData["Exportattendance"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(), TempData["Exportattendance"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(), TempData["Exportattendance"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(), TempData["Exportattendance"]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetEmployeeBatchGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "Attendance";
           // settings.CallbackRouteValues = new { Controller = "Employee", Action = "ExportEmployee" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Attendance Report";

     
           settings.Columns.Add(column =>
            {
                column.Caption = "Employee Name";
                column.FieldName = "Employeename";
              

            });

           settings.Columns.Add(column =>
           {
               column.Caption = "Supervisor";
               column.FieldName = "REPORTTO";


           });

           settings.Columns.Add(column =>
           {
               column.Caption = "Designation";
               column.FieldName = "Designation";


           });

           settings.Columns.Add(column =>
           {
               column.Caption = "State";
               column.FieldName = "state";


           });

           settings.Columns.Add(column =>
           {
               column.Caption = "User ID";
               column.FieldName = "UserLogin";


           });

           settings.Columns.Add(column =>
           {
               column.Caption = "Date";
               column.FieldName = "login_date";
               column.PropertiesEdit.DisplayFormatString = "dd/MM/yyyy";

           });

             settings.Columns.Add(column =>
           {
               column.Caption = "Attendance Type";
               column.FieldName = "ATTEN_STATUS";


           });

           settings.Columns.Add(column =>
           {
               column.Caption = "Work/Leave Type";
               column.FieldName = "WORK_LEAVE_TYPE";


           });
                
            settings.Columns.Add(column =>
            {
                column.Caption = "In Time";
                column.FieldName = "login_time";
                column.PropertiesEdit.DisplayFormatString = "dd/MM/yyyy h:mm tt";
              
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Login Status";
                column.FieldName = "LATE_CNT";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Out Time";
                column.FieldName = "logout_time";
                column.PropertiesEdit.DisplayFormatString = "dd/MM/yyyy h:mm tt";

            }); 


             settings.Columns.Add(column =>
            {
                column.Caption = "Undertime (HH:MM)";
                column.FieldName = "UNDERTIME";
              
            });
             settings.Columns.Add(column =>
            {
                column.Caption = "GPS Inactive Duration (HH:MM)";
                column.FieldName = "GPS_INACTIVE_DURATION";
              
            });
             settings.Columns.Add(column =>
            {
                column.Caption = "Idle Time (HH:MM)";
                column.FieldName = "IDEAL_TIME";
              
            });
             settings.Columns.Add(column =>
            {
                column.Caption = "Idle Time Count";
                column.FieldName = "IDEALTIME_CNT";
              
            });
               
            settings.Columns.Add(column =>
            {
                column.Caption = "Total Working Duration";
                column.FieldName = "duration";
              
            });

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }


        public JsonResult GetTotalAttendance()
        {
            string status = string.Empty;
            try
            {
                List<totalAttendance> list = new List<totalAttendance>();

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_FTSATTNDASHBOARD", sqlcon);
                sqlcmd.Parameters.Add("@Action", "GETTOTALEMP");
                sqlcmd.Parameters.Add("@CREATE_USERID", Session["userid"].ToString());
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();

                if (dt != null && dt.Rows.Count > 0)
                {
                   
                    foreach (DataRow item in dt.Rows)
                    {
                        totalAttendance obj = new totalAttendance();
                        obj.Employee = Convert.ToString(item["Employee"]);
                        obj.Desg = Convert.ToString(item["Desg"]);
                        obj.Dept = Convert.ToString(item["Dept"]);
                        obj.Supervisor = Convert.ToString(item["Supervisor"]);
                        obj.Contact = Convert.ToString(item["Contact"]);
                        list.Add(obj);
                    }
                }

                // Rev 1.0
                //return Json(list);
                var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
                // End of Rev 1.0
            }
            catch
            {
                status = "300";
                return Json(status, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetSubattendaceChart(string ord)
        {
            string status = string.Empty;
            try
            {
                List<Subattchart> list = new List<Subattchart>();

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_FTSATTNDASHBOARD", sqlcon);
                sqlcmd.Parameters.Add("@Action", "GETATTNZOOMING");
                sqlcmd.Parameters.Add("@YYYYMM", ord);
                sqlcmd.Parameters.Add("@CREATE_USERID", Session["userid"].ToString());

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();

                if (dt != null && dt.Rows.Count > 0)
                {

                    foreach (DataRow item in dt.Rows)
                    {
                        Subattchart obj = new Subattchart();
                        obj.Att_Date = Convert.ToString(item["Att_Date"]);
                        obj.month_name = Convert.ToString(item["month_name"]);
                        obj.ord = Convert.ToString(item["ord"]);
                        obj.Employee = Convert.ToString(item["Employee"]);
                        obj.Desg = Convert.ToString(item["Desg"]);
                        obj.Dept = Convert.ToString(item["Dept"]);
                        obj.Supervisor = Convert.ToString(item["Supervisor"]);
                        obj.Contact = Convert.ToString(item["Contact"]);
                        obj.att_Given = Convert.ToString(item["att_Given"]);
                        obj.Work_datetime = Convert.ToString(item["Work_datetime"]);
                        list.Add(obj);
                    }
                }


                return Json(list);
            }
            catch
            {
                status = "300";
                return Json(status, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult GetCalenderClick(string date, string userid)
        {
            string status = string.Empty;
            try
            {
                List<caleClickClass> list = new List<caleClickClass>();

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_FTSATTNDASHBOARD", sqlcon);
                sqlcmd.Parameters.Add("@Action", "GETEMPDATEACTIVITY");
                sqlcmd.Parameters.Add("@USER_ID", userid);
                sqlcmd.Parameters.Add("@DATE", date);
                sqlcmd.Parameters.Add("@CREATE_USERID", Session["userid"].ToString());

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();

                if (dt != null && dt.Rows.Count > 0)
                {

                    foreach (DataRow item in dt.Rows)
                    {
                        caleClickClass obj = new caleClickClass();
                        obj.NAME = Convert.ToString(item["NAME"]);
                        obj.LOGIN_ID = Convert.ToString(item["LOGIN_ID"]);
                        obj.STATE = Convert.ToString(item["STATE"]);
                        obj.SUPERVISOR = Convert.ToString(item["SUPERVISOR"]);
                        obj.STATUS = Convert.ToString(item["STATUS"]);
                        obj.LOGIN_TIME = Convert.ToString(item["LOGIN_TIME"]);
                        obj.LOGOUT_TIME = Convert.ToString(item["LOGOUT_TIME"]);  
                        list.Add(obj);
                    }
                }


                return Json(list);
            }
            catch
            {
                status = "300";
                return Json(status, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetAbsentToday()
        {
            string status = string.Empty;
            try
            {
                List<totalAttendance> list = new List<totalAttendance>();

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_FTSATTNDASHBOARD", sqlcon);
                sqlcmd.Parameters.Add("@Action", "GETABSENTTODAY");
                sqlcmd.Parameters.Add("@CREATE_USERID", Session["userid"].ToString());

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();

                if (dt != null && dt.Rows.Count > 0)
                {
                    
                    foreach (DataRow item in dt.Rows)
                    {
                        totalAttendance obj = new totalAttendance();
                        obj.Employee = Convert.ToString(item["Employee"]);
                        obj.Desg = Convert.ToString(item["Desg"]);
                        obj.Dept = Convert.ToString(item["Dept"]);
                        obj.Supervisor = Convert.ToString(item["Supervisor"]);
                        obj.Contact = Convert.ToString(item["Contact"]);
                        list.Add(obj);
                    }
                }

                var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
               // return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                status = "300";
                return Json(status, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetOntime()
        {
            string status = string.Empty;
            try
            {
                List<onTimeToday> list = new List<onTimeToday>();

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_FTSATTNDASHBOARD", sqlcon);
                sqlcmd.Parameters.Add("@Action", "GETONTIMETODAY");
                sqlcmd.Parameters.Add("@CREATE_USERID", Session["userid"].ToString());

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();

                if (dt != null && dt.Rows.Count > 0)
                {
                    
                    foreach (DataRow item in dt.Rows)
                    {
                        onTimeToday obj = new onTimeToday();
                        obj.Employee = Convert.ToString(item["Employee"]);
                        obj.Desg = Convert.ToString(item["Desg"]);
                        obj.Dept = Convert.ToString(item["Dept"]);
                        obj.Supervisor = Convert.ToString(item["Supervisor"]);
                        obj.Contact = Convert.ToString(item["Contact"]);
                        obj.Att_Time = Convert.ToString(item["Att_Time"]);
                        obj.att_Given = Convert.ToString(item["att_Given"]);
                        list.Add(obj);
                    }
                }


                return Json(list);
            }
            catch
            {
                status = "300";
                return Json(status, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetLateToday()
        {
            string status = string.Empty;
            try
            {
                List<lateToday> list = new List<lateToday>();

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_FTSATTNDASHBOARD", sqlcon);
                sqlcmd.Parameters.Add("@Action", "GETLATETODAY");
                sqlcmd.Parameters.Add("@CREATE_USERID", Session["userid"].ToString());

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();

                if (dt != null && dt.Rows.Count > 0)
                {
                    
                    foreach (DataRow item in dt.Rows)
                    {
                        lateToday obj = new lateToday();
                        obj.Employee = Convert.ToString(item["Employee"]);
                        obj.Desg = Convert.ToString(item["Desg"]);
                        obj.Dept = Convert.ToString(item["Dept"]);
                        obj.Supervisor = Convert.ToString(item["Supervisor"]);
                        obj.Contact = Convert.ToString(item["Contact"]);
                        obj.Att_Time = Convert.ToString(item["Att_Time"]);
                        obj.att_Given = Convert.ToString(item["att_Given"]);
                        obj.Diff = Convert.ToString(item["Diff"]);
                        list.Add(obj);
                    }
                }


                return Json(list);
            }
            catch
            {
                status = "300";
                return Json(status, JsonRequestBehavior.AllowGet);
            }
        }
        public class caleClickClass
        {
            public String NAME { get; set; }
            public String LOGIN_ID	{ get; set; }
            public String STATE	{ get; set; }
            public String SUPERVISOR	{ get; set; }
            public String STATUS	{ get; set; }
            public String LOGIN_TIME	{ get; set; }
            public String LOGOUT_TIME { get; set; }
        }
        public class Subattchart
        {
            public String Att_Date	{ get; set; }
            public String month_name { get; set; }
            public String ord { get; set; }	
            public String Employee{ get; set; }	
            public String Desg	{ get; set; }
            public String Dept	{ get; set; }
            public String Supervisor	{ get; set; }
            public String Contact	{ get; set; }
            public String att_Given	{ get; set; }
            public String Work_datetime { get; set; } 
        }
        public class totalAttendance
        {
            public String Employee { get; set; }
            public String Desg { get; set; }
            public String Dept { get; set; }
            public String Supervisor { get; set; }
            public String Contact { get; set; }	
        }
        public class onTimeToday
        {
            public String Employee { get; set; }
            public String Desg { get; set; }
            public String Dept { get; set; }
            public String Supervisor { get; set; }
            public String Contact { get; set; }
	        public String Att_Time { get; set; }
            public String att_Given { get; set; }
        }

        public class lateToday
        {
            public String Employee { get; set; }
            public String Desg { get; set; }
            public String Dept { get; set; }
            public String Supervisor { get; set; }
            public String Contact { get; set; }
            public String Att_Time { get; set; }
            public String att_Given { get; set; }
            public String Diff { get; set; }
        }
	}
}
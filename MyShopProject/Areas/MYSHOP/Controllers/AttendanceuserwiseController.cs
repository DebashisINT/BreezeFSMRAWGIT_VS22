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

namespace MyShop.Areas.MYSHOP.Controllers
{

    public class AttendanceuserwiseController : Controller
    {


        UserList lstuser = new UserList();
        UserwiseAttendance objshop = new UserwiseAttendance();
        DataTable dtuser = new DataTable();
        DataTable dtdesig = new DataTable();

        public ActionResult Attendance()
        {
            try
            {
               
                UserwiseattendanceInput omodel = new UserwiseattendanceInput();
                string userid = Session["userid"].ToString();
                dtuser = lstuser.GetUserList(userid);
                dtdesig = lstuser.GetDesignationList();
                // dtuser = lstuser.GetUserList();
                List<GetUsersattendance> model = new List<GetUsersattendance>();

                List<GetUserDesignation> modeldesg = new List<GetUserDesignation>();

                model = APIHelperMethods.ToModelList<GetUsersattendance>(dtuser);
                modeldesg = APIHelperMethods.ToModelList<GetUserDesignation>(dtdesig);

                omodel.userlsit = model;
                omodel.designation = modeldesg;

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

        [HttpPost]
        public JsonResult GetUsers(string typeid)
        {
            UserListModel omodel = new UserListModel();
            string userid1 = Session["userid"].ToString();
            dtuser = lstuser.GetUserList(userid1, typeid);
            //  dtuser = lstuser.GetUserList();
            List<GetUsers> model = new List<GetUsers>();

            model = APIHelperMethods.ToModelList<GetUsers>(dtuser);

            if (model != null && model.Count() > 0)
            {
                return Json(model);
            }

            return Json(model);


        }

        public ActionResult GetAttendanceList(UserwiseattendanceInput model)
        {
            try
            {
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
                List<AttendancerecorduserwiseModel> omel = new List<AttendancerecorduserwiseModel>();

                DataTable dt = new DataTable();

                if (model.Fromdate == null)
                {
                    model.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                }

                if (model.Todate == null)
                {
                    model.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                }
                if (model.type == "2")
                {
                    model.Todate = null;
                    model.selectedusrid = null;
                }
                
                string datfrmat = model.Fromdate.Split('-')[2] + '-' + model.Fromdate.Split('-')[1] + '-' + model.Fromdate.Split('-')[0];
                string dattoat = "";

                if (model.type == "1")
                {
                    dattoat = model.Todate.Split('-')[2] + '-' + model.Todate.Split('-')[1] + '-' + model.Todate.Split('-')[0];
                }

                if(model.type=="2")
                {
                    string userid1 = Session["userid"].ToString();
                    model.selectedusrid = userid1;
                }

                if (model.DesgId != null)
                {
                    dt = objshop.GetAttendanceuserlist(model.DesgId, datfrmat, model.usertype, Int32.Parse(model.selectedusrid));
                }
                else
                {
                    dt = objshop.GetAttendanceuserlist(model.selectedusrid, datfrmat, dattoat,model.usertype);
                }
                if (dt.Rows.Count > 0)
                {
                    omel = APIHelperMethods.ToModelList<AttendancerecorduserwiseModel>(dt);
                    TempData["Exportattendanceuserwise"] = omel;
                    TempData.Keep();
                }
                else
                {
                    return PartialView("_PartialAttendanceReport", omel);

                }
                return PartialView("_PartialAttendanceReport", omel);
            }
            catch
            {

                //return Redirect("/OMS/Signoff.aspx", new {are});
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }

        public ActionResult ExportAttendance(int type)
        {
            List<AttendancerecordModel> model = new List<AttendancerecordModel>();
            if (TempData["Exportattendanceuserwise"] != null)
            {

                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(), TempData["Exportattendanceuserwise"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(), TempData["Exportattendanceuserwise"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(), TempData["Exportattendanceuserwise"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(), TempData["Exportattendanceuserwise"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(), TempData["Exportattendanceuserwise"]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetEmployeeBatchGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "Attendance Userwise";
            // settings.CallbackRouteValues = new { Controller = "Employee", Action = "ExportEmployee" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Attendance UserwiseReport";

            settings.Columns.Add(column =>
            {
                column.Caption = "User";
                column.FieldName = "userName";

            });

            settings.Columns.Add(column =>
                  {
                      column.Caption = "Designation";
                      column.FieldName = "designation";

                  });


            settings.Columns.Add(column =>
            {
                column.Caption = "Login Date";
                column.FieldName = "login_date";
                column.PropertiesEdit.DisplayFormatString = "dd/MM/yyyy";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Login Time";
                column.FieldName = "Mintime";


            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Logout Time";
                column.FieldName = "Maxtime";


            });
            settings.Columns.Add(column =>
            {
                column.Caption = "Duration";
                column.FieldName = "duration";

            });


            //settings.Columns.Add(column =>
            //{
            //    column.Caption = "Status";
            //    column.FieldName = "Attendstatus";

            //});


            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }
    }
}
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
    public class ActivityController : Controller
    {
        UserList lstuser = new UserList();
        Activity objActivity = new Activity();
        DataTable dtuser = new DataTable();
        public ActionResult ActivityList()
        {
            try
            {
               
                ActivityInput omodel = new ActivityInput();
                string userid = Session["userid"].ToString();
                dtuser = lstuser.GetUserList(userid);
                List<GetUsersActivity> model = new List<GetUsersActivity>();
                model = APIHelperMethods.ToModelList<GetUsersActivity>(dtuser);
                omodel.userlsit = model;
                if (TempData["Activityuser"] != null)
                {
                    omodel.selectedusrid = TempData["Activityuser"].ToString();
                    TempData.Clear();
                }
                return View(omodel);
            }

            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }


        public ActionResult GetActivityIndex(string User)
        {
            TempData["Activityuser"] = User;
            return RedirectToAction("ActivityList");
        }


        public ActionResult GetActivityList(ActivityInput modelfetch)
        {

            ActivitylistsOutput omodel=new ActivitylistsOutput();
            try
            {
                List<Activitylists> omel = new List<Activitylists>();
                Activitylistscount account = new Activitylistscount();
                DataTable dt = new DataTable();

                DataSet ds = new DataSet();

                if (modelfetch.Fromdate == null)
                {

                    modelfetch.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                }

                if (modelfetch.Todate == null)
                {

                    modelfetch.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                }

                string datfrmat = modelfetch.Fromdate.Split('-')[2] + '-' + modelfetch.Fromdate.Split('-')[1] + '-' + modelfetch.Fromdate.Split('-')[0];
                string dattoat = modelfetch.Todate.Split('-')[2] + '-' + modelfetch.Todate.Split('-')[1] + '-' + modelfetch.Todate.Split('-')[0];
                ds = objActivity.GetDaywiseActivityList(modelfetch.selectedusrid, datfrmat, dattoat, "");
                if (ds.Tables[2].Rows.Count > 0)
                {
                    omel = APIHelperMethods.ToModelList<Activitylists>(ds.Tables[2]);
                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        account = APIHelperMethods.ToModel<Activitylistscount>(ds.Tables[3]);
                    }

                    omodel.lstactivs=omel;
                    omodel.countget=account;
                    TempData["ExportActivity"] = omel;
                    TempData.Keep();
                }
                else
                {
                  //  ViewBag.Message = "There are no data to display";
                    return PartialView("_PartialActivitylist", omodel);
                }

                return PartialView("_PartialActivitylist", omodel);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }




        public ActionResult ExportActivity(int type)
        {
            List<AttendancerecordModel> model = new List<AttendancerecordModel>();
            if (TempData["ExportActivity"] != null)
            {

                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(), TempData["ExportActivity"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(), TempData["ExportActivity"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(), TempData["ExportActivity"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(), TempData["ExportActivity"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(), TempData["ExportActivity"]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetEmployeeBatchGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "Activity";
            // settings.CallbackRouteValues = new { Controller = "Employee", Action = "ExportEmployee" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Activity Report";

            settings.Columns.Add(column =>
            {
                column.Caption = "Shop Name";
                column.FieldName = "shop_name";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Owner Name";
                column.FieldName = "Owner_name";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Owner Contact";
                column.FieldName = "Owner_contact";
            });


            settings.Columns.Add(column =>
            {
                column.Caption = "Shop Address";
                column.FieldName = "shop_address";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "visited Time";
                column.FieldName = "visited_time";
                column.PropertiesEdit.DisplayFormatString = "dd/MM/yyyy HH:mm:ss";

            });


            settings.Columns.Add(column =>
            {
                column.Caption = "Duration Spent";
                column.FieldName = "duration_spent";

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
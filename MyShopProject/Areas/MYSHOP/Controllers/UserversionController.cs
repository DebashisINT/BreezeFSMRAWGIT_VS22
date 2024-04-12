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
    public class UserversionController : Controller
    {
        Userlistversion lstuser = new Userlistversion();

        DataTable dtdesig = new DataTable();
        public ActionResult Version()
        {

            try
            {

                List<UserversioningUsages> omel = new List<UserversioningUsages>();

                DataTable dt = new DataTable();
                //if (TempData["Versioneuser"] != null)
                //{

                dt = lstuser.Getversionuserlist(null, Convert.ToInt32(Session["userid"]));
                TempData.Clear();
                if (dt.Rows.Count > 0)
                {
                    omel = APIHelperMethods.ToModelList<UserversioningUsages>(dt);
                    TempData["ExporVersionusers"] = omel;
                    TempData.Keep();
                }
                else
                {
                    return View(omel);

                }
                //}
                return View(omel);
            }
            catch
            {

                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetVersonuser(string User = null)
        {
            if (User == null)
            {

                User = Convert.ToString(Session["userid"]);
            }
            TempData["Versioneuser"] = User;
            TempData.Keep();
            return RedirectToAction("Version");
        }

        public ActionResult ExportUsers(int type)
        {
            List<AttendancerecordModel> model = new List<AttendancerecordModel>();
            if (TempData["ExporVersionusers"] != null)
            {

                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetUserGridViewSettings(), TempData["ExporVersionusers"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetUserGridViewSettings(), TempData["ExporVersionusers"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetUserGridViewSettings(), TempData["ExporVersionusers"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetUserGridViewSettings(), TempData["ExporVersionusers"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetUserGridViewSettings(), TempData["ExporVersionusers"]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetUserGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "User List";
            // settings.CallbackRouteValues = new { Controller = "Employee", Action = "ExportEmployee" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "User List";

            settings.Columns.Add(column =>
            {
                column.Caption = "User";
                column.FieldName = "UserName";
                column.ExportWidth = 250;
            });


            settings.Columns.Add(column =>
            {
                column.Caption = "Name";
                column.FieldName = "EmployeeName";
                column.ExportWidth = 250;

            });

            settings.Columns.Add(column =>
                {
                    column.Caption = "User ID";
                    column.FieldName = "UserLoginID";
                    column.ExportWidth = 150;
                });



            settings.Columns.Add(column =>
            {
                column.Caption = "Designation";
                column.FieldName = "Designation";
                column.ExportWidth = 100;
            });



            settings.Columns.Add(column =>
            {
                column.Caption = "State";
                column.FieldName = "StateName";
                column.ExportWidth = 110;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Last Activity";
                column.FieldName = "Lastactivity";
                column.ExportWidth = 130;

            });


            settings.Columns.Add(column =>
            {
                column.Caption = "App Version";
                column.FieldName = "VersionNumber";
                column.ExportWidth = 130;
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
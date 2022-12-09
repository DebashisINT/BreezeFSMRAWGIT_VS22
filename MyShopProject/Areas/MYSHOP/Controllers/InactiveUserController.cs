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
    public class InactiveUserController : Controller
    {
        InactiveUserslist lstuser = new InactiveUserslist();
      
        DataTable dtdesig = new DataTable();
        public ActionResult Users(string User = null)
        {


            try
            {

                List<InactiveUsersoutput> omel = new List<InactiveUsersoutput>();

                DataTable dt = new DataTable();
                if(User==null)
                {

                    User = Convert.ToString(Session["userid"]);
                }
                dt = lstuser.GetInactiveuserlist(User);

                if (dt.Rows.Count > 0)
                {
                    omel = APIHelperMethods.ToModelList<InactiveUsersoutput>(dt);
                    TempData["ExportInactiveusers"] = omel;
                    TempData.Keep();
                }
                else
                {
                    return View(omel);

                }
                return View( omel);
            }
            catch
            {

                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
           
        }

        public ActionResult GetInactiveuser(string User=null)
        {
            TempData["Inactiveuser"] = User;
            return RedirectToAction("GetUserInactiveList", User);
        }

     

        public ActionResult GetUserInactiveList(string  userid)
        {
            try
            {
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
                List<InactiveUsersoutput> omel = new List<InactiveUsersoutput>();

                DataTable dt = new DataTable();

                dt = lstuser.GetInactiveuserlist(userid);
                
                if (dt.Rows.Count > 0)
                {
                    omel = APIHelperMethods.ToModelList<InactiveUsersoutput>(dt);
                    TempData["ExportInactiveusers"] = omel;
                    TempData.Keep();
                }
                else
                {
                    return PartialView("_PartialInactiveUsers", omel);

                }
                return PartialView("_PartialInactiveUsers", omel);
            }
            catch
            {

                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }


        public ActionResult ExportUsers(int type)
        {
            List<AttendancerecordModel> model = new List<AttendancerecordModel>();
            if (TempData["ExportInactiveusers"] != null)
            {

                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetUserGridViewSettings(), TempData["ExportInactiveusers"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetUserGridViewSettings(), TempData["ExportInactiveusers"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetUserGridViewSettings(), TempData["ExportInactiveusers"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetUserGridViewSettings(), TempData["ExportInactiveusers"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetUserGridViewSettings(), TempData["ExportInactiveusers"]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetUserGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "Inactive User";
            // settings.CallbackRouteValues = new { Controller = "Employee", Action = "ExportEmployee" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Inactive User";

            settings.Columns.Add(column =>
            {
                column.Caption = "User";
                column.FieldName = "UserName";

            });


            settings.Columns.Add(column =>
            {
                column.Caption = "Name";
                column.FieldName = "EmployeeName";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "User ID";
                column.FieldName = "UserLoginID";

            });


            settings.Columns.Add(column =>
            {
                column.Caption = "Designation";
                column.FieldName = "Designation";

            });


            settings.Columns.Add(column =>
            {
                column.Caption = "State";
                column.FieldName = "StateName";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Last Activity";
                column.FieldName = "Lastactivity";

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
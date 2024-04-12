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
    public class GPSStatusController : Controller
    {
        // GET: /MYSHOP/GPSStatus/ 
        UserList lstuser = new UserList();
        GpsListBL objgps = new GpsListBL();
        DataTable dtuser = new DataTable();
        DataTable dtstate = new DataTable();
        DataTable dtshop = new DataTable();

        public ActionResult List()
        {
            try
            {

                GpsStatusClassInput omodel = new GpsStatusClassInput();
                string userid = Session["userid"].ToString();
                dtuser = lstuser.GetUserList(userid);
                // dtuser = lstuser.GetUserList();
                List<GetUserName> model = new List<GetUserName>();

                model = APIHelperMethods.ToModelList<GetUserName>(dtuser);
                omodel.userlsit = model;
                omodel.selectedusrid = userid;
                omodel.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                omodel.Todate = DateTime.Now.ToString("dd-MM-yyyy");


                if (TempData["Gpsuser"] != null)
                {
                    omodel.selectedusrid = TempData["Gpsuser"].ToString();
                    TempData.Clear();
                }


                return View(omodel);

            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }

        }


        public ActionResult GetGpsStatusList(GpsStatusClassInput model)
        {
            //try
            //{
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

            ViewData["ModelData"] = model;

            string datfrmat = model.Fromdate.Split('-')[2] + '-' + model.Fromdate.Split('-')[1] + '-' + model.Fromdate.Split('-')[0];
            string dattoat = model.Todate.Split('-')[2] + '-' + model.Todate.Split('-')[1] + '-' + model.Todate.Split('-')[0];

            double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;
            if (days <= 30)
            {
                dt = objgps.GetGpsStatusShop(datfrmat, dattoat, model.selectedusrid, "Summary");
            }


            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    omel.Add(new GpsStatusClasstOutput()
                    {

                        name = Convert.ToString(dt.Rows[i]["name"]),
                        total_shop_visited = Convert.ToString(dt.Rows[i]["total_shop_visited"]),
                        active_hrs = Convert.ToString(dt.Rows[i]["active_hrs"]),
                        user_id = Convert.ToString(dt.Rows[i]["user_id"]),
                        inactive_hrs = Convert.ToString(dt.Rows[i]["inactive_hrs"]),
                        idle_percentage = Convert.ToString(dt.Rows[i]["idle_percentage"])

                    });
                }

              //  omel = APIHelperMethods.ToModelList<GpsStatusClasstOutput>(dt);
                TempData["ExporGPS"] = omel;
                TempData.Keep();

            }
            else
            {
                return PartialView("_PartialGPSStatusList", omel);

            }
            return PartialView("_PartialGPSStatusList", omel);
            //}
            //catch
            //{
            //    return RedirectToAction("Logout", "Login", new { Area = "" });

            //}
        }


        public ActionResult ShopListActivity(GpsStatusClassInput model)
        {
            DataTable dt = new DataTable();

            List<GpsStatusActivityshopClasstOutput> omel = new List<GpsStatusActivityshopClasstOutput>();



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

            dt = objgps.GetGpsListShop(datfrmat, dattoat, model.selectedusrid);



            if (dt.Rows.Count > 0)
            {
                omel = APIHelperMethods.ToModelList<GpsStatusActivityshopClasstOutput>(dt);
                TempData["ExporGPS"] = omel;
                TempData.Keep();
            }
            else
            {
                return PartialView("_PartialGpsShopActivityList", omel);
            }
            return PartialView("_PartialGpsShopActivityList", omel);
        }

        public ActionResult ShopListActivityList(GpsStatusClassInput model)
        {
            DataTable dt = new DataTable();

            List<GpsStatusActivityshopClasstOutput> omel = new List<GpsStatusActivityshopClasstOutput>();



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

            dt = objgps.GetGpsListShop(datfrmat, dattoat, model.selectedusrid);



            if (dt.Rows.Count > 0)
            {
                omel = APIHelperMethods.ToModelList<GpsStatusActivityshopClasstOutput>(dt);
                TempData["ExporGPS"] = omel;
                TempData.Keep();
            }
            else
            {
                return PartialView("_PartialGpsActivitylist", omel);
            }
            return PartialView("_PartialGpsActivitylist", omel);
        }



        public ActionResult GPSListActivityList(GpsStatusClassInput model)
        {
            DataTable dt = new DataTable();

            List<GpsStatuInactiveClasstOutput> omel = new List<GpsStatuInactiveClasstOutput>();



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

            dt = objgps.GetGpsStatusShop(datfrmat, dattoat, model.selectedusrid, "Detail");




            if (dt.Rows.Count > 0)
            {
             //   omel = APIHelperMethods.ToModelList<GpsStatuInactiveClasstOutput>(dt);


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    omel.Add(new GpsStatuInactiveClasstOutput()
                    {

                        date = Convert.ToDateTime(dt.Rows[i]["date"]),
                        gps_on_time = Convert.ToString(dt.Rows[i]["gps_on_time"]),
                        gps_off_time = Convert.ToString(dt.Rows[i]["gps_off_time"]),
                        duration = Convert.ToString(dt.Rows[i]["duration"])
                      

                    });
                }



                TempData["ExporGPS"] = omel;
                TempData.Keep();
            }
            else
            {
                return PartialView("_PartialGPSInactiveList", omel);
            }


            return PartialView("_PartialGPSInactiveList", omel);
        }




        public ActionResult ExportGpsStatusList(int type)
        {
            List<AttendancerecordModel> model = new List<AttendancerecordModel>();
            if (TempData["ExporGPS"] != null)
            {

                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetGPSGridViewSettings(), TempData["ExporGPS"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetGPSGridViewSettings(), TempData["ExporGPS"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetGPSGridViewSettings(), TempData["ExporGPS"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetGPSGridViewSettings(), TempData["ExporGPS"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetGPSGridViewSettings(), TempData["ExporGPS"]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetGPSGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "Gps Status List";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "GPS Status List";

            settings.Columns.Add(column =>
            {
                column.Caption = "User";
                column.FieldName = "name";
                column.ExportWidth = 180;
            });


            settings.Columns.Add(column =>
            {
                column.Caption = "No. Of Shops Visited";
                column.FieldName = "total_shop_visited";
                column.ExportWidth = 150;
            });


            settings.Columns.Add(column =>
            {
                column.Caption = "Total Working Duration";
                column.FieldName = "active_hrs";
                column.ExportWidth = 150;

            });
            settings.Columns.Add(column =>
            {
                column.Caption = "GPS Inactive Duration";
                column.FieldName = "inactive_hrs";
                column.ExportWidth = 150;

            });
            settings.Columns.Add(column =>
            {
                column.Caption = "Idle %";
                column.FieldName = "idle_percentage";
                column.ExportWidth = 100;

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
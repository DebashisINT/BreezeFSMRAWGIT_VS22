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
using DevExpress.Web.Mvc;
using DevExpress.Web;
using System.Xml.Linq;

namespace MyShop.Areas.MYSHOP.Controllers
{
    // [RoutePrefix("MYSHOP")]
    public class LocationDetailsController : Controller
    {
        UserList lstuser = new UserList();
        Locationdata objlocation = new Locationdata();

        DataTable dtuser = new DataTable();
        public ActionResult LocationList()
        {
            try
            {
                string key = Convert.ToString(Session["ApiKey"]);
                LocationInput omodel = new LocationInput();
                string userid = Session["userid"].ToString();
                dtuser = lstuser.GetUserList(userid);

                List<GetUsersLocation> model = new List<GetUsersLocation>();

                model = APIHelperMethods.ToModelList<GetUsersLocation>(dtuser);
                omodel.userlsit = model;

                if (TempData["Locationuser"] != null)
                {
                    omodel.selectedusrid = TempData["Locationuser"].ToString();
                    TempData.Clear();
                }
             ///   RetrieveFormatedDrivingdistance("22.5701064","88.4328251", "22.5687734", "88.4321156");
                omodel.KeyId = key;
                return View(omodel);
            }

            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetlocationIndex(string User)
        {
            TempData["Locationuser"] = User;
            return RedirectToAction("LocationList");
        }


        public ActionResult GetLocationList(LocationInput modelfetch)
        {

            try
            {
                List<Locationlists> omel = new List<Locationlists>();
                DataTable dt = new DataTable();

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


                dt = objlocation.GetLocationList(modelfetch.selectedusrid, datfrmat, dattoat, "", Convert.ToInt32(Session["userid"]));

                if (dt.Rows.Count > 0)
                {
                    omel = APIHelperMethods.ToModelList<Locationlists>(dt);


                    TempData["Exportlocation"] = omel;
                    TempData.Keep();
                    return PartialView("_PartialLocationList", omel);
                }
                else
                {
                    //    ViewBag.Message = "There are no data to display";

                    // return View("LocationList", omodel);

                    //    return RedirectToAction("LocationList", new { Area = "MyShop" });

                    return PartialView("_PartialLocationList", omel);
                }

                ///  return PartialView("_PartialLocationList", omel);
            }
            catch
            {

                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }


        public ActionResult ExportLocation(int type)
        {
            List<AttendancerecordModel> model = new List<AttendancerecordModel>();
            if (TempData["Exportlocation"] != null)
            {

                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(), TempData["Exportlocation"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(), TempData["Exportlocation"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(), TempData["Exportlocation"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(), TempData["Exportlocation"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(), TempData["Exportlocation"]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetEmployeeBatchGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "Location";
            // settings.CallbackRouteValues = new { Controller = "Employee", Action = "ExportEmployee" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Location Report";



            settings.Columns.Add(column =>
            {
                column.Caption = "EMP Name";
                column.FieldName = "UserName";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "EMP ID";
                column.FieldName = "EMPCODE";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Location";
                column.FieldName = "location_name";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Latitude";
                column.FieldName = "latitude";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Longitude";
                column.FieldName = "longitude";

            });
            settings.Columns.Add(column =>
            {
                column.Caption = "Distance";
                column.FieldName = "distance_covered";

            });


            settings.Columns.Add(column =>
                  {
                      column.Caption = "Date";
                      column.FieldName = "date";
                      column.PropertiesEdit.DisplayFormatString = "dd/MM/yyyy";

                  });
            settings.Columns.Add(column =>
                  {
                      column.Caption = "Time";
                      column.FieldName = "onlytime";


                  });
            //settings.Columns.Add(column =>
            //      {
            //          column.Caption = "Shop Covered";
            //          column.FieldName = "shops_covered";

            //      });



            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

        public ActionResult UpdateLocation(string lat, string lng, string id, string location)
        {
            int gets = 0;
           // string location = RetrieveFormatedAddress(lat,lng);

            gets = objlocation.LocationUpdate(id, location);
            if (gets > 0)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }

        public string RetrieveFormatedAddress(string lat, string lng)
        {
            string address = "";
            string locationName = "";

            string url = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&sensor=false", lat, lng);

            try
            {
                XElement xml = XElement.Load(url);
                if (xml.Element("status").Value == "OK")
                {
                    locationName = string.Format("{0}",
                    xml.Element("result").Element("formatted_address").Value);
                }
            }
            catch
            {
                locationName = "";
            }
            return locationName;

        }

        public string RetrieveFormatedDrivingdistance(string latstart, string lngstart,string latend, string lngend)
        {
            string address = "";
            string locationName = "";

            string url = string.Format("http://maps.googleapis.com/maps/api/distancematrix/json?origins="+latstart+","+lngstart+"&destinations="+latend+","+lngend+"&mode=driving&language=en-EN&sensor=false");

            try
            {

                using (var webClient = new System.Net.WebClient())
                {
                    var json = webClient.DownloadString(url);
                    // Now parse with JSON.Net

                    JavaScriptSerializer ser = new JavaScriptSerializer();

                    List<DataJsonCollection> movieInfos = ser.Deserialize<List<DataJsonCollection>>(json);
                }

             

                //XElement xml = XElement.Load(url);
                //if (xml.Element("status").Value == "OK")
                //{
                //    locationName = string.Format("{0}",
                //    xml.Element("result").Element("formatted_address").Value);
                //}
            }
            catch
            {
                locationName = "";
            }
            return locationName;

        }


        public class DataJsonCollection
        {
            public string destination_addresses { get; set; }
            public string origin_addresses { get; set; }

            public Rows rows { get; set; }
        }
        public  class Rows
        {
            public Elements elements { get; set; }

        }
        public class Elements
        {
            public string distance { get; set; }
            public string duration { get; set; }
        }
    }
}
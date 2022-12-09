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
using System.IO;
using BusinessLogicLayer.SalesmanTrack;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public partial class DashboardMenuController : Controller
    {
        UserList lstuser = new UserList();
        DataTable dtuser = new DataTable();
        DataTable dttrack = new DataTable();
        Locationdata objlocation = new Locationdata();
        


        public ActionResult GetSalesmanLocation(UserListTrackModel modelinput)
        {
            try
            {
                Trackshop model = new Trackshop();

                string datfrmat = DateTime.Now.ToString("yyyy-MM-dd");

                dtuser = lstuser.GetUserLocationList(modelinput.selectedusrid, datfrmat);

                if (dtuser.Rows.Count > 0)
                {
                    model.Lat_visit = Convert.ToString(dtuser.Rows[0]["Lat_visit"]);
                    model.Long_visit = Convert.ToString(dtuser.Rows[0]["Long_visit"]);
                    model.location_name = Convert.ToString(dtuser.Rows[0]["location_name"]);
                    model.loginstatus = Convert.ToString(dtuser.Rows[0]["loginstatus"]);
                    model.SDate = Convert.ToString(dtuser.Rows[0]["SDate"]);
                    model.latlanLogin = Convert.ToString(dtuser.Rows[0]["latlanLogin"]);
                    model.latlanLogout = Convert.ToString(dtuser.Rows[0]["latlanLogout"]);

                    string Pathget = "~\\jsonsresponse\\" + "_" + modelinput.selectedusrid + "_" + datfrmat + ".json";

                    if (System.IO.File.Exists(Server.MapPath(Pathget)))
                    {

                        using (StreamReader r = new StreamReader(Server.MapPath(Pathget)))
                        {
                            string json = r.ReadToEnd().ToString();
                            model.Fullresponse = json;

                        }

                    }
                    Dashboard dashbrd = new Dashboard();
                    //dttrack = lstuser.GetUserLocationTrackList(modelinput.selectedusrid, datfrmat);
                    dttrack = dashbrd.GetUserLocationTrackList(modelinput.selectedusrid, datfrmat);
                    List<TracksalesmanAreaTrack> lstarea = new List<TracksalesmanAreaTrack>();
                    lstarea = APIHelperMethods.ToModelList<TracksalesmanAreaTrack>(dttrack);                  
                    model.salesmanarea = lstarea;
                    return Json(model);
                }

                else
                {
                    return PartialView("PartialTracking", model);

                }




            }
            catch
            {

                //    return Redirect("/OMS/Signoff.aspx");
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }

        }


        public ActionResult InserSalesmanLocationforMonthlyReport(string locationdetail, string selectedusrid, string Date, string response)
        {

            DataTable dtloaction = new DataTable();

            string currentdate = DateTime.Now.ToString("yyyy-MM-dd");

            string datfrmat = DateTime.Now.ToString("yyyy-MM-dd");


            if (currentdate != datfrmat)
            {
                string Pathget = "~\\jsonsresponse\\" + "_" + selectedusrid + "_" + datfrmat + ".json";
                if (System.IO.File.Exists(Pathget))
                {
                    System.IO.File.Delete(Pathget);

                }
                var dir = Server.MapPath("~\\jsonsresponse");
                var file = Path.Combine(dir, "_" + selectedusrid + "_" + datfrmat + ".json");
                Directory.CreateDirectory(dir);

                System.IO.File.WriteAllText(Server.MapPath(Pathget), response);

            }
            dtloaction = CreateTempTable();

            var myList = new List<string>(locationdetail.Split('^'));
            foreach (var points in myList)
            {

                var point = points.Split('|');
                string pointlatlong = point[0].ToString().Replace('(', ' ').Replace(')', ' ');
                string lat = pointlatlong.Split(',')[0].Trim();
                string lng = pointlatlong.Split(',')[1].Trim();
                if (point[1].Contains("km"))
                {
                    dtloaction.Rows.Add(lat, lng, "", "", point[1].Replace("km", "").Trim());

                }
                else
                {
                    decimal poin2 = Convert.ToDecimal(Int32.Parse(point[1].Replace("m", "").Trim()) / 1000.000000);
                    dtloaction.Rows.Add(lat, lng, "", "", poin2);
                }
            }


            objlocation.LocationDistanceInsertion(dtloaction, selectedusrid, datfrmat);
            return Json("success");

        }

        public DataTable CreateTempTable()
        {

            DataTable dt = new DataTable();


            dt.Columns.Add("Latitude", typeof(string));
            dt.Columns.Add("Longitude", typeof(string));
            dt.Columns.Add("VisitDate", typeof(string));
            dt.Columns.Add("UserId", typeof(string));
            dt.Columns.Add("distanceKm", typeof(string));

            return dt;
        }

	}
}
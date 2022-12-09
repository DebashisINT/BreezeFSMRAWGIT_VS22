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


namespace MyShop.Areas.MYSHOP.Controllers
{
    public class TrackSalesmanController : Controller
    {
        //
        // GET: /TrackSalesman/

        UserList lstuser = new UserList();
        DataTable dtuser = new DataTable();
        DataTable dttrack = new DataTable();
        Locationdata objlocation = new Locationdata();
        public ActionResult Index()
        {
            try
            {
                UserListTrackModel omodel = new UserListTrackModel();
                string userid = Session["userid"].ToString();
                dtuser = lstuser.GetUserList(userid);
                //  dtuser = lstuser.GetUserList();
                List<GetUsers> model = new List<GetUsers>();

                model = APIHelperMethods.ToModelList<GetUsers>(dtuser);
                omodel.userlsit = model;
                omodel.Date = DateTime.Now.ToString("dd-MM-yyyy");
                if (TempData["Trackinguser"] != null)
                {
                    omodel.selectedusrid = TempData["Trackinguser"].ToString();

                }
                return View(omodel);

            }
            catch
            {

                //  return Redirect("/OMS/Signoff.aspx");
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }




        public ActionResult GetTrackingIndex(string User)
        {
            TempData["Trackinguser"] = User;
            return RedirectToAction("Index");
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


        public ActionResult GetSalesmanLocation(UserListTrackModel modelinput)
        {
            try
            {
                Trackshop model = new Trackshop();

                string datfrmat = modelinput.Date.Split('-')[2] + '-' + modelinput.Date.Split('-')[1] + '-' + modelinput.Date.Split('-')[0];

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

                    dttrack = lstuser.GetUserLocationTrackList(modelinput.selectedusrid, datfrmat);

                    List<TracksalesmanAreaTrack> lstarea = new List<TracksalesmanAreaTrack>();
                    lstarea = APIHelperMethods.ToModelList<TracksalesmanAreaTrack>(dttrack);


                    //var jsonSerialiser = new JavaScriptSerializer();
                    //var json = jsonSerialiser.Serialize(lstarea);



                    //     TracksalesmanAreaTrack[] myArray = lstarea.ToArray();
                    //model.salesmanarea = lstarea;



                    //List<TracksalesmanArea> lstarea1 = new List<TracksalesmanArea>();
                    //List<TracksalesmanAreaTrack> lstarea1 = new List<TracksalesmanAreaTrack>();
                    //  lstarea1.Add(new TracksalesmanAreaTrack()
                    //  {
                    //      //title = "Alibaug",
                    //      //lat = "18.641400",
                    //      //lng = "72.872200",
                    //      //description = "Alibaug is a coastal town and a municipal council in Raigad District in the Konkan region of Maharashtra, India."
                    //      location="23.6642337, 87.6854062"
                    //  });
                    //  lstarea1.Add(new TracksalesmanAreaTrack()
                    //  {
                    //      //title = "Alibaug",
                    //      //lat = "18.641400",
                    //      //lng = "72.872200",
                    //      //description = "Alibaug is a coastal town and a municipal council in Raigad District in the Konkan region of Maharashtra, India."
                    //      location = "23.6714967, 87.691745"
                    //  });


                    //lstarea1.Add(new TracksalesmanArea()
                    //{
                    //    title = "Mumbai",
                    //    lat = "18.964700",
                    //    lng = "72.825800",
                    //    description = "Mumbai"
                    //});

                    //List<string> l = new List<string>();
                    //l.Add("23.6714967, 87.691745");
                    //l.Add("23.6642337, 87.6854062");
                    //string[] s = l.ToArray();
                    model.salesmanarea = lstarea;

                    //  json = jsonSerialiser.Serialize(lstarea1);
                    // model.salesmanarea = json;
                    //// return PartialView("PartialTracking", model);

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

            string currentdate=DateTime.Now.ToString("yyyy-MM-dd");

            string datfrmat = Date.Split('-')[2] + '-' + Date.Split('-')[1] + '-' + Date.Split('-')[0];


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
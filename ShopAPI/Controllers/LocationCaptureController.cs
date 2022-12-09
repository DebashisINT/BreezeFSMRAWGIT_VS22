using ShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;

namespace ShopAPI.Controllers
{
    public class LocationCaptureController : ApiController
    {

        public delegate void del(ModelLocationupdate model, List<Locationupdate> omedl2);

        string uploadtext = "~/CommonFolder/Log/Location.txt";


        LocationupdateOutput omodel = new LocationupdateOutput();
        ShopRegister oview = new ShopRegister();


        [HttpPost]
        public HttpResponseMessage Sendlocation(ModelLocationupdate model)
        {
            string sloactionlog = "";
            string sdatetime = DateTime.Now.ToString();
            try
            {
                if (!ModelState.IsValid)
                {
                    omodel.status = "213";
                    omodel.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
                }
                else
                {
                    List<Locationupdate> omedl2 = new List<Locationupdate>();
                    foreach (var s2 in model.location_details)
                    {
                        //Close for Unknown location 30-11-2020 Tanmoy
                        //if (s2.location_name.ToLower() == "unknown")
                        //{
                        //    s2.location_name = RetrieveFormatedAddress(s2.latitude, s2.longitude);
                        //}
                        //Close for Unknown location  30-11-2020 Tanmoy

                        //if (string.IsNullOrEmpty(s2.location_name) == "unknown")
                        // {
                        //     s2.location_name = RetrieveFormatedAddress(s2.latitude, s2.longitude);
                        // }
                        omedl2.Add(new Locationupdate()
                        {
                            location_name = s2.location_name,
                            latitude = s2.latitude,
                            longitude = s2.longitude,
                            distance_covered = s2.distance_covered,
                            last_update_time = s2.last_update_time,
                            date = s2.date,

                            shops_covered = s2.shops_covered,
                            //extra input for meeting attend start
                            meeting_attended = s2.meeting_attended,
                            //extra input for meeting attend end
                            home_distance = s2.home_distance,
                            network_status = s2.network_status,
                            battery_percentage = s2.battery_percentage,
                            home_duration = s2.home_duration
                        });
                        //  sloactionlog = sloactionlog + " Latitude:" + s2.latitude + "  Longitude :" + s2.longitude + " Date:" + s2.date + " distance_covered:" + s2.distance_covered;
                    }

                    del ldMainAct = new del(InsertLocation);
                    ldMainAct.BeginInvoke(model, omedl2, null, null);


                    //String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                    //string sessionId = "";
                    //List<Locationupdate> omedl2 = new List<Locationupdate>();
                    //foreach (var s2 in model.location_details)
                    //{
                    //    if (s2.location_name.ToLower() == "unknown")
                    //    {
                    //        s2.location_name = RetrieveFormatedAddress(s2.latitude, s2.longitude);
                    //    }
                    //    omedl2.Add(new Locationupdate()
                    //    {
                    //        location_name = s2.location_name,
                    //        latitude = s2.latitude,
                    //        longitude = s2.longitude,
                    //        distance_covered = s2.distance_covered,
                    //        last_update_time = s2.last_update_time,
                    //        date = s2.date,
                    //        shops_covered = s2.shops_covered
                    //    });
                    //    sloactionlog = sloactionlog + " Latitude:" + s2.latitude + "  Longitude :" + s2.longitude + " Date:" + s2.date + " distance_covered:" + s2.distance_covered;
                    //}

                    //string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);

                    //DataTable dt = new DataTable();
                    //String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    //SqlCommand sqlcmd = new SqlCommand();
                    //SqlConnection sqlcon = new SqlConnection(con);
                    //sqlcon.Open();
                    //sqlcmd = new SqlCommand("Sp_ApiLocationupdate", sqlcon);
                    //sqlcmd.Parameters.Add("@session_token", model.session_token);
                    //sqlcmd.Parameters.Add("@user_id", model.user_id);
                    //sqlcmd.Parameters.Add("@JsonXML", JsonXML);
                    //sqlcmd.CommandType = CommandType.StoredProcedure;
                    //SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    //da.Fill(dt);
                    //sqlcon.Close();
                    //if (dt.Rows.Count > 0)
                    //{
                    //    oview = APIHelperMethods.ToModel<ShopRegister>(dt);
                    //    omodel.status = "200";
                    //    omodel.message = "Location details successfully updated.";

                    //    //using (StreamWriter stream = new FileInfo((Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(uploadtext)))).AppendText())
                    //    //{
                    //    //    stream.WriteLine("   Location capture  Start " + "Start Time:" + sdatetime + "End Time:" + DateTime.Now + "Status:Success(LocationCapture/Sendlocation)[200]" + "User ID:" + model.user_id + sloactionlog+" Location capture  END ");
                    //    //}
                    //}
                    //else
                    //{
                    //    omodel.status = "202";
                    //    omodel.message = "User id or Session token does not matched";
                    //    //using (StreamWriter stream = new FileInfo((Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(uploadtext)))).AppendText())
                    //    //{
                    //    //    stream.WriteLine("    Location capture  Start " + "Start Time:" + sdatetime + "End Time:" + DateTime.Now + "Status:Not matched(LocationCapture/Sendlocation)[202]" + "User ID:" + model.user_id + sloactionlog+ " Location capture END ");
                    //    //}
                    //}

                    oview = APIHelperMethods.ToModel<ShopRegister>(null);
                    omodel.status = "200";
                    omodel.message = "Location details successfully updated.";

                    var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                    return message;
                }
            }

            catch (Exception ex)
            {
                omodel.status = "204";
                omodel.message = ex.Message;

                //using (StreamWriter stream = new FileInfo((Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(uploadtext)))).AppendText())
                //{

                //    stream.WriteLine("   Location capture  Start " + "Start Time:" + sdatetime + "End Time:" + DateTime.Now + "Status:Error(LocationCapture/Sendlocation)[204]" + ex.Message + "User ID:" + model.user_id + sloactionlog + "   Location capture END ");
                //    //txt.Close();
                //}
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }


        public string RetrieveFormatedAddress(string lat, string lng)
        {
            string address = "";
            string locationName = "";

            string url = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&sensor=false&key=AIzaSyCbYMZjnt8T6yivYfIa4_R9oy-L3SIYyrQ", lat, lng);

            try
            {
                XElement xml = XElement.Load(url);
                if (xml.Element("status").Value == "OK")
                {
                    locationName = string.Format("{0}",
                    xml.Element("result").Element("formatted_address").Value);
                }
            }
            catch (Exception e)
            {
                locationName = "unknown";
            }
            return locationName == "" ? locationName : "unknown";

        }

        public void InsertLocation(ModelLocationupdate model, List<Locationupdate> omedl2)
        {
            String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
            string sessionId = "";

            string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);

            DataTable dt = new DataTable();
            String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
            SqlCommand sqlcmd = new SqlCommand();
            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();
            sqlcmd = new SqlCommand("Proc_FTS_Locationupdate", sqlcon);
            sqlcmd.Parameters.Add("@session_token", model.session_token);
            sqlcmd.Parameters.Add("@user_id", model.user_id);
            sqlcmd.Parameters.Add("@JsonXML", JsonXML);

            sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dt);
            sqlcon.Close();
            if (dt.Rows.Count > 0)
            {
                oview = APIHelperMethods.ToModel<ShopRegister>(dt);
                omodel.status = "200";
                omodel.message = "Location details successfully updated.";

                //using (StreamWriter stream = new FileInfo((Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(uploadtext)))).AppendText())
                //{
                //    stream.WriteLine("   Location capture  Start " + "Start Time:" + sdatetime + "End Time:" + DateTime.Now + "Status:Success(LocationCapture/Sendlocation)[200]" + "User ID:" + model.user_id + sloactionlog+" Location capture  END ");
                //}
            }
            else
            {
                omodel.status = "202";
                omodel.message = "User id or Session token does not matched";

                //using (StreamWriter stream = new FileInfo((Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(uploadtext)))).AppendText())
                //{
                //    stream.WriteLine("    Location capture  Start " + "Start Time:" + sdatetime + "End Time:" + DateTime.Now + "Status:Not matched(LocationCapture/Sendlocation)[202]" + "User ID:" + model.user_id + sloactionlog+ " Location capture END ");
                //}
            }
        }
    }
}

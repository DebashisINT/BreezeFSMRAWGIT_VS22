using ShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Xml.Linq;

namespace ShopAPI.Controllers
{
    public class LogoutController : ApiController
    {


        [HttpPost]

        public HttpResponseMessage UserLogout(Model_Logout model)
        {
            Model_LogoutOutput omodel = new Model_LogoutOutput();
            UserClass oview = new UserClass();
            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {

               // String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                string sessionId = "";

                string location_name = "Logout";
                //location_name = location_name + " at " + RetrieveFormatedAddress(model.latitude, model.longitude);
                if (!string.IsNullOrEmpty(model.address))
                {
                    location_name = location_name + " at " + model.address;
                }
                DataTable dt = new DataTable();

                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];

                SqlCommand sqlcmd = new SqlCommand();

                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();



                sqlcmd = new SqlCommand("Sp_ApiShopUserLogout", sqlcon);
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@SessionToken", model.session_token);
                sqlcmd.Parameters.Add("@latitude", model.latitude);
                sqlcmd.Parameters.Add("@longitude", model.longitude);
                sqlcmd.Parameters.Add("@logout_time", model.logout_time);
                sqlcmd.Parameters.Add("@location_name", location_name);
                sqlcmd.Parameters.Add("@Autologout", model.Autologout);
                sqlcmd.Parameters.Add("@distance", model.distance);


                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "User successfully logged out.";

                }
                else
                {

                    omodel.status = "202";
                    omodel.message = "Invalid user credential.";

                }

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
            catch
            {
                locationName = "";
            }
            return locationName;

        }


    }
}

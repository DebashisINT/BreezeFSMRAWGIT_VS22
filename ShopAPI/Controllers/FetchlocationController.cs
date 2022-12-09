using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ShopAPI.Models;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;

namespace ShopAPI.Controllers
{
    public class FetchlocationController : ApiController
    {

        public HttpResponseMessage List(LocationfetchInput model)
        {
            List<Locationfetch> oview = new List<Locationfetch>();
            LocationfetchOutput odata = new LocationfetchOutput();

            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
            }
            else
            {
                //X509Certificate2 certificate = Request.GetClientCertificate();
                //string user = certificate.Issuer;
                //string sub = certificate.Subject;

                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                string sessionId = "";

                List<Locationupdate> omedl2 = new List<Locationupdate>();

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("Sp_API_Locationfetch", sqlcon);
                sqlcmd.Parameters.Add("@date_span", model.date_span);
                sqlcmd.Parameters.Add("@from_date", model.from_date);
                sqlcmd.Parameters.Add("@to_date", model.to_date);
                sqlcmd.Parameters.Add("@user_id", model.user_id);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<Locationfetch>(dt);
                    odata.location_details = oview;
                    odata.status = "200";
                    odata.message = "Shop details  available";
                    odata.visit_distance = dt.Rows[0]["visit_distance"].ToString();
                }
                else
                {
                    odata.status = "205";
                    odata.message = "No data found";
                    odata.visit_distance = "";
                }
                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }
        }


        public HttpResponseMessage NewList(NewLocationfetchInput model)
        {
            List<NewLocationfetch> oview = new List<NewLocationfetch>();
            NewLocationfetchOutput odata = new NewLocationfetchOutput();

            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
            }
            else
            {
               
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                string sessionId = "";

                List<Locationupdate> omedl2 = new List<Locationupdate>();

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("Sp_API_Locationfetch", sqlcon);
                sqlcmd.Parameters.Add("@date_span", "0");
                sqlcmd.Parameters.Add("@from_date", model.date);
                sqlcmd.Parameters.Add("@to_date", model.date);
                sqlcmd.Parameters.Add("@user_id", model.user_id);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<NewLocationfetch>(dt);
                    odata.location_details = oview;
                    odata.status = "200";
                    odata.message = "Successfully get location";
                    odata.visit_distance = dt.Rows[0]["visit_distance"].ToString();
                }
                else
                {
                    odata.status = "205";
                    odata.message = "No data found";
                    odata.visit_distance = "";
                }
                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }
        }
    }
}

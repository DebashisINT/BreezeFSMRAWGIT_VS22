using ShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;


namespace ShopAPI.Controllers
{
    public class GPSStatusController : ApiController
    {

        [HttpPost]
        public HttpResponseMessage Update(GpslocationInput model)
        {
            GpslocationOutput omodel = new GpslocationOutput();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];

                string sessionId = "";



               

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("Proc_FTS_GPSLocation", sqlcon);
                sqlcmd.Parameters.Add("@session_token", model.session_token);
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@gps_id", model.gps_id);
                sqlcmd.Parameters.Add("@date", model.date);
                sqlcmd.Parameters.Add("@gps_off_time", model.gps_off_time);
                sqlcmd.Parameters.Add("@gps_on_time", model.gps_on_time);
                sqlcmd.Parameters.Add("@duration", model.duration);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                   
                    omodel.status = "200";
                    omodel.message = "GPS Status successfully updated.";


                }
                else
                {

                    omodel.status = "202";
                    omodel.message = "Duplicate";

                }

                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }

        }



    }
}

#region======================================Revision History=========================================================
//1.0   V2.0.38     Debashis    20/02/2023      A new method "GPSLocationUpdateByLists" has been added.Row: 813
#endregion===================================End of Revision History==================================================

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
using System.Web.ModelBinding;
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
                //Rev 1.0 Row: 813
                //String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                String token = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];
                //End of Rev 1.0 Row: 813
                string sessionId = "";               

                DataTable dt = new DataTable();
                //Rev 1.0 Row: 813
                //String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                //End of Rev 1.0 Row: 813
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("Proc_FTS_GPSLocation", sqlcon);
                //Rev 1.0 Row: 813
                //sqlcmd.Parameters.Add("@session_token", model.session_token);
                //sqlcmd.Parameters.Add("@user_id", model.user_id);
                //sqlcmd.Parameters.Add("@gps_id", model.gps_id);
                //sqlcmd.Parameters.Add("@date", model.date);
                //sqlcmd.Parameters.Add("@gps_off_time", model.gps_off_time);
                //sqlcmd.Parameters.Add("@gps_on_time", model.gps_on_time);
                //sqlcmd.Parameters.Add("@duration", model.duration);
                sqlcmd.Parameters.AddWithValue("@session_token", model.session_token);
                sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);
                sqlcmd.Parameters.AddWithValue("@gps_id", model.gps_id);
                sqlcmd.Parameters.AddWithValue("@date", model.date);
                sqlcmd.Parameters.AddWithValue("@gps_off_time", model.gps_off_time);
                sqlcmd.Parameters.AddWithValue("@gps_on_time", model.gps_on_time);
                sqlcmd.Parameters.AddWithValue("@duration", model.duration);
                //End of Rev 1.0 Row: 813
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

        //Rev 1.0 Row: 813
        [HttpPost]
        public HttpResponseMessage GPSLocationUpdateByLists(GPSLocationListsInput model)
        {
            GPSLocationListsOutput omodel = new GPSLocationListsOutput();
            List<GPSLocationListsModel> omodel1 = new List<GPSLocationListsModel>();

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
                    String token = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];

                    foreach (var s2 in model.gps_status_list)
                    {
                        omodel1.Add(new GPSLocationListsModel()
                        {
                            session_token=s2.session_token,
                            gps_id=s2.gps_id,
                            user_id=s2.user_id,
                            date=s2.date,
                            gps_off_time=s2.gps_off_time,
                            gps_on_time=s2.gps_on_time,
                            duration=s2.duration
                        });
                    }

                    string JsonXML = XmlConversion.ConvertToXml(omodel1, 0);
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcmd.CommandTimeout = 60;
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APIGPSLOCATIONTRACK", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "ADDGPSLOCATION");
                    sqlcmd.Parameters.AddWithValue("@JsonXML", JsonXML);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0 && Convert.ToString(dt.Rows[0]["STRMESSAGE"]) == "Success")
                    {
                        omodel.status = "200";
                        omodel.message = "Success.";                    }
                    else
                    {
                        omodel.status = "205";
                        omodel.message = "Records not updated.";
                    }
                    var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                    return message;
                }
            }
            catch (Exception ex)
            {
                omodel.status = "204";
                omodel.message = ex.Message;
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }
        //End of Rev 1.0 Row: 813

    }
}

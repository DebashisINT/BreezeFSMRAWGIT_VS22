using ShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace ShopAPI.Controllers
{
    public class DevicetokenController : ApiController
    {

        [HttpPost]
        public HttpResponseMessage Update(Modeldevicetoken model)
        {

            ConfigurationModel odata = new ConfigurationModel();

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


                sqlcmd = new SqlCommand("proc_UpdateDevicetoken", sqlcon);
                sqlcmd.Parameters.Add("@user_id",model.user_id);
                sqlcmd.Parameters.Add("@session_token", model.session_token);
                sqlcmd.Parameters.Add("@device_token", model.device_token);
                sqlcmd.Parameters.Add("@device_type", model.device_type);


                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                   
                    odata.status = "200";
                    odata.message = "Device token has been updated.";

                }
                else
                {
                    odata.status = "205";
                    odata.message = "Some input parameters are missing.";

                }

                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }
        }

        //Rev Debashis
        [HttpPost]
        public HttpResponseMessage UserDeviceTokenInfo(DevicetokenInfoInput model)
        {
            DevicetokenInfoOutput odata = new DevicetokenInfoOutput();

            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
            }
            else
            {
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();

                sqlcmd = new SqlCommand("PRC_APIDEVICEINFORMATION", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "DEVICEINFO");
                sqlcmd.Parameters.Add("@USER_ID", model.user_id);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    odata.status = "200";
                    odata.message = "Successfully get Device Information.";
                    odata.user_id_for_token = dt.Rows[0]["user_id_for_token"].ToString();
                    odata.user_name_for_token = dt.Rows[0]["user_name_for_token"].ToString();
                    odata.device_token = dt.Rows[0]["device_token"].ToString();
                    odata.device_type = dt.Rows[0]["device_type"].ToString();
                }
                else
                {
                    odata.status = "205";
                    odata.message = "No data found.";
                }

                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }
        }
        //End of Rev Debashis
    }
}

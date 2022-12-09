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
    public class AddressController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage FetchAddress(AddressInput model)
        {
            AddressOutput omodel = new AddressOutput();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                string sessionId = "";
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_ADDRESS", sqlcon);
                sqlcmd.Parameters.Add("@Action", "FETCHADDRESS");
                sqlcmd.Parameters.Add("@USER_ID", model.user_id);
                sqlcmd.Parameters.Add("@PINCODE", model.pin_code);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();




                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Successfully get state, city.";
                    omodel.city_id = Convert.ToString(dt.Rows[0]["city_id"]);
                    omodel.city = Convert.ToString(dt.Rows[0]["city_name"]);
                    omodel.state_id = Convert.ToString(dt.Rows[0]["state_id"]);
                    omodel.state = Convert.ToString(dt.Rows[0]["state"]);
                    omodel.country = Convert.ToString(dt.Rows[0]["country"]);

                }
                else
                {
                    omodel.status = "205";
                    omodel.message = "No data found.";
                }
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }
    }
}

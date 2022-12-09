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
    public class EarlyExitController : ApiController
    {

        [HttpPost]
        //[RequireHttps]
        public HttpResponseMessage Submit(EarlyExitInput model)
        {
            EarlyExitOutput omodel = new EarlyExitOutput();

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
                sqlcmd = new SqlCommand("prc_EarlyLogout", sqlcon);
                sqlcmd.Parameters.Add("@session_token", model.session_token);
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@reason", model.reason);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Successfully submit early logout reason";

                }
                else
                {

                    omodel.status = "205";
                    omodel.message = "No data found";

                }

                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }

        }
    }
}

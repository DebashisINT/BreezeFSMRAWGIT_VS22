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
    public class NearByTeamController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage List(NearByTeamModalInput model)
        {
            NearByTeamModalOutput omodel = new NearByTeamModalOutput();

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
                sqlcmd = new SqlCommand("PRC_NearByTeam", sqlcon);
                sqlcmd.Parameters.Add("@Action", "GetList");
                sqlcmd.Parameters.Add("@user_id", model.user_id);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    List<NearByTeamModal> oview = new List<NearByTeamModal>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        oview.Add(new NearByTeamModal()
                        {
                            id = Convert.ToString(dt.Rows[i]["id"]),
                            name = Convert.ToString(dt.Rows[i]["name"]),
                            phone_no = Convert.ToString(dt.Rows[i]["phone_no"]),
                            latitude = Convert.ToString(dt.Rows[i]["latitude"]),
                            longitude = Convert.ToString(dt.Rows[i]["longitude"])
                        });
                    }
                    omodel.user_list = oview;
                    omodel.status = "200";
                    omodel.message = "Successfully get activity list";
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

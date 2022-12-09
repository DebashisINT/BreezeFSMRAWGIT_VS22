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
    public class PartyStatusController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage List(PartyStatusInput model)
        {
            PartyStatusOutput omodel = new PartyStatusOutput();
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
                sqlcmd = new SqlCommand("PRC_PARTYSTATUS", sqlcon);
                sqlcmd.Parameters.Add("@action", "GETLIST");
                sqlcmd.Parameters.Add("@session_token", model.session_token);
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();

                List<PartyStatus> oview = new List<PartyStatus>();

                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<PartyStatus>(dt);
                    omodel.status = "200";
                    omodel.message = "Successfully get party status.";
                    omodel.party_status = oview;

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

        [HttpPost]
        public HttpResponseMessage Update(PartyStatusUpdateInput model)
        {
            PartyStatusUpdateOutput omodel = new PartyStatusUpdateOutput();
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
                sqlcmd = new SqlCommand("PRC_PARTYSTATUS", sqlcon);
                sqlcmd.Parameters.Add("@action", "UPDATE");
                sqlcmd.Parameters.Add("@session_token", model.session_token);
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@party_status_id", model.party_status_id);
                sqlcmd.Parameters.Add("@reason", model.reason);
                sqlcmd.Parameters.Add("@shop_id", model.shop_id);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();

                List<PartyStatus> oview = new List<PartyStatus>();

                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<PartyStatus>(dt);
                    omodel.status = "200";
                    omodel.message = "Successfully updated party status.";
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
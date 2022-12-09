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
    public class EntityController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage List(EntityInput model)
        {
            EntityOutput omodel = new EntityOutput();
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
                sqlcmd = new SqlCommand("PRC_ENTITYLIST", sqlcon);
                sqlcmd.Parameters.Add("@action", "GETLIST");
                sqlcmd.Parameters.Add("@session_token", model.session_token);
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();

                List<Entity> oview = new List<Entity>();

                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<Entity>(dt);                    
                    omodel.status = "200";
                    omodel.message = "Successfully get entity type.";
                    omodel.entity_type = oview;

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
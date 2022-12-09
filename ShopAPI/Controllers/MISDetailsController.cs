using ShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
//using System.Security.Cryptography.X509Certificates;
using System.Web.Http;


namespace ShopAPI.Controllers
{
    public class MISDetailsController : ApiController
    {

        [HttpPost]
        public HttpResponseMessage List(MISInput model)
        {

            MISlistOutput outputmodel = new MISlistOutput();
            ShopMisClasscounting omodelshopcount = new ShopMisClasscounting();
            List<MISShopslists> oview = new List<MISShopslists>();
            Datalists odata = new Datalists();

            if (!ModelState.IsValid)
            {
                outputmodel.status = "213";
                outputmodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, outputmodel);
            }
            else
            {

                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
                string sessionId = "";

                List<Locationupdate> omedl2 = new List<Locationupdate>();

                DataSet dt = new DataSet();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("SP_API_GetMISlists", sqlcon);
                sqlcmd.Parameters.Add("@session_token", model.session_token);
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@month", model.month);
                sqlcmd.Parameters.Add("@Weburl", weburl);
                sqlcmd.Parameters.Add("@FromDate", model.start_date);
                sqlcmd.Parameters.Add("@Todate", model.end_date);
                sqlcmd.Parameters.Add("@Year", model.year);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Tables[0].Rows.Count > 0)
                {
                    omodelshopcount = APIHelperMethods.ToModel<ShopMisClasscounting>(dt.Tables[1]);
                    oview = APIHelperMethods.ToModelList<MISShopslists>(dt.Tables[0]);
                 

                  odata.session_token = model.session_token;
                  outputmodel.shop_list=oview;
                  outputmodel.shop_list_count = omodelshopcount;
                  outputmodel.status = "200";
                  outputmodel.message = dt.Tables[0].Rows.Count.ToString() + " No. of Shop list available";

              

                }
                else
                {

                    outputmodel.status = "205";
                    outputmodel.message = "No data found";

                }

                var message = Request.CreateResponse(HttpStatusCode.OK, outputmodel);
                return message;
            }

        }

    }
}

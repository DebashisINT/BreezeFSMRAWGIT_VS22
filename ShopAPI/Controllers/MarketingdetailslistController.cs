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
    public class MarketingdetailslistController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Details(Marketingdetailslistinput model)
        {
            Marketingdetailslistioutput omodel = new Marketingdetailslistioutput();
       
            List<Materialdetailslist> modeldetails = new List<Materialdetailslist>();
            List<Materialimageslist> modelimages = new List<Materialimageslist>();
          
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                String tokenmaterial = System.Configuration.ConfigurationSettings.AppSettings["MaterialImageURL"];

                string sessionId = "";
                DataSet dt = new DataSet();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();

                sqlcmd = new SqlCommand("FTS_getAllMarketingDetailsList", sqlcon);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.Add("@shop_id", model.shop_id);
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@weburl", tokenmaterial);
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();

                if (dt.Tables[0].Rows.Count > 0)
                {
                    modeldetails = APIHelperMethods.ToModelList<Materialdetailslist>(dt.Tables[0]);
                    modelimages = APIHelperMethods.ToModelList<Materialimageslist>(dt.Tables[1]);
                    omodel.material_details = modeldetails;
                    omodel.marketing_img = modelimages;
                    omodel.status = "200";
                    omodel.message = "Marketingdetailslist";
                }
                else
                {
                    omodel.status = "205";
                    omodel.message = "Not found.";
                }

                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;

            }
            catch (Exception ex)
            {
                omodel.status = "209";
                omodel.message = ex.Message;
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }


        }

    }
}

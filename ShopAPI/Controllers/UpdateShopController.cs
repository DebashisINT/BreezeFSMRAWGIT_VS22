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

 
    public class UpdateShopController : ApiController
    {
        public HttpResponseMessage Update(ShopUpdationInput model)
        {
            ShopsubmissionOutput omodel = new ShopsubmissionOutput();
            List<DatalistsSumission> oview = new List<DatalistsSumission>();
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
                sqlcmd = new SqlCommand("Sp_API_FTSUpdateShop", sqlcon);
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@shop_id", model.shop_id);
                sqlcmd.Parameters.Add("@shop_lat", model.shop_lat);
                sqlcmd.Parameters.Add("@shop_long", model.shop_long);
                sqlcmd.Parameters.Add("@shop_address", model.shop_address);
                sqlcmd.Parameters.Add("@isAddressUpdated", model.isAddressUpdated);
                sqlcmd.Parameters.Add("@pincode", model.pincode);



                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();


                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<DatalistsSumission>(dt);
                    omodel.status = "200";
                    omodel.message = "Shop Address Updated Successfully.";
                    omodel.shop_list = oview;
                }
                else
                {
                    omodel.status = "202";
                    omodel.message = "Already shop updated previously";
                }

                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;

            }
        }

    }
}

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
    public class MarketinglistController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage List()
        {
            MarketinglistModel omodel = new MarketinglistModel();
            List<RetailBrndingclass> oretailbrand = new List<RetailBrndingclass>();
            List<Popmaterialsclass> opopmater = new List<Popmaterialsclass>();
            try
            {
                    String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                    string sessionId = "";
                    DataSet dt = new DataSet();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();

                    sqlcmd = new SqlCommand("API_Getmarketinglists", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Tables[0].Rows.Count > 0)
                    {
                        oretailbrand = APIHelperMethods.ToModelList<RetailBrndingclass>(dt.Tables[0]);
                        opopmater = APIHelperMethods.ToModelList<Popmaterialsclass>(dt.Tables[1]);
                        omodel.RetailBranding = oretailbrand;
                        omodel.POPMaterial = opopmater;
                        omodel.status = "200";
                        omodel.message = "Marketinglist";
                    }
                    else
                    {
                        omodel.status = "202";
                        omodel.message = "Invalid user credential.";
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

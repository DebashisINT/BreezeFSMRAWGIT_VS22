using Newtonsoft.Json;
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
    public class BreakageMaterialsInfoController : ApiController
    {
        public HttpResponseMessage ListForBreakageMaterials(ListForBreakageMaterialsInput model)
        {
            ListForBreakageMaterialsOutput omodel = new ListForBreakageMaterialsOutput();
            List<BreakagelistOutput> Boview = new List<BreakagelistOutput>();
            String APIHostingPort = System.Configuration.ConfigurationSettings.AppSettings["APIHostingPort"];
            try
            {
                if (!ModelState.IsValid)
                {
                    omodel.status = "213";
                    omodel.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
                }
                else
                {
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APIBREAKAGEINFODETECTION", sqlcon);
                    sqlcmd.Parameters.Add("@ACTION", "BREAKAGELIST");
                    sqlcmd.Parameters.Add("@user_id", model.user_id);
                    sqlcmd.Parameters.Add("@FROMDATE", model.from_date);
                    sqlcmd.Parameters.Add("@TODATE", model.to_date);
                    sqlcmd.Parameters.Add("@SHOP_ID", model.shop_id);
                    sqlcmd.Parameters.Add("@BaseURL", APIHostingPort);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Successfully Get List.";
                        omodel.user_id = Convert.ToString(dt.Rows[0]["user_id"]);
                        omodel.user_name = Convert.ToString(dt.Rows[0]["user_name"]);
                        omodel.shop_id = Convert.ToString(dt.Rows[0]["shop_id"]);
                        omodel.shop_name = Convert.ToString(dt.Rows[0]["shop_name"]);
                        Boview = APIHelperMethods.ToModelList<BreakagelistOutput>(dt);
                        omodel.breakage_list = Boview;
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
            catch (Exception ex)
            {
                omodel.status = "204";
                omodel.message = ex.Message;
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }
    }
}

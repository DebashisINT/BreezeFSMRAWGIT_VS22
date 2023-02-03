#region======================================Revision History=========================================================
//Written By : Debashis Talukder On 18/01/2023
//Purpose : For New Area/Route/Beat informations.Row 794 to 795
#endregion===================================End of Revision History==================================================
using ShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShopAPI.Controllers
{
    public class AreaRouteBeatRelationInfoController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage AreaRouteBeatList(AreaRouteBeatListInput model)
        {
            AreaRouteBeatListOutput omodel = new AreaRouteBeatListOutput();
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
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APIAREAROUTEBEATRELATIONINFO", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "AREAROUTEBEATLIST");
                    sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@BEAT_ID", model.beat_id);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Success.";
                        omodel.area_id = Convert.ToString(dt.Rows[0]["area_id"]);
                        omodel.area_name = Convert.ToString(dt.Rows[0]["area_name"]);
                        omodel.route_id = Convert.ToString(dt.Rows[0]["route_id"]);
                        omodel.route_name = Convert.ToString(dt.Rows[0]["route_name"]);
                        omodel.beat_id = Convert.ToString(dt.Rows[0]["beat_id"]);
                        omodel.beat_name = Convert.ToString(dt.Rows[0]["beat_name"]);
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

        [HttpPost]
        public HttpResponseMessage AreaRouteBeatDetailsList(AreaRouteBeatDetailsListInput model)
        {
            AreaRouteBeatDetailsListOutput omodel = new AreaRouteBeatDetailsListOutput();
            List<ArealistOutput> Aoview = new List<ArealistOutput>();

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
                    DataSet ds = new DataSet();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APIAREAROUTEBEATRELATIONINFO", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "AREAROUTEBEATDETAILSLIST");
                    sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(ds);
                    sqlcon.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            List<RoutelistOutput> Roview = new List<RoutelistOutput>();
                            for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                            {
                                List<BeatlistOutput> Boview = new List<BeatlistOutput>();
                                for (int k = 0; k < ds.Tables[2].Rows.Count; k++)
                                {
                                    if (Convert.ToString(ds.Tables[2].Rows[k]["area_id"]) == Convert.ToString(ds.Tables[1].Rows[j]["area_id"]) &&
                                        Convert.ToString(ds.Tables[2].Rows[k]["route_id"]) == Convert.ToString(ds.Tables[1].Rows[j]["route_id"]))
                                    {
                                        Boview.Add(new BeatlistOutput()
                                        {
                                            beat_id = Convert.ToString(ds.Tables[2].Rows[k]["beat_id"]),
                                            beat_name = Convert.ToString(ds.Tables[2].Rows[k]["beat_name"])
                                        });
                                    }
                                }

                                if (Convert.ToString(ds.Tables[0].Rows[i]["area_id"]) == Convert.ToString(ds.Tables[1].Rows[j]["area_id"]))
                                {
                                    Roview.Add(new RoutelistOutput()
                                    {
                                        route_id = Convert.ToString(ds.Tables[1].Rows[j]["route_id"]),
                                        route_name = Convert.ToString(ds.Tables[1].Rows[j]["route_name"]),
                                        beat_list = Boview
                                    });
                                }
                            }

                            Aoview.Add(new ArealistOutput()
                            {
                                area_id = Convert.ToString(ds.Tables[0].Rows[i]["area_id"]),
                                area_name = Convert.ToString(ds.Tables[0].Rows[i]["area_name"]),
                                route_list = Roview
                            });
                        }

                        omodel.status = "200";
                        omodel.message = "Successfully Get List.";
                        omodel.area_list = Aoview;
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

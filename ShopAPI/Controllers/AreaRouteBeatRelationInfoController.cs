#region======================================Revision History=========================================================
//Written By : Debashis Talukder On 18/01/2023
//Purpose : For New Area/Route/Beat informations.Row 794 to 795
//1.0   V2.0.39     Debashis    19/05/2023      A new method has been added.Row: 842
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

        //Rev 1.0 Row: 842
        [HttpPost]
        public HttpResponseMessage BeatAreaRouteList(BeatAreaRouteListInput model)
        {
            BeatAreaRouteListOutput omodel = new BeatAreaRouteListOutput();
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
                    sqlcmd.Parameters.AddWithValue("@ACTION", "BEATAREAROUTELIST");
                    sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Success.";
                        omodel.user_id = Convert.ToString(dt.Rows[0]["user_id"]);
                        omodel.PLAN_ASSNBEATID = Convert.ToString(dt.Rows[0]["PLAN_ASSNBEATID"]);
                        omodel.PLAN_ASSNBEATName = Convert.ToString(dt.Rows[0]["PLAN_ASSNBEATName"]);
                        omodel.PLAN_ASSNAREAID = Convert.ToString(dt.Rows[0]["PLAN_ASSNAREAID"]);
                        omodel.PLAN_ASSNAREAName = Convert.ToString(dt.Rows[0]["PLAN_ASSNAREAName"]);
                        omodel.PLAN_ASSNROUTEID = Convert.ToString(dt.Rows[0]["PLAN_ASSNROUTEID"]);
                        omodel.PLAN_ASSNROUTEName = Convert.ToString(dt.Rows[0]["PLAN_ASSNROUTEName"]);
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
        //End of Rev 1.0 Row: 842
    }
}

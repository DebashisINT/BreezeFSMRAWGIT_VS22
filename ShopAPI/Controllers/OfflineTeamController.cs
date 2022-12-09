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
    public class OfflineTeamController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage GetAreaList(AreaInput model)
        {
            List<Area> omodel = new List<Area>();
            AreaOutput odata = new AreaOutput();

            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
            }
            else
            {

                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                string sessionId = "";

                List<Area> omedl2 = new List<Area>();

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("proc_FTS_OfflineTeam", sqlcon);
                sqlcmd.Parameters.Add("@Action", "AreaList");
                sqlcmd.Parameters.Add("@UserID", model.user_id);
                sqlcmd.Parameters.Add("@City_Id", model.city_id);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                sqlcon.Dispose();
                if (dt.Rows.Count > 0)
                {
                    omodel = APIHelperMethods.ToModelList<Area>(dt);
                    odata.status = "200";
                    odata.message = "Success";
                    odata.area_list = omodel;
                }
                else
                {
                    odata.status = "205";
                    odata.message = "No data found";

                }

                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage GetMemberList(OfflineMemberInput model)
        {
            List<OfflineMember> omodel = new List<OfflineMember>();
            OfflineMemberOutput odata = new OfflineMemberOutput();

            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
            }
            else
            {

                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                string sessionId = "";

                List<OfflineMember> omedl2 = new List<OfflineMember>();

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("proc_FTS_OfflineTeam", sqlcon);
                sqlcmd.Parameters.Add("@Action", "MemberList");
                sqlcmd.Parameters.Add("@date", model.date);
                sqlcmd.Parameters.Add("@UserID", model.user_id);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                sqlcon.Dispose();
                if (dt.Rows.Count > 0)
                {
                    omodel = APIHelperMethods.ToModelList<OfflineMember>(dt);
                    odata.status = "200";
                    odata.message = "Success";
                    odata.member_list = omodel;
                }
                else
                {
                    odata.status = "205";
                    odata.message = "No data found";

                }

                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage GetShopList(OfflineShopInput model)
        {
            List<OfflineShop> omodel = new List<OfflineShop>();
            OfflineShopOutput odata = new OfflineShopOutput();

            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
            }
            else
            {

                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                string sessionId = "";

                List<OfflineShop> omedl2 = new List<OfflineShop>();
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
                string DoctorDegree = System.Configuration.ConfigurationSettings.AppSettings["DoctorDegree"];



                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("proc_FTS_OfflineTeam", sqlcon);
                sqlcmd.Parameters.Add("@Action", "ShopList");
                sqlcmd.Parameters.Add("@UserID", model.user_id);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.Add("@Weburl", weburl);
                sqlcmd.Parameters.Add("@DoctorDegree", DoctorDegree);
                sqlcmd.Parameters.Add("@date", model.date);
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                sqlcon.Dispose();
                if (dt.Rows.Count > 0)
                {
                   // omodel = APIHelperMethods.ToModelList<OfflineShop>(dt);

                    omodel = (from DataRow dr in dt.Rows
                              select new OfflineShop()
                              {
                                  area_id = Convert.ToString(dr["area_id"]),
                                  booking_amount = Convert.ToString(dr["booking_amount"]),
                                  dd_name = Convert.ToString(dr["dd_name"]),
                                  entity_code = Convert.ToString(dr["entity_code"]),
                                  funnel_stage_id = Convert.ToString(dr["funnel_stage_id"]),
                                  last_visit_date = Convert.ToString(dr["last_visit_date"]),
                                  lead_id = Convert.ToString(dr["lead_id"]),
                                  model_id = Convert.ToString(dr["model_id"]),
                                  primary_app_id = Convert.ToString(dr["primary_app_id"]),
                                  secondary_app_id = Convert.ToString(dr["secondary_app_id"]),
                                  shop_address = Convert.ToString(dr["shop_address"]),
                                  shop_contact = Convert.ToString(dr["shop_contact"]),
                                  shop_id = Convert.ToString(dr["shop_id"]),
                                  shop_lat = Convert.ToString(dr["shop_lat"]),
                                  shop_long = Convert.ToString(dr["shop_long"]),
                                  shop_name = Convert.ToString(dr["shop_name"]),
                                  shop_pincode = Convert.ToString(dr["shop_pincode"]),
                                  shop_type = Convert.ToString(dr["shop_type"]),
                                  stage_id = Convert.ToString(dr["stage_id"]),
                                  total_visited = Convert.ToString(dr["total_visited"]),
                                  type_id = Convert.ToString(dr["type_id"]),
                                  user_id = Convert.ToString(dr["user_id"]),
                                  assign_to_dd_id = Convert.ToString(dr["assign_to_dd_id"]),
                                  assign_to_pp_id = Convert.ToString(dr["assign_to_pp_id"])

                                  

                              }).ToList();
                    
                    
                    odata.status = "200";
                    odata.message = "Success";
                    odata.shop_list = omodel;
                }
                else
                {
                    odata.status = "205";
                    odata.message = "No data found";

                }

                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }
        }
    }
}

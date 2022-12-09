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
    public class PJPDetailsController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage PJPCustomer(PJPCustomerInput model)
        {
            List<PJPCustomerList> omodel = new List<PJPCustomerList>();
            PJPCustomer odata = new PJPCustomer();

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

                List<Locationupdate> omedl2 = new List<Locationupdate>();

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_API_PJPInsertUpdateDetails", sqlcon);
                sqlcmd.Parameters.Add("@Action", "CUSTOMER_LIST");
                sqlcmd.Parameters.Add("@UserID", model.user_id);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel = APIHelperMethods.ToModelList<PJPCustomerList>(dt);
                    odata.status = "200";
                    odata.message = "Successfully get customer list.";
                    odata.cust_list = omodel;
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
        public HttpResponseMessage PJPAddList(PJPInsertInput model)
        {
            PJPOutPut odata = new PJPOutPut();

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

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_API_PJPInsertUpdateDetails", sqlcon);
                sqlcmd.Parameters.Add("@Action", "PJP_ADD");
                sqlcmd.Parameters.Add("@UserID", model.user_id);
                sqlcmd.Parameters.Add("@PJP_Date", model.date);
                sqlcmd.Parameters.Add("@From_Time", model.from_time);
                sqlcmd.Parameters.Add("@To_Time", model.to_time);
                sqlcmd.Parameters.Add("@SHOP_CODE", model.cust_id);
                sqlcmd.Parameters.Add("@LOCATIONS", model.location);
                sqlcmd.Parameters.Add("@REMARKS", model.remarks);
                sqlcmd.Parameters.Add("@CREATED_USER", model.creater_user_id);
                sqlcmd.Parameters.Add("@pjp_lat", model.pjp_lat);
                sqlcmd.Parameters.Add("@pjp_long", model.pjp_long);
                sqlcmd.Parameters.Add("@pjp_radius", model.pjp_radius);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt!=null && dt.Rows.Count > 0)
                {
                    odata.status = "200";
                    odata.message = "Successfully added PJP.";
                }
                else
                {
                    odata.status = "205";
                    odata.message = "Data not Added.";
                }

                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage PJPEditList(PJPEditInput model)
        {
            PJPOutPut odata = new PJPOutPut();

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

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_API_PJPInsertUpdateDetails", sqlcon);
                sqlcmd.Parameters.Add("@Action", "PJP_EDIT");
                sqlcmd.Parameters.Add("@PJP_ID", model.PJP_id);
                sqlcmd.Parameters.Add("@UserID", model.user_id);
                sqlcmd.Parameters.Add("@PJP_Date", model.date);
                sqlcmd.Parameters.Add("@From_Time", model.from_time);
                sqlcmd.Parameters.Add("@To_Time", model.to_time);
                sqlcmd.Parameters.Add("@SHOP_CODE", model.cust_id);
                sqlcmd.Parameters.Add("@LOCATIONS", model.location);
                sqlcmd.Parameters.Add("@REMARKS", model.remarks);
                sqlcmd.Parameters.Add("@CREATED_USER", model.creater_user_id);
                sqlcmd.Parameters.Add("@pjp_lat", model.pjp_lat);
                sqlcmd.Parameters.Add("@pjp_long", model.pjp_long);
                sqlcmd.Parameters.Add("@pjp_radius", model.pjp_radius);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    odata.status = "200";
                    odata.message = "Successfully edited PJP.";
                }
                else
                {
                    odata.status = "205";
                    odata.message = "Data not Added.";
                }

                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage PJPDeleteList(PJPDeleetInput model)
        {
            PJPOutPut odata = new PJPOutPut();

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

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_API_PJPInsertUpdateDetails", sqlcon);
                sqlcmd.Parameters.Add("@Action", "PJP_DELETE");
                sqlcmd.Parameters.Add("@PJP_ID", model.PJP_id);
                sqlcmd.Parameters.Add("@UserID", model.user_id);
                sqlcmd.Parameters.Add("@CREATED_USER", model.creater_user_id);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    odata.status = "200";
                    odata.message = "Successfully deleted PJP.";
                }
                else
                {
                    odata.status = "205";
                    odata.message = "Data not Deleted.";
                }

                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage PJPDetailsList(PJPDetailsListInput model)
        {
            List<PJPDetailsList> omodel = new List<PJPDetailsList>();
            PJPDetailsOutupt odata = new PJPDetailsOutupt();

            PJPOutPut output = new PJPOutPut();

            if (!ModelState.IsValid)
            {
                output.status = "213";
                output.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, output);
            }
            else
            {

                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                string sessionId = "";

                List<Locationupdate> omedl2 = new List<Locationupdate>();

                DataSet ds = new DataSet();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_API_PJPInsertUpdateDetails", sqlcon);
                sqlcmd.Parameters.Add("@Action", "PJP_DetailsList");
                sqlcmd.Parameters.Add("@UserID", model.user_id);
                sqlcmd.Parameters.Add("@YEARS", model.year);
                sqlcmd.Parameters.Add("@MONTH", model.month);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(ds);
                sqlcon.Close();
                if (ds.Tables[1].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                        {
                            omodel.Add(new PJPDetailsList()
                            {
                                id = Convert.ToString(ds.Tables[0].Rows[j]["PJP_ID"]),
                                from_time = Convert.ToString(ds.Tables[0].Rows[j]["From_Time"]),
                                to_time = Convert.ToString(ds.Tables[0].Rows[j]["To_Time"]),
                                customer_name = Convert.ToString(ds.Tables[0].Rows[j]["Shop_Name"]),
                                customer_id = Convert.ToString(ds.Tables[0].Rows[j]["SHOP_CODE"]),
                                location = Convert.ToString(ds.Tables[0].Rows[j]["LOCATIONS"]),
                                date = Convert.ToString(ds.Tables[0].Rows[j]["PJP_Date"]),
                                isUpdateable = Convert.ToBoolean(ds.Tables[0].Rows[j]["isUpdateable"]),
                                remarks = Convert.ToString(ds.Tables[0].Rows[j]["REMARKS"]),
                                user_id = Convert.ToString(ds.Tables[0].Rows[j]["User_Id"]),
                                creater_user_id = Convert.ToString(ds.Tables[0].Rows[j]["CREATED_USER"]),
                                pjp_lat = Convert.ToString(ds.Tables[0].Rows[j]["pjp_lat"]),
                                pjp_long = Convert.ToString(ds.Tables[0].Rows[j]["pjp_long"]),
                                pjp_radius = Convert.ToString(ds.Tables[0].Rows[j]["pjp_radius"])
                            });
                        }

                        odata.status = "200";
                        odata.message = "Successfully get PJP list.";
                        odata.supervisor_name = ds.Tables[1].Rows[0]["REPORTTO"].ToString();
                        odata.pjp_list = omodel;

                    }
                    else
                    {
                        odata.status = "205";
                        odata.message = "No data found.";
                        odata.supervisor_name = ds.Tables[1].Rows[0]["REPORTTO"].ToString();
                        odata.pjp_list = omodel;
                    }
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
        public HttpResponseMessage PJPConfigList(PJPConfigInput model)
        {
            PJPConfigOutPut odata = new PJPConfigOutPut();

            PJPOutPut output = new PJPOutPut();

            if (!ModelState.IsValid)
            {
                output.status = "213";
                output.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, output);
            }
            else
            {

                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                string sessionId = "";

                List<Locationupdate> omedl2 = new List<Locationupdate>();

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_API_PJPInsertUpdateDetails", sqlcon);
                sqlcmd.Parameters.Add("@Action", "PJP_ConfigList");
                sqlcmd.Parameters.Add("@UserID", model.user_id);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    odata.status = "200";
                    odata.message = "Successfully get pjp config.";
                    odata.supervisor_name = dt.Rows[0]["REPORTTO"].ToString();
                    odata.pjp_past_days = dt.Rows[0]["pjp_past_days"].ToString();
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
        public HttpResponseMessage TeamLocationList(TeamLocationInput model)
        {
            List<TeamLocationList> omodel = new List<TeamLocationList>();
            TeamLocation odata = new TeamLocation();

            PJPOutPut output = new PJPOutPut();

            if (!ModelState.IsValid)
            {
                output.status = "213";
                output.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, output);
            }
            else
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                string sessionId = "";

                List<Locationupdate> omedl2 = new List<Locationupdate>();

                DataSet ds = new DataSet();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_API_PJPInsertUpdateDetails", sqlcon);
                sqlcmd.Parameters.Add("@Action", "TeamLocationList");
                sqlcmd.Parameters.Add("@UserID", model.user_id);
                sqlcmd.Parameters.Add("@PJP_Date", model.date);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(ds);
                sqlcon.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        omodel.Add(new TeamLocationList()
                        {
                            id = Convert.ToString(ds.Tables[0].Rows[j]["id"]),
                            location_name = Convert.ToString(ds.Tables[0].Rows[j]["location_name"]),
                            latitude = Convert.ToString(ds.Tables[0].Rows[j]["latitude"]),
                            longitude = Convert.ToString(ds.Tables[0].Rows[j]["longitude"]),
                            distance_covered = Convert.ToString(ds.Tables[0].Rows[j]["distance_covered"]),
                            last_update_time = Convert.ToString(ds.Tables[0].Rows[j]["last_update_time"]),
                            shops_covered = Convert.ToString(ds.Tables[0].Rows[j]["shops_covered"]),
                            meetings_attended = Convert.ToString(ds.Tables[0].Rows[j]["meetings_attended"]),
                            network_status = Convert.ToString(ds.Tables[0].Rows[j]["network_status"]),
                            battery_percentage = Convert.ToString(ds.Tables[0].Rows[j]["battery_percentage"])

                        });
                    }
                    odata.status = "200";
                    odata.message = "Successfully get team location.";
                    odata.total_distance = ds.Tables[1].Rows[0]["total_distance"].ToString();
                    odata.total_visit_distance = ds.Tables[1].Rows[0]["total_visit_distance"].ToString();
                    odata.location_details = omodel;
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
        public HttpResponseMessage PJPList(PJPListInput model)
        {
            List<PJPList> omodel = new List<PJPList>();
            PJPListOutupt odata = new PJPListOutupt();

            PJPOutPut output = new PJPOutPut();

            if (!ModelState.IsValid)
            {
                output.status = "213";
                output.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, output);
            }
            else
            {

                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                string sessionId = "";

                List<Locationupdate> omedl2 = new List<Locationupdate>();

                DataSet ds = new DataSet();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_API_PJPInsertUpdateDetails", sqlcon);
                sqlcmd.Parameters.Add("@Action", "PJP_List");
                sqlcmd.Parameters.Add("@UserID", model.user_id);
                sqlcmd.Parameters.Add("@PJP_Date", model.date);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(ds);
                sqlcon.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        omodel.Add(new PJPList()
                        {
                            id = Convert.ToString(ds.Tables[0].Rows[j]["PJP_ID"]),
                            from_time = Convert.ToString(ds.Tables[0].Rows[j]["From_Time"]),
                            to_time = Convert.ToString(ds.Tables[0].Rows[j]["To_Time"]),
                            customer_name = Convert.ToString(ds.Tables[0].Rows[j]["Shop_Name"]),
                            customer_id = Convert.ToString(ds.Tables[0].Rows[j]["SHOP_CODE"]),
                            location = Convert.ToString(ds.Tables[0].Rows[j]["LOCATIONS"]),
                            date = Convert.ToString(ds.Tables[0].Rows[j]["PJP_Date"]),
                            remarks = Convert.ToString(ds.Tables[0].Rows[j]["REMARKS"])
                        });
                    }

                    odata.status = "200";
                    odata.message = "Successfully get PJP list.";
                    odata.pjp_list = omodel;
                }
                else
                {
                    odata.status = "205";
                    odata.message = "No data found.";
                    odata.pjp_list = omodel;
                }

                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }
        }
    }
}

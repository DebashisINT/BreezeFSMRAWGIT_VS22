#region======================================Revision History=========================================================
//Written By : Debashis Talukder On 11/04/2024
//Purpose: For Leaderboard.Row: 905 to 908 & Refer: 0027300
#endregion===================================End of Revision History==================================================

using ShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ShopAPI.Controllers
{
    public class LeaderboardInfoController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage LeaderboardOverallList(LeaderboardOverallInput model)
        {
            LeaderboardOverallOutput omodel = new LeaderboardOverallOutput();
            List<LeaderboarduserlistOutput> oview = new List<LeaderboarduserlistOutput>();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                String ProfileImagePath = System.Configuration.ConfigurationManager.AppSettings["ProfileImageURL"];
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_FTSAPILEADERBOARDPOINTSDETAILS", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "OVERALL");
                sqlcmd.Parameters.AddWithValue("@USERID", model.user_id);
                sqlcmd.Parameters.AddWithValue("@ACTIVITYBASED", model.activitybased);
                sqlcmd.Parameters.AddWithValue("@BRANCHWISE", model.branchwise);
                sqlcmd.Parameters.AddWithValue("@ACTIVITYMODE", model.flag);
                sqlcmd.Parameters.AddWithValue("@PROFILEIMAGEPATH", ProfileImagePath);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<LeaderboarduserlistOutput>(dt);
                    omodel.status = "200";
                    omodel.message = "Success";
                    omodel.user_list = oview;
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

        [HttpPost]
        public HttpResponseMessage LeaderboardOwnList(LeaderboardOwnInput model)
        {
            LeaderboardOwnOutput omodel = new LeaderboardOwnOutput();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                String ProfileImagePath = System.Configuration.ConfigurationManager.AppSettings["ProfileImageURL"];
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_FTSAPILEADERBOARDPOINTSDETAILS", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "OWN");
                sqlcmd.Parameters.AddWithValue("@USERID", model.user_id);
                sqlcmd.Parameters.AddWithValue("@ACTIVITYBASED", model.activitybased);
                sqlcmd.Parameters.AddWithValue("@BRANCHWISE", model.branchwise);
                sqlcmd.Parameters.AddWithValue("@ACTIVITYMODE", model.flag);
                sqlcmd.Parameters.AddWithValue("@PROFILEIMAGEPATH", ProfileImagePath);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Success";
                    omodel.user_id = Convert.ToInt64(dt.Rows[0]["user_id"]);
                    omodel.user_name = Convert.ToString(dt.Rows[0]["user_name"]);
                    omodel.user_phone = Convert.ToString(dt.Rows[0]["user_phone"]);
                    omodel.attendance = Convert.ToDecimal(dt.Rows[0]["attendance"]);
                    omodel.new_visit=Convert.ToDecimal(dt.Rows[0]["new_visit"]);
                    omodel.revisit = Convert.ToDecimal(dt.Rows[0]["revisit"]);
                    omodel.order = Convert.ToDecimal(dt.Rows[0]["order"]);
                    omodel.activities = Convert.ToDecimal(dt.Rows[0]["activities"]);
                    omodel.position = Convert.ToInt32(dt.Rows[0]["position"]);
                    omodel.totalscore = Convert.ToDecimal(dt.Rows[0]["totalscore"]);
                    omodel.profile_pictures_url = Convert.ToString(dt.Rows[0]["profile_pictures_url"]);
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

        [HttpPost]
        public HttpResponseMessage LeaderboardActivityList(LeaderboardActivityListsInput model)
        {
            LeaderboardActivityListsOutput omodel = new LeaderboardActivityListsOutput();
            List<LeaderboardActivitieslists> oview = new List<LeaderboardActivitieslists>();

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
                sqlcmd = new SqlCommand("PRC_FTSAPILEADERBOARDPOINTSDETAILS", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "ACTIVITYLISTS");

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<LeaderboardActivitieslists>(dt);
                    omodel.status = "200";
                    omodel.message = "Success";
                    omodel.activities_list = oview;
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

        [HttpPost]
        public HttpResponseMessage LeaderboardBranchLists(LeaderboardBranchListsInput model)
        {
            LeaderboardBranchListsOutput omodel = new LeaderboardBranchListsOutput();
            List<HeadBranchlistsOutput> Boview = new List<HeadBranchlistsOutput>();

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
                    sqlcmd = new SqlCommand("PRC_FTSAPILEADERBOARDPOINTSDETAILS", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "BRANCHLISTS");
                    sqlcmd.Parameters.AddWithValue("@CHILDBRANCH", Convert.ToString(System.Web.HttpContext.Current.Session["userbranchHierarchy"]));

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(ds);
                    sqlcon.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            List<SubbranchListsOutput> SBoview = new List<SubbranchListsOutput>();
                            for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                            {
                                if (Convert.ToString(ds.Tables[0].Rows[i]["branch_head_id"]) == Convert.ToString(ds.Tables[1].Rows[j]["branch_head_id"]))
                                {
                                    SBoview.Add(new SubbranchListsOutput()
                                    {
                                        id = Convert.ToInt64(ds.Tables[1].Rows[j]["id"]),
                                        value = Convert.ToString(ds.Tables[1].Rows[j]["value"])
                                    });
                                }
                            }

                            Boview.Add(new HeadBranchlistsOutput()
                            {
                                branch_head = Convert.ToString(ds.Tables[0].Rows[i]["branch_head"]),
                                branch_head_id = Convert.ToInt64(ds.Tables[0].Rows[i]["branch_head_id"]),
                                sub_branch = SBoview
                            });
                        }

                        omodel.status = "200";
                        omodel.message = "Successfully Get Lists.";
                        omodel.branch_list = Boview;
                    }
                    else
                    {
                        omodel.status = "205";
                        omodel.message = "No Data Found.";
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

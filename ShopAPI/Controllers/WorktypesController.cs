using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ShopAPI.Models;
using System.Data;
using System.Data.SqlClient;
namespace ShopAPI.Controllers
{
    public class WorktypesController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Types(WorktypesInput model)
        {
            Worktypesoutput odata = new Worktypesoutput();
            try
            {
                List<worktypes> oview = new List<worktypes>();


                string token = string.Empty;
                string versionname = string.Empty;
                System.Net.Http.Headers.HttpRequestHeaders headers = this.Request.Headers;
                String tokenmatch = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (headers.Contains("version_name"))
                {
                    versionname = headers.GetValues("version_name").First();
                }
                if (headers.Contains("token_Number"))
                {
                    token = headers.GetValues("token_Number").First();
                }


                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                // String con = Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]);
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();


                sqlcmd = new SqlCommand("proc_FTS_WorkTypes", sqlcon);
                sqlcmd.Parameters.Add("@Action", "WorkTypes");
                sqlcmd.Parameters.Add("@User_id", model.user_id);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();

                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<worktypes>(dt);
                    odata.status = "200";
                    odata.message = "Success";
                    odata.worktype_list = oview;
                }

                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;

            }

            catch (Exception ex)
            {


                odata.status = "209";

                odata.message = ex.Message;
                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateWorkType(UpdateworktypesInput model)
        {
            Updateworktypesoutput odata = new Updateworktypesoutput();
            try
            {
                if (!ModelState.IsValid)
                {
                    odata.status = "213";
                    odata.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
                }
                else
                {
                    String tokenmatch = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_FTS_UserUpdeateWorkType", sqlcon);
                    sqlcmd.Parameters.Add("@User_Id", model.user_id);
                    sqlcmd.Parameters.Add("@Work_Type", model.work_type);
                    sqlcmd.Parameters.Add("@Work_Desc", model.work_desc);
                    //Etra value update 31-12-2020 Tanmoy
                    sqlcmd.Parameters.Add("@distributor_name", model.distributor_name);
                    sqlcmd.Parameters.Add("@market_worked", model.market_worked);
                    //Etra value update 31-12-2020 Tanmoy
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt!=null && dt.Rows.Count > 0)
                    {
                        odata.status = dt.Rows[0]["status"].ToString();
                        odata.message = dt.Rows[0]["message"].ToString();// "Work Type updated successfully.";
                    }
                    var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                    return message;
                }
            }
            catch (Exception ex)
            {
                odata.status = "209";
                odata.message = ex.Message;
                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }
        }
    }
}


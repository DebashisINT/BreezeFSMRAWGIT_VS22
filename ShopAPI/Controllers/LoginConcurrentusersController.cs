using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ShopAPI.Models;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace ShopAPI.Controllers
{
    public class LoginConcurrentusersController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage InsertConcurrentUser(LoginConcurrentusersInsertInput model)
        {
            LoginConcurrentusersInsertOutput omodel = new LoginConcurrentusersInsertOutput();
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
                sqlcmd = new SqlCommand("PRC_APILOGINCONCURRENTUSERS", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "INSERTUSERIMEI");
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@IMEI", model.imei);
                sqlcmd.Parameters.Add("@DATE_TIME", model.date_time);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Saved Successfully.";
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
        public HttpResponseMessage FetchConcurrentUser(LoginConcurrentusersFetchInput model)
        {
            LoginConcurrentusersFetchOutput omodel = new LoginConcurrentusersFetchOutput();
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
                sqlcmd = new SqlCommand("PRC_APILOGINCONCURRENTUSERS", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "FETCHUSERIMEI");
                sqlcmd.Parameters.Add("@user_id", model.user_id);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Success.";
                    omodel.user_id = model.user_id;
                    omodel.imei = Convert.ToString(dt.Rows[0]["IMEI_NO"]);
                    omodel.date_time = Convert.ToDateTime(dt.Rows[0]["CREATEDATE"]);
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
        public HttpResponseMessage DeleteConcurrentUser(LoginConcurrentusersDeleteInput model)
        {
            LoginConcurrentusersDeleteOutput omodel = new LoginConcurrentusersDeleteOutput();
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
                sqlcmd = new SqlCommand("PRC_APILOGINCONCURRENTUSERS", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "DELETEUSERIMEI");
                sqlcmd.Parameters.Add("@user_id", model.user_id);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Deleted Successfully.";
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
    }
}

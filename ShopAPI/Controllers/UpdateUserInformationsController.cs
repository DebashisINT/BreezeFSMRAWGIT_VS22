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
using System.Xml.Linq;
using System.IO;
using System.Text;
using System.Device;
using System.Device.Location;
using System.Data.Spatial;
using System.Xml;

namespace ShopAPI.Controllers
{
    public class UpdateUserInformationsController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage UpdateUserName(UpdateUserInfoInput model)
        {
            UpdateUserInfoOutput omodel = new UpdateUserInfoOutput();

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
                sqlcmd = new SqlCommand("PRC_APIUSERINFOUPDATE", sqlcon);
                sqlcmd.Parameters.Add("@Action", "UPDATEUSERNAME");
                sqlcmd.Parameters.Add("@USER_ID", model.name_updation_user_id);
                sqlcmd.Parameters.Add("@UPDATED_NAME", model.updated_name);
                sqlcmd.Parameters.Add("@UPDATED_FIRST_NAME", model.updated_first_name);
                sqlcmd.Parameters.Add("@UPDATED_MIDDLE_NAME", model.updated_middle_name);
                sqlcmd.Parameters.Add("@UPDATED_LAST_NAME", model.updated_last_name);
                sqlcmd.Parameters.Add("@UPDATED_BY_USER_ID", model.updated_by_user_id);
                sqlcmd.Parameters.Add("@UPDATION_DATE_TIME", model.updation_date_time);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Name updation success.";
                    omodel.name_updation_user_id=model.name_updation_user_id;
                    omodel.updated_name = model.updated_name;
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
        public HttpResponseMessage UpdateUserLoginID(UpdateUserLoginInput model)
        {
            UpdateUserLoginOutput omodel = new UpdateUserLoginOutput();

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
                sqlcmd = new SqlCommand("PRC_APIUSERINFOUPDATE", sqlcon);
                sqlcmd.Parameters.Add("@Action", "UPDATEUSERLOGINID");
                sqlcmd.Parameters.Add("@USER_ID", model.update_login_id_of_user_id);
                sqlcmd.Parameters.Add("@USERNEWLOGINID", model.user_login_id_new);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0 && Convert.ToString(dt.Rows[0]["STRMESSAGE"]) == "Unique")
                {
                    omodel.status = "200";
                    omodel.message = "Updated Successfully.";
                }
                else
                {
                    omodel.status = "205";
                    omodel.message = "Duplicate Login ID.";
                }

                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateUserOtherID(UpdateUserOtherIDInput model)
        {
            UpdateUserOtherIDOutput omodel = new UpdateUserOtherIDOutput();

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
                sqlcmd = new SqlCommand("PRC_APIUSERINFOUPDATE", sqlcon);
                sqlcmd.Parameters.Add("@Action", "UPDATEUSEROTHERID");
                sqlcmd.Parameters.Add("@USERINTERNALID", model.update_other_id_user_contactid);
                sqlcmd.Parameters.Add("@OTHERID", model.other_id);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0 && Convert.ToString(dt.Rows[0]["STRMESSAGE"]) == "Update")
                {
                    omodel.status = "200";
                    omodel.message = "Updated Successfully.";
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

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
    public class PhoneNoController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage InsertPhoneNo(PhoneNoInsertInput model)
        {
            PhoneNoInsertOutput omodel = new PhoneNoInsertOutput();

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
                sqlcmd = new SqlCommand("PRC_FSMAPIPHONENOINSERTUPDATE", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "INSERTPHNO");
                sqlcmd.Parameters.Add("@session_token", model.session_token);
                sqlcmd.Parameters.Add("@USER_ID", model.user_id);
                sqlcmd.Parameters.Add("@USER_CONTACTID", model.user_contactid);
                sqlcmd.Parameters.Add("@PHONE_NO", model.phone_no);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Saved successfully.";
                }
                else
                {
                    omodel.status = "205";
                    omodel.message = "No data saved.";
                }
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdatePhoneNo(PhoneNoUpdateInput model)
        {
            PhoneNoUpdateOutput omodel = new PhoneNoUpdateOutput();

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
                sqlcmd = new SqlCommand("PRC_FSMAPIPHONENOINSERTUPDATE", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "UPDATEPHNO");
                sqlcmd.Parameters.Add("@session_token", model.session_token);
                sqlcmd.Parameters.Add("@USER_ID", model.user_id);
                sqlcmd.Parameters.Add("@USER_CONTACTID", model.user_contactid);
                sqlcmd.Parameters.Add("@OLD_PHONE_NO", model.old_phone_no);
                sqlcmd.Parameters.Add("@PHONE_NO", model.new_phone_no);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Updated successfully.";
                }
                else
                {
                    omodel.status = "205";
                    omodel.message = "Updated failed.";
                }
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }
    }
}

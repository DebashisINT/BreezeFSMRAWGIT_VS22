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
using System.IO;
using System.Drawing;

namespace ShopAPI.Controllers
{
    public class DuplicateRecordsController : ApiController
    {
        public HttpResponseMessage PhoneNo(DuplicatePhNoFetchInput model)
        {
            DuplicatePhNoFetchOutput odata = new DuplicatePhNoFetchOutput();
            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
            }
            else
            {
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_APICHECKSHOPDUPLICATERECORDS", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "DUPLICATEPHNO");
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@SHOPPHNO", model.new_shop_phone);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    odata.status = "200";
                    odata.message = "Phone number already exist.";
                }
                else
                {
                    odata.status = "205";
                    odata.message = "Phone number does not exist.";
                }
                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }
        }
    }
}

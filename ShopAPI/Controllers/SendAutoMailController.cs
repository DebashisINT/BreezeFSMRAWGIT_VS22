#region======================================Revision History=========================================================
//Written By : Debashis Talukder On 03/04/2024
//Purpose: For Send Auto Mail.Row: 913
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
    public class SendAutoMailController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage SendAutoMailInfo(SendAutoMailInput model)
        {
            SendAutoMailOutput omodel = new SendAutoMailOutput();

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
                sqlcmd = new SqlCommand("PRC_APISENDAUTOMAILINFO", sqlcon);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.automail_sending_email = Convert.ToString(dt.Rows[0]["automail_sending_email"]);
                    omodel.automail_sending_pass = Convert.ToString(dt.Rows[0]["automail_sending_pass"]);
                    omodel.recipient_email_ids = Convert.ToString(dt.Rows[0]["recipient_email_ids"]);
                    omodel.status = "200";
                    omodel.message = "Success.";
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

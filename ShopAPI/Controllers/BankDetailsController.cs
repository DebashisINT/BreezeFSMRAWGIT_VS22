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
    public class BankDetailsController : ApiController
    {
        [HttpPost]
        //[RequireHttps]
        public HttpResponseMessage Update(BankDetailsInput model)
        {
            BankDetailsOutput omodel = new BankDetailsOutput();
            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                string sessionId = "";
                List<Locationupdate> omedl2 = new List<Locationupdate>();
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("prc_Bankdetails", sqlcon);
                sqlcmd.Parameters.Add("@action", "update");
                sqlcmd.Parameters.Add("@session_token", model.session_token);
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@account_holder", model.account_holder);
                sqlcmd.Parameters.Add("@account_no", model.account_no);
                sqlcmd.Parameters.Add("@bank_name", model.bank_name);
                sqlcmd.Parameters.Add("@ifsc", model.ifsc);
                sqlcmd.Parameters.Add("@shop_id", model.shop_id);
                sqlcmd.Parameters.Add("@upi_id", model.upi);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Successfully updated bank details.";
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
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
    public class StateListController : ApiController
    {

        [HttpGet]
        public HttpResponseMessage States()
        {
            List<StateModel> oview = new List<StateModel>();
            StateListoutput odata = new StateListoutput();

            DataTable dt = new DataTable();
            String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
            SqlCommand sqlcmd = new SqlCommand();
            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();


            sqlcmd = new SqlCommand("Sp_ApiStateCityList", sqlcon);
            sqlcmd.Parameters.Add("@Action", "State");
      

            sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dt);
            sqlcon.Close();
            if (dt.Rows.Count > 0)
            {
                oview = APIHelperMethods.ToModelList<StateModel>(dt);
                odata.status = "200";
                odata.message = "Success";
                odata.state_list = oview;
            }

            var message = Request.CreateResponse(HttpStatusCode.OK, odata);
            return message;

        }
    }
}

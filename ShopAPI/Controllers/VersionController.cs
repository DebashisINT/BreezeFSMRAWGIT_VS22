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
    public class VersionController : ApiController
    {
         [HttpPost]
        public HttpResponseMessage Checking(VersionCheckingModel model)
        {
            VersionCheckingModeloutput odata = new VersionCheckingModeloutput();


            DataTable dt = new DataTable();
            String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
            SqlCommand sqlcmd = new SqlCommand();
            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();


            sqlcmd = new SqlCommand("Proc_Version_ApiCheck", sqlcon);
            sqlcmd.Parameters.Add("@DeviceType", model.devicetype);
       
            sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dt);
            sqlcon.Close();

            if (dt.Rows.Count > 0)
            {
                odata = APIHelperMethods.ToModel<VersionCheckingModeloutput>(dt);
             
           
            }

            var message = Request.CreateResponse(HttpStatusCode.OK, odata);
            return message;

        }
        
    }
}

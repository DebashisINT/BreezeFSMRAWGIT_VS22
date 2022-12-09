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
    public class AadharImageDetectionInfoController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage UserAadharInfoSave(AadharImageDetectionInfoInput model)
        {
            AadharImageDetectionInfoOutput odata = new AadharImageDetectionInfoOutput();

            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
            }
            else
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_APIAADHARIMAGEDETECTION", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "INSERTAADHARINFO");
                sqlcmd.Parameters.Add("@USER_ID", model.user_id);
                sqlcmd.Parameters.Add("@NAMEONAADHAR", model.name_on_aadhaar);
                sqlcmd.Parameters.Add("@AADHARDOB", model.DOB_on_aadhaar);
                sqlcmd.Parameters.Add("@AADHAAR_NO", model.Aadhaar_number);
                sqlcmd.Parameters.Add("@REG_DOC_TYP", model.REG_DOC_TYP);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0 && Convert.ToString(dt.Rows[0]["STRMESSAGE"]) == "Unique")
                {
                    odata.status = "200";
                    odata.message = "Aadhaar Unique.";
                    odata.user_id = model.user_id;
                }
                else
                {
                    odata.status = "205";
                    odata.message = "Aadhaar Duplicate.";
                    odata.user_id = model.user_id;
                }
                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }
        }
    }
}

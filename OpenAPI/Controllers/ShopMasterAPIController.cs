/*******************************************************************************************************
 * Written by Sanchita for V2.0.39 on 03/03/2023 - Implement Open API for Shop Master
 * New project OpenAPI added
 ********************************************************************************************************/
using OpenAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Principal;
using System.Web.UI.WebControls;

// POSTMAN HIT CREDENTIALS
//https://localhost:44395/API/ShopMasterAPI/List

//HEADERS
//Content-Type : application/json
//account-id : 31331/55454854
//api-key : AAAAZj4rINg:APA91bHmnhpJuY6068fHwK3RFHPdWAEWkljlkjlkjklEwQ39jf8xzSm2IFDMwifn9e_A6AY8EYWmnP5IVFyJVyFmgv555dsdsemofko53fYEcJUsbpMYMxinjnzcTdOHQwfSfbenM_tzr


//BODY
//{
//    "session_token":"",
//    "user_id":"378",
//    "Uniquecont":"0"
//}
//RAW - JSON
//

namespace OpenAPI.Controllers
{
    public class ShopMasterAPIController : ApiController
    {
        [HttpPost]
        //[RequireHttps]
        public HttpResponseMessage List(ShopsMasterAPIInput model)
        {
            ShopsMasterAPIOutput omodel = new ShopsMasterAPIOutput();
            List<ShopsMastersAPI> oview = new List<ShopsMastersAPI>();
            ShopsMasterAPIDatalists odata = new ShopsMasterAPIDatalists();

            var APIReq = Request;
            var APIheaders = APIReq.Headers;
            string APIAccountid = "";
            string APIKey = "";
            string APIContentType = "";

            //if (APIheaders.Contains("Content-Type"))
            //{
            //    APIContentType = APIheaders.GetValues("Content-Type").First();
            //}
            if (APIheaders.Contains("account-id"))
            {
                APIAccountid = APIheaders.GetValues("account-id").First();
            }
            if (APIheaders.Contains("api-key"))
            {
                APIKey = APIheaders.GetValues("api-key").First();
            }

            //String OpenAPIKey = System.Configuration.ConfigurationManager.AppSettings["OpenAPIKey"];
            //String OpenAPIAccountId = System.Configuration.ConfigurationManager.AppSettings["OpenAPIAccountId"];
            String OpenAPIKey = "";
            String OpenAPIAccountId = "";

            DataTable dtCobfigData = new DataTable();
            String con1 = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
            SqlCommand sqlcmd1 = new SqlCommand();
            SqlConnection sqlcon1 = new SqlConnection(con1);
            sqlcon1.Open();
            sqlcmd1 = new SqlCommand("SP_API_GetshopMasterlists", sqlcon1);
            sqlcmd1.Parameters.AddWithValue("@ACTION", "GETKEY");
            sqlcmd1.Parameters.AddWithValue("@OPENAPI_CLIENTNAME", "FSM_ITC");
            sqlcmd1.Parameters.AddWithValue("@MODULE_NAME", "SHOP MASTER");
            sqlcmd1.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd1);
            da1.Fill(dtCobfigData);
            sqlcon1.Close();
            if (dtCobfigData.Rows.Count > 0)
            {
                OpenAPIKey = Convert.ToString(dtCobfigData.Rows[0]["OPENAPI_KEY"]);
                OpenAPIAccountId = Convert.ToString(dtCobfigData.Rows[0]["OPENAPI_ACCOUNTID"]);
            }


            if (APIKey=="" || APIAccountid=="")
            {
                omodel.status = "201";
                omodel.message = "No API key found in request.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else if (APIKey != OpenAPIKey || APIAccountid != OpenAPIAccountId )
            {
                omodel.status = "202";
                omodel.message = "Invalid authentication credentials.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            //else if(APIContentType == "" || APIContentType != "application/json")
            //{
            //    omodel.status = "203";
            //    omodel.message = "Invalid authentication credentials.";
            //    return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            //}
            else if (APIKey == OpenAPIKey && APIAccountid == OpenAPIAccountId)
            {
                if (!ModelState.IsValid)
                {
                    omodel.status = "213";
                    omodel.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
                }
                else
                {
                    //X509Certificate2 certificate = Request.GetClientCertificate();
                    //string user = certificate.Issuer;
                    //string sub = certificate.Subject;

                    String weburl = System.Configuration.ConfigurationManager.AppSettings["SiteURL"];
                    string DoctorDegree = System.Configuration.ConfigurationManager.AppSettings["DoctorDegree"];
                    string sessionId = "";

                    List<Locationupdate> omedl2 = new List<Locationupdate>();

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("SP_API_GetshopMasterlists", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "GETLIST");
                    sqlcmd.Parameters.AddWithValue("@session_token", model.session_token);
                    sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@Uniquecont", model.Uniquecont);
                    sqlcmd.Parameters.AddWithValue("@Weburl", weburl);
                    sqlcmd.Parameters.AddWithValue("@DoctorDegree", DoctorDegree);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0)
                    {
                        oview = APIHelperMethods.ToModelList<ShopsMastersAPI>(dt);
                        odata.session_token = model.session_token;
                        odata.shopmaster_list = oview;
                        omodel.status = "200";
                        omodel.message = dt.Rows.Count.ToString() + " No. of Shop list available";

                        omodel.data = odata;

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
            else
            {
                omodel.status = "206";
                omodel.message = "Authentication failed.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
           

        }
    }
}
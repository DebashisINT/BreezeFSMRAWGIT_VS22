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
    public class ShopAssignmentController : ApiController
    {

        [HttpPost]
        public HttpResponseMessage GetAssignedToPPList(AssigntoshopPPInput model)
        {

            List<AssigntoshopPP> oview = new List<AssigntoshopPP>();
            AssigntoshopPPOutput odata = new AssigntoshopPPOutput();

            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
            }
            else
            {
                //X509Certificate2 certificate = Request.GetClientCertificate();
                //string user = certificate.Issuer;
                //string sub = certificate.Subject;

                String token = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];
                string sessionId = "";

                List<Locationupdate> omedl2 = new List<Locationupdate>();

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("Proc_ShopAssignmen", sqlcon);
                sqlcmd.Parameters.AddWithValue("@Action", "PP");
                sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);
                sqlcmd.Parameters.AddWithValue("@state_id", model.state_id);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<AssigntoshopPP>(dt);

                    odata.assigned_to_pp_list = oview;
                    odata.status = "200";
                    odata.message = "Order details  available";
                }
                else
                {

                    odata.status = "205";
                    odata.message = "No data found";

                }

                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }

        }
        [HttpPost]
        public HttpResponseMessage GetAssignedToDDList(AssigntoshopDDInput model)
        {
            List<AssigntoshopDD> oview = new List<AssigntoshopDD>();
            AssigntoshopDDOutput odata = new AssigntoshopDDOutput();

            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
            }
            else
            {
                //X509Certificate2 certificate = Request.GetClientCertificate();
                //string user = certificate.Issuer;
                //string sub = certificate.Subject;

                String token = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];
                string sessionId = "";

                List<Locationupdate> omedl2 = new List<Locationupdate>();

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("Proc_ShopAssignmen", sqlcon);
                sqlcmd.Parameters.AddWithValue("@Action", "DD");
                sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);
                sqlcmd.Parameters.AddWithValue("@state_id", model.state_id);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<AssigntoshopDD>(dt);
                    odata.assigned_to_dd_list = oview;
                    odata.status = "200";
                    odata.message = "Order details  available";
                }
                else
                {
                    odata.status = "205";
                    odata.message = "No data found";
                }

                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage GetAssignedToShopList(AssigntoshopInput model)
        {

            List<Assigntoshop> oview = new List<Assigntoshop>();
            AssigntoshopOutput odata = new AssigntoshopOutput();

            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
            }
            else
            {

                String token = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];
                string sessionId = "";

                List<Locationupdate> omedl2 = new List<Locationupdate>();

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("Proc_ShopAssignmen", sqlcon);
                sqlcmd.Parameters.AddWithValue("@Action", "Shop");
                sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);
                sqlcmd.Parameters.AddWithValue("@state_id", model.state_id);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<Assigntoshop>(dt);
                    odata.shop_list = oview;
                    odata.status = "200";
                    odata.message = "Successfully get assigned to shop list";
                }
                else
                {
                    odata.status = "205";
                    odata.message = "No data found";
                }

                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }
        }

    }
}

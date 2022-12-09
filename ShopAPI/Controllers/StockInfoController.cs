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
    public class StockInfoController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage UpdateStock(StockmanagementInput model)
        {
            OrderAddoutput omodel = new OrderAddoutput();
            UserClass oview = new UserClass();

            try
            {
                string token = string.Empty;
                string versionname = string.Empty;
                System.Net.Http.Headers.HttpRequestHeaders headers = this.Request.Headers;
                String tokenmatch = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];

                if (!ModelState.IsValid)
                {
                    omodel.status = "213";
                    omodel.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
                }

                else
                {
                    // String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                    string sessionId = "";


                    DataTable dt = new DataTable();

                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    //String con = Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]);

                    SqlCommand sqlcmd = new SqlCommand();

                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();

                    sqlcmd = new SqlCommand("Proc_FTS_UpdateStock", sqlcon);

                    sqlcmd.Parameters.Add("@user_id", model.user_id);
                    sqlcmd.Parameters.Add("@SessionToken", model.session_token);

                    sqlcmd.Parameters.Add("@stock_id", model.stock_id);
         

                    sqlcmd.Parameters.Add("@closing_stock_month", model.closing_stock_month);
                    sqlcmd.Parameters.Add("@opening_stock_amount", model.opening_stock_amount);
                    sqlcmd.Parameters.Add("@closing_stock_amount", model.closing_stock_amount);
                    sqlcmd.Parameters.Add("@opening_stock_month", model.opening_stock_month);
                    sqlcmd.Parameters.Add("@opening_stock_month_val", model.opening_stock_month_val);
                    sqlcmd.Parameters.Add("@opening_stock_year_val", model.opening_stock_year_val);

                    sqlcmd.Parameters.Add("@closing_stock_month_val", model.closing_stock_month_val);
                    sqlcmd.Parameters.Add("@closing_stock_year_val", model.closing_stock_year_val);
                    sqlcmd.Parameters.Add("@shop_id", model.shop_id);

                    sqlcmd.Parameters.Add("@m_o", model.m_o);
                    sqlcmd.Parameters.Add("@c_o", model.c_o);
                    sqlcmd.Parameters.Add("@p_o", model.p_o);
                    sqlcmd.Parameters.Add("@stock_date", model.stock_date);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();


                    if (dt.Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Stock Added Successfully.";
                    }
                    else
                    {
                        omodel.status = "202";
                        omodel.message = "Invalid user credential.";
                    }

                    var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                    return message;
                }


            }
            catch (Exception ex)
            {


                omodel.status = "209";

                omodel.message = ex.Message;
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }






        }


        [HttpPost]
        public HttpResponseMessage StockList(StockfetchInput model)
        {

            List<Stockfetch> oview = new List<Stockfetch>();
            StockfetchOutput odata = new StockfetchOutput();

            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
            }
            else
            {

                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                string sessionId = "";

                List<Locationupdate> omedl2 = new List<Locationupdate>();

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("Proc_FTC_StockList", sqlcon);
       
                sqlcmd.Parameters.Add("@user_id", model.user_id);


                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<Stockfetch>(dt);

                    odata.stock_list = oview;
                    odata.status = "200";
                    odata.message = "Stock details  available";

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
        public HttpResponseMessage StockLists(StockfetchInput model)
        {

            List<StockfetchList> oview = new List<StockfetchList>();
            StockfetchOutputList odata = new StockfetchOutputList();

            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
            }
            else
            {

                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                string sessionId = "";

                List<Locationupdate> omedl2 = new List<Locationupdate>();

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("Proc_FTC_StockList", sqlcon);

                sqlcmd.Parameters.Add("@user_id", model.user_id);


                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<StockfetchList>(dt);

                    odata.stock_list = oview;
                    odata.status = "200";
                    odata.message = "Stock details  available";

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

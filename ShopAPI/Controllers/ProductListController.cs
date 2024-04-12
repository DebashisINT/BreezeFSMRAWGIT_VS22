#region======================================Revision History=========================================================
//1.0   V2.0.45     Debashis    03/04/2024      Some new parameters have been added.Row: 909 & 910
#endregion===================================End of Revision History==================================================

using ShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
//using System.Security.Cryptography.X509Certificates;
using System.Web.Http;

namespace ShopAPI.Controllers
{
    public class ProductListController : ApiController
    {

        [HttpPost]
        //[RequireHttps]
        public HttpResponseMessage List(ProductclassInput model)
        {
            ProductlistOutput omodel = new ProductlistOutput();
            List<Productclass> oview = new List<Productclass>();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                //Rev Debashis Row:773
                //String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                //String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
                String token = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];
                String weburl = System.Configuration.ConfigurationManager.AppSettings["SiteURL"];
                //End of Rev Debashis Row:773
                string sessionId = ""; 

                List<Locationupdate> omedl2 = new List<Locationupdate>();

                DataSet ds = new DataSet();
                //Rev Debashis Row:773
                //String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                //End of Rev Debashis Row:773
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("Proc_FTS_Productlist", sqlcon);
                //sqlcmd.Parameters.Add("@session_token", model.session_token);
                //Rev Debashis Row:773                
                //sqlcmd.Parameters.Add("@user_id", model.user_id);
                //sqlcmd.Parameters.Add("@last_updated_date", model.last_update_date);
                sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);
                sqlcmd.Parameters.AddWithValue("@last_updated_date", model.last_update_date);
                //End of Rev Debashis Row:773

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(ds);
                sqlcon.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<Productclass>(ds.Tables[1]);
                  
                    omodel.status = "200";
                    omodel.message = ds.Tables[0].Rows[0][0].ToString() + " No. of Product list available";
                    omodel.product_list = oview;

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

        [HttpPost]
        public HttpResponseMessage ProductRate(ProductRateInput model)
        {
            ProductRateOutput omodel = new ProductRateOutput();
            List<ProductRateList> oview = new List<ProductRateList>();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                //Rev Debashis Row:773
                //String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                String token = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];
                //End of Rev Debashis Row:773

                DataSet ds = new DataSet();
                //Rev Debashis Row:773
                //String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                //End of Rev Debashis Row:773
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_APIPRODUCTRATE", sqlcon);
                //Rev Debashis Row:773
                //sqlcmd.Parameters.Add("@user_id", model.user_id);
                //sqlcmd.Parameters.Add("@shop_id", model.shop_id);
                sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);
                sqlcmd.Parameters.AddWithValue("@shop_id", model.shop_id);
                //End of Rev Debashis Row:773

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(ds);
                sqlcon.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<ProductRateList>(ds.Tables[0]);

                    omodel.status = "200";
                    omodel.message = "Successfully get Product Rate list.";
                    omodel.product_rate_list = oview;
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

        [HttpPost]
        public HttpResponseMessage OfflineProductRate(OfflineProductRateInput model)
        {
            OfflineProductRateOutput omodel = new OfflineProductRateOutput();
            List<OfflineProductRateList> oview = new List<OfflineProductRateList>();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                //Rev Debashis Row:773
                //String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                String token = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];
                //End of Rev Debashis Row:773

                DataSet ds = new DataSet();
                //Rev Debashis Row:773
                //String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                //End of Rev Debashis Row:773
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_APIOfflineProductRate", sqlcon);
                //Rev Debashis Row:773
                //sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);
                //End of Rev Debashis Row:773

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(ds);
                sqlcon.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<OfflineProductRateList>(ds.Tables[0]);

                    omodel.status = "200";
                    omodel.message = "Successfully get Product Rate list.";
                    omodel.product_rate_list = oview;
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

        [HttpPost]
        public HttpResponseMessage ModelList(ModelListInput model)
        {
            ModelListOutput omodel = new ModelListOutput();
            List<ModelItemList> oview = new List<ModelItemList>();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                DataTable dt = new DataTable();
                //Rev Debashis Row:773
                //String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                //End of Rev Debashis Row:773
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("Proc_FTS_Itemlist", sqlcon);
                //Rev Debashis Row:773
                //sqlcmd.Parameters.Add("@user_id", model.user_id);
                //sqlcmd.Parameters.Add("@Action", "ProductList");
                sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);
                sqlcmd.Parameters.AddWithValue("@Action", "ProductList");
                //End of Rev Debashis Row:773

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt!=null && dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<ModelItemList>(dt);
                    omodel.status = "200";
                    omodel.message = "Successfully get Model list.";
                    omodel.model_list = oview;
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

        [HttpPost]
        public HttpResponseMessage PrimaryApplicationList(ModelListInput model)
        {
            PrimaryApplicationtOutput omodel = new PrimaryApplicationtOutput();
            List<ItemList> oview = new List<ItemList>();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                DataTable dt = new DataTable();
                //Rev Debashis Row:773
                //String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                //End of Rev Debashis Row:773
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("Proc_FTS_Itemlist", sqlcon);
                //Rev Debashis Row:773
                //sqlcmd.Parameters.Add("@user_id", model.user_id);
                //sqlcmd.Parameters.Add("@Action", "PrimaryApplication");
                sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);
                sqlcmd.Parameters.AddWithValue("@Action", "PrimaryApplication");
                //End of Rev Debashis Row:773

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<ItemList>(dt);
                    omodel.status = "200";
                    omodel.message = "Successfully get Primary Application list.";
                    omodel.primary_application_list = oview;
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

        [HttpPost]
        public HttpResponseMessage SecondaryApplicationList(ModelListInput model)
        {
            SecondaryApplicationOutput omodel = new SecondaryApplicationOutput();
            List<ItemList> oview = new List<ItemList>();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                DataTable dt = new DataTable();
                //Rev Debashis Row:773
                //String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                //End of Rev Debashis Row:773
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("Proc_FTS_Itemlist", sqlcon);
                //Rev Debashis Row:773
                //sqlcmd.Parameters.Add("@user_id", model.user_id);
                //sqlcmd.Parameters.Add("@Action", "SecondaryApplication");
                sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);
                sqlcmd.Parameters.AddWithValue("@Action", "SecondaryApplication");
                //End of Rev Debashis Row:773

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<ItemList>(dt);
                    omodel.status = "200";
                    omodel.message = "Successfully get Secondary Application list.";
                    omodel.secondary_application_list = oview;
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

        //Rev 1.0 Row: 909 & 910
        [HttpPost]
        public HttpResponseMessage ITCProdMastList(ITCProdMastListInput model)
        {
            ITCProdMastListOutput omodel = new ITCProdMastListOutput();
            List<ITCProdMastLists> oview = new List<ITCProdMastLists>();

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
                sqlcmd = new SqlCommand("PRC_FSMITCPRODUCTLIST", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "GETPRODUCTLISTS");
                sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);                

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<ITCProdMastLists>(dt);
                    omodel.status = "200";
                    omodel.message = "Success";
                    omodel.product_list = oview;
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

        [HttpPost]
        public HttpResponseMessage ITCProdRateList(ITCProdRateListInput model)
        {
            ITCProdRateListOutput omodel = new ITCProdRateListOutput();
            List<ITCProdRateLists> oview = new List<ITCProdRateLists>();

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
                sqlcmd = new SqlCommand("PRC_FSMITCPRODUCTLIST", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "GETPRODUCTRATELISTS");
                sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<ITCProdRateLists>(dt);
                    omodel.status = "200";
                    omodel.message = "Success";
                    omodel.product_rate_list = oview;
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
        //End of Rev 1.0 Row: 909 & 910
    }
}

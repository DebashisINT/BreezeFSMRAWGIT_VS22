#region======================================Revision History=========================================================
//Written By : Debashis Talukder On 30/09/2024
//Purpose: Product Stock Management Details.Refer: Row: 982 & 984
#endregion===================================End of Revision History==================================================

using Newtonsoft.Json;
using ShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;

namespace ShopAPI.Controllers
{
    public class OrderWithStockMgmtDetailsController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage ListForProductStock(ListForProductStockInput model)
        {
            ListForProductStockOutput omodel = new ListForProductStockOutput();
            List<ProductStocklistOutput> Soview = new List<ProductStocklistOutput>();
            try
            {
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
                    sqlcmd = new SqlCommand("PRC_FSMSTOCKMGMTINFODETAILS", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "FETCHALLSTOCK");
                    sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Successfully Get List.";
                        Soview = APIHelperMethods.ToModelList<ProductStocklistOutput>(dt);
                        omodel.stock_list = Soview;
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
            catch (Exception ex)
            {
                omodel.status = "204";
                omodel.message = ex.Message;
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateProductBalStock(UpdateProductBalStockInput model)
        {
            UpdateProductBalStockOutput omodel = new UpdateProductBalStockOutput();
            try
            {
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
                    sqlcmd = new SqlCommand("PRC_FSMSTOCKMGMTINFODETAILS", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "UPDATEPRODBALSTOCK");
                    sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@STOCK_SHOPCODE", model.stock_shopcode);
                    sqlcmd.Parameters.AddWithValue("@STOCK_PRODUCTID", model.stock_productid);
                    sqlcmd.Parameters.AddWithValue("@SUBMITTED_QTY", model.submitted_qty);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Data Updated Successfully.";
                    }
                    else
                    {
                        omodel.status = "202";
                        omodel.message = "No entry.";
                    }
                    var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                    return message;
                }
            }
            catch (Exception ex)
            {
                omodel.status = "204";
                omodel.message = ex.Message;
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }
    }
}

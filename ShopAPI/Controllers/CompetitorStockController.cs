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
    public class CompetitorStockController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage AddCompetitorStock(AddCompetitorStockInput model)
        {
            AddCompetitorStockOutPut omodel = new AddCompetitorStockOutPut();
            UserClass oview = new UserClass();
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
                    string sessionId = "";
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    List<AddCompetitorStockProduct> omedl2 = new List<AddCompetitorStockProduct>();

                    foreach (var s2 in model.competitor_stock_list)
                    {
                        omedl2.Add(new AddCompetitorStockProduct()
                        {
                            product_name = s2.product_name,
                            brand_name = s2.brand_name,
                            mrp = s2.mrp,
                            qty = s2.qty
                        });
                    }


                    string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);
                    Stockoutput omodeloutput = new Stockoutput();
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();

                    sqlcmd = new SqlCommand("Proc_FTS_CompetitorStock", sqlcon);
                    sqlcmd.Parameters.Add("@user_id", model.user_id);
                    sqlcmd.Parameters.Add("@SessionToken", model.session_token);
                    sqlcmd.Parameters.Add("@CompetitorStock_Code", model.competitor_stock_id);
                    sqlcmd.Parameters.Add("@Shop_Id", model.shop_id);
                    sqlcmd.Parameters.Add("@CompetitorStock_date", model.visited_datetime);
                    sqlcmd.Parameters.Add("@Product_List", JsonXML);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Competitor Stock Added Successfully.";
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
                omodel.status = "209";
                omodel.message = ex.Message;
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }


        [HttpPost]
        public HttpResponseMessage CompetitorStockList(CompetitorStockListInput model)
        {
            CompetitorStockListOutPut odata = new CompetitorStockListOutPut();

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

                DataSet ds = new DataSet();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("Proc_FTS_CompetitorStockList", sqlcon);
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@date", model.date);


                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(ds);
                sqlcon.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<CompetitorStockProductList> deliverprod = new List<CompetitorStockProductList>();
                    odata.total_stocklist_count = Convert.ToString(ds.Tables[0].Rows[0]["total_stocklist_count"]);
                    List<CompetitorStockLists> oview = new List<CompetitorStockLists>();
                    CompetitorStockLists orderdetailprd = new CompetitorStockLists();
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        List<CompetitorStockProductList> delivermodelproduct = new List<CompetitorStockProductList>();
                        for (int j = 0; j < ds.Tables[2].Rows.Count; j++)
                        {
                            int i1 = 0;
                            if (Convert.ToString(ds.Tables[2].Rows[j]["CompetitorStock_Id"]) == Convert.ToString(ds.Tables[1].Rows[i]["CompetitorStock_Id"]))
                            {
                                delivermodelproduct.Add(new CompetitorStockProductList()
                                {
                                    id = Convert.ToInt32(ds.Tables[2].Rows[j]["CompetitorStock_ProdId"]),
                                    product_name = Convert.ToString(ds.Tables[2].Rows[j]["Product_Name"]),
                                    brand_name = Convert.ToString(ds.Tables[2].Rows[j]["Product_Brand"]),
                                    qty = Convert.ToString(ds.Tables[2].Rows[j]["Product_Qty"]),
                                    mrp = Convert.ToString(ds.Tables[2].Rows[j]["Product_MRP"])
                                });
                            }
                        }
                        oview.Add(new CompetitorStockLists()
                        {
                            product_list = delivermodelproduct,
                            competitor_stock_id = Convert.ToString(ds.Tables[1].Rows[i]["CompetitorStock_Code"]),
                            shop_id = Convert.ToString(ds.Tables[1].Rows[i]["Shop_Code"]),
                            total_qty = Convert.ToString(ds.Tables[1].Rows[i]["total_qty"]),
                            visited_datetime = Convert.ToDateTime(ds.Tables[1].Rows[i]["CompetitorStock_date"])
                        });
                    }

                    odata.competitor_stock_list = oview;
                    odata.status = "200";
                    odata.message = "Competitor Stock  available";
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

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

namespace ShopAPI.Controllers
{
    public class OrderWithProductAttributeController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage OrderWithProductAttribute(OrderWithProductAttributeInputModel model)
        {
            OrderWithProductAttributeOutputModel omodel = new OrderWithProductAttributeOutputModel();
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
                    List<ProductListInput> omedl2 = new List<ProductListInput>();
                    foreach (var s2 in model.product_list)
                    {
                        omedl2.Add(new ProductListInput()
                        {
                            product_id = s2.product_id,
                            product_name = s2.product_name,
                            gender = s2.gender,
                            size=s2.size,
                            qty=s2.qty,
                            color_id=s2.color_id,
                            rate=s2.rate
                        });
                    }

                    string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APIORDERWITHPRODUCTATTRIBUTE", sqlcon);
                    sqlcmd.Parameters.Add("@ACTION", "INSERTDATA");
                    sqlcmd.Parameters.Add("@user_id", model.user_id);
                    sqlcmd.Parameters.Add("@order_id", model.order_id);
                    sqlcmd.Parameters.Add("@shop_id", model.shop_id);
                    sqlcmd.Parameters.Add("@order_date", model.order_date);
                    sqlcmd.Parameters.Add("@JsonXML", JsonXML);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Successfully Order Saved.";
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
        public HttpResponseMessage ListForOrderedProduct(ListOrderProductInputModel model)
        {
            ListOrderProductOutputModel omodel = new ListOrderProductOutputModel();
            List<GenderlistOutput> Goview = new List<GenderlistOutput>();
            List<OrderProductlistOutput> Poview = new List<OrderProductlistOutput>();
            List<ColorlistOutput> Coview = new List<ColorlistOutput>();
            List<SizelistOutput> Soview = new List<SizelistOutput>();
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
                    DataSet ds = new DataSet();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APIORDERWITHPRODUCTATTRIBUTE", sqlcon);
                    sqlcmd.Parameters.Add("@ACTION", "FETCHDATA");
                    sqlcmd.Parameters.Add("@user_id", model.user_id);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(ds);
                    sqlcon.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Successfully Get List.";
                        Goview = APIHelperMethods.ToModelList<GenderlistOutput>(ds.Tables[0]);
                        omodel.Gender_list = Goview;

                        Poview = APIHelperMethods.ToModelList<OrderProductlistOutput>(ds.Tables[1]);
                        omodel.Product_list = Poview;

                        Coview = APIHelperMethods.ToModelList<ColorlistOutput>(ds.Tables[2]);
                        omodel.Color_list = Coview;

                        Soview = APIHelperMethods.ToModelList<SizelistOutput>(ds.Tables[3]);
                        omodel.size_list = Soview;
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
        public HttpResponseMessage NewListForOrderedProduct(NewListOrderProductInputModel model)
        {
            NewListOrderProductOutputModel omodel = new NewListOrderProductOutputModel();
            List<ShoplistOutput> Soview = new List<ShoplistOutput>();
           
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
                    DataSet ds = new DataSet();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APIORDERWITHPRODUCTATTRIBUTE", sqlcon);
                    sqlcmd.Parameters.Add("@ACTION", "SHOPDETAILS");
                    sqlcmd.Parameters.Add("@user_id", model.user_id);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(ds);
                    sqlcon.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {                       
                       for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                       {
                           List<OrderlistOutput> Ooview = new List<OrderlistOutput>();
                           for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                           {
                               List<NewProductlistOutput> Poview = new List<NewProductlistOutput>();
                               for (int k = 0; k < ds.Tables[2].Rows.Count; k++)
                               {
                                   List<NewColorlistOutput> Coview = new List<NewColorlistOutput>();
                                   for (int l = 0; l < ds.Tables[3].Rows.Count; l++)
                                   {
                                       if (Convert.ToString(ds.Tables[3].Rows[l]["order_id"]) == Convert.ToString(ds.Tables[1].Rows[j]["order_id"]) &&
                                           Convert.ToString(ds.Tables[3].Rows[l]["product_id"]) == Convert.ToString(ds.Tables[2].Rows[k]["product_id"]))
                                       {
                                           Coview.Add(new NewColorlistOutput()
                                           {
                                               color_id = Convert.ToString(ds.Tables[3].Rows[l]["color_id"]),
                                               qty = Convert.ToInt32(ds.Tables[3].Rows[l]["qty"]),
                                               size = Convert.ToString(ds.Tables[3].Rows[l]["size"])
                                           });
                                       }
                                   }
                                   if (Convert.ToString(ds.Tables[2].Rows[k]["order_id"]) == Convert.ToString(ds.Tables[1].Rows[j]["order_id"]))
                                   {
                                       Poview.Add(new NewProductlistOutput()
                                       {
                                           gender = Convert.ToString(ds.Tables[2].Rows[k]["gender"]),
                                           product_id = Convert.ToInt32(ds.Tables[2].Rows[k]["product_id"]),
                                           product_name = Convert.ToString(ds.Tables[2].Rows[k]["product_name"]),
                                           color_list = Coview
                                       });
                                   }
                               }

                               if (Convert.ToString(ds.Tables[0].Rows[i]["shop_id"]) == Convert.ToString(ds.Tables[1].Rows[j]["SHOP_ID"]))
                               {
                                   Ooview.Add(new OrderlistOutput()
                                   {
                                       order_date = Convert.ToString(ds.Tables[1].Rows[j]["order_date"]),
                                       order_id = Convert.ToString(ds.Tables[1].Rows[j]["order_id"]),
                                       product_list = Poview
                                   });
                               }
                           }

                           Soview.Add(new ShoplistOutput()
                           {
                               owner_name = Convert.ToString(ds.Tables[0].Rows[i]["owner_name"]),
                               PhoneNumber = Convert.ToString(ds.Tables[0].Rows[i]["PhoneNumber"]),
                               shop_id = Convert.ToString(ds.Tables[0].Rows[i]["shop_id"]),
                               shop_name = Convert.ToString(ds.Tables[0].Rows[i]["shop_name"]),
                               OrderList = Ooview
                           });
                       }

                       omodel.status = "200";
                       omodel.message = "Successfully Get List.";
                       omodel.user_id = model.user_id;
                       omodel.Shop_list = Soview;
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
        public HttpResponseMessage NewProductOrderList(NewProductOrderListInputModel model)
        {
            NewProductOrderListOutputModel omodel = new NewProductOrderListOutputModel();
            List<ProdOrderlistOutput> POview = new List<ProdOrderlistOutput>();

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
                    DataSet ds = new DataSet();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APIORDERWITHPRODUCTATTRIBUTE", sqlcon);
                    sqlcmd.Parameters.Add("@ACTION", "ORDERDETAILS");
                    sqlcmd.Parameters.Add("@user_id", model.user_id);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(ds);
                    sqlcon.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Successfully Get List.";
                        POview = APIHelperMethods.ToModelList<ProdOrderlistOutput>(ds.Tables[0]);
                        omodel.user_id = model.user_id;
                        omodel.order_list = POview;
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

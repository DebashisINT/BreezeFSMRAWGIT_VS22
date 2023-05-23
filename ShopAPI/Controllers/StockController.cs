#region======================================Revision History=========================================================
//1.0   V2.0.39     Debashis    17/05/2023      A new method has been added.Row: 836
#endregion===================================End of Revision History==================================================
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
    public class StockController : ApiController
    {

        public delegate void del(string username, string password, string Provider, string senderId, string mobile, string message, string type);

        [HttpPost]
        public HttpResponseMessage AddStock(StockModel model)
        {
            OrderAddoutput omodel = new OrderAddoutput();
            UserClass oview = new UserClass();
            String username = System.Configuration.ConfigurationManager.AppSettings["username"];
            String password = System.Configuration.ConfigurationManager.AppSettings["password"];
            String Provider = System.Configuration.ConfigurationManager.AppSettings["Provider"];
            String sender = System.Configuration.ConfigurationManager.AppSettings["sender"];
            try
            {
                string products = "";
                string token = string.Empty;
                string versionname = string.Empty;
                System.Net.Http.Headers.HttpRequestHeaders headers = this.Request.Headers;
                String tokenmatch = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];

                if (!ModelState.IsValid)
                {
                    omodel.status = "213";
                    omodel.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
                }
                else
                {
                    if (model.shop_type=="4")
                    {
                        string sessionId = "";
                        DataTable dt = new DataTable();
                        String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                        List<StockProductlist> omedl2 = new List<StockProductlist>();

                        foreach (var s2 in model.product_list)
                        {
                            omedl2.Add(new StockProductlist()
                            {
                                id = s2.id,
                                qty = s2.qty,
                                rate = s2.rate,
                                total_price = s2.total_price,
                                product_name = s2.product_name
                            });
                            products = products + s2.product_name + "," + " Price: " + s2.total_price + "," + " Qty: " + s2.qty + "||";
                        }


                        string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);
                        Stockoutput omodeloutput = new Stockoutput();
                        SqlCommand sqlcmd = new SqlCommand();
                        SqlConnection sqlcon = new SqlConnection(con);
                        sqlcon.Open();

                        sqlcmd = new SqlCommand("Proc_FTS_Stock", sqlcon);
                        sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);
                        sqlcmd.Parameters.AddWithValue("@SessionToken", model.session_token);
                        sqlcmd.Parameters.AddWithValue("@stock_amount", model.stock_amount);
                        sqlcmd.Parameters.AddWithValue("@Stock_Code", model.stock_id);
                        sqlcmd.Parameters.AddWithValue("@Shop_Id", model.shop_id);
                        sqlcmd.Parameters.AddWithValue("@Stock_Address", model.address);
                        sqlcmd.Parameters.AddWithValue("@Stock_date", model.stock_date_time);
                        sqlcmd.Parameters.AddWithValue("@Lat", model.latitude);
                        sqlcmd.Parameters.AddWithValue("@Long", model.longitude);
                        sqlcmd.Parameters.AddWithValue("@Product_List", JsonXML);
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                        da.Fill(dt);
                        sqlcon.Close();


                        if (dt.Rows.Count > 0)
                        {
                            omodeloutput.user_id = Convert.ToString(dt.Rows[0]["user_id"]);
                            omodeloutput.Stock_amount = Convert.ToString(dt.Rows[0]["stock_amount"]);
                            omodeloutput.Stock_id = Convert.ToString(dt.Rows[0]["Stock_Code"]);
                            omodeloutput.description = Convert.ToString(dt.Rows[0]["Shop_Id"]);
                            omodeloutput.Shop_Name = Convert.ToString(dt.Rows[0]["Shop_Name"]);
                            omodeloutput.Shop_Owner = Convert.ToString(dt.Rows[0]["Shop_Owner"]);
                            omodeloutput.Shop_Owner_Contact = Convert.ToString(dt.Rows[0]["Shop_Owner_Contact"]);

                            //string messagetext = "Hi " + omodeloutput.Shop_Owner + " An Stock is initialized against your Shop : " + omodeloutput.Shop_Name + "|| Product List :" + products;
                            //del ldMainAct = new del(SmsSent);
                            //ldMainAct.BeginInvoke(username, password, Provider, sender, omodeloutput.Shop_Owner_Contact, messagetext, "Text", null, null);

                            omodeloutput.status = "200";
                            omodeloutput.message = "Stock Added Successfully.";

                            //    string messagetext = "Hi " + omodeloutput.Shop_Owner + " An Order is initialized against your Shop : " + omodeloutput.Shop_Name + "|| Product List :" + products;
                            //    string res = SmsSent(username, password, Provider, sender, omodeloutput.Shop_Owner_Contact, messagetext, "Text");
                        }
                        else
                        {
                            omodeloutput.status = "202";
                            omodeloutput.message = "No entry.";
                        }
                        var message = Request.CreateResponse(HttpStatusCode.OK, omodeloutput);
                        return message;
                    }
                    else
                    {
                        omodel.status = "204";
                        omodel.message = "Please Select Distributor.";
                        var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                        return message;
                    }
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
        public HttpResponseMessage StockDetailsShopList(StockDetailsfetchInput_Shop model)
        {
            try
            {
                StockDetailsOutput_Shop odata = new StockDetailsOutput_Shop();

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

                    DataSet ds = new DataSet();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("Proc_FTS_StockListDetails", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);
                   // sqlcmd.Parameters.Add("@Date", model.date);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(ds);
                    sqlcon.Close();

                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
                    {
                        odata.total_stocklist_count = Convert.ToString(ds.Tables[0].Rows[0]["total_Stocklist_count"]);

                        List<StockfetchdetailsList_Shop> oview = new List<StockfetchdetailsList_Shop>();
                        OrderfetchdetailsList orderdetailprd = new OrderfetchdetailsList();
                        decimal STOCK_QTY = 0;
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            STOCK_QTY = 0;
                            List<ShopProducts_Shop> delivermodelproduct = new List<ShopProducts_Shop>();
                            for (int j = 0; j < ds.Tables[2].Rows.Count; j++)
                            {
                                if (ds.Tables[2].Rows.Count > 0)
                                {
                                    int i1 = 0;
                                    if (Convert.ToString(ds.Tables[2].Rows[j]["SHOPID"]) == Convert.ToString(ds.Tables[1].Rows[i]["SHOPID"]) && Convert.ToString(ds.Tables[2].Rows[j]["Stock_ID"]) == Convert.ToString(ds.Tables[1].Rows[i]["StockID"]))
                                    {
                                        delivermodelproduct.Add(new ShopProducts_Shop()
                                        {
                                            id = Convert.ToInt32(ds.Tables[2].Rows[j]["PRODID"]),
                                            brand_id = Convert.ToInt32(ds.Tables[2].Rows[j]["brand_id"]),
                                            category_id = Convert.ToInt32(ds.Tables[2].Rows[j]["category_id"]),
                                            watt_id = Convert.ToInt32(ds.Tables[2].Rows[j]["watt_id"]),
                                            brand = Convert.ToString(ds.Tables[2].Rows[j]["brand"]),
                                            category = Convert.ToString(ds.Tables[2].Rows[j]["category"]),
                                            watt = Convert.ToString(ds.Tables[2].Rows[j]["watt"]),
                                            qty = Convert.ToDecimal(ds.Tables[2].Rows[j]["qty"]),
                                            rate = Convert.ToDecimal(ds.Tables[2].Rows[j]["rate"]),
                                            total_price = Convert.ToDecimal(ds.Tables[2].Rows[j]["total_price"]),
                                            product_name = Convert.ToString(ds.Tables[2].Rows[j]["product_name"])
                                        });
                                        STOCK_QTY = STOCK_QTY + Convert.ToDecimal(ds.Tables[2].Rows[j]["qty"]);
                                    }
                                }
                            }
                            oview.Add(new StockfetchdetailsList_Shop()
                            {
                                product_list = delivermodelproduct,
                                shop_id = Convert.ToString(ds.Tables[1].Rows[i]["SHOPID"]),
                                address = Convert.ToString(ds.Tables[1].Rows[i]["ADDRESS"]),
                                shop_name = Convert.ToString(ds.Tables[1].Rows[i]["NAME"]),
                                stock_qty = STOCK_QTY.ToString(),
                                pin_code = Convert.ToString(ds.Tables[1].Rows[i]["PINCODE"]),
                                shop_lat = Convert.ToString(ds.Tables[1].Rows[i]["LATITUDE"]),
                                shop_long = Convert.ToString(ds.Tables[1].Rows[i]["LONGITUDE"]),
                                stock_id = Convert.ToString(ds.Tables[1].Rows[i]["StockCode"]),
                                stock_date_time = string.IsNullOrEmpty(Convert.ToString(ds.Tables[1].Rows[i]["StockDate"])) ? default(DateTime?) : Convert.ToDateTime(ds.Tables[1].Rows[i]["StockDate"]),
                                stock_amount = Convert.ToDecimal(ds.Tables[1].Rows[i]["Stock_amount"]),
                            });
                        }
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
            catch (Exception ex)
            {
                var message = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                return message;
            }
        }

        public void SmsSent(string username, string password, string Provider, string senderId, string mobile, string message, string type)
        {
            //  http://5.189.187.82/sendsms/sendsms.php?username=QHEkaruna&password=baj8piv3&type=Text&sender=KARUNA&mobile=9831892083&message=HELO
            string response = "";
            string url = Provider + "?username=" + username + "&password=" + password + "&type=" + type + "&sender=" + senderId + "&mobile=" + mobile + "&message=" + message;
            if (mobile.Trim() != "")
            {
                try
                {
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    response = httpResponse.StatusCode.ToString();
                }
                catch
                {

                }
            }
            // return response;
        }

        [HttpPost]
        public HttpResponseMessage AddCurrentStock(CurrentStockModel model)
        {
            CurrentStockModelOutPut omodel = new CurrentStockModelOutPut();
            UserClass oview = new UserClass();
            try
            {
                string products = "";
                string token = string.Empty;
                string versionname = string.Empty;
                System.Net.Http.Headers.HttpRequestHeaders headers = this.Request.Headers;
                String tokenmatch = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];

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
                        String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                        List<CurrentStockProductlist> omedl2 = new List<CurrentStockProductlist>();

                        foreach (var s2 in model.stock_product_list)
                        {
                            omedl2.Add(new CurrentStockProductlist()
                            {
                                product_id = s2.product_id,
                                product_stock_qty = s2.product_stock_qty
                            });
                        }


                        string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);
                        Stockoutput omodeloutput = new Stockoutput();
                        SqlCommand sqlcmd = new SqlCommand();
                        SqlConnection sqlcon = new SqlConnection(con);
                        sqlcon.Open();

                        sqlcmd = new SqlCommand("Proc_FTS_CurrentStock", sqlcon);
                        sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);
                        sqlcmd.Parameters.AddWithValue("@SessionToken", model.session_token);
                        sqlcmd.Parameters.AddWithValue("@Stock_Code", model.stock_id);
                        sqlcmd.Parameters.AddWithValue("@Shop_Id", model.shop_id);
                        sqlcmd.Parameters.AddWithValue("@Stock_date", model.visited_datetime);
                        sqlcmd.Parameters.AddWithValue("@Product_List", JsonXML);
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                        da.Fill(dt);
                        sqlcon.Close();


                        if (dt.Rows.Count > 0)
                        {
                            //omodeloutput.user_id = Convert.ToString(dt.Rows[0]["user_id"]);
                            //omodeloutput.Stock_amount = Convert.ToString(dt.Rows[0]["stock_amount"]);
                            //omodeloutput.Stock_id = Convert.ToString(dt.Rows[0]["Stock_Code"]);
                            //omodeloutput.description = Convert.ToString(dt.Rows[0]["Shop_Id"]);
                            //omodeloutput.Shop_Name = Convert.ToString(dt.Rows[0]["Shop_Name"]);
                            //omodeloutput.Shop_Owner = Convert.ToString(dt.Rows[0]["Shop_Owner"]);
                            //omodeloutput.Shop_Owner_Contact = Convert.ToString(dt.Rows[0]["Shop_Owner_Contact"]);

                            omodel.status = "200";
                            omodel.message = "Current Stock Added Successfully.";

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
        public HttpResponseMessage CurrentStockList(CurrentStockListInput model)
        {
            try
            {
                CurrentStockListOutPut odata = new CurrentStockListOutPut();

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

                    DataSet ds = new DataSet();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("Proc_FTS_CurrentStockList", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@Date", model.date);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(ds);
                    sqlcon.Close();

                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
                    {
                        odata.total_stocklist_count = Convert.ToString(ds.Tables[0].Rows[0]["total_stocklist_count"]);

                        List<CurrentStockLists> oview = new List<CurrentStockLists>();
                        OrderfetchdetailsList orderdetailprd = new OrderfetchdetailsList();
                        decimal STOCK_QTY = 0;
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            STOCK_QTY = 0;
                            List<CurrentStockOutProductList> delivermodelproduct = new List<CurrentStockOutProductList>();
                            for (int j = 0; j < ds.Tables[2].Rows.Count; j++)
                            {
                                if (ds.Tables[2].Rows.Count > 0)
                                {
                                    int i1 = 0;
                                    if (Convert.ToString(ds.Tables[2].Rows[j]["CurrentStock_ID"]) == Convert.ToString(ds.Tables[1].Rows[i]["CurrentStockId"]))
                                    {
                                        delivermodelproduct.Add(new CurrentStockOutProductList()
                                        {
                                            id = Convert.ToInt32(ds.Tables[2].Rows[j]["CurrentStock_ProdId"]),
                                            product_id = Convert.ToInt32(ds.Tables[2].Rows[j]["Product_Id"]),
                                            product_name = Convert.ToString(ds.Tables[2].Rows[j]["sProducts_Name"]),
                                            product_stock_qty = Convert.ToString(ds.Tables[2].Rows[j]["Product_Qty"])
                                        });
                                        STOCK_QTY = STOCK_QTY + Convert.ToDecimal(ds.Tables[2].Rows[j]["Product_Qty"]);
                                    }
                                }
                            }
                            oview.Add(new CurrentStockLists()
                            {
                                product_list = delivermodelproduct,
                                shop_id = Convert.ToString(ds.Tables[1].Rows[i]["Shop_Code"]),
                                visited_datetime = Convert.ToDateTime(ds.Tables[1].Rows[i]["Stock_date"]),
                                stock_id = Convert.ToString(ds.Tables[1].Rows[i]["Stock_Code"]),
                                total_qty = STOCK_QTY.ToString()
                            });
                        }
                        odata.stock_list = oview;
                        odata.status = "200";
                        odata.message = "Current Stock details  available";
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
            catch (Exception ex)
            {
                var message = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                return message;
            }
        }

        //Rev 1.0 Row: 836
        [HttpPost]
        public HttpResponseMessage CurrentStockImageLink(CurrentStockImageLinkInput model)
        {
            CurrentStockImageLinkOutput omodel = new CurrentStockImageLinkOutput();
            String APIHostingPort = System.Configuration.ConfigurationManager.AppSettings["APIHostingPort"];
            List<StockWiseImageList> oview = new List<StockWiseImageList>();
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
                sqlcmd = new SqlCommand("PRC_FTSAPICURRENTSTOCKIMAGEINFO", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "CURRENTSTOCKIMAGELINK");
                sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                sqlcmd.Parameters.AddWithValue("@STOCK_CODE", model.stock_id);
                sqlcmd.Parameters.AddWithValue("@BaseURL", APIHostingPort);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<StockWiseImageList>(dt);
                    omodel.user_id = model.user_id;
                    omodel.stock_id = model.stock_id;
                    omodel.stockwise_image_list = oview;
                    omodel.status = "200";
                    omodel.message = "Successfully get Images lists.";
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
        //End of Rev 1.0 Row: 836
    }
}

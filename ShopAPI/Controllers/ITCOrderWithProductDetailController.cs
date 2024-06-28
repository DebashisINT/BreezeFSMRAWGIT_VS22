#region======================================Revision History=========================================================
//Written By : Debashis Talukder On 03/04/2024
//Purpose: For ITC New Order.Row: 911 to 912
//1.0   V2.0.47     Debashis    06/06/2024      Some new methods & parameters have been added.Row: 938 to 940
//2.0   V2.0.47     Debashis    11/06/2024      Some new parameters have been added.Row: 942 to 944
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

namespace ShopAPI.Controllers
{
    public class ITCOrderWithProductDetailController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage ITCOrderWithProductDetailSave(ITCOrderWithProductDetailSaveInput model)
        {
            ITCOrderWithProductDetailSaveOutput omodel = new ITCOrderWithProductDetailSaveOutput();

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
                    List<ITCOrderProductLists> omodel2 = new List<ITCOrderProductLists>();
                    foreach (var s2 in model.product_list)
                    {
                        omodel2.Add(new ITCOrderProductLists()
                        {
                            order_id = s2.order_id,
                            product_id = s2.product_id,
                            product_name = s2.product_name,
                            submitedQty = s2.submitedQty,
                            submitedSpecialRate = s2.submitedSpecialRate,
                            //Rev 2.0 Row: 942
                            total_amt = s2.total_amt,
                            mrp=s2.mrp,
                            itemPrice=s2.itemPrice
                            //End of Rev 2.0 Row: 942
                        });
                    }

                    string JsonXML = XmlConversion.ConvertToXml(omodel2, 0);

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APIITCORDERWITHPRODUCTDETAIL", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "INSERTDATA");
                    sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@ORDER_CODE", model.order_id);
                    sqlcmd.Parameters.AddWithValue("@ORDER_DATE", model.order_date);
                    sqlcmd.Parameters.AddWithValue("@ORDER_TIME", model.order_time);
                    sqlcmd.Parameters.AddWithValue("@ORDER_DATE_TIME", model.order_date_time);
                    sqlcmd.Parameters.AddWithValue("@SHOP_ID", model.shop_id);
                    sqlcmd.Parameters.AddWithValue("@SHOP_NAME", model.shop_name);
                    sqlcmd.Parameters.AddWithValue("@SHOP_TYPE", model.shop_type);
                    sqlcmd.Parameters.AddWithValue("@ISINRANGE", model.isInrange);
                    sqlcmd.Parameters.AddWithValue("@ORDER_LAT", model.order_lat);
                    sqlcmd.Parameters.AddWithValue("@ORDER_LONG", model.order_long);
                    sqlcmd.Parameters.AddWithValue("@SHOP_ADDRESS", model.shop_addr);
                    sqlcmd.Parameters.AddWithValue("@SHOP_PINCODE", model.shop_pincode);
                    sqlcmd.Parameters.AddWithValue("@ORDER_TOTALAMOUNT", model.order_total_amt);
                    sqlcmd.Parameters.AddWithValue("@ORDER_REMARKS", model.order_remarks);
                    sqlcmd.Parameters.AddWithValue("@JsonXML", JsonXML);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0 && Convert.ToString(dt.Rows[0][0]) == model.order_id)
                    {
                        omodel.status = "200";
                        omodel.message = "Order Saved Successfully.";
                    }
                    else
                    {
                        omodel.status = "205";
                        omodel.message = "Data not Saved.";
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

        //Rev 1.0
        [HttpPost]
        public HttpResponseMessage ITCOrderWithProductDetailEdit(ITCOrderWithProductDetailEditInput model)
        {
            ITCOrderWithProductDetailEditOutput omodel = new ITCOrderWithProductDetailEditOutput();

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
                    List<ITCOrderEditProductLists> omodel2 = new List<ITCOrderEditProductLists>();
                    foreach (var s2 in model.product_list)
                    {
                        omodel2.Add(new ITCOrderEditProductLists()
                        {
                            order_id = s2.order_id,
                            product_id = s2.product_id,
                            product_name = s2.product_name,
                            submitedQty = s2.submitedQty,
                            submitedSpecialRate = s2.submitedSpecialRate,
                            //Rev 2.0 Row: 944
                            total_amt = s2.total_amt,
                            mrp = s2.mrp,
                            itemPrice = s2.itemPrice
                            //End of Rev 2.0 Row: 944
                        });
                    }

                    string JsonXML = XmlConversion.ConvertToXml(omodel2, 0);

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APIITCORDERWITHPRODUCTDETAIL", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "EDITDATA");
                    sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@ORDER_CODE", model.order_id);
                    sqlcmd.Parameters.AddWithValue("@ORDER_DATE", model.order_date);
                    sqlcmd.Parameters.AddWithValue("@ORDER_TIME", model.order_time);
                    sqlcmd.Parameters.AddWithValue("@ORDER_DATE_TIME", model.order_date_time);
                    sqlcmd.Parameters.AddWithValue("@SHOP_ID", model.shop_id);
                    sqlcmd.Parameters.AddWithValue("@SHOP_NAME", model.shop_name);
                    sqlcmd.Parameters.AddWithValue("@SHOP_TYPE", model.shop_type);
                    sqlcmd.Parameters.AddWithValue("@ISINRANGE", model.isInrange);
                    sqlcmd.Parameters.AddWithValue("@ORDER_LAT", model.order_lat);
                    sqlcmd.Parameters.AddWithValue("@ORDER_LONG", model.order_long);
                    sqlcmd.Parameters.AddWithValue("@SHOP_ADDRESS", model.shop_addr);
                    sqlcmd.Parameters.AddWithValue("@SHOP_PINCODE", model.shop_pincode);
                    sqlcmd.Parameters.AddWithValue("@ORDER_TOTALAMOUNT", model.order_total_amt);
                    sqlcmd.Parameters.AddWithValue("@ORDER_REMARKS", model.order_remarks);
                    sqlcmd.Parameters.AddWithValue("@ORDER_EDIT_DATE_TIME", model.order_edit_date_time);
                    sqlcmd.Parameters.AddWithValue("@ORDER_EDIT_REMARKS", model.order_edit_remarks);
                    sqlcmd.Parameters.AddWithValue("@JsonXML", JsonXML);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0 && Convert.ToString(dt.Rows[0][0]) == model.order_id)
                    {
                        omodel.status = "200";
                        omodel.message = "Order Saved Successfully.";
                    }
                    else
                    {
                        omodel.status = "205";
                        omodel.message = "Data not Saved.";
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
        public HttpResponseMessage ITCOrderWithProductDetailDelete(ITCOrderWithProductDetailDeleteInput model)
        {
            ITCOrderWithProductDetailDeleteOutput omodel = new ITCOrderWithProductDetailDeleteOutput();

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
                    List<ITCDeleteProductLists> omodel2 = new List<ITCDeleteProductLists>();
                    foreach (var s2 in model.order_delete_list)
                    {
                        omodel2.Add(new ITCDeleteProductLists()
                        {
                            order_id = s2.order_id
                        });
                    }

                    string JsonXML = XmlConversion.ConvertToXml(omodel2, 0);

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APIITCORDERWITHPRODUCTDETAIL", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "DELETEDATA");
                    sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@JsonXML", JsonXML);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Order Deleted Successfully.";
                    }
                    else
                    {
                        omodel.status = "205";
                        omodel.message = "Data not found.";
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
        //End of Rev 1.0

        [HttpPost]
        public HttpResponseMessage ITCListForOrderedProduct(ITCListforOrderProductInput model)
        {
            ITCListforOrderProductOutput omodel = new ITCListforOrderProductOutput();
            List<ITCOrderlistOutput> Ooview = new List<ITCOrderlistOutput>();

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
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APIITCORDERWITHPRODUCTDETAIL", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "FETCHORDPRODDETAIL");
                    sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(ds);
                    sqlcon.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            List<ITCOrderProductListOutput> Poview = new List<ITCOrderProductListOutput>();
                            for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                            {
                                if (Convert.ToString(ds.Tables[0].Rows[i]["order_id"]) == Convert.ToString(ds.Tables[1].Rows[j]["order_id"]))
                                {
                                    Poview.Add(new ITCOrderProductListOutput()
                                    {
                                        order_id = Convert.ToString(ds.Tables[1].Rows[j]["order_id"]),
                                        product_id = Convert.ToInt64(ds.Tables[1].Rows[j]["product_id"]),
                                        product_name = Convert.ToString(ds.Tables[1].Rows[j]["product_name"]),
                                        submitedQty = Convert.ToDecimal(ds.Tables[1].Rows[j]["submitedQty"]),
                                        submitedSpecialRate = Convert.ToDecimal(ds.Tables[1].Rows[j]["submitedSpecialRate"]),
                                        //Rev 2.0 Row: 943
                                        total_amt = Convert.ToDecimal(ds.Tables[1].Rows[j]["total_amt"]),
                                        mrp = Convert.ToDecimal(ds.Tables[1].Rows[j]["mrp"]),
                                        itemPrice = Convert.ToDecimal(ds.Tables[1].Rows[j]["itemPrice"])
                                        //End of Rev 2.0 Row: 943
                                    });
                                }
                            }

                            Ooview.Add(new ITCOrderlistOutput()
                            {
                                order_id = Convert.ToString(ds.Tables[0].Rows[i]["order_id"]),
                                order_date = Convert.ToString(ds.Tables[0].Rows[i]["order_date"]),
                                order_time = Convert.ToString(ds.Tables[0].Rows[i]["order_time"]),
                                order_date_time = Convert.ToDateTime(ds.Tables[0].Rows[i]["order_date_time"]),
                                shop_id = Convert.ToString(ds.Tables[0].Rows[i]["shop_id"]),
                                shop_name = Convert.ToString(ds.Tables[0].Rows[i]["shop_name"]),
                                shop_type = Convert.ToInt32(ds.Tables[0].Rows[i]["shop_type"]),
                                isInrange = Convert.ToInt32(ds.Tables[0].Rows[i]["isInrange"]),
                                order_lat = Convert.ToString(ds.Tables[0].Rows[i]["order_lat"]),
                                order_long = Convert.ToString(ds.Tables[0].Rows[i]["order_long"]),
                                shop_addr = Convert.ToString(ds.Tables[0].Rows[i]["shop_addr"]),
                                shop_pincode = Convert.ToString(ds.Tables[0].Rows[i]["shop_pincode"]),
                                order_total_amt = Convert.ToDecimal(ds.Tables[0].Rows[i]["order_total_amt"]),
                                order_remarks = Convert.ToString(ds.Tables[0].Rows[i]["order_remarks"]),
                                isUploaded = Convert.ToString(ds.Tables[0].Rows[i]["isUploaded"]),
                                //Rev 1.0
                                order_edit_date_time = Convert.ToDateTime(ds.Tables[0].Rows[i]["order_edit_date_time"]),
                                order_edit_remarks = Convert.ToString(ds.Tables[0].Rows[i]["order_edit_remarks"]),
                                isEdited = Convert.ToString(ds.Tables[0].Rows[i]["isEdited"]),
                                //End of Rev 1.0
                                product_list = Poview
                            });
                        }

                        omodel.status = "200";
                        omodel.message = "Successfully Get List.";
                        omodel.user_id = model.user_id;
                        omodel.order_list = Ooview;
                    }
                    else
                    {
                        omodel.status = "205";
                        omodel.message = "No Data Found.";
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

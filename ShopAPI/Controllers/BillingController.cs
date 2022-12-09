using Newtonsoft.Json.Linq;
using ShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Net.Http.Formatting;


namespace ShopAPI.Controllers
{
    public class BillingController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage AddBilling(BillingModel model)
        {
            try
            {
                AlermShopvisitOutput odata = new AlermShopvisitOutput();

                if (!ModelState.IsValid)
                {
                    odata.status = "213";
                    odata.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
                }
                else
                {
                    string products = "";
                    String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                    string sessionId = "";

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];

                    //Add Product in add billing Tanmoy 22-11-2019
                    List<ProductList> omedl2 = new List<ProductList>();

                    if (model.product_list != null)
                    {
                        foreach (var s2 in model.product_list)
                        {
                            omedl2.Add(new ProductList()
                            {
                                id = s2.id,
                                qty = s2.qty,
                                rate = s2.rate,
                                total_price = s2.total_price,
                                product_name = s2.product_name
                            });

                            products = products + s2.product_name + "," + " Price: " + s2.total_price + "," + " Qty: " + s2.qty + "||";
                        }
                    }


                    string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);
                    //End Add Product in add billing Tanmoy 22-11-2019

                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();

                    sqlcmd = new SqlCommand("Proc_FTS_BillingManagement", sqlcon);
                    sqlcmd.Parameters.Add("@user_id", model.user_id);
                    sqlcmd.Parameters.Add("@bill_id", model.bill_id);
                    sqlcmd.Parameters.Add("@invoice_no", model.invoice_no);
                    sqlcmd.Parameters.Add("@invoice_date", model.invoice_date);
                    sqlcmd.Parameters.Add("@invoice_amount", model.invoice_amount);
                    sqlcmd.Parameters.Add("@remarks", model.remarks);
                    sqlcmd.Parameters.Add("@order_id", model.order_id);
                    //Add Product in add billing Tanmoy 22-11-2019
                    sqlcmd.Parameters.Add("@Product_List", JsonXML);
                    //End Add Product in add billing Tanmoy 22-11-2019
                    sqlcmd.Parameters.Add("@Action", "Insert");
                    //Extra Input for 4Basecare
                    sqlcmd.Parameters.Add("@patient_no", model.patient_no);
                    sqlcmd.Parameters.Add("@patient_name", model.patient_name);
                    sqlcmd.Parameters.Add("@patient_address ", model.patient_address);
                    //Extra Input for 4Basecare

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0)
                    {
                        odata.status = "200";
                        odata.message = "Billing details added successfully.";
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


        [HttpPost]
        public HttpResponseMessage List(BillingListInput model)
        {
            try
            {
                BillingListOutput odata = new BillingListOutput();

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
                    String BillingImage = System.Configuration.ConfigurationSettings.AppSettings["BillingImage"];

                    DataSet ds = new DataSet();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();

                    sqlcmd = new SqlCommand("Proc_FTS_BillingManagement", sqlcon);

                    sqlcmd.Parameters.Add("@user_id", model.user_id);
                    sqlcmd.Parameters.Add("@order_id", model.order_id);
                    sqlcmd.Parameters.Add("@Action", "List");
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(ds);
                    sqlcon.Close();

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        List<BillingModelList> oview = new List<BillingModelList>();

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            //Add Product in Billing List 22-11-2019
                            List<ProductsBillList> product = new List<ProductsBillList>();
                            if (ds.Tables[1].Rows.Count != null && ds.Tables[1].Rows.Count > 0)
                            {
                                for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                                {
                                    if (Convert.ToString(ds.Tables[0].Rows[i]["BillingId"]) == Convert.ToString(ds.Tables[1].Rows[j]["Billing_ID"]))
                                    {
                                        product.Add(new ProductsBillList()
                                        {
                                            id = Convert.ToString(ds.Tables[1].Rows[j]["Product_Id"]),
                                            product_name = Convert.ToString(ds.Tables[1].Rows[j]["sProducts_Description"]),

                                            brand = Convert.ToString(ds.Tables[1].Rows[j]["brand"]),
                                            brand_id = Convert.ToString(ds.Tables[1].Rows[j]["brand_id"]),
                                            category = Convert.ToString(ds.Tables[1].Rows[j]["category"]),
                                            category_id = Convert.ToString(ds.Tables[1].Rows[j]["category_id"]),
                                            watt = Convert.ToString(ds.Tables[1].Rows[j]["watt"]),
                                            watt_id = Convert.ToString(ds.Tables[1].Rows[j]["watt_id"]),

                                            qty = Convert.ToDecimal(ds.Tables[1].Rows[j]["Product_Qty"]),
                                            rate = Convert.ToDecimal(ds.Tables[1].Rows[j]["Product_Rate"]),
                                            total_price = Convert.ToDecimal(ds.Tables[1].Rows[j]["Product_TotalAmount"])
                                        });
                                    }
                                }
                            }
                            //End Add Product in Billing List 22-11-2019

                            oview.Add(new BillingModelList()
                            {
                                user_id = Convert.ToString(ds.Tables[0].Rows[i]["User_Id"]),
                                bill_id = Convert.ToString(ds.Tables[0].Rows[i]["bill_id"]),
                                invoice_no = Convert.ToString(ds.Tables[0].Rows[i]["invoice_no"]),
                                invoice_date = Convert.ToString(ds.Tables[0].Rows[i]["invoice_date"]),
                                invoice_amount = Convert.ToString(ds.Tables[0].Rows[i]["invoice_amount"]),
                                remarks = Convert.ToString(ds.Tables[0].Rows[i]["Remarks"]),
                                order_id = Convert.ToString(ds.Tables[0].Rows[i]["OrderCode"]),
                                billing_image = BillingImage + Convert.ToString(ds.Tables[0].Rows[i]["Inv_Img_id"]),
                                patient_no = Convert.ToString(ds.Tables[0].Rows[i]["patient_no"]),
                                patient_name = Convert.ToString(ds.Tables[0].Rows[i]["patient_name"]),
                                patient_address = Convert.ToString(ds.Tables[0].Rows[i]["patient_address"]),
                                product_list = product
                            });
                        }

                        odata.billing_list = oview;
                        odata.status = "200";
                        odata.message = "Billing list";
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

        [HttpPost]
        public HttpResponseMessage EditBilling(BillingModel model)
        {

            try
            {
                AlermShopvisitOutput odata = new AlermShopvisitOutput();

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

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();

                    sqlcmd = new SqlCommand("Proc_FTS_BillingManagement", sqlcon);
                    sqlcmd.Parameters.Add("@user_id", model.user_id);
                    sqlcmd.Parameters.Add("@bill_id", model.bill_id);
                    sqlcmd.Parameters.Add("@invoice_no", model.invoice_no);
                    sqlcmd.Parameters.Add("@invoice_date", model.invoice_date);
                    sqlcmd.Parameters.Add("@invoice_amount", model.invoice_amount);
                    sqlcmd.Parameters.Add("@remarks", model.remarks);
                    sqlcmd.Parameters.Add("@order_id", model.order_id);
                    sqlcmd.Parameters.Add("@Action", "Edit");

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0)
                    {
                        odata.status = "200";
                        odata.message = "Billing details Edited successfully.";
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

        [HttpPost]
        public HttpResponseMessage DeleteBilling(BillingModel model)
        {
            try
            {
                AlermShopvisitOutput odata = new AlermShopvisitOutput();

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

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();

                    sqlcmd = new SqlCommand("Proc_FTS_BillingManagement", sqlcon);
                    sqlcmd.Parameters.Add("@user_id", model.user_id);
                    sqlcmd.Parameters.Add("@bill_id", model.bill_id);
                    sqlcmd.Parameters.Add("@order_id", model.order_id);
                    sqlcmd.Parameters.Add("@Action", "Delete");

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0)
                    {
                        odata.status = "200";
                        odata.message = "Billing details deleted successfully.";
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
    }
}

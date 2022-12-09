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
    public class SalesQuotationController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage SalesQuotationSave(SalesQuotationSaveInput model)
        {
            SalesQuotationSaveOutput omodel = new SalesQuotationSaveOutput();
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
                    List<ProductListSaveInput> omedl2 = new List<ProductListSaveInput>();
                    foreach (var s2 in model.product_list)
                    {
                        omedl2.Add(new ProductListSaveInput()
                        {
                            product_id = s2.product_id,
                            color_id = s2.color_id,
                            rate_sqft = s2.rate_sqft,
                            rate_sqmtr = s2.rate_sqmtr,
                            qty = s2.qty,
                            amount=s2.amount
                        });
                    }

                    string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APISALESQUOTATIONADDUPDATEDELFETCH", sqlcon);
                    sqlcmd.Parameters.Add("@ACTION", "SAVEDATA");
                    sqlcmd.Parameters.Add("@USER_ID", model.user_id);
                    sqlcmd.Parameters.Add("@QUOTATIONSAVE_DATE", model.save_date_time);
                    sqlcmd.Parameters.Add("@QUOTATION_NUMBER", model.quotation_number);
                    sqlcmd.Parameters.Add("@QUOTATION_DATE_SELECTION", model.quotation_date_selection);
                    sqlcmd.Parameters.Add("@PROJECT_NAME", model.project_name);
                    sqlcmd.Parameters.Add("@TAXES", model.taxes);
                    sqlcmd.Parameters.Add("@FREIGHT", model.Freight);
                    sqlcmd.Parameters.Add("@DELIVERY_TIME", model.delivery_time);
                    sqlcmd.Parameters.Add("@PAYMENT", model.payment);
                    sqlcmd.Parameters.Add("@VALIDITY", model.validity);
                    sqlcmd.Parameters.Add("@BILLING", model.billing);
                    sqlcmd.Parameters.Add("@PRODUCT_TOLERANCE_OF_THICKNESS", model.product_tolerance_of_thickness);
                    sqlcmd.Parameters.Add("@TOLERANCE_OF_COATING_THICKNESS", model.tolerance_of_coating_thickness);
                    sqlcmd.Parameters.Add("@SALESMAN_USER_ID", model.salesman_user_id);
                    sqlcmd.Parameters.Add("@shop_id", model.shop_id);
                    sqlcmd.Parameters.Add("@QUOTATION_CREATED_LAT", model.quotation_created_lat);
                    sqlcmd.Parameters.Add("@QUOTATION_CREATED_LONG", model.quotation_created_long);
                    sqlcmd.Parameters.Add("@QUOTATION_CREATED_ADDRESS", model.quotation_created_address);
                    //Rev Debashis Row:732
                    sqlcmd.Parameters.Add("@REMARKS", model.Remarks);
                    sqlcmd.Parameters.Add("@DOCUMENT_NUMBER", model.document_number);
                    sqlcmd.Parameters.Add("@QUOTATION_STATUS", model.quotation_status);
                    //End of Rev Debashis Row:732
                    sqlcmd.Parameters.Add("@JsonXML", JsonXML);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0 && Convert.ToString(dt.Rows[0]["STRMESSAGE"]) == "Unique")
                    {
                        omodel.status = "200";
                        omodel.message = "Saved successfully.";
                    }
                    else
                    {
                        omodel.status = "205";
                        omodel.message = "Duplicate Quotation Number.";
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
        public HttpResponseMessage ShopWiseSalesQuotationList(ShopWiseSalesQuotationListInput model)
        {
            ShopWiseSalesQuotationListOutput omodel = new ShopWiseSalesQuotationListOutput();
            List<ShopwisesalesquotationlistOutput> QLview = new List<ShopwisesalesquotationlistOutput>();

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
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APISALESQUOTATIONADDUPDATEDELFETCH", sqlcon);
                    sqlcmd.Parameters.Add("@ACTION", "SHOPWISEQUOTATIONLIST");
                    sqlcmd.Parameters.Add("@SHOP_ID", model.shop_id);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Successfully Get List.";
                        QLview = APIHelperMethods.ToModelList<ShopwisesalesquotationlistOutput>(dt);
                        omodel.shop_id = model.shop_id;
                        omodel.shop_name = Convert.ToString(dt.Rows[0]["shop_name"]);
                        omodel.shop_phone_no = Convert.ToString(dt.Rows[0]["shop_phone_no"]);
                        omodel.shop_wise_quotation_list = QLview;
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
        public HttpResponseMessage SalesQuotationList(SalesQuotationListInput model)
        {
            SalesQuotationListOutput omodel = new SalesQuotationListOutput();
            List<QuotationProductDetailsList> QLview = new List<QuotationProductDetailsList>();

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
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APISALESQUOTATIONADDUPDATEDELFETCH", sqlcon);
                    sqlcmd.Parameters.Add("@ACTION", "SHOWQUOTATIONDETAILS");
                    sqlcmd.Parameters.Add("@QUOTATION_NUMBER", model.quotation_number);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Successfully Get List.";
                        QLview = APIHelperMethods.ToModelList<QuotationProductDetailsList>(dt);
                        omodel.quotation_number = Convert.ToString(dt.Rows[0]["quotation_number"]);
                        omodel.save_date_time = Convert.ToString(dt.Rows[0]["save_date_time"]);
                        omodel.quotation_date_selection = Convert.ToString(dt.Rows[0]["quotation_date_selection"]);
                        omodel.project_name = Convert.ToString(dt.Rows[0]["project_name"]);
                        omodel.taxes = Convert.ToString(dt.Rows[0]["taxes"]);
                        omodel.Freight = Convert.ToString(dt.Rows[0]["Freight"]);
                        omodel.delivery_time = Convert.ToString(dt.Rows[0]["delivery_time"]);
                        omodel.payment = Convert.ToString(dt.Rows[0]["payment"]);
                        omodel.validity = Convert.ToString(dt.Rows[0]["validity"]);
                        omodel.billing = Convert.ToString(dt.Rows[0]["billing"]);
                        omodel.product_tolerance_of_thickness = Convert.ToString(dt.Rows[0]["product_tolerance_of_thickness"]);
                        omodel.tolerance_of_coating_thickness = Convert.ToString(dt.Rows[0]["tolerance_of_coating_thickness"]);
                        omodel.salesman_user_id = Convert.ToString(dt.Rows[0]["salesman_user_id"]);
                        omodel.shop_id = Convert.ToString(dt.Rows[0]["shop_id"]);
                        omodel.shop_name = Convert.ToString(dt.Rows[0]["shop_name"]);
                        omodel.shop_phone_no = Convert.ToString(dt.Rows[0]["shop_phone_no"]);
                        omodel.quotation_created_lat = Convert.ToString(dt.Rows[0]["quotation_created_lat"]);
                        omodel.quotation_created_long = Convert.ToString(dt.Rows[0]["quotation_created_long"]);
                        omodel.quotation_created_address = Convert.ToString(dt.Rows[0]["quotation_created_address"]);
                        omodel.shop_addr = Convert.ToString(dt.Rows[0]["shop_addr"]);
                        omodel.shop_email = Convert.ToString(dt.Rows[0]["shop_email"]);
                        omodel.shop_owner_name = Convert.ToString(dt.Rows[0]["shop_owner_name"]);
                        omodel.salesman_name = Convert.ToString(dt.Rows[0]["salesman_name"]);
                        omodel.salesman_designation = Convert.ToString(dt.Rows[0]["salesman_designation"]);
                        omodel.salesman_login_id = Convert.ToString(dt.Rows[0]["salesman_login_id"]);
                        omodel.salesman_email = Convert.ToString(dt.Rows[0]["salesman_email"]);
                        omodel.salesman_phone_no = Convert.ToString(dt.Rows[0]["salesman_phone_no"]);
                        //Rev Debashis Row:734
                        omodel.Remarks = Convert.ToString(dt.Rows[0]["Remarks"]);
                        omodel.document_number = Convert.ToString(dt.Rows[0]["document_number"]);
                        //End of Rev Debashis Row:734
                        //Rev Debashis Row:747
                        omodel.shop_address_pincode = Convert.ToString(dt.Rows[0]["shop_address_pincode"]);
                        //End of Rev Debashis Row:747
                        omodel.quotation_product_details_list = QLview;
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
        public HttpResponseMessage SalesQuotationEdit(SalesQuotationEditInput model)
        {
            SalesQuotationEditOutput omodel = new SalesQuotationEditOutput();
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
                    List<ProductListEditInput> omedl2 = new List<ProductListEditInput>();
                    foreach (var s2 in model.product_list)
                    {
                        omedl2.Add(new ProductListEditInput()
                        {
                            product_id = s2.product_id,
                            color_id = s2.color_id,
                            rate_sqft = s2.rate_sqft,
                            rate_sqmtr = s2.rate_sqmtr,
                            qty = s2.qty,
                            amount = s2.amount
                        });
                    }

                    string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APISALESQUOTATIONADDUPDATEDELFETCH", sqlcon);
                    sqlcmd.Parameters.Add("@ACTION", "UPDATEDATA");
                    sqlcmd.Parameters.Add("@USER_ID", model.updated_by_user_id);
                    sqlcmd.Parameters.Add("@QUOTATIONSAVE_DATE", model.updated_date_time);
                    sqlcmd.Parameters.Add("@QUOTATION_NUMBER", model.quotation_number);
                    sqlcmd.Parameters.Add("@QUOTATION_DATE_SELECTION", model.quotation_date_selection);
                    sqlcmd.Parameters.Add("@PROJECT_NAME", model.project_name);
                    sqlcmd.Parameters.Add("@TAXES", model.taxes);
                    sqlcmd.Parameters.Add("@FREIGHT", model.Freight);
                    sqlcmd.Parameters.Add("@DELIVERY_TIME", model.delivery_time);
                    sqlcmd.Parameters.Add("@PAYMENT", model.payment);
                    sqlcmd.Parameters.Add("@VALIDITY", model.validity);
                    sqlcmd.Parameters.Add("@BILLING", model.billing);
                    sqlcmd.Parameters.Add("@PRODUCT_TOLERANCE_OF_THICKNESS", model.product_tolerance_of_thickness);
                    sqlcmd.Parameters.Add("@TOLERANCE_OF_COATING_THICKNESS", model.tolerance_of_coating_thickness);
                    sqlcmd.Parameters.Add("@SALESMAN_USER_ID", model.salesman_user_id);
                    sqlcmd.Parameters.Add("@shop_id", model.shop_id);
                    sqlcmd.Parameters.Add("@QUOTATION_CREATED_LAT", model.quotation_updated_lat);
                    sqlcmd.Parameters.Add("@QUOTATION_CREATED_LONG", model.quotation_updated_long);
                    sqlcmd.Parameters.Add("@QUOTATION_CREATED_ADDRESS", model.quotation_updated_address);
                    sqlcmd.Parameters.Add("@JsonXML", JsonXML);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Updated Successfully.";
                    }
                    else
                    {
                        omodel.status = "205";
                        omodel.message = "No Data found.";
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
        public HttpResponseMessage SalesQuotationDelete(SalesQuotationDeleteInput model)
        {
            SalesQuotationDeleteOutput omodel = new SalesQuotationDeleteOutput();
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
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APISALESQUOTATIONADDUPDATEDELFETCH", sqlcon);
                    sqlcmd.Parameters.Add("@ACTION", "DELETEDATA");
                    sqlcmd.Parameters.Add("@QUOTATION_NUMBER", model.quotation_number);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Deleted Successfully.";
                    }
                    else
                    {
                        omodel.status = "205";
                        omodel.message = "No Data found.";
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

        //Rev Debashis Row:735
        [HttpPost]
        public HttpResponseMessage SalesDocumentNoQuotationList(SalesDocumentNoQuotationListInput model)
        {
            SalesDocumentNoQuotationListOutput omodel = new SalesDocumentNoQuotationListOutput();
            List<DocumentNoQuotationProductDetailsList> QLview = new List<DocumentNoQuotationProductDetailsList>();

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
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APISALESQUOTATIONADDUPDATEDELFETCH", sqlcon);
                    sqlcmd.Parameters.Add("@ACTION", "QUOTATIONDOCUMENTNOLIST");
                    sqlcmd.Parameters.Add("@DOCUMENT_NUMBER", model.document_number);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Successfully Get List.";
                        QLview = APIHelperMethods.ToModelList<DocumentNoQuotationProductDetailsList>(dt);
                        omodel.quotation_number = Convert.ToString(dt.Rows[0]["quotation_number"]);
                        omodel.save_date_time = Convert.ToString(dt.Rows[0]["save_date_time"]);
                        omodel.quotation_date_selection = Convert.ToString(dt.Rows[0]["quotation_date_selection"]);
                        omodel.project_name = Convert.ToString(dt.Rows[0]["project_name"]);
                        omodel.taxes = Convert.ToString(dt.Rows[0]["taxes"]);
                        omodel.Freight = Convert.ToString(dt.Rows[0]["Freight"]);
                        omodel.delivery_time = Convert.ToString(dt.Rows[0]["delivery_time"]);
                        omodel.payment = Convert.ToString(dt.Rows[0]["payment"]);
                        omodel.validity = Convert.ToString(dt.Rows[0]["validity"]);
                        omodel.billing = Convert.ToString(dt.Rows[0]["billing"]);
                        omodel.product_tolerance_of_thickness = Convert.ToString(dt.Rows[0]["product_tolerance_of_thickness"]);
                        omodel.tolerance_of_coating_thickness = Convert.ToString(dt.Rows[0]["tolerance_of_coating_thickness"]);
                        omodel.salesman_user_id = Convert.ToString(dt.Rows[0]["salesman_user_id"]);
                        omodel.shop_id = Convert.ToString(dt.Rows[0]["shop_id"]);
                        omodel.shop_name = Convert.ToString(dt.Rows[0]["shop_name"]);
                        omodel.shop_phone_no = Convert.ToString(dt.Rows[0]["shop_phone_no"]);
                        omodel.quotation_created_lat = Convert.ToString(dt.Rows[0]["quotation_created_lat"]);
                        omodel.quotation_created_long = Convert.ToString(dt.Rows[0]["quotation_created_long"]);
                        omodel.quotation_created_address = Convert.ToString(dt.Rows[0]["quotation_created_address"]);
                        omodel.shop_addr = Convert.ToString(dt.Rows[0]["shop_addr"]);
                        omodel.shop_email = Convert.ToString(dt.Rows[0]["shop_email"]);
                        omodel.shop_owner_name = Convert.ToString(dt.Rows[0]["shop_owner_name"]);
                        omodel.salesman_name = Convert.ToString(dt.Rows[0]["salesman_name"]);
                        omodel.salesman_designation = Convert.ToString(dt.Rows[0]["salesman_designation"]);
                        omodel.salesman_login_id = Convert.ToString(dt.Rows[0]["salesman_login_id"]);
                        omodel.salesman_email = Convert.ToString(dt.Rows[0]["salesman_email"]);
                        omodel.salesman_phone_no = Convert.ToString(dt.Rows[0]["salesman_phone_no"]);
                        omodel.Remarks = Convert.ToString(dt.Rows[0]["Remarks"]);
                        omodel.document_number = Convert.ToString(dt.Rows[0]["document_number"]);
                        omodel.quotation_product_details_list = QLview;
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
        //End of Rev Debashis Row:735

        //Rev Debashis Row:737
        [HttpPost]
        public HttpResponseMessage SalesDocumentNoQuotationSave(SalesDocumentNoQuotationSaveInput model)
        {
            SalesDocumentNoQuotationSaveOutput omodel = new SalesDocumentNoQuotationSaveOutput();
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
                    List<DocumentNoProductListSaveInput> omedl2 = new List<DocumentNoProductListSaveInput>();
                    foreach (var s2 in model.product_list)
                    {
                        omedl2.Add(new DocumentNoProductListSaveInput()
                        {
                            product_id = s2.product_id,
                            color_id = s2.color_id,
                            rate_sqft = s2.rate_sqft,
                            rate_sqmtr = s2.rate_sqmtr,
                            qty = s2.qty,
                            amount = s2.amount
                        });
                    }

                    string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APISALESQUOTATIONADDUPDATEDELFETCH", sqlcon);
                    sqlcmd.Parameters.Add("@ACTION", "DOCUMENTNOSAVEDATA");
                    sqlcmd.Parameters.Add("@USER_ID", model.user_id);
                    sqlcmd.Parameters.Add("@QUOTATIONSAVE_DATE", model.save_date_time);
                    sqlcmd.Parameters.Add("@QUOTATION_NUMBER", model.quotation_number);
                    sqlcmd.Parameters.Add("@QUOTATION_DATE_SELECTION", model.quotation_date_selection);
                    sqlcmd.Parameters.Add("@PROJECT_NAME", model.project_name);
                    sqlcmd.Parameters.Add("@TAXES", model.taxes);
                    sqlcmd.Parameters.Add("@FREIGHT", model.Freight);
                    sqlcmd.Parameters.Add("@DELIVERY_TIME", model.delivery_time);
                    sqlcmd.Parameters.Add("@PAYMENT", model.payment);
                    sqlcmd.Parameters.Add("@VALIDITY", model.validity);
                    sqlcmd.Parameters.Add("@BILLING", model.billing);
                    sqlcmd.Parameters.Add("@PRODUCT_TOLERANCE_OF_THICKNESS", model.product_tolerance_of_thickness);
                    sqlcmd.Parameters.Add("@TOLERANCE_OF_COATING_THICKNESS", model.tolerance_of_coating_thickness);
                    sqlcmd.Parameters.Add("@SALESMAN_USER_ID", model.salesman_user_id);
                    sqlcmd.Parameters.Add("@shop_id", model.shop_id);
                    sqlcmd.Parameters.Add("@QUOTATION_CREATED_LAT", model.quotation_created_lat);
                    sqlcmd.Parameters.Add("@QUOTATION_CREATED_LONG", model.quotation_created_long);
                    sqlcmd.Parameters.Add("@QUOTATION_CREATED_ADDRESS", model.quotation_created_address);
                    sqlcmd.Parameters.Add("@REMARKS", model.Remarks);
                    sqlcmd.Parameters.Add("@DOCUMENT_NUMBER", model.document_number);
                    sqlcmd.Parameters.Add("@QUOTATION_STATUS", model.quotation_status);
                    sqlcmd.Parameters.Add("@JsonXML", JsonXML);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0 && Convert.ToString(dt.Rows[0]["STRMESSAGE"]) == "Unique")
                    {
                        omodel.status = "200";
                        omodel.message = "Saved successfully.";
                    }
                    else
                    {
                        omodel.status = "205";
                        omodel.message = "Duplicate Quotation Number.";
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
        //End of Rev Debashis Row:737
    }
}

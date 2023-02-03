#region======================================Revision History=========================================================
//1.0   V2.0.32     Debashis    01/09/2022      Some new parameters have been added.Row: 732 to 737
//2.0   V2.0.35     Debashis    14/10/2022      A new parameter has been added.Row: 747
//3.0   V2.0.37     Debashis    10/01/2023      Some new parameters have been added.Row: 790 to 791
//4.0   V2.0.37     Debashis    12/01/2023      Some new parameters have been added.Row: 792 to 793
//5.0   V2.0.37     Debashis    18/01/2023      Some new parameters have been added.Row: 802 to 803
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
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APISALESQUOTATIONADDUPDATEDELFETCH", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "SAVEDATA");
                    sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@QUOTATIONSAVE_DATE", model.save_date_time);
                    sqlcmd.Parameters.AddWithValue("@QUOTATION_NUMBER", model.quotation_number);
                    sqlcmd.Parameters.AddWithValue("@QUOTATION_DATE_SELECTION", model.quotation_date_selection);
                    sqlcmd.Parameters.AddWithValue("@PROJECT_NAME", model.project_name);
                    sqlcmd.Parameters.AddWithValue("@TAXES", model.taxes);
                    sqlcmd.Parameters.AddWithValue("@FREIGHT", model.Freight);
                    sqlcmd.Parameters.AddWithValue("@DELIVERY_TIME", model.delivery_time);
                    sqlcmd.Parameters.AddWithValue("@PAYMENT", model.payment);
                    sqlcmd.Parameters.AddWithValue("@VALIDITY", model.validity);
                    sqlcmd.Parameters.AddWithValue("@BILLING", model.billing);
                    sqlcmd.Parameters.AddWithValue("@PRODUCT_TOLERANCE_OF_THICKNESS", model.product_tolerance_of_thickness);
                    sqlcmd.Parameters.AddWithValue("@TOLERANCE_OF_COATING_THICKNESS", model.tolerance_of_coating_thickness);
                    sqlcmd.Parameters.AddWithValue("@SALESMAN_USER_ID", model.salesman_user_id);
                    sqlcmd.Parameters.AddWithValue("@shop_id", model.shop_id);
                    sqlcmd.Parameters.AddWithValue("@QUOTATION_CREATED_LAT", model.quotation_created_lat);
                    sqlcmd.Parameters.AddWithValue("@QUOTATION_CREATED_LONG", model.quotation_created_long);
                    sqlcmd.Parameters.AddWithValue("@QUOTATION_CREATED_ADDRESS", model.quotation_created_address);
                    //Rev 1.0 Row:732
                    sqlcmd.Parameters.AddWithValue("@REMARKS", model.Remarks);
                    sqlcmd.Parameters.AddWithValue("@DOCUMENT_NUMBER", model.document_number);
                    sqlcmd.Parameters.AddWithValue("@QUOTATION_STATUS", model.quotation_status);
                    //End of Rev 1.0 Row:732
                    //Rev 3.0 Row:791
                    sqlcmd.Parameters.AddWithValue("@SEL_QUOTATION_PDF_TEMPLATE", model.sel_quotation_pdf_template);
                    //sqlcmd.Parameters.AddWithValue("@QUOTATION_CONTACT_PERSON", model.quotation_contact_person);
                    //sqlcmd.Parameters.AddWithValue("@QUOTATION_CONTACT_NUMBER", model.quotation_contact_number);
                    //sqlcmd.Parameters.AddWithValue("@QUOTATION_CONTACT_EMAIL", model.quotation_contact_email);
                    //sqlcmd.Parameters.AddWithValue("@QUOTATION_CONTACT_DOA", model.quotation_contact_doa);
                    //End of Rev 3.0 Row:791
                    sqlcmd.Parameters.AddWithValue("@JsonXML", JsonXML);

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
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APISALESQUOTATIONADDUPDATEDELFETCH", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "SHOPWISEQUOTATIONLIST");
                    sqlcmd.Parameters.AddWithValue("@SHOP_ID", model.shop_id);

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
            //Rev 5.0 Row:803
            List<QuotationExtraContactDetailsList> Exview = new List<QuotationExtraContactDetailsList>();
            //End of Rev 5.0 Row:803

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
                    //Rev 5.0 Row:803
                    //DataTable dt = new DataTable();
                    DataSet ds = new DataSet();
                    //End of Rev 5.0 Row:803
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APISALESQUOTATIONADDUPDATEDELFETCH", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "SHOWQUOTATIONDETAILS");
                    sqlcmd.Parameters.AddWithValue("@QUOTATION_NUMBER", model.quotation_number);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    //Rev 5.0 Row:803
                    //da.Fill(dt);
                    da.Fill(ds);
                    //End of Rev 5.0 Row:803
                    sqlcon.Close();
                    //Rev 5.0 Row:803
                    //if (dt.Rows.Count > 0)
                    //{
                    //    omodel.status = "200";
                    //    omodel.message = "Successfully Get List.";
                    //    QLview = APIHelperMethods.ToModelList<QuotationProductDetailsList>(dt);
                    //    omodel.quotation_number = Convert.ToString(dt.Rows[0]["quotation_number"]);
                    //    omodel.save_date_time = Convert.ToString(dt.Rows[0]["save_date_time"]);
                    //    omodel.quotation_date_selection = Convert.ToString(dt.Rows[0]["quotation_date_selection"]);
                    //    omodel.project_name = Convert.ToString(dt.Rows[0]["project_name"]);
                    //    omodel.taxes = Convert.ToString(dt.Rows[0]["taxes"]);
                    //    omodel.Freight = Convert.ToString(dt.Rows[0]["Freight"]);
                    //    omodel.delivery_time = Convert.ToString(dt.Rows[0]["delivery_time"]);
                    //    omodel.payment = Convert.ToString(dt.Rows[0]["payment"]);
                    //    omodel.validity = Convert.ToString(dt.Rows[0]["validity"]);
                    //    omodel.billing = Convert.ToString(dt.Rows[0]["billing"]);
                    //    omodel.product_tolerance_of_thickness = Convert.ToString(dt.Rows[0]["product_tolerance_of_thickness"]);
                    //    omodel.tolerance_of_coating_thickness = Convert.ToString(dt.Rows[0]["tolerance_of_coating_thickness"]);
                    //    omodel.salesman_user_id = Convert.ToString(dt.Rows[0]["salesman_user_id"]);
                    //    omodel.shop_id = Convert.ToString(dt.Rows[0]["shop_id"]);
                    //    omodel.shop_name = Convert.ToString(dt.Rows[0]["shop_name"]);
                    //    omodel.shop_phone_no = Convert.ToString(dt.Rows[0]["shop_phone_no"]);
                    //    omodel.quotation_created_lat = Convert.ToString(dt.Rows[0]["quotation_created_lat"]);
                    //    omodel.quotation_created_long = Convert.ToString(dt.Rows[0]["quotation_created_long"]);
                    //    omodel.quotation_created_address = Convert.ToString(dt.Rows[0]["quotation_created_address"]);
                    //    omodel.shop_addr = Convert.ToString(dt.Rows[0]["shop_addr"]);
                    //    omodel.shop_email = Convert.ToString(dt.Rows[0]["shop_email"]);
                    //    omodel.shop_owner_name = Convert.ToString(dt.Rows[0]["shop_owner_name"]);
                    //    omodel.salesman_name = Convert.ToString(dt.Rows[0]["salesman_name"]);
                    //    omodel.salesman_designation = Convert.ToString(dt.Rows[0]["salesman_designation"]);
                    //    omodel.salesman_login_id = Convert.ToString(dt.Rows[0]["salesman_login_id"]);
                    //    omodel.salesman_email = Convert.ToString(dt.Rows[0]["salesman_email"]);
                    //    omodel.salesman_phone_no = Convert.ToString(dt.Rows[0]["salesman_phone_no"]);
                    //    //Rev 1.0 Row:734
                    //    omodel.Remarks = Convert.ToString(dt.Rows[0]["Remarks"]);
                    //    omodel.document_number = Convert.ToString(dt.Rows[0]["document_number"]);
                    //    //End of Rev 1.0 Row:734
                    //    //Rev 2.0 Row:747
                    //    omodel.shop_address_pincode = Convert.ToString(dt.Rows[0]["shop_address_pincode"]);
                    //    //End of Rev 2.0 Row:747
                    //    //Rev 3.0 Row:790
                    //    omodel.sel_quotation_pdf_template = Convert.ToString(dt.Rows[0]["sel_quotation_pdf_template"]);
                    //    //omodel.quotation_contact_person = Convert.ToString(dt.Rows[0]["quotation_contact_person"]);
                    //    //omodel.quotation_contact_number = Convert.ToString(dt.Rows[0]["quotation_contact_number"]);
                    //    //omodel.quotation_contact_email = Convert.ToString(dt.Rows[0]["quotation_contact_email"]);
                    //    //omodel.quotation_contact_doa = Convert.ToString(dt.Rows[0]["quotation_contact_doa"]);
                    //    //End of Rev 3.0 Row:790
                    //    omodel.quotation_product_details_list = QLview;
                    //}
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Successfully Get List.";
                        QLview = APIHelperMethods.ToModelList<QuotationProductDetailsList>(ds.Tables[0]);
                        Exview = APIHelperMethods.ToModelList<QuotationExtraContactDetailsList>(ds.Tables[1]);
                        omodel.quotation_number = Convert.ToString(ds.Tables[0].Rows[0]["quotation_number"]);
                        omodel.save_date_time = Convert.ToString(ds.Tables[0].Rows[0]["save_date_time"]);
                        omodel.quotation_date_selection = Convert.ToString(ds.Tables[0].Rows[0]["quotation_date_selection"]);
                        omodel.project_name = Convert.ToString(ds.Tables[0].Rows[0]["project_name"]);
                        omodel.taxes = Convert.ToString(ds.Tables[0].Rows[0]["taxes"]);
                        omodel.Freight = Convert.ToString(ds.Tables[0].Rows[0]["Freight"]);
                        omodel.delivery_time = Convert.ToString(ds.Tables[0].Rows[0]["delivery_time"]);
                        omodel.payment = Convert.ToString(ds.Tables[0].Rows[0]["payment"]);
                        omodel.validity = Convert.ToString(ds.Tables[0].Rows[0]["validity"]);
                        omodel.billing = Convert.ToString(ds.Tables[0].Rows[0]["billing"]);
                        omodel.product_tolerance_of_thickness = Convert.ToString(ds.Tables[0].Rows[0]["product_tolerance_of_thickness"]);
                        omodel.tolerance_of_coating_thickness = Convert.ToString(ds.Tables[0].Rows[0]["tolerance_of_coating_thickness"]);
                        omodel.salesman_user_id = Convert.ToString(ds.Tables[0].Rows[0]["salesman_user_id"]);
                        omodel.shop_id = Convert.ToString(ds.Tables[0].Rows[0]["shop_id"]);
                        omodel.shop_name = Convert.ToString(ds.Tables[0].Rows[0]["shop_name"]);
                        omodel.shop_phone_no = Convert.ToString(ds.Tables[0].Rows[0]["shop_phone_no"]);
                        omodel.quotation_created_lat = Convert.ToString(ds.Tables[0].Rows[0]["quotation_created_lat"]);
                        omodel.quotation_created_long = Convert.ToString(ds.Tables[0].Rows[0]["quotation_created_long"]);
                        omodel.quotation_created_address = Convert.ToString(ds.Tables[0].Rows[0]["quotation_created_address"]);
                        omodel.shop_addr = Convert.ToString(ds.Tables[0].Rows[0]["shop_addr"]);
                        omodel.shop_email = Convert.ToString(ds.Tables[0].Rows[0]["shop_email"]);
                        omodel.shop_owner_name = Convert.ToString(ds.Tables[0].Rows[0]["shop_owner_name"]);
                        omodel.salesman_name = Convert.ToString(ds.Tables[0].Rows[0]["salesman_name"]);
                        omodel.salesman_designation = Convert.ToString(ds.Tables[0].Rows[0]["salesman_designation"]);
                        omodel.salesman_login_id = Convert.ToString(ds.Tables[0].Rows[0]["salesman_login_id"]);
                        omodel.salesman_email = Convert.ToString(ds.Tables[0].Rows[0]["salesman_email"]);
                        omodel.salesman_phone_no = Convert.ToString(ds.Tables[0].Rows[0]["salesman_phone_no"]);
                        omodel.Remarks = Convert.ToString(ds.Tables[0].Rows[0]["Remarks"]);
                        omodel.document_number = Convert.ToString(ds.Tables[0].Rows[0]["document_number"]);
                        omodel.shop_address_pincode = Convert.ToString(ds.Tables[0].Rows[0]["shop_address_pincode"]);
                        omodel.sel_quotation_pdf_template = Convert.ToString(ds.Tables[0].Rows[0]["sel_quotation_pdf_template"]);
                        omodel.quotation_product_details_list = QLview;
                        omodel.extra_contact_list = Exview;
                    }
                    //End of Rev 5.0 Row:803
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
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APISALESQUOTATIONADDUPDATEDELFETCH", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "UPDATEDATA");
                    sqlcmd.Parameters.AddWithValue("@USER_ID", model.updated_by_user_id);
                    sqlcmd.Parameters.AddWithValue("@QUOTATIONSAVE_DATE", model.updated_date_time);
                    sqlcmd.Parameters.AddWithValue("@QUOTATION_NUMBER", model.quotation_number);
                    sqlcmd.Parameters.AddWithValue("@QUOTATION_DATE_SELECTION", model.quotation_date_selection);
                    sqlcmd.Parameters.AddWithValue("@PROJECT_NAME", model.project_name);
                    sqlcmd.Parameters.AddWithValue("@TAXES", model.taxes);
                    sqlcmd.Parameters.AddWithValue("@FREIGHT", model.Freight);
                    sqlcmd.Parameters.AddWithValue("@DELIVERY_TIME", model.delivery_time);
                    sqlcmd.Parameters.AddWithValue("@PAYMENT", model.payment);
                    sqlcmd.Parameters.AddWithValue("@VALIDITY", model.validity);
                    sqlcmd.Parameters.AddWithValue("@BILLING", model.billing);
                    sqlcmd.Parameters.AddWithValue("@PRODUCT_TOLERANCE_OF_THICKNESS", model.product_tolerance_of_thickness);
                    sqlcmd.Parameters.AddWithValue("@TOLERANCE_OF_COATING_THICKNESS", model.tolerance_of_coating_thickness);
                    sqlcmd.Parameters.AddWithValue("@SALESMAN_USER_ID", model.salesman_user_id);
                    sqlcmd.Parameters.AddWithValue("@shop_id", model.shop_id);
                    sqlcmd.Parameters.AddWithValue("@QUOTATION_CREATED_LAT", model.quotation_updated_lat);
                    sqlcmd.Parameters.AddWithValue("@QUOTATION_CREATED_LONG", model.quotation_updated_long);
                    sqlcmd.Parameters.AddWithValue("@QUOTATION_CREATED_ADDRESS", model.quotation_updated_address);
                    sqlcmd.Parameters.AddWithValue("@JsonXML", JsonXML);

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
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APISALESQUOTATIONADDUPDATEDELFETCH", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "DELETEDATA");
                    sqlcmd.Parameters.AddWithValue("@QUOTATION_NUMBER", model.quotation_number);

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

        //Rev 1.0 Row:735
        [HttpPost]
        public HttpResponseMessage SalesDocumentNoQuotationList(SalesDocumentNoQuotationListInput model)
        {
            SalesDocumentNoQuotationListOutput omodel = new SalesDocumentNoQuotationListOutput();
            List<DocumentNoQuotationProductDetailsList> QLview = new List<DocumentNoQuotationProductDetailsList>();
            //Rev 5.0 Row:804
            List<DocumentNoQuotationExtraContactDetailsList> Exview = new List<DocumentNoQuotationExtraContactDetailsList>();
            //End of Rev 5.0 Row:804
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
                    //Rev 5.0 Row:804
                    //DataTable dt = new DataTable();
                    DataSet ds = new DataSet();
                    //End of Rev 5.0 Row:804
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APISALESQUOTATIONADDUPDATEDELFETCH", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "QUOTATIONDOCUMENTNOLIST");
                    sqlcmd.Parameters.AddWithValue("@DOCUMENT_NUMBER", model.document_number);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    //Rev 5.0 Row:804
                    //da.Fill(dt);
                    da.Fill(ds);
                    //End of Rev 5.0 Row:804
                    sqlcon.Close();
                    //Rev 5.0 Row:804
                    //if (dt.Rows.Count > 0)
                    //{
                    //    omodel.status = "200";
                    //    omodel.message = "Successfully Get List.";
                    //    QLview = APIHelperMethods.ToModelList<DocumentNoQuotationProductDetailsList>(dt);
                    //    omodel.quotation_number = Convert.ToString(dt.Rows[0]["quotation_number"]);
                    //    omodel.save_date_time = Convert.ToString(dt.Rows[0]["save_date_time"]);
                    //    omodel.quotation_date_selection = Convert.ToString(dt.Rows[0]["quotation_date_selection"]);
                    //    omodel.project_name = Convert.ToString(dt.Rows[0]["project_name"]);
                    //    omodel.taxes = Convert.ToString(dt.Rows[0]["taxes"]);
                    //    omodel.Freight = Convert.ToString(dt.Rows[0]["Freight"]);
                    //    omodel.delivery_time = Convert.ToString(dt.Rows[0]["delivery_time"]);
                    //    omodel.payment = Convert.ToString(dt.Rows[0]["payment"]);
                    //    omodel.validity = Convert.ToString(dt.Rows[0]["validity"]);
                    //    omodel.billing = Convert.ToString(dt.Rows[0]["billing"]);
                    //    omodel.product_tolerance_of_thickness = Convert.ToString(dt.Rows[0]["product_tolerance_of_thickness"]);
                    //    omodel.tolerance_of_coating_thickness = Convert.ToString(dt.Rows[0]["tolerance_of_coating_thickness"]);
                    //    omodel.salesman_user_id = Convert.ToString(dt.Rows[0]["salesman_user_id"]);
                    //    omodel.shop_id = Convert.ToString(dt.Rows[0]["shop_id"]);
                    //    omodel.shop_name = Convert.ToString(dt.Rows[0]["shop_name"]);
                    //    omodel.shop_phone_no = Convert.ToString(dt.Rows[0]["shop_phone_no"]);
                    //    omodel.quotation_created_lat = Convert.ToString(dt.Rows[0]["quotation_created_lat"]);
                    //    omodel.quotation_created_long = Convert.ToString(dt.Rows[0]["quotation_created_long"]);
                    //    omodel.quotation_created_address = Convert.ToString(dt.Rows[0]["quotation_created_address"]);
                    //    omodel.shop_addr = Convert.ToString(dt.Rows[0]["shop_addr"]);
                    //    omodel.shop_email = Convert.ToString(dt.Rows[0]["shop_email"]);
                    //    omodel.shop_owner_name = Convert.ToString(dt.Rows[0]["shop_owner_name"]);
                    //    omodel.salesman_name = Convert.ToString(dt.Rows[0]["salesman_name"]);
                    //    omodel.salesman_designation = Convert.ToString(dt.Rows[0]["salesman_designation"]);
                    //    omodel.salesman_login_id = Convert.ToString(dt.Rows[0]["salesman_login_id"]);
                    //    omodel.salesman_email = Convert.ToString(dt.Rows[0]["salesman_email"]);
                    //    omodel.salesman_phone_no = Convert.ToString(dt.Rows[0]["salesman_phone_no"]);
                    //    omodel.Remarks = Convert.ToString(dt.Rows[0]["Remarks"]);
                    //    omodel.document_number = Convert.ToString(dt.Rows[0]["document_number"]);
                    //    //Rev 4.0 Row:792
                    //    omodel.sel_quotation_pdf_template = Convert.ToString(dt.Rows[0]["sel_quotation_pdf_template"]);
                    //    //omodel.quotation_contact_person = Convert.ToString(dt.Rows[0]["quotation_contact_person"]);
                    //    //omodel.quotation_contact_number = Convert.ToString(dt.Rows[0]["quotation_contact_number"]);
                    //    //omodel.quotation_contact_email = Convert.ToString(dt.Rows[0]["quotation_contact_email"]);
                    //    //omodel.quotation_contact_doa = Convert.ToString(dt.Rows[0]["quotation_contact_doa"]);
                    //    //End of Rev 4.0 Row:792
                    //    omodel.quotation_product_details_list = QLview;
                    //}
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Successfully Get List.";
                        QLview = APIHelperMethods.ToModelList<DocumentNoQuotationProductDetailsList>(ds.Tables[0]);
                        Exview = APIHelperMethods.ToModelList<DocumentNoQuotationExtraContactDetailsList>(ds.Tables[1]);
                        omodel.quotation_number = Convert.ToString(ds.Tables[0].Rows[0]["quotation_number"]);
                        omodel.save_date_time = Convert.ToString(ds.Tables[0].Rows[0]["save_date_time"]);
                        omodel.quotation_date_selection = Convert.ToString(ds.Tables[0].Rows[0]["quotation_date_selection"]);
                        omodel.project_name = Convert.ToString(ds.Tables[0].Rows[0]["project_name"]);
                        omodel.taxes = Convert.ToString(ds.Tables[0].Rows[0]["taxes"]);
                        omodel.Freight = Convert.ToString(ds.Tables[0].Rows[0]["Freight"]);
                        omodel.delivery_time = Convert.ToString(ds.Tables[0].Rows[0]["delivery_time"]);
                        omodel.payment = Convert.ToString(ds.Tables[0].Rows[0]["payment"]);
                        omodel.validity = Convert.ToString(ds.Tables[0].Rows[0]["validity"]);
                        omodel.billing = Convert.ToString(ds.Tables[0].Rows[0]["billing"]);
                        omodel.product_tolerance_of_thickness = Convert.ToString(ds.Tables[0].Rows[0]["product_tolerance_of_thickness"]);
                        omodel.tolerance_of_coating_thickness = Convert.ToString(ds.Tables[0].Rows[0]["tolerance_of_coating_thickness"]);
                        omodel.salesman_user_id = Convert.ToString(ds.Tables[0].Rows[0]["salesman_user_id"]);
                        omodel.shop_id = Convert.ToString(ds.Tables[0].Rows[0]["shop_id"]);
                        omodel.shop_name = Convert.ToString(ds.Tables[0].Rows[0]["shop_name"]);
                        omodel.shop_phone_no = Convert.ToString(ds.Tables[0].Rows[0]["shop_phone_no"]);
                        omodel.quotation_created_lat = Convert.ToString(ds.Tables[0].Rows[0]["quotation_created_lat"]);
                        omodel.quotation_created_long = Convert.ToString(ds.Tables[0].Rows[0]["quotation_created_long"]);
                        omodel.quotation_created_address = Convert.ToString(ds.Tables[0].Rows[0]["quotation_created_address"]);
                        omodel.shop_addr = Convert.ToString(ds.Tables[0].Rows[0]["shop_addr"]);
                        omodel.shop_email = Convert.ToString(ds.Tables[0].Rows[0]["shop_email"]);
                        omodel.shop_owner_name = Convert.ToString(ds.Tables[0].Rows[0]["shop_owner_name"]);
                        omodel.salesman_name = Convert.ToString(ds.Tables[0].Rows[0]["salesman_name"]);
                        omodel.salesman_designation = Convert.ToString(ds.Tables[0].Rows[0]["salesman_designation"]);
                        omodel.salesman_login_id = Convert.ToString(ds.Tables[0].Rows[0]["salesman_login_id"]);
                        omodel.salesman_email = Convert.ToString(ds.Tables[0].Rows[0]["salesman_email"]);
                        omodel.salesman_phone_no = Convert.ToString(ds.Tables[0].Rows[0]["salesman_phone_no"]);
                        omodel.Remarks = Convert.ToString(ds.Tables[0].Rows[0]["Remarks"]);
                        omodel.document_number = Convert.ToString(ds.Tables[0].Rows[0]["document_number"]);
                        omodel.sel_quotation_pdf_template = Convert.ToString(ds.Tables[0].Rows[0]["sel_quotation_pdf_template"]);
                        omodel.quotation_product_details_list = QLview;
                        omodel.extra_contact_list = Exview;
                    }
                    //End of Rev 5.0 Row:804
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
        //End of Rev 1.0 Row:735

        //Rev 1.0 Row:737
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

                    //Rev 5.0 Row:802
                    List<DocumentNoExtraContactListSaveInput> omedl3 = new List<DocumentNoExtraContactListSaveInput>();
                    foreach (var s2 in model.extra_contact_list)
                    {
                        omedl3.Add(new DocumentNoExtraContactListSaveInput()
                        {
                            quotation_contact_person = s2.quotation_contact_person,
                            quotation_contact_number = s2.quotation_contact_number,
                            quotation_contact_email = s2.quotation_contact_email,
                            quotation_contact_doa = s2.quotation_contact_doa,
                            quotation_contact_dob = s2.quotation_contact_dob
                        });
                    }

                    string JsonXML1 = XmlConversion.ConvertToXml(omedl3, 0);
                    //End of Rev 5.0 Row:802

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APISALESQUOTATIONADDUPDATEDELFETCH", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "DOCUMENTNOSAVEDATA");
                    sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@QUOTATIONSAVE_DATE", model.save_date_time);
                    sqlcmd.Parameters.AddWithValue("@QUOTATION_NUMBER", model.quotation_number);
                    sqlcmd.Parameters.AddWithValue("@QUOTATION_DATE_SELECTION", model.quotation_date_selection);
                    sqlcmd.Parameters.AddWithValue("@PROJECT_NAME", model.project_name);
                    sqlcmd.Parameters.AddWithValue("@TAXES", model.taxes);
                    sqlcmd.Parameters.AddWithValue("@FREIGHT", model.Freight);
                    sqlcmd.Parameters.AddWithValue("@DELIVERY_TIME", model.delivery_time);
                    sqlcmd.Parameters.AddWithValue("@PAYMENT", model.payment);
                    sqlcmd.Parameters.AddWithValue("@VALIDITY", model.validity);
                    sqlcmd.Parameters.AddWithValue("@BILLING", model.billing);
                    sqlcmd.Parameters.AddWithValue("@PRODUCT_TOLERANCE_OF_THICKNESS", model.product_tolerance_of_thickness);
                    sqlcmd.Parameters.AddWithValue("@TOLERANCE_OF_COATING_THICKNESS", model.tolerance_of_coating_thickness);
                    sqlcmd.Parameters.AddWithValue("@SALESMAN_USER_ID", model.salesman_user_id);
                    sqlcmd.Parameters.AddWithValue("@shop_id", model.shop_id);
                    sqlcmd.Parameters.AddWithValue("@QUOTATION_CREATED_LAT", model.quotation_created_lat);
                    sqlcmd.Parameters.AddWithValue("@QUOTATION_CREATED_LONG", model.quotation_created_long);
                    sqlcmd.Parameters.AddWithValue("@QUOTATION_CREATED_ADDRESS", model.quotation_created_address);
                    sqlcmd.Parameters.AddWithValue("@REMARKS", model.Remarks);
                    sqlcmd.Parameters.AddWithValue("@DOCUMENT_NUMBER", model.document_number);
                    sqlcmd.Parameters.AddWithValue("@QUOTATION_STATUS", model.quotation_status);
                    //Rev 4.0 Row:793
                    sqlcmd.Parameters.AddWithValue("@SEL_QUOTATION_PDF_TEMPLATE", model.sel_quotation_pdf_template);
                    //sqlcmd.Parameters.AddWithValue("@QUOTATION_CONTACT_PERSON", model.quotation_contact_person);
                    //sqlcmd.Parameters.AddWithValue("@QUOTATION_CONTACT_NUMBER", model.quotation_contact_number);
                    //sqlcmd.Parameters.AddWithValue("@QUOTATION_CONTACT_EMAIL", model.quotation_contact_email);
                    //sqlcmd.Parameters.AddWithValue("@QUOTATION_CONTACT_DOA", model.quotation_contact_doa);
                    //End of Rev 4.0 Row:793
                    sqlcmd.Parameters.AddWithValue("@JsonXML", JsonXML);
                    //Rev 5.0 Row:802
                    sqlcmd.Parameters.AddWithValue("@JsonXML1", JsonXML1);
                    //End of Rev 5.0 Row:802

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
        //End of Rev 1.0 Row:737
    }
}

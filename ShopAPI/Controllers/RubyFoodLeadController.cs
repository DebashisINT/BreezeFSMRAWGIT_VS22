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
    public class RubyFoodLeadController : ApiController
    {
        public HttpResponseMessage ProspectList(RubyFoodProspectListInput model)
        {
            RubyFoodProspectListOutput omodel = new RubyFoodProspectListOutput();
            List<ProspectList> oview = new List<ProspectList>();

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
                sqlcmd = new SqlCommand("PRC_FSMAPIRUBYFOODCUSTOMIZATION", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "PROSPECTLIST");
                sqlcmd.Parameters.Add("@USER_ID", model.user_id);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<ProspectList>(dt);
                    omodel.status = "200";
                    omodel.message = "Successfully get Prospect list.";
                    omodel.Prospect_list = oview;
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

        public HttpResponseMessage QuestionList(RubyFoodQuestionListInput model)
        {
            RubyFoodQuestionListOutput omodel = new RubyFoodQuestionListOutput();
            List<QuestionList> oview = new List<QuestionList>();

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
                sqlcmd = new SqlCommand("PRC_FSMAPIRUBYFOODCUSTOMIZATION", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "QUESTIONLIST");
                sqlcmd.Parameters.Add("@USER_ID", model.user_id);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<QuestionList>(dt);
                    omodel.status = "200";
                    omodel.message = "Successfully get Question list.";
                    omodel.Question_list = oview;
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
        public HttpResponseMessage QuestionListSave(RubyFoodQuestionListSaveInput model)
        {
            RubyFoodQuestionListSaveOutput omodel = new RubyFoodQuestionListSaveOutput();
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
                    List<QuestionListInput> omedl2 = new List<QuestionListInput>();
                    foreach (var s2 in model.Question_list)
                    {
                        omedl2.Add(new QuestionListInput()
                        {
                            question_id = s2.question_id,
                            answer = s2.answer
                        });
                    }

                    string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_FSMAPIRUBYFOODCUSTOMIZATION", sqlcon);
                    sqlcmd.Parameters.Add("@ACTION", "INSERTDATA");
                    sqlcmd.Parameters.Add("@USER_ID", model.user_id);
                    sqlcmd.Parameters.Add("@SHOP_ID", model.shop_id);
                    sqlcmd.Parameters.Add("@JsonXML", JsonXML);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Successfully Question Saved.";
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

        public HttpResponseMessage QuestionListEdit(RubyFoodQuestionListSaveInput model)
        {
            RubyFoodQuestionListSaveOutput omodel = new RubyFoodQuestionListSaveOutput();
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
                    List<QuestionListInput> omedl2 = new List<QuestionListInput>();
                    foreach (var s2 in model.Question_list)
                    {
                        omedl2.Add(new QuestionListInput()
                        {
                            question_id = s2.question_id,
                            answer = s2.answer
                        });
                    }

                    string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_FSMAPIRUBYFOODCUSTOMIZATION", sqlcon);
                    sqlcmd.Parameters.Add("@ACTION", "UPDATEDATA");
                    sqlcmd.Parameters.Add("@USER_ID", model.user_id);
                    sqlcmd.Parameters.Add("@SHOP_ID", model.shop_id);
                    sqlcmd.Parameters.Add("@JsonXML", JsonXML);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Update Successfully.";
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

        public HttpResponseMessage QuestionAnswerList(RubyFoodQuestionListInput model)
        {
            RubyFoodQuestionAnswerListOutput omodel = new RubyFoodQuestionAnswerListOutput();
            List<QuestionAnswerList> oview = new List<QuestionAnswerList>();

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
                sqlcmd = new SqlCommand("PRC_FSMAPIRUBYFOODCUSTOMIZATION", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "QUESTIONANSWERLIST");
                sqlcmd.Parameters.Add("@USER_ID", model.user_id);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<QuestionAnswerList>(dt);
                    omodel.status = "200";
                    omodel.message = "Successfully get Question Answer list.";
                    omodel.Question_list = oview;
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

        public HttpResponseMessage LeadTypeImageLink(RubyFoodImageLinkInput model)
        {
            RubyFoodImageLinkOutput omodel = new RubyFoodImageLinkOutput();
            String APIHostingPort = System.Configuration.ConfigurationSettings.AppSettings["APIHostingPort"];
            List<LeadShopImageList> oview = new List<LeadShopImageList>();
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
                sqlcmd = new SqlCommand("PRC_FSMAPIRUBYFOODCUSTOMIZATION", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "LEADTYPEIMAGELINK");
                sqlcmd.Parameters.Add("@USER_ID", model.user_id);
                sqlcmd.Parameters.Add("@BaseURL", APIHostingPort);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<LeadShopImageList>(dt);
                    omodel.user_id = model.user_id;
                    omodel.lead_shop_list = oview;
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

        public HttpResponseMessage OrderReturnSave(RubyFoodOrderReturnInput model)
        {
            RubyFoodOrderReturnOutput omodel = new RubyFoodOrderReturnOutput();
            //List<OrderReturnOutDataList> ORview = new List<OrderReturnOutDataList>();
            OrderReturnOutDataList ORview = new OrderReturnOutDataList();
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
                    List<OrderReturnList> omedl2 = new List<OrderReturnList>();
                    foreach (var s2 in model.return_list)
                    {
                        omedl2.Add(new OrderReturnList()
                        {
                            id = s2.id,
                            product_name = s2.product_name,
                            qty = s2.qty,
                            rate = s2.rate,
                            total_price=s2.total_price
                        });
                    }

                    string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_FSMAPIRUBYFOODCUSTOMIZATION", sqlcon);
                    sqlcmd.Parameters.Add("@ACTION", "ORDERRETURNINSERT");
                    sqlcmd.Parameters.Add("@session_token", model.session_token);
                    sqlcmd.Parameters.Add("@user_id", model.user_id);
                    sqlcmd.Parameters.Add("@RETURN_AMOUNT", model.return_amount);
                    sqlcmd.Parameters.Add("@SHOP_ID", model.shop_id);
                    sqlcmd.Parameters.Add("@RETURN_ID", model.return_id);
                    sqlcmd.Parameters.Add("@DESCRIPTION", model.description);
                    sqlcmd.Parameters.Add("@RETURN_DATE_TIME", model.return_date_time);
                    sqlcmd.Parameters.Add("@LATITUDE", model.latitude);
                    sqlcmd.Parameters.Add("@LONGITUDE", model.longitude);
                    sqlcmd.Parameters.Add("@ADDRESS", model.address);
                    sqlcmd.Parameters.Add("@JsonXML", JsonXML);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0)
                    {
                        ORview.status = "200";
                        ORview.message = "Added Successfully.";
                        //ORview = APIHelperMethods.ToModelList<OrderReturnOutDataList>(dt);
                        //omodel.data = ORview;
                        ORview.session_token = Convert.ToString(dt.Rows[0]["session_token"]);
                        ORview.user_id = Convert.ToString(dt.Rows[0]["user_id"]);
                        ORview.return_id = Convert.ToString(dt.Rows[0]["return_id"]);
                        ORview.return_amount = Convert.ToDecimal(dt.Rows[0]["return_amount"]);
                        ORview.description = Convert.ToString(dt.Rows[0]["description"]);
                    }
                    else
                    {
                        ORview.status = "202";
                        ORview.message = "No entry.";
                    }
                    var message = Request.CreateResponse(HttpStatusCode.OK, ORview);
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

        public HttpResponseMessage OrderReturnDetailsList(OrderReturnFetchInput model)
        {
            OrderReturnFetchOutput odata = new OrderReturnFetchOutput();

            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
            }
            else
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];

                DataSet ds = new DataSet();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_FSMAPIRUBYFOODCUSTOMIZATION", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "ORDERRETURNWITHPRODUCTLIST");
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@RETURN_DATE_TIME", model.date);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(ds);
                sqlcon.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    odata.total_returnlist_count = Convert.ToString(ds.Tables[0].Rows[0]["total_returnlist_count"]);
                    List<OrderReturnFetchdetailsList> oview = new List<OrderReturnFetchdetailsList>();
                    //OrderfetchdetailsList orderdetailprd = new OrderfetchdetailsList();
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        List<OrderReturnProducts> delivermodelproduct = new List<OrderReturnProducts>();
                        for (int j = 0; j < ds.Tables[2].Rows.Count; j++)
                        {
                            //int i1 = 0;
                            if (Convert.ToString(ds.Tables[2].Rows[j]["return_id"]) == Convert.ToString(ds.Tables[1].Rows[i]["return_id"]))
                            {
                                delivermodelproduct.Add(new OrderReturnProducts()
                                {
                                    id = Convert.ToInt32(ds.Tables[2].Rows[j]["id"]),
                                    brand_id = Convert.ToInt32(ds.Tables[2].Rows[j]["brand_id"]),
                                    category_id = Convert.ToInt32(ds.Tables[2].Rows[j]["category_id"]),
                                    watt_id = Convert.ToInt32(ds.Tables[2].Rows[j]["watt_id"]),
                                    brand = Convert.ToString(ds.Tables[2].Rows[j]["brand"]),
                                    category = Convert.ToString(ds.Tables[2].Rows[j]["category"]),
                                    watt = Convert.ToString(ds.Tables[2].Rows[j]["watt"]),
                                    product_name = Convert.ToString(ds.Tables[2].Rows[j]["product_name"]),
                                    qty = Convert.ToDecimal(ds.Tables[2].Rows[j]["qty"]),
                                    rate = Convert.ToDecimal(ds.Tables[2].Rows[j]["rate"]),
                                    total_price = Convert.ToDecimal(ds.Tables[2].Rows[j]["total_price"])                                    
                                });
                            }
                        }
                        oview.Add(new OrderReturnFetchdetailsList()
                        {
                            product_list = delivermodelproduct,
                            shop_id = Convert.ToString(ds.Tables[1].Rows[i]["shop_id"]),
                            shop_address = Convert.ToString(ds.Tables[1].Rows[i]["shop_address"]),
                            shop_name = Convert.ToString(ds.Tables[1].Rows[i]["shop_name"]),
                            shop_contact_no = Convert.ToString(ds.Tables[1].Rows[i]["shop_contact_no"]),
                            pin_code = Convert.ToString(ds.Tables[1].Rows[i]["pin_code"]),
                            shop_lat = Convert.ToString(ds.Tables[1].Rows[i]["shop_lat"]),
                            shop_long = Convert.ToString(ds.Tables[1].Rows[i]["shop_long"]),
                            return_lat = Convert.ToString(ds.Tables[1].Rows[i]["return_lat"]),
                            return_long = Convert.ToString(ds.Tables[1].Rows[i]["return_long"]),
                            return_id = Convert.ToString(ds.Tables[1].Rows[i]["return_id"]),
                            return_date_time = Convert.ToString(ds.Tables[1].Rows[i]["return_date_time"]),
                            return_amount = Convert.ToDecimal(ds.Tables[1].Rows[i]["return_amount"])
                        });
                    }
                    odata.status = "200";
                    odata.message = "Order details available";
                    odata.return_list = oview;
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

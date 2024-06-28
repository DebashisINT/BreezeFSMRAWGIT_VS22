#region======================================Revision History=========================================================
//Written By : Debashis Talukder On 31/05/2024
//Purpose: For CRM Opportunity Details.Row: 932 to 936
#endregion===================================End of Revision History==================================================

using ShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShopAPI.Controllers
{
    public class CRMOpportunityDetailsController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage OpportunityStatusList(CRMOpportunityStatusInput model)
        {
            CRMOpportunityStatusOutput omodel = new CRMOpportunityStatusOutput();
            List<StatusList> oview = new List<StatusList>();

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
                sqlcmd = new SqlCommand("PRC_APICRMOPPORTUNITYDETAIL", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "FETCHOPPORTUNITYSTATUS");
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<StatusList>(dt);
                    omodel.status = "200";
                    omodel.message = "Successfully get Status list.";
                    omodel.status_list = oview;
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
        public HttpResponseMessage OpportunityDetailSave(OpportunityDetailSaveInput model)
        {
            OpportunityDetailSaveOutput omodel = new OpportunityDetailSaveOutput();

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
                    List<OpportunityProductLists> omodel2 = new List<OpportunityProductLists>();
                    foreach (var s2 in model.opportunity_product_list)
                    {
                        omodel2.Add(new OpportunityProductLists()
                        {
                            opportunity_id = s2.opportunity_id,
                            shop_id = s2.shop_id,
                            product_id = s2.product_id,
                            product_name = s2.product_name
                        });
                    }

                    string JsonXML = XmlConversion.ConvertToXml(omodel2, 0);

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APICRMOPPORTUNITYDETAIL", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "INSERTDATA");
                    sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@SHOP_ID", model.shop_id);
                    sqlcmd.Parameters.AddWithValue("@SHOP_NAME", model.shop_name);
                    sqlcmd.Parameters.AddWithValue("@SHOP_TYPE", model.shop_type);
                    sqlcmd.Parameters.AddWithValue("@OPPORTUNITY_ID", model.opportunity_id);
                    sqlcmd.Parameters.AddWithValue("@OPPORTUNITY_DESCRIPTION", model.opportunity_description);
                    sqlcmd.Parameters.AddWithValue("@OPPORTUNITY_AMOUNT", model.opportunity_amount);
                    sqlcmd.Parameters.AddWithValue("@OPPORTUNITY_STATUS_ID", model.opportunity_status_id);
                    sqlcmd.Parameters.AddWithValue("@OPPORTUNITY_STATUS_NAME", model.opportunity_status_name);
                    sqlcmd.Parameters.AddWithValue("@CREATED_DATE", model.opportunity_created_date);
                    sqlcmd.Parameters.AddWithValue("@CREATED_TIME", model.opportunity_created_time);
                    sqlcmd.Parameters.AddWithValue("@CREATED_DATE_TIME", model.opportunity_created_date_time);
                    sqlcmd.Parameters.AddWithValue("@JsonXML", JsonXML);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0 && Convert.ToString(dt.Rows[0][0]) == model.opportunity_id)
                    {
                        omodel.status = "200";
                        omodel.message = "Opportunity Saved Successfully.";
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
        public HttpResponseMessage OpportunityDetailEdit(OpportunityDetailEditInput model)
        {
            OpportunityDetailEditOutput omodel = new OpportunityDetailEditOutput();

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
                    List<OpportunityEditProductLists> omodel2 = new List<OpportunityEditProductLists>();
                    foreach (var s2 in model.edit_opportunity_product_list)
                    {
                        omodel2.Add(new OpportunityEditProductLists()
                        {
                            opportunity_id = s2.opportunity_id,
                            shop_id = s2.shop_id,
                            product_id = s2.product_id,
                            product_name = s2.product_name
                        });
                    }

                    string JsonXML = XmlConversion.ConvertToXml(omodel2, 0);

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APICRMOPPORTUNITYDETAIL", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "EDITDATA");
                    sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@SHOP_ID", model.shop_id);
                    sqlcmd.Parameters.AddWithValue("@SHOP_NAME", model.shop_name);
                    sqlcmd.Parameters.AddWithValue("@SHOP_TYPE", model.shop_type);
                    sqlcmd.Parameters.AddWithValue("@OPPORTUNITY_ID", model.opportunity_id);
                    sqlcmd.Parameters.AddWithValue("@OPPORTUNITY_DESCRIPTION", model.opportunity_description);
                    sqlcmd.Parameters.AddWithValue("@OPPORTUNITY_AMOUNT", model.opportunity_amount);
                    sqlcmd.Parameters.AddWithValue("@OPPORTUNITY_STATUS_ID", model.opportunity_status_id);
                    sqlcmd.Parameters.AddWithValue("@OPPORTUNITY_STATUS_NAME", model.opportunity_status_name);
                    sqlcmd.Parameters.AddWithValue("@CREATED_DATE", model.opportunity_created_date);
                    sqlcmd.Parameters.AddWithValue("@CREATED_TIME", model.opportunity_created_time);
                    sqlcmd.Parameters.AddWithValue("@CREATED_DATE_TIME", model.opportunity_created_date_time);
                    sqlcmd.Parameters.AddWithValue("@EDIT_DATE_TIME", model.opportunity_edited_date_time);
                    sqlcmd.Parameters.AddWithValue("@JsonXML", JsonXML);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0 && Convert.ToString(dt.Rows[0][0]) == model.opportunity_id)
                    {
                        omodel.status = "200";
                        omodel.message = "Opportunity Saved Successfully.";
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
        public HttpResponseMessage OpportunityDetailDelete(OpportunityDetailDeleteInput model)
        {
            OpportunityDetailDeleteOutput omodel = new OpportunityDetailDeleteOutput();

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
                    List<OpportunityDeleteProductLists> omodel2 = new List<OpportunityDeleteProductLists>();
                    foreach (var s2 in model.opportunity_delete_list)
                    {
                        omodel2.Add(new OpportunityDeleteProductLists()
                        {
                            opportunity_id = s2.opportunity_id
                        });
                    }

                    string JsonXML = XmlConversion.ConvertToXml(omodel2, 0);

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APICRMOPPORTUNITYDETAIL", sqlcon);
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
                        omodel.message = "Opportunity Deleted Successfully.";
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

        [HttpPost]
        public HttpResponseMessage OpportunityDetailLists(OpportunityDetailListsInput model)
        {
            OpportunityDetailListsOutput omodel = new OpportunityDetailListsOutput();
            List<OpportunitylistOutput> Ooview = new List<OpportunitylistOutput>();

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
                    sqlcmd = new SqlCommand("PRC_APICRMOPPORTUNITYDETAIL", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "FETCHOPPORTUNITYDETAIL");
                    sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(ds);
                    sqlcon.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            List<OpportunityProductListOutput> Poview = new List<OpportunityProductListOutput>();
                            for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                            {
                                if (Convert.ToString(ds.Tables[0].Rows[i]["opportunity_id"]) == Convert.ToString(ds.Tables[1].Rows[j]["opportunity_id"]))
                                {
                                    Poview.Add(new OpportunityProductListOutput()
                                    {
                                        shop_id = Convert.ToString(ds.Tables[1].Rows[j]["shop_id"]),
                                        opportunity_id = Convert.ToString(ds.Tables[1].Rows[j]["opportunity_id"]),
                                        product_id = Convert.ToInt64(ds.Tables[1].Rows[j]["product_id"]),
                                        product_name = Convert.ToString(ds.Tables[1].Rows[j]["product_name"])
                                    });
                                }
                            }

                            Ooview.Add(new OpportunitylistOutput()
                            {                               
                                shop_id = Convert.ToString(ds.Tables[0].Rows[i]["shop_id"]),
                                shop_name = Convert.ToString(ds.Tables[0].Rows[i]["shop_name"]),
                                shop_type = Convert.ToInt32(ds.Tables[0].Rows[i]["shop_type"]),
                                opportunity_id = Convert.ToString(ds.Tables[0].Rows[i]["opportunity_id"]),
                                opportunity_description = Convert.ToString(ds.Tables[0].Rows[i]["opportunity_description"]),
                                opportunity_amount = Convert.ToDecimal(ds.Tables[0].Rows[i]["opportunity_amount"]),
                                opportunity_status_id = Convert.ToInt64(ds.Tables[0].Rows[i]["opportunity_status_id"]),
                                opportunity_status_name = Convert.ToString(ds.Tables[0].Rows[i]["opportunity_status_name"]),
                                opportunity_created_date = Convert.ToString(ds.Tables[0].Rows[i]["opportunity_created_date"]),
                                opportunity_created_time = Convert.ToString(ds.Tables[0].Rows[i]["opportunity_created_time"]),
                                opportunity_created_date_time = Convert.ToString(ds.Tables[0].Rows[i]["opportunity_created_date_time"]),
                                opportunity_edited_date_time = Convert.ToString(ds.Tables[0].Rows[i]["opportunity_edited_date_time"]),
                                opportunity_product_list = Poview
                            });
                        }

                        omodel.status = "200";
                        omodel.message = "Successfully Get List.";
                        omodel.user_id = model.user_id;
                        omodel.opportunity_list = Ooview;
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

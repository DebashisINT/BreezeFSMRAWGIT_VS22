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
    public class DoctorActivityController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage PutDoctorActivity(DoctorActivityModel model)
        {
            DoctorActivitOutput omodel = new DoctorActivitOutput();
            try
            {
                string token = string.Empty;
                string versionname = string.Empty;
                System.Net.Http.Headers.HttpRequestHeaders headers = this.Request.Headers;
                String tokenmatch = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (!ModelState.IsValid)
                {
                    omodel.status = "213";
                    omodel.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
                }
                else
                {
                    // String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                    List<DoctorProduct> omedl2 = new List<DoctorProduct>();
                    List<DoctorProduct> qtyprod = new List<DoctorProduct>();
                    List<DoctorProduct> sampleprod = new List<DoctorProduct>();

                    foreach (var s2 in model.product_list)
                    {
                        omedl2.Add(new DoctorProduct()
                        {
                            product_id = s2.product_id,
                            product_name = s2.product_name,
                        });
                    }

                    foreach (var s2 in model.qty_product_list)
                    {
                        qtyprod.Add(new DoctorProduct()
                        {
                            product_id = s2.product_id,
                            product_name = s2.product_name,
                        });
                    }
                    foreach (var s2 in model.sample_product_list)
                    {
                        sampleprod.Add(new DoctorProduct()
                        {
                            product_id = s2.product_id,
                            product_name = s2.product_name,
                        });
                    }
                    string QtyJsonXML = "";
                    string JsonXML = "";
                    string SampleJsonXML = "";
                    if (model.product_list != null && model.product_list.Count > 0)
                    {
                        JsonXML = XmlConversion.ConvertToXml(omedl2, 0);
                    }

                    if (model.qty_product_list != null && model.qty_product_list.Count > 0)
                    {
                        QtyJsonXML = XmlConversion.ConvertToXml(qtyprod, 0);
                    }

                    if (model.sample_product_list != null && model.sample_product_list.Count > 0)
                    {
                        SampleJsonXML = XmlConversion.ConvertToXml(sampleprod, 0);
                    }

                    if (string.IsNullOrEmpty(model.amount))
                    {
                        model.amount = "0";
                    }
                    if (string.IsNullOrEmpty(model.crm_volume))
                    {
                        model.crm_volume = "0";
                    }

                    DataTable dt = new DataTable();

                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    //String con = Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]);

                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("Proc_API_DoctorActivitySubmit", sqlcon);
                    sqlcmd.Parameters.Add("@user_id", model.user_id);
                    sqlcmd.Parameters.Add("@SessionToken", model.session_token);
                    sqlcmd.Parameters.Add("@shop_id", model.shop_id);
                    sqlcmd.Parameters.Add("@Doctor_visit_id", model.doc_visit_id);
                    sqlcmd.Parameters.Add("@DoctorRemarks", model.doc_remarks);
                    sqlcmd.Parameters.Add("@PrescriberValue", model.is_prescriber);
                    sqlcmd.Parameters.Add("@QtyValue", model.is_qty);

                    sqlcmd.Parameters.Add("@QTY_VOL_REMARKS", model.qty_vol_text);
                    sqlcmd.Parameters.Add("@SampleValue", model.is_sample);
                    sqlcmd.Parameters.Add("@CRMValue", model.is_crm);
                    sqlcmd.Parameters.Add("@MoneyValue", model.is_money);

                    sqlcmd.Parameters.Add("@Amount", model.amount);
                    sqlcmd.Parameters.Add("@KIND_REMARKS", model.what);
                    sqlcmd.Parameters.Add("@FromDate", model.from_cme_date);
                    sqlcmd.Parameters.Add("@ToDate", model.to_crm_date);

                    sqlcmd.Parameters.Add("@CRM_VOLUME", model.crm_volume);
                    sqlcmd.Parameters.Add("@GiftValue", model.is_gift);
                    sqlcmd.Parameters.Add("@WHICH_KINDREMARKS", model.which_kind);
                    sqlcmd.Parameters.Add("@Next_Visit_Date", model.next_visit_date);
                    sqlcmd.Parameters.Add("@MR_Remarks", model.remarks_mr);

                    sqlcmd.Parameters.Add("@Product_List", JsonXML);
                    sqlcmd.Parameters.Add("@QtyProduct_List", QtyJsonXML);
                    sqlcmd.Parameters.Add("@SampleProduct_List", SampleJsonXML);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Successfully updated doctor Activity list.";
                    }
                    else
                    {
                        omodel.status = "202";
                        omodel.message = "Invalid user credential.";
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
        public HttpResponseMessage GetDoctorActivityList(DoctorActivitlistInput model)
        {
            DoctorActivityListoutput omodel = new DoctorActivityListoutput();
            Datalists odata = new Datalists();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                string sessionId = "";

                List<DoctorActivityList> omedl2 = new List<DoctorActivityList>();

                DataSet ds = new DataSet();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("Proc_API_DoctorActivityList", sqlcon);
                sqlcmd.Parameters.Add("@user_id", model.user_id);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(ds);
                sqlcon.Close();
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    List<DoctorActivityList> oview = new List<DoctorActivityList>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        List<DoctorProduct> productList = new List<DoctorProduct>();
                        for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                        {
                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                if (Convert.ToString(ds.Tables[1].Rows[j]["DOCTOR_ACTIVITYID"]) == Convert.ToString(ds.Tables[0].Rows[i]["DOC_ACTIVITYID"]) && Convert.ToInt64(ds.Tables[1].Rows[j]["DOCACTIVITY_HEADID"]) == Convert.ToInt64(ds.Tables[0].Rows[i]["ID"]))
                                {
                                    productList.Add(new DoctorProduct()
                                    {
                                        product_id = Convert.ToString(ds.Tables[1].Rows[j]["PRODUCT_ID"]),
                                        product_name = Convert.ToString(ds.Tables[1].Rows[j]["PRODUCT_NAME"])
                                    });
                                }
                            }
                        }

                        List<DoctorProduct> QtyproductList = new List<DoctorProduct>();
                        if (ds.Tables[2].Rows.Count > 0)
                        {
                            for (int j = 0; j < ds.Tables[2].Rows.Count; j++)
                            {
                                if (ds.Tables[2].Rows.Count > 0)
                                {
                                    if (Convert.ToString(ds.Tables[2].Rows[j]["DOCTOR_ACTIVITYID"]) == Convert.ToString(ds.Tables[0].Rows[i]["DOC_ACTIVITYID"]) && Convert.ToInt64(ds.Tables[2].Rows[j]["DOCACTIVITY_HEADID"]) == Convert.ToInt64(ds.Tables[0].Rows[i]["ID"]))
                                    {
                                        QtyproductList.Add(new DoctorProduct()
                                        {
                                            product_id = Convert.ToString(ds.Tables[2].Rows[j]["PRODUCT_ID"]),
                                            product_name = Convert.ToString(ds.Tables[2].Rows[j]["PRODUCT_NAME"])
                                        });
                                    }
                                }
                            }
                        }

                        List<DoctorProduct> SampleproductList = new List<DoctorProduct>();
                        if (ds.Tables[3].Rows.Count > 0)
                        {
                            for (int j = 0; j < ds.Tables[3].Rows.Count; j++)
                            {
                                if (ds.Tables[2].Rows.Count > 0)
                                {
                                    if (Convert.ToString(ds.Tables[3].Rows[j]["DOCTOR_ACTIVITYID"]) == Convert.ToString(ds.Tables[0].Rows[i]["DOC_ACTIVITYID"]) && Convert.ToInt64(ds.Tables[2].Rows[j]["DOCACTIVITY_HEADID"]) == Convert.ToInt64(ds.Tables[0].Rows[i]["ID"]))
                                    {
                                        SampleproductList.Add(new DoctorProduct()
                                        {
                                            product_id = Convert.ToString(ds.Tables[3].Rows[j]["PRODUCT_ID"]),
                                            product_name = Convert.ToString(ds.Tables[3].Rows[j]["PRODUCT_NAME"])
                                        });
                                    }
                                }
                            }
                        }

                        oview.Add(new DoctorActivityList()
                        {
                            product_list = productList,
                            qty_product_list = QtyproductList,
                            sample_product_list=SampleproductList,
                            shop_id = Convert.ToString(ds.Tables[0].Rows[i]["SHOP_CODE"]),
                            doc_visit_id = Convert.ToString(ds.Tables[0].Rows[i]["DOC_ACTIVITYID"]),
                            doc_remarks = Convert.ToString(ds.Tables[0].Rows[i]["DOC_REMARKS"]),
                            is_prescriber = Convert.ToString(ds.Tables[0].Rows[i]["PRESCRIBER_VALUE"]),
                            is_qty = Convert.ToString(ds.Tables[0].Rows[i]["QTY_VALUE"]),
                            qty_vol_text = Convert.ToString(ds.Tables[0].Rows[i]["QTY_VOL_REMARKS"]),
                            is_sample = Convert.ToString(ds.Tables[0].Rows[i]["SAMPLE_VALUE"]),

                            is_crm = Convert.ToString(ds.Tables[0].Rows[i]["CRM_VALUE"]),
                            is_money = Convert.ToString(ds.Tables[0].Rows[i]["MONEY_VALUE"]),
                            amount = Convert.ToString(ds.Tables[0].Rows[i]["AMOUNT"]),
                            what = Convert.ToString(ds.Tables[0].Rows[i]["KIND_REMARKS"]),
                            from_cme_date = Convert.ToString(ds.Tables[0].Rows[i]["FROM_DATE"]) == "" ? "" : Convert.ToDateTime(ds.Tables[0].Rows[i]["FROM_DATE"]).ToString("yyyy-MM-dd"),
                            to_crm_date = Convert.ToString(ds.Tables[0].Rows[i]["TO_DATE"]) == "" ? "" : Convert.ToDateTime(ds.Tables[0].Rows[i]["TO_DATE"]).ToString("yyyy-MM-dd"),                          

                            crm_volume = Convert.ToString(ds.Tables[0].Rows[i]["CRM_VOLUME"]),
                            is_gift = Convert.ToString(ds.Tables[0].Rows[i]["GIFT_VALUE"]),
                            which_kind = Convert.ToString(ds.Tables[0].Rows[i]["WHICH_KINDREMARKS"]),
                            next_visit_date =Convert.ToString(ds.Tables[0].Rows[i]["NEXT_VISIT_DATE"]) == "" ? "" : Convert.ToDateTime(ds.Tables[0].Rows[i]["NEXT_VISIT_DATE"]).ToString("yyyy-MM-dd"),
                            remarks_mr = Convert.ToString(ds.Tables[0].Rows[i]["MR_REMARKS"])
                        });
                    }
                    omodel.doc_visit_list = oview;
                    omodel.status = "200";
                    omodel.message = "Successfully get doctor Activity list.";
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
    }
}

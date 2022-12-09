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
    public class ChemistActivityController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage PutChemistActivity(ChemistActivitModel model)
        {
            ChemistActivitOutput omodel = new ChemistActivitOutput();

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
                    string sessionId = "";
                    List<ChemistProduct> omedl2 = new List<ChemistProduct>();
                    List<ChemistProduct> pobprod = new List<ChemistProduct>();

                    foreach (var s2 in model.product_list)
                    {
                        omedl2.Add(new ChemistProduct()
                        {
                            product_id = s2.product_id,
                            product_name = s2.product_name,
                        });
                    }

                    foreach (var s2 in model.pob_product_list)
                    {
                        pobprod.Add(new ChemistProduct()
                        {
                            product_id = s2.product_id,
                            product_name = s2.product_name,
                        });
                    }
                    string PobJsonXML = "";
                    string JsonXML = "";
                    if (model.product_list != null && model.product_list.Count > 0)
                    {
                        JsonXML = XmlConversion.ConvertToXml(omedl2, 0);
                    }

                    if (model.pob_product_list != null && model.pob_product_list.Count > 0)
                    {
                        PobJsonXML = XmlConversion.ConvertToXml(pobprod, 0);
                    }
                    if (string.IsNullOrEmpty(model.volume))
                    {
                        model.volume = "0";
                    }

                    DataTable dt = new DataTable();

                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    //String con = Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]);

                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("Proc_API_ChemistActivitySubmit", sqlcon);
                    sqlcmd.Parameters.Add("@user_id", model.user_id);
                    sqlcmd.Parameters.Add("@SessionToken", model.session_token);
                    sqlcmd.Parameters.Add("@chemist_visit_id", model.chemist_visit_id);
                    sqlcmd.Parameters.Add("@isPob", model.isPob);
                    sqlcmd.Parameters.Add("@remarks", model.remarks);
                    sqlcmd.Parameters.Add("@next_visit_date", model.next_visit_date);
                    sqlcmd.Parameters.Add("@shop_id", model.shop_id);
                    sqlcmd.Parameters.Add("@volume", model.volume);
                    sqlcmd.Parameters.Add("@remarks_mr", model.remarks_mr);
                    sqlcmd.Parameters.Add("@Product_List", JsonXML);
                    sqlcmd.Parameters.Add("@PobProduct_List", PobJsonXML);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Successfully updated chemist activity list.";
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
        public HttpResponseMessage GetChemistActivityList(ChemistActivitlistInput model)
        {
            ChemistActivityListoutput omodel = new ChemistActivityListoutput();
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

                List<ChemistActivityList> omedl2 = new List<ChemistActivityList>();

                DataSet ds = new DataSet();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("Proc_API_ChemistActivityList", sqlcon);
                sqlcmd.Parameters.Add("@user_id", model.user_id);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(ds);
                sqlcon.Close();
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    List<ChemistActivityList> oview = new List<ChemistActivityList>();
                    OrderfetchdetailsList orderdetailprd = new OrderfetchdetailsList();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        List<ChemistProduct> productList = new List<ChemistProduct>();
                        for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                        {
                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                if (Convert.ToString(ds.Tables[1].Rows[j]["CHEMIST_ACTIVITYID"]) == Convert.ToString(ds.Tables[0].Rows[i]["CHEMIST_ACTIVITYID"]) && Convert.ToInt64(ds.Tables[1].Rows[j]["ACTIVITY_HEADID"]) == Convert.ToInt64(ds.Tables[0].Rows[i]["ID"]))
                                {
                                    productList.Add(new ChemistProduct()
                                    {
                                        product_id = Convert.ToString(ds.Tables[1].Rows[j]["PRODUCT_ID"]),
                                        product_name = Convert.ToString(ds.Tables[1].Rows[j]["PRODUCT_NAME"])
                                    });
                                }
                            }
                        }

                        List<ChemistProduct> PodproductList = new List<ChemistProduct>();
                        if (ds.Tables[2].Rows.Count > 0)
                        {
                            for (int j = 0; j < ds.Tables[2].Rows.Count; j++)
                            {
                                if (ds.Tables[2].Rows.Count > 0)
                                {
                                    if (Convert.ToString(ds.Tables[2].Rows[j]["CHEMIST_ACTIVITYID"]) == Convert.ToString(ds.Tables[0].Rows[i]["CHEMIST_ACTIVITYID"]) && Convert.ToInt64(ds.Tables[2].Rows[j]["ACTIVITY_HEADID"]) == Convert.ToInt64(ds.Tables[0].Rows[i]["ID"]))
                                    {
                                        PodproductList.Add(new ChemistProduct()
                                        {
                                            product_id = Convert.ToString(ds.Tables[2].Rows[j]["PRODUCT_ID"]),
                                            product_name = Convert.ToString(ds.Tables[2].Rows[j]["PRODUCT_NAME"])
                                        });
                                    }
                                }
                            }
                        }

                        oview.Add(new ChemistActivityList()
                        {
                            product_list = productList,
                            pob_product_list = PodproductList,
                            shop_id = Convert.ToString(ds.Tables[0].Rows[i]["SHOP_CODE"]),
                            chemist_visit_id = Convert.ToString(ds.Tables[0].Rows[i]["CHEMIST_ACTIVITYID"]),
                            isPob = Convert.ToString(ds.Tables[0].Rows[i]["ISPOB"]),
                            volume = Convert.ToString(ds.Tables[0].Rows[i]["VOLUME"]),
                            remarks = Convert.ToString(ds.Tables[0].Rows[i]["REMARKS"]),
                            next_visit_date = (Convert.ToString(ds.Tables[0].Rows[i]["NEXT_VISITDATE"]) == "") ? "" : Convert.ToDateTime(ds.Tables[0].Rows[i]["NEXT_VISITDATE"]).ToString("yyyy-MM-dd"),
                            remarks_mr = Convert.ToString(ds.Tables[0].Rows[i]["MR_REMARKS"])
                        });
                    }
                    omodel.chemist_visit_list = oview;
                    omodel.status = "200";
                    omodel.message = "Successfully get chemist Activity list.";
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

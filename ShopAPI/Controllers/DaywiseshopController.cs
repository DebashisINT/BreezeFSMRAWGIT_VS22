#region======================================Revision History=========================================================
//1.0   V2.0.37     Debashis    10/01/2023      Some new parameters have been added.Row: 787
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
    public class DaywiseshopController : ApiController
    {

        [HttpPost]
        public HttpResponseMessage Records(ShopdaywiseInput model)
        {
            ShopdaywiseOutput omodel = new ShopdaywiseOutput();
            List<ShopdaywiseList> oview = new List<ShopdaywiseList>();
            List<ShopdaywiseList> oview1 = new List<ShopdaywiseList>();
            ShopList odata = new ShopList();
            List<ShopList> shoplst = new List<ShopList>();
            ShopdaywiseList odelails = new ShopdaywiseList();
            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                //Rev 1.0 Row:787
                //String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                String token = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];
                //End of Rev 1.0 Row:787
                string sessionId = "";


                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                //Rev 1.0 Row:787
                //String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                //End of Rev 1.0 Row:787
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("Sp_API_DaywiseShop", sqlcon);
                //Rev 1.0 Row:787
                //sqlcmd.Parameters.Add("@date_span", model.date_span);
                //sqlcmd.Parameters.Add("@from_date", model.from_date);
                //sqlcmd.Parameters.Add("@to_date", model.to_date);
                //sqlcmd.Parameters.Add("@user_id", model.user_id);
                //sqlcmd.Parameters.Add("@Action", "0");
                sqlcmd.Parameters.AddWithValue("@date_span", model.date_span);
                sqlcmd.Parameters.AddWithValue("@from_date", model.from_date);
                sqlcmd.Parameters.AddWithValue("@to_date", model.to_date);
                sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);
                sqlcmd.Parameters.AddWithValue("@Action", "0");
                //End of Rev 1.0 Row:787
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(ds);
                sqlcon.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    omodel.toal_shopvisit_count = Int32.Parse(ds.Tables[0].Rows[0]["totcount"].ToString());
                    omodel.avg_shopvisit_count = Int32.Parse(ds.Tables[0].Rows[0]["avgshop"].ToString());

                  ///  oview = APIHelperMethods.ToModelList<ShopdaywiseList>(ds.Tables[1]);

                    // var DistinctItems = oview.GroupBy(x => x.date).Select(y => y.First());


                    //foreach (var item in DistinctItems)
                    //{
                    //    odelails.date = item.date;

                    //    var DistinctItemsdate = oview.Where(x => x.date == item.date).ToList();
                    //  //  shoplst.Clear();

                    //    foreach (var item2 in DistinctItemsdate)
                    //    {

                    //        shoplst.Add(new ShopList()
                    //        {
                    //            shopid = item2.shopid,
                    //            duration_spent=item2.duration_spent
                    //        });


                    //    }

                    //    oview1.Add(new ShopdaywiseList()
                    //    {

                    //        date = odelails.date,
                    //        shop_list = shoplst
                    //    });
                    //}

                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        odelails.date = ds.Tables[1].Rows[i]["date"].ToString();

                        shoplst = APIHelperMethods.ToModelList<ShopList>(ds.Tables[2]);

                        var DistinctItemsdate = shoplst.Where(x => x.date == odelails.date).ToList();

                        //for (int j = 0; j < ds.Tables[2].Rows.Count; j++)
                        //{
                        //sqlcon.Open();
                        //sqlcmd = new SqlCommand("Sp_API_DaywiseShop", sqlcon);
                        //sqlcmd.Parameters.Add("@from_date", ds.Tables[1].Rows[i]["date"].ToString());
                        //sqlcmd.Parameters.Add("@user_id", model.user_id);
                        //sqlcmd.Parameters.Add("@Action",1);

                        //sqlcmd.CommandType = CommandType.StoredProcedure;

                        //da = new SqlDataAdapter(sqlcmd);
                        //da.Fill(dt);
                        //sqlcon.Close();

                        //if (ds.Tables[1].Rows[i]["date"].ToString() == ds.Tables[2].Rows[j]["date"].ToString())
                        //{
                        //    odelails.shop_list = APIHelperMethods.ToModelList<ShopList>(ds.Tables[2]);
                      //  var listWithoutCol = DistinctItemsdate.Select(x => new { x.duration_spent, x.shopid }).ToList();

                        oview.Add(new ShopdaywiseList()
                        {

                            date = odelails.date,
                            shop_list = DistinctItemsdate
                        });


                        //}

                        //}
                    }
                    omodel.date_list = oview;
                    omodel.status = "200";
                    omodel.message = "Attendance list for last 15 days / start day to end date";

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

        //Rev Debashis Row:749
        [HttpPost]
        public HttpResponseMessage ITCRecords(ITCShopdaywiseInput model)
        {
            ITCShopdaywiseOutput omodel = new ITCShopdaywiseOutput();
            List<ITCShopdaywiseList> oview = new List<ITCShopdaywiseList>();
            List<ITCShopList> shoplst = new List<ITCShopList>();
            ITCShopdaywiseList odelails = new ITCShopdaywiseList();
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
                    //Rev Debbashis Row: 787
                    //String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                    String token = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];
                    //End of Rev Debbashis Row: 787
                    string sessionId = "";
                    DataSet ds = new DataSet();
                    //Rev Debbashis Row: 787
                    //String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    //End of Rev Debbashis Row: 787
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APIITCDAYWISESHOP", sqlcon);
                    //Rev Debbashis Row: 787
                    //sqlcmd.Parameters.Add("@date_span", model.date_span);
                    //sqlcmd.Parameters.Add("@from_date", model.from_date);
                    //sqlcmd.Parameters.Add("@to_date", model.to_date);
                    //sqlcmd.Parameters.Add("@user_id", model.user_id);
                    //sqlcmd.Parameters.Add("@Action", "0");
                    sqlcmd.Parameters.AddWithValue("@date_span", model.date_span);
                    sqlcmd.Parameters.AddWithValue("@from_date", model.from_date);
                    sqlcmd.Parameters.AddWithValue("@to_date", model.to_date);
                    sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@Action", "0");
                    //End of Rev Debbashis Row: 787
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(ds);
                    sqlcon.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        omodel.toal_shopvisit_count = Int32.Parse(ds.Tables[0].Rows[0]["totcount"].ToString());
                        omodel.avg_shopvisit_count = Int32.Parse(ds.Tables[0].Rows[0]["avgshop"].ToString());

                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            odelails.date = ds.Tables[1].Rows[i]["date"].ToString();
                            shoplst = APIHelperMethods.ToModelList<ITCShopList>(ds.Tables[2]);
                            var DistinctItemsdate = shoplst.Where(x => x.date == odelails.date).ToList();

                            oview.Add(new ITCShopdaywiseList()
                            {
                                date = odelails.date,
                                shop_list = DistinctItemsdate
                            });
                        }
                        omodel.date_list = oview;
                        omodel.status = "200";
                        omodel.message = "Attendance list for last 15 days / start day to end date";
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
            catch (Exception ex)
            {
                omodel.status = "204";
                omodel.message = ex.Message;
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }
        //End of Rev Debashis Row:749
    }
}

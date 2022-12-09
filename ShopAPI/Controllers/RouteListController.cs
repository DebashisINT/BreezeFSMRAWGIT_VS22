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
    public class RouteListController : ApiController
    {

        [HttpPost]
        public HttpResponseMessage List(RoutefetchInput model)
        {


            RouteDetailsOutput odata = new RouteDetailsOutput();

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

                List<Locationupdate> omedl2 = new List<Locationupdate>();

                DataSet ds = new DataSet();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("Proc_FTS_RouteList", sqlcon);
                sqlcmd.Parameters.Add("@User_Id", model.user_id);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(ds);
                sqlcon.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {

                    List<RouteDetailsOutputfetch> oview = new List<RouteDetailsOutputfetch>();
                    RouteDetailsOutputfetch orderdetailprd = new RouteDetailsOutputfetch();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {


                        List<RouteListclass> shoplist = new List<RouteListclass>();
                        for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                        {


                            int i1 = 0;
                            if (Convert.ToString(ds.Tables[1].Rows[j]["Pincode"]) == Convert.ToString(ds.Tables[0].Rows[i]["Pincode"]))
                            {

                                shoplist.Add(new RouteListclass()
                                {

                                    shop_id = Convert.ToString(ds.Tables[1].Rows[j]["shop_id"]),
                                    shop_address = Convert.ToString(ds.Tables[1].Rows[j]["shop_address"]),
                                    shop_name = Convert.ToString(ds.Tables[1].Rows[j]["shop_name"]),
                                    shop_contact_no = Convert.ToString(ds.Tables[1].Rows[j]["shop_contact_no"])

                                });



                            }


                        }
                        shoplist.Add(new RouteListclass()
                        {

                            shop_id = Convert.ToString(ds.Tables[0].Rows[i]["Pincode"]) + '~' + "New" + '~' + Convert.ToString(Guid.NewGuid()),
                            shop_address = "",
                            shop_name = "Other",
                            shop_contact_no = ""

                        });
                        oview.Add(new RouteDetailsOutputfetch()
                        {
                            shop_details_list = shoplist,
                            id = Convert.ToString(ds.Tables[0].Rows[i]["Pincode"]),
                            route_name = Convert.ToString(ds.Tables[0].Rows[i]["Routename"]),

                        });
                    }

                    oview.Add(new RouteDetailsOutputfetch()
                    {
                        shop_details_list = null,
                        id = Convert.ToString(Guid.NewGuid()) + '~' + "Other",
                        route_name = "Other",

                    });



                    odata.route_list = oview;
                    odata.status = "200";
                    odata.message = "Route details  available";



                }
                else
                {
                    List<RouteDetailsOutputfetch> oview = new List<RouteDetailsOutputfetch>();
                    oview.Add(new RouteDetailsOutputfetch()
                    {
                        shop_details_list = null,
                        id = Convert.ToString(Guid.NewGuid()) + '~' + "Other",
                        route_name = "Other",

                    });



                    odata.route_list = oview;
                    odata.status = "200";
                    odata.message = "Route details  available";

                }

                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }

        }




        [HttpPost]
        public HttpResponseMessage ListofRoutes(ClassLoginINput_Route model)
        {



            ClassLoginOutput_Route oview = new ClassLoginOutput_Route();
            UserClasscounting ocounting = new UserClasscounting();
            List<WorkTypeslogin> worktype = new List<WorkTypeslogin>();
            try
            {
                if (!ModelState.IsValid)
                {
                    oview.status = "213";
                    oview.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, oview);
                }
                else
                {
                    String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                    String profileImg = System.Configuration.ConfigurationSettings.AppSettings["ProfileImageURL"];



                    DataSet dt = new DataSet();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();

                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();


                    sqlcmd = new SqlCommand("Proc_Route_Login", sqlcon);

                    sqlcmd.Parameters.Add("@UserID", model.UserID);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();



                    if (dt.Tables[2].Rows.Count != 0 || dt.Tables[3].Rows.Count != 0)
                    {




                        worktype = APIHelperMethods.ToModelList<WorkTypeslogin>(dt.Tables[2]);

                        //////--------------------New Route and work Type//////////////



                        List<RouteDetailsOutputuser> onview = new List<RouteDetailsOutputuser>();

                        for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                        {


                            List<RouteShoptclass> shoplist = new List<RouteShoptclass>();
                            for (int j = 0; j < dt.Tables[1].Rows.Count; j++)
                            {


                                int i1 = 0;
                                if (Convert.ToString(dt.Tables[1].Rows[j]["Pincode"]) == Convert.ToString(dt.Tables[0].Rows[i]["Pincode"]))
                                {

                                    shoplist.Add(new RouteShoptclass()
                                    {

                                        shop_id = Convert.ToString(dt.Tables[1].Rows[j]["shop_id"]),
                                        shop_address = Convert.ToString(dt.Tables[1].Rows[j]["shop_address"]),
                                        shop_name = Convert.ToString(dt.Tables[1].Rows[j]["shop_name"]),
                                        shop_contact_no = Convert.ToString(dt.Tables[1].Rows[j]["shop_contact_no"])

                                    });


                                }


                            }

                            for (int j = 0; j < dt.Tables[3].Rows.Count; j++)
                            {
                                if (Convert.ToString(dt.Tables[3].Rows[j]["Pincode"]) == Convert.ToString(dt.Tables[0].Rows[i]["Pincode"]))
                                {

                                    shoplist.Add(new RouteShoptclass()
                                    {

                                        shop_id = Convert.ToString(dt.Tables[3].Rows[j]["shop_id"]),
                                        shop_address = Convert.ToString(dt.Tables[3].Rows[j]["shop_address"]),
                                        shop_name = Convert.ToString(dt.Tables[3].Rows[j]["shop_name"]),
                                        shop_contact_no = Convert.ToString(dt.Tables[3].Rows[j]["shop_contact_no"])

                                    });

                                }

                            }

                            onview.Add(new RouteDetailsOutputuser()
                            {
                                shop_details_list = shoplist,
                                id = Convert.ToString(dt.Tables[0].Rows[i]["Pincode"]),
                                route_name = Convert.ToString(dt.Tables[0].Rows[i]["Routename"]),

                            });
                        }




                        oview.route_list = onview;
                        oview.worktype = worktype;
                        ////////////////////////////



                        oview.status = "200";
                        oview.message = "List populated.";


                    }
                    else
                    {
                        oview.status = "205";
                        oview.message = "No Data Found.";

                    }

                    var message = Request.CreateResponse(HttpStatusCode.OK, oview);
                    return message;
                }

            }
            catch (Exception ex)
            {


                oview.status = "209";

                oview.message = ex.Message;
                var message = Request.CreateResponse(HttpStatusCode.OK, oview);
                return message;
            }

        }


        [HttpPost]
        public HttpResponseMessage ArealocationList(RoutefetchInput model)
        {
            RouteDetailsOutput odata = new RouteDetailsOutput();

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

                List<Locationupdate> omedl2 = new List<Locationupdate>();

                DataSet ds = new DataSet();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("Proc_FTS_RouteList", sqlcon);
                sqlcmd.Parameters.Add("@User_Id", model.user_id);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(ds);
                sqlcon.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {

                    List<RouteDetailsOutputfetch> oview = new List<RouteDetailsOutputfetch>();
                    RouteDetailsOutputfetch orderdetailprd = new RouteDetailsOutputfetch();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {


                        List<RouteListclass> shoplist = new List<RouteListclass>();
                        for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                        {


                            int i1 = 0;
                            if (Convert.ToString(ds.Tables[1].Rows[j]["Pincode"]) == Convert.ToString(ds.Tables[0].Rows[i]["Pincode"]))
                            {

                                shoplist.Add(new RouteListclass()
                                {

                                    shop_id = Convert.ToString(ds.Tables[1].Rows[j]["shop_id"]),
                                    shop_address = Convert.ToString(ds.Tables[1].Rows[j]["shop_address"]),
                                    shop_name = Convert.ToString(ds.Tables[1].Rows[j]["shop_name"]),
                                    shop_contact_no = Convert.ToString(ds.Tables[1].Rows[j]["shop_contact_no"])

                                });



                            }


                        }
                        shoplist.Add(new RouteListclass()
                        {

                            shop_id = Convert.ToString(ds.Tables[0].Rows[i]["Pincode"]) + '~' + "New" + '~' + Convert.ToString(Guid.NewGuid()),
                            shop_address = "",
                            shop_name = "Other",
                            shop_contact_no = ""

                        });
                        oview.Add(new RouteDetailsOutputfetch()
                        {
                            shop_details_list = shoplist,
                            id = Convert.ToString(ds.Tables[0].Rows[i]["Pincode"]),
                            route_name = Convert.ToString(ds.Tables[0].Rows[i]["Routename"]),

                        });
                    }

                    oview.Add(new RouteDetailsOutputfetch()
                    {
                        shop_details_list = null,
                        id = Convert.ToString(Guid.NewGuid()) + '~' + "Other",
                        route_name = "Other",

                    });



                    odata.route_list = oview;
                    odata.status = "200";
                    odata.message = "Route details  available";



                }
                else
                {
                    List<RouteDetailsOutputfetch> oview = new List<RouteDetailsOutputfetch>();
                    oview.Add(new RouteDetailsOutputfetch()
                    {
                        shop_details_list = null,
                        id = Convert.ToString(Guid.NewGuid()) + '~' + "Other",
                        route_name = "Other",

                    });



                    odata.route_list = oview;
                    odata.status = "200";
                    odata.message = "Route details  available";

                }

                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }

        }
    }
}

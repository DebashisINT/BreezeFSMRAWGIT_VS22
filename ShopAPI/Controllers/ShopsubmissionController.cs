#region======================================Revision History=============================================================================================================
//1.0   V2.0.37     Debashis    10/01/2023      Some new parameters have been added.Row: 786
//2.0   V2.0.39     Debashis    27/03/2023      A new parameter has been added.Row: 814 & Refer: 0025749
//3.0   V2.0.39     Debashis    04/04/2023      Optimized Shopsubmission/ShopVisited & Shopsubmission/ITCShopVisited API.
//                                              Refer: 0025779
//4.0   V2.0.39     Debashis    24/04/2023      Some new parameters have been added.Row: 820
//5.0   V2.0.40     Debashis    16/06/2023      Case : If IsUpdateVisitDataInTodayTable this settings is true then with Addshop api the data insert in both tbl_Master_shop & 
//                                              Trans_ShopActivitySubmit_TodayData table.
//                                              At app logout Shopsubmission/ITCShopVisited this api is called with some updated data for the shop & needs to be updated in
//                                              Trans_ShopActivitySubmit_TodayData this table through Shopsubmission/ITCShopVisited this api.Refer: 0026359
//6.0   V2.0.40     Debashis    10/07/2023      A new parameter has been added.Row: 855
//7.0   V2.0.41     Debashis    15/07/2023      New requirment for Update data.Row: 859
//8.0   V2.0.45     Debashis    03/04/2024      Some new parameters have been added.Row: 915 & Refer: 0027335
#endregion===================================End of Revision History======================================================================================================
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

namespace ShopAPI.Controllers
{
    public class ShopsubmissionController : ApiController
    {
        List<DatalistsSumission> oview = new List<DatalistsSumission>();
        ShopsubmissionOutput omodel = new ShopsubmissionOutput();
        public delegate void del(ShopsubmissionInput model, List<ShopsubmissionModel> omedl2);
        string uploadtext = "~/CommonFolder/Log/RevisitLog.txt";
        string svisitlog = "";
        //Rev Debashis : Mantis:0025493
        //String logprint = System.Configuration.ConfigurationSettings.AppSettings["Logprint"];
        String logprint = System.Configuration.ConfigurationManager.AppSettings["Logprint"];
        //End of Rev Debashis : Mantis:0025493

        [HttpPost]
        public HttpResponseMessage ShopVisited(ShopsubmissionInput model)
        {
            //  TextWriter txt = new StreamWriter(Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(uploadtext)));
            string sdatetime = DateTime.Now.ToString();
            try
            {
                //using (StreamWriter stream = new FileInfo((Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(uploadtext)))).AppendText())
                //       {
                //           stream.WriteLine("   Revisit  Start " + "Start Time:" + DateTime.Now.ToString() + "End Time:" + DateTime.Now + "Status:Records not updated(Shopsubmission/ShopVisited)[202]" + "User ID:" + model.user_id + svisitlog + "   Revisit  END ");
                //      }

                if (!ModelState.IsValid)
                {
                    omodel.status = "213";
                    omodel.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
                }
                else
                {
                    //End of Rev Debashis : Mantis:0025493
                    //String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                    String token = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];
                    //End of Rev Debashis : Mantis:0025493
                    string sessionId = "";
                    List<ShopsubmissionModel> omedl2 = new List<ShopsubmissionModel>();
                    foreach (var s2 in model.shop_list)
                    {
                        omedl2.Add(new ShopsubmissionModel()
                        {
                            shop_id = s2.shop_id,
                            spent_duration = s2.spent_duration,
                            visited_date = s2.visited_date,
                            visited_time = s2.visited_time,
                            total_visit_count = s2.total_visit_count,
                            distance_travelled = s2.distance_travelled,
                            user_id = model.user_id,
                            feedback = s2.feedback,
                            distanceFromHomeLoc = s2.distanceFromHomeLoc,
                            isFirstShopVisited = s2.isFirstShopVisited,
                            early_revisit_reason = s2.early_revisit_reason,
                            device_model = s2.device_model,
                            android_version = s2.android_version,
                            battery = s2.battery,
                            net_status = s2.net_status,
                            net_type = s2.net_type,
                            in_time = s2.in_time,
                            out_time = s2.out_time,
                            start_timestamp = s2.start_timestamp,
                            in_location = s2.in_location,
                            out_location = s2.out_location,
                            shop_revisit_uniqKey = s2.shop_revisit_uniqKey,
                            //Rev Debashis
                            pros_id = s2.pros_id,
                            updated_by = s2.updated_by,
                            updated_on = s2.updated_on,
                            agency_name = s2.agency_name,
                            approximate_1st_billing_value = s2.approximate_1st_billing_value,
                            //End of Rev Debashis
                            //Rev 1.0 Row:786
                            multi_contact_name= s2.multi_contact_name,
                            multi_contact_number= s2.multi_contact_number,
                            //End of Rev 1.0 Row:786
                            //Rev 2.0 Row:814
                            IsShopUpdate=s2.IsShopUpdate,
                            //End of Rev 2.0 Row:814
                            //Rev 4.0 Row:820
                            distFromProfileAddrKms= s2.distFromProfileAddrKms,
                            stationCode= s2.stationCode,
                            //End of Rev 4.0 Row:820
                            //Rev 8.0 Row: 915 & Refer: 0027335
                            shop_lat = s2.shop_lat,
                            shop_long = s2.shop_long,
                            shop_addr = s2.shop_addr
                            //End of Rev 8.0 Row: 915 & Refer: 0027335
                        });


                        // svisitlog = svisitlog+ " Shop:"+s2.shop_id + "  Visit:"+s2.visited_time;
                    }


                    List<DatalistsSumission> omed3 = new List<DatalistsSumission>();

                    foreach (var s2 in model.shop_list)
                    {
                        omed3.Add(new DatalistsSumission()
                        {
                            shopid = s2.shop_id,
                            spent_duration = s2.spent_duration,
                            visited_date = Convert.ToDateTime(s2.visited_time),
                            visited_time = Convert.ToDateTime(s2.visited_time),
                            total_visit_count = s2.total_visit_count,
                            distance_travelled = s2.distance_travelled,
                            //Rev 2.0 Row:814
                            IsShopUpdate=s2.IsShopUpdate
                            //End of Rev 2.0 Row:814
                        });

                        svisitlog = svisitlog + " Shop:" + s2.shop_id + "  Visit:" + s2.visited_time;
                    }
                    //Rev 2.0 Mantis: 0025749
                    //if (logprint == "1")
                    //{
                    //    using (StreamWriter stream = new FileInfo((Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(uploadtext)))).AppendText())
                    //    {
                    //        stream.WriteLine("Revisit  Start " + "Start Time:" + DateTime.Now.ToString() + "End Time:" + DateTime.Now + "Status:(Shopsubmission/ShopVisited)[200]" + "User ID:" + model.user_id + svisitlog + "   Revisit  END ");
                    //    }

                    //}
                    
                    //del ldMainAct = new del(InsertShopVisit);
                    //ldMainAct.BeginInvoke(model, omedl2, null, null);


                    ////string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);
                    ////DataTable dt = new DataTable();
                    ////String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    ////SqlCommand sqlcmd = new SqlCommand();
                    ////SqlConnection sqlcon = new SqlConnection(con);
                    ////sqlcmd.CommandTimeout = 60;
                    ////sqlcon.Open();
                    ////sqlcmd = new SqlCommand("Sp_ApiShop_Activitysubmit", sqlcon);
                    ////sqlcmd.Parameters.Add("@session_token", model.session_token);
                    ////sqlcmd.Parameters.Add("@user_id", model.user_id);
                    ////sqlcmd.Parameters.Add("@JsonXML", JsonXML);
                    ////sqlcmd.CommandType = CommandType.StoredProcedure;
                    ////SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    ////da.Fill(dt);
                    ////sqlcon.Close();


                    ////if (dt.Rows.Count > 0)
                    ////{
                    ////    oview = APIHelperMethods.ToModelList<DatalistsSumission>(dt);
                    ////    omodel.status = "200";
                    ////    omodel.message = "Shop details successfully updated.";
                    ////    omodel.shop_list = oview;

                    ////    using (StreamWriter stream = new FileInfo((Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(uploadtext)))).AppendText())
                    ////    {
                    ////        stream.WriteLine("   Revisit  Start " + "Start Time:" + sdatetime + "End Time:" + DateTime.Now + "Status:Success(Shopsubmission/ShopVisited)[200]" + "User ID:" + model.user_id + svisitlog + "   Revisit  END ");
                    ////        //txt.Close();
                    ////    }
                    ////}

                    ////else
                    ////{
                    ////    omodel.status = "202";
                    ////    omodel.message = "Records not updated.";
                    ////    using (StreamWriter stream = new FileInfo((Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(uploadtext)))).AppendText())
                    ////    {
                    ////        stream.WriteLine("   Revisit  Start " + "Start Time:" + sdatetime + "End Time:" + DateTime.Now + "Status:Records not updated(Shopsubmission/ShopVisited)[202]" + "User ID:" + model.user_id +svisitlog+ "   Revisit  END ");
                    ////    }

                    ////          //txt.Close();
                    ////}


                    //oview = omed3;
                    //omodel.status = "200";
                    //omodel.message = "Shop details successfully updated.";
                    //omodel.shop_list = oview;
                    //var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                    //return message;

                    string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);
                    String dates = DateTime.Now.ToString("ddMMyyyyhhmmssffffff");
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcmd.CommandTimeout = 60;
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("Proc_ApiShop_Activitysubmit", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@session_token", model.session_token);
                    sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@isnewShop", model.isnewShop);
                    sqlcmd.Parameters.AddWithValue("@JsonXML", JsonXML);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0 && Convert.ToString(dt.Rows[0]["STRMESSAGE"]) == "Success")
                    {
                        oview = APIHelperMethods.ToModelList<DatalistsSumission>(dt);
                        omodel.status = "200";
                        omodel.message = "Shop details successfully updated.";
                        omodel.shop_list = oview;
                        //Rev 3.0 Mantis: 0025779
                        //if (logprint == "1")
                        //{
                        //    using (StreamWriter stream = new FileInfo((Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(uploadtext)))).AppendText())
                        //    {
                        //        stream.WriteLine("Revisit Start " + "Start Time:" + DateTime.Now + "End Time:" + DateTime.Now + "Status:Success(Shopsubmission/ShopVisited)[200]" + "User ID:" + model.user_id + svisitlog + " Revisit END");
                        //    }
                        //}
                        //End of Rev 3.0 Mantis: 0025779
                    }
                    else
                    {
                        omodel.status = "205";
                        omodel.message = "Records not updated.";
                        //Rev 3.0 Mantis: 0025779
                        //if (logprint == "1")
                        //{
                        //    using (StreamWriter stream = new FileInfo((Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(uploadtext)))).AppendText())
                        //    {
                        //        stream.WriteLine("Revisit Start " + "Start Time:" + DateTime.Now + "End Time:" + DateTime.Now + "Status:Fail(Shopsubmission/ShopVisited)[205]" + "User ID:" + model.user_id + svisitlog + " Revisit END");
                        //    }
                        //}
                        //End of Rev 3.0 Mantis: 0025779
                    }
                    var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                    return message;
                    //End of Rev 2.0 Mantis: 0025749
                }
            }
            catch (Exception ex)
            {
                omodel.status = "204";
                omodel.message = ex.Message;
                //Rev 3.0 Mantis: 0025779
                //if (logprint == "1")
                //{
                //    using (StreamWriter stream = new FileInfo((Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(uploadtext)))).AppendText())
                //    {
                //        stream.WriteLine("   Revisit  Start " + "Start Time:" + sdatetime + "End Time:" + DateTime.Now + "Status:Error(Shopsubmission/ShopVisited)[204]" + ex.Message + "User ID:" + model.user_id + svisitlog + "   Revisit  END ");
                //        //txt.Close();
                //    }
                //}
                //End of Rev 3.0 Mantis: 0025779
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        //Rev 2.0 Mantis: 0025749
        //public void InsertShopVisit(ShopsubmissionInput model, List<ShopsubmissionModel> omedl2)
        //{
        //    // String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];

        //    //Rev Debashis : Mantis:0025493
        //    //String folderLocation = System.Configuration.ConfigurationSettings.AppSettings["ShopSubmitXMLURL"];
        //    String folderLocation = System.Configuration.ConfigurationManager.AppSettings["ShopSubmitXMLURL"];
        //    //End of Rev Debashis : Mantis:0025493

        //    //string sessionId = "";

        //    string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);
        //    String dates = DateTime.Now.ToString("ddMMyyyyhhmmssffffff");
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        //Rev Debashis : Mantis:0025493
        //        //String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
        //        String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
        //        //End of Rev Debashis : Mantis:0025493
        //        SqlCommand sqlcmd = new SqlCommand();
        //        SqlConnection sqlcon = new SqlConnection(con);
        //        sqlcmd.CommandTimeout = 60;
        //        sqlcon.Open();
        //        sqlcmd = new SqlCommand("Proc_ApiShop_Activitysubmit", sqlcon);
        //        //sqlcmd = new SqlCommand("Sp_ApiShop_Activitysubmit", sqlcon);
        //        //Rev Debashis : Mantis:0025493
        //        //sqlcmd.Parameters.Add("@session_token", model.session_token);
        //        //sqlcmd.Parameters.Add("@user_id", model.user_id);
        //        //sqlcmd.Parameters.Add("@JsonXML", JsonXML);
        //        sqlcmd.Parameters.AddWithValue("session_token", model.session_token.ToString());
        //        sqlcmd.Parameters.AddWithValue("@user_id", model.user_id.ToString());
        //        //Rev Debashis : Mantis:0025529 & Row:779
        //        sqlcmd.Parameters.AddWithValue("@isnewShop", model.isnewShop);
        //        //End of Rev Debashis : Mantis:0025529 & Row:779
        //        sqlcmd.Parameters.AddWithValue("@JsonXML", JsonXML);
        //        //End of Rev Debashis : Mantis:0025493
        //        sqlcmd.CommandType = CommandType.StoredProcedure;
        //        SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
        //        da.Fill(dt);
        //        sqlcon.Close();
        //    }
        //    catch
        //    {
        //        //File.WriteAllText("\\\\10.0.8.251\\Location\\Processing\\" + model.user_id + "_" + dates + ".xml", JsonXML);
        //        return;
        //    }
        //    finally
        //    {

        //    }

        //    if (dt.Rows.Count > 0)
        //    {
        //        oview = APIHelperMethods.ToModelList<DatalistsSumission>(dt);
        //        omodel.status = "200";
        //        omodel.message = "Shop details successfully updated.";
        //        // omodel.shop_list = omedl2;
        //        if (logprint == "1")
        //        {
        //            using (StreamWriter stream = new FileInfo((Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(uploadtext)))).AppendText())
        //            {
        //                stream.WriteLine("Revisit  Start " + "Start Time:" + DateTime.Now + "End Time:" + DateTime.Now + "Status:Success(Shopsubmission/ShopVisited)[215]" + "User ID:" + model.user_id + svisitlog + "   Revisit  END ");
        //            }
        //        }
        //    }
        //    else
        //    {
        //        omodel.status = "202";
        //        omodel.message = "Records not updated.";
        //        if (logprint == "1")
        //        {
        //            using (StreamWriter stream = new FileInfo((Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(uploadtext)))).AppendText())
        //            {
        //                stream.WriteLine("Revisit  Start " + "Start Time:" + DateTime.Now + "End Time:" + DateTime.Now + "Status:Records not updated(Shopsubmission/ShopVisited)[216]" + "User ID:" + model.user_id + svisitlog + "   Revisit  END ");
        //            }
        //        }
        //        // File.WriteAllText("\\\\10.0.8.251\\Location\\Processing\\" + model.user_id + "_" + dates + ".xml", JsonXML);
        //        return;
        //        //txt.Close();
        //    }

        //    //if (Convert.ToInt32(model.user_id)<=1000)
        //    //{
        //    //    File.WriteAllText(folderLocation+"\\user100\\" + model.user_id + "_" + dates + ".xml", JsonXML);
        //    //}
        //    //else if (Convert.ToInt32(model.user_id)<=2000 && Convert.ToInt32(model.user_id)>1000)
        //    //{
        //    //    File.WriteAllText(folderLocation+"\\user200\\" + model.user_id + "_" + dates + ".xml", JsonXML);  
        //    //}
        //    //else if (Convert.ToInt32(model.user_id) <= 3000 && Convert.ToInt32(model.user_id) > 2000)
        //    //{
        //    //    File.WriteAllText(folderLocation + "\\user300\\" + model.user_id + "_" + dates + ".xml", JsonXML);
        //    //}
        //    //else if (Convert.ToInt32(model.user_id) <= 4000 && Convert.ToInt32(model.user_id) > 3000)
        //    //{
        //    //    File.WriteAllText(folderLocation + "\\user400\\" + model.user_id + "_" + dates + ".xml", JsonXML);
        //    //}
        //    //else if (Convert.ToInt32(model.user_id) <= 5000 && Convert.ToInt32(model.user_id) > 4000)
        //    //{
        //    //    File.WriteAllText(folderLocation + "\\user500\\" + model.user_id + "_" + dates + ".xml", JsonXML);
        //    //}
        //    //else if (Convert.ToInt32(model.user_id) <= 6000 && Convert.ToInt32(model.user_id) > 5000)
        //    //{
        //    //    File.WriteAllText(folderLocation + "\\user600\\" + model.user_id + "_" + dates + ".xml", JsonXML);
        //    //}
        //    //else if (Convert.ToInt32(model.user_id) > 6000)
        //    //{
        //    //    File.WriteAllText(folderLocation + "\\user700\\" + model.user_id + "_" + dates + ".xml", JsonXML);
        //    //}
        //}
        //End of Rev 2.0 Mantis: 0025749

        [HttpPost]
        public HttpResponseMessage MeetingVisited(MeetingVisitInput model)
        {
            MeetingVisitOutput omodel = new MeetingVisitOutput();
            string sdatetime = DateTime.Now.ToString();
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
                    //Rev Debashis : Mantis:0025493 
                    //String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                    String token = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];
                    //End of Rev Debashis : Mantis:0025493
                    List<MeetingVisitList> omedl2 = new List<MeetingVisitList>();
                    foreach (var s2 in model.meeting_list)
                    {
                        omedl2.Add(new MeetingVisitList()
                        {
                            remarks = s2.remarks,
                            latitude = s2.latitude,
                            longitude = s2.longitude,
                            duration = s2.duration,
                            meeting_type_id = s2.meeting_type_id,
                            address = s2.address,
                            pincode = s2.pincode,
                            distance_travelled = s2.distance_travelled,
                            date = s2.date,
                            date_time = s2.date_time
                        });
                    }

                    //del ldMainAct = new del(InsertShopVisit);
                    //ldMainAct.BeginInvoke(model, omedl2, null, null);


                    string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);
                    DataTable dt = new DataTable();
                    //Rev Debashis : Mantis:0025493
                    //String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    //End of Rev Debashis : Mantis:0025493
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcmd.CommandTimeout = 60;
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("Proc_ApiMetting_Activitysubmit", sqlcon);
                    //Rev Debashis : Mantis:0025493
                    //sqlcmd.Parameters.Add("@session_token", model.session_token);
                    //sqlcmd.Parameters.Add("@user_id", model.user_id);
                    //sqlcmd.Parameters.Add("@JsonXML", JsonXML);
                    sqlcmd.Parameters.AddWithValue("@session_token", model.session_token);
                    sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@JsonXML", JsonXML);
                    //End of Rev Debashis : Mantis:0025493

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        //oview = APIHelperMethods.ToModelList<DatalistsSumission>(dt);
                        omodel.status = "200";
                        omodel.message = "Successfully Meeting list updated.";
                    }
                    else
                    {
                        omodel.status = "202";
                        omodel.message = "Records not updated.";
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
        public HttpResponseMessage OrderNotTakenReason(OrderNotTakenInput model)
        {
            MeetingVisitOutput omodel = new MeetingVisitOutput();
            string sdatetime = DateTime.Now.ToString();
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
                    //Rev Debashis : Mantis:0025493
                    //String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                    String token = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];
                    //End of Rev Debashis : Mantis:0025493
                    List<OrderNotTakenList> omedl2 = new List<OrderNotTakenList>();
                    foreach (var s2 in model.ordernottaken_list)
                    {
                        omedl2.Add(new OrderNotTakenList()
                        {
                            shop_id = s2.shop_id,
                            order_status = s2.order_status,
                            order_remarks = s2.order_remarks,
                            shop_revisit_uniqKey = s2.shop_revisit_uniqKey
                        });
                    }

                    string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);
                    DataTable dt = new DataTable();
                    //Rev Debashis : Mantis:0025493
                    //String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    //End of Rev Debashis : Mantis:0025493
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcmd.CommandTimeout = 60;
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("Proc_ApiShop_OrderNotTakenReason", sqlcon);
                    //Rev Debashis : Mantis:0025493
                    //sqlcmd.Parameters.Add("@session_token", model.session_token);
                    //sqlcmd.Parameters.Add("@user_id", model.user_id);
                    //sqlcmd.Parameters.Add("@JsonXML", JsonXML);
                    sqlcmd.Parameters.AddWithValue("@session_token", model.session_token);
                    sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@JsonXML", JsonXML);
                    //End of Rev Debashis : Mantis:0025493

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Successfully reason updated.";
                    }
                    else
                    {
                        omodel.status = "202";
                        omodel.message = "Records not updated.";
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

        //Rev Debashis Row:748
        [HttpPost]
        public HttpResponseMessage ITCShopVisited(ITCShopsubmissionInput model)
        {
            ITCShopsubmissionOutput omodel = new ITCShopsubmissionOutput();
            List<ITCDatalistsSumission> oview = new List<ITCDatalistsSumission>();
            List<ITCShopsubmissionModel> omedl2 = new List<ITCShopsubmissionModel>();
            List<ITCDatalistsSumission> omed3 = new List<ITCDatalistsSumission>();

            string sdatetime = DateTime.Now.ToString();
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
                    //Rev Debashis : Mantis:0025493
                    //String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                    String token = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];
                    //End of Rev Debashis : Mantis:0025493
                    string sessionId = "";

                    foreach (var s2 in model.shop_list)
                    {
                        omedl2.Add(new ITCShopsubmissionModel()
                        {
                            shop_id = s2.shop_id,
                            spent_duration = s2.spent_duration,
                            visited_date = s2.visited_date,
                            visited_time = s2.visited_time,
                            total_visit_count = s2.total_visit_count,
                            distance_travelled = s2.distance_travelled,
                            user_id = model.user_id,
                            feedback = s2.feedback,
                            distanceFromHomeLoc = s2.distanceFromHomeLoc,
                            isFirstShopVisited = s2.isFirstShopVisited,
                            early_revisit_reason = s2.early_revisit_reason,
                            device_model = s2.device_model,
                            android_version = s2.android_version,
                            battery = s2.battery,
                            net_status = s2.net_status,
                            net_type = s2.net_type,
                            in_time = s2.in_time,
                            out_time = s2.out_time,
                            start_timestamp = s2.start_timestamp,
                            in_location = s2.in_location,
                            out_location = s2.out_location,
                            shop_revisit_uniqKey = s2.shop_revisit_uniqKey,
                            pros_id = s2.pros_id,
                            updated_by = s2.updated_by,
                            updated_on = s2.updated_on,
                            agency_name = s2.agency_name,
                            approximate_1st_billing_value = s2.approximate_1st_billing_value,
                            IsShopUpdate = s2.IsShopUpdate,
                            //Rev 6.0 Row: 855
                            isNewShop= s2.isNewShop,
                            //End of Rev 6.0 Row: 855
                            //Rev 8.0 Row: 915 & Refer: 0027335
                            shop_lat = s2.shop_lat,
                            shop_long= s2.shop_long,
                            shop_addr= s2.shop_addr
                            //End of Rev 8.0 Row: 915 & Refer: 0027335
                        });
                    }

                    foreach (var s2 in model.shop_list)
                    {
                        omed3.Add(new ITCDatalistsSumission()
                        {
                            shopid = s2.shop_id,
                            spent_duration = s2.spent_duration,
                            visited_date = Convert.ToDateTime(s2.visited_time),
                            visited_time = Convert.ToDateTime(s2.visited_time),
                            total_visit_count = s2.total_visit_count,
                            distance_travelled = s2.distance_travelled,
                            IsShopUpdate = s2.IsShopUpdate
                        });

                        svisitlog = svisitlog + " Shop:" + s2.shop_id + "  Visit:" + s2.visited_time;
                    }
                    //Rev 3.0 Mantis: 0025779
                    //if (logprint == "1")
                    //{
                    //    using (StreamWriter stream = new FileInfo((Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(uploadtext)))).AppendText())
                    //    {
                    //        stream.WriteLine("Revisit Start " + "Start Time:" + DateTime.Now + "End Time:" + DateTime.Now + "Status:Success(Shopsubmission/ShopVisited)[200]" + "User ID:" + model.user_id + svisitlog + " Revisit END ");
                    //    }
                    //}
                    //End of Rev 3.0 Mantis: 0025779
                    string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);
                    String dates = DateTime.Now.ToString("ddMMyyyyhhmmssffffff");
                    DataTable dt = new DataTable();
                    //Rev Debashis : Mantis:0025493
                    //String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    //End of Rev Debashis : Mantis:0025493
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcmd.CommandTimeout = 60;
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APIITCSHOPSUBMITACTIVITY", sqlcon);
                    //Rev Debashis : Mantis:0025493
                    //sqlcmd.Parameters.Add("@session_token", model.session_token);
                    //sqlcmd.Parameters.Add("@user_id", model.user_id);
                    //sqlcmd.Parameters.Add("@JsonXML", JsonXML);
                    sqlcmd.Parameters.AddWithValue("@session_token", model.session_token);
                    sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@JsonXML", JsonXML);
                    //End of Rev Debashis : Mantis:0025493

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0 && Convert.ToString(dt.Rows[0]["STRMESSAGE"]) == "Success")
                    {
                        oview = APIHelperMethods.ToModelList<ITCDatalistsSumission>(dt);
                        omodel.status = "200";
                        omodel.message = "Shop details successfully updated.";
                        omodel.shop_list = oview;
                        //Rev 3.0 Mantis: 0025779
                        //if (logprint == "1")
                        //{
                        //    using (StreamWriter stream = new FileInfo((Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(uploadtext)))).AppendText())
                        //    {
                        //        stream.WriteLine("Revisit Start " + "Start Time:" + DateTime.Now + "End Time:" + DateTime.Now + "Status:Success(Shopsubmission/ShopVisited)[200]" + "User ID:" + model.user_id + svisitlog + " Revisit END");
                        //    }
                        //}
                        //End of Rev 3.0 Mantis: 0025779
                    }
                    //Rev 5.0 Mantis: 0026359
                    else if (dt.Rows.Count > 0 && Convert.ToString(dt.Rows[0]["STRMESSAGE"]) == "Updated")
                    {
                        //Rev 7.0 Row: 859
                        oview = APIHelperMethods.ToModelList<ITCDatalistsSumission>(dt);
                        //End of Rev 7.0 Row: 859
                        omodel.status = "200";
                        omodel.message = "Shop details successfully updated.";
                        //Rev 7.0 Row: 859
                        omodel.shop_list = oview;
                        //End of Rev 7.0 Row: 859
                    }
                    //End of Rev 5.0 Mantis: 0026359
                    else
                    {
                        omodel.status = "205";
                        omodel.message = "Records not updated.";
                        //Rev 3.0 Mantis: 0025779
                        //if (logprint == "1")
                        //{
                        //    using (StreamWriter stream = new FileInfo((Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(uploadtext)))).AppendText())
                        //    {
                        //        stream.WriteLine("Revisit Start " + "Start Time:" + DateTime.Now + "End Time:" + DateTime.Now + "Status:Fail(Shopsubmission/ShopVisited)[205]" + "User ID:" + model.user_id + svisitlog + " Revisit END");
                        //    }
                        //}
                        //End of Rev 3.0 Mantis: 0025779
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
        //End of Rev Debashis Row:748
    }
}

#region======================================Revision History=========================================================
//1.0   V2.0.41     Debashis    18/07/2023      Some new parameters have been added.Row: 857 & Refer: 0026547
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
    public class AlarmConfigController : ApiController
    {
        public HttpResponseMessage Configuration(AlarmconfigutaionModelInput model)
        {

            AlarmconfigutaionModelOutput oview = new AlarmconfigutaionModelOutput();

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
                    String token = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];
                    String profileImg = System.Configuration.ConfigurationManager.AppSettings["ProfileImageURL"];

                    DataSet dt = new DataSet();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("Proc_AlarmConfiguration", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Tables[0].Rows.Count > 0)
                    {
                        //  oview.visit_type = "";
                        List<AlarmconfigutaionModel> onview = new List<AlarmconfigutaionModel>();
                        for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                        {
                            onview.Add(new AlarmconfigutaionModel()
                            {
                                id = Convert.ToInt32(dt.Tables[0].Rows[i]["id"]),
                                alarm_time_hours = Convert.ToString(dt.Tables[0].Rows[i]["alarm_time_hours"]),
                                alarm_time_mins = Convert.ToString(dt.Tables[0].Rows[i]["alarm_time_mins"]),
                                report_id = Convert.ToInt32(dt.Tables[0].Rows[i]["report_id"]),
                                report_title = Convert.ToString(dt.Tables[0].Rows[i]["report_title"]),
                            });
                        }

                        oview.status = "200";
                        oview.message = "List populated.";
                        oview.alarm_settings_list = onview;
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
        public HttpResponseMessage FetchAattendance(AlermAttendanceInput model)
        {

            try
            {
                AlermAttendanceOutput odata = new AlermAttendanceOutput();

                if (!ModelState.IsValid)
                {
                    odata.status = "213";
                    odata.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
                }
                else
                {

                    String token = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];
                    string sessionId = "";


                    DataSet ds = new DataSet();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();



                    sqlcmd = new SqlCommand("PRC_FTSAPIEMPLOYEEATTENDANCE_REPORT", sqlcon);

                    sqlcmd.Parameters.AddWithValue("@USERID", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@ASONDATE", model.date);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(ds);
                    sqlcon.Close();


                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        List<AlermAttendance> oview = new List<AlermAttendance>();

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {


                            oview.Add(new AlermAttendance()
                            {

                                member_name = Convert.ToString(ds.Tables[0].Rows[i]["EMPNAME"]),
                                member_id = Convert.ToString(ds.Tables[0].Rows[i]["EMPID"]),
                                status = Convert.ToString(ds.Tables[0].Rows[i]["ATTEN_STATUS"]),
                                report_to = Convert.ToString(ds.Tables[0].Rows[i]["REPORTTO"]),
                                login_time = Convert.ToString(ds.Tables[0].Rows[i]["LOGGEDIN"]),
                                contact_no = Convert.ToString(ds.Tables[0].Rows[i]["CONTACTNO"])
                            });


                        }


                        odata.attendance_report_list = oview;
                        odata.status = "200";
                        odata.message = "Attendance Report List";

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
        public HttpResponseMessage FetchPerformance(AlarmperformanceInput model)
        {
            try
            {
                AlarmperformanceOutput odata = new AlarmperformanceOutput();

                if (!ModelState.IsValid)
                {
                    odata.status = "213";
                    odata.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
                }
                else
                {
                    String token = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];
                    string sessionId = "";

                    DataSet ds = new DataSet();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();

                    sqlcmd = new SqlCommand("PRC_FTSAPIEMPLOYEEPERFORMANCE_REPORT", sqlcon);

                    sqlcmd.Parameters.AddWithValue("@USERID", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@FROMDATE", model.from_date);
                    sqlcmd.Parameters.AddWithValue("@TODATE", model.to_date);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(ds);
                    sqlcon.Close();

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        List<Alarmperformance> oview = new List<Alarmperformance>();

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            oview.Add(new Alarmperformance()
                            {
                                member_name = Convert.ToString(ds.Tables[0].Rows[i]["EMPNAME"]),
                                member_id = Convert.ToString(ds.Tables[0].Rows[i]["EMPCODE"]),
                                total_shop_count = Convert.ToString(ds.Tables[0].Rows[i]["TOTAL_VISIT"]),
                                total_travel_distance = Convert.ToString(ds.Tables[0].Rows[i]["DISTANCE_TRAVELLED"]),
                                report_to = Convert.ToString(ds.Tables[0].Rows[i]["REPORTTO"]),
                                order_vale = Convert.ToString(ds.Tables[0].Rows[i]["Total_Order_Booked_Value"]),
                                collection_value = Convert.ToString(ds.Tables[0].Rows[i]["Total_Collection"]),
                                //Rev 1.0 Row: 857 & Mantis: 0026547
                                user_id = Convert.ToString(ds.Tables[0].Rows[i]["user_id"]),
                                attendance_present_count = Convert.ToInt32(ds.Tables[0].Rows[i]["attendance_present_count"]),
                                attendance_absent_count = Convert.ToInt32(ds.Tables[0].Rows[i]["attendance_absent_count"]),
                                visit_inactivity_party_count = Convert.ToInt32(ds.Tables[0].Rows[i]["visit_inactivity_party_count"]),
                                order_inactivity_party_count = Convert.ToInt32(ds.Tables[0].Rows[i]["order_inactivity_party_count"]),
                                last_visited_date = Convert.ToString(ds.Tables[0].Rows[i]["last_visited_date"]),
                                last_order_date = Convert.ToString(ds.Tables[0].Rows[i]["last_order_date"])
                                //End of Rev 1.0 Row: 857 & Mantis: 0026547
                            });
                        }
                        odata.performance_report_list = oview;
                        odata.status = "200";
                        odata.message = "Preformnace Report List";
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
        public HttpResponseMessage FetchVist(AlarmperformanceInput model)
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

                    String token = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];
                    string sessionId = "";


                    DataSet ds = new DataSet();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();



                    sqlcmd = new SqlCommand("PRC_FTSAPIEMPLOYEEVISIT_REPORT", sqlcon);

                    sqlcmd.Parameters.AddWithValue("@USERID", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@FROMDATE", model.from_date);
                    sqlcmd.Parameters.AddWithValue("@TODATE", model.to_date);


                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(ds);
                    sqlcon.Close();


                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        List<AlarmShopvisit> oview = new List<AlarmShopvisit>();

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {

                            List<Shopvisitalarm> onview = new List<Shopvisitalarm>();

                            for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                            {

                                if (Convert.ToString(ds.Tables[1].Rows[j]["EMPCODE"]) == Convert.ToString(ds.Tables[0].Rows[i]["EMPCODE"]))
                                {
                                    onview.Add(new Shopvisitalarm()
                                    {

                                        shop_name = Convert.ToString(ds.Tables[1].Rows[j]["SHOP_NAME"]),
                                        visit_time = Convert.ToString(ds.Tables[1].Rows[j]["VISITED_TIME"]),
                                        duration_spent = Convert.ToString(ds.Tables[1].Rows[j]["SPENT_DURATION"]),
                                        distance = Convert.ToString(ds.Tables[1].Rows[j]["DISTANCE_TRAVELLED"]),
                                        //Rev Debashis
                                        date = Convert.ToString(ds.Tables[1].Rows[j]["VISITED_DATEORDBY"]),
                                        //End of Rev Debashis
                                        //Rev Debashis Row 728
                                        beat_id = Convert.ToString(ds.Tables[1].Rows[j]["beat_id"]),
                                        beat_name = Convert.ToString(ds.Tables[1].Rows[j]["beat_name"]),
                                        visit_status = Convert.ToString(ds.Tables[1].Rows[j]["visit_status"])
                                        //End of Rev Debashis Row 728
                                    });

                                }
                            }

                            oview.Add(new AlarmShopvisit()
                            {

                                visit_details_list = onview,
                                //Rev Debashis Row 728
                                user_id = Convert.ToString(ds.Tables[0].Rows[i]["EMPUSRID"]),
                                //End of Rev Debashis Row 728
                                member_name = Convert.ToString(ds.Tables[0].Rows[i]["EMPNAME"]),
                                member_id = Convert.ToString(ds.Tables[0].Rows[i]["EMPCODE"]),
                                total_shop_count = Convert.ToString(ds.Tables[0].Rows[i]["TOTAL_VISIT"]),
                                report_to = Convert.ToString(ds.Tables[0].Rows[i]["REPORTTO"]),
                                total_distance_travelled = Convert.ToString(ds.Tables[0].Rows[i]["DISTANCE_TRAVELLED"])
                               
                            });


                        }


                        odata.visit_report_list = oview;
                        odata.status = "200";
                        odata.message = "Visit Report List";

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
        public HttpResponseMessage ReviewConfirm(ReviewConfirmmodel model)
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

                    String token = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];
                    string sessionId = "";


                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();



                    sqlcmd = new SqlCommand("Proc_ReportConfirm", sqlcon);

                    sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@report_time", model.report_time);
                    sqlcmd.Parameters.AddWithValue("@view_time", model.view_time);
                    sqlcmd.Parameters.AddWithValue("@alarm_id", model.alarm_id);
                    sqlcmd.Parameters.AddWithValue("@report_id", model.report_id);


                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();


                    if (dt.Rows.Count > 0)
                    {
     
                        odata.status = "200";
                        odata.message = "Report review confirmed successfully.";

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

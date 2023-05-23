#region======================================Revision History=========================================================
//1.0   V2.0.38     Debashis    24/01/2023      A new parameter has been added.Row: 807
//2.0   V2.0.39     Debashis    17/05/2023      Two new parameters have been added.Row: 840
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
    public class ShopAttendanceController : ApiController
    {

        [HttpPost]

        public HttpResponseMessage AddAttendance(AttendancemanageInput model)
        {
            AttendancemanageOutput omodel = new AttendancemanageOutput();
            UserClass oview = new UserClass();


            try
            {
                string token = string.Empty;
                string versionname = string.Empty;
                System.Net.Http.Headers.HttpRequestHeaders headers = this.Request.Headers;
                String tokenmatch = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];
                //if (headers.Contains("version_name"))
                //{
                //    versionname = headers.GetValues("version_name").First();
                //}
                //if (headers.Contains("token_Number"))
                //{
                //    token = headers.GetValues("token_Number").First();
                //}

                //if (token == tokenmatch)
                //{
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

                        List<shopAttendance> omedl2 = new List<shopAttendance>();

                        foreach (var s2 in model.shop_list)
                        {

                            omedl2.Add(new shopAttendance()
                            {

                                route = s2.route,
                                shop_id = s2.shop_id,


                            });

                        }


                        string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);



                        DataTable dt = new DataTable();

                        String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                        //String con = Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]);

                        SqlCommand sqlcmd = new SqlCommand();
                        SqlConnection sqlcon = new SqlConnection(con);
                        sqlcon.Open();

                        sqlcmd = new SqlCommand("Proc_FTS_Attendancesubmit", sqlcon);


                        sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);
                        sqlcmd.Parameters.AddWithValue("@SessionToken", model.session_token);
                        sqlcmd.Parameters.AddWithValue("@wtype", model.work_type);
                        sqlcmd.Parameters.AddWithValue("@wdesc", model.work_desc);
                        sqlcmd.Parameters.AddWithValue("@wlatitude", model.work_lat);
                        sqlcmd.Parameters.AddWithValue("@wlongitude", model.work_long);
                        sqlcmd.Parameters.AddWithValue("@Waddress", model.work_address);
                        sqlcmd.Parameters.AddWithValue("@Wdatetime", model.work_date_time);
                        sqlcmd.Parameters.AddWithValue("@Isonleave", model.is_on_leave);
                        sqlcmd.Parameters.AddWithValue("@add_attendence_time", model.add_attendence_time);
                        sqlcmd.Parameters.AddWithValue("@RouteID", model.route);
                        sqlcmd.Parameters.AddWithValue("@ShopList_List", JsonXML);
                        sqlcmd.Parameters.AddWithValue("@leave_from_date", model.leave_from_date);
                        sqlcmd.Parameters.AddWithValue("@leave_type", model.leave_type);
                        sqlcmd.Parameters.AddWithValue("@leave_to_date", model.leave_to_date);                        
                        
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                        da.Fill(dt);
                        sqlcon.Close();
                        if (dt!=null && dt.Rows.Count > 0)
                        {
                          
                                omodel.status = "200";
                                omodel.message = "Attendence successfully submitted.";
                           

                        }
                        else
                        {

                            omodel.status = "202";
                            omodel.message = "Invalid user credential.";

                        }

                        var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                        return message;
                    }
                //}



                //else
                //{
                //    omodel.status = "205";
                //    omodel.message = "Token Id does not matched.";
                //    var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                //    return message;

                //}

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
        public HttpResponseMessage AttendanceSubmit(AttendancemanageInput model)
        {
            AttendancemanageOutput omodel = new AttendancemanageOutput();
            UserClass oview = new UserClass();

            try
            {
                string token = string.Empty;
                string versionname = string.Empty;
                System.Net.Http.Headers.HttpRequestHeaders headers = this.Request.Headers;
                String tokenmatch = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];
                
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

                    List<shopAttendance> omedl2 = new List<shopAttendance>();
                    foreach (var s2 in model.shop_list)
                    {
                        omedl2.Add(new shopAttendance()
                        {
                            route = s2.route,
                            shop_id = s2.shop_id,
                        });
                    }
                    List<StatewiseTraget> omedelstatetarget = new List<StatewiseTraget>();

                    if (model.primary_value_list != null)
                    {
                        foreach (var s2 in model.primary_value_list)
                        {
                            omedelstatetarget.Add(new StatewiseTraget()
                            {
                                id = s2.id,
                                primary_value = s2.primary_value,
                            });
                        }

                    }
                    string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);
                    string JsonTargetXML = XmlConversion.ConvertToXml(omedelstatetarget, 0);

                    //Rev Tanmoy 23-12-2019 for fund plan
                    DataTable fundplandt = new DataTable();
                    //if (fundplandt == null)
                    //{
                        fundplandt.Columns.Add("PLAN_ID", typeof(long));
                        fundplandt.Columns.Add("PLAN_AMT", typeof(decimal));
                        fundplandt.Columns.Add("PLAN_DATE", typeof(DateTime));
                        fundplandt.Columns.Add("PLAN_REMARKS", typeof(string));
                        fundplandt.Columns.Add("ACHIV_AMT", typeof(decimal));
                       // fundplandt.Columns.Add("ACHIV_DATE", typeof(DateTime));
                        fundplandt.Columns.Add("ACHIV_REMARKS", typeof(string));
                    //}

                    if (model.Update_Plan_List != null && model.Update_Plan_List.Count>0)
                    {
                        DateTime? ACHIV_DATE = null;
                        foreach (var s2 in model.Update_Plan_List)
                        {
                            //if (s2.achievement_value!=null && s2.achievement_value>0)
                            //{
                            //    ACHIV_DATE = s2.plan_date;
                            //}
                            fundplandt.Rows.Add(s2.plan_id, s2.plan_value, s2.plan_date, s2.plan_remarks, s2.achievement_value, s2.acheivement_remarks);// s2.achievement_date,
                        }
                        model.IsNoPlanUpdate = "1";
                    }
                    else
                    {
                        model.IsNoPlanUpdate = "0";
                    }

                //  string JsonFoundPlanXML = XmlConversion.ConvertToXml(model.Update_Plan_List, 0);

                  //  Rev end Tanmoy 23-12-2019 for fund plan

                    DataTable dt = new DataTable();

                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    //String con = Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]);

                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();

                    sqlcmd = new SqlCommand("Proc_FTS_Attendancesubmit", sqlcon);


                    sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@SessionToken", model.session_token);
                    sqlcmd.Parameters.AddWithValue("@wtype", model.work_type);
                    sqlcmd.Parameters.AddWithValue("@wdesc", model.work_desc);
                    sqlcmd.Parameters.AddWithValue("@wlatitude", model.work_lat);
                    sqlcmd.Parameters.AddWithValue("@wlongitude", model.work_long);
                    sqlcmd.Parameters.AddWithValue("@Waddress", model.work_address);
                    sqlcmd.Parameters.AddWithValue("@Wdatetime", model.work_date_time);
                    sqlcmd.Parameters.AddWithValue("@Isonleave", model.is_on_leave);
                    sqlcmd.Parameters.AddWithValue("@add_attendence_time", model.add_attendence_time);
                    sqlcmd.Parameters.AddWithValue("@RouteID", model.route);
                    sqlcmd.Parameters.AddWithValue("@ShopList_List", JsonXML);
                    sqlcmd.Parameters.AddWithValue("@Target_List", JsonTargetXML);
                    sqlcmd.Parameters.AddWithValue("@leave_from_date", model.leave_from_date);
                    sqlcmd.Parameters.AddWithValue("@leave_type", model.leave_type);
                    sqlcmd.Parameters.AddWithValue("@leave_to_date", model.leave_to_date);
                    sqlcmd.Parameters.AddWithValue("@order_taken", model.order_taken);
                    sqlcmd.Parameters.AddWithValue("@collection_taken", model.collection_taken);
                    sqlcmd.Parameters.AddWithValue("@new_shop_visit", model.new_shop_visit);
                    sqlcmd.Parameters.AddWithValue("@revisit_shop", model.revisit_shop);
                    sqlcmd.Parameters.AddWithValue("@state_id", model.state_id);
                    //Rev Tanmoy 30-10-2019 Add two column in loginlogout table
                    sqlcmd.Parameters.AddWithValue("@Distributor_Name", model.Distributor_Name);
                    sqlcmd.Parameters.AddWithValue("@Market_Worked", model.Market_Worked);
                    //Rev end Tanmoy 30-10-2019 Add two column in loginlogout table

                    //Rev Tanmoy 23-12-2019 send udt for fund plan
                    SqlParameter paramProd = sqlcmd.Parameters.AddWithValue("@FUNDPLAN", SqlDbType.Structured);
                    paramProd.Value = fundplandt;
                    //Rev End Tanmoy 23-12-2019 send udt for fund plan

                    //Rev Tanmoy 23-12-2019 send udt for fund plan
                    sqlcmd.Parameters.AddWithValue("@IsNoPlanUpdate", model.IsNoPlanUpdate);
                    //Rev End Tanmoy 23-12-2019 send udt for fund plan

                    //Rev Tanmoy 13-02-2020 for leave reason
                    sqlcmd.Parameters.AddWithValue("@leave_reason", model.leave_reason);
                    //Rev End Tanmoy 13-02-2020 for leave reason

                    //Rev Tanmoy 04-12-2020 for from_Areaid,to_Areaid,distance
                    sqlcmd.Parameters.AddWithValue("@from_Areaid", model.from_id);
                    sqlcmd.Parameters.AddWithValue("@to_Areaid", model.to_id);
                    sqlcmd.Parameters.AddWithValue("@distance", model.distance);
                    //Rev End Tanmoy 04-12-2020 for from_Areaid,to_Areaid,distance
                    //Rev Debashis Row 725
                    sqlcmd.Parameters.AddWithValue("@beat_id", model.beat_id);
                    //End of Rev Debashis Row 725
                    //Rev Debashis Row 776
                    sqlcmd.Parameters.AddWithValue("@IsJointVisit", model.IsJointVisit);
                    sqlcmd.Parameters.AddWithValue("@JointVisitTeam_MemberName", model.JointVisitTeam_MemberName);
                    sqlcmd.Parameters.AddWithValue("@JointVisitTeam_Member_User_ID", model.JointVisitTeam_Member_User_ID);
                    //End of Rev Debashis Row 776
                    //Rev 1.0 Row: 807
                    sqlcmd.Parameters.AddWithValue("@IsDistributorwiseNearbyShopVisit", model.IsDistributorwiseNearbyShopVisit);
                    //End of Rev 1.0 Row: 807
                    //Rev 2.0 Row: 840
                    sqlcmd.Parameters.AddWithValue("@VISIT_LOCATION_ID", model.visit_location_id);
                    sqlcmd.Parameters.AddWithValue("@AREA_LOCATION_ID", model.area_location_id);
                    //End of Rev 2.0 Row: 840

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (Convert.ToString(dt.Rows[0][0]) == "Attendence successfully submitted.")
                        {
                            omodel.status = "200";
                            omodel.message = "Attendence successfully submitted.";
                        }
                        else
                        {
                            omodel.status = "201";
                            omodel.message = Convert.ToString(dt.Rows[0][0]);
                        }

                    }
                    else
                    {
                        omodel.status = "202";
                        omodel.message = "Invalid user credential.";
                    }

                    var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                    return message;
                }
                //}



                //else
                //{
                //    omodel.status = "205";
                //    omodel.message = "Token Id does not matched.";
                //    var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                //    return message;

                //}

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

        public HttpResponseMessage BeatDetailList(BeatDetListInput model)
        {
            BeatDetListOutput omodel = new BeatDetListOutput();
            try
            {
                System.Net.Http.Headers.HttpRequestHeaders headers = this.Request.Headers;
                String tokenmatch = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];
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
                    //String con = Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]);

                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();

                    sqlcmd = new SqlCommand("PRC_APIBEATDETAILSINFO", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "BEATDETLIST");
                    sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@SessionToken", model.session_token);
                    sqlcmd.Parameters.AddWithValue("@BEAT_DATE", model.beat_date);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Successfully Get Beat Name.";
                        omodel.beat_id = Convert.ToString(dt.Rows[0]["beat_id"]);
                        omodel.beat_name = Convert.ToString(dt.Rows[0]["beat_name"]);
                    }
                    else
                    {
                        omodel.status = "202";
                        omodel.message = "No Data Found.";
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

        public HttpResponseMessage UpdateBeat(UpdateBeatInput model)
        {
            UpdateBeatOutput omodel = new UpdateBeatOutput();
            try
            {
                System.Net.Http.Headers.HttpRequestHeaders headers = this.Request.Headers;
                String tokenmatch = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];
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
                    //String con = Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]);

                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();

                    sqlcmd = new SqlCommand("PRC_APIBEATDETAILSINFO", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "UPDATEBEAT");
                    sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@BEAT_ID", model.updating_beat_id);
                    sqlcmd.Parameters.AddWithValue("@BEAT_DATE", model.updating_date);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Updated Successfully.";
                        omodel.updated_beat_id = Convert.ToString(dt.Rows[0]["updated_beat_id"]);
                        omodel.beat_name = Convert.ToString(dt.Rows[0]["beat_name"]);
                    }
                    else
                    {
                        omodel.status = "202";
                        omodel.message = "No Data Found.";
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
    }
}

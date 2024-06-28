#region======================================Revision History=========================================================
//1.0   V2.0.40     Debashis    30/06/2023      One new method has been added.Row: 854
//2.0   V2.0.47     Debashis    06/06/2024      Some new parameters have been added.Row: 941
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
    public class UserWiseDayStartEndController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage UserDayStartEnd(UserWiseDayStartEndInput model)
        {
            UserWiseDayStartEndOutput odata = new UserWiseDayStartEndOutput();

            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
            }
            else
            {
                String token = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_APIUSERWISEDAYSTARTEND", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "INSERTDATA");
                sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                sqlcmd.Parameters.AddWithValue("@STARTENDDATE", model.date);
                sqlcmd.Parameters.AddWithValue("@LOCATION_NAME", model.location_name);
                sqlcmd.Parameters.AddWithValue("@LATITUDE", model.latitude);
                sqlcmd.Parameters.AddWithValue("@LONGITUDE", model.longitude);
                sqlcmd.Parameters.AddWithValue("@SHOP_TYPE", model.shop_type);
                sqlcmd.Parameters.AddWithValue("@SHOP_ID", model.shop_id);
                sqlcmd.Parameters.AddWithValue("@ISSTART", model.isStart);
                sqlcmd.Parameters.AddWithValue("@ISEND", model.isEnd);
                sqlcmd.Parameters.AddWithValue("@SALE_VALUE", model.sale_Value);
                sqlcmd.Parameters.AddWithValue("@REMARKS", model.remarks);
                sqlcmd.Parameters.AddWithValue("@VISITDDID", model.visit_distributor_id);
                sqlcmd.Parameters.AddWithValue("@VISITDDNAME", model.visit_distributor_name);
                sqlcmd.Parameters.AddWithValue("@VISITDDDATE", model.visit_distributor_date_time);
                sqlcmd.Parameters.AddWithValue("@ISDDVISTEDONCEBYDAY", model.IsDDvistedOnceByDay);
                //Rev 2.0 Row: 941
                sqlcmd.Parameters.AddWithValue("@WORKACTIVITYID", model.attendance_worktype_id);
                sqlcmd.Parameters.AddWithValue("@WORKACTIVITYDESCRIPTION", model.attendance_worktype_name);
                //End of Rev 2.0 Row: 941
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    odata.status = "200";
                    odata.message = "Success day start and day end.";
                    odata.isStart = Convert.ToInt32(dt.Rows[0]["ISSTART"].ToString());
                }
                else
                {
                    odata.status = "205";
                    odata.message = "No data found.";
                }
                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage UserStatusDayStartEnd(UserWiseStatusDayStartEndInput model)
        {
            UserWiseStatusDayStartEndOutput odata = new UserWiseStatusDayStartEndOutput();

            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
            }
            else
            {
                String token = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_APIUSERWISEDAYSTARTEND", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "FETCHDATA");
                sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                sqlcmd.Parameters.AddWithValue("@STARTENDDATE", model.date);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    odata.status = "200";
                    odata.message = "Success day start and day end.";
                    odata.DayStartMarked = Convert.ToBoolean(dt.Rows[0]["DayStartMarked"].ToString());
                    odata.DayEndMarked = Convert.ToBoolean(dt.Rows[0]["DayEndMarked"].ToString());
                    odata.day_start_shop_type = Convert.ToString(dt.Rows[0]["day_start_shop_type"].ToString());
                    odata.day_start_shop_id = Convert.ToString(dt.Rows[0]["day_start_shop_id"].ToString());
                    odata.IsDDvistedOnceByDay = Convert.ToBoolean(dt.Rows[0]["IsDDvistedOnceByDay"].ToString());
                }
                else
                {
                    odata.status = "205";
                    odata.message = "No data found.";
                }
                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage UserDayStartEndList(UserDayStartEndListInput model)
        {
            UserDayStartEndListOutput omodel = new UserDayStartEndListOutput();
            List<DayStEndRecords> oview = new List<DayStEndRecords>();
            
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
                sqlcmd = new SqlCommand("PRC_APIUSERWISEDAYSTARTEND", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "DAYSTARTENDLIST");
                sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                sqlcmd.Parameters.AddWithValue("@START_DATE", model.start_date);
                sqlcmd.Parameters.AddWithValue("@END_DATE", model.end_date);

                sqlcmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<DayStEndRecords>(dt);
                    omodel.status = "200";
                    omodel.message = "Day Start End list for last 15 days / start day to end date";
                    omodel.user_id = dt.Rows[0]["User_Id"].ToString();
                    omodel.user_name = dt.Rows[0]["user_name"].ToString();
                    omodel.day_start_end_list = oview;
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

        //Rev Debashis Row: 736
        [HttpPost]
        public HttpResponseMessage UserDayStartEndDelete(UserDayStartEndDeleteInput model)
        {
            UserDayStartEndDeleteOutput omodel = new UserDayStartEndDeleteOutput();

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
                sqlcmd = new SqlCommand("PRC_APIDELETEDAYSTARTENDINFO", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "DELETEDAYSTARTENDATTENDANCE");
                sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                sqlcmd.Parameters.AddWithValue("@DATE", model.date);
                sqlcmd.Parameters.AddWithValue("@ISDAYSTARTDEL", model.isdaystartdel);
                sqlcmd.Parameters.AddWithValue("@ISDAYENDDEL", model.isdayenddel);

                sqlcmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Delete Successfully.";
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
        //End of Rev Debashis Row: 736
        //Rev 1.0 Row: 854
        [HttpPost]
        public HttpResponseMessage UserAttendanceSummary(UserAttendanceSummaryInput model)
        {
            UserAttendanceSummaryOutput odata = new UserAttendanceSummaryOutput();

            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
            }
            else
            {
                String token = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_APIUSERWISEDAYSTARTEND", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "USERATTENDANCESUMMARY");
                sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    odata.status = "200";
                    odata.message = "Success.";
                    odata.total_work_day = Convert.ToInt16(dt.Rows[0]["total_work_day"].ToString());
                    odata.total_present_day = Convert.ToInt16(dt.Rows[0]["total_present_day"].ToString());
                    odata.total_absent_day = Convert.ToInt16(dt.Rows[0]["total_absent_day"].ToString());
                    odata.total_qualified_day = Convert.ToInt16(dt.Rows[0]["total_qualified_day"].ToString());
                }
                else
                {
                    odata.status = "205";
                    odata.message = "No data found.";
                }
                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }
        }
        //End of Rev 1.0 Row: 854
    }
}

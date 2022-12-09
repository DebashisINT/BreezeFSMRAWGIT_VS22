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
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_APIUSERWISEDAYSTARTEND", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "INSERTDATA");
                sqlcmd.Parameters.Add("@USER_ID", model.user_id);
                sqlcmd.Parameters.Add("@STARTENDDATE", model.date);
                sqlcmd.Parameters.Add("@LOCATION_NAME", model.location_name);
                sqlcmd.Parameters.Add("@LATITUDE", model.latitude);
                sqlcmd.Parameters.Add("@LONGITUDE", model.longitude);
                sqlcmd.Parameters.Add("@SHOP_TYPE", model.shop_type);
                sqlcmd.Parameters.Add("@SHOP_ID", model.shop_id);
                sqlcmd.Parameters.Add("@ISSTART", model.isStart);
                sqlcmd.Parameters.Add("@ISEND", model.isEnd);
                sqlcmd.Parameters.Add("@SALE_VALUE", model.sale_Value);
                sqlcmd.Parameters.Add("@REMARKS", model.remarks);
                sqlcmd.Parameters.Add("@VISITDDID", model.visit_distributor_id);
                sqlcmd.Parameters.Add("@VISITDDNAME", model.visit_distributor_name);
                sqlcmd.Parameters.Add("@VISITDDDATE", model.visit_distributor_date_time);
                sqlcmd.Parameters.Add("@ISDDVISTEDONCEBYDAY", model.IsDDvistedOnceByDay);
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
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_APIUSERWISEDAYSTARTEND", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "FETCHDATA");
                sqlcmd.Parameters.Add("@USER_ID", model.user_id);
                sqlcmd.Parameters.Add("@STARTENDDATE", model.date);
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
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];

                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_APIUSERWISEDAYSTARTEND", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "DAYSTARTENDLIST");
                sqlcmd.Parameters.Add("@USER_ID", model.user_id);
                sqlcmd.Parameters.Add("@START_DATE", model.start_date);
                sqlcmd.Parameters.Add("@END_DATE", model.end_date);

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
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];

                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_APIDELETEDAYSTARTENDINFO", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "DELETEDAYSTARTENDATTENDANCE");
                sqlcmd.Parameters.Add("@USER_ID", model.user_id);
                sqlcmd.Parameters.Add("@DATE", model.date);
                sqlcmd.Parameters.Add("@ISDAYSTARTDEL", model.isdaystartdel);
                sqlcmd.Parameters.Add("@ISDAYENDDEL", model.isdayenddel);

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
    }
}

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
    public class TimeSheetController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage TimeSheetList(inputTimeSheet model)
        {
            List<TimeSheet> oview = new List<TimeSheet>();
            TimeSheetList odata = new TimeSheetList();

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
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];

                DataSet dt = new DataSet();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("API_PRC_TIMESHEET", sqlcon);
                sqlcmd.Parameters.Add("@Action", "GETTIMESHEETDATAUSERANDDATEWISE");
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@weburl", weburl);
                sqlcmd.Parameters.Add("@TIMESHEET_DATE", model.date);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt != null && dt.Tables[0].Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<TimeSheet>(dt.Tables[0]);
                    odata.timesheet_list = oview;


                    if (dt != null && dt.Tables[1].Rows.Count > 0)
                    {
                        odata.superviser_name = Convert.ToString(dt.Tables[1].Rows[0][0]);
                        odata.total_hrs = Convert.ToString(dt.Tables[1].Rows[0][1]);

                    }

                    odata.status = "200";
                    odata.message = "Successfully get timesheet list.";
                }
                
                else
                {
                    if (dt != null && dt.Tables[1].Rows.Count > 0)
                    {
                        odata.superviser_name = Convert.ToString(dt.Tables[1].Rows[0][0]);
                    }
                    odata.status = "205";
                    odata.message = "No data found";
                }
                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }

        }
        [HttpPost]
        public HttpResponseMessage DeleteTimeSheet(inputTimeSheet model)
        {
            List<TimeSheet> oview = new List<TimeSheet>();
            addEditSuccess odata = new addEditSuccess();

            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
            }
            else
            {
                try
                {
                    String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                    string sessionId = "";


                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("API_PRC_TIMESHEET", sqlcon);
                    sqlcmd.Parameters.Add("@Action", "DELETETIMESHEET");
                    sqlcmd.Parameters.Add("@user_id", model.user_id);
                    sqlcmd.Parameters.Add("@timesheet_id", model.timesheet_id);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        odata.timesheet_status = Convert.ToString(dt.Rows[0][0]);
                        if (Convert.ToString(dt.Rows[0][0]) == "Approved" || Convert.ToString(dt.Rows[0][0]) == "Rejected")
                        {
                            odata.status = "204";
                            odata.message = "Already Approved/Rejected.";

                        }
                        else
                        {
                            odata.status = "200";
                            odata.message = "Successfully delete timesheet.";
                        }
                       
                    }
                    else
                    {
                        odata.timesheet_status = "";
                        odata.status = "205";
                        odata.message = "No data found";
                    }
                }
                catch
                {
                    odata.status = "205";
                    odata.message = "No data found";
                }
                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }

        }
        [HttpPost]
        public HttpResponseMessage GetTimeSheetConfig(inputTimeSheet model)
        {

            TimeSheetConfig odata = new TimeSheetConfig();

            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
            }
            else
            {
                try
                {
                    String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                    string sessionId = "";

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("API_PRC_TIMESHEET", sqlcon);
                    sqlcmd.Parameters.Add("@Action", "GETTIMESHEETSETTINGS");
                    sqlcmd.Parameters.Add("@user_id", model.user_id);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        odata.activity_text = Convert.ToString(dt.Rows[0]["value"]);
                        odata.product_text = Convert.ToString(dt.Rows[1]["value"]);
                        odata.project_text = Convert.ToString(dt.Rows[2]["value"]);
                        odata.timesheet_past_days = Convert.ToString(dt.Rows[3]["value"]);
                        odata.time_text = Convert.ToString(dt.Rows[4]["value"]);
                        odata.comment_text = Convert.ToString(dt.Rows[5]["value"]);
                        odata.submit_text = Convert.ToString(dt.Rows[6]["value"]);
                        odata.supervisor_name = Convert.ToString(dt.Rows[7]["value"]);
                        odata.client_text = Convert.ToString(dt.Rows[8]["value"]);



                    }


                    odata.status = "200";
                    odata.message = "Successfully get timesheet config.";
                }
                catch
                {
                    odata.status = "205";
                    odata.message = "No data found";
                }
                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }

        }
        [HttpPost]

        public HttpResponseMessage GetDropDown(inputTimeSheet model)
        {
            List<product> oproduct = new List<product>();
            List<activity> oactivity = new List<activity>();
            List<project> oproject = new List<project>();
            List<client> oclient = new List<client>();

            getDropDownList odata = new getDropDownList();

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


                DataSet dt = new DataSet();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("API_PRC_TIMESHEET", sqlcon);
                sqlcmd.Parameters.Add("@Action", "GETDROPDOWNDATA");
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt != null)
                {
                    if (dt != null && dt.Tables[3].Rows.Count > 0)
                    {
                        oproduct = APIHelperMethods.ToModelList<product>(dt.Tables[3]);
                        odata.product_list = oproduct;

                    }
                    if (dt != null && dt.Tables[0].Rows.Count > 0)
                    {
                        oclient = APIHelperMethods.ToModelList<client>(dt.Tables[0]);
                        odata.client_list = oclient;

                    }
                    if (dt != null && dt.Tables[1].Rows.Count > 0)
                    {
                        oproject = APIHelperMethods.ToModelList<project>(dt.Tables[1]);
                        odata.project_list = oproject;

                    }
                    if (dt != null && dt.Tables[2].Rows.Count > 0)
                    {
                        oactivity = APIHelperMethods.ToModelList<activity>(dt.Tables[2]);
                        odata.activity_list = oactivity;

                    }
                    odata.status = "200";
                    odata.message = "Successfully get timesheet dropdown data.";
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
        [HttpPost]
        public HttpResponseMessage SaveTimeSheet(TimeSheetEntry model)
        {
            addEditSuccess odata = new addEditSuccess();

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


                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("API_PRC_TIMESHEET", sqlcon);
                sqlcmd.Parameters.Add("@Action", "AddTimeSheet");
                sqlcmd.Parameters.Add("@user_id", model.user_id);

                sqlcmd.Parameters.Add("@time", model.time);
                sqlcmd.Parameters.Add("@client_id", model.client_id);
                sqlcmd.Parameters.Add("@product_id", model.product_id);
                sqlcmd.Parameters.Add("@project_id", model.project_id);
                sqlcmd.Parameters.Add("@timesheet_date", model.date);
                sqlcmd.Parameters.Add("@activity_id", model.activity_id);
                sqlcmd.Parameters.Add("@comments", model.comments);
                sqlcmd.Parameters.Add("@timesheet_id", model.timesheet_id);


                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt != null)
                {   odata.status = "200";
                odata.message = "Successfully add timesheet.";
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
        [HttpPost]
        public HttpResponseMessage UpdateTimeSheet(TimeSheetEntry model)
        {
            addEditSuccess odata = new addEditSuccess();

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


                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("API_PRC_TIMESHEET", sqlcon);
                sqlcmd.Parameters.Add("@Action", "UPDATETIMESHEET");
                sqlcmd.Parameters.Add("@user_id", model.user_id);

                sqlcmd.Parameters.Add("@time", model.time);
                sqlcmd.Parameters.Add("@client_id", model.client_id);
                sqlcmd.Parameters.Add("@product_id", model.product_id);
                sqlcmd.Parameters.Add("@project_id", model.project_id);
                sqlcmd.Parameters.Add("@timesheet_date", model.date);
                sqlcmd.Parameters.Add("@activity_id", model.activity_id);
                sqlcmd.Parameters.Add("@comments", model.comments);
                sqlcmd.Parameters.Add("@timesheet_id", model.timesheet_id);


                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt != null && dt.Rows.Count>0)
                {
                    odata.timesheet_status = Convert.ToString(dt.Rows[0][0]);
                    if (Convert.ToString(dt.Rows[0][0]) == "Approved" || Convert.ToString(dt.Rows[0][0]) == "Rejected")
                    {
                        odata.status = "204";
                        odata.message = "Already Approved/Rejected.";

                    }
                    else
                    {
                        odata.status = "200";
                        odata.message = "Successfully edit timesheet.";
                    }
                }
                else
                {
                    odata.timesheet_status = "";
                    odata.status = "205";
                    odata.message = "No data found";
                }
                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }

        }
    }
}

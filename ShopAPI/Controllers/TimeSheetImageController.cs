using ShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopAPI.Controllers
{
    public class TimeSheetImageController : Controller
    {
        //
        // GET: /TimeSheetImage/
        public ActionResult SaveTimeSheet(TimeSheetEntryMultipart modelinput)
        {
            addEditSuccess odata = new addEditSuccess();

            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                
            }
            else
            {
                TimeSheetEntry model = new TimeSheetEntry();
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<TimeSheetEntry>(modelinput.data);


                string sttName = "";

                sttName = modelinput.image.FileName;
                sttName = model.session_token + '_' + model.user_id + '_' + sttName;
                string vPath = Path.Combine(Server.MapPath("~/CommonFolder/TimeSheetImage"), sttName);
                modelinput.image.SaveAs(vPath);



                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                string sessionId = "";


                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("API_PRC_TIMESHEET", sqlcon);
                sqlcmd.Parameters.Add("@Action", "AddTimeSheet");
                sqlcmd.Parameters.Add("@file_name", sttName);
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
                {
                    odata.status = "200";
                    odata.message = "Successfully add timesheet.";
                }
                else
                {
                    odata.status = "205";
                    odata.message = "No data found";
                }
                
                
            }
            return Json(odata);
        }
        [HttpPost]
        public ActionResult UpdateTimeSheet(TimeSheetEntryMultipart modelinput)
        {
            addEditSuccess odata = new addEditSuccess();

            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
               
            }
            else
            {
                TimeSheetEntry model = new TimeSheetEntry();
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<TimeSheetEntry>(modelinput.data);


                string sttName = "";

                sttName = modelinput.image.FileName;
                sttName = model.session_token + '_' + model.user_id + '_' + sttName;
                string vPath = Path.Combine(Server.MapPath("~/CommonFolder/TimeSheetImage"), sttName);
                modelinput.image.SaveAs(vPath);

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
                sqlcmd.Parameters.Add("@file_name", sttName);
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
                        odata.message = "Successfully edit timesheet.";
                    }
                }
                else
                {
                    odata.timesheet_status = "";
                    odata.status = "205";
                    odata.message = "No data found";
                }

                
            }
            return Json(odata);

        }
	}
}
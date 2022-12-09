using Newtonsoft.Json.Linq;
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
    public class DayStartEndImageSaveController : Controller
    {
        public JsonResult DayStartEndImage(DayStartEndImageSaveInput model)
        {
            DayStartEndImageSaveOutput omodel = new DayStartEndImageSaveOutput();
            string ImageName = "";
            DayStartEndImageSaveDetails omedl2 = new DayStartEndImageSaveDetails();
            String APIHostingPort = System.Configuration.ConfigurationSettings.AppSettings["APIHostingPort"];
            string UploadFileDirectory = "~/CommonFolder";
            try
            {
                var hhhh = Newtonsoft.Json.JsonConvert.DeserializeObject<DayStartEndImageSaveDetails>(model.data);

                if (!string.IsNullOrEmpty(model.data))
                {

                    ImageName = model.image.FileName;
                    string vPath = Path.Combine(Server.MapPath("~/CommonFolder/DayStartEndImage"), ImageName);
                    model.image.SaveAs(vPath);
                }

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];

                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();

                sqlcmd = new SqlCommand("PRC_APIUSERWISEDAYSTARTEND", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "SAVEIMAGE");
                sqlcmd.Parameters.Add("@USER_ID", hhhh.user_id);
                sqlcmd.Parameters.Add("@STARTENDDATE", hhhh.date_time);
                sqlcmd.Parameters.Add("@ISSTART", hhhh.day_start);
                sqlcmd.Parameters.Add("@ISEND", hhhh.day_end);
                sqlcmd.Parameters.Add("@PathImage", "/CommonFolder/DayStartEndImage/" + ImageName);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Successfully Image Recieved.";
                }
                else
                {
                    omodel.status = "202";
                    omodel.message = "Image Recieved Fail.";
                }
            }
            catch (Exception msg)
            {
                omodel.status = "204" + ImageName;
                omodel.message = msg.Message;
            }
            return Json(omodel);
        }
	}
}
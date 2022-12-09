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
    public class AadharImageDetectionController : Controller
    {
        public JsonResult AadharImageSave(AadharImageDetectionInput model)
        {
            AadharImageDetectionOutput omodel = new AadharImageDetectionOutput();
            string ImageName = "";
            AadharImageDetectionInputDetails omedl2 = new AadharImageDetectionInputDetails();
            String APIHostingPort = System.Configuration.ConfigurationSettings.AppSettings["APIHostingPort"];
            string UploadFileDirectory = "~/CommonFolder";
            try
            {
                var hhhh = Newtonsoft.Json.JsonConvert.DeserializeObject<AadharImageDetectionInputDetails>(model.data);

                if (!string.IsNullOrEmpty(model.data))
                {
                    ImageName = model.attachments.FileName;
                    string vPath = Path.Combine(Server.MapPath("~/CommonFolder/AadharImage"), ImageName);
                    model.attachments.SaveAs(vPath);
                }

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];

                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();

                sqlcmd = new SqlCommand("PRC_APIAADHARIMAGEDETECTION", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "SAVEAADHARIMAGE");
                sqlcmd.Parameters.Add("@user_id", hhhh.user_id);
                sqlcmd.Parameters.Add("@PathImage", "/CommonFolder/AadharImage/" + ImageName);
                sqlcmd.Parameters.Add("@RegisterDateTime", hhhh.registration_date_time);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Aadhaar Photo Submited Success.";
                    omodel.user_id = hhhh.user_id;
                    omodel.aadhaar_image_link = APIHostingPort + Convert.ToString(dt.Rows[0]["AadharImage"]);
                }
                else
                {
                    omodel.status = "202";
                    omodel.message = "Aadhaar Photo Submited Fail.";
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
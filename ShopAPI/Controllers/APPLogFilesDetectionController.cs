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
    public class APPLogFilesDetectionController : Controller
    {
        public JsonResult APPLogFilesSave(APPLogFilesDetectionInput model)
        {
            APPLogFilesDetectionOutput omodel = new APPLogFilesDetectionOutput();
            string ImageName = "";
            APPLogFilesDetectionInputDetails omedl2 = new APPLogFilesDetectionInputDetails();
            String APIHostingPort = System.Configuration.ConfigurationSettings.AppSettings["APIHostingPort"];
            string UploadFileDirectory = "~/CommonFolder";
            try
            {
                var hhhh = Newtonsoft.Json.JsonConvert.DeserializeObject<APPLogFilesDetectionInputDetails>(model.data);

                if (!string.IsNullOrEmpty(model.data))
                {
                    ImageName = model.attachments.FileName;
                    string vPath = Path.Combine(Server.MapPath("~/CommonFolder/APPLOGFILES"), ImageName);
                    model.attachments.SaveAs(vPath);
                }

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];

                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();

                sqlcmd = new SqlCommand("PRC_APIUSERAPPLOGFILESINFO", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "SAVEAPPLOGFILES");
                sqlcmd.Parameters.Add("@user_id", hhhh.user_id);
                sqlcmd.Parameters.Add("@PathAPPFiles", "/CommonFolder/APPLOGFILES/" + ImageName);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Upload Success.";
                    omodel.user_id = hhhh.user_id;
                    omodel.file_url = APIHostingPort + Convert.ToString(dt.Rows[0]["LOGDOCUMENTPATH"]);
                }
                else
                {
                    omodel.status = "202";
                    omodel.message = "Upload Fail.";
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
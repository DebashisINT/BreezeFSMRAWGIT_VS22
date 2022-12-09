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
    public class BreakageMaterialsDetectionInfoController : Controller
    {
        public JsonResult BreakageMaterialsSave(BreakageMaterialsDetectionSaveInput model)
        {
            BreakageMaterialsDetectionSaveOutput omodel = new BreakageMaterialsDetectionSaveOutput();
            string ImageName = "";
            BreakageMaterialsDetectionSaveInputDetails omedl2 = new BreakageMaterialsDetectionSaveInputDetails();
            String APIHostingPort = System.Configuration.ConfigurationSettings.AppSettings["APIHostingPort"];
            string UploadFileDirectory = "~/CommonFolder";
            try
            {
                var hhhh = Newtonsoft.Json.JsonConvert.DeserializeObject<BreakageMaterialsDetectionSaveInputDetails>(model.data);

                if (!string.IsNullOrEmpty(model.data))
                {

                    ImageName = model.attachments.FileName;
                    string vPath = Path.Combine(Server.MapPath("~/CommonFolder/BreakageImage"), ImageName);
                    model.attachments.SaveAs(vPath);
                }

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];

                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();

                sqlcmd = new SqlCommand("PRC_APIBREAKAGEINFODETECTION", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "SAVEDATA");
                sqlcmd.Parameters.Add("@USER_ID", hhhh.user_id);
                sqlcmd.Parameters.Add("@DATE_TIME", hhhh.date_time);
                sqlcmd.Parameters.Add("@BREAKAGE_NUMBER", hhhh.breakage_number);
                sqlcmd.Parameters.Add("@PRODUCT_ID", hhhh.product_id);
                sqlcmd.Parameters.Add("@DESCRIPTION_OF_BREAKAGE", hhhh.description_of_breakage);
                sqlcmd.Parameters.Add("@CUSTOMER_FEEDBACK", hhhh.customer_feedback);
                sqlcmd.Parameters.Add("@REMARKS", hhhh.remarks);
                sqlcmd.Parameters.Add("@SHOP_ID", hhhh.shop_id);
                sqlcmd.Parameters.Add("@PathImage", "/CommonFolder/BreakageImage/" + ImageName);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Submited Successfully.";
                    omodel.user_id = hhhh.user_id;
                    omodel.image_link = APIHostingPort + Convert.ToString(dt.Rows[0]["DOCUMENTIMAGEPATH"]);
                }
                else
                {
                    omodel.status = "202";
                    omodel.message = "Save Fail.";
                }
            }
            catch (Exception msg)
            {
                omodel.status = "204" + ImageName;
                omodel.message = msg.Message;
            }
            return Json(omodel);
        }

        public JsonResult BreakageMaterialDelete(BreakageMaterialsDeleteInput model)
        {
            BreakageMaterialsDeleteOutput omodel = new BreakageMaterialsDeleteOutput();
            String APIHostingPort = System.Configuration.ConfigurationSettings.AppSettings["APIHostingPort"];
            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Json(omodel);
            }
            else
            {
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_APIBREAKAGEINFODETECTION", sqlcon);
                sqlcmd.Parameters.Add("@Action", "DELETEDATA");
                sqlcmd.Parameters.Add("@USER_ID", model.user_id);
                sqlcmd.Parameters.Add("@BREAKAGE_NUMBER", model.breakage_number);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Delete Success.";

                    string vPath = Path.Combine(Server.MapPath(Convert.ToString(dt.Rows[0]["DOCUMENTIMAGEPATH"])));
                    System.IO.File.Delete(vPath);
                }
                else
                {
                    omodel.status = "205";
                    omodel.message = "No Match Found.";
                }
            }
            return Json(omodel);
        }
	}
}
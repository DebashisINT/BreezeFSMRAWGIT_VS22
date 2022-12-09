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
    public class SurveyQAImageController : Controller
    {
        public JsonResult SurveyQAImageSave(SurveyQAImageInput model)
        {
            SurveyQAImageOutput omodel = new SurveyQAImageOutput();
            string ImageName = "";
            String APIHostingPort = System.Configuration.ConfigurationSettings.AppSettings["APIHostingPort"];
            string UploadFileDirectory = "~/CommonFolder";
            try
            {
                var hhhh = Newtonsoft.Json.JsonConvert.DeserializeObject<SurveyQAImageInputDetails>(model.data);

                if (!string.IsNullOrEmpty(model.data))
                {
                    ImageName = model.attachments.FileName;
                    string vPath = Path.Combine(Server.MapPath("~/CommonFolder/SurveyImage"), ImageName);
                    model.attachments.SaveAs(vPath);
                }

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];

                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();

                sqlcmd = new SqlCommand("PRC_FSMAPISURVEYQUESTIONANSWERINFO", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "IMAGESAVE");
                sqlcmd.Parameters.Add("@USER_ID", hhhh.user_id);
                sqlcmd.Parameters.Add("@QUESTION_ID", hhhh.question_id);
                sqlcmd.Parameters.Add("@SURVEY_ID", hhhh.survey_id);
                sqlcmd.Parameters.Add("@PathImage", "/CommonFolder/SurveyImage/" + ImageName);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Successfully Uploaded.";
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

        public JsonResult SurveyQAImageDelete(SurveyDeleteInputModel model)
        {
            SurveyDeleteOutputModel omodel = new SurveyDeleteOutputModel();
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
                sqlcmd = new SqlCommand("PRC_FSMAPISURVEYQUESTIONANSWERINFO", sqlcon);
                sqlcmd.Parameters.Add("@Action", "DELETESURVEY");
                sqlcmd.Parameters.Add("@USER_ID", model.user_id);
                sqlcmd.Parameters.Add("@SURVEY_ID", model.survey_id);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Survey Deleted Successfully.";

                    foreach (DataRow row in dt.Rows)
                    {
                        string ImageValue = row["DOCUMENTIMAGE"].ToString();
                        //string vPath = Path.Combine(Server.MapPath(Convert.ToString(dt.Rows[0]["DOCUMENTIMAGE"])));
                        string vPath = Path.Combine(Server.MapPath(ImageValue));
                        System.IO.File.Delete(vPath);
                    }
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
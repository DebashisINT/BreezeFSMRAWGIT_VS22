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
    public class FaceImageDetectionController : Controller
    {
        public JsonResult FaceImage(FaceImageDetectionInput model)
        {
            FaceImageDetectionOutput omodel = new FaceImageDetectionOutput();
            string ImageName = "";
            FaceImageDetectionInputDetails omedl2 = new FaceImageDetectionInputDetails();
            String APIHostingPort = System.Configuration.ConfigurationSettings.AppSettings["APIHostingPort"];
            string UploadFileDirectory = "~/CommonFolder";
            try
            {
              
                var hhhh = Newtonsoft.Json.JsonConvert.DeserializeObject<FaceImageDetectionInputDetails>(model.data);

                if (!string.IsNullOrEmpty(model.data))
                {
                    
                        ImageName = model.attachments.FileName;
                        string vPath = Path.Combine(Server.MapPath("~/CommonFolder/FaceImageDetection"), ImageName);
                        model.attachments.SaveAs(vPath);                      
                }

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];

                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();

                sqlcmd = new SqlCommand("PROC_APIFaceImageDetection", sqlcon);
                sqlcmd.Parameters.Add("@Action", "FaceImageSave");
                sqlcmd.Parameters.Add("@user_id", hhhh.user_id);
                sqlcmd.Parameters.Add("@PathImage", "/CommonFolder/FaceImageDetection/" + ImageName);
                //Rev Debashis
                sqlcmd.Parameters.Add("@RegisterDateTime", hhhh.registration_date_time);
                //End of Rev Debashis

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Face Photo Submited Success.";
                    omodel.user_id = hhhh.user_id;
                    omodel.face_image_link = APIHostingPort+Convert.ToString(dt.Rows[0]["FaceImage"]);
                    //Rev Debashis
                    //omodel.registration_date_time = Convert.ToString(hhhh.registration_date_time);
                    //End of Rev Debashis
                }
                else
                {
                    omodel.status = "202";
                    omodel.message = "Face Photo Submited Fail.";
                }
            }
            catch (Exception msg)
            {
                omodel.status = "204" + ImageName;
                omodel.message = msg.Message;
            }
            return Json(omodel);
        }

        public JsonResult FaceImgDelete(FaceImageDeleteInputModel model)
        {
            FaceImageDeleteOutputModel omodel = new FaceImageDeleteOutputModel();
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
                sqlcmd = new SqlCommand("PRC_FTSAPI_USERLISTSHOPMAP", sqlcon);
                sqlcmd.Parameters.Add("@Action", "FaceImgDel");
                sqlcmd.Parameters.Add("@USER_ID", model.user_id);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.user_id = Convert.ToString(dt.Rows[0]["user_id"]);
                    omodel.status = "200";
                    //omodel.message = "Face Photo Delete Success.";
                    omodel.message = "Face Aadhar Delete Success.";
                    //String path = APIHostingPort + Convert.ToString(dt.Rows[0]["face_image_link"]);
                    //System.IO.File.Delete(path);

                    string vPath = Path.Combine(Server.MapPath(Convert.ToString(dt.Rows[0]["face_image_link"])));
                    System.IO.File.Delete(vPath);

                    string vAadharPath = Path.Combine(Server.MapPath(Convert.ToString(dt.Rows[0]["aadhar_image_link"])));
                    System.IO.File.Delete(vAadharPath);
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
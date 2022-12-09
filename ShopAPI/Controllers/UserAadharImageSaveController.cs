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
    public class UserAadharImageSaveController : Controller
    {
        public JsonResult UserAadharImage(UserAadharImageSaveInput model)
        {
            UserAadharImageSaveOutput omodel = new UserAadharImageSaveOutput();
            string ImageName = "";
            UserAadharImageSaveDetails omedl2 = new UserAadharImageSaveDetails();
            String APIHostingPort = System.Configuration.ConfigurationSettings.AppSettings["APIHostingPort"];
            string UploadFileDirectory = "~/CommonFolder";
            try
            {
                var hhhh = Newtonsoft.Json.JsonConvert.DeserializeObject<UserAadharImageSaveDetails>(model.data);

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

                sqlcmd = new SqlCommand("PRC_APIEMPLOYEEAADHARINFORMATION", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "SAVEIMAGE");
                sqlcmd.Parameters.Add("@USER_ID", hhhh.aadhaar_holder_user_id);
                sqlcmd.Parameters.Add("@USERCONTACTID", hhhh.aadhaar_holder_user_contactid);
                sqlcmd.Parameters.Add("@AADHAAR_NO", hhhh.aadhaar_no);
                sqlcmd.Parameters.Add("@AADHAARDATE", hhhh.date);
                sqlcmd.Parameters.Add("@FEEDBACK", hhhh.feedback);
                sqlcmd.Parameters.Add("@ADDRESS", hhhh.address);
                sqlcmd.Parameters.Add("@PathImage", "/CommonFolder/AadharImage/" + ImageName);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Successfully Aadhaar submited.";
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
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
    public class DemoAttendanceController : Controller
    {
        public JsonResult AddAttendanceImage(AttendanceInputData model)
        {
            DemoLoginOutput omodel = new DemoLoginOutput();
            string ImageName = "";
            ImageName = model.image.FileName;
            string UploadFileDirectory = "~/CommonFolder/AttendanceImageDemo";
            try
            {
                var hhhh = Newtonsoft.Json.JsonConvert.DeserializeObject<DemoAttendanceModel>(model.data);

                if (!string.IsNullOrEmpty(model.data))
                {
                    ImageName = hhhh.session_token + '_' + hhhh.user_id + '_' + ImageName;
                    string vPath = Path.Combine(Server.MapPath("~/CommonFolder/AttendanceImageDemo"), ImageName);
                    model.image.SaveAs(vPath);

                }
                string sessionId = "";

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();

                sqlcmd = new SqlCommand("PRC_APIInsertAttendanceImage", sqlcon);
                sqlcmd.Parameters.Add("@USER_ID", hhhh.user_id);
                sqlcmd.Parameters.Add("@IMAGE_NAME", ImageName);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                //SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                //da.Fill(dt);
                int i = sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                if (i > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Successfully Image Recieved.";
                }
                else
                {
                    omodel.status = "202";
                    omodel.message = "Data not inserted";
                }
            }
            catch (Exception msg)
            {
                omodel.status = "204" + ImageName;
                omodel.message = msg.Message;
            }
            return Json(omodel);
        }

        public JsonResult AddLoginImage(AttendanceInputData model)
        {
            DemoLoginOutput omodel = new DemoLoginOutput();
            string ImageName = "";
            ImageName = model.image.FileName;
            string UploadFileDirectory = "~/CommonFolder/LoginImageDemo";
            try
            {
                var hhhh = Newtonsoft.Json.JsonConvert.DeserializeObject<DemoLoginInput>(model.data);
                Encryption epasswrd = new Encryption();
                string Encryptpass = epasswrd.Encrypt(hhhh.password);
                if (!string.IsNullOrEmpty(model.data))
                {
                    ImageName =  hhhh.user_name + '_' + ImageName;
                    string vPath = Path.Combine(Server.MapPath("~/CommonFolder/LoginImageDemo"), ImageName);
                    model.image.SaveAs(vPath);

                }
                string sessionId = "";

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();

                sqlcmd = new SqlCommand("PRC_APIInsertLoginImage", sqlcon);
                sqlcmd.Parameters.Add("@userName", hhhh.user_name);
                sqlcmd.Parameters.Add("@password", Encryptpass);
                sqlcmd.Parameters.Add("@IMAGE_NAME", ImageName);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                int i = sqlcmd.ExecuteNonQuery();
                //SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
               // da.Fill(dt);
                sqlcon.Close();
                if (i > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Successfully Image Recieved.";
                }
                else
                {
                    omodel.status = "202";
                    omodel.message = "Data not inserted";
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
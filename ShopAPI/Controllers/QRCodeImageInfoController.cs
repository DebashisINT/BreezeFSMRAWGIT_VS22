#region======================================Revision History=========================================================
//Written By : Debashis Talukder On 01/06/2023
//Purpose : Save QR Code Image.
#endregion===================================End of Revision History==================================================
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
    public class QRCodeImageInfoController : Controller
    {
        public JsonResult SaveQRCodeImage(SaveQRCodeImageInput model)
        {
            SaveQRCodeImageOutput omodel = new SaveQRCodeImageOutput();
            string ImageName = "";
            SaveQRCodeImageInputDetails omedl2 = new SaveQRCodeImageInputDetails();
            String APIHostingPort = System.Configuration.ConfigurationManager.AppSettings["APIHostingPort"];
            string UploadFileDirectory = "~/CommonFolder";
            try
            {
                var hhhh = Newtonsoft.Json.JsonConvert.DeserializeObject<SaveQRCodeImageInputDetails>(model.data);

                if (!string.IsNullOrEmpty(model.data))
                {
                    ImageName = model.attachments.FileName;
                    string vPath = Path.Combine(Server.MapPath("~/CommonFolder/QRCodeImage"), ImageName);
                    model.attachments.SaveAs(vPath);
                }

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];

                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();

                sqlcmd = new SqlCommand("PRC_FTSAPIQRCODEIMAGEINFO", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "SAVEQRCODEIMAGE");
                sqlcmd.Parameters.AddWithValue("@USER_ID", hhhh.user_id);
                sqlcmd.Parameters.AddWithValue("@PathImage", "/CommonFolder/QRCodeImage/" + ImageName);
                sqlcmd.Parameters.AddWithValue("@BaseURL", APIHostingPort);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Image Save successfully.";
                    omodel.qr_img_link= APIHostingPort + Convert.ToString(dt.Rows[0]["qr_img_link"]);
                }
                else
                {
                    omodel.status = "202";
                    omodel.message = "Image Save Fail.";
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
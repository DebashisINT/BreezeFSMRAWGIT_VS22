#region======================================Revision History=========================================================
//Written By : Debashis Talukder On 17/05/2023
//Purpose : Save Current Stock Images.
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
    public class CurrentStockImageInfoController : Controller
    {
        public JsonResult SaveCurrentStockImage1(SaveCurrentStockImage1Input model)
        {
            SaveCurrentStockImage1Output omodel = new SaveCurrentStockImage1Output();
            string ImageName = "";
            SaveCurrentStockImage1InputDetails omedl2 = new SaveCurrentStockImage1InputDetails();
            String APIHostingPort = System.Configuration.ConfigurationManager.AppSettings["APIHostingPort"];
            string UploadFileDirectory = "~/CommonFolder";
            try
            {
                var hhhh = Newtonsoft.Json.JsonConvert.DeserializeObject<SaveCurrentStockImage1InputDetails>(model.data);

                if (!string.IsNullOrEmpty(model.data))
                {
                    ImageName = model.attachment_stock_image1.FileName;
                    string vPath = Path.Combine(Server.MapPath("~/CommonFolder/CurrentStockImage"), ImageName);
                    model.attachment_stock_image1.SaveAs(vPath);
                }

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];

                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();

                sqlcmd = new SqlCommand("PRC_FTSAPICURRENTSTOCKIMAGEINFO", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "SAVECURRENTSTOCKIMAGE1");
                sqlcmd.Parameters.AddWithValue("@USER_ID", hhhh.user_id);
                sqlcmd.Parameters.AddWithValue("@STOCK_CODE", hhhh.stock_id);
                sqlcmd.Parameters.AddWithValue("@PathImage", "/CommonFolder/CurrentStockImage/" + ImageName);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Image Save successfully.";
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

        public JsonResult SaveCurrentStockImage2(SaveCurrentStockImage2Input model)
        {
            SaveCurrentStockImage2Output omodel = new SaveCurrentStockImage2Output();
            string ImageName = "";
            SaveCurrentStockImage2InputDetails omedl2 = new SaveCurrentStockImage2InputDetails();
            String APIHostingPort = System.Configuration.ConfigurationManager.AppSettings["APIHostingPort"];
            string UploadFileDirectory = "~/CommonFolder";
            try
            {
                var hhhh = Newtonsoft.Json.JsonConvert.DeserializeObject<SaveCurrentStockImage2InputDetails>(model.data);

                if (!string.IsNullOrEmpty(model.data))
                {
                    ImageName = model.attachment_stock_image2.FileName;
                    string vPath = Path.Combine(Server.MapPath("~/CommonFolder/CurrentStockImage"), ImageName);
                    model.attachment_stock_image2.SaveAs(vPath);
                }

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];

                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();

                sqlcmd = new SqlCommand("PRC_FTSAPICURRENTSTOCKIMAGEINFO", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "SAVECURRENTSTOCKIMAGE2");
                sqlcmd.Parameters.AddWithValue("@USER_ID", hhhh.user_id);
                sqlcmd.Parameters.AddWithValue("@STOCK_CODE", hhhh.stock_id);
                sqlcmd.Parameters.AddWithValue("@PathImage", "/CommonFolder/CurrentStockImage/" + ImageName);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Image Save successfully.";
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
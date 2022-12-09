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
    public class RubyLeadImageController : Controller
    {
        public JsonResult RubyLeadImage1Save(RubyLeadImage1Input model)
        {
            RubyLeadImage1Output omodel = new RubyLeadImage1Output();
            string ImageName = "";
            RubyLeadImage1InputDetails omedl2 = new RubyLeadImage1InputDetails();
            String APIHostingPort = System.Configuration.ConfigurationSettings.AppSettings["APIHostingPort"];
            string UploadFileDirectory = "~/CommonFolder";
            try
            {
                var hhhh = Newtonsoft.Json.JsonConvert.DeserializeObject<RubyLeadImage1InputDetails>(model.data);

                if (!string.IsNullOrEmpty(model.data))
                {
                    ImageName = model.rubylead_image1.FileName;
                    string vPath = Path.Combine(Server.MapPath("~/CommonFolder/RubyLeadImage"), ImageName);
                    model.rubylead_image1.SaveAs(vPath);
                }

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];

                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();

                sqlcmd = new SqlCommand("PRC_FSMAPIRUBYFOODCUSTOMIZATION", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "IMAGE1SAVE");
                sqlcmd.Parameters.Add("@USER_ID", hhhh.user_id);
                sqlcmd.Parameters.Add("@SHOP_ID", hhhh.lead_shop_id);
                sqlcmd.Parameters.Add("@PathImage", "/CommonFolder/RubyLeadImage/" + ImageName);

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

        public JsonResult RubyLeadImage2Save(RubyLeadImage2Input model)
        {
            RubyLeadImage2Output omodel = new RubyLeadImage2Output();
            string ImageName = "";
            RubyLeadImage2InputDetails omedl2 = new RubyLeadImage2InputDetails();
            String APIHostingPort = System.Configuration.ConfigurationSettings.AppSettings["APIHostingPort"];
            string UploadFileDirectory = "~/CommonFolder";
            try
            {
                var hhhh = Newtonsoft.Json.JsonConvert.DeserializeObject<RubyLeadImage2InputDetails>(model.data);

                if (!string.IsNullOrEmpty(model.data))
                {
                    ImageName = model.rubylead_image2.FileName;
                    string vPath = Path.Combine(Server.MapPath("~/CommonFolder/RubyLeadImage"), ImageName);
                    model.rubylead_image2.SaveAs(vPath);
                }

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];

                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();

                sqlcmd = new SqlCommand("PRC_FSMAPIRUBYFOODCUSTOMIZATION", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "IMAGE2SAVE");
                sqlcmd.Parameters.Add("@USER_ID", hhhh.user_id);
                sqlcmd.Parameters.Add("@SHOP_ID", hhhh.lead_shop_id);
                sqlcmd.Parameters.Add("@PathImage", "/CommonFolder/RubyLeadImage/" + ImageName);

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
	}
}
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
    public class ActivityMultipartController : Controller
    {
        public JsonResult AddActivity(ActivityAddAttachmentInput model)
        {
            ActivityAddOutput omodel = new ActivityAddOutput();
            string ImageName = "";
            List<Imagees> omedl2 = new List<Imagees>();

            string UploadFileDirectory = "~/CommonFolder";
            try
            {
                var details = JObject.Parse(model.data);

                var hhhh = Newtonsoft.Json.JsonConvert.DeserializeObject<ActivityAddInput>(model.data);

                if (!string.IsNullOrEmpty(model.data))
                {
                    String attachmenttype = "";
                    for (int i = 0; i < model.attachments.Count; i++)
                    {
                        ImageName = model.attachments[i].FileName;
                        ImageName = hhhh.session_token + '_' + hhhh.user_id + '_' + ImageName;
                        string vPath = Path.Combine(Server.MapPath("~/CommonFolder/ActivityAttachment"), ImageName);
                        model.attachments[i].SaveAs(vPath);

                        int part = ImageName.IndexOf("image");
                        if (part>0)
                        {
                            attachmenttype = "Image";
                        }
                        else
                        {
                            attachmenttype = "Attachment";
                        }
                        omedl2.Add(new Imagees()
                        {
                            attachment = ImageName,
                            attachmenttype = attachmenttype,
                        });
                    }
                }

                string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);

                string sessionId = "";

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();

                sqlcmd = new SqlCommand("PRC_FTS_SALESACTIVITY", sqlcon);
                sqlcmd.Parameters.Add("@user_id", hhhh.user_id);
                sqlcmd.Parameters.Add("@ActivityCode", hhhh.id);
                sqlcmd.Parameters.Add("@party_id", hhhh.party_id);
                sqlcmd.Parameters.Add("@date", hhhh.date);
                sqlcmd.Parameters.Add("@time", hhhh.time);
                sqlcmd.Parameters.Add("@name", hhhh.name);
                sqlcmd.Parameters.Add("@activity_id", hhhh.activity_id);
                sqlcmd.Parameters.Add("@type_id", hhhh.type_id);
                sqlcmd.Parameters.Add("@product_id", hhhh.product_id);
                sqlcmd.Parameters.Add("@subject", hhhh.subject);
                sqlcmd.Parameters.Add("@details", hhhh.details);
                sqlcmd.Parameters.Add("@duration", hhhh.duration);
                sqlcmd.Parameters.Add("@priority_id", hhhh.priority_id);
                sqlcmd.Parameters.Add("@due_date", hhhh.due_date);
                sqlcmd.Parameters.Add("@due_time", hhhh.due_time);

                sqlcmd.Parameters.Add("@JsonXML", JsonXML);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Successfully add activity";
                }
                else
                {
                    omodel.status = "202";
                    omodel.message = "Failed to add activity";
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
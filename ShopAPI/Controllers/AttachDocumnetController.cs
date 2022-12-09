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
    public class AttachDocumnetController : Controller
    {
        public JsonResult AddDocumnet(AttachDocumnetModel model)
        {
            ActivityAddOutput omodel = new ActivityAddOutput();
            string ImageName = "";
            List<AttachDocumnetData> omedl2 = new List<AttachDocumnetData>();

            string UploadFileDirectory = "~/CommonFolder";
            try
            {
                var details = JObject.Parse(model.data);

                var hhhh = Newtonsoft.Json.JsonConvert.DeserializeObject<AttachmentDocumentTypeModel>(model.data);

                if (!string.IsNullOrEmpty(model.data))
                {
                    String type_id = "";
                    String id = "";
                    String Date = "";
                    for (int i = 0; i < model.attachment.Count; i++)
                    {
                        ImageName = model.attachment[i].FileName;
                        ImageName = hhhh.session_token + '_' + hhhh.user_id + '_' + ImageName.Split('~')[4];
                        string vPath = Path.Combine(Server.MapPath("~/CommonFolder/AttachDocument"), ImageName);
                        model.attachment[i].SaveAs(vPath);
                        omedl2.Add(new AttachDocumnetData()
                        {
                            id = model.attachment[i].FileName.Split('~')[1],
                            type_id = model.attachment[i].FileName.Split('~')[2],
                            date_time = model.attachment[i].FileName.Split('~')[3],
                            attachment = ImageName
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

                sqlcmd = new SqlCommand("PROC_APIAttachmentDocument", sqlcon);
                sqlcmd.Parameters.Add("@user_id", hhhh.user_id);
                sqlcmd.Parameters.Add("@JsonXML", JsonXML);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Successfully add/edit document";
                }
                else
                {
                    omodel.status = "202";
                    omodel.message = "Failed to add/edit document";
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
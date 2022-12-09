using ShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace ShopAPI.Controllers
{
    public class DynamicFormAttController : Controller
    {
        //
        // GET: /DynamicFormAtt/
        [HttpPost]
        public JsonResult SaveFormWithAttachment(DynamicFormSaveInputAttachment input)
        {

            DynamicFormSaveOutput odata = new DynamicFormSaveOutput();

            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Json(odata);
            }
            else
            {




                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                string sessionId = "";


                JavaScriptSerializer json_serializer = new JavaScriptSerializer();
                //var model =
                //       json_serializer.DeserializeObject(input.data);
                DynamicFormSaveInput model = Newtonsoft.Json.JsonConvert.DeserializeObject<DynamicFormSaveInput>(input.data);

                string result = "";
                using (var stringwriter = new System.IO.StringWriter())
                {
                    var serializer = new XmlSerializer(model.view_list.GetType());
                    serializer.Serialize(stringwriter, model.view_list);
                    result = stringwriter.ToString();
                }


                string sttName = "";

                sttName = input.attachments.FileName;
                sttName = model.session_token + '_' + model.user_id + '_' + sttName;
                string vPath = Path.Combine(Server.MapPath("~/CommonFolder/DynamicFiles"), sttName);
                input.attachments.SaveAs(vPath);




                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();


                sqlcmd = new SqlCommand("PRC_DYNAMIC_LAYOUT", sqlcon);
                sqlcmd.Parameters.Add("@action", "SaveItems");
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@result", result);
                sqlcmd.Parameters.Add("@filename", sttName);
                sqlcmd.Parameters.Add("@dynamicFormName", model.dynamicFormName);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();



                if (dt.Rows.Count > 0)
                {

                    odata.status = "200";
                    odata.message = "Saved Successfully.";

                }
                else
                {
                    odata.status = "205";
                    odata.message = "Some input parameters are missing.";

                }


                return Json(odata);
            }
        }

        [HttpPost]
        public JsonResult SaveFormWithAttachmentEdit(DynamicFormSaveInputAttachment input)
        {

            DynamicFormSaveOutput odata = new DynamicFormSaveOutput();

            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Json(odata);
            }
            else
            {




                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                string sessionId = "";


                JavaScriptSerializer json_serializer = new JavaScriptSerializer();
                //var model =
                //       json_serializer.DeserializeObject(input.data);
                DynamicFormSaveEditInput model = Newtonsoft.Json.JsonConvert.DeserializeObject<DynamicFormSaveEditInput>(input.data);

                string result = "";
                using (var stringwriter = new System.IO.StringWriter())
                {
                    var serializer = new XmlSerializer(model.view_list.GetType());
                    serializer.Serialize(stringwriter, model.view_list);
                    result = stringwriter.ToString();
                }


                string sttName = "";

                sttName = input.attachments.FileName;
                sttName = model.session_token + '_' + model.user_id + '_' + sttName;
                string vPath = Path.Combine(Server.MapPath("~/CommonFolder/DynamicFiles"), sttName);
                input.attachments.SaveAs(vPath);




                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();


                sqlcmd = new SqlCommand("PRC_DYNAMIC_LAYOUT", sqlcon);
                sqlcmd.Parameters.Add("@action", "SaveItemsEdit");
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@key", model.id);
                sqlcmd.Parameters.Add("@result", result);
                sqlcmd.Parameters.Add("@filename", sttName);
                sqlcmd.Parameters.Add("@dynamicFormName", model.dynamicFormName);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();



                if (dt.Rows.Count > 0)
                {

                    odata.status = "200";
                    odata.message = "Saved Successfully.";

                }
                else
                {
                    odata.status = "205";
                    odata.message = "Some input parameters are missing.";

                }


                return Json(odata);
            }
        }
	}
}
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
    public class CollectionFileUpController : Controller
    {
        public JsonResult UploadDocforCollection(CollectionFileUploadInput model)
        {
            CollectionFileUploadOutput omodel = new CollectionFileUploadOutput();
            string docName = "";
            ShopRegister oview = new ShopRegister();
            docName = model.doc.FileName;
            string UploadFileDirectory = "~/CommonFolder/CollectionFile";
            try
            {
                var details = JObject.Parse(model.data);
                var hhhh = Newtonsoft.Json.JsonConvert.DeserializeObject<CollectionFileUploadInputData>(model.data);
                if (!string.IsNullOrEmpty(model.data))
                {
                    docName = hhhh.session_token + '_' + hhhh.user_id + '_' + docName;
                    string vPath = Path.Combine(Server.MapPath("~/CommonFolder/CollectionFile"), docName);
                    model.doc.SaveAs(vPath);
                }
                string sessionId = "";

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();

                sqlcmd = new SqlCommand("proc_FTS_CollectionMultiPart", sqlcon);
                sqlcmd.Parameters.Add("@user_id", hhhh.user_id);
                sqlcmd.Parameters.Add("@shop_id", hhhh.shop_id);
                sqlcmd.Parameters.Add("@collection", hhhh.collection);
                sqlcmd.Parameters.Add("@collection_id", hhhh.collection_id);
                sqlcmd.Parameters.Add("@collection_date", hhhh.collection_date);
                sqlcmd.Parameters.Add("@bill_id", hhhh.bill_id);
                sqlcmd.Parameters.Add("@payment_id", hhhh.payment_id);
                sqlcmd.Parameters.Add("@instrument_no", hhhh.instrument_no);
                sqlcmd.Parameters.Add("@bank", hhhh.bank);
                sqlcmd.Parameters.Add("@remarks", hhhh.remarks);
                sqlcmd.Parameters.Add("@order_id", hhhh.order_id);
                //Extra Input for 4Basecare
                sqlcmd.Parameters.Add("@patient_no", hhhh.patient_no);
                sqlcmd.Parameters.Add("@patient_name", hhhh.patient_name);
                sqlcmd.Parameters.Add("@patient_address ", hhhh.patient_address);
                //Extra Input for 4Basecare
                //Extra Input for EuroBond
                sqlcmd.Parameters.Add("@Hospital", hhhh.Hospital);
                sqlcmd.Parameters.Add("@Email_Address", hhhh.Email_Address);
                //Extra Input for EuroBond
                sqlcmd.Parameters.Add("@docName", docName);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Successfully add collection";
                }
            }
            catch (Exception msg)
            {
                omodel.status = "204" + docName;
                omodel.message = msg.Message;
            }
            return Json(omodel);
        }
	}
}
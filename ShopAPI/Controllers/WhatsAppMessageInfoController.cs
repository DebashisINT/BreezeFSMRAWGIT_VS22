#region======================================Revision History=========================================================
//Written By : Debashis Talukder On 01/08/2023
//Purpose : Save & Fetch List Shop WhatsApp Message.Row: 861 to 862
#endregion===================================End of Revision History==================================================

using ShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShopAPI.Controllers
{
    public class WhatsAppMessageInfoController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage WhatsAppMsgSave(WhatsAppMessageInfoSaveInput model)
        {
            WhatsAppMessageInfoSaveOutput omodel = new WhatsAppMessageInfoSaveOutput();
            List<ShopWhatsAppApiSave> oview = new List<ShopWhatsAppApiSave>();

            try
            {
                if (!ModelState.IsValid)
                {
                    omodel.status = "213";
                    omodel.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
                }
                else
                {
                    String token = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];

                    foreach (var s2 in model.shop_whatsapp_api_list)
                    {
                        oview.Add(new ShopWhatsAppApiSave()
                        {
                            shop_id = s2.shop_id,
                            shop_name = s2.shop_name,
                            contactNo = s2.contactNo,
                            isNewShop = s2.isNewShop,
                            date = s2.date,
                            time = s2.time,
                            isWhatsappSent = s2.isWhatsappSent,
                            whatsappSentMsg = s2.whatsappSentMsg
                        });
                    }
                    string JsonXML = XmlConversion.ConvertToXml(oview, 0);
                    String dates = DateTime.Now.ToString("ddMMyyyyhhmmssffffff");
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcmd.CommandTimeout = 60;
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APIWHATSAPPMSGINFO", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "SAVELIST");
                    sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@JsonXML", JsonXML);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Success.";
                    }
                    else
                    {
                        omodel.status = "205";
                        omodel.message = "Records not Saved.";
                    }
                    var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                    return message;
                }
            }
            catch (Exception ex)
            {
                omodel.status = "204";
                omodel.message = ex.Message;
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage WhatsAppMsgList(WhatsAppMessageInfoListInput model)
        {
            WhatsAppMessageInfoListOutput omodel = new WhatsAppMessageInfoListOutput();
            List<ShopWhatsAppApiList> oview = new List<ShopWhatsAppApiList>();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];

                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_APIWHATSAPPMSGINFO", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "FETCHLIST");
                sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);

                sqlcmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<ShopWhatsAppApiList>(dt);
                    omodel.status = "200";
                    omodel.message = "Success.";
                    omodel.user_id = dt.Rows[0]["User_Id"].ToString();
                    omodel.shop_whatsapp_api_list = oview;
                }
                else
                {
                    omodel.status = "205";
                    omodel.message = "No data found.";
                }

                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }
    }
}

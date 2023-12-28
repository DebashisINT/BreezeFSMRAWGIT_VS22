#region======================================Revision History=========================================================
//Written By : Debashis Talukder On 18/12/2023
//Purpose : Save & Fetch List Call Log.Row: 888 to 889
#endregion===================================End of Revision History==================================================

using ShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShopAPI.Controllers
{
    public class CallLogInformationsController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage CallLogListSave(CallLogListSaveInput model)
        {
            CallLogListSaveOutput omodel = new CallLogListSaveOutput();
            List<Callhislist> omedl2 = new List<Callhislist>();

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

                    foreach (var s2 in model.call_his_list)
                    {
                        omedl2.Add(new Callhislist()
                        {
                            shop_id = s2.shop_id,
                            call_number = s2.call_number,
                            call_date = s2.call_date,
                            call_time = s2.call_time,
                            call_date_time = s2.call_date_time,
                            call_type = s2.call_type,
                            call_duration_sec = s2.call_duration_sec,
                            call_duration = s2.call_duration
                        });
                    }
                    string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);
                    String dates = DateTime.Now.ToString("ddMMyyyyhhmmssffffff");
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcmd.CommandTimeout = 60;
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APICALLLOGINFORMATIONS", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "CALLLOGLISTSAVE");
                    sqlcmd.Parameters.AddWithValue("@USERID", model.user_id);
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
                        omodel.message = "Records not updated.";
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
        public HttpResponseMessage CallLogList(CallLogListInput model)
        {
            CallLogListOutput odata = new CallLogListOutput();
            try
            {
                List<CallhislistOutput> oview = new List<CallhislistOutput>();

                string token = string.Empty;
                string versionname = string.Empty;
                String tokenmatch = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();

                sqlcmd = new SqlCommand("PRC_APICALLLOGINFORMATIONS", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "CALLLOGLIST");
                sqlcmd.Parameters.AddWithValue("@USERID", model.user_id);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();

                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<CallhislistOutput>(dt);
                    odata.status = "200";
                    odata.message = "Success";
                    odata.user_id= model.user_id;
                    odata.call_his_list = oview;
                }
                else
                {
                    odata.status = "205";
                    odata.message = "No Data Found";
                    odata.call_his_list = oview;
                }

                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }

            catch (Exception ex)
            {
                odata.status = "209";
                odata.message = ex.Message;
                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }
        }
    }
}

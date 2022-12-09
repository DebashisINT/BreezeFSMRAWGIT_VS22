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
    public class AppInfoController : ApiController
    {
        //string uploadtext = "~/CommonFolder/Log/DeviceInformatio.txt";
        //LocationupdateOutput omodel = new LocationupdateOutput();

        [HttpPost]
        public HttpResponseMessage DeviceInformatio(DeviceInformationModel model)
        {
            LocationupdateOutput omodel = new LocationupdateOutput();
            string sdatetime = DateTime.Now.ToString();
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
                    //List<DeviceInformationDetails> omedl2 = new List<DeviceInformationDetails>();
                    //foreach (var s2 in model.app_info_list)
                    //{
                    //    omedl2.Add(new DeviceInformationDetails()
                    //    {
                    //        id = s2.id,
                    //        date_time = s2.date_time,
                    //        battery_status = s2.battery_status,
                    //        battery_percentage = s2.battery_percentage,
                    //        network_type = s2.network_type,
                    //        mobile_network_type = s2.mobile_network_type,
                    //        device_model = s2.device_model,
                    //        android_version = s2.android_version
                    //    });
                    //}

                    string JsonXML = XmlConversion.ConvertToXml(model.app_info_list, 0);

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("Proc_API_DeviceInformationUpdate", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@session_token", model.session_token);
                    sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);
                    //Rev Debashis Row: 739
                    sqlcmd.Parameters.AddWithValue("@TOTAL_VISIT_REVISIT_COUNT", model.total_visit_revisit_count);
                    sqlcmd.Parameters.AddWithValue("@TOTAL_VISIT_REVISIT_COUNT_SYNCED", model.total_visit_revisit_count_synced);
                    sqlcmd.Parameters.AddWithValue("@TOTAL_VISIT_REVISIT_COUNT_UNSYNCED", model.total_visit_revisit_count_unsynced);
                    //End of Rev Debashis Row: 739
                    sqlcmd.Parameters.AddWithValue("@JsonXML", JsonXML);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Successfully submit app info";
                    }
                    else
                    {
                        omodel.status = "202";
                        omodel.message = "User id or Session token does not matched";
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
        public HttpResponseMessage GetDeviceInformation(DeviceInfoInput model)
        {
            DeviceInfoOutPut omodel = new DeviceInfoOutPut();
            string sdatetime = DateTime.Now.ToString();
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
                   

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("Proc_API_DeviceInformation", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0)
                    {
                        omodel.app_info_list = APIHelperMethods.ToModelList<DeviceInformation>(dt);
                        omodel.status = "200";
                        omodel.message = "Successfully get app info";
                    }
                    else
                    {
                        omodel.status = "205";
                        omodel.message = "No Data found";
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

        //Rev Debashis Row: 743
        [HttpPost]
        public HttpResponseMessage UserWiseGPSNetStatus(UserWiseGPSNetStatusInput model)
        {
            UserWiseGPSNetStatusOutput omodel = new UserWiseGPSNetStatusOutput();
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
                    string JsonXML = XmlConversion.ConvertToXml(model.gps_net_status_list, 0);

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APIUSERWISEGPSNETSTATUS", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@SESSION_TOKEN", model.session_token);
                    sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@JsonXML", JsonXML);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Saved successfully.";
                    }
                    else
                    {
                        omodel.status = "202";
                        omodel.message = "No Data found.";
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
        //End of Rev Debashis Row: 743
    }
}

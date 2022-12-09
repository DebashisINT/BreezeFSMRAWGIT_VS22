﻿using ShopAPI.Models;
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
    public class LocationAddModifyController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage LocationModify(LocationAddModifyModel model)
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
                    List<LocationListInput> omedl2 = new List<LocationListInput>();
                    foreach (var s2 in model.location_list)
                    {
                        omedl2.Add(new LocationListInput()
                        {
                            id = s2.id,
                            location_name = s2.location_name,
                            latitude = s2.latitude,
                            longitude = s2.longitude
                        });
                    }

                    string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("Proc_API_ActivityLocationUpdate", sqlcon);
                    sqlcmd.Parameters.Add("@user_id", model.user_id);
                    sqlcmd.Parameters.Add("@JsonXML", JsonXML);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Successfully submit location.";
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
        public HttpResponseMessage LocationList(LocationAddModifyListInput model)
        {
            LocationAddModifyListOutPut omodel = new LocationAddModifyListOutPut();
            List<LocationListInput> oview = new List<LocationListInput>();
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
                    sqlcmd = new SqlCommand("Proc_API_ActivityLocationList", sqlcon);
                    sqlcmd.Parameters.Add("@user_id", model.user_id);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Successfully get location.";
                        oview = APIHelperMethods.ToModelList<LocationListInput>(dt);
                        omodel.location_list = oview;
                    }
                    else
                    {
                        omodel.status = "205";
                        omodel.message = "No data found";
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
    }
}

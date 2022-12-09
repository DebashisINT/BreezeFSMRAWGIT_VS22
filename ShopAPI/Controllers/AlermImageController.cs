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
    public class AlermImageController : Controller
    {
        //
        // GET: /AlermImage/
        [HttpPost]
        public JsonResult AlermSelfie(AlermSelfie model)
        {
            string ImageName = "";
            ShopRegister oview = new ShopRegister();
            ImageName = model.image.FileName;
            string UploadFileDirectory = "~/CommonFolder/AlermSelfie";


            var details = JObject.Parse(model.data);
            selfieimage omm = new selfieimage();
            foreach (var item in details)
            {

                string param = item.Key;
                string value = Convert.ToString(item.Value);
                switch (param)
                {
                    case "dateTime":
                        {
                            omm.date_time = Convert.ToDateTime(value).ToString("yyyy-MM-dd HH:MM:ss");;
                            break;
                        }

                    case "lat":
                        {                            
                            omm.lat = value;
                            break;
                        }

                    case "longs":
                        {
                            omm.longs = value;
                            break;
                        }

                    case "report_id":
                        {
                            omm.alarm_id = value;
                            break;
                        }
                    case "session_token":
                        {
                            omm.session_token = value;
                            break;
                        }
                    case "user_id":
                        {
                            omm.user_id = value;
                            break;
                        }

                }

            }


           
            AlermAttendanceOutput odata = new AlermAttendanceOutput();
            try
            {
                ImageName = omm.session_token + '_' + omm.user_id + '_' + ImageName;
                string vPath = Path.Combine(Server.MapPath(UploadFileDirectory), ImageName);
                model.image.SaveAs(vPath);


                if (!ModelState.IsValid)
                {
                    odata.status = "213";
                    odata.message = "Some input parameters are missing.";

                }
                else
                {

                    String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                    string sessionId = "";


                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("Proc_AlermImage", sqlcon);
                    sqlcmd.Parameters.Add("@image", ImageName);
                    sqlcmd.Parameters.Add("@view_time", omm.date_time);
                    sqlcmd.Parameters.Add("@user_id", omm.user_id);
                    sqlcmd.Parameters.Add("@report_time", omm.date_time);
                    sqlcmd.Parameters.Add("@LONG", omm.longs);
                    sqlcmd.Parameters.Add("@LAT", omm.lat);
                    sqlcmd.Parameters.Add("@alarm_id", omm.alarm_id);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();


                    if (dt.Rows.Count > 0)
                    {

                        odata.status = "200";
                        odata.message = "Successfully uploaded selfie.";

                    }
                    else
                    {

                        odata.status = "213";
                        odata.message = "Some input parameters are missing";

                    }


                }

                return Json(odata);
            }
            catch (Exception ex)
            {
                odata.status = "206";
                odata.message =ex.InnerException.Message;
                return Json(odata);

            }

        }
    }
}
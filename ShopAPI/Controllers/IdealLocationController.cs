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
    public class IdealLocationController : ApiController
    {

        string uploadtext = "~/CommonFolder/Log/Location.txt";

        IdealModelOutput omodel = new IdealModelOutput();
    


        [HttpPost]
        public HttpResponseMessage Submit(IdealModelInput model)
        {

            string sloactionlog = "";
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

                    String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                    string sessionId = "";

                    List<IdealModel> omedl2 = new List<IdealModel>();

                    foreach (var s2 in model.location_list)
                    {
                        omedl2.Add(new IdealModel()
                        {
                            ideal_id = s2.ideal_id,
                            start_ideal_date_time = s2.start_ideal_date_time,
                            end_ideal_date_time = s2.end_ideal_date_time,
                            start_ideal_lat = s2.start_ideal_lat,
                            start_ideal_lng = s2.start_ideal_lng,
                            end_ideal_lat = s2.end_ideal_lat,
                            end_ideal_lng = s2.end_ideal_lng
                        });
                    }


                    string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("Proc_Idealloacation", sqlcon);
                    sqlcmd.Parameters.Add("@user_id", model.user_id);
                    sqlcmd.Parameters.Add("@JsonXML", JsonXML);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0)
                    {
                      
                        omodel.status = "200";
                        omodel.message = "Ideal Location details successfully updated.";


                        //using (StreamWriter stream = new FileInfo((Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(uploadtext)))).AppendText())
                        //{
                        //    stream.WriteLine("   Location capture  Start " + "Start Time:" + sdatetime + "End Time:" + DateTime.Now + "Status:Success(LocationCapture/Sendlocation)[200]" + "User ID:" + model.user_id + sloactionlog+" Location capture  END ");
                        //}
                    }
                    else
                    {

                        omodel.status = "202";
                        omodel.message = "User id or Session token does not matched";

                        //using (StreamWriter stream = new FileInfo((Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(uploadtext)))).AppendText())
                        //{
                        //    stream.WriteLine("    Location capture  Start " + "Start Time:" + sdatetime + "End Time:" + DateTime.Now + "Status:Not matched(LocationCapture/Sendlocation)[202]" + "User ID:" + model.user_id + sloactionlog+ " Location capture END ");
                        //}
                    }
                   
                    omodel.status = "200";
                    omodel.message = "Ideal Location details successfully updated.";

                    var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                    return message;
                }
            }

            catch (Exception ex)
            {

                omodel.status = "204";
                omodel.message = ex.Message;

                //using (StreamWriter stream = new FileInfo((Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(uploadtext)))).AppendText())
                //{

                //    stream.WriteLine("   Location capture  Start " + "Start Time:" + sdatetime + "End Time:" + DateTime.Now + "Status:Error(LocationCapture/Sendlocation)[204]" + ex.Message + "User ID:" + model.user_id + sloactionlog + "   Location capture END ");
                //    //txt.Close();
                //}
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;

            }
        }


    }
}

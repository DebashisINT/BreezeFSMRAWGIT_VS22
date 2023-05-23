/*****************************************************************************************************************
 * 1.0      V2.0.40     28-04-2023      When Revisit Shop Image is added, it is not showing in 
 *                                      Dashboard - Employee At Work - Shop Visited image. Refer: 25925
 ******************************************************************************************************************/
using Newtonsoft.Json.Linq;
using ShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
//using System.Web.Mvc;
using System.Net.Http;
using System.Web.Http;
using System.Net;
using System.IO;
using System.Web.Mvc;

namespace ShopAPI.Controllers
{
    public class ShopVisitImageUploadController : Controller
    {

        string uploadtext = "~/CommonFolder/Log/Revisitimage.txt";
        public JsonResult Revisit(ShopvisitImageUpload model)
        {



            //  TextWriter txt = new StreamWriter(Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(uploadtext)));
            string sdatetime = DateTime.Now.ToString();


            ShopvisitImageUploadOutput omodel = new ShopvisitImageUploadOutput();
            ShopvisitImageclass omm = new ShopvisitImageclass();
            string ImageName = "";

            try
            {

                ShopRegister oview = new ShopRegister();
                ImageName = model.shop_image.FileName;
                string UploadFileDirectory = "~/CommonFolder/Shoprevisit";
                try
                {

                    var details = JObject.Parse(model.data);

                    foreach (var item in details)
                    {
                        string param = item.Key;
                        string value = Convert.ToString(item.Value);
                        switch (param)
                        {
                            case "session_token":
                                {
                                    omm.session_token = value;
                                    break;
                                }

                            case "visit_datetime":
                                {
                                    // Rev 1.0
                                    //String value1 = Convert.ToDateTime(item.Value).ToString("yyyy-MM-dd HH:MM:ss");
                                    String value1 = Convert.ToDateTime(value).ToString("yyyy-MM-dd HH:MM:ss");
                                    // End of Rev 1.0
                                    omm.visit_datetime = Convert.ToDateTime(value1);
                                    break;
                                }

                            case "shop_id":
                                {
                                    omm.shop_id = value;
                                    break;
                                }

                            case "user_id":
                                {
                                    omm.user_id = value;
                                    break;
                                }

                        }

                    }

                }
                catch (Exception msg)
                {

                    omodel.status = "206" + ImageName;
                    omodel.message = msg.Message;

                }



                ImageName = omm.session_token + '_' + omm.user_id + '_' + ImageName;
                string vPath = Path.Combine(Server.MapPath("~/CommonFolder/Shoprevisit"), ImageName);
                model.shop_image.SaveAs(vPath);

                string sessionId = "";

                List<ImageDetailsfrXml> omxImg = new List<ImageDetailsfrXml>();
             
                omxImg.Add(new ImageDetailsfrXml()
                       {
                           user_id = omm.user_id,
                           shop_id = omm.shop_id,
                           ImageName = ImageName,
                           visit_datetime = omm.visit_datetime
                       });


                string JsonXML = XmlConversion.ConvertToXml(omxImg, 0);
                String dates = DateTime.Now.ToString("ddMMyyyyhhmmssffffff");
                try
                {
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("Proc_ApiShopRevisitImageUpload", sqlcon);

                    sqlcmd.Parameters.Add("@user_id", omm.user_id);
                    sqlcmd.Parameters.Add("@shopvisit_image", ImageName);
                    sqlcmd.Parameters.Add("@shop_id", omm.shop_id);
                    sqlcmd.Parameters.Add("@visit_date", omm.visit_datetime);


                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();
                    if (dt.Rows.Count > 0)
                    {


                        omodel.status = "200";
                        omodel.message = "Revisited Shop Successfully";

                        //using (StreamWriter stream = new FileInfo((Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(uploadtext)))).AppendText())
                        //{
                        //    stream.WriteLine("    Revisit Image Upload  Start " + "Start Time:" + sdatetime + "End Time:" + DateTime.Now + "Status:Success(ShopVisitImageUpload/Revist)[200]" + "User ID:" + omm.user_id + ",ShopAPI ID :" + omm.shop_id + ", VISIT DATE" + omm.visit_datetime + ",Image :" + ImageName + " Revisit Image Upload  END ");
                        //}
                    }
                    else
                    {

                        omodel.status = "202";
                        omodel.message = "Already Data was inserted";
                        //using (StreamWriter stream = new FileInfo((Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(uploadtext)))).AppendText())
                        //{
                        //    stream.WriteLine("   Revisit  Image Upload Start " + "Start Time:" + sdatetime + "End Time:" + DateTime.Now + "Status:Records not updated(ShopVisitImageUpload/Revist)[202]" + "User ID:" + omm.user_id + ",ShopAPI ID :" + omm.shop_id + ", VISIT DATE" + omm.visit_datetime + ",Image :" + ImageName + "    Revisit Image Upload  END ");
                        //}
                       // System.IO.File.WriteAllText("\\\\10.0.8.251\\Location\\Processing\\Images\\" + omm.user_id + "_" + dates + ".xml", JsonXML);
                    }
                }
                catch
                {
                    //System.IO.File.WriteAllText("\\\\10.0.8.251\\Location\\Processing\\Images\\" + omm.user_id + "_" + dates + ".xml", JsonXML);
                }
                String folderLocation = System.Configuration.ConfigurationSettings.AppSettings["ShopSubmitXMLURL"];

                omodel.status = "200";
                omodel.message = "Revisited Shop Successfully";
                //System.IO.File.WriteAllText(folderLocation + "\\Images\\" + omm.shop_id + "_" + omm.user_id + "_" + dates + ".xml", JsonXML);

            }
            catch (Exception msg)
            {

                omodel.status = "204" + ImageName;
                omodel.message = msg.Message;
                using (StreamWriter stream = new FileInfo((Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(uploadtext)))).AppendText())
                {
                    stream.WriteLine("   Revisit Image Upload Start " + "Start Time:" + sdatetime + "End Time:" + DateTime.Now + "Status:Error(ShopVisitImageUpload/Revist)[204]" + msg.Message + "User ID:" + omm.user_id + ",ShopAPI ID :" + omm.shop_id + ", VISIT DATE" + omm.visit_datetime + ",Image :" + ImageName + "    Revisit Image Upload  END ");
                }
                // File.WriteAllText("\\\\10.0.8.251\\Location\\Processing\\user700\\" + model.user_id + "_" + dates + ".xml", JsonXML);
            }
            // var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
            return Json(omodel);
        }
    }
}
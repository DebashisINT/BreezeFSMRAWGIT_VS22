using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
//using System.Net.Http;
//using System.Web.Http;
using ShopAPI.Models;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using System.Net.Http.Formatting;
using System.Web.UI;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml.Linq;
using System.Net.Mail;


namespace ShopAPI.Controllers
{
    public class TAManagementController : Controller
    {
        [AcceptVerbs("POST")]
        public JsonResult AddTA(TAModelInput model)
        {

            TAinsertionoutput omodel = new TAinsertionoutput();
            TAModel omm = new TAModel();
            string ImageName = "";

            try
            {
                // RegisterShopInputData model = new RegisterShopInputData();

                //TextWriter tw = new StreamWriter("date.txt");
                //// write a line of text to the file
                //tw.WriteLine(DateTime.Now + model.data);
                //tw.Close();


                // close the stream

                ShopRegister oview = new ShopRegister();
                ImageName = model.document.FileName;
                string UploadFileDirectory = "~/CommonFolder/TADocument";
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

                            case "from_date":
                                {
                                    omm.from_date = value;
                                    break;
                                }


                            case "to_date":
                                {
                                    omm.to_date = value;
                                    break;
                                }

                            case "user_id":
                                {

                                    omm.user_id = value;
                                    break;
                                }

                            case "total_amount":
                                {
                                    omm.total_amount = value;
                                    break;
                                }
                            case "description":

                                omm.description = value;
                                break;


                            case "email":

                                omm.email = value;
                                break;

                           


                        }

                    }

                }
                catch (Exception msg)
                {

                    omodel.status = "206" + ImageName;
                    omodel.message = msg.Message;

                }





                ImageName = omm.session_token + '_' + omm.user_id + '_' + ImageName;
                string vPath = Path.Combine(Server.MapPath("~/CommonFolder/TADocument"), ImageName);
                model.document.SaveAs(vPath);

                string sessionId = "";



                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("Proc_FTS_TAAdd", sqlcon);
                sqlcmd.Parameters.Add("@session_token", omm.session_token);
                sqlcmd.Parameters.Add("@from_date", omm.from_date);
                sqlcmd.Parameters.Add("@to_date", omm.to_date);
                sqlcmd.Parameters.Add("@total_amount", omm.total_amount);
                sqlcmd.Parameters.Add("@description", omm.description);
                sqlcmd.Parameters.Add("@email", omm.email);
                sqlcmd.Parameters.Add("@user_id", omm.user_id);
                sqlcmd.Parameters.Add("@TAdocument_image", ImageName);

              
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "TA information added successfully";

                  //  SendMail(omm.email);
                }
                else
                {
                    omodel.status = "202";
                    omodel.message = "Data not inserted";
                }

            }
            catch (Exception msg)
            {

                omodel.status = "204" + ImageName;
                omodel.message = msg.Message;


            }
            // var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
            return Json(omodel);
        }





        public void SendMail(string mailid)
        {
            MailMessage mail = new MailMessage();
           mail.To.Add("");

            mail.From = new MailAddress("sourabh9303@gmail.com");
            mail.Subject = "Email using Gmail";
            string Body = "";
            mail.Body = Body;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential
            ("username", "password");

            //Or your Smtp Email ID and Password
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }


	}
}
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
    public class OTPConfigurationController : ApiController
    {

        [HttpPost]

        public HttpResponseMessage OTPSent(OTPClass model)
        {
            OTPClassOutput omodel = new OTPClassOutput();
            OTPClassOutput oview = new OTPClassOutput();


            try
            {
                string token = string.Empty;
                string versionname = string.Empty;
                System.Net.Http.Headers.HttpRequestHeaders headers = this.Request.Headers;
                String tokenmatch = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                String username = System.Configuration.ConfigurationSettings.AppSettings["username"];
                String password = System.Configuration.ConfigurationSettings.AppSettings["password"];
                 String Provider = System.Configuration.ConfigurationSettings.AppSettings["Provider"];
                String sender = System.Configuration.ConfigurationSettings.AppSettings["sender"];
                if (!ModelState.IsValid)
                {
                    omodel.status = "213";
                    omodel.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
                }
                else
                {
                   string OTprequest= GenerateOTP();
                    // String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                    string sessionId = "";
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("Proc_FTS_OTPsubmit", sqlcon);

                    sqlcmd.Parameters.Add("@user_id", model.user_id);
                    sqlcmd.Parameters.Add("@SessionToken", model.session_token);
                    sqlcmd.Parameters.Add("@shop_id", model.shop_id);
                    sqlcmd.Parameters.Add("@OTP", OTprequest);


                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();


                    if (dt.Rows.Count > 0)
                    {
                        omodel.Shop_Owner = Convert.ToString(dt.Rows[0]["Shop_Owner"]);
                        omodel.Shop_Owner_Contact = Convert.ToString(dt.Rows[0]["Shop_Owner_Contact"]);
                        omodel.OTP = Convert.ToString(dt.Rows[0]["OTPCode"]);
                        string messagetext = "Hi " + omodel.Shop_Owner + " Your OTP is : " + omodel.OTP;
                        string res = SmsSent(username, password, Provider, sender, omodel.Shop_Owner_Contact, messagetext, "Text");
                        if (res != "0")
                        {
                            omodel.status = "200";
                            omodel.message = "OTP Sent successfully.";
                        }
                        else
                        {

                            omodel.status = "200";
                            omodel.message = "OTP Sennding problem.";
                        }
                    }
                    else
                    {

                        omodel.status = "201";
                        omodel.message = "Invalid user credential.";

                    }

                    var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                    return message;
                }
                //}



                //else
                //{
                //    omodel.status = "205";
                //    omodel.message = "Token Id does not matched.";
                //    var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                //    return message;

                //}

            }
            catch (Exception ex)
            {


                omodel.status = "209";

                omodel.message = ex.Message;
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }






        }



        protected string GenerateOTP()
        {
           

            
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "1234567890";

            string characters = numbers;
          
               // characters += alphabets + small_alphabets + numbers;
            
            int length = 6;
            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
       
            return otp;

        }


        public  string SmsSent(string username,string password,string Provider,string senderId,string mobile,string message,string type)
        {

          //  http://5.189.187.82/sendsms/sendsms.php?username=QHEkaruna&password=baj8piv3&type=Text&sender=KARUNA&mobile=9831892083&message=HELO
            string response="";
            string url = Provider + "?username=" + username + "&password=" + password + "&type=" + type + "&sender=" + senderId + "&mobile=" + mobile + "&message=" + message;
            if (mobile.Trim() != "")
            {
                try
                {
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    response=httpResponse.StatusCode.ToString();
                }
                catch
                {
                    return "0";

                }
            }
            return response;
        }



        [HttpPost]
        public HttpResponseMessage OTPVerification(OTPClass model)
        {
            OTPClassOutput omodel = new OTPClassOutput();
           
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
                    String profileImg = System.Configuration.ConfigurationSettings.AppSettings["ProfileImageURL"];
                    Encryption epasswrd = new Encryption();
                   
                    
                    DataSet dt = new DataSet();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();

                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();


                    sqlcmd = new SqlCommand("Proc_FTS_OTPVerification", sqlcon);
                    sqlcmd.Parameters.Add("@user_id", model.user_id);
                    sqlcmd.Parameters.Add("@SessionToken", model.session_token);
                    sqlcmd.Parameters.Add("@shop_id", model.shop_id);
                    sqlcmd.Parameters.Add("@OTP", model.OTP);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();


                    if (dt.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToString(dt.Tables[0].Rows[0]["success"]) == "1")
                        {
                         
                            omodel.status = "200";
                            omodel.OTP = Convert.ToString(dt.Tables[0].Rows[0]["OTP"]); 
                            omodel.Shop_Owner = Convert.ToString(dt.Tables[0].Rows[0]["Shop_Owner"]);
                            omodel.Shop_Owner_Contact = Convert.ToString(dt.Tables[0].Rows[0]["Shop_Owner_Contact"]);
                            omodel.message = "OTP successfully verified.";

                        }

                        else if (Convert.ToString(dt.Tables[0].Rows[0]["success"]) == "0")
                        {
                            omodel.status = "201";
                            omodel.OTP = Convert.ToString(dt.Tables[0].Rows[0]["OTP"]);
                            omodel.Shop_Owner = Convert.ToString(dt.Tables[0].Rows[0]["Shop_Owner"]);
                            omodel.Shop_Owner_Contact = Convert.ToString(dt.Tables[0].Rows[0]["Shop_Owner_Contact"]);  
                            omodel.message = "OTP Already Verified";
                        }

                        else if (Convert.ToString(dt.Tables[0].Rows[0]["success"]) == "3")
                        {
                            omodel.status = "202";
                            omodel.OTP = model.OTP;
                            omodel.message = "Otp verification has failed.";
                        }

                    }
                    else
                    {
                        omodel.status = "203";
                        omodel.message = "Invalid user credential.";

                    }

                    var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                    return message;
                }

            }
            catch (Exception ex)
            {


                omodel.status = "209";

                omodel.message = ex.Message;
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }

        }



    }


}

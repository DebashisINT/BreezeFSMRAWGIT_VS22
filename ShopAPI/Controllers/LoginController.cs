using ShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;


using System.Web.Mvc;



namespace ShopAPI.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public ActionResult Login(ClassLogin model)
        {

            ClassLoginOutput omodel = new ClassLoginOutput();

            try
            {
 
                    String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                    string sessionId = HttpContext.Session.SessionID;

                    DataTable dt = new DataTable();
                    Encryption epasswrd = new Encryption();
                    string Encryptpass = epasswrd.Encrypt(model.password);
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];

                    SqlCommand sqlcmd = new SqlCommand();

                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();

                    sqlcmd = new SqlCommand("Sp_ApiLogin", sqlcon);
                    sqlcmd.Parameters.Add("@userName", model.username);
                    sqlcmd.Parameters.Add("@password", Encryptpass);
                    sqlcmd.Parameters.Add("@SessionToken", sessionId);
                    sqlcmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0" && dt.Rows[0][0].ToString() != "-1")
                        {
                            //omodel = APIHelperMethods.ToModel<UserLoginOutputParameters>(dt);
                            //omodel.session_token = sessionId;
                            //return Json(omodel);
                        }
                        else
                        {
                            if (dt.Rows[0][0].ToString() == "-1")
                            {
                                //omodelerror.ResponseCode = "202";
                                //omodelerror.Responsedetails = "Already User Logged In";
                                //return Json(omodelerror);

                            }
                            else
                            {
                                //omodelerror.ResponseCode = "201";
                                //omodelerror.Responsedetails = "Invalid User name /password";
                                //return Json(omodelerror);
                            }
                        }

                    }
                    else
                    {
                        //omodelerror.ResponseCode = "201";
                        //omodelerror.Responsedetails = "Invalid User name /password";
                        //return Json(omodelerror);
                    }


               

            }
            catch
            {


               

            }

            return Json(omodel);
        }


   


        // PUT api/values/5
        public void Put(int id)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

	}
}
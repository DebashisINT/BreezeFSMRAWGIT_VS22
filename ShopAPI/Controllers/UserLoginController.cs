using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ShopAPI.Models;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.IO;
using System.Text;
using System.Device;
using System.Device.Location;
using System.Data.Spatial;
using System.Xml;

namespace ShopAPI.Controllers
{
    public class UserLoginController : ApiController
    {
        string Address_ShortName = "";
        string Address_country = "";
        string Address_administrative_area_level_1 = "";
        string Address_administrative_area_level_2 = "";
        string Address_administrative_area_level_3 = "";
        string Address_colloquial_area = "";
        string Address_locality = "";
        string Address_sublocality = "";
        string Address_neighborhood = "";


        [HttpPost]
        public HttpResponseMessage Login(ClassLogin model)
        {
            ClassLoginOutput omodel = new ClassLoginOutput();
            UserClass oview = new UserClass();
            UserClasscounting ocounting = new UserClasscounting();
            List<WorkTypeslogin> worktype = new List<WorkTypeslogin>();
            List<StateListLogin> statelist = new List<StateListLogin>();

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
                    String profileImg = System.Configuration.ConfigurationManager.AppSettings["ProfileImageURL"];
                    Encryption epasswrd = new Encryption();
                    string Encryptpass = epasswrd.Encrypt(model.password);
                    string sessionId = "";


                    sessionId = HttpContext.Current.Session.SessionID;
                    string location_name = "Login from ";
                   // location_name = "Login from  " + RetrieveFormatedAddressNew(model.latitude, model.longitude);

                    if(!string.IsNullOrEmpty(model.address))
                    {

                        location_name =location_name + model.address;
                    }

                    //  location_name = "Login from  " + RetrieveFormatedAddress(Convert.ToDouble(model.latitude), Convert.ToDouble(model.longitude));


                    DataSet dt = new DataSet();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();

                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();


                    sqlcmd = new SqlCommand("Sp_ApiShopUserLogin", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@userName", model.username);
                    sqlcmd.Parameters.AddWithValue("@password", Encryptpass);
                    sqlcmd.Parameters.AddWithValue("@SessionToken", sessionId);
                    sqlcmd.Parameters.AddWithValue("@latitude", model.latitude);
                    sqlcmd.Parameters.AddWithValue("@longitude", model.longitude);
                    sqlcmd.Parameters.AddWithValue("@login_time", model.login_time);
                    sqlcmd.Parameters.AddWithValue("@ImeiNo", model.Imei);
                    sqlcmd.Parameters.AddWithValue("@location_name", location_name);
                    sqlcmd.Parameters.AddWithValue("@version_name", model.version_name);
                    sqlcmd.Parameters.AddWithValue("@Weburl", profileImg);
                    sqlcmd.Parameters.AddWithValue("@device_token", model.device_token);
                    
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Tables[1].Rows.Count > 0)
                    {

                        if (Convert.ToString(dt.Tables[1].Rows[0]["success"]) == "200")
                        {
                            oview = APIHelperMethods.ToModel<UserClass>(dt.Tables[1]);
                            ocounting = APIHelperMethods.ToModel<UserClasscounting>(dt.Tables[0]);
                            if (dt.Tables.Count == 3)
                            {
                                if (dt.Tables[2] != null && dt.Tables[2].Rows.Count > 0)
                                {
                                    statelist = APIHelperMethods.ToModelList<StateListLogin>(dt.Tables[2]);
                                }
                            }
                            
                            omodel.status = "200";
                            omodel.session_token = sessionId;
                            omodel.user_details = oview;
                            omodel.user_count = ocounting;
                            omodel.state_list = statelist;
                            omodel.message = "User successfully logged in.";
                           
                        }

                        else if (Convert.ToString(dt.Tables[1].Rows[0]["success"]) == "207")
                        {
                            omodel.status = "207";
                            omodel.message = "Your IMEI is not authorized. Please contact with Administrator";
                        }

                        else if (Convert.ToString(dt.Tables[1].Rows[0]["success"]) == "202")
                        {
                            omodel.status = "202";
                            omodel.message = "Invalid user credential.";
                        }

                        else if (Convert.ToString(dt.Tables[1].Rows[0]["success"]) == "220")
                        {
                            omodel.status = "220";
                            omodel.message = "Login time expired for the day.";
                        }
                        else if (Convert.ToString(dt.Tables[1].Rows[0]["success"]) == "206")
                        {
                            omodel.status = "206";
                            omodel.message = Convert.ToString(dt.Tables[1].Rows[0]["Dynamic_message"]);// "New version is available now. Please update it from the Play Store. Unless you can't login into the app. ";//Html.fromHtml('http://www.google.com')"
                        }

                    }
                    else
                    {
                        omodel.status = "202";
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



        string location = string.Empty;



        public string RetrieveFormatedAddress(string lat, string lng)
        {
            string address = "";
            string locationName = "";

            string url = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&sensor=false&key=AIzaSyCbYMZjnt8T6yivYfIa4_R9oy-L3SIYyrQ", lat, lng);

            try 
            {
                XElement xml = XElement.Load(url);
                if (xml.Element("status").Value == "OK")
                {
                    locationName = string.Format("{0}",
                    xml.Element("result").Element("formatted_address").Value);
                }
            }
            catch
            {
                locationName = "";
            }




            return locationName;

        }

       

        public static string RetrieveFormatedAddressNew(string lat, string lng)
        {
             string baseUri =  "http://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&sensor=false";
             string location = string.Empty;

            string requestUri = string.Format(baseUri, lat, lng);

            using (WebClient wc = new WebClient())
            {
                string result = wc.DownloadString(requestUri);
                var xmlElm = XElement.Parse(result);
                var status = (from elm in xmlElm.Descendants()
                              where
                                  elm.Name == "status"
                              select elm).FirstOrDefault();
                if (status.Value.ToLower() == "ok")
                {
                    var res = (from elm in xmlElm.Descendants()
                               where
                                   elm.Name == "formatted_address"
                               select elm).FirstOrDefault();
                    requestUri = res.Value;
                    location = requestUri;
                }
            }

            return location;
        }

        static string baseUri =
  "http://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&sensor=true";


        //public static string RetrieveFormatedAddress(double lat, double lng)
        //{
        //    string locationfetch = "";
        //    string url = "https://maps.google.com/maps/api/geocode/xml?latlng={0},{1}&sensor=false&key=AIzaSyBif3telvlrJn61kkLXDQA0ViQdDVJWifk";
        //    url = string.Format(url, lat, lng);
        //    WebRequest request = WebRequest.Create(url);
        //    using (WebResponse response = (HttpWebResponse)request.GetResponse())
        //    {
        //        using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
        //        {
        //            DataSet dsResult = new DataSet();
        //            dsResult.ReadXml(reader);
        //            locationfetch = dsResult.Tables["result"].Rows[0]["formatted_address"].ToString();
        //        }
        //    }
        //    return locationfetch;
        //}




        [HttpPost]
        public HttpResponseMessage SubmitHomeLocation(UserHomeLocation model)
        {
            ShopdaywiseOutput omodel = new ShopdaywiseOutput();
            List<ShopdaywiseList> oview = new List<ShopdaywiseList>();
            List<ShopdaywiseList> oview1 = new List<ShopdaywiseList>();
            ShopList odata = new ShopList();
            List<ShopList> shoplst = new List<ShopList>();
            ShopdaywiseList odelails = new ShopdaywiseList();
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


                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("Proc_FTS_UserHomeaddress", sqlcon);
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@latitude", model.latitude);
                sqlcmd.Parameters.Add("@longitude", model.longitude);
                sqlcmd.Parameters.Add("@address", model.address);
                sqlcmd.Parameters.Add("@city", model.city);
                sqlcmd.Parameters.Add("@state", model.state);
                sqlcmd.Parameters.Add("@pincode", model.pincode);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {

                    omodel.status = "200";
                    omodel.message = "Home address submitted successfully.";

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


        [HttpPost]
        public HttpResponseMessage ChangePass(ChangePassword model)
        {
            ChangePassOutput omodel = new ChangePassOutput();
           
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
                    Encryption epasswrd = new Encryption();
                    string OldEncryptpass = epasswrd.Encrypt(model.old_pwd);
                    string NewEncryptpass = epasswrd.Encrypt(model.new_pwd);
                    string sessionId = "";

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APIUserPasswordChange", sqlcon);
                    sqlcmd.Parameters.Add("@user_id", model.user_id);
                    sqlcmd.Parameters.Add("@Old_password", OldEncryptpass);
                    sqlcmd.Parameters.Add("@New_password", NewEncryptpass);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt!=null && dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["MSG"].ToString()=="Successfully changed password.")
                        {
                            omodel.status = "200";
                            omodel.message = dt.Rows[0]["MSG"].ToString();
                        }
                        else
                        {
                            omodel.status = "202";
                            omodel.message = dt.Rows[0]["MSG"].ToString();
                        }
                    }
                    else
                    {
                        omodel.status = "202";
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


        [HttpPost]
        public HttpResponseMessage GetUser(GetUserInput model)
        {
            GetUserOutput omodel = new GetUserOutput();
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
                   
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_GetUserLoginByPhone", sqlcon);
                    sqlcmd.Parameters.Add("@Phone", model.Phone);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt != null && dt.Rows.Count > 0)
                    {
                            omodel.status = "200";
                            omodel.message = dt.Rows[0]["MSG"].ToString();
                            omodel.user_id = dt.Rows[0]["user_id"].ToString();
                    }
                    else
                    {
                        omodel.status = "202";
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

        [HttpPost]
        public HttpResponseMessage GetUserList(GetUserListInput model)
        {
            GetUserListOutPut omodel = new GetUserListOutPut();
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

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_ActiveUserList", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Successfully get User list.";
                        omodel.UserList = APIHelperMethods.ToModelList<GetUserList>(dt);
                    }
                    else
                    {
                        omodel.status = "202";
                        omodel.message = "No data found.";
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

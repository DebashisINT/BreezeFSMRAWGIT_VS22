using BreezeERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;



namespace BreezeERPAPI.Controllers
{
    public class APIController : Controller
    {

        ///string connectionstring="Server=You server name or comp name;Database=Yourdatabasename;Trusted_Connectopn= True"); 

        AndroidFCMPushNotificationStatus pushmodel = new AndroidFCMPushNotificationStatus();

        #region User Login Logout
        [AcceptVerbs("POST")]
        public JsonResult UserLogin(UserLoginInputParameters model)
        {
            UserLoginOutputParameters omodel = new UserLoginOutputParameters();
            ErrorModel omodelerror = new ErrorModel();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == model.Token)
                {
                    string sessionId = HttpContext.Session.SessionID;

                    DataTable dt = new DataTable();
                    Encryption epasswrd = new Encryption();
                    string Encryptpass = epasswrd.Encrypt(model.password);
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];

                    SqlCommand sqlcmd = new SqlCommand();

                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("Sp_ApiLogin", sqlcon);
                    sqlcmd.Parameters.Add("@userName", model.user_name);
                    sqlcmd.Parameters.Add("@password", Encryptpass);
                    sqlcmd.Parameters.Add("@DeviceId", model.device_id);
                    sqlcmd.Parameters.Add("@Devicetype", model.device_type);
                    sqlcmd.Parameters.Add("@SessionToken", sessionId);
                    sqlcmd.Parameters.Add("@Imei_no", model.Imei_no);

                    sqlcmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {


                        if (dt.Rows[0][0].ToString() != "0" && dt.Rows[0][0].ToString() != "-1")
                        {
                            omodel = APIHelperMethods.ToModel<UserLoginOutputParameters>(dt);
                            omodel.session_token = sessionId;
                            return Json(omodel);
                        }
                        else
                        {
                            if (dt.Rows[0][0].ToString() == "-1")
                            {
                                omodelerror.ResponseCode = "202";
                                omodelerror.Responsedetails = "Already User Logged In";
                                return Json(omodelerror);

                            }
                            else
                            {
                                omodelerror.ResponseCode = "201";
                                omodelerror.Responsedetails = "Invalid User name /password";
                                return Json(omodelerror);
                            }
                        }

                    }
                    else
                    {
                        omodelerror.ResponseCode = "201";
                        omodelerror.Responsedetails = "Invalid User name /password";
                        return Json(omodelerror);
                    }


                }
                else
                {
                    omodelerror.ResponseCode = "103";
                    omodelerror.Responsedetails = "Token Id not match";
                    return Json(omodelerror);

                }

            }
            catch
            {

                omodel.ResponseCode = "103";
                omodel.Responsedetails = "Error occured";
                return Json(omodelerror);

            }


        }

        [AcceptVerbs("POST")]
        public JsonResult UserLogout(string user_id, string Token, int User_login_Id, string session_token)
        {
            UserLogOutputParameters omodel = new UserLogOutputParameters();
            ErrorModel omodelerror = new ErrorModel();
            var oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            try
            {

                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == Token)
                {
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);


                    sqlcon.Open();

                    sqlcmd = new SqlCommand("Sp_ApiLogout", sqlcon);
                    sqlcmd.Parameters.Add("@userId", user_id);
                    sqlcmd.Parameters.Add("@User_login_Id", User_login_Id);
                    sqlcmd.Parameters.Add("@SessionToken", session_token);


                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();


                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {


                            omodel = APIHelperMethods.ToModel<UserLogOutputParameters>(dt);

                            return Json(omodel);
                        }
                        else
                        {
                            omodelerror.ResponseCode = "201";
                            omodelerror.Responsedetails = "Invalid User name /password";

                            return Json(omodelerror);

                        }

                    }
                    else
                    {

                        omodelerror.ResponseCode = "201";
                        omodelerror.Responsedetails = "Invalid User name /password";
                        return Json(omodelerror);

                    }

                }
                else
                {
                    omodelerror.ResponseCode = "103";
                    omodelerror.Responsedetails = "Token Id not match";
                    return Json(omodelerror);

                }


            }
            catch
            {
                omodel.ResponseCode = "103";
                omodel.Responsedetails = "Error occured";
                return Json(omodelerror);
            }

        }

        #endregion

        #region CountryList
        [AcceptVerbs("GET")]
        public JsonResult CountryList(string Token)
        {
            countyryListRespose omodel = new countyryListRespose();
            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == Token)
                {
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);


                    sqlcon.Open();

                    sqlcmd = new SqlCommand("APICountryList", sqlcon);


                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();


                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {

                            omodel.ResponseCode = "200";
                            omodel.Responsedetails = "Success";
                            omodel.country_list = APIHelperMethods.ToModelList<CountryList>(dt);

                            return Json(omodel, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "Error";
                            return Json(oerror, JsonRequestBehavior.AllowGet);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "Error";

                        return Json(oerror, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {

                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "token Id not match";

                    return Json(oerror, JsonRequestBehavior.AllowGet);
                }

            }
            catch
            {
                oerror.ResponseCode = "201";
                oerror.Responsedetails = "Error";
                return Json(oerror, JsonRequestBehavior.AllowGet);
            }





        }
        #endregion

        #region StateList
        [AcceptVerbs("GET")]
        public JsonResult StateList(string Token)
        {
            StateListResponse omodel = new StateListResponse();
            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == Token)
                {

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);

                    sqlcon.Open();

                    sqlcmd = new SqlCommand("APIStateList", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {

                            omodel.ResponseCode = "200";
                            omodel.Responsedetails = "Success";
                            omodel.state_list = APIHelperMethods.ToModelList<StateList>(dt);
                            omodel.isUpdated = "false";
                            return Json(omodel, JsonRequestBehavior.AllowGet);

                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "Error";
                            return Json(oerror, JsonRequestBehavior.AllowGet);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "Error";

                        return Json(oerror);
                    }
                }
                else
                {

                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not Match";
                    return Json(oerror, JsonRequestBehavior.AllowGet);
                }



            }
            catch
            {
                oerror.ResponseCode = "201";
                oerror.Responsedetails = "Error";
                return Json(oerror, JsonRequestBehavior.AllowGet);
            }





        }
        #endregion

        #region CityList
        [AcceptVerbs("GET")]
        public JsonResult CityList(string Token)
        {
            CityListResponse omodel = new CityListResponse();
            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == Token)
                {
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);


                    sqlcon.Open();

                    sqlcmd = new SqlCommand("APICityList", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {

                            omodel.ResponseCode = "200";
                            omodel.Responsedetails = "Success";
                            omodel.city_list = APIHelperMethods.ToModelList<CityList>(dt);
                            omodel.isUpdated = "false";
                            return Json(omodel, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "Error";
                            return Json(oerror, JsonRequestBehavior.AllowGet);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "Error";

                        return Json(oerror, JsonRequestBehavior.AllowGet);
                    }


                }
                else
                {
                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not Match";

                    return Json(oerror, JsonRequestBehavior.AllowGet);

                }

            }
            catch
            {
                oerror.ResponseCode = "201";
                oerror.Responsedetails = "Error";
                return Json(oerror, JsonRequestBehavior.AllowGet);
            }





        }
        #endregion

        #region Pin Code
        [AcceptVerbs("POST")]
        public JsonResult PinCodeList(string Token, string city_Id)
        {
            PincodeListResponse omodel = new PincodeListResponse();
            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == Token)
                {
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();

                    sqlcmd = new SqlCommand("APIPincodeList", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@city_Id", city_Id);
                    //  SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    SqlDataReader rdr = sqlcmd.ExecuteReader();
                    //    da.Fill(dt);


                    List<PincodeList> pincodelist1 = new List<PincodeList>();
                    while (rdr.Read())
                    {
                        pincodelist1.Add(new PincodeList()
                        {

                            pincode_id = (int)rdr["pincode_id"],
                            pincode_no = (string)rdr["pincode_no"],
                            city_id = (decimal)rdr["city_id"]

                        });

                    }
                    sqlcon.Close();
                    rdr.Close();

                    if (pincodelist1.Count() > 0)
                    {
                        omodel.ResponseCode = "200";
                        omodel.Responsedetails = "Success";
                        //   omodel.pincodelist = APIHelperMethods.ToModelList<PincodeList>(dt);
                        omodel.pincodelist = pincodelist1;
                        return Json(omodel);
                    }

                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "Error";
                        return Json(oerror);
                    }


                    //if (dt.Rows.Count > 0)
                    //{
                    //    if (dt.Rows[0][0].ToString() != "0")
                    //    {
                    //        omodel.ResponseCode = "200";
                    //        omodel.Responsedetails = "Success";
                    //     //   omodel.pincodelist = APIHelperMethods.ToModelList<PincodeList>(dt);
                    //        omodel.pincodelist = pincodelist1;
                    //        return Json(omodel);
                    //    }
                    //    else
                    //    {
                    //        oerror.ResponseCode = "201";
                    //        oerror.Responsedetails = "Error";
                    //        return Json(oerror);
                    //    }

                    //}
                    //else
                    //{


                    //    oerror.ResponseCode = "201";
                    //    oerror.Responsedetails = "Error";

                    //    return Json(oerror);
                    //}


                }
                else
                {
                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not Match";

                    return Json(oerror);

                }

            }
            catch
            {
                oerror.ResponseCode = "201";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }





        }
        #endregion

        #region Customer Add
        [AcceptVerbs("POST")]
        public JsonResult CustomerAdd(CustomerInputParameters model)
        {
            CustomerOutputParameters omodel = new CustomerOutputParameters();

            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == model.Token)
                {

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);


                    sqlcon.Open();

                    sqlcmd = new SqlCommand("APICustomerAdd", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@mobile_no", model.mobile_no);
                    sqlcmd.Parameters.Add("@alternate_mobile_no", model.alternate_mobile_no);
                    sqlcmd.Parameters.Add("@email", model.email);
                    sqlcmd.Parameters.Add("@pan_number", model.pan_number);
                    sqlcmd.Parameters.Add("@aadhar_no", model.aadhar_no);
                    sqlcmd.Parameters.Add("@cust_name", model.cust_name);
                    sqlcmd.Parameters.Add("@gender", model.gender);
                    sqlcmd.Parameters.Add("@date_of_birth", model.date_of_birth);
                    sqlcmd.Parameters.Add("@block_no", model.block_no);
                    sqlcmd.Parameters.Add("@street_no", model.street_no);
                    sqlcmd.Parameters.Add("@flat_no", model.flat_no);
                    sqlcmd.Parameters.Add("@floor", model.floor);
                    sqlcmd.Parameters.Add("@landmark", model.landmark);
                    sqlcmd.Parameters.Add("@country", model.country);
                    sqlcmd.Parameters.Add("@state", model.state);
                    sqlcmd.Parameters.Add("@city", model.city);
                    sqlcmd.Parameters.Add("@pin_code", model.pin_code);
                    sqlcmd.Parameters.Add("@sales_man_id", model.sales_man_id);


                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {

                            omodel = APIHelperMethods.ToModel<CustomerOutputParameters>(dt);

                            return Json(omodel);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "Error";
                            return Json(oerror);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "Error";

                        return Json(oerror);
                    }

                }
                else
                {
                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not match";

                    return Json(oerror);

                }


            }
            catch
            {
                oerror.ResponseCode = "201";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }

        }
        #endregion

        #region Customer  Update
        [AcceptVerbs("POST")]
        public JsonResult CustomerUpdate(CustomerupdateInputParameters model)
        {
            CustomerUpdateOutputParameters omodel = new CustomerUpdateOutputParameters();
            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == model.Token)
                {

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);


                    sqlcon.Open();

                    sqlcmd = new SqlCommand("APICustomerUpdate", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@mobile_no", model.mobile_no);
                    sqlcmd.Parameters.Add("@alternate_mobile_no", model.alternate_mobile_no);
                    sqlcmd.Parameters.Add("@email", model.email);
                    sqlcmd.Parameters.Add("@pan_number", model.pan_number);
                    sqlcmd.Parameters.Add("@aadhar_no", model.aadhar_no);
                    sqlcmd.Parameters.Add("@cust_name", model.cust_name);
                    sqlcmd.Parameters.Add("@gender", model.gender);
                    sqlcmd.Parameters.Add("@date_of_birth", model.date_of_birth);
                    sqlcmd.Parameters.Add("@block_no", model.block_no);
                    sqlcmd.Parameters.Add("@street_no", model.street_no);
                    sqlcmd.Parameters.Add("@flat_no", model.flat_no);
                    sqlcmd.Parameters.Add("@floor", model.floor);
                    sqlcmd.Parameters.Add("@landmark", model.landmark);
                    sqlcmd.Parameters.Add("@country", model.country);
                    sqlcmd.Parameters.Add("@state", model.state);
                    sqlcmd.Parameters.Add("@city", model.city);
                    sqlcmd.Parameters.Add("@pin_code", model.pin_code);
                    sqlcmd.Parameters.Add("@sales_man_id", model.sales_man_id);
                    sqlcmd.Parameters.Add("@Customer_Id", model.customer_id);

                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {

                            omodel = APIHelperMethods.ToModel<CustomerUpdateOutputParameters>(dt);

                            return Json(omodel);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "Error";
                            return Json(oerror);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "Error";

                        return Json(oerror);
                    }

                }
                else
                {
                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not match";

                    return Json(oerror);

                }


            }
            catch
            {
                oerror.ResponseCode = "201";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }

        }
        #endregion

        #region PRoductList
        [AcceptVerbs("GET")]
        public JsonResult ProductList(string Token, int pageno, int rowcount)
        {
            ProductListOutput omodel = new ProductListOutput();
            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == Token)
                {

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);

                    sqlcon.Open();

                    sqlcmd = new SqlCommand("API_Productlist", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@PageNo", pageno);
                    sqlcmd.Parameters.Add("@Pagerows", rowcount);
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();


                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {
                            omodel.ResponseCode = "200";
                            omodel.Responsedetails = "Success";
                            omodel.totalcount = Convert.ToInt32(dt.Rows[0]["totalcount"]);
                            omodel.product_details = APIHelperMethods.ToModelList<ProductDetails>(dt);
                            return Json(omodel, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "No record Found.";
                            return Json(oerror, JsonRequestBehavior.AllowGet);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "No Record found";

                        return Json(oerror, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {

                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not Match";
                    return Json(oerror, JsonRequestBehavior.AllowGet);
                }



            }
            catch
            {
                oerror.ResponseCode = "201";
                oerror.Responsedetails = "Error";
                return Json(oerror, JsonRequestBehavior.AllowGet);
            }

        }
        #endregion

        #region Product Search
        [AcceptVerbs("POST")]
        public JsonResult ProductSearch(string product_name, string Token, int pageno, int rowcount)
        {
            ProductsearchOutput omodel = new ProductsearchOutput();
            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == Token)
                {

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();


                    sqlcmd = new SqlCommand("API_Productlist", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@ProductName", product_name);
                    sqlcmd.Parameters.Add("@PageNo", pageno);
                    sqlcmd.Parameters.Add("@Pagerows", rowcount);
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();



                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {
                            omodel.ResponseCode = "200";
                            omodel.Responsedetails = "Success";
                            omodel.totalcount = Convert.ToInt32(dt.Rows[0]["totalcount"]);
                            omodel.product_details = APIHelperMethods.ToModelList<ProductDetails>(dt);
                            return Json(omodel);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "No record Found.";
                            return Json(oerror);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "No record Found.";

                        return Json(oerror);
                    }
                }
                else
                {

                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not Match";
                    return Json(oerror);
                }



            }
            catch
            {
                oerror.ResponseCode = "201";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }

        }
        #endregion

        #region  Brands List

        [AcceptVerbs("GET")]
        public JsonResult BrandsList(string Token, int pageno, int rowcount)
        {

            Brandlistoutput omodel = new Brandlistoutput();
            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == Token)
                {

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);


                    sqlcon.Open();

                    sqlcmd = new SqlCommand("API_Brandlist", sqlcon);
                    sqlcmd.Parameters.Add("@PageNo", pageno);
                    sqlcmd.Parameters.Add("@Pagerows", rowcount);

                    sqlcmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();


                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {
                            omodel.ResponseCode = "200";
                            omodel.totalcount = Convert.ToInt32(dt.Rows[0]["totalcount"]);
                            omodel.Responsedetails = "Success";
                            omodel.brand_details = APIHelperMethods.ToModelList<Brands>(dt);
                            return Json(omodel, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "No record Found.";
                            return Json(oerror, JsonRequestBehavior.AllowGet);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "No Record found";

                        return Json(oerror, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {

                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not Match";
                    return Json(oerror, JsonRequestBehavior.AllowGet);
                }

            }
            catch
            {
                oerror.ResponseCode = "201";
                oerror.Responsedetails = "Error";
                return Json(oerror, JsonRequestBehavior.AllowGet);
            }


        }
        #endregion

        #region  Brands Search

        [AcceptVerbs("POST")]
        public JsonResult BrandsSearch(string brand_name, string Token, int pageno, int rowcount)
        {

            Brandlistoutput omodel = new Brandlistoutput();
            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == Token)
                {

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);


                    sqlcon.Open();

                    sqlcmd = new SqlCommand("API_Brandlist", sqlcon);
                    sqlcmd.Parameters.Add("@BrandName", brand_name);
                    sqlcmd.Parameters.Add("@PageNo", pageno);
                    sqlcmd.Parameters.Add("@Pagerows", rowcount);


                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();


                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {
                            omodel.ResponseCode = "200";
                            omodel.Responsedetails = "Success";
                            omodel.totalcount = Convert.ToInt32(dt.Rows[0]["totalcount"]);
                            omodel.brand_details = APIHelperMethods.ToModelList<Brands>(dt);
                            return Json(omodel, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "No record Found.";
                            return Json(oerror, JsonRequestBehavior.AllowGet);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "No Record found";

                        return Json(oerror, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {

                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not Match";
                    return Json(oerror, JsonRequestBehavior.AllowGet);
                }

            }
            catch
            {
                oerror.ResponseCode = "201";
                oerror.Responsedetails = "Error";
                return Json(oerror, JsonRequestBehavior.AllowGet);
            }


        }
        #endregion

        #region  Category List

        [AcceptVerbs("GET")]
        public JsonResult CategoryList(string Token, int pageno, int rowcount)
        {

            Categorylistoutput omodel = new Categorylistoutput();
            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == Token)
                {

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);


                    sqlcon.Open();

                    sqlcmd = new SqlCommand("API_CategoryList", sqlcon);
                    sqlcmd.Parameters.Add("@PageNo", pageno);
                    sqlcmd.Parameters.Add("@Pagerows", rowcount);

                    sqlcmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();


                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {
                            omodel.ResponseCode = "200";
                            omodel.Responsedetails = "Success";
                            omodel.totalcount = Convert.ToInt32(dt.Rows[0]["totalcount"]);
                            omodel.category_details = APIHelperMethods.ToModelList<CategoryClass>(dt);
                            return Json(omodel, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "No record Found.";
                            return Json(oerror, JsonRequestBehavior.AllowGet);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "No Record found";

                        return Json(oerror, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {

                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not Match";
                    return Json(oerror, JsonRequestBehavior.AllowGet);
                }

            }
            catch
            {
                oerror.ResponseCode = "201";
                oerror.Responsedetails = "Error";
                return Json(oerror, JsonRequestBehavior.AllowGet);
            }


        }
        #endregion

        #region  Category Search

        [AcceptVerbs("POST")]
        public JsonResult CategorySearch(string category_name, string Token, int pageno, int rowcount)
        {

            Categorylistoutput omodel = new Categorylistoutput();
            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == Token)
                {

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);


                    sqlcon.Open();

                    sqlcmd = new SqlCommand("API_CategoryList", sqlcon);
                    sqlcmd.Parameters.Add("@CategoryName", category_name);
                    sqlcmd.Parameters.Add("@PageNo", pageno);
                    sqlcmd.Parameters.Add("@Pagerows", rowcount);

                    sqlcmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();


                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {
                            omodel.ResponseCode = "200";
                            omodel.Responsedetails = "Success";
                            omodel.totalcount = Convert.ToInt32(dt.Rows[0]["totalcount"]);
                            omodel.category_details = APIHelperMethods.ToModelList<CategoryClass>(dt);
                            return Json(omodel, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "No record Found.";
                            return Json(oerror, JsonRequestBehavior.AllowGet);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "No Record found";

                        return Json(oerror, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {

                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not Match";
                    return Json(oerror, JsonRequestBehavior.AllowGet);
                }

            }
            catch
            {
                oerror.ResponseCode = "201";
                oerror.Responsedetails = "Error";
                return Json(oerror, JsonRequestBehavior.AllowGet);
            }


        }
        #endregion

        #region Product Search By Category and Brand
        [AcceptVerbs("POST")]
        public JsonResult ProductSearchByCategoryBrand(string[] brand_ids, string[] category_ids, string Token, int pageno, int rowcount)
        {

            ProductsearchOutput omodel = new ProductsearchOutput();
            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == Token)
                {

                    //string bandsjoin = string.Concat(brand_ids);
                    //string Categoryclass = string.Concat(category_ids);

                    string bandsjoin = string.Join(",", brand_ids);
                    string Categoryclass = string.Join(",", category_ids);
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();


                    sqlcmd = new SqlCommand("API_Productlist", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@CategoryId", Categoryclass);
                    sqlcmd.Parameters.Add("@BrandId", bandsjoin);
                    sqlcmd.Parameters.Add("@PageNo", pageno);
                    sqlcmd.Parameters.Add("@Pagerows", rowcount);

                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {
                            omodel.ResponseCode = "200";
                            omodel.Responsedetails = "Success";
                            omodel.totalcount = Convert.ToInt32(dt.Rows[0]["totalcount"]);
                            omodel.product_details = APIHelperMethods.ToModelList<ProductDetails>(dt);
                            return Json(omodel);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "No record Found.";
                            return Json(oerror);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "No record Found.";

                        return Json(oerror);
                    }
                }
                else
                {

                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not Match";
                    return Json(oerror);
                }



            }
            catch
            {
                oerror.ResponseCode = "201";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }

        }
        #endregion

        #region Cutomer Basket Add
        [AcceptVerbs("POST")]
        public JsonResult BasketAdd(BasketInputParameters model)
        {
            BasketOutputParameters omodel = new BasketOutputParameters();

            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == model.Token)
                {

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);


                    sqlcon.Open();

                    sqlcmd = new SqlCommand("BasketAdd", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@product_id", model.product_id);
                    sqlcmd.Parameters.Add("@product_price", model.product_price);
                    sqlcmd.Parameters.Add("@customer_id", model.customer_id);
                    sqlcmd.Parameters.Add("@quantity", model.quantity);
                    sqlcmd.Parameters.Add("@salesman_id", model.salesman_id);
                    sqlcmd.Parameters.Add("@discount_percent", model.discount_percent);

                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {

                            omodel = APIHelperMethods.ToModel<BasketOutputParameters>(dt);

                            return Json(omodel, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "Error";
                            return Json(oerror);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "Error";

                        return Json(oerror);
                    }

                }
                else
                {
                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not match";

                    return Json(oerror);

                }


            }
            catch
            {
                oerror.ResponseCode = "201";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }

        }

        #endregion

        #region Saleonprogress
        [AcceptVerbs("GET")]
        public JsonResult Saleonprogress(string user_id, string Token, int pageno, int rowcount)
        {
            saleProgressOutputpara omodel = new saleProgressOutputpara();

            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == Token)
                {

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);


                    sqlcon.Open();
                    sqlcmd = new SqlCommand("SaleOnProgress_SaleDetails", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@UserId", user_id);
                    sqlcmd.Parameters.Add("@PageNo", pageno);
                    sqlcmd.Parameters.Add("@Pagerows", rowcount);

                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {
                            omodel.ResponseCode = "200";
                            omodel.Responsedetails = "Success";
                            omodel.totalcount = Convert.ToInt32(dt.Rows[0]["totalcount"]);
                            omodel.sale_on_progress_list = APIHelperMethods.ToModelList<Customer>(dt);

                            return Json(omodel, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "Error";
                            return Json(oerror);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "no record exists";

                        return Json(oerror, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not match";

                    return Json(oerror, JsonRequestBehavior.AllowGet);

                }


            }
            catch
            {
                oerror.ResponseCode = "201";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }

        }

        #endregion

        #region Saleonprogress Search
        [AcceptVerbs("POST")]
        public JsonResult SaleonprogressSearch(saleProgressSearchInputpara model)
        {
            saleProgressOutputpara omodel = new saleProgressOutputpara();

            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == model.Token)
                {

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);


                    sqlcon.Open();

                    sqlcmd = new SqlCommand("SaleOnProgress_SaleDetails", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@UserId", model.user_id);
                    sqlcmd.Parameters.Add("@customer_name", model.customer_name);
                    sqlcmd.Parameters.Add("@PageNo", model.pageno);
                    sqlcmd.Parameters.Add("@Pagerows", model.rowcount);

                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {
                            omodel.ResponseCode = "200";
                            omodel.Responsedetails = "Success";
                            omodel.totalcount = Convert.ToInt32(dt.Rows[0]["totalcount"]);
                            omodel.sale_on_progress_list = APIHelperMethods.ToModelList<Customer>(dt);

                            return Json(omodel, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "Error";
                            return Json(oerror);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "Error";

                        return Json(oerror);
                    }

                }
                else
                {
                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not match";

                    return Json(oerror);

                }


            }
            catch
            {
                oerror.ResponseCode = "201";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }

        }

        #endregion

        #region Customer Basket View
        [AcceptVerbs("GET")]
        public JsonResult CutomerbasketView(string customer_id, string sales_man_id, string Token)
        {
            Customerbasketviewoutput omodel = new Customerbasketviewoutput();
            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == Token)
                {

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("API_CustomerBasket", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@customer_id", customer_id);
                    sqlcmd.Parameters.Add("@sales_man_id", sales_man_id);

                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {
                            omodel.ResponseCode = "200";
                            omodel.Responsedetails = "Success";
                            omodel.RequestId = Convert.ToInt64(dt.Rows[0]["RequestId"]);

                            omodel.customer_basket_details = APIHelperMethods.ToModelList<productbasket>(dt);

                            return Json(omodel, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "Error";
                            return Json(oerror, JsonRequestBehavior.AllowGet);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "Id not exists";

                        return Json(oerror, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not match";
                    return Json(oerror, JsonRequestBehavior.AllowGet);
                }


            }
            catch
            {
                oerror.ResponseCode = "201";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }

        }
        #endregion

        #region Basket Delete
        [AcceptVerbs("POST")]
        public JsonResult Basketdelete(Basketdeleteinputparameter model)
        {
            Basketdeleteoutputparameter omodel = new Basketdeleteoutputparameter();

            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == model.Token)
                {

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);


                    sqlcon.Open();

                    sqlcmd = new SqlCommand("API_CustomerBasketDelete", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@customer_id", model.customer_id);
                    sqlcmd.Parameters.Add("@product_id", model.product_id);
                    sqlcmd.Parameters.Add("@sales_man_id", model.sales_man_id);

                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {

                            omodel = APIHelperMethods.ToModel<Basketdeleteoutputparameter>(dt);

                            return Json(omodel, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "Error";
                            return Json(oerror);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "Error";

                        return Json(oerror);
                    }

                }
                else
                {
                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not match";

                    return Json(oerror);

                }


            }
            catch
            {
                oerror.ResponseCode = "201";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }

        }

        #endregion

        #region Customer Delete
        [AcceptVerbs("POST")]
        public JsonResult Customerdelete(Customerdeleteinputparameter model)
        {
            Customerdeleteoutputparameter omodel = new Customerdeleteoutputparameter();

            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == model.Token)
                {
                    string bandsjoin = null;

                    if (model.temp_unique_id_list != null)
                    {
                        bandsjoin = string.Join(",", model.temp_unique_id_list);
                    }

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);

                    sqlcon.Open();

                    sqlcmd = new SqlCommand("API_CustomerDelete", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@customer_id", model.customer_id);
                    sqlcmd.Parameters.Add("@user_id", model.user_id);
                    sqlcmd.Parameters.Add("@temp_unique_id", model.temp_unique_id);
                    sqlcmd.Parameters.Add("@temp_unique_id_list", bandsjoin);
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {

                            omodel = APIHelperMethods.ToModel<Customerdeleteoutputparameter>(dt);

                            return Json(omodel, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "Error";
                            return Json(oerror);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "Error";

                        return Json(oerror);
                    }

                }
                else
                {
                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not match";

                    return Json(oerror);

                }


            }
            catch
            {
                oerror.ResponseCode = "201";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }

        }

        #endregion

        #region FinancerList

        [AcceptVerbs("GET")]
        public JsonResult FinancerList(string User_login_Id, string Token, string financer_name, string finance_req_id, string financer_id)
        {
            FinacOutput omodel = new FinacOutput();

            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == Token)
                {
                    if (financer_name == "")
                    {

                        financer_name = null;
                    }
                    if (finance_req_id == "")
                    {

                        finance_req_id = null;
                    }
                    if (financer_id == "")
                    {

                        financer_id = null;
                    }

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("API_FinancerList", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@User_login_Id", User_login_Id);
                    sqlcmd.Parameters.Add("@FinaceName", financer_name);
                    sqlcmd.Parameters.Add("@finance_req_id", finance_req_id);
                    sqlcmd.Parameters.Add("@financer_id", financer_id);
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {
                            omodel.ResponseCode = "200";
                            omodel.Responsedetails = "Success";
                            omodel.financer_details = APIHelperMethods.ToModelList<finace>(dt);

                            return Json(omodel, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "No record found";
                            return Json(oerror, JsonRequestBehavior.AllowGet);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "No record found";

                        return Json(oerror, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not match";

                    return Json(oerror, JsonRequestBehavior.AllowGet);

                }


            }
            catch
            {
                oerror.ResponseCode = "201";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }

        }

        #endregion

        #region Sales on progress - view customer basket- Send Discount Request

        [AcceptVerbs("POST")]
        public JsonResult SaleonProgressSendDiscountRequest(SenddiscountSalesonprogressInput model)
        {
            SenddiscountSalesonprogressOuput omodel = new SenddiscountSalesonprogressOuput();
            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == model.Token)
                {
                    List<Productsrequestdetails> obj = new List<Productsrequestdetails>();
                    foreach (var item in model.request_details)
                    {
                        obj.Add(new Productsrequestdetails()
                        {
                            discount_percentage = item.discount_percentage,
                            product_quantity = item.product_quantity,
                            product_id = item.product_id,
                            Salesman_Isapplied = item.Salesman_Isapplied,
                            discount_amount = item.discount_amount,
                            final_discount_price = item.final_discount_price
                        });
                    }

                    string ProductXML = APIHelperMethods.ConvertToXml(obj, 0);
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);


                    sqlcon.Open();
                    sqlcmd = new SqlCommand("APICustomerDiscountquantityRequest", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@customer_id", model.customer_id);
                    sqlcmd.Parameters.Add("@user_id", model.user_id);
                    sqlcmd.Parameters.Add("@request_type", model.request_type);
                    sqlcmd.Parameters.Add("@payment_type", model.payment_type);
                    sqlcmd.Parameters.Add("@exchange_amount", model.exchange_amount);
                    sqlcmd.Parameters.Add("@financer_id", model.financer_id);
                    sqlcmd.Parameters.Add("@RequestId", model.RequestId);

                    sqlcmd.Parameters.Add("@ProductList", ProductXML);

                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {
                            omodel = APIHelperMethods.ToModel<SenddiscountSalesonprogressOuput>(dt);

                            DataTable getdevicelist = new DataTable();

                            getdevicelist = GetdeviceId(model.user_id, "Salesman", model.customer_id, model.request_type);
                            if (getdevicelist != null)
                            {
                                for (int i = 0; i < getdevicelist.Rows.Count; i++)
                                {
                                    if (Convert.ToString(getdevicelist.Rows[i]["Mac_Address"]) != "")
                                    {
                                        SendPushNotification("", Convert.ToString(getdevicelist.Rows[i]["Mac_Address"]), Convert.ToString(getdevicelist.Rows[i]["CustomerName"]), model.request_type);
                                    }
                                }
                            }
                            return Json(omodel);
                        }
                        else
                        {

                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "No Record Found";
                            return Json(oerror);

                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "No Record Found";

                        return Json(oerror);
                    }

                }
                else
                {
                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not match";

                    return Json(oerror);

                }


            }
            catch
            {
                oerror.ResponseCode = "201";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }

        }
        #endregion

        #region Billing Request with Address

        [AcceptVerbs("POST")]

        public JsonResult BillingRequestAddresss(BillingDetailscustomerInput model)
        {
            SenddiscountSalesonprogressOuput omodel = new SenddiscountSalesonprogressOuput();
            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == model.Token)
                {

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);

                    sqlcon.Open();

                    sqlcmd = new SqlCommand("API_Customer_BiullingRequest", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@temp_request_id", model.temp_request_id);
                    sqlcmd.Parameters.Add("@delivery_option_type", model.delivery_option_type);
                    sqlcmd.Parameters.Add("@is_delivery_address_same", model.is_delivery_address_same);
                    sqlcmd.Parameters.Add("@block_no", model.block_no);
                    sqlcmd.Parameters.Add("@street_no", model.street_no);
                    sqlcmd.Parameters.Add("@flat_no", model.flat_no);
                    sqlcmd.Parameters.Add("@floor", model.floor);
                    sqlcmd.Parameters.Add("@landmark", model.landmark);
                    sqlcmd.Parameters.Add("@country", model.country);
                    sqlcmd.Parameters.Add("@state", model.state);
                    sqlcmd.Parameters.Add("@city", model.city);
                    sqlcmd.Parameters.Add("@pin_code", model.pin_code);
                    sqlcmd.Parameters.Add("@delivery_date", model.delivery_date);
                    sqlcmd.Parameters.Add("@gstin", model.gstin);
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {

                            oerror.ResponseCode = "200";
                            oerror.Responsedetails = "Success";
                            return Json(oerror);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "No Record Found";
                            return Json(oerror);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "No Record Found";

                        return Json(oerror);
                    }

                }
                else
                {
                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not match";

                    return Json(oerror);

                }


            }
            catch
            {
                oerror.ResponseCode = "201";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }

        }

        #endregion

        #region View Customer Approval

        [AcceptVerbs("POST")]
        public JsonResult Customerapprovalshow(CustomerlistforapprovalInput model)
        {
            CustomerlistforapprovalOutput omodel = new CustomerlistforapprovalOutput();
            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == model.Token)
                {
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();

                    sqlcmd = new SqlCommand("API_ViewCustomerApproval", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@customer_name", model.customer_name);
                    sqlcmd.Parameters.Add("@user_id", model.user_id);
                    sqlcmd.Parameters.Add("@discount_requested_status", model.discount_requested_status);

                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {
                            omodel.ResponseCode = "200";
                            omodel.Responsedetails = "Success";
                            omodel.customer_approval_details = APIHelperMethods.ToModelList<Customerapprove>(dt);
                            return Json(omodel);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "No Record Found";
                            return Json(oerror);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "No Record Found";

                        return Json(oerror);
                    }

                }
                else
                {
                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not match";

                    return Json(oerror);

                }


            }
            catch
            {
                oerror.ResponseCode = "201";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }
        }
        #endregion

        #region View Discount Approval

        [AcceptVerbs("POST")]
        public JsonResult ViewDiscountApproval(string customer_id, string Token, string sales_man_id, string RequestId)
        {
            Salesmandiscountapproval omodel = new Salesmandiscountapproval();
            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == Token)
                {
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);


                    sqlcon.Open();

                    sqlcmd = new SqlCommand("API_DiscountApproval", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@customer_id", customer_id);
                    sqlcmd.Parameters.Add("@sales_man_id", sales_man_id);
                    sqlcmd.Parameters.Add("@RequestId", RequestId);
                    sqlcmd.Parameters.Add("@Mode", 1);

                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {
                            omodel.ResponseCode = "200";
                            omodel.Responsedetails = "Success";
                            omodel.RequestId = Convert.ToInt32(dt.Rows[0]["RequestId"]);
                            omodel.exchange_amount = Convert.ToDecimal(dt.Rows[0]["exchange_amount"]);
                            omodel.view_approval_details = APIHelperMethods.ToModelList<productfordiscountsalesman>(dt);
                            return Json(omodel);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "No Record Found";
                            return Json(oerror);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "No Record Found";

                        return Json(oerror);
                    }

                }
                else
                {
                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not match";

                    return Json(oerror);

                }


            }
            catch
            {
                oerror.ResponseCode = "201";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }
        }
        #endregion

        #region View Discount Approval - delete product

        [AcceptVerbs("POST")]
        public JsonResult ViewDiscountApprovalDelete(string customer_id, string product_id, string Token, string sales_man_id, string RequestId)
        {
            CustomerlistforapprovalOutput omodel = new CustomerlistforapprovalOutput();
            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == Token)
                {
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);


                    sqlcon.Open();

                    sqlcmd = new SqlCommand("API_DiscountApproval", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@customer_id", customer_id);
                    sqlcmd.Parameters.Add("@product_id", product_id);
                    sqlcmd.Parameters.Add("@sales_man_id", sales_man_id);
                    sqlcmd.Parameters.Add("@RequestId", RequestId);
                    sqlcmd.Parameters.Add("@Mode", 2);

                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {
                            oerror.ResponseCode = "200";
                            oerror.Responsedetails = "Success";
                            return Json(oerror);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "No Record Found";
                            return Json(oerror);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "No Record Found";

                        return Json(oerror);
                    }

                }
                else
                {
                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not match";

                    return Json(oerror);

                }


            }
            catch
            {
                oerror.ResponseCode = "201";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }
        }
        #endregion

        #region  View Discount Approval- Send Request

        [AcceptVerbs("POST")]
        public JsonResult DiscountApprovalSendRequest(ViewsendRequestCustomer model)
        {
            SDiscountApprovalSendRequestOutput omodel = new SDiscountApprovalSendRequestOutput();
            Commonclass oerror = new Commonclass();
            string products = "";
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == model.Token)
                {
                    if (model.product_ids != null)
                    {
                        products = string.Join(",", model.product_ids);
                    }
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);


                    sqlcon.Open();

                    sqlcmd = new SqlCommand("Customersendrequestdiscount", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@customer_id", model.customer_id);
                    sqlcmd.Parameters.Add("@user_id", model.user_id);
                    sqlcmd.Parameters.Add("@request_type", model.request_type);
                    sqlcmd.Parameters.Add("@exchange_amount", model.exchange_amount);
                    sqlcmd.Parameters.Add("@payment_type", model.payment_type);
                    sqlcmd.Parameters.Add("@financer_id", model.financer_id);
                    sqlcmd.Parameters.Add("@product_ids", model.product_ids);
                    sqlcmd.Parameters.Add("@RequestId", model.RequestId);

                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {
                            omodel = APIHelperMethods.ToModel<SDiscountApprovalSendRequestOutput>(dt);


                            return Json(omodel);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "No Record Found";
                            return Json(oerror);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "No Record Found";

                        return Json(oerror);
                    }

                }
                else
                {
                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not match";

                    return Json(oerror);

                }


            }
            catch
            {
                oerror.ResponseCode = "201";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }
        }
        #endregion

        #region  Sales Manager Discount Request

        [AcceptVerbs("POST")]
        public JsonResult SalesManagerDiscountRequest(ViewDiscountRequestInput model)
        {
            ViewDiscountRequestOutput omodel = new ViewDiscountRequestOutput();
            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == model.Token)
                {
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);


                    sqlcon.Open();

                    sqlcmd = new SqlCommand("API_SP_ViewDiscountdetailscustomerwise", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@request_status", model.request_status);
                    sqlcmd.Parameters.Add("@user_id", model.user_id);
                    sqlcmd.Parameters.Add("@Isappliedtop", model.Isappliedtop);
                    sqlcmd.Parameters.Add("@user_type", model.user_type);
                    sqlcmd.Parameters.Add("@Mode", 1);
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {
                            omodel.ResponseCode = "200";
                            omodel.Responsedetails = "Success";
                            omodel.disc_request_list = APIHelperMethods.ToModelList<DiscountrequestList>(dt);
                            return Json(omodel);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "No Record Found";
                            return Json(oerror);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "No Record Found";

                        return Json(oerror);
                    }

                }
                else
                {
                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not match";

                    return Json(oerror);

                }


            }
            catch
            {
                oerror.ResponseCode = "201";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }

        }
        #endregion

        #region  Sales Manager Discount Request(s) - details

        [AcceptVerbs("POST")]
        public JsonResult SalesManagerDiscountRequestDetailproduct(SalesmandiscountRequestProductInput model)
        {
            SalesmandiscountRequestProductOutput omodel = new SalesmandiscountRequestProductOutput();
            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == model.Token)
                {
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);


                    sqlcon.Open();

                    sqlcmd = new SqlCommand("API_SP_ViewDiscountdetailscustomerwise", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@salesman_id", model.salesman_id);
                    sqlcmd.Parameters.Add("@user_id", model.user_id);
                    sqlcmd.Parameters.Add("@customer_id", model.customer_id);
                    sqlcmd.Parameters.Add("@Requestid", model.Requestid);
                    sqlcmd.Parameters.Add("@Mode", 2);
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {
                            omodel.ResponseCode = "200";
                            omodel.Responsedetails = "Success";
                            omodel.Requestid = Convert.ToInt32(dt.Rows[0]["Requestid"]);
                            omodel.exchange_amount = Convert.ToDecimal(dt.Rows[0]["exchange_amount"]);
                            omodel.discount_request_details = APIHelperMethods.ToModelList<Discountsalesman>(dt);
                            return Json(omodel);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "No Record Found";
                            return Json(oerror);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "No Record Found";

                        return Json(oerror);
                    }

                }
                else
                {
                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not match";

                    return Json(oerror);

                }


            }
            catch
            {
                oerror.ResponseCode = "201";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }

        }
        #endregion

        #region Discount Request(s) - Approve / Reject

        [AcceptVerbs("POST")]
        public JsonResult SalesManagerDiscountRequestApprove(DiscountApprovalInput model)
        {
            SalesmandiscountRequestProductOutput omodel = new SalesmandiscountRequestProductOutput();
            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == model.Token)
                {
                    List<Productsrequestdetails> obj = new List<Productsrequestdetails>();
                    foreach (var item in model.request_details)
                    {
                        obj.Add(new Productsrequestdetails()
                        {
                            discount_percentage = item.discount_percentage,
                            product_quantity = item.product_quantity,
                            product_id = item.product_id,
                            final_discount_price = item.final_discount_price

                        });
                    }
                    string ProductXML = APIHelperMethods.ConvertToXml(obj, 0);

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);


                    sqlcon.Open();

                    sqlcmd = new SqlCommand("APICustomerDiscountquantityResponsefromsalesmanager", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@customer_id", model.customer_id);
                    sqlcmd.Parameters.Add("@user_id", model.user_id);
                    sqlcmd.Parameters.Add("@request_type", model.request_type);
                    sqlcmd.Parameters.Add("@Isappliedtop", model.Isappliedtop);
                    sqlcmd.Parameters.Add("@ProductList", ProductXML);
                    sqlcmd.Parameters.Add("@sales_man_id", model.sales_man_id);
                    sqlcmd.Parameters.Add("@Requestid", model.Requestid);

                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {
                            oerror.ResponseCode = "200";
                            oerror.Responsedetails = "Success";


                            DataTable getdevicelist = new DataTable();

                            getdevicelist = GetdeviceId(model.sales_man_id, "Manager", model.customer_id, model.request_type);
                            if (getdevicelist != null)
                            {
                                for (int i = 0; i < getdevicelist.Rows.Count; i++)
                                {
                                    if (Convert.ToString(getdevicelist.Rows[i]["Mac_Address"]) != "")
                                    {
                                        SendPushNotification("", Convert.ToString(getdevicelist.Rows[i]["Mac_Address"]), Convert.ToString(getdevicelist.Rows[i]["CustomerName"]), model.request_type);
                                    }
                                }
                            }

                            return Json(oerror);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "No Record Found";
                            return Json(oerror);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "No Record Found";

                        return Json(oerror);
                    }

                }
                else
                {
                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not match";

                    return Json(oerror);

                }


            }
            catch
            {
                oerror.ResponseCode = "103";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }

        }

        #endregion

        #region   Sales Manager Finance Request List

        [AcceptVerbs("POST")]
        public JsonResult SalesMangerFinanceRequestList(string user_id, string finance_request_status, string Token, int? pageno, int? rowcount)
        {
            FinanceRequestOutput omodel = new FinanceRequestOutput();
            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == Token)
                {
                    if (finance_request_status == "")
                    {
                        finance_request_status = null;
                    }
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);

                    sqlcon.Open();

                    sqlcmd = new SqlCommand("GetFinanceList", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@user_id", user_id);
                    sqlcmd.Parameters.Add("@finance_request_status", finance_request_status);
                    sqlcmd.Parameters.Add("@Mode", 1);
                    sqlcmd.Parameters.Add("@PageNo", pageno);
                    sqlcmd.Parameters.Add("@Pagerows", rowcount);

                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {
                            omodel.ResponseCode = "200";
                            omodel.Responsedetails = "Success";
                            omodel.total_count = Convert.ToInt32(dt.Rows[0]["total_count"]);
                            omodel.finance_request_list = APIHelperMethods.ToModelList<FinanceRequest>(dt);
                            return Json(omodel);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "No Record Found";
                            return Json(oerror);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "No Record Found";

                        return Json(oerror);
                    }

                }
                else
                {
                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not match";

                    return Json(oerror);

                }


            }
            catch
            {
                oerror.ResponseCode = "103";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }

        }

        #endregion

        #region   Sales Manager Finance Request List  (Search)

        [AcceptVerbs("POST")]
        public JsonResult SalesMangerFinanceRequestListSearch(string user_id, string customer_name, string finance_request_status, string Token, int? pageno, int? rowcount)
        {
            FinanceRequestOutput omodel = new FinanceRequestOutput();
            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == Token)
                {
                    if (customer_name == "")
                    {
                        customer_name = null;
                    }

                    if (finance_request_status == "")
                    {
                        finance_request_status = null;
                    }
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);

                    sqlcon.Open();

                    sqlcmd = new SqlCommand("GetFinanceList", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@user_id", user_id);
                    sqlcmd.Parameters.Add("@customer_name", customer_name);
                    sqlcmd.Parameters.Add("@finance_request_status", finance_request_status);
                    sqlcmd.Parameters.Add("@Mode", 1);
                    sqlcmd.Parameters.Add("@PageNo", pageno);
                    sqlcmd.Parameters.Add("@Pagerows", rowcount);

                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {
                            omodel.ResponseCode = "200";
                            omodel.Responsedetails = "Success";
                            omodel.total_count = Convert.ToInt32(dt.Rows[0]["total_count"]);
                            omodel.finance_request_list = APIHelperMethods.ToModelList<FinanceRequest>(dt);
                            return Json(omodel);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "No Record Found";
                            return Json(oerror);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "No Record Found";

                        return Json(oerror);
                    }

                }
                else
                {
                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not match";

                    return Json(oerror);

                }


            }
            catch
            {
                oerror.ResponseCode = "103";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }

        }

        #endregion

        #region Sales Manager  Finance Request Details

        [AcceptVerbs("POST")]
        public JsonResult SalesManagerFinanceRequestDetails(string finance_req_id, string Token)
        {
            FinanceRequestDetailssalesfinanceOutput omodel = new FinanceRequestDetailssalesfinanceOutput();
            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == Token)
                {

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);

                    sqlcon.Open();

                    sqlcmd = new SqlCommand("API_GetFinanceRequestDetailsslamanfinance", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@finance_req_id", finance_req_id);
                    sqlcmd.Parameters.Add("@Mode", 1);


                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {
                            omodel.ResponseCode = "200";
                            omodel.Responsedetails = "Success";

                            omodel.exchange_amount = Convert.ToString(dt.Rows[0]["exchange_amount"]);
                            omodel.view_finance_details = APIHelperMethods.ToModelList<productbasketforFinanceRequest>(dt);
                            return Json(omodel);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "No Record Found";
                            return Json(oerror);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "No Record Found";

                        return Json(oerror);
                    }

                }
                else
                {
                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not match";

                    return Json(oerror);

                }


            }
            catch
            {
                oerror.ResponseCode = "103";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }

        }
        #endregion

        #region   SalesMan List and Search

        [AcceptVerbs("POST")]
        public JsonResult SalesmanListSearch(string user_id, string Token, int? pageno, int? rowcount, string salesman_name = null)
        {
            SalesmanagersalesmanRequestOutput omodel = new SalesmanagersalesmanRequestOutput();
            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == Token)
                {
                    if (salesman_name == "")
                    {
                        salesman_name = null;
                    }
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);

                    sqlcon.Open();

                    sqlcmd = new SqlCommand("API_GetFinanceSalesManAllList", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@user_id", user_id);
                    sqlcmd.Parameters.Add("@customer_name", salesman_name);
                    sqlcmd.Parameters.Add("@PageNo", pageno);
                    sqlcmd.Parameters.Add("@Pagerows", rowcount);

                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {
                            omodel.ResponseCode = "200";
                            omodel.Responsedetails = "Success";

                            omodel.total_count = Convert.ToInt32(dt.Rows[0]["total_count"]);

                            omodel.salesman_list = APIHelperMethods.ToModelList<SalesmanListManger>(dt);
                            return Json(omodel);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "No Record Found";
                            return Json(oerror);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "No Record Found";

                        return Json(oerror);
                    }

                }
                else
                {
                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not match";

                    return Json(oerror);

                }


            }
            catch
            {
                oerror.ResponseCode = "103";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }

        }

        #endregion

        #region   FINANCER Finance Request List

        [AcceptVerbs("POST")]
        public JsonResult FinancerfinanceRequestList(string user_id, string Token, string finance_request_status, int? pageno, int? rowcount)
        {
            FinanceRequestOutput omodel = new FinanceRequestOutput();
            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == Token)
                {

                    if (finance_request_status == "")
                    {
                        finance_request_status = null;
                    }


                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);

                    sqlcon.Open();

                    sqlcmd = new SqlCommand("GetFinanceList", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@user_id", user_id);
                    sqlcmd.Parameters.Add("@finance_request_status", finance_request_status);
                    sqlcmd.Parameters.Add("@Mode", 2);
                    sqlcmd.Parameters.Add("@PageNo", pageno);
                    sqlcmd.Parameters.Add("@Pagerows", rowcount);


                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {

                            omodel.ResponseCode = "200";
                            omodel.Responsedetails = "Success";
                            omodel.total_count = Convert.ToInt32(dt.Rows[0]["total_count"]);
                            omodel.finance_request_list = APIHelperMethods.ToModelList<FinanceRequest>(dt);
                            return Json(omodel);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "No Record Found";
                            return Json(oerror);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "No Record Found";

                        return Json(oerror);
                    }

                }
                else
                {
                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not match";

                    return Json(oerror);

                }


            }
            catch
            {
                oerror.ResponseCode = "103";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }

        }

        #endregion

        #region   FINANCER Finance Request List  (Search)

        [AcceptVerbs("POST")]
        public JsonResult FinancerfinanceRequestListSearch(string user_id, string customer_name, string finance_request_status, string Token, int? pageno, int? rowcount)
        {
            FinanceRequestOutput omodel = new FinanceRequestOutput();
            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == Token)
                {
                    if (customer_name == "")
                    {
                        customer_name = null;

                    }
                    if (finance_request_status == "")
                    {
                        finance_request_status = null;
                    }
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);

                    sqlcon.Open();

                    sqlcmd = new SqlCommand("GetFinanceList", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@user_id", user_id);
                    sqlcmd.Parameters.Add("@customer_name", customer_name);
                    sqlcmd.Parameters.Add("@finance_request_status", finance_request_status);
                    sqlcmd.Parameters.Add("@Mode", 2);
                    sqlcmd.Parameters.Add("@PageNo", pageno);
                    sqlcmd.Parameters.Add("@Pagerows", rowcount);


                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {

                            omodel.ResponseCode = "200";
                            omodel.Responsedetails = "Success";
                            omodel.total_count = Convert.ToInt32(dt.Rows[0]["total_count"]);
                            omodel.finance_request_list = APIHelperMethods.ToModelList<FinanceRequest>(dt);
                            return Json(omodel);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "No Record Found";
                            return Json(oerror);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "No Record Found";

                        return Json(oerror);
                    }

                }
                else
                {
                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not match";

                    return Json(oerror);

                }


            }
            catch
            {
                oerror.ResponseCode = "103";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }

        }

        #endregion

        #region  Finance Request Details (Accept)
        [AcceptVerbs("POST")]
        public JsonResult FinanceRequestDetailsAccept(FinancedetailsAcceptInput model)
        {
            FinanceRequestOutput omodel = new FinanceRequestOutput();
            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == model.Token)
                {

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);

                    sqlcon.Open();


                    sqlcmd = new SqlCommand("API_Finance_Acceptdetails", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@finance_userid", model.finance_userid);

                    sqlcmd.Parameters.Add("@finance_req_id", model.finance_req_id);
                    sqlcmd.Parameters.Add("@finance_approval_no", model.finance_approval_no);
                    sqlcmd.Parameters.Add("@total_amount", model.total_amount);
                    sqlcmd.Parameters.Add("@processing_fee", model.processing_fee);
                    sqlcmd.Parameters.Add("@loan_amount", model.loan_amount);
                    sqlcmd.Parameters.Add("@other_charges", model.other_charges);
                    sqlcmd.Parameters.Add("@finance_scheme", model.finance_scheme);
                    sqlcmd.Parameters.Add("@dbd_no", model.dbd_no);
                    sqlcmd.Parameters.Add("@downpayment", model.downpayment);
                    sqlcmd.Parameters.Add("@no_of_emi", model.no_of_emi);
                    sqlcmd.Parameters.Add("@emi_amount", model.emi_amount);
                    sqlcmd.Parameters.Add("@Mode", 3);  //for billing previous was 1
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {

                            oerror.ResponseCode = "200";
                            oerror.Responsedetails = "Success";
                            // omodel.finance_request_list = APIHelperMethods.ToModelList<FinanceRequest>(dt);
                            return Json(oerror);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "No Record Found";
                            return Json(oerror);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "No Record Found";

                        return Json(oerror);
                    }

                }
                else
                {
                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not match";

                    return Json(oerror);

                }


            }
            catch
            {
                oerror.ResponseCode = "103";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }

        }


        #endregion

        #region Finance Request Details (Reject)
        [AcceptVerbs("POST")]
        public JsonResult FinanceRequestDetailsDelete(FinancedetailsAcceptInput model)
        {
            FinanceRequestOutput omodel = new FinanceRequestOutput();
            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == model.Token)
                {

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);

                    sqlcon.Open();

                    sqlcmd = new SqlCommand("API_Finance_Acceptdetails", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@finance_userid", model.finance_userid);
                    sqlcmd.Parameters.Add("@finance_req_id", model.finance_req_id);

                    sqlcmd.Parameters.Add("@reason", model.reason);
                    sqlcmd.Parameters.Add("@Mode", 2);
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {

                            oerror.ResponseCode = "200";
                            oerror.Responsedetails = "Success";
                            // omodel.finance_request_list = APIHelperMethods.ToModelList<FinanceRequest>(dt);
                            return Json(oerror);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "No Record Found";
                            return Json(oerror);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "No Record Found";

                        return Json(oerror);
                    }

                }
                else
                {
                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not match";

                    return Json(oerror);

                }


            }
            catch
            {
                oerror.ResponseCode = "103";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }

        }

        #endregion

        #region #### Get customer Details by mobile number ####

        [AcceptVerbs("POST")]
        public JsonResult Getcustomerbymobilenumber(string customer_mobile_no, string Token)
        {
            saleProgressOutputpara omodel = new saleProgressOutputpara();
            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == Token)
                {

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);

                    sqlcon.Open();

                    sqlcmd = new SqlCommand("API_Mobileduplicatecheck", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@phone", customer_mobile_no);

                    sqlcmd.Parameters.Add("@Mode", 1);
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {
                            omodel.ResponseCode = "200";
                            omodel.Responsedetails = "Success";

                            omodel.sale_on_progress_list = APIHelperMethods.ToModelList<Customer>(dt);
                            return Json(omodel);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "No Record Found";
                            return Json(oerror);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "No Record Found";

                        return Json(oerror);
                    }

                }
                else
                {
                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not match";

                    return Json(oerror);

                }


            }
            catch
            {
                oerror.ResponseCode = "103";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }

        }
        #endregion

        #region View Finance Status Customer

        [AcceptVerbs("POST")]

        public JsonResult SalesManCustomerFinanceList(string user_id, string Token, string request_status, string cust_name = null)
        {
            DataTable dt2 = new DataTable();
            ViewFinanceStatusCustomerOutput omodel = new ViewFinanceStatusCustomerOutput();
            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == Token)
                {
                    if (cust_name == "")
                    {

                        cust_name = null;
                    }

                    if (request_status == "")
                    {

                        request_status = null;
                    }
                    List<FinanceRequestCustomer> omm = new List<FinanceRequestCustomer>();
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);

                    sqlcon.Open();

                    sqlcmd = new SqlCommand("API_ViewFinanceStatusCustomer", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@user_id", user_id);
                    sqlcmd.Parameters.Add("@cust_name", cust_name);
                    sqlcmd.Parameters.Add("@request_status", request_status);
                    sqlcmd.Parameters.Add("@Mode", 1);

                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                long finance_req_id = Convert.ToInt64(dt.Rows[i]["finance_req_id"]);
                                string customer_id = Convert.ToString(dt.Rows[i]["customer_id"]);
                                string customer_name = Convert.ToString(dt.Rows[i]["customer_name"]);
                                string customer_mobile_no = Convert.ToString(dt.Rows[i]["customer_mobile_no"]);
                                string salesman_id = Convert.ToString(dt.Rows[i]["salesman_id"]);
                                string finance_requested_date = Convert.ToString(dt.Rows[i]["finance_requested_date"]);
                                string finance_requested_status = Convert.ToString(dt.Rows[i]["finance_requested_status"]);




                                dt2.Clear();
                                FinanceRequestCustomer omodel2 = new FinanceRequestCustomer();
                                sqlcmd = new SqlCommand("API_ViewFinanceStatusCustomer", sqlcon);
                                sqlcmd.CommandType = CommandType.StoredProcedure;
                                sqlcmd.Parameters.Add("@finance_req_id", Convert.ToInt32(dt.Rows[i]["finance_req_id"]));
                                sqlcmd.Parameters.Add("@Mode", 2);
                                sqlcmd.Parameters.Add("@cust_name", cust_name);
                                SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
                                da1.Fill(dt2);
                                sqlcon.Close();

                                List<FinanceRequestFinance> omm2 = new List<FinanceRequestFinance>();

                                if (dt2.Rows.Count > 0)
                                {
                                    if (dt2.Rows[0][0].ToString() != "0")
                                    {
                                        for (int j = 0; j < dt2.Rows.Count; j++)
                                        {

                                            omm2.Add(
                                      new FinanceRequestFinance()
                                      {
                                          financer_name = Convert.ToString(dt2.Rows[j]["financer_name"]),
                                          financer_id = Convert.ToString(dt2.Rows[j]["financer_id"]),
                                          rejected_reason = Convert.ToString(dt2.Rows[j]["rejected_reason"]),
                                          finance_status = Convert.ToString(dt2.Rows[j]["finance_status"]),
                                          finance_status_date = Convert.ToString(dt2.Rows[j]["finance_status_date"]),
                                          Is_financer_to_apply = Convert.ToBoolean(dt2.Rows[j]["Is_financer_to_apply"]),
                                      });

                                        }

                                    }

                                }

                                omm.Add(
                                  new FinanceRequestCustomer()
                                  {
                                      finance_req_id = finance_req_id,
                                      customer_id = customer_id,
                                      customer_name = customer_name,
                                      customer_mobile_no = customer_mobile_no,
                                      salesman_id = salesman_id,
                                      finance_requested_date = finance_requested_date,
                                      finance_requested_status = finance_requested_status,
                                      financer_list = omm2
                                  });



                            }

                            omodel.ResponseCode = "200";
                            omodel.Responsedetails = "Success";
                            omodel.finance_status_details = omm;
                            return Json(omodel);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "No Record Found";
                            return Json(oerror);
                        }
                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "No Record Found";

                        return Json(oerror);
                    }

                }
                else
                {
                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not match";

                    return Json(oerror);

                }


            }
            catch
            {
                oerror.ResponseCode = "103";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }

        }

        #endregion

        #region View Finance Status (Apply Again)
        [AcceptVerbs("POST")]
        public JsonResult FinancestatusApplyAgain(Viewfinancestatusinput model)
        {

            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];

                if (token == model.Token)
                {
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);

                    sqlcon.Open();

                    sqlcmd = new SqlCommand("API_Customer_Applyagain_forFinance", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@user_id", model.user_id);
                    sqlcmd.Parameters.Add("@customer_id", model.customer_id);
                    sqlcmd.Parameters.Add("@financer_id", model.financer_id);
                    sqlcmd.Parameters.Add("@finance_request_id", model.finance_request_id);

                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {
                            oerror.ResponseCode = "200";
                            oerror.Responsedetails = "Success";
                            return Json(oerror);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "No Record Found";
                            return Json(oerror);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "No Record Found";

                        return Json(oerror);
                    }

                }
                else
                {
                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not match";

                    return Json(oerror);

                }


            }
            catch
            {
                oerror.ResponseCode = "103";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }

        }
        #endregion

        #region Notification SalesMan
        [AcceptVerbs("POST")]
        public JsonResult Salesmannotification(string user_id, string Token)
        {
            ViewNotificationsales omodel = new ViewNotificationsales();
            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == Token)
                {

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);

                    sqlcon.Open();

                    sqlcmd = new SqlCommand("API_Notifications", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@userid", user_id);
                    sqlcmd.Parameters.Add("@Mode", 1);
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {
                            omodel.ResponseCode = "200";
                            omodel.Responsedetails = "Success";

                            omodel.notification_details = APIHelperMethods.ToModelList<ViewNotificationsalesdiscount>(dt);
                            return Json(omodel);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "No Record Found";
                            return Json(oerror);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "No Record Found";

                        return Json(oerror);
                    }

                }
                else
                {
                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not match";

                    return Json(oerror);

                }


            }
            catch
            {
                oerror.ResponseCode = "103";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }

        }
        #endregion

        #region Notification SalesManager
        [AcceptVerbs("POST")]
        public JsonResult Salesmanagernotification(string user_id, string Token)
        {
            ViewNotificationsalesmanager omodel = new ViewNotificationsalesmanager();
            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == Token)
                {

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);

                    sqlcon.Open();

                    sqlcmd = new SqlCommand("API_Notifications", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@userid", user_id);
                    sqlcmd.Parameters.Add("@Mode", 2);
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {
                            omodel.ResponseCode = "200";
                            omodel.Responsedetails = "Success";

                            omodel.notification_details = APIHelperMethods.ToModelList<ViewNotificationsalesmanagerdiscount>(dt);
                            return Json(omodel);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "No Record Found";
                            return Json(oerror);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "No Record Found";

                        return Json(oerror);
                    }

                }
                else
                {
                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not match";

                    return Json(oerror);

                }


            }
            catch
            {
                oerror.ResponseCode = "103";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }

        }
        #endregion

        #region Notification Financer
        [AcceptVerbs("POST")]
        public JsonResult Financernotification(string user_id, string Token)
        {
            ViewNotificationfinancerOutput omodel = new ViewNotificationfinancerOutput();
            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == Token)
                {

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);

                    sqlcon.Open();

                    sqlcmd = new SqlCommand("API_Notifications", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@userid", user_id);
                    sqlcmd.Parameters.Add("@Mode", 3);
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {
                            omodel.ResponseCode = "200";
                            omodel.Responsedetails = "Success";

                            omodel.notification_list = APIHelperMethods.ToModelList<ViewNotificationfinance>(dt);
                            return Json(omodel);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "No Record Found";
                            return Json(oerror);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "No Record Found";

                        return Json(oerror);
                    }

                }
                else
                {
                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not match";

                    return Json(oerror);

                }


            }
            catch
            {
                oerror.ResponseCode = "103";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }

        }
        #endregion

        #region Notification Top Approval
        [AcceptVerbs("POST")]
        public JsonResult Topapprovalnotification(string user_id, string Token)
        {
            ViewNotificationsalesmanager omodel = new ViewNotificationsalesmanager();
            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == Token)
                {

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);

                    sqlcon.Open();

                    sqlcmd = new SqlCommand("API_Notifications", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@userid", user_id);
                    sqlcmd.Parameters.Add("@Mode", 5);
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {
                            omodel.ResponseCode = "200";
                            omodel.Responsedetails = "Success";

                            omodel.notification_details = APIHelperMethods.ToModelList<ViewNotificationsalesmanagerdiscount>(dt);
                            return Json(omodel);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "No Record Found";
                            return Json(oerror);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "No Record Found";

                        return Json(oerror);
                    }

                }
                else
                {
                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not match";

                    return Json(oerror);

                }


            }
            catch
            {
                oerror.ResponseCode = "103";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }


        }
        #endregion

        #region Pushnotification by FCM
        public static void SendPushNotification(string message, string deviceid, string Customer, string Requesttype)
        {
            try
            {
                string applicationID = "AIzaSyBec9GYzFbhHN3R1VffHKi4WSYSPRyV4Q4";
                string senderId = "392508903088";
                string deviceId = deviceid;
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";

                var data2 = new
                {
                    to = deviceId,
                    notification = new
                    {
                        body = message,
                        title = ""
                    },
                    data = new
                    {
                        customer = Customer,
                        customerId = Requesttype
                    }
                };

                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data2);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                string str = sResponseFromServer;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }


        #endregion

        #region GetdeviceId

        public DataTable GetdeviceId(string UserId, string usertype, string customerId, string Requesttype)
        {
            string DeviceId = "";
            DataTable dt2 = new DataTable();
            String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
            SqlCommand sqlcmd = new SqlCommand();
            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();

            sqlcmd = new SqlCommand("Get_DeviceId", sqlcon);

            sqlcmd.CommandType = CommandType.StoredProcedure;

            sqlcmd.Parameters.Add("@userid", UserId);
            sqlcmd.Parameters.Add("@Usertype", usertype);
            sqlcmd.Parameters.Add("@customerId", customerId);
            sqlcmd.Parameters.Add("@Requesttype", Requesttype);

            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dt2);

            if (dt2.Rows.Count > 0)
            {
                // DeviceId = Convert.ToString(dt2.Rows[0]["Mac_Address"]);
                return dt2;
            }

            sqlcon.Close();

            //return DeviceId;
            return null;
        }
        #endregion

        #region  New API:- Change Notification read status
        [AcceptVerbs("POST")]
        public JsonResult ChangeNotificationreadstatus(string notification_id, string user_type, string Token)
        {
            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == Token)
                {

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);

                    sqlcon.Open();

                    sqlcmd = new SqlCommand("API_Notifications", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@notification_id", notification_id);
                    sqlcmd.Parameters.Add("@user_type", user_type);
                    sqlcmd.Parameters.Add("@Mode", 4);


                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {
                            oerror.ResponseCode = "200";
                            oerror.Responsedetails = "Success";

                            return Json(oerror);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "Error";
                            return Json(oerror);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "Error";

                        return Json(oerror);
                    }

                }
                else
                {
                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not match";

                    return Json(oerror);

                }


            }
            catch
            {
                oerror.ResponseCode = "201";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }

        }
        #endregion

        #region Get notificatiion count
        public JsonResult GetNotificationCount(string user_id, string user_type, string Token)
        {
            UserLoginOutputParameters omodel = new UserLoginOutputParameters();
            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == Token)
                {

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);

                    sqlcon.Open();

                    sqlcmd = new SqlCommand("Proc_API_GetnotificationCount", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@UserId", user_id);
                    sqlcmd.Parameters.Add("@User_Type", user_type);



                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {
                            omodel.notification_count = Convert.ToString(dt.Rows[0]["notification_count"]);
                            omodel.ResponseCode = "200";
                            omodel.Responsedetails = "Success";

                            return Json(omodel);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "Error";
                            return Json(oerror);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "No unread Count found";

                        return Json(oerror);
                    }

                }
                else
                {
                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not match";

                    return Json(oerror);

                }


            }
            catch
            {
                oerror.ResponseCode = "201";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }

        }
        #endregion

        #region Check IMEI Number
        public JsonResult GetIMEIExists(string imei_number, string Token)
        {
            //  UserLoginOutputParameters omodel = new UserLoginOutputParameters();
            IMEIClass omodel = new IMEIClass();
            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == Token)
                {

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);

                    sqlcon.Open();

                    sqlcmd = new SqlCommand("Proc_API_IMEICheck", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@imei_number", imei_number);

                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {
                            omodel = APIHelperMethods.ToModel<IMEIClass>(dt);

                            return Json(omodel);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "Error";
                            return Json(oerror);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "No unread Count found";

                        return Json(oerror);
                    }

                }
                else
                {
                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not match";

                    return Json(oerror);

                }


            }
            catch
            {
                oerror.ResponseCode = "201";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }

        }

        #endregion

        #region ### GetCompany Logo   ####
        public JsonResult GetCompanyLogo()
        {
            CompanyLogoclass oerror = new CompanyLogoclass();
            string FilePath = ConfigurationManager.AppSettings["Companylogo"].ToString();
        

                oerror.ResponseCode = "200";
                oerror.Responsedetails = "Success";
                oerror.logo_url = FilePath;
                return Json(oerror,JsonRequestBehavior.AllowGet);
           
        }
        #endregion

        #region My Sales Report
        [AcceptVerbs("POST")]
        public JsonResult MySalesReport(string user_id, string request_type,string Token)
        {
            MysalesReportOutput omodel = new MysalesReportOutput();
            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == Token)
                {

                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);

                    sqlcon.Open();

                    sqlcmd = new SqlCommand("Proc_MysalesCustomer", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@userid", user_id);
                    sqlcmd.Parameters.Add("@request_type", request_type);
                   
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {
                            omodel.ResponseCode = "200";
                            omodel.Responsedetails = "Success";
                            omodel.total_sales = Convert.ToInt32(dt.Rows[0]["total_sales"]);
                            omodel.customer_approval_details = APIHelperMethods.ToModelList<MysalesReportCustomerOutput>(dt);
                            return Json(omodel);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "No Record Found";
                            return Json(oerror);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "No Record Found";

                        return Json(oerror);
                    }

                }
                else
                {
                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not match";

                    return Json(oerror);

                }


            }
            catch
            {
                oerror.ResponseCode = "103";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }

        }
        #endregion

        #region My Sales Details Report
        [AcceptVerbs("POST")]
        public JsonResult MySalesDetailsReport(string user_id, string request_id, string Token)
        {
            Salesmandiscountapproval omodel = new Salesmandiscountapproval();
            Commonclass oerror = new Commonclass();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == Token)
                {
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);


                    sqlcon.Open();

                    sqlcmd = new SqlCommand("Proc_MysalesDetailsCustomer", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@user_id", user_id);
                    sqlcmd.Parameters.Add("@request_id", request_id);
              

                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {
                            omodel.ResponseCode = "200";
                            omodel.Responsedetails = "Success";
                            omodel.RequestId = Convert.ToInt32(dt.Rows[0]["RequestId"]);
                            omodel.exchange_amount = Convert.ToDecimal(dt.Rows[0]["exchange_amount"]);
                            omodel.payment_type = Convert.ToString(dt.Rows[0]["payment_type"]);
                            omodel.view_approval_details = APIHelperMethods.ToModelList<productfordiscountsalesman>(dt);
                            return Json(omodel);
                        }
                        else
                        {
                            oerror.ResponseCode = "201";
                            oerror.Responsedetails = "No Record Found";
                            return Json(oerror);
                        }

                    }
                    else
                    {
                        oerror.ResponseCode = "201";
                        oerror.Responsedetails = "No Record Found";

                        return Json(oerror);
                    }

                }
                else
                {
                    oerror.ResponseCode = "103";
                    oerror.Responsedetails = "Token Id not match";

                    return Json(oerror);

                }


            }
            catch
            {
                oerror.ResponseCode = "201";
                oerror.Responsedetails = "Error";
                return Json(oerror);
            }

        }
        #endregion

        #region User Change password
        [AcceptVerbs("POST")]
        public JsonResult Changepassword(ChangePasswordInput model)
        {
            
            ErrorModel omodelerror = new ErrorModel();
            try
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                if (token == model.Token)
                {
                    string sessionId = HttpContext.Session.SessionID;

                    DataTable dt = new DataTable();
                    Encryption epasswrd = new Encryption();
                    string Encryptpassold = epasswrd.Encrypt(model.Old_password);
                    string Encryptpassnew = epasswrd.Encrypt(model.New_password);

                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];

                    SqlCommand sqlcmd = new SqlCommand();

                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("Sp_ApiChangePassword", sqlcon);
                    sqlcmd.Parameters.Add("@User_id", model.User_id);
                    sqlcmd.Parameters.Add("@Old_password", Encryptpassold);
                    sqlcmd.Parameters.Add("@New_password", Encryptpassnew);
                    sqlcmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {
                            omodelerror.ResponseCode = "200";
                            omodelerror.Responsedetails = "Success";
                            return Json(omodelerror);
                        }
                        else
                        {
                            omodelerror.ResponseCode = "201";
                            omodelerror.Responsedetails = "Old Password does not matched";
                            return Json(omodelerror);
                        }

                    }
                    else
                    {
                        omodelerror.ResponseCode = "201";
                        omodelerror.Responsedetails = "Invalid User Id";
                        return Json(omodelerror);
                    }


                }
                else
                {
                    omodelerror.ResponseCode = "103";
                    omodelerror.Responsedetails = "Token Id not match";
                    return Json(omodelerror);

                }

            }
            catch
            {

                omodelerror.ResponseCode = "103";
                omodelerror.Responsedetails = "Error occured";
                return Json(omodelerror);

            }


        }

        #endregion


    }
}
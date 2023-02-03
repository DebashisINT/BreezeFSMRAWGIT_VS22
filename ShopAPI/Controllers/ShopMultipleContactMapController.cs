#region======================================Revision History=========================================================
//Written By : Debashis Talukder On 10/01/2023
//Purpose: Add mutiple contact for a Shop.Refer: Row:783 to 785 and 799 to 801
#endregion===================================End of Revision History==================================================
using ShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShopAPI.Controllers
{
    public class ShopMultipleContactMapController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage AddShopMultiContact(ShopMultiContactMapInput model)
        {
            ShopMultiContactMapOutput omodel = new ShopMultiContactMapOutput();
            List<ShopMultiContactList> omedl2 = new List<ShopMultiContactList>();
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

                    foreach (var s2 in model.shop_list)
                    {
                        omedl2.Add(new ShopMultiContactList()
                        {
                            shop_id = s2.shop_id,
                            contact_serial1 = s2.contact_serial1,
                            contact_name1 = s2.contact_name1,
                            contact_number1 = s2.contact_number1,
                            contact_email1 = s2.contact_email1,
                            contact_doa1 = s2.contact_doa1,
                            contact_dob1= s2.contact_dob1,
                            contact_serial2 = s2.contact_serial2,
                            contact_name2 = s2.contact_name2,
                            contact_number2 = s2.contact_number2,
                            contact_email2 = s2.contact_email2,
                            contact_doa2 = s2.contact_doa2,
                            contact_dob2= s2.contact_dob2,
                            contact_serial3 = s2.contact_serial3,
                            contact_name3 = s2.contact_name3,
                            contact_number3 = s2.contact_number3,
                            contact_email3 = s2.contact_email3,
                            contact_doa3 = s2.contact_doa3,
                            contact_dob3= s2.contact_dob3,
                            contact_serial4 = s2.contact_serial4,
                            contact_name4 = s2.contact_name4,
                            contact_number4 = s2.contact_number4,
                            contact_email4 = s2.contact_email4,
                            contact_doa4 = s2.contact_doa4,
                            contact_dob4= s2.contact_dob4,
                            contact_serial5 = s2.contact_serial5,
                            contact_name5 = s2.contact_name5,
                            contact_number5 = s2.contact_number5,
                            contact_email5 = s2.contact_email5,
                            contact_doa5 = s2.contact_doa5,
                            contact_dob5= s2.contact_dob5,
                            contact_serial6 = s2.contact_serial6,
                            contact_name6 = s2.contact_name6,
                            contact_number6 = s2.contact_number6,
                            contact_email6 = s2.contact_email6,
                            contact_doa6 = s2.contact_doa6,
                            contact_dob6= s2.contact_dob6
                        });
                    }

                    string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);
                    String dates = DateTime.Now.ToString("ddMMyyyyhhmmssffffff");
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcmd.CommandTimeout = 60;
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APIMASTERSHOPMULTICONTACTMAP", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "ADD");
                    sqlcmd.Parameters.AddWithValue("@session_token", model.session_token);
                    sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@JsonXML", JsonXML);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0 && Convert.ToString(dt.Rows[0]["STRMESSAGE"]) == "Success")
                    {
                        omodel.status = "200";
                        omodel.message = "Saved successfully.";
                    }
                    else
                    {
                        omodel.status = "205";
                        omodel.message = "Records not added.";
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
        public HttpResponseMessage EditShopMultiContact(EditShopMultiContactMapInput model)
        {
            EditShopMultiContactMapOutput omodel = new EditShopMultiContactMapOutput();
            List<EditShopMultiContactList> omedl2 = new List<EditShopMultiContactList>();
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

                    foreach (var s2 in model.shop_list)
                    {
                        omedl2.Add(new EditShopMultiContactList()
                        {
                            shop_id = s2.shop_id,
                            contact_serial1 = s2.contact_serial1,
                            contact_name1 = s2.contact_name1,
                            contact_number1 = s2.contact_number1,
                            contact_email1 = s2.contact_email1,
                            contact_doa1 = s2.contact_doa1,
                            contact_dob1= s2.contact_dob1,
                            contact_serial2 = s2.contact_serial2,
                            contact_name2 = s2.contact_name2,
                            contact_number2 = s2.contact_number2,
                            contact_email2 = s2.contact_email2,
                            contact_doa2 = s2.contact_doa2,
                            contact_dob2= s2.contact_dob2,
                            contact_serial3 = s2.contact_serial3,
                            contact_name3 = s2.contact_name3,
                            contact_number3 = s2.contact_number3,
                            contact_email3 = s2.contact_email3,
                            contact_doa3 = s2.contact_doa3,
                            contact_dob3= s2.contact_dob3,
                            contact_serial4 = s2.contact_serial4,
                            contact_name4 = s2.contact_name4,
                            contact_number4 = s2.contact_number4,
                            contact_email4 = s2.contact_email4,
                            contact_doa4 = s2.contact_doa4,
                            contact_dob4= s2.contact_dob4,
                            contact_serial5 = s2.contact_serial5,
                            contact_name5 = s2.contact_name5,
                            contact_number5 = s2.contact_number5,
                            contact_email5 = s2.contact_email5,
                            contact_doa5 = s2.contact_doa5,
                            contact_dob5= s2.contact_dob5,
                            contact_serial6 = s2.contact_serial6,
                            contact_name6 = s2.contact_name6,
                            contact_number6 = s2.contact_number6,
                            contact_email6 = s2.contact_email6,
                            contact_doa6 = s2.contact_doa6,
                            contact_dob6= s2.contact_dob6
                        });
                    }

                    string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);
                    String dates = DateTime.Now.ToString("ddMMyyyyhhmmssffffff");
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcmd.CommandTimeout = 60;
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APIMASTERSHOPMULTICONTACTMAP", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "EDIT");
                    sqlcmd.Parameters.AddWithValue("@session_token", model.session_token);
                    sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@JsonXML", JsonXML);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0 && Convert.ToString(dt.Rows[0]["STRMESSAGE"]) == "Success")
                    {
                        omodel.status = "200";
                        omodel.message = "Update successfully.";
                    }
                    else
                    {
                        omodel.status = "205";
                        omodel.message = "Records not updated.";
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
        public HttpResponseMessage FetchShopMultiContact(FetchShopMultiContactMapInput model)
        {
            FetchShopMultiContactMapOutput omodel = new FetchShopMultiContactMapOutput();
            List<FetchShopMultiContactList> oview = new List<FetchShopMultiContactList>();
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

                    String dates = DateTime.Now.ToString("ddMMyyyyhhmmssffffff");
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcmd.CommandTimeout = 60;
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APIMASTERSHOPMULTICONTACTMAP", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "FETCHDATA");
                    sqlcmd.Parameters.AddWithValue("@session_token", model.session_token);
                    sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        oview = APIHelperMethods.ToModelList<FetchShopMultiContactList>(dt);
                        omodel.status = "200";
                        omodel.message = "Successfully get list.";
                        omodel.user_id = model.user_id;
                        omodel.shop_list = oview;
                    }
                    else
                    {
                        omodel.status = "205";
                        omodel.message = "No list available.";
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

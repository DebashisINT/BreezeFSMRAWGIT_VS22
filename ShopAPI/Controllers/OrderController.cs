#region======================================Revision History=========================================================
//1.0   V2.0.38     Debashis    23/01/2023      Some new parameters have been added.Row: 805 to 806
//2.0   V2.0.49     Debashis    17/09/2024      A new parameter has been added.Row: 977
//3.0   V2.0.49     Debashis    17/09/2024      A new method has been added.Row: 978
#endregion===================================End of Revision History==================================================
using ShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web;
using System.Web.Http;

namespace ShopAPI.Controllers
{
    public class OrderController : ApiController
    {

        public delegate void del(string username, string password, string Provider, string senderId, string mobile, string message, string type);

        [HttpPost]
        public HttpResponseMessage AddOrder(OrdermanagementInput model)
        {
            OrderAddoutput omodel = new OrderAddoutput();
            UserClass oview = new UserClass();
            String username = System.Configuration.ConfigurationManager.AppSettings["username"];
            String password = System.Configuration.ConfigurationManager.AppSettings["password"];
            String Provider = System.Configuration.ConfigurationManager.AppSettings["Provider"];
            String sender = System.Configuration.ConfigurationManager.AppSettings["sender"];
            try
            {
                string products = "";
                string token = string.Empty;
                string versionname = string.Empty;
                System.Net.Http.Headers.HttpRequestHeaders headers = this.Request.Headers;
                String tokenmatch = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];

                if (!ModelState.IsValid)
                {
                    omodel.status = "213";
                    omodel.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
                }

                else
                {
                    // String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                    string sessionId = "";


                    DataTable dt = new DataTable();

                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    //String con = Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]);


                    List<OrderProductlist> omedl2 = new List<OrderProductlist>();

                    foreach (var s2 in model.product_list)
                    {

                        omedl2.Add(new OrderProductlist()
                        {

                            id = s2.id,
                            qty = s2.qty,
                            rate = s2.rate,
                            total_price = s2.total_price,
                            product_name = s2.product_name,
                            //Extra Input for RubyFood
                            scheme_qty=s2.scheme_qty,
                            scheme_rate=s2.scheme_rate,
                            total_scheme_price=s2.total_scheme_price,
                            //Extra Input for RubyFood
                            //Extra Input for EuroBond
                            MRP=s2.MRP,
                            //Extra Input for EuroBond
                            //Rev 1.0 Row: 805
                            order_mrp=s2.order_mrp,
                            order_discount=s2.order_discount
                            //End of Rev 1.0 Row: 805
                        });

                        products = products + s2.product_name + "," + " Price: " + s2.total_price + "," + " Qty: " + s2.qty + "||";

                    }


                    string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);


                    OrderAddoutput omodeloutput = new OrderAddoutput();

                    SqlCommand sqlcmd = new SqlCommand();

                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();

                    sqlcmd = new SqlCommand("Proc_FTS_Order", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@SessionToken", model.session_token);
                    sqlcmd.Parameters.AddWithValue("@order_amount", model.order_amount);
                    sqlcmd.Parameters.AddWithValue("@order_id", model.order_id);
                    sqlcmd.Parameters.AddWithValue("@Shop_Id", model.shop_id);
                    sqlcmd.Parameters.AddWithValue("@description", model.description);
                    sqlcmd.Parameters.AddWithValue("@Collection", model.collection);
                    sqlcmd.Parameters.AddWithValue("@order_date", model.order_date);
                    sqlcmd.Parameters.AddWithValue("@Remarks", model.remarks);
                    //Extra Input for 4Basecare
                    sqlcmd.Parameters.AddWithValue("@patient_no", model.patient_no);
                    sqlcmd.Parameters.AddWithValue("@patient_name", model.patient_name);
                    sqlcmd.Parameters.AddWithValue("@patient_address ", model.patient_address);
                    //Extra Input for 4Basecare
                    //Extra Input for RubyFood
                    sqlcmd.Parameters.AddWithValue("@Scheme_Amount ", model.scheme_amount);
                    //Extra Input for RubyFood
                    //Extra Input for EuroBond
                    sqlcmd.Parameters.AddWithValue("@Hospital ", model.Hospital);
                    sqlcmd.Parameters.AddWithValue("@Email_Address ", model.Email_Address);
                    //Extra Input for EuroBond
                    //Rev 2.0 Row: 907
                    sqlcmd.Parameters.AddWithValue("@OrderStatus ", model.OrderStatus);
                    //End of Rev 2.0 Row: 907
                    sqlcmd.Parameters.AddWithValue("@Product_List", JsonXML);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();


                    if (dt.Rows.Count > 0)
                    {
                        // omodel = APIHelperMethods.ToModel<OrderAddoutput>(dt);

                        omodeloutput.user_id = Convert.ToString(dt.Rows[0]["user_id"]);
                        omodeloutput.order_amount = Convert.ToString(dt.Rows[0]["order_amount"]);
                        omodeloutput.order_id = Convert.ToString(dt.Rows[0]["order_id"]);
                        omodeloutput.description = Convert.ToString(dt.Rows[0]["decription"]);
                        omodeloutput.Shop_Name = Convert.ToString(dt.Rows[0]["Shop_Name"]);
                        omodeloutput.Shop_Owner = Convert.ToString(dt.Rows[0]["Shop_Owner"]);
                        omodeloutput.Shop_Owner_Contact = Convert.ToString(dt.Rows[0]["Shop_Owner_Contact"]);


                        string messagetext = "Hi " + omodeloutput.Shop_Owner + " An Order is initialized against your Shop : " + omodeloutput.Shop_Name + "|| Product List :" + products;
                        del ldMainAct = new del(SmsSent);
                        ldMainAct.BeginInvoke(username, password, Provider, sender, omodeloutput.Shop_Owner_Contact, messagetext, "Text", null, null);


                        omodeloutput.status = "200";
                        omodeloutput.message = "Order Added Successfully.";

                        //    string messagetext = "Hi " + omodeloutput.Shop_Owner + " An Order is initialized against your Shop : " + omodeloutput.Shop_Name + "|| Product List :" + products;
                        //    string res = SmsSent(username, password, Provider, sender, omodeloutput.Shop_Owner_Contact, messagetext, "Text");
                    }
                    else
                    {
                        omodeloutput.status = "202";
                        omodeloutput.message = "No entry.";
                    }

                    var message = Request.CreateResponse(HttpStatusCode.OK, omodeloutput);
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
        public HttpResponseMessage OrderList(OrderfetchInput model)
        {

            List<Orderfetch> oview = new List<Orderfetch>();
            OrderfetchOutput odata = new OrderfetchOutput();

            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
            }
            else
            {

                String token = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];
                string sessionId = "";

                List<Locationupdate> omedl2 = new List<Locationupdate>();

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("Proc_FTC_OrderList", sqlcon);
                sqlcmd.Parameters.AddWithValue("@date", model.date);
                sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);


                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<Orderfetch>(dt);

                    odata.order_list = oview;
                    odata.status = "200";
                    odata.message = "Order details  available";



                }
                else
                {

                    odata.status = "205";
                    odata.message = "No data found";

                }

                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }

        }


        [HttpPost]
        public HttpResponseMessage OrderDetailsList(OrderDetailsfetchInput model)
        {


            OrderDetailsOutput odata = new OrderDetailsOutput();

            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
            }
            else
            {

                String token = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];
                string sessionId = "";

                List<Locationupdate> omedl2 = new List<Locationupdate>();

                DataSet ds = new DataSet();
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("Proc_FTC_OrderListDetails", sqlcon);
                sqlcmd.Parameters.AddWithValue("@shop_id", model.shop_id);
                sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);
                sqlcmd.Parameters.AddWithValue("@order_id", model.order_id);


                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(ds);
                sqlcon.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<OrderProducts> deliverprod = new List<OrderProducts>();
                    odata.total_orderlist_count = Convert.ToString(ds.Tables[0].Rows[0]["total_orderlist_count"]);
                    //odata.order_id = Convert.ToString(ds.Tables[0].Rows[0]["order_id"]);
                    odata.shop_id = Convert.ToString(ds.Tables[1].Rows[0]["shop_id"]);
                    //   oview = APIHelperMethods.ToModelList<OrderfetchdetailsList>(ds.Tables[1]);
                    List<OrderfetchdetailsList> oview = new List<OrderfetchdetailsList>();
                    OrderfetchdetailsList orderdetailprd = new OrderfetchdetailsList();
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {


                        List<OrderProducts> delivermodelproduct = new List<OrderProducts>();
                        for (int j = 0; j < ds.Tables[2].Rows.Count; j++)
                        {


                            int i1 = 0;
                            if (Convert.ToString(ds.Tables[2].Rows[j]["Order_ID"]) == Convert.ToString(ds.Tables[1].Rows[i]["order_id"]))
                            {

                                delivermodelproduct.Add(new OrderProducts()
                                    {

                                        id = Convert.ToInt32(ds.Tables[2].Rows[j]["id"]),
                                        brand_id = Convert.ToInt32(ds.Tables[2].Rows[j]["brand_id"]),
                                        category_id = Convert.ToInt32(ds.Tables[2].Rows[j]["category_id"]),
                                        watt_id = Convert.ToInt32(ds.Tables[2].Rows[j]["watt_id"]),
                                        brand = Convert.ToString(ds.Tables[2].Rows[j]["brand"]),
                                        category = Convert.ToString(ds.Tables[2].Rows[j]["category"]),
                                        watt = Convert.ToString(ds.Tables[2].Rows[j]["watt"]),
                                        qty = Convert.ToDecimal(ds.Tables[2].Rows[j]["qty"]),
                                        rate = Convert.ToDecimal(ds.Tables[2].Rows[j]["rate"]),
                                        total_price = Convert.ToDecimal(ds.Tables[2].Rows[j]["total_price"]),
                                        product_name = Convert.ToString(ds.Tables[2].Rows[j]["product_name"])
                                    });

                            }


                        }
                        oview.Add(new OrderfetchdetailsList()
                        {
                            product_list = delivermodelproduct,
                            id = Convert.ToString(ds.Tables[1].Rows[i]["id"]),
                            date = Convert.ToString(ds.Tables[1].Rows[i]["date"]),
                            amount = Convert.ToString(ds.Tables[1].Rows[i]["amount"]),
                            description = Convert.ToString(ds.Tables[1].Rows[i]["description"]),
                            collection = Convert.ToString(ds.Tables[1].Rows[i]["collection"])
                        });





                    }

                    odata.order_details_list = oview;
                    odata.status = "200";
                    odata.message = "Order details  available";



                }
                else
                {

                    odata.status = "205";
                    odata.message = "No data found";

                }

                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }

        }


        [HttpPost]
        public HttpResponseMessage OrderDetailsShopList(OrderDetailsfetchInput_Shop model)
        {
            try
            {
                OrderDetailsOutput_Shop odata = new OrderDetailsOutput_Shop();

                if (!ModelState.IsValid)
                {
                    odata.status = "213";
                    odata.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
                }
                else
                {
                    String token = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];
                    string sessionId = "";

                    List<Locationupdate> omedl2 = new List<Locationupdate>();

                    DataSet ds = new DataSet();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("Proc_FTC_OrderListDetails_ShopList", sqlcon);

                    sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@Date", model.date);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(ds);
                    sqlcon.Close();
                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
                    {
                        List<OrderProducts> deliverprod = new List<OrderProducts>();
                        odata.total_orderlist_count = Convert.ToString(ds.Tables[0].Rows[0]["total_orderlist_count"]);

                        List<OrderfetchdetailsList_Shop> oview = new List<OrderfetchdetailsList_Shop>();
                        OrderfetchdetailsList orderdetailprd = new OrderfetchdetailsList();
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            List<OrderProducts_Shop> delivermodelproduct = new List<OrderProducts_Shop>();
                            for (int j = 0; j < ds.Tables[2].Rows.Count; j++)
                            {
                                if (ds.Tables[2].Rows.Count > 0)
                                {
                                    int i1 = 0;
                                    if (Convert.ToString(ds.Tables[2].Rows[j]["SHOPID"]) == Convert.ToString(ds.Tables[1].Rows[i]["SHOPID"]) && Convert.ToString(ds.Tables[2].Rows[j]["Order_ID"]) == Convert.ToString(ds.Tables[1].Rows[i]["ORDID"]))
                                    {
                                        delivermodelproduct.Add(new OrderProducts_Shop()
                                        {
                                            id = Convert.ToInt32(ds.Tables[2].Rows[j]["PRODID"]),
                                            brand_id = Convert.ToInt32(ds.Tables[2].Rows[j]["brand_id"]),
                                            category_id = Convert.ToInt32(ds.Tables[2].Rows[j]["category_id"]),
                                            watt_id = Convert.ToInt32(ds.Tables[2].Rows[j]["watt_id"]),
                                            brand = Convert.ToString(ds.Tables[2].Rows[j]["brand"]),
                                            category = Convert.ToString(ds.Tables[2].Rows[j]["category"]),
                                            watt = Convert.ToString(ds.Tables[2].Rows[j]["watt"]),
                                            qty = Convert.ToDecimal(ds.Tables[2].Rows[j]["qty"]),
                                            rate = Convert.ToDecimal(ds.Tables[2].Rows[j]["rate"]),
                                            total_price = Convert.ToDecimal(ds.Tables[2].Rows[j]["total_price"]),
                                            product_name = Convert.ToString(ds.Tables[2].Rows[j]["product_name"]),
                                            //Extra Output for RubyFood
                                            scheme_qty = Convert.ToDecimal(ds.Tables[2].Rows[j]["scheme_qty"]),
                                            scheme_rate = Convert.ToDecimal(ds.Tables[2].Rows[j]["scheme_rate"]),
                                            total_scheme_price = Convert.ToDecimal(ds.Tables[2].Rows[j]["total_scheme_price"]),
                                            //Extra Output for RubyFood
                                            //Extra Output for EuroBond
                                            MRP = Convert.ToDecimal(ds.Tables[2].Rows[j]["MRP"]),
                                            //Extra Output for EuroBond
                                            //Rev 1.0 Row: 806
                                            order_mrp = Convert.ToDecimal(ds.Tables[2].Rows[j]["order_mrp"]),
                                            order_discount = Convert.ToDecimal(ds.Tables[2].Rows[j]["order_discount"])
                                            //End of Rev 1.0 Row: 806
                                        });
                                    }
                                }
                            }
                            oview.Add(new OrderfetchdetailsList_Shop()
                            {
                                product_list = delivermodelproduct,
                                shop_id = Convert.ToString(ds.Tables[1].Rows[i]["SHOPID"]),
                                shop_address = Convert.ToString(ds.Tables[1].Rows[i]["ADDRESS"]),
                                shop_name = Convert.ToString(ds.Tables[1].Rows[i]["NAME"]),
                                shop_contact_no = Convert.ToString(ds.Tables[1].Rows[i]["CONTACT"]),
                                pin_code = Convert.ToString(ds.Tables[1].Rows[i]["PINCODE"]),
                                shop_lat = Convert.ToString(ds.Tables[1].Rows[i]["LATITUDE"]),
                                shop_long = Convert.ToString(ds.Tables[1].Rows[i]["LONGITUDE"]),
                                order_id = Convert.ToString(ds.Tables[1].Rows[i]["ORDRNO"]),
                                order_date_time = string.IsNullOrEmpty(Convert.ToString(ds.Tables[1].Rows[i]["ORDDATE"])) ? default(DateTime?) : Convert.ToDateTime(ds.Tables[1].Rows[i]["ORDDATE"]),
                                order_amount = Convert.ToDecimal(ds.Tables[1].Rows[i]["order_amount"]),
                                order_lat = Convert.ToString(ds.Tables[1].Rows[i]["Latitude"]),
                                order_long = Convert.ToString(ds.Tables[1].Rows[i]["Longitude"]),
                                patient_no = Convert.ToString(ds.Tables[1].Rows[i]["PATIENT_PHONE_NO"]),
                                patient_name = Convert.ToString(ds.Tables[1].Rows[i]["PATIENT_NAME"]),
                                patient_address = Convert.ToString(ds.Tables[1].Rows[i]["PATIENT_ADDRESS"]),
                                //Extra Output for RubyFood
                                scheme_amount = Convert.ToDecimal(ds.Tables[1].Rows[i]["scheme_amount"]),
                                //Extra Output for RubyFood
                                //Extra Output for EuroBond
                                Hospital = Convert.ToString(ds.Tables[1].Rows[i]["Hospital"]),
                                Email_Address = Convert.ToString(ds.Tables[1].Rows[i]["Email_Address"])
                                //Extra Output for EuroBond
                            });
                        }
                        odata.order_list = oview;
                        odata.status = "200";
                        odata.message = "Order details  available";
                    }
                    else
                    {
                        odata.status = "205";
                        odata.message = "No data found";
                    }

                    var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                    return message;
                }
            }
            catch (Exception ex)
            {
                var message = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                return message;
            }
        }


        public void SmsSent(string username, string password, string Provider, string senderId, string mobile, string message, string type)
        {

            //  http://5.189.187.82/sendsms/sendsms.php?username=QHEkaruna&password=baj8piv3&type=Text&sender=KARUNA&mobile=9831892083&message=HELO
            string response = "";
            string url = Provider + "?username=" + username + "&password=" + password + "&type=" + type + "&sender=" + senderId + "&mobile=" + mobile + "&message=" + message;
            if (mobile.Trim() != "")
            {
                try
                {
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    response = httpResponse.StatusCode.ToString();
                }
                catch
                {
                    //  return "0";

                }
            }
            // return response;
        }



        [HttpPost]
        public HttpResponseMessage OrderShopMail(OrderMailInput model)
        {

            try
            {
                OrderMailOutput odata = new OrderMailOutput();

                if (!ModelState.IsValid)
                {
                    odata.status = "213";
                    odata.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
                }
                else
                {

                    String token = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];
                    string sessionId = "";

                    List<Locationupdate> omedl2 = new List<Locationupdate>();

                    DataSet ds = new DataSet();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("Proc_FTC_OrderMail", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@order_id", model.order_id);
                    sqlcmd.Parameters.AddWithValue("@shop_id", model.shop_id);
                    sqlcmd.Parameters.AddWithValue("@type", model.type);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(ds);
                    sqlcon.Close();
                    if (ds.Tables.Count>0)
                    {
                        if (ds.Tables[4] != null && ds.Tables[4].Rows.Count > 0)
                        {
                            string Subject = Convert.ToString(ds.Tables[0].Rows[0][0]);
                            string EmailBody = Convert.ToString(ds.Tables[1].Rows[0][0]);
                            string ToEmail = Convert.ToString(ds.Tables[2].Rows[0][0]);
                            string CCEmail = Convert.ToString(ds.Tables[3].Rows[0][0]);



                            String mailsStatus = SendMail(ToEmail, CCEmail, "", Subject, EmailBody, null, ds.Tables[4]);
                            if (mailsStatus == "Success")
                            {
                                odata.status = "200";
                                odata.message = "Successfully send email.";
                            }
                            else
                            {
                                odata.status = "200";
                                odata.message = "Email not send.";
                            }
                        }
                        else
                        {
                            odata.status = "205";
                            odata.message = "Mail configuration not found.";
                        }

                    }
                    else
                    {
                        odata.status = "205";
                        odata.message = "Mail id not found.";
                    }

                    var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                    return message;


                }
            }
            catch (Exception ex)
            {

                var message = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                return message;

            }

        }

        internal String SendMail(string ToEmail, string CCEmail, string BCCEmail, string Subject, string EmailBody, HttpFileCollectionBase files, DataTable dtFromEmailDet)
        {
            try
            {
                var Email = dtFromEmailDet.Rows[0][0].ToString();
                var Password = dtFromEmailDet.Rows[0][1].ToString();

                //var Email = "subhra.mukherjee@indusnet.co.in";
                //var Password = "subhra@12345";
                var FromWhere = dtFromEmailDet.Rows[0][2].ToString();
                var OutgoingSMTPHost = dtFromEmailDet.Rows[0][3].ToString();
                var OutgoingPort = dtFromEmailDet.Rows[0][4].ToString();
                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient(OutgoingSMTPHost);
                var FromAdd = Email;
                string[] ToAdd = ToEmail.Split(',');
                string[] CcAdd = CCEmail.Split(',');
                string[] BccAdd = BCCEmail.Split(',');
                var Body = EmailBody;
                var EmailSubject = Subject;
                mail.From = new MailAddress(FromAdd, FromWhere);

                foreach (string to in ToAdd)
                {
                    mail.To.Add(to);
                }
                foreach (string cc in CcAdd)
                {
                    if (cc != "")
                    {
                        mail.CC.Add(cc);
                    }
                }
                foreach (string bcc in BccAdd)
                {
                    if (bcc != "")
                    {
                        mail.Bcc.Add(bcc);
                    }
                }

                mail.Subject = EmailSubject;
                mail.IsBodyHtml = true;
                mail.Body = Body;
                HttpPostedFileBase file = null;
                HttpFileCollectionBase filess = files;
                if (filess != null)
                {
                    for (int i = 0; i < filess.Count; i++)
                    {

                        if (filess[i] != null && files[i].ContentLength > 0)
                        {
                            var attachment = new Attachment(filess[i].InputStream, filess[i].FileName);
                            mail.Attachments.Add(attachment);
                        }
                    }

                }


                smtp.Host = OutgoingSMTPHost.Trim();
                smtp.Port = Convert.ToInt32(OutgoingPort);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(FromAdd, Password);
                smtp.EnableSsl = true;

                smtp.Send(mail);
                smtp.Dispose();
                mail.Dispose();
                return "Success";
            }
            catch (Exception EX)
            {
                return null;
            }
        }

        //Rev 3.0 Row: 978
        [HttpPost]
        public HttpResponseMessage OrderStatusList(OrderStatusfetchInput model)
        {            
            OrderStatusfetchOutput odata = new OrderStatusfetchOutput();
            List<OrderStatusfetch> oview = new List<OrderStatusfetch>();

            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
            }
            else
            {
                String token = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_FSMAPIORDERSTATUS", sqlcon);
                sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<OrderStatusfetch>(dt);
                    odata.user_id = model.user_id;
                    odata.order_status_list = oview;
                    odata.status = "200";
                    odata.message = "Successfully get Order Status List.";
                }
                else
                {
                    odata.status = "205";
                    odata.message = "No data found";
                }

                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }
        }
        //End of Rev 3.0 Row: 978
    }
}

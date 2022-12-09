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
    public class CollectionController : ApiController
    {
        public delegate void del(string username, string password, string Provider, string senderId, string mobile, string message, string type);

        String username = System.Configuration.ConfigurationSettings.AppSettings["username"];
        String password = System.Configuration.ConfigurationSettings.AppSettings["password"];
        String Provider = System.Configuration.ConfigurationSettings.AppSettings["Provider"];
        String sender = System.Configuration.ConfigurationSettings.AppSettings["sender"];
        [HttpPost]
        public HttpResponseMessage AddCollection(Collectionclass_Input model)
        {      
            Collectionclass_Output odata = new Collectionclass_Output();

            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
            }
            else
            {

                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                string sessionId = "";

                List<Locationupdate> omedl2 = new List<Locationupdate>();

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("proc_FTS_Collection", sqlcon);
      
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@shop_id", model.shop_id);
                sqlcmd.Parameters.Add("@collection", model.collection);
                sqlcmd.Parameters.Add("@collection_id", model.collection_id);
                sqlcmd.Parameters.Add("@collection_date", model.collection_date);
                sqlcmd.Parameters.Add("@bill_id", model.bill_id);
                //Extra Input for Collection
                sqlcmd.Parameters.Add("@payment_id", model.payment_id);
                sqlcmd.Parameters.Add("@instrument_no", model.instrument_no);
                sqlcmd.Parameters.Add("@bank", model.bank);
                sqlcmd.Parameters.Add("@remarks", model.remarks);
                //Extra Input for Collection
                //Extra Input for 4Basecare
                sqlcmd.Parameters.Add("@patient_no", model.patient_no);
                sqlcmd.Parameters.Add("@patient_name", model.patient_name);
                sqlcmd.Parameters.Add("@patient_address", model.patient_address);
                //Extra Input for 4Basecare
                //Extra Input for EuroBond
                sqlcmd.Parameters.Add("@Hospital", model.Hospital);
                sqlcmd.Parameters.Add("@Email_Address", model.Email_Address);
                //Extra Input for EuroBond
                //Rev Debashis
                sqlcmd.Parameters.Add("@order_id", model.order_id);
                //End of Rev Debashis
            
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    odata = APIHelperMethods.ToModel<Collectionclass_Output>(dt);

                    odata.user_id = Convert.ToString(dt.Rows[0]["user_id"]);
                    odata.collection = Convert.ToString(dt.Rows[0]["collection"]);
                    odata.shop_id = Convert.ToString(dt.Rows[0]["shop_id"]);
                    odata.Shop_Name = Convert.ToString(dt.Rows[0]["Shop_Name"]);
                    odata.Shop_Owner = Convert.ToString(dt.Rows[0]["Shop_Owner"]);
                    odata.Shop_Owner_Contact = Convert.ToString(dt.Rows[0]["Shop_Owner_Contact"]);

                    string messagetext = "Hi " + odata.Shop_Owner + " " + odata.collection + " amount is colected from your Shop : " + odata.Shop_Name;
                    del ldMainAct = new del(SmsSent);
                    ldMainAct.BeginInvoke(username, password, Provider, sender, odata.Shop_Owner_Contact, messagetext, "Text", null, null);


                    odata.status = "200";
                    odata.message = "Collection Details Saved Successfully";

                 //   string res = SmsSent(username, password, Provider, sender, odata.Shop_Owner_Contact, messagetext, "Text");

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
        public HttpResponseMessage ListCollection(Collectionclass_Input model)
        {

            List<collection_details_list> oview = new List<collection_details_list>();
            CollectionList_Output odata = new CollectionList_Output();

            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
            }
            else
            {

                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                string sessionId = "";

                List<Locationupdate> omedl2 = new List<Locationupdate>();

                DataSet dt = new DataSet();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("proc_FTS_CollectionList", sqlcon);
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@shop_id", model.shop_id);
          

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Tables[0].Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<collection_details_list>(dt.Tables[1]);
                    odata.collection_details_list = oview;
                    odata.total_orderlist_count = Convert.ToInt32(dt.Tables[0].Rows[0]["countcollection"]);
                    odata.status = "200";
                    odata.message = "Collection List Populated Successfully";

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
        public HttpResponseMessage Collectionlist(Collectionclass_Input model)
        {
            List<collection_details_list_bind> oview = new List<collection_details_list_bind>();
            CollectionList_Output_bind odata = new CollectionList_Output_bind();

            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
            }
            else
            {

                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];

                String CollectionFile = System.Configuration.ConfigurationSettings.AppSettings["CollectionFile"];

                string sessionId = "";

                List<Locationupdate> omedl2 = new List<Locationupdate>();

                DataSet dt = new DataSet();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("proc_FTS_CollectionList", sqlcon);
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@shop_id", model.shop_id);
                sqlcmd.Parameters.Add("@Action", "AllCollection");
                sqlcmd.Parameters.Add("@url", CollectionFile);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Tables[0].Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<collection_details_list_bind>(dt.Tables[1]);
                    odata.collection_list = oview;
                    odata.total_collectionist_count = Convert.ToInt32(dt.Tables[0].Rows[0]["countcollection"]);
                    odata.status = "200";
                    odata.message = "Collection List Populated Successfully";

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


        public HttpResponseMessage InvoiceList(CollectionInvoiceList_Input model)
        {

            List<Invoice_List> oview = new List<Invoice_List>();
            CollectionInvoiceList_Output odata = new CollectionInvoiceList_Output();

            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
            }
            else
            {

                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                string sessionId = "";

                List<Invoice_List> omedl2 = new List<Invoice_List>();

                DataSet dt = new DataSet();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("proc_FTS_CollectionList", sqlcon);
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@shop_id", model.shop_id);
                sqlcmd.Parameters.Add("@Action", "InvoiceList");

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dr in dt.Tables[0].Rows)
                    {
                        Invoice_List obj = new Invoice_List();
                        obj.bal_amount = Convert.ToString(dr["Invoice_Unpaid"]);
                        obj.bill_id = Convert.ToString(dr["bill_id"]);
                        obj.billing_image = Convert.ToString(dr["billing_image"]);
                        obj.invoice_date = Convert.ToString(dr["invoice_date"]);
                        obj.invoice_no = Convert.ToString(dr["invoice_no"]);
                        obj.order_id = Convert.ToString(dr["OrderCode"]);
                        obj.paid_amount = Convert.ToString(dr["paid_amt"]);
                        obj.user_id = Convert.ToString(dr["User_Id"]);
                        obj.total_amount = Convert.ToString(dr["invoice_amount"]);

                        DataRow[] drr = dt.Tables[1].Select("Billing_ID='" + Convert.ToString(dr["BillingId"]) + "'");
                        List<Product_List> lstProd = new List<Product_List>();
                        foreach (DataRow item in drr)
                        {
                            Product_List objProd = new Product_List();
                            objProd.brand = Convert.ToString(item["Brand_Id"]);
                            objProd.brand_id = Convert.ToString(item["Brand_Name"]);
                            objProd.category = Convert.ToString(item["ProductClass_Name"]);
                            objProd.category_id = Convert.ToString(item["ProductClass_ID"]);
                            objProd.id = Convert.ToString(item["BillingProd_Id"]);
                            objProd.product_name = Convert.ToString(item["sProducts_Name"]);
                            objProd.qty = Convert.ToString(item["Product_Qty"]);
                            objProd.rate = Convert.ToString(item["Product_Rate"]);
                            objProd.total_price = Convert.ToString(item["Product_TotalAmount"]);
                            objProd.watt = Convert.ToString(item["Size_Name"]);
                            objProd.watt_id = Convert.ToString(item["Size_ID"]);

                            lstProd.Add(objProd);

                        }

                        obj.product_list = lstProd;
                        omedl2.Add(obj);

                    }

                    odata.billing_list = omedl2;
                    odata.status = "200";
                    odata.message = "Collection List Populated Successfully";

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


        public HttpResponseMessage InvoiceListReport(InvoiceListReport_Input model)
        {

            List<InvoiceListReport> oview = new List<InvoiceListReport>();
            InvoiceListReport_Output odata = new InvoiceListReport_Output();

            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
            }
            else
            {
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                string sessionId = "";
                DataSet dt = new DataSet();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("proc_FTS_CollectionList", sqlcon);
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@collection_date", model.date);
                sqlcmd.Parameters.Add("@Action", "InvoiceListReport");
                sqlcmd.Parameters.Add("@weburl", weburl);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dr in dt.Tables[0].Rows)
                    {
                        InvoiceListReport obj = new InvoiceListReport();
                        obj.shop_id = Convert.ToString(dr["shop_id"]);
                        obj.shop_image = Convert.ToString(dr["shop_image"]);
                        obj.shop_name = Convert.ToString(dr["shop_name"]);
                        obj.total_amount = Convert.ToString(dr["total_amount"]);
                        obj.total_bal = Convert.ToString(dr["total_bal"]);
                        obj.total_collection = Convert.ToString(dr["total_collection"]);
                        oview.Add(obj);

                    }

                    odata.amount_list = oview;
                    odata.status = "200";
                    odata.message = "Collection List Populated Successfully";

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

        public HttpResponseMessage CollectionListReport(CollectionListReport_Input model)
        {
            CollectionListReport_Output odata = new CollectionListReport_Output();
            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
            }
            else
            {
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                string sessionId = "";
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("proc_FTS_CollectionList", sqlcon);
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@Action", "CollectionListReport");
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    odata.today_paid = Convert.ToString(dt.Rows[0]["today_paid"]);
                    odata.today_pending = Convert.ToString(dt.Rows[0]["today_pending"]);
                    odata.total_paid = Convert.ToString(dt.Rows[0]["total_paid"]);
                    odata.total_pending = Convert.ToString(dt.Rows[0]["total_pending"]);

                    odata.status = "200";
                    odata.message = "Collection List Populated Successfully";

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
            //return response;
        }

        [HttpPost]
        public HttpResponseMessage PaymentModeList(PaymentMode_Input model)
        {
            PaymentMode odata = new PaymentMode();
            List<PaymentMode_list> oview = new List<PaymentMode_list>();
            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
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
                sqlcmd = new SqlCommand("proc_FTS_PaymentModeList", sqlcon);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt!=null && dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<PaymentMode_list>(dt);
                    odata.paymemt_mode_list = oview;
                    odata.status = "200";
                    odata.message = "Successfully get payment mode list";
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
    }
}

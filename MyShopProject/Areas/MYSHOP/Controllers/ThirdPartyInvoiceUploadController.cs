using BusinessLogicLayer;
using Models;
using MyShop.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml.Linq;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class ThirdPartyInvoiceUploadController : Controller
    {
        //
        // GET: /MYSHOP/ThirdPartyInvoiceUpload/
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult UploadFile(HttpPostedFileBase file)
        {
            try
            {
                string extension = file.ContentType;
                object outputobj = new { isok = true, message = "Try again later." }; 
                if (extension.ToLower() == "text/xml")
                {

                    var xmlPath = Server.MapPath("~/FileUpload" + file.FileName);
                    file.SaveAs(xmlPath);
                    XDocument doc = XDocument.Load(xmlPath);

                    
                    string jsonText = JsonConvert.SerializeXNode(doc);
                    var json = new JavaScriptSerializer().Deserialize<object>(jsonText);

                    var obj = JObject.Parse(jsonText);

                    var objProductDetail = new List<MyShop.Models.ACCOUNTINGALLOCATIONSLIST.ALLINVENTORYENTRIESLIST>();
                    var objHeader = new MyShop.Models.ACCOUNTINGALLOCATIONSLIST.VOUCHER();
                    var objOrder = new INVOICEORDERLISTLIST();
                    for (int i = 0; i < obj["ENVELOPE"]["BODY"]["IMPORTDATA"]["REQUESTDATA"]["TALLYMESSAGE"].Count() - 1; i++)
                    {
                        if (obj["ENVELOPE"]["BODY"]["IMPORTDATA"]["REQUESTDATA"]["TALLYMESSAGE"][i]["VOUCHER"] != null)
                        {
                            var ProductDetail = obj["ENVELOPE"]["BODY"]["IMPORTDATA"]["REQUESTDATA"]["TALLYMESSAGE"][i]["VOUCHER"]["ALLINVENTORYENTRIES.LIST"];
                            var OrderDetail = obj["ENVELOPE"]["BODY"]["IMPORTDATA"]["REQUESTDATA"]["TALLYMESSAGE"][i]["VOUCHER"]["INVOICEORDERLIST.LIST"];
                            var HeaderDetail = obj["ENVELOPE"]["BODY"]["IMPORTDATA"]["REQUESTDATA"]["TALLYMESSAGE"][i]["VOUCHER"];
                            if (ProductDetail.Type == JTokenType.Array)
                            {
                                objProductDetail = ProductDetail.ToObject<List<MyShop.Models.ACCOUNTINGALLOCATIONSLIST.ALLINVENTORYENTRIESLIST>>();
                            }
                            else
                            {
                                var objProductDetails = ProductDetail.ToObject<MyShop.Models.ACCOUNTINGALLOCATIONSLIST.ALLINVENTORYENTRIESLIST>();
                                objProductDetail.Add(objProductDetails);
                            }

                            objHeader = HeaderDetail.ToObject<MyShop.Models.ACCOUNTINGALLOCATIONSLIST.VOUCHER>();
                            objOrder = OrderDetail.ToObject<INVOICEORDERLISTLIST>();
                        }
                        else
                        {
                            goto Final;
                        }







                        //   var json1 = new JavaScriptSerializer().Deserialize<object>(stuff);



                        DBEngine objDb = new DBEngine();
                        DataTable dtOrd = objDb.GetDataTable("select UserID,OrderCode,OrderId from tbl_trans_fts_Orderupdate where OrderCode='" + objOrder.BASICPURCHASEORDERNO + "'");

                        if (dtOrd != null && dtOrd.Rows.Count > 0  )
                        {


                            DataTable dtProdinProcess = objDb.GetDataTable("select * from  FTS_ORDERSTAGE where ORDER_ID='" + objOrder.BASICPURCHASEORDERNO + "' and stage_id=1");


                            if (dtProdinProcess != null && dtProdinProcess.Rows.Count > 0)
                            {
                                string products = "";
                                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                                string sessionId = "";

                                DataTable dt = new DataTable();
                                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];

                                //Add Product in add billing Tanmoy 22-11-2019
                                List<MyShop.Models.ACCOUNTINGALLOCATIONSLIST.ProductLists> omedl2 = new List<MyShop.Models.ACCOUNTINGALLOCATIONSLIST.ProductLists>();

                                bool productvalidation = true;
                                bool ordproductvalidation = true;

                                if (objProductDetail != null)
                                {
                                    foreach (var s2 in objProductDetail)
                                    {


                                        DataTable dtProd = objDb.GetDataTable("select sProducts_ID from master_sproducts where sProducts_Name='" + s2.STOCKITEMNAME + "'");



                                        if (dtProd != null && dtProd.Rows.Count > 0)
                                        {

                                            DataTable dtordProd = objDb.GetDataTable("select * from tbl_FTs_OrderdetailsProduct where Product_Id='" + Convert.ToString(dtProd.Rows[0][0]) + "' and Order_ID='" + Convert.ToString(dtOrd.Rows[0][2]) + "'");


                                            if (dtordProd != null && dtordProd.Rows.Count > 0)
                                            {
                                                string Qty = "0";
                                                string Rate = "0";


                                                //string Qty = new String(s2.BILLEDQTY.Where(Char.IsDigit).ToArray());
                                                //string Rate = new String(s2.RATE.Where(Char.IsDigit).ToArray());


                                                if (!string.IsNullOrEmpty(s2.BILLEDQTY))
                                                    Qty = Convert.ToString(Convert.ToDouble(new String(s2.BILLEDQTY.Where(Char.IsDigit).ToArray())) * .01);
                                                if (!string.IsNullOrEmpty(s2.RATE))
                                                    Rate = Convert.ToString(Convert.ToDouble(new String(s2.RATE.Where(Char.IsDigit).ToArray())) * .01);

                                                omedl2.Add(new MyShop.Models.ACCOUNTINGALLOCATIONSLIST.ProductLists()
                                                {
                                                    id = Convert.ToString(dtProd.Rows[0][0]),
                                                    qty = Convert.ToDecimal(Qty),
                                                    rate = Convert.ToDecimal(Rate),
                                                    total_price = Convert.ToDecimal(s2.AMOUNT),
                                                    product_name = s2.STOCKITEMNAME
                                                });
                                                products = products + s2.STOCKITEMNAME + "," + " Price: " + s2.AMOUNT + "," + " Qty: " + Qty + "||";
                                            }
                                            else
                                            {
                                                ordproductvalidation = false;
                                                break;
                                            }

                                        }
                                        else
                                        {
                                            productvalidation = false;
                                            break;
                                        }


                                    }


                                }








                                if (productvalidation)
                                {
                                    if (ordproductvalidation)
                                    {

                                        string userid = Convert.ToString(dtOrd.Rows[0][0]);
                                        string orderid = Convert.ToString(dtOrd.Rows[0][1]);






                                        string billid = userid + "_bill_" + DateTime.Now.ToString("ddMMyyyyhhmmss");

                                        string date = DateTime.ParseExact(objHeader.DATE, "yyyyMMdd",
                                                            CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

                                        decimal total = objProductDetail.Sum(item => Convert.ToDecimal(item.AMOUNT));

                                        string JsonXML = MyShop.Models.ACCOUNTINGALLOCATIONSLIST.XmlConversion.ConvertToXml(omedl2, 0);
                                        //End Add Product in add billing Tanmoy 22-11-2019

                                        SqlCommand sqlcmd = new SqlCommand();
                                        SqlConnection sqlcon = new SqlConnection(con);
                                        sqlcon.Open();

                                        sqlcmd = new SqlCommand("Proc_FTS_BillingManagement", sqlcon);
                                        sqlcmd.Parameters.Add("@user_id", userid);
                                        sqlcmd.Parameters.Add("@bill_id", billid);
                                        sqlcmd.Parameters.Add("@invoice_no", objHeader.REFERENCE);
                                        sqlcmd.Parameters.Add("@invoice_date", date);
                                        sqlcmd.Parameters.Add("@invoice_amount", total);
                                        sqlcmd.Parameters.Add("@remarks", "From Tally");
                                        sqlcmd.Parameters.Add("@order_id", orderid);
                                        //Add Product in add billing Tanmoy 22-11-2019
                                        sqlcmd.Parameters.Add("@Product_List", JsonXML);
                                        //End Add Product in add billing Tanmoy 22-11-2019
                                        sqlcmd.Parameters.Add("@Action", "Insert");

                                        sqlcmd.CommandType = CommandType.StoredProcedure;
                                        SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                                        da.Fill(dt);
                                        sqlcon.Close();

                                        DataTable dtordProd = objDb.GetDataTable("update FTS_ORDERSTAGE set STAGE_ID=4 where ORDER_ID='" + objOrder.BASICPURCHASEORDERNO + "'");




                                        outputobj = new { isok = true, message = "Invoice updated succsessfully." };


                                    }
                                    else
                                    {
                                        outputobj = new { isok = true, message = "Product Not found in Order can not proceed." };
                                        break;
                                    }



                                }
                                else
                                {
                                    outputobj = new { isok = true, message = "Product Not found." };
                                    break;
                                }
                            }

                            else
                            {
                                outputobj = new { isok = true, message = "Selected Orders are not 'In Process'. Cannot Import." };
                                break;
                            }
                        }
                        else
                        {
                            outputobj = new { isok = true, message = "Order info not found." };
                            break;
                        }
                    }
                }
                else
                {
                    outputobj = new { isok = true, message = "Only XML file allowed." };
                }
                Final:
                return Json(outputobj, JsonRequestBehavior.AllowGet);
            }




            catch (Exception e)
            {

                return Json(new { isok = true, message = e }, JsonRequestBehavior.AllowGet);
            }
        }







    }
}
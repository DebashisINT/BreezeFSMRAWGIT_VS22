/*******************************************************************************************************
 * Written by Priti for V2.0.39 on 08/03/2023 - Implement Open API for Product Master 
 * 1.0  V2.0.40  Priti   20-04-2023     0025872:Create Open API For Product Master
 ********************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OpenAPI.Models;
using System.IO;

// POSTMAN HIT CREDENTIALS
//https://localhost:44395/API/ProductMasterAPI/GetProductDetailsBySearchKey

//HEADERS
//account-id : 01360645-267C-4690-ABC5-A3D66B300B8731331/55454854
//api-key : 01360645-267C-4690-ABC5-A3D66B300B87GVDBHFJNFGJMUJHIJIFGG

//http://3.7.30.86:8074/API/ProductMasterAPI/GetProductDetailsBySearchKey
//BODY
//{
//    session_token: "",
//    SearchKey: "a",
//    Uniquecont: "5"
//}
//RAW - JSON
//
//http://3.7.30.86:8074/API/ProductMasterAPI/InsertProductMaster
//BODY
//{
//    "user_id":"378",
//    "session_token":"",
//    "ProductInput_list":[
//    {
//        "ProductCode" :"PRO123",
//        "ProductName" :"PRO123",
//        "Description" :"PRO123",        
//        "SalesUnit" :"bag",
//        "PurchaseUnit" :"BAG",
//        "MRP" :123.0,
//        "Discount" :123.0
//    } ,
//    {
//        "ProductCode" :"TEST12378",
//        "ProductName" :"123",
//        "Description" :"123",        
//        "SalesUnit" :"Australian Dollar",
//        "PurchaseUnit" :"BAG",
//        "MRP" :123.0,
//        "Discount" :123.0


//    } 

//    ]   
//}

namespace OpenAPI.Controllers
{
    public class ProductMasterAPIController : ApiController
    {

        //[System.Web.Http.HttpPost]
        //public HttpResponseMessage GetProductDetails(ProductDetailsInput model)
        //{
        //    ProductDetailsOutput omodeloutput = new ProductDetailsOutput();
        //    try
        //    {
        //        String OpenAPIKey = "";
        //        String OpenAPIAccountId = "";
        //        DataTable ProductMasterHeader = GetHeaderCredentials();
        //        if (ProductMasterHeader.Rows.Count > 0)
        //        {
        //            OpenAPIKey=Convert.ToString(ProductMasterHeader.Rows[0]["OPENAPI_KEY"]);
        //            OpenAPIAccountId=Convert.ToString(ProductMasterHeader.Rows[0]["OPENAPI_ACCOUNTID"]);                    
        //        }
        //        var APIReq = Request;
        //        var APIheaders = APIReq.Headers;
        //        string APIAccountid = "";
        //        string APIKey = "";

        //        if (APIheaders.Contains("account-id"))
        //        {
        //            APIAccountid = APIheaders.GetValues("account-id").First();
        //        }
        //        if (APIheaders.Contains("api-key"))
        //        {
        //            APIKey = APIheaders.GetValues("api-key").First();
        //        }

        //        if (APIKey == "" || APIAccountid == "")
        //        {
        //            omodeloutput.status = "201";
        //            omodeloutput.message = "No API key found in request.";
        //            return Request.CreateResponse(HttpStatusCode.BadRequest, omodeloutput);
        //        }
        //        else if (APIKey != OpenAPIKey || APIAccountid != OpenAPIAccountId)
        //        {
        //            omodeloutput.status = "202";
        //            omodeloutput.message = "Invalid authentication credentials.";
        //            return Request.CreateResponse(HttpStatusCode.BadRequest, omodeloutput);
        //        }
        //        else if (APIKey == OpenAPIKey && APIAccountid == OpenAPIAccountId)
        //        {
        //            if (!ModelState.IsValid)
        //            {
        //                omodeloutput.status = "213";
        //                omodeloutput.message = "Some input parameters are missing.";
        //                return Request.CreateResponse(HttpStatusCode.BadRequest, omodeloutput);
        //            }
        //            else
        //            {
        //                DataSet ds = GetProductDetailsData(model.session_token, model.ProductCode);
        //                DataTable Header = ds.Tables[0];
        //                if (Header.Rows.Count > 0)
        //                {
        //                    omodeloutput.ProductsCode = Convert.ToString(Header.Rows[0]["ProductsCode"]);
        //                    omodeloutput.ProductsName = Convert.ToString(Header.Rows[0]["ProductsName"]);
        //                    omodeloutput.ProductsDescription = Convert.ToString(Header.Rows[0]["ProductsDescription"]);
        //                    omodeloutput.STKUOMNAME = Convert.ToString(Header.Rows[0]["STKUOMNAME"]);
        //                    omodeloutput.SALESUOMNAME = Convert.ToString(Header.Rows[0]["SALESUOMNAME"]);
        //                    omodeloutput.CLASSCODE = Convert.ToString(Header.Rows[0]["CLASSCODE"]);
        //                    omodeloutput.BRANDNAME = Convert.ToString(Header.Rows[0]["BRANDNAME"]);
        //                    omodeloutput.status = "200";
        //                    omodeloutput.message = "Product Details";


        //                }
        //                else
        //                {

        //                    omodeloutput.status = "205";
        //                    omodeloutput.message = "No data found";

        //                }
        //            }
        //        }
        //    }
        //    catch { }
        //    var message = Request.CreateResponse(HttpStatusCode.OK, omodeloutput);
        //    return message;
        //}

        [System.Web.Http.HttpPost]
        public HttpResponseMessage GetProductDetailsBySearchKey(ProductInput model)
        {
            ProductslistOutput omodeloutput = new ProductslistOutput();
            List<Productslists> oview = new List<Productslists>();

            Datalists odata = new Datalists();

            String OpenAPIKey = "";
            String OpenAPIAccountId = "";
            DataTable ProductMasterHeader = GetHeaderCredentials();
            if (ProductMasterHeader.Rows.Count > 0)
            {
                OpenAPIKey = Convert.ToString(ProductMasterHeader.Rows[0]["OPENAPI_KEY"]);
                OpenAPIAccountId = Convert.ToString(ProductMasterHeader.Rows[0]["OPENAPI_ACCOUNTID"]);
            }
            var APIReq = Request;
            var APIheaders = APIReq.Headers;

            string APIAccountid = "";
            string APIKey = "";

            if (APIheaders.Contains("account-id"))
            {
                APIAccountid = APIheaders.GetValues("account-id").First();
            }
            if (APIheaders.Contains("api-key"))
            {
                APIKey = APIheaders.GetValues("api-key").First();
            }

            if (APIKey == "" || APIAccountid == "")
            {
                omodeloutput.status = "201";
                omodeloutput.message = "No API key found in request.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodeloutput);
            }
            else if (APIKey != OpenAPIKey || APIAccountid != OpenAPIAccountId)
            {
                omodeloutput.status = "202";
                omodeloutput.message = "Invalid authentication credentials.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodeloutput);
            }
            else if (APIKey == OpenAPIKey && APIAccountid == OpenAPIAccountId)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        omodeloutput.status = "213";
                        omodeloutput.message = "Some input parameters are missing.";
                        return Request.CreateResponse(HttpStatusCode.BadRequest, omodeloutput);
                    }
                    else
                    {
                        DataSet ds = GetProductsList(model.session_token, model.SearchKey, Convert.ToInt32(model.Uniquecont));
                        DataTable Header = ds.Tables[0];
                        if (Header.Rows.Count > 0)
                        {
                            oview = APIHelperMethods.ToModelList<Productslists>(ds.Tables[0]);
                            odata.product_list = oview;
                            omodeloutput.status = "200";
                            omodeloutput.message = Header.Rows.Count.ToString() + "No. of Product Details available. ";
                            omodeloutput.data = odata;
                        }
                        else
                        {
                            omodeloutput.status = "205";
                            omodeloutput.message = "No data found";
                        }
                    }
                }
                catch { }

            }
            var message = Request.CreateResponse(HttpStatusCode.OK, omodeloutput);
            return message;
        }

        private static DataSet GetProductDetailsData(string session_token, string ProductCode)
        {
            DataSet ds = new DataSet();

            String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
            SqlCommand sqlcmd = new SqlCommand();
            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();
            sqlcmd = new SqlCommand("PRC_OPENAPI_PRODUCTDETAILS", sqlcon);
            sqlcmd.Parameters.AddWithValue("@ACTION", "GetHeaderProductDetails");
            sqlcmd.Parameters.AddWithValue("@session_token", session_token);
            sqlcmd.Parameters.AddWithValue("@ProductCode", ProductCode);

            sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(ds);
            sqlcon.Close();
            return ds;
        }
        private static DataSet GetProductsList(string session_token, string SearchKey, Int32 Uniquecont)
        {
            DataSet ds = new DataSet();

            String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
            SqlCommand sqlcmd = new SqlCommand();
            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();
            sqlcmd = new SqlCommand("PRC_OPENAPI_PRODUCTDETAILS", sqlcon);
            sqlcmd.Parameters.AddWithValue("@ACTION", "GetProductDetails");
            sqlcmd.Parameters.AddWithValue("@session_token", session_token);
            sqlcmd.Parameters.AddWithValue("@SearchKey", SearchKey);
            sqlcmd.Parameters.AddWithValue("@Uniquecont", Uniquecont);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(ds);
            sqlcon.Close();
            return ds;
        }
        private static DataTable GetHeaderCredentials()
        {
            DataTable ds = new DataTable();

            String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
            SqlCommand sqlcmd = new SqlCommand();
            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();
            sqlcmd = new SqlCommand("PRC_OPENAPI_PRODUCTDETAILS", sqlcon);
            sqlcmd.Parameters.AddWithValue("@ACTION", "GetProductMasterHeaderCredentials");

            sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(ds);
            sqlcon.Close();
            return ds;
        }


        [System.Web.Http.HttpPost]
        public HttpResponseMessage InsertProductMaster(InsertProductInput model)
        {
            ProductslistOutput omodeloutput = new ProductslistOutput();
            String OpenAPIKey = "";
            String OpenAPIAccountId = "";
            DataTable ProductMasterHeader = GetHeaderCredentials();
            if (ProductMasterHeader.Rows.Count > 0)
            {
                OpenAPIKey = Convert.ToString(ProductMasterHeader.Rows[0]["OPENAPI_KEY"]);
                OpenAPIAccountId = Convert.ToString(ProductMasterHeader.Rows[0]["OPENAPI_ACCOUNTID"]);
            }
            var APIReq = Request;
            var APIheaders = APIReq.Headers;

            string APIAccountid = "";
            string APIKey = "";

            if (APIheaders.Contains("account-id"))
            {
                APIAccountid = APIheaders.GetValues("account-id").First();
            }
            if (APIheaders.Contains("api-key"))
            {
                APIKey = APIheaders.GetValues("api-key").First();
            }

            if (APIKey == "" || APIAccountid == "")
            {
                omodeloutput.status = "201";
                omodeloutput.message = "No API key found in request.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodeloutput);
            }
            else if (APIKey != OpenAPIKey || APIAccountid != OpenAPIAccountId)
            {
                omodeloutput.status = "202";
                omodeloutput.message = "Invalid authentication credentials.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodeloutput);
            }
            else if (APIKey == OpenAPIKey && APIAccountid == OpenAPIAccountId)
            {
                
                    if (!ModelState.IsValid)
                    {
                        omodeloutput.status = "213";
                        omodeloutput.message = "Some input parameters are missing.";
                        return Request.CreateResponse(HttpStatusCode.BadRequest, omodeloutput);
                    }
                    else
                    {
                    List<InsertProductInputModel> omedl2 = new List<InsertProductInputModel>();
                    foreach (var s2 in model.ProductInput_list)
                    {
                        omedl2.Add(new InsertProductInputModel()
                        {
                            ProductCode = s2.ProductCode,
                            ProductName = s2.ProductName,
                            Description = s2.Description,
                            SalesUnit = s2.SalesUnit,
                            PurchaseUnit = s2.PurchaseUnit,
                            MRP = s2.MRP,
                            Discount = s2.Discount
                        });
                    }
                    string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);
                    String dates = DateTime.Now.ToString("ddMMyyyyhhmmssffffff");
                        DataTable dt = new DataTable();
                        try
                        {
                            String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                            SqlCommand sqlcmd = new SqlCommand();
                            SqlConnection sqlcon = new SqlConnection(con);
                            sqlcmd.CommandTimeout = 60;
                            sqlcon.Open();
                            sqlcmd = new SqlCommand("PRC_OPENAPI_PRODUCTDETAILS", sqlcon);
                            sqlcmd.Parameters.AddWithValue("@ACTION", "InsertProductDetails");
                            sqlcmd.Parameters.AddWithValue("@session_token", model.session_token);
                            sqlcmd.Parameters.AddWithValue("@user_id", model.user_id.ToString());
                            sqlcmd.Parameters.AddWithValue("@JsonXML", JsonXML);
                            
                            sqlcmd.CommandType = CommandType.StoredProcedure;
                            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                            da.Fill(dt);
                            sqlcon.Close();
                            if (dt.Rows.Count > 0 && Convert.ToString(dt.Rows[0]["STRMESSAGE"]) == "Success")
                            {
                                omodeloutput.status = "200";
                                omodeloutput.message = "Successfully Product Inserted.";
                            }
                            else
                            {
                                omodeloutput.status = "202";
                                omodeloutput.message = "Records not updated.";
                            }
                    }
                        catch
                        {

                        }
                        finally
                        {

                        }
                    }
                }
                
            var message = Request.CreateResponse(HttpStatusCode.OK, omodeloutput);
            return message; 
        }
    }
}

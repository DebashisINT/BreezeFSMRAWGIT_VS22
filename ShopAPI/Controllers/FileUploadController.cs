#region======================================Revision History=========================================================
//1.0   V2.0.48     Debashis    31/07/2024      A new method has been added.Row: 957
//2.0   V2.0.49     Debashis    17/09/2024      A new parameter has been added.Row: 977
#endregion===================================End of Revision History==================================================

using Newtonsoft.Json.Linq;
using ShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ShopAPI.Controllers
{
    public class FileUploadController : Controller
    {
        //
        // GET: /FileUpload/
        [AcceptVerbs("POST")]
        public JsonResult UploadOutletImages(HttpPostedFileBase imageData, string imagePath)
        {
            //EventLogEL eventlog;
            //LogBL log = new LogBL();
            //FileUploadEL result = new FileUploadEL();
            //string IsServerPath = Convert.ToString(ConfigurationManager.AppSettings["IsServerPath"]);
            //string UploadFileDirectory = Convert.ToString(ConfigurationManager.AppSettings["UploadImagePathDirectory"]);
            //if (imageData == null || imageData.ContentLength <= 0)
            //{
            //    result.IsSuccess = false;
            //    result.responseCode = "201";
            //    result.responseMessage = "File is required!";
            //    eventlog = new EventLogEL() {
            //        DateTime = DateTime.Now.ToString(),
            //        EventName = "UploadOutletImages",
            //        ResultMessage = result.responseMessage, 
            //        EventDecription = "Parameters(imageData=null, imagePath=" + imagePath + ")",
            //        OtherInfo = "Please select a file before upload.",
            //    };
            //    log.MaintainLog(eventlog);
            //    return Json(result);
            //}
            //else
            //{
            //    if (imageData.ContentLength > 5242880)
            //    {
            //        result.IsSuccess = false;
            //        result.responseCode = "201";
            //        result.responseMessage = "File size must be less than 5 mb!";
            //        eventlog = new EventLogEL()
            //        {
            //            DateTime = DateTime.Now.ToString(),
            //            EventName = "UploadOutletImages",
            //            ResultMessage = result.responseMessage,
            //            ImageName = imageData.FileName,
            //            EventDecription = "Parameters(imageData=" + imageData.FileName + ", imagePath=" + imagePath + ")",
            //            OtherInfo = "Please compress the file.",
            //        };
            //        log.MaintainLog(eventlog);
            //        return Json(result);
            //    }
            //}  
            //if (string.IsNullOrWhiteSpace(imagePath))
            //{
            //    result.IsSuccess = false;
            //    result.responseCode = "201";
            //    result.responseMessage = "File name with proper path is required!";
            //    eventlog = new EventLogEL()
            //    {
            //        DateTime = DateTime.Now.ToString(),
            //        EventName = "UploadOutletImages",
            //        ResultMessage = result.responseMessage,
            //        ImageName = imageData.FileName,
            //        EventDecription = "Parameters(imageData=" + imageData.FileName + ", imagePath=" + imagePath + ")",
            //        OtherInfo = "Please provide a proper path with image name i.e(/<ROOT FOLDER>/<SUB FOLDER>/<IMAGE NAME>.<EXTENSION NAME>).",
            //    };
            //    log.MaintainLog(eventlog);
            //    return Json(result);
            //}  
            //try
            //{
            //    string[] ImageContainArry = imagePath.Split('/');
            //    string RootDirectory = string.Empty;
            //    string SubFolder = string.Empty;
            //    string ImageName = string.Empty; 
            //    if (ImageContainArry.Count() > 0)
            //    {
            //        RootDirectory = ImageContainArry[0];
            //    }
            //    if (ImageContainArry.Count() > 1)
            //    {
            //        SubFolder = ImageContainArry[1];
            //    }
            //    if (ImageContainArry.Count() > 2)
            //    {
            //        ImageName = ImageContainArry[2];
            //    } 
            //    if(IsServerPath == "no")
            //    {
            //        if (!System.IO.Directory.Exists(Path.Combine(UploadFileDirectory + "/" + RootDirectory + "/" + SubFolder)))
            //        {
            //            System.IO.Directory.CreateDirectory(UploadFileDirectory + "/" + RootDirectory + "/" + SubFolder);
            //            string vPath = Path.Combine(UploadFileDirectory + "/" + RootDirectory + "/" + SubFolder, ImageName);
            //            imageData.SaveAs(vPath);

            //            result.IsSuccess = true;
            //            result.responseMessage = "Success";
            //            result.responseCode = "200";
            //            result.imagePath = imagePath; 
            //            return Json(result);
            //        }
            //        else
            //        {
            //            if(!System.IO.File.Exists(Path.Combine(UploadFileDirectory + "/" + RootDirectory + "/" + SubFolder, ImageName)))
            //            {
            //                string vPath = Path.Combine(UploadFileDirectory + "/" + RootDirectory + "/" + SubFolder, ImageName);
            //                imageData.SaveAs(vPath);

            //                result.IsSuccess = true;
            //                result.responseMessage = "Success";
            //                result.responseCode = "200";
            //                result.imagePath = imagePath; 
            //                return Json(result);
            //            }
            //            else
            //            {
            //                result.IsSuccess = true;
            //                result.responseMessage = "File already exist.";
            //                result.responseCode = "201";
            //                result.imagePath = imagePath;
            //                eventlog = new EventLogEL()
            //                {
            //                    DateTime = DateTime.Now.ToString(),
            //                    EventName = "UploadOutletImages",
            //                    ResultMessage = result.responseMessage,
            //                    ImageName = imageData.FileName,
            //                    EventDecription = "Parameters(imageData=" + imageData.FileName + ", imagePath=" + imagePath + ")",
            //                    OtherInfo = "File already exist.",
            //                };
            //                log.MaintainLog(eventlog);
            //                return Json(result);
            //            } 
            //        }
            //    }
            //    else
            //    {
            //        if (!System.IO.Directory.Exists(Path.Combine(Server.MapPath("/" + RootDirectory + "/" + SubFolder))))
            //        {
            //            System.IO.Directory.CreateDirectory(Server.MapPath("/" + RootDirectory + "/" + SubFolder));
            //            string vPath = Path.Combine(Server.MapPath("/" + RootDirectory + "/" + SubFolder), ImageName);
            //            imageData.SaveAs(vPath);

            //            result.IsSuccess = true;
            //            result.responseMessage = "Success";
            //            result.responseCode = "200";
            //            result.imagePath = imagePath; 
            //            return Json(result);
            //        }
            //        else
            //        {
            //            if (!System.IO.File.Exists(Path.Combine(Server.MapPath("/" + RootDirectory + "/" + SubFolder), ImageName)))
            //            {
            //                string vPath = Path.Combine(Server.MapPath("/" + RootDirectory + "/" + SubFolder), ImageName);
            //                imageData.SaveAs(vPath);

            //                result.IsSuccess = true;
            //                result.responseMessage = "Success";
            //                result.responseCode = "200";
            //                result.imagePath = imagePath; 
            //                return Json(result);
            //            }
            //            else
            //            {
            //                result.IsSuccess = true;
            //                result.responseMessage = "File already exist.";
            //                result.responseCode = "201";
            //                result.imagePath = imagePath;
            //                eventlog = new EventLogEL()
            //                {
            //                    DateTime = DateTime.Now.ToString(),
            //                    EventName = "UploadOutletImages",
            //                    ResultMessage = result.responseMessage,
            //                    ImageName = imageData.FileName,
            //                    EventDecription = "Parameters(imageData=" + imageData.FileName + ", imagePath=" + imagePath + ")",
            //                    OtherInfo = "File already exist.",
            //                };
            //                log.MaintainLog(eventlog);
            //                return Json(result);
            //            } 
            //        }
            //    }
                  
            //}
            //catch (Exception ex)
            //{
            //    string MoreInfo = string.Empty;
            //    result.IsSuccess = false;
            //    result.responseCode = "201";
            //    result.responseMessage = "Failed because: " + ex.Message;
            //    if (ex.InnerException != null)
            //    {
            //        MoreInfo = ex.InnerException.Message;
            //    }
            //    eventlog = new EventLogEL()
            //    {
            //        DateTime = DateTime.Now.ToString(),
            //        EventName = "UploadOutletImages",
            //        ResultMessage = ex.Message,
            //        ImageName = imageData.FileName,
            //        EventDecription = "Parameters(imageData=" + imageData.FileName + ", imagePath=" + imagePath + ")", 
            //        OtherInfo = MoreInfo,
            //    };
            //    log.MaintainLog(eventlog);
            //    return Json(result);
            //}5
            return Json(null);
        }

        public JsonResult UploadAudioforShop(AudioUpload model)
        {
            RevisitAudioOutput omodel = new RevisitAudioOutput();
            string AudioName = "";
            ShopRegister oview = new ShopRegister();
            AudioName = model.audio.FileName;
            string UploadFileDirectory = "~/CommonFolder/Shoprevisit";
            try
            {
                var details = JObject.Parse(model.data);
                var hhhh = Newtonsoft.Json.JsonConvert.DeserializeObject<RevisitAudioInput>(model.data);
                if (!string.IsNullOrEmpty(model.data))
                {
                    AudioName = hhhh.session_token + '_' + hhhh.user_id + '_' + AudioName;
                    string vPath = Path.Combine(Server.MapPath("~/CommonFolder/Shoprevisit"), AudioName);
                    model.audio.SaveAs(vPath);
                }
                string sessionId = "";

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();

                sqlcmd = new SqlCommand("Proc_ApiShopRevisitAudioUpload", sqlcon);
                sqlcmd.Parameters.Add("@user_id", hhhh.user_id);
                sqlcmd.Parameters.Add("@shopvisit_Audio", AudioName);
                sqlcmd.Parameters.Add("@shop_id", hhhh.shop_id);
                sqlcmd.Parameters.Add("@visit_date", hhhh.visit_datetime);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Success";
                }
                else
                {
                    omodel.status = "202";
                    omodel.message = "Already Data was inserted";
                }
            }
            catch (Exception msg)
            {
                omodel.status = "204" + AudioName;
                omodel.message = msg.Message;
            }
            return Json(omodel);
        }

        public JsonResult OrderSignature(OrderSignatureUpload model)
        {
            OrderSignatureOutput omodeloutput = new OrderSignatureOutput();
            string signatureName = "";
            string products = "";
            ShopRegister oview = new ShopRegister();
            signatureName = model.signature.FileName;
            string UploadFileDirectory = "~/CommonFolder/OrderSignature";
            try
            {
                var details = JObject.Parse(model.data);
                var hhhh = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderSignatureInput>(model.data);
                if (!string.IsNullOrEmpty(model.data))
                {
                    signatureName = hhhh.session_token + '_' + hhhh.user_id + '_' + signatureName;
                    string vPath = Path.Combine(Server.MapPath("~/CommonFolder/OrderSignature"), signatureName);
                    model.signature.SaveAs(vPath);
                }
                string sessionId = "";

                List<OrderSignaturelist> omedl2 = new List<OrderSignaturelist>();

                foreach (var s2 in hhhh.product_list)
                {
                    omedl2.Add(new OrderSignaturelist()
                    {
                        id = s2.id,
                        qty = s2.qty,
                        rate = s2.rate,
                        total_price = s2.total_price,
                        product_name = s2.product_name,
                        //Extra Input for EuroBond
                        MRP=s2.MRP
                        //Extra Input for EuroBond
                    });
                    products = products + s2.product_name + "," + " Price: " + s2.total_price + "," + " Qty: " + s2.qty + "||";
                }


                string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();

                sqlcmd = new SqlCommand("Proc_FTS_Order", sqlcon);
                sqlcmd.Parameters.AddWithValue("@user_id", hhhh.user_id);
                sqlcmd.Parameters.AddWithValue("@SessionToken", hhhh.session_token);
                sqlcmd.Parameters.AddWithValue("@order_amount", hhhh.order_amount);
                sqlcmd.Parameters.AddWithValue("@order_id", hhhh.order_id);
                sqlcmd.Parameters.AddWithValue("@Shop_Id", hhhh.shop_id);
                sqlcmd.Parameters.AddWithValue("@description", hhhh.description);
                sqlcmd.Parameters.AddWithValue("@Collection", hhhh.collection);
                sqlcmd.Parameters.AddWithValue("@order_date", hhhh.order_date);
                sqlcmd.Parameters.AddWithValue("@Remarks", hhhh.remarks);
                sqlcmd.Parameters.AddWithValue("@signatureName", signatureName);
                sqlcmd.Parameters.AddWithValue("@Lat", hhhh.latitude);
                sqlcmd.Parameters.AddWithValue("@Long", hhhh.longitude);
                sqlcmd.Parameters.AddWithValue("@Order_Address", hhhh.address);
                //Extra Input for 4Basecare
                sqlcmd.Parameters.AddWithValue("@patient_no", hhhh.patient_no);
                sqlcmd.Parameters.AddWithValue("@patient_name", hhhh.patient_name);
                sqlcmd.Parameters.AddWithValue("@patient_address", hhhh.patient_address);
                //Extra Input for 4Basecare
                //Extra Input for EuroBond
                sqlcmd.Parameters.AddWithValue("@Hospital ", hhhh.Hospital);
                sqlcmd.Parameters.AddWithValue("@Email_Address ", hhhh.Email_Address);
                //Extra Input for EuroBond
                //Rev 2.0 Row: 907
                sqlcmd.Parameters.AddWithValue("@OrderStatus ", hhhh.OrderStatus);
                //End of Rev 2.0 Row: 907
                sqlcmd.Parameters.AddWithValue("@Product_List", JsonXML);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodeloutput.status = "200";
                    omodeloutput.message = "Successfully add order";
                }
                else
                {
                    omodeloutput.status = "202";
                    omodeloutput.message = "Already Data was inserted";
                }
            }
            catch (Exception msg)
            {
                omodeloutput.status = "204" + signatureName;
                omodeloutput.message = msg.Message;
            }
            return Json(omodeloutput);
        }

        //Rev 1.0 Row: 957 & 965
        public JsonResult UploadShopRevisitAudio(UploadShopRevisitAudioInput model)
        {
            UploadRevisitAudioOutput omodel = new UploadRevisitAudioOutput();
            string AudioName = "";
            ShopRegister oview = new ShopRegister();
            AudioName = model.audio.FileName;
            try
            {
                var details = JObject.Parse(model.data);
                var hhhh = Newtonsoft.Json.JsonConvert.DeserializeObject<UploadRevisitAudio>(model.data);
                if (!string.IsNullOrEmpty(model.data))
                {
                    AudioName = hhhh.session_token + '_' + hhhh.user_id + '_' + AudioName;
                    string vPath = Path.Combine(Server.MapPath("~/CommonFolder/ShopRevisitAudio"), AudioName);
                    model.audio.SaveAs(vPath);
                }

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();

                sqlcmd = new SqlCommand("PRC_APISHOPREVISITAUDIOINFO", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "SHOPAUDIOSAVE");
                sqlcmd.Parameters.AddWithValue("@USER_ID", hhhh.user_id);
                sqlcmd.Parameters.AddWithValue("@SHOP_ID", hhhh.shop_id);
                sqlcmd.Parameters.AddWithValue("@VISIT_DATETIME", hhhh.visit_datetime);
                sqlcmd.Parameters.AddWithValue("@REVISITORVISIT", hhhh.revisitORvisit);
                sqlcmd.Parameters.AddWithValue("@SHOPVISIT_AUDIO", AudioName);
                sqlcmd.Parameters.AddWithValue("@AUDIOPATH", "/CommonFolder/ShopRevisitAudio/" + AudioName);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Success.";
                }
                else
                {
                    omodel.status = "202";
                    omodel.message = "Already Data found.";
                }
            }
            catch (Exception msg)
            {
                omodel.status = "204" + AudioName;
                omodel.message = msg.Message;
            }
            return Json(omodel);
        }        
        //End of Rev 1.0 Row: 957 & 965
    }
}
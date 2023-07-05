#region======================================Revision History=========================================================
//Written By : Debashis Talukder On 01/06/2023
//Purpose : Save QR Code Image.
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
using System.Web;
using System.Web.Http;
namespace ShopAPI.Controllers
{
    public class QRCodeImageFetchDeleteController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage FetchQRCodeImageLink(FetchQRCodeImageInput model)
        {
            FetchQRCodeImageOutput omodel = new FetchQRCodeImageOutput();
            String APIHostingPort = System.Configuration.ConfigurationManager.AppSettings["APIHostingPort"];
            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_FTSAPIQRCODEIMAGEINFO", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "FETCHQRCODEIMAGE");
                sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                sqlcmd.Parameters.AddWithValue("@BaseURL", APIHostingPort);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Successfully get Image link.";
                    omodel.qr_img_link = APIHostingPort + Convert.ToString(dt.Rows[0]["qr_img_link"]);
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
        public HttpResponseMessage DeleteQRCodeImageLink(DeleteQRCodeImageInput model)
        {
            DeleteQRCodeImageOutput omodel = new DeleteQRCodeImageOutput();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                String token = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_FTSAPIQRCODEIMAGEINFO", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "DELETEQRCODEIMAGE");
                sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "QR Code Image Deleted Successfully.";

                    string vQRCodeImagePath = Path.Combine(HttpContext.Current.Server.MapPath(Convert.ToString(dt.Rows[0]["qr_img_link"])));
                    System.IO.File.Delete(vQRCodeImagePath);
                }
                else
                {
                    omodel.status = "205";
                    omodel.message = "No data to delete";
                }

                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }
    }
}

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
    public class DEletematerialImageController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Delete(Materialdelete model)
        {
            Materialdeleteoutput omodel = new Materialdeleteoutput();
        
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
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("Sp_API_FTSdeleteMaterial", sqlcon);

                sqlcmd.Parameters.Add("@shop_id", model.shop_id);
                sqlcmd.Parameters.Add("@image_id", model.image_id);
                sqlcmd.Parameters.Add("@user_id", model.user_id);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();

              
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Material Image Deleted Successfully.";

                    omodel.materialimage = Convert.ToString(dt.Rows[0]["materialimage"]);
                    string fileLocation = Path.Combine(HttpContext.Current.Server.MapPath("~/CommonFolder/MaterialImage"), omodel.materialimage);
                    if (System.IO.File.Exists(fileLocation))
                    {
                        System.IO.File.Delete(fileLocation);
                    }

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

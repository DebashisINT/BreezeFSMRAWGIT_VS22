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
    public class FaceRegistrationController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage UserList(UserListInputModel model)
        {
            UserListOutputModel omodel = new UserListOutputModel();
            //Rev Debashis
            String APIHostingPort = System.Configuration.ConfigurationManager.AppSettings["APIHostingPort"];
            //End of Rev Debashis
            List<userDetails> oview = new List<userDetails>();
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
                sqlcmd = new SqlCommand("PRC_FTSAPI_USERLISTSHOPMAP", sqlcon);
                sqlcmd.Parameters.AddWithValue("@Action", "UserList");
                sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                sqlcmd.Parameters.AddWithValue("@BaseURL", APIHostingPort);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<userDetails>(dt);
                    omodel.user_list = oview;
                    omodel.status = "200";
                    omodel.message = "Successfully get user list.";
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

        //Rev Debashis && 19-07-2021
        [HttpPost]
        public HttpResponseMessage FaceMatch(FaceMatchingInputModel model)
        {
            FaceMatchingOutputModel omodel = new FaceMatchingOutputModel();
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
                sqlcmd = new SqlCommand("PRC_FTSAPI_USERLISTSHOPMAP", sqlcon);
                sqlcmd.Parameters.AddWithValue("@Action", "FaceMatch");
                sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                sqlcmd.Parameters.AddWithValue("@BaseURL", APIHostingPort);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.user_id = Convert.ToString(dt.Rows[0]["user_id"]);
                    omodel.status = "200";
                    omodel.message = "Face Photo Success.";
                    omodel.face_image_link = APIHostingPort + Convert.ToString(dt.Rows[0]["face_image_link"]);
                }
                else
                {
                    omodel.status = "205";
                    omodel.message = "No Match Found.";
                }
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }
        //End of Rev Debashis && 19-07-2021
        //Rev Debashis && 20-07-2021
        //[HttpPost]
        //public HttpResponseMessage FaceImgDelete(FaceImageDeleteInputModel model)
        //{
        //    FaceImageDeleteOutputModel omodel = new FaceImageDeleteOutputModel();
        //    String APIHostingPort = System.Configuration.ConfigurationSettings.AppSettings["APIHostingPort"];
        //    if (!ModelState.IsValid)
        //    {
        //        omodel.status = "213";
        //        omodel.message = "Some input parameters are missing.";
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
        //    }
        //    else
        //    {
        //        DataTable dt = new DataTable();
        //        String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
        //        SqlCommand sqlcmd = new SqlCommand();
        //        SqlConnection sqlcon = new SqlConnection(con);
        //        sqlcon.Open();
        //        sqlcmd = new SqlCommand("PRC_FTSAPI_USERLISTSHOPMAP", sqlcon);
        //        sqlcmd.Parameters.Add("@Action", "FaceImgDel");
        //        sqlcmd.Parameters.Add("@USER_ID", model.user_id);
        //        sqlcmd.CommandType = CommandType.StoredProcedure;
        //        SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
        //        da.Fill(dt);
        //        sqlcon.Close();
        //        if (dt.Rows.Count > 0)
        //        {
        //            omodel.user_id = Convert.ToString(dt.Rows[0]["user_id"]);
        //            omodel.status = "200";
        //            omodel.message = "Face Photo Delete Success.";
        //            //String path = APIHostingPort + Convert.ToString(dt.Rows[0]["face_image_link"]);
        //            //System.IO.File.Delete(path);

        //            string vPath = Path.Combine(Server.MapPath("~/CommonFolder/FaceImageDetection"), ImageName);
        //            System.IO.File.Delete(vPath);     
        //        }
        //        else
        //        {
        //            omodel.status = "205";
        //            omodel.message = "No Match Found.";
        //        }
        //        var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
        //        return message;
        //    }
        //}
        //End of Rev Debashis && 20-07-2021

        [HttpPost]
        public HttpResponseMessage FaceRegTypeIDSave(FaceRegTypeIDInputModel model)
        {
            FaceRegTypeIDOutputModel omodel = new FaceRegTypeIDOutputModel();
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
                sqlcmd = new SqlCommand("PROC_APIFaceImageDetection", sqlcon);
                sqlcmd.Parameters.AddWithValue("@Action", "FaceRegTypeIDSave");
                sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                sqlcmd.Parameters.AddWithValue("@FaceRegTypeID", model.type_id);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Type ID update success.";
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
    }
}

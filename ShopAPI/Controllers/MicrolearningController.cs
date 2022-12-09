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
    public class MicrolearningController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage List(MicrolearningInput model)
        {
            MicrolearningOutput odata = new MicrolearningOutput();

            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
            }
            else
            {

                string sessionId = "";
                List<Microlearning> omedl2 = new List<Microlearning>();
                DataTable ds = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["baseUrlLeave"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("prc_Microlearing", sqlcon);
                sqlcmd.Parameters.Add("@User_Id", model.user_id);
                sqlcmd.Parameters.Add("@action", "Getlist");
                sqlcmd.Parameters.Add("@weburl", weburl);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(ds);
                sqlcon.Close();
                if (ds.Rows.Count > 0)
                {
                    omedl2 = APIHelperMethods.ToModelList<Microlearning>(ds);
                    odata.message = "Successfully get list.";
                    odata.status = "200";
                    odata.micro_learning_list = omedl2;
                }
                else
                {
                    odata.message = "No Data found.";
                    odata.status = "205";
                }

                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }

        }

        [HttpPost]
        public HttpResponseMessage UpdateNote(NoteInput model)
        {
            NoteOutput odata = new NoteOutput();

            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
            }
            else
            {

                string sessionId = "";
                List<Microlearning> omedl2 = new List<Microlearning>();
                DataTable ds = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("prc_Microlearing", sqlcon);
                sqlcmd.Parameters.Add("@User_Id", model.user_id);
                sqlcmd.Parameters.Add("@DATE_TIME", model.date_time);
                sqlcmd.Parameters.Add("@ID", model.id);
                sqlcmd.Parameters.Add("@NOTE", model.note);
                sqlcmd.Parameters.Add("@action", "UpdateNote");
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(ds);
                sqlcon.Close();
                if (ds.Rows.Count > 0)
                {
                    omedl2 = APIHelperMethods.ToModelList<Microlearning>(ds);
                    odata.message = "Successfully submit note.";
                    odata.status = "200";
                }
                else
                {
                    odata.message = "Some input parameters are missing.";
                    odata.status = "205";
                }

                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }

        }

        [HttpPost]
        public HttpResponseMessage UpdateVideoPosition(VideoPositionInput model)
        {
            VideoPositionOutput odata = new VideoPositionOutput();

            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
            }
            else
            {

                string sessionId = "";
                List<Microlearning> omedl2 = new List<Microlearning>();
                DataTable ds = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("prc_Microlearing", sqlcon);
                sqlcmd.Parameters.Add("@User_Id", model.user_id);
                sqlcmd.Parameters.Add("@action", "UpdateVideoPosition");
                sqlcmd.Parameters.Add("@ID", model.id);
                sqlcmd.Parameters.Add("@play_back_position", model.play_back_position);
                sqlcmd.Parameters.Add("@play_when_ready", model.play_when_ready);
                sqlcmd.Parameters.Add("@current_window", model.current_window);
                sqlcmd.Parameters.Add("@percentage", model.percentage);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(ds);
                sqlcon.Close();
                if (ds.Rows.Count > 0)
                {
                    odata.message = "Successfully update video postion.";
                    odata.status = "200";
                }
                else
                {
                    odata.message = "No Data found.";
                    odata.status = "205";
                }

                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }

        }


        [HttpPost]
        public HttpResponseMessage UpdateView(UpdateViewInput model)
        {
            UpdateViewOutput odata = new UpdateViewOutput();

            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
            }
            else
            {

                string sessionId = "";
                List<Microlearning> omedl2 = new List<Microlearning>();
                DataTable ds = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("prc_Microlearing", sqlcon);
                sqlcmd.Parameters.Add("@User_Id", model.user_id);
                sqlcmd.Parameters.Add("@action", "UpdateView");
                sqlcmd.Parameters.Add("@ID", model.id);
                sqlcmd.Parameters.Add("@close_date_time", model.close_date_time);
                sqlcmd.Parameters.Add("@open_date_time", model.open_date_time);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(ds);
                sqlcon.Close();
                if (ds.Rows.Count > 0)
                {                    
                    odata.message = "Successfully update file seeing time.";
                    odata.status = "200";
                }
                else
                {
                    odata.message = "No Data found.";
                    odata.status = "205";
                }

                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }

        }

        [HttpPost]
        public HttpResponseMessage DownloadHiostory(DownloadHiostoryInput model)
        {
            DownloadHiostoryOutput odata = new DownloadHiostoryOutput();

            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
            }
            else
            {

                string sessionId = "";
                DataTable ds = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("prc_Microlearing", sqlcon);
                sqlcmd.Parameters.Add("@User_Id", model.user_id);
                sqlcmd.Parameters.Add("@action", "DownloadHiostory");
                sqlcmd.Parameters.Add("@ID", model.id);
                sqlcmd.Parameters.Add("@isDownloaded", model.isDownloaded);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(ds);
                sqlcon.Close();
                if (ds.Rows.Count > 0)
                {
                    odata.message = "Successfully update file seeing time.";
                    odata.status = "200";
                }
                else
                {
                    odata.message = "No Data found.";
                    odata.status = "205";
                }

                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }

        }
    }
}

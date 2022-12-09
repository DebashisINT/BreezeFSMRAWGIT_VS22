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
    public class TaskController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage AddTask(TaskAddInput model)
        {

            TaskAddOutput oview = new TaskAddOutput();

            try
            {
                if (!ModelState.IsValid)
                {
                    oview.status = "213";
                    oview.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, oview);
                }
                else
                {
                    DataSet dt = new DataSet();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("Proc_Task", sqlcon);
                    sqlcmd.Parameters.Add("@Action", "Add");
                    sqlcmd.Parameters.Add("@APP_ID", model.id);
                    sqlcmd.Parameters.Add("@user_id", model.user_id);
                    sqlcmd.Parameters.Add("@TASK_DATE", model.date);
                    sqlcmd.Parameters.Add("@TASK", model.task);
                    sqlcmd.Parameters.Add("@DETAILS", model.details);
                    sqlcmd.Parameters.Add("@isCompleted", model.isCompleted);
                    sqlcmd.Parameters.Add("@Event_Id", model.eventID);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    oview.status = "200";
                    oview.message = "Successfully add task.";

                    var message = Request.CreateResponse(HttpStatusCode.OK, oview);
                    return message;
                }

            }
            catch (Exception ex)
            {
                oview.status = "209";
                oview.message = ex.Message;
                var message = Request.CreateResponse(HttpStatusCode.OK, oview);
                return message;
            }

        }

        [HttpPost]
        public HttpResponseMessage EditTask(TaskAddInput model)
        {

            TaskAddOutput oview = new TaskAddOutput();

            try
            {
                if (!ModelState.IsValid)
                {
                    oview.status = "213";
                    oview.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, oview);
                }
                else
                {
                    DataSet dt = new DataSet();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("Proc_Task", sqlcon);
                    sqlcmd.Parameters.Add("@Action", "Update");
                    sqlcmd.Parameters.Add("@APP_ID", model.id);
                    sqlcmd.Parameters.Add("@user_id", model.user_id);
                    sqlcmd.Parameters.Add("@TASK_DATE", model.date);
                    sqlcmd.Parameters.Add("@TASK", model.task);
                    sqlcmd.Parameters.Add("@DETAILS", model.details);
                    sqlcmd.Parameters.Add("@isCompleted", model.isCompleted);
                    sqlcmd.Parameters.Add("@Event_Id", model.eventID);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    oview.status = "200";
                    oview.message = "Successfully edit task.";

                    var message = Request.CreateResponse(HttpStatusCode.OK, oview);
                    return message;
                }

            }
            catch (Exception ex)
            {
                oview.status = "209";
                oview.message = ex.Message;
                var message = Request.CreateResponse(HttpStatusCode.OK, oview);
                return message;
            }

        }

        [HttpPost]
        public HttpResponseMessage DeleteTask(TaskDeleteInput model)
        {

            TaskAddOutput oview = new TaskAddOutput();

            try
            {
                if (!ModelState.IsValid)
                {
                    oview.status = "213";
                    oview.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, oview);
                }
                else
                {
                    DataSet dt = new DataSet();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("Proc_Task", sqlcon);
                    sqlcmd.Parameters.Add("@Action", "Delete");
                    sqlcmd.Parameters.Add("@APP_ID", model.id);
                    sqlcmd.Parameters.Add("@user_id", model.user_id);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    oview.status = "200";
                    oview.message = "Successfully delete task.";

                    var message = Request.CreateResponse(HttpStatusCode.OK, oview);
                    return message;
                }

            }
            catch (Exception ex)
            {
                oview.status = "209";
                oview.message = ex.Message;
                var message = Request.CreateResponse(HttpStatusCode.OK, oview);
                return message;
            }

        }

        [HttpPost]
        public HttpResponseMessage UpdateTask(TaskUpdateInput model)
        {

            TaskAddOutput oview = new TaskAddOutput();

            try
            {
                if (!ModelState.IsValid)
                {
                    oview.status = "213";
                    oview.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, oview);
                }
                else
                {
                    DataSet dt = new DataSet();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("Proc_Task", sqlcon);
                    sqlcmd.Parameters.Add("@Action", "UpdateStatus");
                    sqlcmd.Parameters.Add("@APP_ID", model.id);
                    sqlcmd.Parameters.Add("@user_id", model.user_id);
                    sqlcmd.Parameters.Add("@isCompleted", model.isCompleted);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    oview.status = "200";
                    oview.message = "Successfully task completed / pending.";

                    var message = Request.CreateResponse(HttpStatusCode.OK, oview);
                    return message;
                }

            }
            catch (Exception ex)
            {
                oview.status = "209";
                oview.message = ex.Message;
                var message = Request.CreateResponse(HttpStatusCode.OK, oview);
                return message;
            }

        }

        [HttpPost]
        public HttpResponseMessage TaskList(TaskListInput model)
        {

            TaskListOutput oview = new TaskListOutput();

            try
            {
                if (!ModelState.IsValid)
                {
                    oview.status = "213";
                    oview.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, oview);
                }
                else
                {
                    List<TaskList> objList = new List<TaskList>();
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("Proc_Task", sqlcon);
                    sqlcmd.Parameters.Add("@Action", "List");
                    sqlcmd.Parameters.Add("@Task_Date", model.date);
                    sqlcmd.Parameters.Add("@user_id", model.user_id);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        objList = APIHelperMethods.ToModelList<TaskList>(dt);
                        oview.status = "200";
                        oview.message = "Successfully get task list.";
                        oview.task_list = objList;
                    }
                    else
                    {
                        oview.status = "201";
                        oview.message = "No data found.";
                        oview.task_list = objList;
                    }

                   

                    var message = Request.CreateResponse(HttpStatusCode.OK, oview);
                    return message;
                }

            }
            catch (Exception ex)
            {
                oview.status = "209";
                oview.message = ex.Message;
                var message = Request.CreateResponse(HttpStatusCode.OK, oview);
                return message;
            }

        }

    }
}

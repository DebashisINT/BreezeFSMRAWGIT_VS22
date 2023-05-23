#region======================================Revision History=========================================================
//1.0   V2.0.39     Debashis    16/05/2023      Some new methods have been added.Row: 828 to 832
#endregion===================================End of Revision History==================================================
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
        //Rev 1.0 Row: 828,829,830,831,832
        [HttpPost]
        public HttpResponseMessage TaskPriorityList(TaskPriorityListInput model)
        {
            TaskPriorityListOutput oview = new TaskPriorityListOutput();

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
                    List<TaskPriorityList> objList = new List<TaskPriorityList>();
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("Proc_Task", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@Action", "TaskPriorityList");
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        objList = APIHelperMethods.ToModelList<TaskPriorityList>(dt);
                        oview.status = "200";
                        oview.message = "Successfully get task priority list.";
                        oview.task_priority_list = objList;
                    }
                    else
                    {
                        oview.status = "201";
                        oview.message = "No data found.";
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

        [HttpPost]
        public HttpResponseMessage TaskPriorityWiseList(TaskPriorityWiseListInput model)
        {
            TaskPriorityWiseListOutput oview = new TaskPriorityWiseListOutput();

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
                    List<TaskPriorityDetList> objList = new List<TaskPriorityDetList>();
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_FTSAPITASKMANAGEMENTINFO", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "TaskPriorityWiseList");
                    sqlcmd.Parameters.AddWithValue("@FROM_DATE", model.from_date);
                    sqlcmd.Parameters.AddWithValue("@TO_DATE", model.to_date);
                    sqlcmd.Parameters.AddWithValue("@TASK_PRIORITY_ID", model.task_priority_id);
                    sqlcmd.Parameters.AddWithValue("@TASK_PRIORITY_NAME", model.task_priority_name);
                    sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        objList = APIHelperMethods.ToModelList<TaskPriorityDetList>(dt);
                        oview.status = "200";
                        oview.message = "Successfully get task list.";
                        oview.task_priority_name = model.task_priority_name;
                        oview.task_priority_id = model.task_priority_id;
                        oview.user_id= model.user_id;
                        oview.task_dtls_list = objList;
                    }
                    else
                    {
                        oview.status = "201";
                        oview.message = "No data found.";
                        oview.task_priority_name = model.task_priority_name;
                        oview.task_priority_id = model.task_priority_id;
                        oview.user_id = model.user_id;
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

        [HttpPost]
        public HttpResponseMessage AddTaskDetailList(AddTaskDetailListInput model)
        {
            AddTaskDetailListOutput oview = new AddTaskDetailListOutput();

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
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_FTSAPITASKMANAGEMENTINFO", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "AddTaskDetailList");
                    sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@TASK_ID", model.task_id);
                    sqlcmd.Parameters.AddWithValue("@TASK_DATE", model.task_date);
                    sqlcmd.Parameters.AddWithValue("@TASK_TIME", model.task_time);
                    sqlcmd.Parameters.AddWithValue("@TASK_STATUS", model.task_status);
                    sqlcmd.Parameters.AddWithValue("@TASK_DETAILS", model.task_details);
                    sqlcmd.Parameters.AddWithValue("@OTHER_REMARKS", model.other_remarks);
                    sqlcmd.Parameters.AddWithValue("@TASK_NEXT_DATE", model.task_next_date);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        oview.status = "200";
                        oview.message = "Task added Successfully.";
                    }
                    else
                    {
                        oview.status = "201";
                        oview.message = "No data added.";
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

        [HttpPost]
        public HttpResponseMessage EditTaskDetailList(EditTaskDetailListInput model)
        {
            EditTaskDetailListOutput oview = new EditTaskDetailListOutput();

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
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_FTSAPITASKMANAGEMENTINFO", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "EditTaskDetailList");
                    sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@TASK_STATUS_ID", model.task_status_id);
                    sqlcmd.Parameters.AddWithValue("@TASK_ID", model.task_id);
                    sqlcmd.Parameters.AddWithValue("@TASK_DATE", model.task_date);
                    sqlcmd.Parameters.AddWithValue("@TASK_TIME", model.task_time);
                    sqlcmd.Parameters.AddWithValue("@TASK_STATUS", model.task_status);
                    sqlcmd.Parameters.AddWithValue("@TASK_DETAILS", model.task_details);
                    sqlcmd.Parameters.AddWithValue("@OTHER_REMARKS", model.other_remarks);
                    sqlcmd.Parameters.AddWithValue("@TASK_NEXT_DATE", model.task_next_date);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        oview.status = "200";
                        oview.message = "Task updated Successfully.";
                    }
                    else
                    {
                        oview.status = "201";
                        oview.message = "No data added.";
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

        [HttpPost]
        public HttpResponseMessage GetTaskDetailList(GetTaskDetailListInput model)
        {
            GetTaskDetailListOutput oview = new GetTaskDetailListOutput();

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
                    List<TaskStatusDetailList> objList = new List<TaskStatusDetailList>();
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_FTSAPITASKMANAGEMENTINFO", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "GetTaskDetailList");
                    sqlcmd.Parameters.AddWithValue("@TASK_ID", model.task_id);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        objList = APIHelperMethods.ToModelList<TaskStatusDetailList>(dt);
                        oview.status = "200";
                        oview.message = "Successfully get task detail list.";
                        oview.task_id= model.task_id;
                        oview.task_status_dtls_list = objList;
                    }
                    else
                    {
                        oview.status = "201";
                        oview.message = "No data found.";
                        oview.task_id = model.task_id;
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
        //End of Rev 1.0 Row: 828,829,830,831,832
    }
}

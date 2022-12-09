using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ShopAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace ShopAPI.Controllers
{
    public class LeaveController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Types(LeavetypeInput model)
        {
            Leavetypeoutput odata = new Leavetypeoutput();
            try
            {
                List<Leavetype> oview = new List<Leavetype>();

                string token = string.Empty;
                string versionname = string.Empty;
                String tokenmatch = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                // String con = Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]);
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();

                sqlcmd = new SqlCommand("proc_FTS_LeaveTypes", sqlcon);
                sqlcmd.Parameters.Add("@Action", "WorkTypes");
                sqlcmd.Parameters.Add("@User_id", model.user_id);



                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();

                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<Leavetype>(dt);
                    odata.status = "200";
                    odata.message = "Success";
                    odata.leave_type_list = oview;
                }

                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;

            }

            catch (Exception ex)
            {
                odata.status = "209";
                odata.message = ex.Message;
                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }
        }


        [HttpPost]
        public HttpResponseMessage GetLeaveList(LeaveListInput model)
        {
            LeaveListoutput odata = new LeaveListoutput();
            try
            {
                List<LeaveList> oview = new List<LeaveList>();

                string token = string.Empty;
                string versionname = string.Empty;
                String tokenmatch = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                // String con = Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]);
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();

                sqlcmd = new SqlCommand("proc_FTS_LeaveTypes", sqlcon);
                sqlcmd.Parameters.Add("@Action", "LeaveList");
                sqlcmd.Parameters.Add("@User_id", model.user_id);
                sqlcmd.Parameters.Add("@from_date", model.from_date);
                sqlcmd.Parameters.Add("@to_date", model.to_date);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();

                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<LeaveList>(dt);
                    odata.status = "200";
                    odata.message = "Success";
                    odata.leave_list = oview;
                }
                else
                {
                       
                        odata.status = "205";
                        odata.message = "No Data Found";
                        odata.leave_list = oview;
                    
                }

                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;

            }

            catch (Exception ex)
            {
                odata.status = "209";
                odata.message = ex.Message;
                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }
        }


    }
}

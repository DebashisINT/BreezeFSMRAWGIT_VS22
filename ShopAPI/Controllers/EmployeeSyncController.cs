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
    public class EmployeeSyncController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage PushEmployee(EmployeeSyncInput model)
        {
            EmployeeSyncModel odata = new EmployeeSyncModel();
            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
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
                sqlcmd = new SqlCommand("FTS_APIEmployeeUserSync", sqlcon);
                sqlcmd.Parameters.Add("@Branch", model.Branch);
                sqlcmd.Parameters.Add("@cnt_UCC", model.cnt_UCC);
                sqlcmd.Parameters.Add("@Salutation", model.Salutation);
                sqlcmd.Parameters.Add("@FirstName", model.FirstName);
                sqlcmd.Parameters.Add("@MiddleName", model.MiddleName);
                sqlcmd.Parameters.Add("@LastName", model.LastName);
                sqlcmd.Parameters.Add("@ContactType", model.ContactType);
                sqlcmd.Parameters.Add("@ReferedBy", model.ReferedBy);
                sqlcmd.Parameters.Add("@DOB", model.DOB);
                sqlcmd.Parameters.Add("@MaritalStatus", model.MaritalStatus);
                sqlcmd.Parameters.Add("@AnniversaryDate", model.AnniversaryDate);
                sqlcmd.Parameters.Add("@Sex", model.Sex);
                sqlcmd.Parameters.Add("@CreateDate", model.CreateDate);
                sqlcmd.Parameters.Add("@CreateUser", model.CreateUser);
                sqlcmd.Parameters.Add("@Bloodgroup", model.Bloodgroup);
                sqlcmd.Parameters.Add("@SettlementMode", model.SettlementMode);
                sqlcmd.Parameters.Add("@ContractDeliveryMode", model.ContractDeliveryMode);
                sqlcmd.Parameters.Add("@DirectTMClient", model.DirectTMClient);
                sqlcmd.Parameters.Add("@RelationshipWithDirector", model.RelationshipWithDirector);
                sqlcmd.Parameters.Add("@HasOtherAccount", model.HasOtherAccount);
                sqlcmd.Parameters.Add("@Is_Active", model.Is_Active);
                sqlcmd.Parameters.Add("@cnt_IdType", model.cnt_IdType);
                sqlcmd.Parameters.Add("@AccountGroupID", model.AccountGroupID);
                sqlcmd.Parameters.Add("@DateofJoining", model.DateofJoining);
                sqlcmd.Parameters.Add("@Organization", model.Organization);
                sqlcmd.Parameters.Add("@JobResponsibility", model.JobResponsibility);
                sqlcmd.Parameters.Add("@Designation", model.Designation);
                sqlcmd.Parameters.Add("@Department", model.Department);
                sqlcmd.Parameters.Add("@ReportTo", model.ReportTo);
                sqlcmd.Parameters.Add("@Deputy", model.Deputy);
                sqlcmd.Parameters.Add("@Colleague", model.Colleague);
                sqlcmd.Parameters.Add("@workinghours", model.workinghours);
                sqlcmd.Parameters.Add("@TotalLeavePA", model.TotalLeavePA);
                sqlcmd.Parameters.Add("@LeaveSchemeAppliedFrom", model.LeaveSchemeAppliedFrom);

                sqlcmd.Parameters.Add("@username", model.username);
                sqlcmd.Parameters.Add("@Encryptpass", model.Encryptpass);
                sqlcmd.Parameters.Add("@UserLoginId", model.UserLoginId);
                sqlcmd.Parameters.Add("@usergroup", model.usergroup);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    odata.status = "200";
                    odata.message = "Successfully get meeting list.";
                    odata.Cnt_id = Convert.ToString(dt.Rows[0]["Cnt_id"]);
                    odata.User_id = Convert.ToString(dt.Rows[0]["User_id"]);
                }
                else
                {
                    odata.status = "205";
                    odata.message = "Failed";
                }
                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }
        }

        //Rev Debashis
        [HttpPost]
        public HttpResponseMessage UserIMEIClear(UserIMEIClearInput model)
        {
            UserIMEIClearOutput odata = new UserIMEIClearOutput();
            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
            }
            else
            {
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_APIDEVICEINFORMATION", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "IMEICLEAR");
                sqlcmd.Parameters.Add("@USER_ID", model.user_id);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    odata.status = "200";
                    odata.message = "IMEI Delete successfully.";
                }
                else
                {
                    odata.status = "205";
                    odata.message = "IMEI not found.";
                }
                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }
        }
        //End of Rev Debashis
    }
}

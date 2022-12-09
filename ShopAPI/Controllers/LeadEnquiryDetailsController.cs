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
    public class LeadEnquiryDetailsController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage LeadEnquiryWiseCustList(LeadEnquiryWiseCustListInput model)
        {
            LeadEnquiryWiseCustListOutput omodel = new LeadEnquiryWiseCustListOutput();
            List<CustDelList> oview = new List<CustDelList>();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_FSMAPILEADENQUIRYDETAILS", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "USERENQUIRYWISECUSTLIST");
                sqlcmd.Parameters.Add("@FROMDATE", model.from_date);
                sqlcmd.Parameters.Add("@TODATE", model.to_date);
                sqlcmd.Parameters.Add("@ENQUIRY_FROM", model.enquiry_from);
                sqlcmd.Parameters.Add("@USER_ID", model.user_id);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<CustDelList>(dt);
                    omodel.status = "200";
                    omodel.message = "Successfully get Lead Enquiry Wise Customer list.";
                    omodel.enquiry_from = model.enquiry_from;
                    omodel.user_id = model.user_id;
                    omodel.customer_dtls_list = oview;
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
        public HttpResponseMessage SaveActivity(SaveActivityInput model)
        {
            SaveActivityOutput omodel = new SaveActivityOutput();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_FSMAPILEADENQUIRYDETAILS", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "SAVEACTIVITY");
                sqlcmd.Parameters.Add("@CRM_ID", model.crm_id);
                sqlcmd.Parameters.Add("@ACTIVITY_DATE", model.activity_date);
                sqlcmd.Parameters.Add("@ACTIVITY_TIME", model.activity_time);
                sqlcmd.Parameters.Add("@ACTIVITY_TYPE_NAME", model.activity_type_name);
                sqlcmd.Parameters.Add("@ACTIVITY_STATUS", model.activity_status);
                sqlcmd.Parameters.Add("@ACTIVITY_DETAILS", model.activity_details);
                sqlcmd.Parameters.Add("@OTHER_REMARKS", model.other_remarks);
                //Rev Debashis Row: 676
                sqlcmd.Parameters.Add("@ACTIVITY_NEXT_DATE", model.activity_next_date);
                //End of Rev Debashis Row: 676

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Added Successfully.";
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
        public HttpResponseMessage ShowActivityList(ShowActivityListInput model)
        {
            ShowActivityListOutput omodel = new ShowActivityListOutput();
            List<ActivityDelList> oview = new List<ActivityDelList>();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_FSMAPILEADENQUIRYDETAILS", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "SHOWACTIVITYLIST");
                sqlcmd.Parameters.Add("@CRM_ID", model.crm_id);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<ActivityDelList>(dt);
                    omodel.status = "200";
                    omodel.message = "Successfully get Activity list.";
                    omodel.crm_id = model.crm_id;
                    omodel.activity_dtls_list = oview;
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
        public HttpResponseMessage UpdateActivity(UpdateActivityInput model)
        {
            UpdateActivityOutput omodel = new UpdateActivityOutput();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_FSMAPILEADENQUIRYDETAILS", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "UPDATEACTIVITY");
                sqlcmd.Parameters.Add("@CRM_ID", model.crm_id);
                sqlcmd.Parameters.Add("@ACTIVITY_ID", model.activity_id);
                sqlcmd.Parameters.Add("@ACTIVITY_DATE", model.activity_date);
                sqlcmd.Parameters.Add("@ACTIVITY_TIME", model.activity_time);
                sqlcmd.Parameters.Add("@ACTIVITY_TYPE_NAME", model.activity_type_name);
                sqlcmd.Parameters.Add("@ACTIVITY_STATUS", model.activity_status);
                sqlcmd.Parameters.Add("@ACTIVITY_DETAILS", model.activity_details);
                sqlcmd.Parameters.Add("@OTHER_REMARKS", model.other_remarks);
                //Rev Debashis Row: 676
                sqlcmd.Parameters.Add("@ACTIVITY_NEXT_DATE", model.activity_next_date);
                //End of Rev Debashis Row: 676

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Updated Successfully.";
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

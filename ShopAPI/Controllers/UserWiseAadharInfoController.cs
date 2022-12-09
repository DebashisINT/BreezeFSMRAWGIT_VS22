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
    public class UserWiseAadharInfoController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage UserAadharInfo(UserWiseAadharInfoInput model)
        {
            UserWiseAadharInfoOutput odata = new UserWiseAadharInfoOutput();

            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
            }
            else
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_APIEMPLOYEEAADHARINFORMATION", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "INSERTDATA");
                sqlcmd.Parameters.Add("@USER_ID", model.aadhaar_holder_user_id);
                sqlcmd.Parameters.Add("@USERCONTACTID", model.aadhaar_holder_user_contactid);
                sqlcmd.Parameters.Add("@AADHAAR_NO", model.aadhaar_no);
                sqlcmd.Parameters.Add("@AADHAARDATE", model.date);
                sqlcmd.Parameters.Add("@FEEDBACK", model.feedback);
                sqlcmd.Parameters.Add("@ADDRESS", model.address);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    odata.status = "200";
                    odata.message = "Successfully Aadhaar submited.";
                }
                else
                {
                    odata.status = "205";
                    odata.message = "No data found.";
                }
                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage UserAadharList(UserAadharListInput model)
        {
            UserAadharListOutput omodel = new UserAadharListOutput();
            List<AlluserAadharList> oview = new List<AlluserAadharList>();

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
                sqlcmd = new SqlCommand("PRC_APIEMPLOYEEAADHARINFORMATION", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "USERAADHARLIST");
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<AlluserAadharList>(dt);
                    omodel.all_aadhaar_list = oview;
                    omodel.status = "200";
                    omodel.message = "Successfully Aadhaar submited.";
                }
                else
                {
                    omodel.status = "205";
                    omodel.message = "No data found.";
                }
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }
    }
}

#region======================================Revision History=========================================================
//Written By : Debashis Talukder On 05/12/2023
//Purpose : Save & Fetch List CRM Contact.Row: 880 to 884 & 898
#endregion===================================End of Revision History==================================================

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
    public class CRMContactInfoController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage CRMCompanyList(CRMCompanyListInput model)
        {
            CRMCompanyListOutput odata = new CRMCompanyListOutput();
            try
            {
                List<CRMCompanyList> oview = new List<CRMCompanyList>();

                string token = string.Empty;
                string versionname = string.Empty;
                String tokenmatch = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();

                sqlcmd = new SqlCommand("PRC_APICRMCONTACTINFO", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "CRMCOMPANYLIST");

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();

                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<CRMCompanyList>(dt);
                    odata.status = "200";
                    odata.message = "Success";
                    odata.company_list = oview;
                }
                else
                {
                    odata.status = "205";
                    odata.message = "No Data Found";
                    odata.company_list = oview;
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
        public HttpResponseMessage CRMTypeList(CRMTypeListInput model)
        {
            CRMTypeListOutput odata = new CRMTypeListOutput();
            try
            {
                List<CRMTypeList> oview = new List<CRMTypeList>();

                string token = string.Empty;
                string versionname = string.Empty;
                String tokenmatch = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();

                sqlcmd = new SqlCommand("PRC_APICRMCONTACTINFO", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "CRMTYPELIST");

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();

                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<CRMTypeList>(dt);
                    odata.status = "200";
                    odata.message = "Success";
                    odata.type_list = oview;
                }
                else
                {
                    odata.status = "205";
                    odata.message = "No Data Found";
                    odata.type_list = oview;
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
        public HttpResponseMessage CRMStatusList(CRMStatusListInput model)
        {
            CRMStatusListOutput odata = new CRMStatusListOutput();
            try
            {
                List<CRMStatusList> oview = new List<CRMStatusList>();

                string token = string.Empty;
                string versionname = string.Empty;
                String tokenmatch = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();

                sqlcmd = new SqlCommand("PRC_APICRMCONTACTINFO", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "CRMSTATUSLIST");

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();

                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<CRMStatusList>(dt);
                    odata.status = "200";
                    odata.message = "Success";
                    odata.status_list = oview;
                }
                else
                {
                    odata.status = "205";
                    odata.message = "No Data Found";
                    odata.status_list = oview;
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
        public HttpResponseMessage CRMSourceList(CRMSourceListInput model)
        {
            CRMSourceListOutput odata = new CRMSourceListOutput();
            try
            {
                List<CRMSourceList> oview = new List<CRMSourceList>();

                string token = string.Empty;
                string versionname = string.Empty;
                String tokenmatch = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();

                sqlcmd = new SqlCommand("PRC_APICRMCONTACTINFO", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "CRMSOURCELIST");

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();

                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<CRMSourceList>(dt);
                    odata.status = "200";
                    odata.message = "Success";
                    odata.source_list = oview;
                }
                else
                {
                    odata.status = "205";
                    odata.message = "No Data Found";
                    odata.source_list = oview;
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
        public HttpResponseMessage CRMStageList(CRMStageListInput model)
        {
            CRMStageListOutput odata = new CRMStageListOutput();
            try
            {
                List<CRMStageList> oview = new List<CRMStageList>();

                string token = string.Empty;
                string versionname = string.Empty;
                String tokenmatch = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();

                sqlcmd = new SqlCommand("PRC_APICRMCONTACTINFO", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "CRMSTAGELIST");

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();

                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<CRMStageList>(dt);
                    odata.status = "200";
                    odata.message = "Success";
                    odata.stage_list = oview;
                }
                else
                {
                    odata.status = "205";
                    odata.message = "No Data Found";
                    odata.stage_list = oview;
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
        public HttpResponseMessage CRMCompanySave(CRMCompanySaveInput model)
        {
            CRMCompanySaveOutput odata = new CRMCompanySaveOutput();
            List<Companynamelist> Lview = new List<Companynamelist>();
            try
            {
                string token = string.Empty;
                string versionname = string.Empty;
                String tokenmatch = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];

                if (!ModelState.IsValid)
                {
                    odata.status = "213";
                    odata.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
                }
                else
                {
                    foreach (var s2 in model.company_name_list)
                    {
                        Lview.Add(new Companynamelist()
                        {
                            company_name = s2.company_name
                        });
                    }
                    string JsonXML = XmlConversion.ConvertToXml(Lview, 0);
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();

                    sqlcmd = new SqlCommand("PRC_APICRMCONTACTINFO", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "CRMCOMPANYSAVE");
                    sqlcmd.Parameters.AddWithValue("@USERID", model.created_by);
                    sqlcmd.Parameters.AddWithValue("@JsonXML", JsonXML);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0)
                    {
                        odata.status = "200";
                        odata.message = "Success";
                    }
                    else
                    {
                        odata.status = "205";
                        odata.message = "No Data Found";
                    }

                    var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                    return message;
                }
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

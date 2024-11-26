#region======================================Revision History=========================================================
//Written By : Debashis Talukder On 15/11/2024
//Purpose: Loan Info Details.Refer: Row: 1000,1001,1002,1003 & 1004
#endregion===================================End of Revision History==================================================

using Newtonsoft.Json;
using ShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using System.Web;

namespace ShopAPI.Controllers
{
    public class LoanInfoDetailsController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage LoanRiskTypeLists(LoanRiskTypeListInput model)
        {
            LoanRiskTypeListOutput omodel = new LoanRiskTypeListOutput();
            List<LRlistOutput> Tview = new List<LRlistOutput>();

            try
            {
                if (!ModelState.IsValid)
                {
                    omodel.status = "213";
                    omodel.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
                }
                else
                {
                    DataSet ds = new DataSet();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_FSMLOANINFODETAILS", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "LOANRISKLIST");
                    sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(ds);
                    sqlcon.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Successfully Get List.";
                        Tview = APIHelperMethods.ToModelList<LRlistOutput>(ds.Tables[0]);
                        omodel.data_list = Tview;
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
            catch (Exception ex)
            {
                omodel.status = "204";
                omodel.message = ex.Message;
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage LoanDispositionLists(LoanDispositionListInput model)
        {
            LoanDispositionListOutput omodel = new LoanDispositionListOutput();
            List<LDlistOutput> Tview = new List<LDlistOutput>();

            try
            {
                if (!ModelState.IsValid)
                {
                    omodel.status = "213";
                    omodel.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
                }
                else
                {
                    DataSet ds = new DataSet();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_FSMLOANINFODETAILS", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "LOANDISPOSITIONLIST");
                    sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(ds);
                    sqlcon.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Successfully Get List.";
                        Tview = APIHelperMethods.ToModelList<LDlistOutput>(ds.Tables[0]);
                        omodel.data_list = Tview;
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
            catch (Exception ex)
            {
                omodel.status = "204";
                omodel.message = ex.Message;
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage LoanFinalStatusLists(LoanFinalStatusListInput model)
        {
            LoanFinalStatusListOutput omodel = new LoanFinalStatusListOutput();
            List<LFSlistOutput> Tview = new List<LFSlistOutput>();

            try
            {
                if (!ModelState.IsValid)
                {
                    omodel.status = "213";
                    omodel.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
                }
                else
                {
                    DataSet ds = new DataSet();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_FSMLOANINFODETAILS", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "LOANFINALSTATUSLIST");
                    sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(ds);
                    sqlcon.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Successfully Get List.";
                        Tview = APIHelperMethods.ToModelList<LFSlistOutput>(ds.Tables[0]);
                        omodel.data_list = Tview;
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
            catch (Exception ex)
            {
                omodel.status = "204";
                omodel.message = ex.Message;
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage LoanDetailFetch(LoanDetailFetchInput model)
        {
            LoanDetailFetchOutput omodel = new LoanDetailFetchOutput();
            List<LoanlistOutput> LDview = new List<LoanlistOutput>();

            try
            {
                if (!ModelState.IsValid)
                {
                    omodel.status = "213";
                    omodel.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
                }
                else
                {
                    DataSet ds = new DataSet();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_FSMLOANINFODETAILS", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "LOANDETAILFETCH");
                    sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(ds);
                    sqlcon.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Successfully Get List.";
                        LDview = APIHelperMethods.ToModelList<LoanlistOutput>(ds.Tables[0]);
                        omodel.data_list = LDview;
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
            catch (Exception ex)
            {
                omodel.status = "204";
                omodel.message = ex.Message;
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage LoanDetailUpdate(LoanDetailUpdateInput model)
        {
            LoanDetailUpdateOutput omodel = new LoanDetailUpdateOutput();

            try
            {
                if (!ModelState.IsValid)
                {
                    omodel.status = "213";
                    omodel.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
                }
                else
                {
                    DataSet ds = new DataSet();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_FSMLOANINFODETAILS", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "LOANDETAILUPDATE");
                    sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@SHOP_ID", model.shop_id);
                    sqlcmd.Parameters.AddWithValue("@RISK_ID", model.risk_id);
                    sqlcmd.Parameters.AddWithValue("@WORKABLE", model.workable);
                    sqlcmd.Parameters.AddWithValue("@DISPOSITION_CODE_ID", model.disposition_code_id);
                    sqlcmd.Parameters.AddWithValue("@PTP_DATE", model.ptp_Date);
                    sqlcmd.Parameters.AddWithValue("@PTP_AMT", model.ptp_amt);
                    sqlcmd.Parameters.AddWithValue("@COLLECTION_DATE", model.collection_date);
                    sqlcmd.Parameters.AddWithValue("@COLLECTION_AMOUNT", model.collection_amount);
                    sqlcmd.Parameters.AddWithValue("@FINAL_STATUS_ID", model.final_status_id);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(ds);
                    sqlcon.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Success.";
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
            catch (Exception ex)
            {
                omodel.status = "204";
                omodel.message = ex.Message;
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }
    }
}

#region======================================Revision History=========================================================
//Written By : Debashis Talukder On 11/11/2024
//Purpose: Target Vs Achievement Info Details.Refer: Row: 992,993,994,996,997,998 & 999
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
using System.Configuration;

namespace ShopAPI.Controllers
{
    public class TAInfoDetailsController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage TargetTypeLists(TargetTypeListInput model)
        {
            TargetTypeListOutput omodel = new TargetTypeListOutput();
            List<TAlistOutput> Tview = new List<TAlistOutput>();

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
                    sqlcmd = new SqlCommand("PRC_FSMTARGETACHIEVEINFODETAILS", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "TARGETTYPELIST");
                    sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(ds);
                    sqlcon.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Successfully Get List.";
                        Tview = APIHelperMethods.ToModelList<TAlistOutput>(ds.Tables[0]);
                        omodel.target_type_list = Tview;
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
        public HttpResponseMessage TargetLevelLists(TargetLevelListInput model)
        {
            TargetLevelListOutput omodel = new TargetLevelListOutput();
            List<TLlistOutput> Tview = new List<TLlistOutput>();

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
                    sqlcmd = new SqlCommand("PRC_FSMTARGETACHIEVEINFODETAILS", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "TARGETLEVELLIST");
                    sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(ds);
                    sqlcon.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Successfully Get List.";
                        Tview = APIHelperMethods.ToModelList<TLlistOutput>(ds.Tables[0]);
                        omodel.target_level_list = Tview;
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
        public HttpResponseMessage TargetTimeFrameLists(TargetTimeFrameListInput model)
        {
            TargetTimeFrameListOutput omodel = new TargetTimeFrameListOutput();
            List<TTFlistOutput> Tview = new List<TTFlistOutput>();

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
                    sqlcmd = new SqlCommand("PRC_FSMTARGETACHIEVEINFODETAILS", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "TARGETTIMEFRAMELIST");
                    sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(ds);
                    sqlcon.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Successfully Get List.";
                        Tview = APIHelperMethods.ToModelList<TTFlistOutput>(ds.Tables[0]);
                        omodel.target_time_list = Tview;
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
        public HttpResponseMessage FetchTargetAchievementDetails(FetchTargetAchievementDetailsInput model)
        {
            FetchTargetAchievementDetailsOutput omodel = new FetchTargetAchievementDetailsOutput();
            List<AchvlistOutput> Tview = new List<AchvlistOutput>();

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
                    sqlcmd = new SqlCommand("PRC_FSMTARGETACHIEVEINFODETAILS", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "FETCHTADETAILS");
                    sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@TARGET_TYPE_ID", model.target_type_id);
                    sqlcmd.Parameters.AddWithValue("@TARGET_LEVEL_ID", model.target_level_id);
                    sqlcmd.Parameters.AddWithValue("@TIME_FRAME", model.time_frame);
                    sqlcmd.Parameters.AddWithValue("@START_DATE", model.start_date);
                    sqlcmd.Parameters.AddWithValue("@END_DATE", model.end_date);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(ds);
                    sqlcon.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Successfully Get List.";
                        omodel.value_for = Convert.ToString(ds.Tables[0].Rows[0]["value_for"]);
                        omodel.target = Convert.ToString(ds.Tables[0].Rows[0]["target"]);
                        omodel.achv = Convert.ToString(ds.Tables[0].Rows[0]["achv"]);
                        Tview = APIHelperMethods.ToModelList<AchvlistOutput>(ds.Tables[1]);
                        omodel.achv_list = Tview;
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


        //[HttpPost]
        //public HttpResponseMessage FetchTargetAchievementDetails(CompositeInputModel input)
        //{
        //    FetchTargetAchievementDetailsOutput responseModel = new FetchTargetAchievementDetailsOutput();
        //    FetchTargetAchievementDetailsDecimalOutput decimalResponseModel = new FetchTargetAchievementDetailsDecimalOutput();

        //    try
        //    {
        //        // Validate input
        //        if (input == null || input.Model1 == null || input.Model2 == null)
        //        {
        //            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Input models cannot be null.");
        //        }

        //        if (!ModelState.IsValid)
        //        {
        //            responseModel.status = "213";
        //            responseModel.message = "Some input parameters are missing.";
        //            return Request.CreateResponse(HttpStatusCode.BadRequest, responseModel);
        //        }

        //        // Execute stored procedure
        //        var dataSet = FetchDataFromDatabase(input);

        //        if (dataSet.Tables[0].Rows.Count > 0)
        //        {
        //            if (input.Model1.target_type_id == 1 || input.Model1.target_type_id == 5)
        //            {
        //                responseModel = MapToIntegerOutput(dataSet);
        //                return Request.CreateResponse(HttpStatusCode.OK, responseModel);
        //            }
        //            else if (input.Model2.target_type_id == 2 || input.Model2.target_type_id == 3 || input.Model2.target_type_id == 4)
        //            {
        //                decimalResponseModel = MapToDecimalOutput(dataSet);
        //                return Request.CreateResponse(HttpStatusCode.OK, decimalResponseModel);
        //            }
        //        }
        //        else
        //        {
        //            responseModel.status = "205";
        //            responseModel.message = "No data found.";
        //        }
        //        return Request.CreateResponse(HttpStatusCode.OK, responseModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        responseModel.status = "204";
        //        responseModel.message = ex.Message;
        //        return Request.CreateResponse(HttpStatusCode.InternalServerError, responseModel);
        //    }
        //}

        //private DataSet FetchDataFromDatabase(CompositeInputModel input)
        //{
        //    DataSet ds = new DataSet();
        //    string connectionString = ConfigurationManager.AppSettings["DBConnectionDefault"];

        //    using (SqlConnection sqlcon = new SqlConnection(connectionString))
        //    {
        //        using (SqlCommand sqlcmd = new SqlCommand("PRC_FSMTARGETACHIEVEINFODETAILS", sqlcon))
        //        {
        //            sqlcmd.CommandType = CommandType.StoredProcedure;
        //            sqlcmd.Parameters.AddWithValue("@ACTION", "FETCHTADETAILS");

        //            // Add parameters for Model1
        //            if (input.Model1.target_type_id == 1 || input.Model1.target_type_id == 5)
        //            {
        //                AddSqlParameters(sqlcmd, input.Model1);
        //            }

        //            // Add parameters for Model2
        //            if (input.Model2.target_type_id == 2 || input.Model2.target_type_id == 3 || input.Model2.target_type_id == 4)
        //            {
        //                AddSqlParameters(sqlcmd, input.Model2);
        //            }

        //            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
        //            da.Fill(ds);
        //        }
        //    }

        //    return ds;
        //}

        //private void AddSqlParameters(SqlCommand sqlcmd, FetchTargetAchievementDetailsInput model1)
        //{
        //    //throw new NotImplementedException();
        //    sqlcmd.Parameters.AddWithValue("@USER_ID", model1.user_id);
        //    sqlcmd.Parameters.AddWithValue("@TARGET_TYPE_ID", model1.target_type_id);
        //    sqlcmd.Parameters.AddWithValue("@TARGET_LEVEL_ID", model1.target_level_id);
        //    sqlcmd.Parameters.AddWithValue("@TIME_FRAME", model1.time_frame);
        //    sqlcmd.Parameters.AddWithValue("@START_DATE", model1.start_date);
        //    sqlcmd.Parameters.AddWithValue("@END_DATE", model1.end_date);
        //}

        //private void AddSqlParameters(SqlCommand sqlcmd, FetchTargetAchievementDetailsDecimalInput model2)
        //{
        //    //throw new NotImplementedException();
        //    sqlcmd.Parameters.AddWithValue("@USER_ID", model2.user_id);
        //    sqlcmd.Parameters.AddWithValue("@TARGET_TYPE_ID", model2.target_type_id);
        //    sqlcmd.Parameters.AddWithValue("@TARGET_LEVEL_ID", model2.target_level_id);
        //    sqlcmd.Parameters.AddWithValue("@TIME_FRAME", model2.time_frame);
        //    sqlcmd.Parameters.AddWithValue("@START_DATE", model2.start_date);
        //    sqlcmd.Parameters.AddWithValue("@END_DATE", model2.end_date);
        //}

        //private FetchTargetAchievementDetailsOutput MapToIntegerOutput(DataSet dataSet)
        //{
        //    var response = new FetchTargetAchievementDetailsOutput
        //    {
        //        status = "200",
        //        message = "Successfully Get List.",
        //        value_for = Convert.ToString(dataSet.Tables[0].Rows[0]["value_for"]),
        //        target = Convert.ToInt32(dataSet.Tables[0].Rows[0]["target"]),
        //        achv = Convert.ToInt32(dataSet.Tables[0].Rows[0]["achv"]),
        //        achv_list = APIHelperMethods.ToModelList<AchvlistOutput>(dataSet.Tables[1])
        //    };
        //    return response;
        //}

        //private FetchTargetAchievementDetailsDecimalOutput MapToDecimalOutput(DataSet dataSet)
        //{
        //    var response = new FetchTargetAchievementDetailsDecimalOutput
        //    {
        //        status = "200",
        //        message = "Successfully Get List.",
        //        value_for = Convert.ToString(dataSet.Tables[0].Rows[0]["value_for"]),
        //        target = Convert.ToDecimal(dataSet.Tables[0].Rows[0]["target"]),
        //        achv = Convert.ToDecimal(dataSet.Tables[0].Rows[0]["achv"]),
        //        achv_list = APIHelperMethods.ToModelList<AchvDecimallistOutput>(dataSet.Tables[1])
        //    };
        //    return response;
        //}
    }
}

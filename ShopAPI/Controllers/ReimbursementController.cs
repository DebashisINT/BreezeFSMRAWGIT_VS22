using ShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;

namespace ShopAPI.Controllers
{
    public class ReimbursementController : ApiController
    {

        LocationupdateOutput omodel = new LocationupdateOutput();
        ShopRegister oview = new ShopRegister();
        [HttpPost]

        public HttpResponseMessage Configuration(ReimbursementModelInput model)
        {

            ReimbursementModel oview = new ReimbursementModel();



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
                    String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                    String profileImg = System.Configuration.ConfigurationSettings.AppSettings["ProfileImageURL"];



                    DataSet dt = new DataSet();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();

                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();


                    sqlcmd = new SqlCommand("Proc_FTS_ConveyanceConfiguration", sqlcon);

                    sqlcmd.Parameters.Add("@UserID", model.user_id);
                    sqlcmd.Parameters.Add("@state_id", model.state_id);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();



                    if (dt.Tables[1].Rows.Count > 0)
                    {



                        List<visit_type_details> onview = new List<visit_type_details>();

                        for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                        {

                            List<string> termsList = new List<string>();

                            List<expense_details> expensedetails = new List<expense_details>();
                            for (int j = 0; j < dt.Tables[1].Rows.Count; j++)
                            {


                                int i1 = 0;
                                if (Convert.ToString(dt.Tables[1].Rows[j]["VisitlocId"]) == Convert.ToString(dt.Tables[0].Rows[i]["VisitlocId"]))
                                {

                                    var containsStringMatch = termsList.OfType<string>().Any(arg => arg == Convert.ToString(dt.Tables[1].Rows[j]["vehicle_id"]));
                                    if (containsStringMatch == false)
                                    {
                                        List<fuel_type> fuel_types = new List<fuel_type>();
                                        for (int k = 0; k < dt.Tables[2].Rows.Count; k++)
                                        {
                                            bool s = Convert.ToBoolean(dt.Tables[1].Rows[j]["fueladjust"]);
                                            if (Convert.ToString(dt.Tables[1].Rows[j]["vehicle_id"]) == Convert.ToString(dt.Tables[2].Rows[k]["vehicle_id"]) && Convert.ToString(dt.Tables[1].Rows[j]["VisitlocId"]) == Convert.ToString(dt.Tables[2].Rows[k]["VisitlocId"]) && s == true)
                                            {
                                                if (Convert.ToString(dt.Tables[2].Rows[k]["fuel_id"]) != "0")
                                                {
                                                    fuel_types.Add(new fuel_type()
                                                    {

                                                        fuel_id = Convert.ToString(dt.Tables[2].Rows[k]["fuel_id"]),
                                                        fuel_name = Convert.ToString(dt.Tables[2].Rows[k]["fuel_name"]),
                                                        maximum_allowance = Convert.ToString(dt.Tables[2].Rows[k]["maximum_allowance"]),
                                                        distance = Convert.ToString(dt.Tables[2].Rows[k]["distance"]),
                                                        rate = Convert.ToString(dt.Tables[2].Rows[k]["rate"])
                                                    });
                                                }

                                            }
                                        }

                                        termsList.Add(Convert.ToString(dt.Tables[1].Rows[j]["vehicle_id"]));

                                        expensedetails.Add(new expense_details()
                                        {

                                            expence_id = Convert.ToString(dt.Tables[1].Rows[j]["expence_id"]),
                                            expence_type = Convert.ToString(dt.Tables[1].Rows[j]["expence_type"]),
                                            vehicle_id = Convert.ToString(dt.Tables[1].Rows[j]["vehicle_id"]),
                                            vehicle_image = "",
                                            vehicle_name = Convert.ToString(dt.Tables[1].Rows[j]["vehicle_name"]),
                                            maximum_allowance = Convert.ToString(dt.Tables[1].Rows[j]["maximum_allowance"]),
                                            distance = Convert.ToString(dt.Tables[1].Rows[j]["distance"]),
                                            rate = Convert.ToString(dt.Tables[1].Rows[j]["rate"]),
                                            fuel_type = fuel_types
                                        });

                                    }
                                }


                            }

                            onview.Add(new visit_type_details()
                            {
                                expense_details = expensedetails,
                                visit_type_id = Convert.ToString(dt.Tables[0].Rows[i]["VisitlocId"]),
                                visit_type_name = Convert.ToString(dt.Tables[0].Rows[i]["VisitName"]),

                            });
                        }



                        oview.visit_type_details = onview;
                        oview.status = "200";
                        oview.message = "List populated.";


                    }
                    else
                    {
                        oview.status = "205";
                        oview.message = "No Data Found.";

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

        public HttpResponseMessage Configurationfetch(ReimbursementModelInput model)
        {
            ReimbursementModelOutput oview = new ReimbursementModelOutput();

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

                    String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                    String profileImg = System.Configuration.ConfigurationSettings.AppSettings["ProfileImageURL"];

                    DataSet dt = new DataSet();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();

                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();

                    sqlcmd = new SqlCommand("proc_Alltypesofconveyance", sqlcon);
                    sqlcmd.Parameters.Add("@UserID", model.user_id);
                    sqlcmd.Parameters.Add("@state_id", model.state_id);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    List<LocationDetails> modellocation = new List<LocationDetails>();
                    List<ExpenseDetails> modelexpense = new List<ExpenseDetails>();
                    List<Travelmodedetails> modeltravelmode = new List<Travelmodedetails>();

                    List<Fueldetails> modelfuele = new List<Fueldetails>();
                    if (dt.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                        {
                            modellocation.Add(new LocationDetails()
                            {
                                visittype_id = Convert.ToString(dt.Tables[0].Rows[i]["location_id"]),
                                visit_name = Convert.ToString(dt.Tables[0].Rows[i]["location_name"]),
                            });
                        }
                    }

                    if (dt.Tables[1].Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Tables[1].Rows.Count; i++)
                        {
                            modelexpense.Add(new ExpenseDetails()
                            {
                                expanse_id = Convert.ToString(dt.Tables[1].Rows[i]["expense_id"]),
                                expanse_type = Convert.ToString(dt.Tables[1].Rows[i]["expense_name"]),
                            });
                        }
                    }

                    if (dt.Tables[2].Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Tables[2].Rows.Count; i++)
                        {
                            modeltravelmode.Add(new Travelmodedetails()
                            {
                                travel_id = Convert.ToString(dt.Tables[2].Rows[i]["travel_id"]),
                                travel_type = Convert.ToString(dt.Tables[2].Rows[i]["travel_name"]),
                                expanse_id = Convert.ToString(dt.Tables[2].Rows[i]["expense_id"]),
                                fuel_config = Convert.ToBoolean(dt.Tables[2].Rows[i]["fuel_attach"])
                            });
                        }
                    }

                    if (dt.Tables[3].Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Tables[3].Rows.Count; i++)
                        {
                            modelfuele.Add(new Fueldetails()
                            {
                                fuel_type_id = Convert.ToString(dt.Tables[3].Rows[i]["fuel_id"]),
                                fuel_type = Convert.ToString(dt.Tables[3].Rows[i]["fuel_name"])
                            });
                        }
                    }
                    oview.visittype_details = modellocation;
                    oview.expense_types = modelexpense;
                    oview.mode_of_travel = modeltravelmode;
                    oview.fuel_types = modelfuele;
                    oview.status = "200";
                    oview.message = "List populated.";
                    if (dt.Tables[4].Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(dt.Tables[4].Rows[0]["dateapply"])))
                        {
                            oview.reimbursement_past_days = Convert.ToString(dt.Tables[4].Rows[0]["dateapply"]);
                        }
                        else
                        {
                            oview.reimbursement_past_days = "7";
                        }
                    }
                    else
                    {
                        oview.reimbursement_past_days = "7";
                    }
                    if (dt.Tables[5].Rows.Count > 0)
                    {
                        oview.isEditable = Convert.ToBoolean(dt.Tables[5].Rows[0]["VALES"]);
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


        public HttpResponseMessage ConfigurationFetchvalues(ReimbursementModelInputfetch model)
        {
            ReimbursementModelOutputfetch oview = new ReimbursementModelOutputfetch();

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
                    String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                    String profileImg = System.Configuration.ConfigurationSettings.AppSettings["ProfileImageURL"];



                    DataSet dt = new DataSet();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();

                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();


                    sqlcmd = new SqlCommand("proc_conveyance_Fetchvalue", sqlcon);
                    sqlcmd.Parameters.Add("@UserID", model.user_id);
                    sqlcmd.Parameters.Add("@state_id", model.state_id);
                    sqlcmd.Parameters.Add("@VisitType", model.visittype_id);
                    sqlcmd.Parameters.Add("@ExpenseID", model.expense_id);
                    sqlcmd.Parameters.Add("@TravelID", model.travel_id);
                    sqlcmd.Parameters.Add("@FuelID", model.fuel_id);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();


                    if (dt.Tables[0].Rows.Count > 0)
                    {




                        oview.distance = Convert.ToString(dt.Tables[0].Rows[0]["distance"]);
                        oview.maximum_allowance = Convert.ToString(dt.Tables[0].Rows[0]["maximum_allowance"]);
                        oview.rate = Convert.ToString(dt.Tables[0].Rows[0]["rate"]);

                        oview.status = "200";
                        oview.message = "List populated.";

                    }

                    else
                    {

                        oview.status = "205";
                        oview.message = "List not populated.";
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
        public HttpResponseMessage ReimbursementShop(reimburInput model)
        {
            reimburLocList oview = new reimburLocList();
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
                    String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                    String profileImg = System.Configuration.ConfigurationSettings.AppSettings["ProfileImageURL"];

                    DataSet dt = new DataSet();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();

                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();

                    sqlcmd = new SqlCommand("PRC_FTSReimbursementLocation", sqlcon);
                    sqlcmd.Parameters.Add("@UserID", model.user_id);
                    sqlcmd.Parameters.Add("@date", model.date);
                    sqlcmd.Parameters.Add("@isEditable", model.isEditable.ToUpper());
                    sqlcmd.Parameters.Add("@Expense_mapId", model.Expense_mapId);
                    sqlcmd.Parameters.Add("@Subexpense_MapId", model.Subexpense_MapId);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Tables[0].Rows.Count > 0)
                    {
                        List<reimburLocationList> onview = new List<reimburLocationList>();

                        for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                        {
                            onview.Add(new reimburLocationList()
                            {
                                loc_id = Convert.ToString(dt.Tables[0].Rows[i]["VisitId"]),
                                loc_name = Convert.ToString(dt.Tables[0].Rows[i]["location_name"]),
                                distance = Convert.ToString(dt.Tables[0].Rows[i]["distance_covered"]),
                            });
                        }

                        oview.loc_list = onview;
                        oview.status = "200";
                        oview.message = "Success";
                    }
                    else
                    {
                        oview.status = "205";
                        oview.message = "No Data Found.";
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
        public HttpResponseMessage LocationCaptureList(LocationCaptureInput model)
        {
            reimburLocationCapture oview = new reimburLocationCapture();
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
                    String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                    DataSet dt = new DataSet();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();

                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();

                    sqlcmd = new SqlCommand("PRC_FTSCaptureLocationList", sqlcon);
                    sqlcmd.Parameters.Add("@UserID", model.user_id);
                    sqlcmd.Parameters.Add("@date", model.date);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Tables[0].Rows.Count > 0)
                    {
                        List<reimburLocationCaptureList> onview = new List<reimburLocationCaptureList>();

                        for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                        {
                            onview.Add(new reimburLocationCaptureList()
                            {
                                loc_id = Convert.ToString(dt.Tables[0].Rows[i]["VisitId"]),
                                loc_name = Convert.ToString(dt.Tables[0].Rows[i]["location_name"]),
                            });
                        }

                        oview.loc_list = onview;
                        oview.status = "200";
                        oview.message = "Success";
                    }
                    else
                    {
                        oview.status = "205";
                        oview.message = "No Data Found.";
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
        //        "isEditable":true/false,
        //"Expense_mapId":"" , (optional)
        //"Subexpense_MapId":"" (optional)
    }
}

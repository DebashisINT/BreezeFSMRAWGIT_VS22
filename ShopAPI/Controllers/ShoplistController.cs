using ShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
//using System.Security.Cryptography.X509Certificates;
using System.Web.Http;


namespace ShopAPI.Controllers
{
    public class ShoplistController : ApiController
    {

        [HttpPost]
        //[RequireHttps]
        public HttpResponseMessage List(ShopslistInput model)
        {
            ShopslistOutput omodel = new ShopslistOutput();
            List<Shopslists> oview = new List<Shopslists>();
            Datalists odata = new Datalists();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                //X509Certificate2 certificate = Request.GetClientCertificate();
                //string user = certificate.Issuer;
                //string sub = certificate.Subject;

                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
                string DoctorDegree = System.Configuration.ConfigurationSettings.AppSettings["DoctorDegree"];
                string sessionId = "";

                List<Locationupdate> omedl2 = new List<Locationupdate>();

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("SP_API_Getshoplists", sqlcon);
                sqlcmd.Parameters.Add("@session_token", model.session_token);
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@Uniquecont", model.Uniquecont);
                sqlcmd.Parameters.Add("@Weburl", weburl);
                sqlcmd.Parameters.Add("@DoctorDegree", DoctorDegree);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<Shopslists>(dt);
                    odata.session_token = model.session_token;
                    odata.shop_list = oview;
                    omodel.status = "200";
                    omodel.message = dt.Rows.Count.ToString()+ " No. of Shop list available";

                    omodel.data = odata;

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
        public HttpResponseMessage AreaList(ArealistInput model)
        {
            ArealistsOutput omodel = new ArealistsOutput();
            List<Arealists> oview = new List<Arealists>();
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
                sqlcmd = new SqlCommand("SP_API_GetArealists", sqlcon);
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@city_id", model.city_id);
                sqlcmd.Parameters.Add("@creater_user_id", model.creater_user_id);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<Arealists>(dt);
                    omodel.area_list = oview;
                    omodel.status = "200";
                    omodel.message = "Successfully get area list.";
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
        public HttpResponseMessage ShopType(ShopTypelistInput model)
        {
            ShopTypelistsOutput omodel = new ShopTypelistsOutput();
            List<ShopType> oview = new List<ShopType>();
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
                sqlcmd = new SqlCommand("PRC_APIshoptype", sqlcon);
                sqlcmd.Parameters.Add("@User_id", model.user_id);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<ShopType>(dt);
                    omodel.Shoptype_list = oview;
                    omodel.status = "200";
                    omodel.message = "Successfully get shop type list.";
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
        public HttpResponseMessage AddShop(AddShopInput omm)
        {
            RegisterShopOutput omodel = new RegisterShopOutput();
            ShopRegister oview = new ShopRegister();

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
                sqlcmd = new SqlCommand("Sp_ApiShopRegister", sqlcon);
                sqlcmd.Parameters.Add("@session_token", omm.session_token);
                sqlcmd.Parameters.Add("@shop_name", omm.shop_name);
                sqlcmd.Parameters.Add("@address", omm.address);
                sqlcmd.Parameters.Add("@pin_code", omm.pin_code);
                sqlcmd.Parameters.Add("@shop_lat", omm.shop_lat);
                sqlcmd.Parameters.Add("@shop_long", omm.shop_long);
                sqlcmd.Parameters.Add("@owner_name", omm.owner_name);
                sqlcmd.Parameters.Add("@owner_contact_no", omm.owner_contact_no);
                sqlcmd.Parameters.Add("@owner_email", omm.owner_email);
                sqlcmd.Parameters.Add("@user_id", omm.user_id);
                sqlcmd.Parameters.Add("@type", omm.type);
                sqlcmd.Parameters.Add("@dob", Convert.ToString(omm.dob) == "" ? null : omm.dob);
                sqlcmd.Parameters.Add("@date_aniversary", Convert.ToString(omm.date_aniversary) == "" ? null : omm.date_aniversary);
                sqlcmd.Parameters.Add("@shop_id", omm.shop_id);
                sqlcmd.Parameters.Add("@added_date", omm.added_date);
                sqlcmd.Parameters.Add("@assigned_to_pp_id", omm.assigned_to_pp_id);
                sqlcmd.Parameters.Add("@assigned_to_dd_id", omm.assigned_to_dd_id);
                sqlcmd.Parameters.Add("@amount", omm.amount);
                sqlcmd.Parameters.Add("@family_member_dob", Convert.ToString(omm.family_member_dob) == "" ? null : omm.family_member_dob);
                sqlcmd.Parameters.Add("@addtional_dob", Convert.ToString(omm.addtional_dob) == "" ? null : omm.addtional_dob);
                sqlcmd.Parameters.Add("@addtional_doa", Convert.ToString(omm.addtional_doa) == "" ? null : omm.addtional_doa);
                sqlcmd.Parameters.Add("@director_name", omm.director_name);
                sqlcmd.Parameters.Add("@key_person_name", omm.key_person_name);
                sqlcmd.Parameters.Add("@phone_no", omm.phone_no);
                sqlcmd.Parameters.Add("@DOC_FAMILY_MEMBER_DOB", omm.doc_family_member_dob);
                sqlcmd.Parameters.Add("@SPECIALIZATION", omm.specialization);
                sqlcmd.Parameters.Add("@AVG_PATIENT_PER_DAY", omm.average_patient_per_day);
                sqlcmd.Parameters.Add("@CATEGORY", omm.category);
                sqlcmd.Parameters.Add("@DOC_ADDRESS", omm.doc_address);
                sqlcmd.Parameters.Add("@DOC_PINCODE", omm.doc_pincode);
                sqlcmd.Parameters.Add("@IsChamberSameHeadquarter", omm.is_chamber_same_headquarter);
                sqlcmd.Parameters.Add("@Remarks", omm.is_chamber_same_headquarter_remarks);
                sqlcmd.Parameters.Add("@CHEMIST_NAME", omm.chemist_name);
                sqlcmd.Parameters.Add("@CHEMIST_ADDRESS", omm.chemist_address);
                sqlcmd.Parameters.Add("@CHEMIST_PINCODE", omm.chemist_pincode);
                sqlcmd.Parameters.Add("@ASSISTANT_NAME", omm.assistant_name);
                sqlcmd.Parameters.Add("@ASSISTANT_CONTACT_NO", omm.assistant_contact_no);
                sqlcmd.Parameters.Add("@ASSISTANT_DOB", omm.assistant_dob);
                sqlcmd.Parameters.Add("@ASSISTANT_DOA", omm.assistant_doa);
                sqlcmd.Parameters.Add("@ASSISTANT_FAMILY_DOB", omm.assistant_family_dob);
                sqlcmd.Parameters.Add("@EntityCode", omm.EntityCode);
                sqlcmd.Parameters.Add("@Entity_Location", omm.Entity_Location);
                sqlcmd.Parameters.Add("@Alt_MobileNo", omm.Alt_MobileNo);
                sqlcmd.Parameters.Add("@Entity_Status", omm.Entity_Status);
                sqlcmd.Parameters.Add("@Entity_Type", omm.Entity_Type);
                sqlcmd.Parameters.Add("@ShopOwner_PAN", omm.ShopOwner_PAN);
                sqlcmd.Parameters.Add("@ShopOwner_Aadhar", omm.ShopOwner_Aadhar);
                sqlcmd.Parameters.Add("@EntityRemarks", omm.Remarks);
                sqlcmd.Parameters.Add("@AreaId", omm.area_id);
                sqlcmd.Parameters.Add("@CityId", omm.CityId);
                sqlcmd.Parameters.Add("@model_id", omm.model_id);
                sqlcmd.Parameters.Add("@primary_app_id", omm.primary_app_id);
                sqlcmd.Parameters.Add("@secondary_app_id", omm.secondary_app_id);
                sqlcmd.Parameters.Add("@lead_id", omm.lead_id);
                sqlcmd.Parameters.Add("@funnel_stage_id", omm.funnel_stage_id);
                sqlcmd.Parameters.Add("@stage_id", omm.stage_id);
                sqlcmd.Parameters.Add("@booking_amount", omm.booking_amount);
                sqlcmd.Parameters.Add("@PartyType_id", omm.type_id);
                sqlcmd.Parameters.Add("@entity_id", omm.entity_id);
                sqlcmd.Parameters.Add("@party_status_id", omm.party_status_id);
                sqlcmd.Parameters.Add("@retailer_id", omm.retailer_id);
                sqlcmd.Parameters.Add("@dealer_id", omm.dealer_id);
                sqlcmd.Parameters.Add("@beat_id", omm.beat_id);
                sqlcmd.Parameters.Add("@assigned_to_shop_id", omm.assigned_to_shop_id);
                sqlcmd.Parameters.Add("@actual_address", omm.actual_address);
                //Rev Debashis
                sqlcmd.Parameters.Add("@agency_name", omm.agency_name);
                sqlcmd.Parameters.Add("@lead_contact_number", omm.lead_contact_number);
                sqlcmd.Parameters.Add("@project_name", omm.project_name);
                sqlcmd.Parameters.Add("@landline_number", omm.landline_number);
                sqlcmd.Parameters.Add("@alternateNoForCustomer", omm.alternateNoForCustomer);
                sqlcmd.Parameters.Add("@whatsappNoForCustomer", omm.whatsappNoForCustomer);
                sqlcmd.Parameters.Add("@isShopDuplicate", omm.isShopDuplicate);
                sqlcmd.Parameters.Add("@purpose", omm.purpose);
                sqlcmd.Parameters.Add("@GSTN_Number", omm.GSTN_Number);
                //End of Rev Debashis

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToString(dt.Rows[0]["returncode"]) == "203")
                    {
                        omodel.status = "203";
                        omodel.message = "Duplicate shop Id or contact number";
                    }
                    else if (Convert.ToString(dt.Rows[0]["returncode"]) == "202")
                    {
                        omodel.status = "202";
                        omodel.message = "User or session token not matched";
                    }
                    else
                    {
                        oview = APIHelperMethods.ToModel<ShopRegister>(dt);
                        omodel.status = "200";
                        omodel.session_token = omm.session_token;
                        omodel.data = oview;
                        omodel.message = "Shop added successfully";
                    }
                }
                else
                {
                    omodel.status = "202";
                    omodel.message = "Data not inserted";
                }

                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage EditShop(AddShopInput omm)
        {
            RegisterShopOutput omodel = new RegisterShopOutput();
            ShopRegister oview = new ShopRegister();
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

                sqlcmd = new SqlCommand("Proc_FTSShopRegister_EDIT", sqlcon);
                sqlcmd.Parameters.Add("@session_token", omm.session_token);
                sqlcmd.Parameters.Add("@shop_name", omm.shop_name);
                sqlcmd.Parameters.Add("@address", omm.address);
                sqlcmd.Parameters.Add("@pin_code", omm.pin_code);
                sqlcmd.Parameters.Add("@shop_lat", omm.shop_lat);
                sqlcmd.Parameters.Add("@shop_long", omm.shop_long);
                sqlcmd.Parameters.Add("@owner_name", omm.owner_name);
                sqlcmd.Parameters.Add("@owner_contact_no", omm.owner_contact_no);
                sqlcmd.Parameters.Add("@owner_email", omm.owner_email);
                sqlcmd.Parameters.Add("@user_id", omm.user_id);
                sqlcmd.Parameters.Add("@type", omm.type);
                sqlcmd.Parameters.Add("@dob", Convert.ToString(omm.dob) == "" ? null : omm.dob);
                sqlcmd.Parameters.Add("@date_aniversary", Convert.ToString(omm.date_aniversary) == "" ? null : omm.date_aniversary);
                sqlcmd.Parameters.Add("@shop_id", omm.shop_id);
                sqlcmd.Parameters.Add("@added_date", omm.added_date);
                sqlcmd.Parameters.Add("@assigned_to_pp_id", omm.assigned_to_pp_id);
                sqlcmd.Parameters.Add("@assigned_to_dd_id", omm.assigned_to_dd_id);
                sqlcmd.Parameters.Add("@amount", omm.amount);
                sqlcmd.Parameters.Add("@family_member_dob", Convert.ToString(omm.family_member_dob) == "" ? null : omm.family_member_dob);
                sqlcmd.Parameters.Add("@addtional_dob", Convert.ToString(omm.addtional_dob) == "" ? null : omm.addtional_dob);
                sqlcmd.Parameters.Add("@addtional_doa", Convert.ToString(omm.addtional_doa) == "" ? null : omm.addtional_doa);
                sqlcmd.Parameters.Add("@director_name", omm.director_name);
                sqlcmd.Parameters.Add("@key_person_name", omm.key_person_name);
                sqlcmd.Parameters.Add("@phone_no", omm.phone_no);
                sqlcmd.Parameters.Add("@DOC_FAMILY_MEMBER_DOB", omm.doc_family_member_dob);
                sqlcmd.Parameters.Add("@SPECIALIZATION", omm.specialization);
                sqlcmd.Parameters.Add("@AVG_PATIENT_PER_DAY", omm.average_patient_per_day);
                sqlcmd.Parameters.Add("@CATEGORY", omm.category);
                sqlcmd.Parameters.Add("@DOC_ADDRESS", omm.doc_address);
                sqlcmd.Parameters.Add("@DOC_PINCODE", omm.doc_pincode);
                sqlcmd.Parameters.Add("@IsChamberSameHeadquarter", omm.is_chamber_same_headquarter);
                sqlcmd.Parameters.Add("@Remarks", omm.is_chamber_same_headquarter_remarks);
                sqlcmd.Parameters.Add("@CHEMIST_NAME", omm.chemist_name);
                sqlcmd.Parameters.Add("@CHEMIST_ADDRESS", omm.chemist_address);
                sqlcmd.Parameters.Add("@CHEMIST_PINCODE", omm.chemist_pincode);
                sqlcmd.Parameters.Add("@ASSISTANT_NAME", omm.assistant_name);
                sqlcmd.Parameters.Add("@ASSISTANT_CONTACT_NO", omm.assistant_contact_no);
                sqlcmd.Parameters.Add("@ASSISTANT_DOB", omm.assistant_dob);
                sqlcmd.Parameters.Add("@ASSISTANT_DOA", omm.assistant_doa);
                sqlcmd.Parameters.Add("@ASSISTANT_FAMILY_DOB", omm.assistant_family_dob);
                sqlcmd.Parameters.Add("@EntityCode", omm.EntityCode);
                sqlcmd.Parameters.Add("@Entity_Location", omm.Entity_Location);
                sqlcmd.Parameters.Add("@Alt_MobileNo", omm.Alt_MobileNo);
                sqlcmd.Parameters.Add("@Entity_Status", omm.Entity_Status);
                sqlcmd.Parameters.Add("@Entity_Type", omm.Entity_Type);
                sqlcmd.Parameters.Add("@ShopOwner_PAN", omm.ShopOwner_PAN);
                sqlcmd.Parameters.Add("@ShopOwner_Aadhar", omm.ShopOwner_Aadhar);
                sqlcmd.Parameters.Add("@EntityRemarks", omm.Remarks);
                sqlcmd.Parameters.Add("@AreaId", omm.area_id);
                sqlcmd.Parameters.Add("@CityId", omm.CityId);
                sqlcmd.Parameters.Add("@model_id", omm.model_id);
                sqlcmd.Parameters.Add("@primary_app_id", omm.primary_app_id);
                sqlcmd.Parameters.Add("@secondary_app_id", omm.secondary_app_id);
                sqlcmd.Parameters.Add("@lead_id", omm.lead_id);
                sqlcmd.Parameters.Add("@funnel_stage_id", omm.funnel_stage_id);
                sqlcmd.Parameters.Add("@stage_id", omm.stage_id);
                sqlcmd.Parameters.Add("@booking_amount", omm.booking_amount);
                sqlcmd.Parameters.Add("@PartyType_id", omm.type_id);
                sqlcmd.Parameters.Add("@entity_id", omm.entity_id);
                sqlcmd.Parameters.Add("@party_status_id", omm.party_status_id);
                sqlcmd.Parameters.Add("@retailer_id", omm.retailer_id);
                sqlcmd.Parameters.Add("@dealer_id", omm.dealer_id);
                sqlcmd.Parameters.Add("@beat_id", omm.beat_id);
                sqlcmd.Parameters.Add("@assigned_to_shop_id", omm.assigned_to_shop_id);
                //Rev Debashis
                sqlcmd.Parameters.Add("@agency_name", omm.agency_name);
                sqlcmd.Parameters.Add("@lead_contact_number", omm.lead_contact_number);
                sqlcmd.Parameters.Add("@project_name", omm.project_name);
                sqlcmd.Parameters.Add("@landline_number", omm.landline_number);
                sqlcmd.Parameters.Add("@alternateNoForCustomer", omm.alternateNoForCustomer);
                sqlcmd.Parameters.Add("@whatsappNoForCustomer", omm.whatsappNoForCustomer);
                sqlcmd.Parameters.Add("@shopStatusUpdate", omm.shopStatusUpdate);
                sqlcmd.Parameters.Add("@GSTN_Number", omm.GSTN_Number);
                //End of Rev Debashis

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToString(dt.Rows[0]["returncode"]) == "202")
                    {
                        omodel.status = "202";
                        omodel.message = "Shop_Id Not exists";
                    }
                    else if (Convert.ToString(dt.Rows[0]["returncode"]) == "203")
                    {
                        omodel.status = "203";
                        omodel.message = "Duplicate Shop Contact Number not allowed";
                    }
                    else
                    {
                        oview = APIHelperMethods.ToModel<ShopRegister>(dt);
                        omodel.status = "200";
                        omodel.session_token = omm.session_token;
                        omodel.data = oview;
                        omodel.message = "Shop Updated successfully";
                    }
                }
                else
                {
                    omodel.status = "202";
                    omodel.message = "Data not inserted";
                }
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage AllShopTypeWithSettings()
        {
            AllShopTypelistsOutput omodel = new AllShopTypelistsOutput();
            List<AllShopType> oview = new List<AllShopType>();
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
                sqlcmd = new SqlCommand("PRC_AllAPIshoptype", sqlcon);
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<AllShopType>(dt);
                    omodel.Shoptype_list = oview;
                    omodel.status = "200";
                    omodel.message = "Successfully get shop type list.";
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

        //Rev Debashis Row: 675
        [HttpPost]
        public HttpResponseMessage ShopActivityFeedbackList(ShopActivityFeedbackListInput model)
        {
            try
            {
                ShopActivityFeedbackListOutput omodel = new ShopActivityFeedbackListOutput();
                List<ShopActListOutput> Soview = new List<ShopActListOutput>();

                if (!ModelState.IsValid)
                {
                    omodel.status = "213";
                    omodel.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
                }
                else
                {
                    String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];

                    DataSet ds = new DataSet();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APIUSERSHOPACTIVITYSUBMITINFO", sqlcon);
                    sqlcmd.Parameters.Add("@ACTION", "FEEDBACKLIST");
                    sqlcmd.Parameters.Add("@USER_ID", model.user_id);
                    sqlcmd.Parameters.Add("@FROMDATE", model.from_date);
                    sqlcmd.Parameters.Add("@TODATE", model.to_date);
                    sqlcmd.Parameters.Add("@DATESPAN", model.date_span);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(ds);
                    sqlcon.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            List<ShopActFeedbackListOutput> Foview = new List<ShopActFeedbackListOutput>();
                            for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                            {
                                if (Convert.ToString(ds.Tables[0].Rows[i]["User_Id"]) == Convert.ToString(ds.Tables[1].Rows[j]["User_Id"]) &&
                                   Convert.ToString(ds.Tables[0].Rows[i]["Shop_Id"]) == Convert.ToString(ds.Tables[1].Rows[j]["Shop_Id"]))
                                {
                                    Foview.Add(new ShopActFeedbackListOutput()
                                    {
                                        feedback = Convert.ToString(ds.Tables[1].Rows[j]["feedback"]),
                                        date_time = Convert.ToString(ds.Tables[1].Rows[j]["date_time"])
                                    });
                                }
                            }

                            Soview.Add(new ShopActListOutput()
                            {
                                shop_id=Convert.ToString(ds.Tables[0].Rows[i]["Shop_Id"]),
                                feedback_remark_list=Foview
                            });
                        }

                        omodel.status = "200";
                        omodel.message = "Successfully Get List.";
                        omodel.user_id = model.user_id;
                        omodel.shop_list = Soview;
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
            catch (Exception ex)
            {
                var message = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                return message;
            }
        }
        //End of Rev Debashis Row: 675

        //Rev Debashis Row: 745
        [HttpPost]

        public HttpResponseMessage SupervisorTeamList(SupervisorTeamListInput model)
        {
            SupervisorTeamListOutput omodel = new SupervisorTeamListOutput();
            List<SupervisorTeamShopslists> oview = new List<SupervisorTeamShopslists>();
            SupervisorTeamDatalists odata = new SupervisorTeamDatalists();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
                string DoctorDegree = System.Configuration.ConfigurationSettings.AppSettings["DoctorDegree"];

                List<Locationupdate> omedl2 = new List<Locationupdate>();

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_APISUPERVISORTEAMSHOPLIST", sqlcon);
                sqlcmd.Parameters.Add("@SESSION_TOKEN", model.session_token);
                sqlcmd.Parameters.Add("@USER_ID", model.user_id);
                sqlcmd.Parameters.Add("@WEBURL", weburl);
                sqlcmd.Parameters.Add("@DOCTORDEGREE", DoctorDegree);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<SupervisorTeamShopslists>(dt);
                    odata.session_token = model.session_token;
                    odata.shop_list = oview;
                    omodel.status = "200";
                    omodel.message = dt.Rows.Count.ToString() + " No. of Shop list available";
                    omodel.data = odata;
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
        //End of Rev Debashis Row: 745

        //Rev Debashis Row: 761 to 765
        [HttpPost]
        public HttpResponseMessage ShopAttachmentImagesList(ShopAttachmentImageslistInput model)
        {
            ShopAttachmentImageslistOutput omodel = new ShopAttachmentImageslistOutput();
            List<ShopAttachmentImagesDatalists> oview = new List<ShopAttachmentImagesDatalists>();
            String APIHostingPort = System.Configuration.ConfigurationSettings.AppSettings["APIHostingPort"];

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
                sqlcmd = new SqlCommand("PRC_APISHOPATTACHMENTIMAGEINFO", sqlcon);
                sqlcmd.Parameters.Add("@Action", "FETCHATTACHMENTIMAGES");
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@SHOP_ID", model.shop_id);
                sqlcmd.Parameters.Add("@APIHostingPort", APIHostingPort);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {                   
                    omodel.status = "200";
                    omodel.message = "Successfully Get List.";
                    omodel.user_id = model.user_id;
                    omodel.shop_id = model.shop_id;
                    oview = APIHelperMethods.ToModelList<ShopAttachmentImagesDatalists>(dt);
                    omodel.image_list = oview;
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
        //End of Rev Debashis Row: 761 to 765
    }
}

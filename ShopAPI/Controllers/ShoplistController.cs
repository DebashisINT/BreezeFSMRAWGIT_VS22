#region======================================Revision History=========================================================
//1.0   V2.0.38     Debashis    02/02/2023      Two new methods have been added.Row: 810 to 811
//2.0   V2.0.39     Debashis    24/04/2023      One new method has been added.Row: 822
//3.0   V2.0.40     Debashis    30/06/2023      One new method has been added.Row: 852
//4.0   V2.0.42     Debashis    06/10/2023      One new parameter has been added.Row: 867 & 873
//5.0   V2.0.43     Debashis    22/12/2023      Some new parameters have been added.Row: 892 & 895
//6.0   V2.0.45     Debashis    14/03/2024      One new method has been added.Row: 902 & Refer: 0027309
//7.0   V2.0.45     Debashis    03/04/2024      One new parameter has been added.Row: 914
//8.0   V2.0.45     Debashis    11/04/2024      One new parameter has been added.Row: 917 & 918
//9.0   V2.0.46     Debashis    24/04/2024      One new parameter has been added.Row: 923
//10.0  V2.0.48     Debashis    05/08/2024      One new method has been added.Row: 965
#endregion===================================End of Revision History==================================================
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

                String token = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];
                String weburl = System.Configuration.ConfigurationManager.AppSettings["SiteURL"];
                string DoctorDegree = System.Configuration.ConfigurationManager.AppSettings["DoctorDegree"];
                string sessionId = "";

                List<Locationupdate> omedl2 = new List<Locationupdate>();

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("SP_API_Getshoplists", sqlcon);
                sqlcmd.Parameters.AddWithValue("@session_token", model.session_token);
                sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);
                sqlcmd.Parameters.AddWithValue("@Uniquecont", model.Uniquecont);
                sqlcmd.Parameters.AddWithValue("@Weburl", weburl);
                sqlcmd.Parameters.AddWithValue("@DoctorDegree", DoctorDegree);

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
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("SP_API_GetArealists", sqlcon);
                sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);
                sqlcmd.Parameters.AddWithValue("@city_id", model.city_id);
                sqlcmd.Parameters.AddWithValue("@creater_user_id", model.creater_user_id);

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
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_APIshoptype", sqlcon);
                sqlcmd.Parameters.AddWithValue("@User_id", model.user_id);
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
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("Sp_ApiShopRegister", sqlcon);
                sqlcmd.Parameters.AddWithValue("@session_token", omm.session_token);
                sqlcmd.Parameters.AddWithValue("@shop_name", omm.shop_name);
                sqlcmd.Parameters.AddWithValue("@address", omm.address);
                sqlcmd.Parameters.AddWithValue("@pin_code", omm.pin_code);
                sqlcmd.Parameters.AddWithValue("@shop_lat", omm.shop_lat);
                sqlcmd.Parameters.AddWithValue("@shop_long", omm.shop_long);
                sqlcmd.Parameters.AddWithValue("@owner_name", omm.owner_name);
                sqlcmd.Parameters.AddWithValue("@owner_contact_no", omm.owner_contact_no);
                sqlcmd.Parameters.AddWithValue("@owner_email", omm.owner_email);
                sqlcmd.Parameters.AddWithValue("@user_id", omm.user_id);
                sqlcmd.Parameters.AddWithValue("@type", omm.type);
                sqlcmd.Parameters.AddWithValue("@dob", Convert.ToString(omm.dob) == "" ? null : omm.dob);
                sqlcmd.Parameters.AddWithValue("@date_aniversary", Convert.ToString(omm.date_aniversary) == "" ? null : omm.date_aniversary);
                sqlcmd.Parameters.AddWithValue("@shop_id", omm.shop_id);
                sqlcmd.Parameters.AddWithValue("@added_date", omm.added_date);
                sqlcmd.Parameters.AddWithValue("@assigned_to_pp_id", omm.assigned_to_pp_id);
                sqlcmd.Parameters.AddWithValue("@assigned_to_dd_id", omm.assigned_to_dd_id);
                sqlcmd.Parameters.AddWithValue("@amount", omm.amount);
                sqlcmd.Parameters.AddWithValue("@family_member_dob", Convert.ToString(omm.family_member_dob) == "" ? null : omm.family_member_dob);
                sqlcmd.Parameters.AddWithValue("@addtional_dob", Convert.ToString(omm.addtional_dob) == "" ? null : omm.addtional_dob);
                sqlcmd.Parameters.AddWithValue("@addtional_doa", Convert.ToString(omm.addtional_doa) == "" ? null : omm.addtional_doa);
                sqlcmd.Parameters.AddWithValue("@director_name", omm.director_name);
                sqlcmd.Parameters.AddWithValue("@key_person_name", omm.key_person_name);
                sqlcmd.Parameters.AddWithValue("@phone_no", omm.phone_no);
                sqlcmd.Parameters.AddWithValue("@DOC_FAMILY_MEMBER_DOB", omm.doc_family_member_dob);
                sqlcmd.Parameters.AddWithValue("@SPECIALIZATION", omm.specialization);
                sqlcmd.Parameters.AddWithValue("@AVG_PATIENT_PER_DAY", omm.average_patient_per_day);
                sqlcmd.Parameters.AddWithValue("@CATEGORY", omm.category);
                sqlcmd.Parameters.AddWithValue("@DOC_ADDRESS", omm.doc_address);
                sqlcmd.Parameters.AddWithValue("@DOC_PINCODE", omm.doc_pincode);
                sqlcmd.Parameters.AddWithValue("@IsChamberSameHeadquarter", omm.is_chamber_same_headquarter);
                sqlcmd.Parameters.AddWithValue("@Remarks", omm.is_chamber_same_headquarter_remarks);
                sqlcmd.Parameters.AddWithValue("@CHEMIST_NAME", omm.chemist_name);
                sqlcmd.Parameters.AddWithValue("@CHEMIST_ADDRESS", omm.chemist_address);
                sqlcmd.Parameters.AddWithValue("@CHEMIST_PINCODE", omm.chemist_pincode);
                sqlcmd.Parameters.AddWithValue("@ASSISTANT_NAME", omm.assistant_name);
                sqlcmd.Parameters.AddWithValue("@ASSISTANT_CONTACT_NO", omm.assistant_contact_no);
                sqlcmd.Parameters.AddWithValue("@ASSISTANT_DOB", omm.assistant_dob);
                sqlcmd.Parameters.AddWithValue("@ASSISTANT_DOA", omm.assistant_doa);
                sqlcmd.Parameters.AddWithValue("@ASSISTANT_FAMILY_DOB", omm.assistant_family_dob);
                sqlcmd.Parameters.AddWithValue("@EntityCode", omm.EntityCode);
                sqlcmd.Parameters.AddWithValue("@Entity_Location", omm.Entity_Location);
                sqlcmd.Parameters.AddWithValue("@Alt_MobileNo", omm.Alt_MobileNo);
                sqlcmd.Parameters.AddWithValue("@Entity_Status", omm.Entity_Status);
                sqlcmd.Parameters.AddWithValue("@Entity_Type", omm.Entity_Type);
                sqlcmd.Parameters.AddWithValue("@ShopOwner_PAN", omm.ShopOwner_PAN);
                sqlcmd.Parameters.AddWithValue("@ShopOwner_Aadhar", omm.ShopOwner_Aadhar);
                sqlcmd.Parameters.AddWithValue("@EntityRemarks", omm.Remarks);
                sqlcmd.Parameters.AddWithValue("@AreaId", omm.area_id);
                sqlcmd.Parameters.AddWithValue("@CityId", omm.CityId);
                sqlcmd.Parameters.AddWithValue("@model_id", omm.model_id);
                sqlcmd.Parameters.AddWithValue("@primary_app_id", omm.primary_app_id);
                sqlcmd.Parameters.AddWithValue("@secondary_app_id", omm.secondary_app_id);
                sqlcmd.Parameters.AddWithValue("@lead_id", omm.lead_id);
                sqlcmd.Parameters.AddWithValue("@funnel_stage_id", omm.funnel_stage_id);
                sqlcmd.Parameters.AddWithValue("@stage_id", omm.stage_id);
                sqlcmd.Parameters.AddWithValue("@booking_amount", omm.booking_amount);
                sqlcmd.Parameters.AddWithValue("@PartyType_id", omm.type_id);
                sqlcmd.Parameters.AddWithValue("@entity_id", omm.entity_id);
                sqlcmd.Parameters.AddWithValue("@party_status_id", omm.party_status_id);
                sqlcmd.Parameters.AddWithValue("@retailer_id", omm.retailer_id);
                sqlcmd.Parameters.AddWithValue("@dealer_id", omm.dealer_id);
                sqlcmd.Parameters.AddWithValue("@beat_id", omm.beat_id);
                sqlcmd.Parameters.AddWithValue("@assigned_to_shop_id", omm.assigned_to_shop_id);
                sqlcmd.Parameters.AddWithValue("@actual_address", omm.actual_address);
                //Rev Debashis
                sqlcmd.Parameters.AddWithValue("@agency_name", omm.agency_name);
                sqlcmd.Parameters.AddWithValue("@lead_contact_number", omm.lead_contact_number);
                sqlcmd.Parameters.AddWithValue("@project_name", omm.project_name);
                sqlcmd.Parameters.AddWithValue("@landline_number", omm.landline_number);
                sqlcmd.Parameters.AddWithValue("@alternateNoForCustomer", omm.alternateNoForCustomer);
                sqlcmd.Parameters.AddWithValue("@whatsappNoForCustomer", omm.whatsappNoForCustomer);
                sqlcmd.Parameters.AddWithValue("@isShopDuplicate", omm.isShopDuplicate);
                sqlcmd.Parameters.AddWithValue("@purpose", omm.purpose);
                sqlcmd.Parameters.AddWithValue("@GSTN_Number", omm.GSTN_Number);
                //End of Rev Debashis
                //Rev 4.0 Row: 867
                sqlcmd.Parameters.AddWithValue("@FSSAILicNo", omm.FSSAILicNo);
                //End of Rev 4.0 Row: 867
                //Rev 5.0 Row: 892
                sqlcmd.Parameters.AddWithValue("@shop_firstName", omm.shop_firstName);
                sqlcmd.Parameters.AddWithValue("@shop_lastName", omm.shop_lastName);
                sqlcmd.Parameters.AddWithValue("@crm_companyID", omm.crm_companyID);
                sqlcmd.Parameters.AddWithValue("@crm_jobTitle", omm.crm_jobTitle);
                sqlcmd.Parameters.AddWithValue("@crm_typeID", omm.crm_typeID);
                sqlcmd.Parameters.AddWithValue("@crm_statusID", omm.crm_statusID);
                sqlcmd.Parameters.AddWithValue("@crm_sourceID", omm.crm_sourceID);
                sqlcmd.Parameters.AddWithValue("@crm_referenceID", omm.crm_referenceID);
                sqlcmd.Parameters.AddWithValue("@crm_referenceID_type", omm.crm_referenceID_type);
                sqlcmd.Parameters.AddWithValue("@crm_stage_ID", omm.crm_stage_ID);
                sqlcmd.Parameters.AddWithValue("@assign_to", omm.assign_to);
                sqlcmd.Parameters.AddWithValue("@saved_from_status", omm.saved_from_status);
                //End of Rev 5.0 Row: 892
                //Rev 8.0 Row: 917
                sqlcmd.Parameters.AddWithValue("@isFromCRM", omm.isFromCRM);
                //End of Rev 8.0 Row: 917
                //Rev 9.0 Row: 923
                sqlcmd.Parameters.AddWithValue("@Shop_NextFollowupDate", omm.Shop_NextFollowupDate);
                //End of Rev 9.0 Row: 923

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
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();

                sqlcmd = new SqlCommand("Proc_FTSShopRegister_EDIT", sqlcon);
                sqlcmd.Parameters.AddWithValue("@session_token", omm.session_token);
                sqlcmd.Parameters.AddWithValue("@shop_name", omm.shop_name);
                sqlcmd.Parameters.AddWithValue("@address", omm.address);
                sqlcmd.Parameters.AddWithValue("@pin_code", omm.pin_code);
                sqlcmd.Parameters.AddWithValue("@shop_lat", omm.shop_lat);
                sqlcmd.Parameters.AddWithValue("@shop_long", omm.shop_long);
                sqlcmd.Parameters.AddWithValue("@owner_name", omm.owner_name);
                sqlcmd.Parameters.AddWithValue("@owner_contact_no", omm.owner_contact_no);
                sqlcmd.Parameters.AddWithValue("@owner_email", omm.owner_email);
                sqlcmd.Parameters.AddWithValue("@user_id", omm.user_id);
                sqlcmd.Parameters.AddWithValue("@type", omm.type);
                sqlcmd.Parameters.AddWithValue("@dob", Convert.ToString(omm.dob) == "" ? null : omm.dob);
                sqlcmd.Parameters.AddWithValue("@date_aniversary", Convert.ToString(omm.date_aniversary) == "" ? null : omm.date_aniversary);
                sqlcmd.Parameters.AddWithValue("@shop_id", omm.shop_id);
                sqlcmd.Parameters.AddWithValue("@added_date", omm.added_date);
                sqlcmd.Parameters.AddWithValue("@assigned_to_pp_id", omm.assigned_to_pp_id);
                sqlcmd.Parameters.AddWithValue("@assigned_to_dd_id", omm.assigned_to_dd_id);
                sqlcmd.Parameters.AddWithValue("@amount", omm.amount);
                sqlcmd.Parameters.AddWithValue("@family_member_dob", Convert.ToString(omm.family_member_dob) == "" ? null : omm.family_member_dob);
                sqlcmd.Parameters.AddWithValue("@addtional_dob", Convert.ToString(omm.addtional_dob) == "" ? null : omm.addtional_dob);
                sqlcmd.Parameters.AddWithValue("@addtional_doa", Convert.ToString(omm.addtional_doa) == "" ? null : omm.addtional_doa);
                sqlcmd.Parameters.AddWithValue("@director_name", omm.director_name);
                sqlcmd.Parameters.AddWithValue("@key_person_name", omm.key_person_name);
                sqlcmd.Parameters.AddWithValue("@phone_no", omm.phone_no);
                sqlcmd.Parameters.AddWithValue("@DOC_FAMILY_MEMBER_DOB", omm.doc_family_member_dob);
                sqlcmd.Parameters.AddWithValue("@SPECIALIZATION", omm.specialization);
                sqlcmd.Parameters.AddWithValue("@AVG_PATIENT_PER_DAY", omm.average_patient_per_day);
                sqlcmd.Parameters.AddWithValue("@CATEGORY", omm.category);
                sqlcmd.Parameters.AddWithValue("@DOC_ADDRESS", omm.doc_address);
                sqlcmd.Parameters.AddWithValue("@DOC_PINCODE", omm.doc_pincode);
                sqlcmd.Parameters.AddWithValue("@IsChamberSameHeadquarter", omm.is_chamber_same_headquarter);
                sqlcmd.Parameters.AddWithValue("@Remarks", omm.is_chamber_same_headquarter_remarks);
                sqlcmd.Parameters.AddWithValue("@CHEMIST_NAME", omm.chemist_name);
                sqlcmd.Parameters.AddWithValue("@CHEMIST_ADDRESS", omm.chemist_address);
                sqlcmd.Parameters.AddWithValue("@CHEMIST_PINCODE", omm.chemist_pincode);
                sqlcmd.Parameters.AddWithValue("@ASSISTANT_NAME", omm.assistant_name);
                sqlcmd.Parameters.AddWithValue("@ASSISTANT_CONTACT_NO", omm.assistant_contact_no);
                sqlcmd.Parameters.AddWithValue("@ASSISTANT_DOB", omm.assistant_dob);
                sqlcmd.Parameters.AddWithValue("@ASSISTANT_DOA", omm.assistant_doa);
                sqlcmd.Parameters.AddWithValue("@ASSISTANT_FAMILY_DOB", omm.assistant_family_dob);
                sqlcmd.Parameters.AddWithValue("@EntityCode", omm.EntityCode);
                sqlcmd.Parameters.AddWithValue("@Entity_Location", omm.Entity_Location);
                sqlcmd.Parameters.AddWithValue("@Alt_MobileNo", omm.Alt_MobileNo);
                sqlcmd.Parameters.AddWithValue("@Entity_Status", omm.Entity_Status);
                sqlcmd.Parameters.AddWithValue("@Entity_Type", omm.Entity_Type);
                sqlcmd.Parameters.AddWithValue("@ShopOwner_PAN", omm.ShopOwner_PAN);
                sqlcmd.Parameters.AddWithValue("@ShopOwner_Aadhar", omm.ShopOwner_Aadhar);
                sqlcmd.Parameters.AddWithValue("@EntityRemarks", omm.Remarks);
                sqlcmd.Parameters.AddWithValue("@AreaId", omm.area_id);
                sqlcmd.Parameters.AddWithValue("@CityId", omm.CityId);
                sqlcmd.Parameters.AddWithValue("@model_id", omm.model_id);
                sqlcmd.Parameters.AddWithValue("@primary_app_id", omm.primary_app_id);
                sqlcmd.Parameters.AddWithValue("@secondary_app_id", omm.secondary_app_id);
                sqlcmd.Parameters.AddWithValue("@lead_id", omm.lead_id);
                sqlcmd.Parameters.AddWithValue("@funnel_stage_id", omm.funnel_stage_id);
                sqlcmd.Parameters.AddWithValue("@stage_id", omm.stage_id);
                sqlcmd.Parameters.AddWithValue("@booking_amount", omm.booking_amount);
                sqlcmd.Parameters.AddWithValue("@PartyType_id", omm.type_id);
                sqlcmd.Parameters.AddWithValue("@entity_id", omm.entity_id);
                sqlcmd.Parameters.AddWithValue("@party_status_id", omm.party_status_id);
                sqlcmd.Parameters.AddWithValue("@retailer_id", omm.retailer_id);
                sqlcmd.Parameters.AddWithValue("@dealer_id", omm.dealer_id);
                sqlcmd.Parameters.AddWithValue("@beat_id", omm.beat_id);
                sqlcmd.Parameters.AddWithValue("@assigned_to_shop_id", omm.assigned_to_shop_id);
                //Rev Debashis
                sqlcmd.Parameters.AddWithValue("@agency_name", omm.agency_name);
                sqlcmd.Parameters.AddWithValue("@lead_contact_number", omm.lead_contact_number);
                sqlcmd.Parameters.AddWithValue("@project_name", omm.project_name);
                sqlcmd.Parameters.AddWithValue("@landline_number", omm.landline_number);
                sqlcmd.Parameters.AddWithValue("@alternateNoForCustomer", omm.alternateNoForCustomer);
                sqlcmd.Parameters.AddWithValue("@whatsappNoForCustomer", omm.whatsappNoForCustomer);
                sqlcmd.Parameters.AddWithValue("@shopStatusUpdate", omm.shopStatusUpdate);
                sqlcmd.Parameters.AddWithValue("@GSTN_Number", omm.GSTN_Number);
                //End of Rev Debashis
                //Rev 4.0 Row: 873
                sqlcmd.Parameters.AddWithValue("@isUpdateAddressFromShopMaster", omm.isUpdateAddressFromShopMaster);
                //End of Rev 4.0 Row: 873
                //Rev 5.0 Row: 895
                sqlcmd.Parameters.AddWithValue("@shop_firstName", omm.shop_firstName);
                sqlcmd.Parameters.AddWithValue("@shop_lastName", omm.shop_lastName);
                sqlcmd.Parameters.AddWithValue("@crm_companyID", omm.crm_companyID);
                sqlcmd.Parameters.AddWithValue("@crm_jobTitle", omm.crm_jobTitle);
                sqlcmd.Parameters.AddWithValue("@crm_typeID", omm.crm_typeID);
                sqlcmd.Parameters.AddWithValue("@crm_statusID", omm.crm_statusID);
                sqlcmd.Parameters.AddWithValue("@crm_sourceID", omm.crm_sourceID);
                sqlcmd.Parameters.AddWithValue("@crm_referenceID", omm.crm_referenceID);
                sqlcmd.Parameters.AddWithValue("@crm_referenceID_type", omm.crm_referenceID_type);
                sqlcmd.Parameters.AddWithValue("@crm_stage_ID", omm.crm_stage_ID);
                sqlcmd.Parameters.AddWithValue("@assign_to", omm.assign_to);
                sqlcmd.Parameters.AddWithValue("@saved_from_status", omm.saved_from_status);
                //End of Rev 5.0 Row: 895
                //Rev 8.0 Row: 918
                sqlcmd.Parameters.AddWithValue("@isFromCRM", omm.isFromCRM);
                //End of Rev 8.0 Row: 918

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
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
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
                    String token = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];

                    DataSet ds = new DataSet();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APIUSERSHOPACTIVITYSUBMITINFO", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "FEEDBACKLIST");
                    sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@FROMDATE", model.from_date);
                    sqlcmd.Parameters.AddWithValue("@TODATE", model.to_date);
                    sqlcmd.Parameters.AddWithValue("@DATESPAN", model.date_span);

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
                                        date_time = Convert.ToString(ds.Tables[1].Rows[j]["date_time"]),
                                        multi_contact_name = Convert.ToString(ds.Tables[1].Rows[j]["multi_contact_name"]),
                                        multi_contact_number = Convert.ToString(ds.Tables[1].Rows[j]["multi_contact_number"])
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
                String token = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];
                String weburl = System.Configuration.ConfigurationManager.AppSettings["SiteURL"];
                string DoctorDegree = System.Configuration.ConfigurationManager.AppSettings["DoctorDegree"];

                List<Locationupdate> omedl2 = new List<Locationupdate>();

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_APISUPERVISORTEAMSHOPLIST", sqlcon);
                sqlcmd.Parameters.AddWithValue("@SESSION_TOKEN", model.session_token);
                sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                sqlcmd.Parameters.AddWithValue("@WEBURL", weburl);
                sqlcmd.Parameters.AddWithValue("@DOCTORDEGREE", DoctorDegree);

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
            String APIHostingPort = System.Configuration.ConfigurationManager.AppSettings["APIHostingPort"];

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_APISHOPATTACHMENTIMAGEINFO", sqlcon);
                sqlcmd.Parameters.AddWithValue("@Action", "FETCHATTACHMENTIMAGES");
                sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);
                sqlcmd.Parameters.AddWithValue("@SHOP_ID", model.shop_id);
                sqlcmd.Parameters.AddWithValue("@APIHostingPort", APIHostingPort);

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

        //Rev 1.0 Row: 810 to 811
        [HttpPost]
        public HttpResponseMessage ModifiedShopLists(ModifiedShopslistInput model)
        {
            ModifiedShopslistOutput omodel = new ModifiedShopslistOutput();
            List<ModifiedShopslists> oview = new List<ModifiedShopslists>();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                String token = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];
                String weburl = System.Configuration.ConfigurationManager.AppSettings["SiteURL"];
                string DoctorDegree = System.Configuration.ConfigurationManager.AppSettings["DoctorDegree"];

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_APIGETMODIFIEDSHOPLISTS", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "GETSHOPLISTS");
                sqlcmd.Parameters.AddWithValue("@User_id", model.user_id);
                sqlcmd.Parameters.AddWithValue("@Weburl", weburl);
                sqlcmd.Parameters.AddWithValue("@DoctorDegree", DoctorDegree);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<ModifiedShopslists>(dt);
                    omodel.status = "200";
                    omodel.message = "Success.";
                    omodel.user_id = model.user_id;
                    omodel.modified_shop_list= oview;
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
        public HttpResponseMessage EditModifiedShop(EditModifiedShopInput model)
        {
            EditModifiedShopOutput omodel = new EditModifiedShopOutput();
            List<EditModifiedShopList> omedl2 = new List<EditModifiedShopList>();
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
                    String token = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];

                    foreach (var s2 in model.shop_modified_list)
                    {
                        omedl2.Add(new EditModifiedShopList()
                        {
                            shop_id = s2.shop_id
                        });
                    }

                    string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcmd.CommandTimeout = 60;
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APIGETMODIFIEDSHOPLISTS", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "EDITMODIFIEDSHOP");
                    sqlcmd.Parameters.AddWithValue("@User_id", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@JsonXML", JsonXML);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0 && Convert.ToString(dt.Rows[0]["STRMESSAGE"]) == "Success")
                    {
                        omodel.status = "200";
                        omodel.message = "Update Successfully.";
                    }
                    else
                    {
                        omodel.status = "205";
                        omodel.message = "Records not updated.";
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
        //End of Rev 1.0 Row: 810 to 811
        //Rev 2.0 Row: 822
        [HttpPost]
        public HttpResponseMessage PartyNotVisitedList(PartyNotVisitListInput model)
        {
            PartyNotVisitListOutput omodel = new PartyNotVisitListOutput();
            List<PartyNotVisitLists> oview = new List<PartyNotVisitLists>();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_APIDASHBOARDANALYTICS", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "PARTYNOTVISITLIST");
                sqlcmd.Parameters.AddWithValue("@USERID", model.user_id);
                sqlcmd.Parameters.AddWithValue("@FROMDATE", model.from_date);
                sqlcmd.Parameters.AddWithValue("@TODATE", model.to_date);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<PartyNotVisitLists>(dt);
                    omodel.last_visit_order_list = oview;
                    omodel.status = "200";
                    omodel.message = "Success.";
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
        //End of Rev 2.0 Row: 822
        //Rev 3.0 Row: 852
        [HttpPost]
        public HttpResponseMessage ShopInactiveList(ShopsInactivelistInput model)
        {
            ShopsInactivelistOutput omodel = new ShopsInactivelistOutput();
            List<ShopsInactivelists> oview = new List<ShopsInactivelists>();
            InactiveDatalists odata = new InactiveDatalists();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                String token = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];
                String weburl = System.Configuration.ConfigurationManager.AppSettings["SiteURL"];
                string DoctorDegree = System.Configuration.ConfigurationManager.AppSettings["DoctorDegree"];

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_APISHOPACTIVEINACTIVE", sqlcon);
                sqlcmd.Parameters.AddWithValue("@session_token", model.session_token);
                sqlcmd.Parameters.AddWithValue("@USERID", model.user_id);
                sqlcmd.Parameters.AddWithValue("@Weburl", weburl);
                sqlcmd.Parameters.AddWithValue("@DoctorDegree", DoctorDegree);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<ShopsInactivelists>(dt);
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
        //End of Rev 3.0 Row: 852

        //Rev 6.0 Row: 902 & Refer: 0027309
        [HttpPost]
        public HttpResponseMessage ITCShopAddressEdit(ITCShopAddressEditInput model)
        {
            ITCShopAddressEditOutput omodel = new ITCShopAddressEditOutput();
            List<ITCAddressEditShopList> omedl2 = new List<ITCAddressEditShopList>();

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
                    String token = System.Configuration.ConfigurationManager.AppSettings["AuthToken"];

                    foreach (var s2 in model.shop_list)
                    {
                        omedl2.Add(new ITCAddressEditShopList()
                        {
                            shop_id = s2.shop_id,
                            shop_updated_lat= s2.shop_updated_lat,
                            shop_updated_long= s2.shop_updated_long,
                            shop_updated_address= s2.shop_updated_address,
                            //Rev 7.0 Row: 914
                            pincode= s2.pincode
                            //End of Rev 7.0 Row: 914
                        });
                    }

                    string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);
                    DataTable dt = new DataTable();
                    String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcmd.CommandTimeout = 60;
                    sqlcon.Open();
                    sqlcmd = new SqlCommand("PRC_APIGETMODIFIEDSHOPLISTS", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "SHOPADDRESSEDIT");
                    sqlcmd.Parameters.AddWithValue("@User_id", model.user_id);
                    sqlcmd.Parameters.AddWithValue("@JsonXML", JsonXML);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();

                    if (dt.Rows.Count > 0 && Convert.ToString(dt.Rows[0]["STRMESSAGE"]) == "Success")
                    {
                        omodel.status = "200";
                        omodel.message = "Update Successfully.";
                    }
                    else
                    {
                        omodel.status = "205";
                        omodel.message = "Records not updated.";
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
        //End of Rev 6.0 Row: 902 & Refer: 0027309

        //Rev 10.0 Row: 965
        [HttpPost]
        public HttpResponseMessage FetchShopRevisitAudio(FetchShopRevisitAudioInput model)
        {
            FetchShopRevisitAudioOutput omodel = new FetchShopRevisitAudioOutput();
            List<Audio_listOutput> Cview = new List<Audio_listOutput>();

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
                    sqlcmd = new SqlCommand("PRC_APISHOPREVISITAUDIOINFO", sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ACTION", "FETCHSHOPAUDIO");
                    sqlcmd.Parameters.AddWithValue("@USER_ID", model.user_id);
                    sqlcmd.Parameters.AddWithValue("DATALIMITINDAYS", model.data_limit_in_days);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(ds);
                    sqlcon.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        omodel.status = "200";
                        omodel.message = "Successfully Get List.";
                        Cview = APIHelperMethods.ToModelList<Audio_listOutput>(ds.Tables[0]);
                        omodel.audio_list = Cview;
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
        //End of Rev 10.0 Row: 965
    }
}

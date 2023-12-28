#region======================================Revision History=========================================================
//1.0   V2.0.42     Debashis    06/10/2023      One new parameter has been added.Row: 868,869,874 & 875
//2.0   V2.0.43     Debashis    22/12/2023      Some new parameters have been added.Row: 893,894,896 & 897
#endregion===================================End of Revision History==================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
//using System.Net.Http;
//using System.Web.Http;
using ShopAPI.Models;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using System.Net.Http.Formatting;
using System.Web.UI;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml.Linq;
using System.Text;
//using System.Web.Http;
//using System.Net.Http;
//using System.Threading.Tasks;

namespace ShopAPI.Controllers
{

    public class ShopRegistrationController : Controller
    {
        string uploadtext = "~/CommonFolder/Log/ShopRegistration.txt";
        [AcceptVerbs("POST")]

        public JsonResult RegisterShop(RegisterShopInputData model)
        {
            // TextWriter txt = new StreamWriter(Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(uploadtext)));

            RegisterShopOutput omodel = new RegisterShopOutput();
            RegisterShopInput omm = new RegisterShopInput();
            string ImageName = "";
            try
            {
                // close the stream

                ShopRegister oview = new ShopRegister();
                ImageName = model.shop_image.FileName;
                string UploadFileDirectory = "~/CommonFolder";
                try
                {

                    var details = JObject.Parse(model.data);

                    foreach (var item in details)
                    {
                        string param = item.Key;
                        string value = Convert.ToString(item.Value);
                        switch (param)
                        {
                            case "session_token":
                                {
                                    omm.session_token = value;
                                    break;
                                }

                            case "shop_name":
                                {
                                    omm.shop_name = value;
                                    break;
                                }

                            case "address":
                                {
                                    omm.address = value;
                                    break;
                                }


                            case "pin_code":
                                {
                                    omm.pin_code = value;
                                    break;
                                }

                            case "shop_lat":
                                {
                                    omm.shop_lat = value;
                                    break;
                                }

                            case "shop_long":
                                {
                                    omm.shop_long = value;
                                    break;
                                }

                            case "owner_name":
                                {
                                    omm.owner_name = value;
                                    break;
                                }

                            case "owner_contact_no":
                                {
                                    omm.owner_contact_no = value;
                                    break;
                                }

                            case "owner_email":
                                {
                                    omm.owner_email = value;
                                    break;
                                }

                            case "user_id":
                                {

                                    omm.user_id = value;
                                    break;
                                }

                            case "type":
                                {
                                    omm.type = Int32.Parse(value);
                                    break;
                                }
                            case "added_date":

                                omm.added_date = string.IsNullOrEmpty(Convert.ToString(value)) ? default(DateTime?) : Convert.ToDateTime(value);

                                //   omm.added_date = (value==null)?null : Convert.ToDateTime(value);;
                                break;


                            case "dob":

                                omm.dob = value;
                                break;

                            case "date_aniversary":

                                omm.date_aniversary = value;
                                break;

                            case "shop_id":

                                omm.shop_id = value;
                                break;
                            case "assigned_to_pp_id":

                                omm.assigned_to_pp_id = value;
                                break;

                            case "assigned_to_dd_id":

                                omm.assigned_to_dd_id = value;
                                break;

                            case "amount":

                                omm.amount = value;
                                break;

                            case "family_member_dob":

                                omm.family_member_dob = string.IsNullOrEmpty(Convert.ToString(value)) ? default(DateTime?) : Convert.ToDateTime(value);
                                break;
                            case "addtional_dob":

                                omm.addtional_dob = string.IsNullOrEmpty(Convert.ToString(value)) ? default(DateTime?) : Convert.ToDateTime(value);
                                break;
                            case "addtional_doa":

                                omm.addtional_doa = string.IsNullOrEmpty(Convert.ToString(value)) ? default(DateTime?) : Convert.ToDateTime(value);
                                break;
                            case "director_name":
                                omm.director_name = value;
                                break;
                            case "key_person_name":
                                omm.key_person_name = value;
                                break;
                            case "phone_no":
                                omm.phone_no = value;
                                break;

                            case "doc_family_member_dob":
                                omm.doc_family_member_dob = string.IsNullOrEmpty(Convert.ToString(value)) ? default(DateTime?) : Convert.ToDateTime(value);
                                break;
                            case "specialization":
                                omm.specialization = value;
                                break;
                            case "average_patient_per_day":
                                omm.average_patient_per_day = value;
                                break;
                            case "category":
                                omm.category = value;
                                break;
                            case "doc_address":
                                omm.doc_address = value;
                                break;
                            case "doc_pincode":
                                omm.doc_pincode = value;
                                break;
                            case "is_chamber_same_headquarter":
                                omm.is_chamber_same_headquarter = value;
                                break;
                            case "is_chamber_same_headquarter_remarks":
                                omm.is_chamber_same_headquarter_remarks = value;
                                break;
                            case "chemist_name":
                                omm.chemist_name = value;
                                break;
                            case "chemist_address":
                                omm.chemist_address = value;
                                break;
                            case "chemist_pincode":
                                omm.chemist_pincode = value;
                                break;
                            case "assistant_name":
                                omm.assistant_name = value;
                                break;
                            case "assistant_contact_no":
                                omm.assistant_contact_no = value;
                                break;
                            case "assistant_dob":
                                omm.assistant_dob = string.IsNullOrEmpty(Convert.ToString(value)) ? default(DateTime?) : Convert.ToDateTime(value); ;
                                break;
                            case "assistant_doa":
                                omm.assistant_doa = string.IsNullOrEmpty(Convert.ToString(value)) ? default(DateTime?) : Convert.ToDateTime(value); ;
                                break;
                            case "assistant_family_dob":
                                omm.assistant_family_dob = string.IsNullOrEmpty(Convert.ToString(value)) ? default(DateTime?) : Convert.ToDateTime(value);
                                break;
                            case "EntityCode":
                                omm.EntityCode = value;
                                break;
                            case "Entity_Location":
                                omm.Entity_Location = value;
                                break;
                            case "Alt_MobileNo":
                                omm.Alt_MobileNo = value;
                                break;
                            case "Entity_Status":
                                omm.Entity_Status = value;
                                break;
                            case "Entity_Type":
                                omm.Entity_Type = value;
                                break;
                            case "ShopOwner_PAN":
                                omm.ShopOwner_PAN = value;
                                break;
                            case "ShopOwner_Aadhar":
                                omm.ShopOwner_Aadhar = value;
                                break;
                            case "Remarks":
                                omm.Remarks = value;
                                break;
                            case "area_id":
                                omm.area_id = value;
                                break;
                            case "CityId":
                                omm.CityId = value;
                                break;

                            case "model_id":
                                omm.model_id = value;
                                break;
                            case "primary_app_id":
                                omm.primary_app_id = value;
                                break;
                            case "secondary_app_id":
                                omm.secondary_app_id = value;
                                break;
                            case "lead_id":
                                omm.lead_id = value;
                                break;
                            case "funnel_stage_id":
                                omm.funnel_stage_id = value;
                                break;
                            case "stage_id":
                                omm.stage_id = value;
                                break;
                            case "booking_amount":
                                omm.booking_amount = value;
                                break;
                            case "type_id":
                                omm.type_id = value;
                                break;
                            case "entity_id":
                                omm.entity_id = value;
                                break;
                            case "party_status_id":
                                omm.party_status_id = value;
                                break;
                            case "retailer_id":
                                omm.retailer_id = value;
                                break;
                            case "dealer_id":
                                omm.dealer_id = value;
                                break;
                            case "beat_id":
                                omm.beat_id = value;
                                break;
                            case "assigned_to_shop_id":
                                omm.assigned_to_shop_id = value;
                                break;
                            case "actual_address":
                                omm.actual_address = value;
                                break;
                            case "shop_revisit_uniqKey":
                                omm.shop_revisit_uniqKey = value;
                                break;
                            //Rev Debashis
                            case "agency_name":
                                omm.agency_name=value;
                                break;
                            case "lead_contact_number":
                                omm.lead_contact_number=value;
                                break;
                            case "project_name":
                                omm.project_name = value;
                                break;
                            case "landline_number":
                                omm.landline_number = value;
                                break;
                            case "alternateNoForCustomer":
                                omm.alternateNoForCustomer = value;
                                break;
                            case "whatsappNoForCustomer":
                                omm.whatsappNoForCustomer = value;
                                break;
                            case "isShopDuplicate":
                                omm.isShopDuplicate = Convert.ToBoolean(value);
                                break;
                            case "purpose":
                                omm.purpose = value;
                                break;
                            case "GSTN_Number":
                                omm.GSTN_Number = value;
                                break;
                            //End of Rev Debashis
                            //Rev 1.0 Row: 869
                            case "FSSAILicNo":
                                omm.FSSAILicNo = value;
                                break;
                            //End of Rev 1.0 Row: 869
                            //Rev 2.0 Row: 894
                            case "shop_firstName":
                                omm.shop_firstName = value;
                                break;
                            case "shop_lastName":
                                omm.shop_lastName = value;
                                break;
                            case "crm_companyID":
                                omm.crm_companyID = Convert.ToInt32(value);
                                break;
                            case "crm_jobTitle":
                                omm.crm_jobTitle = value;
                                break;
                            case "crm_typeID":
                                omm.crm_typeID = Convert.ToInt32(value);
                                break;
                            case "crm_statusID":
                                omm.crm_statusID = Convert.ToInt32(value);
                                break;
                            case "crm_sourceID":
                                omm.crm_sourceID = Convert.ToInt32(value);
                                break;
                            case "crm_referenceID":
                                omm.crm_referenceID = value;
                                break;
                            case "crm_referenceID_type":
                                omm.crm_referenceID_type = value;
                                break;
                            case "crm_stage_ID":
                                omm.crm_stage_ID = Convert.ToInt32(value);
                                break;
                            case "assign_to":
                                omm.assign_to = Convert.ToInt32(value);
                                break;
                            case "saved_from_status":
                                omm.saved_from_status = value;
                                break;
                                //End of Rev 2.0 Row: 894
                        }
                    }
                }
                catch (Exception msg)
                {
                    omodel.status = "206" + ImageName;
                    omodel.message = msg.Message;
                }

                if (!string.IsNullOrEmpty(model.data))
                {
                    ImageName = omm.session_token + '_' + omm.user_id + '_' + ImageName;
                    string vPath = Path.Combine(Server.MapPath("~/CommonFolder"), ImageName);
                    model.shop_image.SaveAs(vPath);
                }
                string sessionId = "";

                //using (StreamWriter stream = new FileInfo((Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(uploadtext)))).AppendText())
                //{
                //    stream.WriteLine("   Shop Add  Start " + "Start Time:" + DateTime.Now.ToString() + "End Time:" + DateTime.Now + "Status:Records not updated(ShopRegistration/RegisterShop)[202]" + "User ID:" + omm.user_id +    omm.added_date   +   ImageName+    model.shop_image.ContentLength  +     model.shop_image.ContentType+"   Shop add  END ");
                //}

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
                sqlcmd.Parameters.AddWithValue("@shop_image", ImageName);
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
                sqlcmd.Parameters.AddWithValue("@shop_revisit_uniqKey", omm.shop_revisit_uniqKey);
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
                //Rev 1.0 Row: 869
                sqlcmd.Parameters.AddWithValue("@FSSAILicNo", omm.FSSAILicNo);
                //End of Rev 1.0 Row: 869
                //Rev 2.0 Row: 894
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
                //End of Rev 2.0 Row: 894

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
                        omodel.session_token = sessionId;
                        omodel.data = oview;
                        omodel.message = "Shop added successfully";
                    }
                }
                else
                {
                    omodel.status = "202";
                    omodel.message = "Data not inserted";
                }
            }
            catch (Exception msg)
            {
                //using (StreamWriter stream = new FileInfo((Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(uploadtext)))).AppendText())
                //{
                //    stream.WriteLine("   Shop Add error   Start " + "Start Time:" + DateTime.Now.ToString() + "End Time:" + DateTime.Now + "Status:Records not updated(ShopRegistration/RegisterShop)[202]" + "User ID:" + omm.user_id + omm.added_date + ImageName + model.shop_image.ContentLength + model.shop_image.ContentType + "   Shop add  END ");
                //}

                omodel.status = "204" + ImageName;
                omodel.message = msg.Message;
            }
            // var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
            return Json(omodel);
        }


        //public HttpResponseMessage RegisterShop()
        //{

        //    string data = Convert.ToString(HttpContext.Current.Request.Form.AllKeys);
        //    var file1 = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;
        //    //string data="";
        //    //if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["data"]))
        //    //{
        //    //    data = HttpContext.Current.Request.Form["data"];
        //    //}
        //    // string data = "";
        //    RegisterShopOutput omodel = new RegisterShopOutput();
        //    RegisterShopInput omm = new RegisterShopInput();
        //    string ImageName = "";

        //    try
        //    {


        //        ShopRegister oview = new ShopRegister();
        //        //   ImageName = model.shop_image.FileName;
        //        string UploadFileDirectory = "~/CommonFolder";
        //        try
        //        {

        //            //   var details = JObject.Parse(data);

        //            var details = JObject.Parse(data);

        //            foreach (var item in details)
        //            {
        //                string param = item.Key;
        //                string value = Convert.ToString(item.Value);
        //                switch (param)
        //                {
        //                    case "session_token":
        //                        {
        //                            omm.session_token = value;
        //                            break;
        //                        }

        //                    case "shop_name":
        //                        {
        //                            omm.shop_name = value;
        //                            break;
        //                        }

        //                    case "address":
        //                        {
        //                            omm.address = value;
        //                            break;
        //                        }


        //                    case "pin_code":
        //                        {
        //                            omm.pin_code = value;
        //                            break;
        //                        }

        //                    case "shop_lat":
        //                        {
        //                            omm.shop_lat = value;
        //                            break;
        //                        }

        //                    case "shop_long":
        //                        {
        //                            omm.shop_long = value;
        //                            break;
        //                        }

        //                    case "owner_name":
        //                        {
        //                            omm.owner_name = value;
        //                            break;
        //                        }

        //                    case "owner_contact_no":
        //                        {
        //                            omm.owner_contact_no = value;
        //                            break;
        //                        }

        //                    case "owner_email":
        //                        {
        //                            omm.owner_email = value;
        //                            break;
        //                        }

        //                    case "user_id":
        //                        {

        //                            omm.user_id = value;
        //                            break;
        //                        }

        //                    case "type":
        //                        {
        //                            omm.type = Int32.Parse(value);
        //                            break;
        //                        }

        //                    case "dob":

        //                        omm.dob = value;
        //                        break;

        //                    case "date_aniversary":

        //                        omm.date_aniversary = value;
        //                        break;

        //                    case "shop_id":

        //                        omm.shop_id = value;
        //                        break;


        //                }

        //            }

        //        }
        //        catch (Exception msg)
        //        {

        //            omodel.status = "206" + ImageName;
        //            omodel.message = msg.Message;
        //            omodel.data.address = omm.address;
        //            omodel.data.type = omm.type;
        //            omodel.data.user_id = omm.user_id;
        //            omodel.data.owner_email = omm.owner_email;
        //            omodel.data.session_token = omm.session_token;
        //            omodel.data.dob = omm.dob;
        //            omodel.data.date_aniversary = omm.date_aniversary;
        //        }





        //        //ImageName = omm.session_token + '_' + omm.user_id + '_' + ImageName;
        //        //string vPath = Path.Combine(Server.MapPath("~/CommonFolder"), ImageName);
        //        //model.shop_image.SaveAs(vPath);

        //        var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;
        //        if (file != null && file.ContentLength > 0)
        //        {
        //            ImageName = omm.session_token + '_' + omm.user_id + '_' + Path.GetFileName(file.FileName);

        //            var path = Path.Combine(
        //                HttpContext.Current.Server.MapPath("~/CommonFolder"),
        //                ImageName
        //            );

        //            file.SaveAs(path);


        //        }


        //        string sessionId = "";

        //        ///sessionId = HttpContext.Current.Session.SessionID;

        //        DataTable dt = new DataTable();
        //        String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
        //        SqlCommand sqlcmd = new SqlCommand();
        //        SqlConnection sqlcon = new SqlConnection(con);
        //        sqlcon.Open();
        //        sqlcmd = new SqlCommand("Sp_ApiShopRegister", sqlcon);
        //        sqlcmd.Parameters.Add("@session_token", omm.session_token);
        //        sqlcmd.Parameters.Add("@shop_name", omm.shop_name);
        //        sqlcmd.Parameters.Add("@address", omm.address);
        //        sqlcmd.Parameters.Add("@pin_code", omm.pin_code);
        //        sqlcmd.Parameters.Add("@shop_lat", omm.shop_lat);
        //        sqlcmd.Parameters.Add("@shop_long", omm.shop_long);
        //        sqlcmd.Parameters.Add("@owner_name", omm.owner_name);
        //        sqlcmd.Parameters.Add("@owner_contact_no", omm.owner_contact_no);
        //        sqlcmd.Parameters.Add("@owner_email", omm.owner_email);
        //        sqlcmd.Parameters.Add("@user_id", omm.user_id);
        //        sqlcmd.Parameters.Add("@shop_image", ImageName);
        //        sqlcmd.Parameters.Add("@type", omm.type);
        //        sqlcmd.Parameters.Add("@dob", Convert.ToString(omm.dob) == "" ? null : omm.dob);
        //        sqlcmd.Parameters.Add("@date_aniversary", Convert.ToString(omm.date_aniversary) == "" ? null : omm.date_aniversary);
        //        sqlcmd.Parameters.Add("@shop_id", omm.shop_id);

        //        sqlcmd.CommandType = CommandType.StoredProcedure;
        //        SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
        //        da.Fill(dt);
        //        sqlcon.Close();
        //        if (dt.Rows.Count > 0)
        //        {
        //            if (Convert.ToString(dt.Rows[0]["returncode"]) == "203")
        //            {
        //                omodel.status = "203";
        //                omodel.message = "Duplicate shop Id or Email";


        //            }
        //            else if (Convert.ToString(dt.Rows[0]["returncode"]) == "202")
        //            {


        //                omodel.status = "202";
        //                omodel.message = "User or session token not matched";
        //            }
        //            else
        //            {
        //                oview = APIHelperMethods.ToModel<ShopRegister>(dt);
        //                omodel.status = "200";
        //                omodel.session_token = sessionId;
        //                omodel.data = oview;
        //                omodel.message = "Shop added successfully";

        //            }
        //        }
        //        else
        //        {

        //            omodel.status = "202";
        //            omodel.message = "Data not inserted";

        //        }


        //    }
        //    catch (Exception msg)
        //    {

        //        omodel.status = "204" + ImageName;
        //        omodel.message = msg.Message;
        //        omodel.data.address = omm.address;
        //        omodel.data.type = omm.type;
        //        omodel.data.user_id = omm.user_id;
        //        omodel.data.owner_email = omm.owner_email;
        //        omodel.data.session_token = omm.session_token;
        //        omodel.data.dob = omm.dob;
        //        omodel.data.date_aniversary = omm.date_aniversary;

        //    }
        //    // var message = Request.CreateResponse(HttpStatusCode.OK, omodel);



        //    var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
        //    return message;


        //    //////////////////----------------------------------Api controller------------------------------

        //    //   RegisterShopInputData model = new RegisterShopInputData();

        //    //RegisterShopOutput omodel = new RegisterShopOutput();
        //    //ShopRegister oview = new ShopRegister();
        //    //if (!ModelState.IsValid)
        //    //{
        //    //    omodel.status = "213";
        //    //    omodel.message = "Some input parameters are missing.";
        //    //    return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
        //    //}
        //    //else
        //    //{

        //    //    var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;

        //    //    if (file != null && file.ContentLength > 0)
        //    //    {
        //    //        var fileName = Path.GetFileName(file.FileName);

        //    //        var path = Path.Combine(
        //    //            HttpContext.Current.Server.MapPath("~/CommonFolder"),
        //    //            fileName
        //    //        );

        //    //        file.SaveAs(path);


        //    //    }



        //    //    String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];

        //    //    string sessionId = "";


        //    //    sessionId = HttpContext.Current.Session.SessionID;


        //    //    DataTable dt = new DataTable();
        //    //    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
        //    //    SqlCommand sqlcmd = new SqlCommand();
        //    //    SqlConnection sqlcon = new SqlConnection(con);
        //    //    sqlcon.Open();
        //    //    sqlcmd = new SqlCommand("Sp_ApiShopRegister", sqlcon);
        //    //    sqlcmd.Parameters.Add("@session_token", model.data.session_token);
        //    //    sqlcmd.Parameters.Add("@shop_name", model.data.shop_name);
        //    //    sqlcmd.Parameters.Add("@address", model.data.address);
        //    //    sqlcmd.Parameters.Add("@pin_code", model.data.pin_code);
        //    //    sqlcmd.Parameters.Add("@shop_lat", model.data.shop_lat);
        //    //    sqlcmd.Parameters.Add("@shop_long", model.data.shop_long);
        //    //    sqlcmd.Parameters.Add("@owner_name", model.data.owner_name);
        //    //    sqlcmd.Parameters.Add("@owner_contact_no", model.data.owner_contact_no);
        //    //    sqlcmd.Parameters.Add("@owner_email", model.data.owner_email);
        //    //    sqlcmd.Parameters.Add("@user_id", model.data.user_id);
        //    //    //  sqlcmd.Parameters.Add("@shop_image", model.shop_image);
        //    //    sqlcmd.Parameters.Add("@type", model.data.type);
        //    //    sqlcmd.Parameters.Add("@dob", model.data.dob);
        //    //    sqlcmd.Parameters.Add("@date_aniversary", model.data.date_aniversary);


        //    //    sqlcmd.CommandType = CommandType.StoredProcedure;
        //    //    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
        //    //    da.Fill(dt);
        //    //    sqlcon.Close();
        //    //    if (dt.Rows.Count > 0)
        //    //    {
        //    //        oview = APIHelperMethods.ToModel<ShopRegister>(dt);
        //    //        omodel.status = "200";
        //    //        omodel.session_token = sessionId;
        //    //        omodel.data = oview;
        //    //        omodel.message = "Shop added successfully";


        //    //    }
        //    //    else
        //    //    {

        //    //        omodel.status = "202";
        //    //        omodel.message = "Session token does not matched";

        //    //    }

        //    //    var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
        //    //    return message;
        //}

        public JsonResult EditShop(RegisterShopInputData model)
        {

            RegisterShopOutput omodel = new RegisterShopOutput();
            RegisterShopInput omm = new RegisterShopInput();
            string ImageName = "";
            string degreeName = "";

            try
            {
                ShopRegister oview = new ShopRegister();
                ImageName = model.shop_image.FileName;
                string UploadFileDirectory = "~/CommonFolder";

                try
                {
                    var details = JObject.Parse(model.data);

                    foreach (var item in details)
                    {
                        string param = item.Key;
                        string value = Convert.ToString(item.Value);
                        switch (param)
                        {
                            case "session_token":
                                {
                                    omm.session_token = value;
                                    break;
                                }

                            case "shop_name":
                                {
                                    omm.shop_name = value;
                                    break;
                                }

                            case "address":
                                {
                                    omm.address = value;
                                    break;
                                }
                            case "pin_code":
                                {
                                    omm.pin_code = value;
                                    break;
                                }

                            case "shop_lat":
                                {
                                    omm.shop_lat = value;
                                    break;
                                }

                            case "shop_long":
                                {
                                    omm.shop_long = value;
                                    break;
                                }

                            case "owner_name":
                                {
                                    omm.owner_name = value;
                                    break;
                                }

                            case "owner_contact_no":
                                {
                                    omm.owner_contact_no = value;
                                    break;
                                }

                            case "owner_email":
                                {
                                    omm.owner_email = value;
                                    break;
                                }

                            case "user_id":
                                {

                                    omm.user_id = value;
                                    break;
                                }

                            case "type":
                                {
                                    omm.type = Int32.Parse(value);
                                    break;
                                }
                            //case "added_date":

                            //    omm.added_date = value;
                            //    break;


                            case "dob":

                                omm.dob = value;
                                break;

                            case "date_aniversary":

                                omm.date_aniversary = value;
                                break;

                            case "shop_id":

                                omm.shop_id = value;
                                break;


                            case "assigned_to_pp_id":

                                omm.assigned_to_pp_id = value;
                                break;

                            case "assigned_to_dd_id":

                                omm.assigned_to_dd_id = value;
                                break;

                            case "amount":

                                omm.amount = value;
                                break;

                            case "family_member_dob":
                                omm.family_member_dob = string.IsNullOrEmpty(Convert.ToString(value)) ? default(DateTime?) : Convert.ToDateTime(value);
                                break;
                            case "addtional_dob":
                                omm.addtional_dob = string.IsNullOrEmpty(Convert.ToString(value)) ? default(DateTime?) : Convert.ToDateTime(value);
                                break;
                            case "addtional_doa":
                                omm.addtional_doa = string.IsNullOrEmpty(Convert.ToString(value)) ? default(DateTime?) : Convert.ToDateTime(value);
                                break;
                            case "director_name":
                                omm.director_name = value;
                                break;
                            case "key_person_name":
                                omm.key_person_name = value;
                                break;
                            case "phone_no":
                                omm.phone_no = value;
                                break;

                            case "doc_family_member_dob":
                                omm.doc_family_member_dob = string.IsNullOrEmpty(Convert.ToString(value)) ? default(DateTime?) : Convert.ToDateTime(value);
                                break;
                            case "specialization":
                                omm.specialization = value;
                                break;
                            case "average_patient_per_day":
                                omm.average_patient_per_day = value;
                                break;
                            case "category":
                                omm.category = value;
                                break;
                            case "doc_address":
                                omm.doc_address = value;
                                break;
                            case "doc_pincode":
                                omm.doc_pincode = value;
                                break;
                            case "is_chamber_same_headquarter":
                                omm.is_chamber_same_headquarter = value;
                                break;
                            case "is_chamber_same_headquarter_remarks":
                                omm.is_chamber_same_headquarter_remarks = value;
                                break;
                            case "chemist_name":
                                omm.chemist_name = value;
                                break;
                            case "chemist_address":
                                omm.chemist_address = value;
                                break;
                            case "chemist_pincode":
                                omm.chemist_pincode = value;
                                break;
                            case "assistant_name":
                                omm.assistant_name = value;
                                break;
                            case "assistant_contact_no":
                                omm.assistant_contact_no = value;
                                break;
                            case "assistant_dob":
                                omm.assistant_dob = string.IsNullOrEmpty(Convert.ToString(value)) ? default(DateTime?) : Convert.ToDateTime(value); ;
                                break;
                            case "assistant_doa":
                                omm.assistant_doa = string.IsNullOrEmpty(Convert.ToString(value)) ? default(DateTime?) : Convert.ToDateTime(value); ;
                                break;
                            case "assistant_family_dob":
                                omm.assistant_family_dob = string.IsNullOrEmpty(Convert.ToString(value)) ? default(DateTime?) : Convert.ToDateTime(value);
                                break;
                            case "EntityCode":
                                omm.EntityCode = value;
                                break;
                            case "Entity_Location":
                                omm.Entity_Location = value;
                                break;
                            case "Alt_MobileNo":
                                omm.Alt_MobileNo = value;
                                break;
                            case "Entity_Status":
                                omm.Entity_Status = value;
                                break;
                            case "Entity_Type":
                                omm.Entity_Type = value;
                                break;
                            case "ShopOwner_PAN":
                                omm.ShopOwner_PAN = value;
                                break;
                            case "ShopOwner_Aadhar":
                                omm.ShopOwner_Aadhar = value;
                                break;
                            case "Remarks":
                                omm.Remarks = value;
                                break;
                            case "area_id":
                                omm.area_id = value;
                                break;
                            case "CityId":
                                omm.CityId = value;
                                break;

                            case "model_id":
                                omm.model_id = value;
                                break;
                            case "primary_app_id":
                                omm.primary_app_id = value;
                                break;
                            case "secondary_app_id":
                                omm.secondary_app_id = value;
                                break;
                            case "lead_id":
                                omm.lead_id = value;
                                break;
                            case "funnel_stage_id":
                                omm.funnel_stage_id = value;
                                break;
                            case "stage_id":
                                omm.stage_id = value;
                                break;
                            case "booking_amount":
                                omm.booking_amount = value;
                                break;
                            case "type_id":
                                omm.type_id = value;
                                break;
                            case "entity_id":
                                omm.entity_id = value;
                                break;
                            case "party_status_id":
                                omm.party_status_id = value;
                                break;
                            case "retailer_id":
                                omm.retailer_id = value;
                                break;
                            case "dealer_id":
                                omm.dealer_id = value;
                                break;
                            case "beat_id":
                                omm.beat_id = value;
                                break;
                            case "assigned_to_shop_id":
                                omm.assigned_to_shop_id = value;
                                break;
                            //Rev Debashis
                            case "agency_name":
                                omm.agency_name = value;
                                break;
                            case "lead_contact_number":
                                omm.lead_contact_number = value;
                                break;
                            case "project_name":
                                omm.project_name = value;
                                break;
                            case "landline_number":
                                omm.landline_number = value;
                                break;
                            case "alternateNoForCustomer":
                                omm.alternateNoForCustomer = value;
                                break;
                            case "whatsappNoForCustomer":
                                omm.whatsappNoForCustomer = value;
                                break;
                            case "GSTN_Number":
                                omm.GSTN_Number = value;
                                break;
                            //End of Rev Debashis
                            //Rev 1.0 Row: 875
                            case "isUpdateAddressFromShopMaster":
                                omm.isUpdateAddressFromShopMaster = Convert.ToBoolean(value);
                                break;
                            //End of Rev 1.0 Row: 875
                            //Rev 2.0 Row: 897
                            case "shop_firstName":
                                omm.shop_firstName = value;
                                break;
                            case "shop_lastName":
                                omm.shop_lastName = value;
                                break;
                            case "crm_companyID":
                                omm.crm_companyID = Convert.ToInt32(value);
                                break;
                            case "crm_jobTitle":
                                omm.crm_jobTitle = value;
                                break;
                            case "crm_typeID":
                                omm.crm_typeID = Convert.ToInt32(value);
                                break;
                            case "crm_statusID":
                                omm.crm_statusID = Convert.ToInt32(value);
                                break;
                            case "crm_sourceID":
                                omm.crm_sourceID = Convert.ToInt32(value);
                                break;
                            case "crm_referenceID":
                                omm.crm_referenceID = value;
                                break;
                            case "crm_referenceID_type":
                                omm.crm_referenceID_type = value;
                                break;
                            case "crm_stage_ID":
                                omm.crm_stage_ID = Convert.ToInt32(value);
                                break;
                            case "assign_to":
                                omm.assign_to = Convert.ToInt32(value);
                                break;
                            case "saved_from_status":
                                omm.saved_from_status = value;
                                break;
                                //End of Rev 2.0 Row: 897
                        }
                    }
                }
                catch (Exception msg)
                {
                    omodel.status = "206" + ImageName;
                    omodel.message = msg.Message;
                }

                ImageName = omm.session_token + '_' + omm.user_id + '_' + ImageName;
                string vPath = Path.Combine(Server.MapPath("~/CommonFolder"), ImageName);
                model.shop_image.SaveAs(vPath);
                string sessionId = "";


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
                sqlcmd.Parameters.AddWithValue("@shop_image", ImageName);
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
                sqlcmd.Parameters.AddWithValue("@GSTN_Number", omm.GSTN_Number);
                //End of Rev Debashis
                //Rev 1.0 Row: 875
                sqlcmd.Parameters.AddWithValue("@isUpdateAddressFromShopMaster", omm.isUpdateAddressFromShopMaster);
                //End of Rev 1.0 Row: 875
                //Rev 2.0 Row: 897
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
                //End of Rev 2.0 Row: 897

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
                        omodel.session_token = sessionId;
                        omodel.data = oview;
                        omodel.message = "Shop Updated successfully";
                    }
                }
                else
                {
                    omodel.status = "202";
                    omodel.message = "Data not inserted";
                }
            }
            catch (Exception msg)
            {
                omodel.status = "204" + ImageName;
                omodel.message = msg.Message;
            }
            // var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
            return Json(omodel);
        }


        //Add new API for Add Billing with Image 22-11-2019
        string uploadBilltext = "~/CommonFolder/Log/BillingWithImage.txt";
        [AcceptVerbs("POST")]

        public JsonResult AddBillingImage(BillingInputData model)
        {

            AlermShopvisitOutput omodel = new AlermShopvisitOutput();
            BillingImageInput omm = new BillingImageInput();
            List<ProductList> ommprod = new List<ProductList>();
            string ImageName = "";

            ShopRegister oview = new ShopRegister();
            ImageName = model.billing_image.FileName;
            string UploadFileDirectory = "~/CommonFolder";
            try
            {
                string products = "";
                var details = JObject.Parse(model.data);

                var hhhh = Newtonsoft.Json.JsonConvert.DeserializeObject<BillingImageInput>(model.data);

                string JsonXML = "";
                if (!string.IsNullOrEmpty(model.data))
                {
                    ImageName = hhhh.session_token + '_' + hhhh.user_id + '_' + ImageName;
                    string vPath = Path.Combine(Server.MapPath("~/CommonFolder"), ImageName);
                    model.billing_image.SaveAs(vPath);

                    JsonXML = XmlConversion.ConvertToXml(hhhh.product_list, 0);
                }
                string sessionId = "";

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();

                sqlcmd = new SqlCommand("Proc_FTS_BillingManagement", sqlcon);
                sqlcmd.Parameters.Add("@user_id", hhhh.user_id);
                sqlcmd.Parameters.Add("@bill_id", hhhh.bill_id);
                sqlcmd.Parameters.Add("@invoice_no", hhhh.invoice_no);
                sqlcmd.Parameters.Add("@invoice_date", hhhh.invoice_date);
                sqlcmd.Parameters.Add("@invoice_amount", hhhh.invoice_amount);
                sqlcmd.Parameters.Add("@remarks", hhhh.remarks);
                sqlcmd.Parameters.Add("@order_id", hhhh.order_id);
                sqlcmd.Parameters.Add("@Product_List", JsonXML);
                sqlcmd.Parameters.Add("@Inv_Img_id", ImageName);
                sqlcmd.Parameters.Add("@Action", "Insert");
                //Extra Input for 4Basecare
                sqlcmd.Parameters.Add("@patient_no", hhhh.patient_no);
                sqlcmd.Parameters.Add("@patient_name", hhhh.patient_name);
                sqlcmd.Parameters.Add("@patient_address ", hhhh.patient_address);
                //Extra Input for 4Basecare
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Billing details added successfully.";
                }
                else
                {
                    omodel.status = "202";
                    omodel.message = "Data not inserted";
                }
            }
            catch (Exception msg)
            {
                omodel.status = "204" + ImageName;
                omodel.message = msg.Message;
            }
            return Json(omodel);
        }

        //End Add new API for Add Billing with Image 22-11-2019

        //Add new API for shop registration with degree 06-01-2020
        public JsonResult NewShopRegister(RegisterShopInputDegreeData model)
        {
            RegisterShopOutput omodel = new RegisterShopOutput();
            RegisterShopInput omm = new RegisterShopInput();
           
            string degreeName = "";
            try
            {
                ShopRegister oview = new ShopRegister();
                degreeName = model.degree.FileName;
                string UploadFileDirectory = "~/DoctorImage";
                try
                {
                    var details = JObject.Parse(model.data);
                    foreach (var item in details)
                    {
                        string param = item.Key;
                        string value = Convert.ToString(item.Value);
                        switch (param)
                        {
                            case "session_token":
                                {
                                    omm.session_token = value;
                                    break;
                                }
                            case "shop_name":
                                {
                                    omm.shop_name = value;
                                    break;
                                }
                            case "address":
                                {
                                    omm.address = value;
                                    break;
                                }
                            case "pin_code":
                                {
                                    omm.pin_code = value;
                                    break;
                                }
                            case "shop_lat":
                                {
                                    omm.shop_lat = value;
                                    break;
                                }
                            case "shop_long":
                                {
                                    omm.shop_long = value;
                                    break;
                                }
                            case "owner_name":
                                {
                                    omm.owner_name = value;
                                    break;
                                }
                            case "owner_contact_no":
                                {
                                    omm.owner_contact_no = value;
                                    break;
                                }
                            case "owner_email":
                                {
                                    omm.owner_email = value;
                                    break;
                                }
                            case "user_id":
                                {

                                    omm.user_id = value;
                                    break;
                                }
                            case "type":
                                {
                                    omm.type = Int32.Parse(value);
                                    break;
                                }
                            case "added_date":

                                omm.added_date = string.IsNullOrEmpty(Convert.ToString(value)) ? default(DateTime?) : Convert.ToDateTime(value);

                                //   omm.added_date = (value==null)?null : Convert.ToDateTime(value);;
                                break;
                            case "dob":
                                omm.dob = value;
                                break;

                            case "date_aniversary":
                                omm.date_aniversary = value;
                                break;

                            case "shop_id":
                                omm.shop_id = value;
                                break;
                            case "assigned_to_pp_id":
                                omm.assigned_to_pp_id = value;
                                break;

                            case "assigned_to_dd_id":
                                omm.assigned_to_dd_id = value;
                                break;

                            case "amount":
                                omm.amount = value;
                                break;

                            case "family_member_dob":
                                omm.family_member_dob = string.IsNullOrEmpty(Convert.ToString(value)) ? default(DateTime?) : Convert.ToDateTime(value);
                                break;
                            case "addtional_dob":
                                omm.addtional_dob = string.IsNullOrEmpty(Convert.ToString(value)) ? default(DateTime?) : Convert.ToDateTime(value);
                                break;
                            case "addtional_doa":
                                omm.addtional_doa = string.IsNullOrEmpty(Convert.ToString(value)) ? default(DateTime?) : Convert.ToDateTime(value);
                                break;
                            case "director_name":
                                omm.director_name = value;
                                break;
                            case "key_person_name":
                                omm.key_person_name = value;
                                break;
                            case "phone_no":
                                omm.phone_no = value;
                                break;
                            case "doc_family_member_dob":
                                omm.doc_family_member_dob = string.IsNullOrEmpty(Convert.ToString(value)) ? default(DateTime?) : Convert.ToDateTime(value);
                                break;
                            case "specialization":
                                omm.specialization = value;
                                break;
                            case "average_patient_per_day":
                                omm.average_patient_per_day = value;
                                break;
                            case "category":
                                omm.category = value;
                                break;
                            case "doc_address":
                                omm.doc_address = value;
                                break;
                            case "doc_pincode":
                                omm.doc_pincode = value;
                                break;
                            case "is_chamber_same_headquarter":
                                omm.is_chamber_same_headquarter = value;
                                break;
                            case "is_chamber_same_headquarter_remarks":
                                omm.is_chamber_same_headquarter_remarks = value;
                                break;
                            case "chemist_name":
                                omm.chemist_name = value;
                                break;
                            case "chemist_address":
                                omm.chemist_address = value;
                                break;
                            case "chemist_pincode":
                                omm.chemist_pincode = value;
                                break;
                            case "assistant_name":
                                omm.assistant_name = value;
                                break;
                            case "assistant_contact_no":
                                omm.assistant_contact_no = value;
                                break;
                            case "assistant_dob":
                                omm.assistant_dob = string.IsNullOrEmpty(Convert.ToString(value)) ? default(DateTime?) : Convert.ToDateTime(value); ;
                                break;
                            case "assistant_doa":
                                omm.assistant_doa = string.IsNullOrEmpty(Convert.ToString(value)) ? default(DateTime?) : Convert.ToDateTime(value); ;
                                break;
                            case "assistant_family_dob":
                                omm.assistant_family_dob = string.IsNullOrEmpty(Convert.ToString(value)) ? default(DateTime?) : Convert.ToDateTime(value);
                                break;
                            case "entity_id":
                                omm.entity_id = value;
                                break;
                            case "party_status_id":
                                omm.party_status_id = value;
                                break;
                            case "retailer_id":
                                omm.retailer_id = value;
                                break;
                            case "dealer_id":
                                omm.dealer_id = value;
                                break;
                            case "beat_id":
                                omm.beat_id = value;
                                break;
                            case "assigned_to_shop_id":
                                omm.assigned_to_shop_id = value;
                                break;
                            case "actual_address":
                                omm.actual_address = value;
                                break;
                            //Rev Debashis
                            case "project_name":
                                omm.project_name = value;
                                break;
                            case "landline_number":
                                omm.landline_number = value;
                                break;
                            case "alternateNoForCustomer":
                                omm.alternateNoForCustomer = value;
                                break;
                            case "whatsappNoForCustomer":
                                omm.whatsappNoForCustomer = value;
                                break;
                            case "isShopDuplicate":
                                omm.isShopDuplicate = Convert.ToBoolean(value);
                                break;
                            case "purpose":
                                omm.purpose = value;
                                break;
                            case "ShopOwner_PAN":
                                omm.ShopOwner_PAN = value;
                                break;
                            case "GSTN_Number":
                                omm.GSTN_Number = value;
                                break;
                            //End of Rev Debashis
                            //Rev 1.0 Row: 868
                            case "FSSAILicNo":
                                omm.FSSAILicNo = value;
                                break;
                            //End of Rev 1.0 Row: 868
                            //Rev 2.0 Row: 893
                            case "shop_firstName":
                                omm.shop_firstName= value;
                                break;
                            case "shop_lastName":
                                omm.shop_lastName = value;
                                break;
                            case "crm_companyID":
                                omm.crm_companyID = Convert.ToInt32(value);
                                break;
                            case "crm_jobTitle":
                                omm.crm_jobTitle = value;
                                break;
                            case "crm_typeID":
                                omm.crm_typeID = Convert.ToInt32(value);
                                break;
                            case "crm_statusID":
                                omm.crm_statusID = Convert.ToInt32(value);
                                break;
                            case "crm_sourceID":
                                omm.crm_sourceID = Convert.ToInt32(value);
                                break;
                            case "crm_referenceID":
                                omm.crm_referenceID = value;
                                break;
                            case "crm_referenceID_type":
                                omm.crm_referenceID_type = value;
                                break;
                            case "crm_stage_ID":
                                omm.crm_stage_ID = Convert.ToInt32(value);
                                break;
                            case "assign_to":
                                omm.assign_to = Convert.ToInt32(value);
                                break;
                            case "saved_from_status":
                                omm.saved_from_status = value;
                                break;
                                //End of Rev 2.0 Row: 893
                        }
                    }
                }
                catch (Exception msg)
                {
                    omodel.status = "206" + degreeName;
                    omodel.message = msg.Message;
                }

                if (!string.IsNullOrEmpty(model.data))
                {
                    degreeName = omm.session_token + '_' + omm.user_id + '_' + degreeName;
                    string vPath = Path.Combine(Server.MapPath("~/DoctorImage"), degreeName);
                    model.degree.SaveAs(vPath);
                }
                string sessionId = "";

               
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
                sqlcmd.Parameters.AddWithValue("@DEGREE", degreeName);
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
                sqlcmd.Parameters.AddWithValue("@entity_id", omm.entity_id);
                sqlcmd.Parameters.AddWithValue("@party_status_id", omm.party_status_id);
                sqlcmd.Parameters.AddWithValue("@retailer_id", omm.retailer_id);
                sqlcmd.Parameters.AddWithValue("@dealer_id", omm.dealer_id);
                sqlcmd.Parameters.AddWithValue("@beat_id", omm.beat_id);
                sqlcmd.Parameters.AddWithValue("@assigned_to_shop_id", omm.assigned_to_shop_id);
                sqlcmd.Parameters.AddWithValue("@actual_address", omm.actual_address);
                //Rev Debashis
                sqlcmd.Parameters.AddWithValue("@project_name", omm.project_name);
                sqlcmd.Parameters.AddWithValue("@landline_number", omm.landline_number);
                sqlcmd.Parameters.AddWithValue("@alternateNoForCustomer", omm.alternateNoForCustomer);
                sqlcmd.Parameters.AddWithValue("@whatsappNoForCustomer", omm.whatsappNoForCustomer);
                sqlcmd.Parameters.AddWithValue("@isShopDuplicate", omm.isShopDuplicate);
                sqlcmd.Parameters.AddWithValue("@purpose", omm.purpose);
                sqlcmd.Parameters.AddWithValue("@ShopOwner_PAN", omm.ShopOwner_PAN);
                sqlcmd.Parameters.AddWithValue("@GSTN_Number", omm.GSTN_Number);
                //End of Rev Debashis
                //Rev 1.0 Row: 868
                sqlcmd.Parameters.AddWithValue("@FSSAILicNo", omm.FSSAILicNo);
                //End of Rev 1.0 Row: 868
                //Rev 2.0 Row: 893
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
                //End of Rev 2.0 Row: 893

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
                        omodel.session_token = sessionId;
                        omodel.data = oview;
                        omodel.message = "Shop added successfully";
                    }
                }
                else
                {
                    omodel.status = "202";
                    omodel.message = "Data not inserted";
                }
            }
            catch (Exception msg)
            {
                omodel.status = "204" + degreeName;
                omodel.message = msg.Message;
            }
            // var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
            return Json(omodel);
        }
        //End Add new API for shop registration with degree 06-01-2020


        //edit new API for shop registration with degree 06-01-2020
        public JsonResult NewShopEdit(RegisterShopInputDegreeData model)
        {
            RegisterShopOutput omodel = new RegisterShopOutput();
            RegisterShopInput omm = new RegisterShopInput();
            string degreeName = "";

            try
            {
                ShopRegister oview = new ShopRegister();
                degreeName = model.degree.FileName;
                string UploadFileDirectory = "~/DoctorImage";

                try
                {
                    var details = JObject.Parse(model.data);

                    foreach (var item in details)
                    {
                        string param = item.Key;
                        string value = Convert.ToString(item.Value);
                        switch (param)
                        {
                            case "session_token":
                                {
                                    omm.session_token = value;
                                    break;
                                }
                            case "shop_name":
                                {
                                    omm.shop_name = value;
                                    break;
                                }
                            case "address":
                                {
                                    omm.address = value;
                                    break;
                                }
                            case "pin_code":
                                {
                                    omm.pin_code = value;
                                    break;
                                }

                            case "shop_lat":
                                {
                                    omm.shop_lat = value;
                                    break;
                                }
                            case "shop_long":
                                {
                                    omm.shop_long = value;
                                    break;
                                }
                            case "owner_name":
                                {
                                    omm.owner_name = value;
                                    break;
                                }
                            case "owner_contact_no":
                                {
                                    omm.owner_contact_no = value;
                                    break;
                                }

                            case "owner_email":
                                {
                                    omm.owner_email = value;
                                    break;
                                }
                            case "user_id":
                                {
                                    omm.user_id = value;
                                    break;
                                }
                            case "type":
                                {
                                    omm.type = Int32.Parse(value);
                                    break;
                                }
                            case "dob":
                                omm.dob = value;
                                break;
                            case "date_aniversary":
                                omm.date_aniversary = value;
                                break;
                            case "shop_id":
                                omm.shop_id = value;
                                break;
                            case "assigned_to_pp_id":
                                omm.assigned_to_pp_id = value;
                                break;
                            case "assigned_to_dd_id":
                                omm.assigned_to_dd_id = value;
                                break;
                            case "amount":
                                omm.amount = value;
                                break;
                            case "family_member_dob":
                                omm.family_member_dob = string.IsNullOrEmpty(Convert.ToString(value)) ? default(DateTime?) : Convert.ToDateTime(value);
                                break;
                            case "addtional_dob":
                                omm.addtional_dob = string.IsNullOrEmpty(Convert.ToString(value)) ? default(DateTime?) : Convert.ToDateTime(value);
                                break;
                            case "addtional_doa":
                                omm.addtional_doa = string.IsNullOrEmpty(Convert.ToString(value)) ? default(DateTime?) : Convert.ToDateTime(value);
                                break;
                            case "director_name":
                                omm.director_name = value;
                                break;
                            case "key_person_name":
                                omm.key_person_name = value;
                                break;
                            case "phone_no":
                                omm.phone_no = value;
                                break;
                            case "doc_family_member_dob":
                                omm.doc_family_member_dob = string.IsNullOrEmpty(Convert.ToString(value)) ? default(DateTime?) : Convert.ToDateTime(value);
                                break;
                            case "specialization":
                                omm.specialization = value;
                                break;
                            case "average_patient_per_day":
                                omm.average_patient_per_day = value;
                                break;
                            case "category":
                                omm.category = value;
                                break;
                            case "doc_address":
                                omm.doc_address = value;
                                break;
                            case "doc_pincode":
                                omm.doc_pincode = value;
                                break;
                            case "is_chamber_same_headquarter":
                                omm.is_chamber_same_headquarter = value;
                                break;
                            case "is_chamber_same_headquarter_remarks":
                                omm.is_chamber_same_headquarter_remarks = value;
                                break;
                            case "chemist_name":
                                omm.chemist_name = value;
                                break;
                            case "chemist_address":
                                omm.chemist_address = value;
                                break;
                            case "chemist_pincode":
                                omm.chemist_pincode = value;
                                break;
                            case "assistant_name":
                                omm.assistant_name = value;
                                break;
                            case "assistant_contact_no":
                                omm.assistant_contact_no = value;
                                break;
                            case "assistant_dob":
                                omm.assistant_dob = string.IsNullOrEmpty(Convert.ToString(value)) ? default(DateTime?) : Convert.ToDateTime(value); ;
                                break;
                            case "assistant_doa":
                                omm.assistant_doa = string.IsNullOrEmpty(Convert.ToString(value)) ? default(DateTime?) : Convert.ToDateTime(value); ;
                                break;
                            case "assistant_family_dob":
                                omm.assistant_family_dob = string.IsNullOrEmpty(Convert.ToString(value)) ? default(DateTime?) : Convert.ToDateTime(value);
                                break;
                            case "entity_id":
                                omm.entity_id = value;
                                break;
                            case "party_status_id":
                                omm.party_status_id = value;
                                break;
                            case "retailer_id":
                                omm.retailer_id = value;
                                break;
                            case "dealer_id":
                                omm.dealer_id = value;
                                break;
                            case "beat_id":
                                omm.beat_id = value;
                                break;
                            case "assigned_to_shop_id":
                                omm.assigned_to_shop_id = value;
                                break;
                            case "actual_address":
                                omm.actual_address = value;
                                break;
                            //Rev Debashis
                            case "project_name":
                                omm.project_name = value;
                                break;
                            case "landline_number":
                                omm.landline_number = value;
                                break;
                            case "alternateNoForCustomer":
                                omm.alternateNoForCustomer = value;
                                break;
                            case "whatsappNoForCustomer":
                                omm.whatsappNoForCustomer = value;
                                break;
                            case "ShopOwner_PAN":
                                omm.ShopOwner_PAN = value;
                                break;
                            case "GSTN_Number":
                                omm.GSTN_Number = value;
                                break;
                            //End of Rev Debashis
                            //Rev 1.0 Row: 874
                            case "isUpdateAddressFromShopMaster":
                                omm.isUpdateAddressFromShopMaster = Convert.ToBoolean(value);
                                break;
                            //End of Rev 1.0 Row: 874
                            //Rev 2.0 Row: 896
                            case "shop_firstName":
                                omm.shop_firstName = value;
                                break;
                            case "shop_lastName":
                                omm.shop_lastName = value;
                                break;
                            case "crm_companyID":
                                omm.crm_companyID = Convert.ToInt32(value);
                                break;
                            case "crm_jobTitle":
                                omm.crm_jobTitle = value;
                                break;
                            case "crm_typeID":
                                omm.crm_typeID = Convert.ToInt32(value);
                                break;
                            case "crm_statusID":
                                omm.crm_statusID = Convert.ToInt32(value);
                                break;
                            case "crm_sourceID":
                                omm.crm_sourceID = Convert.ToInt32(value);
                                break;
                            case "crm_referenceID":
                                omm.crm_referenceID = value;
                                break;
                            case "crm_referenceID_type":
                                omm.crm_referenceID_type = value;
                                break;
                            case "crm_stage_ID":
                                omm.crm_stage_ID = Convert.ToInt32(value);
                                break;
                            case "assign_to":
                                omm.assign_to = Convert.ToInt32(value);
                                break;
                            case "saved_from_status":
                                omm.saved_from_status = value;
                                break;
                                //End of Rev 2.0 Row: 896
                        }
                    }
                }
                catch (Exception msg)
                {
                    omodel.status = "206" + degreeName;
                    omodel.message = msg.Message;
                }

                degreeName = omm.session_token + '_' + omm.user_id + '_' + degreeName;
                string vPath = Path.Combine(Server.MapPath("~/DoctorImage"), degreeName);
                model.degree.SaveAs(vPath);
                string sessionId = "";

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
                sqlcmd.Parameters.AddWithValue("@DEGREE", degreeName);
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
                sqlcmd.Parameters.AddWithValue("@entity_id", omm.entity_id);
                sqlcmd.Parameters.AddWithValue("@party_status_id", omm.party_status_id);
                sqlcmd.Parameters.AddWithValue("@retailer_id", omm.retailer_id);
                sqlcmd.Parameters.AddWithValue("@dealer_id", omm.dealer_id);
                sqlcmd.Parameters.AddWithValue("@beat_id", omm.beat_id);
                sqlcmd.Parameters.AddWithValue("@assigned_to_shop_id", omm.assigned_to_shop_id);
                sqlcmd.Parameters.AddWithValue("@actual_address", omm.actual_address);
                //Rev Debashis
                sqlcmd.Parameters.AddWithValue("@project_name", omm.project_name);
                sqlcmd.Parameters.AddWithValue("@landline_number", omm.landline_number);
                sqlcmd.Parameters.AddWithValue("@alternateNoForCustomer", omm.alternateNoForCustomer);
                sqlcmd.Parameters.AddWithValue("@whatsappNoForCustomer", omm.whatsappNoForCustomer);
                sqlcmd.Parameters.AddWithValue("@ShopOwner_PAN", omm.ShopOwner_PAN);
                sqlcmd.Parameters.AddWithValue("@GSTN_Number", omm.GSTN_Number);
                //End of Rev Debashis
                //Rev 1.0 Row: 874
                sqlcmd.Parameters.AddWithValue("@isUpdateAddressFromShopMaster", omm.isUpdateAddressFromShopMaster);
                //End of Rev 1.0 Row: 874
                //Rev 2.0 Row: 896
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
                //End of Rev 2.0 Row: 896

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
                        omodel.session_token = sessionId;
                        omodel.data = oview;
                        omodel.message = "Shop Updated successfully";
                    }
                }
                else
                {
                    omodel.status = "202";
                    omodel.message = "Data not inserted";
                }
            }
            catch (Exception msg)
            {
                omodel.status = "204" + degreeName;
                omodel.message = msg.Message;
            }
            // var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
            return Json(omodel);
        }
        //End edit new API for shop registration with degree 06-01-2020

        [AcceptVerbs("POST")]
        public JsonResult AddCompetitorImage(CompetitorImageInput model)
        {
            AlermShopvisitOutput omodel = new AlermShopvisitOutput();
            CompetitorImageInputData omm = new CompetitorImageInputData();
            string ImageName = "";

            ShopRegister oview = new ShopRegister();
            ImageName = model.competitor_img.FileName;
            string UploadFileDirectory = "~/CommonFolder";
            try
            {
                string products = "";
                var details = JObject.Parse(model.data);

                var hhhh = Newtonsoft.Json.JsonConvert.DeserializeObject<CompetitorImageInputData>(model.data);

                if (!string.IsNullOrEmpty(model.data))
                {
                    ImageName = (DateTime.Now).ToString("yyyyMMddHHmmssffff") + '_' + hhhh.user_id + '_' + ImageName;
                    string vPath = Path.Combine(Server.MapPath("~/CommonFolder/CompetitorImage"), ImageName);
                    model.competitor_img.SaveAs(vPath);
                }
                string sessionId = "";

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();

                sqlcmd = new SqlCommand("Proc_FTS_CompetitorImage", sqlcon);
                sqlcmd.Parameters.Add("@user_id", hhhh.user_id);
                sqlcmd.Parameters.Add("@CompetitorImage", ImageName);
                sqlcmd.Parameters.Add("@Shop_Code", hhhh.shop_id);
                sqlcmd.Parameters.Add("@visited_date", hhhh.visited_date);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Competitor Image added successfully.";
                }
                else
                {
                    omodel.status = "202";
                    omodel.message = "Data not inserted";
                }
            }
            catch (Exception msg)
            {
                omodel.status = "204" + ImageName;
                omodel.message = msg.Message;
            }
            return Json(omodel);
        }

        //Rev Debashis Row: 761 to 765
        [HttpPost]
        public JsonResult ShopAttachmentImage1(ShopAttachmentImage1Input model)
        {
            ShopAttachmentImage1Output omodel = new ShopAttachmentImage1Output();
            string ImageName = "";
            ShopAttachmentImage1InputDetails omedl2 = new ShopAttachmentImage1InputDetails();
            String APIHostingPort = System.Configuration.ConfigurationSettings.AppSettings["APIHostingPort"];
            string UploadFileDirectory = "~/CommonFolder";
            try
            {
                var hhhh = Newtonsoft.Json.JsonConvert.DeserializeObject<ShopAttachmentImage1InputDetails>(model.data);

                if (!string.IsNullOrEmpty(model.data))
                {

                    ImageName = model.attachment_image1.FileName;
                    string vPath = Path.Combine(Server.MapPath("~/CommonFolder/MultipleShopImages"), ImageName);
                    model.attachment_image1.SaveAs(vPath);
                }

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];

                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();

                sqlcmd = new SqlCommand("PRC_APISHOPATTACHMENTIMAGEINFO", sqlcon);
                sqlcmd.Parameters.Add("@Action", "SAVEATTACHMENTIMAGE1");
                sqlcmd.Parameters.Add("@user_id", hhhh.user_id);
                sqlcmd.Parameters.Add("@SHOP_ID", hhhh.shop_id);
                sqlcmd.Parameters.Add("@AttachmentImage1", "/CommonFolder/MultipleShopImages/" + ImageName);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Attachment Image1 Save Successfully.";
                    omodel.user_id = hhhh.user_id;
                    omodel.Attachment_image1_link = APIHostingPort + Convert.ToString(dt.Rows[0]["AttachmentImage1"]);
                }
                else
                {
                    omodel.status = "202";
                    omodel.message = "Attachment Image1 Save Fail.";
                }
            }
            catch (Exception msg)
            {
                omodel.status = "204" + ImageName;
                omodel.message = msg.Message;
            }
            return Json(omodel);
        }

        [HttpPost]
        public JsonResult ShopAttachmentImage2(ShopAttachmentImage2Input model)
        {
            ShopAttachmentImage2Output omodel = new ShopAttachmentImage2Output();
            string ImageName = "";
            ShopAttachmentImage2InputDetails omedl2 = new ShopAttachmentImage2InputDetails();
            String APIHostingPort = System.Configuration.ConfigurationSettings.AppSettings["APIHostingPort"];
            string UploadFileDirectory = "~/CommonFolder";
            try
            {
                var hhhh = Newtonsoft.Json.JsonConvert.DeserializeObject<ShopAttachmentImage2InputDetails>(model.data);

                if (!string.IsNullOrEmpty(model.data))
                {

                    ImageName = model.attachment_image2.FileName;
                    string vPath = Path.Combine(Server.MapPath("~/CommonFolder/MultipleShopImages"), ImageName);
                    model.attachment_image2.SaveAs(vPath);
                }

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];

                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();

                sqlcmd = new SqlCommand("PRC_APISHOPATTACHMENTIMAGEINFO", sqlcon);
                sqlcmd.Parameters.Add("@Action", "SAVEATTACHMENTIMAGE2");
                sqlcmd.Parameters.Add("@user_id", hhhh.user_id);
                sqlcmd.Parameters.Add("@SHOP_ID", hhhh.shop_id);
                sqlcmd.Parameters.Add("@AttachmentImage2", "/CommonFolder/MultipleShopImages/" + ImageName);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Attachment Image2 Save Successfully.";
                    omodel.user_id = hhhh.user_id;
                    omodel.Attachment_image2_link = APIHostingPort + Convert.ToString(dt.Rows[0]["AttachmentImage2"]);
                }
                else
                {
                    omodel.status = "202";
                    omodel.message = "Attachment Image2 Save Fail.";
                }
            }
            catch (Exception msg)
            {
                omodel.status = "204" + ImageName;
                omodel.message = msg.Message;
            }
            return Json(omodel);
        }

        [HttpPost]
        public JsonResult ShopAttachmentImage3(ShopAttachmentImage3Input model)
        {
            ShopAttachmentImage3Output omodel = new ShopAttachmentImage3Output();
            string ImageName = "";
            ShopAttachmentImage3InputDetails omedl2 = new ShopAttachmentImage3InputDetails();
            String APIHostingPort = System.Configuration.ConfigurationSettings.AppSettings["APIHostingPort"];
            string UploadFileDirectory = "~/CommonFolder";
            try
            {
                var hhhh = Newtonsoft.Json.JsonConvert.DeserializeObject<ShopAttachmentImage3InputDetails>(model.data);

                if (!string.IsNullOrEmpty(model.data))
                {

                    ImageName = model.attachment_image3.FileName;
                    string vPath = Path.Combine(Server.MapPath("~/CommonFolder/MultipleShopImages"), ImageName);
                    model.attachment_image3.SaveAs(vPath);
                }

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];

                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();

                sqlcmd = new SqlCommand("PRC_APISHOPATTACHMENTIMAGEINFO", sqlcon);
                sqlcmd.Parameters.Add("@Action", "SAVEATTACHMENTIMAGE3");
                sqlcmd.Parameters.Add("@user_id", hhhh.user_id);
                sqlcmd.Parameters.Add("@SHOP_ID", hhhh.shop_id);
                sqlcmd.Parameters.Add("@AttachmentImage3", "/CommonFolder/MultipleShopImages/" + ImageName);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Attachment Image3 Save Successfully.";
                    omodel.user_id = hhhh.user_id;
                    omodel.Attachment_image3_link = APIHostingPort + Convert.ToString(dt.Rows[0]["AttachmentImage3"]);
                }
                else
                {
                    omodel.status = "202";
                    omodel.message = "Attachment Image3 Save Fail.";
                }
            }
            catch (Exception msg)
            {
                omodel.status = "204" + ImageName;
                omodel.message = msg.Message;
            }
            return Json(omodel);
        }

        [HttpPost]
        public JsonResult ShopAttachmentImage4(ShopAttachmentImage4Input model)
        {
            ShopAttachmentImage4Output omodel = new ShopAttachmentImage4Output();
            string ImageName = "";
            ShopAttachmentImage4InputDetails omedl2 = new ShopAttachmentImage4InputDetails();
            String APIHostingPort = System.Configuration.ConfigurationSettings.AppSettings["APIHostingPort"];
            string UploadFileDirectory = "~/CommonFolder";
            try
            {
                var hhhh = Newtonsoft.Json.JsonConvert.DeserializeObject<ShopAttachmentImage4InputDetails>(model.data);

                if (!string.IsNullOrEmpty(model.data))
                {

                    ImageName = model.attachment_image4.FileName;
                    string vPath = Path.Combine(Server.MapPath("~/CommonFolder/MultipleShopImages"), ImageName);
                    model.attachment_image4.SaveAs(vPath);
                }

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];

                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();

                sqlcmd = new SqlCommand("PRC_APISHOPATTACHMENTIMAGEINFO", sqlcon);
                sqlcmd.Parameters.Add("@Action", "SAVEATTACHMENTIMAGE4");
                sqlcmd.Parameters.Add("@user_id", hhhh.user_id);
                sqlcmd.Parameters.Add("@SHOP_ID", hhhh.shop_id);
                sqlcmd.Parameters.Add("@AttachmentImage4", "/CommonFolder/MultipleShopImages/" + ImageName);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Attachment Image4 Save Successfully.";
                    omodel.user_id = hhhh.user_id;
                    omodel.Attachment_image4_link = APIHostingPort + Convert.ToString(dt.Rows[0]["AttachmentImage4"]);
                }
                else
                {
                    omodel.status = "202";
                    omodel.message = "Attachment Image4 Save Fail.";
                }
            }
            catch (Exception msg)
            {
                omodel.status = "204" + ImageName;
                omodel.message = msg.Message;
            }
            return Json(omodel);
        }
        //End of Rev Debashis Row: 761 to 765

    }
}

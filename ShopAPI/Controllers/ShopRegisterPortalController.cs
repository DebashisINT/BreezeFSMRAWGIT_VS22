using Newtonsoft.Json.Linq;
using ShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace ShopAPI.Controllers
{
    public class ShopRegisterPortalController : Controller
    {
        [AcceptVerbs("POST")]

        public JsonResult RegisterShop(RegisterShopInputData model)
        {
            RegisterShopOutput omodel = new RegisterShopOutput();
            RegisterShopInputPortal omm = new RegisterShopInputPortal();
            string ImageName = "";
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
                            case "added_date":

                                omm.added_date = DateTime.Now;

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
                                omm.AreaId = value;
                                break;
                            case "CityId":
                                omm.CityId = value;
                                break;
                            case "Entered_by":
                                omm.Entered_by = value;
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
                sqlcmd.Parameters.Add("@shop_image", ImageName);
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
                sqlcmd.Parameters.Add("@AreaId", omm.AreaId);
                sqlcmd.Parameters.Add("@CityId", omm.CityId);
                sqlcmd.Parameters.Add("@Entered_by", omm.Entered_by);

                sqlcmd.Parameters.Add("@entity_id", omm.entity_id);
                sqlcmd.Parameters.Add("@party_status_id", omm.party_status_id);

                sqlcmd.Parameters.Add("@retailer_id", omm.retailer_id);
                sqlcmd.Parameters.Add("@dealer_id", omm.dealer_id);
                sqlcmd.Parameters.Add("@beat_id", omm.beat_id);

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
        static byte[] PadLines(byte[] bytes, int rows, int columns)
        {
            int currentStride = columns; // 3
            int newStride = columns;  // 4
            byte[] newBytes = new byte[newStride * rows];
            for (int i = 0; i < rows; i++)
                Buffer.BlockCopy(bytes, currentStride * i, newBytes, newStride * i, currentStride);
            return newBytes;
        }

        public JsonResult EditShop(RegisterShopInputData model)
        {
            RegisterShopOutput omodel = new RegisterShopOutput();
            RegisterShopInputPortal omm = new RegisterShopInputPortal();
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
                                omm.AreaId = value;
                                break;
                            case "CityId":
                                omm.CityId = value;
                                break;
                            case "Entered_by":
                                omm.Entered_by = value;
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
                sqlcmd.Parameters.Add("@shop_image", ImageName);
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
                sqlcmd.Parameters.Add("@AreaId", omm.AreaId);
                sqlcmd.Parameters.Add("@CityId", omm.CityId);
                sqlcmd.Parameters.Add("@Entered_by", omm.Entered_by);


                sqlcmd.Parameters.Add("@entity_id", omm.entity_id);
                sqlcmd.Parameters.Add("@party_status_id", omm.party_status_id);

                sqlcmd.Parameters.Add("@retailer_id", omm.retailer_id);
                sqlcmd.Parameters.Add("@dealer_id", omm.dealer_id);
                sqlcmd.Parameters.Add("@beat_id", omm.beat_id);

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

        [AcceptVerbs("POST")]
        public JsonResult CustomerSyncinShop(RegisterShopInputData model)
        {
            RegisterShopOutput omodel = new RegisterShopOutput();
            RegisterShopInputPortal omm = new RegisterShopInputPortal();
            string ImageName = "";
            try
            {
                ShopRegister oview = new ShopRegister();
               // ImageName = model.shop_image.FileName;
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

                                omm.added_date = DateTime.Now;

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
                                omm.AreaId = value;
                                break;
                            case "CityId":
                                omm.CityId = value;
                                break;
                            case "Entered_by":
                                omm.Entered_by = value;
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
                            case "IsServicePoint":
                                omm.IsServicePoint = value;
                                break;
                        }
                    }
                }
                catch (Exception msg)
                {
                    omodel.status = "206" + ImageName;
                    omodel.message = msg.Message;
                }

                
                string sessionId = "";

                //using (StreamWriter stream = new FileInfo((Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(uploadtext)))).AppendText())
                //{
                //    stream.WriteLine("   Shop Add  Start " + "Start Time:" + DateTime.Now.ToString() + "End Time:" + DateTime.Now + "Status:Records not updated(ShopRegistration/RegisterShop)[202]" + "User ID:" + omm.user_id +    omm.added_date   +   ImageName+    model.shop_image.ContentLength  +     model.shop_image.ContentType+"   Shop add  END ");
                //}

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PROC_API_CustomerSyncForShop", sqlcon);
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
                sqlcmd.Parameters.Add("@shop_image", ImageName);
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
                sqlcmd.Parameters.Add("@AreaId", omm.AreaId);
                sqlcmd.Parameters.Add("@CityId", omm.CityId);
                sqlcmd.Parameters.Add("@Entered_by", omm.Entered_by);

                sqlcmd.Parameters.Add("@entity_id", omm.entity_id);
                sqlcmd.Parameters.Add("@party_status_id", omm.party_status_id);

                sqlcmd.Parameters.Add("@retailer_id", omm.retailer_id);
                sqlcmd.Parameters.Add("@dealer_id", omm.dealer_id);
                sqlcmd.Parameters.Add("@beat_id", omm.beat_id);
                sqlcmd.Parameters.Add("@IsServicePoint", omm.IsServicePoint);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToString(dt.Rows[0]["returncode"]) == "202")
                    {
                        omodel.status = "202";
                        omodel.message = "Shop Name Not found.";
                    }
                    else if (Convert.ToString(dt.Rows[0]["returncode"]) == "203")
                    {
                        omodel.status = "203";
                        omodel.message = "Entity Code not found.";
                    }
                    else if (Convert.ToString(dt.Rows[0]["returncode"]) == "204")
                    {
                        omodel.status = "204";
                        omodel.message = "Owner Name Not found";
                    }
                    else if (Convert.ToString(dt.Rows[0]["returncode"]) == "205")
                    {
                        omodel.status = "205";
                        omodel.message = "Shop Address not found";
                    }
                    else if (Convert.ToString(dt.Rows[0]["returncode"]) == "206")
                    {
                        omodel.status = "206";
                        omodel.message = "Pin Code not found.";
                    }
                    else if (Convert.ToString(dt.Rows[0]["returncode"]) == "207")
                    {
                        omodel.status = "207";
                        omodel.message = "Owner Contact not found.";
                    }
                    else if (Convert.ToString(dt.Rows[0]["returncode"]) == "208")
                    {
                        omodel.status = "208";
                        omodel.message = "User or session token not matched";                        
                    }
                    else if (Convert.ToString(dt.Rows[0]["returncode"]) == "209")
                    {
                        omodel.status = "209";
                        omodel.message = "Duplicate shop Id or contact number";
                    }
                    else if (Convert.ToString(dt.Rows[0]["returncode"]) == "210")
                    {
                        omodel.status = "210";
                        omodel.message = "Duplicate contact number";
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

        [AcceptVerbs("POST")]
        public JsonResult UserSync(RegisterShopInputData model)
        {
            EmployeeSyncModel omodel = new EmployeeSyncModel();
            EmployeeSyncInput omm = new EmployeeSyncInput();
            string ImageName = "";
            try
            {
                ShopRegister oview = new ShopRegister();
                // ImageName = model.shop_image.FileName;
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
                            case "Branch":
                                {
                                    omm.Branch = value;
                                    break;
                                }

                            case "cnt_UCC":
                                {
                                    omm.cnt_UCC = value;
                                    break;
                                }

                            case "Salutation":
                                {
                                    omm.Salutation = value;
                                    break;
                                }


                            case "FirstName":
                                {
                                    omm.FirstName = value;
                                    break;
                                }

                            case "MiddleName":
                                {
                                    omm.MiddleName = value;
                                    break;
                                }

                            case "LastName":
                                {
                                    omm.LastName = value;
                                    break;
                                }

                            case "ContactType":
                                {
                                    omm.ContactType = value;
                                    break;
                                }

                            case "ReferedBy":
                                {
                                    omm.ReferedBy = value;
                                    break;
                                }

                            case "DOB":
                                {
                                    omm.DOB = value;
                                    break;
                                }

                            case "MaritalStatus":
                                {

                                    omm.MaritalStatus = value;
                                    break;
                                }

                            case "AnniversaryDate":
                                {
                                    omm.AnniversaryDate = value;
                                    break;
                                }
                            case "Sex":

                                omm.Sex = value;
                                break;
                            case "CreateDate":
                                omm.CreateDate = value;
                                break;
                            case "CreateUser":
                                omm.CreateUser = value;
                                break;
                            case "Bloodgroup":

                                omm.Bloodgroup = value;
                                break;
                            case "SettlementMode":

                                omm.SettlementMode = value;
                                break;

                            case "ContractDeliveryMode":

                                omm.ContractDeliveryMode = value;
                                break;

                            case "DirectTMClient":

                                omm.DirectTMClient = value;
                                break;

                            case "RelationshipWithDirector":

                                omm.RelationshipWithDirector =value;
                                break;
                            case "HasOtherAccount":

                                omm.HasOtherAccount = value;
                                break;
                            case "Is_Active":

                                omm.Is_Active = value;
                                break;
                            case "cnt_IdType":
                                omm.cnt_IdType = value;
                                break;
                            case "AccountGroupID":
                                omm.AccountGroupID = value;
                                break;
                            case "DateofJoining":
                                omm.DateofJoining = value;
                                break;

                            case "Organization":
                                omm.Organization = value;
                                break;
                            case "JobResponsibility":
                                omm.JobResponsibility = value;
                                break;
                            case "Designation":
                                omm.Designation = value;
                                break;
                            case "emp_type":
                                omm.emp_type = value;
                                break;
                            case "Department":
                                omm.Department = value;
                                break;
                            case "ReportTo":
                                omm.ReportTo = value;
                                break;
                            case "Deputy":
                                omm.Deputy = value;
                                break;
                            case "Colleague":
                                omm.Colleague = value;
                                break;
                            case "workinghours":
                                omm.workinghours = value;
                                break;
                            case "TotalLeavePA":
                                omm.TotalLeavePA = value;
                                break;
                            case "LeaveSchemeAppliedFrom":
                                omm.LeaveSchemeAppliedFrom = string.IsNullOrEmpty(value) ? Convert.ToString(default(DateTime)) : value;
                                break;
                            case "username":
                                omm.username = value;
                                break;
                            case "Encryptpass":
                                omm.Encryptpass = value;
                                break;

                            case "UserLoginId":
                                omm.UserLoginId = value;
                                break;
                            case "usergroup":
                                omm.usergroup = value;
                                break;
                        }
                    }
                }
                catch (Exception msg)
                {
                    omodel.status = "206" + ImageName;
                    omodel.message = msg.Message;
                }


                string sessionId = "";

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("FTS_APIEmployeeUserSync", sqlcon);
                sqlcmd.Parameters.Add("@Branch", omm.Branch);
                sqlcmd.Parameters.Add("@cnt_UCC", omm.cnt_UCC);
                sqlcmd.Parameters.Add("@Salutation", omm.Salutation);
                sqlcmd.Parameters.Add("@FirstName", omm.FirstName);
                sqlcmd.Parameters.Add("@MiddleName", omm.MiddleName);
                sqlcmd.Parameters.Add("@LastName", omm.LastName);
                sqlcmd.Parameters.Add("@ContactType", omm.ContactType);
                sqlcmd.Parameters.Add("@ReferedBy", omm.ReferedBy);
                sqlcmd.Parameters.Add("@DOB", omm.DOB);
                sqlcmd.Parameters.Add("@MaritalStatus", omm.MaritalStatus);
                sqlcmd.Parameters.Add("@AnniversaryDate", omm.AnniversaryDate);
                sqlcmd.Parameters.Add("@Sex", omm.Sex);
                sqlcmd.Parameters.Add("@CreateDate", omm.CreateDate);
                sqlcmd.Parameters.Add("@CreateUser", omm.CreateUser);
                sqlcmd.Parameters.Add("@Bloodgroup", omm.Bloodgroup);
                sqlcmd.Parameters.Add("@SettlementMode", omm.SettlementMode);
                sqlcmd.Parameters.Add("@ContractDeliveryMode", omm.ContractDeliveryMode);
                sqlcmd.Parameters.Add("@DirectTMClient", omm.DirectTMClient);
                sqlcmd.Parameters.Add("@RelationshipWithDirector", omm.RelationshipWithDirector);
                sqlcmd.Parameters.Add("@HasOtherAccount", omm.HasOtherAccount);
                sqlcmd.Parameters.Add("@Is_Active", omm.Is_Active);
                sqlcmd.Parameters.Add("@cnt_IdType", omm.cnt_IdType);
                sqlcmd.Parameters.Add("@AccountGroupID", omm.AccountGroupID);
                sqlcmd.Parameters.Add("@DateofJoining", omm.DateofJoining);
                sqlcmd.Parameters.Add("@Organization", omm.Organization);
                sqlcmd.Parameters.Add("@JobResponsibility", omm.JobResponsibility);
                sqlcmd.Parameters.Add("@Designation", omm.Designation);
                sqlcmd.Parameters.Add("@Department", omm.Department);
                sqlcmd.Parameters.Add("@ReportTo", omm.ReportTo);
                sqlcmd.Parameters.Add("@Deputy", omm.Deputy);
                sqlcmd.Parameters.Add("@Colleague", omm.Colleague);
                sqlcmd.Parameters.Add("@workinghours", omm.workinghours);
                sqlcmd.Parameters.Add("@TotalLeavePA", omm.TotalLeavePA);
                sqlcmd.Parameters.Add("@LeaveSchemeAppliedFrom", omm.LeaveSchemeAppliedFrom);
                sqlcmd.Parameters.Add("@username", omm.username);
                sqlcmd.Parameters.Add("@Encryptpass", omm.Encryptpass);
                sqlcmd.Parameters.Add("@UserLoginId", omm.UserLoginId);
                sqlcmd.Parameters.Add("@usergroup", omm.usergroup);

                sqlcmd.Parameters.Add("@emp_typeName", omm.emp_type);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                  if (Convert.ToString(dt.Rows[0]["returncode"]) == "202")
                    {
                        omodel.status = "202";
                        omodel.message = "Unique Code Duplicate.";
                    }
                    else
                    {
                        omodel.status = "200";
                        omodel.message = "Success.";
                        omodel.Cnt_id = Convert.ToString(dt.Rows[0]["Cnt_id"]);
                        omodel.User_id = Convert.ToString(dt.Rows[0]["User_id"]);
                    }
                }
                else
                {
                    omodel.status = "205";
                    omodel.message = "Failed";
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
    }
}
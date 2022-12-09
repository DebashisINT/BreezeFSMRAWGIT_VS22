using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using System.IO;
using System.Web;
using ShopAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace ShopAPI.Controllers
{
    public class AddShopController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage RegisterShop(RegisterShopInputPortal model)
        {
            RegisterShopOutput omodel = new RegisterShopOutput();
            try
            {
                ShopRegister oview = new ShopRegister();
                string sessionId = "";

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PROC_API_CustomerSyncForShop", sqlcon);
                sqlcmd.Parameters.Add("@session_token", model.session_token);
                sqlcmd.Parameters.Add("@shop_name", model.shop_name);
                sqlcmd.Parameters.Add("@address", model.address);
                sqlcmd.Parameters.Add("@pin_code", model.pin_code);
                sqlcmd.Parameters.Add("@shop_lat", model.shop_lat);
                sqlcmd.Parameters.Add("@shop_long", model.shop_long);
                sqlcmd.Parameters.Add("@owner_name", model.owner_name);
                sqlcmd.Parameters.Add("@owner_contact_no", model.owner_contact_no);
                sqlcmd.Parameters.Add("@owner_email", model.owner_email);
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@shop_image", "");
                sqlcmd.Parameters.Add("@type", model.type);
                sqlcmd.Parameters.Add("@dob", Convert.ToString(model.dob) == "" ? null : model.dob);
                sqlcmd.Parameters.Add("@date_aniversary", Convert.ToString(model.date_aniversary) == "" ? null : model.date_aniversary);
                sqlcmd.Parameters.Add("@shop_id", model.shop_id);
                sqlcmd.Parameters.Add("@added_date", model.added_date);
                sqlcmd.Parameters.Add("@assigned_to_pp_id", model.assigned_to_pp_id);
                sqlcmd.Parameters.Add("@assigned_to_dd_id", model.assigned_to_dd_id);
                sqlcmd.Parameters.Add("@amount", model.amount);
                sqlcmd.Parameters.Add("@family_member_dob", Convert.ToString(model.family_member_dob) == "" ? null : model.family_member_dob);
                sqlcmd.Parameters.Add("@addtional_dob", Convert.ToString(model.addtional_dob) == "" ? null : model.addtional_dob);
                sqlcmd.Parameters.Add("@addtional_doa", Convert.ToString(model.addtional_doa) == "" ? null : model.addtional_doa);
                sqlcmd.Parameters.Add("@director_name", model.director_name);
                sqlcmd.Parameters.Add("@key_person_name", model.key_person_name);
                sqlcmd.Parameters.Add("@phone_no", model.phone_no);
                sqlcmd.Parameters.Add("@DOC_FAMILY_MEMBER_DOB", model.doc_family_member_dob);
                sqlcmd.Parameters.Add("@SPECIALIZATION", model.specialization);
                sqlcmd.Parameters.Add("@AVG_PATIENT_PER_DAY", model.average_patient_per_day);
                sqlcmd.Parameters.Add("@CATEGORY", model.category);
                sqlcmd.Parameters.Add("@DOC_ADDRESS", model.doc_address);
                sqlcmd.Parameters.Add("@DOC_PINCODE", model.doc_pincode);
                sqlcmd.Parameters.Add("@IsChamberSameHeadquarter", model.is_chamber_same_headquarter);
                sqlcmd.Parameters.Add("@Remarks", model.is_chamber_same_headquarter_remarks);
                sqlcmd.Parameters.Add("@CHEMIST_NAME", model.chemist_name);
                sqlcmd.Parameters.Add("@CHEMIST_ADDRESS", model.chemist_address);
                sqlcmd.Parameters.Add("@CHEMIST_PINCODE", model.chemist_pincode);
                sqlcmd.Parameters.Add("@ASSISTANT_NAME", model.assistant_name);
                sqlcmd.Parameters.Add("@ASSISTANT_CONTACT_NO", model.assistant_contact_no);
                sqlcmd.Parameters.Add("@ASSISTANT_DOB", model.assistant_dob);
                sqlcmd.Parameters.Add("@ASSISTANT_DOA", model.assistant_doa);
                sqlcmd.Parameters.Add("@ASSISTANT_FAMILY_DOB", model.assistant_family_dob);
                sqlcmd.Parameters.Add("@EntityCode", model.EntityCode);
                sqlcmd.Parameters.Add("@Entity_Location", model.Entity_Location);
                sqlcmd.Parameters.Add("@Alt_MobileNo", model.Alt_MobileNo);
                sqlcmd.Parameters.Add("@Entity_Status", model.Entity_Status);
                sqlcmd.Parameters.Add("@Entity_Type", model.Entity_Type);
                sqlcmd.Parameters.Add("@ShopOwner_PAN", model.ShopOwner_PAN);
                sqlcmd.Parameters.Add("@ShopOwner_Aadhar", model.ShopOwner_Aadhar);
                sqlcmd.Parameters.Add("@EntityRemarks", model.Remarks);
                sqlcmd.Parameters.Add("@AreaId", model.AreaId);
                sqlcmd.Parameters.Add("@CityId", model.CityId);
                sqlcmd.Parameters.Add("@Entered_by", model.Entered_by);
                sqlcmd.Parameters.Add("@entity_id", model.entity_id);
                sqlcmd.Parameters.Add("@party_status_id", model.party_status_id);
                sqlcmd.Parameters.Add("@retailer_id", model.retailer_id);
                sqlcmd.Parameters.Add("@dealer_id", model.dealer_id);
                sqlcmd.Parameters.Add("@beat_id", model.beat_id);
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
                omodel.status = "204";
                omodel.message = msg.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK, omodel);
        }
    }
}

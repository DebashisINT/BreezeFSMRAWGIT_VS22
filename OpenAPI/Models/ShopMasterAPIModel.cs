/*******************************************************************************************************
 * Written by Sanchita for V2.0.39 on 03/03/2023 - Implement Open API for Shop Master
 * New project OpenAPI added
 ********************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAPI.Models
{
    public class ShopsMasterAPIDatalists
    {
        public string session_token { get; set; }

        public List<ShopsMastersAPI> shopmaster_list { get; set; }
    }

    public class ShopsMasterAPIInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }

        public int Uniquecont { get; set; }
    }

    public class ShopsMasterAPIOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public ShopsMasterAPIDatalists data { get; set; }
    }

    public class ShopsMastersAPI
    {
        public string shop_id { get; set; }
        public string shop_name { get; set; }

        public string address { get; set; }

        public string pin_code { get; set; }

        public string shop_lat { get; set; }

        public string shop_long { get; set; }

        public string owner_name { get; set; }

        public string owner_contact_no { get; set; }

        public string owner_email { get; set; }

        public string Shop_Image { get; set; }

        public DateTime? dob { get; set; }

        public DateTime? date_aniversary { get; set; }

        public DateTime? last_visit_date { get; set; }

        public int total_visit_count { get; set; }

        public int isAddressUpdated { get; set; }
        public int type { get; set; }
        public string Shoptype { get; set; }
        public string assigned_to_pp_id { get; set; }
        public string assigned_to_dd_id { get; set; }

        public bool is_otp_verified { get; set; }
        public decimal amount { get; set; }

        public Nullable<DateTime> family_member_dob { get; set; }
        public string director_name { get; set; }
        public string key_person_name { get; set; }
        public string phone_no { get; set; }
        public Nullable<DateTime> addtional_dob { get; set; }
        public Nullable<DateTime> addtional_doa { get; set; }
        
        public string degree { get; set; }
        public Nullable<DateTime> doc_family_member_dob { get; set; }
        public string specialization { get; set; }
        public string average_patient_per_day { get; set; }
        public string category { get; set; }
        public string doc_address { get; set; }
        public string doc_pincode { get; set; }
        public string is_chamber_same_headquarter { get; set; }
        public string is_chamber_same_headquarter_remarks { get; set; }
        public string chemist_name { get; set; }
        public string chemist_address { get; set; }
        public string chemist_pincode { get; set; }
        public string assistant_name { get; set; }
        public string assistant_contact_no { get; set; }
        public Nullable<DateTime> assistant_dob { get; set; }
        public Nullable<DateTime> assistant_doa { get; set; }
        public Nullable<DateTime> assistant_family_dob { get; set; }
        
        public string entity_code { get; set; }

        public string area_id { get; set; }

        public string model_id { get; set; }
        public string primary_app_id { get; set; }
        public string secondary_app_id { get; set; }
        public string lead_id { get; set; }
        public string funnel_stage_id { get; set; }
        public string stage_id { get; set; }
        public decimal booking_amount { get; set; }
        
        public string type_id { get; set; }
       
        public DateTime added_date { get; set; }
        
        public string entity_id { get; set; }
        public string party_status_id { get; set; }
        public string retailer_id { get; set; }
        public string dealer_id { get; set; }
        public string beat_id { get; set; }
        public string account_holder { get; set; }
        public string account_no { get; set; }
        public string bank_name { get; set; }
        public string ifsc_code { get; set; }
        public string upi { get; set; }
        public string assigned_to_shop_id { get; set; }
       
        public string project_name { get; set; }
        public string landline_number { get; set; }
        public string agency_name { get; set; }
        public string lead_contact_number { get; set; }
        public string alternateNoForCustomer { get; set; }
        public string whatsappNoForCustomer { get; set; }
        public bool isShopDuplicate { get; set; }
        public string purpose { get; set; }
        public string GSTN_Number { get; set; }
        public string ShopOwner_PAN { get; set; }
       
    }
}

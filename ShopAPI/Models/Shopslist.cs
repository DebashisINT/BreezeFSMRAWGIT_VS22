#region======================================Revision History=========================================================
//1.0   V2.0.37     Debashis    10/01/2023      Some new parameters have been added.Row: 788
//2.0   V2.0.38     Debashis    02/02/2023      Some new parameters have been added.Row: 810 to 811
//3.0   V2.0.39     Debashis    24/04/2023      Some new parameters have been added.Row: 822
//4.0   V2.0.40     Debashis    30/06/2023      Some new parameters have been added.Row: 852
//5.0   V2.0.42     Debashis    06/10/2023      One new parameter has been added.Row: 867,870 & 873
//6.0   V2.0.43     Debashis    22/12/2023      Some new parameters have been added.Row: 892,895 & 898
//7.0   V2.0.45     Debashis    14/03/2024      Some new parameters have been added.Row: 902 & Refer: 0027309
//8.0   V2.0.45     Debashis    03/04/2024      One new parameter has been added.Row: 914
//9.0   V2.0.45     Debashis    11/04/2024      One new parameter has been added.Row: 917 & 918
//10.0  V2.0.45     Debashis    23/04/2025      One new parameter has been added.Row: 921
//11.0  V2.0.46     Debashis    24/04/2025      One new parameter has been added.Row: 923
//12.0  V2.0.46     Debashis    24/04/2025      One new parameter has been added.Row: 924
//13.0  V2.0.48     Debashis    05/08/2024      Some new methods have been added.Row: 965
#endregion===================================End of Revision History==================================================

using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class ShopslistInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }

        public int Uniquecont { get; set; }
    }

    public class ShopslistOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public Datalists data { get; set; }
    }

    public class ArealistInput
    {

        public string session_token { get; set; }
        public string user_id { get; set; }
        public int city_id { get; set; }
        public string creater_user_id { get; set; }
    }

    public class Datalists
    {

        public string session_token { get; set; }

        public List<Shopslists> shop_list { get; set; }

    }

    public class Shopslists
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

        //rev start for more details
        public Nullable<DateTime> family_member_dob { get; set; }
        public string director_name { get; set; }
        public string key_person_name { get; set; }
        public string phone_no { get; set; }
        public Nullable<DateTime> addtional_dob { get; set; }
        public Nullable<DateTime> addtional_doa { get; set; }
        //rev End for more details


        //rev start for more details for doctor
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
        //rev End for more details for doctor

        public string entity_code { get; set; }

        public string area_id { get; set; }

        //Rev Start extar data for Rajgarhia
        public string model_id { get; set; }
        public string primary_app_id { get; set; }
        public string secondary_app_id { get; set; }
        public string lead_id { get; set; }
        public string funnel_stage_id { get; set; }
        public string stage_id { get; set; }
        public decimal booking_amount { get; set; }
        //Rev End extar data for Rajgarhia

        //Rev Start Add new parameter party type id for Mescab
        public string type_id { get; set; }
        //Rev End Add new parameter party type id for Mescab

        //Rev Start Add new parameter Shop Create date
        public DateTime added_date { get; set; }
        //Rev End Add new parameter Shop Create date
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
        //Rev Debashis for EuroBond
        public string project_name { get; set; }
        public string landline_number { get; set; }
        public string agency_name { get; set; }
        public string lead_contact_number { get; set; }
        public string alternateNoForCustomer { get; set; }
        public string whatsappNoForCustomer { get; set; }
        public bool isShopDuplicate { get; set; }
        public string purpose { get; set; }
        //End of Rev Debashis for EuroBond
        //Rev Debashis ROW:756
        public string GSTN_Number { get; set; }
        public string ShopOwner_PAN { get; set; }
        //End of Rev Debashis ROW:756
        //Rev 5.0 Row: 870 & 876
        public string FSSAILicNo { get; set; }
        public bool isUpdateAddressFromShopMaster { get; set; }
        //End of Rev 5.0 Row: 870 & 876
        //Rev 6.0 Row: 898
        public string shop_firstName { get; set; }
        public string shop_lastName { get; set; }
        public string crm_companyName { get; set; }
        public int crm_companyID { get; set; }
        public string crm_jobTitle { get; set; }
        public string crm_type { get; set; }
        public int crm_typeID { get; set; } 
        public int crm_statusID { get; set; }
        public string crm_status { get; set; }
        public string crm_source { get; set; }
        public int crm_sourceID { get; set; }
        public string crm_reference { get; set; }
        public string crm_referenceID { get; set; }
        public string crm_referenceID_type { get; set; }
        public string crm_stage { get; set; }
        public int crm_stage_ID { get; set; }
        public int assign_to { get; set; }
        public string saved_from_status { get; set; }
        //End of Rev 6.0 Row: 898
        //Rev 10.0 Row: 921
        public string remarks { get; set; }
        //End of Rev 10.0 Row: 921
        //Rev 12.0 Row: 924
        public string Shop_NextFollowupDate { get; set; }
        //End of Rev 12.0 Row: 924
    }

    public class ArealistsOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<Arealists> area_list { get; set; }

    }

    public class Arealists
    {
        public string area_id { get; set; }
        public string area_name { get; set; }
    }

    public class ShopTypelistsOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<ShopType> Shoptype_list { get; set; }

    }

    public class ShopType
    {
        public string shoptype_id { get; set; }
        public string shoptype_name { get; set; }

    }

    public class ShopTypelistInput
    {
        [Required]
        public string session_token { get; set; }
        [Required]
        public string user_id { get; set; }
    }

    public class AddShopInput
    {
        [Required]
        public string session_token { get; set; }
        [Required]
        public string user_id { get; set; }

        [Required]
        public string shop_name { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        public string pin_code { get; set; }
        [Required]
        public string shop_lat { get; set; }
        [Required]
        public string shop_long { get; set; }
        [Required]
        public string owner_name { get; set; }
        [Required]
        public string owner_contact_no { get; set; }
        
        public string owner_email { get; set; }
        [Required]
        public int? type { get; set; }
        public string dob { get; set; }
        public string date_aniversary { get; set; }
        [Required]
        public string shop_id { get; set; }
        public Nullable<DateTime> added_date { get; set; }

        public string assigned_to_pp_id { get; set; }
        public string assigned_to_dd_id { get; set; }
        public string amount { get; set; }

        public Nullable<DateTime> family_member_dob { get; set; }
        public string director_name { get; set; }
        public string key_person_name { get; set; }
        public string phone_no { get; set; }
        public Nullable<DateTime> addtional_dob { get; set; }
        public Nullable<DateTime> addtional_doa { get; set; }

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
        public string EntityCode { get; set; }
        public string Entity_Location { get; set; }
        public string Alt_MobileNo { get; set; }
        public string Entity_Status { get; set; }
        public string Entity_Type { get; set; }
        public string ShopOwner_PAN { get; set; }
        public string ShopOwner_Aadhar { get; set; }
        public string Remarks { get; set; }
        public string area_id { get; set; }
        public string CityId { get; set; }
        public string model_id { get; set; }
        public string primary_app_id { get; set; }
        public string secondary_app_id { get; set; }
        public string lead_id { get; set; }
        public string funnel_stage_id { get; set; }
        public string stage_id { get; set; }
        public string booking_amount { get; set; }
        //Rev Start Add new parameter party type id for Mescab
        public string type_id { get; set; }
        //Rev End Add new parameter party type id for Mescab
        public string entity_id { get; set; }
        public string party_status_id { get; set; }
        public string retailer_id { get; set; }
        public string dealer_id { get; set; }
        public string beat_id { get; set; }
        public string assigned_to_shop_id { get; set; }
        [Required]
        public string actual_address { get; set; }
        public string shop_revisit_uniqKey { get; set; }
        //Rev Debashis        
        public string agency_name { get; set; }        
        public string lead_contact_number { get; set; }
        public string project_name { get; set; }
        public string landline_number { get; set; }
        public string alternateNoForCustomer { get; set; }
        public string whatsappNoForCustomer { get; set; }
        public string shopStatusUpdate { get; set; }
        public bool isShopDuplicate { get; set; }
        public string purpose { get; set; }
        public string GSTN_Number { get; set; }
        //End of Rev Debashis
        //Rev 5.0 Row: 867 & 873
        public string FSSAILicNo { get; set; }
        public string isUpdateAddressFromShopMaster { get; set; }
        //End of Rev 5.0 Row: 867 & 873
        //Rev 6.0 Row: 892 & 895
        public string shop_firstName { get; set; }
        public string shop_lastName { get; set; }
        public int crm_companyID { get; set; }
        public string crm_jobTitle { get; set; }
        public int crm_typeID { get; set; }
        public int crm_statusID { get; set; }
        public int crm_sourceID { get; set; }
        public string crm_referenceID { get; set; }
        public string crm_referenceID_type { get; set; }
        public int crm_stage_ID { get; set; }
        public string assign_to { get; set; }
        public string saved_from_status { get; set; }
        //End of Rev 6.0 Row: 892 & 895
        //Rev 9.0 Row: 917 & 918
        public bool isFromCRM { get; set; }
        //End of Rev 9.0 Row: 917 & 918
        //Rev 11.0 Row: 923
        public string Shop_NextFollowupDate { get; set; }
        //End of Rev 11.0 Row: 923
    }

    public class AllShopTypelistsOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<AllShopType> Shoptype_list { get; set; }

    }

    public class AllShopType
    {
        public string shoptype_id { get; set; }
        public string shoptype_name { get; set; }
        public int CurrentStockEnable { get; set; }
        public int CompetitorStockEnable { get; set; }
    }

    //Rev Debashis Row: 675
    public class ShopActivityFeedbackListInput
    {
        public string user_id { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }
        public Int16 date_span { get; set; }
    }

    public class ShopActivityFeedbackListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string user_id { get; set; }
        public List<ShopActListOutput> shop_list { get; set; }
    }

    public class ShopActListOutput
    {
        public string shop_id { get; set; }
        public List<ShopActFeedbackListOutput> feedback_remark_list { get; set; }
    }

    public class ShopActFeedbackListOutput
    {
        public string feedback { get; set; }
        public string date_time { get; set; }
        //Rev 1.0 Row:788
        public string multi_contact_name { get; set; }
        public string multi_contact_number { get; set; }
        //End of Rev 1.0 Row:788
    }
    //End of Rev Debashis Row: 675
    //Rev Debashis Row: 745
    public class SupervisorTeamListInput
    {
        [Required]
        public string session_token { get; set; }
        [Required]
        public string user_id { get; set; }
    }

    public class SupervisorTeamListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public SupervisorTeamDatalists data { get; set; }
    }
    public class SupervisorTeamDatalists
    {
        public string session_token { get; set; }
        public List<SupervisorTeamShopslists> shop_list { get; set; }
    }

    public class SupervisorTeamShopslists
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
    }
    //End of Rev Debashis Row: 745

    //Rev Debashis Row: 761 to 765
    public class ShopAttachmentImageslistInput
    {
        public string session_token { get; set; }
        [Required]
        public string user_id { get; set; }
        [Required]
        public string shop_id { get; set; }
    }

    public class ShopAttachmentImageslistOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string user_id { get; set; }
        public string shop_id { get; set; }
        public List<ShopAttachmentImagesDatalists> image_list { get; set; }
    }

    public class ShopAttachmentImagesDatalists
    {
        public string attachment_image1 { get; set; }
        public string attachment_image2 { get; set; }
        public string attachment_image3 { get; set; }
        public string attachment_image4 { get; set; }

    }
    //End of Rev Debashis Row: 761 to 765

    //Rev 2.0 Row: 810 to 811
    public class ModifiedShopslistInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
    }

    public class ModifiedShopslistOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string user_id { get; set; }
        public List<ModifiedShopslists> modified_shop_list { get; set; }
    }

    public class ModifiedShopslists
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

    public class EditModifiedShopInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public List<EditModifiedShopList> shop_modified_list { get; set; }
    }

    public class EditModifiedShopList
    {
        public string shop_id { get; set; }
    }

        public class EditModifiedShopOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }
    //End of Rev 2.0 Row: 810 to 811
    //Rev 3.0 Row: 822
    public class PartyNotVisitListInput
    {
        public string session_token { get; set; }
        [Required]
        public long user_id { get; set; }
        [Required]
        public string from_date { get; set; }
        [Required]
        public string to_date { get; set; }
    }
    public class PartyNotVisitListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<PartyNotVisitLists> last_visit_order_list { get; set; }
    }
    public class PartyNotVisitLists
    {
        public string shop_name { get; set; }
        public string shop_id { get; set; }
        public int shop_Type { get; set; }
        public string shop_TypeName { get; set; }
        public string last_visited_date { get; set; }
        public string last_order_date { get; set; }
    }
    //End of Rev 3.0 Row: 822
    //Rev 4.0 Row: 852
    public class ShopsInactivelistInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
    }
    public class ShopsInactivelistOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public InactiveDatalists data { get; set; }
    }
    public class InactiveDatalists
    {
        public string session_token { get; set; }
        public List<ShopsInactivelists> shop_list { get; set; }

    }
    public class ShopsInactivelists
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
    //End of Rev 4.0 Row: 852
    //Rev 7.0 Row: 902 & Refer: 0027309
    public class ITCShopAddressEditInput
    {
        public string user_id { get; set; }
        public List<ITCAddressEditShopList> shop_list { get; set; }
    }

    public class ITCAddressEditShopList
    {
        public string shop_id { get; set; }
        public string shop_updated_lat { get; set; }
        public string shop_updated_long { get; set; }
        public string shop_updated_address { get; set; }
        //Rev 8.0 Row: 914
        public string pincode { get; set; }
        //End of Rev 8.0 Row: 914
    }

    public class ITCShopAddressEditOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }
    //End of Rev 7.0 Row: 902 & Refer: 0027309
    //Rev 13.0 Row: 965
    public class FetchShopRevisitAudioInput
    {
        public long user_id { get; set; }
        public int data_limit_in_days { get; set; }
    }

    public class FetchShopRevisitAudioOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<Audio_listOutput> audio_list { get; set; }
    }

    public class Audio_listOutput
    {
        public string shop_id { get; set; }
        public string audio_path { get; set; }
        public string isUploaded { get; set; }
        public string datetime { get; set; }
        public string revisitORvisit { get; set; }
    }
    //End of Rev 13.0 Row: 965
}
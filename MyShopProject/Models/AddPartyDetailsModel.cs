/*************************************************************************************************************
Rev 1.0     Sanchita   V2.0.28    27/01/2023      Bulk modification feature is required in Parties menu. Refer: 25609
Rev 2.0     Priti      V2.0.49    15-11-2024      0027799: A new Global settings required as WillShowLoanDetailsInParty.

*****************************************************************************************************************/
using DevExpress.Web.ASPxTreeList.Internal;
using DevExpress.XtraCharts.Native;
using Microsoft.Owin.BuilderProperties;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Net;
using System.Web;

namespace MyShop.Models
{
    public class AddPartyDetailsModel
    {
        public Int32 Country { get; set; }
        public Int32 State { get; set; }
        public Int32 type { get; set; }
        public string AssignedTo { get; set; }
        public string AssignedToDD { get; set; }
        public string Party_Name { get; set; }
        public string Party_Code { get; set; }
        [Required]
        public string Address { get; set; }
        [Required(ErrorMessage = "//")]
        [RegularExpression(@"^[0-9]{1,3}$", ErrorMessage = "//")]
        public string Pin_Code { get; set; }
        [Required]
        public string owner_name { get; set; }
        public string dobstr { get; set; }
        public string date_aniversarystr { get; set; }
        [Required]
        public string owner_contact_no { get; set; }
        public string Alternate_Contact { get; set; }
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please Enter your valid email")]
        public string owner_email { get; set; }
        public string ShopStatus { get; set; }
        public Int32 EntyType { get; set; }
        public string Owner_PAN { get; set; }
        public string Owner_Adhar { get; set; }
        public string Remarks { get; set; }
        public long NewUser { get; set; }
        public long OldUser { get; set; }

        public string NewUserName { get; set; }
        public string OldUserName { get; set; }

        public string shop_lat { get; set; }
        public string shop_long { get; set; }
        public HttpPostedFile shop_image { get; set; }
        public String image { get; set; }
       

        public string AreaID { get; set; }
        public string CityId { get; set; }
        public string Location { get; set; }

        public string selectedusrid { get; set; }
        public string Is_PageLoad { get; set; }
        public string Fromdate { get; set; }
        public string Todate { get; set; }
        public int Uniquecont { get; set; }

        public string Entity { get; set; }
        public string PartyStatus { get; set; }
        public string GroupBeat { get; set; }
        public string AccountHolder { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string IFSCCode { get; set; }
        public string UPIID { get; set; }
        public string retailer_id { get; set; }
        public string dealer_id { get; set; }
        public String Retaile { get; set; }
        public string AssignedToRetaile { get; set; }

        public List<string> empcode { get; set; }
        public List<string> StateIds { get; set; }

        public string Shop_code { get; set; }

        public string Shoptype { get; set; }
        public List<shopTypes> ShpTypeList { get; set; }

        public string PPCode { get; set; }
        public List<PPList> PPCodeList { get; set; }

        public string EntityType { get; set; }
        public List<EntityTypes> EntityTypeList { get; set; }

        public string NewUserid { get; set; }
        public List<Usersshopassign> NewUseridList { get; set; }

        public string OldUserid { get; set; }
        public List<Usersshopassign> OldUseridList { get; set; }

        public string StateID { get; set; }
        public List<StateListShop> StateList { get; set; }

        public string CountryID { get; set; }
        public List<CountryList> CountryList { get; set; }

        public string DDCode { get; set; }

        public string user_id { get; set; }

        public string FromLogdate { get; set; }
        public string ToLogdate { get; set; }

        public string InactiveUserid { get; set; }
        public List<Usersshopassign> InactiveUseridList { get; set; }

        public string activeReAssgnUserid { get; set; }
        public List<Usersshopassign> ActiveReassgnUseridList { get; set; }

        public String IS_ReAssignedDate { get; set; }


        public string EntityId { get; set; }
        public List<clsGroupBeat> EntityList { get; set; }

        public string PartyStatusId { get; set; }
        public List<clsGroupBeat> PartyStatusList { get; set; }

        public string GroupBeatId { get; set; }
        public List<clsGroupBeat> GroupBeatList { get; set; }
        //Mantis Issue 24571
        public string Cluster { get; set; }
        public string GSTN_NUMBER { get; set; }
        public string Trade_Licence_Number { get; set; }
        public string Alt_MobileNo1 { get; set; }
        public string Shop_Owner_Email2 { get; set; }
        //End of Mantis Issue 24571
        //Mantis Issue 25133
        public string OldGroupBeatId { get; set; }
        public List<GroupBeatAssign> InactiveGroupBeatidList { get; set; }
        //End of Mantis Issue 25133

        // Mantis Issue 25545
        public string AreaRouteBeatUserid { get; set; }
        public List<Usersshopassign> AreaRouteBeatUseridList { get; set; }

        public string AreaRouteBeatReassignedUserid { get; set; }
        public List<Usersshopassign> AreaRouteBeatReassignedUseridList { get; set; }
        public string OldAreaRouteBeatId { get; set; }
        // End of Mantis Issue 25545
        // Rev 1.0
        public Int32 State_BulkModify { get; set; }
        public List<string> StateIds_BulkModify { get; set; }
        public string StateID_BulkModify { get; set; }
        public List<StateList_BulkModify> StateList_BulkModify { get; set; }
        // End of Rev 1.0


        //REV 2.0
        public string BKT { get; set; }
        public Decimal TOTALOUTSTANDING { get; set; }
        public Decimal POS { get; set; }
        public Decimal EMIAMOUNT { get; set; }
        public Decimal ALLCHARGES { get; set; }
        public Decimal TOTALCOLLECTABLE { get; set; }
        public string RISK { get; set; }
        public string WORKABLE { get; set; }
        public string DISPOSITIONCODE { get; set; }
        public string PTPDATE { get; set; }
        public Decimal PTPAMOUNT { get; set; }
        public string COLLECTIONDATE { get; set; }
        public Decimal COLLECTIONAMOUNT { get; set; }
        public string FINALSTATUS { get; set; }

        public Int64 RiskId { get; set; }
        public List<LOANTypes> RiskList { get; set; }

        public string WORKABLEID { get; set; }
        public List<WORKABLEVALUE> WORKABLELIST { get; set; }


        public Int64 DISPOSITIONID { get; set; }
        public List<LOANTypes> DISPOSITIONCODELIST { get; set; }

        public Int64 FINALSTATUSID { get; set; }
        public List<LOANTypes> FINALSTATUSLIST { get; set; }


        //REV 2.0 END


    }

    public class PPList
    {
        public string Shop_Code { get; set; }
        public string Shop_Name { get; set; }
    }

    public class EntityTypes
    {
        public string TypeID { get; set; }
        public string TypeName { get; set; }
    }

    public class StateListShop
    {
        public string StateID { get; set; }
        public string StateName { get; set; }
    }

    public class CityListShop
    {
        public string CityID { get; set; }
        public string CityName { get; set; }
    }

    public class AreaListShop
    {
        public string AreaID { get; set; }
        public string AreaName { get; set; }
    }

    public class RegisterShopInputData
    {

        public string data { get; set; }

        public HttpPostedFileBase shop_image { get; set; }

    }
    public class RegisterShopOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string session_token { get; set; }
        public ShopRegister data { get; set; }
    }


    public class ShopRegister
    {

        public string session_token { get; set; }

        public string user_id { get; set; }

        public string shop_name { get; set; }

        public string address { get; set; }

        public string pin_code { get; set; }

        public string shop_lat { get; set; }

        public string shop_long { get; set; }

        public string owner_name { get; set; }

        public string owner_contact_no { get; set; }

        public string owner_email { get; set; }


        public string dob { get; set; }

        public string date_aniversary { get; set; }

        public int? type { get; set; }
        public string shop_id { get; set; }
        public string assigned_to_dd_id { get; set; }
        public string assigned_to_pp_id { get; set; }


    }

    public class RegisterShopInput
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
        [Required]
        public string owner_email { get; set; }
        public int? type { get; set; }
        public string dob { get; set; }
        public string date_aniversary { get; set; }

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
        public string AreaId { get; set; }
        public string CityId { get; set; }
        public string Old_userID { get; set; }
        public string State_ID { get; set; }

        public string Entered_by { get; set; }

        public string retailer_id { get; set; }
        public string dealer_id { get; set; }
        public string entity_id { get; set; }
        public string party_status_id { get; set; }
        public string beat_id { get; set; }
        public string AccountHolder { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string IFSCCode { get; set; }
        public string UPIID { get; set; }
        public string assigned_to_shop_id { get; set; }
        //Mantis Issue 24571
        public string Cluster { get; set; }
        public string GSTN_NUMBER { get; set; }
        public string Trade_Licence_Number { get; set; }
        public string Alt_MobileNo1 { get; set; }
        public string Shop_Owner_Email2 { get; set; }
        //End of Mantis Issue 24571


        //REV 2.0
        public string BKT { get; set; }
        public Decimal TOTALOUTSTANDING { get; set; }
        public Decimal POS { get; set; }
        public Decimal EMIAMOUNT { get; set; }
        public Decimal ALLCHARGES { get; set; }
        public Decimal TOTALCOLLECTABLE { get; set; }
        public string RISK { get; set; }
        public string WORKABLE { get; set; }
        public string DISPOSITIONCODE { get; set; }
        public string PTPDATE { get; set; }
        public Decimal PTPAMOUNT { get; set; }
        public string COLLECTIONDATE { get; set; }
        public Decimal COLLECTIONAMOUNT { get; set; }
        public string FINALSTATUS { get; set; }
        public Int64 RiskId { get; set; }
        public string WORKABLEID { get; set; }    
        public Int64 DISPOSITIONID { get; set; }      
        public Int64 FINALSTATUSID { get; set; }



        //REV 2.0 END


    }

    public class clsGroupBeat
    {
        public string id { get; set; }
        public string name { get; set; }
    }
    // Rev 1.0
    public class StateList_BulkModify
    {
        public string StateID_BulkModify { get; set; }
        public string StateName_BulkModify { get; set; }
    }
    // End of Rev 1.0
}


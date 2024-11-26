#region======================================Revision History=========================================================================
//1.0   V2.0.38     Debashis    23/01/2023      Multiple contact information to be displayed in the Shops report.
//                                              Refer: 0025585
//2.0   V2.0.39    PRITI       13/02/2023      0025663:Last Visit fields shall be available in Outlet Reports*@
//3.0   V2.0.45    Sanchita    22/01/2024      Supervisor name column is required in Shops report. Mantis: 27199
#endregion===================================End of Revision History==================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{

    public class ShopslistInput
    {
        public string selectedusrid { get; set; }
        public string StateId { get; set; }
        public List<GetUsersshops> userlsit { get; set; }
        public List<GetUsersStates> states { get; set; }
        public string Fromdate { get; set; }
        public string Todate { get; set; }
        public int Uniquecont { get; set; }

        public string Ispageload { get; set; }

        public string KeyId { get; set; }
        //Rev Debashis 0025198
        public List<string> BranchId { get; set; }
        public List<GetBranch> modelbranch = new List<GetBranch>();
        //End of Rev Debashis 0025198
    }


    public class GetUsersshops
    {
        public string UserID { get; set; }
        public string username { get; set; }
    }


    public class GetUsersStates
    {
        public string ID { get; set; }
        public string StateName { get; set; }
    }

    public class GetUsersDepartment
    {
        public string cost_id { get; set; }
        public string cost_description { get; set; }
    }

    public class GetUsersSupervisor
    {
        public string cnt_internalId { get; set; }
        public string REPORTTO { get; set; }
    }

    public class GetUsersCountry
    {
        public string cou_id { get; set; }
        public string cou_country { get; set; }
    }


    public class Shopslists
    {

        public string shop_Auto { get; set; }
        public string UserName { get; set; }
        public string shop_id { get; set; }

        public string shop_name { get; set; }
        public string EntityCode { get; set; }

        public string user_name { get; set; }


        [Required]
        public string address { get; set; }
        [Required]
        public string pin_code { get; set; }

        public string shop_lat { get; set; }

        public string shop_long { get; set; }
        [Required]
        public string owner_name { get; set; }
        [Required]
        public string owner_contact_no { get; set; }


        //[Required]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please Enter your valid email")]
        public string owner_email { get; set; }

        public string Shop_Image { get; set; }


        public string PP { get; set; }

        public string DD { get; set; }

        public string Entity_Location { get; set; }
        public string Entity_Status { get; set; }
        public string Specification { get; set; }       
        public string ShopOwner_PAN { get; set; }
        public string ShopOwner_Aadhar { get; set; }

        public DateTime? Shop_CreateTime { get; set; }

        public DateTime? dob { get; set; }

        public DateTime? date_aniversary { get; set; }
        public string Shoptype { get; set; }

        public List<shopTypes> shptypes { get; set; }

        public string dobstr { get; set; }

        public string date_aniversarystr { get; set; }
        public string time_shop { get; set; }

        public int countactivity { get; set; }

        public string Lastactivitydate { get; set; }
        public string EMPCODE { get; set; }
        public string EMPNAME { get; set; }
        public string STATE { get; set; }
        public string user_loginId { get; set; }
        public string REPORTTO { get; set; }
        public Int32 type { get; set; }


        public List<Usersshopassign> userslist { get; set; }
        public string Assign_To { get; set; }
        public string statename { get; set; }

        //Rev Debashis -- 0024575
        public string District { get; set; }
        public string Cluster { get; set; }
        //End of Rev Debashis -- 0024575
        //Rev Debashis -- 0024576
        public string SubType { get; set; }
        //End of Rev Debashis -- 0024576
        //Rev Debashis -- 0024577
        public string Alt_MobileNo1 { get; set; }
        public string Shop_Owner_Email2 { get; set; }
        //End of Rev Debashis -- 0024577
        //Rev work start 30.06.2022  Mantise no:0024573
        public string gstn_number { get; set; }
        public string trade_licence_number { get; set; }
        //Rev work close 30.06.2022  Mantise no:0024573
        //Rev Debashis 0025198
        public string BRANCHDESC { get; set; }
        //End of Rev Debashis 0025198
        // Mantis Issue 25421
        public string Beat { get; set; }
        // End of Mantis Issue 25421

        //Rev end Pallab 25448
        public string AttachmentImage1 { get; set; }
        public string AttachmentImage2 { get; set; }
        public string AttachmentImage3 { get; set; }
        public string AttachmentImage4 { get; set; }
        //Rev end Pallab 25448
        //Rev 1.0 Mantis: 0025585
        public string CONTACT_NAME1 { get; set; }
        public string CONTACT_NUMBER1 { get; set; }
        public string CONTACT_EMAIL1 { get; set; }
        public string CONTACT_DOA1 { get; set; }
        public string CONTACT_DOB1 { get; set; }
        public string CONTACT_NAME2 { get; set; }
        public string CONTACT_NUMBER2 { get; set; }
        public string CONTACT_EMAIL2 { get; set; }
        public string CONTACT_DOA2 { get; set; }
        public string CONTACT_DOB2 { get; set; }
        public string CONTACT_NAME3 { get; set; }
        public string CONTACT_NUMBER3 { get; set; }
        public string CONTACT_EMAIL3 { get; set; }
        public string CONTACT_DOA3 { get; set; }
        public string CONTACT_DOB3 { get; set; }
        public string CONTACT_NAME4 { get; set; }
        public string CONTACT_NUMBER4 { get; set; }
        public string CONTACT_EMAIL4 { get; set; }
        public string CONTACT_DOA4 { get; set; }
        public string CONTACT_DOB4 { get; set; }
        public string CONTACT_NAME5 { get; set; }
        public string CONTACT_NUMBER5 { get; set; }
        public string CONTACT_EMAIL5 { get; set; }
        public string CONTACT_DOA5 { get; set; }
        public string CONTACT_DOB5 { get; set; }
        public string CONTACT_NAME6 { get; set; }
        public string CONTACT_NUMBER6 { get; set; }
        public string CONTACT_EMAIL6 { get; set; }
        public string CONTACT_DOA6 { get; set; }
        public string CONTACT_DOB6 { get; set; }
        //End of Rev 1.0 Mantis: 0025585

        //REV 2.0
        public string LASTVISITDATE { get; set; }
        public string LASTVISITTIME { get; set; }
        public string LASTVISITEDBY { get; set; }
        //REV 2.0 END
        // Rev 3.0
        public string REPORTTO_NAME { get; set; }
        // End of Rev 3.0
    }

    public class shopTypes
    {
        public Int32 ID { get; set; }

        public string Name { get; set; }
    }
    public class Usersshopassign
    {
        public string UserID { get; set; }

        public string username { get; set; }
    }
    //rev Pratik
    public class GetBranch
    {
        public Int64 BRANCH_ID { get; set; }
        public string CODE { get; set; }
    }
    //End of rev Pratik
    //rev Pratik
    public class GetChannel
    {
        public Int64 ch_id { get; set; }
        public string ch_Channel { get; set; }
    }
    //End of rev Pratik
    //Mantis Issue 25133
    public class GroupBeatAssign
    {
        public string ID { get; set; }

        public string NAME { get; set; }
    }
    //End of Mantis Issue 25133


    public class WORKABLEVALUE
    {
        public string ID { get; set; }

        public string NAME { get; set; }
    }


    public class LOANTypes
    {
        public Int64 ID { get; set; }

        public string Name { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class partyDetailsModel
    {
        public String State { get; set; }
        public String City { get; set; }
        public String Area { get; set; }
        public String Type { get; set; }
        public String AssignedToPP { get; set; }
        public String AssignedToDD { get; set; }
        public String PartyName { get; set; }
        public String PartyCode { get; set; }
        public String Address { get; set; }
        public String PinCode { get; set; }
        public String Owner { get; set; }
        public String DOB { get; set; }
        public String Anniversary { get; set; }
        public String Contact { get; set; }
        public String AlternateContact { get; set; }
        public String Email { get; set; }
        public String Status { get; set; }
        public String EntityType { get; set; }
        public String OwnerPAN { get; set; }
        public String OwnerAadhaar { get; set; }
        public String Remarks { get; set; }
        public String AssigntoUser { get; set; }
        public String PartyLocationLat { get; set; }
        public String PartyLocationLang { get; set; }
        public String Location { get; set; }
        public String ImportStatus { get; set; }
        public String ImportMsg { get; set; }
        public String ImportDate { get; set; }
        public String CreateUser { get; set; }
        //Mantis Issue 24572
        public String Cluster { get; set; }
        public String TradeLicense { get; set; }
        public String AlternateContact1 { get; set; }
        public String AlternateEmail { get; set; }
        public String GSTIN { get; set; }
        //End of Mantis Issue 24572
    }

    public class ReAssignShopModel
    {
        public String SHOP_CODE { get; set; }
        public String Shop_Name { get; set; }
        public String Type { get; set; }
        public String Shop_Owner { get; set; }
        public String Shop_Owner_Contact { get; set; }
        public String Address { get; set; }
        public String DD_NAME { get; set; }
        public String PP_NAME { get; set; }

        public String UPDATED_ON { get; set; }
        public String OLD_UserName { get; set; }
        public String New_UserName { get; set; }
    }

    public class ReAssignShopModelLog
    {
        public String SHOP_CODE { get; set; }
        public String Shop_Name { get; set; }
        public String Type { get; set; }
        public String Shop_Owner { get; set; }
        public String Shop_Owner_Contact { get; set; }
        public String Address { get; set; }
        public String DD_NAME { get; set; }
        public String PP_NAME { get; set; }

        public String UPDATED_ON { get; set; }
        public String OLD_UserName { get; set; }
        public String New_UserName { get; set; }

        public String UserName { get; set; }
        public String UserLoginid { get; set; }
        // Mantis Issue 25431
        public string Beat { get; set; }
        // End of Mantis Issue 25431
    }

    public class ReAssignShop
    {
        public String OldUser { get; set; }
        public String NewUser { get; set; }
        public List<String> ShopCode { get; set; }
    }
    //Mantis Issue 25133
    public class ReAssignGroupBeat
    {
        public String OldGroupBeat { get; set; }
        public String NewGroupBeat { get; set; }
        public List<String> ShopCode { get; set; }
    }
    public class ReAssignShopGroupBeatModel
    {
        public String SHOP_CODE { get; set; }
        public String Shop_Name { get; set; }
        public String Type { get; set; }
        public String Shop_Owner { get; set; }
        public String Shop_Owner_Contact { get; set; }
        public String Address { get; set; }
        public String DD_NAME { get; set; }
        public String PP_NAME { get; set; }

        public String UPDATED_ON { get; set; }
        public String OLD_UserName { get; set; }
        public String New_UserName { get; set; }
        public String GroupBeatName { get; set; }
        public String GroupBeatCode { get; set; }
        public String GroupBeatId { get; set; }
    }
    public class ReAssignShopGroupBeatModelLog
    {
        public String SHOP_CODE { get; set; }
        public String Shop_Name { get; set; }
        public String Type { get; set; }
        public String Shop_Owner { get; set; }
        public String Shop_Owner_Contact { get; set; }
        public String Address { get; set; }
        public String DD_NAME { get; set; }
        public String PP_NAME { get; set; }

        public String UPDATED_ON { get; set; }
        public String NEW_GROUPBEAT { get; set; }
        public String OLD_GROUPBEAT { get; set; }

        public String UserName { get; set; }
        public String UserLoginid { get; set; }
    }
    //End of Mantis Issue 25133

}
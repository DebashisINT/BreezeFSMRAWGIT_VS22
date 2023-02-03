#region======================================Revision History=========================================================
//Written By : Debashis Talukder On 10/01/2023
//Purpose: Add mutiple contact for a Shop.Refer: Row:783 to 785 and 799 to 801
#endregion===================================End of Revision History==================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class ShopMultiContactMapInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public List<ShopMultiContactList> shop_list { get; set; }
    }

    public class ShopMultiContactList
    {
        public string shop_id { get; set; }
        public int contact_serial1 { get; set; }
        public string contact_name1 { get; set; }
        public string contact_number1 { get; set; }
        public string contact_email1 { get; set; }
        public string contact_doa1 { get; set; }
        public string contact_dob1 { get; set; }
        public int contact_serial2 { get; set; }
        public string contact_name2 { get; set; }
        public string contact_number2 { get; set; }
        public string contact_email2 { get; set; }
        public string contact_doa2 { get; set; }
        public string contact_dob2 { get; set; }
        public int contact_serial3 { get; set; }
        public string contact_name3 { get; set; }
        public string contact_number3 { get; set; }
        public string contact_email3 { get; set; }
        public string contact_doa3 { get; set; }
        public string contact_dob3 { get; set; }
        public int contact_serial4 { get; set; }
        public string contact_name4 { get; set; }
        public string contact_number4 { get; set; }
        public string contact_email4 { get; set; }
        public string contact_doa4 { get; set; }
        public string contact_dob4 { get; set; }
        public int contact_serial5 { get; set; }
        public string contact_name5 { get; set; }
        public string contact_number5 { get; set; }
        public string contact_email5 { get; set; }
        public string contact_doa5 { get; set; }
        public string contact_dob5 { get; set; }
        public int contact_serial6 { get; set; }
        public string contact_name6 { get; set; }
        public string contact_number6 { get; set; }
        public string contact_email6 { get; set; }
        public string contact_doa6 { get; set; }
        public string contact_dob6 { get; set; }
    }

    public class ShopMultiContactMapOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class EditShopMultiContactMapInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public List<EditShopMultiContactList> shop_list { get; set; }
    }

    public class EditShopMultiContactList
    {
        public string shop_id { get; set; }
        public int contact_serial1 { get; set; }
        public string contact_name1 { get; set; }
        public string contact_number1 { get; set; }
        public string contact_email1 { get; set; }
        public string contact_doa1 { get; set; }
        public string contact_dob1 { get; set; }
        public int contact_serial2 { get; set; }
        public string contact_name2 { get; set; }
        public string contact_number2 { get; set; }
        public string contact_email2 { get; set; }
        public string contact_doa2 { get; set; }
        public string contact_dob2 { get; set; }
        public int contact_serial3 { get; set; }
        public string contact_name3 { get; set; }
        public string contact_number3 { get; set; }
        public string contact_email3 { get; set; }
        public string contact_doa3 { get; set; }
        public string contact_dob3 { get; set; }
        public int contact_serial4 { get; set; }
        public string contact_name4 { get; set; }
        public string contact_number4 { get; set; }
        public string contact_email4 { get; set; }
        public string contact_doa4 { get; set; }
        public string contact_dob4 { get; set; }
        public int contact_serial5 { get; set; }
        public string contact_name5 { get; set; }
        public string contact_number5 { get; set; }
        public string contact_email5 { get; set; }
        public string contact_doa5 { get; set; }
        public string contact_dob5 { get; set; }
        public int contact_serial6 { get; set; }
        public string contact_name6 { get; set; }
        public string contact_number6 { get; set; }
        public string contact_email6 { get; set; }
        public string contact_doa6 { get; set; }
        public string contact_dob6 { get; set; }
    }

    public class EditShopMultiContactMapOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class FetchShopMultiContactMapInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
    }

    public class FetchShopMultiContactMapOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string user_id { get; set; }
        public List<FetchShopMultiContactList> shop_list { get; set; }
    }

    public class FetchShopMultiContactList
    {
        public string shop_id { get; set; }
        public int contact_serial1 { get; set; }
        public string contact_name1 { get; set; }
        public string contact_number1 { get; set; }
        public string contact_email1 { get; set; }
        public string contact_doa1 { get; set; }
        public string contact_dob1 { get; set; }
        public int contact_serial2 { get; set; }
        public string contact_name2 { get; set; }
        public string contact_number2 { get; set; }
        public string contact_email2 { get; set; }
        public string contact_doa2 { get; set; }
        public string contact_dob2 { get; set; }
        public int contact_serial3 { get; set; }
        public string contact_name3 { get; set; }
        public string contact_number3 { get; set; }
        public string contact_email3 { get; set; }
        public string contact_doa3 { get; set; }
        public string contact_dob3 { get; set; }
        public int contact_serial4 { get; set; }
        public string contact_name4 { get; set; }
        public string contact_number4 { get; set; }
        public string contact_email4 { get; set; }
        public string contact_doa4 { get; set; }
        public string contact_dob4 { get; set; }
        public int contact_serial5 { get; set; }
        public string contact_name5 { get; set; }
        public string contact_number5 { get; set; }
        public string contact_email5 { get; set; }
        public string contact_doa5 { get; set; }
        public string contact_dob5 { get; set; }
        public int contact_serial6 { get; set; }
        public string contact_name6 { get; set; }
        public string contact_number6 { get; set; }
        public string contact_email6 { get; set; }
        public string contact_doa6 { get; set; }
        public string contact_dob6 { get; set; }
    }
}
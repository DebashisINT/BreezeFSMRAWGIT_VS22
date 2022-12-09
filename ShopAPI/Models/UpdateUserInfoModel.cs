using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class UpdateUserInfoInput
    {
        public string session_token { get; set; }
        public string name_updation_user_id { get; set; }
        public string updated_name { get; set; }
        public string updated_first_name { get; set; }
        public string updated_middle_name { get; set; }
        public string updated_last_name { get; set; }
        public string updated_by_user_id { get; set; }
        public string updation_date_time { get; set; }
    }

    public class UpdateUserInfoOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string name_updation_user_id { get; set; }
        public string updated_name { get; set; }
    }

    public class UpdateUserLoginInput
    {
        public string update_login_id_of_user_id { get; set; }
        public string user_login_id_new { get; set; }
    }

    public class UpdateUserLoginOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class UpdateUserOtherIDInput
    {
        public string update_other_id_user_contactid { get; set; }
        public string other_id { get; set; }
    }

    public class UpdateUserOtherIDOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }
}
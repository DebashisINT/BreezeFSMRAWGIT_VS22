using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class UserListInputModel
    {
        [Required]
        public string user_id { get; set; }
        public string session_token { get; set; }
    }

    public class UserListOutputModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<userDetails> user_list { get; set; }        
    }

    public class userDetails
    {
        public string user_name { get; set; }
        public string user_login_id { get; set; }
        public long user_id { get; set; }
        public string user_contactid { get; set; }
        public bool isFaceRegistered { get; set; }
        public string face_image_link { get; set; }
        public bool IsPhotoDeleteShow { get; set; }
        public string ShowDDInFaceRegistration { get; set; }
        //Rev Debashis
        public string registration_date_time { get; set; }
        public bool IsAadhaarRegistered { get; set; }
        public string RegisteredAadhaarNo { get; set; }
        public string RegisteredAadhaarDocLink { get; set; }
        public string aadhaar_remarks { get; set; }
        public string aadhar_image_link { get; set; }
        public long type_id { get; set; }
        public string type_name { get; set; }
        public string Registered_with { get; set; }
        public string emp_phone_no { get; set; }
        public bool IsShowManualPhotoRegnInApp { get; set; }
        public bool IsTeamAttenWithoutPhoto { get; set; }
        public bool IsAllowClickForVisitForSpecificUser { get; set; }
        public bool IsActiveUser { get; set; }
        public bool UpdateOtherID { get; set; }
        public bool UpdateUserID { get; set; }
        public string OtherID { get; set; }
        public bool IsShowTypeInRegistrationForSpecificUser { get; set; }
        public string Employee_Designation { get; set; }
        //End of Rev Debashis

    }

    //Rev Debashis  && 19-07-2021
    public class FaceMatchingInputModel
    {
        [Required]
        public string user_id { get; set; }
        public string session_token { get; set; }
    }

    public class FaceMatchingOutputModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public string user_id { get; set; }
        public string face_image_link { get; set; }
    }
    //End of Rev Debashis && 19-07-2021

    //Rev Debashis  && 20-07-2021
    public class FaceImageDeleteInputModel
    {
        [Required]
        public string user_id { get; set; }
        public string session_token { get; set; }
    }

    public class FaceImageDeleteOutputModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public string user_id { get; set; }
    }
    //End of Rev Debashis && 20-07-2021

    public class FaceRegTypeIDInputModel
    {
        [Required]
        public string user_id { get; set; }
        public string session_token { get; set; }
        public long type_id { get; set; }
    }

    public class FaceRegTypeIDOutputModel
    {
        public string status { get; set; }
        public string message { get; set; }
    }
}
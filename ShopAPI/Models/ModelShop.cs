#region======================================Revision History=========================================================
//1.0   V2.0.39     Debashis    21/04/2023      Some new parameters have been added.Row: 819
//2.0   V2.0.39     Debashis    19/05/2023      Some new parameters have been added.Row: 843
//3.0   V2.0.42     Debashis    06/10/2023      Some new parameter has been added.Row: 868,869,874 & 875
//4.0   V2.0.43     Debashis    22/12/2023      Some new parameters have been added.Row: 893,894,896 & 897
//5.0   V2.0.46     Debashis    26/04/2024      A new parameter has been added.Row: 927
#endregion===================================End of Revision History==================================================
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ShopAPI.Models
{

    #region Login
    public class ClassLogin
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }

        public string latitude { get; set; }
        public string longitude { get; set; }

        public string login_time { get; set; }

        //[Required]
        public string Imei { get; set; }
        public string version_name { get; set; }
        public string address { get; set; }
        public string device_token { get; set; }

    }

    public class ChangePassword
    {
        public String session_token { get; set; }
        [Required]
        public string user_id { get; set; }
        [Required]
        public string old_pwd { get; set; }
        [Required]
        public string new_pwd { get; set; }
    }

    public class ChangePassOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class ClassLoginOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string session_token { get; set; }



        public UserClass user_details { get; set; }

        public UserClasscounting user_count { get; set; }
        public List<StateListLogin> state_list { get; set; }
    }

    public class StateListLogin
    {
        public int id { get; set; }
        public string state_name { get; set; }

    }

    public class GetUserInput
    {
        public String session_token { get; set; }
        [Required]
        public string Phone { get; set; }
    }

    public class GetUserOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string user_id { get; set; }
    }


    public class UserClass
    {

        public string user_id { get; set; }
        public string name { get; set; }

        public string email { get; set; }
        public string phone_number { get; set; }

        public string imeino { get; set; }
        public string success { get; set; }
        public string version_name { get; set; }
        public string address { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public int country { get; set; }
        public int state { get; set; }
        public string city { get; set; }
        public string pincode { get; set; }
        public string profile_image { get; set; }
        public string isAddAttendence { get; set; }
        public string isOnLeave { get; set; }
        public string add_attendence_time { get; set; }
        public string Gps_Accuracy { get; set; }
        public string willAlarmTrigger { get; set; }
        public string idle_time { get; set; }
        public string home_lat { get; set; }
        public string home_long { get; set; }
        public string user_login_time { get; set; }
        public string user_logout_time { get; set; }
        public string isFieldWorkVisible { get; set; }
        //Add two extra parameter Tanmoy
        public string distributor_name { get; set; }
        public string market_worked { get; set; }
        //Add two extra parameter Tanmoy
        //Add two extra parameter Debashis
        public bool IsOnLeaveForToday { get; set; }
        public string OnLeaveForTodayStatus { get; set; }
        //Add two extra parameter Debashis
        //Rev 1.0 Row: 819
        public string profile_latitude { get; set; }
        public string profile_longitude { get; set; }
        //End of Rev 1.0 Row: 819
        //Rev 2.0 Row: 843
        public int visit_location_id { get; set; }
        public int area_location_id { get; set; }
        public string area_location_name { get; set; }
        //End of Rev 2.0 Row: 843
        //Rev 5.0 Row: 927
        public bool user_ShopStatus { get; set; }
        //End of Rev 5.0 Row: 927
    }
    public class ClassLoginINput_Route
    {

        public string UserID { get; set; }
        public string session_token { get; set; }
    }
    public class ClassLoginOutput_Route
    {
        public string status { get; set; }
        public string message { get; set; }
        //Rev Debashis Row: 781
        public string JointVisitSelectedUserName { get; set; }
        public string JointVisit_Employee_Code { get; set; }
        //End of Rev Debashis Row: 781
        public List<WorkTypeslogin> worktype { get; set; }
        public List<RouteDetailsOutputuser> route_list { get; set; }
    }

    public class WorkTypeslogin
    {
        public long id { get; set; }

        public string name { get; set; }

    }

    public class RouteDetailsOutputuser
    {
        public string id { get; set; }

        public string route_name { get; set; }
        public List<RouteShoptclass> shop_details_list { get; set; }
    }


    public class RouteShoptclass
    {
        public string shop_id { get; set; }
        public string shop_address { get; set; }
        public string shop_name { get; set; }
        public string shop_contact_no { get; set; }
    }



    public class UserClasscounting
    {
        public int total_time_spent_at_shop { get; set; }
        public int total_shop_visited { get; set; }

        public int total_attendance { get; set; }
    }

    public class GetUserListInput
    {
        public String session_token { get; set; }
    }

    public class GetUserListOutPut
    {
        public String status { get; set; }
        public String message { get; set; }
        public List<GetUserList> UserList { get; set; }
    }

    public class GetUserList
    {
        public String User_ID { get; set; }
        public String User_Name { get; set; }
        public String User_LoginID { get; set; }
        public String InternalID { get; set; }
    }

    #endregion





    #region Registershop

    public class RegisterShopInputDegreeData
    {
        public string data { get; set; }
        public HttpPostedFileBase degree { get; set; }
    }


    public class RegisterShopInputData
    {
        //public RegisterShopInputData()
        //{
        //    data = new RegisterShopInput();
        //}


        public string data { get; set; }

        public HttpPostedFileBase shop_image { get; set; }
        
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
        public string actual_address { get; set; }
        public string shop_revisit_uniqKey { get; set; }
        //Rev Debashis        
        public string agency_name { get; set; }        
        public string lead_contact_number { get; set; }
        public string project_name { get; set; }
        public string landline_number { get; set; }
        public string alternateNoForCustomer { get; set; }
        public string whatsappNoForCustomer { get; set; }
        public bool isShopDuplicate { get; set; }
        public string purpose { get; set; }
        public string GSTN_Number { get; set; }
        //End of Rev Debashis
        //Rev 3.0 Row: 868,869,874 & 875
        public string FSSAILicNo { get; set; }
        public bool isUpdateAddressFromShopMaster { get; set; }
        //End of Rev 3.0 Row: 868,869,874 & 875
        //Rev 4.0 Row: 893,894,896 & 897
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
        public int assign_to { get; set; }
        public string saved_from_status { get; set; }
        //End of Rev 4.0 Row: 893,894,896 & 897
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

        //public string entity_id { get; set; }
        //public string party_status_id { get; set; }



    }

    public class RegisterShopInputPortal
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

        public string Entered_by { get; set; }
        public string entity_id { get; set; }
        public string party_status_id { get; set; }

        public string retailer_id { get; set; }
        public string dealer_id { get; set; }
        public string beat_id { get; set; }
        public string IsServicePoint { get; set; }
    }

    #endregion



    #region Encryption
    public class Encryption
    {
        #region Properties

        private string Password = "3269875";
        private string Salt = "05983654";
        private string HashAlgorithm = "SHA1";
        private int PasswordIterations = 2;
        private string InitialVector = "OFRna73m*aze01xY";
        private int KeySize = 256;

        public string password
        {
            get { return Password; }
        }

        public string salt
        {
            get { return Salt; }
        }

        public string hashAlgo
        {
            get { return HashAlgorithm; }
        }

        public int passwordterations
        {
            get { return PasswordIterations; }
        }

        public string initialvector
        {
            get { return InitialVector; }
        }

        public int keysize
        {
            get { return KeySize; }
        }

        #endregion Properties

        #region Encrypt region

        public string Encrypt(string PlainText)
        {
            if (string.IsNullOrEmpty(PlainText))
                return "";
            byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(initialvector);
            byte[] SaltValueBytes = Encoding.ASCII.GetBytes(salt);
            byte[] PlainTextBytes = Encoding.UTF8.GetBytes(PlainText);
            PasswordDeriveBytes DerivedPassword = new PasswordDeriveBytes(password, SaltValueBytes, hashAlgo, passwordterations);
            byte[] KeyBytes = DerivedPassword.GetBytes(KeySize / 8);
            RijndaelManaged SymmetricKey = new RijndaelManaged();
            SymmetricKey.Mode = CipherMode.CBC;
            byte[] CipherTextBytes = null;
            using (ICryptoTransform Encryptor = SymmetricKey.CreateEncryptor(KeyBytes, InitialVectorBytes))
            {
                using (MemoryStream MemStream = new MemoryStream())
                {
                    using (CryptoStream CryptoStream = new CryptoStream(MemStream, Encryptor, CryptoStreamMode.Write))
                    {
                        CryptoStream.Write(PlainTextBytes, 0, PlainTextBytes.Length);
                        CryptoStream.FlushFinalBlock();
                        CipherTextBytes = MemStream.ToArray();
                        MemStream.Close();
                        CryptoStream.Close();
                    }
                }
            }
            SymmetricKey.Clear();
            return Convert.ToBase64String(CipherTextBytes);
        }

        #endregion Encrypt region

        #region Decrypt Region

        public string Decrypt(string CipherText)
        {
            try
            {
                if (string.IsNullOrEmpty(CipherText))
                    return "";
                byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(initialvector);
                byte[] SaltValueBytes = Encoding.ASCII.GetBytes(salt);
                byte[] CipherTextBytes = Convert.FromBase64String(CipherText);
                PasswordDeriveBytes DerivedPassword = new PasswordDeriveBytes(password, SaltValueBytes, hashAlgo, passwordterations);
                byte[] KeyBytes = DerivedPassword.GetBytes(KeySize / 8);
                RijndaelManaged SymmetricKey = new RijndaelManaged();
                SymmetricKey.Mode = CipherMode.CBC;
                byte[] PlainTextBytes = new byte[CipherTextBytes.Length];
                int ByteCount = 0;
                using (ICryptoTransform Decryptor = SymmetricKey.CreateDecryptor(KeyBytes, InitialVectorBytes))
                {
                    using (MemoryStream MemStream = new MemoryStream(CipherTextBytes))
                    {
                        using (CryptoStream CryptoStream = new CryptoStream(MemStream, Decryptor, CryptoStreamMode.Read))
                        {
                            ByteCount = CryptoStream.Read(PlainTextBytes, 0, PlainTextBytes.Length);
                            MemStream.Close();
                            CryptoStream.Close();
                        }
                    }
                }
                SymmetricKey.Clear();
                return Encoding.UTF8.GetString(PlainTextBytes, 0, ByteCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }

    #endregion



    #region HelperMethod
    public class APIHelperMethods
    {

        public static T ToModel<T>(DataTable dt)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    foreach (PropertyInfo pro in temp.GetProperties())
                    {
                        if (pro.Name == column.ColumnName && dt.Rows[0][column.ColumnName] != DBNull.Value)
                        {
                            try
                            {
                                pro.SetValue(obj, dt.Rows[0][column.ColumnName], null);
                            }
                            catch
                            {

                            }
                        }
                    }
                }
            }

            return obj;
        }

        public static List<T> ToModelList<T>(DataTable dt)
        {
            Type temp = typeof(T);

            List<T> objList = new List<T>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    T obj = Activator.CreateInstance<T>();

                    foreach (DataColumn column in row.Table.Columns)
                    {
                        foreach (PropertyInfo pro in temp.GetProperties())
                        {
                            if (pro.Name == column.ColumnName && row[column.ColumnName] != DBNull.Value)
                            {
                                try
                                {
                                    pro.SetValue(obj, row[column.ColumnName], null);
                                }
                                catch
                                {

                                }
                            }
                        }
                    }

                    objList.Add(obj);
                }
            }

            return objList;
        }




    }
    #endregion


    public class CompetitorImageInput
    {
        public string data { get; set; }
        public HttpPostedFileBase competitor_img { get; set; }
    }

    public class CompetitorImageInputData
    {
        [Required]
        public string session_token { get; set; }
        [Required]
        public string user_id { get; set; }
        [Required]
        public string shop_id { get; set; }
        public string visited_date { get; set; }
    }

    //Rev Debashis Row: 761 to 765
    public class ShopAttachmentImage1Input
    {
        public string data { get; set; }
        public HttpPostedFileBase attachment_image1 { get; set; }
    }

    public class ShopAttachmentImage1InputDetails
    {
        public string user_id { get; set; }
        public string session_token { get; set; }
        public string shop_id { get; set; }
    }

    public class ShopAttachmentImage1Output
    {
        public string status { get; set; }
        public string message { get; set; }
        public string user_id { get; set; }
        public string Attachment_image1_link { get; set; }
    }

    public class ShopAttachmentImage2Input
    {
        public string data { get; set; }
        public HttpPostedFileBase attachment_image2 { get; set; }
    }

    public class ShopAttachmentImage2InputDetails
    {
        public string user_id { get; set; }
        public string session_token { get; set; }
        public string shop_id { get; set; }
    }
    public class ShopAttachmentImage2Output
    {
        public string status { get; set; }
        public string message { get; set; }
        public string user_id { get; set; }
        public string Attachment_image2_link { get; set; }
    }

    public class ShopAttachmentImage3Input
    {
        public string data { get; set; }
        public HttpPostedFileBase attachment_image3 { get; set; }
    }

    public class ShopAttachmentImage3InputDetails
    {
        public string user_id { get; set; }
        public string session_token { get; set; }
        public string shop_id { get; set; }
    }

    public class ShopAttachmentImage3Output
    {
        public string status { get; set; }
        public string message { get; set; }
        public string user_id { get; set; }
        public string Attachment_image3_link { get; set; }
    }

    public class ShopAttachmentImage4Input
    {
        public string data { get; set; }
        public HttpPostedFileBase attachment_image4 { get; set; }
    }

    public class ShopAttachmentImage4InputDetails
    {
        public string user_id { get; set; }
        public string session_token { get; set; }
        public string shop_id { get; set; }
    }

    public class ShopAttachmentImage4Output
    {
        public string status { get; set; }
        public string message { get; set; }
        public string user_id { get; set; }
        public string Attachment_image4_link { get; set; }
    }
    //End of Rev Debashis Row: 761 to 765
}
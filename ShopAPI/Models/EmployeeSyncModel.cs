using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class EmployeeSyncModel
    {
        public String status { get; set; }
        public String message { get; set; }
        public String Cnt_id { get; set; }
        public String User_id { get; set; }
    }

    public class EmployeeSyncInput
    {
        public String Branch { get; set; }
        public String cnt_UCC { get; set; }
        public String Salutation { get; set; }
        public String FirstName { get; set; }
        public String MiddleName { get; set; }
        public String LastName { get; set; }
        //public String cnt_contactSource { get; set; }
        public String ContactType { get; set; }
        //public String cnt_legalStatus { get; set; }
        public String ReferedBy { get; set; }
        public String DOB { get; set; }
        public String MaritalStatus { get; set; }
        public String AnniversaryDate { get; set; }
        //public String cnt_education { get; set; }
        //public String cnt_profession { get; set; }
        public String Sex { get; set; }
        public String CreateDate { get; set; }
        public String CreateUser { get; set; }
        public String Bloodgroup { get; set; }
        //public String WebLogIn { get; set; }
        //public String PassWord { get; set; }
        public String SettlementMode { get; set; }
        public String ContractDeliveryMode { get; set; }
        public String DirectTMClient { get; set; }
        public String RelationshipWithDirector { get; set; }
        public String HasOtherAccount { get; set; }
        public String Is_Active { get; set; }
        public String cnt_IdType { get; set; }
        public String AccountGroupID { get; set; }
        public String DateofJoining { get; set; }
        public String Organization { get; set; }
        public String JobResponsibility { get; set; }
        public String Designation { get; set; }
        public String emp_type { get; set; }
        public String Department { get; set; }
        public String ReportTo { get; set; }
        public String Deputy { get; set; }
        public String Colleague { get; set; }
        public String workinghours { get; set; }
        public String TotalLeavePA { get; set; }
        public String LeaveSchemeAppliedFrom { get; set; }

        public String username { get; set; }
        public String Encryptpass { get; set; }
        public String UserLoginId { get; set; }
        public String usergroup { get; set; }
    }
    //Rev Debashis
    public class UserIMEIClearInput
    {
        public string user_id { get; set; }
        public string session_token { get; set; }
    }

    public class UserIMEIClearOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }
    //End of Rev Debashis
}
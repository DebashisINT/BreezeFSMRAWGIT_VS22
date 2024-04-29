/***************************************************************************************
 * Written by Sanchita on 24/11/2023 for V2.0.43    A new design page is required as Contact (s) under CRM menu. 
 *                                                  Mantis: 27034 
 * 1.0      v2.0.47      19/04/2024      Sanchita   0027384: Contact Module issues in Portal
 ****************************************************************************************/

using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class CRMContactModel
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Fromdate { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Todate { get; set; }

        public List<GetContactFrom> ContactFrom { get; set; }
        public List<string> ContactFromDesc { get; set; }
        public string Is_PageLoad { get; set; }
        public string user_id { get; set; }
       

        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String PhoneNo { get; set; }
        public String Email { get; set; } 
        public String Address { get; set; }
        public String DOB { get; set; }
        public String Anniversarydate { get; set; }
        public String JobTitle { get; set; }

        public int CompanyId { get; set; }
        public List<CompanyList> CompanyList { get; set; }

        public int AssignToId { get; set; }
        public List<AssignToList> AssignToList { get; set; }
        public string AssignedTo { get; set; }
        public string NewUserid { get; set; }
        public List<Usersshopassign> NewUseridList { get; set; }

        public string NewReferenceUserid { get; set; }


        public int TypeId { get; set; }
        public List<TypeList> TypeList { get; set; }

        public int StatusId { get; set; }
        public List<StatusList> StatusList { get; set; }

        public int SourceId { get; set; }
        public List<SourceList> SourceList { get; set; }

        public string ReferenceId { get; set; }
        public List<ReferenceList> ReferenceList { get; set; }

        public int StageId { get; set; }
        public List<StageList> StageList { get; set; }

        public String Remarks { get; set; }

        public decimal ExpSalesValue { get; set; }

        public String NextFollowDate { get; set; }

        public Int64 TotalContacts { get; set; }
        
        // Rev 1.0
        public String Pincode { get; set; }
        public String WhatsappNo { get; set; }
        // End of Rev 1.0
    }
    public class GetContactFrom
    {
        public string ContactFromDesc { get; set; }
    }

    public class AddCrmContactData
    {
        public String shop_code { get; set; }
        public string user_id { get; set; }
        public string Action { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String PhoneNo { get; set; }
        public String Email { get; set; }
        public String Address { get; set; }
        public String DOB { get; set; }
        public String Anniversarydate { get; set; }
        public String JobTitle { get; set; }
        public int CompanyId { get; set; }
        public string AssignedTo { get; set; }
        public int AssignedToId { get; set; }
        public int TypeId { get; set; }
        public int StatusId { get; set; }
        public int SourceId { get; set; }
        public string ReferenceName { get; set; }
        public string ReferenceId { get; set; }
        public int StageId { get; set; }
        public String Remarks { get; set; }
        public decimal ExpSalesValue { get; set; }
        public String NextFollowDate { get; set; }
        public int Active { get; set; }
        // Rev 1.0
        public String Pincode { get; set; }
        public string WhatsappNo { get; set; }
        // End of Rev 1.0
    }



    public class CompanyList
    {
        public String CompanyId { get; set; }
        public String CompanyName { get; set; }
    }

    public class AssignToList
    {
        public String AssignToId { get; set; }
        public String AssignToName { get; set; }
    }

    public class TypeList
    {
        public String TypeId { get; set; }
        public String TypeName { get; set; }
    }

    public class StatusList
    {
        public String StatusId { get; set; }
        public String StatusName { get; set; }
    }

    public class SourceList
    {
        public String SourceId { get; set; }
        public String SourceName { get; set; }
    }

    public class ReferenceList
    {
        public String ReferenceId { get; set; }
        public String ReferenceName { get; set; }
    }

    public class StageList
    {
        public String StageId { get; set; }
        public String StageName { get; set; }
    }

    public class CRMContactImportLogModel
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public String Address { get; set; }
        public String DateofBirth { get; set; }
        public String DateofAnniversary { get; set; }
        public String Company { get; set; }
        public String JobTitle { get; set; }
        public String AssignTo { get; set; }
        public String Type { get; set; }
        public String Status { get; set; }
		public String Source { get; set; }
		public String Reference { get; set; }
		public String Stages { get; set; }
		public String Remarks { get; set; }
		public Decimal ExpectedSalesValue { get; set; }
		public String NextfollowUpDate { get; set; }
		public String Active { get; set; }
		public String ImportStatus { get; set; }
		public String ImportMsg { get; set; }
        public String ImportDate { get; set; }
		public String CreateUser { get; set; }
        // Rev 1.0
        public String Pincode { get; set; }
        public String WhatsappNo { get; set; }
        // End of rev 1.0
    }
}
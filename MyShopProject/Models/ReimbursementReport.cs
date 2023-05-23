/*******************************************************************************************************************
 * 1.0		Sanchita		V2.0.40		24-04-2023		In TRAVELLING ALLOWANCE -- Approve/Reject Page: One Coloumn('Confirm/Reject') required 
													    before 'Approve/Reject' coloumn. refer: 25809
 * ****************************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Models;
using System.Web.Mvc;


namespace Models
{
    public class ReimbursementReport
    {
        public List<string> StateId { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public List<string> desgid { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public List<string> empcode { get; set; }

        public List<GetUsersStates> states { get; set; }

        public List<GetDesignation> designation { get; set; }

        public List<GetAllEmployee> employee { get; set; }

        public string is_pageload { get; set; }
        public List<Months> MonthList { get; set; }

        public List<Years> YearList { get; set; }

        public String Month { get; set; }

        public String Year { get; set; }
    }

    public class Months
    {
        public string ID { get; set; }
        public string MonthName { get; set; }
    }

    public class Years
    {
        public string ID { get; set; }
        public string YearName { get; set; }
    }

    public class ReimbursementDetails
    {

        public string EmpName { get; set; }
        public string Designation { get; set; }
        public string Reportto { get; set; }
        public string State { get; set; }
        public string Userid { get; set; }
        public string Month { get; set; }
        public String Year { get; set; }
        public List<ReimbursementDetailsList> GetReimbursementDetailsList { get; set; }
         
    }
     public class ReimbursementDetailsList
     {
        public Int64 SEQ { get; set; }
        public string ApplicationID { get; set; }
        public string MapExpenseID { get; set; }
        public string Date { get; set; }
        public string user_contactId { get; set; }
        public string Name { get; set; }
        public string deg_designation { get; set; }
        public string Employee_Grade { get; set; }
        public int Visit_type_id { get; set; }
        public string Visit_Location { get; set; }
        public int Expence_type_id { get; set; }
        public string Expense_Type { get; set; }
        public int Mode_of_travel { get; set; }
        public string TravelMode { get; set; }
        public int Fuel_typeId { get; set; }
        public string FuelType { get; set; }
        public string From_location { get; set; }
        public string To_location { get; set; }
        public string Hotel_name { get; set; }
        public string Remark { get; set; }
        public string App_Rej_Remarks { get; set; }
        public decimal Eligible_Dist  { get; set; }
        public decimal Eligible_Amt { get; set; }
        public decimal Applied_Dist { get; set; }
        public decimal Applied_Amt { get; set; }
        public decimal Apprvd_Dist { get; set; }
        public decimal Apprvd_Amt { get; set; }

        public string Userid { get; set; }
        public string Month { get; set; }
        public int is_ApprovedReject { get; set; }
        public string ToDate { get; set; }
        public int is_ApprovedPermision { get; set; }
        public int is_Image { get; set; }
        public int Settings_Allow_Approved_days { get; set; }
        public string Checked_Message { get; set; }

        public string ListAppCode { get; set; }
        public string status { get; set; }

        public string Year { get; set; }
        // Rev 1.0
        public string Conf_Rej_Remarks { get; set; }
        // End of Rev 1.0
    }
     public class ReimbursementDetailsEdit
     {
         public string user_contactId { get; set; }
         public decimal Apprvd_Dist { get; set; }
         public decimal Apprvd_Amt { get; set; }
         public string App_Rej_Remarks { get; set; }
        // Rev 1.0
        public string Conf_Rej_Remarks { get; set; }
        // End of Rev 1.0
     }

     public class ReimbursementApplicationbills
     {
         public string MapExpenseID { get; set; }
         public string Bills { get; set; }
         public string Image_Name { get; set; }
     }


}

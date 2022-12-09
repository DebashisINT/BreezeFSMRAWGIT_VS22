using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class ReimbursementAllowanceReportViewModel
    {
        public DateTime AppliedOn { get; set; }

        public DateTime ForDate { get; set; }

        public String LoginID { get; set; }

        public String EmpName { get; set; }

        public String Grade { get; set; }
        public String Supervisor { get; set; }
        public String StateName { get; set; }
        public String VisitLocation { get; set; }
        public String ExpenseType { get; set; }

        public String ModeOfTravel { get; set; }
        public String FuelType { get; set; }
        public Decimal EligibleDistance { get; set; }

        public Decimal AppliedDistance { get; set; }

        public Decimal TotalTravelled { get; set; }
        public Decimal ApprovedDistance { get; set; }
        public Decimal EligibleRate { get; set; }
        public Decimal AppliedRate { get; set; }

        public Decimal EligibleAmount { get; set; }
        public Decimal AppliedAmount { get; set; }
        public Decimal ApprovedAmount { get; set; }

        public String Status { get; set; }

        public List<DropDownList> StateList { get; set; }

        public List<DropDownList> ExpenseTypeList { get; set; }

        public List<DropDownList> VisitLocationList{ get; set; }

        public List<DropDownList> EmployeeGradeList { get; set; }

        public List<DropDownList> ModeOfTravelList { get; set; }

        public List<DropDownList> FuelTypeList{ get; set; }
    }

    public class DropDownList
    {
        public string Value { get; set; }

        public string Text { get; set; }
    }
}
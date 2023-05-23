/****************************************************************************************************************************
1.0     v2.0.40     Priti    19/05/2023      0026145:Modification in the ‘Configure Travelling Allowance’ page.
*********************************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class TravelConveyanceModelclass
    {

        public List<TravelConveyanceModelclass> conveyancemode { get; set; }
        public List<Class_Master> visitloc { get; set; }
        public List<Class_Master> expensetype { get; set; }
        public List<Class_Master> designation { get; set; }
        public List<Class_Master> travelmode { get; set; }

        public List<Class_Master> fueltype { get; set; }

        public List<Class_Master> empgrade { get; set; }
        public List<Class_Master> state { get; set; }

        public int VisitlocId { get; set; }
        public int ExpenseId { get; set; }
        public int DesignationId { get; set; }
        public int TravelId { get; set; }
        public int StateIdfetch { get; set; }
        public List<string> StateId { get; set; }

        public int EmpgradeIdfetch { get; set; }
        public List<string> EmpgradeId { get; set; }


        public int fuelID { get; set; }
        public decimal EligibleDistance { get; set; }
        public decimal EligibleRate { get; set; }
        public decimal EligibleAmtday { get; set; }

        public bool IsActive { get; set; }
        public bool fueladjust { get; set; }

        public string  VisitlocName { get; set; }
        public string ExpenseName { get; set; }
        public string DesignationName { get; set; }
        public string TravelName { get; set; }
        public string StateName { get; set; }
        public string EmpgradeName { get; set; }
        public string IsActivename { get; set; }

        public string FuelTypes { get; set; }
        public string TCId { get; set; }


        public string DateConveyance { get; set; }
        public long Slno { get; set; }

        //Rev 1.0
        public List<string> BranchId { get; set; }
        public List<string> AreaId { get; set; }
        public string BranchName { get; set; }
        public string AreaName { get; set; }
        //Rev 1.0 End
    }


    public class Class_Master
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public int Expense_Id { get; set; }
        public int Mode { get; set; }
        public bool fueladjust { get; set; }

    }


}
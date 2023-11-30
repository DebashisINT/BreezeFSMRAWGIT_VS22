//********************************************************************************************************************
// 1.0      v2.0.43    Sanchita    16-10-2023  On demand search is required in Product Master & Projection Entry
//                                             Mantis: 26858
//********************************************************************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class ProjectReportModel
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Fromdate { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Todate { get; set; }
        public string Is_PageLoad { get; set; }


        //[DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Date { get; set; }

        public int Month { get; set; }
        public string Year { get; set; }
        public string Shop { get; set; }
        // Rev 1.0
        public string ShopName { get; set; }
        // End of Rev 1.0
        //public int ProjectName { get; set; }
        public string Area { get; set; }
       // public int ShopType { get; set; }
        public string Contact_Person { get; set; }
        public string PhoneNo { get; set; }
        public decimal ApproxQty { get; set; }
        public string Grade { get; set; }
        public string ProdName { get; set; }
        public string ExptMonth { get; set; }
        public string ExptYear { get; set; }
        public string Remarks { get; set; }
        public string OrderLost { get; set; }

        //[DisplayFormat(ConvertEmptyStringToNull = false)]
        public string proj_complete_dt { get; set; }

        // Mantis Issue 25203
        public string ArctName { get; set;  }
        public string ConslName { get; set; }
        public string FabrName { get; set; }
        public string Others { get; set; }
        public string HODRemarks { get; set; }
        // End of Mantis Issue 25203


        public string Action_type { get; set; }
        public string PR_ID { get; set; }

        public string ExecName { get; set; }
        public string response_code { get; set; }
        public string response_msg { get; set; }

        
       // public string Contact_Person { get; set; }
        //public string PhoneNo { get; set; }
        //public decimal ApproxQty { get; set; }
        //public string Grade { get; set; }
        //public string ProdName { get; set; }
        //public string Remarks { get; set; }
        //
        //public string OrderLost { get; set; }



        // Customer Name
        public string ShopUser { get; set; }
        public string ShopUserId { get; set; }
        public List<ShopUserAssign> ShopUserList { get; set; }
        // Rev 1.0
        public string OldShopUser { get; set; }
        // End of 1.0
        //

        // Project Name
        public string ProjectName { get; set; }
        public string ProjectNameId { get; set; }
        public List<ProjectNameAssign> ProjectNameList { get; set; }
        //

        // Category
        public string ShopType { get; set; }
        public string ShopTypeId { get; set; }
        public List<ShopTypeAssign> ShopTypeList { get; set; }
        //

        public string FilterTypes { get; set; }

        // Order Lost Reason Filter
        public string OrderLostReason { get; set; }
        public string OrderLostReasonId { get; set; }
        public List<OrderLostReason> OrderLostReasonList { get; set; }
        //

        // Project Completed Date Filter
        public string ProjectCompletedDT { get; set; }
        public string ProjectCompletedDTId { get; set; }
        public List<ProjectCompletedDT> ProjectCompletedDTList { get; set; }
        //
    }

    public class ShopUserAssign
    {
        public string Shop_Code { get; set; }
        public string Shop_Name { get; set; }
    }

    public class ProjectNameAssign
    {
        public string Project_Id { get; set; }
        public string Project_Name { get; set; }
    }

    public class ShopTypeAssign
    {
        public string shop_typeId { get; set; }
        public string shop_typeName { get; set; }
    }

    public class OrderLostReason
    {
        public string ID { get; set; }
        public string Close_Reason { get; set; }
        public string Close_Reason_Count { get; set; }
    }

    public class ProjectCompletedDT
    {
        public string ID { get; set; }
        public string Completed_Date { get; set; }
        public string Completed_Date_Count { get; set; }
    }
}
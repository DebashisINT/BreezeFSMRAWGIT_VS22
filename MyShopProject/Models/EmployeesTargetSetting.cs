using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class EmployeesTargetSetting
    {
        public Int32 user_id { get; set; }

        public Int32 EmployeeTargetSettingID { get; set; }

        //public String SettingYear { get; set; }

        //public String Brand { get; set; }

        //public String Category { get; set; }

        //public String Items { get; set; }

        //public String Basis { get; set; }

        //public DateTime? ApplicableFrom { get; set; }

        //public String ApplicableFromTxt { get; set; }

        //public DateTime? ValidUpto { get; set; }

        //public Decimal? NewVisit { get; set; }

        ////public Decimal ReVisit { get; set; }

        //public Decimal? SalesOrderValue { get; set; }

        //public Decimal? Collection { get; set; }

        //public String ValidUptoTxt { get; set; }

        //public String SettingType { get; set; }

        //public DateTime? CreatedDate { get; set; }

        public String EmployeeCode { get; set; }

        public String Employeename { get; set; }

        public String state { get; set; }
        public String stage { get; set; }


        public String ContactNo { get; set; }

        public String Designation { get; set; }

        public String Type { get; set; }

        public String MonthName { get; set; }

        public String Supervisor { get; set; }

        public List<EmployeeState> StateList { get; set; }

        public List<EmployeeDesignation> DesignationList { get; set; }

        public EmployeeDesignation UserDesg { get; set; }
        public List<ProductBrand> BrandList { get; set; }

        public List<ProductCategory> CategoryList { get; set; }

        public List<ProductItems> ItemsList { get; set; }

        public List<EmployeeType> TypeList { get; set; }

        public List<EmployeeType> CounterTypeList { get; set; }

        //public String Value { get; set; }

        public List<TargetType> TargetTypeList { get; set; }

        public Int32 EmpTypeID { get; set; }

        //public Int32? VisitType { get; set; }

        //public String xml { get; set; }

        public String SettingMonthYear { get; set; }

        public Int32 OrderValue { get; set; }

        public Int32 NewCounter { get; set; }

        public Int32 CounterType { get; set; }

        //public Int32 Visit { get; set; }

        public Int32 Collection { get; set; }


        public Int32 Revisit { get; set; }

        public Int32 SettingMonth { get; set; }

        public Int32 SettingYear { get; set; }

        public DateTime ModifiedDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public String EmpTypeName { get; set; }

        public String CounterTypeName { get; set; }

        public String EmpCodeList { get; set; }

        public Boolean IsHierarchywiseTargetSettings { get; set; }

        public EmployeeState UserState { get; set; }

        public int UserID { get; set; }

    }

    public class EmployeeType
    {
        public String EmployeesTargetSettingEmpTypeID { get; set; }

        public String TypeName { get; set; }

        public Int32 Type { get; set; }
    }

    public class TargetType
    {
        public Int32? EmployeesTargetSettingTypeID { get; set; }

        public String TypeName { get; set; }
    }

    public class EmployeeState
    {
        public String StateName { get; set; }
    }

    public class ProductBrand
    {
        public Int32? BrandID { get; set; }

        public String BrandName { get; set; }
    }

    public class ProductCategory
    {
        public Int32? CategoryID { get; set; }

        public String CategoryName { get; set; }
    }

    public class ProductItems
    {
        public Int32? ProductID { get; set; }

        public String ProductName { get; set; }
    }

    public class EmployeeDesignation
    {
        public String DesignationName { get; set; }
    }

    public class EmployeesList
    {
        public String EmpName { get; set; }

        public String EmpCode { get; set; }

        public String LoginID { get; set; }

        public String DisplayEmployee { get; set; }

        public Int32 UserID { get; set; }
    }

    public class EmployeesTargetSettingCounterTarget
    {
        public Int32 EmployeesCounterTargetID { get; set; }

        public String EmployeeCode { get; set; }

        public Int32 FKEmployeeTargetSettingID { get; set; }

        public String Shop_Code { get; set; }

        public String Shop_Name { get; set; }

        public Decimal OrderValue { get; set; }

        public Decimal CollectionValue { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public List<ShopPP> ShopPPList { get; set; }

        public String Message { get; set; }

        public Int32 NewOrderValue { get; set; }

        public Int32 NewCollectionValue { get; set; }

        public Boolean OrderUpdate { get; set; }

        public Boolean CollectionUpdate { get; set; }
    }

    public class ShopPP
    {
        public String Shop_Code { get; set; }

        public String Shop_Name { get; set; }
    }

    public class EmployeesTargetSettingImportLog
    {
        public Int32 LogID { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
        public string Supervisor_Assigned { get; set; }
        public string NewCounter { get; set; }
        public string Revisit { get; set; }

        public string TargetValue { get; set; }
        public string TargetCollection { get; set; }
        public string Type { get; set; }
        public bool Status { get; set; }

        public string Message { get; set; }
        public bool IsShow { get; set; }
        public DateTime CreatedDate { get; set; }

        public String CreatedDateTxt { get; set; }

    }
}
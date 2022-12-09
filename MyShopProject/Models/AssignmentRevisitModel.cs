using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class AssignmentRevisitModel
    {
        public Int32 user_id { get; set; }

        public Int32 EmployeeTargetSettingID { get; set; }

        public String EmployeeCode { get; set; }

        public String Employeename { get; set; }

        public String state { get; set; }

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

        public List<TargetType> TargetTypeList { get; set; }

        public Int32 EmpTypeID { get; set; }

        public String SettingMonthYear { get; set; }

        public Int32 OrderValue { get; set; }

        public Int32 NewCounter { get; set; }

        public Int32 CounterType { get; set; }

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

    public class SearchRevisitList
    {
        public List<string> EmployeeID { get; set; }
        public List<string> StateId { get; set; }
        public List<string> desgid { get; set; }
        public string Month { get; set; }
    }


    public class AssigmentRevisitSettingImportLog
    {

        public string FOR_MONTH { get; set; }
        public DateTime? REVISITDATE { get; set; }
        public string LOGIN_ID { get; set; }
        public string EMPLOYEE_NAME { get; set; }
        public string SHOPPIN { get; set; }
        public long SHOP_ID { get; set; }
        public string SHOP_NAME { get; set; }
        public string SHOP_CONTACT { get; set; }
        public string SHOP_ADDRESS { get; set; }
        public string SHOP_STATE { get; set; }
        public string SHOP_TYPE { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public bool STATUS { get; set; }
        public string STATUS_MESSAGE { get; set; }

    }


}
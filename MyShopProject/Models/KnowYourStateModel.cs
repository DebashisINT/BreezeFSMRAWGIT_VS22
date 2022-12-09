using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class KnowYourStateModel
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
        //Rev Rajdip

        //End Rev Rajdip
    }

    //public class SearchRevisitList
    //{
    //    public List<string> EmployeeID { get; set; }
    //    public List<string> StateId { get; set; }
    //    public List<string> desgid { get; set; }
    //    public string Month { get; set; }
    //}


    public class ItemImport
    {

        public string State { get; set; }

        public string STATE_HEAD { get; set; }

        public String EMP_ID { get; set; }

        public String SUPERVISOR { get; set; }

        public String Emp_id1 { get; set; }

        public String Name { get; set; }

        public String Emp_id2 { get; set; }

        public String No_Of_Outlet { get; set; }

        public String Total_Visit { get; set; }

        public String Average_Visit { get; set; }

        public string Order_Inc_Tax { get; set; }

        public string Delivery_Inc_Tax { get; set; }

        public string Pre_Order_Ex_Tax { get; set; }

        public string Pre_Delivery_Ex_Tax { get; set; }

        public string Delivery_Pending_Ex_Tax { get; set; }

        public string No_Of_Outlet_Order_Taken { get; set; }

        public string No_Of_Outlet_Delivery_Done { get; set; }

        public string Repeat_Outlet_Delivery_Done { get; set; }

        public string Durantion_Spend_In_Outlet_Min_to_5_Min { get; set; }

        public string Durantion_Spend_In_Outlet_Min_To_15_Min { get; set; }

        public string Durantion_Spend_In_Outlet_16_Min_Above { get; set; }

        public string Batten_Others_Watts { get; set; }

        public string Batten_18W { get; set; }

        public string Bulb_9W { get; set; }

        public string Bulb_Other_Watts { get; set; }

        public string Bulb_Tri_Colour { get; set; }

        public string Cabinet { get; set; }

        public string Candle { get; set; }

        public string Cob { get; set; }

        public string Downlight { get; set; }

        public string Driver { get; set; }

        public string Emergency_Bulb { get; set; }

        public string Flood_Light { get; set; }

        public string Night_Bulb { get; set; }

        public string Ring { get; set; }

        public string Slim_Panel { get; set; }
        public string Spot_Light { get; set; }

        public string Street_Light { get; set; }

        public string Strip_Light_Nova_Strip { get; set; }

        public string Track_Light { get; set; }

        public string Zoom_Light { get; set; }

        public string Vacant_1 { get; set; }

        public string Vacant_2 { get; set; }

        public string Vacant_3 { get; set; }

        public string Vacant_4 { get; set; }

        public string Vacant_5 { get; set; }

        public string Total_Qty { get; set; }

        public string Created_date { get; set; }

        public string Created_By { get; set; }

        public string Month { get; set; }

        public string Year { get; set; }

    }
    public class BindUser
    {
        public string EmpId1 { get; set; }

        public string EmpId2 { get; set; }
        public string State { get; set; }

        public string STATE_HEAD { get; set; }

        public String EMP_ID { get; set; }

        public String SUPERVISOR { get; set; }

        public String Emp_id1 { get; set; }

        public String Name { get; set; }

        public String Emp_id2 { get; set; }

        public String No_Of_Outlet { get; set; }

        public String Total_Visit { get; set; }

        public String Average_Visit { get; set; }

        public string Order_Inc_Tax { get; set; }

        public string Delivery_Inc_Tax { get; set; }

        public string Pre_Order_Ex_Tax { get; set; }

        public string Pre_Delivery_Ex_Tax { get; set; }

        public string Delivery_Pending_Ex_Tax { get; set; }

        public string No_Of_Outlet_Order_Taken { get; set; }

        public string No_Of_Outlet_Delivery_Done { get; set; }

        public string Repeat_Outlet_Delivery_Done { get; set; }

        public string Durantion_Spend_In_Outlet_Min_to_5_Min { get; set; }

        public string Durantion_Spend_In_Outlet_Min_To_15_Min { get; set; }

        public string Durantion_Spend_In_Outlet_16_Min_Above { get; set; }

        public string Batten_Others_Watts { get; set; }

        public string Batten_18W { get; set; }

        public string Bulb_9W { get; set; }

        public string Bulb_Other_Watts { get; set; }

        public string Bulb_Tri_Colour { get; set; }

        public string Cabinet { get; set; }

        public string Candle { get; set; }

        public string Cob { get; set; }

        public string Downlight { get; set; }

        public string Driver { get; set; }

        public string Emergency_Bulb { get; set; }

        public string Flood_Light { get; set; }

        public string Night_Bulb { get; set; }

        public string Ring { get; set; }

        public string Slim_Panel { get; set; }
        public string Spot_Light { get; set; }

        public string Street_Light { get; set; }

        public string Strip_Light_Nova_Strip { get; set; }

        public string Track_Light { get; set; }

        public string Zoom_Light { get; set; }

        public string Vacant_1 { get; set; }

        public string Vacant_2 { get; set; }

        public string Vacant_3 { get; set; }

        public string Vacant_4 { get; set; }

        public string Vacant_5 { get; set; }

        public string Total_Qty { get; set; }

        public string Created_date { get; set; }

        public string Created_By { get; set; }

        public string Month { get; set; }

        public string Year { get; set; }
    }

}
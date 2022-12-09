using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class WarehouseMasterModel
    {
        public String WarehouseID { get; set; }
        public String WarehouseName { get; set; }
        public String Address1 { get; set; }
        public String Address2 { get; set; }
        public String Address3 { get; set; }
        public String Country { get; set; }
        public String State { get; set; }
        public String CityDistrict { get; set; }
        public String Pin { get; set; }
        public String ContactName { get; set; }
        public String ContactPhone { get; set; }
        public String Distributer { get; set; }
        public String isDefault { get; set; }

        public List<CountryList> Country_List { get; set; }
        public List<StateList> State_List { get; set; }
        public List<CityDistrictList> CityDistrict_List { get; set; }
        public List<DistributerList> Distributer_List { get; set; }
    }

    public class CountryList
    {
        public String cou_id { get; set; }
        public String cou_country { get; set; }
    }

    //public class StatesList
    //{
    //    public String id { get; set; }
    //    public String state { get; set; }
    //}

    public class CityDistrictList
    {
        public String city_id { get; set; }
        public String city_name { get; set; }
    }

    public class DistributerList
    {
        public String Shop_Code { get; set; }
        public String Shop_Name { get; set; }
    }

    public class ShopList
    {
        public String Shop_code { get; set; }
        public String Shop_name { get; set; }
        public String Type { get; set; }
        public String ContactNo { get; set; }
        public String ReportTo { get; set; }
        public String State { get; set; }
        public String Address { get; set; }
    }
}

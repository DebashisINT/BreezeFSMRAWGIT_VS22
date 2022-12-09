using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class HomeLocationModel
    {
        public List<string> Employee { get; set; }
        public string allEmployee { get; set; }
        public string Ispageload { get; set; }
    }


    public class ListHomeLocation
    {
        public string Emp_Name { get; set; }
        public string address { get; set; }
        public string cityname { get; set; }
        public string statename { get; set; }
        public string pin_code { get; set; }
        public string Latitude { get; set; }
        public string longatude { get; set; }
        public string UpdateDate { get; set; }
        public long UserID { get; set; }
    }

    public class HomeLocationDetails
    {
        public string Emp_Name { get; set; }
        public string address { get; set; }
        public string Latitude { get; set; }
        public string longatude { get; set; }
        public string UpdateDate { get; set; }
        public long UserID { get; set; }
        public List<cityLists> citylst { get; set; }
        public List<StateLists> statelst { get; set; }
        public List<CountryLists> Countrylst { get; set; }

        //public string cityID { get; set; }
        //public string stateID { get; set; }
        //public string CountryID { get; set; }

        public long cityID { get; set; }
        public long stateID { get; set; }
        public long CountryID { get; set; }
        public string PinCode { get; set; }
    }

    public class cityLists
    {
        public long city_id { get; set; }
        public string city_name { get; set; }
    }
    public class StateLists
    {
        public long state_id { get; set; }
        public string state_name { get; set; }
    }
    public class CountryLists
    {
        public long cou_id { get; set; }
        public string cou_country { get; set; }
    }

    public class HomeLocationUpdate
    {
        public string address { get; set; }
        public string Latitude { get; set; }
        public string longatude { get; set; }
        public long UserID { get; set; }
        public string cityName { get; set; }
        public string stateName { get; set; }
        public string PinCode { get; set; }
    }
}
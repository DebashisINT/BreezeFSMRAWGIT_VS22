using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    #region Totalscript
    public class ReimbursementModelInput
    {

        public string user_id { get; set; }
        public string state_id { get; set; }
    }
    public class ReimbursementModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<visit_type_details> visit_type_details { get; set; }


    }

    public class visit_type_details
    {
        public string visit_type_id { get; set; }
        public string visit_type_name { get; set; }
        public List<expense_details> expense_details { get; set; }


    }

    public class expense_details
    {

        public string expence_id { get; set; }
        public string expence_type { get; set; }
        public string vehicle_id { get; set; }
        public string vehicle_name { get; set; }

        public string vehicle_image { get; set; }
        public string maximum_allowance { get; set; }
        public string distance { get; set; }

        public string rate { get; set; }
        public List<fuel_type> fuel_type { get; set; }


    }

    public class fuel_type
    {
        public string fuel_id { get; set; }
        public string fuel_name { get; set; }
        public string maximum_allowance { get; set; }
        public string distance { get; set; }

        public string rate { get; set; }

    }

    public class reimburInput
    {
        public string user_id { get; set; }
        public string session_token { get; set; }
        public string date { get; set; }
        public string isEditable { get; set; }
        public string Expense_mapId { get; set; }
        public string Subexpense_MapId { get; set; }
    }

    public class reimburList
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<reimburShopList> shop_visit_list { get; set; }
    }

    public class reimburShopList
    {
        public string shop_id { get; set; }
        public string shop_name { get; set; }
        public string shop_loc { get; set; }
        public string shop_distance { get; set; }
    }

    public class reimburLocList
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<reimburLocationList> loc_list { get; set; }
    }

    public class reimburLocationList
    {
        public string loc_id { get; set; }
        public string loc_name { get; set; }
        public string distance { get; set; }
    }

    public class LocationCaptureInput
    {
        public string user_id { get; set; }
        public string session_token { get; set; }
        public string date { get; set; }
    }

    public class reimburLocationCapture
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<reimburLocationCaptureList> loc_list { get; set; }
    }

    public class reimburLocationCaptureList
    {
        public string loc_id { get; set; }
        public string loc_name { get; set; }
    }

    #endregion

    #region APIConfigSetting


    public class ReimbursementModelInputfetch
    {

        public string user_id { get; set; }
        public string state_id { get; set; }
        public string visittype_id { get; set; }
        public string expense_id { get; set; }
        public string travel_id { get; set; }
        public string fuel_id { get; set; }

    }
    public class ReimbursementModelOutput
    {

        public string status { get; set; }
        public string message { get; set; }
        public string reimbursement_past_days { get; set; }
        public bool isEditable { get; set; }
        public List<LocationDetails> visittype_details { get; set; }
        public List<ExpenseDetails> expense_types { get; set; }
        public List<Travelmodedetails> mode_of_travel { get; set; }

        public List<Fueldetails> fuel_types { get; set; }
       

    }

    public class ReimbursementModelOutputfetch
    {

        public string status { get; set; }
        public string message { get; set; }
        public string maximum_allowance { get; set; }
        public string distance { get; set; }

        public string rate { get; set; }

    }




    public class LocationDetails
    {

        public string visittype_id { get; set; }
        public string visit_name { get; set; }
    }



    public class ExpenseDetails
    {

        public string expanse_id { get; set; }
        public string expanse_type { get; set; }
    }

    public class Travelmodedetails
    {
        public string travel_id { get; set; }
        public string travel_type { get; set; }
        public string expanse_id { get; set; }
        public bool fuel_config { get; set; }
    }

    public class Fueldetails
    {

        public string fuel_type_id { get; set; }
        public string fuel_type { get; set; }
    }


    #endregion


}
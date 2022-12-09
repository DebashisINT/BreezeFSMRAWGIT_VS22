using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    #region ConveyanceApply

    public class ApplyReimbursementModelOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }


    public class ApplyReimbursementModel
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string state_id { get; set; }
        public string date { get; set; }
        public string visit_type_id { get; set; }
        public string Expense_mapId { get; set; }
        public string Expense_Id { get; set; }
        public List<expense_details_Apply> expense_details { get; set; }



    }
    public class expense_details_Apply
    {
        public string expence_type_id { get; set; }
        public string expence_type { get; set; }
        public List<reimbursement_details> reimbursement_details { get; set; }


    }

    public class reimbursement_details
    {


        public string mode_of_travel { get; set; }
        public string from_location { get; set; }
        public string to_location { get; set; }
        public string amount { get; set; }

        public string total_distance { get; set; }
        public string remark { get; set; }
        public string start_date_time { get; set; }
        public string end_date_time { get; set; }
        public string location { get; set; }
        public string hotel_name { get; set; }
        public string food_type { get; set; }

        public string Subexpense_MapId { get; set; }
        public string fuel_id { get; set; }
        public string from_loc_id { get; set; }
        public string to_loc_id { get; set; }
        //public IEnumerable<HttpPostedFileBase> files { get; set; }
    }

    public class reimbursement_details_Inputstructure
    {

        public string user_id { get; set; }
        public string date { get; set; }
        public string visit_type_id { get; set; }
        public string expence_type_id { get; set; }
        public string expence_type { get; set; }
        public string mode_of_travel { get; set; }
        public string from_location { get; set; }
        public string to_location { get; set; }
        public string amount { get; set; }

        public string total_distance { get; set; }
        public string remark { get; set; }
        public string start_date_time { get; set; }
        public string end_date_time { get; set; }
        public string location { get; set; }
        public string hotel_name { get; set; }
        public string food_type { get; set; }

        public string fuel_id { get; set; }
        public string Expense_mapId { get; set; }
        public string Subexpense_MapId { get; set; }
        //Add From_loc_id and to_location id in add Bill Upload Tanmoy 22-11-2019
        public string from_loc_id { get; set; }
        public string to_loc_id { get; set; }
        //End Add From_loc_id and to_location id in add Bill Upload Tanmoy 22-11-2019
    }
    public class reimbursement_details_InputstructureImage
    {

        public string user_id { get; set; }
        public string date { get; set; }
        public string imagename { get; set; }
        public string Expense_mapId { get; set; }
        public string Subexpense_MapId { get; set; }

        public string visit_type_id { get; set; }

        public string Expense_Id { get; set; }
    }

    public class InputstructureImage
    {



        public string data { get; set; }


    }

    #endregion



    #region ConveyanceList

    public class reimbursement_details_Listing_Input
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string month { get; set; }
        public string year { get; set; }
        public string visit_type { get; set; }
    }


    public class reimbursement_details_Listing
    {
        public string status { get; set; }
        public string message { get; set; }
        public string total_claim_amount { get; set; }
        public string total_approved_amount { get; set; }
        //public string visit_type_id { get; set; }
        //public string visit_type { get; set; }

        public List<reimbursement_details_listing_Expense> expense_list { get; set; }
    }

    public class reimbursement_details_listing_Expense
    {

        public string expense_type_id { get; set; }
        public string expense_type { get; set; }
        public string expense_type_image { get; set; }
        public string total_amount { get; set; }

        public List<expense_list_details_listing> expense_list_details { get; set; }

    }

    public class expense_list_details_listing
    {
        public string applied_date { get; set; }
        public string visit_type_id { get; set; }
        public string visit_type { get; set; }
        public string travel_mode_id { get; set; }
        public string travel_mode { get; set; }
        public string amount { get; set; }
        public string approved_amount { get; set; }
        public string hotel_name { get; set; }
        public string food_type { get; set; }
        public string remarks { get; set; }
        public string from_location { get; set; }
        public string to_location { get; set; }
        public string hotel_location { get; set; }
        public string start_date_time { get; set; }
        public string end_date_time { get; set; }
        public string distance { get; set; }
        public string fuel_id { get; set; }
        public string fuel_type { get; set; }

        public string maximum_rate { get; set; }
        public string maximum_allowance { get; set; }
        public string maximum_distance { get; set; }
        public string status { get; set; }

        public string Expense_mapId { get; set; }
                                       
        public string Subexpense_MapId { get; set; }
        public string isEditable { get; set; }

        public List<expense_list_details_listing_Images> image_list { get; set; }


    }

    public class expense_list_details_listing_Images
    {
        public string links { get; set; }
        public string id { get; set; }

    }



    #endregion


    #region Delete


    public class DeleteReimbursement
    {

        public string session_token { get; set; }
        public string user_id { get; set; }
        public string date { get; set; }
        public string visit_type_id { get; set; }
        public string Expense_mapId { get; set; }
        public string Subexpense_MapId { get; set; }
    }

    public class ReimbursementOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }
    #endregion

#region  Delete Image


    public class DeleteReimbursementImage
    {
        public string id { get; set; }
        public string user_id { get; set; }
    }
#endregion
}
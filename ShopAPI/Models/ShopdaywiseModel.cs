#region======================================Revision History=========================================================
//1.0   V2.0.37     Debashis    10/01/2023      Some new parameters have been added.Row: 787
//2.0   V2.0.39     Debashis    24/04/2023      Some new parameters have been added.Row: 821
//3.0   V2.0.40     Debashis    20/06/2023      A new parameter has been added.Row: 850
#endregion===================================End of Revision History==================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{


    public class ShopdaywiseInput
    {

        //  public string session_token { get; set; }
        public int? user_id { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }
        public int? date_span { get; set; }

    }

    public class ShopdaywiseOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public int toal_shopvisit_count { get; set; }
        public int avg_shopvisit_count { get; set; }
        public List<ShopdaywiseList> date_list { get; set; }
    }

    public class ShopdaywiseList
    {
        public string date { get; set; }


        public List<ShopList> shop_list { get; set; }
    }

    public class ShopList
    {

        public string date { get; set; }
        public string shopid { get; set; }
        public string duration_spent { get; set; }
        public string shop_name { get; set; }
        public string shop_address { get; set; }
        public DateTime? visited_date { get; set; }
        public string device_model { get; set; }
        public string android_version { get; set; }
        public string battery { get; set; }
        public string net_status { get; set; }
        public string net_type { get; set; }

        public string in_time { get; set; }
        public string out_time { get; set; }
        public string start_timestamp { get; set; }
        public string in_location { get; set; }
        public string out_location { get; set; }

        public string Key { get; set; }
        public string shop_revisit_status { get; set; }
        public string shop_revisit_remarks { get; set; }
        //Rev Debashis
        public string pros_id { get; set; }
        public string updated_by { get; set; }
        public DateTime? updated_on { get; set; }
        public string agency_name { get; set; }
        public decimal approximate_1st_billing_value { get; set; }
        //End of Rev Debashis
        //Rev 1.0 Row:787
        public string multi_contact_name { get; set; }
        public string multi_contact_number { get; set; }
        //End of Rev 1.0 Row:787
        //Rev 2.0 Row:821
        public decimal distFromProfileAddrKms { get; set; }
        public int stationCode { get; set; }
        //End of Rev 2.0 Row:821
        //Rev 3.0 Row: 850
        public bool Is_Newshopadd { get; set; }
        //End of Rev 3.0 Row: 850
    }


    #region User Home Address

    public class UserHomeLocation
    {

        public string user_id { get; set; }
        public string session_token { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string pincode { get; set; }
    }

    #endregion

    //Rev Debashis Row:749
    public class ITCShopdaywiseInput
    {
        public int? user_id { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }
        public int? date_span { get; set; }
    }

    public class ITCShopdaywiseOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public int toal_shopvisit_count { get; set; }
        public int avg_shopvisit_count { get; set; }
        public List<ITCShopdaywiseList> date_list { get; set; }
    }

    public class ITCShopdaywiseList
    {
        public string date { get; set; }
        public List<ITCShopList> shop_list { get; set; }
    }

    public class ITCShopList
    {
        public string date { get; set; }
        public string shopid { get; set; }
        public string duration_spent { get; set; }
        public string shop_name { get; set; }
        public string shop_address { get; set; }
        public DateTime? visited_date { get; set; }
        public string device_model { get; set; }
        public string android_version { get; set; }
        public string battery { get; set; }
        public string net_status { get; set; }
        public string net_type { get; set; }
        public string in_time { get; set; }
        public string out_time { get; set; }
        public string start_timestamp { get; set; }
        public string in_location { get; set; }
        public string out_location { get; set; }
        public string Key { get; set; }
        public string shop_revisit_status { get; set; }
        public string shop_revisit_remarks { get; set; }
        public string pros_id { get; set; }
        public string updated_by { get; set; }
        public DateTime? updated_on { get; set; }
        public string agency_name { get; set; }
        public decimal approximate_1st_billing_value { get; set; }
    }
    //End of Rev Debashis Row:749
}
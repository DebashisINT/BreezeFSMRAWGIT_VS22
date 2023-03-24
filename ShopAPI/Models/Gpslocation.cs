#region======================================Revision History=========================================================
//1.0   V2.0.38     Debashis    20/02/2023      Some new classes have been added.Row: 813
#endregion===================================End of Revision History==================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class Gpslocation
    {

    }
    public class GpslocationInput
    {
        public string session_token { get; set; }
        public string gps_id { get; set; }
        public string user_id { get; set; }

        public string date { get; set; }
        public string gps_off_time { get; set; }
        public string gps_on_time { get; set; }
        public string duration { get; set; }
    }
    public class GpslocationOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }
    //Rev 1.0 Row: 813
    public class GPSLocationListsInput
    {
        public List<GPSLocationListsModel> gps_status_list { get; set; }
    }
    public class GPSLocationListsModel
    {
        public string session_token { get; set; }
        public string gps_id { get; set; }
        public string user_id { get; set; }
        public string date { get; set; }
        public string gps_off_time { get; set; }
        public string gps_on_time { get; set; }
        public string duration { get; set; }
    }
    public class GPSLocationListsOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }
    //End of Rev 1.0 Row: 813
}
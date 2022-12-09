using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class DeviceInformationModel
    {
        [Required]
        public string session_token { get; set; }
        [Required]
        public string user_id { get; set; }
        //Rev Debashis Row: 739
        public Int32 total_visit_revisit_count { get; set; }
        public Int32 total_visit_revisit_count_synced { get; set; }
        public Int32 total_visit_revisit_count_unsynced { get; set; }
        //End of Rev Debashis Row: 739
        public List<DeviceInformationDetails> app_info_list { get; set; }
    }

    public class DeviceInformationDetails
    {
        [Required]
        public string id { get; set; }
        [Required]
        public string date_time { get; set; }
        [Required]
        public string battery_status { get; set; }
        [Required]
        public string battery_percentage { get; set; }
        public string network_type { get; set; }
        public string mobile_network_type { get; set; }
        [Required]
        public string device_model { get; set; }
        [Required]
        public string android_version { get; set; }
        //Rev Debashis
        public string Available_Storage { get; set; }
        public string Total_Storage { get; set; }
        //End of Rev Debashis
        //Rev Debashis Row: 742
        public string power_saver_status { get; set; }
        //End of Rev Debashis Row: 742
    }

    public class DeviceInfoInput
    {
        [Required]
        public string session_token { get; set; }
        [Required]
        public string user_id { get; set; }
    }

    public class DeviceInfoOutPut
    {
        [Required]
        public string status { get; set; }
        [Required]
        public string message { get; set; }

        public List<DeviceInformation> app_info_list { get; set; }
    }


    public class DeviceInformation
    {
        [Required]
        public string id { get; set; }
        [Required]
        public DateTime date_time { get; set; }
        [Required]
        public string battery_status { get; set; }
        [Required]
        public string battery_percentage { get; set; }
        public string network_type { get; set; }
        public string mobile_network_type { get; set; }
        [Required]
        public string device_model { get; set; }
        [Required]
        public string android_version { get; set; }
        //Rev Debashis
        public string Available_Storage { get; set; }
        public string Total_Storage { get; set; }
        //End of Rev Debashis
    }

    //Rev Debashis Row: 743
    public class UserWiseGPSNetStatusInput
    {
        [Required]
        public string session_token { get; set; }
        [Required]
        public string user_id { get; set; }
        public List<UserWiseGPSNetStatusDetails> gps_net_status_list { get; set; }
    }

    public class UserWiseGPSNetStatusDetails
    {
        [Required]
        public string date_time { get; set; }
        [Required]
        public string gps_service_status { get; set; }
        [Required]
        public string network_status { get; set; }
    }

    public class UserWiseGPSNetStatusOutput
    {
        public string status { get; set; }
        public string message { get; set; }

    }
    //End of Rev Debashis Row: 743
}
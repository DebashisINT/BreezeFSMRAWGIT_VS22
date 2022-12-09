using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class Modeldevicetoken
    {
        public string user_id { get; set; }
        public string session_token { get; set; }
        public string device_token { get; set; }
        public string device_type { get; set; }
    }

    //Rev Debashis
    public class DevicetokenInfoInput
    {
        public string user_id { get; set; }
        public string session_token { get; set; }
    }

    public class DevicetokenInfoOutput
    {
        public String status { get; set; }
        public String message { get; set; }
        public string user_id_for_token { get; set; }
        public string user_name_for_token { get; set; }
        public string device_token { get; set; }
        public string device_type { get; set; }
    }
    //End of Rev Debashis
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class FaceImageDetectionInput
    {
        
        public string data { get; set; }
        public HttpPostedFileBase attachments { get; set; }
    }

    public class FaceImageDetectionInputDetails
    {
        public string user_id { get; set; }
        public string session_token { get; set; }
        //Rev Debashis
        public DateTime registration_date_time { get; set; }
        //End of Rev Debashis
    }

    public class FaceImageDetectionOutput
    {
        public string status{ get; set; }
        public string message{ get; set; }
        public string user_id{ get; set; }
        public string face_image_link { get; set; }
        //Rev Debashis
        //public string registration_date_time { get; set; }
        //End of Rev Debashis
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{


    public class LocationInput
    {
        public string selectedusrid { get; set; }
        public List<GetUsersLocation> userlsit { get; set; }
        public string Fromdate { get; set; }
        public string Todate { get; set; }
        public int datespan { get; set; }

        public string KeyId { get; set; }
    }



    public class GetUsersLocation
    {
        public string UserID { get; set; }
        public string username { get; set; }

    }



    public class Locationlists
    {
        public string location_name { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string distance_covered { get; set; }
        public string last_update_time { get; set; }
        public DateTime?  date { get; set; }
        public string shops_covered { get; set; }
        public string onlytime { get; set; }
        public string UserName { get; set; }
        public long VisitId { get; set; }

       public string EMPCODE { get; set; }

    }


}
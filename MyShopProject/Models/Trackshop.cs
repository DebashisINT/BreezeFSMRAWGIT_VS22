using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class Trackshop
    {
        public string Lat_visit { get; set; }
        public string Long_visit { get; set; }

        public List<TracksalesmanAreaTrack> salesmanarea { get; set; }
        public string location_name { get; set; }

        public string SDate { get; set; }

        public string loginstatus { get; set; }
        public string latlanLogin { get; set; }

        public string latlanLogout { get; set; }

        public string Fullresponse { get; set; }

        public string IsShowDistance { get; set; }

        public string distance { get; set; }
        
    }
    public class UserListTrackModel
    {
        public string selectedusrid { get; set; }

        public string Date { get; set; }
        public string IsGmap { get; set; }
        public List<GetUsers> userlsit { get; set; }
    }
    public class UserListTrackModelOutlet  
    {
        public string selectedusrid { get; set; }

        public string Date { get; set; }
        public string IsGmap { get; set; }
        public List<GetUsers> userlsit { get; set; }
    }
    public class TracksalesmanArea
    {

        public string title { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public string description { get; set; }
        public string SDate { get; set; }
        public string loginstatus { get; set; }
        public string distance { get; set; }

    }


    public class TracksalesmanAreaTrack
    {

        public string title { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public string description { get; set; }
        public string SDate { get; set; }
        public string loginstatus { get; set; }

        public string location { get; set; }
        public string distance_covered { get; set; }
        public string home_distance { get; set; }
        public string network_status { get; set; }
        public string battery_percentage { get; set; }
        public string COLORS { get; set; }
        public string Shop_Name { get; set; }
        public string Shop_Owner { get; set; }
        public string Shop_Owner_Contact { get; set; }
        
    }

    
}
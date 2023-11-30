using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class MapDashboard
    {
        public string description { get; set; }
        public string lng { get; set; }
        public string lat { get; set; }

        public string User_Id { get; set; }
        public string Loaction { get; set; }
        public string SalesMan { get; set; }
        public string Mobile { get; set; }
        public string Total_Visit { get; set; }

    }
    //Rev Work start 15.06.2022 0024954: Need to change View Route of FSM Dashboard
    public class LocationDashboard
    {
        public string location_name { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }

        public decimal distance_covered { get; set; }
        public string last_update_time { get; set; }
        public DateTime? date { get; set; }
        public int shops_covered { get; set; }
        public string frmdt { get; set; }

        public string todt { get; set; }
        public string onlytime { get; set; }
        public int meeting_attended { get; set; }
        public string visit_distance { get; set; }
        public string network_status { get; set; }
        public string battery_percentage { get; set; }
    }
    //Rev Work close 15.06.2022 0024954: Need to change View Route of FSM Dashboard
    public class PartyDashboard
    {
        public string shop_code{ get; set; }	
        public string Shop_Name	{ get; set; }
        public string Address	{ get; set; }
        public string Shop_Owner	{ get; set; }
        public string Shop_Lat	{ get; set; }
        public string Shop_Long	{ get; set; }
        public string Shop_Owner_Contact	{ get; set; }
        public string PARTYSTATUS	{ get; set; }
        public string MAP_COLOR	{ get; set; }
        public string Shop_CreateUser	{ get; set; }
        public string state { get; set; }
        public string PARENT_COLORCODE { get; set; }
    }
}
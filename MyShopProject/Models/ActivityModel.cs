using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{


    public class ActivityInput
    {
        public string selectedusrid { get; set; }
        public List<GetUsersActivity> userlsit { get; set; }
        public string Fromdate { get; set; }
        public string Todate { get; set; }
        public int datespan { get; set; }


    }



    public class GetUsersActivity
    {
        public string UserID { get; set; }
        public string username { get; set; }

    }



    public class Activitylists
    {
        public string date { get; set; }
        public string shopid { get; set; }
        public string duration_spent { get; set; }
        public string shop_name { get; set; }

        public string Owner_name { get; set; }
        public string Owner_contact { get; set; }
        public string shop_address { get; set; }
        public DateTime? visited_date { get; set; }
        public DateTime? visited_time { get; set; }
        public string time_visit { get; set; }


    }

    public class Activitylistscount
    {

        public int total_time_spent_at_shop { get; set; }
        public int total_shop_visited { get; set; }

        public int total_attendance { get; set; }

        public int avg_shop { get; set; }

        public int avgtime { get; set; }

        public int newshopIcount { get; set; }
    }

    public class ActivitylistsOutput
    {
        public Activitylistscount countget { get; set; }
        public List<Activitylists> lstactivs { get; set; }
    }


}
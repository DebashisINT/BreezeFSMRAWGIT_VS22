using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{

    public class UserwiseattendanceInput
    {
        public string selectedusrid { get; set; }

        public string DesgId { get; set; }

        public List<GetUsersattendance> userlsit { get; set; }
        public List<GetUserDesignation> designation { get; set; }
        public string Fromdate { get; set; }

        public string Todate { get; set; }

        public string type { get; set; }
 public string usertype { get; set; }

    }

    public class GetUsersattendance
    {
        public string UserID { get; set; }
        public string username { get; set; }

    }

    public class GetUserDesignation
    {
        public string ID { get; set; }
        public string DesgName { get; set; }

    }


    public class AttendancerecorduserwiseModel
    {
        public DateTime? login_date { get; set; }
        public DateTime? login_time { get; set; }

        public DateTime? logout_time { get; set; }

        public string duration { get; set; }
        public string Mintime { get; set; }
        public string Maxtime { get; set; }
        public int countlogin { get; set; }

        public int countlogout { get; set; }
        public string Attendstatus { get; set; }
        public string userName { get; set; }
        public string designation { get; set; }


    }

}
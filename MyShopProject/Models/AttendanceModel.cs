using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class AttendanceModelInput
    {
        public string selectedusrid { get; set; }


        public List<GetUsersshopsforattendance> userlsit { get; set; }
        public string Fromdate { get; set; }

        public string Todate { get; set; }

    }

    public class GetUsersshopsforattendance
    {
        public string UserID { get; set; }
        public string username { get; set; }

    }


    public class AttendancerecordModel
    {
        public DateTime? login_date { get; set; }
        public DateTime? login_time { get; set; }

        public DateTime? logout_time { get; set; }

        public string duration { get; set; }
        public string Mintime { get; set; }
        public string Maxtime { get; set; }

        public string Minvisittime { get; set; }
        public string Maxvisittime { get; set; }
        public string Employeename { get; set; }
        public string state { get; set; }
        public string UserLogin { get; set; }
        public string Designation { get; set; }

        public string REPORTTO { get; set; }

        //Rev Tanmoy Add new column  21-08-2019
        public string ATTEN_STATUS { get; set; }
        public string WORK_LEAVE_TYPE { get; set; }
        public string UNDERTIME { get; set; }
        public String GPS_INACTIVE_DURATION { get; set; }
        public string IDEAL_TIME { get; set; }
        public string IDEALTIME_CNT { get; set; }
        public string LATE_CNT { get; set; }
        //END Rev
    }



    public class AttendanceListModel
    {
        public DateTime? login_date { get; set; }
        public DateTime? login_time { get; set; }

        public DateTime? logout_time { get; set; }

        public string duration { get; set; }
        public string Mintime { get; set; }
        public string Maxtime { get; set; }

        public string Minvisittime { get; set; }
        public string Maxvisittime { get; set; }
        public string Isonleave { get; set; }
        public string lateday { get; set; }
        public string user_name { get; set; }
        public string Designation { get; set; }

    }

}
#region======================================Revision History=========================================================
//1.0   V2.0.40     Debashis    30/06/2023      A new parameter has been added.Row: 853
//2.0   V2.0.40     Debashis    30/06/2023      Some new parameters have been added.Row: 854
//3.0   V2.0.47     Debashis    06/06/2024      Some new parameters have been added.Row: 941
#endregion===================================End of Revision History==================================================
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class UserWiseDayStartEndInput
    {
        [Required]
        public string user_id { get; set; }
        public string session_token { get; set; }
        public string date { get; set; }
        public string location_name { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string shop_type { get; set; }
        public string shop_id { get; set; }
        public int isStart { get; set; }
        public int isEnd { get; set; }
        public decimal sale_Value { get; set; }
        public string remarks { get; set; }
        public string visit_distributor_id { get; set; }
        public string visit_distributor_name { get; set; }
        public string visit_distributor_date_time { get; set; }
        public int IsDDvistedOnceByDay { get; set; }
        //Rev 3.0 Row: 941
        public int attendance_worktype_id { get; set; }
        public string attendance_worktype_name { get; set; }
        //End of Rev 3.0 Row: 941
    }

    public class UserWiseDayStartEndOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public int isStart { get; set; }
    }

    public class UserWiseStatusDayStartEndInput
    {
        [Required]
        public string user_id { get; set; }
        public string session_token { get; set; }
        public string date { get; set; }
    }

    public class UserWiseStatusDayStartEndOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public bool DayStartMarked { get; set; }
        public bool DayEndMarked { get; set; }
        public string day_start_shop_type { get; set; }
        public string day_start_shop_id { get; set; }
        public bool IsDDvistedOnceByDay { get; set; }
    }

    public class UserDayStartEndListInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
    }

    public class UserDayStartEndListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string user_id { get; set; }
        public string user_name { get; set; }
        public List<DayStEndRecords> day_start_end_list { get; set; }
    }

    public class DayStEndRecords
    {
        public DateTime? dayStart_date_time { get; set; }
        public DateTime? dayEnd_date_time { get; set; }
        public string location_name { get; set; }
        //Rev 1.0 Row: 853
        public int isQualifiedAttendance { get; set; }
        //End of Rev 1.0 Row: 853
    }

    //Rev Debashis Row: 736
    public class UserDayStartEndDeleteInput
    {
        public string user_id { get; set; }
        public string session_token { get; set; }
        public string date { get; set; }
        public string isdaystartdel { get; set; }
        public string isdayenddel { get; set; }
    }

    public class UserDayStartEndDeleteOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }
    //End of Rev Debashis Row: 736
    //Rev 2.0 Row: 854
    public class UserAttendanceSummaryInput
    {
        [Required]
        public string user_id { get; set; }
    }
    public class UserAttendanceSummaryOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public int total_work_day { get; set; }
        public int total_present_day { get; set; }
        public int total_absent_day { get; set; }
        public int total_qualified_day { get; set; }
    }
    //End of Rev 2.0 Row: 854
}
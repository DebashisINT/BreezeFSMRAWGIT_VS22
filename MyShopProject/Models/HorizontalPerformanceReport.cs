using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class HorizontalPerformanceReport
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Fromdate { get; set; }



        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Todate { get; set; }


        public List<string> StateId { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public List<string> desgid { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public List<string> empcode { get; set; }

        public List<GetUsersStates> states { get; set; }

        public List<GetDesignation> designation { get; set; }

        public List<GetAllEmployee> employee { get; set; }

        public string is_pageload { get; set; }
        //public List<SelectListItem> PopulateLeaveDropdown(string RID)
        //{
        //    List<SelectListItem> items = new List<SelectListItem>();
        //    BusinessLogicLayer.DBEngine objEngine = new BusinessLogicLayer.DBEngine();
        //    DataTable DT = objEngine.GetDataTable(" proll_Main_Master ", " CODE,[DESC] ", " RID='" + RID + "' ");
        //    if (DT != null && DT.Rows.Count > 0)
        //    {
        //        foreach (DataRow row in DT.Rows)
        //        {
        //            items.Add(new SelectListItem
        //            {
        //                Text = row["DESC"].ToString(),
        //                Value = row["CODE"].ToString()
        //            });
        //        }
        //    }

        //    return items;
        //}
    }
    
}
//public class GetDesignation
//{
//    public string desgid { get; set; }

//    public string designame { get; set; }
//}

//public class GetAllEmployee
//{

//    public string empcode { get; set; }

//    public string empname { get; set; }
//}
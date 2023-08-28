#region======================================Revision History=========================================================================

//1.0   V2.0.38     Debashis    20/01/2023      Revisit Contact information is required in the Performance Summary report.Refer: 0025586                                              
//2.0   V2.0.42     Priti       19/07/2023      Branch Parameter is required for various FSM reports.Refer:0026135

#endregion===================================End of Revision History==================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Models;
using System.Web.Mvc;

namespace Models
{
    public class SalesSummaryReport
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

        public int IsShowRate { get; set; }
        public string is_pageload { get; set; }
        //Rev 1.0 Mantis:0025586
        public int IsRevisitContactDetails { get; set; }
        //End of Rev 1.0 Mantis:0025586

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
        //Rev 2.0
        public List<string> BranchId { get; set; }
        public List<GetBranch> modelbranch = new List<GetBranch>();
        //Rev 2.0 End
    }


}
public class GetDesignation
{
    public string desgid { get; set; }

    public string designame { get; set; }
}

public class GetAllEmployee
{
    
    public string empcode { get; set; }

    public string empname { get; set; }
}
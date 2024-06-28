/*****************************************************************************************
 * 1.0    05-06-2024    2.0.47    Sanchita     A new check box shall be implemented in Qualified Attendance 
                                               report named as "Consider Day End". Mantis: 27498 *@
 * ***********************************************************************************************************/
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class TeamVisitModel
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Fromdate { get; set; }



        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Todate { get; set; }


        public List<string> BranchId { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public List<string> desgid { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public List<string> empcode { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public List<string> ChannelId { get; set; }

        public List<GetDesignation> designation { get; set; }

        public List<GetAllEmployee> employee { get; set; }

        public List<GetBranch> modelbranch = new List<GetBranch>();

        public string is_pageload { get; set; }

        // Rev 1.0
        public string IsConsiderDayEnd { get; set; }
        // End of Rev 1.0
    }
}
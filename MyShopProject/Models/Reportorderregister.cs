//====================================================== Revision History ==========================================================
//1.0  04-04-2024   V2 .0.46   Sanchita  0027345: Two checkbox required in parameter for Order register report.
//====================================================== Revision History ==========================================================
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class Reportorderregister
    {
    }

    public class Reportorderregisterinput
    {
        public string selectedusrid { get; set; }
        public List<string> StateId { get; set; }
        public List<GetUserName> userlsit { get; set; }
        public List<GetUsersStates> states { get; set; }
        public List<string> empcode { get; set; }
        public string Is_PageLoad { get; set; }
        public List<string> shopId { get; set; }
        public List<Getmasterstock> shoplist { get; set; }
        public string Fromdate { get; set; }
        public string Todate { get; set; }
        public int Uniquecont { get; set; }
        //rev Pratik
        public int IsSchemeDetails { get; set; }
        //End of rev Pratik
        //Rev Debashis
        public int IsPaitentDetails { get; set; }
        //End of Rev Debashis
        //Rev Debashis 0025198
        public List<string> BranchId { get; set; }
        public List<GetBranch> modelbranch = new List<GetBranch>();
        //End of Rev Debashis 0025198
        // Rev 1.0
        public int IsShowMRP { get; set; }
        public int IsShowDiscount { get; set; }
        // End of Rev 1.0
    }

    public class Reportorderregisteroutput
    {

        public string Fromdate { get; set; }
        public string Todate { get; set; }
        public int Uniquecont { get; set; }
    }


}
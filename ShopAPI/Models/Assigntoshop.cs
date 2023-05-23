#region======================================Revision History=========================================================
//1.0   V2.0.39     Debashis    06/04/2023      A new output has been added.Row: 818
#endregion===================================End of Revision History==================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class AssigntoshopPPInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string state_id { get; set; }
    }

    public class AssigntoshopPP
    {
        public string assigned_to_pp_id { get; set; }
        public string assigned_to_pp_authorizer_name { get; set; }
        public string phn_no { get; set; }

    }

    public class AssigntoshopDDInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string state_id { get; set; }
    }

    public class AssigntoshopInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string state_id { get; set; }
    }

    public class AssigntoshopOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<Assigntoshop> shop_list { get; set; }

    }

    public class Assigntoshop
    {
        public string assigned_to_shop_id { get; set; }
        public string name { get; set; }
        public string phn_no { get; set; }
        public string type_id { get; set; }

    }

     public class AssigntoshopPPOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<AssigntoshopPP> assigned_to_pp_list { get; set; }

    }

    public class AssigntoshopDD
    {
        public string assigned_to_dd_id { get; set; }
        public string assigned_to_dd_authorizer_name { get; set; }
        public string phn_no { get; set; }
        public string assigned_to_pp_id { get; set; }
        public string type_id { get; set; }
        //Rev Debashis
        public string dd_latitude { get; set; }
        public string dd_longitude { get; set; }
        //End of Rev Debashis
        //Rev 1.0 Row:818
        public string address { get; set; }
        //End of Rev 1.0 Row:818
    }

    public class AssigntoshopDDOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<AssigntoshopDD> assigned_to_dd_list { get; set; }

    }


}
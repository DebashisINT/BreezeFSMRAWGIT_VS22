#region======================================Revision History=========================================================================
//1.0   V2.0.38     Debashis    23/01/2023      Multiple contact information to be displayed in the Shops report.
//                                              Refer: 0025585
//2.0   V2.0.41     Sanchita    19/07/2023      Add Branch parameter in Listing of Master -> Shops report. Mantis : 26135
#endregion===================================End of Revision History==================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class CounterClass
    {
        public List<string> StateId { get; set; }

        public List<CounterStates> states { get; set; }

        public string TypeID { get; set; }
        public List<shopCounterTypes> Shoptypes { get; set; }

        public string Ispageload { get; set; }
        //Rev 1.0 Mantis:0025585
        public int IsRevisitContactDetails { get; set; }
        //End of Rev 1.0 Mantis:0025585
        // Rev 2.0
        public List<string> BranchId { get; set; }
        public List<GetBranch> modelbranch = new List<GetBranch>();
        // End of Rev 2.0
    }

    public class CounterStates
    {
        public string ID { get; set; }
        public string StateName { get; set; }
    }


    public class CounterShopList
    {
        public string shop_Auto { get; set; }
        public string UserName { get; set; }
        public string shop_id { get; set; }
        [Required]
        public string shop_name { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        public string pin_code { get; set; }

        public string shop_lat { get; set; }

        public string shop_long { get; set; }
        [Required]
        public string owner_name { get; set; }
        [Required]
        public string owner_contact_no { get; set; }

        [Required]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please Enter your valid email")]
        public string owner_email { get; set; }

        public string Shop_Image { get; set; }

        public string PP { get; set; }

        public string DD { get; set; }

        public DateTime? Shop_CreateTime { get; set; }

        public DateTime? dob { get; set; }

        public DateTime? date_aniversary { get; set; }
        public string Shoptype { get; set; }

        public List<shopTypes> shptypes { get; set; }

        public string dobstr { get; set; }

        public string date_aniversarystr { get; set; }
        public string time_shop { get; set; }

        public int countactivity { get; set; }

        public string Lastactivitydate { get; set; }
    }

    public class shopCounterTypes
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public bool IsChecked { get; set; }
    }


}
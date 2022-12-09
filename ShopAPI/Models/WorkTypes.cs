using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class WorktypesInput
    {
        public string user_id { get; set; }
    }
 
   
    public class Worktypesoutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<worktypes> worktype_list { get; set; }
    }
    public class worktypes
    {
        public int ID { get; set; }
        public string Descrpton { get; set; }

    }

    public class UpdateworktypesInput
    {
        [Required]
        public string session_token { get; set; }
        [Required]
        public string user_id { get; set; }
        [Required]
        public string work_type { get; set; }
        public string work_desc { get; set; }

        //Etra value update 31-12-2020 Tanmoy
        public string distributor_name { get; set; }
        public string market_worked { get; set; }
        //Etra value update 31-12-2020 Tanmoy
    }

    public class Updateworktypesoutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }
}
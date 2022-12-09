using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class BSList
    {
        public String id { get; set; }
        public String name { get; set; }
    }

    public class BSListOutput
    {
        public String status { get; set; }
        public String message { get; set; }
        public List<BSList> bs_list { get; set; }
    }

    public class BSListInput
    {
        [Required]
        public String session_token { get; set; }
        [Required]
        public String user_id { get; set; }
    }
}
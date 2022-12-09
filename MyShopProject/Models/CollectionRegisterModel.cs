using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class CollectionRegisterModel
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Fromdate { get; set; }



        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Todate { get; set; }


        public List<string> StateId { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]

        public List<string> empcode { get; set; }

        public List<GetUsersStates> states { get; set; }

        public List<string> shopId { get; set; }
        public List<Getmasterstock> shoplist { get; set; }

        public List<GetAllEmployees> employee { get; set; }

        public string is_pageload { get; set; }
        public int IsPaitentDetails { get; set; }
    }    
}
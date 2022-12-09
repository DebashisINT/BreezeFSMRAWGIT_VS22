using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class LeadRegisterModel
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

        public List<GetDesignations> designation { get; set; }

        public List<GetAllEmployees> employee { get; set; }

        public string is_pageload { get; set; }
    }
}

public class GetDesignations
{
    public string desgid { get; set; }

    public string designame { get; set; }
}

public class GetAllEmployees
{

    public string empcode { get; set; }

    public string empname { get; set; }
}
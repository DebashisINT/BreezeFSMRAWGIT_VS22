using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class ViewGeotrackModel
    {
        public string StateId { get; set; }
        public List<GetUsersState> modelstates { get; set; }


        public List<string> BranchId { get; set; }
        public List<GetUsersBranch> modelbranch { get; set; }


        public string is_pageload { get; set; }
    }

    public class UserRuoteList
    {
        public string SalesMan { get; set; }
        //public string Shop_Name { get; set; }
        //public string Address { get; set; }
        //public string Shop_Owner { get; set; }
        public string Visit_Lat { get; set; }
        public string Visit_Long { get; set; }
        public string User_Id { get; set; }
        public decimal Distance_Covered { get; set; }

        //public string PARTYSTATUS { get; set; }
        //public string MAP_COLOR { get; set; }
        //public string Shop_CreateUser { get; set; }
        //public string state { get; set; }
        //public string PARENT_COLORCODE { get; set; }
    }
}
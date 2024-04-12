using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class ViewGeographyModel
    {
        public string StateId { get; set; }
        public List<GetUsersState> modelstates { get; set; }


        public List<string> BranchId { get; set; }
        public List<GetUsersBranch> modelbranch { get; set; }


        public List<string> shoptypeId { get; set; }
        public List<GetPartyType> modelpartytypes { get; set; }

        public string is_pageload { get; set; }
    }

    public class GetUsersState
    {
        public string ID { get; set; }
        public string STATENAME { get; set; }
    }

    public class GetUsersBranch
    {
        public Int64 BRANCH_ID { get; set; }
        public string CODE { get; set; }
    }

    public class GetPartyType
    {
        public Int64 SHOP_TYPEID { get; set; }
        public string NAME { get; set; }

    }

    public class OutletList
    {
        public string Shop_Name { get; set; }
        public string Address { get; set; }
        public string Shop_Lat { get; set; }
        public string Shop_Long { get; set; }
        public Int32 Shop_PartyId { get; set; }
        public string Color_Code { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class UserListModel
    {
        public string selectedusrid { get; set; }


        public List<GetUsers> userlsit { get; set; }



       
    }


    public class GetUsers
    {
        public string UserID { get; set; }
        public string username { get; set; }
      

    }

    public class UserGroupList
    {
        public int grp_id { get; set; }
        public string grp_name { get; set; }


    }

 
}
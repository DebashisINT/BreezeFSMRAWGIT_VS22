using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class EmpListModel
    {
        public List<string> BranchId { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public List<string> empcode { get; set; }

        public List<GetBranch> modelbranch = new List<GetBranch>();

       public List<GetAllEmployee> employee { get; set; }

        public string is_pageload { get; set; }
    }
}


public class SelectAllEmployee
{

    public string empcode { get; set; }

    public string empname { get; set; }
}
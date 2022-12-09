using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class EmployeeListModel
    {
        public List<string> StateId { get; set; }
        public List<string> desgid { get; set; }
        public List<string> shopId { get; set; }

        public List<string> CityId { get; set; }

        public List<string> AreaId { get; set; }

        public List<string> CountryId { get; set; }

        public List<string> DeptId { get; set; }

        public string userId { get; set; }
    }

}
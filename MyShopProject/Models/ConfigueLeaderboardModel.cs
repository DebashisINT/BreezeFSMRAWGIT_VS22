using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class ConfigueLeaderboardModel
    {
        public string pointID { get; set; }
        public string POINT_SECTION { get; set; }
        public Decimal POINT_VALUE { get; set; }
        public bool IS_ACTIVE { get; set; }
        public List<pointsection> section { get; set; }
        public List<pointdata> points { get; set; }
        
        public decimal PointAmount { get; set; }
        public bool IsActive { get; set; }
        public int PointSectionId { get; set; }
    }
    public class pointsection
    {
        public string ID { get; set; }
        public string POINT_SECTION { get; set; }
    }

    public class pointdata
    {
        public string pointID { get; set; }
        public string POINT_SECTION { get; set; }
        public Decimal POINT_VALUE { get; set; }
        public string IS_ACTIVE { get; set; }
        public bool IsActive { get; set; }
    }

}
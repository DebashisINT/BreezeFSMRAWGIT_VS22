using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace MyShop.Models
{
   public class BeatModel
    {
        // Area
        public Int32 Area { get; set; }
        public List<string> AreaIds { get; set; }
        public string AreaId { get; set; }
        public List<AreaList> AreaList { get; set; }
        //

        // Route
        public Int32 Route { get; set; }
        public List<string> RouteIds { get; set; }
        public string RouteId { get; set; }
        public List<RouteList> RouteList { get; set; }
        //
    }

    public class AreaList
    {
        public Int64 AreaId { get; set; }
        public string AreaName { get; set; }
    }

    public class RouteList
    {
        public Int64 RouteId { get; set; }
        public string RouteName { get; set; }
    }
}

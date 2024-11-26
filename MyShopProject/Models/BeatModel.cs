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

    public class BeatImportLogModel
    {
        public string AreaCode { get; set; }
        public string AreaName { get; set; }
        public string RouteCode { get; set; }
        public string RouteName { get; set; }
        public string BeatCode { get; set; }
        public string BeatName { get; set; }
        public string UserName { get; set; }
        public string UserID { get; set; }
        public string OutletEntityID { get; set; }
        public string OutletName { get; set;}
        public string ImportStatus { get; set;}
        public string ImportMsg { get; set;}
        public string ImportDate { get; set;}
        public string CreateUser { get; set;}

    }
}

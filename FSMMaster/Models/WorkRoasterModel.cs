using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;

namespace FSMMaster.Models
{
    public class WorkRoasterListModel
    {
        public string RoasterName {get; set;}
        public List<RoasterList> RoasterList { get; set; }
        public string response_code { get; set;}
        public string response_msg { get; set;}
        public string Is_PageLoad { get; set; }

    }

    public class WorkRoasterDet
    {
        public string SEQ { get; set; }
        public string ROSTERID { get; set; }
        public string ROSTENAME { get; set; }
        public string CREATEDATE { get; set; }
        public string CREATEUSER { get; set; }
        public string MODIFIEDDATE { get; set; }
        public string MODIFIEDUSER { get; set; }



    }

    public class RoasterList
    {
        public string RoasterName { get; set; }
        public string Day { get; set; }
        public string BeginTime { get; set; }
        public string EndTime { get; set; }
        public string Grace { get; set; }

    }

    public class RosterModuleMapList
    {
        public string ModuleName { get; set; }
        public string ModuleId { get; set; }
        public bool IsChecked { get; set; }
        public string status { get; set; }
    }

}
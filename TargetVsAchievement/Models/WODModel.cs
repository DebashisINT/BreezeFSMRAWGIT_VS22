using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace TargetVsAchievement.Models
{
    public class WODModel
    {
        public string TARGETLEVELID { get; set; }
        public string TARGETLEVEL { get; set; }
        public string INTERNALID { get; set; }
        public Int64 TARGET_ID { get; set; }
        public String TargetType { get; set; }
        public String TargetNo { get; set; }
        public DateTime TargetDate { get; set; }

        public String WODCount { get; set; }
        public DateTime STARTEDATE { get; set; }
        public DateTime ENDDATE { get; set; }

        // SELECT TARGET TYPE DROPDOWN //
        public List<LevelList> LevelList { get; set; }

        // SELECT TARGET TYPE DROPDOWN //

        public DataSet TargetEntryInsertUpdate(String action, DateTime? TargetDate, Int64 TARGET_ID, String TargetType, String TargetNo,
            DataTable dtTarget, Int64 userid = 0
            )
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("PRC_WODTARGETASSIGN");

            proc.AddVarcharPara("@ACTION", 150, action);
            proc.AddVarcharPara("@TargetType", 100, TargetType);
            proc.AddDateTimePara("@TargetDate", Convert.ToDateTime(TargetDate));
            proc.AddBigIntegerPara("@TARGET_ID", TARGET_ID);
            proc.AddVarcharPara("@TargetNo", 100, TargetNo);
            proc.AddBigIntegerPara("@USER_ID", userid);


            if (action == "INSERTWODTARGET" || action == "UPDATEWODTARGET")
            {
                proc.AddPara("@FSM_UDT_WODTARGETASSIGN", dtTarget);
            }
            ds = proc.GetDataSet();
            return ds;
        }

        public DataTable GETTARGETASSIGNDETAILSBYID(String Action, Int64 DetailsID)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_WODTARGETASSIGN");
            proc.AddVarcharPara("@ACTION", 100, Action);
            proc.AddBigIntegerPara("@TARGET_ID", DetailsID);
            ds = proc.GetTable();
            return ds;
        }
    }


    public class WODTARGETGRIDLIST
    {
        //public String ActualSL { get; set; }
        public string SlNO { get; set; }
        public string TARGETDOCNUMBER { get; set; }
        public string TARGETLEVELID { get; set; }
        public string TARGETLEVEL { get; set; }
        public string INTERNALID { get; set; }
        public string TIMEFRAME { get; set; }
        public string STARTEDATE { get; set; }
        public string ENDDATE { get; set; }
        public string WODCOUNT { get; set; }
        //public string UpdateEdit { get; set; }

        public String Guids { get; set; }

    }

    public class UDTWODTARGET
    {
        public string SlNO { get; set; }
        //public Int64 TARGETDETAILS_ID { get; set; }
        public Int64 TARGETLEVELID { get; set; }
        public string TARGETLEVEL { get; set; }
        public string INTERNALID { get; set; }
        public string TIMEFRAME { get; set; }
        public DateTime STARTEDATE { get; set; }
        public DateTime ENDDATE { get; set; }
        public Int64 WODCOUNT { get; set; }
        //public Int64 UpdateEdit { get; set; }     

    }
}
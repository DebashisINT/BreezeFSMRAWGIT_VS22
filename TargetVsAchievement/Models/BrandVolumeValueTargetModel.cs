using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace TargetVsAchievement.Models
{
    public class BrandVolumeValueTargetModel
    {
        public Int64 TARGET_ID { get; set; }
        public String TargetType { get; set; }
        public String TargetNo { get; set; }
        public DateTime TargetDate { get; set; }

        public string TARGETLEVEL { get; set; }
        public DateTime STARTEDATE { get; set; }
        public DateTime ENDDATE { get; set; }
        public String ORDERAMOUNT { get; set; }
        public String ORDERQTY { get; set; }
        public string BRANDNAME { get; set; }

        public string BRANDID { get; set; }

        // SELECT TARGET TYPE DROPDOWN //
        public List<LevelList> LevelList { get; set; }

        // SELECT TARGET TYPE DROPDOWN //

        // public List<SalesTargetProduct> ListSalesTargetProduct { get; set; }
        public DataSet TargetEntryInsertUpdate(String action, DateTime? TargetDate, Int64 TARGET_ID, String TargetType, String TargetNo,
            DataTable dtTarget, Int64 userid = 0
            )
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("PRC_BRANDVOLUMEVALUETARGETASSIGN");

            proc.AddVarcharPara("@ACTION", 150, action);
            proc.AddVarcharPara("@TargetType", 100, TargetType);
            proc.AddDateTimePara("@TargetDate", Convert.ToDateTime(TargetDate));
            proc.AddBigIntegerPara("@TARGET_ID", TARGET_ID);
            proc.AddVarcharPara("@TargetNo", 100, TargetNo);
            proc.AddBigIntegerPara("@USER_ID", userid);


            if (action == "INSERTBRANDTARGET" || action == "UPDATEBRANDTARGET")
            {
                proc.AddPara("@FSM_UDT_BRANDTARGETASSIGN", dtTarget);
            }
            ds = proc.GetDataSet();
            return ds;
        }

        public DataTable GETTARGETASSIGNDETAILSBYID(String Action, Int64 DetailsID)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_BRANDVOLUMEVALUETARGETASSIGN");
            proc.AddVarcharPara("@ACTION", 100, Action);
            proc.AddBigIntegerPara("@TARGET_ID", DetailsID);
            ds = proc.GetTable();
            return ds;
        }
    }

    public class BRANDVOLUMEVALUETARGETGRIDLIST
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
        public string ORDERAMOUNT { get; set; }
        public string ORDERQTY { get; set; }
        public string BRANDNAME { get; set; }
        public string BRANDID { get; set; }
        public string Guids {  get; set; }
    }

    public class UDTBRANDVOLUMEVALUETARGET
    {
        public string SlNO { get; set; }
       // public Int64 TARGETDETAILS_ID { get; set; }
        public Int64 TARGETLEVELID { get; set; }
        public string TARGETLEVEL { get; set; }
        public string INTERNALID { get; set; }
        public string TIMEFRAME { get; set; }

        public DateTime STARTEDATE { get; set; }

        public DateTime ENDDATE { get; set; }

       
        public decimal ORDERAMOUNT { get; set; }      

        public decimal ORDERQTY { get; set; }

      //  public Int64 UpdateEdit { get; set; }

        public string BRANDNAME { get; set; }

        public Int64 BRANDID { get; set; }

      
    }

}
#region======================================Revision History=========================================================================
//1.0   V2.0.38     Debashis    23/01/2023      Multiple contact information to be displayed in the Shops report.
//                                              Refer: 0025585
//2.0   V2.0.41     Sanchita    19/07/2023      Add Branch parameter in Listing of Master -> Shops report. Mantis : 26135
//3.0   V2.0.47     Priti       19/07/2023      System is getting logged out while trying to export the Shops data into excel. Mantis: 0027324
//4.0   V2.0.47     Sanchita    22-05-2024      0027405: Colum Chooser Option needs to add for the following Modules.
#endregion===================================End of Revision History==================================================================
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesmanTrack
{
    public class Shop
    {
        //Rev Debashis && Added BranchId 0025198
        public DataTable GetShopList(string BranchId, string userid, string fromdate, string todate, string cont, string weburl, string stateId, int Create_UserId = 0)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("SP_API_Getshoplists_Report");
            proc.AddPara("@user_id", userid);
            proc.AddPara("@Uniquecont", cont);
            proc.AddPara("@FromDate", fromdate);
            proc.AddPara("@Todate", todate);
            proc.AddPara("@Weburl", weburl);
            proc.AddPara("@StateId", stateId);
            proc.AddPara("@Action", "ShopDetails");
            proc.AddPara("@Create_UserId", Create_UserId);
            proc.AddPara("@BRANCHID", BranchId);
            ds = proc.GetTable();
            return ds;
        }


        public DataSet GetShopListHierarchy(string userid, string fromdate, string todate, string weburl,string DesgId,string stateid,int pagenumber,int pagecount)
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("SP_API_Getshoplists_Report_Hiaerarchy");
            proc.AddPara("@user_id", userid);
            proc.AddPara("@FromDate", fromdate);
            proc.AddPara("@Todate", todate);
            proc.AddPara("@Weburl", weburl);
            proc.AddPara("@DesgId", DesgId);
            proc.AddPara("@StateId", stateid);
            proc.AddPara("@startindex", pagenumber);
            proc.AddPara("@Maxdata", pagecount);
            ds = proc.GetDataSet();
            return ds;
        }


        public DataTable GetTypesList()
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("SP_API_Getshoplists_Report");
            proc.AddPara("@Action", "Gettypes");
            ds = proc.GetTable();
            return ds;
        }

        public int ShopModify(string shopid,string address,string pincode,string shopname,string ownername,string ownercontact,string owneremail,string types,string dob,string doanniversary,string AssignTo=null)
        {
            int s = 0;
            ProcedureExecute proc = new ProcedureExecute("SP_API_Getshoplists_Report");
            proc.AddPara("@shopid", shopid);
            proc.AddPara("@address", address);
            proc.AddPara("@pincode", pincode);
            proc.AddPara("@shopname", shopname);
            proc.AddPara("@ownername", ownername);
            proc.AddPara("@ownercontact", ownercontact);
            proc.AddPara("@owneremail", owneremail);
            proc.AddPara("@dob", dob);
            proc.AddPara("@doanniversary", doanniversary);
            proc.AddPara("@Shoptype", types);
            proc.AddPara("@AssignTo", AssignTo);
            proc.AddPara("@Action", "Modify");

            s=proc.RunActionQuery();

            return s;
        }

        public int ShopDelete(string shopid)
        {
            int s = 0;
            ProcedureExecute proc = new ProcedureExecute("SP_API_Getshoplists_Report");
            proc.AddPara("@shopid", shopid);
            proc.AddPara("@Action", "Delete");
            s = proc.RunActionQuery();

            return s;
        }


        public DataTable ShopGetDetails(string shopid)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("SP_API_Getshoplists_Report");
            proc.AddPara("@shopid", shopid);
            proc.AddPara("@Action", "ShopDetailsById");
            ds = proc.GetTable();
            return ds;
        }


        //Rev 1.0 Mantis: 0025585
        //public DataTable GetShopListCounterwise(string shoptype, string weburl, string stateId, int Create_UserId = 0)
        // Rev 2.0 [ parameter string BranchId added ]
        public DataTable GetShopListCounterwise(string BranchId, string shoptype, string weburl, string stateId, int IsRevisitContactDetails, int Create_UserId = 0,string ISPAGELOAD="0")
        //End of Rev 1.0 Mantis: 0025585
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("SP_API_Getshoplists_Report");
            proc.AddPara("@Shoptype", shoptype);
            proc.AddPara("@Weburl", weburl);
            proc.AddPara("@StateId", stateId);
            proc.AddPara("@Action", "Counter");
            proc.AddPara("@Create_UserId", Create_UserId);
            //Rev 1.0 Mantis: 0025585
            proc.AddPara("@ISREVISITCONTACTDETAILS", IsRevisitContactDetails);
            //End of Rev 1.0 Mantis: 0025585
            // Rev 2.0
            proc.AddPara("@BRANCHID", BranchId);
            // End of Rev 2.0
            // Rev 3.0
            proc.AddPara("@ISPAGELOAD", ISPAGELOAD);
            // End of Rev 3.0

            ds = proc.GetTable();
            return ds;
        }

        public int ShopPPDDAdd(string userid,string shopid, string address, string pincode, string shopname, string ownername, string ownercontact, string owneremail, string types, string dob, string doanniversary, string AssignTo = null)
        {
            int s = 0;
            ProcedureExecute proc = new ProcedureExecute("Proc_FTS_PPDDInsert");
            proc.AddPara("@shopid", shopid);
            proc.AddPara("@address", address);
            proc.AddPara("@pincode", pincode);
            proc.AddPara("@shopname", shopname);
            proc.AddPara("@ownername", ownername);
            proc.AddPara("@ownercontact", ownercontact);
            proc.AddPara("@owneremail", owneremail);
            proc.AddPara("@dob", dob);
            proc.AddPara("@doanniversary", doanniversary);
            proc.AddPara("@Shoptype", types);
            proc.AddPara("@AssignTo", AssignTo);
            proc.AddPara("@Action", "AddPPDD");
            proc.AddPara("@user_id", userid);
            s = proc.RunActionQuery();

            return s;
        }


        public DataTable GetPPDDList()
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("SP_API_Getshoplists_Report");

            proc.AddPara("@Action", "PPDDLIST");
            ds = proc.GetTable();
            return ds;
        }

        // Rev 4.0
        public int InsertPageRetention(string Col, String USER_ID, String ReportName)
        {
            ProcedureExecute proc = new ProcedureExecute("PRC_FTS_PageRetention");
            proc.AddPara("@Col", Col);
            proc.AddPara("@ReportName", ReportName);
            proc.AddPara("@USER_ID", USER_ID);
            proc.AddPara("@ACTION", "INSERT");
            int i = proc.RunActionQuery();
            return i;
        }

        public DataTable GetPageRetention(String USER_ID, String ReportName)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTS_PageRetention");
            proc.AddPara("@ReportName", ReportName);
            proc.AddPara("@USER_ID", USER_ID);
            proc.AddPara("@ACTION", "DETAILS");
            dt = proc.GetTable();
            return dt;
        }
        // End of Rev 4.0

    }
}

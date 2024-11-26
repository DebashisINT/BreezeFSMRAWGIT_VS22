/*************************************************************************************************************
Rev 1.0     Sanchita   V2.0.28    27/01/2023      Bulk modification feature is required in Parties menu. Refer: 25609
Rev 2.0     Sanchita   V2.0.47    29/05/2024      0027405: Colum Chooser Option needs to add for the following Modules
*****************************************************************************************************************/
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace BusinessLogicLayer.SalesmanTrack
{
    public class AddPartyDetailsBL
    {
        public DataSet AddShopGetDetails()
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSInsertUpdateNewParty");
            proc.AddPara("@ACTION", "Details");
            ds = proc.GetDataSet();
            return ds;
        }
        public DataSet GetLoanDetails()
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSInsertUpdateNewParty");
            proc.AddPara("@ACTION", "LoanDetails");
            ds = proc.GetDataSet();
            return ds;
        }
        public DataTable GetReportPartyDetails(string fromdate, string todate, string userid, String stateID, String userlist, String IS_ReAssignedDate)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSPartiDetails_List");

            proc.AddPara("@FROMDATE", fromdate);
            proc.AddPara("@TODATE", todate);
            proc.AddPara("@STATEID", stateID);
            proc.AddPara("@USERID", userid);
            proc.AddPara("@EMPID", userlist);
            proc.AddPara("@IsReAssignedDate", IS_ReAssignedDate);
            ds = proc.GetTable();
            return ds;
        }

        public int ShopActiveInactive(String Shop_code)
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSInsertUpdateNewParty");
            proc.AddPara("@ACTION", "ShopInactive");
            proc.AddPara("@ShopCode", Shop_code);
            int i = proc.RunActionQuery();
            return i;
        }

        public int AddNewShop(String Shop_code, int State,int type,string AssignedTo,string Party_Name,string Party_Code,string Address,string Pin_Code,string owner_name,string dobstr,
            string date_aniversarystr,string owner_contact_no,string Alternate_Contact,string owner_email,string ShopStatus,int EntyType,string Owner_PAN,string Owner_Adhar,
            string Remarks, long NewUser, long OldUser, string shop_lat, string shop_long)
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSInsertUpdateNewParty");
            if (Shop_code==null || Shop_code == "")
            {
                proc.AddPara("@ACTION", "InsertShop"); 
            }
            else
            {
                proc.AddPara("@ACTION", "UpdateShop"); 
            }
            proc.AddPara("@ShopCode", Shop_code);
            proc.AddPara("@stateId", State);
            proc.AddPara("@ShopType", type);
            proc.AddPara("@assigned_to_pp_id", AssignedTo);
            proc.AddPara("@Shop_Name", Party_Name);
            proc.AddPara("@EntityCode", Party_Code);
            proc.AddPara("@Address", Address);
            proc.AddPara("@Pincode", Pin_Code);
            proc.AddPara("@Shop_Owner", owner_name);
            proc.AddPara("@dob", dobstr);
            proc.AddPara("@date_aniversary", date_aniversarystr);
            proc.AddPara("@Shop_Owner_Contact", owner_contact_no);
            proc.AddPara("@Alt_MobileNo", Alternate_Contact);
            proc.AddPara("@Shop_Owner_Email", owner_email);
            proc.AddPara("@Entity_Status", ShopStatus);
            proc.AddPara("@Entity_Type", EntyType);
            proc.AddPara("@ShopOwner_PAN", Owner_PAN);
            proc.AddPara("@ShopOwner_Aadhar", Owner_Adhar);
            proc.AddPara("@Remarks", Remarks);
            proc.AddPara("@user_id", NewUser);
            proc.AddPara("@OLD_CreateUser", OldUser);
            proc.AddPara("@Shop_Lat", shop_lat);
            proc.AddPara("@Shop_Long", shop_long);
            int i = proc.RunActionQuery();
            return i;
        }

        public DataTable ShopGetDetails(String ShopCode)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSInsertUpdateNewParty");
            proc.AddPara("@ACTION", "EditShop");
            proc.AddPara("@ShopCode", ShopCode);
            dt = proc.GetTable();
            return dt;
        }

        public DataTable LasteEntityCodeStateWise(String stateid, String ShopType)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSInsertUpdateNewParty");
            proc.AddPara("@ACTION", "LasteEntityCodeStateWise");
            proc.AddPara("@stateId", stateid);
            proc.AddPara("@ShopType", ShopType);
            dt = proc.GetTable();
            return dt;
        }

        public DataTable GetStateList(String Country)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSInsertUpdateNewParty");
            proc.AddPara("@ACTION", "GetStateListCountryWise");
            proc.AddPara("@CountryId", Country);
            ds = proc.GetTable();
            return ds;
        }

        public DataTable GetCityList(String state)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSInsertUpdateNewParty");
            proc.AddPara("@ACTION", "GetCityListStateWise");
            proc.AddPara("@stateId", state);
            ds = proc.GetTable();
            return ds;
        }

        public DataTable GetAreaList(String City)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSInsertUpdateNewParty");
            proc.AddPara("@ACTION", "GetAreaListCityWise");
            proc.AddPara("@Shop_City", City);
            ds = proc.GetTable();
            return ds;
        }

        public DataTable PartyDelate(String ShopCode)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSInsertUpdateNewParty");
            proc.AddPara("@ACTION", "DeleteParty");
            proc.AddPara("@ShopCode", ShopCode);
            dt = proc.GetTable();
            return dt;
        }

        public DataTable ShopReAssignUser(String USER_ID, String OLD_USER, String NEW_USER, String ShopCodes)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTS_ReAssignShopToUser");
            proc.AddPara("@ACTION", "ShopReAssignUser");
            proc.AddPara("@USER_ID", USER_ID);
            proc.AddPara("@OLD_USER", OLD_USER);
            proc.AddPara("@NEW_USER", NEW_USER);
            proc.AddPara("@ShopCodes", ShopCodes);
            dt = proc.GetTable();
            return dt;
        }
        //Mantis Issue 25133
        public DataTable ShopReAssignGroupBeat(String USER_ID, String OldGroupBeat, String NewGroupBeat, String ShopCodes)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTS_ReAssignShopToGroupBeat");
            proc.AddPara("@ACTION", "ShopReAssignUser");
            proc.AddPara("@USER_ID", USER_ID);
            proc.AddPara("@OLD_GroupBeat", OldGroupBeat);
            proc.AddPara("@NEW_GroupBeat", NewGroupBeat);
            proc.AddPara("@ShopCodes", ShopCodes);
            dt = proc.GetTable();
            return dt;
        }
        public DataTable ShopReAssignGroupBeatLog(String FromDate, String ToDate)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTS_ReAssignShopToGroupBeat");
            proc.AddPara("@ACTION", "ShopReAssignUserLog");
            proc.AddPara("@FromDate", FromDate);
            proc.AddPara("@ToDate", ToDate);
            dt = proc.GetTable();
            return dt;
        }
        //End of Mantis Issue 25133
        public DataTable ShopReAssignUserLog(String FromDate, String ToDate)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTS_ReAssignShopToUser");
            proc.AddPara("@ACTION", "ShopReAssignUserLog");
            proc.AddPara("@FromDate", FromDate);
            proc.AddPara("@ToDate", ToDate);
            dt = proc.GetTable();
            return dt;
        }

        public DataTable GenerateReAssignShopList(String USER_ID)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTS_ReAssignShopToUser");
            proc.AddPara("@ACTION", "ReAssignShopList");
            proc.AddPara("@USER_ID", USER_ID);
            dt = proc.GetTable();
            return dt;
        }

        public DataTable GetddShopTypeDetails(String shoptype, String TypeID,String Action)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSInsertUpdateNewParty");
            proc.AddPara("@ACTION", Action);
            proc.AddPara("@retailer_id", TypeID);
            dt = proc.GetTable();
            return dt;
        }
        //Mantis Issue 25133
        public DataTable GenerateReAssignShopListGroupBeat(String USER_ID)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTS_ReAssignShopToGroupBeat");
            proc.AddPara("@ACTION", "ReAssignShopList");
            proc.AddPara("@GroupBeat_ID", USER_ID);
            dt = proc.GetTable();
            return dt;
        }
        //End of Mantis Issue 25133

        // Mantis Issue 25545
        public DataTable GenerateReAssignShopListForAreaRouteBeat(String USER_ID)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTS_ReAssignShopToUser");
            proc.AddPara("@ACTION", "ReAssignShopListForAreaRouteBeat");
            proc.AddPara("@USER_ID", USER_ID);
            dt = proc.GetTable();
            return dt;
        }
        public DataTable ShopReAssignUser_ForAreaRouteBeat(String USER_ID, String OLD_USER, String NEW_USER, String ShopCodes)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTS_ReAssignShopToUser");
            proc.AddPara("@ACTION", "ShopReAssignUser_ForAreaRouteBeat");
            proc.AddPara("@USER_ID", USER_ID);
            proc.AddPara("@OLD_USER", OLD_USER);
            proc.AddPara("@NEW_USER", NEW_USER);
            proc.AddPara("@ShopCodes", ShopCodes);
            dt = proc.GetTable();
            return dt;
        }
        public DataTable ShopReAssignUserLog_AreaRouteBeat(String FromDate, String ToDate)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTS_ReAssignShopToUser");
            proc.AddPara("@ACTION", "ShopReAssignUserLog_AreaRouteBeat");
            proc.AddPara("@FromDate", FromDate);
            proc.AddPara("@ToDate", ToDate);
            dt = proc.GetTable();
            return dt;
        }
        // End of Mantis Issue 25545
        // Rev 1.0
        public DataTable BulkModifyPartyLog(String FromDate, String ToDate)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSBulkModifyParty");
            proc.AddPara("@ACTION", "GetBulkModifyPartyLog");
            proc.AddPara("@FromDate", FromDate);
            proc.AddPara("@ToDate", ToDate);
            dt = proc.GetTable();
            return dt;
        }
        // End of Rev 1.0
        // Rev 2.0
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
        // End of Rev 2.0
    }
}

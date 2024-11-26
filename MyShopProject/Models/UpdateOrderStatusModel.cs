using DataAccessLayer;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class UpdateOrderStatusModel
    {
        public string selectedusrid { get; set; }
        public List<string> StateId { get; set; }
        public List<GetUserName> userlsit { get; set; }
        public List<GetUsersStates> states { get; set; }
        public List<string> empcode { get; set; }
        public string Is_PageLoad { get; set; }
        public List<string> shopId { get; set; }
        public List<Getmasterstock> shoplist { get; set; }
        public string Fromdate { get; set; }
        public string Todate { get; set; }
        public int Uniquecont { get; set; }
        
        public int IsSchemeDetails { get; set; }
        
        public int IsPaitentDetails { get; set; }
        
        public List<string> BranchId { get; set; }
        public List<GetBranch> modelbranch = new List<GetBranch>();
       
        public int IsShowMRP { get; set; }
        public int IsShowDiscount { get; set; }
        
        public string UPDATESTATUS { get; set; }

        public string ORDERSTATUSOLD { get; set; }

        public string ORDERSTATUSNEW { get; set; }


        public string STATUSId { get; set; }
        public List<STATUSLIST> STATUSLIST { get; set; }

        
        public DataTable GetUpdateStatusOption(string USERID)
        {
            ProcedureExecute proc;
            int rtrnvalue = 0;
            DataTable dt = new DataTable();
            try
            {
                using (proc = new ProcedureExecute("PRC_LMS_POINTSMASTER"))
                {
                    proc.AddVarcharPara("@ACTION", 100, "GetUpdateStatusOption");
                    proc.AddPara("@USERID",Convert.ToInt32(USERID));
                    dt = proc.GetTable();
                    return dt;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                proc = null;
            }
        }

        public class OrderUpdateDetailsSummary
        {
            public string EmployeeName { get; set; }
            public string BRANCHDESC { get; set; }
            public string shop_name { get; set; }
            public string ENTITYCODE { get; set; }
            public string address { get; set; }
            public string owner_contact_no { get; set; }
            public string Shoptype { get; set; }
            public string date { get; set; }
            public string OrderCode { get; set; }
            public decimal order_amount { get; set; }
            public long OrderId { get; set; }            
            public string Patient_Name { get; set; }
            public string Patient_Phone_No { get; set; }
            public string Patient_Address { get; set; }
            public string Hospital { get; set; }
            public string Email_Address { get; set; }
            public string ORDERSTATUS { get; set; }           

            public Int32 USERID { get; set; }

            public string ORDERSTATUSOLD { get; set; }

            public string ORDERSTATUSNEW { get; set; }
        }
    }

    public class STATUSLIST
    {
        public string STATUSID { get; set; }
        public string STATUSVALUE { get; set; }
    }

    
}
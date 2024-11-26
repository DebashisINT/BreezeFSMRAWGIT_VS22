//********************************************************************************************************************
// 1.0      v2.0.38    Sanchita    13/01/2023  DD Type should be shown based on the Type_ID & Parent_ID mapping as
//                                              per tbl_shoptypeDetails table. Refer: 25578
// 2.0      V2.0.38    Sanchita    27/01/2023  Assign to DD is not showing while making shop from Portal. Refer: 25606
// 3.0      V2.0.40    Sanchita    04-05-2023  A New Expense Report is Required for BP Poddar. Refer: 25833*
// 4.0      V2.0.41    Sanchita    06-06-2023  Inactive DD/PP is showing in the Assign to PP/DD list while creating any Shop
//                                             Refer: 26262 
// 5.0      v2.0.43    Sanchita    16-10-2023  On demand search is required in Product Master & Projection Entry
//                                             Mantis: 26858
// 6.0      V2.0.44    Sanchita    12-12-2023  A new design page is required as Contact (s) under CRM menu. Mantis: 27034  
// 7.0      V2.0.46    Sanchita    11/04/2024  0027348: FSM: Master > Contact > Parties [Delete Facility]
// 8.0      V2.0.49    Sanchita    04/10/2024  In Current stock Register report there shall be a stock import option. Mantis: 27707, 27724
// ********************************************************************************************************************
using DataAccessLayer;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace MyShop.Models
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class ShopAddress : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object GetShop(string SearchKey)
        {
            List<PPModel> listShop = new List<PPModel>();
            if (HttpContext.Current.Session["userid"] != null)
            {
                SearchKey = SearchKey.Replace("'", "''");

                BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();

                // Mantis Issue 24450,24451 [column "Shop_Owner_Contact" added]
                // Rev 4.0
                //DataTable Shop = oDBEngine.GetDataTable("select top(10)Shop_Code,Entity_Location,Replace(Shop_Name,'''','&#39;') as Shop_Name,EntityCode,Shop_Owner_Contact from tbl_Master_shop where (type=2 and Shop_Name like '%" + SearchKey + "%') or  (type=2 and EntityCode like '%" + SearchKey + "%') or (type=2 and Shop_Owner_Contact like '%" + SearchKey + "%')");

                ProcedureExecute proc = new ProcedureExecute("PRC_FTSInsertUpdateNewParty");
                proc.AddPara("@ACTION", "GetPPShop");
                proc.AddPara("@USER_ID", Convert.ToInt32(Session["userid"]));
                proc.AddPara("@SearchKey", SearchKey);
                DataTable Shop = proc.GetTable();
                // End of Rev 4.0

                // Mantis Issue 24450,24451 [ "Shop_Owner_Contact" added]
                listShop = (from DataRow dr in Shop.Rows
                            select new PPModel()
                            {
                                Shop_Code = dr["Shop_Code"].ToString(),
                                Shop_Name = dr["Shop_Name"].ToString(),
                                Entity_Location = Convert.ToString(dr["Entity_Location"]),
                                EntityCode = Convert.ToString(dr["EntityCode"]),
                                Shop_Owner_Contact = Convert.ToString(dr["Shop_Owner_Contact"])
                            }).ToList();
            }

            return listShop;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        // Rev 2.0
        //public object GetDDShop(string SearchKey, String ddType)
        public object GetDDShop(string SearchKey, String ShopTypes, String ddType)
            // End of Rev 2.0
        {
            List<PPModel> listShop = new List<PPModel>();
            if (HttpContext.Current.Session["userid"] != null)
            {
                SearchKey = SearchKey.Replace("'", "''");

                BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();

                // Mantis Issue 24450,24451 [column "Shop_Owner_Contact" added ]
                // Rev 1.0
                //DataTable Shop = oDBEngine.GetDataTable("select top(10)Shop_Code,Entity_Location,Replace(Shop_Name,'''','&#39;') as Shop_Name,EntityCode,Shop_Owner_Contact from tbl_Master_shop where (type=4 and Shop_Name like '%" + SearchKey + "%' and dealer_id='" + ddType + "') or  (type=4 and EntityCode like '%" + SearchKey + "%' and dealer_id='" + ddType + "') or (type=4 and Shop_Owner_Contact like '%" + SearchKey + "%' and dealer_id='" + ddType + "')");

                ProcedureExecute proc = new ProcedureExecute("PRC_FTSInsertUpdateNewParty");
                proc.AddPara("@ACTION", "GetDDShop");
                proc.AddPara("@USER_ID", Convert.ToInt32(Session["userid"]));
                proc.AddPara("@SearchKey", SearchKey);
                proc.AddPara("@dealer_id", ddType);
                // Rev 2.0
                proc.AddPara("@retailer_id", ShopTypes);
                // End of Rev 2.0
                DataTable Shop = proc.GetTable();
                // End of Rev 1.0

                // Mantis Issue 24450,24451 ["Shop_Owner_Contact" added ]
                listShop = (from DataRow dr in Shop.Rows
                            select new PPModel()
                            {
                                Shop_Code = dr["Shop_Code"].ToString(),
                                Shop_Name = dr["Shop_Name"].ToString(),
                                Entity_Location = Convert.ToString(dr["Entity_Location"]),
                                EntityCode = Convert.ToString(dr["EntityCode"]),
                                Shop_Owner_Contact = Convert.ToString(dr["Shop_Owner_Contact"])
                            }).ToList();
            }

            return listShop;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object GetRetailerShop(string SearchKey)
        {
            List<PPModel> listShop = new List<PPModel>();
            if (HttpContext.Current.Session["userid"] != null)
            {
                SearchKey = SearchKey.Replace("'", "''");

                BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();

                DataTable Shop = oDBEngine.GetDataTable("select top(10)Shop_Code,Entity_Location,Replace(Shop_Name,'''','&#39;') as Shop_Name,EntityCode from tbl_Master_shop where (type=1 and Shop_Name like '%" + SearchKey + "%' and retailer_id=2) or  (type=1 and EntityCode like '%" + SearchKey + "%' and retailer_id=2)");

                listShop = (from DataRow dr in Shop.Rows
                            select new PPModel()
                            {
                                Shop_Code = dr["Shop_Code"].ToString(),
                                Shop_Name = dr["Shop_Name"].ToString(),
                                Entity_Location = Convert.ToString(dr["Entity_Location"]),
                                EntityCode = Convert.ToString(dr["EntityCode"])
                            }).ToList();
            }

            return listShop;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object GetUserList(string SearchKey)
        {
            List<UsersModel> listUser = new List<UsersModel>();
            if (HttpContext.Current.Session["userid"] != null)
            {
                SearchKey = SearchKey.Replace("'", "''");

                BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();

                //DataTable Shop = oDBEngine.GetDataTable("select top(10)user_id,user_loginId,Replace(user_name,'''','&#39;') as user_name from tbl_Master_user where (user_inactive='N' and user_name like '%" + SearchKey + "%') or  (user_inactive='N' and user_loginId like '%" + SearchKey + "%')");
                ProcedureExecute proc = new ProcedureExecute("PRC_UserNameSearch");
                proc.AddPara("@USER_ID", Convert.ToInt32(Session["userid"]));
                proc.AddPara("@SearchKey", SearchKey);
                DataTable Shop = proc.GetTable();
                // Mantis Issue 24450,24451 [ EmployeeID added ]
                listUser = (from DataRow dr in Shop.Rows
                            select new UsersModel()
                            {
                                USER_NAME = dr["user_name"].ToString(),
                                USER_LOGINID = dr["user_loginId"].ToString(),
                                USER_ID = Convert.ToString(dr["user_id"]),
                                EMPLOYEEID = Convert.ToString(dr["EmployeeID"])
                            }).ToList();
            }

            return listUser;
        }

        // Rev 6.0
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object GetReferenceList(string SearchKey)
        {
            List<ReferenceUsersModel> listUser = new List<ReferenceUsersModel>();
            if (HttpContext.Current.Session["userid"] != null)
            {
                SearchKey = SearchKey.Replace("'", "''");

                BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();

                ProcedureExecute proc = new ProcedureExecute("PRC_FTSInsertUpdateCRMContact");
                proc.AddPara("@USER_ID", Convert.ToInt32(Session["userid"]));
                proc.AddPara("@SearchKey", SearchKey);
                proc.AddPara("@ACTION", "GETREFERENCELIST");
                DataTable Shop = proc.GetTable();
                listUser = (from DataRow dr in Shop.Rows
                            select new ReferenceUsersModel()
                            {
                                USER_NAME = dr["REF_NAME"].ToString(),
                                USER_ID = Convert.ToString(dr["REF_ID"]),
                                USER_LOGINID = dr["REF_PHONE"].ToString()
                            }).ToList();
            }

            return listUser;
        }
        // End of Rev 6.0

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object GetEmployeeList(string SearchKey)
        {
            List<EmployeeModel> listEmployee = new List<EmployeeModel>();
            if (HttpContext.Current.Session["userid"] != null)
            {
                SearchKey = SearchKey.Replace("'", "''");
                //BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();
                //DataTable Shop = oDBEngine.GetDataTable("select top(10)cnt_internalId,Replace(ISNULL(cnt_firstName,'')+' '+ISNULL(cnt_middleName,'')+ ' '+ISNULL(cnt_lastName,''),'''','&#39;') AS Employee_Name,cnt_UCC from tbl_master_contact where (cnt_firstName like '%" + SearchKey + "%') or  (cnt_middleName like '%" + SearchKey + "%') or  (cnt_lastName like '%" + SearchKey + "%')");
                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_EmployeeNameSearch");
                proc.AddPara("@USER_ID", Convert.ToInt32(Session["userid"]));
                proc.AddPara("@SearchKey", SearchKey);
                dt = proc.GetTable();

                listEmployee = (from DataRow dr in dt.Rows
                                select new EmployeeModel()
                            {
                                id = Convert.ToString(dr["cnt_internalId"]),
                                Employee_Code = Convert.ToString(dr["cnt_UCC"]),
                                Employee_Name = Convert.ToString(dr["Employee_Name"])
                            }).ToList();
            }

            return listEmployee;
        }
        //rev Pratik
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object GetEmployeeListDesigWise(string SearchKey, string Desig, string SearchKeyAE, string SearchKeyWD, string BranchId)
        {
            List<EmployeeModel> listEmployee = new List<EmployeeModel>();
            if (HttpContext.Current.Session["userid"] != null)
            {
                SearchKey = SearchKey.Replace("'", "''");
                //BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();
                //DataTable Shop = oDBEngine.GetDataTable("select top(10)cnt_internalId,Replace(ISNULL(cnt_firstName,'')+' '+ISNULL(cnt_middleName,'')+ ' '+ISNULL(cnt_lastName,''),'''','&#39;') AS Employee_Name,cnt_UCC from tbl_master_contact where (cnt_firstName like '%" + SearchKey + "%') or  (cnt_middleName like '%" + SearchKey + "%') or  (cnt_lastName like '%" + SearchKey + "%')");
                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_EmployeeNameSearch_Desg");
                proc.AddPara("@USER_ID", Convert.ToInt32(Session["userid"]));
                proc.AddPara("@SearchKey", SearchKey);
                proc.AddPara("@DesigId", Desig);
                proc.AddPara("@Action", Desig);
                proc.AddPara("@AeId", SearchKeyAE);
                proc.AddPara("@WdId", SearchKeyWD);
                proc.AddPara("@BranchId", BranchId);
                dt = proc.GetTable();

                listEmployee = (from DataRow dr in dt.Rows
                                select new EmployeeModel()
                                {
                                    id = Convert.ToString(dr["cnt_internalId"]),
                                    Employee_Code = Convert.ToString(dr["cnt_UCC"]),
                                    Employee_Name = Convert.ToString(dr["Employee_Name"])
                                }).ToList();
            }

            return listEmployee;
        }
        //End of rev Pratik
        //Rev Pratik For On demand Product Search
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object GetProductList(string SearchKey)
        {
            List<ProductModel> listProduct = new List<ProductModel>();
            if (HttpContext.Current.Session["userid"] != null)
            {
                SearchKey = SearchKey.Replace("'", "''");
                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_ProductNameSearch");
                proc.AddPara("@USER_ID", Convert.ToInt32(Session["userid"]));
                proc.AddPara("@SearchKey", SearchKey);
                dt = proc.GetTable();

                listProduct = (from DataRow dr in dt.Rows
                               select new ProductModel()
                                {
                                    sProducts_ID = Convert.ToInt32(dr["sProducts_ID"]),
                                    sProducts_Code = Convert.ToString(dr["sProducts_Code"]),
                                    sProducts_Name = Convert.ToString(dr["sProducts_Name"])
                                    //sProducts_Description = Convert.ToString(dr["sProducts_Description"])
                                }).ToList();
            }

            return listProduct;
        }
        //End of Rev Pratik For On demand Product Search
        //Rev Pratik For On demand Shop/Customer Search
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object GetCustomerList(string SearchKey)
        {
            List<CustomerModel> listShop = new List<CustomerModel>();
            if (HttpContext.Current.Session["userid"] != null)
            {
                SearchKey = SearchKey.Replace("'", "''");
                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_ShopNameSearch");
                proc.AddPara("@USER_ID", Convert.ToInt32(Session["userid"]));
                proc.AddPara("@SearchKey", SearchKey);
                dt = proc.GetTable();

                listShop = (from DataRow dr in dt.Rows
                               select new CustomerModel()
                               {
                                   Shop_Code = Convert.ToString(dr["Shop_Code"]),
                                   Shop_Name = Convert.ToString(dr["Shop_Name"]),
                               }).ToList();
            }

            return listShop;
        }
        //End of Rev Pratik For On demand Shop/Customer Search
        //Mantis Issue 25133
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object GetGroupBeatList(string SearchKey, string OldGroupBeatId)
        {
            List<GroupBeatModel> listUser = new List<GroupBeatModel>();
            if (HttpContext.Current.Session["userid"] != null)
            {
                SearchKey = SearchKey.Replace("'", "''");

                BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();

                //DataTable Shop = oDBEngine.GetDataTable("select top(10)user_id,user_loginId,Replace(user_name,'''','&#39;') as user_name from tbl_Master_user where (user_inactive='N' and user_name like '%" + SearchKey + "%') or  (user_inactive='N' and user_loginId like '%" + SearchKey + "%')");
                ProcedureExecute proc = new ProcedureExecute("PRC_GroupBeatNameSearch");
                proc.AddPara("@USER_ID", Convert.ToInt32(Session["userid"]));
                proc.AddPara("@SearchKey", SearchKey);
                proc.AddPara("@OldGroupBeatId", Convert.ToInt32(OldGroupBeatId));
                DataTable Shop = proc.GetTable();
                // Mantis Issue 24450,24451 [ EmployeeID added ]
                listUser = (from DataRow dr in Shop.Rows
                            select new GroupBeatModel()
                            {
                                ID = dr["ID"].ToString(),
                                NAME = dr["NAME"].ToString(),
                                CODE = dr["CODE"].ToString()
                            }).ToList();
            }

            return listUser;
        }
        //End of Mantis Issue 25133
        // Rev 3.0
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object GetEmployeeListHQWise(string SearchKey, string HQid)
        {
            List<EmployeeModel> listEmployee = new List<EmployeeModel>();
            if (HttpContext.Current.Session["userid"] != null)
            {
                SearchKey = SearchKey.Replace("'", "''");
                //BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();
                //DataTable Shop = oDBEngine.GetDataTable("select top(10)cnt_internalId,Replace(ISNULL(cnt_firstName,'')+' '+ISNULL(cnt_middleName,'')+ ' '+ISNULL(cnt_lastName,''),'''','&#39;') AS Employee_Name,cnt_UCC from tbl_master_contact where (cnt_firstName like '%" + SearchKey + "%') or  (cnt_middleName like '%" + SearchKey + "%') or  (cnt_lastName like '%" + SearchKey + "%')");
                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_EmployeeNameSearchByHQ");
                proc.AddPara("@USER_ID", Convert.ToInt32(Session["userid"]));
                proc.AddPara("@SearchKey", SearchKey);
                proc.AddPara("@HQid", HQid);
                dt = proc.GetTable();

                listEmployee = (from DataRow dr in dt.Rows
                                select new EmployeeModel()
                                {
                                    id = Convert.ToString(dr["cnt_internalId"]),
                                    Employee_Code = Convert.ToString(dr["cnt_UCC"]),
                                    Employee_Name = Convert.ToString(dr["Employee_Name"])
                                }).ToList();
            }

            return listEmployee;
        }
        // End of Rev 3.0
        // Rev 5.0
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object GetShopUserName(string SearchKey)
        {
            List<ShopUserAssign> Shoplist = new List<ShopUserAssign>();
            if (HttpContext.Current.Session["userid"] != null)
            {
                SearchKey = SearchKey.Replace("'", "''");

                BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();

                ProcedureExecute proc = new ProcedureExecute("PRC_PROJECTREPORT_LISTING");
                proc.AddPara("@ACTION", "CustomerNameSearch");
                proc.AddPara("@USERID", Convert.ToInt32(Session["userid"]));
                proc.AddPara("@SearchKey", SearchKey);
                DataTable Shop = proc.GetTable();
                
                Shoplist = (from DataRow dr in Shop.Rows
                            select new ShopUserAssign()
                            {
                                Shop_Code = dr["Shop_Code"].ToString(),
                                Shop_Name = dr["Shop_Name"].ToString()
                            }).ToList();
            }

            return Shoplist;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object GetProjectName(string SearchKey)
        {
            List<ProjectNameAssign> Projlist = new List<ProjectNameAssign>();
            if (HttpContext.Current.Session["userid"] != null)
            {
                string[] ar1 = SearchKey.Split(',');

                string SearchKey1 = Convert.ToString(ar1[0]);
                SearchKey = SearchKey1.Replace("'", "''");

                string ShopCode = Convert.ToString(ar1[1]);

                BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();

                ProcedureExecute proc = new ProcedureExecute("PRC_PROJECTREPORT_LISTING");
                proc.AddPara("@ACTION", "ProjectNameSearch");
                proc.AddPara("@USERID", Convert.ToInt32(Session["userid"]));
                proc.AddPara("@SearchKey", SearchKey);
                proc.AddPara("@Shop_Code", ShopCode);
                DataTable Shop = proc.GetTable();

                Projlist = (from DataRow dr in Shop.Rows
                            select new ProjectNameAssign()
                            {
                                Project_Id = dr["Project_Id"].ToString(),
                                Project_Name = dr["Project_Name"].ToString()
                            }).ToList();
            }

            return Projlist;
        }
        // End of Rev 5.0
        // Rev 7.0
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object GetShopListForMassDelete(string SearchKey)
        {
            List<ShopMassDeleteModel> listShop = new List<ShopMassDeleteModel>();
            if (HttpContext.Current.Session["userid"] != null)
            {
                SearchKey = SearchKey.Replace("'", "''");
                //DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_FTSInsertUpdateNewParty");
                proc.AddPara("@ACTION", "GetMassDeleteShopList");
                proc.AddPara("@USER_ID", Convert.ToInt32(Session["userid"]));
                proc.AddPara("@SearchKey", SearchKey);
                DataTable Shop = proc.GetTable();

                listShop = (from DataRow dr in Shop.Rows
                                select new ShopMassDeleteModel()
                                {
                                    Shop_Code = dr["Shop_Code"].ToString(),
                                    Shop_Name = dr["Shop_Name"].ToString(),
                                    Entity_Location = Convert.ToString(dr["Entity_Location"]),
                                    //EntityCode = Convert.ToString(dr["EntityCode"]),
                                    //Shop_Owner_Contact = Convert.ToString(dr["Shop_Owner_Contact"]),
                                    ShopType_Name = dr["ShopType_Name"].ToString()
                                }).ToList();
            }

            return listShop;
        }
        // End of Rev 7.0
        // Rev 8.0
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object GetShopList(string SearchKey)
        {
            List<ShopListModel> listShop = new List<ShopListModel>();
            if (HttpContext.Current.Session["userid"] != null)
            {
                SearchKey = SearchKey.Replace("'", "''");
                //DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_FTSINSERTUPDATECURRENTSTOCK");
                proc.AddPara("@ACTION", "GETSHOPLIST");
                proc.AddPara("@USER_ID", Convert.ToInt32(Session["userid"]));
                proc.AddPara("@SearchKey", SearchKey);
                DataTable Shop = proc.GetTable();

                listShop = (from DataRow dr in Shop.Rows
                            select new ShopListModel()
                            {
                                Shop_Code = dr["SHOP_CODE"].ToString(),
                                Shop_Name = dr["SHOP_NAME"].ToString(),
                                EntityCode = Convert.ToString(dr["ENTITYCODE"]),
                                ShopType_Name = dr["SHOPTYPENAME"].ToString(),
                                Shop_Owner_Contact = Convert.ToString(dr["SHOP_OWNER_CONTACT"]),
                                
                            }).ToList();
            }

            return listShop;
        }
        // End of Rev 8.0
    }

    public class PPModel
    {
        public string Shop_Code { get; set; }
        public string Shop_Name { get; set; }
        public string Entity_Location { get; set; }
        public string EntityCode { get; set; }
        // Mantis Issue 24450,24451
        public string Shop_Owner_Contact { get; set; }
        // End of Mantis Issue 24450,24451
    }

    public class UsersModel
    {
        public string USER_ID { get; set; }
        public string USER_NAME { get; set; }
        public string USER_LOGINID { get; set; } 
        // Mantis Issue 24450,24451
        public string EMPLOYEEID { get; set; }
        // End of Mantis Issue 24450,24451
    }

    public class EmployeeModel
    {
        public string id { get; set; }
        public string Employee_Name { get; set; }
        public string Employee_Code { get; set; }
    }
    //rev Pratik for On demand Product search
    public class ProductModel
    {
        public int sProducts_ID { get; set; }
        public string sProducts_Code { get; set; }
        public string sProducts_Name { get; set; }
        //public string sProducts_Description { get; set; }
    }
    //End of rev Pratik for on demand product search
    //rev Pratik for On demand Shop/Customer search
    public class CustomerModel
    {
        public string Shop_Code { get; set; }
        public string Shop_Name { get; set; }
    }
    //End of rev Pratik for on demand Shop/Customer search
    //Mantis Issue 25133
    public class GroupBeatModel
    {
        public string ID { get; set; }
        public string NAME { get; set; }
        public string CODE { get; set; }

    }
    //End of Mantis Issue 25133

    // Rev 6.0
    public class ReferenceUsersModel
    {
        public string USER_ID { get; set; }
        public string USER_NAME { get; set; }
        public string USER_LOGINID { get; set; }
    }
    // End of Rev 6.0
    // Rev 7.0
    public class ShopMassDeleteModel
    {
        public string Shop_Code { get; set; }
        public string Shop_Name { get; set; }
        public string Entity_Location { get; set; }
       // public string EntityCode { get; set; }
       // public string Shop_Owner_Contact { get; set; }
        public string ShopType_Name { get; set; }
    }
    // End of Rev 7.0
    // Rev 8.0
    public class ShopListModel
    {
        public string Shop_Code { get; set; }
        public string Shop_Name { get; set; }
        //public string Entity_Location { get; set; }
         public string EntityCode { get; set; }
         public string Shop_Owner_Contact { get; set; }
        public string ShopType_Name { get; set; }
    }
    // End of Rev 8.0
}

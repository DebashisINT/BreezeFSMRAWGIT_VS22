/******************************************************************************************************
 * Written by Sanchita on     01-09-2023       2.0.43      FSM - Dashboard - View Party - Enhancement required. Refer: 26753
 * ******************************************************************************************************/
using BusinessLogicLayer;
using MyShop.Models;
using SalesmanTrack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilityLayer;
using Models;
using BusinessLogicLayer.SalesmanTrack;
using DataAccessLayer;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class ViewPartyHierarchyController : Controller
    {
        // GET: MYSHOP/ViewPartyHierarchy
        UserList lstuser = new UserList();
        EmployeeMasterReport omodel = new EmployeeMasterReport();

        public ActionResult ViewPartyH()
        {
            DataTable dtbranch = lstuser.GetHeadBranchList(Convert.ToString(Session["userbranchHierarchy"]), "HO");
            DataTable dtBranchChild = new DataTable();
            if (dtbranch.Rows.Count > 0)
            {
                dtBranchChild = lstuser.GetChildBranch(Convert.ToString(Session["userbranchHierarchy"]));
                if (dtBranchChild.Rows.Count > 0)
                {
                    DataRow dr;
                    dr = dtbranch.NewRow();
                    dr[0] = 0;
                    dr[1] = "All";
                    dtbranch.Rows.Add(dr);
                    dtbranch.DefaultView.Sort = "BRANCH_ID ASC";
                    dtbranch = dtbranch.DefaultView.ToTable();
                }
            }
            omodel.modelbranch = APIHelperMethods.ToModelList<GetBranch>(dtbranch);
            string h_id = omodel.modelbranch.First().BRANCH_ID.ToString();
            ViewBag.HeadBranch = omodel.modelbranch;
            ViewBag.h_id = h_id;

            //string IsElectricianRequiredinViewParty = "0";
            //DBEngine obj1 = new DBEngine();
            //IsElectricianRequiredinViewParty = Convert.ToString(obj1.GetDataTable("select [value] from FTS_APP_CONFIG_SETTINGS WHERE [Key]='IsElectricianRequiredinViewParty'").Rows[0][0]);
            //ViewBag.IsElectricianRequiredinViewParty = IsElectricianRequiredinViewParty;

            return View();
        }

        //public ActionResult GetParty(string StateID, string TYPE_ID, string PARTY_ID, string IS_Electician, string BranchIds)
        public ActionResult GetParty(string StateID, string TYPE_ID, string PARTY_ID, string BranchIds)
        {

            List<PartyDashboard> AddressList = new List<PartyDashboard>();

            //Dashboard model = new Dashboard();
            //DataTable dtmodellatest = model.GETPartyDashboard(StateID, TYPE_ID, PARTY_ID, IS_Electician, BranchIds, Session["userid"].ToString());

            DataTable dtmodellatest = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_DASHBOARDPARTYDETAIL");
            proc.AddPara("@ACTION", "GETLISTSTATEWISE_HIERARCHY");
            proc.AddPara("@StateID", StateID);
            proc.AddPara("@TYPE_ID", TYPE_ID);
            proc.AddPara("@PARTY_ID", PARTY_ID);
            //proc.AddPara("@IS_Electician", IS_Electician);
            proc.AddPara("@CREATE_USERID", Session["userid"].ToString());
            proc.AddPara("@BRANCHID", BranchIds);
            dtmodellatest = proc.GetTable();

            //AddressList = APIHelperMethods.ToModelList<PartyDashboard>(dtmodellatest);
            if (dtmodellatest.Rows.Count > 0)
            {
                AddressList = (from DataRow dr in dtmodellatest.Rows
                           select new PartyDashboard()
                           {
                               shop_code = Convert.ToString(dr["shop_code"]),
                               Shop_Name = Convert.ToString(dr["Shop_Name"]),
                               Address = Convert.ToString(dr["Address"]),
                               Shop_Owner = Convert.ToString(dr["Shop_Owner"]),
                               Shop_Lat = Convert.ToString(dr["Shop_Lat"]),

                               Shop_Long = Convert.ToString(dr["Shop_Long"]),
                               Shop_Owner_Contact = Convert.ToString(dr["Shop_Owner_Contact"]),
                               PARTYSTATUS = Convert.ToString(dr["PARTYSTATUS"]),
                               MAP_COLOR = Convert.ToString(dr["MAP_COLOR"]),
                               Shop_CreateUser = Convert.ToString(dr["Shop_CreateUser"]),
                               state = Convert.ToString(dr["state"]),
                               PARENT_COLORCODE = Convert.ToString(dr["PARENT_MAP_COLORCODE"])
                           }).ToList();
            }

            //return Json(AddressList);
            var jsonResult = Json(AddressList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult GetType()
        {
            DBEngine obj = new DBEngine();
            DataTable dt = obj.GetDataTable("select id,Name from tbl_shoptypeDetails");
            List<ddl> objddl = new List<ddl>();
            objddl = APIHelperMethods.ToModelList<ddl>(dt);
            return Json(objddl);
        }
        public ActionResult GetTypeOutlet()
        {
            DBEngine obj = new DBEngine();
            DataTable dt = obj.GetDataTable("select shop_typeId,Name  from tbl_shoptype  where IsActive=1");
            List<ddlOutlet> objddl = new List<ddlOutlet>();
            objddl = APIHelperMethods.ToModelList<ddlOutlet>(dt);
            return Json(objddl);
        }
        public ActionResult GetPartyStatus()
        {
            DBEngine obj = new DBEngine();
            DataTable dt = obj.GetDataTable("select ID id ,PARTYSTATUS Name from FSM_PARTYSTATUS");
            List<ddl> objddl = new List<ddl>();
            objddl = APIHelperMethods.ToModelList<ddl>(dt);
            return Json(objddl);
        }


        public class ddl
        {
            public Int64 id { get; set; }
            public string Name { get; set; }

        }

        public class ddlOutlet
        {
            public Int64 shop_typeId { get; set; }
            public string Name { get; set; }

        }
    }
}
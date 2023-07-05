/****************************************************************************************************************************
*   1.0     v2.0.36     Sanchita    10/01/2023      Appconfig and User wise setting "IsAllDataInPortalwithHeirarchy = True" then 
*                                                   data in portal shall be populated based on Hierarchy Only. Refer: 25504
*   2.0     v2.0.40     Priti       19/05/2023      0026145:Modification in the ‘Configure Travelling Allowance’ page.
*   3.0     V2.0.41     Sanchita    02/06/2023      FSM - Message will be fired from first tab when logged out from the 2nd tab. Refer: 26273  
 *********************************************************************************************************************************/

using DataAccessLayer;
using Models;
using MyShop.Models;
using SalesmanTrack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using UtilityLayer;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class MasterPopulateController : Controller
    {
        UserList lstuser = new UserList();
        // GET: MYSHOP/MasterPopulate
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetStateList()
        {
            try
            {
                List<GetUsersStates> modelstate = new List<GetUsersStates>();
                DataTable dtstate = lstuser.GetStateList();
                modelstate = APIHelperMethods.ToModelList<GetUsersStates>(dtstate);

                return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_StatesPartial.cshtml", modelstate);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        //Rev 2.0
        public ActionResult GetBranchList(TravelConveyanceModelclass model)
        {
            try
            {
                string StateId = "";
                int i = 1;

                if (model.StateId != null && model.StateId.Count > 0)
                {
                    foreach (string item in model.StateId)
                    {
                        if (i > 1)
                            StateId = StateId + "," + item;
                        else
                            StateId = item;
                        i++;
                    }

                }
                List<GetBranch> modelBranch = new List<GetBranch>();
                DataTable dtBranch = lstuser.GetBranchList(StateId);
                modelBranch = APIHelperMethods.ToModelList<GetBranch>(dtBranch);

                return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_BranchPartial.cshtml", modelBranch);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
        public ActionResult GetAreaBranchWise(TravelConveyanceModelclass model)
        {
            try
            {
                string BranchId = "";
                int i = 1;

                if (model.BranchId != null && model.BranchId.Count > 0)
                {
                    foreach (string item in model.BranchId)
                    {
                        if (i > 1)
                            BranchId = BranchId + "," + item;
                        else
                            BranchId = item;
                        i++;
                    }

                }

                List<GetmasterArea> modelBranch = new List<GetmasterArea>();
                DataTable dtArea = lstuser.GetArealistByBranch(BranchId);
                modelBranch = APIHelperMethods.ToModelList<GetmasterArea>(dtArea);

                return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_AreaBranchWisePartial.cshtml", dtArea);



                //DataTable dtemp = lstuser.Getemplist(state, desig, Convert.ToString(Session["userid"]), dept);
               


                //DataView view = new DataView(dtemp);
                //DataTable distinctValues = view.ToTable(true, "empcode", "empname");

                //List<GetAllEmployee> modelemp = new List<GetAllEmployee>();
                //modelemp = APIHelperMethods.ToModelList<GetAllEmployee>(distinctValues);

                //return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_EmpPartial.cshtml", modelemp);



            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }
        //Rev 2.0 End

        public ActionResult GetShopList(EmployeeListModel model)
        {
            try
            {
                string StateId = "";
                int i = 1;

                if (model.StateId != null && model.StateId.Count > 0)
                {
                    foreach (string item in model.StateId)
                    {
                        if (i > 1)
                            StateId = StateId + "," + item;
                        else
                            StateId = item;
                        i++;
                    }

                }


                List<Getmasterstock> modelshop = new List<Getmasterstock>();
                DataTable dtshop = lstuser.GetShopListByparam(StateId, "ShopbyState");
                modelshop = APIHelperMethods.ToModelList<Getmasterstock>(dtshop);




                return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_ShopPartial.cshtml", modelshop);



            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }


        public ActionResult GetEmpList(EmployeeListModel model)
        {
            try
            {
                string state = "";
                int i = 1;

                if (model.StateId != null && model.StateId.Count > 0)
                {
                    foreach (string item in model.StateId)
                    {
                        if (i > 1)
                            state = state + "," + item;
                        else
                            state = item;
                        i++;
                    }

                }

                string desig = "";
                int k = 1;

                if (model.desgid != null && model.desgid.Count > 0)
                {
                    foreach (string item in model.desgid)
                    {
                        if (k > 1)
                            desig = desig + "," + item;
                        else
                            desig = item;
                        k++;
                    }

                }

                string dept = "";
                int L = 1;
                if (model.DeptId != null && model.DeptId.Count > 0)
                {
                    foreach (string item in model.DeptId)
                    {
                        if (L > 1)
                            dept = dept + "," + item;
                        else
                            dept = item;
                        L++;
                    }
                }


                // Rev 1.0
                //DataTable dtemp = lstuser.Getemplist(state, desig, model.userId, dept);
                DataTable dtemp = lstuser.Getemplist(state, desig, Convert.ToString(Session["userid"]), dept);
                // End of Rev 1.0


                DataView view = new DataView(dtemp);
                DataTable distinctValues = view.ToTable(true, "empcode", "empname");

                List<GetAllEmployee> modelemp = new List<GetAllEmployee>();
                modelemp = APIHelperMethods.ToModelList<GetAllEmployee>(distinctValues);

                return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_EmpPartial.cshtml", modelemp);



            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }

        //public ActionResult GetEmployeeList(EmployeeListModel model)
        //{
        //    try
        //    {
        //        string state = "";
        //        int i = 1;

        //        if (model.StateId != null && model.StateId.Count > 0)
        //        {
        //            foreach (string item in model.StateId)
        //            {
        //                if (i > 1)
        //                    state = state + "," + item;
        //                else
        //                    state = item;
        //                i++;
        //            }

        //        }

        //        string desig = "";
        //        int k = 1;

        //        if (model.desgid != null && model.desgid.Count > 0)
        //        {
        //            foreach (string item in model.desgid)
        //            {
        //                if (k > 1)
        //                    desig = desig + "," + item;
        //                else
        //                    desig = item;
        //                k++;
        //            }

        //        }



        //        DataTable dtemp = lstuser.Getemplist(state, desig);
        //        List<GetAllEmployee> modelemp = new List<GetAllEmployee>();
        //        modelemp = APIHelperMethods.ToModelList<GetAllEmployee>(dtemp);

        //        return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_EmpPartial.cshtml", modelemp);



        //    }
        //    catch
        //    {
        //        return RedirectToAction("Logout", "Login", new { Area = "" });

        //    }
        //}

        public ActionResult GetEmpListActive(EmployeeListModel model)
        {
            try
            {
                string state = "";
                int i = 1;

                if (model.StateId != null && model.StateId.Count > 0)
                {
                    foreach (string item in model.StateId)
                    {
                        if (i > 1)
                            state = state + "," + item;
                        else
                            state = item;
                        i++;
                    }

                }

                string desig = "";
                int k = 1;

                if (model.desgid != null && model.desgid.Count > 0)
                {
                    foreach (string item in model.desgid)
                    {
                        if (k > 1)
                            desig = desig + "," + item;
                        else
                            desig = item;
                        k++;
                    }

                }



                DataTable dtemp = lstuser.GetemplistActive(state, desig);


                DataView view = new DataView(dtemp);
                DataTable distinctValues = view.ToTable(true, "empcode", "empname");

                List<GetAllEmployee> modelemp = new List<GetAllEmployee>();
                modelemp = APIHelperMethods.ToModelList<GetAllEmployee>(distinctValues);

                return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_EmpPartialActive.cshtml", modelemp);



            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }

        public ActionResult GetProductList(EmployeeListModel model)
        {
            try
            {
                DataTable dtemp = lstuser.GetProductlist();

                List<ProductList> modelemp = new List<ProductList>();
                modelemp = APIHelperMethods.ToModelList<ProductList>(dtemp);

                return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_ProductPartial.cshtml", modelemp);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }

        public ActionResult GetEmpListSingleSelectActive(EmployeeListModel model)
        {
            try
            {
                string state = "";
                int i = 1;

                if (model.StateId != null && model.StateId.Count > 0)
                {
                    foreach (string item in model.StateId)
                    {
                        if (i > 1)
                            state = state + "," + item;
                        else
                            state = item;
                        i++;
                    }
                }

                string desig = "";
                int k = 1;

                if (model.desgid != null && model.desgid.Count > 0)
                {
                    foreach (string item in model.desgid)
                    {
                        if (k > 1)
                            desig = desig + "," + item;
                        else
                            desig = item;
                        k++;
                    }
                }

                DataTable dtemp = lstuser.GetemplistActive(state, desig);
                DataView view = new DataView(dtemp);
                DataTable distinctValues = view.ToTable(true, "empcode", "empname");
                List<GetAllEmployee> modelemp = new List<GetAllEmployee>();
                modelemp = APIHelperMethods.ToModelList<GetAllEmployee>(distinctValues);
                return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_EmpSinglePartialActive.cshtml", modelemp);

            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetCityList(EmployeeListModel model)
        {
            try
            {
                string StateId = "";
                int i = 1;

                if (model.StateId != null && model.StateId.Count > 0)
                {
                    foreach (string item in model.StateId)
                    {
                        if (i > 1)
                            StateId = StateId + "," + item;
                        else
                            StateId = item;
                        i++;
                    }
                }

                List<GetmasterCity> modelCity = new List<GetmasterCity>();
                DataTable dtCity = lstuser.GetCityListByparam(StateId, "CitybyState");
                modelCity = APIHelperMethods.ToModelList<GetmasterCity>(dtCity);
                return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_CityPartial.cshtml", modelCity);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetAreaList(EmployeeListModel model)
        {
            try
            {
                string CityId = "";
                int i = 1;
                if (model.CityId != null && model.CityId.Count > 0)
                {
                    foreach (string item in model.CityId)
                    {
                        if (i > 1)
                            CityId = CityId + "," + item;
                        else
                            CityId = item;
                        i++;
                    }
                }

                List<GetmasterArea> modelArea = new List<GetmasterArea>();
                DataTable dtArea = lstuser.GetAreaListByparam(CityId, "AreabyCity");
                modelArea = APIHelperMethods.ToModelList<GetmasterArea>(dtArea);
                return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_AreaPartial.cshtml", modelArea);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetStateListByCountry(EmployeeListModel model)
        {
            try
            {
                string CountryId = "";
                int i = 1;
                if (model.CountryId != null && model.CountryId.Count > 0)
                {
                    foreach (string item in model.CountryId)
                    {
                        if (i > 1)
                            CountryId = CountryId + "," + item;
                        else
                            CountryId = item;
                        i++;
                    }
                }

                List<GetUsersStates> modelstate = new List<GetUsersStates>();
                DataTable dtstate = lstuser.GetStateListCountryWise(CountryId);
                modelstate = APIHelperMethods.ToModelList<GetUsersStates>(dtstate);
                return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_CountrywiseStatesPartial.cshtml", modelstate);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetCountryList()
        {
            try
            {
                List<GetUsersCountry> modelCountry = new List<GetUsersCountry>();
                DataTable dtCountry = lstuser.GetCountryList();
                modelCountry = APIHelperMethods.ToModelList<GetUsersCountry>(dtCountry);
                return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_CountryListPartial.cshtml", modelCountry);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetEmployeeWiseStateList(string UserID)
        {
            try
            {
                List<GetUsersStates> modelstate = new List<GetUsersStates>();
                DataTable dtstate = lstuser.GetStateList(UserID);
                modelstate = APIHelperMethods.ToModelList<GetUsersStates>(dtstate);

                return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_EmployeeWiseStatePartial.cshtml", modelstate);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetDepartmentList()
        {
            try
            {
                List<GetUsersDepartment> modelDepartment = new List<GetUsersDepartment>();
                DataTable dtDepartment = lstuser.GetDepartmentList();
                modelDepartment = APIHelperMethods.ToModelList<GetUsersDepartment>(dtDepartment);

                return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_DepartmentPartial.cshtml", modelDepartment);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetSupervisorList()
        {
            try
            {
                List<GetUsersSupervisor> modelSupervisor = new List<GetUsersSupervisor>();
                DataTable dtSupervisor = lstuser.GetSupervisorList();
                modelSupervisor = APIHelperMethods.ToModelList<GetUsersSupervisor>(dtSupervisor);

                return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_SupervisorPartial.cshtml", modelSupervisor);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        //rev Pratik
        public ActionResult GetHeadBranchList(string childbranch, string Action)
        {
            try
            {
                List<GetBranch> modelbranch = new List<GetBranch>();
                DataTable dtbranch = lstuser.GetHeadBranchList(childbranch, Action);
                DataTable dtBranchChild = new DataTable();
                
                if (dtbranch.Rows.Count > 0)
                {
                    //Rev Debashis && Hierarchy wise Head Branch Bind
                    //ddlbranchHO.Items.Insert(0, new ListItem("All", "All"));
                    dtBranchChild = lstuser.GetChildBranch(Convert.ToString(Session["userbranchHierarchy"]));
                    if (dtBranchChild.Rows.Count > 0)
                    {
                        DataRow dr;
                        dr = dtbranch.NewRow();
                        dr[0] = 0;
                        dr[1] = "All";
                        dtbranch.Rows.Add(dr);
                        //modelbranch = APIHelperMethods.ToModelList<GetBranch>(0, new ListItem("All", "All"));
                        //modelbranch.Items.Insert(0, new ListItem("All", "All"));
                    }
                    //End of Rev Debashis
                }
                modelbranch = APIHelperMethods.ToModelList<GetBranch>(dtbranch);
                return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_HeadBranchPartial.cshtml", modelbranch);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }


        public ActionResult GetChildBranchList(string Hoid)
        {
            BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(string.Empty);
            try
            {
                List<GetBranch> modelbranch = new List<GetBranch>();
                //DataTable dtbranch = lstuser.GetHeadBranchList(Hoid, Hoid);
                //DataTable dtBranchChild = new DataTable();
                DataTable ComponentTable = new DataTable();
                //if ()
                //{
                    
                //}
                
                if (Hoid == null || Hoid == "")
                {
                    Hoid = Session["Hoid"].ToString();
                }
                if (Hoid != "0")
                {
                    Session["Hoid"] = Hoid;
                    ComponentTable = lstuser.GetBranch(Convert.ToString(Session["userbranchHierarchy"]), Hoid);
                }
                else
                {
                    Session["Hoid"] = Hoid;
                    //ComponentTable = oDBEngine.GetDataTable("select * from (select branch_id as ID,branch_description,branch_code from tbl_master_branch a where a.branch_id=1  union all select branch_id as ID,branch_description,branch_code from tbl_master_branch b where b.branch_parentId=1) a order by branch_description");
                    //rev Pratik
                    //ComponentTable = oDBEngine.GetDataTable("select * from (select branch_id as BRANCH_ID,branch_description as CODE,branch_code from tbl_master_branch a where a.branch_id=1  union all select branch_id as BRANCH_ID,branch_description as CODE,branch_code from tbl_master_branch b where b.branch_parentId=1) a order by CODE");                    
                    //** By Default Head Office Code Should be "1" but in this particular case there was an exception. Thats why we have to use in and distinct in this query **//

                    //ComponentTable = oDBEngine.GetDataTable("select * from (select branch_id as BRANCH_ID,branch_description as CODE,branch_code from tbl_master_branch a where a.branch_id=(select branch_id from tbl_master_branch where branch_code='HO')  union all select branch_id as BRANCH_ID,branch_description as CODE,branch_code from tbl_master_branch b where b.branch_parentId=(select branch_id from tbl_master_branch where branch_code='HO')) a order by CODE");
                    ComponentTable = oDBEngine.GetDataTable("select distinct * from (select branch_id as BRANCH_ID,branch_description as CODE,branch_code from tbl_master_branch a where a.branch_id in (1,119)  union all select branch_id as BRANCH_ID,branch_description as CODE,branch_code from tbl_master_branch b where b.branch_parentId in (1,119)) a order by CODE");
                    //End of rev Pratik
                }
                modelbranch = APIHelperMethods.ToModelList<GetBranch>(ComponentTable);
                return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_HeadBranchPartial.cshtml", modelbranch);
               
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public JsonResult GetSelectedChildBranchList(string Hoid)
        {
            BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(string.Empty);
            try
            {
                List<GetBranch> modelbranch = new List<GetBranch>();
                //DataTable dtbranch = lstuser.GetHeadBranchList(Hoid, Hoid);
                //DataTable dtBranchChild = new DataTable();
                DataTable ComponentTable = new DataTable();
                //if ()
                //{

                //}

                if (Hoid == null || Hoid == "")
                {
                    Hoid = Session["Hoid"].ToString();
                }
                if (Hoid != "0")
                {
                    Session["Hoid"] = Hoid;
                    ComponentTable = lstuser.GetBranch(Convert.ToString(Session["userbranchHierarchy"]), Hoid);
                }
                else
                {
                    Session["Hoid"] = Hoid;
                    //ComponentTable = oDBEngine.GetDataTable("select * from (select branch_id as ID,branch_description,branch_code from tbl_master_branch a where a.branch_id=1  union all select branch_id as ID,branch_description,branch_code from tbl_master_branch b where b.branch_parentId=1) a order by branch_description");
                    //Mantis Issue 24869
                    //** By Default Head Office Code Should be "1" but in this particular case there was an exception. Thats why we have to use in and distinct in this query **//

                    //ComponentTable = oDBEngine.GetDataTable("select * from (select branch_id as BRANCH_ID,branch_description as CODE,branch_code from tbl_master_branch a where a.branch_id=1  union all select branch_id as BRANCH_ID,branch_description as CODE,branch_code from tbl_master_branch b where b.branch_parentId=1) a order by CODE");
                    ComponentTable = oDBEngine.GetDataTable("select distinct * from (select branch_id as BRANCH_ID,branch_description as CODE,branch_code from tbl_master_branch a where a.branch_id in(1,119)  union all select branch_id as BRANCH_ID,branch_description as CODE,branch_code from tbl_master_branch b where b.branch_parentId in(1,119)) a order by CODE");
                    //End of Mantis Issue 24869
                }
                modelbranch = APIHelperMethods.ToModelList<GetBranch>(ComponentTable);
                return Json(modelbranch, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetChannelList()
        {
            BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(string.Empty);
            try
            {
                List<GetChannel> modelChannel = new List<GetChannel>();
                DataTable ComponentTable = new DataTable();

                ComponentTable = oDBEngine.GetDataTable("select ch_id,ch_Channel from Employee_Channel");
                
                modelChannel = APIHelperMethods.ToModelList<GetChannel>(ComponentTable);
                return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_ChannelPartial.cshtml", modelChannel);

            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public JsonResult GetSelectedChannelList()
        {
            BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(string.Empty);
            try
            {
                List<GetChannel> modelChannel = new List<GetChannel>();
                DataTable ComponentTable = new DataTable();
                ComponentTable = oDBEngine.GetDataTable("select ch_id,ch_Channel from Employee_Channel");
                modelChannel = APIHelperMethods.ToModelList<GetChannel>(ComponentTable);
                return Json(modelChannel, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        // Rev 3.0
        public ActionResult checkSessionLogout()
        {
            SessionLogoutCheck ret = new SessionLogoutCheck();

            if (Session["userid"] == null)
            {
                ret.SessionLoddedOut = "1";
            }
            else
            {
                ret.SessionLoddedOut = "0";
            }

            return Json(ret, JsonRequestBehavior.AllowGet);

        }
        // End of Rev 3.0
    }
}
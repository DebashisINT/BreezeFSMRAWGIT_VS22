using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BusinessLogicLayer;
using SalesmanTrack;
using System.Data;
using UtilityLayer;
using System.Web.Script.Serialization;
using MyShop.Models;
using BusinessLogicLayer.SalesmanTrack;
using DevExpress.Web.Mvc;


namespace MyShop.Areas.MYSHOP.Controllers
{
    public class EmployeeListController : Controller
    {
        UserList lstuser = new UserList();
        EmployeeHiarchy objemployee = new EmployeeHiarchy();
        DataTable dtuser = new DataTable();
        public ActionResult List()
        {
            try
            {
                Session["TreeListState"] = null;
                ViewBag.ShowServiceColumns = false;
                Session["TreeListState"] = null;
                ActivityInput omodel = new ActivityInput();
                string userid = Session["userid"].ToString();
                dtuser = objemployee.GetEmployeeist(userid);
                List<EmployeeHiarchyModel> model = new List<EmployeeHiarchyModel>();
                model = APIHelperMethods.ToModelList<EmployeeHiarchyModel>(dtuser);

                return View(model);
            }

            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }


    
        public ActionResult _PartialEmployeeList()
        {
            try
            {
                ActivityInput omodel = new ActivityInput();
                string userid = Session["userid"].ToString();
                dtuser = objemployee.GetEmployeeist(userid);
                List<EmployeeHiarchyModel> model = new List<EmployeeHiarchyModel>();
                model = APIHelperMethods.ToModelList<EmployeeHiarchyModel>(dtuser);
                TempData["Getdata"] = model;
                return PartialView("_PartialEmployeeList", model);
            }

            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }

        }
        public ActionResult PartialEmployeeListcallback()
        {
            try
            {
                
                ActivityInput omodel = new ActivityInput();
                string userid = Session["userid"].ToString();
                dtuser = objemployee.GetEmployeeist(userid);
                List<EmployeeHiarchyModel> model = new List<EmployeeHiarchyModel>();
                if (DevExpressHelper.IsCallback)
                {
         
                    model = APIHelperMethods.ToModelList<EmployeeHiarchyModel>(dtuser);

                    return PartialView("_PartialEmployeeList", model);
                }
                return PartialView("_PartialEmployeeList", model);
            }

            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }

        }


        [HttpPost]
        public ActionResult GetShopList(string selectedusrid)
        {
            ShopslistInput model = new ShopslistInput();
            model.selectedusrid = selectedusrid;
            return RedirectToAction("Getshoplist", "ShopList", model);
        }
    }
}
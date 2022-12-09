using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilityLayer;
using BusinessLogicLayer.SalesmanTrack;
using DevExpress.Web.Mvc;
using DevExpress.Web;
using SalesmanTrack;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class HomeLocationController : Controller
    {
        //HomeLocationBL lstuser = new HomeLocationBL();
        HomeLocationBL objshop = new HomeLocationBL();
        public ActionResult Index()
        {
            try
            {

                HomeLocationModel omodel = new HomeLocationModel();
                string userid = Session["userid"].ToString();
                List<ListHomeLocation> modelcounter = new List<ListHomeLocation>();
                //DataTable dtshoptypes = lstuser.GetShopTypes();
                //modelcounter = APIHelperMethods.ToModelList<shopCounterTypes>(dtshoptypes);
                //omodel.Shoptypes = modelcounter;
                omodel.Employee = null;
                omodel.allEmployee = "";

                if (TempData["ExportHomeLocation"] != null)
                {
                    //omodel.selectedusrid = TempData["Shopuser"].ToString();
                    TempData.Clear();
                }
                return View(omodel);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
        public ActionResult GetHomelistPartial(HomeLocationModel model)
        {
            try
            {
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
                List<ListHomeLocation> omel = new List<ListHomeLocation>();

                DataTable dt = new DataTable();
                string datfrmat = "";
                string dattoat = "";

                string Employee = "";
                int i = 1;

                if (model.Employee != null && model.Employee.Count > 0)
                {
                    foreach (string item in model.Employee)
                    {
                        if (i > 1)
                            Employee = Employee + "," + item;
                        else
                            Employee = item;
                        i++;
                    }
                }
                if (model.Ispageload == "1")
                {
                    dt = objshop.GetShopListEmployeewise(Employee, model.allEmployee, "LIST", null, Convert.ToInt32(Session["userid"]));

                    if (dt.Rows.Count > 0)
                    {
                        omel = APIHelperMethods.ToModelList<ListHomeLocation>(dt);
                        TempData["ExportHomeLocation"] = omel;
                        TempData.Keep();
                    }
                    else
                    {
                        TempData["ExportHomeLocation"] = null;
                        TempData.Keep();
                    }
                    return PartialView("_PartialHomeloactionGrid", omel);
                }
                else
                {
                    return PartialView("_PartialHomeloactionGrid", omel);
                }
            }
            catch
            {
                //   return Redirect("~/OMS/Signoff.aspx");
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
        public ActionResult ExportLocationlist(int type)
        {
            // List<AttendancerecordModel> model = new List<AttendancerecordModel>();
            ViewData["ExportHomeLocation"] = TempData["ExportHomeLocation"];
            TempData.Keep();

            if (ViewData["ExportHomeLocation"] != null)
            {

                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(), ViewData["ExportHomeLocation"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(), ViewData["ExportHomeLocation"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(), ViewData["ExportHomeLocation"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(), ViewData["ExportHomeLocation"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(), ViewData["ExportHomeLocation"]);
                    default:
                        break;
                }
            }
            //TempData["Exportcounterist"] = TempData["Exportcounterist"];
            //TempData.Keep();
            return null;
        }
        private GridViewSettings GetEmployeeBatchGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "Home Location";
            // settings.CallbackRouteValues = new { Controller = "Employee", Action = "ExportEmployee" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Home Location";

            settings.Columns.Add(column =>
            {
                column.Caption = "Employee Name";
                column.FieldName = "Emp_Name";

            });
            settings.Columns.Add(column =>
            {
                column.Caption = "Address";
                column.FieldName = "address";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "City";
                column.FieldName = "cityname";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "State";
                column.FieldName = "statename";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Pincode";
                column.FieldName = "pin_code";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Latitude";
                column.FieldName = "Latitude";


            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Longatude";
                column.FieldName = "longatude";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Update Date";
                column.FieldName = "UpdateDate";
                column.PropertiesEdit.DisplayFormatString = "dd/MM/yyyy";
            });

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }
        public ActionResult HomeLocationModify(string EmployeeId)
        {
            DataTable dt = new DataTable();
            HomeLocationDetails omel = new HomeLocationDetails();

            DataTable dtCity = objshop.GetCityList();
            List<cityLists> modelCity = new List<cityLists>();
            modelCity = APIHelperMethods.ToModelList<cityLists>(dtCity);
            omel.citylst = modelCity;

            DataTable dtState = objshop.GetStateList();
            List<StateLists> modelState = new List<StateLists>();
            modelState = APIHelperMethods.ToModelList<StateLists>(dtState);
            omel.statelst = modelState;

            DataTable dtCountry = objshop.GetCountryList();
            List<CountryLists> modelCountry = new List<CountryLists>();
            modelCountry = APIHelperMethods.ToModelList<CountryLists>(dtCountry);
            omel.Countrylst = modelCountry;

            dt = objshop.GetShopListEmployeewise(null, null, "DETAILS", Convert.ToInt32(EmployeeId));
            // omel = APIHelperMethods.ToModel<HomeLocationDetails>(dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                omel.Emp_Name = dt.Rows[0]["Emp_Name"].ToString();
                omel.address = dt.Rows[0]["address"].ToString();
                omel.Latitude = dt.Rows[0]["Latitude"].ToString();
                omel.longatude = dt.Rows[0]["longatude"].ToString();
                omel.UpdateDate = dt.Rows[0]["UpdateDate"].ToString();
                omel.UserID = Convert.ToInt64(dt.Rows[0]["UserID"].ToString());
                omel.cityID = 0;
                omel.stateID = 0;
                if (!string.IsNullOrEmpty(dt.Rows[0]["city_id"].ToString()))
                {
                    omel.cityID = Convert.ToInt64(dt.Rows[0]["city_id"].ToString());
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["StateCode"].ToString()))
                {
                    omel.stateID = Convert.ToInt64(dt.Rows[0]["StateCode"].ToString());
                }
                omel.PinCode = dt.Rows[0]["pin_code"].ToString();
                omel.CountryID = 1;
            }
            //string userid = Session["userid"].ToString();
            //dtuser = lstuser.GetUserList(userid);
            //List<Usersshopassign> model = new List<Usersshopassign>();
            //model = APIHelperMethods.ToModelList<Usersshopassign>(dtuser);
            //omel.userslist = model;

            return PartialView("_PartialEditHomelocation", omel);

        }

        public ActionResult DeleteHomeLocation(string User_ID)
        {
            HomeLocationModel omodel = new HomeLocationModel();
            string userid = Convert.ToString(Session["userid"]);
            DataTable dt = new DataTable();
            int i = objshop.GetHomeLocationDelete("DELETE", User_ID);
            if (i > 0)
            {
                return Json("Success");
            }
            else
            {
                return Json("Failure");

            }
            return View(omodel);

        }

        [HttpPost]
        public ActionResult LocationSubmit(HomeLocationUpdate model)
        {
            if (ModelState.IsValid)
            {
                int gets = 0;
                gets = objshop.GetHomeLocationUpdate("UPDATE", model.address, model.Latitude, model.longatude, model.UserID, model.cityName, model.stateName, model.PinCode);
                if (gets > 0)
                {
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            else
            {
                return Json(false);
            }

        }


    }
}
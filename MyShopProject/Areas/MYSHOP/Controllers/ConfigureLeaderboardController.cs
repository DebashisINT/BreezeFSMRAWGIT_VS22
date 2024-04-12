/**********************************************************************************************************************************
 * Written by   Sanchita  for V2.0.46  on   29-03-2024     27299: One settings page required for Leaderboard in the backend. Below points should be captured for different section.                                          
 * ***********************************************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer;
using System.Data;
using MyShop.Models;
using UtilityLayer;
using DevExpress.Web.Mvc;
using DevExpress.Web;
using DataAccessLayer;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class ConfigureLeaderboardController : Controller
    {
        CommonBL objSystemSettings = new CommonBL();
        DataTable dtvisitloc = new DataTable();
        List<pointsection> modelpoints = new List<pointsection>();
        ConfigueLeaderboardModel omodel = new ConfigueLeaderboardModel();

        // GET: MYSHOP/ConfigureLeaderboard
        public ActionResult Index()
        {
            try {

                EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/ConfigureLeaderboard/Index");
                ViewBag.CanAdd = rights.CanAdd;
                ViewBag.CanExport = rights.CanExport;
                ViewBag.CanEdit = rights.CanEdit;
                
                string userid = Convert.ToString(Session["userid"]);

                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_FSMLEADERBOARDMASTER");
                proc.AddPara("@Action", "GETPOINTS");
                dt = proc.GetTable();

                modelpoints = APIHelperMethods.ToModelList<pointsection>(dt);
                omodel.section = modelpoints;

                omodel.IS_ACTIVE = true;

                return View(omodel);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public ActionResult ConfigurationPartial()
        {
            try {
                EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/ConfigureLeaderboard/Index");
                ViewBag.CanAdd = rights.CanAdd;
                ViewBag.CanExport = rights.CanExport;
                ViewBag.CanEdit = rights.CanEdit;
               
                DataSet ds = new DataSet();
                ProcedureExecute proc = new ProcedureExecute("PRC_FSMLEADERBOARDMASTER");
                proc.AddPara("@Action", "GETPOINTSLISTING");
                ds = proc.GetDataSet();


                List<pointdata> modelpoints = new List<pointdata>();
                ConfigueLeaderboardModel omodel = new ConfigueLeaderboardModel();

                DataTable dtList = new DataTable();
                dtList = ds.Tables[0];
            

                if (dtList.Rows.Count > 0)
                {
                    modelpoints = APIHelperMethods.ToModelList<pointdata>(dtList);
                    omodel.points = modelpoints;
                    TempData["ExportConfiguration"] = omodel.points;
                    TempData.Keep();
                }
                else
                {
                    TempData["ExportConfiguration"] = null;
                    TempData.Clear();

                }

                return PartialView("_PartialPointConfiguration", omodel);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public ActionResult ConfigurationInsert(ConfigueLeaderboardModel model)
        {
            try
            {
                string userid = Convert.ToString(Session["userid"]);

                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_FSMLEADERBOARDMASTER");
                int i = 0;
                proc.AddPara("@Action", "UPDATEPOINTAMOUNT");
                proc.AddPara("@PointSectionId", model.PointSectionId);
                proc.AddPara("@PointAmount", model.PointAmount);
                proc.AddPara("@IsActive", model.IsActive);
                proc.AddPara("@USERID", userid);
                i = proc.RunActionQuery();

                if (i > 0)
                {
                    return Json("Success");
                }
                else
                {
                    return Json("Failure");

                }
            }
            catch (Exception ex)
            {
                throw(ex);
            }

        }
        public ActionResult EditConfiguration(string PointSectionId)
        {
            try
            {
                string userid = Convert.ToString(Session["userid"]);
                DataTable dt = new DataTable();

                ProcedureExecute proc = new ProcedureExecute("PRC_FSMLEADERBOARDMASTER");
                proc.AddPara("@Action", "EDITPOINTAMOUNT");
                proc.AddPara("@PointSectionId", PointSectionId);
                dt = proc.GetTable();


                List<pointdata> modelpoints = new List<pointdata>();
                ConfigueLeaderboardModel omodel = new ConfigueLeaderboardModel();

                modelpoints = APIHelperMethods.ToModelList<pointdata>(dt);
                omodel.points = modelpoints;

                return Json(omodel);
            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }

        public ActionResult ExportConfiguration(int type)
        {

            ViewData["ExportConfiguration"] = TempData["ExportConfiguration"];
            TempData.Keep();

            if (ViewData["ExportConfiguration"] != null)
            {

                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(), ViewData["ExportConfiguration"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(), ViewData["ExportConfiguration"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(), ViewData["ExportConfiguration"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(), ViewData["ExportConfiguration"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(), ViewData["ExportConfiguration"]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetEmployeeBatchGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "Configure Leaderboard";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "ConfigureLeaderboard";

            
            settings.Columns.Add(x =>
            {
                x.FieldName = "pointID";
                x.Caption = "Serial";
                x.ExportWidth = 50;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "POINT_SECTION";
                x.Caption = "Section";
                x.ExportWidth = 300;
            });


            settings.Columns.Add(x =>
            {
                x.FieldName = "POINT_VALUE";
                x.Caption = "Point Value";
                x.ExportWidth = 300;
                x.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "IS_ACTIVE";
                x.Caption = "Active";
                x.ExportWidth = 150;
            });


            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

    }
}
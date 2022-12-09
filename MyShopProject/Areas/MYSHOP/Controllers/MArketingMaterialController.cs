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
using System.IO;
using DevExpress.Web.Mvc;
using DevExpress.Web;
using BusinessLogicLayer.SalesmanTrack;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class MArketingMaterialController : Controller
    {
        UserList lstuser = new UserList();
        Marketingmaterials objmaterial = new Marketingmaterials();
        DataTable dtuser = new DataTable();

        public ActionResult Materials()
        {
            try
            {
                string userid = Session["userid"].ToString();
                DistanceDate omodel = new DistanceDate();
                omodel.Date = DateTime.Now.ToString("dd-MM-yyyy");
                return View(omodel);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetmaterialDetails()
        {
            try
            {
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
                MoaterialDetailsReportOutput omel = new MoaterialDetailsReportOutput();
                DataSet ds = new DataSet();
                string datfrmat = "";
                string dattoat = "";

                ds = objmaterial.GetMarketingDetails();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    omel.mateerial = APIHelperMethods.ToModelList<MaterialName>(ds.Tables[0]);
                    omel.mateerialpop = APIHelperMethods.ToModelList<MaterialName>(ds.Tables[1]);
                    omel.markeingdetails = APIHelperMethods.ToModelList<Marketingetails>(ds.Tables[2]);
                    TempData["Exportmaterial"] = omel.markeingdetails;
                    TempData.Keep();
                }

                return PartialView("_PartialMaterialsDetails", omel);

            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetmaterialImages(string shopId)
        {
            String webmaturl = System.Configuration.ConfigurationSettings.AppSettings["MaterialImageURL"];
            DataTable dt = new DataTable();
            List<MaterialImages> model = new List<MaterialImages>();
            dt = objmaterial.GetMaterialImagesDetails(shopId, webmaturl);
            model = APIHelperMethods.ToModelList<MaterialImages>(dt);
            return PartialView("_PartialMaterialImages", model);
        }

        public ActionResult Exportmateriallist(int type)
        {
            List<AttendancerecordModel> model = new List<AttendancerecordModel>();
            if (TempData["Exportmaterial"] != null)
            {

                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(), TempData["Exportmaterial"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(), TempData["Exportmaterial"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(), TempData["Exportmaterial"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(), TempData["Exportmaterial"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(), TempData["Exportmaterial"]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetEmployeeBatchGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "Marketing Materials";


            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Marketing Materials";

          
            settings.Columns.Add(column =>
            {
                column.Caption = "Shop Name";
                column.FieldName = "Shop_Name";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "UserName";
                column.FieldName = "UserName";

            });

            settings.Columns.Add(column =>
            {

                column.Caption = "Retail Branding - Nordy";
                column.FieldName = "Material_1";

            });

            settings.Columns.Add(column =>
            {

                column.Caption = "Retail Branding - Wall Display";
                column.FieldName = "Material_2";

            });


            settings.Columns.Add(column =>
            {
                column.Caption = "Retail Branding - Counter Top";
                column.FieldName = "Material_3";

            });

            settings.Columns.Add(column =>
            {

                column.Caption = "Retail Branding - Poster";
                column.FieldName = "Material_4";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Retail Branding -  Lit Flange";
                column.FieldName = "Material_5";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Retail Branding - Non Lit Flange";
                column.FieldName = "Material_6";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Retail Branding - Counter Board/ Sun Board";
                column.FieldName = "Material_7";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Retail Branding -  GSB";
                column.FieldName = "Material_8";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Retail Branding - Banner";
                column.FieldName = "Material_9";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Retail Branding - Non Lit Board";
                column.FieldName = "Material_10";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Pop Materials - Poster";
                column.FieldName = "Material_11";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Pop Materials -  Estimate Pad";
                column.FieldName = "Material_12";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Pop Materials - Leaflet";
                column.FieldName = "Material_13";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Pop Materials - Pen";
                column.FieldName = "Material_14";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Pop Materials - Estimate Pad";
                column.FieldName = "Material_15";

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


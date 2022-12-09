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

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class MonthlyreportvisitdurationController : Controller
    {

        UserList lstuser = new UserList();
        Totaldurationmonthly objshop = new Totaldurationmonthly();
        DataTable dtuser = new DataTable();

        public ActionResult VisitDuration()
        {
            DurationshpsDate omodel = new DurationshpsDate();
            omodel.Date = DateTime.Now.ToString("dd-MM-yyyy");
            return View(omodel);
        }

        public ActionResult GetDurationshopDetails(VisitsDurationhopReportInput model)
        {
            try
            {
                //model.Month = "2";
                //model.Year = "2018";
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
                MonthlyVisitDurationReportOutput omel = new MonthlyVisitDurationReportOutput();

                
                DataSet ds = new DataSet();
                string datfrmat = "";
                string dattoat = "";


                string userid = Session["userid"].ToString();

                ds = objshop.GetTotalDurationmonthly(model.Month, userid, Int32.Parse(model.Year));

                if (ds.Tables[0].Rows.Count > 0)
                {

                    omel.dates = APIHelperMethods.ToModelList<DateormatsmontwiseDuration>(ds.Tables[0]);
                    omel.visitduration = APIHelperMethods.ToModelList<VisitDurationReport>(ds.Tables[1]);
                    //omel.users = APIHelperMethods.ToModelList<Userformats>(ds.Tables[2]);

                    TempData["ExportShopvisitduration"] = omel.visitduration;
                    TempData.Keep();
                }

                return PartialView("_PartialVisitDurationReport", omel);
                //  return PartialView("_PartialShopListgridview", omel);

            }
            catch
            {

                //   return Redirect("~/OMS/Signoff.aspx");

                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }


        public ActionResult Exportdurationtlist(int type)
        {
            List<AttendancerecordModel> model = new List<AttendancerecordModel>();
            if (TempData["ExportShopvisitduration"] != null)
            {

                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(), TempData["ExportShopvisitduration"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(), TempData["ExportShopvisitduration"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(), TempData["ExportShopvisitduration"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(), TempData["ExportShopvisitduration"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(), TempData["ExportShopvisitduration"]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetEmployeeBatchGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "Monthly Report Shop Visit Duration";

            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Monthly Report Shop Visit Duration";


            settings.Columns.Add(column =>
            {
                column.Caption = "Reporting Name";
                column.FieldName = "ReportName";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "UserName";
                column.FieldName = "UserName";

            });
            settings.Columns.Add(column =>
            {
                column.Caption = "Designation";
                column.FieldName = "Designation";

            });


            settings.Columns.Add(column =>
            {
                column.Caption = "1";
                column.FieldName = "Date_1";
                column.PropertiesEdit.DisplayFormatString = "0.00";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "2";

                column.FieldName = "Date_2";
                column.PropertiesEdit.DisplayFormatString = "0.00";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "3";
                column.FieldName = "Date_3";
                column.PropertiesEdit.DisplayFormatString = "0.00";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "4";
                column.FieldName = "Date_4";
                column.PropertiesEdit.DisplayFormatString = "0.00";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "5";
                column.FieldName = "Date_5";
                column.PropertiesEdit.DisplayFormatString = "0.00";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "6";
                column.FieldName = "Date_6";
                column.PropertiesEdit.DisplayFormatString = "0.00";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "7";
                column.FieldName = "Date_7";
                column.PropertiesEdit.DisplayFormatString = "0.00";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "8";
                column.FieldName = "Date_8";
                column.PropertiesEdit.DisplayFormatString = "0.00";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "9";
                column.FieldName = "Date_9";
                column.PropertiesEdit.DisplayFormatString = "0.00";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "10";
                column.FieldName = "Date_10";
                column.PropertiesEdit.DisplayFormatString = "0.00";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "11";
                column.FieldName = "Date_11";
                column.PropertiesEdit.DisplayFormatString = "0.00";

            });


            settings.Columns.Add(column =>
            {
                column.Caption = "12";
                column.FieldName = "Date_12";
                column.PropertiesEdit.DisplayFormatString = "0.00";

            });


            settings.Columns.Add(column =>
            {
                column.Caption = "13";
                column.FieldName = "Date_13";
                column.PropertiesEdit.DisplayFormatString = "0.00";

            });


            settings.Columns.Add(column =>
            {
                column.Caption = "14";
                column.FieldName = "Date_14";
                column.PropertiesEdit.DisplayFormatString = "0.00";

            });


            settings.Columns.Add(column =>
            {
                column.Caption = "15";
                column.FieldName = "Date_15";
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "16";
                column.FieldName = "Date_16";
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });


            settings.Columns.Add(column =>
            {
                column.Caption = "17";
                column.FieldName = "Date_17";
                column.PropertiesEdit.DisplayFormatString = "0.00";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "18";
                column.FieldName = "Date_18";
                column.PropertiesEdit.DisplayFormatString = "0.00";

            });


            settings.Columns.Add(column =>
            {
                column.Caption = "19";
                column.FieldName = "Date_19";
                column.PropertiesEdit.DisplayFormatString = "0.00";

            });


            settings.Columns.Add(column =>
            {
                column.Caption = "20";
                column.FieldName = "Date_20";
                column.PropertiesEdit.DisplayFormatString = "0.00";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "21";
                column.FieldName = "Date_21";
                column.PropertiesEdit.DisplayFormatString = "0.00";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "22";
                column.FieldName = "Date_22";
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "23";
                column.FieldName = "Date_23";
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "24";
                column.FieldName = "Date_24";
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });


            settings.Columns.Add(column =>
            {
                column.Caption = "25";
                column.FieldName = "Date_25";
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });


            settings.Columns.Add(column =>
            {
                column.Caption = "26";
                column.FieldName = "Date_26";
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });


            settings.Columns.Add(column =>
            {
                column.Caption = "27";
                column.FieldName = "Date_27";
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "28";
                column.FieldName = "Date_28";
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "29";
                column.FieldName = "Date_29";
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "30";
                column.FieldName = "Date_30";
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "31";
                column.FieldName = "Date_31";
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Total Duration";
                column.FieldName = "TotaldShopspentdurationCal";
                column.PropertiesEdit.DisplayFormatString = "0.00";
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
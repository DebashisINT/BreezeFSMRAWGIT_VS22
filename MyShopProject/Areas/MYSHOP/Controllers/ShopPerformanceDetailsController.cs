using BusinessLogicLayer.SalesmanTrack;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilityLayer;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class ShopPerformanceDetailsController : Controller
    {
        ShopPerformanceDetailBL objshop = new ShopPerformanceDetailBL();
        public ActionResult ShopPerformanceDetailsIndex()
        {
            try
            {
                string userid = Session["userid"].ToString();
                ShopPerformanceDetailsModel omodel = new ShopPerformanceDetailsModel();
                omodel.FromDate = DateTime.Now.ToString("dd-MM-yyyy");
                omodel.ToDate = DateTime.Now.ToString("dd-MM-yyyy");
                // omodel.UserID = userid;
                return View(omodel);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetShopPerformanceDetailsPartial(ShopPerformanceDetailsModel model)
        {
            try
            {
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
                List<ShopPerformanceDetailsListModel> omel = new List<ShopPerformanceDetailsListModel>();

                DataTable dt = new DataTable();
                string datfrmat = "";
                string dattoat = "";

                string Employee = "";
                int i = 1;

                if (model.EmployeeID != null && model.EmployeeID.Count > 0)
                {
                    foreach (string item in model.EmployeeID)
                    {
                        if (i > 1)
                            Employee = Employee + "," + item;
                        else
                            Employee = item;
                        i++;
                    }
                }

                string State_id = "";
                int j = 1;
                if (model.State != null && model.State.Count > 0)
                {
                    foreach (string item in model.State)
                    {
                        if (j > 1)
                            State_id = State_id + "," + item;
                        else
                            State_id = item;
                        j++;
                    }
                }

                string Designation_id = "";
                int k = 1;
                if (model.Designation_id != null && model.Designation_id.Count > 0)
                {
                    foreach (string item in model.Designation_id)
                    {
                        if (k > 1)
                            Designation_id = Designation_id + "," + item;
                        else
                            Designation_id = item;
                        k++;
                    }
                }

                string Is_PageLoad = string.Empty;

                if (model.Ispageload == "0")
                {
                    Is_PageLoad = "Ispageload";
                    model.FromDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                    model.ToDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                }

                if (model.FromDate == null)
                {
                    model.FromDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                }
                else
                {
                    model.FromDate = model.FromDate.Split('-')[2] + '-' + model.FromDate.Split('-')[1] + '-' + model.FromDate.Split('-')[0];
                }

                if (model.ToDate == null)
                {
                    model.ToDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                }
                else
                {
                    model.ToDate = model.ToDate.Split('-')[2] + '-' + model.ToDate.Split('-')[1] + '-' + model.ToDate.Split('-')[0];
                }

                string FromDate = model.FromDate;
                string ToDate = model.ToDate;

                if (model.Ispageload == "1")
                {
                     double days = (Convert.ToDateTime(model.ToDate) - Convert.ToDateTime(model.FromDate)).TotalDays;
                     if (days <= 30)
                     {
                         dt = objshop.GetShopPerfgormanceDetails(Employee, FromDate, ToDate, State_id, Designation_id);
                     }
                    if (dt!=null && dt.Rows.Count > 0)
                    {
                        omel = APIHelperMethods.ToModelList<ShopPerformanceDetailsListModel>(dt);
                        TempData["ExportShopPerformanceDetails"] = omel;
                        TempData.Keep();
                    }
                    else
                    {
                        TempData["ExportShopPerformanceDetails"] = null;
                        TempData.Keep();
                    }
                    return PartialView("_PartialGridShopPerformanceDetails", omel);
                }
                else
                {
                    return PartialView("_PartialGridShopPerformanceDetails", omel);
                }
            }
            catch
            {
                //   return Redirect("~/OMS/Signoff.aspx");
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult ExportShopPerformanceDetailslist(int type)
        {
            // List<AttendancerecordModel> model = new List<AttendancerecordModel>();
            ViewData["ExportShopPerformanceDetails"] = TempData["ExportShopPerformanceDetails"];
            TempData.Keep();

            if (ViewData["ExportShopPerformanceDetails"] != null)
            {

                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetShopPerDetailsGridViewSettings(), ViewData["ExportShopPerformanceDetails"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetShopPerDetailsGridViewSettings(), ViewData["ExportShopPerformanceDetails"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetShopPerDetailsGridViewSettings(), ViewData["ExportShopPerformanceDetails"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetShopPerDetailsGridViewSettings(), ViewData["ExportShopPerformanceDetails"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetShopPerDetailsGridViewSettings(), ViewData["ExportShopPerformanceDetails"]);
                    default:
                        break;
                }
            }
            //TempData["Exportcounterist"] = TempData["Exportcounterist"];
            //TempData.Keep();
            return null;
        }

        private GridViewSettings GetShopPerDetailsGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "Shop Performance Details";
            // settings.CallbackRouteValues = new { Controller = "Employee", Action = "ExportEmployee" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Shop Performance Details";

            settings.Columns.Add(column =>
            {
                column.Caption = "Date";
                column.FieldName = "Orderdate";
                column.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "State Name";
                column.FieldName = "StateName";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Employee Name";
                column.FieldName = "EmployeeName";

            });
            settings.Columns.Add(column =>
            {
                column.Caption = "Designation";
                column.FieldName = "deg_designation";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Shop Name";
                column.FieldName = "Shop_Name";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Code";
                column.FieldName = "ENTITYCODE";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Shop Type";
                column.FieldName = "Typename";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Brand";
                column.FieldName = "Brand_Name";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Category";
                column.FieldName = "Category";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Strength";
                column.FieldName = "Strength";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Order No.";
                column.FieldName = "Order_ID";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Product";
                column.FieldName = "sProducts_Name";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Qty";
                column.FieldName = "Product_Qty";
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Rate";
                column.FieldName = "Product_Rate";
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Value";
                column.FieldName = "Product_Price";
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
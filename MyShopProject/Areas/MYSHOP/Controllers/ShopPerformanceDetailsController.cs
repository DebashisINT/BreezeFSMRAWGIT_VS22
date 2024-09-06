/**************************************************************************************************
 * 1.0      Sanchita    V2.0.38     02/02/2023      Appconfig and User wise setting "IsAllDataInPortalwithHeirarchy = True"
 *                                                  then data in portal shall be populated based on Hierarchy Only. Refer: 25504
 * 2.0      Sanchita    V2.0.47     29/05/2024      0027405: Colum Chooser Option needs to add for the following Modules  
 * 3.0      Priti       V2 .0.48    08-07-2024      0027407: "Party Status" - needs to add in the following reports.
 * ****************************************************************************************************/
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
                // Rev 1.0
                string Userid = Convert.ToString(Session["userid"]);
                // End of Rev 1.0

                // Rev 2.0
                DataTable dtColmn = objshop.GetPageRetention(Session["userid"].ToString(), "SHOP WISE PERFORMANCE - DETAIL");
                if (dtColmn != null && dtColmn.Rows.Count > 0)
                {
                    ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
                }
                // End of Rev 2.0


                if (model.Ispageload == "1")
                {
                     double days = (Convert.ToDateTime(model.ToDate) - Convert.ToDateTime(model.FromDate)).TotalDays;
                     if (days <= 30)
                     {
                        // Rev 1.0
                        //dt = objshop.GetShopPerfgormanceDetails(Employee, FromDate, ToDate, State_id, Designation_id);
                        dt = objshop.GetShopPerfgormanceDetails(Employee, FromDate, ToDate, State_id, Designation_id, Userid);
                        // End of Rev 1.0
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
            // Rev 2.0
            DataTable dtColmn = objshop.GetPageRetention(Session["userid"].ToString(), "SHOP WISE PERFORMANCE - DETAIL");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            }
            // End of Rev 2.0

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

                // Rev 2.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Orderdate'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 2.0
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "State Name";
                column.FieldName = "StateName";

                // Rev 2.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='StateName'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 2.0
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Employee Name";
                column.FieldName = "EmployeeName";

                // Rev 2.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='EmployeeName'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 2.0

            });
            settings.Columns.Add(column =>
            {
                column.Caption = "Designation";
                column.FieldName = "deg_designation";

                // Rev 2.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='deg_designation'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 2.0
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Shop Name";
                column.FieldName = "Shop_Name";

                // Rev 2.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Shop_Name'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 2.0
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Code";
                column.FieldName = "ENTITYCODE";

                // Rev 2.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ENTITYCODE'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 2.0
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Shop Type";
                column.FieldName = "Typename";

                // Rev 2.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Typename'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 2.0
            });

            // Rev 3.0
            settings.Columns.Add(column =>
            {
                column.Caption = "Party Status";
                column.FieldName = "PARTYSTATUS";                
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PARTYSTATUS'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }               
            });
            // End of Rev 3.0

            settings.Columns.Add(column =>
            {
                column.Caption = "Brand";
                column.FieldName = "Brand_Name";

                // Rev 2.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Brand_Name'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 2.0
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Category";
                column.FieldName = "Category";

                // Rev 2.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Category'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 2.0
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Strength";
                column.FieldName = "Strength";

                // Rev 2.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Strength'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 2.0
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Order No.";
                column.FieldName = "Order_ID";

                // Rev 2.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Order_ID'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 2.0
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Product";
                column.FieldName = "sProducts_Name";

                // Rev 2.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='sProducts_Name'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 2.0
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Qty";
                column.FieldName = "Product_Qty";
                column.PropertiesEdit.DisplayFormatString = "0.00";

                // Rev 2.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Product_Qty'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 2.0
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Rate";
                column.FieldName = "Product_Rate";
                column.PropertiesEdit.DisplayFormatString = "0.00";

                // Rev 2.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Product_Rate'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 2.0
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Value";
                column.FieldName = "Product_Price";
                column.PropertiesEdit.DisplayFormatString = "0.00";

                // Rev 2.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Product_Price'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 2.0
            });

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

        // Rev 2.0
        public ActionResult PageRetention(List<String> Columns)
        {
            try
            {
                String Col = "";
                int i = 1;
                if (Columns != null && Columns.Count > 0)
                {
                    Col = string.Join(",", Columns);
                }
                int k = objshop.InsertPageRetention(Col, Session["userid"].ToString(), "SHOP WISE PERFORMANCE - DETAIL");

                return Json(k, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
        // End of Rev 2.0
    }
}
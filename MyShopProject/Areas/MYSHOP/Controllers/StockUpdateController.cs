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
using DevExpress.Web;



namespace MyShop.Areas.MYSHOP.Controllers
{
    public class StockUpdateController : Controller
    {

        UserList lstuser = new UserList();
        StockUpdate objshop = new StockUpdate();
        DataTable dtuser = new DataTable();
        DataTable dtstate = new DataTable();
        DataTable dtshop = new DataTable();

        public ActionResult List()
        {
            try
            {
                StockDetailsListInput omodel = new StockDetailsListInput();
                string userid = Session["userid"].ToString();
                dtuser = lstuser.GetUserList(userid);
                dtstate = lstuser.GetStateList();
                dtshop = lstuser.GetShopList();
                // dtuser = lstuser.GetUserList();
                List<GetUserNameStock> model = new List<GetUserNameStock>();

                List<GetStateNamestock> modelstate = new List<GetStateNamestock>();
                List<Getmasterstock> modelshop = new List<Getmasterstock>();

                model = APIHelperMethods.ToModelList<GetUserNameStock>(dtuser);
                modelstate = APIHelperMethods.ToModelList<GetStateNamestock>(dtstate);
                modelshop = APIHelperMethods.ToModelList<Getmasterstock>(dtshop);
                omodel.userlsit = model;
                omodel.statelist = modelstate;
                omodel.shoplist = modelshop;
                if (TempData["Attendanceuser"] != null)
                {
                    omodel.selectedusrid = TempData["Attendanceuser"].ToString();
                    TempData.Clear();
                }
                return View(omodel);

            }
            catch
            {

                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetStockList(StockDetailsListInput model)
        {
            try
            {
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
                List<StockDetailsListOutput> omel = new List<StockDetailsListOutput>();

                DataTable dt = new DataTable();

                if (model.Fromdate == null)
                {
                    model.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                }

                if (model.Todate == null)
                {
                    model.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                }


                string datfrmat = model.Fromdate.Split('-')[2] + '-' + model.Fromdate.Split('-')[1] + '-' + model.Fromdate.Split('-')[0];
                string dattoat = "";


        
                    dt = objshop.GetStockList(model.stateid, model.shopId, datfrmat, dattoat, Int32.Parse(model.selectedusrid));
              




                if (dt.Rows.Count > 0)
                {
                    omel = APIHelperMethods.ToModelList<StockDetailsListOutput>(dt);
                    TempData["Exportattendanceuserwise"] = omel;
                    TempData.Keep();
                }
                else
                {
                    return PartialView("_PartialStocklist", omel);

                }
                return PartialView("_PartialStocklist", omel);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }

        public ActionResult ExportStock(int type)
        {
            List<AttendancerecordModel> model = new List<AttendancerecordModel>();
            if (TempData["Exportattendanceuserwise"] != null)
            {

                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(), TempData["Exportattendanceuserwise"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(), TempData["Exportattendanceuserwise"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(), TempData["Exportattendanceuserwise"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(), TempData["Exportattendanceuserwise"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(), TempData["Exportattendanceuserwise"]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetEmployeeBatchGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "Stock List";
            // settings.CallbackRouteValues = new { Controller = "Employee", Action = "ExportEmployee" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Stock List";


            settings.Columns.Add(column =>
            {
                column.Caption = "User";
                column.FieldName = "UserName";

            });


            settings.Columns.Add(column =>
            {
                column.Caption = "Shop Name";
                column.FieldName = "shop_name";

            });



            settings.Columns.Add(column =>
            {
                column.Caption = "Type";
                column.FieldName = "assign_type";

            });



            settings.Columns.Add(column =>
              {
                  column.Caption = "State";
                  column.FieldName = "State";

              });
            settings.Columns.Add(column =>
              {
                  column.Caption = "Address";
                  column.FieldName = "address";

              });



            settings.Columns.Add(column =>
            {
                column.Caption = "Stock Date";
                column.FieldName = "StockDate";
                column.PropertiesEdit.DisplayFormatString = "dd/MM/yyyy";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Opening Stock Month";
                column.FieldName = "opening_stock_month";


            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Opening Stock Amount";
                column.FieldName = "opening_stock_amount";


            });
            settings.Columns.Add(column =>
            {
                column.Caption = "Closing Stock Month";
                column.FieldName = "closing_stock_month";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Closing Stock Amount";
                column.FieldName = "closing_stock_amount";

            });

            settings.Columns.Add(column =>
                    {
                        column.Caption = "Order Amount";
                        column.FieldName = "order_amount";

                    });
            settings.Columns.Add(column =>
                    {
                        column.Caption = "M/O";
                        column.FieldName = "m_o";

                    });
            settings.Columns.Add(column =>
            {
                column.Caption = "P/O";
                column.FieldName = "p_o";

            });
            settings.Columns.Add(column =>
            {
                column.Caption = "C/O";
                column.FieldName = "c_o";

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
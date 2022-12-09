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
    public class OrderListController : Controller
    {

        UserList lstuser = new UserList();
        OrderList objshop = new OrderList();
        DataTable dtuser = new DataTable();
        DataTable dtstate = new DataTable();
        DataTable dtshop = new DataTable();

        public ActionResult List()
        {
            try
            {

                OrderDetailsListInput omodel = new OrderDetailsListInput();
                string userid = Session["userid"].ToString();
                dtuser = lstuser.GetUserList(userid);
                dtstate = lstuser.GetStateList();
                dtshop = lstuser.GetShopList();
                // dtuser = lstuser.GetUserList();
                List<GetUserName> model = new List<GetUserName>();

                List<GetStateName> modelstate = new List<GetStateName>();
                List<Getmaster> modelshop = new List<Getmaster>();
                model = APIHelperMethods.ToModelList<GetUserName>(dtuser);
                modelstate = APIHelperMethods.ToModelList<GetStateName>(dtstate);
                modelshop = APIHelperMethods.ToModelList<Getmaster>(dtshop);


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


        public ActionResult GetOrderList(OrderDetailsListInput model)
        {
            try
            {
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
                List<OrderDetailsListOutput> omel = new List<OrderDetailsListOutput>();

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


             
                    dt = objshop.GetallorderList(model.stateid, model.shopId, datfrmat, dattoat, Int32.Parse(model.selectedusrid));
              




                if (dt.Rows.Count > 0)
                {
                    omel = APIHelperMethods.ToModelList<OrderDetailsListOutput>(dt);
                    TempData["Exportattendanceuserwise"] = omel;
                    TempData.Keep();
                }
                else
                {
                    return PartialView("_PartialOrderList", omel);

                }
                return PartialView("_PartialOrderList", omel);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }

        public ActionResult ExportOrderList(int type)
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
            settings.Name = "Order List";
            // settings.CallbackRouteValues = new { Controller = "Employee", Action = "ExportEmployee" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Order List";

            settings.Columns.Add(column =>
            {
                column.Caption = "User";
                column.FieldName = "UserName";

            });
            settings.Columns.Add(column =>
                 {
                     column.Caption = "Sho Name";
                     column.FieldName = "shop_name";

                 });


            settings.Columns.Add(column =>
            {
                column.Caption = "Type";
                column.FieldName = "Shoptype";

            });

            settings.Columns.Add(column =>
                {
                    column.Caption = "Address";
                    column.FieldName = "address";

                });


            settings.Columns.Add(column =>
                {
                    column.Caption = "Owner Name";
                    column.FieldName = "owner_name";

                });

            settings.Columns.Add(column =>
                {
                    column.Caption = "Contact";
                    column.FieldName = "owner_contact_no";

                });


            settings.Columns.Add(column =>
                 {
                     column.Caption = "State";
                     column.FieldName = "State";

                 });


            settings.Columns.Add(column =>
            {
                column.Caption = "Order Date";
                column.FieldName = "Orderdate";
             

            });



            settings.Columns.Add(column =>
            {
                column.Caption = "Order Value";
                column.FieldName = "order_amount";



            });
            settings.Columns.Add(column =>
            {
                column.Caption = "Collection";
                column.FieldName = "collection";


            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Description";
                column.FieldName = "Order_Description";


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